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

#pragma warning disable 4014

namespace I2MS2.Windows
{
    /// <summary>
    /// ManufactureManager.xaml에 대한 상호 작용 논리
    /// </summary>

    public partial class StatEnvironmentDay : Window
    {
        // 출력할 테이블 
        List<sp_stat_eb_day_Result> stat_eb_day_list;           // 
        List<sp_stat_eb_month_Result> stat_eb_month_list;       // 

        List<DurationDate> _duration_year = new List<DurationDate>();
        List<DurationDate> _duration_month = new List<DurationDate>();

        public DataViewModel ViewModel { get; set; }

        string templatefile = "";

        string[] sel_name = new string[] { "Power Status (Kwh)", "Temperature Status (ºC)", "Humidity Status (%)", "Door Status (EA)", "Power Peek Status (Kwh)", "Temperature Peek Status (ºC)", "Humidity Peek Status (%)" };

        public StatEnvironmentDay()
        {
            InitializeComponent();

            ViewModel = new DataViewModel();
            this.DataContext = ViewModel;

            ViewModel.StatActive = new ObservableCollection<ChartData>();
            ViewModel.SelectedFontSize = 11.0;
            ViewModel.MarginBinding = new Thickness(5,0,5,0);
            init_data();
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

            cbodateyear.ItemsSource = _duration_year;
            cbodateyear.SelectedIndex = _duration_year.FindIndex(p => p.date == d1.Year);
            cbodatemonth.ItemsSource = _duration_month;
            cbodatemonth.SelectedIndex = _duration_month.FindIndex(p => p.date == d1.Month);

            for (int i = 0; i < 7; i++)
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
            int c4 = cboType2.SelectedIndex;

            string filter;

            if (c2 == 0)
            {
                filter = string.Format("?location_id=0&year={0}&month=0", c1);
                _txtSelect.Text = "'" +c1.ToString() + " : " + sel_name[c4];
            }
            else
            {
                filter = string.Format("?location_id=0&year={0}&month={1}", c1, c2);
                _txtSelect.Text = "'" +c1.ToString() + "/" + c2.ToString() + " : " + sel_name[c4];
            }

            var v1 = (List<sp_stat_eb_day_Result>)await g.webapi.getList("sp_stat_eb_day", typeof(List<sp_stat_eb_day_Result>), filter);
            if (v1 == null) return false;
            //if (v1.Count == 0) return false;

            stat_eb_day_list = v1.ToList();
            templatefile = "stat_eb_day";
            ViewModel.StatActive.Clear();

            if (c2 == 0)
            {
                for (int i = 0; i < 12; i++)
                {
                    ChartData vm1 = new ChartData() { category = (i + 1).ToString() };

                    var node1 = stat_eb_month_list.Find(p => p.month == (i + 1));
                    if (node1 != null)
                    {
                        switch (c4)
                        {
                            case 0: vm1.value = get_int(node1.powerh); break;
                            case 1: vm1.value = get_int(node1.temperature); break;
                            case 2: vm1.value = get_int(node1.humidity); break;
                            case 3: vm1.value = node1.door ?? 0; break;
                            case 4: vm1.value = get_int(node1.power_peek); break;
                            case 5: vm1.value = get_int(node1.temperature_peek); break;
                            case 6: vm1.value = get_int(node1.humidity_peek); break;
                        }
                        
                    }
                    ViewModel.StatActive.Add(vm1);
                }
            }
            else 
            {
                DateTime d1 = new DateTime(c1, c2, 1);
                int days = d1.AddMonths(1).AddDays(0 - d1.Day).Day;

                for (int i = 0; i < days; i++)
                {
                    ChartData vm1 = new ChartData() { category = (i + 1).ToString() };

                    var node1 = stat_eb_day_list.Find(p => p.day == (i + 1));
                    if (node1 != null)
                    {
                        switch (c4)
                        {
                            case 0: vm1.value = get_int(node1.powerh); break;
                            case 1: vm1.value = get_int(node1.temperature); break;
                            case 2: vm1.value = get_int(node1.humidity); break;
                            case 3: vm1.value = node1.door ?? 0; break;
                            case 4: vm1.value = get_int(node1.power_peek); break;
                            case 5: vm1.value = get_int(node1.temperature_peek); break;
                            case 6: vm1.value = get_int(node1.humidity_peek); break;
                        }
                    }
                    ViewModel.StatActive.Add(vm1);
                }
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
    }

}

