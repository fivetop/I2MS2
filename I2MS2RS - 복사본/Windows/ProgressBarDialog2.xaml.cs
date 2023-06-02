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
using I2MS2.Library;

using WebApiClient;

namespace I2MS2.Windows
{
    /// <summary>
    /// ProgressBarDialog.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ProgressBarDialog2 : Window
    {
        DispatcherTimer timer = new DispatcherTimer();

        private double _percentage = 0;
        private double _percentage2 = 0;

        public ProgressBarDialog2()
        {
            InitializeComponent();
            stateBar.Width = 0;
            stateBar2.Width = 0;
        }

        public void set_progress_bar1(double percentage)
        {
            if (percentage != _percentage)
            {
                _percentage = percentage;
                update(percentage);
                Refresh.Refresh_Controls(stateBar);
            }
        }

        public void set_progress_bar2(double percentage)
        {
            if (percentage != _percentage2)
            {
                _percentage2 = percentage;
                update2(percentage);
                Refresh.Refresh_Controls(stateBar2);
            }
        }

        private void update(double percentage)
        {
            double width_value = (percentage * 346) / 100;
            stateBar.Width = width_value;
        }

        private void update2(double percentage)
        {
            double width_value2 = (percentage * 346) / 100;
            stateBar2.Width = width_value2;
        }
    }

   

}
