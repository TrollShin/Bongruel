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

        private List<Food> lstPayedFood = new List<Food>();
        /*private List<Food> StatList;*/
        public StatControl()
        {
            InitializeComponent();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            /*if (OnGoBackMainWindow != null)
            {
                OnGoBackMainWindow(this, null);
            }*/
            OnGoBackMainWindow?.Invoke(this, null);
        }

        public void payedFoodData(List<Food> foodList)
        {
            foreach (Food item in foodList)
            {
                lstPayedFood.Add(item);
            }

            //payedFoodList.ItemsSource = foodList;

            foreach(Food item in lstPayedFood)
            {
                payedFoodList.Items.Add(item);
            }

            payedFoodList.Items.Refresh();
        }

        //메뉴의 Category가 바뀌면 실행
        private void category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewItem item = category.SelectedItem as ListViewItem;
            List<Food> lstSelectedFood = new List<Food>();

            if (item.Content.ToString().Equals("전체"))
            {
                lstSelectedFood = new List<Food>(App.statData.PayedListFood);
            }
            else
            {
                Category selectCategory = (Category)Enum.Parse(typeof(Category), item.Tag.ToString());
                lstSelectedFood = new List<Food>(App.statData.PayedListFood).Where(x => x.category == selectCategory).ToList();
            }


        }
    }
}


