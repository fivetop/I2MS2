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
namespace I2MS2.Windows
{

    // 알람 / 이벤트 메시지 수신 설정 처리 
    /// <summary>
    /// AlarmEventSetupManager.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AlarmEventSetupManager : Window
    {
        List<EventVM> ev_list = new List<EventVM>();

        public partial class EventVM
        {
            public int event_id { get; set; }
            public string event_type { get; set; }
            public Boolean popup_screen { get; set; }
            public Boolean send_email { get; set; }
            public Boolean send_sms { get; set; }
            public string event_desc { get; set; }
            public string event_group { get; set; }
            public string event_format { get; set; }
            public Boolean is_modify { get; set; }
        }

        public AlarmEventSetupManager()
        {
            InitializeComponent();
            foreach(var e in g.event_list)
            {
                if (e.event_id < 1090101 || e.event_id > 1090125) continue;    // GS_DEL
                    ev_list.Add(makeEventVM(e));
            }

            _lvAlarmEvent.ItemsSource = ev_list;
        }


        private void _lvAlarmEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private EventVM makeEventVM(@event ev)
        {
            event_lang evl = g.event_lang_list.Find(at => (at.event_id == ev.event_id) && (at.lang_id == g.lang_id));

            EventVM ev_vm =  new EventVM()
            {
                event_id = ev.event_id,
                event_type = Etc.get_event_name(ev.event_type),
                event_desc = evl.event_desc,
                event_format = evl.event_format,
                event_group = evl.event_group,
                popup_screen =false,
                send_email = false,
                send_sms = false,
                is_modify = false
            };
            if(ev.popup_screen =="Y")
                ev_vm.popup_screen = true;

            if(ev.send_email =="Y")
                ev_vm.send_email = true;
            
            if(ev.send_sms =="Y")
                ev_vm.send_sms = true;

            return ev_vm;
        }

        private async void _btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!await saveData())
                return;

            Close();
        }

        private async Task<bool> saveData()
        {
            foreach(var ev_vm in ev_list)
            {
                if(ev_vm.is_modify)
                {
                    @event ev = g.event_list.Find(at => at.event_id == ev_vm.event_id);
                    if (ev_vm.popup_screen)
                        ev.popup_screen = "Y";
                    else
                        ev.popup_screen = "N";

                    if (ev_vm.send_email)
                        ev.send_email = "Y";
                    else
                        ev.send_email = "N";

                    if (ev_vm.send_sms)
                        ev.send_sms = "Y";
                    else
                        ev.send_sms = "N";

                    int ret = await g.webapi.put("event", ev.event_id, ev, typeof(@event));
                    if (ret != 0)
                    {
                        MessageBox.Show(g.tr_get("C_Error_Server"));
                        return false;
                    }
                }
            }

            return true;
        }

        private void _chkSMS_Checked(object sender, RoutedEventArgs e)
        {
            itemChecked(e);
        }

        private void _chkVEmail_Checked(object sender, RoutedEventArgs e)
        {
            itemChecked(e);
        }

        private void _chkPopup_Checked(object sender, RoutedEventArgs e)
        {
            itemChecked(e);
        }

        private void _chkPopup_Unchecked(object sender, RoutedEventArgs e)
        {
            itemChecked(e);
        }

        private void _chkVEmail_Unchecked(object sender, RoutedEventArgs e)
        {
            itemChecked(e);
        }

        private void _chkSMS_Unchecked(object sender, RoutedEventArgs e)
        {
            itemChecked(e);
        }

        private void itemChecked(RoutedEventArgs e)
        {
            var v = e.OriginalSource;
            if (v is CheckBox)
            {
                CheckBox ck = (CheckBox)v;
                var src = ck.DataContext;
                if (src is EventVM)
                {
                    EventVM ev_vm = (EventVM)src;
                    ev_vm.is_modify = true;
                }
            }
        }

    
    }
}
