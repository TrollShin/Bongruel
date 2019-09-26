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
                new Seat() {Id = "1" },
                new Seat() {Id = "2" },
                new Seat() {Id = "3" },
                new Seat() {Id = "4" },
                new Seat() {Id = "5" },
                new Seat() {Id = "6" },
            };

            isLoaded = true;
        }
    }
}
