namespace OBRemote.Commands.SetItem;

[Command(
    Name = "scale",
    Description = "set item's scale",
    ShortHelp = "<guid> <x> <y>",
    Sort = 200
)]
public class SetScaleCommand : SetItemCommand {
    public override void Execute(List<string> args, Dictionary<string, string>? options) {
        if(args.Count < 4) throw new ArgumentOutOfRangeException();
        Set(args);
    }
}