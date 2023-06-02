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
    /// ManufactureManager.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ManufactureManager : Window
    {
        public static RoutedCommand NewCommand = new RoutedCommand();
        public static RoutedCommand EditCommand = new RoutedCommand();
        public static RoutedCommand DeleteCommand = new RoutedCommand();
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand CancelCommand = new RoutedCommand();

        public static RoutedCommand New2Command = new RoutedCommand();
        public static RoutedCommand Edit2Command = new RoutedCommand();
        public static RoutedCommand Delete2Command = new RoutedCommand();

        private bool _new = true;
        private bool _edit = false;
        private bool _delete = false;
        private bool _save = false;
        private bool _cancel = false;

        private bool _new2 = false;
        private bool _edit2 = false;
        private bool _delete2 = false;

        List<manufacture> _manufacture_list = null;
        manufacture _manufacture = new manufacture();

        List<contact> _contact_list = null;
        contact _contact = new contact();

        public ManufactureManager()
        {
            InitializeComponent();
            initListView();

            _lvManufacture.SelectedItem = 0;
        }


        private void _cmdNew_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _new;
        }

        private void _cmdNew_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ManufactureManager_New window = new ManufactureManager_New(_manufacture_list);
            window.Owner = this;
            bool b = window.ShowDialog() ?? false;

            if (b == true)
                refreshData(-1);

            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }

        private void _cmdEdit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            bool flag = _save && !_edit;
            enableControl(flag);

            try
            {
                if (txtManufactureName.Text == "")
                {
                    e.CanExecute = false;
                    return;
                }
            }
            catch (Exception e1)
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = _edit;
        }

        private void _cmdEdit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_manufacture == null)
                return;

            _new = false;
            _delete = false;
            _edit = false;
            _save = true;
            _cancel = true;

            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }

        private void _cmdDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                if (txtManufactureName.Text == "")
                {
                    e.CanExecute = false;
                    return;
                }
            }
            catch (Exception e1)
            {
                e.CanExecute = false;
                return;
            }
            e.CanExecute = _delete;
        }

        private async void _cmdDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_manufacture == null)
                return;

            if (_lvContact.Items.Count > 0)
            {
                MessageBox.Show(g.tr_get("C_Error_Contacts_1"));
                return;
            }

            if (MessageBox.Show(g.tr_get("C_Delete_Item"), g.tr_get("C_Confirm"), MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            if (!await deleteManufacture())
                return;
            
            _new = true;
            _edit = false;
            _delete = false;
            _save = false;
            _cancel = false;

            refreshData(-1);

            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }

        private void _cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _save;
        }

        private async void _cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!await saveData())
                return;

            _new = true;
            _edit = false;
            _delete = false;
            _save = false;
            _cancel = false;

            refreshData(_lvManufacture.SelectedIndex);

            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }

        private void _cmdNew2_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _new2;
        }

        private void _cmdCancel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _cancel;
        }

        private void _cmdCancel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            clearControl();
            enableControl(false);

            _new = true;
            _delete = false;
            _edit = false;
            _save = false;
            _cancel = false;
        }


        private void _cmdNew2_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Contact_New window = new Contact_New(_contact_list, _manufacture);
            window.Owner = this;
            bool b = window.ShowDialog() ?? false;

            if (b == true)
                refreshData2(-1);
            initContact();

            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }

        private void _cmdEdit2_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //txtCatalogGroupName.IsEnabled = !_edit;
            if (_contact == null)
                _edit2 = false;
            e.CanExecute = _edit2;
        }

        private void _cmdEdit2_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if(_contact == null) return;
            if (_manufacture == null) return;
            Contact_Edit window = new Contact_Edit(_contact_list, _contact, _manufacture);
            window.Owner = this;
            bool b = window.ShowDialog() ?? false;

            if (b == true)
            {
                int idx = _lvContact.SelectedIndex;
                refreshData2(idx);
            }
            _new2 = true;
            _edit2 = false;
            _delete2 = false;
            _lvContact.SelectedIndex = -1;

            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();

        }

        private void _cmdDelete2_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_contact == null)
                _delete2 = false;
            e.CanExecute = _delete2;
        }

        private async void _cmdDelete2_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_manufacture == null)
                return;

            if (MessageBox.Show(g.tr_get("C_Delete_Item"), g.tr_get("C_Confirm"), MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            if (!await deleteContact())
                return;

            _new2 = true;
            _edit2 = false;
            _delete2 = false;

            refreshData2(-1);

            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }

        private void _lvManufacture_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _new = true;
            _edit = true;
            _delete = true;
            _save = false;

            manufacture mm = (manufacture)_lvManufacture.SelectedItem;

            if (mm == null)
                return;

            _manufacture.manufacture_id = mm.manufacture_id;
            _manufacture.manufacture_name = mm.manufacture_name;
            _manufacture.address = mm.address;
            _manufacture.phone = mm.phone;
            _manufacture.homepage_url = mm.homepage_url;
            _manufacture.post = mm.post;
            _manufacture.ceo_name = mm.ceo_name;
            _manufacture.remarks = mm.remarks;

            dispDetail();

            initContact();

            _new2 = true;
            _edit2 = false;
            _delete2 = false;
        }


        private void _lvContact_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _new2 = true;
            _edit2 = true;
            _delete2 = true;

            _contact = (contact)_lvContact.SelectedItem;

            if (_contact == null)
                return;
        }

        private void initContact()
        {
            int id = _manufacture.manufacture_id;

//            contact c1 = g.contact_list.Where(p => p.manufacture_id == id).ToList();
            
            var tdb1 = from a in g.contact_list
            where a.manufacture_id == id
            select new contact()
            {
                manufacture_id = a.manufacture_id,
                contact_id = a.contact_id,
                contact_name = a.contact_name,
                duty = a.duty,
                position = a.position,
                phone = a.phone,
                mobile = a.mobile,
                email = a.email,
                remarks = a.remarks,
            };
            _contact_list = tdb1.ToList();

            _lvContact.ItemsSource = _contact_list;
        }

        // 좌측 창에 내용을 출력한다.
        private void initListView()
        {
            var tdb1 = from a in g.manufacture_list
                       select new manufacture()
                       {
                           manufacture_id = a.manufacture_id,
                           manufacture_name = a.manufacture_name,
                           phone = a.phone,
                           address = a.address,
                           post = a.post,
                           homepage_url = a.homepage_url,
                           ceo_name = a.ceo_name,
                           remarks = a.remarks,
                       };
            _manufacture_list = tdb1.ToList();
            _lvManufacture.ItemsSource = _manufacture_list;
        }

        private async Task<bool> saveData()
        {
            string tph = txtPhone.Text.Trim();

            if (!g.IsValidvalue(tph, "TEL"))
            {
                MessageBox.Show(g.tr_get("C_Info37"));
                return false;
            }

            manufacture m = _manufacture_list.Find(p => p.manufacture_id == _manufacture.manufacture_id);
            if (m == null)
                return false;

            manufacture m1 = new manufacture();
            m1.manufacture_id = _manufacture.manufacture_id;
            m1.address = txtAddress.Text.Trim();
            m1.phone = txtPhone.Text.Trim();
            m1.post = txtPost.Text.Trim();
            m1.homepage_url = txtHomepageUrl.Text.Trim();
            m1.ceo_name = txtCeoName.Text.Trim();
            m1.remarks = txtRemarks.Text.Trim();

            int r = await g.webapi.put("manufacture", m.manufacture_id, m1, typeof(manufacture));
            if (r != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }

            manufacture m2 = g.manufacture_list.Find(p => p.manufacture_id == m.manufacture_id);
            m2.address = txtAddress.Text.Trim();
            m2.phone = txtPhone.Text.Trim();
            m2.post = txtPost.Text.Trim();
            m2.homepage_url = txtHomepageUrl.Text.Trim();
            m2.ceo_name = txtCeoName.Text.Trim();
            m2.remarks = txtRemarks.Text.Trim();

            return true;
        }



        private async Task<bool> deleteManufacture()
        {
            int delete_id = _manufacture.manufacture_id;

            var c = g.catalog_list.Find(p => p.manufacture_id == _manufacture.manufacture_id);
            if (c != null)
            {
                MessageBox.Show(g.tr_get("C_Error_Catalog_Group_1"));
                return false;
            }


            int rr1 = await g.webapi.delete("manufacture", _manufacture.manufacture_id);
            if (rr1 != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }

            manufacture m = g.manufacture_list.Find(p => p.manufacture_id == delete_id);
            if (m == null)
                return false;

            g.manufacture_list.Remove(m);
            _manufacture_list.Remove(m);

            return true;
        }

        // 음수인경우 마지막을 선택
        private void refreshData(int idx)
        {
            _lvManufacture.ItemsSource = null;
            _lvManufacture.ItemsSource = _manufacture_list;
            int idx2 = idx;
            if (idx < 0)
               idx2 = _lvManufacture.Items.Count - 1;

            _lvManufacture.SelectedIndex = idx2;
        }


        // 음수인경우 마지막을 선택
        private void refreshData2(int idx)
        {
            _lvContact.ItemsSource = null;
            _lvContact.ItemsSource = _contact_list;
            int idx2 = idx;
            if (idx < 0)
                idx2 = _lvContact.Items.Count - 1;

            _lvContact.SelectedIndex = idx2;
        }

        private void clearControl()
        {
            txtManufactureId.Text = "";
            txtManufactureName.Text = "";
            txtAddress.Text = "";
            txtPost.Text = "";
            txtPhone.Text = "";
            txtHomepageUrl.Text = "";
            txtCeoName.Text = "";
            txtRemarks.Text = "";
            _lvContact.SelectedIndex = -1;
            _lvManufacture.SelectedIndex = -1;
        }

        private void dispDetail()
        {
            if (_manufacture == null)
                return;

            txtManufactureId.Text = _manufacture.manufacture_id.ToString();
            txtManufactureName.Text = _manufacture.manufacture_name;
            txtAddress.Text = _manufacture.address;
            txtPost.Text = _manufacture.post;
            txtPhone.Text = _manufacture.phone;
            txtHomepageUrl.Text = _manufacture.homepage_url;
            txtCeoName.Text = _manufacture.ceo_name;
            txtRemarks.Text = _manufacture.remarks;
        }

        private void enableControl(bool flag)
        {
            txtAddress.IsEnabled = flag;
            txtPhone.IsEnabled = flag;
            txtPost.IsEnabled = flag;
            txtHomepageUrl.IsEnabled = flag;
            txtCeoName.IsEnabled = flag;
            txtRemarks.IsEnabled = flag;
        }


        private async Task<bool> deleteContact()
        {
            int delete_id = _contact.contact_id;

            int rr1 = await g.webapi.delete("contact", _contact.contact_id);
            if (rr1 != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }

            contact cc = g.contact_list.Find(p => p.contact_id == delete_id);
            if (cc == null)
                return false;

            g.contact_list.Remove(cc);
            //_contact_list.Remove(cc);
            initContact();
            return true;
        }
    }
}
