using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using ArpLookup;


namespace Nätverksövervakning
{
    public class NetworkLookup
    {
        private string myIP = "";

        public NetworkLookup(string subnetBase) 
        {
            // Hämta egen IP-adress från angivet subnet
            myIP = GetLocalIPAddress(subnetBase);
            if (myIP == "") 
                Console.WriteLine("Kunde inte hämta din IP-adress.");
            else
                Console.WriteLine($"Ditt IP: {myIP}.");
        }

        // Returnera egen IP-adress som sträng
        public string GetLocalIPAddress(string subnetBase)
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                //Console.WriteLine($"Hittade IP: {ip.ToString()}");
                if (ip.AddressFamily == AddressFamily.InterNetwork && ip.ToString().StartsWith(subnetBase))
                {
                    return ip.ToString();
                }
            }
            return "";
        }

        // Pingar given IP-adress och returnerar Task (async)
        public async Task<PingReply> PingAsync(string IPAddress)
        {
            using System.Net.NetworkInformation.Ping ping = new();
            PingReply reply = await ping.SendPingAsync(IPAddress);
            return reply;
        }

        // Söker igenom subnet och returnerar IP och MAC-adresser
        public async Task<List<(string IP, string Hostname)>> ScanSubnetAsync(string subnetBase)
        {
            var tasks = new List<Task<PingReply>>();
            var addresses = new List<string>();

            // Loopa och hoppa över 0 (nätverksadress) och 255 (broadcast)
            for (int i = 1; i <= 254; i++)
            {
                string ip = $"{subnetBase}{i}";
                addresses.Add(ip);

                // Startar pingen (async)
                tasks.Add(PingAsync($"{subnetBase}{i}"));
            }

            // Vänta på att alla pings är klara
            PingReply[] results = await Task.WhenAll(tasks);
            List<string> getIPAddress = new List<string>();
            List<string> getMACAdress = new List<string>();

            // Går igenom resultaten och plockar ut de som svarade
            var pingSuccesss = new List<(string IP, string Hostname)>();
            for (int i = 0; i < results.Length; i++)
            {
                if (results[i].Status == IPStatus.Success)
                {
                    //string hostName = "";

                    // IP-adressen som svarade
                    getIPAddress.Add(addresses[i]);

                    // Hämta MAC
                    PhysicalAddress ? mac = null;
                    mac = Arp.Lookup(IPAddress.Parse(addresses[i]));

                    if (addresses[i] == myIP)
                        addresses[i] += " (Din IP)";

                    string ipResult = addresses[i];
                    string macResult = (mac == null || mac.ToString() == "") ? "Ingen MAC-adress hittades" : mac.ToString();

                    pingSuccesss.Add((ipResult, macResult));
                }
            }

            return pingSuccesss;
        }
    }
}
