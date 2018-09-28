<%@ Page Title="" Language="C#" MasterPageFile="~/Home.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="ReportJobRunCertification.aspx.cs" Inherits="ReportJobRunCertification" %>

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
                                                    &nbsp; Job Run Certification Report
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblError" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="TR_Company" runat="server">
                                                <td class="RADSEARCHBARCOMPANY" align="left">
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td valign="middle">
                                                                Show Errors
                                                            </td>
                                                            <td valign="middle">
                                                                <asp:CheckBox CssClass="TEXT" ID="chkShowErrors" AutoPostBack="false" OnCheckedChanged="chkShowErrors_CheckedChanged"
                                                                    TextAlign="Left" runat="server" Text="" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <rad:RadGrid ID="GridJobReportPress" runat="server" AllowFilteringByColumn="True"
                                                        AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" EnableAJAX="True"
                                                        GridLines="None" Width="100%" PageSize="25" OnNeedDataSource="GridJobReportPress_NeedDataSource"
                                                        Skin="Green" OnItemDataBound="GridJobReportPress_ItemDataBound" OnItemCreated="GridJobReportPress_ItemCreated"
                                                        OnItemCommand="GridJobReportPress_ItemCommand">
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
                                                                    UniqueName="JobStatus" SortAscImageUrl="~/RadControls/Grid/Skins/Green/SortAsc.gif"
                                                                    SortExpression="Status" FilterImageUrl="~/RadControls/Grid/Skins/Green/Filter.gif"
                                                                    DataField="Status" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <%-- <%# DataBinder.Eval(Container.DataItem, "Status") + "&nbsp;&nbsp;&nbsp;<b>" + Eval("ErrorRate") + "%</b>"%>--%>
                                                                        &nbsp;
                                                                        <img id="ibtnStatus" runat="server" style="cursor: pointer; width: 20px; height: 20px" />
                                                                        &nbsp;&nbsp;&nbsp;<b>
                                                                            <%# Eval("ErrorRate")%>
                                                                            %</b>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="8%"></HeaderStyle>
                                                                </custom:FilteringColumn>
                                                                <rad:GridBoundColumn DataField="Status" DataType="System.String" HeaderText="Status"
                                                                    UniqueName="hdnStatus" Visible="false">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridTemplateColumn HeaderText="Create Report" AllowFiltering="false" UniqueName="View"
                                                                    ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <%--<img id="ImgView" runat="server" src="~/Images/view-icon-1.gif" style="cursor: pointer"
                                                                                    title="View in same window" />
                                                                                &nbsp;
                                                                                <img id="ImgView_New" runat="server" src="~/Images/popup.gif" style="cursor: pointer"
                                                                                    title="View in new window" />--%>
                                                                        <img id="imgCreateReport" runat="server" src="~/Images/status-icon.gif" style="cursor: pointer"
                                                                            title="Create Report" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="7%"></HeaderStyle>
                                                                </rad:GridTemplateColumn>
                                                                <rad:GridBoundColumn DataField="Job" HeaderText="Job No" UniqueName="Job" HeaderStyle-Width="11%"
                                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                </rad:GridBoundColumn>
                                                                <custom:FilterColumnByDate UniqueName="FirstSheet" DataField="FirstSheet" HeaderText="First Sheet"
                                                                    SortExpression="FirstSheet" HeaderStyle-Width="14%">
                                                                    <HeaderStyle />
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "FirstSheet")%></ItemTemplate>
                                                                </custom:FilterColumnByDate>
                                                                <custom:FilterColumnByDate UniqueName="LastSheet" DataField="LastSheet" HeaderText="Last Sheet"
                                                                    SortExpression="LastSheet" HeaderStyle-Width="14%">
                                                                    <HeaderStyle />
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "LastSheet")%></ItemTemplate>
                                                                </custom:FilterColumnByDate>
                                                                <rad:GridBoundColumn DataField="CPDeviceDesc" HeaderText="Device" UniqueName="CPDeviceDesc"
                                                                    HeaderStyle-Width="12%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="DongleSerial" HeaderText="Workstation" UniqueName="DongleDescription"
                                                                    HeaderStyle-Width="12%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="BranchName" HeaderText="Branch Name" UniqueName="BranchName"
                                                                    HeaderStyle-Width="12%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="Date" Visible="false">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="TIME" Visible="false">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="GUID" Visible="false">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="CPMeasurementID" Visible="false">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="CompanyID" Visible="false">
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
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="CPDeviceTypeID" Visible="false">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="StatusID" Visible="false">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridBoundColumn DataField="CPSerialNUM" Visible="false">
                                                                </rad:GridBoundColumn>
                                                                <rad:GridTemplateColumn Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCPDeviceTypeID" runat="server" Text='<%# Eval("CPDeviceTypeID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </rad:GridTemplateColumn>
                                                                <rad:GridTemplateColumn Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblGUID" runat="server" Text='<%# Eval("GUID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </rad:GridTemplateColumn>
                                                                <rad:GridTemplateColumn Visible="false">
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
                                                                </rad:GridTemplateColumn>
                                                                <rad:GridTemplateColumn Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCPMeasurementID" runat="server" Text='<%# Eval("CPMeasurementID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </rad:GridTemplateColumn>
                                                                <rad:GridTemplateColumn Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblJobTypeID" runat="server" Text='<%# Eval("JobTypeID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </rad:GridTemplateColumn>
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
                                                                        <img id="Img1" runat="server" src="~/Images/Makeready.png" style="width: 20px; height: 20px"
                                                                            align="absmiddle" />
                                                                        Make Ready &nbsp;
                                                                        <img id="Img2" runat="server" src="~/Images/Production.png" style="width: 20px; height: 20px"
                                                                            align="absmiddle" />
                                                                        Production &nbsp;
                                                                        <img id="Img3" runat="server" src="~/Images/Complete.png" style="width: 20px; height: 20px"
                                                                            align="absmiddle" />
                                                                        Complete &nbsp;
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
                            <asp:Button ID="BtnCreateReport" runat="server" Text="Create Report" OnClick="BtnCreateReport_Click"
                                Style="display: none" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <%------------------------ Report Generate Structure -------------------------%>
                                    <asp:Panel Style="width: 100%" ID="PNL_Result" runat="server" Visible="false">
                                        <div style="overflow: auto; height: 400px">
                                            <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                                                <tr>
                                                    <td align="center" valign="top">
                                                        Target Versus Actual AB Plot
                                                        <br />
                                                        <DCWC:Chart ID="CHART_TargetVersusActualAB" runat="server" BackImageMode="Scaled"
                                                            BackImage="~/Images/JobRunCertification_PointBack.gif" Height="250px" ImageType="Jpeg"
                                                            Width="250px" ImageUrl="~/UploadFiles/temp/JobRunCertification_TargetVersusActualAB_1">
                                                            <Legends>
                                                                <DCWC:Legend Name="Default" Enabled="False">
                                                                </DCWC:Legend>
                                                            </Legends>
                                                            <Series>
                                                                <DCWC:Series Name="Series0" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                    MarkerSize="12" MarkerStyle="Circle" MarkerColor="Green" Color="Green" ShadowOffset="1">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series1" ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker"
                                                                    MarkerSize="12" MarkerStyle="Circle" MarkerColor="Green" MarkerBorderColor="Black"
                                                                    MarkerBorderWidth="1" Color="Green" ShadowOffset="1">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series2" ChartType="FastPoint" CustomAttributes="PolarDrawingStyle=Marker"
                                                                    MarkerSize="12" MarkerStyle="Circle" MarkerColor="Red" Color="Red" ShadowOffset="1">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series3" ChartType="FastPoint" CustomAttributes="PolarDrawingStyle=Marker"
                                                                    MarkerSize="12" MarkerStyle="Circle" MarkerBorderColor="Black" ShadowOffset="1">
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
                                                        <DCWC:Chart ID="ChartResultSummary_PatchError" runat="server" Width="350px" Height="55px"
                                                            ImageUrl="~/UploadFiles/temp/JobRunCertification_ResultSummary_PatchError" ImageType="Jpeg">
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
                                                                    BorderWidth="0" Color="#D4E8FA" Font="Microsoft Sans Serif, 7pt, style=Underline"
                                                                    Label="" CustomAttributes="LabelStyle=Top,DrawingStyle=Cylinder" ShowInLegend="true">
                                                                </DCWC:Series>
                                                            </Series>
                                                            <ChartAreas>
                                                                <DCWC:ChartArea Name="ChartArea1">
                                                                    <Area3DStyle YAngle="0" XAngle="0" Enable3D="true" RightAngleAxes="False" WallWidth="0"
                                                                        Clustered="True"></Area3DStyle>
                                                                    <AxisX Interval="Auto" Enabled="true" LabelsAutoFit="False" Reverse="true">
                                                                        <MajorGrid LineColor="Beige"></MajorGrid>
                                                                        <LabelStyle Font="Microsoft Sans Serif, 7pt" />
                                                                    </AxisX>
                                                                    <AxisY Interval="25" Enabled="True" LabelsAutoFit="False">
                                                                        <MajorGrid LineColor="Beige"></MajorGrid>
                                                                        <LabelStyle Format="###" Font="Arial, 7pt"  FontColor="Black" />
                                                                    </AxisY>
                                                                </DCWC:ChartArea>
                                                            </ChartAreas>
                                                        </DCWC:Chart>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <DCWC:Chart ID="ChartResultSummary_DeltaEValue" runat="server" Width="340px" Height="423px"
                                                            ImageUrl="~/UploadFiles/temp/JobRunCertification_ResultSummary_DeltaEValue" ImageType="Jpeg">
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
                                                                <DCWC:Series Name="Series1" BorderWidth="0" BorderColor="Gray" CustomAttributes="DrawingStyle=Cylinder"
                                                                    ShowInLegend="false">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series2" BorderWidth="0" Color="Transparent" CustomAttributes="DrawingStyle=Cylinder"
                                                                    ShowInLegend="false" MarkerImage="~/Images/Chart-Red-Image-Vertical.png">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series3" BorderWidth="0" Color="Transparent" CustomAttributes="DrawingStyle=Cylinder"
                                                                    ShowInLegend="false" MarkerImage="~/Images/Chart-Yellow-Image-Vertical.png">
                                                                </DCWC:Series>
                                                            </Series>
                                                            <ChartAreas>
                                                                <DCWC:ChartArea Name="ChartArea1">
                                                                    <Area3DStyle YAngle="0" XAngle="0" Enable3D="true" RightAngleAxes="False" WallWidth="0"
                                                                        Clustered="True"></Area3DStyle>
                                                                    <AxisX Interval="Auto" Enabled="true" LabelsAutoFit="False" Reverse="true">
                                                                        <MajorGrid Enabled="false" LineColor="Beige"></MajorGrid>
                                                                        <MajorTickMark Enabled="false" />
                                                                        <LabelStyle Font="Microsoft Sans Serif, 7pt" />
                                                                    </AxisX>
                                                                    <AxisY Interval="10" Enabled="True" LabelsAutoFit="False">
                                                                        <MajorGrid LineColor="Beige"></MajorGrid>
                                                                        <LabelStyle Format="###" Font="Arial, 7pt" FontColor="Black"/>                                                                        
                                                                    </AxisY>
                                                                </DCWC:ChartArea>
                                                            </ChartAreas>
                                                        </DCWC:Chart>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        ------------------------- Page 2 -------------------------------
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <DCWC:Chart ID="CHART_AB25" runat="server" BackImageMode="Scaled" BackImage="~/Images/FarbKreis_LAB3_150.gif"
                                                                        Height="250px" ImageType="Jpeg" Width="250px" ImageUrl="~/UploadFiles/temp/JobRunCertification_Page2_AB25">
                                                                        <Legends>
                                                                            <DCWC:Legend Name="Default" Enabled="False">
                                                                            </DCWC:Legend>
                                                                        </Legends>
                                                                        <Series>
                                                                            <DCWC:Series ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="18"
                                                                             MarkerStyle="Circle" MarkerColor="Green" Name="Series1" ShadowOffset="1">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="18" BorderColor="Black"  BorderStyle="Solid"
                                                                              MarkerStyle="Circle" MarkerColor="Gray" Name="Default" ShadowOffset="1">
                                                                            </DCWC:Series>
                                                                        </Series>
                                                                        <BorderSkin PageColor="AliceBlue" />
                                                                        <Titles>
                                                                            <DCWC:Title Name="Title1">
                                                                            </DCWC:Title>
                                                                        </Titles>
                                                                        <ChartAreas>
                                                                            <DCWC:ChartArea BackColor="Transparent" BorderColor="" Name="Default">
                                                                                <AxisY LineColor="120, 0, 0, 0" TitleAlignment="Near" Maximum="10" Minimum="-10"
                                                                                    LineStyle="NotSet">
                                                                                    <MajorGrid Enabled="False" />
                                                                                    <MajorTickMark Enabled="false" />
                                                                                    <LabelStyle Enabled="False" />
                                                                                </AxisY>
                                                                                <AxisX Margin="False" TitleAlignment="Near" Maximum="10" Minimum="-10" LineStyle="NotSet">
                                                                                    <MajorGrid Enabled="False" />
                                                                                    <MajorTickMark Enabled="false" />
                                                                                    <LabelStyle Enabled="False" />
                                                                                </AxisX>
                                                                            </DCWC:ChartArea>
                                                                        </ChartAreas>
                                                                    </DCWC:Chart>
                                                                </td>
                                                                <td>
                                                                    <DCWC:Chart ID="CHART_AB50" runat="server" BackImageMode="Scaled" BackImage="~/Images/FarbKreis_LAB3_150.gif"
                                                                        Height="250px" ImageType="Jpeg" Width="250px" ImageUrl="~/UploadFiles/temp/JobRunCertification_Page2_AB50">
                                                                        <Legends>
                                                                            <DCWC:Legend Name="Default" Enabled="False">
                                                                            </DCWC:Legend>
                                                                        </Legends>
                                                                        <Series>
                                                                            <DCWC:Series ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="18"
                                                                                MarkerStyle="Circle" MarkerColor="Green" Name="Series1" ShadowOffset="1">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="18" BorderColor="Black"  BorderStyle="Solid"
                                                                                MarkerStyle="Circle" MarkerColor="Gray" Name="Default" ShadowOffset="1">
                                                                            </DCWC:Series>
                                                                        </Series>
                                                                        <BorderSkin PageColor="AliceBlue" />
                                                                        <Titles>
                                                                            <DCWC:Title Name="Title1">
                                                                            </DCWC:Title>
                                                                        </Titles>
                                                                        <ChartAreas>
                                                                            <DCWC:ChartArea BackColor="Transparent" BorderColor="" Name="Default">
                                                                                <AxisY LineColor="120, 0, 0, 0" TitleAlignment="Near" Maximum="10" Minimum="-10"
                                                                                    LineStyle="NotSet">
                                                                                    <MajorGrid Enabled="False" />
                                                                                    <MajorTickMark Enabled="false" />
                                                                                    <LabelStyle Enabled="False" />
                                                                                </AxisY>
                                                                                <AxisX Margin="False" TitleAlignment="Near" Maximum="10" Minimum="-10" LineStyle="NotSet">
                                                                                    <MajorGrid Enabled="False" />
                                                                                    <MajorTickMark Enabled="false" />
                                                                                    <LabelStyle Enabled="False" />
                                                                                </AxisX>
                                                                            </DCWC:ChartArea>
                                                                        </ChartAreas>
                                                                    </DCWC:Chart>
                                                                </td>
                                                                <td>
                                                                    <DCWC:Chart ID="CHART_AB75" runat="server" BackImageMode="Scaled" BackImage="~/Images/FarbKreis_LAB3_150.gif"
                                                                        Height="250px" ImageType="Jpeg" Width="250px" ImageUrl="~/UploadFiles/temp/JobRunCertification_Page2_AB75">
                                                                        <Legends>
                                                                            <DCWC:Legend Name="Default" Enabled="False">
                                                                            </DCWC:Legend>
                                                                        </Legends>
                                                                        <Series>
                                                                            <DCWC:Series ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="18"
                                                                                MarkerStyle="Circle" MarkerColor="Green" Name="Series1" ShadowOffset="1">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series ChartType="Polar" CustomAttributes="PolarDrawingStyle=Marker" MarkerSize="18" BorderColor="Black"  BorderStyle="Solid"
                                                                                MarkerStyle="Circle" MarkerColor="Gray" Name="Default" ShadowOffset="1">
                                                                            </DCWC:Series>
                                                                        </Series>
                                                                        <BorderSkin PageColor="AliceBlue" />
                                                                        <Titles>
                                                                            <DCWC:Title Name="Title1" Alignment="TopCenter" Color="Black">
                                                                            </DCWC:Title>
                                                                        </Titles>
                                                                        <ChartAreas>
                                                                            <DCWC:ChartArea BackColor="Transparent" BorderColor="" Name="Default">
                                                                                <AxisY LineColor="120, 0, 0, 0" TitleAlignment="Near" Maximum="10" Minimum="-10"
                                                                                    LineStyle="NotSet">
                                                                                    <MajorGrid Enabled="False" />
                                                                                    <MajorTickMark Enabled="false" />
                                                                                    <LabelStyle Enabled="False" />
                                                                                </AxisY>
                                                                                <AxisX Margin="False" TitleAlignment="Near" Maximum="10" Minimum="-10" LineStyle="NotSet">
                                                                                    <MajorGrid Enabled="False" />
                                                                                    <MajorTickMark Enabled="false" />
                                                                                    <LabelStyle Enabled="False" />
                                                                                </AxisX>
                                                                            </DCWC:ChartArea>
                                                                        </ChartAreas>
                                                                    </DCWC:Chart>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <DCWC:Chart ID="CHRT_KNPD" runat="server" Width="450px" Height="400px" ImageType="Jpeg"
                                                            ImageUrl="~/UploadFiles/temp/JobRunCertification_Page2_KNPD" BackGradientType="Center"
                                                            Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                                                            <Legends>
                                                                <DCWC:Legend Name="Default" Enabled="false">
                                                                </DCWC:Legend>
                                                            </Legends>
                                                            <Titles>
                                                                <DCWC:Title Text="Black NPD" Alignment="TopCenter" Font="Microsoft Sans Serif, 8pt, style=Bold">
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
                                                                <DCWC:Series Name="Series1" Color="155, 0, 0, 0" BorderWidth="5" ChartType="Spline"
                                                                    MarkerBorderColor="64, 64, 64" ShowLabelAsValue="true" MarkerStyle="Circle" MarkerSize="10" BackGradientEndColor="162, 164, 255">
                                                                </DCWC:Series>
                                                                <DCWC:Series MarkerSize="10" BackGradientEndColor="162, 164, 255" Color="Green" ChartType="Spline"
                                                                    MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" BorderWidth="5" Name="Series2">
                                                                </DCWC:Series>
                                                            </Series>
                                                            <ChartAreas>
                                                                <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                                                    BackColor="White" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                    <Position Height="80" Width="90" X="0" Y="15" />
                                                                    <AxisY LabelsAutoFit="False">
                                                                        <MajorGrid LineColor="Beige"></MajorGrid>
                                                                    </AxisY>
                                                                    <AxisX Margin="False" LabelsAutoFit="False" Interval="Auto">
                                                                        <MajorGrid LineColor="Beige"></MajorGrid>
                                                                        <LabelStyle FontAngle="0" />
                                                                    </AxisX>
                                                                </DCWC:ChartArea>
                                                            </ChartAreas>
                                                        </DCWC:Chart>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <DCWC:Chart ID="CHRT_GrayNPD" runat="server" Width="450px" Height="400px" ImageType="Jpeg"
                                                            ImageUrl="~/UploadFiles/temp/JobRunCertification_Page2_GrayNPD" BackGradientType="Center"
                                                            Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                                                            <Legends>
                                                                <DCWC:Legend Name="Default" Enabled="false">
                                                                </DCWC:Legend>
                                                            </Legends>
                                                            <Titles>
                                                                <DCWC:Title Text="Gray NPD" Font="Microsoft Sans Serif, 8pt, style=Bold">
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
                                                                <DCWC:Series Name="Series1" Color="Gray" ShowLabelAsValue="true" BorderWidth="5" ChartType="Spline" MarkerBorderColor="64, 64, 64"
                                                                    MarkerStyle="Circle" MarkerSize="10">
                                                                </DCWC:Series>
                                                                <DCWC:Series Name="Series2" BackGradientEndColor="162, 164, 255" Color="Green" ChartType="Spline"
                                                                    MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" MarkerSize="10" BorderWidth="5">
                                                                </DCWC:Series>
                                                            </Series>
                                                            <ChartAreas>
                                                                <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                                                    BackColor="White" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                    <Position Height="80" Width="90" X="0" Y="15" />
                                                                    <AxisY LabelsAutoFit="False">
                                                                        <MajorGrid LineColor="Beige"></MajorGrid>
                                                                    </AxisY>
                                                                    <AxisX Margin="False" LabelsAutoFit="False">
                                                                        <MajorGrid LineColor="Beige"></MajorGrid>
                                                                        <LabelStyle FontAngle="0" />
                                                                    </AxisX>
                                                                </DCWC:ChartArea>
                                                            </ChartAreas>
                                                        </DCWC:Chart>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        -----------------------------------------------------------------------------------
                                                        Page 3------------------------------------------
                                                    </td>
                                                </tr>
                                                <%-- <tr>
                                                    <td>
                                                        <table id="Table1" cellpadding="0" cellspacing="0" border="0" style="width: 100%;
                                                            margin-top: 5px" runat="server" class="popup-table">
                                                            <tr>
                                                                <td class="popup-top-bar">
                                                                    <b>Trend Delta E Summary</b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table cellspacing="2">
                                                                        <tr>
                                                                            <td>
                                                                                <DCWC:Chart ID="CHRT_TrendDeltaE" runat="server" ImageType="Jpeg" Width="875px" Height="140px"
                                                                                    ImageUrl="~/UploadFiles/temp/JobRunCertification_Page3_TrendDeltaE" BackGradientType="Center"
                                                                                    Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                                                                                    <Legends>
                                                                                        <DCWC:Legend Name="Default">
                                                                                        </DCWC:Legend>
                                                                                        <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                                                                        </DCWC:Legend>
                                                                                    </Legends>
                                                                                    <Series>
                                                                                        <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="2"
                                                                                            ShowInLegend="false">
                                                                                        </DCWC:Series>
                                                                                        <DCWC:Series Name="Series1" Color="Green" ChartArea="ChartArea1" BorderWidth="2"
                                                                                            ShowInLegend="false">
                                                                                        </DCWC:Series>
                                                                                        <DCWC:Series Name="Series2" Color="Red" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false">
                                                                                        </DCWC:Series>
                                                                                        <DCWC:Series Name="Series3" Color="Blue" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false">
                                                                                        </DCWC:Series>
                                                                                    </Series>
                                                                                    <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                                                                        SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                                                                    <UI>
                                                                                        <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                                            <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                                                FrameBackColor="SteelBlue"></BorderSkin>
                                                                                        </Toolbar>
                                                                                    </UI>
                                                                                    <ChartAreas>
                                                                                        <DCWC:ChartArea Name="ChartArea1">
                                                                                            <Position Height="99" Width="98" X="0" Y="10" />
                                                                                            <AxisY Enabled="True">
                                                                                                <MajorGrid LineColor="Beige" />
                                                                                                <SubAxes>
                                                                                                    <DCWC:SubAxis Title="&Delta; E">
                                                                                                    </DCWC:SubAxis>
                                                                                                </SubAxes>
                                                                                            </AxisY>
                                                                                            <AxisX Enabled="True">
                                                                                                <MajorGrid LineColor="Beige" />
                                                                                                <SubAxes>
                                                                                                    <DCWC:SubAxis Title="Sheet Number">
                                                                                                    </DCWC:SubAxis>
                                                                                                </SubAxes>
                                                                                            </AxisX>
                                                                                        </DCWC:ChartArea>
                                                                                    </ChartAreas>
                                                                                </DCWC:Chart>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td>
                                                        <table id="Table3" cellpadding="0" cellspacing="0" border="0" style="width: 100%;
                                                            margin-top: 5px" runat="server" class="popup-table">
                                                            <tr>
                                                                <td class="popup-top-bar">
                                                                    <b>Zone Delta E Summary</b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <DCWC:Chart ID="CHRT_ZoneDeltaE" runat="server" Width="900px" ImageType="Jpeg" Height="210px"
                                                                        ImageUrl="~/UploadFiles/temp/JobRunCertification_Page3_ZoneDeltaE" BackGradientType="Center"
                                                                        Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
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
                                                                            <DCWC:Series Name="Series1" ShowLabelAsValue="true" Color="200, 0, 0, 0" BorderColor="100, 0 , 0 , 0"
                                                                                BackGradientType="VerticalCenter" ChartArea="ChartArea1" BorderWidth="1" ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                        </Series>
                                                                        <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                                                            SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                                                        <UI>
                                                                            <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                                <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                                    FrameBackColor="SteelBlue"></BorderSkin>
                                                                            </Toolbar>
                                                                        </UI>
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
                                                                                <AxisX Enabled="True" Interval="1">
                                                                                    <MajorGrid LineColor="Beige" />
                                                                                    <SubAxes>
                                                                                        <DCWC:SubAxis Title="Zone Number">
                                                                                        </DCWC:SubAxis>
                                                                                    </SubAxes>
                                                                                </AxisX>
                                                                            </DCWC:ChartArea>
                                                                        </ChartAreas>
                                                                    </DCWC:Chart>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0" border="0">
                                                            <tr>
                                                                <td>
                                                                    <DCWC:Chart ID="CHRT_DensityDeviationP" ImageType="Jpeg" runat="server" Height="165px"
                                                                        Width="323px" ImageUrl="~/UploadFiles/temp/JobRunCertification_DensitometricSummary_DensityDeviationP"
                                                                        BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#FFFFFF"
                                                                        BorderLineColor="#808080">
                                                                        <Legends>
                                                                            <DCWC:Legend Name="Default">
                                                                            </DCWC:Legend>
                                                                            <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                                                            </DCWC:Legend>
                                                                        </Legends>
                                                                        <Titles>
                                                                            <DCWC:Title Text="Density Deviation" Font="Microsoft Sans Serif, 8pt, style=Bold">
                                                                            </DCWC:Title>
                                                                        </Titles>
                                                                        <Series>
                                                                            <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series1" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series2" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
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
                                                                                BackColor="White" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                                <Area3DStyle YAngle="10" XAngle="10" RightAngleAxes="False" WallWidth="0" Clustered="true">
                                                                                </Area3DStyle>
                                                                                <Position Height="99" Width="98" X="0" Y="10" />
                                                                                <AxisY Enabled="True">
                                                                                    <MajorGrid LineColor="Beige" />
                                                                                    <SubAxes>
                                                                                        <DCWC:SubAxis>
                                                                                        </DCWC:SubAxis>
                                                                                    </SubAxes>
                                                                                </AxisY>
                                                                                <AxisX Enabled="True" Interval="1">
                                                                                    <MajorGrid LineColor="Beige" />
                                                                                    <SubAxes>
                                                                                        <DCWC:SubAxis>
                                                                                        </DCWC:SubAxis>
                                                                                    </SubAxes>
                                                                                </AxisX>
                                                                                <AxisX2 Enabled="True" Interval="1" Minimum="0" Maximum="1">
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
                                                                    <DCWC:Chart ID="CHART_DotgainDeviationP" ImageType="Jpeg" runat="server" Height="165px"
                                                                        Width="323px" ImageUrl="~/UploadFiles/temp/JobRunCertification_DensitometricSummary_DotgainDeviationP"
                                                                        BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#FFFFFF"
                                                                        BorderLineColor="#808080">
                                                                        <Legends>
                                                                            <DCWC:Legend Name="Default">
                                                                            </DCWC:Legend>
                                                                            <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                                                            </DCWC:Legend>
                                                                        </Legends>
                                                                        <Titles>
                                                                            <DCWC:Title Text="Dotgain Deviation" Font="Microsoft Sans Serif, 8pt, style=Bold">
                                                                            </DCWC:Title>
                                                                        </Titles>
                                                                        <Series>
                                                                            <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series1" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series2" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series3" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series4" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series5" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
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
                                                                                BackColor="White" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                                <Area3DStyle YAngle="10" XAngle="10" RightAngleAxes="False" WallWidth="0" Clustered="true">
                                                                                </Area3DStyle>
                                                                                <Position Height="99" Width="98" X="0" Y="10" />
                                                                                <AxisY Enabled="True">
                                                                                    <MajorGrid LineColor="Beige" />
                                                                                    <SubAxes>
                                                                                        <DCWC:SubAxis>
                                                                                        </DCWC:SubAxis>
                                                                                    </SubAxes>
                                                                                </AxisY>
                                                                                <AxisX Enabled="True" Interval="1">
                                                                                    <MajorGrid LineColor="Beige" />
                                                                                    <SubAxes>
                                                                                        <DCWC:SubAxis>
                                                                                        </DCWC:SubAxis>
                                                                                    </SubAxes>
                                                                                </AxisX>
                                                                                <AxisX2 Enabled="True" Interval="1" Minimum="0" Maximum="1">
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
                                                                    <DCWC:Chart ID="CHART_PrintDeviationP" ImageType="Jpeg" runat="server" Height="165px"
                                                                        Width="323px" ImageUrl="~/UploadFiles/temp/JobRunCertification_DensitometricSummary_PrintDeviationP"
                                                                        BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#FFFFFF"
                                                                        BorderLineColor="#808080">
                                                                        <Legends>
                                                                            <DCWC:Legend Name="Default">
                                                                            </DCWC:Legend>
                                                                            <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                                                            </DCWC:Legend>
                                                                        </Legends>
                                                                        <Titles>
                                                                            <DCWC:Title Text="Print Contrast Deviation" Font="Microsoft Sans Serif, 8pt, style=Bold">
                                                                            </DCWC:Title>
                                                                        </Titles>
                                                                        <Series>
                                                                            <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series1" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series2" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
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
                                                                                BackColor="White" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                                <Area3DStyle YAngle="10" XAngle="10" RightAngleAxes="False" WallWidth="0" Clustered="true">
                                                                                </Area3DStyle>
                                                                                <Position Height="99" Width="98" X="0" Y="10" />
                                                                                <AxisY Enabled="True">
                                                                                    <MajorGrid LineColor="Beige" />
                                                                                    <SubAxes>
                                                                                        <DCWC:SubAxis>
                                                                                        </DCWC:SubAxis>
                                                                                    </SubAxes>
                                                                                </AxisY>
                                                                                <AxisX Enabled="True" Interval="1">
                                                                                    <MajorGrid LineColor="Beige" />
                                                                                    <SubAxes>
                                                                                        <DCWC:SubAxis>
                                                                                        </DCWC:SubAxis>
                                                                                    </SubAxes>
                                                                                </AxisX>
                                                                                <AxisX2 Enabled="True" Interval="1" Minimum="0" Maximum="1">
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
                                                                    <DCWC:Chart ID="CHART_TrapDeviationP" ImageType="Jpeg" runat="server" Height="165px"
                                                                        Width="323px" ImageUrl="~/UploadFiles/temp/JobRunCertification_DensitometricSummary_TrapDeviationP"
                                                                        BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#FFFFFF"
                                                                        BorderLineColor="#808080">
                                                                        <Legends>
                                                                            <DCWC:Legend Name="Default">
                                                                            </DCWC:Legend>
                                                                            <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                                                            </DCWC:Legend>
                                                                        </Legends>
                                                                        <Titles>
                                                                            <DCWC:Title Text="Trap Deviation" Font="Microsoft Sans Serif, 8pt, style=Bold">
                                                                            </DCWC:Title>
                                                                        </Titles>
                                                                        <Series>
                                                                            <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series1" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series2" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
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
                                                                                BackColor="White" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                                <Area3DStyle YAngle="10" XAngle="10" RightAngleAxes="False" WallWidth="0" Clustered="true">
                                                                                </Area3DStyle>
                                                                                <Position Height="99" Width="98" X="0" Y="10" />
                                                                                <AxisY Enabled="True">
                                                                                    <MajorGrid LineColor="Beige" />
                                                                                    <SubAxes>
                                                                                        <DCWC:SubAxis>
                                                                                        </DCWC:SubAxis>
                                                                                    </SubAxes>
                                                                                </AxisY>
                                                                                <AxisX Enabled="True" Interval="1">
                                                                                    <MajorGrid LineColor="Beige" />
                                                                                    <SubAxes>
                                                                                        <DCWC:SubAxis>
                                                                                        </DCWC:SubAxis>
                                                                                    </SubAxes>
                                                                                </AxisX>
                                                                                <AxisX2 Enabled="True" Interval="1" Minimum="0" Maximum="1">
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
                                                                    <DCWC:Chart ID="CHART_DensityDeviationNP" ImageType="Jpeg" runat="server" Height="165px"
                                                                        Width="323px" ImageUrl="~/UploadFiles/temp/JobRunCertification_DensitometricSummary_DensityDeviationNP"
                                                                        BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#FFFFFF"
                                                                        BorderLineColor="#808080">
                                                                        <Legends>
                                                                            <DCWC:Legend Name="Default">
                                                                            </DCWC:Legend>
                                                                            <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                                                            </DCWC:Legend>
                                                                        </Legends>
                                                                        <Titles>
                                                                            <DCWC:Title Text="Density Deviation" Font="Microsoft Sans Serif, 8pt, style=Bold">
                                                                            </DCWC:Title>
                                                                        </Titles>
                                                                        <Series>
                                                                            <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="1"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series1" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series2" BorderColor="100, 0 , 0 , 0" ChartArea="ChartArea1" BorderWidth="1"
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
                                                                                BackColor="White" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                                <Area3DStyle YAngle="10" XAngle="10" RightAngleAxes="False" WallWidth="0" Clustered="true">
                                                                                </Area3DStyle>
                                                                                <Position Height="99" Width="98" X="0" Y="10" />
                                                                                <AxisY Enabled="True">
                                                                                    <MajorGrid LineColor="Beige" />
                                                                                    <SubAxes>
                                                                                        <DCWC:SubAxis>
                                                                                        </DCWC:SubAxis>
                                                                                    </SubAxes>
                                                                                </AxisY>
                                                                                <AxisX Enabled="True" Interval="1">
                                                                                    <MajorGrid LineColor="Beige" />
                                                                                    <SubAxes>
                                                                                        <DCWC:SubAxis>
                                                                                        </DCWC:SubAxis>
                                                                                    </SubAxes>
                                                                                </AxisX>
                                                                                <AxisX2 Enabled="True" Interval="1" Minimum="0" Maximum="1">
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
                                                    </td>
                                                </tr>
                                            </table>
                                            <%-- ---------------- By Ajay on 29/04/2009 ------------------- --%>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <div id="CHART_DIV_SCROLL1" class="CHART_DIV_SCROLL2">
                                                            <!-- Trend Delta E  -->
                                                            <div class="CHART_DIV">
                                                                <div class="CHART_DIV_LEFT">
                                                                    <DCWC:Chart ID="CHRT_TrendDeltaEP" ImageType="Jpeg" runat="server" Width="850px"
                                                                        Height="125px" ImageUrl="~/UploadFiles/temp/JobRunCertification_TrendLine" BackGradientType="Center"
                                                                        Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                                                                        <Legends>
                                                                            <DCWC:Legend Name="Default">
                                                                            </DCWC:Legend>
                                                                            <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                                                            </DCWC:Legend>
                                                                        </Legends>
                                                                        <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                                                            SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                                                        <UI>
                                                                            <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                                <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                                    FrameBackColor="SteelBlue"></BorderSkin>
                                                                            </Toolbar>
                                                                        </UI>
                                                                        <Series>
                                                                            <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="2"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series1" Color="Green" ChartArea="ChartArea1" BorderWidth="2"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series2" Color="Red" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series3" Color="Blue" ChartArea="ChartArea1" BorderWidth="2" ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                        </Series>
                                                                        <ChartAreas>
                                                                            <DCWC:ChartArea Name="ChartArea1">
                                                                                <Position Height="90" Width="98" X="0" Y="10" />
                                                                                <AxisY Enabled="True">
                                                                                    <MajorGrid LineColor="Beige" />
                                                                                    <SubAxes>
                                                                                        <DCWC:SubAxis Title="Trendline">
                                                                                        </DCWC:SubAxis>
                                                                                    </SubAxes>
                                                                                </AxisY>
                                                                                <AxisX Enabled="True">
                                                                                    <MajorGrid LineColor="Beige" />
                                                                                </AxisX>
                                                                            </DCWC:ChartArea>
                                                                        </ChartAreas>
                                                                    </DCWC:Chart>
                                                                </div>
                                                            </div>
                                                            <!-- Trend Delta E  -->
                                                            <!-- Delta E -->
                                                            <div class="CHART_DIV">
                                                                <div class="CHART_DIV_LEFT">
                                                                    <DCWC:Chart ID="CHRT_DeltaEP" runat="server" ImageType="Jpeg" Width="850px" Height="125px"
                                                                        ImageUrl="~/UploadFiles/temp/JobRunCertification_DeltaE" BackGradientType="Center"
                                                                        Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                                                                        <Legends>
                                                                            <DCWC:Legend Name="Default">
                                                                            </DCWC:Legend>
                                                                            <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                                                            </DCWC:Legend>
                                                                        </Legends>
                                                                        <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                                                            SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                                                        <UI>
                                                                            <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                                <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                                    FrameBackColor="SteelBlue"></BorderSkin>
                                                                            </Toolbar>
                                                                        </UI>
                                                                        <Series>
                                                                            <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="2"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series1" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="2"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series2" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="2"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series3" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="2"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series4" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="2"
                                                                                ShowInLegend="false">
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
                                                                                <AxisX Enabled="True">
                                                                                    <MajorGrid LineColor="Beige" />
                                                                                </AxisX>
                                                                            </DCWC:ChartArea>
                                                                        </ChartAreas>
                                                                    </DCWC:Chart>
                                                                </div>
                                                            </div>
                                                            <!-- Delta E -->
                                                            <!-- Density -->
                                                            <div class="CHART_DIV">
                                                                <div class="CHART_DIV_LEFT">
                                                                    <DCWC:Chart ID="CHRT_DensityP" ImageType="Jpeg" runat="server" Width="850px" Height="125px"
                                                                        ImageUrl="~/UploadFiles/temp/JobRunCertification_Density" BackGradientType="Center"
                                                                        Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                                                                        <Legends>
                                                                            <DCWC:Legend Name="Default">
                                                                            </DCWC:Legend>
                                                                            <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                                                            </DCWC:Legend>
                                                                        </Legends>
                                                                        <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                                                            SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                                                        <UI>
                                                                            <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                                <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                                    FrameBackColor="SteelBlue"></BorderSkin>
                                                                            </Toolbar>
                                                                        </UI>
                                                                        <Series>
                                                                            <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="2"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series1" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="2"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series2" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="2"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series3" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="2"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series4" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="2"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                        </Series>
                                                                        <ChartAreas>
                                                                            <DCWC:ChartArea Name="ChartArea1">
                                                                                <Position Height="90" Width="98" X="0" Y="10" />
                                                                                <AxisY Enabled="True">
                                                                                    <MajorGrid LineColor="Beige" />
                                                                                    <SubAxes>
                                                                                        <DCWC:SubAxis Title="Density">
                                                                                        </DCWC:SubAxis>
                                                                                    </SubAxes>
                                                                                </AxisY>
                                                                                <AxisX Enabled="True">
                                                                                    <MajorGrid LineColor="Beige" />
                                                                                </AxisX>
                                                                            </DCWC:ChartArea>
                                                                        </ChartAreas>
                                                                    </DCWC:Chart>
                                                                </div>
                                                            </div>
                                                            <!-- Density -->
                                                            <!-- Dotgain -->
                                                            <div class="CHART_DIV">
                                                                <div class="CHART_DIV_LEFT">
                                                                    <DCWC:Chart ID="CHRT_DotgainP" runat="server" Width="850px" ImageType="Jpeg" Height="125px"
                                                                        ImageUrl="~/UploadFiles/temp/JobRunCertification_Dotgain" BackGradientType="Center"
                                                                        Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                                                                        <Legends>
                                                                            <DCWC:Legend Name="Default">
                                                                            </DCWC:Legend>
                                                                            <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                                                            </DCWC:Legend>
                                                                        </Legends>
                                                                        <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                                                            SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                                                        <UI>
                                                                            <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                                <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                                    FrameBackColor="SteelBlue"></BorderSkin>
                                                                            </Toolbar>
                                                                        </UI>
                                                                        <Series>
                                                                            <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="2"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series1" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="2"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series2" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="2"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series3" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="2"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series4" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="2"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                        </Series>
                                                                        <ChartAreas>
                                                                            <DCWC:ChartArea Name="ChartArea1">
                                                                                <Position Height="90" Width="98" X="0" Y="10" />
                                                                                <AxisY Enabled="True">
                                                                                    <MajorGrid LineColor="Beige" />
                                                                                    <SubAxes>
                                                                                        <DCWC:SubAxis Title="Dotgain">
                                                                                        </DCWC:SubAxis>
                                                                                    </SubAxes>
                                                                                </AxisY>
                                                                                <AxisX Enabled="True">
                                                                                    <MajorGrid LineColor="Beige" />
                                                                                </AxisX>
                                                                            </DCWC:ChartArea>
                                                                        </ChartAreas>
                                                                    </DCWC:Chart>
                                                                </div>
                                                            </div>
                                                            <!-- Dotgain -->
                                                            <!-- PrintContrast -->
                                                            <div class="CHART_DIV">
                                                                <div class="CHART_DIV_LEFT">
                                                                    <DCWC:Chart ID="CHRT_PrintContrastP" ImageType="Jpeg" runat="server" Width="850px"
                                                                        Height="125px" ImageUrl="~/UploadFiles/temp/JobRunCertification_PrintContrast"
                                                                        BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF"
                                                                        BorderLineColor="#808080">
                                                                        <Legends>
                                                                            <DCWC:Legend Name="Default">
                                                                            </DCWC:Legend>
                                                                            <DCWC:Legend Name="Legends1" Alignment="Center" Docking="Bottom">
                                                                            </DCWC:Legend>
                                                                        </Legends>
                                                                        <BorderSkin FrameBackGradientEndColor="White" FrameBorderColor="Black" FrameBorderWidth="2"
                                                                            SkinStyle="Raised" FrameBackColor="White"></BorderSkin>
                                                                        <UI>
                                                                            <Toolbar BackColor="White" BorderColor="LightSlateGray">
                                                                                <BorderSkin FrameBackGradientEndColor="LightBlue" PageColor="Transparent" SkinStyle="Raised"
                                                                                    FrameBackColor="SteelBlue"></BorderSkin>
                                                                            </Toolbar>
                                                                        </UI>
                                                                        <Series>
                                                                            <DCWC:Series Name="Series0" Color="Transparent" ChartArea="ChartArea1" BorderWidth="2"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series1" Color="150, 000, 255, 255" ChartArea="ChartArea1" BorderWidth="2"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series2" Color="150, 255, 000, 255" ChartArea="ChartArea1" BorderWidth="2"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series3" Color="150, 255, 225, 000" ChartArea="ChartArea1" BorderWidth="2"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                            <DCWC:Series Name="Series4" Color="150, 000, 000, 000" ChartArea="ChartArea1" BorderWidth="2"
                                                                                ShowInLegend="false">
                                                                            </DCWC:Series>
                                                                        </Series>
                                                                        <ChartAreas>
                                                                            <DCWC:ChartArea Name="ChartArea1">
                                                                                <Position Height="90" Width="98" X="0" Y="10" />
                                                                                <AxisY Enabled="True">
                                                                                    <MajorGrid LineColor="Beige" />
                                                                                    <SubAxes>
                                                                                        <DCWC:SubAxis Title="PrintContrast">
                                                                                        </DCWC:SubAxis>
                                                                                    </SubAxes>
                                                                                </AxisY>
                                                                                <AxisX Enabled="True">
                                                                                    <MajorGrid LineColor="Beige" />
                                                                                </AxisX>
                                                                            </DCWC:ChartArea>
                                                                        </ChartAreas>
                                                                    </DCWC:Chart>
                                                                </div>
                                                            </div>
                                                            <!-- PrintContrast -->
                                                        </div>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <DCWC:Chart ID="CHRT_GrayBalance_L" runat="server" Width="900px" Height="250px" ImageType="Jpeg"
                                                                    ImageUrl="~/UploadFiles/temp/JobRunCertification_Page2_GrayBalance_L" BackGradientType="Center"
                                                                    Palette="Kindergarten" BackGradientEndColor="#DFDFDF" BorderLineColor="#808080">
                                                                    <Legends>
                                                                        <DCWC:Legend Name="Default" Enabled="true" BorderStyle="Solid" LegendStyle="Row"
                                                                            BackColor="Transparent" Alignment="Far" Docking="Top">
                                                                            <Position Height="20" Width="30" X="60" Y="5" />
                                                                        </DCWC:Legend>
                                                                    </Legends>
                                                                    <Titles>
                                                                        <DCWC:Title Text="Gray Balance L" Alignment="TopLeft" Font="Microsoft Sans Serif, 8pt, style=Bold">
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
                                                                        <DCWC:Series Name="Series1" ShowLabelAsValue="true" LabelFormat="0"  LegendText="L * Actual" Color="Black" BorderWidth="2"
                                                                            ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" MarkerSize="12"
                                                                            BackGradientEndColor="162, 164, 255">
                                                                        </DCWC:Series>
                                                                        <DCWC:Series MarkerSize="12" LegendText="L * Target" BackGradientEndColor="162, 164, 255"
                                                                            Color="Green" ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle"
                                                                            BorderWidth="2" Name="Series2">
                                                                        </DCWC:Series>
                                                                    </Series>
                                                                    <ChartAreas>
                                                                        <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="Gray"
                                                                            BackColor="White" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                            <Position Height="80" Width="90" X="0" Y="15" />
                                                                            <AxisY LabelsAutoFit="False" Title="L*">
                                                                                <MajorGrid LineColor="Gray"></MajorGrid>
                                                                            </AxisY>
                                                                            <AxisX LabelsAutoFit="False" Margin="False" Interval="Auto" Title="Patch Percent">
                                                                                <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                <LabelStyle FontAngle="0" />
                                                                            </AxisX>
                                                                        </DCWC:ChartArea>
                                                                    </ChartAreas>
                                                                </DCWC:Chart>
                                                            </td>
                                                        </tr>
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
    <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
    <asp:HiddenField ID="HDFJobID" runat="server" />
    <asp:HiddenField ID="HDFDongalID" runat="server" />
    <asp:HiddenField ID="HDFIsNonProcess" runat="server" />

    <script language="javascript" type="text/javascript">
        function OpenMeasurementWindow(JobID, DongalID, IsNonProcess) {

            var objJobID = document.getElementById('<%=HDFJobID.ClientID %>');
            var objDongalID = document.getElementById('<%=HDFDongalID.ClientID %>');
            var objIsNonProcess = document.getElementById('<%=HDFIsNonProcess.ClientID%>');

            objJobID.value = JobID;
            objDongalID.value = DongalID;
            objIsNonProcess.value = IsNonProcess;
            document.getElementById('<%=BtnCreateReport.ClientID %>').click();
        }
    </script>

</asp:Content>
