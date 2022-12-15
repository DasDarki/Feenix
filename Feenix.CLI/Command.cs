namespace Feenix.CLI;

/// <summary>
/// The command is the base class of the CLI.
/// </summary>
internal abstract class Command
{
    internal string Name { get; }
    
    internal string[] Usages { get; }

    protected Command(string name, params string[] usages)
    {
        Name = name;
        Usages = usages;
    }

    internal abstract void Execute(CommandArgs args);
}