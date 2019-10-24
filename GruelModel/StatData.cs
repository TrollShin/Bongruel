using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruelModel
{
    public class StatData
    {
            
        private bool isPayed = false;

        public List<Food> PayedListFood;

        public void Pay()
        {
            if (isPayed) return;

            PayedListFood = new List<Food>()
            {
                new Food() {Name = "신불닭죽", TotalPrice = 0, category = Category.DELICACY},
                new Food() {Name = "둥지팥죽", TotalPrice = 0,  category = Category.TRADITION },
                new Food() {Name = "쇠고기미역죽", TotalPrice = 0, category = Category.NUTRITION },
                new Food() {Name = "삼계전복죽", TotalPrice = 0,  category = Category.RECUPERATION },
                new Food() {Name = "트러플전복죽", TotalPrice = 0, category = Category.SIGNATURE },
            };

            isPayed = true;
        }
    }
}

