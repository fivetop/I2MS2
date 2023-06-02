using MetroChart;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using I2MS2.Models;
using I2MS2.Library;
using WebApi.Models;

namespace I2MS2.Chart
{
    #pragma warning disable 4014

    // bind this view model to your page or window (DataContext)
    public class DataViewModel : DependencyObject, INotifyPropertyChanged 
    {
        #region 변수 선언 
        bool Timer_on = false;
        List<int> _dashboard_time_list = new List<int>() { 60, 0 };       // 해당 판넬의 위치 처리 1,2,....
        public DispatcherTimer Timer = new DispatcherTimer();                           // 댓쉬보드타이머 

        ResourceDictionary resourceDictionary = new ResourceDictionary();               // 리소스 참조용 
        public Dictionary<string, MetroChart.ResourceDictionaryCollection> Palettes { get; set; }

        public ObservableCollection<ChartDataDouble> SystemPPStatus { get; set; }             // 패치패널 포트 사용 현황 
        public ObservableCollection<ChartDataDouble> SystemSwitchStatus { get; set; }         // 스위치 포트 사용 현황 
        public ObservableCollection<ChartDataDouble> SystemRackStatus { get; set; }           // 랙 유니트 현황 
        public ObservableCollection<ChartDataDouble> SystemTerminalStatus { get; set; }       // 단말 접속 현황 

        public ObservableCollection<ChartData> SystemEvent { get; set; }                // 알람 이벤트 정보  
        public ObservableCollection<ChartData> SystemEventEnvironment { get; set; }     // 알람 이벤트 정보  

        public ObservableCollection<ChartData> StateP { get; set; }                     // 전력 
        public ObservableCollection<ChartData> StateTmax { get; set; }                  // 온도 최대
        public ObservableCollection<ChartData> StateTmin { get; set; }                  // 온도 최소
        public ObservableCollection<ChartData> StateHmax { get; set; }                  // 습도 최대
        public ObservableCollection<ChartData> StateHmin { get; set; }                  // 습도 최소
        public ObservableCollection<ChartData> StateD { get; set; }                     // 도어 open / close
            
        public ObservableCollection<ChartData> StatPower { get; set; }                  // 전력 
        public ObservableCollection<ChartData> StatTemp { get; set; }                   // 온도
        public ObservableCollection<ChartData> StatHumi { get; set; }                   // 습도

        #endregion

        #region 통계 변수 선언
        // IPP 카운트 출력용, SW 카운트 출력용 , Rack 카운트 출력용 , 터미날 카운트 출력용 
        private SystemData systemdata;
        public SystemData Systemdata
        {
            get { return systemdata; }
            set { systemdata = value; NotifyPropertyChanged("Systemdata"); }
        }
        // 시간대별 터미날 접속 건수 
        private ObservableCollection<SeriesData> terminalseries;
        public ObservableCollection<SeriesData> TerminalSeries
        {
            get { return terminalseries; } set { terminalseries = value; NotifyPropertyChanged("TerminalSeries"); }
        }
        // 일별 터미날 접속 건수 
        private ObservableCollection<SeriesData> terminalseriesday;
        public ObservableCollection<SeriesData> TerminalSeriesDay
        {
            get { return terminalseriesday; } set { terminalseriesday = value; NotifyPropertyChanged("TerminalSeriesDay"); }
        }
        // 월별 터미날 접속 건수 
        private ObservableCollection<SeriesData> terminalseriesmonth;
        public ObservableCollection<SeriesData> TerminalSeriesMonth
        {
            get { return terminalseriesmonth; } set { terminalseriesmonth = value; NotifyPropertyChanged("TerminalSeriesMonth"); }
        }
        // 통계처리 시간대별 건수 
        private ObservableCollection<ChartData> stattotal;
        public ObservableCollection<ChartData> StatTotal
        {
            get { return stattotal; } set { stattotal = value; NotifyPropertyChanged("StatTotal"); }
        }
        private ObservableCollection<ChartData> statactive;
        public ObservableCollection<ChartData> StatActive
        {
            get { return statactive; } 
            set { statactive = value; NotifyPropertyChanged("StatActive"); }
        }

        private ObservableCollection<SysData> sysDataList;                // 이미지 
        public ObservableCollection<SysData> SysDataList
        {
            get { return sysDataList; }
            set { sysDataList = value; NotifyPropertyChanged("SysDataList"); }
        }

        // 현재 온도 / 상한 설정 / 하한 설정 
        private EnvData curTemp;                                            
        private EnvData curHumi;
        private EnvData curPower;
        private EnvData curDoor;
        public EnvData CurTemp { get { return curTemp; } set { curTemp = value; NotifyPropertyChanged("CurTemp"); } }
        public EnvData CurHumi { get { return curHumi; } set { curHumi = value; NotifyPropertyChanged("CurHumi"); } }
        public EnvData CurPower { get { return curPower; } set { curPower = value; NotifyPropertyChanged("CurPower"); } }
        public EnvData CurDoor { get { return curDoor; } set { curDoor = value; NotifyPropertyChanged("CurDoor"); } }

        #endregion

        #region 뎃쉬 보드 통계 처리
        // 통계 1 ~ 4 포트 사용현황 패치, 스위치, 랙공간 , 터미날 
        private async Task<bool> getDBList_Stat1234()
        {
            string sys1, sys2, sys3, sys4;
            int a1 = Stat.get_tot_pp_ports_by_site_id(g.selected_site_id);
            int a3 = Stat.get_used_pp_ports_by_site_id(g.selected_site_id);
            int a4 = Stat.get_used_ipp_ports_by_site_id(g.selected_site_id);
            int a5 = Stat.get_tot_user_ports_by_site_id(g.selected_site_id);
            int a6 = Stat.get_used_user_ports_by_site_id(g.selected_site_id);

            int t1 = Stat.get_tot_ipp_ports_by_site_id(g.selected_site_id, AssetTreeType.Site);
            int t2 = Stat.get_used_ipp_ports_linked_by_site_id(g.selected_site_id, AssetTreeType.Site);
            double t3 = t1;
            double t4 = t2;
            double a = Math.Round( (t4 / t3) * 100.0, 1) ;
            if (double.IsNaN(a) == true) a = 0.0;

            if (SystemPPStatus.Count < 1)
                SystemPPStatus.Add(new ChartDataDouble() { category = "Patch Panel Port", value = a });
            else
            {
                ChartDataDouble node = SystemPPStatus[0];
                node.value = a;
            }
            sys1 = t2.ToString() + " / " + t1.ToString();

            t1 = Stat.get_tot_sw_ports_by_site_id(g.selected_site_id, AssetTreeType.Site);
            t2 = Stat.get_used_sw_ports_by_site_id(g.selected_site_id, AssetTreeType.Site);
            t3 = t1;
            t4 = t2;
            a = Math.Round((t4 / t3) * 100.0, 1);
            if (double.IsNaN(a) == true) a = 0.0;

            if (SystemSwitchStatus.Count < 1)
                SystemSwitchStatus.Add(new ChartDataDouble() { category = "Switch Port", value = a });
            else
            {
                ChartDataDouble node = SystemSwitchStatus[0];
                node.value = a;
            }
            sys2 = t2.ToString() + " / " + t1.ToString();

            t1 = Stat.get_tot_rack_by_site_id(g.selected_site_id, AssetTreeType.Site);
            t2 = Stat.get_used_rack_by_site_id(g.selected_site_id, AssetTreeType.Site);
            t3 = t1;
            t4 = t2;
            a = Math.Round((t4 / t3) * 100.0, 1);
            if (double.IsNaN(a) == true) a = 0.0;
            if (SystemRackStatus.Count < 1)
                SystemRackStatus.Add(new ChartDataDouble() { category = "Rack Space", value = a });
            else
            {
                ChartDataDouble node = SystemRackStatus[0];
                node.value = a;
            }
            sys3 = t2.ToString() + " / " + t1.ToString();

            var s1 = await Stat.get_tot_terminal_by_site_id(g.selected_site_id);
            var s2 = await Stat.get_used_terminal_by_site_id(g.selected_site_id);
            t3 = s1;
            t4 = s2;
            a = Math.Round((t4 / t3) * 100.0, 1);
            if (double.IsNaN(a) == true) a = 0.0;

            if (SystemTerminalStatus.Count < 1)
                SystemTerminalStatus.Add(new ChartDataDouble() { category = "Terminal", value = 0 });
            else
            {
                ChartDataDouble node = SystemTerminalStatus[0];
                node.value = a;
            }

            if (s1 == 0 || s2 == 0)
                sys4 = "0 / 0";
            else
                sys4 = s2.ToString() + " / " + s1.ToString();

            Systemdata = new SystemData() { Sys1 = sys1, Sys2 = sys2, Sys3 = sys3, Sys4 = sys4 };
            return true;
        }

        ObservableCollection<ChartData> data5 = new ObservableCollection<ChartData>();
        ObservableCollection<SeriesData> Series5 = new ObservableCollection<SeriesData>();

        // 통계 5 시간대별 터미날 접속 현황 처리 
        private async Task<bool> getDBList_Stat5()
        {
            List<sp_stat_terminal_data_hour_Result> terminal_data_hour_list;

            var v1 = (List<sp_stat_terminal_data_hour_Result>)await Stat.get_terminal_by_site_id(g.selected_site_id);
            if (v1 == null) return false;
            //if (v1.Count == 0) return false;

            terminal_data_hour_list = v1.ToList();

            if (data5.Count < 1)
            {
                for (int i = 0; i < 24; i++)
                {
                    ChartData vm1 = new ChartData() { category = i.ToString(), value = 0 };
                    data5.Add(vm1);
                }
            }

            for (int i = 0; i < 24; i++)
            {
                ChartData node = data5[i];
                var node1 = terminal_data_hour_list.Find(p => p.time == (i));
                if (node1 != null)
//                    node.value = node1.avg_of_tot_terminal ?? 0;
                    node.value = node1.avg_of_act_terminal ?? 0;
                else
                    node.value = 0;
            }

            if (Series5.Count < 1)
            {
                Series5.Add(new SeriesData() { SeriesDescription = "Data", SeriesDisplayName = "SampleName", Items = data5 });
            }
            SeriesData node2 = Series5[0];
            node2.Items = data5;

            TerminalSeries = null;
            TerminalSeries = Series5;
            return true;
        }


        ObservableCollection<SysData> sysData6 = new ObservableCollection<SysData>();

        // 통계 6 시스템 현황 처리 
        private bool getDBList_Stat6()
        {
            List<stat_catalog_group_id> stat_catalog_count;     // 장치 종류와 상태 처리용 
            string[] sname = new string[] { "i_Controller", "i_Patch", "Patch Panel", "Switch", "Outlet", "Environment" };
            string[] img = new string[] { "ic_80.png", "ipp_80.png", "pp_80.png", "l3_sw_80.png", "fp_80.png", "eb_80.png" };
            int[] c_list = new int[] { 3110, 3130, 3440, 3330, 3430, 3120 };
            int[] c_list2 = new int[] { 0, 0, 0, 3320, 3420, 0 };
            int cnt = 0;

            // 2015.06.10 실시간 처리를 위함 
            List<ic_connect_status> ic_al_list = g.ic_connect_status_list.FindAll(at => at.ic_connect_status1 == "Y");
            int ic_cnt = ic_al_list.Count();

            // 카다로그 그룹으로 분류된 모든 네트웍 장비에 대한 리스트와 장비수 쿼리 
            var tdb1 = from a in g.asset_list
                       join b in g.catalog_list on a.catalog_id equals b.catalog_id
                       join c in g.catalog_group_list on b.catalog_group_id equals c.catalog_group_id
                       orderby c.catalog_group_id
                       group c by c.catalog_group_id into k
                       select new stat_catalog_group_id()
                       {
                           catalog_group_id = k.First().catalog_group_id,
                           catalog_group_name = k.First().catalog_group_name,
                           DeviceCount = k.Count(),
                           img_string = "/I2MS2;component/Icons/stat/l2_sw_80.png",
                           DeviceFree = 0,
                           DeviceUsage = 0,
                       };
            // 결과에 이미지 이름 추가하기 처리  
            var tdb2 = from a in tdb1
                       select new stat_catalog_group_id()
                       {
                           catalog_group_id = a.catalog_group_id,
                           catalog_group_name = a.catalog_group_name,
                           img_string = "/I2MS2;component/Icons/stat/" + getlink_80_image_id(a.catalog_group_id),
                           DeviceFree = 0,
                           DeviceUsage = 0,
                           DeviceCount = a.DeviceCount,
                       };

            stat_catalog_count = tdb2.ToList();

            if (sysData6.Count < 1)
            {
                for (int i = 0; i < 6; i++)
                {
                    SysData s1 = new SysData();
                    s1.dataImages = new BitmapImage(new Uri("/I2MS2;component/Icons/stat/"+img[i], UriKind.Relative));
                    s1.dataStrings = sname[i];
                    s1.datavalue = 0;
                    sysData6.Add(s1);
                }
            }

            // 시스템 출력에 추가하기 
            for (int i = 0; i < c_list.Count(); i++)
            {
                var v1 = stat_catalog_count.Find(p => p.catalog_group_id == c_list[i]);
                var v2 = stat_catalog_count.Find(p => p.catalog_group_id == c_list2[i]);
                cnt = 0;

                if (v2 != null)
                {
                    cnt = v2.DeviceCount;
                    if (v1 == null)
                    {
                        cnt = 0;
                        v1 = stat_catalog_count.Find(p => p.catalog_group_id == c_list2[i]);
                    }
                }

                if (v1 != null)
                {
                    SysData node = sysData6[i];
                    if(i == 0)
                        cnt = ic_cnt;
                    else
                        cnt = v1.DeviceCount + cnt;
                    node.datavalue = cnt;
                }
            }
            SysDataList = null;
            SysDataList = sysData6;
            return true;
        }

        // 통계 7 패치, 컨트롤러 , 패널 알람 처리 
        private bool getDBList_Stat7()
        {
            int t1 = 1090101;   // 비인가 연결  
            int t2 = 1090102;   // 비인가 연결 복구  
            int t3 = 1090103;   // 비인가 제거 
            int t4 = 1090104;   // 비인가 제거 복구  
            int t11 = 1090112;   // 패치패널 off 
            int t12 = 1090114;   // 컨트롤러 off 
            int t21 = 1090183;   // 펌웨어 업그레이드 실패 

            DateTime d1 = DateTime.Today;
            DateTime d2 = d1.AddDays(1);

            var tdb1 = from a in g.event_hist_list
                       where (Convert.ToDateTime(a.write_time) >= Convert.ToDateTime(d1) && Convert.ToDateTime(a.write_time) <= Convert.ToDateTime(d2))
                       select new { a.event_id };

            int c1 = tdb1.Where(p => p.event_id == t1).Count();
            int c2 = tdb1.Where(p => p.event_id == t2).Count();
            int c3 = tdb1.Where(p => p.event_id == t3).Count();
            int c4 = tdb1.Where(p => p.event_id == t4).Count();
            int c11 = tdb1.Where(p => p.event_id == t11).Count();
            int c12 = tdb1.Where(p => p.event_id == t12).Count();
            int c21 = tdb1.Where(p => p.event_id == t21).Count();

            int[] value = new int[] { 0, 0, 0 };
            value[0] = c1 + c2 + c3 + c4;
            value[1] = c11;
            value[2] = c12;

            if (SystemEvent.Count < 1)
            {
                SystemEvent.Add(new ChartData() { category = "i-Patch Cord", value = c1 + c2 + c3 + c4 });
                SystemEvent.Add(new ChartData() { category = "Patch Panel", value = c11 });
                SystemEvent.Add(new ChartData() { category = "Controller", value = c12 });
            }
            else
            {
                for (int i = 0; i < SystemEvent.Count; i++ )
                {
                    ChartData node = SystemEvent[i];
                    node.value = value[i];
                }
            }
            return true;
        }

        // 통계 8 현재 전력, 온도, 습도, 도어 처리 
        private async Task<bool> getDBList_Stat8()
        {
            List<eb_port_data_cur> eb_port_data_cur_list;           // 현재
            site_environment site_environment_list;           // 현재
            DateTime now = DateTime.Today;
            DateTime tow = now + new TimeSpan(1, 0, 0, 0);

            //_power1.Text = "0 Kwh";
            //_door1.Text = "0 EA";
            //_temperture1.Text = "0 ºC";
            //_humidity1.Text = "0 %";

            site_environment_list = g.site_environment_list.First();


            string filter = "";
            var v2 = (List<eb_port_data_cur>)await g.webapi.getList("eb_port_data_cur", typeof(List<eb_port_data_cur>), filter);
            if (v2 == null) return false;
            var v3 = v2.Where(p => Convert.ToDateTime(p.last_updated) >= Convert.ToDateTime(now) && Convert.ToDateTime(p.last_updated) < Convert.ToDateTime(tow));
            eb_port_data_cur_list = v3.ToList();

            if (eb_port_data_cur_list.Count == 0) return false;
            // 전력  현재
            double d1 = get_double(eb_port_data_cur_list.Max(p => p.power_ph));
            int max = get_int(site_environment_list.high_powerh_th);
            int min = 0;
            if (CurPower == null)
                CurPower = new EnvData { category = d1.ToString() + " Kwh", ivalue = (int)d1, imax = max, imin = 0 };
            else
            {
                CurPower.category = d1.ToString() + " Kwh";
                CurPower.ivalue = (int)d1;
                CurPower.imax = max;
                CurPower.imin = 0;
            }
            // 온도 현재
            int tmp1 = eb_port_data_cur_list.Max(p => p.sensor_t) ?? 0;
            int tmp3 = 0;
            if (tmp1 != 0) tmp3 = tmp1 / 10;
            max = get_int(site_environment_list.high_temp_th);
            min = get_int(site_environment_list.low_temp_th);

            if (CurTemp == null)
                CurTemp = new EnvData { category = tmp3.ToString() + " ºC", ivalue = tmp3, imax = max, imin = min };
            else
            {
                CurTemp.category = tmp3.ToString() + " ºC";
                CurTemp.ivalue = tmp3;
                CurTemp.imax = max;
                CurTemp.imin = min;
            }

            // 습도 현재 
            tmp1 = eb_port_data_cur_list.Max(p => p.sensor_h) ?? 0;
            tmp3 = 0;
            if (tmp1 != 0) tmp3 = tmp1 / 10;
            max = get_int(site_environment_list.high_humi_th);
            min = get_int(site_environment_list.low_humi_th);

            if (CurHumi == null)
                CurHumi = new EnvData { category = tmp3.ToString() + " %", ivalue = tmp3, imax = max, imin = min };
            else
            {
                CurHumi.category = tmp3.ToString() + " %";
                CurHumi.ivalue = tmp3;
                CurHumi.imax = max;
                CurHumi.imin = min;
            }
            // 도어 열린 갯수 
            if (CurDoor == null)
                CurDoor = new EnvData { ivalue = (int)eb_port_data_cur_list.Max(p => p.door) };
            else
            {
                CurDoor.ivalue = (int)eb_port_data_cur_list.Max(p => p.door);
            }
            return true;
        }

        // 통계 9  전력 온도 습도 도어 최대, 최소 경고 처리  
        private bool getDBList_Stat9()
        {
            int[] t1 = new int[] { 1090147, 1090148, 1090151, 1090152, 1090153, 1090154, 1090161, 1090162, 1090163, 1090164, 1090171, 1090172 };
            int[] t2 = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            DateTime d1 = DateTime.Today;
            DateTime d2 = d1.AddDays(1);

            var tdb1 = from a in g.event_hist_list
                       where (Convert.ToDateTime(a.write_time) >= Convert.ToDateTime(d1) && Convert.ToDateTime(a.write_time) <= Convert.ToDateTime(d2))
                       select new { a.event_id };

            if (tdb1 == null) return false;

            for (int i = 0; i < t1.Count(); i++)
            {
                t2[i] = tdb1.Where(p => p.event_id == t1[i]).Count();
            }

            int[] value = new int[] { 0, 0, 0, 0, 0, 0 };
            value[0] = t2[1];
            value[1] = t2[3];
            value[2] = t2[5];
            value[3] = t2[7];
            value[4] = t2[9];
            value[5] = t2[11];

            if (SystemEventEnvironment.Count < 1)
            {
                SystemEventEnvironment.Add(new ChartData() { category = "Power", value = t2[1] });
                SystemEventEnvironment.Add(new ChartData() { category = "Temperature Max", value = t2[3] });
                SystemEventEnvironment.Add(new ChartData() { category = "Temperature Min", value = t2[5] });
                SystemEventEnvironment.Add(new ChartData() { category = "Humidity Max", value = t2[7] });
                SystemEventEnvironment.Add(new ChartData() { category = "Humidity Min", value = t2[9] });
                SystemEventEnvironment.Add(new ChartData() { category = "Door Open", value = t2[11] });
            }
            for (int i = 0; i < SystemEventEnvironment.Count; i++)
            {
                ChartData node = SystemEventEnvironment[i];
                node.value = value[i];
            }
            return true;
        }

        private async Task<bool> getDBList_Stat10()
        {
            List<sp_stat_eb_hour_Result> stat_eb_hour_list;         // 
            DateTime d1 = DateTime.Today;

            string filter;
            filter = string.Format("?location_id=0&year={0}&month={1}&day={2}", d1.Year, d1.Month, d1.Day);
            var v1 = (List<sp_stat_eb_hour_Result>)await g.webapi.getList("sp_stat_eb_hour", typeof(List<sp_stat_eb_hour_Result>), filter);
            if (v1 == null) return false;
            //if (v1.Count == 0) return false;

            stat_eb_hour_list = v1.ToList();

            if (StatPower.Count < 1)
            {
                for (int i = 0; i < 24; i++)
                {
                    ChartData vm1 = new ChartData() { category = i.ToString(), value = 0 };
                    StatPower.Add(vm1);
                }
            }
            for (int i = 0; i < StatPower.Count; i++)
            {
                ChartData node = StatPower[i];
                var node1 = stat_eb_hour_list.Find(p => p.time == i);
                if (node1 != null)
                {
                    node.value = get_int(node1.powerh);
                }
            }
            return true;
        }

        private async Task<bool> getDBList_Stat11()
        {
            List<sp_stat_eb_hour_Result> stat_eb_hour_list;         // 
            DateTime d1 = DateTime.Today;

            string filter;
            filter = string.Format("?location_id=0&year={0}&month={1}&day={2}", d1.Year, d1.Month, d1.Day);
            var v1 = (List<sp_stat_eb_hour_Result>)await g.webapi.getList("sp_stat_eb_hour", typeof(List<sp_stat_eb_hour_Result>), filter);
            if (v1 == null) return false;
            //if (v1.Count == 0) return false;

            stat_eb_hour_list = v1.ToList();

            if (StatTemp.Count < 1)
            {
                for (int i = 0; i < 24; i++)
                {
                    ChartData vm1 = new ChartData() { category = i.ToString(), value = 0 };
                    StatTemp.Add(vm1);
                }
            }
            for (int i = 0; i < StatTemp.Count; i++)
            {
                ChartData node = StatTemp[i];
                var node1 = stat_eb_hour_list.Find(p => p.time == i);
                if (node1 != null)
                {
                    node.value = get_int(node1.temperature);
                }
            }
            return true;
        }

        private async Task<bool> getDBList_Stat12()
        {
            List<sp_stat_eb_hour_Result> stat_eb_hour_list;         // 
            DateTime d1 = DateTime.Today;

            string filter;
            filter = string.Format("?location_id=0&year={0}&month={1}&day={2}", d1.Year, d1.Month, d1.Day);
            var v1 = (List<sp_stat_eb_hour_Result>)await g.webapi.getList("sp_stat_eb_hour", typeof(List<sp_stat_eb_hour_Result>), filter);
            if (v1 == null) return false;
            //if (v1.Count == 0) return false;

            stat_eb_hour_list = v1.ToList();

            if (StatHumi.Count < 1)
            {
                for (int i = 0; i < 24; i++)
                {
                    ChartData vm1 = new ChartData() { category = i.ToString(), value = 0 };
                    StatHumi.Add(vm1);
                }
            }
            for (int i = 0; i < StatHumi.Count; i++)
            {
                ChartData node = StatHumi[i];
                var node1 = stat_eb_hour_list.Find(p => p.time == i);
                if (node1 != null)
                {
                    node.value = get_int(node1.humidity);
                }
            }
            return true;
        }


        ObservableCollection<ChartData> data16 = new ObservableCollection<ChartData>();
        ObservableCollection<SeriesData> Series16 = new ObservableCollection<SeriesData>();

        // 통계 16 일별 터미날 접속 현황 처리 
        private async Task<bool> getDBList_Stat16()
        {
            List<sp_stat_terminal_data_day_Result> terminal_data_list;

            DateTime now = DateTime.Today;

            int days = now.AddMonths(1).AddDays(0 - now.Day).Day;

            string filter2 = string.Format("?location_id=0&year={0}&month={1}", now.Year, now.Month);
            var v2 = (List<sp_stat_terminal_data_day_Result>)await g.webapi.getList("sp_stat_terminal_data_day", typeof(List<sp_stat_terminal_data_day_Result>), filter2);
            if (v2 == null) return false;

            terminal_data_list = v2.ToList();
            if (data16.Count < 1)
            {
                for (int i = 0; i < days; i++)
                {
                    ChartData vm1 = new ChartData() { category = (i+1).ToString(), value = 0 };
                    data16.Add(vm1);
                }
            }

            for (int i = 0; i < days; i++)
            {
                ChartData node = data16[i];
                var node1 = terminal_data_list.Find(p => p.day == (i + 1));
                if (node1 != null)
                    node.value = node1.avg_of_act_terminal ?? 0;
//                    node.value = node1.avg_of_tot_terminal ?? 0;
                else
                    node.value = 0;
            }

            if (Series16.Count < 1)
            {
                Series16.Add(new SeriesData() { SeriesDescription = "Data", SeriesDisplayName = "SampleName", Items = data16 });
            }
            SeriesData node2 = Series16[0];
            node2.Items = data16;

            TerminalSeriesDay = null;
            TerminalSeriesDay = Series16;
            return true;
        }

        ObservableCollection<ChartData> data17 = new ObservableCollection<ChartData>();
        ObservableCollection<SeriesData> Series17 = new ObservableCollection<SeriesData>();
        // 통계 17 월별 터미날 접속 현황 처리 
        private async Task<bool> getDBList_Stat17()
        {
            List<sp_stat_terminal_data_month_Result> terminal_data_list;

            DateTime now = DateTime.Today;

            string filter2 = string.Format("?location_id=0&year={0}", now.Year);
            var v2 = (List<sp_stat_terminal_data_month_Result>)await g.webapi.getList("sp_stat_terminal_data_month", typeof(List<sp_stat_terminal_data_month_Result>), filter2);
            if (v2 == null) return false;

            terminal_data_list = v2.ToList();
            if (data17.Count < 1)
            {
                for (int i = 0; i < 12; i++)
                {
                    ChartData vm1 = new ChartData() { category = (i+1).ToString(), value = 0 };
                    data17.Add(vm1);
                }
            }

            //int[] value = new int[] { 43, 44, 0, 0, 0, 0 };

            for (int i = 0; i < 12; i++)
            {
                ChartData node = data17[i];
                var node1 = terminal_data_list.Find(p => p.month == (i + 1));
                if (node1 != null)
//                    node.value = node1.avg_of_act_terminal ?? 0;
                    node.value = node1.avg_of_tot_terminal ?? 0;
                else
                    node.value = 0;
            }

            if (Series17.Count < 1)
            {
                Series17.Add(new SeriesData() { SeriesDescription = "Data", SeriesDisplayName = "SampleName", Items = data17 });
            }
            SeriesData node2 = Series17[0];
            node2.Items = data17;

            TerminalSeriesMonth = null;
            TerminalSeriesMonth = Series17;
            return true;
        }

        #endregion 

        #region main Process 
        // 화면 로드 완료후 호출 처리 
        public async Task<bool> OnLoaded()
        {
            getDBList_Stat6();
            getDBList_Stat7();
            getDBList_Stat9();

            await getDBList_Stat1234();
            await getDBList_Stat5();
            await getDBList_Stat8();

            await getDBList_Stat10();
            await getDBList_Stat11();
            await getDBList_Stat12();

            await getDBList_Stat16();
            await getDBList_Stat17();

            Timer_on = false;

            return true;
        }

        public async Task<bool> OnLoaded_Event(eEventCode code)
        {

            switch (code)
            {
                case eEventCode.eDoorOpen:
                case eEventCode.eDoorClose:
                case eEventCode.eHighCurrentError:
                case eEventCode.eHighPowerError:
                case eEventCode.eHighPowerHourError:
                case eEventCode.eHighTempError:
                case eEventCode.eHighHumiError:
                case eEventCode.eHighVoltageError:
                    // 대시보드 갱신
                    getDBList_Stat9();
                    await getDBList_Stat8();
//                    g._P2DashBoard.update_dashboard(code);
                    break;
                case eEventCode.eUnauthorizedPlug:
                case eEventCode.eUnauthorizedUnplug:
                case eEventCode.eRestorePlug:
                case eEventCode.eRestoreUnplug:
                    // 대시보드 갱신
                    await getDBList_Stat1234();
//                    Console.WriteLine("eUnauthorizedPlug / eUnauthorizedUnplug");
                    break;
            }

            return true;
        }

        // 초기화 처리 
        public DataViewModel()
        {
            resourceDictionary.Source = new Uri("/I2MS2;component/Resources/UIResourceDictionary.xaml", UriKind.Relative);

            SystemPPStatus = new ObservableCollection<ChartDataDouble>();
            SystemSwitchStatus = new ObservableCollection<ChartDataDouble>();
            SystemRackStatus = new ObservableCollection<ChartDataDouble>();
            SystemTerminalStatus = new ObservableCollection<ChartDataDouble>();
            SysDataList = new ObservableCollection<SysData>();
            SystemEvent = new ObservableCollection<ChartData>();
            StateP = new ObservableCollection<ChartData>();
            StateTmax = new ObservableCollection<ChartData>();
            StateTmin = new ObservableCollection<ChartData>();
            StateHmax = new ObservableCollection<ChartData>();
            StateHmin = new ObservableCollection<ChartData>();
            StateD = new ObservableCollection<ChartData>();

            SystemEventEnvironment = new ObservableCollection<ChartData>();

            StatPower = new ObservableCollection<ChartData>();
            StatTemp = new ObservableCollection<ChartData>();
            StatHumi = new ObservableCollection<ChartData>();

            var l2 = Reg.get_dashboard("dashboard_time");
            if (l2 != null)
                _dashboard_time_list = l2.ToList();
            else
                Reg.save_dashboard(_dashboard_time_list, "dashboard_time");
            TestStartTimer();
        }

        // 타이머 로직 처리 
        public void TestStartTimer()
        {
            int time1 = _dashboard_time_list[0];
            Timer.Interval = System.TimeSpan.FromSeconds(time1);
            //Timer.Interval = System.TimeSpan.FromMilliseconds(3000);
            Timer.Tick += new EventHandler(TimerEvent);
            Timer.Start();

//            OnLoaded();
        }

        internal void TestStopTimer()
        {
            Timer.Stop();
        }

        void TimerEvent(object sender, System.EventArgs e)
        {
            if (Timer_on == true) 
                return;
            try
            {
                Timer_on = true;
                OnLoaded();
            }
            catch 
            {
                Timer_on = false;
            }
        }

        #endregion

        #region function library

        // int to double 
        private double get_double(int? nullable)
        {
            int int1 = nullable ?? 0;
            double double1 = 0;
            if (int1 != 0)
                double1 = int1 / 10;   // 소숫점 처리용 
            return double1;
        }

        private int get_int(int? nullable)
        {
            int int1 = nullable ?? 0;

            if (int1 != 0)
                int1 = int1 / 10;   // 소숫점 처리용 
            return int1;
        }

        private string getlink_80_image_id(int? id)
        {
            int[] c_list = new int[] { 3110, 3130, 3440, 3330, 3430, 3120 };

            int lid = id ?? 0;
            string ret = "";

            if (lid == 0) return ret;
            var a1 = g.catalog_list.Find(p => p.catalog_group_id == lid);
            var a2 = g.image_list.Find(p => p.image_id == a1.link_80_image_id);

            switch(lid)
            {
                case 3110:
                    ret = "ic_80.png";
                    break;
                case 3130:
                    ret = "ipp_80.png";
                    break;
                case 3440:
                    ret = "pp_80.png";
                    break;
                case 3320:
                case 3330:
                    ret = "l3_sw_80.png";
                    break;
                case 3420:
                case 3430:
                    ret = "fp_80.png";
                    break;
                case 3120:
                    ret = "eb_80.png";
                    break;
            }
            return ret;
        }
        #endregion

        #region 속성 선언, 바인딩 속성
        private Thickness marginbinding = new Thickness(5, 0, 5, 0);
        public Thickness MarginBinding
        {
            get { return marginbinding; }
            set
            {
                marginbinding = value;
                NotifyPropertyChanged("MarginBinding");
            }
        }

        private bool darkLayout = true;
        public bool DarkLayout
        {
            get { return darkLayout; }
            set
            {
                darkLayout = value;
                NotifyPropertyChanged("DarkLayout");
                NotifyPropertyChanged("Foreground");
                NotifyPropertyChanged("Background");
                NotifyPropertyChanged("MainBackground");
                NotifyPropertyChanged("MainForeground");
            }
        }
        private double fontSize = 11.0;
        public double SelectedFontSize
        {
            get { return fontSize; }
            set
            {
                fontSize = value;
                NotifyPropertyChanged("SelectedFontSize");
                NotifyPropertyChanged("SelectedFontSizeString");
            }
        }

        public string Foreground
        {
            get
            {
                if (darkLayout)
                {
                    return "#FFEEEEEE";
                }
                return "#FF666666";
            }
        }
        public string MainForeground
        {
            get
            {
                if (darkLayout)
                {
                    return "#FFFFFFFF";
                }
                return "#FF666666";
            }
        }
        public string Background
        {
            get
            {
                if (darkLayout)
                {
                    return resourceDictionary["_colorDarkGray5"].ToString();
                }
                return "#FFF9F9F9";
            }
        }
        public string MainBackground
        {
            get
            {
                if (darkLayout)
                {
                    return "#FF000000";
                }
                return "#FFEFEFEF";
            }
        }
        public string ToolTipFormat
        {
            get
            {
                return "{0} : '{1}'";
            }
        }

        public string ToolTipFormat100
        {
            get
            {
                return "{0} : '{1}'  / ({5:P2}) ";
            }
        }

        private object selectedPalette = null;
        public object SelectedPalette
        {
            get
            {
                return selectedPalette;
            }
            set
            {
                selectedPalette = value;
                NotifyPropertyChanged("SelectedPalette");
            }
        }

        private object selectedItem = null;
        public object SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                // selected item has changed
                selectedItem = value;
            }
        }

        private bool isRowColumnSwitched = true;
        public bool IsRowColumnSwitched
        {
            get
            {
                return isRowColumnSwitched;
            }
            set
            {
                isRowColumnSwitched = value;
                NotifyPropertyChanged("IsRowColumnSwitched");
            }
        }

        private double selectedDoughnutInnerRadiusRatio = 0.7;
        public double SelectedDoughnutInnerRadiusRatio
        {
            get
            {
                return selectedDoughnutInnerRadiusRatio;
            }
            set
            {
                selectedDoughnutInnerRadiusRatio = value;
                NotifyPropertyChanged("SelectedDoughnutInnerRadiusRatio");
                NotifyPropertyChanged("SelectedDoughnutInnerRadiusRatioString");
            }
        }

        public bool force_changed
        {
            get { return true; }
            set { NotifyPropertyChanged(""); }
        }

        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(property));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion 

    }

}