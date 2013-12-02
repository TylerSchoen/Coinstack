using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Btce
{
    public enum BtcePair
    {
        btc_usd,
        btc_rur,
        btc_eur,
        ltc_btc,
        ltc_usd,
        ltc_rur,
        nmc_btc,
        nvc_btc,
        usd_rur,
        eur_usd,
        trc_btc,
        ppc_btc,
        ftc_btc,
        Unknown
    }
    public class BtcePairHelper
    {
        public static BtcePair FromString(string str)
        {
            BtcePair pair = BtcePair.Unknown;
            Enum.TryParse<BtcePair>(str.ToLowerInvariant(), out pair);
            return pair;
        }
        public static string ToString(BtcePair val)
        {
            return Enum.GetName(typeof(BtcePair), val).ToLowerInvariant();
        }
    }
}