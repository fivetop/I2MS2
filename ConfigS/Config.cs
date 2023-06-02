using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebApi.Models;

namespace ConfigS
{
    // 시스템 설정 처리 
    public class ConfigData
    {
        public string lang_code;                            // 이벤트 로그 생성 시 사용
        public double terminal_expired_for_unlocated;       // 단말이 계속 접속을 하지 않을 때 unlocated로 옮겨지는 시간(분단위)
        public double terminal_expired_for_remove;          // 단말이 계속 접속을 하지 않을 때 unlocated에서 삭제하는 시간(분단위)
        public string image_directory;                      // 이미지 화일이 저장되는 위치
        public string log_directory;                        // 로그 화일이 저장되는 위치
        public string fw_directory;                         // 펌웨어 디렉토리
        public string db_connection_string;                 // DB 서버 연결문자열
        public bool enable_ftp_interface;                   // ftp 서버와의 인터페이스 여부
        public bool enable_eb_interface;                    // eb 서버와의 인터페이스 여부
        public bool enable_nms_interface;                   // NMS 서버와의 인터페이스 여부
        public string nms_server_ip_addr;                   // NMS 서버 IP 주소
        public double enviroment_duration;                    // 180초 (IPM 스케쥴타임)
        public int net_scan_interval;                    // 30분 (Net Scan 스케쥴타임)
        public int mail_use;                // 메일서버 사용 여부
        public string mail_server;          // 메일 서버
        public int mail_port;               // 포트
        public string mail_id;              // 아이디
        public string mail_pw;              // 패스
        public int sms_use = 0;             // 메일서버 사용 여부
        public string sms_server;           // 메일 서버
        public string sms_id;               // 아이디
        public string sms_pw;               // 패스    
    }

    // XNL data 를 읽어와서 메모리 저장 
    // 파일이 없을 경우 생성 처리 
    public class Config
    {
        private static List<ConfigData> _config;
        private const string _config_file_name = "webapi_config.xml";

        public Config()
        {
            _config = new List<ConfigData>();

            XmlSerializer serializer = new XmlSerializer(_config.GetType(), new XmlRootAttribute("Config"));

            if (File.Exists(_config_file_name))
            {
                using (StreamReader r = new StreamReader(_config_file_name))
                {
                    _config = serializer.Deserialize(r) as List<ConfigData>;
                    r.Close();
                }
            }
            else
            {
                _config.Add(new ConfigData
                {
                    lang_code = Program._lang_id == 1080001 ? "ko" : "en", 
                    terminal_expired_for_unlocated = Program._terminal_expired_for_unlocated,
                    terminal_expired_for_remove = Program._terminal_expired_for_remove,
                    image_directory = Program._image_directory,
                    log_directory = Program._log_directory,
                    fw_directory = Program._fw_directory,
                    enable_ftp_interface = Program._enable_ftp_interface,
                    enable_eb_interface = Program._enable_eb_interface,
                    enable_nms_interface = Program._enable_nms_interface,
                    nms_server_ip_addr = Program._nms_server_ip_addr,
                    db_connection_string = Program._db_connection_string,
                    enviroment_duration = Program._ipm_interval_seconds,
                    net_scan_interval = Program._swn_repeat_minute,
                    mail_use  = Program.mail_use ,
                    mail_server  = Program.mail_server ,
                    mail_port  = Program.mail_port ,
                    mail_id  = Program.mail_id ,
                    mail_pw  = DESEncrypt.EncryptAES(Program.mail_pw),
                    sms_use  = Program.sms_use ,
                    sms_server  = Program.sms_server ,
                    sms_id  = Program.sms_id ,
                    sms_pw = DESEncrypt.EncryptAES(Program.sms_pw),
                });

                using (StreamWriter w = new StreamWriter(_config_file_name))
                {
                    serializer.Serialize(w, _config);
                }
            }
        }

        public bool reConfig()
        {
            XmlSerializer serializer = new XmlSerializer(_config.GetType(), new XmlRootAttribute("Config"));
            _config.RemoveAt(0);

            _config.Add(new ConfigData
            {
                    lang_code = Program._lang_id == 1080001 ? "ko" : "en", 
                    terminal_expired_for_unlocated = Program._terminal_expired_for_unlocated,
                    terminal_expired_for_remove = Program._terminal_expired_for_remove,
                    image_directory = Program._image_directory,
                    log_directory = Program._log_directory,
                    fw_directory = Program._fw_directory,
                    enable_ftp_interface = Program._enable_ftp_interface,
                    enable_eb_interface = Program._enable_eb_interface,
                    enable_nms_interface = Program._enable_nms_interface,
                    nms_server_ip_addr = Program._nms_server_ip_addr,
                    db_connection_string = Program._db_connection_string,
                    enviroment_duration = Program._ipm_interval_seconds,
                    net_scan_interval = Program._swn_repeat_minute,
                    mail_use  = Program.mail_use ,
                    mail_server  = Program.mail_server ,
                    mail_port  = Program.mail_port ,
                    mail_id  = Program.mail_id ,
                    mail_pw  = DESEncrypt.EncryptAES(Program.mail_pw),
                    sms_use  = Program.sms_use ,
                    sms_server  = Program.sms_server ,
                    sms_id  = Program.sms_id ,
                    sms_pw = DESEncrypt.EncryptAES(Program.sms_pw),
            });

            try 
            {
                using (StreamWriter w1 = new StreamWriter(_config_file_name))
                {
                    serializer.Serialize(w1, _config);
                    w1.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception error: code={0}, msg={1}", e.HResult, e.Message);
                return false;
            }
            return true;
        }

        // 읽어온 config.xml을 전역 변수에 저장 
        public bool start()
        {
            if (_config == null)
                return false;
            if (_config.Count == 0)
                return false;
            var node = _config[0];
            Program._lang_id = node.lang_code == "ko" ? 1080001 : 1080002;    // GS_DEL
            Program._terminal_expired_for_unlocated = node.terminal_expired_for_unlocated;
            Program._terminal_expired_for_remove = node.terminal_expired_for_remove;
            Program._image_directory = node.image_directory;
            Program._log_directory = node.log_directory;
            // Program._fw_directory = node.fw_directory; 
            Program._enable_ftp_interface = node.enable_ftp_interface;
            Program._enable_eb_interface = node.enable_eb_interface;
            Program._enable_nms_interface = node.enable_nms_interface;
            Program._nms_server_ip_addr = node.nms_server_ip_addr;
            Program._db_connection_string = node.db_connection_string;                           
            Program._ipm_interval_seconds = node.enviroment_duration;
            if (Program._ipm_interval_seconds == 0)
                Program._ipm_interval_seconds = 180;
            Program._swn_repeat_minute = node.net_scan_interval;
            if (Program._swn_repeat_minute == 0)
                Program._swn_repeat_minute = 30;
            Program.mail_use = node.mail_use;
            Program.mail_server = node.mail_server;
            Program.mail_port = node.mail_port;
            Program.mail_id = node.mail_id;
            Program.mail_pw = DESEncrypt.DecryptAES(node.mail_pw);
            Program.sms_use = node.sms_use;
            Program.sms_server = node.sms_server;
            Program.sms_id = node.sms_id;
            Program.sms_pw = DESEncrypt.DecryptAES(node.sms_pw);
            return true;
        }
    }
}
