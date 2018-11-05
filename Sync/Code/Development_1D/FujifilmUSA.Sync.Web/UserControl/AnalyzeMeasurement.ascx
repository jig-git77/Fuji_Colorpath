<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AnalyzeMeasurement.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AnalyzeMeasurement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/PopupControl/CreateDLP.ascx" TagName="CreateDLP"
    TagPrefix="uc" %>

<script src="JS/Printing.js?version=<%=JsRandomNumber %>" type="text/javascript"></script>
<link href="Content/kendo/2018.1.221/kendo.common.min.css" rel="stylesheet" />
<link href="Content/kendo/2018.1.221/kendo.default.min.css" rel="stylesheet" />
<script src="Scripts/kendo/2018.1.221/kendo.all.min.js" type="text/javascript"></script>
<style>
    .tempTooltip {
        background-color: transparent !important;
        border: none !important;
        box-shadow: none !important;
        background-image: none !important;
    }

    #tabstrip > ul:first-child > li {
        display: none;
    }

    .listDevice .k-tabstrip-wrapper .k-content {
        height: auto !important;
    }

    #divMeasurementPatchDetails .k-grid-content-locked {
        height: auto !important;
    }

    #divMeasurementPatchDetails .k-grid td {
        line-height: 18px;
    }

    .k-tabstrip .k-content {
        padding-right: 0px;
        padding-left: 0px;
    }

    .k-content .k-state-active {
        /* height: 40px; whatever height you want */
        /*width:100px;*/
        padding-right: 0px;
        padding-left: 0px;
    }

    /*.k-state-active table:first-child {
        width: 99% !important;
    }*/

    .gamutChart {
        background: url('Images/LabBackdrop.jpg');
        background-repeat: no-repeat;
        background-size: cover;
    }

    .spinner {
        opacity: 0;
        display: none;
        width: 20px;
        height: 10px;
        margin-bottom: 5px;
        -webkit-transition: opacity 0.25s, max-width 0.45s;
        -moz-transition: opacity 0.25s, max-width 0.45s;
        -o-transition: opacity 0.25s, max-width 0.45s;
        transition: opacity 0.25s, max-width 0.45s;
        /* Duration fixed since we animate additional hidden width */
    }

    .has-spinner.active {
        cursor: progress;
    }

        .has-spinner.active .spinner {
            display: inline-block;
            opacity: 1;
            max-width: 50px;
            /* More than it will ever come, notice that this affects on animation duration */
        }

    .summaryMetricTable {
        border-spacing: 0px;
        border: 1px solid;
        font: 12px/16px "segoe ui",arial,sans-serif;
    }

        .summaryMetricTable tr {
            pointer-events: none;
        }

        .summaryMetricTable th {
            border-bottom: 1px solid Gray;
            text-align: left;
            padding-left: 5px;
        }

        .summaryMetricTable td {
            border-width: 0 0 0 0px;
            line-height: 1.3em;
        }

        .summaryMetricTable tr:nth-child(even) {
            background: #F2F2F2;
        }

    .k-header > .k-grid-filter, .k-header > .k-header-column-menu {
        display: none;
    }

    .k-grid-header th.k-with-icon .k-link {
        margin-right: 0;
    }

    .k-window-title {
        font-weight: bold;
    }

    .analyze-loader-container {
        border-color: #c5c5c5;
        border-style: solid;
        border-width: 1px;
        -webkit-appearance: none;        
        padding-left: 10px;
        margin-right: 2px;
        margin-left: 10px;
        text-align: center;
        height: 200px;
        /*background-color: #eae8e8;*/
    }
</style>
<script src="http://www.labelwriter.com/software/dls/sdk/js/DYMO.Label.Framework.2.0.2.js" type="text/javascript" charset="UTF-8"> </script>
<script src="JS/AnalyzeMeasurement.js?version=<%=JsRandomNumber %>" type="text/javascript"></script>


<script type="text/javascript">
    var previewImageCodedString = '';
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_initializeRequest(InitializeRequest);

    function InitializeRequest(sender, args) {
        //  alert(jQuery("input[id$=hdHasMeasurement]").val());
        if (jQuery("input[id$=hdHasMeasurement]").length > 0) {
            if (jQuery("input[id$=hdHasMeasurement]").val() == 'True' || jQuery("input[id$=hdHasMeasurement]").val() == 'true') {  //-------------If there are measurements under this opt. run, then only able to create DLP
                console.log(jQuery("input[id$=hdHasMeasurement]").val());
                var updateProgress = $get('<%= UpdateProgressClientID %>');
                var postBackElement = args.get_postBackElement();
                if (postBackElement.id == '<%= lbtnDownloadAndSaveDLP.ClientID %>' || postBackElement.id == '<%= lbtnMeasurement.ClientID %>') {
                    if (($('#<%= lbtnDownloadAndSaveDLP.ClientID %>').length > 0 && $get('<%= lbtnDownloadAndSaveDLP.ClientID %>').text == "Next") || postBackElement.id == '<%= lbtnMeasurement.ClientID %>') {
                        updateProgress.control._associatedUpdatePanelId = null;
                        $get('<%= CreateDLPUpdateProgressClientID %>').control._associatedUpdatePanelId = 'dummyId';
                    }

                    else {
                        updateProgress.control._associatedUpdatePanelId = 'dummyId';
                    }
                }
                else if (postBackElement.id == '<%= lbtnSubmit.ClientID %>') {
                    updateProgress.control._associatedUpdatePanelId = null;
                    $get('<%= CreateDLPUpdateProgressClientID %>').control._associatedUpdatePanelId = 'dummyId';
                }
                else {
                    if (postBackElement.id == '<%= lbtnMeasurementVerification.ClientID %>') {
                        $get('<%= CreateDLPUpdateProgressClientID %>').control._associatedUpdatePanelId = 'dummyId';
                    }
                    else {
                        updateProgress.control._associatedUpdatePanelId = null;
                    }
                }
            }
            else
                updateProgress.control._associatedUpdatePanelId = null;
        }

    }    
</script>
<script type="text/javascript" language="javascript">
    function ConfirmMeasurement() {
        if (confirm('<%= AddMeasurementDeleteMeasurementConfirmation %>'))
            return true;
        else
            return false;
    }

    function ShowClosePrintConditionWindow(projectId, projectName, licenseTypeId, DlpName) {
        var oWindow;
        oWindow = window.radopen("PopupClosePrintCondition.aspx?projectId=" + projectId + "&projectName=" + projectName + "&licenseTypeId=" + licenseTypeId + "&DlpName=" + DlpName + "&ToSubOperation=0", "ClosePrintConditionWindow");
        oWindow.show();
        return false;
    }

    function CloseClosePrintConditionWindow() {
        var oWnd = $find("<%= ClosePrintConditionWindow.ClientID %>");
        oWnd.close();
    }
    function ShowReportGradingWindow() {
        var gradingPath = getPathForReportGrading();
        var oWnd = $find("<%= ReportGradingWindow.ClientID %>");
        oWnd.setUrl(gradingPath);
        oWnd.show();
        return false;
    }
    function Close() {
        var oWnd = $find("<%= ReportGradingWindow.ClientID %>");
        oWnd.close();
        return false;
    }

    function ShowReportCMYKDensityWindow() {
        var cmykDensityPath = getPathForCMYKDensityReport();
        var oWnd = $find("<%= ReportCMYKDensityWindow.ClientID %>");
        oWnd.setUrl(cmykDensityPath);
        oWnd.show();
        return false;
    }
    function CloseReportCMYKDensityWindow() {
        var oWnd = $find("<%= ReportCMYKDensityWindow.ClientID %>");
        oWnd.close();
        return false;
    }

</script>
<script type="text/javascript" language="javascript">
    $(document).ready(function () {

        if (!IsPrinterInstalled()) {
            $('input[type="image"][id*="ibtnPrintPreview"]').hide();
            $('input[type="image"][id*="ibtnPrintLabel"]').hide();
        }
        //Default Action
        $(".hideclass").hide(); //Hide all content
        $("ul.tabs li:eq(" + $('#<%= hdnSelectedTabIndex.ClientID %>').val() + ")").addClass("active").show(); //Activate first tab
        $(".hideclass:eq(" + $('#<%= hdnSelectedTabIndex.ClientID %>').val() + ")").show(); //Show first tab content
        //On Click Event
        $("ul.tabs li").click(function () {
            $("ul.tabs li").removeClass("active"); //Remove any "active" class
            $('#<%= hdnSelectedTabIndex.ClientID %>').val($(this).index());
            $(this).addClass("active"); //Add "active" class to selected tab
            $(".hideclass").hide(); //Hide all tab content
            var activeTab = $(this).find("a").attr("href"); //Find the rel attribute value to identify the active tab + content
            $(activeTab).fadeIn(); //Fade in the active content

            return false;
        });

        $('#chk1DMeasurementSmoothing').on('change', function (e) {
            e.preventDefault();
            CallOneDMeasurement($(this).prop('checked'));
        });
    });

    function CallOneDMeasurement(value) {
        var res = PageMethods.Set1DMeasurementSmoothingSessionValue(value.toString());
    }
</script>

<script type="text/javascript" language="javascript">

    //According to new or old Project open rad window
    function ChooseRadWindow() {

        if (jQuery("input[id$=hdHasMeasurement]").length > 0) {

            if (jQuery("input[id$=hdHasMeasurement]").val() == 'True' || jQuery("input[id$=hdHasMeasurement]").val() == 'true') {  //-------------If there are measurements under this opt. run, then only able to create DLP

                if ("<%= IsNewDlp %>" == "True") {
                    return true;
                }
                else {
                    if ("<%= IsDlpCreated %>" == "True") {
                        return true;
                    }
                    else {
                        var oWnd = $find("<%= ConfirmSaveWindow.ClientID %>");
                        oWnd.show();
                        return false;
                    }
                }
            }
            else
                return false;
        }
    }
</script>
<%--<script src="JS/ChartTooltip.js" type="text/javascript"></script>--%>
<script language="javascript" type="text/javascript">
    function funTVIVisiblity() {
        document.getElementById("trTVI").style.display = "";
        document.getElementById("trSpread").style.display = "none";
        document.getElementById("trCMYK").style.display = "none";
    }
    function funSpreadVisiblity() {
        document.getElementById("trTVI").style.display = "none";
        document.getElementById("trSpread").style.display = "";
        document.getElementById("trCMYK").style.display = "none";
    }

    function funCMYKVisiblity() {
        document.getElementById("trTVI").style.display = "none";
        document.getElementById("trSpread").style.display = "none";
        document.getElementById("trCMYK").style.display = "";
    }

</script>


<script language="javascript" type="text/javascript">
    jQuery(document).ready(function () {
        $("#accordionNew").click
            (
            function () {
                var $panel1 = $("div[id*='panelabGamutComparisonNew']");
                var $img1 = $("div[id*='imgHeaderNew']");
                $panel1.slideToggle(500);
                if ($img1.hasClass('expanded')) {
                    $img1.addClass('collapsed').removeClass('expanded');
                }
                else {
                    $img1.addClass('expanded').removeClass('collapsed');
                }
                return false;
            });

    });

</script>

<div class="contant_top">
    &nbsp;
</div>
<div class="contant_middle">
    <asp:UpdatePanel ID="upCreateDLP" runat="server">
        <ContentTemplate>
            <div class="head">
                <asp:HiddenField runat="server" ID="hdnBaseServiceUrl" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="hdnCurrentUserID" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="hdnVerification" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="hdnSync1DType" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="hdfCurrentCulture" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="hdnValidationType" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="hdnCurrentDlpSubOperation" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="hdnReportDLPName" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="hdnOptimizationRunID" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="hdnProjectID" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="hdnCurrentMeasurement" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="hdnDefaultSelectedDelta" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="hdnCurrentProjectLicenseType" ClientIDMode="Static" />
                <asp:HiddenField ID="hdHasMeasurement" runat="server" Value="0" ClientIDMode="Static" />
                <asp:HiddenField ID="hdPatchMatricSum" runat="server" Value="-1" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnSelectedTabIndex" runat="server" Value="0" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="hdnBasePluginServiceUrl" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="hdnCurrentDeviceName" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="hdnValidationMeasurementStatus" ClientIDMode="Static" />

                <h1 id="header" runat="server">
                    <asp:Localize runat="server" Text="Analyze" ID="btnAnalyze" meta:resourcekey="btnAnalyzeResource1"></asp:Localize></h1>
                <div class="Information_divAnalyze">
                    <asp:LinkButton ID="lbtnDownloadAndSaveDLP" runat="server" CssClass="button" OnClick="lbtnDownloadAndSaveDLP_Click"
                        OnClientClick="return ChooseRadWindow();">
                        <span>
                            <asp:Image ID="Image2" runat="server" ImageUrl="../Images/export.png" meta:resourcekey="Image2Resource1" />
                            <asp:Localize runat="server" Text="Create DLP" ID="locCreateDLP" meta:resourcekey="locCreateDLPResource1"></asp:Localize>
                        </span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lbtnSubmit" CssClass="buttonNormal" runat="server" Visible="false"
                        OnClick="lbtnSubmit_Click">
                        <span>
                            <asp:Localize runat="server" Text="Submit" ID="locSubmit" meta:resourcekey="locSubmitResource1"></asp:Localize>
                        </span>
                    </asp:LinkButton>
                    &nbsp;
                    <asp:LinkButton ID="lbtnMeasurement" runat="server" CssClass="buttonLeft" OnClick="lbtnMeasurement_Click">
                        <span>
                            <asp:Localize runat="server" Text="Measurement" ID="locMeasurement" meta:resourcekey="locMeasurementResource1"></asp:Localize>
                        </span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lbtnMeasurementVerification" runat="server" CssClass="button"
                        OnClick="lbtnMeasurementVerification_Click">
                        <span>
                            <asp:Image ID="imgAnalyze" runat="server" ImageUrl="../Images/analyze.png" />
                            <asp:Localize runat="server" Text="Measure" ID="locMeasurementVerification" meta:resourcekey="locMeasurementVerificationResource1"></asp:Localize>
                        </span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lbtnOptimize" CssClass="buttonNormal" runat="server" Visible="false">
                        <span>
                            <asp:Localize runat="server" Text="Optimize" ID="locOptimize" meta:resourcekey="locOptimizeResource1"></asp:Localize>
                        </span>
                    </asp:LinkButton>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="listDevice" style="margin-left: 10px;">
        <uc:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
    </div>
    <table cellpadding="0" cellspacing="10" border="0" width="97%" style="margin-left: 10px;">
        <tr id="trRBtnListDeltaE" runat="server">
            <td align="center">
                <%--<asp:RadioButtonList ID="rbtnListDeltaE" runat="server" RepeatDirection="Horizontal"
                    AutoPostBack="true" OnSelectedIndexChanged="rbtnListDeltaE_SelectedIndexChanged"
                    CssClass="RadioList" ClientIDMode="Static">
                    <asp:ListItem Text="ΔE2000" Value="E2000" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="ΔE" Value="E"></asp:ListItem>
                </asp:RadioButtonList>--%>
                <asp:RadioButtonList ID="rbtnListDeltaE" runat="server" RepeatDirection="Horizontal"
                    CssClass="RadioList" ClientIDMode="Static">
                    <asp:ListItem Text="ΔE2000" Value="E2000" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="ΔE" Value="E"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td id="tdAverageOfMeasurements" runat="server" style="display: none;">
                            <%--<asp:CheckBox ID="chkAnalyzeAverageOfMeasurements" runat="server" Checked="false" EnableViewState="true"
                                Font-Bold="true" AutoPostBack="true" OnCheckedChanged="chkAnalyzeAverageOfMeasurements_Click"></asp:CheckBox>--%>
                            <asp:CheckBox ID="chkAnalyzeAverageOfMeasurements" runat="server" Checked="false" EnableViewState="true" ClientIDMode="Static"
                                Font-Bold="true"></asp:CheckBox>
                            <span style="font-weight: bold; vertical-align: text-top;">
                                <asp:Localize runat="server" Text="Analyze Average Of Measurements" ID="LocAvgMesurment"
                                    meta:resourcekey="LocAvgMesurmentResource1"></asp:Localize>
                            </span>
                            <asp:Label ID="lblCompError" runat="server" Text="" ForeColor="Red"></asp:Label>
                            <input id="hd_CompError" type="hidden" runat="server" value="5" />
                        </td>
                        <td id="td1DSmoothing" runat="server" align="right" visible="false">
                            <%--<asp:CheckBox ID="chk1DMeasurementSmoothing" runat="server" Checked="false" EnableViewState="true"
                                Font-Bold="true" AutoPostBack="true"
                                OnCheckedChanged="chk1DMeasurementSmoothing_CheckedChanged"></asp:CheckBox>--%>
                            <asp:CheckBox ID="chk1DMeasurementSmoothing" runat="server" Checked="false" EnableViewState="true"
                                Font-Bold="true" ClientIDMode="Static"></asp:CheckBox>
                            <span style="font-weight: bold; vertical-align: text-top;">
                                <asp:Localize runat="server" Text="Enable output smoothing" ID="Localize2"></asp:Localize>
                            </span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trListHeader" runat="server">
            <td id="Td1" runat="server">
                <div style="display: block;" class="contantbox tabcontant1">
                    <div class="Analyze_head">
                        <div id="divAnalyzeHeader" class="Analyze_head_text"
                            style="font-weight: bold; font-size: 14px; float: left; display: none;">
                            <asp:Localize runat="server" Text="Analyze Measurement :" ID="Localize21"
                                meta:resourcekey="locAnaluzeMeasurementResource1"></asp:Localize>
                            <asp:Label ID="lblNewAnalyzeLabel" Style="font-size: 12px !important;" runat="server" ClientIDMode="Static"></asp:Label>
                        </div>
                        <div id="divAnalyzePagingSection" style="display: none;">
                            <asp:Label ID="lblPagingMsg" runat="server" ClientIDMode="Static" Text="Record(s) {start} to {end} of {total}"></asp:Label>
                            &nbsp;|&nbsp;<asp:ImageButton
                                ID="btnFirst" runat="server" ImageUrl="~/Images/Nav_FirstPage_off.gif" ClientIDMode="Static" />&nbsp;
                                <asp:ImageButton ID="btnPrevious" runat="server" ClientIDMode="Static"
                                    ImageUrl="~/Images/Nav_PreviousPage_off.gif" Style="height: 13px" />&nbsp;
                                    <asp:ImageButton ID="btnNext" runat="server" ImageUrl="~/Images/Nav_NextPage_off.gif" ClientIDMode="Static" />&nbsp;
                                        <asp:ImageButton ID="btnLast" runat="server" ImageUrl="~/Images/Nav_LastPage_off.gif" ClientIDMode="Static" />&nbsp;|&nbsp;
                                        <asp:Localize runat="server" ID="Localize22" Text="Records" meta:resourcekey="locRecordsResource1"></asp:Localize>&nbsp;
                                        <asp:DropDownList ID="ddlView" runat="server" CssClass="DROPDOWNLIST2" ClientIDMode="Static">
                                            <asp:ListItem Value="5" Selected="True">5</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="25">25</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                        </asp:DropDownList>
                            &nbsp;|&nbsp;<asp:Localize Text="Go to page" runat="server" ID="Localize23" meta:resourcekey="locGoToPageResource1"></asp:Localize>&nbsp;
                            <asp:DropDownList ID="ddlPaging" runat="server" CssClass="DROPDOWNLIST2" Width="45px" ClientIDMode="Static">
                            </asp:DropDownList>
                            &nbsp;
                        </div>
                    </div>
                    <div class="section_middle">
                        <center>
                            <div class="chart-wrap" style="position: relative;">
                                <div id="divAnalyzeChart" style="height: 200px;background-color:#CECECE"></div>
                                <div class="chart-loading"></div>
                            </div>
                        </center>
                    </div>
                </div>
            </td>
        </tr>
    </table>

    <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="upCreateDLP"
        DisplayAfter="50">
        <ProgressTemplate>
            <AjaxToolkit:AlwaysVisibleControlExtender ID="AlwaysVisibleControl" HorizontalSide="Center"
                VerticalSide="Middle" TargetControlID="updateProgress" HorizontalOffset="325"
                VerticalOffset="210" runat="server" Enabled="True">
            </AjaxToolkit:AlwaysVisibleControlExtender>
            <div style="background: #CCC !important;">
                <uc:CreateDLP runat="server" ID="ucCreateDLP" />
            </div>
            <div class="UpdateProgressBackgroundLessOptimize" style="position: fixed; left: 0px; top: 0px; z-index: 1000; width: 100%; height: 100%;"
                unselectable="on">
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div class="listDevice" style="margin-left: 10px;">
        <div id="divTabContainerLoading" class="analyze-loader-container">
            <img src="Images/analyze-loader.gif" style="margin-top:50px;" />
        </div>
        <div id="divToRender" style="display:none;">
            <table cellpadding="0" cellspacing="10" border="0" width="101%">
                <tr>
                    <td>
                        <div>
                            <div id="tabstrip">
                                <ul>
                                    <li class="k-state-active">
                                        <asp:Localize Text="Summary" ID="locSummary"
                                            runat="server" meta:resourcekey="locSummaryResource1"></asp:Localize>
                                    </li>
                                    <li>
                                        <asp:Localize Text="Ink Paper" ID="locInkPaper"
                                            runat="server" meta:resourcekey="locInkPaperResource1"></asp:Localize>
                                    </li>
                                    <li>
                                        <asp:Localize Text="G7" ID="locG7" runat="server"
                                            meta:resourcekey="locG7Resource1"></asp:Localize>
                                    </li>
                                    <li>
                                        <asp:Localize Text="Gamut" ID="locGray" runat="server"
                                            meta:resourcekey="locGrayResource1"></asp:Localize>
                                    </li>
                                    <li>
                                        <asp:Localize Text="TVI" ID="locTVI" runat="server"
                                            meta:resourcekey="locTVIResource1"></asp:Localize>
                                    </li>
                                    <li>
                                        <asp:Localize Text="Patch Details" ID="locPatchDetails"
                                            runat="server" meta:resourcekey="locPatchDetailsResource1"></asp:Localize>
                                    </li>
                                    <li>
                                        <asp:Localize Text="Report" ID="locReport" runat="server"
                                            meta:resourcekey="locReportResource1"></asp:Localize>
                                    </li>
                                </ul>

                                <%--Measurement summary tab--%>
                                <div>
                                    <div>

                                        <div id="divValidationSummary" cellpadding="0" cellspacing="0" width="650px" style="display: none;">
                                            <div style="float: right; padding-top: 2px; position: absolute; top: 0; right: 0; padding-right: 4px; padding-top: 5px">
                                                <asp:ImageButton ID="ibtnPrintPreview" runat="server"
                                                    ImageUrl="../Images/DymoPrintPreviewIcon.png" ToolTip="Preview label"
                                                    OnClientClick="ShowLabelPrintPreview(); return false;"
                                                    meta:resourcekey="ibtnPrintPreviewResource1" />
                                                <asp:ImageButton ID="ibtnPrintLabel" runat="server"
                                                    ImageUrl="../Images/DymoPrintIcon.png" ToolTip="Print label"
                                                    OnClientClick="PrintLabel(false); return false;"
                                                    meta:resourcekey="ibtnPrintLabelResource1" />
                                            </div>
                                            <table style="width: 100%; border-collapse: collapse;">
                                                <tr>
                                                    <td valign="top" style="width: 63%; border: 1px Solid Gray;">
                                                        <table id="tblValidationSummary" class="Validation_Summary">
                                                            <tr class="Gray">
                                                                <td class="Summary_Left" style="width: 35%;">
                                                                    <asp:Localize ID="Localize25" runat="server" meta:resourcekey="ValidationSummaryMeasurementName"></asp:Localize>
                                                                </td>
                                                                <td class="Summary_Right" style="width: 65%;">
                                                                    <span data-bind="text: MeasurementName"></span>
                                                                </td>
                                                            </tr>
                                                            <tr data-bind="visible: isCurrentDlpSubOperation">
                                                                <td class="Summary_Left_Alt">
                                                                    <asp:Localize ID="Localize26" runat="server" meta:resourcekey="ValidationSummaryStatus"></asp:Localize>
                                                                </td>
                                                                <td class="Summary_Right_Alt">
                                                                    <span data-bind="text: MeasurementStatus, style: { color: MeasurementStatus == 'OK' ? 'green' : 'black' }"></span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="Summary_Left">
                                                                    <asp:Localize ID="Localize27" runat="server" meta:resourcekey="ValidationSummaryMetricPreset"></asp:Localize>
                                                                </td>
                                                                <td class="Summary_Right">
                                                                    <span data-bind="text: MetricTargetPresetName"></span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="Summary_Left_Alt">
                                                                    <asp:Localize ID="Localize28" runat="server" meta:resourcekey="ValidationSummaryPrintStd"></asp:Localize>
                                                                </td>
                                                                <td class="Summary_Right_Alt">
                                                                    <span data-bind="text: PrintStandardType"></span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="Summary_Left">
                                                                    <asp:Localize ID="Localize30" runat="server" meta:resourcekey="ValidationSummaryMeasurementID"></asp:Localize>
                                                                </td>
                                                                <td class="Summary_Right">
                                                                    <span data-bind="text: MeasurementId"></span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="Summary_Left_Alt">
                                                                    <asp:Localize ID="Localize31" runat="server" meta:resourcekey="ValidationSummaryTimestamp"></asp:Localize>
                                                                </td>
                                                                <td class="Summary_Right_Alt">
                                                                    <span data-format="MM/dd/yyyy HH:mm:ss tt" data-bind="text: MeasurementDateFormatted"></span>
                                                                </td>
                                                            </tr>
                                                            <tr id="rowCustomer" data-bind="visible: isCurrentDlpSubOperation">
                                                                <td class="Summary_Left">
                                                                    <asp:Localize ID="Localize32" runat="server" meta:resourcekey="ValidationSummaryCustomer"></asp:Localize>
                                                                </td>
                                                                <td class="Summary_Right">
                                                                    <span data-bind="text: Customer"></span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="Summary_Left_Alt">
                                                                    <asp:Localize ID="Localize33" runat="server" meta:resourcekey="ValidationSummaryEType"></asp:Localize>
                                                                </td>
                                                                <td class="Summary_Right_Alt">
                                                                    <span data-bind="text: dEMethod"></span>
                                                                </td>
                                                            </tr>
                                                            <tr id="rowPageSurface" data-bind="visible: isSync1DLicenseType">
                                                                <td class="Summary_Left">
                                                                    <asp:Localize ID="Localize29" runat="server" meta:resourcekey="ValidationSummaryPageSurface"></asp:Localize>
                                                                </td>
                                                                <td class="Summary_Right">
                                                                    <span data-bind="text: PageOrSurface"></span>
                                                                </td>
                                                            </tr>
                                                            <tr id="rowJobID" data-bind="visible: isSync1DLicenseType">
                                                                <td class="Summary_Left_Alt">
                                                                    <asp:Localize ID="Localize34" runat="server" meta:resourcekey="ValidationSummaryJobID"></asp:Localize>
                                                                </td>
                                                                <td class="Summary_Right_Alt">
                                                                    <span data-bind="text: JDFJobId"></span>
                                                                </td>
                                                            </tr>
                                                            <tr id="rowNotes" data-bind="visible: isCurrentDlpSubOperation">
                                                                <td class="Summary_Left">
                                                                    <asp:Localize ID="Localize35" runat="server" meta:resourcekey="ValidationSummaryNotes"></asp:Localize>
                                                                </td>
                                                                <td class="Summary_Right">
                                                                    <span data-bind="text: Notes"></span>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table id="tblAverageMeasurementSummary" class="Validation_Summary" style="width: 100%; display: none;">
                                                            <tr class="Gray">
                                                                <td style="width: 100%;">
                                                                    <span>Averaging all measurements!</span>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 2%;"></td>
                                                    <td valign="top">
                                                        <table class="summaryMetricTable k-grid k-widget k-display-block" style="width: 100%">
                                                            <thead class="k-grid-header">
                                                                <tr style="line-height: 25px;">
                                                                    <th>
                                                                        <asp:Localize ID="locGridMetricSumName" runat="server" meta:resourcekey="GridMetricSumName" Text=""></asp:Localize></th>
                                                                    <th>
                                                                        <asp:Localize ID="locGridTarget" runat="server" meta:resourcekey="GridTargetValue" Text=""></asp:Localize></th>
                                                                    <th>
                                                                        <asp:Localize ID="locGridActual" runat="server" meta:resourcekey="GridMeasuredValue" Text=""></asp:Localize></th>
                                                                </tr>
                                                            </thead>
                                                            <tbody data-template="validation-metrics-template" data-bind="source: ValidationMetrics"></tbody>
                                                        </table>
                                                        <br />
                                                        <table id="tblPatchTolerance" class="summaryMetricTable k-grid k-widget k-display-block" style="width: 100%">
                                                            <thead class="k-grid-header">
                                                                <tr style="line-height: 25px;">
                                                                    <th>
                                                                        <asp:Localize ID="Localize36" runat="server" meta:resourcekey="GridPatchTolSampleName" Text="SampleName"></asp:Localize></th>
                                                                    <th>
                                                                        <asp:Localize ID="Localize37" runat="server" meta:resourcekey="GridTargetValue" Text="Tol"></asp:Localize></th>
                                                                    <th>
                                                                        <asp:Localize ID="Localize38" runat="server" meta:resourcekey="GridMeasuredValue" Text="Actual"></asp:Localize></th>
                                                                </tr>
                                                            </thead>
                                                            <tbody data-template="patch-tolerance-template" data-bind="source: PatchTolerances"></tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div id="divMeasurementSummary" style="display: none;">
                                            <table id="tblMeasurementSummary" cellspacing="0" cellpadding="0" border="0" class="product_list_table" style="margin: 0; width: 100%">
                                                <tr>
                                                    <td>
                                                        <table cellspacing="0" cellpadding="0" border="0" class="product_list_table" style="margin: 0">
                                                            <tr class="Gray">
                                                                <td style="font-size: 13px; width: 200px;">
                                                                    <asp:Localize ID="locMeasurementName" runat="server" meta:resourcekey="locMeasurementNameResource1" Text=""></asp:Localize>
                                                                </td>
                                                                <td>
                                                                    <span data-bind="text: MeasurementName"></span>
                                                                </td>
                                                            </tr>
                                                            <tr class="White">
                                                                <td style="font-size: 13px;">
                                                                    <asp:Localize Text="MeasurementID:" ID="Localize24" runat="server" meta:resourcekey="locMeasurementIDResource1"></asp:Localize>
                                                                </td>
                                                                <td>
                                                                    <span data-bind="text: MeasurementId"></span>
                                                                </td>
                                                            </tr>
                                                            <tr class="Gray">
                                                                <td style="font-size: 13px;">
                                                                    <asp:Localize Text="Upload Date:" ID="locUploadDate" runat="server" meta:resourcekey="locUploadDateResource1"></asp:Localize>
                                                                </td>
                                                                <td>
                                                                    <span data-bind="text: MeasurementDate" data-dateformat="yyyy-MM-dd"></span>
                                                                </td>
                                                            </tr>
                                                            <tr class="White">
                                                                <td style="font-size: 13px;">
                                                                    <asp:Localize Text="Average Δ" ID="locAverageDelta" runat="server" meta:resourcekey="locAverageDeltaResource1"></asp:Localize>
                                                                <td>
                                                                    <span data-bind="text: Avg_ΔE"></span>
                                                                </td>
                                                            </tr>
                                                            <tr class="Gray">
                                                                <td style="font-size: 13px;">
                                                                    <asp:Localize Text="Maximum Δ" ID="locMaximumDelta"
                                                                        runat="server" meta:resourcekey="locMaximumDeltaResource1"></asp:Localize>
                                                                    <asp:Label ID="lblMaxΔE" runat="server"></asp:Label></td>
                                                                <td>
                                                                    <span data-bind="text: Max_ΔE"></span>
                                                                </td>
                                                            </tr>

                                                            <tr class="White">
                                                                <td style="font-size: 13px;">
                                                                    <asp:Localize Text="Maximum Primary Δ" runat="server"
                                                                        ID="locMaxPrimaryDelta" meta:resourcekey="locMaxPrimaryDeltaResource1"></asp:Localize>
                                                                <td>
                                                                    <span data-bind="text: Pri_ΔE"></span>
                                                                </td>
                                                            </tr>
                                                            <tr class="Gray">
                                                                <td style="font-size: 13px;">
                                                                    <asp:Localize Text="Maximum Primary ΔH:"
                                                                        ID="locMaxPrimaryDeltaH" runat="server" meta:resourcekey="locMaxPrimaryDeltaHResource1"></asp:Localize></td>
                                                                <td>
                                                                    <span data-bind="text: Pri_ΔH"></span>
                                                                </td>
                                                            </tr>
                                                            <tr class="White">
                                                                <td style="font-size: 13px;">
                                                                    <asp:Localize Text="Gray Average Δ"
                                                                        ID="locGrayAvgDelta" runat="server" meta:resourcekey="locGrayAvgDeltaResource1"></asp:Localize>
                                                                <td>
                                                                    <span data-bind="text: Gray_ΔE"></span>
                                                                </td>
                                                            </tr>

                                                            <tr class="Gray">
                                                                <td style="font-size: 13px;">
                                                                    <asp:Localize Text="10th Percentile Δ"
                                                                        ID="loc10PercentileDelta" runat="server"
                                                                        meta:resourcekey="loc10PercentileDeltaResource1"></asp:Localize>
                                                                </td>
                                                                <td>
                                                                    <span data-bind="text: PercΔE_10"></span>
                                                                </td>
                                                            </tr>
                                                            <tr class="White">
                                                                <td style="font-size: 13px;">
                                                                    <asp:Localize Text="90th Percentile Δ"
                                                                        ID="loc90PercentileDelta" runat="server"
                                                                        meta:resourcekey="loc90PercentileDeltaResource1"></asp:Localize>
                                                                </td>
                                                                <td>
                                                                    <span data-bind="text: PercΔE_90"></span>
                                                                </td>
                                                            </tr>
                                                            <tr class="Gray">
                                                                <td style="font-size: 13px;">
                                                                    <asp:Localize Text="95th Percentile Δ"
                                                                        ID="loc95PercentileDelta" runat="server"
                                                                        meta:resourcekey="loc95PercentileDeltaResource1"></asp:Localize>
                                                                </td>
                                                                <td>
                                                                    <span data-bind="text: PercΔE_95"></span>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                                <%--Ink paper chart tab--%>
                                <div>
                                    <div>
                                        <div>
                                            <table border="1" cellpadding="0" cellspacing="0" width="100%" style="background-color: White;">
                                                <tr>
                                                    <td style="width: 50%;" valign="top" class="CHARTTABLE">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td class="green-title">&nbsp; &Delta;<asp:Label ID="Label6" runat="server"></asp:Label>
                                                                    <asp:Localize ID="Localize3" Text=" Primary Patches" runat="server"
                                                                        meta:resourcekey="LocalizeResource1"></asp:Localize>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 60%; vertical-align: bottom;" id="TD9" runat="server">
                                                                    <div id="divChartInkPaperABComparision"></div>

                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 50%;" valign="top" class="CHARTTABLE">
                                                        <table id="tblDensityChart" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td class="green-title">&nbsp;
                                                                                <asp:Localize Text="Density" runat="server" ID="Localize18"
                                                                                    meta:resourcekey="locDensityResource1"></asp:Localize>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">

                                                                        <tr>
                                                                            <td>
                                                                                <div id="divChartInkPaperDansityChart"></div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <%--<tr>
                                                                <td class="green-title">&nbsp; &#916;
                                                                                <asp:Localize Text="Paper L* a* b*" runat="server" ID="Localize19"
                                                                                    meta:resourcekey="locPaperResource1"></asp:Localize>
                                                                </td>
                                                            </tr>--%>
                                                            <tr>
                                                                <td>
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-spacing: 1px;">
                                                                        <tr>
                                                                            <td class="green-title">
                                                                                <span id="spanlocPaperAB">
                                                                                    <asp:Localize Text="Paper a* b*" runat="server" ID="locPaperAB"
                                                                                        meta:resourcekey="locPaperABResource1"></asp:Localize>
                                                                                </span>
                                                                                <a id="lnkPaperPaperLab1LargeChart" style="float: right">
                                                                                    <asp:Image ID="imgPaperPaperLab1LargeChart" ClientIDMode="Static" runat="server" ImageUrl="../Images/eye_blue.png" Style="height: 22px;" />
                                                                                </a>
                                                                            </td>
                                                                            <td class="green-title">
                                                                                <span id="spanlocPaperL">
                                                                                    <asp:Localize Text="Paper L*" runat="server" ID="locPaperL"
                                                                                        meta:resourcekey="locPaperLResource1"></asp:Localize>
                                                                                </span>
                                                                                <a id="lnkPaperPaperLab2LargeChart" style="float: right">
                                                                                    <asp:Image ID="imgPaperPaperLab2LargeChart" ClientIDMode="Static" runat="server" ImageUrl="../Images/eye_blue.png" Style="height: 22px;" />
                                                                                </a>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <div id="divTest" class="gamutChart">
                                                                                    <div id="divChartInkPaperPaperLab1"></div>
                                                                                </div>
                                                                            </td>

                                                                            <td align="center">
                                                                                <div id="divChartInkPaperPaperLab2"></div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>

                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div style="display: block;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <div id="divChartSummaryDeltaChart"></div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <%--G7 chart tab--%>
                                <div>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" width="650px">
                                            <tr>
                                                <td valign="top" style="width: 59%">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                                        <tr>
                                                            <td class="green-title">&nbsp;
                                                                    <span id="spanGrayBalanceDeltaCH">
                                                                        <asp:Localize Text="Gray Balance ΔCh" runat="server"
                                                                            ID="locGrayBalanceDeltaCH" meta:resourcekey="locGrayBalanceDeltaCHResource1"></asp:Localize>
                                                                    </span>
                                                                <a id="lnkGrayBalanceDeltaCHLargeChart" style="float: right; margin-right: 5px;">
                                                                    <asp:Image ID="imgGrayBalanceDeltaCHLargeChart" ClientIDMode="Static" runat="server" ImageUrl="../Images/eye_blue.png" Style="height: 22px;" />
                                                                </a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">
                                                                <div id="divChartG7GrayBalanceDeltaCh"></div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 2%;"></td>
                                                <td style="width: 39%" valign="top">
                                                    <div id="divGraySummaryList">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                                            <tr>
                                                                <td class="green-title">&nbsp;
                                                                                <asp:Localize Text="Gray  ΔCh Summary" runat="server"
                                                                                    ID="locGrayDeltaCHSummary" meta:resourcekey="locGrayDeltaCHSummaryResource1"></asp:Localize>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" style="padding: 2px">
                                                                    <table class="k-grid k-widget k-display-block" style="width: 100%">
                                                                        <thead class="k-grid-header">
                                                                            <tr style="line-height: 25px;">
                                                                                <th>
                                                                                    <asp:Localize Text="Metric" runat="server"
                                                                                        ID="localGrayHeaderMetric" meta:resourcekey="GridMetricSumName"></asp:Localize></th>
                                                                                <th>
                                                                                    <asp:Localize Text="Tolerance" runat="server"
                                                                                        ID="localGrayHeaderTolerance" meta:resourcekey="GridToleranceValue"></asp:Localize></th>
                                                                                <th>
                                                                                    <asp:Localize Text="Actual" runat="server"
                                                                                        ID="localGrayHeaderActual" meta:resourcekey="GridMeasuredValue"></asp:Localize></th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody data-template="gray-summary" data-bind="source: GrayFSummaryList"></tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>

                                        <table cellpadding="0" cellspacing="0" width="650px">
                                            <tr>
                                                <td valign="top" style="width: 59%">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                                        <tr>
                                                            <td class="green-title">&nbsp;
                                                                <span id="spanBlackDeltaL">
                                                                    <asp:Localize Text="Black ΔL" runat="server" ID="locBlackDeltaL"
                                                                        meta:resourcekey="locBlackDeltaLResource1"></asp:Localize>
                                                                </span>
                                                                <a id="lnkBlackDeltaLLargeChart" style="float: right; margin-right: 5px;">
                                                                    <asp:Image ID="imgBlackDeltaLLargeChart" ClientIDMode="Static" runat="server" ImageUrl="../Images/eye_blue.png" Style="height: 22px;" />
                                                                </a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">
                                                                <div id="divChartG7BlackDeltaL"></div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 2%;"></td>
                                                <td style="width: 39%" valign="top">
                                                    <div id="divBlackSummaryList">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                                            <tr>
                                                                <td class="green-title">&nbsp;
                                                                                <asp:Localize Text="Black  ΔL Summary" runat="server"
                                                                                    ID="Localize15" meta:resourcekey="locBlackDeltaLSummaryResource1"></asp:Localize>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" style="padding: 2px">
                                                                    <table class="k-grid k-widget k-display-block" style="width: 100%">
                                                                        <thead class="k-grid-header">
                                                                            <tr style="line-height: 25px;">
                                                                                <th>
                                                                                    <asp:Localize Text="Metric" runat="server"
                                                                                        ID="localBlackHeaderMetric" meta:resourcekey="GridMetricSumName"></asp:Localize></th>
                                                                                <th>
                                                                                    <asp:Localize Text="Tolerance" runat="server"
                                                                                        ID="localBlackHeaderTolerance" meta:resourcekey="GridToleranceValue"></asp:Localize></th>
                                                                                <th>
                                                                                    <asp:Localize Text="Actual" runat="server"
                                                                                        ID="localBlackHeaderActual" meta:resourcekey="GridMeasuredValue"></asp:Localize></th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody data-template="gray-summary" data-bind="source: BlackLSummaryList"></tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>

                                        <table cellpadding="0" cellspacing="0" width="650px">
                                            <tr>
                                                <td valign="top" style="width: 59%">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                                        <tr>
                                                            <td class="green-title">&nbsp;
                                                                <span id="spanGrayDeltaL">
                                                                    <asp:Localize runat="server" Text="Gray ΔL" ID="locGrayDeltaL"
                                                                        meta:resourcekey="locGrayDeltaLResource1"></asp:Localize>
                                                                </span>
                                                                <a id="lnkGrayDeltaLLargeChart" style="float: right; margin-right: 5px;">
                                                                    <asp:Image ID="imgGrayDeltaLLargeChart" ClientIDMode="Static" runat="server" ImageUrl="../Images/eye_blue.png" Style="height: 22px;" />
                                                                </a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">
                                                                <div id="divChartG7GrayDeltaL"></div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 2%;"></td>
                                                <td style="width: 39%" valign="top">
                                                    <div id="divGrayLDeltaSummaryList">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                                            <tr>
                                                                <td class="green-title">&nbsp;
                                                                                <asp:Localize runat="server" Text="Gray  ΔL Summary"
                                                                                    ID="Localize17" meta:resourcekey="locGrayDeltaLSummaryResource1"></asp:Localize>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" style="padding: 2px">
                                                                    <table class="k-grid k-widget k-display-block" style="width: 100%">
                                                                        <thead class="k-grid-header">
                                                                            <tr style="line-height: 25px;">
                                                                                <th>
                                                                                    <asp:Localize Text="Metric" runat="server"
                                                                                        ID="localGrayLHeaderMetric" meta:resourcekey="GridMetricSumName"></asp:Localize></th>
                                                                                <th>
                                                                                    <asp:Localize Text="Tolerance" runat="server"
                                                                                        ID="localGrayLHeaderTolerance" meta:resourcekey="GridToleranceValue"></asp:Localize></th>
                                                                                <th>
                                                                                    <asp:Localize Text="Actual" runat="server"
                                                                                        ID="localGrayLHeaderActual" meta:resourcekey="GridMeasuredValue"></asp:Localize></th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody data-template="gray-summary" data-bind="source: GrayLSummaryList"></tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>

                                        <table cellpadding="0" cellspacing="0" width="650px">
                                            <tr>
                                                <td style="width: 49%">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                                        <tr>
                                                            <td class="green-title">&nbsp; Black NPD
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">
                                                                <div id="divChartG7BlackNPD"></div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 2%;"></td>
                                                <td style="width: 49%;">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                                        <tr>
                                                            <td class="green-title">&nbsp; Gray NPD
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div id="divChartG7GrayNPD"></div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>

                                <%--Gamut or gray chart tab--%>
                                <div>
                                    <div>
                                        <table border="1" cellpadding="0" cellspacing="0" width="200px" style="background-color: White;">
                                            <tr>
                                                <td style="width: 45%;" valign="top" class="CHARTTABLE">

                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td class="green-title">&nbsp;
                                                                                <asp:Localize runat="server" Text="Gamut Plot" ID="Localize4"
                                                                                    meta:resourcekey="locGamutPlotResource1"></asp:Localize>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table id="Table2" runat="server" cellpadding="0" cellspacing="0" border="0" width="100%" class="SYNCTABLE">
                                                                    <tr id="Tr5" runat="server">
                                                                        <td id="Td6" class="green-title" runat="server">
                                                                            <asp:Localize ID="Localize5" runat="server" Text="a* - b* Comparison"
                                                                                meta:resourcekey="locComparisonResource1"></asp:Localize>
                                                                            <%--<img style="height: 22px;" src="../Images/eye_blue.png" />--%>
                                                                            <a id="lnkABComparisionLargeChart" style="float: right; margin-right: 5px;">
                                                                                <asp:Image ID="imgABComparisionLargeChart" ClientIDMode="Static" runat="server" ImageUrl="../Images/eye_blue.png" Style="height: 22px;" /></a>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td align="center" valign="top">
                                                                            <div id="divTest" class="gamutChart">
                                                                                <div id="kendoChartTargetVersusActualAB"></div>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <div style="padding-left: -5px;" class="green-title">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td style="width: 93%">
                                                                                            <div id="accordionNew" class="header collapsibleContainerTitle">
                                                                                                <div>
                                                                                                    <div>
                                                                                                        <div style="float: left">
                                                                                                        </div>
                                                                                                        <div style="float: left;">
                                                                                                            <div id="imgHeaderNew" class="expanded">
                                                                                                                &nbsp;
                                                                                                            </div>
                                                                                                            <span id="divABGamutComparision">
                                                                                                                <asp:Localize ID="Localize6" runat="server" Text="a* - b* Gamut Comparison" meta:resourcekey="locabGamutCompResource1"></asp:Localize>
                                                                                                            </span>
                                                                                                        </div>
                                                                                                        <div class="clear">
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div>
                                                                                                <a id="lnkAB_ComparisionLargeChart">
                                                                                                    <asp:Image ID="imgAB_ComparisionLargeChart" ClientIDMode="Static" runat="server" ImageUrl="../Images/eye_blue.png" Style="height: 22px;" />
                                                                                                </a>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>

                                                                            </div>
                                                                            <asp:Panel ID="panelabGamutComparisonNew" runat="server" BorderStyle="None"
                                                                                Width="100%" meta:resourcekey="panelabGamutComparisonResource1">
                                                                                <div id="kendoChartGamut_ABComparison" style="min-height: 250px; min-width: 300px;" class="gamutChart"></div>
                                                                            </asp:Panel>

                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Tr6" runat="server">
                                                                        <td id="Td7" class="green-title" runat="server">
                                                                            <asp:Localize runat="server" Text="L* Comparison" ID="Localize7"
                                                                                meta:resourcekey="locLComparisonResource1"></asp:Localize>
                                                                            <a id="lnkLComparisionLargeChart" style="float: right; margin-right: 5px">
                                                                                <asp:Image ID="imgLComparisionLargeChart" ClientIDMode="Static" runat="server" ImageUrl="../Images/eye_blue.png" Style="height: 22px;" />
                                                                            </a>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Tr7" runat="server">
                                                                        <td id="Td8" align="center" valign="top" runat="server">
                                                                            <div id="kendoChartLComaparision" style="min-height: 80px; min-width: 300px;"></div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>

                                                <td style="width: 55%;" valign="top">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                                                    <tr>
                                                                        <td class="green-title">&nbsp;
                                                                                            <asp:Localize runat="server" Text="Gray Balance L* a* b*"
                                                                                                ID="Localize8" meta:resourcekey="locGrayBalanceLABResource1"></asp:Localize>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" style="min-width: 350px;">
                                                                                <tr>
                                                                                    <td class="green-title">
                                                                                        <span id="spanGrayBalanceL">
                                                                                            <asp:Localize runat="server" Text="Gray Balance L*"
                                                                                                ID="locGrayBalanceL" meta:resourcekey="locGrayBalanceLResource1"></asp:Localize>
                                                                                        </span>
                                                                                        <a id="lnk_L_GrayBalanceLargeChart" style="float: right; margin-right: 5px;">
                                                                                            <asp:Image ID="imgLGrayBalanceLargeChart" ClientIDMode="Static" runat="server" ImageUrl="../Images/eye_blue.png" Style="height: 22px;" /></a>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <div id="kendoChartLBalanceChart"></div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="green-title">
                                                                                        <span id="spanGrayBalanceA">
                                                                                            <asp:Localize runat="server" Text="Gray Balance a*"
                                                                                                ID="locGrayBalanceA" meta:resourcekey="locGrayBalanceAResource1"></asp:Localize>
                                                                                        </span>
                                                                                        <a id="lnk_A_GrayBalanceLargeChart" style="float: right; margin-right: 5px;">
                                                                                            <asp:Image ID="imgAGrayBalanceLargeChart" ClientIDMode="Static" runat="server" ImageUrl="../Images/eye_blue.png" Style="height: 22px;" /></a>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <div id="kendoChartABalanceChart"></div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="green-title">
                                                                                        <span id="spanGrayBalanceB">
                                                                                            <asp:Localize runat="server" Text="Gray Balance b*"
                                                                                                ID="locGrayBalanceB" meta:resourcekey="locGrayBalanceBResource1"></asp:Localize>
                                                                                        </span>
                                                                                        <a id="lnk_B_GrayBalanceLargeChart" style="float: right; margin-right: 5px;">
                                                                                            <asp:Image ID="imgBGrayBalanceLargeChart" ClientIDMode="Static" runat="server" ImageUrl="../Images/eye_blue.png" Style="height: 22px;" /></a>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <div id="kendoChartBBalanceChart"></div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <div style="display: none;">
                                                                                            <div id="kendoChartDummy">
                                                                                            </div>
                                                                                            <div id="kendoChartDummyBalanceL"></div>
                                                                                            <div id="kendoChartDummyBalanceA"></div>
                                                                                            <div id="kendoChartDummyBalanceB"></div>
                                                                                            <div id="divChartInkPaperDummy"></div>
                                                                                            <div id="kendoChartGamut_ABComparisonDummy"></div>
                                                                                            <div id="kendoChartTargetVersusActualABDummy"></div>
                                                                                            <div id="kendoChartInkPaperLabOneDummy"></div>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>

                                <%--TVI chart tab--%>
                                <div>
                                    <div>

                                        <div id="divTVI_ISO">
                                            <div id="tabstripTVI">
                                                <ul>
                                                    <li class="k-state-active">
                                                        <asp:Localize ID="Localize9" Text="TVI" runat="server" meta:resourcekey="btnTVIResource1"></asp:Localize>
                                                    </li>
                                                    <li>
                                                        <asp:Localize ID="Localize10" Text="Spread" runat="server" meta:resourcekey="btnSpreadResource1"></asp:Localize>
                                                    </li>
                                                    <li>
                                                        <asp:Localize ID="Localize11" Text="CMYK" runat="server" meta:resourcekey="btnCMYKResource1"></asp:Localize>
                                                    </li>
                                                </ul>
                                                <div>
                                                    <div>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="SYNCTABLE">
                                                                        <tr>
                                                                            <td class="green-title" align="center">
                                                                                <asp:Localize ID="Localize1" Text="Cyan" runat="server" meta:resourcekey="locTVICyanResource1"></asp:Localize>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top" align="center">
                                                                                <div id="divChartTVIISO_CYAN">
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="SYNCTABLE">
                                                                        <tr>
                                                                            <td class="green-title" align="center">
                                                                                <asp:Localize ID="Localize39" Text="Magenta" runat="server" meta:resourcekey="locTVIMagentaResource1"></asp:Localize>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <div id="divChartTVIISO_Magenta"></div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="SYNCTABLE">
                                                                        <tr>
                                                                            <td class="green-title" align="center">
                                                                                <asp:Localize ID="Localize40" Text="Yellow" runat="server" meta:resourcekey="locTVIYellowResource1"></asp:Localize>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <div id="divChartTVIISO_Yellow">
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="SYNCTABLE">
                                                                        <tr>
                                                                            <td class="green-title" align="center">
                                                                                <asp:Localize ID="Localize20" Text="Black" runat="server" meta:resourcekey="locTVIBlackResource1"></asp:Localize>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <div id="divChartTVIISO_Black"></div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div>
                                                    <div>
                                                        <div id="divTVI_Spread">
                                                            <table cellpadding="0" cellspacing="0" width="650px">
                                                                <tr>
                                                                    <td valign="top" style="width: 59%">
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">
                                                                            <tr>
                                                                                <td valign="top">
                                                                                    <div id="divChartTVISpread"></div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-left: 60px; padding-right: 60px;">
                                                                                    <table class="TDBOX" border="0" cellpadding="5" cellspacing="0" width="90%">
                                                                                        <tr class="TRTABLEBOX" style="line-height: 30px;">
                                                                                            <td class="TDTABLEBOXLEFT" style="text-align: left;">
                                                                                                <b>
                                                                                                    <asp:Localize ID="locChromVariation" Text="Chromatic Variation" runat="server" meta:resourcekey="locChromaticVariationResource1"></asp:Localize></b>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="text-align: center;">
                                                                                            <td style="padding-left: 40px;">
                                                                                                <table id="chromVariationTable" width="100%" style="line-height: 20px;">
                                                                                                    <tr>
                                                                                                        <th data-bind="visible: Tone20 != 0">20</th>
                                                                                                        <th data-bind="visible: Tone25 != 0">25</th>
                                                                                                        <th data-bind="visible: Tone40 != 0">40</th>
                                                                                                        <th data-bind="visible: Tone50 != 0">50</th>
                                                                                                        <th data-bind="visible: Tone60 != 0">60</th>
                                                                                                        <th data-bind="visible: Tone75 != 0">75</th>
                                                                                                        <th data-bind="visible: Tone80 != 0">80</th>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td data-bind="visible: Tone20 != 0"><span data-bind="text: Tone20"></span></td>
                                                                                                        <td data-bind="visible: Tone25 != 0"><span data-bind="text: Tone25"></span></td>
                                                                                                        <td data-bind="visible: Tone40 != 0"><span data-bind="text: Tone40"></span></td>
                                                                                                        <td data-bind="visible: Tone50 != 0"><span data-bind="text: Tone50"></span></td>
                                                                                                        <td data-bind="visible: Tone60 != 0"><span data-bind="text: Tone60"></span></td>
                                                                                                        <td data-bind="visible: Tone75 != 0"><span data-bind="text: Tone75"></span></td>
                                                                                                        <td data-bind="visible: Tone80 != 0"><span data-bind="text: Tone80"></span></td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div>
                                                    <div>
                                                        <div id="divChartTVIISO_CMYK"></div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="divTVI_G7">
                                            <table cellpadding="0" cellspacing="0" width="650px">
                                                <tr>
                                                    <td valign="top" style="width: 59%">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="CHARTTABLE">

                                                            <tr>
                                                                <td valign="top">
                                                                    <div id="divChartTVISummaryDotGain"></div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>

                                    </div>
                                </div>

                                <%--Patch details tab--%>
                                <div class="tabcontant">
                                    <div>
                                        <div id="divMeasurementPatchDetails">
                                        </div>
                                        <script type="text/x-kendo-template" id="templatePatchDetails">                                                         
                                                        <div id="divMetricSum" style="float:left;">
                                                        <asp:Localize Text="Filter by Metric Sum:" runat="server"
                                                                            ID="locFilterMetricSumTemplate" meta:resourcekey="locFilterMetricSumResource1"></asp:Localize>
                                                            <input type="search" id="ddlMatricSum" style="width: 150px"/>
                                                        </div>
                                                        <div class="toolbar" style="float:right;">
                                                            <a id='lnkLinkFilter' title='Clear filter'>
                                                                <img id='imgClearFilter' src='Images/clear-fillter.png' style='width: 15px;margin-top: 5px;margin-right: 5px;'></img>
                                                            </a>
                                                            <button id="btnExportCGATS" class="k-button">Export to CGATS</button>
                                                            <button id="btnExportCxF" class="k-button">Export to CxF</button>
                                                            
                                                        </div>
                                        </script>
                                    </div>
                                </div>

                                <%--report generation tab--%>
                                <div>
                                    <div id="divReport">
                                        <table id="TblGeneratePDFReport" runat="server" border="0" width="100%" style="height: 100%;"
                                            cellpadding="0" cellspacing="0" class="TABLE1">
                                            <tr id="Tr4" runat="server">
                                                <td id="Td5" valign="top" runat="server">
                                                    <a id="btnGeneratePDF">
                                                        <img src="Images/processbar.gif" style="padding-left: 10px;" /></a>
                                                </td>
                                                <td valign="top">
                                                    <div id="Div_ReportGrading" runat="server" visible="false" style="float: right; padding-right: 10px;">
                                                        <asp:ImageButton ID="btnReportGrading" runat="server" ImageUrl="../Images/Sync DLP Scoring Report_preview.jpg" ToolTip="Scoring Report" OnClientClick="javascript:return ShowReportGradingWindow();"></asp:ImageButton>
                                                    </div>
                                                    <div id="Div_ReportCMYKDensity" runat="server" visible="false" style="float: right; padding-right: 10px;">
                                                        <asp:ImageButton ID="btnReportCMYKDensity" runat="server" ImageUrl="../Images/Sync1D CMYK Density Report_preview.jpg" ToolTip="CMYK Density Report" OnClientClick="javascript:return ShowReportCMYKDensityWindow();"></asp:ImageButton>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <div id="kendoChartTargetVersusActualABLargeArea">
                <div id="kendoChartTargetVersusActualABLarge" class="gamutChart"></div>
            </div>
            <div id="kendoChart_AB_ComparisionLargeArea">
                <div id="kendoChart_AB_ComparisionLarge" class="gamutChart"></div>
            </div>
            <div id="kendoChart_L_ComparisionLargeArea">
                <div id="kendoChart_L_ComparisionLarge"></div>
            </div>
            <div id="kendoChart_G7_LargeArea">
                <div id="kendoChart_G7_Large"></div>
            </div>
            <div id="kendoChart_A_Balance_LargeArea">
                <div id="kendoChart_A_Balance_Large"></div>
            </div>
            <div id="kendoChart_B_Balance_LargeArea">
                <div id="kendoChart_B_Balance_Large"></div>
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
</div>
<div class="contant_bottom">
    &nbsp;
</div>








<telerik:RadWindowManager ID="RadWindowManager" runat="server" Modal="True" meta:resourcekey="RadWindowManagerResource1">
    <Windows>
        <telerik:RadWindow ID="ConfirmSaveWindow" runat="server" ShowContentDuringLoad="false"
            VisibleStatusbar="false" Title="Choose Next Action" Width="702" Height="250"
            Behaviors="Move,Close" NavigateUrl="~/PopupConfirmSave.aspx" meta:resourcekey="ConfirmSaveWindowResource1">
        </telerik:RadWindow>
        <telerik:RadWindow ID="ClosePrintConditionWindow" runat="server" VisibleStatusbar="false"
            ShowContentDuringLoad="false" Title="Close Print Condition" Width="720" Height="200"
            Behaviors="Move,Close">
        </telerik:RadWindow>
        <telerik:RadWindow ID="ReportGradingWindow" runat="server" VisibleStatusbar="false"
            ShowContentDuringLoad="false" Title="Import Measurements" Width="730" Height="420"
            Behaviors="Close, Move" Behavior="Close, Move">
        </telerik:RadWindow>
        <telerik:RadWindow ID="ReportCMYKDensityWindow" runat="server" VisibleStatusbar="false"
            ShowContentDuringLoad="false" Title="CMYK Density" Width="730" Height="420"
            Behaviors="Close, Move" Behavior="Close, Move">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>

<script id="validation-metrics-template" type="text/x-kendo-template">
     #if(IsNotOK){#
        <tr style="color:black">
    #}else{#
    <tr style="color:green">
    #}#
        <td data-bind="text: Metric"></td>
        <td data-bind="text: Tolerance"></td>
        <td data-bind="text: Actual"></td>
    </tr>
</script>

<script id="patch-tolerance-template" type="text/x-kendo-template">
     #if(IsNotOK){#
        <tr style="color:black">
    #}else{#
    <tr style="color:green">
    #}#
        <td data-bind="text: SampleName"></td>
        <td data-bind="text: Tolerance"></td>
        <td data-bind="text: Actual"></td>
    </tr>
</script>

<script id="gray-summary" type="text/x-kendo-template">     
     #if(IsNotOK){#
        <tr style="color:black">
    #}else{#
    <tr style="color:green">
    #}#
        <td data-bind="text: Metric"></td>
        <td data-bind="text: Tolerance"></td>
        <td data-bind="text: Actual"></td>        
    </tr>
</script>
