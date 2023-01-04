using Feenix.Common;
using Tomlet;

namespace Feenix.Server.Configuration;

/// <summary>
/// The config model describes the configuration of the Feenix server. The model can be initialized with
/// the default values by calling the default constructor. The model will be loaded from a TOML file.
/// </summary>
internal class Config
{
    /// <summary>
    /// The current config loaded.
    /// </summary>
    internal static Config Current { get; private set; } = null!;
    
    /// <summary>
    /// <see cref="GeneralSection"/>
    /// </summary>
    internal GeneralSection General { get; set; } = new();
    
    /// <summary>
    /// <see cref="ScriptingSection"/>
    /// </summary>
    internal ScriptingSection Scripting { get; set; } = new();

    /// <summary>
    /// Saves this config to the given path.
    /// </summary>
    private void Save(string path)
    {
        File.WriteAllText(path, TomletMain.TomlStringFrom(this));
    }
    
    /// <summary>
    /// Loads the config. If the config is invalid or not existing, the default config will be returned. For the latter,
    /// the default config gets saved to the config file.
    /// </summary>
    internal static void Load()
    {
        var configPath = Path.Combine(Environment.CurrentDirectory, "config.toml");
        Logger.Verbose("Configuration path set to {}", configPath);
        
        if (!File.Exists(configPath))
        {
            Logger.Warning("Config file not found. Creating default config. Please edit the config file and restart the server.");
            Current = new Config();
            Current.Save(configPath);
        }

        try
        {
            Current = TomletMain.To<Config>(File.ReadAllText(configPath));
        }
        catch
        {
            Logger.Fatal("The current config file is invalid. Falling back to default. Please fix it or delete it to generate a new one.");
            Current = new Config();
        }
    }
}