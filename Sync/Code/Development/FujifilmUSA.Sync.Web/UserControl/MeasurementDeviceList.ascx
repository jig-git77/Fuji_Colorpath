<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MeasurementDeviceList.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.MeasurementDeviceList" %>
<script type="text/javascript" language="javascript">
    function ConfirmDeleteProject() {
        if (confirm('<%= MeasurementDeviceList_DeleteMeasurementDevice_Confirmation %>'))
            return true;
        else
            return false;
    }
    </script>
<div class="admin_top">&nbsp;</div>
<div class="admin_middle">
    <div class="head">
        <h1>Sync Measurement Device</h1>
        <a href="AddMeasurementDevice.aspx" class="button"><span>
            <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/new_project_icon.png" />
            Add Sync Measurement Device</span></a></div>
    <div class="clear">
    </div>   
        <asp:UpdatePanel ID="upPanel" runat="server">
            <ContentTemplate>
           <asp:ListView ID="grdMeasurementDevice" runat="server"  
        ItemPlaceholderID="phItemPlaceholder" 
        onitemcommand="grdMeasurementDevice_ItemCommand">
            <LayoutTemplate>
                <table cellspacing="0" cellpadding="0" border="0" class="admin_list_table">
                <thead>
                    <tr>
                        <th align="left" scope="col">Company</th>
                        <th align="left" scope="col" width="15%">Branch</th>
                        <th align="left" scope="col" width="15%">Dongle Sr. No.</th>
                        <th align="left" scope="col" width="15%">Device Sr. No.</th>
                        <th align="left" scope="col" width="10%">Type</th>
                        <th align="left" scope="col" width="10%">Active?</th>
                        <th align="center" scope="col" width="3%"></th>
                        <th align="center" scope="col" width="3%"></th>
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
                    <td align="left"><asp:Label ID="lblIsActive" runat="server" Text='<%#DisplayIsActiveCustomValue(Eval("IsActive")) %>'></asp:Label></td>
                    <td align="center"><asp:ImageButton ID="imgbtnEdit" runat="server" ToolTip="Edit MeasurementDevice" AlternateText="Edit" ImageUrl="../Images/edit_icon.png" CommandName="EditMeasurementDevice" CommandArgument='<%# Eval("MeasurementDeviceId") %>' /></td>                    
                    <td align="center"><asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete MeasurementDevice" AlternateText="Delete" ImageUrl="../Images/delete_icon.png" CommandName="DeleteMeasurementDevice" CommandArgument='<%# Eval("MeasurementDeviceId") %>' OnClientClick="return ConfirmDeleteProject()" /></td>                    
              </tr>
            </ItemTemplate>
            </asp:ListView>
              </ContentTemplate>
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID="grdMeasurementDevice" EventName="ItemCommand" />
            </Triggers> 
        </asp:UpdatePanel>
</div>
<div class="admin_bottom">&nbsp;</div>
<div class="clear"></div>
