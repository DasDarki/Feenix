using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Feenix.Common;

/// <summary>
/// The logger is a utility component allowing logging of messages to the console for debugging purposes.
/// </summary>
public static class Logger
{
    /// <summary>
    /// Whether the debug mode is enabled or not.
    /// </summary>
    public static bool IsDebug { get; set; } = false;
    
    private static object _lock = new();

    public static void Info(string message, params object[] args)
    {
        PrintFormatted(ConsoleColor.Blue, "INFO", message, args);
    }

    public static void Success(string message, params object[] args)
    {
        PrintFormatted(ConsoleColor.DarkGreen, "SUCCESS", message, args);
    }
    
    public static void Warning(string message, params object[] args)
    {
        PrintFormatted(ConsoleColor.DarkYellow, "WARNING", message, args);
    }
    
    public static void Error(Exception ex, string message, params object[] args)
    {
        PrintFormatted(ConsoleColor.Red, "ERROR", message, args);
        Console.WriteLine(ex);
    }
    
    public static void Fatal(string message, params object[] args)
    {
        PrintFormatted(ConsoleColor.DarkRed, "FATAL", message, args);
    }
    
    public static void Debug(string message, params object[] args)
    {
        if (!IsDebug)
        {
            return;
        }
        
        PrintFormatted(ConsoleColor.White, "DEBUG", message, args);
    }

    /// <summary>
    /// Logs the title card of the application. The title card consists of the "Feenix" title, the type of the
    /// application, the version and the author.
    /// </summary>
    public static void ShowTitleCard()
    {
        var execution = Assembly.GetCallingAssembly();
        
        var isServer = execution.GetName().Name?.StartsWith("Feenix.Server");
        if (!isServer.HasValue)
        {
            return;
        }
        
        var type = isServer.Value ? "Server" : "Client";
        var version = execution.GetName().Version;
        
        Console.WriteLine($@"
     /$$$$$$$$                            /$$
    | $$_____/                           |__/
    | $$     /$$$$$$   /$$$$$$  /$$$$$$$  /$$ /$$   /$$
    | $$$$$ /$$__  $$ /$$__  $$| $$__  $$| $$|  $$ /$$/
    | $$__/| $$$$$$$$| $$$$$$$$| $$  \\ $$| $$ \\  $$$$/
    | $$   | $$_____/| $$_____/| $$  | $$| $$  >$$  $$
    | $$   |  $$$$$$$|  $$$$$$$| $$  | $$| $$ /$$/\\  $$
    |__/    \\_______/ \\_______/|__/  |__/|__/|__/  \\__/ {type} v{version} by github.com/DasDarki
");
    }

    private static void PrintFormatted(ConsoleColor prefixColor, string prefix, string message, params object[] args)
    {
        lock (_lock)
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("[");
            Console.ForegroundColor = prefixColor;
            Console.Write(prefix);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("] ");
            Console.ResetColor();

            var index = 0;
            var count = 0;
            var words = message.Split(' ');
            foreach (var word in words)
            {
                var isLast = count == words.Length - 1;
                
                if (word == "{}")
                {
                    if (index >= args.Length)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("{{out of bounds}}");
                        Console.ResetColor();
                    }
                    else
                    {
                        var obj = args[index];
                        switch (obj)
                        {
                            case sbyte or byte or short or ushort or int or uint or long or ulong or float or double or decimal:
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write(obj);
                                break;
                            case bool:
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(obj);
                                break;
                            case string:
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write(obj);
                                break;
                            default:
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.Write(Encoding.UTF8.GetString(JsonSerializer.SerializeToUtf8Bytes(obj)));
                                break;
                        }
                        
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.Write(word);
                }

                if (!isLast)
                {
                    Console.Write(" ");
                }
                
                count++;
            }
            
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}