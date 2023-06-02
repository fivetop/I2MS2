using I2MS2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebApi.Models;

namespace I2MS2.UserControls.Drawing
{
    // 자산뷰에서 3D 처리 ??
    /// <summary>
    /// DrawingObjectInfoView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DrawingObjectInfoView2 : UserControl
    {
        public Point3D point { get; set; } 

        public ObservableCollection<eb_port_data_cur> _target { get; set; }      // 바인딩 처리용

        public DrawingObjectInfoView2()
        {
            _target = new ObservableCollection<eb_port_data_cur>();
            InitializeComponent();
            this.DataContext = _target;
        }
    }

    public class vConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return 0;
            int v1 = (int)value;
            double v2 = v1 / 10;
            return v2;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class vColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return App.Current.Resources["AccentColorBrush"] as Brush;
            int status = (int)value;
            switch (status)
            {
                case 0:  // 닫힘
                    return App.Current.Resources["AccentColorBrush"] as Brush;
                case 1:  // 알람
                    return App.Current.Resources["_brushRed3"] as Brush;
                default:
                    return App.Current.Resources["AccentColorBrush"] as Brush;
                    //return App.Current.Resources["_brushYellow"] as Brush;
            }
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }


}
