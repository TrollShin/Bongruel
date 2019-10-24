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
    /*public List<Food> LstOrderedFood;
    public string TableId;
    */
    public Seat seat;
    public PaymentType paymentType; 
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

        private bool isLoaded = false;

        private PaymentType paymentType;

        public MenuWindow()
        {
            InitializeComponent();
            this.Loaded += MenuWindow_Loaded;
        }

        public void setOrderMenu(Seat seat)
        {
            this.tableId.Text = seat.Id;
            this.lastOrderedTime.Text = seat.orderTime;
            List<Food> lstFood = new List<Food>(seat.OrderList);

            if (seat.OrderList.Count != 0)
            {
                orderedMenuList = lstFood;
                selectedFood.ItemsSource = orderedMenuList;
                selectedFood.Items.Refresh();
            }
        }

        private void MenuWindow_Loaded(object sender, RoutedEventArgs e)
        {
            init();
        }
        
        //초기화
        private void init()
        {
            if(isLoaded)
            {
                return;
            }

            paymentType = PaymentType.CASH;
            cashRadioButton.IsChecked = true;

            orderedMenuList = new List<Food>();
            initLvFood(new List<Food>(App.foodData.listFood));
            selectedFood.ItemsSource = orderedMenuList;

            isLoaded = true;
        }

        private void initLvFood(List<Food> lstFood)
        {
            foreach (Food item in lstFood)
            {
                Food food = new Food();

                food = returnFood(item);

                lvFood.Items.Add(food);
            }
        }

        // 음식 선택 시 실행
        private void Menu_Select(object sender, MouseButtonEventArgs e)
        { 
            Food food = new Food();
            food = lvFood.SelectedItem as Food;

            selectedMenuImgChange(food.ImagePath);
            addOrderedMenu(food);

            selectedFood.Items.Refresh();

            ItemCollection test = selectedFood.Items;
        }

        // 선택한 음식을 메뉴에 추가시킴
        private void addOrderedMenu(Food food)
        {
            Food item = new Food();
            item = returnFood(food);

            if (isAlreadySelect(item))
            {
                Food selecteFoodItem = new Food();

                selecteFoodItem = orderedMenuList.Find(x => x.Name == item.Name);
                selecteFoodItem.Count += 1;
                selecteFoodItem.Price += getFoodPrice(selecteFoodItem);
            }
            else
            {
                orderedMenuList.Add(item);
            }

            refrashTotalPrice();
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
                if(food.Name == item.Name)
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
            OrderEventArgs args = new OrderEventArgs();
            args.paymentType = this.paymentType;
            
            Seat item = new Seat();

            item = getSeat();

            args.seat = new Seat();
            args.seat = item;

            orderedMenuList.Clear();

            OnGoBackMainWindow?.Invoke(this, args);
        }
        
        //모든 메뉴를 제거함
        private void removeAll_btn_Click(object sender, RoutedEventArgs e)
        {
            orderedMenuList.Clear();
            refrashTotalPrice();
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
            refrashTotalPrice();
            selectedFood.Items.Refresh();
        }

        // 선택한 메뉴의 수량을 +1 시킴
        private void plus_btn_Click(object sender, RoutedEventArgs e)
        {            
            if (!isFoodCountCanChange())
            {
                return;
            }
            int price = getFoodPrice(selectedFood.SelectedItem as Food);

            foodCountChange(1, price);
            refrashTotalPrice();
        }      

        // 선택한 메뉴의 수량을 -1 시킴
        private void minus_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!isFoodCountCanChange())
            {
                return;
            }

            Food selecteFoodItem = selectedFood.SelectedItem as Food;

            if (selecteFoodItem.Count == 1)
            {
                orderedMenuList.Remove(selecteFoodItem);
            }
            else
            {
                foodCountChange(-1, -getFoodPrice(selecteFoodItem));
            }

            refrashTotalPrice();
            selectedFood.Items.Refresh();
        }

        // 주문완료 버튼을 누를 시 실행 tableId 와 주문한 메뉴를 args로 만들고 메인화면으로 보냄
        private void ordered_btn_Click(object sender, RoutedEventArgs e)
        {
            OrderEventArgs args = new OrderEventArgs();
            Seat item = new Seat();

            item = getSeat();

            args.seat = new Seat();
            args.seat = item;

            orderedMenuList.Clear();

            OnGoBackMainWindow?.Invoke(this, args);
        }

        //돌아가기 버튼을 누를 시 실행 
        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            orderedMenuList.Clear();

            OnGoBackMainWindow?.Invoke(this, null);
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton paymentBtn = sender as RadioButton;

            if((PaymentType)Enum.Parse(typeof(PaymentType), paymentBtn.Name) == PaymentType.CASH)
            {
                paymentType = PaymentType.CASH;
            }
            else
            {
                paymentType = PaymentType.CREDIT;
            }
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
            lvFood.Items.Clear();

            ListViewItem item = category.SelectedItem as ListViewItem;
            List<Food> lstSelectedFood = new List<Food>();

            if (item.Content.ToString().Equals("전체"))
            {
                lstSelectedFood = new List<Food>(App.foodData.listFood);
            }
            else
            {
                Category selectCategory = (Category)Enum.Parse(typeof(Category), item.Tag.ToString());
                lstSelectedFood = new List<Food>(App.foodData.listFood).Where(x => x.category == selectCategory).ToList();
            }

            initLvFood(lstSelectedFood);
        }

        //단순히 대입해서 넣으면 연결되어 버리기 때문에 이를 방지하기 위해서 만들었다
        private Food returnFood(Food food)
        {
            Food item = new Food();

            item.category = food.category;
            item.Count = food.Count;
            item.ImagePath = food.ImagePath;
            item.Name = food.Name;
            item.Price = food.Price;

            return item;
        }
    
        private int getFoodPrice(Food food)
        {
            return (App.foodData.listFood.Where(x => x.Name == food.Name).ToList())[0].Price;
        }

        private void foodCountChange(int count, int price)
        {
            Food selecteFoodItem = selectedFood.SelectedItem as Food;
            selecteFoodItem.Count += count;
            selecteFoodItem.Price += price;

            selectedFood.Items.Refresh();
        }

        private void refrashTotalPrice()
        {
            totalPrice.Text = "가격 " + getTotalPrice().ToString();
        }

        private int getTotalPrice()
        {
            int result = 0; 

            foreach(Food item in orderedMenuList)
            {
                result += item.Price;
            }

            return result;
        }

        private Seat getSeat()
        {
            Seat result = new Seat();

            result.Id = tableId.Text;
            result.OrderList = new List<Food>(orderedMenuList);
            result.orderTime = DateTime.Now.ToString();

            return result;
        }
    }
}
