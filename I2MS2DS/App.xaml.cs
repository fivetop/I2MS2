using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using WebApiClient;
using I2MS2.Models;
using I2MS2.Translation;
using System.Reflection;
using I2MS2.Library;
using I2MS2.Windows;
using WebApi.Models;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;

namespace I2MS2
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>




    public partial class App : Application
    {
        // 로긴 후 진행률을 표시하기 위한 다이알로그
        ProgressBarDialog4 _progress = null;

        public App()
        {
            // 가장 먼저 시작되는 곳...
            StartApp();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            // 두 번쨰로 시작되는 곳...
            base.OnStartup(e);
        }

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            // 세 번쨰로 시작되는 곳...

            //Disable shutdown when the dialog closes
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            Login win = new Login();
            bool ret = (bool) win.ShowDialog();
            if (ret)
            {
                // 로긴 후에 데이터 준비 기간 동안 프로그래스 바를 보여준다.
                _progress = new ProgressBarDialog4();
                _progress.setPercent(1);
                _progress.setStatus("Trying to load data...");
                _progress.Show();

                bool b = await init_data();
                if (!b)
                    Current.Shutdown(-1);
                _progress.Close();
                    
                var mainWindow = new MainWindow();
                //Re-enable normal shutdown mode.
                Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                Current.MainWindow = mainWindow;
                mainWindow.Show();
            }
            else
            {
                Current.Shutdown(-1);
            }
        }


        public void StartApp()
        {
            //if (!create_directory_when_not_exist())
            //{
            //    Console.WriteLine("Can't access data directory. Please check access right.");
            //    Thread.Sleep(5);
            //    return;
            //}

            // 기본(대표) 리소스 화일명을 등록
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            TranslationManager.Instance.TranslationProvider = new ResxTranslationProvider("I2MSR.Properties.Resources", typeof(I2MSR.Properties.Resources).Assembly);

            // 이 아래 처리 루틴 들은 대부분 서브 함수들을 호출하는 것으로 동작시킴.

            //Thread.Sleep(3000);   // 3초
            g.webapi = new WebApiClientClass();

            //--------------------------------------------------------
            // 다국어 지원
            //--------------------------------------------------------
            MainWindowViewModel vm = new MainWindowViewModel();

            TranslationManager.set_lang(g.lang_id);
            //if (g.lang_id == 1080001)
            //    TranslationManager.Instance.CurrentLanguage = new CultureInfo("ko-KR");
            //else
            //    TranslationManager.Instance.CurrentLanguage = new CultureInfo("en-US");
            //--------------------------------------------------------
            // 스플래쉬 화면....
            //--------------------------------------------------------
            // SplashScreen splashScreen = new SplashScreen("Images/LSlogo.jpg");
            // splashScreen.Show(true);

            //--------------------------------------------------------
            // 로긴 화면....
            //--------------------------------------------------------

            //--------------------------------------------------------
            // 데이터 작업
            //--------------------------------------------------------

            //int cnt = 0;

            //while (true)
            //{
            //    Debug.WriteLine(string.Format("Tryig user data from I2MS Web Application Server...try_cnt={0}", ++cnt));

            //    if (getUserData().Result)
            //        break;

            //    // 3초 쉬었다가 다시 시도... (3회 까지)
            //    Thread.Sleep(3000);
            //    if (cnt >= 3)
            //    {
            //        Console.WriteLine("Loading user data failed.");
            //        Thread.Sleep(5);
            //        return;
            //    }
            //}


        }


        static bool create_directory_when_not_exist()
        {
            string path = "";
            try
            {
                // 기존에 기획하였던 rack_880, link, icon_16, icon_32, icon_48, icon_64 디렉토리는 필요 없음.
                path = g.CLIENT_IMAGE_PATH;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = g.CLIENT_IMAGE_PATH + "/map";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = g.CLIENT_IMAGE_PATH + "/site";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = g.CLIENT_IMAGE_PATH + "/building";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = g.CLIENT_IMAGE_PATH + "/drawing";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = g.CLIENT_IMAGE_PATH + "/drawing_3d";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = g.CLIENT_IMAGE_PATH + "/catalog";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = g.CLIENT_IMAGE_PATH + "/rack_220";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = g.CLIENT_IMAGE_PATH + "/rack_440";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = g.CLIENT_IMAGE_PATH + "/etc";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Can't connect to database. code={0}, msg={1}", e.HResult, e.Message));
                return false;
            }
            return true;
        }

        public async Task<bool> init_data()
        {
            Type iii = await getData();
            if (_err_table != typeof(int))
            {
                Console.WriteLine("Loading data failed. table={0}.", _err_table);
                return false;
            }
        
            Debug.WriteLine("Data load successed from I2MS Web Application Server...");
            _progress.setStatus("Loading 3d drawings from server...");

            //--------------------------------------------------------
            // 3D 도면 파일 다운로드(sync)
            //--------------------------------------------------------
            await get3DFile();
            _progress.setPercent(75);

            _progress.setStatus("Loading image files from server...");
            //--------------------------------------------------------
            // 이미지 다운로드 (sync)
            //--------------------------------------------------------
            await getImage();
            _progress.setPercent(100);
            _progress.setStatus("Completed downloading.");

            //--------------------------------------------------------
            // UI 준비
            //--------------------------------------------------------

            //1. 프로그램 화면 사이즈 결정
            double X = SystemParameters.PrimaryScreenWidth;
            double Y = SystemParameters.PrimaryScreenHeight;

            if ((X >= 1920) && (Y >= 1080))
                g.screen_resolution = new Size(1600, 1024);
            else
                g.screen_resolution = new Size(1280, 960);
            //g.screen_resolution = new Size(1280, 1024);
            //Console.WriteLine("=== monitor resolution ({0},{1})===========", X, Y);
            Console.WriteLine("=== screen resolution ({0},{1})===========", g.screen_resolution.Width, g.screen_resolution.Height);

            //2. view_option값을 읽어서 처리
            int ratio = Reg.get_view_option();
            if (ratio > 0)
            {
                g.RACK_SIZE_HEIGHT = g.DEF_RACK_SIZE_HEIGHT / ratio;
                g.RACK_SIZE_WIDTH = g.DEF_RACK_SIZE_WIDTH / ratio;
                g.RACK_HEIGHT = g.DEF_RACK_HEIGHT / ratio;

                g.ASSET_HEIGHT = g.DEF_ASSET_HEIGHT / ratio;
                g.ASSET_SIZE_HEIGHT = g.DEF_ASSET_SIZE_HEIGHT / ratio;
                g.ASSET_SIZE_WIDTH = g.DEF_ASSET_SIZE_WIDTH / ratio;

                g.USERPORT_HEIGHT = g.DEF_USERPORT_HEIGHT / ratio;
                g.USERPORT_RADIUS = g.DEF_USERPORT_RADIUS / ratio;
            }
            //--------------------------------------------------------
            // SignalR
            //--------------------------------------------------------

            //Thread.Sleep(5000);

            g.signalr = new SignalRClientClass2();

            return true;
        }

        private async Task get3DFile()
        {
            const int DOWNLOAD_BUFF_SIZE = 40960;

            //폴더가 존재하는지 확인 하고 없으면 생성
            string client_path = string.Format("{0}drawing_3d/", g.CLIENT_IMAGE_PATH);
            if (Directory.Exists(client_path) == false)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(client_path));
            }

            if (g.drawing_3d_list == null)
                return;

            foreach(var at in g.drawing_3d_list)
            {
                //파일 존재 확인
                string file_path = string.Format("{0}drawing_3d/{1}", g.CLIENT_IMAGE_PATH, at.file_name);
                Boolean ret = File.Exists(file_path);
                if (ret == false)
                {
                    //파일을 다운로드 한다
                    Task<int> t1 = g.webapi.downloadFile("drawing_3d", at.file_name, client_path, DOWNLOAD_BUFF_SIZE);
                    int rr = await t1;

                    //결과를 콘솔로 출력한다(추후 로그형태로 변경)
                    if (rr == 0)
                        Console.WriteLine("OK: drawing_3d/{0} download", at.file_name);
                    else
                        Console.WriteLine("ERR: drawing_3d/{0} download fail!",  at.file_name);
                }
            }
        }
        
        private async Task getImage()
        {
            const int DOWNLOAD_BUFF_SIZE = 40960;

            
            //if (g.sp_image_list == null)
            //    return;
            foreach(var at in g.sp_image_list)
            {
                //파일이 존재하는지 확인
                string img_path = string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH, at.folder_name, at.file_name);
                Boolean ret = File.Exists(img_path);
                if(ret==false)
                {
                    //폴더가 존재하는지 확인 하고 없으면 생성
                    string client_path = string.Format("{0}{1}/", g.CLIENT_IMAGE_PATH, at.folder_name);
                    if(Directory.Exists(client_path)==false)
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(client_path));
                    }
                    
                    //파일을 다운로드 한다
                    Task<int> t1 = g.webapi.downloadFile(at.folder_name, at.file_name, client_path, DOWNLOAD_BUFF_SIZE);
                    int rr = await t1;

                    //결과를 콘솔로 출력한다(추후 로그형태로 변경)
                    if(rr==0)
                        Console.WriteLine("OK: {0}/{1} download", at.folder_name, at.file_name);
                    else
                        Console.WriteLine("ERR: {0}/{1} download fail!", at.folder_name, at.file_name);
                }
                
            }
        }


        private async Task<bool> getUserData()
        {
            g.user_list = (List<user>)await g.webapi.getList("user", typeof(List<user>));
            return g.user_list != null;
        }

        Type _err_table;
        private async Task<Type> getData()
        {
            int percentage = 1;
            // 1. location 관련 테이블
            g.region1_list = (List<region1>)await g.webapi.getList("region1", typeof(List<region1>));
            if (g.region1_list == null) { _err_table = typeof(region1); return typeof(region1); }
            _progress.setPercent(++percentage);
            _progress.setStatus("Loading database tables from server...");
            g.region2_list = (List<region2>)await g.webapi.getList("region2", typeof(List<region2>));
            _progress.setPercent(++percentage);
            g.site_list = (List<site>)await g.webapi.getList("site", typeof(List<site>));
            _progress.setPercent(++percentage);
            g.building_list = (List<building>)await g.webapi.getList("building", typeof(List<building>));
            _progress.setPercent(++percentage);
            g.floor_list = (List<floor>)await g.webapi.getList("floor", typeof(List<floor>));
            _progress.setPercent(++percentage);
            g.room_list = (List<room>)await g.webapi.getList("room", typeof(List<room>));
            _progress.setPercent(++percentage);
            g.rack_list = (List<rack>)await g.webapi.getList("rack", typeof(List<rack>));
            _progress.setPercent(++percentage);
            g.rack_config_list = (List<rack_config>)await g.webapi.getList("rack_config", typeof(List<rack_config>));
            _progress.setPercent(++percentage);
            g.user_list = (List<user>)await g.webapi.getList("user", typeof(List<user>));
            _progress.setPercent(++percentage);
            g.site_user_list = (List<site_user>)await g.webapi.getList("site_user", typeof(List<site_user>));
            _progress.setPercent(++percentage);
            g.location_list = (List<location>)await g.webapi.getList("location", typeof(List<location>));
            _progress.setPercent(++percentage);
            g.asset_tree_list = (List<asset_tree>)await g.webapi.getList("asset_tree", typeof(List<asset_tree>));
            _progress.setPercent(++percentage);
            g.favorite_tree_list = (List<favorite_tree>)await g.webapi.getList("favorite_tree", typeof(List<favorite_tree>));
            if (g.favorite_tree_list == null) { _err_table = typeof(favorite_tree); return typeof(favorite_tree); }
            _progress.setPercent(++percentage);

            // 2. asset 관련 테이블
            g.asset_list = (List<asset>) await g.webapi.getList("asset", typeof(List<asset>));
            _progress.setPercent(++percentage);
            g.asset_aux_list = (List<asset_aux>)await g.webapi.getList("asset_aux", typeof(List<asset_aux>));
            _progress.setPercent(++percentage);
            g.sw_card_config_list = (List<sw_card_config>) await g.webapi.getList("sw_card_config", typeof(List<sw_card_config>));
            _progress.setPercent(++percentage);
            g.ic_connect_status_list = (List<ic_connect_status>)await g.webapi.getList("ic_connect_status", typeof(List<ic_connect_status>));
            _progress.setPercent(++percentage);
            g.ipp_connect_status_list = (List<ipp_connect_status>)await g.webapi.getList("ipp_connect_status", typeof(List<ipp_connect_status>));
            _progress.setPercent(++percentage);
            g.ic_ipp_config_list = (List<ic_ipp_config>)await g.webapi.getList("ic_ipp_config", typeof(List<ic_ipp_config>));
            _progress.setPercent(++percentage);
            g.asset_ext_list = (List<asset_ext>)await g.webapi.getList("asset_ext", typeof(List<asset_ext>));
            _progress.setPercent(++percentage);
            g.asset_ipp_port_link_list = (List<asset_ipp_port_link>)await g.webapi.getList("asset_ipp_port_link", typeof(List<asset_ipp_port_link>));
            _progress.setPercent(++percentage);
            g.asset_port_link_list = (List<asset_port_link>)await g.webapi.getList("asset_port_link", typeof(List<asset_port_link>));
            _progress.setPercent(++percentage);
            g.user_port_layout_list = (List<user_port_layout>)await g.webapi.getList("user_port_layout", typeof(List<user_port_layout>));
            _progress.setPercent(++percentage);
            g.work_order_list = (List<work_order>)await g.webapi.getList("work_order", typeof(List<work_order>));
            _progress.setPercent(++percentage);
            g.work_order_task_list = (List<work_order_task>)await g.webapi.getList("work_order_task", typeof(List<work_order_task>));
            _progress.setPercent(++percentage);
            g.changed_link_hist_list = (List<changed_link_hist>)await g.webapi.getList("changed_link_hist", typeof(List<changed_link_hist>));
            _progress.setPercent(++percentage);
            g.changed_link_hist_cell_list = (List<changed_link_hist_cell>)await g.webapi.getList("changed_link_hist_cell", typeof(List<changed_link_hist_cell>));
            _progress.setPercent(++percentage);
            g.eb_port_config_list = (List<eb_port_config>)await g.webapi.getList("eb_port_config", typeof(List<eb_port_config>));
            _progress.setPercent(++percentage);
            g.eb_port_data_cur_list = (List<eb_port_data_cur>)await g.webapi.getList("eb_port_data_cur", typeof(List<eb_port_data_cur>));
            _progress.setPercent(++percentage);

            // 3. catalog 관련 테이블
            g.catalog_list = (List<catalog>) await g.webapi.getList("catalog", typeof(List<catalog>));
            _progress.setPercent(++percentage);
            g.catalog_ext_list = (List<catalog_ext>)await g.webapi.getList("catalog_ext", typeof(List<catalog_ext>));
            _progress.setPercent(++percentage);
            g.catalog_group_list = (List<catalog_group>)await g.webapi.getList("catalog_group", typeof(List<catalog_group>));
            _progress.setPercent(++percentage);
            g.manufacture_list = (List<manufacture>)await g.webapi.getList("manufacture", typeof(List<manufacture>));
            _progress.setPercent(++percentage);
            g.contact_list = (List<contact>)await g.webapi.getList("contact", typeof(List<contact>));
            _progress.setPercent(++percentage);
            g.ext_property_list = (List<ext_property>)await g.webapi.getList("ext_property", typeof(List<ext_property>));
            _progress.setPercent(++percentage);
            g.ext_property_ans_list = (List<ext_property_ans>)await g.webapi.getList("ext_property_ans", typeof(List<ext_property_ans>));
            _progress.setPercent(++percentage);

            // 4. image 관련 테이블
            g.image_list = (List<image>) await g.webapi.getList("image", typeof(List<image>));
            _progress.setPercent(++percentage);
            g.image_type_list = (List<image_type>)await g.webapi.getList("image_type", typeof(List<image_type>));
            _progress.setPercent(++percentage);
            g.drawing_3d_list = (List<drawing_3d>)await g.webapi.getList("drawing_3d", typeof(List<drawing_3d>));
            _progress.setPercent(++percentage);
            g.sp_image_list = (List<sp_list_image_Result>)await g.webapi.getList("sp_list_image", typeof(List<sp_list_image_Result>));
            _progress.setPercent(++percentage);

            // 5. report & event 관련 테이블
            g.report_list = (List<report>) await g.webapi.getList("report", typeof(List<report>));
            _progress.setPercent(++percentage);
            g.report_lang_list = (List<report_lang>)await g.webapi.getList("report_lang", typeof(List<report_lang>));
            _progress.setPercent(++percentage);
            g.report_lang_column_list = (List<report_lang_column>)await g.webapi.getList("report_lang_column", typeof(List<report_lang_column>));
            _progress.setPercent(++percentage);
            g.event_list = (List<@event>)await g.webapi.getList("event", typeof(List<@event>));
            _progress.setPercent(++percentage);
            g.event_lang_list = (List<event_lang>)await g.webapi.getList("event_lang", typeof(List<event_lang>));
            _progress.setPercent(++percentage);
            g.event_hist_list = (List<event_hist>)await g.webapi.getList("event_hist", typeof(List<event_hist>));
            _progress.setPercent(++percentage);
            g.template_list = (List<template>)await g.webapi.getList("template", typeof(List<template>));
            _progress.setPercent(++percentage);
            g.template_column_list = (List<template_column>)await g.webapi.getList("template_column", typeof(List<template_column>));
            _progress.setPercent(++percentage);
            g.language_list = (List<language>)await g.webapi.getList("language", typeof(List<language>));
            _progress.setPercent(++percentage);

            // 6. terminal 관련 테이블
            g.asset_terminal_list = (List<asset_terminal>) await g.webapi.getList("asset_terminal", typeof(List<asset_terminal>));
            _progress.setPercent(++percentage);
            g.asset_terminal_ip_list = (List<asset_terminal_ip>)await g.webapi.getList("asset_terminal_ip", typeof(List<asset_terminal_ip>));
            _progress.setPercent(++percentage);
            g.net_scan_list = (List<net_scan>)await g.webapi.getList("net_scan", typeof(List<net_scan>));
            _progress.setPercent(++percentage);
            g.net_scan_sw_list = (List<net_scan_sw>)await g.webapi.getList("net_scan_sw", typeof(List<net_scan_sw>));
            _progress.setPercent(++percentage);
            g.net_scan_scheduler_list = (List<net_scan_scheduler>)await g.webapi.getList("net_scan_scheduler", typeof(List<net_scan_scheduler>));
            _progress.setPercent(++percentage);

            // 7. 기타 테이블
            g.fw_upgrade_list = (List<fw_upgrade>) await g.webapi.getList("fw_upgrade", typeof(List<fw_upgrade>));
            _progress.setPercent(++percentage);
            g.fw_upgrade_hist_list = (List<fw_upgrade_hist>)await g.webapi.getList("fw_upgrade_hist", typeof(List<fw_upgrade_hist>));
            _progress.setPercent(++percentage);
            g.update_func_list = (List<update_func>)await g.webapi.getList("update_func", typeof(List<update_func>));
            _progress.setPercent(++percentage);
            g.code_list = (List<code>)await g.webapi.getList("code", typeof(List<code>));
            _progress.setPercent(++percentage);

            // 8. Rack Manager
            g.site_environment_list = (List<site_environment>)await g.webapi.getList("site_environment", typeof(List<site_environment>));
            _progress.setPercent(++percentage);

            _err_table = typeof(int);       // success

           
            
            return _err_table;        
        }

        private async void test1()
        {
            // 리스트 조회
            List<region1> r1 = (List<region1>) await g.webapi.getList("region1", typeof(List<region1>));
            List<image> r2 = (List<image>)await g.webapi.getList("image", typeof(List<image>));

            // 개별 조회
            region1 r3 = (region1) await g.webapi.get("region1", 79100001, typeof(region1));
            image r4 = (image) await g.webapi.get("image", 2010001, typeof(image));

            // 추가
            region1 rr = new region1() { region1_name = "test", pos_x = 100, pos_y = 200, region1_image_id = 1, user_id = 2  };
            region1 r5 = (region1) await g.webapi.post("region1", rr, typeof(region1));

            // 수정
            r5.pos_x = 300;
            int r6 = await g.webapi.put("region1", r5.region1_id, r5, typeof(region1));

            // 삭제
            int r7 = await g.webapi.delete("region1", r5.region1_id);
        }

    }
}
