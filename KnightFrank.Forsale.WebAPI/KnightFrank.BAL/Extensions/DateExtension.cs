using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightFrank.BAL.Extensions
{
    public static class DateExtension
    {
        public static string ToDisplayDate(this DateTime dateTime)
        {
            return dateTime.ToString("dd MMM yy");
        }
    }
}
