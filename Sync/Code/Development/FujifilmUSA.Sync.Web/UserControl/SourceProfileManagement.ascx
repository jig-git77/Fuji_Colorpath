<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SourceProfileManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.SourceProfileManagement" %>
<%@ Register TagName="Statusbar" TagPrefix="ucStatusbar" Src="Statusbar.ascx" %>
<script type="text/javascript" language="javascript">
    function ConfirmDeleteSourceProfile() {
        if (confirm('<%= SourceProfileManagement_DeleteSourceProfile_Confirmation %>'))
            return true;
        else
            return false;
    }
</script>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle">
    <div class="head">
        <h1>
            Source Profile Management</h1>
        <asp:LinkButton ID="likAdd" runat="server" class="button" OnClick="likAdd_Click">
            <span>
                <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/new_project_icon.png" />
                Add Source Profile</span>
        </asp:LinkButton>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px; margin-right: 15px;">
        <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="false" />
    </div>
    <asp:UpdatePanel ID="testPanel" runat="server">
        <ContentTemplate>
            <telerik:RadGrid ID="rgSourceProfile" ShowHeader="true" AllowFilteringByColumn="true"
                AllowSorting="false" AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False"
                EnableLinqExpressions="false" OnNeedDataSource="rgSourceProfile_NeedDataSource"
                OnItemCommand="rgSourceProfile_ItemCommand" Width="98%" HeaderStyle-Font-Bold="true">
                <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />
                <MasterTableView DataKeyNames="SourceProfileID">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="ProfileName" AllowFiltering="true" HeaderText="Profile Name"
                            DataField="ProfileName" SortExpression="ProfileName" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            <ItemTemplate>
                                <asp:Label ID="lblProfileName" runat="server" Text='<%# Eval("ProfileDesc") %>' Width="100px" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="ProfileDesc" AllowFiltering="true" HeaderText="Profile Description"
                            DataField="ProfileDesc" SortExpression="ProfileDesc" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            <ItemTemplate>
                                <asp:Label ID="lblProfileDesc" runat="server" Text='<%# Eval("ProfileName") %>' Width="350px" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="Profile" AllowFiltering="true" HeaderText="Profile"
                            DataField="" SortExpression="" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false">
                            <ItemTemplate>
                                <asp:Label ID="lblProfile" runat="server" Text="Click Me!" Font-Bold="true" Width="150px" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn DataField="IsActive" AllowFiltering="true" HeaderText="Active"
                            UniqueName="IsActive" SortExpression="IsActive" FilterControlWidth="100" ItemStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:Label ID="lblIsActive" runat="server" Text='<%# Convert.ToString(Eval("IsUsed")) %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn DataField="SourceProfileID" AllowFiltering="false" HeaderText="Edit"
                            UniqueName="Edit">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Edit Source Profile" AlternateText="Edit"
                                    ImageUrl="../Images/edit_icon.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"SourceProfileID") %>'
                                    CommandName="Edit" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn DataField="SourceProfileID" AllowFiltering="false" HeaderText="Delete"
                            UniqueName="Delete">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete Source Profile"
                                    AlternateText="Delete" ImageUrl="../Images/delete_icon.png" OnClientClick="return ConfirmDeleteSourceProfile();"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"SourceProfileID") %>'
                                    CommandName="Delete" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
