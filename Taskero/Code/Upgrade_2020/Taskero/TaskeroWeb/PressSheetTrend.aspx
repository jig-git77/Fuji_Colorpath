<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDeviceTrendLinePressSheet.Master" AutoEventWireup="true" EnableEventValidation="false"  CodeBehind="PressSheetTrend.aspx.cs" Inherits="TaskeroWeb.PressSheetTrend" %>

<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<table cellpadding="0" cellspacing="5" border="0" style="width:900px">
    <tr>
        <td colspan="3">
            <div>
                Record(s)&nbsp;<asp:Literal ID="LTRL_StartRecord" runat="server">0</asp:Literal>&nbsp;to&nbsp;<asp:Literal ID="LTRL_EndRecord" runat="server">0</asp:Literal>&nbsp;of&nbsp;<asp:Literal ID="LTRL_TotalRec" runat="server">0</asp:Literal>&nbsp;|&nbsp;<asp:ImageButton ID="IBTN_FirstPage" runat="server" ImageUrl="Images/Nav_FirstPage_off.gif" OnClick="IBTN_FirstPage_Click" />&nbsp;<asp:ImageButton ID="IBTN_PreviousPage" runat="server" ImageUrl="Images/Nav_PreviousPage_off.gif" OnClick="IBTN_PreviousPage_Click" />&nbsp;<asp:ImageButton ID="IBTN_NextPage" runat="server" ImageUrl="Images/Nav_NextPage_off.gif" OnClick="IBTN_NextPage_Click" />&nbsp;<asp:ImageButton ID="IBTN_LastPage" runat="server" ImageUrl="Images/Nav_LastPage_off.gif" OnClick="IBTN_LastPage_Click" />&nbsp;|&nbsp;Records&nbsp;<asp:DropDownList ID="DDL_PageSize" runat="server" CssClass="DROPDOWNLIST2" AutoPostBack="true" OnSelectedIndexChanged="DDL_PageSize_SelectedIndexChanged">
                <asp:ListItem Value="5">5</asp:ListItem>
                <asp:ListItem Value="10">10</asp:ListItem>
                <asp:ListItem Value="25">25</asp:ListItem>
                <asp:ListItem Value="50" Selected="True">50</asp:ListItem>
                <asp:ListItem Value="100">100</asp:ListItem>
                <asp:ListItem Value="500">500</asp:ListItem>
                <asp:ListItem Value="1000">1000</asp:ListItem>
                </asp:DropDownList>&nbsp;|&nbsp;Go to page&nbsp;<asp:DropDownList ID="DDL_GoToPage" runat="server" CssClass="DROPDOWNLIST2" AutoPostBack="true" OnSelectedIndexChanged="DDL_GoToPage_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div id="CHART_DIV_SCROLL1" class="CHART_DIV_SCROLL2">
                <!-- Trend Delta E  -->
                <div class="CHART_DIV">
                    <div class="CHART_DIV_LEFT">
                        <DCWC:Chart ID="CHRT_TrendDeltaE" runat="server" Width="850px" Height="125px" ImageUrl="~/UploadFiles/temp/CHRT_TrendDeltaE_#SEQ(300,3)" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                            <Legends>
                                <DCWC:Legend Name="Default"></DCWC:Legend>
                                <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom"></DCWC:Legend>
                            </Legends>
                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                            <UI>
                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                                </Toolbar>
                            </UI>
                            <Series>
                                <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series1" Color="Green" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                                    <DCWC:Series Name="Series2" Color="Red" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                                    <DCWC:Series Name="Series3" Color="Blue" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                            </Series>
                            <ChartAreas>
                                <DCWC:ChartArea Name="ChartArea1">
                                    <Position Height="90" Width="98" X="0" Y="10" />
                                    <AxisY Enabled="True">
                                        <MajorGrid LineColor="Beige" />
                                        <SubAxes>
                                            <DCWC:SubAxis Title="Trendline"></DCWC:SubAxis>
                                        </SubAxes>                                        
                                     </AxisY>
                                    <AxisX Enabled="True">
                                        <MajorGrid LineColor="Beige" />                                                          
                                    </AxisX>                                    
                                </DCWC:ChartArea>                                
                            </ChartAreas>
                        </DCWC:Chart>  
                    </div>
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
                </div>
                <!-- Trend Delta E  -->
                <!-- Delta E -->
                <div class="CHART_DIV">
                    <div class="CHART_DIV_LEFT">
                        <DCWC:Chart ID="CHRT_DeltaE" runat="server" Width="850px" Height="125px" ImageUrl="~/UploadFiles/temp/CHRT_DeltaE_#SEQ(300,3)" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                            <Legends>
                                <DCWC:Legend Name="Default"></DCWC:Legend>
                                <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom"></DCWC:Legend>
                            </Legends>
                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                        <UI>
                            <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                            </Toolbar>
                        </UI>
                            <Series>
                                <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series1" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series2" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series3" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series4" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                            </Series>
                            <ChartAreas>
                                <DCWC:ChartArea Name="ChartArea1">
                                    <Position Height="90" Width="98" X="0" Y="10" />
                                    <AxisY Enabled="True">
                                        <MajorGrid LineColor="Beige" />
                                        <SubAxes>
                                            <DCWC:SubAxis Title="&Delta; E"></DCWC:SubAxis>
                                        </SubAxes>                                        
                                     </AxisY>
                                    <AxisX Enabled="True">
                                        <MajorGrid LineColor="Beige" />                                                          
                                    </AxisX>                                    
                                </DCWC:ChartArea>                                
                            </ChartAreas>
                        </DCWC:Chart>  
                    </div>
                    <div class="CHART_DIV_RIGHT">
                        <img src="Images/GreeLeftArror.gif" ID="BTN_DeltaE" runat="server" align="Open List" />
                        <asp:Panel ID="PNL_DeltaE" runat="server" CssClass="popupMenu">
                        <div style="width:216px; float:left;"><div style="height:10px">
                            <span style="width:215px;height:10px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-top-2.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-top-2.png" width="215" height="10" border="0" alt="" /></span>
                        </div>
                        <div style="height:110px; background:url(images/popup-mid.png) top left repeat-y #FFF; padding:3px 16px 15px 10px; color:#666666; font-family:Tahoma, Arial, Helvetica, sans-serif; font-size:11px; text-align:left; vertical-align:top;border-left:#808080;border-right:#808080">
                            <asp:CheckBox ID="CHK_DeltaE_Cyan" runat="server" Text="<label class='deCYAN'>&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Delta E Cyan" Checked="true"   ToolTip="Delta E Cyan" /><br />
                            <asp:CheckBox ID="CHK_DeltaE_Magenta" runat="server" Text="<label class='deMAGENTA'>&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Delta E Magenta" Checked="true"   ToolTip="Delta E Magenta" /><br />
                            <asp:CheckBox ID="CHK_DeltaE_Yellow" runat="server" Text="<label class='deYELLOW'>&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Delta E Yellow" Checked="true"  ToolTip="Delta E Yellow" /><br />
                            <asp:CheckBox ID="CHK_DeltaE_Black" runat="server" Text="<label class='deBLACK'>&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Delta E Black" Checked="true"  ToolTip="Delta E Black" /><br /><br />
                            <asp:Button ID="BTN_DeltaE_Apply" runat="server" CssClass="BUTTON" Text="Apply" OnClick="BTN_DeltaE_Click" />
                            					</div>
                                        <div style="clear:both"></div>
                                        <div style="float:left; height:19px"><span style="width:215px;height:19px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-bottom.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-bottom.png" width="216" height="19" border="0" alt="" /></span></div>
                        </asp:Panel>            
                        <cc1:hovermenuextender ID="HME_DeltaE" runat="Server" 
                            TargetControlID="BTN_DeltaE"  PopupControlID="PNL_DeltaE" 
                            HoverCssClass="popupHover" PopupPosition="Left" OffsetX="0" OffsetY="0" 
                            PopDelay="50" />                        
                    </div>                    
                </div>
                <!-- Delta E -->                
                <!-- Density -->
                <div class="CHART_DIV">
                    <div class="CHART_DIV_LEFT">
                        <DCWC:Chart ID="CHRT_Density" runat="server" Width="850px" Height="125px" ImageUrl="~/UploadFiles/temp/CHRT_Density_#SEQ(300,3)" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                            <Legends>
                                <DCWC:Legend Name="Default"></DCWC:Legend>
                                <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom"></DCWC:Legend>
                            </Legends>
                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                        <UI>
                            <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                            </Toolbar>
                        </UI>
                            <Series>
                                <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series1" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series2" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series3" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series4" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                            </Series>
                            <ChartAreas>
                                <DCWC:ChartArea Name="ChartArea1">
                                    <Position Height="90" Width="98" X="0" Y="10" />
                                    <AxisY Enabled="True">
                                        <MajorGrid LineColor="Beige" />
                                        <SubAxes>
                                            <DCWC:SubAxis Title="Density"></DCWC:SubAxis>
                                        </SubAxes>                                        
                                     </AxisY>
                                    <AxisX Enabled="True">
                                        <MajorGrid LineColor="Beige" />                                                          
                                    </AxisX>                                    
                                </DCWC:ChartArea>                                
                            </ChartAreas>
                        </DCWC:Chart>  
                    </div>
                    <div class="CHART_DIV_RIGHT">
                        <img src="Images/GreeLeftArror.gif" ID="BTN_Density" runat="server" align="Open List" />
                        <asp:Panel ID="PNL_Density" runat="server" CssClass="popupMenu">
                            <div style="width:216px; float:left;"><div style="height:10px">
                            <span style="width:215px;height:10px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-top-2.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-top-2.png" width="215" height="10" border="0" alt="" /></span>
                        </div>
                        <div style="height:110px; background:url(images/popup-mid.png) top left repeat-y #FFF; padding:3px 16px 15px 10px; color:#666666; font-family:Tahoma, Arial, Helvetica, sans-serif; font-size:11px; text-align:left; vertical-align:top;border-left:#808080;border-right:#808080">
                            <asp:CheckBox ID="CHK_Density_Cyan" runat="server" Text="<label class='deCYAN'>&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Density Cyan" Checked="true"   ToolTip="Density Cyan" /><br />
                            <asp:CheckBox ID="CHK_Density_Magenta" runat="server" Text="<label class='deMAGENTA'>&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Density Magenta" Checked="true"   ToolTip="Density Magenta" /><br />
                            <asp:CheckBox ID="CHK_Density_Yellow" runat="server" Text="<label class='deYELLOW'>&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Density Yellow" Checked="true"  ToolTip="Density Yellow" /><br />
                            <asp:CheckBox ID="CHK_Density_Black" runat="server" Text="<label class='deBLACK'>&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Density Black" Checked="true"  ToolTip="Density Black" /><br /><br />
                            <asp:Button ID="BTN_Density_Apply" runat="server" CssClass="BUTTON" Text="Apply" OnClick="BTN_Density_Click" />
                            					</div>
                                        <div style="clear:both"></div>
                                        <div style="float:left; height:19px"><span style="width:215px;height:19px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-bottom.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-bottom.png" width="216" height="19" border="0" alt="" /></span></div>
                        </asp:Panel>            
                        <cc1:hovermenuextender ID="HME_Density" runat="Server" 
                            TargetControlID="BTN_Density"  PopupControlID="PNL_Density" 
                            HoverCssClass="popupHover" PopupPosition="Left" OffsetX="0" OffsetY="0" 
                            PopDelay="50" />                         
                    </div>                    
                </div>
                <!-- Density -->
                <!-- Dotgain -->
                <div class="CHART_DIV">
                    <div class="CHART_DIV_LEFT">
                        <DCWC:Chart ID="CHRT_Dotgain" runat="server" Width="850px" Height="125px" ImageUrl="~/UploadFiles/temp/CHRT_Dotgain_#SEQ(300,3)" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                            <Legends>
                                <DCWC:Legend Name="Default"></DCWC:Legend>
                                <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom"></DCWC:Legend>
                            </Legends>
                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                        <UI>
                            <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                            </Toolbar>
                        </UI>
                            <Series>
                                <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series1" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series2" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series3" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series4" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                            </Series>
                            <ChartAreas>
                                <DCWC:ChartArea Name="ChartArea1">
                                    <Position Height="90" Width="98" X="0" Y="10" />
                                    <AxisY Enabled="True">
                                        <MajorGrid LineColor="Beige" />
                                        <SubAxes>
                                            <DCWC:SubAxis Title="Dotgain"></DCWC:SubAxis>
                                        </SubAxes>                                        
                                     </AxisY>
                                    <AxisX Enabled="True">
                                        <MajorGrid LineColor="Beige" />                                                          
                                    </AxisX>                                    
                                </DCWC:ChartArea>                                
                            </ChartAreas>
                        </DCWC:Chart>  
                    </div>
                    <div class="CHART_DIV_RIGHT">
                        <img src="Images/GreeLeftArror.gif" ID="BTN_Dotgain" runat="server" align="Open List" />
                        <asp:Panel ID="PNL_Dotgain" runat="server" CssClass="popupMenu">
                            <div style="width:216px; float:left;"><div style="height:10px">
                            <span style="width:215px;height:10px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-top-2.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-top-2.png" width="215" height="10" border="0" alt="" /></span>
                        </div>
                            <div style="height:110px; background:url(images/popup-mid.png) top left repeat-y #FFF; padding:3px 16px 15px 10px; color:#666666; font-family:Tahoma, Arial, Helvetica, sans-serif; font-size:11px; text-align:left; vertical-align:top;border-left:#808080;border-right:#808080">
                            <asp:CheckBox ID="CHK_Dotgain_Cyan" runat="server" Text="<label class='deCYAN'>&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Dotgain Cyan" Checked="true"   ToolTip="Dotgain Cyan" /><br />
                            <asp:CheckBox ID="CHK_Dotgain_Magenta" runat="server" Text="<label class='deMAGENTA'>&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Dotgain Magenta" Checked="true"   ToolTip="Dotgain Magenta" /><br />
                            <asp:CheckBox ID="CHK_Dotgain_Yellow" runat="server" Text="<label class='deYELLOW'>&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Dotgain Yellow" Checked="true"  ToolTip="Dotgain Yellow" /><br />
                            <asp:CheckBox ID="CHK_Dotgain_Black" runat="server" Text="<label class='deBLACK'>&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Dotgain Black" Checked="true"  ToolTip="Dotgain Black" /><br /><br />
                            <asp:Button ID="BTN_Dotgain_Apply" runat="server" CssClass="BUTTON" Text="Apply" OnClick="BTN_Dotgain_Click" />
                            					</div>
                                        <div style="clear:both"></div>
                                        <div style="float:left; height:19px"><span style="width:215px;height:19px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-bottom.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-bottom.png" width="216" height="19" border="0" alt="" /></span></div>
                        </asp:Panel>            
                        <cc1:hovermenuextender ID="HME_Dotgain" runat="Server" 
                            TargetControlID="BTN_Dotgain"  PopupControlID="PNL_Dotgain" 
                            HoverCssClass="popupHover" PopupPosition="Left" OffsetX="0" OffsetY="0" 
                            PopDelay="50" />                         
                    </div>                    
                </div>
                <!-- Dotgain -->
                                 <!-- PrintContrast -->
                <div class="CHART_DIV">
                    <div class="CHART_DIV_LEFT">
                        <DCWC:Chart ID="CHRT_PrintContrast" runat="server" Width="850px" Height="125px" ImageUrl="~/UploadFiles/temp/CHRT_PrintContrast_#SEQ(300,3)" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                            <Legends>
                                <DCWC:Legend Name="Default"></DCWC:Legend>
                                <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom"></DCWC:Legend>
                            </Legends>
                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                        <UI>
                            <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                            </Toolbar>
                        </UI>
                            <Series>
                                <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series1" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series2" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series3" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series4" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false"></DCWC:Series>
                            </Series>
                            <ChartAreas>
                                <DCWC:ChartArea Name="ChartArea1">
                                    <Position Height="90" Width="98" X="0" Y="10" />
                                    <AxisY Enabled="True">
                                        <MajorGrid LineColor="Beige" />
                                        <SubAxes>
                                            <DCWC:SubAxis Title="PrintContrast"></DCWC:SubAxis>
                                        </SubAxes>      
                                     </AxisY>
                                    <AxisX Enabled="True">
                                        <MajorGrid LineColor="Beige" />                                                          
                                    </AxisX>                                    
                                </DCWC:ChartArea>                                
                            </ChartAreas>
                        </DCWC:Chart>  
                    </div>
                    <div class="CHART_DIV_RIGHT">
                    <img src="Images/GreeLeftArror.gif" ID="BTN_PrintContrast" runat="server" align="Open List" />
                        <asp:Panel ID="PNL_PrintContrast" runat="server" CssClass="popupMenu">
                            <div style="width:216px; float:left;"><div style="height:10px">
                            <span style="width:215px;height:10px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-top-2.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-top-2.png" width="215" height="10" border="0" alt="" /></span>
                        </div>
                        <div style="height:110px; background:url(images/popup-mid.png) top left repeat-y #FFF; padding:3px 16px 15px 10px; color:#666666; font-family:Tahoma, Arial, Helvetica, sans-serif; font-size:11px; text-align:left; vertical-align:top;border-left:#808080;border-right:#808080">
                            <asp:CheckBox ID="CHK_PrintContrast_Cyan" runat="server" Text="<label class='deCYAN'>&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Print Contrast Cyan" Checked="true"   ToolTip="Print Contrast Cyan" /><br />
                            <asp:CheckBox ID="CHK_PrintContrast_Magenta" runat="server" Text="<label class='deMAGENTA'>&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Print Contrast Magenta" Checked="true"   ToolTip="Print Contrast Magenta" /><br />
                            <asp:CheckBox ID="CHK_PrintContrast_Yellow" runat="server" Text="<label class='deYELLOW'>&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Print Contrast Yellow" Checked="true"  ToolTip="Print Contrast Yellow" /><br />
                            <asp:CheckBox ID="CHK_PrintContrast_Black" runat="server" Text="<label class='deBLACK'>&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Print Contrast Black" Checked="true"  ToolTip="Print Contrast Black" /><br /><br />
                            <asp:Button ID="BTN_PrintContrast_Apply" runat="server" CssClass="BUTTON" Text="Apply" OnClick="BTN_PrintContrast_Click" />
                					</div>
                            <div style="clear:both"></div>
                            <div style="float:left; height:19px"><span style="width:215px;height:19px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-bottom.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-bottom.png" width="216" height="19" border="0" alt="" /></span></div>
                        </asp:Panel>            
                        <cc1:hovermenuextender ID="HME_PrintContrast" runat="Server" 
                            TargetControlID="BTN_PrintContrast"  PopupControlID="PNL_PrintContrast" 
                            HoverCssClass="popupHover" PopupPosition="Left" OffsetX="0" OffsetY="0" 
                            PopDelay="50" />                         
                    </div>                    
                </div>
                <!-- PrintContrast -->
            </div>
        </td>
    </tr>
</table>
<script language="javascript">
if( screen.width <= 1024 )
    {
        document.getElementById("CHART_DIV_SCROLL1").style.height = "450px";
    }
</script>
<asp:Literal ID="LblError" runat="server"></asp:Literal>
<asp:Literal ID="LTRL_CPDeviceID" runat="server" Visible="false">0</asp:Literal>
<asp:Literal ID="LTRL_JobID" runat="server" Visible="false">0</asp:Literal>
<asp:Literal ID="LTRL_DongleID" runat="server" Visible="false">0</asp:Literal>
<asp:Literal ID="LTRL_SheetNumber" runat="server" Visible="false">0</asp:Literal>
<asp:Literal ID="LTRL_PageSize" runat="server" Visible="false">50</asp:Literal>
<asp:Literal ID="LTRL_CurrentPage" runat="server" Visible="false">-1</asp:Literal>
<asp:Literal ID="LTRL_StartIndex" runat="server" Visible="false">0</asp:Literal>
<asp:Literal ID="LTRL_KeyTrend" runat="server" Visible="false">0</asp:Literal>
<asp:Literal ID="LTRL_KeyJobSheetSummary" runat="server" Visible="false"></asp:Literal>
</ContentTemplate></asp:UpdatePanel>            
</asp:Content>
