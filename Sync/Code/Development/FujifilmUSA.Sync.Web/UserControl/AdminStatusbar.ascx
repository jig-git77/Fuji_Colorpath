<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminStatusbar.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AdminStatusbar"     %>

<%--<div class="error">
    <span>
        <strong>
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        </strong>
    </span>
</div>
--%>

<div class="statusbar" style=" margin: 0px">
    <div class="top">
        &nbsp;</div>
    <div class="middle" runat="server" id="tblMessage">
        <asp:Label ID="lblMessage" runat="server" Text="" Style="font-weight: bold;"></asp:Label>
        &nbsp;
        <br />
    </div>
    <div class="bottom">
        &nbsp;</div>
</div>

