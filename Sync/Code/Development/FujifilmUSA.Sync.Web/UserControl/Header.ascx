<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Header" %>
<div class="top_link">
    <a href="#"><asp:Localize Text="Your IP-Address:" runat="server" 
        ID="locYourIPAddress" meta:resourcekey="locYourIPAddressResource1"></asp:Localize>
        <%=Request.UserHostAddress %>
    </a>| <a href="#"><asp:Localize Text="Logged in as:" runat="server" 
        Id="locLoggedInAs" meta:resourcekey="locLoggedInAsResource1"></asp:Localize>
        <%=FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentUser.LoginId%>
        -<%= FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentUser.FirstName %>
        <%=FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentUser.LastName%>
    </a>|
    <asp:LinkButton ID="lbtnLogout" runat="server" OnClick="lbtnLogout_Click" 
        Text="Logout" meta:resourcekey="lbtnLogoutResource1"></asp:LinkButton>
    | <a href="#"><%= System.Threading.Thread.CurrentThread.CurrentUICulture.NativeName%></a></div>
<!--Start Header-->
<header>
    <section id="header">
        <div class="logo">
        <asp:ImageButton runat="server" ID="imgbtnLogo" OnClick="imgbtnLogo_Click" 
                ImageUrl="~/Images/logo.png" />
        <%--<a href="Index.aspx"><img src="images/logo.png" alt=""></a>--%>
        </div>
        <div class="setting"><a id="lbtnSettings" runat="server" href="~/Settings.aspx">&nbsp;</a></div>
    </section>
</header>
<!--End Header-->
