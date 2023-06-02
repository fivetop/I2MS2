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
using System.Diagnostics;
using MahApps.Metro.Controls;

namespace I2MS2.Windows
{
    /// <summary>
    /// DrawingsManager.xaml에 대한 상호 작용 논리
    /// </summary>
    ///
 
    // 룸 추가에서 사용 
    public partial class RoomManager : MetroWindow
    {
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand SelectFileCommand = new RoutedCommand();

        private int _floor_id = 0;
        private int _room_id = 0;
        private room _r = null;
        private Thickness _rect = new Thickness(0,0,0,0);
        private bool _rect_flag = true;

        public RoomManager(int floor_id, int room_id)
        {
            _floor_id = floor_id;
            _room_id = room_id;
            InitializeComponent();
        }

        private void _window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_room_id > 0)
           {
                initData();
                dispData();
            }
            else
            {
                var f = g.floor_list.Find(p => p.floor_id == _floor_id);
                if (f != null)
                    dispDrawing(f.drawing_3d_id ?? 0);
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

            if (!_rect_flag)
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
            e.CanExecute = !string.IsNullOrEmpty(_txtName.Text);
        }

        private void _cmdSelectFile_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _rectBox.IsEnabled = true;
            _rect_flag = true;
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
            _r = g.room_list.Find(p => p.room_id == _room_id);
            if (_r == null)
                return;
        }

        private const double SCREEN_W = 800;
        private const double SCREEN_H = 600;

        private void dispData()
        {
            if (_r == null)
                return;

            _txtName.Text = _r.room_name;
            _txtRemarks.Text = _r.remarks;
            Thickness th = new Thickness();
            th.Left = ScreenRatio.getScreenX(_r.square_x1, SCREEN_W);
            th.Top = ScreenRatio.getScreenY(_r.square_y1, SCREEN_H);
            double w = ScreenRatio.getScreenX((_r.square_x2 ?? 0) - (_r.square_x1 ?? 0) + 1, SCREEN_W);
            double h = ScreenRatio.getScreenY((_r.square_y2 ?? 0) - (_r.square_y1 ?? 0) + 1, SCREEN_H);
            _rectBox.Margin = th;
            _rectBox.Width = w;
            _rectBox.Height = h;
            _rect_flag = true;

            var f = g.floor_list.Find(p => p.floor_id == _r.floor_id);
            if (f != null)
                dispDrawing(f.drawing_3d_id ?? 0);
        }


        private async Task<bool> saveData()
        {
            string room_name = _txtName.Text.Trim();
            var l = g.location_list.Find(p => p.floor_id == _floor_id);
            if (l == null)
                return false;
            int building_id = l.building_id ?? 0;

            // 신규이면?
            if (_room_id == 0)
            {
                // 같은 빌딩 내에서 룸명이 같으면 안된다.
                var at = g.location_list.Find(p => (p.location_name == room_name) && (p.building_id == building_id));
                if (at != null)
                {
                    MessageBox.Show(g.tr_get("C_Error_4"));
                    return false;
                }     

                _r = new room();
                getData();

                var rr = (room) await g.webapi.post("room", _r, typeof(room));
                if (rr == null)
                    return false;

                g.room_list.Add(rr);
                _room_id = rr.room_id;

                var rr2 = await g.webapi.put("room", _room_id, rr, typeof(room));
                if (rr2 != 0)
                    return false;                                               

                bool b = await g.left_tree_handler.addRoom(_room_id);
                if (!b)
                    return false;
                int location_id = Etc.get_location_id_by_room_id(_room_id);
                g.signalr.send_location_to_signalr(location_id, eAction.eAdd);
            }
            else
            {
                // 같은 빌딩 내에서 층명이 같으면 안된다.
                var at = g.location_list.Find(p => (p.location_name == room_name) && (p.room_id != _room_id) && (p.building_id == building_id));
                if (at != null)
                {
                    MessageBox.Show(g.tr_get("C_Error_4"));
                    return false;
                }

                getData();
                var rr = await g.webapi.put("room", _room_id, _r, typeof(room));
                if (rr != 0)
                    return false;

                bool b = await g.left_tree_handler.editRoom(_room_id, _r.room_name);
                if (!b)
                    return false;
                int location_id = Etc.get_location_id_by_room_id(_room_id);
                g.signalr.send_location_to_signalr(location_id, eAction.eModify);
            }

            return true;
        }

        private void getData()
        {
            _r.room_name = _txtName.Text.Trim();
            _r.remarks = _txtRemarks.Text.Trim();
            _r.floor_id = _floor_id;
            _r.user_id = g.login_user_id;
            _r.square_x1 = ScreenRatio.getDbX(_rectBox.Margin.Left, SCREEN_W);
            _r.square_y1 = ScreenRatio.getDbY(_rectBox.Margin.Top, SCREEN_H);
            _r.square_x2 = ScreenRatio.getDbX(_rectBox.Margin.Left + _rectBox.Width, SCREEN_W);
            _r.square_y2 = ScreenRatio.getDbY(_rectBox.Margin.Top + _rectBox.Height, SCREEN_H);
            _r.flag_x = (_r.square_x1 + _r.square_x2) / 2;
            _r.flag_y = (_r.square_y1 + _r.square_y2) / 2;

            // 2015.06.24 룸 위치 지정중 기존에 잘못된 값이 있을지 몰라서 초기화 처리 romee
            if (_r.square_x2 < 10 || _r.square_y2 <10 || _r.flag_x <10 || _r.flag_y < 10)
            {
                _r.square_x1 = _r.square_y1 = 0;
                _r.square_x2 = _r.square_y2 = 0; 
                _r.flag_x = _r.flag_y = 0;
            }
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
                    _txtFileName.Text = d3.drawing_3d_name;
                }
                //drawAllObject2D();
            }


        }


        //private void drawAllObject2D()
        //{
        //    List<rack> rack_list = new List<rack>();
        //    int i = 0;
        //    rack_list = new List<rack>();
        //    rack_list = g.rack_list.FindAll(at => at.room_id == _r.room_id);

        //    foreach (rack r in rack_list)
        //    {
        //        _ctlDrawingView2D.addRack(r);
        //    }
        //}
        
        #endregion

        #region 마우스 드래그 관련 이벤트
        Point selected_point;
        bool drag_flag = false;
        bool size_flag = false;

        private void _rectBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point selectP = e.GetPosition(_rectBox);
            selected_point = new Point(selectP.X, selectP.Y);
            drag_flag = true;
        }

        private void _window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            drag_flag = false;
            size_flag = false;
        }

        private void _window_MouseLeave(object sender, MouseEventArgs e)
        {
            drag_flag = false;
            size_flag = false;
        }

        private void right_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point selectP = e.GetPosition(_rectBox);

            selected_point = new Point(selectP.X, selectP.Y);
            size_flag = true;
            e.Handled = true;
        }

        // 4:3 비율의 사이즈를 유지한다.
        private void right_MouseMove(object sender, MouseEventArgs e)
        {
            if (size_flag)
            {
                Point new_point = e.GetPosition(_rectBox);

                double x_move = new_point.X - selected_point.X;

                double width = _rectBox.Width + x_move;
                if (width < 80)
                    width = 80;
                if (width > (_gridDrawings.ActualWidth - _rectBox.Margin.Left))
                    width = _gridDrawings.ActualWidth - _rectBox.Margin.Left;

                double height = width * 0.75;
                if (height < 60)
                    height = 60;
                if (height > (_gridDrawings.ActualHeight - _rectBox.Margin.Top))
                {
                    height = _gridDrawings.ActualHeight - _rectBox.Margin.Top;
                    width = height * (4.0 / 3.0);
                }

                _rectBox.Width = width > 0 ? width : 0;
                _rectBox.Height = height > 0 ? height : 0;

                selected_point = new Point(new_point.X, new_point.Y);
                e.Handled = true;
            }

            if (drag_flag)
            {
                Point new_point = e.GetPosition(_rectBox);
                Thickness th = _rectBox.Margin;

                double x_move = new_point.X - selected_point.X;
                double x = th.Left + x_move;
                if (x > (_gridDrawings.ActualWidth - _rectBox.Width))
                    x = _gridDrawings.ActualWidth - _rectBox.Width;
                th.Left = x > 0 ? x : 0;

                double y_move = new_point.Y - selected_point.Y;
                double y = th.Top + y_move;
                if (y > (_gridDrawings.ActualHeight - _rectBox.Height))
                    y = _gridDrawings.ActualHeight - _rectBox.Height;
                th.Top = y > 0 ? y : 0;

                _rectBox.Margin = th;
            }
        }
        #endregion

        
    }
  
}
