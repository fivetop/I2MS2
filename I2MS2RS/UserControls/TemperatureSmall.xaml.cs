using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.ComponentModel;
using System.Collections;

//
// 온도/습도 미터 
// 
namespace I2MS2.UserControls
{
	/// <summary>
	/// Temp1.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class TemperatureMeterSmall : UserControl
	{
        Storyboard tsb;
        DoubleAnimationUsingKeyFrames da;
        SplineDoubleKeyFrame sbkf;

		public TemperatureMeterSmall()
		{
			this.InitializeComponent();

            tsb = (Storyboard)this.FindResource("Storyboard2");
            da = (DoubleAnimationUsingKeyFrames)tsb.Children[0];
            sbkf = (SplineDoubleKeyFrame)da.KeyFrames[0];
		}

        #region 온도계 메소드 
        // 스토리 보드 리턴 
        public Storyboard sb()
        {
            return (Storyboard)this.FindResource("Storyboard2");
        }
        // 컬러 설정  온도계 : Red, 습도계 : Blue
        public void SetFillc(Color c1)
        {
            Brush t1;
            t1 = new SolidColorBrush(c1);
            _gage1.Fill = t1;
            if (c1.ToString() == "Red")
                TempHumiValue = "ºC";
            else
                TempHumiValue = "ºF";
        }
        #endregion

        #region 프로퍼티 메소드



        // 127, 35 기본 사용값 그래프 기본 눈금 
        public double TempHumi
        {
            get { return (double)GetValue(TempHumiProperty); }
            set
            {
                SetValue(TempHumiProperty, value);
            }
        }
        public static DependencyProperty TempHumiProperty = DependencyProperty.Register("TempHumi", typeof(string), typeof(TemperatureMeterSmall), new PropertyMetadata("5", OnTempHumiPropertyChanged));

        private static void OnTempHumiPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
            IEnumerable oldValue = (IEnumerable)e.OldValue;
            IEnumerable newValue = (IEnumerable)e.NewValue;
            TemperatureMeterSmall source = (TemperatureMeterSmall)d;
            source.OnTempHumiPropertyChanged(oldValue, newValue);
        }

        private void OnTempHumiPropertyChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            if (sbkf != null)
            {
                double t1 = Convert.ToDouble(newValue);
                if (t1 != 0.0 && t1 <= TMax)
                    sbkf.Value = (((127.0 - 35) / TMax) * t1) + 35;
                else if (t1 > TMax)
                    sbkf.Value = 127.0;
                else
                    sbkf.Value = 35.0;
                tsb.Begin();
            }
        }



        public Color ucolor
        {
            get { return (Color)GetValue(ucolorProperty); }
            set { SetValue(ucolorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ucolor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ucolorProperty =
            DependencyProperty.Register("ucolor", typeof(Color), typeof(TemperatureMeterSmall), new PropertyMetadata(System.Windows.Media.Colors.Transparent));

        
        // 표현 최대값 
        public double TMax
        {
            get { return (double)GetValue(TMaxProperty); }
            set { SetValue(TMaxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TMax.  This enables animation, styling, binding, etc...
        public static DependencyProperty TMaxProperty =
            DependencyProperty.Register("TMax", typeof(double), typeof(TemperatureMeterSmall), new PropertyMetadata(100.0));
        // 최소값
        public double TMIn
        {
            get { return (double)GetValue(TMInProperty); }
            set { SetValue(TMInProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TMIn.  This enables animation, styling, binding, etc...
        public static DependencyProperty TMInProperty =
            DependencyProperty.Register("TMIn", typeof(double), typeof(TemperatureMeterSmall), new PropertyMetadata(0.0));

        // 25ºC  25ºF 
        public string TempHumiValue
        {
            get { return (string)GetValue(TempHumiValueProperty); }
            set { SetValue(TempHumiValueProperty, value); }
        }
        // Using a DependencyProperty as the backing store for TempHumiValue.  This enables animation, styling, binding, etc...
        public static DependencyProperty TempHumiValueProperty =
            DependencyProperty.Register("TempHumiValue", typeof(string), typeof(TemperatureMeterSmall), new PropertyMetadata("ºC"));

        // 색상 적용         
        public string Fills
        {
            get { return (string)GetValue(FillsProperty); }
            set { SetValue(FillsProperty, value);}
        }

        // Using a DependencyProperty as the backing store for Fills.  This enables animation, styling, binding, etc...
        public static DependencyProperty FillsProperty =
            DependencyProperty.Register("Fills", typeof(string), typeof(TemperatureMeterSmall), new PropertyMetadata("Red"));

        #endregion

        private void _UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RotateTransform rotate = new RotateTransform();
            try
            {
                var t1 = (TransformGroup)this.RenderTransform;
                rotate = t1.Children[2] as RotateTransform;
                string s1 = rotate.Angle.ToString();

                if (s1 == "90")
                {
                    _a1.Visibility = Visibility.Collapsed;
                    _a2.Visibility = Visibility.Collapsed;
                    _a3.Visibility = Visibility.Visible;
                    _a4.Visibility = Visibility.Visible;
                }
                //rotate = this.RenderTransform as RotateTransform;
            }
            catch
            {
                _a1.Visibility = Visibility.Visible;
                _a2.Visibility = Visibility.Visible;
                _a3.Visibility = Visibility.Collapsed;
                _a4.Visibility = Visibility.Collapsed;
            }

        }
    }
}