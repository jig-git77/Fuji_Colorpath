<%@ Page Language="C#" AutoEventWireup="true"  EnableEventValidation="false" MasterPageFile="~/MasterDeviceTrendLinePlate.master"  CodeFile="PlateTrend.aspx.cs" Inherits="PlateTrend" %>

<%@ Register assembly="DundasWebChart" namespace="Dundas.Charting.WebControl" tagprefix="DCWC" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<script language="javascript">
  function PageRedirect( CPMeasurementGUID , CPDevicePrintTargetID , CPDeviceID )
    {
        var sUrl = "PlateTrend.aspx?CPMeasurementGUID="+ CPMeasurementGUID +"&CPDevicePrintTargetID="+ CPDevicePrintTargetID +"&CPDeviceID="+ CPDeviceID ;
        <%if( Request.QueryString["ReportType"] != null ){%> sUrl = sUrl + "&ReportType=D"; <% }%>
        <%if( Request.QueryString["ModalDialog"] != null ){%> sUrl = sUrl + "&ModalDialog=Y"; <% }%>
        window.location.href = sUrl;
    }
</script>
<table cellpadding="0" cellspacing="5" border="0">    
    <tr><td>&nbsp;</td></tr>
    <tr>
        <td colspan="2"><div>
                Record(s)&nbsp;<asp:Literal ID="LTRL_StartRecord" runat="server">0</asp:Literal>&nbsp;to&nbsp;<asp:Literal ID="LTRL_EndRecord" runat="server">0</asp:Literal>&nbsp;of&nbsp;<asp:Literal ID="LTRL_TotalRec" runat="server">0</asp:Literal>&nbsp;|&nbsp;<asp:ImageButton ID="IBTN_FirstPage" runat="server" ImageUrl="Images/Nav_FirstPage_off.gif" OnClick="IBTN_FirstPage_Click" />&nbsp;<asp:ImageButton ID="IBTN_PreviousPage" runat="server" ImageUrl="Images/Nav_PreviousPage_off.gif" OnClick="IBTN_PreviousPage_Click" />&nbsp;<asp:ImageButton ID="IBTN_NextPage" runat="server" ImageUrl="Images/Nav_NextPage_off.gif" OnClick="IBTN_NextPage_Click" />&nbsp;<asp:ImageButton ID="IBTN_LastPage" runat="server" ImageUrl="Images/Nav_LastPage_off.gif" OnClick="IBTN_LastPage_Click" />&nbsp;|&nbsp;Records&nbsp;<asp:DropDownList ID="DDL_PageSize" runat="server" CssClass="DROPDOWNLIST2" AutoPostBack="true" OnSelectedIndexChanged="DDL_PageSize_SelectedIndexChanged">
                <asp:ListItem Value="5">5</asp:ListItem>
                <asp:ListItem Value="10">10</asp:ListItem>
                <asp:ListItem Value="25">25</asp:ListItem>
                <asp:ListItem Value="50" Selected="True">50</asp:ListItem>
                <asp:ListItem Value="100">100</asp:ListItem>
                <asp:ListItem Value="500">500</asp:ListItem>
                <asp:ListItem Value="1000">1000</asp:ListItem>
                </asp:DropDownList>&nbsp;|&nbsp;Go to page&nbsp;<asp:DropDownList ID="DDL_GoToPage" runat="server" CssClass="DROPDOWNLIST2" AutoPostBack="true" OnSelectedIndexChanged="DDL_GoToPage_SelectedIndexChanged"></asp:DropDownList>&nbsp;<asp:Literal ID="LTRL_PrintTargetTitle" runat="server">|&nbsp;Print&nbsp;Target:</asp:Literal>&nbsp;<asp:DropDownList ID="DDL_PrintTarget" runat="server" CssClass="DROPDOWNLIST2" AutoPostBack="true" OnSelectedIndexChanged="DDL_PrintTarget_OnSelectedIndexChanged"></asp:DropDownList>
    </tr>
    <tr><td>&nbsp;</td></tr>
    <tr>
        <td align="center" style="width:850px">
            <DCWC:Chart ID="CHRT_Deviation" runat="server" Width="875px" Height="250px" ImageUrl="~/UploadFiles/temp/CHRT_Deviation_#SEQ(300,3)" AJAXZoomEnabled="true"  ImageType="Jpeg" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                <Legends>
                    <DCWC:Legend Name="Default"></DCWC:Legend>
                    <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom"></DCWC:Legend>
                </Legends>
                <Titles>
                    <DCWC:Title Text="Deviation"></DCWC:Title>
                </Titles>                
                <Series>
                    <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" ChartType="Spline" BorderWidth="1" ShowInLegend="false" ></DCWC:Series>
                    <DCWC:Series Name="Series1" Color="150, 000, 255, 255" ChartArea="ChartArea1" ChartType="Spline" BorderWidth="1" ShowInLegend="false" ></DCWC:Series>
                    <DCWC:Series Name="Series2" Color="150, 255, 000, 255" ChartArea="ChartArea1" ChartType="Spline" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                    <DCWC:Series Name="Series3" Color="150, 255, 225, 000" ChartArea="ChartArea1" ChartType="Spline" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                    <DCWC:Series Name="Series4" Color="150, 000, 000, 000" ChartArea="ChartArea1" ChartType="Spline" BorderWidth="1" ShowInLegend="false"></DCWC:Series>
                </Series>
                <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                <UI>
                    <Toolbar BackColor="White" BorderColor="LightSlateGray">
                        <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                    </Toolbar>
                </UI>
                <ChartAreas>
                   <DCWC:ChartArea Name="ChartArea1">                        
                        <Position Height="80" Width="95" X="0" Y="10" />
                        <AxisY Enabled="True"  Maximum="5" Minimum="-5" Interval="2.5">
                            <MajorGrid LineColor="Beige" Interval="2.5" />
                        </AxisY>
                        <AxisX>
                            <MajorGrid LineColor="Beige" Interval="1" />                            
                        </AxisX>   
                    </DCWC:ChartArea>
                </ChartAreas>
                </ChartAreas>
            </DCWC:Chart>
        </td>
        <td valign="top">                                    
            <img src="Images/GreeLeftArror.gif" ID="BTN_Deviation" runat="server" align="Open List" />
            <asp:Panel ID="PNL_Deviation" runat="server" CssClass="popupMenu">
                <div style="width:216px; float:left;"><div style="height:10px">
                            <span style="width:215px;height:10px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-top-2.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-top-2.png" width="215" height="10" border="0" alt="" /></span>
                        </div>
                <div style="height:110px; background:url(images/popup-mid.png) top left repeat-y #FFF; padding:3px 16px 15px 10px; color:#666666; font-family:Tahoma, Arial, Helvetica, sans-serif; font-size:11px; text-align:left; vertical-align:top;border-left:#808080;border-right:#808080">
                    <asp:CheckBox ID="CHK_Deviation_25" runat="server" Text="<label class='deCYAN'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Deviation 25%" Checked="true"   ToolTip="Deviation 25%" /><br />
                    <asp:CheckBox ID="CHK_Deviation_50" runat="server" Text="<label class='deMAGENTA'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Deviation 50%" Checked="true"   ToolTip="Deviation 50%" /><br />
                    <asp:CheckBox ID="CHK_Deviation_75" runat="server" Text="<label class='deYELLOW'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Deviation 75%" Checked="true"  ToolTip="Deviation 75%" /><br />
                    <asp:CheckBox ID="CHK_Deviation_95" runat="server" Text="<label class='deBLACK'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;Deviation 95%" Checked="true"  ToolTip="Deviation 95%" /><br /><br />
                    <asp:Button ID="BTN_Deviation_Apply" runat="server" CssClass="BUTTON" Text="Apply" OnClick="CHK_Deviation_CheckedChanged" />
                </div>
                <div style="clear:both"></div>
                <div style="float:left; height:19px"><span style="width:215px;height:19px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-bottom.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-bottom.png" width="216" height="19" border="0" alt="" /></span></div>
            </asp:Panel>            
            <cc1:HoverMenuExtender ID="HME_Deviation" runat="Server" TargetControlID="BTN_Deviation"  PopupControlID="PNL_Deviation" HoverCssClass="popupHover" PopupPosition="Left" OffsetX="0" OffsetY="0" PopDelay="50" />              
        </td>        
    </tr>
</table>    
<asp:Literal ID="LblError" runat="server"></asp:Literal>
<asp:Literal ID="LTRL_KeyDeviation" runat="server" Visible="false"></asp:Literal>
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