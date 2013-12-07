using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace Btce
{
    /// <summary>
    /// Summary description for Ticker
    /// </summary>
    public class Ticker
    {
        public double high { get; private set; }
        public double low { get; private set; }
        public double avg { get; private set; }
        public double vol { get; private set; }
        public double vol_cur { get; private set; }
        public double last { get; private set; }
        public double buy { get; private set; }
        public double sell { get; private set; }
        public int updated { get; private set; }
        public UInt32 server_time { get; private set; }
        public static Ticker ReadFromJObject(JObject jObj)
        {
            if (jObj == null)
            {
                return null;
            }
            else
            {
                return new Ticker()
                {
                    high = jObj.Value<double>("avg"),
                    low = jObj.Value<double>("low"),
                    avg = jObj.Value<double>("avg"),
                    vol = jObj.Value<double>("vol"),
                    vol_cur = jObj.Value<double>("vol_cur"),
                    last = jObj.Value<double>("last"),
                    buy = jObj.Value<double>("buy"),
                    sell = jObj.Value<double>("sell"),
                    server_time = jObj.Value<UInt32>("server_time"),
                };
            }
        }
    }
}