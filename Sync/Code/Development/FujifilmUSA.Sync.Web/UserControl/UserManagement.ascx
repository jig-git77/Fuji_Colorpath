<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.UserManagement" %>
<%@ Register TagName="Statusbar" TagPrefix="ucStatusbar" Src="Statusbar.ascx" %>
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
        <h1>
            User Management</h1>
        <asp:LinkButton ID="likAdd" runat="server" class="button" OnClick="likAdd_Click">
            <span>
                <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/new_project_icon.png" />
                Add User</span>
        </asp:LinkButton>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px; margin-right: 15px;">
        <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="false" />
    </div>
    <div style="margin-left: 10px;">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hdSelectedCompanyId" runat="server" Value="0" />
                <telerik:RadGrid ID="rgUser" ShowHeader="true" AllowFilteringByColumn="true" AllowSorting="true"
                    AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False" EnableLinqExpressions="false"
                    OnNeedDataSource="rgUser_NeedDataSource" OnItemCommand="rgUser_ItemCommand" Width="98%"
                    HeaderStyle-Font-Bold="true">
                    <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />
                    <MasterTableView DataKeyNames="UserId">
                        <Columns>
                            <telerik:GridTemplateColumn DataField="LoginId" AllowFiltering="true" HeaderText="User Name"
                                UniqueName="LoginId" SortExpression="LoginId" FilterControlWidth="100" HeaderStyle-Width ="100"  
                                ItemStyle-CssClass="breakWord100" ItemStyle-Width="100" ItemStyle-Wrap="true" 
                                ShowFilterIcon="true" >
                                <ItemTemplate>
                                    <asp:Label ID="lblLoginId" runat="server" Text='<%# Eval("LoginId") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="FirstName" AllowFiltering="true" HeaderText="First Name"
                                UniqueName="FirstName" SortExpression="FirstName" FilterControlWidth="70" HeaderStyle-Width ="70"  
                                 ItemStyle-CssClass="breakWord70" ItemStyle-Width="70" ItemStyle-Wrap="true" 
                                 ShowFilterIcon="true" >
                                <ItemTemplate>
                                    <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("FirstName") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="LastName" AllowFiltering="true" HeaderText="Last Name"
                                UniqueName="LastName" SortExpression="LastName" FilterControlWidth="70" HeaderStyle-Width ="70"  
                                ItemStyle-CssClass="breakWord70" ItemStyle-Width="70" ItemStyle-Wrap="true" 
                                ShowFilterIcon="true" >
                                <ItemTemplate>
                                    <asp:Label ID="lblLastName" runat="server" Text='<%# Eval("LastName") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="Email" AllowFiltering="true" HeaderText="Email"
                                UniqueName="Email" SortExpression="Email" FilterControlWidth="120" HeaderStyle-Width ="120"  
                                ItemStyle-CssClass="breakWord120" ItemStyle-Width="120" ItemStyle-Wrap="true" 
                                ShowFilterIcon="true" >
                                <ItemTemplate>
                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="UserCompanies.Company.CompanyName" AllowFiltering="true"
                                HeaderText="company" UniqueName="companyName" SortExpression="UserCompanies.Company.CompanyName"
                                FilterControlWidth="70" HeaderStyle-Width ="70"  
                                ItemStyle-CssClass="breakWord70" ItemStyle-Width="70" ItemStyle-Wrap="true" 
                                ShowFilterIcon="true" >
                                <ItemTemplate>
                                    <asp:Label ID="lblcompanyName" runat="server" Text='<%# Eval("UserCompany.Company.CompanyName") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="BranchNames" AllowFiltering="true" HeaderText="Branch"
                                UniqueName="BranchName" SortExpression="BranchNames" FilterControlWidth="70" HeaderStyle-Width ="70" 
                                ItemStyle-CssClass="breakWord70" ItemStyle-Width="70" ItemStyle-Wrap="true" 
                                ShowFilterIcon="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblBranchName" runat="server" Text='<%# Eval("BranchNames") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="IsActive" AllowFiltering="false" HeaderText="IsActive?"
                                DataField="IsActive" ItemStyle-HorizontalAlign="Center" FilterControlWidth="50" HeaderStyle-Width ="50" >
                                <ItemTemplate>
                                    <asp:Image ID="imgIsActive" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) ? "~/Images/checkbox_checked.png" : "~/Images/checkbox_unchecked.png"%>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="BranchID" AllowFiltering="true" HeaderText=""
                                UniqueName="Edit" FilterControlWidth="70" HeaderStyle-Width ="70" >
                                <FilterTemplate>
                                    <center> 
                                    <asp:LinkButton ID="lbtnClearFillter" runat="server" ToolTip ="Clear Fillter" CommandName="ClearFilter">
                                                    <span>
                                                        <asp:Image ID="imgClearFillter" runat="server" AlternateText="Clear Fillter" ImageUrl="../Images/clear-fillter.png" />
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
                                                        ImageUrl="../Images/edit_icon.png" CommandArgument='<%# Eval("UserId") %>' CommandName="Edit" />
                                                </div>
                                                <div style="float: left;">
                                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete User" AlternateText="Delete"
                                                        ImageUrl="../Images/delete_icon.png" OnClientClick="javascript:return ConfirmDeleteUser()"
                                                        CommandArgument='<%# Eval("UserId") %>' CommandName="Delete" />
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
