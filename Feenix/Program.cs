// See https://aka.ms/new-console-template for more information

using Feenix;
using Feenix.Common.Protocol;
using HeavyNetwork;

FeenixProtocol.Initialize();

var client = new Client(new HeavyClientOptions
{
    Host = "127.0.0.1",
    Port = 57732
});

client.ConnectAsync().GetAwaiter().GetResult();

while (true)
{
    
}