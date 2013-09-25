<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrintStandardTypeManagement.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.PrintStandardTypeManagement" %>
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
    
     .breakWord290
     {
            max-width: 290px !important;
            word-break: break-all !important;
            word-wrap: break-word !important;
            vertical-align: top;
            line-height: 15px;
     }
    
</style>
<script type="text/javascript" language="javascript">
    function ConfirmDeletePrintStandardType() {
        if (confirm('<%= PrintStandardTypeManagement_DeletePrintStandardType_Confirmation %>'))
            return true;
        else
            return false;
    }
</script>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle">
    <div class="head">
        <h1><asp:Localize runat="server" Text="Print Standard Type Management" 
                ID="locPrintStandardTypeMgmt" 
                meta:resourcekey="locPrintStandardTypeMgmtResource1"></asp:Localize></h1>
        <asp:LinkButton ID="likAdd" runat="server" class="button" 
            OnClick="likAdd_Click" meta:resourcekey="likAddResource1">
            <span>
                <asp:Image ID="imgAdd" runat="server" 
            ImageUrl="../Images/new_project_icon.png" meta:resourcekey="imgAddResource1" />
                
<asp:Localize Text="Add PrintStandardType" runat="server" ID="locAddPrintStandardType" 
            meta:resourcekey="locAddPrintStandardTypeResource1"></asp:Localize>
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
                ID="rgPrintStandardType" 
                AllowFilteringByColumn="True"
                AllowSorting="True" 
                AllowPaging="True" 
                PageSize="5" 
                runat="server" 
                AutoGenerateColumns="False"
                EnableLinqExpressions="False" 
                OnNeedDataSource="rgPrintStandardType_NeedDataSource"
                OnItemCommand="rgPrintStandardType_ItemCommand" 
                Width="98%" 
                HeaderStyle-Font-Bold="true" CellSpacing="0" GridLines="None">
                <GroupingSettings CaseSensitive="false" />                

                <HeaderStyle Font-Bold="True"></HeaderStyle>

                <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />

                <MasterTableView DataKeyNames="PrintStandardTypeID,PrintStandard">
                  
                    <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

                    <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                      <Columns>

                        <telerik:GridTemplateColumn 
                            UniqueName="PrintStandard" 
                            HeaderText="Print Standard Name"
                            DataField="PrintStandard" 
                            SortExpression="PrintStandard" 
                            ShowFilterIcon="true"
                            FilterControlWidth="290"
                            HeaderStyle-Width ="290" 
                            AllowFiltering="true" 
                            ItemStyle-CssClass="breakWord290" ItemStyle-Width="290" 
                            ItemStyle-Wrap="true" AutoPostBackOnFilter="true" 
                            CurrentFilterFunction="Contains" meta:resourcekey="GridTemplateColumnResource1">
                            <ItemTemplate>
                                <asp:Label ID="lblPrintStandard" runat="server" Text='<%# Eval("PrintStandard") %>' 
                                    meta:resourcekey="lblPrintStandardResource1"/>
                            </ItemTemplate>

                            <HeaderStyle Width="290px"></HeaderStyle>

                            <ItemStyle Wrap="True" CssClass="breakWord290" Width="290px"></ItemStyle>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn 
                            UniqueName="SourceProfile" 
                            AllowFiltering="true" 
                            HeaderText="Source Profile"
                            DataField="SourceProfile.ProfileName" 
                            SortExpression="SourceProfile.ProfileName" 
                            ShowFilterIcon="true"
                            FilterControlWidth="290"
                            HeaderStyle-Width ="290"
                            ItemStyle-CssClass="breakWord290" ItemStyle-Width="290" 
                            ItemStyle-Wrap="true" AutoPostBackOnFilter="true" 
                            CurrentFilterFunction="Contains" meta:resourcekey="GridTemplateColumnResource2">
                            <ItemTemplate>
                                    <asp:Label ID="lblSourceProfile" runat="server" Text='<%# Eval("SourceProfile.ProfileName") %>' 
                                        meta:resourcekey="lblSourceProfileResource1" />
                            </ItemTemplate>

                            <HeaderStyle Width="290px"></HeaderStyle>

                            <ItemStyle Wrap="True" CssClass="breakWord290" Width="290px"></ItemStyle>
                        </telerik:GridTemplateColumn>
                        
                    

                        <telerik:GridTemplateColumn 
                                UniqueName="IsSync1DType" 
                                AllowFiltering="false" 
                                HeaderText="IsSync1DType?"
                                DataField="IsSync1DType" 
                                HeaderStyle-Width ="40"
                                FilterControlWidth="40"
                                ItemStyle-HorizontalAlign="Center" 
                            meta:resourcekey="GridTemplateColumnResource4">
                                <ItemTemplate>
                                    <asp:Image ID="imgIsSync1DType" runat="server" 
                                        ImageUrl='<%# Convert.ToBoolean(Eval("IsSync1DType")) ? "~/Images/checkbox_checked.png" : "~/Images/checkbox_unchecked.png" %>' 
                                        meta:resourcekey="imgIsSync1DTypeResource1" />
                                </ItemTemplate>

                                <HeaderStyle Width="40px"></HeaderStyle>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn 
                                DataField="PrintStandardTypeID" 
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
                                                      <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Edit PrintStandardType" AlternateText="Edit"
                                                        ImageUrl="../Images/edit_icon.png" CommandArgument='<%# Eval("PrintStandardTypeID") %>'
                                                        CommandName="Edit" meta:resourcekey="imgbtnUpdateResource1" />
                                                </div>
                                                <div style="float: left;">
                                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete PrintStandardType"
                                                        AlternateText="Delete" ImageUrl="../Images/delete_icon.png" OnClientClick="return ConfirmDeletePrintStandardType();"
                                                        CommandArgument='<%# Eval("PrintStandardTypeID") %>' CommandName="Delete" 
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
