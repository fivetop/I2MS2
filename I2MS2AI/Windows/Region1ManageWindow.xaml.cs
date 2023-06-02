using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using I2MS2.Models;
using I2MS2.Pages;
using I2MS2.UserControls;
using System.Windows.Threading;
using System.IO;
using System.Threading;
//using WebApiClient;
using WebApi.Models;
using I2MS2.Library;

namespace I2MS2.Windows
{
    // 세계 지동 플래그 등록 관리 
    /// <summary>
    /// AddRegion1Window.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Region1ManageWindow : Window
    {
        #region RouteCommand 버튼 관련 정의
        public static RoutedCommand NewCommand = new RoutedCommand();
        public static RoutedCommand EditCommand = new RoutedCommand();
        public static RoutedCommand DeleteCommand = new RoutedCommand();
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand CancelCommand = new RoutedCommand();

        private bool _new = true;
        private bool _edit = false;
        private bool _delete = false;
        private bool _save = false;
        private bool _cancel = false;

        private bool _new_flag = true;
        #endregion

        Boolean is_pickup_location_mode = false;
        Boolean changed_region_name = false;
        Boolean changed_region_pos = false;
        Boolean changed_image = false;

        regionFlagControl point_flag;
        Point selected_point;
        int selected_region_id = -1;
        string selected_image_file_path = null;
        string selected_image_file_name = null;
        string selected_region_name = null;
        string old_region_name = null;

        RegionManage region_mgr;
        ImageManage img_mgr;

        region1 _left_item = null;


        //부모 클래스 내부의 이벤트 호출 연결 고리
        public delegate void AddParentFlagHander(object obj);
        public event AddParentFlagHander addParentFlagEvent;

        public delegate void DelParentFlagHander(object obj);
        public event DelParentFlagHander delParentFlagEvent;

        public delegate void MoveParentFlagHander(object obj);
        public event MoveParentFlagHander moveParentFlagEvent;

        public delegate void EditParentFlagHander(object obj);
        public event EditParentFlagHander editParentFlagEvent;


        #region 초기화
        //초기화
        public Region1ManageWindow(Size _worldMapSize)
        {
            InitializeComponent();

            //로컬에서 사용되는 인스턴스 UI 등의 초기화
            initLocal(_worldMapSize);

        }

        private void initLocal(Size _worldMapSize)
        {
            //region관리 클래스
            region_mgr = new RegionManage(_worldMapSize);
            img_mgr = new ImageManage();

            //리스트 뷰에 region1 리스트 표시
            _lvLeft.ItemsSource = g.region1_list;

            //flag 를 미리 그려놓고 숨겨둠
            Point tempPoint = new Point(-100, -100);
            point_flag = region_mgr.makeRegion1Flag("name", tempPoint);
            _gridCenter.Children.Add(point_flag);

            //선택 포인트를 초기화
            selected_point = new Point(0, 0);
        }
        
        #endregion


        #region 오른쪽 아래 수정창의 이벤트
        //Region 이름을 변경하면 발생하는 이벤트
        private void _txtRegionName_TextChanged(object sender, TextChangedEventArgs e)
        {
            changed_region_name = true;
            selected_region_name = _txtRegionName.Text;

            byte[] mybyte = Encoding.Default.GetBytes(_txtRegionName.Text);
            int TotalLen = int.Parse(mybyte.Length.ToString());
            if (TotalLen > 10)
            {
                Console.WriteLine("총 글자수는 100자를 초과할 수 없습니다.");
            }
        }

        //로케이션 위치를 선택하는 버튼 클릭
        private void _btnPickRegion(object sender, RoutedEventArgs e)
        {
            region_mgr.setMapSize(_imgMap.ActualWidth, _imgMap.ActualHeight);
            is_pickup_location_mode = true;
            changed_region_pos = true;
        }


        //로케이션 맵 이미지를 추가하는 버튼 클릭
        private void _btnAddRegionMap_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open_file_dialog = new OpenFileDialog();
            open_file_dialog.Filter = "Image Files |*.jpeg;*.png;*.jpg;*.gif";

            Nullable<Boolean> result = open_file_dialog.ShowDialog();
            if (result == true)
            {
                selected_image_file_path = open_file_dialog.FileName;
                selected_image_file_name = open_file_dialog.SafeFileName;
                _lblAddRegionMap.Text = string.Format("{0}", open_file_dialog.FileName);
                changed_image = true;
            }
        } 
        #endregion


        #region CRUD 신규,삭제 등 버튼 처리 로직

        private void _cmdNew_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _new;
        }

        private void _cmdNew_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _new_flag = true;
            clearControlLeft();
            enableControlLeft(true);
            resetRegionInfoPanel();
            _txtRegionName.Text = ""; // 신규일 경우만 초기화 처리 

            _new = false;
            _delete = false;
            _edit = false;
            _save = true;
            _cancel = true;

            _txtRegionName.Focus();
            CommandManager.InvalidateRequerySuggested();

        }

        private void _cmdEdit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                if (_txtRegionName.Text == "")
                {
                    e.CanExecute = false;
                    return;
                }
            }
            catch (Exception e1)
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = _edit;
        }

        private void _cmdEdit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            enableControlLeft(true);
            _new = false;
            _delete = false;
            _edit = false;
            _save = true;
            _cancel = true;
            _new_flag = false;

            _txtRegionName.Focus();
        }

        private void _cmdDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            int idx = _lvLeft.SelectedIndex;

            if (idx == -1)
            {
                e.CanExecute = false;
                return;
            }
            if (!_delete)
            {
                e.CanExecute = false;
                return;
            }
            e.CanExecute = true;
        }

        private async void _cmdDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(g.tr_get("C_Delete_Item"), g.tr_get("C_Confirm"), MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            if (await delete())
            {
                refreshLeft(-1);
            }
            resetRegionInfoPanel();
            clearControlLeft();
            enableControlLeft(false);

            _new = true;
            _edit = false;
            _delete = false;
            _save = false;
            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();

        }

        private void _cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try { 
                if(_txtRegionName.Text == "")
                {
                    e.CanExecute = false;
                    return;
                }
            }
            catch(Exception e1)
            {
                e.CanExecute = false;
                return;
            }
            e.CanExecute = _save;
        }
        // 저장 처리 
        private async void _cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!await save())
                return;
            resetRegionInfoPanel();
            if (_new_flag)
            {
                // 화면을 갱신하기 위해 다시 데이터를 리스트에 로드하고 선택한다.
                refreshLeft(-1);
            }
            else
            {
                int idx = _lvLeft.SelectedIndex;
                refreshLeft(idx+1);
            }
            clearControlLeft();
            enableControlLeft(false);

            _new = true;
            _delete = false;
            _edit = false;
            _save = false;
            _cancel = false;

            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }

        private void _cmdCancel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _cancel;
        }

        private void _cmdCancel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            clearControlLeft();
            enableControlLeft(false);

            _new = true;
            _delete = false;
            _edit = false;
            _save = false;
            _cancel = false;
        }
        #endregion


        #region 마우스 동작시 event

        //centerGrid에서 마우스가 위치를 선택한 경우 => 로케이션 위치를 선택할때만 사용
        private void centerGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (is_pickup_location_mode == true)
            {
                //width = 760, height = 377
                Point tempP = e.GetPosition(_gridCenter);

                //Map 안에서 선택한 경우에만 허용
                if (region_mgr.checkPointInMap(e.GetPosition(_gridCenter)) == true)
                {
                    Point selectP = e.GetPosition(_gridMap);
                    is_pickup_location_mode = false;

                    //현재 좌표를 선택된 좌표로 지정
                    selected_point = new Point(selectP.X, selectP.Y);
                    _lblPickUpRegion.Text = string.Format("({0},{1})", selected_point.X, selected_point.Y);
                    _btnPickUpRegion.IsChecked = false;
                }
            }
        }

        //centerGrid에서 마우스가 움직일때 이벤트 발생 => 로케이션 위치를 선택할때만 사용
        private void centerGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (is_pickup_location_mode == true)
            {
                //flag가 mouse를 따라 이동
                Point tempPoint = e.GetPosition(_gridCenter);
                Thickness tempMargin = new Thickness(tempPoint.X - point_flag.Width / 2, tempPoint.Y - point_flag.Height / 2, 0, 0);
                point_flag.Margin = tempMargin;
  
            }
        } 
        #endregion

        #region refresh 로직,  diplay 로직, clear control 로직, 화면 컨트롤 enable
        // 삭제 후 리프레쉬는 인덱스값을 지정하고, 추가 후 리프레쉬는 인덱스 값을 -1을 부여한다.
        private void refreshLeft(int idx)
        {
            if (_lvLeft == null)
                return;

            // 삭제 시
            if (idx > 0)
            {
                _lvLeft.ItemsSource = null;
                _lvLeft.ItemsSource = g.region1_list;
                _lvLeft.SelectedIndex = idx - 1;
            }

            // 추가 또는 수정 시
            if (idx == -1)
            {
                int id = 0;
                if (_left_item != null)
                    id = _left_item.user_id;
                _lvLeft.ItemsSource = null;
                _lvLeft.ItemsSource = g.region1_list;
                _lvLeft.SelectedValue = id;
            }

            var node = _lvLeft.SelectedItem;
            if (node != null)
                _lvLeft.ScrollIntoView(node);
            dispLeft();
            return;
        }

        // 화면에 메모리 내용 디스플레이 -> 리스트에서 선택후 처리됨 
        private void dispLeft()
        {
            var item = _left_item;
            if (item == null)
                return;

            //정보창에 선택된 region의 이름,위치, 이미지 파일 이름을 표시한다
            _txtRegionName.Text = _left_item.region1_name;
            _lblPickUpRegion.Text = string.Format("({0},{1})", _left_item.pos_x, _left_item.pos_y);
            string tmp_str = region_mgr.getImageFileName((int)_left_item.region1_image_id);
            if (tmp_str != null)
            {
                _lblAddRegionMap.Text = tmp_str;
                selected_image_file_name = tmp_str;
            }
            else
                Console.WriteLine("not match region id!!!");

        }


        // 신규 처리시 기존 내용 없애는 용도 
        private void clearControlLeft()
        {
            _txtRegionName.Clear();
            _lblPickUpRegion.Text = ""; // .con.ClearValue(); ;; .Clear();
            _lblAddRegionMap.Text = ""; // .Clear();
            _lvLeft.SelectedIndex = -1;
        }
        // enable control 로직
        private void enableControlLeft(bool flag)
        {
            _txtRegionName.IsEnabled = flag;
            _btnPickUpRegion.IsEnabled = flag;
            _btnAddRegionMap.IsEnabled = flag;
        }
        #endregion


        #region 리스트 뷰에서 아이템 선택

        //리스트뷰에서 아이템을 선택한 이벤트
        private void _lvLeft_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _new = true;
            _delete = true;
            _edit = true;
            _save = false;
            _cancel = false;

            _left_item = (region1)_lvLeft.SelectedItem;

            if (_left_item == null)
                return;
            dispLeft();
            enableControlLeft(false);

            //select 된 위치와 이름,ㅡID를 받아서 클래스 변수에 저장
            Point regionSP = region_mgr.changePosition_DefaultPointToSmalMap((double)_left_item.pos_x, (double)_left_item.pos_y);
            selected_point.X = regionSP.X;
            selected_point.Y = regionSP.Y;
            selected_region_name = _left_item.region1_name;
            selected_region_id = _left_item.region1_id;
            old_region_name = selected_region_name;


            //select 된 위치로 flag를 이동
            Point tempPosition = region_mgr.changePosition_DefaultPointToSmalMap((double)_left_item.pos_x, (double)_left_item.pos_y);
            Thickness tempMargin =
                new Thickness(tempPosition.X + _gridMap.Margin.Left - point_flag.Width / 2,
                              tempPosition.Y + _gridMap.Margin.Top - point_flag.Height / 2,
                              0, 0);
            point_flag.Margin = tempMargin;

        }
        #endregion


        #region 지역정보 신규, 저장, 삭제 등의 이벤트

        //'삭제' 버튼: 선택된 region1Data를 삭제 한다
        private async Task<bool> delete()
        {
            string tregion_name = _txtRegionName.Text.Trim();

            if (tregion_name == "") return false;

            try
            {
                region2 r2 = (region2)g.region2_list.First(at => at.region1_id == selected_region_id);
                if (r2 != null) // 하위가 있으면 삭제 불가 
                {
                    MessageBox.Show(g.tr_get("C_Error_Catalog_Group_1"));
                    return false;
                }
            }
            catch {
                //return;
            }
            //서버의 파일을 삭제 한다
            var t2 = img_mgr.DelImageToServer("map",selected_image_file_name);
            Boolean result = await t2;
            if (result == false)
                Console.WriteLine("Warning:delete image fail!");

            region1 r1 = (region1)g.region1_list.First(at => at.region1_id == selected_region_id);

            if (r1 == null) return false;
            //DB이미지 리스트에서 해당 이미지 정보 삭제
//            region_mgr.delImageToDb((int)r1.region1_image_id, selected_image_file_name);
            img_mgr.delImageToDb((int)r1.region1_image_id,"map", selected_image_file_name);

            //DB에서 해당 region1 데이터 삭제
            region_mgr.delRegion1DataToDb(selected_region_id);
            
            //page1에 region flag를 삭제 한다
            this.delParentFlagEvent(selected_region_id);

            return true;
        }

        //'저장' 버튼: 작성한 지역정보를 추가 또는 수정한다
        private async Task<bool> save()
        {
            string name = _txtRegionName.Text;

            if (name == null || name == "")
            {
                MessageBox.Show(g.tr_get("M1_World_NoName"));
                return false;
            }

            // 신규 지역 등록인 경우
            if (_new_flag)
            {
                //선택된 이미지 파일, 위치를 확인한다
                if (selected_point == new Point(0,0))
                {
                    MessageBox.Show(g.tr_get("M1_World_NoLocation"));
                    return false;
                }
                else if(selected_image_file_path == null)
                {
                    MessageBox.Show(g.tr_get("M1_World_NoImage"));
                    return false;
                }

                //동일한 region1이 존재하는지 확인한다
                foreach(var at in g.region1_list)
                {
                    if (at.region1_name == name)
                    {
                        MessageBox.Show(g.tr_get("M1_World_DuplicateName"));
                        return false;
                    }
                }

                //이미지를 서버에 등록한다
                string server_file_name;
                // var t2 = region_mgr.addImageToServer(selected_image_file_path, selected_image_file_name);
                var t2 = await img_mgr.addImageToServer(selected_image_file_path,"map", selected_image_file_name);
                
                
                server_file_name = t2;
                if (server_file_name != null)
                {
                    try
                    {
                        //이미지 정보를 DB에 추가한다
                        var t3 = await img_mgr.addImageToDb(server_file_name, "map", name);
                        int img_id = t3;

                        //region1정보를 DB에 추가한다
                        var t4 = region_mgr.addRegion1ToDb(name, img_id, selected_point);
                        region1 tmp_region1 = await t4;
                        // 부모 페이지의 월드맵에 해당 지점을 표시 한다
                        this.addParentFlagEvent(tmp_region1);
                        // romee 동기화 필요 
                        g.location_list = (List<location>)await g.webapi.getList("location", typeof(List<location>));
                    }
                    catch {
                        Console.WriteLine("Check Server Log..");
                        // MessageBox.Show("Check Server Log..");
                    }
                }
                else
                {
                    MessageBox.Show(g.tr_get("M1_World_UpImageFails"));
                    return false;
                }
                
            }
            // 기존에 있는 지역을 수정하는 경우
            else
            {
                string server_file_name;
                selected_region_id = _left_item.region1_id;

                //이미지가 수정되었으면 동작
                if (changed_image == true)
                {
                    //Console.WriteLine("==== changed_image ====");
                    //서버에 기존 이미지 파일 삭제

                    //var t1 = region_mgr.DelImageToServer(selected_image_file_name);
                    var t1 = img_mgr.DelImageToServer("map",selected_image_file_name);
                    Boolean result = await t1;
                    
                    //서버에 새로운 이미지 파일 삽입
                    // var t2 = region_mgr.addImageToServer(selected_image_file_path, selected_image_file_name);
                    var t2 = img_mgr.addImageToServer(selected_image_file_path, "map", selected_image_file_name);
                    server_file_name = await t2;
                    
                    if (server_file_name != null)
                    {
                        region1 r = (region1)g.region1_list.First( at => at.region1_id == selected_region_id);
                        //var t3 = region_mgr.modifyImageToDb((int)r.region1_image_id, server_file_name);
                        var t3 = img_mgr.modifyImageToDb((int)r.region1_image_id, server_file_name);
                        //int it = await t3;
                    }
                    else
                    {
                        MessageBox.Show(g.tr_get("M1_World_ChImageFail"));
                        return false;
                    }
                    
                }
                
                //이름과 포지션이 수정된 경우 동작
                if ((changed_region_pos == true)
                    || (changed_region_name == true))
                {
                    //동일한 region1이 존재하는지 확인한다
                    foreach (var at in g.region1_list)
                    {
                        if (at.region1_name == old_region_name && old_region_name == name)
                            continue;
                        if (at.region1_name == name)
                        {
                            MessageBox.Show(g.tr_get("M1_World_DuplicateName"));
                            return false;
                        }
                    }

                    location l = g.location_list.Find(at => (at.region1_id == selected_region_id) && (at.region2_id == null));
                    if (l == null) return false;
                    
                    //DB 변경
                    var t5 = region_mgr.modifyRegion1ToDb(selected_region_id, _txtRegionName.Text, selected_point, changed_region_name);
                    region1 r1 = await t5;

                    //전역변수 변경
                    region1 g_r1 = g.region1_list.Find(at => at.region1_id == r1.region1_id);
                    if (g_r1 == null) return false;
                    g_r1.region1_name = r1.region1_name;
                    g_r1.pos_x = r1.pos_x;
                    g_r1.pos_y = r1.pos_y;
                    g_r1.remarks = r1.remarks;

                    //전역 ast_vm_dic 변경
                    if (g.location_ast_vm_dic.ContainsKey(l.location_id))
                    {
                        AssetTreeVM region1_ast_vm = g.location_ast_vm_dic[l.location_id];
                        region1_ast_vm.disp_name = r1.region1_name;
                    }

                    //관련 데이터 변경
                    Boolean ret = await g.left_tree_handler.editRegion1(selected_region_id, r1.region1_name);
                    // region Map에 해당 포인트의 flag를 변경된 포인트로 이동한다
                    this.moveParentFlagEvent(r1);
                }
            }
            return true;
        } 


        #endregion


        #region 윈도우, 탭 이벤트

        // 윈도우 닫기 버튼클릭
        private void _btnCloseRegion_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            region_mgr.setMapSize(_imgMap.ActualWidth, _imgMap.ActualHeight);
        }

        private void _gridTab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        #endregion


        #region 기타 메소드
        // UI와 선택된 모든 정보의 초기화
        private void resetRegionInfoPanel()
        {
            //지도상의 포인트를 지워준다
            Thickness tempMargin = new Thickness(-100, -100, 0, 0);
            point_flag.Margin = tempMargin;
            selected_region_name = null;
            old_region_name = null;
            changed_image = false;
            changed_region_name = false;
            changed_region_pos = false;

            //선택된 ID를 -1로 만든다
            selected_region_id = -1;

            selected_point = new Point(0, 0);
        } 
        #endregion
    }
}
