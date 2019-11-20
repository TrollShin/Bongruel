using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

namespace Bongruel
{
    /// <summary>
    /// loginControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginControl : UserControl
    {
        public delegate void LoginHandler(object sender, EventArgs e);
        public event LoginHandler OnGoBackMainWindow;

        public const string ip = "10.80.162.157";
        public const int port = 80;

        public LoginControl()
        {
            InitializeComponent();
            //App.bNetwork.Connect(ip, port);
            App.bNetwork.OnConnected += CheckServer;
            
        }    


        private void CheckServer(object sender, bool isConnected)
        {
            if(isConnected)
            {
                App.bNetwork.StartReceive();
                return;
            }
        }

        private void Btnlogin_Click(object sender, RoutedEventArgs e)
        {
                goBackMainWindow();
        }

        private void Btnexit_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void disableLogin()
        {
            Logingrid.Visibility = Visibility.Collapsed;
        }

        private void goBackMainWindow()
        {
            OnGoBackMainWindow?.Invoke(this, null);
        }
    }
}
