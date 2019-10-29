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
            listTable.Items.Add(new LoadingControl());

            onEnable();
        }

        private void onEnable()
        {
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
            timeText.Text = DateTime.Now.ToString("yyyy년 MM월 dd일 hh:mm:ss dddd");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listTable.Items.Clear();

            Load();

            timeText.Text = DateTime.Now.ToString("yyyy년 MM월 dd일 hh:mm:ss dddd");
            timer.Start();
            addSeats();
            //LoadingControl.Visibility = Visibility.Collapsed;
        }

        private void addSeats() //MainWindow 모든 테이블을 출력
        {
            foreach(Seat seat in App.seatData.listseat) {
                TableControl table = new TableControl();

                table.SetItem(seat);
                listTable.Items.Add(table);
            }
            listTable.Items.Refresh();
        }            
        
        //다른 UserControl의 Visibility를 collapsed로 바꿈
        private void OnGoBackMainWindow(object sender, EventArgs e)
        {
            UserControl currentUserControl = sender as UserControl;
            currentUserControl.Visibility = Visibility.Collapsed;

            mainGrid.Visibility = Visibility.Visible;
        }

        //MenuWindow메인화면으로 돌아올 때 주문한 메뉴를 받기위한 함수
        private void menuWindow_GoBackMainWindow(object sender, OrderEventArgs e)
        {
            if (e != null)
            {               
                Seat item = new Seat();

                item.Id = e.seat.Id;
                item.OrderList = e.seat.OrderList;
                item.orderTime = e.seat.orderTime;

                if (e.isPayment)
                {
                    StatControl.payedFoodData(item.OrderList);

                    (listTable.SelectedItem as TableControl).InitTable();
                }
                else
                {
                    (listTable.SelectedItem as TableControl).SetItem(item);//.seat.OrderList = e.LstOrderedFood;                  
                }         

            listTable.Items.Refresh();
            }

            OnGoBackMainWindow(sender, e);
        }

        //테이블을 선택했을때 실행 ( SelectionChanged 이기 때문에 다른 테이블에 접근하기 전까지 같은 테이블에 접근하지 못해서 수정예정 )
        private void listTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seat seatTableControl = new Seat();
            seatTableControl = (listTable.SelectedItem as TableControl).seat;

            OrderWindow.setOrderMenu(seatTableControl);

            disableMain();
            OrderWindow.Visibility = Visibility.Visible;
        }

        //MenuWindow 가는 임시 버튼 
        private void GoMenuWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            disableMain();
            OrderWindow.Visibility = Visibility.Visible;
        }

        //StatControl 가는 임시 버튼
        private void StatControl_Click(object sender, RoutedEventArgs e)
        {
            disableMain();
            StatControl.Visibility = Visibility.Visible;
        }

        //GoLoginControl 가는 임시 버튼
        private void GoLoginControl_Click(object sender, RoutedEventArgs e)
        {
            disableMain();
            LoginControl.Visibility = Visibility.Visible;
        }

        //MainWindow의 Visibility를 collapsed로 바꿈
        private void disableMain()
        {
            mainGrid.Visibility = Visibility.Collapsed;
        }
    }
}
