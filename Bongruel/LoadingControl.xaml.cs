using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace Bongruel
{
    /// <summary>
    /// LoadingControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoadingControl : UserControl, INotifyPropertyChanged
    {
        private BackgroundWorker bgWorker = new BackgroundWorker();

        private int workerState;

        public event PropertyChangedEventHandler PropertyChanged;

        public int WorkerState
        {
            get { return workerState; }
            set
            {
                workerState = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("WorkerState"));
            }
        }
        public LoadingControl()
        {
            InitializeComponent();

            DataContext = this;

            bgWorker.DoWork += (s, e) =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    System.Threading.Thread.Sleep(100);
                    WorkerState = i;
                }
                MessageBox.Show("로딩 완료!");
            };
            bgWorker.RunWorkerAsync();
        }
    }
}
