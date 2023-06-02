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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace I2MS2.UserControls
{
    public partial class PieChartPathData : INotifyPropertyChanged
    {
        public Point ct_point { get; set; }
        public Double radius { get; set; }
        public Double angle { get; set; }
        public Brush brush { get; set; }
        public Double rate { get; set; }
        public Double data { get; set; }
        public int direction_is_clockwite { get; set; }

        public bool force_changed
        {
            get { return true; }
            set { NotifyPropertyChanged(""); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }

    public partial class PieChartData
    {
        public string left_path_data { get; set; }
        public string right_path_data { get; set; }
    }




    /// <summary>
    /// I2MS_PieChart.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class I2MS_PieChart : UserControl
    {

        Double timer_interval = 30;

        public I2MS_PieChart()
        {
            InitializeComponent();

        }

        double _left_data = 0;
        double _right_data = 0;

        public void showPieChart(string left_name, Brush left_brush, Double left_data,
                            string right_name,  Brush right_brush,Double right_data)
        {
            _txtLeftName.Text = left_name;
            _txtLeftData.Text = string.Format("{0}", left_data);
            _pathLeftChart.Fill = left_brush;
            _rectLeft.Fill = left_brush;


            _txtRightName.Text = right_name;
            _txtRightData.Text = string.Format("{0}", right_data);
            _pathRightChart.Fill = right_brush;
            _rectRight.Fill = right_brush;

            _left_data = left_data;
            _right_data = right_data;


            if((left_data==0)&&(right_data==0))
            {
                _ellCircle.Fill = Brushes.Gray;
                _ellCircle.Opacity = 1;
                _txtLeftData.Text = null;
                _txtRightData.Text = null;
                _txtLeftShare.Text = null;
                _txtRightShare.Text = null;
                _pathLeftChart.Data = null;
                _pathRightChart.Data = null;
            }
            else if(left_data==0)
            {
                _txtLeftData.Text = null;
                _txtLeftShare.Text = null;
                _txtRightShare.Text = "100%";
                _ellCircle.Fill = _pathRightChart.Fill;
                _ellCircle.Opacity = 1;
                _pathLeftChart.Data = null;
            }
            else if (right_data == 0)
            {
                _txtRightData.Text = null;
                _txtRightShare.Text = null;
                _txtLeftShare.Text = "100%";
                _ellCircle.Fill = _pathLeftChart.Fill;
                _ellCircle.Opacity = 1;
                _pathRightChart.Data = null;
            }
            else
            {
                _ellCircle.Opacity = 0;
                makePieChart_2SliceWith_Animation();
            }
        }

    
        public void makePieChart_2Slice(Double d1, Double d2)
        {
            Double radius = 40;
            Point ct_p = new Point(60, 60);
            Double tot = d1 + d2;
            Double left, right;
            if (d1 > d2)
            {
                left = d1; right = d2;
            }
            else
            {
                left = d2; right = d1;
            }

            PieChartPathData left_pcd = new PieChartPathData()
            {
                angle = left / tot * 360,
                radius = radius,
                ct_point = ct_p,
                direction_is_clockwite = 0
            };


            PieChartPathData right_pcd = new PieChartPathData()
            {
                angle = right / tot * 360,
                radius = radius,
                ct_point = ct_p,
                direction_is_clockwite = 1
            };
    

            PieChartData pcd = new PieChartData()
            {
                left_path_data = makePieData(left_pcd),
                right_path_data = makePieData(right_pcd)
            };


            //_gridTotPieChart.DataContext = pcd;
            _pathLeftChart.Data = makePie(left_pcd);
            _pathRightChart.Data = makePie(right_pcd);


            Thickness left_margin = calculThickness(left_pcd);
            _txtLeftShare.Margin = left_margin;
            Double left_rate = (left / tot) * 100;
            _txtLeftShare.Text = string.Format("{0}%", left_rate.ToString("f1"));

            Thickness right_margin = calculThickness(right_pcd);
            _txtRightShare.Margin = right_margin;
            Double right_rate = (right / tot) * 100;
            _txtRightShare.Text = string.Format("{0}%", right_rate.ToString("f1"));
        }

        Double bp_angle;
        Double sp_angle;
        Double bp_angle_interval;
        Double sp_angle_interval;

        Double bp_data;
        Double sp_data;
        Double bp_data_interval;
        Double sp_data_interval;
       
        int PieChart_timer_cnt;
        int PieChart_timer_end;
        
        PieChartPathData bp_pcd;
        PieChartPathData sp_pcd;

        DispatcherTimer PieChart_timer;

#if false
         DispatcherTimer bp_timer;
        DispatcherTimer sp_timer;
        
        private void bpTimer_Tick(object sender, EventArgs e)
        {
            if (bp_angle < bp_pcd.angle)
            {
                bp_angle = bp_angle + 1;
                PieChartPathData pcd = new PieChartPathData()
                {
                    angle = bp_angle,
                    rate = bp_angle / 360 * 100,
                    brush = bp_pcd.brush,
                    ct_point = bp_pcd.ct_point,
                    radius = bp_pcd.radius,
                    direction_is_clockwite = bp_pcd.direction_is_clockwite
                };
                drawBigPieChart(pcd);
            }
            else
            {
                bp_timer.Stop();
            }
        }


        private void spTimer_Tick(object sender, EventArgs e)
        {
            if (sp_angle < sp_pcd.angle)
            {
                sp_angle = sp_angle + 1;
                PieChartPathData pcd = new PieChartPathData()
                {
                    angle = sp_angle,
                    rate = sp_angle / 360 * 100,
                    brush = sp_pcd.brush,
                    ct_point = sp_pcd.ct_point,
                    radius = sp_pcd.radius,
                    direction_is_clockwite = sp_pcd.direction_is_clockwite
                };
                drawSmallPieChart(pcd);
            }
            else
            {
                sp_timer.Stop();
            }
        } 
#endif

        private void PieChartTimer_Tick(object sender, EventArgs e)
        {
            if (PieChart_timer_cnt < PieChart_timer_end)
            {
                PieChart_timer_cnt++;
                sp_angle += sp_angle_interval;
                bp_angle += bp_angle_interval;

                sp_data += sp_data_interval;
                bp_data += bp_data_interval;

                PieChartPathData tmp_sp_pcd = new PieChartPathData()
                {
                    angle = sp_angle,
                    data = sp_data,
                    rate = sp_angle / 360 * 100,
                    brush = sp_pcd.brush,
                    ct_point = sp_pcd.ct_point,
                    radius = sp_pcd.radius,
                    direction_is_clockwite = sp_pcd.direction_is_clockwite
                };
                drawRightPieChart(tmp_sp_pcd);

                PieChartPathData tmp_bp_pcd = new PieChartPathData()
                {
                    angle = bp_angle,
                    data = bp_data,
                    rate = bp_angle / 360 * 100,
                    brush = bp_pcd.brush,
                    ct_point = bp_pcd.ct_point,
                    radius = bp_pcd.radius,
                    direction_is_clockwite = bp_pcd.direction_is_clockwite
                };
                drawLeftPieChart(tmp_bp_pcd);
            }
            else
            {
                PieChart_timer.Stop();
            }
        }

        private void drawLeftPieChart(PieChartPathData pcd)
        {
            _pathLeftChart.Data = makePie(pcd);
            _txtLeftShare.Margin = calculThickness(pcd);
            _txtLeftShare.Text = string.Format("{0}%", pcd.rate.ToString("f1"));
            _txtLeftData.Text = string.Format("{0}", pcd.data.ToString("f0"));
        }

        private void drawRightPieChart(PieChartPathData pcd)
        {
            _pathRightChart.Data = makePie(pcd);
            _txtRightShare.Margin = calculThickness(pcd);
            _txtRightShare.Text = string.Format("{0}%", pcd.rate.ToString("f1"));
            _txtRightData.Text = string.Format("{0}", pcd.data.ToString("f0"));
        }

        public void makePieChart_2SliceWith_Animation()
        {
            double ldata = _left_data;
            double rdata = _right_data;

            Double radius = 40;
            Point ct_p = new Point(60, 60);
            Double tot = ldata + rdata;
  

            bp_pcd = new PieChartPathData()
            {
                angle = ldata / tot * 360,
                radius = radius,
                ct_point = ct_p,
                direction_is_clockwite = 0,
                rate = (ldata / tot) * 100,
                data = ldata
            };


            sp_pcd = new PieChartPathData()
            {
                angle = rdata / tot * 360,
                radius = radius,
                ct_point = ct_p,
                direction_is_clockwite = 1,
                rate = (rdata / tot) * 100,
                data = rdata
            };


            _pathLeftChart.Data = null;
            _txtLeftShare.Text = null;
            _pathRightChart.Data = null;
            _txtRightShare.Text = null;


            bp_angle = 0;
            sp_angle = 0;
            bp_data = 0;
            sp_data = 0;


            //bp_timer = new DispatcherTimer();
            //bp_timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            //bp_timer.Tick += new EventHandler(bpTimer_Tick);
            //bp_timer.Start();
            //sp_timer = new DispatcherTimer();
            //sp_timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            //sp_timer.Tick += new EventHandler(spTimer_Tick);
            //sp_timer.Start();

            PieChart_timer_cnt = 0;
            PieChart_timer_end = (int)timer_interval;
            bp_angle_interval = bp_pcd.angle / timer_interval;
            sp_angle_interval = sp_pcd.angle / timer_interval;
            bp_data_interval = bp_pcd.data / timer_interval;
            sp_data_interval = sp_pcd.data / timer_interval;

            int milsec = 300 / (int)timer_interval;
            PieChart_timer = new DispatcherTimer();
            PieChart_timer.Interval = new TimeSpan(0, 0, 0, 0, milsec);
            PieChart_timer.Tick += new EventHandler(PieChartTimer_Tick);
            PieChart_timer.Start();

        }

        PieChartPathData tmp_pcd;
        DispatcherTimer chartDraw_Timer;
        Double max_angle;

        private void chartDraw_Tick(object sender, EventArgs e)
        {
            tmp_pcd.angle = tmp_pcd.angle + 1;
            tmp_pcd.force_changed = true;
            if (tmp_pcd.angle > max_angle)
                chartDraw_Timer.Stop();
        }



        public Thickness calculThickness(PieChartPathData pcd)
        {
            Thickness margin;

            //  끝점 먼저 계산
            Double radian = AtoR(pcd.angle);

            //Double end_point_theta_radian = AtoR(90) - ( AtoR(angle) + Math.Atan(center_point.X / (center_point.Y - radius)) );
            Point st_point_length = new Point(0, pcd.radius);
            Double r_90 = AtoR(90);
            Double r_angle = AtoR(pcd.angle);
            Double r_Atan = Math.Atan(st_point_length.X / (st_point_length.Y));
            Double r_end_point_theta = r_90 - (r_angle + r_Atan);

            Double end_x;
            Double end_y;
            if (pcd.direction_is_clockwite == 1)
            {
                end_x = pcd.ct_point.X + pcd.radius * Math.Cos(r_end_point_theta);
                end_y = pcd.ct_point.Y - pcd.radius * Math.Sin(r_end_point_theta);
            }
            else
            {
                end_x = pcd.ct_point.X - pcd.radius * Math.Cos(r_end_point_theta);
                end_y = pcd.ct_point.Y - pcd.radius * Math.Sin(r_end_point_theta);
            }

            Point tx_point = new Point();
            tx_point.X = (st_point_length.X + end_x) / 2;
            tx_point.Y = (st_point_length.Y + end_y) / 2 - ( pcd.ct_point.Y - pcd.radius) ;

            if (pcd.direction_is_clockwite == 1)
            {
                margin = new Thickness(tx_point.X + 40, tx_point.Y,0,0);
            }
            else
            {
                margin = new Thickness(tx_point.X - 30, tx_point.Y, 0, 0);
            }


            return margin;
        }

        public string makePieData(PieChartPathData pcd)
        {
            //  끝점 먼저 계산
            Double radian = AtoR(pcd.angle);

            //Double end_point_theta_radian = AtoR(90) - ( AtoR(angle) + Math.Atan(center_point.X / (center_point.Y - radius)) );
            Point st_point_length = new Point(0, pcd.radius);
            Double r_90 = AtoR(90);
            Double r_angle = AtoR(pcd.angle);
            Double r_Atan = Math.Atan(st_point_length.X / (st_point_length.Y));
            Double r_end_point_theta = r_90 - (r_angle + r_Atan);

            Double end_x;
            Double end_y;
            Double Direction = 0;
            if (pcd.direction_is_clockwite == 1)
            {
                Direction = 1;
                end_x = pcd.ct_point.X + pcd.radius * Math.Cos(r_end_point_theta);
                end_y = pcd.ct_point.Y - pcd.radius * Math.Sin(r_end_point_theta);
            }
            else
            {
                end_x = pcd.ct_point.X - pcd.radius * Math.Cos(r_end_point_theta);
                end_y = pcd.ct_point.Y - pcd.radius * Math.Sin(r_end_point_theta);
            }

            //원의 중심에서 수직으로 위로 그린 라인 지점
            Point md_p = new Point(pcd.ct_point.X, pcd.ct_point.Y - pcd.radius);

            //Arc 의 종착 지점과 휨 정도 지정
            Double isLargeArc = 0;
            if (pcd.angle >= 180.0)
                isLargeArc = 1;

            string data_str = string.Format("M {0},{1} L {2},{3} A {4},{5} {6} {7} {8} {9},{10} L{11},{12}",
                pcd.ct_point.X, pcd.ct_point.Y,
                md_p.X, md_p.Y,
                 pcd.radius, pcd.radius,
                 pcd.angle, isLargeArc, Direction,
                end_x, end_y,
                pcd.ct_point.X, pcd.ct_point.Y

                );
            return data_str;

        }

        public PathGeometry makePie(PieChartPathData pcd)
        {
            return makePie1(pcd);
        }

        public PathGeometry makePie1(PieChartPathData pcd)
        {
            //Point start_point = new Point(50, 50);

            Point st_point_length = new Point(0, pcd.radius);
            Double r_90 = AtoR(90);
            Double r_angle = AtoR(pcd.angle);
            Double r_Atan = Math.Atan(st_point_length.X / (st_point_length.Y));
            Double r_end_point_theta = r_90 - (r_angle + r_Atan);

           
            //원의 중심에서 수직으로 위로 그린 라인 지점 (Line to)
            Point md_p = new Point(pcd.ct_point.X, pcd.ct_point.Y - pcd.radius);

            //Arc 의 종착 지점과 휨 정도 지정
            Double end_x;
            Double end_y;
            SweepDirection Direction;
            if (pcd.direction_is_clockwite == 1)
            {
                Direction = SweepDirection.Clockwise;
                end_x = pcd.ct_point.X + pcd.radius * Math.Cos(r_end_point_theta);
                end_y = pcd.ct_point.Y - pcd.radius * Math.Sin(r_end_point_theta);
            }
            else
            {
                Direction = SweepDirection.Counterclockwise;
                end_x = pcd.ct_point.X - pcd.radius * Math.Cos(r_end_point_theta);
                end_y = pcd.ct_point.Y - pcd.radius * Math.Sin(r_end_point_theta);
            }
            Double isLargeArc = 0;
            if (pcd.angle >= 180.0)
                isLargeArc = 1;



            //Path에 적용
            //path.Fill = Brushes.Yellow;
            Path path = new Path();
            path.Stroke = pcd.brush;
            PathGeometry pathGeometry = new PathGeometry();

            PathFigure pathFigure = new PathFigure();
            //시작점
            pathFigure.StartPoint = new Point(pcd.ct_point.X, pcd.ct_point.Y);
            pathFigure.IsClosed = true;

            //Line to
            LineSegment lineSegment = new LineSegment(new Point(md_p.X, md_p.Y), true);

            //Arc to
            ArcSegment arcSegment = new ArcSegment();
            arcSegment.IsLargeArc = pcd.angle >= 180.0;
            arcSegment.Point = new Point(end_x, end_y);
            arcSegment.Size = new Size(pcd.radius, pcd.radius);
            arcSegment.SweepDirection = Direction;
            
            pathFigure.Segments.Add(lineSegment);
            pathFigure.Segments.Add(arcSegment);

            pathGeometry.Figures.Add(pathFigure);

            return pathGeometry;

        }

        public PathGeometry makePie2(PieChartPathData pcd)
        {
            //Point start_point = new Point(50, 50);

            Path path = new Path();

            //Canvas.SetLeft(path, grid.ActualWidth / 2);
            //Canvas.SetTop(path, grid.ActualHeight / 2);

            //path.Fill = Brushes.Yellow;
            path.Stroke = pcd.brush;
            PathGeometry pathGeometry = new PathGeometry();

            PathFigure pathFigure = new PathFigure();
            //시작점
            pathFigure.StartPoint = new Point(pcd.ct_point.X, pcd.ct_point.Y);
            pathFigure.IsClosed = true;

            //PieChart의 회전 방향
            SweepDirection Direction;
            Double line_end_x =  Math.Cos(pcd.angle * Math.PI / 180) * pcd.radius;
            Double line_end_y = Math.Sin(pcd.angle * Math.PI / 180) * pcd.radius;
            if (pcd.direction_is_clockwite == 1)
            {
                Direction = SweepDirection.Clockwise;
                line_end_x = pcd.ct_point.X - Math.Cos(pcd.angle * Math.PI / 180) * pcd.radius;
                line_end_y = pcd.ct_point.Y + Math.Sin(pcd.angle * Math.PI / 180) * pcd.radius;
            }
            else
            {
                Direction = SweepDirection.Counterclockwise;
                line_end_x = pcd.ct_point.X - Math.Cos(pcd.angle * Math.PI / 180) * pcd.radius;
                line_end_y = pcd.ct_point.Y - Math.Sin(pcd.angle * Math.PI / 180) * pcd.radius;
            }


            //Line to
            LineSegment lineSegment = new LineSegment(new Point(pcd.ct_point.X, pcd.ct_point.Y - pcd.radius), true);

            //Arc to
            ArcSegment arcSegment = new ArcSegment();
            arcSegment.IsLargeArc = pcd.angle >= 180.0;
            //arcSegment.Point = new Point(
            //    pcd.ct_point.X + Math.Cos(pcd.angle * Math.PI / 180) * pcd.radius,
            //    pcd.ct_point.Y + Math.Sin(pcd.angle * Math.PI / 180) * pcd.radius);
            arcSegment.Point = new Point(line_end_x, line_end_y);
            arcSegment.Size = new Size(pcd.radius, pcd.radius);
            //arcSegment.SweepDirection = SweepDirection.Clockwise;
            arcSegment.SweepDirection = Direction;

            pathFigure.Segments.Add(lineSegment);
            pathFigure.Segments.Add(arcSegment);

            pathGeometry.Figures.Add(pathFigure);
            
            return pathGeometry;

        }

        public string makeSmalPieData(PieChartPathData pcd)
        {
            Point ct_p;
            if (pcd.direction_is_clockwite == 1)
            {
                ct_p = new Point(pcd.ct_point.X + 1, pcd.ct_point.Y);
            }
            else
            {
                ct_p = new Point(pcd.ct_point.X - 1, pcd.ct_point.Y);
            }

            //  끝점 먼저 계산
            Double radian = AtoR(pcd.angle);

            //Double end_point_theta_radian = AtoR(90) - ( AtoR(angle) + Math.Atan(center_point.X / (center_point.Y - radius)) );
            Point st_point_length = new Point(0, pcd.radius);
            Double r_90 = AtoR(90);
            Double r_angle = AtoR(pcd.angle);
            Double r_Atan = Math.Atan(st_point_length.X / (st_point_length.Y));
            Double r_end_point_theta = r_90 - (r_angle + r_Atan);

            Double end_x;
            Double end_y;
            Double Direction = 0;
            if (pcd.direction_is_clockwite == 1)
            {
                Direction = 1;
                end_x = ct_p.X + pcd.radius * Math.Cos(r_end_point_theta);
                end_y = ct_p.Y - pcd.radius * Math.Sin(r_end_point_theta);
            }
            else
            {
                end_x = ct_p.X - pcd.radius * Math.Cos(r_end_point_theta);
                end_y = ct_p.Y - pcd.radius * Math.Sin(r_end_point_theta);
            }

            //원의 중심에서 수직으로 위로 그린 라인 지점
            Point md_p = new Point(ct_p.X, ct_p.Y - pcd.radius);

            //Arc 의 종착 지점과 휨 정도 지정
            Double isLargeArc = 0;
            if (pcd.angle >= 180.0)
                isLargeArc = 1;

            string data_str = string.Format("M {0},{1} L {2},{3} A {4},{5} {6} {7} {8} {9},{10}",
                ct_p.X, ct_p.Y,
                md_p.X, md_p.Y,
                 pcd.radius, pcd.radius,
                 pcd.angle, isLargeArc, Direction,
                end_x, end_y
                );
            return data_str;

        }


        public Double AtoR(Double angle)
        {
            return angle * Math.PI / 180;
        }

        public Double RtoA(Double radian)
        {
            return radian * 180 / Math.PI;
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            makePieChart_2SliceWith_Animation();
        }
    }
}
