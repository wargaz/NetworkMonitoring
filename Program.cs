using System.Net.NetworkInformation;
using System.Reactive.Subjects;
using Nätverksövervakning.UI;

namespace Nätverksövervakning
{
    internal static class Program
    {


        [STAThread]
        static async Task Main()
        {

            ApplicationConfiguration.Initialize();
            Application.Run(new NetworkUI());

            // Logiken flyttad till NetworkUI.cs

            Console.WriteLine("Nätverksövervakning - John Axelsson\n");

            string subnetBase = "192.168.0."; // Låt användaren ange detta senare via UI
            Network toolNetwork = new Network();

            // Skriv ut klientens IP om det finns på angivet subnet
            string myIP = toolNetwork.GetLocalIPAddress(subnetBase);
            if (myIP != "") Console.WriteLine($"Ditt IP: {myIP}.");

            /*** Pinga given URL ***/

            string pingURL = "google.se";
            PingReply reply = await toolNetwork.PingAsync(pingURL);

            Console.WriteLine($"Pingtest till {pingURL}...");

            if (reply.Status == IPStatus.Success)
                Console.WriteLine($"Ping till {pingURL} lyckades. {reply.RoundtripTime} ms.");
            else
                Console.WriteLine($"Ping till {pingURL} misslyckades.");


            /*** Hämta info om aktiva anslutningar i subnet ***/

            Console.WriteLine($"\nGår igenom subnet {subnetBase}0-255...");

            List<(string IP, string MAC, string vendor, string other)> activeConn = await toolNetwork.ScanSubnetAsync(subnetBase);


            foreach (var (IP, MAC, vendor, other) in activeConn)
            {
                string myIPStr = "";
                if (IP == myIP) myIPStr = " - Denna maskin";
                string otherStr = string.IsNullOrEmpty(other) ? "" : $", {other}"; // Så det inte blir extra kolon
                Console.WriteLine($"Hittade: {IP}, {MAC}, {vendor}{otherStr}{myIPStr}");
            }


            //*** Visa latens till aktiva anslutningar ***//
            //TODO: kör kontinuerligt och räkna ut snittet
            int[] latency = new int[activeConn.Count];
            string[] latencyStatus = new string[activeConn.Count];
            for (int i = 0; i < activeConn.Count; i++)
            {
                var (latencyValue, status) = await toolNetwork.GetLatency(activeConn[i].IP);
                latency[i] = (int)latencyValue;
                latencyStatus[i] = status.ToString();
            }

            Console.WriteLine("\nLatens till aktiva anslutningar:");
            for (int i = 0; i < activeConn.Count; i++)
            {
                Console.WriteLine($"IP: {activeConn[i].IP}, Latens: {latency[i]} ms, Status: {latencyStatus[i]}");
            }

            Console.WriteLine("\nTryck på valfri tangent för att avsluta...");
            var scanButton = new Button
            {
                Text = "Skanna nätverk",
                Location = new Point(20, 20),
                Size = new Size(150, 30)
            };

            //Console.ReadKey();


        }
    }
}