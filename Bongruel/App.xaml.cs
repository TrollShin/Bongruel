﻿using System;
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
        public static StatData statData = new StatData();
        public static Helper.BNetwork bNetwork = new Helper.BNetwork();

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //SplashScreen splash = new SplashScreen("SplashScreen.png");
            //splash.Show(true);
            //MainWindow main = new MainWindow();
            //splash.Close(new TimeSpan(0, 0, 1));
            //Task<MainWindow> windowTask = main.Show();
            //await windowTask;
        }

        //private async Task Application_Startup(object sender, StartupEventArgs e)
        // {


        //}
    }
}
