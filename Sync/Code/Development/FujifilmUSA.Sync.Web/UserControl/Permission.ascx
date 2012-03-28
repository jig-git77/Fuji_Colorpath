<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Permission.ascx.cs"
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
                You don't have premission to view this content. click here goto back <asp:LinkButton ID="lbtnHome" runat="server" PostBackUrl="~/Index.aspx" ><span style="font-weight:bold;">home page.</span></asp:LinkButton>
       </div>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
