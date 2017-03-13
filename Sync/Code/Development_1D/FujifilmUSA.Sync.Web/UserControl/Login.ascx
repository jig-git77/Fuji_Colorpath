<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Login" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<script type="text/javascript">
    function SendMessageToParent() {
        var bx = document.getElementById('<%=txtUserName.ClientID %>');
        if (bx != null && window.parent != null) {
            window.parent.postMessage(bx.value, "http://www.softwebdemo.com/Softweb/ColorOrganizerDemo");
        }
        return true;
    }
    function createCookie(name, value, days) {
        var expires;
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toGMTString();
        }
        else expires = "";
        document.cookie = name + "=" + value + expires + "; path=/";
    }

    function readCookie(name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
        }
        return null;
    }

    function eraseCookie(name) {
        createCookie(name, "", -1);
    }

    function areCookiesEnabled() {
        var r = false;
        createCookie("testing", "Hello", 1);
        if (readCookie("testing") != null) {
            r = true;
            eraseCookie("testing");
        }
        return r;
    }
</script>
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
<script type="text/javascript" language="javascript">
    function ShowOpenForgotPasswordWindow() {
        var oWindow;
        oWindow = window.radopen("PopupOpenForgotPassword.aspx", "OpenForgotPasswordWindow");
        oWindow.show();
        return false;
    }

    function ShowBrowserTestWindow() {
        var oWindow;
        oWindow = window.radopen("BrowserTest.aspx", "BrowseTestWindow");
        oWindow.show();
        return false;
    }
</script>
<script language="javascript" type="text/javascript">
    function CloseOpenForgotPasswordWindow() {
        var oWnd = $find("<%= OpenForgotPasswordWindow.ClientID %>");
        oWnd.close();
    }
</script>


<div id="login_box">
    <div class="curve_top_login">
         <div style="text-align:right; padding-right:40px; padding-top:20px;">
             <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/browsericons_1.png"  Visible="false" />
            <asp:ImageButton ID="btn_OpenBrowserTest" runat="server" AlternateText="Open Browser Test" ImageUrl="~/Images/browsericons.png" 
                    OnClientClick="return ShowBrowserTestWindow();" ToolTip="Open Browser Test" Visible="false" />
        </div>
    </div>
    <div id="pageWrapper">
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
       <asp:Label ID="lblMessage" runat="server"></asp:Label>
           
       
        <div class="login_logo">
            <a href="#">
                <asp:Image ID="imgCompanyLogo" runat="server" 
                ImageUrl="~/Images/login_logo.png" meta:resourcekey="imgCompanyLogoResource1" /></a></div>
        <div class="login_contant">
            <div class="login_eror">
                <div style="text-align: left; margin: 10px 10px 10px 0px;">
                    <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
                </div>
            </div>
            <asp:TextBox ID="txtUserName" runat="server" CssClass="input" onblur="javascript:blurControl(this,'Username');"
                onfocus="javascript:focusControl(this,'Username');" Text="Username" 
                meta:resourcekey="txtUserNameResource1"></asp:TextBox>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="input" onblur="javascript:blurControl(this,'Password');"
                onfocus="javascript:focusControl(this,'Password');" Text="Password"
                TextMode="Password" meta:resourcekey="txtPasswordResource1"></asp:TextBox>            
            <asp:CheckBox ID="chkRememberMe" runat="server" CssClass="remember_me" 
                Text=" Remember me!" meta:resourcekey="chkRememberMeResource1" />
            <asp:LinkButton ID="lnkForgotPassword" runat="server" Text="Forgot Password!" 
                OnClientClick="return ShowOpenForgotPasswordWindow();" 
                meta:resourcekey="lnkForgotPasswordResource1"></asp:LinkButton>
            &nbsp;
            <asp:Button ID="bntLogin" runat="server" CssClass="login_bt" 
                OnClick="bntLogin_Click" OnClientClick="return SendMessageToParent();" 
                meta:resourcekey="bntLoginResource1" />            
        </div>
        <div class="clear">
        </div>
     </ContentTemplate>           
        </asp:UpdatePanel>     
    </div>
    <div class="curve_bottom">
        &nbsp;</div>
</div>

<telerik:RadWindowManager ID="RadWindowManager" runat="server" Modal="True">
    <Windows>
        <telerik:RadWindow ID="OpenForgotPasswordWindow" runat="server" IconUrl="" VisibleStatusbar="false"
            ShowContentDuringLoad="false" Title="Forgot Password!" Width="730" Height="230"
            Behaviors="Move, Close">
        </telerik:RadWindow>

         <telerik:RadWindow ID="BrowseTestWindow" runat="server" IconUrl="" VisibleStatusbar="false"
            ShowContentDuringLoad="false" Title="Browser Test" Width="730" Height="530"
            Behaviors="Move, Close">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
<script language="javascript">
    jQuery(document).ready(function () {
        if (!areCookiesEnabled()) {
            alert("Your browser's cookie functionality is turned off. Please turn it on.");
        }
        SetPasswordWatermark();
    });

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
        SetPasswordWatermark();
    });

    function SetPasswordWatermark() {
        if (jQuery("[id*='txtPassword']").val() == '') {
            jQuery("[id*='txtPassword']").val('Password');
        }
    }
</script>
