<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BranchManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.BranchManagement" %>
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
</style>
 <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server" >
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
             Width="98%" HeaderStyle-Font-Bold="true" >
            <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />
            <MasterTableView DataKeyNames="BranchID,CompanyID,BranchName,Location,IsActive">
                <Columns>
                    <telerik:GridTemplateColumn 
                        DataField="BranchName" 
                        AllowFiltering="true" 
                        HeaderText="Branch Name"
                        UniqueName="BranchName" 
                        SortExpression="BranchName" 
                        FilterControlWidth="200"
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
                        FilterControlWidth="150"
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
                        AutoPostBackOnFilter="true"
                        DataType="System.String"
                        >
                        <ItemTemplate>
                            <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("Company.CompanyName") %>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn 
                                    UniqueName="IsActive" 
                                    AllowFiltering="false" 
                                    HeaderText="IsActive?"
                                    DataField="IsActive" 
                                    ItemStyle-HorizontalAlign="Center"  
                                    >
                                    <ItemTemplate>
                                        <asp:Image ID="imgIsActive"  runat="server" ImageUrl=<%# Convert.ToBoolean(Eval("IsActive")) ? "~/Images/checkbox_checked.png" : "~/Images/checkbox_unchecked.png"%> />
                                        
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn 
                        DataField="BranchID" 
                        AllowFiltering="true" 
                        HeaderText=""
                        UniqueName="Edit">
                        <FilterTemplate>
                                 <asp:LinkButton ID="lbtnClearFillter" runat="server" CssClass="gray_bt" CommandName="ClearFilter">
                                                    <span> Clear Fillter </span>
                                </asp:LinkButton>
                        </FilterTemplate>
                        <ItemTemplate>
                        
                           
                            <div>
                                <center>
                                <div style="width: 45px;">     
                                    <div style="float:left; padding-right: 10px;">
                                         <asp:ImageButton ID="imgbtnEdit" 
                                                        runat="server" 
                                                        ToolTip="Edit Branch" 
                                                        AlternateText="Edit"
                                                        ImageUrl="../Images/edit_icon.png" 
                                                        CommandArgument ='<%# Eval("BranchID") %>'
                                                        CommandName="Edit" />
                                    </div>
                                    <div style="float:left;" >
                                       <asp:ImageButton ID="imgbtnDelete" runat="server" 
                                                        ToolTip="Delete Branch" 
                                                        AlternateText="Delete"
                                                        ImageUrl="../Images/delete_icon.png" 
                                                        OnClientClick="javascript:ConfirmDeleteBranch();"
                                                        CommandArgument ='<%# Eval("BranchID") %>'
                                                        CommandName="Delete" />
                                     </div>
                                    <div class="clear"></div>
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
