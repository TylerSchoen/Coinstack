using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Ticker
/// </summary>
public class Ticker
{
    public double high { get; set; }
    public double low { get; set; }
    public double avg { get; set; }
    public double vol { get; set; }
    public double vol_cur { get; set; }
    public double last { get; set; }
    public double buy { get; set; }
    public double sell { get; set; }
    public int updated { get; set; }
    public int server_time { get; set; }
}

public class RootObject
{
    public Ticker ticker { get; set; }
}