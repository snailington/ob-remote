namespace OBRemote.Commands;

[Command(
    Name = "set",
    Description = "set a metadata value",
    ShortHelp = "(room|scene|item|player) [<guid>] <value>",
    Sort = 100
)]
public class SetCommand : RPCCommand {
    public override void Execute(List<string> args, Dictionary<string, string>? options) {
        throw new NotImplementedException();
    }
}