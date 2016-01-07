﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using HostsBlocker.Annotations;
using HostsBlocker.Core;
using HostsBlocker.Models;

namespace HostsBlocker.ViewsModels
{
    public sealed class MainWindowViewModel : INotifyPropertyChanged
    {
        private const string HostsPath = "c:\\Windows\\System32\\drivers\\etc\\hosts";

        private HostsModel hosts;

        public MainWindowViewModel()
        {
            this.hosts = HostsConverter.ToHostsModel(FileWorker.LoadText(HostsPath));
        }

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