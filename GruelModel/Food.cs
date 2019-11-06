using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruelModel
{
    public class Food
    {
        public Food()
        {
        }

        public Food(Food food)
        {
            Name = food.Name;
            Price = food.Price;
            Count = food.Count;
            ImagePath = food.ImagePath;
            category = food.category;
        }

        public string Name { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public string ImagePath { get; set; }
        public Category category { get; set; }

        public int TotalPrice { get; set; }

        /*public int TotalCount { get; set; }*/

        public string Payment { get; set; }
    }
}
