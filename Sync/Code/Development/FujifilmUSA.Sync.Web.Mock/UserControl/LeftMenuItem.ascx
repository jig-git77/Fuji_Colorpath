<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftMenuItem.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.Mock.UserControl.LeftMenuItem" %>
<ul id="menu">
    <li>     
       <span style = "float:left; width:18px"><asp:Image runat="server" ID="imgItem" /> </span>
        <span class="mytext" id="lnktext" runat="server" >
            <asp:LinkButton CssClass="text"  runat="server" ID="lnkItem" OnClick="lnkItem_Click" > 
            </asp:LinkButton>
        </span>
    </li>
</ul>


