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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace I2MS2.UserControls
{
    /// <summary>
    /// RoomLayoutControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class RoomLayoutControl : UserControl
    {
        Point selected_point;
        bool drag_flag = false;
        bool size_flag = false;
        bool point_drag_flag = false;

        Double point_margin_top_in_box;
        Double point_margin_left_in_box;

        public RoomLayoutControl()
        {
            InitializeComponent();

            point_margin_left_in_box = _rectRoomBox.Width / 2;
            point_margin_top_in_box = _rectRoomBox.Height / 2;
            Thickness th = new Thickness(_rectRoomBox.Margin.Left + _rectRoomBox.Width/2
                , _rectRoomBox.Margin.Top + _rectRoomBox.Height/2,0,0);
            _gridNamePoint.Margin = th;
        }

        #region RoomLayoutControl
        public void IsEnable(Boolean en)
        {
            _rectRoomBox.IsEnabled = en;
        }

        private void _ellipsePoint_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_rectRoomBox.IsEnabled == true)
            {
                //Point selectP = e.GetPosition(_rectRoomBox);
                Point selectP = e.GetPosition(_gridDrawings);
                selected_point = new Point(selectP.X, selectP.Y);
                point_drag_flag = true;
                _ellipseNamePoint.Fill = Brushes.OrangeRed;

                //   Console.WriteLine("=== select point({0},{1}) === ", selected_point.X, selected_point.Y);
            }
        }

        private void _rectRoomBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point selectP = e.GetPosition(_rectRoomBox);
            selected_point = new Point(selectP.X, selectP.Y);
            drag_flag = true;

        }

        private void right_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point selectP = e.GetPosition(_rectRoomBox);

            selected_point = new Point(selectP.X, selectP.Y);
            size_flag = true;
            e.Handled = true;
        }

        private void _gridDrawings_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            drag_flag = false;
            size_flag = false;
            point_drag_flag = false;


            _ellipseNamePoint.Fill = Brushes.Gray;
        }

        // 4:3 비율의 사이즈를 유지한다.
        private void _gridDrawings_MouseMove(object sender, MouseEventArgs e)
        {
            if (size_flag)
            {
                Point new_point = e.GetPosition(_rectRoomBox);
                Thickness name_old_th = _gridNamePoint.Margin;
                Thickness name_new_th = new Thickness(0, 0, 0, 0);

                double x_move = new_point.X - selected_point.X;

                double width = _rectRoomBox.Width + x_move;
                if (width < 80)
                    width = 80;
                if (width > (_gridDrawings.ActualWidth - _rectRoomBox.Margin.Left))
                {
                    width = _gridDrawings.ActualWidth - _rectRoomBox.Margin.Left;

                }



                double height = width * 0.75;
                if (height < 60)
                    height = 60;
                if (height > (_gridDrawings.ActualHeight - _rectRoomBox.Margin.Top))
                {
                    height = _gridDrawings.ActualHeight - _rectRoomBox.Margin.Top;
                    width = height * (4.0 / 3.0);

                }
                _rectRoomBox.Width = width > 0 ? width : 0;
                _rectRoomBox.Height = height > 0 ? height : 0;

                selected_point = new Point(new_point.X, new_point.Y);
                e.Handled = true;



                Double name_x_move = x_move / 2;
                if ((_rectRoomBox.Margin.Left - name_x_move < 0) && (_rectRoomBox.Margin.Left + _rectRoomBox.Width - _ellipseNamePoint.Width - name_x_move > 0))
                {
                    Double new_left = name_old_th.Left + name_x_move;
                    name_new_th.Left = new_left > 0 ? new_left : name_old_th.Left;
                }
                else
                    name_new_th.Left = name_old_th.Left;

                Double name_y_move = x_move * (3.0 / 4.0) / 2;
                if ((_rectRoomBox.Margin.Top - name_y_move < 0) && (_rectRoomBox.Margin.Top + _rectRoomBox.Height - _ellipseNamePoint.Height - name_y_move > 0))
                {
                    Double new_top = name_old_th.Top + name_y_move;
                    name_new_th.Top = new_top > 0 ? new_top : name_old_th.Top;
                }
                else
                    name_new_th.Top = name_old_th.Top;


                _gridNamePoint.Margin = name_new_th;
                point_margin_left_in_box = _gridNamePoint.Margin.Left - _rectRoomBox.Margin.Left;
                point_margin_top_in_box = _gridNamePoint.Margin.Top - _rectRoomBox.Margin.Top;
            }

            if (drag_flag)
            {
                Point new_point = e.GetPosition(_rectRoomBox);
                Thickness th = _rectRoomBox.Margin;

                double x_move = new_point.X - selected_point.X;
                double x = th.Left + x_move;
                if (x > (_gridDrawings.ActualWidth - _rectRoomBox.Width))
                    x = _gridDrawings.ActualWidth - _rectRoomBox.Width;
                th.Left = x > 0 ? x : 0;

                double y_move = new_point.Y - selected_point.Y;
                double y = th.Top + y_move;
                if (y > (_gridDrawings.ActualHeight - _rectRoomBox.Height))
                    y = _gridDrawings.ActualHeight - _rectRoomBox.Height;
                th.Top = y > 0 ? y : 0;

                _rectRoomBox.Margin = th;

                Thickness el_th = new Thickness(th.Left + point_margin_left_in_box, th.Top + point_margin_top_in_box, 0, 0);
                _gridNamePoint.Margin = el_th;
                //Console.WriteLine("drag_flag NameMargin:({0},{1})",_gridNamePoint.Margin.Left, _gridNamePoint.Margin.Top);

            }

            if (point_drag_flag)
            {
                //Point new_point = e.GetPosition(_rectRoomBox);
                Point new_point = e.GetPosition(_gridDrawings);
                Thickness old_th = _gridNamePoint.Margin;
                Thickness th = _gridNamePoint.Margin;

                double x_move = new_point.X - selected_point.X;
                double x = old_th.Left + x_move;
                if ((_rectRoomBox.Margin.Left - x < 0) && (_rectRoomBox.Margin.Left + _rectRoomBox.Width - _ellipseNamePoint.Width - x > 0))
                {
                    th.Left = x;
                    selected_point.X = new_point.X;
                    point_margin_left_in_box = _gridNamePoint.Margin.Left - _rectRoomBox.Margin.Left;
                }


                double y_move = new_point.Y - selected_point.Y;
                double y = old_th.Top + y_move;
                if ((_rectRoomBox.Margin.Top - y < 0) && (_rectRoomBox.Margin.Top + _rectRoomBox.Height - _ellipseNamePoint.Height - y > 0))
                {
                    th.Top = y;
                    selected_point.Y = new_point.Y;
                    point_margin_top_in_box = _gridNamePoint.Margin.Top - _rectRoomBox.Margin.Top;
                }

                _gridNamePoint.Margin = th;
                selected_point = new_point;


                //Console.WriteLine("point_drag_flag old:({0},{1}), move({2},{3})",old_th.Left, old_th.Top, x_move,y_move);
                //Console.WriteLine("point_drag_flag NameMargin:({0},{1})", _gridNamePoint.Margin.Left, _gridNamePoint.Margin.Top);
            }
        }
        
        #endregion

       

    }
}
