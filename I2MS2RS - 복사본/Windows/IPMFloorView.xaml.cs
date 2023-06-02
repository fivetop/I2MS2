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
using MahApps.Metro.Controls;

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
    public partial class IPMFloorView : MetroWindow
    {
        public int alive = 0; // 외부 사용 
        List<view_rack> view_rack_list;                     // 랙 처리 
        List<eb_port_data_cur> eb_port_data_cur_list;       // 현재
        List<site_environment> site_environment_list;
        List<int> asset_id_list = new List<int>();
        int[] device_stat = new int[5];
        AssetTreeVM use_fl_vm;
        int floor_id = 0;
        int select_view = 3; // 0:전력 1: 온도 2: 습도 3: 도어  
        int select_rack = 0; // 선택된 랙  

        SolidColorBrush _red = new SolidColorBrush();
        SolidColorBrush _blue = new SolidColorBrush();
        SolidColorBrush _yellow = new SolidColorBrush();

        #region // 초기화 처리 
        public IPMFloorView(int id)
        {
            InitializeComponent();
            floor_id = id;
            alive = 1;

            g._ratio = 2;
            _blue = App.Current.Resources["_brushBlue"] as SolidColorBrush;
            _red = App.Current.Resources["_brushRed"] as SolidColorBrush;
            _yellow = App.Current.Resources["_brushYellow"] as SolidColorBrush;
        }

        private void _window_Closed(object sender, EventArgs e)
        {
            _ctlDrawingView3D.clearRoomInfoAll();
            _ctlDrawingView3D.clearRackInfoAll();
            _ctlDrawingView3D.clearEnvInfoAll();
            if (_ckbxRoomInfoShow.IsChecked ?? false)
                drawRoomInfo();
            if (_ckbxRackAssetInfoShow.IsChecked ?? false)
                drawRackInfo();

            alive = 0;
        }
        #endregion 

        #region // 환경 디비 읽기 처리
        private void dispRack()
        {
            IEnumerable<location> list = null;

            int floor_id2 = floor_id;
            list = g.location_list.Where(p => p.floor_id == floor_id2);
            asset_id_list = Stat.get_tot_eb_by_floor_id(floor_id2, out device_stat[0], out device_stat[1], out device_stat[2], out device_stat[3], out device_stat[4]);
        }

        private async Task<bool> getDBList()
        {
            _power1.Text = "0 Wh";
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
                           alive = a.alive,
                           smoke = a.smoke,
                           powerwh = a.powerwh,
                       };

            var list1 = tdb2.ToList();

            filter = "";
            site_environment_list = (List<site_environment>)await g.webapi.getList("site_environment", typeof(List<site_environment>), filter);
            if (site_environment_list == null) return false;

            if (eb_port_data_cur_list == null)
            {
                eb_port_data_cur_list = new List<eb_port_data_cur>();
                view_rack_list = new List<view_rack>();
            }
            else 
            {
                eb_port_data_cur_list.Clear();
                view_rack_list.Clear();
            }


            for (int i = 0; i < list1.Count(); i = i + 1)
            {
                view_rack view1 = new view_rack();
                eb_port_data_cur e1 = list1[i];
                e1.power_ph = e1.power_ph;
                e1.sensor_t = e1.sensor_t;
                e1.sensor_h = e1.sensor_h;
                e1.door = e1.door;

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
            double d1 = get_double(eb_port_data_cur_list.Max(p => p.power_p));
            //double d1 = get_double(eb_port_data_cur_list.Max(p => p.power_ph));
            _power1.Text = d1.ToString("N1") + " W";

            // 도어 열린 갯수 
            _door1.Text = eb_port_data_cur_list.Where(p => p.door == 1).Count().ToString() + " EA";

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

            _t1.Text = l1.ToString("N1") + " Wh ↑";
            _t2.Text = l2.ToString("N1") + " ~ " + l1.ToString("N1") + " Wh";
            _t3.Text = l2.ToString("N1") + " Wh ↓";
            
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
        #endregion

        #region // 층 자산 읽기 
        private DispatcherTimer _timer = new DispatcherTimer();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DispFloor(floor_id);
        }

        private async void TimerEvent(object sender, System.EventArgs e)
        {
            _timer.Stop();
            //Rectangle_MouseEnter1(null, null);
            Rectangle_MouseEnter4(null, null);
            /*
                                    //3D 화면에서 선택된 렉을 강죠 표시
                                    if (select_rack > 1)
                                        _ctlDrawingView3D.selectRack(select_rack);
                        */

        }

        public void  DispFloor(int floor_id2)
        {
            // 사이트 , 빌딩 , 층 , 도면 정보를 가져오기 
            site st = g.site_list.Find(at => at.site_id == g.select_site.site_id);
            if (st == null) return;
            var f = g.floor_list.Find(p => p.floor_id == floor_id2);
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
            _ckbxRackAssetInfoShow2.IsChecked = true; // 룸정보만 보이기 체크 처리 

            // 도면에서 이벤트 받기 처리 ??
            _ctlDrawingView3D.camMoveEndEventToAssetView += new DrawingView3D.camMoveEndHandler(cameraFocusMoveComplated);

            // 탑 그리드 처리 하기 
            _exP61.Header = l_st.location_path;

            _timer.Interval = System.TimeSpan.FromMilliseconds(1000);
            _timer.Tick += new EventHandler(TimerEvent);
            _timer.Start();

        }

 /*
        private void AssetView_RackInit(AssetTreeVM rk_vm)
        {
            rack rk = g.rack_list.Find(at => at.rack_id == rk_vm.type_id);
            if (rk == null) return;

            room rm = g.room_list.Find(at => at.room_id == rk.room_id);
            if (rm == null) return;

            floor fl = g.floor_list.Find(at => at.floor_id == rm.floor_id);
            if (fl == null) return;

            //현재 사용되는 buidling AssetTreeVM 저장한다
            AssetTreeVM rm_vm = getParentAssetTreeVM(rk_vm, rk_vm.disp_level);
            if (rm_vm == null) return;
            AssetTreeVM fl_vm = getParentAssetTreeVM(rm_vm, rm_vm.disp_level);
            if (fl_vm == null) return;
            use_fl_vm = fl_vm;
            use_rm_vm = rm_vm;
            use_rk_vm = rk_vm;

            //3d 화면 draw
            drawing_3d dr = g.drawing_3d_list.Find(at => at.drawing_3d_id == fl.drawing_3d_id);

            string file_path = string.Format("{0}drawing_3d/{1}", g.CLIENT_IMAGE_PATH, dr.file_name);
            //if (fl.drawing_3d_id != 19040015)
            //    Console.WriteLine("asdf");
            //_ctlDrawingView3D.openDrawingFile(file_path);
            //use_drawing_file_path = file_path;

            //현제까지 적용된 UI를 강재로 Action 시킨다
            Dispatcher.Invoke(new Action(() => { }), DispatcherPriority.ContextIdle, null);

            //drawRoomInfo();
            //_ckbxRoomInfoShow.IsChecked = true;
            drawFloorAllObject(fl_vm);

            // 링크다이어그램을 표시...
            showDefaultLinkViewInRack(rk_vm);

            // 렉뷰 표시
            showRackView(rk);
            _txbTop2Name.Text = rk_vm.disp_name;  // 2016.10.04
            _lvPortList.ItemsSource = null;

            //3D 화면에서 선택된 렉을 강죠 표시
            _ctlDrawingView3D.selectRack(rk.rack_id);

            _ctlDrawingView3D.openDrawingFile(file_path);
            use_drawing_file_path = file_path;

            //카메라를 렉 중심으로 이동
            //Point center_p = new Point((rk.pos_x ?? 0) / 100, (rk.pos_y ?? 0) / 100);
            //moveCamera(center_p, 100);

            Point start_p = new Point(((Double)(rm.square_x1 ?? 0)) / 100, ((Double)(rm.square_y1 ?? 0)) / 100);
            Point end_p = new Point(((Double)(rm.square_x2 ?? 0)) / 100, ((Double)(rm.square_y2 ?? 0)) / 100);
            Point center_p = new Point((start_p.X + end_p.X) / 2, (start_p.Y + end_p.Y) / 2);
            Double room_width = (Math.Abs(end_p.X - start_p.X) * 2);
            moveCamera(center_p, room_width);

            //_ckbxRackAssetInfoShow.IsChecked = true;
        }
*/


        #endregion

        #region AssetView_Room
        // 트리에 있는 모든 노드를 출력 - 랙만 처리 
        private void drawFloorAllObject(AssetTreeVM fl_vm)
        {
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
/*                                    if (view1.eb1.door > 0)
                                        _ctlDrawingView3D.changeRackBrush(rk.rack_id, _red);
                                    else
                                        _ctlDrawingView3D.changeRackBrush(rk.rack_id, _blue);
*/

                                    int t2 = view1.eb1.asset_id;
                                    eb_port_data_cur v1 = g.eb_port_data_cur_list.Find(p => p.asset_id == t2);
                                    if (v1.power_v == null)
                                        break;

                                    if (v1.door > 0)
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
            _ctlDrawingView3D.clearEnvInfoAll();
            if (_ckbxRoomInfoShow.IsChecked ?? false)
                drawRoomInfo();
            if (_ckbxRackAssetInfoShow.IsChecked ?? false)
                drawRackInfo();
        }

        public void SelectRack(int id)
        {
            if (id < 1) return;
            select_rack = id;
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
        private void _ckbxRackAssetInfoShow2_Unchecked(object sender, RoutedEventArgs e)
        {
            clearRackInfo2();
        }

        private void _ckbxRackAssetInfoShow2_Checked(object sender, RoutedEventArgs e)
        {
            drawRackInfo2();
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
                    {
                        _ctlDrawingView3D.drawRackInfo(rk);
                    }
                }
            }
        }

        private void clearRackInfo()
        {
            _ctlDrawingView3D.clearRackInfoAll();
        }

        private void drawRackInfo2()
        {
            if (use_fl_vm == null) return;

            List<room> rm_list = g.room_list.FindAll(at => at.floor_id == use_fl_vm.type_id);
            foreach (var rm in rm_list)
            {
                List<rack> rk_list = g.rack_list.FindAll(at => at.room_id == rm.room_id);
                foreach (var rk in rk_list)
                {
                    if (rk.pos_x != null)
                    {
                        var asset_list = from at in g.asset_tree_list
                                         join lo in g.location_list.Where(p => p.rack_id == rk.rack_id) on at.location_id equals lo.location_id
                                         join aa in g.asset_list on at.asset_id equals aa.asset_id
                                         join cc in g.catalog_list.Where(p => p.catalog_id == 412002 || p.catalog_id == 412003) on aa.catalog_id equals cc.catalog_id
                                         join eb in g.eb_port_data_cur_list on at.asset_id equals eb.asset_id
                            select new 
                            {
                                asset_id = aa.asset_id,
                            };

                        eb_port_data_cur v1 = null;
                        var t1 = asset_list.ToList();

                        if (t1.Count < 1)
                        {
                            //_ctlDrawingView3D.drawEnvInfo(rk, v1);
                            break;
                        }
                        int t2 = t1[0].asset_id;
                        v1 = g.eb_port_data_cur_list.Find(p=>p.asset_id == t2);
                        if (v1.power_v == null)
                            break;
                        _ctlDrawingView3D.drawEnvInfo(rk, v1);

                        if (v1.door > 0)
                            _ctlDrawingView3D.changeRackBrush(rk.rack_id, _red);
                        else
                            _ctlDrawingView3D.changeRackBrush(rk.rack_id, _blue);
                    }
                }
            }
        }

        private void clearRackInfo2()
        {
            _ckbxRackAssetInfoShow2.IsChecked = false;
            _ctlDrawingView3D.clearEnvInfoAll();
        }


        public void RefreshEnv()
        {
            if (_ckbxRackAssetInfoShow2.IsChecked == true)
            {
                _ctlDrawingView3D.clearEnvInfoAll();
                drawRackInfo2();
            }
            else {
                if (use_fl_vm == null) return;
                drawFloorRack(use_fl_vm);
            }
        }
        #endregion

        #region // 범례 변경 처리

        private async void Rectangle_MouseEnter1(object sender, MouseEventArgs e)
        {
            _grid1.Visibility = System.Windows.Visibility.Visible;
            _grid2.Visibility = System.Windows.Visibility.Hidden;

            dispRack();
            await getDBList();

            if (site_environment_list == null)   // romee 2015.10.30 환경 장비가 없으면 죽는 문제 있음 
                return;
            var t1 = site_environment_list.Find(p => p.site_id == g.selected_site_id);
            if (t1 == null) return;

            double d1 = get_double(t1.high_power_color);
            double d2 = get_double(t1.low_power_color);

            _t1.Text = d1.ToString("N1") + " Wh ↑";
            _t2.Text = d2.ToString("N1") + " ~ " + d1.ToString("N1") + " Wh";
            _t3.Text = d2.ToString("N1") + " Wh ↓";
            select_view = 0;
            drawFloorRack(use_fl_vm);
            return;
        }
        private async void Rectangle_MouseEnter2(object sender, MouseEventArgs e)
        {
            _grid1.Visibility = System.Windows.Visibility.Visible;
            _grid2.Visibility = System.Windows.Visibility.Hidden;

            dispRack();
            await getDBList();

            if (site_environment_list == null)   // romee 2015.10.30 환경 장비가 없으면 죽는 문제 있음 
                return;
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

            dispRack();
            await getDBList();

            if (site_environment_list == null)   // romee 2015.10.30 환경 장비가 없으면 죽는 문제 있음 
                return;
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

            dispRack();
            await getDBList();

            if (site_environment_list == null)   // romee 2015.10.30 환경 장비가 없으면 죽는 문제 있음 
                return;
            select_view = 3;
            drawFloorRack(use_fl_vm);
            return;
        }

        #endregion

    }
}
