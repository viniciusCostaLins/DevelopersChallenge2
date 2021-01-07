using System;

namespace Nibo.Domain.Utils
{
    public static class NiboUtil
    {
        public static DateTime ToDate(this string date)
        {
            try
            {
                if (date.Length < 8)
                {
                    return new DateTime();
                }

                var dd = Int32.Parse(date.Substring(6, 2));
                var mm = Int32.Parse(date.Substring(4, 2));
                var yyyy = Int32.Parse(date.Substring(0, 4));

                return new DateTime(yyyy, mm, dd);
            }
            catch
            {
                throw new Exception("Unable to parse date");
            }
        }
    }
}
