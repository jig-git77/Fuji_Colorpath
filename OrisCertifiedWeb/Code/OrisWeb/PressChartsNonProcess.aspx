<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDeviceTrendLineNonProcess.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="PressChartsNonProcess.aspx.cs" Inherits="OrisWeb.PressChartsNonProcess" %>
<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <script type="text/javascript">
//        function PageRedirect(_Index, JobID, CPDeviceID, ZoneNumber, DongleID) {
//            document.getElementById("FRM1").src = "PressChartsPatchDetails.aspx?JobID=" + JobID + "&DongleID=" + DongleID + "&CPDeviceID=" + CPDeviceID + "&ZoneNumber=" + ZoneNumber;
//            document.getElementById("FRM1").style.display = "";
//        }
        function SetValue(obj) {


            var obj1 = document.getElementById('<%=HDChart.ClientID %>');
            obj1.value = obj;
            document.getElementById('<%=BtnChart.ClientID %>').click();
        }
    </script>

    <table id="Table1" cellpadding="0" cellspacing="0" border="0" style="width: 100%; margin-top: 5px"
        runat="server" class="popup-table">
        <tr>
            <td class="popup-top-bar">
                <b>Zone Selection</b>
            </td>
        </tr>
        <tr>
            <td align="left">
                <div class="CHART_DIV_SCROLL_CHART_HORIZONTAL">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr class="ALTERNATINGITEMSTYLE">
                            <td align="center">
                                &nbsp;<hr style="width: 115px" />
                                Zone
                            </td>
                            <td>
                                <asp:DataList ID="DL_Zone1" runat="server" RepeatDirection="Horizontal" CssClass="DATALIST1"
                                    OnItemDataBound="DL_Zone1_ItemDataBound" CellSpacing="0">
                                    <ItemStyle CssClass="ALTERNATINGITEMSTYLE" Font-Bold="true" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <div id="DIV1" runat="server">
                                            <asp:DataList ID="DL_Zone1_Colors" runat="server" RepeatDirection="Horizontal" OnItemDataBound="DL_Zone1_Colors_ItemDataBound">
                                                <ItemStyle CssClass="CHRT_DIV_DATALIST" />
                                                <ItemTemplate>
                                                    <asp:Literal ID="LTRL_Color" runat="server"></asp:Literal>
                                                </ItemTemplate>
                                            </asp:DataList><hr style="width: 58px" />
                                            <%#DataBinder.Eval(Container.DataItem, "ZoneNumber")%>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                    </table>
                    <DCWC:Chart ID="CHRT_ZoneDeltaE" runat="server" Width="900px" Height="160px" ImageUrl="~/UploadFiles/temp/CHRT_TrendDeltaE_#SEQ(300,3)"
                        ImageType="Jpeg" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF"
                        BorderLineColor="#808080">
                        <Legends>
                            <DCWC:Legend Name="Default">
                            </DCWC:Legend>
                            <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                            </DCWC:Legend>
                        </Legends>
                        <Annotations>
                            <DCWC:CalloutAnnotation AllowTextEditing="True" AllowResizing="True" Name="Callout1"
                                AllowPathEditing="False" CalloutStyle="RoundedRectangle" BackColor="Gray" TextColor="White"
                                Alignment="TopCenter" BackGradientEndColor="White" Text="Selected Zone">
                            </DCWC:CalloutAnnotation>
                        </Annotations>
                        <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                            SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                        <UI>
                            <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                    FrameBackColor="SteelBlue"></BorderSkin>
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
                            <DCWC:ChartArea Name="ChartArea1" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                BackColor="White" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                <Area3DStyle YAngle="10" XAngle="10" RightAngleAxes="False" WallWidth="0" Clustered="True">
                                </Area3DStyle>
                                <Position Height="99" Width="98" X="0" Y="10" />
                                <AxisY Enabled="True">
                                    <MajorGrid LineColor="Beige" Interval="1" />
                                    <SubAxes>
                                        <DCWC:SubAxis Title="&Delta; E">
                                        </DCWC:SubAxis>
                                    </SubAxes>
                                </AxisY>
                                <AxisX Enabled="True" Interval="0.5">
                                    <MajorGrid LineColor="Beige" />
                                    <SubAxes>
                                        <DCWC:SubAxis Title="Zone Number">
                                        </DCWC:SubAxis>
                                    </SubAxes>
                                </AxisX>
                            </DCWC:ChartArea>
                        </ChartAreas>
                    </DCWC:Chart>
                    <asp:HiddenField ID="HDChart" runat="server" />
                    <asp:Button ID="BtnChart" runat="server" Text="Button" OnClick="BtnChart_Click" style="display:none" />
                </div>
            </td>
        </tr>
    </table>
    <iframe id="FRM1" name="FRM1" runat="server" style="height: 365px; width: 100%; display: none" frameborder="0"
        scrolling="no"></iframe>
    <div id="DIV_ShowInfo" name="DIV_ShowInfo" style="display: none; position: absolute;">
    </div>
    <div id="DIV_Template" name="DIV_Template" style="display: none;">
        <div style="width: 216px; float: left;">
            <div style="height: 10px">
                <span style="width: 215px; height: 10px; display: inline-block; filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-top-2.png');">
                    <img style="filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-top-2.png"
                        width="215" height="10" border="0" alt="" /></span>
            </div>
            <div style="height: 70px; background: url(images/popup-mid.png) top left repeat-y #FFF;
                padding: 3px 16px 15px 10px; color: #666666; font-family: Tahoma, Arial, Helvetica, sans-serif;
                font-size: 11px; text-align: left; vertical-align: top; border-left: #808080;
                border-right: #808080">
                <div style="margin: 0 auto; text-align: center">
                    <b>Zone Number : ##ZONENUMBER##</b><table style="width: 100%">
                        <tr>
                            <td>
                                ##COLORS##
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <ul>
                                    <li>? = IsDud</li>
                                    <li>X = IsEnabled</li>
                                </ul>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div style="clear: both">
            </div>
            <div style="float: left; height: 19px">
                <span style="width: 215px; height: 19px; display: inline-block; filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-bottom.png');">
                    <img style="filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-bottom.png"
                        width="216" height="19" border="0" alt="" /></span>
            </div>
        </div>

        <script language="javascript" type="text/javascript">
            var posx;
            var posy;
            function getMouse(e) {
                posx = 0; posy = 0;
                var ev = (!e) ? window.event : e; //IE:Moz
                if (ev.pageX) {//Moz
                    posx = ev.pageX + window.pageXOffset;
                    posy = ev.pageY + window.pageYOffset;
                }
                else if (ev.clientX) {//IE
                    posx = ev.clientX + document.body.scrollLeft;
                    posy = ev.clientY + document.body.scrollTop;
                }
                else {
                    return false;
                } //old browsers

                MoveDiv();

                //document.getElementById('myspan').firstChild.data='X='+posx+' Y='+posy;
            }
            document.onmousemove = getMouse;

            function ShowColorDiv(ZoneNumber) {
                document.getElementById("DIV_ZoneNumber_" + ZoneNumber).style.display = "";
                document.getElementById("DIV_ZoneNumber_" + ZoneNumber).style.top = (posy - 40) + "px";
                document.getElementById("DIV_ZoneNumber_" + ZoneNumber).style.left = (posx - 30) + "px";
            }
            function ShowHideDiv(ZoneNumber) {
                //document.getElementById("DIV_ZoneNumber_"+ ZoneNumber)
                document.getElementById("DIV_ZoneNumber_" + ZoneNumber).style.display = "none";
            }
            function MoveDiv() {
                document.getElementById("DIV_ShowInfo").style.top = (posy + 10) + "px";
                document.getElementById("DIV_ShowInfo").style.left = (posx - 100) + "px";
            }
            function ShowPopUpDiv(ZoneNumber, Colors, IsDudOrIsEnabled) {
                MoveDiv();

                if (ZoneNumber != "") {
                    document.getElementById("DIV_ShowInfo").innerHTML = document.getElementById("DIV_Template").innerHTML;

                    document.getElementById("DIV_ShowInfo").style.display = "";

                    var _Colors = "";
                    if (Colors != "") {
                        var ARR_Colors = Colors.toString().split(",");
                        var ARR_IsDudOrIsEnabled = IsDudOrIsEnabled.toString().split(",");
                        for (var i = 0; i < ARR_Colors.length; i = i + 1) {
                            _Colors += "<div class=\"CHRT_DIV_MOUSEOVER_1_INNER_DIV\" style=\"background-color:" + ARR_Colors[i] + ";\">" + ARR_IsDudOrIsEnabled[i] + "</div>";
                        }
                    }
                    document.getElementById("DIV_ShowInfo").innerHTML = document.getElementById("DIV_ShowInfo").innerHTML.replace("##ZONENUMBER##", ZoneNumber);
                    document.getElementById("DIV_ShowInfo").innerHTML = document.getElementById("DIV_ShowInfo").innerHTML.replace("##COLORS##", _Colors);
                }
            }
            function HidePopUpDiv() {
                document.getElementById("DIV_ShowInfo").style.display = "none";
            }
        </script>

        <asp:Literal ID="LblError" runat="server"></asp:Literal>
        <asp:Literal ID="LTRL_KeyJobZoneSummary" runat="server" Visible="False"></asp:Literal>
        <asp:Literal ID="LTRL_DIVs" runat="server" Visible="false"></asp:Literal>
        <asp:Literal ID="LTRL_Colors" runat="server" Visible="false"></asp:Literal>
        <asp:Literal ID="LTRL_IsEnabledImage" runat="server" Visible="false"></asp:Literal>
        <asp:Literal ID="LTRL_JobID" runat="server" Visible="false"></asp:Literal>
        <asp:Literal ID="LTRL_DongleID" runat="server" Visible="false"></asp:Literal>
        <asp:Literal ID="LTRL_CPDeviceID" runat="server" Visible="false"></asp:Literal>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

