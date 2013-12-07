using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using Newtonsoft.Json;
using Btce;

public partial class Money : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        WebClient w = new WebClient();
        string url = "http://openexchangerates.org/api/latest.json?app_id=4960fa032dd44e4c8a506ee580b395df";

        var jObj = JsonConvert.DeserializeObject<RootObject>(w.DownloadString(url));
        string serializeJObj;
        
        // Retrieve LTC price in USD from BTC-e API
        var ticker = BtceApi.GetTicker(BtcePair.ltc_usd);
        double ltc = ticker.buy;
        // Pass the conversion rate to the view
        jObj.rates.LTC = ltc;

        serializeJObj = JsonConvert.SerializeObject(jObj);

        Response.ContentType = "application/json";
        Response.Write(serializeJObj);
        Response.End();
    }
}