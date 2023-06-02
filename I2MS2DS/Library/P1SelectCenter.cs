using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WebApi.Models;
using I2MS2.Models;
using I2MS2.UserControls;
using System.Windows.Media.Effects;
using System.Windows.Media;
using I2MS2.Animation;

namespace I2MS2.Library
{
    // 리전 사용 
    class P1SelectCenter
    {
        //윌드맵의 크기를 지정해서 기록(region1의 위치를 추가하거나 할때 비교를 위해서 변경시 반드시 확인 필요!!!)
        public Size map_size;
        public Point flag_text_defaut_point = new Point(70, 80);

        //region의 이름을 표시하는 Text UserControl 리스트
        List<regionFlagTextControl> region_flag_text_list = new List<regionFlagTextControl>();

        //region의 위치를 표시하는 flag UserControl 리스트
        List<regionFlagControl> region_flag_list = new List<regionFlagControl>();



        public void setMapSize(double width, double height)
        {
            map_size.Width = width;
            map_size.Height = height;
        }

        //region flag를 추가하는 함수
        public regionFlagControl addRegionFlag(int region_id, int posx, int posy)
        {
            regionFlagControl tmp_flag = makeRegionFlag(region_id, posx, posy);
            region_flag_list.Add(tmp_flag);

            return tmp_flag;
        }

        public regionFlagTextControl addRegionFlagText(int region_id, string region_name, int posx, int posy)
        {
            regionFlagTextControl tmp_flag_text = makeRegionFlagText(region_id, region_name, posx, posy);
            region_flag_text_list.Add(tmp_flag_text);

            return tmp_flag_text;
        }

        public regionFlagControl changeFlagMargin(int region_id, int posx, int posy)
        {
            regionFlagControl tmp_flag = (regionFlagControl)region_flag_list.First(at => at.id == region_id);
            Point tp = changePosition_DefaultToMap((double)posx, (double)posy);
            Thickness tempFlagMargin = new Thickness(tp.X - tmp_flag.Width / 2, tp.Y - tmp_flag.Height / 2, 0, 0);
            tmp_flag.Margin = tempFlagMargin;
            //Console.Write("{0}:",tmp_flag.Name);
            //Console.Write("from pos({0},{1})   ", posx, posy);
            //Console.Write("to pos({0},{1})   ", tp.X, tp.Y);
            //Console.WriteLine("to Margin({0},{1})", tmp_flag.Margin.Left, tmp_flag.Margin.Top);
            return tmp_flag;
        }

        public regionFlagTextControl changeFlagTextMargin(int region_id, int posx, int posy, string region_name)
        {
            regionFlagTextControl tmp_flag_text = (regionFlagTextControl)region_flag_text_list.First(at => at.id == region_id);
            Point tp = changePosition_DefaultToMap((double)posx, (double)posy);
            Point margin = changePosition_DefaultToMap(flag_text_defaut_point.X, flag_text_defaut_point.Y);
            Thickness tmp_margin = new Thickness(tp.X - margin.X, tp.Y - margin.Y, 0, 0);
            tmp_flag_text.Margin = tmp_margin;
            tmp_flag_text.region1_text.Text = region_name;
            return tmp_flag_text;
        }

        public void delFlag(int region_id)
        {
            regionFlagControl tmp_flag = (regionFlagControl)region_flag_list.First(at => at.id == region_id);
            region_flag_list.Remove(tmp_flag);
        }

        public void delFlagText(int region_id)
        {
            regionFlagTextControl tmp_text_flag = (regionFlagTextControl)region_flag_text_list.First(at => at.id == region_id);
            region_flag_text_list.Remove(tmp_text_flag);
        }

        
        //flag를 만드는 부분
        public regionFlagControl makeRegionFlag(int region_id, int posx, int posy)
        {
            //기본 위치를 현재 map 사이즈에 맞는 위치로 변경
            Point cp = changePosition_DefaultToMap((double)posx, (double)posy);
            //regionFlagControl region_flag = new regionFlagControl(this);
            regionFlagControl region_flag = new regionFlagControl(region_id);
            region_flag.Width = 50;
            region_flag.Height = 50;
            region_flag.HorizontalAlignment = HorizontalAlignment.Left;
            region_flag.VerticalAlignment = VerticalAlignment.Top;
            Thickness tempMargin = new Thickness(cp.X - region_flag.Width / 2, cp.Y - region_flag.Height / 2, 0, 0);
            region_flag.Margin = tempMargin;

            DropShadowEffect tempDrEf = new DropShadowEffect();
            region_flag.Effect = tempDrEf;


            //Region1Flag 의 이름은 Region1FlagID{regio1n_id} 형태로 만들어 나중에 검색가능하게 해 준다
            region_flag.Name = string.Format("Region1FlagID{0}", region_id); ;
            
            ////MouseLeftButtonDown시에 이벤트를 설정한다 => 이벤트는 각 페이지에서 설정한다
            //region_flag.customMouseEnterEvent += new regionFlagControl.CustomMouseEnterHandler(flagMouseEnterEvent);
            //region_flag.customMouseLeaveEvent += new regionFlagControl.CustomMouseLeaveHandler(flagMouseLeaveEvent);
            
            return region_flag;
        }

        public regionFlagTextControl makeRegionFlagText(int region_id, string region_name, int posx, int posy)
        {
            regionFlagTextControl tmp_flag_text = new regionFlagTextControl(region_id, region_name);

            Point tp = changePosition_DefaultToMap(posx, posy);

            //Text의 경우는 flag위에 올라가야 함으로 60,70 정도 위치 이동시킨다
            Point margin = changePosition_DefaultToMap(flag_text_defaut_point.X, flag_text_defaut_point.Y);
            Thickness tempMargin = new Thickness(tp.X - margin.X, tp.Y - margin.Y, 0, 0);

            tmp_flag_text.Margin = tempMargin;
            tmp_flag_text.HorizontalAlignment = HorizontalAlignment.Left;
            tmp_flag_text.VerticalAlignment = VerticalAlignment.Top;
            tmp_flag_text.Width = 140;
            tmp_flag_text.Height = 30;

            //처음 상태에서는 보이지 않도록 설정한다
            ScaleTransform scaleTran = new ScaleTransform(0, 0);
            tmp_flag_text.RenderTransform = scaleTran;

            //region_flag_text 의 이름은 regionFlagTextID{region1_id} 형태로 만들어 나중에 검색가능하게 해 준다
            tmp_flag_text.Name = string.Format("regionFlagTextID{0}", region_id);

            return tmp_flag_text;
        }



        
        public void FlagTextScaleUp(string flag_name)
        {
            regionFlagTextControl tmp_flag_text = findFlagText_byName(flag_name);
            Point tmp_center = new Point(70, 40);
            Vector tmp_from_v = new Vector(0, 0);
            Vector tmp_to_v = new Vector(1, 1);

            SimpleAnimation anim = new SimpleAnimation();
            anim.uControlScaleAnimation(tmp_flag_text, tmp_from_v, tmp_to_v, tmp_center, 0.8, 100);
        }

        public void FlagTextScaleDown(string flag_name)
        {
            regionFlagTextControl tmp_flag_text = findFlagText_byName(flag_name);

            Point tmp_center = new Point(70, 40);
            Vector tmp_from_v = new Vector(1, 1);
            Vector tmp_to_v = new Vector(0, 0);

            SimpleAnimation anim = new SimpleAnimation();
            anim.uControlScaleAnimation(tmp_flag_text, tmp_from_v, tmp_to_v, tmp_center, 0.8, 100);
        }


        public regionFlagTextControl findFlagText_byName(string flag_name)
        {
            string flag_id = flag_name.Substring(11);
            for (int i = 0; i < region_flag_text_list.Count; i++)
            {
                string flag_text_id = region_flag_text_list[i].Name.Substring(14);
                if (flag_id == flag_text_id)
                {
                    return region_flag_text_list[i];
                }
            }
            return null;
        }



        public regionFlagControl findFlag(int region_id)
        {
            return (regionFlagControl)region_flag_list.First(at => at.id == region_id);
        }

        public regionFlagTextControl findFlagText(int region_id)
        {
            return (regionFlagTextControl)region_flag_text_list.First(at => at.id == region_id);
        }



        #region 기본값과 Map Size에 따른 각각의 플래그 위치 계산을 위한 메소드들
        private Point changePosition_MapToDefault(double in_x, double in_y)
        {
            double out_x = in_x * (g.default_map_size.Width / map_size.Width);
            double out_y = in_y * (g.default_map_size.Height / map_size.Height);

            return new Point(out_x, out_y);
        }

        private Point changePosition_DefaultToMap(double in_x, double in_y)
        {
            double out_x = in_x * (map_size.Width / g.default_map_size.Width);
            double out_y = in_y * (map_size.Height / g.default_map_size.Height);

            return new Point(out_x, out_y);
        }
        #endregion
    }
}
