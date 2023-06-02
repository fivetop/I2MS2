using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

namespace I2MS2.UserControls
{
	/// <summary>
	/// UserControlMeter.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class AngleMeter : UserControl
	{
        public AngleMeter()
        {
            InitializeComponent();
        }

        public int ivalue
        {
            get { return (int)GetValue(ivalueProperty); }
            set { SetValue(ivalueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ivalue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ivalueProperty =
            DependencyProperty.Register("ivalue", typeof(int), typeof(AngleMeter), new PropertyMetadata(1, OnivalueChanged));

        private static void OnivalueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            int oldValue = (int)e.OldValue;
            int newValue = (int)e.NewValue;
            AngleMeter source = (AngleMeter)d;
            source.OnTempHumiPropertyChanged(oldValue, newValue);
        }

        private void OnTempHumiPropertyChanged(int oldValue, int newValue)
        {
            double t1 = Convert.ToDouble(newValue);
            double t2;
            //textValue.Text = t1.ToString();

            if (t1 != 0.0 && t1 <= TMax)
                t2 = (((270.0) / TMax) * t1);
            else if (t1 > TMax)
                t2 = 270.0;
            else
                t2 = 0.0;
            Animation(t2);
        }

        public void Animation(double p)
        {
            (path.RenderTransform as RotateTransform).BeginAnimation(RotateTransform.AngleProperty, null);

            DoubleAnimationUsingKeyFrames da1 = new DoubleAnimationUsingKeyFrames();
            SplineDoubleKeyFrame kf1 = new SplineDoubleKeyFrame();
            Storyboard sb1 = new Storyboard();
            sb1.Stop();
            da1.Duration = new Duration(TimeSpan.FromSeconds(1));
            da1.BeginTime = TimeSpan.FromSeconds((0));

            kf1.KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0));
            kf1.Value = 0;
            da1.KeyFrames.Add(kf1);
            kf1.KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 1));
            kf1.Value = p;
            da1.KeyFrames.Add(kf1);

            Storyboard.SetTarget(da1, path);
            Storyboard.SetTargetProperty(da1, new PropertyPath("RenderTransform.Angle"));

            sb1.Children.Add(da1);
            sb1.Begin();
        }

        public double TMax
        {
            get { return (double)GetValue(TMaxProperty); }
            set { SetValue(TMaxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TMax.  This enables animation, styling, binding, etc...
        public static DependencyProperty TMaxProperty =
            DependencyProperty.Register("TMax", typeof(double), typeof(AngleMeter));

    }
}