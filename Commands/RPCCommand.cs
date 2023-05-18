using System.Text.Json;
using WebSocketSharp;
using ErrorEventArgs = WebSocketSharp.ErrorEventArgs;

namespace OBRemote.Commands;

public abstract class RPCCommand : ICommand {
    WebSocket? socket;
    
    public virtual void Execute(List<string> args, Dictionary<string, string>? options) {
        socket = new("ws://localhost:12210/in");
        socket.OnError += (_, e) => OnError(e);
        socket.OnMessage += (_, e) => OnMessage(e);
        socket.Connect();
    }
    
    public void Send(string data) {
        socket?.Send(data);
    }
    
    public void Send<T>(T rpc) {
        Send(JsonSerializer.Serialize(rpc));
    }
    
    public void Close() {
        // fixme: packet doesn't send if we close immediately after, websocket-sharp doesn't flush stream?
        Thread.Sleep(50);
        socket?.Close(1001);
    }
    
    public virtual void OnMessage(MessageEventArgs evt) {}
    public virtual void OnError(ErrorEventArgs evt) {}
}