<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MeasurementDeviceList.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.MeasurementDeviceList" %>
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
    <div style="margin-left: 10px; margin-right: 15px;">
        <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="false" />
    </div>
    <div style="margin-left: 10px;">
    <asp:UpdatePanel ID="testPanel" runat="server">
        <ContentTemplate>
            <telerik:RadGrid ID="rgMeasurementDevice" ShowHeader="true" AllowFilteringByColumn="true"
                AllowSorting="false" AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False"
                EnableLinqExpressions="false" OnNeedDataSource="rgMeasurementDevice_NeedDataSource"
                OnItemCommand="rgMeasurementDevice_ItemCommand" Width="98%"
                HeaderStyle-Font-Bold="true"  >
                <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />
                <MasterTableView DataKeyNames="MeasurementDeviceId">
                    <Columns>
                        <telerik:GridTemplateColumn 
                            UniqueName="CompanyName" 
                            AllowFiltering="true" 
                            HeaderText="Company Name"
                            DataField="CompanyName" 
                            SortExpression="CompanyName" 
                            AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" 
                            ShowFilterIcon="false" 
                            FilterControlWidth="140"
                            HeaderStyle-Width ="140" 
                            >
                            <ItemTemplate>
                                <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn 
                            UniqueName="BranchName" 
                            AllowFiltering="true" 
                            HeaderText="Branch Name"
                            DataField="BranchName" 
                            SortExpression="BranchName" 
                            AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" 
                            ShowFilterIcon="false" 
                            FilterControlWidth="140"
                            HeaderStyle-Width ="140" 
                            >
                            <ItemTemplate>
                                <asp:Label ID="lblBranchName" runat="server" Text='<%# Eval("BranchName") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn 
                            UniqueName="LicenseType" 
                            AllowFiltering="true" 
                            HeaderText="License Type"
                            DataField="LicenseType" 
                            SortExpression="LicenseType" 
                            AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" 
                            ShowFilterIcon="false"  
                            FilterControlWidth="140"
                            HeaderStyle-Width ="140" 
                            >
                            <ItemTemplate>
                                <asp:Label ID="lblLicenseType" runat="server" Text='<%# Eval("LicenseType") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn 
                            UniqueName="SerialNumber" 
                            AllowFiltering="true" 
                            HeaderText="Serial Number"
                            DataField="SerialNumber" 
                            SortExpression="SerialNumber" 
                            AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" 
                            ShowFilterIcon="false"
                            FilterControlWidth="100"
                            HeaderStyle-Width ="100" >
                            <ItemTemplate>
                                <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Eval("SerialNumber") %>'/>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn 
                            UniqueName="InstrumentType" 
                            AllowFiltering="true" 
                            HeaderText="Instrument Type"
                            DataField="InstrumentType.InstrumentName" 
                            SortExpression="InstrumentType.InstrumentName" 
                            AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" 
                            ShowFilterIcon="false"
                            FilterControlWidth="70"
                            HeaderStyle-Width ="70" 
                            >
                            <ItemTemplate>
                                <asp:Label ID="lblInstrumentType" runat="server" Text='<%# Eval("InstrumentType.InstrumentName") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        
                        <telerik:GridTemplateColumn 
                                UniqueName="IsActive" 
                                AllowFiltering="false" 
                                HeaderText="IsActive?"
                                DataField="IsActive" 
                                ItemStyle-HorizontalAlign="Center"
                                FilterControlWidth="40"
                                HeaderStyle-Width ="40" 
                                >
                                <ItemTemplate>
                                    <asp:Image ID="imgIsActive" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) ? "~/Images/checkbox_checked.png" : "~/Images/checkbox_unchecked.png"%>' />
                                </ItemTemplate>
                        </telerik:GridTemplateColumn>


                           <telerik:GridTemplateColumn 
                                DataField="BranchID" 
                                AllowFiltering="true" 
                                HeaderText=""
                                UniqueName="Edit">
                                <FilterTemplate>
                                    <asp:LinkButton ID="lbtnClearFillter" runat="server" CssClass="gray_bt" CommandName="ClearFilter">
                                                    <span> Clear Fillter </span>
                                    </asp:LinkButton>
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
<%--                    <td align="left"><asp:Label ID="lblCompanyName" runat="server" Text='<%#Eval("CompanyName") %>'></asp:Label></td>
                    <td align="left"><asp:Label ID="lblBranchName" runat="server" Text='<%#Eval("BranchName") %>'></asp:Label></td>
                    <td align="left"><asp:Label ID="lblSerialNumber" runat="server" Text='<%#Eval("SerialNumber") %>'></asp:Label></td>
                    <td align="left"><asp:Label ID="lblMeasuremntDeviceType" runat="server" Text='<%#Eval("MeasurementDeviceTypeDetail.DeviceTypeName") %>'></asp:Label></td>
                    <td align="left"><asp:Label ID="lblIsActive" runat="server" Text='<%#DisplayIsActiveCustomValue(Eval("IsActive")) %>'></asp:Label></td>
                    <td align="center"><asp:ImageButton ID="imgbtnEdit" runat="server" ToolTip="Edit MeasurementDevice" AlternateText="Edit" ImageUrl="../Images/edit_icon.png" CommandName="EditMeasurementDevice" CommandArgument='<%# Eval("MeasurementDeviceId") %>' /></td>                    
                    <td align="center"><asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete MeasurementDevice" AlternateText="Delete" ImageUrl="../Images/delete_icon.png" CommandName="DeleteMeasurementDevice" CommandArgument='<%# Eval("MeasurementDeviceId") %>' OnClientClick="return ConfirmDeleteProject()" /></td>                    
--%>