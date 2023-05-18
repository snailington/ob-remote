namespace OBRemote.Commands;

public interface ICommand {
    void Execute(List<string> args, Dictionary<string, string>? options);
}