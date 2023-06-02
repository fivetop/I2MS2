
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebApi.Models;
using I2MS2.Animation;
using I2MS2.Models;
using I2MS2.UserControls;
using I2MS2.Windows;
using I2MS2.Library;

namespace I2MS2.Pages
{
    // 세계 지도에서 선택 처리 
    /// <summary>
    /// Page1_1_SelectCenterWorld.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class P1SelectCenter_World : Page
    {
        //페이지가 로딩된 상태인지 확인 하는 플래그
        Boolean is_page_loaded = false;


        P1SelectCenter p1_mgr;

        public delegate void selectSiteEventHandler(int site_id);
        public event selectSiteEventHandler selectSiteEventToMain;

        //클래스 초기화
        public P1SelectCenter_World()
        {
            InitializeComponent();
            p1_mgr = new P1SelectCenter();
        }

        #region 페이지의 load, resize시 이벤트
        //page load시 호출되는 이벤트 : 상위 프래임 사이즈를 받고 그에 맟추어 flag를 그려준다
        private void _pageP1SelectCenter_World_Loaded(object sender, RoutedEventArgs e)
        {
            p1_mgr.setMapSize(_imgMap.ActualWidth, _imgMap.ActualHeight);

            //region 데이터로 flag와 flagText 를 지도에 추가한다
            foreach (var at in g.region1_list)
            {
                addRegionFlagAndFlagText(at);
            }
            is_page_loaded = true;
        }

        //page 사이즈가 조정된 경우에 호출되는 이벤트
        private void _pageP1SelectCenter_World_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //페이지가 로드되기 전에는 flag가 없으므로 컨트롤 안됨
            if (is_page_loaded == true)
            {
                p1_mgr.setMapSize(_imgMap.ActualWidth, _imgMap.ActualHeight);

                //모든 플래그와 텍스트를 사이즈 이동에 따라 이동시킨다
                foreach (var at in g.region1_list)
                {
                    moveRegionFlagAndFlagText(at);
                }

                //Console.WriteLine("mapSize({0},{1})", _imgMap.ActualWidth, _imgMap.ActualHeight);
                //Console.WriteLine("mapGrid({0},{1})", _gridMap.ActualWidth, _gridMap.ActualHeight);
                //Console.WriteLine("default({0},{1})", g.default_map_size.Width, g.default_map_size.Height);
                //Console.WriteLine("setting mapSize({0},{1})", p1_mgr.map_size.Width, p1_mgr.map_size.Height);

            }
        } 
        #endregion


        #region flag와 flagText를 만들고 추가 삭제, 위치 변경하는 부분
        //지도위에 region1의 flag와 flagText를 추가한다
        public void addRegionFlagAndFlagText(region1 r1)
        {
            regionFlagControl tmp_flag = p1_mgr.addRegionFlag((int)r1.region1_id, (int)r1.pos_x, (int)r1.pos_y);
            //MouseLeftButtonDown시에 이벤트를 설정한다
            tmp_flag.customMouseEnterEvent += new regionFlagControl.CustomMouseEnterHandler(flagMouseEnterEvent);
            tmp_flag.customMouseLeaveEvent += new regionFlagControl.CustomMouseLeaveHandler(flagMouseLeaveEvent);
            tmp_flag.MouseLeftButtonDown += RegionSelectEvent;
            _gridMap.Children.Add(tmp_flag);

            regionFlagTextControl region_flag_text = p1_mgr.addRegionFlagText(r1.region1_id, r1.region1_name, (int)r1.pos_x, (int)r1.pos_y);
            _gridMap.Children.Add(region_flag_text);
        }


        //지도위에 region1의 flag와 flagText를 삭제한다
        public void delRegionFlagAndFlagText(int region_id)
        {
            regionFlagControl tmp_flag = p1_mgr.findFlag(region_id);
            _gridMap.Children.Remove(tmp_flag);
            p1_mgr.delFlag(region_id);

            regionFlagTextControl tmp_flag_text = p1_mgr.findFlagText(region_id);
            _gridMap.Children.Remove(tmp_flag_text);
            p1_mgr.delFlagText(region_id);
            
        }


        //지도위에 region1 flag과 이름의 위치를 변경한다
        public void moveRegionFlagAndFlagText(region1 r)
        {
            p1_mgr.changeFlagMargin(r.region1_id,(int)r.pos_x, (int)r.pos_y);
            p1_mgr.changeFlagTextMargin(r.region1_id, (int)r.pos_x, (int)r.pos_y, r.region1_name);
        } 
        #endregion


        #region 버튼등의 click 이벤트
        //지역 추가 버튼을 클릭
        private void _btnAddRegion_Click(object sender, RoutedEventArgs e)
        {
            //지역추가 창을 실행한다
            Region1ManageWindow adr1_window = new Region1ManageWindow(p1_mgr.map_size);
            adr1_window.Owner = Application.Current.MainWindow;

            //addRegion1Window에 이벤트들을 추가한다
            adr1_window.addParentFlagEvent += new Region1ManageWindow.AddParentFlagHander(addFlagByChild);
            adr1_window.delParentFlagEvent += new Region1ManageWindow.DelParentFlagHander(delFlagByChild);
            adr1_window.moveParentFlagEvent += new Region1ManageWindow.MoveParentFlagHander(moveFlagByChild);
            adr1_window.ShowDialog();

        }

        //지역이 선택되었을때 실행되는 이벤트
        private void RegionSelectEvent(object sender, RoutedEventArgs e)
        {
            regionFlagControl flag = (regionFlagControl)e.Source;
            if (flag == null) return;
            region1 r = (region1)g.region1_list.First(at => at.region1_id == flag.id);
            if (r == null) return;
            P1SelectCenter_Region1 p1_region1 = new P1SelectCenter_Region1(r);
            if (p1_region1 == null) return;
            //p1_region1.selectSiteEventToWorld += new P1SelectCenter_Region1.selectSiteEventHandler(selectSiteEventFromR1);
            try
            {
                this.NavigationService.Navigate(p1_region1);
            }
            catch (Exception) {
                return;
            }
        }

        #endregion


        #region regionFlag에 추가되는 이벤트

        public void flagMouseEnterEvent(string name)
        {
            p1_mgr.FlagTextScaleUp(name);
        }

        public void flagMouseLeaveEvent(string name)
        {
            p1_mgr.FlagTextScaleDown(name);
        } 
        #endregion


        #region AddRegion1Window에 추가되는 이벤트
        // 플래그 컨트롤에서 올라오는 이벤트 처리 -> 화면 갱신용
        //AddRegion1Window 에서 flag를 추가했을때 호출되는 이벤트
        private void addFlagByChild(object o)
        {
            region1 r1 = (region1)o;
            addRegionFlagAndFlagText(r1);
        }

        //AddRegion1Window 에서 flag를 삭제했을때 호출되는 이벤트
        private void delFlagByChild(object o)
        {
            int r_id = (int)o;
            delRegionFlagAndFlagText(r_id);
        }

        //AddRegion1Window 에서 flag를 이동했을때 호출되는 이벤트
        private void moveFlagByChild(object o)
        {
            region1 r1 = (region1)o;
            moveRegionFlagAndFlagText(r1);
        }

        #endregion

        private void _gridMap_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point tp = e.GetPosition(_gridMap);
            Console.WriteLine("{0},{1}", tp.X, tp.Y);
        }



        public void selectSiteEventFromR1(int id)
        {
            if (selectSiteEventToMain == null)
                return;
            selectSiteEventToMain(id);
        }


    }
}
