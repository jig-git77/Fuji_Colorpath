<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectList.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ProjectList" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<%@ Register Src="~/UserControl/PopupControl/ClosePrintCondition.ascx" TagName="ClosePrintCondition"
    TagPrefix="uc" %>
<%@ Register Src="~/UserControl/PopupControl/OpenPrintCondition.ascx" TagName="OpenPrintCondition"
    TagPrefix="uc" %>
<script type="text/javascript" language="javascript">
    function ConfirmDeleteProject() {
        if (confirm('<%= ProjectList_DeleteProject_Confirmation %>'))
            return true;
        else
            return false;
    }
</script>
<script type="text/javascript" language="javascript">
    function ShowOpenPrintConditionWindow(projectName) {
        var msg = 'Current ' + projectName + ' is open and has been inactive for 2.5 days.</br> ' +
                   ' Confirm which DLP is used in your workflow. </br> In order to continue optimizing ' +
                   ' this DLP your workflow must be using ' + projectName + '.';
        jQuery("span[id$='lblOpenPrintConditionMessage']").html(msg);
        var oWnd = $find("<%= OpenPrintConditionWindow.ClientID %>");
        oWnd.show();
        return false;
    }
    function ShowClosePrintConditionWindow(projectName) {
        var msg = 'Options for ' + projectName + ' Print Condition';
        jQuery("span[id$='lblClosePrintConditionMessage']").html(msg);
        var oWnd = $find("<%= ClosePrintConditionWindow.ClientID %>");
        oWnd.show();
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
    div.RadGrid .rgRow
    {
        background: #F7F7F7;
    }
    div.RadGrid .rgAltRow
    {
        background: #F7F7F7;
    }
    .rgPager table
    {
        border-top: 0px;
        border-bottom: 1px solid #000000;
        border-spacing: 1pt;
        width: 100%;
    }
    .RadGrid_Default .rgRow a, .RadGrid_Default .rgAltRow a, .RadGrid_Default .rgEditRow a, .RadGrid_Default tr.rgEditRow a, .RadGrid_Default tr.rgHoveredRow a, .RadGrid_Default tr.rgActiveRow a, .RadGrid_Default .rgFooter a, .RadGrid_Default .rgEditForm a
    {
        color: #003CA9;
    }
    .RadGrid_Default td.rgPagerCell
    {
        -moz-border-bottom-colors: none;
        -moz-border-image: none;
        -moz-border-left-colors: none;
        -moz-border-right-colors: none;
        -moz-border-top-colors: none;
        border-color: #828282 #EEEEEE #EEEEEE;
        border-style: solid;
        border-width: 0px 0 0px 0px;
    }
    .RadGrid_Default .rgRow td, .RadGrid_Default .rgAltRow td, .RadGrid_Default .rgEditRow td, .RadGrid_Default .rgFooter td
    {
        border-style: none;
        border-width: 0 0 1px;
    }
</style>
<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <h1>
            DLP Optimization</h1>
        <asp:LinkButton ID="lbtnCreateNewPrintCondition" runat="server" CssClass="button"
            ValidationGroup="vgDestination" OnClick="lbtnCreateNewPrintCondition_Click">
            <span>
                <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/new_project_icon.png" />
                Create New Print Condition</span></asp:LinkButton>
    </div>
    <div class="clear">
    </div>
    <div class="formDeviceNoLine" style="display: none;">
        <ucStatusbar:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
        <%-- <div style="float: left">
                                    <label>
                                        Select DLP Optimized Print Condition</label>
                                </div>
         <div style="float: right">
                                    <asp:LinkButton ID="lbtnCreateNewPrintCondition" runat="server" ValidationGroup="vgDestination"
                                        CssClass="gray_bt" onclick="lbtnCreateNewPrintCondition_Click" >
                                        <span>
                                            Create New Print Condition </span>
                                    </asp:LinkButton>
                                </div>--%>
        <div class="clear">
        </div>
    </div>
    <div class="listDevice" style="margin-left: 15px;">
        <table cellpadding="0" cellspacing="0" border="0">
            <tr id="trListHeader" runat="server">
                <td>
                    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                        <AjaxSettings>
                            <telerik:AjaxSetting AjaxControlID="rgProjectList">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="rgProjectList" LoadingPanelID="RadAjaxLoadingPanel1" />
                                </UpdatedControls>
                            </telerik:AjaxSetting>
                            <telerik:AjaxSetting AjaxControlID="rgRadWindowManager">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="RadWindowManager" LoadingPanelID="RadAjaxLoadingPanel1" />
                                </UpdatedControls>
                            </telerik:AjaxSetting>
                        </AjaxSettings>
                    </telerik:RadAjaxManager>
                    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
                    <telerik:RadGrid ID="rgProjectList" ShowHeader="true" AllowFilteringByColumn="true"
                        AllowSorting="false" AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False"
                        EnableLinqExpressions="false" OnNeedDataSource="rgProjectList_NeedDataSource"
                        OnItemCommand="rgProjectList_ItemCommand" OnItemDataBound="rgProjectList_ItemDataBound"
                        Width="100%">
                        <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />
                        <MasterTableView DataKeyNames="ProjectId">
                            <Columns>
                                <telerik:GridTemplateColumn DataField="ProjectId" AllowFiltering="false" HeaderText=""
                                    UniqueName="Select" SortExpression="ProjectId" FilterControlWidth="50">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnSelect" runat="server" ToolTip="Select Project" AlternateText="Select"
                                            ImageUrl="../Images/next.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ProjectId") %>'
                                            CommandName="Select" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="ProjectStatusId" AllowFiltering="true" HeaderText="Status"
                                    DataField="ProjectStatusId" SortExpression="ProjectStatusId">
                                    <ItemTemplate>
                                        <asp:Image ID="imgStatus" runat="server" />
                                    </ItemTemplate>
                                    <FilterTemplate>
                                        <telerik:RadComboBox ID="RadComboBoxStatus" AppendDataBoundItems="true" runat="server"
                                            OnClientSelectedIndexChanged="StatusIndexChanged" AutoPostBack="false" Width="50"
                                            DropDownWidth="70" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("ProjectStatusId").CurrentFilterValue %>'>
                                            <Items>
                                                <telerik:RadComboBoxItem Text="ALL" />
                                                <telerik:RadComboBoxItem Text="Open" Value="1" />
                                                <telerik:RadComboBoxItem Text="Closed" Value="2" />
                                                <telerik:RadComboBoxItem Text="Certify" Value="3" />
                                                <telerik:RadComboBoxItem Text="NotUsed" Value="4" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
                                            <script type="text/javascript">
                                                function StatusIndexChanged(sender, args) {
                                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                    tableView.filter("ProjectStatusId", args.get_item().get_value(), "EqualTo");
                                                }
                                            </script>
                                        </telerik:RadScriptBlock>
                                    </FilterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="ProjectName" AllowFiltering="true" HeaderText="Project Name"
                                    UniqueName="ProjectName" SortExpression="ProjectName" FilterControlWidth="100"
                                    ItemStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProjectName" runat="server" Text='<%# Eval("ProjectName") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="PrintStandardTypeId" DataField="PrintStandardTypeId"
                                    AllowFiltering="true" HeaderText="Standard" SortExpression="PrintStandardTypeId">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStandard" runat="server" Text="" />
                                    </ItemTemplate>
                                    <FilterTemplate>
                                        <telerik:RadComboBox ID="RadComboBoxPrintStandard" AppendDataBoundItems="true" runat="server"
                                            OnClientSelectedIndexChanged="PrintStandardIndexChanged" AutoPostBack="false"
                                            Width="70" DropDownWidth="100" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("PrintStandardTypeId").CurrentFilterValue %>'>
                                            <Items>
                                                <telerik:RadComboBoxItem Text="ALL" />
                                                <telerik:RadComboBoxItem Text="GRACoL" Value="1" />
                                                <telerik:RadComboBoxItem Text="SWOP_5" Value="2" />
                                                <telerik:RadComboBoxItem Text="SWOP_3" Value="3" />
                                                <telerik:RadComboBoxItem Text="PSO_12647_2" Value="4" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
                                            <script type="text/javascript">
                                                function PrintStandardIndexChanged(sender, args) {
                                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                    tableView.filter("PrintStandardTypeId", args.get_item().get_value(), "EqualTo");
                                                }
                                            </script>
                                        </telerik:RadScriptBlock>
                                    </FilterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="Media" AllowFiltering="true" HeaderText="Media"
                                    UniqueName="Media" SortExpression="Media" FilterControlWidth="50">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMedia" runat="server" Text='<%# Eval("Media") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="Colorant" AllowFiltering="true" HeaderText="Colorant"
                                    UniqueName="Colorant" SortExpression="Colorant" FilterControlWidth="50">
                                    <ItemTemplate>
                                        <asp:Label ID="lblColorant" runat="server" Text='<%# Eval("Colorant") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="CreatedDate" DataField="CreatedDate" 
                                HeaderText="Last Active"
                                    DataFormatString="{0:MM/dd/yy hh:mm}" HeaderStyle-Width="100px">
                                    <FilterTemplate>
                                        <telerik:RadDatePicker ID="LastActivePicker" runat="server" Width="100px" ClientEvents-OnDateSelected="DateSelected"
                                            DbSelectedDate='<%# CreatedDateDate(Container) %>' />
                                        <telerik:RadScriptBlock ID="RadScriptBlock3" runat="server">
                                            <script type="text/javascript">
                                                function DateSelected(sender, args) {
                                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                    var date = FormatSelectedDate(sender);
                                                    if (date != '') {
                                                        var nextDate = FormatNextDate(sender);
                                                        tableView.filter("CreatedDate", date + " " + nextDate, "Between");
                                                    }
                                                    else
                                                        tableView.filter("CreatedDate", date, "EqualTo");
                                                }
                                                function FormatSelectedDate(picker) {
                                                    var date = picker.get_selectedDate();
                                                    var dateInput = picker.get_dateInput();
                                                    var formattedDate = dateInput.get_dateFormatInfo().FormatDate(date, dateInput.get_displayDateFormat());
                                                    return formattedDate;
                                                }
                                                function FormatNextDate(picker) {
                                                    var date = picker.get_selectedDate();
                                                    var nextDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() + 1);
                                                    var dateInput = picker.get_dateInput();
                                                    var formattedDate = dateInput.get_dateFormatInfo().FormatDate(nextDate, dateInput.get_displayDateFormat());
                                                    return formattedDate;
                                                }
                                               
                                            </script>
                                        </telerik:RadScriptBlock>
                                    </FilterTemplate>
                                </telerik:GridBoundColumn>
                                <%--<telerik:GridDateTimeColumn 
                                    FilterControlWidth="100px" 
                                    DataField="CreatedDate" 
                                    HeaderText="Last Active"
                                    SortExpression="CreatedDate" 
                                    UniqueName="CreatedDate" 
                                    PickerType="DatePicker" 
                                    DataFormatString="{0:MM/dd/yy hh:mm}">
                                    <HeaderStyle Width="100px" />
                                </telerik:GridDateTimeColumn>
--%>

                                <telerik:GridTemplateColumn DataField="ProjectId" AllowFiltering="false" HeaderText=""
                                    UniqueName="Delete">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete Project" AlternateText="Delete"
                                            ImageUrl="../Images/delete_icon.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ProjectId") %>'
                                            CommandName="Delete" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
    <div class="clear">
    </div>
    <telerik:RadWindowManager ID="RadWindowManager" runat="server" Modal="True">
        <Windows>
            <telerik:RadWindow ID="OpenPrintConditionWindow" runat="server" ShowContentDuringLoad="false"
                Title="Open Print Condition" Width="650" Height="300" Behaviors="Close, Move"
                Behavior="Close, Move">
                <ContentTemplate>
                    <uc:OpenPrintCondition ID="ucOpenPrintCondition" runat="server" />
                </ContentTemplate>
            </telerik:RadWindow>
            <telerik:RadWindow ID="ClosePrintConditionWindow" runat="server" ShowContentDuringLoad="false"
                Title="Close Print Condition" Width="650" Height="270" Behaviors="Close, Move"
                Behavior="Close, Move">
                <ContentTemplate>
                    <uc:ClosePrintCondition ID="ucClosePrintCondition" runat="server" />
                </ContentTemplate>
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</div>
<div class="contant_bottom">
    &nbsp;</div>
