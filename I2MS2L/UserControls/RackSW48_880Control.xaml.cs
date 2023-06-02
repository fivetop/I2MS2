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

    public partial class RackSW48_880Control : UserControl
    {

        public delegate void MySelectedChangeEventHandler(object obj);
        public event MySelectedChangeEventHandler MySelectedChangeEvent;

        // MyItemsSource

        public static readonly DependencyProperty MyItemsSourceProperty =
            DependencyProperty.Register("MyItemsSource", typeof(IEnumerable), typeof(RackSW48_880Control));

        public IEnumerable MyItemsSource
        {
            get { return (IEnumerable)GetValue(MyItemsSourceProperty); }
            set
            {
                SetValue(MyItemsSourceProperty, value);
            }
        }

        public RackSW48_880Control()
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

    public class SW48PortWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            double width = 1;
            if (value == null)
                return width;
            int port_no = (int)value;

            if ((port_no == 11) || (port_no == 12))
                width = 33;
            else if ((port_no == 23) || (port_no == 24))
                width = 31;
            else if ((port_no == 35) || (port_no == 36))
                width = 31;
            else if ((port_no == 47) || (port_no == 48))
                width = 34;
            else if (port_no == 50)
                width = 30;
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

    public class SW48PortHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            double height = 1;
            if (value == null)
                return height;
            int port_no = (int)value;

            if (port_no > 48)
                height = 44;
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

    public class SW48PortStatusConverter : IValueConverter
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
            }
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
