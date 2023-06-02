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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebApi.Models;
using System.Text.RegularExpressions;

namespace I2MS2.UserControls.Drawing
{
    /// <summary>
    /// DrawingView2D.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DrawingView2D : UserControl
    {

        public delegate void SelectedRackHandler(int id);
        public event SelectedRackHandler selectedRackEvent;

        Drawing2D drawer2d;
        DrawingDataManager drawDataMgr;

        List<int> selected_rackid_list;
        Point start_p;


        Size canvas_size;
        
        public string _drawing3d_path;
        public String drawing3d_path {
            get { return _drawing3d_path; }
            set { _drawing3d_path = value; }
        }

        private Boolean is_edit_rack = false;

        public DrawingView2D()
        {
            InitializeComponent();
            drawer2d = new Drawing2D(_canvasDrawing);
            drawDataMgr = new DrawingDataManager();

            selected_rackid_list = new List<int>(); 
        }

        private void _ctlDrawingView2_Loaded(object sender, RoutedEventArgs e)
        {
            //openDrawingFile(_drawing3d_path);
            canvas_size = new Size(_canvasDrawing.ActualWidth, _canvasDrawing.ActualHeight);
        }



        public Boolean openDrawingFile(string path)
        {
            drawer2d.removeAllWall();

            if(File.Exists(path))
            {
                //check canvas size
                canvas_size = new Size(_canvasDrawing.ActualWidth, _canvasDrawing.ActualHeight);
               

                BinaryFormatter openformat = new BinaryFormatter();
                FileStream openStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                List<WallDraw>[] open_w_list = new List<WallDraw>[4];

                open_w_list = (List<WallDraw>[])openformat.Deserialize(openStream);
                
                for (int i = 0; i < 4; i++)
                {
                    foreach (var w in open_w_list[i])
                    {
                        addWall(w);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public void addRackList(List<rack> rk_list)
        {
            foreach(var rk in rk_list)
            {
                addRack(rk);
            }
        }


        public void addWall(WallDraw db_w)
        {
            Point s_p = drawDataMgr.getCanvasPoint_FromVMPoint(canvas_size, db_w.start_p);
            Point e_p = drawDataMgr.getCanvasPoint_FromVMPoint(canvas_size, db_w.end_p);
            Double t = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, db_w.thickness);
            Double h = drawDataMgr.getCanvasValue_FromVMValue(canvas_size, db_w.height);
            WallDraw w_c = new WallDraw()
            {
                start_p = s_p,
                end_p = e_p,
                thickness = t,
                height = h,
                alpha = db_w.alpha,
                colorA = db_w.colorA,
                colorR = db_w.colorR,
                colorG = db_w.colorG,
                colorB = db_w.colorB
            };
            drawer2d.addWallbyWall(w_c);
        }

        public void addRack(rack rk)
        {
            Point p = drawDataMgr.getCanvasPoint_FromVMPoint(canvas_size, new Point(rk.pos_x ?? 0, rk.pos_y ?? 0));
            Size s = drawDataMgr.getCanvasSize_FromVMSize(canvas_size, new Size(1200, 2000));
            Double h = drawDataMgr.getCanvasValue_FromVMValue(canvas_size,2000);
            Color c = Colors.SkyBlue;
            drawer2d.addRack(p, s, h, c, rk.rack_id,rk.rack_name);
        }


        public void selectRack(int rack_id)
        {
            releaseAllRack();

            drawer2d.selectRack(rack_id);
            selected_rackid_list.Add(rack_id);
        }

        public void releaseRack(int rack_id)
        {
            drawer2d.releaseRack(rack_id);
            selected_rackid_list.Remove(rack_id);
        }

        public void releaseAllRack()
        {
            foreach(int at in selected_rackid_list)
            {
                drawer2d.releaseRack(at);
            }
            selected_rackid_list.Clear();
        }

        public void moveSelectRack(Vector v)
        {
            foreach(int at in selected_rackid_list)
            {
                drawer2d.moveRack(v, at);
            }
        }

        public void editRackAllow(Boolean en)
        {
            is_edit_rack = en;
        }


        private int getNumberInStr(string str)
        {
            string str1 = Regex.Replace(str, @"\D","");
            return int.Parse(str1);
        }

        public Point getRackPoint(int rack_id)
        {
            Point p = drawer2d.getRackPoint(rack_id);
            return drawDataMgr.getVMPoint_FromCanvasPoint(canvas_size, p);
        }

        private void _canvasDrawing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.Source is Rectangle)
            {
                Point cur_p = e.GetPosition(_canvasDrawing);
                start_p = cur_p;
                Rectangle rect = e.Source as Rectangle;
                int rack_id = getNumberInStr(rect.Name);
                //selectRack(rack_id);
                selectedRackEvent(rack_id);
            }
        }

        private void _canvasDrawing_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                if(is_edit_rack)
                {
                    Point cur_p = e.GetPosition(_canvasDrawing);
                    Vector v = new Vector();
                    v.X = cur_p.X - start_p.X;
                    v.Y = cur_p.Y - start_p.Y;
                    
                    moveSelectRack(v);
                    start_p = cur_p;
                }
            }
        }

        private void _canvasDrawing_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            releaseAllRack();
        }

        private Point simplePoint(Point src)
        {
            return new Point( (int)(src.X),(int)(src.Y));
        }

      
    }
}
