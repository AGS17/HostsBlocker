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
        public static String LoadText(string path)
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

        public static void Save(string path, string source)
        {
            File.WriteAllText(path, source);
        }
    }
}
