using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrimpNet
{
    public static partial class Extensions
    {
        /// <summary>
        /// Create a date value that is safe to persist to MS-SQL
        /// </summary>
        /// <param name="dotNetDate">Value to be persisted</param>
        /// <returns>Date that has been truncated to SQLMin or SQLMax dates</returns>
        public static DateTime ToSqlDate(this DateTime dotNetDate)
        {
            return Utils.Date.ToSqlDate(dotNetDate);
        }

        private static readonly long unixZeroTimeTicks = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks;

        /// <summary>
        /// Convert a .Net DateTime to standard Unix int format
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>Number of seconds since 1/1/1970 </returns>
        public static long ToUnixTime(this DateTime value)
        {
            return (long)((value.ToUniversalTime().Ticks - unixZeroTimeTicks) / 10000);
        }

        /// <summary>
        /// Convert a Unix int date to .Net DateTime (Local time zone)
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>Number of seconds since 1/1/1970 </returns>
        public static DateTime FromUnixTime(this long value)
        {

            return (new DateTime(unixZeroTimeTicks+(value*10000)).ToLocalTime());
        }
    }
}
