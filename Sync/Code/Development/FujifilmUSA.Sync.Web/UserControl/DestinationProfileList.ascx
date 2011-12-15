<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DestinationProfileList.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.DestinationProfileList" %>
<div class="admin_top">&nbsp;</div>
<div class="admin_middle">
    <div class="head">
        <h1>Destination Profile</h1>
        <a href="" class="button"><span>
            <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/new_project_icon.png" />
            Add Destination Profile</span></a></div>
    <div class="clear">
    </div>   
           <asp:ListView ID="grdDestinationProfile" runat="server"  
        ItemPlaceholderID="phItemPlaceholder">
            <LayoutTemplate>
                <table cellspacing="0" cellpadding="0" border="0" class="admin_list_table">
                <thead>
                    <tr>
                        <th align="left" scope="col">Destination Profile Name</th>
                        <th align="left" scope="col">IsUse</th>                        
                        <th align="center" scope="col"></th>
                        <th align="center" scope="col"></th>
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
                    <td align="left"><asp:Label ID="lblCompanyName" runat="server" Text='<%#Eval("DestinationProfileName") %>'></asp:Label></td>
                    <td align="left"><asp:Label ID="lblBranchName" runat="server" Text='<%#Eval("IsUse") %>'></asp:Label></td>
                    <td align="center"><asp:ImageButton ID="imgbtnEdit" runat="server" ToolTip="Edit Destination Profile" AlternateText="Edit" ImageUrl="../Images/edit_icon.png" CommandName="EditDestinationProfile" CommandArgument='<%# Eval("DestinationProfileID") %>' /></td>                    
                    <td align="center"><asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete Destination Profile" AlternateText="Delete" ImageUrl="../Images/delete_icon.png" CommandName="DeleteDestinationProfile" CommandArgument='<%# Eval("DestinationProfileID") %>' OnClientClick="return ConfirmDeleteProject()" /></td>                    
              </tr>
            </ItemTemplate>
            </asp:ListView>
           
</div>
<div class="admin_bottom">&nbsp;</div>
<div class="clear"></div>

