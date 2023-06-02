using I2MS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WebApi.Models;



namespace I2MS2.Library
{
    // 트리뷰중 지능형 뷰 모델 
    public class IntelligentTreeViewManager
    {
        public TreeView itree;

        //TreeView에서 시작되는 레벨을 저장하는 곳
        AssetTreeViewManager astMgr;
        List<int[]> expandinfo_list = new List<int[]>();
        private List<AssetTreeVM> itree_vm_tv_list = new List<AssetTreeVM>();
        private Dictionary<int, AssetTreeVM> ipp_ast_vm_dic = new Dictionary<int, AssetTreeVM>();

        #region Init
        //초기화 함수
        public IntelligentTreeViewManager(TreeView _tv, int site_id)
        {
            //트리뷰 세팅
            itree = _tv;
            astMgr = new AssetTreeViewManager();

            itree_initData(site_id);
        }


        //site 변경에 따른 재초기화 메서드
        public void reInitItree(int site_id)
        {
            itree.ItemsSource = null;
            itree_vm_tv_list.Clear();
            ipp_ast_vm_dic.Clear();
            g.ic_ast_vm_dic.Clear();

            itree_initData(site_id);
        }

        private void itree_initData(int site_id)
        {
            //ic 를 트리 데이터에 추가
            List<asset> ic_ast_list = g.asset_list.FindAll(at => CatalogType.getCatalogType(at.catalog_id) == AssetTreeType.i_Controller);
            foreach (var ic_ast in ic_ast_list)
            {
                location tmp_l = g.location_list.Find(at => at.location_id == ic_ast.location_id);
                //현재 사이트에 해당하는 IC이면 itree에 추가한다
                if (tmp_l.site_id == site_id)
                {
                    AssetTreeVM ic_ast_vm = astMgr.makeAssetTreeVM(ic_ast);
                    if (ic_ast_vm == null)
                        return;
                    g.ic_ast_vm_dic.Add(ic_ast_vm.asset_id ?? 0,ic_ast_vm);
                    itree_vm_tv_list.Add(astMgr.cpAssetTreeTV(ic_ast_vm));
                }
            }


            //ipp를 트리 데이터에 추가
            List<asset> ipp_ast_list = g.asset_list.FindAll(at => CatalogType.getCatalogType(at.catalog_id) == AssetTreeType.i_PatchPanel);
            foreach (var ipp_ast in ipp_ast_list)
            {
                location ipp_l = g.location_list.Find(at => at.location_id == ipp_ast.location_id);
                //현재 사이트에 해당하는 ipp인지 확인한다
                if (ipp_ast.location_id == 0) // 빈 로케이션일 경우 다음으로 진행 , 밑으로 가면 에러남 // 2015.11.27    romee
                    continue;
                if ((ipp_l != null) && (ipp_l.site_id == site_id))
                {
                    AssetTreeVM l_ast_vm = g.location_ast_vm_dic[ipp_ast.location_id];
                    AssetTreeVM ipp_vm = l_ast_vm.child_list.Find(at => at.type_id == ipp_ast.asset_id);
                    AssetTreeVM ipp_vm_tv = astMgr.cpAssetTreeTV(ipp_vm);
                    ipp_ast_vm_dic.Add(ipp_vm.asset_id ?? 0, ipp_vm);

                    //IC와 연결정보가 있는경우 dic에 있는 IC 에서 찾아서 추가한다(Model에만 저장함)
                    ic_ipp_config ic_ipp_cfg = g.ic_ipp_config_list.Find(at => at.ipp_asset_id == ipp_ast.asset_id);
                    if (ic_ipp_cfg != null)
                    {
                        //AssetTreeVM ic_ast_vm = itree_vm_tv_list.Find(at => at.asset_id == ic_ipp_cfg.ic_asset_id);
                        //ic_ast_vm.child_list.Add(ipp_vm_tv);
                        //ic_ast_vm.is_expander_visible = Visibility.Visible;
                        //if (ipp_vm.child_alarm_cnt > 0)
                        //{
                        //    ic_ast_vm.child_alarm_cnt += ipp_vm.child_alarm_cnt;
                        //    ic_ast_vm.is_child_alarm_visible = Visibility.Visible;
                        //}
                        if (g.ic_ast_vm_dic.ContainsKey(ic_ipp_cfg.ic_asset_id))
                        {
                            AssetTreeVM ic_ast_vm = g.ic_ast_vm_dic[ic_ipp_cfg.ic_asset_id];
                            ic_ast_vm.child_list.Add(ipp_vm_tv);
                            ic_ast_vm.is_expander_visible = Visibility.Visible;

                            AssetTreeVM ic_ast_vm_tv = itree_vm_tv_list.Find(at => at.asset_id == ic_ipp_cfg.ic_asset_id);
                            ic_ast_vm_tv.is_expander_visible = Visibility.Visible;
                            if (ipp_vm.child_alarm_cnt > 0)
                            {
                                ic_ast_vm.child_alarm_cnt += ipp_vm.child_alarm_cnt;
                                ic_ast_vm.is_child_alarm_visible = Visibility.Visible;
                               
                            }
                            ic_ast_vm.force_changed = true;
                        }
                    }
                    //IC와 연결 정보가 없으면 itree에 바로 추가한다
                    else
                        itree_vm_tv_list.Add(ipp_vm_tv);
                
                }
            }

            itree.ItemsSource = itree_vm_tv_list;
            Refresh.Refresh_Controls(itree);
      //      getAndApplyExpandInfoList();
        
        } 
        #endregion

        #region Add, Del IC
        public void addIc(int ic_asset_id)
        {
            //1. make ic AssetTreeVM 
            asset ic_ast = g.asset_list.Find(at => at.asset_id == ic_asset_id);
            AssetTreeVM ic_vm = astMgr.makeAssetTreeVM(ic_ast);
            itree_vm_tv_list.Add(ic_vm);

            itree.ItemsSource = null;
            itree.ItemsSource = itree_vm_tv_list;
        }

        public void removeIc(int ic_asset_id)
        {
            //1. find ic
            AssetTreeVM ic_vm = itree_vm_tv_list.Find(at => at.asset_id == ic_asset_id);
            itree_vm_tv_list.Remove(ic_vm);

            itree.ItemsSource = null;
            itree.ItemsSource = itree_vm_tv_list;
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
            TreeViewItem ipp_tvi;

            //연결 정보가 있으면 ic 에 속해 있다
            //find ic
            ic_ipp_config ipp_cfg = g.ic_ipp_config_list.Find(at => at.ipp_asset_id == ipp_asset_id);
            if (ipp_cfg != null)
            {
                //ic ast_vm을 찾아서 alarm에 따라 child_alarm_cnt 처리
                AssetTreeVM ic_ast_vm_tv = itree_vm_tv_list.Find(at => at.asset_id == ipp_cfg.ic_asset_id);
                if (ic_ast_vm_tv == null) return;

                if (on_alarm == 1)
                {
                    ic_ast_vm_tv.child_alarm_cnt++;
                    ic_ast_vm_tv.is_child_alarm_visible = Visibility.Visible;
                    ic_ast_vm_tv.force_changed = true;
                }
                else if (on_alarm == -1)
                {
                    ic_ast_vm_tv.child_alarm_cnt--;
                    if (ic_ast_vm_tv.child_alarm_cnt < 1) ic_ast_vm_tv.is_child_alarm_visible = Visibility.Hidden;
                    ic_ast_vm_tv.force_changed = true;
                }


                //ipp tvi 찾아서 ipp도 동일하게 처리
                int index = itree_vm_tv_list.FindIndex(at => at.asset_id == ic_ast_vm_tv.asset_id);
                TreeViewItem ic_tvi = (TreeViewItem)itree.ItemContainerGenerator.ContainerFromIndex(index);
                if (ic_tvi == null) return;

                int index2 = ic_ast_vm_tv.child_list.FindIndex(at => at.asset_id == ipp_asset_id);
                if (index2 <0) return;
                ipp_tvi = (TreeViewItem)ic_tvi.ItemContainerGenerator.ContainerFromIndex(index2);
                if (ipp_tvi == null) return;
                
                AssetTreeVM ipp_ast_vm_tv = (AssetTreeVM)ipp_tvi.Header;
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
                    if (ipp_ast_vm_tv.child_alarm_cnt < 1) ipp_ast_vm_tv.is_child_alarm_visible = Visibility.Hidden;
                    ipp_ast_vm_tv.force_changed = true;
                }

            }
            else
            {
                AssetTreeVM ipp_ast_vm_tv = itree_vm_tv_list.Find(at=> at.asset_id ==ipp_asset_id);
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
                    if (ipp_ast_vm_tv.child_alarm_cnt < 1) ipp_ast_vm_tv.is_child_alarm_visible = Visibility.Hidden;
                    ipp_ast_vm_tv.force_changed = true;
                }
                //ipp tvi 찾아서  alarm에 따라 child_alarm_cnt 처리
                int index = itree_vm_tv_list.FindIndex(at => at.asset_id == ipp_asset_id);
                ipp_tvi = (TreeViewItem)itree.ItemContainerGenerator.ContainerFromIndex(index);
                if (ipp_tvi == null) return;
            }


            //port tvi 찾아서 동일하게 처리
            TreeViewItem port_tvi = (TreeViewItem)ipp_tvi.ItemContainerGenerator.ContainerFromIndex(port_no-1);
            if (port_tvi == null) return;

            AssetTreeVM port_ast_vm = (AssetTreeVM)port_tvi.Header;
            if (port_ast_vm == null) return;

            port_ast_vm.image_file_path = img_path;
            if (on_alarm == 1) port_ast_vm.on_alarm = true;
            else if (on_alarm == -1) port_ast_vm.on_alarm = false;
            port_ast_vm.force_changed = true;
            
        }

        public void changeIppStatusView(int ipp_asset_id, Boolean up_down)
        {
            TreeViewItem ipp_tvi;

            //연결 정보가 있으면 ic 에 속해 있다
            //find ic
            ic_ipp_config ipp_cfg = g.ic_ipp_config_list.Find(at => at.ipp_asset_id == ipp_asset_id);
            if(ipp_cfg!=null)
            {
                AssetTreeVM ic_ast_vm_tv = itree_vm_tv_list.Find(at => at.asset_id == ipp_cfg.ic_asset_id);
                if (ic_ast_vm_tv == null) return;

                if (up_down == false)
                {
                    ic_ast_vm_tv.child_alarm_cnt++;
                    ic_ast_vm_tv.is_child_alarm_visible = Visibility.Visible;
                    ic_ast_vm_tv.force_changed = true;
                }
                else if (up_down == true)
                {
                    ic_ast_vm_tv.child_alarm_cnt--;
                    if (ic_ast_vm_tv.child_alarm_cnt < 1) ic_ast_vm_tv.is_child_alarm_visible = Visibility.Hidden;
                    ic_ast_vm_tv.force_changed = true;
                }

                //ipp tvi 찾아서 ipp도 동일하게 처리
                int index = itree_vm_tv_list.FindIndex(at => at.asset_id == ic_ast_vm_tv.asset_id);
                TreeViewItem ic_tvi = (TreeViewItem)itree.ItemContainerGenerator.ContainerFromIndex(index);
                if (ic_tvi == null) return;

                int index2 = ic_ast_vm_tv.child_list.FindIndex(at => at.asset_id == ipp_asset_id);
                if (index2 < 0) return;
                ipp_tvi = (TreeViewItem)ic_tvi.ItemContainerGenerator.ContainerFromIndex(index2);
                if (ipp_tvi == null) return;

                AssetTreeVM ipp_ast_vm_tv = (AssetTreeVM)ipp_tvi.Header;
                if (ipp_ast_vm_tv == null) return;
                if (up_down == false)
                {
                    ipp_ast_vm_tv.on_alarm = true;
                    ipp_ast_vm_tv.force_changed = true;
                }
                else if (up_down == true)
                {
                    ipp_ast_vm_tv.on_alarm = false;
                    ipp_ast_vm_tv.force_changed = true;
                }


            }
            else
            {
                AssetTreeVM ipp_ast_vm = itree_vm_tv_list.Find(at => at.asset_id == ipp_asset_id);
                if (ipp_ast_vm == null) return;

                ipp_ast_vm.on_alarm = !up_down;
                ipp_ast_vm.force_changed = true;

            }
            //AssetTreeVM ipp_ast_vm = 
        }
        public void changeIcStatusView(int ic_asset_id, Boolean up_down)
        {
            AssetTreeVM ic_ast_vm = itree_vm_tv_list.Find(at => at.asset_id == ic_asset_id);
            if (ic_ast_vm == null) return;

            ic_ast_vm.on_alarm = !up_down;
            ic_ast_vm.force_changed = true;
        }


        #region Add,Del,Move IPP
        public void addIpp(int ipp_asset_id)
        {
            //1. make ipp AssetTreeVM
            asset ipp_ast = g.asset_list.Find(at => at.asset_id == ipp_asset_id);
            AssetTreeVM ipp_vm = makeIppAssetTree(ipp_ast);

            AssetTreeVM ipp_vm_tv = astMgr.cpAssetTreeTV(ipp_vm);

            //2. find and add to parent
            ic_ipp_config ic_ipp_cfg = g.ic_ipp_config_list.Find(at => at.ipp_asset_id == ipp_ast.asset_id);
            if (ic_ipp_cfg != null)
            {
                AssetTreeVM ic_vm = itree_vm_tv_list.Find(at => at.asset_id == ic_ipp_cfg.ic_asset_id);
                ic_vm.child_list.Add(ipp_vm_tv);
                ic_vm.is_expander_visible = Visibility.Visible;
                if (ipp_vm.child_alarm_cnt > 0)
                {
                    ic_vm.child_alarm_cnt += ipp_vm.child_alarm_cnt;
                    ic_vm.is_child_alarm_visible = Visibility.Visible;
                }

                Boolean is_expand = false;
                int index = itree_vm_tv_list.FindIndex(at => at.asset_id == ic_vm.asset_id);
                TreeViewItem ic_tvi = (TreeViewItem)itree.ItemContainerGenerator.ContainerFromIndex(index);
                if (ic_tvi == null) return;
                is_expand = ic_tvi.IsExpanded;
                ic_tvi.ItemsSource = null;
                ic_tvi.ItemsSource = ic_vm.child_list;
                ic_tvi.IsExpanded = is_expand;

            }
            else
            {
                itree_vm_tv_list.Add(ipp_vm_tv);
                itree.ItemsSource = null;
                itree.ItemsSource = itree_vm_tv_list;
            }
        }

        public void moveIpp(int ipp_asset_id)
        {
#if false

            //1. find vm
            AssetTreeVM ipp_vm = ipp_ast_vm_dic[ipp_asset_id];

            //2. find ic
            foreach (AssetTreeVM ic_vm in itree_vm_tv_list)
            {
                if (ic_vm.child_list.Exists(at => at.asset_id == ipp_asset_id))
                {
                    //3. remove vm
                    ic_vm.child_list.Remove(ipp_vm);
                    break;
                }
            }

            //4. add vm
            ic_ipp_config ic_ipp_cfg = g.ic_ipp_config_list.Find(at => at.ipp_asset_id == ipp_vm.asset_id);

            foreach (AssetTreeVM ic_vm in itree_vm_tv_list)
            {
                if (ic_vm.asset_id == ic_ipp_cfg.ic_asset_id)
                {
                    //3. remove vm
                    ic_vm.child_list.Add(ipp_vm);
                    if (ipp_vm.child_alarm_cnt > 0)
                    {
                        ic_vm.child_alarm_cnt += ipp_vm.child_alarm_cnt;
                        ic_vm.is_child_alarm_visible = Visibility.Visible;
                    }
                    break;
                }
            }

            itree.ItemsSource = null;
            itree.ItemsSource = itree_vm_tv_list; 
#else
            //1.itree 초기화
            itree.ItemsSource = null;

            //2. 현재 트리에서 ipp_vm을 찾아서 지워준다
            AssetTreeVM ipp_vm_tv = itree_vm_tv_list.Find(at => at.asset_id == ipp_asset_id);
            if(ipp_vm_tv!=null)
            {
                itree_vm_tv_list.Remove(ipp_vm_tv);
            }
            else
            {
                foreach(var ic_vm_tv_before in itree_vm_tv_list)
                {
                    if (ic_vm_tv_before.type == AssetTreeType.i_Controller)
                    {
                        if (ic_vm_tv_before.child_list.Count > 0)
                        {
                            foreach (var tmp_ipp_vm_tv in ic_vm_tv_before.child_list)
                            {
                                if (tmp_ipp_vm_tv.asset_id == ipp_asset_id)
                                {
                                    ipp_vm_tv = tmp_ipp_vm_tv;
                                    ic_vm_tv_before.child_list.Remove(ipp_vm_tv);

                                    if (ipp_vm_tv.child_alarm_cnt > 0)
                                    {
                                        ic_vm_tv_before.child_alarm_cnt -= ipp_vm_tv.child_alarm_cnt;
                                        if (ipp_vm_tv.on_alarm)
                                            ic_vm_tv_before.child_alarm_cnt -= 1;

                                        if (ic_vm_tv_before.child_alarm_cnt <= 0)
                                        {
                                            ic_vm_tv_before.child_alarm_cnt = 0;
                                            ic_vm_tv_before.is_child_alarm_visible = Visibility.Hidden;
                                        }
                                    }

                                    if (ic_vm_tv_before.child_list.Count == 0)
                                        ic_vm_tv_before.is_expander_visible = Visibility.Hidden;

                                }
                            }
                        }
                    }
                }
            }


            if (ipp_vm_tv == null)
                return;

            //3. ipp_vm_tv_before가 들어갈 자리를 찾아서 넣어 준다
            ic_ipp_config ic_ipp_cfg = g.ic_ipp_config_list.Find(at => at.ipp_asset_id == ipp_asset_id);
            if (ic_ipp_cfg != null)
            {
                AssetTreeVM ic_vm = itree_vm_tv_list.Find(at => at.asset_id == ic_ipp_cfg.ic_asset_id);
                ic_vm.child_list.Add(ipp_vm_tv);
                ic_vm.is_expander_visible = Visibility.Visible;
                if (ipp_vm_tv.child_alarm_cnt > 0)
                {
                    ic_vm.child_alarm_cnt += ipp_vm_tv.child_alarm_cnt;
                    ic_vm.is_child_alarm_visible = Visibility.Visible;
                }
            }
            else
            {
                itree_vm_tv_list.Add(ipp_vm_tv);
            }

            itree.ItemsSource = itree_vm_tv_list;
#endif
        }

        public void removeIpp(int ipp_asset_id)
        {
            //1. find and remove to parent
            ic_ipp_config ic_ipp_cfg = g.ic_ipp_config_list.Find(at => at.ipp_asset_id == ipp_asset_id);
            if (ic_ipp_cfg != null)
            {
                AssetTreeVM ic_vm = itree_vm_tv_list.Find(at => at.asset_id == ic_ipp_cfg.ic_asset_id);
                if (ic_vm == null)
                    return;

                AssetTreeVM ipp_vm = ic_vm.child_list.Find(at => at.asset_id == ipp_asset_id);
                if (ipp_vm == null)
                    return;


                try
                {
                    //트리뷰에서 ic를 찾아서 null로 만들어 준다
                    int index = itree_vm_tv_list.FindIndex(at => at.asset_id == ic_vm.asset_id);
                    TreeViewItem ic_tvi = (TreeViewItem)itree.ItemContainerGenerator.ContainerFromIndex(index);
                    Boolean is_expand = ic_tvi.IsExpanded;
                    ic_tvi.ItemsSource = null;
                    

                    //ic_vm에서 ipp를 삭제 해준다
                    ic_vm.child_list.Remove(ipp_vm);
                    if (ipp_vm.child_alarm_cnt > 0)
                    {
                        ic_vm.child_alarm_cnt -= ipp_vm.child_alarm_cnt;
                        if (ipp_vm.on_alarm)
                            ic_vm.child_alarm_cnt -= 1;

                        if (ic_vm.child_alarm_cnt <= 0)
                        {
                            ic_vm.child_alarm_cnt = 0;
                            ic_vm.is_child_alarm_visible = Visibility.Hidden;
                        }
                    }
                    
                    ipp_ast_vm_dic.Remove(ipp_asset_id);
                    if (ic_vm.child_list.Count == 0)
                        ic_vm.is_expander_visible = Visibility.Hidden;


                    ic_tvi.ItemsSource = ic_vm.child_list;
                    //ic_tvi.IsExpanded = is_expand;
                }
                catch (Exception) { }
            }
            else
            {
                AssetTreeVM ipp_vm_tv = itree_vm_tv_list.Find(at => at.asset_id == ipp_asset_id);
                itree_vm_tv_list.Remove(ipp_vm_tv);

                ipp_ast_vm_dic.Remove(ipp_asset_id);
                try
                {
                    itree.ItemsSource = null;
                }
                catch (Exception) { }
                itree.ItemsSource = itree_vm_tv_list;
            }


        }

        #endregion


        #region 확장, 축소에 대한 처리

        //축소 이벤트 발생시 호출
        public void unExpandTree(AssetTreeVM ast_vm_item)
        {
            TreeViewItem tvi = getTreeViewItem(ast_vm_item);
            if(tvi==null) return;
                tvi.IsExpanded = false;
                tvi.ItemsSource = null;
              
            removeExpandState(ast_vm_item);
        }
        //확대 이벤트 발생시 호출
        public void expandTree(AssetTreeVM ast_vm_item)
        {
#if false
            //컨트롤러인 경우 해당 레벨만 확장한다
            if (ast_vm_item.type == AssetTreeType.i_Controller)
            {
                // if (ast_vm_item.child_list.Count <= 0) return;

                int index = itree_vm_tv_list.FindIndex(at => at.asset_id == ast_vm_item.asset_id);

                TreeViewItem tvi = (TreeViewItem)itree.ItemContainerGenerator.ContainerFromIndex(index);
                if (tvi == null) return;

                //연결된 ipp를 찾는다
                if (g.ic_ast_vm_dic.ContainsKey(ast_vm_item.asset_id ?? 0) == false) return;
                AssetTreeVM ic_ast_vm_with_child = astMgr.cpAssetTreeTVWithChild(g.ic_ast_vm_dic[ast_vm_item.asset_id ?? 0]);

                tvi.ItemsSource = ic_ast_vm_with_child.child_list;
                tvi.IsExpanded = true;
                Refresh.Refresh_Controls(itree);

                addExpandState(ast_vm_item);
            }
            else
            {
                //Dic 에서 ipp_vm을 가져온다(child가 여기에는 포함되어 있다)
                AssetTreeVM ipp_vm = ipp_ast_vm_dic[ast_vm_item.type_id];
                ic_ipp_config ipp_cfg = g.ic_ipp_config_list.Find(at => at.ipp_asset_id == ipp_vm.type_id);
                //연결 정보가 있으면 ic를 먼저찾고 아니면 해당 tvi를 찾는다
                if (ipp_cfg != null)
                {
                    //ic 의 tvi를 찾아 온다
                    if (g.ic_ast_vm_dic.ContainsKey(ipp_cfg.ic_asset_id) == false) return;
                    AssetTreeVM ic_vm = g.ic_ast_vm_dic[ipp_cfg.ic_asset_id];
                    int index = itree_vm_tv_list.FindIndex(at => at.asset_id == ic_vm.asset_id);

                    TreeViewItem ic_tvi = (TreeViewItem)itree.ItemContainerGenerator.ContainerFromIndex(index);
                    if (ic_tvi == null) return;
                    int ipp_index = ic_vm.child_list.FindIndex(at => at.asset_id == ipp_vm.asset_id);

                    // Refresh.Refresh_Controls(itree);
                    itree.Dispatcher.Invoke(new Action(() => { }), DispatcherPriority.ContextIdle, null);


                    //TreeViewItem ipp_tvi = findChildItem(ic_tvi, ipp_index);
                    TreeViewItem ipp_tvi = (TreeViewItem)ic_tvi.ItemContainerGenerator.ContainerFromIndex(ipp_index);
                    if (ipp_tvi == null) return;
                    AssetTreeVM ipp_vm_tv = astMgr.cpAssetTreeTVWithChild(ipp_vm);
                    ipp_tvi.ItemsSource = ipp_vm_tv.child_list;
                    ipp_tvi.IsExpanded = true;
                    //Refresh.Refresh_Controls(itree);

                    //현제까지 적용된 UI를 강재로 Action 시킨다
                    itree.Dispatcher.Invoke(new Action(() => { }), DispatcherPriority.ContextIdle, null);

                    addExpandState(ast_vm_item);
                }
                else
                {
                    //연결된 IC가 없는 경우 바로 ipp찾아서 바꿔준다
                    int ipp_index = itree_vm_tv_list.FindIndex(at => at.asset_id == ipp_vm.asset_id);
                    TreeViewItem ipp_tvi = (TreeViewItem)itree.ItemContainerGenerator.ContainerFromIndex(ipp_index);
                    if (ipp_tvi == null) return;
                    AssetTreeVM ipp_vm_tv = astMgr.cpAssetTreeTVWithChild(ipp_vm);

                    ipp_tvi.ItemsSource = ipp_vm_tv.child_list;
                    ipp_tvi.IsExpanded = true;
                    Refresh.Refresh_Controls(itree);

                    addExpandState(ast_vm_item);
                }

            }
#else
            AssetTreeVM ast_vm = getAndCpAssetTreeVmWithChild(ast_vm_item);
            if (ast_vm == null) return;

            TreeViewItem tvi = getTreeViewItem(ast_vm_item);
            if (tvi == null) return;

            tvi.ItemsSource = ast_vm.child_list;
            tvi.IsExpanded = true;
            addExpandState(ast_vm_item);


                        
            //Refresh.Refresh_Controls(itree);

            
#endif

        }


        private AssetTreeVM getAndCpAssetTreeVmWithChild(AssetTreeVM ast_vm)
        {
            AssetTreeVM ast_vm_md = getAssetTreeVMinMD(ast_vm);
            return astMgr.cpAssetTreeTVWithChild(ast_vm_md);
        }

        private AssetTreeVM getAssetTreeVMinMD(AssetTreeVM ast_vm)
        {
            if (ast_vm.type == AssetTreeType.i_Controller)
            {
                if (g.ic_ast_vm_dic.ContainsKey(ast_vm.asset_id ?? 0) == false) return null;
                return g.ic_ast_vm_dic[ast_vm.asset_id ?? 0];
            }
            else if (ast_vm.type == AssetTreeType.i_PatchPanel)
            {
                if (ipp_ast_vm_dic.ContainsKey(ast_vm.asset_id ?? 0) == false) return null;
                return ipp_ast_vm_dic[ast_vm.asset_id ?? 0];
            }
            else if (ast_vm.type == AssetTreeType.Port)
            {
                if (ipp_ast_vm_dic.ContainsKey(ast_vm.asset_id ?? 0) == false) return null;
                AssetTreeVM ipp_ast_vm = ipp_ast_vm_dic[ast_vm.asset_id ?? 0];

                return ipp_ast_vm.child_list.Find(at => at.port_no == ast_vm.port_no);
            }
            else
                return null;

        }

        List<int[]> init_expandinfo_list;
        public void getAndApplyExpandInfoList()
        {
            init_expandinfo_list = new List<int[]>();
            if (expandinfo_list.Count != 0) return;
            List<int[]> tmp_list = Reg.get_asset_tree("simplewin_tree");
            foreach (var exp in tmp_list)
            {
                if (g.ic_ast_vm_dic.ContainsKey(exp[1]))//ic
                {
                    AssetTreeVM ic_ast_vm = g.ic_ast_vm_dic[exp[1]];
                    Boolean ret = expandTreeCustom(ic_ast_vm);
                    if (!ret)
                        init_expandinfo_list.Add(exp);
                }
                else if(ipp_ast_vm_dic.ContainsKey(exp[1]))//ipp
                {
                    if(exp[2]==0)//ipp
                    {
                        AssetTreeVM ipp_ast_vm = ipp_ast_vm_dic[exp[1]];

                        AssetTreeVM ic_ast_vm = getParentICAssetTreeVM(ipp_ast_vm);
                        if (ic_ast_vm != null)
                            expandTreeCustom(ic_ast_vm);
                        

                        Boolean ret2 = expandTreeCustom(ipp_ast_vm);
                        if (!ret2)
                            init_expandinfo_list.Add(exp);
                      
                    }
                    else//port
                    {

                    }
                }
            }
        }

        //데이터를 설정하여 확장을 진행한다
        private Boolean expandTreeCustom(AssetTreeVM ast_vm)
        {
            TreeViewItem tvi = getTreeViewItem(ast_vm);
            if (tvi == null) return false;
            AssetTreeVM ast_vm_tv = (AssetTreeVM)tvi.Header;
            if (ast_vm_tv == null) return false;
            if (ast_vm_tv.is_expanded == false)
            {
                ast_vm_tv.is_expanded = true;
                ast_vm_tv.force_changed = true;
            }

            //if (tvi.ItemContainerGenerator.Status != System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
            //{
            //    tvi.ItemContainerGenerator.StatusChanged += ItemContainerGenerator_ChildStatusChanged_ForExpand;
            //    expandTree(ast_vm);
            //}
            //else
                expandTree(ast_vm);
            return true;
        }
        
        private TreeViewItem getTreeViewItem(AssetTreeVM ast_vm)
        {
            if(ast_vm.type==AssetTreeType.i_Controller)
            {
                int index = itree_vm_tv_list.FindIndex(at => at.asset_id == ast_vm.asset_id);
                TreeViewItem tvi = (TreeViewItem)itree.ItemContainerGenerator.ContainerFromIndex(index);
                return tvi;
            }
            //else if ((ast_vm.type == AssetTreeType.i_PatchPanel) || (ast_vm.type == AssetTreeType.Port))
            else //ipp, port인 경우
            {
                //ipp, port 모두 asset_id는 동일한 ipp asset_id 이므로.....
                if (ipp_ast_vm_dic.ContainsKey(ast_vm.asset_id ?? 0) == false) return null;
                AssetTreeVM ipp_vm = ipp_ast_vm_dic[ast_vm.asset_id ?? 0];

                TreeViewItem ipp_tvi;
                ic_ipp_config ipp_cfg = g.ic_ipp_config_list.Find(at => at.ipp_asset_id == ipp_vm.asset_id);
                //연결 정보가 있으면 ic를 먼저찾고 아니면 해당 tvi를 찾는다
                if (ipp_cfg != null)
                {
                    //ic 의 tvi를 찾아 온다
                    //AssetTreeVM ic_vm = itree_vm_tv_list.Find(at => at.asset_id == ipp_cfg.ic_asset_id);
                    if (g.ic_ast_vm_dic.ContainsKey(ipp_cfg.ic_asset_id) == false) return null;
                    AssetTreeVM ic_vm = g.ic_ast_vm_dic[ipp_cfg.ic_asset_id];
                    int index = itree_vm_tv_list.FindIndex(at => at.asset_id == ic_vm.asset_id);

                    TreeViewItem ic_tvi = (TreeViewItem)itree.ItemContainerGenerator.ContainerFromIndex(index);
                    if (ic_tvi == null) return null;
                    int ipp_index = ic_vm.child_list.FindIndex(at => at.asset_id == ipp_vm.asset_id);

                    ipp_tvi = (TreeViewItem)ic_tvi.ItemContainerGenerator.ContainerFromIndex(ipp_index);
                    if(ipp_tvi==null)
                    {
                   //     ic_tvi.ItemContainerGenerator.StatusChanged += ItemContainerGenerator_ParentStatusChanged_ForExpand;
                    }

                    if (ast_vm.type == AssetTreeType.i_PatchPanel)
                        return ipp_tvi;
                }
                else
                {
                    //연결된 IC가 없는 경우 바로 ipp찾아서 바꿔준다
                    int ipp_index = itree_vm_tv_list.FindIndex(at => at.asset_id == ipp_vm.asset_id);
                    ipp_tvi = (TreeViewItem)itree.ItemContainerGenerator.ContainerFromIndex(ipp_index);
                    if (ast_vm.type == AssetTreeType.i_PatchPanel)
                        return ipp_tvi;
                }
                //Port인 경우는 추가로 포트를 찾아서 처리해준다
                int port_index = ipp_vm.child_list.FindIndex(at => at.port_no == ast_vm.port_no);
                TreeViewItem port_tvi = (TreeViewItem)ipp_tvi.ItemContainerGenerator.ContainerFromIndex(port_index);
                return port_tvi;
            }
           
        }

        void ItemContainerGenerator_ParentStatusChanged_ForExpand(object sender, EventArgs e)
        {
            ItemContainerGenerator gr = (ItemContainerGenerator)sender;

            if (gr.Status != System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated) return;
            foreach (var item in gr.Items)
            {
                if (!(item is AssetTreeVM)) return;

                AssetTreeVM ipp_vm = (AssetTreeVM)item;
                int[] exp = init_expandinfo_list.Find(at => (at[1] == ipp_vm.asset_id) && (at[2] == 0));
                if (exp != null)
                {
                    //IPP tvi가 준비되면 확장시키도록 이벤트를 만든다
                    TreeViewItem ipp_tvi = getTreeViewItem(ipp_vm);
                    if (ipp_tvi != null)
                    {
                        expandTreeCustom(ipp_vm);
                        //ipp_vm.is_expanded = true;
                        //ipp_vm.force_changed = true;
                        //ipp_tvi.ItemContainerGenerator.StatusChanged += ItemContainerGenerator_ChildStatusChanged_ForExpand;
                        init_expandinfo_list.Remove(exp);
                        //addExpandState(ipp_vm);
                    }
                }
            }

        }

        //ipp가 준비되면 확장되는 이벤트
        void ItemContainerGenerator_ChildStatusChanged_ForExpand(object sender, EventArgs e)
        {
            ItemContainerGenerator gr = (ItemContainerGenerator)sender;
            if (gr.Status != System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated) return;


            if (gr.Items[0] is AssetTreeVM)
            {
                AssetTreeVM ast_child_vm = (AssetTreeVM)gr.Items[0];
                AssetTreeVM ast_vm = ast_child_vm.parant_ast_vm;
                TreeViewItem tvi = getTreeViewItem(ast_vm);
                //tvi.ItemContainerGenerator.StatusChanged -= ItemContainerGenerator_ChildStatusChanged_ForExpand;

                Refresh.Refresh_Controls(itree);
            }
        }

        // 확장 정보 레지스트리 저장 
        private void addExpandState(AssetTreeVM ast_vm)
        {
            if(itree_vm_tv_list.Exists(at=> (at.asset_id ==ast_vm.asset_id)&&(at.port_no == 0)))
            {
                //port는 아니면서 최상위 리스트에 있는 것들
                int[] tmp_exp_info = new int[3];
                tmp_exp_info[0] = ast_vm.location_id;
                tmp_exp_info[1] = ast_vm.asset_id ?? 0;
                tmp_exp_info[2] = 0;

                if (expandinfo_list.Exists(at => (at[0] == tmp_exp_info[0]) && (at[0] == tmp_exp_info[0]) && (at[0] == tmp_exp_info[0]))
                    == false)
                    expandinfo_list.Add(tmp_exp_info);
                updateExpandInfoReg();
                
            }
            else if(ast_vm.type== AssetTreeType.i_PatchPanel)
            {
                //ic 의 확장정보는 제거한다
                AssetTreeVM ic_ast_vm = getParentICAssetTreeVM(ast_vm);
                if (ic_ast_vm == null) return;

                int[] ic_exp = expandinfo_list.Find(at => (at[1] == ic_ast_vm.asset_id) && (at[2] == 0));
                if (ic_exp != null) 
                    expandinfo_list.Remove(ic_exp);

                //해당 타입의 확장 정보를 추가
                int[] tmp_exp_info = new int[3];
                tmp_exp_info[0] = ast_vm.location_id;
                tmp_exp_info[1] = ast_vm.asset_id ?? 0;
                tmp_exp_info[2] = 0;
                if (expandinfo_list.Exists(at => (at[0] == tmp_exp_info[0]) && (at[1] == tmp_exp_info[1]) && (at[2] == tmp_exp_info[2]))
                    == false)
                    expandinfo_list.Add(tmp_exp_info);
                
               updateExpandInfoReg();
            }
            else if(ast_vm.type==AssetTreeType.Port)
            {
                //ipp의 확장정보 제거
                if (ipp_ast_vm_dic.ContainsKey(ast_vm.asset_id ?? 0) == false) return;
                AssetTreeVM ipp_ast_vm = ipp_ast_vm_dic[ast_vm.asset_id ?? 0];
                if (ipp_ast_vm == null) return;

                int[] ipp_exp = expandinfo_list.Find(at => (at[1] == ipp_ast_vm.asset_id) && (at[2] == 0));
                if (ipp_exp != null) return;
                    expandinfo_list.Remove(ipp_exp);
                
                //해당 타입의 확장 정보를 추가
                int[] tmp_exp_info = new int[3];
                tmp_exp_info[0] = ast_vm.location_id;
                tmp_exp_info[1] = ast_vm.asset_id ?? 0;
                tmp_exp_info[2] = ast_vm.port_no;

                if (expandinfo_list.Exists(at => (at[0] == tmp_exp_info[0]) && (at[1] == tmp_exp_info[1]) && (at[2] == tmp_exp_info[2]))
                    == false) 
                    expandinfo_list.Add(tmp_exp_info);

                updateExpandInfoReg();
            }

        }


        // 확장 정보 레지스트리 변경
        private void removeExpandState(AssetTreeVM ast_vm)
        {
            if(ast_vm.type==AssetTreeType.i_Controller)
            {
                int[] ic_exp = expandinfo_list.Find(at => (at[0] == ast_vm.location_id) && (at[1] == ast_vm.asset_id) && (at[2] == 0));
                if (ic_exp != null)
                    expandinfo_list.Remove(ic_exp);

                List<ic_ipp_config> ipp_cfg_list = g.ic_ipp_config_list.FindAll(at => at.ic_asset_id == ast_vm.asset_id);
                foreach(var ipp_cfg in ipp_cfg_list)
                {
                    if(ipp_ast_vm_dic.ContainsKey(ipp_cfg.ipp_asset_id ?? 0))
                    {
                        AssetTreeVM ipp_ast_vm = ipp_ast_vm_dic[ipp_cfg.ipp_asset_id ?? 0];
                        List<int[]> exp_list = expandinfo_list.FindAll(at =>
                            (at[0] == ipp_ast_vm.location_id) && (at[1] == ipp_ast_vm.asset_id) && (at[2] == 0));

                        foreach(var exp in exp_list)
                        {
                            expandinfo_list.Remove(exp);
                        }
                    }
                }
                updateExpandInfoReg();
            }
            else if(ast_vm.type==AssetTreeType.i_PatchPanel)
            {
                List<int[]> exp_list = expandinfo_list.FindAll(at =>
                            (at[0] == ast_vm.location_id) && (at[1] == ast_vm.asset_id) && (at[2] == 0));

                foreach (var exp in exp_list)
                {
                    expandinfo_list.Remove(exp);
                }
              
                //ic가 열려있으므로 ic에 대한 확장 정보를 추가해야 한다
                AssetTreeVM ic_ast_vm = getParentICAssetTreeVM(ast_vm);
                if(ic_ast_vm!=null)
                {
                    if(expandinfo_list.Exists(at=> 
                        (at[0]==ic_ast_vm.location_id)&&(at[1]==ic_ast_vm.asset_id)&&(at[2]==0))==false)
                    {
                        int[] tmp_exp_info = new int[3];
                        tmp_exp_info[0] = ast_vm.location_id;
                        tmp_exp_info[1] = ast_vm.asset_id ?? 0;
                        tmp_exp_info[2] = ast_vm.port_no;
                        expandinfo_list.Add(tmp_exp_info);
                    }
                }
                updateExpandInfoReg();
            }
        }

        // 레지스트리 저장 
        private void updateExpandInfoReg()
        {
            Boolean ret = Reg.save_tree("simplewin_tree", expandinfo_list);
        }

        // 패치패널 뷰모델에서 컨트롤러 뷰모데 얻기  
        private AssetTreeVM getParentICAssetTreeVM(AssetTreeVM ast_vm)
        {
            ic_ipp_config ipp_cfg = g.ic_ipp_config_list.Find(at => at.ipp_asset_id == ast_vm.asset_id);
            if (ipp_cfg == null) return null;

            //ic 의 tvi를 찾아 온다
            return itree_vm_tv_list.Find(at => at.asset_id == ipp_cfg.ic_asset_id);
        }

        // 아울렛의 차일드 얻기        
        private List<AssetTreeVM> getCpChild(AssetTreeVM p_ast_vm)
        {
            if(p_ast_vm.child_list.Count ==0)
                return null;

            List<AssetTreeVM> ast_vm_list = new List<AssetTreeVM>();
            foreach(var ast_vm in p_ast_vm.child_list)
            {
                ast_vm_list.Add(astMgr.cpAssetTreeTV(ast_vm));
            }
            return ast_vm_list;
        }

     
        #endregion

        // 지능형 패치패널 및 포트 뷰 모델 만들기 -> ipp_dic 에 저장 
        private AssetTreeVM makeIppAssetTree(asset ipp_ast)
        {
            AssetTreeVM ipp_ast_vm = astMgr.makeAssetTreeVM(ipp_ast);
            ipp_ast_vm.is_expander_visible = Visibility.Visible;
           
            int alarm_cnt = 0;
            //child port를 만든다
            List<asset_ipp_port_link> ipp_p_list = g.asset_ipp_port_link_list.FindAll(at => at.ipp_asset_id == ipp_ast.asset_id);
            foreach (var ipp_p in ipp_p_list)
            {
                AssetTreeVM port_ast_vm = new AssetTreeVM()
                {
                    asset_id = ipp_ast.asset_id,
                    location_id = ipp_ast.location_id,
                    asset_tree_id = 0,
                    disp_name = String.Format("Port{0}", ipp_p.port_no),
                    disp_level = 0,
                    disp_order = 0,
                    is_expander_visible = Visibility.Hidden,
                    is_expanded = false,
                    check_view = Visibility.Hidden,
                    //image_file_path = string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH, "icon_16", temp_image.file_name),
                    image_file_path = "/I2MS2;component/Icons/port_16.png",
                    port_no = ipp_p.port_no,
                    is_child_alarm_visible = Visibility.Hidden,
                    child_alarm_cnt = 0,
                    on_alarm = false,

                    //type = getAssetTreeTypeLevel(ast),
                    type = AssetTreeType.Port,
                    type_id = ipp_p.asset_ipp_port_link_id
                };

                if ((ipp_p.alarm_status == "U") || (ipp_p.alarm_status == "P"))
                {
                    port_ast_vm.image_file_path = "/I2MS2;component/Icons/port_alarm_16.png";
                    port_ast_vm.on_alarm = true;
                    alarm_cnt++;
                }
                else if ((ipp_p.ipp_port_status == "P") || (ipp_p.ipp_port_status == "L"))
                {
                    port_ast_vm.image_file_path = "/I2MS2;component/Icons/port_on_16.png";
                    
                }
                ipp_ast_vm.child_list.Add(port_ast_vm);
            }

            if (alarm_cnt > 0)
            {
                ipp_ast_vm.is_child_alarm_visible = Visibility.Visible;
                ipp_ast_vm.child_alarm_cnt = alarm_cnt;
            }

            ipp_ast_vm_dic.Add(ipp_ast_vm.asset_id ?? 0, ipp_ast_vm);

            return ipp_ast_vm;
        }

        //TreeViewItem 아래에 있는 TreeViewItem을 index로 찾아준다
        private TreeViewItem findChildItem(TreeViewItem tvi, int id)
        {
            TreeViewItem item = null;

            try
            {
                item = (TreeViewItem)tvi.ItemContainerGenerator.ContainerFromIndex(id);
            }
            catch (Exception) { }
            return item;
        }

        public AssetTreeVM getIpp(int ipp_asset_id)
        {
            return ipp_ast_vm_dic[ipp_asset_id];

        }

    }
}
