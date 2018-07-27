using System;

namespace RoutinesLibrary.Data
{
    public class DateTimeHelper
    {
        public static long DateTimeToUnixTimeStamp(DateTime date)
        {
            long unixTimestamp = date.Ticks - new DateTime(1970, 1, 1).Ticks;
            unixTimestamp /= TimeSpan.TicksPerSecond;

            return unixTimestamp;
        }

        public static DateTime UnixTimeStampToDateTime(long unixTimestamp)
        {
            DateTime date = new DateTime(1970, 1, 1);
            long unixTimeStampInTicks = unixTimestamp * TimeSpan.TicksPerSecond;

            return new DateTime(date.Ticks + unixTimeStampInTicks);
        }
    }
}