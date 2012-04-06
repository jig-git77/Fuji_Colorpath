<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftMenu.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.LeftMenu" %>
<%@ Reference Control="~/UserControl/LeftMenuItem.ascx" %>
<%@ Reference Control="~/UserControl/LeftMenuProjectItem.ascx" %>
<%@ Reference Control="~/UserControl/LeftMenuProjectSetting.ascx" %>
<div class="sidebar_top">
    &nbsp;</div>
    <div class="sidebar_middle" id="divLeftmenu" runat="server" >
            <ul>
                <asp:PlaceHolder runat="server" ID="ph"></asp:PlaceHolder>
            </ul>
        </div>
<div class="sidebar_bottom">
    &nbsp;</div>
