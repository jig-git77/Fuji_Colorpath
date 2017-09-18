<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ProjectList.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ProjectList" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<%@ Register Src="~/UserControl/PopupControl/ClosePrintCondition.ascx" TagName="ClosePrintCondition"
    TagPrefix="uc" %>
<%@ Register Src="~/UserControl/PopupControl/OpenPrintCondition.ascx" TagName="OpenPrintCondition"
    TagPrefix="uc" %>
<link href="Content/kendo/2017.2.621/kendo.common.min.css" rel="stylesheet" />
<link href="Content/kendo/2017.2.621/kendo.default.min.css" rel="stylesheet" />
<script src="Scripts/kendo/2017.2.621/kendo.all.min.js"></script>
<script src="JS/ProjectList.js"></script>
<script type="text/javascript" language="javascript">


    function ConfirmDeleteProject() {
        if (confirm('<%= ProjectListDeleteProjectConfirmation %>'))
            return true;
        else
            return false;
    }
</script>
<script type="text/javascript" language="javascript">
    function ConfirmDeleteSpotProject() {
        if (confirm('<%= ProjectListDeleteSpotProjectConfirmation %>'))
            return true;
        else
            return false;
    }
</script>
<script type="text/javascript" language="javascript">
    //function ShowOpenPrintConditionWindow(projectId, projectName, days, DlpName, TotalRuns, isSpotLibraryProject = false, spotProjectID = 0) {
    //    var oWindow;
    //    oWindow = window.radopen("PopupOpenPrintCondition.aspx?projectId=" + projectId + "&projectName=" + projectName + "&days=" + days + "&DlpName=" + DlpName + "&TotalRuns=" + TotalRuns + "&isSpotLibraryProject=" + isSpotLibraryProject + "&spotProjectID=" + spotProjectID, "OpenPrintConditionWindow");
    //    oWindow.show();
    //    return false;
    //}
    function ShowOpenPrintConditionWindow(projectId, projectName, days, DlpName, TotalRuns) {
        var oWindow;
        oWindow = window.radopen("PopupOpenPrintCondition.aspx?projectId=" + projectId + "&projectName=" + projectName + "&days=" + days + "&DlpName=" + DlpName + "&TotalRuns=" + TotalRuns, "OpenPrintConditionWindow");
        oWindow.show();
        return false;
    }
    function ShowClosePrintConditionWindow(projectId, projectName, licenseTypeId, DlpName) {
        var oWindow;
        oWindow = window.radopen("PopupClosePrintCondition.aspx?projectId=" + projectId + "&projectName=" + projectName + "&licenseTypeId=" + licenseTypeId + "&DlpName=" + DlpName, "ClosePrintConditionWindow");
        oWindow.show();
        return false;
    }
</script>
<script language="javascript" type="text/javascript">
    function CloseOpenPrintConditionWindow() {
        var oWnd = $find("<%= OpenPrintConditionWindow.ClientID %>");
        oWnd.close();
    }
    function CloseClosePrintConditionWindow() {
        var oWnd = $find("<%= ClosePrintConditionWindow.ClientID %>");
        oWnd.close();
    }
</script>
<style type="text/css">
    .RadGrid .rgRow td, .RadGrid .rgAltRow td, .RadGrid .rgEditRow td, .RadGrid .rgFooter td, .RadGrid .rgFilterRow td, .RadGrid .rgHeader, .RadGrid .rgResizeCol, .RadGrid .rgGroupHeader td {
        padding-right: 0px !important;
    }

    div.RadGrid .rgRow {
        background: #F7F7F7;
    }

    div.RadGrid .rgAltRow {
        background: #F7F7F7;
    }

    .rgPager table {
        border-top: 0px;
        border-bottom: 1px solid #000000;
        border-spacing: 1pt;
        width: 100%;
    }

    .RadGrid_Default .rgRow a, .RadGrid_Default .rgAltRow a, .RadGrid_Default .rgEditRow a, .RadGrid_Default tr.rgEditRow a, .RadGrid_Default tr.rgHoveredRow a, .RadGrid_Default tr.rgActiveRow a, .RadGrid_Default .rgFooter a, .RadGrid_Default .rgEditForm a {
        color: #003CA9;
    }

    .RadGrid_Default td.rgPagerCell {
        -moz-border-bottom-colors: none;
        -moz-border-image: none;
        -moz-border-left-colors: none;
        -moz-border-right-colors: none;
        -moz-border-top-colors: none;
        border-color: #828282 #EEEEEE #EEEEEE;
        border-style: solid;
        border-width: 0px 0 0px 0px;
    }

    .RadGrid_Default .rgRow td, .RadGrid_Default .rgAltRow td, .RadGrid_Default .rgEditRow td, .RadGrid_Default .rgFooter td {
        border-style: none;
        border-width: 0 0 1px;
    }


    .RadGrid_Default .rgRow .rgSorted {
        background-color: #F7F7F7;
        border-bottom-color: #F7F7F7;
    }

    .RadGrid_Default .rgAltRow .rgSorted {
        background-color: #F7F7F7;
        border-bottom-color: #F7F7F7;
    }

    .RadGrid_Default th.rgSorted {
        background-color: #F1F1F1;
        background-position: 0px;
    }

    .breakWord80 {
        max-width: 80px !important;
        word-break: break-all !important;
        word-wrap: break-word !important;
        vertical-align: top;
        line-height: 15px;
    }

    .breakWord60 {
        max-width: 60px !important;
        word-break: break-all !important;
        word-wrap: break-word !important;
        vertical-align: top;
        line-height: 15px;
    }
</style>

<style type="text/css">
    .InnerHeaderStyle {
        background: #C0C0C0 !important;
        /*font-size: 13px !important;*/
        /*color: black !important;*/ /*add more style definitions here*/
    }

    .InnerItemStyle {
        background: #C0C0C0 !important;
        /*color: brown !important;*/ /*add more style definitions here*/
    }

    .InnerAlernatingItemStyle {
        background: #C0C0C0 !important;
        /*color: brown !important;*/ /*add more style definitions here*/
    }

    .CommandItemStyle {
        background: #C0C0C0 !important;
        /*color: brown !important;*/ /*add more style definitions here*/
    }

    .FilterItemStyle {
        background: #C0C0C0 !important;
        /*color: brown !important;*/ /*add more style definitions here*/
    }

    .PagerItemStyle {
        background: #C0C0C0 !important;
        /*color: brown !important;*/ /*add more style definitions here*/
    }

    .k-grid .k-hierarchy-col,
    .k-grid .k-hierarchy-cell {
        width: 10px;
        overflow: hidden;
    }

    #kendoProjectList >  .k-grid-content {
        min-height: 100px;        
    }
</style>

<div class="contant_top">
    <asp:HiddenField runat="server" ID="hdnBaseServiceUrl" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdfHasDeletePermission" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdfCurrentUser" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdfCurrentDeviceID" ClientIDMode="Static" />
    &nbsp;
</div>
<div class="contant_middle">
    <div class="head">
        <h1>
            <asp:Label ID="lblTitle" runat="server" Text="Project List" meta:resourcekey="lblTitleResource1"></asp:Label>
        </h1>
        <asp:LinkButton ID="lbtnCreateNewPrintCondition" runat="server" CssClass="button"
            ValidationGroup="vgDestination"
            OnClick="lbtnCreateNewPrintCondition_Click">
            <span>
                <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/new_project_icon.png" meta:resourcekey="imgAddResource1" />
                <asp:Localize runat="server" Text="Create New Print Condition" ID="locCreateNewPrintCondition"
                    meta:resourcekey="locCreateNewPrintConditionResource1"></asp:Localize>
            </span>
        </asp:LinkButton>
    </div>
    <div class="clear">
    </div>
    <div class="listDevice" style="margin-left: 15px;">
        <asp:UpdatePanel ID="testPanel" runat="server">
            <ContentTemplate>
                <div>
                    <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
                </div>
                <div class="clear">
                </div>
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr id="trListHeader" runat="server">
                        <td runat="server">
                            <telerik:RadGrid ID="rgProjectList" AllowFilteringByColumn="True" AllowPaging="True"
                                PageSize="5" runat="server" AutoGenerateColumns="False" EnableLinqExpressions="False"
                                OnNeedDataSource="rgProjectList_NeedDataSource" OnItemCommand="rgProjectList_ItemCommand" OnItemCreated="rgProjectList_ItemCreated"
                                OnItemDataBound="rgProjectList_ItemDataBound" OnDetailTableDataBind="rgProjectList_DetailTableDataBind" Width="100%" CellSpacing="0" GridLines="None"
                                AllowSorting="True">
                                <GroupingSettings CaseSensitive="False" />
                                <HeaderStyle Font-Bold="True" />
                                <PagerStyle Position="Top" AlwaysVisible="True" Font-Size="Small" />
                                <MasterTableView Name="ParentProjects" DataKeyNames="ProjectId">
                                    <DetailTables>
                                        <telerik:GridTableView DataKeyNames="SpotProjectID" Name="SpotProjects" Width="100%" AutoGenerateColumns="false" CommandItemDisplay="Top">

                                            <HeaderStyle CssClass="InnerHeaderStyle" />
                                            <ItemStyle CssClass="InnerItemStyle" />
                                            <AlternatingItemStyle CssClass="InnerAlernatingItemStyle" />
                                            <CommandItemStyle CssClass="CommandItemStyle" />
                                            <FilterItemStyle CssClass="FilterItemStyle" />
                                            <PagerStyle Position="Top" AlwaysVisible="True" Font-Size="Small" CssClass="PagerItemStyle" />

                                            <CommandItemSettings ShowAddNewRecordButton="false" />
                                            <CommandItemTemplate>
                                                <asp:Button ID="btnAddSpotLibrary" runat="server" Text="Create New Library" CommandName="AddSpotLibrary" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ProjectID") %>' />
                                            </CommandItemTemplate>
                                            <Columns>
                                                <telerik:GridTemplateColumn DataField="LastOptmizeId" UniqueName="LastOptmizeId"
                                                    ItemStyle-Width="30px" meta:resourcekey="GridTemplateColumnResource30">
                                                    <ItemTemplate>
                                                        <%--<asp:ImageButton ID="imgbtnSpotSelect" runat="server" ToolTip="Open" AlternateText="Select"
                                                                    ImageUrl="../Images/open_folder_yellow.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"LastOptmizeId") %>'
                                                                    CommandName="SelectSpotTargetProject" meta:resourcekey="imgbtnSelectSpotResource1"  Width="20" Height="20"/>--%>
                                                        <asp:ImageButton ID="imgbtnSpotSelect" runat="server" ToolTip="Open" AlternateText="Select"
                                                            ImageUrl="../Images/open_folder_yellow.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"SpotProjectID") %>'
                                                            CommandName="SelectSpotTargetProject" meta:resourcekey="imgbtnSelectSpotResource1" Width="20" Height="20" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn SortExpression="ProjectName" HeaderText="Name" HeaderButtonType="TextButton"
                                                    DataField="ProjectName">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn SortExpression="SpotTarget.SpotTargetName" HeaderText="Library" HeaderButtonType="TextButton"
                                                    DataField="SpotTarget.SpotTargetName">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn SortExpression="CreateDate" HeaderText="Last Active" HeaderButtonType="TextButton"
                                                    DataField="CreateDate" DataFormatString="{0:MM/dd/yy hh:mm}" ShowFilterIcon="False">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="SpotProjectID" UniqueName="Delete" FilterControlAltText="Filter Delete column"
                                                    meta:resourcekey="GridTemplateColumnResource7">
                                                    <FilterTemplate>
                                                        <asp:LinkButton ID="lbtnClearFillter" runat="server" CssClass="button" CommandName="ClearFilterSpotTarget"
                                                            ToolTip="Clear Filter">
                                                            <span>
                                                                <asp:Image ID="imgClearFillter" runat="server" AlternateText="Clear Fillter" ImageUrl="../Images/clear-fillter.png"
                                                                    meta:resourcekey="imgClearFillterResource1" />
                                                            </span>
                                                        </asp:LinkButton>
                                                    </FilterTemplate>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete SpotProject" AlternateText="Delete"
                                                            ImageUrl="../Images/delete_icon.png" OnClientClick="return ConfirmDeleteSpotProject();"
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem,"SpotProjectID") %>' CommandName="DeleteSpotProject"
                                                            meta:resourcekey="imgbtnDeleteResource1" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="40px" />
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridTemplateColumn DataField="LicenseType" AllowFiltering="True" UniqueName="LicenseType" FilterControlAltText="Filter Select column" HeaderText="Type"
                                            ItemStyle-Width="30px" meta:resourcekey="GridTemplateColumnResource0">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnSelect" runat="server" ToolTip="Optimization" AlternateText="Select"
                                                    ImageUrl="../Images/OptimizationBlue.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ProjectId") %>'
                                                    CommandName="Select" meta:resourcekey="imgbtnSelectResource1" />
                                            </ItemTemplate>
                                            <FilterTemplate>
                                                <telerik:RadComboBox ID="rcmbLicenseType" AppendDataBoundItems="True" runat="server"
                                                    SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("LicenseType").CurrentFilterValue %>'
                                                    OnClientSelectedIndexChanged="LicenseTypeIndexChanged" Width="40px" DropDownWidth="150px">
                                                </telerik:RadComboBox>
                                                <telerik:RadScriptBlock ID="radscriptLicenseType" runat="server">
                                                    <script type="text/javascript">
                                                        function LicenseTypeIndexChanged(sender, args) {
                                                            var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                            tableView.filter("LicenseType", args.get_item().get_value(), "EqualTo");
                                                        }
                                                    </script>
                                                </telerik:RadScriptBlock>
                                            </FilterTemplate>
                                            <HeaderStyle Width="30px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="ValidationStatus" AllowFiltering="True" UniqueName="ValidationStatus" HeaderText="Validation"
                                            FilterControlAltText="Filter ValidationStatus column" meta:resourcekey="GridTemplateColumnResource1">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnVarification" runat="server" ToolTip="Validation" AlternateText="Validation"
                                                    ImageUrl="../Images/next.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ProjectId") %>'
                                                    CommandName="Validation" />
                                            </ItemTemplate>
                                            <FilterTemplate>
                                                <telerik:RadComboBox ID="rcmbValidationStatus" AppendDataBoundItems="True" runat="server"
                                                    SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("ValidationStatus").CurrentFilterValue %>'
                                                    OnClientSelectedIndexChanged="ValidationStatusIndexChanged" Width="40px" DropDownWidth="120px">
                                                </telerik:RadComboBox>
                                                <telerik:RadScriptBlock ID="radscriptValidationStatus" runat="server">
                                                    <script type="text/javascript">
                                                        function ValidationStatusIndexChanged(sender, args) {
                                                            var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                            tableView.filter("ValidationStatus", args.get_item().get_value(), "EqualTo");
                                                        }
                                                    </script>
                                                </telerik:RadScriptBlock>
                                            </FilterTemplate>
                                            <HeaderStyle Width="30px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="Status" FilterControlWidth="30px" HeaderText="Status" Visible="false"
                                            DataField="Status" FilterControlAltText="Filter Status column" meta:resourcekey="GridTemplateColumnResource2">
                                            <ItemTemplate>
                                                <asp:Image ID="imgStatus" runat="server" meta:resourcekey="imgStatusResource1" />
                                            </ItemTemplate>
                                            <FilterTemplate>
                                                <telerik:RadComboBox ID="RadComboBoxStatus" AppendDataBoundItems="True" runat="server"
                                                    OnClientSelectedIndexChanged="StatusIndexChanged" Width="40px" DropDownWidth="70px"
                                                    SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("Status").CurrentFilterValue %>'
                                                    meta:resourcekey="RadComboBoxStatusResource1">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="ALL" runat="server" meta:resourcekey="RadComboBoxItemResource1"
                                                            Owner="" Selected="True" />
                                                        <telerik:RadComboBoxItem Text="Open" Value="Open" runat="server" meta:resourcekey="RadComboBoxItemResource2"
                                                            Owner="" />
                                                        <telerik:RadComboBoxItem Text="Closed" Value="Closed" runat="server" meta:resourcekey="RadComboBoxItemResource3"
                                                            Owner="" />
                                                        <telerik:RadComboBoxItem Text="Certify" Value="Certify" runat="server" meta:resourcekey="RadComboBoxItemResource4"
                                                            Owner="" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
                                                    <script type="text/javascript">
                                                        function StatusIndexChanged(sender, args) {
                                                            var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                            tableView.filter("Status", args.get_item().get_value(), "EqualTo");
                                                        }
                                                    </script>
                                                </telerik:RadScriptBlock>
                                            </FilterTemplate>
                                            <HeaderStyle Width="50px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="ProjectName" HeaderText="Name" UniqueName="ProjectName"
                                            SortExpression="ProjectName" FilterControlWidth="90px" AutoPostBackOnFilter="True"
                                            CurrentFilterFunction="Contains" FilterControlAltText="Filter ProjectName column"
                                            meta:resourcekey="GridTemplateColumnResource3">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjectName" runat="server" Text='<%# Eval("ProjectName") %>' meta:resourcekey="lblProjectNameResource1" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="breakWord80" Width="80px" Wrap="True" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="SourceProfile" DataField="SourceProfile.ProfileName"
                                            HeaderText="Standard" FilterControlWidth="80px" SortExpression="SourceProfile.ProfileName"
                                            AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" FilterControlAltText="Filter SourceProfile column"
                                            meta:resourcekey="GridTemplateColumnResource4">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStandard" runat="server" Text='<%# Eval("SourceProfile.ProfileName") %>'
                                                    meta:resourcekey="lblStandardResource1" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="breakWord80" Width="80px" Wrap="True" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="PrintCondition.Media" HeaderText="Media" UniqueName="Media"
                                            SortExpression="PrintCondition.Media" FilterControlWidth="60px" AutoPostBackOnFilter="True"
                                            CurrentFilterFunction="Contains" FilterControlAltText="Filter Media column" meta:resourcekey="GridTemplateColumnResource5">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMedia" runat="server" Text='<%# Eval("PrintCondition.Media") %>'
                                                    meta:resourcekey="lblMediaResource1" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="breakWord60" Width="60px" Wrap="True" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="PrintCondition.Colorant" HeaderText="Colorant"
                                            UniqueName="Colorant" SortExpression="PrintCondition.Colorant" FilterControlWidth="60px"
                                            AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" FilterControlAltText="Filter Colorant column"
                                            meta:resourcekey="GridTemplateColumnResource6">
                                            <ItemTemplate>
                                                <asp:Label ID="lblColorant" runat="server" Text='<%# Eval("PrintCondition.Colorant") %>'
                                                    meta:resourcekey="lblColorantResource1" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="breakWord60" Width="60px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridDateTimeColumn DataField="CreateDate" HeaderText="Last Active" SortExpression="CreateDate"
                                            UniqueName="CreateDate" DataFormatString="{0:MM/dd/yy hh:mm}" ShowFilterIcon="False"
                                            FilterControlWidth="100px" AutoPostBackOnFilter="True" CurrentFilterFunction="GreaterThanOrEqualTo"
                                            FilterControlAltText="Filter CreateDate column" meta:resourcekey="GridDateTimeColumnResource1">
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridTemplateColumn DataField="ProjectId" UniqueName="Delete" FilterControlAltText="Filter Delete column"
                                            meta:resourcekey="GridTemplateColumnResource7">
                                            <FilterTemplate>
                                                <asp:LinkButton ID="lbtnClearFillter" runat="server" CssClass="button" CommandName="ClearFilter"
                                                    ToolTip="Clear Filter">
                                                    <span>
                                                        <asp:Image ID="imgClearFillter" runat="server" AlternateText="Clear Fillter" ImageUrl="../Images/clear-fillter.png"
                                                            meta:resourcekey="imgClearFillterResource1" />
                                                    </span>
                                                </asp:LinkButton>
                                            </FilterTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete Project" AlternateText="Delete"
                                                    ImageUrl="../Images/delete_icon.png" OnClientClick="return ConfirmDeleteProject();"
                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ProjectId") %>' CommandName="Delete"
                                                    meta:resourcekey="imgbtnDeleteResource1" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="40px" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <asp:HiddenField ID="hdSelectedSourceProfileId" runat="server" Value="0" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="clear">
    </div>
    <div class="listDevice">
        <br />
        <div>
            <div id="kendoProjectList"></div>
        </div>
    </div>
    <div class="clear">
    </div>
    <telerik:RadWindowManager ID="RadWindowManager" runat="server" Modal="True">
        <Windows>
            <telerik:RadWindow ID="OpenPrintConditionWindow" runat="server" IconUrl="" VisibleStatusbar="false"
                ShowContentDuringLoad="false" Title="Open Print Condition" Width="720" Height="220"
                Behaviors="Move,Close">
            </telerik:RadWindow>
            <telerik:RadWindow ID="ClosePrintConditionWindow" runat="server" VisibleStatusbar="false"
                ShowContentDuringLoad="false" Title="Close Print Condition" Width="720" Height="200"
                Behaviors="Move,Close">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <input id="Hidden_TotalProjects" type="hidden" runat="server" value="0" />
</div>
<div class="contant_bottom">
    &nbsp;
</div>
