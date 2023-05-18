using System.Text.Json;
using WebSocketSharp.Server;
using ErrorEventArgs = WebSocketSharp.ErrorEventArgs;

namespace OBRemote.Commands.Server;

public class OutputService : WebSocketBehavior {
    ServerCommand server;
    
    public OutputService(ServerCommand srv) {
        server = srv;
        server.OnOut = Send;
    }

    protected override void OnOpen() {
        Console.WriteLine($"connection opened: {Context.Origin}");
    }

    protected override void OnError(ErrorEventArgs e) {
        Console.WriteLine($"relay stream error {e.Message}");
    }
}