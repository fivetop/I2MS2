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
using I2MS2.Library;
using WebApiClient;
using System.Globalization;
using System.ComponentModel;
using System.Diagnostics;

namespace I2MS2.Windows
{
    // 로케이션, 자산을 복사, 대량복사를 관리 
    /// <summary>
    /// AssetManager.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CloneManager : Window
    {
        class CloneVM
        {
            public int sn { get; set; }
            public string name { get; set; }
            public string result { get; set; }
            public bool make_done { get; set; }     // 저장 시 사용되며 저장이 성공된 항목만 true로 변경...
            public int floor_no { get; set; }
        }

        private int _asset_id = 0;
        private int _dest_sw_slot_asset_id = 0;
        private int _location_id = 0;
        private int _dest_location_id = 0;
        List<CloneVM> _vm_list = new List<CloneVM>();

        public static RoutedCommand SaveCommand = new RoutedCommand();

        public CloneManager(int asset_id, int dest_sw_slot_asset_id, int location_id, int dest_location_id)
        {
            //g.result_asset_id = 0;
            //_new_flag = asset_id == 0;
            _asset_id = asset_id;
            _dest_sw_slot_asset_id = dest_sw_slot_asset_id;
            _location_id = location_id;
            _dest_location_id = dest_location_id;
            InitializeComponent();

            initData();         
        }

        private bool is_floor()
        {
            if (_asset_id > 0)
                return false;
            var l = g.location_list.Find(p => p.location_id == _location_id);
            if (l == null)
                return false;
            return l.location_level == 5; 
        }

        #region 신규,삭제 등 버튼 처리 로직
        private void _cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _vm_list.Count > 0;
        }

        private async void _cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!await saveData())
                return;

            DialogResult = true;
            //g.result_asset_id = _asset_vm.asset_id;
            Close();
        }

        #endregion

        // 창에 내용을 출력한다.
        private void initData()
        {
            if (is_floor())
            {
                _sp11.Visibility = Visibility.Hidden;
                _sp12.Visibility = Visibility.Hidden;
                _sp11.Height = 0;
                _sp12.Height = 0;
            }
            else
            {
                _sp21.Visibility = Visibility.Hidden;
                _sp22.Visibility = Visibility.Hidden;
                _sp21.Height = 0;
                _sp22.Height = 0;
            }

            // 복사할 원본 이름을 구해온다.
            string name = "";
            if (_asset_id == 0)
            {
                var lo = g.location_list.Find(p => p.location_id == _location_id);
                if (lo == null)
                    return;
                name = lo.location_name;
            }
            else 
            {
                var a = g.asset_list.Find(p => p.asset_id == _asset_id);
                if (a == null)
                    return;
                name = a.asset_name;
            }
            _txtSource.Text = name;

            // 복사할 대상 위치 이름을 구해온다.
            var lo2 = g.location_list.Find(p => p.location_id == _dest_location_id);
            if (lo2 == null)
                return;
            if (_dest_sw_slot_asset_id == 0)
                _txtDest.Text = lo2.location_name;
            else
            {
                string sw_slot_asset_name = Etc.get_asset_name(_dest_sw_slot_asset_id);
                _txtDest.Text = sw_slot_asset_name;
            }

            // 복사할 수량을 처음에 1로....
            _txtQty.Text = "1";

            start_parse();
            disp_result();
        }

        private int _start_idx1= 0;     // 첫 번째 숫자가 시작하는 문자열 시작 위치
        private int _length1 = 0;       // 길이
        private int _start_idx2 = 0;    // 두 번째 숫자가 시작하는 문자열 시작 위치
        private int _length2 = 0;
        private int _start_idx3 = 0;    // 세 번째 숫자가 시작하는 문자열 시작 위치
        private int _length3 = 0;

        int _start_no1 = 0;     // 시작 번호
        int _digit_no1 = 0;     // 만들 숫자 자릿 수
        int _step_no1 = 0;      // 증분 번호
        int _start_no2 = 0;
        int _digit_no2 = 0;     // 만들 숫자 자릿 수
        int _step_no2 = 0;
        int _start_no3 = 0;
        int _digit_no3 = 0;     // 만들 숫자 자릿 수
        int _step_no3 = 0;

        string _name;

        private void start_parse()
        {
            _name = _txtSource.Text.Trim();
            bool b = false;

            // 플로어에서만 예외 처리....
            if (is_floor())
            {
                b = parseToken(_name, 1, out _start_idx1, out _length1);
                _digit_no1 = _length1;
                if (!b)
                {
                    _start_idx1 = 0;
                    _length1 = 0;
                    _digit_no1 = 0;
                    return;
                }
                int startFloor = Etc.get_int(_name.Substring(_start_idx1, _length1));
                _txtStartFloor.Text = (startFloor + 1).ToString();
                _txtDigitFloor.Text = _digit_no1.ToString();
                _txtStepFloor.Text = "1";
            }

            // 플로어를 제외한...

            b = parseToken(_name, 1, out _start_idx1, out _length1);
            _digit_no1 = _length1;
            if (!b)
            {
                _start_idx1 = 10;
                _length1 = 0;
                _digit_no1 = 0;
                _txtStart2.IsEnabled = false;
                _txtDigit2.IsEnabled = false;
                _txtStep2.IsEnabled = false;
                _txtStart3.IsEnabled = false;
                _txtDigit3.IsEnabled = false;
                _txtStep3.IsEnabled = false;
                return;
            }
            int start1 = Etc.get_int(_name.Substring(_start_idx1, _length1));
            _txtStart1.Text = (start1 + 1).ToString();
            _txtDigit1.Text = _digit_no1.ToString();
            _txtStep1.Text = "1";

            b = parseToken(_name, 2, out _start_idx2, out _length2);
            _digit_no2 = _length2;
            if (!b)
            {
                _start_idx2 = 0;
                _length2 = 0;
                _digit_no2 = 0;
                _txtStart2.IsEnabled = false;
                _txtDigit2.IsEnabled = false;
                _txtStep2.IsEnabled = false;
                _txtStart3.IsEnabled = false;
                _txtDigit3.IsEnabled = false;
                _txtStep3.IsEnabled = false;
                return;
            }
            int start2 = Etc.get_int(_name.Substring(_start_idx2, _length2));
            _txtStart2.Text = (start2 + 1).ToString();
            _txtDigit1.Text = _digit_no2.ToString();
            _txtStep2.Text = "1";

            b = parseToken(_name, 3, out _start_idx3, out _length3);
            _digit_no3 = _length3;
            if (!b)
            {
                _start_idx3 = 0;
                _length3 = 0;
                _digit_no3 = 0;
                _txtStart3.IsEnabled = false;
                _txtDigit3.IsEnabled = false;
                _txtStep3.IsEnabled = false;
                return;
            }
            int start3 = Etc.get_int(_name.Substring(_start_idx3, _length3));
            _txtStart3.Text = (start3 + 1).ToString();
            _txtDigit3.Text = _digit_no3.ToString();
            _txtStep3.Text = "1";
        }

        private void disp_result()
        {
            int asset_id = _asset_id;
            int location_id = _location_id;
            int err_cnt = 0;
            if (is_floor())
            {
                _start_no1 = Etc.get_int(_txtStartFloor.Text);
                _digit_no1 = Etc.get_int(_txtDigitFloor.Text);
                _step_no1 = Etc.get_int(_txtStepFloor.Text);
                _start_no2 = 0;
                _digit_no2 = 0;
                _step_no2 = 0;
                _start_no3 = 0;
                _digit_no3 = 0;
                _step_no3 = 0;
            }
            else
            {
                _start_no1 = Etc.get_int(_txtStart1.Text);
                _digit_no1 = Etc.get_int(_txtDigit1.Text);
                _step_no1 = Etc.get_int(_txtStep1.Text);
                _start_no2 = Etc.get_int(_txtStart2.Text);
                _digit_no2 = Etc.get_int(_txtDigit2.Text);
                _step_no2 = Etc.get_int(_txtStep2.Text);
                _start_no3 = Etc.get_int(_txtStart3.Text);
                _digit_no3 = Etc.get_int(_txtDigit3.Text);
                _step_no3 = Etc.get_int(_txtStep3.Text);
            }


            var l = g.location_list.Find(p => p.location_id == location_id);
            if (l == null)
                return;
            int building_id = l.building_id ?? 0;
            if (building_id == 0)
                return;

            _vm_list.Clear();
            int i = 0;
            int len = Etc.get_int(_txtQty.Text);        // 만들 수량...

            for (i = 0; i < len; i++)
            {
                CloneVM vm = new CloneVM(){
                    sn = i + 1
                };
                string name = make_name(i);
                vm.name = name;
                // 플로어 번호는 시작번호와 같이...
                if (is_floor())
                {
                    vm.floor_no = i * _step_no1 + _start_no1;
                }

                if (is_floor() && (g.floor_list.Find(p => (p.floor_no == vm.floor_no) && (p.building_id == building_id)) != null))
                {
                    vm.result = "Floor Dup";
                    err_cnt++;
                }
                else if (_vm_list.Find(p => p.name == name) != null)
                {
                    vm.result = "Duplacation";
                    err_cnt++;
                }
                else if (asset_id == 0)
                {
                    // 같은 빌딩 내에서 이름이 중복되면 안된다. (자산 생성은 Floor 부터 가능...)
                    var lo = g.location_list.Find(p => (p.location_name == name) && (p.building_id == building_id));
                    if (lo != null)
                    {
                        vm.result = "Duplacation";
                        err_cnt++;
                    }
                    else
                        vm.result = "OK";
                }
                else
                {
                    var list = from aa in g.asset_list.Where(p => p.asset_name == name)
                               join bb in g.location_list.Where(p => p.site_id == g.selected_site_id) on aa.location_id equals bb.location_id
                               select new { aa.asset_id };
                    if (list.Count() > 0)
                    {
                        vm.result = "Duplacation";
                        err_cnt++;
                    }
                    else
                        vm.result = "OK";
                }
                _vm_list.Add(vm);
            }

            _lv.ItemsSource = null;
            _lv.ItemsSource = _vm_list;

            if (err_cnt > 0)
                _txtErr.Foreground = App.Current.Resources["_brushRed"] as Brush;
            else
                _txtErr.Foreground = App.Current.Resources["_brushNormalText"] as Brush;
            _txtErr.Text = err_cnt.ToString();
        }

        private string make_name(int idx)
        {
            // ABC##DEF##GHI##JKL
            string text1 = "";
            string no1 = "";
            string text2 = "";
            string no2 = "";
            string text3 = "";
            string no3 = "";
            string text4 = "";
            string name = "";


            // 첫 번째 숫자도 없는 경우....
            if (_length1 == 0)
                return string.Format("{0}_{1:000}", _name, _start_no1 + _step_no1 * idx);

            text1 = _name.Substring(0, _start_idx1);
            no1 = Etc.get_string(_start_no1 + _step_no1 * idx, _digit_no1);
            // 두 번째 숫자가 없는 경우
            if (_length2 == 0)
            {
                text2 = _name.Substring(_start_idx1 + _length1);
                name = string.Format("{0}{1}{2}", text1, no1, text2);
                return name;
            }

            no2 = Etc.get_string(_start_no2 + _step_no2 * idx, _digit_no2);
            text2 = _name.Substring(_start_idx1 + _length1, _start_idx2 - _start_idx1 - _length1);

            // 세 번째 숫자가 없는 경우
            if (_length3 == 0)
            {
                text3 = _name.Substring(_start_idx2 + _length2);
                name = string.Format("{0}{1}{2}{3}{4}", text1, no1, text2, no2, text3);
                return name;
            }

            no3 = Etc.get_string(_start_no3 + _step_no3 * idx, _digit_no3);
            text3 = _name.Substring(_start_idx2 + _length2, _start_idx3 - _start_idx2 - _length2);
            text4 = _name.Substring(_start_idx3 + _length3);
            name = string.Format("{0}{1}{2}{3}{4}{5}{6}", text1, no1, text2, no2, text3, no3, text4);
            return name;
        }

        // sn_idx : 1부터 시작....
        // start_idx : 발견된 숫자 문자열의 인덱스로 0부터 가능
        // length : 발견된 숫자 문자열의 길이
        private bool parseToken(string source, int sn_idx, out int start_idx, out int length)
        {
            int len = source.Length;

            int i;
            int idx = 1;
            bool start_numeric = false;
            start_idx = 0;
            length = 0;
            for (i = 0; i < len; i++)
            {
                string c = source.Substring(i, 1);
                if (is_numeric(c))
                {
                    if (start_numeric)
                    {
                        length++;
                    }
                    else
                    {
                        start_numeric = true;
                        start_idx = i;
                        length = 1;
                    }
                }
                else
                {
                    if (start_numeric)
                    {
                        start_numeric = false;
                        if (idx == sn_idx)
                            return true;
                        idx++;
                    }
                }
            }
            if ((len == (start_idx + length)) && (idx == sn_idx))
                return true;
            return false;
        }

        private bool is_numeric(string source)
        {
            int num = 0;
            bool is_num = int.TryParse(source, out num);
            return is_num;
        }


        private async Task<bool> saveData()
        {
            _btnSave.IsEnabled = false;
            _btnCancel.IsEnabled = false;
            foreach (var vm in _vm_list)
            {
                if ((vm.result == "OK") && !vm.make_done)
                {
                    this.Cursor = Cursors.Wait;
                    bool b = false;
                    if (_asset_id == 0)
                        b = await make_clone_location(_location_id, vm, _dest_location_id);
                    else
                        b = await make_clone_asset(_asset_id, vm, _dest_location_id, _dest_sw_slot_asset_id);
                    this.Cursor = null;

                    if (!b)
                        return false;
                }
            }

            return true;
        }

        private async Task<bool> make_clone_location(int location_id, CloneVM vm, int dest_location_id)
        {
            var lo = g.location_list.Find(p => p.location_id == location_id);
            if (lo == null)
                return false;
            int level = lo.location_level;
            switch(level)
            {
                case 5 : 
                    // floor
                    return await make_clone_floor(lo.floor_id ?? 0, vm, dest_location_id);
                case 6:
                    // floor
                    return await make_clone_room(lo.room_id ?? 0, vm, dest_location_id);
                case 7:
                    // floor
                    return await make_clone_rack(lo.rack_id ?? 0, vm, dest_location_id);
                default:
                    return false;
            }
        }

        private async Task<bool> make_clone_floor(int floor_id, CloneVM vm, int dest_location_id)
        {
            string new_floor_name = vm.name;
            int new_floor_no = vm.floor_no;

            var s = g.floor_list.Find(p => p.floor_id == floor_id);
            if (s == null)
                return false;

            floor new_floor = new floor()
            {
                building_id = s.building_id,
                drawing_3d_id = s.drawing_3d_id,
                floor_name = new_floor_name,
                floor_no = new_floor_no,
                user_id = g.login_user_id
            };

            // Step 1. DB 생성(foor, location, asset_tree

            var add = (floor)await g.webapi.post("floor", new_floor, typeof(floor));
            if (add == null)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }
            int new_floor_id = add.floor_id;

            // Step 2. 메모리에 추가
            g.floor_list.Add(add);

            // 즐겨찾기트리와 위치트리 메모리에 추가하고 화면 갱신
            bool b = await g.left_tree_handler.addFloor(new_floor_id);
            if (!b)
                return false;

            return true;
        }

        private async Task<bool> make_clone_room(int room_id, CloneVM vm, int dest_location_id)
        {
            string new_room_name = vm.name;

            var s = g.room_list.Find(p => p.room_id == room_id);
            if (s == null)
                return false;

            // 2015.06.24 룸 생성시 기본 위치 가져가기 처리 romee
            room new_room = new room()
            {
                floor_id = s.floor_id,
                room_name = new_room_name,
                user_id = g.login_user_id,
                square_x1 = ScreenRatio.getDbX(50, 800),
                square_y1 = ScreenRatio.getDbY(50, 600),
                square_x2 = ScreenRatio.getDbX(290, 800),
                square_y2 = ScreenRatio.getDbY(230, 600),
                flag_x = 21760,
                flag_y = 17920
            };

            // Step 1. DB 생성(room, location, asset_tree

            var add = (room)await g.webapi.post("room", new_room, typeof(room));
            if (add == null)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }
            int new_room_id = add.room_id;

            // Step 2. 메모리에 추가
            g.room_list.Add(add);

            // 즐겨찾기트리와 위치트리 메모리에 추가하고 화면 갱신
            bool b = await g.left_tree_handler.addRoom(new_room_id);
            if (!b)
                return false;

            return true;
        }

        private async Task<bool> make_clone_rack(int rack_id, CloneVM vm, int dest_location_id)
        {
            string new_rack_name = vm.name;

            var s = g.rack_list.Find(p => p.rack_id == rack_id);
            if (s == null)
                return false;

            rack new_rack = new rack()
            {
                room_id = s.room_id,
                rack_name = new_rack_name,
                rack_catalog_id = s.rack_catalog_id,
                vcm_l_catalog_id = s.vcm_l_catalog_id,
                vcm_r_catalog_id = s.vcm_r_catalog_id,
                user_id = g.login_user_id
            };

            // Step 1. DB 생성(rack, location, asset_tree

            var add = (rack)await g.webapi.post("rack", new_rack, typeof(rack));
            if (add == null)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }
            int new_rack_id = add.rack_id;

            // Step 2. 메모리에 추가
            g.rack_list.Add(add);

            // 즐겨찾기트리와 위치트리 메모리에 추가하고 화면 갱신
            bool b = await g.left_tree_handler.addRack(new_rack_id);
            if (!b)
                return false;

            return true;
        }

        private async Task<bool> make_clone_asset(int asset_id, CloneVM vm, int dest_location_id, int dest_sw_slot_asset_id)
        {
            var s = g.asset_list.Find(p => p.asset_id == asset_id);
            if (s == null)
                return false;
            var s2 = g.asset_aux_list.Find(p => p.asset_id == asset_id);
            if (s2 == null)
                return false;

            bool sw_card_flag = CatalogType.is_sw_card(s.catalog_id);

            string new_asset_name = vm.name;

            string rack_mount_type = "";
            int slot_no2 = 0;

            // 스위치 카드는 랙슬롯에 영향을 받지 않는다.
            if (!sw_card_flag)
            {
                if (Etc.is_rack(dest_location_id))
                {
                    var b3 = g.left_tree_handler.check_rack_slot(dest_location_id, s.catalog_id, out rack_mount_type, out slot_no2);
                    if (!b3)
                        return false;
                }
            }

            asset new_asset = new asset()
            {
                 asset_name = new_asset_name,
                 catalog_id = s.catalog_id,
                 install_date = s.install_date,
                 last_updated = DateTime.Now,
                 location_id = dest_location_id,
                 install_user_name = s.install_user_name,
                 user_id = g.login_user_id,
                 is_layout = "N"
            };

            // Step 1. 각종 DB 생성(asset, location, asset_tree, .....

            var add = (asset)await g.webapi.post("asset", new_asset, typeof(asset));
            if (add == null)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }
            int new_asset_id = add.asset_id;

            // Step 2. 메모리에 추가
            g.asset_list.Add(add);

            var aa = (asset_aux) await g.webapi.get("asset_aux", new_asset_id, typeof(asset_aux));
            if (aa == null)
                return false;

            aa.as_company = s2.as_company;
            aa.as_duration = s2.as_duration;
            aa.as_end_date = s2.as_end_date;
            aa.as_free_duration = s2.as_free_duration;
            aa.as_free_end_date = s2.as_free_end_date;
            aa.as_free_start_date = s2.as_free_start_date;
            aa.as_management_div = s2.as_management_div;
            aa.as_management_user_name = s2.as_management_user_name;
            aa.as_price = s2.as_price;
            aa.as_start_date = s2.as_start_date;
            aa.bu_depreciation_duration = s2.bu_depreciation_duration;
            aa.bu_depreciation_end_year = s2.bu_depreciation_end_year;
            aa.bu_depreciation_start_year = s2.bu_depreciation_start_year;
            aa.bu_purchase_date = s2.bu_purchase_date;
            aa.bu_purchase_user_name = s2.bu_purchase_user_name;
            aa.ra_vcm_depth = s2.ra_vcm_depth;
            aa.ra_vcm_type = s2.ra_vcm_type;
            aa.snmp_get_community = s2.snmp_get_community;
            aa.snmp_set_community = s2.snmp_set_community;
            aa.snmp_trap_svr_ip = s2.snmp_trap_svr_ip;
            aa.snmp_v3_password = s2.snmp_v3_password;
            aa.snmp_v3_user = s2.snmp_v3_user;
            aa.snmp_version = s2.snmp_version;
            aa.st_cur_disk_amount = s2.st_cur_disk_amount;
            aa.st_cur_num_of_disks = s2.st_cur_num_of_disks;
            aa.st_type = s2.st_type;
            aa.sv_host_name = s2.sv_host_name;
            aa.sv_kind_of_os = s2.sv_kind_of_os;
            aa.sv_num_of_disks = s2.sv_num_of_disks;
            aa.sv_num_of_nic = s2.sv_num_of_nic;
            aa.sv_os_ver = s2.sv_os_ver;
            aa.sv_tot_disk_amount = s2.sv_tot_disk_amount;
            aa.sw_vlan = s2.sw_vlan;
            //aa.sw_max_slots = s2.sw_max_slots;

            var r1 = await g.webapi.put("asset_aux", new_asset_id, aa, typeof(asset_aux));
            if (r1 != 0)
                return false;

            // 메모리에 추가
            g.asset_aux_list.Add(aa);

            // asset_ext도 추가해야 함.
            string filter = string.Format("?asset_id={0}", new_asset_id);
            var list = (List<asset_ext>) await g.webapi.getList("asset_ext", typeof(List<asset_ext>), filter);
            if (list != null)
            {
                foreach(var ae in list)
                {
                    g.asset_ext_list.Add(ae);
                }
            }

            if (sw_card_flag)
            {
                var list2 = g.sw_card_config_list.Where(p => (p.sw_asset_id == dest_sw_slot_asset_id) && !(p.sw_card_asset_id > 0)).OrderBy(p => p.slot_no).ToList();
                // 스위치슬롯갯수를 초과한 경우 무시...
                if (list2.Count() != 0)
                {
                    var scc = list2[0];
                    scc.sw_card_asset_id = new_asset_id;
                    var r = await g.webapi.put("sw_card_config", scc.sw_card_config_id, scc, typeof(sw_card_config));
                    if (r != 0)
                    {
                        MessageBox.Show(g.tr_get("C_Error_Server"));
                        return false;
                    }
                }
            }

            // 즐겨찾기트리와 위치트리 메모리에 추가하고 화면 갱신
            bool b = await g.left_tree_handler.addAsset(new_asset_id);
            if (!b)
                return false;

            if (!sw_card_flag)
            {
                // rack_config 를 여기서...
                int rack_id = Etc.get_rack_id_by_location_id(dest_location_id);
                if (rack_id > 0)
                {
                    bool b4 = await g.left_tree_handler.add_to_rack_config(rack_id, rack_mount_type, slot_no2, new_asset_id, s.catalog_id);
                    if (!b4)
                        return false;
                }
            }

            // 다른 사용자에게도 알린다.
            g.signalr.send_asset_to_signalr(new_asset_id, eAction.eAdd);

            return true;
        }

    

 

        private void _btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _start_no1 = Etc.get_int(_txtStart1.Text);
            _step_no1 = Etc.get_int(_txtStep1.Text);
            _start_no2 = Etc.get_int(_txtStart2.Text);
            _step_no2 = Etc.get_int(_txtStep2.Text);
            _start_no3 = Etc.get_int(_txtStart3.Text);
            _step_no3 = Etc.get_int(_txtStep3.Text);

            disp_result();
        }

    }
}
