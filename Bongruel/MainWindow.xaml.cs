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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //BNetwork로 넘어감
        /*private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Helper.BNetwork bNetwork = new Helper.BNetwork();
                bNetwork.Create();
                bNetwork.Connect("10.80.163.138", 8000);
                bNetwork.Send(tbInput.Text);
            }

            catch(Exception)
            {
                MessageBox.Show("없음 서버 응답 요청 시작 다시");
            }
        }*/

        private void GoMenuWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            changeUserControl(new MenuWindow());
        }

        private void GoBNetwork_Click(object sender, RoutedEventArgs e)
        {
            changeUserControl(new BNetworkControl());
        }

        //임시
        private void changeUserControl(UserControl userControl)
        {
            buttonGrid.Visibility = Visibility.Collapsed;
            contentControl.Content = userControl;
        }
    }
}
