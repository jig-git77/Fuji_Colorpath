<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectListNew.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.ProjectListNew" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<%@ Register Src="~/UserControl/PopupControl/ClosePrintCondition.ascx" TagName="ClosePrintCondition"
    TagPrefix="uc" %>
<%@ Register Src="~/UserControl/PopupControl/OpenPrintCondition.ascx" TagName="OpenPrintCondition"
    TagPrefix="uc" %>

<script id="licenseType-printName-template" type="text/x-kendo-template">
   <table class="tablecss" width="100%" cellpadding="1" cellspacing="1">
        <tr>
            <td  style="vertical-align:middle;">
                <span style="display:flex;align-items:center;">
                    <a id='lnkProjectType'><img id='imgprojectType' src=''></a><strong style="cursor:pointer;">#: ProjectName #</strong>
                </span>
            </td>
        </tr>
        <tr>
    <td  style="vertical-align:middle;">
    <ul class="list-of-button">
	<li>
        <a class='linktbutton' id='lnkProjectTypeIcon'>
            <span style="display: flex; align-items: center;white-space: nowrap;cursor:pointer;">
            <img id='imgprojectTypeIcon' style="margin-right: 5px;width:20px;" src='' />
            <label id='spanImagePrjTypeTitle' style="font-weight:normal;cursor:pointer;"><label>
        </span>
        </a>
    </li>
    <li><a class='linktbutton' id='lnkSpotProjectIcon'><span style="display: flex; align-items: center;white-space: nowrap;"><img id='imgprojectBCO' style="margin-right: 5px;" width="20px;" src='Images/SpotProject.png' />BCO</span></a></li>
    <li><a class='linktbutton' id='lnkValidationIcon'><span style="display: flex; align-items: center;white-space: nowrap;"><img id='imgprojectValidation' style="margin-right: 5px;" width="20px;" src='' />Validation</span></a></li>
    </ul>
    </td>
        </tr>
    </table>
</script>
<link href="Content/kendo/2020.2.617/kendo.common.min.css" rel="stylesheet" />
<link href="Content/kendo/2020.2.617/kendo.default.min.css" rel="stylesheet" />
<script src="Scripts/kendo/2020.2.617/kendo.all.min.js"></script>
<script src="jQuery/jquery.tmpl.js?version=<%=JsRandomNumber %>" type="text/javascript"></script>
<script src="JQuery/jquery.simplemodal.js?version=<%=JsRandomNumber %>" type="text/javascript"></script>
<script src="JS/ProjectListNew.js?version=<%=JsRandomGuid %>"></script>



<script type="text/javascript" language="javascript">
    function ConfirmDeleteProject() {
        if (confirm('<%= ProjectListDeleteProjectConfirmation %>'))
            return true;
        else
            return false;
    }
</script>
<script type="text/javascript" language="javascript">
    function ConfirmDeleteSpotProject() {
        if (confirm('<%= ProjectListDeleteSpotProjectConfirmation %>'))
            return true;
        else
            return false;
    }
</script>
<script language="javascript" type="text/javascript">
    function CloseOpenPrintConditionWindow() {
        var oWnd = $find("<%= OpenPrintConditionWindow.ClientID %>");
        oWnd.close();
    }
    function CloseClosePrintConditionWindow() {
        var oWnd = $find("<%= ClosePrintConditionWindow.ClientID %>");
        oWnd.close();
    }
    function CloseOpenSpotProjectListWindow() {
        var oWnd = $find("<%= ShowSpotProjectListWindow.ClientID %>");
        oWnd.close();
    }    
</script>
<style type="text/css">
    /*.RadGrid .rgRow td, .RadGrid .rgAltRow td, .RadGrid .rgEditRow td, .RadGrid .rgFooter td, .RadGrid .rgFilterRow td, .RadGrid .rgHeader, .RadGrid .rgResizeCol, .RadGrid .rgGroupHeader td {
        padding-right: 0px !important;
    }

    div.RadGrid .rgRow {
        background: #F7F7F7;
    }

    div.RadGrid .rgAltRow {
        background: #F7F7F7;
    }

    .rgPager table {
        border-top: 0px;
        border-bottom: 1px solid #000000;
        border-spacing: 1pt;
        width: 100%;
    }

    .RadGrid_Default .rgRow a, .RadGrid_Default .rgAltRow a, .RadGrid_Default .rgEditRow a, .RadGrid_Default tr.rgEditRow a, .RadGrid_Default tr.rgHoveredRow a, .RadGrid_Default tr.rgActiveRow a, .RadGrid_Default .rgFooter a, .RadGrid_Default .rgEditForm a {
        color: #003CA9;
    }

    .RadGrid_Default td.rgPagerCell {
        -moz-border-bottom-colors: none;
        -moz-border-image: none;
        -moz-border-left-colors: none;
        -moz-border-right-colors: none;
        -moz-border-top-colors: none;
        border-color: #828282 #EEEEEE #EEEEEE;
        border-style: solid;
        border-width: 0px 0 0px 0px;
    }

    .RadGrid_Default .rgRow td, .RadGrid_Default .rgAltRow td, .RadGrid_Default .rgEditRow td, .RadGrid_Default .rgFooter td {
        border-style: none;
        border-width: 0 0 1px;
    }


    .RadGrid_Default .rgRow .rgSorted {
        background-color: #F7F7F7;
        border-bottom-color: #F7F7F7;
    }

    .RadGrid_Default .rgAltRow .rgSorted {
        background-color: #F7F7F7;
        border-bottom-color: #F7F7F7;
    }

    .RadGrid_Default th.rgSorted {
        background-color: #F1F1F1;
        background-position: 0px;
    }

    .breakWord80 {
        max-width: 80px !important;
        word-break: break-all !important;
        word-wrap: break-word !important;
        vertical-align: top;
        line-height: 15px;
    }

    .breakWord60 {
        max-width: 60px !important;
        word-break: break-all !important;
        word-wrap: break-word !important;
        vertical-align: top;
        line-height: 15px;
    }*/
    ul.list-of-button {
        margin: 0;
        padding: 0;
        display: flex;
        cursor: pointer;
    }

        ul.list-of-button li {
            margin-right: 10px;
        }

    .tablecss tbody tr:hover {
        background: unset !important;
    }

    .linktbutton {
        background-color: #ffffff;
        border: #d5d5d5 solid 1px;
        display: inline-block;
        padding: 5px 10px;
        border-radius: 10px;
        margin-top: 10px;
        cursor: pointer;
    }

    .linktbuttonDisabled {
        background-color: #ffffff;
        border: #d5d5d5 solid 1px;
        display: inline-block;
        padding: 5px 10px;
        border-radius: 10px;
        margin-top: 10px;
        cursor: pointer;
        opacity: 0.5
    }

    .k-grid-header {
        padding-right: 0 !important;
    }

    .k-grid-content {
        overflow-y: hidden;
    }

    .k-grid td {
        line-height: 2em;
    }

    .k-window-action {
        visibility: hidden;
    }

    .k-window-titlebar {
        background: #3A9977;
        //border: 2px solid #3A9977;
    }

    div.k-window {
        background: #3A9977;
        font: 16px/22px 'Trebuchet MS', Verdana, Arial;
    }

    .k-window-title {
        color: white;
        font-weight: bold;
    }
</style>

<style type="text/css">
    .InnerHeaderStyle {
        background: #C0C0C0 !important;
        /*font-size: 13px !important;*/
        /*color: black !important;*/ /*add more style definitions here*/
    }

    .InnerItemStyle {
        background: #C0C0C0 !important;
        /*color: brown !important;*/ /*add more style definitions here*/
    }

    .InnerAlernatingItemStyle {
        background: #C0C0C0 !important;
        /*color: brown !important;*/ /*add more style definitions here*/
    }

    .CommandItemStyle {
        background: #C0C0C0 !important;
        /*color: brown !important;*/ /*add more style definitions here*/
    }

    .FilterItemStyle {
        background: #C0C0C0 !important;
        /*color: brown !important;*/ /*add more style definitions here*/
    }

    .PagerItemStyle {
        background: #C0C0C0 !important;
        /*color: brown !important;*/ /*add more style definitions here*/
    }

    .k-grid .k-hierarchy-col,
    .k-grid .k-hierarchy-cell {
        width: 10px;
        overflow: hidden;
    }

    #kendoProjectList > .k-grid-content {
        min-height: 100px;
    }

    .k-grid .k-header .k-textbox {
        float: left;
    }
</style>

<div class="contant_top">
    <asp:HiddenField runat="server" ID="hdnBaseServiceUrl" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdfHasDeletePermission" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdfCurrentUser" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdfCurrentDeviceID" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdfCurrentCulture" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdfSpotProjectAllowed" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdfAuthToken" ClientIDMode="Static" />
    &nbsp;
</div>
<div class="contant_middle">
    <div class="head">
        <h1>
            <asp:Label ID="lblTitle" runat="server" Text="Project List" meta:resourcekey="lblTitleResource1"></asp:Label>
        </h1>
        <asp:LinkButton ID="lbtnCreateNewPrintCondition" runat="server" CssClass="button"
            ValidationGroup="vgDestination"
            OnClick="lbtnCreateNewPrintCondition_Click">
            <span>
                <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/new_project_icon.png" meta:resourcekey="imgAddResource1" />
                <asp:Localize runat="server" Text="Create New Print Condition" ID="locCreateNewPrintCondition"
                    meta:resourcekey="locCreateNewPrintConditionResource1"></asp:Localize>
            </span>
        </asp:LinkButton>
    </div>
    <div class="clear">
    </div>

    <div class="listDevice">
        <div>
            <div id="kendoProjectList"></div>
        </div>
    </div>
    <div class="clear">
    </div>
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
            <telerik:RadWindow ID="ShowSpotProjectListWindow" runat="server" VisibleStatusbar="false"
                ShowContentDuringLoad="false" Title="Brand Color Optimization" Width="930" Height="530"
                Behaviors="Close, Move" Behavior="Close, Move">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <input id="Hidden_TotalProjects" type="hidden" runat="server" value="0" />

    <div id="windowAlert" style="display: none;">
        <span id="alertMessage"></span>
        <br />
        <div style="text-align: center;">
            <button type="button" id="btnAlertPopup" style="line-height: 15px; width: 50px; padding: 5px 0px 5px;">OK</button>
        </div>

    </div>
</div>
<div class="contant_bottom">
    &nbsp;
</div>

<div id='dialog'>
</div>
