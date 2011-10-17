<%@ Page Language="C#" MasterPageFile="~/Home.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="JobsReport.aspx.cs"
    Inherits="JobsReport" %>



<%@ Register Src="Common/HomeMenu.ascx" TagName="HomeMenu" TagPrefix="uc1" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<%@ Register TagPrefix="custom" Namespace="Fuji.Business" Assembly="Fuji.Business" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="940px" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
            <td>
                <!--Menu Conttrol Will Load here-->
                <uc1:HomeMenu ID="HomeMenu1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
                    <tr>
                        <td>
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" class="TABLE1">
                                        <tr>
                                            <td class="green-title">
                                                &nbsp; Job
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LblError" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="TR_Company" runat="server">
                                            <td class="RADSEARCHBARCOMPANY" align="center">
                                                <asp:DropDownList ID="DDL_CompanyList" runat="server" CssClass="inputbox" AutoPostBack="True"
                                                    OnSelectedIndexChanged="DDL_CompanyList_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:Literal ID="LTRL_UserCompany" runat="server"></asp:Literal>
                                            </td>                                           
                                        </tr>                                       
                                        <tr>
                                            <td>
                                                
                                                <ajaxtoolkit:TabContainer ID="TabContainer1" runat="server" Width="100%" style="padding:0px" OnActiveTabChanged="TabContainer1_ActiveTabChanged" AutoPostBack="true">
                                                    <ajaxtoolkit:TabPanel ID="TabPanPrepress" Width="100%" runat="server" HeaderText="Prepress" Style="padding:0px;width:100%">
                                                        <ContentTemplate>
                                                            <rad:RadGrid 
                                                                ID="GridJobReportPrepress" 
                                                                runat="server" 
                                                                AllowFilteringByColumn="True"
                                                                AllowPaging="True" 
                                                                AllowSorting="True" 
                                                                AutoGenerateColumns="False" EnableAJAX="True"
                                                                GridLines="None" 
                                                                Width="100%" 
                                                                PageSize="25" 
                                                                OnNeedDataSource="GridJobReportPrepress_NeedDataSource"
                                                                Skin="Green" 
                                                                OnItemDataBound="GridJobReportPrepress_ItemDataBound" 
                                                                OnItemCreated="GridJobReportPrepress_ItemCreated"
                                                                OnItemCommand="GridJobReportPrepress_ItemCommand">
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
                                                                            DataField="Status" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                &nbsp;
                                                                                <img id="ibtnStatus" runat="server" style="cursor: pointer" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Width="8%"></HeaderStyle>
                                                                        </custom:FilteringColumn>
                                                                        <rad:GridBoundColumn DataField="Status" DataType="System.String" HeaderText="Status"
                                                                            UniqueName="hdnStatus" Visible="false">
                                                                        </rad:GridBoundColumn>
                                                                        <rad:GridTemplateColumn HeaderText="View" AllowFiltering="false" UniqueName="View"
                                                                            ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <img id="ImgView" runat="server" src="~/Images/view-icon-1.gif" style="cursor: pointer"
                                                                                    title="View in same window" />
                                                                                &nbsp;
                                                                                <img id="ImgView_New" runat="server" src="~/Images/popup.gif" style="cursor: pointer"
                                                                                    title="View in new window" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Width="7%"></HeaderStyle>
                                                                        </rad:GridTemplateColumn>
                                                                        <rad:GridBoundColumn DataField="Job" HeaderText="Job No" UniqueName="Job" HeaderStyle-Width="11%"
                                                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                        </rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="PrintTarget" HeaderText="Print Target" UniqueName="PrintTarget"
                                                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                        </rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="CPDeviceDesc" HeaderText="Device" UniqueName="CPDeviceDesc"
                                                                            HeaderStyle-Width="12%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                        </rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="DongleSerial" HeaderText="Workstation" UniqueName="DongleDescription"
                                                                            HeaderStyle-Width="12%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                        </rad:GridBoundColumn>
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
                                                                                <%# DataBinder.Eval(Container.DataItem, "DateTime")%></ItemTemplate>
                                                                        </custom:FilterColumnByDate>
                                                                        <rad:GridBoundColumn DataField="Date" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="TIME" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="GUID" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="CPMeasurementID" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="CompanyID" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="CompanyName" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="BranchID" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="CPDongleID" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="CPDeviceID" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="CPDevicePrintTargetID" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="Customer" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="CPDeviceTypeID" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="StatusID" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="CPSerialNUM" Visible="false"></rad:GridBoundColumn>
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
                                                        </ContentTemplate>
                                                    </ajaxtoolkit:TabPanel>
                                                    <ajaxtoolkit:TabPanel ID="TabPanPress" Width="100%" runat="server" HeaderText="Press">
                                                        <ContentTemplate>
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
                                                                                <img id="ibtnStatus" runat="server" style="cursor: pointer; width:20px; height:20px" />  &nbsp;&nbsp;&nbsp;<b> <%# Eval("ErrorRate")%> %</b>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Width="8%"></HeaderStyle>
                                                                        </custom:FilteringColumn>
                                                                        <rad:GridBoundColumn DataField="Status" DataType="System.String" HeaderText="Status"
                                                                            UniqueName="hdnStatus" Visible="false">
                                                                        </rad:GridBoundColumn>
                                                                        <rad:GridTemplateColumn HeaderText="View" AllowFiltering="false" UniqueName="View"
                                                                            ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <img id="ImgView" runat="server" src="~/Images/view-icon-1.gif" style="cursor: pointer"
                                                                                    title="View in same window" />
                                                                                &nbsp;
                                                                                <img id="ImgView_New" runat="server" src="~/Images/popup.gif" style="cursor: pointer"
                                                                                    title="View in new window" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Width="7%"></HeaderStyle>
                                                                        </rad:GridTemplateColumn>
                                                                        <rad:GridBoundColumn DataField="Job" HeaderText="Job No" UniqueName="Job" HeaderStyle-Width="11%"
                                                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                        </rad:GridBoundColumn>      
                                                                        <custom:FilterColumnByDate UniqueName="FirstSheet" DataField="FirstSheet"  HeaderText="First Sheet"
                                                                            SortExpression="FirstSheet" HeaderStyle-Width="14%">
                                                                            <HeaderStyle />
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "FirstSheet")%></ItemTemplate>
                                                                        </custom:FilterColumnByDate>
                                                                        <custom:FilterColumnByDate UniqueName="LastSheet" DataField="LastSheet"  HeaderText="Last Sheet"
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
                                                                     <%--   <custom:FilteringColumn HeaderText="Device Type" SortDescImageUrl="~/RadControls/Grid/Skins/Green/SortDesc.gif"
                                                                            UniqueName="DeviceType" SortAscImageUrl="~/RadControls/Grid/Skins/Green/SortAsc.gif"
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
                                                                                <%# DataBinder.Eval(Container.DataItem, "DateTime")%></ItemTemplate>
                                                                        </custom:FilterColumnByDate>  --%>
                                                                        <rad:GridBoundColumn DataField="Date" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="TIME" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="GUID" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="CPMeasurementID" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="CompanyID" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="CompanyName" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="BranchID" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="CPDongleID" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="CPDeviceID" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="CPDevicePrintTargetID" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="Customer" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="CPDeviceTypeID" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="StatusID" Visible="false"></rad:GridBoundColumn>
                                                                        <rad:GridBoundColumn DataField="CPSerialNUM" Visible="false"></rad:GridBoundColumn>
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
                                                                                <img id="Img1" runat="server" src="~/Images/Makeready.png" style="width:20px; height:20px" align="absmiddle" />
                                                                                Make Ready &nbsp;
                                                                                <img id="Img2" runat="server" src="~/Images/Production.png" style="width:20px; height:20px" align="absmiddle" />
                                                                                Production &nbsp;
                                                                                <img id="Img3" runat="server" src="~/Images/Complete.png"  style="width:20px; height:20px" align="absmiddle" />
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
                                                        </ContentTemplate>
                                                    </ajaxtoolkit:TabPanel>
                                                </ajaxtoolkit:TabContainer>
                                                
                                                
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
                                    <ajaxtoolkit:ModalPopupExtender runat="server" ID="programmaticModalPopup" BehaviorID="programmaticModalPopupBehavior"
                                        Y="50" TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="programmaticPopup"
                                        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="programmaticPopupDragHandle">
                                    </ajaxtoolkit:ModalPopupExtender>
                                    <asp:Panel runat="server" ID="programmaticPopup" Style="display: none; height: 765px;
                                        background-color: Transparent; background: ">
                                        <a id="hideModalPopupViaClientButton" href="#" style="border: none; text-decoration: none;">
                                            <img src="Images/Close2.png" style="position: absolute; border: none; right: 10px;
                                                top: 10px; text-align: right;" />
                                        </a>
                                        <iframe id="FrmPopup" src="" width="952px" height="770px" frameborder="0" marginwidth="0"
                                            scrolling="no" marginheight="0" allowtransparency="true" style="background-color: transparent">
                                        </iframe>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script language="javascript" type="text/javascript">
        function pageLoad() {
            $addHandler($get("hideModalPopupViaClientButton"), 'click', hideModalPopupViaClient);
        }

        function hideModalPopupViaClient(ev) {
            ev.preventDefault();
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.hide();
            document.getElementById("FrmPopup").src = "";
        }

        function OpenMeasurementWindow(URLStr, IsShow, IsNewWindow, NewId) 
        {
            if (IsShow == true) {
                if (IsNewWindow == true) 
                {
                    window.open(URLStr, NewId, 'width=952px,height=795px,resizable=1,scrollbars=0');
                }
                else 
                {
                    if (screen.width <= 1024) 
                    {
                        document.getElementById("<%=programmaticPopup.ClientID %>").style.height = "630px";
                    }

                    document.getElementById("FrmPopup").src = URLStr;
                    var modalPopupBehavior = $find('programmaticModalPopupBehavior');
                    modalPopupBehavior.show();
                }
            }
            else {
                alert("Facility for device type color, monitor and wide format only!");
            }
        }
        function setWidth() {
            document.getElementById(document.getElementById("<%=TabContainer1.ClientID %>").id + "_body").style.padding = 0;
        }
    </script>
 
</asp:Content>
