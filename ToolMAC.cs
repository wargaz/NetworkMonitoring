using System.Net;
using System.Net.NetworkInformation;
using ArpLookup;

namespace Nätverksövervakning
{
    public class ToolMAC
    {
        // Hämtar MAC-adress för en given IP-adress
        public async Task<string> GetMAC(string IPAdress)
        {
            PhysicalAddress? mac = await Arp.LookupAsync(IPAddress.Parse(IPAdress));
            return mac == null ? "" : mac.ToString();
        }
    }
}
