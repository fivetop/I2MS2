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
    // 지역 선택 처리 
    /// <summary>
    /// Page1_1_SelectCenterWorld.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class P1SelectCenter_Region1 : Page
    {
        //페이지가 로딩된 상태인지 확인 하는 플래그
        Boolean is_page_loaded = false;

        P1SelectCenter p1_mgr;
        region1 cur_region;


        //public delegate void selectSiteEventHandler(int site_id);
        //public event selectSiteEventHandler selectSiteEventToWorld;

        //클래스 초기화
        public P1SelectCenter_Region1(region1 r)
        {
            InitializeComponent();
            p1_mgr = new P1SelectCenter();
            cur_region = r;

            // GS_DEL
            if (g.login_user_id != 90001)
            {
                _btnAddRegion.Visibility = Visibility.Hidden;
            }

            //foreach(var at in g.region2_list)
            //{
            //    Console.WriteLine("{0}-{1}:{2}", at.region1_id, at.region2_id, at.region2_name);
            //}
            sp_list_image_Result sp_image = null;

            try
            { 
                sp_image = (sp_list_image_Result)g.sp_image_list.First(at => at.image_id == r.region1_image_id);
            }
            catch(Exception e)
            {
                Console.WriteLine("{0}" , e.Data.ToString());
                return;
            }
            string img_string = string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH, "map", sp_image.file_name);
            try
            {
                _imgMap.Source = new BitmapImage(new Uri(img_string));
            }
            catch { }
        }                                                                                         

        #region 페이지의 load, resize시 이벤트
        //page load시 호출되는 이벤트 : 상위 프래임 사이즈를 받고 그에 맟추어 flag를 그려준다
        private void _pageP1SelectCenter_Region1_Loaded(object sender, RoutedEventArgs e)
        {
            p1_mgr.setMapSize(_imgMap.ActualWidth, _imgMap.ActualHeight);

            //region 데이터로 flag와 flagText 를 지도에 추가한다
            foreach (var at in g.region2_list)
            {
                if(at.region1_id == cur_region.region1_id)
                    addRegionFlagAndFlagText(at);
            }
            is_page_loaded = true;
        }

        //page 사이즈가 조정된 경우에 호출되는 이벤트
        private void _pageP1SelectCenter_Region1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //페이지가 로드되기 전에는 flag가 없으므로 컨트롤 안됨
            if (is_page_loaded == true)
            {
                p1_mgr.setMapSize(_imgMap.ActualWidth, _imgMap.ActualHeight);

                //모든 플래그와 텍스트를 사이즈 이동에 따라 이동시킨다
                foreach (var at in g.region2_list)
                {
                    if (at.region1_id == cur_region.region1_id)
                        moveRegionFlagAndFlagText(at);
                }
            }
        } 
        #endregion


        #region flag와 flagText를 만들고 추가 삭제, 위치 변경하는 부분
        //지도위에 region1의 flag와 flagText를 추가한다
        public void addRegionFlagAndFlagText(region2 r)
        {
            regionFlagControl tmp_flag = p1_mgr.addRegionFlag((int)r.region2_id, (int)r.pos_x, (int)r.pos_y);
            //MouseLeftButtonDown시에 이벤트를 설정한다
            tmp_flag.customMouseEnterEvent += new regionFlagControl.CustomMouseEnterHandler(flagMouseEnterEvent);
            tmp_flag.customMouseLeaveEvent += new regionFlagControl.CustomMouseLeaveHandler(flagMouseLeaveEvent);
            tmp_flag.MouseLeftButtonDown += RegionSelectEvent;
            _gridMap.Children.Add(tmp_flag);

            regionFlagTextControl region_flag_text = p1_mgr.addRegionFlagText(r.region2_id, r.region2_name, (int)r.pos_x, (int)r.pos_y);
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
        public void moveRegionFlagAndFlagText(region2 r)
        {
            p1_mgr.changeFlagMargin(r.region2_id,(int)r.pos_x, (int)r.pos_y);
            p1_mgr.changeFlagTextMargin(r.region2_id, (int)r.pos_x, (int)r.pos_y, r.region2_name);
        } 
        #endregion


        #region 버튼등의 click 이벤트
        //지역 추가 버튼을 클릭
        private void _btnAddRegion_Click(object sender, RoutedEventArgs e)
        {
            sp_list_image_Result sp_image = (sp_list_image_Result)g.sp_image_list.First(at => at.image_id == cur_region.region1_image_id);
            string img_string = string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH, "map", sp_image.file_name);

            //지역추가 창을 실행한다
            Region2ManageWindow adr1_window = new Region2ManageWindow(cur_region.region1_id, img_string, p1_mgr.map_size);
            adr1_window.Owner = Application.Current.MainWindow;

            //addRegion2Window에 이벤트들을 추가한다
            adr1_window.addParentFlagEvent += new Region2ManageWindow.AddParentFlagHander(addFlagByChild);
            adr1_window.delParentFlagEvent += new Region2ManageWindow.DelParentFlagHander(delFlagByChild);
            adr1_window.moveParentFlagEvent += new Region2ManageWindow.MoveParentFlagHander(moveFlagByChild);
            adr1_window.ShowDialog();
        }

        //지역이 선택되었을때 실행되는 이벤트
        private void RegionSelectEvent(object sender, RoutedEventArgs e)
        {
            regionFlagControl flag = (regionFlagControl)e.Source;
            //Console.WriteLine("Select region id = {0}", flag.id);

            //go to region2 page!!!
            region2 r = (region2)g.region2_list.First(at => at.region2_id == flag.id);
            
            P1SelectCenter_Region2 p1_region2 = new P1SelectCenter_Region2(r);
            //p1_region2.selectSiteEvent += new P1SelectCenter_Region2.selectSiteEventHandler(selectSiteEvent);
            if (NavigationService != null)
                this.NavigationService.Navigate(p1_region2);

        }

        //public void selectSiteEvent(int id)
        //{
        ////    selectSiteEventToWorld(id);
        ////    g.main_window.selectSiteEvent(id);
        //}
        

        //이전페이지로 가는 이벤트
        private void _btnBacktoPage_Click(object sender, RoutedEventArgs e)
        {
            P1SelectCenter_World p1_world = new P1SelectCenter_World();
            this.NavigationService.Navigate(p1_world);
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

        //AddRegion1Window 에서 flag를 추가했을때 호출되는 이벤트
        private void addFlagByChild(object o)
        {
            region2 r = (region2)o;
            addRegionFlagAndFlagText(r);
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
            region2 r = (region2)o;
            moveRegionFlagAndFlagText(r);
        }

        #endregion
    }
}
