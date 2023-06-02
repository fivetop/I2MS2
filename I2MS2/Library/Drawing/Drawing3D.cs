#define NO_ALPHA_FL

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

using I2MS2.Library;
using I2MS2.UserControls;
using I2MS2.UserControls.Drawing;
using I2MS2.Models;
using _3DTools;
using System.Windows.Media.Imaging;
using System.IO;
using I2MS2.Animation;

namespace I2MS2.Library.Drawing
{
    // 3D 자산 그리기, 텍스트 그리기  
    public class Drawing3D
    {
       //DrawingDataManager drawDataMgr;

        Dictionary<int, ModelVisual3D> wall_m3d_dic = new Dictionary<int, ModelVisual3D>();         // 벽
        Dictionary<int, ModelVisual3D> wallCorner_m3d_dic = new Dictionary<int, ModelVisual3D>();   // 벽 코너
        Dictionary<int, ModelVisual3D> room_m3d_dic = new Dictionary<int, ModelVisual3D>();         // 룸
        Dictionary<int, ModelVisual3D> rack_m3d_dic = new Dictionary<int, ModelVisual3D>();         // 랙
        Dictionary<int, ModelVisual3D> ast_m3d_dic = new Dictionary<int, ModelVisual3D>();          // 자산
        Dictionary<int, ModelVisual3D> pc_m3d_dic = new Dictionary<int, ModelVisual3D>();           // 단말
        Dictionary<int, ModelVisual3D> fl_m3d_dic = new Dictionary<int, ModelVisual3D>();           // 층
        Dictionary<int, ModelVisual3D> up_m3d_dic = new Dictionary<int, ModelVisual3D>();           // 아울렛 유저포트
        Dictionary<int, ModelVisual3D> upln_m3d_dic = new Dictionary<int, ModelVisual3D>();         // 아울렛 유저포트 라인

        //List<DrawingObjectInfoView> room_doiv_list = new List<DrawingObjectInfoView>();
        Dictionary<int, DrawingObjectInfoView> room_info_dic = new Dictionary<int, DrawingObjectInfoView>(); //글씨
        Dictionary<int, DrawingObjectInfoView> rack_info_dic = new Dictionary<int, DrawingObjectInfoView>();
        Dictionary<int, DrawingObjectInfoView> ast_info_dic = new Dictionary<int, DrawingObjectInfoView>();
        Dictionary<int, DrawingObjectInfoListView> up_info_dic = new Dictionary<int, DrawingObjectInfoListView>();

        Dictionary<int, ModelVisual3D> flCube_m3d_dic = new Dictionary<int, ModelVisual3D>(); //층선택용 큐브

        Dictionary<int, List<AssetTreeVM>> up_infoViewData_dic = new Dictionary<int, List<AssetTreeVM>>(); // 다중 단말 처리용 

        SimpleAnimation animMgr;

        ModelVisual3D selected_model;

        Viewport3D viewport;
        Canvas canvas;

       
        // 초기화 
        public Drawing3D(Viewport3D _viewport, Canvas _canvas)
        {
            viewport = _viewport;
            canvas = _canvas;
            animMgr = new SimpleAnimation();

            init3DModel();
        }

        // 초기화
        private void init3DModel()
        {
            DirectionalLight directionalLight = new DirectionalLight();
            directionalLight.Color = Colors.Gray;

            Vector3D tempVector = new Vector3D(-10, -10, -10);
            directionalLight.Direction = tempVector;

            Model3DGroup model_group = new Model3DGroup();
            model_group.Children.Add(directionalLight);

            ModelVisual3D model_visual = new ModelVisual3D();
            model_visual.Content = model_group;
            viewport.Children.Add(model_visual);
        }
        
        #region muliFloorView List
        // 층 선택용 큐브 만들기 
        public void AddFloorCube(int fl_no, string fl_name)
        {
            ModelVisual3D tempModel = new ModelVisual3D();
            String str;

            str = string.Format("{0}", fl_name); // Romee 2018.03.13 층 이름출력 변경
/*
            if (fl_no <= 0)  // Romee 2015.08.17  
            { 
                str = string.Format("B{0}F", Math.Abs(fl_no - 1));
            }
            else
            {
                str = string.Format("{0}F", fl_no);
            }
*/
            //tempModel = CreateCube(new Point(0,0), new Size(1000, 1000), 180, Colors.DarkGray, fl_no * 200);
            tempModel = CreateCubeWithSideText(new Point(0,0), new Size(1000, 1000), 180, Brushes.DarkGray, fl_no * 200, str);

            if (flCube_m3d_dic.ContainsKey(fl_no)) return;
            flCube_m3d_dic.Add(fl_no, tempModel);
            viewport.Children.Add(tempModel);
        }
        // 지우기 
        public void removeFloorCubeAll()
        {
            foreach(var _flCube in flCube_m3d_dic)
            {
                ModelVisual3D tempModel = (ModelVisual3D)_flCube.Value;
                viewport.Children.Remove(tempModel);
            }
            flCube_m3d_dic.Clear();
            
        }
        // 
        public int getFloorCubeNo(ModelVisual3D m3d)
        {
            foreach(var fl in flCube_m3d_dic)
            {
                if (fl.Value == m3d)
                    return fl.Key;
            }
            return -200;
        }
        // 층번호로 큐브 얻기 
        public ModelVisual3D getFloorCube(int fl_no)
        {
            if(flCube_m3d_dic.ContainsKey(fl_no))
                return flCube_m3d_dic[fl_no];
            else
                return null;
        }

        // 큐브 색상 변경 
        public void changeColorFloorCube(ModelVisual3D m3d, Brush brush)
        {
            Model3DGroup mg = (Model3DGroup)m3d.Content;//12개
            foreach (var model in mg.Children)
            {
                //     Console.WriteLine("test");
                if (model is GeometryModel3D)
                {
                    GeometryModel3D geo_model = (GeometryModel3D)model;
                    geo_model.Material = new DiffuseMaterial(brush);
                }
            }
        }
        #endregion

        #region UserPort Control

#if false
        public void AddUserPort(int id, int number, Point p, Double radius, Double h, Point parent_p, Size parent_s)
        {
            ModelVisual3D tempModel = CreateCylinderWithText(p, radius, 20, h, new SolidColorBrush(USERPORT_COLOR), string.Format("{0}", number));

            viewport.Children.Add(tempModel);
            up_m3d_dic.Add(id, tempModel);

            Point3D p0;
            Point3D p1;
            Point3D p2;
            Point3D p3;

            Point p_ct_p = new Point(parent_p.X + parent_s.Width / 2, parent_p.Y + parent_s.Height / 2);
            Point ct_p = new Point(p.X + radius, p.Y + radius);
            getPointForLine(p_ct_p, ct_p, 0.5, out p0, out p1, out p2, out p3);
            Brush br = new SolidColorBrush(USERPORT_LINE_COLOR);
            ModelVisual3D lineTempModel = CreateLine2(p0, p1, p2, p3, br);
            viewport.Children.Add(lineTempModel);
            upln_m3d_dic.Add(id, lineTempModel);

        }
        
#endif
        // 유저 포트 , parent_p 라인 긋기 처리 
        public void AddUserPort(int id, int number, Point p, Double radius, Double h, Point parent_p, Size parent_s, String img_path, List<AssetTreeVM> child_list)
        {
            //ModelVisual3D tempModel = CreateCylinderWithImg(p, radius, 20, h, new SolidColorBrush(g.USERPORT_COLOR), img_path);

            String num_str = string.Format("{0}",number);
            ModelVisual3D tempModel = CreateCylinderWithText(p, radius, 20, h, new SolidColorBrush(g.USERPORT_COLOR), num_str);

            if (up_m3d_dic.ContainsKey(id)) return;
            viewport.Children.Add(tempModel);
            up_m3d_dic.Add(id, tempModel);

            if (up_infoViewData_dic.ContainsKey(id)) return;
            up_infoViewData_dic.Add(id, child_list); // 원래는 한개만 처리> 단말이 여러개일 경우 처리를 위함 

            Point3D p0;
            Point3D p1;
            Point3D p2;
            Point3D p3;

            Point p_ct_p = new Point(parent_p.X + parent_s.Width / 2, parent_p.Y + parent_s.Height / 2);
            Point ct_p = new Point(p.X + radius, p.Y + radius);
            getPointForLine(p_ct_p, ct_p, 0.2, out p0, out p1, out p2, out p3); // 라인 
            Brush br = new SolidColorBrush(g.USERPORT_LINE_COLOR);
            ModelVisual3D lineTempModel = CreateLine2(p0, p1, p2, p3, br);
            viewport.Children.Add(lineTempModel); 
            if (upln_m3d_dic.ContainsKey(id)) return; // 라인에 대함 
            upln_m3d_dic.Add(id, lineTempModel);
        } 

        #endregion

        #region AssetControl
        // 자산 
        public void AddAsset(int id, Color c, Point p, Size s, Double h, String img_path)
        {
            //Size s = new Size(10, 10);
            //Double h = 5;

            Color c2 = Color.FromArgb(0xFF,0x87,0xAD,0xCF);
            ModelVisual3D tempModel = CreateCubeWithTopImage(p, s, h, new SolidColorBrush(c), img_path);

            if (ast_m3d_dic.ContainsKey(id)) return;
            viewport.Children.Add(tempModel);
            ast_m3d_dic.Add(id, tempModel);
        }

        DiffuseMaterial select_diff;
        // 자산 선택 
        public void selectAsset(int asset_id)
        {
            ModelVisual3D oldModel;
            try
            {
                oldModel = ast_m3d_dic[asset_id];
            }
            catch(Exception)
            {
                return;
            }
            selected_model = oldModel;
            Model3DGroup oldModelGroup = (Model3DGroup)oldModel.Content;
            foreach (var model in oldModelGroup.Children) // 그룸에서 받아 표면 바꾸기 
            {
           //     Console.WriteLine("test");
                if (model is GeometryModel3D)
                {
                    GeometryModel3D geo_model = (GeometryModel3D)model;
                    DiffuseMaterial mt = (DiffuseMaterial)geo_model.Material;
                    SolidColorBrush from_br = (SolidColorBrush)mt.Brush;
                    SolidColorBrush to_br = new SolidColorBrush(Colors.Red);
                    animMgr.SolidBrushAnimation(from_br, to_br, true, true); // 에니 처리 
                }
            }
        }
        // 선택 2 - 사용안함 
        public void selectAsset2(int asset_id) 
        {
            try
            {
                ModelVisual3D model = ast_m3d_dic[asset_id];
                Point3D p = new Point3D(0,0,0);
                animMgr.scaleAnimation3D(model, 1.1,p, true, true); // 스케일 처리 
            }
            catch (Exception)
            {
                return;
            }
            
        }
        // 해제
        public void releaseAsset2(int asset_id)
        {
            try
            {
                ModelVisual3D model = ast_m3d_dic[asset_id];
                model.Transform = null;
            }
            catch (Exception)
            {
                return;
            }
        }
        // 해제 
        public void releaseAsset(int asset_id)
        {
            try
            {
                ModelVisual3D oldModel = ast_m3d_dic[asset_id];
                selected_model = oldModel;
                Model3DGroup oldModelGroup = (Model3DGroup)oldModel.Content;
                foreach (var model in oldModelGroup.Children)
                {
                    //   Console.WriteLine("test");
                    if (model is GeometryModel3D)
                    {
                        GeometryModel3D geo_model = (GeometryModel3D)model;
                        //geo_model.Material = select_diff;
                        ////geo_model.Material = new DiffuseMaterial(Brushes.SkyBlue);

                        DiffuseMaterial mt = (DiffuseMaterial)geo_model.Material; // 색상 바꾸기 
                        SolidColorBrush br = (SolidColorBrush)mt.Brush;
                        animMgr.SolidBrushAnimationStop(br); // 스탑 처리 
                    }
                }
            }
            catch (Exception) { }
        }

        
        #endregion

        #region Rack Control

        // 랙 생성시 2디 아이콘을 모델3디에 적용 처리 
        public void addRack(Point p, Size s, Double h, Color c, int rack_id)
        {
            //ModelVisual3D tempModel = CreateCube(p, s, h, g.RACK_COLOR, 0);
            String img_path = "/I2MS2;component/Icons/rack_32.png";
            ModelVisual3D tempModel = CreateCubeWithTopImage(p, s, h, new SolidColorBrush(g.RACK_COLOR), img_path); // 이미지

            if (rack_m3d_dic.ContainsKey(rack_id)) return;

            rack_m3d_dic.Add(rack_id, tempModel);
            viewport.Children.Add(tempModel);
        }

        // 랙 선택 
        public void selectRack(int id)
        {
            ModelVisual3D oldModel;
            try
            {
                oldModel = rack_m3d_dic[id];
            }
            catch(Exception)
            {
                return;
            }
            selected_model = oldModel;
            Model3DGroup oldModelGroup = (Model3DGroup)oldModel.Content;
            foreach (var model in oldModelGroup.Children)
            {
           //     Console.WriteLine("test");
                if (model is GeometryModel3D)
                {
                    GeometryModel3D geo_model = (GeometryModel3D)model;
                    DiffuseMaterial mt = (DiffuseMaterial)geo_model.Material;
                    SolidColorBrush from_br = (SolidColorBrush)mt.Brush;
                    SolidColorBrush to_br = new SolidColorBrush(Colors.Blue);  // (Colors.Red);     // romee 5/13 주택은행 색상 변경 요청 
                    animMgr.SolidBrushAnimation(from_br, to_br, true, true);
                }
            }
        }
        // 랙 색상 변경 처리
        public void changeRackBrush(int id,SolidColorBrush br)
        {
            ModelVisual3D oldModel;
            try
            {
                oldModel = rack_m3d_dic[id];
            }
            catch (Exception)
            {
                return;
            }
            selected_model = oldModel;
            Model3DGroup oldModelGroup = (Model3DGroup)oldModel.Content;
            foreach (var model in oldModelGroup.Children)
            {
                //     Console.WriteLine("test");
                if (model is GeometryModel3D)
                {
                    GeometryModel3D geo_model = (GeometryModel3D)model;
                    DiffuseMaterial mt = (DiffuseMaterial)geo_model.Material;
                    mt.Brush =  br;
                }
            }
        }
        // 랙 선택 2 -  스케일 처리 사용안함 
        public void selectRack2(int id)
        {
            ModelVisual3D model = rack_m3d_dic[id];
            Model3DGroup mdg = (Model3DGroup)model.Content;
           
            Point3D p = new Point3D(0, 0, 0);

            p.X = mdg.Bounds.Location.X + mdg.Bounds.SizeX / 2;
            p.Y = mdg.Bounds.Location.Y + mdg.Bounds.SizeY / 2;
            p.Z = mdg.Bounds.Location.Z + mdg.Bounds.SizeZ / 2;
            animMgr.scaleAnimation3D(model, 1.1, p, true, true);
        }

        // 선택 해제
        public void releaseRack2(int id)
        {
            ModelVisual3D model = rack_m3d_dic[id];
            model.Transform = null;
        }
        // 해제
        public void releaseRack(int id)
        {
            try
            {
                ModelVisual3D oldModel = rack_m3d_dic[id];
                selected_model = oldModel;
                Model3DGroup oldModelGroup = (Model3DGroup)oldModel.Content;
                foreach (var model in oldModelGroup.Children)
                {
                    //   Console.WriteLine("test");
                    if (model is GeometryModel3D)
                    {
                        GeometryModel3D geo_model = (GeometryModel3D)model;
                        DiffuseMaterial mt = (DiffuseMaterial)geo_model.Material;
                        SolidColorBrush br = (SolidColorBrush)mt.Brush;
                        animMgr.SolidBrushAnimationStop(br);
                    }
                }
            }
            catch (Exception) { }
        }
        // 모든 아이템 지우기 
        public void removeItemAll()
        {
            foreach (var at in rack_m3d_dic)            // 랙
            {
                viewport.Children.Remove(at.Value);
            }
            rack_m3d_dic.Clear();

            foreach (var at in ast_m3d_dic)             // 자산
            {
                viewport.Children.Remove(at.Value);
            }
            ast_m3d_dic.Clear();
          
            foreach (var at in up_m3d_dic)              // 유저 포트
            {
                viewport.Children.Remove(at.Value);
            }
            up_m3d_dic.Clear();

            foreach (var at in upln_m3d_dic)            // 유저 포트 인포 
            {
                viewport.Children.Remove(at.Value);
            }
            upln_m3d_dic.Clear();
            up_infoViewData_dic.Clear();
        }

        //public void drawRackInfo(int rack_id, String rack_name)
        //{
        //    Point? np = getRack2DPoint(rack_id);
        //    if (np != null)
        //    {
        //        Point p = (Point)np;
        //        TextBlock tx = makeRackInfoTextBlock(rack_id, rack_name, p.X, p.Y);
        //        canvas.Children.Add(tx);
        //    }
        //} 
        #endregion
        // 포인트 얻어오기 랙
        public Point? getRack2DPoint(int rack_id)
        {
            Model3DGroup md = rack_m3d_dic[rack_id].Content as Model3DGroup;
            return Point3DToScreen2D(md.Bounds.Location);
        }
        // 포인트 얻어오기 룸
        public Point? getRoom2DPoint(int room_id)
        {
            Model3DGroup md = room_m3d_dic[room_id].Content as Model3DGroup;
            return Point3DToScreen2D(md.Bounds.Location);
        }
        // 룸 포인트 
        public void addRoomPoint(Point p, int room_id)      
        {
            ModelVisual3D tempModel = new ModelVisual3D();

            tempModel = CreateCube(p, new Size(3, 3), 3, Colors.Red, 0);
            if (room_m3d_dic.ContainsKey(room_id)) return;
            room_m3d_dic.Add(room_id, tempModel);
            viewport.Children.Add(tempModel);
        }

        #region InfoView 2d text
        // 룸 인포 
        public void addRoomInfoView(int room_id, string room_name, Point ct_2d_p)
        {
            Point3D ct_3d_p = new Point3D(ct_2d_p.Y, ct_2d_p.X, 0);
            Point? np = Point3DToScreen2D(ct_3d_p);
            if (np != null)
            {
                Point rm_p = (Point)np;

                if (room_info_dic.ContainsKey(room_id))
                    return;

                DrawingObjectInfoView doiv = new DrawingObjectInfoView(); // 2D 글씨 처리 
                doiv._txtInfo.Text = room_name;
                doiv.Name = string.Format("RoomInfo{0}", room_id);
                doiv.HorizontalAlignment = HorizontalAlignment.Left;
                doiv.VerticalAlignment = VerticalAlignment.Top;
                doiv.point = ct_3d_p;
                Thickness th;
                th = new Thickness(rm_p.X, rm_p.Y, 0, 0);
                doiv.Margin = th;

                if (room_m3d_dic.ContainsKey(room_id)) return;
                room_info_dic.Add(room_id, doiv);
                canvas.Children.Add(doiv);


            }
            else
            {

            }
        }
        public void removeRoomInfoViewAll()
        {
            foreach (var rm_info in room_info_dic)
            {
                canvas.Children.Remove(rm_info.Value);
            }
            room_info_dic.Clear();
        }

        // 랙 인포 
        public void addRackInfoView(int rack_id, string rack_name, Point p, Double h)
        {
            Point3D p_3d = new Point3D(p.Y, p.X, h);
            Point? np = Point3DToScreen2D(p_3d);
            if (np != null)
            {
                if (rack_info_dic.ContainsKey(rack_id))
                    return;

                Point rk_p = (Point)np;
                DrawingObjectInfoView doiv = new DrawingObjectInfoView();
                doiv._txtInfo.Text = rack_name;
                doiv.Name = string.Format("RoomInfo{0}", rack_id);
                doiv.HorizontalAlignment = HorizontalAlignment.Left;
                doiv.VerticalAlignment = VerticalAlignment.Top;
                doiv.point = p_3d;

                //String img_path = "C:/Users/Administrator/Source/Workspaces/I2MS2/I2MS2/I2MS2/Icons/rack_16.png";
                String img_path = "/I2MS2;component/Icons/rack_16.png";
                Boolean exist = File.Exists(img_path);
                doiv._imgType.Source = new BitmapImage(new Uri(img_path, UriKind.RelativeOrAbsolute));
                
                Thickness th;
                th = new Thickness(rk_p.X, rk_p.Y, 0, 0);
                doiv.Margin = th;

                if (rack_info_dic.ContainsKey(rack_id)) return;
                rack_info_dic.Add(rack_id, doiv);
                canvas.Children.Add(doiv);
            }
        }
        public void removeRackInfoViewAll()
        {
            foreach (var rk_info in rack_info_dic)
            {
                canvas.Children.Remove(rk_info.Value);
            }
            rack_info_dic.Clear();
        }

        // 자산 보기                                                   
        public void addAssetInfoView(int id, string name, AssetTreeType type, Point p, Double h)
        {
            Point3D p_3d = new Point3D(p.Y, p.X, h);
            Point? np = Point3DToScreen2D(p_3d);
            if (np != null)
            {
                if (ast_info_dic.ContainsKey(id))
                    return;


                Point tmp_p = (Point)np;
                DrawingObjectInfoView doiv = new DrawingObjectInfoView();
                doiv._txtInfo.Text = name;
                doiv.Name = string.Format("AssetInfo{0}", id);
                doiv.HorizontalAlignment = HorizontalAlignment.Left;
                doiv.VerticalAlignment = VerticalAlignment.Top;
                doiv.point = p_3d;

                String img_path;
                switch (type)
                {
                    case AssetTreeType.FacePlate:
                        img_path = "/I2MS2;component/Icons/fp_16.png";
                        break;
                    case AssetTreeType.MutoaBox:
                        img_path = "/I2MS2;component/Icons/mb_16.png";
                        break;
                    case AssetTreeType.ConsolidationPoint:
                        img_path = "/I2MS2;component/Icons/cp_16.png";
                        break;
                    default:
                        img_path = null;
                        break;
                }

                if(img_path==null)
                    doiv._imgType.Width = 0;
                else
                {
                    if (!File.Exists(img_path))
                    {   //must fix it!!
                        doiv._imgType.Source = new BitmapImage(new Uri(img_path, UriKind.RelativeOrAbsolute));
                    }
                }
                Thickness th;
                try
                { 
                    th = new Thickness(tmp_p.X, tmp_p.Y, 0, 0);
                    doiv.Margin = th;
                }
                catch(Exception)
                {
                    th = new Thickness(0, 0, 0, 0);
                    doiv.Margin = th;
                }

                if (ast_info_dic.ContainsKey(id)) return;
                ast_info_dic.Add(id, doiv);
                canvas.Children.Add(doiv);
            }
        }
        public void removeAssetInfoViewAll()
        {
            foreach (var ast_info in ast_info_dic)
            {
                canvas.Children.Remove(ast_info.Value);
            }
            ast_info_dic.Clear();
        }

        // 사용자 포트 인포
        public void addUserPortInfoView(int id, string name, Point p, Double h)
        {
#if false
            Point3D p_3d = new Point3D(p.Y, p.X, h);
            Point? np = Point3DToScreen2D(p_3d);
            if (np != null)
            {
                Point tmp_p = (Point)np;
                DrawingObjectInfoView doiv = new DrawingObjectInfoView();
                doiv._txtInfo.Text = name;
                doiv.Name = string.Format("UserPortInfo{0}", id);
                doiv.HorizontalAlignment = HorizontalAlignment.Left;
                doiv.VerticalAlignment = VerticalAlignment.Top;
                doiv.point = p_3d;

                String img_path = "/I2MS2;component/Icons/port_16.png";
                if (!File.Exists(img_path))
                {
                    doiv._imgType.Source = new BitmapImage(new Uri(img_path, UriKind.RelativeOrAbsolute));
                }
                Thickness th;
                th = new Thickness(tmp_p.X, tmp_p.Y, 0, 0);
                doiv.Margin = th;

                up_info_dic.Add(id, doiv);
                canvas.Children.Add(doiv);
            } 
#else
            Point3D p_3d = new Point3D(p.Y, p.X, h);
            Point? np = Point3DToScreen2D(p_3d);
            if (np != null)
            {
                if (up_infoViewData_dic.ContainsKey(id) == false) return;
                Point tmp_p = (Point)np;
                DrawingObjectInfoListView doilv = new DrawingObjectInfoListView(); // 단말이 여러개라 ..
                doilv._lvInfo.ItemsSource = up_infoViewData_dic[id];
                
                doilv.point = p_3d;


                Thickness th;
                th = new Thickness(tmp_p.X, tmp_p.Y, 0, 0);
                doilv.Margin = th;

                if (up_info_dic.ContainsKey(id)) 
                    return;
                up_info_dic.Add(id, doilv);
                canvas.Children.Add(doilv);
            } 
#endif
        }
        public void removeUserPortInfoViewAll()
        {
            foreach (var up_info in up_info_dic)
            {
                canvas.Children.Remove(up_info.Value);
            }
            up_info_dic.Clear();
        }

        // 다시 그리기 
        public void reDrawInfoViewAll()
        {
            if (room_info_dic.Count != 0)
            {
                foreach (var rm_info in room_info_dic)
                {
                    Point? np = Point3DToScreen2D(rm_info.Value.point);
                    if (np != null)
                    {
                        Point p = (Point)np;
                        Thickness th = new Thickness(p.X, p.Y, 0, 0);
                        rm_info.Value.Margin = th;
                    }
                }
            }

            if (rack_info_dic.Count != 0)
            {
                foreach (var rk_info in rack_info_dic)
                {
                    Point? np = Point3DToScreen2D(rk_info.Value.point);
                    if (np != null)
                    {
                        Point p = (Point)np;
                        Thickness th = new Thickness(p.X, p.Y, 0, 0);
                        rk_info.Value.Margin = th;
                    }
                }
            }

            if (ast_info_dic.Count != 0)
            {
                foreach (var ast_info in ast_info_dic)
                {
                    Point? np = Point3DToScreen2D(ast_info.Value.point);
                    if (np != null)
                    {
                        Point p = (Point)np;
                        Thickness th = new Thickness(p.X, p.Y, 0, 0);
                        ast_info.Value.Margin = th;
                    }
                }
            }

            if (up_info_dic.Count != 0)
            {
                foreach (var up_info in up_info_dic)
                {
                    Point? np = Point3DToScreen2D(up_info.Value.point);
                    if (np != null)
                    {
                        Point p = (Point)np;
                        Thickness th = new Thickness(p.X, p.Y, 0, 0);
                        up_info.Value.Margin = th;
                    }
                }
            }
        }

        
        #endregion
        // 랙 인포 
        private TextBlock makeRackInfoTextBlock(int id, string str, Double x, Double y)
        {
            Thickness th = new Thickness(x, y, 0, 0);
            TextBlock tx = new TextBlock();
            tx.Margin = th;
            tx.HorizontalAlignment = HorizontalAlignment.Left;
            tx.VerticalAlignment = VerticalAlignment.Top;
            tx.Text = str;
            tx.Name = string.Format("RackInfo{0}", id);
            tx.Foreground = Brushes.White;

            return tx;
        }

        // 3D tools 사용, 3D 뷰포트에서 2D 포인트 가져오기 -> 프로그램에서 사용하기 위해  
        public Point? Point3DToScreen2D(Point3D point3D)
        {
            bool bOK = false;

            // We need a Viewport3DVisual but we only have a Viewport3D.
            Viewport3DVisual vpv = VisualTreeHelper.GetParent(viewport.Children[0]) as Viewport3DVisual;

            // Get the world to viewport transform matrix
            Matrix3D m = MathUtils.TryWorldToViewportTransform(vpv, out bOK);

            if (bOK)
            {
                // Transform the 3D point to 2D
                Point3D transformedPoint = m.Transform(point3D);

                Point screen2DPoint = new Point(transformedPoint.X, transformedPoint.Y);

                //if ((screen2DPoint.X < 0) || (screen2DPoint.Y < 0))
                //    return null;
                if (screen2DPoint.X < 0)
                    screen2DPoint.X = 0;
                else if (screen2DPoint.X > canvas.ActualWidth)
                    screen2DPoint.X = canvas.ActualWidth;
                else if (Double.IsNaN(screen2DPoint.X))
                    return null;

                if (screen2DPoint.Y < 0)
                    screen2DPoint.Y = 0;
                else if (screen2DPoint.Y > canvas.ActualHeight)
                    screen2DPoint.Y = canvas.ActualHeight;
                else if (Double.IsNaN(screen2DPoint.Y))
                    return null;

                return new Nullable<Point>(screen2DPoint);
            }
            else
            {
                return null;
            }
        }

        #region OneFloor // 층별뷰 -> 멀티 플로우 에서만 사용 
        // 멀티 플로우 -> 위아래 이동 
        // 이동전 그리기 -> 이동후 그전 층 삭제  
        public void removeOneFloor(int number)
        {
            try
            {
                ModelVisual3D m3d = fl_m3d_dic[number];
                viewport.Children.Remove(m3d);
                fl_m3d_dic.Remove(number);
            }
            catch (Exception ex) { Console.WriteLine("{0}",ex.Message); }
        }

        // 층 추가 
        public void addOneFloor(Point p, Size s, Double z, Brush brush, List<WallDraw> w_list,List<WallCornerDraw> wc_list, List<RackDrawVM> rk_list, int number, string fl_name)
        {
            if (w_list == null)
                return;
            ModelVisual3D tempModel = new ModelVisual3D();
            Model3DGroup tmpMG = new Model3DGroup();

           //1. draw floor and wall
            tmpMG = CreateRectangle(p, s, z, brush);
            for (int j = 0; j < w_list.Count; j++)
            {
                //if (w_list[j].alpha >= 1)
                {
                    CreateWallInOneModel(tmpMG, w_list[j], z);
                }
            }

            foreach(var wc in wc_list)
            {
                CreateWallCornerInOneModelNoAlpha(tmpMG, wc, z); // 코너 
            }


            //2. draw rack
            foreach(var rk in rk_list)
            {
                CreateCubeOneModel(tmpMG, rk.point, rk.size, rk.height,z ,Brushes.SkyBlue); // 랙
            }
            
            // IPM 추가 필요 romee
            
            tempModel.Content = tmpMG;


            //3. draw name
            Size s1 = new Size(150, 100);

            String str;

            str = string.Format("{0}", fl_name); // Romee 2018.03.13 층 이름출력 변경
            /*
                        if (number <= 0)  // Romee 2015.08.18 지하가 0으로 나오는 관계로 B 삽입 
                        {
                            str = string.Format("B{0}F", Math.Abs(number - 1));
                        }
                        else
                        {
                            str = string.Format("{0}F", number);
                        }
             */
   
            //    p1
            //    p------------------=> Y
            //   /                 /
            //  /                 /
            // /                 /
            ///               p2/
            //-----------------/
            //X

            Double margin = 100;
            Double z0 = z + margin;
            Point p1 = new Point(p.X, p.Y + 100);
            Point p2 = new Point(p.X + 768, p.Y + 1024 -100);
            //
            //tx_p0__________tx_p3 +Y
            //  |            |
            //  |            |
            //  |   1F       |
            //  |            |
            //  |____________|
            //tx_p1           tx_p2
            //  p1
            //-Z

            Point3D tx_p0 = new Point3D(p1.X, p1.Y, z0 + s1.Height);
            Point3D tx_p1 = new Point3D(p1.X, p1.Y, z0 );
            Point3D tx_p2 = new Point3D(p1.X, p1.Y + s1.Width, z0 );
            Point3D tx_p3 = new Point3D(p1.X, p1.Y + s1.Width, z0 + s1.Height);

            Viewport2DVisual3D v2dVisual = make2DTextVisual3D(tx_p0, tx_p1, tx_p2, tx_p3, str, 1000);
            tempModel.Children.Add(v2dVisual);
            //   //반대편에서 바라볼때 나타나도록 그려준다
            //tx_p4__________tx_p7 -Y
            //  |            |
            //  |            |
            //  |   1F       |
            //  |            |
            //  |____________|
            //tx_p5           tx_p6
            //  p2
            //-Z



            Point3D tx_p4 = new Point3D(p2.X, p2.Y, z0 + s1.Height);
            Point3D tx_p5 = new Point3D(p2.X, p2.Y, z0 );
            Point3D tx_p6 = new Point3D(p2.X, p2.Y - s1.Width, z0);
            Point3D tx_p7 = new Point3D(p2.X, p2.Y - s1.Width, z0 + s1.Height);

            Viewport2DVisual3D v2dVisual2 = make2DTextVisual3D(tx_p4, tx_p5, tx_p6, tx_p7, str, 1000);
            tempModel.Children.Add(v2dVisual2);

            if (fl_m3d_dic.ContainsKey(number)) return;
            fl_m3d_dic.Add(number, tempModel);

            viewport.Children.Add(tempModel);

        }

        #endregion

        #region Wall Control
        // 벽 생성
        public void addWall(WallDraw w_vm)
        {
            ModelVisual3D tempModel = new ModelVisual3D();

            tempModel = CreateWall(w_vm, w_vm.Z);
            if (wall_m3d_dic.ContainsKey(w_vm.id)) return; 
            wall_m3d_dic.Add(w_vm.id, tempModel);
            viewport.Children.Add(tempModel);
        }


        public void removeWallbyNum(int num)
        {
            ModelVisual3D m = null;
            if (wall_m3d_dic.ContainsKey(num))
                m = wall_m3d_dic[num];
            if(m!=null)
            {
                viewport.Children.Remove(m);
                wall_m3d_dic.Remove(num);
            }
        }

        public void removeAllWall()
        {
            foreach(var at in wall_m3d_dic)
            {
                viewport.Children.Remove(at.Value);
            }
            wall_m3d_dic.Clear();
        }

        public void selectWall(WallDraw w_vm)
        {
            ModelVisual3D m = wall_m3d_dic[w_vm.id];
            if(m!=null)
                ModifyWallColor(m, w_vm, new SolidColorBrush(Color.FromArgb(w_vm.colorA, w_vm.colorR, w_vm.colorG, w_vm.colorB)));
        }

        public void reDrawWall(WallDraw w_vm)
        {
            ModelVisual3D m = wall_m3d_dic[w_vm.id];
            if (m != null)
                reDrawWallModel(m, w_vm);
        }
        
        #endregion

        #region WallConer Control
        // 벽 코너 그리기 
        public void addWallCorner(WallCornerDraw wc)
        {
            ModelVisual3D tempModel = new ModelVisual3D();

            if (wallCorner_m3d_dic.ContainsKey(wc.id)) return;

            tempModel = CreateSquarePrism(wc.p_list[0], wc.p_list[1] , wc.p_list[2], wc.p_list[3], wc.height, new SolidColorBrush(Color.FromArgb(wc.colorA, wc.colorR, wc.colorG, wc.colorB)), wc.alpha);

            wallCorner_m3d_dic.Add(wc.id, tempModel);
            viewport.Children.Add(tempModel);
        }

        public void removeWallCornerbyId(int wc_id)
        {
            if(wallCorner_m3d_dic.ContainsKey(wc_id))
            {
                ModelVisual3D tempModel = wallCorner_m3d_dic[wc_id];
                viewport.Children.Remove(tempModel);
                wallCorner_m3d_dic.Remove(wc_id);
            }
        } 

        public void removeAllWallCorner()
        {
            foreach (var at in wallCorner_m3d_dic)
            {
                viewport.Children.Remove(at.Value);
            }
            wallCorner_m3d_dic.Clear();
        }
        // 코너 변경 
        public void modifyWallCorner(WallCornerDraw wc)
        {
            removeWallCornerbyId(wc.id);
            addWallCorner(wc);
        }
        #endregion
        // 벽 컬러 바꾸기 
        public void ModifyWallColor(ModelVisual3D model, WallDraw w_vm, Brush brush)
        {
            //SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(w_vm.colorA, w_vm.colorR, w_vm.colorG, w_vm.colorB));
            double thiness = w_vm.thickness;
            double alpha = w_vm.alpha;

            model.Children.Clear();

            Point3D p0, p1, p2, p3, p4, p5, p6, p7;
            getPointForCubeByWallData(w_vm, 0, out p0, out p1, out p2, out p3, out p4, out p5, out p6, out p7);

            addWallToModel3d(model, p0, p1, p2, p3, p4, p5, p6, p7, brush, alpha);
        }

        // 다시 그리기 
        private void reDrawWallModel(ModelVisual3D model, WallDraw w_vm)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(w_vm.colorA, w_vm.colorR, w_vm.colorG, w_vm.colorB));
            double thiness = w_vm.thickness;
            double alpha = w_vm.alpha;

            model.Children.Clear();

            Point3D p0, p1, p2, p3, p4, p5, p6, p7;
            getPointForCubeByWallData(w_vm, 0, out p0, out p1, out p2, out p3, out p4, out p5, out p6, out p7);

            addWallToModel3d(model, p0, p1, p2, p3, p4, p5, p6, p7, brush, alpha);
        }

        // 벽 그리기 
        public ModelVisual3D CreateWall(WallDraw w_vm, double Z)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(w_vm.colorA, w_vm.colorR, w_vm.colorG, w_vm.colorB));
            double thiness = w_vm.thickness;
            double alpha = w_vm.alpha;

            Point3D p0, p1, p2, p3, p4, p5, p6, p7;
            getPointForCubeByWallData(w_vm, Z, out p0, out p1, out p2, out p3, out p4, out p5, out p6, out p7);

            ModelVisual3D tempModel = new ModelVisual3D();
            addWallToModel3d(tempModel,p0, p1, p2, p3, p4, p5, p6, p7, brush, alpha );
           
            return tempModel;
        }
        // 벽그리기 하나
        public Model3DGroup CreateWallInOneModel(Model3DGroup modelGroup, WallDraw w_vm, double Z)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(w_vm.colorA, w_vm.colorR, w_vm.colorG, w_vm.colorB));
            double alpha = w_vm.alpha;

            Point3D p0, p1, p2, p3, p4, p5, p6, p7;
            getPointForCubeByWallData(w_vm, Z, out p0, out p1, out p2, out p3, out p4, out p5, out p6, out p7);

            //front side triangles
            CreateQuadrangleModel(modelGroup, p1, p5, p6, p2, brush);
            //right side triangles
            CreateQuadrangleModel(modelGroup, p2, p6, p7, p3, brush);
            //back side triangles
            CreateQuadrangleModel(modelGroup, p0, p3, p7, p4, brush);
            //left side triangles
            CreateQuadrangleModel(modelGroup, p0, p4, p5, p1, brush);
            //top side triangles
            CreateQuadrangleModel(modelGroup, p5, p4, p7, p6, brush);
            //bottom side triangles
            CreateQuadrangleModel(modelGroup, p0, p3, p2, p1, brush);

            return modelGroup;
        }


        public Model3DGroup CreateWallCornerInOneModelNoAlpha(Model3DGroup modelGroup, WallCornerDraw wc, double Z)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(wc.colorA, wc.colorR, wc.colorG, wc.colorB));
            double alpha = wc.alpha;

            
            //ModelVisual3D tempModel = new ModelVisual3D();
            //Model3DGroup prism = new Model3DGroup();


            //     p4----------------p5
            //    /!                /|
            //   / !               / |
            //  /  !              /  |
            // /   !             /   |
            //p7----------------p6   |
            //|    !            |    |
            //|    p0-----------|----p1
            //|   /             |   /
            //|  /              |  /
            //| /               | /
            //|/                |/
            //p3----------------p2

            Point3D p0 = new Point3D(wc.p_list[0].Y, wc.p_list[0].X, Z);
            Point3D p1 = new Point3D(wc.p_list[1].Y, wc.p_list[1].X, Z);
            Point3D p2 = new Point3D(wc.p_list[2].Y, wc.p_list[2].X, Z);
            Point3D p3 = new Point3D(wc.p_list[3].Y, wc.p_list[3].X, Z);

            Point3D p4 = new Point3D(wc.p_list[0].Y, wc.p_list[0].X, Z + wc.height);
            Point3D p5 = new Point3D(wc.p_list[1].Y, wc.p_list[1].X, Z + wc.height);
            Point3D p6 = new Point3D(wc.p_list[2].Y, wc.p_list[2].X, Z + wc.height);
            Point3D p7 = new Point3D(wc.p_list[3].Y, wc.p_list[3].X, Z + wc.height);

            //if (alpha == 1)
            {
                //front side triangles
                CreateQuadrangleModel(modelGroup, p7, p6, p2, p3, brush);
                //right side triangles
                CreateQuadrangleModel(modelGroup, p4, p7, p3, p0, brush);
                //back side triangles
                CreateQuadrangleModel(modelGroup, p5, p4, p0, p1, brush);
                //left side triangles
                CreateQuadrangleModel(modelGroup, p6, p5, p1, p2, brush);
                //top side triangles
                CreateQuadrangleModel(modelGroup, p4, p5, p6, p7, brush);
                //bottom side triangles
                //CreateQuadrangleModel(modelGroup, p0, p3, p2, p1, brush);


                //front side triangles
                CreateQuadrangleModel(modelGroup, p3, p2, p6, p7, brush);
                //right side triangles
                CreateQuadrangleModel(modelGroup, p0, p3, p7, p4, brush);
                //back side triangles
                CreateQuadrangleModel(modelGroup, p1, p0, p4, p5, brush);
                //left side triangles
                CreateQuadrangleModel(modelGroup, p2, p1, p5, p6, brush);
                //top side triangles
                CreateQuadrangleModel(modelGroup, p7, p6, p5, p4, brush);
#if false


                //윗부분
                prism.Children.Add(createTriangleModel(p4, p5, p6, brush));
                prism.Children.Add(createTriangleModel(p6, p7, p4, brush));

                //앞에서 보이는 부분
                prism.Children.Add(createTriangleModel(p7, p6, p2, brush));
                prism.Children.Add(createTriangleModel(p2, p3, p7, brush));

                //left view
                prism.Children.Add(createTriangleModel(p4, p7, p3, brush));
                prism.Children.Add(createTriangleModel(p3, p0, p4, brush));

                //right view
                prism.Children.Add(createTriangleModel(p6, p5, p1, brush));
                prism.Children.Add(createTriangleModel(p1, p2, p6, brush));

                //back view
                prism.Children.Add(createTriangleModel(p5, p4, p0, brush));
                prism.Children.Add(createTriangleModel(p0, p1, p5, brush));

                //반대인 경우를 생각하여 반대방향에서 한번더 그린다???
                //윗부분
                prism.Children.Add(createTriangleModel(p7, p6, p5, brush));
                prism.Children.Add(createTriangleModel(p5, p4, p7, brush));

                //앞에서 보이는 부분
                prism.Children.Add(createTriangleModel(p3, p3, p6, brush));
                prism.Children.Add(createTriangleModel(p6, p7, p3, brush));

                //left view
                prism.Children.Add(createTriangleModel(p0, p3, p7, brush));
                prism.Children.Add(createTriangleModel(p7, p4, p0, brush));

                //right view
                prism.Children.Add(createTriangleModel(p2, p1, p5, brush));
                prism.Children.Add(createTriangleModel(p5, p6, p2, brush));

                //back view
                prism.Children.Add(createTriangleModel(p1, p0, p4, brush));
                prism.Children.Add(createTriangleModel(p4, p5, p1, brush));
                tempModel.Content = prism; 
#endif
            }
#if false
            else
            {
                //front side triangles
                MeshGeometry3D frontMesh = new MeshGeometry3D();
                AddQuadrangleMesh2(p7, p6, p2, p3, frontMesh);
                Viewport2DVisual3D frontView = CreateViewport2DVisual3D(frontMesh, calculate2DXY(p1, p5, p6), brush, alpha);
                tempModel.Children.Add(frontView);

                //right side triangles
                MeshGeometry3D rightMesh = new MeshGeometry3D();
                AddQuadrangleMesh2(p6, p5, p1, p2, rightMesh);
                Viewport2DVisual3D rightView = CreateViewport2DVisual3D(rightMesh, calculate2DXY(p1, p5, p6), brush, alpha);
                tempModel.Children.Add(rightView);

                //back side triangles
                MeshGeometry3D backMesh = new MeshGeometry3D();
                AddQuadrangleMesh2(p5, p4, p0, p1, backMesh);
                Viewport2DVisual3D backView = CreateViewport2DVisual3D(backMesh, calculate2DXY(p3, p7, p4), brush, alpha);
                tempModel.Children.Add(backView);

                //left side triangles
                MeshGeometry3D LeftMesh = new MeshGeometry3D();
                AddQuadrangleMesh2(p4, p7, p3, p0, LeftMesh);
                Viewport2DVisual3D leftView = CreateViewport2DVisual3D(LeftMesh, calculate2DXY(p0, p4, p5), brush, alpha);
                tempModel.Children.Add(leftView);

                //top side triangles
                MeshGeometry3D topMesh = new MeshGeometry3D();
                AddQuadrangleMesh2(p4, p5, p6, p7, topMesh);
                Viewport2DVisual3D topView = CreateViewport2DVisual3D(topMesh, calculate2DXY(p5, p4, p7), brush, alpha);
                //Viewport2DVisual3D topView = CreateViewport2DVisual3D(topMesh, calculate2DXY(p5, p4, p7), Brushes.Black, alpha);
                tempModel.Children.Add(topView);

                ////bottom side triangles
                //MeshGeometry3D bottomMesh = new MeshGeometry3D();
                //AddQuadrangleMesh2(p3, p2, p1, p0, bottomMesh);
                //Viewport2DVisual3D bottomView = CreateViewport2DVisual3D(bottomMesh, calculate2DXY(p0, p3, p2), brush, alpha);
                //tempModel.Children.Add(bottomView);


                //반대인 경우
                //front side triangles
                MeshGeometry3D frontMesh2 = new MeshGeometry3D();
                AddQuadrangleMesh2(p3, p2, p6, p7, frontMesh2);
                Viewport2DVisual3D frontView2 = CreateViewport2DVisual3D(frontMesh2, calculate2DXY(p1, p5, p6), brush, alpha);
                tempModel.Children.Add(frontView2);

                //right side triangles
                MeshGeometry3D rightMesh2 = new MeshGeometry3D();
                AddQuadrangleMesh2(p2, p1, p5, p6, rightMesh2);
                Viewport2DVisual3D rightView2 = CreateViewport2DVisual3D(rightMesh2, calculate2DXY(p1, p5, p6), brush, alpha);
                tempModel.Children.Add(rightView2);

                //back side triangles
                MeshGeometry3D backMesh2 = new MeshGeometry3D();
                AddQuadrangleMesh2(p1, p0, p4, p5, backMesh2);
                Viewport2DVisual3D backView2 = CreateViewport2DVisual3D(backMesh2, calculate2DXY(p3, p7, p4), brush, alpha);
                tempModel.Children.Add(backView2);

                //left side triangles
                MeshGeometry3D LeftMesh2 = new MeshGeometry3D();
                AddQuadrangleMesh2(p0, p3, p7, p4, LeftMesh2);
                Viewport2DVisual3D leftView2 = CreateViewport2DVisual3D(LeftMesh2, calculate2DXY(p0, p4, p5), brush, alpha);
                tempModel.Children.Add(leftView2);

                //top side triangles
                MeshGeometry3D topMesh2 = new MeshGeometry3D();
                AddQuadrangleMesh2(p7, p6, p5, p4, topMesh2);
                Viewport2DVisual3D topView2 = CreateViewport2DVisual3D(topMesh2, calculate2DXY(p5, p4, p7), brush, alpha);
                //Viewport2DVisual3D topView = CreateViewport2DVisual3D(topMesh, calculate2DXY(p5, p4, p7), Brushes.Black, alpha);
                tempModel.Children.Add(topView2);

                ////bottom side triangles
                //MeshGeometry3D bottomMesh2 = new MeshGeometry3D();
                //AddQuadrangleMesh2(p0, p1, p2, p3, bottomMesh2);
                //Viewport2DVisual3D bottomView2 = CreateViewport2DVisual3D(bottomMesh2, calculate2DXY(p0, p3, p2), brush, alpha);
                //tempModel.Children.Add(bottomView2); 
            } 
#endif
            return modelGroup;
        }


        public Model3DGroup CreateCubeOneModel(Model3DGroup modelGroup, Point p, Size s, Double h, Double Z, Brush brush)
        {
            //2d->3d 변환시 xy좌표가 바꿔진다
            Point st_p = new Point(p.Y, p.X);
            Double sY = s.Width;
            Double sX = s.Height;

            //     p4----------------p7
            //    /!                /|
            //   / !               / |
            //  /  !              /  |
            // /   !             /   |
            //p5----------------p6   |
            //|    !            |    |
            //|    p0-----------|----p3
            //|   /             |   /
            //|  /              |  /
            //| /               | /
            //|/                |/
            //p1----------------p2

            Point3D p0 = new Point3D(st_p.X, st_p.Y, Z);
            Point3D p1 = new Point3D(st_p.X + sX, st_p.Y, Z);
            Point3D p2 = new Point3D(st_p.X + sX, st_p.Y + sY, Z);
            Point3D p3 = new Point3D(st_p.X, st_p.Y + sY, Z);
            Point3D p4 = new Point3D(st_p.X, st_p.Y, Z + h);
            Point3D p5 = new Point3D(st_p.X + sX, st_p.Y, Z + h);
            Point3D p6 = new Point3D(st_p.X + sX, st_p.Y + sY, Z + h);
            Point3D p7 = new Point3D(st_p.X, st_p.Y + sY, Z + h);
          
            //front side triangles
            CreateQuadrangleModel(modelGroup, p3, p7, p6, p2, brush);
            //right side triangles
            CreateQuadrangleModel(modelGroup, p2, p6, p5, p1, brush);
            //back side triangles
            CreateQuadrangleModel(modelGroup, p0, p1, p5, p4, brush);
            //left side triangles
            CreateQuadrangleModel(modelGroup, p0, p4, p7, p3, brush);
            //top side triangles
            CreateQuadrangleModel(modelGroup, p7, p4, p5, p6, brush);
            //bottom side triangles
            CreateQuadrangleModel(modelGroup, p0, p1, p2, p3, brush);

            return modelGroup;
        }
        
        public ModelVisual3D CreateCube(Point p, Size s, Double h, Color c, double Z)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(c.A, c.R, c.G, c.B));

            Point3D p0, p1, p2, p3, p4, p5, p6, p7;
            getPointForCubeByRackData(p, s, h, Z, out p0, out p1, out p2, out p3, out p4, out p5, out p6, out p7);

            ModelVisual3D tempModel = new ModelVisual3D();
            addWallToModel3d(tempModel, p0, p1, p2, p3, p4, p5, p6, p7, brush, 1);

            return tempModel;
        }

        private Model3DGroup createTriangleModel(Point3D p0, Point3D p1, Point3D p2, Brush brush)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            Vector3D normal = calculateNormal(p0, p1, p2);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);
            Material material = new DiffuseMaterial(
                brush);
            GeometryModel3D model = new GeometryModel3D(
                mesh, material);
            Model3DGroup group = new Model3DGroup();
            group.Children.Add(model);
            return group;
        }
#if NO_ALPHA_FL
        public Model3DGroup CreateRectangle(Point p, Size s, Double z, Brush brush)
#else
        public Model3DGroup CreateRectangle(ModelVisual3D model, Point p, Size s, Double z, Brush brush)
#endif
        {
            Model3DGroup plate = new Model3DGroup();
            Point sp = new Point(p.Y, p.X);
            Double dx = s.Height;
            Double dy = s.Width;

            //    p0----------------p3=> Y
            //   /                 /
            //  /                 /
            // /                 /
            ///                 /
            //p1---------------p2
            //X

            Point3D p0 = new Point3D(sp.X, sp.Y, z);
            Point3D p1 = new Point3D(sp.X + dx, sp.Y, z);
            Point3D p2 = new Point3D(sp.X + dx, sp.Y + dy, z);
            Point3D p3 = new Point3D(sp.X, sp.Y + dy, z);

#if NO_ALPHA_FL
            CreateQuadrangleModel(plate, p0, p1, p2, p3, brush);
#else
            //front side triangles
            MeshGeometry3D frontMesh = new MeshGeometry3D();
            AddQuadrangleMesh2(p0, p1, p2, p3, frontMesh);
            Viewport2DVisual3D frontView = CreateViewport2DVisual3D(frontMesh, calculate2DXY(p0, p1, p2), brush, 0.8);
            model.Children.Add(frontView);
#endif
            return plate;
        }

        public ModelVisual3D CreateRectangle2(Point p, Size s, Double z, Brush brush)
        {
            ModelVisual3D tempModel = new ModelVisual3D();
            Model3DGroup plate = new Model3DGroup();
            Point sp = new Point(p.Y, p.X);
            Double dx = s.Height;
            Double dy = s.Width;

            //    p0----------------p3=> Y
            //   /                 /
            //  /                 /
            // /                 /
            ///                 /
            //p1---------------p2
            //X

            Point3D p0 = new Point3D(sp.X, sp.Y, z);
            Point3D p1 = new Point3D(sp.X + dx, sp.Y, z);
            Point3D p2 = new Point3D(sp.X + dx, sp.Y + dy, z);
            Point3D p3 = new Point3D(sp.X, sp.Y + dy, z);

            CreateQuadrangleModel(plate, p0, p1, p2, p3, brush);

            tempModel.Content = plate;
            return tempModel;
        }

        public ModelVisual3D CreateSquarePrism(Point sq0, Point sq1, Point sq2, Point sq3, Double height, Brush brush, Double alpha)
        {
            ModelVisual3D tempModel = new ModelVisual3D();
            Model3DGroup prism = new Model3DGroup();


            //     p4----------------p5
            //    /!                /|
            //   / !               / |
            //  /  !              /  |
            // /   !             /   |
            //p7----------------p6   |
            //|    !            |    |
            //|    p0-----------|----p1
            //|   /             |   /
            //|  /              |  /
            //| /               | /
            //|/                |/
            //p3----------------p2

            Point3D p0 = new Point3D(sq0.Y, sq0.X, 0);
            Point3D p1 = new Point3D(sq1.Y, sq1.X, 0);
            Point3D p2 = new Point3D(sq2.Y, sq2.X, 0);
            Point3D p3 = new Point3D(sq3.Y, sq3.X, 0);

            Point3D p4 = new Point3D(sq0.Y, sq0.X, height);
            Point3D p5 = new Point3D(sq1.Y, sq1.X, height);
            Point3D p6 = new Point3D(sq2.Y, sq2.X, height);
            Point3D p7 = new Point3D(sq3.Y, sq3.X, height);

            if (alpha == 1)
            {
                //윗부분
                prism.Children.Add(createTriangleModel(p4, p5, p6, brush));
                prism.Children.Add(createTriangleModel(p6, p7, p4, brush));

                //앞에서 보이는 부분
                prism.Children.Add(createTriangleModel(p7, p6, p2, brush));
                prism.Children.Add(createTriangleModel(p2, p3, p7, brush));

                //left view
                prism.Children.Add(createTriangleModel(p4, p7, p3, brush));
                prism.Children.Add(createTriangleModel(p3, p0, p4, brush));

                //right view
                prism.Children.Add(createTriangleModel(p6, p5, p1, brush));
                prism.Children.Add(createTriangleModel(p1, p2, p6, brush));

                //back view
                prism.Children.Add(createTriangleModel(p5, p4, p0, brush));
                prism.Children.Add(createTriangleModel(p0, p1, p5, brush));

                //반대인 경우를 생각하여 반대방향에서 한번더 그린다???
                //윗부분
                prism.Children.Add(createTriangleModel(p7, p6, p5, brush));
                prism.Children.Add(createTriangleModel(p5, p4, p7, brush));

                //앞에서 보이는 부분
                prism.Children.Add(createTriangleModel(p3, p3, p6, brush));
                prism.Children.Add(createTriangleModel(p6, p7, p3, brush));

                //left view
                prism.Children.Add(createTriangleModel(p0, p3, p7, brush));
                prism.Children.Add(createTriangleModel(p7, p4, p0, brush));

                //right view
                prism.Children.Add(createTriangleModel(p2, p1, p5, brush));
                prism.Children.Add(createTriangleModel(p5, p6, p2, brush));

                //back view
                prism.Children.Add(createTriangleModel(p1, p0, p4, brush));
                prism.Children.Add(createTriangleModel(p4, p5, p1, brush));
                tempModel.Content = prism; 
            }
            else
            {
                //front side triangles
                MeshGeometry3D frontMesh = new MeshGeometry3D();
                AddQuadrangleMesh2(p7, p6, p2, p3, frontMesh);
                Viewport2DVisual3D frontView = CreateViewport2DVisual3D(frontMesh, calculate2DXY(p1, p5, p6), brush, alpha);
                tempModel.Children.Add(frontView);

                //right side triangles
                MeshGeometry3D rightMesh = new MeshGeometry3D();
                AddQuadrangleMesh2(p6, p5, p1, p2, rightMesh);
                Viewport2DVisual3D rightView = CreateViewport2DVisual3D(rightMesh, calculate2DXY(p1, p5, p6), brush, alpha);
                tempModel.Children.Add(rightView);

                //back side triangles
                MeshGeometry3D backMesh = new MeshGeometry3D();
                AddQuadrangleMesh2(p5, p4, p0, p1, backMesh);
                Viewport2DVisual3D backView = CreateViewport2DVisual3D(backMesh, calculate2DXY(p3, p7, p4), brush, alpha);
                tempModel.Children.Add(backView);

                //left side triangles
                MeshGeometry3D LeftMesh = new MeshGeometry3D();
                AddQuadrangleMesh2(p4, p7, p3, p0, LeftMesh);
                Viewport2DVisual3D leftView = CreateViewport2DVisual3D(LeftMesh, calculate2DXY(p0, p4, p5), brush, alpha);
                tempModel.Children.Add(leftView);

                //top side triangles
                MeshGeometry3D topMesh = new MeshGeometry3D();
                AddQuadrangleMesh2(p4, p5, p6, p7, topMesh);
                Viewport2DVisual3D topView = CreateViewport2DVisual3D(topMesh, calculate2DXY(p5, p4, p7), brush, alpha);
                //Viewport2DVisual3D topView = CreateViewport2DVisual3D(topMesh, calculate2DXY(p5, p4, p7), Brushes.Black, alpha);
                tempModel.Children.Add(topView);

                ////bottom side triangles
                //MeshGeometry3D bottomMesh = new MeshGeometry3D();
                //AddQuadrangleMesh2(p3, p2, p1, p0, bottomMesh);
                //Viewport2DVisual3D bottomView = CreateViewport2DVisual3D(bottomMesh, calculate2DXY(p0, p3, p2), brush, alpha);
                //tempModel.Children.Add(bottomView);


                //반대인 경우
                //front side triangles
                MeshGeometry3D frontMesh2 = new MeshGeometry3D();
                AddQuadrangleMesh2(p3, p2, p6, p7, frontMesh2);
                Viewport2DVisual3D frontView2 = CreateViewport2DVisual3D(frontMesh2, calculate2DXY(p1, p5, p6), brush, alpha);
                tempModel.Children.Add(frontView2);

                //right side triangles
                MeshGeometry3D rightMesh2 = new MeshGeometry3D();
                AddQuadrangleMesh2(p2, p1, p5, p6, rightMesh2);
                Viewport2DVisual3D rightView2 = CreateViewport2DVisual3D(rightMesh2, calculate2DXY(p1, p5, p6), brush, alpha);
                tempModel.Children.Add(rightView2);

                //back side triangles
                MeshGeometry3D backMesh2 = new MeshGeometry3D();
                AddQuadrangleMesh2(p1, p0, p4, p5, backMesh2);
                Viewport2DVisual3D backView2 = CreateViewport2DVisual3D(backMesh2, calculate2DXY(p3, p7, p4), brush, alpha);
                tempModel.Children.Add(backView2);

                //left side triangles
                MeshGeometry3D LeftMesh2 = new MeshGeometry3D();
                AddQuadrangleMesh2(p0, p3, p7, p4, LeftMesh2);
                Viewport2DVisual3D leftView2 = CreateViewport2DVisual3D(LeftMesh2, calculate2DXY(p0, p4, p5), brush, alpha);
                tempModel.Children.Add(leftView2);

                //top side triangles
                MeshGeometry3D topMesh2 = new MeshGeometry3D();
                AddQuadrangleMesh2(p7, p6, p5, p4, topMesh2);
                Viewport2DVisual3D topView2 = CreateViewport2DVisual3D(topMesh2, calculate2DXY(p5, p4, p7), brush, alpha);
                //Viewport2DVisual3D topView = CreateViewport2DVisual3D(topMesh, calculate2DXY(p5, p4, p7), Brushes.Black, alpha);
                tempModel.Children.Add(topView2);

                ////bottom side triangles
                //MeshGeometry3D bottomMesh2 = new MeshGeometry3D();
                //AddQuadrangleMesh2(p0, p1, p2, p3, bottomMesh2);
                //Viewport2DVisual3D bottomView2 = CreateViewport2DVisual3D(bottomMesh2, calculate2DXY(p0, p3, p2), brush, alpha);
                //tempModel.Children.Add(bottomView2); 
            }

            return tempModel;
        }

        public ModelVisual3D CreateTriangularPrism(Point tr0, Point tr1, Point tr2, Double height, Brush brush)
        {
            ModelVisual3D tempModel = new ModelVisual3D();
            Model3DGroup prism = new Model3DGroup();
            //  ___
            //p0   ---__
            //        ___---p1
            //p2___---      |
            //|             |
            //|             |
            //|             |
            //|             |
            //|        ___---p3
            //p4____---


            Point3D p0 = new Point3D(tr0.X, tr0.Y, height);
            Point3D p1 = new Point3D(tr1.X, tr1.Y, height);
            Point3D p2 = new Point3D(tr2.X, tr2.Y, height);
            Point3D p3 = new Point3D(tr0.X, tr0.Y, 0);
            Point3D p4 = new Point3D(tr1.X, tr1.Y, 0);
            Point3D p5 = new Point3D(tr2.X, tr2.Y, 0);

            //윗부분
            prism.Children.Add(createTriangleModel(p0, p1, p2, brush));

            //앞에서 보이는 부분
            prism.Children.Add(createTriangleModel(p2, p1, p3, brush));
            prism.Children.Add(createTriangleModel(p3, p4, p2, brush));

            //left view
            prism.Children.Add(createTriangleModel(p0, p2, p4, brush));
            prism.Children.Add(createTriangleModel(p4, p5, p0, brush));

            //right view
            prism.Children.Add(createTriangleModel(p1, p0, p5, brush));
            prism.Children.Add(createTriangleModel(p5, p4, p1, brush));

            tempModel.Content = prism;
            return tempModel;
        }

        public ModelVisual3D CreateCylinder(Point p, Double radius, Double piece_count, Double height, Brush brush)
        {
            ModelVisual3D tempModel = new ModelVisual3D();
            Model3DGroup cylinder = new Model3DGroup();

            //2d->3d 변환시 xy좌표가 바꿔진다
            Point ct_p = new Point(p.Y + radius, p.X + radius);
            Double r = radius;
            Double Z = height;

            Point3D p0 = new Point3D(ct_p.X, ct_p.Y, Z);
            Point3D p1 = new Point3D(ct_p.X + r, ct_p.Y, Z);
            Point3D p2;
            Point3D p3 = new Point3D(ct_p.X + r, ct_p.Y, 0);
            Point3D p4;

            //  ___
            //p0   ---__
            //        ___---p1
            //p2___---      |
            //|             |
            //|             |
            //|             |
            //|             |
            //|        ___---p3
            //p4____---

            for (int i = 0; i < piece_count; i++)
            {
                Point p2_2d = makeTriangleLastPoint(new Point(p0.X, p0.Y), new Point(p1.X, p1.Y), 360 / piece_count);
                p2 = new Point3D(p2_2d.X, p2_2d.Y, Z);
                p4 = new Point3D(p2_2d.X, p2_2d.Y, 0);
                //윗부분
                cylinder.Children.Add(createTriangleModel(p0, p1, p2, brush));

                //앞에서 보이는 부분
                cylinder.Children.Add(createTriangleModel(p2, p1, p3, brush));
                cylinder.Children.Add(createTriangleModel(p3, p4, p2, brush));


                // Console.WriteLine("({0:F0},{1:F0},{2:F0}): angle = {3:F2}", p1.X, p1.Y, p1.Z, 360/piece_count);
                p1 = p2;
                p3 = p4;

            }

            tempModel.Content = cylinder;
            return tempModel;
        }

        public ModelVisual3D CreateLine(Point start_p, Point end_p, Double h, Double w, Brush brush)
        {
            ModelVisual3D tempModel = new ModelVisual3D();

            Point st_p = new Point(start_p.Y, start_p.X);
            Point ed_p = new Point(end_p.Y, end_p.X);


            //     p4----------------p7
            //    /!                /|
            //   / !               / |
            //  /  !              /  |
            // /   !             /   |
            //p5----------------p6   |
            //|    !            |    |
            //|    p0----st_p---|----p3
            //|   /        /    |   /
            //|  /        /     |  /
            //| /        /      | /
            //|/        /       |/
            //p1-----ed_p-------p2

            Point3D p0 = new Point3D(st_p.X, st_p.Y , 0);
            Point3D p1 = new Point3D(ed_p.X, ed_p.Y , 0);
            Point3D p2 = new Point3D(st_p.X + w, st_p.Y + w, 0);
            Point3D p3 = new Point3D(ed_p.X + w, ed_p.Y + w, 0);
            Point3D p4 = new Point3D(st_p.X, st_p.Y , h);
            Point3D p5 = new Point3D(ed_p.X, ed_p.Y , h);
            Point3D p6 = new Point3D(st_p.X + w, st_p.Y + w , h);
            Point3D p7 = new Point3D(ed_p.X + w, ed_p.Y + w, h);

            Model3DGroup line = new Model3DGroup();

            //front side triangles
            CreateQuadrangleModel(line, p3, p7, p6, p2, brush);
            //right side triangles
            CreateQuadrangleModel(line, p2, p6, p5, p1, brush);
            //back side triangles
            CreateQuadrangleModel(line, p0, p1, p5, p4, brush);
            //left side triangles
            CreateQuadrangleModel(line, p0, p4, p7, p3, brush);
            //top side triangles
            CreateQuadrangleModel(line, p7, p4, p5, p6, brush);
            //CreateQuadrangleModel(cube, p5, p4, p7, p6, Brushes.Black);
            //bottom side triangles
            CreateQuadrangleModel(line, p0, p1, p2, p3, brush);
            tempModel.Content = line;

            return tempModel;
        }

        public ModelVisual3D CreateLine2(Point3D p0, Point3D p1, Point3D p2, Point3D p3, Brush brush)
       {
            ModelVisual3D tempModel = new ModelVisual3D();
 
            //    p0----st_p--------p3
            //   /        /        /
            //  /        /        /
            // /        /        /
            ///        /        /
            //p1-----ed_p------p2


            Model3DGroup line = new Model3DGroup();

            CreateQuadrangleModel(line, p0, p1, p2, p3, brush);
            tempModel.Content = line;

            return tempModel;
        }



        public ModelVisual3D CreateCubeWithSideText(Point p, Size s, Double h, Brush brush, double Z, String str)
        {
            ModelVisual3D tempModel = new ModelVisual3D();
            //2d->3d 변환시 xy좌표가 바꿔진다
            Point st_p = new Point(p.Y, p.X);
            Double sY = s.Width;
            Double sX = s.Height;



            //     p4----------------p7
            //    /!                /|
            //   / !               / |
            //  /  !              /  |
            // /   !             /   |
            //p5----------------p6   |
            //|    !            |    |
            //|    p0-----------|----p3
            //|   /             |   /
            //|  /              |  /
            //| /               | /
            //|/                |/
            //p1----------------p2

            Point3D p0 = new Point3D(st_p.X, st_p.Y, Z);
            Point3D p1 = new Point3D(st_p.X + sX, st_p.Y, Z);
            Point3D p2 = new Point3D(st_p.X + sX, st_p.Y + sY, Z);
            Point3D p3 = new Point3D(st_p.X, st_p.Y + sY, Z);
            Point3D p4 = new Point3D(st_p.X, st_p.Y, Z+h);
            Point3D p5 = new Point3D(st_p.X + sX, st_p.Y, Z + h);
            Point3D p6 = new Point3D(st_p.X + sX, st_p.Y + sY, Z + h);
            Point3D p7 = new Point3D(st_p.X, st_p.Y + sY, Z + h);

            Model3DGroup cube = new Model3DGroup();

            //front side triangles
            CreateQuadrangleModel(cube, p3, p7, p6, p2, brush);
            //right side triangles
            CreateQuadrangleModel(cube, p2, p6, p5, p1, brush);
            //back side triangles
            CreateQuadrangleModel(cube, p0, p1, p5, p4, brush);
            //left side triangles
            CreateQuadrangleModel(cube, p0, p4, p7, p3, brush);
            //top side triangles
            CreateQuadrangleModel(cube, p7, p4, p5, p6, brush);
            //CreateQuadrangleModel(cube, p5, p4, p7, p6, Brushes.Black);
            //bottom side triangles
            CreateQuadrangleModel(cube, p0, p1, p2, p3, brush);
            tempModel.Content = cube;



            //        p7 => Y 0.1
            //       /|
            //      / |
            //     /  |
            //    /   | 
            //   p6  t|
            // X | x  | 
            //   |t    p3
            //   |   /
            //   |  /
            //   | /
            //   |/ 
            //  p2
            
            Double margin_left = 700;
            Double margin_top = 30;
            Double margin_bottom = 30;
            Double margin_right = 30;
            //Point3D p6_1 = new Point3D(p6.X - margin_left , p6.Y + 0.1 , p6.Z - margin_top);
            //Point3D p2_1 = new Point3D(p2.X - margin_left, p2.Y + 0.1, p2.Z + margin_bottom);
            //Point3D p3_1 = new Point3D(p3.X + margin_right, p3.Y + 0.1, p3.Z + margin_bottom);
            //Point3D p7_1 = new Point3D(p7.X + margin_right, p7.Y + 0.1, p7.Z - margin_top);

            Point3D p6_1 = new Point3D(p6.X - margin_left, p6.Y + 0.1, p6.Z - margin_top);
            Point3D p2_1 = new Point3D(p2.X - margin_left, p2.Y + 0.1, p2.Z + margin_bottom);
            Point3D p3_1 = new Point3D(p3.X + margin_right, p3.Y + 0.1, p3.Z + margin_bottom);
            Point3D p7_1 = new Point3D(p7.X + margin_right, p7.Y + 0.1, p7.Z - margin_top);
            
            
            //Viewport2DVisual3D v2dVisual = make2DTextVisual3D(p6, p2, p3, p7, str, 1000);
            Viewport2DVisual3D v2dVisual = make2DTextVisual3D(p6_1, p2_1, p3_1, p7_1, str, 1000);
            tempModel.Children.Add(v2dVisual);

            return tempModel;
        }



        public ModelVisual3D CreateCubeWithTopImage(Point p, Size s, Double h, Brush brush, String img_path)
        {
            ModelVisual3D tempModel = new ModelVisual3D();

            //2d->3d 변환시 xy좌표가 바꿔진다
            Point st_p = new Point(p.Y, p.X);
            Double sY = s.Width;
            Double sX = s.Height;



            //     p4----------------p7
            //    /!                /|
            //   / !               / |
            //  /  !              /  |
            // /   !             /   |
            //p5----------------p6   |
            //|    !            |    |
            //|    p0-----------|----p3
            //|   /             |   /
            //|  /              |  /
            //| /               | /
            //|/                |/
            //p1----------------p2

            Point3D p0 = new Point3D(st_p.X     , st_p.Y     , 0);
            Point3D p1 = new Point3D(st_p.X + sX, st_p.Y     , 0);
            Point3D p2 = new Point3D(st_p.X + sX, st_p.Y + sY, 0);
            Point3D p3 = new Point3D(st_p.X     , st_p.Y + sY, 0);
            Point3D p4 = new Point3D(st_p.X     , st_p.Y     , h);
            Point3D p5 = new Point3D(st_p.X + sX, st_p.Y     , h);
            Point3D p6 = new Point3D(st_p.X + sX, st_p.Y + sY, h);
            Point3D p7 = new Point3D(st_p.X     , st_p.Y + sY, h);
           
            Model3DGroup cube = new Model3DGroup();
          
            //front side triangles
            CreateQuadrangleModel(cube, p3, p7, p6, p2, brush);
            //right side triangles
            CreateQuadrangleModel(cube, p2, p6, p5, p1, brush);
            //back side triangles
            CreateQuadrangleModel(cube, p0, p1, p5, p4, brush);
            //left side triangles
            CreateQuadrangleModel(cube, p0, p4, p7, p3, brush);
            //top side triangles
            CreateQuadrangleModel(cube, p7, p4, p5, p6, brush);
            //CreateQuadrangleModel(cube, p5, p4, p7, p6, Brushes.Black);
            //bottom side triangles
            CreateQuadrangleModel(cube, p0, p1, p2, p3, brush);
            tempModel.Content = cube;

#if true
            //top side triangles

            //   tx_p0____________tx_p3  => Y
            //      /             /
            //     /     Text    / 
            //tx_p1_____________tx_p2 
            //
            // ||
            // X
            Double margin = 1;
            Point3D tx_p0 = new Point3D(p4.X + margin, p4.Y + margin, p4.Z + 0.3);
            Point3D tx_p1 = new Point3D(p5.X - margin, p5.Y + margin, p5.Z + 0.3);
            Point3D tx_p2 = new Point3D(p6.X - margin, p6.Y - margin, p6.Z + 0.3);
            Point3D tx_p3 = new Point3D(p7.X + margin, p7.Y - margin, p7.Z + 0.3);

            if (img_path != null)
            {
                Viewport2DVisual3D v2dVisual = make2DImageVisual3D(tx_p0, tx_p1, tx_p2, tx_p3, img_path);
                tempModel.Children.Add(v2dVisual);
            }
#endif
            return tempModel;
        }

        // 원기둥 만들기 
        public ModelVisual3D CreateCylinderWithText(Point p, Double radius, Double piece_count, Double height, Brush brush,String str)
        {
            ModelVisual3D tempModel = new ModelVisual3D();
            Model3DGroup cylinder = new Model3DGroup();

            //2d->3d 변환시 xy좌표가 바꿔진다
            Point ct_p = new Point(p.Y + radius, p.X + radius);
            //Point ct_p = new Point(p.Y , p.X);
            Double r = radius;
            Double Z = height;

            Point3D p0 = new Point3D(ct_p.X, ct_p.Y, Z);
            Point3D p1 = new Point3D(ct_p.X + r, ct_p.Y, Z);
            Point3D p2;
            Point3D p3 = new Point3D(ct_p.X + r, ct_p.Y, 0);
            Point3D p4;


            for (int i = 0; i < piece_count; i++) // 조각 만큼 돌기 
            {
                Point p2_2d = makeTriangleLastPoint(new Point(p0.X, p0.Y), new Point(p1.X, p1.Y), 360 / piece_count);
                p2 = new Point3D(p2_2d.X, p2_2d.Y, Z);
                p4 = new Point3D(p2_2d.X, p2_2d.Y, 0);
                //윗부분
                cylinder.Children.Add(createTriangleModel(p0, p1, p2, brush));

                //앞에서 보이는 부분
                cylinder.Children.Add(createTriangleModel(p2, p1, p3, brush));
                cylinder.Children.Add(createTriangleModel(p3, p4, p2, brush));


                // Console.WriteLine("({0:F0},{1:F0},{2:F0}): angle = {3:F2}", p1.X, p1.Y, p1.Z, 360/piece_count);
                p1 = p2;
                p3 = p4;
            }
            tempModel.Content = cylinder;


            //top side triangles

            //   tx_p0____________tx_p3  => Y
            //      /             /
            //     /     Text    / 
            //tx_p1_____________tx_p2 
            //
            // ||
            // X

          //  Double ratio = 3 / 4;
#if true
            Point3D tx_p0 = new Point3D(ct_p.X - radius / 2, ct_p.Y - radius / 2, Z + 0.1);
            Point3D tx_p1 = new Point3D(ct_p.X + radius / 2, ct_p.Y - radius / 2, Z + 0.1);
            Point3D tx_p2 = new Point3D(ct_p.X + radius / 2, ct_p.Y + radius / 2, Z + 0.1);
            Point3D tx_p3 = new Point3D(ct_p.X - radius / 2, ct_p.Y + radius / 2, Z + 0.1);
#else
            Point3D tx_p0 = new Point3D(ct_p.X - radius * ratio, ct_p.Y - radius * ratio, Z + 0.1);
            Point3D tx_p1 = new Point3D(ct_p.X + radius * ratio, ct_p.Y - radius * ratio, Z + 0.1);
            Point3D tx_p2 = new Point3D(ct_p.X + radius * ratio, ct_p.Y + radius * ratio, Z + 0.1);
            Point3D tx_p3 = new Point3D(ct_p.X - radius * ratio, ct_p.Y + radius * ratio, Z + 0.1);
#endif

            Viewport2DVisual3D v2dVisual = make2DTextVisual3D(tx_p0, tx_p1, tx_p2, tx_p3, str, (int)radius);
            tempModel.Children.Add(v2dVisual);

            return tempModel;
        }

        public ModelVisual3D CreateCylinderWithImg(Point p, Double radius, Double piece_count, Double height, Brush brush, String img_path)
        {
            ModelVisual3D tempModel = new ModelVisual3D();
            Model3DGroup cylinder = new Model3DGroup();

            //2d->3d 변환시 xy좌표가 바꿔진다
            Point ct_p = new Point(p.Y + radius, p.X + radius);
            Double r = radius;
            Double Z = height;

            Point3D p0 = new Point3D(ct_p.X, ct_p.Y, Z);
            Point3D p1 = new Point3D(ct_p.X + r, ct_p.Y, Z);
            Point3D p2;
            Point3D p3 = new Point3D(ct_p.X + r, ct_p.Y, 0);
            Point3D p4;


            for (int i = 0; i < piece_count; i++)
            {
                Point p2_2d = makeTriangleLastPoint(new Point(p0.X, p0.Y), new Point(p1.X, p1.Y), 360 / piece_count);
                p2 = new Point3D(p2_2d.X, p2_2d.Y, Z);
                p4 = new Point3D(p2_2d.X, p2_2d.Y, 0);
                //윗부분
                cylinder.Children.Add(createTriangleModel(p0, p1, p2, brush));

                //앞에서 보이는 부분
                cylinder.Children.Add(createTriangleModel(p2, p1, p3, brush));
                cylinder.Children.Add(createTriangleModel(p3, p4, p2, brush));


                // Console.WriteLine("({0:F0},{1:F0},{2:F0}): angle = {3:F2}", p1.X, p1.Y, p1.Z, 360/piece_count);
                p1 = p2;
                p3 = p4;
            }
            tempModel.Content = cylinder;


            //top side triangles

            //   tx_p0____________tx_p3  => Y
            //      /             /
            //     /     Text    / 
            //tx_p1_____________tx_p2 
            //
            // ||
            // X

            //  Double ratio = 3 / 4;
#if true
            Point3D tx_p0 = new Point3D(ct_p.X - radius / 2, ct_p.Y - radius / 2, Z + 0.1);
            Point3D tx_p1 = new Point3D(ct_p.X + radius / 2, ct_p.Y - radius / 2, Z + 0.1);
            Point3D tx_p2 = new Point3D(ct_p.X + radius / 2, ct_p.Y + radius / 2, Z + 0.1);
            Point3D tx_p3 = new Point3D(ct_p.X - radius / 2, ct_p.Y + radius / 2, Z + 0.1);
#else
            Point3D tx_p0 = new Point3D(ct_p.X - radius * ratio, ct_p.Y - radius * ratio, Z + 0.1);
            Point3D tx_p1 = new Point3D(ct_p.X + radius * ratio, ct_p.Y - radius * ratio, Z + 0.1);
            Point3D tx_p2 = new Point3D(ct_p.X + radius * ratio, ct_p.Y + radius * ratio, Z + 0.1);
            Point3D tx_p3 = new Point3D(ct_p.X - radius * ratio, ct_p.Y + radius * ratio, Z + 0.1);
#endif

            Viewport2DVisual3D v2dVisual = make2DImageVisual3D(tx_p0, tx_p1, tx_p2, tx_p3, img_path);
            tempModel.Children.Add(v2dVisual);

            return tempModel;
        }

        private Viewport2DVisual3D make2DTextVisual3D(Point3D p0, Point3D p1, Point3D p2, Point3D p3, String str, int font_size)
        {
            //top side triangles

            //   tx_p0____________tx_p3  => Y
            //      /             /
            //     /     Text    / 
            //tx_p1_____________tx_p2 
            //
            // ||
            // X


            MeshGeometry3D topMesh = new MeshGeometry3D();

            topMesh.Positions.Add(p0);
            topMesh.Positions.Add(p1);
            topMesh.Positions.Add(p2);
            topMesh.Positions.Add(p3);

            topMesh.TriangleIndices.Add(0);
            topMesh.TriangleIndices.Add(1);
            topMesh.TriangleIndices.Add(2);

            topMesh.TriangleIndices.Add(2);
            topMesh.TriangleIndices.Add(3);
            topMesh.TriangleIndices.Add(0);

            topMesh.TextureCoordinates.Add(new Point(0, 0));
            topMesh.TextureCoordinates.Add(new Point(0, 1));
            topMesh.TextureCoordinates.Add(new Point(1, 1));
            topMesh.TextureCoordinates.Add(new Point(1, 0));

    
            topMesh.Normals.Add(new Vector3D(0, 0, 1));

            Viewport2DVisual3D v2dVisual = new Viewport2DVisual3D();
            v2dVisual.Geometry = topMesh;

            DiffuseMaterial diff_mert = new DiffuseMaterial(Brushes.White);
            Viewport2DVisual3D.SetIsVisualHostMaterial(diff_mert, true);
            v2dVisual.Material = diff_mert;
#if true
            TextBlock tx = new TextBlock();
            tx.Text = str;
            tx.Foreground = Brushes.White;
            tx.HorizontalAlignment = HorizontalAlignment.Center;
            tx.VerticalAlignment = VerticalAlignment.Center;
            int f_size = font_size;
            tx.FontSize = 10;
            tx.FontWeight = FontWeights.Bold;
            //tx.MouseLeftButtonDown += text_EventBubbling;
            //tx.MouseMove += text_EventBubbling;
            //tx.MouseLeave += text_EventBubbling;

            v2dVisual.Visual = tx;
#else
            Image img = new Image();
            //BitmapImage bmp = new BitmapImage(new Uri(img_path));
            BitmapImage bmp = new BitmapImage(new Uri("C:/Users/Administrator/Source/Workspaces/I2MS2/I2MS2/I2MS2/Icons/fp_16.png"));
            ImageSource img_src = bmp;
            img.Source = img_src;

            v2dVisual.Visual =img;
#endif
            return v2dVisual;
        }

        //public void text_EventBubbling(object sender, RoutedEventArgs e)
        //{
        //    tx_Console
        //    // 버블링 유지
        //    e.Handled = false;
        //}



        private Viewport2DVisual3D make2DImageVisual3D(Point3D p0, Point3D p1, Point3D p2, Point3D p3, String img_path)
        {
            //top side triangles

            //   tx_p0____________tx_p3  => Y
            //      /             /
            //     /     Text    / 
            //tx_p1_____________tx_p2 
            //
            // ||
            // X

            MeshGeometry3D topMesh = new MeshGeometry3D();

            topMesh.Positions.Add(p0);
            topMesh.Positions.Add(p1);
            topMesh.Positions.Add(p2);
            topMesh.Positions.Add(p3);

            topMesh.TriangleIndices.Add(0);
            topMesh.TriangleIndices.Add(1);
            topMesh.TriangleIndices.Add(2);

            topMesh.TriangleIndices.Add(2);
            topMesh.TriangleIndices.Add(3);
            topMesh.TriangleIndices.Add(0);

            topMesh.TextureCoordinates.Add(new Point(0, 0));
            topMesh.TextureCoordinates.Add(new Point(0, 1));
            topMesh.TextureCoordinates.Add(new Point(1, 1));
            topMesh.TextureCoordinates.Add(new Point(1, 0));


            topMesh.Normals.Add(new Vector3D(0, 0, 1));

            Viewport2DVisual3D v2dVisual = new Viewport2DVisual3D();
            v2dVisual.Geometry = topMesh;

            DiffuseMaterial diff_mert = new DiffuseMaterial(Brushes.White);
            Viewport2DVisual3D.SetIsVisualHostMaterial(diff_mert, true);
            v2dVisual.Material = diff_mert;

            //Image img = new Image();
            //BitmapImage bmp = new BitmapImage(new Uri(img_path, UriKind.RelativeOrAbsolute));
            ////BitmapImage bmp = new BitmapImage(new Uri("C:/Users/Administrator/Source/Workspaces/I2MS2/I2MS2/I2MS2/Icons/fp_16.png"));
            //ImageSource img_src = bmp;
            //img.Source = img_src;

            Image img = new Image();
            img.Width = 16;     // 64
            img.Height = 16;    // 64
            BitmapImage bmp = new BitmapImage(new Uri(img_path, UriKind.RelativeOrAbsolute));
            //BitmapImage bmp = new BitmapImage(new Uri("C:/Users/Administrator/Source/Workspaces/I2MS2/I2MS2/I2MS2/Images/서울시청_004.jpg"));
            img.Source = bmp;

            v2dVisual.Visual =img;

            return v2dVisual;
        }

        public void addWallToModel3d(ModelVisual3D model, 
            Point3D p0, Point3D p1, Point3D p2, Point3D p3, Point3D p4, Point3D p5, Point3D p6, Point3D p7, 
            Brush brush, Double alpha)
        {
            //맞는지 확인 필요
            //
            //    p4____________p5
            //   /             /|
            //  /             / |
            //p7_____________p6 |
            // |             |  |
            // |  p0_ _ _ _ _| _p1
            // | /           | /
            // |/            |/
            //p3_____________p2

            Model3DGroup cube = new Model3DGroup();
            if (alpha == 1)
            {
                //front side triangles
                CreateQuadrangleModel(cube, p1, p5, p6, p2, brush);
                //right side triangles
                CreateQuadrangleModel(cube, p2, p6, p7, p3, brush);
                //back side triangles
                CreateQuadrangleModel(cube, p0, p3, p7, p4, brush);
                //left side triangles
                CreateQuadrangleModel(cube, p0, p4, p5, p1, brush);
                //top side triangles
                CreateQuadrangleModel(cube, p5, p4, p7, p6, brush);
                //CreateQuadrangleModel(cube, p5, p4, p7, p6, Brushes.Black);
                //bottom side triangles
                CreateQuadrangleModel(cube, p0, p3, p2, p1, brush);
                model.Content = cube;
            }
            else // 뷰포트3d 가 느려질 경우 하위 마킹 처리 
            {

                //front side triangles
                MeshGeometry3D frontMesh = new MeshGeometry3D();
                AddQuadrangleMesh2(p1, p5, p6, p2, frontMesh);
                Viewport2DVisual3D frontView = CreateViewport2DVisual3D(frontMesh, calculate2DXY(p1, p5, p6), brush, alpha);
                model.Children.Add(frontView);

                //right side triangles
                MeshGeometry3D rightMesh = new MeshGeometry3D();
                AddQuadrangleMesh2(p2, p6, p7, p3, rightMesh);
                Viewport2DVisual3D rightView = CreateViewport2DVisual3D(rightMesh, calculate2DXY(p1, p5, p6), brush, alpha);
                model.Children.Add(rightView);

                //back side triangles
                MeshGeometry3D backMesh = new MeshGeometry3D();
                AddQuadrangleMesh2(p0, p3, p7, p4, backMesh);
                Viewport2DVisual3D backView = CreateViewport2DVisual3D(backMesh, calculate2DXY(p3, p7, p4), brush, alpha);
                model.Children.Add(backView);

                //left side triangles
                MeshGeometry3D LeftMesh = new MeshGeometry3D();
                AddQuadrangleMesh2(p0, p4, p5, p1, LeftMesh);
                Viewport2DVisual3D leftView = CreateViewport2DVisual3D(LeftMesh, calculate2DXY(p0, p4, p5), brush, alpha);
                model.Children.Add(leftView);

                //top side triangles
                MeshGeometry3D topMesh = new MeshGeometry3D();
                AddQuadrangleMesh2(p5, p4, p7, p6, topMesh);
                Viewport2DVisual3D topView = CreateViewport2DVisual3D(topMesh, calculate2DXY(p5, p4, p7), brush, alpha);
                //Viewport2DVisual3D topView = CreateViewport2DVisual3D(topMesh, calculate2DXY(p5, p4, p7), Brushes.Black, alpha);
                model.Children.Add(topView);

                //bottom side triangles
                MeshGeometry3D bottomMesh = new MeshGeometry3D();
                AddQuadrangleMesh2(p0, p3, p2, p1, bottomMesh);
                Viewport2DVisual3D bottomView = CreateViewport2DVisual3D(bottomMesh, calculate2DXY(p0, p3, p2), brush, alpha);
                model.Children.Add(bottomView);
            }

        }

        // 벽을 그리기 위한 계산 함수 
        private void getPointForCubeByWallData(WallDraw w, double Z,
            out Point3D P0, out Point3D P1, out Point3D P2, out Point3D P3, out Point3D P4, out Point3D P5, out Point3D P6, out Point3D P7)
        {
            Point wp1 = w.start_p;
            Point wp2 = w.end_p;

            Point startPosition = new Point();
            Point endPosition = new Point();

            //double thiness = drawDataMgr.get3DValue_FromVMValue(w.thickness);
            double thiness = w.thickness;

            double _pointToPointY = wp2.X - wp1.X;
            if (_pointToPointY > 0)
            {
                startPosition.X = wp1.Y;
                startPosition.Y = wp1.X;
                endPosition.X = wp2.Y;
                endPosition.Y = wp2.X;
            }
            else
            {
                startPosition.X = wp2.Y;
                startPosition.Y = wp2.X;
                endPosition.X = wp1.Y;
                endPosition.Y = wp1.X;
            }

            //normal cube
            //double _wallHeght = drawDataMgr.get3DValue_FromVMValue(w.height);
            double _wallHeght = w.height;
            double _startX, _startY;
            double _endX, _endY;

            _startX = startPosition.X;
            _startY = startPosition.Y;
            _endX = endPosition.X;
            _endY = endPosition.Y;

            // It is term between start to end
            double _termX = endPosition.X - startPosition.X;
            double _termY = endPosition.Y - startPosition.Y;

            // Caculate 
            double _radian = Math.Atan(_termY / _termX);
           
            // Point move lenght by Wallthiness and radian
            double A = Math.Abs(thiness * (Math.Cos(AngleToRadian(90) - _radian)));
            double B = Math.Abs(thiness * (Math.Sin(AngleToRadian(90) - _radian)));

            if (_termX < 0)
            {
                B = -B;
            }

            double _2D_X0 = _startX - A / 2;
            double _2D_X1 = _startX + A / 2;
            double _2D_X2 = _endX - A / 2;
            double _2D_X3 = _endX + A / 2;

            double _2D_Y0 = _startY + B / 2;
            double _2D_Y1 = _startY - B / 2;
            double _2D_Y2 = _endY + B / 2;
            double _2D_Y3 = _endY - B / 2;

            Point3D _p0 = new Point3D(_2D_X0, _2D_Y0, Z);
            Point3D _p1 = new Point3D(_2D_X2, _2D_Y2, Z);
            Point3D _p2 = new Point3D(_2D_X3, _2D_Y3, Z);
            Point3D _p3 = new Point3D(_2D_X1, _2D_Y1, Z);
            Point3D _p4 = new Point3D(_2D_X0, _2D_Y0, Z + _wallHeght);
            Point3D _p5 = new Point3D(_2D_X2, _2D_Y2, Z + _wallHeght);
            Point3D _p6 = new Point3D(_2D_X3, _2D_Y3, Z + _wallHeght);
            Point3D _p7 = new Point3D(_2D_X1, _2D_Y1, Z + _wallHeght);

            P0 = new Point3D(_p0.X , _p0.Y  , _p0.Z  );
            P1 = new Point3D(_p1.X , _p1.Y  , _p1.Z  );
            P2 = new Point3D(_p2.X , _p2.Y  , _p2.Z  );
            P3 = new Point3D(_p3.X , _p3.Y  , _p3.Z  );
            P4 = new Point3D(_p4.X , _p4.Y  , _p4.Z  );
            P5 = new Point3D(_p5.X , _p5.Y  , _p5.Z  );
            P6 = new Point3D(_p6.X , _p6.Y  , _p6.Z  );
            P7 = new Point3D(_p7.X , _p7.Y  , _p7.Z  );

        }

        // 유저포트 라인 그리기 
        private void getPointForLine(Point st_p, Point ed_p, Double thiness,
            out Point3D P0, out Point3D P1, out Point3D P2, out Point3D P3)
        {
            Point wp1 = st_p;
            Point wp2 = ed_p;

            Point startPosition = new Point();
            Point endPosition = new Point();
  
            double _pointToPointY = wp2.X - wp1.X;
            if (_pointToPointY >= 0)
            {
                startPosition.X = wp1.Y;
                startPosition.Y = wp1.X;
                endPosition.X = wp2.Y;
                endPosition.Y = wp2.X;
            }
            else
            {
                startPosition.X = wp2.Y;
                startPosition.Y = wp2.X;
                endPosition.X = wp1.Y;
                endPosition.Y = wp1.X;
            }

            //normal cube
            //double _wallHeght = drawDataMgr.get3DValue_FromVMValue(w.height);
            //double _wallHeght = height;
            double _startX, _startY;
            double _endX, _endY;

            _startX = startPosition.X;
            _startY = startPosition.Y;
            _endX = endPosition.X;
            _endY = endPosition.Y;

            // It is term between start to end
            double _termX = endPosition.X - startPosition.X;
            double _termY = endPosition.Y - startPosition.Y;


            // Caculate 
            double _radian =0;

            if (_termY == 0)
                _radian = AngleToRadian(90);
            else if (_termX == 0)
                _radian = AngleToRadian(0);
            else 
                _radian = Math.Atan(_termY / _termX);
            

            // Point move lenght by Wallthiness and radian
            double A = Math.Abs(thiness * (Math.Cos(AngleToRadian(90) - _radian)));
            double B = Math.Abs(thiness * (Math.Sin(AngleToRadian(90) - _radian)));

            //double A = Math.Abs(thiness * (Math.Sin(AngleToRadian(90) - _radian)));
            //double B = Math.Abs(thiness * (Math.Cos (AngleToRadian(90) - _radian)));
            

            if (_termX < 0)
            {
                B = -B;
            }

            //    p0----st_p--------p3 => Y
            //   /        /        /
            //  /        /        /
            // /        /        /
            ///        /        /
            //p1-----ed_p------p2
            //X
            double _2D_X0 = _startX + A / 2;
            double _2D_X1 = _endX + A / 2;
            double _2D_X2 = _endX - A / 2;
            double _2D_X3 = _startX - A / 2;

            double _2D_Y0 = _startY - B / 2;
            double _2D_Y1 = _endY - B / 2;
            double _2D_Y2 = _endY + B / 2;
            double _2D_Y3 = _startY + B / 2;

            //Point3D _p0 = new Point3D();
            //Point3D _p1 = new Point3D(_2D_X2, _2D_Y2, 0);
            //Point3D _p2 = new Point3D(_2D_X3, _2D_Y3, 0);
            //Point3D _p3 = new Point3D(_2D_X1, _2D_Y1, 0);


            P0 = new Point3D(_2D_X0, _2D_Y0, 0.1);
            P1 = new Point3D(_2D_X1, _2D_Y1, 0.1);
            P2 = new Point3D(_2D_X2, _2D_Y2, 0.1);
            P3 = new Point3D(_2D_X3, _2D_Y3, 0.1);
           
        }

        // 랙모양 포인트 얻기 
        private void getPointForCubeByRackData(Point p, Size s, Double h, double Z,
out Point3D P0, out Point3D P1, out Point3D P2, out Point3D P3, out Point3D P4, out Point3D P5, out Point3D P6, out Point3D P7)
        {
            Point sp = new Point(p.Y, p.X);
            Point ep = new Point(p.Y + s.Height, p.X + s.Width);


            double _2D_X0 = sp.X;
            double _2D_X1 = sp.X;
            double _2D_X2 = ep.X;
            double _2D_X3 = ep.X;

            double _2D_Y0 = sp.Y;
            double _2D_Y1 = ep.Y;
            double _2D_Y2 = ep.Y;
            double _2D_Y3 = sp.Y;

            Point3D _p0 = new Point3D(_2D_X0, _2D_Y0, Z);
            Point3D _p1 = new Point3D(_2D_X1, _2D_Y1, Z);
            Point3D _p2 = new Point3D(_2D_X2, _2D_Y2, Z);
            Point3D _p3 = new Point3D(_2D_X3, _2D_Y3, Z);
            Point3D _p4 = new Point3D(_2D_X0, _2D_Y0, Z + h);
            Point3D _p5 = new Point3D(_2D_X1, _2D_Y1, Z + h);
            Point3D _p6 = new Point3D(_2D_X2, _2D_Y2, Z + h);
            Point3D _p7 = new Point3D(_2D_X3, _2D_Y3, Z + h);


            P0 = new Point3D(_p0.X, _p0.Y, _p0.Z);
            P1 = new Point3D(_p1.X, _p1.Y, _p1.Z);
            P2 = new Point3D(_p2.X, _p2.Y, _p2.Z);
            P3 = new Point3D(_p3.X, _p3.Y, _p3.Z);
            P4 = new Point3D(_p4.X, _p4.Y, _p4.Z);
            P5 = new Point3D(_p5.X, _p5.Y, _p5.Z);
            P6 = new Point3D(_p6.X, _p6.Y, _p6.Z);
            P7 = new Point3D(_p7.X, _p7.Y, _p7.Z);
        }

        // 삼각형을 사각형으로 라이브러리화 
        private Model3DGroup CreateQuadrangleModel(Model3DGroup _model, Point3D p0, Point3D p1, Point3D p2, Point3D p3, Brush brush)
        {
            MeshGeometry3D mesh = CreateQuadrangleMesh(p0, p1, p2, p3);

            Material material = new DiffuseMaterial(brush);
            GeometryModel3D model = new GeometryModel3D(mesh, material);
         
            _model.Children.Add(model);
            return _model;
        }
        // 삼각형 그리기 두개 
        private MeshGeometry3D CreateQuadrangleMesh(Point3D p0, Point3D p1, Point3D p2, Point3D p3)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);
            mesh.Positions.Add(p3);


            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);

            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);

            Vector3D normal = calculateNormal(p0, p1, p2);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);

            Vector3D normal2 = calculateNormal(p0, p1, p2);
            mesh.Normals.Add(normal2);
            mesh.Normals.Add(normal2);
            mesh.Normals.Add(normal2);

            // 객체에 표시되는 2D의 자리 romee
            mesh.TextureCoordinates = new PointCollection(
                new Point[] { new Point(0, 0), 
                              new Point(0, 1), 
                              new Point(1, 1), 
                              new Point(1, 0) });

            return mesh;
        }
        // 사용안함 
        private MeshGeometry3D AddQuadrangleMesh2(Point3D p0, Point3D p1, Point3D p2, Point3D p3, MeshGeometry3D mesh)
        {
            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);
            mesh.Positions.Add(p3);


            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);

            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);

            

            Vector3D normal = calculateNormal(p0, p1, p2);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);

            Vector3D normal2 = calculateNormal(p0, p1, p2);
            mesh.Normals.Add(normal2);
            mesh.Normals.Add(normal2);
            mesh.Normals.Add(normal2);


            mesh.TextureCoordinates = new PointCollection(
                new Point[] { new Point(0, 0), 
                              new Point(0, 1), 
                              new Point(1, 1), 
                              new Point(1, 0) });

            return mesh;
        }

        // 삼각현 그리기 
        private Model3DGroup createTriangleModel(Point3D p0, Point3D p1, Point3D p2)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            Vector3D normal = calculateNormal(p0, p1, p2);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);
            Material material = new DiffuseMaterial(
                new SolidColorBrush(Colors.Gray));
            GeometryModel3D model = new GeometryModel3D(
                mesh, material);
            Model3DGroup group = new Model3DGroup();
            group.Children.Add(model);
            return group;
        }

        // 투명도 
        private Viewport2DVisual3D CreateViewport2DVisual3D(MeshGeometry3D mesh, Point RectSize, Brush brush, double opacity)
        {
            var view = new Viewport2DVisual3D();
            view.Geometry = mesh;
            var viewMaterial = new DiffuseMaterial(brush);
            Viewport2DVisual3D.SetIsVisualHostMaterial(viewMaterial, true);
            view.Material = viewMaterial;
            view.Visual = new Rectangle { Fill = brush, Height = RectSize.X, Width = RectSize.Y, Opacity = opacity };
            return view;
        }

        // 텍스트 처리 
        private Viewport2DVisual3D CreateViewport2DVisual3DText(MeshGeometry3D mesh, Point RectSize, Brush brush, String str)
        {
            TextBlock tx = new TextBlock();
            //tx.Text = str;
            tx.Text = "ttt";
            tx.Foreground = Brushes.Black;
            tx.FontSize = 4;
            tx.FontWeight = FontWeights.Bold;
            VisualBrush vb = new VisualBrush();

        
            vb.Visual = tx;

            var view = new Viewport2DVisual3D();
            view.Geometry = mesh;
            var viewMaterial = new DiffuseMaterial(vb);
            Viewport2DVisual3D.SetIsVisualHostMaterial(viewMaterial, true);
            view.Material = viewMaterial;
            return view;
        }
        // 3D -> 2D 로 
        private Point calculate2DXY(Point3D p0, Point3D p1, Point3D p2)
        {
            Point XY = new Point();

            double _termX1 = p1.X - p0.X;
            double _termY1 = p1.Y - p0.Y;
            double _termZ1 = p1.Z - p0.Z;
            double _termX2 = p2.X - p1.X;
            double _termY2 = p2.Y - p1.Y;
            double _termZ2 = p2.Z - p1.Z;


            double _x2D = Math.Pow((Math.Pow(_termX1, 2) + Math.Pow(_termY1, 2) + Math.Pow(_termZ1, 2)), 0.5);
            double _y2D = Math.Pow((Math.Pow(_termX2, 2) + Math.Pow(_termY2, 2) + Math.Pow(_termZ2, 2)), 0.5);


            XY.X = _x2D;
            XY.Y = _x2D;

            return XY;
        }

        // 자신의 포인트에 따른 뷰벡터 노멀 ..
        private Vector3D calculateNormal(Point3D p0, Point3D p1, Point3D p2)
        {
            Vector3D v0 = new Vector3D(
                p1.X - p0.X, p1.Y - p0.Y, p1.Z - p0.Z);
            Vector3D v1 = new Vector3D(
                p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            return Vector3D.CrossProduct(v0, v1);
        }
        // 변경 
        private double AngleToRadian(double _angle)
        {
            return _angle * (Math.PI / 180);
        }

        private double RadianToAngle(double _radian)
        {
            return _radian * (180/Math.PI);
        }

        // 원기둥 그릴때 마지막 위치 찾기 
        private Point makeTriangleLastPoint(Point p0, Point p1, Double angle)
        {
            Double p01x = Math.Abs(p1.X - p0.X);
            Double p01y = Math.Abs(p1.Y - p0.Y);

            Double dx01 = p1.X - p0.X;
            Double dy01 = p1.Y - p0.Y;

            Double c = Math.Pow((Math.Pow(p01x, 2) + Math.Pow(p01y, 2)), 0.5);
            Double ceta = AngleToRadian(angle);

            Double ceta2;
            Double ceta3;

            Double p02x;
            Double p02y;

            Point p2 = new Point();

            if ((dx01 >= 0) && (dy01 >= 0))
            {
                ceta2 = Math.Abs(Math.Atan(p01y / p01x));
                ceta3 = AngleToRadian(90) - ceta - ceta2;
                p02x = c * Math.Sin(ceta3);
                p02y = c * Math.Cos(ceta3);

                p2.X = p0.X + p02x;
                p2.Y = p0.Y + p02y;
            }
            else if ((dx01 < 0) && (dy01 >= 0))
            {
                ceta2 = Math.Abs(Math.Atan(p01x / p01y));
                ceta3 = AngleToRadian(90) - ceta - ceta2;
                p02x = c * Math.Cos(ceta3);
                p02y = c * Math.Sin(ceta3);

                p2.X = p0.X - p02x;
                p2.Y = p0.Y + p02y;
            }
            else if ((dx01 < 0) && (dy01 < 0))
            {
                ceta2 = Math.Abs(Math.Atan(p01y / p01x));
                ceta3 = AngleToRadian(90) - ceta - ceta2;
                p02x = c * Math.Sin(ceta3);
                p02y = c * Math.Cos(ceta3);

                p2.X = p0.X - p02x;
                p2.Y = p0.Y - p02y;
            }
            else
            {
                ceta2 = Math.Abs(Math.Atan(p01x / p01y));
                ceta3 = AngleToRadian(90) - ceta - ceta2;
                p02x = c * Math.Cos(ceta3);
                p02y = c * Math.Sin(ceta3);

                p2.X = p0.X + p02x;
                p2.Y = p0.Y - p02y;
            }
            return p2;
        }

#if false

        private void initBottom()
        {
            ModelVisual3D bottom = new ModelVisual3D();

            Point p = new Point(0, 0);
            Size size = new Size();
            size.Height = 1200;
            size.Width = 1200;
            bottom = createBottom(p, size, bottom);
            //tempBottom.Transform = rotateTransform;
            viewport.Children.Add(bottom);
        }

        public ModelVisual3D createBottom(Point mapMargin, Size mapSize, ModelVisual3D model)
        {
            Model3DGroup bottom = new Model3DGroup();


            Point3D p0 = new Point3D(mapMargin.Y, mapMargin.X, 0);
            Point3D p1 = new Point3D(mapMargin.Y + mapSize.Height, mapMargin.X, 0);
            Point3D p2 = new Point3D(mapMargin.Y + mapSize.Height, mapMargin.X + mapSize.Width, 0);
            Point3D p3 = new Point3D(mapMargin.Y, mapMargin.X + mapSize.Width, 0);

            bottom.Children.Add(createTriangleModel(p3, p0, p1));
            bottom.Children.Add(createTriangleModel(p1, p2, p3));
            model.Content = bottom;
            return model;
        } 
#endif

    }
}
