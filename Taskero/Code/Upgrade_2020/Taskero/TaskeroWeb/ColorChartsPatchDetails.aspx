<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ColorChartsPatchDetails.aspx.cs" EnableEventValidation="false" Inherits="TaskeroWeb.ColorChartsPatchDetails" %>


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
      <table cellpadding="0" cellspacing="0" border="0" style="width: 100%" runat="server" id="TR_PatchDetails" class="popup-table">
        <tr>
            <td class="popup-top-bar">Patch Details&nbsp;:&nbsp;<asp:Literal ID="LTRL_PatchDetails" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td class="popup-table-td">
                <table cellpadding="0" cellspacing="5" border="0" style="width:100%">
                    <tr>
                        <td style="width:550px" valign="top">
                            <table cellpadding="0" cellspacing="0" class="DATAGRID1" style="width:100%">
                    <tr class="HEADERSTYLE">
                        <td colspan="2">
                         Target
                        </td>                        
                        <td colspan="2">
                            Actual
                        </td>
                    </tr>
                    <tr>
                        <td id="TD_LAB1_COLOR" runat="server" colspan="2" class="LABCOLORSTYLE1">
                            &nbsp;
                        </td>
                        <td id="TD_LAB2_COLOR" runat="server" colspan="2" class="LABCOLORSTYLE1">
                            &nbsp;
                        </td>
                    </tr>
                    <tr class="HEADERSTYLE">
                        <td>
                            Metric
                        </td>
                        <td>
                            Target
                        </td>
                        <td>
                            Actual
                        </td>
                        <td>
                            Delta
                        </td>
                    </tr>
                    <tr class="ITEMSTYLE">
                        <td class="CENTEREDTEXTWIDTHBOLD">
                            L
                        </td>
                        <td class="CENTEREDTEXTWIDTH">
                            <asp:Literal ID="LTRL_TARGET_L1" runat="server"></asp:Literal>
                        </td>
                        <td class="CENTEREDTEXTWIDTH">
                            <asp:Literal ID="LTRL_ACTUAL_L2" runat="server"></asp:Literal>
                        </td>
                        <td class="CENTEREDTEXTWIDTH">
                            <asp:Literal ID="LTRL_Delta_DL" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ALTERNATINGITEMSTYLE">
                        <td class="CENTEREDTEXTWIDTHBOLD">
                            A
                        </td>
                        <td class="CENTEREDTEXTWIDTH">
                            <asp:Literal ID="LTRL_TARGET_A1" runat="server"></asp:Literal>
                        </td>
                        <td class="CENTEREDTEXTWIDTH">
                            <asp:Literal ID="LTRL_ACTUAL_A2" runat="server"></asp:Literal>
                        </td>
                        <td class="CENTEREDTEXTWIDTH">
                            <asp:Literal ID="LTRL_Delta_DA" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ITEMSTYLE">
                        <td class="CENTEREDTEXTWIDTHBOLD">
                            B
                        </td>
                        <td class="CENTEREDTEXTWIDTH">
                            <asp:Literal ID="LTRL_TARGET_B1" runat="server"></asp:Literal>
                        </td>
                        <td class="CENTEREDTEXTWIDTH">
                            <asp:Literal ID="LTRL_ACTUAL_B2" runat="server"></asp:Literal>
                        </td>
                        <td class="CENTEREDTEXTWIDTH">
                            <asp:Literal ID="LTRL_Delta_DB" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ALTERNATINGITEMSTYLE">
                        <td class="CENTEREDTEXTWIDTHBOLD">
                            H
                        </td>
                        <td class="CENTEREDTEXTWIDTH">
                            ---
                        </td>
                        <td class="CENTEREDTEXTWIDTH">
                            ---
                        </td>
                        <td class="CENTEREDTEXTWIDTH">
                            <asp:Literal ID="LTRL_Delta_DH" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ITEMSTYLE">
                        <td class="CENTEREDTEXTWIDTHBOLD">
                            C
                        </td>
                        <td class="CENTEREDTEXTWIDTH">
                            ---
                        </td>
                        <td class="CENTEREDTEXTWIDTH">
                            ---
                        </td>
                        <td class="CENTEREDTEXTWIDTH">
                            <asp:Literal ID="LTRL_Delta_DC" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr class="ALTERNATINGITEMSTYLE">
                        <td class="CENTEREDTEXTWIDTHBOLD">
                            PC
                        </td>
                        <td class="CENTEREDTEXTWIDTH">
                            ---</asp:Literal>
                        </td>
                        <td class="CENTEREDTEXTWIDTH">
                            ---
                        </td>
                        <td class="CENTEREDTEXTWIDTH">
                            <asp:Literal ID="LTRL_Delta_PC" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
                        </td>
                        <td style="width:250px">
                            <dcwc:chart id="CHRT_PlotCMYK" runat="server" backimagemode="Scaled" backimage="~/Images/FarbKreis_LAB2_150.gif"
                    height="250px" width="250px" imageurl="~/UploadFiles/temp/CHRT_PlotCMYK_#SEQ(300,3)">
                    <Legends>
                        <DCWC:Legend Name="Legend1" Enabled="False"></DCWC:Legend>
                    </Legends>
                    <Series>
                        <DCWC:Series ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="15" MarkerStyle="Circle" MarkerColor="Red" Name="Series1" ShadowOffset="1"></DCWC:Series>
                        <DCWC:Series ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="10" MarkerStyle="Circle" MarkerColor="Green" Name="Series2" ShadowOffset="1"></DCWC:Series>
                    </Series>
                    <BorderSkin PageColor="AliceBlue" />
                    <Titles>
                        <DCWC:Title Name="Title1"></DCWC:Title>
                    </Titles>
                    <ChartAreas>
                        <DCWC:ChartArea BackColor="Transparent" BorderColor="" Name="Default">
                            <AxisY LineColor="120, 0, 0, 0" TitleAlignment="Near" Maximum="100" Minimum="-100" LineStyle="NotSet">
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
                </dcwc:chart>
                        </td>
                        <td>
                            <dcwc:chart id="CHRT_PlotL" runat="server" borderlinecolor="Black" borderlinestyle="Solid"
                    borderlinewidth="0" width="100px" height="250" imageurl="~/UploadFiles/temp/CHRT_PlotL_#SEQ(300,3)" BackColor="Transparent">
                    <Series>
                        <DCWC:Series ChartType="Point" MarkerColor="Black" MarkerStyle="Diamond" MarkerSize="15" Name="Series1" ShadowOffset="1"></DCWC:Series>
                        <DCWC:Series ChartType="Point" MarkerColor="White" BorderColor="Black" MarkerStyle="Circle" MarkerSize="15" Name="Series2" ShadowOffset="1"></DCWC:Series>
                        <DCWC:Series ChartType="Point" MarkerColor="Red" MarkerStyle="Circle" MarkerSize="15" Name="Series3" ShadowOffset="1"></DCWC:Series>
                        <DCWC:Series ChartType="Point" MarkerColor="Green" MarkerStyle="Circle" MarkerSize="10" Name="Series4" ShadowOffset="1"></DCWC:Series>
                    </Series>
                    <BorderSkin PageColor="AliceBlue" />
                    <ChartAreas>
                        <DCWC:ChartArea Name="ChartArea1">
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
                </dcwc:chart>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>        
    </table>
    <asp:Literal ID="LTRL_KEY" runat="server" Visible="false"></asp:Literal>
    <asp:Literal ID="LblError" runat="server" Visible="false">0</asp:Literal>
    <asp:Literal ID="LTRL_CPPatchDataLabID" runat="server" Visible="false">0</asp:Literal>
    <asp:Literal ID="LTRL_CPDevicePrintTargetID" runat="server" Visible="false">0</asp:Literal>
    <asp:Literal ID="LTRL_CPDeviceID" runat="server" Visible="false">0</asp:Literal>
    <asp:Literal ID="LTRL_CPMeasurementGUID" runat="server" Visible="false"></asp:Literal>
    </form>
</body>
</html>

