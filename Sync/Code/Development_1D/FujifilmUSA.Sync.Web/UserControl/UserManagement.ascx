<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="UserManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.UserManagement" %>
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
    
     .breakWord120
     {
            max-width: 120px !important;
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
     .breakWord70
     {
            max-width: 70px !important;
            word-break: break-all !important;
            word-wrap: break-word !important;
            vertical-align: top;
            line-height: 15px;
     }
    
</style>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript" language="javascript">
        function ConfirmDeleteUser() {
            if (confirm('<%= UserManagement_DeleteUser_Confirmation %>')) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</telerik:RadCodeBlock>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle">
    <div class="head">
        <h1><asp:Localize ID="locUserManagment" runat="server" Text="User Management" 
                meta:resourcekey="locUserManagmentResource1"></asp:Localize></h1>
         <asp:LinkButton ID="likUserReport" runat="server" class="button" 
            OnClick="likUserReport_Click" Visible="false">
            <span>
                <asp:Image ID="Image1" runat="server" 
            ImageUrl="../Images/new_project_icon.png" meta:resourcekey="imgUserReportResource1" />
                
            <asp:Localize runat="server" Text="User Report" Id="Localize1" 
                        meta:resourcekey="locUserReportResource1"></asp:Localize>
            </span>
        </asp:LinkButton>
        <asp:LinkButton ID="likAdd" runat="server" class="button" 
            OnClick="likAdd_Click">
            <span>
                <asp:Image ID="imgAdd" runat="server" 
            ImageUrl="../Images/new_project_icon.png" meta:resourcekey="imgAddResource1" />
                
            <asp:Localize runat="server" Text="Add User" Id="locAddUser" 
                        meta:resourcekey="locAddUserResource1"></asp:Localize>
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
                <telerik:RadGrid ID="rgUser" AllowFilteringByColumn="True" AllowSorting="True"
                    AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False" EnableLinqExpressions="False"
                    OnNeedDataSource="rgUser_NeedDataSource" 
                    OnItemCommand="rgUser_ItemCommand" Width="98%" CellSpacing="0" GridLines="None">
                    <GroupingSettings CaseSensitive="False" />
                    <HeaderStyle Font-Bold="True" />
                    <PagerStyle Position="Top" AlwaysVisible="True" Font-Size="Small" />
                    <MasterTableView DataKeyNames="UserId">
                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                            Visible="True">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                            Visible="True">
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn DataField="LoginId" HeaderText="User Name"
                                UniqueName="LoginId" SortExpression="LoginId" FilterControlWidth="100px" 
                                AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" 
                                FilterControlAltText="Filter LoginId column" 
                                meta:resourcekey="GridTemplateColumnResource1">
                                <ItemTemplate>
                                    <asp:Label ID="lblLoginId" runat="server" Text='<%# Eval("LoginId") %>' 
                                        meta:resourcekey="lblLoginIdResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                                <ItemStyle CssClass="breakWord100" Width="100px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="FirstName" HeaderText="First Name"
                                UniqueName="FirstName" SortExpression="FirstName" 
                                FilterControlWidth="70px" AutoPostBackOnFilter="True" 
                                CurrentFilterFunction="Contains" FilterControlAltText="Filter FirstName column" 
                                meta:resourcekey="GridTemplateColumnResource2">
                                <ItemTemplate>
                                    <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("FirstName") %>' 
                                        meta:resourcekey="lblFirstNameResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="70px" />
                                <ItemStyle CssClass="breakWord70" Width="70px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="LastName" HeaderText="Last Name"
                                UniqueName="LastName" SortExpression="LastName" FilterControlWidth="70px" 
                                AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" 
                                FilterControlAltText="Filter LastName column" 
                                meta:resourcekey="GridTemplateColumnResource3">
                                <ItemTemplate>
                                    <asp:Label ID="lblLastName" runat="server" Text='<%# Eval("LastName") %>' 
                                        meta:resourcekey="lblLastNameResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="70px" />
                                <ItemStyle CssClass="breakWord70" Width="70px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="Email" HeaderText="Email"
                                UniqueName="Email" SortExpression="Email" FilterControlWidth="120px" 
                                AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" 
                                FilterControlAltText="Filter Email column" 
                                meta:resourcekey="GridTemplateColumnResource4">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>' 
                                        meta:resourcekey="lblEmailResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="120px" />
                                <ItemStyle CssClass="breakWord120" Width="120px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="UserCompany.Company.CompanyName"
                                HeaderText="Company" UniqueName="companyName" SortExpression="UserCompany.Company.CompanyName"
                                FilterControlWidth="70px" AutoPostBackOnFilter="True" 
                                CurrentFilterFunction="Contains" 
                                FilterControlAltText="Filter companyName column" 
                                meta:resourcekey="GridTemplateColumnResource5">
                                <ItemTemplate>
                                    <asp:Label ID="lblcompanyName" runat="server" 
                                        Text='<%# Eval("UserCompany.Company.CompanyName") %>' 
                                        meta:resourcekey="lblcompanyNameResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="70px" />
                                <ItemStyle CssClass="breakWord70" Width="70px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="BranchNames" HeaderText="Branch"
                                UniqueName="BranchName" SortExpression="BranchNames" 
                                FilterControlWidth="70px" AutoPostBackOnFilter="True" 
                                CurrentFilterFunction="Contains" 
                                FilterControlAltText="Filter BranchName column" 
                                meta:resourcekey="GridTemplateColumnResource6">
                                <ItemTemplate>
                                    <asp:Label ID="lblBranchName" runat="server" Text='<%# Eval("BranchNames") %>' 
                                        meta:resourcekey="lblBranchNameResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="70px" />
                                <ItemStyle CssClass="breakWord70" Width="70px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="IsActive" AllowFiltering="False" HeaderText="IsActive?"
                                DataField="IsActive" FilterControlWidth="50px" 
                                FilterControlAltText="Filter IsActive column" 
                                meta:resourcekey="GridTemplateColumnResource7" >
                                <ItemTemplate>
                                    <asp:Image ID="imgIsActive" runat="server" 
                                        ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) ? "~/Images/checkbox_checked.png" : "~/Images/checkbox_unchecked.png" %>' 
                                        meta:resourcekey="imgIsActiveResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="BranchID"
                                UniqueName="Edit" FilterControlWidth="70px" 
                                FilterControlAltText="Filter Edit column" 
                                meta:resourcekey="GridTemplateColumnResource8" >
                                <FilterTemplate>
                                    <center> 
                                    <asp:LinkButton ID="lbtnClearFillter" runat="server" ToolTip ="Clear Fillter" 
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
                                                    <asp:ImageButton ID="imgbtnEdit" runat="server" ToolTip="Edit User" AlternateText="Edit"
                                                        ImageUrl="../Images/edit_icon.png" CommandArgument='<%# Eval("UserId") %>' 
                                                        CommandName="Edit" meta:resourcekey="imgbtnEditResource1" />
                                                </div>
                                                <div style="float: left;">
                                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete User" AlternateText="Delete"
                                                        ImageUrl="../Images/delete_icon.png" OnClientClick="javascript:return ConfirmDeleteUser()"
                                                        CommandArgument='<%# Eval("UserId") %>' CommandName="Delete" 
                                                        meta:resourcekey="imgbtnDeleteResource1" />
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                        </center>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Width="70px" />
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
