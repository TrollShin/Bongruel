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

        public List<Food> PayedListFood;

        public void Load()
        {
            if (isLoaded) return;

            PayedListFood = new List<Food>()
            {
                new Food() {Name = "낙지김치죽", Price = 0, ImagePath = "", category = Category.DELICACY, Count = 0},
                new Food() {Name = "녹두죽", Price = 0, ImagePath = "", category = Category.TRADITION, Count = 0},
                new Food() {Name = "단호박죽", Price = 0, ImagePath = "", category = Category.TRADITION, Count = 0},
                new Food() {Name = "동지팥죽", Price = 0, ImagePath = "", category = Category.TRADITION, Count = 0},
                new Food() {Name = "매생이굴죽", Price = 0, ImagePath = "", category = Category.DELICACY, Count = 0},
                new Food() {Name = "버섯굴죽", Price = 0, ImagePath = "", category = Category.NUTRITION, Count = 0},
                new Food() {Name = "불낙죽", Price = 0, ImagePath = "", category = Category.DELICACY, Count = 0},
                new Food() {Name = "삼계전복죽", Price = 0, ImagePath = "", category = Category.RECUPERATION, Count = 0},
                new Food() {Name = "삼계죽", Price = 0, ImagePath = "", category = Category.RECUPERATION, Count = 0},
                new Food() {Name = "새우죽", Price = 0, ImagePath = "", category = Category.NUTRITION, Count = 0},
                new Food() {Name = "쇠고기미역죽", Price = 0, ImagePath = "", category = Category.NUTRITION, Count = 0},
                new Food() {Name = "쇠고기버섯죽", Price = 0, ImagePath = "", category = Category.NUTRITION, Count = 0},
                new Food() {Name = "쇠고기야채죽", Price = 0, ImagePath = "", category = Category.NUTRITION, Count = 0},
                new Food() {Name = "신불닭죽", Price = 0, ImagePath = "", category = Category.DELICACY, Count = 0},
                new Food() {Name = "신짬뽕죽", Price = 0, ImagePath = "", category = Category.DELICACY, Count = 0},
                new Food() {Name = "야채죽", Price = 0, ImagePath = "", category = Category.NUTRITION, Count = 0},
                new Food() {Name = "자연송이쇠고기죽", Price = 0, ImagePath = "", category = Category.RECUPERATION, Count = 0},
                new Food() {Name = "잣죽", Price = 0, ImagePath = "", category = Category.TRADITION, Count = 0},
                new Food() {Name = "쇠고기육개장죽", Price = 0, ImagePath = "", category = Category.DELICACY, Count = 0},
                new Food() {Name = "참치야채죽", Price = 0, ImagePath = "", category = Category.NUTRITION, Count = 0},
                new Food() {Name = "트러플전복죽", Price = 0, ImagePath = "", category = Category.SIGNATURE, Count = 0},
                new Food() {Name = "해물죽", Price = 0, ImagePath = "", category = Category.NUTRITION, Count = 0},
                new Food() {Name = "홍게품은죽", Price = 0, ImagePath = "", category = Category.SIGNATURE, Count = 0},
                new Food() {Name = "흑임자죽", Price = 0, ImagePath = "", category = Category.TRADITION, Count = 0},
            };

            for (int i = 0; i < PayedListFood.Count; i++)
            {
                PayedListFood[i].ImagePath = "Assets/" + PayedListFood[i].Name + ".jpg";
            }

            isLoaded = true;
        }
    }
}

