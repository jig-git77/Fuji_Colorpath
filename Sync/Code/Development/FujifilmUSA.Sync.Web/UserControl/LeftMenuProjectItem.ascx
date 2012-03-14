<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftMenuProjectItem.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.LeftMenuProjectItem" %>
<ul id="menu2">
    <li>
        <asp:LinkButton runat="server" ID="lnkRunName" OnClick="lnkRunName_Click"></asp:LinkButton>
        <ul class="submenu" runat="server" id="ulSubMenu">
            <li>
                <asp:LinkButton runat="server" ID="lnkMeasurement" Text="Measurement" OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkAnalyse" Text="Analyze"  OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkDownload" Text="Download" OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
        </ul>
    </li>
</ul>