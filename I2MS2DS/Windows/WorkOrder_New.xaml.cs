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
using System.ComponentModel;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using I2MS2.Library;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;

namespace I2MS2.Windows
{
    /// <summary>
    /// DrawingsManager.xaml에 대한 상호 작용 논리
    /// </summary>
    ///

    public partial class WorkOrder_New : Window
    {
        int _pp_asset_id = 0;
        int _ic_xc_mode = 0;                            // 1=ic, 2=xc
        int _tot_task_cnt = 0;

        public int _wo_id = 0;                          // return 값
        public bool _reserve_flag = false;              // return
        public DateTime _reserved_date = DateTime.Now;  // return
        public int smartphone = 0;                      // return 값


        public WorkOrder_New(int pp_asset_id, int ic_xc_mode, int tot_task_cnt)
        {
            _pp_asset_id = pp_asset_id;
            _ic_xc_mode = ic_xc_mode;
            _tot_task_cnt = tot_task_cnt;
            InitializeComponent();
            initData();
        }

        #region Event 루틴
        private async void _btnStart_Click(object sender, RoutedEventArgs e)
        {
            smartphone = 0;
            if (!await saveData())
                return;

            DialogResult = true;
            Close();
        }

        private void _btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private async void _btnPhone_Click(object sender, RoutedEventArgs e)
        {
            smartphone = 2;
            if (!await saveData())
                return;

            DialogResult = true;
            Close();
        }

        
        #endregion

        #region 그 외 메소드

        private void initData()
        {
            int wo_id = 1;
            try
            {
                wo_id = g.work_order_list.Max(p => p.wo_id) + 1;
            }
            catch (Exception) { };
            
            DateTime dt = DateTime.Now;
            _txtWorkOrderDesc.Text = string.Format("{0}_{1}_{2}_{3}", dt.ToShortDateString(), dt.ToShortTimeString(), g.tr_get("C_Work"), wo_id);
            DateTime dt2 = dt.AddHours(1);
            _dateDate.SelectedDate = dt2;
            _txtHour.Text = dt2.Hour.ToString();
            _txtMinute.Text = "0";
        }

        private async Task<bool> saveData()
        {
            bool reserve_flag = _rdoReserved2.IsChecked == true;
            int hour = 9;
            int minute = 0;
            bool b1 = false;
            bool b2 = false;
            bool b3 = false;
            b1 = int.TryParse(_txtHour.Text, out hour);
            b2 = int.TryParse(_txtHour.Text, out minute);

            if (hour < 0)
                b3 = false;
            if (hour > 23)
                b3 = false;
            if (minute > 59)
                b3 = false;
            if (minute < 0)
                b3 = false;
            if (_dateDate.SelectedDate == null)
                b3 = false;
            DateTime reserved_date = _dateDate.SelectedDate ?? DateTime.Now;

            if ((b1 || b2 || b3) && reserve_flag)
            {
                //MessageBox.Show(g.tr_get("C_Error_6"));
                MessageBox.Show(g.tr_get("C_Error_Reserved_date"));
                return false;
            }
            reserved_date.AddHours(hour);
            reserved_date.AddMinutes(minute);
            DateTime? reserved_date2 = null;
            if (reserve_flag)
                reserved_date2 = reserved_date;

            work_order node = new work_order()
            {
                pp_asset_id = _pp_asset_id,
                process_user_id = g.login_user_id,
                reg_user_id = g.login_user_id,
                wo_name = _txtWorkOrderDesc.Text.Trim(),
                remarks = _txtRemarks.Text.Trim(),
                wo_result = "R",        // R=등록, C=Cancel, F=Finish
                wo_xc_connect_type = _ic_xc_mode == 1 ? "I" : "X",
                reserve_flag = reserve_flag ? "Y" : "-",
                reserved_date = reserved_date2,
                tot_task_cnt = _tot_task_cnt,
                write_date = DateTime.Now
            };

            work_order new_node = (work_order) await g.webapi.post("work_order", node, typeof(work_order));
            if (new_node == null)
            {
                MessageBox.Show(g.tr_get("C_Error_6"));
                return false;
            }

            g.work_order_list.Add(new_node);
            _wo_id = new_node.wo_id;
            _reserve_flag = reserve_flag;
            _reserved_date = reserved_date;
            return true;
        }

        #endregion

    }
}
