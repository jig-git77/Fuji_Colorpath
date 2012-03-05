<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="FujifilmUSA.Sync.Web.Mock.UserControl.Login" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<script language="javascript">
    function blurControl(obj, text) {
        if (jQuery(obj).val() == '') {
            jQuery(obj).val(text);
        }
    }
    function focusControl(obj, text) {
        if (jQuery(obj).val() == text)
        { jQuery(obj).val(''); }
    }
</script>
<div id="login_box">
    <div class="curve_top_login">
    </div>  
    <div id="pageWrapper">
        <div class="login_logo">
            <a href="#">
                <asp:Image ID="imgCompanyLogo" runat="server" ImageUrl="~/Images/login_logo.png" /></a></div>    
        <div class="login_contant">
            <div class="login_eror">
            <div style="text-align:left; margin: 10px 10px 10px 0px;">
              <ucStatusbar:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
            </div>
            </div>
            <asp:TextBox ID="txtUserName" runat="server" CssClass="input" onblur="javascript:blurControl(this,'Username');"
                onfocus="javascript:focusControl(this,'Username');" Text="Username"></asp:TextBox>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="input" onblur="javascript:blurControl(this,'Password');"
                onfocus="javascript:focusControl(this,'Password');" Text="Password" TextMode="Password"></asp:TextBox>
            <asp:CheckBox ID="chkRememberMe" runat="server" CssClass="remember_me" Text=" Remember me!"/>
            <asp:Button ID="bntLogin" runat="server" CssClass="login_bt" OnClick="bntLogin_Click" />
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="curve_bottom">
        &nbsp;</div>
</div>
<script language="javascript">
    jQuery(document).ready(function () {
        if (jQuery("[id*='txtPassword']").val() == '') {
            jQuery("[id*='txtPassword']").val('Password'); 
        }
    });   
</script>
