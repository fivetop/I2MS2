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
using MahApps.Metro.Controls;

namespace I2MS2.Windows
{
    public class MenufacturePrintList : INotifyPropertyChanged
    {
        public MenufacturePrintList()
        {
            this.node_list = new List<MenufacturePrintList>();
        }
        public List<MenufacturePrintList> node_list { get; set; }

        public int RowNumber { get; set; }
        public int manufacture_id { get; set; }
        public string manufacture_name { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string post { get; set; }
        public string homepage_url { get; set; }
        public string ceo_name { get; set; }
        public string remarks { get; set; }

        public int contact_id { get; set; }
        public int manufacture_id2 { get; set; }
        public string contact_name { get; set; }
        public string duty { get; set; }
        public string position { get; set; }
        public string phone2 { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }

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
    public partial class ManufactureList : MetroWindow
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
        List<MenufacturePrintList> _print_list = new List<MenufacturePrintList>();       

        // 출력 이름 
        DateTime now = DateTime.Now;
        string title2;
        int report_id = 1120001;

        private bool blive = false;

        public ManufactureList()
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
            _save = false;

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
            if (txtsave_name.Text == string.Empty)
                _save = false;
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
            contact c_default = new contact
            {
                contact_id = 0,
                manufacture_id = 0,
            };

            // 데이터 취합 
            int i = 1;

            var tdb1 = from a in g.manufacture_list
                       join b in g.contact_list on a.manufacture_id  equals b.manufacture_id into ps
                       from b in ps.DefaultIfEmpty(c_default) 
                       orderby a.manufacture_id 
                       select new MenufacturePrintList()
                       {
                           manufacture_id = a.manufacture_id,
                           manufacture_name = a.manufacture_name,
                           phone = a.phone,
                           address = a.address,
                           post = a.post,
                           homepage_url = a.homepage_url,
                           ceo_name = a.ceo_name,
                           remarks = a.remarks,

                           contact_id = b.contact_id,
                           contact_name = b.contact_name,
                           duty = b.duty,
                           position = b.position,
                           phone2 = b.phone,
                           mobile = b.mobile,
                           email = b.email,
                           RowNumber = i++,
                       };

            _print_list = tdb1.ToList();

            string str1 = " / " + g.select_site.site_name;
            txtadd.Text = "Record Count (" + Convert.ToString(_print_list.Count) + ")" + str1; 

            // 차일드 테이블에서 두개 이상인 데이터 있으면 삭제 처리 
            // 대표 관리자만 보여주기 처리 
            int oldid = 0;
            for (int j = 0; j < _print_list.Count; j++)
            {
                var t1 = _print_list[j];
                if (t1.manufacture_id == oldid)
                    _print_list.RemoveAt(j);
                else
                    oldid = t1.manufacture_id;
 
            }
        }

        // 리스트 뷰에 그리드 컬럼을 동적 생성 토록 변경 
        private void initListView()
        {
            // 리소스 스트링을 디비화 할 필요 있음 

            _listHeader.Add(new listHeader { h_width = 0, h_title = "ID", h_bind = "manufacture_id" });
            _listHeader.Add(new listHeader { h_width = 80, h_title = "C_No", h_bind = "RowNumber" });
            _listHeader.Add(new listHeader { h_width = 120, h_title = "M9_Manufacture_Name", h_bind = "manufacture_name" });
            _listHeader.Add(new listHeader { h_width = 100, h_title = "M9_Contact_Phone", h_bind = "phone" });
            _listHeader.Add(new listHeader { h_width = 100, h_title = "C_Post", h_bind = "post" });                           
            _listHeader.Add(new listHeader { h_width = 100, h_title = "C_CEO", h_bind = "ceo_name" });                      
            _listHeader.Add(new listHeader { h_width = 180, h_title = "M9_Manufacture_Address", h_bind = "address" });
            _listHeader.Add(new listHeader { h_width = 130, h_title = "C_Homepage", h_bind = "homepage_url" });                  
            _listHeader.Add(new listHeader { h_width = 150, h_title = "C_Remarks", h_bind = "remarks" });
            _listHeader.Add(new listHeader { h_width = 100, h_title = "C_Manager", h_bind = "contact_name" });                     
            _listHeader.Add(new listHeader { h_width = 60, h_title = "M9_Contact_Duty", h_bind = "duty" });
            _listHeader.Add(new listHeader { h_width = 60, h_title = "M9_Contact_Position", h_bind = "position" });
            _listHeader.Add(new listHeader { h_width = 100, h_title = "M9_Contact_Phone", h_bind = "phone2" });
            _listHeader.Add(new listHeader { h_width = 100, h_title = "M9_Contact_Mobile", h_bind = "mobile" });
            _listHeader.Add(new listHeader { h_width = 100, h_title = "M9_Contact_Email", h_bind = "email" });

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
                g1.DisplayMemberBinding = new Binding(l1.h_bind );
                if (i == 1)
                {
                    GridViewExtensions.SetIsContentCentered(g1, true);
                }
                g1.Header = b2;
                if (i == 0)
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
                ((System.ComponentModel.INotifyPropertyChanged) v1.Columns[i]).PropertyChanged += gridViewColumn_INotifyPropertyChanged;
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
                    MenuItem m2 = (MenuItem) m1.Items[i-1];
                    if(tc.column_width == 0)
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

            if (chkdata(name, "",  10001)) return false;

            if (MessageBox.Show(name + g.tr_get("C_Error14"), "Save", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return false;
            }
            var temp = _template_list.Find(p => p.template_name  == name);
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
            if (chkdata(contents2memdb(true), false,  10003)) return false;

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
                w1 = (int) t1.newgvc.ActualWidth; 
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
            if (_print_list.Count < 1) return;

            this.Cursor = Cursors.Wait;
            iPrint ip = new iPrint();
            ip.p_name = title2 + g.tr_get("C_Report6");
            ip.p_title1 = g.tr_get("C_Report6");
            ip.p_title2 = "template_manufacturer_list";
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
                        case 2: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.manufacture_name)))); break;
                        case 3: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.phone)))); break;
                        case 4: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.post)))); break;
                        case 5: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.ceo_name)))); break;
                        case 6: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.address)))); break;
                        case 7: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.homepage_url)))); break;
                        case 8: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.remarks)))); break;
                        case 9: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.contact_name)))); break;
                        case 10: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.duty)))); break;
                        case 11: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.position)))); break;
                        case 12: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.phone2)))); break;
                        case 13: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.mobile)))); break;
                        case 14: ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.email)))); break;
                    }
                }
            }
            PrintPreView winPrint = new PrintPreView(ip);
            winPrint.Owner = MainWindow3.GetWindow(this);
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

    }

}


