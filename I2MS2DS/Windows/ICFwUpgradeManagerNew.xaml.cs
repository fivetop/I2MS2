using I2MS2.Library;
using I2MS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WebApi.Models;

namespace I2MS2.Windows
{
    /// <summary>
    /// ICFwUpgradeManagerNew.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ICFwUpgradeManagerNew : Window
    {
        Boolean _is_open_file = false;
        Boolean _is_select_lastupdate = false;
        Boolean _is_write_fwname = false;

        String select_file_path;
        String select_datetime;

        public static RoutedCommand SaveCommand = new RoutedCommand();
        
        public ICFwUpgradeManagerNew()
        {
            InitializeComponent();
        }

        private void _btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void _cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //e.CanExecute = (_is_open_file && _is_select_lastupdate && _is_write_fwname);
            e.CanExecute = (_is_open_file && _is_write_fwname);
        }

        private async void _cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!await saveData())
                return;

            Close();
        }

        private void _btnFwSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                //Filter = "Image Files|*.3d"
             //   Filter = "I2MS2 3DFiles|*.3d"
            };

            openFile.ShowDialog();

            Console.WriteLine("load filename = {0}", openFile.FileName);
            string path = openFile.FileName;

            _txbFwFileName.Text = openFile.SafeFileName;
            select_file_path = openFile.FileName;
            _is_open_file = true;
        }
#if false
        private void _dpFwLastUpdate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var picker = sender as DatePicker;

            DateTime? date = picker.SelectedDate;
            if (date != null)
            {
                select_datetime = date.ToString();

                //DateTime t = (DateTime)date;   
                //_is_select_lastupdate = true;

                //String t_str = t.GetDateTimeFormats().GetValue(76).ToString();
                //byte[] test_bytes = Encoding.ASCII.GetBytes(t_str);
                

                //String t_str2 = ASCIIEncoding.ASCII.GetString(test_bytes);

                //DateTime dt2 = DateTime.Parse(t_str2);
                //String t_str3 = dt2.ToString();

            }
            
        }
#endif
        private void _txbFwName_TextChanged(object sender, TextChangedEventArgs e)
        {
            _is_write_fwname = true;
        }

        private async Task<bool> saveData()
        {
            fw_upgrade fw = new fw_upgrade()
            {
                fw_id = 0,
                fw_name = _txbFwName.Text,
                fw_file_name = _txbFwFileName.Text,
                fw_version = _txbFwVersion.Text,
                remarks = _txbFwRemarks.Text,
                user_id = g.login_user_id,
                last_updated = DateTime.Now
            };

            if (fw.fw_version == string.Empty)
            {
                System.Windows.Forms.MessageBox.Show("Empty Item..");
                return false;
            }
#if false
            fw.last_updated = BitConverter.GetBytes(_dpFwLastUpdate.SelectedDate.HasValue);
#endif 
            //서버에 fw파일을 업로드한다
            int UPLOAD_BUFF_SIZE = 40960;
            ProgressBarDialog progressbar_dialog = new ProgressBarDialog();
            Task<TransferResult> t1 = g.webapi.uploadFile("firmware", select_file_path, UPLOAD_BUFF_SIZE);
            progressbar_dialog.ShowDialog();

            TransferResult ret = await t1;
            if (ret.result_code != 0)
                return false;

            //서버 파일 네임 저장
            fw.fw_file_name = ret.server_file_name;

            //서버 db에 firmware를 업로드한다
            fw_upgrade fw_ret = (fw_upgrade)await g.webapi.post("fw_upgrade", fw, typeof(fw_upgrade));
            g.fw_upgrade_list.Add(fw_ret);
            if (fw_ret.fw_id == 0)
                return false;

            return true;
        }
    }
}
