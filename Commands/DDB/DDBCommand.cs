using System.Security.Authentication;
using System.Text.Json;
using MagicCircle;
using OBRemote.Commands.Server;
using WebSocketSharp;
using ErrorEventArgs = WebSocketSharp.ErrorEventArgs;

namespace OBRemote.Commands.DDB;

[Command(
    Name = "ddb",
    Description = "connect to a DDB campaign game log and relay events to magic circle",
    ShortHelp = ""
)]
public class DDBCommand : RPCCommand {
    DDBConfig? config;
    
    WebSocket? ddbSocket;
    
    bool localMode;
    ServerCommand? localServer;
    
    public override void Execute(List<string> args, Dictionary<string, string>? options) {
        LoadConfig();
        
        localMode = true;
        
        if(localMode) {
            localServer = new();
            localServer.StartServer(args, options);
        } else {
            base.Execute(args, options);
        }
        
        var task = Task.Run(async () => await RelayMain());
        task.Wait();
        
        if(!localMode) Close();
    }
    
    void LoadConfig(string path = "ddb.json") {
        using var file = File.OpenRead(path);
        config = JsonSerializer.Deserialize<DDBConfig>(file);
        if(config == null) throw new FormatException("Configuration file error");
    }
    
    async Task RelayMain() {
        const string gameLogUrl = "wss://game-log-api-live.dndbeyond.com/v1?";
        
        var stt = await AcquireSTT();
        
        ddbSocket = new(gameLogUrl + $"gameId={config?.gameId}&userId={config?.userId}&stt={stt}");
        ddbSocket.Origin = "https://www.dndbeyond.com";
        ddbSocket.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls12;
        ddbSocket.OnMessage += OnMessage;
        ddbSocket.OnError += (_, e) => {
            Console.Error.WriteLine($"{e.Exception} {e.Message}");
            Environment.Exit(10);
        };
        
        ddbSocket.Connect();
        
        Console.WriteLine("connected to DDB");
        
        var pingTask = Task.Run(async () => {
            while(true) {
                await Task.Delay(1000 * 60 * 5);
                ddbSocket.Send("{\"data\": \"ping\"");
            }
        });
        
        await Task.Delay(-1);
    }
    
    async Task<string> AcquireSTT() {
        const string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/113.0.0.0 Safari/537.36";
        using HttpClient client = new();
        var headers = client.DefaultRequestHeaders;
        headers.Clear();
        
        headers.Add("User-Agent", userAgent);
        headers.Add("Origin", "https://www.dndbeyond.com");
        headers.Add("Referer", "https://www.dndbeyond.com");
        headers.Add("Cookie", "CobaltSession=" + config?.token);
        
        var response = await client.PostAsync("https://auth-service.dndbeyond.com/v1/cobalt-token", null);
        if(!response.IsSuccessStatusCode) {
            throw new AuthenticationException($"token acqusition failed: {response.ReasonPhrase}");
        }
        
        var content = await response.Content.ReadAsStreamAsync();
        var token = await JsonSerializer.DeserializeAsync<CobaltToken>(content);
        if(token == null) {
            throw new FormatException("token response malformed");
        }
        
        return token.token ?? "";
    }
    
    void OnMessage(object? sender, MessageEventArgs args) {
        if(args.Data == "pong") return;

        var evt = JsonSerializer.Deserialize<DDBEvent>(args.Data);
        if(evt == null) throw new IOException($"ddb message error {args.Data}");

        Console.WriteLine($"incoming from DDB: {evt.eventType}");
        var rpc = TranslateEvent(evt);
        if(rpc == null) return;
        
        if(localMode) {
            localServer?.Send(JsonSerializer.Serialize(rpc));
        } else {
            Send(JsonSerializer.Serialize(rpc));
        }
    }
    
    MessageRPC? TranslateEvent(DDBEvent evt) {
        switch(evt.eventType) {
            case "dice/roll/fulfilled":
                return TranslateRollEvent(evt);
        }
        return null;
    }
    
    MessageRPC TranslateRollEvent(DDBEvent evt) {
        var rolls = new List<int>();
        var results = new List<int>();
        foreach(var set in evt.data!.rolls![0].diceNotation!.set!) {
            if(set.dice is null) continue;;
            foreach(var dice in set.dice) {
                rolls.Add(int.Parse(dice.dieType?[1..]!));
                results.Add(dice.dieValue ?? 0);
            }
        }
        
        // intrepet the ddb information into magic circle roll kinds
        string text = Char.ToUpper(evt.data.action![0]) + evt.data.action.Substring(1);
        
        string kind = evt.data.rolls[0].rollType ?? "";
        string suffix = "";
        bool suppressKind = false;
        
        if(evt.data.action == "Initiative") {
            kind = "initiative";
            suppressKind = true;
        }
        else if(kind == "to hit") kind = "attack";
        else if(kind == "heal") kind = "rest";

        var tags = new List<string>();
        if(evt.data.rolls[0].rollKind != "") tags.Add(evt.data.rolls[0].rollKind ?? "");

        switch(evt.data?.rolls[0].rollKind) {
            case "advantage":
                suffix += "kh";
                break;
            case "disadvantage":
                suffix += "kl";
                break;
        }

        var modifier = evt.data?.rolls[0].diceNotation?.constant ?? 0;
        suffix += modifier switch {
            > 0 => "+" + modifier,
            0 => "",
            < 0 => "-" + modifier
        };

        return new MessageRPC{
            cmd = "msg",
            type = "dice",
            text = suppressKind ? text : $"{text} {kind}",
            author = evt.data!.context!.name,
            metadata = new RollInfo{
                kind = kind,
                tags = tags.Count > 0 ? tags : null,
                dice = rolls,
                results = results,
                suffix = suffix,
                total = evt.data!.rolls![0].result!.total
            }
        };
    }
}