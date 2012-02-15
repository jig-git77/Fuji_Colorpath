<%@ Page Language="C#" MasterPageFile="~/Home.master" AutoEventWireup="true" CodeFile="SyncDetail.aspx.cs"
    EnableEventValidation="false" Inherits="SyncDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="Common/GenerateSyncReport.ascx" TagName="GenerateSyncReport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="flex/AC_OETags.js" type="text/javascript"></script>
    <%--<script src="JS/xychartread_isis.js" type="text/javascript"></script>--%>
    <script language="JavaScript" type="text/javascript">
        var requiredMajorVersion = 10;
        var requiredMinorVersion = 0;
        var requiredRevision = 0;
    </script>
    <table width="940px" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="23" height="36" valign="top">
                            <img src="images/menu-curve-left.png" alt="FujiFilm" border="0" />
                        </td>
                        <td valign="top" class="content-menu-top">
                            <b>Sync</b>
                        </td>
                        <td width="23" valign="top">
                            <img src="images/menu-curve-right.png" alt="FujiFilm" border="0" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" class="SYNCTABLE">
                                <tr>
                                    <td class="green-title">
                                        &nbsp; Sync
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblError" runat="server" CssClass="ERROR"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 500px;" valign="top">
                                        <table width="100%" style="height: 570px;" border="0" cellpadding="0" cellspacing="5"
                                            class="SYNCTABLE">
                                            <tr>
                                                <td style="width: 20%" valign="top">
                                                    <%--Left Navigation--%>
                                                    <table border="0" width="100%" style="height: 640px;" cellpadding="0" cellspacing="0"
                                                        class="TABLE1">
                                                        <tr>
                                                            <td style="padding: 3px;" valign="top">
                                                                <asp:Literal ID="LtrLeftNevigation" runat="server"></asp:Literal>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 80%" valign="top">
                                                    <%--Main Body--%>
                                                    <table id="TblPress" runat="server" border="0" width="100%" style="height: 640px;"
                                                        cellpadding="0" cellspacing="0" class="TABLE1">
                                                        <tr>
                                                            <td valign="top">
                                                                <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td style="padding: 3px" colspan="2">
                                                                            <b>Select Device Below</b>
                                                                            <%--or Click Add Press--%>
                                                                        </td>
                                                                        <%--<td align="right" style="padding: 5px">
                                                                            <asp:Button ID="BTN_AddPress" runat="server" CssClass="BUTTON" Text="Add Press" OnClientClick="return OpenAddPressWindow('SyncAddPressPopup.aspx')" />
                                                                        </td>--%>
                                                                    </tr>
                                                                    <tr id="TrCompanyBranch" runat="server">
                                                                        <td align="center">
                                                                            <table border="0" cellpadding="3" cellspacing="0" width="45%">
                                                                                <tr>
                                                                                    <td>
                                                                                        Company Name :
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="DROPDOWNLIST" AutoPostBack="True"
                                                                                            Style="width: 200px" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Branch Name :
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="ddlBranch" runat="server" Width="150px" CssClass="DROPDOWNLIST"
                                                                                            AutoPostBack="True" Style="width: 200px" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <rad:RadGrid ID="RG_SyncPress" runat="server" AutoGenerateColumns="false" PagerStyle-Mode="NextPrevAndNumeric"
                                                                                ShowHeader="true" AllowPaging="false" AllowSorting="true" AllowFilteringByColumn="false"
                                                                                Width="100%" OnNeedDataSource="RG_SyncPress_NeedDataSource" Skin="Green" OnSortCommand="RG_SyncPress_SortCommand"
                                                                                OnItemDataBound="RG_SyncPress_ItemDataBound" MasterTableView-EditFormSettings-EditFormType="Template">
                                                                                <ItemStyle HorizontalAlign="Left" Wrap="false" Width="920px" />
                                                                                <ClientSettings>
                                                                                    <Scrolling AllowScroll="true" ScrollHeight="400" />
                                                                                </ClientSettings>
                                                                                <PagerStyle Position="Top" />
                                                                                <HeaderStyle HorizontalAlign="Center" Width="920px" />
                                                                                <MasterTableView>
                                                                                    <PagerStyle Mode="NumericPages" Position="Top" Width="910px" />
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="920px" />
                                                                                    <Columns>
                                                                                        <rad:GridBoundColumn DataField="PressID" HeaderText="PressID" Visible="false" UniqueName="column1">
                                                                                        </rad:GridBoundColumn>
                                                                                        <rad:GridBoundColumn DataField="CPDongleID" HeaderText="CPDongleID" Visible="false"
                                                                                            UniqueName="column2">
                                                                                        </rad:GridBoundColumn>
                                                                                        <rad:GridTemplateColumn DataField="PressName" HeaderText="Sync Device" ItemStyle-Width="40%"
                                                                                            HeaderStyle-Width="40%" AllowFiltering="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="LbtnPressName" CssClass="SYNCLINK" runat="server" Text='<%# Eval("PressName")%>'
                                                                                                    OnClick="LbtnPressName_Click"></asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </rad:GridTemplateColumn>
                                                                                        <rad:GridBoundColumn DataField="PressManufacturer" UniqueName="PressManufacturer"
                                                                                            AllowFiltering="false" Visible="true" HeaderText="Device Manufacturer" ItemStyle-Width="40%"
                                                                                            HeaderStyle-Width="40%">
                                                                                        </rad:GridBoundColumn>
                                                                                    </Columns>
                                                                                </MasterTableView>
                                                                            </rad:RadGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table id="TblPressRun" runat="server" border="0" width="100%" style="height: 640px;"
                                                        visible="false" cellpadding="0" cellspacing="0" class="TABLE1">
                                                        <tr>
                                                            <td valign="top">
                                                                <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td style="padding: 5px">
                                                                            <b>Select Project Below or Click Add Project</b>
                                                                        </td>
                                                                        <td align="right" style="padding: 5px">
                                                                            <asp:Button ID="BTN_AddProject" runat="server" CssClass="BUTTON" Text="Add Project"
                                                                                OnClick="BTN_AddProject_Click" ProjectID="-1" />&nbsp;&nbsp; &nbsp;<asp:Button ID="BTN_Retarget"
                                                                                    runat="server" CssClass="BUTTON" Text="Retarget Project" OnClick="BTN_ReTargetProject_Click"
                                                                                    ProjectID="-1" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:HiddenField ID="PRClientDatatime" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <rad:RadGrid ID="RG_SyncProject" runat="server" AutoGenerateColumns="false" PagerStyle-Mode="NextPrevAndNumeric"
                                                                                ShowHeader="true" AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="true"
                                                                                Width="100%" OnPageIndexChanged="RG_SyncProject_PageIndexChanged" OnNeedDataSource="RG_SyncProject_NeedDataSource"
                                                                                Skin="Green" PageSize="25" OnSortCommand="RG_SyncProject_SortCommand" OnDetailTableDataBind="RG_SyncProject_DetailTableDataBind"
                                                                                OnItemDataBound="RG_SyncProject_ItemDataBound" MasterTableView-EditFormSettings-EditFormType="Template"
                                                                                OnItemCommand="RG_SyncProject_ItemCommand" OnItemCreated="RG_SyncProject_ItemCreated">
                                                                                <ItemStyle HorizontalAlign="Left" Wrap="false" Width="920px" />
                                                                                <ClientSettings>
                                                                                    <Scrolling AllowScroll="true" ScrollHeight="620px" />
                                                                                </ClientSettings>
                                                                                <PagerStyle Position="Top" />
                                                                                <HeaderStyle HorizontalAlign="Center" Width="920px" />
                                                                                <MasterTableView>
                                                                                    <PagerStyle Mode="NumericPages" Position="Top" Width="910px" />
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="920px" />
                                                                                    <DetailTables>
                                                                                        <rad:GridTableView Width="100%" AutoGenerateColumns="false" runat="server" Name="PressRun"
                                                                                            ShowHeader="false">
                                                                                            <RowIndicatorColumn CurrentFilterFunction="NoFilter" FilterListOptions="VaryByDataType"
                                                                                                Visible="False">
                                                                                                <HeaderStyle Width="10px"></HeaderStyle>
                                                                                            </RowIndicatorColumn>
                                                                                            <ExpandCollapseColumn CurrentFilterFunction="NoFilter" FilterListOptions="VaryByDataType"
                                                                                                Visible="False" Resizable="False">
                                                                                                <HeaderStyle Width="10px"></HeaderStyle>
                                                                                            </ExpandCollapseColumn>
                                                                                            <Columns>
                                                                                                <rad:GridBoundColumn DataField="PressRunID" HeaderText="PressRunID" Visible="false"
                                                                                                    UniqueName="column1">
                                                                                                </rad:GridBoundColumn>
                                                                                                <rad:GridBoundColumn DataField="Status" HeaderText="Status" UniqueName="ProjStatusID"
                                                                                                    Visible="false">
                                                                                                </rad:GridBoundColumn>
                                                                                                <rad:GridTemplateColumn DataField="PressRunName" HeaderText="Press Run Name" ItemStyle-Width="66%"
                                                                                                    HeaderStyle-Width="66%" AllowFiltering="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="LbtnPressRunName" CssClass="SYNCLINK" runat="server" Text='<%# Eval("PressRunName")%>'
                                                                                                            OnClick="LbtnPressRun_Click"></asp:LinkButton>
                                                                                                    </ItemTemplate>
                                                                                                </rad:GridTemplateColumn>
                                                                                                <rad:GridTemplateColumn DataField="Status" HeaderText="Status" ItemStyle-Width="14%"
                                                                                                    HeaderStyle-Width="14%" AllowFiltering="false" ItemStyle-HorizontalAlign="Center">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblStatus" runat="server" Text='<%# Eval("StatusType") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </rad:GridTemplateColumn>
                                                                                                <rad:GridTemplateColumn HeaderText="Iteration" ItemStyle-HorizontalAlign="Center"
                                                                                                    ItemStyle-Width="20%" HeaderStyle-Width="20%" AllowFiltering="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Button ID="BtnCreate" CssClass="BUTTON" runat="server" Text="Create" OnClick="BtnCreate_Click"
                                                                                                            OnClientClick="return GetPressRunClientDate();" />
                                                                                                    </ItemTemplate>
                                                                                                </rad:GridTemplateColumn>
                                                                                            </Columns>
                                                                                        </rad:GridTableView>
                                                                                    </DetailTables>
                                                                                    <RowIndicatorColumn CurrentFilterFunction="NoFilter" FilterListOptions="VaryByDataType"
                                                                                        Visible="False">
                                                                                        <HeaderStyle Width="10px"></HeaderStyle>
                                                                                    </RowIndicatorColumn>
                                                                                    <ExpandCollapseColumn CurrentFilterFunction="NoFilter" FilterListOptions="VaryByDataType"
                                                                                        Resizable="False">
                                                                                        <HeaderStyle Width="10px"></HeaderStyle>
                                                                                        <ItemStyle Width="10px" />
                                                                                    </ExpandCollapseColumn>
                                                                                    <Columns>
                                                                                        <rad:GridBoundColumn DataField="ProjectID" HeaderText="ProjectID" Visible="false"
                                                                                            UniqueName="column1">
                                                                                        </rad:GridBoundColumn>
                                                                                        <rad:GridBoundColumn DataField="ChartTypeID" HeaderText="ChartTypeID" Visible="false"
                                                                                            UniqueName="column2">
                                                                                        </rad:GridBoundColumn>
                                                                                        <%--<rad:GridBoundColumn DataField="ProjStatusID" HeaderText="ProjStatusID" UniqueName="ProjStatusID"
                                                                                            Visible="false">
                                                                                        </rad:GridBoundColumn>--%>
                                                                                        <rad:GridTemplateColumn DataField="ProjectName" DataType="System.String" HeaderText="Project Name"
                                                                                            UniqueName="ProjectName" ItemStyle-Width="58%" HeaderStyle-Width="58%" AllowFiltering="true"
                                                                                            CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="LbtnProjectName" CssClass="SYNCLINK" CommandName="ExpandCollapse"
                                                                                                    ToolTip="Edit" runat="server" Text='<%# Eval("ProjectName")%>'></asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </rad:GridTemplateColumn>
                                                                                        <rad:GridTemplateColumn ItemStyle-Width="4%" HeaderStyle-Width="4%" AllowFiltering="false"
                                                                                            ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>
                                                                                                <asp:Image ID="imgReTarg" runat="server" Style="cursor: pointer; width: 20px; height: 20px"
                                                                                                    src="Images/MakeReady.png" alt='<%# Eval("ReTargProject") %>' Visible="false" />
                                                                                            </ItemTemplate>
                                                                                        </rad:GridTemplateColumn>
                                                                                        <rad:GridTemplateColumn HeaderText="Edit" ItemStyle-Width="4%" HeaderStyle-Width="4%"
                                                                                            AllowFiltering="false" ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="ibtnEdit" OnClick="ibtnEdit_Click" ImageUrl="Images/Edit-Icon.gif"
                                                                                                    runat="server" />
                                                                                            </ItemTemplate>
                                                                                        </rad:GridTemplateColumn>
                                                                                        <rad:GridTemplateColumn HeaderText="Delete" ItemStyle-Width="4%" HeaderStyle-Width="4%"
                                                                                            AllowFiltering="false" ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="ibtnDelete" OnClick="ibtnDelete_Click" ImageUrl="Images/Delete-Icon.gif"
                                                                                                    runat="server" />
                                                                                            </ItemTemplate>
                                                                                        </rad:GridTemplateColumn>
                                                                                        <rad:GridTemplateColumn DataField="StatusType" HeaderText="Status" ItemStyle-Width="14%"
                                                                                            UniqueName="StatusType" HeaderStyle-Width="14%" AllowFiltering="true" ItemStyle-HorizontalAlign="Center"
                                                                                            CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="LblStatus" runat="server" Text='<%# Eval("StatusType") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </rad:GridTemplateColumn>
                                                                                        <rad:GridTemplateColumn HeaderText="Iteration" ItemStyle-HorizontalAlign="Center"
                                                                                            ItemStyle-Width="20%" HeaderStyle-Width="20%" AllowFiltering="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:HiddenField ID="HDLevel" runat="server" Value='<%# Eval("level") %>' />
                                                                                                <asp:Button ID="BtnCreate" CssClass="BUTTON" runat="server" Text="Create" OnClick="BtnCreate_Click"
                                                                                                    OnClientClick="return GetPressRunClientDate();" />
                                                                                            </ItemTemplate>
                                                                                        </rad:GridTemplateColumn>
                                                                                    </Columns>
                                                                                    <PagerStyle AlwaysVisible="True" />
                                                                                    <PagerTemplate>
                                                                                        <div style="float: left">
                                                                                            <asp:Literal ID="LTRL_RecordText" runat="server"></asp:Literal>
                                                                                            <asp:ImageButton ID="LBTN_First" CommandName="Page" CausesValidation="false" CommandArgument="First"
                                                                                                runat="server" ImageUrl="Images/Nav_FirstPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                                            <asp:ImageButton ID="LBTN_Prev" CommandName="Page" CausesValidation="false" CommandArgument="Prev"
                                                                                                runat="server" ImageUrl="Images/Nav_PreviousPage_off.gif" align="absmiddle">
                                                                                            </asp:ImageButton>
                                                                                            <asp:ImageButton ID="LBTN_Next" CommandName="Page" CausesValidation="false" CommandArgument="Next"
                                                                                                runat="server" ImageUrl="Images/Nav_NextPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                                            <asp:ImageButton ID="LBTN_Last" CommandName="Page" CausesValidation="false" CommandArgument="Last"
                                                                                                runat="server" ImageUrl="Images/Nav_LastPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                                            &nbsp;
                                                                                            <asp:TextBox ID="tbPageNumber" runat="server" Columns="3" Text='<%# (int)DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex") + 1 %>'
                                                                                                Visible="False" />
                                                                                            <asp:LinkButton ID="LBTN_Go" runat="server" CommandName="CustomChangePage" Visible="False">Go</asp:LinkButton>
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
                                                                                        <div style="float: right">
                                                                                            &nbsp;
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
                                                    <table id="TblPrintTargetCondition" runat="server" border="0" width="100%" style="height: 640px;"
                                                        visible="false" cellpadding="0" cellspacing="0" class="TABLE1">
                                                        <tr>
                                                            <td valign="top">
                                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                    <tr>
                                                                        <td class="green-title">
                                                                            <asp:Label runat="server" ID="lblProjectHeader"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table cellpadding="0" cellspacing="4" border="0" width="95%">
                                                                                <tr>
                                                                                    <td class="FORM-TEXT-BLACK" style="width: 20%">
                                                                                        Project Name :
                                                                                    </td>
                                                                                    <td style="width: 30%">
                                                                                        <asp:TextBox ID="TxtProjectName" runat="server" CssClass="TEXTBOX"></asp:TextBox>
                                                                                        <asp:HiddenField ID="ClientDatatime" runat="server" />
                                                                                    </td>
                                                                                    <td id="td1" runat="server" class="FORM-TEXT-BLACK" style="width: 20%">
                                                                                        Output Type :
                                                                                    </td>
                                                                                    <td id="td2" runat="server" style="width: 30%">
                                                                                        <asp:DropDownList ID="DDLOutputType" runat="server" Width="120px" CssClass="DROPDOWNLIST"
                                                                                            AutoPostBack="True" OnSelectedIndexChanged="DDLOutputType_SelectedIndexChanged"
                                                                                            EnableViewState="false">
                                                                                            <asp:ListItem Text="Offset" Value="1"></asp:ListItem>
                                                                                            <asp:ListItem Text="Screen" Value="2"></asp:ListItem>
                                                                                            <asp:ListItem Text="Wide Format" Value="3"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td id="tdProjectTypeTitle" runat="server" class="FORM-TEXT-BLACK" style="width: 20%">
                                                                                        Project Type :
                                                                                    </td>
                                                                                    <td id="tdProjectTypeDDL" runat="server" style="width: 30%">
                                                                                        <asp:DropDownList ID="DDLProjectType" runat="server" Width="120px" CssClass="DROPDOWNLIST"
                                                                                            AutoPostBack="True" OnSelectedIndexChanged="DDLProjectType_SelectedIndexChanged"
                                                                                            EnableViewState="true">
                                                                                            <asp:ListItem Text="G7" Value="1"></asp:ListItem>
                                                                                            <asp:ListItem Text="ISO" Value="2"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td class="FORM-TEXT-BLACK" style="width: 20%">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td style="width: 30%">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">
                                                                <ajaxtoolkit:TabContainer ID="TabContainer1" runat="server" Width="100%" Style="padding: 0px;
                                                                    height: 440px;">
                                                                    <ajaxtoolkit:TabPanel ID="TabPanPrintTarget" Width="100%" runat="server" HeaderText="Print Target"
                                                                        Style="padding: 0px; width: 100%; height: 440px;">
                                                                        <ContentTemplate>
                                                                            <table border="0" width="100%" style="height: 440px;" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td valign="top">
                                                                                        <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td style="padding-bottom: 3px">
                                                                                                    <table border="0" width="99%" cellpadding="0" cellspacing="0">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <b>Color Metrics</b>
                                                                                                            </td>
                                                                                                            <td id="certrun" runat="server" align="right">
                                                                                                                ReTarget based on :
                                                                                                                <asp:DropDownList ID="ddlCertifiedProject" runat="server" CssClass="DROPDOWNLIST"
                                                                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlCertifiedProject_SelectedIndexChanged">
                                                                                                                </asp:DropDownList>
                                                                                                            </td>
                                                                                                            <td id="standardpt" runat="server" align="right">
                                                                                                                Select Preset :
                                                                                                                <asp:DropDownList ID="DdlStanderdsPreset" runat="server" CssClass="DROPDOWNLIST"
                                                                                                                    AutoPostBack="True" OnSelectedIndexChanged="DdlStanderdsPreset_SelectedIndexChanged">
                                                                                                                </asp:DropDownList>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="center" class="TDBOX">
                                                                                                    <table border="0" width="99%" cellpadding="0">
                                                                                                        <tr id="trG7" runat="server">
                                                                                                            <td>
                                                                                                                <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                                                                                                    <tr>
                                                                                                                        <td style="width: 47%" valign="top">
                                                                                                                            <table border="0" width="100%" cellpadding="0" cellspacing="5">
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <b>Lab</b>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <table border="0" width="100%" cellpadding="1" cellspacing="0" class="TABLEBOX">
                                                                                                                                            <tr class="TRTABLEBOX">
                                                                                                                                                <td style="width: 22%">
                                                                                                                                                    &nbsp;
                                                                                                                                                </td>
                                                                                                                                                <td colspan="3" align="center">
                                                                                                                                                    Targets
                                                                                                                                                </td>
                                                                                                                                                <td style="width: 6%">
                                                                                                                                                    &nbsp;
                                                                                                                                                </td>
                                                                                                                                                <td align="center">
                                                                                                                                                    Thresholds
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr class="TRTABLEBOX">
                                                                                                                                                <td style="width: 22%" class="TDTABLEBOXLEFT">
                                                                                                                                                    Patch
                                                                                                                                                </td>
                                                                                                                                                <td style="width: 18%" align="center">
                                                                                                                                                    L*
                                                                                                                                                </td>
                                                                                                                                                <td style="width: 18%" align="center">
                                                                                                                                                    a*
                                                                                                                                                </td>
                                                                                                                                                <td style="width: 18%" align="center">
                                                                                                                                                    b*
                                                                                                                                                </td>
                                                                                                                                                <td style="width: 6%">
                                                                                                                                                    &nbsp;
                                                                                                                                                </td>
                                                                                                                                                <td style="width: 18%" align="center">
                                                                                                                                                    &Delta; E
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td class="TDTABLEBOXLEFT">
                                                                                                                                                    Substrate
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtPaperTL" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtPaperTL" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtPaperTL"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtPaperTa" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtPaperTa" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtPaperTa"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtPaperTb" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtPaperTb" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtPaperTb"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td>
                                                                                                                                                    &nbsp;
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtPaperTH" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtPaperTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtPaperTH"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td class="TDTABLEBOXLEFT">
                                                                                                                                                    Cyan
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtCyanTL" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtCyanTL" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtCyanTL"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtCyanTa" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtCyanTa" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtCyanTa"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtCyanTb" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtCyanTb" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtCyanTb"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td>
                                                                                                                                                    &nbsp;
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtCyanTH" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtCyanTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtCyanTH"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td class="TDTABLEBOXLEFT">
                                                                                                                                                    Magenta
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtMagendaTL" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtMagendaTL" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtMagendaTL"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtMagendaTa" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtMagendaTa" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtMagendaTa"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtMagendaTb" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtMagendaTb" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtMagendaTb"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td>
                                                                                                                                                    &nbsp;
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtMagendaTH" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtMagendaTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtMagendaTH"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td class="TDTABLEBOXLEFT">
                                                                                                                                                    Yellow
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtYellowTL" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtYellowTL" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtYellowTL"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtYellowTa" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtYellowTa" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtYellowTa"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtYellowTb" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtYellowTb" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtYellowTb"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    &nbsp;
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtYellowTH" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtYellowTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtYellowTH"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td class="TDTABLEBOXLEFT">
                                                                                                                                                    Black
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtBlackTL" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtBlackTL" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtBlackTL"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtBlackTa" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtBlackTa" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtBlackTa"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtBlackTb" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtBlackTb" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtBlackTb"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    &nbsp;
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtBlackTH" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtBlackTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtBlackTH"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td class="TDTABLEBOXLEFT">
                                                                                                                                                    Red
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtRedTL" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtRedTL" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtRedTL"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtRedTa" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtRedTa" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtRedTa"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtRedTb" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtRedTb" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtRedTb"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td>
                                                                                                                                                    &nbsp;
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtRedTH" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtRedTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtRedTH"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td class="TDTABLEBOXLEFT">
                                                                                                                                                    Green
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtGreenTL" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtGreenTL" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtGreenTL"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtGreenTa" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtGreenTa" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtGreenTa"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtGreenTb" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtGreenTb" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtGreenTb"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td>
                                                                                                                                                    &nbsp;
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtGreenTH" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtGreenTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtGreenTH"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td class="TDTABLEBOXLEFT">
                                                                                                                                                    Blue
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtBlueTL" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtBlueTL" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtBlueTL"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtBlueTa" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtBlueTa" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtBlueTa"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtBlueTb" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtBlueTb" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtBlueTb"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td>
                                                                                                                                                    &nbsp;
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtBlueTH" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtBlueTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtBlueTH"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td class="TDTABLEBOXLEFT">
                                                                                                                                                    CMY 100%
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="Txt3CTL" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxt3CTL" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="Txt3CTL"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="Txt3CTa" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxt3CTa" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="Txt3CTa"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="Txt3CTb" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxt3CTb" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="Txt3CTb"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td>
                                                                                                                                                    &nbsp;
                                                                                                                                                </td>
                                                                                                                                                <td class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="Txt3CTH" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxt3CTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="Txt3CTH"></asp:RequiredFieldValidator>
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
                                                                                                                                    <td>
                                                                                                                                        <b>Gray Tolerance</b>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <table border="0" width="100%" cellpadding="1" cellspacing="0" class="TABLEBOX">
                                                                                                                                            <tr class="TRTABLEBOX">
                                                                                                                                                <td class="TDTABLEBOXLEFT">
                                                                                                                                                    Threshold
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr class="TRTABLEBOX">
                                                                                                                                                <td class="TDTABLEBOXLEFT">
                                                                                                                                                    &Delta; Ch
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td class="TDTABLEBOXLEFT" style="padding-top: 3px; padding-bottom: 3px;">
                                                                                                                                                    <asp:TextBox ID="TxtDeltaFTH" runat="server" Width="50px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtDeltaFTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtDeltaFTH"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </table>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                        <td style="width: 53%" valign="top">
                                                                                                                            <table border="0" width="100%" cellpadding="0" cellspacing="5">
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <b>Density</b>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <table border="0" width="100%" cellpadding="1" cellspacing="0" class="TABLEBOX">
                                                                                                                                            <tr class="TRTABLEBOX">
                                                                                                                                                <td>
                                                                                                                                                    &nbsp;
                                                                                                                                                </td>
                                                                                                                                                <td colspan="4" align="center">
                                                                                                                                                    Targets
                                                                                                                                                </td>
                                                                                                                                                <td style="width: 2px">
                                                                                                                                                    &nbsp;
                                                                                                                                                </td>
                                                                                                                                                <td colspan="4" align="center">
                                                                                                                                                    Threshold
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr class="TRTABLEBOX">
                                                                                                                                                <td style="width: 14%" class="TDTABLEBOXLEFT">
                                                                                                                                                    Patch
                                                                                                                                                </td>
                                                                                                                                                <td style="width: 11%" align="center">
                                                                                                                                                    C
                                                                                                                                                </td>
                                                                                                                                                <td style="width: 11%" align="center">
                                                                                                                                                    M
                                                                                                                                                </td>
                                                                                                                                                <td style="width: 11px" align="center">
                                                                                                                                                    Y
                                                                                                                                                </td>
                                                                                                                                                <td style="width: 11%" align="center">
                                                                                                                                                    K
                                                                                                                                                </td>
                                                                                                                                                <td style="width: 2%">
                                                                                                                                                    &nbsp;
                                                                                                                                                </td>
                                                                                                                                                <td style="width: 10%" align="center">
                                                                                                                                                    &Delta;C
                                                                                                                                                </td>
                                                                                                                                                <td style="width: 10%" align="center">
                                                                                                                                                    &Delta;M
                                                                                                                                                </td>
                                                                                                                                                <td style="width: 10%" align="center">
                                                                                                                                                    &Delta;Y
                                                                                                                                                </td>
                                                                                                                                                <td style="width: 10%" align="center">
                                                                                                                                                    &Delta;K
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td class="TDTABLEBOXLEFT">
                                                                                                                                                    Solid
                                                                                                                                                </td>
                                                                                                                                                <td align="center" class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtCyanDT" runat="server" Width="26px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtCyanDT" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtCyanDT"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td align="center" class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtMagendaDT" runat="server" Width="26px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtMagendaDT" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtMagendaDT"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td align="center" class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtYellowDT" runat="server" Width="26px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtYellowDT" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtYellowDT"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td align="center" class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtBalckDT" runat="server" Width="26px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtBalckDT" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtBalckDT"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td style="width: 2px">
                                                                                                                                                    &nbsp;
                                                                                                                                                </td>
                                                                                                                                                <td align="center" class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtCyanDTH" runat="server" Width="24px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtCyanDTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtCyanDTH"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td align="center" class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtMagendaDTH" runat="server" Width="24px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtMagendaDTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtMagendaDTH"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td align="center" class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtYellowDTH" runat="server" Width="24px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtYellowDTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtYellowDTH"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td align="center" class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="TxtBalckDTH" runat="server" Width="24px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxtBalckDTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="TxtBalckDTH"></asp:RequiredFieldValidator>
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
                                                                                                                                    <td>
                                                                                                                                        <b>TVI</b>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <table border="0" width="100%" cellpadding="1" cellspacing="0" class="TABLEBOX">
                                                                                                                                            <tr class="TRTABLEBOX">
                                                                                                                                                <td>
                                                                                                                                                    &nbsp;
                                                                                                                                                </td>
                                                                                                                                                <td colspan="4" align="center">
                                                                                                                                                    Targets
                                                                                                                                                </td>
                                                                                                                                                <td align="center">
                                                                                                                                                    Threshold
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr class="TRTABLEBOX">
                                                                                                                                                <td class="TDTABLEBOXLEFT">
                                                                                                                                                    Patch
                                                                                                                                                </td>
                                                                                                                                                <td align="center">
                                                                                                                                                    C
                                                                                                                                                </td>
                                                                                                                                                <td align="center">
                                                                                                                                                    M
                                                                                                                                                </td>
                                                                                                                                                <td align="center">
                                                                                                                                                    Y
                                                                                                                                                </td>
                                                                                                                                                <td align="center">
                                                                                                                                                    K
                                                                                                                                                </td>
                                                                                                                                                <td>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td class="TDTABLEBOXLEFT">
                                                                                                                                                    1/4 Tone
                                                                                                                                                </td>
                                                                                                                                                <td align="center" class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="Txt25TC" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxt25TC" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="Txt25TC"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td align="center" class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="Txt25TM" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxt25TM" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="Txt25TM"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td align="center" class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="Txt25TY" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxt25TY" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="Txt25TY"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td align="center" class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="Txt25TK" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxt25TK" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="Txt25TK"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td align="center" class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="Txt25TH" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxt25TH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="Txt25TH"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td class="TDTABLEBOXLEFT">
                                                                                                                                                    Mid Tone
                                                                                                                                                </td>
                                                                                                                                                <td align="center" class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="Txt50TC" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxt50TC" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="Txt50TC"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td align="center" class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="Txt50TM" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxt50TM" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="Txt50TM"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td align="center" class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="Txt50TY" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxt50TY" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="Txt50TY"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td align="center" class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="Txt50TK" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxt50TK" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="Txt50TK"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td align="center" class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="Txt50TH" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxt50TH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="Txt50TH"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td class="TDTABLEBOXLEFT">
                                                                                                                                                    3/4 Tone
                                                                                                                                                </td>
                                                                                                                                                <td align="center" class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="Txt75TC" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxt75TC" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="Txt75TC"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td align="center" class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="Txt75TM" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxt75TM" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="Txt75TM"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td align="center" class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="Txt75TY" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxt75TY" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="Txt75TY"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td align="center" class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="Txt75TK" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxt75TK" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="Txt75TK"></asp:RequiredFieldValidator>
                                                                                                                                                </td>
                                                                                                                                                <td align="center" class="TDTABLEBOX">
                                                                                                                                                    <asp:TextBox ID="Txt75TH" runat="server" Width="42px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                        ID="RFTxt75TH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                        ControlToValidate="Txt75TH"></asp:RequiredFieldValidator>
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
                                                                                                                        <td>
                                                                                                                            <asp:CheckBox ID="chkDLP" runat="server" Text="Use DLP for iteration**" Checked="true" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr id="trISO" runat="server">
                                                                                                            <td>
                                                                                                                <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                                                                                                    <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                                                                                                        <tr>
                                                                                                                            <td style="width: 47%" valign="top">
                                                                                                                                <table border="0" width="100%" cellpadding="0" cellspacing="5">
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            <b>Lab</b>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            <table border="0" width="100%" cellpadding="1" cellspacing="0" class="TABLEBOX">
                                                                                                                                                <tr class="TRTABLEBOX">
                                                                                                                                                    <td style="width: 19%;">
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                    <td align="center" colspan="3">
                                                                                                                                                        Targets
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 2%">
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                    <td align="center" colspan="2">
                                                                                                                                                        Thresholds
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr class="TRTABLEBOX">
                                                                                                                                                    <td style="width: 19%;" class="TDTABLEBOXLEFT">
                                                                                                                                                        Patch
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 17%" align="center">
                                                                                                                                                        L*
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 17%" align="center">
                                                                                                                                                        a*
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 17%" align="center">
                                                                                                                                                        b*
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 2%">
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 14%" align="center">
                                                                                                                                                        &Delta;E
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 14%" align="center">
                                                                                                                                                        &Delta;h
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td class="TDTABLEBOXLEFT">
                                                                                                                                                        Cyan
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOCyanTL" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOCyanTL" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOCyanTL"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOCyanTa" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOCyanTa" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOCyanTa"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOCyanTb" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOCyanTb" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOCyanTb"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOCyanTH" runat="server" Width="28px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOCyanTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOCyanTH"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOCyanHTH" runat="server" Width="28px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOCyanHTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOCyanHTH"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td class="TDTABLEBOXLEFT">
                                                                                                                                                        Magenta
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOMagendaTL" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOMagendaTL" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOMagendaTL"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOMagendaTa" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOMagendaTa" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOMagendaTa"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOMagendaTb" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOMagendaTb" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOMagendaTb"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOMagendaTH" runat="server" Width="28px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOMagendaTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOMagendaTH"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOMagendaHTH" runat="server" Width="28px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOMagendaHTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOMagendaHTH"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td class="TDTABLEBOXLEFT">
                                                                                                                                                        Yellow
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOYellowTL" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOYellowTL" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOYellowTL"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOYellowTa" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOYellowTa" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOYellowTa"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOYellowTb" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOYellowTb" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOYellowTb"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOYellowTH" runat="server" Width="28px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOYellowTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOYellowTH"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOYellowHTH" runat="server" Width="28px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOYellowHTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOYellowHTH"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td class="TDTABLEBOXLEFT">
                                                                                                                                                        Black
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOBlackTL" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOBlackTL" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOBlackTL"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOBlackTa" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOBlackTa" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOBlackTa"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOBlackTb" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOBlackTb" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOBlackTb"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOBlackTH" runat="server" Width="28px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOBlackTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOBlackTH"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td class="TDTABLEBOXLEFT">
                                                                                                                                                        Red
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISORedTL" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISORedTL" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISORedTL"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISORedTa" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISORedTa" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISORedTa"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISORedTb" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISORedTb" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISORedTb"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISORedTH" runat="server" Width="28px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISORedTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISORedTH"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td class="TDTABLEBOXLEFT">
                                                                                                                                                        Green
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOGreenTL" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOGreenTL" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOGreenTL"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOGreenTa" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOGreenTa" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOGreenTa"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOGreenTb" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOGreenTb" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOGreenTb"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOGreenTH" runat="server" Width="28px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOGreenTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOGreenTH"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td class="TDTABLEBOXLEFT">
                                                                                                                                                        Blue
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOBlueTL" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOBlueTL" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOBlueTL"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOBlueTa" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOBlueTa" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOBlueTa"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOBlueTb" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOBlueTb" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOBlueTb"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOBlueTH" runat="server" Width="28px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOBlueTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOBlueTH"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td class="TDTABLEBOXLEFT">
                                                                                                                                                        CMY 100%
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO3CTL" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO3CTL" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO3CTL"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO3CTa" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO3CTa" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO3CTa"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO3CTb" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO3CTb" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO3CTb"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO3CTH" runat="server" Width="28px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO3CTH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO3CTH"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td class="TDTABLEBOXLEFT">
                                                                                                                                                        CMY 75%
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOGray75TL" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOGray75TL" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOGray75TL"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOGray75Ta" runat="server" Width="35px" CssClass="TEXTBOX" Visible="false"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOGray75Ta" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOGray75Ta"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOGray75Tb" runat="server" Width="35px" CssClass="TEXTBOX" Visible="false"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOGray75Tb" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOGray75Tb"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOGray75TH" runat="server" Width="28px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOGray75TH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOGray75TH"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td class="TDTABLEBOXLEFT">
                                                                                                                                                        CMY 50%
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOGray50TL" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOGray50TL" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOGray50TL"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOGray50Ta" runat="server" Width="35px" CssClass="TEXTBOX" Visible="false"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOGray50Ta" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOGray50Ta"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOGray50Tb" runat="server" Width="35px" CssClass="TEXTBOX" Visible="false"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOGray50Tb" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOGray50Tb"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOGray50TH" runat="server" Width="28px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOGray50TH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOGray50TH"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td class="TDTABLEBOXLEFT">
                                                                                                                                                        CMY 25%
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOGray25TL" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOGray25TL" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOGray25TL"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOGray25Ta" runat="server" Width="35px" CssClass="TEXTBOX" Visible="false"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOGray25Ta" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOGray25Ta"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOGray25Tb" runat="server" Width="35px" CssClass="TEXTBOX" Visible="false"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOGray25Tb" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOGray25Tb"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOGray25TH" runat="server" Width="28px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOGray25TH" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOGray25TH"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        &nbsp;
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
                                                                                                                                        <td>
                                                                                                                                            <b>Substrate</b>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            <table border="0" width="100%" cellpadding="1" cellspacing="0" class="TABLEBOX">
                                                                                                                                                <tr class="TRTABLEBOX">
                                                                                                                                                    <td align="center" colspan="3">
                                                                                                                                                        Targets
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 4%">
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                    <td align="center" colspan="3">
                                                                                                                                                        Thresholds
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr class="TRTABLEBOX">
                                                                                                                                                    <td style="width: 16%" align="center">
                                                                                                                                                        L*
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 16%" align="center">
                                                                                                                                                        a*
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 16%" align="center">
                                                                                                                                                        b*
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 4%">
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 16%" align="center">
                                                                                                                                                        &Delta;L*
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 16%" align="center">
                                                                                                                                                        &Delta;a*
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 16%" align="center">
                                                                                                                                                        &Delta;b*
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td class="TDTABLEBOX" align="center">
                                                                                                                                                        <asp:TextBox ID="TxtISOAPPaperTL" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOAPPaperTL" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOAPPaperTL"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX" align="center">
                                                                                                                                                        <asp:TextBox ID="TxtISOAPPaperTa" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOAPPaperTa" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOAPPaperTa"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX" align="center">
                                                                                                                                                        <asp:TextBox ID="TxtISOAPPaperTb" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOAPPaperTb" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOAPPaperTb"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 4%">
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX" align="center">
                                                                                                                                                        <asp:TextBox ID="TxtISOTHPaperTL" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOTHPaperTL" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOTHPaperTL"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX" align="center">
                                                                                                                                                        <asp:TextBox ID="TxtISOTHPaperTa" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOTHPaperTa" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOTHPaperTa"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX" align="center">
                                                                                                                                                        <asp:TextBox ID="TxtISOTHPaperTb" runat="server" Width="35px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOTHPaperTb" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOTHPaperTb"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                            </table>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                            <td style="width: 53%" valign="top">
                                                                                                                                <table border="0" width="100%" cellpadding="0" cellspacing="5">
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            <b>TVI</b>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            <table border="0" width="100%" cellpadding="1" cellspacing="0" class="TABLEBOX">
                                                                                                                                                <tr class="TRTABLEBOX">
                                                                                                                                                    <td>
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                    <td colspan="4" align="center">
                                                                                                                                                        Targets
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 2%">
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                    <td colspan="4" align="center">
                                                                                                                                                        Thresholds
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr class="TRTABLEBOX">
                                                                                                                                                    <td style="width: 14%" class="TDTABLEBOXLEFT">
                                                                                                                                                        Patch
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 12%" align="center">
                                                                                                                                                        C
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 12%" align="center">
                                                                                                                                                        M
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 12%" align="center">
                                                                                                                                                        Y
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 12%" align="center">
                                                                                                                                                        K
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 2%">
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 9%" align="center">
                                                                                                                                                        &Delta;C
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 9%" align="center">
                                                                                                                                                        &Delta;M
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 9%" align="center">
                                                                                                                                                        &Delta;Y
                                                                                                                                                    </td>
                                                                                                                                                    <td style="width: 9%" align="center">
                                                                                                                                                        &Delta;K
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td class="TDTABLEBOXLEFT">
                                                                                                                                                        1/4 Tone
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO25CA" runat="server" Width="30px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO25CA" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO25CA"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO25MA" runat="server" Width="30px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO25MA" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO25MA"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO25YA" runat="server" Width="30px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO25YA" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO25YA"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO25KA" runat="server" Width="30px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO25KA" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO25KA"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO25CT" runat="server" Width="20px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO25CT" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO25CT"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO25MT" runat="server" Width="20px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO25MT" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO25MT"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO25YT" runat="server" Width="20px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO25YT" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO25YT"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO25KT" runat="server" Width="20px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO25KT" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO25KT"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td class="TDTABLEBOXLEFT">
                                                                                                                                                        Mid Tone
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO50CA" runat="server" Width="30px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO50CA" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO50CA"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO50MA" runat="server" Width="30px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO50MA" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO50MA"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO50YA" runat="server" Width="30px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO50YA" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO50YA"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO50KA" runat="server" Width="30px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO50KA" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO50KA"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO50CT" runat="server" Width="20px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO50CT" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO50CT"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO50MT" runat="server" Width="20px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO50MT" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO50MT"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO50YT" runat="server" Width="20px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO50YT" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO50YT"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO50KT" runat="server" Width="20px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO50KT" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO50KT"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td class="TDTABLEBOXLEFT">
                                                                                                                                                        3/4 Tone
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO75CA" runat="server" Width="30px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO75CA" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO75CA"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO75MA" runat="server" Width="30px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO75MA" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO75MA"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO75YA" runat="server" Width="30px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO75YA" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO75YA"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO75KA" runat="server" Width="30px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO75KA" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO75KA"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO75CT" runat="server" Width="20px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO75CT" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO75CT"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO75MT" runat="server" Width="20px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO75MT" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO75MT"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO75YT" runat="server" Width="20px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO75YT" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO75YT"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO75KT" runat="server" Width="20px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO75KT" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO75KT"></asp:RequiredFieldValidator>
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
                                                                                                                                        <td>
                                                                                                                                            <b>TVI Spreads</b>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            <table border="0" width="100%" cellpadding="1" cellspacing="0" class="TABLEBOX">
                                                                                                                                                <tr class="TRTABLEBOX">
                                                                                                                                                    <td align="center" colspan="3">
                                                                                                                                                        Max Spreads
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr class="TRTABLEBOX">
                                                                                                                                                    <td align="center">
                                                                                                                                                        1/4 Tone
                                                                                                                                                    </td>
                                                                                                                                                    <td align="center">
                                                                                                                                                        Mid Tone
                                                                                                                                                    </td>
                                                                                                                                                    <td align="center">
                                                                                                                                                        3/4 Tone
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td align="center" class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOQuarterTone" runat="server" Width="50px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOQuarterTone" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOQuarterTone"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td align="center" class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOMidTone" runat="server" Width="50px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOMidTone" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOMidTone"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td align="center" class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISO3QuarterTone" runat="server" Width="50px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISO3QuarterTone" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISO3QuarterTone"></asp:RequiredFieldValidator>
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
                                                                                                                                        <td>
                                                                                                                                            <b>Density</b>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            <table border="0" width="100%" cellpadding="1" cellspacing="0" class="TABLEBOX">
                                                                                                                                                <tr class="TRTABLEBOX">
                                                                                                                                                    <td colspan="4" align="center">
                                                                                                                                                        Targets
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr class="TRTABLEBOX">
                                                                                                                                                    <td align="center">
                                                                                                                                                        Cyan
                                                                                                                                                    </td>
                                                                                                                                                    <td align="center">
                                                                                                                                                        Magenta
                                                                                                                                                    </td>
                                                                                                                                                    <td align="center">
                                                                                                                                                        Yellow
                                                                                                                                                    </td>
                                                                                                                                                    <td align="center">
                                                                                                                                                        Black
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                                <tr>
                                                                                                                                                    <td align="center" class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOCyanDT" runat="server" Width="50px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOCyanDT" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOCyanDT"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td align="center" class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOMagendaDT" runat="server" Width="50px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOMagendaDT" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOMagendaDT"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td align="center" class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOYellowDT" runat="server" Width="50px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOYellowDT" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOYellowDT"></asp:RequiredFieldValidator>
                                                                                                                                                    </td>
                                                                                                                                                    <td align="center" class="TDTABLEBOX">
                                                                                                                                                        <asp:TextBox ID="TxtISOBalckDT" runat="server" Width="50px" CssClass="TEXTBOX"></asp:TextBox><asp:RequiredFieldValidator
                                                                                                                                                            ID="RFTxtISOBalckDT" runat="server" ErrorMessage="*" ValidationGroup="PrintTarget"
                                                                                                                                                            ControlToValidate="TxtISOBalckDT"></asp:RequiredFieldValidator>
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
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                    </ajaxtoolkit:TabPanel>
                                                                    <ajaxtoolkit:TabPanel ID="TabPanPrintCondition" Width="100%" runat="server" HeaderText="Print Condition">
                                                                        <ContentTemplate>
                                                                            <table border="0" width="100%" style="height: 440px;" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td valign="top">
                                                                                        <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td valign="top">
                                                                                                    <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                                                                                        <tr>
                                                                                                            <td align="center">
                                                                                                                <fieldset>
                                                                                                                    <legend class="subtitle">General Data</legend>
                                                                                                                    <table border="0" width="99%" cellpadding="0" cellspacing="5">
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px; width: 50%;">
                                                                                                                                <b>Substrate Description</b>
                                                                                                                            </td>
                                                                                                                            <td style="width: 50%;">
                                                                                                                                <b>Ink</b>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px">
                                                                                                                                <asp:TextBox ID="TxtGDSubstrateDesc" PCTypeId="1" runat="server" Width="320px" MaxLength="100"
                                                                                                                                    CssClass="TEXTBOX"></asp:TextBox>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:TextBox ID="TxtGDInk" PCTypeId="2" runat="server" Width="320px" MaxLength="100"
                                                                                                                                    CssClass="TEXTBOX"></asp:TextBox>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px">
                                                                                                                                *Include Manufacturer, Name, Finish, Grade Weight
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                *Include Manufacturer, Set Name
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </fieldset>
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
                                                                                            <tr id="TrScreen" runat="server">
                                                                                                <td valign="top">
                                                                                                    <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                                                                                        <tr>
                                                                                                            <td align="center">
                                                                                                                <fieldset>
                                                                                                                    <legend class="subtitle">Screen Printing Data</legend>
                                                                                                                    <table border="0" width="99%" cellpadding="0" cellspacing="5">
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px; width: 50%;">
                                                                                                                                <b>Stencil</b>
                                                                                                                            </td>
                                                                                                                            <td style="width: 50%;">
                                                                                                                                <b>Angle and Frequency</b>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px">
                                                                                                                                <asp:TextBox ID="TxtSPDStencil" PCTypeId="9" runat="server" Width="320px" MaxLength="100"
                                                                                                                                    CssClass="TEXTBOX"></asp:TextBox>
                                                                                                                            </td>
                                                                                                                            <td rowspan="8" valign="top">
                                                                                                                                <table border="0" width="99%" cellpadding="0" cellspacing="5">
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            Cyan
                                                                                                                                        </td>
                                                                                                                                        <td>
                                                                                                                                            <asp:TextBox ID="TxtSPDCyan" PCTypeId="12" runat="server" Width="120px" MaxLength="100"
                                                                                                                                                CssClass="TEXTBOX"></asp:TextBox>
                                                                                                                                        </td>
                                                                                                                                        <td>
                                                                                                                                            Magenta
                                                                                                                                        </td>
                                                                                                                                        <td>
                                                                                                                                            <asp:TextBox ID="TxtSPDMagenta" PCTypeId="13" runat="server" Width="120px" MaxLength="100"
                                                                                                                                                CssClass="TEXTBOX"></asp:TextBox>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            Yellow
                                                                                                                                        </td>
                                                                                                                                        <td>
                                                                                                                                            <asp:TextBox ID="TxtSPDYellow" PCTypeId="14" runat="server" Width="120px" MaxLength="100"
                                                                                                                                                CssClass="TEXTBOX"></asp:TextBox>
                                                                                                                                        </td>
                                                                                                                                        <td>
                                                                                                                                            Black
                                                                                                                                        </td>
                                                                                                                                        <td>
                                                                                                                                            <asp:TextBox ID="TxtSPDBlack" PCTypeId="15" runat="server" Width="120px" MaxLength="100"
                                                                                                                                                CssClass="TEXTBOX"></asp:TextBox>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td colspan="4">
                                                                                                                                            *Include angle and frequency
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px">
                                                                                                                                *Include Mesh Count, Coating
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px">
                                                                                                                                <b>Rip</b>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px">
                                                                                                                                <asp:TextBox ID="TxtSPDRip" PCTypeId="10" runat="server" Width="320px" MaxLength="100"
                                                                                                                                    CssClass="TEXTBOX"></asp:TextBox>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px">
                                                                                                                                *Include Rip Name, Version
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px">
                                                                                                                                <b>CTS/Film Device</b>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px">
                                                                                                                                <asp:TextBox ID="TxtSPDCTSFilmDevice" PCTypeId="11" runat="server" Width="320px"
                                                                                                                                    MaxLength="100" CssClass="TEXTBOX"></asp:TextBox>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px">
                                                                                                                                *Include Manufacturer, Model
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </fieldset>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr id="TrWideFormat" runat="server">
                                                                                                <td valign="top">
                                                                                                    <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                                                                                        <tr>
                                                                                                            <td align="center">
                                                                                                                <fieldset>
                                                                                                                    <legend class="subtitle">Wide Format Printing Data</legend>
                                                                                                                    <table border="0" width="99%" cellpadding="0" cellspacing="5">
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px; width: 50%;">
                                                                                                                                <b>Rip</b>
                                                                                                                            </td>
                                                                                                                            <td style="width: 50%;">
                                                                                                                                <b>Print Mode</b>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px">
                                                                                                                                <asp:TextBox ID="TxtWFRip" PCTypeId="10" runat="server" Width="320px" MaxLength="100"
                                                                                                                                    CssClass="TEXTBOX"></asp:TextBox>
                                                                                                                            </td>
                                                                                                                            <td valign="top">
                                                                                                                                <asp:TextBox ID="TxtWFPrintMode" PCTypeId="17" runat="server" Width="320px" MaxLength="100"
                                                                                                                                    CssClass="TEXTBOX"></asp:TextBox>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px">
                                                                                                                                *Include Rip Name, Version
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px">
                                                                                                                                <b>Screening Type</b>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <b>UV Lamp Settings</b>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px">
                                                                                                                                <asp:TextBox ID="TxtWFScreeningType" PCTypeId="7" runat="server" Width="320px" MaxLength="100"
                                                                                                                                    CssClass="TEXTBOX"></asp:TextBox>
                                                                                                                            </td>
                                                                                                                            <td valign="top">
                                                                                                                                <asp:TextBox ID="TxtWFUVLampSettings" PCTypeId="16" runat="server" Width="320px"
                                                                                                                                    MaxLength="100" CssClass="TEXTBOX"></asp:TextBox>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </fieldset>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr id="TrOffsett" runat="server">
                                                                                                <td valign="top">
                                                                                                    <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                                                                                        <tr>
                                                                                                            <td align="center">
                                                                                                                <fieldset>
                                                                                                                    <legend class="subtitle">Offsett Printing Data</legend>
                                                                                                                    <table border="0" width="99%" cellpadding="0" cellspacing="5">
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px; width: 25%;">
                                                                                                                                <b>ISO 2846 Ink</b>
                                                                                                                            </td>
                                                                                                                            <td style="width: 25%;">
                                                                                                                                <b>Ink Order</b>
                                                                                                                            </td>
                                                                                                                            <td style="width: 50%;">
                                                                                                                                <b>Fountain Solution</b>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px">
                                                                                                                                <asp:TextBox ID="TxtOPDISO2846Ink" PCTypeId="3" runat="server" Width="160px" MaxLength="100"
                                                                                                                                    CssClass="TEXTBOX"></asp:TextBox>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:DropDownList ID="DdlOPDInkOrder" PCTypeId="4" runat="server" CssClass="DROPDOWNLIST">
                                                                                                                                    <asp:ListItem Selected="True" Text="K C M Y" Value="KCMY"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="C M Y K" Value="CMYK"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="Y M C K" Value="YMCK"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="C K M Y" Value="CKMY"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="C K Y M" Value="CKYM"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="C M K Y" Value="CMKY"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="C Y K M" Value="CYKM"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="C Y M K" Value="CYMK"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="K C Y M" Value="KCYM"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="K M C Y" Value="KMCY"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="K M Y C" Value="KMYC"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="K Y C M" Value="KYCM"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="K Y M C" Value="KYMC"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="M C K Y" Value="MCKY"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="M C Y K" Value="MCYK"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="M K C Y	" Value="MKCY"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="M K Y C" Value="MKYC"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="M Y C K" Value="MYCK"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="M Y K C" Value="MYKC"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="Y C K M" Value="YCKM"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="Y C M K" Value="YCMK"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="Y K C M" Value="YKCM"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="Y K M C" Value="YKMC"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="Y M K C" Value="YMKC"></asp:ListItem>
                                                                                                                                </asp:DropDownList>
                                                                                                                            </td>
                                                                                                                            <td valign="top">
                                                                                                                                <asp:TextBox ID="TxtOPDFountainSolution" PCTypeId="5" runat="server" Width="320px"
                                                                                                                                    MaxLength="100" CssClass="TEXTBOX"></asp:TextBox>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td colspan="2">
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                *Include Manufacturer, Name
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px" colspan="2">
                                                                                                                                <b>Blanket</b>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <b>Screening</b>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px" colspan="2">
                                                                                                                                <asp:TextBox ID="TxtOPDBlanket" PCTypeId="6" runat="server" Width="320px" MaxLength="100"
                                                                                                                                    CssClass="TEXTBOX"></asp:TextBox>
                                                                                                                            </td>
                                                                                                                            <td valign="top">
                                                                                                                                <asp:TextBox ID="TxtOPDScreening" PCTypeId="7" runat="server" Width="320px" MaxLength="100"
                                                                                                                                    CssClass="TEXTBOX"></asp:TextBox>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px" colspan="2">
                                                                                                                                *Include Manufacturer, Name
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                *Include Type(AM-FM), Dot Shape, Frequency
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px" colspan="3">
                                                                                                                                <b>Blanket Age</b>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px" colspan="3">
                                                                                                                                <asp:TextBox ID="TxtOPDBlanketAge" PCTypeId="8" runat="server" Width="160px" MaxLength="100"
                                                                                                                                    CssClass="TEXTBOX"></asp:TextBox>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </fieldset>
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
                                                                                                <td valign="top">
                                                                                                    <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                                                                                        <tr>
                                                                                                            <td align="center">
                                                                                                                <fieldset>
                                                                                                                    <legend class="subtitle">Notes</legend>
                                                                                                                    <table border="0" width="99%" cellpadding="0" cellspacing="5">
                                                                                                                        <tr>
                                                                                                                            <td style="padding-left: 5px;">
                                                                                                                                <asp:TextBox ID="TxtNotes" PCTypeId="18" runat="server" Columns="130" Rows="5" TextMode="MultiLine"
                                                                                                                                    onkeypress="return imposeMaxLength(this, 250);" MaxLength="250" CssClass="TEXTBOX"></asp:TextBox>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </fieldset>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                    </ajaxtoolkit:TabPanel>
                                                                    <ajaxtoolkit:TabPanel ID="TabPanMeasurementSetup" Width="100%" runat="server" HeaderText="Measurement Setup">
                                                                        <ContentTemplate>
                                                                            <table border="0" width="100%" style="height: 440px; padding: 20px;" cellpadding="0"
                                                                                cellspacing="0">
                                                                                <tr style="height: 40px;">
                                                                                    <td valign="top">
                                                                                        <b>
                                                                                            <asp:CheckBox ID="ChkUseMeasurementPlugin" CssClass="subtitle" runat="server" Text="Use Measurement Plugin" /></b>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 120px;">
                                                                                    <td valign="top">
                                                                                        <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td valign="top" style="height: 120px;">
                                                                                                    <fieldset style="width: 90%;">
                                                                                                        <legend class="subtitle">Measurement Settings</legend>
                                                                                                        <table border="0" width="100%" cellpadding="2" cellspacing="2">
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    Illuminant :
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:DropDownList ID="ddlMeasurentIlluminant" runat="server" CssClass="DROPDOWNLIST">
                                                                                                                        <asp:ListItem Selected="True" Text="D50" Value="1"></asp:ListItem>
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    Observer :
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:DropDownList ID="ddlMeasurentObserver" runat="server" CssClass="DROPDOWNLIST">
                                                                                                                        <asp:ListItem Selected="True" Text="2" Value="1"></asp:ListItem>
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    Density Type :
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:DropDownList ID="ddlMeasurentDensityType" runat="server" CssClass="DROPDOWNLIST">
                                                                                                                        <asp:ListItem Selected="True" Text="StatusT" Value="1"></asp:ListItem>
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </fieldset>
                                                                                                </td>
                                                                                                <td valign="top">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                                        <ContentTemplate>
                                                                                                            <table border="0" width="90%" cellpadding="2" cellspacing="2">
                                                                                                                <tr>
                                                                                                                    <td class="subtitle">
                                                                                                                        Device Type :
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:DropDownList ID="ddlMeasurentDeviceType" runat="server" CssClass="DROPDOWNLIST"
                                                                                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlMeasurentDeviceType_SelectedIndexChanged">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="subtitle">
                                                                                                                        Alignment Chart :
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:DropDownList ID="ddlMeasurentAlignmentChart" runat="server" CssClass="DROPDOWNLIST">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
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
                                                                        </ContentTemplate>
                                                                    </ajaxtoolkit:TabPanel>
                                                                </ajaxtoolkit:TabContainer>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="padding-right: 15px;">
                                                                <asp:Button ID="BtnSavePrintTargetCondition" runat="server" CssClass="BUTTON" ValidationGroup="PrintTarget"
                                                                    Text="Save : Print Target & Condition" OnClick="BtnSavePrintTargetCondition_Click"
                                                                    OnClientClick="return GetProjectClientDate();" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table id="TblMeasurement" runat="server" border="0" width="100%" style="height: 640px;"
                                                        visible="false" cellpadding="0" cellspacing="0" class="TABLE1">
                                                        <tr id="TrPlugin" runat="server">
                                                            <td valign="top">
                                                                <iframe id="IFramePlugin" runat="server" name="IFramePlugin" style="height: 600px;
                                                                    width: 100%; display: none" frameborder="0" scrolling="auto"></iframe>
                                                            </td>
                                                        </tr>
                                                        <tr id="TrFlexObject" runat="server">
                                                            <td valign="top">
                                                                <script language="JavaScript" type="text/javascript">
                                                                    var hasProductInstall = DetectFlashVer(6, 0, 65);
                                                                    var hasRequestedVersion = DetectFlashVer(requiredMajorVersion, requiredMinorVersion, requiredRevision);
                                                                    if (hasProductInstall && !hasRequestedVersion) {
                                                                        var MMPlayerType = (isIE == true) ? "ActiveX" : "PlugIn";
                                                                        var MMredirectURL = window.location;
                                                                        document.title = document.title.slice(0, 47) + " - Flash Player Installation";
                                                                        var MMdoctitle = document.title;
                                                                        AC_FL_RunContent(
                                                                        "src", "playerProductInstall",
                                                                        "FlashVars", "",
                                                                        "width", "100%",
                                                                        "height", "610",
                                                                        "align", "middle",
                                                                        "id", "FileUploadApp",
                                                                        "quality", "high",
                                                                        "bgcolor", "#6BB19C",
                                                                        "name", "FileUploadApp",
                                                                        "allowScriptAccess", "sameDomain",
                                                                        "type", "application/x-shockwave-flash",
                                                                        "pluginspage", "http://www.adobe.com/go/getflashplayer"
                                                                        );
                                                                    } else if (hasRequestedVersion) {
                                                                        AC_FL_RunContent(
                                                                        "src", "Flex/FileUploadApp",
                                                                        "FlashVars", "appurl=<%=appurl%>&uploadurl=<%=uploadurl%>&pressrunid=<%=pressrunid%>&serialno=<%=serialno%>&pressrunclose=<%=pressrunclose%>",
                                                                        "width", "100%",
                                                                        "height", "610",
                                                                        "align", "middle",
                                                                        "id", "FileUploadApp",
                                                                        "quality", "high",
                                                                        "bgcolor", "#6BB19C",
                                                                        "name", "FileUploadApp",
                                                                        "allowScriptAccess", "sameDomain",
                                                                        "type", "application/x-shockwave-flash",
                                                                        "pluginspage", "http://www.adobe.com/go/getflashplayer"
                                                                        );
                                                                    } else {
                                                                        var alternateContent = 'Alternate HTML content should be placed here. '
                                                                        + 'This content requires the Adobe Flash Player. '
                                                                        + '<a href=http://www.adobe.com/go/getflash/>Get Flash</a>';
                                                                        document.write(alternateContent);  // insert non-flash content
                                                                    }
                                                                </script>
                                                                <noscript>
                                                                    <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" id="FileUploadApp" width="100%"
                                                                        height="610" codebase="http://fpdownload.macromedia.com/get/flashplayer/current/swflash.cab">
                                                                        <param name="movie" value="Flex/FileUploadApp.swf" />
                                                                        <param name="quality" value="high" />
                                                                        <param name="wMode" value="transparent" />
                                                                        <param name="bgcolor" value="#6BB19C" />
                                                                        <param name="allowScriptAccess" value="sameDomain" />
                                                                        <embed src="Flex/FileUploadApp.swf" quality="high" bgcolor="#6BB19C" width="100%"
                                                                            height="610" wmode="transparent" name="admin" align="middle" play="true" loop="false"
                                                                            quality="high" allowscriptaccess="sameDomain" type="application/x-shockwave-flash"
                                                                            pluginspage="http://www.adobe.com/go/getflashplayer"></embed>
                                                                    </object>
                                                                </noscript>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                    <ContentTemplate>
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                            <tr>
                                                                                <td align="right" style="padding-right: 15px; padding-bottom: 5px;">
                                                                                    <asp:Label ID="lblAnalyzeMsg" runat="server" CssClass="ERROR"></asp:Label>
                                                                                </td>
                                                                                <td align="right" style="padding-right: 15px; padding-bottom: 5px;">
                                                                                    <asp:Button ID="BtnNextAnalyze" runat="server" CssClass="BUTTON" Text="Next : Analyze"
                                                                                        OnClick="BtnNextAnalyze_Click" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                                <asp:Button ID="BtnNextAnalyze1" runat="server" CssClass="BUTTON" Text="Next : Analyze"
                                                                    Style="display: none;" OnClick="BtnNextAnalyze1_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table id="TblAnalyze" runat="server" border="0" width="100%" style="height: 640px;"
                                                        visible="false" cellpadding="0" cellspacing="0" class="TABLE1">
                                                        <tr>
                                                            <td>
                                                                <table id="tblAverageOfMeasurements" runat="server" border="0" width="100%" cellpadding="5"
                                                                    cellspacing="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:CheckBox ID="chkAnalyzeAverageOfMeasurements" runat="server" Checked="false"
                                                                                Font-Bold="true" AutoPostBack="true" OnCheckedChanged="chkAnalyzeAverageOfMeasurements_Click">
                                                                            </asp:CheckBox>
                                                                            <span style="font-weight: bold; vertical-align: text-top;">Analyze Average Of Measurements</span>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">
                                                                <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td>
                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                                                                <tr>
                                                                                    <td class="green-title">
                                                                                        &nbsp; Analyze Measurement &nbsp; &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:UpdatePanel ID="UP_TrendLineCharts" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <div style="overflow: auto; width: 750px;">
                                                                                                    <DCWC:Chart ID="CHRT_TrendLineDeltaE" runat="server" Height="140px" ImageUrl="~/UploadFiles/temp/CHRT_TrendDeltaE_#SEQ(300,3)"
                                                                                                        BackGradientType="Center" Palette="Kindergarten" BackGradientEndColor="#DFDFDF"
                                                                                                        BorderLineColor="#808080">
                                                                                                        <Legends>
                                                                                                            <DCWC:Legend Name="Default">
                                                                                                            </DCWC:Legend>
                                                                                                        </Legends>
                                                                                                        <Series>
                                                                                                            <DCWC:Series Name="Series0" ChartType="Spline" Color="Transparent" ChartArea="ChartArea1"
                                                                                                                BorderWidth="2" ShowInLegend="false">
                                                                                                            </DCWC:Series>
                                                                                                            <DCWC:Series Name="Series1" ChartType="Spline" Color="Green" ChartArea="ChartArea1"
                                                                                                                BorderWidth="2" ShowInLegend="false">
                                                                                                            </DCWC:Series>
                                                                                                            <DCWC:Series Name="Series2" ChartType="Spline" Color="Red" ChartArea="ChartArea1"
                                                                                                                BorderWidth="2" ShowInLegend="false">
                                                                                                            </DCWC:Series>
                                                                                                            <DCWC:Series Name="Series3" ChartType="Spline" Color="Blue" ChartArea="ChartArea1"
                                                                                                                BorderWidth="2" ShowInLegend="false">
                                                                                                            </DCWC:Series>
                                                                                                            <DCWC:Series Name="Series4" ChartType="Spline" Color="Blue" ChartArea="ChartArea1"
                                                                                                                BorderWidth="2" ShowInLegend="false">
                                                                                                            </DCWC:Series>
                                                                                                            <DCWC:Series Name="Series5" ChartType="Spline" Color="Blue" ChartArea="ChartArea1"
                                                                                                                BorderWidth="2" ShowInLegend="false">
                                                                                                            </DCWC:Series>
                                                                                                            <DCWC:Series Name="Series6" ChartType="Spline" Color="Blue" ChartArea="ChartArea1"
                                                                                                                BorderWidth="2" ShowInLegend="false">
                                                                                                            </DCWC:Series>
                                                                                                            <DCWC:Series Name="Series7" ChartType="Spline" Color="Blue" ChartArea="ChartArea1"
                                                                                                                BorderWidth="2" ShowInLegend="false">
                                                                                                            </DCWC:Series>
                                                                                                        </Series>
                                                                                                        <ChartAreas>
                                                                                                            <DCWC:ChartArea Name="ChartArea1">
                                                                                                                <Position Height="99" Width="99" X="0" Y="0" />
                                                                                                                <AxisY LabelsAutoFit="False" Enabled="True" Title="&Delta; E" TitleFont="Verdana, 8pt"
                                                                                                                    Interval="Auto">
                                                                                                                    <MajorGrid LineColor="Beige" />
                                                                                                                </AxisY>
                                                                                                                <AxisX LabelsAutoFit="False" Interval="1" TitleFont="Verdana, 5pt" Title="">
                                                                                                                    <MajorGrid LineColor="Beige" />
                                                                                                                    <LabelStyle FontAngle="0" />
                                                                                                                </AxisX>
                                                                                                            </DCWC:ChartArea>
                                                                                                        </ChartAreas>
                                                                                                    </DCWC:Chart>
                                                                                                </div>
                                                                                                <asp:HiddenField ID="HDTrendDeltaAll" runat="server" />
                                                                                                <asp:Button ID="HDButTrendDelta" runat="server" Text="Button" OnClick="HDButTrendDelta_Click"
                                                                                                    Style="display: none" />
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <iframe id="FRM1" runat="server" name="FRM1" style="height: 445px; width: 100%; display: none"
                                                                                frameborder="0" scrolling="no"></iframe>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="padding-right: 15px; padding-bottom: 5px;">
                                                                <%-- <asp:Button ID="BtnISOCurves" runat="server" CssClass="BUTTON" Text="Next: Create ISO Curves"
                                                                    OnClick="BtnISOCurves_Click" />&nbsp;&nbsp;--%>
                                                                <asp:Button ID="BtnNextCreateCurves" runat="server" CssClass="BUTTON" Text="Next: Create Curves"
                                                                    OnClick="BtnNextCreateCurves_Click" />&nbsp;&nbsp;
                                                                <asp:Button ID="BtnGenerateReport" OnClick="BtnGenerateReport_Click" runat="server"
                                                                    CssClass="BUTTON" Text="Generate PDF Report" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table id="TblGeneratePDFReport" runat="server" border="0" width="100%" style="height: 640px;"
                                                        visible="false" cellpadding="0" cellspacing="0" class="TABLE1">
                                                        <tr>
                                                            <td valign="top">
                                                                <iframe id="FRMGeneratePDFReport" runat="server" name="FRMGeneratePDFReport" style="height: 600px;
                                                                    width: 100%; display: none" frameborder="0" scrolling="no"></iframe>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table id="TblExportCurves" runat="server" border="0" width="100%" style="height: 640px;"
                                                        visible="false" cellpadding="0" cellspacing="0" class="TABLE1">
                                                        <tr>
                                                            <td valign="top">
                                                                <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td style="padding: 8px">
                                                                            <b>Export Curves</b>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                                                                <tr>
                                                                                    <td class="green-title">
                                                                                        &nbsp; Measurement List
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <table style="width: 98%; border: 1PX SOLID #C1C1C1" cellspacing="0" cellpadding="0"
                                                                                            border="0">
                                                                                            <tr>
                                                                                                <td style="width: 10%; border-right: 1PX SOLID #C1C1C1;" class="BGRIDTITLE" align="center">
                                                                                                    Used
                                                                                                </td>
                                                                                                <td style="width: 50%; border-right: 1PX SOLID #C1C1C1;" class="BGRIDTITLE" align="center">
                                                                                                    Measurement Name
                                                                                                </td>
                                                                                                <td style="width: 40%; border-right: 1PX SOLID #C1C1C1;" class="BGRIDTITLE" align="center">
                                                                                                    Upload Date Time
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                        <rad:RadGrid ID="RG_SyncMeasurement" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                                                                            AllowSorting="true" AllowFilteringByColumn="false" Width="100%" Skin="Green">
                                                                                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                                                            <ClientSettings>
                                                                                                <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="100" />
                                                                                            </ClientSettings>
                                                                                            <PagerStyle Position="Top" />
                                                                                            <MasterTableView>
                                                                                                <Columns>
                                                                                                    <rad:GridTemplateColumn DataField="IsUsed" HeaderText="Used" ItemStyle-Width="10%"
                                                                                                        HeaderStyle-Width="10%" AllowFiltering="false" ItemStyle-HorizontalAlign="Center">
                                                                                                        <ItemTemplate>
                                                                                                            <img src='<%# UsedImageName(Eval("IsUsed").ToString()) %>' height="10px" width="12px" />
                                                                                                        </ItemTemplate>
                                                                                                    </rad:GridTemplateColumn>
                                                                                                    <rad:GridTemplateColumn DataField="MeasurementName" HeaderText="MeasurementName"
                                                                                                        ItemStyle-Width="50%" HeaderStyle-Width="50%" AllowFiltering="false" ItemStyle-HorizontalAlign="Center">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Lbl_Measurement" runat="server" Text='<%# OriginalFileNameReturn(Eval("MeasurementName").ToString()) %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </rad:GridTemplateColumn>
                                                                                                    <rad:GridTemplateColumn DataField="UploadDateTime" DataType="System.DateTime" UniqueName="UploadDateTime"
                                                                                                        AllowFiltering="false" Visible="true" HeaderText="Upload Date Time" ItemStyle-Width="40%"
                                                                                                        HeaderStyle-Width="40%" ItemStyle-HorizontalAlign="Center">
                                                                                                        <ItemTemplate>
                                                                                                            <%# DataBinder.Eval(Container.DataItem, "UploadDateTime").ToString().Replace(":000", " ") %>
                                                                                                        </ItemTemplate>
                                                                                                    </rad:GridTemplateColumn>
                                                                                                </Columns>
                                                                                            </MasterTableView>
                                                                                        </rad:RadGrid>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <ajaxtoolkit:TabContainer ID="TabContainer2" runat="server" Width="100%" Style="padding: 0px">
                                                                                <ajaxtoolkit:TabPanel ID="TabExportView" Width="100%" runat="server" HeaderText="Export View"
                                                                                    Style="padding: 0px; width: 100%">
                                                                                    <ContentTemplate>
                                                                                        <asp:UpdatePanel ID="UpdPanExportView" runat="server" UpdateMode="Conditional">
                                                                                            <ContentTemplate>
                                                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                    <tr>
                                                                                                        <td colspan="2" style="padding: 5px">
                                                                                                            Curve Type :
                                                                                                            <asp:DropDownList ID="DdlCurveType" runat="server" Width="80px" CssClass="DROPDOWNLIST"
                                                                                                                AutoPostBack="True" OnSelectedIndexChanged="DdlCurveType_SelectedIndexChanged">
                                                                                                                <asp:ListItem Text="1%" Value="1"></asp:ListItem>
                                                                                                                <asp:ListItem Text="5%" Value="5"></asp:ListItem>
                                                                                                                <asp:ListItem Text="10%" Value="10"></asp:ListItem>
                                                                                                                <asp:ListItem Text="Harmony" Value="35"></asp:ListItem>
                                                                                                                <asp:ListItem Text="Nexus" Value="45"></asp:ListItem>
                                                                                                                <asp:ListItem Text="Rampage" Value="15"></asp:ListItem>
                                                                                                                <asp:ListItem Text="XMF" Value="25"></asp:ListItem>
                                                                                                                <asp:ListItem Text="Caldera" Value="55"></asp:ListItem>
                                                                                                                <%--  <asp:ListItem Text="&#916;Ref" Value="65"></asp:ListItem>--%>
                                                                                                            </asp:DropDownList>
                                                                                                            &nbsp;&nbsp;
                                                                                                            <asp:CheckBox ID="Chk_C" runat="server" Text="Cyan" Checked="true" AutoPostBack="True"
                                                                                                                OnCheckedChanged="Chk_CheckedChanged" />&nbsp;
                                                                                                            <asp:CheckBox ID="Chk_M" runat="server" Text="Magenta" Checked="true" AutoPostBack="True"
                                                                                                                OnCheckedChanged="Chk_CheckedChanged" />&nbsp;
                                                                                                            <asp:CheckBox ID="Chk_Y" runat="server" Text="Yellow" Checked="true" AutoPostBack="True"
                                                                                                                OnCheckedChanged="Chk_CheckedChanged" />&nbsp;
                                                                                                            <asp:CheckBox ID="Chk_K" runat="server" Text="Black" Checked="true" AutoPostBack="True"
                                                                                                                OnCheckedChanged="Chk_CheckedChanged" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr style="border: solid 1px;">
                                                                                                        <td style="width: 60%">
                                                                                                            <DCWC:Chart ID="CHRT_ExportCurve" runat="server" Width="410px" Height="350px" ImageType="Jpeg"
                                                                                                                ImageUrl="~/UploadFiles/temp/CHRT_ExportCurve" BackGradientType="Center" Palette="Kindergarten"
                                                                                                                BackGradientEndColor="White" BorderLineColor="#808080">
                                                                                                                <Series>
                                                                                                                    <DCWC:Series Name="Series1" ShowInLegend="false" Color="Cyan" BorderWidth="2" ChartType="Spline"
                                                                                                                        MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" MarkerSize="2">
                                                                                                                    </DCWC:Series>
                                                                                                                    <DCWC:Series Name="Series2" ShowInLegend="false" Color="Magenta" BorderWidth="2"
                                                                                                                        ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" MarkerSize="2">
                                                                                                                    </DCWC:Series>
                                                                                                                    <DCWC:Series Name="Series3" ShowInLegend="false" Color="Yellow" BorderWidth="2" ChartType="Spline"
                                                                                                                        MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" MarkerSize="2">
                                                                                                                    </DCWC:Series>
                                                                                                                    <DCWC:Series Name="Series4" ShowInLegend="false" Color="Black" BorderWidth="2" ChartType="Spline"
                                                                                                                        MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" MarkerSize="2">
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
                                                                                                                    <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                                                                                                        BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                                                                        <Position Height="99" Width="94" X="0" Y="2" />
                                                                                                                        <AxisY LabelsAutoFit="False" Title="" Interval="10" Minimum="0" Maximum="100">
                                                                                                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                                                        </AxisY>
                                                                                                                        <AxisX LabelsAutoFit="False" Margin="False" Interval="25" Minimum="0" Maximum="100"
                                                                                                                            Title="">
                                                                                                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                                                            <LabelStyle FontAngle="0" />
                                                                                                                        </AxisX>
                                                                                                                    </DCWC:ChartArea>
                                                                                                                </ChartAreas>
                                                                                                            </DCWC:Chart>
                                                                                                        </td>
                                                                                                        <td style="width: 40%">
                                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                <tr>
                                                                                                                    <td align="left">
                                                                                                                        <table style="width: 95%; border: 1PX SOLID #C1C1C1" cellspacing="0" cellpadding="0"
                                                                                                                            border="0">
                                                                                                                            <tr>
                                                                                                                                <td style="width: 36%; border-right: 1PX SOLID #C1C1C1;" class="BGRIDTITLE" align="center">
                                                                                                                                    Reference %
                                                                                                                                </td>
                                                                                                                                <td style="width: 16%; border-right: 1PX SOLID #C1C1C1;" class="BGRIDTITLE" align="center">
                                                                                                                                    C
                                                                                                                                </td>
                                                                                                                                <td style="width: 16%; border-right: 1PX SOLID #C1C1C1;" class="BGRIDTITLE" align="center">
                                                                                                                                    M
                                                                                                                                </td>
                                                                                                                                <td style="width: 16%; border-right: 1PX SOLID #C1C1C1;" class="BGRIDTITLE" align="center">
                                                                                                                                    Y
                                                                                                                                </td>
                                                                                                                                <td style="width: 16%; border-right: 1PX SOLID #C1C1C1;" class="BGRIDTITLE" align="center">
                                                                                                                                    K
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <rad:RadGrid ID="RGPressRunCurve" runat="server" AutoGenerateColumns="false" PagerStyle-Mode="NextPrevAndNumeric"
                                                                                                                            ShowHeader="true" AllowPaging="false" AllowSorting="false" AllowFilteringByColumn="false"
                                                                                                                            Width="100%" Skin="Green">
                                                                                                                            <ItemStyle HorizontalAlign="Left" Wrap="false" Width="100%" />
                                                                                                                            <ClientSettings>
                                                                                                                                <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="330" />
                                                                                                                            </ClientSettings>
                                                                                                                            <PagerStyle Position="Top" />
                                                                                                                            <HeaderStyle HorizontalAlign="Center" Width="100%" />
                                                                                                                            <MasterTableView>
                                                                                                                                <HeaderStyle HorizontalAlign="Center" Width="100%" />
                                                                                                                                <Columns>
                                                                                                                                    <rad:GridBoundColumn DataField="ReferenceNo" UniqueName="ReferenceNo" AllowFiltering="false"
                                                                                                                                        Visible="true" HeaderText="Reference %" ItemStyle-Width="36%" HeaderStyle-Width="36%"
                                                                                                                                        ItemStyle-HorizontalAlign="Center">
                                                                                                                                    </rad:GridBoundColumn>
                                                                                                                                    <rad:GridBoundColumn DataField="CMYK_C" UniqueName="CMYK_C" AllowFiltering="false"
                                                                                                                                        ItemStyle-BackColor="#E0F3FF" Visible="true" HeaderText="C" ItemStyle-Width="16%"
                                                                                                                                        HeaderStyle-Width="16%" ItemStyle-HorizontalAlign="Center">
                                                                                                                                    </rad:GridBoundColumn>
                                                                                                                                    <rad:GridBoundColumn DataField="CMYK_M" UniqueName="CMYK_M" AllowFiltering="false"
                                                                                                                                        ItemStyle-BackColor="#FFEFF9" Visible="true" HeaderText="M" ItemStyle-Width="16%"
                                                                                                                                        HeaderStyle-Width="16%" ItemStyle-HorizontalAlign="Center">
                                                                                                                                    </rad:GridBoundColumn>
                                                                                                                                    <rad:GridBoundColumn DataField="CMYK_Y" UniqueName="CMYK_Y" AllowFiltering="false"
                                                                                                                                        ItemStyle-BackColor="#FFFDEE" Visible="true" HeaderText="Y" ItemStyle-Width="16%"
                                                                                                                                        HeaderStyle-Width="16%" ItemStyle-HorizontalAlign="Center">
                                                                                                                                    </rad:GridBoundColumn>
                                                                                                                                    <rad:GridBoundColumn DataField="CMYK_K" UniqueName="CMYK_K" AllowFiltering="false"
                                                                                                                                        ItemStyle-BackColor="#E7EBEE" Visible="true" HeaderText="K" ItemStyle-Width="16%"
                                                                                                                                        HeaderStyle-Width="16%" ItemStyle-HorizontalAlign="Center">
                                                                                                                                    </rad:GridBoundColumn>
                                                                                                                                </Columns>
                                                                                                                            </MasterTableView>
                                                                                                                        </rad:RadGrid>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="2" align="right" style="padding-right: 15px; padding-top: 10px;">
                                                                                                            <asp:Button ID="btnExportProfileExportView" runat="server" CssClass="BUTTON" Text="Export Profile"
                                                                                                                OnClick="btnExportProfileExportView_Click" />
                                                                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                            <asp:Button ID="BtnExportCurvesExportView" runat="server" CssClass="BUTTON" Text="Export Curves"
                                                                                                                OnClick="BtnExportCurvesExportView_Click" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </ContentTemplate>
                                                                                            <Triggers>
                                                                                                <asp:PostBackTrigger ControlID="btnExportProfileExportView" />
                                                                                                <asp:PostBackTrigger ControlID="BtnExportCurvesExportView" />
                                                                                            </Triggers>
                                                                                        </asp:UpdatePanel>
                                                                                    </ContentTemplate>
                                                                                </ajaxtoolkit:TabPanel>
                                                                                <ajaxtoolkit:TabPanel ID="TabDeltaView" Width="100%" runat="server" HeaderText="&#916;View">
                                                                                    <ContentTemplate>
                                                                                        <asp:UpdatePanel ID="UpdPaDeltaView" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:RadioButton ID="rbtnDetlaRef" runat="server" Text="&#916;Ref" Checked="true"
                                                                                                                OnClick="funDeltaRefPrev('Ref');" GroupName="DeltaView" />
                                                                                                            <asp:RadioButton ID="rbtnDeltaPrev" runat="server" Text="&#916;Previous Run" GroupName="DeltaView"
                                                                                                                OnClick="funDeltaRefPrev('Previous');" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr id="trRef" style="border: solid 1px;">
                                                                                                        <td style="width: 60%">
                                                                                                            <DCWC:Chart ID="CHRT_ExportCurveDeltaRef" runat="server" Width="410px" Height="350px"
                                                                                                                ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CHRT_ExportCurveDeltaRef" BackGradientType="Center"
                                                                                                                Palette="Kindergarten" BackGradientEndColor="White" BorderLineColor="#808080">
                                                                                                                <Series>
                                                                                                                    <DCWC:Series Name="Series1" ShowInLegend="false" Color="Cyan" BorderWidth="2" ChartType="Spline"
                                                                                                                        MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" MarkerSize="2">
                                                                                                                    </DCWC:Series>
                                                                                                                    <DCWC:Series Name="Series2" ShowInLegend="false" Color="Magenta" BorderWidth="2"
                                                                                                                        ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" MarkerSize="2">
                                                                                                                    </DCWC:Series>
                                                                                                                    <DCWC:Series Name="Series3" ShowInLegend="false" Color="Yellow" BorderWidth="2" ChartType="Spline"
                                                                                                                        MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" MarkerSize="2">
                                                                                                                    </DCWC:Series>
                                                                                                                    <DCWC:Series Name="Series4" ShowInLegend="false" Color="Black" BorderWidth="2" ChartType="Spline"
                                                                                                                        MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" MarkerSize="2">
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
                                                                                                                    <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                                                                                                        BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                                                                        <Position Height="99" Width="94" X="0" Y="2" />
                                                                                                                        <AxisY LabelsAutoFit="False" Title="" Interval="Auto" Minimum="-2" Maximum="2">
                                                                                                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                                                        </AxisY>
                                                                                                                        <AxisX LabelsAutoFit="False" Margin="False" Interval="25" Minimum="0" Maximum="100"
                                                                                                                            Title="">
                                                                                                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                                                            <LabelStyle FontAngle="0" />
                                                                                                                        </AxisX>
                                                                                                                    </DCWC:ChartArea>
                                                                                                                </ChartAreas>
                                                                                                            </DCWC:Chart>
                                                                                                        </td>
                                                                                                        <td style="width: 40%">
                                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                <tr>
                                                                                                                    <td align="left">
                                                                                                                        <table style="width: 95%; border: 1PX SOLID #C1C1C1" cellspacing="0" cellpadding="0"
                                                                                                                            border="0">
                                                                                                                            <tr>
                                                                                                                                <td style="width: 36%; border-right: 1PX SOLID #C1C1C1;" class="BGRIDTITLE" align="center">
                                                                                                                                    Reference %
                                                                                                                                </td>
                                                                                                                                <td style="width: 16%; border-right: 1PX SOLID #C1C1C1;" class="BGRIDTITLE" align="center">
                                                                                                                                    C
                                                                                                                                </td>
                                                                                                                                <td style="width: 16%; border-right: 1PX SOLID #C1C1C1;" class="BGRIDTITLE" align="center">
                                                                                                                                    M
                                                                                                                                </td>
                                                                                                                                <td style="width: 16%; border-right: 1PX SOLID #C1C1C1;" class="BGRIDTITLE" align="center">
                                                                                                                                    Y
                                                                                                                                </td>
                                                                                                                                <td style="width: 16%; border-right: 1PX SOLID #C1C1C1;" class="BGRIDTITLE" align="center">
                                                                                                                                    K
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <rad:RadGrid ID="RGPressRunCurveDeltaRef" runat="server" AutoGenerateColumns="false"
                                                                                                                            PagerStyle-Mode="NextPrevAndNumeric" ShowHeader="true" AllowPaging="false" AllowSorting="false"
                                                                                                                            AllowFilteringByColumn="false" Width="100%" Skin="Green">
                                                                                                                            <ItemStyle HorizontalAlign="Left" Wrap="false" Width="100%" />
                                                                                                                            <ClientSettings>
                                                                                                                                <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="330" />
                                                                                                                            </ClientSettings>
                                                                                                                            <PagerStyle Position="Top" />
                                                                                                                            <HeaderStyle HorizontalAlign="Center" Width="100%" />
                                                                                                                            <MasterTableView>
                                                                                                                                <HeaderStyle HorizontalAlign="Center" Width="100%" />
                                                                                                                                <Columns>
                                                                                                                                    <rad:GridBoundColumn DataField="ReferenceNo" UniqueName="ReferenceNo" AllowFiltering="false"
                                                                                                                                        Visible="true" HeaderText="Reference %" ItemStyle-Width="36%" HeaderStyle-Width="36%"
                                                                                                                                        ItemStyle-HorizontalAlign="Center">
                                                                                                                                    </rad:GridBoundColumn>
                                                                                                                                    <rad:GridBoundColumn DataField="CMYK_C" UniqueName="CMYK_C" AllowFiltering="false"
                                                                                                                                        ItemStyle-BackColor="#E0F3FF" Visible="true" HeaderText="C" ItemStyle-Width="16%"
                                                                                                                                        HeaderStyle-Width="16%" ItemStyle-HorizontalAlign="Center">
                                                                                                                                    </rad:GridBoundColumn>
                                                                                                                                    <rad:GridBoundColumn DataField="CMYK_M" UniqueName="CMYK_M" AllowFiltering="false"
                                                                                                                                        ItemStyle-BackColor="#FFEFF9" Visible="true" HeaderText="M" ItemStyle-Width="16%"
                                                                                                                                        HeaderStyle-Width="16%" ItemStyle-HorizontalAlign="Center">
                                                                                                                                    </rad:GridBoundColumn>
                                                                                                                                    <rad:GridBoundColumn DataField="CMYK_Y" UniqueName="CMYK_Y" AllowFiltering="false"
                                                                                                                                        ItemStyle-BackColor="#FFFDEE" Visible="true" HeaderText="Y" ItemStyle-Width="16%"
                                                                                                                                        HeaderStyle-Width="16%" ItemStyle-HorizontalAlign="Center">
                                                                                                                                    </rad:GridBoundColumn>
                                                                                                                                    <rad:GridBoundColumn DataField="CMYK_K" UniqueName="CMYK_K" AllowFiltering="false"
                                                                                                                                        ItemStyle-BackColor="#E7EBEE" Visible="true" HeaderText="K" ItemStyle-Width="16%"
                                                                                                                                        HeaderStyle-Width="16%" ItemStyle-HorizontalAlign="Center">
                                                                                                                                    </rad:GridBoundColumn>
                                                                                                                                </Columns>
                                                                                                                            </MasterTableView>
                                                                                                                        </rad:RadGrid>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr id="trPrev" style="display: none; border: solid 1px;">
                                                                                                        <td style="width: 60%">
                                                                                                            <DCWC:Chart ID="CHRT_ExportCurveDeltaPrev" runat="server" Width="410px" Height="350px"
                                                                                                                ImageType="Jpeg" ImageUrl="~/UploadFiles/temp/CHRT_ExportCurveDeltaPrev" BackGradientType="Center"
                                                                                                                Palette="Kindergarten" BackGradientEndColor="White" BorderLineColor="#808080">
                                                                                                                <Series>
                                                                                                                    <DCWC:Series Name="Series1" ShowInLegend="false" Color="Cyan" BorderWidth="2" ChartType="Spline"
                                                                                                                        MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" MarkerSize="2">
                                                                                                                    </DCWC:Series>
                                                                                                                    <DCWC:Series Name="Series2" ShowInLegend="false" Color="Magenta" BorderWidth="2"
                                                                                                                        ChartType="Spline" MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" MarkerSize="2">
                                                                                                                    </DCWC:Series>
                                                                                                                    <DCWC:Series Name="Series3" ShowInLegend="false" Color="Yellow" BorderWidth="2" ChartType="Spline"
                                                                                                                        MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" MarkerSize="2">
                                                                                                                    </DCWC:Series>
                                                                                                                    <DCWC:Series Name="Series4" ShowInLegend="false" Color="Black" BorderWidth="2" ChartType="Spline"
                                                                                                                        MarkerBorderColor="64, 64, 64" MarkerStyle="Circle" MarkerSize="2">
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
                                                                                                                    <DCWC:ChartArea Name="Default" BorderColor="26, 59, 105" BackGradientEndColor="White"
                                                                                                                        BackColor="Gray" BackGradientType="TopBottom" BorderStyle="Solid" ShadowOffset="1">
                                                                                                                        <Position Height="99" Width="94" X="0" Y="2" />
                                                                                                                        <AxisY LabelsAutoFit="False" Title="" Interval="Auto" Minimum="-10" Maximum="10">
                                                                                                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                                                        </AxisY>
                                                                                                                        <AxisX LabelsAutoFit="False" Margin="False" Interval="25" Minimum="0" Maximum="100"
                                                                                                                            Title="">
                                                                                                                            <MajorGrid LineColor="Gray"></MajorGrid>
                                                                                                                            <LabelStyle FontAngle="0" />
                                                                                                                        </AxisX>
                                                                                                                    </DCWC:ChartArea>
                                                                                                                </ChartAreas>
                                                                                                            </DCWC:Chart>
                                                                                                        </td>
                                                                                                        <td style="width: 40%">
                                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                <tr>
                                                                                                                    <td align="left">
                                                                                                                        <table style="width: 95%; border: 1PX SOLID #C1C1C1" cellspacing="0" cellpadding="0"
                                                                                                                            border="0">
                                                                                                                            <tr>
                                                                                                                                <td style="width: 36%; border-right: 1PX SOLID #C1C1C1;" class="BGRIDTITLE" align="center">
                                                                                                                                    Reference %
                                                                                                                                </td>
                                                                                                                                <td style="width: 16%; border-right: 1PX SOLID #C1C1C1;" class="BGRIDTITLE" align="center">
                                                                                                                                    C
                                                                                                                                </td>
                                                                                                                                <td style="width: 16%; border-right: 1PX SOLID #C1C1C1;" class="BGRIDTITLE" align="center">
                                                                                                                                    M
                                                                                                                                </td>
                                                                                                                                <td style="width: 16%; border-right: 1PX SOLID #C1C1C1;" class="BGRIDTITLE" align="center">
                                                                                                                                    Y
                                                                                                                                </td>
                                                                                                                                <td style="width: 16%; border-right: 1PX SOLID #C1C1C1;" class="BGRIDTITLE" align="center">
                                                                                                                                    K
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <rad:RadGrid ID="RGPressRunCurveDeltaPrev" runat="server" AutoGenerateColumns="false"
                                                                                                                            PagerStyle-Mode="NextPrevAndNumeric" ShowHeader="true" AllowPaging="false" AllowSorting="false"
                                                                                                                            AllowFilteringByColumn="false" Width="100%" Skin="Green">
                                                                                                                            <ItemStyle HorizontalAlign="Left" Wrap="false" Width="100%" />
                                                                                                                            <ClientSettings>
                                                                                                                                <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="330" />
                                                                                                                            </ClientSettings>
                                                                                                                            <PagerStyle Position="Top" />
                                                                                                                            <HeaderStyle HorizontalAlign="Center" Width="100%" />
                                                                                                                            <MasterTableView>
                                                                                                                                <HeaderStyle HorizontalAlign="Center" Width="100%" />
                                                                                                                                <Columns>
                                                                                                                                    <rad:GridBoundColumn DataField="ReferenceNo" UniqueName="ReferenceNo" AllowFiltering="false"
                                                                                                                                        Visible="true" HeaderText="Reference %" ItemStyle-Width="36%" HeaderStyle-Width="36%"
                                                                                                                                        ItemStyle-HorizontalAlign="Center">
                                                                                                                                    </rad:GridBoundColumn>
                                                                                                                                    <rad:GridBoundColumn DataField="CMYK_C" UniqueName="CMYK_C" AllowFiltering="false"
                                                                                                                                        ItemStyle-BackColor="#E0F3FF" Visible="true" HeaderText="C" ItemStyle-Width="16%"
                                                                                                                                        HeaderStyle-Width="16%" ItemStyle-HorizontalAlign="Center">
                                                                                                                                    </rad:GridBoundColumn>
                                                                                                                                    <rad:GridBoundColumn DataField="CMYK_M" UniqueName="CMYK_M" AllowFiltering="false"
                                                                                                                                        ItemStyle-BackColor="#FFEFF9" Visible="true" HeaderText="M" ItemStyle-Width="16%"
                                                                                                                                        HeaderStyle-Width="16%" ItemStyle-HorizontalAlign="Center">
                                                                                                                                    </rad:GridBoundColumn>
                                                                                                                                    <rad:GridBoundColumn DataField="CMYK_Y" UniqueName="CMYK_Y" AllowFiltering="false"
                                                                                                                                        ItemStyle-BackColor="#FFFDEE" Visible="true" HeaderText="Y" ItemStyle-Width="16%"
                                                                                                                                        HeaderStyle-Width="16%" ItemStyle-HorizontalAlign="Center">
                                                                                                                                    </rad:GridBoundColumn>
                                                                                                                                    <rad:GridBoundColumn DataField="CMYK_K" UniqueName="CMYK_K" AllowFiltering="false"
                                                                                                                                        ItemStyle-BackColor="#E7EBEE" Visible="true" HeaderText="K" ItemStyle-Width="16%"
                                                                                                                                        HeaderStyle-Width="16%" ItemStyle-HorizontalAlign="Center">
                                                                                                                                    </rad:GridBoundColumn>
                                                                                                                                </Columns>
                                                                                                                            </MasterTableView>
                                                                                                                        </rad:RadGrid>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="2" align="right" style="padding-right: 15px; padding-top: 10px;">
                                                                                                            <asp:Button ID="btnExportProfileDeltaView" runat="server" CssClass="BUTTON" Text="Export Profile"
                                                                                                                OnClick="btnExportProfileDeltaView_Click" />
                                                                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                            <asp:Button ID="BtnExportCurvesDeltaView" runat="server" CssClass="BUTTON" Text="Export Delta"
                                                                                                                OnClick="BtnExportCurvesDeltaView_Click" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </ContentTemplate>
                                                                                            <Triggers>
                                                                                                <asp:PostBackTrigger ControlID="btnExportProfileDeltaView" />
                                                                                                <asp:PostBackTrigger ControlID="BtnExportCurvesDeltaView" />
                                                                                            </Triggers>
                                                                                        </asp:UpdatePanel>
                                                                                    </ContentTemplate>
                                                                                </ajaxtoolkit:TabPanel>
                                                                            </ajaxtoolkit:TabContainer>
                                                                            <script language="javascript" type="text/javascript">
                                                                                function funDeltaRefPrev(obj) {
                                                                                    if (obj == 'Ref') {
                                                                                        document.getElementById("trRef").style.display = "";
                                                                                        document.getElementById("trPrev").style.display = "none";
                                                                                    }
                                                                                    else if (obj == 'Previous') {
                                                                                        document.getElementById("trRef").style.display = "none";
                                                                                        document.getElementById("trPrev").style.display = "";
                                                                                    }
                                                                                }                                                                                        
                                                                            </script>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="padding-right: 15px">
                                                                            <asp:Button ID="BtnClosePrintRun" runat="server" CssClass="BUTTON" Text="Close Print Run"
                                                                                OnClientClick="return GetClientDate();" OnClick="BtnClosePrintRun_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
    <asp:Button runat="server" ID="btnBindPressGrid" Style="display: none" OnClick="btnBindPressGrid_Click" />
    <asp:Button runat="server" ID="btnBindPressRunGrid" Style="display: none" OnClick="btnBindPressRunGrid_Click" />
    <asp:Button runat="server" ID="btnBindPrintTargetCondition" Style="display: none"
        OnClick="btnBindPrintTargetCondition_Click" />
    <asp:Button runat="server" ID="btnBindMeasurement" Style="display: none" OnClick="btnBindMeasurement_Click" />
    <asp:Button runat="server" ID="btnBindAnalyze" Style="display: none" OnClick="btnBindAnalyze_Click" />
    <asp:HiddenField ID="HDTrendDelta" runat="server" />
    <asp:HiddenField ID="HDCurrentMeasurement" runat="server" />
    <asp:HiddenField ID="HDClientDatatime" runat="server" />
    <asp:HiddenField ID="HDCurrentMeasurementName" runat="server" />
    <asp:HiddenField ID="HDPressRunName" runat="server" />
    <asp:Button ID="HDButCurrentMeasurement" runat="server" Text="Button" OnClick="HDButCurrentMeasurement_Click"
        Style="display: none" />
    <ajaxtoolkit:ModalPopupExtender runat="server" ID="programmaticModalPopup" BehaviorID="programmaticModalPopupBehavior"
        Y="225" TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="programmaticPopup"
        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="programmaticPopupDragHandle">
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel runat="server" ID="programmaticPopup" Style="display: none; height: 250px;
        background-color: Transparent; background: ">
        <a id="hideModalPopupViaClientButton" href="#" style="border: none; text-decoration: none;">
            <img src="Images/Close2.png" style="position: absolute; border: none; right: 5px;
                top: 3px; text-align: right;" />
        </a>
        <iframe id="FrmPopup" src="" width="400px" height="250px" frameborder="0" marginwidth="0"
            scrolling="no" marginheight="0" allowtransparency="true" style="background-color: transparent">
        </iframe>
    </asp:Panel>
    <asp:Button runat="server" ID="hiddenTargetControlForGamutModalPopup" Style="display: none" />
    <ajaxtoolkit:ModalPopupExtender runat="server" ID="programmaticGamutModalPopup" BehaviorID="programmaticGamutModalPopupBehavior"
        Y="75" TargetControlID="hiddenTargetControlForGamutModalPopup" PopupControlID="programmaticGamutPopup"
        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="programmaticPopupDragHandle">
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel runat="server" ID="programmaticGamutPopup" Style="display: none; height: 700px;
        background-color: Transparent;">
        <a id="hideGamutModalPopupViaClientButton" href="#" style="border: none; text-decoration: none;">
            <img src="Images/Close2.png" style="position: absolute; border: none; right: 5px;
                top: 3px; text-align: right;" />
        </a>
        <iframe id="FrmChartPopup" src="" frameborder="0" marginwidth="0" scrolling="no"
            marginheight="0" allowtransparency="true" style="background-color: transparent">
        </iframe>
    </asp:Panel>
    <script language="javascript" type="text/javascript">
        function pageLoad() {
            $addHandler($get("hideModalPopupViaClientButton"), 'click', hideModalPopupViaClient);
            $addHandler($get("hideGamutModalPopupViaClientButton"), 'click', hideModalGamutPopupViaClient);
        }
        function hideModalPopupViaClient(ev) {
            ev.preventDefault();
            var modalPopupBehavior = $find('programmaticModalPopupBehavior'); modalPopupBehavior.hide();
            document.getElementById("FrmPopup").src = "";
        }
        function OpenAddPressWindow(URLStr) {
            // if (screen.width <= 1024) { // document.getElementById("<%=programmaticPopup.ClientID%>").style.height = "630px"; 
            // } 
            document.getElementById("FrmPopup").src = URLStr;
            var modalPopupBehavior = $find('programmaticModalPopupBehavior'); modalPopupBehavior.show();
            return false;
        }
        function CallPressBindGrid() {
            document.getElementById("<%= btnBindPressGrid.ClientID%>").click();
        }
        function CallPressRunBindGrid() {
            document.getElementById("<%= btnBindPressRunGrid.ClientID%>").click();
        }
        function CallPrintTargetConditionBind() {
            document.getElementById("<%= btnBindPrintTargetCondition.ClientID%>").click();
        }
        function CallMeasurementBind() {
            document.getElementById("<%= btnBindMeasurement.ClientID%>").click();
        }
        function CallAnalyzetBind() {
            document.getElementById("<%= btnBindAnalyze.ClientID%>").click();
        }
        function SetValueTrendDelta(obj) {
            var obj1 = document.getElementById('<%=HDTrendDelta.ClientID %>');
            obj1.value = obj;
            document.getElementById('<%=HDButTrendDelta.ClientID %>').click();
        }
        function SetCurrentMeasurement(MID) {
            var obj1 = document.getElementById('<%=HDCurrentMeasurement.ClientID %>');
            obj1.value = MID;
            document.getElementById('<%=HDButCurrentMeasurement.ClientID %>').click();
        }

        function hideModalGamutPopupViaClient(ev) {
            ev.preventDefault();
            var modalPopupBehavior = $find('programmaticGamutModalPopupBehavior'); modalPopupBehavior.hide();
            document.getElementById("FrmChartPopup").src = "";
        }
        function OpenGamutWindow(URLStr, widht, height) {
            document.getElementById("FrmChartPopup").src = URLStr;
            document.getElementById("FrmChartPopup").width = widht; //'600px';
            document.getElementById("FrmChartPopup").height = height; //'700px'
            var modalPopupBehavior = $find('programmaticGamutModalPopupBehavior'); modalPopupBehavior.show();
            return false;
        }
        function GetProjectClientDate() {
            var dt = new Date();
            var tzo = dt.toDateString() + ' ' + dt.getHours() + ':' + dt.getMinutes() + ':' + dt.getSeconds();
            document.getElementById('<%= ClientDatatime.ClientID %>').value = tzo.toString();
            return true;
        }
        function GetPressRunClientDate() {
            var dt = new Date();
            var tzo = dt.toDateString() + ' ' + dt.getHours() + ':' + dt.getMinutes() + ':' + dt.getSeconds();
            document.getElementById('<%= PRClientDatatime.ClientID %>').value = tzo.toString();
            return true;
        }
        function GetClientDate() {
            var dt = new Date();
            var tzo = dt.toDateString() + ' ' + dt.getHours() + ':' + dt.getMinutes() + ':' + dt.getSeconds();
            document.getElementById('<%= HDClientDatatime.ClientID %>').value = tzo.toString();
            return true;
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Enter only numeric.");
                return false;
            }
            return true;
        }
        function imposeMaxLength(Object, MaxLen) {
            if (Object.value.length <= MaxLen) {
                return (Object.value.length <= MaxLen);
            }
            return false;
        }
    </script>
</asp:Content>
