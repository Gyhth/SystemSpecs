using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

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
            try
            {
                String ip = getIPAddress();
                writetoFile("IP:" + ip);
            }
            catch (Exception noAddress)
            {
                writetoFile("IP: Unable to obtain IP:" + noAddress.Message);
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

        private static String getIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No IP Found");
        }

    }
}
