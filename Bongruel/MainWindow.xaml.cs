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
using System.Windows.Threading;

namespace Bongruel
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
//            listTable.Items.Add(new LoadingControl());

            OrderWindow.OnGoBackMainWindow += menuWindow_GoBackMainWindow;
            StatControl.OnGoBackMainWindow += OnGoBackMainWindow;
            LoginControl.OnGoBackMainWindow += OnGoBackMainWindow;
            timer.Tick += Timer_Tick;
        }

        private void Load()
        {
            App.seatData.Load();
            App.foodData.Load();

            timer.Interval = TimeSpan.FromSeconds(1);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeText.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listTable.Items.Clear();

            Load();

            dayText.Text = DateTime.Now.ToString("yyyy년 MM월 dd일 dddd");
            timeText.Text = DateTime.Now.ToString("hh:mm:ss");
            timer.Start();
            addSeats();
            //LoadingControl.Visibility = Visibility.Collapsed;

            //*******************************************************푸시하기전에 확인*****************************************************************************************
            changeUserControl(LoginControl);
        }

        //MainWindow 모든 테이블을 출력
        private void addSeats() 
        {
            foreach(Seat seat in App.seatData.listseat) {
                TableControl table = new TableControl();

                table.SetItem(seat);
                listTable.Items.Add(table);
            }
            listTable.Items.Refresh();
        }            
        
        //메인윈도우로 돌아갈때 실행되는 함수
        private void OnGoBackMainWindow(object sender, EventArgs e)
        {
            UserControl currentUserControl = sender as UserControl;

            goBackMainWindow(currentUserControl);
        }

        //MainWindow에서 메인화면으로 돌아올 때 실행되는 함수
        private void menuWindow_GoBackMainWindow(object sender, OrderEventArgs e)
        {
            //e == null 이라면 돌아가기 버튼
            //e != null 이라면 주문하기 or 결제하기
            if (e != null)
            {               
                Seat item = new Seat();

                item.Id = e.seat.Id;
                item.OrderList = e.seat.OrderList;
                item.orderTime = e.seat.orderTime;

                addOrderedMenu(item, e.isPayment);

            listTable.Items.Refresh();
            }

            OnGoBackMainWindow(sender, e);
        }

        //MenuWindow에서 메인화면으로 돌아올 때 주문한 메뉴를 받기위한 함수
        private void addOrderedMenu(Seat seat, bool isPayment)
        {
            if (isPayment)
            {
                StatControl.payedFoodData(seat.OrderList);

                (listTable.SelectedItem as TableControl).InitTable();
            }
            else
            {
                (listTable.SelectedItem as TableControl).SetItem(seat);          
            }
        }

        //테이블을 선택했을때 실행
        private void listTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seat seatTableControl = new Seat();
            seatTableControl = (listTable.SelectedItem as TableControl).seat;

            OrderWindow.setOrderMenu(seatTableControl);

            changeUserControl(OrderWindow);
        }

        //StatControl 가는 버튼
        private void StatControl_Click(object sender, RoutedEventArgs e)
        {
            changeUserControl(StatControl);
        }

        //BNetworkControl 가는 버튼
        private void BNetworkControl_Click(object sender, RoutedEventArgs e)
        {
            changeUserControl(BNetwork);
        }

        //메인으로 돌아가기
        private void goBackMainWindow(UserControl curUserControl)
        {
            curUserControl.Visibility = Visibility.Collapsed;
            mainGrid.Visibility = Visibility.Visible;
        }
        
        //다른 UserControl로 가기
        private void changeUserControl(UserControl userControl)
        {
            mainGrid.Visibility = Visibility.Collapsed;
            userControl.Visibility = Visibility.Visible;
        }
    }
}
