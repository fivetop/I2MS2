using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using I2MS2.Models;
using System.Windows.Media;
using I2MS2.UserControls.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace I2MS2.Library.Drawing
{
    // 절대 사이즈와 상대 좌표 처리, 캔버스상 위치 처리 
    public class DrawingDataManager
    {
        static Size DBsize =  new Size(102400,76800);

        public Point getVMPoint_FromCanvasPoint(Size size, Point p)
        {
            Double ratio = getRatio_byDB(size);
            return new Point(p.X * ratio, p.Y * ratio); 
        }

        public Point getCanvasPoint_FromVMPoint(Size size, Point p)
        {
            Double ratio = getRatio_byDB(size);
            return new Point(p.X / ratio, p.Y / ratio);
        }

        public Size getCanvasSize_FromVMSize(Size size, Size s)
        {
            Double ratio = getRatio_byDB(size);
            return new Size(s.Width / ratio, s.Height / ratio);
        }

        public Double getVMValue_FromCanvasValue(Size size, Double value)
        {
            Double ratio = getRatio_byDB(size);
            return value * ratio; 
        }

        public Double getCanvasValue_FromVMValue(Size size, Double value)
        {
            Double ratio = getRatio_byDB(size);
            return value / ratio;
        }


        public Double getVMValue_FromDefaultValue(Double value)
        {
            Double ratio = getRatio_byDB(new Size(1024,768));
            return value * ratio;
        }

        public Double getDefaultValue_FromVMValue(Double value)
        {
            Double ratio = getRatio_byDB(new Size(1024, 768));
            return value / ratio;
        }


        public Point getVMPoint_From3DPoint(Point p)
        {
            Double ratio = getRatio_byDB(new Size(1024,768));
            return new Point(p.X * ratio, p.Y * ratio);
        }

        public Point get3DPoint_FromVMPoint(Point p)
        {
            Double ratio = getRatio_byDB(new Size(1024, 768));
            return new Point(p.X / ratio, p.Y / ratio);
        }

        public Size get3DSize_FromVMSize(Size s)
        {
            Double ratio = getRatio_byDB(new Size(1024, 768));
            return new Size(s.Width / ratio, s.Height / ratio);
        }

        public Double getVMValue_From3DValue(Double value)
        {
            Double ratio = getRatio_byDB(new Size(1024, 768));
            return value * ratio;
        }

        public Double get3DValue_FromVMValue(Double value)
        {
            Double ratio = getRatio_byDB(new Size(1024, 768));
            return value / ratio;
        }

        public WallDraw makeDBWallData(int l, int id, Size size, Point s_p, Point e_p, SolidColorBrush brush, Double thick, Double height, Double alpha)
        {
            WallDraw w = new WallDraw()
            {
                layer = l,
                id = id,
                start_p = getVMPoint_FromCanvasPoint(size, s_p),
                end_p = getVMPoint_FromCanvasPoint(size, e_p),
                thickness = getVMValue_FromCanvasValue(size, thick),
                //height = getVMValue_FromDefaultValue(height),
                height = getVMValue_FromCanvasValue(size, height),
                alpha = alpha,
                colorA = brush.Color.A,
                colorR = brush.Color.R,
                colorG = brush.Color.G,
                colorB = brush.Color.B
            };
            //벽의 각끝 모서리 계산
            Point spA, spB, epA, epB;
            Boolean reverse = caculateWallPoint(w, out spA, out  spB, out  epA, out epB);
            if (reverse)
            {
                Point sp = w.start_p;
                Point ep = w.end_p;

                w.start_p = ep;
                w.end_p = sp;
            }

            w.start_pA = spA;
            w.start_pB = spB;
            w.end_pA = epA;
            w.end_pB = epB;

            return w;
        }

        public WallDraw makeWallVMDataFor3d(WallDraw w)
        {
            return new WallDraw()
            {
                id = w.id,
                start_p = get3DPoint_FromVMPoint(w.start_p),
                start_pA = get3DPoint_FromVMPoint(w.start_pA),
                start_pB = get3DPoint_FromVMPoint(w.start_pB),
                end_p = get3DPoint_FromVMPoint(w.end_p),
                end_pA = get3DPoint_FromVMPoint(w.end_pA),
                end_pB = get3DPoint_FromVMPoint(w.end_pB),
                thickness = get3DValue_FromVMValue(w.thickness),
                height = get3DValue_FromVMValue(w.height),
                alpha = w.alpha,
                colorA = w.colorA,
                colorR = w.colorR,
                colorG = w.colorG,
                colorB = w.colorB,
                Z = w.Z
            };
        }


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

#if false
        public WallDraw makeWallVmData(int l, int id, Size size, Point s_p, Point e_p, SolidColorBrush brush, Double thick, Double height, Double alpha)
        {
            Double height2 = (Double)id * (height / 10000) + height;
            WallDraw w = new WallDraw()
            {
                layer = l,
                id = id,
                start_p = getVMPoint_FromCanvasPoint(size, s_p),
                end_p = getVMPoint_FromCanvasPoint(size, e_p),
                thickness = getVMValue_FromCanvasValue(size, thick),
                height = getVMValue_FromDefaultValue(height2),
                //height = getVMValue_FromCanvasValue(size, height2),
                alpha = alpha,
                colorA = brush.Color.A,
                colorR = brush.Color.R,
                colorG = brush.Color.G,
                colorB = brush.Color.B
            };

            //벽의 각끝 모서리 계산
            Point spA, spB, epA, epB;
            Boolean reverse = caculateWallPoint(w, out spA, out  spB, out  epA, out epB);
            if (reverse)
            {
                Point sp = w.start_p;
                Point ep = w.end_p;

                w.start_p = ep;
                w.end_p = sp;
            }

            w.start_pA = spA;
            w.start_pB = spB;
            w.end_pA = epA;
            w.end_pB = epB;

            return w;
        } 
#endif

        public WallCornerDraw convertWallCornerCanvasToDB(Size size, WallCornerDraw wcdc)
        {
            WallCornerDraw wcd = new WallCornerDraw()
            {
                layer = wcdc.layer,
                id = wcdc.id,

                alpha = wcdc.alpha,
                angle = wcdc.angle,
                colorA = wcdc.colorA,
                colorR = wcdc.colorR,
                colorG = wcdc.colorG,
                colorB = wcdc.colorB,


                height = getVMValue_FromCanvasValue(size, wcdc.height),
                Z = wcdc.Z,
                w1_id = wcdc.w1_id,
                w2_id = wcdc.w2_id
            };

            foreach (var p in wcdc.p_list)
            {
                Point tmp_p = getVMPoint_FromCanvasPoint(size, p);
                wcd.p_list.Add(tmp_p);
            }

            return wcd;
        }

        public WallCornerDraw makeWallCornerDraw(WallDraw w, WallDraw con_w, int id)
        {    
            WallCornerDraw wcd = caculateWallCorner(w, con_w, id);
            return wcd;
        }


        private WallCornerDraw caculateWallCorner(WallDraw w1, WallDraw w2, int id)
        {
            WallCornerDraw wcd = new WallCornerDraw();
            
            //기본값 설정
            wcd.layer = w1.layer;
            wcd.id = id;
            wcd.colorA = w1.colorA;
            wcd.colorR = w1.colorR;
            wcd.colorG = w1.colorG;
            wcd.colorB = w1.colorB;


            //p_w1_endA                       p_w1_A
            //  |--------------------------------|  
            //  |                                | 
            //  | pA                  p_w2_B  pO |         p_w2_A
            //  |                       |--------|---------| 
            //  |                       |        |         |
            //  |-----------------------|--------|         |
            //p_w1_endB                 |       p_w1_B     |
            //                          |                  |
            //                          ~                  ~
            //                          |------------------|
            //                    p_w2_endB      pB        p_w2_endA
            //


            Point pO;// 연결 점
            Point pA;// W의 반대쪽끝
            Point pB;// W2의 반대쪽 끝

            //W 쪽에서 연결점 옆의 두점
            Point p_w1_A;
            Point p_w1_B;

            //CW쪽에서 연결점 옆에 두점
            Point p_w2_A;
            Point p_w2_B;

            //각각의 벽에 연결점 반대쪽의 한점
            Point p_w1_endA; 
            Point p_w1_endB;
            Point p_w2_endA;
            Point p_w2_endB;
            
            //위의 각 점들을 판단한다
            if (w1.start_p == w2.start_p)
            {
                pO = w1.start_p;
                pA = w1.end_p;
                pB = w2.end_p;

                p_w1_A = w1.start_pA;
                p_w1_B = w1.start_pB;
                p_w1_endA = w1.end_pA;
                p_w1_endB = w1.end_pB;

                p_w2_A = w2.start_pA;
                p_w2_B = w2.start_pB;
                p_w2_endA = w2.end_pA;
                p_w2_endB = w2.end_pB;
            }
            else if (w1.start_p == w2.end_p)
            {
                pO = w1.start_p;
                pA = w1.end_p;
                pB = w2.start_p;

                p_w1_A = w1.start_pA;
                p_w1_B = w1.start_pB;
                p_w1_endA = w1.end_pA;
                p_w1_endB = w1.end_pB;

                p_w2_A = w2.end_pA;
                p_w2_B = w2.end_pB;
                p_w2_endA = w2.start_pA;
                p_w2_endB = w2.start_pB;
            }
            else if (w1.end_p == w2.start_p)
            {
                pO = w1.end_p;
                pA = w1.start_p;
                pB = w2.end_p;

                p_w1_A = w1.end_pA;
                p_w1_B = w1.end_pB;
                p_w1_endA = w1.start_pA;
                p_w1_endB = w1.start_pB;

                p_w2_A = w2.start_pA;
                p_w2_B = w2.start_pB;
                p_w2_endA = w2.end_pA;
                p_w2_endB = w2.end_pB;
            }
            else if (w1.end_p == w2.end_p)
            {
                pO = w1.end_p;
                pA = w1.start_p;
                pB = w2.start_p;

                p_w1_A = w1.end_pA;
                p_w1_B = w1.end_pB;
                p_w1_endA = w1.start_pA;
                p_w1_endB = w1.start_pB;

                p_w2_A = w2.end_pA;
                p_w2_B = w2.end_pB;
                p_w2_endA = w2.start_pA;
                p_w2_endB = w2.start_pB;
            }
            else
                return null;


            //세점 사이의 각도를 계산한다
            
            //A,B,C 세점 사이의 각
            //Acos( (OA^2 + OB^2 - AB^2)/(2*OA*OB) )
            
            //pA-pO, pB-pO을 구한다
            Point OA = new Point(pA.X - pO.X, pA.Y - pO.Y);
            Point OB = new Point(pB.X - pO.X, pB.Y - pO.Y);
            Point AB = new Point(pB.X - pA.X, pB.Y - pA.Y);
            Double OA2 = Pro(OA.X) + Pro(OA.Y);
            Double OB2 = Pro(OB.X) + Pro(OB.Y);
            Double AB2 = Pro(AB.X) + Pro(AB.Y);

            Double radian;
            Double angle;
            Double K = (OA2 + OB2 - AB2);
            if(K==0)
            {
                angle = 90;
            }
            else
            {
                radian = Math.Acos((OA2 + OB2 - AB2) / (2 * Rt(OA2) * Rt(OB2)));
                angle = RadianToAngle(radian);
            }

            // w1 의 연결점 근처의 점중에 어떤점이 w2 내부에 포함되있는지 판단한다
            // w2 내부에 들어가지 않은 점만을 연결 한다
            Point p_w1, p_w2, p_w1_end, p_w2_end;


            Boolean W1A_in_W2 = (isIn(p_w1_A, p_w2_A, p_w2_endB)) || (isIn(p_w1_A, p_w2_B, p_w2_endA));
            Boolean W1B_in_W2 = (isIn(p_w1_B, p_w2_A, p_w2_endB)) || (isIn(p_w1_B, p_w2_B, p_w2_endA));

            Boolean W2A_in_W1 = (isIn(p_w2_A, p_w1_A, p_w1_endB)) || (isIn(p_w2_A, p_w1_B, p_w1_endA));
            Boolean W2B_in_W1 = (isIn(p_w2_B, p_w1_A, p_w1_endB)) || (isIn(p_w2_B, p_w1_B, p_w1_endA));


            if ((W1A_in_W2 && W1B_in_W2) || (W2A_in_W1 && W2B_in_W1))
                return null;


            //w1의 A,B점중 w2에 포함되지 않은 것을 찾는다
            if (W1A_in_W2)
            {
                p_w1 = p_w1_B;
                p_w1_end = p_w1_endB;
            }
            else
            {
                p_w1 = p_w1_A;
                p_w1_end = p_w1_endA;
            }
            
            //w2의 A,B점중 w1에 포함되지 않은 것을 찾는다
            if (W2A_in_W1)
            {
                p_w2 = p_w2_B;
                p_w2_end = p_w2_endB;
            }
            else
            {
                p_w2 = p_w2_A;
                p_w2_end = p_w2_endA;
            }

            if ((angle < 180) && (angle > 20))
            {
                //외각선이 만나는 교점을 계산한다
                Point cross_p = caculateCrossPoint(p_w1, p_w1_end, p_w2, p_w2_end);
                wcd.p_list.Add(p_w1);
                wcd.p_list.Add(cross_p);
                wcd.p_list.Add(p_w2);
                wcd.p_list.Add(pO);

            }
            else
                return null; 
            //else
            //{
            //    wcd.p_list.Add(p_w1);
            //    wcd.p_list.Add(p_w2);
            //    wcd.p_list.Add(pO);
            //}
            wcd.w1_id = w1.id;
            wcd.w2_id = w2.id;
            wcd.angle = angle;
            wcd.alpha = w1.alpha;
            wcd.height = w1.height;
            

            return wcd;
        }


        private Point caculateCrossPoint(Point p_A1, Point p_A2, Point p_B1, Point p_B2)
        {
            Double termAX = (p_A2.X - p_A1.X);
            Double termBX = (p_B2.X - p_B1.X);



            if ((termAX == 0) || (termBX == 0))
            {
                if (termAX == 0)
                {
                    if ((p_B2.Y - p_B1.Y) == 0)
                        return new Point(p_A2.X, p_B2.Y);
                    Double grB = (p_B2.Y - p_B1.Y) / (p_B2.X - p_B1.X);
                    Double cB = p_B1.Y - grB * p_B1.X;
                    //termAX가 0이므로 X값의 변화가 없는 것이므로 X가 p_A2.X 에 해당하는 Y를 구하면 된다
                    Double X = p_A1.X;
                    Double Y = grB * X + cB;
                    return new Point(X, Y);
                }
                else if (termBX == 0)
                {
                    if((p_A2.Y - p_A1.Y)==0)
                    {
                        return new Point(p_B2.X, p_A2.Y);
                    }
                    Double grA = (p_A2.Y - p_A1.Y) / (p_A2.X - p_A1.X);
                    Double cA = p_A1.Y - grA * p_A1.X;
                    Double X = p_A1.X;
                    Double Y = grA * X + cA;
                    return new Point(X, Y);
                }
                else
                    return new Point(0,0);
            }
            else
            {
                //두 직선의 기울기 계산
                //gr = (y2 - y1)/(x2-x1)
                Double grB = (p_B2.Y - p_B1.Y) / (p_B2.X - p_B1.X);
                Double grA = (p_A2.Y - p_A1.Y) / (p_A2.X - p_A1.X);


                //y =  grX  + c 공식에서 c = y- grX;
                Double cA = p_A1.Y - grA * p_A1.X;
                Double cB = p_B1.Y - grB * p_B1.X;

                //아래의 두가지 식을 모두 충족하는 X,Y가 교점이므로
                //Y = grA* X + cA
                //Y = grB* X + cB
                // X,Y는 아래와 같다
                // X = (cB-cA)/(grA - grB);
                // Y = grA * X + cA

                //예외) grA , grB가 모두 0인경우

                Double X, Y;
                if ((grA - grB) == 0)
                {
                    if (termAX == 0)
                    {
                        Y = p_B1.Y;
                        X = p_A1.X;
                    }
                    else
                    {
                        Y = p_A1.Y;
                        X = p_B1.X;
                    }
                }
                else
                {
                    X = (cB - cA) / (grA - grB);
                    Y = grA * X + cA;
                }
                return new Point(X, Y);
            }
        }

        private Boolean isIn(Point check_p,Point st_p, Point end_p)
        {
            Boolean check_X = false;
            Boolean check_Y = false;

            Double startX;
            Double startY;
            Double endX;
            Double endY;

            if (st_p.X < end_p.X)
            {
                startX = st_p.X;
                endX = end_p.X;
            }
            else
            {
                startX = end_p.X;
                endX = st_p.X;
            }

            if(st_p.Y < end_p.Y)
            {
                startY = st_p.Y;
                endY = end_p.Y;
            }
            else
            {
                startY = end_p.Y;
                endY = st_p.Y;
            }


            if ((check_p.X >= startX) && (check_p.X <= endX))
                check_X = true;
          
            if ((check_p.Y >= startY) && (check_p.Y <= endY))
                check_Y = true;

            if ((check_X == true) && (check_Y == true))
                return true;
            else
                return false;

        }


        private Boolean caculateWallPoint(WallDraw w, out Point spA, out Point spB, out Point epA, out Point epB)
        {
            Point wp1 = w.start_p;
            Point wp2 = w.end_p;

            Point startPosition = new Point();
            Point endPosition = new Point();

            Boolean reverse_y = false;

            //double thiness = drawDataMgr.get3DValue_FromVMValue(w.thickness);
            double thiness = w.thickness;

            //double _pointToPointX = wp2.X - wp1.X;
            //if (_pointToPointX> 0)

            double _pointToPointY = wp2.Y - wp1.Y;
            if (_pointToPointY > 0)
            {
                startPosition.X = wp1.X;
                startPosition.Y = wp1.Y;
                endPosition.X = wp2.X;
                endPosition.Y = wp2.Y;
            }
            else
            {
                reverse_y = true;
                startPosition.X = wp2.X;
                startPosition.Y = wp2.Y;
                endPosition.X = wp1.X;
                endPosition.Y = wp1.Y;
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

            spA = new Point(_2D_X0, _2D_Y0);
            spB = new Point(_2D_X1, _2D_Y1);
            epA = new Point(_2D_X2, _2D_Y2);
            epB = new Point(_2D_X3, _2D_Y3);

            return reverse_y;
        }


        public WallDraw ConvertCanvasWallToDBWall(Size size, WallDraw dbw)
        {
            WallDraw w = new WallDraw()
            {
                id = dbw.id,
                layer = dbw.layer,
                start_p = getVMPoint_FromCanvasPoint(size, dbw.start_p),
                end_p = getVMPoint_FromCanvasPoint(size, dbw.end_p),
                thickness = getVMValue_FromCanvasValue(size, dbw.thickness),
                height = getVMValue_FromCanvasValue(size, dbw.height),
                alpha = dbw.alpha,
                colorA = dbw.colorA,
                colorR = dbw.colorR,
                colorG = dbw.colorG,
                colorB = dbw.colorB,

                start_pA = getVMPoint_FromCanvasPoint(size, dbw.start_pA),
                start_pB = getVMPoint_FromCanvasPoint(size, dbw.start_pB),
                end_pA = getVMPoint_FromCanvasPoint(size, dbw.end_pA),
                end_pB = getVMPoint_FromCanvasPoint(size, dbw.end_pB)
            };
            return w;
        }

        public WallDraw ConvertDBWallDataToCanvasWall(Size size, WallDraw dbw)
        {
            WallDraw w = new WallDraw()
            {
                id = dbw.id,
                layer = dbw.layer,
                start_p = getCanvasPoint_FromVMPoint(size, dbw.start_p),
                end_p = getCanvasPoint_FromVMPoint(size, dbw.end_p),
                thickness = getCanvasValue_FromVMValue(size, dbw.thickness),
                height = getCanvasValue_FromVMValue(size, dbw.height),
                alpha = dbw.alpha,
                colorA = dbw.colorA,
                colorR = dbw.colorR,
                colorG = dbw.colorG,
                colorB = dbw.colorB,

                start_pA =getCanvasPoint_FromVMPoint(size, dbw.start_pA),
                start_pB = getCanvasPoint_FromVMPoint(size,dbw.start_pB),
                end_pA = getCanvasPoint_FromVMPoint(size,dbw.end_pA),
                end_pB = getCanvasPoint_FromVMPoint(size,dbw.end_pB)
            };
            return w;
        }

        public WallCornerDraw convertWallCornerDBtoCanvas(Size size, WallCornerDraw dbwcd)
        {
            WallCornerDraw wcd = new WallCornerDraw()
            {
                layer = dbwcd.layer,
                id = dbwcd.id,

                angle = dbwcd.angle,
                height = getCanvasValue_FromVMValue(size, dbwcd.height),

                alpha = dbwcd.alpha,
                colorA = dbwcd.colorA,
                colorR = dbwcd.colorR,
                colorG = dbwcd.colorG,
                colorB = dbwcd.colorB,
            };


            foreach(var p in dbwcd.p_list)
            {
                wcd.p_list.Add(getCanvasPoint_FromVMPoint(size, p));
            }
            return wcd;
        }

        public WallCornerDraw convertWallCornerDBto3D(WallCornerDraw dbwcd)
        {
            WallCornerDraw wcd = new WallCornerDraw()
            {
                layer = dbwcd.layer,
                id = dbwcd.id,

                angle = dbwcd.angle,
                height = get3DValue_FromVMValue(dbwcd.height),

                alpha = dbwcd.alpha,
                colorA = dbwcd.colorA,
                colorR = dbwcd.colorR,
                colorG = dbwcd.colorG,
                colorB = dbwcd.colorB,
            };


            foreach (var p in dbwcd.p_list)
            {
                wcd.p_list.Add(get3DPoint_FromVMPoint(p));
            }
            return wcd;
        }

        private Double Pro(Double src)
        {
            return Math.Pow(src, 2);
        }

        private Double Rt(Double src)
        {
            return Math.Pow(src, 0.5);
        }

        private double AngleToRadian(double _angle)
        {
            return _angle * (Math.PI / 180);
        }

        private double RadianToAngle(double _radian)
        {
            return _radian * (180 / Math.PI);
        }

        public Double getRatio_byDB(Size size)
        {
            return DBsize.Width / size.Width;
        }

        public Double getRatio_byDefault(Size size)
        {
            return size.Width/1024;
        }

        public Boolean saveDrawingData(List<WallDraw>[] w_list, List<WallCornerDraw>[] wc_list, String save_path)
        {
            if (!(File.Exists(save_path)))
            {
                BinaryFormatter saveformat = new BinaryFormatter();
                DrawingSaveModel dsm = new DrawingSaveModel();
                dsm.w_list = w_list;
                dsm.wc_list = wc_list;

                try
                {
                    FileStream saveStream = new FileStream(save_path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    //saveformat.Serialize(saveStream, w_list);
                    //saveformat.Serialize(saveStream, wc_list);
                    saveformat.Serialize(saveStream, dsm);

                    saveStream.Close();

                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0}", e.Message);
                    MessageBox.Show(e.Message);
                    return false;
                }
            }
            return false;
        }

        public DrawingSaveModel openDrawingData(String open_path)
        {
            if (File.Exists(open_path))
            {
                try
                {
                    BinaryFormatter openformat = new BinaryFormatter();
                    FileStream openStream = new FileStream(open_path, FileMode.Open, FileAccess.Read);
                    DrawingSaveModel open_data = new DrawingSaveModel();

                    open_data = (DrawingSaveModel)openformat.Deserialize(openStream);

                    openStream.Close();

                    return open_data;

                }
                catch (Exception e)
                {
                    Console.WriteLine("{0}", e.Message);
                    //MessageBox.Show("Please check image file name. (openDrawingData)");
                    return null;
                }
            }
            return null;
        }
    }
}
