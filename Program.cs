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

            string subnetBase = "192.168.0."; // Låt användaren ange detta senare via UI
            NetworkLookup ping = new NetworkLookup(subnetBase);

            /*** PING ***/

            string pingURL = "google.se";
            PingReply reply = await ping.PingAsync(pingURL);

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

            Console.WriteLine($"\nGår igenom subnet {subnetBase}0-255...");

            List<(string IP, string Hostname)> results = await ping.ScanSubnetAsync(subnetBase);


            foreach (var (IP, Hostname) in results)
            {
                Console.WriteLine($"Hittade: {IP} ({Hostname})");
            }


            Console.WriteLine("\nTryck på valfri tangent för att avsluta...");
            Console.ReadKey();

        }
    }
}