<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MeasurementDeviceList.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.MeasurementDeviceList" %>
<%@ Register TagName="Statusbar" TagPrefix="ucStatusbar" Src="Statusbar.ascx" %>
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
        <ucStatusbar:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
    </div>
      <asp:UpdatePanel ID="testPanel" runat="server">
            <ContentTemplate>
    <telerik:RadGrid ID="rgMeasurementDevice" ShowHeader="true" AllowFilteringByColumn="true"
        AllowSorting="false" AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False"
        EnableLinqExpressions="false" 
        OnNeedDataSource="rgMeasurementDevice_NeedDataSource"
        OnItemCommand="rgMeasurementDevice_ItemCommand" 
        Width="100%" >
        <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />
        <MasterTableView DataKeyNames="MeasurementDeviceId">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="CompanyName" AllowFiltering="true" HeaderText="Company Name"
                    DataField="CompanyName" SortExpression="CompanyName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    ShowFilterIcon="false">
                    <ItemTemplate>
                        <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>' Width="150px" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="BranchName" AllowFiltering="true" HeaderText="Branch Name"
                    DataField="BranchName" SortExpression="BranchName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    ShowFilterIcon="false">
                    <ItemTemplate>
                        <asp:Label ID="lblBranchName" runat="server" Text='<%# Eval("BranchName") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="LicenseType" AllowFiltering="true" HeaderText="License Type"
                    DataField="LicenseType" SortExpression="LicenseType" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    ShowFilterIcon="false">
                    <ItemTemplate>
                        <asp:Label ID="lblLicenseType" runat="server" Text='<%# Eval("LicenseType") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="SerialNumber" AllowFiltering="true" HeaderText="Serial Number"
                    DataField="SerialNumber" SortExpression="SerialNumber" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" ShowFilterIcon="false">
                    <ItemTemplate>
                        <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Eval("SerialNumber") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="InstrumentType" AllowFiltering="true" HeaderText="Instrument Type"
                    DataField="InstrumentType" SortExpression="InstrumentType" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" ShowFilterIcon="false">
                    <ItemTemplate>
                        <asp:Label ID="lblInstrumentType" runat="server" Text='<%# Eval("InstrumentType.InstrumentName") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="IsActive" AllowFiltering="true" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" FilterControlWidth="100" ItemStyle-Wrap="true">
                    <ItemTemplate>
                        <asp:Label ID="lblIsActive" runat="server" Text='<%# Convert.ToString(Eval("IsActive")) %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
              
                <telerik:GridTemplateColumn DataField="MeasurementDeviceId" AllowFiltering="false"
                    HeaderText="Edit" UniqueName="Update">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Edit Measurement Device"
                            AlternateText="Edit" ImageUrl="../Images/edit_icon.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"MeasurementDeviceId") %>'
                            CommandName="Edit" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="MeasurementDeviceId" AllowFiltering="false"
                    HeaderText="Delete" UniqueName="Delete">
                    <ItemTemplate>
                              <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                <script type="text/javascript" language="javascript">
                                    function ConfirmDeleteMeasurementDevice() {
                                        debugger;
                                        if (confirm('<%= MeasurementDeviceList_DeleteMeasurementDevice_Confirmation %>'))
                                            return true;
                                        else
                                            return false;
                                        
                                    }
                                </script>
                            </telerik:RadCodeBlock>
                        <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete MeasurementDevice" AlternateText="Delete"
                                        ImageUrl="../Images/delete_icon.png" 
                                        OnClientClick="javascript:ConfirmDeleteMeasurementDevice();"
                                        CommandArgument ='<%# DataBinder.Eval(Container.DataItem,"MeasurementDeviceId") %>'
                                        CommandName="Delete" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
    </ContentTemplate>
    </asp:UpdatePanel>
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