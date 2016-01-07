using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HostsBlocker.Core;
using HostsBlocker.Models;

namespace HostsBlocker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HostsModel hosts;

        public MainWindow()
        {
            this.InitializeComponent();

            this.hosts = HostsConverter.ToHostsModel(FileWorker.LoadText());
            this.DataContext = this.hosts;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.TargetTextBox.Text))
            {
                this.ErrorTextBlock.Text = "Fill target box out";
                this.TargetTextBox.SelectAll();
                this.TargetTextBox.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(this.RedirectTextBox.Text))
            {
                this.ErrorTextBlock.Text = "Fill redirect box out";
                this.RedirectTextBox.SelectAll();
                this.RedirectTextBox.Focus();
                return;
            }

            this.hosts.Add(new HostInfoModel
            {
                Comment = this.TitleTextBox.Text,
                IsBlocking = this.IsBlockingCheckBox.IsChecked.GetValueOrDefault(),
                RedirectTo = this.RedirectTextBox.Text,
                Target = this.TargetTextBox.Text
            });
        }
    }
}
