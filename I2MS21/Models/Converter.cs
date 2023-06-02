using System;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;
using System.Windows;
using WebApi.Models;
using System.IO;
using System.Windows.Media.Imaging;

namespace I2MS2.Models
{
    public class GetFrontBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return new SolidColorBrush(Colors.Red);

            bool is_front = (bool) value;
            if (is_front)
                return new  SolidColorBrush(Colors.Green);
            else
                return new SolidColorBrush(Colors.Yellow);
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class DummyColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return Colors.Transparent; 

            return value;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class String2DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return DateTime.Now;

            string str = (string) value;

            return DateTime.Parse(str);
            ;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            DateTime dt = (DateTime) value;

            return dt.ToShortDateString();
        }
    }

    public class PPStatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return App.Current.Resources["_brushDarkGray5"] as Brush;
            string status = (string)value;
            switch(status)
            {
                case "Y" :  // 연결
                    return App.Current.Resources["_brushGreen"] as Brush;
                case "A" :  // 알람
                    return App.Current.Resources["_brushRed"] as Brush;
                default :
                    return App.Current.Resources["_brushDarkGray5"] as Brush;
            }
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class SelectedImage220Converter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Hidden;
            int status = (int)value;
            if (status == 220)
                    return Visibility.Visible;
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class SelectedImage220Converter2 : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
            int status = (int)value;
            return status == 220;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class SelectedImage440Converter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Hidden;
            int status = (int)value;
            if (status == 440)
                return Visibility.Visible;
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class SelectedImage440Converter2 : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
            int status = (int)value;
            return status == 440;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class SelectedImage880Converter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Hidden;
            int status = (int)value;
            if (status == 880)
                return Visibility.Visible;
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class SelectedImage880Converter2 : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
            int status = (int)value;
            return status == 880;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }


    public class Bool2VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Hidden;
            bool status = (bool)value;
            if (status)
                return Visibility.Visible;
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class Bitmap220_440Converter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType,
            object parameter, CultureInfo culture)
        {
            ImageSourceConverter conv = new ImageSourceConverter();
            string image_220 = "";
            string image_440 = "";
            string null_file = "pack://application:,,,/I2MS2;component/Images/null.png";
            double height = 0;
            double unit_size = 0;
            double one_unit_height = 0;

            if (values == null)
                return get_bitmap(null_file);
            try
            {
                image_220 = (string)values[0];
                image_440 = (string)values[1];
                height = (double)values[2];
                unit_size = (double)values[3];
                one_unit_height = height / (unit_size > 0 ? unit_size : 1);
            }
            catch (Exception)
            {
                return get_bitmap(null_file);
            }

            string filename = Path.GetFileName(image_220);

            if ((one_unit_height > 30) && (image_440.IndexOf("/null.png") == -1))   // romee 2/9  // != > ==  변경 
                return get_bitmap(image_440);
            else
                return get_bitmap(image_220);
        }

        public object[] ConvertBack(object value, Type[] targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public BitmapImage get_bitmap(string file)
        {
            string file_name = file;
            if (file.Length > 17)
            {
                if (file.Substring(0, 17) == "/I2MS2;component/")
                    file_name = "pack://application:,,," + file;
            }
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.CacheOption = BitmapCacheOption.OnLoad;
            logo.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            logo.UriSource = new Uri(file_name);
            logo.EndInit();
            return logo;
        }

    }




    #region 링크 다이어그램에서 사용하는 컨버터들
    // 각 셀의 홀짝에 즉 자산셀에 대해서만 visible 처리
    public class AssetCellVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Hidden;

            int col_no = (int)value;   // 0번부터 시작
            if ((col_no % 2) == 0)
                return Visibility.Visible;
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    // romee 2015.07.14 UP 1
    public class GetDispCellConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (values == null)
                return "";

            string template_type;
            string asset_name;
            int port_no;
            string ca_disp_name;

            try
            {
                template_type = (string)values[0];
                asset_name = (string)values[1];
                port_no = (int)values[2];
                ca_disp_name = (string)values[3];
            }
            catch (Exception)
            {
                return "";
            }

            switch (template_type)
            {
                case "empty":
                    return "";
                case "cable":
                    return ca_disp_name;
                case "asset":
//                    return string.Format("{0}/{1}", asset_name, port_no);
                    return string.Format("{0}\n#{1}", asset_name, port_no);
                default:
                    return "";
            }
        }

        public object[] ConvertBack(object value, Type[] targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // 자산표현시 프론트 커넥터의 경우 케이블 연결 커넥터의 모서리 부분을 회색으로 표현
    public class GetLeftConnectorEdgeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return Colors.Transparent;

            bool is_left_front = (bool)value;
            if (is_left_front)
                return Colors.Yellow;
            return Colors.Transparent;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class GetRightConnectorEdgeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return Colors.Transparent;

            bool is_left_front = (bool)value;
            if (!is_left_front)
                return Colors.Yellow;
            return Colors.Transparent;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    // 케이블 내피-커스텀색상, 자산셀에서 사용
    public class GetEdgeColor1Converter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (values == null)
                return Colors.Transparent;

            ePortStatus plug_status;
            Color ca_disp_color_rgb;
            try
            {
                plug_status = (ePortStatus)values[0];
                ca_disp_color_rgb = (Color)values[1];
            }
            catch (Exception)
            {
                return Colors.Transparent;
            }

            if ((plug_status == ePortStatus.Plugged) || (plug_status == ePortStatus.Linked))
                return ca_disp_color_rgb;

            return Colors.Transparent;
        }

        public object[] ConvertBack(object value, Type[] targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // 케이블 외피색상, 자산셀에서만 사용
    public class GetEdgeColor2Converter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return Colors.Transparent;

            ePortStatus plug_status = (ePortStatus)value;
            if ((plug_status == ePortStatus.Plugged) || (plug_status == ePortStatus.Linked))
                return Colors.Black;
            return Colors.Transparent;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    // 케이블 내피-커스텀색상, 케이블셀에서 사용
    public class GetCableColor1Converter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (values == null)
                return Colors.Transparent;

            ePortStatus plug_status;
            Color ca_disp_color_rgb;

            try
            {
                plug_status = (ePortStatus)values[0];
                ca_disp_color_rgb = (Color)values[1];
            }
            catch (Exception)
            {
                return Colors.Transparent;
            }

            if (plug_status == ePortStatus.Unplugged)
                return ca_disp_color_rgb;

            return Colors.Transparent;
        }

        public object[] ConvertBack(object value, Type[] targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // 케이블 외피색상, 케이블셀에서만 사용
    public class GetCableColor2Converter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return Colors.Transparent;

            ePortStatus plug_status = (ePortStatus)value;
            if (plug_status == ePortStatus.Unplugged)
                return Colors.Black;
            return Colors.Transparent;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }


    public class GetMiniColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return Colors.Transparent;

            string template_type = (string)value;
            switch (template_type)
            {
                case "cable":
                    return Colors.Gray;
                case "asset":
                    return Colors.DarkGray;
                case "empty":
                default:
                    return Colors.Transparent;
            }
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }


    public class GetLeftCableMarginConverter : IValueConverter
    {
        private const int cable_top_margin = 29;
        private const int cable_edge_top_margin = 22;

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return new Thickness(10, cable_top_margin, 10, 0);

            ePortStatus plug_status = (ePortStatus)value;
            if ((plug_status == ePortStatus.Plugged) || (plug_status == ePortStatus.Linked))
                return new Thickness(0, cable_top_margin, 0, 0);
            return new Thickness(10, cable_top_margin, 10, 0);
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class GetRightCableMarginConverter : IValueConverter
    {
        private const int cable_top_margin = 29;
        private const int cable_edge_top_margin = 22;

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return new Thickness(0, cable_top_margin, 10, 0);

            ePortStatus plug_status = (ePortStatus)value;
            if ((plug_status == ePortStatus.Plugged) || (plug_status == ePortStatus.Linked))
                return new Thickness(0, cable_top_margin, 0, 0);
            return new Thickness(0, cable_top_margin, 10, 0);
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    // 케이블 외피색상, 케이블셀에서만 사용
    public class PlugStatus2VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Hidden;

            ePortStatus plug_status = (ePortStatus)value;
            if (plug_status == ePortStatus.Unplugged)
                return Visibility.Visible;
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class CenterCellConverter : IValueConverter
    {
        Color color = (Color)App.Current.Resources["_colorDarkGray1"];

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return color;

            int col_no = (int)value;   // 0번부터 시작
            if (col_no == g.CENTER_COL)
                return Colors.DarkBlue;
            if ((col_no % 2) == 0)
                return Colors.Gray;
            return Colors.Black;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    #endregion

    
}
