using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Input;
using HostsBlocker.Core;
using HostsBlocker.Models;
using HostsBlocker.Utils;
using ScreenSaver.Main.Utils.Commands;

namespace HostsBlocker.ViewModels
{
    public sealed class MainWindowViewModel : BaseViewModel
    {
        public const string HostsPath = "c:\\Windows\\System32\\drivers\\etc\\hosts";

        #region Properties
        public HostsModel Hosts { get; set; }

        private HostInfoModel currentHostInfo;
        private HostInfoModel CurrentHostInfo
        {
            get { return this.currentHostInfo; }
            set
            {
                if (value == null)
                    return;

                this.currentHostInfo = value;
                this.OnPropertyChanged(nameof(this.CurrentTitle));
                this.OnPropertyChanged(nameof(this.CurrentTarget));
                this.OnPropertyChanged(nameof(this.CurrentRedirect));
                this.OnPropertyChanged(nameof(this.CurrentIsBlocking));
            }
        }

        public string CurrentTitle
        {
            get { return this.CurrentHostInfo.Title; }
            set
            {
                this.CurrentHostInfo.Title = value;
                this.OnPropertyChanged(nameof(this.CurrentTitle));
            }
        }
        public string CurrentTarget
        {
            get { return this.CurrentHostInfo.Target; }
            set
            {
                if (!this.IsValid(value, "Target box", @"^([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$"))
                    return;
                this.CurrentHostInfo.Target = value;
                this.OnPropertyChanged(nameof(this.CurrentTarget));
            }
        }
        public string CurrentRedirect
        {
            get { return this.CurrentHostInfo.RedirectTo; }
            set
            {
                if (!this.IsValid(value, "Redirect box", @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$"))
                    return;
                this.CurrentHostInfo.RedirectTo = value;
                this.OnPropertyChanged(nameof(this.CurrentRedirect));
            }
        }
        public bool CurrentIsBlocking
        {
            get { return this.CurrentHostInfo.IsBlocking; }
            set
            {
                this.CurrentHostInfo.IsBlocking = value;
                this.OnPropertyChanged(nameof(this.CurrentIsBlocking));
            }
        }

        public HostInfoModel SelectedItem
        {
            get { return this.Hosts.SelectedItem; }
            set
            {
                if (value == null)
                    return;

                this.Hosts.SelectedItem = value;
                this.CurrentHostInfo = new HostInfoModel(value);
            }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get { return this.errorMessage; }
            set
            {
                this.errorMessage = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    new Timer(state => this.ErrorMessage = null,
                        this.ErrorMessage, 3000, 0);
                }
                this.OnPropertyChanged(nameof(this.ErrorMessage));
            }
        }

        #endregion

        #region Methods

        private bool IsValid(string text, string name, string pattern = null)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                this.ErrorMessage = $"Fill out the {name}";
                return false;
            }

            if (pattern != null && !Regex.IsMatch(text, pattern))
            {
                this.ErrorMessage = $"The {name} is not valid";
                return false;
            }
            this.ErrorMessage = null;

            return true;
        }

        private bool IsValid()
        {
            var result = string.IsNullOrWhiteSpace(this.ErrorMessage);
            if (!result)
            {
                this.ErrorMessage = "Something wrong";
            }
            return result;
        }

        #endregion

        #region Commands

        public ICommand DeleteSelectedCommand { get; private set; }
        private void DeleteSelectedMethod()
        {
            var deletingObjects = new List<HostInfoModel>(this.Hosts.Where(x => x.IsSelected));
            foreach (var deletingItem in deletingObjects)
            {
                this.Hosts.Remove(deletingItem);
            }
        }

        public ICommand UpdateCurrentCommand { get; private set; }
        private void UpdateCurrentMethod()
        {
            if (this.Hosts.SelectedItemsCount < 1)
            {
                this.ErrorMessage = "Nothing selected";
                return;
            }
            if (this.Hosts.SelectedItemsCount > 1)
            {
                this.ErrorMessage = "More than 1 item selected";
                return;
            }
            if (this.Hosts.ContainsTarget(this.CurrentHostInfo))
            {
                this.ErrorMessage = $"The item with same Target address ({this.CurrentHostInfo.Target}) is issue";
                return;
            }
            if (!this.Hosts.UpdateSelected(this.CurrentHostInfo))
            {
                this.ErrorMessage = "Something was wrong. The host has not been updated";
            }
            
        }

        public ICommand AddNewCommand { get; private set; }
        private void AddNewMethod()
        {
            if (this.Hosts.ContainsTarget(this.CurrentHostInfo))
            {
                this.ErrorMessage = $"The item with same Target address ({this.CurrentHostInfo.Target}) is issue";
                return;
            }
            this.Hosts.Add(this.CurrentHostInfo);
        }

        #endregion

        public MainWindowViewModel()
        {
            this.ErrorMessage = null;
            this.Hosts = HostsConverter.ToHostsModel(FileWorker.LoadText(HostsPath));
            this.CurrentHostInfo = new HostInfoModel { Target = "target.domain", RedirectTo = "0.0.0.0" };

            this.DeleteSelectedCommand = new SimpleCommand(this.DeleteSelectedMethod);
            this.UpdateCurrentCommand = new SimpleCommand(this.UpdateCurrentMethod);
            this.AddNewCommand = new SimpleCommand(this.AddNewMethod);
        }
    }
}