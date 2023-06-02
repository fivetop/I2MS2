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

    public partial class StatTerminalHour : Window
    {
        // 출력할 테이블 
        List<sp_stat_terminal_data_hour_Result> terminal_data_hour_list;         

        List<locationView> _location_list = new List<locationView>();

        List<DurationDate> _duration_year = new List<DurationDate>();
        List<DurationDate> _duration_month = new List<DurationDate>();
        List<DurationDate> _duration_day = new List<DurationDate>();

        public DataViewModel ViewModel { get; set; }

        int itotalactive = 1; // total 0 , active  1

        public StatTerminalHour()
        {
            InitializeComponent();

            ViewModel = new DataViewModel();
            this.DataContext = ViewModel;

            ViewModel.StatTotal = new ObservableCollection<ChartData>();
            ViewModel.StatActive = new ObservableCollection<ChartData>();

            init_data();
            getDBList();
        }

        private void init_data()
        {
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

            for (int i = 1; i <= d2.Day; i++)
            {
                var aa = new DurationDate { date = i, sdate = i.ToString() };
                _duration_day.Add(aa);
            }
            cbodateyear.ItemsSource = _duration_year;
            cbodateyear.SelectedIndex = _duration_year.FindIndex(p => p.date == d1.Year);
            cbodatemonth.ItemsSource = _duration_month;
            cbodateday.ItemsSource = _duration_day;
            _duration_month.Insert(0, new DurationDate { date = 0, sdate = "--All Field (default)--"});
            _duration_day.Insert(0, new DurationDate { date = 0, sdate = "--All Field (default)--"});
            cbodatemonth.SelectedIndex = 0;
            cbodateday.SelectedIndex = 0;
        }

        #region init 로직
        private async Task<bool> initData()
        {
            int c1 = _duration_year[cbodateyear.SelectedIndex].date;
            int c2 = _duration_month[cbodatemonth.SelectedIndex].date;
            int c3 = _duration_day[cbodateday.SelectedIndex].date;

            string filter;
            if (c2 == 0)
            {
                filter = string.Format("?site_id=0&year={0}&month=0&day=0", c1);
                _txtSelect.Text = "'" +c1.ToString() ;

            }
            else if (c3 == 0)
            {
                filter = string.Format("?site_id=0&year={0}&month={1}&day=0", c1, c2);
                _txtSelect.Text = "'" +c1.ToString() + "/" + c2.ToString();

            }
            else
            { 
                filter = string.Format("?site_id=0&year={0}&month={1}&day={2}", c1, c2, c3);
                _txtSelect.Text = "'" +c1.ToString() + "/" + c2.ToString() + "/" + c3.ToString();

            }
            var v1 = (List<sp_stat_terminal_data_hour_Result>)await g.webapi.getList("sp_stat_terminal_data_hour", typeof(List<sp_stat_terminal_data_hour_Result>), filter);
            if (v1 == null) return false;
            if (v1.Count == 0)
            {
                terminal_data_hour_list = v1.ToList();
                return false;
            }

            terminal_data_hour_list = v1.ToList();
            return true;
        }

        private async Task<bool> getDBList()
        {
            await initData();

            int i = 0;

            ViewModel.StatActive.Clear();
            ViewModel.StatTotal.Clear();

            for (i = 0; i < 24; i++)
            {
                ChartData vm1 = new ChartData()
                {
                    category = i.ToString()
                };
                ChartData vm2 = new ChartData()
                {
                    category = i.ToString()
                };

                var node1 = terminal_data_hour_list.Find(p => p.time == i);
                if (node1 != null)
                {
                    vm1.value = node1.avg_of_act_terminal ?? 0;
                    vm2.value = node1.avg_of_tot_terminal ?? 0;
                }
                ViewModel.StatActive.Add(vm1);
                ViewModel.StatTotal.Add(vm2);
            }

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
            es.title = I2MSR.Properties.Resources.ResourceManager.GetString("Menu_StatTerminal3");
            es.subtitle1 = I2MSR.Properties.Resources.ResourceManager.GetString("C_Search");
            es.subtitle2 = I2MSR.Properties.Resources.ResourceManager.GetString("C_Hour");
            es.subtitle3 = I2MSR.Properties.Resources.ResourceManager.GetString("C_Value");

            if (itotalactive == 0)
                es.out_list1 = new List<ChartData>(ViewModel.StatTotal);
            else
                es.out_list1 = new List<ChartData>(ViewModel.StatActive);
            es.save_2_excel("stat_terminal_hour", 1);
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            _series1.ItemsSource = ViewModel.StatTotal;
            itotalactive = 0;
        }

        private void RadioButton_Click_1(object sender, RoutedEventArgs e)
        {
            _series1.ItemsSource = ViewModel.StatActive;
            itotalactive = 1;
        }
        private void _chart1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _chart1.OnApplyTemplate();
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
            cbodateday.ItemsSource = null;
            cbodateday.ItemsSource = _duration_day;
            _duration_day.Insert(0, new DurationDate { date = 0, sdate = "--All Field (default)--" });
            cbodateday.SelectedIndex = 0;
        }

    }

}
