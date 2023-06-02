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
namespace I2MS2.Windows
{

    /// <summary>
    /// DrawingsManager.xaml에 대한 상호 작용 논리
    /// </summary>
    ///
    // 2D 벽설계 파일 , 3D 도면 선택 , 층과 도면 편집기 에서 사용 
    public partial class SelectDrawing3d : Window
    {
        public static RoutedCommand OkCommand = new RoutedCommand();

        List<sp_img_vm> _image_vm_list = new List<sp_img_vm>();
        int _image_type = 1160004;

        public SelectDrawing3d(int image_id)
        {
            InitializeComponent();
            g.result_image_id = 0;

            initData();
            initUI(image_id);
        }


        private void initData()
        {
            var list = g.sp_image_list.Where(p => p.image_type_id == _image_type);

            foreach (var at in list)
            {
                sp_img_vm img_vm = makeSpListImageResultVm(at);
                _image_vm_list.Add(img_vm);
            }
        }

        private void initUI(int image_id)
        {
            this.DataContext = _image_vm_list;
            _lvLeft.ItemsSource = _image_vm_list;

            var vm = _image_vm_list.Find(p => p.image_id == image_id);
            if (vm != null)
                _lvLeft.SelectedItem = vm;
        } 

        private void _cmdOk_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var item = (sp_img_vm)_lvLeft.SelectedItem;

            e.CanExecute = item != null;
        }

        private void _cmdOk_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var item = (sp_img_vm)_lvLeft.SelectedItem;
                              
            if (item == null)
                g.result_drawing_3d_id = 0;

            g.result_drawing_3d_id = item.drawing_3d_id ?? 0;
            DialogResult = true;
            Close();
        }

        private void _btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private sp_img_vm makeSpListImageResultVm(sp_list_image_Result img)
        {
            sp_img_vm img_vm = new sp_img_vm()
            {
                image_id = img.image_id,
                image_name = img.image_name,
                image_type_id = img.image_type_id,
                image_type_name = img.image_type_name,

                file_name = img.file_name,
                folder_name = img.folder_name,
                client_file_path = string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH, img.folder_name, img.file_name),
                deletable = img.deletable,
                remarks = img.remarks,
                image_source = null,
                size_x = img.size_x,
                size_y = img.size_y,
                size_text = string.Format("{0} X {1}", img.size_x, img.size_y),
                drawing_3d_id = img.drawing_3d_id,
                drawing_3d_file_name = img.drawing_3d_file_name,

                recomend_size_x = img.recomend_size_x,
                recomend_size_y = img.recomend_size_y
            };
            try
            {
                img_vm.image_source = new BitmapImage(new Uri(img_vm.client_file_path));
            }
            catch (Exception) { }

            return img_vm;
        }
    }   
}
