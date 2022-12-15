using Tomlet.Attributes;

namespace Feenix.Server.Configuration;

/// <summary>
/// The [General] section of the configuration file. Contains general server settings.
/// </summary>
internal class GeneralSection
{
    [TomlPrecedingComment("Whether the application is in debug or not.")]
    internal bool IsDebug { get; set; } = false;

    [TomlPrecedingComment("Whether to bind the server to the loopback address (true) or any address (false).")]
    internal bool IsLocal { get; set; } = true;
    
    [TomlPrecedingComment("The port of the application.")]
    internal int Port { get; set; } = 57732;
}