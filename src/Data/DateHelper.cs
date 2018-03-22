using System;

namespace RoutinesLibrary.Data
{
    public class DateHelper
    {
        public static long DateToTimeStamp(DateTime date)
        {
            long unixTimestamp = date.Ticks - new DateTime(1970, 1, 1).Ticks;
            unixTimestamp /= TimeSpan.TicksPerSecond;

            return unixTimestamp;
        }

        public static DateTime TimeStampToDate(long timestamp)
        {
            DateTime date = new DateTime(1970, 1, 1);
            long unixTimeStampInTicks = timestamp * TimeSpan.TicksPerSecond;

            return new DateTime(date.Ticks + unixTimeStampInTicks);
        }
    }
}