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
        //임시
        private List<Food> orderedMenuList = new List<Food>();

        public MenuWindow()
        {
            InitializeComponent();
            this.Loaded += MenuWindow_Loaded;
        }

        private void MenuWindow_Loaded(object sender, RoutedEventArgs e)
        {
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
            ImageSource foodImgSource = (sender as Image).Source;

            selectedMenuImgChange(foodImgSource);
            addOrderedMenu(foodImgSource);
        }

        private void addOrderedMenu(ImageSource imgSource)
        {
            Food food = App.foodData.listFood.Find(match => match.ImagePath == imgSource.ToString());

            orderedMenuList.Add(food);
            selectedFood.Items.Refresh();
        }

        /// <summary>
        /// 메뉴창 아래에있는 이미지를 입력받은 Img로 바꿔줌
        /// </summary>
        /// <param name="imgPath">Img Path</param>
        private void selectedMenuImgChange(ImageSource imgPath)
        {
            foodImage.Source = imgPath;
        }

        private void SelectedFood_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
