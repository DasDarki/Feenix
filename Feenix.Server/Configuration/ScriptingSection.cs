using Tomlet.Attributes;

namespace Feenix.Server.Configuration;

/// <summary>
/// The [Scripting] section of the configuration file. Contains scripting related settings.
/// </summary>
internal class ScriptingSection
{
    [TomlPrecedingComment("The minimum needed script API version to trespass the server.")]
    internal int MinScriptApi { get; set; } = 1;
    
    [TomlPrecedingComment("Whether unsigned, unregistered - so called anonymous - scripts are allowed to trespass the server.")]
    internal bool AllowAnonymousScripts { get; set; } = false;

    [TomlPrecedingComment("A list of urls which script registries the server should check against against.")]
    internal List<string> ScriptRegistries { get; set; } = new();
    
    [TomlPrecedingComment("A list containing event names which are not allowed to be used by scripts.")]
    internal List<string> EventBlacklist { get; set; } = new();
    
    [TomlPrecedingComment("A list containing event names which only are allowed to be used by scripts. If not empty, only events in this list are allowed.")]
    internal List<string> EventWhitelist { get; set; } = new();
}