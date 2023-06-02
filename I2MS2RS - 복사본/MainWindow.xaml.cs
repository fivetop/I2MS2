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

namespace I2MS2
{

    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow 
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

#if GS_DEL

        public static RoutedCommand ExportManagerMenuCommand = new RoutedCommand();
        public static RoutedCommand ImportManagerMenuCommand = new RoutedCommand();
        public static RoutedCommand EnvironmentListMenuCommand = new RoutedCommand();
        public static RoutedCommand TerminalViewMenuCommand = new RoutedCommand();
        public static RoutedCommand ICFwUpgradeMenuCommand = new RoutedCommand();
        public static RoutedCommand LanguageSettingMenuCommand = new RoutedCommand();
        public static RoutedCommand MailSMSMenuCommand = new RoutedCommand();
        public static RoutedCommand EnvironmentSettingMenuCommand = new RoutedCommand();


        public static RoutedCommand StatEventMenuCommand1 = new RoutedCommand();
        public static RoutedCommand StatEventMenuCommand2 = new RoutedCommand();
        public static RoutedCommand StatEventMenuCommand3 = new RoutedCommand();
#endif

        #endregion

        #region 초기화
        public MainWindow()
        {
            //g.main_window = this;

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
                _graidFrame.Width = _gridCenter.ActualWidth - 20;
            if (_gridCenter.ActualHeight > (800))
                _graidFrame.Height = _gridCenter.ActualHeight - 20;
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
            P2DashBoard p2 = (P2DashBoard)_framePage2View.Content;
            g._P2DashBoard = p2;
            p2.PageLoad();
            p2.set_location_id(Etc.get_location_id_by_site_id(g.selected_site_id));
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
            Window win = new MainWindow2();
            win.Show();
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

            IPMFloorView window = new IPMFloorView(floor_id);
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();
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
            window.ShowDialog();
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

            var flyout = this.Flyouts.Items[0] as Flyout;
            if (flyout == null) return;
            flyout.IsOpen = !flyout.IsOpen;
        }

        // 트리에 따른 선택 
        private AssetTreeVM get_tree_vm()
        {
            AssetTreeVM vm = null;
            vm = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            return vm;
        }

    }
}
