using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace I2MS2.UserControls
{
    /// <summary>
    /// DB_Panel.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DB_Panel : UserControl
    {
        public delegate void CloseEventHandler(object obj, RoutedEventArgs e, Object o);
        public event CloseEventHandler CloseEvent;

        public DB_Panel()
        {
            InitializeComponent();
        }

        private void ExitButton_Clicked(object sender, RoutedEventArgs e)
        {
            CloseEvent(sender, e, this);
        }

        public int position
        {
            get { return (int)GetValue(positionProperty); }
            set { SetValue(positionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for position.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty positionProperty =
            DependencyProperty.Register("position", typeof(int), typeof(DB_Panel), new PropertyMetadata(0));

        

        public string  TitleName
        {
            get { return (string )GetValue(TitleNameProperty); }
            set { SetValue(TitleNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleNameProperty =
            DependencyProperty.Register("TitleName", typeof(string ), typeof(DB_Panel), new PropertyMetadata("Title Name"));

        public static object Getchild(DependencyObject obj)
        {
            return (object)obj.GetValue(childProperty);
        }

        public static void Setchild(DependencyObject obj, object value)
        {
            obj.SetValue(childProperty, value);
        }

        // Using a DependencyProperty as the backing store for child.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty childProperty =
            DependencyProperty.RegisterAttached("child", typeof(object), typeof(DB_Panel), new PropertyMetadata(null));

    }

    // 대쉬 보드 패널 폭 컨버터
    public class Panel1WidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return 0;
            double width = (double)value;
            double width2 = (width - 78) / 4;
            if (width2 < 0)
                width2 = 0;
            return width2;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    // 대쉬 보드 패널 폭 컨버터
    public class Panel2WidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return 0;
            double width = (double)value;
            double width2 = (width - 60) / 2;
            if (width2 < 0)
                width2 = 0;
            return width2;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    // 대쉬 보드 패널 폭 컨버터
    public class Panel3WidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return 0;
            double width = (double)value;
            double width2 = (width - 54) * 3 / 4;
            if (width2 < 0)
                width2 = 0;
            return width2;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    // 대쉬 보드 패널 폭 컨버터
    public class Panel4WidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return 0;
            double width = (double)value;
            double width2 = width - 48;
            if (width2 < 0)
                width2 = 0;
            else if (width2 < 1200)
                width2 = 1200;

            return width2;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    // 대쉬 보드 패널 폭 컨버터
    public class ZoomPercentageConverter2 : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return "0%";
            double v1 = (double)value;
            double v2 = v1 * 2;
            string s2 = String.Format("{0:00.0}%", v2);
            return s2;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    // 대쉬 보드 패널 폭 컨버터
    public class ZoomPercentageConverter3 : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return "0 Kwh";
            double v1 = (double)value;
            double v2 = v1 * 2;
            string s2 = String.Format("{0:00.0} KWh", v2);
            return s2;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    // 대쉬 보드 패널 폭 컨버터
    public class AngleWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType,
            object parameter, CultureInfo culture)
        {
            Color y = Color.FromRgb(0xAF, 0xB8, 0x06);
            Color r = Color.FromRgb(0xf7, 0x40, 0x40);
            Color b = Color.FromRgb(0x12, 0xd3, 0xf2);

            if (values == null)
                return b;

            int ivalue;
            int imax;
            int imin;

            try
            {
                ivalue = (int)values[0];
                imax = (int)values[1];
                imin = (int)values[2];
            }
            catch (Exception)
            {
                return b;
            }
            if (imax == 0) return b;
            if (ivalue >= imax) return r;
            if (ivalue <= imin) return y;
            return b;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
            //return value;
        }

        //public object ConvertBack(object value, Type targetType,
        //    object parameter, CultureInfo culture)
        //{
        //    return value;
        //}
    }



}
