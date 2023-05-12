using HealthCare.Context;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace HealthCare.Repository
{
    public static class Utility
    {
        public static DateTime ParseDate(string str)
        {
            return DateTime.ParseExact(str, Global.dateFormat, CultureInfo.InvariantCulture);
        }

        internal static TimeSpan ParseDuration(string str)
        {
            return TimeSpan.ParseExact(str, Global.timeSpanFormat, CultureInfo.InvariantCulture);
        }

        public static T Parse<T>(string str) 
        {
            return (T)Enum.Parse(typeof(T), str);
        }

        public static string ToString(DateTime dt)
        {
            return dt.ToString(Global.dateFormat);
        }
        public static string ToString(TimeSpan dt)
        {
            return dt.ToString(Global.timeSpanFormat);
        }

        public static string ToString<T>(IEnumerable<T> data, char separator = '|')
        {
            return string.Join(separator, data);
        }

        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}
