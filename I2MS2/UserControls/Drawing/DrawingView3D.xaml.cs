#define USE_ORTHOGRAPHIC_CAMERA

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

using I2MS2.Library.Drawing;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using I2MS2.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Media.Media3D;
using WebApi.Models;
using I2MS2.Pages;
using I2MS2.Library;

namespace I2MS2.UserControls.Drawing
{
    // 3D 처리 라이브러리 
    /// <summary>
    /// DrawingView3D.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    enum viewContorllerSelect_t
    {
        NONE,
        JOYSTICK,
        JOYPAD,
        TILTUP,
        TILTDOWN
    };

    public partial class CameraData
    {
        public Double Width { get; set; }
        public Point3D Position { get; set; }
        public Vector3D LookDirection { get; set; }
    }

    
    public partial class DrawingView3D : UserControl
    {
        CameraData def_cam_data;            // 초기 카메라의 포지션, 디렉션 
        Drawing3D drawer3d;                 // 3D 드로우
        DrawingDataManager drawDataMgr;     // 드로잉 데이터 변환 처리 

        // view Controller variable
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

        Dictionary<int, WallDraw> w_dic = new Dictionary<int, WallDraw>();
        Dictionary<int, WallCornerDraw> wc_dic = new Dictionary<int, WallCornerDraw>();

        // 초기화
        public DrawingView3D()
        {
            InitializeComponent();
            drawer3d = new Drawing3D(_mainViewPort3D, _canvasDisp);
            drawDataMgr = new DrawingDataManager();
            
            initCameraControl();
            initJoyStick();
            initGradient();
            initZoomSlider();
        }

        Drawing3DCamera camCtl;
        //Double cameraViewMax = 5000;
        //Double cameraViewMin = 10;

        Double cameraViewMax = 1300;    // firstOrthoGraphicCameraWidth 의 맥스 
        Double cameraViewMin = 50;      // 민 
        Double firstOrthoGraphicCameraWidth = 1300; //카메라의 바라보는 넓이 
        Double BottomLevelUnit = 100;

        Point cameraFieldLimitMax = new Point(2000, 2000);
        Point cameraFieldLimitMin = new Point(-2000, -2000);


        //부모 클래스 내부의 이벤트 호출 연결 고리
        public delegate void camMoveEndHandler(object obj);
        public event camMoveEndHandler camMoveEndEventToAssetView;

        // 카메라 초기화 처리 
        private void initCameraControl()
        {
            view3D_moveVector = new Vector(0, 0);
            firstOrthoGraphicCameraWidth = mainCamera.Width;
            camCtl = new Drawing3DCamera(mainCamera, cameraViewMax, cameraViewMin, BottomLevelUnit, cameraFieldLimitMax, cameraFieldLimitMin, true);
            camCtl.camMoveEndEvent += new  Drawing3DCamera.camMoveEndEventHandler(camMoveEnd);
            
            //camCtl.rotateByAngle(20);
            camCtl.rotateByAngle(-90); // 기준 
            
            def_cam_data = new CameraData();
            def_cam_data.Width = mainCamera.Width;
            def_cam_data.Position = mainCamera.Position;
            def_cam_data.LookDirection = mainCamera.LookDirection;
           
        }

        // 마우스 처리 
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


        private void initZoomSlider()
        {
            zoomSlider.Value = 100 - ((firstOrthoGraphicCameraWidth - cameraViewMin) / (cameraViewMax - cameraViewMin) * 100);
            mainCamera.Width = firstOrthoGraphicCameraWidth;
        }
#if false

        public Boolean openDrawingFile(string path)
        {
            drawer3d.removeAllWall();

            if (File.Exists(path))
            {
                BinaryFormatter openformat = new BinaryFormatter();
                FileStream openStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                List<WallDraw>[] open_w_list = new List<WallDraw>[4];

                open_w_list = (List<WallDraw>[])openformat.Deserialize(openStream);
                int static_num = 0;
                for (int i = 0; i < 4; i++)
                {
                    foreach (var w in open_w_list[i])
                    {
                        w.id = static_num++;
                        addWall(w);

                        //addWall(makeWallVM(w, static_num++));
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
#else
        // 3D 파일 오픈 벽과 코너 그리기 
        public Boolean openDrawingFile(string path)
        {
           
            drawer3d.removeAllWall();
            drawer3d.removeAllWallCorner();
//            clearItemAll();                // 초기화 오류 romee/1/20           2016.06.21 에러 
            try
                {
                    BinaryFormatter openformat = new BinaryFormatter();
                    FileStream openStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                    List<WallDraw>[] open_w_list = new List<WallDraw>[4];
                    
                    open_w_list = (List<WallDraw>[])openformat.Deserialize(openStream);
                    
                    for(int i=0;i<4;i++)
                    {
                        foreach(var w in open_w_list[i])
                        {
                            addWall(w);
                        }
                    }

                    try
                    {
                        List<WallCornerDraw>[] open_wc_list = (List<WallCornerDraw>[])openformat.Deserialize(openStream);
                   
                        for(int i=0;i<4;i++)
                        {
                            foreach(var wc in open_wc_list[i])
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
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}", e.Message);
                MessageBox.Show("Please check image file name. (openDrawingFile)");
            }

            return true;
        }
#endif
#if false
        private WallDraw makeWallVMDataFor3d(WallDraw w)
        {
            return new WallDraw()
            {
                id = w.id,
                start_p = drawDataMgr.get3DPoint_FromVMPoint(w.start_p),
                start_pA = drawDataMgr.get3DPoint_FromVMPoint(w.start_pA),
                start_pB = drawDataMgr.get3DPoint_FromVMPoint(w.start_pB),
                end_p = drawDataMgr.get3DPoint_FromVMPoint(w.end_p),
                end_pA = drawDataMgr.get3DPoint_FromVMPoint(w.end_pA),
                end_pB = drawDataMgr.get3DPoint_FromVMPoint(w.end_pB),
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
        
#endif




#if false
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
#endif
        // 자산 그리기
        public void addAsset(int id, AssetTreeType type, Point db_p)
        {
            Point p = drawDataMgr.get3DPoint_FromVMPoint(db_p);
            Size s = drawDataMgr.get3DSize_FromVMSize(new Size(g.ASSET_SIZE_WIDTH, g.ASSET_SIZE_HEIGHT));
            Double h = drawDataMgr.get3DValue_FromVMValue(g.ASSET_HEIGHT);
            String img_path = null;

            Color c;
            switch(type)
            {
                case AssetTreeType.FacePlate:
                    //img_path = "C:/Users/Administrator/Source/Workspaces/I2MS2/I2MS2/I2MS2/Icons/fp_64.png";
                    img_path = "/I2MS2;component/Icons/fp_16.png";
                    c = g.FP_COLOR;
                    break;
                case AssetTreeType.MutoaBox:
                    //img_path = "C:/Users/Administrator/Source/Workspaces/I2MS2/I2MS2/I2MS2/Icons/mb_64.png";
                    img_path = "/I2MS2;component/Icons/mb_16.png";
                    c = g.MB_COLOR;
                    break;
                case AssetTreeType.ConsolidationPoint:
                    //img_path = "C:/Users/Administrator/Source/Workspaces/I2MS2/I2MS2/I2MS2/Icons/cp_64.png";
                    img_path = "/I2MS2;component/Icons/cp_16.png";
                    c = g.CP_COLOR;
                    break;
               
                default:
                    c = g.FP_COLOR;
                    break;
            }

            //if (!File.Exists(img_path))
            //    img_path = null;
            drawer3d.AddAsset(id, c, p, s, h, img_path);
        }

        public void selectAsset(int asset_id)
        {
            drawer3d.selectAsset(asset_id);
        }

        public void releaseAsset(int asset_id)
        {
            drawer3d.releaseAsset(asset_id);
        }

#if false
        public void addUserPort(int id, int number, Point db_p, Point parent_p, Boolean is_with_pc)
        {
            Point p = drawDataMgr.get3DPoint_FromVMPoint(db_p);
            Double r = drawDataMgr.get3DValue_FromVMValue(g.USERPORT_RADIUS);
            Double h = drawDataMgr.get3DValue_FromVMValue(g.USERPORT_HEIGHT);
            Point p_p = drawDataMgr.get3DPoint_FromVMPoint(parent_p);
            Size p_s = drawDataMgr.get3DSize_FromVMSize(new Size(g.ASSET_SIZE_WIDTH, g.ASSET_SIZE_HEIGHT));
            drawer3d.AddUserPort(id, number, p, r, h, p_p, p_s);
        } 
#endif
        // 유저 포트 그리기 
        // 2디 좌표를 3디로 변환후 그리기 
        public void addUserPort(int id, int number, Point db_p, Point parent_p, String img_path, List<AssetTreeVM> child_list)
        {
            Point p = drawDataMgr.get3DPoint_FromVMPoint(db_p);
            Double r = drawDataMgr.get3DValue_FromVMValue(g.USERPORT_RADIUS);
            Double h = drawDataMgr.get3DValue_FromVMValue(g.USERPORT_HEIGHT);
            Point p_p = drawDataMgr.get3DPoint_FromVMPoint(parent_p);
            Size p_s = drawDataMgr.get3DSize_FromVMSize(new Size(g.ASSET_SIZE_WIDTH, g.ASSET_SIZE_HEIGHT));


            drawer3d.AddUserPort(id, number, p, r, h, p_p, p_s, img_path, child_list);
        }

        public void addRack(rack rd)
        {
            Point p = drawDataMgr.get3DPoint_FromVMPoint(new Point(rd.pos_x ?? 0 ,rd.pos_y ?? 0));
            Size s = drawDataMgr.get3DSize_FromVMSize(new Size(g.RACK_SIZE_WIDTH, g.RACK_SIZE_HEIGHT));
            Double h = drawDataMgr.get3DValue_FromVMValue(g.RACK_HEIGHT);
            Color c = g.RACK_COLOR;
            drawer3d.addRack(p,s,h,c, rd.rack_id);
        //    drawer3d.drawRackInfo(rd.rack_id, rd.rack_name); 
        }

        public void selectRack(int id)
        {
            drawer3d.selectRack(id);
        }

        public void changeRackBrush(int id, SolidColorBrush br)
        {
            drawer3d.changeRackBrush(id, br);
        }

        public void releaseRack(int id)
        {
            drawer3d.releaseRack(id);
        }
        //public void clearRackAll()
        //{
        //    drawer3d.removeRackAll();
        //}

        // romee 2016-04-27 지우고 새로 그려야 함 
        public void clearItemAll()
        {
            try
            { 
                w_dic.Clear();
                wc_dic.Clear();
                drawer3d.removeItemAll();
            }
            catch(Exception e)
            {
            }
        }

        public void drawRoomInfo(room rm)
        {
            Point start_p = new Point();
            start_p.X = drawDataMgr.get3DValue_FromVMValue((Double)(rm.square_x1 ?? 0));
            start_p.Y = drawDataMgr.get3DValue_FromVMValue((Double)(rm.square_y1 ?? 0));

            Point end_p = new Point();
            end_p.X = drawDataMgr.get3DValue_FromVMValue((Double)(rm.square_x2 ?? 0));
            end_p.Y = drawDataMgr.get3DValue_FromVMValue((Double)(rm.square_y2 ?? 0));

            Point center_p = new Point((start_p.X + end_p.X) / 2, (start_p.Y + end_p.Y) / 2);
            Point ct_p = new Point();
            ct_p.X = drawDataMgr.get3DValue_FromVMValue((Double)(rm.flag_x ?? 0));
            ct_p.Y = drawDataMgr.get3DValue_FromVMValue((Double)(rm.flag_y ?? 0));

            drawer3d.addRoomInfoView(rm.room_id, rm.room_name, ct_p);
        }

        
        public void clearRoomInfoAll()
        {
            drawer3d.removeRoomInfoViewAll();
        }

        public void drawRackInfo(rack rk)
        {
            Point p = new Point();
            p.X = drawDataMgr.get3DValue_FromVMValue(((Double)(rk.pos_x ?? 0) + g.RACK_SIZE_WIDTH / 2));
            p.Y = drawDataMgr.get3DValue_FromVMValue(((Double)(rk.pos_y ?? 0) + g.RACK_SIZE_HEIGHT / 2));
            Double h = drawDataMgr.get3DValue_FromVMValue(g.RACK_HEIGHT);

            drawer3d.addRackInfoView(rk.rack_id, rk.rack_name, p, h );
        }

        public void clearRackInfoAll()
        {
            drawer3d.removeRackInfoViewAll();
        }

        public void drawAssetInfo(asset ast, AssetTreeType type)
        {
            Point p = new Point();
            p.X = drawDataMgr.get3DValue_FromVMValue(((Double)(ast.pos_x ?? 0) + g.ASSET_SIZE_WIDTH / 2));
            p.Y = drawDataMgr.get3DValue_FromVMValue(((Double)(ast.pos_y ?? 0) + g.ASSET_SIZE_HEIGHT / 2));
            Double h = drawDataMgr.get3DValue_FromVMValue(g.ASSET_HEIGHT);

            drawer3d.addAssetInfoView(ast.asset_id, ast.asset_name,type, p, h);
        }

        public void clearAssetInfoAll()
        {
            drawer3d.removeAssetInfoViewAll();
        }

        public void drawUserPortInfo(String p_name, user_port_layout up)
        {
            Point p = new Point();
            //p.X = drawDataMgr.get3DValue_FromVMValue(((Double)(up.pos_x ?? 0) + g.USERPORT_RADIUS));
            //p.Y = drawDataMgr.get3DValue_FromVMValue(((Double)(up.pos_y ?? 0) + g.USERPORT_RADIUS));
            p.X = drawDataMgr.get3DValue_FromVMValue((Double)(up.pos_x ?? 0));
            p.Y = drawDataMgr.get3DValue_FromVMValue((Double)(up.pos_y ?? 0) + g.USERPORT_RADIUS);
            
            Double h = drawDataMgr.get3DValue_FromVMValue(g.USERPORT_HEIGHT);
            //h = 0;
            drawer3d.addUserPortInfoView(up.user_port_layout_id, string.Format("{0}-{1}", p_name, up.port_no), p, h);
            //drawer3d.addUserPortInfoView(up.user_port_layout_id, string.Format("{0}",up.port_no), p, h);
        }

        public void clearUserPortInfoAll()
        {
            drawer3d.removeUserPortInfoViewAll();
        }


        #region Wall Manage( add,remove,selected......)
        public void addWall(WallDraw w)
        {
            if (w_dic.ContainsKey(w.id)) return;
                
            w_dic.Add(w.id, w);
            WallDraw w_3d = drawDataMgr.makeWallVMDataFor3d(w);
            drawer3d.addWall(w_3d);
        }

        public void selectWall(WallDraw w_vm)
        {
            WallDraw w_vm_3d = drawDataMgr.makeWallVMDataFor3d(w_vm);
            drawer3d.selectWall(w_vm_3d);
        }

        public void releaseWall(WallDraw w_vm)
        {
            WallDraw w_vm_3d = drawDataMgr.makeWallVMDataFor3d(w_vm);
            modifyWall(w_vm_3d);
        }

        public void modifyWall(WallDraw w_vm)
        {
            WallDraw w_vm_3d = drawDataMgr.makeWallVMDataFor3d(w_vm);
            drawer3d.reDrawWall(w_vm_3d);
        }
        public void removeWallByNum(int id)
        {
            if (!w_dic.ContainsKey(id)) return;

            w_dic.Remove(id);
            drawer3d.removeWallbyNum(id);
        }

        public void removeAllWall()
        {
            w_dic.Clear();
            wc_dic.Clear();
            drawer3d.removeAllWall();
            drawer3d.removeAllWallCorner();
        } 
        #endregion


        #region WallCorner Manage
        public void addWallCorner(WallCornerDraw wc)
        {
            if (wc_dic.ContainsKey(wc.id)) return;

            wc_dic.Add(wc.id, wc);
            WallCornerDraw wc2 = drawDataMgr.convertWallCornerDBto3D(wc);
            drawer3d.addWallCorner(wc2);
        }

        public void modifyWallCorner(WallCornerDraw wc)
        {
            WallCornerDraw wc2 = drawDataMgr.convertWallCornerDBto3D(wc);
            drawer3d.modifyWallCorner(wc2);
        }


        public void removeWallCorner(int wc_id)
        {
            if (!wc_dic.ContainsKey(wc_id)) return;

            wc_dic.Remove(wc_id);
            drawer3d.removeWallCornerbyId(wc_id);
        }
        #endregion

        #region Camera Control
  #if NO_ANIM
        public void cameraFocucingRoom(Point center_p, Double width)
        {

            Double ct_x = mainCamera.Position.X + mainCamera.LookDirection.X;
            Double ct_y = mainCamera.Position.Y + mainCamera.LookDirection.Y;

            Vector v = new Vector(center_p.Y - ct_x, center_p.X - ct_y);
           camCtl.movingBy2DMoveWithZoom(v, width);

            
            mainCamera.Width = width;
            firstOrthoGraphicCameraWidth = width;

            initZoomSlider();
#else
        //public void cameraFocusMove(Point center_p, Double width)
        public void cameraFocucingRoom(Point center_p, Double width)
        {
            Double ct_x = mainCamera.Position.X + mainCamera.LookDirection.X;
            Double ct_y = mainCamera.Position.Y + mainCamera.LookDirection.Y;

            Vector v = new Vector(center_p.Y - ct_x, center_p.X - ct_y);

            camCtl.movingBy2DMoveAnimWithZoom(v, width, 1000);
#endif
        }
#if NO_ANIM
        public void cameraFocuingFloor()
        {
            //Point cam_ct_p = camCtl.getCameraCenter2D();

            Double ct_x = mainCamera.Position.X + mainCamera.LookDirection.X;
            Double ct_y = mainCamera.Position.Y + mainCamera.LookDirection.Y;

            Double def_ct_x = def_cam_data.Position.X + def_cam_data.LookDirection.X;
            Double def_ct_y = def_cam_data.Position.Y + def_cam_data.LookDirection.Y;


            Vector v = new Vector(def_ct_x - ct_x, def_ct_y - ct_y);

            
            camCtl.movingBy2DMoveWithZoom(v, def_cam_data.Width);

            mainCamera.Width = def_cam_data.Width;
            firstOrthoGraphicCameraWidth = def_cam_data.Width;


            initZoomSlider();
            //mainCamera.Position = def_cam_data.Position;
            //mainCamera.Width = def_cam_data.Width;
#else
        public void cameraFocuingFloor()
        {
            Double ct_x = mainCamera.Position.X + mainCamera.LookDirection.X;
            Double ct_y = mainCamera.Position.Y + mainCamera.LookDirection.Y;

            Double def_ct_x = def_cam_data.Position.X + def_cam_data.LookDirection.X;
            Double def_ct_y = def_cam_data.Position.Y + def_cam_data.LookDirection.Y;

            
            Vector v = new Vector(def_ct_x - ct_x, def_ct_y - ct_y);


            camCtl.movingBy2DMoveAnimWithZoom(v, def_cam_data.Width, 1000);
#endif
        }


        public void camMoveEnd(object obj)
        {
            camMoveEndEventToAssetView(obj);
            zoomSlider.Value = 100 - ((mainCamera.Width - cameraViewMin) / (cameraViewMax - cameraViewMin) * 100);

        }



        #endregion


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


        private void _gridMain_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                zoomSlider.Value += 2;
            else
                zoomSlider.Value -= 2;
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
            tiltUpTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
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
            tiltDownTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
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

   

        //================END Zoom slider Event==========================================


        #endregion


    } 
        
}
