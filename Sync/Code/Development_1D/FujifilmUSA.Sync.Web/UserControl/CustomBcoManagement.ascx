<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomBcoManagement.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.CustomBcoManagement" %>
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

    .breakWord120 {
        max-width: 120px !important;
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

    .breakWord70 {
        max-width: 70px !important;
        word-break: break-all !important;
        word-wrap: break-word !important;
        vertical-align: top;
        line-height: 15px;
    }
</style>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript" language="javascript">
        function ConfirmDeleteSpotTarget() {
            if (confirm('<%= SpotTargetManagement_DeleteSpot_Confirmation %>')) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</telerik:RadCodeBlock>
<div class="admin_top">
    &nbsp;
</div>
<div class="admin_middle">
    <div class="head">
        <h1>
            <asp:Localize Text="Custom BCO Library Management" runat="server"
                meta:resourcekey="locHeaderCustomBcoLibraryManagementResource1"></asp:Localize></h1>

        <asp:LinkButton ID="likAdd" runat="server" class="button" meta:resourcekey="likAddResource1" OnClick="likAdd_Click">
            <span>
                <asp:Image ID="imgAdd" runat="server"
                    ImageUrl="../Images/new_project_icon.png" meta:resourcekey="imgAddResource1"  />

                <asp:Localize runat="server" ID="locSpotTarget" Text="New BCO Library"
                    meta:resourcekey="locNewBcoLibraryResource1"></asp:Localize>
            </span>
        </asp:LinkButton>

        <asp:LinkButton ID="likDelete" runat="server" class="button" meta:resourcekey="likAddResource1"  OnClientClick="javascript:return ConfirmDeleteSpotTarget()" OnClick="likDelete_Click">
            <span>
                <asp:Image ID="Image1" runat="server"
                    ImageUrl="../Images/delete_icon_LeftMenu.png" meta:resourcekey="imgAddResource1"/>

                <asp:Localize runat="server" ID="Localize2" Text="Delete"
                    meta:resourcekey="locDeleteResource1"></asp:Localize>
            </span>
        </asp:LinkButton>

    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">

        <asp:UpdatePanel ID="testPanel" runat="server">
            <ContentTemplate>
                <asp:Label runat="server" ID="lblMessage" Style="color: red;"></asp:Label>
                <br />
                <telerik:RadGrid ID="rgSpotTarget" AllowFilteringByColumn="True"
                    AllowSorting="True" AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False"
                    EnableLinqExpressions="False" OnNeedDataSource="rgSpotTarget_NeedDataSource" OnItemCommand="rgSpotTarget_ItemCommand"
                    Width="98%" CellSpacing="0"
                    GridLines="None">
                    <GroupingSettings CaseSensitive="False" />
                    <HeaderStyle Font-Bold="True" />
                    <PagerStyle Position="Top" AlwaysVisible="True" Font-Size="Small" />
                    <MasterTableView DataKeyNames="SpotTargetID,SpotTargetName">
                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                        </ExpandCollapseColumn>
                        <Columns>

                              <telerik:GridTemplateColumn Visible="false"
                                UniqueName="SpotTargetID" HeaderText="Spot Target ID" DataField="SpotTargetID"
                                SortExpression="SpotTargetID" FilterControlWidth="130px"
                                AutoPostBackOnFilter="True" CurrentFilterFunction="Contains"
                                FilterControlAltText="Filter Color Name column"
                                meta:resourcekey="GridTemplateColumnResource1">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpotTargetID" runat="server" Text='<%# Eval("SpotTargetID") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="130px" />
                                <ItemStyle Width="130px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn
                                UniqueName="DeleteConfirm" AllowFiltering="False"  DataField="IsActive"
                                meta:resourcekey="GridTemplateColumnResource7">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkDelete" />
                                </ItemTemplate>
                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn
                                UniqueName="SpotTargetName" HeaderText="Color Name" DataField="SpotTargetName"
                                SortExpression="SpotTargetName" FilterControlWidth="130px"
                                AutoPostBackOnFilter="True" CurrentFilterFunction="Contains"
                                FilterControlAltText="Filter Color Name column"
                                meta:resourcekey="GridTemplateColumnResource1">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpotTargetName" runat="server" Text='<%# Eval("SpotTargetName") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="130px" />
                                <ItemStyle Width="130px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                               <telerik:GridTemplateColumn
                                UniqueName="CompanyName" HeaderText="Company Name" DataField="Company.CompanyName"
                                SortExpression="Company.CompanyName" FilterControlWidth="130px"
                                AutoPostBackOnFilter="True" CurrentFilterFunction="Contains"
                                FilterControlAltText="Filter CompanyName column"
                                meta:resourcekey="GridTemplateColumnResource2">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("Company.CompanyName") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="130px" />
                                <ItemStyle Width="130px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn
                                UniqueName="Description" HeaderText="Description" DataField="Description"
                                SortExpression="Description" FilterControlWidth="130px"
                                AutoPostBackOnFilter="True" CurrentFilterFunction="Contains"
                                FilterControlAltText="Filter Description column"
                                meta:resourcekey="GridTemplateColumnResource3">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="130px" />
                                <ItemStyle Width="130px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                              <telerik:GridTemplateColumn
                                UniqueName="NumberOfColors" HeaderText="Number Of Colors" DataField="NumberOfColors"
                                SortExpression="NumberOfColors" FilterControlWidth="130px"
                                AutoPostBackOnFilter="True" CurrentFilterFunction="Contains"
                                FilterControlAltText="Filter Number Of Colors column"
                                meta:resourcekey="GridTemplateColumnResource4">
                                <ItemTemplate>
                                    <asp:Label ID="lblNumberOfColors" runat="server" Text='<%# Eval("NumberOfColors") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="130px" />
                                <ItemStyle Width="130px" Wrap="True" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="SpotTargetID"
                                UniqueName="Edit" FilterControlWidth="70px"
                                FilterControlAltText="Filter Edit column">
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
                                                    <asp:ImageButton ID="imgbtnEdit" runat="server" ToolTip="Edit Custom  BCO Library" AlternateText="Edit"
                                                        ImageUrl="../Images/edit_icon.png" CommandArgument='<%# Eval("SpotTargetID") %>'
                                                        CommandName="Edit" meta:resourcekey="imgbtnEditResource1" />
                                                </div>
                                                <div style="float: left;">
                                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Edit Custom  BCO Library" AlternateText="Delete"
                                                        ImageUrl="../Images/delete_icon.png" OnClientClick="javascript:return ConfirmDeleteSpotTarget()"
                                                        CommandArgument='<%# Eval("SpotTargetID") %>' CommandName="Delete"
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
    &nbsp;
</div>
<div class="clear">
</div>

