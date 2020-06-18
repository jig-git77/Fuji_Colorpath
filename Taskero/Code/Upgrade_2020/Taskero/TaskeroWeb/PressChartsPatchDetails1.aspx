<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false"  CodeBehind="PressChartsPatchDetails1.aspx.cs" Inherits="TaskeroWeb.PressChartsPatchDetails1" %>


<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>FUJIFILM Graphic Systems U.S.A., Inc.</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <meta name="keywords" content="FUJIFILM Graphic Systems U.S.A., Inc." />
    <meta name="description" content="FUJIFILM Graphic Systems U.S.A., Inc." />
    <link href="CSS/Default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtrlCSS" runat="server"></asp:Literal>
    <table id="Table1" cellpadding="0" cellspacing="0" border="0" style="width: 100%;margin-top:5px" runat="server" class="popup-table">
        <tr><td colspan="3" class="popup-top-bar"><b>Patch Details</b></td></tr>
        <tr>
            <td style="width:150px">
                <DCWC:Chart ID="CHART_GAMUT" runat="server" BackImageMode="Scaled" BackImage="~/Images/FarbKreis_LAB2_150.gif"
                            Height="150px" Width="150px" ImageUrl="~/UploadFiles/temp/CHRT_GAMUT_#SEQ(300,3)">
                            <Legends>
                                <DCWC:Legend Name="Default" Enabled="False">
                                </DCWC:Legend>
                            </Legends>
                            
                            <Series>
                                <DCWC:Series ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="8"
                                    MarkerStyle="Circle" MarkerColor="Green" Color="Red" Name="Series1" ShadowOffset="1">
                                </DCWC:Series>
                                <DCWC:Series ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="5"
                                    MarkerStyle="Circle" MarkerColor="Red" Color="Green" Name="Series2" ShadowOffset="1">
                                </DCWC:Series>
                                <DCWC:Series ChartType="FastPoint" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="8"
                                    MarkerStyle="Circle" MarkerColor="Green" Color="Green" Name="Series3" ShadowOffset="1">
                                </DCWC:Series>
                                <DCWC:Series ChartType="FastPoint" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="5"
                                    MarkerStyle="Circle" MarkerColor="Red" Color="Green" Name="Series4" ShadowOffset="1">
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
            <td style="width:75px">
                <DCWC:Chart ID="CHART_L" runat="server" BorderLineColor="Black" BorderLineStyle="Solid"
                            BorderLineWidth="0" Width="75px" Height="150px" ImageUrl="~/UploadFiles/temp/CHART_L_#SEQ(300,3)">
                            <Legends>
                                <DCWC:Legend Name="Default" Enabled="False">
                                </DCWC:Legend>
                            </Legends>
                            <Series>
                                <DCWC:Series ChartType="Point" MarkerColor="Black" MarkerStyle="Diamond" MarkerSize="15"
                                    Name="Series1" ShadowOffset="1">
                                </DCWC:Series>
                                <DCWC:Series ChartType="Point" MarkerColor="Black" MarkerStyle="Diamond" MarkerSize="15"
                                    Name="Series2" ShadowOffset="1">
                                </DCWC:Series>
                                <DCWC:Series ChartType="Point" MarkerColor="Green" MarkerStyle="Circle" MarkerSize="13"
                                    Name="Series3" ShadowOffset="1">
                                </DCWC:Series>
                                <DCWC:Series ChartType="Point" MarkerColor="Red" MarkerStyle="Circle" MarkerSize="7"
                                    Name="Series4" ShadowOffset="1">
                                </DCWC:Series>
                            </Series>
                            <BorderSkin PageColor="AliceBlue" />
                            <Titles>
                                <DCWC:Title Name="Title1">
                                </DCWC:Title>
                            </Titles>
                            <ChartAreas>
                                <DCWC:ChartArea Name="Default">
                                    <AxisY Interval="50" IntervalOffset="10" Maximum="110" Minimum="-10">
                                        <MinorGrid Disabled="False" IntervalOffset="120" />
                                        <MajorTickMark Style="none" />
                                    </AxisY>
                                    <AxisX TitleAlignment="Near" Interval="1" Maximum="2">
                                        <MajorTickMark Style="none" />
                                        <LabelStyle Enabled="False" />
                                    </AxisX>
                                    <AxisX2>
                                        <LabelStyle Interval="1" />
                                    </AxisX2>
                                </DCWC:ChartArea>
                            </ChartAreas>
                        </DCWC:Chart>
            </td>
            <td valign="top">
                <table cellspacing="0" cellpadding="0" style="width:100%" border="0" class="DATAGRID">
                    <tr class="HEADERSTYLE1">                        
                        <td style="width:5%">&nbsp;</td>
                        <td style="width:8%">Metric</td>
                        <td style="width:6%">L</td>
                        <td style="width:7%">A</td>
                        <td style="width:7%">B</td>
                        <td style="width:8%">Hue</td>
                        <td style="width:8%">Chroma</td>
                        <td style="width:8%">Delta H</td>
                        <td style="width:8%">Density</td>
                        <td style="width:7%">Trap</td>
                        <td style="width:8%">Dotgain</td>
                        <td style="width:7%">Brightness</td>
                        <td style="width:8%">G7 DeltaEAB</td>
                    </tr>                    
                    <tr class="ITEMSTYLE" align="center">
                        <td id="TD_TargetColor" runat="server">&nbsp;</td>
                        <td>Target</td>
                        <td><asp:Literal ID="LTRL_TARGET_L" runat="server">---</asp:Literal></td>
                        <td><asp:Literal ID="LTRL_TARGET_A" runat="server">---</asp:Literal></td>
                        <td><asp:Literal ID="LTRL_TARGET_B" runat="server">---</asp:Literal></td>
                        <td><asp:Literal ID="LTRL_TARGET_Hue" runat="server">---</asp:Literal></td>
                        <td><asp:Literal ID="LTRL_TARGET_Chorma" runat="server">---</asp:Literal></td>
                        <td><asp:Literal ID="LTRL_TARGET_DeltaH" runat="server">---</asp:Literal></td>
                        <td><asp:Literal ID="LTRL_TARGET_Density" runat="server">---</asp:Literal></td>
                        <td><asp:Literal ID="LTRL_TARGET_Trap" runat="server">---</asp:Literal></td>
                        <td><asp:Literal ID="LTRL_TARGET_Dotgain" runat="server">---</asp:Literal></td>
                        <td><asp:Literal ID="LTRL_TARGET_BRIGHTNESS" runat="server">---</asp:Literal></td>
                        <td><asp:Literal ID="LTRL_TARGET_G7DELTAAB" runat="server">---</asp:Literal></td>
                    </tr>
                    <tr class="ALTERNATINGITEMSTYLE" align="center">
                        <td id="TD_ActualColor" runat="server">&nbsp;</td>
                        <td>Actual</td>
                        <td><asp:Literal ID="LTRL_ACTUAL_L" runat="server">---</asp:Literal></td>
                        <td><asp:Literal ID="LTRL_ACTUAL_A" runat="server">---</asp:Literal></td>
                        <td><asp:Literal ID="LTRL_ACTUAL_B" runat="server">---</asp:Literal></td>
                        <td><asp:Literal ID="LTRL_ACTUAL_Hue" runat="server">---</asp:Literal></td>
                        <td><asp:Literal ID="LTRL_ACTUAL_Chorma" runat="server">---</asp:Literal></td>
                        <td><asp:Literal ID="LTRL_ACTUAL_DeltaH" runat="server">---</asp:Literal></td>
                        <td><asp:Literal ID="LTRL_ACTUAL_Density" runat="server">---</asp:Literal></td>
                        <td><asp:Literal ID="LTRL_ACTUAL_Trap" runat="server">---</asp:Literal></td>
                        <td><asp:Literal ID="LTRL_ACTUAL_Dotgain" runat="server">---</asp:Literal></td>
                        <td><asp:Literal ID="LTRL_ACTUAL_BRIGHTNESS" runat="server"></asp:Literal></td>
                        <td><asp:Literal ID="LTRL_ACTUAL_G7DELTAAB" runat="server">---</asp:Literal></td>
                    </tr>                    
                </table>
                <DCWC:Chart ID="CHRT_PatchZoneDetails" runat="server" Width="690px" Height="75px" ImageUrl="~/UploadFiles/temp/CHRT_PatchZoneDetails_#SEQ(300,3)" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">>
                    <Legends>
                        <DCWC:Legend Name="Default"></DCWC:Legend>                        
                    </Legends>
                    <Titles>
                        <DCWC:Title Text="" Alignment="BottomCenter"></DCWC:Title>
                    </Titles>
                    <Series>
                        <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                        <DCWC:Series Name="Series1" Color="200, 0, 0, 0" BorderColor="100, 0 , 0 , 0" BackGradientType="VerticalCenter" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                    </Series>
                    <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                    <UI>
                        <Toolbar BackColor="White" BorderColor="LightSlateGray">
                            <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                        </Toolbar>
                    </UI>
                    <ChartAreas>
                        <DCWC:ChartArea Name="ChartArea1">
                            <Position Height="98" Width="98" X="0" Y="10" />                      
                            <AxisY Enabled="True">
                                <MajorGrid LineColor="Beige" />    
                                 <SubAxes>
                                    <DCWC:SubAxis Title="&Delta; E"></DCWC:SubAxis>
                                </SubAxes>
                            </AxisY>
                             <AxisX Enabled="True">
                                <MajorGrid LineColor="Beige" />
                                <LabelStyle FontAngle="0" />                                
                            </AxisX>
                        </DCWC:ChartArea>
                    </ChartAreas>
                </DCWC:Chart>
            </td>
        </tr>
    </table>       
    <asp:Literal ID="LblError" runat="server"></asp:Literal>
    <asp:Literal ID="LTRL_JobID" runat="server" Visible="False"></asp:Literal>
    <asp:Literal ID="LTRL_DongleID" runat="server" Visible="False"></asp:Literal>
    <asp:Literal ID="LTRL_ColorID" runat="server" Visible="False"></asp:Literal>
    <asp:Literal ID="LTRL_CPDeviceID" runat="server" Visible="False"></asp:Literal>
    <asp:Literal ID="LTRL_ZoneNumber" runat="server" Visible="False"></asp:Literal>
    <asp:Literal ID="LTRL_SheetNumber" runat="server" Visible="False"></asp:Literal>
    <asp:Literal ID="LTRL_KeyJobZoneSummary" runat="server" Visible="False"></asp:Literal>
    <asp:Literal ID="LTRL_KeyJobZoneSummaryPerSheet" runat="server" Visible="False"></asp:Literal>
    </form>
</body>
</html>
