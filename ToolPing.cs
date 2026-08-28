using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Nätverksövervakning
{
    internal class ToolPing
    {

        // Returnera egen IP-adress som sträng
        public string GetLocalIPAddress(string subnetBase)
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork && ip.ToString().StartsWith(subnetBase))
                    return ip.ToString();
            return "";
        }

        // Pingar given IP-adress och returnerar PingReply
        public async Task<PingReply> PingAsync(string IPAddress)
        {
            var ping = new Ping();
            PingReply reply = await ping.SendPingAsync(IPAddress);
            return reply;
        }
    }
}
