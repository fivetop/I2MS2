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
using System.Windows.Navigation;
using System.Windows.Shapes;
using I2MS2.Translation;
using I2MS2.Models;
using I2MS2.UserControls;
using System.Drawing;
using WebApi.Models;
using I2MS2.Library;
using I2MS2.Animation;
using System.Globalization;
using I2MS2.Windows;
using System.Threading;

namespace I2MS2.Pages
{
    /// <summary>
    /// P5LineManager.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public partial class P5LineManager : Page
    {
        #region // 선언부

        Color color_red = (Color)Application.Current.Resources["_colorRed"];
        Color color_green = (Color)Application.Current.Resources["_colorGreen"];
        Color color_blue = (Color)Application.Current.Resources["_colorBlue"];

        LinkDiagram ld = new LinkDiagram();

        List<List<WorkCell>> cell_list3 = new List<List<WorkCell>>();           // 전체 패치 리스트셀(탭컨트롤 수 만큼-가장 외곽 리스트)
        ListView _lv1 = null;                                                   // 현재 선택한 탭의 리스트뷰
        public List<ipp_list> _ipps = new List<ipp_list>();                     // 패치 리스트
        ipp_list _ipp = null;                                                   // 선택한 패치

        public static RoutedCommand AddCableCommand = new RoutedCommand();              // 케이블 
        public static RoutedCommand TurnAssetCommand = new RoutedCommand();             // 자산 전후 변경  
        public static RoutedCommand ScanICCommand = new RoutedCommand();                // 컨트롤러 스캔
        public static RoutedCommand AcceptConnectionCommand = new RoutedCommand();      // 인가 / 비인가 처리 
        public static RoutedCommand CopyCommand = new RoutedCommand();                  // 복사
        public static RoutedCommand PasteCommand = new RoutedCommand();                 // 붙이기 
        public static RoutedCommand EscapeCommand = new RoutedCommand();                // 취소
        public static RoutedCommand DeleteCommand = new RoutedCommand();                // 삭제
        public static RoutedCommand WorkOrderCommand = new RoutedCommand();             // 작업오더
        public static RoutedCommand CancelWorkOrderCommand = new RoutedCommand();       // 작업 취소    
        public static RoutedCommand SaveCommand = new RoutedCommand();                  // 저장 -> 스위치 상태 변경 
        #endregion

        public P5LineManager()
        {
            g._P5LineManager = this;

            InitializeComponent();

            AddCableCommand.InputGestures.Add(new KeyGesture(Key.Insert));
            TurnAssetCommand.InputGestures.Add(new KeyGesture(Key.F2));
            CopyCommand.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Control));
            PasteCommand.InputGestures.Add(new KeyGesture(Key.V, ModifierKeys.Control));
            EscapeCommand.InputGestures.Add(new KeyGesture(Key.Escape));
            DeleteCommand.InputGestures.Add(new KeyGesture(Key.Delete));
            SaveCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));

            // 탭컨트롤에서 탭메뉴 종료 명령이 동작하려면 아래가 있어야 한다.
            this.DataContext = this;

            getPPList();

            // 탭컨트롤과 리스트뷰에 초기 샘플로 기록되어 있는 항목들을 지운다.
            _tc.Items.Clear();
            _lvLeft.Items.Clear();
            _lvLeft.ItemsSource = _ipps;

            open_asset_by_registry();
        }
        #region // 초기화 처리 
        
        // 링크다이어그램 생성 
        private void getPPList()
        {
            // i-PP, PP, i-FDF, FDF 등 패치 패널류 포함
            var _ipps2 = from aa in g.asset_list
                         join cc in g.catalog_list on aa.catalog_id equals cc.catalog_id
                         join ll in g.location_list on aa.location_id equals ll.location_id
                         orderby ll.location_path
                         where CatalogType.is_pp(cc.catalog_id)
                         select new ipp_list()
                         {
                             asset_id = aa.asset_id,
                             asset_name = aa.asset_name,
                             catalog_id = aa.catalog_id,
                             catalog_group_id = cc.catalog_group_id,
                             location_path = ll.location_path,
                             floor_name = g.floor_list.First(e => e.floor_id == ll.floor_id).floor_name,
                             room_name = g.room_list.First(e => e.room_id == ll.room_id).room_name,
                             checked_color = color_red
                         };

            _ipps = _ipps2.ToList();
        }
        // 레지스트리에서 열려진 링크 다이어그램 가져오기 
        private void open_asset_by_registry()
        {
//            return;
            // GS_DEL 
//#if GS_DEL
            var asset_list = Reg.get_link_diagram();
            if (asset_list == null)
                return;

            foreach(var node in asset_list)
            {
                ipp_list ipp = _ipps.Find(p => p.asset_id == node);
                if (ipp != null)
                    open_sheet(ipp);
            }
//#endif
        }
        #endregion

        #region // Command 루틴
        private void _cmdAddCable_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eAddCable))
            {
                e.CanExecute = false;
                return;
            }

            WorkCell vm = (WorkCell) _lv1.SelectedItem;

            if ((vm.template_type == "empty") && (vm.catalog_id == 0) && ((vm.col_no % 2) == 1))
            {
                int idx = vm.idx;
                var list = _ipp.list;
                WorkCell left = list[idx - 1];
                WorkCell right = list[idx + 1];

                // 좌우측에 장치가 있어야 한다.
                if ((left.template_type != "asset") || (right.template_type != "asset"))
                {
                    e.CanExecute = false;
                    return;
                }

                // 한쪽방향에만 플러그가 있는 경우 후면이보이면 케이블 선택할 수 없도록 한다.
                // 현재 스위치 업 링크가 안됨 -> 인터 컨넥션 처리 때문 
                // romee 2015.09.08 
                if (CatalogType.is_one_side(left.catalog_id) && left.is_left_front)
                {
                    e.CanExecute = false;
                    return;
                }
                if (CatalogType.is_one_side(right.catalog_id) && !right.is_left_front)
                {
                    e.CanExecute = false;
                    return;
                }

                e.CanExecute = true;
                return;
            }
            e.CanExecute = false;
        }
        // 케이블 처리 
        private void _cmdAddCable_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WorkCell vm = (WorkCell) _lv1.SelectedItem;

            if ((vm.template_type == "empty") && (vm.catalog_id == 0) && ((vm.col_no % 2) == 1)) // 워크셀이 빈건가?
            {
                int idx = vm.idx;
                var list = _ipp.list;
                int cable_catalog_id = 0;
                WorkCell left = list[idx - 1];
                WorkCell right = list[idx + 1];
                // 중심 라인은 무조건 지능형임 기본 케이블 사용  
                if (((vm.col_no - 1) == g.CENTER_COL) && (CatalogType.is_ipp(left.catalog_id)))
                    cable_catalog_id = Etc.get_standard_ica(left.asset_id);
                // 이미 선택된 케이블이 있으면 케이블 선택 필요없음 
                // 인터에서 케이블 선택 필요 없음 
                // romee 2015.09.09
                if (cable_catalog_id == 0)
                {
                    CableSelector window = new CableSelector(left, right);
                    window.Owner = App.Current.MainWindow;
                    bool b = window.ShowDialog() ?? false;
                    if (!b)
                        return;
                    cable_catalog_id = window._vm.catalog_id;
                }
                // 선택된 케이블 배선 처리 
                ld.insertCableCell(list, idx, cable_catalog_id);
            }
            return;
        }

        private void _cmdScanIC_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eScanIC))
            {
                e.CanExecute = false;
                return;
            }

            WorkCell vm = (WorkCell)_lv1.SelectedItem;
            if (vm == null)
            {
                e.CanExecute = false;
                return;
            }

            if (vm.col_no != 10)
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = CatalogType.is_pp(vm.catalog_id);
        }
        // 컨트롤러 스캔 
        private void _cmdScanIC_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            int ipp_asset_id = _ipp.asset_id;
            int ic_asset_id = Etc.get_ic_asset_id_by_ipp_asset_id(ipp_asset_id);
            if (ic_asset_id == 0)
                return;
            g.main_window.scan_ic(ic_asset_id);
        }

        private void _cmdAcceptConnection_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eAcceptConnection))
            {
                e.CanExecute = false;
                return;
            }

            WorkCell vm = (WorkCell)_lv1.SelectedItem;
            e.CanExecute = is_accept_alarm(vm) > 0;
        }
        // 인가, 비인가 처리 
        private int is_accept_alarm(WorkCell vm)
        {
            if ((vm.template_type != "empty") && 
                ((vm.col_no == g.CENTER_COL) || (vm.col_no == (g.CENTER_COL+1)) || (vm.col_no == (g.CENTER_COL+2))))
            {
                if (vm.template_type == "cable")
                {
                    WorkCell left = _ipp.list[vm.idx - 1];
                    WorkCell right = _ipp.list[vm.idx + 1];

                    if ((left.alarm_status == eAlarmStatus.None) && (right.alarm_status == eAlarmStatus.None))
                    {
                        return 0;
                    }
                    //좌측이 인터커넥트IPP 이면서...연결 해지일때.....처리 가능...
                    //좌측이 인터커넥트IPP 이면서...연결 하려하거나.... 연결을 하지 않으려 할때 처리 가능....
                    //반쪽만 덜렁 거리는 형태는 불가능...
                    if (CatalogType.is_ipp_ic(left.catalog_id))
                    {
                        if ((left.asset_id > 0) && (right.asset_id > 0) && (get_cur_port_status(left) == ePortStatus.Unplugged))
                            return 2;    // 여기서 절단 허용....                            
                        else
                            return 0;
                    }
                    else
                    {
                        if ((left.asset_id > 0) && (right.asset_id > 0) &&
                            (get_cur_port_status(left) == ePortStatus.Unplugged) && (get_cur_port_status(right) == ePortStatus.Unplugged))
                            return 2;   // 여기서 절단 허용...
                        else if ((left.asset_id > 0) && (right.asset_id > 0) &&
                            (get_cur_port_status(left) == ePortStatus.Linked) && (get_cur_port_status(right) == ePortStatus.Linked))
                            return 1;   // 여기서 연결 허용...
                        else
                            return 0;
                    }
                }
            }
            return 0;
        }
        // 인가 / 비인가 처리 
        private async void _cmdAcceptConnection_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WorkCell vm = (WorkCell)_lv1.SelectedItem;
            if (vm == null)
                return;

            // accept 조건에 만족하는 상황인지 확인.
            int connect = is_accept_alarm(vm);  // 1=연결, 2=단절, 0=해당 없음

            int idx = vm.idx;
            WorkCell left = _ipp.list[idx - 1];
            WorkCell right = _ipp.list[idx + 1];
            
            switch(connect)
            {
                case 1 :
                case 2:
                    // 알람허용요청....
                    if (left.alarm_status != eAlarmStatus.None)
                        await request_cancel_alarm(left, right);
                    if (right.alarm_status != eAlarmStatus.None)
                        await request_cancel_alarm(right, left);
                    // 웹서버에 연결을 요청한다. (IC용과 XC용 분리)
                    await request_make_connect(left, right);                     
                    break;
                default:
                    return;
            }
        }

        public async Task<bool> request_cancel_alarm(WorkCell left, WorkCell right)
        {
            int ipp_asset_id = left.asset_id;
            int sys_id = Etc.get_sys_id_by_ipp_asset_id(ipp_asset_id);
            int pp_id = Etc.get_pp_id_by_asset_id(ipp_asset_id);
            int port_no = left.port_no;
            int remote_asset_id = 0;
            int remote_sys_id = 0;
            int remote_pp_id = 0;
            int remote_port_no = 0;
            int alarm_type = 0; // cancel alarm
            ePortStatus port_status = get_cur_port_status(ipp_asset_id, port_no);
            ePortStatus remote_port_status = port_status == ePortStatus.Linked ? ePortStatus.Linked : ePortStatus.Unplugged;
            ePPMode pp_mode = CatalogType.is_ipp_ic(left.catalog_id) ? ePPMode.eIC : ePPMode.eXC;

            if (pp_mode == ePPMode.eXC)
            {
                remote_asset_id = right.asset_id;
                remote_sys_id = Etc.get_sys_id_by_ipp_asset_id(remote_asset_id);
                remote_pp_id = Etc.get_pp_id_by_asset_id(remote_asset_id);
                remote_port_no =  right.port_no;
            }

            request re = new request();
            re.CancelAlarm(sys_id, pp_id, port_no, remote_sys_id, remote_pp_id, remote_port_no, remote_port_status, alarm_type);

            var rr = (request) await g.webapi.post("request", re, typeof(request));
            if (rr == null)
                return false;

            return true;
        }

        public async Task<bool> request_make_connect(WorkCell left, WorkCell right)
        {
            int ipp_asset_id = left.asset_id;
            int sys_id = Etc.get_sys_id_by_ipp_asset_id(ipp_asset_id);
            int pp_id = Etc.get_pp_id_by_asset_id(ipp_asset_id);
            int port_no = left.port_no;
            int remote_asset_id = right.asset_id;;
            int remote_sys_id = 0;
            int remote_pp_id = 0;
            int remote_port_no = right.port_no;
            ePortStatus port_status = get_cur_port_status(ipp_asset_id, port_no);
            ePortStatus link_status = port_status == ePortStatus.Linked ? ePortStatus.Linked : ePortStatus.Unplugged;
            ePPMode pp_mode = CatalogType.is_ipp_ic(left.catalog_id) ? ePPMode.eIC : ePPMode.eXC;

            request re = new request();
            if (pp_mode == ePPMode.eXC)
            {
                remote_sys_id = Etc.get_sys_id_by_ipp_asset_id(remote_asset_id);
                remote_pp_id = Etc.get_pp_id_by_asset_id(remote_asset_id);
                re.ForceMakeConnectXC(sys_id, pp_id, port_no, remote_sys_id, remote_pp_id, remote_port_no, link_status);
            }
            else
            {
                re.ForceMakeConnectIC(ipp_asset_id, port_no, remote_asset_id, remote_port_no, link_status);
            }

            var rr = (request)await g.webapi.post("request", re, typeof(request));
            if (rr == null)
                return false;

            return true;
        }

        // 현재 포트 상태 
        public ePortStatus get_cur_port_status(WorkCell cell)
        {
            ePortStatus status = get_cur_port_status(cell.asset_id, cell.port_no);
            return status;
        }

        // 현재 포트 상태 
        public ePortStatus get_cur_port_status(int ipp_asset_id, int port_no)
        {
            var aipl = g.asset_ipp_port_link_list.Find(p => (p.ipp_asset_id == ipp_asset_id) && (p.port_no == port_no));
            if (aipl == null)
                return ePortStatus.Unknown;
            ePortStatus status = Etc.get_status_type(aipl.ipp_port_status);
            return status;
        }



        private void _cmdTurnAsset_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eTurnAsset))
            {
                e.CanExecute = false;
                return;
            }

            WorkCell vm = (WorkCell)_lv1.SelectedItem;

            if ((vm.template_type == "asset") && (vm.col_no != g.CENTER_COL))
            {
                if (CatalogType.is_ipp_fp(vm.catalog_id) && (vm.col_no == 8 || vm.col_no == 14))  // 마곡 간선
                { 
                    e.CanExecute = false;
                    return;
                }
                // 케이블이 하나라도 연결되어 있는 경우는 회전이 안됨.
                e.CanExecute = ((vm.front_cable_catalog_id == 0) && (vm.rear_cable_catalog_id == 0));
                return;
            }
            e.CanExecute = false;
        }
        // 자산 전후 방향 교체 
        private void _cmdTurnAsset_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WorkCell vm = (WorkCell)_lv1.SelectedItem;

            if (vm != null)
            {
                if ((vm.template_type == "asset") && (vm.col_no != g.MAX_COL))
                {
                    // 자산 회전

                    var list = (List<WorkCell>)_lv1.ItemsSource;
                    ld.turn_asset(list, vm.idx);
                    return;
                }
            }
        }

        private bool _paste = false;
        private WorkCell _copy_vm = null;
        private void _cmdCopy_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eCopy2))
            {
                e.CanExecute = false;
                return;
            }

            WorkCell vm = (WorkCell)_lv1.SelectedItem;
            if (vm != null)
            {
                if (vm.template_type != "empty")
                {
                    e.CanExecute = true;
                    return;
                }
            }
            e.CanExecute = false;
        }
        // 복사 
        private void _cmdCopy_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WorkCell vm = (WorkCell)_lv1.SelectedItem;
            if (vm != null)
            {
                _paste = true;
                if (_copy_vm != null)
                {
                    _copy_vm.selected_cell = false;
                    _copy_vm.force_changed = true;
                }
                _copy_vm = vm;
                _copy_vm.selected_cell = true;
                _copy_vm.force_changed = true;
            }
        }


        private void _cmdEscape_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        // 취소
        private void _cmdEscape_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _paste = false;
            if (_copy_vm != null)
            {
                _copy_vm.selected_cell = false;
                _copy_vm.force_changed = true;
                _copy_vm = _copy_vm = null;
            }
        }

        private void _cmdPaste_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.ePaste2))
            {
                e.CanExecute = false;
                return;
            }

            WorkCell vm = (WorkCell)_lv1.SelectedItem;
            if (vm != null)
            {
                if (vm.template_type == "empty")
                {
                    e.CanExecute = _paste;
                    return;
                }
            }
            e.CanExecute = false;
        }
        // 붙여넣기 
        private void _cmdPaste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WorkCell s = _copy_vm;
            WorkCell d = (WorkCell)_lv1.SelectedItem;
            int source_idx = s.idx;
            int dest_idx = _lv1.SelectedIndex;
            if (dest_idx >= 0)
            {
                if (s.template_type == "asset")
                {
                    int asset_id = _copy_vm.asset_id;
                    int port_no = ld.get_clone_port_no(asset_id, s.port_no, cell_list3);
                    if (port_no > 0)
                    {
                        if (ld.is_asset_cell(d) && ld.is_asset_cell(s))
                        {
                            ld.cloneAssetCell(d, asset_id, port_no, cell_list3, _ipp.list);
                        }
                    }
                }
                else if (s.template_type == "cable")
                {
                    if (!ld.is_asset_cell(d) && !ld.is_asset_cell(s))
                    {
                        var list = (List<WorkCell>)_lv1.ItemsSource;
                        ld.insertCableCell(list, dest_idx, s.catalog_id);
                    }
                }
            }
        }

        private void _cmdDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eDelete2))
            {
                e.CanExecute = false;
                return;
            }

            WorkCell vm = (WorkCell)_lv1.SelectedItem;
            if (vm != null)
            {
                if (vm.template_type != "empty")
                {
                    e.CanExecute = true;
                    return;
                }
            }
            e.CanExecute = false;
        }
        // 삭제 
        private void _cmdDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WorkCell vm = (WorkCell)_lv1.SelectedItem;
            if (vm != null)
            {
                int idx = _lv1.SelectedIndex;
                List<WorkCell> list = (List<WorkCell>) _lv1.ItemsSource;
                ld.deleteCell(list, idx);
            }
        }

        private void _cmdWorkOrder_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eWorkOrder))
            {
                e.CanExecute = false;
                return;
            }


            if (g.work_order_progressing)
            {
                e.CanExecute = false;
                return;
            }

            if (_ipp == null)
            {
                e.CanExecute = false;
                return;
            }
            var list = _ipp.list;

            foreach (var node in list)
            {
                if (node.is_wo_mark)
                {
                    e.CanExecute = true;
                    return;
                }
            }
            e.CanExecute = false;
        }
        // 작업 오더 
        private async void _cmdWorkOrder_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            int ipp_asset_id = _ipp.asset_id;
            int ic_xc_mode = CatalogType.is_ipp_ic(_ipp.catalog_id) ? 1 : 2;
            int tot_task_cnt = 0;
            foreach (var vm in _ipp.list)
            {
                if (vm.is_wo_mark)
                    tot_task_cnt++;
            }

            // 알람이 존재하는 경우 경고, romee alarm exception
            if (_ipp != null)
            {
                List<WorkCell> cell_list2 = (List<WorkCell>) _lv1.ItemsSource;
                if (cell_list2 != null)
                {                
                    var node = cell_list2.Find(p => (p.alarm_status != eAlarmStatus.None) && (p.asset_id > 0)); 
                    if (node != null)
                    {
                        MessageBox.Show(g.tr_get("C_Error_WorkOrder_Alarm"));
                        return;
                    }
                }

                //int num_of_ports = Etc.get_num_of_ports_by_asset_id(ipp_asset_id);
                //int i = 0;
                //for (i = 0; i < num_of_ports; i++)
                //{
                //    var aippl = g.asset_ipp_port_link_list.Find(p => (p.ipp_asset_id == ipp_asset_id) && (p.port_no == (i + 1)));
                //    if (aippl != null)
                //    {
                //        if ((aippl.alarm_status == "U") || (aippl.alarm_status == "P"))
                //        {
                //            MessageBox.Show(g.tr_get("C_Error_WorkOrder_Alarm"));
                //            return;
                //        }
                //    }
                //}
            }

            WorkOrder_New window = new WorkOrder_New(ipp_asset_id, ic_xc_mode, tot_task_cnt);
            window.Owner = App.Current.MainWindow;
            bool b = window.ShowDialog() ?? false;
            if (b == true)
            { 
                // work_order table은 기록되었음
                int wo_id = window._wo_id;
                bool reserve_flag = window._reserve_flag;
                DateTime reserved_date = window._reserved_date;
                int smartphone = window.smartphone;                      // return 값     romee 스마트폰 스타트 처리 

                // work_order_task table에 기록할 차례

                bool bb = await processWorkOrder(wo_id, reserve_flag, reserved_date, smartphone);

                if (bb)
                {
                    // 작업 지시는 서버가 백그라운드로 진행합니다.
                    // 서버는 work_order에서 전체 task count를 알아내고....
                    // work_order_task로 데이터가 insert 될 때 마다 마지막 task_no를 확인하여 
                    // 데이터가 모두 DB에 기록이 된 후부터 작업지시 프로세스가 동작한다.
                    g.wo_id = wo_id;
                    if (smartphone == 2)
                    {
                       // g.work_order_progressing = true;      // 시험 필요 스마트 폰 기능 v2.1 // romee 2015.11.27
                    }
                    // g.work_order_progressing = true; <-- 이벤트를 받아서 처리...
                }
            }
            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }

        private void _cmdCancelWorkOrder_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eWorkOrder))
            {
                e.CanExecute = false;
                return;
            }

            if (!g.work_order_progressing)
            {
                e.CanExecute = false;
                return;
            }

            if (_ipp == null)
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = true;
        }
        // 작업 오더 취소 
        private async void _cmdCancelWorkOrder_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            int wo_id = g.wo_id;
            if (wo_id == 0)
                return;
            var node = g.work_order_list.Find(p => p.wo_id == wo_id);
            if (node == null)
                return;
            node.wo_result = "C";       // cancel
            node.write_date = DateTime.Now;
            var b = await g.webapi.put("work_order", wo_id, node, typeof(request));
            if (b != 0)
                return;
            g.work_order_progressing = false;
            g.wo_id = 0;

            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }

        private void _cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eSaveConnect))
            {
                e.CanExecute = false;
                return;
            }

            if (_ipp == null)
                return;
            int num_of_ports = _ipp.num_of_ports;
            var list = _ipp.list;

            foreach(var node in list)
            {
                if (node.is_ins_mark || node.is_del_mark)
                {
                    e.CanExecute = true;
                    return;
                }
            }
            e.CanExecute = false;
        }
        // 저장 
        private async void _cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_ipp == null)
                return;
            int num_of_ports = _ipp.num_of_ports;
            var list = _ipp.list;
            bool b1 = await saveData(list, num_of_ports);
            if (!b1)
            {

            }
            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }

        #endregion

        #region // 탭 관련 Command

        private Utils.RelayCommand _cmdCloseCommand;
        /// <summary>
        /// Returns a command that closes a TabItem.
        /// </summary>
        public ICommand CloseCommand
        {
            get
            {
                if (_cmdCloseCommand == null)
                {
                    _cmdCloseCommand = new Utils.RelayCommand(
                        param => this.CloseTab_Execute(param),
                        param => this.CloseTab_CanExecute(param)
                        );
                }
                return _cmdCloseCommand;
            }
        }


        /// <summary>
        /// Release unused COM objects
        /// </summary>
        /// <param name="obj"></param>
        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                GC.Collect();
                Console.WriteLine("mem: " + GC.GetTotalMemory(true).ToString());
            }
        }

        /// <summary>
        /// Called when the command is to be executed. 탭 닫기 
        /// </summary>
        /// <param name="parm">
        /// The TabItem in which the Close-button was clicked.
        /// </param>
        private void CloseTab_Execute(object parm)
        {
            GC.Collect();
            Console.WriteLine("mem1: " + GC.GetTotalMemory(true).ToString());

            TabItem ti = parm as TabItem;
            if (ti != null)
            {
                // 2015.08.21 romee 배선뷰 메모리 지우기 처리  
                try
                {
                    // 전체 그리드 
                    Grid v1 = (Grid) ti.Content;

                    // 메인 리스트뷰
                    ListView v2 = v1.Children[0] as ListView;
                    // 작은 그리드
                    Grid v3 = v1.Children[1] as Grid;
                    // 작은 리스트뷰 
                    ListView v4 = v3.Children[0] as ListView;

                    //v2.Drop -= lv1_Drop;
                    //v2.SelectionChanged -= lv1_SelectionChanged;
                    //v2.MouseDown -= lv1_MouseDown;
                    //v2.MouseUp -= lv1_MouseUp;
                    //v2.MouseMove -= lv1_MouseMove;
                    //v2.MouseLeave -= lv1_MouseLeave;

                    //for (int i = 0; i < v2.Items.Count; i++)
                    //    v2.Items.RemoveAt(0);
                    v2.ItemsSource = null;
                    v2.Items.Clear();

                    v4.ItemsSource = null;
                    v4.Items.Clear();

                    v3.Children.RemoveAt(0);
                    v3.Children.Clear();

                    v1.Children.RemoveAt(0);
                    v1.Children.RemoveAt(0);
                    v1.RowDefinitions.Clear();
                    v1.ColumnDefinitions.Clear();
                    v1.Children.Clear();
                    //ReleaseObject(v1);
                }
                catch(Exception e) 
                {
                    Console.WriteLine("Err : " + e.ToString());
                }

                GC.Collect();
                Console.WriteLine("mem2: " + GC.GetTotalMemory(true).ToString());
                
                string asset_name = (string)ti.Header;
                // 워크셀에서도 삭제
                List<WorkCell> list = cell_list3.Find(e => e[g.CENTER_COL].asset_name == asset_name);
                if (list == null)
                    return;
                int asset_id = list[g.CENTER_COL].asset_id;

                // romee list item 도 삭제 
                for (int i = 0; i < list.Count; i++ )
                    list.RemoveAt(0);
                list.Clear();
                cell_list3.Remove(list);

                // 탭삭제
                _tc.Items.Remove(parm);

                ti.Content = null;  
                //ReleaseObject(ti);

                // 좌측 IPP 리스트에서 체크 표시를 지운다.
                try
                {
                    ipp_list tmp = _ipps.Find(ee => ee.asset_id == asset_id);
                    if (tmp != null)
                    {
                        tmp.checked_color = color_red;
                        tmp.force_changed = true;
                    }
                }
                catch (Exception) { }

                if (_tc.Items.Count == 0)
                {
                    //_tc.AllowDrop = true;
                    //_tc.Drop += _tc_Drop;
                    _txtDesc.Visibility = Visibility.Visible;
                }

                // 열려있는 탭에 대해 패치id를 레지스트리에 저장
                List<int> asset_list = _ipps.Where(p => p.checked_color == Colors.Transparent).Select(p => p.asset_id).ToList();
                Reg.save_link_diagram(asset_list);
            }
        }

        /// <summary>
        /// Called when the availability of the Close command needs to be determined.
        /// </summary>
        /// <param name="parm">
        /// The TabItem for which to determine the availability of the Close-command.
        /// </param>
        private bool CloseTab_CanExecute(object parm)
        {
            //For the sample, the closing of TabItems will only be
            //unavailable for disabled TabItems and the very first TabItem.
            TabItem ti = parm as TabItem;
            //if (ti != null && ti != _tc.Items[0])
            if (ti != null)
                //We have a valid reference to a TabItem, so return 
                //true if the TabItem is enabled.
                return ti.IsEnabled;

            //If no reference to a TabItem could be obtained, the command 
            //cannot be executed
            return false;
        }

        #endregion

        #region //이벤트 처리 루틴
        // 트리 뷰 사용치 않음 
        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int idx = _lvLeft.Items.IndexOf(item);
            _lvLeft.SelectedIndex = idx;

            if (idx >= 0)
            {
                ipp_list ipp = _lvLeft.SelectedItem as ipp_list;
                if (ipp != null)
                    open_sheet(ipp);
            }
        }

        // 빈 화면에 드롭 시...
        private void _gridEmpty_Drop(object sender, DragEventArgs e)
        {
            open_sheet_pre(e);
        }

        private void _tc_Drop(object sender, DragEventArgs e)
        {
            open_sheet_pre(e);
            e.Handled = true;     // empty 그리드로 드롭되지 않도록 막음...
        }

        private void open_sheet_pre(DragEventArgs e)
        {
            AssetTreeVM vm = null;
            int source_asset_id = 0;
            if (e.Data.GetDataPresent("asset_tree_for_treeview"))
                vm = e.Data.GetData("asset_tree_for_treeview") as AssetTreeVM;
            else if (e.Data.GetDataPresent("intelligent_tree_for_treeview"))
                vm = e.Data.GetData("intelligent_tree_for_treeview") as AssetTreeVM;
            else if (e.Data.GetDataPresent("favorite_tree_for_treeview"))
                vm = e.Data.GetData("favorite_tree_for_treeview") as AssetTreeVM;
            else
                return;

            if (vm == null)
                return;

            // 이미 open 되어 있는지...
            source_asset_id = vm.asset_id ?? 0;
            var find = _ipps.Find(p => p.asset_id == source_asset_id);
            if (find == null)
                return;
            if (find.catalog_group_id == 3440) // 일반 패치패널은 배선뷰로 놓을 수 없음 romee 2015.09.09 
                return;
            if (find.checked_color == Colors.Transparent)
                return;
            open_sheet(find);    
        }

        // 선택한 패치에 대해 쉬트만들기
        private void open_sheet(ipp_list ipp)
        {
            // 한장의 쉬트
            List<WorkCell> cell_list2 = new List<WorkCell>();

            List<WorkCell> list;
            try { list = cell_list3.Find(ee => ee[g.CENTER_COL].asset_id == ipp.asset_id); }
            catch (Exception) { return; }
            if (list != null)
                return;

            var a = g.asset_list.Find(p => p.asset_id == ipp.asset_id);
            if (a == null)
                return;
            var c = g.catalog_list.Find(p => p.catalog_id == a.catalog_id);
            int num_of_ports = c != null ? c.num_of_ports : 0;
            if (num_of_ports < 1)
                num_of_ports = 1;

            // 링크다이어그램을 그려라...  (포트 수 x 21만큼 cell_list2에 add됨)
            ld.openAsset(ipp.asset_id, 1, num_of_ports, cell_list2);

            // 위에서 채운 나머지 만큼 cell_list2에 empty cell을 add
            int remain;
            for (remain = num_of_ports; remain < g.MAX_ROW; remain++)
            {
                int col = 0;
                for (col = 0; col < g.MAX_COL; col++)
                {
                    WorkCell empty = new WorkCell();
                    empty.idx = remain * g.MAX_COL + col;   // 인덱스는 0부터~~~ 21개 x 50개 -1까지...
                    empty.col_no = col;                     // 0부터 시작하는 x축 좌표와 같다.
                    empty.template_type = "empty";
                    cell_list2.Add(empty);
                }
            }

            // cell_list3 에 cell_list를 추가한다.
            cell_list3.Add(cell_list2);

            int pos = cell_list3.Count() - 1;

            TabItem ti = new TabItem();
            ti.Header = ipp.asset_name;

            Grid grid1 = new Grid();
            ListView lv1 = new ListView();

            lv1.ItemsSource = cell_list2;
            lv1.ItemsPanel = this.Resources["MyItemsPanelTemplate"] as ItemsPanelTemplate;
            lv1.ItemContainerStyle = Application.Current.Resources["_WorkCellItemContainerStyle"] as Style;
            lv1.Style = Application.Current.Resources["I2MS_ListViewStyle"] as Style;
            lv1.SelectedIndex = g.CENTER_COL;
            lv1.SelectionMode = SelectionMode.Single;
            lv1.ScrollIntoView(cell_list2[g.CENTER_COL + 5]);
            lv1.FocusVisualStyle = null;
            _lv1 = lv1;
            lv1.AllowDrop = true;
            lv1.Drop += lv1_Drop;
            lv1.Background = new SolidColorBrush(Colors.Transparent);
            lv1.SelectionChanged += lv1_SelectionChanged;
            lv1.MouseDown += lv1_MouseDown;
            lv1.MouseUp += lv1_MouseUp;
            lv1.MouseMove += lv1_MouseMove;
            lv1.MouseLeave += lv1_MouseLeave;

            Grid grid2 = new Grid();
            grid2.Width = g.MAX_COL * 9;
            grid2.Height = g.MAX_ROW * 6;
            grid2.HorizontalAlignment = HorizontalAlignment.Left;
            grid2.VerticalAlignment = VerticalAlignment.Bottom;
            grid2.Background = new SolidColorBrush(Colors.Black);
            grid2.Opacity = 0.5;
            grid2.Margin = new Thickness(11, 0, 0, 27);

            // lv2: 미니맵
            ListView lv2 = new ListView();
            lv2.ItemsSource = cell_list2;
            lv2.Name = "lv2";
            lv2.ItemsPanel = this.Resources["MyItemsPanelTemplate"] as ItemsPanelTemplate;
            lv2.ItemContainerStyle = Application.Current.Resources["_lvMiniItemContainerStyle"] as Style;
            lv2.Style = Application.Current.Resources["I2MS_ListViewStyle"] as Style;
            lv2.SelectionMode = SelectionMode.Single;
            grid2.MouseEnter += new MouseEventHandler(grid2_MouseEnter);

            lv2.IsEnabled = false;
            lv2.SelectedIndex = g.CENTER_COL;
            grid2.Children.Add(lv2);
            grid1.Children.Add(lv1);
            grid1.Children.Add(grid2);

            lv1.IsSynchronizedWithCurrentItem = true;
            lv2.IsSynchronizedWithCurrentItem = true;
            grid1.DataContext = cell_list2;

            ti.Content = grid1;

            _tc.Items.Add(ti);

            // 좌측 IPP 리스트에서 체크 표시 한다.
            try
            {
                ipp_list tmp = _ipps.Find(ee => ee.asset_id == ipp.asset_id);
                if (tmp != null)
                {
                    tmp.checked_color = Colors.Transparent;
                    tmp.force_changed = true;
                }
            }
            catch (Exception) { }

            ipp.ti = ti;
            ipp.lv = lv1;
            ipp.list = cell_list2;
            ipp.num_of_ports = num_of_ports;

            _tc.SelectedIndex = pos;

            if (_tc.Items.Count > 0)
                _txtDesc.Visibility = Visibility.Hidden;

            // 오픈된 패치 자산id를 레지스트리에 기억
            List<int> asset_list = _ipps.Where(p => p.checked_color == Colors.Transparent).Select(p => p.asset_id).ToList();
            Reg.save_link_diagram(asset_list);
        }
        // 선택이 변경 되면
        void lv1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WorkCell vm = (WorkCell)_lv1.SelectedItem;
            if (vm != null)
            {
                if (vm.template_type == "asset")
                {
                    g.prop_data.force_clear = true;
                    if (vm.asset_id > 0)
                        g.main_window._ctlRightSide.dispAssetProperty(vm.asset_id);
                    g.main_window._ctlRightSide.dispLocationProperty(vm.location_id);
                    g.prop_data.force_changed = true;
                }
                if (vm.template_type == "cable")
                {
                    g.prop_data.force_clear = true;
                    if (vm.catalog_id > 0)
                        g.main_window._ctlRightSide.dispCatalogProperty(vm.catalog_id);
                    g.prop_data.force_changed = true;
                }
            }
        }
        // 
        void lv1_MouseLeave(object sender, MouseEventArgs e)
        {
            _wheel = false;
        }

        bool _wheel = false;
        Point _wheel_pos = new Point(0, 0);
        Point _wheel_pos2 = new Point(0, 0);
        void lv1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_wheel)
            {
                // 이동 간격 체크
                Point new_point = e.GetPosition(_lv1);
                ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(_lv1) as ScrollViewer;

                double x = scrollViewer.HorizontalOffset;
                double y = scrollViewer.VerticalOffset;

                double diff_x = _wheel_pos.X - new_point.X;
                double diff_y = _wheel_pos.Y - new_point.Y;

                _wheel_pos.X = new_point.X;
                _wheel_pos.Y = new_point.Y;

                _wheel_pos2.X += diff_x;
                _wheel_pos2.Y += diff_y;

                scrollViewer.ScrollToHorizontalOffset(_wheel_pos2.X);
                scrollViewer.ScrollToVerticalOffset(_wheel_pos2.Y);

                e.Handled = true;
            }
        }
        // 자식 찾기 
        public T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        void lv1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _wheel = false;
        }
        // 
        void lv1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
            {
                Point pos = e.GetPosition(_lv1);


                _wheel_pos = new Point(pos.X, pos.Y);

                if (!_wheel)
                {
                    ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(_lv1) as ScrollViewer;
                    double x = scrollViewer.HorizontalOffset;
                    double y = scrollViewer.VerticalOffset;
                    _wheel_pos2.X = x;
                    _wheel_pos2.Y = y;
                    _wheel = true;
                }
            }
        }
        //
        void lv1_Drop(object sender, DragEventArgs e)
        {
            e.Handled = true;     // 탭컨트롤로 드롭되지 않도록 막음...

            AssetTreeVM vm = null;
            int source_asset_id = 0;
            int source_port_no = 0;
            if (e.Data.GetDataPresent("asset_tree_for_treeview"))
                vm = e.Data.GetData("asset_tree_for_treeview") as AssetTreeVM;
            else if (e.Data.GetDataPresent("intelligent_tree_for_treeview"))
                vm = e.Data.GetData("intelligent_tree_for_treeview") as AssetTreeVM;
            else if (e.Data.GetDataPresent("favorite_tree_for_treeview"))
                vm = e.Data.GetData("favorite_tree_for_treeview") as AssetTreeVM;
            else 
                return;
            if (vm == null)
                return;

            source_asset_id = vm.asset_id ?? 0;
            source_port_no = vm.port_no;
            int catalog_id = Etc.get_catalog_id_by_asset_id(source_asset_id);

            // 드롭이 가능한 것은?
            // 컨트롤러, 스위치, 패치패널, Face Plate, MUTOABOX, 서버, 워크스테이션
            if (!CatalogType.is_link_diagram(catalog_id))
                return;

            // 해당 쉬트에 이미 등록된 아이템 이라면 리턴 처리  romee/jake    ?? 왜 등록된것을 또 등록하게 했는지??
            List<WorkCell> cell_list2 = (List<WorkCell>)_lv1.ItemsSource;
            var cell = cell_list2.Find(p => (p.asset_id == source_asset_id) && (p.port_no == source_port_no));
            if (cell != null) // 이미 사용중인 자산입니다. 
                return;

            // Drop 한 곳의 위치를 알아온다.
            ListViewItem dest_item = FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);
            if (dest_item != null)
            {
                var vm2 = (WorkCell)_lv1.ItemContainerGenerator.ItemFromContainer(dest_item);
                int idx = _lv1.Items.IndexOf(vm2);
                if (idx >= 0)
                {
                    _lv1.SelectedIndex = idx;

                    int base_asset_id = _ipp.asset_id;
                    // 기준(센터)에 있는 자산을 드롭하려한 경우
                    if (source_asset_id == base_asset_id)
                        return;

                    int dest_num_of_ports = Etc.get_num_of_ports_by_asset_id(base_asset_id);
                    if ((vm2.idx / g.MAX_COL) >= dest_num_of_ports)
                        return;

                    int cur_col = vm2.col_no;
                    bool ipp_org = CatalogType.is_ipp(catalog_id);
                    bool ifp_org = CatalogType.is_ipp_fp(catalog_id);

                    // 12번째 컬럼에 패치를 드롭한경우
                    if ((cur_col == 12) && ipp_org)
                    {
                        bool ipp_ic_center = CatalogType.is_ipp_ic(_ipp.catalog_id);
                        bool ipp_ic_org = CatalogType.is_ipp_ic(catalog_id);
                        if (ipp_ic_org)
                            return;

                        // Inter vs Cross 타입이 다르면
                        if (ipp_ic_center != ipp_ic_org)
                            return;
                    }
                    else if ((ipp_org && cur_col != 10 && cur_col != 12) || (ifp_org && cur_col != 8 && ifp_org && cur_col != 14)) // 지능형 패치 이면서 10번이나 12번이 아닌경우 
                    {
                        if (ifp_org && cur_col != 8 && ifp_org && cur_col != 14) // 광 지능형 패치 이면서 8번이나 14번이 아닌경우    // 마곡 간선
                        {
                            return;
                        }
                    }
                    else if (ipp_org && cur_col == 10) // 지능형 패치 이면서 10번인 경우  
                    {
                        return;
                    }
                    else if (cur_col == 10) // 10번은 기준 이므로 자신 외에는 아무것도 못옴 romee 2016.01.27  
                    {
                        return;
                    }
                    else if (cur_col == 12 && CatalogType.is_fp(catalog_id)) // 아울렛 이면서 10번인 경우 romee 2016.01.27 
                    {
                        return;
                    }

                    if (source_port_no == 0)
                    {
                        // 셀에 자산 전체 포트를 드롭
                        int i;
                        int num_of_ports = Etc.get_num_of_ports_by_asset_id(source_asset_id);
                        for (i = 0; i < num_of_ports; i++)
                        {
                            int port_no = ld.get_clone_port_no(source_asset_id, source_port_no, cell_list3);
                            int dest_idx = vm2.idx + g.MAX_COL * i;
                            if (dest_idx < _ipp.list.Count)
                            {
                                WorkCell dest = _ipp.list[dest_idx];
                                bool b = ld.addAsset2WorkCell(dest, source_asset_id, port_no, cell_list3, _ipp.list, true); // 사용자 추가 시 true
                                if (!b)
                                    break;  
                            }
                        }
                    }
                    else
                    {
                        // 셀에 자산 한포트를 드롭...
                        ld.addAsset2WorkCell(vm2, source_asset_id, source_port_no, cell_list3, _ipp.list, true);
                    }
                }
            }
        }

        private void grid2_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender.GetType() != typeof(Grid))
                return;

            Grid grid = sender as Grid;

            if (grid.HorizontalAlignment == HorizontalAlignment.Left)
            {
                grid.HorizontalAlignment = HorizontalAlignment.Right;
                grid.Margin = new Thickness(0, 0, 27, 27);
            }
            else
            {
                grid.HorizontalAlignment = HorizontalAlignment.Left;
                grid.Margin = new Thickness(11, 0, 0, 27);
            }
        }

        private void _tc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = _tc.SelectedItem as TabItem;
            if (item == null)
                return;

            string s = item.Header.ToString();

            ipp_list il = _ipps.Find(p => p.asset_name == s);
            if (il == null)
                return;

            _lv1 = il.lv;
            _ipp = il;
        }

        // 알람 테스트용 버튼...
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(1);
        }


        #endregion

        #region // 데이터 베이스 저장 로직등 
        // 케이블을 중심으로 기록한다. (work order는 별도의 메뉴에서 저장하고 여기서는 저장하지 않는다.)
        private async Task<bool> saveData(List<WorkCell> list, int num_of_ports)
        {
            // 먼저 케이블이 연결되어 있는 자산들을 대상으로.... 연결 또는 절단 (인텔리전트 패치 구간은 손대지 않음)
            foreach(var node in list)
            {
                if (node.is_ins_mark && !node.is_wo_mark) 
                {
                    if (node.template_type == "cable")
                        await connectNode(list, node);
                }
                else if (node.is_del_mark && !node.is_wo_mark) 
                {
                    if (node.template_type == "cable")
                        await disconnectNode(list, node);
                }
            }

            // 자산이 홀로 연결되지 않고 있던 자산이 삭제된 경우
            foreach (var node in list)
            {
                if ((node.is_del_mark) && (node.template_type == "asset"))
                {
                    await deleteAssetNode(list, node);
                }
            }

            // 인터커넥터형이 아닌 경우 종료
            if (!CatalogType.is_ipp_ic(list[g.CENTER_COL].catalog_id))
                return true;
            bool write_db = false;

            // 연결 작업지시 마크가 있는 경우 연결 정보를 DB에 기록하고 alarm 동기화 할지 물어본다.
            foreach (var node in list.Where(p => p.col_no == (g.CENTER_COL + 1)))
            {
                if (node.is_ins_mark && node.is_wo_mark)
                {
                    write_db = true;
                    break;
                }
            }

            if (write_db)
            {
                bool b1 = MessageBox.Show(g.tr_get("P5_Save_1"), g.tr_get("P5_Save_2"),
                                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
                if (!b1)
                    return true;
            }

            // 저장
            foreach (var node in list.Where(p => p.col_no == (g.CENTER_COL + 1)))
            {
                WorkCell left = list[node.idx - 1];
                WorkCell right = list[node.idx + 1];
                if (node.is_ins_mark && node.is_wo_mark)
                {
                    // 링크다이어그램 화면도 바꾸고, 실DB도 연결정보 바꾸고, 메모리쪽도 연결정보 바꾼다.
                    await connectNode(list, node);
                    ePortStatus status = Etc.get_port_status_from_ipp_asset_port_link(left.asset_id, left.port_no);
                    if (status != ePortStatus.Linked)
                        // 알람 발생
                        await send_alarm(left.asset_id, left.port_no, 1);
                    else
                        await request_cancel_alarm(left, right);
                }
                else if (node.is_del_mark && node.is_wo_mark)
                {
                    await disconnectNode(list, node);
                    ePortStatus status = Etc.get_port_status_from_ipp_asset_port_link(left.asset_id, left.port_no);
                    if (status != ePortStatus.Unplugged)
                        // 알람 발생
                        await send_alarm(left.asset_id, left.port_no, 2);
                    else
                        await request_cancel_alarm(left, right);
                }
            }

            return true;
        }

        private async Task<bool> send_alarm(int asset_id, int port_no, int alarm_type)
        {
            int sys_id = Etc.get_sys_id_by_ipp_asset_id(asset_id);
            int pp_id = Etc.get_pp_id_by_asset_id(asset_id);
            request re = new request();

            re.Alarm(sys_id, pp_id, port_no, alarm_type);
            var node = (request)await g.webapi.post("request", re, typeof(request));
            if (node == null)
                return false;
            return true;
        }

        private async Task<bool> connectNode(List<WorkCell> list, WorkCell cell)
        {
            var left = list[cell.idx - 1];
            var right = list[cell.idx + 1];

            bool b = await g.left_tree_handler.connect_asset_port_link(left, right, cell);
            if (!b)
                return false;         

            if (left.is_ins_mark)
                left.is_ins_mark = false;
            cell.is_ins_mark = false;
            cell.is_wo_mark = false;
            if (right.is_ins_mark)
                right.is_ins_mark = false;

            left.force_changed = true;
            cell.force_changed = true;
            right.force_changed = true;

            return true;            
        }

        private async Task<bool> disconnectNode(List<WorkCell> list, WorkCell cell)
        {
            var left = list[cell.idx - 1];
            var right = list[cell.idx + 1];

            bool b = await g.left_tree_handler.disconnect_asset_port_link(left, right, cell);
            if (!b)
                return false;

            cell.is_wo_mark = false;
            ld.deleteCableCell(list, cell.idx);
            if (left.is_del_mark)
                ld.deleteAssetCell(list, left.idx);
            if (right.is_del_mark)
                ld.deleteAssetCell(list, right.idx);

            return true;            
        }

        // 자산만 단독으로 삭제하는 경우.... 현재 이 루틴을 호출할 때 좌우 케이블이 없는 경우에 호출되고 있음.
        //                                   다만 루틴은 좌우에 케이블이 있는 경우 케이블도 삭제하게 처리하였음.
        private async Task<bool> deleteAssetNode(List<WorkCell> list, WorkCell center)
        {
            WorkCell left = null;
            WorkCell right = null;
            // 좌.우 케이블 연결이 있는지 확인
            if (center.col_no > 1)
                if (list[center.idx - 1].template_type == "cable")
                    left = list[center.idx - 2];
            if ((center.col_no + 1) < g.MAX_COL)
                if (list[center.idx + 1].template_type == "cable")
                    right = list[center.idx + 2];

            int? left_asset_id = null;
            int? left_port_no = null;
            int center_asset_id = 0;
            int center_port_no = 0;
            int? center_left_cable_catalog_id = null;
            int? center_right_cable_catalog_id = null;
            int? right_asset_id = null;
            int? right_port_no = null;

            if (left != null)
            {
                left_asset_id = left.asset_id;
                left_port_no = left.port_no;
                center_left_cable_catalog_id = list[center.idx - 1].catalog_id;
            }
            center_asset_id = center.asset_id;
            center_port_no = center.port_no;
            if (right != null)
            {
                right_asset_id = right.asset_id;
                right_port_no = right.port_no;
                center_right_cable_catalog_id = list[center.idx + 1].catalog_id;
            }

            asset_port_link left_apl = null;
            asset_port_link center_apl = null;
            asset_port_link right_apl = null;

            if (left != null)
                left_apl = g.asset_port_link_list.Find(p => (p.asset_id == left_asset_id) && (p.port_no == left_port_no));
            center_apl = g.asset_port_link_list.Find(p => (p.asset_id == center_asset_id) && (p.port_no == center_port_no));
            if (center_apl == null)
                return false;
            if (right != null)
                right_apl = g.asset_port_link_list.Find(p => (p.asset_id == right_asset_id) && (p.port_no == right_port_no));

            if (left_apl != null)
            {
                if (left.is_left_front)
                {
                    left_apl.rear_asset_id = null;
                    left_apl.rear_port_no = null;
                    left_apl.rear_cable_catalog_id = null;
                    left_apl.rear_plug_side = null;
                }
                else
                {
                    left_apl.front_asset_id = null;
                    left_apl.front_port_no = null;
                    left_apl.front_cable_catalog_id = null;
                    left_apl.front_plug_side = null;
                }
            }

            center_apl.front_asset_id = null;
            center_apl.front_port_no = null;
            center_apl.front_cable_catalog_id = null;
            center_apl.front_plug_side = null;
            center_apl.rear_asset_id = null;
            center_apl.rear_port_no = null;
            center_apl.rear_cable_catalog_id = null;
            center_apl.rear_plug_side = null;

            if (right_apl != null)
            {
                if (right.is_left_front)
                {
                    right_apl.front_asset_id = null;
                    right_apl.front_port_no = null;
                    right_apl.front_cable_catalog_id = null;
                    right_apl.front_plug_side = null;
                }
                else
                {
                    right_apl.rear_asset_id = null;
                    right_apl.rear_port_no = null;
                    right_apl.rear_cable_catalog_id = null;
                    right_apl.rear_plug_side = null;
                }
            }

            int r1 = 0;
            int r2 = 0;
            int r3 = 0;

            if (left_apl != null)
                r1 = await g.webapi.put("asset_port_link", left_apl.asset_port_link_id, left_apl, typeof(asset_port_link));

            r2 = await g.webapi.put("asset_port_link", center_apl.asset_port_link_id, center_apl, typeof(asset_port_link));

            if (right_apl != null)
                r3 = await g.webapi.put("asset_port_link", right_apl.asset_port_link_id, right_apl, typeof(asset_port_link));

            if ((r1 != 0) || (r2 != 0) || (r3 != 0))
            {
                if (left.is_left_front)
                {
                    left_apl.rear_asset_id = center_asset_id;
                    left_apl.rear_port_no = center_port_no;
                    left_apl.rear_cable_catalog_id = center_left_cable_catalog_id;
                    left_apl.rear_plug_side = center.is_left_front ? "F" : "R";
                }
                else
                {
                    left_apl.front_asset_id = center_asset_id;
                    left_apl.front_port_no = center_port_no;
                    left_apl.front_cable_catalog_id = center_left_cable_catalog_id;
                    left_apl.front_plug_side = center.is_left_front ? "F" : "R";
                }

                if (right.is_left_front)
                {
                    right_apl.front_asset_id = center_asset_id;
                    right_apl.front_port_no = center_port_no;
                    right_apl.front_cable_catalog_id = center_right_cable_catalog_id;
                    right_apl.front_plug_side = center.is_left_front ? "R" : "F";
                }
                else
                {
                    right_apl.rear_asset_id = center_asset_id;
                    right_apl.rear_port_no = center_port_no;
                    right_apl.rear_cable_catalog_id = center_right_cable_catalog_id;
                    right_apl.rear_plug_side = center.is_left_front ? "R" : "F";
                }

                if (left_apl != null)
                {
                    if (center.is_left_front)
                    {
                        center_apl.front_asset_id = left_asset_id;
                        center_apl.front_port_no = left_port_no;
                        center_apl.front_cable_catalog_id = left.is_left_front ? left.rear_cable_catalog_id : left.front_cable_catalog_id;
                        center_apl.front_plug_side = left.is_left_front ? "R" : "F";
                    }
                    else
                    {
                        center_apl.rear_asset_id = left_asset_id;
                        center_apl.rear_port_no = left_port_no;
                        center_apl.rear_cable_catalog_id = left.is_left_front ? left.rear_cable_catalog_id : left.front_cable_catalog_id;
                        center_apl.rear_plug_side = left.is_left_front ? "R" : "F";
                    }
                }

                if (right_apl != null)
                {
                    if (center.is_left_front)
                    {
                        center_apl.rear_asset_id = right_asset_id;
                        center_apl.rear_port_no = right_port_no;
                        center_apl.rear_cable_catalog_id = right.is_left_front ? right.front_cable_catalog_id : right.rear_cable_catalog_id;
                        center_apl.rear_plug_side = right.is_left_front ? "F" : "R";
                    }
                    else
                    {
                        center_apl.front_asset_id = right_asset_id;
                        center_apl.front_port_no = right_port_no;
                        center_apl.front_cable_catalog_id = right.is_left_front ? right.front_cable_catalog_id : right.rear_cable_catalog_id;
                        center_apl.front_plug_side = right.is_left_front ? "F" : "R";
                    }
                }

                // 에러가 나든 말든 강제로 writing
                if (left_apl != null)
                    await g.webapi.put("asset_port_link", left_apl.asset_port_link_id, left_apl, typeof(asset_port_link));

                await g.webapi.put("asset_port_link", center_apl.asset_port_link_id, center_apl, typeof(asset_port_link));

                if (right_apl != null)
                    await g.webapi.put("asset_port_link", right_apl.asset_port_link_id, right_apl, typeof(asset_port_link));
                return false;
            }

            if (left != null)
                ld.deleteCableCell(list, center.idx - 1);
            if (right != null)
                ld.deleteCableCell(list, center.idx + 1);
            ld.deleteAssetCell(list, center.idx);

            return true;
        }

        // 워크오더 실행 
        private async Task<bool> processWorkOrder(int wo_id, bool reserve_flag, DateTime reserved_date, int smartphone)
        {
            int task_sn = 0;

            var iic = g.ic_ipp_config_list.Find(p => p.ipp_asset_id == _ipp.asset_id);
            if (iic == null)
                return false;

            // int remote_ic_asset_id = iic.ic_asset_id;

            foreach (var vm in _ipp.list)
            {
                if (vm.is_wo_mark)
                {
                    task_sn++;
                    WorkCell left = _ipp.list[vm.idx - 1];
                    WorkCell right = _ipp.list[vm.idx + 1];
                    work_order_task wot = new work_order_task()
                    {
                        command_type = vm.is_ins_mark ? "P" : "U",
                        port_no = left.port_no,
                        remote_asset_id = right.asset_id,
                        remote_asset_type = CatalogType.is_ipp(right.catalog_id) ? "P" : "S",  // 패치 패널인지 스위치인지...
                        remote_ic_asset_id = Etc.get_ic_asset_id_by_ipp_asset_id(right.asset_id),
                        remote_port_no = right.port_no,
                        task_no = task_sn,
                        task_result = "-",
                        wo_id = wo_id,
                        write_time = DateTime.Now
#if I2MS_V21 // 스마트폰 작업 지시 
                        ,smartphone = smartphone
#endif
                    };

                    var r = (work_order_task)await g.webapi.post("work_order_task", wot, typeof(work_order_task));
                    if (r == null)
                    {
                        MessageBox.Show(g.tr_get("C_Error_6"));
                        return false;
                    }
                    g.work_order_task_list.Add(r);
                }
            }

            // work_order_progress 는 생략...
            return true;
        }


        #endregion

        #region // 시그날 알 처리 
        public void update_sw_port_status(int sw_asset_id, int sw_port_no, ePortStatus status)
        {
            //ld.update_sw_port_status(sw_asset_id, sw_port_no, status, cell_list3);
        }

        public void update_link_info(int ipp_asset_id, int port_no, int remote_ipp_asset_id, int remote_port_no, ePortStatus status)
        {
            // 연결
            if (((status == ePortStatus.Linked) || (status == ePortStatus.Plugged)) && (remote_ipp_asset_id > 0))
            {
                ld.add_link_info_asset_to_multi_screen(ipp_asset_id, port_no, remote_ipp_asset_id, remote_port_no, status, cell_list3);
                //ld.add_link_info_asset_to_multi_screen(remote_ipp_asset_id, remote_port_no, ipp_asset_id, port_no, status, cell_list3);
            }
            // 연결 삭제
            if ((status == ePortStatus.Unplugged) || ((status == ePortStatus.Plugged) && (remote_ipp_asset_id == 0)))
            {
                ld.del_link_info_asset_to_multi_screen(ipp_asset_id, port_no, status, cell_list3);
                //ld.del_link_info_asset_to_multi_screen(remote_ipp_asset_id, remote_port_no, status, cell_list3);
            }
            ld.update_port_status(ipp_asset_id, port_no, status, cell_list3);
        }

        public void update_link_info_ic(int ipp_asset_id, int port_no, int sw_asset_id, int sw_port_no, ePortStatus status)
        {
            // 연결
            if (status == ePortStatus.Linked) 
            {
                ld.add_link_info_asset_to_multi_screen(ipp_asset_id, port_no, sw_asset_id, sw_port_no, status, cell_list3);
            }
            // 연결 삭제
            if (status == ePortStatus.Unplugged) 
            {
                ld.del_link_info_asset_to_multi_screen(ipp_asset_id, port_no, status, cell_list3);
            }
            ld.update_port_status(ipp_asset_id, port_no, status, cell_list3);
        }

        // 화면의 포트 상태 변경  -> 시그날알에서만 호출됨 
        public void update_port_status(int ipp_asset_id, int port_no, int remote_ipp_asset_id, int remote_port_no, ePortStatus status)
        {
            // 워크오더 포트에 상태가 변경이 된 경우 포트 상태만 바꾼다.
            if (!ld.is_wo_port(ipp_asset_id, port_no, cell_list3))
            {
                // 연결 -> 플러그나 링크이면서 리모트가 있어야 수행 처리  
                if (((status == ePortStatus.Plugged) && (remote_ipp_asset_id > 0)) || (status == ePortStatus.Linked))
                {
                    // 패치 조작으로 변경된 경우 이미 디비에 있으므로 인서트 마크 필요 없음 
                    ld.add_connected_asset_to_multi_screen(ipp_asset_id, port_no, remote_ipp_asset_id, remote_port_no, status, cell_list3, false);  // false=사용자가 직접 추가한 것이 아니면...
                    // ld.add_connected_asset_to_multi_screen_ex(ipp_asset_id, port_no, remote_ipp_asset_id, remote_port_no, status, cell_list3, false); // romee/jake 
                }
                // 연결 삭제 -> 언 플러그 인경우 
                //if ((status == ePortStatus.Unplugged) || ((status == ePortStatus.Plugged) && (remote_ipp_asset_id == 0)))
                if (status == ePortStatus.Unplugged) 
                {
                    ld.del_connected_asset_to_multi_screen(ipp_asset_id, port_no, status, cell_list3);
                }
            }
            // 화면 갱신 
            ld.update_port_status(ipp_asset_id, port_no, status, cell_list3);
        }
        // 터미날 변경 처리 romee 2015.11.03 터미날 변경시 화면 업데이트 처리  - 시험 필요 
        public void update_terminal_linemanager(int old_outlet_asset_id, int old_outlet_port_no, int outlet_asset_id, int outlet_port_no, int terminal_asset_id)
        {
            // 기존 지우기
            ld.update_work_cell(old_outlet_asset_id, old_outlet_port_no, terminal_asset_id, 0, cell_list3);
            // 새로 만들기 
            ld.update_work_cell(outlet_asset_id, outlet_port_no, terminal_asset_id, 1, cell_list3);
        }
        // 알람 상태 
        public void update_alarm_status(int ipp_asset_id, int port_no, string status)
        {
            ld.update_alarm_status(ipp_asset_id, port_no, status, cell_list3);
        }
        // 웍크오더 상태 
        public void update_wo_status(int ipp_asset_id, int port_no, string status)
        {
            ld.update_wo_status(ipp_asset_id, port_no, status, cell_list3);
        }
        // 포트 트레이스 
        public void update_trace_status(int ipp_asset_id, int port_no, string status)
        {
            ld.update_trace_status(ipp_asset_id, port_no, status, cell_list3);
        }
        // 지능형 패치 추가 
        public void add_ipp_asset(int ipp_asset_id)
        {
            asset a = Etc.get_asset(ipp_asset_id);
            if (a == null)
                return;
            int catalog_id = a.catalog_id;
            catalog c = Etc.get_catalog(catalog_id);
            if (c == null)
                return;
            location lo = g.location_list.Find(p => p.location_id == a.location_id);
            if (lo == null)
                return;
            if (!CatalogType.is_ipp(catalog_id))
                return;

            var node = new ipp_list();
            node.asset_id = ipp_asset_id;
            node.asset_name = a.asset_name;
            node.catalog_id = a.catalog_id;
            node.catalog_group_id = c.catalog_group_id;
            node.location_path = lo.location_path;
            var f = g.floor_list.Find(p => p.floor_id == lo.floor_id);
            node.floor_name = f != null ? f.floor_name : "";
            var r = g.room_list.Find(p => p.room_id == lo.room_id);
            node.room_name = r != null ? r.room_name : "";
            node.checked_color = color_red;

            _ipps.Add(node);
        }
        // 지능형 삭제 처리 
        public void remove_ipp_asset(int ipp_asset_id)
        {
            _ipps.RemoveAll(p => p.asset_id == ipp_asset_id);
        }
        // 패치 변경 처리 
        public void edit_ipp_asset(int ipp_asset_id, string asset_name)
        {
            var node = _ipps.Find(p => p.asset_id == ipp_asset_id);
            if (node != null)
            {
                node.asset_name = asset_name;
                node.force_changed = true;
            }
        }

        // 워크오더 시작 또는 취소 시 오는 이벤트쪽에서 갱신 요청
        public void update_button_status()
        {
            CommandManager.InvalidateRequerySuggested();            
        }
#endregion

        // Helper to search up the VisualTree
        private static T FindAnchestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        #region // 미사용
        public void clear_all_status_for_all_sheets(int asset_id, int port_no, List<List<WorkCell>> cell_list3)
        {
            foreach (var cell_list2 in cell_list3)
            {
                var cell = cell_list2.Find(p => (p.asset_id == asset_id) && (p.port_no == port_no));
                if (cell != null)
                {
                    WorkCell left = null;
                    WorkCell right = null;
                    WorkCell cable = null;
                    int idx = cell.idx;
                    if (cell.col_no == g.CENTER_COL)
                    {
                        left = cell;
                        cable = cell_list2[idx + 1];
                        right = cell_list2[idx + 2];
                    }
                    else if (cell.col_no == (g.CENTER_COL + 2))
                    {
                        left = cell_list2[idx - 2];
                        cable = cell_list2[idx - 1];
                        right = cell;
                    }
                    else
                        continue;

                    left.is_ins_mark = false;
                    cable.is_ins_mark = false;
                    right.is_ins_mark = false;

                    left.is_del_mark = false;
                    cable.is_del_mark = false;
                    right.is_del_mark = false;

                    left.is_wo_mark = false;
                    cable.is_wo_mark = false;
                    right.is_wo_mark = false;

                    left.alarm_status = eAlarmStatus.None;
                    cable.alarm_status = eAlarmStatus.None;
                    right.alarm_status = eAlarmStatus.None;

                    left.wo_status = eWorkStatus.None;
                    cable.wo_status = eWorkStatus.None;
                    right.wo_status = eWorkStatus.None;

                    left.trace_status = eTraceStatus.Disabled;
                    cable.trace_status = eTraceStatus.Disabled;
                    right.trace_status = eTraceStatus.Disabled;

                    ld.drawConnection(cell_list2, left.idx, cable.catalog_id);
                }
            }

        }
        #endregion 

    }
    // UI 노드를 따라가며 자식 노드 아이템 찾기 
    public static class UIChildFinder
    {
        public static DependencyObject FindChild(this DependencyObject reference, string childName, Type childType)
        {
            DependencyObject foundChild = null;
            if (reference != null)
            {
                int childrenCount = VisualTreeHelper.GetChildrenCount(reference);
                for (int i = 0; i < childrenCount; i++)
                {
                    var child = VisualTreeHelper.GetChild(reference, i);
                    // If the child is not of the request child type child
                    if (child.GetType() != childType)
                    {
                        // recursively drill down the tree
                        foundChild = FindChild(child, childName, childType);
                    }
                    else if (!string.IsNullOrEmpty(childName))
                    {
                        var frameworkElement = child as FrameworkElement;
                        // If the child's name is set for search
                        if (frameworkElement != null && frameworkElement.Name == childName)
                        {
                            // if the child's name is of the request name
                            foundChild = child;
                            break;
                        }
                    }
                    else
                    {
                        // child element found.
                        foundChild = child;
                        break;
                    }
                }
            }
            return foundChild;
        }
    }

}
