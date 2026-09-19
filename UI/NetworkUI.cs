using System.Net.NetworkInformation;

namespace Nätverksövervakning.UI
{
    public partial class NetworkUI : Form
    {
        // Skapa nätverksobjektet som samlar alla metoder från
        // ToolServices, ToolPing, ToolMAC, ToolVendor och ToolLatency
        Network network = new Network();

        private List<(string IP, string MAC, string vendor, string other)> activeConn = new();

        public NetworkUI()
        {
            InitializeComponent();

            LabelResultIP.Text = "";
            LabelErrorIP.Text = "";
            GroupLoading.Text = "";
            GroupLoading.Visible = false;
            GroupResult.Visible = false;
            DataGrid.Columns.Add("IP", "IP-adress");
            DataGrid.Columns.Add("MAC", "MAC-adress");
            DataGrid.Columns.Add("Latency", "Latens");
            DataGrid.Columns.Add("Vendor", "Tillverkare");
            DataGrid.Columns.Add("Other", "Tjänster");

        }

        // Visa egen IP
        private string ShowMyIP(string subnet)
        {
            string myIP = network.GetLocalIPAddress(subnet);
            LabelResultIP.Text = $"Din IP-adress: {myIP}";
            return myIP;
        }

        // Visa aktiva anslutningar i nätverket
        private async Task<List<(string IP, string MAC, string vendor, string other)>> GetSubnetInfo(string myIP, string subnet)
        {
            GroupLoading.Text = $"Går igenom subnet {subnet}0-255...";

            // Aktivera load bar
            GroupLoading.Visible = true;
            LoadProgress.Value = 0;
            LoadProgress.Maximum = 254; // 0-255, men hoppar över 0 och 255
            var progress = new Progress<int>(value => LoadProgress.Value = value);

            // Hämta aktiva anslutningar
            // Skickar med progress till ScanSubnetAsync för att uppdatera load bar därifrån
            var activeConn = await network.ScanSubnetAsync(subnet, progress);

            return activeConn;
        }


        private async Task<string> GetLatency(string IP)
        {
            var (latencyValue, status) = await network.GetLatency(IP);
            if (status != IPStatus.Success) return "Misslyckades";
            
            return $"{latencyValue} ms";
        }

        private async Task GenerateResultAsync(string subnet)
        {

            var latency = new List<string>();


            // Visa egen IP
            string myIP = ShowMyIP(subnet);


            // Lägg till subnet info
            activeConn = await GetSubnetInfo(myIP, subnet);

            // Om inget hittades, visa felmeddelande
            if (activeConn.Count == 0)
            {
                GroupLoading.Visible = false;
                LabelErrorIP.Visible = true;
                LabelErrorIP.Text = "Hittade inget på angivet subnet";
                return;
            }


            // Lägg till latens till aktiva anslutningar
            for (var i = 0; i < activeConn.Count; i++)
                latency.Add(await GetLatency(activeConn[i].IP));


            // Lägger till all info till DataGrid
            for (int i = 0; i < activeConn.Count; i++)
            {
                var (IP, MAC, vendor, services) = activeConn[i];
                DataGrid.Rows.Add(new string[] { IP, MAC, latency[i], vendor, services });
            }

            // Starta timer för latens, så den uppdateras kontinuerligt
            LatencyTimer.Start();

            // Visa resultat
            GroupLoading.Visible = false;
            GroupResult.Visible = true;


        }

        // Kör-knappen för att scanna subnet
        private async void ButtonIP_Click(object sender, EventArgs e)
        {
            ButtonIP.Enabled = false;
            GroupResult.Visible = false;
            LabelErrorIP.Visible = false;

            if (int.TryParse(LabelInputIP.Text, out int result))
            {
                if (result >= 0 && result <= 256)
                {
                    await GenerateResultAsync($"192.168.{LabelInputIP.Text}.");
                    ButtonIP.Enabled = true;
                    return;
                }
            }

            // Om input-fältet inte är giltig int mellan 0-256, visa felmeddelande
            LabelErrorIP.Text = "Ogiltigt nummer. Ange nummer mellan 0-256.";
        }

        // Timer som uppdaterar latens en gång i sekunden
        private async void LatencyTimer_Tick(object sender, EventArgs e)
        {
            var latencyStr = new List<string>();

            // Uppdaterar latens för alla aktiva anslutningar
            for (int i = 0; i < activeConn.Count; i++)
                latencyStr.Add(await GetLatency(activeConn[i].IP));

            // Uppdaterar DataGrid med nya latensvärden
            // Jag lade det i egen loop så alla uppdateras samtidigt i tabellen,
            // annars skulle det uppdateras en i taget eftersom de blev klara efter await
            for (int i = 0; i < activeConn.Count; i++)
                DataGrid.Rows[i].Cells["Latency"].Value = latencyStr[i];
        }
    }
}
