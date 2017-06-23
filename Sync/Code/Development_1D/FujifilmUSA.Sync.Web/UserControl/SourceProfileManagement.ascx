<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="SourceProfileManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.SourceProfileManagement" %>
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
    
     .breakWord150
     {
            max-width: 150px !important;
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
        <h1><asp:Localize runat="server" Text="Source Profile Management" 
                ID="locSourceProfileMgmt" meta:resourcekey="locSourceProfileMgmtResource1"></asp:Localize></h1>
        <asp:LinkButton ID="likAdd" runat="server" class="button" 
            OnClick="likAdd_Click" meta:resourcekey="likAddResource1">
            <span>
                <asp:Image ID="imgAdd" runat="server" 
            ImageUrl="../Images/new_project_icon.png" meta:resourcekey="imgAddResource1" />
                
<asp:Localize Text="Add Source Profile" runat="server" ID="locAddSourceProfile" 
            meta:resourcekey="locAddSourceProfileResource1"></asp:Localize>
</span>
        </asp:LinkButton>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
    <%--<asp:UpdatePanel ID="testPanel" runat="server">
        <ContentTemplate>--%>
            <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
            Visible="False" />
            <telerik:RadGrid 
                ID="rgSourceProfile" 
                AllowFilteringByColumn="True"
                AllowSorting="True" 
                AllowPaging="True" 
                PageSize="5" 
                runat="server" 
                AutoGenerateColumns="False"
                EnableLinqExpressions="False" 
                OnNeedDataSource="rgSourceProfile_NeedDataSource"
                OnItemCommand="rgSourceProfile_ItemCommand" 
                Width="98%" 
                HeaderStyle-Font-Bold="true" CellSpacing="0" GridLines="None">
                <GroupingSettings CaseSensitive="false" />                

<HeaderStyle Font-Bold="True"></HeaderStyle>

                <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />

                <MasterTableView DataKeyNames="SourceProfileID,ProfileName">
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

<ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                    <Columns>

                        <telerik:GridTemplateColumn 
                            UniqueName="ProfileName" 
                            HeaderText="Profile Name"
                            DataField="ProfileName" 
                            SortExpression="ProfileName" 
                            ShowFilterIcon="true"
                            FilterControlWidth="150"
                            HeaderStyle-Width ="150" 
                            AllowFiltering="true" 
                            ItemStyle-CssClass="breakWord150" ItemStyle-Width="150" 
                            ItemStyle-Wrap="true" AutoPostBackOnFilter="true" 
                            CurrentFilterFunction="Contains" meta:resourcekey="GridTemplateColumnResource1">
                            <ItemTemplate>
                                <asp:Label ID="lblProfileName" runat="server" Text='<%# Eval("ProfileName") %>' 
                                    meta:resourcekey="lblProfileNameResource1"/>
                            </ItemTemplate>
                            <HeaderStyle Width="150px"></HeaderStyle>
                            <ItemStyle Wrap="True" CssClass="breakWord150" Width="150px"></ItemStyle>
                        </telerik:GridTemplateColumn>
                        
                        <telerik:GridTemplateColumn 
                            UniqueName="Region" 
                            HeaderText="Region Name"
                            DataField="Region.RegionName" 
                            SortExpression="Region.RegionName" 
                            ShowFilterIcon="true"
                            FilterControlWidth="70"
                            HeaderStyle-Width ="70" 
                            AllowFiltering="true" 
                            ItemStyle-CssClass="breakWord70" ItemStyle-Width="70" 
                            ItemStyle-Wrap="true" AutoPostBackOnFilter="true" 
                            CurrentFilterFunction="Contains" meta:resourcekey="GridTemplateColumnResource6">
                            <ItemTemplate>
                                <asp:Label ID="lblRegion" runat="server" Text='<%# Eval("Region.RegionName") %>' />
                            </ItemTemplate>
                            <HeaderStyle Width="70px"></HeaderStyle>
                            <ItemStyle Wrap="True" CssClass="breakWord150" Width="70px"></ItemStyle>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn 
                            UniqueName="CompanyName" 
                            HeaderText="Company Name"
                            DataField="Company.CompanyName" 
                            SortExpression="Company.CompanyName" 
                            ShowFilterIcon="true"
                            FilterControlWidth="150"
                            HeaderStyle-Width ="150" 
                            AllowFiltering="true" 
                            ItemStyle-CssClass="breakWord150" ItemStyle-Width="150" 
                            ItemStyle-Wrap="true" AutoPostBackOnFilter="true" 
                            CurrentFilterFunction="Contains" meta:resourcekey="GridTemplateColumnResource7">
                            <ItemTemplate>
                                <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("Company.CompanyName") %>' 
                                   meta:resourcekey="lblCompanyNameResource1"/>
                            </ItemTemplate>
                            <HeaderStyle Width="150px"></HeaderStyle>
                            <ItemStyle Wrap="True" CssClass="breakWord150" Width="150px"></ItemStyle>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn 
                            UniqueName="ProfileDesc" 
                            AllowFiltering="true" 
                            HeaderText="Profile Description"
                            DataField="ProfileDesc" 
                            SortExpression="ProfileDesc" 
                            ShowFilterIcon="true"
                            FilterControlWidth="150"
                            HeaderStyle-Width ="150"
                            ItemStyle-CssClass="breakWord150" ItemStyle-Width="150" 
                            ItemStyle-Wrap="true" AutoPostBackOnFilter="true" 
                            CurrentFilterFunction="Contains" meta:resourcekey="GridTemplateColumnResource2">
                            <ItemTemplate>
                                    <asp:Label ID="lblProfileDesc" runat="server" Text='<%# Eval("ProfileDesc") %>' 
                                        meta:resourcekey="lblProfileDescResource1" />
                            </ItemTemplate>

<HeaderStyle Width="150px"></HeaderStyle>

<ItemStyle Wrap="True" CssClass="breakWord150" Width="150px"></ItemStyle>
                        </telerik:GridTemplateColumn>
                        
                        <telerik:GridTemplateColumn 
                            UniqueName="Profile" 
                            AllowFiltering="false" 
                            HeaderText="Profile"
                            DataField="" 
                            SortExpression="" 
                            HeaderStyle-Width ="70"
                            FilterControlWidth="70" meta:resourcekey="GridTemplateColumnResource3"
                            >
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnProfile" runat="server" ToolTip ="Download source profile" 
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"SourceProfileID") %>'
                                    CommandName="SourceProfile">
                                  <span>
                                    <asp:Localize Text="Click Me!" runat="server" ID="locClickMe" meta:resourcekey="locClickMeResource1"></asp:Localize>
                                  </span>
                                </asp:LinkButton>
                            </ItemTemplate>

<HeaderStyle Width="70px"></HeaderStyle>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn 
                                UniqueName="IsActive" 
                                AllowFiltering="false" 
                                HeaderText="IsActive?"
                                DataField="IsActive" 
                                HeaderStyle-Width ="40"
                                FilterControlWidth="40"
                                ItemStyle-HorizontalAlign="Center" 
                            meta:resourcekey="GridTemplateColumnResource4">
                                <ItemTemplate>
                                    <asp:Image ID="imgIsActive" runat="server" 
                                        ImageUrl='<%# Convert.ToBoolean(Eval("IsUsed")) ? "~/Images/checkbox_checked.png" : "~/Images/checkbox_unchecked.png" %>' 
                                        meta:resourcekey="imgIsActiveResource1" />
                                </ItemTemplate>

<HeaderStyle Width="40px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn 
                                DataField="SourceProfileID" 
                                AllowFiltering="true" 
                                HeaderText=""
                                UniqueName="Edit" meta:resourcekey="GridTemplateColumnResource5">
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
                                                      <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Edit Source Profile" AlternateText="Edit"
                                                        ImageUrl="../Images/edit_icon.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"SourceProfileID") %>'
                                                        CommandName="Edit" meta:resourcekey="imgbtnUpdateResource1" />
                                                </div>
                                                <div style="float: left;">
                                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete Source Profile"
                                                        AlternateText="Delete" ImageUrl="../Images/delete_icon.png" OnClientClick="return ConfirmDeleteSourceProfile();"
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"SourceProfileID") %>'
                                                        CommandName="Delete" Visible="False" 
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
