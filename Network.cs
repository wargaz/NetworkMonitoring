using System.Net.NetworkInformation;

namespace Nätverksövervakning
{
    public class Network
    {

        private ToolPing toolPing = new ToolPing();
        private ToolVendor toolVendor = new ToolVendor();
        private ToolMAC toolMAC = new ToolMAC();
        private ToolServices toolServices = new ToolServices();
        private ToolLatency toolLatency = new ToolLatency();

        
        // Returnera egen IP-adress som sträng
        public string GetLocalIPAddress(string subnetBase) => toolPing.GetLocalIPAddress(subnetBase);

        
        // Pingar given IP-adress och returnerar PingReply
        public async Task<PingReply> PingAsync(string IPAddress) => await toolPing.PingAsync(IPAddress);

        // Hämtar MAC-adress för en given IP-adress
        public async Task<string> GetMAC(string IPAdress)
        {
            await toolPing.PingAsync(IPAdress); // Pinga först för att uppdatera ARP-tabellen
            return await toolMAC.GetMAC(IPAdress);
        }


        // Anropar enheter på nätverket som svarar med DNS-poster
        public async Task<List<(string IP, string name)>> GetServices() => await toolServices.GetServices();


        // Matchar IP-adress med DNS-poster från GetOther()
        public string GetServiceAssign(List<(string IP, string name)> other, string ip) => toolServices.GetServiceAssign(other, ip);


        // Latens: pingar IP och returnerar latens i ms och status
        public async Task<(long latency, IPStatus status)> GetLatency(string IPAddress) => await toolLatency.GetLatency(IPAddress);



        // Söker igenom subnet och returnerar IP, MAC, vendor info och info från Zeroconf
        public async Task<List<(string IP, string MAC, string vendor, string other)>> ScanSubnetAsync(string subnetBase)
        {

            var tasks = new List<Task<PingReply>>();
            var addresses = new List<string>();

            // Loopa och hoppa över 0 (nätverksadress) och 255 (broadcast)
            for (int i = 1; i <= 254; i++)
            {
                string ip = $"{subnetBase}{i}";
                addresses.Add(ip);

                // Startar pingen (async)
                tasks.Add(toolPing.PingAsync($"{subnetBase}{i}"));
            }

            // Vänta på att alla pings är klara
            PingReply[] results = await Task.WhenAll(tasks);
            List<string> getIPAddress = new List<string>();


            // Hämta other info
            List<(string IP, string name)> otherInfo = new List<(string IP, string name)>();
            otherInfo = await GetServices();

            // Går igenom resultaten och plockar ut de som svarade
            var pingSuccesss = new List<(string IP, string MAC, string vendor, string other)>();
            for (int i = 0; i < results.Length; i++)
            {
                if (results[i].Status == IPStatus.Success)
                {

                    // IP-adressen som svarade
                    getIPAddress.Add(addresses[i]);

                    string ipResult = addresses[i];
                    string macResult = await GetMAC(ipResult);
                    string vendorResult = toolVendor.GetVendor(macResult);
                    string otherResult = GetServiceAssign(otherInfo, ipResult);

                    pingSuccesss.Add((ipResult, macResult, vendorResult, otherResult));
                }
            }

            return pingSuccesss;
        }


    }
}
