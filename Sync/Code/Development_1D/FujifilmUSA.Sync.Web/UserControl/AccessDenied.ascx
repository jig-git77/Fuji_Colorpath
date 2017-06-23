<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccessDenied.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AccessDenied" %>
<script type="text/javascript" language="javascript">
    function pageLoad() {
        $('#dock').hide();
    }
</script>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle">
    <div class="head">
        <h1><asp:Localize runat="server" Text="Access Denied" ID="locDenied"></asp:Localize></h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 20px;">
        <div style=" font-size: 14px;">
                <asp:Localize ID="locAccessDenied" runat="server" 
                    Text="Access Denied. Please click here to go back"></asp:Localize>&nbsp;
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="likIndex_Click"><span style="font-weight:bold;"><asp:Localize 
                    Text="home page." runat="server" ID="locHomePage"></asp:Localize>
</span></asp:LinkButton>
            </div>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>

