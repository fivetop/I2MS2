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

namespace I2MS2.UserControls
{
    /// <summary>
    /// RackSlotControl.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public partial class RackGeneralControl : UserControl
    {
        // 화면에 표시할 비트맵
        public static readonly DependencyProperty MyImageSourceProperty =
            DependencyProperty.Register("MyImageSource", typeof(BitmapImage), typeof(RackGeneralControl),
            new FrameworkPropertyMetadata((BitmapImage)new BitmapImage(),
                      FrameworkPropertyMetadataOptions.None));

        public BitmapImage MyImageSource
        {
            get { return (BitmapImage)GetValue(MyImageSourceProperty); }
            set
            {
                SetValue(MyImageSourceProperty, value);
            }
        }

        // 유닛 사이즈
        public static readonly DependencyProperty MyUnitSizeProperty =
            DependencyProperty.Register("MyUnitSize", typeof(int), typeof(RackGeneralControl));

        public int MyUnitSize
        {
            get { return (int)GetValue(MyUnitSizeProperty); }
            set
            {
                SetValue(MyUnitSizeProperty, value);
            }
        }

        public RackGeneralControl()
        {
            InitializeComponent();   
        }
    }

    //public class GridLengthConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        double val = (double)value;
    //        GridLength gridLength = new GridLength(val, GridUnitType.Star);

    //        return gridLength;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        GridLength val = (GridLength)value;

    //        return val.Value;
    //    }
    //}

}
