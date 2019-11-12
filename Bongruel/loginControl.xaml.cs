﻿using System;
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

namespace Bongruel
{
    /// <summary>
    /// loginControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginControl : UserControl
    {
        public delegate void LoginHandler(object sender, EventArgs e);
        public event LoginHandler OnGoBackMainWindow;
        private Helper.BNetwork bNetwork = new Helper.BNetwork();

        public LoginControl()
        {
            InitializeComponent();
            bNetwork.Create();
            bNetwork.Connect("10.80.163.138", 80);
        }

    

        private void Btnlogin_Click(object sender, RoutedEventArgs e)
        {
            if (id.Text == "@2114")
            {
                bNetwork.Send(id.Text);
                goBackMainWindow();
            }
            else
            {
                MessageBox.Show("아이디를 다시 확인해주세요");
            }
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
            if(OnGoBackMainWindow != null)
            {
                OnGoBackMainWindow(this, null);
               
            }
        }
    }
}
