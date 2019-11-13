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
/*                new Stat() { FoodName = "신불닭죽", Price = 0, Count = 0, FoodCategory = Category.DELICACY},
                new Stat() { FoodName = "둥지팥죽", Price = 0, Count = 0, FoodCategory = Category.TRADITION},
                new Stat() { FoodName = "쇠고기미역죽", Price = 0, Count = 0, FoodCategory = Category.NUTRITION},
                new Stat() { FoodName = "삼계전복죽", Price = 0, Count = 0, FoodCategory = Category.RECUPERATION},
                new Stat() { FoodName = "트러플전복죽", Price = 0, Count = 0, FoodCategory = Category.SIGNATURE},*/
                new Stat() {FoodName = "낙지김치죽", Price = 0, FoodCategory = Category.DELICACY, Count = 0},
                new Stat() {FoodName = "녹두죽", Price = 0, FoodCategory = Category.TRADITION, Count = 0},
                new Stat() {FoodName = "단호박죽", Price = 0, FoodCategory = Category.TRADITION, Count = 0},
                new Stat() {FoodName = "동지팥죽", Price = 0, FoodCategory = Category.TRADITION, Count = 0},
                new Stat() {FoodName = "매생이굴죽", Price = 0, FoodCategory = Category.DELICACY, Count = 0 },
                new Stat() {FoodName = "버섯굴죽", Price = 0, FoodCategory = Category.NUTRITION, Count = 0},
                new Stat() {FoodName = "불낙죽", Price = 0, FoodCategory = Category.DELICACY, Count = 0},
                new Stat() {FoodName = "삼계전복죽", Price = 0, FoodCategory = Category.RECUPERATION, Count = 0},
                new Stat() {FoodName = "삼계죽", Price = 0, FoodCategory = Category.RECUPERATION, Count = 0},
                new Stat() {FoodName = "새우죽", Price = 0, FoodCategory = Category.NUTRITION, Count = 0},
                new Stat() {FoodName = "쇠고기미역죽", Price = 0, FoodCategory = Category.NUTRITION, Count = 0},
                new Stat() {FoodName = "쇠고기버섯죽", Price = 0, FoodCategory = Category.NUTRITION, Count = 0},
                new Stat() {FoodName = "쇠고기야채죽", Price = 0, FoodCategory = Category.NUTRITION, Count = 0},
                new Stat() {FoodName = "신불닭죽", Price = 0, FoodCategory = Category.DELICACY, Count = 0},
                new Stat() {FoodName = "신짬뽕죽", Price = 0, FoodCategory = Category.DELICACY, Count = 0},
                new Stat() {FoodName = "야채죽", Price = 0, FoodCategory = Category.NUTRITION, Count = 0},
                new Stat() {FoodName = "자연송이쇠고기죽", Price = 0, FoodCategory = Category.RECUPERATION, Count = 0},
                new Stat() {FoodName = "잣죽", Price = 0, FoodCategory = Category.TRADITION, Count = 0},
                new Stat() {FoodName = "쇠고기육개장죽", Price = 0, FoodCategory = Category.DELICACY, Count = 0},
                new Stat() {FoodName = "참치야채죽", Price = 0, FoodCategory = Category.NUTRITION, Count = 0},
                new Stat() {FoodName = "트러플전복죽", Price = 0, FoodCategory = Category.SIGNATURE, Count = 0},
                new Stat() {FoodName = "해물죽", Price = 0, FoodCategory = Category.NUTRITION, Count = 0},
                new Stat() {FoodName = "홍게품은죽", Price = 0, FoodCategory = Category.SIGNATURE, Count = 0},
                new Stat() {FoodName = "흑임자죽", Price = 0, FoodCategory = Category.TRADITION, Count = 0},
            };

            isLoaded = true;
        }
    }
}

