<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftMenuItem.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.LeftMenuItem" %>
    <ul id="menu">
        <li>                        
            <asp:LinkButton CssClass="text" runat="server" ID="lnkItem" OnClick="lnkItem_Click">            
            <span class='image'>
            <asp:Image CssClass="image" runat="server" ID="imgItem" /></span>
            <span class="text" id="lnktext" runat="server"></span>
           </asp:LinkButton>
        </li>
    </ul>
