using HeavyNetwork.Protocol;

namespace Feenix.Common.Protocol.Client;

public class PingPacket : Packet
{
    public string Message { get; set; }

    public override void Write(PacketWriter writer)
    {
        writer.WriteString(Message);
    }

    public override void Read(PacketReader reader)
    {
        Message = reader.ReadString();
    }
}