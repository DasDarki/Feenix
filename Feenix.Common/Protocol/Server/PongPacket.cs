using HeavyNetwork.Protocol;

namespace Feenix.Common.Protocol.Server;

public class PongPacket : Packet
{
    public string Reversed { get; set; }
    
    public override void Write(PacketWriter writer)
    {
        writer.WriteString(Reversed);
    }

    public override void Read(PacketReader reader)
    {
        Reversed = reader.ReadString();
    }
}