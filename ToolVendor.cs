using System.Net.NetworkInformation;
using MacAddressVendorLookup;

namespace Nätverksövervakning
{

    public class ToolVendor
    {
        private MacVendorBinaryReader vendorInfoProvider;
        private AddressMatcher addressMatcher;

        public ToolVendor()
        {
            // Initiera MacVendorBinaryReader och AddressMatcher för GetVendor()

            vendorInfoProvider = new MacVendorBinaryReader();
            using (var resourceStream = ManufBinResource.GetStream().Result)
                vendorInfoProvider.Init(resourceStream).Wait();
            addressMatcher = new AddressMatcher(vendorInfoProvider);
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
    }
}
