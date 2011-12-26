<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeviceLinkProfileList.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.DeviceLinkProfileList" %>
<script type="text/javascript" language="javascript">
    function ConfirmDeleteProject() {
        if (confirm('<%= DLP_DeleteDestinationProfile_Confirmation %>'))
            return true;
        else
            return false;
    }
    </script>
<div class="contant_top">&nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <h1>Device Link Profile</h1>
        <a href="AddDeviceLinkProfile.aspx" class="button"><span>
            <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/new_project_icon.png" />
            New DLP</span></a></div>
    <div class="clear">
    </div>   
           <asp:ListView ID="grdDLP" runat="server"  
        ItemPlaceholderID="phItemPlaceholder"   OnItemCommand="grdDLP_ItemCommand"> 
            <LayoutTemplate>
                <table cellspacing="0" cellpadding="0" border="0" class="product_list_table">
                <thead>
                    <tr>
                        <th align="left" scope="col">DLP Name</th>
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
                    <td align="left"><asp:Label ID="lblDLPName" runat="server" Text='<%#Eval("ProfileName") %>'></asp:Label></td>
                    <td align="left" width="30%"><asp:Label ID="lblInUse" runat="server" Text='<%#DisplayIsActiveCustomValue(Eval("IsUse")) %>'></asp:Label></td>
                    <td align="center" width="5%"><asp:ImageButton ID="imgbtnEdit" runat="server" ToolTip="Edit Destination Profile" AlternateText="Edit" ImageUrl="../Images/edit_icon.png" CommandName="EditDestinationProfile" CommandArgument='<%# Eval("ProfileId") %>' /></td>                    
                    <td align="center" width="5%"><asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete Destination Profile" AlternateText="Delete" ImageUrl="../Images/delete_icon.png" CommandName="DeleteDestinationProfile" CommandArgument='<%# Eval("ProfileId") %>' OnClientClick="return ConfirmDeleteProject()" /></td>                    
              </tr>
            </ItemTemplate>
            </asp:ListView>            
</div> 
<div class="contant_bottom">&nbsp;</div>
<div class="clear"></div>
