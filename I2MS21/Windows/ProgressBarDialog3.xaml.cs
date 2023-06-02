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
    public partial class ProgressBarDialog3 : Window
    {
        private double _percentage1 = 0;
        private double _percentage2 = 0;
        private double _percentage3 = 0;
        private double _percentage4 = 0;

        public ProgressBarDialog3()
        {
            InitializeComponent();
            stateBar1.Width = 0;
            stateBar2.Width = 0;
            stateBar3.Width = 0;
            stateBar4.Width = 0;
        }

        public void set_progress_bar1(double percentage1)
        {
            if (percentage1 != _percentage1)
            {
                _percentage1 = percentage1;
                update1(percentage1);
                Refresh.Refresh_Controls(stateBar1);
            }
        }

        public void set_progress_bar2(double percentage2)
        {
            if (percentage2 != _percentage2)
            {
                _percentage2 = percentage2;
                update2(percentage2);
                Refresh.Refresh_Controls(stateBar2);
            }
        }

        public void set_progress_bar3(double percentage3)
        {
            if (percentage3 != _percentage3)
            {
                _percentage3 = percentage3;
                update3(percentage3);
                Refresh.Refresh_Controls(stateBar3);
            }
        }

        public void set_progress_bar4(double percentage4)
        {
            if (percentage4 != _percentage4)
            {
                _percentage4 = percentage4;
                update4(percentage4);
                Refresh.Refresh_Controls(stateBar4);
            }
        }

        private void update1(double percentage1)
        {
            double width_value1 = (percentage1 * 346) / 100;
            stateBar1.Width = width_value1;
        }

        private void update2(double percentage2)
        {
            double width_value2 = (percentage2 * 346) / 100;
            stateBar2.Width = width_value2;
        }

        private void update3(double percentage3)
        {
            double width_value3 = (percentage3 * 346) / 100;
            stateBar3.Width = width_value3;
        }

        private void update4(double percentage4)
        {
            double width_value4 = (percentage4 * 346) / 100;
            stateBar4.Width = width_value4;
        }
    }

   

}
