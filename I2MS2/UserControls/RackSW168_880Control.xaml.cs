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

    public partial class RackSW168_880Control : UserControl
    {

        public delegate void MySelectedChangeEventHandler(object obj);
        public event MySelectedChangeEventHandler MySelectedChangeEvent;

        // MyItemsSource

        public static readonly DependencyProperty MyItemsSourceProperty =
            DependencyProperty.Register("MyItemsSource", typeof(IEnumerable), typeof(RackSW168_880Control));

        public IEnumerable MyItemsSource
        {
            get { return (IEnumerable)GetValue(MyItemsSourceProperty); }
            set
            {
                SetValue(MyItemsSourceProperty, value);
            }
        }

        public RackSW168_880Control()
        {
            InitializeComponent();
        }

        private void _lvArrow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int idx = 0;
            idx = _lvArrow.SelectedIndex;
            MySelectedChangeEvent(idx);
        }

    }

    public class SW168PortWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            double width = 1;
            if (value == null)
                return width;
            int port_no = (int)value;

            if ((port_no == 7) || (port_no == 31) || (port_no == 55) || (port_no == 79) || (port_no == 103) || (port_no == 127) || (port_no == 151) || (port_no == 175))
                width = 32;
//            if ((port_no == 19) || (port_no == 43) || (port_no == 67) || (port_no == 91) || (port_no == 115) || (port_no == 139) || (port_no == 163) || (port_no == 187))
//                width = 32;
            else
                width = 28;
            return width;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class SW168PortHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            double height = 1;
            if (value == null)
                return height;
            int port_no = (int)value;

            if (port_no > 168)
                height = 23;
            else if (port_no > 144)
                height = 24;
            else if (port_no > 120)
                height = 24;
            else if (port_no > 96)
                height = 25;
            else
                height = 22;
            return height;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class SW168PortStatusConverter : IValueConverter
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
                    return App.Current.Resources["_brushGreen"] as Brush;
                case "L":
                    // linked
                    return App.Current.Resources["_brushGreen"] as Brush;
                default:
                    return App.Current.Resources["_brushDarkGray1"] as Brush;
                    //return App.Current.Resources["_brushGreen"] as Brush;
            }
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
