using Feenix.Common.Protocol.Client;
using Feenix.Common.Protocol.Server;
using HeavyNetwork;

namespace Feenix;

internal class Client : HeavyClient
{
    public Client(HeavyClientOptions options, IServiceProvider? serviceProvider = null) : base(options, serviceProvider)
    {
    }

    protected override void OnConnected()
    {
        base.OnConnected();
        
        SendPacket(new PingPacket
        {
            Message = "Hallo Welt!"
        });
    }

    [PacketHandler(typeof(PongPacket))]
    internal void OnPongPacket(PongPacket packet)
    {
        Console.WriteLine(packet.Reversed);
    }
}