using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Nätverksövervakning
{
    public class ObjPing
    {

        /* Pingar given IP-adress och returnerar Task (async) */
        public async Task<PingReply> MyPingAsync(String IPAddress)
        {
            using System.Net.NetworkInformation.Ping ping = new();
            PingReply reply = await ping.SendPingAsync(IPAddress);
            return reply;
        }

        public async Task<List<string>> ScanSubnetAsync(string subnetBase)
        {
            var tasks = new List<Task<PingReply>>();
            var addresses = new List<string>();

            for (int i = 1; i <= 254; i++)
            {
                string ip = $"{subnetBase}{i}";
                addresses.Add(ip);

                // Startar pingen (men väntar inte)
                tasks.Add(MyPingAsync($"{subnetBase}{i}"));
            }

            // Vänta på att alla pings är klara
            PingReply[] results = await Task.WhenAll(tasks);

            // Gå igenom resultaten och plocka ut de som svarade
            var pingSuccesss = new List<string>();
            for (int i = 0; i < results.Length; i++)
            {
                if (results[i].Status == IPStatus.Success)
                {
                    pingSuccesss.Add(addresses[i]);
                }
            }

            return pingSuccesss;
        }
    }
}
