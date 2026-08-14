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

            Console.WriteLine("Nätverksövervakning");

            ObjPing ping = new ObjPing();
            PingReply reply = await ping.MyPingAsync("192.168.0.129");

            if (reply.Status == IPStatus.Success)
            {
                Console.WriteLine($"Ping successful. {reply.RoundtripTime} ms.");
            }
            else
            {
                Console.WriteLine($"Ping failed.");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();

        }
    }
}