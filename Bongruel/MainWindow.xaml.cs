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
using GruelModel;

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

            OrderWindow.OnGoBackMainWindow += OnGoBackMainWindow;
            StatControl.OnGoBackMainWindow += OnGoBackMainWindow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            App.seatData.Load();

            addSeatList();
        }

        private void addSeatList()
        {
            foreach(Seat seat in App.seatData.listseat) {
                TableControl table = new TableControl();

                table.SetItem(seat);
                listTable.Items.Add(table);
            }
            listTable.Items.Refresh();
        }

        private void GoMenuWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            disableMain();
            OrderWindow.Visibility = Visibility.Visible;
        }

        private void StatControl_Click(object sender, RoutedEventArgs e)
        {
            disableMain();
            StatControl.Visibility = Visibility.Visible;
        }

        private void disableMain()
        {
            mainGrid.Visibility = Visibility.Collapsed;
        }

        private void OnGoBackMainWindow(object sender)
        {
            UserControl currentUserControl = sender as UserControl;
            currentUserControl.Visibility = Visibility.Collapsed;

            mainGrid.Visibility = Visibility.Visible;
        }
    }
}
