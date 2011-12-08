<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftMenuProjectItem.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.LeftMenuProjectItem" %>
<%--<li>
        <asp:LinkButton runat="server" ID="lnkProjectSetup" Text="Project Setup"></asp:LinkButton>
    </li>--%>
<ul id="menu2">
    <li>
        <asp:LinkButton runat="server" ID="lnkSyncRunName"></asp:LinkButton>
   
<ul class="submenu" runat="server" id="ulSyncSubMenu">
    <li>
        <asp:LinkButton runat="server" ID="lnkMeasurement" Text="Measurement"></asp:LinkButton>
    </li>
    <li>
        <asp:LinkButton runat="server" ID="lnkAnalyse" Text="Analyse"></asp:LinkButton>
    </li>
    <li>
        <asp:LinkButton runat="server" ID="lnkExport" Text="Export"></asp:LinkButton>
    </li>
</ul>
 </li>
</ul>