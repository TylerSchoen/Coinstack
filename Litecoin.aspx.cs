using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Btce;

public partial class Litecoin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var ticker = BtceApi.GetTicker(BtcePair.ltc_usd);

    }
}