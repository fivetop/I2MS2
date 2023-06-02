using I2MS2.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WebApi.Models;
using I2MS2.Windows;
using I2MS2.Library;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Documents;
using System.Diagnostics;

namespace I2MS2.Windows
{
    public class CatalogPrintList : INotifyPropertyChanged
    {
        public CatalogPrintList()
        {
            this.node_list = new List<CatalogPrintList>();
        }
        public List<CatalogPrintList> node_list { get; set; }

        public int RowNumber { get; set; }
        public int catalog_id { get; set; }
        public int catalog_group_id { get; set; }
        public string catalog_name { get; set; }
        public string catalog_full_name { get; set; }
        public Nullable<int> manufacture_id { get; set; }
        public string manufacture_nmae { get; set; }
        public string model_code { get; set; }
        public string order_code { get; set; }
        public Nullable<int> size_w { get; set; }
        public Nullable<int> size_d { get; set; }
        public Nullable<int> size_h { get; set; }
        public Nullable<decimal> weight { get; set; }
        public int num_of_ports { get; set; }
        public Nullable<int> image_id { get; set; }
        public Nullable<int> icon_16_image_id { get; set; }
        public Nullable<int> icon_32_image_id { get; set; }
        public Nullable<int> icon_48_image_id { get; set; }
        public Nullable<int> icon_64_image_id { get; set; }
        public Nullable<int> link_80_image_id { get; set; }
        public string deletable { get; set; }
        public string pp_use_intelligent { get; set; }
        public string pp_config_type { get; set; }
        public string pp_media_type { get; set; }
        public string pp_utp_jack_type { get; set; }
        public string pp_utp_shield_type { get; set; }
        public string pp_figure_type { get; set; }
        public Nullable<int> ic_num_of_pp_connectors { get; set; }
        public Nullable<int> ic_num_of_max_pp { get; set; }
        public Nullable<int> ic_num_of_power { get; set; }
        public Nullable<int> st_num_of_disk { get; set; }
        public string sw_figure_type { get; set; }
        public Nullable<int> sw_num_of_slots { get; set; }
        public string sw_model_type { get; set; }
        public string cp_plug_side { get; set; }
        public string ca_use_intelligent { get; set; }
        public string ca_install_type { get; set; }
        public string ca_for_army { get; set; }
        public string ca_media_type { get; set; }
        public string ca_is_utp_shield { get; set; }
        public string ca_utp_cable_type { get; set; }
        public string ca_fiber_cable_type { get; set; }
        public string ca_fiber_connector_type { get; set; }
        public Nullable<int> ca_disp_color_rgb { get; set; }
        public string ca_disp_name { get; set; }
        public string rm_is_rack_mount { get; set; }
        public Nullable<int> rm_unit_size { get; set; }
        public Nullable<int> rm_image_220_image_id { get; set; }
        public Nullable<int> rm_image_440_image_id { get; set; }
        public Nullable<int> rm_image_880_image_id { get; set; }
        public int user_id { get; set; }
        public string last_updated { get; set; }
        public string remarks { get; set; }

//        public int catalog_group_id { get; set; }
        public string catalog_group_name { get; set; }

        public bool force_changed
        {
            get { return true; }
            set { NotifyPropertyChanged(""); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

    }

    /// <summary>
    /// ManufactureManager.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CatalogList : Window
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
        List<CatalogPrintList> _print_list = new List<CatalogPrintList>();

        // 출력 이름 
        DateTime now = DateTime.Now;
        string title2;
        int report_id = 1120003;

        private bool blive = false;

        public CatalogList()
        {
            InitializeComponent();

            _report_list = g.report_list.ToList();
            _template_list = g.template_list.ToList();
            _template_column_list = g.template_column_list.ToList();

            title2 = now.ToString("yyyyMMdd");
            txtsave_name.Text = ""; //  title2 + " 제조사 목록";

            comboboxUpdate(0);
            getDBList();
            initListView();
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
        private void getDBList()
        {
            catalog_group c_default = new catalog_group
            {
                catalog_group_id = 0,
            };

            // 데이터 취합 
            int i = 1;

            var tdb1 = from a in g.catalog_list
                       join c in g.manufacture_list on a.manufacture_id equals c.manufacture_id 
                       join b in g.catalog_group_list on a.catalog_group_id equals b.catalog_group_id into ps
                       from b in ps.DefaultIfEmpty(c_default)
                       orderby a.catalog_id
                       select new CatalogPrintList()
                       {
                           catalog_id = a.catalog_id,
                           catalog_group_id = a.catalog_group_id,
                           catalog_name = a.catalog_name,
                           catalog_full_name = a.catalog_full_name,
                           manufacture_id = a.manufacture_id,
                           manufacture_nmae = c.manufacture_name,
                           model_code = a.model_code,
                           order_code = a.order_code,
                           size_w = a.size_w,
                           size_d = a.size_d,
                           size_h = a.size_h,
                           weight = a.weight,
                           num_of_ports = a.num_of_ports,
                           image_id = a.image_id,
                           icon_16_image_id = a.icon_16_image_id,
                           icon_32_image_id = a.icon_32_image_id,
                           icon_48_image_id = a.icon_48_image_id,
                           icon_64_image_id = a.icon_64_image_id,
                           link_80_image_id = a.link_80_image_id,
                           deletable = a.deletable,
                           pp_use_intelligent = a.pp_use_intelligent,
                           pp_config_type = a.pp_config_type,
                           pp_media_type = a.pp_media_type,
                           pp_utp_jack_type = a.pp_utp_jack_type,
                           pp_utp_shield_type = a.pp_utp_shield_type,
                           pp_figure_type = a.pp_figure_type,
                           ic_num_of_pp_connectors = a.ic_num_of_pp_connectors,
                           ic_num_of_max_pp = a.ic_num_of_max_pp,
                           ic_num_of_power = a.ic_num_of_power,
                           st_num_of_disk = a.st_num_of_disk,
                           sw_figure_type = a.sw_figure_type,
                           sw_num_of_slots = a.sw_num_of_slots,
                           sw_model_type = a.sw_model_type,
                           cp_plug_side = a.cp_plug_side,
                           ca_use_intelligent = a.ca_use_intelligent,
                           ca_install_type = a.ca_install_type,
                           ca_for_army = a.ca_for_army,
                           ca_media_type = a.ca_media_type,
                           ca_is_utp_shield = a.ca_is_utp_shield,
                           ca_utp_cable_type = a.ca_utp_cable_type,
                           ca_fiber_cable_type = a.ca_fiber_cable_type,
                           ca_fiber_connector_type = a.ca_fiber_connector_type,
                           ca_disp_color_rgb = a.ca_disp_color_rgb,
                           ca_disp_name = a.ca_disp_name,
                           rm_is_rack_mount = a.rm_is_rack_mount,
                           rm_unit_size = a.rm_unit_size,
                           rm_image_220_image_id = a.rm_image_220_image_id,
                           rm_image_440_image_id = a.rm_image_440_image_id,
                           rm_image_880_image_id = a.rm_image_880_image_id,
                           user_id = a.user_id,
                           last_updated = a.last_updated.GetDateTimeFormats().GetValue(76).ToString(),
                           remarks = a.remarks,
                           catalog_group_name = b.catalog_group_name,
                           RowNumber = i++,
                       };

            _print_list = tdb1.ToList();

            string str1 = " / " + g.select_site.site_name;
            txtadd.Text = "Record Count (" + Convert.ToString(_print_list.Count) + ")" + str1; 

        }


        // 리스트 뷰에 그리드 컬럼을 동적 생성 토록 변경 
        private void initListView()
        {
            // 리소스 스트링을 디비화 할 필요 있음 

            _listHeader.Add(new listHeader { h_width = 0, h_title = "ID", h_bind = "catalog_id" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "C_No", h_bind = "RowNumber" });
            _listHeader.Add(new listHeader { h_width = 120, h_title = "M_Prop3_1_Group", h_bind = "catalog_group_name" });
            _listHeader.Add(new listHeader { h_width = 200, h_title = "M_Prop3_1_Name", h_bind = "catalog_name" });
            _listHeader.Add(new listHeader { h_width = 200, h_title = "M_Prop3_1_FullName", h_bind = "catalog_full_name" });
            _listHeader.Add(new listHeader { h_width = 200, h_title = "M_Prop3_1_Manufacure", h_bind = "manufacture_nmae" });
            _listHeader.Add(new listHeader { h_width = 120, h_title = "M_Prop3_1_Code", h_bind = "model_code" });
            _listHeader.Add(new listHeader { h_width = 120, h_title = "M_Prop3_1_Order", h_bind = "order_code" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_Width", h_bind = "size_w" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_Depth", h_bind = "size_d" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_Height", h_bind = "size_h" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_Weight", h_bind = "weight" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_NumberOfPorts", h_bind = "num_of_ports" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_pp_1", h_bind = "pp_use_intelligent" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_pp_2", h_bind = "pp_config_type" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_pp_3", h_bind = "pp_media_type" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_pp_4", h_bind = "pp_utp_jack_type" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_pp_5", h_bind = "pp_utp_shield_type" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_pp_6", h_bind = "pp_figure_type" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_NumberOfPorts", h_bind = "ic_num_of_pp_connectors" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_ic_1", h_bind = "ic_num_of_max_pp" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_ic_2", h_bind = "ic_num_of_power" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_st_1", h_bind = "st_num_of_disk" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_sw_1", h_bind = "sw_figure_type" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_sw_2", h_bind = "sw_num_of_slots" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_sw_3", h_bind = "sw_model_type" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_cp_1", h_bind = "cp_plug_side" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_ca_1", h_bind = "ca_use_intelligent" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_ca_2", h_bind = "ca_install_type" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_ca_3", h_bind = "ca_for_army" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_ca_4", h_bind = "ca_media_type" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_ca_5", h_bind = "ca_is_utp_shield" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_ca_6", h_bind = "ca_utp_cable_type" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_ca_7", h_bind = "ca_fiber_cable_type" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_ca_8", h_bind = "ca_fiber_connector_type" });
//            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_ca_9", h_bind = "ca_disp_color_rgb" });    // 의미 없음 -> 추후 지원 여부 결정 
//            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_ca_9_2", h_bind = "ca_disp_name" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_rm_1", h_bind = "rm_is_rack_mount" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_rm_2", h_bind = "rm_unit_size" });
//            _listHeader.Add(new listHeader { h_width = 80, h_title = "M9_Catalog_LastUpdate", h_bind = "last_updated" });
            _listHeader.Add(new listHeader { h_width = 180, h_title = "M_Prop3_1_LastUpdate", h_bind = "last_updated" });

            //image_id = a.image_id,  // 의미 없음 
            //icon_16_image_id = a.icon_16_image_id,
            //icon_32_image_id = a.icon_32_image_id,
            //icon_48_image_id = a.icon_48_image_id,
            //icon_64_image_id = a.icon_64_image_id,
            //link_80_image_id = a.link_80_image_id,
            //deletable = a.deletable,
            //rm_image_220_image_id = a.rm_image_220_image_id,
            //rm_image_440_image_id = a.rm_image_440_image_id,
            //rm_image_880_image_id = a.rm_image_880_image_id,
            //user_id = a.user_id,
            //last_updated = a.last_updated,

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
                if (i == 1)
                {
                    GridViewExtensions.SetIsContentCentered(g1, true);
                }
                g1.Header = b2;
                //g1.Width = l1.h_width;
                if(i == 0)
                    g1.Width = l1.h_width;
                else
                    g1.Width = Double.NaN; //  l1.h_width;
                _lvGridView.Columns.Add(g1);
            }
            _lvManufacture.ItemsSource = _print_list; // _manufacture_list;

            // 메뉴 동적 생성 
            GridView v1 = (GridView)_lvManufacture.View;
            GridViewColumn b1 = (GridViewColumn)v1.Columns[0];
            TextBlock s2 = (TextBlock)((Border)v1.Columns[0].Header).Child;

            for (int i = 1; i <= v1.Columns.Count - 1; i++)
            {
                MenuItem m1 = new MenuItem();
                s2 = (TextBlock)((Border)v1.Columns[i].Header).Child;
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
            var t2_l = _template_column_list.Where(p => (p.template_id == t1.template_id )).ToList();

            ContextMenu m1 = _lvManufacture.ContextMenu;

            for (int i = 1; i < t2_l.Count+1; i++)
            {
                template_column tc = t2_l.Find(p => (p.template_column_no  == i));
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
            //var temp = _template_list.Find(p => p.template_name == name && p.report_id == report_id);
            if (temp != null)
            {
                MessageBox.Show(g.tr_get("C_Error11"));
                return false;
                /*
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
                */
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
        private void _btnExcel_Click(object sender, RoutedEventArgs e)
        {
            if (_print_list.Count < 1) return;

            _btnClick(0);
        }

        private void _btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (_print_list.Count < 1) return;

            _btnClick(1);
        }


        // 프린터 출력 
        private void _btnClick(int pr_ex)
        {
            this.Cursor = Cursors.Wait;
            iPrint ip = new iPrint();
            ip.p_name = title2 + g.tr_get("C_Report2");
            ip.p_title1 = g.tr_get("C_Report2");
            ip.p_title2 = "template_catalog_list";
            ip.anaylize(_lvManufacture);

            for (int row = 0; row < _print_list.Count; row++)
            {
                var mp = _print_list[row];

                ip.oTable.RowGroups[0].Rows.Add(new TableRow());
                ip.r1 = ip.oTable.RowGroups[0].Rows[row + 1];
                ip.r1.Background = System.Windows.Media.Brushes.White;
                ip.r1.Foreground = System.Windows.Media.Brushes.Navy;
                ip.r1.FontSize = 10;

                // template_column 저장
                for (int i = 0; i < _lvcompare.Count; i++)
                {
                    lvCompare t1 = _lvcompare[i];
                    if (t1.newgvc.Width == 0) continue;

                    switch (t1.newid)
                    {
                        case 1: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.RowNumber.ToString())))); break;
                        case 2: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.catalog_group_name)))); break;
                        case 3: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.catalog_name)))); break;
                        case 4: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.catalog_full_name)))); break;
                        case 5: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.manufacture_nmae)))); break;
                        case 6: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.model_code)))); break;
                        case 7: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.order_code)))); break;
                        case 8: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.size_w.ToString())))); break;
                        case 9: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.size_d.ToString())))); break;
                        case 10: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.size_h.ToString())))); break;
                        case 11: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.weight.ToString())))); break;
                        case 12: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.num_of_ports.ToString())))); break;
                        case 13: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.pp_use_intelligent)))); break;
                        case 14: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.pp_config_type)))); break;
                        case 15: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.pp_media_type)))); break;
                        case 16: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.pp_utp_jack_type)))); break;
                        case 17: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.pp_utp_shield_type)))); break;
                        case 18: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.pp_figure_type)))); break;
                        case 19: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.ic_num_of_pp_connectors.ToString())))); break;
                        case 20: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.ic_num_of_max_pp.ToString())))); break;
                        case 21: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.ic_num_of_power.ToString())))); break;
                        case 22: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.st_num_of_disk.ToString())))); break;
                        case 23: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.sw_figure_type)))); break;
                        case 24: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.sw_num_of_slots.ToString())))); break;
                        case 25: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.sw_model_type)))); break;
                        case 26: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.cp_plug_side)))); break;
                        case 27: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.ca_use_intelligent)))); break;
                        case 28: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.ca_install_type)))); break;
                        case 29: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.ca_for_army)))); break;
                        case 30: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.ca_media_type)))); break;
                        case 31: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.ca_is_utp_shield)))); break;
                        case 32: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.ca_utp_cable_type)))); break;
                        case 33: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.ca_fiber_cable_type)))); break;
                        case 34: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.ca_fiber_connector_type)))); break;
                        case 35: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.rm_is_rack_mount)))); break;
                        case 36: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.rm_unit_size.ToString())))); break;
                        case 37: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.last_updated)))); break;
                    }
                    
                }
            }
            PrintPreView winPrint = new PrintPreView(ip);
            winPrint.Owner = MainWindow.GetWindow(this);
            this.Cursor = null;

            if (pr_ex == 0)
            {
                winPrint.StartExcel();
            }
            else 
            {
                winPrint.StartPrint();
            }
            //winPrint.ShowDialog();
        }
        #endregion

        private void _window_Loaded(object sender, RoutedEventArgs e)
        {
            _save = false;
        }

    }

}


