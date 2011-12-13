<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MeasurementDeviceList.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.MeasurementDeviceList" %>
<div class="contant_top">
    &nbsp;</div>    
<div class="contant_middle">
    <div class="head">
        <h1>Sync Measurement Device</h1>
        <a href="AddMeasurementDevice.aspx" class="button"><span>
            <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/new_project_icon.png" />
            Add Sync Measurement Device</span></a></div>
    <div class="clear">
    </div>   
          <%--  <asp:ListView ID="grdProjectList" runat="server" OnItemCommand="grdProjectList_ItemCommand" OnItemDataBound="grdProjectList_ItemDataBound" ItemPlaceholderID="phItemPlaceholder">
            <LayoutTemplate>
                <table cellspacing="0" cellpadding="0" border="0" class="product_list_table">
                <thead>
                    <tr>
                        <th align="left" scope="col">Project Name</th>
                        <th align="center" scope="col" width="10%">Info</th>
                        <th align="center" scope="col" width="10%">Delete</th>
                        <th align="center" scope="col" width="10%">Status</th>
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
                    <td align="left"><asp:LinkButton ID="lbtnProjectName" runat="server" Text='<%# Eval("ProjectName") %>' ForeColor="Blue" Font-Underline="true" OnClick="lbtnProjectName_Click" /></td>
                    <td align="center"><asp:ImageButton ID="btnInfo" runat="server" ToolTip="Information" AlternateText="Information" ImageUrl="../Images/info_icon.png" /></td>
                    <td align="center"><asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete Project" AlternateText="Delete" ImageUrl="../Images/delete_icon.png" CommandName="DeleteProject" CommandArgument='<%# Eval("ProjectId") %>' /></td>
                    <td align="center"><asp:Image ID="imgStatus" runat="server" ImageUrl="../Images/close_icon.png" /></td>
              </tr>
            </ItemTemplate>
            </asp:ListView>      --%>       
           
</div>
<div class="contant_bottom">
    &nbsp;</div>

<div class="clear">
</div>