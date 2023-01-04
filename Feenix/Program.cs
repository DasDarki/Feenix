// See https://aka.ms/new-console-template for more information

using Feenix;
using Feenix.Common.Protocol;
using Feenix.Common.Protocol.Client;

FeenixProtocol.Initialize();

var client = new FeenixClient("127.0.0.1", 57732);

client.Connected += (sender, eventArgs) =>
{
    client.SendPacket(new PingPacket("Hello World!"));
};

await client.ConnectAsync();

while (true)
{
    
}