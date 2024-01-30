<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ReportTargetMeasurement.aspx.cs" Inherits="OrisWeb.ReportTargetMeasurement" %>


<%@ Register Src="~/Common/ReportMenu.ascx" TagName="ReportMenu" TagPrefix="uc1" %>
<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" align="center" border="0" width="100%">
        <tr>
            <td align="center">
                <table width="940" border="0" cellspacing="0" cellpadding="0" align="center">
                    <tr>
                        <td>
                            <!--Report Menu Conttrol Will Load here-->
                            <uc1:ReportMenu ID="ReportMenu1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table width="940" border="0" cellspacing="0" cellpadding="0" align="center">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="padding: 5px;" class="TABLE1">
                                        <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                                            <tr>
                                                <td class="green-title" valign="middle" align="left">
                                                    <asp:Label ID="lblReportTitle" runat="server" Text="Color Specification Adherence Report"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="green-form-bg" style="border-bottom: none" id="TDReportTitle"
                                                    runat="server">
                                                    <asp:Label ID="LblError" runat="server" CssClass="ERROR"></asp:Label>
                                                    <asp:Literal ID="LTRL_CPMeasurementGUID" runat="server" Visible="false"></asp:Literal></center>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                                            <tr>
                                                <td align="center" valign="top">
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Panel Style="width: 100%" ID="PNL_Search" runat="server" Visible="true">
                                                                <table style="width: 100%" class="green-form-bg" cellspacing="0" cellpadding="0"
                                                                    align="center" border="0">
                                                                    <tr>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" class="form-text-blue">
                                                                            Packaging Promotions Instore Marketing
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" valign="top">
                                                                            <b>&nbsp;&nbsp;&nbsp;Selection Filter</b>
                                                                            <table width="100%" border="0" cellspacing="5" cellpadding="5">
                                                                                <tr>
                                                                                    <td style="background-color: White; border: 1px solid #808080;">
                                                                                        <table width="100%" border="0" cellspacing="2" cellpadding="3" class="form-text">
                                                                                            <tr>
                                                                                                <td style="width: 20%" class="BOLD" align="center">
                                                                                                    Promo ID Number
                                                                                                </td>
                                                                                                <td style="width: 20%" class="BOLD" align="center">
                                                                                                    Item Number
                                                                                                </td>
                                                                                                <td style="width: 20%" class="BOLD" align="center">
                                                                                                    Brand
                                                                                                </td>
                                                                                                <td style="width: 20%" class="BOLD" align="center">
                                                                                                    Order Code
                                                                                                </td>
                                                                                                <td>
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="center">
                                                                                                    <asp:TextBox ID="txtPromoIDNumber" onkeypress="return Charater(event)" CssClass="TEXTBOX"
                                                                                                        runat="server"></asp:TextBox>
                                                                                                </td>
                                                                                                <td align="center">
                                                                                                    <asp:TextBox ID="txtItemNumber" onkeypress="return Charater(event)" CssClass="TEXTBOX"
                                                                                                        runat="server"></asp:TextBox>
                                                                                                </td>
                                                                                                <td align="center">
                                                                                                    <asp:TextBox ID="txtBrand" CssClass="TEXTBOX" onkeypress="return Charater(event)"
                                                                                                        runat="server"></asp:TextBox>
                                                                                                </td>
                                                                                                <td align="center">
                                                                                                    <asp:TextBox ID="txtOrderCode" onkeypress="return Charater(event)" CssClass="TEXTBOX"
                                                                                                        runat="server"></asp:TextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Button ID="BTN_SearchJob" runat="server" CssClass="green2-btn" Text="Search"
                                                                                                        OnClick="BTN_SearchJob_Click"></asp:Button>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" valign="top">
                                                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                                <tr>
                                                                                    <td align="left" style="width: 70%">
                                                                                        <b>&nbsp;&nbsp;Job Selection List</b>
                                                                                    </td>
                                                                                    <td align="right" style="width: 30%">
                                                                                        <table cellpadding="0" cellspacing="0" border="0">
                                                                                            <tr>
                                                                                                <td align="left" style="width: 20%; padding-left: 5px">
                                                                                                    <asp:Button ID="BTN_ListPrev" runat="server" CssClass="BUTTON" ToolTip="Previous"
                                                                                                        Text="<< Prev" OnClick="BTN_ListPrev_Click" />
                                                                                                </td>
                                                                                                <td align="center" style="width: 150px">
                                                                                                    &nbsp;&nbsp;<asp:Label ID="lblPage" runat="server"></asp:Label>&nbsp;&nbsp;
                                                                                                </td>
                                                                                                <td align="right" style="width: 20%; padding-right: 5px">
                                                                                                    <asp:Button ID="BTN_ListNext" runat="server" ToolTip="Next" CssClass="BUTTON" Text="Next >>"
                                                                                                        OnClick="BTN_ListNext_Click" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <table width="100%" border="0" cellspacing="5" cellpadding="5">
                                                                                <tr>
                                                                                    <td style="background-color: White; border: 1px solid #808080;">
                                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form-text">
                                                                                            <tr>
                                                                                                <td style="width: 100%" class="BOLD" align="center" valign="middle">
                                                                                                    <div style="height: 260px; width: 100%; overflow: auto">
                                                                                                        <asp:Label ID="lblNoSearchFound" runat="server" CssClass="ERROR"></asp:Label>
                                                                                                        <asp:Button ID="btnSelectJob" runat="server" OnClick="btnSelectJob_Click" Style="display: none" />
                                                                                                        <asp:DataList Width="100%" ID="dlJobs" runat="server" OnItemDataBound="dlJobs_ItemDataBound"
                                                                                                            RepeatColumns="1" RepeatDirection="Vertical">
                                                                                                            <SelectedItemStyle BorderColor="#00ccff" />
                                                                                                            <ItemTemplate>
                                                                                                                <table style="cursor: pointer" cellpadding="2" cellspacing="2" width="100%" border="0">
                                                                                                                    <tr id="trTable" title="Click to select record." runat="server">
                                                                                                                        <td style="width: 2%" align="left">
                                                                                                                            <asp:Image ID="imgStar" runat="server" AlternateText="Star" />
                                                                                                                        </td>
                                                                                                                        <td style="width: 50%" valign="top" align="left">
                                                                                                                            <%# Eval("Job") %>
                                                                                                                            <asp:Label ID="lblGuid" Text='<%# Eval("CPMeasurementGuid") %>' runat="server" Visible="false"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td style="width: 50%" valign="top">
                                                                                                                            <%# Eval("CreatedDT")%>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' Visible="false"></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:DataList>
                                                                                                    </div>
                                                                                                    <asp:HiddenField ID="hidSelectedItem" runat="server" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Button ID="btnCreateReport" OnClick="btnCreateReport_Click" runat="server" CssClass="green2-btn"
                                                        Text="Create Report"></asp:Button>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <%------------------------ Report Generate Structure -------------------------%>
                                                <asp:Panel Style="width: 100%" ID="PNL_Result" runat="server" Visible="false">
                                                    <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                                                        <tr>
                                                            <td valign="middle" style="padding-top: 5px" align="left">
                                                                <%-- -----------Start TargetVsActualAB --------------------- --%>
                                                                <DCWC:Chart ID="CHART_TargetVersusActualAB" runat="server" BackImageMode="Scaled"
                                                                    BackImage="~/Images/FarbKreis_LAB2_150.gif" Height="160px" ImageType="Jpeg" Width="160px"
                                                                    ImageUrl="~/UploadFiles/temp/TargetMeasurement_TargetVersusActualAB_1">
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
                                                                            MarkerStyle="Circle" MarkerColor="Red" Color="Red" Name="Series3" ShadowOffset="1">
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
                                                        </tr>
                                                        <tr>
                                                            <td align="center" valign="top">
                                                                <DCWC:Chart ID="ChartSummaryDelta" runat="server" Width="350px" Height="323px" ImageUrl="~/UploadFiles/temp/TargetMeasurement_SummaryDelta_2"
                                                                    ImageType="Jpeg">
                                                                    <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                                                        SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                                                    <UI>
                                                                        <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                            <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                                FrameBackColor="SteelBlue"></BorderSkin>
                                                                        </Toolbar>
                                                                    </UI>
                                                                    <Series>
                                                                        <DCWC:Series 
                                                                            Name="Series0" 
                                                                            BackGradientType="DiagonalRight"
                                                                            BorderColor="26, 59, 105" 
                                                                            BorderWidth="0" 
                                                                            Color="#D4E8FA" 
                                                                            Font="Microsoft Sans Serif, 7pt, style=Underline"
                                                                            Label="" 
                                                                            CustomAttributes="LabelStyle=Top,DrawingStyle=Cylinder" 
                                                                            ShowInLegend="true" 
                                                                            >
                                                                        </DCWC:Series>
                                                                        <DCWC:Series 
                                                                            Name="Series1" 
                                                                            BorderWidth="0" 
                                                                            Color="Gray" 
                                                                            CustomAttributes="DrawingStyle=Cylinder"
                                                                            ShowInLegend="false" 
                                                                            >
                                                                        </DCWC:Series>
                                                                        <DCWC:Series
                                                                            Name="Series2" 
                                                                            Color="Transparent"
                                                                            CustomAttributes="DrawingStyle=Cylinder"
                                                                            ShowInLegend="false" 
                                                                            MarkerImage="~/Images/Chart-Red-Image-Vertical.png" 
                                                                            >
                                                                        </DCWC:Series>
                                                                    </Series>
                                                                    <ChartAreas>
                                                                        <DCWC:ChartArea Name="ChartArea1">
                                                                            <Area3DStyle YAngle="0" XAngle="0" Enable3D="true" RightAngleAxes="False" WallWidth="0" Clustered="True"></Area3DStyle>
                                                                            <AxisX Interval="Auto" Enabled="true" LabelsAutoFit="False" Reverse="true">
                                                                                <MajorGrid LineColor="Beige"></MajorGrid>
                                                                                <LabelStyle Font="Microsoft Sans Serif, 7pt" />
                                                                            </AxisX>
                                                                            <AxisY Interval="1" Enabled="True" LabelsAutoFit="False">
                                                                                <MajorGrid LineColor="Beige"></MajorGrid>
                                                                                <LabelStyle Format="###" Font="Microsoft Sans Serif, 7pt" />
                                                                            </AxisY>
                                                                        </DCWC:ChartArea>
                                                                    </ChartAreas>
                                                                </DCWC:Chart>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <DCWC:Chart ID="ChartDeltaE2000" runat="server" Width="340px" Height="423px" ImageUrl="~/UploadFiles/temp/TargetMeasurement_ChartDeltaE2000_3"
                                                                    ImageType="Jpeg">
                                                                    <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                                                        SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                                                    <UI>
                                                                        <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                            <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                                FrameBackColor="SteelBlue"></BorderSkin>
                                                                        </Toolbar>
                                                                    </UI>
                                                                    <Series>
                                                                        <DCWC:Series 
                                                                            Name="Series0" 
                                                                            BackGradientType="DiagonalRight"
                                                                            BorderColor="26, 59, 105" BorderWidth="0" 
                                                                            Color="#D4E8FA" 
                                                                            ShadowOffset="70"
                                                                            Font="Microsoft Sans Serif, 7pt, style=Underline"
                                                                            Label="" 
                                                                            CustomAttributes="LabelStyle=Top,DrawingStyle=Cylinder" 
                                                                            ShowInLegend="true" 
                                                                            >
                                                                        </DCWC:Series>
                                                                        <DCWC:Series
                                                                            Name="Series1" 
                                                                            BorderWidth="0" 
                                                                            BorderColor="Gray"
                                                                            CustomAttributes="DrawingStyle=Cylinder"
                                                                            ShowInLegend="false" >
                                                                        </DCWC:Series>
                                                                        <DCWC:Series
                                                                            Name="Series2" 
                                                                            BorderWidth="0" 
                                                                            Color="Transparent"
                                                                            CustomAttributes="DrawingStyle=Cylinder"
                                                                            ShowInLegend="false" 
                                                                            MarkerImage="~/Images/Chart-Red-Image-Vertical.png" 
                                                                            >
                                                                        </DCWC:Series>
                                                                    </Series>
                                                                    <ChartAreas>
                                                                        <DCWC:ChartArea Name="ChartArea1">
                                                                            <Area3DStyle YAngle="0" XAngle="0" Enable3D="true" RightAngleAxes="False" WallWidth="0" Clustered="True">
                                                                            </Area3DStyle>
                                                                            <AxisX Interval="Auto" Enabled="true" LabelsAutoFit="False" Reverse="true">
                                                                                <MajorGrid Enabled="false" LineColor="Beige"></MajorGrid>
                                                                                <MajorTickMark Enabled="false" />
                                                                                <LabelStyle Font="Microsoft Sans Serif, 7pt" />
                                                                            </AxisX>
                                                                            <AxisY Interval="1" Enabled="True" LabelsAutoFit="False">
                                                                                <MajorGrid LineColor="Beige"></MajorGrid>
                                                                                <LabelStyle Format="###" Font="Microsoft Sans Serif, 7pt" />
                                                                            </AxisY>
                                                                        </DCWC:ChartArea>
                                                                    </ChartAreas>
                                                                </DCWC:Chart>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <%------------------------------------------------------------------------------------%>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>

    <script language="javascript" type="text/javascript">
        function chkSelection(objhidSelectedItem) {
            if (objhidSelectedItem.value == '') {
                alert("Select Record by clicking on list.");
                return false;
            }

            document.getElementById("<% =LblError.ClientID %>").value = "";
            return true;
        }

        function Charater(objEvent) {
            var iKeyCode;
            if (navigator.appName == "Microsoft Internet Explorer") // To Check the browser is IE
            {
                iKeyCode = objEvent.keyCode;
            }
            else {
                iKeyCode = objEvent.which;
            }

            if (iKeyCode == 95) {// Is Pressed Character is UnderScore(_)
                return false;
            }
        }

        function SelectedItem(selItem) {
            document.getElementById("<%=hidSelectedItem.ClientID %>").value = selItem;
            document.getElementById("<%= btnSelectJob.ClientID %>").click();
        }
    </script>

</asp:Content>

