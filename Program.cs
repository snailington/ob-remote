using System.Reflection;
using System.Security.Authentication;
using OBRemote.Commands;

var commands = Assembly.GetExecutingAssembly().GetTypes()
    .Where(t => t.IsAssignableTo(typeof(ICommand)) && t.GetCustomAttribute<CommandAttribute>() != null)
    .Select(t => (Type: t, Attribute: t.GetCustomAttribute<CommandAttribute>()!))
    .OrderBy(t => t.Attribute.Sort)
    .ToList();

if(args.Length == 0) {
    PrintHelp();
    return 1;
}

(Type Type, CommandAttribute Attribute) cmdInfo = commands.FirstOrDefault(t => t.Attribute.Name == args[0]);
if(cmdInfo.Type == null) { PrintHelp(); return 1; }

try {
    var cmd = (ICommand?)Activator.CreateInstance(cmdInfo.Type);
    if(cmd == null) { PrintHelp(); Environment.Exit(1); return 1; }
    cmd.Execute(new List<string>(args), null);
} catch(ArgumentOutOfRangeException) {
    PrintShortHelp(cmdInfo.Attribute);
    return 2;
} catch(NotImplementedException) {
    Console.Error.WriteLine($"{args[0]} not implemented");
    return 3;
} catch(AuthenticationException e) {
    Console.Error.WriteLine(e.Message);
    Console.Error.WriteLine("Check your DDB credentials and try again.");
    return 4;
} catch(Exception e) {
    Console.Error.WriteLine($"{e.Message}");
    return 5;
}

return 0;

void PrintHelp() {
    Console.WriteLine($"usage: {System.AppDomain.CurrentDomain.FriendlyName} <command>");
    Console.WriteLine("commands:");
    foreach(var command in commands) {
        if(command.Attribute.Hidden) continue;
        Console.WriteLine($"\t{command.Attribute.Name} - {command.Attribute.Description}");
    }
}

void PrintShortHelp(CommandAttribute cmd) {
    Console.WriteLine($"usage: {AppDomain.CurrentDomain.FriendlyName} {cmd.Name} {cmd.ShortHelp}");
}