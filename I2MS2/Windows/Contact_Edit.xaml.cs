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

    public partial class Contact_Edit : Window
    {
        private List<contact> _contact_list;
        private contact _contact;
        private manufacture _manufacture;

        public Contact_Edit(List<contact> contact_list, contact contact, manufacture manufacture)
        {
            InitializeComponent();

            _contact_list = contact_list;
            _contact = contact;
            _manufacture = manufacture;

            initData();

            txtContactName.Focus();
        }

        private async void _btnSave_Click(object sender, RoutedEventArgs e)
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

        private void initData()
        {
            txtContactId.Text = _contact.contact_id.ToString();
            txtContactName.Text = _contact.contact_name;
            txtDuty.Text = _contact.duty;
            txtPosition.Text = _contact.position;
            txtPhone.Text = _contact.phone;
//            txtMobile.Text = _contact.mobile;
//            txtEmail.Text = _contact.email;
            txtRemarks.Text = _contact.remarks;
        }

        private async Task<bool> saveData(string name)
        {
            int id = _contact.contact_id;


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


            contact cc = _contact_list.Find(p => (p.contact_name == name) && (p.contact_id != id));
            if (cc != null)
            {
                MessageBox.Show(g.tr_get("C_Error_Duplicated_Contacts"));
                return false;
            }

            cc = _contact_list.Find(p => p.contact_id == id);
            if (cc == null)
                return false;

            cc.contact_name = txtContactName.Text.Trim();
            cc.duty = txtDuty.Text.Trim();
            cc.position = txtPosition.Text.Trim();
            cc.phone = txtPhone.Text.Trim();
//            cc.mobile = txtMobile.Text.Trim();
//            cc.email = txtEmail.Text.Trim();
            cc.remarks = txtRemarks.Text.Trim();

            contact c1 = new contact();
            c1.contact_id = id;
            c1.manufacture_id = cc.manufacture_id;
            c1.contact_name = txtContactName.Text.Trim();
            c1.duty = txtDuty.Text.Trim();
            c1.position = txtPosition.Text.Trim();
            c1.phone = txtPhone.Text.Trim();
//            c1.mobile = txtMobile.Text.Trim();
//            c1.email = txtEmail.Text.Trim();
            c1.remarks = txtRemarks.Text.Trim();

            int r = await g.webapi.put("contact", cc.contact_id, c1, typeof(contact));
            if (r != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }

            c1 = g.contact_list.Find(p => p.contact_id == id);
            if (c1 == null)
                return false;

            c1.contact_name = txtContactName.Text.Trim();
            c1.duty = txtDuty.Text.Trim();
            c1.position = txtPosition.Text.Trim();
            c1.phone = txtPhone.Text.Trim();
//            c1.mobile = txtMobile.Text.Trim();
//            c1.email = txtEmail.Text.Trim();
            c1.remarks = txtRemarks.Text.Trim();

            return true;
        }


    }
}
