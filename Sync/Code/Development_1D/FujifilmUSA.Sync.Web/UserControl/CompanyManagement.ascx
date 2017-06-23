<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="CompanyManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.CompanyManagement" %>
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
        .RadGrid_Default .rgRow .rgSorted {
        background-color: #F7F7F7;
        border-bottom-color: #F7F7F7;
    }
    .RadGrid_Default .rgAltRow .rgSorted {
            background-color: #F7F7F7;
            border-bottom-color: #F7F7F7;
    }

    .RadGrid_Default th.rgSorted 
    {
            background-color: #F1F1F1;
            background-position: 0px;
    }
    .breakWord200
     {
            max-width: 200px !important;
            word-break: break-all !important;
            word-wrap: break-word !important;
            vertical-align: top;
            line-height: 15px;
     }
    .breakWord300
     {
            max-width: 300px !important;
            word-break: break-all !important;
            word-wrap: break-word !important;
            vertical-align: top;
            line-height: 15px;
     }
     
    
</style>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript" language="javascript">
        function ConfirmDeleteCompany() {
            if (confirm('<%= CompanyManagement_DeleteCompany_Confirmation %>'))
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
        <h1><asp:Localize runat="server" Text="Company Management" ID="locCompanyMgmt" 
                meta:resourcekey="locCompanyMgmtResource1"></asp:Localize></h1>
        <asp:LinkButton ID="likAdd" runat="server" class="button" 
            OnClick="likAdd_Click" meta:resourcekey="likAddResource1">
            <span>
                <asp:Image ID="imgAdd" runat="server" 
            ImageUrl="../Images/new_project_icon.png" meta:resourcekey="imgAddResource1" />
                
<asp:Localize runat="server" Text="Add Company" ID="locAddCompany" 
            meta:resourcekey="locAddCompanyResource1"></asp:Localize>
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
                <telerik:RadGrid ID="rgCompany" AllowFilteringByColumn="True" AllowSorting="True"
                    AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False" EnableLinqExpressions="False"
                    OnNeedDataSource="rgCompany_NeedDataSource" OnItemCommand="rgCompany_ItemCommand"
                    OnItemDataBound="rgCompany_ItemDataBound" Width="98%" CellSpacing="0" 
                    GridLines="None">
                    <GroupingSettings CaseSensitive="False" />
                    <HeaderStyle Font-Bold="True" />
                    <PagerStyle Position="Top" AlwaysVisible="True" Font-Size="Small" />
                    <MasterTableView DataKeyNames="CompanyId,CompanyName,RegionId,IsActive">
                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                            Visible="True">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                            Visible="True">
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn DataField="CompanyName" HeaderText="Company Name"
                                UniqueName="CompanyName" SortExpression="CompanyName" 
                                FilterControlWidth="300px" AutoPostBackOnFilter="True" 
                                CurrentFilterFunction="Contains" 
                                FilterControlAltText="Filter CompanyName column" 
                                meta:resourcekey="GridTemplateColumnResource1">                              
                                <ItemTemplate>
                                    <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>' 
                                        meta:resourcekey="lblCompanyNameResource1" />
                                </ItemTemplate>
                                <ItemStyle CssClass="breakWord300" Width="300px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="Region.RegionName" HeaderText="Region Name"
                                UniqueName="RegionName" SortExpression="Region.RegionName" 
                                FilterControlWidth="200px" AutoPostBackOnFilter="True" 
                                CurrentFilterFunction="Contains" 
                                FilterControlAltText="Filter RegionName column" 
                                meta:resourcekey="GridTemplateColumnResource2">                                
                                <ItemTemplate>
                                    <asp:Label ID="lblRegionName" runat="server" 
                                        Text='<%# Eval("Region.RegionName") %>' 
                                        meta:resourcekey="lblRegionNameResource1" />
                                </ItemTemplate>
                                <ItemStyle CssClass="breakWord200" Width="200px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="IsActive" AllowFiltering="False" HeaderText="IsActive?"
                                DataField="IsActive" FilterControlAltText="Filter IsActive column" 
                                meta:resourcekey="GridTemplateColumnResource3">
                                <ItemTemplate>
                                    <asp:Image ID="imgIsActive" runat="server" 
                                        ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) ? "~/Images/checkbox_checked.png" : "~/Images/checkbox_unchecked.png" %>' 
                                        meta:resourcekey="imgIsActiveResource1" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="CompanyId"
                                UniqueName="Edit" FilterControlAltText="Filter Edit column" 
                                meta:resourcekey="GridTemplateColumnResource4">
                                <FilterTemplate>
                                    <center>
                                    <asp:LinkButton ID="lbtnClearFillter" runat="server" ToolTip="Clear Fillter"  
                                            CommandName="Fillter">
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
                                                    <asp:ImageButton ID="imgbtnEdit" runat="server" ToolTip="Edit Company" AlternateText="Edit"
                                                        ImageUrl="../Images/edit_icon.png" CommandArgument='<%# Eval("CompanyId") %>'
                                                        CommandName="Edit" meta:resourcekey="imgbtnEditResource1" />
                                                </div>
                                                <div style="float: left;">
                                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete Company" AlternateText="Delete"
                                                        ImageUrl="../Images/delete_icon.png" OnClientClick="javascript:return ConfirmDeleteCompany();"
                                                        CommandArgument='<%# Eval("CompanyId") %>' CommandName="Delete" 
                                                        meta:resourcekey="imgbtnDeleteResource1" />
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                        </center>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
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
