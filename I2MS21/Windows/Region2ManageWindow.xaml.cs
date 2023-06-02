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
    // 나라지도에 지역 등록 관리 
    /// <summary>
    /// AddRegion1Window.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Region2ManageWindow : Window
    {
        Boolean is_pickup_location_mode = false;
        Boolean changed_region_name = false;
        Boolean changed_region_pos = false;
        
        regionFlagControl point_flag;
        Point selected_point;
        int selected_region_id = -1;
        string selected_region_name = null;

        //현재 region1의 ID
        int cur_region1_id;

        RegionManage region_mgr;
        ImageManage img_mgr;
        

        //부모 클래스 내부의 이벤트 호출 연결 고리
        public delegate void AddParentFlagHander(object obj);
        public event AddParentFlagHander addParentFlagEvent;

        public delegate void DelParentFlagHander(object obj);
        public event DelParentFlagHander delParentFlagEvent;

        public delegate void MoveParentFlagHander(object obj);
        public event MoveParentFlagHander moveParentFlagEvent;


        #region 초기화
        //초기화
        public Region2ManageWindow(int region1_id ,string image_path, Size _worldMapSize)
        {
            InitializeComponent();


            //로컬에서 사용되는 인스턴스 UI 등의 초기화
            initLocal(region1_id, image_path, _worldMapSize);

        }

        private void initLocal(int region1_id, string image_path, Size _worldMapSize)
        {
            cur_region1_id = region1_id;


            //region관리 클래스
            region_mgr = new RegionManage(_worldMapSize);
            img_mgr = new ImageManage();

            //리스트 뷰에 region1 리스트 표시
            _lvRegionInfo.ItemsSource = g.region2_list.Where(at => at.region1_id == cur_region1_id);

            //flag 를 미리 그려놓고 숨겨둠
            Point tempPoint = new Point(-100, -100);
            point_flag = region_mgr.makeRegion1Flag("name", tempPoint);
            _gridCenter.Children.Add(point_flag);

            //선택 포인트를 초기화
            selected_point = new Point(0, 0);

            _imgMap.Source = new BitmapImage(new Uri(image_path));
        }
        
        #endregion


        #region 오른쪽 아래 수정창의 이벤트
        //Region 이름을 변경하면 발생하는 이벤트
        private void _txtRegionName_TextChanged(object sender, TextChangedEventArgs e)
        {
            changed_region_name = true;
            selected_region_name = _txtRegionName.Text;
        }

        //로케이션 위치를 선택하는 버튼 클릭
        private void _btnPickRegion(object sender, RoutedEventArgs e)
        {
            region_mgr.setMapSize(_imgMap.ActualWidth, _imgMap.ActualHeight);
            is_pickup_location_mode = true;
            changed_region_pos = true;
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


        #region 리스트 뷰에서 아이템 선택

        //리스트뷰에서 아이템을 선택한 이벤트
        private void _lvRegionInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_lvRegionInfo.SelectedItem != null)
            {
                //선택된 아이템에 따라 정보를 표시하고 select된 정보로 클래스 변수에 저장한다
                region1ItemSeletionChanged();
            }
        } 


        private void region1ItemSeletionChanged()
        {
            region2 r = (region2)_lvRegionInfo.SelectedItem;

            //select 된 위치와 이름,ㅡID를 받아서 클래스 변수에 저장
            Point regionSP = region_mgr.changePosition_DefaultPointToSmalMap((double)r.pos_x, (double)r.pos_y);
            selected_point.X = regionSP.X;
            selected_point.Y = regionSP.Y;
            selected_region_name = r.region2_name;
            selected_region_id = r.region2_id;


            //select 된 위치로 flag를 이동
            Point tempPosition = region_mgr.changePosition_DefaultPointToSmalMap((double)r.pos_x, (double)r.pos_y);
            Thickness tempMargin =
                new Thickness(tempPosition.X + _gridMap.Margin.Left - point_flag.Width / 2,
                              tempPosition.Y + _gridMap.Margin.Top - point_flag.Height / 2,
                              0, 0);
            point_flag.Margin = tempMargin;

            //정보창에 선택된 region의 이름,위치, 이미지 파일 이름을 표시한다
            _txtRegionName.Text = r.region2_name;
            _lblPickUpRegion.Text = string.Format("({0},{1})", r.pos_x, r.pos_y);
            //string tmp_str = region_mgr.getImageFileName((int)r.region2_image_id);
            //if (tmp_str != null)
            //{
            //    _lblAddRegionMap.Text = tmp_str;
            //    selected_image_file_name = tmp_str;
            //}
            //else
            //    Console.WriteLine("not match region id!!!");
        }
        #endregion


        #region 지역정보 신규, 저장, 삭제 등의 이벤트

        //'신규' 버튼: 현재 선택된 정보들을 모두 지운다
        private void _btnNew_Click(object sender, RoutedEventArgs e)
        {
            resetRegionInfoPanel();

        }
        //'삭제' 버튼: 선택된 region1Data를 삭제 한다
        private void _btnDelRegion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                site r2 = (site)g.site_list.First(at => at.region2_id == selected_region_id);
                if (r2 != null) // 하위가 있으면 삭제 불가 
                    return;
            }
            catch { }
            //DB에서 해당 region1 데이터 삭제
            region_mgr.delRegion2DataToDb(selected_region_id);
            
            //page1에 region flag를 삭제 한다
            this.delParentFlagEvent(selected_region_id);

            resetRegionInfoPanel();
            
        }

        //'저장' 버튼: 작성한 지역정보를 추가 또는 수정한다
        private async void _btnSaveRegion_Click(object sender, RoutedEventArgs e)
        {
            // 신규 지역 등록인 경우
            if (selected_region_id == -1)
            {
                //선택된 위치를 확인한다
                if(selected_region_name == null)
                {
                    MessageBox.Show(g.tr_get("M1_World_NoName"));
                    return;
                }
                else if (selected_point == new Point(0,0))
                {
                    MessageBox.Show(g.tr_get("M1_World_NoLocation"));
                    return;
                }
                
                //동일한 region이 존재하는지 확인한다
                foreach(var at in g.region2_list)
                {
                    if (at.region2_name == selected_region_name)
                    {
                        MessageBox.Show(g.tr_get("M1_World_DuplicateName"));
                        return;
                    }
                }

                //region1정보를 DB에 추가한다
                var t4 = region_mgr.addRegion2ToDb(cur_region1_id, selected_region_name,  selected_point);
                region2 r = await t4;

                // 부모 페이지의 월드맵에 해당 지점을 표시 한다
                this.addParentFlagEvent(r);
                    
                resetRegionInfoPanel();
            
            }
            // 기존에 있는 지역을 수정하는 경우
            else
            {
                //이름과 포지션이 수정된 경우 동작
                if ((changed_region_pos == true)
                    || (changed_region_name == true))
                {
                    location l = g.location_list.Find(at => (at.region2_id == selected_region_id) && (at.site_id == null));
                    if (l == null) return;
                    
                    //db변경
                    var t5 = region_mgr.modifyRegion2ToDb(selected_region_id, _txtRegionName.Text, selected_point);
                    region2 r = await t5;

                    //전역변수 변경
                    region2 g_r = g.region2_list.Find(at => at.region2_id == r.region2_id);
                    if (g_r == null) return;
                    g_r.region2_name = r.region2_name;
                    g_r.pos_x = r.pos_x;
                    g_r.pos_y = r.pos_y;
                    g_r.remarks = r.remarks;

                    //전역 ast_vm_dic 변경
                    if (g.location_ast_vm_dic.ContainsKey(l.location_id))
                    {
                        AssetTreeVM region2_ast_vm = g.location_ast_vm_dic[l.location_id];
                        region2_ast_vm.disp_name = r.region2_name;
                    }

                    //관련 데이터 변경
                    Boolean ret = await g.left_tree_handler.editRegion2(selected_region_id, r.region2_name);

                    // region Map에 해당 포인트의 flag를 변경된 포인트로 이동한다
                    this.moveParentFlagEvent(r);
                }
                resetRegionInfoPanel();
            }
            
        } 


        #endregion


        #region 윈도우, 탭 이벤트

        // 윈도우 닫기 버튼클릭
        private void _btnCloseRegion_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void _gridTab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            region_mgr.setMapSize(_imgMap.ActualWidth, _imgMap.ActualHeight);
        } 
        #endregion


        #region 기타 메소드
        // UI와 선택된 모든 정보의 초기화
        private void resetRegionInfoPanel()
        {
            //지도상의 포인트를 지워준다
            Thickness tempMargin = new Thickness(-100, -100, 0, 0);
            point_flag.Margin = tempMargin;

            _lblPickUpRegion.Text = null;

            //선택된 지역정보 이름을 지운다
            _txtRegionName.Text = null;
            selected_region_name = null;

            //_lblAddRegionMap.Text = null;


            changed_region_name = false;
            changed_region_pos = false;

            //선택된 ID를 -1로 만든다
            selected_region_id = -1;

            selected_point = new Point(0, 0);

            //지역정보 ListView를 다시 그려준다
            _lvRegionInfo.ItemsSource = null;
            _lvRegionInfo.ItemsSource = g.region2_list.Where(at => at.region1_id == cur_region1_id);
          
        } 
        #endregion
    }
}
