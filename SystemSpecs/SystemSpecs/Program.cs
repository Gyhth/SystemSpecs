using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.IO;

namespace SystemSpecs
{
    class Program
    {
        static void Main(string[] args)
        {
            String path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Specs.txt";
            if (File.Exists(path)) {
                File.Delete(path);
            }
            FileStream output = File.Open(path, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
            writetoFile(output, "User Information");
            writetoFile(output, "----------------");
            writetoFile(output, "Username: " + Environment.UserName);
            writetoFile(output, "User Domain: " + Environment.UserDomainName);
            writetoFile(output, Environment.NewLine);
            writetoFile(output, "OS Information");
            writetoFile(output, "----------------");
            OperatingSystem os = Environment.OSVersion;
            writetoFile(output, "Version: " + os.Version.ToString());
            writetoFile(output, "Service Pack: " + os.ServicePack);
            writetoFile(output, "Platform: " + os.Platform);
            writetoFile(output, "64-Bit: " + Environment.Is64BitOperatingSystem);
            writetoFile(output, Environment.NewLine);
            writetoFile(output, "Hardware Information");
            writetoFile(output, "----------------");

        }

        static void writetoFile(FileStream outputFile, String text)
        {
            Byte[] info = new UTF8Encoding().GetBytes(text + Environment.NewLine);
            outputFile.Write(info, 0, info.Length);
        }
    }
}
