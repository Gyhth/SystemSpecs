using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SystemSpecs
{
    class Pinger
    {
       

        public Boolean pingAddress(String address)
        {
            bool pingable = false;
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send(address);
            pingable = reply.Status == IPStatus.Success;   
            return pingable;
        }

        public String getNextAddress(String address)
        {
            String nextAddress = "";
            string[] splitCharacter = new string[] { "." };
            String[] addressParts = address.Split(splitCharacter, StringSplitOptions.RemoveEmptyEntries);
            byte classDAddress = byte.Parse(addressParts.Last());
            if (classDAddress < 255) {
                classDAddress += 1;
                nextAddress = addressParts[0] + "." + addressParts[1] + "." + addressParts[2] + "." + classDAddress;
            }
            return nextAddress;
        }

        public List<String> buildSubnet(String address)
        {
            Boolean moreAddresses = true;
            List<String> addresses = new List<String>();
            String nextAddress = "";
            String currentAddress = address;
            while (moreAddresses)
            {
                nextAddress = getNextAddress(currentAddress);
                if (nextAddress == String.Empty)
                {
                    moreAddresses = false;
                }
                else
                {
                    addresses.Add(nextAddress);
                }
                currentAddress = nextAddress;
            }
            return addresses;
        }

        private async Task<List<PingReply>> PingAsync(List<String> addresses)
        {
            Ping pingSender = new Ping();
            var tasks = addresses.Select(ip => new Ping().SendPingAsync(ip, 2000));
            var results = await Task.WhenAll(tasks);
            return results.ToList();
        }

        public async Task<List<PingReply>> pingSubnet(String address)
        {
            List<String> addresses = buildSubnet(address);
            List<PingReply> results = await PingAsync(addresses);
            return results;
        }
    }

}
