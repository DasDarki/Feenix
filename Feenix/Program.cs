// See https://aka.ms/new-console-template for more information

using Feenix;
using Feenix.Common.Protocol.Client;
using Neptunium;

Network.ConnectAsync<Client>("127.0.0.1", 25319, "", client =>
{
    client.SendPacket(new PingPacket
    {
        Message = "Hallo Welt!"
    });
}).GetAwaiter().GetResult();


while (true)
{
    
}