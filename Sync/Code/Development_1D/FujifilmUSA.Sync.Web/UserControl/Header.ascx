<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Header.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Header" %>
<div class="top_link">
    <a href="#"><asp:Localize Text="Your IP-Address:" runat="server" 
        ID="locYourIPAddress" meta:resourcekey="locYourIPAddressResource1"></asp:Localize>
        <%=Request.UserHostAddress %>
    </a>| <a href="#"><asp:Localize Text="Logged in as:" runat="server" 
        Id="locLoggedInAs" meta:resourcekey="locLoggedInAsResource1"></asp:Localize>
        <%= UsernameHeaderText%>        
    </a>|
    <asp:LinkButton ID="lbtnLogout" runat="server" OnClick="lbtnLogout_Click" 
        Text="Logout" meta:resourcekey="lbtnLogoutResource1"></asp:LinkButton>
    | <a href="#"><%= System.Threading.Thread.CurrentThread.CurrentUICulture.NativeName%></a></div>
<!--Start Header-->
<%--<header>--%>


 <div id="header">
        <table style="width:100%;">
            <tr>
                <td align="left" style="width:20%;">
                        <div class="logo">
                           <asp:ImageButton runat="server" ID="imgbtnLogo" OnClick="imgbtnLogo_Click" 
                                ImageUrl="~/Images/logo.png" />
                        </div>
                </td>
                <td align="right">
                    <div class="drupa">
                     <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Drupa1.png"></asp:Image>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Drupa2.png"></asp:Image>
                    </div>
                </td>
                <td align="right" style="width:20%;">
                    <div class="setting"><a id="lbtnSettings" runat="server" href="~/Settings.aspx">&nbsp;</a></div>
                </td>
            </tr>
        </table>
    	
    </div>

  <%--  <section id="header">
        <div class="logo">
        <asp:ImageButton runat="server" ID="imgbtnLogo" OnClick="imgbtnLogo_Click" 
                ImageUrl="~/Images/logo.png" />
        <%--<a href="Index.aspx"><img src="images/logo.png" alt=""></a>--%>
      <%--  </div>
        <div class="drupa"><img src="~/Images/Drupa1.png" alt="" /><img src="~/Images/Drupa2.png" alt="" /></div>
        <div class="setting"><a id="lbtnSettings" runat="server" href="~/Settings.aspx">&nbsp;</a></div>
    </section>--%>
 <%--</header>--%>
<!--End Header-->
