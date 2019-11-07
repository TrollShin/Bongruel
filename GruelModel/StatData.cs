using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruelModel
{
    public class StatData
    {
            
        private bool isLoaded = false;

        public List<Stat> PayedListFood;

        public void Load()
        {
            if (isLoaded) return;

            PayedListFood = new List<Stat>()
            {
                new Stat() { FoodName = "신불닭죽", Price = 0, Count = 0, FoodCategory = Category.DELICACY},
                new Stat() { FoodName = "둥지팥죽", Price = 0, Count = 0, FoodCategory = Category.TRADITION},
                new Stat() { FoodName = "쇠고기미역죽", Price = 0, Count = 0, FoodCategory = Category.NUTRITION},
                new Stat() { FoodName = "삼계전복죽", Price = 0, Count = 0, FoodCategory = Category.RECUPERATION},
                new Stat() { FoodName = "트러플전복죽", Price = 0, Count = 0, FoodCategory = Category.SIGNATURE},
            };

            isLoaded = true;
        }
    }
}

