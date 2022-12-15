using Feenix.CLI.Commands;

namespace Feenix.CLI;

internal static class Program
{
    private static readonly List<Command> Commands = new()
    {
        new SignerCommand()
    };

    internal static void Main(string[] args)
    {
        if (args.Length <= 0)
        {
            ShowHelp();
            return;
        }

        try
        {
            if (args[0] == "help")
            {
                ShowHelp();
                return;
            }
            
            var command = Commands.FirstOrDefault(c => string.Equals(c.Name, args[0], StringComparison.OrdinalIgnoreCase));
            if (command != null)
            {
                command.Execute(new CommandArgs(args.Skip(1).ToArray()));
            }
            else
            {
                throw new Exception("Unknown command. Use feenix help!");
            }
        }
        catch (Exception e)
        {
            Console.ResetColor();
            Console.Write("Feenix CLI threw an error: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.Message);
            Console.ResetColor();
        } 
    }

    private static void ShowHelp()
    {
        Console.WriteLine("All Feenix CLI commands:");
        foreach (var command in Commands)
        {
            foreach (var usage in command.Usages)
            {
                Console.WriteLine($"  {command.Name} ${usage}");
            }
        }
    }
}