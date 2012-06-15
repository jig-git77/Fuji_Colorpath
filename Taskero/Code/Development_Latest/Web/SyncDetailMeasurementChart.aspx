<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SyncDetailMeasurementChart.aspx.cs"
    Inherits="SyncDetailMeasurementChart" %>

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
                    <ajaxtoolkit:TabPanel ID="TabPanSummary" Width="100%" runat="server" HeaderText="Ink Paper"
                        Style="padding: 0px; width: 100%" EnableViewState="false">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="UP_TrendLineCharts" runat="server">
                                <ContentTemplate>
                                    <uc1:ProgressBar ID="ProgressBar1" runat="server" />
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td style="width: 50%;" valign="top" class="CHARTTABLE">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td colspan="2" class="green-title">
                                                            &nbsp; &Delta;E Primary Patches
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
                                                            <DCWC:Chart ID="ChartSummary_DeltaE" runat="server" Width="210px" Height="345px"
                                                                ImageUrl="~/UploadFiles/temp/Summary_DeltaE" ImageType="Jpeg" EnableViewState="false">
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
                                            <td style="width: 50%;">
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
                                                                        <DCWC:Chart ID="CHRT_SummaryDensity" ImageType="Jpeg" runat="server" Width="380px"
                                                                            Height="175px" ImageUrl="~/UploadFiles/temp/CHRT_DensityDeviation" BackGradientType="Center"
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
                                                                                    <DCWC:Chart ID="CHRT_Paper1" runat="server" BackImageMode="Scaled" BackImage="~/Images/ISOPaperGraph1.jpg"
                                                                                        ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CHRT_Paper1" Width="240px" Height="120px"
                                                                                        EnableViewState="false">
                                                                                        <Legends>
                                                                                            <DCWC:Legend Name="Default" Enabled="False">
                                                                                            </DCWC:Legend>
                                                                                        </Legends>
                                                                                        <Series>
                                                                                            <DCWC:Series Name="Green" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                                                MarkerSize="10" MarkerBorderWidth="2" MarkerBorderColor="Green" MarkerStyle="Circle"
                                                                                                MarkerColor="Green" ShadowOffset="1">
                                                                                            </DCWC:Series>
                                                                                            <DCWC:Series Name="Paper" ChartType="Polar" MarkerBorderColor="Black" MarkerColor="White"
                                                                                                CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="10" MarkerBorderWidth="2"
                                                                                                MarkerStyle="Circle" ShadowOffset="1">
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
                                                                                    <DCWC:Chart ID="CHRT_Paper2" runat="server" BackImageMode="Scaled" BackImage="~/Images/ISOPaperGraph2.jpg"
                                                                                        ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CHRT_Paper2" Width="69px" Height="118px"
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
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </ajaxtoolkit:TabPanel>
                    <ajaxtoolkit:TabPanel ID="TabPanGray" Width="100%" runat="server" HeaderText="G7">
                        <ContentTemplate>
                            <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                <tr>
                                    <td style="width: 65%">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                            <tr>
                                                <td class="green-title">
                                                    &nbsp; Gray Balance ΔCh
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <DCWC:Chart ID="Chart_GrayF" runat="server" Width="500px" Height="105px" ImageType="Jpeg"
                                                        ImageUrl="~/UploadFiles/temp/GrayF" BackGradientType="Center" Palette="Kindergarten"
                                                        BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false">
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
                                                            <DCWC:Series Name="Series1Point" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                            </DCWC:Series>  
                                                            <DCWC:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false" Color="Red" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                                         BorderWidth="2" Name="Series2">
                                                         </DCWC:Series>  
                                                          <DCWC:Series Name="Series2Point" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                Color="Red" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                 BackGradientEndColor="162, 164, 255">
                                                            </DCWC:Series>                                                       
                                                        </Series>
                                                        <ChartAreas>
                                                            <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                                                BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                <Position Height="99" Width="94" X="0" Y="2" />
                                                                <AxisY LabelsAutoFit="False" Title="" TitleFont="Verdana, 8pt, style=Bold" Interval="1"
                                                                    Minimum="0" Maximum="5">
                                                                    <MajorGrid LineColor="Gray"></MajorGrid>
                                                                    <LabelStyle FontAngle="0" Font="Verdana, 6pt" />
                                                                </AxisY>
                                                                <AxisX LabelsAutoFit="False" Margin="False" Interval="25" Title="" Minimum="0" Maximum="100">
                                                                    <MajorGrid LineColor="Gray"></MajorGrid>
                                                                    <LabelStyle FontAngle="0" Font="Verdana, 6pt" />
                                                                </AxisX>
                                                            </DCWC:ChartArea>
                                                        </ChartAreas>
                                                    </DCWC:Chart>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 35%;" valign="top">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                            <tr>
                                                <td class="green-title">
                                                    &nbsp; Gray  ΔCh Quartile Summary
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 2px">
                                                    <asp:Repeater ID="rptGrayQuartileSummary" runat="server">
                                                        <HeaderTemplate>
                                                            <table class="DATAGRID" style="width: 100%">
                                                                <tr class="HEADERSTYLE">
                                                                    <td style="width: 50%">
                                                                        Gray Quartile
                                                                    </td>
                                                                    <td style="width: 25%">
                                                                        Ave.  ΔCh
                                                                    </td>
                                                                    <td style="width: 25%">
                                                                        Max  ΔCh
                                                                    </td>
                                                                </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <%#Eval("Gray Quartile") %>
                                                                </td>
                                                                <td>
                                                                    <%#Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(Eval("AvgΔF")))%>
                                                                </td>
                                                                <td>
                                                                    <%#Eval("MAXΔF")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                <tr>
                                    <td style="width: 50%">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                            <tr>
                                                <td class="green-title">
                                                    &nbsp; Black NPD
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <DCWC:Chart ID="CHRT_BlackNPD" runat="server" Width="365px" Height="250px" ImageType="Jpeg"
                                                        ImageUrl="~/UploadFiles/temp/GrayNPD" BackGradientType="Center" Palette="Kindergarten"
                                                        BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false">
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
                                                                Color="Black" BorderWidth="3" ChartType="Spline" MarkerBorderColor="Green" BackGradientEndColor="162, 164, 255">
                                                            </DCWC:Series>
                                                            <DCWC:Series Name="Series0Point" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                Color="Black" BorderWidth="3" ChartType="Spline" MarkerBorderColor="Green" BackGradientEndColor="162, 164, 255">
                                                            </DCWC:Series>
                                                            <DCWC:Series Name="Series1" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                Color="Green" BorderWidth="3" ChartType="Spline" MarkerBorderColor="Black" BackGradientEndColor="162, 164, 255">
                                                            </DCWC:Series>
                                                            <DCWC:Series Name="Series1Point" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                Color="Green" BorderWidth="3" ChartType="Spline" MarkerBorderColor="Black" BackGradientEndColor="162, 164, 255">
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
                                    <td style="width: 50%;">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                            <tr>
                                                <td class="green-title">
                                                    &nbsp; Gray NPD
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <DCWC:Chart ID="CHRT_GrayNPD" runat="server" Width="365px" Height="250px" ImageType="Jpeg"
                                                        ImageUrl="~/UploadFiles/temp/GrayNPD" BackGradientType="Center" Palette="Kindergarten"
                                                        BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false">
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
                                                                Color="Black" BorderWidth="3" ChartType="Spline" MarkerBorderColor="Green" MarkerStyle="Circle"
                                                                MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                            </DCWC:Series>                                                            
                                                            <DCWC:Series Name="Series1" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                Color="Green" BorderWidth="3" ChartType="Spline" MarkerBorderColor="Black" MarkerStyle="Circle"
                                                                MarkerSize="15" BackGradientEndColor="162, 164, 255">
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
                                                    <table runat="server" cellpadding="0" cellspacing="0" border="0" width="100%" class="SYNCTABLE">
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
                                                                <DCWC:Chart ID="CHART_ABComparison" runat="server" BackImageMode="Scaled" BackImage="~/Images/CPSyncABComparison.jpg"
                                                                    ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CPSyncABComparison" Width="280px"
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
                                                                <DCWC:Chart ID="CHART_LComparison" runat="server" BackImageMode="Scaled" BackImage="~/Images/CPSyncLComparison.jpg"
                                                                    ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CPSyncLComparison" Width="280px"
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
                                                                            <DCWC:Chart ID="Chart_L" runat="server" Width="480px" Height="125px" ImageType="Jpeg"
                                                                                ImageUrl="~/UploadFiles/temp/ChartL" BackGradientType="Center" Palette="Kindergarten"
                                                                                BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false">
                                                                                <Titles>
                                                                                    <DCWC:Title Color="Black" Text="L*">
                                                                                        <Position X="90" Y="10" />
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
                                                                            <DCWC:Chart ID="Chart_a" runat="server" Width="480px" Height="125px" ImageType="Jpeg"
                                                                                ImageUrl="~/UploadFiles/temp/ChartA" BackGradientType="Center" Palette="Kindergarten"
                                                                                BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false">
                                                                                <Legends>
                                                                                    <DCWC:Legend Name="Default" Enabled="false">
                                                                                    </DCWC:Legend>
                                                                                </Legends>
                                                                                <Titles>
                                                                                    <DCWC:Title Color="Black" Text="a*">
                                                                                        <Position X="90" Y="10" />
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
                                                                            <DCWC:Chart ID="Chart_b" runat="server" Width="480px" Height="125px" ImageType="Jpeg"
                                                                                ImageUrl="~/UploadFiles/temp/Chartb" BackGradientType="Center" Palette="Kindergarten"
                                                                                BackGradientEndColor="White" BorderLineColor="#808080" EnableViewState="false">
                                                                                <Legends>
                                                                                    <DCWC:Legend Name="Default" Enabled="false">
                                                                                    </DCWC:Legend>
                                                                                </Legends>
                                                                                <Titles>
                                                                                    <DCWC:Title Color="Black" Text="b*">
                                                                                        <Position X="90" Y="10" />
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
                                                                        <td>
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
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                <tr>
                                    <td id="TDDotgain" runat="server">
                                        <DCWC:Chart ID="CHRT_SummaryDotgain" runat="server" Width="750px" Height="360px"
                                            ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CHRT_SummaryDotgain" BackGradientType="Center"
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
                                                <DCWC:Series Name="Series4" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                    MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                </DCWC:Series>
                                                <DCWC:Series Name="Series4Point" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                    MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
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
                                                        <LabelStyle FontAngle="0" />
                                                    </AxisX>
                                                </DCWC:ChartArea>
                                            </ChartAreas>
                                        </DCWC:Chart>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajaxtoolkit:TabPanel>
                    <ajaxtoolkit:TabPanel ID="TabPanChart" Width="100%" runat="server" HeaderText="Patch Details">
                        <ContentTemplate>
                            <asp:ImageButton ID="btn_Excel" runat="server" OnClick="btn_Excel_Click" ImageUrl="~/Images/excel-icon.gif"
                                ToolTip="Export To Excel" />&nbsp;<asp:ImageButton ID="btn_DownloadFile" runat="server"
                                    OnClick="btn_DownloadFile_Click" ImageUrl="~/Images/notepad-icon.gif" ToolTip="Download File" />&nbsp;<span class="required-text-small">**Density value displayed here is Absolute Density</span></br>
                            <div style="overflow: hidden; width: 740px; height: 30px;">
                                <table class="MasterTable_Green gridTabContainer1_TabPanChart_GV_MeasurementData_ctl01"
                                    cellpadding="0" cellspacing="0" border="0">
                                    <tr style="height: 30px">
                                        <td style="width: 21px" class="GridHeader_Green">
                                            &nbsp;
                                        </td>
                                        <td style="width: 25px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementData$ctl01$ctl02$ctl00$ctl00','')">
                                                Sample
                                                <br />
                                                Name</a>
                                        </td>
                                        <td style="width: 23px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementData$ctl01$ctl02$ctl00$ctl01','')">
                                                C</a>
                                        </td>
                                        <td style="width: 24px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementData$ctl01$ctl02$ctl00$ctl02','')">
                                                M</a>
                                        </td>
                                        <td style="width: 24px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementData$ctl01$ctl02$ctl00$ctl03','')">
                                                Y</a>
                                        </td>
                                        <td style="width: 24px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementData$ctl01$ctl02$ctl00$ctl04','')">
                                                K</a>
                                        </td>
                                        <!--<td style="width: 60px" class="GridHeader_Green">
                                                    <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementData$ctl01$ctl02$ctl00$ctl05','')">
                                                        Density</a> </td>-->
                                        <td style="width: 29px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementData$ctl01$ctl02$ctl00$ctl05','')">
                                                D_R</a>
                                        </td>
                                        <td style="width: 29px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementData$ctl01$ctl02$ctl00$ctl06','')">
                                                D_G</a>
                                        </td>
                                        <td style="width: 28px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementData$ctl01$ctl02$ctl00$ctl07','')">
                                                D_B</a>
                                        </td>
                                        <td style="width: 29px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementData$ctl01$ctl02$ctl00$ctl08','')">
                                                D_V</a>
                                        </td>
                                        <td style="width: 21px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementData$ctl01$ctl02$ctl00$ctl09','')">
                                                X</a>
                                        </td>
                                        <td style="width: 20px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementData$ctl01$ctl02$ctl00$ctl10','')">
                                                Y</a>
                                        </td>
                                        <td style="width: 21px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementData$ctl01$ctl02$ctl00$ctl11','')">
                                                Z</a>
                                        </td>
                                        <td style="width: 21px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementData$ctl01$ctl02$ctl00$ctl12','')">
                                                L*</a>
                                        </td>
                                        <td style="width: 22px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementData$ctl01$ctl02$ctl00$ctl113','')">
                                                a*</a>
                                        </td>
                                        <td style="width: 37px" class="GridHeader_Green">
                                            <a title="Click here to sort" href="javascript:__doPostBack('TabContainer1$TabPanChart$GV_MeasurementData$ctl01$ctl02$ctl00$ctl14','')">
                                                b*</a>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="overflow: auto; width: 740px; height: 340px;">
                                <rad:RadGrid ID="GV_MeasurementData" runat="server" AllowFilteringByColumn="False"
                                    AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" GridLines="None"
                                    Width="720px" PageSize="25" Skin="Green" OnNeedDataSource="GV_MeasurementData_NeedDataSource"
                                    OnSortCommand="GV_MeasurementData_SortCommand" OnItemDataBound="GV_MeasurementData_ItemDataBound"
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
    </form>
</body>
</html>
