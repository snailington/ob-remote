using System.Text.Json;

namespace OBRemote.Commands.DDB;

public class CobaltToken {
    public string? token { get; set; }
    public int? ttl { get; set; }
}