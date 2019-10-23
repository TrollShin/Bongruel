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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TableControl : UserControl
    {
        public Seat seat = new Seat();

        public TableControl()
        {
            InitializeComponent();
        }

        public void SetItem(String seatId)
        {
            tableId.Text = seatId;
            seat.Id = seatId;
        }
        public void SetItem(List<Food> orderedMenu)
        {
            if (orderedMenu == null) return;

            string message = "";
            message = changeFoodListToString(orderedMenu);

            textOrderedMenu.Text = message;

            foreach (Food item in orderedMenu)
            {
                seat.OrderList.Add(item);
            }
            //seat.OrderList = orderedMenu;
        }
        public void SetItem(GruelModel.Seat seat)
        {
            string message = "";
            message = changeFoodListToString(seat.OrderList);

            tableId.Text = seat.Id;
            textOrderedMenu.Text = message;

            this.seat = seat;
        }

        /// <summary>
        /// 입력받은 FoodList 를 테이블에 추가하기 위한 규격으로 바꿈 
        /// </summary>
        private string changeFoodListToString(List<Food> foods)
        {
            string result = "";

            if(foods == null)
            {
                return result;
            }

            foreach (Food item in foods)
            {
                if (foods.IndexOf(item) == 3)
                {
                    result += "... 및" + (foods.Count - 3).ToString() + "개\n";
                    break;
                }

                result += item.Name.ToString() + "\n";
            }

            return result;
        }
    }
}
