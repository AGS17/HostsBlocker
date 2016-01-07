using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HostsBlocker.Models
{
    public class HostsModel : IEnumerable<HostInfoModel>, ICollection<HostInfoModel>
    {
        private List<HostInfoModel> hosts;

        public HostsModel()
        {
            this.hosts = new List<HostInfoModel>();
        }
        public HostsModel(HostInfoModel host)
        {
            this.hosts = new List<HostInfoModel> { host };
        }
        public HostsModel(IEnumerable<HostInfoModel> hosts)
        {
            this.hosts = new List<HostInfoModel>(hosts);
        }

        public List<HostInfoModel> Hosts => this.hosts;
        //public List<string> Names => this.hosts.Select(x => x.Target).ToList();

        public int Count => this.hosts.Count;

        public bool IsReadOnly => false;

        public void Add(HostInfoModel item)
        {
            this.hosts.Add(item);
        }

        public void Clear()
        {
            this.hosts.Clear();
        }

        public bool Contains(HostInfoModel item)
        {
            return this.hosts.Contains(item);
        }

        public void CopyTo(HostInfoModel[] array, int arrayIndex)
        {
            this.hosts.CopyTo(array, arrayIndex);
        }

        public bool Remove(HostInfoModel item)
        {
            return this.hosts.Remove(item);
        }



        IEnumerator<HostInfoModel> IEnumerable<HostInfoModel>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            return new HostEnumerator(this);
        }

        private class HostEnumerator : IEnumerator<HostInfoModel>
        {
            private int position = -1;
            private readonly HostsModel hosts;

            public HostEnumerator(HostsModel hosts)
            {
                this.hosts = hosts;
            }

            public bool MoveNext()
            {
                if (this.position < this.hosts.hosts.Count - 1)
                {
                    this.position++;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
            public void Reset()
            {
                this.position = -1;
            }

            object IEnumerator.Current => this.hosts.hosts[this.position];
            public HostInfoModel Current => this.hosts.hosts[this.position];
            public void Dispose() { }
        }
    }
}