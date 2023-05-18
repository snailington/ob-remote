namespace OBRemote.Commands.SetItem;

[Command(
    Name = "layer",
    Description = "set item's layer",
    ShortHelp = "<guid> <value>",
    Sort = 200
)]
public class SetLayerCommand : SetItemCommand {
    public override void Execute(List<string> args, Dictionary<string, string>? options) {
        if(args.Count < 3) throw new ArgumentOutOfRangeException();
        Set(args);
    }
}