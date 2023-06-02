using System;
using System.Collections.Generic;
using System.Windows;
using WebApi.Models;
using I2MS2.Translation;
using System.Windows.Media;
using WebApiClient;
using I2MS2.Library;
using I2MS2.Pages;
using I2MS2.Windows;
using System.Windows.Input;

namespace I2MS2.Models
{
    public static class g
    {
        // 서버 관련 변수
        public static string host_string = "localhost"; 
        public static string web_api_uri_string = "http://" + host_string + ":5000/";
        public static string signalr_uri_string = "http://" + host_string + ":5100/";

        public static WebApiClientClass webapi = null;
        public static SignalRClientClass2 signalr = null;
        //public static event RoutedEventHandler signalr_received;   // 메시지 도착 시 이벤트 발생

        public const string NULL_FILE_PATH = "/I2MS2;component/Images/null.png";
        public const string ALARM_FILE_PATH = "/I2MS2;component/Icons/port_alarm_16.png";
        public static string CLIENT_IMAGE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/LSCable/SimpleWin/Images/";
        public const string SERVER_SUB_DIR_MAP = "map";
        public const string SERVER_SUB_DIR_SITE = "site";

        public const int TIME_TICK = 100;
        public const int MAX_COL = 21;
        public const int CENTER_COL = MAX_COL / 2;
        public const int MAX_ROW = 24;
        //public const int MAX_CELL = MAX_COL * MAX_ROW;
        public const int VIRTUAL_HUB_CATALOG_ID = 434001;

       
        // 1. location 관련 테이블
        public static List<region1> region1_list;
        public static List<region2> region2_list;
        public static List<site> site_list;
        public static List<building> building_list;
        public static List<floor> floor_list;
        public static List<room> room_list;
        public static List<rack> rack_list;
        public static List<rack_config> rack_config_list;
        public static List<user> user_list;
        public static List<site_user> site_user_list;
        public static List<location> location_list;
        public static List<asset_tree> asset_tree_list;
        public static List<favorite_tree> favorite_tree_list;

        // 2. asset 관련 테이블
        public static List<asset> asset_list;
        public static List<asset_aux> asset_aux_list;
        public static List<sw_card_config> sw_card_config_list;
        public static List<ic_connect_status> ic_connect_status_list;
        public static List<ipp_connect_status> ipp_connect_status_list;
        public static List<ic_ipp_config> ic_ipp_config_list;
        public static List<asset_ext> asset_ext_list;
        public static List<asset_ipp_port_link> asset_ipp_port_link_list;
        public static List<asset_port_link> asset_port_link_list;
        public static List<user_port_layout> user_port_layout_list;
        public static List<work_order> work_order_list;
        public static List<work_order_task> work_order_task_list;
        public static List<changed_link_hist> changed_link_hist_list;
        public static List<changed_link_hist_cell> changed_link_hist_cell_list;
        public static List<eb_port_config> eb_port_config_list;
        public static List<eb_port_data_cur> eb_port_data_cur_list;

        // 3. catalog 관련 테이블
        public static List<catalog> catalog_list;
        public static List<catalog_ext> catalog_ext_list;
        public static List<catalog_group> catalog_group_list;
        public static List<manufacture> manufacture_list;
        public static List<contact> contact_list;
        public static List<ext_property> ext_property_list;
        public static List<ext_property_ans> ext_property_ans_list;

        // 4. image 관련 테이블
        public static List<image> image_list;
        public static List<image_type> image_type_list;
        public static List<drawing_3d> drawing_3d_list;
        public static List<sp_list_image_Result> sp_image_list;

        // 5. report & event 관련 테이블
        public static List<report> report_list;
        public static List<report_lang> report_lang_list;
        public static List<report_lang_column> report_lang_column_list;
        public static List<@event> event_list;
        public static List<event_lang> event_lang_list;
        public static List<event_hist> event_hist_list;
        public static List<template> template_list;
        public static List<template_column> template_column_list;
        public static List<language> language_list;

        // 6. terminal 관련 테이블
        public static List<asset_terminal> asset_terminal_list;
        public static List<asset_terminal_ip> asset_terminal_ip_list;
        public static List<net_scan> net_scan_list;
        public static List<net_scan_sw> net_scan_sw_list;
        public static List<net_scan_scheduler> net_scan_scheduler_list;

        // 7. 기타 테이블
        public static List<fw_upgrade> fw_upgrade_list;
        public static List<fw_upgrade_hist> fw_upgrade_hist_list;
        public static List<update_func> update_func_list;
        public static List<code> code_list;

        // 8. Rack Manager
        public static List<site_environment> site_environment_list;

        
        // 8-1. AssetTree를 treeview로 표시하기 위한 뷰모델 리스트
        public static List<AssetTreeVM> asset_tree_vm_list = new List<AssetTreeVM>(); // 나라가 들어 있는 루트 노드 region1  ?? romee
        //location 들만 검색 가능하다!!!!!
        public static Dictionary<int, AssetTreeVM> location_ast_vm_dic = new Dictionary<int, AssetTreeVM>();    // 로케이션 트리 검색을 위해 사용
        public static Dictionary<int, AssetTreeVM> asset_ast_vm_dic = new Dictionary<int, AssetTreeVM>();       // 엣세 트리 검색을 위해 사용
        public static Dictionary<int, AssetTreeVM> ic_ast_vm_dic = new Dictionary<int, AssetTreeVM>();          // 지능형 트리 검색을 위해 사용 
        
        // 9. UI 관련 변수
        public static Size screen_resolution;
        public static Size default_map_size = new Size(1600,1000);

        public static int login_user_id = 90002;     // 로긴 ID
        //public static int lang_id = 1080002;        // 1080001=한국어, 1080002=영어
        public static int lang_id = 1080001;        // 1080001=한국어, 1080002=영어
        // public static int selected_site_id = 79300119;
        public static int selected_site_id = 0;
        public static List<EventVM> popup_event_list = new List<EventVM>();
        public static MainWindow main_window = null;
        public static ICFwUpgradeManagerApply fw_apply_window = null;
        public static bool work_order_progressing = false;          // 작업지시가 동작 중일 때 true (중복 작업지시 차단 목적)
        public static int wo_id = 0;                                // 작업 지시 취소 시 필요
        public static KeyEventArgs user_key = null;                 // 사용자 입력키 => 사용후 널 처리요망 romee/1/20
        //MyProp my_prop = new MyProp(); 

        //11. 속성창을 위한 데이터
        public static PropertyData prop_data = new PropertyData();
        //=> 속성을 위해서 선택된 정보
        public static region1 select_region1;
        public static region2 select_region2;
        public static site select_site;
        public static building select_building;
        public static floor select_floor;
        public static room select_room;
        public static rack select_rack;
        //
        //public static string selected_site_name = "AAA 사이트";
        public static int selected_building_id = -1;

        // 자산 등록에서 사용하는 임시 변수
        public static int result_asset_id = 0;

        // 이미지 선택에서 사용하는 임시 변수
        public static int result_image_id = 0;
        public static int result_drawing_3d_id = 0;


        //3d 도면 2d 도면시 사용되는 값=> 변경가능
        //public static Double RACK_SIZE_WIDTH = 1000;
        //public static Double RACK_SIZE_HEIGHT = 2000;
        public static Double RACK_SIZE_WIDTH = 2000;
        public static Double RACK_SIZE_HEIGHT = 1000;
        public static Double RACK_HEIGHT = 2000;

        public static Double ASSET_SIZE_WIDTH = 1000;
        public static Double ASSET_SIZE_HEIGHT = 1000;
        public static Double ASSET_HEIGHT = 300;

        public static Double USERPORT_RADIUS = 500;
        public static Double USERPORT_HEIGHT = 200;


        //기본 값이다
        public static Double DEF_RACK_SIZE_WIDTH = 2000;
        public static Double DEF_RACK_SIZE_HEIGHT = 1000;
        public static Double DEF_RACK_HEIGHT = 2000;

        public static Double DEF_ASSET_SIZE_WIDTH = 1000;
        public static Double DEF_ASSET_SIZE_HEIGHT = 1000;
        public static Double DEF_ASSET_HEIGHT = 300;

        public static Double DEF_USERPORT_RADIUS = 250;
        public static Double DEF_USERPORT_HEIGHT = 150;

        //도면 색상 설정
        public static Color RACK_COLOR = Color.FromArgb(0xFF, 0x5C, 0xD1, 0xE5);
        //public static Color RACK_COLOR = Color.FromArgb(0xFF, 0x00, 0xa2, 0xa3);
        public static Color MB_COLOR = Color.FromArgb(0xFF, 0x84, 0xDB, 0x85);
        public static Color CP_COLOR = Color.FromArgb(0xFF, 0x84, 0xDB, 0x85);
        public static Color FP_COLOR = Color.FromArgb(0xFF, 0x84, 0xDB, 0x85);
        //public static Color USERPORT_COLOR = Color.FromArgb(0xFF, 0xB7, 0x98, 0x6f);
        public static Color USERPORT_COLOR = Color.FromArgb(0xFF, 0xBD, 0xBD, 0xBD);
        public static Color USERPORT_LINE_COLOR = Color.FromArgb(0xFF, 0x5D, 0x5D, 0x5D);


        
        // 빌딩/층/룸/랙 추가 시 사용하는 임시 변수
        //public static int result_location_id = 0;
        //public static int result_building_id = 0;
        //public static int result_floor_id = 0;
        //public static int result_room_id = 0;
        //public static int result_rack_id = 0;

        public static bool reload = false;      // 로긴화면부터 다시 진입
        public static LeftTreeHandler left_tree_handler = new LeftTreeHandler();

        public static P5LineManager _P5LineManager = null;
        public static P4AssetView _P4AssetView = null;
        public static P2DashBoard _P2DashBoard = null;


        // 메일 , SMS
        public static int mail_use = 0;             // 메일서버 사용 여부
        public static string mail_server = "smtp.gmail.com";           // 메일 서버            // https://myaccount.google.com/security#connectedapps 보안수준이 낮은앱 허용 
        public static int mail_port = 587;                // 포트
        public static string mail_id = "my@gmailypassword";               // 아이디
        public static string mail_pw = "gmailypassword";               // 패스

        public static int sms_use= 0;             // 메일서버 사용 여부
        public static string sms_server = "http://sms.nicesms.co.kr/cpsms_utf8/cpsms.aspx";           // 메일 서버
        public static string sms_id = "lstest";               // 아이디
        public static string sms_pw = "lscns2";               // 패스

        public static string tr_get(string key)
        {
            try
            {
                string str = (string) TranslationManager.Instance.Translate(key);
                return str;
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception error. code={0}, message={1}", e.HResult, e.Message);
            }
            return null;
        }


    }


}
