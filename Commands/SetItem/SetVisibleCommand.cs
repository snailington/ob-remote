namespace OBRemote.Commands.SetItem;

[Command(
    Name = "visible",
    Description = "set an item's visibility",
    ShortHelp = "<guid> (true|false)",
    Sort = 200
)]
public class SetVisibleCommand : SetItemCommand {
    public override void Execute(List<string> args, Dictionary<string, string>? options) {
        if(args.Count < 3) throw new ArgumentOutOfRangeException();
        Set(args);
    }
}