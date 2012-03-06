<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModelWindowContainer.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.ModelWindowContainer"     %>
<%@ Register Src="~/UserControl/ProjectList/ClosePrintCondition.ascx" TagName="ClosePrintCondition" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/ProjectList/OpenPrintCondition.ascx" TagName="OpenPrintCondition" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/AddProject/SaveWindow.ascx" TagName="SaveWindow" TagPrefix="uc" %>

<script language="javascript" type="text/javascript">
    function Close() {
        var oWindow = GetRadWindow1();
        GetRadWindow1().close();
    }

    function GetRadWindow1() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow;
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
        return oWindow;
    } 
</script>
<asp:PlaceHolder runat="server" ID="ph"></asp:PlaceHolder>
<uc:ClosePrintCondition ID="ucClosePrintCondition" runat="server"  />
<uc:OpenPrintCondition ID="ucOpenPrintCondition" runat="server" />
<uc:SaveWindow ID="ucSaveWindow" runat="server" />