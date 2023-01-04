using System.Runtime.Serialization;

namespace Feenix.Common.Protocol.Api;

/// <summary>
/// The packet describes a structure which contains data to send over the network. The packet itself describes
/// the structure of the packet, while an instance contains the actual data.
/// </summary>
public abstract class Packet
{
    /// <summary>
    /// Gets called when the packet should be written to the byte buffer before sending.
    /// </summary>
    /// <param name="writer">The <see cref="PacketWriter"/> for this packet.</param>
    protected abstract void Write(PacketWriter writer);
    
    /// <summary>
    /// Gets called when the packet should be read from the byte buffer after receiving.
    /// </summary>
    /// <param name="reader">The <see cref="PacketReader"/> for this packet.</param>
    protected abstract void Read(PacketReader reader);
    
    /// <summary>
    /// Converts the packet to a byte array.
    /// </summary>
    public byte[] ToBytes()
    {
        using var writer = new PacketWriter(PacketRegistry.GetPacketId(GetType()));
        Write(writer);
        return writer.ToArray();
    }
    
    /// <summary>
    /// Creates a new instance of the packet.
    /// </summary>
    /// <param name="bytes">The bytes for the packets data.</param>
    /// <returns>The packet or null.</returns>
    public static Packet? FromBytes(byte[] bytes)
    {
        using var reader = new PacketReader(bytes);
        var opcode = reader.ReadPacketID();
        var packet = FormatterServices.GetUninitializedObject(PacketRegistry.GetPacketType(opcode)) as Packet;
        packet?.Read(reader);
        return packet;
    }
}