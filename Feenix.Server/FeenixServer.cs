using Feenix.Server.Configuration;
using HeavyNetwork;

namespace Feenix.Server;

/// <summary>
/// The main component for the Feenix server. From here the networking is being managed.
/// </summary>
internal class FeenixServer : HeavyServer<FeenixClient>
{
    /// <summary>
    /// The singleton running instance of this <see cref="FeenixServer"/> for internal usage.
    /// </summary>
    internal static FeenixServer Instance { get; } = new();
    
    /// <summary>
    /// Creates a new feenix server.
    /// </summary>
    private FeenixServer(IServiceProvider? serviceProvider = null) 
        : base(BuildOptions(), serviceProvider)
    {
    }

    /// <summary>
    /// Creates the <see cref="HeavyServerOptions"/> for this server.
    /// </summary>
    private static HeavyServerOptions BuildOptions()
    {
        var config = Config.Current;

        var options = new HeavyServerOptions
        {
            Host = config.General.IsLocal ? "127.0.0.1" : "0.0.0.0",
            Port = config.General.Port
        };

        return options;
    }
}