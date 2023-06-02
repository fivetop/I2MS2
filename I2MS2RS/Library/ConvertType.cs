using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I2MS2.Library
{
    // 타입 변환
    public static class ConvertType
    {
        public static Nullable<DateTime> convertString2NullDate(string date_string)
        {
            DateTime date;
            if (DateTime.TryParse(date_string, out date))
                return date;
            return null;
        }

        public static DateTime convertString2Date(string date_string)
        {
            DateTime date;
            DateTime.TryParse(date_string, out date);
            return date;
        }

        public static Nullable<int> convertString2NullInt(string num_string)
        {
            int num;
            if (int.TryParse(num_string, out num))
                return num;
            return null;
        }

        public static int convertString2Int(string num_string)
        {
            int num;
            int.TryParse(num_string, out num);
            return num;
        }

    }
}
