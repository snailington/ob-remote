namespace OBRemote.Commands.DDB;

public class DDBEvent {
    public string id { get; set; }
    public string userId { get; set; }
    public string source { get; set; }
    public string gameId { get; set; }
    public bool persist { get; set; }
    public string dateTime { get; set; }
    public string entityId { get; set; }
    public string entityType { get; set; }
    public string eventType { get; set; }
    
    public EventData data { get; set; }
}

public class EventData {
    public string action { get; set; }
    public string setId { get; set; }
    public EventContext context { get; set; }
    public List<EventRoll> rolls { get; set; }
}

public class EventContext {
    public string avatarUrl { get; set; }
    public string entityId { get; set; }
    public string entityType { get; set; }
    public string messageScope { get; set; }
    public string messageTarget { get; set; }
    public string name { get; set; }
}

public class EventRoll {
    public string diceNotationStr { get; set; }
    public string rollKind { get; set; }
    public string rollType { get; set; }
    public EventRollDiceNotation diceNotation { get; set; }
    public EventRollResult result { get; set; }
}

public class EventRollDiceNotation {
    public int constant { get; set; }
    public List<EventRollDiceSet> set { get; set; }
}

public class EventRollDiceSet {
    public int count { get; set; }
    public List<EventRollDice> dice { get; set; }
    public string dieType { get; set; }
    public int operand { get; set; }
    public int operation { get; set; }
}

public class EventRollDice {
    public string dieType { get; set; }
    public int dieValue { get; set; }
}

public class EventRollResult {
    public int constant { get; set; }
    public string text { get; set; }
    public int total { get; set; }
    public List<int> values { get; set; }
}