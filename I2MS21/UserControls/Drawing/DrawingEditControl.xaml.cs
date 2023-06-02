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


using I2MS2.Models;
using I2MS2.Library.Drawing;
using I2MS2.Windows;
using WebApi.Models;

using System.ComponentModel;

using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using I2MS2.Animation;
using System.Xml.Serialization;


namespace I2MS2.UserControls.Drawing
{
    // 드로잉 파일 에디처 처리 
    enum DrawingMode
    {
        NONE,
        MOVE,       // 패닝
        DRAWING,    // 그리기    
        MODIFY,     // 수정
        WALLPROP_PICK,  // 벽 속성 
        DELETE
    };
    /// <summary>
    /// DrawingEditControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DrawingEditControl : UserControl
    {
        #region define for command
        Boolean layer1_bt_canexcute = true;
        Boolean layer2_bt_canexcute = true;
        Boolean layer3_bt_canexcute = true;
        Boolean layer4_bt_canexcute = true;
        Boolean selectall_menu_canexcute = false;
        Boolean move_bt = false;
        Boolean modify_bt = false;
        Boolean draw_bt = false;

        public static RoutedCommand Cmd_btnShowLayer1 = new RoutedCommand();
        public static RoutedCommand Cmd_btnShowLayer2 = new RoutedCommand();
        public static RoutedCommand Cmd_btnShowLayer3 = new RoutedCommand();
        public static RoutedCommand Cmd_btnShowLayer4 = new RoutedCommand();
        public static RoutedCommand Cmd_menuSelectOneLayerAllWall = new RoutedCommand();
        public static RoutedCommand Cmd_btnMove = new RoutedCommand();
        public static RoutedCommand Cmd_btnDraw = new RoutedCommand();
        public static RoutedCommand Cmd_btnModify = new RoutedCommand();
        public static RoutedCommand Cmd_selectDrawLayer = new RoutedCommand();
        #endregion

        #region define other value
        DrawingMode drawing_mode = DrawingMode.NONE;

        Point scl_offset = new Point(0, 0);
        Point scl_last_p = new Point(0, 0);
        
        Point start_p;
        Point last_p;

        WallDraw pick_w;
        WallDraw connect_w;
        WallDraw modify_w;

        Drawing2D drawer2d;
        //Drawing3D drawer3d;
        DrawingDataManager drawDataMgr;

        Size canvas_size;

        //show layer 1~4, programming cur_layer 0~3
        int cur_layer = -1;

        //wall을 화면에 표시할때 구분하는 번호이다
        //wall을 다시 로딩하게 되면 다시 작성해서 넣어주게 된다
        int wall_static_id = 0;
        int wallc_static_num = 0;
        int last_draw_wall_num = -1;
        //int selected_wall_num = -1;
        List<int> selected_wall_id_list = new List<int>();
        private string save_path;

        private SolidColorBrush wall_brush;
        private Double wall_thick;
        private Double wall_height;
        private Double wall_alpha;
        //private List<WallDraw> wall_vm_list = new List<WallDraw>();
        private List<WallDraw>[] wall_vm_list = new List<WallDraw>[4];
        private List<WallCornerDraw>[] wallc_list = new List<WallCornerDraw>[4];
        
        #endregion

        #region Initialize
        public DrawingEditControl()
        {
            InitializeComponent();

            drawDataMgr = new DrawingDataManager();

            initUI();
            initWallInfo();
            drawer2d = new Drawing2D(_canvasDrawing);
            drawer2d.wall_brush = wall_brush;
            drawer2d.PickWallPointEvent += new Drawing2D.PickPointEventHander(pickWallPointEvent);
            drawer2d.ConnectWallPointEvent += new Drawing2D.ConnectWallPointEventHander(connectWallPointEvent);
            drawer2d.SelectWallEvent += new Drawing2D.SelectWallEventHander(selectWallEvent);
          
            _ctlColorSelector.colorPickEvent += new ColorSelector.ColorPickEventHandler(ColorSelector_colorPickEvent);

            _btnShowLayer1.IsChecked = true;
            _btnShowLayer2.IsChecked = false;
            _btnShowLayer3.IsChecked = false;
            _btnShowLayer4.IsChecked = false;

        }


        // 화면 초기화 
        private void initUI()
        {

            _btnImageShow.IsChecked = true;
            _btnGridShow.IsChecked = true;

            drawGuideLine(_gridGuide,
                new Point(0, 0), new Point(1024, 768));

            canvas_size = new Size(1024, 768);

            for (int i = 0; i < 4; i++)
            {
                wall_vm_list[i] = new List<WallDraw>();
                wallc_list[i] = new List<WallCornerDraw>();
            }

            SimpleAnimation anim = new SimpleAnimation();
            ScaleTransform scaleTran = new ScaleTransform();
            scaleTran.CenterX = _grid3DView.Width;
            scaleTran.CenterY = _grid3DView.Height;
            scaleTran.ScaleX = 0.25;
            scaleTran.ScaleY = 0.25;
            _grid3DView.RenderTransform = scaleTran;

        }

        private void initWallInfo()
        {
            wall_brush = App.Current.Resources["_brushBlue"] as SolidColorBrush;
            wall_thick = 3;
            wall_height = 20;
            wall_alpha = 1;

        } 
        #endregion

        //private void _ctlDrawingEdit_LayoutUpdated(object sender, EventArgs e)
        //{
        //    _radioLayer1.IsChecked = true;
        //    _btnDraw.IsChecked = true;
        //}
        // 화면 로드후  
        private void _ctlDrawingEdit_Loaded(object sender, RoutedEventArgs e)
        {
            _radioLayer1.IsChecked = true;
            _btnMove.IsChecked = true;
        }
        // 벽 속성 컨트롤 로드후 실행
        private void _ctlWallProp_Loaded(object sender, RoutedEventArgs e)
        {
            wall_thick = _ctlWallProp.setWallThiness((int)wall_thick);
            wall_height = _ctlWallProp.setWallHeight((int)wall_height);
            //wall_alpha = _ctlWallProp.setAlpha(wall_alpha);
        }

        #region Draw Guide Line Method  격자 처리
        private void reDrawGuideLine(Point startPoint, Point endPoint)
        {
            _gridGuide.Children.Clear();
            drawGuideLine(_gridGuide, startPoint, endPoint);
        }

        //draw Guilde Line
        public void drawGuideLine(Grid grid, Point startPoint, Point endPoint)
        {
            int _horizontalLineCount = (int)endPoint.Y / 10 + 1;
            int _verticalLineCount = (int)endPoint.X / 10 + 1;

            Line[] HorizontalLine = new Line[_horizontalLineCount];
            for (int i = 0; i < _horizontalLineCount; i++)
            {
                HorizontalLine[i] = new Line();
                HorizontalLine[i].Opacity = 0.5;
                if (i % 5 == 0)
                    HorizontalLine[i].StrokeThickness = 1;
                else
                    HorizontalLine[i].StrokeThickness = 0.2;

                HorizontalLine[i].Stroke = Brushes.DarkBlue;
                HorizontalLine[i].X1 = startPoint.X;
                HorizontalLine[i].Y1 = startPoint.Y + i * 10;
                HorizontalLine[i].X2 = endPoint.X;
                HorizontalLine[i].Y2 = startPoint.Y + i * 10;

                grid.Children.Add(HorizontalLine[i]);
            }

            Line[] VerticalLine = new Line[_verticalLineCount];
            for (int j = 0; j < _verticalLineCount; j++)
            {
                VerticalLine[j] = new Line();
                VerticalLine[j].Opacity = 0.5;
                if ((j % 5) == 0)
                    VerticalLine[j].StrokeThickness = 0.5;
                else
                    VerticalLine[j].StrokeThickness = 0.1;
                //VerticalLine[j].Stroke = SystemColors.WindowFrameBrush;
                VerticalLine[j].Stroke = Brushes.DarkBlue;
                VerticalLine[j].X1 = startPoint.X + j * 10;
                VerticalLine[j].Y1 = startPoint.Y;
                VerticalLine[j].X2 = startPoint.X + j * 10;
                VerticalLine[j].Y2 = endPoint.Y;
                grid.Children.Add(VerticalLine[j]);
            }
        } 
        #endregion

        #region 드로윙 레이어 및 방식 선텍 메뉴 토글 버튼 이벤트
#if false
        //show layer 1~4, programing 0~3
        private void _cboDrawLayer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_cboDrawLayer.SelectedValue == null)
                return;
            ComboBoxItem cbo_item = _cboDrawLayer.SelectedValue as ComboBoxItem;
            string text = cbo_item.Content as string;

            releaseWall();

            int layer = findNumberFromString(text) - 1;
            cur_layer = layer;


            int last_count = wall_vm_list[layer].Count - 1;
            if (last_count > -1)
                last_draw_wall_num = wall_vm_list[layer][last_count].number;

            switch (layer)
            {
                case 0:
                    _btnShowLayer1.Command.Execute(sender);
                    break;
                case 1:
                    _btnShowLayer2.Command.Execute(sender);
                    break;
                case 2:
                    _btnShowLayer3.Command.Execute(sender);
                    break;
                case 3:
                    _btnShowLayer4.Command.Execute(sender);
                    break;

            }

        } 
#else
        private void _radioLayer1_Checked(object sender, RoutedEventArgs e)
        {
            move_bt = true;
            modify_bt = true;
            draw_bt = true;
            editLayerSelectChanged(0);
        }
        private void _radioLayer2_Checked(object sender, RoutedEventArgs e)
        {
            move_bt = true;
            modify_bt = true;
            draw_bt = true;
            editLayerSelectChanged(1);
        }
        private void _radioLayer3_Checked(object sender, RoutedEventArgs e)
        {
            move_bt = true;
            modify_bt = true;
            draw_bt = true;

            editLayerSelectChanged(2);
        }
        private void _radioLayer4_Checked(object sender, RoutedEventArgs e)
        {
            move_bt = true;
            modify_bt = true;
            draw_bt = true;

            editLayerSelectChanged(3);
        }
        private void editLayerSelectChanged(int layer)
        {
            releaseWall();
            cur_layer = layer ;

            int last_count = wall_vm_list[layer].Count - 1;
            if (last_count > -1)
                last_draw_wall_num = wall_vm_list[layer][last_count].id;

            switch (layer)
            {
                case 0:
                    _btnShowLayer1.Command.Execute(layer);
                    break;
                case 1:
                    _btnShowLayer2.Command.Execute(layer);
                    break;
                case 2:
                    _btnShowLayer3.Command.Execute(layer);
                    break;
                case 3:
                    _btnShowLayer4.Command.Execute(layer);
                    break;
            }
        }
#endif
        // 도면 이동 모드
        private void _btnMove_Checked(object sender, RoutedEventArgs e)
        {
            if (cur_layer != -1)
            {
                move_bt = false;
                draw_bt = true;
                modify_bt = true;
                _btnModify.IsChecked = false;
                _btnDraw.IsChecked = false;
                selectall_menu_canexcute = false;

                //_ctlDrawingEdit.Cursor = Cursors.Hand;
                _canvasDrawing.Cursor = Cursors.Hand;
                drawing_mode = DrawingMode.MOVE;
            }
        }
        // 도면 편집 모드 
        private void _btnDraw_Checked(object sender, RoutedEventArgs e)
        {
            if (cur_layer != -1)
            {
                move_bt = true;
                draw_bt = false;
                modify_bt = true;
                _btnMove.IsChecked = false;
                _btnModify.IsChecked = false;
                selectall_menu_canexcute = false;

                if(drawing_mode== DrawingMode.MODIFY)
                {
                    releaseWall();
                }


                //_ctlDrawingEdit.Cursor = Cursors.Pen;
                _canvasDrawing.Cursor = Cursors.Pen;
                drawing_mode = DrawingMode.DRAWING;
                _ctlColorSelector.set_wallPropPick_mode(true);
            }
            else
            {
                _btnDraw.IsChecked = false;
                MessageBox.Show("Please Select Layer!!");
            }
        }
        // 아이템 선택후 수정 모드 
        private void _btnModify_Checked(object sender, RoutedEventArgs e)
        {
            if (cur_layer != -1)
            {
                selectall_menu_canexcute = true;
                move_bt = true;
                draw_bt = true;
                modify_bt = false;

                _btnMove.IsChecked = false;
                _btnDraw.IsChecked = false;
                drawing_mode = DrawingMode.MODIFY;
                _menuSelectOneLayerAllWall.Command.Execute(true);
                //_ctlDrawingEdit.Cursor = Cursors.Arrow;
                _canvasDrawing.Cursor = Cursors.Arrow;
                _ctlColorSelector.set_wallPropPick_mode(false); // 피커 해제 처리 
            }
            else
            {
                _btnDraw.IsChecked = false;
                MessageBox.Show("Please Select Layer!!");
            }
        }



        //private void _btnDraw_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    //_ctlDrawingEdit.Cursor = Cursors.Arrow;
        //    //drawing_mode = DrawingMode.NONE;
        //}

        //private void _btnModify_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    //_ctlDrawingEdit.Cursor = Cursors.Arrow;
        //    //drawing_mode = DrawingMode.NONE;
        //    //(sender as ToggleButton).Command.Execute(sender);

        //    //releaseWall();
        //}



        #endregion

        #region Zoom Button Event
#if false
        private void _btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            releaseWall();
            Double w = _gridDrawing.ActualWidth;
            Double h = _gridDrawing.ActualHeight;

            _gridDrawing.Width = w * 2;
            _gridDrawing.Height = h * 2;

            _canvasDrawing.Width = w * 2;
            _canvasDrawing.Height = h * 2;

            _imgDrawingBase.Width = w * 2;
            _imgDrawingBase.Height = h * 2;

            wall_thick = wall_thick * 2;
            canvas_size = new Size(w * 2, h * 2);
            reDrawGuideLine(new Point(0, 0), new Point(w * 2, h * 2));
            reDrawWall();
        }

        private void _btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            releaseWall();
            Double w = _gridDrawing.ActualWidth;
            Double h = _gridDrawing.ActualHeight;

            _gridDrawing.Width = w / 2;
            _gridDrawing.Height = h / 2;

            _canvasDrawing.Width = w / 2;
            _canvasDrawing.Height = h / 2;

            _imgDrawingBase.Width = w / 2;
            _imgDrawingBase.Height = h / 2;


            wall_thick = wall_thick / 2;
            canvas_size = new Size(w / 2, h / 2);
            reDrawGuideLine(new Point(0, 0), new Point(w / 2, h / 2));
            reDrawWall();
        }  
#endif
        #endregion

        #region Select Grid & Image Show mode Event
        // 격자 선 보이기 
        private void _btnGridShow_Checked(object sender, RoutedEventArgs e)
        {
            _gridGuide.Opacity = 1;
        }
        private void _btnGridShow_Unchecked(object sender, RoutedEventArgs e)
        {
            _gridGuide.Opacity = 0;
        }
        // 평면도 보이기 
        private void _btnImageShow_Checked(object sender, RoutedEventArgs e)
        {
            _imgDrawingBase.Opacity = 1;
        }
        private void _btnImageShow_Unchecked(object sender, RoutedEventArgs e)
        {
            _imgDrawingBase.Opacity = 0;
        } 
        #endregion

        #region Select Show Layer Button Event
        private void _btnShowLayer1_Checked(object sender, RoutedEventArgs e)
        {
            if (_ctlDrawingEdit.IsLoaded==true)
                showOneLayerWall(0);
        }
        private void _btnShowLayer2_Checked(object sender, RoutedEventArgs e)
        {
            if (_ctlDrawingEdit.IsLoaded == true)
                showOneLayerWall(1);
        }
        private void _btnShowLayer3_Checked(object sender, RoutedEventArgs e)
        {
            if (_ctlDrawingEdit.IsLoaded == true)
                showOneLayerWall(2);
        }
        private void _btnShowLayer4_Checked(object sender, RoutedEventArgs e)
        {
            if (_ctlDrawingEdit.IsLoaded == true)
                showOneLayerWall(3);
        }
        private void _btnShowLayer1_Unchecked(object sender, RoutedEventArgs e)
        {
           hideOneLayerWall(0);
        }
        private void _btnShowLayer2_Unchecked(object sender, RoutedEventArgs e)
        {
            hideOneLayerWall(1);
        }
        private void _btnShowLayer3_Unchecked(object sender, RoutedEventArgs e)
        {
            hideOneLayerWall(2);
        }
        private void _btnShowLayer4_Unchecked(object sender, RoutedEventArgs e)
        {
            hideOneLayerWall(3);
        } 
        #endregion

        #region KeyBoard Event // 키보드
        private void _ctlDrawingEdit_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    switch (drawing_mode)
                    {
                        case DrawingMode.DRAWING:
                        case DrawingMode.MODIFY:
                        case DrawingMode.DELETE:
                        case DrawingMode.NONE:
                            _btnDraw.IsChecked = false;
                            _btnModify.IsChecked = false;

                            _ctlDrawingEdit.Cursor = Cursors.Arrow;
                            drawing_mode = DrawingMode.NONE;
                            break;
                    }
                    break;

                case Key.Delete:
                    switch (drawing_mode)
                    {
                        case DrawingMode.DRAWING:
                            break;
                        case DrawingMode.MODIFY:
                            if (selected_wall_id_list.Count != 0)
                            {
                                removeSelectWall();
                            }
                            break;
                        case DrawingMode.DELETE:
                        case DrawingMode.NONE:
                            break;
                    }

                    break;

                case Key.Z:
                    if (Keyboard.IsKeyDown(Key.LeftCtrl))
                    {
                        switch (drawing_mode)
                        {
                            case DrawingMode.DRAWING:
                                //remove last wall
                                removeLastDrawWall();
                                break;
                            case DrawingMode.MODIFY:
                            case DrawingMode.DELETE:
                            case DrawingMode.NONE:
                                break;
                        }
                    }
                    break;
            }
        } 
        #endregion

        #region Drawing Canvas Mouse Event // 마우스로 그리기 처리 

        private void _canvasDrawing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch (drawing_mode)
            {
                case DrawingMode.MOVE:
                    scl_last_p = e.GetPosition(_canvasDrawing);

                    break;
                case DrawingMode.DRAWING:
                    Point cur_p = makeSimplePoint(e.GetPosition(_canvasDrawing));
                    start_p = cur_p;
                    break;

                case DrawingMode.MODIFY:
                    if (e.Source is DrawingEdit2DWall)
                    {
                        //Line wl = (Line)e.Source;
                        //int selected_wall_num = findNumberFromString(wl.Name);

                        //WallDraw w_vm = wall_vm_list[cur_layer].Find(at => at.id == selected_wall_num);
                        //if (w_vm != null)
                        //{
                        //    if (!Keyboard.IsKeyDown(Key.LeftCtrl))
                        //    {
                        //        releaseWall();
                        //    }

                            
                        //    drawer2d.selectWall(selected_wall_num);
                        //    selected_wall_id_list.Add(selected_wall_num);

                        //    _ctlDrawing3D.selectWall(w_vm);
                        //}
                    }
                    else
                    {
                        releaseWall();
                       
                    }
                    break;
             }
        
        }

   


        private void _canvasDrawing_MouseMove(object sender, MouseEventArgs e)
        {
            switch (drawing_mode)
            {
                case DrawingMode.MOVE:
                    if (!(e.LeftButton == MouseButtonState.Pressed)) break;
                    
                    Point cur_p0 = e.GetPosition(_canvasDrawing);
                    Double move_x = scl_last_p.X - cur_p0.X;
                    Double move_y = scl_last_p.Y - cur_p0.Y;
                    scl_offset.X += move_x;
                    scl_offset.Y += move_y;

                    //Console.WriteLine("move({0:f0}, {1:f0})", move_x, move_y);


                    _sclDrawing.ScrollToHorizontalOffset(scl_offset.X);
                    _sclDrawing.ScrollToVerticalOffset(scl_offset.Y);
                    start_p = cur_p0;
                    //Console.WriteLine("({0:f0}, {1:f0})", _sclDrawing.ContentHorizontalOffset, _sclDrawing.ContentVerticalOffset);

                    break;
                case DrawingMode.DRAWING:
                    Point cur_p = makeSimplePoint(e.GetPosition(_canvasDrawing));
                    Double xd = Math.Abs(last_p.X - cur_p.X);
                    Double yd = Math.Abs(last_p.Y - cur_p.Y);
                    if ((start_p.X == 0) && (start_p.Y == 0))
                        return;
                   //if ((e.LeftButton == MouseButtonState.Pressed) &&
                    //    ((xd > 1) && (yd > 1) || ((xd > 5) || (yd > 5))))
                    if ((e.LeftButton == MouseButtonState.Pressed) && ((xd != 0) || (yd != 0)))
                    {
                        if ((xd > 5) || (yd > 5))
                        {
                            last_p = cur_p;
                            drawer2d.drawTempWall(start_p, cur_p, wall_brush, wall_thick, wall_alpha);
                        }
                    }
                    break;
                case DrawingMode.MODIFY:
                    if (!(e.LeftButton == MouseButtonState.Pressed)) break;
                    
                    if(modify_w== null) break;

                    Point cur_p2 = makeSimplePoint(e.GetPosition(_canvasDrawing));
                    Double xd2 = Math.Abs(last_p.X - cur_p2.X);
                    Double yd2 = Math.Abs(last_p.Y - cur_p2.Y);

                    //if ((xd2 == 0) || (yd2 == 0)) break;

                    if (modify_w.start_p == modify_w.select_p)
                        start_p = modify_w.end_p;
                    else if (modify_w.end_p == modify_w.select_p)
                        start_p = modify_w.start_p;
                    else
                        break;
                    
                    last_p = cur_p2;

                    drawer2d.drawTempWall(start_p, cur_p2, wall_brush, wall_thick, wall_alpha);
                    

                    break;
                case DrawingMode.DELETE:
                case DrawingMode.NONE:

                    break;
            }
#if false
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                Point cur_p = e.GetPosition(_canvasDrawing);
                Double move_x = scl_last_p.X - cur_p.X;
                Double move_y = scl_last_p.Y - cur_p.Y;
                scl_offset.X += move_x;
                scl_offset.Y += move_y;

                _sclDrawing.ScrollToHorizontalOffset(scl_offset.X);
                _sclDrawing.ScrollToVerticalOffset(scl_offset.Y);
                start_p = cur_p;

                Console.WriteLine("({0:f0}, {1:f0})", _sclDrawing.ContentHorizontalOffset, _sclDrawing.ContentVerticalOffset);

            } 
#endif
        }


        private void _canvasDrawing_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            switch (drawing_mode)
            {
                case DrawingMode.DRAWING:
                    Point cur_p = makeSimplePoint(e.GetPosition(_canvasDrawing));
                    //temp 라인의 삭제

                    drawer2d.removeTempWall();

                    makeAndAddWall(cur_p);
                    break;
                case DrawingMode.MODIFY:
                    if (modify_w == null) return;
                    Point cur_p2 = makeSimplePoint(e.GetPosition(_canvasDrawing));

                    drawer2d.removeTempWall();

                    //1. 기존 벽수정

                    WallDraw modify_w2 = drawDataMgr.ConvertCanvasWallToDBWall(canvas_size, modify_w);
                    modifyWall(modify_w2, cur_p2);
                    //drawer2d.modifyWall(modify_w);

                    //2. 기존 벽에 딸린 Corner제거

                    //3. Coner만들기



                    modify_w = null;
                    break;
                case DrawingMode.DELETE:
                case DrawingMode.NONE:

                    break;
            }
        }



        private void _canvasDrawing_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if(e.ChangedButton == MouseButton.Left)
            //{
            //    scl_last_p = e.GetPosition(_canvasDrawing);
            //}
        }

        private void _canvasDrawing_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Double w = _gridDrawing.ActualWidth;
            Point p = e.GetPosition(_sclDrawing);

            Console.WriteLine("{0}{1}",p.X, p.Y);
            if (e.Delta > 0)
            {
               
                if (w > 10240)
                    return;
                _canvasZoom(2,p);

            }
            else
            {
                if (w < 1100)
                    return;
                _canvasZoom(0.5,p);
            }
        }
        private void _canvasZoom(Double ratio, Point center_p)
        {
            releaseWall();
            Double w = _gridDrawing.ActualWidth;
            Double h = _gridDrawing.ActualHeight;

            Double scX = _sclDrawing.ContentHorizontalOffset;
            Double scY = _sclDrawing.ContentVerticalOffset;

            _gridDrawing.Width = w * ratio;
            _gridDrawing.Height = h * ratio;

            _canvasDrawing.Width = w * ratio;
            _canvasDrawing.Height = h *ratio;

            _imgDrawingBase.Width = w *  ratio;
            _imgDrawingBase.Height = h * ratio;


            wall_thick = wall_thick * ratio;
            wall_height = wall_height * ratio;
            canvas_size = new Size(w *ratio, h * ratio);
            reDrawGuideLine(new Point(0, 0), new Point(w * ratio, h * ratio));
            reDrawWall();

           // if (ratio==2)
#if false
            {
                _sclDrawing.ScrollToHorizontalOffset(_gridDrawing.Width / 2 - 512);
                _sclDrawing.ScrollToVerticalOffset(_gridDrawing.Height / 2 - 384);
            }
#else
            if (ratio > 1)
            {
                _sclDrawing.ScrollToHorizontalOffset(scX * ratio + 512);
                _sclDrawing.ScrollToVerticalOffset(scY * ratio + 384);
            }
            else
            {
                _sclDrawing.ScrollToHorizontalOffset((scX -512) * ratio);
                _sclDrawing.ScrollToVerticalOffset((scY -384) * ratio);
            }
#endif
        }

        private void _menuSelectOneLayerAllWall_Click(object sender, RoutedEventArgs e)
        {
            if (cur_layer > -1 && cur_layer < 4)
            {
                selectOneLayerAllWall();
            }
        }

        private void pickWallPointEvent(Object obj)
        {
            Console.WriteLine("pick_w===");
            if ((obj is WallDraw) == false) return;

            WallDraw w = (WallDraw)obj;

            if (drawing_mode == DrawingMode.DRAWING)
                pick_w = w;
            else if (drawing_mode == DrawingMode.MODIFY)
            {
                //modify_w = drawDataMgr.ConvertCanvasWallToDBWall(canvas_size,w);
                modify_w = w;
                drawer2d.hideModifyWall(w.id);
                drawer2d.drawTempWall(w.start_p, w.end_p, wall_brush, wall_thick, wall_alpha);
            }
        }

        private void connectWallPointEvent(Object obj)
        {
            Console.WriteLine("connect_w===");
            
            if(obj is int)
            {
                connect_w = null;
                return;
            }

            if ((obj is WallDraw) == false) return;

            WallDraw w = (WallDraw)obj;
            connect_w = w;
        }

        private void selectWallEvent(Object obj)
        {
            if (!(obj is WallDraw)) return;
            WallDraw w = (WallDraw)obj;

            switch(drawing_mode)
            {
                case DrawingMode.MODIFY:
                    //컨트롤키가 눌려있지 않은 경우는 선택된 벽을 모두 해제 한다
                    if (Keyboard.IsKeyDown(Key.LeftCtrl) == false)
                    {
                        foreach (var id in selected_wall_id_list)
                        {
                            drawer2d.releaseWall(id);
                        }
                        selected_wall_id_list.Clear();
                    }

                    drawer2d.selectWall(w.id);
                    selected_wall_id_list.Add(w.id);
                    break;
                case DrawingMode.WALLPROP_PICK:
                    _ctlColorSelector.changeColor(new SolidColorBrush(Color.FromArgb(w.colorA, w.colorR, w.colorG, w.colorB))); // 컬러 변경 

                    int ratio =(int) canvas_size.Width / 1024;
                    _ctlWallProp.setWallThiness(w.thickness/ratio);
                    _ctlWallProp.setWallHeight(w.height/ratio);
                    _ctlWallProp.setAlpha(w.alpha);

                    drawing_mode = DrawingMode.DRAWING;
                    _ctlDrawingEdit.Cursor = Cursors.Arrow;
                    _canvasDrawing.Cursor = Cursors.Pen;
                    break;
            }
        }

        #endregion

        #region Bottom Button Event (import, new, open, save)
        // 평면도 불러오기 
        private void _btnImportImage_Click(object sender, RoutedEventArgs e)
        {
            List<sp_list_image_Result> sp_image_drawing_list = new List<sp_list_image_Result>();

            foreach(var img in g.sp_image_list)
            {
                if(img.folder_name == "drawing")
                {
                    sp_image_drawing_list.Add(img);
                }
            }
            DrawingSelectImageWindow imgWindow = new DrawingSelectImageWindow(sp_image_drawing_list);
            imgWindow.ShowDialog();
            if(imgWindow.select_img_vm != null)
            {
                sp_img_vm select_img_vm = imgWindow.select_img_vm;
                ImageSource source = new BitmapImage(new Uri(select_img_vm.client_file_path));
                _imgDrawingBase.Source = source;
            }
        }
        // 새로운 도면 생성 
        private void _btnNew_Click(object sender, RoutedEventArgs e)
        {
            releaseWall();
            clearWallData();
        }
        // 로컬 파일 열기 
        private void _btnOpen_Click(object sender, RoutedEventArgs e)
        {
            releaseWall();
            clearWallData();
            openWawllData();
        }

        //private void _btnSave_Click(object sender, RoutedEventArgs e)
        //{
        //    releaseWall();
        //    _btnDraw.IsChecked = false;
        //    _btnModify.IsChecked = false;
        //    saveWallData();
        //}
        // 기존 리스트에서 선택 처리 
        private void _btnSelect_Click(object sender, RoutedEventArgs e)
        {
            releaseWall();
            clearWallData();
            selectDrawing();
        }
        // 저장 서버에 업로드 처리 
        private void _btnUpload_Click(object sender, RoutedEventArgs e)
        {
            releaseWall();
            _btnDraw.IsChecked = false;
            _btnModify.IsChecked = false;
            //saveWallData();
            String save_full_path = tmpSaveWallData();
            if (save_full_path != null)
                uploadData(save_full_path);
            //else // 취소로 빠지면 그냥 나가기 
            //    MessageBox.Show("UpdateFail");
        }
        #endregion

        #region 3D Grid Event

        private void _btn3DZoomIn_Click(object sender, RoutedEventArgs e)
        {
            //_viewPort3D.Width = 880;
            //_viewPort3D.Height = 800;
        }
        // 3D 시 화면 열리기 
        private void _grid3DView_MouseEnter(object sender, MouseEventArgs e)
        {
#if false
            _grid3DView.Width = 1024;
            _grid3DView.Height = 768;
            Thickness margin = new Thickness(0, 10, 10, 0);
            _grid3DView.Margin = margin;
#else
            SimpleAnimation anim = new SimpleAnimation();
            Point ct_p = new Point(_grid3DView.ActualWidth, _grid3DView.ActualHeight);
            //Point ct_p = new Point(0, 0);
            Vector from_v = new Vector(0.25, 0.25);
            Vector to_v = new Vector(1, 1);
            anim.gridScaleAnimation(_grid3DView, from_v, to_v, ct_p, 0.8, 500);

#endif
        }
        // 축소 
        private void _grid3DView_MouseLeave(object sender, MouseEventArgs e)
        {
#if false
            _grid3DView.Width = 220;
            _grid3DView.Height = 180;
            Thickness margin = new Thickness(0, 86, 10, 0);
            _grid3DView.Margin = margin;
#else
            SimpleAnimation anim = new SimpleAnimation();
            Point ct_p = new Point(_grid3DView.ActualWidth, _grid3DView.ActualHeight);
            //Point ct_p = new Point(0, 0); 
            Vector from_v = new Vector(1, 1);
            Vector to_v = new Vector(0.25, 0.25);
            anim.gridScaleAnimation(_grid3DView, from_v, to_v, ct_p, 0.8, 500);

#endif
        }
        
        #endregion

        #region Point Caculation Method // 라인 그리기 

        private Point makeSimplePoint(Point _point)
        {
            
            Point cur_p;
            if (Keyboard.IsKeyDown(Key.LeftShift))
                cur_p = toVerySimplePoint(_point);
            else if (Keyboard.IsKeyDown(Key.LeftAlt))
                cur_p = toSimplePoint(_point);
            else
                cur_p = toMiddleSimplePoint(_point);

            return cur_p;
        }


        //2D Map Select Point to Simple
        private Point toSimplePoint(Point _point)
        {
            Point _resultPoint = new Point();

            int x = (int)_point.X;
            int y = (int)_point.Y;
            _resultPoint.X = (Double)x;
            _resultPoint.Y = (Double)y;

            //Console.WriteLine("result({0},{1})", _resultPoint.X, _resultPoint.Y);
            return _resultPoint;
        }


        private Point toMiddleSimplePoint(Point _point)
        {
            Point _resultPoint = new Point();
            double _decimalX = _point.X % 10;
            double _decimalY = _point.Y % 10;
            if (_decimalX > 5)
                _resultPoint.X = _point.X - _decimalX + 10;
            else
                _resultPoint.X = _point.X - _decimalX;

            if (_decimalY > 5)
                _resultPoint.Y = _point.Y - _decimalY + 10;
            else
                _resultPoint.Y = _point.Y - _decimalY;

            //Console.WriteLine("input({0},{1} => output({2},{3})", _point.X, _point.Y, _resultPoint.X, _resultPoint.Y);
            return _resultPoint;
        }

        private Point toVerySimplePoint(Point _point)
        {
            Point _resultPoint = new Point();
            double _decimalX = _point.X % 50;
            double _decimalY = _point.Y % 50;
            if (_decimalX > 25)
                _resultPoint.X = _point.X - _decimalX + 50;
            else
                _resultPoint.X = _point.X - _decimalX;

            if (_decimalY > 25)
                _resultPoint.Y = _point.Y - _decimalY + 50;
            else
                _resultPoint.Y = _point.Y - _decimalY;

            //Console.WriteLine("input({0},{1} => output({2},{3})", _point.X, _point.Y, _resultPoint.X, _resultPoint.Y);
            return _resultPoint;
        }
        
        #endregion

        #region Command Excuted & CanExcuted
        private void _btnShowLayer1_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (cur_layer == 0)
            {
                _btnShowLayer1.IsChecked = true;
                layer1_bt_canexcute = false;
                layer2_bt_canexcute = true;
                layer3_bt_canexcute = true;
                layer4_bt_canexcute = true;
            }
        }

        private void _btnShowLayer2_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (cur_layer == 1)
            {
                _btnShowLayer2.IsChecked = true;
                layer1_bt_canexcute = true;
                layer2_bt_canexcute = false;
                layer3_bt_canexcute = true;
                layer4_bt_canexcute = true;
            }
        }

        private void _btnShowLayer3_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (cur_layer == 2)
            {
                _btnShowLayer3.IsChecked = true;
                layer1_bt_canexcute = true;
                layer2_bt_canexcute = true;
                layer3_bt_canexcute = false;
                layer4_bt_canexcute = true;
            }
        }

        private void _btnShowLayer4_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (cur_layer == 3)
            {
                _btnShowLayer4.IsChecked = true;
                layer1_bt_canexcute = true;
                layer2_bt_canexcute = true;
                layer3_bt_canexcute = true;
                layer4_bt_canexcute = false;
            }

        }

        private void _btnShowLayer1_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = layer1_bt_canexcute;
        }
        private void _btnShowLayer2_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = layer2_bt_canexcute;
        }
        private void _btnShowLayer3_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = layer3_bt_canexcute;
        }
        private void _btnShowLayer4_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = layer4_bt_canexcute;
        }

        private void _menuSelectOneLayerAllWall_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = selectall_menu_canexcute;
        }



        private void _btnMove_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = move_bt;
        }

        private void _btnDraw_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = draw_bt;
        }

        private void _btnModify_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = modify_bt;
        }

        private void _btnMove_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void _btnDraw_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void _btnModify_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_btnModify.IsChecked == true)
                selectall_menu_canexcute = true;
            else
                selectall_menu_canexcute = false;
        }

        private void _selectDrawLayer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            move_bt = true;
            modify_bt = true;
            draw_bt = true;
        }
        #endregion

        #region Wall add, remove, modify Method // 벽과 컬러 처리 

        private void removeLastDrawWall()
        {
            WallDraw w_vm = wall_vm_list[cur_layer].Find(at => at.id == last_draw_wall_num);
            if (w_vm != null)
            {

                wall_vm_list[cur_layer].Remove(w_vm);
                drawer2d.removeWallByNumber(w_vm.id);
                _ctlDrawing3D.removeWallByNum(w_vm.id);


                int count = wall_vm_list[cur_layer].Count - 1;
                if (wall_vm_list[cur_layer].Count > 0)
                    last_draw_wall_num = wall_vm_list[cur_layer][count].id;
                else
                    last_draw_wall_num = -1;

                removeWallCornerByWallId(w_vm.id,cur_layer, true);
            }
        }

#if false
        private void reDrawWallOnly2D()
        {
            drawer2d.removeAllWall();
            //_ctlDrawing3D
            foreach (var w_vm in wall_vm_list[cur_layer])
            {
                WallDraw cw = drawDataMgr.makeCanvasWallDataFromDBWall(canvas_size, w_vm);
                drawer2d.addWallbyWallVm(cw);
            }
        }
#endif

        private void reDrawWall()
        {
            drawer2d.removeAllWall();
            _ctlDrawing3D.removeAllWall();


            for (int i = 0; i < 4; i++)
            {
                foreach (var w_vm in wall_vm_list[i])
                {
                    WallDraw cw = drawDataMgr.ConvertDBWallDataToCanvasWall(canvas_size, w_vm);
                    drawer2d.addWallbyWallVm(cw);
                    _ctlDrawing3D.addWall(w_vm);
                }
                foreach (var wcd in wallc_list[i])
                {
                    WallCornerDraw cwcd = drawDataMgr.convertWallCornerDBtoCanvas(canvas_size, wcd);
                    drawer2d.addWallCorner(cwcd);
                    _ctlDrawing3D.addWallCorner(wcd);
                }
            }
        }

        private void reDrawWall2D()//사용안함
        {
            drawer2d.removeAllWall();
            _ctlDrawing3D.removeAllWall();


            for (int i = 0; i < 4; i++)
            {
                foreach (var w_vm in wall_vm_list[i])
                {
                    WallDraw cw = drawDataMgr.ConvertDBWallDataToCanvasWall(canvas_size, w_vm);
                    drawer2d.addWallbyWallVm(cw);
                    _ctlDrawing3D.addWall(w_vm);
                }
                foreach (var wcd in wallc_list[i])
                {
                    WallCornerDraw cwcd = drawDataMgr.convertWallCornerDBtoCanvas(canvas_size, wcd);
                    drawer2d.addWallCorner(cwcd);
                    _ctlDrawing3D.addWallCorner(wcd);
                }
            }
        }


        private void showOneLayerWall(int layer)
        {
            foreach (var w_vm in wall_vm_list[layer])
            {
                WallDraw cw = drawDataMgr.ConvertDBWallDataToCanvasWall(canvas_size, w_vm);
                drawer2d.addWallbyWallVm(cw);
                _ctlDrawing3D.addWall(w_vm);
            }
            foreach (var wcd in wallc_list[layer])
            {
                WallCornerDraw cwcd = drawDataMgr.convertWallCornerDBtoCanvas(canvas_size, wcd);
                drawer2d.addWallCorner(cwcd);
                _ctlDrawing3D.addWallCorner(wcd);
            }
        }

        private void hideOneLayerWall(int layer)
        {
            foreach (var w_vm in wall_vm_list[layer])
            {
                drawer2d.removeWallByNumber(w_vm.id);
                _ctlDrawing3D.removeWallByNum(w_vm.id);
                last_draw_wall_num = -1;

                //removeWallCornerByWallId(w_vm.id,layer, false);
            }

            foreach(var wc in wallc_list[layer])
            {
                drawer2d.removeWallCorner(wc);
                _ctlDrawing3D.removeWallCorner(wc.id);
            }

        }

        private void removeOneLayerWall(int layer)
        {
            foreach (var w_vm in wall_vm_list[layer])
            {
                drawer2d.removeWallByNumber(w_vm.id);
                _ctlDrawing3D.removeWallByNum(w_vm.id);
                last_draw_wall_num = -1;
//                removeWallCornerByWallId(w_vm.id,layer, true);
            }
            wall_vm_list[layer].Clear();

            foreach (var wc in wallc_list[layer])
            {
                drawer2d.removeWallCorner(wc);
                _ctlDrawing3D.removeWallCorner(wc.id);
            }
            wallc_list[layer].Clear();
        }


        private void selectOneLayerAllWall()
        {
            foreach (var w_vm in wall_vm_list[cur_layer])
            {
                selected_wall_id_list.Add(w_vm.id);
                selectWallEffect(w_vm);
            }
        }

        private void selectWallEffect(WallDraw w_vm)
        {
            drawer2d.selectWall(w_vm.id);
            //_ctlDrawing3D.selectWall(w_vm);
        }

        private void AddSelectWall()
        {

        }

        private void releaseWall()
        {
            foreach (var n in selected_wall_id_list)
            {
                drawer2d.releaseWall(n);
                //_ctlDrawing3D.releaseWall(wall_vm_list[cur_layer].Find(at => at.id == n));
            }
            selected_wall_id_list.Clear();
        }

        private List<WallCornerDraw> getWallCornerByWallId(int wall_id,int layer)
        {
            //List<WallCornerDraw> wc_list = wallc_list[layer].FindAll(at => at.w1_id == wall_id);
            //List<WallCornerDraw> wc_list2 = wallc_list[layer].FindAll(at => at.w2_id == wall_id);
            //wc_list.AddRange(wc_list2);

            List<WallCornerDraw> tmp_wc_list = wallc_list[layer].FindAll(at => (at.w1_id == wall_id) || (at.w2_id == wall_id));

            return tmp_wc_list;
        }

        private void removeWallCornerByWallId(int wall_id,int layer, Boolean remove_data)
        {
            List<WallCornerDraw> wc_list = getWallCornerByWallId(wall_id, layer);

            if (wc_list.Count != 0)
            {
                foreach (var wc in wc_list)
                {
                    drawer2d.removeWallCorner(wc);
                    _ctlDrawing3D.removeWallCorner(wc.id);
                    if (remove_data)
                    {
                        if (wallc_list[layer].Exists(at => at.id == wc.id))
                            wallc_list[layer].Remove(wc);
                    }
                }
            }
        }


        private void removeSelectWall()
        {
            foreach (var id in selected_wall_id_list)
            {
                drawer2d.removeWallByNumber(id);
                _ctlDrawing3D.removeWallByNum(id);
              
                WallDraw w = wall_vm_list[cur_layer].Find(at => at.id == id);
                wall_vm_list[cur_layer].Remove(w);

                removeWallCornerByWallId(id,cur_layer, true);
               
            }
            selected_wall_id_list.Clear();
        }

        private void modifyWall(WallDraw w, Point end_p)
        {
            //벽에 연결되 있던 Corner들을 먼저 삭제한다
            //List<WallCornerDraw> wc_list = wallc_list[w.layer].FindAll(at => (at.w1_id == w.id)||(at.w2_id ==w.id));
            //foreach(var wc in wc_list)
            //{
            //    drawer2d.removeWallCorner(wc);
            //    wallc_list[w.layer].Remove(wc);
            //}

            removeWallCornerByWallId(w.id, w.layer, true);

            SolidColorBrush temp_br = new SolidColorBrush(Color.FromArgb(w.colorA, w.colorR, w.colorG, w.colorB));

            Double thickness = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, w.thickness);
            Double height = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, w.height);
            WallDraw new_w = drawDataMgr.makeDBWallData(w.layer, w.id, canvas_size, start_p, end_p, temp_br,
                                                  thickness,
                                                  height, 
                                                  w.alpha);
            //벽 리스트에 저장된 정보를 수정한다
            WallDraw w_in_list = wall_vm_list[w.layer].Find(at => at.id == w.id);
            if (w_in_list == null) return;
            w_in_list.start_p = new_w.start_p;
            w_in_list.start_pA = new_w.start_pA;
            w_in_list.start_pB = new_w.start_pB;
            w_in_list.end_p = new_w.end_p;
            w_in_list.end_pA = new_w.end_pA;
            w_in_list.end_pB = new_w.end_pB;


            Point pick_point = new Point();
            pick_point.X = drawDataMgr.getVMValue_FromCanvasValue(canvas_size, start_p.X);
            pick_point.Y = drawDataMgr.getVMValue_FromCanvasValue(canvas_size, start_p.Y);
            
            //pick_w 의 위치가 될 벽을 찾아서 나중에 처리한다
            WallDraw temp_pick_w = wall_vm_list[w.layer].Find(at =>
                ((at.start_p == pick_point) || (at.end_p == pick_point)) &&(at.id!=w.id) );
            if(temp_pick_w!=null)
                pick_w = drawDataMgr.ConvertDBWallDataToCanvasWall(canvas_size, temp_pick_w);



            WallDraw w_c = drawDataMgr.ConvertDBWallDataToCanvasWall(canvas_size, new_w);
            
            //벽을 수정한다
            drawer2d.modifyWall(w_c);
            _ctlDrawing3D.modifyWall(new_w);
            //선택된 점에서 벽을 연결한 경우
            if (pick_w != null)
            {
                //벽사이 연결 데이터 만들기
                WallCornerDraw wcd_c = drawDataMgr.makeWallCornerDraw(w_c, pick_w, wallc_static_num);
                if (wcd_c != null)
                {
                    WallCornerDraw wcd = drawDataMgr.convertWallCornerCanvasToDB(canvas_size, wcd_c);
                    wallc_static_num++;
                    wallc_list[cur_layer].Add(wcd);

                    drawer2d.addWallCorner(wcd_c);
                    _ctlDrawing3D.addWallCorner(wcd);
                    //현제 선택된 wall 정보 해제
                }
                pick_w = null;
            }

            //기존의 벽에 연결한 경우
            if (connect_w != null)
            {
                //벽사이 연결 데이터 만들기
                WallCornerDraw wcd_c = drawDataMgr.makeWallCornerDraw(w_c, connect_w, wallc_static_num);
                if (wcd_c != null)
                {
                    WallCornerDraw wcd = drawDataMgr.convertWallCornerCanvasToDB(canvas_size, wcd_c);
                    wallc_static_num++;
                    wallc_list[cur_layer].Add(wcd);

                    drawer2d.addWallCorner(wcd_c);
                    _ctlDrawing3D.addWallCorner(wcd);
                    //현제 선택된 wall 정보 해제
                }
                connect_w = null;
            }

        }

        // 2,3 에서 그리고 디비 저장 3가ㅣ지 형태의 데이터 관리 
        private void makeAndAddWall(Point end_p)
        {
            if ((start_p.X == 0) && (start_p.Y == 0))
                return;
            //기본데이터 만들기
            //WallDraw w = drawDataMgr.makeWallVmData(cur_layer, wall_static_id, canvas_size, start_p, end_p, wall_brush,
            //                                      wall_thick, wall_height, wall_alpha);
            WallDraw w = drawDataMgr.makeDBWallData(cur_layer, wall_static_id, canvas_size, start_p, end_p, wall_brush,
                                                  wall_thick, wall_height, wall_alpha);

            
            wall_vm_list[cur_layer].Add(w);
            last_draw_wall_num = wall_static_id;
            wall_static_id++;
            WallDraw w_c = drawDataMgr.ConvertDBWallDataToCanvasWall(canvas_size, w);
            //drawer2d.addWallbyWallVm(w_c, pick_w);
            drawer2d.addWallbyWallVm(w_c);
            _ctlDrawing3D.addWall(w);

            //선택된 점에서 벽을 연결한 경우
            if (pick_w != null)
            {
                //벽사이 연결 데이터 만들기
                WallCornerDraw wcd_c = drawDataMgr.makeWallCornerDraw(w_c, pick_w, wallc_static_num);
                if (wcd_c != null)
                {
                    WallCornerDraw wcd = drawDataMgr.convertWallCornerCanvasToDB(canvas_size, wcd_c);
                    wallc_static_num++;
                    wallc_list[cur_layer].Add(wcd);

                    drawer2d.addWallCorner(wcd_c);
                    _ctlDrawing3D.addWallCorner(wcd);
                    //현제 선택된 wall 정보 해제
                }
                pick_w = null;
            }
            
            //기존의 벽에 연결한 경우
            if(connect_w != null)
            {
                //벽사이 연결 데이터 만들기
                WallCornerDraw wcd_c = drawDataMgr.makeWallCornerDraw(w_c, connect_w, wallc_static_num);
                if (wcd_c != null)
                {
                    WallCornerDraw wcd = drawDataMgr.convertWallCornerCanvasToDB(canvas_size, wcd_c);
                    wallc_static_num++;
                    wallc_list[cur_layer].Add(wcd);

                    drawer2d.addWallCorner(wcd_c);
                    _ctlDrawing3D.addWallCorner(wcd);
                    //현제 선택된 wall 정보 해제
                }
                connect_w = null;
            }

            start_p = new Point(0,0);
            
            //_sp.Margin = new Thickness(w_c.start_p.X - 5, w_c.start_p.Y - 5, 0, 0);
            //_ep.Margin = new Thickness(w_c.end_p.X - 5, w_c.end_p.Y - 5, 0, 0);

            //_spA.Margin = new Thickness(w_c.start_pA.X - 5, w_c.start_pA.Y - 5, 0, 0);
            //_spB.Margin = new Thickness(w_c.start_pB.X - 5, w_c.start_pB.Y - 5, 0, 0);
            //_epA.Margin = new Thickness(w_c.end_pA.X - 5, w_c.end_pA.Y - 5, 0, 0);
            //_epB.Margin = new Thickness(w_c.end_pB.X - 5, w_c.end_pB.Y - 5, 0, 0);
        }

        // 컬러 속성 바뀌면 처리 -> 색상 변경 이벤트 
        private void ColorSelector_colorChangedEvent(SolidColorBrush brush)
        {
            wall_brush = brush;
            //_ctlWallProp.setWallBrush(brush);
            drawer2d.wall_brush = brush;

            foreach (var num in selected_wall_id_list)
            {
                WallDraw w_vm = wall_vm_list[cur_layer].Find(w => w.id == num);
                if (w_vm != null)
                {
                    w_vm.colorA = brush.Color.A;
                    w_vm.colorR = brush.Color.R;
                    w_vm.colorG = brush.Color.G;
                    w_vm.colorB = brush.Color.B;
                    drawer2d.changeWallColor(num, brush);
                    _ctlDrawing3D.modifyWall(w_vm);
                    //연결된 코너의 색상도 변경한다
                    List<WallCornerDraw> wc_list = getWallCornerByWallId(w_vm.id, cur_layer);
                    foreach(var wc in wc_list)
                    {
                        wc.colorA = brush.Color.A;
                        wc.colorR = brush.Color.R;
                        wc.colorG = brush.Color.G;
                        wc.colorB = brush.Color.B;
                        _ctlDrawing3D.modifyWallCorner(wc);
                    }
                }

            }

        }
        // 피커 이벤트 처리 
        private void ColorSelector_colorPickEvent(Object obj)
        {
            if (drawing_mode != DrawingMode.DRAWING) return;

            drawing_mode = DrawingMode.WALLPROP_PICK;
            _ctlDrawingEdit.Cursor = Cursors.UpArrow;
            _canvasDrawing.Cursor = Cursors.UpArrow;
            
        }
        // 벽속성 바뀌면 처리 , 컨트롤에서 이벤트 발생 
        private void WallPropertyControl_wallPropChangedEvent(string type, object value)
        {
            Double ratio = drawDataMgr.getRatio_byDefault(canvas_size);

            switch (type)
            {
                case "THICKNESS":
                    wall_thick = (Double)value * ratio;
                    List<WallCornerDraw> tmp_wc_list = new List<WallCornerDraw>();

                    foreach (var num in selected_wall_id_list)
                    {
                        //기존의 벽
                        WallDraw old_w = wall_vm_list[cur_layer].Find(w => w.id == num);

                        if (old_w != null)
                        {
                            //새로 교체될 벽
                            WallDraw old_w_c = drawDataMgr.ConvertDBWallDataToCanvasWall(canvas_size,old_w);

                            WallDraw new_w = drawDataMgr.makeDBWallData(cur_layer, old_w_c.id, canvas_size, old_w_c.start_p, old_w_c.end_p,
                                                 new SolidColorBrush(Color.FromArgb(old_w_c.colorA, old_w_c.colorR, old_w_c.colorG, old_w_c.colorB)),
                                                 wall_thick,
                                                 drawDataMgr.getCanvasValue_FromVMValue(canvas_size, old_w.height),
                                                 old_w_c.alpha);

                            //old_w.thickness = drawDataMgr.getVMValue_FromCanvasValue(canvas_size, wall_thick);
                            drawer2d.changeWallThick(num, wall_thick);
                            _ctlDrawing3D.modifyWall(new_w);

                            old_w.start_p = new_w.start_p;
                            old_w.end_p = new_w.end_p;
                            old_w.thickness = new_w.thickness;
                            old_w.start_pA = new_w.start_pA;
                            old_w.start_pB = new_w.start_pB;
                            old_w.end_pA = new_w.end_pA;
                            old_w.end_pB = new_w.end_pB;

                            //연결된 코너에 대한 정보도 수정이 필요하다?
                            //아무튼 수정되는 벽들에 연결된 모든 코너를 모은다
                            List<WallCornerDraw> tmp_wc_l = getWallCornerByWallId(new_w.id, new_w.layer);
                            if (tmp_wc_l != null)
                            {
                                foreach (var tmp_wc in tmp_wc_l)
                                {
                                    if (!tmp_wc_list.Contains((WallCornerDraw)tmp_wc))
                                        tmp_wc_list.Add(tmp_wc);
                                }
                            }
                        }
                    }


                    foreach (var tmp_wc in tmp_wc_list)
                    {
                        int id = -1;
                        //각 뷰에서 기존 wallConer삭제
                        drawer2d.removeWallCorner(tmp_wc);
                        _ctlDrawing3D.removeWallCorner(tmp_wc.id);

                        id = tmp_wc.id;
                        wallc_list[cur_layer].Remove(tmp_wc);

                        //연결된 벽들에 대한 정보를 받아온다
                        WallDraw w1_con_w = wall_vm_list[cur_layer].Find(at => at.id == tmp_wc.w1_id);
                        WallDraw w2_con_w = wall_vm_list[cur_layer].Find(at => at.id == tmp_wc.w2_id);
                        if ((w1_con_w != null) || (w2_con_w != null))
                        {
                            //Canvas 에 맞게 변경
                            WallDraw w1_con_w_c = drawDataMgr.ConvertDBWallDataToCanvasWall(canvas_size, w1_con_w);
                            WallDraw w2_con_w_c = drawDataMgr.ConvertDBWallDataToCanvasWall(canvas_size, w2_con_w);

                            //WallCorner를 계산한다
                            WallCornerDraw wcd_c = drawDataMgr.makeWallCornerDraw(w1_con_w_c, w2_con_w_c, id);
                            if(wcd_c!=null)
                            {
                                WallCornerDraw wcd = drawDataMgr.convertWallCornerCanvasToDB(canvas_size, wcd_c);
                                wallc_static_num++;
                                wallc_list[cur_layer].Add(wcd);

                                drawer2d.addWallCorner(wcd_c);
                                _ctlDrawing3D.addWallCorner(wcd);

                            }
                        }
                    }

#if false
                    //start_p와 end_p 에 연결된 각점을 찾아서
                    WallDraw st_con_w = wall_vm_list[w_vm.layer].Find(at => (at.id != w_vm.id) &&
                                                                    ((at.start_p == w_vm.start_p) || (at.start_p == w_vm.end_p)));
                    WallDraw ed_con_w = wall_vm_list[w_vm.layer].Find(at => (at.id != w_vm.id) &&
                                                                    ((at.end_p == w_vm.end_p) || (at.end_p == w_vm.start_p)));

                    //getWallCornerByWallId();

                    WallDraw w_c = drawDataMgr.ConvertDBWallDataToCanvasWall(canvas_size, w_vm);
                    //선택된 점에서 벽을 연결한 경우
                    if (st_con_w != null)
                    {
                        WallDraw st_con_w_c = drawDataMgr.ConvertDBWallDataToCanvasWall(canvas_size, st_con_w);

                        //벽사이 연결 데이터 만들기
                        WallCornerDraw wcd_c = drawDataMgr.makeWallCornerDraw(w_c, st_con_w_c, wallc_static_num);
                        if (wcd_c != null)
                        {
                            WallCornerDraw wcd = drawDataMgr.convertWallCornerCanvasToDB(canvas_size, wcd_c);
                            wallc_static_num++;
                            wallc_list[cur_layer].Add(wcd);

                            drawer2d.addWallCorner(wcd_c);
                            _ctlDrawing3D.addWallCorner(wcd);
                            //현제 선택된 wall 정보 해제
                        }
                    }

                    //기존의 벽에 연결한 경우
                    if (ed_con_w != null)
                    {
                        WallDraw ed_con_w_c = drawDataMgr.ConvertDBWallDataToCanvasWall(canvas_size, ed_con_w);

                        //벽사이 연결 데이터 만들기
                        WallCornerDraw wcd_c = drawDataMgr.makeWallCornerDraw(w_c, ed_con_w_c, wallc_static_num);
                        if (wcd_c != null)
                        {
                            WallCornerDraw wcd = drawDataMgr.convertWallCornerCanvasToDB(canvas_size, wcd_c);
                            wallc_static_num++;
                            wallc_list[cur_layer].Add(wcd);

                            drawer2d.addWallCorner(wcd_c);
                            _ctlDrawing3D.addWallCorner(wcd);
                            //현제 선택된 wall 정보 해제
                        }

                    }
#endif
                    break;
                case "ALPHA":
                    wall_alpha = (Double)value;

                    foreach (var num in selected_wall_id_list)
                    {
                        WallDraw w_vm = wall_vm_list[cur_layer].Find(w => w.id == num);
                        if (w_vm != null)
                        {
                            w_vm.alpha = wall_alpha;
                            drawer2d.changeWallAlpha(num, wall_alpha);
                            //_ctlDrawing3D.modifyWall(w_vm);
                            _ctlDrawing3D.removeWallByNum(num);
                            _ctlDrawing3D.addWall(w_vm);

                            //연결된 코너를 찾아서 Alpha 변경
                            List<WallCornerDraw> tmp_wc_l = getWallCornerByWallId(w_vm.id, w_vm.layer);
                            foreach(var wc in tmp_wc_l)
                            {
                                wc.alpha = wall_alpha;
                                _ctlDrawing3D.removeWallCorner(wc.id);
                                _ctlDrawing3D.addWallCorner(wc);
                                //_ctlDrawing3D.modifyWallCorner(wc);

                            }
                        }
                    }
                    break;
                case "HEIGHT":
                    wall_height = (Double)value * ratio;
                    foreach (var num in selected_wall_id_list)
                    {
                        WallDraw w_vm = wall_vm_list[cur_layer].Find(w => w.id == num);
                        if (w_vm != null)
                        {
                            w_vm.height = drawDataMgr.getVMValue_FromCanvasValue(canvas_size,wall_height);
                            _ctlDrawing3D.modifyWall(w_vm);

                            //연결된 코너를 찾아서 높이 변경
                            List<WallCornerDraw> tmp_wc_l = getWallCornerByWallId(w_vm.id, w_vm.layer);
                            foreach (var wc in tmp_wc_l)
                            {
                                wc.height = drawDataMgr.getVMValue_FromCanvasValue(canvas_size, wall_height);
                                _ctlDrawing3D.modifyWallCorner(wc);
                            }
                        }
                    }
                    break;
            }
        }
        #endregion

        #region other method
        private int findNumberFromString(string str)
        {
            string number_str = Regex.Replace(str, @"\D", "");
            return int.Parse(number_str);
        }


        private void clearWallData()
        {
            last_draw_wall_num = -1;
            wall_static_id = 0;
            for (int i = 0; i < 4; i++)
            {
                removeOneLayerWall(i);
            }
        }

        private void openWallDataFromFile(String path)
        {
            if(File.Exists(path))
            {
                try
                {
                    BinaryFormatter openformat = new BinaryFormatter();
                    FileStream openStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                    List<WallDraw>[] open_w_list = new List<WallDraw>[4];

                    open_w_list = (List<WallDraw>[])openformat.Deserialize(openStream);
 
                    
                    wall_static_id = 0;

                    for (int i = 0; i < 4; i++)
                    {
                        wall_vm_list[i].Clear();
                        foreach (var w in open_w_list[i])
                        {
                            WallDraw w_vm = new WallDraw()
                            {
                                layer = w.layer,
                                start_p = w.start_p,
                                start_pA = w.start_pA,
                                start_pB = w.start_pB,
                                end_p = w.end_p,
                                end_pA = w.end_pA,
                                end_pB = w.end_pB,
                                thickness = w.thickness,
                                height = w.height,
                                alpha = w.alpha,
                                colorA = w.colorA,
                                colorR = w.colorR,
                                colorG = w.colorG,
                                colorB = w.colorB,
                                id = w.id
                                
                            };
                            wall_vm_list[i].Add(w_vm);
                            if (wall_static_id <= w.id)
                                wall_static_id = w.id + 1;
                            //wall_static_id++;
                        }
                    }

#if true
                    try
                    {
                        List<WallCornerDraw>[] open_wc_list = (List<WallCornerDraw>[])openformat.Deserialize(openStream);
                        wallc_static_num = 0;
                            
                        for (int i = 0; i < 4; i++)
                        {
                            wallc_list[i].Clear();
                            foreach (var wc in open_wc_list[i])
                            {
                                WallCornerDraw wcd = new WallCornerDraw()
                                {
                                    layer = wc.layer,
                                    id = wc.id,

                                    alpha = wc.alpha,
                                    angle = wc.angle,
                                    colorA = wc.colorA,
                                    colorR = wc.colorR,
                                    colorG = wc.colorG,
                                    colorB = wc.colorB,


                                    height = wc.height,
                                    Z = wc.Z,
                                    w1_id = wc.w1_id,
                                    w2_id = wc.w2_id
                                };

                                foreach (var p in wc.p_list)
                                {
                                    Point tmp_p = p;
                                    wcd.p_list.Add(tmp_p);
                                }
                                wallc_list[i].Add(wcd);
                                
                                if(wallc_static_num <= wc.id)
                                    wallc_static_num = wc.id +1;
                            }
                        }
                    }
                    catch (Exception ex) { }
                    openStream.Close();
#endif           

                    if (_btnShowLayer1.IsChecked == true)
                        showOneLayerWall(0);
                    if (_btnShowLayer2.IsChecked == true)
                        showOneLayerWall(1);
                    if (_btnShowLayer3.IsChecked == true)
                        showOneLayerWall(2);
                    if (_btnShowLayer4.IsChecked == true)
                        showOneLayerWall(3);
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0}", e.Message);
                    MessageBox.Show("Please check image file name. (openWallDataFromFile)");
                }
            }
        }

        private void openWawllData()
        {
            OpenFileDialog openDrawingDialog = new OpenFileDialog
            {
                //Filter = "Image Files|*.3d"
                Filter = "I2MS2 3DFiles|*.3d"
            };
            openDrawingDialog.ShowDialog();

            Console.WriteLine("load filename = {0}", openDrawingDialog.FileName);
            string path = openDrawingDialog.FileName;

            openWallDataFromFile(path);
        }

#if false
        private void saveWallData()
        {
            SaveFileDialog saveDrawingDialog = new SaveFileDialog();
            saveDrawingDialog.ShowDialog();

            List<WallDraw>[] save_w_list = new List<WallDraw>[4];
            for (int i = 0; i < 4; i++)
            {
                save_w_list[i] = new List<WallDraw>();
                //ObservableCollection<Wall> save_w_list = new ObservableCollection<Wall>();
                foreach (var v in wall_vm_list[i])
                {
                    WallDraw w = new WallDraw()
                    {
                        layer = v.layer,
                        start_p = v.start_p,
                        end_p = v.end_p,
                        thickness = v.thickness,
                        height = v.height,
                        alpha = v.alpha,
                        colorA = v.colorA,
                        colorR = v.colorR,
                        colorG = v.colorG,
                        colorB = v.colorB
                    };
                    save_w_list[i].Add(w);
                }
            }


            Console.WriteLine("save filename = {0}.3d", saveDrawingDialog.FileName);

            string savePath = string.Format("{0}.3d", saveDrawingDialog.FileName);

            BinaryFormatter saveformat = new BinaryFormatter();

            try
            {
                FileStream saveStream = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                saveformat.Serialize(saveStream, save_w_list);
                saveformat.Serialize(saveStream, wallc_list);
                saveStream.Close();

                save_path = savePath;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}", e.Message);
                MessageBox.Show(e.Message);
            }

        } 
#endif
        // 3D 파일 저장 로직 
        private String tmpSaveWallData()
        {
            String saveFolder = System.IO.Directory.GetCurrentDirectory();
            String filename;
         
            DrawingNamming namming = new DrawingNamming();
            namming.ShowDialog();
            if (namming.DialogResult == true)
            {
                filename = namming.name;
                if (filename == string.Empty)
                    return null;
            }
            else
                return null;

#if false
            List<WallDraw>[] save_w_list = new List<WallDraw>[4];
            for (int i = 0; i < 4; i++)
            {
                save_w_list[i] = new List<WallDraw>();
                //ObservableCollection<Wall> save_w_list = new ObservableCollection<Wall>();
                foreach (var v in wall_vm_list[i])
                {
                    WallDraw w = new WallDraw()
                    {
                        layer = v.layer,
                        start_p = v.start_p,
                        start_pA = v.start_pA,
                        start_pB = v.start_pB,
                        end_p = v.end_p,
                        end_pA = v.end_pA,
                        end_pB = v.end_pB,
                        thickness = v.thickness,
                        height = v.height,
                        alpha = v.alpha,
                        colorA = v.colorA,
                        colorR = v.colorR,
                        colorG = v.colorG,
                        colorB = v.colorB
                    };
                    save_w_list[i].Add(w);
                }
            } 
#endif
            string savePath;

            savePath = string.Format("{0}/{1}.3d", saveFolder, filename);

            if (!(File.Exists(savePath)))
            {
                BinaryFormatter saveformat = new BinaryFormatter();

                try
                {
                    FileStream saveStream = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
#if false
                    saveformat.Serialize(saveStream, save_w_list);
#else
                    //saveformat.Serialize(saveStream, save_w_list);
                    saveformat.Serialize(saveStream, wall_vm_list);
                    saveformat.Serialize(saveStream, wallc_list);
#endif
                    saveStream.Close();

                    save_path = savePath;
                    return savePath;
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0}", e.Message);
                    MessageBox.Show(e.Message);
                    return null;
                }
            }   

            return null;

        }


        private async void uploadData(String save_full_path)
        {
            String src_file_name = System.IO.Path.GetFileName(save_full_path);
            //서버에 3d 파일을 추가한다
            var t1 = addDrawingFileToServer(save_full_path, src_file_name);
            string server_file_name = await t1;

            if (server_file_name != null)
            {
                //3d정보를 DB에 추가한다
                drawing_3d src_dr3d = new drawing_3d()
                {
                    drawing_3d_name = src_file_name,
                    file_name = server_file_name,
                    remarks = null
                };

                drawing_3d dr_3d = (drawing_3d)await g.webapi.post("drawing_3d", src_dr3d, typeof(drawing_3d));

                g.drawing_3d_list.Add(dr_3d);

                MessageBox.Show("Drawing Upload End");
            }
            else
            {
                MessageBox.Show("Drawing Upload Fail!!");
            }
        }
        
        // 2D 벽 설계 파일 가져오기, DB로 부터  
        private void selectDrawing()
        {
            DrawingSelectDrawing3DWindow DrawingWindow = new DrawingSelectDrawing3DWindow();
            DrawingWindow.ShowDialog();
            if (DrawingWindow.select_drawing_3d != null)
            {
                drawing_3d select_img_vm = DrawingWindow.select_drawing_3d;
                string path = string.Format("{0}drawing_3d/{1}", g.CLIENT_IMAGE_PATH, select_img_vm.file_name);
                openWallDataFromFile(path);
            }
        }

        // 서버에 추가처리 
        public async Task<string>addDrawingFileToServer(string file_path, string file_name)
        {
            //서버에 이미지를 등록해준다
            int UPLOAD_BUFF_SIZE = 40960;
            ProgressBarDialog progressbar_dialog = new ProgressBarDialog();

            Task<TransferResult> t1 = g.webapi.uploadFile("drawing_3d", file_path, UPLOAD_BUFF_SIZE);
            progressbar_dialog.ShowDialog();

            TransferResult ret = await t1;
            //Console.WriteLine("result_code = {0}:{1}", r.result_code,r.server_file_name);


            if (ret.result_code == 0)
            {
                File.Copy(file_path, string.Format("{0}drawing_3d/{1}", g.CLIENT_IMAGE_PATH, ret.server_file_name));
                return ret.server_file_name;
            }
            else
                return null;

        }


        #endregion

    }
}
