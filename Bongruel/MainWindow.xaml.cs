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



        //임시 컨트롤 전환 (다른 방법을 찾아야함) 
        private void GoMenuWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            switchUserControl(new MenuWindow());
        }

        private void GoBNetwork_Click(object sender, RoutedEventArgs e)
        {
            switchUserControl(new BNetworkControl());
        }

        //임시
        private void switchUserControl(UserControl userControl)
        {
            buttonGrid.Visibility = Visibility.Collapsed;
            contentControl.Content = userControl;
        }
    }
}
