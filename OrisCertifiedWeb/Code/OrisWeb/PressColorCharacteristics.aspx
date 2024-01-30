<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDeviceTrendLinePress.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="PressColorCharacteristics.aspx.cs" Inherits="OrisWeb.PressColorCharacteristics" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" class="TABLE1" style="width:900px;margin-top:5px;width:100%">
    <tr>
        <td colspan="4" class="popup-top-bar" align="center">
            Color Summary
        </td>
    </tr>
    <tr>
        <td style="WIDTH:25%" valign="top">
            <table cellpadding="0" cellspacing="0" class="DATAGRID" style="width:98%" align="center">
                <tr class="HEADERSTYLE">
                    <td>Metric</td>
                    <td>Actual</td>
                    <td>STD</td>
                </tr>
                <tr class="ITEMSTYLE"> 
                    <td>Average DE</td>
                    <td><asp:Literal ID="LTRL_ACT_AVGDE" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_AVGDE" runat="server"></asp:Literal></td>
                </tr>
                <tr class="ALTERNATINGITEMSTYLE">
                    <td>Cyan DE</td>
                    <td><asp:Literal ID="LTRL_ACT_CyanDE" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_CyanDE" runat="server"></asp:Literal></td>
                </tr>
                <tr class="ITEMSTYLE">
                    <td>Magenta DE</td>
                    <td><asp:Literal ID="LTRL_ACT_MagentaDE" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_MagentaDE" runat="server"></asp:Literal></td>
                </tr>
                <tr class="ALTERNATINGITEMSTYLE">
                    <td>Yellow DE</td>
                    <td><asp:Literal ID="LTRL_ACT_YellowDE" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_YellowDE" runat="server"></asp:Literal></td>
                </tr>                
            </table>
        </td>
        <td style="WIDTH:25%" valign="top">
             <table cellpadding="0" cellspacing="0" class="DATAGRID" style="width:98%" align="center">
                <tr class="HEADERSTYLE">
                    <td>Metric</td>
                    <td>Actual</td>
                    <td>STD</td>
                </tr>
                <tr class="ITEMSTYLE">
                    <td>Black DE</td>
                    <td><asp:Literal ID="LTRL_ACT_BlackDE" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_BlackDE" runat="server"></asp:Literal></td>
                </tr>
                <tr class="ALTERNATINGITEMSTYLE">
                    <td>Paper DE</td>
                    <td><asp:Literal ID="LTRL_ACT_PaperDE" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_PaperDE" runat="server"></asp:Literal></td>
                </tr>
                <tr class="ITEMSTYLE">
                    <td><asp:Literal ID="LTRL_Spot1DE_ColorName" runat="server" Text="Spot 1 DE"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_ACT_Spot1DE" runat="server">---</asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_Spot1DE" runat="server">---</asp:Literal></td>
                </tr>
                <tr class="ALTERNATINGITEMSTYLE">
                    <td><asp:Literal ID="LTRL_Spot2DE_ColorName" runat="server" Text="Spot 2 DE"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_ACT_Spot2DE" runat="server">---</asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_Spot2DE" runat="server">---</asp:Literal></td>
                </tr>
            </table>
        </td>
        <td style="WIDTH:25%" valign="top">
             <table cellpadding="0" cellspacing="0" class="DATAGRID" style="width:98%" align="center">
                <tr class="HEADERSTYLE">
                    <td>Metric</td>
                    <td>Actual</td>
                    <td>STD</td>
                </tr>
                <tr class="ITEMSTYLE">
                    <td>Average DH</td>
                    <td><asp:Literal ID="LTRL_ACT_AVGDH" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_AVGDH" runat="server"></asp:Literal></td>
                </tr>
                <tr class="ALTERNATINGITEMSTYLE">
                    <td>Cyan DH</td>
                    <td><asp:Literal ID="LTRL_ACT_CyanDH" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_CyanDH" runat="server"></asp:Literal></td>
                </tr>
                <tr class="ITEMSTYLE">
                    <td>Magenta DH</td>
                    <td><asp:Literal ID="LTRL_ACT_MagentaDH" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_MagentaDH" runat="server"></asp:Literal></td>
                </tr>
                <tr class="ALTERNATINGITEMSTYLE">
                    <td>Yellow DH</td>
                    <td><asp:Literal ID="LTRL_ACT_YellowDH" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_YellowDH" runat="server"></asp:Literal></td>
                </tr>
            </table>
        </td>
        <td style="WIDTH:25%" valign="top">
            <table cellpadding="0" cellspacing="0" class="DATAGRID" style="width:100%" align="center">
                <tr class="HEADERSTYLE">
                    <td>Metric</td>
                    <td>Actual</td>
                    <td>STD</td>
                </tr>                
                <tr class="ITEMSTYLE">
                    <td>Black DH</td>
                    <td><asp:Literal ID="LTRL_ACT_BlackDH" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_BlackDH" runat="server"></asp:Literal></td>
                </tr>
                <tr class="ALTERNATINGITEMSTYLE">
                    <td>Paper DH</td>
                    <td><asp:Literal ID="LTRL_ACT_PaperDH" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_PaperDH" runat="server"></asp:Literal></td>
                </tr>
                <tr class="ITEMSTYLE">
                    <td><asp:Literal ID="LTRL_Spot1DH_ColorName" runat="server" Text="Spot 1 DH"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_ACT_Spot1DH" runat="server">---</asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_Spot1DH" runat="server">---</asp:Literal></td>
                </tr>
                <tr class="ALTERNATINGITEMSTYLE">
                    <td><asp:Literal ID="LTRL_Spot2DH_ColorName" runat="server" Text="Spot 2 DH"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_ACT_Spot2DH" runat="server">---</asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_Spot2DH" runat="server">---</asp:Literal></td>
                </tr>
            </table>
        </td>
    </tr>    
</table>
<table cellpadding="0" cellspacing="0" border="0" class="DATAGRID" style="width:900px;margin-top:5px;width:100%">
    <td colspan="7" class="popup-top-bar" align="center">
        Gamut and Gray Balance Details
    </td>
    <tr>
        <td align="center" class="alternate-td-dark">
            <strong>Gamut </strong> &nbsp;<img id="ImgShowGamut" runat="server" src="Images/Icon-Search.gif" onclick="PopupGamut();" style="cursor:pointer" />
            <br />
            <DCWC:Chart ID="CHRT_GAMUT" runat="server" BackImageMode="Scaled" BackImage="~/Images/FarbKreis_LAB2_150.gif"
                Height="125px" Width="125px" ImageUrl="~/UploadFiles/temp/CHRT_GAMUT_#SEQ(300,3)">
                <Legends>
                    <DCWC:Legend Name="Default" Enabled="False">
                    </DCWC:Legend>
                </Legends>
                <Series>
                    <DCWC:Series Name="Series1" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="8"
                        MarkerStyle="Circle" MarkerColor="Green" Color="Green" ShadowOffset="1">
                    </DCWC:Series>
                    <DCWC:Series Name="Series2" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="5"
                        MarkerStyle="Circle" MarkerColor="Red" Color="Red" ShadowOffset="1">
                    </DCWC:Series>
                    <DCWC:Series Name="Series3" ChartType="FastPoint" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="8"
                        MarkerStyle="Circle" MarkerColor="Green" Color="Green" ShadowOffset="1">  
                    </DCWC:Series>
                     <DCWC:Series Name="Series4" ChartType="FastPoint" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="5"
                        MarkerStyle="Circle" MarkerColor="Red" Color="Red" ShadowOffset="1">  
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
        <td align="center" class="alternate-td-dark">
            <strong>a b 25% </strong>
            <br />
            
            <DCWC:Chart ID="CHRT_AB25" runat="server" BackImageMode="Scaled" BackImage="~/Images/FarbKreis_LAB3_150.gif"
                Height="125px" Width="125px" ImageUrl="~/UploadFiles/temp/CHRT_AB25_#SEQ(300,3)">
                <Legends>
                    <DCWC:Legend Name="Default" Enabled="False">
                    </DCWC:Legend>
                </Legends>
                <Series>
                    <DCWC:Series ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="13"
                        MarkerStyle="Circle" MarkerColor="Green" Name="Series1" ShadowOffset="1">
                    </DCWC:Series>
                    <DCWC:Series ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="7"
                        MarkerStyle="Circle" MarkerColor="Red" Name="Default" ShadowOffset="1">
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
                        <AxisX Margin="False" TitleAlignment="Near" Maximum="10" Minimum="-10" LineStyle="NotSet">
                            <MajorGrid Enabled="False" />
                            <MajorTickMark Enabled="false" />
                            <LabelStyle Enabled="False" />
                        </AxisX>
                    </DCWC:ChartArea>
                </ChartAreas>
            </DCWC:Chart>
        </td>
        <td align="right" class="alternate-td-dark">
            <strong>L 25% &nbsp; </strong>
            <br />
            <DCWC:Chart ID="CHRT_L25" runat="server" BorderLineColor="Black" BorderLineStyle="Solid"
                BorderLineWidth="0" Width="75px" Height="125px" ImageUrl="~/UploadFiles/temp/CHRT_L25_#SEQ(300,3)">
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
        <td align="center" class="alternate-td-dark">
            <strong>a b 50% </strong>
            <br />
            <DCWC:Chart ID="CHRT_AB50" runat="server" BackImageMode="Scaled" BackImage="~/Images/FarbKreis_LAB3_150.gif"
                Height="125px" Width="125px" ImageUrl="~/UploadFiles/temp/CHRT_AB50_#SEQ(300,3)">
                <Legends>
                    <DCWC:Legend Name="Default" Enabled="False">
                    </DCWC:Legend>
                </Legends>
                <Series>
                    <DCWC:Series ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="13"
                        MarkerStyle="Circle" MarkerColor="Green" Name="Series1" ShadowOffset="1">
                    </DCWC:Series>
                    <DCWC:Series ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="7"
                        MarkerStyle="Circle" MarkerColor="Red" Name="Default" ShadowOffset="1">
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
                        <AxisX Margin="False" TitleAlignment="Near" Maximum="10" Minimum="-10" LineStyle="NotSet">
                            <MajorGrid Enabled="False" />
                            <MajorTickMark Enabled="false" />
                            <LabelStyle Enabled="False" />
                        </AxisX>
                    </DCWC:ChartArea>
                </ChartAreas>
            </DCWC:Chart>
        </td>
        <td align="right" class="alternate-td-dark">
            <strong>L 50% &nbsp; </strong>
            <br />
            <DCWC:Chart ID="CHRT_L50" runat="server" BorderLineColor="Black" BorderLineStyle="Solid"
                BorderLineWidth="0" Width="75px" Height="125px" ImageUrl="~/UploadFiles/temp/CHRT_L50_#SEQ(300,3)">
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
        <td align="center" class="alternate-td-dark">
            <strong>a b 75% </strong>
            <br />
            <DCWC:Chart ID="CHRT_AB75" runat="server" BackImageMode="Scaled" BackImage="~/Images/FarbKreis_LAB3_150.gif"
                Height="125px" Width="125px" ImageUrl="~/UploadFiles/temp/CHRT_AB75_#SEQ(300,3)">
                <Legends>
                    <DCWC:Legend Name="Default" Enabled="False">
                    </DCWC:Legend>
                </Legends>
                <Series>
                    <DCWC:Series ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="13"
                        MarkerStyle="Circle" MarkerColor="Green" Name="Series1" ShadowOffset="1">
                    </DCWC:Series>
                    <DCWC:Series ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="7"
                        MarkerStyle="Circle" MarkerColor="Red" Name="Default" ShadowOffset="1">
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
                        <AxisX Margin="False" TitleAlignment="Near" Maximum="10" Minimum="-10" LineStyle="NotSet">
                            <MajorGrid Enabled="False" />
                            <MajorTickMark Enabled="false" />
                            <LabelStyle Enabled="False" />
                        </AxisX>
                    </DCWC:ChartArea>
                </ChartAreas>
            </DCWC:Chart>
        </td>
        <td align="right" class="alternate-td-dark">
            <strong>L 75% &nbsp; </strong>
            <br />
            <DCWC:Chart ID="CHRT_L75" runat="server" BorderLineColor="Black" BorderLineStyle="Solid"
                BorderLineWidth="0" Width="75px" Height="125px" ImageUrl="~/UploadFiles/temp/CHRT_L75_#SEQ(300,3)">
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
    </tr>
</table>
<table cellpadding="0" cellspacing="0" border="0" style="width: 900px;margin-top:5px;width:100%" class="DATAGRID">
    <tr>
        <td colspan="6" class="popup-top-bar" align="center">
            Print Details
        </td>
    </tr>    
    <tr  class="ITEMSTYLE">
        <td align="center">
            <DCWC:Chart ID="CHRT_Density" runat="server" Width="180px" Height="200px"  ImageUrl="~/UploadFiles/temp/CHRT_Density_#SEQ(300,3)" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                <Legends>
                    <DCWC:Legend Name="Default" Enabled="false">
                    </DCWC:Legend>
                </Legends>
                <Titles>
                    <DCWC:Title Text="Density" Font="Microsoft Sans Serif, 8pt, style=Bold">
                    </DCWC:Title>
                </Titles>
                <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                        <UI>
                            <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                            </Toolbar>
                        </UI>
                <Series>
                    <DCWC:Series Name="Series1" ShadowOffset="1" BorderWidth="0" CustomAttributes="DrawingStyle=Cylinder">
                    </DCWC:Series>
                </Series>
                <ChartAreas>
                    <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                        BackColor="White" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                        <Position Height="80" Width="90" X="0" Y="15" />
                        <AxisY LabelsAutoFit="False">
                            <MajorGrid LineColor="Beige"></MajorGrid>                                        
                        </AxisY>
                        <AxisX LabelsAutoFit="False">
                            <MajorGrid LineColor="Beige"></MajorGrid>
                            <LabelStyle FontAngle="-45" />
                        </AxisX>
                    </DCWC:ChartArea>
                </ChartAreas>
            </DCWC:Chart>
        </td>
        <td valign="top">       
        <div style="position:absolute; padding-left:220px; border:1 px solid 3333;">        
        <img src="Images/GreeLeftArror.gif" id="BTN_TVI" runat="server" align="Open List" />
        </div>
        
        <DCWC:Chart ID="CHRT_TVI" runat="server" ImageUrl="~/UploadFiles/temp/CHRT_TVI_#SEQ(300,3)" Width="250px" Height="200px" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
            <Legends>
                <DCWC:Legend Name="Default" Enabled="False"></DCWC:Legend>
                <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom"></DCWC:Legend>
            </Legends>
            <Titles>
                <DCWC:Title Text="Dotgain" Font="Microsoft Sans Serif, 8pt, style=Bold"></DCWC:Title>
            </Titles>
            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
            <UI>
                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                </Toolbar>
            </UI>
            <Series>
                <DCWC:Series Name="Series0" Color="Transparent" BorderWidth="1" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" MarkerSize="3"></DCWC:Series>
                <DCWC:Series Name="Series1" Color="150, 000, 255, 255"  BorderWidth="1" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Triangle" MarkerSize="3"></DCWC:Series>
                <DCWC:Series Name="Series2" Color="150, 255, 000, 255"  BorderWidth="1" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Triangle" MarkerSize="3"></DCWC:Series>
                <DCWC:Series Name="Series3" Color="150, 255, 225, 000" BorderWidth="1" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Triangle" MarkerSize="3"></DCWC:Series>
                <DCWC:Series Name="Series4" Color="150, 000, 000, 000" BorderWidth="1" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Triangle" MarkerSize="3"></DCWC:Series>
            </Series>
            <ChartAreas>
                <DCWC:ChartArea Name="ChartArea1">
                    <Position Height="80" Width="90" X="0" Y="15" />
                    <AxisY Enabled="True" Maximum="50" Minimum="-50" Interval="25" LabelsAutoFit="False">
                        <MajorGrid LineColor="Beige" Interval="25" />                                                    
                    </AxisY>
                    <AxisX Interval="Auto" LabelsAutoFit="False">
                        <MajorGrid LineColor="Beige" />
                    </AxisX>
                </DCWC:ChartArea>
            </ChartAreas>                                       
        </DCWC:Chart>        
        <asp:Panel ID="PNL_TVI" runat="server" CssClass="popupMenu">
            <div style="width:216px; float:left;"><div style="height:10px"><span style="width:200px;height:10px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-top-2.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-top-2.png" width="215" height="10" border="0" alt="" /></span></div>
            <div style="height:110px; background:url(images/popup-mid.png) top left repeat-y #FFF; padding:3px 16px 15px 10px; color:#666666; font-family:Tahoma, Arial, Helvetica, sans-serif; font-size:11px; text-align:left; vertical-align:top;border-left:#808080;border-right:#808080">                        

            <asp:CheckBox ID="CHK_TVI_C" runat="server" Text="<label class='deCYAN'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;CYAN"
                Checked="true" ToolTip="CYAN" /><br />
            <asp:CheckBox ID="CHK_TVI_M" runat="server" Text="<label class='deMAGENTA'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;MAGENTA"
                Checked="true" ToolTip="MAGENTA" /><br />
            <asp:CheckBox ID="CHK_TVI_Y" runat="server" Text="<label class='deYELLOW'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;YELLOW"
                Checked="true" ToolTip="YELLOW" /><br />
            <asp:CheckBox ID="CHK_TVI_K" runat="server" Text="<label class='deBLACK'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;BLACK"
                Checked="true" ToolTip="BLACK" /><br />
            <br />
            <asp:Button ID="BTN_TVI_Apply" runat="server" CssClass="BUTTON" Text="Apply"
                OnClick="BTN_TVI_Click" />
            </div>
            <div style="clear:both"></div>
            <div style="float:left; height:19px"><span style="width:200px;height:19px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-bottom.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-bottom.png" width="216" height="19" border="0" alt="" /></span></div>
            </asp:Panel>
        <cc1:hovermenuextender id="HME_TVI" runat="Server" targetcontrolid="BTN_TVI"
            popupcontrolid="PNL_TVI" hovercssclass="popupHover" popupposition="Left"
            offsetx="0" offsety="0" popdelay="50" />                                    
          
        </td>
        <td align="center">
            <DCWC:Chart ID="CHRT_PrintContrast" runat="server" Width="150px" Height="200px" ImageUrl="~/UploadFiles/temp/CHRT_PrintContrast_#SEQ(300,3)" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                <Legends>
                    <DCWC:Legend Name="Default" Enabled="false">
                    </DCWC:Legend>
                </Legends>
                <Titles>
                    <DCWC:Title Text="Print Contrast" Font="Microsoft Sans Serif, 8pt, style=Bold">
                    </DCWC:Title>
                </Titles>
                <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                <UI>
                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                    </Toolbar>
                </UI>
                <Series>
                     <DCWC:Series Name="Series1" ShadowOffset="1" BorderWidth="0" CustomAttributes="DrawingStyle=Cylinder">
                    </DCWC:Series>
                </Series>
                <ChartAreas>
                    <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                        BackColor="White" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                        <Position Height="80" Width="90" X="0" Y="15" />
                        <AxisY LabelsAutoFit="False">
                            <MajorGrid LineColor="Beige"></MajorGrid>                                        
                        </AxisY>
                        <AxisX LabelsAutoFit="False">
                            <MajorGrid LineColor="Beige"></MajorGrid>
                            <LabelStyle FontAngle="-45" />
                        </AxisX>
                    </DCWC:ChartArea>
                </ChartAreas>
            </DCWC:Chart>
        </td>
        <td align="center">
            <DCWC:Chart ID="CHRT_KYPD" runat="server" Width="150px" Height="200px" ImageUrl="~/UploadFiles/temp/CHRT_PrintContrast_#SEQ(300,3)" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                <Legends>
                    <DCWC:Legend Name="Default" Enabled="false">
                    </DCWC:Legend>
                </Legends>
                <Titles>
                    <DCWC:Title Text="K NPD Curve" Font="Microsoft Sans Serif, 8pt, style=Bold">
                    </DCWC:Title>
                </Titles>
                <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                        <UI>
                            <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                            </Toolbar>
                        </UI>
                <Series>
                    <DCWC:Series Name="Series1" Color="155, 0, 0, 0" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Triangle" MarkerSize="3" BackGradientEndColor="162, 164, 255"></DCWC:Series>
                    <DCWC:Series MarkerSize="3" BackGradientEndColor="162, 164, 255" Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Triangle" BorderWidth="2" Name="Series2"></DCWC:Series>
                </Series>
                <ChartAreas>
                    <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                        BackColor="White" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                        <Position Height="80" Width="90" X="0" Y="15" />
                        <AxisY LabelsAutoFit="False">
                            <MajorGrid LineColor="Beige"></MajorGrid>                                        
                        </AxisY>
                        <AxisX LabelsAutoFit="False">
                            <MajorGrid LineColor="Beige"></MajorGrid>
                            <LabelStyle FontAngle="-45" />
                        </AxisX>
                    </DCWC:ChartArea>
                </ChartAreas>
            </DCWC:Chart>
        </td>
        <td align="center" style="width: 204px">
            <DCWC:Chart ID="CHRT_CMYNPD" runat="server" Width="150px" Height="200px" ImageUrl="~/UploadFiles/temp/CHRT_PrintContrast_#SEQ(300,3)" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                <Legends>
                    <DCWC:Legend Name="Default" Enabled="false">
                    </DCWC:Legend>
                </Legends>
                <Titles>
                    <DCWC:Title Text="CMY NPD Curve" Font="Microsoft Sans Serif, 8pt, style=Bold">
                    </DCWC:Title>
                </Titles>
                <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                        <UI>
                            <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                            </Toolbar>
                        </UI>
                <Series>
                    <DCWC:Series Name="Series1" Color="Gray"  BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Triangle" MarkerSize="3"></DCWC:Series>
                    <DCWC:Series MarkerSize="3" BackGradientEndColor="162, 164, 255" Color="Green" Name="Series2" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Triangle" BorderWidth="2"></DCWC:Series>
                </Series>
                <ChartAreas>
                    <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                        BackColor="White" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                        <Position Height="80" Width="90" X="0" Y="15" />
                        <AxisY LabelsAutoFit="False">
                            <MajorGrid LineColor="Beige"></MajorGrid>                                        
                        </AxisY>
                        <AxisX LabelsAutoFit="False">
                            <MajorGrid LineColor="Beige"></MajorGrid>
                            <LabelStyle FontAngle="-45" />
                        </AxisX>
                    </DCWC:ChartArea>
                </ChartAreas>
            </DCWC:Chart>
        </td>
        <!-- Begin Trap chart cshah@fujifilmgs.com -->
         <td align="center">
            <DCWC:Chart ID="CHRT_TRAP" runat="server" Width="150px" Height="200px" ImageUrl="~/UploadFiles/temp/CHRT_TRAP_#SEQ(300,3)" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                <Legends>
                    <DCWC:Legend Name="Default" Enabled="false">
                    </DCWC:Legend>
                </Legends>
                <Titles>
                    <DCWC:Title Text="Trap" Font="Microsoft Sans Serif, 8pt, style=Bold">
                    </DCWC:Title>
                </Titles>
                <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                <UI>
                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                    </Toolbar>
                </UI>
                <Series>
                     <DCWC:Series Name="Series1" ShadowOffset="1" BorderWidth="0" CustomAttributes="DrawingStyle=Cylinder">
                    </DCWC:Series>
                </Series>
                <ChartAreas>
                    <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                        BackColor="White" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                        <Position Height="80" Width="90" X="0" Y="15" />
                        <AxisY LabelsAutoFit="False">
                            <MajorGrid LineColor="Beige"></MajorGrid>                                        
                        </AxisY>
                        <AxisX LabelsAutoFit="False">
                            <MajorGrid LineColor="Beige"></MajorGrid>
                            <LabelStyle FontAngle="-45" />
                        </AxisX>
                    </DCWC:ChartArea>
                </ChartAreas>
            </DCWC:Chart>
        </td>
        
       
    </tr>
</table>
<asp:Literal ID="LblError" runat="server"></asp:Literal>
<asp:Literal ID="LTRL_CPDeviceID" runat="server" Visible="false">0</asp:Literal>
<asp:Literal ID="LTRL_JobID" runat="server" Visible="false">0</asp:Literal>
<asp:Literal ID="LTRL_DongleID" runat="server" Visible="false">0</asp:Literal>
<asp:Literal ID="LTRL_KeyColorChars" runat="server" Visible="false">0</asp:Literal>
<script language="javascript" type="text/javascript">    
function PopupGamut()
{
    window.open('PressPopUpGaumat.aspx?JobID=<%=Request.QueryString["JobID"] %>&DongleID=<%=Request.QueryString["DongleID"] %>' ,'PopupDamut', 'width=325,height=332');
}    
</script>
</asp:Content>