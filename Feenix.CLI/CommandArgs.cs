namespace Feenix.CLI;

/// <summary>
/// The command arguments passed to the command.
/// </summary>
internal class CommandArgs
{
    private readonly List<string> _args;

    internal CommandArgs(IEnumerable<string> args)
    {
        _args = new List<string>();
        
        var inQuotes = false;
        var currentArg = string.Empty;
        foreach (var arg in args)
        {
            if (arg.StartsWith("\""))
            {
                inQuotes = true;
                currentArg = arg[1..];
            }
            else if (arg.EndsWith("\""))
            {
                inQuotes = false;
                currentArg += string.Concat(" ", arg.AsSpan(0, arg.Length - 1));
                _args.Add(currentArg);
                currentArg = string.Empty;
            }
            else if (inQuotes)
            {
                currentArg += " " + arg;
            }
            else
            {
                _args.Add(arg);
            }
        }
    }
    
    internal bool Subcommand(string name)
    {
        return _args.Count > 0 && string.Equals(_args[0], name, StringComparison.OrdinalIgnoreCase);
    }

    internal bool Exists(string name)
    {
        var arg = _args.FirstOrDefault(a => a.StartsWith("-" + name, StringComparison.OrdinalIgnoreCase));
        return arg != null;
    }

    internal string Arg(int idx)
    {
        if (idx >= _args.Count)
            throw new ArgumentException("Required argument at index " + idx + " not found.");
        
        return _args[idx];
    }

    internal string Required(string name)
    {
        var arg = _args.FirstOrDefault(a => a.StartsWith("-" + name, StringComparison.OrdinalIgnoreCase));
        if (arg == null)
        {
            throw new ArgumentException($"Required argument '-{name}' not found.");
        }
        
        var parts = arg.Split('=');
        if (parts.Length != 2)
        {
            throw new ArgumentException($"Required argument '-{name}' is missing a value.");
        }
        
        return parts[1];
    }

    internal string Optional(string name, string def = "")
    {
        var arg = _args.FirstOrDefault(a => a.StartsWith("-" + name, StringComparison.OrdinalIgnoreCase));
        if (arg == null)
        {
            return def;
        }
        
        var parts = arg.Split('=');
        if (parts.Length != 2)
        {
            return def;
        }
        
        return parts[1];
    }
}