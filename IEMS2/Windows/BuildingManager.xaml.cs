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
using System.ComponentModel;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using I2MS2.Library;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using MahApps.Metro.Controls;

namespace I2MS2.Windows
{
    // 빌딩 이미지 등록 처리 
    /// <summary>
    /// DrawingsManager.xaml에 대한 상호 작용 논리
    /// </summary>
    ///
    public partial class BuildingManager : MetroWindow
    {
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand SelectFileCommand = new RoutedCommand();

        private int _site_id = 0;
        private int _building_id = 0;
        private building _b = null;
        public int _image_id = 0;

        public BuildingManager(int site_id, int building_id)
        {
            _site_id = site_id;
            _building_id = building_id;
            InitializeComponent();

            if (building_id > 0)
            {
                initData();
                dispData();
            }
        }

        #region Command & Event

        private void _cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_txtName.Text))
            {
                e.CanExecute = false;
                return;
            }
 /*
            if (_imgDrawings.Source == null)
            {
                e.CanExecute = false;
                return;
            }
*/
            e.CanExecute = true;
        }

        private async void _cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!await saveData())
                return;

            try
            {
                DialogResult = true;
            }
            catch { }
            Close();
        }

        private void _cmdSelectFile_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void _cmdSelectFile_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            int image_id = _b == null ? 0 : (_b.building_image_id ?? 0);

            selectImageFile(image_id);
        }

        private void _btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        #endregion

        #region 그 외 메소드

        private void initData()
        {
            _b = g.building_list.Find(p => p.building_id == _building_id);
            if (_b == null)
                return;

            _image_id = _b.building_image_id ?? 0;
        }

        private void dispData()
        {
            if (_b == null)
                return;

            _txtName.Text = _b.building_name;
            _txtRemarks.Text = _b.remarks;
            dispDrawing(_b.building_image_id ?? 0);
        }

        private async Task<bool> saveData()
        {
            string name = _txtName.Text;

            // 신규이면?
            if (_building_id == 0)
            {
                var at = g.location_list.Find(p => p.location_name == name);
                if (at != null)
                {
                    MessageBox.Show(g.tr_get("C_Error_2"));
                    return false;
                }     

                _b = new building();
                _b.building_image_id = _image_id;
                _b.building_name = _txtName.Text;
                _b.remarks = _txtRemarks.Text;
                _b.site_id = _site_id;
                _b.user_id = g.login_user_id;

                var bb = (building) await g.webapi.post("building", _b, typeof(building));
                if (bb == null)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }

                g.building_list.Add(bb);
                
                _building_id = bb.building_id;

                bool b = await g.left_tree_handler.addBuilding(_building_id);
                if (!b)
                    return false;
                int location_id = Etc.get_location_id_by_building_id(_building_id);
                g.signalr.send_location_to_signalr(location_id, eAction.eAdd);
            }
            else
            {
                var at = g.location_list.Find(p => (p.location_name == name) && (p.building_id != _building_id));
                if (at != null)
                {
                    MessageBox.Show(g.tr_get("C_Error_2"));
                    return false;
                }

                _b.building_image_id = _image_id;
                _b.building_name = _txtName.Text;
                _b.remarks = _txtRemarks.Text;
                _b.site_id = _site_id;
                _b.user_id = g.login_user_id;
                var rr = await g.webapi.put("building", _building_id, _b, typeof(building));
                if (rr != 0)
                    return false;

                bool b = await g.left_tree_handler.editBuilding(_building_id, _b.building_name);
                if (!b)
                    return false;
                int location_id = Etc.get_location_id_by_building_id(_building_id);
                g.signalr.send_location_to_signalr(location_id, eAction.eModify);
            }

            return true;
        }

        private void selectImageFile(int image_id)
        {
            int image_type = 1160003;
            SelectImage window = new SelectImage(image_type, image_id);
            window.Owner = this;
            bool b = window.ShowDialog() ?? false;
            if (b == true)
                _image_id = g.result_image_id;

            dispDrawing(_image_id);
        }

        private void dispDrawing(int image_id)
        {
            _imgDrawings.Source = null;
            var im = g.sp_image_list.Find(p => p.image_id == image_id);
            if (im == null)
                return;

            string image_file = string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH, im.folder_name, im.file_name);
            ImageSource source = null;
            try
            {
                source = new BitmapImage(new Uri(image_file));
            }
            catch (Exception) { }
        
            _imgDrawings.Source = source;
        }

        #endregion

    }

   
}
