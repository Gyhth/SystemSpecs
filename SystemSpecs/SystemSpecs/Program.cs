using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;

namespace SystemSpecs
{
    class Program
    {
        private static FileStream output;
        static void Main(string[] args)
        {
            String path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Specs.txt";
            if (File.Exists(path)) {
                File.Delete(path);
            }
            output = File.Open(path, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
            outputHeader("User Information");
            writetoFile("Username: " + Environment.UserName);
            writetoFile("User Domain: " + Environment.UserDomainName);
            writetoFile(Environment.NewLine);
            outputHeader("OS Information");
            OperatingSystem os = Environment.OSVersion;
            writetoFile("Version: " + os.Version.ToString());
            writetoFile("Service Pack: " + os.ServicePack);
            writetoFile("Platform: " + os.Platform);
            writetoFile("64-Bit: " + Environment.Is64BitOperatingSystem);
            writetoFile(Environment.NewLine);
            outputHeader("Network Information");
            ArrayList ips = getIPAddresses();
            if (ips.Count > 0)
            {
                foreach (String ip in ips) {
                    writetoFile("IP:" + ip);
                }
            }
            else
            {
                writetoFile("No IPs found.");
            }
        
        }

        private static void writetoFile(String text)
        {
            Byte[] info = new UTF8Encoding().GetBytes(text + Environment.NewLine);
            output.Write(info, 0, info.Length);
        }

        private static void outputHeader(String title)
        {
            writetoFile(title);
            writetoFile("----------------");

        }

        private static ArrayList getIPAddresses()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            ArrayList ips = new ArrayList();
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                     ips.Add(ip.ToString());
                }
            }
            return ips;
        }

    }
}
