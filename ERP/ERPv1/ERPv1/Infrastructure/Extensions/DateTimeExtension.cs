using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.Infrastructure.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime ConvertDate(this string date)
        {
            DateTime dt;
            bool IsConvertDate = DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
            if(!IsConvertDate)
            { dt = DateTime.Parse(date); }
            return dt;
        }
    }
}
