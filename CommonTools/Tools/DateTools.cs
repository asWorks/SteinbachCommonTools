using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonTools.Tools
{
    public class DateTools
    {

        public static DateTime GetTodayWithCustomTime(int hour, int minute, int second)
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;

            return new DateTime(year, month, day, hour, minute, second);

        }
        /// <summary>
        /// Gibt aktuelles Datum mit aktueller voller Stunde zurück
        /// </summary>
        /// <param name="FullHourOffset">Offset der zur volle Stunde addiert wird. </param>
        /// <param name="minute">Minuten</param>

        /// <returns>DateTime</returns>
        public static DateTime GetTodayWithCustomTime(int FullHourOffset, int minute = 0)
        {
            int h = DateTime.Now.Hour + FullHourOffset;
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;

            return new DateTime(year, month, day, h, minute, 0);

        }
        /// <summary>
        /// Gibt das übergebene Datum mit der übergbenen Uhrzeit zurück.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static DateTime GetDateWithCustomHour(DateTime dt, int hour, int minute, int second)
        {
            int year = dt.Year;
            int month = dt.Month;
            int day = dt.Day;

            return new DateTime(year, month, day, hour, minute, 0);

        }
    }

}
