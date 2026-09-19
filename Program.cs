using System.Net.NetworkInformation;
using System.Reactive.Subjects;
using Nätverksövervakning.UI;

namespace Nätverksövervakning
{
    internal static class Program
    {


        [STAThread]
        static void Main()
        {

            // v. 0.1.5

            ApplicationConfiguration.Initialize();
            Application.Run(new NetworkUI());

            // Logiken flyttad till NetworkUI.cs

        }
    }
}