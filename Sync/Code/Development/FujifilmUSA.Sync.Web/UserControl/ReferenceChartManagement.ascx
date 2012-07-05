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
        <h1>
            Reference Chart Management</h1>
        <asp:LinkButton ID="likAdd" runat="server" class="button" OnClick="likAdd_Click">
            <span>
                <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/new_project_icon.png" />
                Add Reference Chart</span>
        </asp:LinkButton>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
    <asp:UpdatePanel ID="testPanel" runat="server">
        <ContentTemplate>
            <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" Visible="false" />
            <telerik:RadGrid ID="rgRefChart" ShowHeader="true" AllowFilteringByColumn="true" AllowSorting="true" AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False"
                EnableLinqExpressions="false" OnNeedDataSource="rgRefChart_NeedDataSource" OnItemCommand="rgRefChart_ItemCommand" Width="98%" HeaderStyle-Font-Bold="true">
                <GroupingSettings CaseSensitive="false" />
                <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />
                <MasterTableView DataKeyNames="ReferenceChartTypeId,ChartName">
                    <Columns>
                        <telerik:GridTemplateColumn 
                            UniqueName="ChartName" HeaderText="Chart Name" DataField="ChartName" SortExpression="ChartName" ShowFilterIcon="true" FilterControlWidth="150"
                            HeaderStyle-Width ="150" AllowFiltering="true" ItemStyle-Width="150" ItemStyle-Wrap="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">                            
                            <ItemTemplate>
                                <asp:Label ID="lblChartName" runat="server" Text='<%# Eval("ChartName") %>'/>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn 
                            UniqueName="NumOfRows" AllowFiltering="true" HeaderText="Num Of Rows" DataField="NumOfRows" SortExpression="NumOfRows" 
                            ShowFilterIcon="true" FilterControlWidth="70" HeaderStyle-Width ="70" ItemStyle-Width="70" ItemStyle-Wrap="true" AutoPostBackOnFilter="true" CurrentFilterFunction="GreaterThanOrEqualTo">
                            <ItemTemplate>
                                    <asp:Label ID="lblNumOfRows" runat="server" Text='<%# Eval("NumOfRows") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>                        
                         <telerik:GridTemplateColumn 
                            UniqueName="NumOfCols" AllowFiltering="true" HeaderText="Num Of Cols" DataField="NumOfCols" SortExpression="NumOfCols" 
                            ShowFilterIcon="true" FilterControlWidth="70" HeaderStyle-Width ="70" ItemStyle-Width="70" ItemStyle-Wrap="true" AutoPostBackOnFilter="true" CurrentFilterFunction="GreaterThanOrEqualTo">
                            <ItemTemplate>
                                    <asp:Label ID="lblNumOfCols" runat="server" Text='<%# Eval("NumOfCols") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn 
                            UniqueName="TotalPatches" AllowFiltering="true" HeaderText="Total Patches" DataField="TotalPatches" SortExpression="TotalPatches" 
                            ShowFilterIcon="true" FilterControlWidth="70" HeaderStyle-Width ="70" ItemStyle-Width="70" ItemStyle-Wrap="true" AutoPostBackOnFilter="true" CurrentFilterFunction="GreaterThanOrEqualTo">
                            <ItemTemplate>
                                    <asp:Label ID="lblTotalPatches" runat="server" Text='<%# Eval("TotalPatches") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                         <telerik:GridTemplateColumn 
                            UniqueName="InstrumentName" AllowFiltering="true" HeaderText="Instrument Name" DataField="InstrumentType.InstrumentName" SortExpression="InstrumentType.InstrumentName" 
                            ShowFilterIcon="true" FilterControlWidth="90" HeaderStyle-Width ="90" ItemStyle-Width="90" ItemStyle-Wrap="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            <ItemTemplate>
                                    <asp:Label ID="lblInstrumentName" runat="server" Text='<%# Eval("InstrumentType.InstrumentName") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                         <telerik:GridTemplateColumn 
                            UniqueName="ProjectTypeName" AllowFiltering="true" HeaderText="Project Type" DataField="ProjectType.ProjectTypeName" SortExpression="ProjectType.ProjectTypeName" 
                            ShowFilterIcon="true" FilterControlWidth="70" HeaderStyle-Width ="70" ItemStyle-Width="70" ItemStyle-Wrap="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            <ItemTemplate>
                                    <asp:Label ID="lblProjectTypeName" runat="server" Text='<%# Eval("ProjectType.ProjectTypeName") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>                     
                        <telerik:GridTemplateColumn 
                                UniqueName="IsActive" AllowFiltering="false" HeaderText="IsActive?" DataField="IsActive" HeaderStyle-Width ="40"
                                FilterControlWidth="40" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Image ID="imgIsActive" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("IsUsed")) ? "~/Images/checkbox_checked.png" : "~/Images/checkbox_unchecked.png"%>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn 
                                DataField="ReferenceChartTypeId" AllowFiltering="true" HeaderText="" UniqueName="Edit" HeaderStyle-Width ="40">
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
                                            <div >
                                                <div style="float: left; padding-right: 10px;">
                                                      <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Edit Reference Chart" AlternateText="Edit"
                                                        ImageUrl="../Images/edit_icon.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ReferenceChartTypeId") %>'
                                                        CommandName="Edit" />
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
    &nbsp;</div>
<div class="clear">
</div>

