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

    public partial class StatTerminalMonth : Window
    {
        // 출력할 테이블 
        List<sp_stat_terminal_data_month_Result> terminal_data_list;         

        List<locationView> _location_list = new List<locationView>();
        List<DurationDate> _duration_year = new List<DurationDate>();
        List<DurationDate> _duration_month = new List<DurationDate>();

        public DataViewModel ViewModel { get; set; }

        int itotalactive = 1; // total 0 , active  1

        public StatTerminalMonth()
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
            var tdb2 = from apl in g.location_list
                       where apl.site_id == g.selected_site_id
                       where (apl.location_level == 4 || apl.location_level == 5)
                       orderby apl.location_id
                       select new locationView()
                       {
                           location_id = apl.location_id,
                           location_name = apl.location_name,
                           location_path = apl.location_path,
                           site_id = apl.site_id,
                           building_id = apl.building_id ?? 0,
                           floor_id = apl.floor_id ?? 0,
                       };
            _location_list = tdb2.ToList();

            _location_list.Insert(0, new locationView { location_id = 0, location_path = "--All Field (default)--" });
            cboType1.ItemsSource = _location_list;
            cboType1.SelectedIndex = 0;
            
            for (int i = 2013; i < 2040; i++)
            {
                var aa = new DurationDate { date = i };
                _duration_year.Add(aa);
            }

            DateTime now = DateTime.Today;
            cbodateyear.ItemsSource = _duration_year;
            cbodateyear.SelectedIndex = _duration_year.FindIndex(p => p.date == now.Year);
        }

        #region init 로직
        private async Task<bool> initData()
        {
            int c1 = _duration_year[cbodateyear.SelectedIndex].date;
            DateTime d1 = new DateTime(c1, 1, 1);
            DateTime d2 = d1.AddMonths(12).AddDays(0 - d1.Day);

            locationView s1 = (locationView)cboType1.SelectedItem;
            
            string filter2 = string.Format("?location_id={0}&year={1}", s1.location_id, d1.Year);
            if (s1.location_id == 0)
            { 
                int location_id = Etc.get_location_id_by_site_id(g.selected_site_id);
                filter2 = string.Format("?location_id={0}&year={1}", location_id, d1.Year);
                _txtSelect.Text = "'" +d1.Year.ToString();
            }
            else
            {
                _txtSelect.Text = "'" +s1.location_path + "      : " + d1.Year.ToString();
            }

            var v2 = (List<sp_stat_terminal_data_month_Result>)await g.webapi.getList("sp_stat_terminal_data_month", typeof(List<sp_stat_terminal_data_month_Result>), filter2);
            if (v2 == null) return false;
            if (v2.Count == 0)
            {
                terminal_data_list = v2.ToList();
                return false;
            }
            terminal_data_list = v2.ToList();

            return true;
        }

        private async Task<bool> getDBList()
        {
            await initData();

            int i = 0;

            ViewModel.StatActive.Clear();
            ViewModel.StatTotal.Clear();

            for (i = 0; i < 12; i++)
            {
                ChartData vm1 = new ChartData()
                {
                    category = (i + 1).ToString()
                };
                ChartData vm2 = new ChartData()
                {
                    category = (i + 1).ToString()
                };

                var node1 = terminal_data_list.Find(p => p.month == (i+1));
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
            es.title = I2MSR.Properties.Resources.ResourceManager.GetString("Menu_StatTerminal1");
            es.subtitle1 = I2MSR.Properties.Resources.ResourceManager.GetString("C_Search");
            es.subtitle2 = I2MSR.Properties.Resources.ResourceManager.GetString("C_Month");
            es.subtitle3 = I2MSR.Properties.Resources.ResourceManager.GetString("C_Value");

            if (itotalactive == 0)
                es.out_list1 = new List<ChartData>(ViewModel.StatTotal);
            else
                es.out_list1 = new List<ChartData>(ViewModel.StatActive);
            es.save_2_excel("stat_terminal_month", 1);
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

    }

}

