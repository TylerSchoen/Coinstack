<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<asp:Content ContentPlaceholderID="body" runat="server">
    <link href="Content/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="Content/js/bootstrap-multiselect.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // Multiselect data, need to import JSON rates into labels here
            var data = [
                { label: "USD", value: "USD" },
                { label: "BTC", value: "BTC" },
                { label: "LTC", value: "LTC" },
                { label: "EUR", value: "EUR" },
                { label: "GBP", value: "GBP" },
                { label: "CAD", value: "CAD" },
                { label: "AUD", value: "AUD" },
            ];

            rates();

            // Multiselect Options & Settings
            $("#fromSelect").multiselect({
                buttonClass: 'btn btn-lg',
                enableCaseInsensitiveFiltering: true
            });
            //$("#fromSelect").multiselect('dataprovider', data);
            //$("#fromSelect").multiselect('select', 'BTC')

            $("#toSelect").multiselect({
                buttonClass: 'btn btn-lg',
                enableCaseInsensitiveFiltering: true
            });
            $("#toSelect").multiselect('dataprovider', data);
            $("#toSelect").multiselect('select', 'USD')
            
            fx.settings.to = "USD";
            // Update exchange rates every 10 seconds
            setInterval("rates()", 10000);
            
        });

        function fromConvert(fromVal) {
            temp = fx(fromVal).from('USD').to('LTC');
            retVal = fx(temp).from('USD').to(fx.settings.to);

            return retVal;
        }

        function toConvert(toVal) {
            temp = fx(toVal).from(fx.settings.to).to('USD');
            retVal = fx(temp).from('USD').to('LTC');

            return retVal;
        }

        function rates() {
            $.ajax({
                type: "GET",
                url: "Money.aspx",
                data: "json",
                success: function (data) {
                    if (typeof fx !== "undefined" && fx.rates) {
                        fx.rates = data.rates;
                        fx.base = data.base;
                        fx.settings.from = "LTC";
                        fx.settings.middle = "USD";
                        
                        if ($('#inputTo').is(':focus')) {
                            fx.settings.from = "LTC";
                            fx.settings.to = $('#toContainer button').attr('title');

                            $('#inputFrom').val(fx.convert($('#inputTo').val())); //.from(fx.settings.from).to(fx.settings.to));
                        } else if ($('#inputFrom').is(':focus')) {
                            fx.settings.from = $('#toContainer button').attr('title');
                            fx.settings.to = "LTC";

                            $('#inputTo').val(fx.convert(($('#inputFrom').val()))) //.from(fx.settings.from).to(fx.settings.to));
                        } else if ($('#inputTo').val() == '') {
                            $('#inputTo').val(fx($('#inputFrom').val()).from(fx.settings.to).to(fx.settings.from));
                        }
                         
                    } else {
                        // If not, apply to fxSetup global:
                        var fxSetup = {
                            rates: data.rates,
                            base: data.base
                        }
                    }
                }
            });
        }
    </script>
    <div class="jumbotron alert-dismissable">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
        <div class="container">
            <h1>Welcome to Coinstack</h1>
            <p>
                Coinstack is an all-in-one Cryptocurrency ticker, tracker, and converter.
                Get all of the Cryptocurrency information you need right at your fingertips, 
                when and where you want it. Manage all of your tickers and tools in one place,
                and get custom notifications on the fly so you can do more trading and less watching.
            </p>
            <p>
                <a class="btn btn-primary btn-lg" href="#about" role="button">Learn more »</a>
            </p>
        </div>
    </div>
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">Litecoin Converter</h3>
            </div>
            <div class="panel-body">
                <div class="col-sm-offset-2 col-sm-8">
                        <div class="input-group input-group-lg">
                            <span class="input-group-addon">Ł</span>
                            <input type="text" id="inputFrom" class="form-control" value="1" style="text-align: center;" />
                            <span class="input-group-addon">=</span>
                            <input type="text" id="inputTo" class="form-control" style="text-align: center;" />
                        <div id="toContainer" class="input-group-btn">
                            <ul id="toSelect" class="multiselect-container dropdown-menu">
                                
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $('#inputFrom').keyup(function () {
            fx.settings.from = $('#toContainer button').attr('title');
            fx.settings.to = "LTC";

            $('#inputTo').val(fx.convert(($('#inputFrom').val()))); //.from(fx.settings.from).to(fx.settings.to));
        });
        $("#inputTo").keyup(function () {
            fx.settings.to = $('#toContainer button').attr('title');
            fx.settings.from = "LTC";

            $('#inputFrom').val(fx.convert(($('#inputTo').val()))); //.from(fx.settings.from).to(fx.settings.to));
        });
        $('#toContainer').change(function () {
            fx.settings.to = $('#toContainer button').attr('title');
            fx.settings.from = "LTC";

            //$('#inputTo').val(fx($('#inputFrom').val()).from(fx.settings.from).to(fx.settings.to));
            $('#inputTo').val(fromConvert($('#inputFrom').val()));
        });
    </script>
    
</asp:Content>
