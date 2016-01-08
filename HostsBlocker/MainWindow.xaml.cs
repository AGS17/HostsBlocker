using System.Collections;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using HostsBlocker.Core;
using HostsBlocker.Models;
using HostsBlocker.ViewsModels;

namespace HostsBlocker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel dataViewModel;

        public MainWindow()
        {
            this.InitializeComponent();

            this.dataViewModel = new MainWindowViewModel();
            this.DataContext = this.dataViewModel;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var deletingObjects = new ArrayList(this.HostsListBox.SelectedItems);
            foreach (var item in deletingObjects)
            {
                this.dataViewModel.Hosts.Remove((HostInfoModel) item);
            }
        }

        private bool DefaultTextBoxChecker(TextBox target, string regexPattern)
        {
            if (string.IsNullOrWhiteSpace(target.Text))
            {
                this.dataViewModel.ErrorMessage = $"Fill out the {target.ToolTip}";
                target.Focus();
                return false;
            }

            if (!Regex.IsMatch(target.Text, regexPattern))
            {
                this.dataViewModel.ErrorMessage = $"The {target.ToolTip} is not valid";
                target.SelectAll();
                target.Focus();
                return false;
            }

            return true;
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var item = this.GenerateHostInfo();
            if (item == null)
                return;
            if (this.dataViewModel.Hosts.Contains(item))
            {
                this.dataViewModel.ErrorMessage = "The item with same Target address is issue";
                return;
            }
            this.dataViewModel.Hosts.Add(item);
        }

        private void HostsListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var current = (HostInfoModel) ((ListBox) sender).SelectedItem;
            if(current == null) return;

            this.IsBlockingCheckBox.IsChecked = current.IsBlocking;
            this.TitleTextBox.Text = current.Title;
            this.TargetTextBox.Text = current.Target;
            this.RedirectTextBox.Text = current.RedirectTo;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var messageBoxResult =
                MessageBox.Show(
                    "Do you want to update hosts list?\nYes - save and exit\nNo - exit without saving\nCancel - return",
                    "What you want?", MessageBoxButton.YesNoCancel);
            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes:
                    FileWorker.Save(MainWindowViewModel.HostsPath, HostsConverter.ToString(this.dataViewModel.Hosts));
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }

        private HostInfoModel GenerateHostInfo()
        {
            if (!this.DefaultTextBoxChecker(this.TargetTextBox, @"^([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$"))
                return null;

            if (!this.DefaultTextBoxChecker(this.RedirectTextBox, @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$"))
                return null;

            this.dataViewModel.ErrorMessage = null;

            return new HostInfoModel
            {
                Comment = this.TitleTextBox.Text,
                IsBlocking = this.IsBlockingCheckBox.IsChecked.GetValueOrDefault(),
                RedirectTo = this.RedirectTextBox.Text,
                Target = this.TargetTextBox.Text
            };
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var item = this.GenerateHostInfo();
            if (item == null)
                return;
            if (this.HostsListBox.SelectedItems.Count > 1)
            {
                this.dataViewModel.ErrorMessage = "Has been selected more than 1 item";
                return;
            }
            var updatingItemIndex = this.HostsListBox.SelectedIndex;
            if (updatingItemIndex < 0 || updatingItemIndex >= this.dataViewModel.Hosts.Count)
            {
                this.dataViewModel.ErrorMessage = "Nothing selected";
                return;
            }
            this.dataViewModel.Hosts[updatingItemIndex] = item;
        }
    }
}
