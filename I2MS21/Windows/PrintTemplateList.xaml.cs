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
    public class ViewList : INotifyPropertyChanged
    {
        public ViewList()
        {
            this.node_list = new List<ViewList>();
        }
        public List<ViewList> node_list { get; set; }

        public int RowNumber { get; set; }        
        public int template_id { get; set; }
        public int report_id { get; set; }
        public string template_name { get; set; }
        public int num_of_template_column { get; set; }
        public int user_id { get; set; }
        public string last_updated2 { get; set; }
        public string remarks { get; set; }

//        public string report_desc { get; set; }  // report 지우기 처리 romee 2/5
        public string user_name { get; set; }


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
    /// PrintTempleateList.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PrintTemplateList : Window
    {
        #region RouteCommand 버튼 관련 정의
        public static RoutedCommand PrintCommand = new RoutedCommand();
        public static RoutedCommand DeleteCommand = new RoutedCommand();

        private bool _print = true;
        private bool _delete = false;
        #endregion

        // 리포트 관련 테이블
        List<report> _report_list = null;
        List<template> _template_list = null;
        List<template_column> _template_column_list = null;
        List<lvCompare> _lvcompare = new List<lvCompare>();
        List<listHeader> _listHeader = new List<listHeader>();

        // 출력할 테이블 
        List<ViewList> _print_list = new List<ViewList>();
        ViewList _selitem = new ViewList();

        // 출력 이름 
        DateTime now = DateTime.Now;
        string title2;

        public PrintTemplateList()
        {
            InitializeComponent();

            _report_list = g.report_list.ToList();
            _template_list = g.template_list.ToList();
            _template_column_list = g.template_column_list.ToList();

            title2 = now.ToString("yyyyMMdd");
            txtsave_name.Text = ""; //  title2 + " 제조사 목록";

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
            // 데이터 취합 
            int i = 1;
            var tdb1 = from a in g.template_list
                       join c in g.user_list on a.user_id  equals c.user_id 
                       orderby a.template_id
                       select new ViewList()
                       {
                            template_id = a.template_id ,
                            report_id = a.report_id,
                            template_name = a.template_name,
                            user_id = a.user_id,
                            last_updated2 = a.last_updated2.GetDateTimeFormats().GetValue(76).ToString(),
                            remarks = a.remarks,
                            user_name = c.user_name,
                            RowNumber = i++,
                       };

            _print_list = tdb1.ToList();
        }

        // 리스트 뷰에 그리드 컬럼을 동적 생성 토록 변경 
        private void initListView()
        {
            // 리소스 스트링을 디비화 할 필요 있음 

            _listHeader.Add(new listHeader { h_width = 0, h_title = "ID", h_bind = "template_id" });
            _listHeader.Add(new listHeader { h_width = 0, h_title = "ID2", h_bind = "report_id" });
            _listHeader.Add(new listHeader { h_width = 60, h_title = "C_No", h_bind = "RowNumber" });
            _listHeader.Add(new listHeader { h_width = 200, h_title = "C_TempleateName", h_bind = "template_name" });
            _listHeader.Add(new listHeader { h_width = 100, h_title = "M9_UserManager_UserName", h_bind = "user_name" });
            _listHeader.Add(new listHeader { h_width = 180, h_title = "M_Prop3_1_LastUpdate", h_bind = "last_updated2" });

            // 동적 생성 
            for (int i = 0; i < _listHeader.Count; i++)
            {
                listHeader l1 = _listHeader[i];
                TextBlock t1 = new TextBlock();
                t1.TextAlignment = TextAlignment.Center;
                t1.Text = I2MSR.Properties.Resources.ResourceManager.GetString(l1.h_title);
                t1.Style = Application.Current.Resources["I2MS_ListViewColHeaderText"] as Style;
                Border b2 = new Border();
                b2.BorderThickness = new Thickness(0);
                b2.Child = t1;

                GridViewColumn g1 = new GridViewColumn();
                g1.DisplayMemberBinding = new Binding(l1.h_bind);

                if (i == 2 || i == 4 || i == 5)
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

        #endregion


        #region add, edit, save 로직, delete 로직, 프린터 출력 컨버트 로직(Db <- Screen, Screen <- Db)

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
            string name = txtsave_name.Text;
            ViewList v = (ViewList) _lvManufacture.SelectedItem;
            if (v == null) return false;
            int delete_id = (int)v.template_id;

            if (chkdata(name, "", 10001))
            {
                _delete = false;
                return false;
            }
            if (MessageBox.Show(name + g.tr_get("C_Error13"), "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                _delete = false;
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

            // 리스트뷰 업데이트 하기 
            getDBList();
            _lvManufacture.ItemsSource = _print_list; // _manufacture_list;
            txtsave_name.Text = "";
            return true;
        }
        #endregion

        #region  프린터 출력 컨버트 로직(Db <- Screen, Screen <- Db)
        // 프린터 출력 
        private void _btnPrint_Click(object sender, RoutedEventArgs e)
        {
            //Reloadlvcompare();

            this.Cursor = Cursors.Wait;
            iPrint ip = new iPrint();
            ip.p_name = title2 + g.tr_get("C_Report7");
            ip.p_title1 = g.tr_get("C_Report7");
            ip.p_title2 = "template_print_list";
            ip.anaylize(_lvManufacture);

            for (int row = 0; row < _print_list.Count; row++)
            {
                var mp = _print_list[row];

                ip.oTable.RowGroups[0].Rows.Add(new TableRow());
                ip.r1 = ip.oTable.RowGroups[0].Rows[row + 1];
                ip.r1.Background = System.Windows.Media.Brushes.Azure;
                ip.r1.Foreground = System.Windows.Media.Brushes.Navy;
                ip.r1.FontSize = 10;

                //ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.RowNumber.ToString()))));
                ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.template_name)))); 
                ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.user_name)))); 
                ip.r1.Cells.Add(new TableCell(new Paragraph(new Run(mp.last_updated2)))); 
            }
            PrintPreView winPrint = new PrintPreView(ip);
            winPrint.Owner = MainWindow.GetWindow(this);
            this.Cursor = null;

            winPrint.ShowDialog();
        }

        #endregion

        private void _lvManufacture_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var t1 = _lvManufacture.SelectedIndex;

            if (t1 == -1) return;
            _selitem  = (ViewList)_lvManufacture.SelectedItem;
            txtsave_name.Text = _selitem.template_name;
            _delete = true;
        }
    }
}
