using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruelModel
{
    public class Food
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public string ImagePath { get; set; }
        public Category category { get; set; }
    }
}
