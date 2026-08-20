using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;


namespace Nätverksövervakning
{
    public class ObjPing
    {
        private String myIP = "";

        public ObjPing(String subnetBase) 
        {
            // Hämta egen IP-adress från angivet subnet
            myIP = GetLocalIPAddress(subnetBase);
            if (myIP == "") 
                Console.WriteLine("Kunde inte hämta din IP-adress.");
            else
                Console.WriteLine($"Ditt IP: {myIP}.");
        }

        // Returnera egen IP-adress
        public string GetLocalIPAddress(string subnetBase)
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork && ip.ToString().StartsWith(subnetBase))
                {
                    return ip.ToString();
                }
            }
            return "";
        }

        // Pingar given IP-adress och returnerar Task (async)
        public async Task<PingReply> PingAsync(String IPAddress)
        {
            using System.Net.NetworkInformation.Ping ping = new();
            PingReply reply = await ping.SendPingAsync(IPAddress);
            return reply;
        }

        // Söker igenom subnet och returnerar alla IP-adresser som svarar
        public async Task<List<string>> ScanSubnetAsync(string subnetBase)
        {
            var tasks = new List<Task<PingReply>>();
            var addresses = new List<string>();

            for (int i = 1; i <= 254; i++)
            {
                string ip = $"{subnetBase}{i}";
                addresses.Add(ip);

                // Startar pingen (men väntar inte)
                tasks.Add(PingAsync($"{subnetBase}{i}"));
            }

            // Vänta på att alla pings är klara
            PingReply[] results = await Task.WhenAll(tasks);

            // Gå igenom resultaten och plocka ut de som svarade
            var pingSuccesss = new List<string>();
            for (int i = 0; i < results.Length; i++)
            {
                if (results[i].Status == IPStatus.Success)
                {
                    if (addresses[i] == myIP)
                        addresses[i] += " (Egen IP)";
                    pingSuccesss.Add(addresses[i]);
                }
            }

            return pingSuccesss;
        }
    }
}
