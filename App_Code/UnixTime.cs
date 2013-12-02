using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Btce.Utils
{
    public static class UnixTime
    {
        static DateTime unixEpoch = new DateTime(1970, 1, 1);
        public static UInt32 Now { get { return GetFromDateTime(DateTime.UtcNow); } }
        public static UInt32 GetFromDateTime(DateTime dateTime) { return (UInt32)(dateTime - unixEpoch).TotalSeconds; }
        public static DateTime ConvertToDateTime(UInt32 unixTime) { return unixEpoch.AddSeconds(unixTime); }
    }
}