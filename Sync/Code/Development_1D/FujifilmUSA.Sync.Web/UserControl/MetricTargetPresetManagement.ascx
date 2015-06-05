<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MetricTargetPresetManagement.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.MetricTargetPresetManagement" %>

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
        function ConfirmDeleteMetricTargetPreset() {
            if (confirm('<%= MetricTargetPresetManagement_DeleteMetricTargetPreset_Confirmation %>'))
                return true;
            else
                return false;
        }
    </script>
</telerik:RadCodeBlock>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle">
    <div class="head">
        <h1><asp:Localize runat="server" ID="locMetricTargetPresetMgmt" Text="Metric Target Preset Management" 
                meta:resourcekey="locMetricTargetPresetMgmtResource1"></asp:Localize></h1>
          <asp:LinkButton ID="lnkbtnPatchTolerance" runat="server" class="button" 
            onclick="lnkbtnPatchTolerance_Click" Visible="false">
                <span>
                    <asp:Image ID="Image1" runat="server" 
                ImageUrl="../Images/new_project_icon.png" meta:resourcekey="imgAddPMTPResource1" />
                
            <asp:Localize runat="server" Text="Add Metric Target Patch Preset" ID="Localize1" 
                        meta:resourcekey="locAddMetricTargetPatchPresetResource1"></asp:Localize>
            </span>
        </asp:LinkButton>

        &nbsp;<asp:LinkButton ID="likAdd" runat="server" class="button" 
            OnClick="likAdd_Click"  Visible="false">
            <span>
                <asp:Image ID="imgAdd" runat="server" 
            ImageUrl="../Images/new_project_icon.png" meta:resourcekey="imgAddMTPResource1" />
                
            <asp:Localize runat="server" Text="Add Metric Target Preset" ID="locAddMetricTargetPreset" 
                        meta:resourcekey="locAddMetricTargetPresetResource1"></asp:Localize>
            </span>
        </asp:LinkButton>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                    Visible="False" />
                <asp:HiddenField ID="hdSelectedCompanyId" runat="server" Value="0" />
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
                                FilterControlWidth="90px" AutoPostBackOnFilter="True" 
                                CurrentFilterFunction="Contains" 
                                FilterControlAltText="Filter MetricTargetPresetName column" 
                                meta:resourcekey="GridTemplateColumnResource1">
                                <ItemTemplate>
                                    <asp:Label ID="lblMetricTargetPresetName" runat="server" Text='<%# Eval("MetricTargetPresetName") %>' 
                                        meta:resourcekey="lblMetricTargetPresetNameResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="90px" />
                                <ItemStyle CssClass="breakWord90" Width="90px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="MetricTargetPresetDesc" HeaderText="Description"
                                UniqueName="MetricTargetPresetDesc" SortExpression="MetricTargetPresetDesc" FilterControlWidth="90px" 
                                AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" 
                                FilterControlAltText="Filter MetricTargetPresetDesc column" 
                                meta:resourcekey="GridTemplateColumnResource2">
                                <ItemTemplate>
                                    <asp:Label ID="lblMetricTargetPresetDesc" runat="server" Text='<%# Eval("MetricTargetPresetDesc") %>' 
                                        meta:resourcekey="lblLocationResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="90px" />
                                <ItemStyle CssClass="breakWord130" Width="90px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                          
                            <telerik:GridTemplateColumn DataField="Company.CompanyName"
                                HeaderText="Company" UniqueName="CompanyName" 
                                SortExpression="Company.CompanyName" FilterControlWidth="70px" 
                                AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" 
                                FilterControlAltText="Filter CompanyName column" 
                                meta:resourcekey="GridTemplateColumnResource3">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompanyName" runat="server" 
                                        Text='<%# Eval("Company.CompanyName") %>' 
                                        meta:resourcekey="lblCompanyNameResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="70px" />
                                <ItemStyle CssClass="breakWord130" Width="70px" Wrap="True" />
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
                             <telerik:GridTemplateColumn DataField="ValidationType"
                                HeaderText="Type" UniqueName="ValidationType" SortExpression="ValidationType" 
                                FilterControlWidth="40px" AutoPostBackOnFilter="True" 
                                CurrentFilterFunction="Contains" FilterControlAltText="Filter ValidationType column" 
                                meta:resourcekey="GridTemplateColumnResource6">
                                <ItemTemplate>
                                    <asp:Label ID="lblValidationType" runat="server" 
                                        Text='<%# Convert.ToInt16(Eval("ValidationType")) == 1? "G7" : Convert.ToInt16(Eval("ValidationType")) == 2? "ISO" : "Default" %>'  meta:resourcekey="lblValidationTypeResource1" />
                                </ItemTemplate>
                                 <HeaderStyle Width="40px" />
                                 <ItemStyle CssClass="breakWord40" Width="40px" Wrap="True" />
                            </telerik:GridTemplateColumn>

                             <telerik:GridTemplateColumn UniqueName="IsSync1D" AllowFiltering="False" 
                                HeaderText="1D?" FilterControlWidth="50px" DataField="IsSync1D" 
                                FilterControlAltText="Filter IsSync1D column" 
                                meta:resourcekey="GridTemplateColumnResource7">
                                <ItemTemplate>
                                    <asp:Image ID="imgIsSync1D" runat="server" 
                                        ImageUrl='<%# Convert.ToBoolean(Eval("IsSync1D")) ? "~/Images/checkbox_checked.png" : "~/Images/checkbox_unchecked.png" %>' 
                                        meta:resourcekey="imgIsSync1DResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                             <telerik:GridTemplateColumn UniqueName="Validation_1D" AllowFiltering="False" 
                                HeaderText="V_1D?" FilterControlWidth="50px" DataField="Validation_1D" 
                                FilterControlAltText="Filter Validation_1D column" 
                                meta:resourcekey="GridTemplateColumnResource8">
                                <ItemTemplate>
                                    <asp:Image ID="imgValidation_1D" runat="server" 
                                        ImageUrl='<%# Convert.ToBoolean(Eval("Validation_1D")) ? "~/Images/checkbox_checked.png" : "~/Images/checkbox_unchecked.png" %>' 
                                        meta:resourcekey="imgValidation_1DResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                             <telerik:GridTemplateColumn UniqueName="Validation_4D" AllowFiltering="False" 
                                HeaderText="V_4D?" FilterControlWidth="50px" DataField="Validation_4D" 
                                FilterControlAltText="Filter Validation_4D column" 
                                meta:resourcekey="GridTemplateColumnResource9">
                                <ItemTemplate>
                                    <asp:Image ID="imgValidation_4D" runat="server" 
                                        ImageUrl='<%# Convert.ToBoolean(Eval("Validation_4D")) ? "~/Images/checkbox_checked.png" : "~/Images/checkbox_unchecked.png" %>' 
                                        meta:resourcekey="imgValidation_4DResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
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
                                                    <asp:ImageButton ID="imgbtnEdit" runat="server" ToolTip="Edit MetricTargetPreset" AlternateText="Edit"
                                                        ImageUrl="../Images/edit_icon.png" CommandArgument='<%# Eval("MetricTargetPresetID") %>'
                                                        CommandName="Edit" meta:resourcekey="imgbtnEditResource1" />
                                                </div>
                                                <div style="float: left;">
                                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete MetricTargetPreset" AlternateText="Delete"
                                                        ImageUrl="../Images/delete_icon.png" OnClientClick="javascript:return ConfirmDeleteMetricTargetPreset();"
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
