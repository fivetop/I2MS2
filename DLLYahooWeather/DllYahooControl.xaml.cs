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
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Threading;

namespace DLLYahooWeather
{
    /// <summary>
    /// DLLYahooWeatherCtrl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DLLYahooWeatherCtrl : UserControl
    {
        public static readonly DependencyProperty CurTempProperty = DependencyProperty.Register(
          "CurTemp", typeof(string), typeof(DLLYahooWeatherCtrl), new PropertyMetadata("?°"));
        public string CurTemp
        {
            get { return (string)this.GetValue(CurTempProperty); }
            set { this.SetValue(CurTempProperty, value); }
        }

        public static readonly DependencyProperty TodayTempProperty = DependencyProperty.Register(
          "TodayTemp", typeof(string), typeof(DLLYahooWeatherCtrl), new PropertyMetadata(String.Empty));
        public string TodayTemp
        {
            get { return (string)this.GetValue(TodayTempProperty); }
            set { this.SetValue(TodayTempProperty, value); }
        }

        public static readonly DependencyProperty CurImageProperty = DependencyProperty.Register(
          "CurImage", typeof(ImageSource), typeof(DLLYahooWeatherCtrl), new PropertyMetadata(null));
        public ImageSource CurImage
        {
            get { return (ImageSource)this.GetValue(CurImageProperty); }
            set { this.SetValue(CurImageProperty, value); }
        }

        public static readonly DependencyProperty TodayImageProperty = DependencyProperty.Register(
          "TodayImage", typeof(ImageSource), typeof(DLLYahooWeatherCtrl), new PropertyMetadata(null));
        public ImageSource TodayImage
        {
            get { return (ImageSource)this.GetValue(TodayImageProperty); }
            set { this.SetValue(TodayImageProperty, value); }
        }

        public static readonly DependencyProperty UnitIsCProperty = DependencyProperty.Register(
          "UnitIsC", typeof(bool), typeof(DLLYahooWeatherCtrl), new PropertyMetadata(true));
        public bool UnitIsC
        {
            get { return (bool)this.GetValue(UnitIsCProperty); }
            set
            {
                this.SetValue(UnitIsCProperty, value);
                if (WOEID.Length > 6)
                    UpdateValues();
            }
        }

        public static readonly DependencyProperty WOEIDProperty = DependencyProperty.Register(
          "WOEID", typeof(string), typeof(DLLYahooWeatherCtrl), new PropertyMetadata(""));
        public string WOEID
        {
            get { return (string)this.GetValue(WOEIDProperty); }
            set
            {
                this.SetValue(WOEIDProperty, value);
                if (value.Length > 6)
                    UpdateValues();
            }
        }

        // 15초에 한번씩 날씨 가져와서 셋팅하기 
        public DLLYahooWeatherCtrl()
        {
            InitializeComponent();
            LoadSettings();
            CurImage = new BitmapImage(new Uri("pack://application:,,,/DLLYahooWeather;component/na.png"));
            TodayImage = new BitmapImage(new Uri("pack://application:,,,/DLLYahooWeather;component/na.png"));
            CurTemp = "18°";
            TodayTemp = "0° 18°";

            this.DataContext = this;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(15);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        public void DllCall()
        {
            UpdateValues();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            UpdateValues();
        }

        private void LoadSettings()
        {
            Properties.Settings.Default.Reload();
            UnitIsC = Properties.Settings.Default.unit;
            WOEID = Properties.Settings.Default.woeid;
        }

        private void UpdateValues()
        {
            try
            {
                using (WebClient meteo = new WebClient())
                {
                    string unit = "c";
                    if (!UnitIsC)
                        unit = "f";
                    meteo.DownloadStringCompleted += new DownloadStringCompletedEventHandler(gismeteo_DownloadStringCompleted);
                    meteo.DownloadStringAsync(new Uri(String.Format("http://weather.yahooapis.com/forecastrss?w={0}&u={1}", WOEID, unit)));
                }
            }
            catch
            {
                CurImage = new BitmapImage(new Uri("pack://application:,,,/DLLYahooWeather;component/na.png"));
                TodayImage = null;
                CurTemp = "?°";
                TodayTemp = "";
            }
        }

        // 현재의 온도와 금일의 날씨 온도 가져 오기 : 야후 API 사용  
        void gismeteo_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                Regex regex = new Regex("temp=\"([-+0-9]*)\"");
                string val = regex.Match(e.Result).Groups[1].Value;
                CurTemp = (val.Contains("+") ? val : "+" + val) + "°";

                Regex codeRegex = new Regex("code=\"([0-9]*)\"");
                Match codeMatch = codeRegex.Match(e.Result);
                string code = codeMatch.Groups[1].Value;
                //CurImage = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img\\" + code + ".png"));
                CurImage = new BitmapImage(new  Uri("pack://application:,,,/DLLYahooWeather;component/img/" + code + ".png"));
                 

                code = codeMatch.NextMatch().Groups[1].Value;
//                TodayImage = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img\\" + code + ".png"));
                TodayImage = new BitmapImage(new Uri("pack://application:,,,/DLLYahooWeather;component/img/" + code + ".png"));

                Console.WriteLine("CODE : {0}",code);

                regex = new Regex("low=\"([-+0-9]*)\"");
                Match match = regex.Match(e.Result);
                string low1 = (match.Groups[1].Value.Contains("+") ?
                    match.Groups[1].Value : "+" + match.Groups[1].Value) + "°"; ;
                match = regex.Match(e.Result).NextMatch();
                string low2 = (match.Groups[1].Value.Contains("+") ?
                    match.Groups[1].Value : "+" + match.Groups[1].Value) + "°"; ;
                regex = new Regex("high=\"([-+0-9]*)\"");
                match = regex.Match(e.Result);
                string high1 = (match.Groups[1].Value.Contains("+") ?
                    match.Groups[1].Value : "+" + match.Groups[1].Value) + "°"; ;
                match = regex.Match(e.Result).NextMatch();
                string high2 = (match.Groups[1].Value.Contains("+") ?
                    match.Groups[1].Value : "+" + match.Groups[1].Value) + "°"; ;
                TodayTemp = low1 + "  " + high1;
                Console.WriteLine("TodayTemp : {0}", TodayTemp);
            }
            catch
            {
                CurImage = new BitmapImage(new Uri("pack://application:,,,/DLLYahooWeather;component/na.png"));
                TodayImage = null;
                CurTemp = "?°";
                TodayTemp = "";
            }
        }
    }
}
