using Feenix.Common;
using Feenix.Common.Protocol.Api;
using LiteNetwork.Client;

namespace Feenix;

/// <summary>
/// The Feenix client class is the opposite pendant of the Feenix client class on the
/// server side.
/// </summary>
internal class FeenixClient : LiteClient
{
    /// <summary>
    /// Creates a new feenix client to connect to the given hostname and port.
    /// </summary>
    /// <param name="hostname"></param>
    /// <param name="port"></param>
    public FeenixClient(string hostname, int port) 
        : base(BuildOptions(hostname, port))
    {
    }

    /// <summary>
    /// Sends the packet to the server.
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
            Logger.Fatal("Received invalid packet from server: {}.", Convert.ToBase64String(packetBuffer));
        }
        else
        {
            PacketHandler.ExecuteHandler(packet);
        }
        
        return Task.CompletedTask;
    }

    private static LiteClientOptions BuildOptions(string hostname, int port)
    {
        var options = new LiteClientOptions
        {
            Host = hostname,
            Port = port
        };

        return options;
    }
}