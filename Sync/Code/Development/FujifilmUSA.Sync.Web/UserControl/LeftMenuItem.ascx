<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftMenuItem.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.LeftMenuItem" %>
<ul id="menu">
    <li><span class='image'>
        <asp:Image CssClass="image" runat="server" ID="imgItem" />
        <asp:LinkButton CssClass="text" runat="server" ID="lnkItem" 
           OnClick="lnkItem_Click">            
        </asp:LinkButton>       
    </span></li>
</ul>
