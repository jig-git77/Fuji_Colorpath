<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Header" %>
<div class="top_link">
    <a href="#">Your IP-Address:
        <%=Request.UserHostAddress %>
    </a>| <a href="#">Logged in as:
        <%=FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentUser.LoginId%>
        -<%= FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentUser.FirstName %>
        <%=FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentUser.LastName%>
    </a>|
    <asp:LinkButton ID="lbtnLogout" runat="server" OnClick="lbtnLogout_Click">Logout</asp:LinkButton>
    | <a href="#">English</a></div>
<!--Start Header-->
<header>
    <section id="header">
        <div class="logo">
        <asp:ImageButton runat="server" ID="imgbtnLogo" OnClick="imgbtnLogo_Click" ImageUrl="~/Images/logo.png" />
        <%--<a href="Index.aspx"><img src="images/logo.png" alt=""></a>--%>
        </div>
        <div class="setting"><a id="lbtnSettings" runat="server" href="~/Settings.aspx">&nbsp;</a></div>
    </section>
</header>
<!--End Header-->
