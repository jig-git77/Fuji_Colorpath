<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MeasurementDeviceList.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.MeasurementDeviceList" %>
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
    
     .breakWord120
     {
            max-width: 120px !important;
            word-break: break-all !important;
            word-wrap: break-word !important;
            vertical-align: top;
            line-height: 15px;
     }
      .breakWord100
     {
            max-width: 100px !important;
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
    function ConfirmDeleteMeasurementDevice() {
        if (confirm('<%= MeasurementDeviceList_DeleteMeasurementDevice_Confirmation %>'))
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
            Measurement Device Management</h1>
        <asp:LinkButton ID="likAdd" runat="server" class="button" OnClick="likAdd_Click">
            <span>
                <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/new_project_icon.png" />
                Add Sync Measurement Device</span>
        </asp:LinkButton>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
    <asp:UpdatePanel ID="testPanel" runat="server">
        <ContentTemplate>
            <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" Visible="false" />
            <telerik:RadGrid ID="rgMeasurementDevice" ShowHeader="true" AllowFilteringByColumn="true"
                AllowSorting="true" AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False"
                EnableLinqExpressions="false" OnNeedDataSource="rgMeasurementDevice_NeedDataSource"
                OnItemCommand="rgMeasurementDevice_ItemCommand" Width="98%"
                HeaderStyle-Font-Bold="true"  >
                <GroupingSettings CaseSensitive="false" />
                <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />
                <MasterTableView DataKeyNames="MeasurementDeviceId">
                    <Columns>
                        <telerik:GridTemplateColumn 
                            UniqueName="CompanyName" AllowFiltering="true" HeaderText="Company Name" DataField="CompanyName" SortExpression="CompanyName" 
                            ShowFilterIcon="true" FilterControlWidth="120" HeaderStyle-Width ="120" ItemStyle-CssClass="breakWord120" ItemStyle-Width="120" 
                            ItemStyle-Wrap="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">                           
                            <ItemTemplate>
                                <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn 
                            UniqueName="BranchName" AllowFiltering="true" HeaderText="Branch Name" DataField="BranchName" SortExpression="BranchName" 
                            ShowFilterIcon="true" FilterControlWidth="120" HeaderStyle-Width ="120" ItemStyle-CssClass="breakWord120" ItemStyle-Width="120" 
                            ItemStyle-Wrap="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            <ItemTemplate>
                                <asp:Label ID="lblBranchName" runat="server" Text='<%# Eval("BranchName") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                       
                        <telerik:GridTemplateColumn 
                            UniqueName="SerialNumber" AllowFiltering="true" HeaderText="Serial Number" DataField="SerialNumber" SortExpression="SerialNumber" 
                            ShowFilterIcon="true" FilterControlWidth="90" HeaderStyle-Width ="90" ItemStyle-CssClass="breakWord70" ItemStyle-Width="90" 
                            ItemStyle-Wrap="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            <ItemTemplate>
                                <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Eval("SerialNumber") %>'/>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        
                        <telerik:GridTemplateColumn 
                            UniqueName="InstrumentType" AllowFiltering="true" HeaderText="Instrument Type" DataField="InstrumentType.InstrumentName" SortExpression="InstrumentType.InstrumentName" 
                            ShowFilterIcon="true" FilterControlWidth="100" HeaderStyle-Width ="100" ItemStyle-CssClass="breakWord100" ItemStyle-Width="100" 
                            ItemStyle-Wrap="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            <ItemTemplate>
                                <asp:Label ID="lblInstrumentType" runat="server" Text='<%# Eval("InstrumentType.InstrumentName") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                         <telerik:GridTemplateColumn 
                            UniqueName="Description" AllowFiltering="true" HeaderText="Description" DataField="Description" SortExpression="Description" 
                            ShowFilterIcon="true"  FilterControlWidth="120" HeaderStyle-Width ="120" ItemStyle-CssClass="breakWord120" ItemStyle-Width="120" 
                            ItemStyle-Wrap="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            <ItemTemplate>
                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn 
                                UniqueName="IsActive" AllowFiltering="false" HeaderText="IsActive?" DataField="IsActive" 
                                ItemStyle-HorizontalAlign="Center" FilterControlWidth="40" HeaderStyle-Width ="40">
                                <ItemTemplate>
                                    <asp:Image ID="imgIsActive" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) ? "~/Images/checkbox_checked.png" : "~/Images/checkbox_unchecked.png"%>' />
                                </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn 
                                DataField="BranchID" AllowFiltering="true" HeaderText="" UniqueName="Edit">
                                <FilterTemplate>
                                    <center>
                                    <asp:LinkButton ID="lbtnClearFillter" runat="server" ToolTip="Clear Fillter" CommandName="ClearFilter">
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
                                                   <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Edit Measurement Device"
                                                        AlternateText="Edit" ImageUrl="../Images/edit_icon.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"MeasurementDeviceId") %>'
                                                        CommandName="Edit" />
                                                </div>
                                                <div style="float: left;">
                                                       <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete MeasurementDevice"
                                                                AlternateText="Delete" ImageUrl="../Images/delete_icon.png" OnClientClick="return ConfirmDeleteMeasurementDevice();"
                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem,"MeasurementDeviceId") %>'
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
        </ContentTemplate>
    </asp:UpdatePanel>
    </div> 
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
