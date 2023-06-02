//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;

//namespace I2MS2.Pages
//{
//    /// <summary>
//    /// P4AssetView.xaml에 대한 상호 작용 논리
//    /// </summary>
//    public partial class P4AssetView : Page
//    {
//        public P4AssetView()
//        {
//            InitializeComponent();
//        }
//    }
//}

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


using I2MS2.Models;
using WebApi.Models;
using System.Windows.Threading;
using I2MS2.Library;
using I2MS2.UserControls.Drawing;
using I2MS2.UserControls;
using I2MS2.Animation;
using System.IO;

namespace I2MS2.Pages
{
    // 자산 뷰
    enum assetViewMode
    {
        NONE,
        SITE,
        BUILDING,
        FLOOR,
        ROOM,
        RACK,
        ASSET_IN_RACK,
        ASSET
    };

    /// <summary>
    /// P4AssetManager.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class P4AssetView : Page
    {
        List<user_port_layout> up_list = new List<user_port_layout>();
        
        // romee 2/23 자산뷰 상단 업데이트용  
        AssetTreeType atype;
        int aid;

        assetViewMode asset_view_mode = assetViewMode.NONE;
        AssetTreeVM use_site_vm;
        AssetTreeVM use_bd_vm;
        AssetTreeVM use_fl_vm;
        AssetTreeVM use_rm_vm;
        AssetTreeVM use_rk_vm;
        AssetTreeVM use_asset_vm;

        AssetTreeVM select_as_vm;
        private int asset_id_for_link_diagram = 0;  //
        private int port_no_for_link_diagram = 0;   //

        String use_drawing_file_path;

        List<int> regP4Info = new List<int>(); // 레지스트리 저장용 

        public P4AssetView()
        {
            g._P4AssetView = this;
            InitializeComponent();

        }

        // 로드후 처리 
        private void P4AssetView_Loaded(object sender, RoutedEventArgs e)
        {
            //AssetView_SiteInit(g.select_site.site_id);
            //changeUIAssetViewMode(assetViewMode.SITE);
            _ctlDrawingView3D.camMoveEndEventToAssetView += new DrawingView3D.camMoveEndHandler(cameraFocusMoveComplated);

            _ctlRack.SelectionChangedEvent += new RackControl.SelectChangedHander(SelectionChangedInRack);

///#if GS_DEL
            var l1 = Reg.get_dashboard("regP4Info");
            if (l1 != null) regP4Info = l1.ToList();
            else
            {
                regP4Info.Add(1);
                regP4Info.Add(0);
                regP4Info.Add(0);
                Reg.save_dashboard(regP4Info, "regP4Info");
            }
//#endif
            if (regP4Info[0] == 1) _ckbxRoomInfoShow.IsChecked = true;
            else _ckbxRoomInfoShow.IsChecked = false;
            if (regP4Info[1] == 1) _ckbxRackAssetInfoShow.IsChecked = true;
            else _ckbxRackAssetInfoShow.IsChecked = false;
            if (regP4Info[2] == 1) _ckbxUserPortInfoShow.IsChecked = true;
            else _ckbxUserPortInfoShow.IsChecked = false;
        }

        #region // 초기화 처리 
        // 초기화 처리 -> 메인에서 호출         
        public void InitAssetView(int site_id)
        {
            AssetView_SiteInit(g.select_site.site_id);
            InitUIAssetViewMode();
            display_property(use_site_vm);
            //changeUIAssetViewMode(assetViewMode.SITE);
        }
        // 로그오프 재 초기화 -> 메인에서 호출    
        public void ReInitAssetView(int site_id)
        {
            if (site_id != g.select_site.site_id)
                return;

            //자산뷰가 현재 도면을 표시하고 있으면 해당 부분을 우선 다 삭제 한다
            if (use_fl_vm != null)
            {
                _ctlDrawingView3D.clearItemAll();
                _ctlDrawingView3D.removeAllWall();
            }

            changeUIAssetViewMode(assetViewMode.SITE);
            AssetView_SiteInit(site_id);
        }

        // 초기화 처리 
        private void InitUIAssetViewMode()
        {

            asset_view_mode = assetViewMode.SITE;
            ScaleTransform scale_tran = new ScaleTransform(0, 1);
            
            //_gridTop1.RenderTransform = scale_tran;
            _gridTop2.RenderTransform = scale_tran;

            _gridLeftListViewRack.RenderTransform = scale_tran;
            //_gridLeftListViewSubAsset.RenderTransform = scale_tran;

            //_gridRightViewImage.RenderTransform = scale_tran;
            //_gridRightView3D.RenderTransform = scale_tran;
            _gridRightView3D.Opacity = 0;
        }
        #endregion

        #region // 화면 변경 처리        
        // 자산 변경시 
        public void assetChange(AssetTreeVM ast_vm)
        {

            switch(ast_vm.type)
            {
                case AssetTreeType.Site:
                    AssetViewCurrentDataClear();
                    changeUIAssetViewMode(assetViewMode.SITE);
                    AssetView_SiteInit(ast_vm.type_id);

                     break;
                case AssetTreeType.Building:
                    AssetViewCurrentDataClear();
                    changeUIAssetViewMode(assetViewMode.BUILDING);
                    AssetView_BuildingInit(ast_vm);
                    break;
                case AssetTreeType.Floor:
                    AssetViewCurrentDataClear();
                    changeUIAssetViewMode(assetViewMode.FLOOR);
                    AssetView_FloorInit(ast_vm);

                    break;
                case AssetTreeType.Room:
                    AssetViewCurrentDataClear();
                    changeUIAssetViewMode(assetViewMode.ROOM);
                    AssetView_RoomInit(ast_vm);
                    break;

                case AssetTreeType.Rack:
                    AssetViewCurrentDataClear();
                    changeUIAssetViewMode(assetViewMode.RACK);
                    AssetView_RackInit(ast_vm);
                    break;
                default://Asset, PC, Port
                    //if (ast_vm.type == AssetTreeType.Port)
                    //    break;

                    //렉에 속해 있는 Asset인지 확인한다
                    AssetTreeVM p_ast_vm = Etc.get_location_ast_vm(ast_vm.location_id);
                    if (p_ast_vm == null) break;

                    if(p_ast_vm.type==AssetTreeType.Rack)
                    {
                        if ( (asset_view_mode == assetViewMode.RACK)&&(use_rk_vm.location_id == ast_vm.location_id))
                        {
                            selectAssetInRack(ast_vm.asset_id ?? 0);
                        }
                        else
                        {
                            AssetViewCurrentDataClear();
                            changeUIAssetViewMode(assetViewMode.RACK);
                            AssetView_RackInit(p_ast_vm);

                            selectAssetInRack(ast_vm.asset_id ?? 0);
                        }
                    }
                    else if((ast_vm.type != AssetTreeType.Port)&&(ast_vm.type!=AssetTreeType.PC))
                    {
                        //이전 AssetView를 지운다
                        AssetViewCurrentDataClear();
                        changeUIAssetViewMode(assetViewMode.ASSET);
                        AssetView_AssetInit(ast_vm);
                    }
                    else if(ast_vm.type== AssetTreeType.Port)
                    {
                        //이전 AssetView를 지운다
                        AssetViewCurrentDataClear();
                        changeUIAssetViewMode(assetViewMode.ASSET);

                        AssetTreeVM p_ast_vm2 = ast_vm.parant_ast_vm;
                        AssetView_AssetInit(p_ast_vm2);
                    }
                    else if(ast_vm.type == AssetTreeType.PC)
                    {
                        //이전 AssetView를 지운다
                        AssetViewCurrentDataClear();
                        changeUIAssetViewMode(assetViewMode.ASSET);
                        AssetTreeVM p_ast_vm3 = ast_vm.parant_ast_vm;
                        AssetTreeVM pp_ast_vm3 = p_ast_vm3.parant_ast_vm;
                        AssetView_AssetInit(pp_ast_vm3);
                    }

                    break;
                
            }
        }
        // 기존 데이터 클리어 
        public void AssetViewCurrentDataClear()
        {
            switch (asset_view_mode)
            {
                case assetViewMode.SITE:
                    break;
                case assetViewMode.BUILDING:
                    
                    break;
                case assetViewMode.FLOOR:
                    AssetView_FloorClear();
                    break;
                case assetViewMode.ROOM:
                    AssetView_RoomClear();
                    break;
                case assetViewMode.RACK:
                    AssetView_RackClear();
                    break;
                case assetViewMode.ASSET:
                    AssetView_AssetClear();
                    break;
            }
        }
        // 좌측 리스트 하이드 처리 
        private void changeUIAssetViewModeExceptMain(assetViewMode ast_mode)
        {
            switch (asset_view_mode)
            {
                case assetViewMode.SITE:
                case assetViewMode.BUILDING:
                    if ((ast_mode == assetViewMode.RACK) || (ast_mode == assetViewMode.ASSET))
                        topGridHideAnimation(_gridTop1);
                    leftGridHideAnimation(_gridLeftListViewSubAsset);
                    break;
                case assetViewMode.FLOOR:
                case assetViewMode.ROOM:
                    if ((ast_mode == assetViewMode.RACK) || (ast_mode == assetViewMode.ASSET))
                        topGridHideAnimation(_gridTop1);
                    leftGridHideAnimation(_gridLeftListViewSubAsset);
                    break;
                case assetViewMode.RACK:
                    if ((ast_mode != assetViewMode.RACK) || (ast_mode != assetViewMode.ASSET))
                        topGridHideAnimation(_gridTop2);
                    leftGridHideAnimation(_gridLeftListViewRack);
                    break;
                case assetViewMode.ASSET:
                    if ((ast_mode != assetViewMode.RACK) || (ast_mode != assetViewMode.ASSET))
                        topGridHideAnimation(_gridTop2);
                    leftGridHideAnimation(_gridLeftListViewSubAsset);
                    break;
            }
            asset_view_mode = ast_mode;
        }
        // 좌측 우측 상단 하이드 처리 
        private void changeUIAssetViewMode(assetViewMode ast_mode)
        {
            switch(asset_view_mode)
            {
                case assetViewMode.SITE:
                case assetViewMode.BUILDING:
                    if ( (ast_mode == assetViewMode.RACK)||(ast_mode == assetViewMode.ASSET))
                        topGridHideAnimation(_gridTop1);
                    leftGridHideAnimation(_gridLeftListViewSubAsset);
                    mainGridHideAnimation(_gridRightViewImage);
                    break;
                case assetViewMode.FLOOR:
                case assetViewMode.ROOM:
                    if ((ast_mode == assetViewMode.RACK) || (ast_mode == assetViewMode.ASSET))
                        topGridHideAnimation(_gridTop1);
                    leftGridHideAnimation(_gridLeftListViewSubAsset);
                    mainGridHideAnimation(_gridRightView3D);
                    break;
                case assetViewMode.RACK:
                    if ((ast_mode != assetViewMode.RACK) || (ast_mode != assetViewMode.ASSET))
                        topGridHideAnimation(_gridTop2);
                    leftGridHideAnimation(_gridLeftListViewRack);
                    mainGridHideAnimation(_gridRightView3D);
                    break;
                case assetViewMode.ASSET:
                    if ((ast_mode != assetViewMode.RACK) || (ast_mode != assetViewMode.ASSET))
                        topGridHideAnimation(_gridTop2);
                    leftGridHideAnimation(_gridLeftListViewSubAsset);
                    mainGridHideAnimation(_gridRightView3D);
                    break;
            }
            asset_view_mode = ast_mode;
        }
        #endregion

        #region // ModeChange Animation
   
        private void topGridHideAnimation(Grid gd)
        {
            SimpleAnimation anim = new SimpleAnimation();
            Point temp_center = new Point(0, 0);
            Vector temp_from_v = new Vector(1, 1);
            Vector temp_to_v = new Vector(1, 0);

            anim.animComplateEvent += new SimpleAnimation.animCompleteEventHandler(topGridHideAnimationComplete);
            anim.gridScaleAnimationWithEvnet(gd, temp_from_v, temp_to_v, temp_center, 0.8, 1);
        }

        private void leftGridHideAnimation(Grid gd)
        {
            SimpleAnimation anim = new SimpleAnimation();
            Point temp_center = new Point(0, 0);
            //Point temp_center = new Point(gd.ActualWidth, gd.ActualHeight);
            Vector temp_from_v = new Vector(1, 1);
            Vector temp_to_v = new Vector(0, 0);

            anim.animComplateEvent += new SimpleAnimation.animCompleteEventHandler(leftGridHideAnimationComplete);
            anim.gridScaleAnimationWithEvnet(gd, temp_from_v, temp_to_v, temp_center, 0.8, 100);
        }

        private void mainGridHideAnimation(Grid gd)
        {
            SimpleAnimation anim = new SimpleAnimation();
            anim.animComplateEvent += new SimpleAnimation.animCompleteEventHandler(mainGridHideAnimationComplete);
            anim.gridOpacityAnimationWithEvent(gd, 1, 0, 0.8, 500);
        }

        private void topGridShowAnimation(Grid gd)
        {
            SimpleAnimation anim = new SimpleAnimation();
            Point temp_center = new Point(0, 0);
            Vector temp_from_v = new Vector(1, 0);
            Vector temp_to_v = new Vector(1, 1);

  //          anim.animComplateEvent += new SimpleAnimation.animCompleteEventHandler(topGridHideAnimationComplete);
            anim.gridScaleAnimation(gd, temp_from_v, temp_to_v, temp_center, 0.3, 500);
        }

        private void leftGridShowAnimation(Grid gd)
        {
            SimpleAnimation anim = new SimpleAnimation();
            Point temp_center = new Point(0, 0);
            //Point temp_center = new Point(gd.ActualWidth, gd.ActualHeight);
            Vector temp_from_v = new Vector(0, 0);
            Vector temp_to_v = new Vector(1, 1);

   //         anim.animComplateEvent += new SimpleAnimation.animCompleteEventHandler(leftGridHideAnimationComplete);
            anim.gridScaleAnimation(gd, temp_from_v, temp_to_v, temp_center, 0.8, 500);
        }

        private void mainGridShowAnimation(Grid gd)
        {
            SimpleAnimation anim = new SimpleAnimation();
            //Point temp_center = new Point(0, 0);
            Point temp_center = new Point(gd.ActualWidth, gd.ActualHeight);
            Vector temp_from_v = new Vector(0, 1);
            Vector temp_to_v = new Vector(1, 1);

//            anim.animComplateEvent += new SimpleAnimation.animCompleteEventHandler(mainGridHideAnimationComplete);
            anim.gridOpacityAnimation(gd, 0, 1,  0.8, 300);
        }

        private void topGridHideAnimationComplete(object sender)
        {
            switch (asset_view_mode)
            {
                case assetViewMode.SITE:
                case assetViewMode.BUILDING:
                case assetViewMode.FLOOR:
                case assetViewMode.ROOM:
                    topGridShowAnimation(_gridTop1);
                    break;
                case assetViewMode.RACK:
                case assetViewMode.ASSET:
                    topGridShowAnimation(_gridTop2);
                    break;
            }
        }

        private void leftGridHideAnimationComplete(object sender)
        {
            switch (asset_view_mode)
            {
                case assetViewMode.SITE:
                case assetViewMode.BUILDING:
                case assetViewMode.FLOOR:
                case assetViewMode.ROOM:
                case assetViewMode.ASSET:
                    leftGridShowAnimation(_gridLeftListViewSubAsset);
                    break;
                case assetViewMode.RACK:
                    leftGridShowAnimation(_gridLeftListViewRack);
                    break;
            }
        }

        private void mainGridHideAnimationComplete(object sender)
        {
            switch (asset_view_mode)
            {
                case assetViewMode.SITE:
                case assetViewMode.BUILDING:
                    mainGridShowAnimation(_gridRightViewImage);
                    break;
                case assetViewMode.FLOOR:
                case assetViewMode.ROOM:
                case assetViewMode.RACK:
                case assetViewMode.ASSET:
                    mainGridShowAnimation(_gridRightView3D);
                    break;
            }
        }
        
        #endregion
 
        #region // AssetView_Site
        private void AssetView_SiteInit(int site_id)
        {
            //현재 선택된 사이트의 정보를 g.에서 받아와서 그 사이트 정보로 초기화한다
            //site st = g.site_list.Find(at => at.site_name == g.selected_site_name);
            //g.
            site st = g.site_list.Find(at => at.site_id == site_id);
            if (st == null) return;

            location l_st = g.location_list.Find(at => (at.site_id == st.site_id) && (at.building_id == null));
            if (l_st == null) return;
            AssetTreeVM st_vm;
            try
            {
                st_vm = g.location_ast_vm_dic[l_st.location_id];
                if (st_vm == null) return;
            }
            catch {
                // GS 인증 신규 사이트 오픈시 기존 찌꺼기 날리기 
                _lvSubAssetList.ItemsSource = null;
                return; 
            }

            //현재 사용되는 site AssetTreeVM 저장한다
            use_site_vm = st_vm;
            
            //사이트 기본정보 설정
            _txtName.Text = g.tr_get("C_Site");
            _txtRemarks.Text = g.tr_get("C_Remarks");

            _txbName.Text = st.site_name;
            _txbRemarks.Text = st.remarks;


            //통계 정보 가져와서 각 그래프레 적용
            location l = g.location_list.Find(at => at.location_name == st.site_name);
            setChartData(AssetTreeType.Site, st.site_id);
            atype = AssetTreeType.Site;
            aid = st.site_id;

            _lvSubAssetList.ItemsSource = null;
            _lvSubAssetList.ItemsSource = st_vm.child_list;

            //사이트 이미지 정보 적용
            sp_list_image_Result sp_img = g.sp_image_list.Find(at => at.image_id == st.site_image_id);
            if (sp_img != null)
            {
                String tmp_img_path = string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH, sp_img.folder_name, sp_img.file_name);
                String img_path;
                if (File.Exists(tmp_img_path))
                {
                    img_path = tmp_img_path;
                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.CacheOption = BitmapCacheOption.OnLoad;
                    bmp.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    bmp.UriSource = new Uri(img_path);
                    //bmp.UriSource = new Uri("C:/Users/Administrator/Source/Workspaces/I2MS2/I2MS2/I2MS2/Images/null.png");
                    bmp.EndInit();
                    _imgPicture.Source = bmp;
                }
                
            }

        } 


        #endregion

        #region // AssetView_Building
        private void AssetView_BuildingInit(AssetTreeVM bd_vm)
        {
            building bd = g.building_list.Find(at => at.building_id == bd_vm.type_id);
            if (bd == null) return;
            
            //현재 사용되는 buidling AssetTreeVM 저장한다
            use_bd_vm = bd_vm;

            //기본정보 설정
            _txtName.Text = g.tr_get("C_Building");
            _txtRemarks.Text = g.tr_get("C_Remarks");
            _txbName.Text = bd.building_name;
            _txbRemarks.Text = bd.remarks;

            //사이트 이미지 정보 적용
            sp_list_image_Result sp_img = g.sp_image_list.Find(at => at.image_id == bd.building_image_id);
            if (sp_img != null)
            {
                string file_path = string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH, sp_img.folder_name, sp_img.file_name);
                string file_path2 = string.Format("{0}{1}", g.CLIENT_IMAGE_PATH, "noname.jpg");
                BitmapImage bmp = new BitmapImage();

                try
                {
                    bmp.BeginInit();
                    bmp.CacheOption = BitmapCacheOption.OnLoad;
                    bmp.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    bmp.UriSource = new Uri(file_path);
                    bmp.EndInit();
                    _imgPicture.Source = bmp;
                }
                catch (Exception e) 
                {
//                    bmp.BeginInit();
//                    bmp.CacheOption = BitmapCacheOption.OnLoad;
//                    bmp.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    bmp.UriSource = new Uri(file_path2);
//                    bmp.EndInit();
                    _imgPicture.Source = null;
                }
            }

            //통계 정보 가져와서 각 그래프레 적용
            location l = g.location_list.Find(at => at.location_id == bd_vm.location_id);
            setChartData(AssetTreeType.Building, bd.building_id);
            atype = AssetTreeType.Building;
            aid = bd.building_id;

            _lvSubAssetList.ItemsSource = null;
            _lvSubAssetList.ItemsSource = bd_vm.child_list;

            if (g.selected_building_id != bd_vm.type_id)
            {
                P3FloorView p3 = (P3FloorView)g.main_window._framePage3View.Content; // 층별뷰를 업데이트 
                p3.setBuilding(bd_vm.type_id);
                g.selected_building_id = bd_vm.type_id;

            }
        }

        
        #endregion
   
        #region // AssetView_Floor
        private void AssetView_FloorInit(AssetTreeVM fl_vm)
        {
            floor fl = g.floor_list.Find(at => at.floor_id == fl_vm.type_id);
            if (fl == null) return;

            //현재 사용되는 buidling AssetTreeVM 저장한다
            use_fl_vm = fl_vm;
            use_rm_vm = null; // romee 1/22 다른층 룸에서 층으로 오면 룸을 클리어 

            //기본정보 설정
            _txtName.Text = g.tr_get("C_Floor");
            _txtRemarks.Text = g.tr_get("C_Remarks");
            _txbName.Text = fl.floor_name;
            _txbRemarks.Text = fl.remarks;

            drawing_3d dr = g.drawing_3d_list.Find(at => at.drawing_3d_id == fl.drawing_3d_id);
            if (dr == null)
                return;
            string file_path = string.Format("{0}drawing_3d/{1}", g.CLIENT_IMAGE_PATH, dr.file_name);
            if (fl.drawing_3d_id != 19040015)
                Console.WriteLine("asdf");
            //_ctlDrawingView3D.openDrawingFile(file_path);
            //use_drawing_file_path = file_path;

            //통계 정보 가져와서 각 그래프레 적용
            location l = g.location_list.Find(at => at.location_id == fl_vm.location_id);
            setChartData(AssetTreeType.Floor, fl.floor_id);
            atype = AssetTreeType.Floor;
            aid = fl.floor_id;

            //현제까지 적용된 UI를 강재로 Action 시킨다
            Dispatcher.Invoke(new Action(() => { }), DispatcherPriority.ContextIdle, null);

            _lvSubAssetList.ItemsSource = null;
            _lvSubAssetList.ItemsSource = fl_vm.child_list;

            //drawRoomInfo();
            //_ckbxRoomInfoShow.IsChecked = true;

            _ctlDrawingView3D.clearItemAll();
            drawFloorAllObject(fl_vm);
            _ctlDrawingView3D.openDrawingFile(file_path);
            use_drawing_file_path = file_path;

            //_ctlDrawingView3D.cameraFocuingFloor();
            moveCameraFocusFloor();

        }

        private void AssetView_FloorInitFromRoom(AssetTreeVM fl_vm)
        {
            //현재 사용되는 Floor AssetTreeVM 저장한다
            use_fl_vm = fl_vm;
            floor fl = g.floor_list.Find(at => at.floor_id == fl_vm.type_id);

            if (fl == null)
                return;
            //기본정보 설정
            _txtName.Text = g.tr_get("C_Floor");
            _txtRemarks.Text = g.tr_get("C_Remarks");
            _txbName.Text = fl.floor_name;
            _txbRemarks.Text = fl.remarks;

            //_ctlDrawingView3D.cameraFocuingFloor();

            //통계 정보 가져와서 각 그래프레 적용
            location l = g.location_list.Find(at => at.location_name == fl.floor_name);
            setChartData(AssetTreeType.Floor, fl.floor_id);
            atype = AssetTreeType.Floor;
            aid = fl.floor_id;

            _lvSubAssetList.ItemsSource = null;
            _lvSubAssetList.ItemsSource = fl_vm.child_list;

            moveCameraFocusFloor();
        }

        private void AssetView_FloorClear()
        {
            up_list.Clear();
            clearInfoViewAll();

            _ctlDrawingView3D.clearItemAll();

            //_ckbxRoomInfoShow.IsChecked = false;
            //_ckbxRackAssetInfoShow.IsChecked = false;
            //_ckbxUserPortInfoShow.IsChecked = false;
        }

        private void AssetView_FloorClear_GotoRoom()
        {
            clearInfoViewAll();
        }



        #endregion

        #region // AssetView_Room

        private void AssetView_RoomInit(AssetTreeVM rm_vm)
        {
            room rm = g.room_list.Find(at => at.room_id == rm_vm.type_id);
            if (rm == null) return;

            floor fl = g.floor_list.Find(at => at.floor_id == rm.floor_id);
            if (fl == null) return;

            //현재 사용되는 buidling AssetTreeVM 저장한다
            AssetTreeVM fl_vm = getParentAssetTreeVM(rm_vm, rm_vm.disp_level);
            if (fl_vm == null) return;
            use_fl_vm = fl_vm;
            use_rm_vm = rm_vm;

            //기본정보 설정
            _txtName.Text = g.tr_get("C_Room");
            _txtRemarks.Text = g.tr_get("C_Remarks");
            _txbName.Text = rm.room_name;
            _txbRemarks.Text = rm.remarks;

            drawing_3d dr = g.drawing_3d_list.Find(at => at.drawing_3d_id == fl.drawing_3d_id);

            string file_path = string.Format("{0}drawing_3d/{1}", g.CLIENT_IMAGE_PATH, dr.file_name);
            //if (fl.drawing_3d_id != 19040015)
            //    Console.WriteLine("asdf");
            //_ctlDrawingView3D.openDrawingFile(file_path);
            //use_drawing_file_path = file_path;

            //통계 정보 가져와서 각 그래프레 적용
            location l = g.location_list.Find(at => at.location_id == rm_vm.location_id);
            setChartData(AssetTreeType.Room, rm.room_id);
            atype = AssetTreeType.Room;
            aid = rm.room_id;

            //현제까지 적용된 UI를 강재로 Action 시킨다
            Dispatcher.Invoke(new Action(() => { }), DispatcherPriority.ContextIdle, null);

            _lvSubAssetList.ItemsSource = null;
            _lvSubAssetList.ItemsSource = rm_vm.child_list;

            //drawRoomInfo();

            //_ckbxRoomInfoShow.IsChecked = true;
            drawFloorAllObject(fl_vm);

            _ctlDrawingView3D.openDrawingFile(file_path);
            use_drawing_file_path = file_path;

            Point start_p = new Point(((Double)(rm.square_x1 ?? 0)) / 100, ((Double)(rm.square_y1 ?? 0)) / 100);
            Point end_p = new Point(((Double)(rm.square_x2 ?? 0)) / 100, ((Double)(rm.square_y2 ?? 0)) / 100);
            Point center_p = new Point((start_p.X + end_p.X) / 2, (start_p.Y + end_p.Y) / 2);
            Double room_width = (Math.Abs(end_p.X - start_p.X) * 2);

            moveCamera(center_p, room_width);
        }



        private void AssetView_RoomInitFromFloor(AssetTreeVM rm_vm)
        {
            room rm = g.room_list.Find(at => at.room_id == rm_vm.type_id);
            if (rm == null) return;

            //현재 사용되는 Room AssetTreeVM 저장한다
            use_rm_vm = rm_vm;


            //기본정보 설정
            _txtName.Text = g.tr_get("C_Room");
            _txtRemarks.Text = g.tr_get("C_Remarks");
            _txbName.Text = rm.room_name;
            _txbRemarks.Text = rm.remarks;

            Point start_p = new Point(((Double)(rm.square_x1 ?? 0)) / 100, ((Double)(rm.square_y1 ?? 0)) / 100);
            Point end_p = new Point(((Double)(rm.square_x2 ?? 0)) / 100, ((Double)(rm.square_y2 ?? 0)) / 100);
            Point center_p = new Point((start_p.X + end_p.X) / 2, (start_p.Y + end_p.Y) / 2);
            Double room_width = (Math.Abs(end_p.X - start_p.X) * 2);

            //통계 정보 가져와서 각 그래프레 적용
            location l = g.location_list.Find(at => at.location_id == rm_vm.location_id);
            setChartData(AssetTreeType.Room, rm.room_id);
            atype = AssetTreeType.Room;
            aid = rm.room_id;

            _lvSubAssetList.ItemsSource = null;
            _lvSubAssetList.ItemsSource = rm_vm.child_list;

            moveCamera(center_p, room_width);
            //drawInfoViewAll();
        }

        private void moveCamera(Point center_p, Double width)
        {
            clearInfoViewAll();
            _ctlDrawingView3D.cameraFocucingRoom(center_p, width);
        }

        private void moveCameraFocusFloor()
        {
            clearInfoViewAll();
            _ctlDrawingView3D.cameraFocuingFloor();
        }


        public void cameraFocusMoveComplated(object sender)
        {
            //       clearInfoViewAll();
            drawInfoViewAll();
        }

        private void AssetView_RoomInitFromRack(AssetTreeVM rm_vm)
        {
            room rm = g.room_list.Find(at => at.room_id == rm_vm.type_id);
            if (rm == null) return;

            //현재 사용되는 Room AssetTreeVM 저장한다
            use_rm_vm = rm_vm;

            //기본정보 설정
            _txtName.Text = g.tr_get("C_Room");
            _txtRemarks.Text = g.tr_get("C_Remarks");
            _txbName.Text = rm.room_name;
            _txbRemarks.Text = rm.remarks;

            Point start_p = new Point(((Double)(rm.square_x1 ?? 0)) / 100, ((Double)(rm.square_y1 ?? 0)) / 100);
            Point end_p = new Point(((Double)(rm.square_x2 ?? 0)) / 100, ((Double)(rm.square_y2 ?? 0)) / 100);
            Point center_p = new Point((start_p.X + end_p.X) / 2, (start_p.Y + end_p.Y) / 2);
            Double room_width = (Math.Abs(end_p.X - start_p.X) * 2);

            //통계 정보 가져와서 각 그래프레 적용
            location l = g.location_list.Find(at => at.location_id == rm_vm.location_id);
            setChartData(AssetTreeType.Room, rm.room_id);
            atype = AssetTreeType.Room;
            aid = rm.room_id;

            _lvSubAssetList.ItemsSource = null;
            _lvSubAssetList.ItemsSource = rm_vm.child_list;

            moveCamera(center_p, room_width);
        }

        private void AssetView_RoomClear()
        {
            _lvPortList.ItemsSource = null;
            _txtTop2Name.Text = "";
            up_list.Clear();
            //moveCameraFocusFloor();

            _ctlDrawingView3D.clearItemAll();

            //_ckbxRoomInfoShow.IsChecked = false;
            //_ckbxRackAssetInfoShow.IsChecked = false;
            //_ckbxUserPortInfoShow.IsChecked = false;

            _lvSubAssetList.ItemsSource = null;
        }

        private void AssetView_RoomClearGotoFloor()
        {
            clearInfoViewAll();
            _lvSubAssetList.ItemsSource = null;
        }

        private void AssetView_RoomClearGotoRackAsset()
        {
            clearInfoViewAll();
            _lvSubAssetList.ItemsSource = null;
        }

        #endregion

        #region // AssetView_Rack

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


        private void AssetView_RackInitFromRoom(AssetTreeVM rk_vm)
        {
            //현재 사용되는 buidling AssetTreeVM 저장한다
            use_rk_vm = rk_vm;
            rack rk = g.rack_list.Find(at => at.rack_id == rk_vm.type_id);
            if (rk == null)
                return;
            room rm = g.room_list.Find(at => at.room_id == rk.room_id);
            if (rm == null) return;

            //기본정보 표시
            // 링크다이어그램을 표시...
            showDefaultLinkViewInRack(rk_vm);

            // 렉뷰 표시
            showRackView(rk);
            _txbTop2Name.Text = rk_vm.disp_name;  // 2016.10.04
            _lvPortList.ItemsSource = null;

            //3D 화면에서 선택된 렉을 강죠 표시
            _ctlDrawingView3D.selectRack(rk.rack_id);

            //카메라를 렉 중심으로 이동
            //Point center_p = new Point((rk.pos_x ?? 0)/100 ,(rk.pos_y ?? 0)/100);
            //moveCamera(center_p, 100);
            Point start_p = new Point(((Double)(rm.square_x1 ?? 0)) / 100, ((Double)(rm.square_y1 ?? 0)) / 100);
            Point end_p = new Point(((Double)(rm.square_x2 ?? 0)) / 100, ((Double)(rm.square_y2 ?? 0)) / 100);
            Point center_p = new Point((start_p.X + end_p.X) / 2, (start_p.Y + end_p.Y) / 2);
            Double room_width = (Math.Abs(end_p.X - start_p.X) * 2);
            moveCamera(center_p, room_width);

            //_ckbxRackAssetInfoShow.IsChecked = true;
        }

        private void AssetView_RackClear()
        {
            up_list.Clear();
            //moveCameraFocusFloor();

            _ctlDrawingView3D.clearItemAll();

            //_ckbxRoomInfoShow.IsChecked = false;
            //_ckbxRackAssetInfoShow.IsChecked = false;
            //_ckbxUserPortInfoShow.IsChecked = false;

            _ctlRack.MyItemsSource = null;
            _ctlLink.Clear();
        }


        private void AssetView_RackClearGotoRoom()
        {
            // _ckbxRackAssetInfoShow.IsChecked = false;
            _ctlDrawingView3D.releaseRack(use_rk_vm.type_id);
            _ctlRack.MyItemsSource = null;
            _ctlLink.Clear();
        }

        //RackView상태에서 Asset이 선택된 경우에 호출
        void SelectionChangedInRack(int asset_id)
        {
            if (asset_view_mode != assetViewMode.RACK) return;
            if (use_rk_vm == null) return;

            AssetTreeVM rk_vm = g.location_ast_vm_dic[use_rk_vm.location_id];
            foreach (var child in rk_vm.child_list)
            {
                if (child.asset_id == asset_id)
                {
                    _ctlLink.Show(child.asset_id ?? 0, 1);
                    select_as_vm = child;
                    _txbTop2Name.Text = child.disp_name;
                    _lvPortList.ItemsSource = child.child_list;

                    return;
                }
            }

        }

        RackLib rack_lib;
        private void selectAssetInRack(int asset_id)
        {
            RackVM vm = rack_lib.getRackVMAsset(asset_id);
            _ctlRack.selectSlot(vm);
        }

        private void showDefaultLinkViewInRack(AssetTreeVM rk_vm_tv)
        {
            RackVM vm = (RackVM)_ctlRack._lvSlots.SelectedItem;
            if (vm == null)
                return;

            AssetTreeVM rk_vm = g.location_ast_vm_dic[rk_vm_tv.location_id];
            foreach (var child in rk_vm.child_list)
            {
                if (child.type == AssetTreeType.i_PatchPanel)
                {
                    _ctlLink.Show(child.asset_id ?? 0, 1);
                    select_as_vm = child;
                    return;
                }
            }
        }

        // 랙 location id를 사용하여 링크 다이어그램을 갱신.
        private void showDefaultLinkViewInRack(int location_id)
        {
            AssetTreeVM rk_vm = g.location_ast_vm_dic[location_id];
            foreach (var child in rk_vm.child_list)
            {
                if (child.type == AssetTreeType.i_PatchPanel)
                {
                    _ctlLink.Show(child.asset_id ?? 0, 1);
                    select_as_vm = child;
                    return;
                }
            }
        }

        private void showRackView(rack rk)
        {
            catalog c = g.catalog_list.Find(at => at.catalog_id == rk.rack_catalog_id);
            if (c == null)
            {
                return;
            }
            _ctlRack.MyItemsSource = null;

            _ctlRack.RackName = rk.rack_name;
            int unit_size = c.rm_unit_size ?? 0;
            _ctlRack.TotalUnit = unit_size;

            int _pixel_4_unit = 15;
            _ctlRack.PixelPerUnit = _pixel_4_unit;
            _ctlRack.Width = _pixel_4_unit * 11 + 20 + 10 + 10 + 20;
            _ctlRack.Height = _pixel_4_unit * unit_size + 20 + 20;
            int ss = _pixel_4_unit = 440; // >= 30 ? 440 : 220; // romee 2/23
            _ctlRack.SelectedImage = ss;

            List<RackVM> _list_mount_vm = new List<RackVM>();
            rack_lib = new RackLib(rk.rack_id, null, _list_mount_vm);
            rack_lib.dispRack();
            _ctlRack.MyItemsSource = _list_mount_vm;
        }

        #endregion

        #region // AssetView_Asset

        private void AssetView_AssetInit(AssetTreeVM as_vm)
        {
            asset ast = g.asset_list.Find(at => at.asset_id == as_vm.asset_id);
            if (ast == null) return;

            location ast_l = g.location_list.Find(at => at.location_id == ast.location_id);
            if (ast_l == null) return;

            room rm = g.room_list.Find(at => at.room_id == ast_l.room_id);
            if (rm == null) return;

            floor fl = g.floor_list.Find(at => at.floor_id == rm.floor_id);
            if (fl == null) return;

            //현재 사용되는 buidling AssetTreeVM 저장한다
            AssetTreeVM rm_vm = getParentAssetTreeVM(as_vm, as_vm.disp_level);
            if (rm_vm == null) return;
            AssetTreeVM fl_vm = getParentAssetTreeVM(rm_vm, rm_vm.disp_level);
            if (fl_vm == null) return;
            use_fl_vm = fl_vm;
            use_rm_vm = rm_vm;
            use_asset_vm = as_vm;

            //3d 화면 draw
            drawing_3d dr = g.drawing_3d_list.Find(at => at.drawing_3d_id == fl.drawing_3d_id);

            string file_path = string.Format("{0}drawing_3d/{1}", g.CLIENT_IMAGE_PATH, dr.file_name);
            

            //_ctlDrawingView3D.openDrawingFile(file_path);
            //use_drawing_file_path = file_path;
            ////현제까지 적용된 UI를 강재로 Action 시킨다
            Dispatcher.Invoke(new Action(() => { }), DispatcherPriority.ContextIdle, null);

            //drawRoomInfo();
            //_ckbxRoomInfoShow.IsChecked = true;
            drawFloorAllObject(fl_vm);

            //기본정보 표시
            _txbTop2Name.Text = as_vm.disp_name;

            // 링크다이어그램 표시를 위해 선택된 AssetTreeVM의 포트를 바인딩
            _lvPortList.ItemsSource = null;
            AssetTreeVM ast_vm_md = g.asset_ast_vm_dic[as_vm.asset_id ?? 0];
            _lvPortList.ItemsSource = ast_vm_md.child_list;

            // 링크다이어그램을 표시...
            showLinkView(ast.asset_id, 1);

            //3D 화면에서 선택된 Asset을 강죠 표시
            _ctlDrawingView3D.selectAsset(ast.asset_id);

            //카메라를 Asset 중심으로 이동
            //Point center_p = new Point((ast.pos_x ?? 0) / 100, (ast.pos_y ?? 0) / 100);
            //moveCamera(center_p, 100);

            _ctlDrawingView3D.openDrawingFile(file_path);
            use_drawing_file_path = file_path;

            Point start_p = new Point(((Double)(rm.square_x1 ?? 0)) / 100, ((Double)(rm.square_y1 ?? 0)) / 100);
            Point end_p = new Point(((Double)(rm.square_x2 ?? 0)) / 100, ((Double)(rm.square_y2 ?? 0)) / 100);
            Point center_p = new Point((start_p.X + end_p.X) / 2, (start_p.Y + end_p.Y) / 2);
            Double room_width = (Math.Abs(end_p.X - start_p.X) * 2);
            moveCamera(center_p, room_width);
            // _ckbxRackAssetInfoShow.IsChecked = true;
        }


        private void AssetView_AssetInitFromRoom(AssetTreeVM as_vm)
        {
            //현재 사용되는 buidling AssetTreeVM 저장한다
            use_asset_vm = as_vm;
            asset ast = g.asset_list.Find(at => at.asset_id == as_vm.asset_id);
            if (ast == null)
                return;
            int location_id = ast.location_id;
            var lo = g.location_list.Find(p => p.location_id == location_id);
            if (lo == null)
                return;
            room rm = g.room_list.Find(at => at.room_id == lo.room_id);
            if (rm == null) return;

            //기본정보 표시
            _txbTop2Name.Text = as_vm.disp_name;


            // 링크다이어그램 표시를 위해 선택된 AssetTreeVM의 포트를 바인딩
            _lvPortList.ItemsSource = null;
            AssetTreeVM ast_vm_md = g.asset_ast_vm_dic[as_vm.asset_id ?? 0];
            _lvPortList.ItemsSource = ast_vm_md.child_list;

            // 링크다이어그램을 표시...
            showLinkView(ast.asset_id, 1);

            //3D 화면에서 선택된 Asset을 강죠 표시
            _ctlDrawingView3D.selectAsset(ast.asset_id);

            //카메라를 Asset 중심으로 이동
            //Point center_p = new Point((ast.pos_x ?? 0) / 100, (ast.pos_y ?? 0) / 100);
            //moveCamera(center_p, 100);
            Point start_p = new Point(((Double)(rm.square_x1 ?? 0)) / 100, ((Double)(rm.square_y1 ?? 0)) / 100);
            Point end_p = new Point(((Double)(rm.square_x2 ?? 0)) / 100, ((Double)(rm.square_y2 ?? 0)) / 100);
            Point center_p = new Point((start_p.X + end_p.X) / 2, (start_p.Y + end_p.Y) / 2);
            Double room_width = (Math.Abs(end_p.X - start_p.X) * 2);
            moveCamera(center_p, room_width);
            //_ckbxRackAssetInfoShow.IsChecked = true;
        }


        private void AssetView_AssetClearGotoRoom()
        {
            _ctlDrawingView3D.releaseAsset(use_asset_vm.asset_id ?? 0);
            _ctlLink.Clear();

            //_ckbxRoomInfoShow.IsChecked = false;
            //_ckbxRackAssetInfoShow.IsChecked = false;
            //_ckbxUserPortInfoShow.IsChecked = false;
        }

        private void AssetView_AssetClear()
        {
            up_list.Clear();
            //moveCameraFocusFloor();

            _ctlDrawingView3D.clearItemAll();

            //_ckbxRoomInfoShow.IsChecked = false;
            //_ckbxRackAssetInfoShow.IsChecked = false;
            //_ckbxUserPortInfoShow.IsChecked = false;

            _ctlLink.Clear();
        }
        #endregion

        #region // 화면 그리기 처리 
        // 그리기 
        private void drawFloorAllObject(AssetTreeVM fl_vm)
        {

            if (fl_vm == null) return;
            foreach(var rm_vm in fl_vm.child_list)
            {
                foreach(var ast_vm in rm_vm.child_list)
                {
                    switch (ast_vm.type)
                    {
                        case AssetTreeType.Rack:
                            rack rk = g.rack_list.Find(at => at.rack_id == ast_vm.type_id);
                            if( (rk!= null) &&(rk.pos_x !=null))
                                _ctlDrawingView3D.addRack(rk);
                            break;
                        case AssetTreeType.FacePlate:
                        case AssetTreeType.MutoaBox:
                        case AssetTreeType.ConsolidationPoint:
                            asset ast = g.asset_list.Find(at => at.asset_id == ast_vm.type_id);
                            if( (ast != null )&& (ast.pos_x != null))
                            {
                                _ctlDrawingView3D.addAsset(ast.asset_id, CatalogType.getCatalogType(ast.catalog_id), new Point(ast.pos_x ?? 0, ast.pos_y ?? 0));
#if false
                                List<user_port_layout> tmp_up_list = g.user_port_layout_list.FindAll(at => at.asset_id == ast.asset_id);
                                if (tmp_up_list.Count != 0)
                                {
                                    up_list.AddRange(tmp_up_list);
                                    foreach (user_port_layout up in tmp_up_list)
                                    {
                                        if (up.is_layout == "Y")
                                            _ctlDrawingView3D.addUserPort(up.user_port_layout_id, up.port_no, new Point(up.pos_x ?? 0, up.pos_y ?? 0), new Point((Double)(ast.pos_x ?? 0), (Double)(ast.pos_y ?? 0)), true);
                                    }
                                } 
#else
                                if ((ast_vm.type == AssetTreeType.FacePlate) || (ast_vm.type == AssetTreeType.MutoaBox))
                                {
                                    foreach(var port_ast_vm in ast_vm.child_list)
                                    {
                                        List<asset_terminal> ast_t_list = g.asset_terminal_list.FindAll(at =>
                                        (at.cur_outlet_asset_id == port_ast_vm.asset_id) && (at.cur_outlet_port_no == port_ast_vm.port_no));
                                        
                                        user_port_layout up = g.user_port_layout_list.Find(at => (at.asset_id == ast_vm.asset_id)&&(at.port_no==port_ast_vm.port_no));
                                        //if ((up != null) && (ast_t_list.Count!=0))
                                        if ( (up != null)  )
                                        {
                                            if (up.is_layout == "Y")
                                            {
                                                String img_path;
                                                //1개의 pc가 있는경우는 PC 그 이상인 겨우 HUB로 표시한다
                                                if (ast_t_list.Count == 1)
                                                    img_path = "/I2MS2;component/Icons/pc_on_16.png";
                                                else
                                                    img_path = "/I2MS2;component/Icons/hub_16.png";
                                                _ctlDrawingView3D.addUserPort(
                                                    up.user_port_layout_id, up.port_no, new Point(up.pos_x ?? 0, up.pos_y ?? 0),
                                                    new Point((Double)(ast.pos_x ?? 0), (Double)(ast.pos_y ?? 0)), img_path,
                                                    port_ast_vm.child_list);
                                            }
                                        }
                                    }
                                }
#endif
                            }
                            break;
                    }
                }
            }
        }
        #endregion

        #region // 좌측 리스트 
        
        private void _lvSubAssetList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AssetTreeVM vm = (AssetTreeVM) _lvSubAssetList.SelectedItem;
            if (vm == null)
                return;
            display_property(vm);
        }

        private void display_property(AssetTreeVM vm)
        {
            if (vm == null)
                return;
            int asset_id = vm.asset_id ?? 0;
            int location_id = vm.location_id;
            int disp_level = vm.disp_level;
            g.prop_data.force_clear = true;
            if (asset_id > 0)
                g.main_window._ctlRightSide.dispAssetProperty(asset_id);

            g.main_window._ctlRightSide.dispLocationProperty(location_id, disp_level);
            g.prop_data.force_changed = true;
        }

        private void _lvSubAssetList_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if ((_lvSubAssetList.SelectedItem == null) || !(_lvSubAssetList.SelectedItem is AssetTreeVM))
                return;


            _lvSubAssetList.IsEnabled = false;

            switch (asset_view_mode)
            {
                case assetViewMode.SITE:
                    changeUIAssetViewMode(assetViewMode.BUILDING);
                    AssetTreeVM b_vm = (AssetTreeVM)_lvSubAssetList.SelectedItem;
                    AssetView_BuildingInit(b_vm);
                    display_property(b_vm);
                    break;

                case assetViewMode.BUILDING:
                    changeUIAssetViewMode(assetViewMode.FLOOR);
                    AssetTreeVM fl_vm = (AssetTreeVM)_lvSubAssetList.SelectedItem;
                    AssetView_FloorInit(fl_vm);
                    display_property(fl_vm);
                    break;
                case assetViewMode.FLOOR:
                    changeUIAssetViewModeExceptMain(assetViewMode.ROOM);
                    AssetView_FloorClear_GotoRoom();
                    AssetTreeVM rm_vm = (AssetTreeVM)_lvSubAssetList.SelectedItem;
                    AssetView_RoomInitFromFloor(rm_vm);
                    display_property(rm_vm);
                    break;
                case assetViewMode.ROOM:
                    AssetTreeVM ast_vm = (AssetTreeVM)_lvSubAssetList.SelectedItem;
                    if (ast_vm.type == AssetTreeType.Rack)
                    {
                        //Rack인경우
                        AssetView_RoomClearGotoRackAsset();
                        changeUIAssetViewModeExceptMain(assetViewMode.RACK);
                        AssetView_RackInitFromRoom(ast_vm);
                    }
                    else
                    {
                        //Asset인경우 그러나 Rack에 포함되지 않은 F/P, M/B등이다
                        AssetView_RoomClearGotoRackAsset();
                        changeUIAssetViewModeExceptMain(assetViewMode.ASSET);
                        AssetView_AssetInitFromRoom(ast_vm);
                    }
                    display_property(ast_vm);
                    break;
            }
            _lvSubAssetList.IsEnabled = true;
        }
        #endregion

        #region // 버튼 처리 ,뒤로 가기 버튼이 눌렸을 때...
        private void _btnBack_Cliecked(object sender, RoutedEventArgs e)
        {
            switch (asset_view_mode)
            {
                case assetViewMode.SITE:
                    break;
                case assetViewMode.BUILDING:
                    changeUIAssetViewMode(assetViewMode.SITE);
                    use_bd_vm = null;
                    AssetView_SiteInit(g.select_site.site_id);
                    display_property(use_site_vm);                    
                    break;
                case assetViewMode.FLOOR:
                    changeUIAssetViewMode(assetViewMode.BUILDING);
                    AssetView_FloorClear();
                    use_fl_vm = null;
                    if (use_bd_vm == null)
                        break;
                    AssetView_BuildingInit(use_bd_vm);
                    display_property(use_bd_vm);                    
                    break;
                case assetViewMode.ROOM:
                    changeUIAssetViewModeExceptMain(assetViewMode.FLOOR);
                    AssetView_RoomClearGotoFloor();
                    use_rm_vm = null;
                    if (use_fl_vm == null)
                        break;
                    AssetView_FloorInitFromRoom(use_fl_vm);
                    display_property(use_fl_vm);                    
                    break;

                case assetViewMode.RACK:
                    changeUIAssetViewModeExceptMain(assetViewMode.ROOM);
                    AssetView_RackClearGotoRoom();
                    if (use_rm_vm == null)
                        break;
                    AssetView_RoomInitFromRack(use_rm_vm);
                    use_rk_vm = null;
                    display_property(use_rm_vm);                    
                    break;
                case assetViewMode.ASSET:
                    changeUIAssetViewModeExceptMain(assetViewMode.ROOM);
                    AssetView_AssetClearGotoRoom();
                    if (use_rm_vm == null)
                        break;
                    use_asset_vm = null;
                    AssetView_RoomInitFromRack(use_rm_vm);
                    display_property(use_rm_vm);                    
                    break;
            }
        }
        #endregion

        #region // 3D 도면상 보이기 / 안보이기 처리 
        private void _ckbxRoomInfoShow_Checked(object sender, RoutedEventArgs e)
        {
            drawRoomInfo();
            // GS_DEL
            regP4Info[0] = 1;
            Reg.save_dashboard(regP4Info, "regP4Info");
        }

        private void _ckbxRoomInfoShow_Unchecked(object sender, RoutedEventArgs e)
        {
            clearRoomInfo();
            regP4Info[0] = 0;
            Reg.save_dashboard(regP4Info, "regP4Info");
        }

        private void _ckbxRackAssetInfoShow_Checked(object sender, RoutedEventArgs e)
        {
            drawRackInfo();
            drawAssetInfo();
            regP4Info[1] = 1;
            Reg.save_dashboard(regP4Info, "regP4Info");
        }

        private void _ckbxRackAssetInfoShow_Unchecked(object sender, RoutedEventArgs e)
        {
            clearRackInfo();
            clearAssetInfo();
            regP4Info[1] = 0;
            Reg.save_dashboard(regP4Info, "regP4Info");
        }

        private void _ckbxUserPortInfoShow_Checked(object sender, RoutedEventArgs e)
        {
            drawUserPortInfo();
            regP4Info[2] = 1;
            Reg.save_dashboard(regP4Info, "regP4Info");
        }

        private void _ckbxUserPortInfoShow_Unchecked(object sender, RoutedEventArgs e)
        {
            clearUserPortInfo();
            regP4Info[2] = 0;
            Reg.save_dashboard(regP4Info, "regP4Info");
        }
        #endregion

        #region // 도면 그리기 처리 , 룸, 랙 , 자산 

        private void clearInfoViewAll()
        {
            _ctlDrawingView3D.clearRoomInfoAll();
            _ctlDrawingView3D.clearRackInfoAll();
            _ctlDrawingView3D.clearAssetInfoAll();
            _ctlDrawingView3D.clearUserPortInfoAll();

        }

        private void drawInfoViewAll()
        {
            if (_ckbxRoomInfoShow.IsChecked ?? false)
                drawRoomInfo();

            if (_ckbxRackAssetInfoShow.IsChecked ?? false)
            {
                drawRackInfo();
                drawAssetInfo();
            }

            if (_ckbxUserPortInfoShow.IsChecked ?? false)
                drawUserPortInfo();
        }

        private void drawRoomInfo()
        {
            if (use_fl_vm != null)
            {
                List<room> rm_list = g.room_list.FindAll(at => at.floor_id == use_fl_vm.type_id);
                foreach (var rm in rm_list)
                {
                    if( (rm.square_x1!=null)&&(rm.flag_x !=null))
                        _ctlDrawingView3D.drawRoomInfo(rm);
                }
            }
        }

        private void clearRoomInfo()
        {
            _ctlDrawingView3D.clearRoomInfoAll();
        }

        private void drawRackInfo()
        {
            if (use_fl_vm != null)
            {
                if (use_rm_vm != null)
                {
                    List<rack> rk_list = g.rack_list.FindAll(at => at.room_id == use_rm_vm.type_id);
                    foreach (var rk in rk_list)
                    {
                        if (rk.pos_x != null)
                            _ctlDrawingView3D.drawRackInfo(rk);
                    }
                }
                else
                {
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
            }
        }

        private void clearRackInfo()
        {
            _ctlDrawingView3D.clearRackInfoAll();
        }


        private void drawAssetInfo()
        {
            if (use_fl_vm == null)
                return;

            if (use_rm_vm != null)
            {
                if (use_rm_vm.child_list.Count == 0)
                    return;
                foreach (var ast_vm in use_rm_vm.child_list)
                {
                    if ((ast_vm.type == AssetTreeType.FacePlate)
                        || (ast_vm.type == AssetTreeType.ConsolidationPoint)
                        || (ast_vm.type == AssetTreeType.MutoaBox)
                        )
                    {
                        asset ast = g.asset_list.Find(at => at.asset_id == ast_vm.type_id);
                        _ctlDrawingView3D.drawAssetInfo(ast, ast_vm.type);
                        Console.WriteLine("{0}", ast.asset_name);

                    }
                }
            }
            else
            {
                if (use_fl_vm.child_list.Count == 0)
                    return;
                foreach (var rm_vm in use_fl_vm.child_list)
                {
                    if (rm_vm.child_list.Count != 0)
                    {
                        foreach (var ast_vm in rm_vm.child_list)
                        {
                            if ((ast_vm.type == AssetTreeType.FacePlate)
                                || (ast_vm.type == AssetTreeType.ConsolidationPoint)
                                || (ast_vm.type == AssetTreeType.MutoaBox))
                            {
                                asset ast = g.asset_list.Find(at => at.asset_id == ast_vm.type_id);
                                _ctlDrawingView3D.drawAssetInfo(ast, ast_vm.type);
                                Console.WriteLine("{0}", ast.asset_name);
                            }
                        }
                    }
                }

            }

        }

        private void clearAssetInfo()
        {
            _ctlDrawingView3D.clearAssetInfoAll();
        }



        private void drawUserPortInfo()
        {
            if (use_fl_vm == null)
                return;

            if (use_rm_vm != null)
            {
                if (use_rm_vm.child_list.Count == 0)
                    return;
                foreach (var ast_vm in use_rm_vm.child_list)
                {
                    if ((ast_vm.type == AssetTreeType.FacePlate)
                        || (ast_vm.type == AssetTreeType.ConsolidationPoint)
                        || (ast_vm.type == AssetTreeType.MutoaBox))
                    {
                        List<user_port_layout> tmp_up_list = g.user_port_layout_list.FindAll(at => at.asset_id == ast_vm.asset_id);
                        if (tmp_up_list.Count != 0)
                        {
                            foreach (var up in tmp_up_list)
                            {
                                _ctlDrawingView3D.drawUserPortInfo(ast_vm.disp_name, up);
                            }
                        }
                    }
                }
            }
            else
            {
                if (use_fl_vm.child_list.Count == 0)
                    return;
                foreach (var rm_vm in use_fl_vm.child_list)
                {
                    if (rm_vm.child_list.Count != 0)
                    {
                        foreach (var ast_vm in rm_vm.child_list)
                        {
                            if ((ast_vm.type == AssetTreeType.FacePlate)
                                || (ast_vm.type == AssetTreeType.ConsolidationPoint)
                                || (ast_vm.type == AssetTreeType.MutoaBox))
                            {
                                List<user_port_layout> tmp_up_list = g.user_port_layout_list.FindAll(at => at.asset_id == ast_vm.asset_id);
                                if (tmp_up_list.Count != 0)
                                {
                                    foreach (var up in tmp_up_list)
                                    {
                                        _ctlDrawingView3D.drawUserPortInfo(ast_vm.disp_name, up);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void clearUserPortInfo()
        {
            _ctlDrawingView3D.clearUserPortInfoAll();
        }
        #endregion

        #region // 데이터 처리 , 부모 찾기, 자산 찾기 
        
        // 해당 자산의 부모 가져오기 처리 
        private AssetTreeVM getParentAssetTreeVM(AssetTreeVM ast_vm, int disp_level)
        {
            int location_id = ast_vm.location_id;
            int asset_id = ast_vm.asset_id ?? 0;
            AssetTreeType type = ast_vm.type;

            location l = g.location_list.Find(at => at.location_id == location_id);

            int p_level = disp_level - 1;
            location p_l;
            switch (p_level)
            {
                case 1:
                    region1 r1 = g.region1_list.Find(at => at.region1_id == l.region1_id);
                    if (r1 == null)
                        return null;
                    p_l = g.location_list.Find(at =>
                        (at.region1_id == r1.region1_id) && (at.region2_id == null));
                    break;
                case 2:
                    region2 r2 = g.region2_list.Find(at => at.region2_id == l.region2_id);
                    if (r2 == null)
                        return null;
                    p_l = g.location_list.Find(at =>
                        (at.region2_id == r2.region2_id) && (at.site_id == null));
                    break;
                case 3:
                    site s = g.site_list.Find(at => at.site_id == l.site_id);
                    if (s == null)
                        return null;
                    p_l = g.location_list.Find(at =>
                        (at.site_id == s.site_id) && (at.building_id == null));
                    break;
                case 4:
                    building bd = g.building_list.Find(at => at.building_id == l.building_id);
                    if (bd == null)
                        return null;
                    p_l = g.location_list.Find(at =>
                        (at.building_id == bd.building_id) && (at.floor_id == null));
                    break;
                case 5:
                    floor fl = g.floor_list.Find(at => at.floor_id == l.floor_id);
                    if (fl == null)
                        return null;
                    p_l = g.location_list.Find(at =>
                        (at.floor_id == fl.floor_id) && (at.room_id == null));
                    break;
                case 6:
                    room rm = g.room_list.Find(at => at.room_id == l.room_id);
                    if (rm == null)
                        return null;
                    p_l = g.location_list.Find(at =>
                        (at.room_id == rm.room_id) && (at.rack_id == null));
                    break;
                case 7:
                    if (l.rack_id != null)
                    {
                        //rack인경우
                        rack rk = g.rack_list.Find(at => at.rack_id == l.rack_id);
                        if (rk == null)
                            return null;
                        p_l = g.location_list.Find(at =>
                            (at.rack_id == rk.rack_id));
                        break;
                    }
                    else
                    {
                        //자산인 경우
                        AssetTreeVM l_ast_vm = getAssetTreeVMinDic(location_id);
                        if (l_ast_vm == null)
                            return null;
                        return l_ast_vm.child_list.Find(at => at.asset_id == asset_id);
                        //return getAssetTreeVMinDic(location_id);
                    }
                case 8:
                    //if (l.rack_id != null)
                    {
                        //자산 //자산인 경우
                        AssetTreeVM l_ast_vm = getAssetTreeVMinDic(location_id);
                        if (l_ast_vm == null)
                            return null;
                        return l_ast_vm.child_list.Find(at => at.asset_id == asset_id);
                    }
                //        else
                //        {
                //            //포트
                //            AssetTreeVM l_ast_vm = getAssetTreeVMinDic(location_id);
                //            if (l_ast_vm == null)
                //                return null;

                //            return l_ast_vm.child_list.Find(at => at.asset_id == asset_id);
                //        }

                //case 9:
                //    //포트
                //    AssetTreeVM l_ast_vm2 = getAssetTreeVMinDic(location_id);
                //    if (l_ast_vm2 == null)
                //        return null;

                //    return l_ast_vm2.child_list.Find(at => at.asset_id == asset_id);


                default:
                    return null;
            }

            try
            {
                AssetTreeVM p_ast_vm = g.location_ast_vm_dic[p_l.location_id];
                return p_ast_vm;
            }
            catch (Exception ex) { Console.WriteLine("{0}", ex.Message); return null; }


        }
        // 로케이션 아이디로 모델 가져오기 
        private AssetTreeVM getAssetTreeVMinDic(int location_id)
        {
            if (location_id == 0)
                return null;

            try
            {
                return g.location_ast_vm_dic[location_id];
            }
            catch (Exception) { return null; }
        }
        #endregion

        #region // 상단 차트 처리 용           romee 2/23
        private void setChartData(AssetTreeType type, int id)
        {
//            int tot_pp = 1;
//            int used_pp = 1;
            int tot_ipp = 1;
            int used_ipp = 1;
            int tot_up = 1;
            int used_up = 1;
            int sw_tot = 1;
            int sw_used = 1;
            int rack_tot = 1;
            int rack_used = 1;

            //각 차트에 데이터 적용 => location 쪽 db 완성되면 코드 추가 필요
            switch (type)
            {
                case AssetTreeType.Site:
//                    tot_pp = Stat.get_tot_pp_ports_by_site_id(id);
//                    used_pp = Stat.get_used_pp_ports_by_site_id(id);
//                    used_ipp = Stat.get_used_ipp_ports_by_site_id(id);
                    tot_up = Stat.get_tot_user_ports_by_site_id(id);
                    used_up = Stat.get_used_user_ports_by_site_id(id);
                    break;
                case AssetTreeType.Building:
                    tot_up = Stat.get_tot_user_ports_by_building_id(id);
                    used_up = Stat.get_used_user_ports_by_building_id(id);
                    break;
                case AssetTreeType.Floor:
                    tot_up = Stat.get_tot_user_ports_by_floor_id(id);
                    used_up = Stat.get_used_user_ports_by_floor_id(id);
                    break;
                case AssetTreeType.Room:
                    tot_up = Stat.get_tot_user_ports_by_room_id(id);
                    used_up = Stat.get_used_user_ports_by_room_id(id);
//                    var s1 = await Stat.get_tot_terminal_by_site_id(g.selected_site_id);
//                    var s2 = await Stat.get_used_terminal_by_site_id(g.selected_site_id);
                    break;
                case AssetTreeType.Rack:
                    tot_up = Stat.get_tot_user_ports_by_rack_id(id);
                    used_up = Stat.get_used_user_ports_by_rack_id(id);
                    break;
                default:
                    break;
            }
            tot_ipp = Stat.get_tot_ipp_ports_by_site_id(id, type);
            used_ipp = Stat.get_used_ipp_ports_linked_by_site_id(id, type);
            sw_tot = Stat.get_tot_sw_ports_by_site_id(id, type);
            sw_used = Stat.get_used_sw_ports_by_site_id(id, type);
            rack_tot = Stat.get_tot_rack_by_site_id(id, type);
            rack_used = Stat.get_used_rack_by_site_id(id, type);

            if (tot_ipp == 0) tot_ipp = 1;
            if (used_ipp == 0) used_ipp = 0;
            if (tot_up == 0) tot_up = 1;
            if (used_up == 0) used_up = 0;
            if (sw_tot == 0) sw_tot = 1;
            if (sw_used == 0) sw_used = 0;
            if (rack_tot == 0) rack_tot = 1;
            if (rack_used == 0) rack_used = 0;

            try { 
                _ctlPieChart.showBarChart(g.tr_get("D_Stat3"), rack_used, rack_tot, App.Current.Resources["_brushBlue"] as Brush);
                _ctlNormalBarChart.showBarChart(g.tr_get("D_Stat2"), sw_used, sw_tot, App.Current.Resources["_brushGray"] as Brush);
                _ctlIntelliBarChart.showBarChart(g.tr_get("D_Stat1"), used_ipp, tot_ipp, App.Current.Resources["_brushBlue"] as Brush);
                _ctlUserPortBarChart.showBarChart(g.tr_get("C_User_Port_Utilization"), used_up, tot_up, App.Current.Resources["_brushRed"] as Brush);
            }
            catch { }
        }
        #endregion

        #region // 상단 링크다이어그램 변경 처리 
        private void showLinkView(int asset_id, int port_no)
        {
            asset_id_for_link_diagram = asset_id;
            port_no_for_link_diagram = port_no;
            _ctlLink.Show(asset_id, port_no);
        }

        //렉뷰, 자산뷰 에서 자산의 포트 선택이 변경된 경우 실행
        private void _lvPortList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_lvPortList.SelectedItem == null)
                return;

            AssetTreeVM port_vm = (AssetTreeVM)_lvPortList.SelectedItem;
            showLinkView(port_vm.asset_id ?? 0, port_vm.port_no);
        }
        
        // 포트 상태가 바뀌면 이 루틴이 호출된다.
        public void update_link_info(int asset_id, int port_no)
        {
            // 링크다이어그램쪽 포트 상태 변화...
            update_link_diagram_port_status(asset_id, port_no);
        }

        // 포트 상태가 바뀌면 이 루틴이 호출된다.
        public void update_port_status(int asset_id, int port_no, ePortStatus status)
        {
            // 링크다이어그램쪽 포트 상태 변화...
            update_link_diagram_port_status(asset_id, port_no);

            if (asset_view_mode == assetViewMode.RACK)
                // 인텔리전트 패치 패널에 대해 랙포트 상태 갱신
                rack_lib.update_port_status(asset_id, port_no);
        }

        // 알람 상태가 바뀌면 이 루틴이 호출된다.
        public void update_alarm_status(int asset_id, int port_no, eAlarmStatus status)
        {
            // 링크다이어그램쪽 포트 상태 변화...
            update_link_diagram_port_status(asset_id, port_no);

            if (asset_view_mode == assetViewMode.RACK)
                // 인텔리전트 패치 패널에 대해 랙포트 상태 갱신
                rack_lib.update_alarm_status(asset_id, port_no);
        }
        // 링크다이어그램쪽 포트 상태 변화...
        private void update_link_diagram_port_status(int asset_id, int port_no)
        {
            if ((asset_view_mode == assetViewMode.RACK) || (asset_view_mode == assetViewMode.ASSET) || (asset_view_mode == assetViewMode.ASSET_IN_RACK))
            {
                if (select_as_vm != null)
                {
                    if ((asset_id_for_link_diagram == asset_id) && (port_no_for_link_diagram == port_no)) // 상단 자산아이디와 포트 번호이면 상단 변경 처리 
                        showLinkView(asset_id, port_no);
                }
            }
        }

        // 랙안에서 장치들을 클릭했을 때.
        private void _ctlRack_SelectionChanged(object sender, RoutedEventArgs e)
        {
            RackVM vm = (RackVM)_ctlRack._lvSlots.SelectedItem;
            if (vm == null)
                return;

            int asset_id = vm.asset_id;
            int location_id = Etc.get_location_id_by_asset_id(asset_id);
            if (asset_id > 0)
            {
                g.prop_data.force_clear = true;
                g.main_window._ctlRightSide.dispAssetProperty(asset_id);
                g.prop_data.force_changed = true;
            }
            g.main_window._ctlRightSide.dispLocationProperty(location_id);

            AssetTreeVM rk_vm = null;
            if (g.location_ast_vm_dic.ContainsKey(location_id))
                rk_vm = g.location_ast_vm_dic[location_id];
            if (rk_vm != null)
            {
                foreach (var child in rk_vm.child_list)
                {
                    if (child.asset_id == asset_id)
                    {
                        _ctlLink.Show(child.asset_id ?? 0, 1);
                        select_as_vm = child;
                        _txbTop2Name.Text = child.disp_name;
                        _lvPortList.ItemsSource = child.child_list;
                        return;
                    }
                }
            }

            // 링크다이어그램 갱신
            _ctlLink.Show(asset_id, 1);
        }
        #endregion

        #region // 화면 다시 그리기 처리 
        
        // 새로 고침 처리 3D 화면 처리용 
        private void _btnReflash_Click(object sender, RoutedEventArgs e)
        {
            reflashAll();
        }
        // 전체 고침 처리 
        public void reflashAll()
        {
            AssetView_SiteInit(g.select_site.site_id);
            drawFloorAllObject(use_fl_vm);

            if (use_fl_vm != null)
            {

                floor fl = g.floor_list.Find(at => at.floor_id == use_fl_vm.type_id);
                if (fl == null) return;

                drawing_3d dr = g.drawing_3d_list.Find(at => at.drawing_3d_id == fl.drawing_3d_id);
                if (dr == null) return;

                string file_path = string.Format("{0}drawing_3d/{1}", g.CLIENT_IMAGE_PATH, dr.file_name);

                if (file_path != use_drawing_file_path)
                {
                    _ctlDrawingView3D.clearItemAll();
                    _ctlDrawingView3D.removeAllWall();
                    _ctlDrawingView3D.openDrawingFile(file_path);
                }
                reflashInfoviewAll();
            } 
        }

        public void reflashInfoviewAll()
        {
            clearInfoViewAll();
            drawInfoViewAll();
        }

        private void _p4_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            reflashInfoviewAll();
        }
        #endregion

        internal void update_chart(eEventCode code)
        {
            switch (code)
            {
                case eEventCode.eUnauthorizedPlug:
                case eEventCode.eUnauthorizedUnplug:
                case eEventCode.eRestorePlug:
                case eEventCode.eRestoreUnplug:
                    // 대시보드 갱신
                    setChartData(atype, aid);
                    break;
            }
            return;
        }
    }
}

