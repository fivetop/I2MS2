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
using System.Diagnostics;
using I2MS2.Windows;
using I2MS2.Library;
using System.Collections;

namespace I2MS2.UserControls
{
    /// <summary>
    /// RackControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class RackControl : UserControl
    {
        public static RoutedCommand InsertSpaceCommand = new RoutedCommand();
        public static RoutedCommand DeleteSpaceCommand = new RoutedCommand();
        public static RoutedCommand DeleteAssetCommand = new RoutedCommand();
        
        public event RoutedEventHandler InsertSpaceClicked;
        public event RoutedEventHandler DeleteSpaceClicked;
        public event RoutedEventHandler DeleteAssetClicked;
        public event RoutedEventHandler SelectionChanged;


        public delegate void SelectChangedHander(int id);
        public event SelectChangedHander SelectionChangedEvent;

       
       
        // TotalUnit

        public static readonly DependencyProperty TotalUnitProperty =
            DependencyProperty.Register("TotalUnit", typeof(double), typeof(RackControl), new UIPropertyMetadata((double)42));

        public double TotalUnit
        {
            get { return (double)GetValue(TotalUnitProperty); }
            set
            {
                SetValue(TotalUnitProperty, value);
            }
        }

        // RackName

        public static readonly DependencyProperty RackNameProperty =
            DependencyProperty.Register("RackName", typeof(string), typeof(RackControl));

        public string RackName
        {
            get { return (string)GetValue(RackNameProperty); }
            set
            {
                SetValue(RackNameProperty, value);
            }
        }

        // MyItemsSource

        public static readonly DependencyProperty MyItemsSourceProperty =
            DependencyProperty.Register("MyItemsSource", typeof(IEnumerable), typeof(RackControl));

        public IEnumerable MyItemsSource
        {
            get { return (IEnumerable)GetValue(MyItemsSourceProperty); }
            set
            {
                SetValue(MyItemsSourceProperty, value);
            }
        }

        // PixelPerUnit

        public static readonly DependencyProperty PixelPerUnitProperty =
            DependencyProperty.Register("PixelPerUnit", typeof(double), typeof(RackControl), new UIPropertyMetadata((double)20));

        public double PixelPerUnit
        {
            get { return (double)GetValue(PixelPerUnitProperty); }
            set
            {
                SetValue(PixelPerUnitProperty, value);
            }
        }

        // SelectedImage = 220, 440, 880 보여줄 이미지 크기에 따라....

        public static readonly DependencyProperty SelectedImageProperty =
            DependencyProperty.Register("SelectedImage", typeof(int), typeof(RackControl), new UIPropertyMetadata((int)440));

        public int SelectedImage
        {
            get { return (int)GetValue(SelectedImageProperty); }
            set
            {
                SetValue(SelectedImageProperty,  value);
            }
        }

        public RackControl()
        {
            InitializeComponent();
        }

        private void _cmdInsertSpace_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var vm = (RackVM) _lvSlots.SelectedItem;
            e.CanExecute = vm != null;
        }

        private void _cmdInsertSpace_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (InsertSpaceClicked != null)
            {
                InsertSpaceClicked(this, new RoutedEventArgs());
            }
        }

        private void _cmdDeleteSpace_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var vm = (RackVM)_lvSlots.SelectedItem;
            int asset_id = vm != null ? vm.asset_id : 0;
            e.CanExecute = asset_id == 0;
        }

        private void _cmdDeleteSpace_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (DeleteSpaceClicked != null)
            {
                DeleteSpaceClicked(this, new RoutedEventArgs());
            }
        }

        private void _cmdDeleteAsset_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var vm = (RackVM)_lvSlots.SelectedItem;
            int asset_id = vm != null ? vm.asset_id : 0;
            e.CanExecute = asset_id != 0;
        }

        private void _cmdDeleteAsset_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (DeleteAssetClicked != null)
            {
                DeleteAssetClicked(this, new RoutedEventArgs());
            }
        }

        private void _lvSlots_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectionChanged != null)
            {
                SelectionChanged(this, new RoutedEventArgs());
                
            }
            else if(SelectionChangedEvent !=null)
            {
                if (_lvSlots.SelectedItem is RackVM)
                {
                    RackVM vm = (RackVM)_lvSlots.SelectedItem;
                    SelectionChangedEvent(vm.asset_id);
                }
            }
        }

        public void selectSlot(RackVM vm)
        {
            int index = _lvSlots.Items.IndexOf(vm);
            _lvSlots.SelectedIndex = index;
        }
    }


    public class GridLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val = (double)value;
            GridLength gridLength = new GridLength(val, GridUnitType.Star);

            return gridLength;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GridLength val = (GridLength)value;

            return val.Value;
        }
    }

    public class WidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val = (double)value;
            // 스크롤바 사이즈만큼 뺀다.
            double height = val - 20;

            if (height < 0)
                height = 0;

            return height;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val = (double) value;
            double width = val + 20;
            return width;
        }
    }

    public class HeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val = (double)value;
            // 슬롯은 11:1의 비율이다.
            double height = (val - 20) / 11;

            return height;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val = (double)value;
            double width = val * 11 + 40;
            return width;
        }
    }

    public class HeightConverter2 : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            double height = (double) values[0] - 20;
            double unit_size = (double) values[1];
            double total = height * unit_size;

            double r = total / 11;
            if (r < 0)
                r = 0;
            return r;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class WidthConverter_ : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val = (double)value;
            // 좌측 텍스트 공간 + 좌.우 장치고정 공간
            double width = val * 11 + 20 + 10 + 10;
            if (width < 0)
                width = 0;
            return width;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val = (double)value;
            double width = (val - 20 - 10 - 10) / 11;
            if (width < 0)
                width = 0;
            return width;
        }
    }

    public class HeightConverter_ : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val = (double)value;
            // 슬롯은 11:1의 비율이다.
            double height = val;
            return height;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val = (double)value;
            double height = val;
            return height;
        }
    }

    public class HeightConverter2_ : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            double pixel_4_unit = (double)values[0];
            double unit_size = (double)values[1];
            double height = pixel_4_unit * unit_size;
            if (height < 0)
                height = 0;
            return height;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TopConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            double height = (double)values[0];
            double unit_size = (double)values[1];
            double unit_height = height / (unit_size > 0 ? unit_size : 1);
            double top = 0;
            if (unit_size > 1)
                top = -(height - unit_height);

            Thickness th = new Thickness(0, top, 0, 0);
            return th;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class RackSelector : StyleSelector
    {
        public Style ipp_style { get; set; }
        public Style general_style { get; set; }
        public Style empty_style { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            RackVM vm = item as RackVM;

            if (vm == null)
                return empty_style;
            if (vm.catalog_id == 0)
                return empty_style;
            if (CatalogType.is_ipp(vm.catalog_id))
                return ipp_style;
            else
                return general_style;
        }
    }
}
