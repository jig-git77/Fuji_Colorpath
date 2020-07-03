<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SpotProjectList.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.PopupControl.SpotProjectList" %>
<link href="Content/kendo/2020.2.617/kendo.common.min.css" rel="stylesheet" />
<link href="Content/kendo/2020.2.617/kendo.default.min.css" rel="stylesheet" />
<script src="Scripts/kendo/2020.2.617/kendo.all.min.js"></script>
<script src="jQuery/jquery.tmpl.js?version=<%=JsRandomNumber %>" type="text/javascript"></script>
<script src="JQuery/jquery.simplemodal.js?version=<%=JsRandomNumber %>" type="text/javascript"></script>
<script src="JS/SpotProjectListNew.js?version=<%=JsRandomGuid %>"></script>
<script language="javascript" type="text/javascript">
    function CloseOpenSpotProjectListWindow() {
        var oWindow = GetSpotProjectListingInformationRadWindow();
        GetSpotProjectListingInformationRadWindow().close();
        return false;
    }

    function GetSpotProjectListingInformationRadWindow() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow;
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
        return oWindow;
    }
     function ConfirmDeleteSpotProject() {
        if (confirm('<%= ProjectListDeleteSpotProjectConfirmation %>'))
            return true;
        else
            return false;
    }    
    function createSpotProject() {        
        return Redirect($("#hdfProjectID").val(),'AddSpotProject');
    }
</script>
<table style="width: 100%">
    <tr>
        <td style="vertical-align: top; border-bottom: solid 1px #808080">
            <table style="width: 100%">
                <tr>
                    <td style="vertical-align: middle;">
                        <strong>
                            <asp:Label ID="lblTitle" runat="server">Brand Color Libraries</asp:Label></strong>
                    </td>
                    <td style="vertical-align: top;">
                        <div class="buttonright" style="padding-left: 3px">
                            <asp:LinkButton ID="btnCreateSpotProject" runat="server" CssClass="gray_bt" OnClick="btnCreateSpotProject_Click">
                                <span>
                                    
                                    <asp:Localize runat="server" ID="locCreateLibrary"></asp:Localize>
                                </span>
                            </asp:LinkButton>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">
            <div class="listDevice">
                <div>
                    <div id="kendoSpotProjectList"></div>
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">
            <table style="width: 100%">
                <tr>
                    <td style="vertical-align: top;width:85%;">
                        <div class="buttonright">
                            <asp:LinkButton ID="btnExportSpotProject" runat="server" CssClass="gray_bt" OnClientClick="return ShowSpotLibraryExportWindow(this);">
                                <span>
                                    <asp:Localize runat="server" Text="Export Combined Library" ID="Localize1">Export Combined Library</asp:Localize>
                                </span>
                            </asp:LinkButton>
                        </div>
                    </td>
                    <td style="vertical-align: top;">
                        <div class="buttonright">
                            <asp:LinkButton ID="btnCancel" runat="server" OnClientClick="javascript: CloseOpenSpotProjectListWindow(); return false;"
                                        CssClass="gray_bt">
                                        <span>
                                            <asp:Localize runat="server" Text="Cancel" ID="Localize2"
                                                meta:resourcekey="locCancelResource1"></asp:Localize>
                                        </span>
                                    </asp:LinkButton>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
  <telerik:RadWindowManager ID="RadWindowManager" runat="server" Modal="True">
        <Windows>
            <telerik:RadWindow ID="OpenPrintConditionWindow" runat="server" IconUrl="" VisibleStatusbar="false"
                ShowContentDuringLoad="false" Title="Open Print Condition" Width="720" Height="220"
                Behaviors="Move,Close">
            </telerik:RadWindow>
            <telerik:RadWindow ID="ClosePrintConditionWindow" runat="server" VisibleStatusbar="false"
                ShowContentDuringLoad="false" Title="Close Print Condition" Width="720" Height="200"
                Behaviors="Move,Close">
            </telerik:RadWindow>
            <telerik:RadWindow ID="ReportSpotLibraryExportWindow" runat="server" VisibleStatusbar="false"
                ShowContentDuringLoad="false" Title="Spot Library Export" Width="730" Height="300"
                Behaviors="Close, Move" Behavior="Close, Move">
            </telerik:RadWindow>            
        </Windows>
    </telerik:RadWindowManager>
<asp:HiddenField runat="server" ID="hdnBaseServiceUrl" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="hdfHasDeletePermission" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="hdfCurrentUser" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="hdfProjectID" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="hdfCurrentCulture" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="hdfSpotProjectAllowed" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="hdfAuthToken" ClientIDMode="Static" />
