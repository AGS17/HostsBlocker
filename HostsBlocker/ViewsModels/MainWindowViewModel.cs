using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HostsBlocker.Annotations;
using HostsBlocker.Models;

namespace HostsBlocker.ViewsModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private HostsModel hosts;

        public HostsModel Hosts
        {
            get { return this.hosts; }
            set
            { 
                this.hosts = value;
                this.OnPropertyChanged(nameof(this.Hosts));
            }
        }

        //public void AddHost(HostInfoModel host)
        //{
        //    this.hosts.Add(host);
        //    //this.OnCollectionChanged(NotifyCollectionChangedAction.Add, nameof(this.Hosts));
        //}

        //public bool RemoveHost(HostInfoModel host)
        //{
        //    var result = this.hosts.Remove(host);
        //    //if (result)
        //    //    this.OnCollectionChanged(NotifyCollectionChangedAction.Remove, nameof(this.Hosts));
        //    return result;
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}