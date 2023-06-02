using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I2MS2.UserControls;
using I2MS2.Models;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WebApi.Models;
using System.Windows;

namespace I2MS2.Library
{
    // 랙 관리 
    public class PortVM : INotifyPropertyChanged
    {
        #region PortVM 정의
        public int ipp_asset_id { get; set; }
        public int port_no { get; set; }
        public bool is_rear_cable { get; set; }
        public string alarm_status { get; set; }
        public string wo_status { get; set; }
        public string is_port_trace { get; set; }
        public string port_status { get; set; }
        public string cur_location_path { get; set; }
        public string cur_disp_name { get; set; }
        public string host_name { get; set; }
        public string ip_addr { get; set; }
        public string mac_addr { get; set; }
        public int front_asset_id { get; set; }
        public int front_port_no { get; set; }
        public string front_location_path { get; set; }
        public string front_disp_name { get; set; }
        public int rear_asset_id { get; set; }
        public int rear_port_no { get; set; }
        public string rear_location_path { get; set; }
        public string rear_disp_name { get; set; }

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
        #endregion
    }

    public class RackVM : INotifyPropertyChanged
    {
        #region RackVM 정의
        public int rack_config_id { get; set; }
        public int rack_id { get; set; }
        public int catalog_id { get; set; }
        public int catalog_group_id { get; set; }
        public int asset_id { get; set; }
        public int slot_no { get; set; }
        public string name { get; set; }
        public double unit_size { get; set; }
        public string bitmap_220_file_path { get; set; }
        public string bitmap_440_file_path { get; set; }
        public string bitmap_880_file_path { get; set; }
        public int image_220_id { get; set; }
        public int image_440_id { get; set; }
        public int image_880_id { get; set; }
        public bool is_mount { get; set; }
        public List<PortVM> port_list { get; set; }

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
        #endregion
    }

    public class RackLib
    {
        private List<RackVM> _rack_vm_list = null;
        RackControl _rack = null;
        int _rack_id = 0;
        int _num_of_slots;

        public RackLib(int rack_id, RackControl rack_control, List<RackVM> vm_list)
        {
            _rack_id = rack_id;
            _rack = rack_control;
            _rack_vm_list = vm_list;
        }

        public void dispRack()
        {
            int rack_id = _rack_id;

            var rr = g.rack_list.Find(p => p.rack_id == rack_id);
            if (rr == null)
                return;

            int rack_catalog_id = rr.rack_catalog_id ?? 0;
            if (rack_catalog_id == 0)
                return;

            var cc = g.catalog_list.Find(p => p.catalog_id == rack_catalog_id);
            if (cc == null)
                return;

            _num_of_slots = cc.rm_unit_size ?? 0;
            if (_num_of_slots == 0)
                return;

            int slot_no = _num_of_slots;
            int min = _num_of_slots;
            int last_catalog_id = 0;

            // 구성 리스트를 준비하여 바인딩
            _rack_vm_list.Clear();
            var list = g.rack_config_list.Where(p => (p.rack_id == rack_id) && (p.rack_mount_type == "S") && (p.asset_id > 0)).OrderByDescending(p => p.slot_no);

            if (list != null)
            {
                foreach (var node in list)
                {
                    if (node.asset_id > 0)
                    {
                        addAsset2Rack(rr.rack_id, node.slot_no, node.asset_id ?? 0, node.catalog_id ?? 0, node.rack_config_id);
                        if (min > node.slot_no)
                        {
                            min = node.slot_no;
                            last_catalog_id = node.catalog_id ?? 0;
                        }
                    }
                }
            }

            if (last_catalog_id > 0)
            {
                var f2 = g.catalog_list.Find(p => p.catalog_id == last_catalog_id);
                if (f2 != null)
                    slot_no = min -= f2.rm_unit_size ?? 1;
            }

            // 마운트되지 않은 자산을 추가함.

            var list2 = from at2 in g.asset_tree_list  
                        join a2 in g.asset_list on at2.asset_id equals a2.asset_id
                        join l2 in g.location_list on a2.location_id equals l2.location_id
                        where l2.rack_id == _rack_id
                        orderby at2.disp_order
                        select new asset { 
                            asset_id = a2.asset_id,
                            asset_name = a2.asset_name,
                            catalog_id = a2.catalog_id
                        };

            int added_cnt = 0;
            foreach(var node2 in list2)
            {
                var f1 = _rack_vm_list.Find(p => p.asset_id == node2.asset_id);
                if (f1 == null)
                {
                    var c2 = g.catalog_list.Find(p => p.catalog_id == node2.catalog_id);
                    if (c2 != null)
                    {
                        if (c2.rm_is_rack_mount == "Y")
                        {
                            if (c2.rm_unit_size > 0)
                            {
                                if (slot_no >= (c2.rm_unit_size ?? 1))
                                {
                                    addAsset2Rack(rr.rack_id, slot_no, node2.asset_id, node2.catalog_id, 0);
                                    slot_no -= c2.rm_unit_size ?? 1;
                                    added_cnt++;
                                }
                                else
                                {
                                    MessageBox.Show(g.tr_get("C_Error_Rack_Config_3"), g.tr_get("C_Info"), MessageBoxButton.OK, MessageBoxImage.Information);
                                    break;
                                }
                            }
                            else
                            {
                                if (!CatalogType.is_eb(c2.catalog_id))
                                {
                                    MessageBox.Show(string.Format(g.tr_get("C_Error_Rack_Config_5") + "asset_id={0}, name={1}", node2.asset_id, node2.asset_name),
                                        g.tr_get("C_Info"), MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                            }
                        }
                    }
                }
            }

            // 빈 공간 추가
            int cnt = _rack_vm_list.Count();
            int idx = 0;
            slot_no = _num_of_slots;
            RackVM vm2 = null;

            while (slot_no > 0)
            {
                // 리스트에 레코드가 없는 경우 추가.
                if (idx >= cnt)
                {
                    vm2 = new RackVM();
                    vm2.rack_id = rack_id;
                    vm2.unit_size = 1;
                    vm2.slot_no = slot_no;
                    _rack_vm_list.Add(vm2);
                    slot_no--;
                }
                else
                {
                    vm2 = _rack_vm_list[idx];

                    if (vm2.slot_no == slot_no)
                        slot_no -= (int)vm2.unit_size;
                    else
                    {
                        vm2 = new RackVM();
                        vm2.rack_id = rack_id;
                        vm2.unit_size = 1;
                        vm2.slot_no = slot_no;
                        _rack_vm_list.Insert(idx, vm2);
                        slot_no--;
                        cnt++;
                    }
                }
                idx++;
            }

            if (added_cnt > 0)
                MessageBox.Show(string.Format(g.tr_get("C_Error_Rack_Config_6") + "count={0}", added_cnt), 
                    "정보", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void addAsset2Rack(int rack_id, int slot_no, int asset_id, int catalog_id, int rack_config_id)
        {
            var vm = new RackVM();
            vm.rack_id = rack_id;
            vm.slot_no = slot_no;
            vm.is_mount = true;

            vm.rack_config_id = rack_config_id;
            vm.asset_id = asset_id;

            vm.catalog_id = catalog_id;
            var a = g.asset_list.Find(p => p.asset_id == asset_id);
            vm.name = a != null ? a.asset_name : "";

            double unit_size = 1;
            var c = g.catalog_list.Find(p => p.catalog_id == catalog_id);
            if (c != null)
            {
                unit_size = (double)(c.rm_unit_size ?? 0);
                vm.catalog_group_id = c.catalog_group_id;
                vm.image_220_id = c.rm_image_220_image_id ?? 0;
                vm.image_440_id = c.rm_image_440_image_id ?? 0;
                vm.image_880_id = c.rm_image_880_image_id ?? 0;
            }
            vm.unit_size = unit_size;

            var img_220 = g.sp_image_list.Find(p => p.image_id == vm.image_220_id);
            vm.bitmap_220_file_path = img_220 != null ? string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH, img_220.folder_name, img_220.file_name) : g.NULL_FILE_PATH;

            var img_440 = g.sp_image_list.Find(p => p.image_id == vm.image_440_id);
            vm.bitmap_440_file_path = img_440 != null ? string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH, img_440.folder_name, img_440.file_name) : g.NULL_FILE_PATH;

            var img_880 = g.sp_image_list.Find(p => p.image_id == vm.image_880_id);
            vm.bitmap_880_file_path = img_880 != null ? string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH, img_880.folder_name, img_880.file_name) : g.NULL_FILE_PATH;

            addPortInfo(vm);
            _rack_vm_list.Add(vm);
        }

        public bool moveAsset(RackVM dest_vm, RackVM source_vm, int dest_idx, int source_idx)
        {
            //int i = 0;
            if (source_idx < 0)
                return false;
            if (dest_idx < 0)
                return false;

            int unit_size = (int)source_vm.unit_size;
            int dest_slot_no = dest_vm.slot_no;
            int source_slot_no = source_vm.slot_no;

            if (source_vm.asset_id <= 0)
                return false;

            // 소스 아이템 제거
            _rack_vm_list.Remove(source_vm);
            _rack_vm_list.Remove(dest_vm);

            // 위에서 밑으로 이동 시
            if (source_idx < dest_idx)
            {
                _rack_vm_list.Insert(source_idx, dest_vm);
                _rack_vm_list.Insert(dest_idx, source_vm);
            }
            else
            {
                _rack_vm_list.Insert(dest_idx, source_vm);
                _rack_vm_list.Insert(source_idx, dest_vm);
            }

            int slot_no = _num_of_slots;
            foreach(var node in _rack_vm_list)
            {
                node.slot_no = slot_no;
                slot_no -= (int) node.unit_size;
            }

            return true;
        }

        public RackVM getRackVMAsset(int asset_id)
        {
            RackVM ast = _rack_vm_list.Find(p => p.asset_id == asset_id);
            if (ast != null)
                return ast;
            else
                return null;
        }

        public RackVM findEmptySlot()
        {
            int idx = 0;
            foreach (var node in _rack_vm_list)
            {
                if ((node.asset_id == 0) && (node.catalog_id == 0))
                    return node;
                idx++;
            }
            return null;
        }

        public int findLastSlot()
        {
            int idx = 0;
            foreach (var node in _rack_vm_list)
            {
                if ((node.asset_id == 0) && (node.catalog_id == 0))
                    return idx - 1;
                idx++;
            }
            return -1;
        }

        public void deleteLastSlot()
        {
            int idx = findLastSlot();
            // no record?
            if (idx == -1)
                return;

            RackVM vm = _rack_vm_list[idx];

            int cnt = (int) vm.unit_size;
            int slot_no = vm.slot_no;

            int i = 0;
            for (i = idx; i < idx + cnt; i++)
            {
                clearVM(_rack_vm_list[i], slot_no--);
            }
        }

        public void deleteSlot(int slot_no)
        {
            if (slot_no < 1)
                return;

            int max_slot = _rack_vm_list[0].slot_no;

            int idx = 0;

            int i = 0;
            bool find = false;
            for (i = 0; i < _rack_vm_list.Count(); i++)
            {
                if (_rack_vm_list[i].slot_no == slot_no)
                {
                    find = true;
                    break;
                }
                idx++;
            }

            if (!find)
                return;

            // no record?
            if (idx < 0)
                return;

            RackVM vm = _rack_vm_list[idx];
            int cnt = (int)vm.unit_size;
            clearVM(vm, slot_no);

            //  유닛 사이즈만큼 삭제
            int slot_no2 = slot_no;
            for (i = (idx + 1); i < (idx + cnt); i++)
            {
                var vm2 = new RackVM();
                clearVM(vm2, --slot_no2);
                addPortInfo(vm2);
                _rack_vm_list.Insert(i, vm2);
            }
        }

        public void clearVM(RackVM vm, int slot_no)
        {
            vm.asset_id = 0;
            vm.catalog_id = 0;
            vm.unit_size = 1;
            vm.image_220_id = 0;
            vm.image_440_id = 0;
            vm.image_880_id = 0;
            vm.bitmap_220_file_path = g.NULL_FILE_PATH;
            vm.bitmap_440_file_path = g.NULL_FILE_PATH;
            vm.bitmap_880_file_path = g.NULL_FILE_PATH;
            vm.rack_config_id = 0;
            vm.slot_no = slot_no;
            vm.unit_size = 1;
            vm.port_list = new List<PortVM>();
            vm.force_changed = true;
        }

        private void addPortInfo(RackVM vm)
        {
            if (!CatalogType.is_ipp(vm.catalog_id))
                return;
            int ipp_asset_id = vm.asset_id;

            var list = from pp in g.asset_ipp_port_link_list.Where(p => p.ipp_asset_id == ipp_asset_id)
                       join a in g.asset_list on pp.ipp_asset_id equals a.asset_id
                       join l in g.location_list on a.location_id equals l.location_id
                       select new PortVM
                       {
                           ipp_asset_id = ipp_asset_id,
                           port_no = pp.port_no,
                           alarm_status = pp.alarm_status,
                           wo_status = pp.wo_status,
                           is_port_trace = pp.is_port_trace,
                           port_status = pp.ipp_port_status,
                           cur_location_path = l.location_path,
                           cur_disp_name = string.Format("{0}/{1}", a.asset_name, pp.port_no),
                       };

            vm.port_list = list.ToList();

            foreach (var node in vm.port_list)
            {
                update_link_info(node);
            }            
        }

        private void update_link_info(PortVM node)
        {
            var cur = g.asset_port_link_list.Find(p => (p.asset_id == node.ipp_asset_id) && (p.port_no == node.port_no));
            if (cur != null)
            {
                var front = g.asset_port_link_list.Find(p => (p.asset_id == cur.front_asset_id) && (p.port_no == cur.front_port_no));
                if (front != null)
                {
                    node.front_asset_id = front.asset_id;
                    node.front_port_no = front.port_no;
                    var a1 = g.asset_list.Find(p => p.asset_id == front.asset_id);
                    if (a1 != null)
                    {
                        node.front_disp_name = string.Format("{0}/{1}", a1.asset_name, front.port_no);
                        var l1 = g.location_list.Find(p => p.location_id == a1.location_id);
                        if (l1 != null)
                            node.front_location_path = l1.location_path;
                    }
                }
                var rear = g.asset_port_link_list.Find(p => (p.asset_id == cur.rear_asset_id) && (p.port_no == cur.rear_port_no));
                if (rear != null)
                {
                    node.rear_asset_id = rear.asset_id;
                    node.rear_port_no = rear.port_no;
                    node.is_rear_cable = true;
                    var a2 = g.asset_list.Find(p => p.asset_id == rear.asset_id);
                    if (a2 != null)
                    {
                        node.rear_disp_name = string.Format("{0}/{1}", a2.asset_name, rear.port_no);
                        var l2 = g.location_list.Find(p => p.location_id == a2.location_id);
                        if (l2 != null)
                            node.rear_location_path = l2.location_path;
                    }
                }
                // Jake, 향후 ip 정보를 채워야 함.
            }
        }

        private void update_port_vm_info(PortVM node)
        {
            int asset_id = node.ipp_asset_id;
            int port_no = node.port_no;
            var aipl = g.asset_ipp_port_link_list.Find(p => (p.ipp_asset_id == asset_id) && (p.port_no == port_no));
            node.alarm_status = aipl != null ? aipl.alarm_status : "";
            node.wo_status = aipl != null ? aipl.wo_status : "";
            node.is_port_trace = aipl != null ? aipl.is_port_trace : "";
            node.port_status = aipl != null ? aipl.ipp_port_status : "";

            update_link_info(node);
            node.force_changed = true;
        }

        // 포트 상태가 바뀌면 이 루틴이 호출된다.
        public void update_port_status(int asset_id, int port_no)
        {
            if (_rack_vm_list == null)
                return;
            foreach(var node in _rack_vm_list)
            {
                if (node.asset_id == asset_id)
                {
                    if (node.port_list != null)
                    {
                        var node2 = node.port_list.Find(p => p.port_no == port_no);
                        update_port_vm_info(node2);
                    }
                }
            }            
        }

        // 알람 상태가 바뀌면 이 루틴이 호출된다.
        public void update_alarm_status(int asset_id, int port_no)
        {
            update_port_status(asset_id, port_no);
        }

    }
}
