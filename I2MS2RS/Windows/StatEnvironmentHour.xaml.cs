using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Documents;
using System.Diagnostics;

using I2MS2.Models;
using I2MS2.Library;
using WebApi.Models;
using I2MS2.UserControls;
using I2MS2.Chart;
using MahApps.Metro.Controls;

#pragma warning disable 4014

namespace I2MS2.Windows
{
    /// <summary>
    /// ManufactureManager.xaml에 대한 상호 작용 논리
    /// </summary>

    public partial class StatEnvironmentHour : MetroWindow
    {
        // 출력할 테이블 
        List<sp_stat_eb_hour_Result> stat_eb_hour_list;         // 

        List<DurationDate> _duration_year = new List<DurationDate>();
        List<DurationDate> _duration_month = new List<DurationDate>();
        List<DurationDate> _duration_day = new List<DurationDate>();
        List<locationView> _location_list = new List<locationView>();

        public DataViewModel ViewModel { get; set; }

        string templatefile = "";

        string[] sel_name = new string[] { "Power Status (Kw)", "Temperature Status (ºC)", "Humidity Status (%)", "Door Status (EA)", 
            "Voltage Status(V)", "Current Status(A)", "Smoke Status (EA)", "Electrical energy(kWh)", "Power factor(%)" };

        public StatEnvironmentHour()
        {
            InitializeComponent();

            ViewModel = new DataViewModel();
            this.DataContext = ViewModel;

            ViewModel.StatActive = new ObservableCollection<ChartData>();
            ViewModel.SelectedFontSize = 11.0;
            ViewModel.MarginBinding = new Thickness(5,0,5,0);
            init_data();
            getDBList2();
            getDBList();
        }

        private void init_data()
        {
            List<string> _sel_option1 = new List<string>();

            DateTime d1 = DateTime.Today;
            DateTime d2 = d1.AddMonths(1).AddDays(0 - d1.Day);

            for (int i = 2013; i < 2040; i++)
            {
                var aa = new DurationDate { date = i, sdate = i.ToString() };
                _duration_year.Add(aa);
            }

            for (int i = 1; i < 13; i++)
            {
                var aa = new DurationDate { date = i, sdate = i.ToString() };
                _duration_month.Add(aa);
            }

            for (int i = 1; i < 32; i++) // d2.Day 
            {
                var aa = new DurationDate { date = i, sdate = i.ToString() };
                _duration_day.Add(aa);
            }
            cbodateyear.ItemsSource = _duration_year;
            cbodateyear.SelectedIndex = _duration_year.FindIndex(p => p.date == d1.Year);
            cbodatemonth.ItemsSource = _duration_month;
            cbodateday.ItemsSource = _duration_day;
            _duration_month.Insert(0, new DurationDate { date = 0, sdate = "--All Field (default)--" });
            _duration_day.Insert(0, new DurationDate { date = 0, sdate = "--All Field (default)--" });
            cbodatemonth.SelectedIndex = 0;
            cbodateday.SelectedIndex = 0;

            for (int i = 0; i < sel_name.Count(); i++)
            {
                _sel_option1.Add(sel_name[i]);
            }
            cboType2.ItemsSource = _sel_option1;
            cboType2.SelectedIndex = 0;
        }

        #region init 로직
        private async Task<bool> initData()
        {
            int c1 = _duration_year[cbodateyear.SelectedIndex].date;
            int c2 = _duration_month[cbodatemonth.SelectedIndex].date;
            int c3 = _duration_day[cbodateday.SelectedIndex].date;
            int c4 = cboType2.SelectedIndex;
            locationView s1 = (locationView)cboType1.SelectedItem;

            string filter;
            _txtvalue.Text = "Value";

            if (s1.location_id != 0)
            {
                if (c2 == 0)
                {
                    filter = string.Format("?location_id={0}&year={1}&month=0&day=0", s1.location_id, c1);
                    _txtSelect.Text = "'" + c1.ToString() + " : " + sel_name[c4];
                }
                else if (c3 == 0)
                {
                    filter = string.Format("?location_id={0}&year={1}&month={2}&day=0", s1.location_id, c1, c2);
                    _txtSelect.Text = "'" + c1.ToString() + "/" + c2.ToString() + " : " + sel_name[c4];
                }
                else
                {
                    filter = string.Format("?location_id={0}&year={1}&month={2}&day={3}", s1.location_id, c1, c2, c3);
                    _txtSelect.Text = "'" + c1.ToString() + "/" + c2.ToString() + "/" + c3.ToString() + " : " + sel_name[c4];
                }

            }
            else
            {
                if (c2 == 0)
                {
                    filter = string.Format("?location_id=0&year={0}&month=0&day=0", c1);
                    _txtSelect.Text = "'" + c1.ToString() + " : " + sel_name[c4];
                }
                else if (c3 == 0)
                {
                    filter = string.Format("?location_id=0&year={0}&month={1}&day=0", c1, c2);
                    _txtSelect.Text = "'" + c1.ToString() + "/" + c2.ToString() + " : " + sel_name[c4];
                }
                else
                {
                    filter = string.Format("?location_id=0&year={0}&month={1}&day={2}", c1, c2, c3);
                    _txtSelect.Text = "'" + c1.ToString() + "/" + c2.ToString() + "/" + c3.ToString() + " : " + sel_name[c4];
                }
            }


            _txtcategory.Text = I2MSR.Properties.Resources.ResourceManager.GetString("C_Hour");
            var v1 = (List<sp_stat_eb_hour_Result>)await g.webapi.getList("sp_stat_eb_hour", typeof(List<sp_stat_eb_hour_Result>), filter);
            if (v1 == null) return false;
            //if (v1.Count == 0) return false;

            stat_eb_hour_list = v1.ToList();
            templatefile = "stat_eb_hour";
            ViewModel.StatActive.Clear();

            for (int i = 0; i < 24; i++)
            {
                ChartData vm1 = new ChartData() { category = i.ToString() };

                var node1 = stat_eb_hour_list.Find(p => p.time == i);
                if (node1 != null)
                {
                    switch (c4)
                    {
                        case 0: vm1.value = get_int(node1.powerwh); break;
                        case 1: vm1.value = get_int(node1.temperature); break;
                        case 2: vm1.value = get_int(node1.humidity); break;
                        case 3: vm1.value = node1.door ?? 0; break;
                        case 4: vm1.value = get_int(node1.voltage); break;
                        case 5: vm1.value = get_int(node1.current2); break;
                        case 6: vm1.value = get_int(node1.smoke); break;
                        case 7: vm1.value = get_int(node1.power); break;
                        case 8: vm1.value = get_int(node1.powerh); break;

                        default:
                            vm1.value = get_int(node1.powerh); break;
                    }
                }
                ViewModel.StatActive.Add(vm1);
            }
            return true;
        }

        private int get_int(int? nullable)
        {
            int int1 = nullable ?? 0;

            if (int1 != 0)
                int1 = int1 / 10;   // 소숫점 처리용 
            return int1;
        }

        private async Task<bool> getDBList()
        {
            await initData();

            initListView();
            this.DataContext = ViewModel;
            return true;
        }

        // 리스트 뷰에 그리드 컬럼을 동적 생성 토록 변경 
        private void initListView()
        {
        }

        private void getDBList2()
        {
            var tdb2 = from apl in g.location_list
                       where apl.site_id == g.selected_site_id
                       where (apl.location_level == 4 || apl.location_level == 5 || apl.location_level == 6 || apl.location_level == 7)
                       orderby apl.location_id
                       select new locationView()
                       {
                           location_id = apl.location_id,
                           location_name = apl.location_name,
                           location_path = apl.location_path,
                           location_building = getBuildingName(apl.building_id),
                           location_floor = getFloorName(apl.floor_id),
                           location_room = getroomName(apl.room_id),
                           location_rack = getrackName(apl.rack_id),
                           site_id = apl.site_id,
                           building_id = apl.building_id ?? 0,
                           floor_id = apl.floor_id ?? 0,
                       };
            _location_list = tdb2.ToList();

            _location_list.Insert(0, new locationView { location_id = 0, location_building = "--All Field (default)--" });
            cboType1.ItemsSource = _location_list;
            cboType1.SelectedIndex = 0;
        }

        private string getBuildingName(int? id)
        {
            int lid = id ?? 0;
            string ret = "";

            if (lid == 0) return ret;
            var a1 = g.building_list.Find(p => p.building_id == lid);
            if (a1 != null)
            {
                ret = a1.building_name;
            }
            return ret;
        }

        private string getFloorName(int? id)
        {
            int lid = id ?? 0;
            string ret = "";

            if (lid == 0) return ret;
            var a1 = g.floor_list.Find(p => p.floor_id == lid);
            if (a1 != null)
            {
                ret = a1.floor_name;
            }
            return ret;
        }

        private string getroomName(int? id)
        {
            int lid = id ?? 0;
            string ret = "";

            if (lid == 0) return ret;
            var a1 = g.room_list.Find(p => p.room_id == lid);
            if (a1 != null)
            {
                ret = a1.room_name;
            }
            return ret;
        }

        private string getrackName(int? id)
        {
            int lid = id ?? 0;
            string ret = "";

            if (lid == 0) return ret;
            var a1 = g.rack_list.Find(p => p.rack_id == lid);
            if (a1 != null)
            {
                ret = a1.rack_name;
            }
            return ret;
        }

        #endregion

        // 자산명 검색 처리 
        private void _btnSearch1_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            getDBList();
            this.Cursor = null;
        }

        // 엑셀 출력 처리 
        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            ExportStatistics es = new ExportStatistics();
            es.subtitle = _txtSelect.Text;

            es.out_list1 = new List<ChartData>(ViewModel.StatActive);
            es.save_2_excel(templatefile, 1);
        }

        private void _chart1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _chart1.OnApplyTemplate (); 
        }

        private void cbodatemonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int c1 = _duration_year[cbodateyear.SelectedIndex].date;
            int c2 = _duration_month[cbodatemonth.SelectedIndex].date;

            if (c2 < 1) return;
            DateTime d1 = new DateTime(c1, c2, 1);
            DateTime d2 = d1.AddMonths(1).AddDays(0 - d1.Day);

            _duration_day.Clear();
            for (int i = 1; i <= d2.Day; i++)
            {
                var aa = new DurationDate { date = i, sdate = i.ToString() };
                _duration_day.Add(aa);
            }
            _duration_day.Insert(0, new DurationDate { date = 0, sdate = "--All Field (default)--" });
            cbodateday.ItemsSource = null;
            cbodateday.ItemsSource = _duration_day;
            cbodateday.SelectedIndex = 0;

        }
    }

}

