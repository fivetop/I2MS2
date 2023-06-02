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

using I2MS2.Library;
using I2MS2.Models;
using I2MS2.Windows;

using WebApi.Models;
using System.Windows.Threading;
using System.Globalization;
using System.Threading;
using I2MS2.Translation;


namespace I2MS2.UserControls
{
    /// <summary>
    /// LeftSideControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LeftSideControl : UserControl
    {
        public AssetTreeViewManagerLeftSide astMgrLeftSide;

        public delegate void PutAndPullEventHandler(object obj);
        public event PutAndPullEventHandler putAndFullEvent;

        public delegate void SelectItemChangeEventHandler(object obj);
        public event SelectItemChangeEventHandler selectItemChange;

        Boolean is_itree_loaded = false;
        Boolean is_ftree_loaded = false;

        private AssetTreeVM selected_ast_vm;


        public LeftSideControl()
        {
            InitializeComponent();

            //asset_tree  treeview 
            //g.select_site = g.site_list.Find(at => at.site_id == g.select_site.site_id);
            //if (g.select_site == null)
            //    return;

            //InitLeftSide(g.select_site.site_id);
            
        }
        
        // 트리뷰 초기화 
        // 로케이션 트리 데이터 읽어오기
        // 지능형 트리뷰 초기화 처리 
        // 즐겨 찾기 초기화
        public void InitLeftSide(int site_id)
        {
            initLocationTreeData(g.select_site.site_id);
        }

        // 재 초기화 , 
        public void reInitLeftSide(int site_id)
        {
            is_ftree_loaded = false;
            is_itree_loaded = false;

            astMgrLeftSide.reInitAssetTreeViewManager(site_id);
        }
        // 트리에서 메뉴로 선택시 차일드 포함 리턴
        public AssetTreeVM getAssetTreeVMinMD(AssetTreeVM ast_vm)
        {
            AssetTreeViewManager astMgr = new AssetTreeViewManager();
            return astMgr.getAssetTreeVMinMD(ast_vm);
        }

        #region // 트리뷰 초기화 처리 
        // 자산 
        public void initLocationTreeData(int site_id)
        {
            //site이름을 가지고 site, building까지의 tree를 표시한다
            astMgrLeftSide = new AssetTreeViewManagerLeftSide(_tvAssetTree);
            astMgrLeftSide.InitAssetTreeViewManager(site_id);
        }
        #endregion

        #region // 트리뷰 로드후 처리 로드된 트리를 확장 처리한다.
        //트리뷰가 로딩될때 expand된상태로 시작한다 
        // expend 하지않음 romee
        private void _tvAssetTree_Loaded(object sender, RoutedEventArgs e)
        {
        }

        // 지능형 로드 완료시 // 탭버튼 눌린다음 확장 처리가 됨.
        private void _tvIntelligentTree_Loaded(object sender, RoutedEventArgs e)
        {
        }
        // 즐겨찾기 로드 완료시 
        private void _tvFavoriteTree_Loaded(object sender, RoutedEventArgs e)
        {
        }
        #endregion

        #region // 전체 트리 선택 이벤트 처리 
        //asset_tree Item을 선택한 경우
        private void _tvAssetTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (_tvAssetTree.SelectedItem == null)
                return;

            AssetTreeVM ast_vm = (AssetTreeVM)_tvAssetTree.SelectedItem;
            if (ast_vm != null)
            {
                selected_ast_vm = ast_vm;
            }
        }
        #endregion

        #region // 각 트리 확장과 축소 처리 
        // 자산  
        private void _tgbtnTreeExpander_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement).DataContext == null) return;
            var bb = (sender as FrameworkElement).DataContext;
            if (!(bb is AssetTreeVM)) return;

            AssetTreeVM item = (AssetTreeVM)(sender as FrameworkElement).DataContext;
            astMgrLeftSide.expandTree(item);
            //_tvAssetTree.ItemsSource = item.child_list;
        }
        private void _tgbtnTreeExpander_Unchecked(object sender, RoutedEventArgs e)
        {
            var bb = (sender as FrameworkElement).DataContext;
            if (!(bb is AssetTreeVM)) return;

            AssetTreeVM item = (AssetTreeVM)(sender as FrameworkElement).DataContext;
            astMgrLeftSide.unExpandTree(item);
        }
        #endregion 

        #region //트리뷰를 더블클릭하면 확장한다 -> 더블클릭시에도 확장으로 처리 -> 확장을 호출 
        // 자산 
        private void _tvAssetTree_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AssetTreeVM ast_vm = (AssetTreeVM)_tvAssetTree.SelectedItem;
            if (ast_vm == null) return;
            ast_vm.is_expanded = !ast_vm.is_expanded; // 확장 호출 
            ast_vm.force_changed = true;
            e.Handled = true;
        }
        #endregion

        #region // 공통 처리 - 시그날에 대한 처리   
        public void changePCStatus(int outlet_asset_id, int outlet_port_no, int terminal_asset_id, bool on_off)
        {
            String img_path;
            if (on_off == true)
                img_path = "/I2MS2;component/Icons/pc_on_16.png";
            else
                img_path = "/I2MS2;component/Icons/pc_16.png";

            //location tree
            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(delegate
            {
                try
                {
                    //자산 트리 데이터 변경
                    astMgrLeftSide.changePcChange(outlet_asset_id, outlet_port_no, terminal_asset_id, img_path);
                }
                catch (Exception)
                { }
            }));

        }

        public void addPC(int outl_asset_id, int port_no, int terminal_asset_id, Boolean on_off)
        {
            String img_path;
            if (on_off == true)
                img_path = "/I2MS2;component/Icons/pc_on_16.png";
            else
                img_path = "/I2MS2;component/Icons/pc_16.png";

            //asset outl_ast = g.asset_list.Find(at => at.asset_id == outl_asset_id);
            //if (outl_ast == null) return;
            asset pc_ast = g.asset_list.Find(at => at.asset_id == terminal_asset_id);
            if (pc_ast == null) return;

            if (pc_ast.location_id != 0)
            {
                //location tree
                App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(delegate
                {
                    try
                    {
                        //자산 트리 데이터 변경
                        astMgrLeftSide.removePc(outl_asset_id, port_no, terminal_asset_id);
                        astMgrLeftSide.addPc(outl_asset_id, port_no, terminal_asset_id, img_path);
                    }
                    catch (Exception)
                    { }
                }));

            }
        }

        public void removePC(int outl_asset_id, int port_no, int terminal_asset_id)
        {
            //location트리에 위치가 지정된 PC
            if (g.asset_ast_vm_dic.ContainsKey(terminal_asset_id))
            {
                //location tree
                App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(delegate
                {
                    try
                    {
                        //로케이션 트리 데이터 삭제
                        astMgrLeftSide.removePc(outl_asset_id, port_no, terminal_asset_id);
                        g.asset_ast_vm_dic.Remove(terminal_asset_id);           // Jake,  ok?
                    }
                    catch (Exception)
                    { }
                }));
            }
            //위치가 없어 conless 쪽에 있는 PC
            else
            {
            }
        }

        public void movePCToUnlocated(int outl_asset_id, int port_no, int terminal_asset_id)
        {
            String img_path;
            img_path = "/I2MS2;component/Icons/pc_16.png";

            asset terminal_ast = g.asset_list.Find(at => at.asset_id == terminal_asset_id);
            if (terminal_ast == null) return;

            //AssetTree to Ctree
            movePcLtreeToCtree(outl_asset_id, port_no, terminal_ast, img_path);
        }

        public void movePCToAssetTree(int old_outlet_asset_id, int old_outlet_port_no, int outl_asset_id, int port_no, int terminal_asset_id)
        {
        }

        private void movePcLtreeToLtree(int old_outlet_asset_id, int old_outlet_port_no, int outl_asset_id, int port_no, asset terminal_ast, String img_path)
        {
            //location tree
            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(delegate
            {
                try
                {
                    //로케이션 트리 데이터 변경
                    astMgrLeftSide.removePc(old_outlet_asset_id, old_outlet_port_no, terminal_ast.asset_id);
                    astMgrLeftSide.removePc(terminal_ast.asset_id);
                    astMgrLeftSide.addPc(outl_asset_id, port_no, terminal_ast.asset_id, img_path);
                }
                catch (Exception)
                { }
            }));
        }

        private void movePcLtreeToCtree(int outl_asset_id, int port_no, asset terminal_ast, String img_path)
        {
            //location tree
            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(delegate
            {
                try
                {
                    //로케이션 트리 데이터 변경
                    astMgrLeftSide.removePc(terminal_ast.asset_id);
                }
                catch (Exception)
                { }
            }));
        }

        private void movePcCtreeToLtree(int outl_asset_id, int port_no, asset terminal_ast, String img_path)
        {
            //location tree
            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(delegate
            {
                try
                {
                    //로케이션 트리 데이터 변경
                    astMgrLeftSide.addPc(outl_asset_id, port_no, terminal_ast.asset_id, img_path);
                }
                catch (Exception)
                { }
            }));
        }
        #endregion

        #region // Asset Tree 추가 / 삭제 / 이동
        public void addAssetToTreeView(int asset_id)
        {
            asset_tree a = g.asset_tree_list.Find(at => at.asset_id == asset_id);
            if (a == null)
                return;
            
            asset ast = g.asset_list.Find(at => at.asset_id == asset_id);
            if(ast == null)
                return;
          
            astMgrLeftSide.addAssetToTreeView(a, ast.location_id);
            AssetTreeType type = CatalogType.getCatalogType(ast.catalog_id);
        }

        public void delAssetToTreeView(int asset_id)
        {
            //asset_tree a = g.asset_tree_list.Find(at => at.asset_id == asset_id);
            asset_tree astt = g.asset_tree_list.Find(at => at.asset_id == asset_id);
            if (astt == null)
                return;
#if false

            AssetTreeVM p_ast_vm = g.location_ast_vm_dic[astt.location_id];
            if (p_ast_vm == null)
                return;


            AssetTreeVM ast_vm = p_ast_vm.child_list.Find(at => at.type_id == astt.asset_id);
            if (ast_vm == null)
                return;
#else
            
            if (!g.asset_ast_vm_dic.ContainsKey(asset_id))
                return;
            AssetTreeVM ast_vm = g.asset_ast_vm_dic[asset_id];

            AssetTreeVM p_ast_vm = ast_vm.parant_ast_vm;
            if (p_ast_vm == null) return;
#endif
            if (asset_id == astt.asset_id)
            {
                astMgrLeftSide.delAssetToTreeView(ast_vm);
                asset ast = g.asset_list.Find(at => at.asset_id == asset_id);
                AssetTreeType type = CatalogType.getCatalogType(ast.catalog_id);
            }
            else
            {
                MessageBox.Show(g.tr_get("C_Error_AssetTree_1"));
            }
        }
        // 로케이션 추가인 경우 
        public void addLocationToTreeView(int location_id)
        {
            asset_tree a = g.asset_tree_list.Find(at => at.location_id == location_id);
            if (a == null)
                return;

            int prev_location_id = Etc.get_prev_location_id(location_id);
            astMgrLeftSide.addAssetToTreeView(a, prev_location_id);
        }

        public void delLocationToTreeView(int location_id)
        {
            asset_tree ast = g.asset_tree_list.Find(at => at.location_id == location_id);
            if (ast == null)
                return;

            AssetTreeVM ast_vm = g.location_ast_vm_dic[location_id];
            if (ast_vm == null)
                return;

            astMgrLeftSide.delAssetToTreeView(ast_vm);
        }

        public void editAssetToTreeView(int asset_id)
        {
            asset_tree a = g.asset_tree_list.Find(at => at.asset_id == asset_id);
            if (g.asset_ast_vm_dic.ContainsKey(asset_id) == false) return;
            AssetTreeVM ast_vm = g.asset_ast_vm_dic[asset_id];
            astMgrLeftSide.editAssetToTreeView(a, ast_vm);
        }

        public void editLocationToTreeView(int location_id)
        {
            asset_tree a = g.asset_tree_list.Find(at => at.location_id == location_id);
            if (g.location_ast_vm_dic.ContainsKey(location_id) == false) return;
            AssetTreeVM ast_vm = g.location_ast_vm_dic[location_id];
            astMgrLeftSide.editAssetToTreeView(a, ast_vm);
        }

        public void updateAssetTreeItem(AssetTreeVM ast_vm)
        {
            astMgrLeftSide.updateAssetTreeItem(ast_vm);
        }

        public AssetTreeVM getAndCopyAssetTreeVM(AssetTreeVM ast_vm)
        {
            return astMgrLeftSide.getAndCopyAssetTreeVM_withChild(ast_vm);
        }
        #endregion


        #region port 상태에 대한 처리
        public void updatePortStatusAllTree(int ipp_asset_id, int port_no,String port_status)
        {
        }

        // 포트 알람 발생시 아이콘 변경 처리 
        public void updatePortAlarmAllTree(int ipp_asset_id, int port_no, String port_status)
        {
            String img_path;
            int on_alarm = 0;

            //1. find ipp
            //AssetTreeVM ipp_ast_vm = itreeMgr.getIpp(ipp_asset_id);
            if(g.asset_ast_vm_dic.ContainsKey(ipp_asset_id)== false) return;
            AssetTreeVM ipp_ast_vm = g.asset_ast_vm_dic[ipp_asset_id];
            

            //2. find port vm
            AssetTreeVM port_ast_vm = ipp_ast_vm.child_list.Find(at => at.port_no == port_no);
            if (port_ast_vm == null)
                return;

            Boolean on_plug;
            switch (port_status)
            {
                case "-":
                    port_ast_vm.on_alarm = false;
                    asset_ipp_port_link port = g.asset_ipp_port_link_list.Find(at => at.asset_ipp_port_link_id == port_ast_vm.type_id);
                    if (port == null)
                        return;

                    
                    if ((port.ipp_port_status == "L") || (port.ipp_port_status == "P"))
                    {
                        img_path = "/I2MS2;component/Icons/port_on_16.png";
                        on_plug = true;
                    }
                    else
                    { 
                        img_path = "/I2MS2;component/Icons/port_16.png";
                        on_plug = false;
                    }
                    on_alarm = -1;
                    break;
                case "U":
                    //ipp_ast_vm.on_alarm = true;
                    img_path = "/I2MS2;component/Icons/port_16.png";
                    on_plug = false;
                    on_alarm = +1;
                    break;
                case "P":
                    img_path = "/I2MS2;component/Icons/port_on_16.png";
                    on_plug = true;
                    on_alarm = +1;
                    break;
                default:
                    return;
            }
          

            //location tree
            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(delegate
            {
                try
                {
                    //로케이션 트리 데이터 변경 => model data도 여기서 변경한다
                    //=> 나머지는 트리 VM에서만 변경해야 한다
                    astMgrLeftSide.changePortStatusView(port_ast_vm, on_plug, on_alarm);
                }
                catch (Exception)
                { }
            }));

        }

        // 스위치 상태 변경 처리 - 시그날 알 처리  
        public void updateIcswStatus()
        {
            astMgrLeftSide.changeswStatus();
        }

        #endregion

        #region ipp 상태에 대한 처리
        public void updateIppAlarmStatus(int ipp_asset_id, Boolean up_down)
        {
            //location tree
            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(delegate
            {
                try
                {
                    astMgrLeftSide.changeIppStatusView(ipp_asset_id, up_down);
                }
                catch (Exception) { }
            }));

        }
        #endregion

        #region IC 상태에 대한 처리
        public void updateIcAlarmStatus(int ic_asset_id, Boolean up_down)
        {
            //location tree
            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(delegate
            {
                try
                {
                    astMgrLeftSide.changeIcStatusView(ic_asset_id, up_down);
                }
                catch (Exception) { }
            }));

        }
        #endregion

        #region // Right Mouse Button 처리 -> 구체 처리 없음 
        // 팝업 메뉴 뛰우기 -> 컨텍스트 에서 자동 처리 
        private void _tvAssetTree_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            if (panel == null) return;
            asset_tree_for_treeview myClicked = panel.DataContext as asset_tree_for_treeview; // ??
            if (myClicked == null) return;
        }
        // 지능형 마우스 이벤트 처리
        private void _tvIntelligentTree_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _tvAssetTree_MouseRightButtonDown(sender, e);       // 상동
        }
        // 즐겨찾기 마우스 이벤트 처리
        private void _tvFavoriteTree_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _tvAssetTree_MouseRightButtonDown(sender, e);       // 상동
        }
        // 자산 마우스 이벤트 처리 
        private void _tvAssetTree_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeView tv = (TreeView)sender;
            IInputElement element = tv.InputHitTest(e.GetPosition(tv));
            while (!((element is TreeView) || element == null))
            {
                if (element is TreeViewItem)
                    break;

                if (element is FrameworkElement)
                {
                    FrameworkElement fe = (FrameworkElement)element;
                    element = (IInputElement)(fe.Parent ?? fe.TemplatedParent);
                }
                else
                    break;
            }
            if (element is TreeViewItem)
            {
                element.Focus();
                e.Handled = false;
            }
        }
        // 지능형 트리 마우스 이벤트 처리 
        private void _tvIntelligentTree_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _tvAssetTree_PreviewMouseRightButtonDown(sender, e);
        }
        // 지능형 트리 마우스 이벤트 처리 
        private void _tvFavoriteTree_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _tvAssetTree_PreviewMouseRightButtonDown(sender, e);
        }
        #endregion

        #region // Mouse Move 처리
        // 자산 마우스 무브시 처리 
        private void _tvAssetTree_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //범위 지정 필요.. 윈도우 밖으로 나가면 죽음
                Point position = e.GetPosition(null);

                if (Math.Abs(position.X - tv_drag_start_pot.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(position.Y - tv_drag_start_pot.Y) > SystemParameters.MinimumVerticalDragDistance)
                    _tvAssetTree_BeginDrag(e);
            }
        }
        // 지능형 
        private void _tvIntelligentTree_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //범위 지정 필요.. 윈도우 밖으로 나가면 죽음
                Point position = e.GetPosition(null);

                if (Math.Abs(position.X - tv_drag_start_pot.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(position.Y - tv_drag_start_pot.Y) > SystemParameters.MinimumVerticalDragDistance)
                    _tvIntelligentTree_BeginDrag(e);
            }
        }
        // 즐겨찾기 
        private void _tvFavoriteTree_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //범위 지정 필요.. 윈도우 밖으로 나가면 죽음
                Point position = e.GetPosition(null);

                if (Math.Abs(position.X - tv_drag_start_pot.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(position.Y - tv_drag_start_pot.Y) > SystemParameters.MinimumVerticalDragDistance)
                    _tvFavoriteTree_BeginDrag(e);
            }
        }
        #endregion

        #region  // Left Mouse Button 처리
        private Point tv_drag_start_pot;
        // 선택이나 드래그 준비 처리 
        // 자산 마우스 이벤트 처리 
        private void _tvAssetTree_Item_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // 처리없음 
            if (e.ClickCount == 2)
            {
                //더블클릭한 경우에만 해당 Item을 셀렉트 한다
                //AssetTreeVM ast_vm = (AssetTreeVM)_tvAssetTree.SelectedItem;
                //if (ast_vm == null)
                //    return;

                //astMgrLeftSide.expandTree(ast_vm);

                //selectItemChange(ast_vm);
            }
        }

        private void _tvAssetTree_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tv_drag_start_pot = e.GetPosition(null);

            //if(e.ClickCount==2)
            //{
            //    //더블클릭한 경우에만 해당 Item을 셀렉트 한다
            //    AssetTreeVM ast_vm = (AssetTreeVM)sender;
            //    if (ast_vm == null)
            //        return;
            //    selectItemChange(ast_vm);
            //}
        }
        // 지능형 마우스 
        private void _tvIntelligentTree_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _tvAssetTree_MouseLeftButtonDown(sender, e);        // 상동
        }
        // 지능형 
        private void _tvFavoriteTree_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _tvAssetTree_MouseLeftButtonDown(sender, e);        // 상동
        }
        #endregion

        #region // DRAG 처리 , Drop 부분은 각각의 그리드,페이지 등에서 따로 구현 해야만 함
        // 자산 드래그 
        private void _tvAssetTree_BeginDrag(MouseEventArgs e)
        {
            AssetTreeVM item;
            
            try
            {
                // get the data for the ListViewItem
                item = (AssetTreeVM)(e.OriginalSource as FrameworkElement).DataContext;
                if (item == null) return; // romee/jake
                DataObject data = new DataObject("asset_tree_for_treeview", item);
                DragDropEffects de = DragDrop.DoDragDrop(_tvAssetTree, data, DragDropEffects.Move);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception error: code={0}, msg={1}", ex.HResult, ex.Message);
            };
        }
        // 지능형 드래그 
        private void _tvIntelligentTree_BeginDrag(MouseEventArgs e)
        {
            // get the data for the ListViewItem
            AssetTreeVM item = (AssetTreeVM)(e.OriginalSource as FrameworkElement).DataContext;

            if (item != null)
            {
                DataObject data = new DataObject("intelligent_tree_for_treeview", item);

                try
                {
                    DragDropEffects de = DragDrop.DoDragDrop(_tvAssetTree, data, DragDropEffects.Move);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception error: code={0}, msg={1}", ex.HResult, ex.Message);
                };
            }
        }
        // 즐겨찾기 드래그 
        private void _tvFavoriteTree_BeginDrag(MouseEventArgs e)
        {
            // get the data for the ListViewItem
            AssetTreeVM item = (AssetTreeVM)(e.OriginalSource as FrameworkElement).DataContext;

            if (item != null)
            {
                DataObject data = new DataObject("favorite_tree_for_treeview", item);
                try
                {
                    DragDropEffects de = DragDrop.DoDragDrop(_tvAssetTree, data, DragDropEffects.Move);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception error: code={0}, msg={1}", ex.HResult, ex.Message);
                };
            }
        }
        // ??
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
        #endregion

        #region // Drop 처리  -> 룸사이 이동 가능 엣셋에 대해서 
        private async void _tvAssetTree_Drop(object sender, DragEventArgs e)
        {
            AssetTreeVM vm = null;
            int source_asset_id = 0;
            int source_port_no = 0;
            if (e.Data.GetDataPresent("asset_tree_for_treeview"))
                vm = e.Data.GetData("asset_tree_for_treeview") as AssetTreeVM;
            else 
                return;
 
            if (vm == null)
                return;

            source_asset_id = vm.asset_id ?? 0;
            source_port_no = vm.port_no;
            int catalog_id = Etc.get_catalog_id_by_asset_id(source_asset_id);

            // Drop 한 곳의 위치를 알아온다.
            TreeViewItem dest_item = FindAnchestor<TreeViewItem>((DependencyObject)e.OriginalSource);
            if (dest_item != null)
            {
                AssetTreeVM dest_ast_vm = (AssetTreeVM)dest_item.Header;
                //현재는 Asset인 경우만 지원한다
                if( (dest_ast_vm.asset_tree_id!=vm.asset_tree_id))
                {
                    AssetTreeVM source_parent_vm = vm.parant_ast_vm;
                    // AssetTreeVM dest_parent_vm = dest_ast_vm.parant_ast_vm;
                    AssetTreeVM dest_parent_vm = getAssetTreeVMinMD(dest_ast_vm.parant_ast_vm);

                    // 같은 위치에서
                    // if (source_parent_vm.location_id == dest_ast_vm.location_id)
                    //    return;
                    if ((vm.disp_level - 1) == dest_ast_vm.disp_level)
                    {
                        astMgrLeftSide.moveAssetInSameParent(vm, dest_ast_vm);
                        await g.left_tree_handler.moveAssetTreeItem(vm, source_parent_vm, dest_ast_vm, dest_parent_vm);
                    }
                    else if (vm.disp_level == dest_ast_vm.disp_level)
                    {
                        astMgrLeftSide.moveAssetInSameParent(vm, dest_ast_vm);
                        await g.left_tree_handler.moveAssetTreeItem(vm, source_parent_vm, dest_ast_vm, dest_parent_vm);
                    }

                }
            }
        }

        #endregion

        #region // 버튼 처리 고정 / 유동 처리 전체 Grid 위치 이동 이벤트
        private void _tgbPutIn_Checked(object sender, RoutedEventArgs e)
        {
            putAndFullEvent("l_put");
        }

        private void _tgbPutIn_Unchecked(object sender, RoutedEventArgs e)
        {
            putAndFullEvent("l_pull");
        }
        #endregion

        #region // 검색 버튼 처리 Search 검색 처리 
        // Helper to search up the VisualTree
        private static T FindAnchestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        public static CultureInfo CurrentCulture
        {
            get
            {
                return TranslationManager.Instance.CurrentLanguage;
                //return Thread.CurrentThread.CurrentCulture;
            }
        }

        private void _btnSearch_Click(object sender, RoutedEventArgs e)
        {
            String search_str = _txtSearch.Text;
            AssetTreeVM ast_vm = astMgrLeftSide.seachAssetTreeVM(search_str);
            if (ast_vm == null) return;

            astMgrLeftSide.selectAssetTreeVM(ast_vm);
            
        }

        private void _txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            _listSearch.Height = 0;
            if (e.Key == Key.Enter)
            {
                String search_str = _txtSearch.Text;
                AssetTreeVM ast_vm = astMgrLeftSide.seachAssetTreeVM(search_str);
                if (ast_vm == null) return;

                astMgrLeftSide.selectAssetTreeVM(ast_vm);
            }
        }
        // 검색 에서
        private void _txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_txtSearch.Text.Length != 0)
            {
                String search_str = _txtSearch.Text;
                List<AssetTreeVM> ast_vm_l = astMgrLeftSide.searchAllAssetTreeVM(search_str); // 일정수만 가능 

                _listSearch.Height = Double.NaN;
                _listSearch.ItemsSource = null;
                _listSearch.ItemsSource = ast_vm_l;
            }
            else
            {
                _listSearch.Height = 0;
            }
            
        }

        private void _listSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_listSearch.SelectedItem == null) return;

            AssetTreeVM ast_vm = (AssetTreeVM)_listSearch.SelectedItem;
            if (ast_vm == null) return;

            astMgrLeftSide.selectAssetTreeVM(ast_vm);

            _listSearch.ItemsSource = null;
            _listSearch.Height = 0;
        }
        #endregion

        #region  // 사용 안함 
        private AssetTreeVM GetItemAtLocation(Point location)
        {
            AssetTreeVM foundItem = default(AssetTreeVM);
            HitTestResult hitTestResults = VisualTreeHelper.HitTest(_tvAssetTree, location);

            if (hitTestResults.VisualHit is FrameworkElement)
            {
                object dataObject = (hitTestResults.VisualHit as
                    FrameworkElement).DataContext;

                if (dataObject is AssetTreeVM)
                {
                    foundItem = (AssetTreeVM)dataObject;
                }
            }

            return foundItem;
        } 
        #endregion

        internal void searchlocation(int p)
        {
            AssetTreeVM ast_vm = astMgrLeftSide.seachAssetTreeBylocation(p);
            if (ast_vm == null) return;

            astMgrLeftSide.selectAssetTreeVM(ast_vm);
        }

        internal void searchasset(int p)
        {
            AssetTreeVM ast_vm = astMgrLeftSide.seachAssetTreeByasset(p);
            if (ast_vm == null) return;

            astMgrLeftSide.selectAssetTreeVM(ast_vm);
        }

    }

    #region converter

    public class TreeLightColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return Colors.Transparent;

            AssetTreeType type = (AssetTreeType) value;

            if ((type == AssetTreeType.Site) || (type == AssetTreeType.Building) || (type == AssetTreeType.Floor) || (type == AssetTreeType.Room) || (type == AssetTreeType.Rack))
                return Color.FromRgb(86,156,214);

            return Colors.Gray;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }
    public class TreeDarkColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return Colors.Transparent;

            AssetTreeType type = (AssetTreeType)value;

            if ((type == AssetTreeType.Site) || (type == AssetTreeType.Building) || (type == AssetTreeType.Floor) || (type == AssetTreeType.Room) || (type == AssetTreeType.Rack))
                return Colors.Transparent;

            return Colors.Transparent;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }
    #endregion  
   
}