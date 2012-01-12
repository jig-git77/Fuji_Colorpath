<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Statusbar.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Statusbar"     %>

<%--<div class="error">
    <span>
        <strong>
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        </strong>
    </span>
</div>
--%>

<div class="mesurementStatusbar" style=" margin: 0px">
    <div class="top">
        &nbsp;</div>
    <div class="middle" runat="server" id="tblMessage">
        <asp:Label ID="lblMessage" runat="server" Text="" Style="font-weight: bold; padding-left: 5px;"></asp:Label>
        &nbsp;
        <br />
        
    </div>
    <div class="bottom">
        &nbsp;</div>
</div>

