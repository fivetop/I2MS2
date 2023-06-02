using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;
using System.Collections.Specialized;
using System.Xml.Linq;
using System.IO;

namespace ConfigS
{

    public class Program
    {
        // 아래 설정은 webapi_config.xml에 들어갈 default 값으로 초기 설치 후 실행 시 아래의 값으로 설정된다.
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
        public static double _ipm_interval_seconds = 20;                    // 20초 (IPM 스케쥴타임)


        public void Init()
        {
            Console.WriteLine(" ");
            Console.WriteLine("      Start I2MS Web Application Server");
            Console.WriteLine("      Copyright(C) 2014 LS Cable&System Co, LTD.");
            Console.WriteLine(" ");
            Console.WriteLine(" ");

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

    }
}
