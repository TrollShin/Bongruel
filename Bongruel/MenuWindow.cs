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
    public Seat seat;
    public bool isPayment;
    public int totalPrice;
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
        }

        public void setOrderMenu(Seat seat)
        {
            this.tableId.Text = seat.Id;
            this.lastOrderedTime.Text = seat.orderTime;

            //seat.orderList에 food 가 1개이상 들어있다면 orderedMenuList에 추가시킴
            if (seat.OrderList.Count != 0)
            {
                orderedMenuList.AddRange(seat.OrderList);// = new List<Food>(seat.OrderList);
                //selectedFood.ItemsSource = orderedMenuList;

                selectedFood.Items.Refresh();
            }
        }
        
        //초기화
        public void init()
        {
            if(isLoaded)
            {
                return;
            }

            //현금이 기본
            paymentType = PaymentType.CASH;
            CASH.IsChecked = true;

            orderedMenuList = new List<Food>();
            initLvFood(new List<Food>(App.foodData.listFood));
            selectedFood.ItemsSource = orderedMenuList;

            isLoaded = true;
        }

        //메뉴 초기화
        private void initLvFood(List<Food> lstFood)
        {
            foreach (Food item in lstFood)
            {
                Food food = new Food(item);

                lvFood.Items.Add(food);
            }
        }

        // 음식 선택 시 실행
        private void Menu_Select(object sender, MouseButtonEventArgs e)
        { 
            Food food = new Food(lvFood.SelectedItem as Food);

            selectedMenuImgChange(food.ImagePath);
            addOrderedMenu(food);

            selectedFood.Items.Refresh();
        }

        // 선택한 음식을 메뉴에 추가시킴
        private void addOrderedMenu(Food food)
        {
            if (isAlreadySelect(food))
            {
                Food selecteFoodItem = new Food();

                selecteFoodItem = orderedMenuList.Find(x => x.Name == food.Name);
                selecteFoodItem.Count += 1;
                selecteFoodItem.Price += getFoodPrice(selecteFoodItem);
            }
            else
            {
                orderedMenuList.Add(food);
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

            args = getOrederEventArgs(true);

            orderedMenuList.Clear();

            //아직은 현금과 카드에서 다른점이 없다
            if(paymentType.ToString() == "CASH")
            {
                OnGoBackMainWindow?.Invoke(this, args);
            }
            else if(paymentType.ToString() == "CREDIT")
            {
                OnGoBackMainWindow?.Invoke(this, args);
            }
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
            if (!isFoodSelected())
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
            if (!isFoodSelected())
            {
                return;
            }
            int price = getFoodPrice(selectedFood.SelectedItem as Food);

            changeFoodCount(1, price);
            refrashTotalPrice();
        }      

        // 선택한 메뉴의 수량을 -1 시킴
        private void minus_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!isFoodSelected())
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
                changeFoodCount(-1, -getFoodPrice(selecteFoodItem));
            }

            refrashTotalPrice();
            selectedFood.Items.Refresh();
        }

        // 주문완료 버튼을 누를 시 실행 tableId 와 주문한 메뉴를 args로 만들고 메인화면으로 보냄
        private void ordered_btn_Click(object sender, RoutedEventArgs e)
        {
            OrderEventArgs args = new OrderEventArgs();

            args = getOrederEventArgs(false);

            orderedMenuList.Clear();
            selectedFood.Items.Refresh();

            OnGoBackMainWindow?.Invoke(this, args);
        }

        private OrderEventArgs getOrederEventArgs(bool isPayment)
        {
            OrderEventArgs result = new OrderEventArgs();

            result.seat = new Seat(getSeat());
            result.isPayment = isPayment;
            result.totalPrice = getTotalPrice();

            return result;
        }

        //돌아가기 버튼을 누를 시 실행 
        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            orderedMenuList.Clear();

            OnGoBackMainWindow?.Invoke(this, null);
        }

        //결제수단 선택
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
        private bool isFoodSelected()
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
    
        //parameter food 의 가격을 리턴함
        private int getFoodPrice(Food food)
        {
            return (App.foodData.listFood.Where(x => x.Name == food.Name).ToList())[0].Price;
        }

        private void changeFoodCount(int count, int price)
        {
            Food selecteFoodItem = selectedFood.SelectedItem as Food;
            selecteFoodItem.Count += count;
            selecteFoodItem.Price += price;

            selectedFood.Items.Refresh();
        }

        //총 각격을 출력해줌
        private void refrashTotalPrice()
        {
            totalPrice.Text = getTotalPrice().ToString();
        }

        //총 가격을 리턴해줌
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
