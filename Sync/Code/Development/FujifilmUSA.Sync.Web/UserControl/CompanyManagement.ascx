<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompanyManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.CompanyManagement" %>
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
        <h1>
            Company Management</h1>
        <asp:LinkButton ID="likAdd" runat="server" class="button" OnClick="likAdd_Click">
            <span>
                <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/new_project_icon.png" />
                Add Company</span>
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
        <telerik:RadGrid ID="rgCompany" ShowHeader="true" AllowFilteringByColumn="true" AllowSorting="false"
            AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False" EnableLinqExpressions="false"
            OnNeedDataSource="rgCompany_NeedDataSource" OnItemCommand="rgCompany_ItemCommand"
            OnItemDataBound="rgCompany_ItemDataBound" Width="98%" HeaderStyle-Font-Bold="true" >
            <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />
            <MasterTableView DataKeyNames="CompanyId,CompanyName,RegionId,IsActive">
                <Columns>
                    <telerik:GridTemplateColumn DataField="CompanyName" AllowFiltering="true" HeaderText="Company Name"
                        UniqueName="CompanyName" SortExpression="CompanyName" FilterControlWidth="300"
                        ItemStyle-Wrap="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true">
                        <ItemTemplate>
                            <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="Region.RegionName" AllowFiltering="true" HeaderText="Region Name"
                        UniqueName="RegionName" SortExpression="Region.RegionName" FilterControlWidth="200"
                        ItemStyle-Wrap="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true">
                        <ItemTemplate>
                            <asp:Label ID="lblRegionName" runat="server" Text='<%# Eval("Region.RegionName") %>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn 
                                    UniqueName="IsActive" 
                                    AllowFiltering="false" 
                                    HeaderText="IsActive?"
                                    DataField="IsActive" 
                                    >
                                    <ItemTemplate>
                                        <asp:Image ID="imgIsActive"  runat="server" ImageUrl=<%# Convert.ToBoolean(Eval("IsActive")) ? "~/Images/checkbox_checked.png" : "~/Images/checkbox_unchecked.png"%> />
                                        
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn 
                        DataField="CompanyId" 
                        AllowFiltering="true" 
                        HeaderText=""
                        UniqueName="Edit"
                        HeaderStyle-Width="100">
                        <FilterTemplate>
                                <asp:LinkButton ID="lbtnClearFillter" runat="server" CssClass="gray_bt" CommandName="Fillter">
                                                    <span> Clear Fillter </span>
                                </asp:LinkButton>
                                                
                        </FilterTemplate>
                        <ItemTemplate>
                          

                            <div>
                                <center>
                                <div style="width: 45px;">     
                                    <div style="float:left; padding-right: 10px;">
                                        <asp:ImageButton ID="imgbtnEdit" runat="server" ToolTip="Edit Company" AlternateText="Edit"
                                        ImageUrl="../Images/edit_icon.png" 
                                        CommandArgument ='<%# Eval("CompanyId") %>'
                                        CommandName="Edit" />
                                    </div>
                                    <div style="float:left;" >
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete Company" AlternateText="Delete"
                                        ImageUrl="../Images/delete_icon.png" 
                                        OnClientClick="javascript:return ConfirmDeleteCompany();"
                                        CommandArgument ='<%# Eval("CompanyId") %>'
                                        CommandName="Delete" />
                                     </div>
                                    <div class="clear"></div>
                            </div>
                            </center>
                            </div>

                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <%--<telerik:GridTemplateColumn DataField="CompanyId" AllowFiltering="false" HeaderText=""
                        UniqueName="Delete">
                        <ItemTemplate>
                          
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>--%>
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
