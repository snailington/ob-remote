namespace OBRemote.Commands.SetItem;

[Command(
    Name = "name",
    Description = "set an item's name",
    ShortHelp = "<guid> value",
    Sort = 200
)]
public class SetNameCommand : SetItemCommand {
    public override void Execute(List<string> args, Dictionary<string, string>? options) {
        if(args.Count < 3) throw new ArgumentOutOfRangeException();
        Set(args);
    }
}