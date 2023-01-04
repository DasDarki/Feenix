using Feenix.Common;
using Feenix.Common.Protocol.Api;
using Feenix.Common.Protocol.Client;
using Feenix.Common.Protocol.Server;
using LiteNetwork.Server;

namespace Feenix.Server;

/// <summary>
/// Represents a connected Feenix client in the Feenix network.
/// </summary>
internal class FeenixClient : LiteServerUser
{
    /// <summary>
    /// Creates a new feenix server user and registers the default packet handlers.
    /// </summary>
    public FeenixClient()
    {
        PacketHandler.Scan(this);
    }
    
    /// <summary>
    /// Sends a packet to the client.
    /// </summary>
    public void SendPacket(Packet packet)
    {
        Send(packet.ToBytes());
    }

    /// <summary>
    /// Handles incoming messages by converting them into packets and trigger their packet handlers.
    /// </summary>
    public override Task HandleMessageAsync(byte[] packetBuffer)
    {
        var packet = Packet.FromBytes(packetBuffer);

        if (packet == null)
        {
            Logger.Fatal("Received invalid packet from {}: {}.", Id, Convert.ToBase64String(packetBuffer));
        }
        else
        {
            PacketHandler.ExecuteHandler(packet, this);
        }
        
        return Task.CompletedTask;
    }

    protected override void OnConnected()
    {
        base.OnConnected();
        
        Logger.Debug("New User fully connected: {}", Id);
    }

    [PacketHandler(typeof(PingPacket))]
    public void OnPing(PingPacket packet)
    {
        var reversed = packet.Message.Reverse().ToArray();
        SendPacket(new PongPacket(new string(reversed)));
    }
}