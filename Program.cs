using Nätverksövervakning.UI;
using System;
using System.Net.NetworkInformation;

namespace Nätverksövervakning
{
    internal static class Program
    {
        [STAThread]
        static async Task Main()
        {
            // Implementerar UI senare:
            //ApplicationConfiguration.Initialize();
            //Application.Run(new WindowMain());

            Console.WriteLine("Nätverksövervakning - John Axelsson\n");

            ObjPing ping = new ObjPing();

            /*** PING ***/

            String pingURL = "google.se";
            PingReply reply = await ping.MyPingAsync(pingURL);

            Console.WriteLine($"Pingtest till {pingURL}...");

            if (reply.Status == IPStatus.Success)
            {
                Console.WriteLine($"Ping till {pingURL} lyckades. {reply.RoundtripTime} ms.");
            }
            else
            {
                Console.WriteLine($"Ping till {pingURL} misslyckades.");
            }

            /*** SUBNET PING ***/

            String subnetBase = "192.168.0.";

            Console.WriteLine($"\nGår igenom subnet {subnetBase}0-255...");

            List<String> results = await ping.ScanSubnetAsync(subnetBase);


            foreach (string ip in results)
            {
                Console.WriteLine($"Hittade: {ip}");
            }


            Console.WriteLine("\nTryck på valfri tangent för att avsluta...");
            Console.ReadKey();

        }
    }
}