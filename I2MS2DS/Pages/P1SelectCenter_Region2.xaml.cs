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
using WebApi.Models;
using I2MS2.Models;
using I2MS2.Windows;
using System.Windows.Threading;
using System.IO;

namespace I2MS2.Pages
{
    // 사이트 선택 처리 
    /// <summary>
    /// P1SelectCenter_Region2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class P1SelectCenter_Region2 : Page
    {
        region2 cur_region2;
        List<site_use_image> site_use_image_list = new List<site_use_image>();


        //public delegate void selectSiteEventHandler(int site_id);
        //public event selectSiteEventHandler selectSiteEvent;

        public P1SelectCenter_Region2(region2 r2)
        {
            InitializeComponent();

            cur_region2 = r2;

            initSiteListDisplay();
            
        }
        // 사이트 리스트 초기화 
        private void initSiteListDisplay()
        {
            foreach (var at in g.site_list.Where(at => at.region2_id == cur_region2.region2_id))
            {
                try
                {
                    sp_list_image_Result sp_img = (sp_list_image_Result)g.sp_image_list.Find(si => si.image_id == at.site_image_id);
                    //Console.WriteLine("site={0}, image={1}", at.site_name, sp_img.file_name);

                    String img_file;
                    if (sp_img != null)
                        img_file = sp_img.file_name;
                    else
                        img_file = g.NULL_FILE_PATH;
                    
                    site_use_image sui = makeSiteList(at, img_file);
                   
                    int number = site_use_image_list.Count + 1;
                    sui.number = number; 
                    site_use_image_list.Add(sui);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERR:{0}", ex.Message);
                }

            }
            _lvSiteList.ItemsSource = site_use_image_list;
        }

        // 사이트 이미지 만들기 처리 
        private site_use_image makeSiteList(site st, String file_name)
        {
            site_use_image stui = new site_use_image();

            stui.site_id = st.site_id;
            stui.site_name = st.site_name;
            stui.region2_id = st.region2_id;
            stui.remarks = st.remarks;

            stui.site_image_id = st.site_image_id;
            stui.site_image_file_name = file_name;
            String img_path = string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH, g.SERVER_SUB_DIR_SITE, file_name);

            if (File.Exists(img_path))
                stui.site_image_file_path = img_path;
            else
                stui.site_image_file_path = g.NULL_FILE_PATH;
            //Console.WriteLine("{0}", sui.site_image_file_path);


            return stui;
        }


        private void _pageP1SelectCenter_Region2_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void _pageP1SelectCenter_Region2_SizeChanged(object sender, SizeChangedEventArgs e)
        {
           // Console.WriteLine("_lvSiteList size({0},{1})", _lvSiteList.ActualWidth, _lvSiteList.ActualHeight);
            //_lvSiteList.ItemsPanel.Template.
        }
        // 사이트 생성 
        private void _btnManageSite_Click(object sender, RoutedEventArgs e)
        {
            SiteManageWindow siteMWindow = new SiteManageWindow(site_use_image_list, cur_region2.region2_id);

            siteMWindow.reloadParentListView += new SiteManageWindow.ReloadParentListViewHandler(reloadSiteList);
            siteMWindow.cleanParentListView += new SiteManageWindow.cleanParentListViewHandler(cleanSiteList);
            siteMWindow.ShowDialog();
        }
        // 뒤로 처리 
        private void _btnBacktoPage_Click(object sender, RoutedEventArgs e)
        {
            region1 r1 = (region1)g.region1_list.First(at => at.region1_id == cur_region2.region1_id);
            P1SelectCenter_Region1 p1_region1 = new P1SelectCenter_Region1(r1);
            this.NavigationService.Navigate(p1_region1);

        }
        // 사이트 선택 변경  
        private void _lvSiteList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            site_use_image st;
            //site st = (site)e.Source;
            if(_lvSiteList.SelectedItem != null)
                st = (site_use_image)_lvSiteList.SelectedItem;
        //    Console.WriteLine("select = {0}", st.site_name);
        }
        // 다시 그리기 처리 
        private void reloadSiteList(object o)
        {
            _lvSiteList.ItemsSource = null;
            _lvSiteList.ItemsSource = site_use_image_list;
        }
        // 리스트 비우기 처리 
        private void cleanSiteList(object o)
        {
        //    string file_name = (string)o;
            //_lvSiteList.ItemsSource = null;
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                     new Action(() => this._lvSiteList.ItemsSource = null));
        }
        // 사이트 선택 완료 
        private void _lvSiteList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            site_use_image sel_site = (site_use_image)_lvSiteList.SelectedItem;
            if (sel_site == null) return;
            Console.WriteLine("type = {0}",e.Source.GetType());
            g.selected_site_id = sel_site.site_id;
            //selectSiteEvent(sel_site.site_id);
            g.main_window.selectSiteEvent(sel_site.site_id);
        }
       
    }
}
