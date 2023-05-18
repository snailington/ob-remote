namespace OBRemote.Commands.SetItem;

[Command(
    Name = "disable-hit",
    Description = "enable/disable an item's hit detection",
    ShortHelp = "<guid> <value>",
    Sort = 200
)]
public class SetDisableHitCommand : SetItemCommand {
    public override void Execute(List<string> args, Dictionary<string, string>? options) {
        if(args.Count < 3) throw new ArgumentOutOfRangeException();
        Set("disableHit", args);
    }
}