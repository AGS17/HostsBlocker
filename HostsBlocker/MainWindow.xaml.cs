using System.Collections;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
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

        private string DefaultTextBoxChecker(TextBox target, string regexPattern)
        {
            string result = null;

            if (string.IsNullOrWhiteSpace(target.Text))
            {
                result = $"Fill out the {target.ToolTip}";
                target.Focus();
                return result;
            }

            if (!Regex.IsMatch(target.Text, regexPattern))
            {
                result = $"The {target.ToolTip} is not valid";
                target.SelectAll();
                target.Focus();
                return result;
            }

            return result;
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var errorMessage = this.DefaultTextBoxChecker(this.TargetTextBox, @"^([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$");
            this.ErrorTextBlock.Text = errorMessage;
            if (errorMessage != null)
                return;

            errorMessage = this.DefaultTextBoxChecker(this.RedirectTextBox, @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
            this.ErrorTextBlock.Text = errorMessage;
            if (errorMessage != null)
                return;

            this.dataViewModel.Hosts.Add(new HostInfoModel
            {
                Comment = this.TitleTextBox.Text,
                IsBlocking = this.IsBlockingCheckBox.IsChecked.GetValueOrDefault(),
                RedirectTo = this.RedirectTextBox.Text,
                Target = this.TargetTextBox.Text
            });
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
    }
}
