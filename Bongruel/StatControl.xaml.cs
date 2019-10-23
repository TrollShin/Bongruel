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
        /*private List<Food> StatList;*/
        public StatControl()
        {
            InitializeComponent();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            if (OnGoBackMainWindow != null)
            {
                OnGoBackMainWindow(this, null);
            }
        }
         //메뉴의 Category가 바뀌면 실행
             private void category_SelectionChanged(object sender, SelectionChangedEventArgs e)
             {
                 ListViewItem item = category.SelectedItem as ListViewItem;
                 List<Food> lstSelectedFood = new List<Food>();

                 if (item.Content.ToString().Equals("전체"))
                 {
                     lstSelectedFood = App.foodData.listFood;
                 }
                 else
                 {
                     Category selectCategory = statCategoryConvertFromString(item.Content.ToString());
                     lstSelectedFood = App.foodData.listFood.Where(x => x.category == selectCategory).ToList();
                 }

            int TotalPrice = 0;
            int TotalCount = 0;

            for (int i = 0; i < lstSelectedFood.Count; i++)
            {
                TotalPrice += lstSelectedFood[i].Price * lstSelectedFood[i].Count;
                TotalCount += lstSelectedFood[i].Count;
            }

                 Stat.ItemsSource = lstSelectedFood;
            tbTotalPrice.Content = "매출액:" + TotalPrice.ToString() + "판매량:" + TotalCount.ToString();
              }

             private Category statCategoryConvertFromString(string strCategory)
             {
                 Category result = new Category();

                 switch (strCategory)
                 {
                     case "시그니처":
                         result = Category.SIGNATURE;
                         break;
                     case "영양":
                         result = Category.NUTRITION;
                         break;
                     case "보양":
                         result = Category.RECUPERATION;
                         break;
                     case "별미":
                         result = Category.DELICACY;
                         break;
                     case "전통":
                         result = Category.TRADITION;
                         break;
                 }

                 return result;
             }
         }
    }

