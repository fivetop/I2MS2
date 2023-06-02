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

namespace I2MS2.Windows
{
    /// <summary>
    /// CatalogGroupManager.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CatalogGroupManager : Window
    {
        public static RoutedCommand NewCommand = new RoutedCommand();
        public static RoutedCommand EditCommand = new RoutedCommand();
        public static RoutedCommand DeleteCommand = new RoutedCommand();
        public static RoutedCommand SaveCommand = new RoutedCommand();

        private bool _new = false;
        private bool _edit = false;
        private bool _delete = false;
        private bool _save = false;

        List<CatalogGroupTree> _catalog_group_tree = new List<CatalogGroupTree>();
        CatalogGroupTree _cgt;

        public CatalogGroupManager()
        {
            InitializeComponent();

            initTree();
        }

        private void _tree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem tvi = (TreeViewItem)_tree.ItemContainerGenerator.ContainerFromIndex(_tree.Items.CurrentPosition);

            _cgt = (CatalogGroupTree)_tree.SelectedItem;

            if (_cgt == null)
                return;

            dispDetail(_cgt);

            _new = true;
            _edit = true;
            _delete = true;
            _save = false;
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

        private void _cmdNew_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_cgt == null)
            {
                e.CanExecute = false;
                return;
            }

            if (_cgt.disp_level != 1)
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = _new;
        }

        private void _cmdNew_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string level1 = txtLevel1.Text;

            CatalogGroupManager_New window = new CatalogGroupManager_New(this, _tree, _catalog_group_tree, _cgt, level1);
            window.Owner = this;
            Nullable<bool> result = window.ShowDialog();
        }

        private void _cmdEdit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_cgt == null)
            {
                e.CanExecute = false;
                txtCatalogGroupName.IsEnabled = false;
                return;
            }

            if (_cgt.disp_level != 2)
            {
                e.CanExecute = false;
                txtCatalogGroupName.IsEnabled = false;
                return;
            }

            txtCatalogGroupName.IsEnabled = !_edit;
            e.CanExecute = _edit;
        }

        private void _cmdEdit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _edit = false;
        }

        private void _cmdDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_cgt == null)
            {
                e.CanExecute = false;
                return;
            }

            if (_cgt.disp_level != 2)
            {
                e.CanExecute = false;
                return;
            }

            // 별표 표시는 삭제 불가능
            if (_cgt.fixed_mark == "*")
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = _delete;
        }

        private async void _cmdDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            await deleteCatalogGroup();

            _new = true;
            _edit = true;
            _delete = true;
            _save = false;
        }

        private void _cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _save;
        }

        private async void _cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            await saveData();

            _save = false;
            _edit = true;
            _delete = true;
        }

        private void txtCatalogGroupName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string name = txtCatalogGroupName.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                _save = false;
                return;
            }

            if (_cgt == null)
                return;

            if (name != _cgt.catalog_group_name)
                _save = true;
        }

        // 좌측 트리 창에 내용을 출력한다.

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

        //private void initTree()
        //{
        //    List<catalog_group> list = g.catalog_group_list.ToList();

        //    int id;
        //    Nullable<int> next_id = null;
        //    catalog_group cg = g.catalog_group_list.Find(e => e.prev_catalog_group_id == null);
        //    if (cg == null)
        //        return;
        //    next_id = cg.catalog_group_id;

        //    while (true)
        //    {
        //        cg = g.catalog_group_list.Find(e => e.catalog_group_id == next_id);
        //        if (cg == null)
        //            break;
        //        id = cg.catalog_group_id;
        //        next_id = cg.next_catalog_group_id ?? 0;

        //        CatalogGroupTree cgt = new CatalogGroupTree();

        //        cgt.catalog_group_id = cg.catalog_group_id;
        //        cgt.catalog_group_name = cg.catalog_group_name;
        //        cgt.disp_level = cg.catalog_level ?? 0;
        //        cgt.level1_catalog_group_id = cg.level1_catalog_group_id ?? 0;
        //        cgt.level2_catalog_group_id = cg.level2_catalog_group_id ?? 0;
        //        cgt.is_expander_visible = Visibility.Visible;
        //        if (cg.deletable == "Y")
        //            cgt.fixed_mark = "";
        //        else
        //            cgt.fixed_mark = "*";
        //        cgt.image_id = ImageIcon.get_icon_id_by_catalog_group_id(cg.catalog_group_id);
        //        sp_list_image_Result ii = g.sp_image_list.Find(e => e.image_id == cgt.image_id);
        //        if (ii != null)
        //            cgt.image_file_path = string.Format("/I2MS2;component/Icons/{0}", ii.file_name);
        //        else
        //            cgt.image_file_path = string.Format("/I2MS2;component/Icons/{0}", "null_16.png");

        //        if (cg.catalog_level == 1)
        //        {
        //            catalog_group_tree.Add(cgt);
        //        }

        //        if (cg.catalog_level == 2)
        //        {
        //            catalog_group_tree.Last().node_list.Add(cgt);
        //        }
        //    }

        //    _tree.ItemsSource = catalog_group_tree;
        //}

        // 우측 편집 창에 내용을 출력한다.
        private void dispDetail(CatalogGroupTree _cgt)
        {
            string level1 = "";
            if (_cgt.disp_level == 1)
                level1 = _cgt.catalog_group_name;
            else
                level1 = g.catalog_group_list.Find(p => p.catalog_group_id == _cgt.level1_catalog_group_id).catalog_group_name;
            txtLevel1.Text = level1;

            string level2 = "";
            if (_cgt.disp_level == 2)
                level2 = _cgt.catalog_group_name;
            txtLevel2.Text = level2;

            txtCatalogGroupName.Text = _cgt.catalog_group_name;
        }

        private async Task saveData()
        {
            string name = txtCatalogGroupName.Text.Trim();

            catalog_group cg = g.catalog_group_list.Find(p => p.catalog_group_name == name);
            if (cg != null)
            {
                MessageBox.Show(g.tr_get("C_Duplicated_Catalog_Group"));
                return;
            }

            cg = g.catalog_group_list.Find(p => p.catalog_group_id == _cgt.catalog_group_id);
            if (cg == null)
                return;
            cg.catalog_group_name = name;
            cg.catalog = null; 
            int r = await g.webapi.put("catalog_group", cg.catalog_group_id, cg, typeof(catalog_group));
            if (r != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return;
            }

            switch (cg.catalog_level)
            {
                case 1 :
                    txtLevel1.Text = name;
                    break;
                case 2:
                    txtLevel2.Text = name;
                    break;
            }

            _cgt.catalog_group_name = name;
            _cgt.force_changed = true;
        }


        private async Task addCatalogGroup(string name)
        {
            string level1 = txtLevel1.Text;
            string level2 = txtLevel2.Text;
            catalog_group new_cg;
            //int prev_id,;
            int add_id = 0;
            //Nullable<int> next_id = null;
            int level, level1_id, level2_id = 0;

            int seq = g.catalog_group_list.Max(p => p.disp_order);

            try
            {
                catalog_group r1 = g.catalog_group_list.Find(p => p.catalog_group_name == level1);
                level1_id = r1 == null ? 0 : r1.catalog_group_id;
                catalog_group r2 = g.catalog_group_list.Find(p => p.catalog_group_name == level2);
                level2_id = r2 == null ? 0 : r2.catalog_group_id;

                int cnt1 = g.catalog_group_list.FindAll(p => p.level1_catalog_group_id == level1_id).Count;
                int cnt2 = level2_id == 0 ? 0 : g.catalog_group_list.FindAll(p => p.level2_catalog_group_id == level2_id).Count;

                level = level2_id == 0 ? 2 : 3;

                //if (level2_id == 0)
                //    prev_id = cnt1 == 0 ? level1_id : g.catalog_group_list.FindLast(p => p.level1_catalog_group_id == level1_id).catalog_group_id;
                //else
                //    prev_id = cnt2 == 0 ? level2_id : g.catalog_group_list.FindLast(p => p.level2_catalog_group_id == level2_id).catalog_group_id;

                //next_id = g.catalog_group_list.Find(p => p.catalog_group_id == prev_id).next_catalog_group_id;

                new_cg = new catalog_group()
                {
                    catalog_group_name = name,
                    catalog_level = level,
                    enable = "Y",
                    deletable = "Y",
                    level1_catalog_group_id = level1_id,
                    level2_catalog_group_id = level2_id,
                    disp_order = seq+1
                    //prev_catalog_group_id = prev_id,
                    //next_catalog_group_id = next_id
                };
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("Exception error: code={0}, message={1}", e.HResult, e.Message));
                return;
            }

            catalog_group add_cg = (catalog_group)await g.webapi.post("catalog_group", new_cg, typeof(catalog_group));
            if (add_cg == null)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return;
            }

            g.catalog_group_list.Add(add_cg);
            add_id = add_cg.catalog_group_id;

            CatalogGroupTree new_cgt = new CatalogGroupTree()
            {
                catalog_group_id = add_id,
                catalog_group_name = name,
                disp_level = level,
                image_id = 0,
                image_file_path = "",
                is_expander_visible = Visibility.Visible,
                level1_catalog_group_id = level1_id,
                level2_catalog_group_id = level2_id,
                fixed_mark = "",
                node_list = new List<CatalogGroupTree>()
            };

            new_cgt.image_id = ImageIcon.get_etc_icon_id();
            sp_list_image_Result ii = g.sp_image_list.Find(e => e.image_id == new_cgt.image_id);
            if (ii != null)
                new_cgt.image_file_path = string.Format("/I2MS2;component/Icons/{0}", ii.file_name);

            _catalog_group_tree[0].force_changed = true;
            CatalogGroupTree node = _catalog_group_tree.Find(p => p.catalog_group_id == level1_id);
            node.node_list.Add(new_cgt);
            node.force_changed = true;
            CatalogGroupTree node2 = _catalog_group_tree.Find(p => p.catalog_group_id == level1_id).node_list.FindLast(p => true);
            node2.force_changed = true;

            _cgt.force_changed = true;
        }

        private async Task deleteCatalogGroup()
        {
            int delete_id = _cgt.catalog_group_id;
            //Nullable<int> prev_id, next_id = null;

            if (MessageBox.Show(g.tr_get("C_Delete_Item"), g.tr_get("C_Confirm"), MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            
            catalog_group delete_cg = g.catalog_group_list.Find(p => p.catalog_group_id == delete_id);
            if (delete_cg == null)
                return;

            int child_cnt = _cgt.node_list.Count;
            if (child_cnt > 0)
            {
                MessageBox.Show(g.tr_get("C_Error_Catalog_Group_1"));
                return;
            }

            //prev_id = delete_cg.prev_catalog_group_id;
            //next_id = delete_cg.next_catalog_group_id;

            if (!string.IsNullOrEmpty(_cgt.fixed_mark))
            {
                MessageBox.Show(g.tr_get("C_Error_Cant_Delete"));
                return;
            }


            int rr3 = await g.webapi.delete("catalog_group", delete_id);
            if (rr3 != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return;
            }

            g.catalog_group_list.Remove(delete_cg);

            int pos1 = getIndex1(_catalog_group_tree, _cgt.level1_catalog_group_id);
            if (pos1 < 0)
                return;

            int pos2 = 0;
            TreeViewItem tvi11 = (TreeViewItem)_tree.ItemContainerGenerator.ContainerFromIndex(pos1);
            TreeViewItem tvi22 = null;

            int level = _cgt.disp_level;

            switch (level)
            {
                case 1:
                    _catalog_group_tree.Remove(_cgt);
                    _tree.ItemsSource = null;
                    _tree.ItemsSource = _catalog_group_tree;
                    break;
                case 2:
                    _catalog_group_tree[pos1].node_list.Remove(_cgt);
                    tvi11.ItemsSource = null;
                    tvi11.ItemsSource = _catalog_group_tree[pos1].node_list;
                    break;
                case 3:
                    pos2 = getIndex2(_catalog_group_tree, _cgt.level2_catalog_group_id, pos1);
                    if (pos2 < 0)
                        return;
                    _catalog_group_tree[pos1].node_list[pos2].node_list.Remove(_cgt);
                    tvi22 = (TreeViewItem)tvi11.ItemContainerGenerator.ContainerFromIndex(pos2);
                    tvi22.ItemsSource = null;
                    tvi22.ItemsSource = _catalog_group_tree[pos1].node_list[pos2].node_list;
                    break;
            }
        }

        private void backupExpanded()
        {
            int idx1 = 0;
            int idx2 = 0;
            int idx3 = 0;
            foreach (CatalogGroupTree aa in _catalog_group_tree)
            {
                TreeViewItem tvi1 = (TreeViewItem)_tree.ItemContainerGenerator.ContainerFromIndex(idx1++);
                aa.is_expanded = tvi1 == null ? false : tvi1.IsExpanded;
                idx2 = 0;
                foreach (CatalogGroupTree bb in aa.node_list)
                {
                    TreeViewItem tvi2 = (TreeViewItem) tvi1.ItemContainerGenerator.ContainerFromIndex(idx2++);
                    bb.is_expanded = tvi2 == null ? false : tvi2.IsExpanded;
                    idx3 = 0;
                    foreach (CatalogGroupTree cc in bb.node_list)
                    {
                        TreeViewItem tvi3 = (TreeViewItem)tvi2.ItemContainerGenerator.ContainerFromIndex(idx3++);
                        cc.is_expanded = tvi3 == null ? false : tvi3.IsExpanded;
                    }
                }
            }
        }

        private void restoreExpanded()
        {
            int idx1 = 0;
            int idx2 = 0;
            //int idx3 = 0;
            foreach (CatalogGroupTree aa in _catalog_group_tree)
            {
                TreeViewItem tvi1 = (TreeViewItem)_tree.ItemContainerGenerator.ContainerFromIndex(idx1++);
                if (tvi1 != null)
                    tvi1.IsExpanded = aa.is_expanded;
                idx2 = 0;
                foreach (CatalogGroupTree bb in aa.node_list)
                {
                    TreeViewItem tvi2 = (TreeViewItem)tvi1.ItemContainerGenerator.ContainerFromIndex(idx2++);
                    if (tvi2 != null)
                        tvi2.IsExpanded = bb.is_expanded;
                }
            }
        }

        // 선택한 첫 번째 트리 인덱스를 알아온다. -1=not found
        public int getIndex1(List<CatalogGroupTree> catalog_group_tree, int id)
        {
            int cnt = 0;
            foreach (CatalogGroupTree cgt in catalog_group_tree)
            {
                if (id == cgt.catalog_group_id)
                    return cnt;
                cnt++;
            }
            return -1;
        }

        // 선택한 두 번째 트리 인덱스를 알아온다. -1=not found
        public int getIndex2(List<CatalogGroupTree> catalog_group_tree, int id, int pos1)
        {
            int cnt = 0;
            try
            {
                List<CatalogGroupTree> list = catalog_group_tree[pos1].node_list;
                foreach (CatalogGroupTree cgt in list)
                {
                    if (id == cgt.catalog_group_id)
                        return cnt;
                    cnt++;
                }
            }
            catch (Exception e)
            { Console.WriteLine("Exception error: error={0}, message={1}", e.HResult, e.Message); }

            return -1;
        }
    }
}
