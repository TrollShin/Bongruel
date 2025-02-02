﻿using System;
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

            listseat = new List<Seat>();

            for(int i =1;i<=6;i++)
            {
                listseat.Add(new Seat() { Id = i.ToString() + "번", OrderList = new List<Food>() });
            }

            isLoaded = true;
        }
    }
}
