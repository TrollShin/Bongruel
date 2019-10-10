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
            orderedMenuList.Add(food);
            selectedFood.Items.Refresh();
        }

        /// <summary>
        /// 메뉴창 아래에있는 이미지를 입력받은 Img로 바꿔줌
        /// </summary>
        /// <param name="imgPath">Img Path</param>
        private void selectedMenuImgChange(string imgPath)
        {
            foodImage.Source = convertStringToImgSource(imgPath);
        }

        private ImageSource convertStringToImgSource(string imgPath)
        {
            ImageSource result = null;

            ImageSourceConverter converter = new ImageSourceConverter();
            result = converter.ConvertFromString(imgPath) as ImageSource;

            return result;
        }
    }
}
