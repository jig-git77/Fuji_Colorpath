<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrintPreview.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.PrintPreview" %>
<script src="JQuery/jquery-1.7.1.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        var imgPreview = document.getElementById("<%= imgPreview.ClientID %>");
        imgPreview.src = "data:image/png;base64," + window.opener.previewImageCodedString;
    });

    PrintLabel = function () {
        window.opener.PrintLabel(false);
        return false;
    };
</script>   
<div>
    <div style="text-align: right; margin-bottom: 5px; width: 382px;">
        <asp:ImageButton ID="ibtnPrintLabel" runat="server" 
            ImageUrl="../Images/DymoPrintIcon.png" ToolTip="Print label" 
            OnClientClick="PrintLabel(); return false;" 
            meta:resourcekey="ibtnPrintLabelResource1"  />
    </div>
    <asp:Image runat="server" ID="imgPreview" />
</div>   
