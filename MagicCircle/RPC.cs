namespace MagicCircle;

/// Magic Circle message structure
public class RPC {
    public string? cmd { get; set; }
}

public class SetItemRPC : RPC {
    public string? item { get; set; }
    public string? key { get; set; }
    public string? value { get; set; }
}

public class MessageRPC : RPC {
    public string? type { get; set; }
    public string? text { get; set; }
    public string? author { get; set; }
    public string? whisper { get; set; }

    public RollInfo? metadata { get; set; }
}

public class RollInfo {
    public string? kind { get; set; }
    
    public List<string>? tags { get; set; }
    
    public List<int>? dice { get; set; }
    public string? suffix { get; set; }
    public List<int>? results { get; set; }
    
    public int? total { get; set; }
}