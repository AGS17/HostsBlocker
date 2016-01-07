using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HostsBlocker.Models
{
    public class HostsModel : ObservableCollection<HostInfoModel>
    {
        public HostsModel() { }
        public HostsModel(HostInfoModel host)
        {
            this.Add(host);
        }
        public HostsModel(IEnumerable<HostInfoModel> hosts)
        {
            foreach (var host in hosts)
            {
                this.Add(host);
            }
        }

        public new void Add(HostInfoModel item)
        {
            if (this.Contains(item)) return;
            base.Add(item);
        }

        public new bool Contains(HostInfoModel item)
        {
            return this.Select(x => x.Target).Contains(item.Target);
        }
        
    }
}