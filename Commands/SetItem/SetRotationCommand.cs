namespace OBRemote.Commands.SetItem;

[Command(
    Name = "rotation",
    Description = "set item's rotation",
    ShortHelp = "<guid> <value>",
    Sort = 200
)]
public class SetRotationCommand : SetItemCommand {
    public override void Execute(List<string> args, Dictionary<string, string>? options) {
        if(args.Count < 3) throw new ArgumentOutOfRangeException();
        Set(args);
    }
}