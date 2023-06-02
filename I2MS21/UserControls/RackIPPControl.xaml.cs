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

    public partial class RackIPPControl : UserControl
    {
        // MyItemsSource

        public static readonly DependencyProperty MyItemsSourceProperty =
            DependencyProperty.Register("MyItemsSource", typeof(IEnumerable), typeof(RackIPPControl));

        public IEnumerable MyItemsSource
        {
            get { return (IEnumerable)GetValue(MyItemsSourceProperty); }
            set
            {
                SetValue(MyItemsSourceProperty, value);
            }
        }

        //public static readonly DependencyProperty MyImageSourceProperty =
        //    DependencyProperty.Register("MyImageSource", typeof(BitmapImage), typeof(RackIPPControl),
        //    new FrameworkPropertyMetadata((BitmapImage)new BitmapImage(),
        //              FrameworkPropertyMetadataOptions.None));

        //public BitmapImage MyImageSource
        //{
        //    get { return (BitmapImage)GetValue(MyImageSourceProperty); }
        //    set
        //    {
        //        SetValue(MyImageSourceProperty, value);
        //    }
        //}

        public RackIPPControl()
        {
            InitializeComponent();   
        }
    }
}
