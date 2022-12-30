<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MToolVersionManagement.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.MToolVersionManagement" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<style type="text/css">
    div.RadGrid .rgRow {
        background: #F7F7F7;
    }

    div.RadGrid .rgAltRow {
        background: #F7F7F7;
    }

    .rgPager table {
        border-top: 0px;
        border-bottom: 1px solid #000000;
        border-spacing: 1pt;
        width: 100%;
    }

    .RadGrid_Default .rgRow a, .RadGrid_Default .rgAltRow a, .RadGrid_Default .rgEditRow a, .RadGrid_Default tr.rgEditRow a, .RadGrid_Default tr.rgHoveredRow a, .RadGrid_Default tr.rgActiveRow a, .RadGrid_Default .rgFooter a, .RadGrid_Default .rgEditForm a {
        color: #003CA9;
    }

    .RadGrid_Default td.rgPagerCell {
        -moz-border-bottom-colors: none;
        -moz-border-image: none;
        -moz-border-left-colors: none;
        -moz-border-right-colors: none;
        -moz-border-top-colors: none;
        border-color: #828282 #EEEEEE #EEEEEE;
        border-style: solid;
        border-width: 0px 0 0px 0px;
    }

    .RadGrid_Default .rgRow td, .RadGrid_Default .rgAltRow td, .RadGrid_Default .rgEditRow td, .RadGrid_Default .rgFooter td {
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

    .RadGrid_Default th.rgSorted {
        background-color: #F1F1F1;
        background-position: 0px;
    }

    .breakWord150 {
        max-width: 150px !important;
        word-break: break-all !important;
        word-wrap: break-word !important;
        vertical-align: top;
        line-height: 15px;
    }

    .breakWord130 {
        max-width: 130px !important;
        word-break: break-all !important;
        word-wrap: break-word !important;
        vertical-align: top;
        line-height: 15px;
    }

    .breakWord100 {
        max-width: 100px !important;
        word-break: break-all !important;
        word-wrap: break-word !important;
        vertical-align: top;
        line-height: 15px;
    }
</style>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript" language="javascript">
        function ConfirmDeleteMToolVersion() {
            if (confirm('<%= MToolVersionManagement_DeleteMToolVersion_Confirmation %>'))
                return true;
            else
                return false;
        }
    </script>
</telerik:RadCodeBlock>
<div class="admin_top">
    &nbsp;
</div>
<div class="admin_middle">
    <div class="head">
        <h1>
            <asp:Localize runat="server" ID="locMToolVersionMgmt" Text="MTool Version Management" meta:resourcekey="locMtoolManagementResource1"></asp:Localize></h1>
        <asp:LinkButton ID="likAdd" runat="server" class="button"
            OnClick="likAdd_Click" meta:resourcekey="likAddResource1">
            <span>
                <asp:Image ID="imgAdd" runat="server"
                    ImageUrl="../Images/new_project_icon.png" meta:resourcekey="imgAddResource1" />

                <asp:Localize runat="server" Text="Add MTool Version" ID="locMToolVersion" meta:resourcekey="locAddHeaderMtoolVersionResource1"></asp:Localize>
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
                <asp:HiddenField ID="hdSelectMToolVersionId" runat="server" Value="0" />
                <telerik:RadGrid ID="rgMToolList" AllowFilteringByColumn="True" AllowSorting="True"
                    AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False" EnableLinqExpressions="False"
                    OnNeedDataSource="rgMToolList_NeedDataSource" OnItemCommand="rgMToolList_ItemCommand"
                    Width="98%" CellSpacing="0" GridLines="None"
                    meta:resourcekey="rgMToolListResource1">
                    <GroupingSettings CaseSensitive="False" />
                    <HeaderStyle Font-Bold="True" />
                    <PagerStyle Position="Top" AlwaysVisible="True" Font-Size="Small" />
                    <MasterTableView DataKeyNames="OSPlatformDetailsID,OSPlatformID,LatestVersion,MinReqVersion,URLlocation,UpdateInfo,IsUsed">
                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column"
                            Visible="True">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column"
                            Visible="True">
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn DataField="OSPlatformName" HeaderText="OS Platform Name"
                                UniqueName="OSPlatformName" SortExpression="OSPlatformName"
                                FilterControlWidth="130px" AutoPostBackOnFilter="True"
                                CurrentFilterFunction="Contains"
                                FilterControlAltText="Filter OSPlatformName column"
                                meta:resourcekey="GridTemplateColumnResource1">
                                <ItemTemplate>
                                    <asp:Label ID="lblOSPlatformName" runat="server" Text='<%# Eval("OSPlatformName") %>'
                                        meta:resourcekey="lblOSPlatformName1" />
                                </ItemTemplate>
                                <HeaderStyle Width="130px" />
                                <ItemStyle CssClass="breakWord130" Width="130px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="LatestVersion" HeaderText="LatestVersion"
                                UniqueName="LatestVersion" SortExpression="LatestVersion" FilterControlWidth="90px"
                                AutoPostBackOnFilter="True" CurrentFilterFunction="Contains"
                                FilterControlAltText="Filter LatestVersion column"
                                meta:resourcekey="GridTemplateColumnResource2">
                                <ItemTemplate>
                                    <asp:Label ID="lblLatestVersion" runat="server" Text='<%# Eval("LatestVersion") %>'/>
                                </ItemTemplate>
                                <HeaderStyle Width="90px" />
                                <ItemStyle CssClass="breakWord130" Width="90px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="MinReqVersion" HeaderText="MinReqVersion"
                                UniqueName="MinReqVersion" SortExpression="MinReqVersion"
                                FilterControlWidth="70px" AutoPostBackOnFilter="True"
                                CurrentFilterFunction="Contains" FilterControlAltText="Filter MinReqVersion column"
                                meta:resourcekey="GridTemplateColumnResource3">
                                <ItemTemplate>
                                    <asp:Label ID="lblMinReqVersion" runat="server" Text='<%# Eval("MinReqVersion") %>'/>
                                </ItemTemplate>
                                <HeaderStyle Width="70px" />
                                <ItemStyle CssClass="breakWord100" Width="70px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="URLlocation"
                                HeaderText="URLlocation" UniqueName="URLlocation"
                                SortExpression="URLlocation" FilterControlWidth="200px"
                                AutoPostBackOnFilter="True" CurrentFilterFunction="Contains"
                                FilterControlAltText="Filter URLlocation column"
                                meta:resourcekey="GridTemplateColumnResource4">
                                <ItemTemplate>
                                    <asp:Label ID="lblURLlocation" runat="server"
                                        Text='<%# Eval("URLlocation") %>'
                                        meta:resourcekey="lblURLlocationResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="110px" />
                                <ItemStyle CssClass="breakWord150" Width="110px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="UpdateInfo"
                                HeaderText="UpdateInfo" UniqueName="UpdateInfo" SortExpression="UpdateInfo"
                                FilterControlWidth="50px" AutoPostBackOnFilter="True"
                                CurrentFilterFunction="Contains" FilterControlAltText="Filter UpdateInfo column"
                                meta:resourcekey="GridTemplateColumnResource5">
                                <ItemTemplate>
                                    <asp:Label ID="lblUpdateInfo" runat="server"
                                        Text='<%# Eval("UpdateInfo") %>' meta:resourcekey="lblUpdateInfoResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="250px" />
                                <ItemStyle CssClass="breakWord150" Width="250px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="IsUsed" AllowFiltering="False"
                                HeaderText="IsUsed?" FilterControlWidth="50px" DataField="IsUsed"
                                FilterControlAltText="Filter IsUsed column" 
                                meta:resourcekey="GridTemplateColumnResource6">
                                <ItemTemplate>
                                    <asp:Image ID="imgIsUsed" runat="server"
                                        ImageUrl='<%# Convert.ToBoolean(Eval("IsUsed")) ? "~/Images/checkbox_checked.png" : "~/Images/checkbox_unchecked.png" %>'
                                        meta:resourcekey="imgIsUsedResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="OSPlatformDetailsID"
                                UniqueName="Edit" FilterControlAltText="Filter Edit column"
                                meta:resourcekey="GridTemplateColumnResource7">
                                <FilterTemplate>
                                    <center>
                                        <asp:LinkButton ID="lbtnClearFillter" runat="server" ToolTip="Clear Fillter" 
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
                                                    <asp:ImageButton ID="imgbtnEdit" runat="server" ToolTip="Edit MToolVersion" AlternateText="Edit"
                                                        ImageUrl="../Images/edit_icon.png" CommandArgument='<%# Eval("OSPlatformDetailsID") %>'
                                                        CommandName="Edit" meta:resourcekey="imgbtnEditResource1" />
                                                </div>
                                                <div style="float: left;">
                                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete MToolVersion" AlternateText="Delete"
                                                        ImageUrl="../Images/delete_icon.png" OnClientClick="javascript:return ConfirmDeleteMToolVersion();"
                                                        CommandArgument='<%# Eval("OSPlatformDetailsID") %>' CommandName="Delete" 
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;
</div>
<div class="clear">
</div>
