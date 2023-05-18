using WebSocketSharp.Server;

namespace OBRemote.Commands.Server;

[Command(
    Name = "server",
    Description = "launch a server to relay local RPC commands to owlbear via magic circle",
    ShortHelp = "",
    Sort = -100
)]
public class ServerCommand : ICommand {
    WebSocketServer? server;
    Task? task;
    
    public Action<string>? OnOut;
    
    public void Execute(List<string> args, Dictionary<string, string>? options) {
        StartServer(args, options).Wait();
    }
    
    public Task StartServer(List<string> args, Dictionary<string, string>? options) {
        server = new WebSocketServer(12210);

        server.AddWebSocketService("/", () => new OutputService(this));
        server.AddWebSocketService("/in", () => new InputService(this));

        Console.WriteLine("Source import string:");
        Console.WriteLine($"\t{{\"name\":\"remote\",\"type\":\"websocket\",\"perms\":\"rwm\",\"url\":\"ws://localhost:{server.Port}/\"}}\n\n");

        task = Task.Run(async () => await ServerMain());
        return task;
    }
    
    public void Send(string data) {
        OnOut?.Invoke(data);
    }
    
    protected async Task ServerMain() {
        server?.Start();
        await Task.Delay(-1);
    }
}