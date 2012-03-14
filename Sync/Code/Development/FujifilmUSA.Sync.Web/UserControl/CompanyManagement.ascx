<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompanyManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.CompanyManagement" %>
<%@ Register TagName="Statusbar" TagPrefix="ucStatusbar" Src="Statusbar.ascx" %>

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
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rgCompany">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgCompany" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Vista" />
        <telerik:RadGrid ID="rgCompany" ShowHeader="true" AllowFilteringByColumn="true" AllowSorting="false"
            AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False" EnableLinqExpressions="false"
            OnNeedDataSource="rgCompany_NeedDataSource" OnItemCommand="rgCompany_ItemCommand"
            OnItemDataBound="rgCompany_ItemDataBound" Width="98%" >
            <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />
            <MasterTableView DataKeyNames="CompanyId,CompanyName,RegionId">
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
                    <telerik:GridTemplateColumn DataField="CompanyId" AllowFiltering="false" HeaderText=""
                        UniqueName="Edit">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnEdit" runat="server" ToolTip="Edit Company" AlternateText="Edit"
                                ImageUrl="../Images/edit_icon.png" 
                                CommandArgument ='<%# Eval("CompanyId") %>'
                                CommandName="Edit" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CompanyId" AllowFiltering="false" HeaderText=""
                        UniqueName="Delete">
                        <ItemTemplate>
                             <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server" >
                                <script type="text/javascript" language="javascript">
                                    function ConfirmDeleteCompany() {
                                        if (confirm('<%= CompanyManagement_DeleteCompany_Confirmation %>'))
                                            return true;
                                        else
                                            return false;
                                    }
                                </script>
                            </telerik:RadCodeBlock>
                            <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete Company" AlternateText="Delete"
                                ImageUrl="../Images/delete_icon.png" 
                                OnClientClick="javascript:ConfirmDeleteCompany();"
                                CommandArgument ='<%# Eval("CompanyId") %>'
                                CommandName="Delete" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
