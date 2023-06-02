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

    /// <summary>
    /// UserManager.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SendManager : Window
    {
        #region RouteCommand 버튼 관련 정의
        public static RoutedCommand EditCommand = new RoutedCommand();
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand CancelCommand = new RoutedCommand();

        private bool _edit = false;
        private bool _save = false;
        private bool _cancel = false;
        #endregion

        public SendManager()
        {
            InitializeComponent();
            InitData();
            _edit = true;
            enableControl(false);
        }

        private void InitData()
        {
#if GS_DEL
            txtmail_server.Text = g.mail_server;
            txtmail_port.Text = string.Format("{0}", g.mail_port);
            txtmail_id.Text = g.mail_id;
            txtmail_pw.Text = g.mail_pw;

            txtsms_url.Text = g.sms_server;
            txtsms_id.Text = g.sms_id;
            txtsms_pw.Text = g.sms_pw;

            _ckbx1.IsChecked = g.mail_use == 0 ? false : true;
            _ckbx2.IsChecked = g.sms_use == 0 ? false : true;
#endif
      }

        #region CRUD 신규,삭제 등 버튼 처리 로직


        private void _cmdEdit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _edit;
        }

        private void _cmdEdit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            enableControl(true);
            _edit = false;
            _save = true;
            _cancel = true;
            txtmail_server.Focus();
        }

        private void _cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtmail_server.Text))
            {
                e.CanExecute = false;
                return;
            }
            e.CanExecute = _save;
        }
        // 저장 처리 
        private void _cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!saveLeft())
                return;
            _edit = true;
            _save = false;
            _cancel = false;
            enableControl(false);
            CommandManager.InvalidateRequerySuggested();
        }
        private void _cmdCancel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _cancel;
        }

        private void _cmdCancel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            enableControl(false);
            _edit = true;
            _save = false;
            _cancel = false;
        }
        #endregion


        // enable control 로직
        private void enableControl(bool flag)
        {
            _ckbx1.IsEnabled = flag;
            _ckbx2.IsEnabled = flag;
            txtmail_server.IsEnabled = flag;
            txtmail_port.IsEnabled = flag;
            txtmail_id.IsEnabled = flag;
            txtmail_pw.IsEnabled = flag;
            txtsms_url.IsEnabled = flag;
            txtsms_id.IsEnabled = flag;
            txtsms_pw.IsEnabled = flag;
        }


        #region add, edit save 로직, delete 로직
        private bool saveLeft()
        {
#if GS_DEL
            if (_ckbx1.IsChecked == true)
            {
                g.mail_server = txtmail_server.Text.ToString();
                g.mail_port = Etc.get_int( txtmail_port.Text.ToString());
                g.mail_id = txtmail_id.Text.ToString();
                g.mail_pw = txtmail_pw.Text.ToString();
            }
            if (_ckbx2.IsChecked == true)
            {
                g.sms_server = txtsms_url.Text.ToString();
                g.sms_id = txtsms_id.Text.ToString();
                g.sms_pw = txtsms_pw.Text.ToString();
            }
#endif
            if ((txtmail_server.Text.ToString() == string.Empty) || (txtmail_port.Text.ToString() == string.Empty) || (txtmail_id.Text.ToString() == string.Empty) || (txtmail_pw.Text.ToString() == string.Empty))
            {
                //MessageBox.Show("Check setting value!!");
                return false;
            }
            if ((txtsms_url.Text.ToString() == string.Empty) || (txtsms_id.Text.ToString() == string.Empty) || (txtsms_pw.Text.ToString() == string.Empty) )
            {
                //MessageBox.Show("Check setting value!!");
                return false;
            }
            return true;
        }
        #endregion

        private void _btnEdit_Copy_Click(object sender, RoutedEventArgs e)
        {
#if GS_DEL

            SendMag mail = new SendMag();
            mail.body = "사용자 정보	사용자가 로그인하였습니다.";
            mail.subject = "사용자가 로그인하였습니다.";

            mail.to = "romyoh@gmail.com";
            mail.tosms = "010-6778-1009";
            mail.Send_email();
            //mail.Send_sms();
#endif
        }
    }
}
