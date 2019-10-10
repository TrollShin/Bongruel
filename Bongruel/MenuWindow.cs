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

namespace Bongruel
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : UserControl
    {
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
            this.Visibility = Visibility.Collapsed;
        }

        private void Menu_Select(object sender, MouseButtonEventArgs e)
        {
            Food food = lvFood.SelectedItem as Food;

            selectedMenuImgChange(food.ImagePath);
            //ImageSource foodImgSource = (sender as Image).Source;

            //selectedMenuImgChange(foodImgSource);
            //addOrderedMenu(foodImgSource);
        }

        private void addOrderedMenu()
        {
            //Food food = App.foodData.listFood.Find(match => match.ImagePath == imgSource.ToString());

            //orderedMenuList.Add(food);
            //selectedFood.Items.Refresh();
        }

        /// <summary>
        /// 메뉴창 아래에있는 이미지를 입력받은 Img로 바꿔줌
        /// </summary>
        /// <param name="imgPath">Img Path</param>
        private void selectedMenuImgChange(string imgPath)
        {
            //foodImage.Source = imgPath;
        }

        private string convertStringToImgSource(string imgPath)
        {
            string result = "";



            return result;
        }
    }
}
