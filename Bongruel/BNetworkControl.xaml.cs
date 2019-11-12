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

namespace Bongruel
{
    /// <summary>
    /// Interaction logic for BNetworkControl.xaml
    /// </summary>
    public partial class BNetworkControl : UserControl
    {
       private Helper.BNetwork bNetwork = new Helper.BNetwork();

        public BNetworkControl()
        {
            InitializeComponent();
            tbInput.Text = "@2114";
            bNetwork.Create();
            bNetwork.Connect("10.80.163.138", 80);
        }
        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                bNetwork.Send(tbInput.Text);
            }

            catch (Exception ex)
            {
               
                MessageBox.Show(ex.Message);
            }

        }
    }
}
