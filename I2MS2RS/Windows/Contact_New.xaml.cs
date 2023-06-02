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

namespace I2MS2.Windows
{
    /// <summary>
    /// CatalogGroupManager_New.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public partial class Contact_New : Window
    {
        public static RoutedCommand SaveCommand = new RoutedCommand();

        private bool _save = false;

        private List<contact> _contact_list;
        private manufacture _manufacture;

        public Contact_New(List<contact> contact_list, manufacture manufacture)
        {
            InitializeComponent();

            _contact_list = contact_list;
            _manufacture = manufacture;

            txtManufactureName.Text = manufacture.manufacture_name;

            txtContactName.Focus();
        }

        private void _cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _save;
        }

        private async void _cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string name = txtContactName.Text.Trim();

            bool result = await saveData(name);

            if (result == false)
                return;

            try
            {
                this.DialogResult = true;
            }
            catch { }

            Close();
        }

        private void _btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DialogResult = false;

            Close();
        }

        private async Task<bool> saveData(string name)
        {

//            string tem = txtEmail.Text.Trim();
            string tph = txtPhone.Text.Trim();
//            string tmo = txtMobile.Text.Trim();
/*
            if (!g.IsValidvalue(tem, "EMAIL"))
            {
                MessageBox.Show(g.tr_get("C_Info38"));
                return false;
            }
            if (!g.IsValidvalue(tmo, "TEL"))
            {
                MessageBox.Show(g.tr_get("C_Info37"));
                return false;
            }
*/

            if (!g.IsValidvalue(tph, "TEL"))
            {
                MessageBox.Show(g.tr_get("C_Info37"));
                return false;
            }

            contact cc = _contact_list.Find(p => p.contact_name == name);
            if (cc != null)
            {
                MessageBox.Show(g.tr_get("C_Error_Duplicated_Contacts"));
                return false;
            }

            contact c1 = new contact()
            {
                contact_name = name,
                manufacture_id = _manufacture.manufacture_id,
                duty = txtDuty.Text.Trim(),
                position = txtPosition.Text.Trim(),
                phone = txtPhone.Text.Trim(),
//                mobile = txtMobile.Text.Trim(),
//                email = txtEmail.Text.Trim(),
                remarks = txtRemarks.Text.Trim()
            };

            cc = (contact)await g.webapi.post("contact", c1, typeof(contact));
            if (cc == null)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }
            g.contact_list.Add(cc);
            return true;
        }

        private void txtContactName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string name = txtContactName.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                _save = false;
                return;
            }

            _save = true;
        }

    }
}
