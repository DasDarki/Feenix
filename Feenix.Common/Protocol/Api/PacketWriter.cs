using System.Drawing;
using System.Numerics;

namespace Feenix.Common.Protocol.Api;

/// <summary>
/// The packet writer takes care of writing data to the packets byte buffer.
/// </summary>
public class PacketWriter : IDisposable
{
    private readonly BinaryWriter _buffer;

    /// <summary>
    /// Creates a new instance of the <see cref="PacketWriter"/> class with an empty buffer and appends the packet id.
    /// </summary>
    public PacketWriter(ulong packetId)
    {
        _buffer = new BinaryWriter(new MemoryStream());

        WriteUInt64(packetId);
    }

    public byte[] ToArray()
    {
        return ((MemoryStream) _buffer.BaseStream).ToArray();
    }

    public void WriteInt16(short value)
    {
        _buffer.Write(value);
    }

    public void WriteUInt16(ushort value)
    {
        _buffer.Write(value);
    }

    public void WriteInt32(int value)
    {
        _buffer.Write(value);
    }

    public void WriteUInt32(uint value)
    {
        _buffer.Write(value);
    }

    public void WriteInt64(long value)
    {
        _buffer.Write(value);
    }

    public void WriteUInt64(ulong value)
    {
        _buffer.Write(value);
    }

    public void WriteFloat(float value)
    {
        _buffer.Write(value);
    }

    public void WriteDouble(double value)
    {
        _buffer.Write(value);
    }

    public void WriteBoolean(bool value)
    {
        _buffer.Write(value);
    }

    public void WriteString(string value)
    {
        _buffer.Write(value);
    }

    public void WriteBytes(byte[] value)
    {
        _buffer.Write(value);
    }

    public void WriteBytes(byte[] value, int offset, int count)
    {
        _buffer.Write(value, offset, count);
    }

    public void WriteBytes(byte[] value, int offset, int count, bool reverse)
    {
        if (reverse)
        {
            Array.Reverse(value, offset, count);
        }

        _buffer.Write(value, offset, count);
    }

    public void WriteBytes(byte[] value, bool reverse)
    {
        if (reverse)
        {
            Array.Reverse(value);
        }

        _buffer.Write(value);
    }

    public void WriteBytes(byte[] value, int count, bool reverse)
    {
        if (reverse)
        {
            Array.Reverse(value, 0, count);
        }

        _buffer.Write(value, 0, count);
    }

    public void WriteBytes(byte[] value, int count)
    {
        _buffer.Write(value, 0, count);
    }

    public void WriteDecimal(decimal value)
    {
        _buffer.Write(value);
    }

    public void WriteChar(char value)
    {
        _buffer.Write(value);
    }

    public void WriteDateTime(DateTime value)
    {
        _buffer.Write(value.ToBinary());
    }

    public void WriteGuid(Guid value)
    {
        _buffer.Write(value.ToByteArray());
    }

    public void WriteVector2(Vector2 value)
    {
        _buffer.Write(value.X);
        _buffer.Write(value.Y);
    }

    public void WriteVector3(Vector3 value)
    {
        _buffer.Write(value.X);
        _buffer.Write(value.Y);
        _buffer.Write(value.Z);
    }

    public void WriteVector4(Vector4 value)
    {
        _buffer.Write(value.X);
        _buffer.Write(value.Y);
        _buffer.Write(value.Z);
        _buffer.Write(value.W);
    }

    public void WriteQuaternion(Quaternion value)
    {
        _buffer.Write(value.X);
        _buffer.Write(value.Y);
        _buffer.Write(value.Z);
        _buffer.Write(value.W);
    }

    public void WriteMatrix4X4(Matrix4x4 value)
    {
        _buffer.Write(value.M11);
        _buffer.Write(value.M12);
        _buffer.Write(value.M13);
        _buffer.Write(value.M14);
        _buffer.Write(value.M21);
        _buffer.Write(value.M22);
        _buffer.Write(value.M23);
        _buffer.Write(value.M24);
        _buffer.Write(value.M31);
        _buffer.Write(value.M32);
        _buffer.Write(value.M33);
        _buffer.Write(value.M34);
        _buffer.Write(value.M41);
        _buffer.Write(value.M42);
        _buffer.Write(value.M43);
        _buffer.Write(value.M44);
    }

    public void WriteColor(Color value)
    {
        _buffer.Write(value.ToArgb());
    }

    public void WriteEnum<T>(T value)
    {
        _buffer.Write(Convert.ToInt32(value));
    }

    public void WriteList<T>(List<T> value, Action<T> writeAction)
    {
        _buffer.Write(value.Count);

        foreach (var item in value)
        {
            writeAction(item);
        }
    }

    public void Dispose()
    {
        _buffer.Dispose();
    }
}