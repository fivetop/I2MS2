using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WebApiClient;
using I2MS2.Models;
using I2MS2.Translation;
using I2MS2.Library;
using I2MS2.Windows;
using WebApi.Models;
using Microsoft.Win32;
using System.Threading.Tasks;
using Utils;

namespace I2MS2.Windows
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Login : Window
	{
        int login_error = 0;
        int login_error2 = 0;
        string _user_id = "";
        string _password = "";
        string _server = "";
        public Login()
		{
			this.InitializeComponent();

//#if GS_DEL
            _user_id = Reg.get_saved_user_id();
            _id.Text = _user_id;

            string enc_password = Reg.get_saved_password();

            _pw.Password = enc_password;

            _server = Reg.get_saved_server_domain();
            g.lang_id = Reg.get_lang_id();
//#endif
                       
            _server = g.host_string;
            int lang_id = g.lang_id;
            // GS_DEL
            _cboLanguage.SelectedIndex = lang_id == 1080002 ? 1 : 0;
            _ip.Text = _server;
		}

        private async Task<bool> getUserData()
        {
            bool aa = await g.getUserData();
            if (!aa)
                return false;
            g.event_list = (List<@event>)await g.webapi.getList("event", typeof(List<@event>));
            g.event_lang_list = (List<event_lang>)await g.webapi.getList("event_lang", typeof(List<event_lang>));
            g.event_hist_list = (List<event_hist>)await g.webapi.getList("event_hist", typeof(List<event_hist>));

            return g.user_list != null;
        }

        private async void _ok_Click(object sender, RoutedEventArgs e)
        {
            string id = _id.Text.Trim();
            string pw = _pw.Password;
            string server = _ip.Text.Trim();

            if (string.IsNullOrEmpty(server))
            {
                MessageBox.Show(g.tr_get("C_Error_Server_Domain"));
                return;
            }

            int lang = _cboLanguage.SelectedIndex;    
            int lang_id = 1080001;      // 한국어
            if (lang == 1)
                lang_id = 1080002;      // 영어


            g.host_string = server;
            // I2MS LED romee 
            g.web_api_uri_string = "http://" + server + ":5002/";
            g.signalr_uri_string = "http://" + server + ":5102/";

            g.webapi.set_server(g.web_api_uri_string);

            bool r1 = await getUserData();
            if (!r1)
            {
                MessageBox.Show(g.tr_get("C_Error_Server_Domain_2"));
                return;
            }
            // GS_DEL
            _test.Text = g._seever_time.ToLongDateString();
            // GS 인증 로긴 실패시 이력 저장 처리  2016.06.16
            event_hist _item = new event_hist();
            event_lang evl = g.event_lang_list.Find(at => (at.event_id == 1090009) && (at.lang_id == g.lang_id));

            if (evl == null)
            {
                _item.event_id = 1090009;
                _item.event_text = "Contents data check." + "(" + id + ")";  //  " / " + pw +
            }
            else
            {
                _item.event_id = evl.event_id;
                _item.event_text = evl.event_desc + "(" + id + ")";
            }
            _item.write_time = DateTime.Now;
            _item.is_confirm = "N";
            _item.event_type = "I";
            // GS 인증 로긴 실패시 이력 저장 처리  2016.06.16

            var u = g.user_list.Find(p=> p.login_id == id);
            if (u == null)
            {
                login_error++;
                if (login_error > 5)
                {
                    MessageBox.Show(g.tr_get("C_Error_Login_Count"));
                    await g.log_set(_item);
                    DialogResult = false;
                    return;
                }
                await g.log_set(_item);
                MessageBox.Show(g.tr_get("C_Error_Login"));
                return;
            }
            string pw2 = DESEncrypt.DecryptSHA_AES( u.login_password, pw);
            //var r = g.user_list.Find(p => p.login_password == pw2 && p.login_id == id);
            if (pw2 == null)
            {
                login_error2++;
                if (login_error2 > 5)
                {
                    MessageBox.Show(g.tr_get("C_Error_Password_Count"));
                    DialogResult = false;
                    await g.log_set(_item);
                    return;
                }
                MessageBox.Show(g.tr_get("C_Error_Password"));
                await g.log_set(_item);
                return;
            }

//#if GS_DEL
            //string enc_password = DESEncrypt.EncryptSHA(pw);
            Reg.save_user_id_and_password(id, pw);

            Reg.save_server_domain(server);
            Reg.save_lang_id(lang_id);
//#endif
            g.login_user_id = u.user_id;
            g.lang_id = lang_id;
            TranslationManager.set_lang(g.lang_id);
            try
            {
                DialogResult = true;
            }
            catch (Exception) { }
        }

        private void _no_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            //Close();
        }

        private void LayoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try { 
                DragMove();
            }
            catch(Exception e1)
            {
            }
        }

	}
}