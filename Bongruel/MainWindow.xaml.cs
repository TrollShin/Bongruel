using System;
using System.Collections.Generic;
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
using System.Net.Sockets;

namespace Bongruel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GoMenuWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow.Visibility = Visibility.Visible;
        }

        private void GoBNetwork_Click(object sender, RoutedEventArgs e)
        {
            BNetwork.Visibility = Visibility.Visible;
        }
    }
}
