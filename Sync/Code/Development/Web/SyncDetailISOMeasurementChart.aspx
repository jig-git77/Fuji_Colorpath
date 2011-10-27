<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SyncDetailISOMeasurementChart.aspx.cs"
    Inherits="SyncDetailISOMeasurementChart" %>

<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register Src="Common/ProgressBar.ascx" TagName="ProgressBar" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>FUJIFILM Graphic Systems U.S.A., Inc.</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <meta name="keywords" content="FUJIFILM Graphic Systems U.S.A., Inc." />
    <meta name="description" content="FUJIFILM Graphic Systems U.S.A., Inc." />
    <link href="CSS/Default.css" rel="stylesheet" type="text/css" />

    <script src="JS/ChartTooltip.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Literal ID="LtrlCSS" runat="server"></asp:Literal>
    <asp:Literal ID="LtrlJS" runat="server"></asp:Literal>
    <table id="Table1" cellpadding="0" cellspacing="0" border="0" style="width: 100%"
        runat="server" class="popup-table">
        <tr>
            <td>
                <asp:Label ID="LblError" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <ajaxtoolkit:TabContainer ID="TabContainer1" runat="server" Width="100%" Style="padding: 0px">
                    <ajaxtoolkit:TabPanel ID="TabPanInkPaper" Width="100%" runat="server" HeaderText="Ink Paper"
                        Style="padding: 0px; width: 100%" EnableViewState="false">
                        <ContentTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 50%;" valign="top" class="CHARTTABLE">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td colspan="2" class="green-title">
                                                    &nbsp; &#916;E Primary Patches
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 40%;" valign="top">
                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tr style="height: 16px">
                                                            <td valign="middle" style="width: 25px" class="SyncBottomBorder">
                                                                &nbsp;
                                                            </td>
                                                            <td valign="middle" style="width: 75%" class="SyncBottomBorder">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="height: 15px;">
                                                            </td>
                                                        </tr>
                                                        <asp:Literal ID="LitDeltaEPatches" runat="server"></asp:Literal>
                                                    </table>
                                                </td>
                                                <td style="width: 60%;" id="TDEPrimary" runat="server">
                                                    <DCWC:Chart ID="ChartISOSummary_DeltaE" runat="server" Width="210px" Height="315px"
                                                        ImageUrl="~/UploadFiles/temp/ISOSummary_DeltaE" ImageType="Jpeg" EnableViewState="false">
                                                        <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                                            SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                                        <UI>
                                                            <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                    FrameBackColor="SteelBlue"></BorderSkin>
                                                            </Toolbar>
                                                        </UI>
                                                        <Series>
                                                            <DCWC:Series Name="Series0" BackGradientType="DiagonalRight" BorderColor="26, 59, 105"
                                                                BorderWidth="0" Color="#D4E8FA" ShadowOffset="70" Font="Microsoft Sans Serif, 7pt, style=Underline"
                                                                Label="" CustomAttributes="LabelStyle=Top,DrawingStyle=Cylinder" ShowInLegend="true">
                                                            </DCWC:Series>
                                                            <DCWC:Series Name="Series1" BorderWidth="0" BorderColor="Gray" Color="Gray" CustomAttributes="DrawingStyle=Cylinder"
                                                                ShowInLegend="false">
                                                            </DCWC:Series>
                                                            <DCWC:Series Name="Series2" BorderWidth="0" Color="Transparent" CustomAttributes="DrawingStyle=Cylinder"
                                                                ShowInLegend="false" MarkerImage="~/Images/Chart-Red-Image-Vertical.png">
                                                            </DCWC:Series>
                                                        </Series>
                                                        <ChartAreas>
                                                            <DCWC:ChartArea Name="ChartArea1">
                                                                <Area3DStyle YAngle="0" XAngle="0" Enable3D="true" RightAngleAxes="False" WallWidth="0"
                                                                    Clustered="True"></Area3DStyle>
                                                                <AxisX Interval="Auto" Enabled="true" LabelsAutoFit="False" Reverse="true">
                                                                    <MajorGrid Enabled="false" LineColor="Beige"></MajorGrid>
                                                                    <MajorTickMark Enabled="false" />
                                                                    <LabelStyle Font="Verdana, 7pt" />
                                                                </AxisX>
                                                                <AxisY Enabled="True" LabelsAutoFit="False">
                                                                    <MajorGrid LineColor="Beige"></MajorGrid>
                                                                    <LabelStyle Format="###" Font="Verdana, 7pt" FontColor="Black" />
                                                                </AxisY>
                                                            </DCWC:ChartArea>
                                                        </ChartAreas>
                                                    </DCWC:Chart>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 50%;" valign="top">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                                        <tr>
                                                            <td class="green-title">
                                                                &nbsp; Density
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <DCWC:Chart ID="CHRT_SummaryISODensity" ImageType="Jpeg" runat="server" Width="380px"
                                                                    Height="175px" ImageUrl="~/UploadFiles/temp/CHRT_SummaryISODensity" BackGradientType="Center"
                                                                    Palette="Kindergarten" BackGradientEndColor="#FFFFFF" BorderLineColor="#808080"
                                                                    EnableViewState="false">
                                                                    <Series>
                                                                        <DCWC:Series Name="Series0" BackGradientType="DiagonalRight" BorderColor="26, 59, 105"
                                                                            BorderWidth="0" Color="Transparent" ShadowOffset="70" Font="Microsoft Sans Serif, 7pt, style=Underline"
                                                                            Label="" CustomAttributes="LabelStyle=Top,DrawingStyle=Cylinder" ShowInLegend="true">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series Name="Series1" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                                                            CustomAttributes="DrawingStyle=Cylinder" ShowInLegend="false">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series Name="Series2" BorderWidth="0" Color="Transparent" CustomAttributes="DrawingStyle=Cylinder"
                                                                            ShowInLegend="false" MarkerImage="~/Images/green.png">
                                                                        </DCWC:Series>
                                                                    </Series>
                                                                    <BorderSkin SkinStyle="None"></BorderSkin>
                                                                    <UI>
                                                                        <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                            <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                                FrameBackColor="SteelBlue"></BorderSkin>
                                                                        </Toolbar>
                                                                    </UI>
                                                                    <ChartAreas>
                                                                        <DCWC:ChartArea Name="ChartArea1" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                                                            BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                            <Area3DStyle YAngle="10" Light="Realistic" XAngle="10" Enable3D="true" RightAngleAxes="False"
                                                                                WallWidth="0" Clustered="true"></Area3DStyle>
                                                                            <%--Enable3D="true"--%>
                                                                            <Position Height="99" Width="94" X="0" Y="2" />
                                                                            <AxisY Enabled="True">
                                                                                <MajorGrid LineColor="Beige" />
                                                                            </AxisY>
                                                                            <AxisX Enabled="True" Interval="Auto">
                                                                                <MajorGrid LineColor="Beige" />
                                                                            </AxisX>
                                                                        </DCWC:ChartArea>
                                                                    </ChartAreas>
                                                                </DCWC:Chart>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                                        <tr>
                                                            <td class="green-title">
                                                                &nbsp; &#916; Paper L* a* b*
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td style="background: #FFFFFF url(images/BGISOPaperGraph1.jpg) no-repeat; width: 276px;
                                                                            height: 169px; padding-left: 34px; padding-bottom: 14px;">
                                                                            <DCWC:Chart ID="CHRT_ISOPaper1" runat="server" BackImageMode="Scaled" BackImage="~/Images/ISOPaperGraph1.jpg"
                                                                                ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CHRT_ISOPaper1" Width="240px" Height="120px"
                                                                                EnableViewState="false">
                                                                                <Legends>
                                                                                    <DCWC:Legend Name="Default" Enabled="False">
                                                                                    </DCWC:Legend>
                                                                                </Legends>
                                                                                <Series>
                                                                                    <DCWC:Series Name="Green" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                                        MarkerSize="8" MarkerBorderWidth="2" MarkerBorderColor="Green" MarkerStyle="Circle"
                                                                                        MarkerColor="Green" ShadowOffset="1">
                                                                                    </DCWC:Series>
                                                                                    <DCWC:Series Name="Paper" ChartType="Polar" MarkerBorderColor="Black" MarkerColor="White"
                                                                                        CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="8" MarkerBorderWidth="2"
                                                                                        MarkerStyle="Circle" ShadowOffset="1">
                                                                                    </DCWC:Series>                                                                                    
                                                                                    <DCWC:Series YValuesPerPoint="2" XValueType="Double" Name="SeriesTop" ChartType="SplineRange"
                                                                                        Color="80, 65, 140, 140" YValueType="Double">
                                                                                    </DCWC:Series>                                                                                    
                                                                                    <DCWC:Series Name="SeriesRed1" Color="Red" ChartType="Line" BorderWidth="1" ShowInLegend="false" >
                                                                                    </DCWC:Series>
                                                                                    <DCWC:Series Name="SeriesRed2" Color="Red" ChartType="Line" BorderWidth="1" ShowInLegend="false" >
                                                                                    </DCWC:Series>
                                                                                    <DCWC:Series Name="SeriesRed3" Color="Red" ChartType="Line" BorderWidth="1" ShowInLegend="false" >
                                                                                    </DCWC:Series>
                                                                                    <DCWC:Series Name="SeriesRed4" Color="Red" ChartType="Line" BorderWidth="1" ShowInLegend="false" >
                                                                                    </DCWC:Series>
                                                                                </Series>
                                                                                <BorderSkin PageColor="AliceBlue" />
                                                                                <Titles>
                                                                                    <DCWC:Title Name="Title1">
                                                                                    </DCWC:Title>
                                                                                </Titles>
                                                                                <ChartAreas>
                                                                                    <DCWC:ChartArea BackColor="Transparent" BorderColor="" Name="Default">
                                                                                        <AxisY LineColor="120, 0, 0, 0" TitleAlignment="Near" Maximum="10" Minimum="-10"
                                                                                            LineStyle="NotSet">
                                                                                            <MajorGrid Enabled="False" />
                                                                                            <MajorTickMark Enabled="false" />
                                                                                            <LabelStyle Enabled="False" />
                                                                                        </AxisY>
                                                                                        <AxisX Margin="False" TitleAlignment="Near" Maximum="20" Minimum="-20" LineStyle="NotSet">
                                                                                            <MajorGrid Enabled="False" />
                                                                                            <MajorTickMark Enabled="false" />
                                                                                            <LabelStyle Enabled="False" />
                                                                                        </AxisX>
                                                                                    </DCWC:ChartArea>
                                                                                </ChartAreas>
                                                                            </DCWC:Chart>
                                                                        </td>
                                                                        <td style="background: #FFFFFF url(images/BGISOPaperGraph2.jpg) no-repeat; width: 104px;
                                                                            height: 169px; padding-left: 28px; padding-bottom: 12px;">
                                                                            <DCWC:Chart ID="CHRT_ISOPaper2" runat="server" BackImageMode="Scaled" BackImage="~/Images/ISOPaperGraph2.jpg"
                                                                                ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CHRT_ISOPaper2" Width="69px" Height="118px"
                                                                                EnableViewState="false">
                                                                                <Legends>
                                                                                    <DCWC:Legend Name="Default" Enabled="False">
                                                                                    </DCWC:Legend>
                                                                                </Legends>
                                                                                <Series>
                                                                                    <DCWC:Series Name="Series1" BorderColor="100, 0 , 0 , 0" ChartArea="Default" BorderWidth="1"
                                                                                        CustomAttributes="DrawingStyle=Cylinder" ShowInLegend="false">
                                                                                    </DCWC:Series>
                                                                                    <DCWC:Series Name="Series2" Color="Green" ChartArea="Default" BorderWidth="2" ShowInLegend="false">
                                                                                    </DCWC:Series>
                                                                                    <DCWC:Series YValuesPerPoint="2" XValueType="Double" Name="SeriesTop" ChartType="SplineRange"
                                                                                        BorderColor="Red" Color="80, 65, 140, 140" YValueType="Double">
                                                                                    </DCWC:Series>                                                                                    
                                                                                </Series>
                                                                                <BorderSkin PageColor="AliceBlue" />
                                                                                <Titles>
                                                                                    <DCWC:Title Name="Title1">
                                                                                    </DCWC:Title>
                                                                                </Titles>
                                                                                <ChartAreas>
                                                                                    <DCWC:ChartArea BackColor="Transparent" BorderColor="" Name="Default">
                                                                                        <AxisY LineColor="120, 0, 0, 0" TitleAlignment="Near" Maximum="10" Minimum="-10"
                                                                                            LineStyle="NotSet">
                                                                                            <MajorGrid Enabled="False" />
                                                                                            <MajorTickMark Enabled="false" />
                                                                                            <LabelStyle Enabled="False" />
                                                                                        </AxisY>
                                                                                        <AxisX Margin="true" TitleAlignment="Near" LineStyle="NotSet">
                                                                                            <MajorGrid Enabled="False" />
                                                                                            <MajorTickMark Enabled="false" />
                                                                                            <LabelStyle Enabled="False" />
                                                                                        </AxisX>
                                                                                        <AxisX2 Interval="Auto" Minimum="0" Maximum="1" LineStyle="NotSet">
                                                                                            <MajorGrid Enabled="False" />
                                                                                            <MajorTickMark Enabled="false" />
                                                                                            <LabelStyle Enabled="False" />
                                                                                        </AxisX2>
                                                                                    </DCWC:ChartArea>
                                                                                </ChartAreas>
                                                                            </DCWC:Chart>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajaxtoolkit:TabPanel>
                    <ajaxtoolkit:TabPanel ID="TabPanTVI" Width="100%" runat="server" HeaderText="TVI">
                        <ContentTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnTVI" runat="server" Text="TVI" OnClientClick="funTVIVisiblity(); return false;" />
                                        &nbsp;<asp:Button ID="btnSpread" OnClientClick="funSpreadVisiblity(); return false;"
                                            runat="server" Text="Spread" />
                                    </td>
                                </tr>
                                <tr id="trTVI">
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="SYNCTABLE">
                                                        <tr>
                                                            <td class="green-title" align="center">
                                                                Cyan
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" align="center">
                                                                <DCWC:Chart ID="CHRT_DotgainISOCyan" runat="server" Width="370px" Height="175px"
                                                                    ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/DotgainISOCyan" BackGradientType="Center"
                                                                    Palette="Kindergarten" BackGradientEndColor="White" BorderLineColor="#808080">
                                                                    <Legends>
                                                                        <DCWC:Legend Name="Default" Enabled="false">
                                                                        </DCWC:Legend>
                                                                    </Legends>
                                                                    <BorderSkin SkinStyle="None"></BorderSkin>
                                                                    <UI>
                                                                        <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                            <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                                FrameBackColor="SteelBlue"></BorderSkin>
                                                                        </Toolbar>
                                                                    </UI>
                                                                    <Series>
                                                                        <DCWC:Series Name="Series0" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                            Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="Black" MarkerStyle="Circle"
                                                                            MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series YValuesPerPoint="2" XValueType="Double" Name="SeriesTop" ChartType="SplineRange"
                                                                            BorderColor="220, 65, 140, 140" Color="80, 65, 140, 140" YValueType="Double">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                                                            Color="Green" ChartType="Spline" MarkerBorderColor="Green" MarkerStyle="Circle"
                                                                            BorderWidth="2" Name="Series1">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series MarkerSize="5" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                                                            Color="Gray" ChartType="Point" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                                            Name="Series2">
                                                                        </DCWC:Series>
                                                                    </Series>
                                                                    <ChartAreas>
                                                                        <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                                                            BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                            <Position Height="99" Width="94" X="0" Y="2" />
                                                                            <AxisY LabelsAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="Auto">
                                                                                <MajorGrid LineColor="Gray"></MajorGrid>
                                                                            </AxisY>
                                                                            <AxisX LabelsAutoFit="False" Margin="False" Interval="25" Title="">
                                                                                <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                <LabelStyle FontAngle="0" />
                                                                            </AxisX>
                                                                        </DCWC:ChartArea>
                                                                    </ChartAreas>
                                                                </DCWC:Chart>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="SYNCTABLE">
                                                        <tr>
                                                            <td class="green-title" align="center">
                                                                Magenta
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <DCWC:Chart ID="CHRT_DotgainISOMeganta" runat="server" Width="370px" Height="175px"
                                                                    ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/DotgainISOMagenta" BackGradientType="Center"
                                                                    Palette="Kindergarten" BackGradientEndColor="White" BorderLineColor="#808080">
                                                                    <Legends>
                                                                        <DCWC:Legend Name="Default" Enabled="false">
                                                                        </DCWC:Legend>
                                                                    </Legends>
                                                                    <BorderSkin SkinStyle="None"></BorderSkin>
                                                                    <UI>
                                                                        <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                            <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                                FrameBackColor="SteelBlue"></BorderSkin>
                                                                        </Toolbar>
                                                                    </UI>
                                                                    <Series>
                                                                        <DCWC:Series Name="Series0" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                            Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="Black" MarkerStyle="Circle"
                                                                            MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series YValuesPerPoint="2" XValueType="Double" Name="SeriesTop" ChartType="SplineRange"
                                                                            BorderColor="220, 65, 140, 140" Color="80, 65, 140, 140" YValueType="Double">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                                                            Color="Green" ChartType="Spline" MarkerBorderColor="Green" MarkerStyle="Circle"
                                                                            BorderWidth="2" Name="Series1">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series MarkerSize="5" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                                                            Color="Gray" ChartType="Point" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                                            Name="Series2">
                                                                        </DCWC:Series>
                                                                    </Series>
                                                                    <ChartAreas>
                                                                        <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                                                            BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                            <Position Height="99" Width="94" X="0" Y="2" />
                                                                            <AxisY LabelsAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="Auto">
                                                                                <MajorGrid LineColor="Gray"></MajorGrid>
                                                                            </AxisY>
                                                                            <AxisX LabelsAutoFit="False" Margin="False" Interval="25" Title="">
                                                                                <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                <LabelStyle FontAngle="0" />
                                                                            </AxisX>
                                                                        </DCWC:ChartArea>
                                                                    </ChartAreas>
                                                                </DCWC:Chart>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="SYNCTABLE">
                                                        <tr>
                                                            <td class="green-title" align="center">
                                                                Yellow
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <DCWC:Chart ID="CHRT_DotgainISOYellow" runat="server" Width="370px" Height="175px"
                                                                    ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/DotgainISOYellow" BackGradientType="Center"
                                                                    Palette="Kindergarten" BackGradientEndColor="White" BorderLineColor="#808080">
                                                                    <Legends>
                                                                        <DCWC:Legend Name="Default" Enabled="false">
                                                                        </DCWC:Legend>
                                                                    </Legends>
                                                                    <BorderSkin SkinStyle="None"></BorderSkin>
                                                                    <UI>
                                                                        <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                            <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                                FrameBackColor="SteelBlue"></BorderSkin>
                                                                        </Toolbar>
                                                                    </UI>
                                                                    <Series>
                                                                        <DCWC:Series Name="Series0" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                            Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="Black" MarkerStyle="Circle"
                                                                            MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series YValuesPerPoint="2" XValueType="Double" Name="SeriesTop" ChartType="SplineRange"
                                                                            BorderColor="220, 65, 140, 140" Color="80, 65, 140, 140" YValueType="Double">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                                                            Color="Green" ChartType="Spline" MarkerBorderColor="Green" MarkerStyle="Circle"
                                                                            BorderWidth="2" Name="Series1">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series MarkerSize="5" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                                                            Color="Gray" ChartType="Point" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                                            Name="Series2">
                                                                        </DCWC:Series>
                                                                    </Series>
                                                                    <ChartAreas>
                                                                        <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                                                            BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                            <Position Height="99" Width="94" X="0" Y="2" />
                                                                            <AxisY LabelsAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="Auto">
                                                                                <MajorGrid LineColor="Gray"></MajorGrid>
                                                                            </AxisY>
                                                                            <AxisX LabelsAutoFit="False" Margin="False" Interval="25" Title="">
                                                                                <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                <LabelStyle FontAngle="0" />
                                                                            </AxisX>
                                                                        </DCWC:ChartArea>
                                                                    </ChartAreas>
                                                                </DCWC:Chart>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="SYNCTABLE">
                                                        <tr>
                                                            <td class="green-title" align="center">
                                                                Black
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <DCWC:Chart ID="CHRT_DotgainISOBlack" runat="server" Width="370px" Height="175px"
                                                                    ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/DotgainISOBlack" BackGradientType="Center"
                                                                    Palette="Kindergarten" BackGradientEndColor="White" BorderLineColor="#808080">
                                                                    <Legends>
                                                                        <DCWC:Legend Name="Default" Enabled="false">
                                                                        </DCWC:Legend>
                                                                    </Legends>
                                                                    <BorderSkin SkinStyle="None"></BorderSkin>
                                                                    <UI>
                                                                        <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                            <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                                FrameBackColor="SteelBlue"></BorderSkin>
                                                                        </Toolbar>
                                                                    </UI>
                                                                    <Series>
                                                                        <DCWC:Series Name="Series0" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                            Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="Black" MarkerStyle="Circle"
                                                                            MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series YValuesPerPoint="2" XValueType="Double" Name="SeriesTop" ChartType="SplineRange"
                                                                            BorderColor="220, 65, 140, 140" Color="80, 65, 140, 140" YValueType="Double">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                                                            Color="Green" ChartType="Spline" MarkerBorderColor="Green" MarkerStyle="Circle"
                                                                            BorderWidth="2" Name="Series1">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series MarkerSize="5" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                                                            Color="Gray" ChartType="Point" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                                            Name="Series2">
                                                                        </DCWC:Series>
                                                                    </Series>
                                                                    <ChartAreas>
                                                                        <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                                                            BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                            <Position Height="99" Width="94" X="0" Y="2" />
                                                                            <AxisY LabelsAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="Auto">
                                                                                <MajorGrid LineColor="Gray"></MajorGrid>
                                                                            </AxisY>
                                                                            <AxisX LabelsAutoFit="False" Margin="False" Interval="25" Title="">
                                                                                <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                <LabelStyle FontAngle="0" />
                                                                            </AxisX>
                                                                        </DCWC:ChartArea>
                                                                    </ChartAreas>
                                                                </DCWC:Chart>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="trSpread" style="display: none">
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td align="right" style="padding-right: 30px">
                                                            <asp:CheckBox ID="ChkC" runat="server" Text="C" Checked="true" AutoPostBack="true"
                                                                OnCheckedChanged="Chk_CheckedChanged" />
                                                            &nbsp;&nbsp;<asp:CheckBox ID="ChkM" runat="server" Text="M" Checked="true" AutoPostBack="true"
                                                                OnCheckedChanged="Chk_CheckedChanged" />&nbsp;<asp:CheckBox ID="ChkY" runat="server"
                                                                    Text="Y" Checked="true" AutoPostBack="true" OnCheckedChanged="Chk_CheckedChanged" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <DCWC:Chart ID="CHRT_SummaryISODotgain" runat="server" Width="750px" Height="310px"
                                                                ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CHRT_SummaryISODotgain" BackGradientType="Center"
                                                                Palette="Kindergarten" BackGradientEndColor="White" BorderLineColor="#808080"
                                                                EnableViewState="false">
                                                                <Series>
                                                                    <DCWC:Series Name="Series1" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                        Color="Cyan" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                                    </DCWC:Series>
                                                                    <DCWC:Series Name="Series1Point" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                        Color="Cyan" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                                    </DCWC:Series>
                                                                    <DCWC:Series Name="Series2" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                        Color="Magenta" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                                    </DCWC:Series>
                                                                    <DCWC:Series Name="Series2Point" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                        Color="Magenta" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                                    </DCWC:Series>
                                                                    <DCWC:Series Name="Series3" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                        Color="Yellow" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                                    </DCWC:Series>
                                                                    <DCWC:Series Name="Series3Point" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                        Color="Yellow" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                                    </DCWC:Series>
                                                                    <DCWC:Series YValuesPerPoint="2" ShowInLegend="false" XValueType="String" Name="SeriesTop"
                                                                        ChartType="SplineRange" BorderColor="220, 65, 140, 140" Color="80, 65, 140, 140" 
                                                                        YValueType="String">
                                                                    </DCWC:Series>
                                                                </Series>
                                                                <BorderSkin SkinStyle="None"></BorderSkin>
                                                                <UI>
                                                                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                            FrameBackColor="SteelBlue"></BorderSkin>
                                                                    </Toolbar>
                                                                </UI>
                                                                <ChartAreas>
                                                                    <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                                                        BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                        <Position Height="99" Width="94" X="0" Y="2" />
                                                                        <AxisY LabelsAutoFit="False" Title="" Interval="Auto">
                                                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                                                        </AxisY>
                                                                        <AxisX LabelsAutoFit="False" Margin="False" Interval="25" Title="">
                                                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                                                        </AxisX>
                                                                    </DCWC:ChartArea>
                                                                </ChartAreas>
                                                            </DCWC:Chart>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left:40px">
                                                            <table class="TDBOX" border="0" cellpadding="5" cellspacing="0" width="90%">
                                                                <tr class="TRTABLEBOX">
                                                                    <td style="width:23%;"></td>
                                                                    <td style="width:26%;">25%</td>
                                                                    <td style="width:25%;">50%</td>
                                                                    <td style="width:26%;">75%</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="TDTABLEBOXLEFT"><b>Max Spread</b></td>
                                                                    <td class="TDTABLEBOX"><asp:Label ID="lblMaxSpread25" runat="server" Text=""></asp:Label></td>
                                                                    <td class="TDTABLEBOX"><asp:Label ID="lblMaxSpread50" runat="server" Text=""></asp:Label></td>
                                                                    <td class="TDTABLEBOX"><asp:Label ID="lblMaxSpread75" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajaxtoolkit:TabPanel>
                    
                    <ajaxtoolkit:TabPanel ID="TabPanLab" Width="100%" runat="server" HeaderText="Lab">
                        <ContentTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 45%;" valign="top" class="CHARTTABLE">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td class="green-title">
                                                    &nbsp; Gamut Plot
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="Table2" runat="server" cellpadding="0" cellspacing="0" border="0" width="100%"
                                                        class="SYNCTABLE">
                                                        <tr>
                                                            <td class="green-title">
                                                                a* - b* Comparison
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" valign="top">
                                                                <DCWC:Chart ID="CHART_ABISOComparison" runat="server" BackImageMode="Scaled" BackImage="~/Images/CPSyncABComparison.jpg"
                                                                    ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CPSyncABISOComparison" Width="280px"
                                                                    Height="250px" EnableViewState="false">
                                                                    <Legends>
                                                                        <DCWC:Legend Name="Default" Enabled="False">
                                                                        </DCWC:Legend>
                                                                    </Legends>
                                                                    <Series>
                                                                        <DCWC:Series Name="Targets" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series Name="Cyan" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series Name="Magenta" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series Name="Yellow" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series Name="Red" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series Name="Green" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series Name="Blue" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series Name="Paper" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                                                        </DCWC:Series>
                                                                        
                                                                       
                                                                    </Series>
                                                                    <BorderSkin PageColor="AliceBlue" />
                                                                    <Titles>
                                                                        <DCWC:Title Name="Title1">
                                                                        </DCWC:Title>
                                                                    </Titles>
                                                                    <ChartAreas>
                                                                        <DCWC:ChartArea BackColor="Transparent" BorderColor="" Name="Default">
                                                                            <AxisY LineColor="120, 0, 0, 0" TitleAlignment="Near" Maximum="100" Minimum="-100"
                                                                                LineStyle="NotSet">
                                                                                <MajorGrid Enabled="False" />
                                                                                <MajorTickMark Enabled="false" />
                                                                                <LabelStyle Enabled="False" />
                                                                            </AxisY>
                                                                            <AxisX Margin="False" TitleAlignment="Near" Maximum="100" Minimum="-100" LineStyle="NotSet">
                                                                                <MajorGrid Enabled="False" />
                                                                                <MajorTickMark Enabled="false" />
                                                                                <LabelStyle Enabled="False" />
                                                                            </AxisX>
                                                                        </DCWC:ChartArea>
                                                                    </ChartAreas>
                                                                </DCWC:Chart>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="green-title">
                                                                L* Comparison
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" valign="top">
                                                                <DCWC:Chart ID="CHART_LISOComparison" runat="server" BackImageMode="Scaled" BackImage="~/Images/CPSyncLComparison.jpg"
                                                                    ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CPSyncISOLComparison" Width="280px"
                                                                    Height="75px" EnableViewState="false">
                                                                    <Legends>
                                                                        <DCWC:Legend Name="Default" Enabled="False">
                                                                        </DCWC:Legend>
                                                                    </Legends>
                                                                    <Series>
                                                                        <DCWC:Series Name="Series0" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                            MarkerSize="12" MarkerStyle="Circle" MarkerColor="Green" Color="Green" ShadowOffset="1">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series Name="Series1" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                            MarkerSize="12" MarkerStyle="Circle" MarkerColor="Green" MarkerBorderWidth="1"
                                                                            Color="Green" ShadowOffset="1">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series Name="Series2" ChartType="FastPoint" CustomAttributes="PolarDrawingStyle=Marker"
                                                                            MarkerSize="12" MarkerStyle="Circle" MarkerColor="Green" Color="Red" ShadowOffset="1">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series Name="Series3" ChartType="FastPoint" CustomAttributes="PolarDrawingStyle=Marker"
                                                                            MarkerSize="12" MarkerStyle="Circle" ShadowOffset="1">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series Name="Series4" ChartType="FastPoint" CustomAttributes="PolarDrawingStyle=Marker"
                                                                            MarkerSize="4" MarkerStyle="Circle" ShadowOffset="1">
                                                                        </DCWC:Series>
                                                                    </Series>
                                                                    <BorderSkin PageColor="AliceBlue" />
                                                                    <Titles>
                                                                        <DCWC:Title Name="Title1">
                                                                        </DCWC:Title>
                                                                    </Titles>
                                                                    <ChartAreas>
                                                                        <DCWC:ChartArea BackColor="Transparent" BorderColor="" Name="Default">
                                                                            <AxisY LineColor="120, 0, 0, 0" TitleAlignment="Near" Maximum="15" Minimum="-15"
                                                                                LineStyle="NotSet">
                                                                                <MajorGrid Enabled="False" />
                                                                                <MajorTickMark Enabled="false" />
                                                                                <LabelStyle Enabled="False" />
                                                                            </AxisY>
                                                                            <AxisX Margin="False" TitleAlignment="Near" Maximum="100" Minimum="-100" LineStyle="NotSet">
                                                                                <MajorGrid Enabled="False" />
                                                                                <MajorTickMark Enabled="false" />
                                                                                <LabelStyle Enabled="False" />
                                                                            </AxisX>
                                                                        </DCWC:ChartArea>
                                                                    </ChartAreas>
                                                                </DCWC:Chart>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 55%;" valign="top">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                                        <tr>
                                                            <td class="green-title">
                                                                &nbsp; Gray Balance L* a* b*
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <DCWC:Chart ID="Chart_LISO" runat="server" Width="480px" Height="90px" ImageType="Jpeg"
                                                                                ImageUrl="~/UploadFiles/temp/ChartLISO" BackGradientType="Center" Palette="Kindergarten"
                                                                                BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false">
                                                                                <Titles>
                                                                                    <DCWC:Title Color="Black" Text="L*">
                                                                                        <Position X="90" Y="15" />
                                                                                    </DCWC:Title>
                                                                                </Titles>
                                                                                <Legends>
                                                                                    <DCWC:Legend Name="Default" Enabled="false">
                                                                                    </DCWC:Legend>
                                                                                </Legends>
                                                                                <BorderSkin SkinStyle="None"></BorderSkin>
                                                                                <UI>
                                                                                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                                            FrameBackColor="SteelBlue"></BorderSkin>
                                                                                    </Toolbar>
                                                                                </UI>
                                                                                <Series>
                                                                                    <DCWC:Series Name="Series1" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                                        Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                                                    </DCWC:Series>
                                                                                    <DCWC:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                                                                        Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                                                        BorderWidth="2" Name="Series2">
                                                                                    </DCWC:Series>
                                                                                </Series>
                                                                                <ChartAreas>
                                                                                    <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="Gray"
                                                                                        BackColor="White" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                                        <Position Height="99" Width="94" X="0" Y="0" />
                                                                                        <AxisY LabelsAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="25"
                                                                                            Minimum="0" Maximum="100">
                                                                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                            <LabelStyle FontAngle="0" Font="Verdana, 6pt" />
                                                                                        </AxisY>
                                                                                        <AxisX LabelsAutoFit="False" Margin="False" Interval="25" Title="" Minimum="0" Maximum="100">
                                                                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                            <LabelStyle FontAngle="0" Font="Verdana, 6pt" FontColor="Transparent" />
                                                                                        </AxisX>
                                                                                    </DCWC:ChartArea>
                                                                                </ChartAreas>
                                                                            </DCWC:Chart>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <DCWC:Chart ID="Chart_aISO" runat="server" Width="480px" Height="90px" ImageType="Jpeg"
                                                                                ImageUrl="~/UploadFiles/temp/ChartAISO" BackGradientType="Center" Palette="Kindergarten"
                                                                                BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false">
                                                                                <Legends>
                                                                                    <DCWC:Legend Name="Default" Enabled="false">
                                                                                    </DCWC:Legend>
                                                                                </Legends>
                                                                                <Titles>
                                                                                    <DCWC:Title Color="Black" Text="a*">
                                                                                        <Position X="90" Y="15" />
                                                                                    </DCWC:Title>
                                                                                </Titles>
                                                                                <BorderSkin SkinStyle="None"></BorderSkin>
                                                                                <UI>
                                                                                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                                            FrameBackColor="SteelBlue"></BorderSkin>
                                                                                    </Toolbar>
                                                                                </UI>
                                                                                <Series>
                                                                                    <DCWC:Series Name="Series1" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                                        Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                                                    </DCWC:Series>
                                                                                    <DCWC:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                                                                        Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                                                        BorderWidth="2" Name="Series2">
                                                                                    </DCWC:Series>
                                                                                </Series>
                                                                                <ChartAreas>
                                                                                    <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="#339966"
                                                                                        BackColor="#ff66cc" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                                        <Position Height="99" Width="94" X="0" Y="0" />
                                                                                        <AxisY LabelsAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="2"
                                                                                            Minimum="-6" Maximum="6">
                                                                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                            <LabelStyle FontAngle="0" Font="Verdana, 6pt" />
                                                                                        </AxisY>
                                                                                        <AxisX LabelsAutoFit="False" Margin="False" Title="" Interval="25" Minimum="0" Maximum="100">
                                                                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                            <LabelStyle FontAngle="0" Font="Verdana, 6pt" FontColor="Transparent" />
                                                                                        </AxisX>
                                                                                    </DCWC:ChartArea>
                                                                                </ChartAreas>
                                                                            </DCWC:Chart>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <DCWC:Chart ID="Chart_bISO" runat="server" Width="480px" Height="90px" ImageType="Jpeg"
                                                                                ImageUrl="~/UploadFiles/temp/ChartbISO" BackGradientType="Center" Palette="Kindergarten"
                                                                                BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false">
                                                                                <Legends>
                                                                                    <DCWC:Legend Name="Default" Enabled="false">
                                                                                    </DCWC:Legend>
                                                                                </Legends>
                                                                                <Titles>
                                                                                    <DCWC:Title Color="Black" Text="b*">
                                                                                        <Position X="90" Y="15" />
                                                                                    </DCWC:Title>
                                                                                </Titles>
                                                                                <BorderSkin SkinStyle="None"></BorderSkin>
                                                                                <UI>
                                                                                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                                            FrameBackColor="SteelBlue"></BorderSkin>
                                                                                    </Toolbar>
                                                                                </UI>
                                                                                <Series>
                                                                                    <DCWC:Series Name="Series1" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                                        Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                                                    </DCWC:Series>
                                                                                    <DCWC:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                                                                        Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                                                        BorderWidth="2" Name="Series2">
                                                                                    </DCWC:Series>
                                                                                </Series>
                                                                                <ChartAreas>
                                                                                    <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="#3333cc"
                                                                                        BackColor="#ffff99" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                                        <Position Height="99" Width="94" X="0" Y="0" />
                                                                                        <AxisY LabelsAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="2"
                                                                                            Minimum="-6" Maximum="6">
                                                                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                            <LabelStyle FontAngle="0" Font="Verdana, 6pt" />
                                                                                        </AxisY>
                                                                                        <AxisX LabelsAutoFit="False" Margin="False" Interval="25" Minimum="0" Maximum="100"
                                                                                            Title="">
                                                                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                            <LabelStyle FontAngle="0" Font="Verdana, 6pt" />
                                                                                        </AxisX>
                                                                                    </DCWC:ChartArea>
                                                                                </ChartAreas>
                                                                            </DCWC:Chart>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="green-title">
                                                                            Gray Balance &#916;E
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <DCWC:Chart ID="Chart_GrayBalanceEISO" ImageType="Jpeg" runat="server" Width="475px"
                                                                                Height="90px" ImageUrl="~/UploadFiles/temp/Chart_GrayBalanceEISO" BackGradientType="Center"
                                                                                Palette="Kindergarten" BackGradientEndColor="#FFFFFF" BorderLineColor="#808080"
                                                                                EnableViewState="false">
                                                                                <Titles>
                                                                                    <DCWC:Title Color="Black" Text="&Delta;E">
                                                                                        <Position X="90" Y="25" />
                                                                                    </DCWC:Title>
                                                                                </Titles>
                                                                                <Series>
                                                                                    <DCWC:Series Name="Series0" BackGradientType="DiagonalRight" BorderColor="26, 59, 105"
                                                                                        BorderWidth="0" Color="#D4E8FA" ShadowOffset="70" Font="Microsoft Sans Serif, 7pt, style=Underline"
                                                                                        Label="" CustomAttributes="LabelStyle=Top,DrawingStyle=Cylinder" ShowInLegend="true">
                                                                                    </DCWC:Series>
                                                                                    <DCWC:Series Name="Series1" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                                                                        CustomAttributes="DrawingStyle=Cylinder" ShowInLegend="false">
                                                                                    </DCWC:Series>
                                                                                    <DCWC:Series Name="Series2" BorderWidth="0" Color="Transparent" CustomAttributes="DrawingStyle=Cylinder"
                                                                                        ShowInLegend="false" MarkerImage="~/Images/green.png">
                                                                                    </DCWC:Series>
                                                                                </Series>
                                                                                <BorderSkin SkinStyle="None"></BorderSkin>
                                                                                <UI>
                                                                                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                                            FrameBackColor="SteelBlue"></BorderSkin>
                                                                                    </Toolbar>
                                                                                </UI>
                                                                                <ChartAreas>
                                                                                    <DCWC:ChartArea Name="ChartArea1" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                                                                        BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                                        <Area3DStyle YAngle="10" XAngle="10" Enable3D="true" RightAngleAxes="False" WallWidth="0"
                                                                                            Clustered="true"></Area3DStyle>
                                                                                        <%--Enable3D="true"--%>
                                                                                        <Position Height="99" Width="94" X="0" Y="2" />
                                                                                        <AxisY Enabled="True">
                                                                                            <MajorGrid LineColor="Beige" />
                                                                                            <LabelStyle FontAngle="0" Font="Verdana, 6pt" />
                                                                                        </AxisY>
                                                                                        <AxisX Enabled="True" Interval="Auto">
                                                                                            <MajorGrid LineColor="Beige" />
                                                                                            <LabelStyle FontAngle="0" Font="Verdana, 6pt" />
                                                                                        </AxisX>
                                                                                    </DCWC:ChartArea>
                                                                                </ChartAreas>
                                                                            </DCWC:Chart>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajaxtoolkit:TabPanel>
                    <ajaxtoolkit:TabPanel ID="TabPanChart" Width="100%" runat="server" HeaderText="Patch Details">
                        <ContentTemplate>
                            <asp:ImageButton ID="btn_Excel" runat="server" OnClick="btn_Excel_Click" ImageUrl="~/Images/excel-icon.gif"
                                ToolTip="Export To Excel" />&nbsp;<asp:ImageButton ID="btn_DownloadFile" runat="server"
                                    OnClick="btn_DownloadFile_Click" ImageUrl="~/Images/notepad-icon.gif" ToolTip="Download File" /></br>
                            <div style="overflow: hidden; width: 740px; height: 30px;">
                                <table class="MasterTable_Green gridTabContainer1_TabPanChart_GV_MeasurementISOData_ctl01"
                                    cellpadding="0" cellspacing="0" border="0">
                                    <tr style="height: 30px">
                                        <td style="width: 21px" class="GridHeader_Green">
                                            &nbsp;
                                        </td>
                                        <td style="width: 25px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementISOData$ctl01$ctl02$ctl00$ctl00','')">
                                                Sample
                                                <br />
                                                Name</a>
                                        </td>
                                        <td style="width: 23px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementISOData$ctl01$ctl02$ctl00$ctl01','')">
                                                C</a>
                                        </td>
                                        <td style="width: 24px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementISOData$ctl01$ctl02$ctl00$ctl02','')">
                                                M</a>
                                        </td>
                                        <td style="width: 24px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementISOData$ctl01$ctl02$ctl00$ctl03','')">
                                                Y</a>
                                        </td>
                                        <td style="width: 24px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementISOData$ctl01$ctl02$ctl00$ctl04','')">
                                                K</a>
                                        </td>
                                        <!--<td style="width: 60px" class="GridHeader_Green">
                                                    <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementISOData$ctl01$ctl02$ctl00$ctl05','')">
                                                        Density</a> </td>-->
                                        <td style="width: 29px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementISOData$ctl01$ctl02$ctl00$ctl05','')">
                                                D_R</a>
                                        </td>
                                        <td style="width: 29px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementISOData$ctl01$ctl02$ctl00$ctl06','')">
                                                D_G</a>
                                        </td>
                                        <td style="width: 28px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementISOData$ctl01$ctl02$ctl00$ctl07','')">
                                                D_B</a>
                                        </td>
                                        <td style="width: 29px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementISOData$ctl01$ctl02$ctl00$ctl08','')">
                                                D_V</a>
                                        </td>
                                        <td style="width: 21px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementISOData$ctl01$ctl02$ctl00$ctl09','')">
                                                X</a>
                                        </td>
                                        <td style="width: 20px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementISOData$ctl01$ctl02$ctl00$ctl10','')">
                                                Y</a>
                                        </td>
                                        <td style="width: 21px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementISOData$ctl01$ctl02$ctl00$ctl11','')">
                                                Z</a>
                                        </td>
                                        <td style="width: 21px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementISOData$ctl01$ctl02$ctl00$ctl12','')">
                                                L*</a>
                                        </td>
                                        <td style="width: 22px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementISOData$ctl01$ctl02$ctl00$ctl113','')">
                                                a*</a>
                                        </td>
                                        <td style="width: 37px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementISOData$ctl01$ctl02$ctl00$ctl14','')">
                                                b*</a>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="overflow: auto; width: 740px; height: 340px;">
                                <rad:RadGrid ID="GV_MeasurementISOData" runat="server" AllowFilteringByColumn="False"
                                    AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" GridLines="None"
                                    Width="720px" PageSize="25" Skin="Green" OnNeedDataSource="GV_MeasurementISOData_NeedDataSource"
                                    OnSortCommand="GV_MeasurementISOData_SortCommand" OnItemDataBound="GV_MeasurementISOData_ItemDataBound"
                                    ShowHeader="false">
                                    <ItemStyle HorizontalAlign="Left" Wrap="true" Width="740px" />
                                    <PagerStyle Position="Top" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <MasterTableView AllowSorting="true" AllowNaturalSort="false">
                                        <NoRecordsTemplate>
                                            No Data Found.</NoRecordsTemplate>
                                        <Columns>
                                            <rad:GridTemplateColumn ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="30px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="LBL_Color" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridBoundColumn DataField="SampleName" HeaderText="Sample Name" UniqueName="SampleName">
                                                <ItemStyle Width="56px"></ItemStyle>
                                            </rad:GridBoundColumn>
                                            <rad:GridBoundColumn DataField="CMYK_C" HeaderText="C" SortExpression="CMYK_C">
                                                <ItemStyle Width="16px"></ItemStyle>
                                            </rad:GridBoundColumn>
                                            <rad:GridBoundColumn DataField="CMYK_M" HeaderText="M" SortExpression="CMYK_M">
                                                <ItemStyle Width="16px"></ItemStyle>
                                            </rad:GridBoundColumn>
                                            <rad:GridBoundColumn DataField="CMYK_Y" HeaderText="Y" SortExpression="CMYK_Y">
                                                <ItemStyle Width="16px"></ItemStyle>
                                            </rad:GridBoundColumn>
                                            <rad:GridBoundColumn DataField="CMYK_K" HeaderText="K" SortExpression="CMYK_K">
                                                <ItemStyle Width="16px"></ItemStyle>
                                            </rad:GridBoundColumn>
                                            <rad:GridBoundColumn DataField="D_R" HeaderText="D_R" SortExpression="D_R">
                                                <ItemStyle Width="15px"></ItemStyle>
                                            </rad:GridBoundColumn>
                                            <rad:GridBoundColumn DataField="D_G" HeaderText="D_G" SortExpression="D_G">
                                                <ItemStyle Width="15px"></ItemStyle>
                                            </rad:GridBoundColumn>
                                            <rad:GridBoundColumn DataField="D_B" HeaderText="D_B" SortExpression="D_B">
                                                <ItemStyle Width="15px"></ItemStyle>
                                            </rad:GridBoundColumn>
                                            <rad:GridBoundColumn DataField="D_V" HeaderText="D_V" SortExpression="D_V">
                                                <ItemStyle Width="15px"></ItemStyle>
                                            </rad:GridBoundColumn>
                                            <rad:GridBoundColumn DataField="CIEXYZ_X" HeaderText="X" SortExpression="CIEXYZ_X">
                                                <ItemStyle Width="30px"></ItemStyle>
                                            </rad:GridBoundColumn>
                                            <rad:GridBoundColumn DataField="CIEXYZ_Y" HeaderText="Y" SortExpression="CIEXYZ_Y">
                                                <ItemStyle Width="30px"></ItemStyle>
                                            </rad:GridBoundColumn>
                                            <rad:GridBoundColumn DataField="CIEXYZ_Z" HeaderText="Z" SortExpression="CIEXYZ_Z">
                                                <ItemStyle Width="30px"></ItemStyle>
                                            </rad:GridBoundColumn>
                                            <rad:GridBoundColumn DataField="CIELAB_L*" HeaderText="L*" DataFormatString="{0:f2}"
                                                SortExpression="CIELAB_L*">
                                                <ItemStyle Width="30px"></ItemStyle>
                                            </rad:GridBoundColumn>
                                            <rad:GridBoundColumn DataField="CIELAB_a*" HeaderText="a*" DataFormatString="{0:f2}"
                                                SortExpression="CIELAB_a*">
                                                <ItemStyle Width="30px"></ItemStyle>
                                            </rad:GridBoundColumn>
                                            <rad:GridBoundColumn DataField="CIELAB_b*" HeaderText="b*" DataFormatString="{0:f2}"
                                                SortExpression="CIELAB_b*">
                                                <ItemStyle Width="30px"></ItemStyle>
                                            </rad:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </rad:RadGrid>
                            </div>
                        </ContentTemplate>
                    </ajaxtoolkit:TabPanel>
                </ajaxtoolkit:TabContainer>
            </td>
        </tr>
    </table>
    <div class="FadingTooltip" id="FADINGTOOLTIP" style="z-index: 999; visibility: hidden;
        position: absolute">
    </div>

    <script language="javascript" type="text/javascript">
        function funTVIVisiblity() {
            document.getElementById("trTVI").style.display = "";
            document.getElementById("trSpread").style.display = "none";

        }
        function funSpreadVisiblity() {
            document.getElementById("trTVI").style.display = "none";
            document.getElementById("trSpread").style.display = "";

        }
    </script>

    </form>
</body>
</html>
