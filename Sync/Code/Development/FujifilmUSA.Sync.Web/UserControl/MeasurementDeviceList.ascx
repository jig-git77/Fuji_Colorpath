<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MeasurementDeviceList.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.MeasurementDeviceList" %>
<%@ Register TagName="Statusbar" TagPrefix="ucStatusbar" Src="Statusbar.ascx" %>
<script type="text/javascript" language="javascript">
    function ConfirmDeleteProject() {
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
            Sync Measurement Device</h1>
        <asp:LinkButton ID="likAdd" runat="server" class="button" OnClick="likAdd_Click">
            <span>
                <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/new_project_icon.png" />
                Add Sync Measurement Device</span>
        </asp:LinkButton>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px; margin-right: 15px;">
        <ucStatusbar:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
    </div>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
      <%--  <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgMeasurementDevice">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgMeasurementDevice" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>--%>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
    <telerik:RadGrid ID="rgMeasurementDevice" ShowHeader="true" AllowFilteringByColumn="true"
        AllowSorting="false" AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False"
        EnableLinqExpressions="false" 
        OnNeedDataSource="rgMeasurementDevice_NeedDataSource"
        OnItemCommand="rgMeasurementDevice_ItemCommand" 
        OnItemDataBound="rgMeasurementDevice_ItemDataBound"
        Width="100%" >
        <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />
        <MasterTableView DataKeyNames="MeasurementDeviceId">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="Company" AllowFiltering="true" HeaderText="Company"
                    DataField="Company" SortExpression="Company" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    ShowFilterIcon="false">
                    <ItemTemplate>
                        <asp:Label ID="lblCompany" runat="server" Text='<%# Eval("Company") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            
                <telerik:GridTemplateColumn UniqueName="Branch" AllowFiltering="true" HeaderText="Branch"
                    DataField="Branch" SortExpression="Branch" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    ShowFilterIcon="false">
                    <ItemTemplate>
                        <asp:Label ID="lblBranchName" runat="server" Text='<%# Eval("Branch") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="Branch" AllowFiltering="true" HeaderText="SerialNumber"
                    DataField="SerialNumber" SortExpression="SerialNumber" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" ShowFilterIcon="false">
                    <ItemTemplate>
                        <asp:Label ID="lblSerialNumberName" runat="server" Text='<%# Eval("SerialNumber") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="DeviceType" AllowFiltering="true" HeaderText="DeviceType"
                    DataField="DeviceType">
                    <ItemTemplate>
                        <asp:Label ID="lblDeviceType" runat="server" Text='<%# Eval("DeviceType") %>' />
                    </ItemTemplate>
                    <FilterTemplate>
                        <telerik:RadComboBox ID="RadComboBoxStatus" AppendDataBoundItems="true" runat="server"
                            OnClientSelectedIndexChanged="StatusIndexChanged" AutoPostBack="false" Width="50"
                            DropDownWidth="70" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("DeviceType").CurrentFilterValue %>'>
                            <Items>
                                <telerik:RadComboBoxItem Text="ALL" />
                                <telerik:RadComboBoxItem Text="Open" Value="Open" />
                                <telerik:RadComboBoxItem Text="Closed" Value="Closed" />
                                <telerik:RadComboBoxItem Text="Certify" Value="Certify" />
                                <telerik:RadComboBoxItem Text="NotUsed" Value="NotUsed" />
                            </Items>
                        </telerik:RadComboBox>
                        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
                            <script type="text/javascript">
                                function StatusIndexChanged(sender, args) {
                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                    tableView.filter("DeviceType", args.get_item().get_value(), "EqualTo");
                                }
                            </script>
                        </telerik:RadScriptBlock>
                    </FilterTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="IsActive" AllowFiltering="true" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" FilterControlWidth="100" ItemStyle-Wrap="true">
                    <ItemTemplate>
                        <asp:Image ID="imgStatus" runat="server" ImageUrl='<%= Convert.ToBoolean(Eval("IsActive")) ? "~/Images/export.png" : "~/Images/analyze.png" %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
              
                <telerik:GridTemplateColumn DataField="MeasurementDeviceId" AllowFiltering="false"
                    HeaderText="" UniqueName="Update">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Edit Measurement Device"
                            AlternateText="Edit" ImageUrl="../Images/edit_icon.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"MeasurementDeviceId") %>'
                            CommandName="Update" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="MeasurementDeviceId" AllowFiltering="false"
                    HeaderText="" UniqueName="Delete">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete Measurement Device"
                            AlternateText="Delete" ImageUrl="../Images/delete_icon.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"MeasurementDeviceId") %>'
                            CommandName="Delete" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
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