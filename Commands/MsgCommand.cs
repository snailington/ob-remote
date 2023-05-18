namespace OBRemote.Commands;

[Command(
    Name = "msg",
    Description = "send a chat message",
    ShortHelp = "<message>"
)]
public class MsgCommand : RPCCommand {
    public override void Execute(List<string> args, Dictionary<string, string>? options) {
        if(args.Count < 2) throw new ArgumentOutOfRangeException();
        base.Execute(args, options);
        
        Send(new MagicCircle.MessageRPC{
            cmd = "msg",
            type = "chat",
            text = String.Join(' ', args.Skip(1))
        });
        
        Close();
    }
}