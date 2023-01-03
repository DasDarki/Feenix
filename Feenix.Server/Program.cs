using Feenix.Common;
using Feenix.Common.Protocol;
using Feenix.Server;
using Feenix.Server.Configuration;

Logger.ShowTitleCard();
Logger.Info("Loading configuration...");
Config.Load();

Logger.Info("Initializing server...");
FeenixProtocol.Initialize();

FeenixServer.Instance.StartAsync().GetAwaiter().GetResult();

Logger.Info("Server started. Feenix is now up and running.");

while (true)
{
    var input = Console.ReadLine();

    if (input == "!exit")
    {
        break;
    }
}

// TODO add stop code here
FeenixServer.Instance.StopAsync().GetAwaiter().GetResult();
Logger.Info("Server stopped.");