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
using I2MS2.Library;

namespace I2MS2.Windows
{
    /// <summary>
    /// ProgressBarDialog.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ProgressBarDialog4
    {
        public ProgressBarDialog4()
        {
            InitializeComponent();
            stateBar.Width = 0;
        }
        
        public void setPercent(int p)
        {
            double width_value =0;
            if(p==100)
                width_value = 400;
            else
                width_value = (p * 346) / 100;
            stateBar.Width = width_value;
        }

        public void setStatus(string status)
        {
            _txtStatus.Text = status;
        }

        public void setStatus2(string status)
        {
            _txtStatus.Text = status;
            Refresh.Refresh_Controls(_txtStatus);
        }

        public void set_progress_bar2(double percentage)
        {
            update2(percentage);
            Refresh.Refresh_Controls(stateBar);
        }

        private void update2(double percentage)
        {
            double width_value2 = (percentage * 346) / 100;
            stateBar.Width = width_value2;
        }
    }
}
