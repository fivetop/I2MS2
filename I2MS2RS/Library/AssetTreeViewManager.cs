using I2MS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WebApi.Models;

namespace I2MS2.Library
{
    // 트리뷰중 자산관리 뷰 모델  ??
    public class AssetTreeViewManager
    {
        // 뷰모델 생성 - 자산 트리로 
        public AssetTreeVM makeAssetTreeVM(asset_tree ast)
        {
            //asset     
            sp_list_image_Result temp_image = (sp_list_image_Result)g.sp_image_list.Find(at => at.image_id == ast.image_id);
            string image_name = temp_image == null ? "etc_16.png" : temp_image.file_name;
            Visibility expander_visible = Visibility.Hidden;
            AssetTreeVM ast_vm = new AssetTreeVM()
            {
                asset_id = ast.asset_id,
                location_id = ast.location_id,
                asset_tree_id = ast.asset_tree_id,
                disp_name = ast.disp_name,
                disp_level = ast.disp_level,
                disp_order = ast.disp_order,
                is_expander_visible = expander_visible,
                check_view = Visibility.Hidden,
                //image_file_path = string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH, "icon_16", temp_image.file_name),
                image_file_path = string.Format("/I2MS2;component/Icons/{0}", image_name),
                port_no = 0,
                is_child_alarm_visible = Visibility.Hidden,
                child_alarm_cnt = 0,
                on_alarm = false,
                is_expanded = false,
                type = getAssetTreeTypeLevel(ast),
            };
            
            ast_vm.type_id = getTypeId(ast_vm);
           

            return ast_vm;
        }


        // 뷰모델 생성 - 자산으로 
        public AssetTreeVM makeAssetTreeVM(asset ast)
        {
            //asset     
            asset_tree ast_tv = g.asset_tree_list.Find(at => at.asset_id == ast.asset_id);
            string image_name = "etc_16.png";
            if (ast_tv != null)
            {
                sp_list_image_Result temp_image = (sp_list_image_Result)g.sp_image_list.Find(at => at.image_id == ast_tv.image_id);
                image_name = temp_image == null ? "etc_16.png" : temp_image.file_name;
            }
            Visibility expander_visible = Visibility.Hidden;
            AssetTreeVM ast_vm = new AssetTreeVM()
            {
                asset_id = ast.asset_id,
                location_id = ast.location_id,
                asset_tree_id = 0,
                disp_name = ast.asset_name,
                disp_level = 0,
                disp_order = 0,
                is_expander_visible = expander_visible,
                check_view = Visibility.Hidden,
                image_file_path = string.Format("/I2MS2;component/Icons/{0}", image_name),
                port_no = 0,
                child_alarm_cnt = 0,
                is_child_alarm_visible = Visibility.Hidden,
                on_alarm = false,
                is_expanded = false,
                //type = getAssetTreeTypeLevel(ast),
                type = CatalogType.getCatalogType(ast.catalog_id),
                type_id = ast.asset_id
            };

            return ast_vm;
        }

        // 뷰모델 복사 - 소스에서 
        public AssetTreeVM cpAssetTreeTV(AssetTreeVM ast_vm_src)
        {
            return new AssetTreeVM()
            {
                asset_id = ast_vm_src.asset_id,
                location_id = ast_vm_src.location_id,
                asset_tree_id = ast_vm_src.asset_tree_id,
                disp_name = ast_vm_src.disp_name,
                disp_level = ast_vm_src.disp_level,
                disp_order = ast_vm_src.disp_order,
                is_expander_visible = ast_vm_src.is_expander_visible,
                check_view = ast_vm_src.check_view,
                image_file_path = ast_vm_src.image_file_path,
                type = ast_vm_src.type,
                type_id = ast_vm_src.type_id,
                port_no = ast_vm_src.port_no,
                is_child_alarm_visible = ast_vm_src.is_child_alarm_visible,
                child_alarm_cnt = ast_vm_src.child_alarm_cnt,
                on_alarm = ast_vm_src.on_alarm,

                parant_ast_vm = ast_vm_src.parant_ast_vm
            };
        }

        // 뷰모델 복사 - 소스에서 차일드 까지
        public AssetTreeVM cpAssetTreeTVWithChild(AssetTreeVM ast_vm_src)
        {
            AssetTreeVM ast_vm_result = new AssetTreeVM()
            {
                asset_id = ast_vm_src.asset_id,
                location_id = ast_vm_src.location_id,
                asset_tree_id = ast_vm_src.asset_tree_id,
                disp_name = ast_vm_src.disp_name,
                disp_level = ast_vm_src.disp_level,
                disp_order = ast_vm_src.disp_order,
                is_expander_visible = ast_vm_src.is_expander_visible,
                check_view = ast_vm_src.check_view,
                image_file_path = ast_vm_src.image_file_path,
                type = ast_vm_src.type,
                type_id = ast_vm_src.type_id,
                port_no = ast_vm_src.port_no,
                is_child_alarm_visible = ast_vm_src.is_child_alarm_visible,
                child_alarm_cnt = ast_vm_src.child_alarm_cnt,
                on_alarm = ast_vm_src.on_alarm,
                //child_list = ast_vm_src.child_list,
                parant_ast_vm = ast_vm_src.parant_ast_vm
            };

            if(ast_vm_src.child_list.Count>0)
            foreach(var child in ast_vm_src.child_list)
            {
                ast_vm_result.child_list.Add(cpAssetTreeTV(child));
            }

            return ast_vm_result;
        }

        // 자산 트리 레벨에서 레벨 이름 가져오기 
        private AssetTreeType getAssetTreeTypeLevel(asset_tree ast)
        {
            AssetTreeType type = AssetTreeType.Unknown;
            //int type_lv;
            //type_level
            //0:wold, 1:nation, 2:city, 3:site, 4:building, 5:floor, 6:room, 7:rack 
            //11:patchpanel, 12: i-patchpanel, 13:server, 14:cable, 15:i-cable, 
            //16:i-controller, 17:switch,  18:storage, 19:Consolidation Point
            //20:faceplate, 21:Mutoa Box, 22:Environment(Energy) Box
            //99:unkown


            // asset_tree type 
            if (ast.asset_id == null)
            {
                //location 
                location l = g.location_list.Find(at => at.location_id == ast.location_id);
                if (l == null)
                    return type;
                switch (l.location_level)
                {
                    case 0:
                        type = AssetTreeType.World;
                        break;
                    case 1:
                        type = AssetTreeType.Region1;
                        break;
                    case 2:
                        type = AssetTreeType.Region2;
                        break;
                    case 3:
                        type = AssetTreeType.Site;
                        break;
                    case 4:
                        type = AssetTreeType.Building;
                        break;
                    case 5:
                        type = AssetTreeType.Floor;
                        break;
                    case 6:
                        type = AssetTreeType.Room;
                        break;
                    case 7:
                        type = AssetTreeType.Rack;
                        break;
                }
            }
            else
            {
                asset tmp_asset = g.asset_list.Find(at => at.asset_id == ast.asset_id);
                type = CatalogType.getCatalogType(tmp_asset.catalog_id);
            }

            return type;
        }

        // 자산 트리 레벨에서 레벨 타입 가져오기 
        private int getTypeId(AssetTreeVM ast_vm)
        {
            location l = g.location_list.Find(at=> at.location_id == ast_vm.location_id);
            int type_id = -1;
            switch(ast_vm.type)
            {
                case AssetTreeType.Region1:
                    region1 r1 = g.region1_list.Find(at => at.region1_id == l.region1_id);
                    if (r1 != null)
                        type_id = r1.region1_id;
                    break;
                case AssetTreeType.Region2:
                    region2 r2 = g.region2_list.Find(at => at.region2_id == l.region2_id);
                    if (r2 != null)
                        type_id = r2.region2_id;
                    break;
                case AssetTreeType.Site:
                    site s = g.site_list.Find(at => at.site_id == l.site_id);
                    if (s != null)
                        type_id = s.site_id;
                    break;
                case AssetTreeType.Building:
                    building bd = g.building_list.Find(at => at.building_id == l.building_id);
                    type_id = bd.building_id;
                    break;
                case AssetTreeType.Floor:
                    floor fl = g.floor_list.Find(at => at.floor_id == l.floor_id);
                    type_id = fl.floor_id;
                    break;
                case AssetTreeType.Room:
                    room rm = g.room_list.Find(at => at.room_id == l.room_id);
                    type_id = rm.room_id;
                    break;
                case AssetTreeType.Rack:
                    rack rk = g.rack_list.Find(at => at.rack_id == l.rack_id);
                    type_id = rk.rack_id;
                    break;
                case AssetTreeType.Cable:
                case AssetTreeType.ConsolidationPoint:
                case AssetTreeType.EnviromentBox:
                case AssetTreeType.FacePlate:
                case AssetTreeType.i_Cable:
                case AssetTreeType.i_Controller:
                case AssetTreeType.i_PatchPanel:
                case AssetTreeType.MutoaBox:
                case AssetTreeType.PatchPanel:
                case AssetTreeType.Server:
                case AssetTreeType.Storage:
                case AssetTreeType.Switch:
                case AssetTreeType.SwitchCard:
                case AssetTreeType.SwitchSlot:
                case AssetTreeType.Unknown:
                    asset asst = g.asset_list.Find(at => at.asset_id == ast_vm.asset_id);
                    type_id = asst != null ? asst.asset_id : 0;
                    break;
            }

            return type_id;
            
        }


        // 자산 트리 모델에서 부모 노드 찾아오기 
        // 로케이션과 엣셋 틀림 
        public AssetTreeVM getParentAssetTreeVMinMD(AssetTreeVM ast_vm)
        {
            //location인 경우
            if (ast_vm.asset_id == null)
            {
                int location_id = ast_vm.location_id;
                location l = g.location_list.Find(at => at.location_id == location_id);
                location p_l;

                if (l == null) // DB \가 깨지느 경우 대비 romee 4/1  
                {
                    string a;
                    a = string.Format("DB ERROR ==> LOC ID : {0}", ast_vm.location_id);
                    Console.WriteLine(a);
                    return null;
                } 
                int p_level = ast_vm.disp_level - 1;
                switch (p_level)
                {
                    case 1:
                        region1 r1 = g.region1_list.Find(at => at.region1_id == l.region1_id);
                        if (r1 == null)
                            return null;
                        p_l = g.location_list.Find(at =>
                            (at.region1_id == r1.region1_id) && (at.region2_id == null));
                        break;
                    case 2:
                        region2 r2 = g.region2_list.Find(at => at.region2_id == l.region2_id);
                        if (r2 == null)
                            return null;
                        p_l = g.location_list.Find(at =>
                            (at.region2_id == r2.region2_id) && (at.site_id == null));
                        break;
                    case 3:
                        site s = g.site_list.Find(at => at.site_id == l.site_id);
                        if (s == null)
                            return null;
                        p_l = g.location_list.Find(at =>
                            (at.site_id == s.site_id) && (at.building_id == null));
                        break;
                    case 4:
                        building bd = g.building_list.Find(at => at.building_id == l.building_id);
                        if (bd == null)
                            return null;
                        p_l = g.location_list.Find(at =>
                            (at.building_id == bd.building_id) && (at.floor_id == null));
                        break;
                    case 5:
                        floor fl = g.floor_list.Find(at => at.floor_id == l.floor_id);
                        if (fl == null)
                            return null;
                        p_l = g.location_list.Find(at =>
                            (at.floor_id == fl.floor_id) && (at.room_id == null));
                        break;
                    case 6:
                        room rm = g.room_list.Find(at => at.room_id == l.room_id);
                        if (rm == null)
                            return null;
                        p_l = g.location_list.Find(at =>
                            (at.room_id == rm.room_id) && (at.rack_id == null));
                        break;
                    case 7:
                        rack rk = g.rack_list.Find(at => at.rack_id == l.rack_id);
                        if (rk == null)
                            return null;
                        p_l = g.location_list.Find(at => (at.rack_id == rk.rack_id));
                        break;
                    default:
                        return null;

                }
                if (g.location_ast_vm_dic.ContainsKey(p_l.location_id))
                    return g.location_ast_vm_dic[p_l.location_id];
            }
            // asset 인경우
            // 우선 부모 로케이션을 
            else
            {
                //1. 우선 해당 에셋이 포함된 location을 찾는다
                location l = g.location_list.Find(at => at.location_id == ast_vm.location_id);

                if (l == null) // DB \가 깨지느 경우 대비 romee 4/1  
                {
                    string a;
                    a = string.Format("DB ERROR ==> LOC ID : {0}", ast_vm.location_id);
                    Console.WriteLine(a);
                    return null;
                } 

                //끝단은 room(6) or rack(7) 이다
                int l_level = l.location_level;
                int cur_level = ast_vm.disp_level;
                int l_id = ast_vm.location_id;
                if (g.location_ast_vm_dic.ContainsKey(l_id) == false)
                    return null;

                //2. 해당 location과 ast_vm 과의 level 차이를 확인하여 각각 철리한다

                //2-1  level 차이r가 1 이면 location ast_vm 이 부모이다
                if ((cur_level - l_level) == 1)
                {
                    //location ast_vm
                    AssetTreeVM l_ast_vm = g.location_ast_vm_dic[l_id];
                    return l_ast_vm;
                }
                //2-2 1 이상인 경우 
                else
                {
                    switch (ast_vm.type)
                    {
                        case AssetTreeType.SwitchCard:
                            //card가 mount 된 switch asset_id로 찾을 수 있다
                            sw_card_config sw_c = g.sw_card_config_list.Find(at => at.sw_card_asset_id == ast_vm.asset_id);
                            if (sw_c == null) return null;

                            if (g.asset_ast_vm_dic.ContainsKey(sw_c.sw_asset_id))
                                return g.asset_ast_vm_dic[sw_c.sw_asset_id];

                            break;
                        case AssetTreeType.Port:
                            //port 인경우 asset_id는 부모의 asset_id 이므로 찾을 수 있다
                            if (g.asset_ast_vm_dic.ContainsKey(ast_vm.asset_id ?? 0))
                                return g.asset_ast_vm_dic[ast_vm.asset_id ?? 0];


                            break;
                        case AssetTreeType.PC:
                            asset_terminal ast_t = g.asset_terminal_list.Find(at => at.terminal_asset_id == ast_vm.asset_id);
                            if (ast_t == null) return null;

                            if (g.asset_ast_vm_dic.ContainsKey(ast_t.cur_outlet_asset_id ?? 0))
                            {
                                AssetTreeVM asset_vm = g.asset_ast_vm_dic[ast_t.cur_outlet_asset_id ?? 0];
                                AssetTreeVM port_vm = asset_vm.child_list.Find(at => at.port_no == ast_t.cur_outlet_port_no);
                                if (port_vm != null)
                                    return port_vm;
                            }
                            break;
                    }

                }
            }
            return null;
        }

        // 뷰 
        public AssetTreeVM getAssetTreeVMinMD(AssetTreeVM ast_vm_tv)
        {
            //location
            if (ast_vm_tv.asset_id == null)
            {
                if (g.location_ast_vm_dic.ContainsKey(ast_vm_tv.location_id))
                {
                    return g.location_ast_vm_dic[ast_vm_tv.location_id];
                }
            }
            //asset
            else
            {
                if (g.asset_ast_vm_dic.ContainsKey(ast_vm_tv.asset_id ?? 0))
                {
                    //일반 asset인 경우 바로 리턴
                    if (ast_vm_tv.type != AssetTreeType.Port)
                    {
                        return g.asset_ast_vm_dic[ast_vm_tv.asset_id ?? 0];
                    }
                    //port인경우 예외 처리
                    else
                    {
                        AssetTreeVM p_ast_vm = g.asset_ast_vm_dic[ast_vm_tv.asset_id ?? 0];
                        AssetTreeVM port_ast_vm = p_ast_vm.child_list.Find(at => at.port_no == ast_vm_tv.port_no);
                        if (port_ast_vm != null)
                            return port_ast_vm;
                    }
                }
            }
            return null;
        }

        // 뷰모델 복사 - 소스에서 차일드 까지  모든 -> 2단계 확장하여 처리  => 사용안함 
        public AssetTreeVM cpAssetTreeTVWithChildAll(AssetTreeVM ast_vm_src)
        {
            AssetTreeVM ast_vm_result = new AssetTreeVM()
            {
                asset_id = ast_vm_src.asset_id,
                location_id = ast_vm_src.location_id,
                asset_tree_id = ast_vm_src.asset_tree_id,
                disp_name = ast_vm_src.disp_name,
                disp_level = ast_vm_src.disp_level,
                disp_order = ast_vm_src.disp_order,
                is_expander_visible = ast_vm_src.is_expander_visible,
                check_view = ast_vm_src.check_view,
                image_file_path = ast_vm_src.image_file_path,
                type = ast_vm_src.type,
                type_id = ast_vm_src.type_id,
                port_no = ast_vm_src.port_no,
                is_child_alarm_visible = ast_vm_src.is_child_alarm_visible,
                child_alarm_cnt = ast_vm_src.child_alarm_cnt,
                on_alarm = ast_vm_src.on_alarm,
                //child_list = ast_vm_src.child_list,
                parant_ast_vm = ast_vm_src.parant_ast_vm
            };

            if (ast_vm_src.child_list.Count > 0)
                foreach (var child in ast_vm_src.child_list)
                {
                    ast_vm_result.child_list.Add(cpAssetTreeTVWithChild(child));
                }

            return ast_vm_result;
        }



    }
}
