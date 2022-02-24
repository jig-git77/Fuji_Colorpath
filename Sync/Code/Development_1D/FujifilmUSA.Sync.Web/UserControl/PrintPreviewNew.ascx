<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrintPreviewNew.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.PrintPreviewNew" %>
<script src="JQuery/jquery-3.5.1.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        var htmlContent = window.opener.labelPrintHTMLString;
        document.getElementById("imgPreview").innerHTML = htmlContent;
        //$("#imgPreview").html(htmlContent);
    });
    function openPrinter() {
        window.print();
    }
</script>
<style type="text/css">
    @media print {
        .noprint {
            display: none;
        }
    }
 @page {
    size: landscape;
    margin:  0 !important;
    padding: 0 !important;
  }
</style>
<div>
    <div style="text-align: right; margin-bottom: 5px; width: 490px;" class="noprint">
        <asp:ImageButton ID="ibtnHelp" runat="server"
            ImageUrl="../Images/DymoPrintHelpIcon-New.png" ToolTip="Help Print label" 
            meta:resourcekey="ibtnPrintLabelResource1" OnClick="ibtnHelp_Click"   />
        <asp:ImageButton ID="ibtnPrintLabel" runat="server"
            ImageUrl="../Images/DymoPrintIcon-New.png" ToolTip="Print label"
            OnClientClick="openPrinter();"
            meta:resourcekey="ibtnPrintLabelResource1" />
    </div>
    <div id="imgPreview" style="background-color: white;max-width: 101.6mm; margin: auto;">
    </div>
</div>
