using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;
using Btce.Utils;

namespace Btce
{
    public class BtceApi
    {
        string key;
        HMACSHA512 hasher;
        UInt32 nonce;
        public BtceApi(string key, string secret)
        {
            this.key = key;
            hasher = new HMACSHA512(Encoding.ASCII.GetBytes(secret));
            nonce = UnixTime.Now;
        }

        string Query(Dictionary<string, string> args)
        {
            args.Add("nonce", GetNonce().ToString());

            var dataStr = BuildPostData(args);
            var data = Encoding.ASCII.GetBytes(dataStr);

            var request = WebRequest.Create(new Uri("https://btc-e.com/tapi")) as HttpWebRequest;
            if (request == null)
                throw new Exception("Non HTTP WebRequest");

            request.Method = "POST";
            request.Timeout = 15000;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            request.Headers.Add("Key", key);
            request.Headers.Add("Sign", ByteArrayToString(hasher.ComputeHash(data)).ToLower());
            var reqStream = request.GetRequestStream();
            reqStream.Write(data, 0, data.Length);
            reqStream.Close();
            return new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
        }
        static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }
        static string BuildPostData(Dictionary<string, string> d)
        {
            StringBuilder s = new StringBuilder();
            foreach (var item in d)
            {
                s.AppendFormat("{0}={1}", item.Key, HttpUtility.UrlEncode(item.Value));
                s.Append("&");
            }
            if (s.Length > 0) s.Remove(s.Length - 1, 1);
            return s.ToString();
        }
        UInt32 GetNonce()
        {
            return nonce++;
        }
        public static Ticker GetTicker(BtcePair pair)
        {
            string queryStr = string.Format("http://btc-e.com/api/2/{0}/ticker", BtcePairHelper.ToString(pair));
            return Ticker.ReadFromJObject(JObject.Parse(Query(queryStr))["ticker"] as JObject);
        }
        static string Query(string url)
        {
            var request = WebRequest.Create(url);
            request.Proxy = WebRequest.DefaultWebProxy;
            request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            if (request == null)
                throw new Exception("Non HTTP WebRequest");
            return new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
        }
    }
}