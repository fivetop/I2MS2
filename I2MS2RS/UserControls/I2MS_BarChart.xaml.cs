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
using System.Windows.Threading;

namespace I2MS2.UserControls
{
    /// <summary>
    /// I2MS_BarChart.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class I2MS_BarChart : UserControl
    {
        Double data;
        Double tot;

        Double timer_interval = 10;
        int timer_cnt;
        int timer_end;

        Double rect_width;
        Double rect_width_interval;
        Double rect_width_end;

        Double rect_rate;
        Double rect_rate_interval;
        Double rect_rate_end;

        Double rect_data;
        Double rect_data_interval;
        Double rect_data_end;

        DispatcherTimer timer;

        public I2MS_BarChart()
        {
            InitializeComponent();
        }

        public void showBarChart(string name, int _data, int _tot, Brush brush)
        {
            data = _data;
            tot = _tot;

            _txtTitle.Text = name;
            _rectTitle.Fill = brush;
            _txtTot.Text = string.Format("{0}", tot);
            _rectRateBar.Fill = brush;
            _rectTotBar.Fill = brush;

            if (tot == 0)
            {
                _txtTot.Text = string.Format("0");
            }
            if (data == 0)
            {
                _txtRate.Text = string.Format("0%");
                setBarChart();
                _txtData.Text = string.Format("0"); 
            }
            else
            {
                setBarChart_withAnimation();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if(timer_cnt<timer_end)
            {
                timer_cnt++;
                rect_width += rect_width_interval;
                rect_rate += rect_rate_interval;
                rect_data += rect_data_interval;
                _rectRateBar.Width = rect_width;
                disp_value();
                if (rect_rate == 0 || Double.IsNaN(rect_rate))
                {
                    _txtRate.Text = string.Format("0%");
                }
                else
                {
                    _txtRate.Text = string.Format("{0}%", rect_rate.ToString("f1"));
                }
            }
            else
            {
                timer.Stop();
            }
        }

        private void setBarChart_withAnimation()
        {
            timer_cnt = 0;
            timer_end = (int)timer_interval;
            rect_width = 0;
            if (data == 0 || tot == 0) return;
            rect_width_end = (data / tot) * _gridTotBar.ActualWidth;
            rect_width_interval = rect_width_end / timer_interval;

            rect_rate = 0;
            rect_rate_end = ((Double)data / (Double)tot) * 100; ;
            rect_rate_interval = rect_rate_end / timer_interval;

            rect_data = 0;
            rect_data_end = data;
            rect_data_interval = rect_data_end / timer_interval;

            int milsec = 300 / (int)timer_interval;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, milsec);
            timer.Tick +=  new EventHandler(timer_Tick);
            timer.Start();
        }

        public void setBarChart()
        {
            _rectRateBar.Width = ( data / tot) * _gridTotBar.ActualWidth;
            disp_value();
        }

        Double txtData_df_margin_left = 5;
        private void disp_value()
        {
            _txtData.Text = string.Format("{0}", rect_data.ToString("f0"));
            Thickness margin = new Thickness();
            margin.Top = 0;
            margin.Bottom = 0;
            margin.Right = 0;
            double width = _rectRateBar.Width > 0 ? _rectRateBar.Width : 0;
            // 90 % 초과 시 막대 그래프 안쪽에 텍스트를 넣는다.
            double left = width > (_rectTotBar.ActualWidth * 90 / 100) ? width - _txtData.ActualWidth - 5 : txtData_df_margin_left + width;
            margin.Left = left;
            _txtData.Margin = margin;
            //Console.WriteLine(string.Format("data={0}, left margin={1}, actual_width={2}, width={3}", data, left, _rectRateBar.ActualWidth, _rectRateBar.Width));
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            setBarChart();
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            setBarChart_withAnimation();
        }         
    }
}
