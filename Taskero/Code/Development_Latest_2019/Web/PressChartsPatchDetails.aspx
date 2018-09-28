<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PressChartsPatchDetails.aspx.cs"  EnableEventValidation="false" Inherits="PressChartsPatchDetails" %>

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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <script type="text/javascript">
//    function PageRedirect( JobID , CPDeviceID , ZoneNumber , ColorID , DongleID )
//    {            
//        <%if( Request.QueryString["SheetNumber"] == null ) {%>
//            document.getElementById("FRM1").src = "PressChartsPatchDetails1.aspx?JobID="+ JobID +"&DongleID="+ DongleID +"&CPDeviceID="+ CPDeviceID +"&ZoneNumber="+ ZoneNumber +"&ColorID=" + ColorID ;
//        <%}else{%>
//            document.getElementById("FRM1").src = "PressChartsPatchDetails1.aspx?JobID="+ JobID +"&DongleID="+ DongleID +"&CPDeviceID="+ CPDeviceID +"&ZoneNumber="+ ZoneNumber +"&ColorID=" + ColorID +"&SheetNumber=<%=Request.QueryString["SheetNumber"] %>";
//        <%} %>
//        document.getElementById("FRM1").style.display = "";
//    }
      function SetValue(obj) {


            var obj1 = document.getElementById('<%=HDChart.ClientID %>');
            obj1.value = obj;
            document.getElementById('<%=BtnChart.ClientID %>').click();
        }
    </script>
    <asp:Literal ID="LtrlCSS" runat="server"></asp:Literal>
    <table id="Table1" cellpadding="0" cellspacing="0" border="0" style="width: 100%;margin-top:5px" runat="server" class="popup-table">
        <tr><td class="popup-top-bar">Zone&nbsp;<asp:Literal ID="LTRL_ZoneNumber1" runat="server"></asp:Literal>&nbsp;Details</td></tr>
        <tr>
            <td><DCWC:Chart ID="CHRT_ZoneDetails" runat="server" Width="900px" Height="125px" ImageUrl="~/UploadFiles/temp/CHRT_TrendDeltaE_#SEQ(300,3)" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                    <Legends>
                        <DCWC:Legend Name="Default">
                        </DCWC:Legend>
                        <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                        </DCWC:Legend>
                    </Legends>
                    <Titles>
                        <DCWC:Title Text="Zone Details" Visible="false">
                        </DCWC:Title>
                    </Titles>
                    <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2" SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                        <UI>
                            <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised" FrameBackColor="SteelBlue"></BorderSkin>
                            </Toolbar>
                        </UI>
                    <Series>
                        <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1"
                            ShowInLegend="false">
                        </DCWC:Series>
                        <DCWC:Series Name="Series1" Color="200, 0, 0, 0" BorderColor="100, 0 , 0 , 0" BackGradientType="VerticalCenter"
                            ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false">
                        </DCWC:Series>
                    </Series>
                    <ChartAreas>
                        <DCWC:ChartArea Name="ChartArea1">
                            <Position Height="90" Width="98" X="0" Y="10" />
                            <AxisY Enabled="True">
                                <MajorGrid LineColor="Beige" />
                                <SubAxes>
                                    <DCWC:SubAxis Title="&Delta; E">
                                    </DCWC:SubAxis>
                                </SubAxes>
                            </AxisY>
                            <AxisX Enabled="True" LabelsAutoFit="False" Interval="1">
                                <MajorGrid LineColor="Beige" />
                                <LabelStyle FontAngle="0" />
                            </AxisX>
                        </DCWC:ChartArea>
                    </ChartAreas>
                </DCWC:Chart><asp:HiddenField ID="HDChart" runat="server" />
                    <asp:Button ID="BtnChart" runat="server" Text="Button" OnClick="BtnChart_Click" style="display:none" /></td>
        </tr>
    </table>    
    <iframe id="FRM1" name="FRM1" runat="server" style="height: 190px; width: 100%; display: none" frameborder="0" scrolling="no"></iframe>
    <asp:Literal ID="LblError" runat="server"></asp:Literal>
    <asp:Literal ID="LTRL_JobID" runat="server" Visible="False"></asp:Literal>
    <asp:Literal ID="LTRL_CPDeviceID" runat="server" Visible="False"></asp:Literal>
    <asp:Literal ID="LTRL_DongleID" runat="server" Visible="False"></asp:Literal>
    <asp:Literal ID="LTRL_ZoneNumber" runat="server" Visible="False"></asp:Literal>
    <asp:Literal ID="LTRL_SheetNumber" runat="server" Visible="False"></asp:Literal>
    <asp:Literal ID="LTRL_KeyJobZoneSummary" runat="server" Visible="False"></asp:Literal>
    <asp:Literal ID="LTRL_KeyJobZoneSummaryPerSheet" runat="server" Visible="False"></asp:Literal>
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
