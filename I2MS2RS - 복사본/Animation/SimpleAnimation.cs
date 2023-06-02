using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace I2MS2.Animation
{
    class SimpleAnimation
    {

        public delegate void animCompleteEventHandler(object obj);
        public event animCompleteEventHandler animComplateEvent;



        public void SolidBrushAnimation(SolidColorBrush br, SolidColorBrush to_br, Boolean repeat, Boolean auto_reverse)
        {
            ColorAnimation animation = new ColorAnimation
            {
                From = br.Color,
                To = to_br.Color,
                Duration = new Duration(TimeSpan.FromSeconds(4)),  // 1
                AutoReverse = false
            };
            // animation.Completed += new EventHandler(animation_Completed);
            animation.AccelerationRatio = 0.5;
            animation.AutoReverse = auto_reverse;
            if(repeat)
                animation.RepeatBehavior = RepeatBehavior.Forever;
            br.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        public void SolidBrushAnimationStop(SolidColorBrush br)
        {
            ColorAnimation animation = new ColorAnimation();
            animation = null;
            br.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }


        #region 2D Animation
        #region PathAnimation
        public void pathColorAnimation(Path path, Color fromC, Color toC, double ratio, double milSec)
        {
            ColorAnimation anim = new ColorAnimation();

            anim.From = fromC;
            anim.To = toC;
            anim.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
            path.Fill.BeginAnimation(SolidColorBrush.ColorProperty, anim);
        }
        #endregion

        #region EllipseAnimation
        public void ellipseMoveAnimation(Ellipse ellipse, Point from, Point to, double ratio, double milSec)
        {
            DoubleAnimation moveDoubleAnimationX = new DoubleAnimation();
            moveDoubleAnimationX.From = from.X;
            moveDoubleAnimationX.To = to.X;
            moveDoubleAnimationX.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            moveDoubleAnimationX.DecelerationRatio = ratio;

            DoubleAnimation moveDoubleAnimationY = new DoubleAnimation();
            moveDoubleAnimationY.From = from.Y;
            moveDoubleAnimationY.To = to.Y;
            moveDoubleAnimationY.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            moveDoubleAnimationY.DecelerationRatio = ratio;

            Storyboard MoveStoryBoard = new Storyboard();

            MoveStoryBoard.Children.Add(moveDoubleAnimationY);
            Storyboard.SetTargetName(moveDoubleAnimationY, ellipse.Name);
            Storyboard.SetTargetProperty(moveDoubleAnimationY, new PropertyPath(Canvas.TopProperty));


            MoveStoryBoard.Children.Add(moveDoubleAnimationX);
            Storyboard.SetTargetName(moveDoubleAnimationX, ellipse.Name);
            Storyboard.SetTargetProperty(moveDoubleAnimationX, new PropertyPath(Canvas.LeftProperty));

            MoveStoryBoard.Begin(ellipse);

        }


        public void ellipseScaleAnimation(Ellipse ellipse, Vector from, Vector to, Point center, double ratio, double milSec)
        {
            DoubleAnimation scaleAnimX = new DoubleAnimation();
            scaleAnimX.From = from.X;
            scaleAnimX.To = to.X;
            scaleAnimX.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            scaleAnimX.DecelerationRatio = ratio;
            DoubleAnimation scaleAnimY = new DoubleAnimation();
            scaleAnimY.From = from.Y;
            scaleAnimY.To = to.Y;
            scaleAnimY.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            scaleAnimY.DecelerationRatio = ratio;

            ScaleTransform scaleTran = new ScaleTransform();
            scaleTran.CenterX = center.X;
            scaleTran.CenterY = center.Y;
            ellipse.RenderTransform = scaleTran;
            scaleTran.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimX);
            scaleTran.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimY);
        }

        public void ellipseOpacityAnimation(Ellipse ellipse, double from, double to, double ratio, double milSec)
        {
            DoubleAnimation SightDoubleAnimation = new DoubleAnimation();
            SightDoubleAnimation.From = from;
            SightDoubleAnimation.To = to;
            SightDoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            SightDoubleAnimation.AutoReverse = false;
            //myDoubleAnimation.RepeatBehavior = RepeatBehavior.Forever;

            Storyboard SightStoryboard;
            SightStoryboard = new Storyboard();
            SightStoryboard.Children.Add(SightDoubleAnimation);
            Storyboard.SetTargetName(SightDoubleAnimation, ellipse.Name);
            Storyboard.SetTargetProperty(SightDoubleAnimation, new PropertyPath(UserControl.OpacityProperty));
            SightStoryboard.Begin(ellipse);
        }

        #endregion

        #region GridAnimation
        public void gridMoveAnimation(Grid grid, Point from, Point to, double ratio, double milSec)
        {
            DoubleAnimation moveDoubleAnimationX = new DoubleAnimation();
            moveDoubleAnimationX.From = from.X;
            moveDoubleAnimationX.To = to.X;
            moveDoubleAnimationX.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            moveDoubleAnimationX.DecelerationRatio = ratio;

            DoubleAnimation moveDoubleAnimationY = new DoubleAnimation();
            moveDoubleAnimationY.From = from.Y;
            moveDoubleAnimationY.To = to.Y;
            moveDoubleAnimationY.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            moveDoubleAnimationY.DecelerationRatio = ratio;

            Storyboard MoveStoryBoard = new Storyboard();

            MoveStoryBoard.Children.Add(moveDoubleAnimationY);
            Storyboard.SetTargetName(moveDoubleAnimationY, grid.Name);
            Storyboard.SetTargetProperty(moveDoubleAnimationY, new PropertyPath(Canvas.TopProperty));


            MoveStoryBoard.Children.Add(moveDoubleAnimationX);
            Storyboard.SetTargetName(moveDoubleAnimationX, grid.Name);
            Storyboard.SetTargetProperty(moveDoubleAnimationX, new PropertyPath(Canvas.LeftProperty));

            MoveStoryBoard.Begin(grid);

        }


        public void gridScaleAnimation(Grid grid, Vector from, Vector to, Point center, double ratio, double milSec)
        {
            DoubleAnimation scaleAnimX = new DoubleAnimation();
            scaleAnimX.From = from.X;
            scaleAnimX.To = to.X;
            scaleAnimX.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            scaleAnimX.DecelerationRatio = ratio;
            DoubleAnimation scaleAnimY = new DoubleAnimation();
            scaleAnimY.From = from.Y;
            scaleAnimY.To = to.Y;
            scaleAnimY.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            scaleAnimY.DecelerationRatio = ratio;

            ScaleTransform scaleTran = new ScaleTransform();
            scaleTran.CenterX = center.X;
            scaleTran.CenterY = center.Y;
            grid.RenderTransform = scaleTran;
            scaleTran.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimX);
            scaleTran.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimY);
        }

        public void gridScaleAnimationWithEvnet(Grid grid, Vector from, Vector to, Point center, double ratio, double milSec)
        {
            DoubleAnimation scaleAnimX = new DoubleAnimation();
            scaleAnimX.From = from.X;
            scaleAnimX.To = to.X;
            scaleAnimX.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            scaleAnimX.DecelerationRatio = ratio;
            DoubleAnimation scaleAnimY = new DoubleAnimation();
            scaleAnimY.From = from.Y;
            scaleAnimY.To = to.Y;
            scaleAnimY.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            scaleAnimY.DecelerationRatio = ratio;

            scaleAnimX.Completed += gridAnimationEndEvent;

            ScaleTransform scaleTran = new ScaleTransform();
            scaleTran.CenterX = center.X;
            scaleTran.CenterY = center.Y;
            grid.RenderTransform = scaleTran;
            scaleTran.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimX);
            scaleTran.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimY);
        }

        public void gridAnimationEndEvent(object sender, EventArgs e)
        {
            if (animComplateEvent != null)
                animComplateEvent(sender);
       }

        public void gridOpacityAnimation(Grid grid, double from, double to, double ratio, double milSec)
        {
            DoubleAnimation SightDoubleAnimation = new DoubleAnimation();
            SightDoubleAnimation.From = from;
            SightDoubleAnimation.To = to;
            SightDoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            SightDoubleAnimation.AutoReverse = false;
            //myDoubleAnimation.RepeatBehavior = RepeatBehavior.Forever;

            Storyboard SightStoryboard;
            SightStoryboard = new Storyboard();
            SightStoryboard.Children.Add(SightDoubleAnimation);
            Storyboard.SetTargetName(SightDoubleAnimation, grid.Name);
            Storyboard.SetTargetProperty(SightDoubleAnimation, new PropertyPath(UserControl.OpacityProperty));
            SightStoryboard.Begin(grid);
        }

        public void gridOpacityAnimationWithEvent(Grid grid, double from, double to, double ratio, double milSec)
        {
            DoubleAnimation SightDoubleAnimation = new DoubleAnimation();
            SightDoubleAnimation.From = from;
            SightDoubleAnimation.To = to;
            SightDoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            SightDoubleAnimation.AutoReverse = false;
            //myDoubleAnimation.RepeatBehavior = RepeatBehavior.Forever;

            SightDoubleAnimation.Completed += gridAnimationEndEvent;

            Storyboard SightStoryboard;
            SightStoryboard = new Storyboard();
            SightStoryboard.Children.Add(SightDoubleAnimation);
            Storyboard.SetTargetName(SightDoubleAnimation, grid.Name);
            Storyboard.SetTargetProperty(SightDoubleAnimation, new PropertyPath(UserControl.OpacityProperty));
            SightStoryboard.Begin(grid);
        }

        public void gridBlinkingAnimationStart(Grid grid, double from, double to, double ratio, double milSec)
        {
            DoubleAnimation BlinkDoubleAnimation = new DoubleAnimation();
            BlinkDoubleAnimation.From = from;
            BlinkDoubleAnimation.To = to;
            BlinkDoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            BlinkDoubleAnimation.AutoReverse = true;
            BlinkDoubleAnimation.RepeatBehavior = RepeatBehavior.Forever;

            Storyboard BlinkStoryboard;
            BlinkStoryboard = new Storyboard();
            BlinkStoryboard.Children.Add(BlinkDoubleAnimation);
            Storyboard.SetTargetName(BlinkDoubleAnimation, grid.Name);
            Storyboard.SetTargetProperty(BlinkDoubleAnimation, new PropertyPath(Grid.OpacityProperty));
            BlinkStoryboard.Begin(grid);
        }

        public void gridBlinkingAnimationStop(Grid grid)
        {
            grid.RenderTransform = null;
            //DoubleAnimation BlinkDoubleAnimation = new DoubleAnimation();
            //BlinkDoubleAnimation.From = 1.0;
            //BlinkDoubleAnimation.To = 0.5;
            //BlinkDoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
            //BlinkDoubleAnimation.AutoReverse = true;
            //BlinkDoubleAnimation.RepeatBehavior = RepeatBehavior.Forever;

            //Storyboard BlinkStoryboard;
            //BlinkStoryboard = new Storyboard();
            //BlinkStoryboard.Children.Add(BlinkDoubleAnimation);
            //Storyboard.SetTargetName(BlinkDoubleAnimation, grid.Name);
            //Storyboard.SetTargetProperty(BlinkDoubleAnimation, new PropertyPath(Grid.OpacityProperty));
            //BlinkStoryboard.Begin(grid);
        }
        #endregion

        #region UserControl Animation
        public void uControlMoveAnimation(UserControl uControl, Point from, Point to, double ratio, double milSec)
        {
            DoubleAnimation moveDoubleAnimationX = new DoubleAnimation();
            moveDoubleAnimationX.From = from.X;
            moveDoubleAnimationX.To = to.X;
            moveDoubleAnimationX.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            moveDoubleAnimationX.DecelerationRatio = ratio;

            DoubleAnimation moveDoubleAnimationY = new DoubleAnimation();
            moveDoubleAnimationY.From = from.Y;
            moveDoubleAnimationY.To = to.Y;
            moveDoubleAnimationY.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            moveDoubleAnimationY.DecelerationRatio = ratio;

            Storyboard MoveStoryBoard = new Storyboard();

            MoveStoryBoard.Children.Add(moveDoubleAnimationY);
            Storyboard.SetTargetName(moveDoubleAnimationY, uControl.Name);
            Storyboard.SetTargetProperty(moveDoubleAnimationY, new PropertyPath(Canvas.TopProperty));


            MoveStoryBoard.Children.Add(moveDoubleAnimationX);
            Storyboard.SetTargetName(moveDoubleAnimationX, uControl.Name);
            Storyboard.SetTargetProperty(moveDoubleAnimationX, new PropertyPath(Canvas.LeftProperty));

            MoveStoryBoard.Begin(uControl);

        }


        public void uControlScaleAnimation(UserControl uControl, Vector from, Vector to, Point center, double ratio, double milSec)
        {
            DoubleAnimation scaleAnimX = new DoubleAnimation();
            scaleAnimX.From = from.X;
            scaleAnimX.To = to.X;
            scaleAnimX.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            scaleAnimX.DecelerationRatio = ratio;
            DoubleAnimation scaleAnimY = new DoubleAnimation();
            scaleAnimY.From = from.Y;
            scaleAnimY.To = to.Y;
            scaleAnimY.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            scaleAnimY.DecelerationRatio = ratio;

            ScaleTransform scaleTran = new ScaleTransform();
            scaleTran.CenterX = center.X;
            scaleTran.CenterY = center.Y;
            uControl.RenderTransform = scaleTran;
            scaleTran.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimX);
            scaleTran.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimY);
        }

        public void uControlOpacityAnimation(UserControl uControl, double from, double to, double ratio, double milSec)
        {
            DoubleAnimation SightDoubleAnimation = new DoubleAnimation();
            SightDoubleAnimation.From = from;
            SightDoubleAnimation.To = to;
            SightDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
            SightDoubleAnimation.AutoReverse = false;
            //myDoubleAnimation.RepeatBehavior = RepeatBehavior.Forever;

            Storyboard SightStoryboard;
            SightStoryboard = new Storyboard();
            SightStoryboard.Children.Add(SightDoubleAnimation);
            Storyboard.SetTargetName(SightDoubleAnimation, uControl.Name);
            Storyboard.SetTargetProperty(SightDoubleAnimation, new PropertyPath(UserControl.OpacityProperty));
            SightStoryboard.Begin(uControl);
        }

        public void UserControlForegroundAnimation(UserControl userControl, Color fromC, Color toC, double milSec)
        {
            ColorAnimation anim = new ColorAnimation();

            anim.From = fromC;
            anim.To = toC;
            anim.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
            userControl.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, anim);
        }
        #endregion


        #region Frame Animation
        public void FrameMoveAnimation(Frame frame, Point from, Point to, double ratio, double milSec)
        {
            DoubleAnimation moveDoubleAnimationX = new DoubleAnimation();
            moveDoubleAnimationX.From = from.X;
            moveDoubleAnimationX.To = to.X;
            moveDoubleAnimationX.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            moveDoubleAnimationX.DecelerationRatio = ratio;

            DoubleAnimation moveDoubleAnimationY = new DoubleAnimation();
            moveDoubleAnimationY.From = from.Y;
            moveDoubleAnimationY.To = to.Y;
            moveDoubleAnimationY.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            moveDoubleAnimationY.DecelerationRatio = ratio;

            Storyboard MoveStoryBoard = new Storyboard();

            MoveStoryBoard.Children.Add(moveDoubleAnimationY);
            Storyboard.SetTargetName(moveDoubleAnimationY, frame.Name);
            Storyboard.SetTargetProperty(moveDoubleAnimationY, new PropertyPath(Canvas.TopProperty));


            MoveStoryBoard.Children.Add(moveDoubleAnimationX);
            Storyboard.SetTargetName(moveDoubleAnimationX, frame.Name);
            Storyboard.SetTargetProperty(moveDoubleAnimationX, new PropertyPath(Canvas.LeftProperty));

            MoveStoryBoard.Begin(frame);

        }


        public void FrameScaleAnimation(Frame frame, Vector from, Vector to, Point center, double ratio, double milSec)
        {
            DoubleAnimation scaleAnimX = new DoubleAnimation();
            scaleAnimX.From = from.X;
            scaleAnimX.To = to.X;
            scaleAnimX.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            scaleAnimX.DecelerationRatio = ratio;
            DoubleAnimation scaleAnimY = new DoubleAnimation();
            scaleAnimY.From = from.Y;
            scaleAnimY.To = to.Y;
            scaleAnimY.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            scaleAnimY.DecelerationRatio = ratio;

            ScaleTransform scaleTran = new ScaleTransform();
            scaleTran.CenterX = center.X;
            scaleTran.CenterY = center.Y;
            frame.RenderTransform = scaleTran;
            scaleTran.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimX);
            scaleTran.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimY);
        }

        public void FrameOpacityAnimation(Frame frame, double from, double to, double ratio, double milSec)
        {
            DoubleAnimation SightDoubleAnimation = new DoubleAnimation();
            SightDoubleAnimation.From = from;
            SightDoubleAnimation.To = to;
            SightDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
            SightDoubleAnimation.AutoReverse = false;
            //myDoubleAnimation.RepeatBehavior = RepeatBehavior.Forever;

            Storyboard SightStoryboard;
            SightStoryboard = new Storyboard();
            SightStoryboard.Children.Add(SightDoubleAnimation);
            Storyboard.SetTargetName(SightDoubleAnimation, frame.Name);
            Storyboard.SetTargetProperty(SightDoubleAnimation, new PropertyPath(UserControl.OpacityProperty));
            SightStoryboard.Begin(frame);
        }

        public void FrameForegroundAnimation(Frame frame, Color fromC, Color toC, double milSec)
        {
            ColorAnimation anim = new ColorAnimation();

            anim.From = fromC;
            anim.To = toC;
            anim.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
            frame.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, anim);
        }
        
        #endregion

        #endregion

        public void ColorAnimation(FrameworkElement el, Color fromC, Color toC, double milSec)
        {
            ColorAnimation anim = new ColorAnimation();

            anim.From = fromC;
            anim.To = toC;
            anim.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            
            if(el is Line)
            {
                Line l = (Line)el;
                l.Stroke.BeginAnimation(SolidColorBrush.ColorProperty, anim);
            }
            else if(el is Rectangle)
            {
                Rectangle rect = (Rectangle)el;
                rect.Fill.BeginAnimation(SolidColorBrush.ColorProperty, anim);
            }
            //frame.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, anim);
        }

        public void OpacityAnimation(FrameworkElement el, double from, double to, double ratio, double milSec)
        {
            DoubleAnimation SightDoubleAnimation = new DoubleAnimation();
            SightDoubleAnimation.From = from;
            SightDoubleAnimation.To = to;
            SightDoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            SightDoubleAnimation.AutoReverse = false;
            //myDoubleAnimation.RepeatBehavior = RepeatBehavior.Forever;

            Storyboard SightStoryboard;
            SightStoryboard = new Storyboard();
            SightStoryboard.Children.Add(SightDoubleAnimation);
            Storyboard.SetTargetName(SightDoubleAnimation, el.Name);
            Storyboard.SetTargetProperty(SightDoubleAnimation, new PropertyPath(UserControl.OpacityProperty));
            SightStoryboard.Begin(el);

        }

        #region 3D Animation


        public void scaleAnimation3D(ModelVisual3D mv3d, Double scale, Point3D ct_p, Boolean repeat, Boolean auto_reverse)
        {
            Transform3DGroup transform = new Transform3DGroup();
            ScaleTransform3D _scaleTransform = new ScaleTransform3D();
            _scaleTransform.ScaleX = 1;
            _scaleTransform.ScaleY = 1;
            _scaleTransform.ScaleZ = 1;

            _scaleTransform.CenterX = ct_p.X;
            _scaleTransform.CenterY = ct_p.Y;
            _scaleTransform.CenterZ = ct_p.Z;


            DoubleAnimation anim = new DoubleAnimation(scale, new Duration(TimeSpan.FromSeconds(1)));
            if (repeat)
                anim.RepeatBehavior = RepeatBehavior.Forever;

            anim.AutoReverse = auto_reverse;

            transform.Children.Add(_scaleTransform);

            mv3d.Transform = transform;
            //cubeModelGroup.Transform = transform;
            _scaleTransform.BeginAnimation(ScaleTransform3D.ScaleXProperty, anim);
            _scaleTransform.BeginAnimation(ScaleTransform3D.ScaleYProperty, anim);
            _scaleTransform.BeginAnimation(ScaleTransform3D.ScaleZProperty, anim);
        }


        public void opacityAnimation3D()
        {

        }


        #endregion
    }
}
