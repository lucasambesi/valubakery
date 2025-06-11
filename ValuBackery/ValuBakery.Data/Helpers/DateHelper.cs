using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValuBakery.Data.Helpers
{
    public static class DateHelper
    {
        public static readonly string OnlyDatePattern = "yyyy-MM-dd";

        public static readonly string OnlyTimePattern = "hh:mm:ss";

        public static readonly string DateTimePattern = "yyyy-MM-ddThh:mm:ss";

        public static readonly DateTime MaxDateTime = DateTime.MaxValue;

        public static DateTime CurrenctDateUTC()
        {
            return DateTime.UtcNow.Date;
        }

        public static DateTime CurrenctUtcNow()
        {
            return DateTime.UtcNow;
        }

        public static string UtcNowString()
        {
            return DateTime.UtcNow.Date.ToString(OnlyDatePattern);
        }
    }
}
