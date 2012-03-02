<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="FujifilmUSA.Sync.Web.Mock.UserControl.Header" %>
<div class="top_link"><a href="#">Your IP-Address:  <%=Request.UserHostAddress %></a>    |    <a href="#">Logged in as:  <%=FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentUser.LoginId%>-<%= FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentUser.FirstName %> <%=FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentUser.LastName%></a>    |    
        <asp:LinkButton ID="lbtnLogout" runat="server" onclick="lbtnLogout_Click">Logout</asp:LinkButton>    |    <a href="#">English</a></div>
<!--Start Header-->
<header>
    <section id="header">
        <div class="logo"><a href="Index.aspx"><img src="images/logo.png" alt=""></a></div>
        <div class="setting"><a href="Settings.aspx">&nbsp;</a></div>
    </section>
</header>
<!--End Header-->