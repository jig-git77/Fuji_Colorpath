<%@ Page Language="C#" MasterPageFile="~/MasterDeviceTrendLine.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="ColorColorCharacteristics.aspx.cs" Inherits="ColorColorCharacteristics" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <br />
            <table cellpadding="0" cellspacing="0" border="0" class="TABLE1" style="width:900px">
                <tr>
                    <td colspan="7" class="popup-top-bar" align="center">
                        Gamut and Gray Balance Details
                    </td>
                </tr>
                <tr><td colspan="7">&nbsp;</td></tr>
                <tr>
                    <td align="center">
                        <strong>Gamut </strong> &nbsp;<img id="ImgShowGamut" src="Images/Icon-Search.gif" onclick="PopupGamut();" style="cursor:pointer" />
                        <br />
                        <DCWC:Chart ID="CHART_GAMUT" runat="server" BackImageMode="Scaled" BackImage="~/Images/FarbKreis_LAB2_150.gif"
                            Height="150px" Width="150px" ImageUrl="~/UploadFiles/temp/CHRT_GAMUT_#SEQ(300,3)">
                            <Legends>
                                <DCWC:Legend Name="Default" Enabled="False">
                                </DCWC:Legend>
                            </Legends>
                            <Series>
                                <DCWC:Series ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="8"
                                    MarkerStyle="Circle" MarkerColor="Red" Color="Red" Name="Series1" ShadowOffset="1">
                                </DCWC:Series>
                                <DCWC:Series ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="5"
                                    MarkerStyle="Circle" MarkerColor="Green" Color="Green" Name="Series2" ShadowOffset="1">
                                </DCWC:Series>
                                <DCWC:Series ChartType="FastPoint" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="8"
                                    MarkerStyle="Circle" MarkerColor="Red" Color="Green" Name="Series3" ShadowOffset="1">
                                </DCWC:Series>
                                <DCWC:Series ChartType="FastPoint" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="5"
                                    MarkerStyle="Circle" MarkerColor="Green" Color="Green" Name="Series4" ShadowOffset="1">
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
                    <td align="right">
                        <strong>L 25% &nbsp; </strong>
                        <br />
                        <DCWC:Chart ID="CHART_L25" runat="server" BorderLineColor="Black" BorderLineStyle="Solid"
                            BorderLineWidth="0" Width="75px" Height="150px" ImageUrl="~/UploadFiles/temp/CHRT_L25_#SEQ(300,3)">
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
                                <DCWC:Series ChartType="Point" MarkerColor="Red" MarkerStyle="Circle" MarkerSize="13"
                                    Name="Series3" ShadowOffset="1">
                                </DCWC:Series>
                                <DCWC:Series ChartType="Point" MarkerColor="Green" MarkerStyle="Circle" MarkerSize="7"
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
                    <td align="center">
                        <strong>a b 25% </strong>
                        <br />
                        <DCWC:Chart ID="CHART_AB25" runat="server" BackImageMode="Scaled" BackImage="~/Images/FarbKreis_LAB3_150.gif"
                            Height="150px" Width="150px" ImageUrl="~/UploadFiles/temp/CHRT_AB25_#SEQ(300,3)">
                            <Legends>
                                <DCWC:Legend Name="Default" Enabled="False">
                                </DCWC:Legend>
                            </Legends>
                            <Series>
                                <DCWC:Series ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="13"
                                    MarkerStyle="Circle" MarkerColor="Red" Name="Series1" ShadowOffset="1">
                                </DCWC:Series>
                                <DCWC:Series ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="7"
                                    MarkerStyle="Circle" MarkerColor="Green" Name="Default" ShadowOffset="1">
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
                    <td align="right">
                        <strong>L 50% &nbsp; </strong>
                        <br />
                        <DCWC:Chart ID="CHART_L50" runat="server" BorderLineColor="Black" BorderLineStyle="Solid"
                            BorderLineWidth="0" Width="75px" Height="150px" ImageUrl="~/UploadFiles/temp/CHRT_L50_#SEQ(300,3)">
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
                                <DCWC:Series ChartType="Point" MarkerColor="Red" MarkerStyle="Circle" MarkerSize="13"
                                    Name="Series3" ShadowOffset="1">
                                </DCWC:Series>
                                <DCWC:Series ChartType="Point" MarkerColor="Green" MarkerStyle="Circle" MarkerSize="7"
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
                    <td align="center">
                        <strong>a b 50% </strong>
                        <br />
                        <DCWC:Chart ID="CHART_AB50" runat="server" BackImageMode="Scaled" BackImage="~/Images/FarbKreis_LAB3_150.gif"
                            Height="150px" Width="150px" ImageUrl="~/UploadFiles/temp/CHRT_AB50_#SEQ(300,3)">
                            <Legends>
                                <DCWC:Legend Name="Default" Enabled="False">
                                </DCWC:Legend>
                            </Legends>
                            <Series>
                                <DCWC:Series ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="13"
                                    MarkerStyle="Circle" MarkerColor="Red" Name="Series1" ShadowOffset="1">
                                </DCWC:Series>
                                <DCWC:Series ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="7"
                                    MarkerStyle="Circle" MarkerColor="Green" Name="Default" ShadowOffset="1">
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
                    <td align="right">
                        <strong>L 75% &nbsp; </strong>
                        <br />
                        <DCWC:Chart ID="CHART_L75" runat="server" BorderLineColor="Black" BorderLineStyle="Solid"
                            BorderLineWidth="0" Width="75px" Height="150px" ImageUrl="~/UploadFiles/temp/CHRT_L75_#SEQ(300,3)">
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
                                <DCWC:Series ChartType="Point" MarkerColor="Red" MarkerStyle="Circle" MarkerSize="13"
                                    Name="Series3" ShadowOffset="1">
                                </DCWC:Series>
                                <DCWC:Series ChartType="Point" MarkerColor="Green" MarkerStyle="Circle" MarkerSize="7"
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
                    <td align="center">
                        <strong>a b 75% </strong>
                        <br />
                        <DCWC:Chart ID="CHART_AB75" runat="server" BackImageMode="Scaled" BackImage="~/Images/FarbKreis_LAB3_150.gif"
                            Height="150px" Width="150px" ImageUrl="~/UploadFiles/temp/CHRT_AB75_#SEQ(300,3)">
                            <Legends>
                                <DCWC:Legend Name="Default" Enabled="False">
                                </DCWC:Legend>
                            </Legends>
                            <Series>
                                <DCWC:Series ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="13"
                                    MarkerStyle="Circle" MarkerColor="Red" Name="Series1" ShadowOffset="1">
                                </DCWC:Series>
                                <DCWC:Series ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="7"
                                    MarkerStyle="Circle" MarkerColor="Green" Name="Default" ShadowOffset="1">
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
                <tr><td colspan="7">&nbsp;</td></tr>
            </table>
            <div>&nbsp;</div>
            <table id="TBLPrintDetails" runat="server" cellpadding="0" cellspacing="0" border="0" class="popup-table" style="width:900px">
                <tr>
                    <td colspan="3" align="center"  class="popup-top-bar">Print Details</td>
                </tr>
                <tr style="height:300px">
                    <td align="center">
                        <DCWC:Chart 
                            ID="CHART_Density" 
                            runat="server" 
                            Width="270px" 
                            Height="250px" 
                            ImageUrl="~/UploadFiles/temp/CHRT_Density_#SEQ(300,3)"
                            ImageType="Jpeg" 
                            BackGradientType="Center" 
                            Palette="Kindergarten" 
                            BackGradientEndColor="#DFDFDF" 
                            BorderLineColor="#808080">
                            <Legends>
                                <DCWC:Legend Name="Default" Enabled="false"></DCWC:Legend>
                            </Legends>
                            <Titles>
                                <DCWC:Title Text="Density" Font="Microsoft Sans Serif, 8pt, style=Bold"></DCWC:Title>
                            </Titles>
                            <Series>
                                <DCWC:Series Name="Series1" ShadowOffset="1" BorderWidth="0" CustomAttributes="DrawingStyle=Cylinder"></DCWC:Series>
                            </Series>
                            <BorderSkin FrameBackGradientEndColor="Transparent" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="Transparent"></BorderSkin>
                            <UI>
                                <Toolbar BackColor="Transparent" BorderColor="LightSlateGray">
                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                                </Toolbar>
                            </UI>
                            <ChartAreas>
                                <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                    BackColor="White" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
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
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <DCWC:Chart ID="CHART_TVI" runat="server" ImageUrl="~/UploadFiles/temp/CHRT_TVI_#SEQ(300,3)" Width="300px" 
                                        Height="250px" ImageType="Jpeg" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                                        <Legends>
                                            <DCWC:Legend Name="Default" Enabled="False">
                                            </DCWC:Legend>
                                            <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                            </DCWC:Legend>
                                        </Legends>
                                        <Titles>
                                            <DCWC:Title Text="Dotgain" Font="Microsoft Sans Serif, 8pt, style=Bold">
                                                
                                            </DCWC:Title>
                                        </Titles>
                                        <Series>
                                            <DCWC:Series 
                                                Name="Series0"  Color="Transparent" 
                                                BorderWidth="1" ChartType="Spline" 
                                                MarkerBorderColor="#FFFFFF" 
                                                MarkerStyle="Circle" 
                                                MarkerSize="3">
                                            </DCWC:Series>
                                            <DCWC:Series Name="Series1" Color="150, 000, 255, 255"  BorderWidth="1"
                                                ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Triangle" MarkerSize="3">
                                            </DCWC:Series>
                                            <DCWC:Series Name="Series2" Color="150, 255, 000, 255"  BorderWidth="1"
                                                ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Triangle" MarkerSize="3">
                                            </DCWC:Series>
                                            <DCWC:Series Name="Series3" Color="150, 255, 225, 000" BorderWidth="1"
                                                ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Triangle" MarkerSize="3">
                                            </DCWC:Series>
                                            <DCWC:Series Name="Series4" Color="150, 000, 000, 000" BorderWidth="1"
                                                ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Triangle" MarkerSize="3">
                                            </DCWC:Series>
                                        </Series>
                                        <BorderSkin FrameBackGradientEndColor="Transparent" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="Transparent"></BorderSkin>
                                        <UI>
                                            <Toolbar BackColor="Transparent" BorderColor="LightSlateGray">
                                                <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                                            </Toolbar>
                                        </UI>
                                        <ChartAreas>
                                            <DCWC:ChartArea Name="ChartArea1">
                                                <Position Height="90" Width="90" X="5" Y="5" />
                                                <AxisY Enabled="True" Maximum="25" Minimum="-25" Interval="10" LabelsAutoFit="False">
                                                    <MajorGrid LineColor="Beige" />                                                    
                                                </AxisY>
                                                <AxisX Interval="Auto" LabelsAutoFit="False">
                                                    <MajorGrid LineColor="Beige" />
                                                </AxisX>
                                            </DCWC:ChartArea>
                                        </ChartAreas>
                                        </ChartAreas>
                                    </DCWC:Chart>
                                </td>
                                <td valign="top">
                                <img src="Images/GreeLeftArror.gif" id="BTN_TVI" runat="server" align="Open List" />
                                    <asp:Panel ID="PNL_TVI" runat="server" CssClass="popupMenu">
                                        <div style="width:216px; float:left;"><div style="height:10px">
                                            <span style="width:215px;height:10px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-top-2.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-top-2.png" width="215" height="10" border="0" alt="" /></span>
                                        </div>
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
                                            OnClick="CHK_TVI_CheckedChanged" />
                                        </div>
                                        <div style="clear:both"></div>
                                        <div style="float:left; height:19px"><span style="width:215px;height:19px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-bottom.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-bottom.png" width="216" height="19" border="0" alt="" /></span></div>
                                    </asp:Panel>
                                    <cc1:hovermenuextender id="HME_TVI" runat="Server" targetcontrolid="BTN_TVI"
                                        popupcontrolid="PNL_TVI" hovercssclass="popupHover" popupposition="Left"
                                        offsetx="0" offsety="0" popdelay="50" />                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="center">
                        <DCWC:Chart ID="CHART_PrintContrast" runat="server" Width="250px" 
                            Height="250px" ImageUrl="~/UploadFiles/temp/CHRT_PrintContrast_#SEQ(300,3)" ImageType="Jpeg" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                            <Legends>
                                <DCWC:Legend Name="Default" Enabled="false">
                                </DCWC:Legend>
                            </Legends>
                            <Titles>
                                <DCWC:Title Text="Print Contrast" Font="Microsoft Sans Serif, 8pt, style=Bold">
                                </DCWC:Title>
                            </Titles>
                            <Series>
                                 <DCWC:Series Name="Series0" BackGradientType="TopBottom" 
                                    BorderColor="26, 59, 105" BorderWidth="0" Color="Gray" 
                                    Font="Microsoft Sans Serif, 7pt" Label=""
                                    CustomAttributes="LabelStyle=Top" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series1" ShadowOffset="1" BorderWidth="0" 
                                    CustomAttributes="DrawingStyle=Cylinder" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series2" BorderColor="26, 59, 105" BorderWidth="0" 
                                    Font="Microsoft Sans Serif, 7pt" Label="Max"
                                    CustomAttributes="LabelStyle=Top" MarkerBorderWidth="0" MarkerSize="0" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series3" BorderColor="26, 59, 105" BorderWidth="0" 
                                    Font="Microsoft Sans Serif, 7pt" Label="Min" 
                                    CustomAttributes="LabelStyle=Top" MarkerSize="0" MarkerBorderWidth="0" 
                                     ShowInLegend="false"></DCWC:Series>
                            </Series>
                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                            <UI>
                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                                </Toolbar>
                            </UI>
                            <ChartAreas>
                                <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                    BackColor="White" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
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
            <asp:Literal ID="LTRL_CPMeasurementGUID" runat="server" Visible="false"></asp:Literal>
            <asp:Literal ID="LTRL_CPDeviceID" runat="server" Visible="false"></asp:Literal>
            <asp:Literal ID="LTRL_CPDevicePrintTargetID" runat="server" Visible="false"></asp:Literal>
            <asp:Literal ID="LTRL_KeyColorChars" runat="server" Visible="false"></asp:Literal>
            <asp:Literal ID="LTRL_KeyColorCharsDotgain" runat="server" Visible="false"></asp:Literal>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <script language="javascript" type="text/javascript">
    
    function PopupGamut()
    {
        window.open('ColorPopUpGuamat.aspx?CPMeasurementGUID=<%= Request.QueryString["CPMeasurementGUID"].ToString() %>' ,'PopupDamut', 'width=300,height=332');
    }
    
    </script>
    
</asp:Content>
