using I2MS2.Animation;
using I2MS2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace I2MS2.UserControls.Drawing
{
    // 에디터에서 벽을 그릴때 사용함 
    /// <summary>
    /// DrawingEdit2DWall.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DrawingEdit2DWall : UserControl
    {
        public delegate void PickPointEventHander(object obj);
        public event PickPointEventHander PickWallPointEvent;

        public delegate void ConnectWallPointEventHander(object obj);
        public event ConnectWallPointEventHander ConnectWallPointEvent;

        public delegate void SelectWallEventHander(object obj);
        public event SelectWallEventHander SelectWallEvent;

        public WallDraw w;
        private WallDraw pick_w;
        private WallDraw connect_w;
        private SimpleAnimation anim;

        public Boolean is_selected = false;
        Point P1;
        Point P2;

        public DrawingEdit2DWall(WallDraw _w, WallDraw _pick_w)
        {
            InitializeComponent();

            w = _w;

            anim = new SimpleAnimation();
            initWall();

            if (_pick_w != null)
            {
                pick_w = _pick_w;
                //makeWallCornerTriangle();
            }
        }

        // 초기화 
        private void initWall()
        {
            Point P1;
            Point P2;
            Boolean revers = false;
            if (w.start_p.X < w.end_p.X)
            {
                if (w.start_p.Y < w.end_p.Y)
                {
                    P1 = new Point(w.start_p.X, w.start_p.Y);
                    P2 = new Point(w.end_p.X, w.end_p.Y);
                    this.Margin = new Thickness(P1.X, P1.Y, 0, 0);
                    _lDrawing.X1 = 0;
                    _lDrawing.Y1 = 0;
                    _lDrawing.X2 = (P2.X - P1.X);
                    _lDrawing.Y2 = (P2.Y - P1.Y);
                }
                else
                {
                    P1 = new Point(w.start_p.X, w.start_p.Y);
                    P2 = new Point(w.end_p.X, w.end_p.Y);
                    this.Margin = new Thickness(P1.X, P2.Y, 0, 0);
                    _lDrawing.X1 = 0;
                    _lDrawing.Y1 = (P1.Y - P2.Y);
                    _lDrawing.X2 = (P2.X - P1.X);
                    _lDrawing.Y2 = 0;
                }
            }
            else//(w.end_p.X <= w.start_p.X)
            {
                revers = true;
                if (w.end_p.Y < w.start_p.Y)
                {
                    P1 = new Point(w.end_p.X, w.end_p.Y);
                    P2 = new Point(w.start_p.X, w.start_p.Y);
                    this.Margin = new Thickness(P1.X, P1.Y, 0, 0);
                    _lDrawing.X1 = 0;
                    _lDrawing.Y1 = 0;
                    _lDrawing.X2 = (P2.X - P1.X);
                    _lDrawing.Y2 = (P2.Y - P1.Y);
                }
                else
                {
                    P1 = new Point(w.end_p.X, w.end_p.Y);
                    P2 = new Point(w.start_p.X, w.start_p.Y);
                    this.Margin = new Thickness(P1.X, P2.Y, 0, 0);
                    _lDrawing.X1 = 0;
                    _lDrawing.Y1 = (P1.Y - P2.Y);
                    _lDrawing.X2 = (P2.X - P1.X);
                    _lDrawing.Y2 = 0;
                }
            }
            _lDrawing.StrokeThickness = w.thickness;
            _lDrawing.Stroke = new SolidColorBrush(Color.FromArgb(w.colorA, w.colorR, w.colorG, w.colorB));
            _lDrawing.Opacity = w.alpha;

            _lDrawingSelect.X1 = _lDrawing.X1;
            _lDrawingSelect.Y1 = _lDrawing.Y1;
            _lDrawingSelect.X2 = _lDrawing.X2;
            _lDrawingSelect.Y2 = _lDrawing.Y2;

            //_lDrawingSkel.X1 = _lDrawing.X1;
            //_lDrawingSkel.Y1 = _lDrawing.Y1;
            //_lDrawingSkel.X2 = _lDrawing.X2;
            //_lDrawingSkel.Y2 = _lDrawing.Y2;


            _ellp1.Margin = new Thickness(_lDrawing.X1 - 5, _lDrawing.Y1 - 5, 0, 0);
            _ellp2.Margin = new Thickness(_lDrawing.X2 - 5, _lDrawing.Y2 - 5, 0, 0);
        }
        // 수정 
        public void modifyWall(WallDraw modify_w)
        {
            w = modify_w;
            initWall();
        }

        // 선 종료
        private void _gridlDrawing_MouseEnter(object sender, MouseEventArgs e)
        {
            //_ellp1.Visibility = Visibility.Visible;
            //_ellp2.Visibility = Visibility.Visible;
            //anim.OpacityAnimation(_lDrawingSkel, 0, 0.7, 0.8, 300);
            //anim.ColorAnimation(_lDrawing, Color.FromArgb(w.colorA, w.colorR, w.colorG, w.colorB), Colors.Red, 300);
            if (is_selected == true) return;
            

            DropShadowEffect shadowEffect = new DropShadowEffect();
            shadowEffect.Color = Colors.White;
            shadowEffect.BlurRadius = 50;
            shadowEffect.Direction = 300;
            shadowEffect.ShadowDepth = 1;
            shadowEffect.Opacity = 0.3;

            _lDrawing.Effect = shadowEffect;
        }
        // 선 선택 해제
        private void _gridlDrawing_MouseLeave(object sender, MouseEventArgs e)
        {
           //anim.OpacityAnimation(_lDrawingSkel, 0.7, 0, 0.8, 300);
            //anim.ColorAnimation(_lDrawing, Colors.Red, Color.FromArgb(w.colorA, w.colorR, w.colorG, w.colorB), 100);
            if (is_selected==true)
                return;
            
            _lDrawing.Effect = null;
        }
        // 선 선택
        private void _gridlDrawing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectWallEvent(w);
        }
        // 벽 선택 
        public void selectWall()
        {
            is_selected = true;
#if false
            DropShadowEffect shadowEffect = new DropShadowEffect();
            shadowEffect.Color = Colors.White;
            shadowEffect.BlurRadius = 50;
            shadowEffect.Direction = 300;
            shadowEffect.ShadowDepth = 5;
            shadowEffect.Opacity = 0.8;

            _lDrawing.Effect = shadowEffect; 
#else
            //_ellp1.Visibility = Visibility.Visible;
            //_ellp2.Visibility = Visibility.Visible;
            //anim.OpacityAnimation(_lDrawingSkel, 0, 0.7, 0.8, 300);
            //anim.ColorAnimation(_lDrawing, Color.FromArgb(w.colorA, w.colorR, w.colorG, w.colorB), Colors.Red, 300);



            DropShadowEffect shadowEffect = new DropShadowEffect();
            shadowEffect.Color = Color.FromArgb(w.colorA, w.colorR, w.colorG, w.colorB);
            shadowEffect.BlurRadius = 50;
            shadowEffect.Direction = 300;
            shadowEffect.ShadowDepth = 5;
            shadowEffect.Opacity = 0.8;

            _lDrawing.Effect = shadowEffect; 
#endif
        }
        // 해제
        public void releaseWall()
        {
            is_selected = false;
            _lDrawing.Effect = null;
        }
        // 투명도 조정
        public void hideModifyWall()
        {
            _lDrawing.Opacity = 0.3;
        }
        // 보이기
        public void showModifyWall()
        {
            _lDrawing.Opacity = 1.0;
        }
        // 이벤트 
        private void ellp1_MouseEvent(object sender, MouseEventArgs e)
        {
            if (_ellp1.IsMouseOver)
            {
                anim.ellipseOpacityAnimation(_ellp1, 0, 0.5, 0.8, 300);
                anim.ellipseScaleAnimation(_ellp1, new Vector(1, 1), new Vector(2, 2), new Point(5, 5), 0.8, 300);
                ConnectWallPointEvent(w);
            }
            else
            {
                anim.ellipseOpacityAnimation(_ellp1, 0.5, 0, 0.8, 300);
                anim.ellipseScaleAnimation(_ellp1, new Vector(2, 2), new Vector(1, 1), new Point(5, 5), 0.8, 300);
                ConnectWallPointEvent(0);
            }
        }

        private void ellp2_MouseEvent(object sender, MouseEventArgs e)
        {
            if (_ellp2.IsMouseOver)
            {
                anim.ellipseOpacityAnimation(_ellp2, 0, 0.5, 0.8, 300);
                anim.ellipseScaleAnimation(_ellp2, new Vector(1, 1), new Vector(2, 2), new Point(5, 5), 0.8, 300);
                ConnectWallPointEvent(w);
            }
            else
            {
                anim.ellipseOpacityAnimation(_ellp2, 0.5, 0, 0.8, 300);
                anim.ellipseScaleAnimation(_ellp2, new Vector(2, 2), new Vector(1, 1), new Point(5, 5), 0.8, 300);
                ConnectWallPointEvent(0);
            }
        }
        // 점 선택
        private void _ellp2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            w.select_p = new Point(this.Margin.Left + _lDrawing.X2, this.Margin.Top + _lDrawing.Y2);
            PickWallPointEvent(w);
        }
        // 점 선택
        private void _ellp1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            w.select_p = new Point(this.Margin.Left + _lDrawing.X1, this.Margin.Top + _lDrawing.Y1);
            PickWallPointEvent(w);
        }
    
    }
}
