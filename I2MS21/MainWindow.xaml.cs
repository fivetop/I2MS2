using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using I2MS2.Models;
using I2MS2.Pages;
using I2MS2.Animation;
using I2MS2.Windows;
using System.Net.Http;
using System.Net.Http.Headers;

using WebApiClient;
using WebApi.Models;

using System.Threading;
using I2MS2.Translation;
using System.Windows.Controls.Primitives;
using I2MS2.Library;
using I2MS2.UserControls;
using System.Globalization;
using Microsoft.AspNet.SignalR.Client;

namespace I2MS2
{

    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        enum PageInfo
        {
            NO_PAGE,
            P1_SELECT_CENTOR,
            P2_DASHBOARD,
            P3_FLOOR_VIEW,
            P4_ASSET_MANAGER,
            P5_LINE_MANAGER,
            P6_IPM
        };

        PageInfo current_page = PageInfo.NO_PAGE;
       LeftSideControl _ctlLeftSide;
        public RightSideControl _ctlRightSide;

        Boolean isReadyMainRun = false;

        #region RouteCommand 버튼 관련 정의

        public static RoutedCommand LineLinkListMenuCommand = new RoutedCommand();
        public static RoutedCommand AssetListMenuCommand = new RoutedCommand();
        public static RoutedCommand LocationListMenuCommand = new RoutedCommand();
        public static RoutedCommand CatalogListMenuCommand = new RoutedCommand();
        public static RoutedCommand EnvironmentListMenuCommand = new RoutedCommand();
        public static RoutedCommand ManufactureListMenuCommand = new RoutedCommand();
        public static RoutedCommand UserListMenuCommand = new RoutedCommand();
        public static RoutedCommand LinkDiagramChangeHistMenuCommand = new RoutedCommand();
        public static RoutedCommand TerminalViewMenuCommand = new RoutedCommand();
        public static RoutedCommand EventListMenuCommand = new RoutedCommand();
        public static RoutedCommand WorkOrderListMenuCommand = new RoutedCommand();

        public static RoutedCommand UserManagerMenuCommand = new RoutedCommand();
        public static RoutedCommand SiteUserManagerMenuCommand = new RoutedCommand();
        public static RoutedCommand DrawingsManagerMenuCommand = new RoutedCommand();
        public static RoutedCommand Drawings3DManagerMenuCommand = new RoutedCommand();
        public static RoutedCommand ManufactureManagerMenuCommand = new RoutedCommand();
        public static RoutedCommand CatalogManagerMenuCommand = new RoutedCommand();
        public static RoutedCommand CatalogGroupManagerMenuCommand = new RoutedCommand();
        public static RoutedCommand ExtendedPropertyManagerMenuCommand = new RoutedCommand();
        public static RoutedCommand ExtAssignManagerMenuCommand = new RoutedCommand();
        public static RoutedCommand NetworkSchedulerManagerMenuCommand = new RoutedCommand();
        public static RoutedCommand ExportManagerMenuCommand = new RoutedCommand();
        public static RoutedCommand ImportManagerMenuCommand = new RoutedCommand();
        public static RoutedCommand AlarmEventSetupMenuCommand = new RoutedCommand();
        public static RoutedCommand ICFwUpgradeMenuCommand = new RoutedCommand();
        public static RoutedCommand EtcViewOptionMenuCommand = new RoutedCommand();
        public static RoutedCommand PrintTemplateMenuCommand = new RoutedCommand();
        public static RoutedCommand EnvironmentSettingMenuCommand = new RoutedCommand();
        public static RoutedCommand LanguageSettingMenuCommand = new RoutedCommand();
        public static RoutedCommand ScanAll_ICMenuCommand = new RoutedCommand();
        public static RoutedCommand MailSMSMenuCommand = new RoutedCommand();

        public static RoutedCommand SelectCommand = new RoutedCommand();
        public static RoutedCommand AddFavoriteCommand = new RoutedCommand();
        public static RoutedCommand CopyCommand = new RoutedCommand();
        public static RoutedCommand CloneCommand = new RoutedCommand();
        public static RoutedCommand DeleteFavoriteCommand = new RoutedCommand();
        public static RoutedCommand DeleteCommand = new RoutedCommand();
        public static RoutedCommand AddBuildingCommand = new RoutedCommand();
        public static RoutedCommand EditBuildingCommand = new RoutedCommand();
        public static RoutedCommand AddFloorCommand = new RoutedCommand();
        public static RoutedCommand EditFloorCommand = new RoutedCommand();
        public static RoutedCommand AddRoomCommand = new RoutedCommand();
        public static RoutedCommand EditRoomCommand = new RoutedCommand();
        public static RoutedCommand AddRackCommand = new RoutedCommand();
        public static RoutedCommand EditRackCommand = new RoutedCommand();
        public static RoutedCommand ConfigRackMountCommand = new RoutedCommand();
        public static RoutedCommand AddAssetCommand = new RoutedCommand();
        public static RoutedCommand EditAssetCommand = new RoutedCommand();
        public static RoutedCommand AssetMoveTreeCommand = new RoutedCommand();
        public static RoutedCommand IntelligentMoveTreeCommand = new RoutedCommand();
        public static RoutedCommand FavoriteMoveTreeCommand = new RoutedCommand();
        public static RoutedCommand ScanICCommand = new RoutedCommand();
        public static RoutedCommand ConfigICCommand = new RoutedCommand();
        public static RoutedCommand ConfigSWCommand = new RoutedCommand();
        public static RoutedCommand RoomRackLayoutCommand = new RoutedCommand();
        public static RoutedCommand ViewRackCommand = new RoutedCommand();
        public static RoutedCommand ViewICCommand = new RoutedCommand();
        public static RoutedCommand ViewPPCommand = new RoutedCommand();
        public static RoutedCommand ViewSWCommand = new RoutedCommand();
        public static RoutedCommand ViewLinkDiagramCommand = new RoutedCommand();
        public static RoutedCommand ViewFloorIPMCommand = new RoutedCommand();
        public static RoutedCommand ViewIPMCommand = new RoutedCommand();

        public static RoutedCommand StatTerminalMenuCommand1 = new RoutedCommand();
        public static RoutedCommand StatTerminalMenuCommand2 = new RoutedCommand();
        public static RoutedCommand StatTerminalMenuCommand3 = new RoutedCommand();
        public static RoutedCommand StatEventMenuCommand1 = new RoutedCommand();
        public static RoutedCommand StatEventMenuCommand2 = new RoutedCommand();
        public static RoutedCommand StatEventMenuCommand3 = new RoutedCommand();

        #endregion

        #region 초기화
        public MainWindow()
        {
            g.main_window = this;
            InitializeComponent();

            //_effectConnect
            //_colorConnect g.signalr_connect

            _windowMainWindow.Height = g.screen_resolution.Height;
            _windowMainWindow.Width = g.screen_resolution.Width;

            CopyCommand.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Control));
            CloneCommand.InputGestures.Add(new KeyGesture(Key.V, ModifierKeys.Control));
            //DeleteCommand.InputGestures.Add(new KeyGesture(Key.Delete, ModifierKeys.None));

            //asset_tree 데이터로 treeview 만들기
            //initLocationTreeData(g.asset_tree_list);

            //DB 미연결시 시뮬레이션 코드
            //selfSimulator();
            ini_mailsms();
            // UI 초기화
            initUISet();

            var u = g.user_list.Find(p => p.user_id == g.login_user_id);
            string user_name = u != null ? u.user_name : "";
            _txtUserName.Text = user_name;
            connect_signalr();  // 비동기로 호출...
        }

        private void ini_mailsms()
        {
            g.mail_use = Reg.get_int("mail_use");
            if (g.mail_use == -1)
            {
                g.mail_use = 0;
                Reg.set_int("mail_use", g.mail_use);
                Reg.set_string("mail_server", g.mail_server);
                Reg.set_int("mail_port" , g.mail_port);
                Reg.set_string("mail_id", g.mail_id );
                Reg.set_string("mail_pw", g.mail_pw );

                Reg.set_int("sms_use", g.sms_use);
                Reg.set_string("sms_server", g.sms_server);
                Reg.set_string("sms_id", g.sms_id);
                Reg.set_string("sms_pw", g.sms_pw);
            }
            else
            {
                g.mail_server = Reg.get_string("mail_server");
                g.mail_port = Reg.get_int("mail_port");
                g.mail_id = Reg.get_string("mail_id");
                g.mail_pw = Reg.get_string("mail_pw");

                g.sms_use = Reg.get_int("sms_use");
                g.sms_server = Reg.get_string("sms_server");
                g.sms_id = Reg.get_string("sms_id");
                g.sms_pw = Reg.get_string("sms_pw");
            }
        }

        // 비동기 호출됨 
        private async Task<bool> connect_signalr()
        {
            // SignalR 허브로 연결
            return await g.signalr.connect(g.signalr_uri_string);
        }



        //UI 화면을 초기화 한다 각각의 패이지인 유저컨트롤등의 scale을 보이지 않게 0으로 설정 한다
        private void initUISet()
        {
            //사이드 바를 눈에 보이지 않게 설정
            ScaleTransform scale_tran = new ScaleTransform(0, 1);
            _gridLeftSideMenuIn.RenderTransform = scale_tran;
           
            _gridRightSide.RenderTransform = scale_tran;
            _framePage2View.RenderTransform = scale_tran;
            _framePage3View.RenderTransform = scale_tran;
            _framePage4View.RenderTransform = scale_tran;
            _framePage5View.RenderTransform = scale_tran;
            //_framePage6View.RenderTransform = scale_tran;

            //TopMenu 모양을 결정하기 위하여 바인딩한다
            _btnTopMenu_SelectCenter.DataContext = new TopMenuButtonData() { name = "center", img = "/I2MS2;component/Icons/TopMenuIcon1_1.png", pressed_img = "/I2MS2;component/Icons/TopMenuIcon1_2.png" };
            _btnTopMenu_DashBoard.DataContext = new TopMenuButtonData() { name = "dashboard", img = "/I2MS2;component/Icons/TopMenuIcon2_1.png", pressed_img = "/I2MS2;component/Icons/TopMenuIcon2_2.png" };
            _btnTopMenu_FloorView.DataContext = new TopMenuButtonData() { name = "floorview", img = "/I2MS2;component/Icons/TopMenuIcon3_1.png", pressed_img = "/I2MS2;component/Icons/TopMenuIcon3_2.png" };
            _btnTopMenu_AssetManage.DataContext = new TopMenuButtonData() { name = "assetmgr", img = "/I2MS2;component/Icons/TopMenuIcon4_1.png", pressed_img = "/I2MS2;component/Icons/TopMenuIcon4_2.png" };
            _btnTopMenu_LineManage.DataContext = new TopMenuButtonData() { name = "linemgr", img = "/I2MS2;component/Icons/TopMenuIcon5_1.png", pressed_img = "/I2MS2;component/Icons/TopMenuIcon5_2.png" };
            //_btnTopMenu_IPM.DataContext = new TopMenuButtonData() { name = "ipm", img = "/I2MS2;component/Icons/TopMenuIcon6_1.png", pressed_img = "/I2MS2;component/Icons/TopMenuIcon6_2.png" };


            //TopMenu Button disable
            _btnTopMenu_SelectCenter.IsEnabled = false;
            _btnTopMenu_DashBoard.IsEnabled = false;
            _btnTopMenu_FloorView.IsEnabled = false;
            _btnTopMenu_AssetManage.IsEnabled = false;
            _btnTopMenu_LineManage.IsEnabled = false;
            //_btnTopMenu_IPM.IsEnabled = false;

            //최초 실행시 SelectCenterButton 이 체크된 상태에서 시작
            _btnTopMenu_SelectCenter.IsChecked = true;

            _ctlLeftSide = new LeftSideControl();
            _ctlLeftSide.Width = 320; //left side 지정
            _ctlLeftSide.putAndFullEvent += new LeftSideControl.PutAndPullEventHandler(putAndPullLeftSideControl);
            _gridLeftSideMenuIn.Children.Add(_ctlLeftSide);
            g.left_tree_handler._ctlLeftSide = _ctlLeftSide;

            _ctlRightSide = new RightSideControl();
            _ctlRightSide.putAndFullEvent += new RightSideControl.PutAndPullEventHandler(putAndPullLeftSideControl);
            _gridRightSide.Children.Add(_ctlRightSide);

            //left, right side Event disable
            _canvasLeftSideMenuShowButton.IsEnabled = false;
            _canvasRightSideMenuShowButton.IsEnabled = false;
            //_canvasLeftSideMenuShowButton.IsEnabled = true;
            //_canvasRightSideMenuShowButton.IsEnabled = true;

            //Property binding to right side  
            _ctlRightSide.DataContext = g.prop_data;

            //current page set
            current_page = PageInfo.P1_SELECT_CENTOR;

        }


        #endregion

        #region 출력 메뉴 관리 Command
        // 선번장 목록 출력 
        private void _cmdLineLinkListMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
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
            e.CanExecute = UserRight.is_ok(eUserRight.eWorkOrderList);
        }

        private void _cmdWorkOrderListMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        // 네트워크 검색 뷰
        private void _cmdTerminalViewMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = UserRight.is_ok(eUserRight.eTerminalView);
        }

        private void _cmdTerminalViewMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TerminalView window = new TerminalView();
            window.Owner = this;
            window.ShowDialog();
        }

        // 이벤트 목록 출력 
        private void _cmdEventListMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
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
            e.CanExecute = UserRight.is_ok(eUserRight.eLinkDiagramChangeHistList);
        }

        private void _cmdLinkDiagramChangeHistMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        // 터미날 통계
        private void _cmdStatTerminalMenu1_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
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
            e.CanExecute = UserRight.is_ok(eUserRight.eStatEvent3);
        }

        private void _cmdStatEventMenu3_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            StatEnvironmentHour window = new StatEnvironmentHour();
            window.Owner = this;
            window.ShowDialog();
        }

        #endregion

        #region 설정 메뉴 관리 Command
        // 사용자 관리
        private void _cmdUserManagerMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = UserRight.is_ok(eUserRight.eUserManager);
        }

        private void _cmdUserManagerMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            UserManager window = new UserManager();
            window.Owner = this;
            window.ShowDialog();
        }

        // 사이트 사용자 관리
        private void _cmdSiteUserManagerMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = UserRight.is_ok(eUserRight.eSiteUserManager);
        }

        private void _cmdSiteUserManagerMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            UserSiteManager window = new UserSiteManager();
            window.Owner = this;
            window.ShowDialog();
        }

        // 이미지 등록 관리 
        private void _cmdDrawingsManagerMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = UserRight.is_ok(eUserRight.eDrawingsManager);
        }

        private void _cmdDrawingsManagerMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            BitmapManager window = new BitmapManager();
            window.Owner = this;
            window.ShowDialog();
        }

        // 3D 도면 등록 관리
        private void _cmdDrawings3DManagerMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = UserRight.is_ok(eUserRight.eDrawings3DManager);
        }

        private void _cmdDrawings3DManagerMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DrawingEditer window = new DrawingEditer();
            window.Owner = this;
            window.ShowDialog();
        }

        // 제조사 및 연락처 등록 관리
        private void _cmdManufactureManagerMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = UserRight.is_ok(eUserRight.eManufactureManager);
        }

        private void _cmdManufactureManagerMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ManufactureManager window = new ManufactureManager();
            window.Owner = this;
            window.ShowDialog();
        }

        // 카탈로그 등록 관리 
        private void _cmdCatalogManagerMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = UserRight.is_ok(eUserRight.eCatalogManager);
        }

        private void _cmdCatalogManagerMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CatalogManager window = new CatalogManager();
            window.Owner = this;
            window.ShowDialog();
        }

        // 카탈로그 그룹 관리 
        private void _cmdCatalogGroupManagerMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = UserRight.is_ok(eUserRight.eCatalogGroupManager);
        }

        private void _cmdCatalogGroupManagerMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CatalogGroupManager window = new CatalogGroupManager();
            window.Owner = this;
            window.ShowDialog();
        }

        // 확장 속성 등록 관리
        private void _cmdExtendedPropertyManagerMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = UserRight.is_ok(eUserRight.eExtendedPropertyManager);
        }

        private void _cmdExtendedPropertyManagerMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ExtendedPropertyManager window = new ExtendedPropertyManager();
            window.Owner = this;
            window.ShowDialog();
        }

        // 확장 속성 할당 관리
        private void _cmdExtAssignManagerMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = UserRight.is_ok(eUserRight.eExtAssignManager);
        }

        private void _cmdExtAssignManagerMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ExtAssignManager window = new ExtAssignManager();
            window.Owner = this;
            window.ShowDialog();
        }

        // 네트워크 스케쥴러 관리                     
        private void _cmdNetworkSchedulerManagerMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = UserRight.is_ok(eUserRight.eNetworkSchedulerManager);
        }

        private void _cmdNetworkSchedulerManagerMenu_Executed(object sender, RoutedEventArgs e)
        {
            NetworkSchedulerManager window = new NetworkSchedulerManager();
            window.Owner = this;
            window.ShowDialog();
        }

        // 데이터 저장(Export to Excel)
        private void _cmdExportManagerMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = UserRight.is_ok(eUserRight.eExportManager);
        }

        private void _cmdExportManagerMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ExportMaster em = new ExportMaster();
            em.export_2_excel();
        }

        // 데이터 불러오기(Import from Excel)
        private void _cmdImportManagerMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = UserRight.is_ok(eUserRight.eImportManger);
        }

        private async void _cmdImportManagerMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ExportMaster em = new ExportMaster();
            await em.import_from_excel();
        }

        // 알람 / 이벤트 설정
        private void _cmdAlarmEventSetupMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = UserRight.is_ok(eUserRight.eAlarmEventSetup);
        }

        private void _cmdAlarmEventSetupMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AlarmEventSetupManager window = new AlarmEventSetupManager();
            window.Owner = this;
            window.ShowDialog();
        }

        // 컨트롤러 F/W 업그레이드
        private void _cmdICFwUpgradeMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = UserRight.is_ok(eUserRight.eICFwUpgrade);
        }

        private void _cmdICFwUpgradeMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ICFwUpgradeManager window = new ICFwUpgradeManager();
            window.Owner = this;
            window.ShowDialog();
        }

        // 기타 표시 옵션 설정
        private void _cmdEtcViewOptionMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = UserRight.is_ok(eUserRight.eEtcViewOption);
        }

        private void _cmdEtcViewOptionMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EtcViewOption window = new EtcViewOption();
            window.Owner = this;
            window.ShowDialog();
        }

        // 출력 템플릿 관리
        private void _cmdPrintTemplateMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = UserRight.is_ok(eUserRight.ePrintTemplate);
        }

        private void _cmdPrintTemplateMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PrintTemplateList window = new PrintTemplateList();
            window.Owner = this;
            window.ShowDialog();
        }

        // 모니터링 정보 설정 
        private void _cmdEnvironmentSettingMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = UserRight.is_ok(eUserRight.eEnvironmentSetting);
        }

        private void _cmdEnvironmentSettingMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EnviromentManager window = new EnviromentManager();
            window.Owner = this;
            window.ShowDialog();
        }

        // 언어 설정 --> 실시간 변경은 사용치 않음 -> romee 2015.07.08
        private void _cmdLanguageSettingMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = UserRight.is_ok(eUserRight.eLanguageSetting);
        }

        private void _cmdLanguageSettingMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
//            languageWindow window = new languageWindow();
//            window.Owner = this;
//            window.ShowDialog();
        }

        // 전체 컨트롤러 스캔 
        private void _cmdScanAll_ICMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = UserRight.is_ok(eUserRight.eScanAll_IC);
        }
        private void _cmdScanAll_ICMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            bool b = MessageBox.Show(g.tr_get("M_Scan_IC_And_Save"), g.tr_get("C_Scan_Controller"),
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (!b)
                return;

            var list = g.ic_connect_status_list;

            foreach (var node in list)
            {
                int ic_asset_id = node.ic_asset_id;
                bool b2 = scan_ic_ex(ic_asset_id);
                if (!b2)
                    break;
            }
        }

        // 이메일 , SMS 
        private void _cmdMailSMSMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = UserRight.is_ok(eUserRight.eMailSMS);
        }
        private void _cmdMailSMSMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SendManager window = new SendManager();
            window.Owner = this;
            window.ShowDialog();
        }

        #endregion
            
        #region 좌측 트리 메뉴 관리 Command

        // 선택 
        private void _cmdSelect_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = get_tree_vm();
            if (ast_vm == null)
                e.CanExecute = false;
            else
                e.CanExecute = true;
            //int asset_id = ast_vm.asset_id ?? 0;
            //e.CanExecute = asset_id != 0;
        }

        private void _cmdSelect_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = get_tree_vm();
            if (ast_vm == null)
                return;

            AssetTreeVM ast_vm_md = _ctlLeftSide.getAssetTreeVMinMD(ast_vm);
#if false
            //포트 알람 테스트용 코드
            if (ast_vm.type == AssetTreeType.Port)
            {
                if (ast_vm_md.on_alarm)
                    _ctlLeftSide.updatePortAlarmAllTree(ast_vm.asset_id ?? 0, ast_vm.port_no, "-");
                else
                    _ctlLeftSide.updatePortAlarmAllTree(ast_vm.asset_id ?? 0, ast_vm.port_no, "P");
            }
            else
#endif
            // P4 를 변경하기 위함 자산뷰
            TreeViewSelectItem(ast_vm_md);

            // 층별뷰를 변경하기 위함
            if (ast_vm.type != AssetTreeType.Site)
            {
                int bd_id = 0;

                //현재 선택된 빌딩과 선택된 asset이 속한 빌딩이 다르면
                location l = g.location_list.Find(at => at.location_id == ast_vm.location_id);
                if (l == null) return;
                if (l.building_id == null) return;

                bd_id = l.building_id ?? 0;
                if (g.selected_building_id != bd_id)
                {
                    //빌딩을 변경 한다
                    P3FloorView p3 = (P3FloorView)_framePage3View.Content; // 층별뷰 업데이트 처리 
                    p3.setBuilding(bd_id);
                    g.selected_building_id = bd_id;
                }
            }

            ////테스트용 코드 
            //if (ast_vm.type == AssetTreeType.i_Controller)
            //{
            //    _ctlLeftSide.updateIcAlarmStatus(ast_vm.asset_id ?? 0, test_bool);
            //    test_bool = !test_bool;
            //}
        }
        bool test_bool = false;
        // 즐겨 찾기 추가 
        private void _cmdAddFavorite_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = get_tree_vm();
            if (ast_vm == null)
            {
                e.CanExecute = false;
                return;
            }
            int asset_id = ast_vm.asset_id ?? 0;
            if (asset_id == 0)
            {
                e.CanExecute = false;
                return;
            }
            if ((ast_vm.type == AssetTreeType.PC) || (ast_vm.type == AssetTreeType.Port) || (ast_vm.type == AssetTreeType.SwitchCard))
            {
                e.CanExecute = false;
                return;
            }

            var find = g.favorite_tree_list.Find(p => p.asset_id == asset_id);
            if (find != null)
            {
                e.CanExecute = false;
                return;
            }
            e.CanExecute = true;
        }

        private async void _cmdAddFavorite_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = get_tree_vm();
            if (ast_vm == null) return;
            int asset_id = ast_vm.asset_id ?? 0;
            if (asset_id == 0) return;
            int location_id = ast_vm.location_id;
            var find = g.favorite_tree_list.Find(p => p.asset_id == asset_id);
            if (find != null) return;

            bool r1 = await g.left_tree_handler.addFavorite(location_id, asset_id);
            if (!r1)
                MessageBox.Show(g.tr_get("C_Error_Server"));
        }

        private bool _copy = false;
        private int _copy_asset_id = 0;
        private int _copy_location_id = 0;
        // 복사 
        private void _cmdCopy_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eCopy))
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

            if ((ast_vm.type == AssetTreeType.Port) || (ast_vm.type == AssetTreeType.Site) || (ast_vm.type == AssetTreeType.Building) || (ast_vm.type == AssetTreeType.Unknown) || (ast_vm.type == AssetTreeType.PC))
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = true;
        }

        private void _cmdCopy_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (ast_vm == null)
            {
                return;
            }

            asset_tree ast = g.asset_tree_list.Find(at => at.asset_tree_id == ast_vm.asset_tree_id);
            _copy_asset_id = ast.asset_id ?? 0;
            _copy_location_id = ast.location_id;

            _copy = true;
        }

        // 붙여넣기 
        private void _cmdClone_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eClone))
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
            if (!_copy)
            {
                e.CanExecute = false;
                return;
            }

            // Jake, 포트나 PC는 복사가 안되게 추후에 막아야 함. --현재 PC 설정이...이상이 있음.
            if ((ast_vm.type == AssetTreeType.Port) || (ast_vm.type == AssetTreeType.Site) || (ast_vm.type == AssetTreeType.Unknown) || (ast_vm.type == AssetTreeType.PC))
            {
                e.CanExecute = false;
                return;
            }

            int dest_location_id = 0;
            asset_tree ast = g.asset_tree_list.Find(at => at.asset_tree_id == ast_vm.asset_tree_id);
            dest_location_id = ast.location_id;

            // 1) location 항목

            if (_copy_asset_id == 0)
            {
                // Copy Paste를 같은 레벨에서 한경우 dest를 엄마위치를 읽어온다.
                if (Etc.is_same_location_level(dest_location_id, _copy_location_id))
                    dest_location_id = Etc.get_prev_location_id(dest_location_id);
                // 복제할 항목은 floor부터 rack까지만 가능....
                if (!Etc.is_floor_or_room_or_rack(_copy_location_id))
                {
                    e.CanExecute = false;
                    return;
                }
                e.CanExecute = Etc.is_copy_location(dest_location_id, _copy_location_id);
                return;
            }

            // 2) 자산 항목

            if (dest_location_id == 0)
            {
                e.CanExecute = false;
                return;
            }

            int src_catalog_id = Etc.get_catalog_id_by_asset_id(_copy_asset_id);
            // 랙에 추가할 수 없는 장치들은 랙에 복제가 안되도록 해야 함...
            if (CatalogType.is_sw_card(src_catalog_id))
            {
                // 카드형스위치자산은 슬롯형스위치에서만 복제가 가능하게 해야함.
                int dest_asset_id = ast.asset_id ?? 0;
                int dest_catalog_id = Etc.get_catalog_id_by_asset_id(_copy_asset_id);
                e.CanExecute = CatalogType.is_sw_slot(dest_catalog_id) || CatalogType.is_sw_card(dest_catalog_id);
                return;
            }
            else
            {
                if (!CatalogType.is_rack_mountable(src_catalog_id) && Etc.is_rack(dest_location_id) )
                {
                    e.CanExecute = false;
                    return;
                }
            }

            // 자산을 복제 시.... room 과 rack만 가능...   
            e.CanExecute = Etc.is_room_or_rack(dest_location_id);
            return;
        }

        private void _cmdClone_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            int dest_location_id = 0;
            int dest_asset_id = 0;
            int dest_sw_slot_asset_id = 0; 
            AssetTreeVM ast_vm = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (ast_vm != null)
            {
                asset_tree ast = g.asset_tree_list.Find(at => at.asset_tree_id == ast_vm.asset_tree_id);
                dest_location_id = ast.location_id;
                dest_asset_id = ast.asset_id ?? 0;
            }

            if (_copy_asset_id == 0)
            {
                // Copy Paste를 같은 레벨에서 한경우 dest를 엄마위치를 읽어온다.
                if (Etc.is_same_location_level(dest_location_id, _copy_location_id))
                    dest_location_id = Etc.get_prev_location_id(dest_location_id);
                // 복제할 위치가 floor부터 rack까지만 가능....
                if (!Etc.is_floor_or_room_or_rack(_copy_location_id))
                    return;
                if (!Etc.is_copy_location(dest_location_id, _copy_location_id))
                    return;
            }
            else
            {
                // 자산을 복제 시.... room 과 rack만 가능...
                if (!Etc.is_room_or_rack(dest_location_id))
                    return;
                int src_catalog_id = Etc.get_catalog_id_by_asset_id(_copy_asset_id);
                // 랙에 추가할 수 없는 장치들은 랙에 복제가 안되도록 해야 함...
                if (CatalogType.is_sw_card(src_catalog_id))
                {
                    // 카드형스위치자산은 슬롯형스위치에서만 복제가 가능하게 해야함.
                    int dest_catalog_id = Etc.get_catalog_id_by_asset_id(dest_asset_id);
                    if (CatalogType.is_sw_card(dest_catalog_id))
                    {
                        dest_sw_slot_asset_id = Etc.get_sw_slot_asset_id(dest_asset_id);
                    }
                    else if(CatalogType.is_sw_slot(dest_catalog_id))
                        dest_sw_slot_asset_id = dest_asset_id;
                    else
                        return;
                }
            }
            if (dest_location_id == 0)
                return;

            CloneManager window = new CloneManager(_copy_asset_id, dest_sw_slot_asset_id, _copy_location_id, dest_location_id);
            window.Owner = this;
            bool b = window.ShowDialog() ?? false;
        }

        // 즐겨찾기 지우기 
        private void _cmdDeleteFavorite_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eDelete))
            {
                e.CanExecute = false;
                return;
            }

            AssetTreeVM ast_vm = (AssetTreeVM)_ctlLeftSide._tvFavoriteTree.SelectedItem;
            if (ast_vm == null)
            {
                e.CanExecute = false;
                return;
            }
            int asset_id = ast_vm.asset_id ?? 0;
            favorite_tree ft = g.favorite_tree_list.Find(p => p.asset_id == asset_id);

            e.CanExecute = ft != null;
        }

        private async void _cmdDeleteFavorite_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = (AssetTreeVM)_ctlLeftSide._tvFavoriteTree.SelectedItem;
            if (ast_vm == null)
            {
                return;
            }
            int asset_id = ast_vm.asset_id ?? 0;
            favorite_tree ft = g.favorite_tree_list.Find(p => p.asset_id == asset_id);
            if (ft == null)
                return;

            await g.left_tree_handler.deleteAssetFromFavoriteTree(asset_id, ft.favorite_tree_id);
        }
        // 트리에 따른 선택 
        private AssetTreeVM get_tree_vm()
        {
            AssetTreeVM vm = null;
            int idx = _ctlLeftSide._tc.SelectedIndex;
            switch (idx)
            {
                case 0: // 자산
                    vm = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
                    break;
                case 1: // 지능형
                    vm = (AssetTreeVM)_ctlLeftSide._tvIntelligentTree.SelectedItem;
                    break;
                case 2: // 즐겨찾기
                    vm = (AssetTreeVM)_ctlLeftSide._tvFavoriteTree.SelectedItem;
                    break;
                default:
                    return null;
            }
            return vm;
        }



        // 자산트리, 지능형트리, 즐겨찾기트리에서 사용
        // 지우기 
        private void _cmdDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = get_tree_vm();
            if (ast_vm == null)
            {
                e.CanExecute = false;
                return;
            }
            int asset_id = ast_vm.asset_id ?? 0;
            if (asset_id == 0)
            {
                if (ast_vm.is_expander_visible == Visibility.Visible)
                {
                    e.CanExecute = false;
                    return;
                }
            }
            e.CanExecute = true;
        }

        private async void _cmdDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = get_tree_vm();
            if (ast_vm == null)
                return;
            int asset_id = ast_vm.asset_id ?? 0;
            int location_id = ast_vm.location_id;

            if (MessageBox.Show(g.tr_get("C_Delete_Item"), g.tr_get("C_Confirm"), MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            // 배선관리에 오픈되어 있으면 삭제가 안되게 함.
            if (Etc.is_open_link_diagram(asset_id))
            {
                MessageBox.Show(g.tr_get("C_Error_Asset_2"));
                return;                       
            }

            // 네트워크 스케줄러에 등록되어 있는 것은 삭제가 안되게 함.
            if (Etc.is_use_sw_card_in_network_scheduler(asset_id))
            {
                MessageBox.Show(g.tr_get("C_Error_Asset_4"));
                return;                       
            }

            int catalog_id = Etc.get_catalog_id_by_asset_id(asset_id);
            if (CatalogType.is_pc(catalog_id))
            {
                int outlet_asset_id = ast_vm._parant_ast_vm.asset_id ?? 0;
                int outlet_port_no = ast_vm._parant_ast_vm.port_no;
                // 단말 삭제루틴 호출
                await g.left_tree_handler.deleteTerminal(outlet_asset_id, outlet_port_no, asset_id);
                // 다른클라이언트에게 브로드캐스팅
                g.signalr.send_asset_to_signalr(asset_id, eAction.eRemove);
            }
            else if (asset_id > 0)
            {
                // 링크가 연결되어 있는 경우 삭제 불가
                if (Etc.is_use_link_diagram(asset_id))
                {
                    MessageBox.Show(g.tr_get("C_Error_Asset_1"));
                    return;                       
                }
                // 자산을 삭제...
                await g.left_tree_handler.deleteAsset(asset_id);
                // 다른클라이언트에게 브로드캐스팅
                g.signalr.send_asset_to_signalr(asset_id, eAction.eRemove);
            }
            else
            {
                // 장소를 삭제...
                await g.left_tree_handler.deleteLocation(location_id);
                // 다른클라이언트에게 브로드캐스팅
                g.signalr.send_location_to_signalr(location_id, eAction.eRemove);
            }

            // 삭제 후 바로위의 항목으로 이동하고 싶음....
        }

        // 빌딩 추가 
        private void _cmdAddBuilding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eAddBuilding))
            {
                e.CanExecute = false;
                return;
            }

            AssetTreeVM at = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (at == null)
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = at.disp_level == 3;
        }

        private void _cmdAddBuilding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (ast_vm == null)
                return;
            asset_tree ast = g.asset_tree_list.Find(at => at.asset_tree_id == ast_vm.asset_tree_id);
            int location_id = ast.location_id;
            var l = g.location_list.Find(p => p.location_id == location_id);
            if (l == null)
                return;

            int site_id = l.site_id ?? 0;

            BuildingManager window = new BuildingManager(site_id, 0);
            window.Owner = this;
            window.ShowDialog();
        }

        // 빌딩 수정 
        private void _cmdEditBuilding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eEditBuilding))
            {
                e.CanExecute = false;
                return;
            }

            AssetTreeVM at = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (at == null)
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = at.disp_level == 4;
        }

        private void _cmdEditBuilding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (ast_vm == null)
                return;
            asset_tree ast = g.asset_tree_list.Find(at => at.asset_tree_id == ast_vm.asset_tree_id);


            int location_id = ast.location_id;
            var l = g.location_list.Find(p => p.location_id == location_id);
            if (l == null)
                return;

            int site_id = l.site_id ?? 0;
            int building_id = l.building_id ?? 0;

            BuildingManager window = new BuildingManager(site_id, building_id);
            window.Owner = this;
            window.ShowDialog();
        }

        // 층 추가 
        private void _cmdAddFloor_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eAddFloor))
            {
                e.CanExecute = false;
                return;
            }

            AssetTreeVM at = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (at == null)
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = at.disp_level == 4;
        }

        private void _cmdAddFloor_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (ast_vm == null)
                return;
            asset_tree ast = g.asset_tree_list.Find(at => at.asset_tree_id == ast_vm.asset_tree_id);
            int location_id = ast.location_id;
            var l = g.location_list.Find(p => p.location_id == location_id);
            if (l == null)
                return;

            int building_id = l.building_id ?? 0;

            FloorManager window = new FloorManager(building_id, 0);
            window.Owner = this;
            window.ShowDialog();
        }

        // 층 수정 
        private void _cmdEditFloor_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eEditFloor))
            {
                e.CanExecute = false;
                return;
            }

            AssetTreeVM at = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (at == null)
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = at.disp_level == 5;
        }

        private void _cmdEditFloor_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (ast_vm == null)
                return;
            asset_tree ast = g.asset_tree_list.Find(at => at.asset_tree_id == ast_vm.asset_tree_id);
            int location_id = ast.location_id;
            var l = g.location_list.Find(p => p.location_id == location_id);
            if (l == null)
                return;

            int building_id = l.building_id ?? 0;
            int floor_id = l.floor_id ?? 0;

            FloorManager window = new FloorManager(building_id, floor_id);
            window.Owner = this;
            window.ShowDialog();
        }

        // 룸 추가 
        private void _cmdAddRoom_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eAddRoom))
            {
                e.CanExecute = false;
                return;
            }

            AssetTreeVM at = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (at == null)
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = at.disp_level == 5;
        }

        private void _cmdAddRoom_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (ast_vm == null)
                return;
            asset_tree ast = g.asset_tree_list.Find(at => at.asset_tree_id == ast_vm.asset_tree_id);
            int location_id = ast.location_id;
            var l = g.location_list.Find(p => p.location_id == location_id);
            if (l == null)
                return;

            int floor_id = l.floor_id ?? 0;

            RoomManager window = new RoomManager(floor_id, 0);
            window.Owner = this;
            window.ShowDialog();
        }
        // 룸 수정 
        private void _cmdEditRoom_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eEditRoom))
            {
                e.CanExecute = false;
                return;
            }

            AssetTreeVM at = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (at == null)
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = at.disp_level == 6;
        }

        private void _cmdEditRoom_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (ast_vm == null)
                return;
            asset_tree ast = g.asset_tree_list.Find(at => at.asset_tree_id == ast_vm.asset_tree_id);
            int location_id = ast.location_id;
            var l = g.location_list.Find(p => p.location_id == location_id);
            if (l == null)
                return;

            int floor_id = l.floor_id ?? 0;
            int room_id = l.room_id ?? 0;

            RoomManager window = new RoomManager(floor_id, room_id);
            window.Owner = this;
            window.ShowDialog();
        }

        // 랙추가 
        private void _cmdAddRack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eAddRack))
            {
                e.CanExecute = false;
                return;
            }

            AssetTreeVM at = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (at == null)
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = at.disp_level == 6;
        }

        private void _cmdAddRack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (ast_vm == null)
                return;
            asset_tree ast = g.asset_tree_list.Find(at => at.asset_tree_id == ast_vm.asset_tree_id);
            int location_id = ast.location_id;
            var l = g.location_list.Find(p => p.location_id == location_id);
            if (l == null)
                return;

            int room_id = l.room_id ?? 0;

            RackManager window = new RackManager(room_id, 0);
            window.Owner = this;
            window.ShowDialog();
        }

        // 랙 수정 
        private void _cmdEditRack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eEditRack))
            {
                e.CanExecute = false;
                return;
            }

            AssetTreeVM at = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (at == null)
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = at.disp_level == 7;
        }

        private void _cmdEditRack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (ast_vm == null)
                return;
            asset_tree ast = g.asset_tree_list.Find(at => at.asset_tree_id == ast_vm.asset_tree_id);
            int location_id = ast.location_id;
            var l = g.location_list.Find(p => p.location_id == location_id);
            if (l == null)
                return;

            int room_id = l.room_id ?? 0;
            int rack_id = l.rack_id ?? 0;

            RackManager window = new RackManager(room_id, rack_id);
            window.Owner = this;
            window.ShowDialog();
        }

        // 랙 마운트 설정 
        private void _cmdConfigRackMount_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eConfigRackMount))
            {
                e.CanExecute = false;
                return;
            }

            AssetTreeVM at = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (at == null)
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = at.type == AssetTreeType.Rack;
        }

        private void _cmdConfigRackMount_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (ast_vm == null)
                return;
            asset_tree ast = g.asset_tree_list.Find(at => at.asset_tree_id == ast_vm.asset_tree_id);
            int location_id = ast.location_id;
            var l = g.location_list.Find(p => p.location_id == location_id);
            if (l == null)
                return;

            int rack_id = l.rack_id ?? 0;

            RackMountManager window = new RackMountManager(location_id, rack_id);
            window.Owner = this;
            bool b = window.ShowDialog() ?? false;
            if (b == true)
            {
                // 여기에서 트리를 갱신
                _ctlLeftSide.updateAssetTreeItem(ast_vm);
            }
        }

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
        
        // 자산 이동  <- 내일...
        private void _cmdAssetMoveTree_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private bool _asset_move_tree_flag = false;
        private void _cmdAssetMoveTree_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_asset_move_tree_flag)
            {
                _asset_move_tree_flag = true;
                _ctlLeftSide._tvAssetTree.AllowDrop = true;
            }
            else
            {
                _asset_move_tree_flag = false;
                _ctlLeftSide._tvAssetTree.AllowDrop = false;
            }
        }

        // 자산 이동 
        private void _cmdIntelligentMoveTree_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private bool _intelligent_move_tree_flag = false;
        private void _cmdIntelligentMoveTree_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_intelligent_move_tree_flag)
            {
                _intelligent_move_tree_flag = true;
                _ctlLeftSide._tvIntelligentTree.AllowDrop = true;
            }
            else
            {
                _intelligent_move_tree_flag = false;
                _ctlLeftSide._tvIntelligentTree.AllowDrop = false;
            }
        }

        // 자산 이동 
        private void _cmdFavoriteMoveTree_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private bool _favorite_move_tree_flag = false;
        private void _cmdFavoriteMoveTree_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_favorite_move_tree_flag)
            {
                _favorite_move_tree_flag = true;
                _ctlLeftSide._tvFavoriteTree.AllowDrop = true;
            }
            else
            {
                _favorite_move_tree_flag = false;
                _ctlLeftSide._tvFavoriteTree.AllowDrop = false;
            }
        }

        // 콘트롤러 스캔 
        private void _cmdScanIC_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eScanIC))
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
            if (asset_id == 0)
            {
                e.CanExecute = false;
                return;
            }
            int catalog_id = Etc.get_catalog_id_by_asset_id(asset_id);
            e.CanExecute = CatalogType.is_ic(catalog_id) || CatalogType.is_ipp(catalog_id); ;
        }

        private void _cmdScanIC_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = get_tree_vm();
            if (ast_vm == null)
                return;
            int asset_id = ast_vm.asset_id ?? 0;
            int catalog_id = Etc.get_catalog_id_by_asset_id(asset_id);
            if (CatalogType.is_ipp(catalog_id))
            {
                var iic = g.ic_ipp_config_list.Find(p => p.ipp_asset_id == asset_id);
                if (iic == null)
                    return;
                asset_id = iic.ic_asset_id;
            }
            scan_ic(asset_id);
        }
        // 컨트롤러 스캔 처리 
        public void scan_ic(int ic_asset_id)
        {
            bool b = MessageBox.Show(g.tr_get("M_Scan_IC_And_Save"), g.tr_get("C_Scan_Controller"),
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (!b)
                return;
            scan_ic_ex(ic_asset_id);
        }

        public bool scan_ic_ex(int ic_asset_id)
        {
            var a = g.asset_list.Find(p => p.asset_id == ic_asset_id);
            var au = g.asset_aux_list.Find(p => p.asset_id == ic_asset_id);
            if ((a == null) || (au == null))
                return false;
            int sys_id = au.ic_con_id ?? 0;
            string ip_addr = a.ipv4;
            string snmp_community = au.snmp_get_community;

            request rq = new request()
            {
                sys_id = sys_id,
                snmp_community = snmp_community,
                ip_addr = ip_addr,
                type = eRequestType.eScanIC
            };
            var r = g.webapi.post("request", rq, typeof(request));
            if (r == null)
            {
                MessageBox.Show(g.tr_get("C_Error_Scan_Failed"));
                return false;
            }
            return true;
        }

        // 컨트롤러 설정 
        private void _cmdConfigIC_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eConfigIC))
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
            if (asset_id == 0)
            {
                e.CanExecute = false;
                return;
            }
            int catalog_id = Etc.get_catalog_id_by_asset_id(asset_id);
            e.CanExecute = CatalogType.is_ic(catalog_id);
        }

        private void _cmdConfigIC_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = get_tree_vm();
            if (ast_vm == null)
                return;
            int asset_id = ast_vm.asset_id ?? 0;
            int location_id = ast_vm.location_id;

            ICMountManager window = new ICMountManager(location_id, asset_id);
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();
        }
        // 스위치 설정 
        private void _cmdConfigSW_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eConfigSW))
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
            if (asset_id == 0)
            {
                e.CanExecute = false;
                return;
            }
            int catalog_id = Etc.get_catalog_id_by_asset_id(asset_id);
            if (CatalogType.is_sw(catalog_id))
            {
                var c = g.catalog_list.Find(p => p.catalog_id == catalog_id);
                if (c != null)
                    if (c.sw_figure_type == "S")
                    {
                        e.CanExecute = true;
                        return;
                    }
            }
            e.CanExecute = false;
            return;
        }

        private void _cmdConfigSW_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = get_tree_vm();
            if (ast_vm == null)
                return;
            int asset_id = ast_vm.asset_id ?? 0;
            int location_id = ast_vm.location_id;

            SWMountManager window = new SWMountManager(location_id, asset_id);
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();
        }

        // 랙보기 
        private void _cmdViewRack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eViewRack))
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
        }

        private void _cmdViewRack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (ast_vm == null)
                return;
            asset_tree ast = g.asset_tree_list.Find(at => at.asset_tree_id == ast_vm.asset_tree_id);
            int location_id = ast.location_id;

            RackView window = new RackView(location_id);
            window.Owner = this;
            try
            {
            window.ShowDialog();
        }
            catch(Exception ex) 
            {}
        }
        // 컨트롤러 보기 
        private void _cmdViewIC_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eViewIC))
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
            if (asset_id == 0)
            {
                e.CanExecute = false;
                return;
            }
            int catalog_id = Etc.get_catalog_id_by_asset_id(asset_id);
            e.CanExecute = CatalogType.is_ic(catalog_id);
        }
        
        private void _cmdViewIC_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = get_tree_vm();
            if (ast_vm == null)
                return;
            int asset_id = ast_vm.asset_id ?? 0;
            int location_id = ast_vm.location_id;

            ICView window = new ICView(asset_id);
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();
        }
        // 패치패널 보기 
        private void _cmdViewPP_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eViewPP))
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
            if (asset_id == 0)
            {
                e.CanExecute = false;
                return;
            }
            int catalog_id = Etc.get_catalog_id_by_asset_id(asset_id);
            e.CanExecute = CatalogType.is_ipp(catalog_id);
        }

        private void _cmdViewPP_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = get_tree_vm();
            if (ast_vm == null)
                return;
            int asset_id = ast_vm.asset_id ?? 0;

            PPView window = new PPView(asset_id);
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();
        }

        // 스위치 보기 
        private void _cmdViewSW_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eViewSW))
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
            if (asset_id == 0)
            {
                e.CanExecute = false;
                return;
            }
            int catalog_id = Etc.get_catalog_id_by_asset_id(asset_id);
            e.CanExecute = CatalogType.is_sw(catalog_id);
        }

        private void _cmdViewSW_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = get_tree_vm();
            if (ast_vm == null)
                return;
            int asset_id = ast_vm.asset_id ?? 0;

            SWView window = new SWView(asset_id);
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();
        }

        // 링크다이아그램 보기 
        private void _cmdViewLinkDiagram_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eViewLinkDiagram))
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
            e.CanExecute = asset_id > 0;

            if (asset_id > 0)
            {
                int catalog_id = Etc.get_catalog_id_by_asset_id(asset_id);
                e.CanExecute = !CatalogType.is_eb(catalog_id);
                // IPM 이면 링크 다이어그램  필요 없음 처리 romee 2/9
            }
        }

        private void _cmdViewLinkDiagram_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM ast_vm = get_tree_vm();
            if (ast_vm == null)
                return;
            int asset_id = ast_vm.asset_id ?? 0;

            LinkDiagramView window = new LinkDiagramView(asset_id);
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();
        }

        // 환경 장치 층별 보기 
        private void _cmdViewFloorIPM_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
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

            if(ast.asset_id != null)
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
        // 룸에 랙 설정 
        private void _cmdRoomRackLayoutMount_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!UserRight.is_ok(eUserRight.eRoomRackLayoutMount))
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
                e.CanExecute = false;
                return;
            }

            e.CanExecute = (ast_vm.disp_level == 5) || (ast_vm.disp_level == 6);
        }

        private void _cmdCRoomRackLayout_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssetTreeVM at = (AssetTreeVM)_ctlLeftSide._tvAssetTree.SelectedItem;
            if (at != null)
            {
                AssetTreeVM ast_vm = _ctlLeftSide.getAndCopyAssetTreeVM(at);
                RoomAndRackLayoutManager window = new RoomAndRackLayoutManager(ast_vm);
                window.Owner = Application.Current.MainWindow;
                window.ShowDialog();
            }
        }

        #endregion

        #region 윈도우 이벤트
        //윈도우 로딩 이벤트
        private void _windowMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //윈도우 사이즈에 따라 페이지 사이즈를 변경한다
            _graidFrame.Width = _gridCenter.ActualWidth - 20 - _gridCenterLeft.ActualWidth - _gridCenterRight.ActualWidth;
            _graidFrame.Height = _gridCenter.ActualHeight - 20;
            _gridLeftSideMenuIn.Height = _gridCenter.ActualHeight - 10;
            _gridRightSide.Height = _gridCenter.ActualHeight - 10;


            //트리뷰의 select item 이벤트 체크
            //_ctlLeftSide.selectItemChange += new LeftSideControl.SelectItemChangeEventHandler(LeftTreeSelectItemEvent);
        }


        //Page1 Region2 에서 site를 더블클릭 했을때 호출
        //사이트 정보를 변경한다
        public void selectSiteEvent(int id)
        {
            //     changePage(PageInfo.P4_ASSET_MANAGER);

            if (UserRight.is_site_user_right(id) == false)
            {
                MessageBox.Show(g.tr_get("C_Error_Site"));
                return;
            }
            P4AssetView p4 = (P4AssetView)_framePage4View.Content;
            site st = g.site_list.Find(at => at.site_id == id);
            if (st == null)
                return;
            if (Reg.get_dcim() == 0) // 초기에 셋팅한 한번만 처리 한다. 다음부턴 사이트아이디로 처리 
            {
                Reg.save_dcim(st.site_id);
            }
            // 진행 바를 출력...
            ProgressBarDialog4 progress = new ProgressBarDialog4();
            progress.Show();
            progress.setPercent(30);
            progress.setStatus("Loading site data...");

            g.select_site = st;


            if (isReadyMainRun == false)
            {
                isReadyMainRun = true;

                //left, right side Event enable
                _canvasLeftSideMenuShowButton.IsEnabled = true;
                _canvasRightSideMenuShowButton.IsEnabled = true;
                _btnTopMenu_SelectCenter.IsEnabled = true;
                _btnTopMenu_DashBoard.IsEnabled = true;
                _btnTopMenu_FloorView.IsEnabled = true;
                _btnTopMenu_AssetManage.IsEnabled = true;
                _btnTopMenu_LineManage.IsEnabled = true;


                _ctlLeftSide.InitLeftSide(id);
                progress.setPercent(60);
                p4.InitAssetView(id);

            }
            else
            {
                // 두번째 로긴 부터는 초기화를 여기서 처리  ?? 이유 ?? romee
                _ctlLeftSide.reInitLeftSide(id);
                progress.setPercent(60);
                p4.ReInitAssetView(id);
            }
            _btnTopMenu_AssetManage.IsChecked = true;

            try
            {
                g.selected_building_id = g.building_list.Find(p => p.site_id == id).building_id;
            }
            catch { }

            if (g.selected_building_id > 0)
            {
                P3FloorView p3 = (P3FloorView)_framePage3View.Content; // 층별뷰 업데이트 처리 
                p3.setBuilding(g.selected_building_id);
            }

            P2DashBoard p2 = (P2DashBoard)_framePage2View.Content;
            p2.PageLoad();

            progress.setPercent(100);
            progress.Close();
        }

        //트리에서 자산이 선택된 경우
        private void TreeViewSelectItem(AssetTreeVM ast_vm)
        {

            //1. 자산뷰를 변경한다
            P4AssetView p4 = (P4AssetView)_framePage4View.Content;
            p4.assetChange(ast_vm);
            if (_btnTopMenu_AssetManage.IsChecked == false)
                _btnTopMenu_AssetManage.IsChecked = true;

            //2. 필요한 경우 빌딩뷰를 변경한다


            //3. 


            P3FloorView p3 = (P3FloorView)_framePage3View.Content;

            //p3.setBuilding(ast_vm.type_id);

        }


        //윈도우 리사이즈 이벤트
        private void _windowMainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //윈도우 사이즈 변화에 따라 페이지 프래임 사이즈를 변경한다
            // minimum 1280x1024
            if (_gridCenter.ActualWidth > 1280)
                _graidFrame.Width = _gridCenter.ActualWidth - 20 - _gridCenterLeft.ActualWidth - _gridCenterRight.ActualWidth;
            if (_gridCenter.ActualHeight > (800))
                _graidFrame.Height = _gridCenter.ActualHeight - 20;

            _gridLeftSideMenuIn.Height = _gridCenter.ActualHeight - 10;
            _gridRightSide.Height = _gridCenter.ActualHeight - 10;

        }


        #endregion

        #region TopMenu 이벤트

        //      WindowState mainwindow_status = WindowState.Normal;

        //상위탭을 선택했을때 창이 이동가능하도록 해준다
        private void _btnTopMenuGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (_windowMainWindow.WindowState == WindowState.Normal)
                    _windowMainWindow.WindowState = WindowState.Maximized;
                else
                    _windowMainWindow.WindowState = WindowState.Normal;

                //윈도우 사이즈 변화에 따라 페이지 프래임 사이즈를 변경한다
                // minimum 1280x1024
                if (_gridCenter.ActualWidth > 1280)
                    _graidFrame.Width = _gridCenter.ActualWidth - 20 - _gridCenterLeft.ActualWidth - _gridCenterRight.ActualWidth;
                if (_gridCenter.ActualHeight > 800)
                    _graidFrame.Height = _gridCenter.ActualHeight - 20;

                _gridLeftSideMenuIn.Height = _gridCenter.ActualHeight - 10;
                _gridRightSide.Height = _gridCenter.ActualHeight - 10;
            }
            else
            {
                try
                {
                    DragMove();
                }
                catch { }
            }
        }

        //종료 빨콩을 누르면 종료한다
        private void ExitButton_Clicked(object sender, RoutedEventArgs e)
        {
            Close();

        }
        #endregion

        #region TopMenu버튼 checked 이벤트
        //page1을 선택한 경우
        private void _btnTopMenu_SelectCenter_Checked(object sender, RoutedEventArgs e)
        {
            changePageAndButton(PageInfo.P1_SELECT_CENTOR);
        }
        //page2를 선택한 경우
        private void _btnTopMenu_DashBoard_Checked(object sender, RoutedEventArgs e)
        {
            changePageAndButton(PageInfo.P2_DASHBOARD);
        }

        //page3을 선택한 경우
        private void _btnTopMenu_FloorView_Checked(object sender, RoutedEventArgs e)
        {
            changePageAndButton(PageInfo.P3_FLOOR_VIEW);
        }

        //page4을 선택한 경우
        private void _btnTopMenu_AssetManage_Checked(object sender, RoutedEventArgs e)
        {
            changePageAndButton(PageInfo.P4_ASSET_MANAGER);
        }

        //page5을 선택한 경우
        private void _btnTopMenu_LineManage_Checked(object sender, RoutedEventArgs e)
        {
            changePageAndButton(PageInfo.P5_LINE_MANAGER);
        }

        //page6을 선택한 경우
        private void _btnTopMenu_IPM_Checked(object sender, RoutedEventArgs e)
        {
            changePageAndButton(PageInfo.P6_IPM);
        }
        #endregion

        #region 사이드바 버튼 관련 이벤트
        private void _gridLeftSideMenuIn_MouseLeave(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(_gridCenter);

            if ((p.X > 300) || (p.Y < 0) || (p.X > 920))
            {
                if (_togglebtnLeftSideBarToggle.IsChecked == true)
                    _togglebtnLeftSideBarToggle.IsChecked = false;
            }
        }
        //왼쪽 끝으로 마우스 이동하면 사이드바 Show버튼 Checked시킴
        private void _canvasLeftSideMenuShowButton_MouseEnter(object sender, MouseEventArgs e)
        {
            if (_togglebtnLeftSideBarToggle.IsChecked == false)
            {
                _togglebtnLeftSideBarToggle.IsChecked = true;

            }
        }


        //왼쪽 사이드바 Show버튼
        private void _togglebtnLeftSideBarToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            SimpleAnimation anim = new SimpleAnimation();
            Point temp_from_p = new Point(0, 0);
            ////Point tempToP = new Point(295, 0);
            Point tempToP = new Point(-100, 0);
            anim.gridMoveAnimation(_gridLeftSideMenuShowButton, temp_from_p, tempToP, 0.8, 300);


            Point temp_center = new Point(0, 0);
            Vector temp_from_v = new Vector(0, 1);
            Vector temp_to_v = new Vector(1, 1);
            anim.gridScaleAnimation(_gridLeftSideMenuIn, temp_from_v, temp_to_v, temp_center, 0.8, 300);
        }
        //왼쪽 사이드바 Hide버튼
        private void _togglebtnLeftSideBarToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            SimpleAnimation anim = new SimpleAnimation();
            //Point temp_from_p = new Point(295, 0);
            Point temp_from_p = new Point(-100, 0);
            Point temp_to_p = new Point(0, 0);
            anim.gridMoveAnimation(_gridLeftSideMenuShowButton, temp_from_p, temp_to_p, 0.8, 300);

            Point temp_center = new Point(0, 0);
            Vector temp_from_v = new Vector(1, 1);
            Vector temp_to_v = new Vector(0, 1);
            anim.gridScaleAnimation(_gridLeftSideMenuIn, temp_from_v, temp_to_v, temp_center, 0.8, 300);
        }

        //오른쪽 끝에서 마우스 이탈시에 hide Right Side
        private void _gridRightSide_MouseLeave(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(_gridRightSide);

            if ((p.X < 0) || (p.Y < 0) || (p.X > 920))
            {
                if (_togglebtnRightSideBarToggle.IsChecked == true)
                    _togglebtnRightSideBarToggle.IsChecked = false;
            }
        }

        //오른쪽 끝으로 마우스 이동하면 사이드바 Show버튼 Checked시킴
        private void _canvasRightSideMenuShowButton_MouseEnter(object sender, MouseEventArgs e)
        {
            if (_togglebtnRightSideBarToggle.IsChecked == false)
            {
                _togglebtnRightSideBarToggle.IsChecked = true;
            }
        }

        //오른쪽 사이드바  show 버튼
        private void _togglebtnRightSideBarToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            SimpleAnimation anim = new SimpleAnimation();
            Point temp_from_p = new Point(0, 0);
            //Point temp_to_p = new Point(-400, 0);
            Point temp_to_p = new Point(50, 0);
            anim.gridMoveAnimation(_gridRightSideMenuShowButton, temp_from_p, temp_to_p, 0.8, 400);


            Point tempCenter = new Point(400, 0);
            Vector temp_from_v = new Vector(0, 1);
            Vector temp_to_v = new Vector(1, 1);
            anim.gridScaleAnimation(_gridRightSide, temp_from_v, temp_to_v, tempCenter, 0.8, 400);
        }

        //오른쪽 사이드바  Hide버튼
        private void _togglebtnRightSideBarToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            SimpleAnimation anim = new SimpleAnimation();
            //Point temp_from_p = new Point(-400, 0);
            Point temp_from_p = new Point(50, 0);
            Point temp_to_p = new Point(0, 0);
            anim.gridMoveAnimation(_gridRightSideMenuShowButton, temp_from_p, temp_to_p, 0.8, 400);

            Point temp_center = new Point(400, 0);
            Vector temp_from_v = new Vector(1, 1);
            Vector temp_to_v = new Vector(0, 1);
            anim.gridScaleAnimation(_gridRightSide, temp_from_v, temp_to_v, temp_center, 0.8, 400);
        }

        #endregion

        #region 페이지 전환 메소드
        private void changePageAndButton(PageInfo newPage)
        {
            if (isReadyMainRun == false)
                return;

            //page Hide전환 애니메이션 값을 설정한다
            Point hide_page_center = new Point(0, 0);
            Vector hide_page_from_v = new Vector(1, 1);
            Vector hide_page_to_v = new Vector(0, 0);
            SimpleAnimation anim = new SimpleAnimation();

            //  페이지 전환
            //이전 페이지에 축소, 이전 페이지 버튼 활성화, 버튼 unchecked
            switch (current_page)
            {
                case PageInfo.P1_SELECT_CENTOR:
                    hide_page_center.X = 60;
                    anim.FrameScaleAnimation(_framePage1View, hide_page_from_v, hide_page_to_v, hide_page_center, 0.8, 300);
                    _btnTopMenu_SelectCenter.IsEnabled = true;
                    _btnTopMenu_SelectCenter.IsChecked = false;
                    break;
                case PageInfo.P2_DASHBOARD:
                    hide_page_center.X = 180;
                    anim.FrameScaleAnimation(_framePage2View, hide_page_from_v, hide_page_to_v, hide_page_center, 0.8, 300);
                    _btnTopMenu_DashBoard.IsEnabled = true;
                    _btnTopMenu_DashBoard.IsChecked = false;
                    break;
                case PageInfo.P3_FLOOR_VIEW:
                    hide_page_center.X = 300;
                    anim.FrameScaleAnimation(_framePage3View, hide_page_from_v, hide_page_to_v, hide_page_center, 0.8, 300);
                    _btnTopMenu_FloorView.IsEnabled = true;
                    _btnTopMenu_FloorView.IsChecked = false;
                    break;
                case PageInfo.P4_ASSET_MANAGER:
                    hide_page_center.X = 420;
                    anim.FrameScaleAnimation(_framePage4View, hide_page_from_v, hide_page_to_v, hide_page_center, 0.8, 300);
                    _btnTopMenu_AssetManage.IsEnabled = true;
                    _btnTopMenu_AssetManage.IsChecked = false;
                    break;
                case PageInfo.P5_LINE_MANAGER:
                    hide_page_center.X = 540;
                    anim.FrameScaleAnimation(_framePage5View, hide_page_from_v, hide_page_to_v, hide_page_center, 0.8, 300);
                    _btnTopMenu_LineManage.IsEnabled = true;
                    _btnTopMenu_LineManage.IsChecked = false;
                    break;
                //case PageInfo.P6_IPM:
                //    hide_page_center.X = 660;
                //    anim.FrameScaleAnimation(_framePage6View, hide_page_from_v, hide_page_to_v, hide_page_center, 0.8, 300);
                //    _btnTopMenu_IPM.IsEnabled = true;
                //    _btnTopMenu_IPM.IsChecked = false;
                //    break;
            }

            //page show   
            Point show_page_center = new Point(0, 0);
            Vector show_page_from_v = new Vector(0, 0);
            Vector show_page_to_v = new Vector(1, 1);

            //새로운 페이지 확대, 버튼 disable, current  page 변경       
            switch (newPage)
            {
                case PageInfo.P1_SELECT_CENTOR:
                    show_page_center.X = 60;
                    anim.FrameScaleAnimation(_framePage1View, show_page_from_v, show_page_to_v, show_page_center, 0.8, 300);
                    _btnTopMenu_SelectCenter.IsEnabled = false;
                    break;
                case PageInfo.P2_DASHBOARD:
                    show_page_center.X = 180;
                    anim.FrameScaleAnimation(_framePage2View, show_page_from_v, show_page_to_v, show_page_center, 0.8, 300);
                    _btnTopMenu_DashBoard.IsEnabled = false;
                    break;
                case PageInfo.P3_FLOOR_VIEW:
                    show_page_center.X = 300;
                    anim.FrameScaleAnimation(_framePage3View, show_page_from_v, show_page_to_v, show_page_center, 0.8, 300);
                    _btnTopMenu_FloorView.IsEnabled = false;
                    break;
                case PageInfo.P4_ASSET_MANAGER:
                    show_page_center.X = 420;
                    anim.FrameScaleAnimation(_framePage4View, show_page_from_v, show_page_to_v, show_page_center, 0.8, 300);
                    _btnTopMenu_AssetManage.IsEnabled = false;
                    break;
                case PageInfo.P5_LINE_MANAGER:
                    show_page_center.X = 540;
                    anim.FrameScaleAnimation(_framePage5View, show_page_from_v, show_page_to_v, show_page_center, 0.8, 300);
                    _btnTopMenu_LineManage.IsEnabled = false;
                    break;
                //case PageInfo.P6_IPM:
                //    show_page_center.X = 660;
                //    anim.FrameScaleAnimation(_framePage6View, show_page_from_v, show_page_to_v, show_page_center, 0.8, 300);
                //    _btnTopMenu_IPM.IsEnabled = false;
                //    break;
            }
            current_page = newPage;

        }

        #endregion

        #region 이벤트 처리 및 기타
        //Drop 이벤트 발생했을때 처리하는 부분에 대한 샘플
        //좌측 트리뷰에서 가져온 정보에 대하서만 적용가능
        private void _gridCenter_Drop(object sender, DragEventArgs e)
        {
            //System.Console.WriteLine("_gridCenter_Drop");
            //asset_tree_for_treeview item = (asset_tree_for_treeview)e.Data.GetData("asset_tree_for_treeview");

            //TextBlock tx = new TextBlock();
            //tx.Text = item.disp_name;
            //tx.Foreground = Brushes.White;
            //tx.Background = Brushes.Black;
            //tx.HorizontalAlignment = HorizontalAlignment.Center;
            //tx.VerticalAlignment = VerticalAlignment.Center;

            //_gridCenter.Children.Add(tx);

        }

        private void _lvEvent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
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

            if(eh.asset_id > 0 && String.IsNullOrEmpty(eh.ipv4)) // romee/1/22   // 서버에서 ipv4를 저장하지 않으므로 이벤트 수신시 해당 ipv4 저장 처리 
            {
                asset ast = (asset)await g.webapi.get("asset", eh.asset_id ?? 0, typeof(asset));
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

            int i = 0;
            Object[] obj = new object[] { i.ToString() };
            _lvEvent.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(delegate()
            {
                _lvEvent.ItemsSource = null;
                _lvEvent.ItemsSource = g.popup_event_list;
            }));
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

        // 시그널R 재연결하는 루틴으로 디버그를 장시간(30초? 이상)하는 경우 연결이 끊기는 거에 대비하기 위한... (개발자를 위한 기능)
        // 향후 더 보강을 하려면 리커넥트를 자동으로 하는 것 정도...
        private async void _imgConnect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (g.signalr.State == ConnectionState.Disconnected)
            {
                await g.signalr.connect(g.signalr_uri_string);
            }
        }

        private void _framePage1View_Loaded(object sender, RoutedEventArgs e)
        {

        }



        static bool _page1_loaded = false;
        private void _framePage1View_LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (_page1_loaded)
            {
                return;
            }

            _page1_loaded = true;
            P1SelectCenter_World p1_world = (P1SelectCenter_World)_framePage1View.Content;

            //p1_world.selectSiteEventToMain += new P1SelectCenter_World.selectSiteEventHandler(P1SelectSiteEvent);

        }

        private void _framePage1View_Initialized(object sender, EventArgs e)
        {

        }

        private void _framePage4View_LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (Reg.get_dcim() > 1) // DCIM 처리 romee
            {
                int ret1 = Reg.get_dcim();
                g.selected_site_id = ret1;
                selectSiteEvent(ret1);
            }

        }

        private void putAndPullLeftSideControl(object o)
        {
            string str = (string)o;
            switch (str)
            {
                case "l_put":
                    _gridLeftSideMenuIn.Children.Clear();
                    _gridCenterLeft.Children.Add(_ctlLeftSide);
                    _canvasLeftSideMenuShowButton.Visibility = System.Windows.Visibility.Hidden;
                    //윈도우 사이즈에 따라 페이지 사이즈를 변경한다
                    //_gridCenterLeft.Width = _canvasLeftSideMenu.ActualWidth;
                    //_graidFrame.Width = _graidFrame.Width - _canvasLeftSideMenu.ActualWidth;

                    _gridCenterLeft.Width = _ctlLeftSide.ActualWidth;
                    _graidFrame.Width = _graidFrame.Width - _ctlLeftSide.ActualWidth;

                    break;
                case "l_pull":
                    _gridCenterLeft.Children.Clear();
                    _gridLeftSideMenuIn.Children.Add(_ctlLeftSide);
                    _canvasLeftSideMenuShowButton.Visibility = System.Windows.Visibility.Visible;
                    _gridCenterLeft.Width = 0;

                    //_graidFrame.Width = _graidFrame.Width + _canvasLeftSideMenu.ActualWidth;
                    _graidFrame.Width = _graidFrame.Width + _ctlLeftSide.ActualWidth;
                    break;

                case "r_put":
                    _gridRightSide.Children.Clear();
                    _gridCenterRight.Children.Add(_ctlRightSide);
                    _canvasRightSideMenuShowButton.Visibility = System.Windows.Visibility.Hidden;
                    //_gridCenterRight.Width = _canvasRightSideMenu.ActualWidth;
                    //_graidFrame.Width = _graidFrame.Width - _canvasRightSideMenu.ActualWidth;
                    _gridCenterRight.Width = _ctlRightSide.ActualWidth;
                    _graidFrame.Width = _graidFrame.Width - _ctlRightSide.ActualWidth;

                    break;
                case "r_pull":
                    _gridCenterRight.Children.Clear();
                    _gridRightSide.Children.Add(_ctlRightSide);
                    _canvasRightSideMenuShowButton.Visibility = System.Windows.Visibility.Visible;
                    _gridCenterRight.Width = 0;
                    //_graidFrame.Width = _graidFrame.Width + _canvasRightSideMenu.ActualWidth;
                    _graidFrame.Width = _graidFrame.Width + _ctlRightSide.ActualWidth;

                    break;

            }

            ////자산뷰 페이지가 열린경우 InfoView의 위치를 재보정 해야 한다
            //if(_btnTopMenu_AssetManage.IsChecked==true)
            //{
            //    P4AssetView p4 = (P4AssetView)_framePage4View.Content;
            //    p4.reflashInfoviewAll();
            //}
        }
        #endregion

        #region // 윈도우 유틸 처리 
        // Help 처리 필요 
        // 버젼 표기 romee 2015.09.03
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        //    _windowMainWindow.WindowState = WindowState.Minimized;
            var obj = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            string version = string.Format("Application Version {0}.0.{1}", obj.Major, obj.Revision);

            MessageBox.Show(this, "Version : " + version);

#if debug
            event_hist eh = g.event_hist_list.Find(p => p.event_hist_id == 19116605);

            EventVM vm = new EventVM();
            vm.template = "data";
            vm.event_hist_id = 19116605;
            vm.event_type = eEventType.eInfo;
            vm.event_text = eh.event_text;
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
#endif            

#if debug
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
#endif
        }
        // 로그 오프 처리 
        private void _btnLogoff_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(g.tr_get("C_LogOutConfirm"), g.tr_get("C_Confirm"), MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            System.Windows.Forms.Application.Restart();
            System.Windows.Application.Current.Shutdown();
        }
        // 리프레쉬 처리 
        private void _windowMainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                if (current_page == PageInfo.P1_SELECT_CENTOR) return;
                g.user_key = e;                                      // 사용자 입력키 저장  romee/1/20               
                _ctlLeftSide.reInitLeftSide(g.selected_site_id);     // romee/1/16

                if (current_page == PageInfo.P3_FLOOR_VIEW)
                {
                    //빌딩을 변경 한다
                    P3FloorView p3 = (P3FloorView)_framePage3View.Content; // 층별뷰 업데이트 처리 
                    p3.setBuilding(g.selected_building_id);
                }
            }

            if (e.Key == Key.F1)
            {
                PopUpWindow popup_window = new PopUpWindow("Version Info. : 2.0.0.2 \n LS Cable & System");
                popup_window.Title = "About Box";
                popup_window.Show();
            }
            if(_btnTopMenu_AssetManage.IsChecked==true)
            {
                if (e.Key == Key.F5)
                {
                    // 자산뷰 리프레쉬 처리 
                    P4AssetView p4 = (P4AssetView)_framePage4View.Content;
                    p4.reflashAll();
                }
            }
        }
        // 폼 최소화 처리 
        private void MiniButton_Clicked(object sender, RoutedEventArgs e)
        {
            _windowMainWindow.WindowState = WindowState.Minimized;

        }
	    #endregion

    }

    #region IValueConverter

    public class EventBorderConverter : IValueConverter
    {
        private Color red = (Color) App.Current.Resources["_colorRed"];         // error
        private Color green = (Color) App.Current.Resources["_colorGreen"];     // info
        private Color blue = (Color) App.Current.Resources["_colorBlue"];
        private Color yellow = (Color)App.Current.Resources["_colorYellow"];    // Warnning

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return Colors.Transparent;

            eEventType type = (eEventType)value;

            switch(type)
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
