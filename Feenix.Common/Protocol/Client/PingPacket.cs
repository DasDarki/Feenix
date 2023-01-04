using Feenix.Common.Protocol.Api;

namespace Feenix.Common.Protocol.Client;

public class PingPacket : Packet
{
    public string Message { get; private set; }

    public PingPacket(string message)
    {
        Message = message;
    }

    protected override void Write(PacketWriter writer)
    {
        writer.WriteString(Message);
    }

    protected override void Read(PacketReader reader)
    {
        Message = reader.ReadString();
    }
}