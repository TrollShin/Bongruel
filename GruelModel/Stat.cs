using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruelModel
{
    public class Stat
    {
        public Stat()
        {
        }

        public Stat(Stat stat)
        {

        }

        public string FoodName { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public Category FoodCategory { get; set; } 
    }
}
