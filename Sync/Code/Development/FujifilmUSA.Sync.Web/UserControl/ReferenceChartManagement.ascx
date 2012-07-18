<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReferenceChartManagement.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.ReferenceChartManagement" %>

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
        <h1><asp:Localize Text="Reference Chart Management" runat="server" 
                meta:resourcekey="LocalizeResource1"></asp:Localize></h1>
        <asp:LinkButton ID="likAdd" runat="server" class="button" 
            OnClick="likAdd_Click" meta:resourcekey="likAddResource1">
            <span>
                <asp:Image ID="imgAdd" runat="server" 
            ImageUrl="../Images/new_project_icon.png" meta:resourcekey="imgAddResource1" />
                
<asp:Localize runat="server" ID="locAddReferenceChart" Text="Add Reference Chart" 
            meta:resourcekey="locAddReferenceChartResource1"></asp:Localize>
</span>
        </asp:LinkButton>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
    <asp:UpdatePanel ID="testPanel" runat="server">
        <ContentTemplate>
            <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                Visible="False" />
            <telerik:RadGrid ID="rgRefChart" AllowFilteringByColumn="True" 
                AllowSorting="True" AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False"
                EnableLinqExpressions="False" OnNeedDataSource="rgRefChart_NeedDataSource" 
                OnItemCommand="rgRefChart_ItemCommand" Width="98%" CellSpacing="0" 
                GridLines="None">
                <GroupingSettings CaseSensitive="False" />
                <HeaderStyle Font-Bold="True" />
                <PagerStyle Position="Top" AlwaysVisible="True" Font-Size="Small" />
                <MasterTableView DataKeyNames="ReferenceChartTypeId,ChartName">
                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                        Visible="True">
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                        Visible="True">
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridTemplateColumn 
                            UniqueName="ChartName" HeaderText="Chart Name" DataField="ChartName" 
                            SortExpression="ChartName" FilterControlWidth="150px" 
                            AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" 
                            FilterControlAltText="Filter ChartName column" 
                            meta:resourcekey="GridTemplateColumnResource1">                            
                            <ItemTemplate>
                                <asp:Label ID="lblChartName" runat="server" Text='<%# Eval("ChartName") %>' 
                                    meta:resourcekey="lblChartNameResource1"/>
                            </ItemTemplate>
                            <HeaderStyle Width="150px" />
                            <ItemStyle Width="150px" Wrap="True" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn 
                            UniqueName="NumOfRows" HeaderText="Num Of Rows" DataField="NumOfRows" 
                            SortExpression="NumOfRows" FilterControlWidth="70px" 
                            AutoPostBackOnFilter="True" CurrentFilterFunction="GreaterThanOrEqualTo" 
                            FilterControlAltText="Filter NumOfRows column" 
                            meta:resourcekey="GridTemplateColumnResource2">
                            <ItemTemplate>
                                    <asp:Label ID="lblNumOfRows" runat="server" Text='<%# Eval("NumOfRows") %>' 
                                        meta:resourcekey="lblNumOfRowsResource1" />
                            </ItemTemplate>
                            <HeaderStyle Width="70px" />
                            <ItemStyle Width="70px" Wrap="True" />
                        </telerik:GridTemplateColumn>                        
                         <telerik:GridTemplateColumn 
                            UniqueName="NumOfCols" HeaderText="Num Of Cols" DataField="NumOfCols" 
                            SortExpression="NumOfCols" FilterControlWidth="70px" 
                            AutoPostBackOnFilter="True" CurrentFilterFunction="GreaterThanOrEqualTo" 
                            FilterControlAltText="Filter NumOfCols column" 
                            meta:resourcekey="GridTemplateColumnResource3">
                            <ItemTemplate>
                                    <asp:Label ID="lblNumOfCols" runat="server" Text='<%# Eval("NumOfCols") %>' 
                                        meta:resourcekey="lblNumOfColsResource1" />
                            </ItemTemplate>
                             <HeaderStyle Width="70px" />
                             <ItemStyle Width="70px" Wrap="True" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn 
                            UniqueName="TotalPatches" HeaderText="Total Patches" 
                            DataField="TotalPatches" SortExpression="TotalPatches" 
                            FilterControlWidth="70px" AutoPostBackOnFilter="True" 
                            CurrentFilterFunction="GreaterThanOrEqualTo" 
                            FilterControlAltText="Filter TotalPatches column" 
                            meta:resourcekey="GridTemplateColumnResource4">
                            <ItemTemplate>
                                    <asp:Label ID="lblTotalPatches" runat="server" 
                                        Text='<%# Eval("TotalPatches") %>' 
                                        meta:resourcekey="lblTotalPatchesResource1" />
                            </ItemTemplate>
                            <HeaderStyle Width="70px" />
                            <ItemStyle Width="70px" Wrap="True" />
                        </telerik:GridTemplateColumn>
                         <telerik:GridTemplateColumn 
                            UniqueName="InstrumentName" HeaderText="Instrument Name" 
                            DataField="InstrumentType.InstrumentName" 
                            SortExpression="InstrumentType.InstrumentName" FilterControlWidth="90px" 
                            AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" 
                            FilterControlAltText="Filter InstrumentName column" 
                            meta:resourcekey="GridTemplateColumnResource5">
                            <ItemTemplate>
                                    <asp:Label ID="lblInstrumentName" runat="server" 
                                        Text='<%# Eval("InstrumentType.InstrumentName") %>' 
                                        meta:resourcekey="lblInstrumentNameResource1" />
                            </ItemTemplate>
                             <HeaderStyle Width="90px" />
                             <ItemStyle Width="90px" Wrap="True" />
                        </telerik:GridTemplateColumn>
                         <telerik:GridTemplateColumn 
                            UniqueName="ProjectTypeName" HeaderText="Project Type" 
                            DataField="ProjectType.ProjectTypeName" 
                            SortExpression="ProjectType.ProjectTypeName" FilterControlWidth="70px" 
                            AutoPostBackOnFilter="True" CurrentFilterFunction="Contains" 
                            FilterControlAltText="Filter ProjectTypeName column" 
                            meta:resourcekey="GridTemplateColumnResource6">
                            <ItemTemplate>
                                    <asp:Label ID="lblProjectTypeName" runat="server" 
                                        Text='<%# Eval("ProjectType.ProjectTypeName") %>' 
                                        meta:resourcekey="lblProjectTypeNameResource1" />
                            </ItemTemplate>
                             <HeaderStyle Width="70px" />
                             <ItemStyle Width="70px" Wrap="True" />
                        </telerik:GridTemplateColumn>                     
                        <telerik:GridTemplateColumn 
                                UniqueName="IsActive" AllowFiltering="False" HeaderText="IsActive?" DataField="IsActive"
                                FilterControlWidth="40px" 
                            FilterControlAltText="Filter IsActive column" 
                            meta:resourcekey="GridTemplateColumnResource7">
                                <ItemTemplate>
                                    <asp:Image ID="imgIsActive" runat="server" 
                                        ImageUrl='<%# Convert.ToBoolean(Eval("IsUsed")) ? "~/Images/checkbox_checked.png" : "~/Images/checkbox_unchecked.png" %>' 
                                        meta:resourcekey="imgIsActiveResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="40px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn 
                                DataField="ReferenceChartTypeId" UniqueName="Edit" 
                            FilterControlAltText="Filter Edit column" 
                            meta:resourcekey="GridTemplateColumnResource8">
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
                                            <div >
                                                <div style="float: left; padding-right: 10px;">
                                                      <asp:ImageButton ID="imgbtnUpdate" runat="server" 
                                                          ToolTip="Edit Reference Chart" AlternateText="Edit"
                                                        ImageUrl="../Images/edit_icon.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ReferenceChartTypeId") %>'
                                                        CommandName="Edit" meta:resourcekey="imgbtnUpdateResource1" />
                                                </div>                                             
                                                
                                            </div>
                                        </center>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Width="40px" />
                         </telerik:GridTemplateColumn>                       
                    </Columns>
                    <EditFormSettings>
                        <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                        </EditColumn>
                    </EditFormSettings>
                    <PagerStyle AlwaysVisible="True" />
                </MasterTableView>
                <FilterMenu EnableImageSprites="False">
                </FilterMenu>
            </telerik:RadGrid>
        </ContentTemplate>      
    </asp:UpdatePanel>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>

