using I2MS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace I2MS2.Library.Drawing
{
    // 3D 카메라 처리 -> 카메라 이동 및 줌 처리 , 타이머를 통한 애니메이션 처리 , 패닝 처리 , 층이동 처리 
    class Drawing3DCamera
    {
        //*****2D to 3D reduce ratio define*****!!
        public double reduceRatio = 10;

        PerspectiveCamera PerspMainCamera;      // 원근법 있음
        OrthographicCamera OrthoMainCamera;     // 원근법 없음    -> 현재 사용 

        Point3D cameraFocus;
        double cameraFieldOfViewMax, cameraFieldOfViewMin;
        double cameraWidthMax, cameraWidthMin;

        Point cameraFieldMax, cameraFieldMin;
        double floorLevelUnit;

        Boolean IsOrthoGraphicCamera = false;


        //부모 클래스 내부의 이벤트 호출 연결 고리
        public delegate void camMoveEndEventHandler(object obj);
        public event camMoveEndEventHandler camMoveEndEvent;

        public delegate void camZMoveEndEventHandler(object obj);
        public event camZMoveEndEventHandler camZMoveEndEvent;



        //Camera Position(px,py,pz) Camera Look Vector(-vx,-vy,-vz)을 기본으로 잡고 있는 경우
        public Point getCameraCenter2D()
        {
            Double ct_x = OrthoMainCamera.Position.X + OrthoMainCamera.LookDirection.X;
            Double ct_y = OrthoMainCamera.Position.Y + OrthoMainCamera.LookDirection.Y;

            return new Point(ct_x, ct_y);
        }
        // 사용안함  PerspectiveCamera -> OrthographicCamera 변경 
        public Drawing3DCamera(PerspectiveCamera _camera, double _camFofVMax, double _camFofVMin, double _floorLevelUnit, Point _cameraFieldMax, Point _cameraFieldMin)
        {
            //init PerspMainCamera, camera Focus
            
            PerspMainCamera = _camera;
            cameraFocus = new Point3D(
                PerspMainCamera.Position.X + PerspMainCamera.LookDirection.X,
                PerspMainCamera.Position.Y + PerspMainCamera.LookDirection.Y,
                PerspMainCamera.Position.Z + PerspMainCamera.LookDirection.Z);
            cameraFieldOfViewMax = _camFofVMax;
            cameraFieldOfViewMin = _camFofVMin;

            cameraFieldMax = _cameraFieldMax;
            cameraFieldMin = _cameraFieldMin;
            floorLevelUnit = _floorLevelUnit;

          //  Console.WriteLine("cameraFocus=({0},{1},{2})", cameraFocus.X, cameraFocus.Y, cameraFocus.Z);
        }

        public Drawing3DCamera(OrthographicCamera _camera, double _camWidthMax, double _camWidthVMin, double _floorLevelUnit, Point _cameraFieldMax, Point _cameraFieldMin, Boolean _isOrthGraphicCam)
        {
            //init PerspMainCamera, camera Focus

            OrthoMainCamera = _camera;
            cameraFocus = new Point3D(
                OrthoMainCamera.Position.X + OrthoMainCamera.LookDirection.X,
                OrthoMainCamera.Position.Y + OrthoMainCamera.LookDirection.Y,
                OrthoMainCamera.Position.Z + OrthoMainCamera.LookDirection.Z);
            cameraWidthMax = _camWidthMax;
            cameraWidthMin = _camWidthVMin;

            cameraFieldMax = _cameraFieldMax;
            cameraFieldMin = _cameraFieldMin;
            floorLevelUnit = _floorLevelUnit;

            IsOrthoGraphicCamera = _isOrthGraphicCam;

        //    Console.WriteLine("cameraFocus=({0},{1},{2})", cameraFocus.X, cameraFocus.Y, cameraFocus.Z);
        }

        // 사용안함 -> 카메라 바꾸면서 사용하다 이제는 안함 
        public void cameraChange(OrthographicCamera _camera, double _camWidthMax, double _camWidthVMin, double _floorLevelUnit, Point _cameraFieldMax, Point _cameraFieldMin)
        {
            OrthoMainCamera = _camera;
            cameraFocus = new Point3D(
                OrthoMainCamera.Position.X + OrthoMainCamera.LookDirection.X,
                OrthoMainCamera.Position.Y + OrthoMainCamera.LookDirection.Y,
                OrthoMainCamera.Position.Z + OrthoMainCamera.LookDirection.Z);
            cameraWidthMax = _camWidthMax;
            cameraWidthMin = _camWidthVMin;

            cameraFieldMax = _cameraFieldMax;
            cameraFieldMin = _cameraFieldMin;
            floorLevelUnit = _floorLevelUnit;

            IsOrthoGraphicCamera = true;

       //     Console.WriteLine("cameraFocus=({0},{1},{2})", cameraFocus.X, cameraFocus.Y, cameraFocus.Z);
        }
        // 카메라의 이동으로 화면의 줌인 -> 패닝 처리 
        DispatcherTimer camMove_Timer; // 타이머 사용 
        Vector3D camMoveDV;
        int cam_move_timer_cnt = 0;
        int cam_move_step = 10; // 화면 이동틱 카운트 10번 고정 -> 


        DispatcherTimer camMoveZ_Timer; // 멀티뷰에서 층이동 
        Double camMoveZ_DV;
        int cam_move_z_timer_cnt = 0;
        int cam_move_z_step = 50;

        // 틱 발생시 
        private void camMove_Tick(object sender, EventArgs e)
        {
            if(cam_move_timer_cnt < cam_move_step) 
            {
                movingBy2DMoveWithZoom2(new Vector(camMoveDV.X, camMoveDV.Y), camMoveDV.Z); // 
                cam_move_timer_cnt++;
            }
            else
            {
                camMove_Timer.Stop(); // 스톱 
                cam_move_timer_cnt = 0;
                camMoveDV = new Vector3D(0, 0, 0);
                camMoveEndEvent(null);
            }
        }
        // 카메라 이동 시작 
        public void movingBy2DMoveAnimWithZoom(Vector _3dvector, Double width, int milsec)
        {
            Console.WriteLine("move({0:F0},{1:F0}, width={2:F0}", _3dvector.X, _3dvector.Y, width);
            Double tick_num = cam_move_step;
            cam_move_timer_cnt = 0;
            camMoveDV = new Vector3D();
            camMoveDV.X = _3dvector.X / tick_num;
            camMoveDV.Y = _3dvector.Y / tick_num;
            camMoveDV.Z = (width - OrthoMainCamera.Width) / tick_num;

            camMove_Timer = new DispatcherTimer();
            camMove_Timer.Interval = new TimeSpan( 0, 0, 0, 0, 10); // romee
            //camMove_Timer.Interval = new TimeSpan(0, 0, 0, 1);
            camMove_Timer.Tick += new EventHandler(camMove_Tick);

            camMove_Timer.Start();
        }
        // 카메라가 내려가면 Z 가 올라감  -> 
        private void camMoveZ_Tick(object sender, EventArgs e)
        {
            if (cam_move_z_timer_cnt < cam_move_z_step)
            {
                movingByMoveZ(camMoveZ_DV);
                cam_move_z_timer_cnt++;
            }
            else
            {
                camMoveZ_Timer.Stop();
                cam_move_z_timer_cnt = 0;
                camMoveZ_DV = 0;
                camZMoveEndEvent(null);
            }
        }
        // 
        public void movingByZMoveAnim(Double Z, int milsec)
        {
            Double tick_num = cam_move_z_step;

            camMoveZ_DV = Z  / tick_num;

            camMoveZ_Timer = new DispatcherTimer();
            camMoveZ_Timer.Interval = new TimeSpan(0, 0, 0, 0, 10); // romee
            //camMove_Timer.Interval = new TimeSpan(0, 0, 0, 1);
            camMoveZ_Timer.Tick += new EventHandler(camMoveZ_Tick);

            camMoveZ_Timer.Start();
        }

        // 사용자 마우스에 대한 줌 처리 
        public void movingBy2DMoveWithZoom( Vector _3dvector, Double width)
        {
            Point3D tempCamPosition = new Point3D();

            if (IsOrthoGraphicCamera)
            {
                tempCamPosition.Z = OrthoMainCamera.Position.Z;

                double checkX = OrthoMainCamera.Position.X + _3dvector.X;
                double checkY = OrthoMainCamera.Position.Y + _3dvector.Y;

                tempCamPosition.X = OrthoMainCamera.Position.X + _3dvector.X;
                cameraFocus.X += _3dvector.X;

                tempCamPosition.Y = OrthoMainCamera.Position.Y + _3dvector.Y;
                cameraFocus.Y += _3dvector.Y;

                OrthoMainCamera.Position = tempCamPosition;
                OrthoMainCamera.Width = width;
            }
            else
            { // 사용안함 
                tempCamPosition.Z = PerspMainCamera.Position.Z;

                double checkX = PerspMainCamera.Position.X + _3dvector.X;
                double checkY = PerspMainCamera.Position.Y + _3dvector.Y;

                tempCamPosition.X = PerspMainCamera.Position.X + _3dvector.X;
                cameraFocus.X += _3dvector.X;


                tempCamPosition.Y = PerspMainCamera.Position.Y + _3dvector.Y;
                cameraFocus.Y += _3dvector.Y;

                PerspMainCamera.Position = tempCamPosition;
                PerspMainCamera.FieldOfView = width;
            }

        }

        // 카메라 이동 처리 
        public void movingBy2DMoveWithZoom2(Vector _3dvector, Double width_v)
        {
            Point3D tempCamPosition = new Point3D();

            tempCamPosition.Z = OrthoMainCamera.Position.Z;

            double checkX = OrthoMainCamera.Position.X + _3dvector.X;
            double checkY = OrthoMainCamera.Position.Y + _3dvector.Y;

            tempCamPosition.X = OrthoMainCamera.Position.X + _3dvector.X;
            cameraFocus.X += _3dvector.X;

            tempCamPosition.Y = OrthoMainCamera.Position.Y + _3dvector.Y;
            cameraFocus.Y += _3dvector.Y;

            OrthoMainCamera.Position = tempCamPosition;
            OrthoMainCamera.Width += width_v;
        }
        // 층 이동 무빙 
        public void movingByMoveZ(Double z)
        {
            Point3D tempCamPosition = new Point3D();
            tempCamPosition = OrthoMainCamera.Position;
            tempCamPosition.Z = OrthoMainCamera.Position.Z + z;

            OrthoMainCamera.Position = tempCamPosition;
        }

        // 마우스 이동 
        public void movingBy2DMoveByJoyStick(Vector _2dvector)
        {
            Point3D tempCamPosition = new Point3D();
            Vector _3dvector = caculateMoveCamera(_2dvector);

            if (IsOrthoGraphicCamera)
            {
                tempCamPosition.Z = OrthoMainCamera.Position.Z;

                double checkX = OrthoMainCamera.Position.X + _3dvector.X;
                double checkY = OrthoMainCamera.Position.Y + _3dvector.Y;

                tempCamPosition.X = OrthoMainCamera.Position.X + _3dvector.X;
                cameraFocus.X += _3dvector.X;


                tempCamPosition.Y = OrthoMainCamera.Position.Y + _3dvector.Y;
                cameraFocus.Y += _3dvector.Y;

                OrthoMainCamera.Position = tempCamPosition;
            }
            else
            {
                tempCamPosition.Z = PerspMainCamera.Position.Z;

                double checkX = PerspMainCamera.Position.X + _3dvector.X;
                double checkY = PerspMainCamera.Position.Y + _3dvector.Y;

                tempCamPosition.X = PerspMainCamera.Position.X + _3dvector.X;
                cameraFocus.X += _3dvector.X;


                tempCamPosition.Y = PerspMainCamera.Position.Y + _3dvector.Y;
                cameraFocus.Y += _3dvector.Y;

                PerspMainCamera.Position = tempCamPosition;
            }

        }
        // 앵글 회전 
        public void rotateByAngle(double _angle)
        {
            rotateCamera(_angle);
        }
        // 값에 따른 줌 처리 
        public void zoomByValue(double _value)
        {
            if (IsOrthoGraphicCamera)
            {
                OrthoMainCamera.Width = ((cameraWidthMax - cameraWidthMin) / 100) * (100 - _value) + cameraWidthMin;
              //  Console.WriteLine("camWidth ={0}", OrthoMainCamera.Width);
            }
            else
                PerspMainCamera.FieldOfView = ((cameraFieldOfViewMax - cameraFieldOfViewMin) / 100) * (100 - _value) + cameraFieldOfViewMin; 
        }

        // 위로 
        public void moveUpFloor(Double z)
        {
            //double zvalue = floorLevelUnit;
            double zvalue = z;

            movingByZMoveAnim(zvalue, 400);
            //moveFloorLevel(zvalue);
        }
        // 아래로 
        public void moveDownFloor(Double z)
        {
            //double zvalue = -floorLevelUnit;
            double zvalue = -z;

            movingByZMoveAnim(zvalue, 400);
            //moveFloorLevel(zvalue);
        }

        // 해당 층으로 
        Point3D tempPosition;
        private void  moveFloorLevel(double zvalue) // 인자 층 
        {
            tempPosition = new Point3D();
      
//#if NO_ANIM
#if true
            tempPosition = OrthoMainCamera.Position;
            tempPosition.Z = OrthoMainCamera.Position.Z + zvalue;
            OrthoMainCamera.Position = tempPosition;
            cameraFocus.Z = zvalue + tempPosition.Z + OrthoMainCamera.LookDirection.Z;
#else
            tempPosition = OrthoMainCamera.Position;
            tempPosition.Z = OrthoMainCamera.Position.Z + zvalue;


            Transform3DGroup _transform = new Transform3DGroup();
            TranslateTransform3D _translateTransform = new TranslateTransform3D();
            _translateTransform.OffsetZ = OrthoMainCamera.Position.Z;

            DoubleAnimation animZ = new DoubleAnimation(tempPosition.Z, new Duration(TimeSpan.FromMilliseconds(200)));
            animZ.Completed += traslateZCompleted;

            _transform.Children.Add(_translateTransform);

            OrthoMainCamera.Transform = _transform;

            _translateTransform.BeginAnimation(TranslateTransform3D.OffsetZProperty, animZ);
    
#endif
           
        }
        // 타이머가 끝나면 호출됨 -> 카메라 보는 위치 + 사용자 보는 위치 => 카메라 포커스 romee 
        private void traslateZCompleted(object sender, EventArgs e)
        {
            OrthoMainCamera.Position = tempPosition;
            cameraFocus.Z = tempPosition.Z + OrthoMainCamera.LookDirection.Z;
        }

        // 이동 카메라  X, Y
        private Vector caculateMoveCamera(Vector _2dVector)
        {
            //moveY caculate
            Vector XmoveDirection = caculateMoveCameraX(_2dVector.X);
            Vector YmoveDirection = caculateMoveCameraY(_2dVector.Y);
            Vector moveDirection = 
                new Vector(
                    (XmoveDirection.X + YmoveDirection.X)/reduceRatio, 
                    (XmoveDirection.Y + YmoveDirection.Y)/reduceRatio);


            return moveDirection;
        }

        // 이동 카메라  X 
        private Vector caculateMoveCameraX(double moveX)
        {
            double Xp;
            double Yp;

            if (IsOrthoGraphicCamera)
            {
                Xp = OrthoMainCamera.Position.X;
                Yp = OrthoMainCamera.Position.Y;
            }
            else
            {
                Xp = PerspMainCamera.Position.X;
                Yp = PerspMainCamera.Position.Y;
            }
            double X1 = Xp - cameraFocus.X;
            double Y1 = Yp - cameraFocus.Y;
            Vector XmoveDirection = new Vector(0, 0);

            if (moveX == 0)
                return XmoveDirection;

            //moveX caculate
            //double ce1 = Math.Atan(X1 / Y1) * 180 / Math.PI;
            double ce1 = Math.Atan(Math.Abs(X1 / Y1)) * 180 / Math.PI;

            double tempDistance_B = Math.Pow(Math.Pow(X1, 2) + Math.Pow(Y1, 2), 0.5);
            double ce2 = Math.Atan(moveX / tempDistance_B) * 180 / Math.PI;
            //double ce2 = Math.Atan(Math.Abs(moveX / tempDistance_B)) * 180 / Math.PI;


            double tempDistance_C = Math.Pow(Math.Pow(tempDistance_B, 2) + Math.Pow(moveX, 2), 0.5);
            double ce3 = 90 - (ce1 + ce2);
            //double ce3;
            //int k = 0; 
            int Xd = 1, Yd = 1;

            if (X1 >= 0)
            {
                if (Y1 >= 0)
                {
                    if ((ce1 + ce2) < 90)// 1
                    {
                    //    k = 1;
                        ce3 = 90 - (ce1 + ce2);
                    }
                    else// 1-2
                    {
                    //    k = 12;
                        ce3 = -90 + (ce1 + ce2);
                        Yd = -1;
                    }
                }
                else
                {
                    if (((90 - ce1) + ce2) < 90)// 2
                    {
                    //    k = 2;
                        ce3 = 90 - (ce1 - ce2);
                        Yd = -1;
                    }
                    else// 2-3
                    {
                    //    k = 23;
                        ce3 = 90 + (ce1 - ce2);
                        Xd = -1;
                        Yd = -1;
                    }
                }
            }
            else
            {
                if (Y1 >= 0)
                {
                    if (((90 - ce1) + ce2) < 90)// 4
                    {
                    //    k = 4;
                        ce3 = 90 - (ce1 - ce2);
                        Xd = -1;
                    }
                    else// 4-1
                    {
                    //    k = 41;
                        ce3 = 90 + (ce1 - ce2);
                    }
                }
                else
                {
                    if ((ce1 + ce2) < 90)// 3
                    {
                    //    k = 3;
                        ce3 = 90 - (ce1 + ce2);
                        Xd = -1;
                        Yd = -1;
                    }
                    else// 3-4
                    {
                     //   k = 34;
                        ce3 = -90 + (ce1 + ce2);
                        Xd = -1;
                    }
                }
            }
            //ce3 = 90 - (ce1 + ce2);
            double X2 = Xd * tempDistance_C * Math.Cos(ce3 * (Math.PI / 180));
            double Y2 = Yd * tempDistance_C * Math.Sin(ce3 * (Math.PI / 180));
            //double X2 = tempDistance_C * Math.Cos(ce3 * (Math.PI / 180));
            //double Y2 = tempDistance_C * Math.Sin(ce3 * (Math.PI / 180));

            XmoveDirection.X = X2 - X1;
            XmoveDirection.Y = Y2 - Y1;

            //Console.WriteLine("===============position[{0}]===================",k);
            //Console.WriteLine("center.X={0}, Y={1}", (int)camFocusPoint3D.X, (int)camFocusPoint3D.Y);
            //Console.WriteLine("camPos.X={0}, Y={1}, directionLength={2}", (int)PerspMainCamera0.Position.X, (int)PerspMainCamera0.Position.Y,
            //    (int)Math.Pow((Math.Pow(PerspMainCamera0.Position.X - camFocusPoint3D.X, 2) + Math.Pow(PerspMainCamera0.Position.Y - camFocusPoint3D.Y, 2)), 0.5));
            //Console.WriteLine("X1={0}, Y1={1}, X2={2}, Y2={3}", (int)X1, (int)Y1, (int)X2, (int)Y2);
            //Console.WriteLine("A={0}, B={1}, C={2}", moveX, tempDistance_B, (int)tempDistance_C);
            //Console.WriteLine("ce1={0}, ce2={1}, ce3={2}", (int)ce1, (int)ce2, (int)ce3 );
            //Console.WriteLine("X:X move={0}, Y move={1}", (int)XmoveDirection.X, (int)XmoveDirection.Y);

            return XmoveDirection;
        }
        // 카메라 위치 
        public void printCamFocus(int id)
        {
            Console.WriteLine("{0} ->({1:F0},{2:F0},{3:F0})", id, cameraFocus.X, cameraFocus.Y, cameraFocus.Z);
        }
        //Y 축 이동 
        private Vector caculateMoveCameraY(double moveY)
        {
            double Xp;
            double Yp;
            if (IsOrthoGraphicCamera)
            {
                Xp = OrthoMainCamera.Position.X;
                Yp = OrthoMainCamera.Position.Y;
            }
            else
            {
                Xp = PerspMainCamera.Position.X;
                Yp = PerspMainCamera.Position.Y;
            }
            

            double X1 = Xp - cameraFocus.X;
            double Y1 = Yp - cameraFocus.Y;

            double Xdir = 1, Ydir = 1;
            if (X1 < 0)
                Xdir = -1;
            if (Y1 < 0)
                Ydir = -1;

            //double ce = Math.Atan(X1 / Y1) * 180 / Math.PI;
            double ce = Math.Atan(Math.Abs(X1 / Y1)) * 180 / Math.PI;

            double Xdif = moveY * Math.Sin(ce * (Math.PI / 180)) * Xdir;
            double Ydif = moveY * Math.Cos(ce * (Math.PI / 180)) * Ydir;

            Vector YmoveDirection = new Vector(-Xdif, -Ydif);
            //Console.WriteLine("X1={0}, Y1={1}, Xd={2}, Yd={3}, ce={4}", (int)X1, (int)Y1, (int)Xdif, (int)Ydif, (int)ce);
            //Console.WriteLine("Y:X move={0}, Y move={1}", (int)YmoveDirection.X, (int)YmoveDirection.Y);

            return YmoveDirection;

        }

        // 회전 
        private void rotateCamera(double angle)
        {
            // change position

            //Console.WriteLine("cam({0},{1}), jogFocus({2},{3})", PerspMainCamera.Position.X, PerspMainCamera.Position.Y, cameraFocus.X, cameraFocus.Y);
            double X1;
            double Y1;
            if (IsOrthoGraphicCamera)
            {
                X1 = OrthoMainCamera.Position.X - cameraFocus.X;
                Y1 = OrthoMainCamera.Position.Y - cameraFocus.Y;
            }
            else
            {
                X1 = PerspMainCamera.Position.X - cameraFocus.X;
                Y1 = PerspMainCamera.Position.Y - cameraFocus.Y;
            }
            
            
            double ce1 = -angle;
            double ce2 = -angle / 2;
            double ce3;
            double C = Math.Pow((Math.Pow(X1, 2) + Math.Pow(Y1, 2)), 0.5);


            ce3 = Math.Atan(Math.Abs(X1 / Y1)) * 180 / Math.PI;

            double ce4;
            double Xd = 1, Yd = 1;
           // double k = 0;
            //double ce4 = 90 - (3/2*ce1 + Math.Atan(Math.Abs(X1/Y1)) * 180/Math.PI);
            if (X1 >= 0)
            {
                if (Y1 >= 0)
                {
                    if ((ce3 + ce1) < 90)// 1
                    {
             //           k = 1;
                        ce4 = 90 - (ce3 + ce1);
                    }
                    else// 1-2
                    {
               //         k = 12;
                        ce4 = -90 + (ce3 + ce1);
                        Yd = -1;
                    }
                }
                else
                {
                    if (((90 - ce3) + ce1) < 90)// 2
                    {
                 //       k = 2;
                        ce4 = 90 - (ce3 - ce1);
                        Yd = -1;
                    }
                    else// 2-3
                    {
                   //     k = 23;
                        ce4 = 90 + (ce3 - ce1);
                        Xd = -1;
                        Yd = -1;
                    }
                }
            }
            else
            {
                if (Y1 >= 0)
                {
                    if (((90 - ce3) + ce1) < 90)// 4
                    {
                    //    k = 4;
                        ce4 = 90 - (ce3 - ce1);
                        Xd = -1;
                    }
                    else// 4-1
                    {
                    //    k = 41;
                        ce4 = 90 + (ce3 - ce1);
                    }
                }
                else
                {
                    if ((ce3 + ce1) < 90)// 3
                    {
                    //    k = 3;
                        ce4 = 90 - (ce3 + ce1);
                        Xd = -1;
                        Yd = -1;
                    }
                    else// 3-4
                    {
                    //    k = 34;
                        ce4 = -90 + (ce3 + ce1);
                        Xd = -1;
                    }
                }
            }

            //double ce4 = 90 - (ce1 + ce3);

            double X2 = Xd * C * Math.Cos(ce4 * Math.PI / 180);
            double Y2 = Yd * C * Math.Sin(ce4 * Math.PI / 180);


            if (IsOrthoGraphicCamera)
            {
                Point3D tempPosition1 = new Point3D(cameraFocus.X + X2, cameraFocus.Y + Y2, OrthoMainCamera.Position.Z);
                OrthoMainCamera.Position = tempPosition1;

                //change direction
                double VX1 = cameraFocus.X - OrthoMainCamera.Position.X;
                double VY1 = cameraFocus.Y - OrthoMainCamera.Position.Y;
                double VZ1 = cameraFocus.Z - OrthoMainCamera.Position.Z;
                Vector3D tempVector3D1 = new Vector3D(VX1, VY1, VZ1);
                OrthoMainCamera.LookDirection = tempVector3D1;
            }
            else
            {
                Point3D tempPosition = new Point3D(cameraFocus.X + X2, cameraFocus.Y + Y2, PerspMainCamera.Position.Z);
                PerspMainCamera.Position = tempPosition;

                //change direction
                double VX = cameraFocus.X - PerspMainCamera.Position.X;
                double VY = cameraFocus.Y - PerspMainCamera.Position.Y;
                double VZ = cameraFocus.Z - PerspMainCamera.Position.Z;
                Vector3D tempVector3D = new Vector3D(VX, VY, VZ);
                PerspMainCamera.LookDirection = tempVector3D;
            }

        }

        // 현재 틸트 -> 카메라 앵글 각도 -. 위, 아래 
        public double getCurrentTiltAngle()
        {
            double X1, Y1, Z1;
            if (IsOrthoGraphicCamera)
            {
                X1 = OrthoMainCamera.Position.X - cameraFocus.X;
                Y1 = OrthoMainCamera.Position.Y - cameraFocus.Y;
                Z1 = OrthoMainCamera.Position.Z - cameraFocus.Z;
            }
            else
            {
                X1 = PerspMainCamera.Position.X - cameraFocus.X;
                Y1 = PerspMainCamera.Position.Y - cameraFocus.Y;
                Z1 = PerspMainCamera.Position.Z - cameraFocus.Z;
            }
            

            double C = Sqrt(Pow(X1) + Pow(Y1) + Pow(Z1));

            //TZ coordinate system
            double T1 = Sqrt(Pow(X1) + Pow(Y1));
            double theta1 = ACos(Abs(T1 / C));

            return theta1;
        }
        // 앵글에서 틸트 
        public Point3D tiltbyAngle(double tiltValue)
        {
            double thetaA = tiltValue;

            double X1, Y1, Z1;
            if (IsOrthoGraphicCamera)
            {
                X1 = OrthoMainCamera.Position.X - cameraFocus.X;
                Y1 = OrthoMainCamera.Position.Y - cameraFocus.Y;
                Z1 = OrthoMainCamera.Position.Z - cameraFocus.Z;
            }
            else
            {
                X1 = PerspMainCamera.Position.X - cameraFocus.X;
                Y1 = PerspMainCamera.Position.Y - cameraFocus.Y;
                Z1 = PerspMainCamera.Position.Z - cameraFocus.Z;
            }
            

            double C = Sqrt(Pow(X1) + Pow(Y1) + Pow(Z1));

            //TZ coordinate system
            double T1 = Sqrt(Pow(X1) + Pow(Y1));
            double theta1 = ACos(Abs(T1 / C));
            double theta2 = 90 - (theta1 + thetaA);

            //Console.WriteLine("=============================");
            //Console.WriteLine("X1={0},Y1={1},Z1={2}", (int)X1, (int)Y1, (int)Z1);
            //Console.WriteLine("theta1={0},theta2={1},thetaA={2}", (int)theta1, (int)theta2, (int)thetaA);

            if (theta1 + thetaA >= 90)
            {
                if (IsOrthoGraphicCamera)
                    return OrthoMainCamera.Position;
                else
                    return PerspMainCamera.Position;
            }
            if (theta1 + thetaA < 0)
            {
                if (IsOrthoGraphicCamera)
                    return OrthoMainCamera.Position;
                else
                    return PerspMainCamera.Position;
            }
            //theta1 + thetaA < 90 allways
            double Xdir = 1, Ydir = 1;
            //int k = 0;
            if (X1 < 0) Xdir = -1;
            if (Y1 < 0) Ydir = -1;

            double T2 = C * Sin(theta2);
            double Z2 = C * Cos(theta2);

            //XY coordinate system
            double thetaXY1 = Atan(Abs(X1 / Y1));
            double X2 = T2 * Sin(thetaXY1) * Xdir;
            double Y2 = T2 * Cos(thetaXY1) * Ydir;


            //Console.WriteLine("X2={0},Y2={1},Z2={2}", (int)X2, (int)Y2, (int)Z2);
            //Console.WriteLine("theta1+thetaA={0}", theta1 + thetaA);

            Point3D targetPosition = new Point3D();
            targetPosition.X = cameraFocus.X + X2;
            targetPosition.Y = cameraFocus.Y + Y2;
            targetPosition.Z = cameraFocus.Z + Z2;


            Vector3D targetLookDirection = new Vector3D();
            targetLookDirection.X = cameraFocus.X - targetPosition.X;
            targetLookDirection.Y = cameraFocus.Y - targetPosition.Y;
            targetLookDirection.Z = cameraFocus.Z - targetPosition.Z;

            if (IsOrthoGraphicCamera)
            {
                OrthoMainCamera.Position = targetPosition;
                OrthoMainCamera.LookDirection = targetLookDirection;
            }
            else
            {
                PerspMainCamera.Position = targetPosition;
                PerspMainCamera.LookDirection = targetLookDirection;
            }
            return targetPosition;
        }
        #region // Math function=============================
        
        private double Abs(double number)
        {
            return Math.Abs(number);
        }

        private double Sin(double angle)
        {
            return Math.Sin(AngleToRadian(angle));
        }

        private double Cos(double angle)
        {
            return Math.Cos(AngleToRadian(angle));
        }

        private double Atan(double number)
        {
            return RadianToAngle(Math.Atan(number));
        }

        private double ACos(double number)
        {
            return RadianToAngle(Math.Acos(number));
        }


        private double RadianToAngle(double radian)
        {
            return radian * 180 / Math.PI;
        }

        private double AngleToRadian(double angle)
        {
            return angle * Math.PI / 180;
        }


        private double Pow(double number)
        {
            return Math.Pow(number, 2);
        }

        private double Sqrt(double number)
        {
            return Math.Pow(number, 0.5);
        }
        #endregion
    }
}
