using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HostsBlocker.Annotations;
using HostsBlocker.Core;
using HostsBlocker.Models;

namespace HostsBlocker.ViewsModels
{
    public sealed class MainWindowViewModel : INotifyPropertyChanged
    {
        public const string HostsPath = "c:\\Windows\\System32\\drivers\\etc\\hosts";

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

        private string errorMessage;
        public string ErrorMessage {
            get { return this.errorMessage; }
            set
            {
                this.errorMessage = value;
                this.OnPropertyChanged(nameof(this.ErrorMessage));
            }
        }

        public MainWindowViewModel()
        {
            this.errorMessage = string.Empty;
            this.hosts = HostsConverter.ToHostsModel(FileWorker.LoadText(HostsPath));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}