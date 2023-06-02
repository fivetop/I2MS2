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
using MahApps.Metro.Controls;

namespace I2MS2.Windows
{
    #region Layout VM Class
    public partial class RoomLayoutVM
    {
        public int room_id { get; set; }
        public int floor_id { get; set; }
        public string room_name { get; set; }
        public Nullable<int> square_x1 { get; set; }
        public Nullable<int> square_y1 { get; set; }
        public Nullable<int> square_x2 { get; set; }
        public Nullable<int> square_y2 { get; set; }
        public int user_id { get; set; }
        public byte[] last_updated { get; set; }
        public string remarks { get; set; }
        public Nullable<int> flag_x { get; set; }
        public Nullable<int> flag_y { get; set; }

        public Boolean is_changed { get; set; }
    }
    public partial class RackLayoutVM
    {
        public int rack_id { get; set; }
        public int room_id { get; set; }
        public string rack_row { get; set; }
        public int rack_no { get; set; }
        public Nullable<int> pos_x { get; set; }
        public Nullable<int> pos_y { get; set; }
        public int angle { get; set; }
        public Nullable<int> rack_catalog_id { get; set; }
        public Nullable<int> vcm_l_catalog_id { get; set; }
        public Nullable<int> vcm_r_catalog_id { get; set; }
        public int user_id { get; set; }
        public byte[] last_updated { get; set; }
        public string remarks { get; set; }
        public string rack_name { get; set; }

        public Boolean is_changed { get; set; }
    }

    public partial class AssetLayoutVM
    {
        public int asset_id { get; set; }
        public int catalog_id { get; set; }
        public int location_id { get; set; }
        public string asset_name { get; set; }
        public string serial_no { get; set; }
        public string ipv4 { get; set; }
        public string ipv6 { get; set; }
        public string install_user_name { get; set; }
        public Nullable<System.DateTime> install_date { get; set; }
        public int user_id { get; set; }
        public string remarks { get; set; }
        public string is_layout { get; set; }
        public Nullable<int> pos_x { get; set; }
        public Nullable<int> pos_y { get; set; }
        public System.DateTime last_updated { get; set; }
        public AssetTreeType type { get; set; }

        public Boolean is_changed { get; set; }
    }

    public partial class UserPortLayoutVM
    {
        public int user_port_layout_id { get; set; }
        public int asset_id { get; set; }
        public int port_no { get; set; }
        public string is_layout { get; set; }
        public Nullable<int> pos_x { get; set; }
        public Nullable<int> pos_y { get; set; }
        public byte[] last_updated { get; set; }
        public AssetTreeType type { get; set; }

        public Boolean is_changed { get; set; }
    }
    
    #endregion


    public enum RoomRackLayoutMode
    {
        NONE,
        ROOM_VIEW,
        ROOM_EDIT,
        RACK_VIEW,
        RACK_EDIT,
        ASSET_VIEW,
        ASSET_EDIT,
        USERPORT_VIEW,
        USERPORT_EDIT
    }
    /// <summary>
    /// RoomAndRackLayoutManager.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class RoomAndRackLayoutManager : MetroWindow
    {
        AssetTreeVM _ast_vm;
        AssetTreeVM selected_ast_vm;
        DrawingDataManager drawDataMgr;
        Size canvas_size;
        RoomRackLayoutMode layout_mode;

        String floor_drawing3d_file_path;

        RoomLayoutVM selected_rm_lvm;
        RackLayoutVM selected_rk_lvm;
        AssetLayoutVM selected_ast_lvm;
        UserPortLayoutVM selected_up_lvm;

        WebApiClientClass webapi;


        List<RoomLayoutVM> room_lvm_list = new List<RoomLayoutVM>();
        List<RackLayoutVM> rack_lvm_list = new List<RackLayoutVM>();
        List<RackLayoutVM> use_rack_lvm_list = new List<RackLayoutVM>();

        List<AssetLayoutVM> ast_lvm_list = new List<AssetLayoutVM>();
        List<AssetLayoutVM> use_ast_lvm_list = new List<AssetLayoutVM>();

        List<UserPortLayoutVM> up_lvm_list = new List<UserPortLayoutVM>();
        List<UserPortLayoutVM> use_up_lvm_list = new List<UserPortLayoutVM>();

        Drawing2D drawer2dWall;
        Drawing2D drawer2dRack;

        Boolean en_drag_item = false;
        Boolean is_edit = false;

        Point start_p;
        Point scl_last_p;                       // 스크롤 옵셋
        Point scl_offset =  new Point(0,0);


        public RoomAndRackLayoutManager(AssetTreeVM ast_vm)
        {
            InitializeComponent();

            drawDataMgr = new DrawingDataManager();
            drawer2dWall = new Drawing2D(_canvasWallDrawing);
            drawer2dRack = new Drawing2D(_canvasRackDrawing);
            drawer2dRack.setSubCanvas(_canvasRackDrawingSub);
            layout_mode = RoomRackLayoutMode.NONE;
            _ast_vm = ast_vm;

            webapi = new WebApiClientClass(g.web_api_uri_string);

            hideRoomLayout();
            //_ctlAssetTreeControl.customSelectedItemEvent += new AssetTreeControl.CustomSelectedItemHandler(SelectedAssetTreeViewItem);
            //_rectRoomBox.IsEnabled = true;
        }

        private void _window_Loaded(object sender, RoutedEventArgs e)
        {
            //전체 그리기 화면의 사이즈를 파악한다
            canvas_size.Width = _gridDrawing.ActualWidth;
            canvas_size.Height = _gridDrawing.ActualHeight;


            String drawing_3d_file_name;
            AssetTreeVM mainAstVM;

            floor fl;
            if (_ast_vm.type == AssetTreeType.Floor)
            {
                fl = g.floor_list.Find(at => at.floor_id == _ast_vm.type_id);
                mainAstVM = _ast_vm;
            }
            //선택되서 들어오는 asset_tree가 Room인 경우는 해당 룸위의 Floor를 찾아서 적용한다
            else
            {
                int fl_location_id = Etc.get_prev_location_id(_ast_vm.location_id);
                mainAstVM = g.location_ast_vm_dic[fl_location_id];
                fl = g.floor_list.Find(at => at.floor_id == mainAstVM.type_id);
            }
          


            //트리뷰에 원하는 형태의 asset_tree 바인딩
            initAssetTreeMgr();
            assetTreeBinding(mainAstVM);

             drawing_3d dr3d = g.drawing_3d_list.Find(at => at.drawing_3d_id == fl.drawing_3d_id);
            drawing_3d_file_name = string.Format("{0}drawing_3d/{1}", g.CLIENT_IMAGE_PATH, dr3d.file_name);

            List<room> tmp_room_list = g.room_list.FindAll(at => at.floor_id == fl.floor_id);
            foreach(var tmp_rm in tmp_room_list)
            {
                room_lvm_list.Add(makeRoomLayoutVM(tmp_rm));


                List<rack> tmp_rk_list = g.rack_list.FindAll(at => at.room_id == tmp_rm.room_id);
                List<RackLayoutVM> tmp_rk_lvm_list = new List<RackLayoutVM>();
                foreach(var tmp_rk in tmp_rk_list)
                {
                    tmp_rk_lvm_list.Add(makeRackLayoutVM(tmp_rk));
                }
                rack_lvm_list.AddRange(tmp_rk_lvm_list);

                //List<asset> tmp_ast_list = g.asset_list.FindAll(at => at.location_id == );
            }
           
         

            floor_drawing3d_file_path = drawing_3d_file_name;
            //층의 도면을 그려준다
            openDrawingFile(drawing_3d_file_name);

            //랙을 그려준다 ==> room이 선택된 경우에만 보여준다
            //drawRackList(rack_list);

            // 트리에서 헤당 로케이션 활성화 처리 romee
            foreach (AssetTreeVM ast_rm in _tvAssetTree.Items)
            {
                //현재 selected_ast_vm 이 위치한 룸의 TreeViewItem을 받아온다
                if (ast_rm.disp_name == _ast_vm.disp_name)
                {
                    TreeViewItem tvi_rm = (TreeViewItem)_tvAssetTree.ItemContainerGenerator.ContainerFromItem(ast_rm); // 해당 트리아이템 가져오기 
                    tvi_rm.IsSelected = true;
                }

            }

        }

        private Boolean checkIsInSameTree(AssetTreeVM ast1_vm, AssetTreeVM ast2_vm)
        {
            AssetTreeVM ast1_p_vm = getParentAssetTreeVM(ast1_vm.location_id, ast1_vm.disp_level);
            if (ast1_p_vm == null)
                return false;

            foreach(var ast_vm in ast1_p_vm.child_list)
            {
                if (ast_vm.type_id == ast2_vm.type_id)
                    return true;
            }
            return false;
        }


        //트리뷰에서 아이템을 선택한 경우에 호출되는 이벤트
        private void SelectedAssetTreeViewItem(AssetTreeVM sel_ast_vm)
        { 
            Boolean is_change_parent_in_tree = false;
            if(selected_ast_vm!=null)
                is_change_parent_in_tree = !checkIsInSameTree(selected_ast_vm, sel_ast_vm);


            selected_ast_vm = sel_ast_vm;

            if (sel_ast_vm == null) return;   // romee 2015.11.13 

            switch (sel_ast_vm.type)
            {
                case AssetTreeType.Room:
                    //location l1 = g.location_list.Find(at => at.location_id == sel_ast_vm.location_id);
                    //이전 렉과 Asset,UP를 지우고
                    drawer2dRack.removeAll();

                    //사용되는 asset,rack,userport 리스트를 모두 지운다
                    use_ast_lvm_list.Clear();
                    use_rack_lvm_list.Clear();
                    use_up_lvm_list.Clear();

                    changeLayoutMode(sel_ast_vm.type);
                    showRoomLayoutByAstVM(sel_ast_vm);
                    
                    selected_rm_lvm = room_lvm_list.Find(at=> at.room_id == sel_ast_vm.type_id);
                    selected_ast_lvm = null;
                    selected_rk_lvm = null;
                    selected_up_lvm = null;
                            
                    break;

                case AssetTreeType.Rack:
                    location l2 = g.location_list.Find(at => at.location_id == sel_ast_vm.location_id);

                    if ( (layout_mode != RoomRackLayoutMode.RACK_EDIT)&&(layout_mode != RoomRackLayoutMode.RACK_VIEW) )
                    {
                        AssetTreeVM p_ast_vm = getParentAssetTreeVM(sel_ast_vm.location_id, sel_ast_vm.disp_level);
                        changeLayoutMode(sel_ast_vm.type);
                        
                        //해당 룸을 표시한다
                        enableRoomLayout(false);
                        showRoomLayoutByAstVM(p_ast_vm);

                        //렉인경우 Asset은 Location ID가 room 의 location id 이므로 room 의 locationid 를 넣어준다
                        drawItemInRoom(l2.room_id, p_ast_vm.location_id);
                    }
                    selectRack(l2.rack_id ?? 0, true);

                    RackLayoutVM rk_lvm = rack_lvm_list.Find(at => at.rack_id == l2.rack_id);
                    selected_rk_lvm = rk_lvm;
                    selected_ast_lvm = null;
                    selected_rm_lvm = null;
                    selected_up_lvm = null;
                    break;

                case AssetTreeType.FacePlate:
                case AssetTreeType.ConsolidationPoint:
                case AssetTreeType.MutoaBox:
             
                    if (((layout_mode != RoomRackLayoutMode.ASSET_EDIT) && (layout_mode != RoomRackLayoutMode.ASSET_VIEW))
                        || is_change_parent_in_tree)
                    {
                        changeLayoutMode(sel_ast_vm.type);

                        //해당 룸을 표시한다
                        AssetTreeVM p_ast_vm3 = getParentAssetTreeVM(sel_ast_vm.location_id, sel_ast_vm.disp_level);
                        enableRoomLayout(false);
                        showRoomLayoutByAstVM(p_ast_vm3);

                        location l3 = g.location_list.Find(at => at.location_id == sel_ast_vm.location_id);

                        drawItemInRoom(l3.room_id, sel_ast_vm.location_id);
                    }
                    selectAsset(sel_ast_vm.asset_id ?? 0,false);

                    selected_rk_lvm = null;
                    selected_ast_lvm = ast_lvm_list.Find(at=> at.asset_id == sel_ast_vm.type_id);
                    selected_rm_lvm = null;
                    selected_up_lvm = null;

                    break;
                case AssetTreeType.UserPort:
                    changeLayoutMode(AssetTreeType.UserPort);

                    if (((layout_mode != RoomRackLayoutMode.USERPORT_EDIT) && (layout_mode != RoomRackLayoutMode.USERPORT_VIEW))
                        || is_change_parent_in_tree)
                    {
                        //해당 룸을 표시한다
                        AssetTreeVM p_ast_vm4 = getParentAssetTreeVM(sel_ast_vm.location_id, sel_ast_vm.disp_level);
                        enableRoomLayout(false);
                        showRoomLayoutByAstVM(p_ast_vm4);

                        location l4 = g.location_list.Find(at => at.location_id == sel_ast_vm.location_id);

                        drawItemInRoom(l4.room_id, sel_ast_vm.location_id);
                    }
                    //selectUserPort
                    selectUserPort(sel_ast_vm.type_id, false);

                    selected_rk_lvm = null;
                    selected_ast_lvm = null;
                    selected_rm_lvm = null;
                    selected_up_lvm = up_lvm_list.Find(at=> at.user_port_layout_id == sel_ast_vm.type_id);;
                    break;
            }
        }

        private void drawItemInRoom(Nullable<int> room_id, int location_id)
        {
            if (room_id != null)
            {
                drawer2dRack.removeAll();

                //해당 룸의 렉을 표시한다
                use_rack_lvm_list.Clear();
                use_rack_lvm_list = rack_lvm_list.FindAll(at => at.room_id == room_id);
                if(use_rack_lvm_list.Count!=0)
                    drawRackList(use_rack_lvm_list);

                //해당 룸의 Asset을 표시한다
                use_ast_lvm_list.Clear();
                use_ast_lvm_list = ast_lvm_list.FindAll(at => at.location_id == location_id);
                if (use_ast_lvm_list.Count != 0)
                    drawAssetList(use_ast_lvm_list);


                //해당 룸의 UserPort를 표시한다
                use_up_lvm_list.Clear();
                foreach (var use_ast_lvm in use_ast_lvm_list)
                {
                    List<UserPortLayoutVM> tmp_up_lvm_list = up_lvm_list.FindAll(at => at.asset_id == use_ast_lvm.asset_id);
                    use_up_lvm_list.AddRange(tmp_up_lvm_list);
                }
                if(use_up_lvm_list.Count!=0)
                    drawUserPortList(use_up_lvm_list);
            }
        }

        #region EditMode Select ToggleButton Event
        private void _tbtnEdit_Checked(object sender, RoutedEventArgs e)
        {
            is_edit = true;
            if (selected_ast_vm == null) return;

            changeLayoutMode(selected_ast_vm.type);
        }

        private void _tbtnEdit_Unchecked(object sender, RoutedEventArgs e)
        {
            is_edit = false;
            if (selected_ast_vm == null) return;

            changeLayoutMode(selected_ast_vm.type);
        }


        private void changeLayoutMode(AssetTreeType type)
        {

            switch (type)
            {
                case AssetTreeType.Room:
                    
                    if (is_edit)
                    {
                        clearDrawGuideLine();
                        enableRoomLayout(true);
                        layout_mode = RoomRackLayoutMode.ROOM_EDIT;
                    }
                    else
                    {
                        clearDrawGuideLine();
                        enableRoomLayout(false);
                        layout_mode = RoomRackLayoutMode.ROOM_VIEW;
                    }
                    break;

                case AssetTreeType.Rack:
                    //use_ast_lvm_list.Clear();
                    //use_up_lvm_list.Clear();
                    if (is_edit)
                    {
                        reDrawGuideLine(new Point(0, 0), new Point(canvas_size.Width, canvas_size.Height));
                        if (selected_rk_lvm != null)
                            drawer2dRack.selectEditRack(selected_rk_lvm.rack_id);
                        enableRoomLayout(false);
                        layout_mode = RoomRackLayoutMode.RACK_EDIT;
                    }
                    else
                    {
                        clearDrawGuideLine();

                        if (selected_rk_lvm != null)
                            drawer2dRack.releaseEditRack(selected_rk_lvm.rack_id);
                        enableRoomLayout(false);
                        layout_mode = RoomRackLayoutMode.RACK_VIEW;
                    }
                    break;


                case AssetTreeType.FacePlate:
                case AssetTreeType.ConsolidationPoint:
                case AssetTreeType.MutoaBox:
                    //use_rack_lvm_list.Clear();
                    //use_up_lvm_list.Clear();
                    if (is_edit)
                    {
                        reDrawGuideLine(new Point(0, 0), new Point(canvas_size.Width, canvas_size.Height));
                        if (selected_ast_lvm != null)
                            drawer2dRack.selectEditAsset(selected_ast_lvm.asset_id);
                        enableRoomLayout(false);
                        layout_mode = RoomRackLayoutMode.ASSET_EDIT;
                    }
                    else
                    {
                        clearDrawGuideLine();
                        if (selected_ast_lvm != null)
                            drawer2dRack.releaseEditAsset(selected_ast_lvm.asset_id);
                        enableRoomLayout(false);
                        layout_mode = RoomRackLayoutMode.ASSET_VIEW;
                    }

                    break;

                case AssetTreeType.UserPort:
                    //use_ast_lvm_list.Clear();
                    //use_rack_lvm_list.Clear();
                    if (is_edit)
                    {
                        reDrawGuideLine(new Point(0, 0), new Point(canvas_size.Width, canvas_size.Height));
                        if (selected_up_lvm != null)
                            drawer2dRack.selectEditUserPort(selected_up_lvm.user_port_layout_id);
                        enableRoomLayout(false);
                        layout_mode = RoomRackLayoutMode.USERPORT_EDIT;
                    }
                    else
                    {
                        clearDrawGuideLine();
                        if (selected_up_lvm != null)
                            drawer2dRack.releaseEditUserPort(selected_up_lvm.user_port_layout_id);
                        enableRoomLayout(false);
                        layout_mode = RoomRackLayoutMode.USERPORT_VIEW;
                    }



                    break;
            }


        }

        #endregion

        //draw2d에서 rack을 선택한 경우에 발생하는 이벤트
        private void SelectedItemInCanvas(int id, AssetTreeType type)
        {

            //1. 현재의 TreeView에서 선택된 아이템을 찾아 해제 한다
            Boolean ret = clearSelectedItemInTV();
            if(ret)
            {
                //2. Canvas에서 선택한 아이템을 찾아 IsSelect 해준다
                selectTreeViewItem(id, type);
            }
            

        }

        private Boolean selectTreeViewItem(int id, AssetTreeType type)
        {
            RoomLayoutVM rm_lvm = null;
            RackLayoutVM rk_lvm = null;
            AssetLayoutVM ast_lvm = null;
            UserPortLayoutVM up_lvm = null;


            switch (type)
            {
                case AssetTreeType.Rack:
                    rk_lvm = rack_lvm_list.Find(at=>at.rack_id == id);
                    if (rk_lvm == null) return false;

                    rm_lvm = room_lvm_list.Find(at => at.room_id == rk_lvm.room_id);
                    if (rm_lvm == null) return false;
                    
                    break;

                case AssetTreeType.FacePlate:
                case AssetTreeType.MutoaBox:
                case AssetTreeType.ConsolidationPoint:
                    ast_lvm = ast_lvm_list.Find(at => at.asset_id == id);
                    if (ast_lvm == null) return false;
                   
                    location l = g.location_list.Find(at => at.location_id == ast_lvm.location_id);
                    if (l == null) return false;
                   
                    rm_lvm = room_lvm_list.Find(at => at.room_id == l.room_id);
                    if (rm_lvm == null) return false;
                   
                    break;

                case AssetTreeType.UserPort:
                    up_lvm = up_lvm_list.Find(at => at.user_port_layout_id == id);
                    if (up_lvm == null) return false;
                   
                    ast_lvm = ast_lvm_list.Find(at => at.asset_id == up_lvm.asset_id);
                    if (ast_lvm == null) return false;
                   
                    location l2 = g.location_list.Find(at => at.location_id == ast_lvm.location_id);
                    if (l2 == null) return false;
                   
                    rm_lvm = room_lvm_list.Find(at => at.room_id == l2.room_id);
                    if (rm_lvm == null) return false;
                   
                    break;

                default:
                    return false;

            }


            //최상위(room) 아이템 부터 찾는다
            //foreach (AssetTreeVM ast_rm in _tvAssetTree.ItemContainerGenerator.Items)
            foreach (AssetTreeVM ast_rm in _tvAssetTree.Items)
            {
                //현재 selected_ast_vm 이 위치한 룸의 TreeViewItem을 받아온다
                if (ast_rm.type_id == rm_lvm.room_id)
                {
                    TreeViewItem tvi_rm = (TreeViewItem)_tvAssetTree.ItemContainerGenerator.ContainerFromItem(ast_rm);
                    //foreach (AssetTreeVM ast_lv2 in tvi_rm.ItemContainerGenerator.Items)
                    foreach (AssetTreeVM ast_lv2 in tvi_rm.Items)
                    {
                        //UserPort가 아닌 경우(rk,fp,cp....)에는 현재 레벨에서 선택된TreeViewItem을 찾는다
                        if (type != AssetTreeType.UserPort)
                        {
                            //Rack인경우
                            if (type == AssetTreeType.Rack)
                            {
                                if (ast_lv2.type_id == rk_lvm.rack_id)
                                {
                                    // TreeViewItem Select 
                                    TreeViewItem tvi_rk = (TreeViewItem)tvi_rm.ItemContainerGenerator.ContainerFromItem(ast_lv2);
                                    tvi_rk.IsSelected = true;
                                    return true;
                                }
                            }
                            else
                            {
                                if( ast_lv2.type_id == ast_lvm.asset_id)
                                {
                                    // TreeViewItem Select 
                                    TreeViewItem tvi_ast = (TreeViewItem)tvi_rm.ItemContainerGenerator.ContainerFromItem(ast_lv2);
                                    tvi_ast.IsSelected = true;
                                    return true;
                                }
                            }
                            
                        }
                        else
                        {
                            //AssetTreeVM의 type_id 는 Asset인 경우asset_id 이므로 
                            if (ast_lvm.asset_id == ast_lv2.type_id)
                            {
                                TreeViewItem tvi_ast = (TreeViewItem)tvi_rm.ItemContainerGenerator.ContainerFromItem(ast_lv2);
                                if (tvi_ast.IsExpanded == false)
                                {
                                    tvi_ast.IsExpanded = true;
                                    //현제까지 적용된 UI를 강재로 Action 시킨다
                                    Dispatcher.Invoke(new Action(() => { }), DispatcherPriority.ContextIdle, null);
                                }  

                                foreach (AssetTreeVM ast_lv3 in tvi_ast.Items)
                                {
                                    if (ast_lv3.type_id == up_lvm.user_port_layout_id)
                                    {
                                        TreeViewItem tvi_up = (TreeViewItem)tvi_ast.ItemContainerGenerator.ContainerFromItem(ast_lv3);
                                        try
                                        {
                                            tvi_up.IsSelected = true;
                                        }
                                        catch(Exception ex)
                                        {
                                            Console.WriteLine("{0}", ex.Message);
                                        }
                                        return true;
                                    }
                                }
                            }
                        }

                    }
                }

            }
            return false;
        }
 

        private Boolean clearSelectedItemInTV()
        {
            //1. 현재의 TreeView에서 선택된 아이템을 찾아 해제 한다
            if (selected_ast_vm != null)
            {
                location l = g.location_list.Find(at => at.location_id == selected_ast_vm.location_id);
                RoomLayoutVM rm_lvm = room_lvm_list.Find(at => at.room_id == l.room_id);

                //최상위(room) 아이템 부터 찾는다
                //foreach (AssetTreeVM ast_rm in _tvAssetTree.ItemContainerGenerator.Items)
                foreach (AssetTreeVM ast_rm in _tvAssetTree.Items)
                {
                    //현재 selected_ast_vm 이 위치한 룸의 TreeViewItem을 받아온다
                    if (ast_rm.type_id == rm_lvm.room_id)
                    {
                        TreeViewItem tvi_rm = (TreeViewItem)_tvAssetTree.ItemContainerGenerator.ContainerFromItem(ast_rm);
                        //foreach (AssetTreeVM ast_lv2 in tvi_rm.ItemContainerGenerator.Items)
                        foreach (AssetTreeVM ast_lv2 in tvi_rm.Items)
                        {
                            //UserPort가 아닌 경우(rk,fp,cp....)에는 현재 레벨에서 선택된TreeViewItem을 찾는다
                            if (selected_ast_vm.type != AssetTreeType.UserPort)
                            {
                                if (ast_lv2.type_id == selected_ast_vm.type_id)
                                {
                                    //해당 TreeViewItem을 unSelect 해준다
                                    TreeViewItem tvi = (TreeViewItem)tvi_rm.ItemContainerGenerator.ContainerFromItem(ast_lv2);
                                    tvi.IsSelected = false;
                                    return true;
                                }
                            }
                            else
                            {
                                //AssetTreeVM의 type_id 는 Asset인 경우asset_id 이므로 
                                if (selected_ast_vm.asset_id == ast_lv2.type_id)
                                {
                                    TreeViewItem tvi_ast = (TreeViewItem)tvi_rm.ItemContainerGenerator.ContainerFromItem(ast_lv2);

                                    foreach (AssetTreeVM ast_lv3 in tvi_ast.Items)
                                    {
                                        if (ast_lv3.type_id == selected_ast_vm.type_id)
                                        {
                                            TreeViewItem tvi_up = (TreeViewItem)tvi_ast.ItemContainerGenerator.ContainerFromItem(ast_lv3);
                                            tvi_up.IsSelected = false;
                                            return true;
                                        }
                                    }
                                }
                            }

                        }
                    }
                    
                }

            }
            return false;
        }



        #region AssetTreeControl

        List<AssetTreeVM> local_ast_vm_list = new List<AssetTreeVM>();

        Dictionary<int, AssetTreeVM> ast_vm_dic = new Dictionary<int, AssetTreeVM>();

        AssetTreeViewManager astMgr;

        private void initAssetTreeMgr()
        {
            astMgr = new AssetTreeViewManager();
        }

        private void assetTreeViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            //TreeView      
            TreeViewItem tvi = (TreeViewItem)_tvAssetTree.ItemContainerGenerator.ContainerFromIndex(_tvAssetTree.Items.CurrentPosition);
            tvi.IsExpanded = true;
        }

        public void assetTreeBinding(AssetTreeVM ast_vm_floor)
        {
            AssetTreeVM ast_vm = null;
            AssetTreeVM p_ast_vm;
            location fl_l = g.location_list.Find(at => at.location_id == ast_vm_floor.location_id);
            //floor fl = g.floor_list.Find(at => at.floor_id == fl_l.floor_id);
            int floor_id = fl_l.floor_id ?? 0;

            foreach (asset_tree ast in g.asset_tree_list.OrderBy(p => p.disp_order).OrderBy(p => p.disp_level))
            {
                location l = null;
                try 
                { 
                    ast_vm = astMgr.makeAssetTreeVM(ast);
                    l = g.location_list.Find(at => at.location_id == ast_vm.location_id);
                    if (l == null) // romee 2015.04.27 널체크 필요 트리뷰 모두 처리 필요 
                        continue;
                }
                catch(Exception e)
                {
                    Console.WriteLine("{0}", e.Message); 
                }

                //해당 floor_id에 포함된 것들 만을 추가한다
                if (floor_id == l.floor_id)
                {
                    // room 인경우
                    if (ast_vm.disp_level == ast_vm_floor.disp_level + 1)
                    {
                        local_ast_vm_list.Add(ast_vm);
                        ast_vm_dic.Add(ast_vm.location_id, ast_vm);
                    }

                    else if (ast_vm.disp_level > ast_vm_floor.disp_level + 1)
                    {
                        // rack 인 경우
                        if (ast_vm.asset_id == null)
                        {
                            p_ast_vm = getParentAssetTreeVM(ast_vm.location_id, ast_vm.disp_level);
                            if (p_ast_vm != null)
                            {
                                ast_vm.parant_ast_vm = p_ast_vm;
                                p_ast_vm.child_list.Add(ast_vm);
                                p_ast_vm.is_expander_visible = Visibility.Visible;

                                ast_vm_dic.Add(ast_vm.location_id, ast_vm);
                            }
                        }
                        //asset_id 가 있는 겅우 중  MB, CP, EB, FP 만 포함 
                        else
                        {
                            if ((ast_vm.type == AssetTreeType.MutoaBox) || (ast_vm.type == AssetTreeType.ConsolidationPoint)
                                || (ast_vm.type == AssetTreeType.FacePlate))
                            {
                                p_ast_vm = getParentAssetTreeVM(ast_vm.location_id, ast_vm.disp_level);
                                if (p_ast_vm != null)
                                {
                                    ast_vm.parant_ast_vm = p_ast_vm;
                                    p_ast_vm.child_list.Add(ast_vm);
                                    p_ast_vm.is_expander_visible = Visibility.Visible;

                                    //ast_vm_dic.Add(ast_vm.location_id, ast_vm);
                                    //Asset의 경우에만 특별히 여기서 리스트를 만든다
                                    asset tmp_ast = g.asset_list.Find(at => at.asset_id == ast_vm.asset_id);
                                    ast_lvm_list.Add(makeAssetlayoutVM(tmp_ast, ast_vm.type));

                                    //해당 Asset에 연결된 UserPort를 찾아서 Asset 아래에 추가하고 리스트에 추가한다
                                    List<user_port_layout> up_list = g.user_port_layout_list.FindAll(at => at.asset_id == ast_vm.asset_id);

                                    if (up_list.Count != 0)
                                    {
                                        foreach (var up in up_list)
                                        {
                                            Visibility expander_visible = Visibility.Hidden;
                                            AssetTreeVM up_ast_vm = new AssetTreeVM()
                                            {
                                                asset_id = up.asset_id,
                                                location_id = ast_vm.location_id,
                                                asset_tree_id = ast_vm.asset_tree_id,
                                                disp_name = string.Format("UserPort{0}", up.port_no),
                                                disp_level = ast_vm.disp_level,//부모의 displevel을 적용한다
                                                disp_order = up.port_no,
                                                is_expander_visible = expander_visible,
                                                image_file_path = string.Format("C:/Users/Administrator/Source/Workspaces/I2MS2/I2MS2/I2MS2/Icons/port_16.png"),
                                                type = AssetTreeType.UserPort,
                                                type_id = up.user_port_layout_id,
                                                check_view = Visibility.Hidden

                                            };
                                            ast_vm.child_list.Add(up_ast_vm);
                                            UserPortLayoutVM tmp_up_lvm = makeUserPortlayoutVM(up);
                                            up_lvm_list.Add(tmp_up_lvm);
                                        }
                                    }

                                }
                            }
                        }
                    }
                }

            }
            _tvAssetTree.ItemsSource = local_ast_vm_list;
        }

        private AssetTreeVM getAssetTreeVM(int id, AssetTreeType type)
        {
            switch (type)
            {
                case AssetTreeType.Room:
                    AssetTreeVM rm_ast_vm = local_ast_vm_list.Find(at => at.type_id == id);
                    if (rm_ast_vm != null)
                        return rm_ast_vm;
                    break;


                case AssetTreeType.Rack:
                    RackLayoutVM rk_lvm = rack_lvm_list.Find(at => at.rack_id == id);
                    if (rk_lvm == null) break;
                    
                    AssetTreeVM rm_ast = local_ast_vm_list.Find(at => at.type_id == rk_lvm.room_id);
                    if (rm_ast == null) break;
                    
                    AssetTreeVM rk_ast = rm_ast.child_list.Find(at => at.type_id == rk_lvm.rack_id);
                    if (rk_ast != null)
                        return rk_ast;
                    
                    break;

                case AssetTreeType.FacePlate:
                case AssetTreeType.MutoaBox:
                case AssetTreeType.ConsolidationPoint:
                    AssetLayoutVM ast_lvm = ast_lvm_list.Find(at => at.asset_id == id);
                    if (ast_lvm == null) break;
                    
                    location l = g.location_list.Find(at => at.location_id == ast_lvm.location_id);
                    AssetTreeVM rm_ast2 = local_ast_vm_list.Find(at => at.type_id == l.room_id);
                    if (rm_ast2 == null) break;
                    
                    AssetTreeVM ast_ast = rm_ast2.child_list.Find(at => at.type_id == ast_lvm.asset_id);
                    if (ast_ast != null)
                       return ast_ast;
                    
                    break;

                case AssetTreeType.UserPort:
                    UserPortLayoutVM up_lvm = up_lvm_list.Find(at => at.user_port_layout_id == id);
                    if (up_lvm == null) break;

                    AssetLayoutVM ast_lvm2 = ast_lvm_list.Find(at => at.asset_id == up_lvm.asset_id);
                    if (ast_lvm2 == null) break;

                    location l2 = g.location_list.Find(at => at.location_id == ast_lvm2.location_id);
                    AssetTreeVM rm_ast3 = local_ast_vm_list.Find(at => at.type_id == l2.room_id);
                    if (rm_ast3 == null) break;

                    AssetTreeVM ast_ast2 = rm_ast3.child_list.Find(at => at.type_id == ast_lvm2.asset_id);
                    if (ast_ast2 == null) break;


                    AssetTreeVM up_ast = ast_ast2.child_list.Find(at => at.type_id == up_lvm.user_port_layout_id);
                    if (up_ast != null)
                        return up_ast;

                    break;

                default:
                    break;

            }
            return null;
        }

        private AssetTreeVM getParentAssetTreeVM(int location_id, int disp_level)
        {
            location l = g.location_list.Find(at => at.location_id == location_id);

            int p_level = disp_level - 1;
            location p_l;
            switch (p_level)
            {
                case 1:
                    region1 r1 = g.region1_list.Find(at => at.region1_id == l.region1_id);
                    p_l = g.location_list.Find(at =>
                        (at.region1_id == r1.region1_id) && (at.region2_id == null));

                    break;
                case 2:
                    region2 r2 = g.region2_list.Find(at => at.region2_id == l.region2_id);
                    p_l = g.location_list.Find(at =>
                        (at.region2_id == r2.region2_id) && (at.site_id == null));
                    break;
                case 3:
                    site s = g.site_list.Find(at => at.site_id == l.site_id);
                    p_l = g.location_list.Find(at =>
                        (at.site_id == s.site_id) && (at.building_id == null));
                    break;
                case 4:
                    building bd = g.building_list.Find(at => at.building_id == l.building_id);
                    p_l = g.location_list.Find(at =>
                        (at.building_id == bd.building_id) && (at.floor_id == null));
                    break;
                case 5:
                    floor fl = g.floor_list.Find(at => at.floor_id == l.floor_id);
                    p_l = g.location_list.Find(at =>
                        (at.floor_id == fl.floor_id) && (at.room_id == null));
                    break;
                case 6:
                    room rm = g.room_list.Find(at => at.room_id == l.room_id);
                    p_l = g.location_list.Find(at =>
                        (at.room_id == rm.room_id) && (at.rack_id == null));
                    break;
                case 7:
                    rack rk = g.rack_list.Find(at => at.rack_id == l.rack_id);
                    p_l = g.location_list.Find(at =>
                        (at.rack_id == rk.rack_id));
                    break;
                default:
                    p_l = null;
                    break;
            }



            try
            {
                AssetTreeVM P_ast_vm = ast_vm_dic[p_l.location_id];

                return P_ast_vm;
            }
            catch (Exception ex) { Console.WriteLine("{0}", ex.Message); return null; }

        }

        private AssetTreeVM getAssetTreeVM(AssetTreeVM ast_vm_tv)
        {
            AssetTreeVM p_ast_vm = getParentAssetTreeVM(ast_vm_tv.location_id, ast_vm_tv.disp_level);
            foreach (AssetTreeVM tmp_ast_vm in p_ast_vm.child_list)
            {
                if (tmp_ast_vm.asset_tree_id == ast_vm_tv.asset_tree_id)
                    return tmp_ast_vm;
            }
            return null;
        }


        //TreeViewItem 아래에 있는 TreeViewItem을 index로 찾아준다
        private TreeViewItem findChildItem(TreeViewItem tvi, int id)
        {
            TreeViewItem item = null;
            try
            {
                item = (TreeViewItem)tvi.ItemContainerGenerator.ContainerFromIndex(id);
            }
            catch (Exception) { }
            return item;
        }

        private void _tvAssetTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (_tvAssetTree.SelectedItem is AssetTreeVM)
                SelectedAssetTreeViewItem((AssetTreeVM)_tvAssetTree.SelectedItem);
        }
      
        #endregion

        #region RoomLayoutControl

        Point room_selected_point;
        bool room_drag_flag = false;
        bool room_size_flag = false;
        bool room_point_drag_flag = false;

        Double room_point_margin_top;
        Double room_point_margin_left;


        #region RoomLayout Control Hide,Show,Enable Method
        
        private void showRoomLayoutByAstVM(AssetTreeVM ast_vm)
        {
            hideRoomLayout();
            location l = g.location_list.Find(at => at.location_id == ast_vm.location_id);
            RoomLayoutVM rm_lvm = room_lvm_list.Find(at => at.room_id == l.room_id);
            
            if (rm_lvm != null)
            {
                selected_rm_lvm = rm_lvm;

                Double x1 = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, rm_lvm.square_x1 ?? 0);
                Double x2 = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, rm_lvm.square_x2 ?? 0);
                Double y1 = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, rm_lvm.square_y1 ?? 0);
                Double y2 = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, rm_lvm.square_y2 ?? 0);

                Point p = new Point(x1, y1);
                Size s = new Size(Math.Abs(x2 - x1), Math.Abs(y2 - y1));

                Double f_x = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, rm_lvm.flag_x ?? 0);
                Double f_y = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, rm_lvm.flag_y ?? 0);

                Point f_p = new Point(f_x, f_y);

                showRoomLayout(p, s, f_p);
            }
        }

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

            _gridRoomLayout.HorizontalAlignment = HorizontalAlignment.Stretch;
            _gridRoomLayout.VerticalAlignment = VerticalAlignment.Stretch;
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

        #region RoomLayout Mouse Event
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

        private void _rectRoomBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point selectP = e.GetPosition(_rectRoomBox);
            room_selected_point = new Point(selectP.X, selectP.Y);
            room_drag_flag = true;

        }

        private void _rmLayoutCtlright_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point selectP = e.GetPosition(_rectRoomBox);

            room_selected_point = new Point(selectP.X, selectP.Y);
            room_size_flag = true;
            e.Handled = true;
        }

        private void _gridRoomLayout_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (layout_mode == RoomRackLayoutMode.ROOM_EDIT)
            {
                room_drag_flag = false;
                room_size_flag = false;
                room_point_drag_flag = false;

                int new_x1 = (int)drawDataMgr.getVMValue_FromCanvasValue(canvas_size, _rectRoomBox.Margin.Left);
                int new_x2 = (int)drawDataMgr.getVMValue_FromCanvasValue(canvas_size, _rectRoomBox.Margin.Left + _rectRoomBox.Width);
                int new_y1 = (int)drawDataMgr.getVMValue_FromCanvasValue(canvas_size, _rectRoomBox.Margin.Top);
                int new_y2 = (int)drawDataMgr.getVMValue_FromCanvasValue(canvas_size, _rectRoomBox.Margin.Top + _rectRoomBox.Height);

                int new_flag_x = (int)drawDataMgr.getVMValue_FromCanvasValue(canvas_size, _gridRoomNamePoint.Margin.Left);
                int new_flag_y = (int)drawDataMgr.getVMValue_FromCanvasValue(canvas_size, _gridRoomNamePoint.Margin.Top);

                if( (selected_rm_lvm.square_x1 != new_x1)
                    ||(selected_rm_lvm.square_x2 != new_x2)
                    ||(selected_rm_lvm.square_y1 != new_y1)
                    ||(selected_rm_lvm.square_y2 != new_y2)
                    ||(selected_rm_lvm.flag_x != new_flag_x)
                    ||(selected_rm_lvm.flag_y != new_flag_y))
                {
                    selected_rm_lvm.square_x1 = new_x1;
                    selected_rm_lvm.square_x2 = new_x2;
                    selected_rm_lvm.square_y1 = new_y1;
                    selected_rm_lvm.square_y2 = new_y2;

                    selected_rm_lvm.flag_x = new_flag_x;
                    selected_rm_lvm.flag_y = new_flag_y;

                    selected_rm_lvm.is_changed = true;
                    _ellipseRoomNamePoint.Fill = Brushes.Gray;
                    selected_ast_vm.check_view = Visibility.Visible;
                    selected_ast_vm.force_changed = true;


                    foreach(var child in selected_ast_vm.child_list)
                    {
                        switch(child.type)
                        {
                            case AssetTreeType.Rack:
                                RackLayoutVM rk_lvm = rack_lvm_list.Find(at => at.rack_id == child.type_id);
                                rk_lvm.is_changed = true;
                                rk_lvm.pos_x = null;
                                rk_lvm.pos_y = null;
                                child.check_view = Visibility.Visible;

                                break;

                            case AssetTreeType.FacePlate:
                            case AssetTreeType.ConsolidationPoint:
                            case AssetTreeType.MutoaBox:
                                AssetLayoutVM ast_lvm = ast_lvm_list.Find(at => at.asset_id == child.type_id);
                                ast_lvm.is_changed = true;
                                ast_lvm.pos_x = null;
                                ast_lvm.pos_y = null;
                                child.check_view = Visibility.Visible;
                                
                                foreach(var up_ast in child.child_list)
                                {
                                    if(up_ast.type == AssetTreeType.UserPort)
                                    {
                                        UserPortLayoutVM up_lvm = up_lvm_list.Find(at => at.user_port_layout_id == up_ast.type_id);
                                        up_lvm.is_changed = true;
                                        up_lvm.pos_x = null;
                                        up_lvm.pos_y = null;
                                        up_lvm.is_layout = "N";
                                        up_ast.check_view = Visibility.Visible;
                                        //drawer2dRack.removeUserPort(up_lvm.user_port_layout_id);
                                    }
                                }
                                
                                break;
                        }
                    }


                    foreach(var rk_lvm in rack_lvm_list)
                    {
                        if(rk_lvm.room_id == selected_rm_lvm.room_id)
                        {
                            rk_lvm.is_changed = true;
                            rk_lvm.pos_x = null;
                            rk_lvm.pos_y = null;
                        }
                    } 
                    
                    foreach (var ast_lvm in ast_lvm_list)
                    {
                        if(ast_lvm.location_id == selected_ast_vm.location_id)
                        {
                            ast_lvm.is_changed = true;
                            ast_lvm.pos_x = null;
                            ast_lvm.pos_y = null;

                        }
                    }

                }
            }
        }


        // 4:3 비율의 사이즈를 유지한다.
        private void _gridRoomLayout_MouseMove(object sender, MouseEventArgs e)
        {
            if (room_size_flag)
            {
                Point new_point = e.GetPosition(_rectRoomBox);
               
                double x_move = new_point.X - room_selected_point.X;

                double width = _rectRoomBox.Width + x_move;
                if (width < 80)
                    width = 80;
                if (width > (_gridDrawing.ActualWidth - _rectRoomBox.Margin.Left))
                {
                    width = _gridDrawing.ActualWidth - _rectRoomBox.Margin.Left;

                }

                double height = width * 0.75;
                if (height < 60)
                    height = 60;
                if (height > (_gridDrawing.ActualHeight - _rectRoomBox.Margin.Top))
                {
                    height = _gridDrawing.ActualHeight - _rectRoomBox.Margin.Top;
                    width = height * (4.0 / 3.0);

                }
                _rectRoomBox.Width = width > 0 ? width : 0;
                _rectRoomBox.Height = height > 0 ? height : 0;

                room_selected_point = new Point(new_point.X, new_point.Y);
                e.Handled = true;


                Thickness name_old_th = _gridRoomNamePoint.Margin;
                Thickness name_new_th = new Thickness(0, 0, 0, 0);
                name_new_th = name_old_th;
                if ( Width > 80)
                {
                    Double name_x_move = x_move / 2;
                    Double new_left = name_old_th.Left + name_x_move;
                    if (_rectRoomBox.Margin.Left - new_left < 0)
                        name_new_th.Left = new_left > 0 ? new_left : name_old_th.Left;
                }
                
                if (height > 60)
                {
                    Double name_y_move = x_move * (3.0 / 4.0) / 2;
                    Double new_top = name_old_th.Top + name_y_move;
                    if (_rectRoomBox.Margin.Top - new_top < 0)
                        name_new_th.Top = new_top > 0 ? new_top : name_old_th.Top;
                }
                
                _gridRoomNamePoint.Margin = name_new_th;
                room_point_margin_left = _gridRoomNamePoint.Margin.Left - _rectRoomBox.Margin.Left;
                room_point_margin_top = _gridRoomNamePoint.Margin.Top - _rectRoomBox.Margin.Top;
             //   Console.WriteLine("move_x:{0}, old:({1},{2}), new:({3},{4})", name_x_move, name_old_th.Left, name_old_th.Top, name_new_th.Left, name_new_th.Top);
            }

            if (room_drag_flag)
            {
                Point new_point = e.GetPosition(_rectRoomBox);
                Thickness th = _rectRoomBox.Margin;

                double x_move = new_point.X - room_selected_point.X;
                double x = th.Left + x_move;
                if (x > (_gridDrawing.ActualWidth - _rectRoomBox.Width))
                    x = _gridDrawing.ActualWidth - _rectRoomBox.Width;
                th.Left = x > 0 ? x : 0;

                double y_move = new_point.Y - room_selected_point.Y;
                double y = th.Top + y_move;
                if (y > (_gridDrawing.ActualHeight - _rectRoomBox.Height))
                    y = _gridDrawing.ActualHeight - _rectRoomBox.Height;
                th.Top = y > 0 ? y : 0;

                _rectRoomBox.Margin = th;

                Thickness el_th = new Thickness(th.Left + room_point_margin_left, th.Top + room_point_margin_top, 0, 0);
                _gridRoomNamePoint.Margin = el_th;
                //Console.WriteLine("drag_flag NameMargin:({0},{1})",_gridNamePoint.Margin.Left, _gridNamePoint.Margin.Top);

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

            Console.WriteLine("{0}{1}", p.X, p.Y);
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


        #endregion


        #region draw2D Method
        public Boolean openDrawingFile(string path)
        {
            drawer2dWall.removeAllWall();

            if (File.Exists(path))
            {
                BinaryFormatter openformat = new BinaryFormatter();
                FileStream openStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                List<WallDraw>[] open_w_list = new List<WallDraw>[4];

                open_w_list = (List<WallDraw>[])openformat.Deserialize(openStream);

                for (int i = 0; i < 4; i++)
                {
                    foreach (var w in open_w_list[i])
                    {
                        addWall(w);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
 
        private void selectRack(int rack_id, Boolean force_change)
        {
            if(selected_rk_lvm== null)
            {
                selected_rk_lvm = use_rack_lvm_list.Find(at => at.rack_id == rack_id);
                drawer2dRack.selectRack(rack_id);
                if (is_edit)
                    drawer2dRack.selectEditRack(rack_id);
            }
            else if(  (selected_rk_lvm.rack_id != rack_id) || force_change)
            {
                drawer2dRack.releaseRack(selected_rk_lvm.rack_id);

                selected_rk_lvm = use_rack_lvm_list.Find(at => at.rack_id == rack_id);
                drawer2dRack.selectRack(rack_id);
                if (is_edit)
                    drawer2dRack.selectEditRack(rack_id);
            }
        }


        private void selectAsset(int asset_id, Boolean force_change)
        {
            if(selected_ast_lvm== null)
            {
                selected_ast_lvm = use_ast_lvm_list.Find(at => at.asset_id == asset_id);
                drawer2dRack.selectAsset(asset_id);
                if (is_edit)
                    drawer2dRack.selectEditAsset(asset_id);
            }
            else if((selected_ast_lvm.asset_id != asset_id) || force_change)
            {
                drawer2dRack.releaseAsset(selected_ast_lvm.asset_id);
               
                selected_ast_lvm = use_ast_lvm_list.Find(at => at.asset_id == asset_id);
                drawer2dRack.selectAsset(asset_id);
                if (is_edit)
                    drawer2dRack.selectEditAsset(asset_id);
            }
        }


        private void selectUserPort(int up_id, Boolean force_change)
        {
            if (selected_up_lvm == null)
            {
                selected_up_lvm = use_up_lvm_list.Find(at => at.user_port_layout_id == up_id);
                drawer2dRack.selectUserPort(up_id);
                if (is_edit)
                    drawer2dRack.selectEditUserPort(up_id);
            }
            else if ((selected_up_lvm.user_port_layout_id != up_id) || force_change)
            {
                drawer2dRack.releaseUserPort(selected_up_lvm.user_port_layout_id);

                selected_up_lvm = use_up_lvm_list.Find(at => at.user_port_layout_id == up_id);
                drawer2dRack.selectUserPort(up_id);
                if (is_edit)
                    drawer2dRack.selectEditUserPort(up_id);
            }
        }



        public void addWall(WallDraw db_w)
        {
            Point s_p = drawDataMgr.getCanvasPoint_FromVMPoint(canvas_size, db_w.start_p);
            Point e_p = drawDataMgr.getCanvasPoint_FromVMPoint(canvas_size, db_w.end_p);
            Double t = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, db_w.thickness);
            Double h = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, db_w.height);
            WallDraw w_c = new WallDraw()
            {
                start_p = s_p,
                end_p = e_p,
                thickness = t,
                height = h,
                alpha = db_w.alpha,
                colorA = db_w.colorA,
                colorR = db_w.colorR,
                colorG = db_w.colorG,
                colorB = db_w.colorB
            };
            drawer2dWall.addWallbyWall(w_c);
        }

        public void drawRackList(List<RackLayoutVM> rk_list)
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
                    AssetTreeVM rk_ast = getAssetTreeVM(rk.rack_id, AssetTreeType.Rack);
                    rk_ast.check_view = Visibility.Visible;
                    rk_ast.force_changed = true;
                }
                else
                {
                    p = drawDataMgr.getCanvasPoint_FromVMPoint(canvas_size, new Point(rk.pos_x ?? 0, rk.pos_y ?? 0));
                    p2 = ToSimplePoint(p);
                }
                Size s = drawDataMgr.getCanvasSize_FromVMSize(canvas_size, new Size(g.RACK_SIZE_WIDTH, g.RACK_SIZE_HEIGHT));
                Double h = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, g.RACK_HEIGHT);
                Color c = Colors.SkyBlue;
                drawer2dRack.addRack(p2, s, h, c, rk.rack_id, rk.rack_name);

                
            }
        }


        public void drawAssetList(List<AssetLayoutVM> ast_list)
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
                    AssetTreeVM ast_ast = getAssetTreeVM(ast.asset_id, ast.type);
                    if (ast_ast != null)
                    {
                        ast_ast.check_view = Visibility.Visible;
                        ast_ast.force_changed = true;
                    }
                }
                else
                {
                    p = drawDataMgr.getCanvasPoint_FromVMPoint(canvas_size, new Point(ast.pos_x ?? 0, ast.pos_y ?? 0));
                    p2 = ToSimplePoint(p);
                }
                

                Size s = drawDataMgr.getCanvasSize_FromVMSize(canvas_size, new Size(g.ASSET_SIZE_WIDTH, g.ASSET_SIZE_HEIGHT));
                Double h = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, g.ASSET_SIZE_HEIGHT);
                
                drawer2dRack.addAsset(p2, s, h, ast.type, ast.asset_id, ast.asset_name);
               

            }
        }


        public void drawUserPortList(List<UserPortLayoutVM> draw_up_lvm_list)
        {
            foreach (var up_lvm in draw_up_lvm_list)
            {
                if(up_lvm.is_layout== "Y")
                {
                    Point p = drawDataMgr.getCanvasPoint_FromVMPoint(canvas_size, new Point(up_lvm.pos_x ?? 0, up_lvm.pos_y ?? 0));

                    Size s = drawDataMgr.getCanvasSize_FromVMSize(canvas_size, new Size(g.USERPORT_RADIUS * 2, g.USERPORT_RADIUS * 2));
                    Double h = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, g.USERPORT_HEIGHT);

                    AssetLayoutVM parent_ast_lvm = ast_lvm_list.Find(at => at.asset_id == up_lvm.asset_id);
                    Point parent_p = drawDataMgr.getCanvasPoint_FromVMPoint(canvas_size, new Point(parent_ast_lvm.pos_x ?? 0, parent_ast_lvm.pos_y ?? 0));
                    Size parent_s = drawDataMgr.getCanvasSize_FromVMSize(canvas_size, new Size(g.ASSET_SIZE_WIDTH, g.ASSET_SIZE_HEIGHT));
                    
                    drawer2dRack.addUserPort(p,s,up_lvm.user_port_layout_id, up_lvm.port_no, parent_p, parent_s);

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
        private room makeRoom(RoomLayoutVM rm_lvm)
        {
            return new room()
            {
                room_id = rm_lvm.room_id,
                floor_id = rm_lvm.floor_id,
                room_name = rm_lvm.room_name,
                square_x1 = rm_lvm.square_x1,
                square_x2 = rm_lvm.square_x2,
                square_y1 = rm_lvm.square_y1,
                square_y2 = rm_lvm.square_y2,
                user_id = rm_lvm.user_id,
                last_updated = rm_lvm.last_updated,
                remarks = rm_lvm.remarks,
                flag_x = rm_lvm.flag_x,
                flag_y = rm_lvm.flag_y
            };
        }

        private RoomLayoutVM makeRoomLayoutVM(room rm)
        {
            return new RoomLayoutVM()
            {
                room_id = rm.room_id,
                floor_id = rm.floor_id,
                room_name = rm.room_name,
                square_x1 = rm.square_x1,
                square_x2 = rm.square_x2,
                square_y1 = rm.square_y1,
                square_y2 = rm.square_y2,
                user_id = rm.user_id,
                last_updated = rm.last_updated,
                remarks = rm.remarks,
                flag_x = rm.flag_x,
                flag_y = rm.flag_y,
                is_changed = false
            };
        }

        private RackLayoutVM makeRackLayoutVM(rack rk)
        {
            return new RackLayoutVM
            {
                room_id = rk.room_id,
                rack_id = rk.rack_id,
                rack_name = rk.rack_name,
                pos_x = rk.pos_x,
                pos_y = rk.pos_y,
                rack_catalog_id = rk.rack_catalog_id,
                rack_no = rk.rack_no,
                rack_row = rk.rack_row,
                angle = rk.angle,
                vcm_l_catalog_id = rk.vcm_l_catalog_id,
                vcm_r_catalog_id = rk.vcm_r_catalog_id,
                user_id = rk.user_id,
                last_updated = rk.last_updated,
                remarks = rk.remarks,
                is_changed = false
            };
        }

        private rack makeRack(RackLayoutVM rk_lvm)
        {
            return new rack()
            {
                room_id = rk_lvm.room_id,
                rack_id = rk_lvm.rack_id,
                rack_name = rk_lvm.rack_name,
                pos_x = rk_lvm.pos_x,
                pos_y = rk_lvm.pos_y,
                rack_catalog_id = rk_lvm.rack_catalog_id,
                rack_no = rk_lvm.rack_no,
                rack_row = rk_lvm.rack_row,
                angle = rk_lvm.angle,
                vcm_l_catalog_id = rk_lvm.vcm_l_catalog_id,
                vcm_r_catalog_id = rk_lvm.vcm_r_catalog_id,
                user_id = rk_lvm.user_id,
                last_updated = rk_lvm.last_updated,
                remarks = rk_lvm.remarks
            };
        }

        private AssetLayoutVM makeAssetlayoutVM(asset ast, AssetTreeType ast_type)
        {
            return new AssetLayoutVM()
            {
                asset_id = ast.asset_id,
                catalog_id = ast.catalog_id,
                location_id = ast.location_id,
                asset_name = ast.asset_name,
                serial_no = ast.serial_no,
                ipv4 = ast.ipv4,
                ipv6 = ast.ipv6,
                install_date = ast.install_date,
                install_user_name = ast.install_user_name,
                user_id = ast.user_id,
                last_updated = ast.last_updated,
                remarks = ast.remarks,
                is_layout = ast.is_layout,
                pos_x = ast.pos_x,
                pos_y = ast.pos_y,
                type = ast_type
            };
        }

        private asset makeAsset(AssetLayoutVM ast_lvm)
        {
            return new asset()
            {
                asset_id = ast_lvm.asset_id,
                catalog_id = ast_lvm.catalog_id,
                location_id = ast_lvm.location_id,
                asset_name = ast_lvm.asset_name,
                serial_no = ast_lvm.serial_no,
                ipv4 = ast_lvm.ipv4,
                ipv6 = ast_lvm.ipv6,
                install_date = ast_lvm.install_date,
                install_user_name = ast_lvm.install_user_name,
                user_id = ast_lvm.user_id,
                last_updated = ast_lvm.last_updated,
                remarks = ast_lvm.remarks,
                is_layout = ast_lvm.is_layout,
                pos_x = ast_lvm.pos_x,
                pos_y = ast_lvm.pos_y,
            };
        }


        private UserPortLayoutVM makeUserPortlayoutVM(user_port_layout up)
        {
            return  new UserPortLayoutVM()
            {
                user_port_layout_id = up.user_port_layout_id,
                asset_id = up.asset_id,
                port_no = up.port_no,
                pos_x = up.pos_x,
                pos_y = up.pos_y,
                is_layout = up.is_layout,
                last_updated = up.last_updated,
                type = AssetTreeType.UserPort
            };

        }

        private user_port_layout makeUserPort(UserPortLayoutVM up_lvm)
        {
            return new user_port_layout()
            {
                user_port_layout_id = up_lvm.user_port_layout_id,
                asset_id = up_lvm.asset_id,
                port_no = up_lvm.port_no,
                is_layout = up_lvm.is_layout,
                pos_x = up_lvm.pos_x,
                pos_y = up_lvm.pos_y,
                last_updated = up_lvm.last_updated
                
            };
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

        private void canvasZoom(Double ratio, Point center_p)
        {
            Double w = _gridDrawing.ActualWidth;
            Double h = _gridDrawing.ActualHeight;

            Double scX = _sclDrawing.ContentHorizontalOffset;
            Double scY = _sclDrawing.ContentVerticalOffset;

            _gridDrawing.Width = w * ratio;
            _gridDrawing.Height = h * ratio;

            //_canvasWallDrawing.Width = w * ratio;
            //_canvasWallDrawing.Height = h * ratio;

            //_gridGuide.Width = w * ratio;
            //_gridGuide.Height = h * ratio;

            canvas_size = new Size(w * ratio, h * ratio);
            reDrawGuideLine(new Point(0, 0), new Point(w * ratio, h * ratio));
            
            
            if (ratio > 1)
            {
                _sclDrawing.ScrollToHorizontalOffset(scX * ratio + 512);
                _sclDrawing.ScrollToVerticalOffset(scY * ratio + 384);
            }
            else
            {
                _sclDrawing.ScrollToHorizontalOffset((scX - 512) * ratio);
                _sclDrawing.ScrollToVerticalOffset((scY - 384) * ratio);
            }
            reDrawAll();
        }

        private void reDrawAll()
        {
            canvas_size = new Size(_gridDrawing.Width, _gridDrawing.Height);
            openDrawingFile(floor_drawing3d_file_path);

            if (selected_ast_vm != null)
            {
                location l = g.location_list.Find(at => at.location_id == selected_ast_vm.location_id);

                if (selected_ast_vm.type == AssetTreeType.Room)
                {
                    showRoomLayoutByAstVM(selected_ast_vm);
                    changeLayoutMode(AssetTreeType.Room);
                }
                else
                {   
                    //해당 룸을 표시한다
                    AssetTreeVM p_ast_vm = getParentAssetTreeVM(selected_ast_vm.location_id, selected_ast_vm.disp_level);
                    
                    showRoomLayoutByAstVM(p_ast_vm);

                    //이전 렉, 에셋을 지우고
                    drawer2dRack.removeAll();
                    
                    //해당 룸의 렉을 표시한다
                    drawRackList(use_rack_lvm_list);

                    //해당 룸의 Asset을 표시한다
                    drawAssetList(use_ast_lvm_list);

                    //해당 룸의 UserPort를 표시한다
                    drawUserPortList(use_up_lvm_list);

                    if (selected_rk_lvm != null)
                    {
                        selectRack(selected_ast_vm.type_id, true);
                        changeLayoutMode(AssetTreeType.Rack);
                    }
                    else if(selected_ast_lvm != null)
                    {
                        selectAsset(selected_ast_lvm.asset_id, true);
                        changeLayoutMode(selected_ast_lvm.type);
                    }
                    else if(selected_up_lvm != null)
                    {
                        selectUserPort(selected_up_lvm.user_port_layout_id, true);
                        changeLayoutMode(AssetTreeType.UserPort);
                    }
                }
            }
        }      
        #endregion


        #region MouseEvent

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
                            drawer2dRack.moveRack(new Vector(simp_m_x, simp_m_y), selected_rk_lvm.rack_id);
                            break;
                        case RoomRackLayoutMode.ASSET_EDIT:
                            drawer2dRack.moveAsset(new Vector(simp_m_x, simp_m_y), selected_ast_lvm.asset_id);
                            break;
                        case RoomRackLayoutMode.USERPORT_EDIT:
                            drawer2dRack.moveUserPort(new Vector(simp_m_x, simp_m_y), selected_up_lvm.user_port_layout_id);
                            break;
                    }
                    start_p = cur_p;
                }
                else if(!is_edit)
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
        } 

        private void _gridDrawing_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (en_drag_item == true)
            {
                en_drag_item = false;

                switch (layout_mode)
                {
                    case RoomRackLayoutMode.RACK_EDIT:
                        Point rk_p = drawer2dRack.getRackPoint(selected_rk_lvm.rack_id);

                        int new_x = (int)drawDataMgr.getVMValue_FromCanvasValue(canvas_size, rk_p.X);
                        int new_y = (int)drawDataMgr.getVMValue_FromCanvasValue(canvas_size, rk_p.Y);

                        if ((selected_rk_lvm.pos_x != new_x) || (selected_rk_lvm.pos_y != new_y))
                        {
                            //check in the room
                            Boolean is_in_room = IsInTheRoom(selected_rk_lvm.room_id, new Point(new_x, new_y));
                            if (is_in_room == false) break;

                            //temp save  change rack layout  
                            selected_rk_lvm.pos_x = new_x;
                            selected_rk_lvm.pos_y = new_y;
                            selected_rk_lvm.is_changed = true;
                            selected_ast_vm.check_view = Visibility.Visible;
                            selected_ast_vm.force_changed = true;
                        }
                        break;

                    case RoomRackLayoutMode.ASSET_EDIT:
                        Point ast_p = drawer2dRack.getAssetPoint(selected_ast_lvm.asset_id);

                        int new_x2 = (int)drawDataMgr.getVMValue_FromCanvasValue(canvas_size, ast_p.X);
                        int new_y2 = (int)drawDataMgr.getVMValue_FromCanvasValue(canvas_size, ast_p.Y);

                        if ((selected_ast_lvm.pos_x != new_x2) || (selected_ast_lvm.pos_y != new_y2))
                        {
                            //check in the room
                            location l = g.location_list.Find(at => at.location_id == selected_ast_lvm.location_id);
                            if (l == null) break;
                            Boolean is_in_room = IsInTheRoom(l.room_id ?? 0, new Point(new_x2, new_y2));
                            if (is_in_room == false) break;

                            //temp save  change asset layout  
                            selected_ast_lvm.pos_x = new_x2;
                            selected_ast_lvm.pos_y = new_y2;
                            selected_ast_lvm.is_changed = true;
                            selected_ast_vm.check_view = Visibility.Visible;
                            selected_ast_vm.force_changed = true;

                            List<UserPortLayoutVM> tmp_up_lvm_list = up_lvm_list.FindAll(at => at.asset_id == selected_ast_lvm.asset_id);
                            int count = 0;
                            Double term_vm = g.USERPORT_RADIUS*2;
                            foreach(var up_lvm in tmp_up_lvm_list)
                            {
                                Double term = drawDataMgr.getCanvasValue_FromVMValue(canvas_size,term_vm);
                                
                                Point np = new Point(ast_p.X + term * count, ast_p.Y + term * 2);
                                Point np_db = drawDataMgr.getVMPoint_FromCanvasPoint(canvas_size, np);
                                up_lvm.pos_x = (int)np_db.X; 
                                up_lvm.pos_y = (int)np_db.Y;
                                up_lvm.is_changed = true;
                                assetTreeVMCheckVisibilty(up_lvm.user_port_layout_id, AssetTreeType.UserPort, true);

                                Size s = drawDataMgr.getCanvasSize_FromVMSize(canvas_size, new Size(g.USERPORT_RADIUS*2, g.USERPORT_RADIUS*2));
                                AssetLayoutVM parent_ast_lvm = ast_lvm_list.Find(at => at.asset_id == up_lvm.asset_id);
                                Point parent_p = drawDataMgr.getCanvasPoint_FromVMPoint(
                                                        canvas_size, new Point(parent_ast_lvm.pos_x ?? 0, parent_ast_lvm.pos_y ?? 0));
                                Size parent_s = drawDataMgr.getCanvasSize_FromVMSize(canvas_size, new Size(g.ASSET_SIZE_WIDTH, g.ASSET_SIZE_HEIGHT));
                                if(up_lvm.is_layout =="Y")
                                {
                                    drawer2dRack.moveUserPortAt(np,s, up_lvm.user_port_layout_id, parent_p, parent_s);
                                }
                                else
                                {
                                    up_lvm.is_layout = "Y";
                                    drawer2dRack.addUserPort(np, s, up_lvm.user_port_layout_id, up_lvm.port_no, parent_p, parent_s);
                                }
                                count++;
                            }
                        }
                        break;
                    case RoomRackLayoutMode.USERPORT_EDIT:
                        Point up_p = drawer2dRack.getUserPortPoint(selected_up_lvm.user_port_layout_id);

                        int new_x3 = (int)drawDataMgr.getVMValue_FromCanvasValue(canvas_size, up_p.X);
                        int new_y3 = (int)drawDataMgr.getVMValue_FromCanvasValue(canvas_size, up_p.Y);

                        if ((selected_up_lvm.pos_x != new_x3) || (selected_up_lvm.pos_y != new_y3))
                        {
                            //check in the room
                            asset ast = g.asset_list.Find(at => at.asset_id == selected_up_lvm.asset_id);
                            if (ast == null) break;
                            location l2 = g.location_list.Find(at => at.location_id == ast.location_id);
                            if (l2 == null) break;
                            Boolean is_in_room = IsInTheRoom(l2.room_id ?? 0, new Point(new_x3, new_y3));
                            if (is_in_room == false) break;

                            //temp save  change room layout  
                            selected_up_lvm.pos_x = new_x3;
                            selected_up_lvm.pos_y = new_y3;
                            selected_up_lvm.is_changed = true;
                            selected_ast_vm.check_view = Visibility.Visible;
                            selected_ast_vm.force_changed = true;
                        }
                        break;
                }
            }

        }


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

                SelectedItemInCanvas(rack_id, AssetTreeType.Rack);
                en_drag_item = true;
            }
            //Asset을 선택한 경우
            else if (e.Source is DrawingItem2DAsset)
            {
                DrawingItem2DAsset _uc_ast = (DrawingItem2DAsset)e.Source;
                int asset_id = getNumberInStr(_uc_ast.Name);

                AssetLayoutVM ast_lvm = ast_lvm_list.Find(at => at.asset_id == asset_id);
                if (ast_lvm != null)
                {
                    SelectedItemInCanvas(asset_id, ast_lvm.type);
                    en_drag_item = true;
                }
            }
            //User Port를 선택한 경우
            else if (e.Source is DrawingItem2DUserPort)
            {
                DrawingItem2DUserPort up = (DrawingItem2DUserPort)e.Source;
                int up_id = getNumberInStr(up.Name);

                UserPortLayoutVM up_lvm = up_lvm_list.Find(at => at.user_port_layout_id == up_id);
                if (up_lvm != null)
                {
                    SelectedItemInCanvas(up_id, up_lvm.type);
                    en_drag_item = true;
                }
            }
        }

        #endregion


        private void _btnSave_Click(object sender, RoutedEventArgs e)
        {

            int room_save_count = 0;
            int rack_save_count = 0;
            int ast_save_count = 0;
            int up_save_count = 0;
            foreach(var rm_lvm in room_lvm_list)
            {
                if (rm_lvm.is_changed)
                {
                    room_save_count++;
                    saveRoom(rm_lvm);
                    rm_lvm.is_changed = false;
                    assetTreeVMCheckVisibilty(rm_lvm.room_id, AssetTreeType.Room, false);
                }
            }

            foreach(var rk_lvm in rack_lvm_list)
            {
                if (rk_lvm.is_changed)
                {
                    rack_save_count++;
                    saveRack(rk_lvm);
                    rk_lvm.is_changed = false;
                    assetTreeVMCheckVisibilty(rk_lvm.rack_id, AssetTreeType.Rack, false);
                }
            }

            foreach(var ast_lvm in ast_lvm_list)
            {
                if(ast_lvm.is_changed)
                {
                    ast_save_count++;
                    saveAsset(ast_lvm);
                    ast_lvm.is_changed = false;
                    assetTreeVMCheckVisibilty(ast_lvm.asset_id,ast_lvm.type, false);
                }
            }

            foreach(var up_lvm in up_lvm_list)
            {
                if(up_lvm.is_changed)
                {
                    up_save_count++;
                    saveUserPort(up_lvm);
                    up_lvm.is_changed = false;
                    assetTreeVMCheckVisibilty(up_lvm.user_port_layout_id, AssetTreeType.UserPort, false);
                }
            }

            // GS_DEL
            String save_str = "Nothing Changed!";
            if ((room_save_count + rack_save_count + ast_save_count + up_save_count) != 0)
            { 
                save_str = string.Format("Room:{0} Rack:{1} Asset:{2} UP:{3}", room_save_count, rack_save_count, ast_save_count, up_save_count);
                MessageBox.Show(g.tr_get("C_Info25"));
            }

//            PopUpWindow popup_window = new PopUpWindow(save_str);
//            popup_window.Show();

            _btnSave.Focusable = false;
            _tbtnEdit.IsChecked = false;
        }

      
        #region Save Method
        private async void saveRoom(RoomLayoutVM rm_lvm)
        {
            //server db update
            room rm = makeRoom(rm_lvm);
            int ret = await webapi.put("room", rm.room_id, rm, typeof(room));

            //global update
            room g_rm = g.room_list.Find(at => at.room_id == rm_lvm.room_id);
            g_rm.square_x1 = rm_lvm.square_x1;
            g_rm.square_x2 = rm_lvm.square_x2;
            g_rm.square_y1 = rm_lvm.square_y1;
            g_rm.square_y2 = rm_lvm.square_y2;

            g_rm.flag_x = rm_lvm.flag_x;
            g_rm.flag_y = rm_lvm.flag_y;

        }
        private async void saveRack(RackLayoutVM rk_lvm)
        {
            //server db update
            rack rk = makeRack(rk_lvm);
            
            //check is in the room
            Boolean is_save = IsInTheRoom(rk_lvm.room_id, new Point(rk_lvm.pos_x ?? 0, rk_lvm.pos_y ?? 0)); 
            if(is_save == false)
            {
                rk.pos_x = null;
                rk.pos_y = null;
            }
            int ret = await webapi.put("rack", rk.rack_id, rk, typeof(rack));

            //global update
            rack g_rk = g.rack_list.Find(at => at.rack_id == rk.rack_id);
            g_rk.pos_x = rk.pos_x;
            g_rk.pos_y = rk.pos_y;
        }


        private async void saveAsset(AssetLayoutVM ast_lvm)
        {
            //server db update
            asset ast = makeAsset(ast_lvm);

            //check is in the room
            location l = g.location_list.Find(at=> at.location_id == ast.location_id);
            Boolean is_save = IsInTheRoom(l.room_id ?? 0, new Point(ast_lvm.pos_x ?? 0, ast_lvm.pos_y ?? 0));
            if (is_save == false)
            {
                //룸의 배치 범위를 벗어나는 경우
                ast.pos_x = null;
                ast.pos_y = null;
                ast.is_layout = "N";
            }
            else
            {
                ast.is_layout = "Y";
            }

            int ret = await webapi.put("asset", ast.asset_id, ast, typeof(asset));

            //global update
            asset g_ast = g.asset_list.Find(at => at.asset_id == ast_lvm.asset_id);
            g_ast.pos_x = ast.pos_x;
            g_ast.pos_y = ast.pos_y;
            g_ast.is_layout = ast.is_layout;
        }

        private async void saveUserPort(UserPortLayoutVM up_lvm)
        {
            //server db update
            user_port_layout up = makeUserPort(up_lvm);
            //up.is_layout = "Y";


            //user port 에 연결된 Asset을 찾아 그에 해당하는 룸을 찾는다
            AssetLayoutVM ast_lvm = ast_lvm_list.Find(at => at.asset_id == up_lvm.asset_id);

            //check is in the room
            location l = g.location_list.Find(at => at.location_id == ast_lvm.location_id);
            Boolean is_save = IsInTheRoom(l.room_id ?? 0, new Point(up_lvm.pos_x ?? 0, up_lvm.pos_y ?? 0));

            if (is_save == false)
            {
                //룸의 배치 범위를 벗어나는 경우
                up.pos_x = null;
                up.pos_y = null;
                up.is_layout = "N";
            }
            else
            {
                up.is_layout = "Y";
            }

            int ret = await webapi.put("user_port_layout", up.user_port_layout_id, up, typeof(user_port_layout));

            //global update
            user_port_layout g_up = g.user_port_layout_list.Find(at => at.user_port_layout_id == up_lvm.user_port_layout_id);
            g_up.pos_x = up.pos_x;
            g_up.pos_y = up.pos_y;
            g_up.is_layout = up.is_layout;
        }
        
        #endregion

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
            RoomLayoutVM rm_lvm = room_lvm_list.Find(at => at.room_id == room_id);
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
            AssetTreeVM ast_vm = getAssetTreeVM(id, type);
            if (ast_vm == null)
                return;

            if (check == true)
            {
                ast_vm.check_view = Visibility.Visible;
                ast_vm.force_changed = true;
            }
            else
            {
                ast_vm.check_view = Visibility.Hidden;
                ast_vm.force_changed = true;
            }
        }

       
    }

}
