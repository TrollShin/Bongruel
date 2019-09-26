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
        public TableControl()
        {
            InitializeComponent();
        }

        public void SetItem(String seatId)
        {
            tableId.Text = seatId;
        }
        public void SetItem(List<Food> orderedMenu)
        {
            string message = "";
            message = changeFoodListToString(orderedMenu);

            textOrderedMenu.Text = message;
        }
        public void SetItem(GruelModel.Seat seat)
        {
            string message = "";
            message = changeFoodListToString(seat.OrderList);

            textOrderedMenu.Text = message;
        }

        /// <summary>
        /// 입력받은 FoodList 를 테이블에 추가하기 위한 규격으로 바꿈 
        /// </summary>
        private string changeFoodListToString(List<Food> foods)
        {
            string result = "";

            foreach (Food item in foods)
            {
                if (foods.IndexOf(item) == 4)
                {
                    result += "... 및" + (foods.Count - 4).ToString() + "개\n";
                    break;
                }

                result += item.Name.ToString() + "\n";
            }

            return result;
        }
    }
}
