namespace OBRemote.Commands;

[Command(
    Name = "raw",
    Description = "send a raw rpc packet",
    ShortHelp = "<data>",
    Hidden = true
)]
public class RawCommand : RPCCommand {
    public override void Execute(List<string> args, Dictionary<string, string>? options) {
        if(args.Count < 2) throw new ArgumentOutOfRangeException();
        base.Execute(args, options);
        
        Send(String.Join(' ', args.Skip(1)));
        
        Close();
    }
}