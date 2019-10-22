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
    public List<Food> LstOrderedFood;
    public string TableId;
}

namespace Bongruel
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : UserControl
    {
        public delegate void GobackHandler(object sender, OrderEventArgs e);
        public event GobackHandler OnGoBackMainWindow;

         private List<Food> orderedMenuList;

        public MenuWindow()
        {
            InitializeComponent();
            this.Loaded += MenuWindow_Loaded;
        }

        public void setOrderMenu(string tableId, List<Food> orderedMenu)
        {
            this.tableId.Text = tableId;

            if(/*orderedMenu != null*/ orderedMenu.Count != 0)
            {
                orderedMenuList = orderedMenu;
                selectedFood.ItemsSource = orderedMenuList;
                selectedFood.Items.Refresh();

                List<Food> food = orderedMenuList;
            }
        }

        private void MenuWindow_Loaded(object sender, RoutedEventArgs e)
        {
            App.foodData.Load();

            init();
        }
        
        //초기화
        private void init()
        {
            orderedMenuList = new List<Food>();

            lvFood.ItemsSource = App.foodData.listFood;
            selectedFood.ItemsSource = orderedMenuList;
        }

        // 음식 선택 시 실행
        private void Menu_Select(object sender, MouseButtonEventArgs e)
        { 
            Food food = lvFood.SelectedItem as Food;

            selectedMenuImgChange(food.ImagePath);
            addOrderedMenu(food);

            selectedFood.Items.Refresh();

            ItemCollection test = selectedFood.Items;
        }

        // 선택한 음식을 메뉴에 추가시킴
        private void addOrderedMenu(Food food)
        {
            if(isAlreadySelect(food))
            {                
                orderedMenuList[orderedMenuList.IndexOf(food)].Count += 1;
                food.Price *= food.Count;
            }
            else
            {
                orderedMenuList.Add(food);
            }
        }

        // 선택한 메뉴가 이미 선택되어 있다면 true 아니면 false
        private bool isAlreadySelect(Food food)
        {
            if(orderedMenuList == null)
            {
                return false;
            }

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

        //결제버튼
        private void payMent_btn_Click(object sender, RoutedEventArgs e)
        {
            
        }
        
        //모든 메뉴를 제거함
        private void removeAll_btn_Click(object sender, RoutedEventArgs e)
        {
            orderedMenuList.Clear();
            selectedFood.Items.Refresh();
        }

        //선택한 메뉴를 제거함
        private void remove_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!isFoodCountCanChange())
            {
                return;
            }

            orderedMenuList.Remove(selectedFood.SelectedItem as Food);
            selectedFood.Items.Refresh();

            List<Food> test = App.foodData.listFood;

        }

        // 선택한 메뉴의 수량을 +1 시킴
        private void plus_btn_Click(object sender, RoutedEventArgs e)
        {            
            if (!isFoodCountCanChange())
            {
                return;
            }

            (selectedFood.SelectedItem as Food).Count += 1;

            selectedFood.Items.Refresh();
        }

        // 선택한 메뉴의 수량을 -1 시킴
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

        // 주문완료 버튼을 누를 시 실행 tableId 와 주문한 메뉴를 args로 만들고 메인화면으로 보냄
        private void ordered_btn_Click(object sender, RoutedEventArgs e)
        {
            OrderEventArgs args = new OrderEventArgs();

            args.LstOrderedFood = orderedMenuList.ToList();
            args.TableId = tableId.Text;

            orderedMenuList.Clear();

            OnGoBackMainWindow?.Invoke(this, args);
        }

        //돌아가기 버튼을 누를 시 실행 
        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            orderedMenuList.Clear();

            OnGoBackMainWindow?.Invoke(this, null);
        }

        // 메뉴의 수량 변경시 사용자가 메뉴를 선택했는지 확인 
        //선택하면 true 아니면 false
        private bool isFoodCountCanChange()
        {
            if ((selectedFood.SelectedItem as Food) == null)
            {
                return false;
            }

            return true;
        }

        // 메뉴의 Category가 바뀌면 실행
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
                Category selectCategory = (Category)Enum.Parse(typeof(Category), item.Tag.ToString());
                lstSelectedFood = App.foodData.listFood.Where(x => x.category == selectCategory).ToList();
            }

            lvFood.ItemsSource = lstSelectedFood;
        }      
    }
}
