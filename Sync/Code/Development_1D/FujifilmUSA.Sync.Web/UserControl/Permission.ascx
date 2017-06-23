<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Permission.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.Permission" %>
<script type="text/javascript" language="javascript">
        function pageLoad() {
                 $('#dock').hide();
            }
</script>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle">
    <div class="head">
        <h1><asp:Localize runat="server" Text="Session" ID="locSession" 
                meta:resourcekey="locSessionResource1"></asp:Localize></h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 20px;">
        <div style=" font-size: 14px;">
                <asp:Localize Text="You don't have premission to view this content. click here goto back" 
                    runat="server" ID="locPermissionMessgae" 
                    meta:resourcekey="locPermissionMessgaeResource1"></asp:Localize> 
                <asp:LinkButton ID="lbtnHome" runat="server" PostBackUrl="~/Index.aspx"><span style="font-weight:bold;"><asp:Localize 
                    Text="home page." runat="server" ID="locHomePage" 
                    meta:resourcekey="locHomePageResource1"></asp:Localize>
</span></asp:LinkButton>
       </div>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
