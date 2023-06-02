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
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;
using I2MS2.Windows;
using I2MS2.Models;

using WebApiClient;

namespace I2MS2.Windows
{
    /// <summary>
    /// ProgressBarDialog.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ProgressBarDialog : Window
    {
        private double width_value =0;
        DispatcherTimer timer;

        public ProgressBarDialog()
        {
            InitializeComponent();
            stateBar.Width = 0;

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Tick += new EventHandler(setStateBarWidth_tick);
            timer.Start();
        }
        
        private void setStateBarWidth_tick(object sender, EventArgs e)
        {
            int p = g.webapi.percentage;
            if (p == 100)
                width_value = 400;
            else
                width_value = (p * 346) / 100;
            
            if (width_value != 400)
            {
                if (stateBar.Width != width_value)
                    stateBar.Width = width_value;
            }
            else
            {
                timer.Stop();
                Close();
            }
        }
       
        public void setPercent(int p)
        {
            if(p==100)
                width_value = 400;
            else
                width_value = (p * 346) / 100;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            g.webapi.cancel();
        }
    }
}
