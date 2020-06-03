<%@ Page Language="C#" MasterPageFile="~/MasterDeviceTrendLine.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="ColorTrend.aspx.cs" Inherits="ColorTrend" %>


<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<script language="javascript">
    function PageRedirect( CPMeasurementGUID , CPDevicePrintTargetID , CPDeviceID )
    {
        var sUrl = "ColorTrend.aspx?CPMeasurementGUID="+ CPMeasurementGUID +"&CPDevicePrintTargetID="+ CPDevicePrintTargetID +"&CPDeviceID="+ CPDeviceID ;
        <%if( Request.QueryString["ReportType"] != null ){%> sUrl = sUrl + "&ReportType=D"; <% }%>
        <%if( Request.QueryString["ModalDialog"] != null ){%> sUrl = sUrl + "&ModalDialog=Y"; <% }%>
        
        window.location.href = sUrl ;
    }
    function Show( CTRL )
    {
        document.getElementById("DIV_"+ CTRL ).style.display = "";
    }
    function Hide( CTRL )
    {
        document.getElementById("DIV_"+ CTRL ).style.display = "none";
    }    
</script>
<table cellpadding="0" cellspacing="5" border="0" style="width:920px">
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
                </asp:DropDownList>&nbsp;|&nbsp;Go to page&nbsp;<asp:DropDownList ID="DDL_GoToPage" runat="server" CssClass="DROPDOWNLIST2" AutoPostBack="true" OnSelectedIndexChanged="DDL_GoToPage_SelectedIndexChanged"></asp:DropDownList>&nbsp;<asp:Literal ID="LTRL_PrintTargetTitle" runat="server">|&nbsp;Print&nbsp;Target:</asp:Literal>&nbsp;<asp:DropDownList ID="DDL_PrintTarget" runat="server" CssClass="DROPDOWNLIST2" AutoPostBack="true" OnSelectedIndexChanged="DDL_PrintTarget_OnSelectedIndexChanged"></asp:DropDownList>
            </div>
            <br />
            <div class="ERROR"><asp:Literal ID="LblError" runat="server"></asp:Literal></div>
            <div>
                <DCWC:Chart ID="CHRT_Date" runat="server" Width="850px" Height="40px" ImageUrl="~/UploadFiles/temp/CHRT_Date_#SEQ(300,3)" ImageType="Jpeg" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                    <Legends>
                        <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom"></DCWC:Legend>
                    </Legends>
                    <Series>
                        <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                        <DCWC:Series Name="Series1" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                    </Series>
                    <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                            <UI>
                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                                </Toolbar>
                            </UI>
                    <ChartAreas>
                        <DCWC:ChartArea Name="ChartArea1" BackColor="Transparent">                            
                            <Position Height="60" Width="99" X="5" Y="0" />
                            <AxisY LineColor="Transparent" Enabled="True">
                                <MinorGrid LineColor="Transparent" Interval="1" />
                                <MajorGrid LineColor="Transparent" />
                                <LabelStyle FontColor="Transparent" />                           
                            </AxisY>
                            <AxisX Enabled="True" LineColor="Transparent">
                                <MinorGrid LineColor="Transparent" />
                                <MajorGrid LineColor="Transparent" />                                
                            </AxisX>
                        </DCWC:ChartArea>
                    </ChartAreas>                
                </DCWC:Chart>
            </div>
            <div style="clear:both"></div>
            <div id="CHART_DIV_SCROLL" class="CHART_DIV_SCROLL">
                <!-- Delta E -->
                <div class="CHART_DIV">
                    <div class="CHART_DIV_LEFT">
                        <DCWC:Chart ID="CHRT_DeltaE" runat="server" Width="850px" Height="125px"  ImageUrl="~/UploadFiles/temp/CHRT_DeltaE_#SEQ(300,3)" ImageType="Jpeg" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                            <Legends>
                                <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom"></DCWC:Legend>
                            </Legends>
                            <Titles>
                                <DCWC:Title Text="Delta E" Visible="false"></DCWC:Title>
                            </Titles>                
                            <Series>
                                <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series1" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series2" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series3" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series4" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series5" Color="150, 050, 050, 050" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series6" Color="150, 255, 200, 100" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series7" Color="150, 000, 255, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series8" Color="150, 255, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"> </DCWC:Series>
                                <DCWC:Series Name="Series9" Color="150, 000, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>                                
                            </Series>
                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                            <UI>
                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                    <BorderSkin FrameBackGradientEndColor="Gray" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                                </Toolbar>
                            </UI>
                            <ChartAreas>
                                <DCWC:ChartArea Name="ChartArea1">
                                    <Position Height="100" Width="99" X="0" Y="10" />
                                    <AxisY Enabled="True">
                                        <MajorGrid LineColor="Beige" />
                                        <SubAxes>
                                            <DCWC:SubAxis Title="&Delta; E"></DCWC:SubAxis>
                                        </SubAxes>
                                    </AxisY>
                                    <AxisX Enabled="True">                                        
                                        <LabelStyle FontColor="Transparent" />
                                        <MajorGrid LineColor="Beige"/>                  
                                    </AxisX>
                                </DCWC:ChartArea>
                            </ChartAreas>                           
                        </DCWC:Chart>
                    </div>
                    <img src="Images/GreeLeftArror.gif" ID="BTN_DeltaE" runat="server" align="Open List" />
                    <div class="CHART_DIV_RIGHT">
                        <asp:Panel ID="PNL_DeltaE" runat="server" CssClass="popupMenu">
                        <div style="width:216px; float:left;"><div style="height:10px">
                            <span style="width:215px;height:10px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-top-2.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-top-2.png" width="215" height="10" border="0" alt="" /></span>
                        </div>
                        <div style="height:200px; background:url(images/popup-mid.png) top left repeat-y #FFF; padding:3px 16px 15px 10px; color:#666666; font-family:Tahoma, Arial, Helvetica, sans-serif; font-size:11px; text-align:left; vertical-align:top;border-left:#808080;border-right:#808080">
                            <asp:CheckBox ID="CHK_DeltaE_Cyan" runat="server" Text="<label class='deCYAN'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;de Cyan" Checked="true"   ToolTip="de Cyan" /><br />
                            <asp:CheckBox ID="CHK_DeltaE_Magenta" runat="server" Text="<label class='deMAGENTA'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;de Magenta" Checked="true"   ToolTip="de Magenta" /><br />
                            <asp:CheckBox ID="CHK_DeltaE_Yellow" runat="server" Text="<label class='deYELLOW'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;de Yellow" Checked="true"  ToolTip="de Yellow" /><br />
                            <asp:CheckBox ID="CHK_DeltaE_Black" runat="server" Text="<label class='deBLACK'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;de Black" Checked="true"  ToolTip="de Black" /><br />
                            <asp:CheckBox ID="CHK_DeltaE_PaperWhite" runat="server" Text="<label class='dePAPERWHITE'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;de Paper White" Checked="true"  ToolTip="de Paper White" /><br />
                            <asp:CheckBox ID="CHK_DeltaE_Average" runat="server" Text="<label class='deAVERAGE'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;de Average" Checked="true"  ToolTip="de Average" /><br />
                            <asp:CheckBox ID="CHK_DeltaE_Maximum" runat="server" Text="<label class='deMAXIMUM'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;de Maximum" Checked="true"  ToolTip="de Maximum" /><br />
                            <asp:CheckBox ID="CHK_DeltaE_Primary" runat="server" Text="<label class='dePRIMARY'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;de Primary" Checked="true"  ToolTip="de Primary" /><br />
                            <asp:CheckBox ID="CHK_DeltaE_Gray" runat="server" Text="<label class='deGRAY'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;de Gray" Checked="true"  ToolTip="de Gray" /><br /><br />
                            <asp:Button ID="BTN_DeltaE_Apply" runat="server" CssClass="BUTTON" Text="Apply" OnClick="BTN_DeltaE_Click" />
                        </div>
                        <div style="clear:both"></div>
                         <div style="float:left; height:19px">
                            <span style="width:215px;height:19px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-bottom.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-bottom.png" width="216" height="19" border="0" alt="" /></span>
                         </div>                           
                        </asp:Panel>            
                        <cc1:HoverMenuExtender ID="HME_DeltaE" runat="Server" TargetControlID="BTN_DeltaE"  PopupControlID="PNL_DeltaE" HoverCssClass="popupHover" PopupPosition="Left" OffsetX="0" OffsetY="0" PopDelay="50" /> 
                        
                    </div>
                </div>
                <!-- Delta E -->
                <div style="clear:both"></div>
                <!-- Density -->
                <div class="CHART_DIV">
                    <div class="CHART_DIV_LEFT">
                        <DCWC:Chart ID="CHRT_Density" runat="server" Width="850px" Height="150px" ImageUrl="~/UploadFiles/temp/CHRT_Density_#SEQ(300,3)" ImageType="Jpeg" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                            <Legends>
                                <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom"></DCWC:Legend>
                            </Legends>
                            <Titles>
                                <DCWC:Title Text="Density" Visible="false"></DCWC:Title>
                            </Titles>                
                            <Series>
                                <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series1" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series2" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series3" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series4" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>                    
                            </Series>
                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                            <UI>
                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                                </Toolbar>
                            </UI>
                            <ChartAreas>
                                <DCWC:ChartArea Name="ChartArea1">
                                   <Position Height="100" Width="99" X="0" Y="10" />
                                    <AxisY Enabled="True" Minimum="0" Maximum="2">
                                        <MajorGrid LineColor="Beige" Interval="0.5" />
                                        <SubAxes>
                                            <DCWC:SubAxis Title="Density"></DCWC:SubAxis>
                                        </SubAxes>
                                    </AxisY>
                                    <AxisX Enabled="True">
                                        <LabelStyle FontColor="Transparent" />
                                        <MajorGrid LineColor="Beige"/>                            
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
                        <div style="height:90px; background:url(images/popup-mid.png) top left repeat-y #FFF; padding:3px 16px 15px 10px; color:#666666; font-family:Tahoma, Arial, Helvetica, sans-serif; font-size:11px; text-align:left; vertical-align:top;border-left:#808080;border-right:#808080">
                            <asp:CheckBox ID="CHK_Density_Cyan" runat="server" Text="<label class='deCYAN'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Cyan" Checked="true"   ToolTip="Cyan" /><br />
                            <asp:CheckBox ID="CHK_Density_Magenta" runat="server" Text="<label class='deMAGENTA'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Magenta" Checked="true"   ToolTip="Magenta" /><br />
                            <asp:CheckBox ID="CHK_Density_Yellow" runat="server" Text="<label class='deYELLOW'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Yellow" Checked="true"  ToolTip="Yellow" /><br />
                            <asp:CheckBox ID="CHK_Density_Black" runat="server" Text="<label class='deBLACK'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Black" Checked="true"  ToolTip="Black" /><br /><br />
                            <asp:Button ID="BTN_Density_Apply" runat="server" CssClass="BUTTON" Text="Apply" OnClick="BTN_Density_Click" />
                            </div>
                        <div style="clear:both"></div>
                         <div style="float:left; height:19px">
                            <span style="width:215px;height:19px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-bottom.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-bottom.png" width="216" height="19" border="0" alt="" /></span>
                         </div>
                        </asp:Panel>
                        <cc1:HoverMenuExtender ID="HME_Density" runat="Server" TargetControlID="BTN_Density"  PopupControlID="PNL_Density" HoverCssClass="popupHover" PopupPosition="Left" OffsetX="0" OffsetY="0" PopDelay="50" />                         
                    </div>
                </div>                
                <!-- Density -->
                <div style="clear:both"></div>
                <!-- Dotgain -->
                <div class="CHART_DIV">
                    <div class="CHART_DIV_LEFT">
                        <DCWC:Chart ID="CHRT_DotGain" runat="server" Width="850px" Height="150px" ImageUrl="~/UploadFiles/temp/CHRT_Dotgain_#SEQ(300,3)" ImageType="Jpeg" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                            <Legends>
                                <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom"></DCWC:Legend>
                            </Legends>
                            <Titles>
                                <DCWC:Title Text="DotGain" Visible="false"></DCWC:Title>
                            </Titles>                
                            <Series>
                                <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                
                                <DCWC:Series Name="Cyan0" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Magenta0" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Yellow0" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Black0" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                
                                
                                <DCWC:Series Name="Cyan1" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Magenta1" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Yellow1" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Black1" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                
                                <DCWC:Series Name="Cyan2" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Magenta2" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Yellow2" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Black2" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                
                                <DCWC:Series Name="Cyan3" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Magenta3" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Yellow3" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Black3" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>


                                <DCWC:Series Name="Cyan4" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Magenta4" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Yellow4" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Black4" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>

                                <DCWC:Series Name="Cyan5" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Magenta5" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Yellow5" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Black5" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>

                                <DCWC:Series Name="Cyan6" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Magenta6" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Yellow6" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Black6" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>

                                <DCWC:Series Name="Cyan7" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Magenta7" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Yellow7" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Black7" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>

                            </Series>
                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                            <UI>
                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                                </Toolbar>
                            </UI>
                            <ChartAreas>                    
                                <DCWC:ChartArea Name="ChartArea1">
                                    <Position Height="100" Width="99" X="0" Y="10" />
                                     <AxisY Enabled="True"  Minimum="-25" Maximum="25">
                                        <MajorGrid LineColor="Beige" />
                                        <SubAxes>
                                            <DCWC:SubAxis Title="Dotgain"></DCWC:SubAxis>
                                        </SubAxes>
                                    </AxisY>
                                    <AxisX Enabled="True">
                                        <LabelStyle FontColor="Transparent" />
                                        <MajorGrid LineColor="Beige"/>                            
                                    </AxisX>                                       
                                </DCWC:ChartArea>
                            </ChartAreas>
                            </ChartAreas>
                        </DCWC:Chart>
                    </div>
                    <div class="CHART_DIV_RIGHT">
                        <img src="Images/GreeLeftArror.gif" ID="BTN_DotGain" runat="server" align="Open List" />
                        <asp:Panel ID="PNL_DotGain" runat="server" CssClass="popupMenu">            
                        <div style="width:216px; float:left;"><div style="height:10px">
                            <span style="width:215px;height:10px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-top-2.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-top-2.png" width="215" height="10" border="0" alt="" /></span>
                        </div>
                        <div style="height:300px; background:url(images/popup-mid.png) top left repeat-y #FFF; padding:3px 16px 15px 10px; color:#666666; font-family:Tahoma, Arial, Helvetica, sans-serif; font-size:11px; text-align:left; vertical-align:top;border-left:#808080;border-right:#808080">
                            <asp:CheckBox ID="CHK_DotGain_Cyan0" runat="server" Text="<label class='deCYAN'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Cyan Quarter Tone" Checked="true"   /><br />
                            <asp:CheckBox ID="CHK_DotGain_Magenta0" runat="server" Text="<label class='deMAGENTA'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Magenta Quarter Tone" Checked="true" /><br />
                            <asp:CheckBox ID="CHK_DotGain_Yellow0" runat="server" Text="<label class='deYELLOW'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Yellow Quarter Tone" Checked="true" /><br />
                            <asp:CheckBox ID="CHK_DotGain_Black0" runat="server" Text="<label class='deBLACK'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Black Quarter Tone" Checked="true" /><br />
                            <hr width="150px" />          
                            <asp:CheckBox ID="CHK_DotGain_Cyan1" runat="server" Text="<label class='deCYAN'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Cyan Mid Tone" Checked="true" /><br />
                            <asp:CheckBox ID="CHK_DotGain_Magenta1" runat="server" Text="<label class='deMAGENTA'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Magenta Mid Tone" Checked="true" /><br />
                            <asp:CheckBox ID="CHK_DotGain_Yellow1" runat="server" Text="<label class='deYELLOW'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Yellow Mid Tone" Checked="true" /><br />
                            <asp:CheckBox ID="CHK_DotGain_Black1" runat="server" Text="<label class='deBLACK'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Black Mid Tone" Checked="true" /><br />
                            <hr width="150px" />                          
                            <asp:CheckBox ID="CHK_DotGain_Cyan2" runat="server" Text="<label class='deCYAN'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Cyan Three Quarter Tone" Checked="true" /><br />
                            <asp:CheckBox ID="CHK_DotGain_Magenta2" runat="server" Text="<label class='deMAGENTA'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Magenta Three Quarter Tone" Checked="true" /><br />
                            <asp:CheckBox ID="CHK_DotGain_Yellow2" runat="server" Text="<label class='deYELLOW'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Yellow Three Quarter Tone" Checked="true" /><br />
                            <asp:CheckBox ID="CHK_DotGain_Black2" runat="server" Text="<label class='deBLACK'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Black Three Quarter Tone" Checked="true"/><br /><br />
                            <asp:Button ID="BTN_DotGain_Apply" runat="server" CssClass="BUTTON" Text="Apply" OnClick="BTN_DotGain_Click" />
                             </div>
                        <div style="clear:both"></div>
                         <div style="float:left; height:19px">
                            <span style="width:215px;height:19px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-bottom.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-bottom.png" width="216" height="19" border="0" alt="" /></span>
                         </div>
                        </asp:Panel>
                        <cc1:HoverMenuExtender ID="HME_DotGain" runat="Server" TargetControlID="BTN_DotGain"  PopupControlID="PNL_DotGain" HoverCssClass="popupHover" PopupPosition="Left" OffsetX="0" OffsetY="0" PopDelay="50" />                         
                    </div>
                </div>
                <!-- Dotgain -->
                <div style="clear:both"></div>
                <!-- Print Contrast -->
                <div class="CHART_DIV">
                    <div class="CHART_DIV_LEFT">
                        <DCWC:Chart ID="CHRT_PrintContrast" runat="server" Width="850px" Height="150px" ImageUrl="~/UploadFiles/temp/CHRT_PrintContrast_#SEQ(300,3)"  ImageType="Jpeg" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                            <Legends>
                                <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom"></DCWC:Legend>
                            </Legends>
                            <Titles>
                                <DCWC:Title Text="Print Contrast" Visible="false"></DCWC:Title>
                            </Titles>                
                            <Series>
                                <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series1" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series2" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series3" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series4" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>                    
                            </Series>
                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                            <UI>
                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                                </Toolbar>
                            </UI>
                            <ChartAreas>                    
                                <DCWC:ChartArea Name="ChartArea1">
                                    <Position Height="100" Width="99" X="0" Y="10" />
                                    <AxisY Enabled="True"  Interval="10" Minimum="0" Maximum="50" >
                                        <MajorGrid LineColor="Beige" />
                                        <SubAxes>
                                            <DCWC:SubAxis Title="Print Contrast"></DCWC:SubAxis>
                                        </SubAxes>
                                    </AxisY>
                                    <AxisX Enabled="True">
                                        <LabelStyle FontColor="Transparent" />
                                        <MajorGrid LineColor="Beige"/>                            
                                    </AxisX>   
                                </DCWC:ChartArea>
                            </ChartAreas>
                            </ChartAreas>
                        </DCWC:Chart>
                    </div>
                    <div class="CHART_DIV_RIGHT">
                        <img src="Images/GreeLeftArror.gif" ID="BTN_PrintContrast" runat="server" align="Open List" />
                        <asp:Panel ID="PNL_PrintContrast" runat="server" CssClass="popupMenu">
                          <div style="width:216px; float:left;"><div style="height:10px">
                            <span style="width:215px;height:10px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-top-2.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-top-2.png" width="215" height="10" border="0" alt="" /></span>
                        </div>
                        <div style="height:90px; background:url(images/popup-mid.png) top left repeat-y #FFF; padding:3px 16px 15px 10px; color:#666666; font-family:Tahoma, Arial, Helvetica, sans-serif; font-size:11px; text-align:left; vertical-align:top;border-left:#808080;border-right:#808080">
                            <asp:CheckBox ID="CHK_PrintContrast_Cyan" runat="server" Text="<label class='deCYAN'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Cyan" Checked="true"   ToolTip="Cyan" /><br />
                            <asp:CheckBox ID="CHK_PrintContrast_Magenta" runat="server" Text="<label class='deMAGENTA'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Magenta" Checked="true"   ToolTip="Magenta" /><br />
                            <asp:CheckBox ID="CHK_PrintContrast_Yellow" runat="server" Text="<label class='deYELLOW'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Yellow" Checked="true"  ToolTip="Yellow" /><br />
                            <asp:CheckBox ID="CHK_PrintContrast_Black" runat="server" Text="<label class='deBLACK'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Black" Checked="true"  ToolTip="Black" /><br /><br />
                            <asp:Button ID="BTN_PrintContrast_Apply" runat="server" CssClass="BUTTON" Text="Apply" OnClick="BTN_PrintContrast_Click" />
                              </div>
                        <div style="clear:both"></div>
                         <div style="float:left; height:19px">
                            <span style="width:215px;height:19px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-bottom.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-bottom.png" width="216" height="19" border="0" alt="" /></span>
                         </div>
                        </asp:Panel>
                        <cc1:HoverMenuExtender ID="HME_PrintContrast" runat="Server" TargetControlID="BTN_PrintContrast"  PopupControlID="PNL_PrintContrast" HoverCssClass="popupHover" PopupPosition="Left" OffsetX="0" OffsetY="0" PopDelay="50" />                         
                    </div>
                </div>
                <!-- Print Contrast -->
                <div style="clear:both"></div>
                <!-- Ink -->
                <div class="CHART_DIV">
                    <div class="CHART_DIV_LEFT">
                        <DCWC:Chart ID="CHRT_Ink" runat="server" Width="850px" Height="150px"  ImageType="Jpeg" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080" ImageUrl="~/UploadFiles/temp/CHRT_Ink_#SEQ(300,3)">
                            <Legends>
                                <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom"></DCWC:Legend>
                            </Legends>
                            <Titles>
                                <DCWC:Title Text="Ink Levels" Visible="false"></DCWC:Title>
                            </Titles>                
                            <Series>
                                <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series1" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series2" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series3" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series4" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series5" Color="150, 255, 200, 100" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series6" Color="150, 000, 255, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series7" Color="150, 255, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"> </DCWC:Series>
                                <DCWC:Series Name="Series8" Color="150, 000, 000, 255" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series9" Color="150, 130, 255, 50" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                            </Series>
                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                            <UI>
                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                                </Toolbar>
                            </UI>
                            <ChartAreas>
                                <DCWC:ChartArea Name="ChartArea1">
                                    <Position Height="100" Width="99" X="0" Y="10" />
                                    <AxisY Enabled="True" Minimum="0" Maximum="1">
                                        <MajorGrid LineColor="Beige" Interval="0.2" />
                                         <SubAxes>
                                            <DCWC:SubAxis Title="Ink Levels"></DCWC:SubAxis>
                                        </SubAxes>
                                    </AxisY>
                                    <AxisX Enabled="True">
                                        <LabelStyle FontColor="Transparent" />
                                        <MajorGrid LineColor="Beige" />                            
                                    </AxisX>                                      
                                </DCWC:ChartArea>
                            </ChartAreas>
                            </ChartAreas>
                        </DCWC:Chart>
                    </div>
                    <div class="CHART_DIV_RIGHT">
                    <img src="Images/GreeLeftArror.gif" ID="BTN_Ink" runat="server" align="Open List" />
                        <asp:Panel ID="PNL_Ink" runat="server" CssClass="popupMenu">
                         <div style="width:216px; float:left;"><div style="height:10px">
                            <span style="width:215px;height:10px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-top-2.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-top-2.png" width="215" height="10" border="0" alt="" /></span>
                        </div>
                        <div style="height:115px; background:url(images/popup-mid.png) top left repeat-y #FFF; padding:3px 16px 15px 10px; color:#666666; font-family:Tahoma, Arial, Helvetica, sans-serif; font-size:11px; text-align:left; vertical-align:top;border-left:#808080;border-right:#808080">
                            <asp:CheckBox ID="CHK_Ink_1" runat="server" Text="<label class='deCYAN'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Ink 1" Checked="true"   />&nbsp;<asp:CheckBox ID="CHK_Ink_2" runat="server" Text="<label class='deMAGENTA'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Ink 2" Checked="true"   /><br />
                            <asp:CheckBox ID="CHK_Ink_3" runat="server" Text="<label class='deYELLOW'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Ink 3" Checked="true"  />&nbsp;<asp:CheckBox ID="CHK_Ink_4" runat="server" Text="<label class='deBLACK'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Ink 4" Checked="true"  /><br />
                            <asp:CheckBox ID="CHK_Ink_5" runat="server" Text="<label class='dePAPERWHITE'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Ink 5" Checked="true"   />&nbsp;<asp:CheckBox ID="CHK_Ink_6" runat="server" Text="<label class='deAVERAGE'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Ink 6" Checked="true"   /><br />
                            <asp:CheckBox ID="CHK_Ink_7" runat="server" Text="<label class='deMAXIMUM'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Ink 7" Checked="true"  />&nbsp;<asp:CheckBox ID="CHK_Ink_8" runat="server" Text="<label class='dePRIMARY'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Ink 8" Checked="true"  /><br />
                            <asp:CheckBox ID="CHK_Ink_9" runat="server" Text="<label class='deLAST'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Ink 9" Checked="true"   /><br />
                            <asp:Button ID="BTN_Ink_Apply" runat="server" CssClass="BUTTON" Text="Apply" OnClick="BTN_Ink_Click" />
                            </div>
                        <div style="clear:both"></div>
                         <div style="float:left; height:19px">
                            <span style="width:215px;height:19px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-bottom.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-bottom.png" width="216" height="19" border="0" alt="" /></span>
                         </div>
                        </asp:Panel>
                        <cc1:HoverMenuExtender ID="HME_Ink" runat="Server" TargetControlID="BTN_Ink"  PopupControlID="PNL_Ink" HoverCssClass="popupHover" PopupPosition="Left" OffsetX="0" OffsetY="0" PopDelay="50" />                         
                    </div>
                </div>
                <!-- Ink -->
                <div style="clear:both"></div>
                <!-- Temperature Humidity -->
                <div class="CHART_DIV">
                    <div class="CHART_DIV_LEFT">
                        <DCWC:Chart ID="CHRT_TemperatureHumidity" runat="server" Width="845px" Height="150px"  ImageType="Jpeg" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080" ImageUrl="~/UploadFiles/temp/CHRT_TemperatureHumidity_#SEQ(300,3)">
                            <Legends>
                                <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom"></DCWC:Legend>
                            </Legends>
                            <Titles>
                                <DCWC:Title Text="Temperature Humidity" Visible="false"></DCWC:Title>
                            </Titles>                
                            <Series>
                                <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series1" Color="150, 000, 255, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                                <DCWC:Series Name="Series2" Color="150, 255, 000, 000" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                            </Series>
                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                            <UI>
                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                                </Toolbar>
                            </UI>
                            <ChartAreas>
                                <DCWC:ChartArea Name="ChartArea1">
                                   <Position Height="90" Width="99" X="0.1" Y="10" />
                                    <AxisY>
                                        <MajorGrid LineColor="Beige"/>
                                         <SubAxes>
                                            <DCWC:SubAxis Title="Temp. Humidity"></DCWC:SubAxis>
                                        </SubAxes>
                                    </AxisY>
                                    <AxisX>
                                        <MajorGrid LineColor="Beige" />
                                    </AxisX>
                                </DCWC:ChartArea>
                            </ChartAreas>
                            </ChartAreas>
                        </DCWC:Chart>
                    </div>
                    <div class="CHART_DIV_RIGHT">&nbsp;</div>
                </div>
                <!-- Temperature Humidity -->                
            </div>
        </td>
    </tr>
</table>
<script language="javascript">
if( screen.width <= 1024 )
    {
        document.getElementById("CHART_DIV_SCROLL").style.height = "450px";
    }
</script>
<asp:Literal ID="LTRL_KeyGuid" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="LTRL_KeyDeltaEChartWithDensity" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="LTRL_CPMeasurementGUID" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="LTRL_CPMeasurementGUIDs" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="LTRL_StatuSs" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="LTRL_CPDeviceID" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="LTRL_CPDevicePrintTargetID" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="LTRL_CPDeviceName" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="LTRL_PageSize" runat="server" Visible="false">50</asp:Literal>
<asp:Literal ID="LTRL_CurrentPage" runat="server" Visible="false">-1</asp:Literal>
<asp:Literal ID="LTRL_StartIndex" runat="server" Visible="false">0</asp:Literal>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

