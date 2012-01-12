<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SourceProfileList.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.SourceProfileList" %>
<%@ Register TagName="Statusbar" TagPrefix="ucStatusbar" Src="Statusbar.ascx"  %>

<script type="text/javascript" language="javascript">
    function ConfirmDeleteSourceProfile() {
        if (confirm('<%= SourceProfile_DeleteSourceProfile_Confirmation %>'))
            return true;
        else
            return false;
    }
    </script>
<div class="admin_top">&nbsp;</div>
<div class="admin_middle">
    <div class="head">
        <h1>Source Profile</h1>
        <a href="AddSourceProfile.aspx" class="button"><span>
            <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/new_project_icon.png" />
            New Source Profile</span></a></div>
    <div class="clear">
    </div> 
    <div style="margin-left: 10px; margin-right: 15px;" ><ucStatusbar:Statusbar ID="ucStatusbar1" runat="server" Visible="false" /></div>  
        <asp:UpdatePanel ID="upPanel" runat="server">
            <ContentTemplate>
        <asp:ListView ID="grdSourceProfile" runat="server"  
        ItemPlaceholderID="phItemPlaceholder" 
        onitemcommand="grdSourceProfile_ItemCommand">
            <LayoutTemplate>
                <table cellspacing="0" cellpadding="0" border="0" class="admin_list_table">
                <thead>
                    <tr>
                        <th align="left" scope="col">Source Profile Name</th>
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
                    <td align="left"><asp:Label ID="lblSourceProfileName" runat="server" Text='<%#Eval("ProfileName") %>'></asp:Label></td>
                    <td align="left" width="30%"><asp:Label ID="lblInUse" runat="server" Text='<%#DisplayIsActiveCustomValue(Eval("IsUse")) %>'></asp:Label></td>
                    <td align="center" width="5%"><asp:ImageButton ID="imgbtnEdit" runat="server" ToolTip="Edit Source Profile" AlternateText="Edit" ImageUrl="../Images/edit_icon.png" CommandName="EditSourceProfile" CommandArgument='<%# Eval("ProfileID") %>' /></td>                    
                    <td align="center" width="5%"><asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete Source Profile" AlternateText="Delete" ImageUrl="../Images/delete_icon.png" CommandName="DeleteSourceProfile" CommandArgument='<%# Eval("ProfileID") %>' OnClientClick="return ConfirmDeleteSourceProfile()" /></td>                    
              </tr>
            </ItemTemplate>
            </asp:ListView>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="grdSourceProfile" EventName="ItemCommand" />
            </Triggers>
        </asp:UpdatePanel>

           
</div>
<div class="admin_bottom">&nbsp;</div>
<div class="clear"></div>

