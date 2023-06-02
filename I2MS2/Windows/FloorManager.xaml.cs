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
 
    public partial class FloorManager : Window
    {
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand SelectFileCommand = new RoutedCommand();

        private int _building_id = 0;
        private int _floor_id = 0;
        private floor _f = null;
        public int _drawing_3d_id = 0;

        public FloorManager(int building_id, int floor_id)
        {
            _building_id = building_id;
            _floor_id = floor_id;
            InitializeComponent();            
        }


        private void _window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_floor_id > 0)
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

            if (_drawing_3d_id == 0)
            {
                e.CanExecute = false;
                return;
            }

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
            int drawing_3d_id = _f == null ? 0 : (_f.drawing_3d_id ?? 0);

            DrawingSelectDrawing3DWindow DrawingWindow = new DrawingSelectDrawing3DWindow();
            DrawingWindow.ShowDialog();
            if (DrawingWindow.select_drawing_3d != null)
            {
                drawing_3d select_drawing_3d = DrawingWindow.select_drawing_3d;
                string path = string.Format("{0}drawing_3d/{1}", g.CLIENT_IMAGE_PATH, select_drawing_3d.file_name);
                Boolean result = _ctlDrawingView2D.openDrawingFile(path);
                if (result == true)
                {
                    _drawing_3d_id = select_drawing_3d.drawing_3d_id;
                }
            }
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
            _f = g.floor_list.Find(p => p.floor_id == _floor_id);
            if (_f == null)
                return;

            _drawing_3d_id = _f.drawing_3d_id ?? 0;
        }

        private void dispData()
        {
            if (_f == null)
                return;

            _spinFloorLevel.SpinValue = _f.floor_no;
            _txtName.Text = _f.floor_name;
            _txtRemarks.Text = _f.remarks;

            dispDrawing(_drawing_3d_id);
        }

        private async Task<bool> saveData()
        {
            int floor_level = _spinFloorLevel.SpinValue;
            string floor_name = _txtName.Text;

            // 신규이면?
            if (_floor_id == 0)
            {
                // 같은 빌딩 내에서 층명이 같으면 안된다.
                var at = g.location_list.Find(p => (p.location_name == floor_name) && (p.building_id == _building_id));
                if (at != null)
                {
                    MessageBox.Show(g.tr_get("C_Info53"));
                    return false;
                }

                // 같은 빌딩 내에서 층 번호가 같으면 안된다.
                var f1 = g.floor_list.Find(p => (p.floor_no == floor_level) && (p.building_id == _building_id));
                if (f1 != null)
                {
                    MessageBox.Show(g.tr_get("C_Error_3"));
                    return false;
                }     

                _f = new floor();
                _f.floor_no = floor_level;
                _f.floor_name = floor_name;
                _f.drawing_3d_id = _drawing_3d_id;
                _f.remarks = _txtRemarks.Text;
                _f.building_id = _building_id;
                _f.user_id = g.login_user_id;

                var ff = (floor) await g.webapi.post("floor", _f, typeof(floor));
                if (ff == null)
                    return false;

                g.floor_list.Add(ff);
                
                _floor_id = ff.floor_id;

                bool b = await g.left_tree_handler.addFloor(_floor_id);
                if (!b)
                    return false;
                int location_id = Etc.get_location_id_by_floor_id(_floor_id);
                g.signalr.send_location_to_signalr(location_id, eAction.eAdd);
            }
            else
            {
                // 같은 빌딩 내에서 층명이 같으면 안된다.
                var at = g.location_list.Find(p => (p.location_name == floor_name) && (p.floor_id != _floor_id) && (p.building_id == _building_id));
                if (at != null)
                {
                    MessageBox.Show(g.tr_get("C_Info53"));
                    return false;
                }

                // 같은 빌딩 내에서 층 번호가 같으면 안된다.
                var f1 = g.floor_list.Find(p => (p.floor_no == floor_level) && (p.floor_id != _floor_id) && (p.building_id == _building_id));
                if (f1 != null)
                {
                    MessageBox.Show(g.tr_get("C_Error_3"));
                    return false;
                }     

                _f.floor_no = floor_level;
                _f.floor_name = floor_name;
                _f.drawing_3d_id = _drawing_3d_id;
                _f.remarks = _txtRemarks.Text;
                _f.building_id = _building_id;
                _f.user_id = g.login_user_id;
                var rr = await g.webapi.put("floor", _floor_id, _f, typeof(floor));
                if (rr != 0)
                    return false;

                bool b = await g.left_tree_handler.editFloor(_floor_id, _f.floor_name);
                if (!b)
                    return false;
                int location_id = Etc.get_location_id_by_floor_id(_floor_id);
                g.signalr.send_location_to_signalr(location_id, eAction.eModify);
            }

            return true;
        }

        private void selectImageFile(int image_id)
        {
            SelectDrawing3d window = new SelectDrawing3d(image_id);
            window.Owner = this;
            bool b = window.ShowDialog() ?? false;
            if (b == true)
                _drawing_3d_id = g.result_drawing_3d_id;

            dispDrawing(_drawing_3d_id);
        }

        private void dispDrawing(int drawing_3d_id)
        {
            string file_name = "no image";
            var d3 = g.drawing_3d_list.Find(p => p.drawing_3d_id == drawing_3d_id);
            if (d3 != null)
            {
                file_name = d3.file_name;
                string path = string.Format("{0}drawing_3d/{1}", g.CLIENT_IMAGE_PATH, d3.file_name);
                Boolean result = _ctlDrawingView2D.openDrawingFile(path);
                if (result == true)
                {
                    _drawing_3d_id = d3.drawing_3d_id;
                }
            }
        }
        #endregion
    }
}
