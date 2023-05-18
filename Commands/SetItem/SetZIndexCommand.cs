namespace OBRemote.Commands.SetItem;

[Command(
    Name = "zindex",
    Description = "set item's z-index",
    ShortHelp = "<guid> <value>",
    Sort = 200
)]
public class SetZIndexCommand : SetItemCommand {
    public override void Execute(List<string> args, Dictionary<string, string>? options) {
        if(args.Count < 3) throw new ArgumentOutOfRangeException();
        Set("zIndex", args);
    }
}