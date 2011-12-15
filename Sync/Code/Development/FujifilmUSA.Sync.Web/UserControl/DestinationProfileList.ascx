<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DestinationProfileList.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.DestinationProfileList" %>
<script type="text/javascript" language="javascript">
    function ConfirmDeleteProject() {
        if (confirm('<%= DestinationProfile_DeleteDestinationProfile_Confirmation %>'))
            return true;
        else
            return false;
    }
    </script>
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
        ItemPlaceholderID="phItemPlaceholder" 
        onitemcommand="grdDestinationProfile_ItemCommand">
            <LayoutTemplate>
                <table cellspacing="0" cellpadding="0" border="0" class="admin_list_table">
                <thead>
                    <tr>
                        <th align="left" scope="col">Destination Profile Name</th>
                        <th align="left" scope="col" width="30%">IsUse</th>                        
                        <th align="center" scope="col" width="5%"></th>
                        <th align="center" scope="col" width="5%"></th>
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
                    <td align="left" width="30%"><asp:Label ID="lblBranchName" runat="server" Text='<%#DisplayIsActiveCustomValue(Eval("IsUse")) %>'></asp:Label></td>
                    <td align="center" width="5%"><asp:ImageButton ID="imgbtnEdit" runat="server" ToolTip="Edit Destination Profile" AlternateText="Edit" ImageUrl="../Images/edit_icon.png" CommandName="EditDestinationProfile" CommandArgument='<%# Eval("DestinationProfileID") %>' /></td>                    
                    <td align="center" width="5%"><asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete Destination Profile" AlternateText="Delete" ImageUrl="../Images/delete_icon.png" CommandName="DeleteDestinationProfile" CommandArgument='<%# Eval("DestinationProfileID") %>' OnClientClick="return ConfirmDeleteProject()" /></td>                    
              </tr>
            </ItemTemplate>
            </asp:ListView>
           
</div>
<div class="admin_bottom">&nbsp;</div>
<div class="clear"></div>

