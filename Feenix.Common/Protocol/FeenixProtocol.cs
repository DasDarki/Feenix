using Feenix.Common.Protocol.Client;
using Feenix.Common.Protocol.Server;
using Neptunium.Protocol;

namespace Feenix.Common.Protocol;

/// <summary>
/// Registers all the packets for the protocol in the neptunium packet registry.
/// </summary>
public static class FeenixProtocol
{
    /// <summary>
    /// Initializes the protocol.
    /// </summary>
    public static void Initialize()
    {
        PacketRegistry.Register<PingPacket>();
        PacketRegistry.Register<PongPacket>();
    }
}