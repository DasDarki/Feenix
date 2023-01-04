using Feenix.Common.Protocol.Api;

namespace Feenix.Common.Protocol.Server;

public class PongPacket : Packet
{
    public string Reversed { get; private set; }

    public PongPacket(string reversed)
    {
        Reversed = reversed;
    }
    
    protected override void Write(PacketWriter writer)
    {
        writer.WriteString(Reversed);
    }

    protected override void Read(PacketReader reader)
    {
        Reversed = reader.ReadString();
    }
}