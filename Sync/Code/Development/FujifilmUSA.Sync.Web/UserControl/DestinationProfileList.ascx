<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DestinationProfileList.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.DestinationProfileList" %>
<%@ Register TagName="Statusbar" TagPrefix="ucStatusbar" Src="Statusbar.ascx" %>
<script type="text/javascript" language="javascript">
    function ConfirmDeleteDestinationProfile() {
        if (confirm('<%= DestinationProfile_DeleteDestinationProfile_Confirmation %>'))
            return true;
        else
            return false;
    }
</script>
<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <h1>
            Destination Profile</h1>
        <a href="AddDestinationProfile.aspx" class="button"><span>
            <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/new_project_icon.png" />
            New Destination Profile</span></a></div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px; margin-right: 15px;">
        <ucStatusbar:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
    </div>
    <asp:ListView ID="grdDestinationProfile" runat="server" ItemPlaceholderID="phItemPlaceholder"
        OnItemCommand="grdDestinationProfile_ItemCommand">
        <LayoutTemplate>
            <table cellspacing="0" cellpadding="0" border="0" class="product_list_table">
                <thead>
                    <tr>
                        <th align="left" scope="col">
                            Destination Profile Name
                        </th>
                        <th align="left" scope="col" width="30%">
                            IsUse
                        </th>
                        <th align="center" scope="col" width="5%">
                        </th>
                        <th align="center" scope="col" width="5%">
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:PlaceHolder ID="phItemPlaceholder" runat="server"></asp:PlaceHolder>
                </tbody>
            </table>
        </LayoutTemplate>
        <EmptyDataTemplate>
            <div class="no_data_found">
                No records available</div>
        </EmptyDataTemplate>
        <ItemTemplate>
            <tr class="purpal">
                <td align="left">
                    <asp:Label ID="lblDestinationProfileName" runat="server" Text='<%#Eval("ProfileName") %>'></asp:Label>
                </td>
                <td align="left" width="30%">
                    <asp:Label ID="lblInUse" runat="server" Text='<%#DisplayIsActiveCustomValue(Eval("IsUse")) %>'></asp:Label>
                </td>
                <td align="center" width="5%">
                    <asp:ImageButton ID="imgbtnEdit" runat="server" ToolTip="Download Destination Profile"
                        AlternateText="Edit" ImageUrl="../Images/edit_icon.png" CommandName="DownloadDestinationProfile"
                        CommandArgument='<%# Eval("ProfileID") %>' OnClientClick="return true;" />
                </td>
                <td align="center" width="5%">
                    <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete Destination Profile"
                        AlternateText="Delete" ImageUrl="../Images/delete_icon.png" CommandName="DeleteDestinationProfile"
                        CommandArgument='<%# Eval("ProfileID") %>' OnClientClick="return ConfirmDeleteDestinationProfile()" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</div>
<div class="contant_bottom">
    &nbsp;</div>
<div class="clear">
</div>
