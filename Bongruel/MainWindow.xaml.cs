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
using System.Diagnostics;

namespace Bongruel
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();

        private bool isLoaded = false;

        public MainWindow()
        { 
            InitializeComponent();

            OrderWindow.OnGoBackMainWindow += menuWindow_GoBackMainWindow;
            StatControl.OnGoBackMainWindow += OnGoBackMainWindow;
            LoginControl.OnGoBackMainWindow += OnGoBackMainWindow;
           // App.bNetwork.OnConnected += CheckServer;
            App.bNetwork.OnDisConncected += Disconnected;

            timer.Tick += Timer_Tick;
            timer.Tick += Timer_CheckLoad;
        }

        /*
        private void CheckServer(object sender, bool isConnected)
        {
            if(isConnected == false) //chris - 서버가 종료되면
            {    
                MessageBox.Show("서버와의 연결이 끊겼습니다");
                connectedDate.Text = DateTime.Now.ToString("yyyy.dddd.MM.dd hh:mm:ss");
               changeUserControl(LoginControl);
            }

            #if false
            if(isConnected)
            {
                return;
            }
            MessageBox.Show("서버와의 연결이 끊겼습니다");
            connectedDate.Text = DateTime.Now.ToString("yyyy.dddd.MM.dd hh:mm:ss");
            #endif
        }
        */

        private void Disconnected(object sender, bool? isConnected)
        {
            MessageBox.Show("서버와의 연결이 끊겼습니다", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Dispatcher.BeginInvoke(new Action(() => {
                Login.Visibility = Visibility.Visible;
                connectedDate.Text = DateTime.Now.ToString("yyyy.dddd.MM.dd hh:mm:ss");
            }));
         }

        #if false
        if(isConnected)
        {
            return;
        }
        MessageBox.Show("서버와의 연결이 끊겼습니다");
        connectedDate.Text = DateTime.Now.ToString("yyyy.dddd.MM.dd hh:mm:ss");
        #endif


        private void Load()
        {
            if(isLoaded)
            {
                return;
            }
            isLoaded = true;

            App.seatData.Load();
            App.foodData.Load();
            App.statData.Load();

            addSeats();
            OrderWindow.init();
            StatControl.init();
        }

        //현재 시간 표시
        private void Timer_Tick(object sender, EventArgs e)
        {
            timeText.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        //모두 Load 되었는지 확인하고 모두 Load 되었다면 로딩을 종료 
        private void Timer_CheckLoad(object sender, EventArgs e)
        {
            Load();

            if(listTable.Items.Count != 0)
            {
                Loading.Visibility = Visibility.Collapsed;                
                timer.Tick -= Timer_CheckLoad;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listTable.Items.Clear();

            timer.Interval = TimeSpan.FromSeconds(1);

            dayText.Text = DateTime.Now.ToString("yyyy년 MM월 dd일 dddd");
            timeText.Text = DateTime.Now.ToString("hh:mm:ss");
            timer.Start();
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

        //MenuWindow에서 메인화면으로 돌아올 때 실행되는 함수
        private void menuWindow_GoBackMainWindow(object sender, OrderEventArgs e)
        {
            //e == null 이라면 돌아가기 버튼
            //e != null 이라면 주문하기 or 결제하기
            if (e != null)
            {
                addOrderedMenu(e);

                listTable.Items.Refresh();
            }

            OnGoBackMainWindow(sender, e);
        }

        //MenuWindow에서 메인화면으로 돌아올 때 주문한 메뉴를 받기위한 함수
        private void addOrderedMenu(OrderEventArgs e)
        {
            if (e.isPayment)
            {
                StatControl.payedFoodData(e.seat, e.totalPrice);

                (listTable.SelectedItem as TableControl).InitTable();
            }
            else
            {
                (listTable.SelectedItem as TableControl).SetItem(e.seat);          
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
            //changeUserControl(BNetwork);
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
            userControl.Visibility = Visibility.Visible;
            mainGrid.Visibility = Visibility.Collapsed;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            App.bNetwork.Send(IdTextBox.Text);
            MessageBox.Show("로그인 성공");
            App.bNetwork.StartReceive();
            loginDate.Text = DateTime.Now.ToString("yyyy.dddd.MM.dd hh:mm:ss");
            Login.Visibility = Visibility.Collapsed;
        }
    }
}
