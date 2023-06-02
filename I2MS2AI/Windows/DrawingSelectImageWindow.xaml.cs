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
using System.IO;

namespace I2MS2.Windows
{
    // 평면도 파일 불러오기 처리 
    /// <summary>
    /// ImageSelectWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DrawingSelectImageWindow : Window
    {
        sp_img_vm _select_img_vm;

        List<sp_img_vm> sp_img_list;
        public sp_img_vm select_img_vm { 
            get{ return _select_img_vm;} 
            set{ _select_img_vm = value;}
        }

        public DrawingSelectImageWindow(List<sp_list_image_Result> _sp_img_list)
        {
            InitializeComponent();

            sp_img_list = new List<sp_img_vm>();
            foreach(var img in _sp_img_list)
            {
                sp_img_vm img_vm = makeSpListImageResultVm(img);
                sp_img_list.Add(img_vm);
            }
            _lvImageList.ItemsSource = sp_img_list;
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
                //client_file_path = string.Format("/I2MS2;component/Icons/{0}", img.file_name),
                client_file_path = string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH, img.folder_name, img.file_name),
                deletable = img.deletable,
                remarks = img.remarks,

                size_x = img.size_x,
                size_y = img.size_y,
                size_text = string.Format("{0} X {1}", img.size_x, img.size_y),
                drawing_3d_id = img.drawing_3d_id,
                drawing_3d_file_name = img.drawing_3d_file_name,

                recomend_size_x = img.recomend_size_x,
                recomend_size_y = img.recomend_size_y
            };
            if(!File.Exists(img_vm.client_file_path))
                img_vm.client_file_path = g.CLIENT_IMAGE_PATH + "No_Image.png";


            return img_vm;
        }

        private void _lvImageList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _select_img_vm = (sp_img_vm)_lvImageList.SelectedItem;
            
            Close();
        }


    }
}
