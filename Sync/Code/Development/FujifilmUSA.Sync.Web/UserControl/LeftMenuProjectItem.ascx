<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftMenuProjectItem.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.LeftMenuProjectItem" %>
<ul id="menu2">
    <li>
        <asp:LinkButton runat="server" ID="lnkSyncRunName" OnClick="lnkSyncRunName_Click"></asp:LinkButton>
        <ul class="submenu" runat="server" id="ulSyncSubMenu">
            <li>
                <asp:LinkButton runat="server" ID="lnkMeasurement" Text="Measurement" OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkAnalyse" Text="Analyse" OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkExport" Text="Export" OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
        </ul>
    </li>
</ul>
