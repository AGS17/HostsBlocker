using System.ComponentModel;
using System.Runtime.CompilerServices;
using HostsBlocker.Annotations;
using HostsBlocker.Models;

namespace HostsBlocker.ViewsModels
{
    public sealed class MainWindowViewModel : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}