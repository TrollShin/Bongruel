using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruelModel
{
    public class Seat
    {
        public Seat()
        {
        }

        public Seat(Seat seat)
        {
            Id = seat.Id;
            OrderList = seat.OrderList;
            orderTime = seat.orderTime;
        }

        public string Id { get; set; }

        public List<Food> OrderList { get; set; }

        public string orderTime { get; set; }
    }
}
