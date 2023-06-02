using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using WebApi.Models;
using I2MS2.Models;
using WebApiClient;
using System;
using System.Threading.Tasks;
using I2MS2.Library;
using System.Windows.Controls;
using System.Linq;

namespace I2MS2.Windows
{
    /// <summary>
    /// CatalogGroupManager_New.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public partial class CatalogGroupManager_New : Window
    {
        public static RoutedCommand SaveCommand = new RoutedCommand();

        private bool _save = false;

        private CatalogGroupManager _parent;
        private List<CatalogGroupTree> catalog_group_tree;
        private CatalogGroupTree _cgt;
        private TreeView _tree;

        public CatalogGroupManager_New(CatalogGroupManager parent, TreeView treeview, List<CatalogGroupTree> cgt_list, CatalogGroupTree cgt, string level1)
        {
            InitializeComponent();

            _parent = parent;
            catalog_group_tree = cgt_list;
            _cgt = cgt;
            _tree = treeview;

            txtLevel1.Text = level1;

            txtCatalogGroupName.Focus();
        }

        private void _cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _save;
        }

        private async void _cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string name = txtCatalogGroupName.Text.Trim();

            catalog_group cg = g.catalog_group_list.Find(p => p.catalog_group_name == name);
            if (cg != null)
            {
                MessageBox.Show(g.tr_get("C_Duplicated_Catalog_Group"));
                return;
            }

            bool result = await addCatalogGroup(name);

            this.DialogResult = result;

            Close();
        }

        private void txtCatalogGroupName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string name = txtCatalogGroupName.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                _save = false;
                return;
            }

            if (_cgt.catalog_group_name == name)
            {
                _save = false;
                return;
            }

            _save = true;
        }

        private void _btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DialogResult = false;

            Close();
        }

        // 레벨1은 추가 불가능(시스템에서 이미 5개로 고정), 레벨2만 자유롭게 등록할 수 있다.
        private async Task<bool> addCatalogGroup(string name)
        {
            string level1 = txtLevel1.Text;
            catalog_group new_cg;
            //int prev_id = 0;
            //Nullable<int> next_id = null;
            int add_id = 0;
            int level1_id = 0;
            int cnt1 = 0;

            int seq = g.catalog_group_list.Max(p => p.disp_order);

            try
            {
                catalog_group r1 = g.catalog_group_list.Find(p => p.catalog_group_name == level1);
                level1_id = r1 == null ? 0 : r1.catalog_group_id;

                cnt1 = g.catalog_group_list.FindAll(p => (p.catalog_level == 2) && (p.level1_catalog_group_id == level1_id)).Count;

                //prev_id = cnt1 == 0 ? level1_id : g.catalog_group_list.FindLast(p => (p.catalog_level == 2) &&( p.level1_catalog_group_id == level1_id)).catalog_group_id;
                //next_id = g.catalog_group_list.Find(p => p.catalog_group_id == prev_id).next_catalog_group_id;

                new_cg = new catalog_group()
                {
                    catalog_group_name = name,
                    catalog_level = 2,
                    enable = "Y",
                    deletable = "Y",
                    level1_catalog_group_id = level1_id,
                    level2_catalog_group_id = 0,
                    disp_order = seq
                    //prev_catalog_group_id = prev_id,
                    //next_catalog_group_id = next_id
                };
            }
            catch(Exception e)
            {
                MessageBox.Show(string.Format("Exception error: code={0}, message={1}", e.HResult, e.Message));
                return false;
            }

            catalog_group add_cg = (catalog_group) await g.webapi.post("catalog_group", new_cg, typeof(catalog_group));
            if (add_cg == null)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }

            g.catalog_group_list.Add(add_cg);
            add_id = add_cg.catalog_group_id;

            CatalogGroupTree new_cgt = new CatalogGroupTree()
            {
                catalog_group_id = add_id,
                catalog_group_name = name,
                disp_level = 2,
                image_id = 0,
                image_file_path = "",
                is_expander_visible = Visibility.Visible,
                level1_catalog_group_id = level1_id,
                level2_catalog_group_id = 0,
                fixed_mark = "",
                node_list = new List<CatalogGroupTree>()
            };

            new_cgt.image_id = ImageIcon.get_icon_id_by_catalog_group_id(level1_id);
            sp_list_image_Result ii = g.sp_image_list.Find(e => e.image_id == new_cgt.image_id);
            if (ii != null)
                new_cgt.image_file_path = string.Format("/I2MS2;component/Icons/{0}", ii.file_name);
            else
                new_cgt.image_file_path = string.Format("/I2MS2;component/Icons/{0}", "null_16.png");

            CatalogGroupTree node;
            node = catalog_group_tree.Find(p => p.catalog_group_id == level1_id);
            if (node == null)
                return false;

            node.node_list.Add(new_cgt);

            int pos1 = _parent.getIndex1(catalog_group_tree, level1_id);
            if (pos1 < 0)
                return false;

            TreeViewItem tvi1 = (TreeViewItem)_tree.ItemContainerGenerator.ContainerFromIndex(pos1);
            tvi1.ItemsSource = null;
            tvi1.ItemsSource = catalog_group_tree[pos1].node_list;

            return true;
        }

    }
}
