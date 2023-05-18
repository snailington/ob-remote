namespace OBRemote.Commands.SetItem;

[Command(
    Name = "attach",
    Description = "attach an item to another",
    ShortHelp = "<guid> <target>",
    Sort = 200
)]
public class SetAttachmentCommand : SetItemCommand {
    public override void Execute(List<string> args, Dictionary<string, string>? options) {
        if(args.Count < 3) throw new ArgumentOutOfRangeException();
        Set("attachedTo", args);
    }
}