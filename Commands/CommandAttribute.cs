namespace OBRemote.Commands;

[AttributeUsage(AttributeTargets.Class)]
public class CommandAttribute: Attribute {
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ShortHelp { get; set; }
    public int Sort { get; set; } = 0;
    public bool Hidden { get; set; } = false;
}