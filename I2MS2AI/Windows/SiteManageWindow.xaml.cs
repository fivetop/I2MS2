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
using I2MS2.Models;
using System.IO;
using Microsoft.Win32;
using WebApi.Models;
using I2MS2.Library;

namespace I2MS2.Windows
{
    // 지역에 사이트 등록 관리 
    /// <summary>
    /// SiteManageWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public partial class SiteManageWindow : Window
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
        List<site_use_image> site_use_image_list = new List<site_use_image>();

        string selected_site_name;
        Boolean changed_name;


        string selected_site_rmarks;
        Boolean changed_remarks;

        string selected_image_file_path;
        string selected_image_file_name;
        Boolean changed_image = false;

        int cur_region2_id = -1;

        int selected_site_id = -1;


        ImageManage img_mgr;
        SiteManage st_mgr;

        site_use_image _left_item = null;

        //부모 클래스 내부의 이벤트 호출 연결 고리
        public delegate void ReloadParentListViewHandler(object obj);
        public event ReloadParentListViewHandler reloadParentListView;

        public delegate void cleanParentListViewHandler(object obj);
        public event cleanParentListViewHandler cleanParentListView;

        #region 초기화
        public SiteManageWindow(List<site_use_image> _st_list, int region2_id)
        {
            InitializeComponent();
            site_use_image_list = _st_list;
            _lvLeft.ItemsSource = site_use_image_list;


            cur_region2_id = region2_id;
            img_mgr = new ImageManage();
            st_mgr = new SiteManage();

            if(g.login_user_id != 90001)
            {
                _new = false;
                _edit = false;
                _delete = false;
                _save = false;
                _cancel = false;
                _new_flag = false;
            }
            
        }

        #endregion
 
        #region 오른쪽 아래 수정창의 이벤트
        private void _btnAddSiteImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open_file_dialog = new OpenFileDialog();
            open_file_dialog.Filter = "Image Files |*.jpeg;*.png;*.jpg;*.gif";

            Nullable<Boolean> result = open_file_dialog.ShowDialog();
            if (result == true)
            {
                selected_image_file_path = open_file_dialog.FileName;
                selected_image_file_name = open_file_dialog.SafeFileName;
                _lblAddSiteImage.Text = string.Format("{0}", open_file_dialog.FileName);
                img_mgr.drawImage(_imgSite, open_file_dialog.FileName);
                changed_image = true;
            }
        }

        private void _txtSiteName_TextChanged(object sender, TextChangedEventArgs e)
        {
            selected_site_name = _txtSiteName.Text;
            changed_name = true;
        }

        private void _txtSiteRemark_TextChanged(object sender, TextChangedEventArgs e)
        {
            //selected_site_rmarks = _txtSiteRemark.Text;
            changed_remarks = true;
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
            resetSiteInfoPanel();
            _txtSiteName.Text = ""; // 신규일 경우만 초기화 처리 

            _new = false;
            _delete = false;
            _edit = false;
            _save = true;
            _cancel = true;

            _txtSiteName.Focus();
            CommandManager.InvalidateRequerySuggested();

        }

        private void _cmdEdit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                if (_txtSiteName.Text == "")
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

            _txtSiteName.Focus();
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
            resetSiteInfoPanel();
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
            try
            {
                if (_txtSiteName.Text == "")
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
            e.CanExecute = _save;
        }
        // 저장 처리 
        private async void _cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!await save())
                return;
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
                _lvLeft.ItemsSource =site_use_image_list;
                _lvLeft.SelectedIndex = idx - 1;
            }

            // 추가 또는 수정 시
            if (idx == -1)
            {
                int id = 0;
                if (_left_item != null)
                    id = _left_item.user_id;
                _lvLeft.ItemsSource = null;
                _lvLeft.ItemsSource = site_use_image_list;
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
            _txtSiteName.Text = _left_item.site_name;
            _txtSiteRemark.Text = _left_item.remarks;
            selected_site_id = _left_item.site_id;
            selected_image_file_name = _left_item.site_image_file_name;
            _lblAddSiteImage.Text = _left_item.site_image_file_name;

            if (File.Exists(_left_item.site_image_file_path) == true)
            {
                //_imgSite.Source = new BitmapImage(new Uri(stui.site_image_file_path));
                img_mgr.drawImage(_imgSite, _left_item.site_image_file_path);

            }
        }


        // 신규 처리시 기존 내용 없애는 용도 
        private void clearControlLeft()
        {
            _txtSiteName.Clear();
            _lvLeft.SelectedIndex = -1;
            _txtSiteRemark.Text = null;
            _imgSite.Source = null;
            _lblAddSiteImage.Text = null;
            _lvLeft.SelectedIndex = -1;
        }
        // enable control 로직
        private void enableControlLeft(bool flag)
        {
            _txtSiteName.IsEnabled = flag;
            _txtSiteRemark.IsEnabled = flag;
            _btnAddSiteImage.IsEnabled = flag;
        }
        #endregion

        #region 리스트 뷰에서 아이템 선택
        private void _lvLeft_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (g.login_user_id != 90001)
            {
                _new = false;
                _edit = false;
                _delete = false;
                _save = false;
                _cancel = false;
                _new_flag = false;

                return;
            }

            _new = true;
            _delete = true;
            _edit = true;
            _save = false;
            _cancel = false;

            _left_item = (site_use_image)_lvLeft.SelectedItem;

            if (_left_item == null)
                return;
            dispLeft();
            enableControlLeft(false);

        }
        #endregion

        //'저장' 버튼: 작성한 사이트정보를 추가 또는 수정한다
        private async Task<bool> save()
        {
            string tregion_name = _txtSiteName.Text.Trim();

            if (tregion_name == "") return false;

            //새로 등록하는 경우
            if (_new_flag)
            {
                if (!(changed_image && changed_name))
                {
                    MessageBox.Show(g.tr_get("C_lackOfInformation"));
                    return false;
                }

                //동일한 사이트가 존재하는지 확인한다
                foreach (var at in site_use_image_list)
                {
                    if (at.site_name == selected_site_name)
                    {
                        MessageBox.Show(g.tr_get("M1_World_DuplicateName"));
                        return false;
                    }
                }

                // 이미지 업로드
                var t1 = img_mgr.addImageToServer(selected_image_file_path, "site", selected_image_file_name);
                string server_file_name = await t1;

                if (server_file_name == null)
                {
                    MessageBox.Show(g.tr_get("M1_World_ChImageFail"));
                    return false;
                }

                //이미지 정보를 DB에 추가한다
                var t2 = img_mgr.addImageToDb(server_file_name, "site", selected_site_name);
                int img_id = await t2;

                //사이트 정보를 DB에 추가 한다
                var t3 = st_mgr.addSiteToDb(selected_site_name, selected_site_rmarks, img_id, cur_region2_id);
                site st = await t3;

                //사이트 정보를 클래스 변수에 추가한다
                site_use_image sti = makeSiteClassVar(st.site_id, selected_site_name, selected_site_rmarks, server_file_name, img_id, g.login_user_id, cur_region2_id);
                int number = site_use_image_list.Count + 1;
                sti.number = number;
                site_use_image_list.Add(sti);

                //사이트 정보를 전역 리스트에 추가
                //g.site_list.Add(st);
                        
                //관련 데이터에 추가
                Boolean ret = await g.left_tree_handler.addSite(st.site_id);

                location l = g.location_list.Find(at => (at.site_id == st.site_id) && (at.building_id == null));
                if (l == null) return false;

                //전역 ast vm dic에 추가
                if (!g.location_ast_vm_dic.ContainsKey(l.location_id))
                {
                    AssetTreeViewManager astMgr = new AssetTreeViewManager();
                    asset_tree astt = g.asset_tree_list.Find(at => at.location_id == l.location_id);
                    if(astt!=null)
                    {
                        AssetTreeVM site_ast_vm = astMgr.makeAssetTreeVM(astt);
                        location r2_location = g.location_list.Find(at => (at.region2_id == cur_region2_id)&&(at.site_id==null));
                        if (r2_location == null) return false;
                                    
                        if(g.location_ast_vm_dic.ContainsKey(r2_location.location_id))
                        {
                            AssetTreeVM r2_ast_vm = g.location_ast_vm_dic[r2_location.location_id];
                            r2_ast_vm.child_list.Add(site_ast_vm);
                        }
                    }
                            
                }
                resetSiteInfoPanel();
                refreshLeft(-1);
                MessageBox.Show(g.tr_get("M1_SiteMgrWindow_SiteRegisted"));
            }
            //수정하는 경우
            else
            {
                string server_file_name;

                selected_site_id = _left_item.site_id;


                //클래스 변수 변경
                site_use_image stui = site_use_image_list.First(at => at.site_id == selected_site_id);
                
                
                
                //이미지가 수정되었으면 동작 
                if (changed_image == true)
                {
                    //서버에 기존 이미지 파일 삭제
                    var t1 = img_mgr.DelImageToServer("site",selected_image_file_name);
                    Boolean result = await t1;

                    //서버에 새로운 이미지 파일 삽입
                    var t2 = img_mgr.addImageToServer(selected_image_file_path, "site", selected_image_file_name);
                    server_file_name = await t2;
                    stui.site_image_file_path = string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH, "site", server_file_name);
                    stui.site_image_file_name = server_file_name;
            
                    if(server_file_name != null)
                    {
                        site st = (site)g.site_list.First(at => at.site_id == selected_site_id);
                        var t3 = img_mgr.modifyImageToDb((int)st.site_image_id, server_file_name);
                        //int it = await t3;
                    }
                    else
                    {
                        MessageBox.Show(g.tr_get("M1_World_ChImageFail"));
                        return false;
                    }
                }

                //이름과 remark가 수정된 경우 동작
                if ((changed_remarks == true)
                    || (changed_name == true))
                {
                    location l = g.location_list.Find(at => (at.site_id == selected_site_id) && (at.building_id == null));
                    if (l == null) return false;

                    //DB에서 변경
                    stui.site_name = selected_site_name;
                    var t5 = st_mgr.modifySiteToDb(selected_site_id, selected_site_name, selected_site_rmarks, changed_name);
                    site st = await t5;
                   
                    //전역변수 에서 변경
                    site g_st = g.site_list.Find(at => at.site_id == st.site_id);
                    g_st.site_name = st.site_name;
                    g_st.site_image_id = st.site_image_id;
                    g_st.remarks = st.remarks;
                    
                    //전역 ast vm dic에서 변경
                    if(g.location_ast_vm_dic.ContainsKey(l.location_id))
                    {
                        AssetTreeVM site_ast_vm = g.location_ast_vm_dic[l.location_id];
                        site_ast_vm.disp_name =  st.site_name;
                    }

                    //현재 창의 UI변경
                    stui.site_name = st.site_name;
                    stui.remarks = st.remarks;

                    //관련 데이터 변경
                    Boolean ret = await g.left_tree_handler.editSite(st.site_id, st.site_name);

                    // 메모리 디비 업데이트 처리 완료

                }
                resetSiteInfoPanel();
                int idx = _lvLeft.SelectedIndex;
                refreshLeft(idx + 1);

                MessageBox.Show(g.tr_get("M1_SiteMgrWindow_SiteModifyed"));
            }
            return true;
        }

        // 사이트 삭제의경우 위험 -> 하위 자료가 있으면 삭제안되게 처리 필요 // romee
        private async Task<bool> delete()
        {
            string tname = _txtSiteName.Text.Trim();

            if (tname == "") return false;

            if (g.site_list.Count == 1)
            {
                MessageBox.Show(g.tr_get("C_Error_6"));
                return false;
            }

            if (selected_site_name == null)
            {
                MessageBox.Show(g.tr_get("C_Error_Cant_Delete"));
            }

            // 해당사이트를 가져와서 하위 로케이션이 있으면 리턴한다. 
            try { 
                site st1 = (site)g.site_list.First(at => at.site_name == selected_site_name);
                if (st1 == null) return false;
                building bd = g.building_list.Find(at => at.site_id == st1.site_id);
                if (bd != null)
                {
                    MessageBox.Show(g.tr_get("C_Error_Cant_Delete"));
                    return false;
                } 
            }
            catch
            {
            }

            // 서버의 파일을 삭제 한다
            Console.WriteLine("{0}", selected_image_file_name);
            
            // 클라이언트에서 파일 삭제에 앞서 해당 파일을 UI 리소스 에서 제외해야 한다
            // cleanParentListView(selected_image_file_name);  romee 
            _imgSite.Source = null;
            

            var t2 = img_mgr.DelImageToServer("site", selected_image_file_name);
            Boolean result = await t2;
            if (result == false)
                Console.WriteLine("Warning:delete image fail!");

            site st = (site)g.site_list.First(at => at.site_id == selected_site_id);

            //DB이미지 리스트에서 해당 이미지 정보 삭제
            //            region_mgr.delImageToDb((int)r1.region1_image_id, selected_image_file_name);
            img_mgr.delImageToDb((int)st.site_image_id, "site", selected_image_file_name);

            //DB에서 해당 region1 데이터 삭제
            Boolean r = await st_mgr.delSiteToDb(selected_site_id);

            //클래스 변수에서 site 삭제
            site_use_image stui = site_use_image_list.First(at => at.site_id == selected_site_id);
            site_use_image_list.Remove(stui);

            return true;
        }

        private void resetSiteInfoPanel()
        {
            selected_site_name = null;
            selected_site_rmarks = null;
            selected_image_file_path = null;
            selected_image_file_name = null;
            changed_name = false;
            changed_remarks = false;
            changed_image = false;
            
            // 상위 페이지 ListView를 리셋
            reloadParentListView(selected_site_id);
            selected_site_id = -1;
        }

        private site_use_image makeSiteClassVar(int site_id, string site_name ,string remarks, 
                                                string server_file_name, int image_id, 
                                                int user_id,int region2_id )
        {
            site_use_image stui = new site_use_image();

            stui.site_id = site_id;
            stui.site_name = site_name;
            stui.remarks = remarks;
            stui.site_image_id = image_id;
            stui.user_id = user_id;
            stui.region2_id = region2_id;
            stui.site_image_file_name = server_file_name;
            stui.site_image_file_path = string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH, g.SERVER_SUB_DIR_SITE, server_file_name);


            return stui;
        }

        private site makeSiteVar(string site_name, string remarks,
                                                string server_file_name, int image_id,
                                                int user_id, int region2_id)
        {
            site st = new site();

            st.site_name = site_name;
            st.remarks = remarks;
            st.site_image_id = image_id;
            st.user_id = user_id;
            st.region2_id = region2_id;

            return st;
        }

        #region 윈도우, 탭 이벤트

        private void _gridTab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void _btnCloseSite_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion

    }
}
