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

        /* Objekt som pingar given IP-adress och returnerar sträng */
        public async Task<PingReply> MyPingAsync(String IPAddress)
        {
            using System.Net.NetworkInformation.Ping ping = new();
            PingReply reply = await ping.SendPingAsync(IPAddress);
            return reply;

            //if (reply.Status == IPStatus.Success)
            //{
            //    Console.WriteLine($"Online - {reply.RoundtripTime} ms");
            //}
            //else
            //{
            //    Console.WriteLine($"Offline - {reply.Status}");
            //}

            //return reply.Status.ToString();
        }
    }
}
