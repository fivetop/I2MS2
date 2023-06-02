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

            _user_id = Reg.get_saved_user_id();
            string enc_password = Reg.get_saved_password();
            _password = DESEncrypt.Decrypt(enc_password);
            _server = Reg.get_saved_server_domain();
            int lang_id = Reg.get_lang_id();
            _cboLanguage.SelectedIndex = lang_id == 1080002 ? 1 : 0;

            // Insert code required on object creation below this point.
            _id.Text = _user_id;
            _pw.Password = _password;
            _ip.Text = _server;
		}

        private async Task<bool> getUserData()
        {
            g.user_list = (List<user>)await g.webapi.getList("user", typeof(List<user>));
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
            g.web_api_uri_string = "http://" + server + ":5000/";
            g.signalr_uri_string = "http://" + server + ":5100/";

            g.webapi.set_server(g.web_api_uri_string);

            bool r1 = await getUserData();
            if (!r1)
            {
                MessageBox.Show(g.tr_get("C_Error_Server_Domain_2"));
                return;
            }

            var u = g.user_list.Find(p=> p.login_id == id);
            if (u == null)
            {
                login_error++;
                if (login_error > 5)
                {
                    MessageBox.Show(g.tr_get("C_Error_Login_Count"));
                    DialogResult = false;
                    return;
                }
                MessageBox.Show(g.tr_get("C_Error_Login"));
                return;
            }
            var r = g.user_list.Find(p => DESEncrypt.Decrypt(p.login_password) == pw);
            if (r == null)
            {
                login_error2++;
                if (login_error2 > 5)
                {
                    MessageBox.Show(g.tr_get("C_Error_Password_Count"));
                    DialogResult = false;
                    return;
                }
                MessageBox.Show(g.tr_get("C_Error_Password"));
                return;
            }
            string enc_password = DESEncrypt.Encrypt(pw);

            Reg.save_user_id_and_password(id, enc_password);
            Reg.save_server_domain(server);
            Reg.save_lang_id(lang_id);
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
            DragMove();
        }

	}
}