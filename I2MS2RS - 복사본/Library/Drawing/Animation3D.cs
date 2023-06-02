using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace I2MS2.Library.Drawing
{
    // 3D 애니메이션 처리 
    public class Animation3D
    {
        public Animation3D()
        {

        }

        // 사용안함 
        public void model3d_ScaleAnimXY(ModelVisual3D m3d, Double from, Double to,Point3D ct_p)
        {
            Transform3DGroup transform = new Transform3DGroup();
            ScaleTransform3D _scaleTransform = new ScaleTransform3D();
            _scaleTransform.ScaleX = from;
            _scaleTransform.ScaleY = from;
            _scaleTransform.ScaleZ = from;

            _scaleTransform.CenterX = ct_p.X;
            _scaleTransform.CenterY = ct_p.Y;
            _scaleTransform.CenterZ = ct_p.Z;


            DoubleAnimation anim = new DoubleAnimation(to, new Duration(TimeSpan.FromMilliseconds(200)));
            anim.DecelerationRatio = 0.8;
            transform.Children.Add(_scaleTransform);

            m3d.Transform = transform;
            //cubeModelGroup.Transform = transform;
            _scaleTransform.BeginAnimation(ScaleTransform3D.ScaleXProperty, anim);
            _scaleTransform.BeginAnimation(ScaleTransform3D.ScaleYProperty, anim);
            //_scaleTransform.BeginAnimation(ScaleTransform3D.ScaleZProperty, anim);
        }


        public void model3d_MoveAnimXY(ModelVisual3D m3d, Point3D from, Point3D to)
        {
            Transform3DGroup transform = new Transform3DGroup();
            TranslateTransform3D _translateTransform = new TranslateTransform3D();
            _translateTransform.OffsetX = 0;
            _translateTransform.OffsetY = 0;
//            _translateTransform.OffsetZ = from.Z;

            transform.Children.Add(_translateTransform);
            m3d.Transform = transform;
            //DoubleAnimation animX = new DoubleAnimation(from.X, to.X, new Duration(TimeSpan.FromMilliseconds(200)));
            //DoubleAnimation animY = new DoubleAnimation(from.Y, to.Y, new Duration(TimeSpan.FromMilliseconds(200)));

            Double milSec = 200;
            Double ratio = 0.8;

            DoubleAnimation animX = new DoubleAnimation();
            animX.From = from.X;
            animX.To = to.X;
            animX.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            animX.DecelerationRatio = ratio;

            DoubleAnimation animY = new DoubleAnimation();
            animY.From = from.Y;
            animY.To = to.Y;
            animY.Duration = new Duration(TimeSpan.FromMilliseconds(milSec));
            animY.DecelerationRatio = ratio;
           //animY.To = null;
            animY.By = from.Y;


            _translateTransform.BeginAnimation(TranslateTransform3D.OffsetXProperty, animX);
            _translateTransform.BeginAnimation(TranslateTransform3D.OffsetYProperty, animY);
            //_translateTransform.BeginAnimation(TranslateTransform3D.OffsetZProperty, animX);

        }


        public void model3d_MoveXY(ModelVisual3D m3d, Point3D from, Point3D to)
        {
            Transform3DGroup transform = new Transform3DGroup();
            TranslateTransform3D _translateTransform = new TranslateTransform3D();
            _translateTransform.OffsetX = to.X;
            _translateTransform.OffsetY = to.Y;
            

            transform.Children.Add(_translateTransform);
            m3d.Transform = transform;
            Point3D p =  new Point3D();
            Boolean result = m3d.Transform.TryTransform(to, out p);
            Console.WriteLine("result:{0} =({1},{2},{3})", result,p.X, p.Y, p.Z);

            //_translateTransform.BeginAnimation(TranslateTransform3D.OffsetXProperty, animX);
            //_translateTransform.BeginAnimation(TranslateTransform3D.OffsetYProperty, animY);
            ////_translateTransform.BeginAnimation(TranslateTransform3D.OffsetZProperty, animX);

        }

    }
}
