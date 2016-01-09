using System.Collections;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using HostsBlocker.Core;
using HostsBlocker.Models;
using HostsBlocker.ViewModels;

namespace HostsBlocker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private new MainWindowViewModel DataContext => base.DataContext as MainWindowViewModel;

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var messageBoxResult =
                MessageBox.Show(
                    "Do you want to update hosts list?\nYes - save and exit\nNo - exit without saving\nCancel - return",
                    "What you want?", MessageBoxButton.YesNoCancel);
            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes:
                    FileWorker.Save(MainWindowViewModel.HostsPath, HostsConverter.ToString(this.DataContext.Hosts));
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }
    }
}
