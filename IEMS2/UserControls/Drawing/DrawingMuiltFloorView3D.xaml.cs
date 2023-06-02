using I2MS2.Library.Drawing;
using I2MS2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WebApi.Models;

namespace I2MS2.UserControls.Drawing
{
    // 층별뷰에서 사용되는 3D 처리 
    /// <summary>
    /// DrawingMuiltFloorView3D.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DrawingMuiltFloorView3D : UserControl
    {
        int static_num = 0;
                
        Drawing3D drawer3d;
        DrawingDataManager drawDataMgr;
        CameraData def_cam_data;


        #region View Controller variable
        Point joyPadCenterPoint;
        Size joyPadSize;

        viewContorllerSelect_t viewContorllerSelect = viewContorllerSelect_t.NONE;
        Vector joyStick_moveVector;
        Vector view3D_moveVector;
        Point joyStickPad_moveStartPoint;

        double joyPad_BeforeAngle = 0;
        double joyPad_BeforeAngle_temp = 0;

        DispatcherTimer joyStickMoveTimer;
        DispatcherTimer tiltUpTimer;
        DispatcherTimer tiltDownTimer;

        LinearGradientBrush tiltGrButtonPush;
        LinearGradientBrush tiltGrButtonNormal; 
        #endregion


        Drawing3DCamera camCtl;
        Double cameraViewMax = 5000;
        Double cameraViewMin = 100;

        Double firstOrthoGraphicCameraWidth = 1300;
        Double BottomLevelUnit = 100;

        Point cameraFieldLimitMax = new Point(20000, 20000);
        Point cameraFieldLimitMin = new Point(-20000, -20000);

        List<int>[] wall_list_in_fl= new List<int>[200];

        //부모 클래스 내부의 이벤트 호출 연결 고리
        public delegate void camMoveEndHandler(object obj);
        public event camMoveEndHandler camMoveEndEventToFloorView;


        int before_fl_no =-200;

        // 초기화 
        public DrawingMuiltFloorView3D()
        {
            InitializeComponent();
            drawer3d = new Drawing3D(_mainViewPort3D, _canvasDisp);
            drawDataMgr = new DrawingDataManager();
            
            initJoyStick();
            initGradient();
            initZoomSlider();
            
            initCameraControl();

            for (int i = 0; i < 200;i++ )
            {
                wall_list_in_fl[i] = new List<int>();
            }

            camCtl.printCamFocus(2);
        }


        #region ViewController Init
        // 초기화
        private void initJoyStick()
        {
            joyStick_moveVector = new Vector(0, 0);
            joyPadCenterPoint = new Point(viewControllerGrid.Width / 2, viewControllerGrid.Height / 2);
            joyPadSize = new Size(joyPad.Width, joyPad.Height);
            //Console.WriteLine("joPad.Size=({0},{1}), joyPad.CenterPoint({2},{3})", joyPad.Width, joyPad.Height, joyPadCenterPoint.X, joyPadCenterPoint.Y);
            joyStickPad_moveStartPoint = new Point(0, 0);

        }

        LinearGradientBrush moveFloorGrButtonPush;
        LinearGradientBrush moveFloorGrButtonNormal;

        //Gradient Set
        private void initGradient()
        {
            tiltGrButtonPush = new LinearGradientBrush(
               Color.FromArgb(255, 220, 255, 0),
               Color.FromArgb(255, 95, 110, 0),
               new Point(0.5, 0),
               new Point(0.5, 1)
            );  // Opaque blue

            tiltGrButtonNormal = new LinearGradientBrush(
               Color.FromArgb(255, 182, 182, 182),
               Color.FromArgb(255, 85, 85, 85),   // Opaque red
               new Point(0.5, 0),
               new Point(0.5, 1)
               );  // Opaque blue

            moveFloorGrButtonPush = new LinearGradientBrush(
               Color.FromArgb(255, 220, 255, 0),
               Color.FromArgb(255, 95, 110, 0),
               new Point(0.5, 0),
               new Point(0.5, 1)
            );  // Opaque blue

            moveFloorGrButtonNormal = new LinearGradientBrush(
               Color.FromArgb(255, 182, 182, 182),
               Color.FromArgb(255, 85, 85, 85),   // Opaque red
               new Point(0.5, 0),
               new Point(0.5, 1)
               );  // Opaque blue
        }

        // 줌 슬라이더 
        private void initZoomSlider()
        {
            zoomSlider.Value = 100 - ((firstOrthoGraphicCameraWidth - cameraViewMin) / (cameraViewMax - cameraViewMin) * 100);
            mainCamera.Width = firstOrthoGraphicCameraWidth;
        }
        
        #endregion
        // 
        private void initCameraControl()
        {
            view3D_moveVector = new Vector(0, 0);
            firstOrthoGraphicCameraWidth = mainCamera.Width;
            camCtl = new Drawing3DCamera(mainCamera, cameraViewMax, cameraViewMin, BottomLevelUnit, cameraFieldLimitMax, cameraFieldLimitMin, true);
            camCtl.camZMoveEndEvent += new Drawing3DCamera.camZMoveEndEventHandler(camZMoveEnd);

            camCtl.rotateByAngle(-90);
            //camCtl.moveUpFloor();
            //camCtl.moveUpFloor();
            def_cam_data = new CameraData();
            def_cam_data.Width = mainCamera.Width;
            def_cam_data.Position = mainCamera.Position;
            def_cam_data.LookDirection = mainCamera.LookDirection;
        }

        public void changeFloor(Double z, int _before_fl_no)
        {
            before_fl_no =_before_fl_no;
            camCtl.moveUpFloor(z);
            
        }

        public void camZMoveEnd(object obj)
        {
            removeFloor(before_fl_no);
            camMoveEndEventToFloorView(null);
        }



        public void camToDownFloor(Double z)
        {
            camCtl.moveDownFloor(z);
        }

        public void camToUpFloor(Double z)
        {
            camCtl.moveUpFloor(z);
        }


#if false
        public List<WallDraw> openDrawingFile(string path)
        {
            if (File.Exists(path))
            {
                List<WallDraw> wall_list = new List<WallDraw>();
                BinaryFormatter openformat = new BinaryFormatter();
                FileStream openStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                List<WallDraw>[] open_w_list = new List<WallDraw>[4];

                //2nd draw all walls
                open_w_list = (List<WallDraw>[])openformat.Deserialize(openStream);
                for (int i = 0; i < 4; i++)
                {
                    foreach (var w in open_w_list[i])
                    {

                        WallDraw w_3d = drawDataMgr.makeWallVMDataFor3d(w);
                        wall_list.Add(w_3d);
                    }

                }

                try
                {
                    List<WallCornerDraw>[] open_wc_list = (List<WallCornerDraw>[])openformat.Deserialize(openStream);

                    for (int i = 0; i < 4; i++)
                    {
                        foreach (var wc in open_wc_list[i])
                        {
                            if (!wc_dic.ContainsKey(wc.id))
                            {
                                addWallCorner(wc);
                            }
                        }
                    }

                }
                catch (Exception ex) { }
                openStream.Close();

                return wall_list;

            }
            else
                return null;
        } 
#endif

        public Boolean removeFloor(int fl_no)
        {
            drawer3d.removeOneFloor(fl_no);
            return true;
        }


        public Boolean drawFloor(List<WallDraw> w_list, List<WallCornerDraw> wc_list, List<RackDrawVM>rk_list, Double z, int number)
        {
            Point p = new Point(0, 0);
            Size s = new Size(1024, 768);
            //Double z = (number-1) * 150;
            Brush br = new SolidColorBrush(Color.FromArgb(0xFF,0x25,0x25,0x25));

            drawer3d.addOneFloor(p, s, z, br, w_list, wc_list,rk_list, number);
            return true;
        }



        private WallDraw makeWallVMDataFor3d(WallDraw w)
        {
            return new WallDraw()
            {
                id = w.id,
                start_p = drawDataMgr.get3DPoint_FromVMPoint(w.start_p),
                end_p = drawDataMgr.get3DPoint_FromVMPoint(w.end_p),
                thickness = drawDataMgr.get3DValue_FromVMValue(w.thickness),
                height = drawDataMgr.get3DValue_FromVMValue(w.height),
                alpha = w.alpha,
                colorA = w.colorA,
                colorR = w.colorR,
                colorG = w.colorG,
                colorB = w.colorB,
                Z = w.Z
            };
        }


        private WallDraw makeWallVM(WallDraw w, int num)
        {
            return new WallDraw()
            {
                id = num,
                start_p = w.start_p,
                end_p = w.end_p,
                thickness = w.thickness,
                height = w.height,
                alpha = w.alpha,
                colorA = w.colorA,
                colorR = w.colorR,
                colorG = w.colorG,
                colorB = w.colorB,
                Z = 0
            };
        }

        #region ViewControl With Mouse Event

        private void _gridMain_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            joyStick_moveVector.X = 0;
            joyStick_moveVector.Y = 0;

            view3D_moveVector = new Vector(0, 0);

            joyStickPad_moveStartPoint = e.GetPosition(viewControllerGrid);

        }

        private void _gridMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //update moveVector for move_Tick after caculate
                joyStick_moveVector = joyStick_calculate_moveVector(e.GetPosition(viewControllerGrid));
                view3D_moveVector = calculateMove3DVector(joyStick_moveVector);

                joyStickPad_moveStartPoint = e.GetPosition(viewControllerGrid);


                camCtl.movingBy2DMoveByJoyStick(view3D_moveVector);
                //camCtl.movingBy2DMoveByJoyStick(joyStick_moveVector);
                drawer3d.reDrawInfoViewAll();

                //move joyStick
                //joyStickMove(joyStick_moveVector);
            }
        }


        private void _gridMain_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                zoomSlider.Value += 1;
            else
                zoomSlider.Value -= 1;
        }
        #endregion

        
        #region ViewControler
        private void viewControllerGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string typeString = string.Format("{0}", e.Source.GetType());
            Console.WriteLine("type={0}", typeString);
            if (e.Source is Ellipse)
            {
                Ellipse selectEllipse = (Ellipse)e.Source;
                if (selectEllipse.Name == "joyStick")
                {
                    viewContorllerSelect = viewContorllerSelect_t.JOYSTICK;
                }
                else if (selectEllipse.Name == "joyPad")
                {
                    viewContorllerSelect = viewContorllerSelect_t.JOYPAD;
                }
            }
            else if (e.Source is TextBlock)
            {
                TextBlock tempTextBlock = (TextBlock)e.Source;
                if (tempTextBlock.Name == "joyPadText")
                    viewContorllerSelect = viewContorllerSelect_t.JOYPAD;
            }
            else if (e.Source is System.Windows.Documents.Run)
            {
                //Run tempRun = (Run)e.Source;
                //Console.WriteLine("{0}",tempRun.Name);
                //if(tempRun.Name=="joyPadTextRec")
                viewContorllerSelect = viewContorllerSelect_t.JOYPAD;
            }
            else if (e.Source is Rectangle)
            {
                Rectangle tempRectangle = (Rectangle)e.Source;
                if (tempRectangle.Name == "joyPadTextRec")
                    viewContorllerSelect = viewContorllerSelect_t.JOYPAD;
            }
            else if (e.Source is Polygon)
            {
                Polygon selectPolygon = (Polygon)e.Source;
                if (selectPolygon.Name == "tiltUp")
                {
                    viewContorllerSelect = viewContorllerSelect_t.TILTUP;
                }
                else if (selectPolygon.Name == "tiltDown")
                {
                    viewContorllerSelect = viewContorllerSelect_t.TILTDOWN;
                }

            }

            switch (viewContorllerSelect)
            {
                case viewContorllerSelect_t.JOYSTICK:

                    joyStick_moveVector.X = 0;
                    joyStick_moveVector.Y = 0;

                    view3D_moveVector = new Vector(0, 0);

                    joyStickPad_moveStartPoint = e.GetPosition(viewControllerGrid);

                    joyStickMoveTimer = new DispatcherTimer();
                    joyStickMoveTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
                    joyStickMoveTimer.Tick += new EventHandler(joyStickMove_Tick);
                    joyStickMoveTimer.Start();
                    break;
                case viewContorllerSelect_t.JOYPAD:
                    joyStickPad_moveStartPoint = e.GetPosition(viewControllerGrid);
                    break;
                case viewContorllerSelect_t.TILTUP:
                    tiltUpStart();
                    break;
                case viewContorllerSelect_t.TILTDOWN:
                    tiltDownStart();
                    break;
                default:
                    break;
            }
        }

        private void viewControllerGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                switch (viewContorllerSelect)
                {
                    case viewContorllerSelect_t.JOYSTICK:

                        //update moveVector for move_Tick after caculate
                        joyStick_moveVector = joyStick_calculate_moveVector(e.GetPosition(viewControllerGrid));
                        view3D_moveVector = calculateMove3DVector(joyStick_moveVector);
                        //move joyStick
                        joyStickMove(joyStick_moveVector);

                        break;
                    case viewContorllerSelect_t.JOYPAD:
                        double rotateAngle = joyPad_caculateAngleFromPoint(e.GetPosition(viewControllerGrid));

                        RotateTransform tempRotateTransform = new RotateTransform();
                        //tempRotateTransform.CenterX = joyPadCenterPoint.X;
                        //tempRotateTransform.CenterY = joyPadCenterPoint.Y;
                        tempRotateTransform.CenterX = joyPad.Width / 2;
                        tempRotateTransform.CenterY = joyPad.Height / 2;
                        tempRotateTransform.Angle = joyPad_BeforeAngle + rotateAngle;

                        //joyPad.RenderTransform = tempRotateTransform;
                        joyPadCanvas.RenderTransform = tempRotateTransform;

                        //3d view rotate
                        double _3dAngle = rotateAngle - (joyPad_BeforeAngle_temp - joyPad_BeforeAngle);

                        //Console.WriteLine("_3dAngle={0}, joyPad bt={1}, joyPad ba={2}", rotateAngle, joyPad_BeforeAngle_temp, joyPad_BeforeAngle);
            
                        camCtl.rotateByAngle(_3dAngle);
                        drawer3d.reDrawInfoViewAll();
            

                        joyPad_BeforeAngle_temp = joyPad_BeforeAngle + rotateAngle;


                        break;
                    case viewContorllerSelect_t.TILTUP:
                        break;
                    case viewContorllerSelect_t.TILTDOWN:
                        break;
                    default:
                        break;
                }
            }
        }

        private void viewControllerGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            viewController_EventEnd();
        }

        private void viewControllerGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            viewController_EventEnd();
        }



        private void viewController_EventEnd()
        {
            switch (viewContorllerSelect)
            {
                case viewContorllerSelect_t.JOYSTICK:
                    joyStickMoveTimer.Stop();
                    joyStickMove(new Vector(0, 0));
                    break;
                case viewContorllerSelect_t.JOYPAD:
                    joyPad_BeforeAngle = joyPad_BeforeAngle_temp;
                    break;
                case viewContorllerSelect_t.TILTUP:
                    tiltUpStop();
                    break;
                case viewContorllerSelect_t.TILTDOWN:
                    tiltDownStop();
                    break;
                default:
                    break;
            }
            viewContorllerSelect = viewContorllerSelect_t.NONE;
        }


        //========================== Timer Tick ==================================
        private void joyStickMove_Tick(object sender, EventArgs e)
        {
            camCtl.movingBy2DMoveByJoyStick(view3D_moveVector);
            //camCtl.movingBy2DMove(joyStick_moveVector);
            drawer3d.reDrawInfoViewAll();
        }

        private void tiltUpRun_Tick(object sender, EventArgs e)
        {
            camCtl.tiltbyAngle(1);
            drawer3d.reDrawInfoViewAll();
        }


        private void tiltDownRun_Tick(object sender, EventArgs e)
        {
            camCtl.tiltbyAngle(-1);
            drawer3d.reDrawInfoViewAll();
        }
        //========================== END Timer Tick ==================================

        //==================joyStick,Pad move=================
        private void joyStickMove(Vector _vector)
        {
            TranslateTransform tempTranslate = new TranslateTransform();
            tempTranslate.X = _vector.X;
            tempTranslate.Y = _vector.Y;

            joyStick.RenderTransform = tempTranslate;
        }

        //==================joyStick,Pad move END=================



        //==================joyStick,Pad caculate================================
        #region JoyStick,Pad CalCulate
        private Vector joyStick_calculate_moveVector(Point currentPoint)
        {
            double moveX = currentPoint.X - joyStickPad_moveStartPoint.X;
            double moveY = currentPoint.Y - joyStickPad_moveStartPoint.Y;


            //!!!! need more detail limit (this limit shape is Rectangle, we need limit shap circle)
            if (moveX > joyPadSize.Width / 3)
                moveX = joyPadSize.Width / 3;
            else if (moveX < -joyPadSize.Width / 3)
                moveX = -joyPadSize.Width / 3;

            if (moveY > joyPadSize.Height / 3)
                moveY = joyPadSize.Height / 3;
            else if (moveY < -joyPadSize.Height / 3)
                moveY = -joyPadSize.Height / 3;

            Vector moveVector = new Vector(moveX, moveY);
            return moveVector;

        }

        private Vector calculateMove3DVector(Vector v)
        {
            //Double ratio = 200 / zoomSlider.Value;
            Double ratio = mainCamera.Width / 100;
            
            Vector moveVector = new Vector(v.X * ratio, v.Y * ratio);
            return moveVector;
        }

        private double joyPad_caculateAngleFromPoint(Point currentPoint)
        {
            double X1 = joyStickPad_moveStartPoint.X - joyPadCenterPoint.X;
            double Y1 = joyStickPad_moveStartPoint.Y - joyPadCenterPoint.Y;
            double theta1 = (Math.Atan(Math.Abs(X1 / Y1))) * 180 / Math.PI;

            double X2 = currentPoint.X - joyPadCenterPoint.X;
            double Y2 = currentPoint.Y - joyPadCenterPoint.Y;
            double theta2 = (Math.Atan(Math.Abs(X2 / Y2))) * 180 / Math.PI;

            double theta3 = 0;

            double thX = X1 * X2;
            double thY = Y1 * Y2;


            if (((X1 >= 0) && (Y1 >= 0)) || ((X1 < 0) && (Y1 < 0)))
            {
                if ((thX >= 0) && (thY >= 0))  //theta3 < 90
                {
                    theta3 = theta1 - theta2;
                }
                else if ((thX < 0) && (thY >= 0)) //theta3 < 180
                {
                    theta3 = theta1 + theta2;
                }
                else if ((thX < 0) && (thY < 0)) //theta3 < 270
                {
                    theta3 = 180 + theta1 - theta2;
                }
                else if ((thX >= 0) && (thY < 0)) //theta3 < 360
                {
                    theta3 = 180 + theta1 + theta2;
                }
            }
            else
            {
                if ((thX >= 0) && (thY >= 0))  //theta3 < 90
                {
                    theta3 = theta2 - theta1;
                }
                else if ((thX >= 0) && (thY < 0)) //theta3 < 180
                {
                    theta3 = 180 - (theta2 + theta1);
                }
                else if ((thX < 0) && (thY < 0)) //theta3 < 270
                {
                    theta3 = 180 + theta2 - theta1;
                }
                else if ((thX < 0) && (thY >= 0)) //theta3 < 360
                {
                    theta3 = 360 - (theta2 + theta1);
                }
            }

            //Console.WriteLine("==================");
            //Console.WriteLine("X1={0},Y1={1}", (int)X1, (int)Y1);
            //Console.WriteLine("X2={0},Y2={1}", (int)X2, (int)Y2);
            //Console.WriteLine("theta1={0}, theta2={1}, theta3={2}", (int)theta1, (int)theta2, (int)theta3);
            return theta3;
        }
        #endregion
        //==========================================================


        //==============tilt func ===================================
        private void tiltUpStart()
        {
            tiltUp.Fill = tiltGrButtonPush;
            ScaleTransform tempScaleTrans = new ScaleTransform(1.2, 1.2);
            tiltUp.LayoutTransform = tempScaleTrans;

            tiltUpTimer = new DispatcherTimer();
            tiltUpTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            tiltUpTimer.Tick += new EventHandler(tiltUpRun_Tick);
            tiltUpTimer.Start();
        }

        private void tiltUpStop()
        {
            tiltUp.Fill = tiltGrButtonNormal;
            ScaleTransform tempScaleTrans = new ScaleTransform(1, 1);
            tiltUp.LayoutTransform = tempScaleTrans;

            tiltUpTimer.Stop();
        }

        private void tiltDownStart()
        {
            tiltDown.Fill = tiltGrButtonPush;
            ScaleTransform tempScaleTrans = new ScaleTransform(1.2, 1.2);
            tiltDown.LayoutTransform = tempScaleTrans;

            tiltDownTimer = new DispatcherTimer();
            tiltDownTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            tiltDownTimer.Tick += new EventHandler(tiltDownRun_Tick);
            tiltDownTimer.Start();
        }

        private void tiltDownStop()
        {
            tiltDown.Fill = tiltGrButtonNormal;
            ScaleTransform tempScaleTrans = new ScaleTransform(1, 1);
            tiltDown.LayoutTransform = tempScaleTrans;
            tiltDownTimer.Stop();
        }




        private Storyboard myStoryboard;

        private void viewControlPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation myDoubleAnimation = new DoubleAnimation();
            myDoubleAnimation.From = 0.3;
            myDoubleAnimation.To = 1;
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(500));

            myStoryboard = new Storyboard();
            myStoryboard.Children.Add(myDoubleAnimation);
            Storyboard.SetTargetName(myDoubleAnimation, viewControlPanel.Name);
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(StackPanel.OpacityProperty));

            myStoryboard.Begin(viewControlPanel);
        }

        private void viewControlPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation myDoubleAnimation = new DoubleAnimation();
            myDoubleAnimation.From = 1;
            myDoubleAnimation.To = 0.3;
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(500));


            myStoryboard = new Storyboard();
            myStoryboard.Children.Add(myDoubleAnimation);
            Storyboard.SetTargetName(myDoubleAnimation, viewControlPanel.Name);
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(StackPanel.OpacityProperty));

            myStoryboard.Begin(viewControlPanel);
        }

        //============================================================
        //================Zoom slider Event==========================================
        private void zoomInGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            zoomSlider.Value += 10;
            camCtl.zoomByValue(zoomSlider.Value);
            drawer3d.reDrawInfoViewAll();
            
        }

        private void zoomOutGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            zoomSlider.Value -= 10;
            camCtl.zoomByValue(zoomSlider.Value);
            drawer3d.reDrawInfoViewAll();
            
        }

        private void zoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (camCtl != null)
            {
                camCtl.zoomByValue(zoomSlider.Value);
                drawer3d.reDrawInfoViewAll();
            }
            
        }

        private void _btnRol20_Click(object sender, RoutedEventArgs e)
        {
            camCtl.rotateByAngle(20);
        }

        private void Up_Event(object sender, RoutedEventArgs e)
        {
            camCtl.moveUpFloor(100);
        }

        private void Down_Event(object sender, RoutedEventArgs e)
        {
            camCtl.moveDownFloor(-100);
        }

        //================END Zoom slider Event==========================================

        #endregion


    }
}
