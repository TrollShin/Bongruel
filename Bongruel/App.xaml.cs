using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GruelModel;

namespace Bongruel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        public static FoodData foodData = new FoodData();
        public static SeatData seatData = new SeatData();
    }
}
