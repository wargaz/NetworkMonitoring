using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using ArpLookup;
using MacAddressVendorLookup;
using Zeroconf;


namespace Nätverksövervakning
{
    public class NetworkLookup
    {
        private string myIP = "";
        private MacVendorBinaryReader vendorInfoProvider;
        private AddressMatcher addressMatcher;

        public NetworkLookup(string subnetBase) 
        {

            // Initiera MacVendorBinaryReader och AddressMatcher för GetVendor()
            vendorInfoProvider = new MacVendorBinaryReader();
            using (var resourceStream = ManufBinResource.GetStream().Result)
                vendorInfoProvider.Init(resourceStream).Wait();
            addressMatcher = new AddressMatcher(vendorInfoProvider);


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
                tasks.Add(PingAsync($"{subnetBase}{i}"));
            }

            // Vänta på att alla pings är klara
            PingReply[] results = await Task.WhenAll(tasks);
            List<string> getIPAddress = new List<string>();

            // Hämta other info
            List<(string IP, string name)> otherInfo = new List<(string IP, string name)>();
            otherInfo = await GetOther();

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
                    string vendorResult = GetVendor(macResult);
                    string otherResult = GetOtherAssign(otherInfo, ipResult);

                    pingSuccesss.Add((ipResult, macResult, vendorResult, otherResult));
                }
            }

            return pingSuccesss;
        }

        // Hämtar MAC-adress för en given IP-adress
        public async Task<string> GetMAC(string IPAdress)
        {
            await PingAsync(IPAdress); // Försök pinga först för att säkerställa att ARP-tabellen är uppdaterad
            PhysicalAddress? mac = null;
            mac = Arp.Lookup(IPAddress.Parse(IPAdress));
            return (mac == null || mac.ToString() == "") ? "Ingen MAC-adress hittades" : mac.ToString();
        }

        // Hämtar vendor info från MACadressen
        public string GetVendor(string macAddress)
        {
            // Kontrollera giltig MAC (12 hexadecimala tecken)
            if (string.IsNullOrEmpty(macAddress) || macAddress.Length != 12
                || !macAddress.All(c => Uri.IsHexDigit(c)))
                return "";

            // Hämta vendor
            PhysicalAddress mac = PhysicalAddress.Parse(macAddress);
            var vendorInfo = addressMatcher.FindInfo(mac);

            return vendorInfo?.Organization ?? "Okänd tillverkare";
        }
        
        // Anropar enheter på nätverket som svarar med DNS-poster
        public async Task<List<(string IP, string name)>> GetOther()
        {
            var results = await ZeroconfResolver.ResolveAsync("_http._tcp.local.", TimeSpan.FromSeconds(3));
            List<(string IP, string name)> other = new List<(string IP, string name)>();

            ILookup<string, string> domains = await ZeroconfResolver.BrowseDomainsAsync(TimeSpan.FromSeconds(3));

            foreach (var domain in domains)
            {
                foreach (string entry in domain)
                {
                    string ip = entry.Split(':')[0].Trim();
                    other.Add((ip, domain.Key));
                }
            }
            return other;
        }

        // Matchar IP-adress med DNS-poster från GetOther()
        public string GetOtherAssign(List<(string IP, string name)> other, string ip)
        {
            for (int i = 0; i < other.Count; i++)
            {
                if (ip == other[i].IP)
                    return other[i].name;
            }
            return "";
        }


        // Latens
        public async Task<(long latency, IPStatus status)> GetLatency(string IPAddress)
        {
            var ping = new Ping();
            PingReply reply = await ping.SendPingAsync(IPAddress);
            long latency = reply.Status == IPStatus.Success ? reply.RoundtripTime : -1;
            return (latency, reply.Status);
        }
    }
}
