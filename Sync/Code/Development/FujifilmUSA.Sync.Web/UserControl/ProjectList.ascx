<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectList.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ProjectList" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<%@ Register Src="~/UserControl/PopupControl/ClosePrintCondition.ascx" TagName="ClosePrintCondition"
    TagPrefix="uc" %>
<%@ Register Src="~/UserControl/PopupControl/OpenPrintCondition.ascx" TagName="OpenPrintCondition"
    TagPrefix="uc" %>
<script type="text/javascript" language="javascript">
    function ConfirmDeleteProject() {
        if (confirm('<%= ProjectListDeleteProjectConfirmation %>'))
            return true;
        else
            return false;
    }
</script>
<script type="text/javascript" language="javascript">
    function ShowOpenPrintConditionWindow(projectId, projectName, days, DlpName, TotalRuns) {
        var oWindow;
        oWindow = window.radopen("PopupOpenPrintCondition.aspx?projectId=" + projectId + "&projectName=" + projectName + "&days=" + days + "&DlpName=" + DlpName + "&TotalRuns=" + TotalRuns, "OpenPrintConditionWindow");
        oWindow.show();
        return false;
    }
    function ShowClosePrintConditionWindow(projectId, projectName) {
        var oWindow;
        oWindow = window.radopen("PopupClosePrintCondition.aspx?projectId=" + projectId + "&projectName=" + projectName, "ClosePrintConditionWindow");
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
    
    
    .RadGrid_Default .rgRow .rgSorted
    {
        background-color: #F7F7F7;
        border-bottom-color: #F7F7F7;
    }
    .RadGrid_Default .rgAltRow .rgSorted
    {
        background-color: #F7F7F7;
        border-bottom-color: #F7F7F7;
    }
    
    .RadGrid_Default th.rgSorted
    {
        background-color: #F1F1F1;
        background-position: 0px;
    }
    
     .breakWord80
     {
            max-width: 80px !important;
            word-break: break-all !important;
            word-wrap: break-word !important;
            vertical-align: top;
            line-height: 15px;
     }
     
     .breakWord60
     {
            max-width: 60px !important;
            word-break: break-all !important;
            word-wrap: break-word !important;
            vertical-align: top;
            line-height: 15px;
     }
     
     
     

    
</style>
<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <h1>
            <asp:Label ID="lblTitle" runat="server" Text="Project List"></asp:Label>
            </h1>
        <asp:LinkButton ID="lbtnCreateNewPrintCondition" runat="server" CssClass="button"
            ValidationGroup="vgDestination" OnClick="lbtnCreateNewPrintCondition_Click">
            <span>
                <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/new_project_icon.png" />
                Create New Print Condition</span></asp:LinkButton>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 15px;">
        <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="false" />
    </div>
    <div class="clear">
    </div>
    <div class="listDevice" style="margin-left: 15px;">
        <asp:UpdatePanel ID="testPanel" runat="server">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" border="0">                   
                    <tr id="trListHeader" runat="server">
                        <td>
                            <telerik:RadGrid ID="rgProjectList" AllowFilteringByColumn="True" AllowPaging="True"
                                PageSize="5" runat="server" AutoGenerateColumns="False" EnableLinqExpressions="False"
                                OnNeedDataSource="rgProjectList_NeedDataSource" OnItemCommand="rgProjectList_ItemCommand"
                                OnItemDataBound="rgProjectList_ItemDataBound" Width="100%" CellSpacing="0" GridLines="None"
                                AllowSorting="true">
                                <HeaderStyle Font-Bold="true" />
                                <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />
                                <MasterTableView DataKeyNames="ProjectId">
                                    <Columns>
                                        <telerik:GridTemplateColumn DataField="ProjectId" AllowFiltering="false" HeaderText=""
                                            UniqueName="Select" SortExpression="ProjectId" HeaderStyle-Width="30">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnSelect" runat="server" ToolTip="Select Project" AlternateText="Select"
                                                    ImageUrl="../Images/next.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ProjectId") %>'
                                                    CommandName="Select" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="Status" HeaderStyle-Width="50" FilterControlWidth="50"
                                            AllowFiltering="true" HeaderText="Status" DataField="Status">
                                            <ItemTemplate>
                                                <asp:Image ID="imgStatus" runat="server" />
                                            </ItemTemplate>
                                            <FilterTemplate>
                                                <telerik:RadComboBox ID="RadComboBoxStatus" AppendDataBoundItems="true" runat="server"
                                                    OnClientSelectedIndexChanged="StatusIndexChanged" AutoPostBack="false" Width="50"
                                                    DropDownWidth="70" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("Status").CurrentFilterValue %>'>
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="ALL" />
                                                        <telerik:RadComboBoxItem Text="Open" Value="Open" />
                                                        <telerik:RadComboBoxItem Text="Closed" Value="Closed" />                                                      
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
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="ProjectName" AllowFiltering="true" HeaderText="Name"
                                            UniqueName="ProjectName" SortExpression="ProjectName" 
                                            ShowFilterIcon="true"
                                            FilterControlWidth="80" 
                                            ItemStyle-CssClass="breakWord80" ItemStyle-Width="80" ItemStyle-Wrap="true"
                                            >
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjectName" runat="server" Text='<%# Eval("ProjectName") %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="SourceProfile" DataField="SourceProfile.ProfileName"
                                            AllowFiltering="true" HeaderText="Standard" 
                                            FilterControlWidth="80"
                                            ItemStyle-CssClass="breakWord80" ItemStyle-Width="80" ItemStyle-Wrap="true"
                                            SortExpression="SourceProfile.ProfileName" ShowFilterIcon="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStandard" runat="server" Text='<%# Eval("SourceProfile.ProfileName") %>' />
                                            </ItemTemplate>
                                            <%--  <FilterTemplate>
                                                <telerik:RadComboBox ID="RadComboBoxPrintStandard" AppendDataBoundItems="true" runat="server"
                                                    OnClientSelectedIndexChanged="PrintStandardIndexChanged" AutoPostBack="false"
                                                    Width="100" DropDownWidth="150" OnPreRender="RadComboBoxPrintStandard_PreRender"
                                                    OnSelectedIndexChanged="RadComboBoxPrintStandard_SelectedIndexChanged">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="All" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
                                                    <script type="text/javascript">
                                                        function PrintStandardIndexChanged(sender, args) {
                                                            var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                            tableView.filter("SourceProfileId", args.get_item().get_value(), "EqualTo");
                                                        }
                                                    </script>
                                                </telerik:RadScriptBlock>
                                            </FilterTemplate>--%>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="PrintCondition.Media" AllowFiltering="true"
                                            HeaderText="Media" UniqueName="Media" SortExpression="PrintCondition.Media" 
                                            ShowFilterIcon="true"
                                            FilterControlWidth="60"
                                            ItemStyle-CssClass="breakWord60" 
                                            ItemStyle-Width="60" 
                                            ItemStyle-Wrap="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMedia" runat="server" Text='<%# Eval("PrintCondition.Media") %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn 
                                            DataField="PrintCondition.Colorant" AllowFiltering="true"
                                            HeaderText="Colorant" UniqueName="Colorant" SortExpression="PrintCondition.Colorant"
                                            ShowFilterIcon="true"
                                            FilterControlWidth="60"
                                            ItemStyle-CssClass="breakWord60" 
                                            ItemStyle-Width="60"
                                            >
                                            <ItemTemplate>
                                                <asp:Label ID="lblColorant" runat="server" Text='<%# Eval("PrintCondition.Colorant") %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn UniqueName="CreateDate" DataField="CreateDate" HeaderText="Last Active"
                                            DataFormatString="{0:MM/dd/yy hh:mm}" HeaderStyle-Width="100">
                                            <FilterTemplate>
                                                <telerik:RadDatePicker ID="LastActivePicker" runat="server" Width="100px" ClientEvents-OnDateSelected="DateSelected"
                                                    DbSelectedDate='<%# FillterCreatedDateDate(Container) %>' />
                                                <telerik:RadScriptBlock ID="RadScriptBlock3" runat="server">
                                                    <script type="text/javascript">
                                                        function DateSelected(sender, args) {
                                                            var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                            var date = FormatSelectedDate(sender);
                                                            if (date != '') {
                                                                var nextDate = FormatNextDate(sender);
                                                                tableView.filter("CreateDate", date + " " + nextDate, "Between");
                                                            }
                                                            else
                                                                tableView.filter("CreateDate", date, "EqualTo");
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
                                        <telerik:GridTemplateColumn DataField="ProjectId" AllowFiltering="true" HeaderText=""
                                            HeaderStyle-Width="40" UniqueName="Delete"
                                            >
                                            <FilterTemplate>
                                                <%-- <asp:LinkButton ID="lbtnClearFillter" runat="server" CssClass="gray_bt" CommandName="ClearFilter">
                                                        <span> Clear Fillter </span>
                                                 </asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbtnClearFillter" runat="server" CssClass="button" CommandName="ClearFilter"
                                                    ToolTip="Clear Fillter">
                                                    <span>
                                                        <asp:Image ID="imgClearFillter" runat="server" AlternateText="Clear Fillter" ImageUrl="../Images/clear-fillter.png" />
                                                    </span>
                                                </asp:LinkButton>
                                            </FilterTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete Project" AlternateText="Delete"
                                                    ImageUrl="../Images/delete_icon.png" OnClientClick="return ConfirmDeleteProject();"
                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ProjectId") %>' CommandName="Delete" />
                                            </ItemTemplate>
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
    <telerik:RadWindowManager ID="RadWindowManager" runat="server" Modal="True">
        <Windows>
            <telerik:RadWindow ID="OpenPrintConditionWindow" runat="server" IconUrl="" VisibleStatusbar="false"
                ShowContentDuringLoad="false" Title="Open Print Condition" Width="720" Height="220"
                Behaviors="Move,Close" >
            </telerik:RadWindow>
            <telerik:RadWindow ID="ClosePrintConditionWindow" runat="server" VisibleStatusbar="false"
                ShowContentDuringLoad="false" Title="Close Print Condition" Width="720" Height="200"
                Behaviors="Move,Close" >
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <input id="Hidden_TotalProjects" type="hidden" runat="server" value="0" />
</div>
<div class="contant_bottom">
    &nbsp;</div>
