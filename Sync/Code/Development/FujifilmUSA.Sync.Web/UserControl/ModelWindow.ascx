<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModelWindow.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.ModelWindow"     %>
<%--<%@ Register Src="~/UserControl/ProjectList/ClosePrintCondition.ascx" TagName="ClosePrintCondition" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/ProjectList/OpenPrintCondition.ascx" TagName="OpenPrintCondition" TagPrefix="uc" %>
--%>
<script type="text/javascript" language="javascript">
    var title = '';
    function OpenInformationBox(url) {
        var oWnd = radopen(url, "RadModelWindow");
        if (url.indexOf("ModelControlId=1") >= 0) {
            title = "Open Print Condition";
            oWnd.setSize(650, 300);
        }
        if (url.indexOf("ModelControlId=2") >= 0) {
            title = "Close Print Condition";
            oWnd.setSize(650, 270);
        }
        if (url.indexOf("ModelControlId=3") >= 0) {
            title = "Download Chart";
            oWnd.setSize(650, 270);
        }
        oWnd.add_pageLoad(windowLoad);
        return false;
    }
    function windowLoad(oWindow, args) {
        oWindow.set_title(title);
        oWindow.set_status('');
    }
</script>
<telerik:RadWindowManager ID="RadWindowManager" runat="server" 
    Modal="True" 
    >
    <Windows>
                <telerik:RadWindow 
                    id="RadModelWindow" 
                    runat="server"
                    showcontentduringload="false"
                    VisibleStatusbar ="false"
                    behaviors="Close, Move" Behavior="Close, Move"
                    >
                    
                </telerik:RadWindow>
            </Windows>
</telerik:RadWindowManager>

<%--<uc:ClosePrintCondition ID="ucClosePrintCondition" runat="server"  />
<uc:OpenPrintCondition ID="ucOpenPrintCondition" runat="server" />--%>