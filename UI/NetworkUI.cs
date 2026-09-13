using System.Reactive.Subjects;
using Microsoft.VisualBasic.Devices;

namespace Nätverksövervakning.UI
{
    public partial class NetworkUI : Form
    {
        // Skapa nätverksobjektet som samlar alla metoder från
        // ToolServices, ToolPing, ToolMAC, ToolVendor och ToolLatency
        Network network = new Network();

        public NetworkUI()
        {
            InitializeComponent();

            LabelResultIP.Text = "";
            LabelErrorIP.Text = "";
            LabelLoad.Text = "";
            ListConnections.Visible = false;
            LoadProgress.Visible = false;
            
        }

        private async Task GenerateResultAsync(string subnet)
        {

            //
            // Visa egen IP
            //

            string myIP = network.GetLocalIPAddress(subnet);
            LabelResultIP.Text = $"Din IP-adress: {myIP}";


            //
            // Visa aktiva anslutningar i nätverket
            // 

            LabelLoad.Text = $"Går igenom subnet {subnet}0-255...";

            // Aktivera load bar
            LoadProgress.Visible = true;
            LoadProgress.Value = 0;
            LoadProgress.Maximum = 254;
            var progress = new Progress<int>(value => LoadProgress.Value = value);
            
            
            List<(string IP, string MAC, string vendor, string other)> activeConn = await network.ScanSubnetAsync(subnet, progress);

            ListConnections.Visible = true;

            foreach (var (IP, MAC, vendor, other) in activeConn)
            {
                
                string myIPStr = "";
                if (IP == myIP) myIPStr = " - Denna maskin";
                string otherStr = string.IsNullOrEmpty(other) ? "" : $", {other}"; // Så det inte blir extra kolon
                
                ListConnections.Items.Add($"Hittade: {IP}, {MAC}, {vendor}{otherStr}{myIPStr}");
            }
        }

        private async void ButtonIP_Click(object sender, EventArgs e)
        {
            LabelErrorIP.Text = "";

            if (LabelInputIP != null)
            {
                if (int.TryParse(LabelInputIP.Text, out int result))
                {
                    if (result >= 0 && result <= 256)
                    {
                        await GenerateResultAsync($"192.168.{LabelInputIP.Text}.");
                        return;
                    }
                }
            }

            // Om input-fältet inte är giltig int mellan 0-256, visa felmeddelande
            LabelErrorIP.Text = "Ogiltigt nummer. Ange nummer mellan 0-256.";
            if (LabelInputIP == null) LabelErrorIP.Text = "NULL";
        }
    }
}
