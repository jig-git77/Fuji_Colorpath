<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SourceProfileManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.SourceProfileManagement" %>
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
</style>
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
    <div style="margin-left: 10px;">
    <%--<asp:UpdatePanel ID="testPanel" runat="server">
        <ContentTemplate>--%>
            <telerik:RadGrid 
                ID="rgSourceProfile" 
                ShowHeader="true" 
                AllowFilteringByColumn="true"
                AllowSorting="true" 
                AllowPaging="True" 
                PageSize="5" 
                runat="server" 
                AutoGenerateColumns="False"
                EnableLinqExpressions="false" 
                OnNeedDataSource="rgSourceProfile_NeedDataSource"
                OnItemCommand="rgSourceProfile_ItemCommand" 
                Width="98%" 
                HeaderStyle-Font-Bold="true"
                 >

                <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />

                <MasterTableView DataKeyNames="SourceProfileID">
                    <Columns>

                        <telerik:GridTemplateColumn 
                            UniqueName="ProfileName" 
                            HeaderText="Profile Name"
                            DataField="ProfileName" 
                            SortExpression="ProfileName" 
                            ShowFilterIcon="true"
                            FilterControlWidth="290"
                            HeaderStyle-Width ="290" 
                            AllowFiltering="true" 
                            >
                            <ItemTemplate>
                                <asp:Label ID="lblProfileName" runat="server" Text='<%# Eval("ProfileName") %>'/>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn 
                            UniqueName="ProfileDesc" 
                            AllowFiltering="true" 
                            HeaderText="Profile Description"
                            DataField="ProfileDesc" 
                            SortExpression="ProfileDesc" 
                            ShowFilterIcon="true"
                            FilterControlWidth="290"
                            HeaderStyle-Width ="290">
                            <ItemTemplate>
                                    <asp:Label ID="lblProfileDesc" runat="server" Text='<%# Eval("ProfileDesc") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        
                        <telerik:GridTemplateColumn 
                            UniqueName="Profile" 
                            AllowFiltering="false" 
                            HeaderText="Profile"
                            DataField="" 
                            SortExpression="" 
                            HeaderStyle-Width ="70"
                            FilterControlWidth="70"
                            >
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnProfile" runat="server" ToolTip ="Download source profile" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"SourceProfileID") %>'
                                    CommandName="SourceProfile">
                                  <span>
                                     Click Me!
                                  </span>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn 
                                UniqueName="IsActive" 
                                AllowFiltering="false" 
                                HeaderText="IsActive?"
                                DataField="IsActive" 
                                HeaderStyle-Width ="40"
                                FilterControlWidth="40"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Image ID="imgIsActive" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("IsUsed")) ? "~/Images/checkbox_checked.png" : "~/Images/checkbox_unchecked.png"%>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn 
                                DataField="SourceProfileID" 
                                AllowFiltering="true" 
                                HeaderText=""
                                UniqueName="Edit">
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
                                                      <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Edit Source Profile" AlternateText="Edit"
                                                        ImageUrl="../Images/edit_icon.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"SourceProfileID") %>'
                                                        CommandName="Edit" />
                                                </div>
                                                <div style="float: left;">
                                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete Source Profile"
                                                        AlternateText="Delete" ImageUrl="../Images/delete_icon.png" OnClientClick="return ConfirmDeleteSourceProfile();"
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"SourceProfileID") %>'
                                                        CommandName="Delete" />
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
       <%-- </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lbtnProfile" />
        </Triggers>
    </asp:UpdatePanel>--%>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
