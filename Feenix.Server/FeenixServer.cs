using Feenix.Server.Configuration;
using LiteNetwork.Server;

namespace Feenix.Server;

/// <summary>
/// The main component for the Feenix server. From here the networking is being managed.
/// </summary>
internal class FeenixServer : LiteServer<FeenixClient>
{
    /// <summary>
    /// The singleton running instance of this <see cref="FeenixServer"/> for internal usage.
    /// </summary>
    internal static FeenixServer Instance { get; } = new();
    
    /// <summary>
    /// Creates a new feenix server.
    /// </summary>
    private FeenixServer() 
        : base(BuildOptions())
    {
    }

    /// <summary>
    /// Creates the <see cref="LiteServerOptions"/> for this server.
    /// </summary>
    private static LiteServerOptions BuildOptions()
    {
        var config = Config.Current;

        var options = new LiteServerOptions
        {
            Host = config.General.IsLocal ? "127.0.0.1" : "0.0.0.0",
            Port = config.General.Port
        };

        return options;
    }
}