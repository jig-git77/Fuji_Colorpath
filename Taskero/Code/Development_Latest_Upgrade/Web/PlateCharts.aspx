<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterDeviceTrendLinePlate.master"  EnableEventValidation="false" CodeFile="PlateCharts.aspx.cs" Inherits="PlateCharts" %>
<%@ Register assembly="DundasWebChart" namespace="Dundas.Charting.WebControl" tagprefix="DCWC" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<table cellpadding="0" cellspacing="5" border="0">    
    <tr>
        <td align="center" style="width:850px">
    <DCWC:Chart ID="CHRT_Deviation" runat="server" Width="875px" Height="200px" ImageUrl="~/UploadFiles/temp/CHRT_Deviation_#SEQ(300,3)"  ImageType="Jpeg" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                <Legends>
                    <DCWC:Legend Name="Default" Enabled="false" >
                    </DCWC:Legend>                    
                </Legends>
                <Titles>
                    <DCWC:Title Text="Deviation"></DCWC:Title>
                </Titles>                
                <Series>
                    <DCWC:Series Name="Series0" Color="Transparent" ShowInLegend="false"></DCWC:Series>
                    <DCWC:Series Name="Series1" Color="Black" BorderColor="64, 64, 64" 
                        ShadowOffset="1"></DCWC:Series>
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
                        <AxisX Interval="5">
                            <MajorGrid LineColor="Beige" Interval="1" />                            
                            <LabelStyle Interval="Auto" />
                        </AxisX>   
                        
                    </DCWC:ChartArea>
                </ChartAreas>
                </ChartAreas>
            </DCWC:Chart>
             </td>
        <td valign="top">                                    
            &nbsp;<asp:Panel ID="PNL_Deviation" runat="server"  CssClass="popupMenu">
                <div style="width:216px; float:left;"><div style="height:10px">
                    <span style="width:215px;height:10px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-top-2.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-top-2.png" width="215" height="10" border="0" alt="" /></span>
                </div>
              
                <div style="clear:both"></div>
                <div style="float:left; height:19px"><span style="width:215px;height:19px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-bottom.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-bottom.png" width="216" height="19" border="0" alt="" /></span></div>
            </asp:Panel>            
        </td>
    </tr>
</table>  
<asp:Literal ID="LblError" runat="server"></asp:Literal>
<asp:Literal ID="LTRL_KEY" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="LTRL_CPMeasurementGUID" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="LTRL_CPMeasurementGUIDs" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="LTRL_StatuSs" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="LTRL_CPDeviceID" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="LTRL_CPDevicePrintTargetID" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="LTRL_CPDeviceName" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="LTRL_PageSize" runat="server" Visible="false">25</asp:Literal>
<asp:Literal ID="LTRL_StartIndex" runat="server" Visible="false">0</asp:Literal>  
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>