namespace OBRemote.Commands.SetItem;

[Command(
    Name = "position",
    Description = "set item's position",
    ShortHelp = "<guid> <x> <y>",
    Sort = 200
)]
public class SetPositionCommand : SetItemCommand {
    public override void Execute(List<string> args, Dictionary<string, string>? options) {
        if(args.Count < 4) throw new ArgumentOutOfRangeException();
        Set(args);
    }
}