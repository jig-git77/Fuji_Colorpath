<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftMenu.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.LeftMenu" %>
<%@ Reference Control="~/UserControl/LeftMenuItem.ascx" %>
<%@ Reference Control="~/UserControl/LeftMenuProjectItem.ascx" %>
<div class="sidebar_top">
    &nbsp;</div>
<asp:HiddenField ID="hdHasMeasurement" runat="server" Value="0" />
<div class="sidebar_middle" id="divLeftmenu" runat="server">
    <ul>
        <asp:PlaceHolder runat="server" ID="ph"></asp:PlaceHolder>
    </ul>
</div>
<div class="clear">
</div>
<div class="sidebar_bottom">
    &nbsp;</div>
    
<%--<asp:Button runat="server" ID="btnSyncRun" OnClick />--%>
