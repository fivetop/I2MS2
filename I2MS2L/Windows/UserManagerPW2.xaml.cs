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
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace I2MS2.Windows
{
    /// <summary>
    /// UserManagerPW.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserManagerPW2 : Window
    {
        #region RouteCommand 버튼 관련 정의
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand CancelCommand = new RoutedCommand();

        private bool _save = true;
        private bool _cancel = true;

        string p_id;
        string p_pw;
        #endregion

        public UserManagerPW2()
        {
            InitializeComponent();
            initLeft();
        }

        #region init 로직
        // 좌측 창에 내용을 출력한다.
        private void initLeft()
        {
            var item = g.user_list.Find(p => p.user_id == g.login_user_id);
            txtuser_name.Text = item.user_name;
            txtlogin_id.Text = item.login_id;
            txtorg_pw.Text = "";

            p_id = item.login_id;
            p_pw = item.login_password;
        }
        #endregion

        #region CRUD 신규,삭제 등 버튼 처리 로직
        private void _cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtlogin_password.Password) || string.IsNullOrEmpty(txtlogin_password2.Password))
            {
                e.CanExecute = false;
                return;
            }
            if (txtlogin_password.Password == txtorg_pw.Text)
            {
                e.CanExecute = false;
                return;
            }
            e.CanExecute = _save = true;
        }
        // 저장 처리 
        private async void _cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!await saveLeft())
                return;
            try
            {
                DialogResult = true;
            }
            catch { }
        }

        private void _cmdCancel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void _cmdCancel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = false;
        }
        #endregion


        #region add, edit save 로직, delete 로직

        private async Task<bool> saveLeft()
        {
            string tid = txtlogin_id.Text.Trim();
            string tpw = txtlogin_password.Password.Trim();

            if (txtlogin_password.Password != txtlogin_password2.Password)
            {
                MessageBox.Show(g.tr_get("C_Info40"));
                return false;
            }
            if (txtlogin_password.Password == "Abcde12345")
            {
                MessageBox.Show(g.tr_get("C_Info41"));
                return false;
            }
            if (tid == p_id)
            {
                MessageBox.Show(g.tr_get("C_Info42"));
                return false;
            }
            if (DESEncrypt.DecryptSHA_AES(p_pw, tpw) == p_pw)
            {
                MessageBox.Show(g.tr_get("C_Info43"));
                return false;
            }

            // GS 인증 로긴 정보 생성 규칙 적용 romee 2016.04.28
            // 길이 체크
            // 스펠 체크
            if (tid.Length < 5 || tid.Length > 10)
            {
                MessageBox.Show(g.tr_get("C_Error_Info1"));
                return false;
            }
            // 영숫자 조합 체크
            if (g.isMatch (tpw) != 3)
            {
                 //alert("영대문자,영소문자, 숫자의 조합으로 구성되어야 합니다.");
                MessageBox.Show(g.tr_get("C_Error_Info1"));
                 return false;
            }
            // 영숫자 조합 체크
            if (g.isMatch2(tid) < 1)
            {
                //alert("영대문자,영소문자, 숫자의 조합으로 구성되어야 합니다.");
                MessageBox.Show(g.tr_get("C_Error_Info1"));
                return false;
            }

            if (g.isMatchSpecial(tpw))
            {
                //alert("특수문자는 사용할 수 없습니다.");
                MessageBox.Show(g.tr_get("C_Error_Info2"));
                return false;
            }
            if (tpw.Length < 10 || tpw.Length > 16)
            {
                MessageBox.Show(g.tr_get("C_Error_Info1"));
                return false;
            }

            user item = item = g.user_list.Find(p => p.user_id == g.login_user_id);
            item.login_id = tid;
            item.last_updated2 = DateTime.Now;
            // GS_DEL
            if (item.user_group == "S")
                item.pwchk = 1;

            item.login_password = DESEncrypt.EncryptSHA_AES(txtlogin_password.Password);
            int r = await g.webapi.put("user", item.user_id, item, typeof(user));
            if (r != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }
            // 메모리 디비 업데이트 처리 
            var temp = g.user_list.Find(p => p.user_id == g.login_user_id);
            // 사용자 수정
            bool r1 = await log_set(temp, 1090005, temp.login_id);
            return true;
        }

        public async Task<bool> log_set(user key, int event_id, string id)
        {
            event_hist _item = new event_hist();

            event_lang evl = g.event_lang_list.Find(at => (at.event_id == event_id) && (at.lang_id == g.lang_id));
            string tgroup = TypeConverter.get_ext_type_name(key.user_group);

            if (evl == null)
            {
                _item.user_id = g.login_user_id;
                _item.event_id = event_id;
                _item.event_text = "Contents data check.";
            }
            else
            {
                _item.user_id = g.login_user_id;
                _item.event_id = evl.event_id;
                _item.event_text = evl.event_desc + " ->" + key.user_name + " :Permission -> " + tgroup;
            }
            _item.write_time = DateTime.Now;
            _item.is_confirm = "N";
            _item.event_type = "I";
            if (event_id != 1090004)
                _item.confirm_user_id  = key.user_id;  //  TypeConverter.get_ext_type_name(item.user_group)

            var out_node = (event_hist)await g.webapi.post("event_hist", _item, typeof(event_hist));
            g.event_hist_list.Add(_item);
            return true;

        }

        #endregion

        private void txtlogin_password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox targetTextbox = sender as PasswordBox;
            string t1 = targetTextbox.Password;
            Char newChar;

            try
            {
                var a1 = t1.Substring(t1.Length - 1);
                newChar = a1[0];
            }
            catch
            {
                e.Handled = false;
                return;
            }
            e.Handled = true;

            int byteCount = Encoding.Default.GetByteCount(t1.ToString());
            if (byteCount < targetTextbox.MaxLength)
            {
                if ((newChar >= 33 && newChar <= 47) || (newChar >= 58 && newChar <= 64) || (newChar >= 91 && newChar <= 96) || (newChar >= 123 && newChar <= 126))
                {
                    targetTextbox.Password = "";
                    e.Handled = true;
                }
                else
                    e.Handled = false;
            }
            else
            {
                Console.WriteLine("총 글자수는 {0}자를 초과할 수 없습니다.", byteCount);
            }


        }
    }

}
