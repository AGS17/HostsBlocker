using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HostsBlocker.Models;

namespace HostsBlocker.Core
{
    public class HostsConverter
    {
        public static HostsModel ToHostsModel(String input)
        {
            const string pattern = @"(?<isBlocking>#)?[\s]*(?<redirectTo>([0-9]{1,3}.){3}[0-9]{1,3})[\s]+(?<target>[^ ]+)[\s]*#(?<comment>[0-9a-zA-Z .,]*)?\r\n";
            var matches = Regex.Matches(input, pattern);

            var hosts = new HostsModel();
            foreach (Match match in matches)
            {
                Console.WriteLine($"{match.Groups["isBlocking"]} {match.Groups["redirectTo"]} {match.Groups["target"]} {match.Groups["comment"]} ");
                var item = new HostInfoModel
                {
                    Comment = match.Groups["comment"].ToString().Trim(),
                    IsBlocking = string.IsNullOrWhiteSpace(match.Groups["isBlocking"].ToString()),
                    RedirectTo = match.Groups["redirectTo"].ToString().Trim(),
                    Target = match.Groups["target"].ToString().Trim()
                };
                hosts.Add(item);
            }
            return hosts;
        }

        public static string ToString(HostsModel hosts)
        {
            var result = string.Empty;
            foreach (HostInfoModel host in hosts)
            {
                if (!host.IsBlocking)
                    result += "#";
                result += $"{host.RedirectTo} {host.Target} #{host.Comment}\r\n";
            }
            return result;
        }
    }
}
