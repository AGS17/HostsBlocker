using System.ComponentModel;
using System.Runtime.CompilerServices;
using HostsBlocker.Annotations;

namespace HostsBlocker.Models
{
    public abstract class MainWindowViewModel : INotifyPropertyChanged
    {
        public HostsModel Hosts { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}