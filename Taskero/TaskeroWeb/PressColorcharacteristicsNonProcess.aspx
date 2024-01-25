<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDeviceTrendLineNonProcess.Master" AutoEventWireup="true" EnableEventValidation="false"  CodeBehind="PressColorcharacteristicsNonProcess.aspx.cs" Inherits="TaskeroWeb.PressColorcharacteristicsNonProcess" %>

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
                    <%--<td>Cyan DE</td>--%>
                    <td><asp:Literal ID="LTRL_Ink1_ColorName" runat="server" Text="Ink-1"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_ACT_CyanDE" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_CyanDE" runat="server"></asp:Literal></td>
                </tr>
                <tr class="ITEMSTYLE">
                    <%--<td>Magenta DE</td>--%>
                    <td><asp:Literal ID="LTRL_Ink2_ColorName" runat="server" Text="Ink-2"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_ACT_MagentaDE" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_MagentaDE" runat="server"></asp:Literal></td>
                </tr>
                <tr class="ALTERNATINGITEMSTYLE">
                    <%--<td>Yellow DE</td>--%>
                    <td><asp:Literal ID="LTRL_Ink3_ColorName" runat="server" Text="Ink-3"></asp:Literal></td>
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
                    <%--<td>Black DE</td>--%>
                    <td><asp:Literal ID="LTRL_Ink4_ColorName" runat="server" Text="Ink-4"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_ACT_BlackDE" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_BlackDE" runat="server"></asp:Literal></td>
                </tr>
                <tr class="ALTERNATINGITEMSTYLE">
                    <td><asp:Literal ID="LTRL_Ink5_ColorName" runat="server" Text="Ink-5"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_ACT_Spot1DE" runat="server">---</asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_Spot1DE" runat="server">---</asp:Literal></td>
                </tr>
                <tr class="ITEMSTYLE">
                    <td><asp:Literal ID="LTRL_Ink6_ColorName" runat="server" Text="Ink-6"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_ACT_Spot2DE" runat="server">---</asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_Spot2DE" runat="server">---</asp:Literal></td>
                </tr>
                <tr class="ALTERNATINGITEMSTYLE">
                    <td>Paper DE</td>
                    <td><asp:Literal ID="LTRL_ACT_PaperDE" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_PaperDE" runat="server"></asp:Literal></td>
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
                    <%--<td>Cyan DH</td>--%>
                    <td><asp:Literal ID="LTRL_Ink1DH_ColorName" runat="server" Text="Ink-1 DH"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_ACT_CyanDH" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_CyanDH" runat="server"></asp:Literal></td>
                </tr>
                <tr class="ITEMSTYLE">
                    <%--<td>Magenta DH</td>--%>
                    <td><asp:Literal ID="LTRL_Ink2DH_ColorName" runat="server" Text="Ink-2 DH"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_ACT_MagentaDH" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_MagentaDH" runat="server"></asp:Literal></td>
                </tr>
                <tr class="ALTERNATINGITEMSTYLE">
                    <%--<td>Yellow DH</td>--%>
                    <td><asp:Literal ID="LTRL_Ink3DH_ColorName" runat="server" Text="Ink-3 DH"></asp:Literal></td>
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
                    <%--<td>Black DH</td>--%>
                    <td><asp:Literal ID="LTRL_Ink4DH_ColorName" runat="server" Text="Ink-4 DH"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_ACT_BlackDH" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_BlackDH" runat="server"></asp:Literal></td>
                </tr>
                <tr class="ALTERNATINGITEMSTYLE">
                    <td><asp:Literal ID="LTRL_Ink5DH_ColorName" runat="server" Text="Ink-5 DH"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_ACT_Spot1DH" runat="server">---</asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_Spot1DH" runat="server">---</asp:Literal></td>
                </tr>
                <tr class="ITEMSTYLE">
                    <td><asp:Literal ID="LTRL_Ink6DH_ColorName" runat="server" Text="Ink-6 DH"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_ACT_Spot2DH" runat="server">---</asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_Spot2DH" runat="server">---</asp:Literal></td>
                </tr>
                <tr class="ALTERNATINGITEMSTYLE">
                    <td>Paper DH</td>
                    <td><asp:Literal ID="LTRL_ACT_PaperDH" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="LTRL_STD_PaperDH" runat="server"></asp:Literal></td>
                </tr>
            </table>
        </td>
    </tr>    
</table>
<table cellpadding="0" cellspacing="0" border="0" class="DATAGRID" style="width:900px;margin-top:5px;width:100%">
    <tr>
        <td colspan="2" class="popup-top-bar" align="center">
            Gamut and Gray Balance Details
        </td>
    </tr>
    <tr class="ITEMSTYLE" style="height:400px"> 
        <td align="center" class="alternate-td-dark" style="width:45%">
            <strong>Gamut </strong> &nbsp;<%--<img id="ImgShowGamut" runat="server" src="Images/Icon-Search.gif" onclick="PopupGamut();" style="cursor:pointer" />--%>
            <br />
                    <%--
                    <DCWC:CHART 
                        id="CHRT_GAMUT" 
                        runat="server" 
                        Width="300px" 
                        BackImageMode="Scaled" 
                        Height="300px" 
                        BackImage="~/Images/FarbKreis_LAB2_150.gif"
                        ImageUrl="~/TempImages/ChartPic_#SEQ(300,3)" 
                        ImageType="Png" 
                        Palette="Dundas" 
                        >
							<titles>
								<DCWC:Title Visible="false" Name="Title 1"></dcwc:Title>
							</titles>
							<legends>
								<dcwc:Legend Enabled="false"
								    AutoFitText="False" 
								    Name="Default" 
								    BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" Alignment="Far">
									<position y="74.08253" height="14.23021" width="19.34047" x="74.73474"></position>
								</dcwc:Legend>
							</legends>
							<borderskin PageColor="AliceBlue"></borderSkin>
							<Series> 
							   	<DCWC:Series 
							   	    Name="Series1" 
							   	    MarkerSize="7" 
							   	    MarkerColor="Red" 
							   	    MarkerStyle="Circle" 
							   	    ChartType="Radar" 
							   	    Color="Red" 
							   	    ShadowOffset="1"></DCWC:Series>
							   	    
							   	<DCWC:Series 
                                    Name="Series2" 
                                    MarkerSize="5"
                                    MarkerColor="Green" 
                                    MarkerStyle="Circle"
                                    ChartType="Radar"  
                                    Color="Green" 
                                    ShadowOffset="1">
                                </DCWC:Series>
                                <DCWC:Series 
                                    Name="Series3" 
                                    MarkerSize="7"
                                    MarkerColor="Green" 
                                    MarkerStyle="Circle" 
                                    Color="Green" 
                                    ChartType="Radar" 
                                    ShadowOffset="1">
                                </DCWC:Series>
                                <DCWC:Series 
                                    Name="Series4" 
                                    MarkerSize="5"
                                    MarkerColor="Green" 
                                    MarkerStyle="Circle" 
                                    Color="Green" 
                                    ChartType="Radar" 
                                    ShadowOffset="1">
                                </DCWC:Series>
                                <DCWC:Series 
                                    Name="Series5" 
                                    MarkerSize="7"
                                    MarkerColor="Green" 
                                    MarkerStyle="Circle" 
                                    Color="Green" 
                                    ChartType="Radar" 
                                    BorderColor="Green"
                                    ShadowOffset="1">
                                </DCWC:Series>
                                <DCWC:Series 
                                    Name="Series6" 
                                    MarkerSize="5"
                                    MarkerColor="Green" 
                                    MarkerStyle="Circle" 
                                    Color="Green" 
                                    ChartType="Radar" 
                                    BorderColor="Green"
                                    ShadowOffset="1">
                                </DCWC:Series>
							</Series>
							<ChartAreas>
								<dcwc:ChartArea Name="Default" BorderColor="64, 64, 64, 64"  BackColor="Transparent">
									<AxisY linecolor="120, 0, 0, 0" TitleAlignment="Near"  Minimum="0" Maximum="100" LineStyle="NotSet">
										<labelstyle font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
										<MajorGrid LineStyle="Solid" Enabled="false"  linecolor="64, 64, 64, 64"></MajorGrid>
										<MajorTickMark LineStyle="Solid" size="0.6"></MajorTickMark>
									</AxisY>
									<AxisX linecolor="64, 64, 64, 64">
										<LabelStyle font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
										<MajorGrid Enabled="false" linecolor="64, 64, 64, 64"></MajorGrid>
										<MajorTickMark Enabled="false" />
									</AxisX>
								</dcwc:ChartArea>
								 
							</ChartAreas>
						</DCWC:CHART>--%>
						
            
            <DCWC:Chart 
                ID="CHRT_GAMUT"  
                runat="server" 
                BackImageMode="Scaled" 
                BackImage="~/Images/FarbKreis_LAB2_150.gif"
                Height="300px" 
                Width="300px" 
                ImageUrl="~/UploadFiles/temp/CHRT_GAMUT_#SEQ(300,3)">
                <Legends>
                    <DCWC:Legend Name="Default" Enabled="False">
                    </DCWC:Legend>
                </Legends>
                <Series>
                    <DCWC:Series ChartType="Point" CustomAttributes="PointDrawingStyle=Marker" MarkerSize="5"
                        MarkerStyle="Circle" MarkerColor="Red" Color="Red" Name="Series1" ShadowOffset="1">
                    </DCWC:Series>
                    <DCWC:Series ChartType="Point" CustomAttributes="PointDrawingStyle=Marker" MarkerSize="8"
                        MarkerStyle="Circle" MarkerColor="Green" Color="Green" Name="Series2" ShadowOffset="1">
                    </DCWC:Series>
                    <DCWC:Series ChartType="Point" CustomAttributes="PointDrawingStyle=Marker" MarkerSize="5"
                        MarkerStyle="Circle" MarkerColor="Red" Color="Green" Name="Series3" ShadowOffset="1">
                    </DCWC:Series>
                    <DCWC:Series ChartType="Point" CustomAttributes="PointDrawingStyle=Marker" MarkerSize="8"
                        MarkerStyle="Circle" MarkerColor="Green" Color="Green" Name="Series4" ShadowOffset="1">
                    </DCWC:Series>
                    
                     <DCWC:Series ChartType="Point" CustomAttributes="PointDrawingStyle=Marker" MarkerSize="5"
                        MarkerStyle="Circle" MarkerColor="Red" Color="Green" Name="Series5" ShadowOffset="1">
                    </DCWC:Series>
                    <DCWC:Series ChartType="Point" CustomAttributes="PointDrawingStyle=Marker" MarkerSize="8"
                        MarkerStyle="Circle" MarkerColor="Green" Color="Green" Name="Series6" ShadowOffset="1">
                    </DCWC:Series>
                </Series>
                <BorderSkin PageColor="AliceBlue" />
                <ChartAreas>
                    <DCWC:ChartArea BackColor="Transparent" BorderColor="" Name="Default">
                        <AxisY LineColor="120, 0, 0, 0" TitleAlignment="Near" Minimum="-100" Maximum="100"  LineStyle="NotSet" Interval="20">
                            <MajorGrid  Enabled="false"/>
                            <MajorTickMark Enabled="false"/>
                            <LabelStyle Enabled="false"/>
                        </AxisY>
                        <AxisX Margin="False" TitleAlignment="Near" Minimum="-100" Maximum="100"  LineStyle="NotSet" Interval="20">
                            <MajorGrid Enabled="false"/>
                            <MajorTickMark Enabled="false" />
                            <LabelStyle Enabled="false"/>
                        </AxisX>
                    </DCWC:ChartArea>
                </ChartAreas>
            </DCWC:Chart>
            
        </td>
        <td align="center"  style="width:55%">
            <DCWC:Chart ID="CHRT_Density" runat="server" Width="450px" Height="350px"  ImageUrl="~/UploadFiles/temp/CHRT_Density_#SEQ(300,3)" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
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
