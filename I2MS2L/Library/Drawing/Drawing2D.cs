using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


using I2MS2.Models;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using I2MS2.UserControls.Drawing;
using System.Windows.Input;


namespace I2MS2.Library.Drawing
{
    // 2D 자산 그리기 
    class Drawing2D
    {
        public delegate void PickPointEventHander(object obj);
        public event PickPointEventHander PickWallPointEvent;

        public delegate void ConnectWallPointEventHander(object obj);
        public event ConnectWallPointEventHander ConnectWallPointEvent;

        public delegate void SelectWallEventHander(object obj);
        public event SelectWallEventHander SelectWallEvent;
        
        private Canvas canvas;
        private Canvas sub_canvas;
        private Line tmp_wall;
        public Brush wall_brush;

        private List<Path> path_c_list = new List<Path>();
        private Dictionary<int, Path> path_dic = new Dictionary<int, Path>();

        public Drawing2D(Canvas _canvas)
        {
            canvas = _canvas;
        }

        public void setSubCanvas(Canvas _canvas)
        {
            sub_canvas =_canvas;
        }

        public void removeAll()
        {
            canvas.Children.Clear();

            if(sub_canvas != null)
            {
                sub_canvas.Children.Clear();
                up_line_dic.Clear();
            }

        }

        #region AssetControl

        public void addAsset(Point p, Size s, Double h, AssetTreeType type, int id, string asset_name)
        {
#if false
            String img_path;
            Brush back_brush;
            switch (type)
            {
                case AssetTreeType.FacePlate:
                    img_path = "C:/Users/Administrator/Source/Workspaces/I2MS2/I2MS2/I2MS2/Icons/fp_16.png";
                    back_brush = Brushes.Blue;
                    break;
                case AssetTreeType.MutoaBox:
                    img_path = "C:/Users/Administrator/Source/Workspaces/I2MS2/I2MS2/I2MS2/Icons/mb_16.png";
                    back_brush = Brushes.Purple;
                    break;
                case AssetTreeType.ConsolidationPoint:
                    img_path = "C:/Users/Administrator/Source/Workspaces/I2MS2/I2MS2/I2MS2/Icons/cp_16.png";
                    back_brush = Brushes.Green;
                    break;
                case AssetTreeType.EnviromentBox:
                    img_path = "C:/Users/Administrator/Source/Workspaces/I2MS2/I2MS2/I2MS2/Icons/etc_16.png";
                    back_brush = Brushes.Yellow;
                    break;
                default:
                    img_path = "C:/Users/Administrator/Source/Workspaces/I2MS2/I2MS2/I2MS2/Icons/etc_16.png";
                    back_brush = Brushes.SkyBlue;
                    break;
            } 
#endif


            //add canvas 2d wall
            DrawingItem2DAsset _uc_ast = new DrawingItem2DAsset();
            _uc_ast._txtName.Text = string.Format(asset_name);
            _uc_ast._bdAsset.Width = s.Width;
            _uc_ast._bdAsset.Height = s.Height;
           
#if false
            BitmapImage bmp = new BitmapImage(new Uri(img_path));
            ImageSource img_src = bmp;
            _uc_ast._imgAsset.Source = img_src;
            _uc_ast._imgAsset.Width = s.Width;
            _uc_ast._imgAsset.Height = s.Height;
#endif

            _uc_ast.HorizontalAlignment = HorizontalAlignment.Left;
            _uc_ast.VerticalAlignment = VerticalAlignment.Top;

            Thickness margin = new Thickness(p.X, p.Y, 0, 0);
            _uc_ast.Margin = margin;
            _uc_ast.Name = string.Format("Asset{0}", id);

            canvas.Children.Add(_uc_ast);
        }


        public void moveAsset(Vector v, int id)
        {
            foreach (var at in canvas.Children)
            {
                if (at is DrawingItem2DAsset)
                {
                    DrawingItem2DAsset ast = at as DrawingItem2DAsset;
                    if (ast.Name == string.Format("Asset{0}", id))
                    {
                        Thickness th = ast.Margin;
                        th.Left += v.X;
                        th.Top += v.Y;
                        ast.Margin = th;
                        break;
                    }
                }
            }
        }


    
        public Point getAssetPoint(int id)
        {
            foreach (var at in canvas.Children)
            {
                if (at is DrawingItem2DAsset)
                {
                    DrawingItem2DAsset ast = at as DrawingItem2DAsset;
                    if (ast.Name == string.Format("Asset{0}", id))
                    {
                        Point p = new Point();
                        p.X = ast.Margin.Left;
                        p.Y = ast.Margin.Top;
                        return p;
                    }
                }
            }
            return new Point(0, 0);
        }


        private DrawingItem2DAsset findAssetByID(int id)
        {
            foreach (var at in canvas.Children)
            {
                if (at is DrawingItem2DAsset)
                {
                    DrawingItem2DAsset ast = at as DrawingItem2DAsset;
                    string num_string = string.Format("Asset{0}", id);
                    if (ast.Name == num_string)
                    {
                        return ast;
                    }
                }
            }
            return null;
        }


        public void selectEditAsset(int id)
        {
            DrawingItem2DAsset ast = findAssetByID(id);
            if (ast != null)
            {
                ast._bdAsset.BorderThickness = new Thickness(2,2,2,2);
                ast._bdAsset.BorderBrush = Brushes.Red;
            }
        }

        public void releaseEditAsset(int id)
        {
            DrawingItem2DAsset ast = findAssetByID(id);
            if (ast != null)
            {
                ast._bdAsset.BorderThickness = new Thickness(0);  
            }
        }

        public void selectAsset(int id)
        {
            DropShadowEffect shadowEffect = new DropShadowEffect();
            shadowEffect.Color = Colors.White;
            shadowEffect.BlurRadius = 30;
            shadowEffect.Direction = 300;
            shadowEffect.ShadowDepth = 2;

            DrawingItem2DAsset ast = findAssetByID(id);
            //Rectangle rect = findAssetByID(id);
            if (ast != null)
                ast.Effect = shadowEffect;

            //    rect.StrokeThickness = 2;
            //    rect.Stroke = Brushes.Red;
        }

        public void releaseAsset(int id)
        {
            DrawingItem2DAsset ast = findAssetByID(id);
            if (ast != null)
            {
                ast.Effect = null;
                ast._bdAsset.BorderThickness = new Thickness(0);
            }
        }
        
        #endregion

        #region UserPortControl
        Dictionary<int, Line> up_line_dic = new Dictionary<int, Line>();



        private void addUPLine(int id, Point p, Size s, Point parent_p, Size parent_s)
        {
            Line l = new Line();
            l.X1 = parent_p.X + parent_s.Width / 2;
            l.Y1 = parent_p.Y + parent_s.Height / 2;
            l.X2 = p.X + s.Width / 2;
            l.Y2 = p.Y + s.Height / 2;
            l.Stroke = Brushes.Gray;
            l.StrokeThickness = 1;

            up_line_dic.Add(id, l);
            sub_canvas.Children.Add(l);
        }

        private void removeUPLine(int id)
        {
            Line l = up_line_dic[id];
            if (l != null)
            {
                sub_canvas.Children.Remove(l);
                up_line_dic.Remove(id);
            }
        }


        private void moveUPLine(int id, Vector v)
        {
            Line l = up_line_dic[id];
            if (l != null)
            {
                l.X2 += v.X;
                l.Y2 += v.Y;
            }
        }


        private void moveUPLineAt(int id, Point p, Size s, Point parent_p, Size parent_s)
        {
             Line l = up_line_dic[id];
            if (l != null)
            {
                l.X1 = parent_p.X + parent_s.Width / 2;
                l.Y1 = parent_p.Y + parent_s.Height / 2;
                l.X2 = p.X + s.Width / 2;
                l.Y2 = p.Y + s.Height / 2;
            }
        }


        public void addUserPort(Point p,Size s, int id, int number, Point parent_p, Size parent_s)
        {
            DrawingItem2DUserPort _uc_up = new DrawingItem2DUserPort();
            _uc_up.HorizontalAlignment = HorizontalAlignment.Left;
            _uc_up.VerticalAlignment = VerticalAlignment.Top;
            Thickness margin = new Thickness(p.X, p.Y, 0, 0);
            _uc_up.Margin = margin;
            _uc_up.Width = s.Width;
            _uc_up.Height = s.Height;
            _uc_up._txtNumber.Text = string.Format("{0}", number);
            _uc_up.Name = string.Format("UserPort{0}", id);

            canvas.Children.Add(_uc_up);

            addUPLine(id, p, s, parent_p, parent_s);
        }


        public void removeUserPort(int id)
        {
            foreach(var at in canvas.Children)
            {
                if( at is Ellipse)
                {
                    DrawingItem2DUserPort up = at as DrawingItem2DUserPort;
                    if (up.Name == string.Format("UserPort{0}", id))
                    {
                        canvas.Children.Remove(up);
                        removeUPLine(id);
                        break;
                    }
                }
            }
        }

        public void moveUserPort(Vector v, int id)
        {
            foreach (var at in canvas.Children)
            {
                if (at is DrawingItem2DUserPort)
                {
                    DrawingItem2DUserPort up = at as DrawingItem2DUserPort;
                    if (up.Name == string.Format("UserPort{0}", id))
                    {
                        Thickness th = up.Margin;
                        th.Left += v.X;
                        th.Top += v.Y;
                        up.Margin = th;
                        
                        moveUPLine(id,v);
                        break;
                    }
                }
            }
        }

        public void moveUserPortAt(Point p,Size s, int id, Point parent_p, Size parent_s)
        {
            foreach (var at in canvas.Children)
            {
                if (at is DrawingItem2DUserPort)
                {
                    DrawingItem2DUserPort up = at as DrawingItem2DUserPort;
                    if (up.Name == string.Format("UserPort{0}", id))
                    {
                        Thickness th = up.Margin;
                        th.Left = p.X;
                        th.Top = p.Y;
                        up.Margin = th;
                        moveUPLineAt(id, p,s,parent_p, parent_s);
                        break;
                    }
                }
            }
        }

        public Point getUserPortPoint(int id)
        {
            foreach (var at in canvas.Children)
            {
                if (at is DrawingItem2DUserPort)
                {
                    DrawingItem2DUserPort up = at as DrawingItem2DUserPort;
                    if (up.Name == string.Format("UserPort{0}", id))
                    {
                        Point p = new Point();
                        p.X = up.Margin.Left;
                        p.Y = up.Margin.Top;
                        return p;
                    }
                }
            }
            return new Point(0, 0);
        }


        private DrawingItem2DUserPort findUserPortByID(int id)
        {
            foreach (var at in canvas.Children)
            {
                if (at is DrawingItem2DUserPort)
                {
                    DrawingItem2DUserPort up = at as DrawingItem2DUserPort;
                    string num_string = string.Format("UserPort{0}", id);
                    if (up.Name == num_string)
                    {
                        return up;
                    }
                }
            }
            return null;
        }


        public void selectEditUserPort(int id)
        {
            DrawingItem2DUserPort up = findUserPortByID(id);
            if (up != null)
            {
                up._ellUserPort.StrokeThickness = 2;
                up._ellUserPort.Stroke = Brushes.Red;
            }
        }

        public void releaseEditUserPort(int id)
        {
            DrawingItem2DUserPort up = findUserPortByID(id);
            if (up != null)
            {
                up._ellUserPort.StrokeThickness = 0;
            }
        }



        public void selectUserPort(int id)
        {
            DropShadowEffect shadowEffect = new DropShadowEffect();
            shadowEffect.Color = Colors.White;
            shadowEffect.BlurRadius = 30;
            shadowEffect.Direction = 300;
            shadowEffect.ShadowDepth = 2;

            DrawingItem2DUserPort up = findUserPortByID(id);
            if (up != null)
                up.Effect = shadowEffect;

            //    rect.StrokeThickness = 2;
            //    rect.Stroke = Brushes.Red;
        }

        public void releaseUserPort(int id)
        {
            DrawingItem2DUserPort up = findUserPortByID(id);
            if (up != null)
            {
                up.Effect = null;
                up._ellUserPort.StrokeThickness = 0;
            }
        }
        
        #endregion

        #region RackControl

        public void addRack(Point p, Size s, Double h, Color c, int id, string rack_name)
        {
            DrawingItem2DRack _uc_rack = new DrawingItem2DRack();
            _uc_rack._txtName.Text = string.Format(rack_name);

            _uc_rack._rectRack.Width = s.Width;
            _uc_rack._rectRack.Height = s.Height;
        
            _uc_rack._rectRack.Fill = new SolidColorBrush(c);
            _uc_rack.HorizontalAlignment = HorizontalAlignment.Left;
            _uc_rack.VerticalAlignment = VerticalAlignment.Top;
            
            Thickness margin = new Thickness(p.X, p.Y, 0, 0);
            _uc_rack.Margin = margin;
            _uc_rack.Name = string.Format("Rack{0}", id);

            canvas.Children.Add(_uc_rack);
        }

        public void moveRack(Vector v, int id)
        {
            foreach (var rc in canvas.Children)
            {
                if(rc is DrawingItem2DRack)
                {   
                    DrawingItem2DRack _uc_rack = rc as DrawingItem2DRack;
                    if (_uc_rack.Name == string.Format("Rack{0}", id))
                    {
                        Thickness th = _uc_rack.Margin;
                        th.Left += v.X;
                        th.Top += v.Y;
                        _uc_rack.Margin = th;
                        break;
                    }

                }
            }
        }

        public Point getRackPoint(int id)
        {
            foreach (var rc in canvas.Children)
            {
                if (rc is DrawingItem2DRack)
                {
                    DrawingItem2DRack _uc_rack = rc as DrawingItem2DRack;
                    if (_uc_rack.Name == string.Format("Rack{0}", id))
                    {
                        Point p = new Point();
                        p.X = _uc_rack.Margin.Left;
                        p.Y = _uc_rack.Margin.Top;
                        return p;
                    }
                }
            }
            return new Point(0, 0);
        }


        private DrawingItem2DRack findRackByID(int id)
        {
            foreach (var at in canvas.Children)
            {
                if (at is DrawingItem2DRack)
                {
                    DrawingItem2DRack _uc_rack = at as DrawingItem2DRack;
                    if (_uc_rack.Name == string.Format("Rack{0}", id))
                    {
                        return _uc_rack;
                    }

                }
            }
            return null;
        }


        public void selectEditRack(int id)
        {
            DrawingItem2DRack _uc_rack = findRackByID(id);
            if (_uc_rack != null)
            {
                _uc_rack._rectRack.StrokeThickness = 2;
                _uc_rack._rectRack.Stroke = Brushes.Red;
            }
        }

        public void releaseEditRack(int id)
        {
            DrawingItem2DRack _uc_rack = findRackByID(id);
            if (_uc_rack != null)
            {
                _uc_rack._rectRack.StrokeThickness = 0;
            }
        }



        public void selectRack(int id)
        {
            DropShadowEffect shadowEffect = new DropShadowEffect();
            shadowEffect.Color = Colors.White;
            shadowEffect.BlurRadius = 30;
            shadowEffect.Direction = 300;
            shadowEffect.ShadowDepth = 2;

            DrawingItem2DRack _uc_rack = findRackByID(id);
            if (_uc_rack != null)
                _uc_rack.Effect = shadowEffect;

            //    rect.StrokeThickness = 2;
            //    rect.Stroke = Brushes.Red;
        }

        public void releaseRack(int id)
        {
            DrawingItem2DRack _uc_rack = findRackByID(id);
            if (_uc_rack != null)
            {
                _uc_rack.Effect = null;
                _uc_rack._rectRack.StrokeThickness = 0;
            }
        }
        
        #endregion

        #region WallControl
		public void removeAllWall()
        {
            canvas.Children.Clear();
            path_dic.Clear();
        }

       
        public void removeWallByNumber(int id)
        {
            DrawingEdit2DWall l = findWallById(id);
            canvas.Children.Remove(l);

        }

        public void hideModifyWall(int id)
        {
            DrawingEdit2DWall l = findWallById(id);
            l.hideModifyWall();
        }

        public void modifyWall(WallDraw w)
        {
            DrawingEdit2DWall l = findWallById(w.id);
            l.modifyWall(w);
        }

        public void addWallbyWall(WallDraw w)
        {
            //add canvas 2d wall
            Line l = new Line();
            l.Stroke = new SolidColorBrush(Color.FromArgb(w.colorA, w.colorR, w.colorG, w.colorB));
            l.StrokeThickness = w.thickness;
            l.X1 = w.start_p.X;
            l.Y1 = w.start_p.Y;
            l.X2 = w.end_p.X;
            l.Y2 = w.end_p.Y;
            l.Opacity = w.alpha;

            canvas.Children.Add(l);
        }

        public void addWallbyWallVm(WallDraw w)
        {
            //add canvas 2d wall

            DrawingEdit2DWall dr_wall = new DrawingEdit2DWall(w, null);
            dr_wall.PickWallPointEvent += new DrawingEdit2DWall.PickPointEventHander(pickWallPoint);
            dr_wall.ConnectWallPointEvent += new DrawingEdit2DWall.ConnectWallPointEventHander(connectWallPoint);
            dr_wall.SelectWallEvent += new DrawingEdit2DWall.SelectWallEventHander(selectWallUC);
            canvas.Children.Add(dr_wall);

        }



        //public void addWallbyWallVm(WallDraw w, WallDraw con_w)
        //{
        //    //add canvas 2d wall

        //    DrawingEdit2DWall dr_wall = new DrawingEdit2DWall(w, con_w);
        //    dr_wall.PickWallPointEvent += new DrawingEdit2DWall.PickPointEventHander(pickWallPoint);
        //    canvas.Children.Add(dr_wall);

        //}

        public void addWallCorner(WallCornerDraw wcd)
        {
            if (path_dic.ContainsKey(wcd.id)) return;

            Path path_c = new Path();
            //corner.Fill = new SolidColorBrush(Color.FromArgb(wcd.colorA, wcd.colorR, wcd.colorG, wcd.colorB));
            path_c.Fill = Brushes.Red;
            path_c.HorizontalAlignment = HorizontalAlignment.Left;
            path_c.VerticalAlignment = VerticalAlignment.Top;

            PathGeometry pathGeo = new PathGeometry();

            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = wcd.p_list[0];


            for (int i = 1; i < wcd.p_list.Count;i++ )
            {
                LineSegment lineSeg = new LineSegment(wcd.p_list[i], true);
                pathFigure.Segments.Add(lineSeg);
            }

            pathGeo.Figures.Add(pathFigure);
            path_c.Data = pathGeo;
            path_c.Name = string.Format("wc{0}",wcd.id);
            canvas.Children.Add(path_c);
            //path_c_list.Add(path_c);
            path_dic.Add(wcd.id, path_c);
        }

        public void removeWallCorner(WallCornerDraw wcd)
        {
            //String name = string.Format("wc{0}",wcd.id);
            //Path path_c = path_c_list.Find(at => at.Name == name);
            //if (path_c == null) return;
            if (path_dic.ContainsKey(wcd.id))
            {
                Path path_c = path_dic[wcd.id];
                canvas.Children.Remove(path_c);
                path_dic.Remove(wcd.id);
            }
        }

        private void pickWallPoint(Object obj)
        {
            PickWallPointEvent(obj);
        }

        private void connectWallPoint(Object obj)
        {
            ConnectWallPointEvent(obj);
        }

        private void selectWallUC(Object obj)
        {
            SelectWallEvent(obj);
        }

        public void drawTempWall(Point st_p, Point end_p, Brush brush, Double thick, Double alpha)
        {
            if (tmp_wall != null)
                canvas.Children.Remove(tmp_wall);

            tmp_wall = new Line();
            tmp_wall.Stroke = brush;
            tmp_wall.StrokeThickness = thick;
            tmp_wall.X1 = st_p.X;
            tmp_wall.Y1 = st_p.Y;
            tmp_wall.X2 = end_p.X;
            tmp_wall.Y2 = end_p.Y;
            tmp_wall.Opacity = alpha;
            canvas.Children.Add(tmp_wall);
            //Console.WriteLine("tempWall ({0:f0},{1:f0}) =>({2:f0},{3:f0})", st_p.X, st_p.Y, end_p.X, end_p.Y);
        }

        Ellipse p_el;

        private void wall_MouseMove(object sender, MouseEventArgs e)
        {
            if (!(sender is Line)) return;

            Line l = (Line)sender;
            
            Point p = e.GetPosition(canvas);
            Point target_p = new Point(0, 0) ;
            Double term = 5;
            Point lp1_max = new Point(l.X1 + term, l.Y1 + term);
            Point lp1_min = new Point(l.X1 + term, l.Y1 + term);
            Point lp2_max = new Point(l.X2 + term, l.Y2 + term);
            Point lp2_min = new Point(l.X2 + term, l.Y2 + term);

            if(  (p.X > lp1_min.X)&&(p.Y > lp1_min.Y)
                &&(p.X < lp1_max.X)&&(p.Y < lp1_max.Y))
            {
                target_p.X = l.X1;
                target_p.Y = l.Y1;
            }
            else if((p.X > lp2_min.X)&&(p.Y > lp2_min.Y)
                &&(p.X < lp2_max.X)&&(p.Y < lp2_max.Y))
            {
                target_p.X = l.X2;
                target_p.Y = l.Y2;
            }
            
        }
        private void initCornerP()
        {
            p_el = new Ellipse();
            p_el.Width = 2;
            p_el.Height = 2;
            p_el.Margin = new Thickness(0, 0, 0, 0);

        }

        private void drawCornerP(Point p)
        {
            Thickness margin = new Thickness(p.X +1, p.Y +1, 0,0);

            if (canvas.Children.Contains(p_el) == false)
                canvas.Children.Add(p_el);
        }


        public void removeTempWall()
        {
            if (tmp_wall != null)
                canvas.Children.Remove(tmp_wall);

        }

        public void changeWallColor(int id, Brush brush)
        {
            DrawingEdit2DWall w = findWallById(id);
            w._lDrawing.Stroke = brush;
            //l.Stroke = brush;
        }


        public void changeWallThick(int id, Double thick)
        {
            DrawingEdit2DWall w = findWallById(id);
            w._lDrawing.StrokeThickness = thick;
        }

        public void changeWallAlpha(int id, Double alpha)
        {
            DrawingEdit2DWall w = findWallById(id);
            w._lDrawing.Opacity = alpha;
        }

        public void selectWall(int id)
        {
            DrawingEdit2DWall dw = findWallById(id);
            dw.selectWall();
            //DropShadowEffect shadowEffect = new DropShadowEffect();
            //shadowEffect.Color = Colors.White;
            //shadowEffect.BlurRadius = 50;
            //shadowEffect.Direction = 300;
            //shadowEffect.ShadowDepth = 5;

            //DrawingEdit2DWall w = findWallByNumber(num);
            //w._lDrawing.Effect = shadowEffect;
        }



        public void releaseWall(int id)
        {
            DrawingEdit2DWall dw = findWallById(id);
            dw.releaseWall();
        }


        public DrawingEdit2DWall findWallById(int num)
        {
            foreach (var at in canvas.Children)
            {
                if (at is DrawingEdit2DWall)
                {
                    DrawingEdit2DWall w = at as DrawingEdit2DWall;
                    if (w.w.id == num)
                    {
                        return w; 
                    }
                }
            }
            return null;
        } 
	#endregion

    }
}
