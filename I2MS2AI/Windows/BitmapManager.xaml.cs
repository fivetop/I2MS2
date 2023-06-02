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

using WebApi.Models;
using I2MS2.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using I2MS2.Library;
using System.IO;
using System.Threading;
using System.Windows.Threading;
namespace I2MS2.Windows
{
    // 비트맵 이미지 관리 -> 메인 호출 
    // 프로그램내의 비트맵 관리 처리 
    #region Data class
    public partial class img_folder : INotifyPropertyChanged
    {
        public img_folder()
        {
            child_list = new ObservableCollection<sp_img_vm>();
        }

        public string type_name { get; set; }
        public string folder_name { get; set; }

        public ObservableCollection<sp_img_vm> child_list { get; set; }

        public bool force_changed
        {
            get { return true; }
            set { NotifyPropertyChanged(""); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }


    public partial class sp_img_vm : INotifyPropertyChanged
    {
        public int image_id { get; set; }
        public string image_name { get; set; }
        public int image_type_id { get; set; }
        public string image_type_name { get; set; }


        public string file_name { get; set; }
        public string folder_name { get; set; }
        public string client_file_path { get; set; }
        public ImageSource image_source { get; set; }

        public string deletable { get; set; }
        public string remarks { get; set; }

        public Nullable<int> size_x { get; set; }
        public Nullable<int> size_y { get; set; }
        public string size_text { get; set; }

        public Nullable<int> drawing_3d_id { get; set; }
        public string drawing_3d_file_name { get; set; }

        public int recomend_size_x { get; set; }
        public int recomend_size_y { get; set; }

        public bool force_changed
        {
            get { return true; }
            set { NotifyPropertyChanged(""); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    } 
    #endregion

    /// <summary>
    /// DrawingsManager.xaml에 대한 상호 작용 논리
    /// </summary>
    ///
 
    public partial class BitmapManager : Window
    {
        enum CurMode
        {
            NORMAL,
            NEW,
            EDIT
        };

        CurMode curMode = CurMode.NORMAL;

        public static RoutedCommand NewCommand = new RoutedCommand();
        public static RoutedCommand EditCommand = new RoutedCommand();
        public static RoutedCommand DeleteCommand = new RoutedCommand();
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand CancelCommand = new RoutedCommand();
        public static RoutedCommand SelectFileCommand = new RoutedCommand();

        private bool _new = true;
        private bool _delete = false;
        private bool _edit = false;
        private bool _save = false;
        private bool _cancel = false;
        private bool _select_file = false;

        private bool img_change_flag = false;

        public const Boolean use_list = true;

        //선택된 TreeViewItem을 임시로 저장하는 변수
        private TreeViewItem selected_tvi;

        private string selected_file_path;
        private int selected_file_size_x;
        private int selected_file_size_y;

        List<img_folder> fd_list = new List<img_folder>();
        sp_img_vm select_img_vm;

        ImageManage img_mgr; 


        #region 초기화

        public BitmapManager()
        {
            InitializeComponent();

            img_mgr = new ImageManage();
            initData();
            initUI();
        }

        private void initData()
        {

#if true
            foreach (var at in g.image_type_list)
            {
                img_folder img_fd = new img_folder(){ type_name = at.image_type_name, folder_name = at.folder_name};

                if( at.image_type_id!= 1160008
                    && at.image_type_id != 1160009
                    && at.image_type_id != 1160011
                    && at.image_type_id != 1160012
                    && at.image_type_id != 1160013
                    && at.image_type_id != 1160014
                    )
                    fd_list.Add(img_fd); // 랙 880 이나, 아이콘이 아닌것만 이미지 관리 가능 / 나머진 시스템 리소스로 관리 됨.
            }

            foreach (var at in g.sp_image_list)
            {
                sp_img_vm img_vm = makeSpListImageResultVm(at);
                img_folder fd = fd_list.Find(f => f.folder_name == at.folder_name);
                if(fd != null)
                {
                    fd.child_list.Add(img_vm);
                    fd.force_changed = true;
                }
            }
#else
            foreach (var at in g.sp_image_list)
            {
                sp_img_vm img_vm = makeSpListImageResultVm(at);
                img_folder fd = fd_list.Find(_fd => _fd.name == img_vm.folder_name);
                if (fd == null)
                {
                    fd = new img_folder() { name = at.folder_name };
                    fd.child_list.Add(img_vm);
                    fd.force_changed = true;
                    fd_list.Add(fd);
                }
                else
                {
                    fd.child_list.Add(img_vm);
                    fd.force_changed = true;
                }
            } 
#endif
        }

        // UI 초기화 
        private void initUI()
        {
            _tvImageTree.ItemsSource = fd_list;
            //_stackDrawingsInfo.DataContext = select_img_vm;
            _cboImgGroup.ItemsSource = fd_list;
        } 
        #endregion

        #region TreeView 이벤트
        // 트리뷰 로딩시 호출, 각 Folder Item을 Expand 시켜 준다
        private void _tvImageTree_Loaded(object sender, RoutedEventArgs e)
        {
            expandAllImageTree();
        }

        //트리뷰 Item선택시 이벤트
        private void _tvImageTree_OnItemSelected(object sender, RoutedEventArgs e)
        {
            //현재의 item을 받아 온다
            _tvImageTree.Tag = e.OriginalSource;
            TreeViewItem now_tvi = _tvImageTree.Tag as TreeViewItem;

            //이전의 item과 같지않으면 이전의 item은 해제한다 => 특수한 경우에만 필요한 부분
            if (selected_tvi != null)
            {
                if (now_tvi.Header != selected_tvi.Header)
                    selected_tvi.IsSelected = false;
            }

            var v = now_tvi.Header;
            
            //선택된 item이 이미지 인 경우 정보를 변경한다
            if (v is sp_img_vm)
            {
                //수정, 신규 중에도 다시 NORMAL로 돌아온다
                curMode = CurMode.NORMAL;

                _tvImageTree.Tag = e.OriginalSource;
                selected_tvi = now_tvi;

                //수정 모드 였을수 있으므로 강제 해제 한다
                editEnd();

                //정보를 변경한다
                select_img_vm = (sp_img_vm)v;
                select_img_vm.force_changed = true;

                _txtImgName.Text = select_img_vm.image_name;
                _txtImgFileName.Text = select_img_vm.file_name;
                _txtImgRemarks.Text = select_img_vm.remarks;
                _txtImgSize.Text = select_img_vm.size_text;
                _txtIDeletable.Text = select_img_vm.deletable;
                if (select_img_vm.deletable != "N")
                    _txtIDeletable.Text = "Y";
                _imgDrawings.Source = null;
                selected_file_path = select_img_vm.client_file_path;
                img_mgr.drawImage(_imgDrawings, selected_file_path);

                _cboImgGroup.SelectedValue = getGroupName(select_img_vm.image_type_id);
//                _cboImgGroup.SelectedValue = select_img_vm.image_type_id;

                //cmd 수정
                _new = true;
                _edit = true;
                _delete = true;
                _cancel = false;
                _save = false;
                _select_file = false;

            }
            //폴더를 선택한 경우에는 원래 선택된 곳이 선택되어 있도록 한다=> OnItemSelected 이벤트가 다시 호출됨
            else
            {
                if (selected_tvi != null)
                {
                    _tvImageTree.Tag = e.OriginalSource;

                    now_tvi.IsSelected = false;
                    selected_tvi.IsSelected = true;
                }
            }
        }

        private string getGroupName(int id)
        {
            int lid = id;
            string ret = "";

            if (lid == 0)
                return ret;
            var a1 = g.image_type_list.Find(p => p.image_type_id == lid);
            if (a1 != null)
            {
                ret = string.Format("{0}", a1.image_type_name);
            }
            return ret;
        }


        #endregion

        #region Command
        #region NewCommand
        private void _cmdNew_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _new;
            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }

        private void _cmdNew_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            curMode = CurMode.NEW;
            clearSelectItem();

            _txtImgName.IsEnabled = true;
            _txtImgRemarks.IsEnabled = true;
            _cboImgGroup.IsEnabled = true;

            selectImageFile();
            _txtImgName.Focus();

            _new = false;
            _delete = false;
            _edit = false;
            _save = true;
            _cancel = true;
            _select_file = true;
        }
        #endregion

        #region EditCommand

        private void _cmdEdit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //bool flag = _save && !_edit;
            if (_txtImgName.Text == "")
                _edit = false;
            e.CanExecute = _edit;
        }

        private void _cmdEdit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            curMode = CurMode.EDIT;

            //if (_manufacture == null)
            //    return;
            _edit = false;
            _save = true;
            _cancel = true;
            _select_file = true;
            _delete = false;

            _txtImgName.IsEnabled = true;
            _txtImgRemarks.IsEnabled = true;
            _cboImgGroup.IsEnabled = true;


            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }
        #endregion

        #region DelCommand
        private void _cmdDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _delete;
        }

        private async void _cmdDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (select_img_vm.deletable == "N")
            {
                MessageBox.Show(g.tr_get("C_Error_Cant_Delete"));
                return;
            }

            if (MessageBox.Show(g.tr_get("C_Delete_Item"), g.tr_get("C_Confirm"), MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            _imgDrawings.Source = null;

            string folder_name = select_img_vm.folder_name;
            string file_name = select_img_vm.file_name;
            string file_path = select_img_vm.client_file_path;

            int image_id = select_img_vm.image_id;

            // 2015.07.13 romee 이미지가 선택이 안되었으면 리턴 처리 
            if (image_id == 0)
                return;

            //서버에 기존 이미지 파일 삭제
            var t1 = img_mgr.DelImageToServer(folder_name, file_name);
            Boolean result = await t1;

            //DB이미지 리스트에서 해당 이미지 정보 삭제
            img_mgr.delImageToDb(image_id, folder_name, file_name);


            img_folder fd = fd_list.Find(at => at.folder_name == folder_name);
            fd.child_list.Remove(select_img_vm);

            _new = true;
            _edit = false;
            _delete = false;
            _save = false;
            _cancel = false;

            clearSelectItem();

            resetImageTree();

            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }
        #endregion

        #region SaveCommand

        private void _cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _save;
        }

        private async void _cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!await saveData())
                return;

            editEnd();

            _new = true;
            _delete = false;
            _edit = false;
            _save = false;
            _cancel = false;
            _select_file = false;

            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }
        private void _cmdCancel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _cancel;
        }

        private void _cmdCancel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _new = true;
            _edit = false;
            _delete = false;
            _save = false;
            _cancel = false;
            _select_file = false;
            clearSelectItem();
        }
        #endregion

        #region SelectFileCommand
        private void _cmdSelectFile_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _select_file;
        }

        private void _cmdSelectFile_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            selectImageFile();
            _txtImgName.Focus();
        }

        #endregion 
        #endregion

        #region 그 외 메소드

        private async Task<bool> saveData()
        {
            string image_name = _txtImgName.Text.Trim();
            string file_name = _txtImgFileName.Text.Trim();
            string image_type_name = _cboImgGroup.Text;
            string remarks = _txtImgRemarks.Text.Trim();

            //1. 신규모드
            if (curMode == CurMode.NEW)
            {
                if (string.IsNullOrEmpty(image_name) == true)
                {
                    MessageBox.Show(g.tr_get("C_Info51"));
                    return false;
                }

                if (string.IsNullOrEmpty(image_type_name) == true)
                {
                    MessageBox.Show(g.tr_get("C_Info52"));
                    return false;
                }

                if (g.image_list.Exists(at => at.file_name == file_name) == true)
                {
                    MessageBox.Show(g.tr_get("ImgMgr_ExistSameImg"));
                    return false;
                }

                img_folder fd = fd_list.Find(at => at.type_name == image_type_name);
                string folder_name = fd.folder_name;

                //이미지를 서버에 업로드 한다
                var t1 = img_mgr.addImageToServer(selected_file_path, folder_name, file_name);
                string server_file_name = await t1;
                if (server_file_name != null)
                {
                    //이미지 정보를 DB에 추가한다

                    image tmp_img = new image();
                    tmp_img.file_name = server_file_name;
                    tmp_img.image_name = image_name;
                    tmp_img.size_x = selected_file_size_x;
                    tmp_img.size_y = selected_file_size_y;
                    tmp_img.deletable = "Y";
                    tmp_img.remarks = remarks;
                    //tmp_img.image_type_id = ;

                    var t2 = img_mgr.addImageToDb(tmp_img, folder_name);
                    int img_id = await t2;


                    //이미지를 class list에 추가한다
                    sp_list_image_Result sp_img = g.sp_image_list.Find(at => at.image_id == img_id);
                    sp_img_vm img_vm = makeSpListImageResultVm(sp_img);

                    img_folder sel_fd = fd_list.Find(at => at.folder_name == folder_name);
                    sel_fd.child_list.Add(img_vm);

                    //clearSelectItem();

                    //treeview를 reset한다
                    resetImageTree();

                }
                else
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }
            }
            //2. 수정모드
            else if (curMode == CurMode.EDIT)
            {
                if (select_img_vm == null)
                    return false;

                string new_img_name = _txtImgName.Text;
                string new_remarks = _txtImgRemarks.Text;
                string new_image_type_name = _cboImgGroup.Text;
                string new_file_name = _txtImgFileName.Text;
                img_folder fd = fd_list.Find(at => at.type_name == image_type_name);
                string new_folder_name = fd.folder_name;


                //1.이미지 가 수정되거나 폴더명이 변경된 경우
                if ((img_change_flag == true) || (select_img_vm.folder_name != new_folder_name))
                {
                    ImageSource img_src = new BitmapImage(new Uri(selected_file_path));
                    //_imgDrawings.Source = img_src;
                    selected_file_size_x = (int)img_src.Width;
                    selected_file_size_y = (int)img_src.Height;

                    //기존서버의 이미지를 삭제한다
                    var t1 = img_mgr.DelImageToServer(select_img_vm.folder_name, select_img_vm.file_name);
                    Boolean result = await t1;

                    //새로운 이미지를 업로드한다
                    var t2 = img_mgr.addImageToServer(selected_file_path, new_folder_name, _txtImgFileName.Text);
                    string server_file_name = await t2;

                    //DB 에서 image data를 수정한다
                    if (server_file_name != null)
                    {
                        var t3 = img_mgr.modifyImageAllInfoToDb(
                            (int)select_img_vm.image_id, new_folder_name, server_file_name,
                            new_img_name, new_remarks, selected_file_size_x, selected_file_size_y);
                        select_img_vm.file_name = server_file_name;
                        select_img_vm.size_x = selected_file_size_x;
                        select_img_vm.size_y = selected_file_size_y;
                        select_img_vm.size_text = string.Format("{0} x {1}", selected_file_size_x, selected_file_size_y);

                        //폴더 명이 변경된 경우
                        if (select_img_vm.folder_name != new_folder_name)
                        {
                            img_folder old_fd = fd_list.Find(at => at.folder_name == select_img_vm.folder_name);
                            old_fd.child_list.Remove(select_img_vm);

                            img_folder new_fd = fd_list.Find(at => at.folder_name == new_folder_name);
                            new_fd.child_list.Add(select_img_vm);

                            select_img_vm.folder_name = new_folder_name;
                        }
                    }
                    else
                    {
                        MessageBox.Show(g.tr_get("C_Error_Server"));
                    }
                }
                //2.이미지 정보만 수정된 경우
                else
                {
                    var t3 = img_mgr.modifyImageAllInfoToDb(
                        (int)select_img_vm.image_id, new_folder_name, new_file_name,
                        new_img_name, new_remarks, selected_file_size_x, selected_file_size_y);
                }
                //클래스 변수 를 수정한다
                select_img_vm.image_name = new_img_name;
                select_img_vm.remarks = new_remarks;
                //treeview를 reset한다
                resetImageTree();
            }
            curMode = CurMode.NORMAL;
            return true;
        }



        //선택된 item을 해제하고 모든 표시된 정보를 clear한다
        private void clearSelectItem()
        {

            select_img_vm = new sp_img_vm();

            _txtImgName.Text = null;
            _txtImgFileName.Text = null;
            _txtImgRemarks.Text = null;
            _txtImgSize.Text = null;
            _imgDrawings.Source = null;
            _cboImgGroup.SelectedValue = null;
            _txtIDeletable.Text = null;

        }

        //파일을 선택하기위헤 dialog를 open하고 선택되면 해당 정보를 저장하는 함수
        private void selectImageFile()
        {
            var dlg = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.png;*.bmp;*.gif"
            };
            if (dlg.ShowDialog() == true)
            {
                ImageSource img_src = new BitmapImage(new Uri(dlg.FileName));
                //_imgDrawings.Source = img_src;
                select_img_vm.client_file_path = dlg.FileName;
                _txtImgFileName.Text = dlg.SafeFileName;
                _txtImgSize.Text = string.Format("{0}x{1}", img_src.Width, img_src.Height);
                selected_file_path = dlg.FileName;
                selected_file_size_x = (int)img_src.Width;
                selected_file_size_y = (int)img_src.Height;
                img_mgr.drawImage(_imgDrawings, selected_file_path);

                img_change_flag = true;
            }
        }

        private void resetImageTree()
        {
            _tvImageTree.ItemsSource = null;
            _tvImageTree.ItemsSource = fd_list;

            expandAllImageTree();

        }

        private void expandAllImageTree()
        {
            for (int i = 0; i < _tvImageTree.Items.Count; i++)
            {
                TreeViewItem item = _tvImageTree.ItemContainerGenerator.ContainerFromIndex(i) as TreeViewItem;
                item.IsExpanded = true;
            }
        }


        private void editEnd()
        {
            _txtImgName.IsEnabled = false;
            _txtImgRemarks.IsEnabled = false;
            _cboImgGroup.IsEnabled = false;

            img_change_flag = false;

        }

        private sp_img_vm makeSpListImageResultVm(sp_list_image_Result img)
        {
            sp_img_vm img_vm = new sp_img_vm()
            {
                image_id = img.image_id,
                image_name = img.image_name,
                image_type_id = img.image_type_id,
                image_type_name = img.image_type_name,

                file_name = img.file_name,
                folder_name = img.folder_name,
                //client_file_path = string.Format("/I2MS2;component/Icons/{0}", img.file_name),
                client_file_path = string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH, img.folder_name, img.file_name),
                deletable = img.deletable,
                remarks = img.remarks,

                size_x = img.size_x,
                size_y = img.size_y,
                size_text = string.Format("{0} X {1}", img.size_x, img.size_y),
                drawing_3d_id = img.drawing_3d_id,
                drawing_3d_file_name = img.drawing_3d_file_name,

                recomend_size_x = img.recomend_size_x,
                recomend_size_y = img.recomend_size_y
            };
            return img_vm;
        }
        
        #endregion
    }

   
}
