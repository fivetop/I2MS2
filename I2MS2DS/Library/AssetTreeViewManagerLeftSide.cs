//#define USE_PREV_NEXT

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WebApi.Models;
using I2MS2.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Input;


namespace I2MS2.Library
{
    // 트리뷰중 자산관리 뷰 모델 
    public class AssetTreeViewManagerLeftSide
    {
        //asset_tree의 데이터 연결 노드를 저장하는 곳
        //private List<tree_node> tn = new List<tree_node>();
        //TreeView에서 사용가능하도록 asset_tree를 가공하여 저장하는 곳
       // private List<asset_tree_for_treeview>[] asset_tree_lv = new List<asset_tree_for_treeview>[8];
        //private List<AssetTreeVM> asset_tree_vm_list = new List<AssetTreeVM>();
        //Dictionary<int, AssetTreeVM> ast_vm_dic = new Dictionary<int, AssetTreeVM>();

        public TreeView l_tv; //보여질 TreeView
        private int base_level; //TreeView에서 시작되는 레벨을 저장하는 곳
        private List<AssetTreeVM> asset_tree_vm_tv_list = new List<AssetTreeVM>(); // 뷰 모델
        AssetTreeViewManager astMgr; // 데이터 모델
        private bool isAlarmCheck = false;

        #region 초기화 부분
        //초기화 함수
        public AssetTreeViewManagerLeftSide(TreeView _l_tv)
        {
            //트리뷰 세팅
            l_tv = _l_tv;
            astMgr = new AssetTreeViewManager();

        //    initTreeViewData();
        //    initLoacaionTree(site_id);
        }
        // 트리뷰 초기화 처리 
        public void InitAssetTreeViewManager(int site_id)
        {
            initTreeViewData();         // 트리 모든 데이터 들어옴 , 모델 생성 완료 
            initLoacaionTree(site_id);  // 사이트 하나만 넘겨주고 처리  
        }
        
        //site가 변경되어 treeView를 다시 초기화 하는 경우 호출
        public void reInitAssetTreeViewManager(int site_id)
        {
            l_tv.ItemsSource = null;            // 클리어 한다음 재 초기화 한다. 
            asset_tree_vm_tv_list.Clear();
            initLoacaionTree(site_id);
        }

        
        // asset_tree데이터를 가지고 treeView에서 사용할수 있는 형태로 가공하여 관리한다
        // 검색을 위한 딕셔너리 처리 
         public void initTreeViewData()
        {
            AssetTreeVM ast_vm;
            AssetTreeVM p_ast_vm;

            foreach(asset_tree ast in g.asset_tree_list.OrderBy(p => p.disp_order).OrderBy(p => p.disp_level))
            {
                //AssetTreeVM으로 만든다
                ast_vm = astMgr.makeAssetTreeVM(ast);


                //첫번째 배열이면 md_list에 추가한다 region 1 추가 
                if (ast_vm.disp_level -1 == 0)
                {
                    g.asset_tree_vm_list.Add(ast_vm);
                    g.location_ast_vm_dic.Add(ast_vm.location_id, ast_vm);
                }
                //첫번째 배열이 아니면 무조건 부모가 있다
                else
                {
                    if(ast_vm.asset_tree_id == 19209993)
                    {
                        Console.WriteLine("aaa");
                    }

                    //asset_id가 없는 경우는 location이다. g.location_ast_vm_dic  에 저장됨
                    if(ast_vm.asset_id==null)
                    {
                        p_ast_vm = astMgr.getParentAssetTreeVMinMD(ast_vm);

                        if(p_ast_vm!=null)
                        {
                            ast_vm.parant_ast_vm = p_ast_vm;

                            p_ast_vm.child_list.Add(ast_vm);
                            p_ast_vm.is_expander_visible = Visibility.Visible;

                            if (!(g.location_ast_vm_dic.ContainsKey(ast_vm.location_id)))
                                g.location_ast_vm_dic.Add(ast_vm.location_id, ast_vm);  // 룸까지 저장 처리 
                        }
                        
                    }
                    //asset_id를 가진경우는 지역이 아니라 자산이다 g.asset_ast_vm_dic 에 저장됨
                    //단! 포트는 DB상에 없어서 asset_id도 없고 강제로 만들어 주므로 g.asset_ast_vm_dic에는 들어가지 않는다
                    else
                    {
                        if (ast_vm.type != AssetTreeType.PC)
                        {
                            p_ast_vm = astMgr.getParentAssetTreeVMinMD(ast_vm);

                            ast_vm = makeChildPortsToAssetTreeVM(ast_vm); // 포트를 달고 하위 단말이 있으면 포함 시킨다. 
                            if (p_ast_vm != null)
                            {
                                ast_vm.parant_ast_vm = p_ast_vm;

                                p_ast_vm.child_list.Add(ast_vm);
                                p_ast_vm.is_expander_visible = Visibility.Visible;

                                if (!(g.asset_ast_vm_dic.ContainsKey(ast_vm.asset_id ?? 0)))
                                    g.asset_ast_vm_dic.Add(ast_vm.asset_id ?? 0, ast_vm);  // 아울렛 까지 저장 

                            }
                        }

                    }
                    
                }

            }
        }

        // 정리된 뷰모델 데이터로 TreeView에 집어 넣는다
        // 초기에는 확장을 하지 않고 루트 노드만 보인다.
        // 이후 레지스트리에 저장된 확장 정보로 이전 사용한 확장 정보까지 노드를 확장 한다.  
         public void initLoacaionTree(int site_id)
        {
            //화면에 표시할 사이트를 지정한다

            AssetTreeVM _site;
            foreach (var _region1 in g.asset_tree_vm_list) // 트리 노드를 따라 가며 뺑뺑이 ?? 삭제?
            {
                foreach (var _region2 in _region1.child_list) // 삭제 ?
                {
                    _site = _region2.child_list.Find(at => at.type_id == site_id);
                    //선택된 사이트를 전역 변수에 저장한다
                    //g.base_asset_tree = _site;
                    if (_site != null)
                    {
                        //사이트 정보를 복사한다 루트 하나만 , 차일드는 복사 안함  
                        AssetTreeVM _site_vm = astMgr.cpAssetTreeTV(_site);
                        
#if false
                        foreach (var _building in _site.child_list)
                        {
                            AssetTreeVM _building_vm = astMgr.cpAssetTreeTV(_building);
                            _site_vm.child_list.Add(_building_vm);
                        }
#endif
                        asset_tree_vm_tv_list.Add(_site_vm);

                        l_tv.ItemsSource = asset_tree_vm_tv_list;
                        Refresh.Refresh_Controls(l_tv);

                        //빌딩까지 보이도록 확장한 상태에서 시작한다
#if false
                        TreeViewItem tvi = l_tv.ItemContainerGenerator.ContainerFromIndex(l_tv.Items.CurrentPosition) as TreeViewItem;
                        AssetTreeVM tmp_ast_vm = getAndCopyAssetTreeVM_withChild(_site_vm);
                        tvi.ItemsSource = tmp_ast_vm.child_list;
                        tvi.IsExpanded = true;
#else
                        //base_level : 1~8
                        base_level = _site_vm.disp_level;

                        //expandTree(_site_vm);
                        getAndApplyExpandInfoList(); // 저장된 확장 정보로 노드 확장 

                        //알람정보에 대해서 처리한다, 알람 카운트 처리 중요 (후에 변경시 확인 요망 )
                        getAndApplyAlarmList();
#endif               
                        
                        break;
                    }
                            
                }
            }
            
         }
      
    #endregion

        #region 확장, 축소에 대한 처리

        //축소 이벤트 발생시 호출
        public void unExpandTree(AssetTreeVM ast_vm_item)
        {
            TreeViewItem tvi = getTreeViewItem(ast_vm_item);
            if (tvi.IsExpanded == false) return;

            tvi.ItemsSource = null;
            tvi.IsExpanded = false;
            removeExpandState(ast_vm_item);
        }

        //확대 이벤트 발생시 호출
        public void expandTree(AssetTreeVM ast_vm_item)
        {
            TreeViewItem tvi = getTreeViewItem(ast_vm_item);
            //if (tvi.IsExpanded == true) return;

            //현재 item을 한단계의 child를 포함해 copy온다
            AssetTreeVM ast_vm = getAndCopyAssetTreeVM_withChild(ast_vm_item);
            tvi.ItemsSource = ast_vm.child_list;
            tvi.IsExpanded = true;
            Refresh.Refresh_Controls(l_tv);
            addExpandState(ast_vm);
        }
        #endregion


        #region 트리 아이콘 변경 처리 
        List<int[]> expandinfo_list = new List<int[]>();

        // 지능형 컨트롤러, 패치패널 , 포트 아이콘 변경처리 
        private void getAndApplyAlarmList()
        {
            List<asset_ipp_port_link> al_list = g.asset_ipp_port_link_list.FindAll(at => (at.alarm_status == "P") || (at.alarm_status == "U"));

            if (isAlarmCheck == false) // 초기화시 한번만 수행하게 처리 romee/1/22
            { 
                isAlarmCheck = true;
                foreach (var al in al_list)
                {
                    if (g.asset_ast_vm_dic.ContainsKey(al.ipp_asset_id))
                    {
                        AssetTreeVM ipp_vm = g.asset_ast_vm_dic[al.ipp_asset_id];
                        AssetTreeVM port_vm = ipp_vm.child_list.Find(at => at.port_no == al.port_no);

                        Boolean on_plug;
                        if (al.alarm_status == "p")
                            on_plug = true;
                        else
                            on_plug = false;
                        changePortStatusView(port_vm, on_plug, 1);
                    }
                }
            }

            if (g.user_key != null)
            {
                // 알람 클리어 처리 필요  romee 1/16
                // F5 리플레시 누를경우 알람 카운트 증가 현상 있음 모듈을 새로 작성이 필요 romee/1/16
                // 트리에서 알람카은트가 남는 현상을 F5 키로 제거하기 위해 만듬 -> 리프레쉬 기능  
                if (al_list.Count == 0 && (g.user_key.Key == Key.F5))
                {
                    List<asset_ipp_port_link> al_list2 = g.asset_ipp_port_link_list.FindAll(at => (at.alarm_status == "-"));
                    foreach (var al in al_list2)
                    {
                        if (g.asset_ast_vm_dic.ContainsKey(al.ipp_asset_id))
                        {
                            AssetTreeVM ipp_vm = g.asset_ast_vm_dic[al.ipp_asset_id];
                            AssetTreeVM port_vm = ipp_vm.child_list.Find(at => at.port_no == al.port_no);
                            changePortStatusArAlarmInit(ipp_vm);
                        }
                    }
                }
                g.user_key = null;
            }

            List<ic_connect_status> ic_al_list = g.ic_connect_status_list.FindAll(at => at.ic_connect_status1 != "Y");
            foreach (var ic_al in ic_al_list)
            {
                 changeIcStatusView(ic_al.ic_asset_id,true);
            }
            List<ipp_connect_status> ipp_al_list = g.ipp_connect_status_list.FindAll(at => (at.ic_asset_id != null) && (at.connect_status != "Y"));
            foreach(var ipp_al in ipp_al_list)
            {
                changeIppStatusView(ipp_al.ipp_asset_id, true);
            }
            changeswStatus();

        }

        private void changePortStatusArAlarmInit(AssetTreeVM port_vm)
        {

            AssetTreeVM ast_vm_in_md = astMgr.getAssetTreeVMinMD(port_vm);
            if (ast_vm_in_md == null)
                return;

            AssetTreeVM ast_vm_in_vm = getAssetTreeVMinVM(port_vm);

            if (ast_vm_in_md.child_alarm_cnt < 1) return;
            ast_vm_in_md.child_alarm_cnt -= 1;
            if (ast_vm_in_md.child_alarm_cnt <= 0)
            {
                ast_vm_in_md.child_alarm_cnt = 0;
                ast_vm_in_md.is_child_alarm_visible = Visibility.Hidden;
            }

            RemoveParentAlarmCnt(ast_vm_in_md); // 알람 카운트 처리 
        }

        // 스위치 상태 변경 처리 
        public void changeswStatus()
        {
            // 스위치 상태 추가 
            List<asset_port_link> apl_list = g.asset_port_link_list.FindAll(at => Etc.get_catalog_by_asset_id(at.asset_id).catalog_group_id == 3320); // 스위치 찾기 
            foreach (var al in apl_list)
            {
                if (g.asset_ast_vm_dic.ContainsKey(al.asset_id))
                {
                    AssetTreeVM ipp_vm = g.asset_ast_vm_dic[al.asset_id];
                    AssetTreeVM port_vm = ipp_vm.child_list.Find(at => at.port_no == al.port_no);
                    if (ipp_vm == null || port_vm == null)
                    { }
                    if(al.front_asset_id == null)
                        changeswStatusView(port_vm, false, 1);
                    else
                        changeswStatusView(port_vm, true, 1);

                }
            }
        }

        // 레지스트리에 저장된 확장 정보로 노드 확장 처리
        // 저장 순서 : 로케이션, 엣셋 , 포트 
        private void getAndApplyExpandInfoList()
        {
            List<int[]> tmp_list = Reg.get_asset_tree("asset_tree");

            //expandinfo_list.Remove(expandinfo_list[1]);
            foreach (var expandinfo in tmp_list)
            {
                //location
                if ((expandinfo[1] == 0) && (expandinfo[2] == 0))
                {
                    if (g.location_ast_vm_dic.ContainsKey(expandinfo[0]) == false) return;
                    AssetTreeVM ast_vm = g.location_ast_vm_dic[expandinfo[0]];
                    expandTreeCustom(ast_vm);
                }
                //asset
                else if ((expandinfo[1] != 0) && (expandinfo[2] == 0))
                {
                    if (g.asset_ast_vm_dic.ContainsKey(expandinfo[1]) == false) return;
                    AssetTreeVM ast_vm = g.asset_ast_vm_dic[expandinfo[1]];
                    expandTreeCustom(ast_vm);
                }
                //port
                else
                {
                    if (g.asset_ast_vm_dic.ContainsKey(expandinfo[1]) == false) return;
                    AssetTreeVM p_ast_vm = g.asset_ast_vm_dic[expandinfo[1]];

                    AssetTreeVM ast_vm = p_ast_vm.child_list.Find(at => at.port_no == expandinfo[2]);
                    if (ast_vm == null) return;
                    expandTreeCustom(ast_vm);

                }
            }

        }

        // 노드 확장 처리 
        public void expandTreeCustom(AssetTreeVM ast_vm)
        {
            location l = g.location_list.Find(at=>at.location_id == ast_vm.location_id);

            //1. 사이트 레벨의 tvi를 받아 온다
            int site_l_id = Etc.get_location_id_by_site_id(l.site_id ?? 0);

            if (g.location_ast_vm_dic.ContainsKey(site_l_id) == false) return;
            AssetTreeVM cur_ast_vm = g.location_ast_vm_dic[site_l_id];
            AssetTreeVM p_ast_vm = cur_ast_vm;

            AssetTreeVM site_ast_vm_tv = asset_tree_vm_tv_list[0]; // 무조건 사이트 부터 가져옴 
            if (site_ast_vm_tv == null) return;

            if (site_ast_vm_tv.location_id != cur_ast_vm.location_id) return;


            TreeViewItem tvi = (TreeViewItem)l_tv.ItemContainerGenerator.ContainerFromItem(site_ast_vm_tv);
            if (tvi == null) return;

            expandTreeTvi(tvi, cur_ast_vm);
            if (ast_vm.type == AssetTreeType.Site) return;

            //cur_lv을 site_level로 맞춰놓는다
            int cur_l_id;

            //location 의 처리
            while(cur_ast_vm.asset_id==null)
            {
                //현재 레벨의 로케이션 id를 찾는다
                switch (p_ast_vm.disp_level )
                {
                    case 3://부모가 site
                        cur_l_id = Etc.get_location_id_by_building_id(l.building_id ?? 0);
                        break;
                    case 4://부모가 building
                        cur_l_id = Etc.get_location_id_by_floor_id(l.floor_id ?? 0);
                        break;
                    case 5://부모가 floor
                        cur_l_id = Etc.get_location_id_by_room_id(l.room_id ?? 0);
                        break;
                    case 6://부모가 room
                        cur_l_id = Etc.get_location_id_by_rack_id(l.rack_id ?? 0);
                        break;
                    default:
                        cur_l_id = 0;
                        break;
                }

                //location_id로 cur_ast_vm을 찾는다
                if (g.location_ast_vm_dic.ContainsKey(cur_l_id) == false) break;
                cur_ast_vm = g.location_ast_vm_dic[cur_l_id];
                
                //index를 확인하여 현재의 tvi를 찾는다
                int index = p_ast_vm.child_list.FindIndex(at=> at.location_id == cur_ast_vm.location_id);

                tvi = (TreeViewItem)tvi.ItemContainerGenerator.ContainerFromIndex(index);
                if (tvi == null) return;
                //tvi = findChildItem(tvi, index);

                //현재의 tvi를 확장한다
                expandTreeTvi(tvi, cur_ast_vm);

                //현재 ast_vm을 부모에 ast_vm으로 넣어준다
                p_ast_vm = cur_ast_vm;
            }

            //asset_id가 없는경우 여기서 리턴
            if (ast_vm.asset_id == null) return;


            //asset의 처리
            if(ast_vm.type == AssetTreeType.Port)
            {
                //Port의 경우 Asset의 확장이 먼저 필요
                AssetTreeVM ast_ast_vm = ast_vm.parant_ast_vm;
                if (ast_ast_vm == null) return;

                TreeViewItem ast_tvi = getTreeViewItem(ast_ast_vm);
                if (ast_tvi == null) return;
                expandTreeTvi(ast_tvi, ast_ast_vm);


                //Port의 확장
                TreeViewItem port_tvi = getTreeViewItem(ast_vm);
                if (port_tvi == null) return;
                expandTreeTvi(port_tvi, ast_vm);
            }
            else if(ast_vm.type == AssetTreeType.PC)
            {
                //PC의 경우 Asset과 Port의 확장이 먼저
                AssetTreeVM port_ast_vm = ast_vm.parant_ast_vm;
                if (port_ast_vm == null) return;

                AssetTreeVM ast_ast_vm = port_ast_vm.parant_ast_vm;
                if (ast_ast_vm == null) return;

                //Asset 확장
                TreeViewItem ast_tvi = getTreeViewItem(ast_ast_vm);
                if (ast_tvi == null) return;
                expandTreeTvi(ast_tvi, ast_ast_vm);

                //Port의 확장
                TreeViewItem port_tvi = getTreeViewItem(port_ast_vm);
                if (port_tvi == null) return;
                expandTreeTvi(port_tvi, port_ast_vm);

                //PC의 확장?
                TreeViewItem pc_tvi = getTreeViewItem(ast_vm);
                if (pc_tvi == null) return;
                expandTreeTvi(pc_tvi, ast_vm);
            }
            else
            {
                //일반적인 Asset의 경우는 자신만 확장 해주면됨
                TreeViewItem ast_tvi = getTreeViewItem(ast_vm);
                if (ast_tvi == null) return;
                expandTreeTvi(ast_tvi, ast_vm);
            }
        }

        // 노드를 확장 시키기 트리와 뷰모델로
        private void expandTreeTvi(TreeViewItem tvi, AssetTreeVM ast_vm)
        {
            if (tvi == null) return;
#if true
            AssetTreeVM ast_vm_tv = (AssetTreeVM)tvi.Header; // 헤더를 가져와 하위 확장 처리 , 차일드 확장
            if (ast_vm_tv.is_expanded == false)
            {
                ast_vm_tv.is_expanded = true;
                ast_vm_tv.force_changed = true;
            }
            Refresh.Refresh_Controls(l_tv); // UI Refresh
#else
            AssetTreeVM ast_vm_cp = getAndCopyAssetTreeVM_withChild(ast_vm);
            if (ast_vm_cp == null) return;

            tvi.ItemsSource = ast_vm_cp.child_list;
            tvi.IsExpanded = true;

            Refresh.Refresh_Controls(l_tv);

            AssetTreeVM ast_vm_tv = (AssetTreeVM)tvi.Header;
            ast_vm_tv.is_expanded = true;
            
            Refresh.Refresh_Controls(l_tv);
#endif   
        }
        // 노드 확장정보 저장 
        private void addExpandState(AssetTreeVM ast_vm)
        {
            //부모의 expandinfo 제거
            AssetTreeVM p_ast_vm = astMgr.getParentAssetTreeVMinMD(ast_vm);
            if (p_ast_vm.asset_id == null)
            {
                //1.부모가 location인 경우
                int[] p_int = expandinfo_list.Find(at => (at[0] == p_ast_vm.location_id) && (at[1] == 0) && (at[2] == 0));
                if(p_int!=null)
                    removeExpandinfoFromList(p_int[0], 0, 0);

                //자식은 asset 일수도 location 일수도 있다
                addExpandinfoToList(ast_vm.location_id, ast_vm.asset_id ?? 0, 0);
            
            }
            else if (p_ast_vm.type != AssetTreeType.Port)
            {
                //2.부모가 asset인 경우
                int[] p_int = expandinfo_list.Find(at => (at[0] == p_ast_vm.location_id) && (at[1] == p_ast_vm.asset_id) && (at[2] == 0));
                if (p_int != null)
                    removeExpandinfoFromList(p_int[0], 0, 0);
                //자식은 port 아니면 asset 이다
                addExpandinfoToList(ast_vm.location_id, ast_vm.asset_id ?? 0, ast_vm.port_no);
            
            }
            else
            {    //=> 이경우는 아직 사용되지 않음
                //3.부모가 port인 경우
                int[] p_int = expandinfo_list.Find(at => (at[0] == p_ast_vm.location_id) && (at[1] == p_ast_vm.asset_id) && (at[2] == p_ast_vm.port_no));
                if (p_int != null)
                    expandinfo_list.Remove(p_int);
                //부모가 port이면 자식은 pc 이다
                addExpandinfoToList(ast_vm.location_id, ast_vm.asset_id ?? 0, ast_vm.port_no);
            }
      
        }
        
        // 확장정보 삭제 -> 업데이트 
        private void removeExpandState(AssetTreeVM ast_vm)
        {
            location l = g.location_list.Find(at=>at.location_id == ast_vm.location_id);
            if(l==null) return;

            //현재의 expandinfo 제거
            int[] cur_int = new int[3];
            if (ast_vm.asset_id == null)
            {
                //1. location인 경우
                List<location> l_l_list;
                //해당 loction아래에 포함되는 location을 받아 온다
                switch(ast_vm.type)
                {
                    case AssetTreeType.Site:
                        //해당 site에 포함되는 location을 받아 온다
                        l_l_list = g.location_list.FindAll(at => (at.site_id == l.site_id));
                        break;
                    case AssetTreeType.Building:
                        l_l_list = g.location_list.FindAll(at => (at.building_id == l.building_id));
                        break;
                    case AssetTreeType.Floor:
                        l_l_list = g.location_list.FindAll(at => (at.floor_id == l.floor_id));
                        break;
                    case AssetTreeType.Room:
                        l_l_list = g.location_list.FindAll(at => (at.room_id == l.room_id));
                        break;
                    case AssetTreeType.Rack:
                        l_l_list = g.location_list.FindAll(at => (at.rack_id == l.rack_id));
                        break;
                    default:
                        return;
                        
                }

                //각각 location_id와 동일한 곳을 찾아 삭제한다
                foreach (var l_l in l_l_list)
                {
                    List<int[]> l_exp_list = expandinfo_list.FindAll(at => at[0] == l_l.location_id);
                    foreach (var l_exp in l_exp_list)
                    {
                        removeExpandinfoFromList(l_exp[0], l_exp[1], l_exp[2]);
                    }
                }
                
            }
            else if (ast_vm.type != AssetTreeType.Port)
            {
                //2. asset인 경우 

                //해당 Asset_id를 동일하게 포함하는 port들의 확장 정보를 지운다
                List<int[]> l_exp_list = expandinfo_list.FindAll(at => at[0] == (ast_vm.location_id)&&  (at[1] == ast_vm.asset_id));
                foreach (var l_exp in l_exp_list)
                {
                    removeExpandinfoFromList(l_exp[0],l_exp[1],l_exp[2]);
                }

                if(ast_vm.type ==AssetTreeType.SwitchSlot)
                {
                    List<sw_card_config> sw_cfg_list = g.sw_card_config_list.FindAll(at=> at.sw_asset_id == ast_vm.asset_id);
                    foreach(var sw_cfg in sw_cfg_list){
                        removeExpandinfoFromList(ast_vm.location_id, sw_cfg.sw_card_asset_id ?? 0,0);
                    }
                }
                    
            }
            else
            {
                //3. port인 경우 
                //자신의 확장 정보만을 지운다
                cur_int = expandinfo_list.Find(at => (at[0] == ast_vm.location_id) && (at[1] == ast_vm.asset_id) && (at[2] == ast_vm.port_no));
                if (cur_int != null)
                    expandinfo_list.Remove(cur_int);
            }
            updateExpandInfoReg();
        }

        // 업데이트 
        private void addExpandinfoToList(int l_id, int ast_id, int p_no)
        {
            int[] cur_int = new int[3];
            cur_int[0] = l_id;
            cur_int[1] = ast_id;
            cur_int[2] = p_no;

            if (expandinfo_list.Exists(at => (at[0] == l_id) && (at[1] == ast_id) && (at[2] == p_no)) == false)
            {
                expandinfo_list.Add(cur_int);
                updateExpandInfoReg();
            }
        }

        // 업데이트 
        private void removeExpandinfoFromList(int l_id, int ast_id, int p_no)
        {
            int[] cur_int = expandinfo_list.Find(at => (at[0] == l_id) && (at[1] == ast_id) && (at[2] == p_no));
            {
                expandinfo_list.Remove(cur_int);
                updateExpandInfoReg();
            }
        }

        // 레지 업데이트 
        private void updateExpandInfoReg()
        {
            Boolean ret = Reg.save_tree("asset_tree", expandinfo_list);
        }
#endregion

        #region asset_tree_tv를 찾는 메소드

        //실제 트리뷰에 연결된 AssetTreeVM 데이터를 받아온다
        public  AssetTreeVM getAssetTreeVMinVM(AssetTreeVM ast_vm)
        {
#if false

            //최상위 tvi를 받아 온다 
            List<AssetTreeVM> ast_vm_in_vm_list = tv.ItemsSource as List<AssetTreeVM>;

            // ast_vm_in_vm_list[0]는 최상위 즉 사이트의 ast_vm 이다
            //실제 Tree에 들어가있는 ast_vm
            AssetTreeVM ast_vm_in_vm = ast_vm_in_vm_list[0];
            //데이터 모델에 저장된 ast_vm
            AssetTreeVM ast_vm_in_md;

            //두번쨰, 빌딩 레벨의 tvi를 받아 온다
            TreeViewItem tvi = (TreeViewItem)l_tv.ItemContainerGenerator.ContainerFromIndex(l_tv.Items.CurrentPosition);

            if (tvi == null)
                return null;

            //disp_level 4 빌딩부터 조회한다
            for (int i = base_level + 1; i <= ast_vm.disp_level; i++)
            {
                //tvi에 속해 있는 ast_vm 들을 list로 받아온다
                ast_vm_in_vm_list = tvi.ItemsSource as List<AssetTreeVM>;
                if (ast_vm_in_vm_list == null) return null;
                if (ast_vm_in_vm_list.Count == 0) return null;

                //현재 레벨에 해당하는 location_id로 
                //데이터 모델에서 현재 레벨에 해당하는 ast_vm을 찾는다
                int l_id = getLocationIdbyLevel(ast_vm.location_id, i);

                //l_id 가 -1이 아니라면 현재 레벨은 location이다
                if (l_id != -1)
                {
                    //if (g.location_ast_vm_dic.ContainsKey(l_id) == false) return null;
                    //ast_vm_in_md = g.location_ast_vm_dic[l_id];

                    //ast_vm 의 level과 현재의 레벨이 같으면 해당 항목으로 index를 찾을수 있다
                    if (i == ast_vm.disp_level)
                    {
                        ast_vm_in_vm = ast_vm_in_vm_list.Find(at => at.location_id == l_id);
                        if (ast_vm_in_vm != null)
                            return ast_vm_in_vm;
                    }
                    //현재 level이 맞지 않으면 다음레벨로 넘어 간다
                    else
                    {
                        int index = ast_vm_in_vm_list.FindIndex(at => at.location_id == l_id);

                        //다음 tvi를 만들고 넘어간다
                        tvi = findChildItem(tvi, index);
                        if (tvi == null) return null;
                    }
                }

                //현재 레벨이 asset이라면 아래 3가지 중 하나로 예외처리가 필요하다
                else
                {
                    switch (ast_vm.type)
                    {
                        case AssetTreeType.SwitchCard:
                            //SwitchCard는 반드시위에 SwitchSlot이 있다
                            sw_card_config sw_c = g.sw_card_config_list.Find(at => at.sw_card_asset_id == ast_vm.asset_id);
                            if (sw_c == null) return null;

                            //slot의 인덱스를 찾아 다음 tvi에서 itemSource를 받아 온다
                            int sw_sl_index = ast_vm_in_vm_list.FindIndex(at => at.asset_id == sw_c.sw_asset_id);
                            tvi = findChildItem(tvi, sw_sl_index);
                            if (tvi == null) return null;

                            ast_vm_in_vm_list = tvi.ItemsSource as List<AssetTreeVM>;
                            if (ast_vm_in_vm_list == null) return null;
                            if (ast_vm_in_vm_list.Count == 0) return null;

                            //sw slot아래의 sw card리스트에서 해당하는 card를 찾아서 리턴
                            return ast_vm_in_vm_list.Find(at => at.asset_id == ast_vm.asset_id);

                        case AssetTreeType.Port:
                            //포트인 경우는 위에 Asset들이 있다
                            //포트가 연결된 Asset의 인덱스를 찾아 다음 tvi에서 itemSource를 받아 온다
                            int ast_index = ast_vm_in_vm_list.FindIndex(at => at.asset_id == ast_vm.asset_id);
                            tvi = findChildItem(tvi, ast_index);
                            if (tvi == null) return null;

                            ast_vm_in_vm_list = tvi.ItemsSource as List<AssetTreeVM>;
                            if (ast_vm_in_vm_list == null) return null;
                            if (ast_vm_in_vm_list.Count == 0) return null;

                            //port리스트에서 해당 포트를 찾아 리턴한다
                            return ast_vm_in_vm_list.Find(at => at.port_no == ast_vm.port_no);

                        case AssetTreeType.PC:
                            //PC는 위에 포트가 있고 Asset이 있다 , 2단계가 있다
                            //예외 처리작업 필요!!!!  check!!!
                            //AssetTreeVM tmp_asset_ast_vm2 = astMgr.getParentAssetTreeVMinMD(ast_vm);
                            break;
                        default:
                            //나머지 경우는 asset_id로 쉽게 찾을수 있다
                            if (ast_vm_in_vm_list.Exists(at => at.asset_id == ast_vm.asset_id))
                                return ast_vm_in_vm_list.Find(at => at.asset_id == ast_vm.asset_id);
                            break;

                    }
                    return ast_vm_in_vm;
                }
            }
            return null; 
#else
            TreeViewItem tvi = getTreeViewItem(ast_vm);
            if (tvi == null) return null;
            return (AssetTreeVM)tvi.Header;
#endif
        }


        //location_id를 가지고 그 로케이션에서 해당 레벨에 해당하는 location_id를 찾아온다
        private int getLocationIdbyLevel(int location_id, int disp_level)
        {
            location l = g.location_list.Find(at => at.location_id == location_id);
            if (l == null) return -1;
            location p_l;
            switch (disp_level)
            {
                case 1:
                    region1 r1 = g.region1_list.Find(at => at.region1_id == l.region1_id);
                    if (r1 == null) return -1;
                    p_l = g.location_list.Find(at => (at.region1_id == r1.region1_id) && (at.region2_id == null));
                    break;
                case 2:
                    region2 r2 = g.region2_list.Find(at => at.region2_id == l.region2_id);
                    if (r2 == null) return -1;
                    p_l = g.location_list.Find(at =>
                        (at.region2_id == r2.region2_id) && (at.site_id == null));
                    break;
                case 3:
                    site s = g.site_list.Find(at => at.site_id == l.site_id);
                    if (s == null) return -1;
                    p_l = g.location_list.Find(at =>
                        (at.site_id == s.site_id) && (at.building_id == null));
                    break;
                case 4:
                    building bd = g.building_list.Find(at => at.building_id == l.building_id);
                    if (bd == null) return -1;
                    p_l = g.location_list.Find(at =>
                        (at.building_id == bd.building_id) && (at.floor_id == null));
                    break;
                case 5:
                    floor fl = g.floor_list.Find(at => at.floor_id == l.floor_id);
                    if (fl == null) return -1;
                    p_l = g.location_list.Find(at =>
                        (at.floor_id == fl.floor_id) && (at.room_id == null));
                    break;
                case 6:
                    room rm = g.room_list.Find(at => at.room_id == l.room_id);
                    if (rm == null) return -1;
                    p_l = g.location_list.Find(at =>
                        (at.room_id == rm.room_id) && (at.rack_id == null));
                    break;
                case 7:
                    rack rk = g.rack_list.Find(at => at.rack_id == l.rack_id);
                    if (rk == null) return -1;
                    p_l = g.location_list.Find(at => (at.rack_id == rk.rack_id));
                    break;
                default:
                    p_l = null;
                    break;
            }
            if (p_l == null) return -1;
            else return p_l.location_id;
        }

        // 차일드 까지 , 뷰모델 참조하여
        public AssetTreeVM getAndCopyAssetTreeVM_withChild(AssetTreeVM ast_vm_tv)
        {
            AssetTreeVM ast_vm = astMgr.getAssetTreeVMinMD(ast_vm_tv);  // ?????? 차일드 카피만 하면 안되는가 ?? // 인터일 경우 스위치 상태 변경 처리 ????
            AssetTreeVM target_ast_vm = astMgr.cpAssetTreeTV(ast_vm);
            foreach (var at in ast_vm.child_list)
            {
                AssetTreeVM target_child = astMgr.cpAssetTreeTV(at);
                target_ast_vm.child_list.Add(target_child);
            }

            return target_ast_vm;
        }
        // 모델에서 참조
        public AssetTreeVM getAndCopyAssetTreeVM(AssetTreeVM ast_vm_tv)
        {
            AssetTreeVM ast_vm = astMgr.getAssetTreeVMinMD(ast_vm_tv);
            AssetTreeVM target_ast_vm = astMgr.cpAssetTreeTV(ast_vm);
            
            return target_ast_vm;
        }
        // 차일드 리스트만 
        private List<AssetTreeVM> getCopyChild(AssetTreeVM p_ast_vm)
        {
            if (p_ast_vm.child_list.Count == 0)
                return null;

            List<AssetTreeVM> ast_vm_list = new List<AssetTreeVM>();
            foreach (var ast_vm in p_ast_vm.child_list)
            {
                ast_vm_list.Add(astMgr.cpAssetTreeTV(ast_vm));
            }
            return ast_vm_list;
        }
        #endregion
        // 사이트 안에 있는지 확인  
        public Boolean is_in_site(int location_id, int site_id)
        {
            location l = g.location_list.Find(at => at.location_id == location_id);
            if (l.site_id == site_id)
                return true;
            else
                return false;
        }
        // 검색에서 사용 
        public List<AssetTreeVM> searchAllAssetTreeVM(String ast_str)
        {
            int cnt;
            List<asset_tree> astt_l = g.asset_tree_list.FindAll(at => (at.disp_name.IndexOf(ast_str) != -1)&&is_in_site(at.location_id, g.selected_site_id));
            List<AssetTreeVM> ast_vm_l = new List<AssetTreeVM>();

            cnt = 0;
            foreach(var astt in astt_l)
            {
                cnt++;
                if (cnt > 20) return ast_vm_l;  // 20 건만 처리 
                AssetTreeVM ast_vm;
                if(astt.asset_id==null)
                {
                    if (g.location_ast_vm_dic.ContainsKey(astt.location_id))
                    {
                        ast_vm = g.location_ast_vm_dic[astt.location_id];
                        ast_vm_l.Add(ast_vm);
                    }
                }
                else
                {
                    if (g.asset_ast_vm_dic.ContainsKey(astt.asset_id ?? 0))
                    {
                        ast_vm = g.asset_ast_vm_dic[astt.asset_id ?? 0];
                        ast_vm_l.Add(ast_vm);
                    }
                }
            }
            return ast_vm_l;
        }
        // 동일
        public AssetTreeVM seachAssetTreeVM(String ast_str)
        {
            //asset_tree에서 동일한 이름이 있는지 찾는다
            asset_tree astt = g.asset_tree_list.Find(at => at.disp_name.IndexOf(ast_str) != -1);
            if (astt == null)
            {
                //맞는게 없으면 비슷한거라도.....
                //List<asset_tree> astt_l = g.asset_tree_list.


                MessageBox.Show("No Match Name!");
                return null;
            }

            AssetTreeVM ast_vm;
            if (astt.asset_id == null)
            {
                if (g.location_ast_vm_dic.ContainsKey(astt.location_id))
                    ast_vm = g.location_ast_vm_dic[astt.location_id];
                else
                    return null;
            }
            else
            {
                if (g.asset_ast_vm_dic.ContainsKey(astt.asset_id ?? 0))
                    ast_vm = g.asset_ast_vm_dic[astt.asset_id ?? 0];
                else
                    return null;
            }

            return ast_vm;
        }
        // 로케이션 아이디로 트리뷰 찾기
        public AssetTreeVM seachAssetTreeBylocation(int p)
        {
            asset_tree astt = g.asset_tree_list.Find(at => at.location_id == p);
            if (astt == null)
            {
                return null;
            }
            AssetTreeVM ast_vm;
            if (g.location_ast_vm_dic.ContainsKey(astt.location_id))
                ast_vm = g.location_ast_vm_dic[astt.location_id];
            else
                return null;
            return ast_vm;
        }
        // 자산 아이디로 트리뷰 찾기
        public AssetTreeVM seachAssetTreeByasset(int p)
        {
            asset_tree astt = g.asset_tree_list.Find(at => at.asset_id == p);
            if (astt == null)
            {
                return null;
            }
            AssetTreeVM ast_vm;
            if (g.asset_ast_vm_dic.ContainsKey(astt.asset_id ?? 0))
                ast_vm = g.asset_ast_vm_dic[astt.asset_id ?? 0];
            else
                return null;
            return ast_vm;
        }

        // 선택 -> 확장 없이 선택 -> 확장후 사용 -> 스크롤바 밑에 존재시 밑으로 이동이 필요 romee 
        public void selectAssetTreeVM(AssetTreeVM ast_vm)
        {
            AssetTreeVM p_ast_vm = astMgr.getParentAssetTreeVMinMD(ast_vm);
            if (p_ast_vm == null) return;
            
            expandTreeCustom(p_ast_vm);

            TreeViewItem tvi = getTreeViewItem(ast_vm);
            if (tvi == null) return;
            tvi.IsSelected = true;

        }

       
        #region Add,Del,Edit Asset 메소드
        //drag_ast_vm 선택해서 드래그한 Asset
        //drop_ast_vm 마우스로 드롭한 위치에 Asset
        //drag_ast_vm 을 drop_ast_vm 아래에 위치하게 한다
        public void moveAssetInSameParent(AssetTreeVM drag_ast_vm, AssetTreeVM drop_ast_vm)
        {
            //1. 부모를 받아와서 같은 부모인지 확인한다
            AssetTreeVM p_dg_ast_vm = astMgr.getParentAssetTreeVMinMD(drag_ast_vm);
            if (p_dg_ast_vm == null) return;
            AssetTreeVM p_dp_ast_vm = astMgr.getParentAssetTreeVMinMD(drop_ast_vm);
            if (p_dp_ast_vm == null) return;

            if ((drag_ast_vm.disp_level != 4) && (drag_ast_vm.disp_level != 5) && (drag_ast_vm.disp_level != 6) && (drag_ast_vm.disp_level != 7))
                return;


            if (p_dg_ast_vm == p_dp_ast_vm)
            {
                //2. 재정렬 한다
                AssetTreeVM dg_ast_vm_md = p_dg_ast_vm.child_list.Find(at => at.asset_tree_id == drag_ast_vm.asset_tree_id);
                if (dg_ast_vm_md == null) return;

                int dr_index = p_dg_ast_vm.child_list.FindIndex(at => at.asset_tree_id == drag_ast_vm.asset_tree_id);
                if (dr_index == -1) return;

                p_dg_ast_vm.child_list.Remove(dg_ast_vm_md);


                int dp_index = p_dg_ast_vm.child_list.FindIndex(at => at.asset_tree_id == drop_ast_vm.asset_tree_id);
                if ((dp_index == -1) && (dp_index > p_dg_ast_vm.child_list.Count))
                {
                    p_dg_ast_vm.child_list.Insert(dr_index + 1, dg_ast_vm_md);

                    return;
                }

                p_dg_ast_vm.child_list.Insert(dp_index + 1, dg_ast_vm_md);

                //3. 각각의 disp_order를 변경한다
                foreach (var item in p_dg_ast_vm.child_list)
                {
                    if (item is AssetTreeVM)
                    {
                        int index = p_dg_ast_vm.child_list.FindIndex(at => at.asset_tree_id == item.asset_tree_id);
                        item.disp_order = index + 1;
                    }
                }

                //4. 부모를 reset한다
                reload_treeViewItem(p_dg_ast_vm, true);


                //5.Item을 다시 선택한다
                TreeViewItem tvi = getTreeViewItem(dg_ast_vm_md);
                tvi.IsSelected = true;
            }
            else
            {
                //2. drag의 부모에서 drag_vm을 삭제한다
                AssetTreeVM dg_ast_vm_md = p_dg_ast_vm.child_list.Find(at => at.asset_tree_id == drag_ast_vm.asset_tree_id);
                if (dg_ast_vm_md == null) return;

                p_dg_ast_vm.child_list.Remove(dg_ast_vm_md);
                
                //3. drag의 부모의 child를 정렬한다
                foreach (var item in p_dg_ast_vm.child_list)
                {
                    if (item is AssetTreeVM)
                    {
                        int index = p_dg_ast_vm.child_list.FindIndex(at => at.asset_tree_id == item.asset_tree_id);
                        item.disp_order = index + 1;
                    }
                }
                //3-1. reset한다
                reload_treeViewItem(p_dg_ast_vm, true);

                //4. drop쪽의 부모에 추가한다
                if(drop_ast_vm.disp_level == drag_ast_vm.disp_level)
                {
                    dg_ast_vm_md.disp_order = p_dp_ast_vm.child_list.Count + 1;
                    dg_ast_vm_md.location_id = p_dp_ast_vm.location_id;
                    dg_ast_vm_md.parant_ast_vm = drop_ast_vm.parant_ast_vm;  // Jake

                    int dp_index = p_dp_ast_vm.child_list.FindIndex(at => at.asset_tree_id == drop_ast_vm.asset_tree_id);
                    if ((dp_index == -1) && (dp_index > p_dp_ast_vm.child_list.Count))
                    {
                        p_dp_ast_vm.child_list.Add(dg_ast_vm_md);
                    }
                    else
                    {
                        p_dp_ast_vm.child_list.Insert(dp_index + 1, dg_ast_vm_md);
                    }

                    //4-1. 부모를 reset한다
                    reload_treeViewItem(p_dp_ast_vm, true);

                }
                else if ( (drop_ast_vm.disp_level +1 )== drag_ast_vm.disp_level)
                {
                    AssetTreeVM dp_ast_vm_md = astMgr.getAssetTreeVMinMD(drop_ast_vm);
                    dg_ast_vm_md.location_id = drop_ast_vm.location_id;
                    dp_ast_vm_md.child_list.Add(dg_ast_vm_md);
                    dg_ast_vm_md.parant_ast_vm = drop_ast_vm;  // Jake
                   
                    //4-1. reset한다
                    reload_treeViewItem(dp_ast_vm_md, true);
                }

                
                //5.Item을 다시 선택한다
                TreeViewItem tvi = getTreeViewItem(dg_ast_vm_md);
                if (tvi == null) return;
                tvi.IsSelected = true;


            }
        }
        // 자산인 경우 
        public void addAssetToTreeView(asset_tree ast, int prev_location_id)
        {
            //asset_tree를 asset_tree_tv로 만든다
            AssetTreeVM ast_vm = astMgr.makeAssetTreeVM(ast);

            AssetTreeVM p_ast_vm = astMgr.getParentAssetTreeVMinMD(ast_vm);

            //부모 노드에 add 하기전 부모의 child count 가 0이면 expander 를 visible로 변경한다
            if (p_ast_vm.child_list.Count == 0)
            {
                p_ast_vm.is_expander_visible = Visibility.Visible;
                p_ast_vm.force_changed = true;
            }

            if (ast_vm.asset_id!=null)
            {
                //Asset인 경우
                ast_vm = makeChildPortsToAssetTreeVM(ast_vm);
                if( !(g.asset_ast_vm_dic.ContainsKey(ast_vm.asset_id ?? 0)))
                    g.asset_ast_vm_dic.Add(ast_vm.asset_id ?? 0, ast_vm);
            }
            else
            {   //location 인 경우
                //dic에 저장한다
                if (!(g.location_ast_vm_dic.ContainsKey(ast_vm.location_id)))
                    g.location_ast_vm_dic.Add(ast_vm.location_id, ast_vm);
            }

            //부모 노드에 추가하고 추가된 부모를 받아온다
            addChild_AssetTreeTV_MD(p_ast_vm, ast_vm);
            ast_vm.parant_ast_vm = p_ast_vm;


            //트리뷰를 필요한 부분만 reset한다
            reload_treeViewItem(p_ast_vm,true);

        }
        // 삭제
        public void delAssetToTreeView(AssetTreeVM ast_vm)
        {            
            //부모의 node를 만들어서 부모 item을 찾아 온다
            AssetTreeVM p_ast_vm = astMgr.getParentAssetTreeVMinMD(ast_vm);
            if (p_ast_vm == null)
                return;


            if (((ast_vm.disp_level == 7) || (ast_vm.disp_level == 8)) && (ast_vm.asset_id != null))
            {
                //Asset인 경우
                //ast_vm = makeAssetToAssetTreeVm(ast_vm);  //need check 141023
                if (g.asset_ast_vm_dic.ContainsKey(ast_vm.asset_id ?? 0))
                    g.asset_ast_vm_dic.Remove(ast_vm.asset_id ?? 0);
            }
            else
            {   //location 인 경우
                //dic에 저장한다
                if (g.location_ast_vm_dic.ContainsKey(ast_vm.location_id))
                    g.location_ast_vm_dic.Remove(ast_vm.location_id);
            }


            //부모의 child_list에서 item을 삭제 한다
            AssetTreeVM child_ast_vm = p_ast_vm.child_list.Find(at => at.asset_tree_id == ast_vm.asset_tree_id);
            if (child_ast_vm == null)
                return;

            p_ast_vm.child_list.Remove(child_ast_vm);

            reload_treeViewItem(p_ast_vm, false);
        }
        // 수정
        public void editAssetToTreeView(asset_tree astt, AssetTreeVM ast_vm)
        {
            if (astt != null)
            {
                sp_list_image_Result temp_image = (sp_list_image_Result)g.sp_image_list.Find(at => at.image_id == astt.image_id);
                string image_name = temp_image == null ? "etc_16.png" : temp_image.file_name;

                ast_vm.asset_tree_id = astt.asset_tree_id;
                ast_vm.disp_name = astt.disp_name;
                ast_vm.disp_level = astt.disp_level;
                ast_vm.force_changed = true;

                AssetTreeVM ast_vm_tv = getAssetTreeVMinVM(ast_vm);
                if (ast_vm_tv == null) return;
                ast_vm_tv.asset_tree_id = astt.asset_tree_id;
                ast_vm_tv.disp_name = astt.disp_name;
                ast_vm_tv.disp_level = astt.disp_level;
                ast_vm_tv.force_changed = true;
            }
            else //PC의 경우만 여기에 해당함
            {
                asset ast = g.asset_list.Find(at => at.asset_id == ast_vm.asset_id);
                if (ast == null) return;

                ast_vm.disp_name = ast.asset_name;
                ast_vm.disp_level = ast_vm.disp_level;
                ast_vm.force_changed = true;

                AssetTreeVM ast_vm_tv = getAssetTreeVMinVM(ast_vm);
                if (ast_vm_tv == null) return;
                ast_vm_tv.disp_name = ast.asset_name;
                ast_vm_tv.disp_level = ast_vm.disp_level;
                ast_vm_tv.force_changed = true;
            }
           
        }


        #endregion

        #region 단말 PC 의 추가 / 변경 / 삭제 처리 
        //단말을 추가하는 메서드
        public void addPc(int outl_asset_id, int port_no, int terminal_asset_id, string img_path)
        {
            //md에 해당 PC를 먼저 추가한다
            asset_terminal ast_t = g.asset_terminal_list.Find(at => at.terminal_asset_id == terminal_asset_id);
            if (ast_t == null) return;

            if (g.asset_ast_vm_dic.ContainsKey(outl_asset_id) == false) return;
            AssetTreeVM outl_ast_vm = g.asset_ast_vm_dic[outl_asset_id];

            AssetTreeVM port_ast_vm = outl_ast_vm.child_list.Find(at => at.port_no == port_no);
            if (port_ast_vm == null) return;

            asset ast = g.asset_list.Find(at => at.asset_id == terminal_asset_id);
            if (ast == null) return;
            AssetTreeVM pc_ast_vm = makeAssetTreeVM_Pc(ast, ast_t, port_ast_vm, port_ast_vm.child_list.Count);
            pc_ast_vm.image_file_path = img_path;
            port_ast_vm.child_list.Add(pc_ast_vm);
            if (g.asset_ast_vm_dic.ContainsKey(pc_ast_vm.asset_id ?? 0) == false)
                g.asset_ast_vm_dic.Add(pc_ast_vm.asset_id ?? 0, pc_ast_vm);

            //pc_list VM에 추가한다
            TreeViewItem tvi = getTreeViewItem(port_ast_vm);
            if (tvi == null) return;
            tvi.ItemsSource = null;

            AssetTreeVM port_ast_vm_tv_cp = astMgr.cpAssetTreeTVWithChild(port_ast_vm);
            tvi.ItemsSource = port_ast_vm_tv_cp.child_list;

            if (port_ast_vm_tv_cp.child_list.Count > 0)
            {
                AssetTreeVM port_ast_vm_tv = getAssetTreeVMinVM(port_ast_vm_tv_cp);
                port_ast_vm_tv.is_expander_visible = Visibility.Visible;
                port_ast_vm_tv.force_changed = true;
            }
        }

        //단말을 삭제하는 메서드
        public void removePc(int outl_asset_id, int port_no, int terminal_asset_id)
        {
            //md에 해당 PC를 먼저 삭제한다
            if (g.asset_ast_vm_dic.ContainsKey(outl_asset_id) == false) return;
            AssetTreeVM outl_ast_vm = g.asset_ast_vm_dic[outl_asset_id];

            AssetTreeVM port_ast_vm = outl_ast_vm.child_list.Find(at => at.port_no == port_no);
            if (port_ast_vm == null) return;

            AssetTreeVM pc_ast_vm = port_ast_vm.child_list.Find(at => at.asset_id == terminal_asset_id);
            if (pc_ast_vm == null) return;

            port_ast_vm.child_list.Remove(pc_ast_vm);
            if (g.asset_ast_vm_dic.ContainsKey(pc_ast_vm.asset_id ?? 0))
                g.asset_ast_vm_dic.Remove(pc_ast_vm.asset_id ?? 0);


            //pc_list VM을 reload한다
            //TreeViewItem tvi = getTreeViewItem(pc_ast_vm);
            TreeViewItem tvi = getTreeViewItem(port_ast_vm);
            if (tvi == null) return;
            tvi.ItemsSource = null;

            AssetTreeVM port_ast_vm_tv_cp = astMgr.cpAssetTreeTVWithChild(port_ast_vm);
            tvi.ItemsSource = port_ast_vm_tv_cp.child_list;

            if (port_ast_vm_tv_cp.child_list.Count < 1)
            {
                AssetTreeVM port_ast_vm_tv = getAssetTreeVMinVM(port_ast_vm_tv_cp);
                port_ast_vm_tv.is_expander_visible = Visibility.Hidden;
                port_ast_vm_tv.force_changed = true;
            }
        }

        //단말을 삭제하는 메서드 , 터미날 엣셋 아이디로 
        public void removePc(int terminal_asset_id)
        {
            //md에 해당 PC를 먼저 찾는다
            if (g.asset_ast_vm_dic.ContainsKey(terminal_asset_id) == false) return;
            AssetTreeVM pc_ast_vm = g.asset_ast_vm_dic[terminal_asset_id];

            AssetTreeVM port_ast_vm = astMgr.getParentAssetTreeVMinMD(pc_ast_vm);
            if (port_ast_vm == null) return;

            AssetTreeVM outl_ast_vm = astMgr.getParentAssetTreeVMinMD(port_ast_vm);
            if (outl_ast_vm == null) return;

            //md에서 삭제한다
            if (port_ast_vm.child_list.Exists(at => at.asset_id == pc_ast_vm.asset_id) == false) return;
            port_ast_vm.child_list.Remove(pc_ast_vm);
            g.asset_ast_vm_dic.Remove(terminal_asset_id);


            //TreeView에서 삭제한다(reload)
            reload_treeViewItem(port_ast_vm, false);
        }

        //단말을 변경하는 메서드
        public void changePcChange(int outl_asset_id, int port_no, int terminal_asset_id, String img_path)
        {
            //md에 해당 PC를 먼저 변경한다
            if (g.asset_ast_vm_dic.ContainsKey(outl_asset_id) == false) return;
            AssetTreeVM outl_ast_vm = g.asset_ast_vm_dic[outl_asset_id];

            AssetTreeVM port_ast_vm = outl_ast_vm.child_list.Find(at => at.port_no == port_no);
            if (port_ast_vm == null) return;

            AssetTreeVM pc_ast_vm = port_ast_vm.child_list.Find(at => at.asset_id == terminal_asset_id);
            if (pc_ast_vm == null) return;

            pc_ast_vm.image_file_path = img_path;

            //pc_list VM을 reload한다
            TreeViewItem tvi = getTreeViewItem(port_ast_vm);
            if (tvi == null) return;
            tvi.ItemsSource = null;

            AssetTreeVM port_ast_vm_tv = astMgr.cpAssetTreeTVWithChild(port_ast_vm);
            tvi.ItemsSource = port_ast_vm_tv.child_list;
        }

        #endregion

        #region 지능형 장치 상태 변경 처리

#if false
        public void changePortStatusView(AssetTreeVM ast_vm, String img_path, int on_alarm)
        {

            AssetTreeVM ast_vm_in_md = astMgr.getAssetTreeVMinMD(ast_vm);
            if (ast_vm_in_md == null)
                return;

            ast_vm_in_md.image_file_path = img_path;


            if (on_alarm == 1)
            {
                if (ast_vm_in_md.on_alarm == true) return;
                ast_vm_in_md.on_alarm = true;
                AssetTreeVM ast_vm_in_vm = getAssetTreeVMinVM(ast_vm);
                if (ast_vm_in_vm != null)
                {
                    ast_vm_in_vm.on_alarm = true;
                    ast_vm_in_vm.force_changed = true;
                    ast_vm_in_vm.image_file_path = img_path;
                }
                AddParentAlarmCnt(ast_vm_in_md);
            }
            else if (on_alarm == -1)
            {
                if (ast_vm_in_md.on_alarm == false) return;

                ast_vm_in_md.on_alarm = false;
                AssetTreeVM ast_vm_in_vm = getAssetTreeVMinVM(ast_vm);
                if (ast_vm_in_vm != null)
                {
                    ast_vm_in_vm.on_alarm = false;
                    ast_vm_in_vm.force_changed = true;
                    ast_vm_in_vm.image_file_path = img_path;
                }
                RemoveParentAlarmCnt(ast_vm_in_md);
            }
        } 
#else
        // 지능형 포트의 아이콘 변경 처리 
        public void changePortStatusView(AssetTreeVM ast_vm, Boolean on_plug, int on_alarm)
        {

            AssetTreeVM ast_vm_in_md = astMgr.getAssetTreeVMinMD(ast_vm);
            if (ast_vm_in_md == null)
                return;

            String img_path;
            if (on_plug)
                img_path = "/I2MS2;component/Icons/port_on_16.png";
            else
                img_path = "/I2MS2;component/Icons/port_16.png";

            ast_vm_in_md.image_file_path = img_path;
            AssetTreeVM ast_vm_in_vm = getAssetTreeVMinVM(ast_vm);
            if (ast_vm_in_vm != null)
            {
                ast_vm_in_vm.image_file_path = img_path;
                ast_vm_in_vm.force_changed = true;
            }


            if (on_alarm == 1)
            {
                bool old_alarm_flag = ast_vm_in_md.on_alarm;
                ast_vm_in_md.on_alarm = true;
                if (ast_vm_in_vm != null)
                {
                    ast_vm_in_vm.on_alarm = true;
                    ast_vm_in_vm.force_changed = true;
                }

                if (old_alarm_flag && (ast_vm_in_md.type == AssetTreeType.Port))  // 기존 알람이면 리턴 처리 romee/1/23
                    return;
                AddParentAlarmCnt(ast_vm_in_md); // 알람 카운트 처리 

            }
            else if (on_alarm == -1)
            {
                bool old_alarm_flag = ast_vm_in_md.on_alarm;
                ast_vm_in_md.on_alarm = false;
                if (ast_vm_in_vm != null)
                {
                    ast_vm_in_vm.on_alarm = false;
                    ast_vm_in_vm.force_changed = true;
                }
                //if (!old_alarm_flag && (ast_vm_in_md.type == AssetTreeType.Port))  // 기존 알람이면 리턴 처리 romee/1/23
                //    return;
                RemoveParentAlarmCnt(ast_vm_in_md); // 알람 카운트 처리 
            }
        }
#endif

        // 지능형 패널 상태 변경 처리 
        public void changeIppStatusView(int ipp_asset_id, Boolean up_down)
        {
            if (g.asset_ast_vm_dic.ContainsKey(ipp_asset_id))
            {
                AssetTreeVM ipp_ast_vm_md = g.asset_ast_vm_dic[ipp_asset_id];
                AssetTreeVM ipp_ast_vm_tv = getAssetTreeVMinVM(ipp_ast_vm_md);

                if (!up_down)
                {
                    if (ipp_ast_vm_md.on_alarm == true) return;

                    ipp_ast_vm_md.on_alarm = true;
                    if (ipp_ast_vm_tv != null)
                    {
                        ipp_ast_vm_tv.on_alarm = true;
                        ipp_ast_vm_tv.force_changed = true;
                    }
                    AddParentAlarmCnt(ipp_ast_vm_md);
                }
                else
                {

                    if (ipp_ast_vm_md.on_alarm == false) return;

                    ipp_ast_vm_md.on_alarm = false;
                    if (ipp_ast_vm_tv != null)
                    {
                        ipp_ast_vm_tv.on_alarm = false;
                        ipp_ast_vm_tv.force_changed = true;
                    }
                    RemoveParentAlarmCnt(ipp_ast_vm_md);
                }
            }
        }

        // 스위치 상태 변경 처리 
        public void changeswStatusView(AssetTreeVM ast_vm, Boolean on_plug, int on_alarm)
        {
            try {

                if (ast_vm.asset_id == 59007598)
                { }
                AssetTreeVM ast_vm_in_md = astMgr.getAssetTreeVMinMD(ast_vm);
                if (ast_vm_in_md == null)
                    return;

                String img_path;
                if (on_plug)
                    img_path = "/I2MS2;component/Icons/port_on_16.png";
                else
                    img_path = "/I2MS2;component/Icons/port_16.png";

                ast_vm_in_md.image_file_path = img_path;
                AssetTreeVM ast_vm_in_vm = getAssetTreeVMinVM(ast_vm);
                if (ast_vm_in_vm == null)
                    return;

                if (ast_vm_in_vm != null)
                {
                    ast_vm_in_vm.image_file_path = img_path;
                    ast_vm_in_vm.force_changed = true;
                }
            }
            catch { }

            
        }

        // 지능형 컨트롤러 상태 변경 처리 
        public void changeIcStatusView(int ic_asset_id, Boolean up_down)
        {
            if (g.asset_ast_vm_dic.ContainsKey(ic_asset_id))
            {
                AssetTreeVM ic_ast_vm_md = g.asset_ast_vm_dic[ic_asset_id];
                AssetTreeVM ic_ast_vm_tv = getAssetTreeVMinVM(ic_ast_vm_md);

                if (!up_down)
                {
                    //이전 상태와 다른 경우에만 적용 한다
                    if (ic_ast_vm_md.on_alarm == true) return;

                    ic_ast_vm_md.on_alarm = true;
                    if (ic_ast_vm_tv != null)
                    {
                        ic_ast_vm_tv.on_alarm = true;
                        ic_ast_vm_tv.force_changed = true;
                    }
                    AddParentAlarmCnt(ic_ast_vm_md);

                }
                else
                {

                    if (ic_ast_vm_md.on_alarm == false) return;

                    ic_ast_vm_md.on_alarm = false;
                    if (ic_ast_vm_tv != null)
                    {
                        ic_ast_vm_tv.on_alarm = false;
                        ic_ast_vm_tv.force_changed = true;
                    }

                    RemoveParentAlarmCnt(ic_ast_vm_md);
                }
            }
        }

        #endregion 

        #region 기타 메소드


        //asset인 경우에 필요한 요소들을 추가하여 AssetTreeVM을 완성한다 포트가 존재하는 경우 
        //public AssetTreeVM makeAssetToAssetTreeVm(AssetTreeVM ast_vm)
        public AssetTreeVM makeChildPortsToAssetTreeVM(AssetTreeVM ast_vm)
        {
            //== port add temp code ==
            asset asst = g.asset_list.Find(at => at.asset_id == ast_vm.asset_id);
            catalog c = g.catalog_list.Find(at => at.catalog_id == asst.catalog_id);

            if (c.num_of_ports > 0)
            {
                
                ///만약 ipp인경우 포트의 상태를 읽어서 처리하기 위해 해당하는 정보를 읽어와서 처리한다,
                //또한 자신의 알람상태도 확인하여 처리한다
                if (ast_vm.type == AssetTreeType.i_PatchPanel)
                {
                    ast_vm.is_expander_visible = Visibility.Visible;
                    ast_vm.force_changed = true;

                    //지능형 포트의 처리
                    List<asset_ipp_port_link> ipp_p_list = g.asset_ipp_port_link_list.FindAll(at => at.ipp_asset_id == ast_vm.type_id);
                    foreach (var ipp_p in ipp_p_list)
                    {
                        AssetTreeVM port_ast_vm = makeAssetTreeVM_Port(ipp_p.port_no, ipp_p.asset_ipp_port_link_id, ast_vm);

                        if ((ipp_p.alarm_status == "U") || (ipp_p.alarm_status == "P"))
                        {
                            port_ast_vm.image_file_path = "/I2MS2;component/Icons/port_alarm_16.png";
                            port_ast_vm.on_alarm = true;
                            AddParentAlarmCnt(port_ast_vm);
                        }
                        else if ((ipp_p.ipp_port_status == "P") || (ipp_p.ipp_port_status == "L"))
                        {
                            port_ast_vm.image_file_path = "/I2MS2;component/Icons/port_on_16.png";
                        }
                        ast_vm.child_list.Add(port_ast_vm);
                    }

                    //ic 와의 연결상태에 따라 ipp 상태 처리
                    ic_ipp_config ic_ipp_cfg = g.ic_ipp_config_list.Find(at => at.ipp_asset_id == ast_vm.type_id);
                    if (ic_ipp_cfg != null)
                    {
                        if (false)
                        {
                            //만약 연결되지 않은 상태이면 
                            ast_vm.image_file_path = "/I2MS2;component/Icons/ipp_alarm_16.png";
                            ast_vm.on_alarm = true;
                        }
                    }

                }
                //IPP외에 아래 6가지의 경우에는 포트가 존재 하므로 추가해 준다
                else if (
                    (ast_vm.type == AssetTreeType.PatchPanel)
                    || (ast_vm.type == AssetTreeType.Switch)
                    || (ast_vm.type == AssetTreeType.SwitchCard)
                    || (ast_vm.type == AssetTreeType.MutoaBox)
                    || (ast_vm.type == AssetTreeType.FacePlate)
                    || (ast_vm.type == AssetTreeType.ConsolidationPoint)
                    )
                {
                    ast_vm.is_expander_visible = Visibility.Visible;
                    ast_vm.force_changed = true;

                    //일반 포트의 처리
                    List<asset_port_link> p_list = g.asset_port_link_list.FindAll(at => at.asset_id == ast_vm.asset_id);
                    foreach (var p in p_list)
                    {
                        AssetTreeVM port_ast_vm = makeAssetTreeVM_Port(p.port_no, p.asset_port_link_id, ast_vm);

                        //FacePlate의 경우에는 아래에 PC가 존재할 수 있다!!
                        if (ast_vm.type == AssetTreeType.FacePlate)
                        {
                            //PC의 추가
                            List<asset_terminal> ast_t_list = g.asset_terminal_list.FindAll(at =>
                                (at.cur_outlet_asset_id == port_ast_vm.asset_id) && (at.cur_outlet_port_no == port_ast_vm.port_no));

                            if (ast_t_list.Count > 0)
                            {
                                port_ast_vm.is_expander_visible = Visibility.Visible;

                                int disp_order = 0;
                                foreach (var ast_t in ast_t_list)
                                {
                                    asset pc_ast = g.asset_list.Find(at => at.asset_id == ast_t.terminal_asset_id);
                                    if (pc_ast != null)
                                    {
                                        AssetTreeVM pc_ast_vm = makeAssetTreeVM_Pc(pc_ast ,ast_t, port_ast_vm, disp_order);

                                        // 2016.06.18 romee 지워진 터미날인지 확인후 출력 터미날 삭제 에러 처리
                                        // 익스 파이어된 터미날은 출력 안하기 처리 
                                        var at = g.asset_terminal_list.Find(p1 => p1.terminal_asset_id == pc_ast.asset_id);
                                        if (at == null)
                                            continue;
                                        //location_id가 0이 아닌경우에만 port에 추가한다
                                        if (pc_ast.location_id != 0 && (at.terminal_status != "E"))
                                            port_ast_vm.child_list.Add(pc_ast_vm);
                                        
                                        disp_order++;
                                        if (g.asset_ast_vm_dic.ContainsKey(pc_ast_vm.asset_id ?? 0) == false)
                                            g.asset_ast_vm_dic.Add(pc_ast_vm.asset_id ?? 0, pc_ast_vm);
                                    }
                                }
                            }
                        }

                        ast_vm.child_list.Add(port_ast_vm);
                        
                    }
                    
                }
            }
            return ast_vm;

        }
        // 포트 만들기 vm
        private AssetTreeVM makeAssetTreeVM_Port(int p_no, int link_id, AssetTreeVM p_ast_vm)
        {
            return  new AssetTreeVM()
            {
                asset_id = p_ast_vm.asset_id,
                asset_tree_id = p_ast_vm.asset_tree_id,
                location_id = p_ast_vm.location_id,
                disp_name = string.Format("Port{0}", p_no),
                disp_level = p_ast_vm.disp_level + 1,
                disp_order = p_no,
                image_file_path = string.Format("/I2MS2;component/Icons/port_16.png"),
                is_expander_visible = Visibility.Hidden,
                check_view = Visibility.Hidden,
                type_id = link_id,
                type = AssetTreeType.Port,
                port_no = p_no,
                parant_ast_vm = p_ast_vm,
                is_child_alarm_visible = Visibility.Hidden,
                child_alarm_cnt = 0,
                on_alarm = false,
                is_expanded = false
            };


        }

        
        // PC 만들기 
        private AssetTreeVM makeAssetTreeVM_Pc(asset ast, asset_terminal ast_t, AssetTreeVM port_ast_vm,int dp_order)
        {
            String img_path;
            if(ast_t.terminal_status=="Y")
                img_path = "/I2MS2;component/Icons/pc_on_16.png";
            else
                img_path = "/I2MS2;component/Icons/pc_16.png";

            return  new AssetTreeVM()
            {
                asset_id = ast.asset_id,
                asset_tree_id = 1,
                location_id = port_ast_vm.location_id,
                disp_name = ast.asset_name,
                disp_level = port_ast_vm.disp_level +1,
                disp_order = dp_order ,
                image_file_path = img_path,
                is_expander_visible = Visibility.Hidden,
                check_view = Visibility.Hidden,
                type_id = ast.asset_id,
                type = AssetTreeType.PC,
                port_no = ast_t.cur_outlet_port_no ?? 0,
                parant_ast_vm = port_ast_vm,
                is_child_alarm_visible = Visibility.Hidden,
                child_alarm_cnt = 0,
                on_alarm = false,
                is_expanded = false
            };
        }

        // 알람 카운트 모델과 뷰에 동시 처리 
        private void AddParentAlarmCnt(AssetTreeVM ast_vm)
        {
            int p_location_id = ast_vm.location_id;
            int p_disp_lv = ast_vm.disp_level;
            int p_child_cnt = ast_vm.child_alarm_cnt;


            AssetTreeVM tmp_ast_vm = astMgr.cpAssetTreeTV(ast_vm);
            while (true)
            {
                if (tmp_ast_vm.disp_level <= base_level)
                    break;

                //MD쪽의 처리
                AssetTreeVM pp_ast_vm = astMgr.getParentAssetTreeVMinMD(tmp_ast_vm);
                if (pp_ast_vm == null) break;

                pp_ast_vm.child_alarm_cnt += 1;
                pp_ast_vm.is_child_alarm_visible = Visibility.Visible;
                p_location_id = pp_ast_vm.location_id;
                p_disp_lv = pp_ast_vm.disp_level;
                p_child_cnt = pp_ast_vm.child_alarm_cnt;

                tmp_ast_vm = astMgr.cpAssetTreeTV(pp_ast_vm);


                //VM쪽의 처리
                AssetTreeVM pp_ast_vm_tv = getAssetTreeVMinVM(pp_ast_vm);
                if (pp_ast_vm_tv != null)
                {
                    pp_ast_vm_tv.child_alarm_cnt += 1;
                    pp_ast_vm_tv.is_child_alarm_visible = Visibility.Visible;
                    pp_ast_vm_tv.force_changed = true;
                }
            }
        }
        // 알람 카운트 삭제 
        private void RemoveParentAlarmCnt(AssetTreeVM ast_vm)
        {
            int p_location_id = ast_vm.location_id;
            int p_disp_lv = ast_vm.disp_level;
            int p_child_cnt = ast_vm.child_alarm_cnt;

            AssetTreeVM tmp_ast_vm = astMgr.cpAssetTreeTV(ast_vm);
            while (true)
            {
                if (tmp_ast_vm.disp_level <= base_level)
                    break;
                
                //MD쪽의 처리
                AssetTreeVM pp_ast_vm = astMgr.getParentAssetTreeVMinMD(tmp_ast_vm);
                if (pp_ast_vm == null)
                    break;

                pp_ast_vm.child_alarm_cnt -= 1;
                if (pp_ast_vm.child_alarm_cnt <= 0)
                {
                    pp_ast_vm.child_alarm_cnt = 0;
                    pp_ast_vm.is_child_alarm_visible = Visibility.Hidden;
                }
                p_location_id = pp_ast_vm.location_id;
                p_disp_lv = pp_ast_vm.disp_level;
                p_child_cnt = pp_ast_vm.child_alarm_cnt;

                tmp_ast_vm = astMgr.cpAssetTreeTV(pp_ast_vm);

                //VM쪽의 처리
                AssetTreeVM pp_ast_vm_tv = getAssetTreeVMinVM(pp_ast_vm);
                if (pp_ast_vm_tv != null)
                {
                    if (pp_ast_vm.child_alarm_cnt <= 0)
                        pp_ast_vm_tv.is_child_alarm_visible = Visibility.Hidden;
                    pp_ast_vm_tv.child_alarm_cnt -= 1;
                    pp_ast_vm_tv.force_changed = true;
                }
            }
        }


        // 차일드 추가 PC 추가인경우 
        private void addChild_AssetTreeTV_MD(AssetTreeVM p_ast_vm_src, AssetTreeVM child)
        {
            AssetTreeVM p_ast_vm = astMgr.getAssetTreeVMinMD(p_ast_vm_src);
            p_ast_vm.child_list.Add(child);
            p_ast_vm.is_expander_visible = Visibility.Visible;
            p_ast_vm.force_changed = true;

        }


        //TreeViewItem 아래에 있는 TreeViewItem을 index로 찾아준다 -> 
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

        //현재는 rack 내부에서 변경된 경우에만 적용가능 -> 랙뷰 에서 사용 
        public void updateAssetTreeItem(AssetTreeVM p_ast_vm_src)
        {
            //아래의 tv 아이템 제거
            unExpandTree(p_ast_vm_src);

            //== 아래에 들어가 tv 아이템을 수정해서 다시 정렬한다

            //vm 데이터 에서 해당 항목을 가져옴
            AssetTreeVM p_ast_vm = astMgr.getAssetTreeVMinMD(p_ast_vm_src);

            //child를 old_list로 copy한다
            List<AssetTreeVM> ast_vm_child_old_list = new List<AssetTreeVM>();
            foreach (AssetTreeVM at in p_ast_vm.child_list)
            {
                AssetTreeVM tmp_ast_vm = astMgr.cpAssetTreeTV(at);
                ast_vm_child_old_list.Add(tmp_ast_vm);
            }

            //child 를 clear한다
            p_ast_vm.child_list.Clear();

            //child 리스트의 순서를 확인하고 asset_tree_id를 다시 작성한다
            foreach (var ast_vm_old in ast_vm_child_old_list)
            {
                asset_tree ast = g.asset_tree_list.Find(at => at.asset_id == ast_vm_old.asset_id);
                ast_vm_old.asset_tree_id = ast.asset_tree_id;
                ast_vm_old.disp_order = ast.disp_order;
            }

            //asset_tree_id에 맞게 재 정렬 한다
            foreach (AssetTreeVM tmp_ast_vm in ast_vm_child_old_list.OrderBy(p => p.disp_order))
            {
                if (tmp_ast_vm != null)
                    p_ast_vm.child_list.Add(tmp_ast_vm);
            }

            AssetTreeVM p_ast_vm_with_child = getAndCopyAssetTreeVM_withChild(p_ast_vm);

            reload_treeViewItem(p_ast_vm_with_child, false);
        }

        //ast_vm에 해당하는 tvi를 찾아서 reload 해준다 
        private void reload_treeViewItem(AssetTreeVM ast_vm, Boolean is_expand)
        {
            TreeViewItem tvi = getTreeViewItem(ast_vm);
            
            if (tvi == null) return;
            Boolean bf_is_expand = tvi.IsExpanded;
            tvi.ItemsSource = null;
            
            AssetTreeVM tmp_ast_vm = getAndCopyAssetTreeVM_withChild(ast_vm);
            tvi.ItemsSource = tmp_ast_vm.child_list;

            //treeview의 child 겟수에 따라 expander 표시 여부 결정
            AssetTreeVM ast_vm_tv = (AssetTreeVM)tvi.Header;
            if (ast_vm.child_list.Count == 0)
                ast_vm_tv.is_expander_visible = Visibility.Hidden;
            else
                ast_vm_tv.is_expander_visible = Visibility.Visible;
            ast_vm_tv.force_changed = true;

            if (is_expand)
                tvi.IsExpanded = is_expand;
            else
                tvi.IsExpanded = bf_is_expand;
        }


        //ast_vm에 해당하는 tvi를 받아온다
        private TreeViewItem getTreeViewItem(AssetTreeVM ast_vm)
        {
            //최상위 tvi를 받아 온다 
            List<AssetTreeVM> ast_vm_in_vm_list = l_tv.ItemsSource as List<AssetTreeVM>;

            // ast_vm_in_vm_list[0]는 최상위 즉 사이트의 ast_vm 이다
            //실제 Tree에 들어가있는 ast_vm
            AssetTreeVM ast_vm_in_vm = ast_vm_in_vm_list[0];
            //데이터 모델에 저장된 ast_vm
            AssetTreeVM ast_vm_in_md;

            //두번쨰, 빌딩 레벨의 tvi를 받아 온다
            TreeViewItem tvi = (TreeViewItem)l_tv.ItemContainerGenerator.ContainerFromIndex(l_tv.Items.CurrentPosition);

            if (tvi == null)
                return null;
            else if (ast_vm.type == AssetTreeType.Site)
                return tvi;


            //disp_level 4 빌딩부터 조회한다
            for (int i = base_level + 1; i <= ast_vm.disp_level; i++)
            {
                //tvi에 속해 있는 ast_vm 들을 list로 받아온다
                ast_vm_in_vm_list = tvi.ItemsSource as List<AssetTreeVM>;
                if (ast_vm_in_vm_list == null) return null;
                if (ast_vm_in_vm_list.Count == 0) return null;

                //현재 레벨에 해당하는 location_id로 
                //데이터 모델에서 현재 레벨에 해당하는 ast_vm을 찾는다
                int l_id = getLocationIdbyLevel(ast_vm.location_id, i);

                //l_id 가 -1이 아니라면 현재 레벨은 location이다
                if (l_id != -1)
                {
                    //현재 레벨에서 l이 가리키는 tvi
                    int index = ast_vm_in_vm_list.FindIndex(at => at.location_id == l_id);
                    tvi = findChildItem(tvi, index);
                    if (tvi == null) return null;


                    //ast_vm 의 level과 현재의 레벨이 같으면 break!
                    if (i == ast_vm.disp_level)
                    {
                        break;
                    }
                }

                //현재 레벨이 asset이라면 아래 3가지 중 하나로 예외처리가 필요하다
                else
                {
                    switch (ast_vm.type)
                    {
                        case AssetTreeType.SwitchCard:
                            //SwitchCard는 반드시위에 SwitchSlot이 있다
                            sw_card_config sw_c = g.sw_card_config_list.Find(at => at.sw_card_asset_id == ast_vm.asset_id);
                            if (sw_c == null) return null;

                            //slot의 인덱스를 찾아 slot의 tvi를 찾는다
                            int sw_sl_index = ast_vm_in_vm_list.FindIndex(at => at.asset_id == sw_c.sw_asset_id);
                            tvi = findChildItem(tvi, sw_sl_index);

                            ast_vm_in_vm_list = tvi.ItemsSource as List<AssetTreeVM>;
                            if (ast_vm_in_vm_list == null) return null;
                            if (ast_vm_in_vm_list.Count == 0) return null;

                            //card의 인덱스를 찾아 card의 tvi를 찾는다 
                            int sw_cd_index = ast_vm_in_vm_list.FindIndex(at => at.asset_id == ast_vm.asset_id);
                            tvi = findChildItem(tvi, sw_cd_index);

                            //card의 md를 찾는다
                            if (g.asset_ast_vm_dic.ContainsKey(ast_vm.asset_id ?? 0) == false) return null;

                            break;

                        case AssetTreeType.Port:
                            //포트인 경우는 위에 Asset들이 있다
                            //포트가 연결된 Asset의 tvi를 찾는다
                            int ast_index = ast_vm_in_vm_list.FindIndex(at => at.asset_id == ast_vm.asset_id);
                            if (ast_index == -1) return null; //  romee 2/4 error 
                            tvi = findChildItem(tvi, ast_index);

                            ast_vm_in_vm_list = tvi.ItemsSource as List<AssetTreeVM>;
                            if (ast_vm_in_vm_list == null) return null;
                            if (ast_vm_in_vm_list.Count == 0) return null;

                            //port의 인텍스를 찾아 port의 tvi를 찾는다  
                            int p_index = ast_vm_in_vm_list.FindIndex(at => at.port_no == ast_vm.port_no);
                            tvi = findChildItem(tvi, p_index);

                            break;
                        case AssetTreeType.PC:
                            //PC는 위에 포트가 있고 Asset이 있다 , 2단계가 있다
                            //먼저 asset_terminal을 가져와 연결된 asset의 정보와 포트를 확인한다
                            asset_terminal ast_t = g.asset_terminal_list.Find(at => at.terminal_asset_id == ast_vm.asset_id);
                            int ast_index2 = ast_vm_in_vm_list.FindIndex(at => at.asset_id == ast_t.cur_outlet_asset_id);
                            if (ast_index2 == -1) return null; //  romee 2/4 error 
                            tvi = findChildItem(tvi, ast_index2);
                            
                            ast_vm_in_vm_list = tvi.ItemsSource as List<AssetTreeVM>;
                            if (ast_vm_in_vm_list == null) return null;
                            if (ast_vm_in_vm_list.Count == 0) return null;
                            
                            //port index로 port의 tvi를 찾는다
                            int p_index2 = ast_vm_in_vm_list.FindIndex(at => at.port_no == ast_t.cur_outlet_port_no);
                            tvi = findChildItem(tvi, p_index2);
                            
                            ast_vm_in_vm_list = tvi.ItemsSource as List<AssetTreeVM>;
                            if (ast_vm_in_vm_list == null) return null;
                            if (ast_vm_in_vm_list.Count == 0) return null;

                            //pc asset_id로 pc의 tvi를 찾는다
                            int pc_index = ast_vm_in_vm_list.FindIndex(at => at.asset_id == ast_t.terminal_asset_id);
                            tvi = findChildItem(tvi, pc_index);

                            //AssetTreeVM tmp_asset_ast_vm2 = astMgr.getParentAssetTreeVMinMD(ast_vm);
                            break;
                        default:
                            //asset의 index를 찾아 asset 의 tvi를 찾는다
                            int index = ast_vm_in_vm_list.FindIndex(at => at.asset_id == ast_vm.asset_id);
                            if (index == -1) return null; //  romee 2/4 error 
                            tvi = findChildItem(tvi, index);

                            //asset의 md를 찾는다
                            break;

                    }
                    break;
                }

            }//for문 끝

            if (tvi == null) return null;
            return tvi;
        }

        #endregion

        private void update_port_list(List<AssetTreeVM> ast_vm_in_vm_list, TreeViewItem tvi)
        {
            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(delegate
            {
                try
                {
                    tvi.ItemsSource = null;
                    tvi.ItemsSource = ast_vm_in_vm_list; // asset_tree_vm_tv_list;
                }
                catch (Exception)
                { }
            }));
        }
    }
}



