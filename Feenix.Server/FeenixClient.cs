using DotNetty.Transport.Channels;
using Neptunium;

namespace Feenix.Server;

/// <summary>
/// Represents a connected Feenix client in the Feenix network.
/// </summary>
internal class FeenixClient : ClientConnection
{
    /// <summary>
    /// Managed by Neptunium. Do not change!
    /// </summary>
    public FeenixClient(long id, IChannel channel) : base(id, channel)
    {
    }
}