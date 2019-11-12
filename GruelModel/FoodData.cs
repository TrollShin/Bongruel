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
                new Food() {Name = "낙지김치죽", Price = 9500, ImagePath = "", category = Category.DELICACY, Count = 1},
                new Food() {Name = "녹두죽", Price = 8500, ImagePath = "", category = Category.TRADITION, Count = 1},
                new Food() {Name = "단호박죽", Price = 9000, ImagePath = "", category = Category.TRADITION, Count = 1},
                new Food() {Name = "동지팥죽", Price = 9000, ImagePath = "", category = Category.TRADITION, Count = 1},
                new Food() {Name = "매생이굴죽", Price = 10000, ImagePath = "", category = Category.DELICACY, Count = 1},
                new Food() {Name = "버섯굴죽", Price = 9000, ImagePath = "", category = Category.NUTRITION, Count = 1},
                new Food() {Name = "불낙죽", Price = 11000, ImagePath = "", category = Category.DELICACY, Count = 1},
                new Food() {Name = "삼계전복죽", Price = 15000, ImagePath = "", category = Category.RECUPERATION, Count = 1},
                new Food() {Name = "삼계죽", Price = 11000, ImagePath = "", category = Category.RECUPERATION, Count = 1},
                new Food() {Name = "새우죽", Price = 9000, ImagePath = "", category = Category.NUTRITION, Count = 1},
                new Food() {Name = "쇠고기미역죽", Price = 8500, ImagePath = "", category = Category.NUTRITION, Count = 1},
                new Food() {Name = "쇠고기버섯죽", Price = 9000, ImagePath = "", category = Category.NUTRITION, Count = 1},
                new Food() {Name = "쇠고기야채죽", Price = 9000, ImagePath = "", category = Category.NUTRITION, Count = 1},
                new Food() {Name = "신불닭죽", Price = 10000, ImagePath = "", category = Category.DELICACY, Count = 1},
                new Food() {Name = "신짬뽕죽", Price = 10000, ImagePath = "", category = Category.DELICACY, Count = 1},
                new Food() {Name = "야채죽", Price = 8000, ImagePath = "", category = Category.NUTRITION, Count = 1},
                new Food() {Name = "자연송이쇠고기죽", Price = 13000, ImagePath = "", category = Category.RECUPERATION, Count = 1},
                new Food() {Name = "잣죽", Price = 9500, ImagePath = "", category = Category.TRADITION, Count = 1},
                new Food() {Name = "진품쇠고기육개장죽", Price = 10000, ImagePath = "", category = Category.DELICACY, Count = 1},
                new Food() {Name = "참치야채죽", Price = 8500, ImagePath = "", category = Category.NUTRITION, Count = 1},
                new Food() {Name = "트러플전복죽", Price = 16000, ImagePath = "", category = Category.SIGNATURE, Count = 1},
                new Food() {Name = "해물죽", Price = 10000, ImagePath = "", category = Category.NUTRITION, Count = 1},
                new Food() {Name = "홍게품은죽", Price = 13000, ImagePath = "", category = Category.SIGNATURE, Count = 1},
                new Food() {Name = "흑임자죽", Price = 8000, ImagePath = "", category = Category.TRADITION, Count = 1},
            };

            for(int i = 0; i < listFood.Count; i++)
            {
                listFood[i].ImagePath = "Assets/" + listFood[i].Name + ".jpg";
            }

            isLoaded = true;
        }
    }
}