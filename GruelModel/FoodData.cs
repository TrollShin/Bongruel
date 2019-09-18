using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruelModel
{
    class FoodData
    {
        private bool isLoaded = false;

        public List<Food> listFood;

        public void Load()
        {
            if (isLoaded) return;

            listFood = new List<Food>()
            {
                new Food() {Name = "신불닭죽", Price = 10000, ImagePath = "Assets/신불닭죽_pic.jpg", category = SIGNATURE},
            };

            isLoaded = true;
        }
    }
}
