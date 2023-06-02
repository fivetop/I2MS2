using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

#pragma warning disable 4014

namespace I2MS2.Windows
{
    public class EnvironmentPrintList : PropertyData
    {
        public EnvironmentPrintList()
        {
            this.node_list = new List<EnvironmentPrintList>();
        }
        public List<EnvironmentPrintList> node_list { get; set; }


        public int RowNumber { get; set; }
        public string last_updated_string { get; set; }
        public string date { get; set; }

        // 선번장 출력용 
        public int time_0_23 { get; set; }
        public Nullable<int> power_ph { get; set; }
        public Nullable<int> sensor_t { get; set; }
        public Nullable<int> sensor_h { get; set; }
        public Nullable<int> door { get; set; }
    }

    /// <summary>
    /// ManufactureManager.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class EnvironmentList : Window
    {
        #region RouteCommand 버튼 관련 정의
        public static RoutedCommand PrintCommand = new RoutedCommand();
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand DeleteCommand = new RoutedCommand();

        private bool _print = true;
        private bool _save = false;
        private bool _delete = false;
        #endregion

        // 리포트 관련 테이블
        List<report> _report_list = null;
        List<template> _template_list = null;
        List<template_column> _template_column_list = null;
        List<lvCompare> _lvcompare = new List<lvCompare>();
        List<listHeader> _listHeader = new List<listHeader>();

        template _left_item = null;
        template_column _left_column_item = null;

        // 출력할 테이블 
        List<EnvironmentPrintList> _print_list = new List<EnvironmentPrintList>();
        List<eb_port_data_hour> eb_port_data_hour_list;         // 누적
        List<locationView> _location_list = new List<locationView>();

        // 출력 이름 
        DateTime now = DateTime.Now;
        string title2;
        int report_id = 1120010;

        private bool blive = false;

        public EnvironmentList()
        {
            InitializeComponent();

            _report_list = g.report_list.ToList();
            _template_list = g.template_list.ToList();
            _template_column_list = g.template_column_list.ToList();

            title2 = now.ToString("yyyyMMdd");
            txtsave_name.Text = "";

            txt_sdate1.SelectedDate = DateTime.Today;
            txt_sdate2.SelectedDate = DateTime.Today; 

            comboboxUpdate(0);
            getDBList();
        }

        #region CRUD 신규,삭제 등 버튼 처리 로직
        private void _cmdPrint_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _print;
        }
        private void _cmdPrint_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }

        private void _cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _save;
        }
        private async void _cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!await saveLeft())
                return;
            _save = false;
        }

        private void _cmdDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _delete;
        }
        private async void _cmdDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!await deleteLeft())
                return;
            _delete = false;
        }

        #endregion

        #region init 로직

        private async Task<bool> initData()
        {
            // eb_port_data_hour_list 와 eb_port_data_cur_list 가져오기 
            string filter = "";
            var v1 = (List<eb_port_data_hour>)await g.webapi.getList("eb_port_data_hour", typeof(List<eb_port_data_hour>), filter);
            if (v1 == null) return false;

            string sdate1 = txt_sdate1.Text.ToString().Trim();
            string sdate2 = txt_sdate2.Text.ToString().Trim();

            var d1 = Convert.ToDateTime(sdate1);
            var d2 = Convert.ToDateTime(sdate2) + new TimeSpan(23, 59, 59);

            var tdb1 = from a in v1
                       where Convert.ToDateTime(a.date) >= d1 && Convert.ToDateTime(a.date) <= d2
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
                       };


            var t1 = from b in tdb1
                     group b by new { b.date, b.time_0_23 } into a
                     select new eb_port_data_hour()
                     {
                         asset_id = a.First().asset_id,
                         date = a.Key.date,
                         time_0_23 = a.Key.time_0_23,
                         power_ph = (a.Sum(c => c.power_ph) / a.Sum(c => c.power_ph_cnt)) / 10,
                         sensor_t = (a.Sum(c => c.sensor_t) / a.Sum(c => c.sensor_t_cnt)) / 10,
                         sensor_h = (a.Sum(c => c.sensor_h) / a.Sum(c => c.sensor_h_cnt)) / 10,
                         door = a.Sum(c => c.door),
                         last_updated = a.First().last_updated,
                     };


            eb_port_data_hour_list = t1.ToList();

            return true;
        }

        private async Task<bool> getDBList()
        {
            await initData();

            // 데이터 취합 
            int i = 1;
            var tdb1 = from a in eb_port_data_hour_list 
                       join b in g.asset_list on a.asset_id equals b.asset_id
                       orderby a.asset_id
                       select new EnvironmentPrintList()
                       {
                           asset_name = getAssetName(a.asset_id),
                           install_date = a.date,
                           date = a.date.ToShortDateString(),
                           time_0_23 = a.time_0_23,
                           power_ph = a.power_ph,
                           sensor_t = a.sensor_t,
                           sensor_h = a.sensor_h,
                           door = a.door,
                           last_updated = a.last_updated,
                          
                           RowNumber = i++,
                           last_updated_string = a.last_updated.ToShortDateString(),
                       };

            _print_list = tdb1.ToList();

            string str1 = " / " + g.select_site.site_name;
            txtadd.Text = "Record Count (" + Convert.ToString(_print_list.Count) + ")" + str1;

            initListView();

            return true;
        }

        private string getAssetName(int? id)
        {
            int lid = id ?? 0;
            string ret = "";

            if (lid == 0)
                return ret;
            var a1 = g.asset_list.Find(p => p.asset_id == lid);
            if (a1 != null)
            {
                ret = string.Format("{0}", a1.asset_name);
            }
            return ret;
        }


        // 리스트 뷰에 그리드 컬럼을 동적 생성 토록 변경 
        private void initListView()
        {
            // 리소스 스트링을 디비화 할 필요 있음 
            _listHeader.Clear();

            _listHeader.Add(new listHeader { h_width = 0, h_title = "ID", h_bind = "asset_id" });
            _listHeader.Add(new listHeader { h_width = 60, h_title = "C_No", h_bind = "RowNumber" });
            _listHeader.Add(new listHeader { h_width = 160, h_title = "C_Name", h_bind = "asset_name" });
            _listHeader.Add(new listHeader { h_width = 120, h_title = "C_Date", h_bind = "date" });
            _listHeader.Add(new listHeader { h_width = 60, h_title = "C_Time", h_bind = "time_0_23" });
            _listHeader.Add(new listHeader { h_width = 120, h_title = "C_Power", h_bind = "power_ph" });
            _listHeader.Add(new listHeader { h_width = 120, h_title = "C_Temperature", h_bind = "sensor_t" });
            _listHeader.Add(new listHeader { h_width = 120, h_title = "C_Humidity", h_bind = "sensor_h" });
            _listHeader.Add(new listHeader { h_width = 120, h_title = "C_Door", h_bind = "door" });
            
            _lvGridView.Columns.Clear();
            // 동적 생성 
            for (int i = 0; i < _listHeader.Count; i++)
            {
                listHeader l1 = _listHeader[i];
                TextBlock t1 = new TextBlock();
                t1.Text = I2MSR.Properties.Resources.ResourceManager.GetString(l1.h_title);
                t1.Style = Application.Current.Resources["I2MS_ListViewColHeaderText"] as Style;
                Border b2 = new Border();
                b2.BorderThickness = new Thickness(0);
                b2.Child = t1;

                GridViewColumn g1 = new GridViewColumn();
                g1.DisplayMemberBinding = new Binding(l1.h_bind);
                if (i != 0 || i != 2)
                {
                    GridViewExtensions.SetIsContentCentered(g1, true);
                }
                g1.Header = b2;
                g1.Width = l1.h_width;
                _lvGridView.Columns.Add(g1);
            }
            _lvManufacture.ItemsSource = _print_list; // _manufacture_list;

            // 메뉴 동적 생성 
            GridView v1 = (GridView)_lvManufacture.View;
            GridViewColumn b1 = (GridViewColumn)v1.Columns[0];
            TextBlock s2 = (TextBlock)((Border)v1.Columns[0].Header).Child;

            _lvManufacture.ContextMenu.Items.Clear();
            for (int i = 1; i <= v1.Columns.Count - 1; i++)
            {
                s2 = (TextBlock)((Border)v1.Columns[i].Header).Child;
                if (v1.Columns[i].Width == 0)
                {
                    continue;
                }
                MenuItem m1 = new MenuItem();
                m1.Header = s2.Text;
                m1.IsCheckable = true;
                m1.IsChecked = true;
                m1.Click += new RoutedEventHandler(menu_Click);
                _lvManufacture.ContextMenu.Items.Add(m1);
                ((System.ComponentModel.INotifyPropertyChanged)v1.Columns[i]).PropertyChanged += gridViewColumn_INotifyPropertyChanged;
            }
            // 이벤트 추가 
            v1.Columns.CollectionChanged += gridView_CollectionChanged;

            // 리트트뷰의 원래 위치와 속성을 비교할 클래스에 저장 
            _lvcompare.Clear();
            for (int i = 1; i <= v1.Columns.Count - 1; i++)
            {
                b1 = (GridViewColumn)v1.Columns[i];
                s2 = (TextBlock)((Border)v1.Columns[i].Header).Child;
                lvCompare pl = new lvCompare();
                pl.orgid = pl.newid = i;
                pl.orggvcWidth = b1.Width;
                pl.newgvc = b1;
                pl.newtb = s2;
                _lvcompare.Add(pl);
            }
        }

        private void gridViewColumn_INotifyPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ActualWidth")
            {
                _save = true;
            }
        }

        #endregion

        #region 각종 이벤트 핸들러 처리
        private void gridView_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Move)
            {
                GridView v1 = (GridView)_lvManufacture.View;
                TextBlock s2 = (TextBlock)((Border)v1.Columns[e.OldStartingIndex].Header).Child;
                TextBlock s3 = (TextBlock)((Border)v1.Columns[e.NewStartingIndex].Header).Child;

                lvCompare t2 = _lvcompare.Find(p => (p.newtb == s2));
                t2.newid = e.OldStartingIndex;
                lvCompare t3 = _lvcompare.Find(p => (p.newtb == s3));
                t3.newid = e.NewStartingIndex;

                _save = true;
            }
        }

        // 메뉴를 사용 특정 아이템을 보이기 처리시 사용 
        private void menu_Click(object sender, RoutedEventArgs e)
        {
            MenuItem t1 = e.Source as MenuItem;
            string t2 = t1.Header.ToString();
            GridView v1 = (GridView)_lvManufacture.View;
            GridViewColumn v2 = (GridViewColumn)v1.Columns[1];
            TextBlock s3 = (TextBlock)((Border)v1.Columns[1].Header).Child;

            for (int i = 1; i <= v1.Columns.Count - 1; i++)
            {
                s3 = (TextBlock)((Border)v1.Columns[i].Header).Child;
                if (s3.Text == t2)
                {
                    v2 = (GridViewColumn)v1.Columns[i];
                    if (t1.IsChecked == false)
                        v2.Width = 0;
                    else
                        v2.Width = 120;
                }
            }
            _save = true;
        }

        // 콤보를 사용 출력 템플릿이 변경되면 화면을 갱신 하여 준다. 
        private void cboTypeTemplate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridView v1 = (GridView)_lvManufacture.View;
            if (v1.Columns.Count == 0) return;
            TextBlock s3 = (TextBlock)((Border)v1.Columns[0].Header).Child;

            var v = cboTypeTemplate.SelectedValue;
            if (v == null)
                return;
            string name = ((template)(cboTypeTemplate.SelectedItem)).template_name;

            int i1 = (int)v;
            if (i1 == 0)
            {
                if (blive == false)
                    return;
                else
                {
                    // 삭제나 기본 출력이 선택되면 수행 처리 
                    for (int i = 1; i < _lvcompare.Count + 1; i++)
                    {
                        lvCompare pl = _lvcompare.Find(p => (p.orgid == i));
                        if (pl.newid != pl.orgid)
                        {
                            v1.Columns.Move(pl.newid, pl.orgid);
                            pl.newid = pl.orgid;
                        }
                        pl.newgvc.Width = pl.orggvcWidth;
                    }
                    _delete = false;
                    _save = false;
                    txtsave_name.Text = ""; //  title2 + " 제조사 목록";
                    return;
                }
            }

            // 선택된 내용으로 디비를 읽어와서 리스트 채우기 
            var t1 = _template_list.Find(p => (p.template_id == i1));
            if (t1 == null) return;
            var t2_l = _template_column_list.Where(p => (p.template_id == t1.template_id)).ToList();

            ContextMenu m1 = _lvManufacture.ContextMenu;

            for (int i = 1; i < t2_l.Count + 1; i++)
            {
                template_column tc = t2_l.Find(p => (p.template_column_no == i));
                lvCompare pl = _lvcompare.Find(p => (p.orgid == i));

                if (pl != null)
                {
                    if ((pl.newid != tc.report_column_no) && (pl.orgid != tc.report_column_no))
                    {
                        v1.Columns.Move(pl.newid, tc.report_column_no);
                    }
                    else if ((pl.newid != tc.report_column_no))
                    {
                        v1.Columns.Move(pl.newid, tc.report_column_no);
                    }
                    pl.newgvc.Width = (double)(tc.column_width ?? 100.0);
                    MenuItem m2 = (MenuItem)m1.Items[i - 1];
                    if (tc.column_width == 0)
                        m2.IsChecked = false;
                    else
                        m2.IsChecked = true;
                }
            }
            blive = true;
            _delete = true;
            _save = false;

            txtsave_name.Text = name; //  title2 + " 제조사 목록";
        }

        #endregion

        #region add, edit, save 로직, delete 로직, 프린터 출력 컨버트 로직(Db <- Screen, Screen <- Db)

        // 메모리에 화면 내용을 옮김
        private bool contents2memdb(bool new_flag)
        {
            var item = _left_item;
            if (item == null)
                return false;
            item.report_id = report_id;
            item.template_name = txtsave_name.Text.Trim();
            item.num_of_template_column = 8;
            item.user_id = g.login_user_id;
            item.remarks = "";
            item.last_updated2 = DateTime.Now;
            return true;
        }
        private async Task<bool> saveLeft()
        {
            int add_id = 0;

            // template 저장
            string name = txtsave_name.Text.Trim();

            if (chkdata(name, "", 10001)) return false;

            if (MessageBox.Show(name + g.tr_get("C_Error14"), "Save", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return false;
            }
            var temp = _template_list.Find(p => p.template_name == name);
            if (temp != null)
            {
                // 기존 디비 삭제 처리 먼저 한다.
                // 1 차일드 디비 와 메모리 삭제 처리 
                string filter = string.Format("?template_id={0}", temp.template_id);
                var r1 = await g.webapi.delete("template_column", filter);
                g.template_column_list.RemoveAll(p => p.template_id == temp.template_id);
                _template_column_list.RemoveAll(p => p.template_id == temp.template_id);

                // 2. 메인 디비 와 메모리 삭제 
                int rr1 = await g.webapi.delete("template", temp.template_id);
                if (rr1 != 0)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }
                g.template_list.RemoveAll(p => p.template_id == temp.template_id);
                _template_list.RemoveAll(p => p.template_id == temp.template_id);
            }
            // 3. 추가 메모리 에서 카피
            _left_item = new template() { template_id = 0 };
            if (chkdata(contents2memdb(true), false, 10003)) return false;

            // 4. DB에 추가 처리 
            var out_node = (template)await g.webapi.post("template", _left_item, typeof(template));
            if (chkdata(out_node, null, 10004)) return false;

            // 5. 메모리 추가 처리 
            g.template_list.Add(out_node);
            add_id = out_node.template_id;

            // 6. 화면 갱신 처리 
            _template_list = g.template_list.ToList();

            int w1 = 0;

            // 9. template_column 저장 디비 와 메모리 동시 처리 
            for (int i = 0; i < _lvcompare.Count; i++)
            {
                _left_column_item = new template_column();
                lvCompare t1 = _lvcompare[i];
                _left_column_item.template_id = add_id;
                _left_column_item.template_column_no = t1.orgid;
                _left_column_item.report_column_no = t1.newid;
                w1 = (int)t1.newgvc.ActualWidth;
                _left_column_item.column_width = w1;

                var out_node2 = (template_column)await g.webapi.post("template_column", _left_column_item, typeof(template_column));
                if (chkdata(out_node2, null, 10004)) return false;

                g.template_column_list.Add(out_node2);
            }
            // 10. 차일드 메모리 갱신용 
            _template_column_list = g.template_column_list.ToList(); // 화면용 리스트 갱신 

            // 11. 콤보 박스 업데이트 하기 
            comboboxUpdate(add_id);
            return true;
        }

        private void comboboxUpdate(int id)
        {
            var list2 = g.template_list.Where(p => p.report_id == report_id).ToList();
            list2.Insert(0, new template { template_id = 0, template_name = "--All Field (default)--" });
            cboTypeTemplate.ItemsSource = list2;
            cboTypeTemplate.SelectedValue = id;
            cboTypeTemplate_SelectionChanged(cboTypeTemplate, null);
        }

        private bool chkdata(object name, object p1, int p2)
        {
            switch (p2)
            {
                case 10001:
                    if (name == p1)
                    {
                        MessageBox.Show(g.tr_get("C_Error12"));
                        return true;
                    }
                    break;
                case 10002:
                    if (name == p1)
                        return true;
                    else
                    {
                        MessageBox.Show(g.tr_get("C_Error11"));
                        return false;
                    }
                case 10003:
                    if (name == p1)
                    {
                        MessageBox.Show(g.tr_get("C_Error_Server"));
                        return true;
                    }
                    break;
                case 10004:
                    if (name == p1)
                    {
                        MessageBox.Show(g.tr_get("C_Error_Server"));
                        return true;
                    }
                    break;
            }
            return false;
        }

        private async Task<bool> deleteLeft()
        {
            string name = ((template)(cboTypeTemplate.SelectedItem)).template_name;
            var v = cboTypeTemplate.SelectedValue;
            if (v == null) return false;
            int delete_id = (int)v;

            if (chkdata(name, "", 10001))
            {
                _delete = false;
                return false;
            }
            if (MessageBox.Show(name + g.tr_get("C_Error13"), "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return false;
            }

            // 서브 레코드 먼저 필터링후 삭제 
            string filter = string.Format("?template_id={0}", delete_id);
            var r1 = await g.webapi.delete("template_column", filter);

            // 메인 삭제 
            int rr1 = await g.webapi.delete("template", delete_id);
            if (rr1 != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }

            g.template_list.RemoveAll(p => p.template_id == delete_id);
            _template_list.RemoveAll(p => p.template_id == delete_id);
            g.template_column_list.RemoveAll(p => p.template_id == delete_id);
            _template_column_list.RemoveAll(p => p.template_id == delete_id);

            comboboxUpdate(0);
            return true;
        }
        #endregion

        #region  프린터 출력 컨버트 로직(Db <- Screen, Screen <- Db)
        // 프린터 출력 
        private void _btnPrint_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            iPrint ip = new iPrint();
            ip.p_name = title2 + g.tr_get("C_Report3");
            ip.p_title1 = g.tr_get("C_Report3");
            ip.p_title2 = "template_environment_list";
            ip.anaylize(_lvManufacture);

            for (int row = 0; row < _print_list.Count; row++)
            {
                var mp = _print_list[row];

                ip.oTable.RowGroups[0].Rows.Add(new TableRow());
                ip.r1 = ip.oTable.RowGroups[0].Rows[row + 1];
                ip.r1.Background = System.Windows.Media.Brushes.Azure;
                ip.r1.Foreground = System.Windows.Media.Brushes.Navy;
                ip.r1.FontSize = 10;

                string temp = "";
                // template_column 저장
                for (int i = 0; i < _lvcompare.Count; i++)
                {
                    lvCompare t1 = _lvcompare[i];

                    if (t1.newgvc.Width == 0)
                        continue;
                    switch (t1.newid)
                    {
                        case 1: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.RowNumber.ToString())))); break;
                        case 2: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.asset_name)))); break;
                        case 3: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.date)))); break;
                        case 4: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.time_0_23.ToString())))); break;
                        case 5: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.power_ph.ToString())))); break;
                        case 6: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.sensor_t.ToString())))); break;
                        case 7: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.sensor_h.ToString())))); break;
                        case 8: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.door.ToString())))); break;
                    }
                }
            }
            PrintPreView winPrint = new PrintPreView(ip);
            winPrint.Owner = MainWindow.GetWindow(this);
            this.Cursor = null;

            winPrint.ShowDialog();
        }

        #endregion

        // 자산명 검색 처리 
        private void _btnSearch1_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            getDBList();
            this.Cursor = null;
        }
    }

}

