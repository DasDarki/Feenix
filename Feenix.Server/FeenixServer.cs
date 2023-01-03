using Feenix.Common;
using Feenix.Common.Protocol.Client;
using Feenix.Common.Protocol.Server;
using Feenix.Server.Configuration;
using Neptunium;
using Neptunium.Server;

namespace Feenix.Server;

/// <summary>
/// The main component for the Feenix server. From here the networking is being managed.
/// </summary>
internal class FeenixServer
{
    /// <summary>
    /// The singleton instance of the running Feenix server.
    /// </summary>
    internal static FeenixServer Instance { get; set; } = null!;
    
    private readonly IServer<FeenixClient>? _server;

    internal FeenixServer()
    {
        _server = Network.CreateAsync<FeenixClient>(
            Config.Current.General.IsLocal,
            Config.Current.General.Port,
            string.IsNullOrEmpty(Config.Current.General.Password) ? null : Config.Current.General.Password
        ).GetAwaiter().GetResult();
        
        PacketHandler.Scan(this);
    }

    /// <summary>
    /// Starts the server.
    /// </summary>
    internal void Start()
    {
        if (_server == null)
        {
            Logger.Fatal("Failed to initialize server.");
            return;
        }
        
        Logger.Info("Starting Feenix server...");
        _server.StartAsync().GetAwaiter().GetResult();
        
        Logger.Info("Feenix server started.");
    }

    [PacketHandler(typeof(PingPacket))]
    internal void OnPingPacket(FeenixClient client, PingPacket packet)
    {
        var reverse = packet.Message.Reverse().ToArray();
        
        Console.WriteLine("Received ping packet from {0} with message: {1}", client, packet.Message);
        
        client.SendPacket(new PongPacket
        {
            Message = new string(reverse)
        });
    }
}