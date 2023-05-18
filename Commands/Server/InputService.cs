using WebSocketSharp;
using WebSocketSharp.Server;
using ErrorEventArgs = WebSocketSharp.ErrorEventArgs;

namespace OBRemote.Commands.Server;

public class InputService : WebSocketBehavior {
    ServerCommand server;

    public InputService(ServerCommand srv) {
        server = srv;
    }
    
    protected override void OnOpen() {
        base.OnOpen();
        Console.WriteLine($"stream opened {Context.RequestUri}");
    }

    protected override void OnClose(CloseEventArgs e) {
        base.OnClose(e);
    }

    protected override void OnError(ErrorEventArgs e) {
        base.OnError(e);
    }

    protected override void OnMessage(MessageEventArgs e) {
        server.Send(e.Data);
    }
}