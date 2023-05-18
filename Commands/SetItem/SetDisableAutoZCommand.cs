namespace OBRemote.Commands.SetItem;

[Command(
    Name = "disable-autoz",
    Description = "enable/disable an item's auto Z sorting",
    ShortHelp = "<guid> <value>",
    Sort = 200
)]
public class SetDisableAutoZCommand : SetItemCommand {
    public override void Execute(List<string> args, Dictionary<string, string>? options) {
        if(args.Count < 3) throw new ArgumentOutOfRangeException();
        Set("disableAutoZIndex", args);
    }
}