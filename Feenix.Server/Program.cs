using Feenix.Common;
using Feenix.Common.Protocol;
using Feenix.Server;
using Feenix.Server.Configuration;

Logger.ShowTitleCard();
Logger.Info("Loading configuration...");
Config.Load();

Logger.Info("Initializing server...");
FeenixProtocol.Initialize();

FeenixServer.Instance = new FeenixServer();
FeenixServer.Instance.Start();

while (true)
{
    var input = Console.ReadLine();

    if (input == "!exit")
    {
        break;
    }
}

// TODO add stop code here