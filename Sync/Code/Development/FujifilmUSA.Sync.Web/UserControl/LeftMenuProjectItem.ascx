<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftMenuProjectItem.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.LeftMenuProjectItem" %>
<div>
    <li>
        <asp:LinkButton runat="server" ID="lnkProjectName"></asp:LinkButton>
    </li>
    <li>
    <asp:LinkButton runat="server" ID="lnkProjectSetup" Text="Project Setup"></asp:LinkButton>
    </li>
    <ul class='submenu'>
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
</div>
