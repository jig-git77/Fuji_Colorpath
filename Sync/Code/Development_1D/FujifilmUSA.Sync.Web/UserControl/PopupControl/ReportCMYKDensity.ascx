<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportCMYKDensity.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.PopupControl.ReportCMYKDensity" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<script src="JQuery/jquery-1.9.1.min.js"></script>
<link href="Content/kendo/2018.1.221/kendo.common.min.css" rel="stylesheet" />
<link href="Content/kendo/2018.1.221/kendo.default.min.css" rel="stylesheet" />
<script src="Scripts/kendo/2018.1.221/kendo.all.min.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">

    var jsonCyanDensity = [];
    var jsonMagentaDensity = [];
    var jsonYellowDensity = [];
    var jsonBlackDensity = [];
    //********** kendo objects *********//
    var drawing = kendo.drawing;
    var geometry = kendo.geometry;
    //********** Gradient kendo objects *********//

    var gradientGray = new drawing.LinearGradient({
        start: [0, 0], // Bottom left
        end: [0, 1],   // Top left
        stops: [{
            offset: 0,
            color: "#000",
            opacity: 0.2
        }, {
            offset: 1,
            color: "#000",
            opacity: 0
        }]
    });

    $(document).ready(function () {

        var apiParameters = {
            OptimizationRunId: document.getElementById('<%= txt_OptimizeRunID.ClientID %>').value
        };

        $.ajax({
            url: parent.SERVICE_BASE_URL + 'api/Chart/GetColorDensityChart',
            data: apiParameters,
            type: 'GET',
            dataType: 'json',            
            success: function (response) {
                if (response.data != null) {
                    jsonCyanDensity = response.data.CMYKDensity.CyanList;
                    jsonMagentaDensity = response.data.CMYKDensity.MagentaList;
                    jsonYellowDensity = response.data.CMYKDensity.YellowList;
                    jsonBlackDensity = response.data.CMYKDensity.BlackList;
                }
                BindCyanDensity();
                BindMagentaDensity();
                BindYellowDensity()
                BindBlackDensity();
            },
            error: function (xhr, errorType, exception) {
                //alert(exception);
                kendo.ui.progress($chartLoader, false);
            }
        });
    });

    function BindCyanDensity() {

        var cyanList = [];
        var seriesCyan = [];
        var seriesCyanLimit = [];
        var minΔECyan = 0;
        var maxΔECyan = 0;

        var DeltaEValues = $.map(jsonCyanDensity, function (element) { return element.ΔE; });
        maxΔECyan = Math.max.apply(Math, DeltaEValues);
        minΔECyan = Math.min.apply(Math, DeltaEValues);

        var kendoTargetObj = {};
        kendoTargetObj.data = [];
        kendoTargetObj.color = "Red";
        kendoTargetObj.markers = { background: "Red" };
        kendoTargetObj.highlight = {
            visible: false
        }


        $.each(jsonCyanDensity, function (index, obj) {
            var markerSize = 5;
            if (obj.ΔE == minΔECyan) {
                markerSize = 10;
            }

            var element = {
                xValue: obj.Density.toFixed(2),
                yValue: obj.ΔE.toFixed(2),
                color: 'blue',
                markerSize: markerSize,
            }
            seriesCyan.push(element);

            var limitArr1 = [1, obj.ΔElimit];
            kendoTargetObj.data.push(limitArr1);
            var limitArr2 = [2, obj.ΔElimit];
            kendoTargetObj.data.push(limitArr2);
        });

       

        $("#divDensityCyan").kendoChart({
            renderAs: "canvas",
            seriesDefaults: {
                type: "scatterLine",
                style: "smooth"
            },
            //title: {
            //    text: "Cyan Density"
            //},
            dataSource: seriesCyan,
            series: [
                {
                    xField: "xValue",
                    yField: "yValue",
                    color: "blue",
                    markers: { background: function (e) { return e.dataItem.color; }, size: function (e) { return e.dataItem.markerSize; } },

                    tooltip: {
                        visible: true,
                        template: '<table><tr><td><b>Density : </b>#= dataItem.xValue #</td></tr><tr><td><b>ΔE :</b>#= dataItem.yValue #</td></tr></table>'
                    }
                },
                kendoTargetObj
            ],
            xAxis: {
                name: "xAxis",
                min: 1,
                max: 2,
                majorUnit: 0.1,
                labels: {
                    format: "{0}"
                },
            },
            yAxis: {
                name: "yAxis",                
                min: 0,
                max: 10,
                majorUnit: 1,
                labels: {
                    format: "{0}"
                },
            },
            chartArea: {
                width: 320,
                height: 320
            },
            plotArea: {
                background: "white"
            },
            render: renderChartWithBackgroundByAxis
        });
    }

    function BindMagentaDensity() {

        var seriesMagenta = [];
        var seriesMagentaLimit = [];
        var minΔEMagenta = 0;
        var maxΔEMagenta = 0;

        var DeltaEValues = $.map(jsonMagentaDensity, function (element) { return element.ΔE; });
        maxΔEMagenta = Math.max.apply(Math, DeltaEValues);
        minΔEMagenta = Math.min.apply(Math, DeltaEValues);

        var densityLimit = {};
        densityLimit.data = [];
        densityLimit.color = "Red";
        densityLimit.markers = { background: "Red" };
        densityLimit.highlight = {
            visible: false
        }
        $.each(jsonMagentaDensity, function (index, obj) {
            var markerSize = 5;
            if (obj.ΔE == minΔEMagenta) {
                markerSize = 10;
            }

            var element = {
                xValue: obj.Density.toFixed(2),
                yValue: obj.ΔE.toFixed(2),
                color: 'blue',
                markerSize: markerSize,
            }
            seriesMagenta.push(element);

            var limitArr1 = [1, obj.ΔElimit];
            densityLimit.data.push(limitArr1);
            var limitArr2 = [2, obj.ΔElimit];
            densityLimit.data.push(limitArr2);
        });



        $("#divMagentaCyan").kendoChart({
            renderAs: "canvas",
            seriesDefaults: {
                type: "scatterLine",
                style: "smooth"
            },
            //title: {
            //    text: "Magenta Density"
            //},
            dataSource: seriesMagenta,
            series: [
                {
                    xField: "xValue",
                    yField: "yValue",
                    color: "blue",
                    markers: { background: function (e) { return e.dataItem.color; }, size: function (e) { return e.dataItem.markerSize; } },
                    tooltip: {
                        visible: true,
                        template: '<table><tr><td><b>Density : </b>#= dataItem.xValue #</td></tr><tr><td><b>ΔE :</b>#= dataItem.yValue #</td></tr></table>'
                    }
                },
                densityLimit
            ],
            xAxis: {
                name: "xAxis",
                min: 1,
                max: 2,
                majorUnit: 0.1,
                labels: {
                    format: "{0}"
                },
            },
            yAxis: {
                name: "yAxis",
                min: 0,
                max: 10,
                majorUnit: 1,
                labels: {
                    format: "{0}"
                },
            },
            chartArea: {
                width: 320,
                height: 320
            },
            plotArea: {
                background: "white"
            },
            render: renderChartWithBackgroundByAxis
        });
    }

    function BindYellowDensity() {

        var seriesYellow = [];
        var seriesYellowLimit = [];
        var minΔEYellow = 0;
        var maxΔEYellow = 0;

        var DeltaEValues = $.map(jsonYellowDensity, function (element) { return element.ΔE; });
        maxΔEYellow = Math.max.apply(Math, DeltaEValues);
        minΔEYellow = Math.min.apply(Math, DeltaEValues);

        var densityLimit = {};
        densityLimit.data = [];
        densityLimit.color = "Red";
        densityLimit.markers = { background: "Red" };
        densityLimit.highlight = {
            visible: false
        }

        $.each(jsonYellowDensity, function (index, obj) {
            var markerSize = 5;
            if (obj.ΔE == minΔEYellow) {
                markerSize = 10;
            }

            var element = {
                xValue: obj.Density.toFixed(2),
                yValue: obj.ΔE.toFixed(2),
                color: 'blue',
                markerSize: markerSize,
            }
            seriesYellow.push(element);

            var limitArr1 = [1, obj.ΔElimit];
            densityLimit.data.push(limitArr1);
            var limitArr2 = [2, obj.ΔElimit];
            densityLimit.data.push(limitArr2);
        });



        $("#divYellowCyan").kendoChart({
            renderAs: "canvas",
            seriesDefaults: {
                type: "scatterLine",
                style: "smooth"
            },
            //title: {
            //    text: "Yellow Density"
            //},
            dataSource: seriesYellow,
            series: [
                {
                    xField: "xValue",
                    yField: "yValue",
                    color: "blue",
                    markers: { background: function (e) { return e.dataItem.color; }, size: function (e) { return e.dataItem.markerSize; } },
                    tooltip: {
                        visible: true,
                        template: '<table><tr><td><b>Density : </b>#= dataItem.xValue #</td></tr><tr><td><b>ΔE :</b>#= dataItem.yValue #</td></tr></table>'
                    }
                },
                densityLimit
            ],
            xAxis: {
                name: "xAxis",
                min: 1,
                max: 2,
                majorUnit: 0.1,
                labels: {
                    format: "{0}"
                },
            },
            yAxis: {
                name: "yAxis",
                min: 0,
                max: 10,
                majorUnit: 1,
                labels: {
                    format: "{0}"
                },
            },
            chartArea: {
                width: 320,
                height: 320
            },
            plotArea: {
                background: "white"
            },
            render: renderChartWithBackgroundByAxis
        });
    }

    function BindBlackDensity() {

        var seriesBlack = [];
        var seriesBlackLimit = [];
        var minΔEBlack = 0;
        var maxΔEBlack = 0;

        var DeltaEValues = $.map(jsonBlackDensity, function (element) { return element.ΔE; });
        maxΔEBlack = Math.max.apply(Math, DeltaEValues);
        minΔEBlack = Math.min.apply(Math, DeltaEValues);

        var densityLimit = {};
        densityLimit.data = [];
        densityLimit.color = "Red";
        densityLimit.markers = { background: "Red" };
        densityLimit.highlight = {
            visible: false
        }

        $.each(jsonBlackDensity, function (index, obj) {
            var markerSize = 5;
            if (obj.ΔE == minΔEBlack) {
                markerSize = 10;
            }

            var element = {
                xValue: obj.Density.toFixed(2),
                yValue: obj.ΔE.toFixed(2),
                color: 'blue',
                markerSize: markerSize,
            }
            seriesBlack.push(element);

            var limitArr1 = [1, obj.ΔElimit];
            densityLimit.data.push(limitArr1);
            var limitArr2 = [2, obj.ΔElimit];
            densityLimit.data.push(limitArr2);
        });

        $("#divBlackCyan").kendoChart({
            renderAs: "canvas",
            seriesDefaults: {
                type: "scatterLine",
                style: "smooth"
            },
            //title: {
            //    text: "Black Density"
            //},
            dataSource: seriesBlack,
            series: [
                {
                    xField: "xValue",
                    yField: "yValue",
                    color: "blue",
                    markers: { background: function (e) { return e.dataItem.color; }, size: function (e) { return e.dataItem.markerSize; } },
                    tooltip: {
                        visible: true,
                        template: '<table><tr><td><b>Density : </b>#= dataItem.xValue #</td></tr><tr><td><b>ΔE :</b>#= dataItem.yValue #</td></tr></table>'
                    }
                },
                densityLimit
            ],
            xAxis: {
                name: "xAxis",
                min: 1,
                max: 2,
                majorUnit: 0.1,
                labels: {
                    format: "{0}"
                },
            },
            yAxis: {
                name: "yAxis",
                min: 0,
                max: 10,
                majorUnit: 1,
                labels: {
                    format: "{0}"
                },
            },
            chartArea: {
                width: 320,
                height: 320
            },
            plotArea: {
                background: "white"
            },
            render: renderChartWithBackgroundByAxis
        });
    }

    function renderChartWithBackgroundByAxis(e) {
        var xSlot = {};
        var ySlot = {};
        var path = {};
        var chart = e.sender;
        var xAxis = chart.getAxis("xAxis");
        var yAxis = chart.getAxis("yAxis");
        var xAxisArray = xAxis._axis.children.map(r => parseFloat(r.content));
        var yAxisArray = yAxis._axis.children.map(r => parseFloat(r.content));


       xSlot = xAxis.slot(Math.abs(Math.min.apply(Math, xAxisArray)), Math.abs(Math.max.apply(Math, xAxisArray)));
            ySlot = yAxis.slot(Math.abs(Math.min.apply(Math, yAxisArray)), Math.abs(Math.max.apply(Math, yAxisArray)));
       


        //set the rectangle area on chart
        var rect = new geometry.Rect([
            xSlot.origin.x, ySlot.origin.y
        ], [
                xSlot.width(), ySlot.height()
            ]);


        
            path = drawing.Path.fromRect(rect, {
                stroke: null,
                fill: gradientGray
            });

        chart.surface.draw(path);
    }

    function Close() {
        var oWindow = GetMeasurementRadWindow();
        GetMeasurementRadWindow().close();
        return false;
    }

    function GetMeasurementRadWindow() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow;
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
        return oWindow;
    }


</script>
<script src="JS/ChartTooltip.js" type="text/javascript"></script>

<style type="text/css">
   
    .textwidth40
    {
        width: 40px;
    }
    .textwidth30
    {
        width: 30px;
    }
</style>

<div style="padding-left:5px;">


     <table cellpadding="0" cellspacing="10" width="100%">
        <tr>
            <td>
                <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
            </td>
        </tr>
      <%-- <tr>
        <td style="width: 48%;" valign="top">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                <tr>
                    <td class="green-title">
                        &nbsp; <asp:Localize Text="Cyan" runat="server" 
                            ID="locCMYKDensityCyan" meta:resourcekey="locCMYKDensityCyanResource1"></asp:Localize>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="padding: 2px">
                        <asp:Chart ID="Chart_CMYKDensity_C" runat="server" Width="320px"  Height="320px" BackGradientType="Center" 
                            BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false" ImageType="Jpeg" ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page1_DeltaChChart">
                            <Legends>
                                <asp:Legend Name="Default" Enabled="False">
                                </asp:Legend>
                            </Legends>
                            <BorderSkin SkinStyle="None"></BorderSkin>                                               
                            <Series>
                                <asp:Series Name="Series1" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Blue" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                         
                                <asp:Series Name="Series1Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>  
                                <asp:Series Name="Series2" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Blue" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                         
                                <asp:Series Name="Series2Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>  
                                <asp:Series  Name="Series3" MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="false" Color="Red" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                BorderWidth="2">
                                </asp:Series>  
                                <asp:Series Name="Series3Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                        Color="Red" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                            BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                       
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                    BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                    <Position Height="99" Width="94" X="0" Y="2" />
                                    <AxisY IsLabelAutoFit="False" Title="ΔE" Interval="1" Minimum="0" Maximum="10">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                    </AxisY>
                                    <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="0.1" Title="Density" Minimum="1" Maximum="2">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 48%;" valign="top">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                <tr>
                    <td class="green-title">
                        &nbsp; <asp:Localize Text="Magenta" runat="server" 
                            ID="Localize1" meta:resourcekey="locCMYKDensityCyanResource1"></asp:Localize>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="padding: 2px">
                         <asp:Chart ID="Chart_CMYKDensity_M" runat="server" Width="320px"  Height="320px" BackGradientType="Center" 
                            BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false" ImageType="Jpeg" ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page1_DeltaChChart">
                            <Legends>
                                <asp:Legend Name="Default" Enabled="False">
                                </asp:Legend>
                            </Legends>
                            <BorderSkin SkinStyle="None"></BorderSkin>                                               
                            <Series>
                                <asp:Series Name="Series1" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Blue" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                         
                                <asp:Series Name="Series1Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>  
                                <asp:Series Name="Series2" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Blue" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                         
                                <asp:Series Name="Series2Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>  
                                <asp:Series  Name="Series3" MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="false" Color="Red" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                BorderWidth="2">
                                </asp:Series>  
                                <asp:Series Name="Series3Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                        Color="Red" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                            BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                       
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                    BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                    <Position Height="99" Width="94" X="0" Y="2" />
                                    <AxisY IsLabelAutoFit="False" Title="ΔE" Interval="1" Minimum="0" Maximum="10">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                    </AxisY>
                                    <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="0.1" Title="Density" Minimum="1" Maximum="2">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
            </table>
        </td>
       </tr>
       <tr>
        <td style="width: 48%;" valign="top">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                <tr>
                    <td class="green-title">
                        &nbsp; <asp:Localize Text="Yellow" runat="server" 
                            ID="Localize2" meta:resourcekey="locCMYKDensityCyanResource1"></asp:Localize>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="padding: 2px">
                         <asp:Chart ID="Chart_CMYKDensity_Y" runat="server" Width="320px"  Height="320px" BackGradientType="Center" 
                            BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false" ImageType="Jpeg" ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page1_DeltaChChart">
                            <Legends>
                                <asp:Legend Name="Default" Enabled="False">
                                </asp:Legend>
                            </Legends>
                            <BorderSkin SkinStyle="None"></BorderSkin>                                               
                            <Series>
                                <asp:Series Name="Series1" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Blue" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                         
                                <asp:Series Name="Series1Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>  
                                <asp:Series Name="Series2" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Blue" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                         
                                <asp:Series Name="Series2Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>  
                                <asp:Series  Name="Series3" MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="false" Color="Red" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                BorderWidth="2">
                                </asp:Series>  
                                <asp:Series Name="Series3Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                        Color="Red" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                            BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                       
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                    BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                    <Position Height="99" Width="94" X="0" Y="2" />
                                    <AxisY IsLabelAutoFit="False" Title="ΔE" Interval="1" Minimum="0" Maximum="10">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                    </AxisY>
                                    <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="0.1" Title="Density" Minimum="1" Maximum="2">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 48%;" valign="top">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                <tr>
                    <td class="green-title">
                        &nbsp; <asp:Localize Text="Black" runat="server" 
                            ID="Localize3" meta:resourcekey="locCMYKDensityCyanResource1"></asp:Localize>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="padding: 2px">
                         <asp:Chart ID="Chart_CMYKDensity_K" runat="server" Width="320px"  Height="320px" BackGradientType="Center" 
                            BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false" ImageType="Jpeg" ImageLocation="~/UploadFiles/temp/SingleMeasurement_Page1_DeltaChChart">
                            <Legends>
                                <asp:Legend Name="Default" Enabled="False">
                                </asp:Legend>
                            </Legends>
                            <BorderSkin SkinStyle="None"></BorderSkin>                                               
                            <Series>
                                <asp:Series Name="Series1" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Blue" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                         
                                <asp:Series Name="Series1Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>  
                                <asp:Series Name="Series2" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Blue" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                         
                                <asp:Series Name="Series2Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                    MarkerStyle="Circle" MarkerSize="15" BackSecondaryColor="162, 164, 255">
                                </asp:Series>  
                                <asp:Series  Name="Series3" MarkerSize="12" BackSecondaryColor="162, 164, 255" IsVisibleInLegend="false" Color="Red" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                BorderWidth="2">
                                </asp:Series>  
                                <asp:Series Name="Series3Point" IsValueShownAsLabel="false" LabelFormat="0" IsVisibleInLegend="false"
                                        Color="Red" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                            BackSecondaryColor="162, 164, 255">
                                </asp:Series>                                                       
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="Default" BorderColor="26, 59, 105" BackSecondaryColor="White"
                                    BackColor="Gray" BackGradientStyle="TopBottom" BorderDashStyle="Solid" ShadowOffset="1">
                                    <Position Height="99" Width="94" X="0" Y="2" />
                                    <AxisY IsLabelAutoFit="False" Title="ΔE" Interval="1" Minimum="0" Maximum="10">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                    </AxisY>
                                    <AxisX IsLabelAutoFit="False" IsMarginVisible="False" Interval="0.1" Title="Density" Minimum="1" Maximum="2">
                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                        <LabelStyle Angle="0" Font="Verdana, 6pt" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
            </table>
        </td>
       </tr>--%>
         <tr>
        <td style="width: 48%;" valign="top">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                <tr>
                    <td class="green-title">
                        &nbsp; <asp:Localize Text="Cyan" runat="server" 
                            ID="Localize4" meta:resourcekey="locCMYKDensityCyanResource1"></asp:Localize>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="padding: 2px">                        
                        <div id="divDensityCyan"></div>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 48%;" valign="top">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                <tr>
                    <td class="green-title">
                        &nbsp; <asp:Localize Text="Magenta" runat="server" 
                            ID="Localize5" meta:resourcekey="locCMYKDensityCyanResource1"></asp:Localize>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="padding: 2px">
                        <div id="divMagentaCyan"></div>                         
                    </td>
                </tr>
            </table>
        </td>
       </tr>
         <tr>
        <td style="width: 48%;" valign="top">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                <tr>
                    <td class="green-title">
                        &nbsp; <asp:Localize Text="Yellow" runat="server" 
                            ID="Localize6" meta:resourcekey="locCMYKDensityCyanResource1"></asp:Localize>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="padding: 2px">
                         <div id="divYellowCyan"></div>                         
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 48%;" valign="top">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                <tr>
                    <td class="green-title">
                        &nbsp; <asp:Localize Text="Black" runat="server" 
                            ID="Localize7" meta:resourcekey="locCMYKDensityCyanResource1"></asp:Localize>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="padding: 2px">
                         <div id="divBlackCyan"></div>                         
                    </td>
                </tr>
            </table>
        </td>
       </tr>
        <tr>
            <td></td>
            <td>
                <div class="buttonright" style="padding-left: 3px">
                    <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript: Close();"
                        CssClass="gray_bt">
                                        <span> <asp:Localize runat="server" ID="locCancel" 
                        Text="Cancel" meta:resourcekey="locCancelResource1" ></asp:Localize> 
                                
 
                                </span>
                    </asp:LinkButton>
                </div>
             
                <div class="clear">
                </div>
            </td>
        </tr>
    </table>

</div>
<div class="FadingTooltip" id="FADINGTOOLTIP" style="z-index: 999; visibility: hidden;
    position: absolute">
</div>
<div style="visibility:hidden;">
    <asp:TextBox ID="txt_ProjectID" runat="server"></asp:TextBox>
    <asp:TextBox ID="txt_OptimizeRunID" runat="server"></asp:TextBox>
    <asp:TextBox ID="txt_MeasurementID" runat="server"></asp:TextBox>
</div>