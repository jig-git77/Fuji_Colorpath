<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="BranchManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.BranchManagement" %>
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
        function ConfirmDeleteBranch() {
            if (confirm('<%= BranchManagement_DeleteBranch_Confirmation %>'))
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
        <h1><asp:Localize runat="server" ID="locBranchMgmt" Text="Branch Management" 
                meta:resourcekey="locBranchMgmtResource1"></asp:Localize></h1>
        <asp:LinkButton ID="likAdd" runat="server" class="button" 
            OnClick="likAdd_Click" meta:resourcekey="likAddResource1">
            <span>
                <asp:Image ID="imgAdd" runat="server" 
            ImageUrl="../Images/new_project_icon.png" meta:resourcekey="imgAddResource1" />
                
<asp:Localize runat="server" Text="Add Branch" ID="locAddBranch" 
            meta:resourcekey="locAddBranchResource1"></asp:Localize>
</span>
        </asp:LinkButton>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                    Visible="False" />
                <asp:HiddenField ID="hdSelectedCompanyId" runat="server" Value="0" />
                <telerik:RadGrid ID="rgBranch" AllowFilteringByColumn="True" AllowSorting="True"
                    AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False" EnableLinqExpressions="False"
                    OnNeedDataSource="rgBranch_NeedDataSource" OnItemCommand="rgBranch_ItemCommand"
                    Width="98%" CellSpacing="0" GridLines="None" 
                    meta:resourcekey="rgBranchResource1">
                    <GroupingSettings CaseSensitive="False" />
                    <HeaderStyle Font-Bold="True" />
                    <PagerStyle Position="Top" AlwaysVisible="True" Font-Size="Small" />
                    <MasterTableView DataKeyNames="BranchID,CompanyID,BranchName,Location,IsActive">
                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                            Visible="True">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                            Visible="True">
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn DataField="BranchName" HeaderText="Branch Name"
                                UniqueName="BranchName" SortExpression="BranchName" 
                                FilterControlWidth="130px" AutoPostBackOnFilter="True" 
                                CurrentFilterFunction="Contains" 
                                FilterControlAltText="Filter BranchName column" 
                                meta:resourcekey="GridTemplateColumnResource1">
                                <ItemTemplate>
                                    <asp:Label ID="lblBranchName" runat="server" Text='<%# Eval("BranchName") %>' 
                                        meta:resourcekey="lblBranchNameResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="130px" />
                                <ItemStyle CssClass="breakWord130" Width="130px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="Location" HeaderText="Location"
                                UniqueName="Location" SortExpression="Location" FilterControlWidth="90px" 
                                AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" 
                                FilterControlAltText="Filter Location column" 
                                meta:resourcekey="GridTemplateColumnResource2">
                                <ItemTemplate>
                                    <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location") %>' 
                                        meta:resourcekey="lblLocationResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="90px" />
                                <ItemStyle CssClass="breakWord130" Width="90px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                              <telerik:GridTemplateColumn DataField="SupportID" HeaderText="SupportID"
                                UniqueName="SupportID" SortExpression="SupportID" 
                                FilterControlWidth="70px" AutoPostBackOnFilter="True" 
                                CurrentFilterFunction="Contains" FilterControlAltText="Filter SupportID column" 
                                meta:resourcekey="GridTemplateColumnResource3">
                                <ItemTemplate>
                                    <asp:Label ID="lblSupportID" runat="server" Text='<%# Eval("SupportID") %>' 
                                        meta:resourcekey="lblSupportIDResource1" />
                                </ItemTemplate>
                                  <HeaderStyle Width="70px" />
                                  <ItemStyle CssClass="breakWord100" Width="70px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="Company.CompanyName"
                                HeaderText="CompanyName" UniqueName="CompanyName" 
                                SortExpression="Company.CompanyName" FilterControlWidth="200px" 
                                AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" 
                                FilterControlAltText="Filter CompanyName column" 
                                meta:resourcekey="GridTemplateColumnResource4">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompanyName" runat="server" 
                                        Text='<%# Eval("Company.CompanyName") %>' 
                                        meta:resourcekey="lblCompanyNameResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="200px" />
                                <ItemStyle CssClass="breakWord150" Width="200px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                             <telerik:GridTemplateColumn DataField="Region.RegionName"
                                HeaderText="Region" UniqueName="Region" SortExpression="Region.RegionName" 
                                FilterControlWidth="50px" AutoPostBackOnFilter="True" 
                                CurrentFilterFunction="Contains" FilterControlAltText="Filter Region column" 
                                meta:resourcekey="GridTemplateColumnResource5">
                                <ItemTemplate>
                                    <asp:Label ID="lblRegion" runat="server" 
                                        Text='<%# Eval("Region.RegionName") %>' meta:resourcekey="lblRegionResource1" />
                                </ItemTemplate>
                                 <HeaderStyle Width="50px" />
                                 <ItemStyle CssClass="breakWord150" Width="50px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="IsActive" AllowFiltering="False" 
                                HeaderText="IsActive?" FilterControlWidth="50px" DataField="IsActive" 
                                FilterControlAltText="Filter IsActive column" 
                                meta:resourcekey="GridTemplateColumnResource6">
                                <ItemTemplate>
                                    <asp:Image ID="imgIsActive" runat="server" 
                                        ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) ? "~/Images/checkbox_checked.png" : "~/Images/checkbox_unchecked.png" %>' 
                                        meta:resourcekey="imgIsActiveResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="BranchID"
                                UniqueName="Edit" FilterControlAltText="Filter Edit column" 
                                meta:resourcekey="GridTemplateColumnResource7">
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
                                                    <asp:ImageButton ID="imgbtnEdit" runat="server" ToolTip="Edit Branch" AlternateText="Edit"
                                                        ImageUrl="../Images/edit_icon.png" CommandArgument='<%# Eval("BranchID") %>'
                                                        CommandName="Edit" meta:resourcekey="imgbtnEditResource1" />
                                                </div>
                                                <div style="float: left;">
                                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete Branch" AlternateText="Delete"
                                                        ImageUrl="../Images/delete_icon.png" OnClientClick="javascript:return ConfirmDeleteBranch();"
                                                        CommandArgument='<%# Eval("BranchID") %>' CommandName="Delete" 
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
