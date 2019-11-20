using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Bongruel;
using GruelModel;

namespace Bongruel
{
    /// <summary>
    /// StatControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StatControl : UserControl
    {
        public delegate void StatEventHandler(object sender, EventArgs e);
        public event StatEventHandler OnGoBackMainWindow;
        private Helper.BNetwork bNetwork = App.bNetwork;

        public const string ip = "10.80.162.157";
        public const int port = 80;
        string id = "@2114";

        private List<Food> lstPayedFood;
        /*private List<Food> StatList;*/
        public StatControl()
        {
            InitializeComponent();
            bNetwork.Connect(ip, port);
            
        }

        public void init()
        {
            lstPayedFood = App.statData.PayedListFood;

            payedFoodList.ItemsSource = lstPayedFood;
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            OnGoBackMainWindow?.Invoke(this, null);
        }

        public void payedFoodData(Seat seat, int price)
        {
            List<Food> item = new List<Food>(seat.OrderList);

            applyPayedFoodData(item);
            totalPrice.Text = (int.Parse(totalPrice.Text) + price).ToString();
            sendPaymentData(seat);

            payedFoodList.Items.Refresh();
        }               

        private void applyPayedFoodData(List<Food> foodList)
        {
            List<Food> lstStat = App.statData.PayedListFood;

            for(int i = 0 ; i < foodList.Count() ; i++)
            {
                Food item = lstStat.Find(x => x.Name == foodList[i].Name);
                item.Count += foodList[i].Count;
                item.Price += foodList[i].Price;
            }
        }

        //메뉴의 Category가 바뀌면 실행
        private void category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewItem item = category.SelectedItem as ListViewItem;
            List<Food> lstSelectedFood = App.statData.PayedListFood;

            if (!(item.Content.ToString().Equals("전체")))
            {
                Category selectCategory = (Category)Enum.Parse(typeof(Category), item.Tag.ToString());
                lstSelectedFood = App.statData.PayedListFood.Where(x => x.category == selectCategory).ToList();
            }

            lstPayedFood = lstSelectedFood;

            payedFoodList.ItemsSource = lstPayedFood;
        }

        /*private int getTotalPrice(List<Food> lstFood)
        {
            int result = 0;
            List<Food> lstItem = lstFood;

            for(int i = 0; i < lstItem.Count(); i++)
            {
                result += lstItem[i].Price;
            }

            return result;
        }*/
        
        private void TotalPriceSend_Click(object sender, RoutedEventArgs e)
        {
            bNetwork.Send(id + "#총 매출액: " + totalPrice.Text + "원");
            MessageBox.Show("성공적으로 통계를 보냈습니다.");
        }

        private void sendPaymentData(Seat seat)
        {
            String text = id + "#" + seat.Id + " 테이블 " + totalPrice.Text + "원 결제";

            bNetwork.Send(text);
        }
    }

}


