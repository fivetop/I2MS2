using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace I2MS2.Windows
{
    /// <summary>
    /// Alert.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Alert : Window
    {
        public bool isAlive = false;
        public Alert()
        {
            isAlive = true;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            var t = new DispatcherTimer(TimeSpan.FromSeconds(10), DispatcherPriority.Normal, Tick1, this.Dispatcher);
            sp.Play();
        }

        private void Tick1(object sender, EventArgs e)
        {
            var t1 = (DispatcherTimer)sender;
            t1.Stop();
            sp.Stop();
            this.Close();
            isAlive = false;
        }

        private void _1_png_MouseDown(object sender, MouseButtonEventArgs e)
        {
            sp.Stop();
            this.Close();
            isAlive = false;
        }
    }
}
