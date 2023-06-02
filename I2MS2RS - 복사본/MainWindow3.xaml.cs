using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Controls;
using I2MS2.Models;
using I2MS2.Pages;
using I2MS2.Animation;
using I2MS2.Windows;

using WebApi.Models;

using I2MS2.Library;
using I2MS2.UserControls;
using System.Globalization;
using Microsoft.AspNet.SignalR.Client;
using MetroDemo;
using MahApps.Metro.Controls.Dialogs;
using MetroDemo.ExampleWindows;
using MahApps.Metro.Controls;
using MetroDemo.ExampleViews;
using I2MS2.Chart;
using System.Windows.Threading;

namespace I2MS2
{

    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow3 
    {
        Boolean isReadyMainRun = false;
        private readonly MainWindowViewModel2 _viewModel;

        #region RouteCommand 버튼 관련 정의
        public static RoutedCommand SelectCommand = new RoutedCommand();
        public static RoutedCommand ViewFloorIPMCommand = new RoutedCommand();
        public static RoutedCommand ViewIPMCommand = new RoutedCommand();
        public static RoutedCommand SetIPMCommand = new RoutedCommand();
        public static RoutedCommand AssetListMenuCommand = new RoutedCommand();
        public static RoutedCommand LineLinkListMenuCommand = new RoutedCommand();
        public static RoutedCommand LocationListMenuCommand = new RoutedCommand();
        public static RoutedCommand CatalogListMenuCommand = new RoutedCommand();
        public static RoutedCommand ManufactureListMenuCommand = new RoutedCommand();
        public static RoutedCommand UserListMenuCommand = new RoutedCommand();
        public static RoutedCommand EventListMenuCommand = new RoutedCommand();
        public static RoutedCommand WorkOrderListMenuCommand = new RoutedCommand();
        public static RoutedCommand EnvironmentListMenuCommand = new RoutedCommand();
        public static RoutedCommand StatTerminalMenuCommand1 = new RoutedCommand();
        public static RoutedCommand StatTerminalMenuCommand2 = new RoutedCommand();
        public static RoutedCommand StatTerminalMenuCommand3 = new RoutedCommand();

        public static RoutedCommand AddAssetCommand = new RoutedCommand();
        public static RoutedCommand EditAssetCommand = new RoutedCommand();

        public static RoutedCommand EnvironmentSettingMenuCommand = new RoutedCommand();
        public static RoutedCommand StatEventMenuCommand1 = new RoutedCommand();
        public static RoutedCommand StatEventMenuCommand2 = new RoutedCommand();
        public static RoutedCommand StatEventMenuCommand3 = new RoutedCommand();
        public static RoutedCommand PrintTemplateMenuCommand = new RoutedCommand();

        public static RoutedCommand ConfigICCommand = new RoutedCommand();
        public static RoutedCommand EnvironmentTargetMenuCommand = new RoutedCommand();
        
        #endregion

        #region 초기화
        public MainWindow3()
        {
            g.main_window = this;

            _viewModel = new MainWindowViewModel2(DialogCoordinator.Instance);
            DataContext = _viewModel;

            InitializeComponent();

            _windowMainWindow.Height = g.screen_resolution.Height;
            _windowMainWindow.Width = g.screen_resolution.Width;

            //DB 미연결시 시뮬레이션 코드
            //selfSimulator();

            var u = g.user_list.Find(p => p.user_id == g.login_user_id);
            string user_name = u != null ? u.user_name : "";
            _txtUserName.Text = user_name;
            connect_signalr();  // 비동기로 호출...
        }

        // 비동기 호출됨 
        private async Task<bool> connect_signalr()
        {
            // SignalR 허브로 연결
            return await g.signalr.connect(g.signalr_uri_string);
        }

        #endregion

        #region 출력 메뉴 관리 Command
        // 선번장 목록 출력 
        private void _cmdLineLinkListMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (g.select_site == null)
            {
                e.CanExecute = false; return;
            }
            e.CanExecute = UserRight.is_ok(eUserRight.eLineLinkList);
        }

        private void _cmdLineLinkListMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LineLinkList window = new LineLinkList();
            window.Owner = this;
            window.ShowDialog();
        }

        // 자산목록 출력 
        private void _cmdAssetListMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (g.select_site == null)
            {
                e.CanExecute = false; return;
            }
            e.CanExecute = UserRight.is_ok(eUserRight.eAssetList);
        }

        private void _cmdAssetListMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetList window = new AssetList();
            window.Owner = this;
            window.ShowDialog();
        }

        // 위치 목록 조회 출력 
        private void _cmdLocationListMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (g.select_site == null)
            {
                e.CanExecute = false; return;
            }
            e.CanExecute = UserRight.is_ok(eUserRight.eLocationList);
        }

        private void _cmdLocationListMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LocationList window = new LocationList();
            window.Owner = this;
            window.ShowDialog();
        }

        // 환경 정보  조회 출력 
        private void _cmdEnvironmentListMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (g.select_site == null)
            {
                e.CanExecute = false; return;
            }
            e.CanExecute = UserRight.is_ok(eUserRight.eEnvironmentList);
        }

        private void _cmdEnvironmentListMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EnvironmentList window = new EnvironmentList();
            window.Owner = this;
            window.ShowDialog();
        }


        // 카탈록 출력 
        private void _cmdCatalogListMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (g.select_site == null)
            {
                e.CanExecute = false; return;
            }
            e.CanExecute = UserRight.is_ok(eUserRight.eCatalogList);
        }

        private void _cmdCatalogListMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CatalogList window = new CatalogList();
            window.Owner = this;
            window.ShowDialog();
        }

        // 제조사 출력 
        private void _cmdManufactureListMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (g.select_site == null)
            {
                e.CanExecute = false; return;
            }
            e.CanExecute = UserRight.is_ok(eUserRight.eManufactureList);
        }

        private void _cmdManufactureListMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ManufactureList window = new ManufactureList();
            window.Owner = this;
            window.ShowDialog();
        }

        // 연락처 / 사용자 목록 출력
        private void _cmdUserListMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (g.select_site == null)
            {
                e.CanExecute = false; return;
            }
            e.CanExecute = UserRight.is_ok(eUserRight.eUserList);
        }

        private void _cmdUserListMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            UserList window = new UserList();
            window.Owner = this;
            window.ShowDialog();
        }

        // 작업오더 목록 출력
        private void _cmdWorkOrderListMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (g.select_site == null)
            {
                e.CanExecute = false; return;
            }
            e.CanExecute = UserRight.is_ok(eUserRight.eWorkOrderList);
        }

        private void _cmdWorkOrderListMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WorkOrderList window = new WorkOrderList();
            window.Owner = this;
            window.ShowDialog();
        }

        // 이벤트 목록 출력 
        private void _cmdEventListMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (g.select_site == null)
            {
                e.CanExecute = false; return;
            }
            e.CanExecute = UserRight.is_ok(eUserRight.eEventList);
        }

        private void _cmdEventListMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EventList window = new EventList();
            window.Owner = this;
            window.ShowDialog();
        }

        // 링크다이어그램 변경 목록 출력
        private void _cmdLinkDiagramChangeHistMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (g.select_site == null)
            {
                e.CanExecute = false; return;
            }
            e.CanExecute = UserRight.is_ok(eUserRight.eLinkDiagramChangeHistList);
        }

        private void _cmdLinkDiagramChangeHistMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        // 터미날 통계
        private void _cmdStatTerminalMenu1_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (g.select_site == null)
            {
                e.CanExecute = false; return;
            }
            e.CanExecute = UserRight.is_ok(eUserRight.eStatTerminal1);
        }

        private void _cmdStatTerminalMenu1_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            StatTerminalMonth window = new StatTerminalMonth();
            window.Owner = this;
            window.ShowDialog();
        }
        private void _cmdStatTerminalMenu2_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (g.select_site == null)
            {
                e.CanExecute = false; return;
            }
            e.CanExecute = UserRight.is_ok(eUserRight.eStatTerminal2);
        }

        private void _cmdStatTerminalMenu2_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            StatTerminalDay window = new StatTerminalDay();
            window.Owner = this;
            window.ShowDialog();
        }
        private void _cmdStatTerminalMenu3_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (g.select_site == null)
            {
                e.CanExecute = false; return;
            }
            e.CanExecute = UserRight.is_ok(eUserRight.eStatTerminal3);
        }

        private void _cmdStatTerminalMenu3_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            StatTerminalHour window = new StatTerminalHour();
            window.Owner = this;
            window.ShowDialog();
        }
        // 이벤트 메뉴 통계 
        private void _cmdStatEventMenu1_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (g.select_site == null)
            {
                e.CanExecute = false; return;
            }
            e.CanExecute = UserRight.is_ok(eUserRight.eStatEvent1);
        }
        private void _cmdStatEventMenu1_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            StatEnvironmentMonth window = new StatEnvironmentMonth();
            window.Owner = this;
            window.ShowDialog();
        }
        private void _cmdStatEventMenu2_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (g.select_site == null)
            {
                e.CanExecute = false; return;
            }
            e.CanExecute = UserRight.is_ok(eUserRight.eStatEvent2);
        }

        private void _cmdStatEventMenu2_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            StatEnvironmentDay window = new StatEnvironmentDay();
            window.Owner = this;
            window.ShowDialog();
        }
        private void _cmdStatEventMenu3_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (g.select_site == null)
            {
                e.CanExecute = false; return;
            }
            e.CanExecute = UserRight.is_ok(eUserRight.eStatEvent3);
        }

        private void _cmdStatEventMenu3_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            StatEnvironmentHour window = new StatEnvironmentHour();
            window.Owner = this;
            window.ShowDialog();
        }

        #endregion


        #region 좌측 트리 메뉴 관리 Command
        // 자산 추가 
        private void _cmdAddAsset_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eAddAsset))
            {
                e.CanExecute = false;
                return;
            }

            AssetTreeVM ast_vm = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (ast_vm == null)
            {
                e.CanExecute = false;
                return;
            }
            asset_tree ast = g.asset_tree_list.Find(at => at.asset_tree_id == ast_vm.asset_tree_id);
            if (ast == null)
            {
                e.CanExecute = false;
                return;
            }
            int asset_id = ast.asset_id ?? 0;

            if (asset_id != 0)
            {
                int catalog_id = Etc.get_catalog_id_by_asset_id(asset_id);
                // 새시형스위치 경우에만 자산추가가 가능..
                e.CanExecute = CatalogType.is_sw_slot(catalog_id);
                return;
            }

            e.CanExecute = (ast_vm.disp_level == 6) || (ast_vm.disp_level == 7);
        }

        private async void _cmdAddAsset_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            int asset_id = 0;
            int location_id = 0;
            AssetTreeVM ast_vm = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (ast_vm != null)
            {
                asset_tree ast = g.asset_tree_list.Find(at => at.asset_tree_id == ast_vm.asset_tree_id);
                asset_id = ast.asset_id ?? 0;
                location_id = ast.location_id;
            }

            // AssetManager에서 asset, asset_aux, rack_config, sw_config, asset_ext 를 책임진다.
            AssetManager am = new AssetManager(asset_id, location_id, true);
            am.Owner = Application.Current.MainWindow;
            var r = am.ShowDialog();

            int new_asset_id = g.result_asset_id;

            if ((r != null) && (r == true))
            {
                // 아래 공통 모듈에서 나머지 테이블들을 책임지고, 화면 갱신 등을 수행한다.
                bool b = await g.left_tree_handler.addAsset(new_asset_id);

                if (b)
                    // 다른 사용자에게도 알린다.
                    g.signalr.send_asset_to_signalr(new_asset_id, eAction.eAdd);
            }

        }
        // 자산 수정
        private void _cmdEditAsset_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eEditAsset))
            {
                e.CanExecute = false;
                return;
            }

            AssetTreeVM ast_vm = get_tree_vm();
            if (ast_vm == null)
            {
                e.CanExecute = false;
                return;
            }
            int asset_id = ast_vm.asset_id ?? 0;
            e.CanExecute = asset_id != 0;
        }

        private void _cmdEditAsset_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = get_tree_vm();
            if (ast_vm == null)
                return;
            int asset_id = ast_vm.asset_id ?? 0;
            int location_id = ast_vm.location_id;

            if (Etc.is_open_link_diagram(asset_id))
            {
                MessageBox.Show(g.tr_get("C_Error_Asset_3"));
                return;
            }

            AssetManager am = new AssetManager(asset_id, location_id, false);
            am.Owner = Application.Current.MainWindow;
            am.ShowDialog();
        }

        // 출력 템플릿 관리
        private void _cmdPrintTemplateMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (g.select_site == null)
            {
                e.CanExecute = false; return;
            }
            e.CanExecute = UserRight.is_ok(eUserRight.ePrintTemplate);
        }

        private void _cmdPrintTemplateMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PrintTemplateList window = new PrintTemplateList();
            window.Owner = this;
            window.ShowDialog();
        }


        #endregion

        #region // 윈도우 이벤트
        //윈도우 로딩 이벤트
        private void _windowMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (Reg.get_dcim() > 1) // DCIM 처리 romee
            {
                int ret1 = Reg.get_dcim();
                g.selected_site_id = ret1;
                selectSiteEvent(ret1);
            }

            if (isReadyMainRun == false)
            {
                isReadyMainRun = true;
                if (g.selected_site_id == 0)
                {
                    // 본 프로그램은 SSO를 지원합니다. 
                    MessageBox.Show(this, " Support single sign on !!", "SimpleWIN");
                    Application.Current.Shutdown(-1);
                }
                else 
                { 
                    _ctlLeftSide.InitLeftSide(g.selected_site_id);
                    g.left_tree_handler._ctlLeftSide = _ctlLeftSide;

                    g.window = new IPMFloorView(89201123);
                    g.window.Owner = Application.Current.MainWindow;

                }
            }
        }

        //Page1 Region2 에서 site를 더블클릭 했을때 호출
        //사이트 정보를 변경한다
        public void selectSiteEvent(int id)
        {
            site st = g.site_list.Find(at => at.site_id == id);
            if (st == null)
                return;
            g.select_site = st;

            try
            {
                g.selected_building_id = g.building_list.Find(p => p.site_id == id).building_id;
            }
            catch { }
        }

        //윈도우 리사이즈 이벤트
        private void _windowMainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //윈도우 사이즈 변화에 따라 페이지 프래임 사이즈를 변경한다
            // minimum 1280x1024
            if (_gridCenter.ActualWidth > 1280)
                MainTabControl.Width = _gridCenter.ActualWidth - 20;
            if (_gridCenter.ActualHeight > (800))
                MainTabControl.Height = _gridCenter.ActualHeight - 20;

            if (Application.Current.MainWindow.WindowState == WindowState.Normal)
            {
                // max -> normal bug 
                if (MainTabControl.Width > 1280)
                    MainTabControl.Width = _gridCenter.ActualWidth - 20;
                //g._dash2.InvalidateVisual();
            }
            //Console.WriteLine("Main Size : {0}", Width);
        }

        // 시그널R 재연결하는 루틴으로 디버그를 장시간(30초? 이상)하는 경우 연결이 끊기는 거에 대비하기 위한... (개발자를 위한 기능)
        // 향후 더 보강을 하려면 리커넥트를 자동으로 하는 것 정도...
        private async void _imgConnect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (g.signalr.State == ConnectionState.Disconnected)
            {
                await g.signalr.connect(g.signalr_uri_string);
            }
        }

        // 댓쉬보드 로딩시 처리 
        private void _framePage2View_LoadCompleted(object sender, NavigationEventArgs e)
        {
        }

        #endregion

        #region // 버튼 처리 , 윈도우 유틸 처리 
        // Help 처리 필요 
        // 버젼 표기 romee 2015.09.03
        // 리프레쉬 처리 
        private void _windowMainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
            }
            // 패스워드 초기화 컴맨드 2017-01-17 romee ctrl+F12
            if (e.Key == Key.F12 && Keyboard.Modifiers == ModifierKeys.Control)
            {
                user node = g.user_list.Find(p => p.login_id == "system" && p.user_group == "S");

                node.login_password = DESEncrypt.EncryptSHA_AES("system");
                g.webapi.put("user", 90001, node, typeof(user));
            }

            if (e.Key == Key.F1)
            {
                var obj = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string version = string.Format("Application Version {0}.01.{1}", obj.Major, obj.Revision);

                PopUpWindow popup_window = new PopUpWindow("Version Info. : " + version);
                popup_window.Title = "About Box";
                popup_window.Show();

                //MessageBox.Show(this, "Version : " + version);
            }
        }

        // 회사 사이트로 이동 처리 
        private void LaunchMahAppsOnCompany(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.lssimple.com/");
        }

        // 테마 버튼 클릭시 메뉴 등록 처리 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        // 로그 오프 처리 
        private void _btnLogoff_Click(object sender, RoutedEventArgs e)
        {
            //Window win = new WindowFloor();
            //win.Show();

            if (g.window.alive == 0)
            {
                g.window = new IPMFloorView(89201123);
                g.window.Owner = Application.Current.MainWindow;
            }
            g.window.Show();
            /*
                if (MessageBox.Show(g.tr_get("C_LogOutConfirm"), g.tr_get("C_Confirm"), MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
                System.Windows.Forms.Application.Restart();
                System.Windows.Application.Current.Shutdown();
            */
        }

        // 로케이션 윈도우 보이기 처리 
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var flyout = this.Flyouts.Items[0] as Flyout;
            if (flyout == null)
            {
                return;
            }
            flyout.IsOpen = !flyout.IsOpen;
        }

        // 시험 용 
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Window win = new Window1Test();
            //win.Show();

            //g._P2DashBoard._p_main.Update();
            Width = Width + 5;
        }


	    #endregion

        // 선택 
        private void _cmdSelect_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = get_tree_vm();
            if (ast_vm == null)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void _cmdSelect_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = get_tree_vm();
            if (ast_vm == null)
                return;
            AssetTreeVM ast_vm_md = _ctlLeftSide.getAssetTreeVMinMD(ast_vm);

            var l = g.location_list.Find(p => p.location_id == ast_vm.location_id);
            if (l == null)
                return;
            _selectLocation.Content = l.location_path;
            _selectLocationid.Content = l.location_id;
            //g.selected_location_id = l.location_id;
            g.DVModel.Location_Id = l.location_id;

            var flyout = this.Flyouts.Items[0] as Flyout;
            if (flyout == null) return;
            flyout.IsOpen = !flyout.IsOpen;
        }

        // 환경 장치 층별 보기 
        private void _cmdViewFloorIPM_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
#if GS_DEL
            if (g._environment != 99)
            {
                e.CanExecute = false;
                return;
            }
#endif
            if (!UserRight.is_ok(eUserRight.eViewFloorIPM))
            {
                e.CanExecute = false;
                return;
            }

            AssetTreeVM ast_vm = get_tree_vm();
            if (ast_vm == null)
            {
                e.CanExecute = false;
                return;
            }
            int location_id = ast_vm.location_id;
            var lo = g.location_list.Find(p => p.location_id == location_id);
            if (lo == null)
            {
                e.CanExecute = false;
                return;
            }
            int floor_id = lo.floor_id ?? 0;
            var f = g.floor_list.Find(p => p.floor_id == floor_id);
            if (f == null)
            {
                e.CanExecute = false;
                return;
            }
            e.CanExecute = true;
        }

        private void _cmdViewFloorIPM_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            int location_id = 0;
            AssetTreeVM ast_vm = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (ast_vm == null)
                return;

            asset_tree ast = g.asset_tree_list.Find(at => at.asset_tree_id == ast_vm.asset_tree_id);
            location_id = ast.location_id;

            var lo = g.location_list.Find(p => p.location_id == ast.location_id);
            if (lo == null)
                return;

            int floor_id = lo.floor_id ?? 0;
            var f = g.floor_list.Find(p => p.floor_id == floor_id);
            if (f == null)
                return;

            if (g.window.alive == 0)
            {
                g.window = new IPMFloorView(floor_id);
                g.window.Owner = Application.Current.MainWindow;
            }
            g.window.Show();

            var flyout = this.Flyouts.Items[0] as Flyout;
            if (flyout == null) return;
            flyout.IsOpen = !flyout.IsOpen;

        }

        // 환경 장치 보기 
        private void _cmdViewIPM_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
#if GS_DEL
            if (g._environment != 99)
            {
                e.CanExecute = false;
                return;
            }
#endif
            if (!UserRight.is_ok(eUserRight.eViewIPM))
            {
                e.CanExecute = false;
                return;
            }

            AssetTreeVM ast_vm = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (ast_vm == null)
            {
                e.CanExecute = false;
                return;
            }
            asset_tree ast = g.asset_tree_list.Find(at => at.asset_tree_id == ast_vm.asset_tree_id);
            if (ast == null)
            {
                e.CanExecute = false;
                return;
            }
            e.CanExecute = (ast.asset_id ?? 0) == 0;

            if (ast.asset_id != null)
            {
                int catalog_id = Etc.get_catalog_id_by_asset_id(ast.asset_id ?? 0);
                e.CanExecute = CatalogType.is_eb(catalog_id);
            }
        }

        private void _cmdViewIPM_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (ast_vm == null)
                return;
            asset_tree ast = g.asset_tree_list.Find(at => at.asset_tree_id == ast_vm.asset_tree_id);
            int location_id = ast.location_id;

            IPMView window = new IPMView(location_id);
            window.Owner = Application.Current.MainWindow;
            window.Show();

            var flyout = this.Flyouts.Items[0] as Flyout;
            if (flyout == null) return;
            flyout.IsOpen = !flyout.IsOpen;

        }
        // 환경 장치 설정 
        private void _cmdSetIPM_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
#if GS_DEL
            if (g._environment != 99)
            {
                e.CanExecute = false;
                return;
            }
#endif
            if (!UserRight.is_ok(eUserRight.eViewIPM))
            {
                e.CanExecute = false;
                return;
            }

            AssetTreeVM ast_vm = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (ast_vm == null)
            {
                e.CanExecute = false;
                return;
            }
            asset_tree ast = g.asset_tree_list.Find(at => at.asset_tree_id == ast_vm.asset_tree_id);
            if (ast == null)
            {
                e.CanExecute = false;
                return;
            }
            e.CanExecute = (ast.asset_id ?? 0) == 0;

            if (ast.asset_id != null)
            {
                int catalog_id = Etc.get_catalog_id_by_asset_id(ast.asset_id ?? 0);
                e.CanExecute = CatalogType.is_eb(catalog_id);
            }
        }

        private void _cmdSetIPM_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (ast_vm == null)
                return;
            asset_tree ast = g.asset_tree_list.Find(at => at.asset_tree_id == ast_vm.asset_tree_id);
            int location_id = ast.location_id;

            if (ast.asset_id == null) return;
            EnviromentManagerSet window = new EnviromentManagerSet(ast.asset_id ?? 0);
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();

            var flyout = this.Flyouts.Items[0] as Flyout;
            if (flyout == null) return;
            flyout.IsOpen = !flyout.IsOpen;

        }

        // 환경 장치 목표량 설정 
        private void _cmdEnvironmentTargetMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
#if GS_DEL
            if (g._environment != 99)
            {
                e.CanExecute = false;
                return;
            }
#endif
            if (!UserRight.is_ok(eUserRight.eViewIPM))
            {
                e.CanExecute = false;
                return;
            }
            e.CanExecute = true;
        }

        private void _cmdEnvironmentTargetMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EnviromentTarget window = new EnviromentTarget();
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();
        }

        // 트리에 따른 선택 
        private AssetTreeVM get_tree_vm()
        {
            AssetTreeVM vm = null;
            vm = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            return vm;
        }

        private void MainTabControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataViewModel tempmodel1 = new DataViewModel();

            tempmodel1.Location_Id = Etc.get_location_id_by_site_id(g.selected_site_id);

            g._dash1.DataContext = tempmodel1;
            g._dash2.DataContext = tempmodel1;
            g._dash3.DataContext = tempmodel1;

            g.DVModel = tempmodel1;
        }

        private async Task<bool> Loaded2()
        {
            bool a = await g.DVModel.OnLoaded();
            return true;
        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        // 컨트롤러 설정 
        private void _cmdConfigIC_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            if (!UserRight.is_ok(eUserRight.eConfigIC))
            {
                return;
            }

            AssetTreeVM ast_vm = get_tree_vm();
            if (ast_vm == null)
            {
                return;
            }
            int asset_id = ast_vm.asset_id ?? 0;
            if (asset_id == 0)
            {
                return;
            }
            int catalog_id = Etc.get_catalog_id_by_asset_id(asset_id);
            if (catalog_id == 412002)
                e.CanExecute = true;
            return;
            //e.CanExecute = CatalogType.is_eb(catalog_id);
        }

        private void _cmdConfigIC_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = get_tree_vm();
            if (ast_vm == null)
                return;
            int asset_id = ast_vm.asset_id ?? 0;
            int location_id = ast_vm.location_id;

            var lo = g.location_list.Find(p => p.location_id == location_id);
            if (lo == null)
                return;

            int floor_id = lo.floor_id ?? 0;

            location_id = Etc.get_location_id_by_floor_id(floor_id);

            IEMountManager window = new IEMountManager(location_id, asset_id);
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();
        }

        private void _lvEvent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
/*
            EventVM vm = _lvEvent.SelectedItem as EventVM;

            EventViewer window = new EventViewer(vm);
            window.Owner = this;
            window.ShowDialog();

            if (window._ret == 1)
            {// 트리 로케이션 처리 
                _ctlLeftSide.searchlocation(vm.location_id);
            }
            else if (window._ret == 2)
            {// 트리 자산 처리  
                _ctlLeftSide.searchasset(vm.asset_id);
            }
*/
        }
        // 선택 변경시 처리 
        private void _lvEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        public async Task<bool> updateEvent(int event_hist_id)
        {
            event_hist eh = (event_hist)await g.webapi.get("event_hist", event_hist_id, typeof(event_hist));
            if (eh == null)
                return false;

            if (eh.asset_id > 0 && String.IsNullOrEmpty(eh.ipv4)) // romee/1/22   // 서버에서 ipv4를 저장하지 않으므로 이벤트 수신시 해당 ipv4 저장 처리 
            {
                asset ast = g.asset_list.Find(p => p.asset_id == eh.asset_id); 
                if (ast != null)
                {
                    eh.ipv4 = ast.ipv4;
                }
            }
            var find = g.event_hist_list.Find(p => p.event_hist_id == event_hist_id);
            if (find == null)
                g.event_hist_list.Add(eh);

            int event_id = eh.event_id;
            var e = g.event_list.Find(p => p.event_id == event_id);
            var el = g.event_lang_list.Find(p => (p.event_id == event_id) && (p.lang_id == g.lang_id));
            if ((e == null) || (el == null))
                return false;
            bool popup_screen = e.popup_screen == "Y";
            string msg = eh.event_text;
            eEventType event_type = Common.get_event_type(eh.event_type);

            int cr = msg.IndexOf(". ");
            if (cr != -1)
                msg = msg.Insert(cr + 1, "\r\n");

            // 7건까지만 표시
            if (g.popup_event_list.Count > 7)
            {
                g.popup_event_list.RemoveAt(6);
            }

            // 이벤트가 하나도 없는 경우 새로운 이벤트가 왔으니 가장 마지막 하단은 기초 정보표시용 템플릿으로....
            if (g.popup_event_list.Count == 0)
            {
                EventVM vm0 = new EventVM();
                vm0.base_text = "Event";
                vm0.template = "base";
                g.popup_event_list.Add(vm0);
            }
            else
            {
                g.popup_event_list[g.popup_event_list.Count - 1].base_text = string.Format("Event");
            }

            EventVM vm = new EventVM();
            vm.template = "data";
            vm.event_hist_id = event_hist_id;
            vm.event_type = event_type;
            vm.event_text = msg;
            vm.location_id = eh.location_id ?? 0;
            vm.asset_id = eh.asset_id ?? 0;
            vm.port_no = eh.port_no ?? 0;
            vm.catalog_id = eh.catalog_id ?? 0;
            vm.ip_addr = eh.ipv4;
            vm.mac_addr = eh.mac;
            vm.wo_id = eh.wo_id ?? 0;
            vm.write_time = eh.write_time;
            vm.is_confirm = eh.is_confirm == "Y";
            vm.confirm_user_id = eh.confirm_user_id ?? 0;
            g.popup_event_list.Insert(0, vm);


            this.flyoutsControl.DataContext = g.popup_event_list[0];
            var flyout = this.flyoutsControl.Items[1] as Flyout;
            if (flyout == null) return false;
            flyout.IsOpen = true; // =  !flyout.IsOpen;
            var t = new DispatcherTimer(TimeSpan.FromSeconds(2), DispatcherPriority.Normal, Tick1, this.Dispatcher);

            g._dash2.EventAlarm();
            /*
            int i = 0;
            Object[] obj = new object[] { i.ToString() };
            _lvEvent.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(delegate()
                    {
                        _lvEvent.ItemsSource = null;
                        _lvEvent.ItemsSource = g.popup_event_list;
                    }));
            */
            return true;
        }

        private void _btnClose_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int idx = _lvEvent.Items.IndexOf(item);
            _lvEvent.SelectedIndex = idx;

            if (idx >= 0)
            {
                EventVM vm = _lvEvent.SelectedItem as EventVM;
                if (vm.template == "base")
                    g.popup_event_list.Clear();
                else
                {
                    if (g.popup_event_list.Count < 3)
                        g.popup_event_list.Clear();
                    else
                        g.popup_event_list.Remove(vm);
                }
                _lvEvent.ItemsSource = null;
                _lvEvent.ItemsSource = g.popup_event_list;
            }
        }

        void Tick1(object sender, EventArgs e)
        {
            var t1 = (DispatcherTimer)sender;
            t1.Stop();
            var flyout = this.flyoutsControl.Items[1] as Flyout;
            if (flyout == null) return;
            if(flyout.IsOpen)
                flyout.IsOpen = false;
        }


    }

    #region IValueConverter

    public class EventBorderConverter : IValueConverter
    {
        private Color red = (Color)App.Current.Resources["_colorRed"];         // error
        private Color green = (Color)App.Current.Resources["_colorGreen"];     // info
        private Color blue = (Color)App.Current.Resources["_colorBlue"];
        private Color yellow = (Color)App.Current.Resources["_colorYellow"];    // Warnning

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return Colors.Transparent;

            eEventType type = (eEventType)value;

            switch (type)
            {
                case eEventType.eInfo:
                    return green;
                case eEventType.eWarnning:
                    return yellow;
                case eEventType.eError:
                    return red;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }
    #endregion

}
