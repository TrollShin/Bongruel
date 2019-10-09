using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruelModel
{
    public class SeatData
    {
        private bool isLoaded = false;

        public List<Seat> listseat;

        public void Load()
        {
            if (isLoaded) return;

            listseat = new List<Seat>()
            {
                new Seat() {Id = "1", OrderList = null},
                new Seat() {Id = "2", OrderList = null },
                new Seat() {Id = "3", OrderList = null },
                new Seat() {Id = "4", OrderList = null },
                new Seat() {Id = "5", OrderList = null },
                new Seat() {Id = "6", OrderList = null },
            };

            isLoaded = true;
        }
    }
}
