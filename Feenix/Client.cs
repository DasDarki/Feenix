using Feenix.Common.Protocol.Server;
using Neptunium;
using Neptunium.Client;

namespace Feenix;

internal class Client : BaseClient
{
    internal Client()
    {
        PacketHandler.Scan(this);
    }

    [PacketHandler(typeof(PongPacket))]
    internal void OnPongPacket(PongPacket packet)
    {
        Console.WriteLine(packet.Message);
    }
}