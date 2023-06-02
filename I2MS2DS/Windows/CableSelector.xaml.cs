using System;
using System.Collections;
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
using System.ComponentModel;
using System.Globalization;

namespace I2MS2.Windows
{
    /// <summary>
    /// CatalogGroupManager.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public class CableVM : INotifyPropertyChanged
    {
        public int catalog_id { get; set; }
        public string catalog_name { get; set; }
        public Color cable_color { get; set; }
        public Color cable_color2 { get; set; }
        public string cable_disp_name { get; set; }
        public bool is_utp { get; set; }
        public bool is_shield { get; set; }
        public bool is_intelligent { get; set; }
        public bool is_patch { get; set; }
        public bool is_enabled { get; set; }

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

    public partial class CableSelector : Window
    {
        public static RoutedCommand OkCommand = new RoutedCommand();

        private bool _ok = true;
        public CableVM _vm = null;

        private List<CableVM> _cable_list = new List<CableVM>();
        WorkCell _left = null;
        WorkCell _right = null;

        public CableSelector(WorkCell left, WorkCell right)
        {
            _left = left;
            _right = right;
            InitializeComponent();
            initData();
        }

        private void _cmdOk_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _ok;
        }

        private void _cmdOk_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CableVM vm = (CableVM)_lvCable.SelectedItem;
            if (vm == null)
                return;

            _vm = vm;
            DialogResult = true;
            Close();
        }

        private void _lvCable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CableVM vm = (CableVM)_lvCable.SelectedItem;

            _ok = false;

            if (vm == null)
                return;

            _ok = vm.is_enabled;
        }

        private void initData()
        {
            var list = g.catalog_list.Where(p => CatalogType.is_ca(p.catalog_id));
            _cable_list.Clear();
            int choice = -1;
            int cnt = 0;
            foreach(var node in list)
            {
                CableVM vm = new CableVM()
                {
                    cable_disp_name = node.ca_disp_name,
                    catalog_id = node.catalog_id,
                    catalog_name = node.catalog_name,
                    is_enabled = true,
                    is_intelligent = node.ca_use_intelligent == "Y",
                    is_shield = node.ca_is_utp_shield == "Y",
                    is_utp = node.ca_media_type == "U",
                    is_patch = CatalogType.is_ca_patch(node.catalog_id),
                    cable_color = CatalogType.get_color_rgba((uint)(node.ca_disp_color_rgb ?? 0x00ffffff)),
                    cable_color2 = Colors.Black
                };

                // 인텔리전트 패치 전면에 인텔리전트 케이블을 사용하지 않은 경우
                if (CatalogType.is_ipp(_left.catalog_id) && !_left.is_left_front & !CatalogType.is_ica(node.catalog_id))
                    vm.is_enabled = false;
                if (CatalogType.is_ipp(_right.catalog_id) && _right.is_left_front & !CatalogType.is_ica(node.catalog_id))
                    vm.is_enabled = false;

                // XC용 인텔리전트 패치 전면에 XC용 인텔리전트 케이블을 사용하지 않은 경우
                if (CatalogType.is_ipp_xc(_left.catalog_id) && !_left.is_left_front & !CatalogType.is_ica_xc(node.catalog_id))
                    vm.is_enabled = false;
                if (CatalogType.is_ipp_xc(_right.catalog_id) && _right.is_left_front & !CatalogType.is_ica_xc(node.catalog_id))
                    vm.is_enabled = false;

                // IC용 인텔리전트 패치 전면에 IC용 인텔리전트 케이블을 사용하지 않은 경우
                if (CatalogType.is_ipp_ic(_left.catalog_id) && !_left.is_left_front & !CatalogType.is_ica_ic(node.catalog_id))
                    vm.is_enabled = false;
                if (CatalogType.is_ipp_ic(_right.catalog_id) && _right.is_left_front & !CatalogType.is_ica_ic(node.catalog_id))
                    vm.is_enabled = false;

                // 인텔리전트 패치 후면에 인텔리전트 케이블을 사용한 경우
                if (CatalogType.is_ipp(_left.catalog_id) && _left.is_left_front & CatalogType.is_ica(node.catalog_id))
                    vm.is_enabled = false;
                if (CatalogType.is_ipp(_right.catalog_id) && !_right.is_left_front & CatalogType.is_ica(node.catalog_id))
                    vm.is_enabled = false;

                // 일반 패치 전면, 후면 무조건 일반 케이블 사용 
                //if (CatalogType.is_pp(_left.catalog_id) && !_left.is_left_front & !CatalogType.is_ca_patch(node.catalog_id))
                //    vm.is_enabled = false;
                //if (CatalogType.is_pp(_right.catalog_id) && _right.is_left_front & !CatalogType.is_ca_patch(node.catalog_id))
                //    vm.is_enabled = false;
                // 라인 카다로그를 익ㄹ어와서 사용해도 되는 케이블인지 판단 하기 위함 
                // 
                // romee 2015.09.08
                // 일반패널인데 지능형 케이블이면 안됨
                // 인터 컨넷션 문제 없음 
                if (CatalogType.is_pp(_right.catalog_id) && _right.is_left_front && CatalogType.is_ica(node.catalog_id))
                    vm.is_enabled = false;
                // 지능형이 아닌데 지능형 케이블이면 안됨
                if (!CatalogType.is_ipp(_left.catalog_id) && !CatalogType.is_ipp(_right.catalog_id) && CatalogType.is_ica(node.catalog_id))
                    vm.is_enabled = false;

                // 패치 후면에 패치케이블을 사용한 경우
                if (CatalogType.is_pp(_left.catalog_id) && _left.is_left_front & CatalogType.is_ca_patch(node.catalog_id))
                    vm.is_enabled = false;
                if (CatalogType.is_pp(_right.catalog_id) && !_right.is_left_front & CatalogType.is_ca_patch(node.catalog_id))
                    vm.is_enabled = false;

                if (CatalogType.is_ca_fiber(node.catalog_id))    // 2/23 romee 광패치 인 경우 광만 비교 로 수정 
                { 
                    // Fiber케이블을 사용하지 않은 경우
                    if (CatalogType.is_pp_fiber(_left.catalog_id) && !CatalogType.is_ca_fiber(node.catalog_id))
                        vm.is_enabled = false;
                    if (CatalogType.is_pp_fiber(_right.catalog_id) && !CatalogType.is_ca_fiber(node.catalog_id))
                        vm.is_enabled = false;
                    if (CatalogType.is_pp_utp(_left.catalog_id) || CatalogType.is_pp_utp(_right.catalog_id))
                        vm.is_enabled = false;
                }
                else
                {
                    // UTP케이블을 사용하지 않은경우 
                    if (!CatalogType.is_pp_utp(_left.catalog_id) && !CatalogType.is_ca_utp(node.catalog_id))
                        vm.is_enabled = false;
                    if (!CatalogType.is_pp_utp(_right.catalog_id) && !CatalogType.is_ca_utp(node.catalog_id))
                        vm.is_enabled = false;
                    if (CatalogType.is_pp_fiber(_left.catalog_id)  || CatalogType.is_pp_fiber(_right.catalog_id))
                        vm.is_enabled = false;
                    // 좌 우 둘다 지능형이 아닌경우 
                    // 스위치 - 패치 , 패치 - 패치 , 패치 - 아울렛, 패치 - 스위치 , 스위치 - 아울렛 
                    //if (!CatalogType.is_ipp(_left.catalog_id) && !CatalogType.is_ipp(_right.catalog_id) && CatalogType.is_ca_utp(node.catalog_id)) 
                    //    vm.is_enabled = false;
                }
                if (vm.is_enabled && choice == -1)
                    choice = cnt;

                cnt++;
                _cable_list.Add(vm);
            }

            _lvCable.ItemsSource = _cable_list;
            if (choice != -1)
                _lvCable.SelectedIndex = choice;
        }

    }
    public class GetCableTextBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return App.Current.Resources["_brushDarkGray4"] as Brush;
            bool flag = (bool)value;
            if (flag)
                return App.Current.Resources["_brushNormalText"] as Brush;
            else
                return App.Current.Resources["_brushDarkGray4"] as Brush;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
