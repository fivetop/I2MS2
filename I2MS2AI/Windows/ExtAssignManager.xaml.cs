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
using I2MS2.Library.ColorPicker;

namespace I2MS2.Windows
{
    /// <summary>
    /// CatalogGroupManager.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public class CatalogListVM : INotifyPropertyChanged
    {
        public int catalog_group1_id { get; set; }
        public string catalog_group1_name { get; set; }
        public int catalog_group2_id { get; set; }
        public string catalog_group2_name { get; set; }
        public int image_id { get; set; }
        public string image_file_path { get; set; }
        public int catalog_id { get; set; }
        public string catalog_name { get; set; }

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

    public class ExtAssignVM : INotifyPropertyChanged
    {
        public int catalog_ext_id { get; set; }
        public int ext_id { get; set; }
        public int catalog_id { get; set; }
        public string ext_name { get; set; }
        public int ext_length { get; set; }
        public string ext_type { get; set; }
        public string remarks { get; set; }
        public bool is_registered { get; set; }
        public bool? is_checked { get; set; }
        public int total_count { get; set; }
        public int select_count { get; set; }
                                        
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

    public partial class ExtAssignManager : Window
    {
        public static RoutedCommand SaveCommand = new RoutedCommand();

        private List<CatalogListVM> _catalog_vm_list = new List<CatalogListVM>();
        private List<ExtAssignVM> _ext_vm_list = new List<ExtAssignVM>();

        public ExtAssignManager()
        {
            InitializeComponent();
            initLeftData();
            initRightData();

            _lvCatalog.ItemsSource = _catalog_vm_list;
            _lvExt.ItemsSource = _ext_vm_list;
            _lvCatalog.SelectedIndex = 0;
        }


        private void _cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private async void _cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!await saveData())
                return;
        }

        private void _lvCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach(ExtAssignVM vm in _ext_vm_list)
            {
                vm.total_count = 0;
                vm.select_count = 0;
                var list = _lvCatalog.SelectedItems;

                foreach(CatalogListVM node in list)
                {
                    var ce = g.catalog_ext_list.Find(p => (p.catalog_id == node.catalog_id) && (p.ext_id == vm.ext_id));
                    vm.total_count++;
                    if (ce != null)
                        vm.select_count++;
                }

                if (vm.select_count == 0)
                    vm.is_checked = false;
                else if (vm.total_count == vm.select_count)
                    vm.is_checked = true;
                else
                    vm.is_checked = null;

                vm.force_changed = true;
            }
        }

        // 좌측에 내용을 출력한다.
        private void initLeftData()
        {
            var list = from node in g.catalog_list
                       select new CatalogListVM()
                       {
                           catalog_id = node.catalog_id,
                           catalog_name = node.catalog_name,
                           catalog_group2_id = node.catalog_group_id,
                           image_id = ImageIcon.get_icon_id_by_catalog_group_id(node.catalog_group_id)
                       };

            _catalog_vm_list = list.ToList();

            foreach (var vm in _catalog_vm_list)
            {
                vm.image_file_path = ImageIcon.get_icon_file_path(vm.image_id);
                var cg2 = g.catalog_group_list.Find(p => p.catalog_group_id == vm.catalog_group2_id);
                vm.catalog_group2_name = cg2 != null ? cg2.catalog_group_name : "";
                vm.catalog_group1_id = cg2 != null ? (cg2.level1_catalog_group_id ?? 0) : 0;
                var cg1 = g.catalog_group_list.Find(p => p.catalog_group_id == vm.catalog_group1_id);
                vm.catalog_group1_name = cg1 != null ? cg1.catalog_group_name : "";
            }
        }

        // 좌측에 내용을 출력한다.
        private void initRightData()
        {
            var list = from ce in g.ext_property_list
                       select new ExtAssignVM()
                       {
                           ext_id = ce.ext_id,
                           ext_length = ce.ext_length,
                           ext_name = ce.ext_name,
                           ext_type = ce.ext_type,
                           remarks = ce.remarks
                       };

            if (list.Count() > 0)
               _ext_vm_list = list.ToList();
        }




        private async Task<bool> saveData()
        {

            var list = _lvCatalog.SelectedItems;

            foreach (ExtAssignVM vm in _ext_vm_list)
            {
                foreach (CatalogListVM node in list)
                {
                    var ce = g.catalog_ext_list.Find(p => (p.catalog_id == node.catalog_id) && (p.ext_id == vm.ext_id));
                    if ((vm.is_checked == true) && (ce == null))
                    {
                        // 추가
                        catalog_ext ce2 = new catalog_ext() {  
                                catalog_id = node.catalog_id,
                                ext_id = vm.ext_id,
                                user_id = g.login_user_id                        
                        };
                        catalog_ext ce3 = (catalog_ext) await g.webapi.post("catalog_ext", ce2, typeof(catalog_ext));
                        if (ce3 == null)
                        {
                            MessageBox.Show(g.tr_get("C_Error_Server"));
                            return false;
                        }
                        g.catalog_ext_list.Add(ce3);
                    }
                    else if ((vm.is_checked == false) && (ce != null))
                    {
                        // 삭제
                        var r1 = await g.webapi.delete("catalog_ext", ce.catalog_ext_id);
                        if (r1 != 0)
                        {
                            //MessageBox.Show(g.tr_get("C_Error_Server"));
                            return false;
                        }
                        g.catalog_ext_list.Remove(ce);
                    }
                }
            }
            return true;
        }
    }
}
