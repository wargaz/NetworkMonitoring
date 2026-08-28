using System.Net.NetworkInformation;


namespace Nätverksövervakning
{
    internal class ToolLatency
    {

        // Latens: pingar IP och returnerar latens i ms och status
        public async Task<(long latency, IPStatus status)> GetLatency(string IPAddress)
        {
            var ping = new Ping();
            PingReply reply = await ping.SendPingAsync(IPAddress);
            long latency = reply.Status == IPStatus.Success ? reply.RoundtripTime : -1;
            return (latency, reply.Status);
        }
    }
}
