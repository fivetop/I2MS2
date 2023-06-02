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

    public partial class CatalogSelector : Window
    {
        public static RoutedCommand OkCommand = new RoutedCommand();

        private bool _ok = true;

        private List<CatalogGroupTree> _catalog_group_tree = new List<CatalogGroupTree>();
        private CatalogGroupTree _cgt;

        private List<catalog> _catalog_list = new List<catalog>();
        private List<CatalogExtVM> _catalog_ext_vm_list = new List<CatalogExtVM>();
        private AssetManager _am = null;

        public CatalogSelector(AssetManager am)
        {
            _am = am;
            InitializeComponent();
            initTree();
        }

        private void _tree_Loaded(object sender, RoutedEventArgs e)
        {
            int n = 0;
            foreach (CatalogGroupTree cgt in _tree.Items)
            {
                TreeViewItem item = _tree.ItemContainerGenerator.ContainerFromIndex(n++) as TreeViewItem;
                item.IsExpanded = true;
            }
        }


        private void _cmdOk_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _ok;
        }

        private void _cmdOk_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            catalog c = (catalog)_lvCatalog.SelectedItem;
            if (c == null)
                return;                   

            _am.selected_catalog = g.catalog_list.Find(p => p.catalog_id == c.catalog_id);
            DialogResult = true;
            Close();
        }

        private void _tree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem tvi = (TreeViewItem)_tree.ItemContainerGenerator.ContainerFromIndex(_tree.Items.CurrentPosition);

            _cgt = (CatalogGroupTree)_tree.SelectedItem;

            _ok = false;

            if (_cgt == null)
                return;

            dispDetail(_cgt);
        }

        private void _lvCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            catalog c = (catalog)_lvCatalog.SelectedItem;

            _ok = false;

            if (c == null)
                return;

            _ok = true;
        }

        private void _btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }


        private void initTree()
        {
            int id;
            List<catalog_group> list = g.catalog_group_list.OrderBy(p => p.disp_order).ToList();

            foreach (var cg in list)
            {
                id = cg.catalog_group_id;

                CatalogGroupTree cgt = new CatalogGroupTree();

                cgt.catalog_group_id = cg.catalog_group_id;
                cgt.catalog_group_name = cg.catalog_group_name;
                cgt.disp_level = cg.catalog_level ?? 0;
                cgt.level1_catalog_group_id = cg.level1_catalog_group_id ?? 0;
                cgt.level2_catalog_group_id = cg.level2_catalog_group_id ?? 0;
                cgt.is_expander_visible = Visibility.Visible;
                if (cg.deletable == "Y")
                    cgt.fixed_mark = "";
                else
                    cgt.fixed_mark = "*";
                cgt.image_id = ImageIcon.get_icon_id_by_catalog_group_id(cg.catalog_group_id);
                sp_list_image_Result ii = g.sp_image_list.Find(e => e.image_id == cgt.image_id);
                if (ii != null)
                    cgt.image_file_path = string.Format("/I2MS2;component/Icons/{0}", ii.file_name);
                else
                    cgt.image_file_path = string.Format("/I2MS2;component/Icons/{0}", "null_16.png");

                if (cg.catalog_level == 1)
                {
                    _catalog_group_tree.Add(cgt);
                }

                if (cg.catalog_level == 2)
                {
                    _catalog_group_tree.Last().node_list.Add(cgt);
                }
            }
            _tree.ItemsSource = _catalog_group_tree;
        }

        // 우측 편집 창에 내용을 출력한다.
        private void dispDetail(CatalogGroupTree _cgt)
        {
            if (_cgt == null)
                return;

            if (_cgt.catalog_group_id == 0)
                return;

            int catalog_group_id = _cgt.catalog_group_id;
            _catalog_list = g.catalog_list.Where(p => (p.catalog_group_id == catalog_group_id) && !CatalogType.is_dont_add_asset(p.catalog_id)).ToList();

            _lvCatalog.ItemsSource = _catalog_list;
        }


    }
}
