<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta content="index,follow" name="robots" />
    <meta content="text/html; charset=iso-8859-1" http-equiv="Content-Type" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="minimum-scale=1.0, width=device-width, maximum-scale=0.6667, user-scalable=no"
        name="viewport" />
    <title>::..Fujifilm..::</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="wapper">
        <div class="logo">
            <a href="#">
                <img src="images/logo.png" alt="Fujifilm" title="Fujifilm" /></a></div>
        <div id="Div_Login" runat="server" class="login_form">
            <asp:Label ID="LblError1" runat="server" CssClass="ERROR"></asp:Label>
            <div class="login_form_in">
                <div class="Username">
                    <ul>
                        <li class="name">Username</li>
                        <li><span>
                            <asp:TextBox ID="TXT_LoginUserName" runat="server" CssClass="inputtext"></asp:TextBox>
                        </span></li>
                    </ul>
                </div>
                <div class="Username">
                    <ul>
                        <li class="name">Password</li>
                        <li><span>
                            <asp:TextBox ID="TXT_LoginPassword" runat="server" TextMode="Password" CssClass="inputtext"></asp:TextBox>
                        </span></li>
                    </ul>
                </div>
                <div class="login_btn">
                    <asp:ImageButton ID="IBtnLogin" ImageUrl="~/images/login_btn.png" runat="server"
                        OnClick="IBtnLogin_Click" />
                </div>
                <div class="forgot_password">
                    <asp:LinkButton ID="LbtnForgotPassword" runat="server" OnClick="LbtnForgotPassword_Click">Forgot Password?</asp:LinkButton>
                </div>
            </div>
            <div class="poweredby">
                powered by <span>
                    <img src="images/poweredby_icon.png" alt="powered by" title="powered by" /></span></div>
        </div>
        <div id="Div_ForgotPassword" runat="server" class="login_form">
            <asp:Label ID="LblError" runat="server" CssClass="ERROR"></asp:Label>
            <div class="login_form_in">
                <div class="Username">
                    <ul>
                        <li class="name">Username</li>
                        <li><span>
                            <asp:TextBox ID="TxtForgotPasswordUserName" runat="server" CssClass="inputtext"></asp:TextBox>
                        </span></li>
                    </ul>
                </div>
                <div class="Username">
                    <ul>
                        <li class="name">Email</li>
                        <li><span>
                            <asp:TextBox ID="TxtForgotPasswordEmail" runat="server" CssClass="inputtext"></asp:TextBox>
                        </span></li>
                    </ul>
                </div>
                <div class="login_btn">
                    <asp:ImageButton ID="IbtnForgotPassword" ImageUrl="~/images/forgot_password.png" OnClick="IbtnForgotPassword_Click"
                        runat="server" />
                </div>
                <div class="forgot_password">
                    <asp:LinkButton ID="LbtnLogin" runat="server" OnClick="LbtnLogin_Click">Login?</asp:LinkButton>
                </div>
            </div>
            <div class="poweredby">
                powered by <span>
                    <img src="images/poweredby_icon.png" alt="powered by" title="powered by" /></span></div>
        </div>
    </div>
    </form>
</body>
</html>
