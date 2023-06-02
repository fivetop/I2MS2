// #define I2MS2_V21

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
using System.ComponentModel;

namespace I2MS2.Windows
{
    /// <summary>
    /// ManufactureManager.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    
    // 2015.09.03 romee  
    // 터미날 가져오기 모듈 -> 넷 스캔 등록 과 검색 결과 추가 

    public partial class NetworkScheduler_New : Window
    {
        public net_scan_scheduler nss;

        public NetworkScheduler_New(net_scan_scheduler _nss)
        {
            nss = _nss;

            InitializeComponent();
            initSchedule();
        }

        #region // 스케쥴러 처리 사용안함
        // 스케쥴러 내용을 출력한다.
        private void initSchedule()
        {
            txtStartTime.Text = nss.schedule_time;
            txtRepeatMinute.Text = nss.repeat_minute.ToString();
            rdoPattern2.IsChecked = (nss.enabled == "Y") && (nss.repeat_pattern == "E");    // Everyday
            rdoPattern3.IsChecked = (nss.enabled == "Y") && (nss.repeat_pattern == "S");    // Selecteed Day
            rdoPattern1.IsChecked = nss.enabled != "Y";
            chkDay0.IsChecked = nss.repeat_day0 == "Y";
            chkDay1.IsChecked = nss.repeat_day1 == "Y";
            chkDay2.IsChecked = nss.repeat_day2 == "Y";
            chkDay3.IsChecked = nss.repeat_day3 == "Y";
            chkDay4.IsChecked = nss.repeat_day4 == "Y";
            chkDay5.IsChecked = nss.repeat_day5 == "Y";
            chkDay6.IsChecked = nss.repeat_day6 == "Y";

            txtnetid.Text = "NetID: " + nss.net_scan_scheduler_id.ToString();
            enableScheduleControl();
        }

        private void enableScheduleControl()
        {
            if (txtRepeatMinute == null)
                return;
            try { 
                txtRepeatMinute.IsEnabled = rdoPattern1.IsChecked != true;
                txtStartTime.IsEnabled = rdoPattern1.IsChecked != true;
                bool enabled = rdoPattern3.IsChecked == true;
                chkDay0.IsEnabled = enabled;
                chkDay1.IsEnabled = enabled;
                chkDay2.IsEnabled = enabled;
                chkDay3.IsEnabled = enabled;
                chkDay4.IsEnabled = enabled;
                chkDay5.IsEnabled = enabled;
                chkDay6.IsEnabled = enabled;
            }
            catch(Exception e)
            {
            }
        }
        #endregion

        #region // Util
        // 음수인경우 마지막을 선택

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool b = saveData();

            if (b)
            {
                try
                {
                    this.DialogResult = true;
                }
                catch { }
                Close();
            }
        }

        private bool saveData()
        {
            // 스케쥴 저장
            nss.schedule_time = txtStartTime.Text.Trim();
            nss.repeat_minute = Etc.get_int(txtRepeatMinute.Text);
            nss.enabled = rdoPattern1.IsChecked != true ? "Y" : "N";
            nss.repeat_pattern = "-";
            if (rdoPattern2.IsChecked == true)
                nss.repeat_pattern = "E";       // Everyday
            if (rdoPattern3.IsChecked == true)
                nss.repeat_pattern = "S";       // Selecteed Day
            nss.repeat_day0 = chkDay0.IsChecked == true ? "Y" : "-";
            nss.repeat_day1 = chkDay1.IsChecked == true ? "Y" : "-";
            nss.repeat_day2 = chkDay2.IsChecked == true ? "Y" : "-";
            nss.repeat_day3 = chkDay3.IsChecked == true ? "Y" : "-";
            nss.repeat_day4 = chkDay4.IsChecked == true ? "Y" : "-";
            nss.repeat_day5 = chkDay5.IsChecked == true ? "Y" : "-";
            nss.repeat_day6 = chkDay6.IsChecked == true ? "Y" : "-";
            nss.last_updated = DateTime.Now;
            nss.user_id = g.login_user_id;
            //nss.dates = "";
            return true;
        }
        
        private void rdoPattern1_Checked(object sender, RoutedEventArgs e)
        {
            enableScheduleControl();
        }
        private void rdoPattern1_Unchecked(object sender, RoutedEventArgs e)
        {
            enableScheduleControl();
        }
        private void rdoPattern2_Checked(object sender, RoutedEventArgs e)
        {
            enableScheduleControl();
        }
        private void rdoPattern2_Unchecked(object sender, RoutedEventArgs e)
        {
            enableScheduleControl();
        }
        private void rdoPattern3_Checked(object sender, RoutedEventArgs e)
        {
            enableScheduleControl();
        }
        private void rdoPattern3_Unchecked(object sender, RoutedEventArgs e)
        {
            enableScheduleControl();
        }
        #endregion

        private void _btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }

        private void chkDay0_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
