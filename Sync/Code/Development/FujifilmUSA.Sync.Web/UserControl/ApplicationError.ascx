<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApplicationError.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ApplicationError" %>
<%@ Register Src="~/UserControl/Footer.ascx" TagName="Footer" TagPrefix="uc" %>
<script type="text/javascript" language="javascript">
 GetRadWindow = function () {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow;
        else if (window.frameElement && window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
        return oWindow;
    };

    OnClickHereToLogin = function () {
        var wnd = GetRadWindow();
        if (wnd) {
            wnd.BrowserWindow.location.href = "Login.aspx";
        }
        else {
            location = "Login.aspx";
        }        
    };
</script>

<div class="Global_top">
    &nbsp;</div>
<div class="Global_middle" style="line-height: 34px;">
    <div class="head">
        <h1><asp:Localize runat="server" Text="System Error" ID="txtSystemError" 
                meta:resourcekey="txtSystemErrorResource1"></asp:Localize></h1>
    </div>
    <div class="clear">
    </div>

    <div style="margin-left: 10px;">       
        <center>

            <div style="height:200px;">
                <div style="font-size: 14px;"><br /><br /><br />
                    <span style="font-size: 18px; color: red;"><asp:Localize Text="ERROR: Unexpected error occured. Please
                        contact your administrator." runat="server" ID="locUnexpectedError" 
                        meta:resourcekey="locUnexpectedErrorResource1"></asp:Localize></span>
                    <br /><br />
                    <asp:Localize runat="server" Text="Please click here to go back to" 
                        ID="locGoBack" meta:resourcekey="locGoBackResource1"></asp:Localize> 
                    <asp:LinkButton ID="lnkLogin" runat="server" OnClick="lnkLogin_Click" OnClientClick="javascript: OnClickHereToLogin();"
                        meta:resourcekey="lnkLoginResource1"><span style="font-weight:bold;"><asp:Localize 
                        runat="server" Text="Login page." ID="locLoginPage" 
                        meta:resourcekey="locLoginPageResource1"></asp:Localize>
</span></asp:LinkButton>
                </div>
            </div>
         </center>
      
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
