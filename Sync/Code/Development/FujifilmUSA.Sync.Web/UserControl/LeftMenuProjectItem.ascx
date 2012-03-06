<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftMenuProjectItem.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.LeftMenuProjectItem" %>
<ul id="menu2">
    <li>
        <asp:LinkButton runat="server" ID="lnkSyncRunName" ></asp:LinkButton>
        <ul class="submenu" runat="server" id="ulSyncSubMenu">
            <li>
                <asp:LinkButton runat="server" ID="lnkMeasurement" Text="Measurement" ></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkAnalyse" Text="Analyze"  ></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkExport" Text="Export" ></asp:LinkButton>
            </li>
        </ul>
    </li>
</ul>

<script language="javascript" type="text/javascript">
    function CheckMeasurement() {        
        if (jQuery("input[id*='hdHasMeasurement']").val() > 0)             
            return true;        
        else
            return false;        
    }
</script>