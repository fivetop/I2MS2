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
using System.Windows.Threading;

using I2MS2.Models;
using I2MS2.Library;
using WebApi.Models;
using I2MS2.UserControls;
using I2MS2.UserControls.Drawing;

#pragma warning disable 4014

namespace I2MS2.Windows
{

    public partial class view_rack
    {
        public int rack_id { get; set; }
        public eb_port_data_cur eb1 { get; set; }
        public site_environment si1 { get; set; }
    }

    /// <summary>
    /// IPMFloorView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class IPMFloorView : Window
    {
        List<view_rack> view_rack_list;                     // 랙 처리 
        List<eb_port_data_cur> eb_port_data_cur_list;       // 현재
        List<site_environment> site_environment_list;
        List<int> asset_id_list = new List<int>();
        int[] device_stat = new int[5];
        AssetTreeVM use_fl_vm;
        int floor_id = 0;
        int select_view = 0; // 0:전력 1: 온도 2: 습도 3: 도어  

        public IPMFloorView(int id)
        {
            InitializeComponent();
            floor_id = id;

            _grid1.Visibility = System.Windows.Visibility.Visible;
            _grid2.Visibility = System.Windows.Visibility.Hidden;

            dispRack(floor_id);
        }

        private async Task<bool> getDBList()
        {
            await initData();
            return true;
        }

        private async Task<bool> initData()
        {
            _power1.Text = "0 Kwh";
            _door1.Text = "0 EA";
            _temperture1.Text = "0 ºC";
            _humidity1.Text = "0 %";

            string filter = "";
//            var v2 = (List<eb_port_data_cur>)await g.webapi.getList("eb_port_data_cur", typeof(List<eb_port_data_cur>), filter);
            var v2 = (List<eb_port_data_cur>) g.eb_port_data_cur_list;
            if (v2 == null) return false;

            int[] t2 = asset_id_list.ToArray();

            var tdb2 = from a in v2
                       where t2.Contains(a.asset_id) && (a.power_ph != null || a.sensor_t != null ||  a.sensor_h != null || a.door != null )
                       orderby a.asset_id
                       select new eb_port_data_cur()
                       {
                           asset_id = a.asset_id,
                           port_no = a.port_no,
                           power_v = a.power_v,
                           power_i = a.power_i,
                           power_p = a.power_p,
                           power_ph = a.power_ph,
                           sensor_t = a.sensor_t,
                           sensor_h = a.sensor_h,
                           door = a.door,
                           last_updated = a.last_updated,
                       };

            var list1 = tdb2.ToList();

            filter = "";
            site_environment_list = (List<site_environment>)await g.webapi.getList("site_environment", typeof(List<site_environment>), filter);
            if (site_environment_list == null) return false;

            eb_port_data_cur_list = new List<eb_port_data_cur>();
            view_rack_list = new List<view_rack>();

            for (int i = 0; i < list1.Count(); i = i + 3)
            {
                view_rack view1 = new view_rack();
                eb_port_data_cur e1 = list1[i];
                eb_port_data_cur e2 = list1[i+1];
                eb_port_data_cur e3 = list1[i+2];
                e1.power_ph = e1.power_ph;
                e1.sensor_t = e2.sensor_t;
                e1.sensor_h = e2.sensor_h;
                e1.door = e3.door;

                int location_id = Etc.get_location_id_by_asset_id(e1.asset_id);
                if (location_id == 0) break;
                int rack_id = Etc.get_rack_id_by_location_id(location_id);
                if (rack_id == 0) break;

                view1.rack_id = rack_id;
                view1.eb1 = e1;
                view1.si1 = site_environment_list.First();
                view_rack_list.Add(view1);
                eb_port_data_cur_list.Add(e1);
            }


            // 전력  현재
            double d1 = get_double(eb_port_data_cur_list.Max(p => p.power_ph));
            _power1.Text = d1.ToString("N1") + " Kwh";

            // 도어 열린 갯수 
            _door1.Text = eb_port_data_cur_list.Max(p => p.door).ToString() + " EA";

            // 온도 현재
            int tmp1 = eb_port_data_cur_list.Max(p => p.sensor_t) ?? 0;
            int tmp3 = 0;
            if (tmp1 != 0) tmp3 = tmp1 / 10;
            _temperture1.Text = tmp3.ToString() + " ºC";

            // 습도 현재 
            tmp1 = eb_port_data_cur_list.Max(p => p.sensor_h) ?? 0;
            tmp3 = 0;
            if (tmp1 != 0) tmp3 = tmp1 / 10;
            _humidity1.Text = tmp3.ToString() + " %";

            // 우측 전력 범례 처리
            double l1 = get_double(site_environment_list.First().high_power_color);
            double l2 = get_double(site_environment_list.First().low_power_color);

            _t1.Text = l1.ToString("N1") + " Kwh ↑";
            _t2.Text = l2.ToString("N1") + " ~ " + l1.ToString("N1") + " Kwh";
            _t3.Text = l2.ToString("N1") + " Kwh ↓";
            
            return true;
        }

        // int to double 
        private double get_double(int? nullable)
        {
            int int1 = nullable ?? 0;
            double double1 = 0;
            if (int1 != 0)
                double1 = int1 / 10;   // 소숫점 처리용 
            return double1;
        }

        private void dispRack(int location_id)
        {
            IEnumerable<location> list = null;

            int floor_id2 = location_id;
            list = g.location_list.Where(p => p.floor_id == floor_id2);
            asset_id_list = Stat.get_tot_eb_by_floor_id(floor_id2, out device_stat[0], out device_stat[1], out device_stat[2], out device_stat[3], out device_stat[4]);

            _exP61.Header = list.First().location_path + " / Enviroment Device Current information";
        }

        private DispatcherTimer _timer = new DispatcherTimer();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // 사이트 , 빌딩 , 층 , 도면 정보를 가져오기 
            site st = g.site_list.Find(at => at.site_id == g.select_site.site_id);
            if (st == null) return;
            var f = g.floor_list.Find(p => p.floor_id == floor_id);
            if (f == null) return;
            location l_st = g.location_list.Find(at => (at.site_id == st.site_id) && (at.building_id == f.building_id) && (at.floor_id == f.floor_id));
            if (l_st == null) return;

            drawing_3d dr = g.drawing_3d_list.Find(at => at.drawing_3d_id == f.drawing_3d_id);
            string file_path = string.Format("{0}drawing_3d/{1}", g.CLIENT_IMAGE_PATH, dr.file_name);

            // 화면에 뿌려주기 
            _ctlDrawingView3D.openDrawingFile(file_path);

            //현제까지 적용된 UI를 강재로 Action 시킨다
//            Dispatcher.Invoke(new Action(() => { }), DispatcherPriority.ContextIdle, null);

            // 해당층의 트리정보 가져오기 
            use_fl_vm = g.location_ast_vm_dic[l_st.location_id];
            if (use_fl_vm == null) return;

            // 해당층의 노드 보여주기 
            drawFloorAllObject(use_fl_vm);
            _ckbxRoomInfoShow.IsChecked = true; // 룸정보만 보이기 체크 처리 

            // 도면에서 이벤트 받기 처리 ??
            _ctlDrawingView3D.camMoveEndEventToAssetView += new DrawingView3D.camMoveEndHandler(cameraFocusMoveComplated);

            // 탑 그리드 처리 하기 
            _exP61.Header = l_st.location_path;

            _timer.Interval = System.TimeSpan.FromMilliseconds(1000);
            _timer.Tick += new EventHandler(TimerEvent);
            _timer.Start();

        }

        private async void TimerEvent(object sender, System.EventArgs e)
        {
            _timer.Stop();
            Rectangle_MouseEnter1(null, null);

        }

        #region AssetView_Room
        // 트리에 있는 모든 노드를 출력 - 랙만 처리 
        private void drawFloorAllObject(AssetTreeVM fl_vm)
        {
            SolidColorBrush _red = new SolidColorBrush();
            SolidColorBrush _blue = new SolidColorBrush();
            SolidColorBrush _yellow = new SolidColorBrush();

            _blue = App.Current.Resources["_brushBlue"] as SolidColorBrush;
            _red = App.Current.Resources["_brushRed"] as SolidColorBrush;
            _yellow = App.Current.Resources["_brushYellow"] as SolidColorBrush;

            foreach (var rm_vm in fl_vm.child_list)  // 층을 가져오기 
            {
                foreach (var ast_vm in rm_vm.child_list) // 룸을 가져오기 
                {
                    switch (ast_vm.type) // 랙만 처리 
                    {
                        case AssetTreeType.Rack:
                            rack rk = g.rack_list.Find(at => at.rack_id == ast_vm.type_id);
                            if ((rk != null) && (rk.pos_x != null)) // && is_eb() )
                                _ctlDrawingView3D.addRack(rk);
                            _ctlDrawingView3D.changeRackBrush(rk.rack_id, _blue);

                            if (view_rack_list == null) continue;
                            view_rack view1 = view_rack_list.Find(p => p.rack_id == rk.rack_id);
                            switch (select_view)
                            {
                                case 0: // 전력 
                                    //해당 rack의 색상을 변경한다
                                    if (view1.eb1.power_ph >= view1.si1.high_power_color)
                                        _ctlDrawingView3D.changeRackBrush(rk.rack_id, _red);
                                    else
                                        _ctlDrawingView3D.changeRackBrush(rk.rack_id, _blue);
                                    break;
                                case 1: // 온도 
                                    if (view1.eb1.sensor_t >= view1.si1.high_temp_color)
                                        _ctlDrawingView3D.changeRackBrush(rk.rack_id, _red);
                                    else if (view1.eb1.sensor_t <= view1.si1.low_temp_color)
                                        _ctlDrawingView3D.changeRackBrush(rk.rack_id, _yellow);
                                    else
                                        _ctlDrawingView3D.changeRackBrush(rk.rack_id, _blue);
                                    break;
                                case 2: // 습도 
                                    if (view1.eb1.sensor_h >= view1.si1.high_humi_color)
                                        _ctlDrawingView3D.changeRackBrush(rk.rack_id, _red);
                                    else if (view1.eb1.sensor_h <= view1.si1.low_humi_color)
                                        _ctlDrawingView3D.changeRackBrush(rk.rack_id, _yellow);
                                    else
                                        _ctlDrawingView3D.changeRackBrush(rk.rack_id, _blue);
                                    break;
                                case 3: // 도어  
                                    _ctlDrawingView3D.changeRackBrush(rk.rack_id, _blue);
                                    break;
                                default:
                                    _ctlDrawingView3D.changeRackBrush(rk.rack_id, _blue);
                                    break;
                            }
                            break;
                    }
                }
            }
        }

        // 트리에 있는 모든 노드를 출력 - 랙만 처리 
        private void drawFloorRack(AssetTreeVM fl_vm)
        {
            SolidColorBrush _red = new SolidColorBrush();
            SolidColorBrush _blue = new SolidColorBrush();
            SolidColorBrush _yellow = new SolidColorBrush();

            _blue = App.Current.Resources["_brushBlue"] as SolidColorBrush;
            _red = App.Current.Resources["_brushRed"] as SolidColorBrush;
            _yellow = App.Current.Resources["_brushYellow"] as SolidColorBrush;

            foreach (var rm_vm in fl_vm.child_list)  // 층을 가져오기 
            {
                foreach (var ast_vm in rm_vm.child_list) // 룸을 가져오기 
                {
                    switch (ast_vm.type) // 랙만 처리 
                    {
                        case AssetTreeType.Rack:
                            rack rk = g.rack_list.Find(at => at.rack_id == ast_vm.type_id);
                            //if ((rk != null) && (rk.pos_x != null)) // && is_eb() )
                            //    _ctlDrawingView3D.addRack(rk);
                            //_ctlDrawingView3D.changeRackBrush(rk.rack_id, _blue);

                            if (view_rack_list == null) continue;
                            view_rack view1 = view_rack_list.Find(p => p.rack_id == rk.rack_id);
                            if (view1 == null)
                            {
                                _ctlDrawingView3D.changeRackBrush(rk.rack_id, _blue);
                                continue;
                            }
                            switch (select_view)
                            {
                                case 0: // 전력 
                                    //해당 rack의 색상을 변경한다
                                    if (view1.eb1.power_ph >= view1.si1.high_power_color)
                                        _ctlDrawingView3D.changeRackBrush(view1.rack_id, _red);
                                    else
                                        _ctlDrawingView3D.changeRackBrush(rk.rack_id, _blue);
                                    break;
                                case 1: // 온도 
                                    if (view1.eb1.sensor_t >= view1.si1.high_temp_color)
                                        _ctlDrawingView3D.changeRackBrush(rk.rack_id, _red);
                                    else if (view1.eb1.sensor_t <= view1.si1.low_temp_color)
                                        _ctlDrawingView3D.changeRackBrush(rk.rack_id, _yellow);
                                    else
                                        _ctlDrawingView3D.changeRackBrush(rk.rack_id, _blue);
                                    break;
                                case 2: // 습도 
                                    if (view1.eb1.sensor_h >= view1.si1.high_humi_color)
                                        _ctlDrawingView3D.changeRackBrush(rk.rack_id, _red);
                                    else if (view1.eb1.sensor_h <= view1.si1.low_humi_color)
                                        _ctlDrawingView3D.changeRackBrush(rk.rack_id, _yellow);
                                    else
                                        _ctlDrawingView3D.changeRackBrush(rk.rack_id, _blue);
                                    break;
                                case 3: // 도어  
                                    if (view1.eb1.door > 0)
                                        _ctlDrawingView3D.changeRackBrush(rk.rack_id, _red);
                                    else
                                        _ctlDrawingView3D.changeRackBrush(rk.rack_id, _blue);
                                    break;
                                default:
                                    _ctlDrawingView3D.changeRackBrush(rk.rack_id, _blue);
                                    break;
                            }
                            break;
                    }
                }
            }
        }

        // 3D floor wall 구성이 종료되면 호출됨.
        public void cameraFocusMoveComplated(object sender)
        {
            _ctlDrawingView3D.clearRoomInfoAll();
            _ctlDrawingView3D.clearRackInfoAll();
            if (_ckbxRoomInfoShow.IsChecked ?? false)
                drawRoomInfo();
            if (_ckbxRackAssetInfoShow.IsChecked ?? false)
                drawRackInfo();
        }
        #endregion


        #region 룸 / 랙 인포메이션 보기 체크박스 처리 
        private void _ckbxRoomInfoShow_Checked(object sender, RoutedEventArgs e)
        {
            drawRoomInfo();
        }
        private void _ckbxRoomInfoShow_Unchecked(object sender, RoutedEventArgs e)
        {
            clearRoomInfo();
        }
        private void _ckbxRackAssetInfoShow_Checked(object sender, RoutedEventArgs e)
        {
            drawRackInfo();
        }
        private void _ckbxRackAssetInfoShow_Unchecked(object sender, RoutedEventArgs e)
        {
            clearRackInfo();
        }
        #endregion


        #region 2D Infomation Show & hide

        private void drawRoomInfo()
        {
            if (use_fl_vm == null) return;

            List<room> rm_list = g.room_list.FindAll(at => at.floor_id == use_fl_vm.type_id);
            foreach (var rm in rm_list)
            {
                if ((rm.square_x1 != null) && (rm.flag_x != null))
                    _ctlDrawingView3D.drawRoomInfo(rm);
            }
        }

        private void clearRoomInfo()
        {
            _ctlDrawingView3D.clearRoomInfoAll();
        }

        private void drawRackInfo()
        {
            if (use_fl_vm == null) return;

            List<room> rm_list = g.room_list.FindAll(at => at.floor_id == use_fl_vm.type_id);
            foreach (var rm in rm_list)
            {
                List<rack> rk_list = g.rack_list.FindAll(at => at.room_id == rm.room_id);
                foreach (var rk in rk_list)
                {
                    if (rk.pos_x != null)
                        _ctlDrawingView3D.drawRackInfo(rk);
                }
            }
        }

        private void clearRackInfo()
        {
            _ctlDrawingView3D.clearRackInfoAll();
        }


        #endregion

        // 범례 변경 처리 

        private async void Rectangle_MouseEnter1(object sender, MouseEventArgs e)
        {
            _grid1.Visibility = System.Windows.Visibility.Visible;
            _grid2.Visibility = System.Windows.Visibility.Hidden;

            await getDBList();

            var t1 = site_environment_list.Find(p => p.site_id == g.selected_site_id);
            if (t1 == null) return;

            double d1 = get_double(t1.high_power_color);
            double d2 = get_double(t1.low_power_color);

            _t1.Text = d1.ToString("N1") + " Kwh ↑";
            _t2.Text = d2.ToString("N1") + " ~ " + d1.ToString("N1") + " Kwh";
            _t3.Text = d2.ToString("N1") + " Kwh ↓";
            select_view = 0;
            drawFloorRack(use_fl_vm);
            return;
        }
        private async void Rectangle_MouseEnter2(object sender, MouseEventArgs e)
        {
            _grid1.Visibility = System.Windows.Visibility.Visible;
            _grid2.Visibility = System.Windows.Visibility.Hidden;

            await getDBList();

            var t1 = site_environment_list.Find(p => p.site_id == g.selected_site_id);
            if (t1 == null) return;

            int d1 = (int)get_double(t1.high_temp_color);
            int d2 = (int)get_double(t1.low_temp_color);

            _t1.Text = d1.ToString() + " ºC ↑";
            _t2.Text = d2.ToString() + " ~ " + d1.ToString() + " ºC";
            _t3.Text = d2.ToString() + " ºC ↓";
            select_view = 1;
            drawFloorRack(use_fl_vm);
            return;
        }
        private async void Rectangle_MouseEnter3(object sender, MouseEventArgs e)
        {
            _grid1.Visibility = System.Windows.Visibility.Visible;
            _grid2.Visibility = System.Windows.Visibility.Hidden;

            await getDBList();

            var t1 = site_environment_list.Find(p => p.site_id == g.selected_site_id);
            if (t1 == null) return;

            int d1 = (int)get_double(t1.high_humi_color);
            int d2 = (int)get_double(t1.low_humi_color);

            _t1.Text = d1.ToString() + " % ↑";
            _t2.Text = d2.ToString() + " ~ " + d1.ToString() + " %";
            _t3.Text = d2.ToString() + " % ↓";
            select_view = 2;
            drawFloorRack(use_fl_vm);
            return;
        }
        private async void Rectangle_MouseEnter4(object sender, MouseEventArgs e)
        {
            _grid1.Visibility = System.Windows.Visibility.Hidden;
            _grid2.Visibility = System.Windows.Visibility.Visible;

            await getDBList();
            
            select_view = 3;
            drawFloorRack(use_fl_vm);
            return;
        }

    }
}
