using I2MS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WebApi.Models;

namespace I2MS2.Library
{
    // 트리뷰중 즐겨찾기 뷰 모델 
    public class FavoriteTreeViewManager
    {
        //보여질 TreeView
        public TreeView ftree;

        AssetTreeViewManager astMgr;
        List<int[]> expandinfo_list = new List<int[]>();
        public List<AssetTreeVM> favorite_ast_vm_list = new List<AssetTreeVM>();

           //초기화 함수
        public FavoriteTreeViewManager(TreeView _tv, int site_id)
        {
            //트리뷰 세팅
            ftree = _tv;
            astMgr = new AssetTreeViewManager();

            ftree_initData(site_id);
        }

        public void reInitFtree(int site_id)
        {
            ftree.ItemsSource = null;
            favorite_ast_vm_list.Clear();
            ftree_initData(site_id);
        }


        private void ftree_initData(int site_id)
        {
            var list = g.favorite_tree_list.OrderBy(p => p.disp_order);
            foreach(var f in list)
            {
                location f_l = g.location_list.Find(at => at.location_id == f.location_id);
                if (f_l.site_id == site_id)
                {
                    asset ast = g.asset_list.Find(at => at.asset_id == f.asset_id);
                    AssetTreeVM l_ast_vm = g.location_ast_vm_dic[ast.location_id];
                    AssetTreeVM ast_vm = l_ast_vm.child_list.Find(at => at.type_id == ast.asset_id);
                    //AssetTreeVM f_ast_vm = astMgr.cpAssetTreeTVWithChildAll(ast_vm);
                    AssetTreeVM f_ast_vm = astMgr.cpAssetTreeTV(ast_vm);
                    f_ast_vm.favorite_tree_id = f.favorite_tree_id;
                    favorite_ast_vm_list.Add(f_ast_vm);
                }
            }
            
            ftree.ItemsSource = favorite_ast_vm_list;


        }

        public void addPc(int outl_asset_id, int port_no, int terminal_asset_id)
        {
            AssetTreeVM port_ast_vm_md = getPortAssetTreeVMinMD(outl_asset_id, port_no);
            if (port_ast_vm_md == null) return;

            reloadAssetTreeVMinTV(port_ast_vm_md);
        }

        public void removePc(int outl_asset_id, int port_no, int terminal_asset_id)
        {
            AssetTreeVM port_ast_vm_md = getPortAssetTreeVMinMD(outl_asset_id, port_no);
            if (port_ast_vm_md == null) return;

            reloadAssetTreeVMinTV(port_ast_vm_md);
        }

        public void changePcStatus(int outl_asset_id, int port_no, int terminal_asset_id, String img_path)
        {
            AssetTreeVM port_ast_vm_md = getPortAssetTreeVMinMD(outl_asset_id, port_no);
            if (port_ast_vm_md == null) return;

            reloadAssetTreeVMinTV(port_ast_vm_md);
        }

        //drag_ast_vm 선택해서 드래그한 Asset
        //drop_ast_vm 마우스로 드롭한 위치에 Asset
        //drag_ast_vm 을 drop_ast_vm 아래에 위치하게 한다
        public void moveAssetInSameParent(AssetTreeVM drag_ast_vm, AssetTreeVM drop_ast_vm)
        {
            //1. 부모를 받아와서 같은 부모인지 확인한다
            if (favorite_ast_vm_list.Exists(at => at.favorite_tree_id == drag_ast_vm.favorite_tree_id) == false) return;
            if (favorite_ast_vm_list.Exists(at => at.favorite_tree_id == drop_ast_vm.favorite_tree_id) == false) return;
           
            //2. 재정렬 한다
            AssetTreeVM dg_ast_vm_tv = favorite_ast_vm_list.Find(at => at.favorite_tree_id == drag_ast_vm.favorite_tree_id);


            int dg_index = favorite_ast_vm_list.FindIndex(at => at.favorite_tree_id == drag_ast_vm.favorite_tree_id);
            if (dg_index == -1) return;

            favorite_ast_vm_list.Remove(dg_ast_vm_tv);
            int dp_index = favorite_ast_vm_list.FindIndex(at => at.favorite_tree_id == drop_ast_vm.favorite_tree_id);
            if (dp_index == -1)
            {
                favorite_ast_vm_list.Insert(dg_index, dg_ast_vm_tv);
                return;
            }
            favorite_ast_vm_list.Insert(dp_index + 1, dg_ast_vm_tv);

            //3. 각각의 disp_order를 변경한다
            foreach (var item in favorite_ast_vm_list)
            {
                if (item is AssetTreeVM)
                {
                    int index = favorite_ast_vm_list.FindIndex(at => at.favorite_tree_id == item.favorite_tree_id);
                    item.disp_order = index + 1;
                }
            }

            //4. 부모를 reset한다
            ftree.ItemsSource = null;
            ftree.ItemsSource = favorite_ast_vm_list;

            //5.Item을 다시 선택한다
            TreeViewItem tvi = getTreeViewItem(dg_ast_vm_tv);
            tvi.IsSelected = true;
        }


        public void addAsset(int asset_id)
        {
            ftree.ItemsSource = null;
            
            asset ast = g.asset_list.Find(at => at.asset_id == asset_id);
            AssetTreeVM l_ast_vm = g.location_ast_vm_dic[ast.location_id];
            AssetTreeVM ast_vm = l_ast_vm.child_list.Find(at => at.type_id == ast.asset_id);
            //AssetTreeVM f_ast_vm = astMgr.cpAssetTreeTVWithChildAll(ast_vm);
            AssetTreeVM f_ast_vm = astMgr.cpAssetTreeTV(ast_vm);

            favorite_tree f = g.favorite_tree_list.Find(at => at.asset_id == asset_id);
            if (f == null) return;
            f_ast_vm.favorite_tree_id = f.favorite_tree_id;
                    
            favorite_ast_vm_list.Add(f_ast_vm);

           ftree.ItemsSource = favorite_ast_vm_list;
        }

        public void removeAsset(int asset_id)
        {
            AssetTreeVM ast_vm = favorite_ast_vm_list.Find(at => at.asset_id == asset_id);
            if (ast_vm == null)
                return;
            //unExpandTree(ast_vm);
            ftree.ItemsSource = null;
           
            favorite_ast_vm_list.Remove(ast_vm);
            ftree.ItemsSource = favorite_ast_vm_list;
        }


        #region 확장, 축소에 대한 처리

        //축소 이벤트 발생시 호출

        public void unExpandTree(AssetTreeVM ast_vm_item)
        {
#if false
            int index = favorite_ast_vm_list.FindIndex(at => at.asset_id == ast_vm_item.asset_id);
            try
            {
                TreeViewItem tvi = (TreeViewItem)ftree.ItemContainerGenerator.ContainerFromIndex(index);
                tvi.IsExpanded = false;
            }
            catch (Exception) { }
#else
            TreeViewItem tvi = getTreeViewItem(ast_vm_item);
            tvi.ItemsSource = null;
            tvi.IsExpanded = false;
            removeExpandState(ast_vm_item);
#endif
        } 

        //확대 이벤트 발생시 호출
        public void expandTree(AssetTreeVM ast_vm_item)
        {
#if false
            int index = favorite_ast_vm_list.FindIndex(at => at.asset_id == ast_vm_item.asset_id);
            try
            {
                TreeViewItem tvi = (TreeViewItem)ftree.ItemContainerGenerator.ContainerFromIndex(index);
                tvi.IsExpanded = true;
            }
            catch (Exception) { }
#else
            //현재 item을 한단계의 child를 포함해 copy온다
            AssetTreeVM ast_vm_md = astMgr.getAssetTreeVMinMD(ast_vm_item);
            AssetTreeVM ast_vm = astMgr.cpAssetTreeTVWithChild(ast_vm_md);

            TreeViewItem tvi = getTreeViewItem(ast_vm_item);
            tvi.ItemsSource = ast_vm.child_list;
            tvi.IsExpanded = true;

            addExpandState(ast_vm_item);
#endif
        }
        #region EXPAND info manage

        public void getAndApplyExpandInfoList()
        {
            List<int[]> tmp_list = Reg.get_asset_tree("favorite_tree");

            //expandinfo_list.Remove(expandinfo_list[1]);
            foreach (var expandinfo in tmp_list)
            {
                if ((expandinfo[1] != 0) && (expandinfo[2] == 0))
                {
                    AssetTreeVM ast_vm = favorite_ast_vm_list.Find(at => at.asset_id == expandinfo[1]);
                    if (ast_vm == null) return;

                    ast_vm.is_expanded = true;
                    ast_vm.force_changed = true;
                    //expandTreeCustom(ast_vm);
                }
                //port
                else
                {
                    if (g.asset_ast_vm_dic.ContainsKey(expandinfo[1]) == false) return;
                    AssetTreeVM p_ast_vm = g.asset_ast_vm_dic[expandinfo[1]];

                    AssetTreeVM ast_vm = p_ast_vm.child_list.Find(at => at.port_no == expandinfo[2]);
                    if (ast_vm == null) return;
                    //expandTreeCustom(ast_vm);

                }
            }

        }

        private void addExpandState(AssetTreeVM ast_vm)
        {
            if (ast_vm.type == AssetTreeType.Port)
            {
                //포트인 경위 상위 asset의 확장정보가 있는 경우 삭제해야 한다
                int[] p_int = expandinfo_list.Find(at => (at[1] == ast_vm.asset_id) && (at[2] == 0));
                if (p_int != null)
                    expandinfo_list.Remove(p_int);

                addExpandToList(ast_vm.asset_id ?? 0, ast_vm.port_no);
            }
            else
            {
                addExpandToList(ast_vm.asset_id ?? 0, 0);
            }
        }


        private void removeExpandState(AssetTreeVM ast_vm)
        {
            if (ast_vm.type == AssetTreeType.Port)
            {
                removeExpandFromList(ast_vm.asset_id ?? 0, ast_vm.port_no);
            }
            else
            {
                removeExpandFromList(ast_vm.asset_id ?? 0, 0);

                List<int[]> exp_list = expandinfo_list.FindAll(at => at[1] == ast_vm.asset_id);
                foreach (var exp in exp_list)
                {
                    removeExpandFromList(exp[1], exp[2]);
                }
            }
        }

        private void addExpandToList(int asset_id, int port_no)
        {
            int[] tmp_exp_info = new int[3];
            tmp_exp_info[0] = 0;
            tmp_exp_info[1] = asset_id;
            tmp_exp_info[2] = port_no;
            if (expandinfo_list.Exists(at => (at[0] == tmp_exp_info[0]) && (at[1] == tmp_exp_info[1]) && (at[2] == tmp_exp_info[2]))
                    == false)
                expandinfo_list.Add(tmp_exp_info);
            updateExpandInfoReg();
        }


        private void removeExpandFromList(int asset_id, int port_no)
        {
            int[] tmp_int = expandinfo_list.Find(at => (at[0] == asset_id) && (at[0] == port_no));
            if (tmp_int != null)
            {
                expandinfo_list.Remove(tmp_int);
                updateExpandInfoReg();
            }

        }

        private void updateExpandInfoReg()
        {
            Boolean ret = Reg.save_tree("favorite_tree", expandinfo_list);
        }

        
        #endregion


        private void reloadAssetTreeVMinTV(AssetTreeVM ast_vm)
        {
            TreeViewItem tvi = getTreeViewItem(ast_vm);
            if (tvi == null) return;
            Boolean is_expand = tvi.IsExpanded;
            tvi.ItemsSource = null;

            AssetTreeVM ast_vm_cp = astMgr.cpAssetTreeTVWithChild(ast_vm);
            if (ast_vm_cp == null) return;

            tvi.ItemsSource = ast_vm_cp.child_list;
            tvi.IsExpanded = is_expand;

        }

        private TreeViewItem getTreeViewItem(AssetTreeVM ast_vm)
        {
            TreeViewItem tvi;

            //ftree는 자산의 경우만 해당하므로 그아래의 것들만 확인한다
            switch(ast_vm.type)
            {
                case AssetTreeType.SwitchCard:
                    //Card는 위에 slot이 있다
                    sw_card_config sw_c = g.sw_card_config_list.Find(at => at.sw_card_asset_id == ast_vm.asset_id);
                    if (sw_c == null) return null;
                    
                    //slot의 tvi를 가져온다
                    int sw_sl_index = favorite_ast_vm_list.FindIndex(at => at.asset_id == sw_c.sw_asset_id);
                    TreeViewItem sw_slot_tvi = (TreeViewItem)ftree.ItemContainerGenerator.ContainerFromIndex(sw_sl_index);
                    if (sw_slot_tvi == null) return null;

                    //tvi에 속해 있는 ast_vm 들을 list로 받아온다
                    List<AssetTreeVM> sw_card_list = sw_slot_tvi.ItemsSource as List<AssetTreeVM>;
                    if (sw_card_list.Count == 0) return null;

                    //card의 tvi를 가져온다
                    int sw_cd_index = sw_card_list.FindIndex(at => at.asset_id == ast_vm.asset_id);
                    tvi = (TreeViewItem)sw_slot_tvi.ItemContainerGenerator.ContainerFromIndex(sw_cd_index);

                    break;
                case AssetTreeType.Port:
                    //포트인 경우는 위에 Asset들이 있다
                    //포트가 연결된 Asset의 tvi를 찾는다
                    int ast_index = favorite_ast_vm_list.FindIndex(at => at.asset_id == ast_vm.asset_id);
                    TreeViewItem ast_tvi = (TreeViewItem)ftree.ItemContainerGenerator.ContainerFromIndex(ast_index);
                    if (ast_tvi == null)
                        return null;

                    List<AssetTreeVM> p_ast_vm_list = ast_tvi.ItemsSource as List<AssetTreeVM>;
                    if (p_ast_vm_list.Count == 0) return null;

                    //port의 인텍스를 찾아 port의 tvi를 찾는다  
                    int p_index = p_ast_vm_list.FindIndex(at => at.port_no == ast_vm.port_no);
                    tvi = (TreeViewItem)ast_tvi.ItemContainerGenerator.ContainerFromIndex(p_index);
                    break;
                //PC는 위에 Port와 Asset이 있다
                case AssetTreeType.PC:
                    //PC는 위에 포트가 있고 Asset이 있다 , 2단계가 있다
                    //먼저 asset_terminal을 가져와 연결된 asset의 정보와 포트를 확인한다
                    asset_terminal ast_t = g.asset_terminal_list.Find(at => at.terminal_asset_id == ast_vm.asset_id);
                    int ast_index2 = favorite_ast_vm_list.FindIndex(at => at.asset_id == ast_t.cur_outlet_asset_id);
                    TreeViewItem ast_tvi2 = (TreeViewItem)ftree.ItemContainerGenerator.ContainerFromIndex(ast_index2);

                    List<AssetTreeVM> p_ast_vm_list2 = ast_tvi2.ItemsSource as List<AssetTreeVM>;
                    if (p_ast_vm_list2.Count == 0) return null;

                    //port index로 port의 tvi를 찾는다
                    int p_index2 = p_ast_vm_list2.FindIndex(at => at.port_no == ast_vm.port_no);
                    TreeViewItem port_tvi = (TreeViewItem)ast_tvi2.ItemContainerGenerator.ContainerFromIndex(p_index2);
                    
                    List<AssetTreeVM> pc_ast_vm_list = port_tvi.ItemsSource as List<AssetTreeVM>;
                    if (pc_ast_vm_list.Count == 0) return null;

                    //pc asset_id로 pc의 tvi를 찾는다
                    int pc_index = pc_ast_vm_list.FindIndex(at => at.asset_id == ast_t.terminal_asset_id);
                    tvi = (TreeViewItem)port_tvi.ItemContainerGenerator.ContainerFromIndex(pc_index);

                    break;
                //나머지는 최상위 리스트에서 확인 가능하다
                default:
                    tvi = (TreeViewItem)ftree.ItemContainerGenerator.ContainerFromItem(ast_vm);
                    break;
            }
            if (tvi == null) return null;
            else return tvi;
        }

        private AssetTreeVM getPortAssetTreeVMinMD(int asset_id, int port_no)
        {
            if (g.asset_ast_vm_dic.ContainsKey(asset_id) == false) return null;
            AssetTreeVM ast_ast_vm = g.asset_ast_vm_dic[asset_id];

            return ast_ast_vm.child_list.Find(at => at.port_no == port_no);
        }

        #endregion

        public void getAndApplyAlarmList()
        {
            List<asset_ipp_port_link> al_list = g.asset_ipp_port_link_list.FindAll(at => (at.alarm_status == "P") || (at.alarm_status == "U"));

            foreach (var al in al_list)
            {
                if (g.asset_ast_vm_dic.ContainsKey(al.ipp_asset_id))
                {
                    changePortStatusView(al.ipp_asset_id, al.port_no, "/I2MS2;component/Icons/port_alarm_16.png", 1);
                    //changePortStatusView(port_vm, "/I2MS2;component/Icons/port_alarm_16.png", 1);
                }
            }

            List<ic_connect_status> ic_al_list = g.ic_connect_status_list.FindAll(at => at.ic_connect_status1 != "Y");
            foreach (var ic_al in ic_al_list)
            {
                changeIcStatusView(ic_al.ic_asset_id, true);
            }
            List<ipp_connect_status> ipp_al_list = g.ipp_connect_status_list.FindAll(at => (at.ic_asset_id != null) && (at.connect_status != "Y"));
            foreach (var ipp_al in ipp_al_list)
            {
                changeIppStatusView(ipp_al.ipp_asset_id, true);
            }
        }


        public void changePortStatusView(int ipp_asset_id, int port_no, String img_path, int on_alarm)
        {
            //ipp의 alarm_child_cnt 를 1 증가시키고 보여준다
            AssetTreeVM ipp_ast_vm_tv = favorite_ast_vm_list.Find(at => at.asset_id == ipp_asset_id);
            if (ipp_ast_vm_tv == null) return;

            if (on_alarm == 1)
            {
                ipp_ast_vm_tv.child_alarm_cnt++;
                ipp_ast_vm_tv.is_child_alarm_visible = Visibility.Visible;
                ipp_ast_vm_tv.force_changed = true;
            }
            else if (on_alarm == -1) 
            {
                ipp_ast_vm_tv.child_alarm_cnt--;
                if (ipp_ast_vm_tv.child_alarm_cnt<1)ipp_ast_vm_tv.is_child_alarm_visible = Visibility.Hidden;
                ipp_ast_vm_tv.force_changed = true;
            }

            TreeViewItem ipp_tvi = getTreeViewItem(ipp_ast_vm_tv);
            if (ipp_tvi == null) return;

            List<AssetTreeVM> port_ast_vm_tv_list = ipp_tvi.ItemsSource as List<AssetTreeVM>;
            if (port_ast_vm_tv_list.Count == 1) return;
     
            AssetTreeVM port_ast_vm = port_ast_vm_tv_list.Find(at => at.port_no == port_no);
            if (port_ast_vm == null) return;

            port_ast_vm.image_file_path = img_path;
            port_ast_vm.force_changed = true;
        }


        public void changeIppStatusView(int ipp_asset_id, Boolean up_down)
        {
            AssetTreeVM ipp_ast_vm_tv = favorite_ast_vm_list.Find(at => at.asset_id == ipp_asset_id);
            if (ipp_ast_vm_tv != null)
            {
                if (!up_down)
                {
                    ipp_ast_vm_tv.on_alarm = true;
                    ipp_ast_vm_tv.force_changed = true;
                }
                else
                {
                    ipp_ast_vm_tv.on_alarm = false;
                    ipp_ast_vm_tv.force_changed = true;
                }
            }
            else
            {
                //연결 정보가 있으면 ic 에 속해 있다
                //find ic
                ic_ipp_config ipp_cfg = g.ic_ipp_config_list.Find(at => at.ipp_asset_id == ipp_asset_id);
                if (ipp_cfg != null) return;

                AssetTreeVM ic_ast_vm_tv = favorite_ast_vm_list.Find(at => at.asset_id == ipp_cfg.ic_asset_id);
                if (ic_ast_vm_tv == null) return;

                if (!up_down)
                {
                    ic_ast_vm_tv.child_alarm_cnt++;
                    ic_ast_vm_tv.is_child_alarm_visible = Visibility.Visible;
                    ic_ast_vm_tv.force_changed = true;
                }
                else
                {
                    ic_ast_vm_tv.child_alarm_cnt--;
                    if (ic_ast_vm_tv.child_alarm_cnt < 1) ic_ast_vm_tv.is_child_alarm_visible = Visibility.Hidden;
                    ic_ast_vm_tv.force_changed = true;
                }


                //ipp tvi 찾아서 ipp도 동일하게 처리
                int index = favorite_ast_vm_list.FindIndex(at => at.asset_id == ic_ast_vm_tv.asset_id);
                TreeViewItem ic_tvi = (TreeViewItem)ftree.ItemContainerGenerator.ContainerFromIndex(index);
                if (ic_tvi == null) return;
                TreeViewItem ipp_tvi;

                int index2 = ic_ast_vm_tv.child_list.FindIndex(at => at.asset_id == ipp_asset_id);
                if (index2 < 0) return;
                ipp_tvi = (TreeViewItem)ic_tvi.ItemContainerGenerator.ContainerFromIndex(index2);
                if (ipp_tvi == null) return;

                ipp_ast_vm_tv = (AssetTreeVM)ipp_tvi.Header;
                if (ipp_ast_vm_tv == null) return;
                if (!up_down)
                {
                    ipp_ast_vm_tv.on_alarm = true;
                    ipp_ast_vm_tv.force_changed = true;
                }
                else
                {
                    ipp_ast_vm_tv.on_alarm = false;
                    ipp_ast_vm_tv.force_changed = true;
                }


            }


            
        }

        public void changeIcStatusView(int ic_asset_id, Boolean up_down)
        {
            AssetTreeVM ic_ast_vm = favorite_ast_vm_list.Find(at => at.asset_id == ic_asset_id);
            if (ic_ast_vm == null) return;

            if (!up_down)
            {
                ic_ast_vm.on_alarm = true;
                ic_ast_vm.force_changed = true;
            }
            else
            {
                ic_ast_vm.on_alarm = false;
                ic_ast_vm.force_changed = true;
            }
        }


    }
}
