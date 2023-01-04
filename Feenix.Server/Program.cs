using Feenix.Common;
using Feenix.Common.Protocol;
using Feenix.Server;
using Feenix.Server.Configuration;

Logger.ShowTitleCard();
Logger.Info("Loading configuration...");
Config.Load();

Logger.Info("Initializing server...");
FeenixProtocol.Initialize();

await FeenixServer.Instance.StartAsync();

Logger.Info("Server started. Feenix is now up and running.");

while (true)
{
    var input = Console.ReadLine();

    if (input == "!exit")
    {
        break;
    }
}

await FeenixServer.Instance.StopAsync();
Logger.Info("Server stopped.");