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
using System.Globalization;
using System.ComponentModel;
using System.Collections;

namespace I2MS2.UserControls
{
    /// <summary>
    /// RackSlotControl.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public partial class RackIPP880Control : UserControl
    {

        public delegate void MySelectedChangeEventHandler(object obj);
        public event MySelectedChangeEventHandler MySelectedChangeEvent;

        // MyItemsSource

        public static readonly DependencyProperty MyItemsSourceProperty =
            DependencyProperty.Register("MyItemsSource", typeof(IEnumerable), typeof(RackIPP880Control));

        public IEnumerable MyItemsSource
        {
            get { return (IEnumerable)GetValue(MyItemsSourceProperty); }
            set
            {
                SetValue(MyItemsSourceProperty, value);
            }
        }

        public RackIPP880Control()
        {
            InitializeComponent();   
        }

        private void _lvArrow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int idx = _lvArrow.SelectedIndex;
            MySelectedChangeEvent(idx);
        }
    }

    public class RearCableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return App.Current.Resources["_brushDarkGray1"] as Brush;
            bool status = (bool)value;

            if (status)
                return App.Current.Resources["_brushDarkGray3"] as Brush;
            else
                return App.Current.Resources["_brushDarkGray1"] as Brush;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }


    public class LedStatusConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            string alarm_status = (string)values[0];
            string wo_status = (string)values[1];
            string is_port_trace = (string)values[2];

            // port trace
            if (is_port_trace == "Y")
                return App.Current.Resources["_brushBlue"] as Brush;

            // alarm
            if ((alarm_status == "P") || (alarm_status == "U"))
                return App.Current.Resources["_brushRed"] as Brush;

            // work order
            if ((wo_status == "P") || (alarm_status == "U"))
                return App.Current.Resources["_brushGreen"] as Brush;

            return App.Current.Resources["_brushDarkGray1"] as Brush;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class PlugStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return App.Current.Resources["_brushDarkGray1"] as Brush;
            string status = (string)value;

            switch (status)
            {
                case "P":
                    // plugged
                    return App.Current.Resources["_brushBlue"] as Brush;
                case "L":
                    // linked
                    return App.Current.Resources["_brushBlue"] as Brush;
                default:
                    return App.Current.Resources["_brushDarkGray1"] as Brush;
            }
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class PlugStatusConverter2 : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            string alarm_status = (string)values[0];
            string wo_status = (string)values[1];       // 미사용
            string is_port_trace = (string)values[2];   // 미사용
            string port_status = (string)values[3];

            // alarm
            if ((alarm_status == "P") || (alarm_status == "U"))
                return App.Current.Resources["_brushRed"] as Brush;

            // 포트사용
            if ((port_status == "P") || (port_status == "L"))
                return App.Current.Resources["_brushGreen"] as Brush;

            return App.Current.Resources["_brushDarkGray1"] as Brush;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }    
}
