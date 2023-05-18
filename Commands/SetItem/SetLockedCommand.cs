namespace OBRemote.Commands.SetItem;

[Command(
    Name = "lock",
    Description = "set an item's lock status",
    ShortHelp = "<guid> (true|false)",
    Sort = 200
)]
public class SetLockedCommand : SetItemCommand {
    public override void Execute(List<string> args, Dictionary<string, string>? options) {
        if(args.Count < 3) throw new ArgumentOutOfRangeException();
        Set("locked", args);
    }
}