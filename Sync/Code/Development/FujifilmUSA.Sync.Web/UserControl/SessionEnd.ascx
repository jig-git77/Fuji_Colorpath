<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SessionEnd.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.SessionEnd" %>
<script type="text/javascript" language="javascript">
        function pageLoad() {
                 $('#dock').hide();
            }
</script>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle">
    <div class="head">
        <h1>Session</h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 20px;">
        <div style=" font-size: 14px;">
                Session has been unexpectadly abandoned. Please click here to go back to  <asp:LinkButton ID="LinkButton1" runat="server" OnClick="likIndex_Click"><span style="font-weight:bold;">home page.</span></asp:LinkButton>
            </div>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
