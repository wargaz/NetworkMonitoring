using Zeroconf;

namespace Nätverksövervakning
{
    internal class ToolServices
    {


        // Anropar enheter på nätverket som svarar med DNS-poster
        public async Task<List<(string IP, string name)>> GetServices()
        {
            var results = await ZeroconfResolver.ResolveAsync("_http._tcp.local.", TimeSpan.FromSeconds(3));
            var serviceList = new List<(string IP, string name)>();

            ILookup<string, string> domains = await ZeroconfResolver.BrowseDomainsAsync(TimeSpan.FromSeconds(3));

            foreach (var domain in domains)
            {
                foreach (string entry in domain)
                {
                    string ip = entry.Split(':')[0].Trim();
                    serviceList.Add((ip, domain.Key));
                }
            }
            return serviceList;
        }


        // Matchar IP-adress med DNS-poster från GetOther()
        public string GetServiceAssign(List<(string IP, string name)> other, string ip)
        {
            for (int i = 0; i < other.Count; i++)
            {
                if (ip == other[i].IP)
                    return $"{other[i].name}";
            }
            return "";
        }
    }
}
