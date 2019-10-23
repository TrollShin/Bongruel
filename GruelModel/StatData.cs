using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruelModel
{
    class StatData
    {
            
        private bool isPayed = false;

        public List<Food> StatlistFood;

        public void Pay()
        {
            if (isPayed) return;

            StatlistFood = new List<Food>()
            {
                new Food() {Name = "신불닭죽", TotalPrice = 10000, category = Category.DELICACY, TotalCount = 1},
                new Food() {Name = "둥지팥죽", TotalPrice = 9000,  category = Category.TRADITION, TotalCount = 1},
                new Food() {Name = "쇠고기미역죽", TotalPrice = 8500, category = Category.NUTRITION, TotalCount = 1},
                new Food() {Name = "삼계전복죽", TotalPrice = 15000,  category = Category.RECUPERATION, TotalCount = 1},
                new Food() {Name = "트러플전복죽", TotalPrice = 16000, category = Category.SIGNATURE, TotalCount = 1},
            };

            isPayed = true;
        }
    }
}

