<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDeviceTrendLine.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ColorCharts.aspx.cs" Inherits="TaskeroWeb.ColorCharts" %>

<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <script type="text/javascript" language="javascript">
                function PageRedirect(CPMeasurementGUID, CPDevicePrintTargetID, CPDeviceID, CPPatchDataLabID, ListChart, ActiveTabIndex, DeltaE) {
                    //window.location.href = "DTLChart.aspx?CPMeasurementGUID="+ CPMeasurementGUID +"&CPDevicePrintTargetID="+ CPDevicePrintTargetID +"&CPDeviceID="+ CPDeviceID +"&CPPatchDataLabID="+ CPPatchDataLabID +"&ListChart="+ ListChart +"&ActiveTabIndex=" + ActiveTabIndex ;
                    document.getElementById("FRM1").src = "ColorChartsPatchDetails.aspx?CPMeasurementGUID=" + CPMeasurementGUID + "&CPDevicePrintTargetID=" + CPDevicePrintTargetID + "&CPDeviceID=" + CPDeviceID + "&CPPatchDataLabID=" + CPPatchDataLabID + "&ListChart=" + ListChart + "&ActiveTabIndex=" + ActiveTabIndex + "&DeltaE=" + DeltaE;
                    document.getElementById("FRM1").style.display = "";
                }
                //    style="width:890px;height:250px;overflow-x:hidden;overflow-y:auto"
            </script>

            <table id="Table1" cellpadding="0" cellspacing="0" border="0" style="width: 100%;
                margin-top: 5px" runat="server" class="popup-table">
                <tr>
                    <td class="popup-top-bar">
                        <b>
                            <asp:RadioButtonList ID="RBTN_ListChart" runat="server" CellPadding="5" RepeatDirection="Horizontal">
                                <asp:ListItem Value="C" Text="Chart" Selected="true"></asp:ListItem>
                                <asp:ListItem Value="L" Text="List"></asp:ListItem>
                                <asp:ListItem Value="PC" Text="Patch View"></asp:ListItem>
                            </asp:RadioButtonList>
                        </b>
                    </td>
                </tr>
                <tr><td>
                <table>
                    <td>
                        <table style="width: 100%;" cellspacing="5" border="0" cellpadding="0">
                            <tr>
                                <td class="ERROR">
                                    <asp:Literal ID="LblError" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr id="TR_CHRT" runat="server">
                                <td align="center">
                                    <div class="CHART_DIV_SCROLL_CHART" style="display:none">
                                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td colspan="2" align="center" style="height: 50px" valign="top">
                                                    Summary Delta
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 40%" valign="top">
                                                    
                                                    <table cellpadding="0" cellspacing="0" border="1" width="100%">
                                                        <tr style="height: 44px">
                                                            <td valign="bottom">
                                                                &nbsp;
                                                            </td>
                                                            <td valign="middle" align="center">
                                                                Actual
                                                            </td>
                                                            <td valign="middle" align="center">
                                                                Threshold
                                                            </td>
                                                        </tr>                                                                                                        
                                                       <asp:Label ID="lblReportData" runat="server"></asp:Label>
                                                    </table>
                                                </td>
                                                <td style="width: 60%">
                                                    <DCWC:Chart ID="ChartTmp" runat="server" Width="575px" Height="323px" ImageUrl="~/UploadFiles/temp/CHRT_deColors_#SEQ(300,3)"
                                                        ImageType="Jpeg" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF"
                                                        BorderLineColor="#808080">
                                                        <Legends>
                                                            <DCWC:Legend Name="Default">
                                                            </DCWC:Legend>
                                                            <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                                            </DCWC:Legend>
                                                        </Legends>
                                                        <%--<Legend Alignment="Center" Docking="Bottom" BackColor="White" BackGradientEndColor="Gray"
                                                            BackGradientType="TopBottom" BorderColor="#909090">
                                                            <CustomItems>
                                                                <DCWC:LegendItem Name="Caution" Image="~/Images/MarkerYellow_Circle.gif">
                                                                </DCWC:LegendItem>
                                                                <DCWC:LegendItem Name="Error" Image="~/Images/MarkerRed_Circle.gif">
                                                                </DCWC:LegendItem>
                                                            </CustomItems>
                                                        </Legend>--%>
                                                        <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                                            SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                                        <UI>
                                                            <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                    FrameBackColor="SteelBlue"></BorderSkin>
                                                            </Toolbar>
                                                        </UI>
                                                        <Series>
                                                            <DCWC:Series Name="Series0" BackGradientType="DiagonalRight" BorderColor="26, 59, 105"
                                                                BorderWidth="0" Color="Gray" Font="Microsoft Sans Serif, 7pt, style=Underline"
                                                                Label="" CustomAttributes="LabelStyle=Top" ShowInLegend="false" ChartType="Bar">
                                                            </DCWC:Series>
                                                            <DCWC:Series Name="Series1" BorderWidth="0" CustomAttributes="DrawingStyle=Cylinder"
                                                                ShowInLegend="false" ChartType="Bar">
                                                            </DCWC:Series>
                                                            <%--<DCWC:Series 
                                        Name="Series2" 
                                        ShowInLegend="true" 
                                        MarkerImage="~/Images/MarkerRed_Circle.gif"
                                        ChartType="Bar"></DCWC:Series>--%>
                                                            <%--<DCWC:Series 
                                        Name="Series3" 
                                        MarkerImage="~/Images/MarkerYellow_Circle.gif"
                                        ShowInLegend="true" 
                                        ChartType="Bar"></DCWC:Series>--%>
                                                        </Series>
                                                        <ChartAreas>
                                                            <DCWC:ChartArea Name="ChartArea1">
                                                                <Area3DStyle YAngle="0" XAngle="0" RightAngleAxes="False" WallWidth="0" Clustered="True">
                                                                </Area3DStyle>
                                                                <AxisX Interval="1" Enabled="true" LabelsAutoFit="False" Reverse="true">
                                                                    <MajorGrid LineColor="Beige"></MajorGrid>
                                                                    <LabelStyle Font="Microsoft Sans Serif, 7pt" />
                                                                </AxisX>
                                                                <AxisY Enabled="True" Interval="Auto" LabelsAutoFit="False">
                                                                    <MajorGrid LineColor="Beige"></MajorGrid>
                                                                    <LabelStyle Format="##0.0" Font="Microsoft Sans Serif, 7pt" />
                                                                </AxisY>
                                                            </DCWC:ChartArea>
                                                        </ChartAreas>
                                                    </DCWC:Chart>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="CHART_DIV_SCROLL_CHART">
                                        <DCWC:Chart ID="CHRT_deColors" runat="server" Width="875px" Height="245px" ImageUrl="~/UploadFiles/temp/CHRT_deColors_#SEQ(300,3)"
                                            ImageType="Jpeg" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF"
                                            BorderLineColor="#808080">
                                            <Legends>
                                                <DCWC:Legend Name="Default">
                                                </DCWC:Legend>
                                                <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                                </DCWC:Legend>
                                            </Legends>
                                            <Titles>
                                                <DCWC:Title Text="de Colors 01">
                                                </DCWC:Title>
                                            </Titles>
                                            <Legend Alignment="Center" Docking="Bottom" BackColor="White" BackGradientEndColor="Gray"
                                                BackGradientType="TopBottom" BorderColor="#909090">
                                                <CustomItems>
                                                    <DCWC:LegendItem Name="Caution" Image="~/Images/Chart-Yellow-Image.png">
                                                    </DCWC:LegendItem>
                                                    <DCWC:LegendItem Name="Error" Image="~/Images/Chart-Red-Image.png">
                                                    </DCWC:LegendItem>
                                                </CustomItems>
                                            </Legend>
                                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                                SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                            <UI>
                                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                        FrameBackColor="SteelBlue"></BorderSkin>
                                                </Toolbar>
                                            </UI>
                                            <Series>
                                                <DCWC:Series Name="Series0" BackGradientType="TopBottom" BorderColor="26, 59, 105"
                                                    BorderWidth="0" Color="Gray" Font="Microsoft Sans Serif, 7pt, style=Underline"
                                                    Label="" CustomAttributes="LabelStyle=Top" ShowInLegend="false" LabelBackColor="White">
                                                </DCWC:Series>
                                                <DCWC:Series Name="Series1" ShadowOffset="1" BorderWidth="0" CustomAttributes="DrawingStyle=Cylinder"
                                                    ShowInLegend="false" ChartType="StackedColumn">
                                                </DCWC:Series>
                                                <DCWC:Series Name="Series2" MarkerImage="~/Images/Chart-Red-Image.png" ShowInLegend="true"
                                                    ChartType="Point">
                                                </DCWC:Series>
                                                <DCWC:Series Name="Series3" MarkerImage="~/Images/Chart-Yellow-Image.png" ShowInLegend="true"
                                                    ChartType="Point">
                                                </DCWC:Series>
                                            </Series>
                                            <ChartAreas>
                                                <DCWC:ChartArea Name="ChartArea1">
                                                    <Area3DStyle YAngle="10" XAngle="10" RightAngleAxes="False" WallWidth="0" Clustered="True">
                                                    </Area3DStyle>
                                                    <AxisX Interval="1" Enabled="true" LabelsAutoFit="False">
                                                        <MajorGrid LineColor="Beige"></MajorGrid>
                                                        <LabelStyle Font="Microsoft Sans Serif, 7pt" />
                                                    </AxisX>
                                                    <AxisY Enabled="True" Interval="Auto" LabelsAutoFit="False">
                                                        <MajorGrid LineColor="Beige"></MajorGrid>
                                                        <LabelStyle Format="##0.0" Font="Microsoft Sans Serif, 7pt" />
                                                    </AxisY>
                                                </DCWC:ChartArea>
                                            </ChartAreas>
                                        </DCWC:Chart>
                                        <DCWC:Chart ID="CHRT_deColors_2" runat="server" Width="875px" Height="245px" ImageUrl="~/UploadFiles/temp/CHRT_deColors2_#SEQ(300,3)"
                                            ImageType="Jpeg" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF"
                                            BorderLineColor="#808080">
                                            <Legends>
                                                <DCWC:Legend Name="Default">
                                                </DCWC:Legend>
                                                <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                                </DCWC:Legend>
                                            </Legends>
                                            <Titles>
                                                <DCWC:Title Text="de Colors 02">
                                                </DCWC:Title>
                                            </Titles>
                                            <Legend Alignment="Center" Docking="Bottom" BackColor="White" BackGradientEndColor="Gray"
                                                BackGradientType="TopBottom" BorderColor="#909090">
                                                <CustomItems>
                                                    <DCWC:LegendItem Name="Caution" Image="~/Images/Chart-Yellow-Image.png">
                                                    </DCWC:LegendItem>
                                                    <DCWC:LegendItem Name="Error" Image="~/Images/Chart-Red-Image.png">
                                                    </DCWC:LegendItem>
                                                </CustomItems>
                                            </Legend>
                                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                                SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                            <UI>
                                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                        FrameBackColor="SteelBlue"></BorderSkin>
                                                </Toolbar>
                                            </UI>
                                            <Series>
                                                <DCWC:Series Name="Series0" BackGradientType="TopBottom" BorderColor="26, 59, 105"
                                                    BorderWidth="0" Color="Gray" Font="Microsoft Sans Serif, 7pt, style=Underline"
                                                    Label="" CustomAttributes="LabelStyle=Top" ShowInLegend="false" LabelBackColor="White">
                                                </DCWC:Series>
                                                <DCWC:Series Name="Series1" ShadowOffset="1" BorderWidth="0" CustomAttributes="DrawingStyle=Cylinder"
                                                    ShowInLegend="false" ChartType="StackedColumn">
                                                </DCWC:Series>
                                                <DCWC:Series Name="Series2" MarkerImage="~/Images/Chart-Red-Image.png" ShowInLegend="true"
                                                    ChartType="Point">
                                                </DCWC:Series>
                                                <DCWC:Series Name="Series3" MarkerImage="~/Images/Chart-Yellow-Image.png" ShowInLegend="true"
                                                    ChartType="Point">
                                                </DCWC:Series>
                                            </Series>
                                            <ChartAreas>
                                                <DCWC:ChartArea Name="ChartArea1">
                                                    <Area3DStyle YAngle="10" XAngle="10" RightAngleAxes="False" WallWidth="0" Clustered="True">
                                                    </Area3DStyle>
                                                    <AxisX Interval="1" Enabled="true" LabelsAutoFit="False">
                                                        <MajorGrid LineColor="Beige"></MajorGrid>
                                                        <LabelStyle Font="Microsoft Sans Serif, 7pt" />
                                                    </AxisX>
                                                    <AxisY Enabled="True" Interval="Auto" LabelsAutoFit="False">
                                                        <MajorGrid LineColor="Beige"></MajorGrid>
                                                        <LabelStyle Format="##0.0" Font="Microsoft Sans Serif, 7pt" />
                                                    </AxisY>
                                                </DCWC:ChartArea>
                                            </ChartAreas>
                                        </DCWC:Chart>
                                        <DCWC:Chart ID="CHRT_deColors_3" runat="server" Width="875px" Height="245px" ImageUrl="~/UploadFiles/temp/CHRT_deColors2_#SEQ(300,3)"
                                            ImageType="Jpeg" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF"
                                            BorderLineColor="#808080">
                                            <Legends>
                                                <DCWC:Legend Name="Default">
                                                </DCWC:Legend>
                                                <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                                </DCWC:Legend>
                                            </Legends>
                                            <Titles>
                                                <DCWC:Title Text="de Colors 03">
                                                </DCWC:Title>
                                            </Titles>
                                            <Legend Alignment="Center" Docking="Bottom" BackColor="White" BackGradientEndColor="Gray"
                                                BackGradientType="TopBottom" BorderColor="#909090">
                                                <CustomItems>
                                                    <DCWC:LegendItem Name="Caution" Image="~/Images/Chart-Yellow-Image.png">
                                                    </DCWC:LegendItem>
                                                    <DCWC:LegendItem Name="Error" Image="~/Images/Chart-Red-Image.png">
                                                    </DCWC:LegendItem>
                                                </CustomItems>
                                            </Legend>
                                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                                SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                            <UI>
                                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                        FrameBackColor="SteelBlue"></BorderSkin>
                                                </Toolbar>
                                            </UI>
                                            <Series>
                                                <DCWC:Series Name="Series0" BackGradientType="TopBottom" BorderColor="26, 59, 105"
                                                    BorderWidth="0" Color="Gray" Font="Microsoft Sans Serif, 7pt, style=Underline"
                                                    Label="" CustomAttributes="LabelStyle=Top" ShowInLegend="false" LabelBackColor="White">
                                                </DCWC:Series>
                                                <DCWC:Series Name="Series1" ShadowOffset="1" BorderWidth="0" CustomAttributes="DrawingStyle=Cylinder"
                                                    ShowInLegend="false" ChartType="StackedColumn">
                                                </DCWC:Series>
                                                <DCWC:Series Name="Series2" MarkerImage="~/Images/Chart-Red-Image.png" ShowInLegend="true"
                                                    ChartType="Point">
                                                </DCWC:Series>
                                                <DCWC:Series Name="Series3" MarkerImage="~/Images/Chart-Yellow-Image.png" ShowInLegend="true"
                                                    ChartType="Point">
                                                </DCWC:Series>
                                            </Series>
                                            <ChartAreas>
                                                <DCWC:ChartArea Name="ChartArea1">
                                                    <Area3DStyle YAngle="10" XAngle="10" RightAngleAxes="False" WallWidth="0" Clustered="True">
                                                    </Area3DStyle>
                                                    <AxisX Interval="1" Enabled="true" LabelsAutoFit="False">
                                                        <MajorGrid LineColor="Beige"></MajorGrid>
                                                        <LabelStyle Font="Microsoft Sans Serif, 7pt" />
                                                    </AxisX>
                                                    <AxisY Enabled="True" Interval="Auto" LabelsAutoFit="False">
                                                        <MajorGrid LineColor="Beige"></MajorGrid>
                                                        <LabelStyle Format="##0.0" Font="Microsoft Sans Serif, 7pt" />
                                                    </AxisY>
                                                </DCWC:ChartArea>
                                            </ChartAreas>
                                        </DCWC:Chart>
                                        <DCWC:Chart ID="CHRT_deColors_4" runat="server" Width="875px" Height="245px" ImageUrl="~/UploadFiles/temp/CHRT_deColors2_#SEQ(300,3)"
                                            ImageType="Jpeg" BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF"
                                            BorderLineColor="#808080">
                                            <Legends>
                                                <DCWC:Legend Name="Default">
                                                </DCWC:Legend>
                                                <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                                </DCWC:Legend>
                                            </Legends>
                                            <Titles>
                                                <DCWC:Title Text="de Colors 04">
                                                </DCWC:Title>
                                            </Titles>
                                            <Legend Alignment="Center" Docking="Bottom" BackColor="White" BackGradientEndColor="Gray"
                                                BackGradientType="TopBottom" BorderColor="#909090">
                                                <CustomItems>
                                                    <DCWC:LegendItem Name="Caution" Image="~/Images/Chart-Yellow-Image.png">
                                                    </DCWC:LegendItem>
                                                    <DCWC:LegendItem Name="Error" Image="~/Images/Chart-Red-Image.png">
                                                    </DCWC:LegendItem>
                                                </CustomItems>
                                            </Legend>
                                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                                SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                            <UI>
                                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                        FrameBackColor="SteelBlue"></BorderSkin>
                                                </Toolbar>
                                            </UI>
                                            <Series>
                                                <DCWC:Series Name="Series0" BackGradientType="TopBottom" BorderColor="26, 59, 105"
                                                    BorderWidth="0" Color="Gray" Font="Microsoft Sans Serif, 7pt, style=Underline"
                                                    Label="" CustomAttributes="LabelStyle=Top" ShowInLegend="false" LabelBackColor="White">
                                                </DCWC:Series>
                                                <DCWC:Series Name="Series1" ShadowOffset="1" BorderWidth="0" CustomAttributes="DrawingStyle=Cylinder"
                                                    ShowInLegend="false" ChartType="StackedColumn">
                                                </DCWC:Series>
                                                <DCWC:Series Name="Series2" MarkerImage="~/Images/Chart-Red-Image.png" ShowInLegend="true"
                                                    ChartType="Point">
                                                </DCWC:Series>
                                                <DCWC:Series Name="Series3" MarkerImage="~/Images/Chart-Yellow-Image.png" ShowInLegend="true"
                                                    ChartType="Point">
                                                </DCWC:Series>
                                            </Series>
                                            <ChartAreas>
                                                <DCWC:ChartArea Name="ChartArea1">
                                                    <Area3DStyle YAngle="10" XAngle="10" RightAngleAxes="False" WallWidth="0" Clustered="True">
                                                    </Area3DStyle>
                                                    <AxisX Interval="1" Enabled="true" LabelsAutoFit="False">
                                                        <MajorGrid LineColor="Beige"></MajorGrid>
                                                        <LabelStyle Font="Microsoft Sans Serif, 7pt" />
                                                    </AxisX>
                                                    <AxisY Enabled="True" Interval="Auto" LabelsAutoFit="False">
                                                        <MajorGrid LineColor="Beige"></MajorGrid>
                                                        <LabelStyle Format="##0.0" Font="Microsoft Sans Serif, 7pt" />
                                                    </AxisY>
                                                </DCWC:ChartArea>
                                            </ChartAreas>
                                        </DCWC:Chart>
                                    </div>
                                </td>
                            </tr>
                            <tr id="TR_DL" runat="server" style="display: none;">
                                <td align="center">
                                    <div class="CHART_DIV_SCROLL">
                                        <asp:GridView ID="GV_deColors" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                            OnRowDataBound="GV_deColors_RowDataBound" Width="80%" OnSorting="GV_deColors_Sorting">
                                            <AlternatingRowStyle CssClass="ALTERNATINGSTYLE" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LBL_Color" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Patch Name" SortExpression="CPPatchDataLabID">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="LTRL_PatchName" runat="server"></asp:Literal></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="LAB_L1" HeaderText="LAB L1" SortExpression="LAB_L1"></asp:BoundField>
                                                <asp:BoundField DataField="LAB_A1" HeaderText="LAB A1" SortExpression="LAB_A1"></asp:BoundField>
                                                <asp:BoundField DataField="LAB_B1" HeaderText="LAB B1" SortExpression="LAB_B1"></asp:BoundField>
                                                <asp:BoundField DataField="LAB_L2" HeaderText="LAB L2" SortExpression="LAB_L2"></asp:BoundField>
                                                <asp:BoundField DataField="LAB_A2" HeaderText="LAB A2" SortExpression="LAB_A2"></asp:BoundField>
                                                <asp:BoundField DataField="LAB_B2" HeaderText="LAB B2" SortExpression="LAB_B2"></asp:BoundField>
                                                <asp:BoundField DataField="CMYK_C" HeaderText="C" SortExpression="CMYK_C"></asp:BoundField>
                                                <asp:BoundField DataField="CMYK_M" HeaderText="M" SortExpression="CMYK_M"></asp:BoundField>
                                                <asp:BoundField DataField="CMYK_Y" HeaderText="Y" SortExpression="CMYK_Y"></asp:BoundField>
                                                <asp:BoundField DataField="CMYK_K" HeaderText="K" SortExpression="CMYK_K"></asp:BoundField>
                                                <asp:BoundField DataField="dL" HeaderText="dL" SortExpression="dL"></asp:BoundField>
                                                <asp:BoundField DataField="dA" HeaderText="dA" SortExpression="dA"></asp:BoundField>
                                                <asp:BoundField DataField="dB" HeaderText="dB" SortExpression="dB"></asp:BoundField>
                                                <asp:BoundField DataField="dE" HeaderText="dE" SortExpression="dE"></asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr id="TR_LargeChart" runat="server" style="display: none;">
                                <td align="center" valign="top">
                                    <b>Sort By : </b>
                                    <asp:DropDownList ID="DDL_Sort" runat="server" CssClass="DROPDOWNLIST">
                                        <asp:ListItem Value="CPPatchDataLabID">Patch Name</asp:ListItem>
                                        <asp:ListItem Value="LAB_L1">LAB L1</asp:ListItem>
                                        <asp:ListItem Value="LAB_A1">LAB A1</asp:ListItem>
                                        <asp:ListItem Value="LAB_B1">LAB B1</asp:ListItem>
                                        <asp:ListItem Value="LAB_L2">LAB L2</asp:ListItem>
                                        <asp:ListItem Value="LAB_A2">LAB A2</asp:ListItem>
                                        <asp:ListItem Value="LAB_B2">LAB B2</asp:ListItem>
                                        <asp:ListItem Value="CMYK_C">C</asp:ListItem>
                                        <asp:ListItem Value="CMYK_M">M</asp:ListItem>
                                        <asp:ListItem Value="CMYK_Y">Y</asp:ListItem>
                                        <asp:ListItem Value="CMYK_K">K</asp:ListItem>
                                        <asp:ListItem Value="dL">dL</asp:ListItem>
                                        <asp:ListItem Value="dA">dA</asp:ListItem>
                                        <asp:ListItem Value="dB">dB</asp:ListItem>
                                        <asp:ListItem Value="dE">dE</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;<asp:DropDownList ID="DDL_Order" runat="server" CssClass="DROPDOWNLIST">
                                        <asp:ListItem Value="">Ascending</asp:ListItem>
                                        <asp:ListItem Value="Desc">Descending</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;<asp:Button ID="BTN_Sort" runat="server" Text="Sort Order" OnClick="BTN_Sort_Click"
                                        CssClass="BUTTON" /><br />
                                    <br />
                                    <div class="CHART_DIV_SCROLL_CHART" align="center">
                                        <asp:Literal ID="LTRL_LargeChart" runat="server"></asp:Literal>
                                    </div>
                                </td>
                            </tr>
                            <tr id="TR_Frame" runat="server" style="display: none;">
                                <td align="center">
                                    <iframe id="FRM1" name="FRM1" style="height: 310px; width: 100%; display: none" frameborder="0"
                                        scrolling="no"></iframe>
                                </td>
                            </tr>
                        </table>
                    </td>
                </table>
                </td></tr>
            </table>
            <asp:Literal ID="LTRL_KEY" runat="server" Visible="false"></asp:Literal>
            <asp:Literal ID="LTRL_CPPatchDataLabID" runat="server" Visible="false">0</asp:Literal>
            <asp:Literal ID="LTRL_CPDevicePrintTargetID" runat="server" Visible="false">0</asp:Literal>
            <asp:Literal ID="LTRL_CPDeviceID" runat="server" Visible="false">0</asp:Literal>
            <asp:Literal ID="LTRL_CPMeasurementGUID" runat="server" Visible="false"></asp:Literal>

            <script language="javascript" type="text/javascript">
                function ShowHideGridChart(RBTN_IDs, TBL_Chrt_IDs, TBL_DL_IDs, TR_LargeChart_IDs, TR_Frame_IDs) {
                    //========= Default ==========//        
                    document.getElementById(TBL_Chrt_IDs).style.display = "none";
                    document.getElementById(TBL_DL_IDs).style.display = "none";
                    document.getElementById(TR_LargeChart_IDs).style.display = "none";
                    document.getElementById(TR_Frame_IDs).style.display = "none";

                    //========= Show if select = Chart in RadioButton ==========//        
                    if (document.getElementById(RBTN_IDs + "_2") == null) {
                        if (document.getElementById(RBTN_IDs + "_0").checked)
                        { document.getElementById(TBL_DL_IDs).style.display = ""; }
                        else
                        { document.getElementById(TR_LargeChart_IDs).style.display = ""; document.getElementById(TR_Frame_IDs).style.display = ""; }
                    }
                    else {
                        if (document.getElementById(RBTN_IDs + "_0").checked)
                        { document.getElementById(TBL_Chrt_IDs).style.display = ""; document.getElementById(TR_Frame_IDs).style.display = ""; }
                        else if (document.getElementById(RBTN_IDs + "_1").checked)
                        { document.getElementById(TBL_DL_IDs).style.display = ""; }
                        else
                        { document.getElementById(TR_LargeChart_IDs).style.display = ""; document.getElementById(TR_Frame_IDs).style.display = ""; }
                    }
                }
                ShowHideGridChart('<%=RBTN_ListChart.ClientID %>', '<%=TR_CHRT.ClientID %>', '<%=TR_DL.ClientID %>', '<%=TR_LargeChart.ClientID %>', '<%=TR_Frame.ClientID %>');    
            </script>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>