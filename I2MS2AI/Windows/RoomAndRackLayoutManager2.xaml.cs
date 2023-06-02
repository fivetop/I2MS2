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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WebApi.Models;
using I2MS2.UserControls;
using I2MS2.UserControls.Drawing;
using I2MS2.Library;
using I2MS2.Library.Drawing;
using WebApiClient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Windows.Threading;

namespace I2MS2.Windows
{

    /// <summary>
    /// RoomAndRackLayoutManager.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class RoomAndRackLayoutManager2 : Window
    {
        AssetTreeVM _ast_vm;
        DrawingDataManager drawDataMgr;
        Size canvas_size;
        RoomRackLayoutMode layout_mode;

        DrawingVM selected_dvm;
        DrawingVM selecting_dvm;

        WebApiClientClass webapi;

        List<DrawingVM> dvm_list = new List<DrawingVM>();

        List<DrawingVM> use_rack_lvm_list = new List<DrawingVM>();
        List<DrawingVM> use_ast_lvm_list = new List<DrawingVM>();
        List<DrawingVM> use_up_lvm_list = new List<DrawingVM>();

        Drawing2D drawer2d;

        // 드래그, 에디트 처리
        Boolean en_drag_item = false;
        Boolean is_edit = false;

        // 드래그 처리 사용 
        Point start_p;
        Point scl_last_p;                       // 스크롤 옵셋
        Point scl_offset =  new Point(0,0);

        #region // initial 

        public RoomAndRackLayoutManager2(AssetTreeVM ast_vm)
        {
            InitializeComponent();

            drawDataMgr = new DrawingDataManager();
            drawer2d = new Drawing2D(_canvasRackDrawing);
            drawer2d.setSubCanvas(_canvasRackDrawingSub);
            layout_mode = RoomRackLayoutMode.NONE;
            _ast_vm = ast_vm;

            webapi = new WebApiClientClass(g.web_api_uri_string);

            hideRoomLayout();
        }

        private void _window_Loaded(object sender, RoutedEventArgs e)
        {
            //전체 그리기 화면의 사이즈를 파악한다
            canvas_size.Width = _gridDrawing.ActualWidth;
            canvas_size.Height = _gridDrawing.ActualHeight;


            AssetTreeVM mainAstVM;

            floor fl;
            // 룸만 받고 층은 알아서 가져오기 
            int fl_location_id = Etc.get_prev_location_id(_ast_vm.location_id);
            mainAstVM = g.location_ast_vm_dic[fl_location_id];
            fl = g.floor_list.Find(at => at.floor_id == mainAstVM.type_id);

            if (fl == null)
                return;

            // 초기 데이터 일어오기 
            initData();
            //그리기 
            drawItemInRoom(_ast_vm.type_id, _ast_vm.location_id);
        }

        private void initData()
        {
            List<room> tmp_room_list = g.room_list.FindAll(at => at.room_id == _ast_vm.type_id);
            foreach (var tmp_rm in tmp_room_list)
            {
                dvm_list.Add(makeVM(tmp_rm, AssetTreeType.Room));

                List<rack> tmp_rk_list = g.rack_list.FindAll(at => at.room_id == tmp_rm.room_id);
                foreach (var tmp_rk in tmp_rk_list)
                {
                    dvm_list.Add(makeVM(tmp_rk, AssetTreeType.Rack));
                }
            }

            List<asset> tmp_asset_list = g.asset_list.FindAll(at => at.location_id == _ast_vm.location_id);

            foreach (var use_ast_lvm in tmp_asset_list)
            {
                dvm_list.Add(makeVM(use_ast_lvm, AssetTreeType.FacePlate));
                List<user_port_layout> tmp_up_list = g.user_port_layout_list.FindAll(at => at.asset_id == use_ast_lvm.asset_id);
                foreach (var use_up_lvm in tmp_up_list)
                {
                    dvm_list.Add(makeVM(use_up_lvm, AssetTreeType.UserPort));
                }
            }
        }


        #endregion

        #region // Mouse Event

        private void _gridDrawing_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //                Point cur_p = e.GetPosition(_sclDrawing);
                Point cur_p = e.GetPosition(_gridDrawing);

                if (en_drag_item == true)
                {
                    Point simp_st_p = ToSimplePoint(start_p);
                    Point simp_cur_p = ToSimplePoint(cur_p);
                    Double simp_m_x = simp_cur_p.X - simp_st_p.X;
                    Double simp_m_y = simp_cur_p.Y - simp_st_p.Y;

                    switch (layout_mode)
                    {
                        case RoomRackLayoutMode.RACK_EDIT:
                            drawer2d.moveRack(new Vector(simp_m_x, simp_m_y), selected_dvm.rack_id);
                            break;
                        case RoomRackLayoutMode.ASSET_EDIT:
                            drawer2d.moveAsset(new Vector(simp_m_x, simp_m_y), selected_dvm.asset_id);
                            break;
                        case RoomRackLayoutMode.USERPORT_EDIT:
                            drawer2d.moveUserPort(new Vector(simp_m_x, simp_m_y), selected_dvm.user_port_layout_id);
                            break;
                    }
                    start_p = cur_p;
                }
                else if (!is_edit)
                {
                    Point cur_p0 = e.GetPosition(_gridDrawing);
                    Double move_x = scl_last_p.X - cur_p0.X;
                    Double move_y = scl_last_p.Y - cur_p0.Y;
                    scl_offset.X += move_x;
                    scl_offset.Y += move_y;

                    _sclDrawing.ScrollToHorizontalOffset(scl_offset.X);
                    _sclDrawing.ScrollToVerticalOffset(scl_offset.Y);
                    start_p = cur_p;
                }

            }
            else
            {
                DrawingVM new_dvm = null;

                start_p = e.GetPosition(_gridDrawing);
                if (!is_edit)
                    scl_last_p = start_p;
                //Console.WriteLine("({0},{1}, {2})", start_p.X, start_p.Y, e.Source.ToString());
                //Rack을 선택한 경우
                if (e.Source is DrawingItem2DRack)
                {
                    DrawingItem2DRack _uc_rack = (DrawingItem2DRack)e.Source;
                    int rack_id = getNumberInStr(_uc_rack.Name);
                    new_dvm = Find(rack_id, AssetTreeType.Rack);
                }
                //Asset을 선택한 경우
                else if (e.Source is DrawingItem2DAsset)
                {
                    DrawingItem2DAsset _uc_ast = (DrawingItem2DAsset)e.Source;
                    int asset_id = getNumberInStr(_uc_ast.Name);
                    new_dvm = Find(asset_id, AssetTreeType.FacePlate);
                }
                //User Port를 선택한 경우
                else if (e.Source is DrawingItem2DUserPort)
                {
                    DrawingItem2DUserPort up = (DrawingItem2DUserPort)e.Source;
                    int up_id = getNumberInStr(up.Name);
                    new_dvm = Find(up_id, AssetTreeType.UserPort);
                }
                //User Port를 선택한 경우
                else if (e.Source is Ellipse)
                {
                    new_dvm = Find(_ast_vm.type_id, AssetTreeType.Room);
                    Point selectP = e.GetPosition(_gridDrawing);
                    room_selected_point = new Point(selectP.X, selectP.Y);
                    _rectRoomBox.IsEnabled = true;
                }
                else
                {
                    is_edit = false;
//                      DrawingMap();
                }

                if (new_dvm == null) return;
                if (new_dvm == selecting_dvm) return;
                selecting_dvm = new_dvm;
                changeLayoutMode(selecting_dvm.type);
                dvm_change(0);
                DrawingMap(selecting_dvm);
                selected_dvm = selecting_dvm;


            }
        } 

        // 랙, 자산, 포트 선택후 드래그 처리 
        private void _gridDrawing_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (en_drag_item == true)
            {
                en_drag_item = false;

                switch (layout_mode)
                {
                    case RoomRackLayoutMode.RACK_EDIT:
                        Point rk_p = drawer2d.getRackPoint(selected_dvm.rack_id);

                        int new_x = (int)drawDataMgr.getVMValue_FromCanvasValue(canvas_size, rk_p.X);
                        int new_y = (int)drawDataMgr.getVMValue_FromCanvasValue(canvas_size, rk_p.Y);

                        if ((selected_dvm.pos_x != new_x) || (selected_dvm.pos_y != new_y))
                        {
                            //check in the room
                            Boolean is_in_room = IsInTheRoom(selected_dvm.room_id, new Point(new_x, new_y));
                            if (is_in_room == false) break;

                            //temp save  change rack layout  
                            selected_dvm.pos_x = new_x;
                            selected_dvm.pos_y = new_y;
                            selected_dvm.is_changed = true;
                        }
                        break;

                    case RoomRackLayoutMode.ASSET_EDIT:
                        Point ast_p = drawer2d.getAssetPoint(selected_dvm.asset_id);

                        int new_x2 = (int)drawDataMgr.getVMValue_FromCanvasValue(canvas_size, ast_p.X);
                        int new_y2 = (int)drawDataMgr.getVMValue_FromCanvasValue(canvas_size, ast_p.Y);

                        if ((selected_dvm.pos_x != new_x2) || (selected_dvm.pos_y != new_y2))
                        {
                            //check in the room
                            location l = g.location_list.Find(at => at.location_id == selected_dvm.location_id);
                            if (l == null) break;
                            Boolean is_in_room = IsInTheRoom(l.room_id ?? 0, new Point(new_x2, new_y2));
                            if (is_in_room == false) break;

                            //temp save  change asset layout  
                            selected_dvm.pos_x = new_x2;
                            selected_dvm.pos_y = new_y2;
                            selected_dvm.is_changed = true;

                            List<DrawingVM> tmp_up_lvm_list = dvm_list.FindAll(at => at.asset_id == selected_dvm.asset_id);
                            int count = 0;
                            Double term_vm = g.USERPORT_RADIUS * 2;
                            foreach (var up_lvm in tmp_up_lvm_list)
                            {
                                Double term = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, term_vm);

                                Point np = new Point(ast_p.X + term * count, ast_p.Y + term * 2);
                                Point np_db = drawDataMgr.getVMPoint_FromCanvasPoint(canvas_size, np);
                                up_lvm.pos_x = (int)np_db.X;
                                up_lvm.pos_y = (int)np_db.Y;
                                up_lvm.is_changed = true;

                                Size s = drawDataMgr.getCanvasSize_FromVMSize(canvas_size, new Size(g.USERPORT_RADIUS * 2, g.USERPORT_RADIUS * 2));
                                DrawingVM parent_ast_lvm = dvm_list.Find(at => at.asset_id == up_lvm.asset_id);
                                Point parent_p = drawDataMgr.getCanvasPoint_FromVMPoint(
                                                        canvas_size, new Point(parent_ast_lvm.pos_x ?? 0, parent_ast_lvm.pos_y ?? 0));
                                Size parent_s = drawDataMgr.getCanvasSize_FromVMSize(canvas_size, new Size(g.ASSET_SIZE_WIDTH, g.ASSET_SIZE_HEIGHT));
                                if (up_lvm.is_layout == "Y")
                                {
                                    drawer2d.moveUserPortAt(np, s, up_lvm.user_port_layout_id, parent_p, parent_s);
                                }
                                else
                                {
                                    up_lvm.is_layout = "Y";
                                    drawer2d.addUserPort(np, s, up_lvm.user_port_layout_id, up_lvm.port_no, parent_p, parent_s);
                                }
                                count++;
                            }
                        }
                        break;
                    case RoomRackLayoutMode.USERPORT_EDIT:
                        Point up_p = drawer2d.getUserPortPoint(selected_dvm.user_port_layout_id);

                        int new_x3 = (int)drawDataMgr.getVMValue_FromCanvasValue(canvas_size, up_p.X);
                        int new_y3 = (int)drawDataMgr.getVMValue_FromCanvasValue(canvas_size, up_p.Y);

                        if ((selected_dvm.pos_x != new_x3) || (selected_dvm.pos_y != new_y3))
                        {
                            //check in the room
                            asset ast = g.asset_list.Find(at => at.asset_id == selected_dvm.asset_id);
                            if (ast == null) break;
                            location l2 = g.location_list.Find(at => at.location_id == ast.location_id);
                            if (l2 == null) break;
                            Boolean is_in_room = IsInTheRoom(l2.room_id ?? 0, new Point(new_x3, new_y3));
                            if (is_in_room == false) break;

                            //temp save  change room layout  
                            selected_dvm.pos_x = new_x3;
                            selected_dvm.pos_y = new_y3;
                            selected_dvm.is_changed = true;
                        }
                        break;
                }
            }
            else
            {
                selected_dvm = Find(_ast_vm.type_id, _ast_vm.type);

                if (_ast_vm.type == AssetTreeType.Room)
                {
                    //ClearSelectItem();
                }
            }

        }

        // 랙 선택, 자산 선택, 유저포트 선택 시 
        // 룸박스도 여기서 처리 필요 
        private void _gridDrawing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            start_p = e.GetPosition(_gridDrawing);
            if (!is_edit)
                scl_last_p = start_p;
            Console.WriteLine("({0},{1})", start_p.X, start_p.Y);
            //Rack을 선택한 경우
            if (e.Source is DrawingItem2DRack)
            {
                DrawingItem2DRack _uc_rack = (DrawingItem2DRack)e.Source;
                int rack_id = getNumberInStr(_uc_rack.Name);

                en_drag_item = true;
                is_edit = true;
                if (selected_dvm == null) return;
                changeLayoutMode(selected_dvm.type);


            }
            //Asset을 선택한 경우
            else if (e.Source is DrawingItem2DAsset)
            {
                DrawingItem2DAsset _uc_ast = (DrawingItem2DAsset)e.Source;
                int asset_id = getNumberInStr(_uc_ast.Name);

                DrawingVM ast_lvm = dvm_list.Find(at => at.asset_id == asset_id);
                if (ast_lvm != null)
                {
                    en_drag_item = true;
                    is_edit = true;
                    if (selected_dvm == null) return;
                    changeLayoutMode(selected_dvm.type);
                }
            }
            //User Port를 선택한 경우
            else if (e.Source is DrawingItem2DUserPort)
            {
                DrawingItem2DUserPort up = (DrawingItem2DUserPort)e.Source;
                int up_id = getNumberInStr(up.Name);

                DrawingVM up_lvm = dvm_list.Find(at => at.user_port_layout_id == up_id);
                if (up_lvm != null)
                {
                    en_drag_item = true;
                    is_edit = true;
                    if (selected_dvm == null) return;
                    changeLayoutMode(selected_dvm.type);

                }
            }
            else 
            {
                is_edit = false;
                if (selected_dvm == null) return;
                changeLayoutMode(selected_dvm.type);
                selecting_dvm = null;
                dvm_change(0);

            }

        }

        private void changeLayoutMode(AssetTreeType type)
        {
            if (is_edit)
            {
                switch (type)
                {
                    case AssetTreeType.Room:
                            clearDrawGuideLine();
                            enableRoomLayout(true);
                            layout_mode = RoomRackLayoutMode.ROOM_EDIT;
                        break;

                    case AssetTreeType.Rack:
                            reDrawGuideLine(new Point(0, 0), new Point(canvas_size.Width, canvas_size.Height));
                            if (selected_dvm != null)
                                drawer2d.selectEditRack(selected_dvm.rack_id);
                            enableRoomLayout(false);
                            layout_mode = RoomRackLayoutMode.RACK_EDIT;
                        break;
                    case AssetTreeType.FacePlate:
                    case AssetTreeType.ConsolidationPoint:
                    case AssetTreeType.MutoaBox:
                            reDrawGuideLine(new Point(0, 0), new Point(canvas_size.Width, canvas_size.Height));
                            if (selected_dvm != null)
                                drawer2d.selectEditAsset(selected_dvm.asset_id);
                            enableRoomLayout(false);
                            layout_mode = RoomRackLayoutMode.ASSET_EDIT;
                        break;

                    case AssetTreeType.UserPort:
                            reDrawGuideLine(new Point(0, 0), new Point(canvas_size.Width, canvas_size.Height));
                            if (selected_dvm != null)
                                drawer2d.selectEditUserPort(selected_dvm.user_port_layout_id);
                            enableRoomLayout(false);
                            layout_mode = RoomRackLayoutMode.USERPORT_EDIT;
                        break;
                }
            }
            else 
            { 
                clearDrawGuideLine();
                enableRoomLayout(false);
                layout_mode = RoomRackLayoutMode.ROOM_VIEW;
            }
        }

        // 모든 선택된것을 지우고 현재 선택된 거만 셀렉트 처리 
        private void dvm_change(int p)
        {
            // 모두 지우기 
            foreach (var dvm1 in dvm_list)
            {
                if (dvm1.State == 1)
                { 
                    switch (selected_dvm.type)
                    {
                        case AssetTreeType.Room:
                            _ellipseRoomNamePoint.Fill = Brushes.Gray;
                            break;
                        case AssetTreeType.Rack:
                            drawer2d.releaseEditRack(dvm1.rack_id);
                            break;
                        case AssetTreeType.FacePlate:
                        case AssetTreeType.ConsolidationPoint:
                        case AssetTreeType.MutoaBox:
                            drawer2d.releaseEditAsset(dvm1.asset_id);
                            break;
                        case AssetTreeType.UserPort:
                            drawer2d.releaseEditUserPort(dvm1.user_port_layout_id);
                            break;
                    }
                }
                dvm1.State = 0;
            }

            if (selecting_dvm == null)
                return;
            // 선택된거 다시 그리기 
            switch (selecting_dvm.type)
            {
                case AssetTreeType.Room:
                    _ellipseRoomNamePoint.Fill = Brushes.OrangeRed;
                    showRoomLayoutByAstVM(selecting_dvm);
                    break;
                case AssetTreeType.Rack:
                    selectRack(selecting_dvm.rack_id, true);
                    break;
                case AssetTreeType.FacePlate:
                case AssetTreeType.ConsolidationPoint:
                case AssetTreeType.MutoaBox:
                    selectAsset(selecting_dvm.asset_id, false);
                    break;
                case AssetTreeType.UserPort:
                    selectUserPort(selecting_dvm.type_id, false);
                    break;
            }
            selecting_dvm.State = 1;
        }

        private void showRoomLayoutByAstVM(DrawingVM selecting_dvm)
        {
            if (selecting_dvm == null)
                return;
            Double x1 = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, selecting_dvm.square_x1 ?? 0);
            Double x2 = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, selecting_dvm.square_x2 ?? 0);
            Double y1 = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, selecting_dvm.square_y1 ?? 0);
            Double y2 = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, selecting_dvm.square_y2 ?? 0);

            Point p = new Point(x1, y1);
            Size s = new Size(Math.Abs(x2 - x1), Math.Abs(y2 - y1));

            Double f_x = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, selecting_dvm.pos_x ?? 0);
            Double f_y = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, selecting_dvm.pos_y ?? 0);

            Point f_p = new Point(f_x, f_y);

            showRoomLayout(p, s, f_p);
        }

        #endregion

        #region //RoomLayout Mouse Event   // 룸네임 위치 이동 처리 -> 드레그 시작
        private void _ellipseRoomNamePoint_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_rectRoomBox.IsEnabled == true)
            {
                //Point selectP = e.GetPosition(_rectRoomBox);
                Point selectP = e.GetPosition(_gridDrawing);
                room_selected_point = new Point(selectP.X, selectP.Y);
                room_point_drag_flag = true;
                _ellipseRoomNamePoint.Fill = Brushes.OrangeRed;

                //   Console.WriteLine("=== select point({0},{1}) === ", room_selected_point.X, room_selected_point.Y);
            }
        }
        // 룸박스 이동 시작
        private void _rectRoomBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }
        // 룸 사각 박스 크기 조절 시작 
        private void _rmLayoutCtlright_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void _gridRoomLayout_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // 룸에디트일 경우 처리 
            if (layout_mode == RoomRackLayoutMode.ROOM_EDIT)
            {

            }
        }


        // 4:3 비율의 사이즈를 유지한다.
        private void _gridRoomLayout_MouseMove(object sender, MouseEventArgs e)
        {
            if (room_size_flag)
            {
            }

            if (room_drag_flag)
            {
            }

            if (room_point_drag_flag)
            {
                //Point new_point = e.GetPosition(_rectRoomBox);
                Point new_point = e.GetPosition(_gridDrawing);
                Thickness old_th = _gridRoomNamePoint.Margin;
                Thickness th = _gridRoomNamePoint.Margin;

                double x_move = new_point.X - room_selected_point.X;
                double x = old_th.Left + x_move;
                if ((_rectRoomBox.Margin.Left - x < 0) && (_rectRoomBox.Margin.Left + _rectRoomBox.Width - _ellipseRoomNamePoint.Width - x > 0))
                {
                    th.Left = x;
                    room_selected_point.X = new_point.X;
                    room_point_margin_left = _gridRoomNamePoint.Margin.Left - _rectRoomBox.Margin.Left;
                }


                double y_move = new_point.Y - room_selected_point.Y;
                double y = old_th.Top + y_move;
                if ((_rectRoomBox.Margin.Top - y < 0) && (_rectRoomBox.Margin.Top + _rectRoomBox.Height - _ellipseRoomNamePoint.Height - y > 0))
                {
                    th.Top = y;
                    room_selected_point.Y = new_point.Y;
                    room_point_margin_top = _gridRoomNamePoint.Margin.Top - _rectRoomBox.Margin.Top;
                }

                _gridRoomNamePoint.Margin = th;
                room_selected_point = new_point;


                //Console.WriteLine("point_drag_flag old:({0},{1}), move({2},{3})",old_th.Left, old_th.Top, x_move,y_move);
                //Console.WriteLine("point_drag_flag NameMargin:({0},{1})", _gridNamePoint.Margin.Left, _gridNamePoint.Margin.Top);
            }
        }


        private void _gridRoomLayout_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Double w = _gridDrawing.ActualWidth;
            Point p = e.GetPosition(_sclDrawing);


            bool handle = (Keyboard.Modifiers & ModifierKeys.Control) > 0;
            if (!handle)
                return;

            //Console.WriteLine("mouse: {0},{1}", p.X, p.Y);
            if (e.Delta > 0)
            {

                if (w > 10240)
                    return;
                canvasZoom(2, p);

            }
            else
            {
                if (w < 1100)
                    return;
                canvasZoom(0.5, p);
            }
        }
        #endregion

        #region // Drawing logic 

        private void DrawingMap(DrawingVM select_dvm)
        {
            if (select_dvm == null)
                return;
            switch (select_dvm.type)
            {
                case AssetTreeType.Room:
                    showRoomLayoutByAstVM(select_dvm);
                    break;

                case AssetTreeType.Rack:
                    location l2 = g.location_list.Find(at => at.location_id == select_dvm.location_id);

                    if ((layout_mode != RoomRackLayoutMode.RACK_EDIT) && (layout_mode != RoomRackLayoutMode.ROOM_VIEW))
                    {
                        changeLayoutMode(select_dvm.type);
                        //해당 룸을 표시한다
                        enableRoomLayout(false);
                        //렉인경우 Asset은 Location ID가 room 의 location id 이므로 room 의 locationid 를 넣어준다
                        drawItemInRoom(l2.room_id, select_dvm.location_id);
                    }
                    break;

                case AssetTreeType.FacePlate:
                case AssetTreeType.ConsolidationPoint:
                case AssetTreeType.MutoaBox:

                    if (((layout_mode != RoomRackLayoutMode.ASSET_EDIT) && (layout_mode != RoomRackLayoutMode.ROOM_VIEW))
                        )
                    {
                        changeLayoutMode(select_dvm.type);

                        //해당 룸을 표시한다
                        enableRoomLayout(false);

                        location l3 = g.location_list.Find(at => at.location_id == select_dvm.location_id);

                        drawItemInRoom(l3.room_id, select_dvm.location_id);
                    }
                    break;
                case AssetTreeType.UserPort:
                    changeLayoutMode(AssetTreeType.UserPort);

                    if (((layout_mode != RoomRackLayoutMode.USERPORT_EDIT) && (layout_mode != RoomRackLayoutMode.ROOM_VIEW))
                        )
                    {
                        //해당 룸을 표시한다
                        enableRoomLayout(false);
                        showRoomLayoutByAstVM(selected_dvm);

                        location l4 = g.location_list.Find(at => at.location_id == select_dvm.location_id);

                        drawItemInRoom(l4.room_id, select_dvm.location_id);
                    }
                    break;
            }
        }

        private DrawingVM Find(int p, AssetTreeType assetTreeType)
        {
            DrawingVM ret;
            ret = dvm_list.Find(at => (at.room_id == p || at.rack_id == p || at.asset_id == p || at.user_port_layout_id == p) && at.type == assetTreeType);
            return ret;
        }

        private void drawItemInRoom(Nullable<int> room_id, int location_id)
        {
            if (room_id != null)
            {
                int id = (int)room_id;
                drawer2d.removeAll();

                selecting_dvm = Find(id , AssetTreeType.Room);
                showRoomLayoutByAstVM(selecting_dvm);

                //해당 룸의 렉을 표시한다
                use_rack_lvm_list.Clear();
                use_rack_lvm_list = dvm_list.FindAll(at => at.room_id == selecting_dvm.room_id && at.type == AssetTreeType.Rack);
                if(use_rack_lvm_list.Count!=0)
                    drawRackList(use_rack_lvm_list);

                //해당 룸의 Asset을 표시한다
                use_ast_lvm_list.Clear();
                use_ast_lvm_list = dvm_list.FindAll(at => at.location_id == location_id);
                if (use_ast_lvm_list.Count != 0)
                    drawAssetList(use_ast_lvm_list);


                //해당 룸의 UserPort를 표시한다
                use_up_lvm_list.Clear();
                foreach (var use_ast_lvm in use_ast_lvm_list)
                {
                    List<DrawingVM> tmp_up_lvm_list = dvm_list.FindAll(at => at.asset_id == use_ast_lvm.asset_id && at.type == AssetTreeType.UserPort);
                    use_up_lvm_list.AddRange(tmp_up_lvm_list);
                }
                if(use_up_lvm_list.Count!=0)
                    drawUserPortList(use_up_lvm_list);
            }
        }
        #endregion


        #region RoomLayout Control Hide,Show,Enable Method

        Point room_selected_point;
        bool room_drag_flag = false;
        bool room_size_flag = false;
        bool room_point_drag_flag = false;

        Double room_point_margin_top;
        Double room_point_margin_left;

        private void showRoomLayout(Point position, Size size, Point flag_p)
        {
            if (size.Width == 0)
                size = new Size(200, 150);
            _rectRoomBox.Margin = new Thickness(position.X, position.Y, 0, 0);
            _rectRoomBox.Width = size.Width;
            _rectRoomBox.Height = size.Height;
            room_point_margin_left = _rectRoomBox.Width / 2;
            room_point_margin_top = _rectRoomBox.Height / 2;

            if (flag_p.X == 0)
            {
                Thickness th = new Thickness(_rectRoomBox.Margin.Left + _rectRoomBox.Width / 2
                , _rectRoomBox.Margin.Top + _rectRoomBox.Height / 2, 0, 0);
                _gridRoomNamePoint.Margin = th;
            }
            else
                _gridRoomNamePoint.Margin = new Thickness(flag_p.X, flag_p.Y, 0, 0);
/*

            _gridRoomLayout.HorizontalAlignment = HorizontalAlignment.Stretch;
            _gridRoomLayout.VerticalAlignment = VerticalAlignment.Stretch;
 */
        }

        private void hideRoomLayout()
        {
            _rectRoomBox.Width = 0;
            _rectRoomBox.Height = 0;
            _gridRoomNamePoint.Margin = new Thickness(0, 0, 0, 0);

            _gridRoomLayout.HorizontalAlignment = HorizontalAlignment.Left;
            _gridRoomLayout.VerticalAlignment = VerticalAlignment.Top;
        }

        private void enableRoomLayout(Boolean en)
        {
            _rectRoomBox.IsEnabled = en;
        } 
        #endregion
 
        #region draw2D Method
 
        private void selectRack(int rack_id, Boolean force_change)
        {
            if (selected_dvm == null)
            {
                selected_dvm = use_rack_lvm_list.Find(at => at.rack_id == rack_id);
                drawer2d.selectRack(rack_id);
                if (is_edit)
                    drawer2d.selectEditRack(rack_id);
            }
            else if ((selected_dvm.rack_id != rack_id) || force_change)
            {
                drawer2d.releaseRack(selected_dvm.rack_id);

                selected_dvm = use_rack_lvm_list.Find(at => at.rack_id == rack_id);
                drawer2d.selectRack(rack_id);
                if (is_edit)
                    drawer2d.selectEditRack(rack_id);
            }
        }


        private void selectAsset(int asset_id, Boolean force_change)
        {
            if(selected_dvm== null)
            {
                selected_dvm = use_ast_lvm_list.Find(at => at.asset_id == asset_id);
                drawer2d.selectAsset(asset_id);
                if (is_edit)
                    drawer2d.selectEditAsset(asset_id);
            }
            else if((selected_dvm.asset_id != asset_id) || force_change)
            {
                drawer2d.releaseAsset(selected_dvm.asset_id);
               
                selected_dvm = use_ast_lvm_list.Find(at => at.asset_id == asset_id);
                drawer2d.selectAsset(asset_id);
                if (is_edit)
                    drawer2d.selectEditAsset(asset_id);
            }
        }


        private void selectUserPort(int up_id, Boolean force_change)
        {
            if (selected_dvm == null)
            {
                selected_dvm = use_up_lvm_list.Find(at => at.user_port_layout_id == up_id);
                drawer2d.selectUserPort(up_id);
                if (is_edit)
                    drawer2d.selectEditUserPort(up_id);
            }
            else if ((selected_dvm.user_port_layout_id != up_id) || force_change)
            {
                drawer2d.releaseUserPort(selected_dvm.user_port_layout_id);

                selected_dvm = use_up_lvm_list.Find(at => at.user_port_layout_id == up_id);
                drawer2d.selectUserPort(up_id);
                if (is_edit)
                    drawer2d.selectEditUserPort(up_id);
            }
        }



        public void drawRackList(List<DrawingVM> rk_list)
        {
            int num_p_count = 0;
            foreach (var rk in rk_list)
            {
                Point p ;
                Point p2;
                if (rk.pos_x == null)
                {
                    int name_length = rk.rack_name.Length;
                    //p = new Point(
                    //    _rectRoomBox.Margin.Left - drawDataMgr.getCanvasValue_FromVMValue(canvas_size, 7000)
                    //    , _rectRoomBox.Margin.Top + num_p_count * 30);

                    p = new Point(
                        _rectRoomBox.Margin.Left - drawDataMgr.getCanvasValue_FromVMValue(canvas_size, g.RACK_SIZE_WIDTH) *3/2
                        , _rectRoomBox.Margin.Top + num_p_count * drawDataMgr.getCanvasValue_FromVMValue(canvas_size,g.RACK_SIZE_HEIGHT)*3/2);
                    //, _rectRoomBox.Margin.Top + num_p_count * drawDataMgr.getCanvasValue_FromVMValue(canvas_size, 2100));
                    num_p_count++;

                    //p2 = ToSimplePoint(p);
                    p2 = p;
                    rk.pos_x = (int)drawDataMgr.getVMValue_FromCanvasValue(canvas_size, p2.X);
                    rk.pos_y = (int)drawDataMgr.getVMValue_FromCanvasValue(canvas_size, p2.Y);
                    rk.is_changed = true;
                }
                else
                {
                    p = drawDataMgr.getCanvasPoint_FromVMPoint(canvas_size, new Point(rk.pos_x ?? 0, rk.pos_y ?? 0));
                    p2 = ToSimplePoint(p);
                }
                Size s = drawDataMgr.getCanvasSize_FromVMSize(canvas_size, new Size(g.RACK_SIZE_WIDTH, g.RACK_SIZE_HEIGHT));
                Double h = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, g.RACK_HEIGHT);
                Color c = Colors.SkyBlue;
                drawer2d.addRack(p2, s, h, c, rk.rack_id, rk.rack_name);

                
            }
        }


        public void drawAssetList(List<DrawingVM> ast_list)
        {
            int num_p_count = 0;
            foreach (var ast in ast_list)
            {
                Point p;
                Point p2;
                if (ast.pos_x == null)
                {
                    p = new Point(
                        _rectRoomBox.Margin.Left - drawDataMgr.getCanvasValue_FromVMValue(canvas_size, g.RACK_SIZE_WIDTH) *3/2
                        , _rectRoomBox.Margin.Top + num_p_count * drawDataMgr.getCanvasValue_FromVMValue(canvas_size, g.ASSET_SIZE_HEIGHT) * 3 / 2);
                    num_p_count++;

                    //p2 = ToSimplePoint(p);
                    p2 = p;
                    ast.pos_x = (int)drawDataMgr.getVMValue_FromCanvasValue(canvas_size, p2.X);
                    ast.pos_y = (int)drawDataMgr.getVMValue_FromCanvasValue(canvas_size, p2.Y);
                    ast.is_changed = true;
                }
                else
                {
                    p = drawDataMgr.getCanvasPoint_FromVMPoint(canvas_size, new Point(ast.pos_x ?? 0, ast.pos_y ?? 0));
                    p2 = ToSimplePoint(p);
                }
                

                Size s = drawDataMgr.getCanvasSize_FromVMSize(canvas_size, new Size(g.ASSET_SIZE_WIDTH, g.ASSET_SIZE_HEIGHT));
                Double h = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, g.ASSET_SIZE_HEIGHT);
                
                drawer2d.addAsset(p2, s, h, ast.type, ast.asset_id, ast.asset_name);
               

            }
        }


        public void drawUserPortList(List<DrawingVM> draw_up_lvm_list)
        {
            foreach (var up_lvm in draw_up_lvm_list)
            {
                if(up_lvm.is_layout== "Y")
                {
                    Point p = drawDataMgr.getCanvasPoint_FromVMPoint(canvas_size, new Point(up_lvm.pos_x ?? 0, up_lvm.pos_y ?? 0));

                    Size s = drawDataMgr.getCanvasSize_FromVMSize(canvas_size, new Size(g.USERPORT_RADIUS * 2, g.USERPORT_RADIUS * 2));
                    Double h = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, g.USERPORT_HEIGHT);

                    DrawingVM parent_ast_lvm = dvm_list.Find(at => at.asset_id == up_lvm.asset_id);
                    Point parent_p = drawDataMgr.getCanvasPoint_FromVMPoint(canvas_size, new Point(parent_ast_lvm.pos_x ?? 0, parent_ast_lvm.pos_y ?? 0));
                    Size parent_s = drawDataMgr.getCanvasSize_FromVMSize(canvas_size, new Size(g.ASSET_SIZE_WIDTH, g.ASSET_SIZE_HEIGHT));
                    
                    drawer2d.addUserPort(p,s,up_lvm.user_port_layout_id, up_lvm.port_no, parent_p, parent_s);

                }
            }
        }
        

        #endregion

        #region Draw Guide Line Method

        private void clearDrawGuideLine()
        {
            _gridGuide.Children.Clear();
        }

        private void reDrawGuideLine(Point startPoint, Point endPoint)
        {
            _gridGuide.Children.Clear();
            drawGuideLine(_gridGuide, startPoint, endPoint);
        }

        //draw Guilde Line
        public void drawGuideLine(Grid grid, Point startPoint, Point endPoint)
        {
            Brush _brush = Brushes.Aqua;
            Double _opacity = 0.5;
            Double _bigLineThiness = 0.5;
            Double _smallLineThiness = 0.1;
            
            int _horizontalLineCount = (int)endPoint.Y / 10 + 1;
            int _verticalLineCount = (int)endPoint.X / 10 + 1;



            Line[] HorizontalLine = new Line[_horizontalLineCount];
            for (int i = 0; i < _horizontalLineCount; i++)
            {
                HorizontalLine[i] = new Line();
                HorizontalLine[i].Opacity = _opacity;
                if (i % 5 == 0)
                    HorizontalLine[i].StrokeThickness = _bigLineThiness;
                else
                    HorizontalLine[i].StrokeThickness = _smallLineThiness;

                HorizontalLine[i].Stroke = _brush;
                HorizontalLine[i].X1 = startPoint.X;
                HorizontalLine[i].Y1 = startPoint.Y + i * 10;
                HorizontalLine[i].X2 = endPoint.X;
                HorizontalLine[i].Y2 = startPoint.Y + i * 10;

                grid.Children.Add(HorizontalLine[i]);
            }

            Line[] VerticalLine = new Line[_verticalLineCount];
            for (int j = 0; j < _verticalLineCount; j++)
            {
                VerticalLine[j] = new Line();
                VerticalLine[j].Opacity = _opacity;
                if ((j % 5) == 0)
                    VerticalLine[j].StrokeThickness = _bigLineThiness;
                else
                    VerticalLine[j].StrokeThickness = _smallLineThiness;
                //VerticalLine[j].Stroke = SystemColors.WindowFrameBrush;
                VerticalLine[j].Stroke = _brush;
                VerticalLine[j].X1 = startPoint.X + j * 10;
                VerticalLine[j].Y1 = startPoint.Y;
                VerticalLine[j].X2 = startPoint.X + j * 10;
                VerticalLine[j].Y2 = endPoint.Y;
                grid.Children.Add(VerticalLine[j]);
            }
        }
        #endregion

        #region LayoutVM data control method

        private DrawingVM makeVM(Object o1, AssetTreeType type)
        {
            DrawingVM t1 = new DrawingVM();


            switch (type)
            {
                case AssetTreeType.Room:
                    t1.room_id = ((room)o1).room_id;
                    t1.floor_id = ((room)o1).floor_id;
                    t1.room_name = ((room)o1).room_name;
                    t1.square_x1 = ((room)o1).square_x1;
                    t1.square_x2 = ((room)o1).square_x2;
                    t1.square_y1 = ((room)o1).square_y1;
                    t1.square_y2 = ((room)o1).square_y2;
                    t1.pos_x = ((room)o1).flag_x;
                    t1.pos_y = ((room)o1).flag_y;
                    t1.type_id = ((room)o1).room_id; 
                    break;

                case AssetTreeType.Rack:
                    t1.room_id = ((rack) o1).room_id;
                    t1.rack_id = ((rack) o1).rack_id;
                    t1.rack_name = ((rack)o1).rack_name;
                    t1.pos_x = ((rack)o1).pos_x;
                    t1.pos_y = ((rack)o1).pos_y;
                    t1.type_id = ((rack)o1).rack_id;
                    break;

                case AssetTreeType.FacePlate:
                case AssetTreeType.MutoaBox:
                case AssetTreeType.ConsolidationPoint:
                    t1.asset_id = ((asset)o1).asset_id;
                    t1.catalog_id = ((asset)o1).catalog_id;
                    t1.location_id = ((asset)o1).location_id;
                    t1.asset_name = ((asset)o1).asset_name;
                    t1.is_layout = ((asset)o1).is_layout;
                    t1.pos_x = ((asset)o1).pos_x;
                    t1.pos_y = ((asset)o1).pos_y;
                    t1.type_id = ((asset)o1).asset_id;
                    break;

                case AssetTreeType.UserPort:
                    t1.user_port_layout_id = ((user_port_layout)o1).user_port_layout_id;
                    t1.asset_id = ((user_port_layout)o1).asset_id;
                    t1.port_no = ((user_port_layout)o1).port_no;
                    t1.is_layout = ((user_port_layout)o1).is_layout;
                    t1.pos_x = ((user_port_layout)o1).pos_x;
                    t1.pos_y = ((user_port_layout)o1).pos_y;
                    t1.type_id = ((user_port_layout)o1).user_port_layout_id;
                    break;

            }
            t1.type = type;
            t1.State = 0;
            return t1;
        }

 
        #endregion

        #region ZoomButton Event
        // 줌인
        private void _btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            //_gridDrawing.Height = _gridDrawing.ActualHeight * 2;
            //_gridDrawing.Width = _gridDrawing.ActualWidth * 2;

            //reDrawAll();
        }
        // 줌아웃
        private void _btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            //_gridDrawing.Height = _gridDrawing.ActualHeight / 2;
            //_gridDrawing.Width = _gridDrawing.ActualWidth / 2;

            //reDrawAll();
        }

        // 2018-03-28 
        // 현재 축소시 잘 안되는 현상 있음 
        // 우선 두고 넘어감 
        // 버그 잡이 출동 
        private void canvasZoom(Double ratio, Point center_p)
        {
            scl_last_p = Mouse.GetPosition(_canvasWallDrawing);

            Double w = _gridDrawing.ActualWidth;
            Double h = _gridDrawing.ActualHeight;

            _gridDrawing.Width = w * ratio;
            _gridDrawing.Height = h * ratio;

            //_canvasWallDrawing.Width = w * ratio;
            //_canvasWallDrawing.Height = h * ratio;

            //_gridGuide.Width = w * ratio;
            //_gridGuide.Height = h * ratio;

            canvas_size = new Size(w * ratio, h * ratio);
//            reDrawGuideLine(new Point(0, 0), new Point(w * ratio, h * ratio));
///*
            Point p1 = Mouse.GetPosition(_gridDrawing);
            Double scX; //  = _sclDrawing.ContentHorizontalOffset + (p1.X);
            Double scY; // = _sclDrawing.ContentVerticalOffset + (p1.Y);
            Console.WriteLine("mouse : {0},{1}", p1.X, p1.Y);
            //Console.WriteLine("XY2 {0},{1}", scl_last_p.X, scl_last_p.Y);
            Console.WriteLine("offset : {0},{1}", _sclDrawing.ContentHorizontalOffset, _sclDrawing.ContentVerticalOffset);
            if (ratio > 1)
            {
                scX = _sclDrawing.ContentHorizontalOffset + (p1.X);
                scY = _sclDrawing.ContentVerticalOffset + (p1.Y);
                Console.WriteLine("result up : {0},{1}", scX, scY);
            }
            else
            {
                if (_sclDrawing.ContentHorizontalOffset * 2 > _sclDrawing.ExtentWidth)
                {
                    //scX = p1.X - _sclDrawing.ContentHorizontalOffset + 34; // (_sclDrawing.ExtentWidth - _sclDrawing.ContentHorizontalOffset) + (p1.X);
                    //scY = p1.Y - _sclDrawing.ContentVerticalOffset + 34;  //(_sclDrawing.ExtentHeight - _sclDrawing.ContentVerticalOffset) + (p1.Y);
                    scX = _sclDrawing.ContentHorizontalOffset - (p1.X / 2); // (_sclDrawing.ExtentWidth - _sclDrawing.ContentHorizontalOffset) + (p1.X);
                    scY = _sclDrawing.ContentVerticalOffset - (p1.Y / 2);  //(_sclDrawing.ExtentHeight - _sclDrawing.ContentVerticalOffset) + (p1.Y);
                }
                else
                { 
                    scX = p1.X - _sclDrawing.ContentHorizontalOffset + 34; // (_sclDrawing.ExtentWidth - _sclDrawing.ContentHorizontalOffset) + (p1.X);
                    scY = p1.Y - _sclDrawing.ContentVerticalOffset + 34;  //(_sclDrawing.ExtentHeight - _sclDrawing.ContentVerticalOffset) + (p1.Y);
                }
                Console.WriteLine("result dn : {0},{1}", scX, scY);
            }

            
            //Console.WriteLine("_sclDrawing {0},{1}", _sclDrawing.ContentHorizontalOffset, _sclDrawing.ContentVerticalOffset);

            _sclDrawing.ScrollToHorizontalOffset(scX);
            _sclDrawing.ScrollToVerticalOffset(scY);
            reDrawAll();
//*/
        }

        private void reDrawAll()
        {
            canvas_size = new Size(_gridDrawing.Width, _gridDrawing.Height);

            if (selected_dvm != null)
            {
                location l = g.location_list.Find(at => at.location_id == selected_dvm.location_id);

                //이전 렉, 에셋을 지우고
                drawer2d.removeAll();
                    
                //해당 룸의 렉을 표시한다
                drawRackList(use_rack_lvm_list);

                //해당 룸의 Asset을 표시한다
                drawAssetList(use_ast_lvm_list);

                //해당 룸의 UserPort를 표시한다
                drawUserPortList(use_up_lvm_list);

                showRoomLayoutByAstVM(selected_dvm);
                changeLayoutMode(selected_dvm.type);
            }
        }      
        #endregion

        #region // button save, update //EditMode Select ToggleButton Event

        private void _btnSave_Click(object sender, RoutedEventArgs e)
        {

            int room_save_count = 0;
            foreach (var rm_lvm in dvm_list)
            {
                if (rm_lvm.is_changed)
                {
                    room_save_count++;
                    SaveVM(rm_lvm);
                    rm_lvm.is_changed = false;
                }
            }
            _btnSave.Focusable = false;
            _tbtnEdit.IsChecked = false;
        }

        private async void SaveVM(DrawingVM t1)
        {
            int ret;
            Boolean is_save;

            switch (t1.type)
            {
                case AssetTreeType.Room:
                    room g_rm = g.room_list.Find(at => at.room_id == t1.room_id);
                    g_rm.square_x1 = t1.square_x1;
                    g_rm.square_x2 = t1.square_x2;
                    g_rm.square_y1 = t1.square_y1;
                    g_rm.square_y2 = t1.square_y2;

                    g_rm.flag_x = t1.pos_x;
                    g_rm.flag_y = t1.pos_y;
                    ret = await webapi.put("room", g_rm.room_id, g_rm, typeof(room));
                    break;

                case AssetTreeType.Rack:
                    //check is in the room
                    is_save = IsInTheRoom(t1.room_id, new Point(t1.pos_x ?? 0, t1.pos_y ?? 0));
                    if (is_save == false)
                    {
                        t1.pos_x = null;
                        t1.pos_y = null;
                    }

                    //global update
                    rack g_rk = g.rack_list.Find(at => at.rack_id == t1.rack_id);
                    g_rk.pos_x = t1.pos_x;
                    g_rk.pos_y = t1.pos_y;
                    ret = await webapi.put("rack", g_rk.rack_id, g_rk, typeof(rack));
                    break;

                case AssetTreeType.FacePlate:
                case AssetTreeType.MutoaBox:
                case AssetTreeType.ConsolidationPoint:

                    //check is in the room
                    location l = g.location_list.Find(at => at.location_id == t1.location_id);
                    is_save = IsInTheRoom(l.room_id ?? 0, new Point(t1.pos_x ?? 0, t1.pos_y ?? 0));
                    if (is_save == false)
                    {
                        //룸의 배치 범위를 벗어나는 경우
                        t1.pos_x = null;
                        t1.pos_y = null;
                        t1.is_layout = "N";
                    }
                    else
                    {
                        t1.is_layout = "Y";
                    }


                    //global update
                    asset g_ast = g.asset_list.Find(at => at.asset_id == t1.asset_id);
                    g_ast.pos_x = t1.pos_x;
                    g_ast.pos_y = t1.pos_y;
                    g_ast.is_layout = t1.is_layout;
                    ret = await webapi.put("asset", g_ast.asset_id, g_ast, typeof(asset));
                    break;

                case AssetTreeType.UserPort:
                    //check is in the room
                    //global update
                    user_port_layout g_up = g.user_port_layout_list.Find(at => at.user_port_layout_id == t1.user_port_layout_id);
                    g_up.pos_x = t1.pos_x;
                    g_up.pos_y = t1.pos_y;
                    g_up.is_layout = t1.is_layout;
                    ret = await webapi.put("user_port_layout", g_up.user_port_layout_id, g_up, typeof(user_port_layout));
                    break;

            }
        }

        private void _tbtnEdit_Checked(object sender, RoutedEventArgs e)
        {
            is_edit = true;
            if (selected_dvm == null) return;
            changeLayoutMode(selected_dvm.type);


            RoomAndRackLayoutManager window = new RoomAndRackLayoutManager(_ast_vm);
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();

        }

        private void _tbtnEdit_Unchecked(object sender, RoutedEventArgs e)
        {
            is_edit = false;
            if (selected_dvm == null) return;
            changeLayoutMode(selected_dvm.type);
        }
        #endregion


        #region // util 
        private Point ToSimplePoint(Point _point)
        {
            Point _resultPoint = new Point();
            double _decimalX = _point.X % 10;
            double _decimalY = _point.Y % 10;
            if (_decimalX > 5)
                _resultPoint.X = _point.X - _decimalX + 10;
            else
                _resultPoint.X = _point.X - _decimalX;

            if (_decimalY > 5)
                _resultPoint.Y = _point.Y - _decimalY + 10;
            else
                _resultPoint.Y = _point.Y - _decimalY;

            //Console.WriteLine("input({0},{1} => output({2},{3})", _point.X, _point.Y, _resultPoint.X, _resultPoint.Y);
            return _resultPoint;
        }

        private Boolean IsInTheRoom(int room_id, Point p)
        {
            DrawingVM rm_lvm = dvm_list.Find(at => at.room_id == room_id);
            if (rm_lvm == null)
                return false;

            Point rm_p1 = new Point(rm_lvm.square_x1 ?? 0,  rm_lvm.square_y1 ?? 0);
            Point rm_p2 = new Point(rm_lvm.square_x2 ?? 0, rm_lvm.square_y2 ?? 0);


            if ((p.X > rm_p1.X) && (p.X < rm_p2.X) && (p.Y > rm_p1.Y) && (p.Y < rm_p2.Y))
                return true;

            return false;
        }

        private int getNumberInStr(string str)
        {
            try
            {
                string str1 = Regex.Replace(str, @"\D", "");
                return int.Parse(str1);
            }
            catch (Exception e) { Console.WriteLine("{0}", e.Message); return 0; }
        }


        private void assetTreeVMCheckVisibilty(int id, AssetTreeType type, Boolean check)
        {
        }
        #endregion

        Point? lastMousePositionOnTarget;

        private void _gridRoomLayout_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
        }

        private void _sclDrawing_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            /*
            Point p1 = Mouse.GetPosition(_gridDrawing);


            if (p1.X != scl_last_p.X) return;
            double dX1 = p1.X;// -scl_last_p.X;
            double dY1 = p1.Y;// -scl_last_p.Y;

            Double scX; //  = _sclDrawing.ContentHorizontalOffset + (p1.X);
            Double scY; // = _sclDrawing.ContentVerticalOffset + (p1.Y);
            scX = _sclDrawing.ContentHorizontalOffset + (dX1);
            scY = _sclDrawing.ContentVerticalOffset + (dY1);

            //Console.WriteLine("XY {0},{1}", scX, scY);
            Console.WriteLine("XY1 {0},{1}", p1.X, p1.Y);
            Console.WriteLine("XY2 {0},{1}", scl_last_p.X, scl_last_p.Y);

            //Console.WriteLine("_sclDrawing {0},{1}", _sclDrawing.ContentHorizontalOffset, _sclDrawing.ContentVerticalOffset);

            _sclDrawing.ScrollToHorizontalOffset(scX);
            _sclDrawing.ScrollToVerticalOffset(scY);
            //reDrawAll();
            */
            return;
        }

        private void _sclDrawing_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
//            scl_last_p = Mouse.GetPosition(_gridRoomLayout);

        }

    }

}
