<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MeasurementDeviceList.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.MeasurementDeviceList" %>
<div class="admin_top">&nbsp;</div>
<div class="admin_middle">
    <div class="head">
        <h1>Sync Measurement Device</h1>
        <a href="AddMeasurementDevice.aspx" class="button"><span>
            <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/new_project_icon.png" />
            Add Sync Measurement Device</span></a></div>
    <div class="clear">
    </div>   
           <asp:ListView ID="grdMeasurementDevice" runat="server"  ItemPlaceholderID="phItemPlaceholder">
            <LayoutTemplate>
                <table cellspacing="0" cellpadding="0" border="0" class="admin_list_table">
                <thead>
                    <tr>
                        <th align="left" scope="col">Company Name</th>
                        <th align="left" scope="col" width="15%">Branch Name</th>
                        <th align="left" scope="col" width="10%">Dongle Serial No.</th>
                        <th align="left" scope="col" width="15%">Serial Number</th>
                        <th align="left" scope="col" width="10%">Device Type</th>
                        <th align="center" scope="col" width="10%">Is Active</th>
                        <th align="center" scope="col" width="10%">Edit</th>
                        <th align="center" scope="col" width="10%">Delete</th>
                    </tr>
                </thead>
                <tbody>                                    
                    <asp:PlaceHolder ID="phItemPlaceholder" runat="server"></asp:PlaceHolder>
                </tbody>
                </table>
            </LayoutTemplate>
            <EmptyDataTemplate><div class="no_data_found">No records available</div></EmptyDataTemplate>
            <ItemTemplate>
                <tr class="purpal">
                    <td align="left"><asp:Label ID="lblCompanyName" runat="server" Text='<%#Eval("CompanyName") %>'></asp:Label></td>
                    <td align="left"><asp:Label ID="lblBranchName" runat="server" Text='<%#Eval("BranchName") %>'></asp:Label></td>
                    <td align="left"><asp:Label ID="lblDongleSerialNo" runat="server" Text='<%#Eval("CPSerialNumber") %>'></asp:Label></td>
                    <td align="left"><asp:Label ID="lblSerialNumber" runat="server" Text='<%#Eval("SerialNumber") %>'></asp:Label></td>
                    <td align="left"><asp:Label ID="lblMeasuremntDeviceType" runat="server" Text='<%#Eval("MeasurementDeviceTypeDetail.DeviceTypeName") %>'></asp:Label></td>
                    <td align="center"><asp:Label ID="lblIsActive" runat="server" Text='<%#Eval("IsActive") %>'></asp:Label></td>
                    <td align="center"><asp:ImageButton ID="imgbtnEdit" runat="server" ToolTip="Edit MeasurementDevice" AlternateText="Edit" ImageUrl="../Images/edit_icon.png" CommandName="EditMeasurementDevice" CommandArgument='<%# Eval("MeasurementDeviceId") %>' /></td>                    
                    <td align="center"><asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete MeasurementDevice" AlternateText="Delete" ImageUrl="../Images/delete_icon.png" CommandName="DeleteMeasurementDevice" CommandArgument='<%# Eval("MeasurementDeviceId") %>' /></td>                    
              </tr>
            </ItemTemplate>
            </asp:ListView>
           
</div>
<div class="admin_bottom">&nbsp;</div>
<div class="clear"></div>