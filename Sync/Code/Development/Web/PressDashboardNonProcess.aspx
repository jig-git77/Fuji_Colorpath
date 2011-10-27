<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDeviceTrendLineNonProcess.master"  EnableEventValidation="false" AutoEventWireup="true" CodeFile="PressDashboardNonProcess.aspx.cs" Inherits="PressDashboardNonProcess" %>

<%@ Register assembly="DundasWebGauge" namespace="Dundas.Gauges.WebControl" tagprefix="DGWC" %>
<%@ Register assembly="DundasWebChart" namespace="Dundas.Charting.WebControl" tagprefix="DCWC" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
    <table id="Table1" cellpadding="0" cellspacing="0" border="0" style="width: 100%;margin-top:5px" runat="server" class="popup-table">
        <tr><td colspan="4" class="popup-top-bar"><b>Job Summary</b></td></tr>
        <tr>
            <td align="center" style="width:25%">
                <DGWC:GaugeContainer ID="CHRT_AvgDeltaE" ImageUrl="~/UploadFiles/temp/CHRT_AvgDeltaE_#SEQ(300,3)" runat="server" AutoLayout="False" BackColor="Transparent" Height="175px" Width="175px">
                    <backframe style="None" />
                    <StateIndicators>
                        <DGWC:StateIndicator FillColor="White" FillGradientEndColor="Gray" Name="StateIndicator1" Parent="CircularGauges.Default">
                            <States><DGWC:State FillColor="LawnGreen" FillGradientEndColor="DarkOliveGreen" Name="State1" /></States>
                            <Location X="7" Y="15" />
                            <Size Height="10" Width="10" />
                        </DGWC:StateIndicator>
                    </StateIndicators>
                    <circulargauges>
                        <DGWC:CircularGauge Name="Default">
                            <scales>
                                <DGWC:CircularScale Name="Default" Radius="60" StartAngle="90" SweepAngle="90" 
                                    BorderColor="White" FillColor="Ivory" Width="1" Maximum="25" Interval="5"> 
                                    <LabelStyle RotateLabels="False" TextColor="White" />
                                </DGWC:CircularScale>
                            </scales>
                            <pointers>
                                <DGWC:CircularPointer Name="Default" CapFillGradientEndColor="Black" 
                                    CapFillGradientType="DiagonalRight" CapWidth="15" FillColor="Red" 
                                    FillGradientEndColor="Crimson" Width="10" />
                            </pointers>
                            <PivotPoint X="75" Y="75"></PivotPoint>
                            <Location X="0" Y="0"></Location>
                            <Size Width="100" Height="100"></Size>
                            <backframe frameshape="CustomQuarterCircularNW1" style="Edged" FrameColor="003,129,236" FrameGradientEndColor="14,65,112" BackColor="3,93,145" BackGradientEndColor="3,127,223"/>
                        </DGWC:CircularGauge>
                    </circulargauges>
                    <values>
                        <DGWC:InputValue Name="Default"></DGWC:InputValue>
                    </values>
                    <Labels>
                        <DGWC:GaugeLabel Angle="310" BackColor="" BackGradientEndColor="" 
                            BorderStyle="NotSet" Font="Microsoft Sans Serif, 10pt" Name="Label1" 
                            Parent="CircularGauges.Default" ResizeMode="None" Text="Average Delta E" 
                            TextAlignment="MiddleCenter" TextColor="White" TextShadowOffset="2">
                            <Location X="23" Y="50" />
                            <Size Height="10" Width="65" />
                        </DGWC:GaugeLabel>
                    </Labels>
                </DGWC:GaugeContainer>
            </td>
            <td align="center" style="width:25%">
                <DGWC:GaugeContainer ID="CHRT_MaxDeltaE" ImageUrl="~/UploadFiles/temp/CHRT_MaxDeltaE_#SEQ(300,3)" runat="server" AutoLayout="False" BackColor="Transparent" Height="175px" Width="175px">
                    <backframe style="None" />
                    <StateIndicators>
                        <DGWC:StateIndicator FillColor="White" FillGradientEndColor="Gray" Name="StateIndicator1" Parent="CircularGauges.Default">
                            <States><DGWC:State FillColor="LawnGreen" FillGradientEndColor="DarkOliveGreen" Name="State1" /></States>
                            <Location X="7" Y="15" />
                            <Size Height="10" Width="10" />
                        </DGWC:StateIndicator>
                    </StateIndicators>
                    <circulargauges>
                        <DGWC:CircularGauge Name="Default">
                            <scales>
                                <DGWC:CircularScale Name="Default" Radius="60" StartAngle="90" SweepAngle="90" 
                                    BorderColor="White" FillColor="Ivory" Width="1" Maximum="25" Interval="5"> 
                                    <LabelStyle RotateLabels="False" TextColor="White" />
                                </DGWC:CircularScale>
                            </scales>
                            <pointers>
                                <DGWC:CircularPointer Name="Default" CapFillGradientEndColor="Black" 
                                    CapFillGradientType="DiagonalRight" CapWidth="15" FillColor="Red" 
                                    FillGradientEndColor="Crimson" Width="10" />
                            </pointers>
                            <PivotPoint X="75" Y="75"></PivotPoint>
                            <Location X="0" Y="0"></Location>
                            <Size Width="100" Height="100"></Size>
                            <backframe frameshape="CustomQuarterCircularNW1" style="Edged" FrameColor="003,129,236" FrameGradientEndColor="14,65,112" BackColor="3,93,145" BackGradientEndColor="3,127,223"/>
                        </DGWC:CircularGauge>
                    </circulargauges>
                    <values>
                        <DGWC:InputValue Name="Default"></DGWC:InputValue>
                    </values>
                    <Labels>
                        <DGWC:GaugeLabel Angle="310" BackColor="" BackGradientEndColor="" 
                            BorderStyle="NotSet" Font="Microsoft Sans Serif, 10pt" Name="Label1" 
                            Parent="CircularGauges.Default" ResizeMode="None" Text="Maximum Delta E" 
                            TextAlignment="MiddleCenter" TextColor="White" TextShadowOffset="2">
                            <Location X="23" Y="50" />
                            <Size Height="10" Width="65" />
                        </DGWC:GaugeLabel>
                    </Labels>
                </DGWC:GaugeContainer>
            </td>
            <td align="center" style="width:25%">
                <DGWC:GaugeContainer ID="CHRT_StdDev" ImageUrl="~/UploadFiles/temp/CHRT_StdDev_#SEQ(300,3)" runat="server" AutoLayout="False" BackColor="Transparent"  Height="175px" Width="175px">
                    <backframe style="None" />
                    <StateIndicators>
                        <DGWC:StateIndicator FillColor="White" FillGradientEndColor="Gray" Name="StateIndicator1" Parent="CircularGauges.Default">
                            <States><DGWC:State FillColor="LawnGreen" FillGradientEndColor="DarkOliveGreen" Name="State1" /></States>
                            <Location X="7" Y="15" />
                            <Size Height="10" Width="10" />
                        </DGWC:StateIndicator>
                    </StateIndicators>
                    <circulargauges>
                        <DGWC:CircularGauge Name="Default">
                            <scales>
                                <DGWC:CircularScale Name="Default" Radius="60" StartAngle="90" SweepAngle="90" 
                                    BorderColor="White" FillColor="Ivory" Width="1" Maximum="25" Interval="5"> 
                                    <LabelStyle RotateLabels="False" TextColor="White" />
                                </DGWC:CircularScale>
                            </scales>
                            <pointers>
                                <DGWC:CircularPointer Name="Default" CapFillGradientEndColor="Black" 
                                    CapFillGradientType="DiagonalRight" CapWidth="15" FillColor="Red" 
                                    FillGradientEndColor="Crimson" Width="10" />
                            </pointers>
                            <PivotPoint X="75" Y="75"></PivotPoint>
                            <Location X="0" Y="0"></Location>
                            <Size Width="100" Height="100"></Size>
                            <backframe frameshape="CustomQuarterCircularNW1" style="Edged" FrameColor="003,129,236" FrameGradientEndColor="14,65,112" BackColor="3,93,145" BackGradientEndColor="3,127,223"/>
                        </DGWC:CircularGauge>
                    </circulargauges>
                    <values>
                        <DGWC:InputValue Name="Default"></DGWC:InputValue>
                    </values>
                    <Labels>
                        <DGWC:GaugeLabel Angle="310" BackColor="" BackGradientEndColor="" 
                            BorderStyle="NotSet" Font="Microsoft Sans Serif, 10pt" Name="Label1" 
                            Parent="CircularGauges.Default" ResizeMode="None" Text="Std. Deviation" 
                            TextAlignment="MiddleCenter" TextColor="White" TextShadowOffset="2">
                            <Location X="23" Y="50" />
                            <Size Height="10" Width="65" />
                        </DGWC:GaugeLabel>
                    </Labels>
                </DGWC:GaugeContainer>
            </td>
            <td align="center" style="width:25%">
                <DGWC:GaugeContainer ID="CHRT_ErrorRate" ImageUrl="~/UploadFiles/temp/CHRT_ErrorRate_#SEQ(300,3)" runat="server" AutoLayout="False" BackColor="Transparent"  Height="175px" Width="175px">
                    <backframe style="None" />
                    <StateIndicators>
                        <DGWC:StateIndicator FillColor="White" FillGradientEndColor="Gray" Name="StateIndicator1" Parent="CircularGauges.Default">
                            <States><DGWC:State FillColor="LawnGreen" FillGradientEndColor="DarkOliveGreen" Name="State1" /></States>
                            <Location X="7" Y="15" />
                            <Size Height="10" Width="10" />
                        </DGWC:StateIndicator>
                    </StateIndicators>
                    <circulargauges>
                        <DGWC:CircularGauge Name="Default">
                            <scales>
                                <DGWC:CircularScale Name="Default" Radius="60" StartAngle="90" SweepAngle="90" 
                                    BorderColor="White" FillColor="Ivory" Width="1" Maximum="100" Interval="20"> 
                                    <LabelStyle RotateLabels="False" TextColor="White" />
                                </DGWC:CircularScale>
                            </scales>
                            <pointers>
                                
                                    <DGWC:CircularPointer Name="Default" CapFillGradientEndColor="Black" 
                                    CapFillGradientType="DiagonalRight" CapWidth="15" FillColor="Red" 
                                    FillGradientEndColor="Crimson" Width="10" />
                            </pointers>
                            <PivotPoint X="75" Y="75"></PivotPoint>
                            <Location X="0" Y="0"></Location>
                            <Size Width="100" Height="100"></Size>
                            <backframe frameshape="CustomQuarterCircularNW1" style="Edged" FrameColor="003,129,236" FrameGradientEndColor="14,65,112" BackColor="3,93,145" BackGradientEndColor="3,127,223"/>
                        </DGWC:CircularGauge>
                    </circulargauges>
                    <values>
                        <DGWC:InputValue Name="Default"></DGWC:InputValue>
                    </values>
                    <Labels>
                        <DGWC:GaugeLabel Angle="310" BackColor="" BackGradientEndColor="" 
                            BorderStyle="NotSet" Font="Microsoft Sans Serif, 10pt" Name="Label1" 
                            Parent="CircularGauges.Default" ResizeMode="None" Text="Error Rate" 
                            TextAlignment="MiddleCenter" TextColor="White" TextShadowOffset="2">
                            <Location X="23" Y="50" />
                            <Size Height="10" Width="65" />
                        </DGWC:GaugeLabel>
                    </Labels>
                </DGWC:GaugeContainer>
            </td>
        </tr>
    </table>
    <table id="Table2" cellpadding="0" cellspacing="0" border="0" style="width: 100%;margin-top:5px" runat="server" class="popup-table">
        <tr><td class="popup-top-bar"><b>Trend Delta E Summary</b></td></tr>
        <tr><td><table cellspacing="2">
                    <tr>
                        <td>
                            <DCWC:Chart ID="CHRT_TrendDeltaE" runat="server" Width="875px" Height="140px" ImageUrl="~/UploadFiles/temp/CHRT_TrendDeltaE_#SEQ(300,3)"  BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                                <Legends>
                                    <DCWC:Legend Name="Default"></DCWC:Legend>
                                    <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom"></DCWC:Legend>
                                </Legends>
                                <Series>
                                    <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                                    <DCWC:Series Name="Series1" Color="Green" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                                    <DCWC:Series Name="Series2" Color="Red" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                                    <DCWC:Series Name="Series3" Color="Blue" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                                </Series>
                                <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                <UI>
                                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                                    </Toolbar>
                                </UI>
                                <ChartAreas>
                                    <DCWC:ChartArea Name="ChartArea1">
                                        <Position Height="99" Width="98" X="0" Y="10" />
                                        <AxisY Enabled="True">
                                            <MajorGrid LineColor="Beige" />
                                            <SubAxes><DCWC:SubAxis Title="&Delta; E"></DCWC:SubAxis></SubAxes>
                                         </AxisY>
                                        <AxisX Enabled="True">
                                            <MajorGrid LineColor="Beige" />
                                            <SubAxes><DCWC:SubAxis Title="Sheet Number"></DCWC:SubAxis></SubAxes>
                                        </AxisX>                                    
                                    </DCWC:ChartArea>                                
                                </ChartAreas>
                            </DCWC:Chart>                
                         </td>
                        <td valign="top">
                            <div class="CHART_DIV_RIGHT">
                                <img src="Images/GreeLeftArror.gif" ID="BTN_TrendDeltaE" runat="server" align="Open List" />           
                                <asp:Panel ID="PNL_TrendDeltaE" runat="server" CssClass="popupMenu">
                                <div style="width:216px; float:left;"><div style="height:10px">
                                    <span style="width:215px;height:10px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-top-2.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-top-2.png" width="215" height="10" border="0" alt="" /></span>
                                </div>
                                <div style="height:90px; background:url(images/popup-mid.png) top left repeat-y #FFF; padding:3px 16px 15px 10px; color:#666666; font-family:Tahoma, Arial, Helvetica, sans-serif; font-size:11px; text-align:left; vertical-align:top;border-left:#808080;border-right:#808080">                        
                                    <asp:CheckBox ID="CHK_TrendDeltaE_AvgDe" runat="server" Text="<label class='deGREEN'>&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Average Delta E" Checked="true"   ToolTip="Average Delta E" /><br />
                                    <asp:CheckBox ID="CHK_TrendDeltaE_MaxDe" runat="server" Text="<label class='deRED'>&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Maximum Delta E" Checked="true"   ToolTip="Maximum Delta E" /><br />
                                    <asp:CheckBox ID="CHK_TrendDeltaE_StdDE" runat="server" Text="<label class='deBLUE'>&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Std. Dev. Delta E" Checked="true"  ToolTip="Standard Deviation Delta E" /><br /><br />
                                    <asp:Button ID="BTN_TrendDeltaE_Apply" runat="server" CssClass="BUTTON" Text="Apply" OnClick="BTN_TrendDeltaE_Click" />
                                    </div>
                                    <div style="clear:both"></div>
                                    <div style="float:left; height:19px"><span style="width:215px;height:19px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-bottom.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-bottom.png" width="216" height="19" border="0" alt="" /></span></div>
                                </asp:Panel>            
                                <cc1:hovermenuextender ID="HME_TrendDeltaE" runat="Server" 
                                    TargetControlID="BTN_TrendDeltaE"  PopupControlID="PNL_TrendDeltaE" 
                                    HoverCssClass="popupHover" PopupPosition="Left" OffsetX="0" OffsetY="0" 
                                    PopDelay="50" />                                 
                            </div>
                        </td>
                    </tr>
                </table></td></tr>
    </table>
    <table id="Table3" cellpadding="0" cellspacing="0" border="0" style="width: 100%;margin-top:5px" runat="server" class="popup-table">
        <tr><td class="popup-top-bar"><b>Zone Delta E Summary</b></td></tr>
        <tr>
            <td align="center"><DCWC:Chart ID="CHRT_ZoneDeltaE" runat="server" Width="900px" Height="150px" ImageUrl="~/UploadFiles/temp/CHRT_TrendDeltaE_#SEQ(300,3)" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                    <Legends>
                        <DCWC:Legend Name="Default"></DCWC:Legend>
                        <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom"></DCWC:Legend>
                    </Legends>
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
                        <DCWC:ChartArea Name="ChartArea1" BorderColor="26, 59, 105" BackGradientEndColor="White" BackColor="White" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                        <Area3DStyle YAngle="10" XAngle="10" RightAngleAxes="False" WallWidth="0" Clustered="True"></Area3DStyle>
                            <Position Height="99" Width="98" X="0" Y="10" />
                            <AxisY Enabled="True">
                                <MajorGrid LineColor="Beige" Interval="1" />
                                <SubAxes><DCWC:SubAxis Title="&Delta; E"></DCWC:SubAxis></SubAxes>
                             </AxisY>
                            <AxisX Enabled="True"  Interval="1">
                                <MajorGrid LineColor="Beige"/>                  
                                <SubAxes><DCWC:SubAxis Title="Zone Number"></DCWC:SubAxis></SubAxes>
                            </AxisX>                                
                        </DCWC:ChartArea>
                    </ChartAreas>                                
                </DCWC:Chart></td>
        </tr>
    </table>
    <asp:Literal ID="LblError" runat="server"></asp:Literal>
    <asp:Literal ID="LTRL_KeyJobSummary" runat="server" Visible="false"></asp:Literal>
    <asp:Literal ID="LTRL_KeyJobSheetSummary" runat="server" Visible="false"></asp:Literal>
    <asp:Literal ID="LTRL_KeyJobZoneSummary" runat="server" Visible="false"></asp:Literal>    
    <asp:Literal ID="LTRL_CPDeviceID" runat="server" Visible="false">0</asp:Literal>
    <asp:Literal ID="LTRL_JobID" runat="server" Visible="false">0</asp:Literal>
    <asp:Literal ID="LTRL_DongleID" runat="server" Visible="false">0</asp:Literal>
    <script language="javascript" type="text/javascript">
        function OpenMeasurementWindow( URLStr , NewId )
        {
            window.open( URLStr , NewId , 'width=950px,height=765px,resizable=0,scrollbars=0' );
        }
    </script>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

