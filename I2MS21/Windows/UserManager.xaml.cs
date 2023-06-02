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

namespace I2MS2.Windows
{
    /// <summary>
    /// UserManager.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserManager : Window
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

        List<UserTree> _left_list = null;
        UserTree _left_item = null;

        public UserManager()
        {
            InitializeComponent();
            initLeft();
            _lvLeft.SelectedItem = 0;

            cboType.Items.Clear();
            var item0 = new ComboBoxItem();
            item0.Content = "--Select--";
            var item1 = new ComboBoxItem();
            item1.Content = "Super User";
            var item2 = new ComboBoxItem();
            item2.Content = "Administrator";
            var item3 = new ComboBoxItem();
            item3.Content = "User";

            item0.Style = App.Current.Resources["I2MS_ComboboxItemStyle"] as Style;
            item1.Style = App.Current.Resources["I2MS_ComboboxItemStyle"] as Style;
            item2.Style = App.Current.Resources["I2MS_ComboboxItemStyle"] as Style;
            item3.Style = App.Current.Resources["I2MS_ComboboxItemStyle"] as Style;

            cboType.Items.Add(item0);
            if (UserRight.is_super_user())
                cboType.Items.Add(item1);
            cboType.Items.Add(item2);
            cboType.Items.Add(item3);
        }

        #region init 로직
        // 좌측 창에 내용을 출력한다.
        private void initLeft()
        {
            List<user> list = null;

            // 수퍼유저로 로긴한 경우 수퍼유저를 수정할 수 있다.
            if (UserRight.is_super_user())
                list = g.user_list;
            else
                list = g.user_list.Where(p => p.user_group != "S").ToList();

            _left_list = new List<UserTree>();
            foreach (var node in list)
            {
                UserTree item = new UserTree();
                convert2VM(item, node);
                _left_list.Add(item);
            };
            _lvLeft.ItemsSource = null;
            _lvLeft.ItemsSource = _left_list;
        }
        #endregion

        #region 컨버트 로직(Db <- Screen, Screen <- Db)
        // Left : node <- item
        private void convert2DB(user item, UserTree node)
        {
            item.user_id = node.user_id;
            item.remarks = node.remarks;
            item.user_group = node.user_group;
            item.user_name = node.user_name;
            item.login_id = node.login_id;
            item.login_password = node.login_password;
            item.email = node.email;
            item.use_email = node.use_email;
            item.phone = node.phone;
            item.mobile = node.mobile;
            item.use_sms = node.use_sms;
            item.reg_user_id = node.reg_user_id;
            item.deletable = node.deletable;
            item.remarks = node.remarks;
            item.last_updated2 = node.last_updated2;
        }

        // Left : vm_node <- db_node
        private void convert2VM(UserTree item, user node)
        {
            item.user_id = node.user_id;
            item.remarks = node.remarks;
            item.user_group = node.user_group;
            item.user_name = node.user_name;
            item.login_id = node.login_id;
            item.login_password = node.login_password;
            item.login_password2 = node.login_password;
            item.email = node.email;
            item.use_email = node.use_email;
            item.phone = node.phone;
            item.mobile = node.mobile;
            item.use_sms = node.use_sms;
            item.reg_user_id = node.reg_user_id;
            item.deletable = node.deletable;
            item.remarks= node.remarks;
            item.last_updated2 = node.last_updated2;
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
            txtlogin_password.Password = ""; // 신규일 경우만 초기화 처리 
            txtlogin_password2.Password = ""; // 신규일 경우만 초기화 처리 

            _new = false;
            _delete = false;
            _edit = false;
            _save = true;
            _cancel = true;

            txtuser_name.Focus();
            CommandManager.InvalidateRequerySuggested();

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

            txtuser_name.Focus();
        }

        private void _cmdDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            int idx = _lvLeft.SelectedIndex;

            if (idx == -1)
            {
                e.CanExecute = false;
                return;
            }
            if (!_delete)
            {
                e.CanExecute = false;
                return;
            }
            if (_left_item != null)
            {
                if (_left_item.deletable == "Y" && _left_item.user_id > 0)
                    e.CanExecute = true;
                else
                    e.CanExecute = false;
                return;
            }
            e.CanExecute = true;
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
            if (string.IsNullOrEmpty(txtuser_name.Text) ||
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

            _left_item = (UserTree)_lvLeft.SelectedItem;

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
            if (_left_list == null)
                return;

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
                int id = 0;
                if(_left_item != null)
                    id = _left_item.user_id;
                _lvLeft.ItemsSource = null;
                _lvLeft.ItemsSource = _left_list;
                _lvLeft.SelectedValue = id;
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
            var item = _left_item;
            if (item == null)
                return;

            txtuser_id.Text = item.user_id.ToString();
            txtuser_name.Text = item.user_name;
            txtlogin_id.Text = item.login_id;
            txtlogin_password.Password = DESEncrypt.Decrypt(item.login_password);
            cboType.SelectedIndex = TypeConverter.get_ext_type_index(item.user_group);
            txtemail.Text = item.email;
            chkuse_email.IsChecked = item.use_email == "Y";
            txtphone.Text = item.phone;
            txtmobile.Text = item.mobile;
            chkuse_sms.IsChecked = item.use_sms == "Y";
            chkdeletable.IsChecked = item.deletable == "Y";
            txtremarks.Text = item.remarks;
        }

        // 메모리에 화면 내용을 옮김
        private bool contents2memdb(bool new_flag)
        {
            var item = _left_item;
            if (item == null)
                return false;
            item.user_name = txtuser_name.Text.Trim();
            item.login_id = txtlogin_id.Text.Trim();
            item.login_password = DESEncrypt.Encrypt(txtlogin_password.Password);
            item.login_password2 = DESEncrypt.Encrypt(txtlogin_password2.Password);
            item.user_group = TypeConverter.get_ext_type(cboType.Text.Trim());
            item.email = txtemail.Text.Trim();
            item.use_email = chkuse_email.IsChecked == true ? "Y" : "N";
            item.phone = txtphone.Text.Trim();
            item.mobile = txtmobile.Text.Trim();
            item.use_sms = chkuse_sms.IsChecked == true ? "Y" : "N";
            if (item.user_id == 90001)
                item.deletable = "N";
            else
                item.deletable = "Y";
            item.remarks = txtremarks.Text.Trim();
            return true;
        }

        // 신규 처리시 기존 내용 없애는 용도 
        private void clearControlLeft()
        {
            txtuser_id.Clear();
            txtuser_name.Clear();
            txtlogin_id.Clear();
            txtlogin_password.Clear();
            txtlogin_password2.Clear();
            cboType.SelectedIndex = 0;
            txtemail.Clear();
            chkuse_email.IsChecked = false;
            txtphone.Clear();
            txtmobile.Clear();
            chkuse_sms.IsEnabled = false;
            txtremarks.Clear();
            chkdeletable.IsEnabled = false;
            _lvLeft.SelectedIndex = -1;
        }
        // enable control 로직
        private void enableControlLeft(bool flag)
        {
            txtuser_name.IsEnabled = flag;
            txtlogin_id.IsEnabled = flag;
            txtlogin_password.IsEnabled = flag;
            txtlogin_password2.IsEnabled = flag;

            if (_left_item != null)
            {
                if (flag)
                {
                    txtlogin_password.Password = DESEncrypt.Decrypt(_left_item.login_password);
                    txtlogin_password2.Password = DESEncrypt.Decrypt(_left_item.login_password2);
                }
                else
                {
                    txtlogin_password.Password = "";
                    txtlogin_password2.Password = ""; 
                }
            }
            cboType.IsEnabled = flag;
            if (flag)
            {
                int idx = cboType.SelectedIndex;
            }
            txtemail.IsEnabled = flag;
            chkuse_email.IsEnabled = flag;
            txtphone.IsEnabled = flag;
            txtmobile.IsEnabled = flag;
            chkuse_sms.IsEnabled = flag;
            txtremarks.IsEnabled = flag;
            chkdeletable.IsEnabled = flag;
        }
        #endregion

        #region add, edit save 로직, delete 로직
        private async Task<bool> saveLeft()
        {
            string name = txtuser_name.Text.Trim();

            if (_left_list == null)
                _left_list = new List<UserTree>();

            if (txtlogin_password.Password != txtlogin_password2.Password)
            {
                MessageBox.Show(g.tr_get("C_Error_Password_1"));
                return false;
            }

            // 신규 상태에서 저장 버튼을 누른 경우
            if (_new_flag)
            {
                var temp = _left_list.Find(p => p.user_name == name);
                if (temp != null)
                {
                    MessageBox.Show(g.tr_get("C_Error11"));
                    return false;
                }

                _left_item = new UserTree() { user_id = 0 };
                if (!contents2memdb(true))
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }

                var node = new user();
                convert2DB(node, _left_item);
                node.reg_user_id = g.login_user_id;  // 현재 접속자 아이디를 저장 시키자. 
                node.last_updated2 = DateTime.Now;

                var out_node = (user)await g.webapi.post("user", node, typeof(user));
                if (out_node == null)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }
                g.user_list.Add(out_node);                
                initLeft();
            }
            else
            {
                if (!contents2memdb(false))
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }
                var node = new user();
                convert2DB(node, _left_item);
                node.last_updated2 = DateTime.Now;
                int r = await g.webapi.put("user", _left_item.user_id, node, typeof(user));
                if (r != 0)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }
                // 메모리 디비 업데이트 처리 
                var temp = g.user_list.Find(p => p.user_id == _left_item.user_id);
                convert2DB(temp, _left_item);

            }
            return true;
        }

        private async Task<bool> deleteLeft()
        {
            int delete_id = _left_item.user_id;

            if (_left_item.deletable != "Y")
            {
                MessageBox.Show(g.tr_get("C_Error_Not_Deletable"));
                return false;
            }

            int rr1 = await g.webapi.delete("user", delete_id);
            if (rr1 != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }

            var node = g.user_list.Find(p => p.user_id == delete_id);
            if (node == null)
                return false;

            g.user_list.Remove(node);

            var item = _left_list.Find(p => p.user_id == delete_id);
            if (item == null)
                return false;

            _left_list.Remove(item);

            return true;
        }
        #endregion
    }

    #region 벨류 컨버터, 스태틱 라이브러러
    public static class TypeConverter
    {
        public static string get_ext_type_name(string type)
        {
            switch (type)
            {
                case "S":
                    return "Super User";
                case "A":
                    return "Administrator";
                case "U":
                    return "User";
                default:
                    return "";
            };
        }

        public static string get_ext_type(string name)
        {
            if (name == "Super User")
                return "S";
            if (name == "Administrator")
                return "A";
            if (name == "User")
                return "U";
            return "";
        }

        public static int get_ext_type_index(string type)
        {
            if (UserRight.is_super_user())
            {
                switch (type)
                {
                    case "S":
                        return 1;
                    case "A":
                        return 2;
                    case "U":
                        return 3;
                    default:
                        return 3;
                };
            }
            else
            {
                switch (type)
                {
                    case "A":
                        return 1;
                    case "U":
                        return 2;
                    default:
                        return 2;
                };
            }
        }
    }

    public class UserTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";
            string type = (string)value;

            return TypeConverter.get_ext_type_name(type);
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";
            string name = (string)value;

            return TypeConverter.get_ext_type(name);
        }
    }
    #endregion
}
