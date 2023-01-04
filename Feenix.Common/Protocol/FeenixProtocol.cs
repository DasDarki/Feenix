using Feenix.Common.Protocol.Api;
using Feenix.Common.Protocol.Client;
using Feenix.Common.Protocol.Server;

namespace Feenix.Common.Protocol;

/// <summary>
/// Registers all the packets for the protocol in the packet registry.
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