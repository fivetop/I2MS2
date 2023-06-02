using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I2MS2.Library
{
    // 스크린 관리 
    public static class ScreenRatio
    {
        public static double default_screen_x = 1024;
        public static double default_screen_y = 768;

        // Max: x=102400 y=76800
        public static int getDbX(double? screen_pos_x, double screen_width)
        {
            double x = screen_pos_x ?? 0;
            double w = screen_width;
            if (w == 0)
                w = default_screen_x;
            int r = (int)(x * default_screen_x * 100 / w);
            if (r < 0)
                r = 0;
            if (r > default_screen_x * 100)
                r = (int)(default_screen_x * 100);
            return r;
        }

        public static int getDbY(double? screen_pos_y, double screen_height)
        {
            double y = screen_pos_y ?? 0;
            double h = screen_height;
            if (h == 0)
                h = default_screen_y;
            int r = (int)(y * default_screen_y * 100 / h);
            if (r < 0)
                r = 0;
            if (r > default_screen_y * 100)
                r = (int)(default_screen_y * 100);
            return r;
        }

        // Max: x=screen_width(800) y=screen_height(600)
        public static double getScreenX(int? db_pos_x, double screen_width)
        {
            int x = db_pos_x ?? 0;
            double w = screen_width;
            if (w == 0)
                w = default_screen_x;
            double r = x * w / default_screen_x / 100;
            if (r < 0)
                r = 0;
            if (r > w)
                r = (int)w;
            return r;
        }

        public static double getScreenY(int? db_pos_y, double screen_height)
        {
            int y = db_pos_y ?? 0;
            double h = screen_height;
            if (h == 0)
                h = default_screen_y;
            double r = y * h / default_screen_y / 100;
            if (r < 0)
                r = 0;
            if (r > h)
                r = (int)h;
            return r;
        }

    }
}
