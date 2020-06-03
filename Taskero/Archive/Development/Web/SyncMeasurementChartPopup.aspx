<%@ Page Title="" Language="C#" MasterPageFile="~/SyncPopup.master" AutoEventWireup="true"
    CodeFile="SyncMeasurementChartPopup.aspx.cs" Inherits="SyncMeasurementChartPopup" %>

<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table id="TblGamut" runat="server" cellpadding="0" cellspacing="0" border="0" width="100%"
        class="SYNCTABLE">
        <tr>
            <td class="green-title">
                a* - b* Comparison
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:Label ID="LblError" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <DCWC:Chart ID="CHART_ABComparison" runat="server" BackImageMode="Scaled" BackImage="~/Images/CPSyncABComparison.jpg"
                    Height="420px" ImageType="Jpeg" Width="480px" ImageUrl="~/UploadFiles/temp/CPSyncABComparison">
                    <Legends>
                        <DCWC:Legend Name="Default" Enabled="False">
                        </DCWC:Legend>
                    </Legends>
                    <Series>
                        <DCWC:Series Name="Series0" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                            MarkerSize="12" MarkerStyle="Circle" MarkerColor="Green" Color="Green" ShadowOffset="1">
                        </DCWC:Series>
                        <DCWC:Series Name="Series1" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                            MarkerSize="12" MarkerStyle="Circle" MarkerColor="Green" MarkerBorderColor="Black"
                            MarkerBorderWidth="1" Color="Green" ShadowOffset="1">
                        </DCWC:Series>
                        <DCWC:Series Name="Series2" ChartType="FastPoint" CustomAttributes="PolarDrawingStyle=Marker"
                            MarkerSize="12" MarkerStyle="Circle" MarkerColor="Green" Color="Red" ShadowOffset="1">
                        </DCWC:Series>
                        <DCWC:Series Name="Series3" ChartType="FastPoint" CustomAttributes="PolarDrawingStyle=Marker"
                            MarkerSize="12" MarkerStyle="Circle" MarkerBorderColor="Black" ShadowOffset="1">
                        </DCWC:Series>
                        <DCWC:Series Name="Series4" ChartType="FastPoint" CustomAttributes="PolarDrawingStyle=Marker"
                            MarkerSize="4" MarkerStyle="Circle" MarkerBorderColor="Black" ShadowOffset="1">
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
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <DCWC:Chart ID="CHART_LComparison" runat="server" BackImageMode="Scaled" BackImage="~/Images/CPSyncLComparison.jpg"
                    Height="107px" ImageType="Jpeg" Width="480px" ImageUrl="~/UploadFiles/temp/CPSyncLComparison">
                    <Legends>
                        <DCWC:Legend Name="Default" Enabled="False">
                        </DCWC:Legend>
                    </Legends>
                    <Series>
                        <DCWC:Series Name="Series0" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                            MarkerSize="12" MarkerStyle="Circle" MarkerColor="Green" Color="Green" ShadowOffset="1">
                        </DCWC:Series>
                        <DCWC:Series Name="Series1" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                            MarkerSize="12" MarkerStyle="Circle" MarkerColor="Green" MarkerBorderColor="Black"
                            MarkerBorderWidth="1" Color="Green" ShadowOffset="1">
                        </DCWC:Series>
                        <DCWC:Series Name="Series2" ChartType="FastPoint" CustomAttributes="PolarDrawingStyle=Marker"
                            MarkerSize="12" MarkerStyle="Circle" MarkerColor="Green" Color="Red" ShadowOffset="1">
                        </DCWC:Series>
                        <DCWC:Series Name="Series3" ChartType="FastPoint" CustomAttributes="PolarDrawingStyle=Marker"
                            MarkerSize="12" MarkerStyle="Circle" MarkerBorderColor="Black" ShadowOffset="1">
                        </DCWC:Series>
                        <DCWC:Series Name="Series4" ChartType="FastPoint" CustomAttributes="PolarDrawingStyle=Marker"
                            MarkerSize="4" MarkerStyle="Circle" MarkerBorderColor="Black" ShadowOffset="1">
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
    <table id="TblDotGain" runat="server" cellpadding="0" cellspacing="0" border="0"
        width="100%" class="SYNCTABLE">
        <tr>
            <td class="green-title" colspan="2">
                Dotgain Comparison
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
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
                            <DCWC:Chart ID="CHRT_DotgainCyan" runat="server" Width="370px" Height="240px" ImageType="Jpeg"
                                ImageUrl="~/UploadFiles/temp/DotgainCyan" BackGradientType="Center" Palette="Kindergarten"
                                BackGradientEndColor="White" BorderLineColor="#808080">
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
                                        Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="Black"
                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series0Point" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="Black"
                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
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
                            <DCWC:Chart ID="CHRT_DotgainMeganta" runat="server" Width="370px" Height="240px"
                                ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/DotgainMagenta" BackGradientType="Center"
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
                                        Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="Black"
                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series0Point" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="Black"
                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
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
                            <DCWC:Chart ID="CHRT_DotgainYellow" runat="server" Width="370px" Height="240px" ImageType="Jpeg"
                                ImageUrl="~/UploadFiles/temp/DotgainYellow" BackGradientType="Center" Palette="Kindergarten"
                                BackGradientEndColor="White" BorderLineColor="#808080">
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
                                        Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="Black"
                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series0Point" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="Black"
                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
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
                            <DCWC:Chart ID="CHRT_DotgainBlack" runat="server" Width="370px" Height="240px" ImageType="Jpeg"
                                ImageUrl="~/UploadFiles/temp/DotgainBlack" BackGradientType="Center" Palette="Kindergarten"
                                BackGradientEndColor="White" BorderLineColor="#808080">
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
                                        Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="Black"
                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                    </DCWC:Series>
                                    <DCWC:Series Name="Series0Point" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                        Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="Black"
                                        MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
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
     <div class="FadingTooltip" id="FADINGTOOLTIP" style="z-index: 999; visibility: hidden;
        position: absolute">
    </div>
</asp:Content>
