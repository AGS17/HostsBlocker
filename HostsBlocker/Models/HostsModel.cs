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

        private int selectedItemIndex;
        public HostInfoModel SelectedItem
        {
            get { return this.selectedItemIndex >= 0 ? this[this.selectedItemIndex] : null; }
            set
            {
                if (value != null
                    && !this.Contains(value)
                    && value.Equals(this[this.selectedItemIndex]))
                    return;
                this.selectedItemIndex = this.IndexOf(value);
            }
        }

        public int SelectedItemsCount => this.Count(x => x.IsSelected);

        public bool ContainsTarget(string target)
        {
            return this.Select(x => x.Target).Contains(target);
        }
        public bool ContainsTarget(HostInfoModel item)
        {
            return this.Select(x => x.Target).Contains(item.Target);
        }

        public bool Update(HostInfoModel oldItem, HostInfoModel newItem)
        {
            if (!this.Contains(oldItem))
            {
                return false;
            }
            if (this.ContainsTarget(newItem))
            {
                return false;
            }

            this[this.IndexOf(oldItem)] = new HostInfoModel(newItem);

            return true;
        }
        public bool Update(int oldItemIndex, HostInfoModel newItem)
        {
            if (oldItemIndex < 0 || oldItemIndex >= this.Count)
            {
                return false;
            }
            if (this.ContainsTarget(newItem))
            {
                return false;
            }

            this[oldItemIndex] = new HostInfoModel(newItem);

            return true;
        }

        public bool UpdateSelected(HostInfoModel newItem)
        {
            return this.SelectedItemsCount == 1
                && this.Update(this.selectedItemIndex, newItem);
        }
    }
}