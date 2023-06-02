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
using System.Windows.Shapes;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls.Primitives;
using System.Collections;
using System.IO;
using MahApps.Metro.Controls;

using I2MS2.Models;
using I2MS2.Library;
using WebApi.Models;
using I2MS2.UserControls;

#pragma warning disable 4014

namespace I2MS2.Windows
{
    /// <summary>
    /// IPMView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class IPMView : MetroWindow
    {
        List<SysData> _sysdata_list = new List<SysData>();      // 이미지 
        List<eb_port_data_cur> eb_port_data_cur_list;           // 현재
        List<eb_port_data_hour> eb_port_data_hour_list;         // 누적
        List<int> asset_id_list = new List<int>();              // 엣셋 아이디 리스트
        int[] device_stat = new int[5];                         // 환경 장치 상태 

        int _location_id = 0;

        public IPMView(int location_id)
        {
            _location_id = location_id;
            InitializeComponent();
            dispRack(location_id);
            initData();
        }

        private async Task<bool> initData()
        {
            _power1.Text = "0 Kwh";
            _power2.Text = "0 Kwh";
            _power3.Text = "0 Kwh";
            _door1.Text = "0 EA";
            _door2.Text = "0 EA";
            _door3.Text = "0 EA";
            _temperture1.Text = "0 ºC";
            _temperture2.Text = "0 ºC";
            _humidity1.Text = "0 %";
            _humidity2.Text = "0 %";

            // eb_port_data_hour_list 와 eb_port_data_cur_list 가져오기 
            string filter = "";
            var v1 = (List<eb_port_data_hour>)await g.webapi.getList("eb_port_data_hour", typeof(List<eb_port_data_hour>), filter);
            if (v1 == null) return false;
            var v2 = (List<eb_port_data_cur>)await g.webapi.getList("eb_port_data_cur", typeof(List<eb_port_data_cur>), filter);
            if (v2 == null) return false;


            DateTime now = DateTime.Today;
            DateTime yes = now - new TimeSpan(1,0,0,0);

            int[] t2 = asset_id_list.ToArray ();

            var tdb1 = from a in v1
                       where t2.Contains(a.asset_id)
                       orderby a.asset_id
                       select new eb_port_data_hour()
                       {
                           asset_id = a.asset_id,
                           port_no = a.port_no,
                           date = a.date,
                           time_0_23 = a.time_0_23,
                           power_v = a.power_v,
                           power_v_cnt = a.power_v_cnt,
                           power_i = a.power_i,
                           power_i_cnt = a.power_i_cnt,
                           power_p = a.power_p,
                           power_p_cnt = a.power_p_cnt,
                           power_ph = a.power_ph,
                           power_ph_cnt = a.power_ph_cnt,
                           sensor_t = a.sensor_t,
                           sensor_t_cnt = a.sensor_t_cnt,
                           sensor_h = a.sensor_h,
                           sensor_h_cnt = a.sensor_h_cnt,
                           door = a.door,
                           power_peek_v = a.power_peek_v,
                           power_peek_i = a.power_peek_i,
                           power_peek_p = a.power_peek_p,
                           power_peek_ph = a.power_peek_ph,
                           sensor_peek_t = a.sensor_peek_t,
                           sensor_peek_h = a.sensor_peek_h,
                           last_updated = a.last_updated,
                           powerwh = a.powerwh,
                       };

            var tdb2 = from a in v2
                       where t2.Contains(a.asset_id)
                       orderby a.asset_id
                       select new eb_port_data_cur()
                       {
                           asset_id = a.asset_id,
                           port_no = a.port_no,
                           power_v = a.power_v,
                           power_i = a.power_i,
                           power_p = a.power_p,
                           power_ph = a.power_ph,
                           sensor_t = a.sensor_t,
                           sensor_h = a.sensor_h,
                           door = a.door,
                           last_updated = a.last_updated,
                           powerwh = a.powerwh,
                       };

            eb_port_data_hour_list = tdb1.ToList();
            eb_port_data_cur_list = tdb2.ToList();

            // 전일자와 현재 일자만 가져오기 
            var l1 = eb_port_data_hour_list.Where(p => p.date == yes);
            var l2 = eb_port_data_hour_list.Where(p => p.date == now);

            // 현재일자가 없으면 마지막 일자를 가져오기 
            if (l2.Count() == 0)
            {
                l2 = eb_port_data_hour_list.Where(p => p.time_0_23 == 23);
            }

            // 전력  현재 , 이전일자 최대 , 금일자 최대    soluwin 2017-01-13
            double d1 = get_double(eb_port_data_cur_list.Max(p => p.powerwh));
            //double d1 = get_double(eb_port_data_cur_list.Max(p => p.power_ph));
            _power1.Text = d1.ToString("N1") + " Kwh";
            d1 = get_double(l1.Sum(p => p.power_peek_ph));
            _power2.Text = d1.ToString("N1") + " Kwh";
            d1 = get_double(l2.Sum(p => p.power_peek_ph));
            _power3.Text = d1.ToString("N1") + " Kwh";

            // 온도 현재 , 금일 최대
            int tmp1 = eb_port_data_cur_list.Max(p => p.sensor_t) ?? 0; 
            int tmp3 = 0;
            if (tmp1 != 0) tmp3 = tmp1 / 10;
            _temperture1.Text = tmp3.ToString() + " ºC";
            d1 = l2.Max(p => p.sensor_peek_t) ?? 0;
            tmp3 = 0;
            if (d1 != 0) tmp3 = (int) d1/10;
            _temperture2.Text = tmp3.ToString() + " ºC";

            // 습도 현재 , 금일 최대 
            tmp1 = eb_port_data_cur_list.Max(p => p.sensor_h) ?? 0; 
            tmp3 = 0;
            if (tmp1 != 0) tmp3 = tmp1 / 10;
            _humidity1.Text = tmp3.ToString() + " %";
            d1 = l2.Max(p => p.sensor_peek_h) ?? 0;
            tmp3 = 0;
            if (d1 != 0) tmp3 = (int)d1 / 10;
            _humidity2.Text = tmp3.ToString() + " %";

            // 도어 열린 갯수, 총갯수, 닫힌갯수 
            int close = device_stat[4] - l2.Max(p => p.door).Value;
            _door1.Text = eb_port_data_cur_list.Max(p => p.door).ToString() + " EA";
            _door2.Text = device_stat[4].ToString() + " EA";
            _door3.Text = close.ToString() + " EA";

            return true;

        }

        // int to double 
        private double get_double(int? nullable)
        {
            int int1 = nullable ?? 0;
            double double1 = 0;
            if (int1 != 0)
                double1 = int1 / 10;   // 소숫점 처리용 
            return double1;
        }

        // 사이트, 건물, 층, 룸, 랙 별 환경장치 갯수와 엣셋 아이디 받기 처리  


        private void dispRack(int location_id)
        {
            string[] fname = new string[] { "e0.png", "e1.png", "e2.png", "e3.png", "e4.png" };
            string[] sname = new string[] { "EnergyBox: ", "PowerStrip: ", "Temperture: ", "Humidity: ", "Door: " };
            // IEnumerable<int> list1 = null;

            var l = g.location_list.Find(p => p.location_id == location_id);
            if (l == null)
                return;

            IEnumerable<location> list = null;

            switch (l.location_level)
            {
                case 3:
                    int site_id2 = l.site_id ?? 0;
                    list = g.location_list.Where(p => p.site_id == site_id2);
                    asset_id_list= Stat.get_tot_eb_by_site_id(site_id2, out device_stat[0], out device_stat[1], out device_stat[2], out device_stat[3], out device_stat[4]);
                    break;
                case 4:
                    int building_id2 = l.building_id ?? 0;
                    list = g.location_list.Where(p => p.building_id == building_id2);
                    asset_id_list = Stat.get_tot_eb_by_building_id(building_id2, out device_stat[0], out device_stat[1], out device_stat[2], out device_stat[3], out device_stat[4]);
                    break;
                case 5:
                    int floor_id2 = l.floor_id ?? 0;
                    list = g.location_list.Where(p => p.floor_id == floor_id2);
                    asset_id_list = Stat.get_tot_eb_by_floor_id(floor_id2, out device_stat[0], out device_stat[1], out device_stat[2], out device_stat[3], out device_stat[4]);
                    break;
                case 6:
                    int room_id2 = l.room_id ?? 0;
                    list = g.location_list.Where(p => p.room_id == room_id2);
                    asset_id_list = Stat.get_tot_eb_by_room_id(room_id2, out device_stat[0], out device_stat[1], out device_stat[2], out device_stat[3], out device_stat[4]);
                    break;
                case 7:
                    int rack_id2 = l.rack_id ?? 0;
                    list = g.location_list.Where(p => p.rack_id == rack_id2);
                    asset_id_list = Stat.get_tot_eb_by_rack_id(rack_id2, out device_stat[0], out device_stat[1], out device_stat[2], out device_stat[3], out device_stat[4]);
                    break;

            }
            //            _exP60.Header = list.First().location_path + " Enviroment Device information";
            //_exP61.Header = list.First().location_path + " Enviroment Device information";

            // 장치의 종류와 갯수, 이미지 가겨오기 
            for(int i =0; i < 5; i++)
            {
                SysData s1 = new SysData();
                s1.dataImages = new BitmapImage(new Uri("/I2MS2;component/Icons/env/"+fname[i], UriKind.Relative));
                s1.dataStrings = sname[i] + device_stat[i].ToString();
                s1.datavalue = device_stat[i];
                _sysdata_list.Add(s1);
            }
//            _lb.ItemsSource = _sysdata_list;

        }

    }


}
