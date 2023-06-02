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
using WebApi.Models;
using I2MS2.Models;
using I2MS2.Library;
using WebApiClient;
using System.Globalization;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace I2MS2.Windows
{
    public class SiteUserVM : INotifyPropertyChanged
    {
        #region ExtPropertyVM 정의
        public int site_id { get; set; }
        public int site_user_id { get; set; }
        public int user_id { get; set; }
        public string user_right { get; set; }
        public int reg_user_id { get; set; }
        public byte[] last_updated { get; set; }
        public string site_name { get; set; }
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
        #endregion
    }

    /// <summary>
    /// UserManager.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserSiteManager : Window
    {
        #region RouteCommand 버튼 관련 정의
        public static RoutedCommand NewCommand = new RoutedCommand();
        public static RoutedCommand EditCommand = new RoutedCommand();
        public static RoutedCommand DeleteCommand = new RoutedCommand();
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand CancelCommand = new RoutedCommand();

        private bool _new = true;
        private bool _edit = false;
        private bool _delete = false;
        private bool _save = false;
        private bool _cancel = false;

        private bool _new_flag = true;
        #endregion

        //List<UserTree> _left_list = null;
        //UserTree _left_item = null;

        List<SiteUserVM> _left_list = null;
        SiteUserVM _left_item = null;

        ObservableCollection<string> _site_list = new ObservableCollection<string>();
        ObservableCollection<string> _user_list = new ObservableCollection<string>();

        public UserSiteManager()
        {
            InitializeComponent();
            initLeft();
            _lvLeft.SelectedItem = 0;
        }

        #region init 로직
        // 좌측 창에 내용을 출력한다.
        private void initLeft()
        {
            getList();

            _site_list.Clear();
            _user_list.Clear();

            _site_list.Add("--Select--");
            _user_list.Add("--Select--");

            foreach (var node in g.site_list)
                _site_list.Add(node.site_name);

            List<user> list = g.user_list.Where(p => p.user_group != "S").ToList();

            foreach (var node in list)
                _user_list.Add(node.user_name);
            cboTypeSite.ItemsSource = _site_list;
            cboTypeUser.ItemsSource = _user_list;
            cboTypeSite.SelectedIndex = 0;
            cboTypeUser.SelectedIndex = 0;
            _lvLeft.ItemsSource = _left_list;
        }

        private void getList()
        {
            var l1 = from a in g.site_list
                     join b in g.site_user_list on a.site_id equals b.site_id
                     join c in g.user_list on b.user_id equals c.user_id
                        orderby a.site_id
                     select new SiteUserVM()
                        {
                            site_id = b.site_id,
                            site_user_id = b.site_user_id,
                            user_id = b.user_id,
                            user_right = b.user_right,
                            reg_user_id = b.reg_user_id, 
                            site_name = a.site_name, 
                            user_name = c.user_name,  
                        };

            _left_list = l1.ToList();
        }
        #endregion

        #region 컨버트 로직(Db <- Screen, Screen <- Db)
        // Left : node <- item
        private void convert2DB(site_user item, SiteUserVM node)
        {
            var t1 = g.site_list.Find(p => (p.site_name == _left_item.site_name));
            var t2 = g.user_list.Find(p => (p.user_name == _left_item.user_name));
            item.site_user_id = node.site_user_id;
            item.site_id = t1.site_id;
            item.user_right = _left_item.user_right;
            item.user_id = t2.user_id;
            item.reg_user_id = g.login_user_id;
            //item.last_updated = DateTime.Now.ToString();
        }

        // Left : vm_node <- db_node
        private void convert2VM(SiteUserVM item, site_user node)
        {
            item.site_id = node.site_id;
        }

        #endregion

        #region CRUD 신규,삭제 등 버튼 처리 로직

        private void _cmdNew_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _new;
        }

        private void _cmdNew_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _new_flag = true;
            clearControlLeft();
            enableControlLeft(true);

            _new = false;
            _delete = false;
            _edit = false;
            _save = true;
            _cancel = true;

//            txtuser_name.Focus();
        }

        private void _cmdEdit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _edit;
        }

        private void _cmdEdit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            enableControlLeft(true);
            _new = false;
            _delete = false;
            _edit = false;
            _save = true;
            _cancel = true;
            _new_flag = false;

//            txtuser_name.Focus();
        }

        private void _cmdDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _delete;
        }

        private async void _cmdDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(g.tr_get("C_Delete_Item"), g.tr_get("C_Confirm"), MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            int idx = _lvLeft.SelectedIndex;

            if (await deleteLeft())
            {
                if (idx > 0)
                {
                    refreshLeft(idx);
                    return;
                }
            }
            _new = true;
            _edit = true;
            _delete = true;
            _save = false;
        }

        private void _cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if ((cboTypeSite.SelectedIndex == 0) || (cboTypeUser.SelectedIndex == 0) ||
                (cboType.SelectedIndex == 0))
            {
                e.CanExecute = false;
                return;
            }
            e.CanExecute = _save;
        }
        // 저장 처리 
        private async void _cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!await saveLeft())
                return;
            if (_new_flag)
            {
                // 화면을 갱신하기 위해 다시 데이터를 리스트에 로드하고 선택한다.
                refreshLeft(-1);
            }
            else
            {
                _left_item.force_changed = true;
            }

            enableControlLeft(false);

            _new = true;
            _delete = true;
            _edit = true;
            _save = false;
            _cancel = false;

            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }

        private void _cmdCancel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _cancel;
        }

        private void _cmdCancel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_new_flag)
                clearControlLeft();
            else
                dispLeft();

            enableControlLeft(false);

            _new = true;
            _delete = false;
            _edit = false;
            _save = false;
            _cancel = false;
        }
        #endregion

        #region 각종 이벤트 핸들러 처리
        // 리스트 선택 처리 
        private void _lvLeft_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _new = true;
            _delete = true;
            _edit = true;
            _save = false;
            _cancel = false;

            _left_item = (SiteUserVM)_lvLeft.SelectedItem;

            if (_left_item == null)
                return;
            dispLeft();
            enableControlLeft(false);
        }

        private void cboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 권한 변경시 처리될 내용 처리하는 부분 
        }
        #endregion

        #region refresh 로직,  diplay 로직, clear control 로직, 화면 컨트롤 enable
        // 삭제 후 리프레쉬는 인덱스값을 지정하고, 추가 후 리프레쉬는 인덱스 값을 -1을 부여한다.
        private void refreshLeft(int idx)
        {
            // 삭제 시
            if (idx > 0)
            {
                _lvLeft.ItemsSource = null;
                _lvLeft.ItemsSource = _left_list;
                _lvLeft.SelectedIndex = idx - 1;
            }

            // 추가 또는 수정 시
            if (idx == -1)
            {
                if(_left_item != null)
                {
                    int id = _left_item.site_user_id;
                    _lvLeft.ItemsSource = null;
                    _lvLeft.ItemsSource = _left_list;
                    _lvLeft.SelectedValue = id;
                }
            }

            var node = _lvLeft.SelectedItem;
            if (node != null)
                _lvLeft.ScrollIntoView(node);
            dispLeft();
            return;
        }

        // 화면에 메모리 내용 디스플레이 -> 리스트에서 선택후 처리됨 
        private void dispLeft()
        {
            int i1;
            string s1, s2;
            var item = _left_item;
            if (item == null)
                return;

            txtsite_user_id.Text = item.site_user_id.ToString();
            s1 = item.site_name;
            s2 = item.user_name;

            i1 = cboTypeSite.Items.IndexOf(s1); 
            cboTypeSite.SelectedIndex = i1;

            i1 = cboTypeUser.Items.IndexOf(s2); 
            cboTypeUser.SelectedIndex = i1;

            cboType.SelectedIndex = get_ext_type_index(item.user_right);
        }

        public static int get_ext_type_index(string type)
        {
            switch (type)
            {
                    // 슈퍼 유저는 모든 사이트 관리하므로 의미없음 
                //case "S":
                //    return 1;
                case "A":
                    return 1;
                case "U":
                    return 2;
                default:
                    return 2;
            };
        }

        // 메모리에 화면 내용을 옮김
        private bool contents2memdb(bool new_flag)
        {
            int t1;
            var item = _left_item;
            if (item == null)
                return false;
            int.TryParse(txtsite_user_id.Text, out t1);
            item.site_user_id = t1;
            item.site_name = cboTypeSite.SelectionBoxItem.ToString();
            item.user_name = cboTypeUser.SelectionBoxItem.ToString();
            item.user_right = TypeConverter.get_ext_type(cboType.Text.Trim());
            return true;
        }

        // 신규 처리시 기존 내용 없애는 용도 
        private void clearControlLeft()
        {
            txtsite_user_id.Text = "";
            cboTypeSite.SelectedIndex = 0;
            cboTypeUser.SelectedIndex = 0;
            cboType.SelectedIndex = 0;
            _lvLeft.SelectedIndex = -1;
        }
        // enable control 로직
        private void enableControlLeft(bool flag)
        {
            cboType.IsEnabled = flag;
            cboTypeSite.IsEnabled = flag;
            cboTypeUser.IsEnabled = flag;

            if (flag)
            {
                int idx = cboType.SelectedIndex;
            }
        }
        #endregion

        #region add, edit save 로직, delete 로직
        private async Task<bool> saveLeft()
        {
            string s1 =  cboTypeUser.SelectionBoxItem.ToString();
            string s2 =  cboTypeSite.SelectionBoxItem.ToString();

            if (_left_list == null)
                _left_list = new List<SiteUserVM>();

            // 신규 상태에서 저장 버튼을 누른 경우
            if (_new_flag)
            {
                _left_item = new SiteUserVM() { site_id = 0 };
                if (!contents2memdb(true))
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }
                var temp = _left_list.Find(p => (p.site_name == _left_item.site_name) && (p.user_name == _left_item.user_name) );
                if (temp != null)
                {
                    MessageBox.Show(g.tr_get("C_Error11"));
                    return false;
                }

                var node = new site_user();
                convert2DB(node, _left_item);
                node.reg_user_id = g.login_user_id;  // 현재 접속자 아이디를 저장 시키자. 
                var out_node = (site_user)await g.webapi.post("site_user", node, typeof(site_user));
                if (out_node == null)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }
                g.site_user_list.Add(out_node);
                initLeft();
            }
            else
            {
                if (!contents2memdb(false))
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }
                var node = new site_user();
                convert2DB(node, _left_item);
                int r = await g.webapi.put("site_user", _left_item.site_user_id, node, typeof(site_user));
                if (r != 0)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }
                // 메모리 디비 업데이트 처리  
                var temp = g.site_user_list.Find(p => p.site_user_id == _left_item.site_user_id);
                convert2DB(temp, _left_item);
            }
            return true;
        }

        private async Task<bool> deleteLeft()
        {

            int delete_id = _left_item.site_user_id;

            int rr1 = await g.webapi.delete("site_user", delete_id);
            if (rr1 != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }

            var node = g.site_user_list.Find(p => p.site_user_id == delete_id);
            if (node == null)
                return false;

            g.site_user_list.Remove(node);

            var item = _left_list.Find(p => p.site_user_id == delete_id);
            if (item == null)
                return false;

            _left_list.Remove(item);

            return true;
        }
        #endregion

        private void cboTypeUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //
        }

        private void cboTypeSite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //
        }
    }

    #region 벨류 컨버터, 스태틱 라이브러러
    //public static class TypeConverter
    //{
    //    public static string get_ext_type_name(string type)
    //    {
    //        switch (type)
    //        {
    //            case "S":
    //                return "Super User";
    //            case "A":
    //                return "Administrator";
    //            case "U":
    //                return "User";
    //            default:
    //                return "";
    //        };
    //    }

    //    public static string get_ext_type(string name)
    //    {
    //        if (name == "Super User")
    //            return "S";
    //        if (name == "Administrator")
    //            return "A";
    //        if (name == "User")
    //            return "U";
    //        return "";
    //    }

    //    public static int get_ext_type_index(string type)
    //    {
    //        switch (type)
    //        {
    //            case "S":
    //                return 1;
    //            case "A":
    //                return 2;
    //            case "U":
    //                return 3;
    //            default:
    //                return 3;
    //        };
    //    }
    //}

    //public class UserTypeConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType,
    //        object parameter, CultureInfo culture)
    //    {
    //        if (value == null)
    //            return "";
    //        string type = (string)value;

    //        return TypeConverter.get_ext_type_name(type);
    //    }

    //    public object ConvertBack(object value, Type targetType,
    //        object parameter, CultureInfo culture)
    //    {
    //        if (value == null)
    //            return "";
    //        string name = (string)value;

    //        return TypeConverter.get_ext_type(name);
    //    }
    //}
    #endregion
}
