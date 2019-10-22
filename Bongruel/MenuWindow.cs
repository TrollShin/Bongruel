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
            orderedMenuList.Clear();

            this.tableId.Text = tableId;

            if(orderedMenu != null)
            {
                orderedMenuList = orderedMenu;
            }
        }

        private void MenuWindow_Loaded(object sender, RoutedEventArgs e)
        {
            orderedMenuList = new List<Food>();
            
            App.foodData.Load();
            lvFood.ItemsSource = App.foodData.listFood;

            selectedFood.ItemsSource = orderedMenuList;
        }
        
        // 음식 선택 시 실행
        private void Menu_Select(object sender, MouseButtonEventArgs e)
        { 
            Food food = lvFood.SelectedItem as Food;

            selectedMenuImgChange(food.ImagePath);
            addOrderedMenu(food);
        }

        // 선택한 음식을 메뉴에 추가시킴
        private void addOrderedMenu(Food food)
        {
            if(orderedMenuList != null && isAlreadySelect(food))
            {
                orderedMenuList[orderedMenuList.IndexOf(food)].Count += 1;
                selectedFood.Items.Refresh();
                return;   
            }
            List<Seat> item = App.seatData.listseat;
            orderedMenuList.Add(food);
            selectedFood.Items.Refresh();
        }

        // 선택한 메뉴가 이미 선택되어 있다면 true 아니면 false
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

        // 선택한 메뉴의 수량을 +1 시킴
        private void plus_btn_Click(object sender, RoutedEventArgs e)
        {
            
            if (!isFoodCountcanChange())
            {
                return;
            }

            (selectedFood.SelectedItem as Food).Count += 1;

            /* 
             * plus 를 multiSelete 방식으로 수정시 사용
             * for(int i = 0; i < selectedFood.SelectedItems.Count; i++)
             * {
             * (selectedFood.SelectedItems[i] as Food).Count += 1;
             * }
             */

            selectedFood.Items.Refresh();
        }

        // 선택한 메뉴의 수량을 -1 시킴
        private void minus_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!isFoodCountcanChange())
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
            args.LstOrderedFood = orderedMenuList;
            args.TableId = tableId.Text;

            OnGoBackMainWindow?.Invoke(this, args);
        }

        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (OnGoBackMainWindow != null)
            {
                OnGoBackMainWindow(this, null);
            }
        }

        // 메뉴의 수량 변경시 사용자가 메뉴를 선택했는지 확인 
        //선택하면 true 아니면 false
        private bool isFoodCountcanChange()
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
                Category selectCategory = foodCategoryConvertFromString(item.Content.ToString());
                lstSelectedFood = App.foodData.listFood.Where(x => x.category == selectCategory).ToList();
            }

            lvFood.ItemsSource = lstSelectedFood;
        }

        private Category foodCategoryConvertFromString(string strCategory) 
        {
            Category result = new Category();

            switch(strCategory)
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
