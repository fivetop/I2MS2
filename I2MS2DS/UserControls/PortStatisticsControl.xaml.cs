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

using WebApi.Models;

namespace I2MS2.UserControls
{
    //public partial class PieChartPathData
    //{
    //    public Point ct_point { get; set; }
    //    public Double radius { get; set; }
    //    public Double angle { get; set; }
    //    public Brush brush { get; set; }
    //    public int direction_is_clockwite { get; set; }
    //}

    //public partial class PieChartData
    //{
    //    public string i_path_data { get; set; }
    //    public string n_path_data { get; set; }
    //}

    public partial class BarChartData
    {
        
    }

    public class location_stat
    {
        public int location_id = 1;
        public int num_of_tot_pp_ports;
        public int num_of_used_pp_ports;

        public int num_of_tot_int_pp_ports;
        public int num_of_used_int_pp_ports;

        public int num_of_tot_nor_pp_ports;
        public int num_of_used_nor_pp_ports;

        public int num_of_tot_user_ports;
        public int num_of_loc_user_ports;
    }

    /// <summary>
    /// PortStatisticsControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PortStatisticsControl : UserControl
    {
        public PortStatisticsControl()
        {
            InitializeComponent();


            location_stat los = new location_stat()
            {
                location_id = 1,
                num_of_tot_pp_ports = 50000,
                num_of_used_pp_ports = 40000,

                num_of_tot_int_pp_ports = 30000,
                num_of_used_int_pp_ports = 20000,

                num_of_tot_nor_pp_ports = 20000,
                num_of_used_nor_pp_ports = 18000,

                num_of_tot_user_ports = 40000,
                num_of_loc_user_ports = 38000
            };
            AddPie(los);

        }


        public void AddPie(location_stat los)
        {
            Double radius = 40;
            Point ct_p = new Point(60,60);



            PieChartPathData i_pcd = new PieChartPathData()
            {
                angle = (Double)los.num_of_tot_int_pp_ports / (Double)los.num_of_tot_pp_ports * 360,
                radius = radius,
                ct_point = ct_p,
                direction_is_clockwite = 0
            };


            PieChartPathData n_pcd = new PieChartPathData()
            {
                angle = (Double)los.num_of_tot_nor_pp_ports / (Double)los.num_of_tot_pp_ports * 360,
                radius = radius,
                ct_point = ct_p,
                direction_is_clockwite = 1
            };

            PieChartData pcd = new PieChartData()
            {
                left_path_data = makePieData(i_pcd),
                right_path_data = makePieData(n_pcd)
            };


            _gridTotPieChart.DataContext = pcd;
           //// _pathIntelliPort.DataContext = pcd;
           // _pathNormalPort.DataContext = pcd;
            
            
        }



        //public string makePieData(Point center_point, Double radius, Double angle, SweepDirection direction)
        //{
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

            
            //string data_str = string.Format("M {0},{1} L {2},{3} A {4},{5} {6} {7} {8} {9},{10}",
            //    pcd.ct_point.X, pcd.ct_point.Y,
            //    md_p.X, md_p.Y,
            //     pcd.radius, pcd.radius,
            //     pcd.angle, isLargeArc, Direction,
            //    end_x, end_y

            //    );
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

        public string makeSmalPieData(PieChartPathData pcd)
        {
            Point ct_p;
            if(pcd.direction_is_clockwite ==1)
            {
                ct_p = new Point(pcd.ct_point.X+1, pcd.ct_point.Y);
            }
            else
            {
                ct_p = new Point(pcd.ct_point.X-1, pcd.ct_point.Y);
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
    }
}
