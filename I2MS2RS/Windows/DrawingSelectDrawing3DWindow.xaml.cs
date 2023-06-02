using I2MS2.Models;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace I2MS2.Windows
{
    // 3D 도면 선택 및 도면 삭제 , 2D 벽 설계 파일 
    public partial class drawing_3d_vm
    {
        public int drawing_3d_id { get; set; }
        public string drawing_3d_name { get; set; }
        public string file_path { get; set; }
        public string file_name { get; set; }
        public string remarks { get; set; }
    }

    /// <summary>
    /// DrawingSelectDrawing3DWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DrawingSelectDrawing3DWindow : MetroWindow
    {
        public drawing_3d _select_drawing_3d;
        public drawing_3d select_drawing_3d 
        {
            get { return _select_drawing_3d; }
            set { _select_drawing_3d = value; }
        }


        List<drawing_3d_vm> d_vm_list = new List<drawing_3d_vm>();

        public DrawingSelectDrawing3DWindow()
        {
            InitializeComponent();

            foreach(var d in g.drawing_3d_list)
            {
                drawing_3d_vm d_vm = makeDrawing3DVM(d);
                d_vm_list.Add(d_vm);
            }
            _lvDrawing3DList.ItemsSource = d_vm_list;
        
    
        }
        private drawing_3d_vm makeDrawing3DVM(drawing_3d d)
        {
            return new drawing_3d_vm()
            {
                drawing_3d_id = d.drawing_3d_id,
                drawing_3d_name = d.drawing_3d_name,
                file_name = d.file_name,
                remarks = d.remarks,
                file_path = string.Format("{0}drawing_3d/{1}", g.CLIENT_IMAGE_PATH, d.file_name)
            };
        }


        private drawing_3d makeDrawing3D(drawing_3d_vm d_vm)
        {
            return new drawing_3d()
            {
                drawing_3d_id = d_vm.drawing_3d_id,
                file_name = d_vm.file_name,
                remarks = d_vm.remarks
            };
        }

        private void _lvDrawing3DList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_lvDrawing3DList.SelectedItem != null)
            {
                drawing_3d_vm select_drawing_3d_vm = (drawing_3d_vm)_lvDrawing3DList.SelectedItem;
                //_ctlDrawingView2D.openDrawingFile(string.Format("{0}drawing_3d/{1}",g.CLIENT_IMAGE_PATH, select_drawing_3d_vm.file_name);
                _ctlDrawingView2D.openDrawingFile(select_drawing_3d_vm.file_path);
                _txtFileName.Text = select_drawing_3d_vm.file_name;
                _txtRemarks.Text = select_drawing_3d_vm.remarks;
            }
        }

        private void _btnSelect_Click(object sender, RoutedEventArgs e)
        {
            var select_drawing_3d_vm = _lvDrawing3DList.SelectedItem;
            if (select_drawing_3d_vm is drawing_3d_vm)
            {
                _select_drawing_3d = makeDrawing3D((drawing_3d_vm)select_drawing_3d_vm);
                Close();
            }
        }

        private async void _btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show( g.tr_get("C_Error13"), "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }

            if (_lvDrawing3DList.SelectedItem is drawing_3d_vm)
            {
                drawing_3d_vm dr_vm = (drawing_3d_vm)_lvDrawing3DList.SelectedItem;

                // 층에서 사용중인 파일은 삭제 안되게 처리 
                var fl = g.floor_list.Find(p=>p.drawing_3d_id == dr_vm.drawing_3d_id);
                if (fl != null)
                {
                    MessageBox.Show(g.tr_get("C_Error_Cant_Delete"));
                    // 해당 층에서 사용중인 파일임 메시지 처리 
                    return;
                }
                //1.서버에서 3d 파일을 삭제하고 로컬 파일도 삭제한다
                var t1 = delDrawingFileToServer(dr_vm.file_name);
                Boolean result = await t1;

                //2. 서버에서 3d 정보를 삭제한다
                int ret = await g.webapi.delete("drawing_3d", dr_vm.drawing_3d_id);

                //3. 전역변수와 클래스 변수에서 3d 정보를 삭제한다
                drawing_3d dr = g.drawing_3d_list.Find(at => at.drawing_3d_id == dr_vm.drawing_3d_id);
                if (dr == null) return;

                g.drawing_3d_list.Remove(dr);
                d_vm_list.Remove(dr_vm);

                //4. 변경 사항을 UI에 적용
                _txtFileName.Text = null;
                _txtRemarks.Text = null;
                _lvDrawing3DList.ItemsSource = null;
                _lvDrawing3DList.ItemsSource = d_vm_list;
                
            }
        }

        private async Task<Boolean> delDrawingFileToServer(String file_name)
        {
            String sub_dir = "drawing_3d";
            Task<int> t1 = g.webapi.deleteFile(sub_dir, file_name);
            int ret = await t1;
            if (ret == 0)
            {
                string file_path = string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH, sub_dir, file_name);
                if (File.Exists(file_path) == true)
                {
                    try
                    {
                        File.Delete(file_path);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("{0}:{1}", ex.HResult, ex.Message);
                        return false;
                    }

                }
                return false;
            }
            else
                return false;

        }
    }
}
