using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ConfigS
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void _ok_Click(object sender, RoutedEventArgs e)
        {
            Program pg = new Program();

            Program._lang_id = _cboLanguage.SelectedIndex == 0 ? 1080001 : 1080002;
            Program._nms_server_ip_addr = _p1.Text;
            Program._enable_ftp_interface = _p10.IsChecked ?? true;
            Program._enable_eb_interface = _p11.IsChecked ?? true;
            Program._enable_nms_interface = _p2.IsChecked ?? true;

            Program._db_connection_string = _p7.Text;
            Program._log_directory = _p3.Text;
            Program._image_directory = _p4.Text;
            Program._terminal_expired_for_unlocated =  System.Convert.ToInt32(_p5.Text);
            Program._terminal_expired_for_remove = System.Convert.ToInt32(_p6.Text);
            Program._ipm_interval_seconds = System.Convert.ToInt32(_p8.Text);
            Program._swn_repeat_minute = System.Convert.ToInt32(_p9.Text);
            
            pg.save();
    
        }

        private void _no_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ConfigWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Program pg = new Program();
            pg.Init();

            _cboLanguage.SelectedIndex = Program._lang_id == 1080001 ? 0 : 1;
            _p1.Text = Program._nms_server_ip_addr;
            _p2.IsChecked = Program._enable_nms_interface;
            _p10.IsChecked = Program._enable_ftp_interface;
            _p11.IsChecked = Program._enable_eb_interface;
            _p7.Text = Program._db_connection_string;
            _p3.Text = Program._log_directory;
            _p4.Text = Program._image_directory;
            _p5.Text = Program._terminal_expired_for_unlocated.ToString();
            _p6.Text = Program._terminal_expired_for_remove.ToString();
            _p8.Text = Program._ipm_interval_seconds.ToString();
            _p9.Text = Program._swn_repeat_minute.ToString();
        }

        private void _mail_Click(object sender, RoutedEventArgs e)
        {
            try { 
                ini_mailsms();
            }
            catch(Exception e1)
            {
                MessageBox.Show("Write error server configuration.");

            }
            SendManager window = new SendManager();
            window.Owner = this;
            window.ShowDialog();
        }

        private void ini_mailsms()
        {
        }
    }

    public class Program
    {
        // 아래 설정은 webapi_config.xml에 들어갈 default 값으로 초기 설치 후 실행 시 아래의 값으로 설정된다.
        public static bool _enable_ftp_interface = false;                   // ftp 서버와 인터페이스 여부
        public static bool _enable_eb_interface = false;                    // eb 서버와 인터페이스 여부
        public static bool _enable_nms_interface = false;                    // nms 서버와 인터페이스 여부
        public static string _nms_server_ip_addr = "165.244.44.169";        // 조과장...개발PC
        public static int _lang_id = 1080001;                               // korea
        public static double _terminal_expired_for_unlocated = 5;           // 5분 (테스트용)
        public static double _terminal_expired_for_remove = 10;             // 10분 (테스트용)
        public static string _image_directory = "C:/SimpleWinData/images";
        public static string _log_directory = "C:/SimpleWinData/log";
        public static string _fw_directory = "C:/SimpleWinData/images/firmware";
        public static string _db_connection_string = "data source=YOUNG-DESKTOP;initial catalog=i2ms2;persist security info=True;user id=i2ms2;password=i2ms2;MultipleActiveResultSets=True;App=EntityFramework";
        public static string _db_connection_string2 = "";
        public static double _ipm_interval_seconds = 180;                    // 180초 (IPM 스케쥴타임)
        public static int _swn_repeat_minute = 30;                        // 30분 (Net Scan 스케쥴타임)

        // 메일 , SMS
        public static int mail_use = 0;             // 메일서버 사용 여부
        public static string mail_server = "smtp.gmail.com";           // 메일 서버
        public static int mail_port = 587;                // 포트
        public static string mail_id = "gmailid@gmail.com";               // 아이디
        public static string mail_pw = "00998811a";               // 패스

        public static int sms_use = 0;             // 메일서버 사용 여부
        public static string sms_server = "http://sms.nicesms.co.kr/cpsms_utf8/cpsms.aspx";           // 메일 서버
        public static string sms_id = "lstest";               // 아이디
        public static string sms_pw = "lscns2";               // 패스

        public void Init()
        {
            // 설정 읽어오기 
            Config config = new Config();
            bool b0 = config.start();
            if (!b0)
            {
                Console.WriteLine("Can't read config.xml");
                Thread.Sleep(3000);
                return;
            }
        }

        public void save()
        {
            // 설정 읽어오기 
            Config config = new Config();
            bool b0 = config.reConfig();
            if (!b0)
            {
                Thread.Sleep(3000);
                MessageBox.Show("Write error server configuration.");
                return;
            }
            MessageBox.Show("Compleated server configuration.");
        }
    }
}
