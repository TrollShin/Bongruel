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

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            Helper.BNetwork bNetwork = new Helper.BNetwork();
            bNetwork.Create();
            bNetwork.Connect("10.80.163.138", 8000);
            bNetwork.Send(tbInput.Text);
        }

        //임시 컨트롤 전환 (다른 방법을 찾아야함) 
        private void GoMenuWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new MenuWindow();
        }
    }
}
