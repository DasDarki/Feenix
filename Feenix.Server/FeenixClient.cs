using Feenix.Common;
using Feenix.Common.Protocol.Client;
using Feenix.Common.Protocol.Server;
using HeavyNetwork;

namespace Feenix.Server;

/// <summary>
/// Represents a connected Feenix client in the Feenix network.
/// </summary>
internal class FeenixClient : HeavyServerUser
{
    protected override void OnConnected()
    {
        base.OnConnected();
        
        Logger.Debug("New User fully connected: {0}", Id);
    }

    [PacketHandler(typeof(PingPacket))]
    public void OnPing(PingPacket packet)
    {
        var reversed = packet.Message.Reverse().ToArray();
        SendPacket(new PongPacket
        {
            Reversed = new string(reversed)
        });
    }
}