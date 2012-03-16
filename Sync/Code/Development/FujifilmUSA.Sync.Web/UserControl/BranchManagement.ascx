<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BranchManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.BranchManagement" %>
<%@ Register TagName="Statusbar" TagPrefix="ucStatusbar" Src="Statusbar.ascx" %>

<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle">
    <div class="head">
        <h1>
            Branch Management</h1>
        <asp:LinkButton ID="likAdd" runat="server" class="button" OnClick="likAdd_Click">
            <span>
                <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/new_project_icon.png" />
                Add Branch</span>
        </asp:LinkButton>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px; margin-right: 15px;">
        <ucStatusbar:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
    </div>
    <div style="margin-left: 10px;">
     <asp:UpdatePanel runat="server">
            <ContentTemplate>
            <asp:HiddenField ID="hdSelectedCompanyId" runat="server" Value="0" />
        <telerik:RadGrid ID="rgBranch" ShowHeader="true" AllowFilteringByColumn="true" AllowSorting="false"
            AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False" EnableLinqExpressions="false"
            OnNeedDataSource="rgBranch_NeedDataSource" OnItemCommand="rgBranch_ItemCommand"
             Width="98%" >
            <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />
            <MasterTableView DataKeyNames="BranchID,CompanyID,BranchName,Location,IsActive">
                <Columns>
                    <telerik:GridTemplateColumn 
                        DataField="BranchName" 
                        AllowFiltering="true" 
                        HeaderText="Branch Name"
                        UniqueName="BranchName" 
                        SortExpression="BranchName" 
                        FilterControlWidth="300"
                        ItemStyle-Wrap="true" 
                        CurrentFilterFunction="Contains" 
                        ShowFilterIcon="false"
                        AutoPostBackOnFilter="true"
                        >
                        <ItemTemplate>
                            <asp:Label ID="lblBranchName" runat="server" Text='<%# Eval("BranchName") %>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn 
                        DataField="Location" 
                        AllowFiltering="true" 
                        HeaderText="Location"
                        UniqueName="Location" 
                        SortExpression="Location" 
                        FilterControlWidth="200"
                        ItemStyle-Wrap="true" 
                        CurrentFilterFunction="Contains" 
                        ShowFilterIcon="false"
                        AutoPostBackOnFilter="true">
                        <ItemTemplate>
                            <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location") %>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn 
                        DataField="Company.CompanyName" 
                        AllowFiltering="true" 
                        HeaderText="CompanyName"
                        UniqueName="CompanyName" 
                        SortExpression="CompanyName" 
                        FilterControlWidth="200"
                        ItemStyle-Wrap="true" 
                        CurrentFilterFunction="Contains" 
                        ShowFilterIcon="false"
                        AutoPostBackOnFilter="true">
                        <ItemTemplate>
                            <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("Company.CompanyName") %>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn 
                        DataField="BranchID" 
                        AllowFiltering="false" 
                        HeaderText=""
                        UniqueName="Edit">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnEdit" 
                                runat="server" 
                                ToolTip="Edit Branch" 
                                AlternateText="Edit"
                                ImageUrl="../Images/edit_icon.png" 
                                CommandArgument ='<%# Eval("BranchID") %>'
                                CommandName="Edit" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn 
                        DataField="BranchID" 
                        AllowFiltering="false" 
                        HeaderText=""
                        UniqueName="Delete">
                        <ItemTemplate>
                             <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server" >
                                <script type="text/javascript" language="javascript">
                                    function ConfirmDeleteBranch() {
                                        if (confirm('<%= CompanyManagement_DeleteCompany_Confirmation %>'))
                                            return true;
                                        else
                                            return false;
                                    }
                                </script>
                            </telerik:RadCodeBlock>
                            <asp:ImageButton ID="imgbtnDelete" runat="server" 
                                ToolTip="Delete Branch" 
                                AlternateText="Delete"
                                ImageUrl="../Images/delete_icon.png" 
                                OnClientClick="javascript:ConfirmDeleteBranch();"
                                CommandArgument ='<%# Eval("BranchID") %>'
                                CommandName="Delete" />
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
