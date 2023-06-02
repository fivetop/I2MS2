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
    public partial class ManufactureManager_New : Window
    {
        public static RoutedCommand SaveCommand = new RoutedCommand();
        private bool _save = false;

        List<manufacture> _manufacture_list;

        public ManufactureManager_New(List<manufacture> manufacture_list)
        {
            InitializeComponent();
            _manufacture_list = manufacture_list;

            txtManufactureName.Focus();
        }


        private void _cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            _save = !string.IsNullOrEmpty(txtManufactureName.Text);
            e.CanExecute = _save;
        }

        private async void _cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            bool b = await saveData();

            if (b)
            {
                try
                {
                    this.DialogResult = true;
                }
                catch { }
                Close();
            }
        }

        private void _gridTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private async Task<bool> saveData()
        {
            string tph = txtPhone.Text.Trim();

            if (!g.IsValidvalue(tph, "TEL"))
            {
                MessageBox.Show(g.tr_get("C_Info37"));
                return false;
            }

            string name = txtManufactureName.Text.Trim();

            manufacture m = _manufacture_list.Find(p => p.manufacture_name == name);
            if (m != null)
            {
                MessageBox.Show(g.tr_get("C_Error_Duplicated_Company"));
                return false;
            }

            m = new manufacture()
            {
                manufacture_name = name,
                address = txtAddress.Text.TrimStart().TrimEnd(),
                phone = txtPhone.Text.TrimStart().TrimEnd(),
                post = txtPost.Text.TrimStart().TrimEnd(),
                homepage_url = txtHomepageUrl.Text.TrimStart().TrimEnd(),
                ceo_name = txtCeoName.Text.TrimStart().TrimEnd(),
                remarks = txtRemarks.Text.TrimStart().TrimEnd(),
            };

            m = (manufacture) await g.webapi.post("manufacture", m, typeof(manufacture));
            if (m == null)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }

            _manufacture_list.Add(m);
            g.manufacture_list.Add(m);

            return true;
        }
        
        private void _btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }
    }
}
