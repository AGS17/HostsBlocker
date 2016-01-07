using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostsBlocker.Models;

namespace HostsBlocker.Core
{
    public class FileWorker
    {
        public static string LoadText(string path = "c:\\Windows\\System32\\drivers\\etc\\hosts")
        {
            var result = string.Empty;
            try
            {
                using (var sr = new StreamReader(path))
                {
                    result = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"The file could not be read: {e.Message}");
            }
            return result;
        }
    }
}
