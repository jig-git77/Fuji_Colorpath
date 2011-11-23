<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Login" %>
<div id="login_box">
    <div class="curve_top_login">
    </div>
    <div id="pageWrapper">
        <div class="login_logo">
            <a href="#">
                <asp:Image ID="imgCompanyLogo" runat="server" ImageUrl="~/Images/login_logo.png" /></a></div>
        <div class="login_contant">
            <input type="text" name="" value="Username" class="input" onblur="this.value==''?this.value='Username':this.value=this.value"
                onfocus="this.value=='Username'?this.value='':this.value=this.value" />
            <input type="password" name="" value="Password" class="input" onblur="this.value==''?this.value='Password':this.value=this.value"
                onfocus="this.value=='Password'?this.value='':this.value=this.value" />
            <asp:Button ID="bntLogin" runat="server" CssClass="login_bt" />
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="curve_bottom">
        &nbsp;</div>
</div>
