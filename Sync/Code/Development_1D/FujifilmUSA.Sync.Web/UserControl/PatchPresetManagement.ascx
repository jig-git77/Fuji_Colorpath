<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="PatchPresetManagement.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.PatchPresetManagement" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
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
    .breakWord150
    {
        max-width: 150px !important;
        word-break: break-all !important;
        word-wrap: break-word !important;
        vertical-align: top;
        line-height: 15px;
    }
    .breakWord130
    {
        max-width: 130px !important;
        word-break: break-all !important;
        word-wrap: break-word !important;
        vertical-align: top;
        line-height: 15px;
    }
    .breakWord100
    {
        max-width: 100px !important;
        word-break: break-all !important;
        word-wrap: break-word !important;
        vertical-align: top;
        line-height: 15px;
    }
</style>
 
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript" language="javascript">
        function ConfirmDeletePatchPreset() {
            if (confirm('<%= PatchPresetManagement_DeletePatchPreset_Confirmation %>'))
                return true;
            else
                return false;
        }

        function ConfirmTotalPatchPreset() {
            if (('<%= TotalPatchPresets %>') < 3)
                return true;
            else
                alert('<%= PatchPresetManagement_HitLimitPatchPresets %>');
            return false;
        }
    </script>
</telerik:RadCodeBlock>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle">

    <div class="head">
        <h1><asp:Localize runat="server" Text="Patch Preset Management" ID="locPatchPresetMgmt" 
                meta:resourcekey="locPatchPresetMgmtResource1"></asp:Localize></h1>
            <asp:LinkButton ID="lnkbtnPatchTolerance" runat="server" class="button" OnClientClick="javascript:return ConfirmTotalPatchPreset();"
             onclick="lnkbtnPatchTolerance_Click">
                <span>
                    <asp:Image ID="Image1" runat="server" 
                ImageUrl="../Images/new_project_icon.png" meta:resourcekey="imgAddPMTPResource1" />
                
                <asp:Localize runat="server" Text="Add Patch Preset" ID="Localize1" 
                        meta:resourcekey="locAddPatchPresetResource1"></asp:Localize>
            </span>
        </asp:LinkButton>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
       <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
                <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                    Visible="False" />
                <telerik:RadGrid ID="rgMetricTargetPreset" AllowFilteringByColumn="True" AllowSorting="True"
                    AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False" EnableLinqExpressions="False"
                    OnNeedDataSource="rgMetricTargetPreset_NeedDataSource" OnItemCommand="rgMetricTargetPreset_ItemCommand" 
                    Width="98%" CellSpacing="0" GridLines="None" 
                    meta:resourcekey="rgMetricTargetPresetResource1">
                    <GroupingSettings CaseSensitive="False" />
                    <HeaderStyle Font-Bold="True" />
                    <PagerStyle Position="Top" AlwaysVisible="True" Font-Size="Small" />
                    <MasterTableView DataKeyNames="MetricTargetPresetID,Company.CompanyID,MetricTargetPresetName,IsUsed,IsPatchTolerance">
                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                            Visible="True">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                            Visible="True">
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn DataField="MetricTargetPresetName" HeaderText="Preset Name"
                                UniqueName="MetricTargetPresetName" SortExpression="MetricTargetPresetName" 
                                FilterControlWidth="130px" AutoPostBackOnFilter="True" 
                                CurrentFilterFunction="Contains" 
                                FilterControlAltText="Filter MetricTargetPresetName column" 
                                meta:resourcekey="GridTemplateColumnResource1">
                                <ItemTemplate>
                                    <asp:Label ID="lblMetricTargetPresetName" runat="server" Text='<%# Eval("MetricTargetPresetName") %>' 
                                        meta:resourcekey="lblMetricTargetPresetNameResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="130px" />
                                <ItemStyle CssClass="breakWord130" Width="130px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="MetricTargetPresetDesc" HeaderText="Description"
                                UniqueName="MetricTargetPresetDesc" SortExpression="MetricTargetPresetDesc" FilterControlWidth="150px" 
                                AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" 
                                FilterControlAltText="Filter MetricTargetPresetDesc column" 
                                meta:resourcekey="GridTemplateColumnResource2">
                                <ItemTemplate>
                                    <asp:Label ID="lblMetricTargetPresetDesc" runat="server" Text='<%# Eval("MetricTargetPresetDesc") %>' 
                                        meta:resourcekey="lblLocationResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="150px" />
                                <ItemStyle CssClass="breakWord150" Width="150px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                          
                            <telerik:GridTemplateColumn DataField="Company.CompanyName"
                                HeaderText="Company" UniqueName="CompanyName" 
                                SortExpression="Company.CompanyName" FilterControlWidth="150px" 
                                AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" 
                                FilterControlAltText="Filter CompanyName column" 
                                meta:resourcekey="GridTemplateColumnResource3">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompanyName" runat="server" 
                                        Text='<%# Eval("Company.CompanyName") %>' 
                                        meta:resourcekey="lblCompanyNameResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="150px" />
                                <ItemStyle CssClass="breakWord150" Width="150px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                             <telerik:GridTemplateColumn DataField="Region.RegionName"
                                HeaderText="Region" UniqueName="Region" SortExpression="Region.RegionName" 
                                FilterControlWidth="50px" AutoPostBackOnFilter="True" 
                                CurrentFilterFunction="Contains" FilterControlAltText="Filter Region column" 
                                meta:resourcekey="GridTemplateColumnResource4">
                                <ItemTemplate>
                                    <asp:Label ID="lblRegion" runat="server" 
                                        Text='<%# Eval("Region.RegionName") %>' meta:resourcekey="lblRegionResource1" />
                                </ItemTemplate>
                                 <HeaderStyle Width="50px" />
                                 <ItemStyle CssClass="breakWord50" Width="50px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                             <telerik:GridTemplateColumn DataField="dEMethod"
                                HeaderText="dEMethod" UniqueName="dEMethod" SortExpression="dEMethod" 
                                FilterControlWidth="50px" AutoPostBackOnFilter="True" 
                                CurrentFilterFunction="Contains" FilterControlAltText="Filter dEMethod column" 
                                meta:resourcekey="GridTemplateColumnResource5">
                                <ItemTemplate>
                                    <asp:Label ID="lbldEMethod" runat="server" 
                                        Text='<%# Eval("dEMethod") %>' meta:resourcekey="lbldEMethodResource1" />
                                </ItemTemplate>
                                 <HeaderStyle Width="50px" />
                                 <ItemStyle CssClass="breakWord50" Width="50px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                           
                            <telerik:GridTemplateColumn UniqueName="IsUsed" AllowFiltering="False" 
                                HeaderText="IsUsed?" FilterControlWidth="50px" DataField="IsUsed" 
                                FilterControlAltText="Filter IsUsed column" 
                                meta:resourcekey="GridTemplateColumnResource10">
                                <ItemTemplate>
                                    <asp:Image ID="imgIsUsed" runat="server" 
                                        ImageUrl='<%# Convert.ToBoolean(Eval("IsUsed")) ? "~/Images/checkbox_checked.png" : "~/Images/checkbox_unchecked.png" %>' 
                                        meta:resourcekey="imgIsUsedResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="MetricTargetPresetID"
                                UniqueName="Edit" FilterControlAltText="Filter Edit column" 
                                meta:resourcekey="GridTemplateColumnResource11">
                                <FilterTemplate>
                                    <center>
                                        <asp:LinkButton ID="lbtnClearFillter" runat="server" ToolTip="Clear Fillter" 
                                            CommandName="ClearFilter">
                                            <span>
                                                <asp:Image ID="imgClearFillter" runat="server" 
                                            AlternateText="Clear Fillter" ImageUrl="../Images/clear-fillter.png" 
                                            meta:resourcekey="imgClearFillterResource1" />

                                            </span>
                                        </asp:LinkButton>
                                    </center>
                                </FilterTemplate>
                                <ItemTemplate>
                                    <div>
                                        <center>
                                            <div style="width: 45px;">
                                                <div style="float: left; padding-right: 10px;">
                                                    <asp:ImageButton ID="imgbtnEdit" runat="server" ToolTip="Edit PatchPreset" AlternateText="Edit"
                                                        ImageUrl="../Images/edit_icon.png" CommandArgument='<%# Eval("MetricTargetPresetID") %>'
                                                        CommandName="Edit" meta:resourcekey="imgbtnEditResource1" />
                                                </div>
                                                <div style="float: left;">
                                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete PatchPreset" AlternateText="Delete"
                                                        ImageUrl="../Images/delete_icon.png" OnClientClick="javascript:return ConfirmDeletePatchPreset();"
                                                        CommandArgument='<%# Eval("MetricTargetPresetID") %>' CommandName="Delete" 
                                                        meta:resourcekey="imgbtnDeleteResource1" />
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                        </center>
                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>                        
                    </MasterTableView>                    
                </telerik:RadGrid>
           <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
   
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
