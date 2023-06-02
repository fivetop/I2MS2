using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

// 이미지만을 가지고 애니메이션 처리 
// i1 은 바탕이미지, i2는 애니메이션용 이미지 
// 그리드에 위치 시키고 i2를 애니메이션 시킬 최대 크기로 제작
// i2를 상,하 사이즈에 맞도록 조정 후 애니메이션 처리 
// 이미지 크기에 따라 조정값이 틀리므로 애니메이션 제작시 체크 

namespace I2MS2.UserControls
{
	/// <summary>
	/// TempControl.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class TempHumi : UserControl
	{
        Storyboard sb;
        DoubleAnimationUsingKeyFrames da;
        EasingDoubleKeyFrame kf;

        public TempHumi()
		{
			this.InitializeComponent();

            sb = (Storyboard)this.FindResource("Storyboard1");
            da = (DoubleAnimationUsingKeyFrames) sb.Children[0];
            kf = (EasingDoubleKeyFrame)da.KeyFrames[0];
		}


        //  -190, -5 기본 사용값 그래프 기본 눈금 이미지 상하 조정값 
        public double ivalue
        {
            get { return (double)GetValue(ivalueProperty); }
            set { SetValue(ivalueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ivalue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ivalueProperty =
            DependencyProperty.Register("ivalue", typeof(double), typeof(TempHumi), new PropertyMetadata(10.0, OnivalueChanged));

        private static void OnivalueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var oldValue = e.OldValue;
            var newValue = e.NewValue;
            TempHumi source = (TempHumi)d;
            source.OnivalueChanged(oldValue, newValue);
        }

        // 상 - 하 = 변동 폭  -133   상 25 하 158 이미지에 따라 차이가 있음  
        private void OnivalueChanged(object oldValue, object newValue)
        {
            if (kf != null)
            {
                double t1 = Convert.ToDouble(newValue);
                if (t1 != 0.0 && t1 < TMax)
                    kf.Value = ((-133 / TMax) * t1) + 158;
                else if (t1 >= TMax)
                    kf.Value = 25; // 상 
                else
                    kf.Value = 158;   // 하 
                //myAnimation(kf.Value);
                sb.Begin();
            }
        }

        // 표현 최대값 
        public double TMax
        {
            get { return (double)GetValue(TMaxProperty); }
            set { SetValue(TMaxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TMax.  This enables animation, styling, binding, etc...
        public static DependencyProperty TMaxProperty =
            DependencyProperty.Register("TMax", typeof(double), typeof(TempHumi), new PropertyMetadata(100.0));
        // 최소값
        public double TMIn
        {
            get { return (double)GetValue(TMInProperty); }
            set { SetValue(TMInProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TMIn.  This enables animation, styling, binding, etc...
        public static DependencyProperty TMInProperty =
            DependencyProperty.Register("TMIn", typeof(double), typeof(TempHumi), new PropertyMetadata(0.0));
	}
}