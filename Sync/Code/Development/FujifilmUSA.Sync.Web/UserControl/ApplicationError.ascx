<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApplicationError.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ApplicationError" %>
<%@ Register Src="~/UserControl/Footer.ascx" TagName="Footer" TagPrefix="uc" %>

<div class="Global_top">
    &nbsp;</div>
<div class="Global_middle" style="line-height: 34px;">
    <div class="head">
        <h1>
            System Error</h1>
    </div>
    <div class="clear">
    </div>

    <div style="margin-left: 10px;">       
        <center>

            <div style="height:200px;">
                <div style="font-size: 14px;"><br /><br /><br />
                    <span style="font-size: 18px; color: red;">Opps!!! Unexpected error occured. Please
                        contact your administrator.</span>
                    <br /><br />
                    Please click here to go back to
                    <asp:LinkButton ID="lnkLogin" runat="server" OnClick="lnkLogin_Click"><span style="font-weight:bold;">Login page.</span></asp:LinkButton>
                </div>
            </div>
         </center>
      
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
