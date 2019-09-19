using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruelModel
{
    public class FoodData
    {
        private bool isLoaded = false;

        public List<Food> listFood;

        public void Load()
        {
            if (isLoaded) return;

            listFood = new List<Food>()
            {
                //메뉴의 카테고리 (0.시그니처 1.보양 2.영양 3.별미 4.전통 5.반찬)
                new Food() {Name = "신불닭죽", Price = 10000, ImagePath = "Assets/신불닭죽_pic.jpg", category = Category.DELICACY},
                new Food() {Name = "둥지팥죽", Price = 9000, ImagePath = "Assets/둥지팥죽_pic.jpg", category = Category.TRADITION},
                new Food() {Name = "든든한끼장조림", Price = 2900, ImagePath = "Assets/든든한끼장조림_pic.jpg", category = Category.SIDEDISH},
                new Food() {Name = "쇠고기미역죽", Price = 8500, ImagePath = "Assets/쇠고기미역죽_pic.jpg", category = Category.NUTRITION},
                new Food() {Name = "삼계전복죽", Price = 15000, ImagePath = "Assets/삼계전복죽_pic.jpg", category = Category.RECUPERATION},
                new Food() {Name = "트러플전복죽", Price = 16000, ImagePath = "Assets/트러플전복죽_pic.jpg", category = Category.SIGNATURE},
            };

            isLoaded = true;
        }
    }
}
