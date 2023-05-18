namespace OBRemote.Commands;

[Command(
    Name = "get",
    Description = "get a metadata value",
    ShortHelp = "(room|scene|item|player) [<guid>]",
    Sort = 100
)]
public class GetCommand : RPCCommand {
    public override void Execute(List<string> args, Dictionary<string, string>? options) {
        throw new NotImplementedException();
    }
}