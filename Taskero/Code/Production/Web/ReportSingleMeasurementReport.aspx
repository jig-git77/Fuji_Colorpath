<%@ Page Title="" Language="C#" MasterPageFile="~/Home.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="ReportSingleMeasurementReport.aspx.cs"
    Inherits="ReportSingleMeasurementReport" %>

<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register Src="Common/ReportMenu.ascx" TagName="ReportMenu" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<%@ Register TagPrefix="custom" Namespace="Fuji.Business" Assembly="Fuji.Business" %>
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
                            <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
                                <tr>
                                    <td>
                                        <br />
                                        <table cellpadding="0" cellspacing="0" border="0" class="TABLE1">
                                            <tr>
                                                <td class="green-title">
                                                    &nbsp; Single Measurement Report
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblError" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <rad:RadGrid ID="GridJobReportPrepress" runat="server" AllowFilteringByColumn="True"
                                                        AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" EnableAJAX="True"
                                                        GridLines="None" Width="100%" PageSize="25" OnNeedDataSource="GridJobReportPrepress_NeedDataSource"
                                                        Skin="Green" OnItemDataBound="GridJobReportPrepress_ItemDataBound" OnItemCreated="GridJobReportPrepress_ItemCreated" OnItemCommand="GridJobReportPrepress_ItemCommand">
                                                        <ItemStyle Wrap="true" />
                                                        <ClientSettings Resizing-AllowColumnResize="true">
                                                            <Scrolling AllowScroll="True" ScrollHeight="400" />
                                                        </ClientSettings>
                                                        <PagerStyle Position="Top" />
                                                        <MasterTableView>
                                                            <PagerStyle Position="Top" />
                                                            <RowIndicatorColumn Visible="False">
                                                            </RowIndicatorColumn>
                                                            <ExpandCollapseColumn Visible="False" Resizable="False">
                                                            </ExpandCollapseColumn>
                                                            <Columns>
                                                                <custom:FilteringColumn HeaderText="Status" SortDescImageUrl="~/RadControls/Grid/Skins/Green/SortDesc.gif"
                                                                    UniqueName="Status" SortAscImageUrl="~/RadControls/Grid/Skins/Green/SortAsc.gif"
                                                                    SortExpression="Status" FilterImageUrl="~/RadControls/Grid/Skins/Green/Filter.gif"
                                                                    DataField="MeasurementStatus" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        &nbsp;<img id="ibtnStatus" runat="server" style="cursor: pointer" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="6%"></HeaderStyle>
                                                                    <ItemStyle Width="6%" />
                                                                </custom:FilteringColumn>
                                                                <rad:GridBoundColumn DataField="MeasurementStatus" DataType="System.String" HeaderText="Status"
                                                                    UniqueName="hdnStatus" Visible="false">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridTemplateColumn HeaderText="Report" AllowFiltering="false" UniqueName="View"
                                                                    ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>                                                                       
                                                                        <img id="imgCreateReport" runat="server" src="~/Images/status-icon.gif" style="cursor: pointer"
                                                                            title="Create Report" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="6%"></HeaderStyle>
                                                                    <ItemStyle Width="6%" />
                                                                </rad:GridTemplateColumn>
                                                                <rad:GridBoundColumn DataField="Job" HeaderText="Job No" UniqueName="Job" HeaderStyle-Width="15%" ItemStyle-Width="15%"
                                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="PrintTarget" HeaderText="Print Target" UniqueName="PrintTarget" HeaderStyle-Width="15%" ItemStyle-Width="15%"
                                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="CPDeviceDesc" HeaderText="Device" UniqueName="CPDeviceDesc"
                                                                    HeaderStyle-Width="12%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                </rad:GridBoundColumn>
                                                                <%--<rad:GridBoundColumn DataField="DongleSerial" HeaderText="Workstation" UniqueName="DongleDescription"
                                                                            HeaderStyle-Width="12%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                        </rad:GridBoundColumn>--%>
                                                                <rad:GridBoundColumn DataField="BranchName" HeaderText="Branch Name" UniqueName="BranchName"
                                                                    HeaderStyle-Width="12%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                </rad:GridBoundColumn>
                                                                <custom:FilteringColumn HeaderText="Device Type" SortDescImageUrl="~/RadControls/Grid/Skins/Green/SortDesc.gif"
                                                                    UniqueName="DeviceTypeWithoutPress" SortAscImageUrl="~/RadControls/Grid/Skins/Green/SortAsc.gif"
                                                                    SortExpression="Status" FilterImageUrl="~/RadControls/Grid/Skins/Green/Filter.gif"
                                                                    DataField="CPDeviceTypeDesc">
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "CPDeviceTypeDesc")%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="10%"></HeaderStyle>
                                                                </custom:FilteringColumn>
                                                                <custom:FilterColumnByDate UniqueName="DateTime" DataField="DateTime" HeaderText="Date"
                                                                    SortExpression="DateTime" HeaderStyle-Width="14%">
                                                                    <HeaderStyle />
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "CreatedDT")%></ItemTemplate>
                                                                </custom:FilterColumnByDate>
                                                                <%--<rad:GridBoundColumn DataField="Date" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="TIME" Visible="false"></rad:GridBoundColumn>--%>
                                                                <rad:GridBoundColumn DataField="CPMeasurmentGUID" Visible="false">
                                                                </rad:GridBoundColumn>
                                                                <%--<rad:GridBoundColumn DataField="CPMeasurementID" Visible="false"></rad:GridBoundColumn>--%>
                                                                <%--<rad:GridBoundColumn DataField="CompanyID" Visible="false">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="CompanyName" Visible="false">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="BranchID" Visible="false">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="CPDongleID" Visible="false">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="CPDeviceID" Visible="false">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="CPDevicePrintTargetID" Visible="false">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="Customer" Visible="false">
                                                                </rad:GridBoundColumn>--%>
                                                                <rad:GridBoundColumn DataField="CPDeviceTypeID" Visible="false">
                                                                </rad:GridBoundColumn>
                                                               <%-- <rad:GridBoundColumn DataField="StatusID" Visible="false">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="CPSerialNUM" Visible="false">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridTemplateColumn Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCPDeviceTypeID" runat="server" Text='<%# Eval("CPDeviceTypeID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </rad:GridTemplateColumn>--%>
                                                                <rad:GridTemplateColumn Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblGUID" runat="server" Text='<%# Eval("CPMeasurmentGUID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </rad:GridTemplateColumn>
                                                              <%--  <rad:GridTemplateColumn Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCPDevicePrintTargetID" runat="server" Text='<%# Eval("CPDevicePrintTargetID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </rad:GridTemplateColumn>
                                                                <rad:GridTemplateColumn Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCPDeviceID" runat="server" Text='<%# Eval("CPDeviceID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </rad:GridTemplateColumn>
                                                                <rad:GridTemplateColumn Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCPSerialNUM" runat="server" Text='<%# Eval("CPSerialNUM") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </rad:GridTemplateColumn>--%>
                                                            </Columns>
                                                            <PagerStyle AlwaysVisible="True" />
                                                            <PagerTemplate>
                                                                <div class="RADSEARCHBAR">
                                                                    <div style="float: left">
                                                                        <asp:Literal ID="LTRL_RecordText" runat="server"></asp:Literal>&nbsp;
                                                                        <asp:ImageButton ID="IBTN_First" CommandName="Page" CausesValidation="false" CommandArgument="First"
                                                                            runat="server" ImageUrl="Images/Nav_FirstPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                        <asp:ImageButton ID="IBTN_Prev" CommandName="Page" CausesValidation="false" CommandArgument="Prev"
                                                                            runat="server" ImageUrl="Images/Nav_PreviousPage_off.gif" align="absmiddle">
                                                                        </asp:ImageButton>
                                                                        <asp:ImageButton ID="IBTN_Next" CommandName="Page" CausesValidation="false" CommandArgument="Next"
                                                                            runat="server" ImageUrl="Images/Nav_NextPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                        <asp:ImageButton ID="IBTN_Last" CommandName="Page" CausesValidation="false" CommandArgument="Last"
                                                                            runat="server" ImageUrl="Images/Nav_LastPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                        &nbsp;
                                                                        <asp:TextBox ID="TXT_PageNumber" runat="server" Columns="3" Text='<%# (int)DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex") + 1 %>'
                                                                            Visible="False" />
                                                                        <asp:LinkButton ID="IBTN_Go" runat="server" CommandName="CustomChangePage" Visible="False">Go</asp:LinkButton>
                                                                        <font class="text">| Records</font>
                                                                        <asp:DropDownList ID="DDL_RecordPage" runat="server" AutoPostBack="true" CommandName="Page"
                                                                            class="inputbox">
                                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                                            <asp:ListItem Value="25" Selected>25</asp:ListItem>
                                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                                            <asp:ListItem Value="100">100</asp:ListItem>
                                                                            <asp:ListItem Value="500">500</asp:ListItem>
                                                                            <asp:ListItem Value="1000">1000</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        per page
                                                                    </div>
                                                                    <div style="float: left; width: 150px;">
                                                                        &nbsp;</div>
                                                                    <div style="float: left">
                                                                        <img id="Img1" runat="server" src="~/Images/GreenStar.gif" align="absmiddle" />
                                                                        Passed &nbsp;
                                                                        <img id="Img2" runat="server" src="~/Images/YellowStar.gif" align="absmiddle" />
                                                                        Caution &nbsp;
                                                                        <img id="Img3" runat="server" src="~/Images/RedStar.gif" align="absmiddle" />
                                                                        Failed &nbsp;
                                                                    </div>
                                                                    <div style="float: right">
                                                                        <asp:Button ID="BTN_Reset" TabIndex="48" runat="server" CssClass="BUTTON" Text="Clear Filter"
                                                                            CommandName="ResetFilter"></asp:Button>
                                                                        &nbsp;
                                                                    </div>
                                                                </div>
                                                            </PagerTemplate>
                                                        </MasterTableView>
                                                    </rad:RadGrid>
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
                            <asp:Button ID="BtnCreateReport" runat="server" Text="Create Report" OnClick="BtnCreateReport_Click" Style="display: none"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <%------------------------ Report Generate Structure -------------------------%>
                                    <asp:Panel Style="width: 100%" ID="PNL_Result" runat="server" Visible="false">
                                        <div style="overflow: auto; height:400px;">
                                            <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                                                <tr>
                                                    <td align="center" valign="top">
                                                        Target Versus Actual AB Plot
                                                        <br />
                                                        <DCWC:Chart ID="CHART_TargetVersusActualAB" runat="server" BackImageMode="Scaled"
                                                            BackImage="~/Images/SingleMeasurement_PointBack.gif" Height="218px" ImageType="Jpeg"
                                                            Width="218px" ImageUrl="~/UploadFiles/temp/SingleMeasurement_Page1_TargetVersusActualAB">
                                                            <Legends>
                                                                <DCWC:Legend Name="Default" Enabled="False">
                                                                </DCWC:Legend>
                                                            </Legends>
                                                            <Series>
                                                                <DCWC:Series Name="Series0" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                    MarkerSize="9" MarkerStyle="Circle" ShadowOffset="1">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series1" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                    MarkerSize="9" MarkerStyle="Circle" ShadowOffset="1">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series2" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                    MarkerSize="9" MarkerStyle="Circle" ShadowOffset="1">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series3" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                    MarkerSize="9" MarkerStyle="Circle" ShadowOffset="1">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series4" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                    MarkerSize="9" MarkerStyle="Circle" ShadowOffset="1">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series5" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                    MarkerSize="9" MarkerStyle="Circle" ShadowOffset="1">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series6" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                    MarkerSize="6" MarkerStyle="Circle" ShadowOffset="1">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series7" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                    MarkerSize="6" MarkerStyle="Circle" ShadowOffset="1">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series8" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                    MarkerSize="6" MarkerStyle="Circle" ShadowOffset="1">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series9" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                    MarkerSize="6" MarkerStyle="Circle" ShadowOffset="1">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series10" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                    MarkerSize="6" MarkerStyle="Circle" ShadowOffset="1">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series11" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                    MarkerSize="6" MarkerStyle="Circle" ShadowOffset="1">
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
                                                                        LineStyle="NotSet" >
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
                                                    <td>
                                                        Summary Delta Value
                                                        <br />
                                                        <DCWC:Chart ID="ChartResultSummary_Delta" runat="server" Width="340px" Height="290px"
                                                            ImageUrl="~/UploadFiles/temp/SingleMeasurement_Page1_ResultSummary_Delta" ImageType="Jpeg">
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
                                                                    BorderWidth="0" Color="#D4E8FA" ShadowOffset="70" Font="Microsoft Sans Serif, 7pt, style=Underline"
                                                                    Label="" CustomAttributes="LabelStyle=Top,DrawingStyle=Cylinder" ShowInLegend="true">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series1" BorderWidth="0" BorderColor="Gray" Color="Gray" CustomAttributes="DrawingStyle=Cylinder"
                                                                    ShowInLegend="false">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series2" BorderWidth="0" Color="Transparent" CustomAttributes="DrawingStyle=Cylinder"
                                                                    ShowInLegend="false" MarkerImage="~/Images/Chart-Red-Image-Vertical.png">
                                                                </DCWC:Series>
                                                            </Series>
                                                            <ChartAreas>
                                                                <DCWC:ChartArea Name="ChartArea1">
                                                                    <Area3DStyle YAngle="0" XAngle="0" Enable3D="true" RightAngleAxes="False" WallWidth="0"
                                                                        Clustered="True"></Area3DStyle>
                                                                    <AxisX Interval="Auto" Enabled="true" LabelsAutoFit="False" Reverse="true">
                                                                        <MajorGrid Enabled="false" LineColor="Beige"></MajorGrid>
                                                                        <MajorTickMark Enabled="false" />
                                                                        <LabelStyle Font="Verdana, 7pt" />                                                                        
                                                                    </AxisX>
                                                                    <AxisY Enabled="True" LabelsAutoFit="False">
                                                                        <MajorGrid LineColor="Beige"></MajorGrid>
                                                                        <LabelStyle Format="###" Font="Verdana, 7pt" FontColor="Black" />
                                                                    </AxisY>
                                                                </DCWC:ChartArea>
                                                            </ChartAreas>
                                                        </DCWC:Chart>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Summary Delta E Value
                                                        <br />
                                                        <DCWC:Chart ID="ChartResultSummary_DeltaEValue" runat="server" Width="340px" Height="400px"
                                                            ImageUrl="~/UploadFiles/temp/SingleMeasurement_Page1_ResultSummary_DeltaEValue"
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
                                                                <DCWC:Series Name="Series0" BackGradientType="DiagonalRight" BorderColor="26, 59, 105"
                                                                    BorderWidth="0" Color="#D4E8FA" ShadowOffset="70" Font="Microsoft Sans Serif, 7pt, style=Underline"
                                                                    Label="" CustomAttributes="LabelStyle=Top,DrawingStyle=Cylinder" ShowInLegend="true">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series1" BorderWidth="0" BorderColor="Gray" Color="Gray" CustomAttributes="DrawingStyle=Cylinder"
                                                                    ShowInLegend="false">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series2" BorderWidth="0" Color="Transparent" CustomAttributes="DrawingStyle=Cylinder"
                                                                    ShowInLegend="false" MarkerImage="~/Images/Chart-Red-Image-Vertical.png">
                                                                </DCWC:Series>
                                                            </Series>
                                                            <ChartAreas>
                                                                <DCWC:ChartArea Name="ChartArea1">
                                                                    <Area3DStyle YAngle="0" XAngle="0" Enable3D="true" RightAngleAxes="False" WallWidth="0"
                                                                        Clustered="True"></Area3DStyle>
                                                                    <AxisX Interval="Auto" Enabled="true" LabelsAutoFit="False" Reverse="true">
                                                                        <MajorGrid Enabled="false" LineColor="Beige"></MajorGrid>
                                                                        <MajorTickMark Enabled="false" />
                                                                        <LabelStyle Font="Verdana, 7pt" />                                                                        
                                                                    </AxisX>
                                                                    <AxisY Enabled="True" LabelsAutoFit="False">
                                                                        <MajorGrid LineColor="Beige"></MajorGrid>
                                                                        <LabelStyle Format="###" Font="Verdana, 7pt" FontColor="Black" />
                                                                    </AxisY>
                                                                </DCWC:ChartArea>
                                                            </ChartAreas>
                                                        </DCWC:Chart>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Gray L* Deviation Graph
                                                        <br />
                                                        <DCWC:Chart ID="CHRT_GrayDeviation_L" runat="server" Width="900px" Height="160px"
                                                            ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/SingleMeasurement_Page2_GrayDeviation_L"
                                                            BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="White"
                                                            BorderLineColor="#808080">                                                            
                                                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                                                SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                                            <UI>
                                                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                        FrameBackColor="SteelBlue"></BorderSkin>
                                                                </Toolbar>
                                                            </UI>
                                                            <Series>
                                                                <DCWC:Series MarkerSize="0" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                                                    Color="#6A6B6B" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                                    BorderWidth="5" Name="Series1">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series2" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                    MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                                </DCWC:Series>
                                                                
                                                            </Series>
                                                            <ChartAreas>
                                                                <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="Gray"
                                                                    BackColor="White" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                    <Position Height="90" Width="90" X="0" Y="15" />
                                                                    <AxisY LabelsAutoFit="False" Title=""   >
                                                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                                                    </AxisY>
                                                                    <AxisX LabelsAutoFit="False" Margin="False" Interval="Auto" Title="">
                                                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                                                        <LabelStyle FontAngle="0" />
                                                                    </AxisX>
                                                                </DCWC:ChartArea>
                                                            </ChartAreas>
                                                        </DCWC:Chart>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Gray a* Deviation Graph
                                                        <br />
                                                        <DCWC:Chart ID="CHRT_GrayDeviation_A" runat="server" Width="900px" Height="160px"
                                                            ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/SingleMeasurement_Page2_GrayDeviation_A"
                                                            BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="White"
                                                            BorderLineColor="#808080">                                                          
                                                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                                                SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                                            <UI>
                                                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                        FrameBackColor="SteelBlue"></BorderSkin>
                                                                </Toolbar>
                                                            </UI>
                                                            <Series>
                                                                <DCWC:Series MarkerSize="0" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                                                    Color="#6A6B6B" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                                    BorderWidth="5" Name="Series1">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series2" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                    MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                                </DCWC:Series>
                                                                
                                                            </Series>
                                                            <ChartAreas>
                                                                <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="#339966"
                                                                    BackColor="#ff66cc" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                    <Position Height="80" Width="90" X="0" Y="15" />
                                                                    <AxisY LabelsAutoFit="False" Title="" >
                                                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                                                    </AxisY>
                                                                    <AxisX LabelsAutoFit="False" Margin="False" Interval="Auto" Title="">
                                                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                                                        <LabelStyle FontAngle="0" />
                                                                    </AxisX>
                                                                </DCWC:ChartArea>
                                                            </ChartAreas>
                                                        </DCWC:Chart>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Gray b* Deviation Graph
                                                        <br />
                                                        <DCWC:Chart ID="CHRT_GrayDeviation_B" runat="server" Width="900px" Height="160px"
                                                            ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/SingleMeasurement_Page2_GrayDeviation_B"
                                                            BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="White"
                                                            BorderLineColor="#808080">                                                          
                                                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                                                SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                                            <UI>
                                                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                        FrameBackColor="SteelBlue"></BorderSkin>
                                                                </Toolbar>
                                                            </UI>
                                                            <Series>
                                                                 <DCWC:Series MarkerSize="0" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                                                    Color="#6A6B6B" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                                    BorderWidth="5" Name="Series1">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series2" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                    MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                                </DCWC:Series>
                                                               
                                                            </Series>
                                                            <ChartAreas>
                                                                <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="#3333cc"
                                                                    BackColor="#ffff99" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                    <Position Height="80" Width="90" X="0" Y="15" />
                                                                    <AxisY LabelsAutoFit="False" Title="">
                                                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                                                    </AxisY>
                                                                    <AxisX LabelsAutoFit="False" Margin="False" Interval="Auto" Title="">
                                                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                                                        <LabelStyle FontAngle="0" />
                                                                    </AxisX>
                                                                </DCWC:ChartArea>
                                                            </ChartAreas>
                                                        </DCWC:Chart>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Gray NPD
                                                        <br />
                                                        <DCWC:Chart ID="CHRT_GrayNPD" runat="server" Width="450px" Height="385px" ImageType="Jpeg"
                                                            ImageUrl="~/UploadFiles/temp/SingleMeasurement_Page2_GrayNPD" BackGradientType="Center"
                                                            Palette="Kindergarten" BackGradientEndColor="White" BorderLineColor="#808080">
                                                            <Legends>
                                                                <DCWC:Legend Name="Default" Enabled="false">
                                                                </DCWC:Legend>
                                                            </Legends>
                                                            <Titles>
                                                                <DCWC:Title Text="Gray NPD" Alignment="TopCenter" Font="Verdana, 8pt, style=Bold">
                                                                </DCWC:Title>
                                                            </Titles>
                                                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                                                SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                                            <UI>
                                                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                        FrameBackColor="SteelBlue"></BorderSkin>
                                                                </Toolbar>
                                                            </UI>
                                                            <Series>
                                                                <DCWC:Series Name="Series1" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                    MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                                </DCWC:Series>
                                                                <DCWC:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                                                    Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                                    BorderWidth="2" Name="Series2">
                                                                </DCWC:Series>
                                                            </Series>
                                                            <ChartAreas>
                                                                <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                                                    BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                    <Position Height="80" Width="90" X="0" Y="15" />
                                                                    <AxisY LabelsAutoFit="False" Title="Density" TitleFont="Verdana, 8pt, style=Bold" Interval="Auto">
                                                                        <MajorGrid LineColor="Gray"></MajorGrid>                                                                       
                                                                        
                                                                    </AxisY>
                                                                    <AxisX LabelsAutoFit="False" Margin="False" Interval="Auto" Title="">
                                                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                                                        <LabelStyle FontAngle="0" />
                                                                    </AxisX>
                                                                </DCWC:ChartArea>
                                                            </ChartAreas>
                                                        </DCWC:Chart>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Black NPD
                                                        <br />
                                                        <DCWC:Chart ID="CHRT_BlackNPD" runat="server" Width="450px" Height="385" ImageType="Jpeg"
                                                            ImageUrl="~/UploadFiles/temp/SingleMeasurement_Page2_BlackNPD" BackGradientType="Center"
                                                            Palette="Kindergarten" BackGradientEndColor="White" BorderLineColor="#808080">
                                                            <Titles>
                                                                <DCWC:Title Text="Black NPD" Alignment="TopCenter" Font="Verdana, 8pt, style=Bold">
                                                                </DCWC:Title>
                                                            </Titles>
                                                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                                                SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                                            <UI>
                                                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                        FrameBackColor="SteelBlue"></BorderSkin>
                                                                </Toolbar>
                                                            </UI>
                                                            <Series>
                                                                <DCWC:Series Name="Series1" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                    MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                                </DCWC:Series>
                                                                <DCWC:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                                                    Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                                    BorderWidth="2" Name="Series2">
                                                                </DCWC:Series>
                                                            </Series>
                                                            <ChartAreas>
                                                                <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                                                    BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                    <Position Height="80" Width="90" X="0" Y="15" />
                                                                    <AxisY LabelsAutoFit="False" Title="Density" TitleFont="Verdana, 8pt, style=Bold" Interval="Auto">
                                                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                                                    </AxisY>
                                                                    <AxisX LabelsAutoFit="False" Margin="False" Interval="Auto" Title="">
                                                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                                                        <LabelStyle FontAngle="0" />
                                                                    </AxisX>
                                                                </DCWC:ChartArea>
                                                            </ChartAreas>
                                                        </DCWC:Chart>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Dotgain Actual
                                                        <br />
                                                        <DCWC:Chart ID="CHRT_DotgainActual" runat="server" Width="900px" Height="250px" ImageType="Jpeg"
                                                            ImageUrl="~/UploadFiles/temp/SingleMeasurement_Page3_CHRT_DotgainActual" BackGradientType="Center"
                                                            Palette="Kindergarten" BackGradientEndColor="White" BorderLineColor="#808080">                                                           
                                                            <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                                                SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                                            <UI>
                                                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                        FrameBackColor="SteelBlue"></BorderSkin>
                                                                </Toolbar>
                                                            </UI>
                                                            <Series>
                                                                <DCWC:Series Name="Series1" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                    Color="Cyan" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                    MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series2" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                    Color="Magenta" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                    MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series3" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                    Color="Yellow" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                    MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series4" ShowLabelAsValue="false" LabelFormat="0" ShowInLegend="false"
                                                                    Color="Black" BorderWidth="2" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                    MarkerStyle="Circle" MarkerSize="15" BackGradientEndColor="162, 164, 255">
                                                                </DCWC:Series>
                                                                <DCWC:Series MarkerSize="12" BackGradientEndColor="162, 164, 255" ShowInLegend="false"
                                                                    Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                                    BorderWidth="2" Name="Series5">
                                                                </DCWC:Series>
                                                            </Series>
                                                            <ChartAreas>
                                                                <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                                                    BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                    <Position Height="80" Width="90" X="0" Y="15" />
                                                                    <AxisY LabelsAutoFit="False" Title="" Interval="Auto">
                                                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                                                    </AxisY>
                                                                    <AxisX LabelsAutoFit="False" Margin="False" Interval="Auto" Title="">
                                                                        <MajorGrid LineColor="Gray"></MajorGrid>
                                                                        <LabelStyle FontAngle="0" />
                                                                    </AxisX>
                                                                </DCWC:ChartArea>
                                                            </ChartAreas>
                                                        </DCWC:Chart>
                                                    </td>
                                                </tr>
                                               <tr>
                                                    <td>
                                                        Dotgain Deviation<br />
                                                        <DCWC:Chart ID="CHART_DotgainDeviation" ImageType="Jpeg" runat="server" Width="380px" Height="450px" 
                                                             ImageUrl="~/UploadFiles/temp/SingleMeasurement_Page3_DotgainDeviation"
                                                            BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#FFFFFF"
                                                            BorderLineColor="#808080">
                                                            <Legends>
                                                                <DCWC:Legend Name="Default">
                                                                </DCWC:Legend>
                                                                <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                                                </DCWC:Legend>
                                                            </Legends>                                                           
                                                            <Series>
                                                                <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1"
                                                                    ShowInLegend="false">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series1" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1" CustomAttributes="DrawingStyle=Cylinder"
                                                                    ShowInLegend="false">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series2" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="2"
                                                                    ShowInLegend="false">
                                                                </DCWC:Series>
                                                                 <DCWC:Series Name="Series3" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                                                    ShowInLegend="false">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series4" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                                                    ShowInLegend="false">
                                                                </DCWC:Series>
                                                            </Series>
                                                            <BorderSkin SkinStyle="None"></BorderSkin>
                                                            <UI>
                                                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                        FrameBackColor="SteelBlue"></BorderSkin>
                                                                </Toolbar>
                                                            </UI>
                                                            <ChartAreas>
                                                                <DCWC:ChartArea Name="ChartArea1" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                                                    BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                    <Area3DStyle YAngle="10" XAngle="10" RightAngleAxes="False" WallWidth="0" Clustered="true">
                                                                    </Area3DStyle>
                                                                    <Position Height="99" Width="94" X="0" Y="2" />
                                                                    <AxisY Enabled="True">
                                                                        <MajorGrid LineColor="Gray" />
                                                                        <SubAxes>
                                                                            <DCWC:SubAxis>
                                                                            </DCWC:SubAxis>
                                                                        </SubAxes>
                                                                    </AxisY>
                                                                    <AxisX Enabled="True" Interval="Auto">
                                                                        <MajorGrid LineColor="Gray" />
                                                                        <SubAxes>
                                                                            <DCWC:SubAxis>
                                                                            </DCWC:SubAxis>
                                                                        </SubAxes>
                                                                    </AxisX>
                                                                    <AxisX2 Enabled="True" Interval="Auto" Minimum="0" Maximum="1">
                                                                        <LabelStyle ShowEndLabels="false" />
                                                                        <MajorGrid LineColor="Beige" />
                                                                        <SubAxes>
                                                                            <DCWC:SubAxis>
                                                                            </DCWC:SubAxis>
                                                                        </SubAxes>
                                                                    </AxisX2>
                                                                </DCWC:ChartArea>
                                                            </ChartAreas>
                                                        </DCWC:Chart>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Density Deviation<br />
                                                        <DCWC:Chart ID="CHRT_DensityDeviation" ImageType="Jpeg" runat="server" Width="380px" Height="450px" 
                                                             ImageUrl="~/UploadFiles/temp/SingleMeasurement_Page3_DensityDeviation"
                                                            BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#FFFFFF"
                                                            BorderLineColor="#808080">
                                                            <Legends>
                                                                <DCWC:Legend Name="Default">
                                                                </DCWC:Legend>
                                                                <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                                                </DCWC:Legend>
                                                            </Legends>                                                           
                                                            <Series>
                                                                <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1"
                                                                    ShowInLegend="false">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series1" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1" CustomAttributes="DrawingStyle=Cylinder"
                                                                    ShowInLegend="false">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series2" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="2"
                                                                    ShowInLegend="false">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series3" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                                                    ShowInLegend="false">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series4" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                                                    ShowInLegend="false">
                                                                </DCWC:Series>
                                                            </Series>
                                                            <BorderSkin SkinStyle="None"></BorderSkin>
                                                            <UI>
                                                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                        FrameBackColor="SteelBlue"></BorderSkin>
                                                                </Toolbar>
                                                            </UI>
                                                            <ChartAreas>
                                                                <DCWC:ChartArea Name="ChartArea1" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                                                    BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                    <Area3DStyle YAngle="10" XAngle="10" RightAngleAxes="False" WallWidth="0" Clustered="true">
                                                                    </Area3DStyle>
                                                                    <Position Height="99" Width="94" X="0" Y="2" />
                                                                    <AxisY Enabled="True">
                                                                        <MajorGrid LineColor="Beige" />
                                                                        <SubAxes>
                                                                            <DCWC:SubAxis>
                                                                            </DCWC:SubAxis>
                                                                        </SubAxes>
                                                                    </AxisY>
                                                                    <AxisX Enabled="True" Interval="Auto">
                                                                        <MajorGrid LineColor="Beige" />
                                                                        <SubAxes>
                                                                            <DCWC:SubAxis>
                                                                            </DCWC:SubAxis>
                                                                        </SubAxes>
                                                                    </AxisX>
                                                                    <AxisX2 Enabled="True" Interval="Auto" Minimum="0" Maximum="1">
                                                                        <LabelStyle ShowEndLabels="false" />
                                                                        <MajorGrid LineColor="Beige" />
                                                                        <SubAxes>
                                                                            <DCWC:SubAxis>
                                                                            </DCWC:SubAxis>
                                                                        </SubAxes>
                                                                    </AxisX2>
                                                                </DCWC:ChartArea>
                                                            </ChartAreas>
                                                        </DCWC:Chart>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Print Contrast Deviation<br />
                                                        <DCWC:Chart ID="CHRT_PrintContrastDeviation" ImageType="Jpeg" runat="server" Width="380px" Height="450px" 
                                                         ImageUrl="~/UploadFiles/temp/SingleMeasurement_Page3_PrintContrastDeviation"
                                                            BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#FFFFFF"
                                                            BorderLineColor="#808080">
                                                            <Legends>
                                                                <DCWC:Legend Name="Default">
                                                                </DCWC:Legend>
                                                                <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                                                </DCWC:Legend>
                                                            </Legends>                                                          
                                                            <Series>
                                                                <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1"
                                                                    ShowInLegend="false">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series1" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1" CustomAttributes="DrawingStyle=Cylinder"
                                                                    ShowInLegend="false">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series2" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="2"
                                                                    ShowInLegend="false">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series3" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                                                    ShowInLegend="false">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series4" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                                                    ShowInLegend="false">
                                                                </DCWC:Series>
                                                            </Series>
                                                            <BorderSkin SkinStyle="None"></BorderSkin>
                                                            <UI>
                                                                <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                    <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                        FrameBackColor="SteelBlue"></BorderSkin>
                                                                </Toolbar>
                                                            </UI>
                                                            <ChartAreas>
                                                                <DCWC:ChartArea Name="ChartArea1" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                                                    BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                    <Area3DStyle YAngle="10" XAngle="10" RightAngleAxes="False" WallWidth="0" Clustered="true">
                                                                    </Area3DStyle>
                                                                    <Position Height="99" Width="94" X="0" Y="2" />
                                                                    <AxisY Enabled="True">
                                                                        <MajorGrid LineColor="Beige" />
                                                                        <SubAxes>
                                                                            <DCWC:SubAxis>
                                                                            </DCWC:SubAxis>
                                                                        </SubAxes>
                                                                    </AxisY>
                                                                    <AxisX Enabled="True" Interval="Auto">
                                                                        <MajorGrid LineColor="Beige" />
                                                                        <SubAxes>
                                                                            <DCWC:SubAxis>
                                                                            </DCWC:SubAxis>
                                                                        </SubAxes>
                                                                    </AxisX>
                                                                    <AxisX2 Enabled="True" Interval="Auto" Minimum="0" Maximum="1">
                                                                        <LabelStyle ShowEndLabels="false" />
                                                                        <MajorGrid LineColor="Beige" />
                                                                        <SubAxes>
                                                                            <DCWC:SubAxis>
                                                                            </DCWC:SubAxis>
                                                                        </SubAxes>
                                                                    </AxisX2>
                                                                </DCWC:ChartArea>
                                                            </ChartAreas>
                                                        </DCWC:Chart>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
    <asp:HiddenField ID="HDFCPMeasurementGUID" runat="server" />
     <script language="javascript" type="text/javascript">
        function OpenMeasurementWindow(MeasurementGUID) {
            var CPMeasurementGUID = document.getElementById('<%=HDFCPMeasurementGUID.ClientID %>');            
            CPMeasurementGUID.value = MeasurementGUID;            
            document.getElementById('<%=BtnCreateReport.ClientID %>').click();
        }
    </script>
</asp:Content>
