using GruelModel;
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
public class OrderEventArgs : EventArgs
{

}

namespace Bongruel
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : UserControl
    {
        public delegate void OrderHandler(object sender);
        public event OrderHandler OnGoBackMainWindow;

        private List<Food> orderedMenuList;

        public MenuWindow()
        {
            InitializeComponent();
            this.Loaded += MenuWindow_Loaded;
        }

        private void MenuWindow_Loaded(object sender, RoutedEventArgs e)
        {
            orderedMenuList = new List<Food>();

            App.foodData.Load();
            lvFood.ItemsSource = App.foodData.listFood;

            selectedFood.ItemsSource = orderedMenuList;
        }
        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            if(OnGoBackMainWindow != null)
            {
                OnGoBackMainWindow(this);
            }
        }

        private void Menu_Select(object sender, MouseButtonEventArgs e)
        { 
            Food food = lvFood.SelectedItem as Food;

            selectedMenuImgChange(food.ImagePath);
            addOrderedMenu(food);
        }

        private void addOrderedMenu(Food food)
        {
            if(isAlreadySelect(food))
            {
                orderedMenuList[orderedMenuList.IndexOf(food)].Count += 1;
                selectedFood.Items.Refresh();
                return;   
            }

            orderedMenuList.Add(food);
            selectedFood.Items.Refresh();
        }

        private bool isAlreadySelect(Food food)
        {
            foreach(Food item in orderedMenuList)
            {
                if(food == item)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 메뉴창 아래에있는 이미지를 입력받은 Img로 바꿔줌
        /// </summary>
        /// <param name="imgPath">Img Path</param>
        private void selectedMenuImgChange(string imgPath)
        {
            foodImage.Source = new BitmapImage(new Uri(imgPath, UriKind.Relative));
        }

        private void plus_btn_Click(object sender, RoutedEventArgs e)
        {
            
            if (!isFoodCountCanChange())
            {
                return;
            }

            (selectedFood.SelectedItem as Food).Count += 1;

            /* plus 를 multiSelete 방식으로 수정시 사용
            for(int i = 0; i < selectedFood.SelectedItems.Count; i++)
            {
                (selectedFood.SelectedItems[i] as Food).Count += 1;
            }
            */

            selectedFood.Items.Refresh();



        }

        private void minus_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!isFoodCountCanChange())
            {
                return;
            }

            if ((selectedFood.SelectedItem as Food).Count == 1)
            {
                orderedMenuList.Remove(selectedFood.SelectedItem as Food);
            }
            else
            {
                (selectedFood.SelectedItem as Food).Count -= 1;
            }

            selectedFood.Items.Refresh();
        }

        private bool isFoodCountCanChange()
        {
            if((selectedFood.SelectedItem as Food) == null)
            {
                return false;
            }

            return true;
        }

        private Food returnFood(object item)
        {
            Food result = new Food();

            result.category = (item as Food).category;
            result.Count = (item as Food).Count;
            result.ImagePath = (item as Food).ImagePath;
            result.Name = (item as Food).Name;
            result.Price = (item as Food).Price;

            return result;
        }
    }
}
