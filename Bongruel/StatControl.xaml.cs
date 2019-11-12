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
        private Helper.BNetwork bNetwork = new Helper.BNetwork();

        private List<Stat> lstPayedFood;
        /*private List<Food> StatList;*/
        public StatControl()
        {
            InitializeComponent();
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

        public void payedFoodData(List<Food> foodList)
        {
            List<Food> item = new List<Food>(foodList);

            applyPayedFoodData(item);
            totalPrice.Text = getTotalPrice().ToString();

            payedFoodList.Items.Refresh();
        }

        private void applyPayedFoodData(List<Food> foodList)
        {
            List<Stat> lstStat = App.statData.PayedListFood;

            for(int i = 0 ; i < foodList.Count() ; i++)
            {
                Stat item = lstStat.Find(x => x.FoodName == foodList[i].Name);
                item.Count += foodList[i].Count;
                item.Price += foodList[i].Price;
            }
        }

        //메뉴의 Category가 바뀌면 실행
        private void category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewItem item = category.SelectedItem as ListViewItem;
            List<Stat> lstSelectedFood = App.statData.PayedListFood;

            if (!(item.Content.ToString().Equals("전체")))
            {
                Category selectCategory = (Category)Enum.Parse(typeof(Category), item.Tag.ToString());
                lstSelectedFood = App.statData.PayedListFood.Where(x => x.FoodCategory == selectCategory).ToList();
            }

            lstPayedFood = lstSelectedFood;

            payedFoodList.ItemsSource = lstPayedFood;
        }

        private int getTotalPrice()
        {
            int result = 0;
            List<Stat> lstStat = App.statData.PayedListFood;

            for(int i = 0; i < lstStat.Count(); i++)
            {
                result += lstStat[i].Price;
            }

            return result;
        }
        
        private void TotalPriceSend_Click(object sender, RoutedEventArgs e)
        {
            totalPrice.Text = getTotalPrice().ToString();
                    
            if (bNetwork.CheckServer("10.80.163.138", 8000) == true)
            {
                bNetwork.Send("총 매출액: " + totalPrice.Text + "");
                MessageBox.Show("성공적으로 통계를 보냈습니다.");
            }
            else
            {
                Debug.WriteLine("총 매출액: " + totalPrice.Text + "");
                MessageBox.Show("서버 접속 불가: 서버 응답 없음");
                
            }
        }
    }

}


