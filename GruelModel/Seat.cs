using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruelModel
{
    public class Seat
    {
        public string Id { get; set; }

        public List<Food> OrderList { get; set; }
    }
}
