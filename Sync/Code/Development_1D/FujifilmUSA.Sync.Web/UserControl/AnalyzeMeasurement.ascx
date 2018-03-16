<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AnalyzeMeasurement.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AnalyzeMeasurement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/PopupControl/CreateDLP.ascx" TagName="CreateDLP"
    TagPrefix="uc" %>

<script src="JS/Printing.js?version=<%=JsRandomNumber %>" type="text/javascript"></script>
<link href="Content/kendo/2017.2.621/kendo.common.min.css" rel="stylesheet" />
<link href="Content/kendo/2017.2.621/kendo.default.min.css" rel="stylesheet" />
<style>
    #divMeasurementPatchDetails .k-grid td {
        line-height: 18px;
    }
</style>
<script src="http://www.labelwriter.com/software/dls/sdk/js/DYMO.Label.Framework.2.0.2.js" type="text/javascript" charset="UTF-8"> </script>
<script src="JS/AnalyzeMeasurement.js" type="text/javascript"></script>

<%--<script type="text/javascript">

    function CheckAvgCompability() {

        var answer = document.getElementById('ContentPlaceHolder1_ucAnalyze_hd_CompError').value;

        if (answer == "0") {
            setTimeout('__doPostBack(\'ctl00$ContentPlaceHolder1$ucAnalyze$chkAnalyzeAverageOfMeasurements\',\'\')', 0);
            return true;
        }
        else if (answer == "1") {
            jQuery("[id*='lblCompError']").html("Error in different ReferenceCharts, Please select only measurements which used same reference charts.");
            return false;
        }
        else if (answer == "2") {
            jQuery("[id*='lblCompError']").html("Error in different MetricTargetPresets, Please select only measurements which used same metric target presets.");
            return false;
        }
        else {
            return false;
        }
    }

</script>--%>

<%--<script type="text/javascript">
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
</script>--%>
<%--<script type="text/javascript" language="javascript">
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
        var oWnd = $find("<%= ReportGradingWindow.ClientID %>");
        oWnd.show();
        return false;
    }
    function Close() {
        var oWnd = $find("<%= ReportGradingWindow.ClientID %>");
        oWnd.close();
        return false;
    }

    function ShowReportCMYKDensityWindow() {
        var oWnd = $find("<%= ReportCMYKDensityWindow.ClientID %>");
        oWnd.show();
        return false;
    }
    function CloseReportCMYKDensityWindow() {
        var oWnd = $find("<%= ReportCMYKDensityWindow.ClientID %>");
        oWnd.close();
        return false;
    }

</script>--%>
<%--<script type="text/javascript" language="javascript">
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
    });
</script>--%>

<%--<script type="text/javascript" language="javascript">

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
<script src="JS/ChartTooltip.js" type="text/javascript"></script>--%>
<%--<script language="javascript" type="text/javascript">
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

</script>--%>


<%--<script language="javascript" type="text/javascript">
    jQuery(document).ready(function () {
        $("div[id='accordion']").click
            (
            function () {
                var $panel = $("div[id*='panelabGamutComparison']");
                var $img = $("div[id*='imgHeader']");
                $panel.slideToggle(500);
                if ($img.hasClass('expanded')) {
                    $img.addClass('collapsed').removeClass('expanded');
                }
                else {
                    $img.addClass('expanded').removeClass('collapsed');
                }
                return false;
            }
            );

        //$("div[id='accordion']").click();

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
            }
            );

        //$("#accordionNew").click();
    });

</script>--%>

<div class="contant_top">
    &nbsp;
</div>
<div class="contant_middle">
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
                        <td id="tdAverageOfMeasurements" runat="server" visible="false">
                            <%--<asp:CheckBox ID="chkAnalyzeAverageOfMeasurements" runat="server" Checked="false" EnableViewState="true"
                                Font-Bold="true" AutoPostBack="true" OnCheckedChanged="chkAnalyzeAverageOfMeasurements_Click"></asp:CheckBox>--%>
                            <asp:CheckBox ID="chkAnalyzeAverageOfMeasurements" runat="server" Checked="false" EnableViewState="true"
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
                                Font-Bold="true"></asp:CheckBox>
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
                            <asp:Label ID="lblPagingMsg" runat="server" ClientIDMode="Static" Text="Record(s) {start} to {end} of {total}" Width="140"></asp:Label>
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
                        <center><div id="divAnalyzeChart"></div></center>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <div class="listDevice" style="margin-left: 10px;">
        <div id="divToRender" runat="server">
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
                                        <asp:Localize Text="Report" ID="locTVI" runat="server"
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
                                <div>
                                    <div>
                                        
                                                <%--<div id="divValidationSummary" cellpadding="0" cellspacing="0" width="650px">

                                                    <table style="width: 100%;border-collapse:collapse;">
                                                        <tr>
                                                            <td valign="top" style="width: 63%; border: 1px Solid Gray;">
                                                                <table class="Validation_Summary" style="width: 100%">
                                                                    <tr class="Gray">
                                                                        <td class="Summary_Left" style="width:150px;">
                                                                            <asp:Localize ID="Localize25" runat="server" meta:resourcekey="ValidationSummaryMeasurementName"></asp:Localize>
                                                                        </td>
                                                                        <td class="Summary_Right">
                                                                            <span data-bind="text: MeasurementName"></span>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="Summary_Left_Alt">
                                                                            <asp:Localize ID="Localize26" runat="server" meta:resourcekey="ValidationSummaryStatus"></asp:Localize>
                                                                        </td>
                                                                        <td class="Summary_Right_Alt">
                                                                            <span data-bind="text: MeasurementStatus, style: { color: MeasurementStatus == 'OK' ? 'green':'black'}"></span>                                                                                
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
                                                                    <tr id="rowCustomer">
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
                                                                    <tr id="rowPageSurface">
                                                                        <td class="Summary_Left">
                                                                            <asp:Localize ID="Localize29" runat="server" meta:resourcekey="ValidationSummaryPageSurface"></asp:Localize>
                                                                        </td>
                                                                        <td class="Summary_Right">
                                                                            <span data-bind="text: PageOrSurface"></span>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="rowJobID">
                                                                        <td class="Summary_Left_Alt">
                                                                            <asp:Localize ID="Localize34" runat="server" meta:resourcekey="ValidationSummaryJobID"></asp:Localize>
                                                                        </td>
                                                                        <td class="Summary_Right_Alt">
                                                                            <span data-bind="text: JDFJobId"></span>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="rowNotes">
                                                                        <td class="Summary_Left">
                                                                            <asp:Localize ID="Localize35" runat="server" meta:resourcekey="ValidationSummaryNotes"></asp:Localize>
                                                                        </td>
                                                                        <td class="Summary_Right">
                                                                            <span data-bind="text: Notes"></span>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 2%;"></td>
                                                            <td valign="top">                                                                
                                                                    <table class="k-grid k-widget k-display-block" style="width:100%">
                                                                    <thead class="k-grid-header">
                                                                        <tr style="line-height: 25px;">
                                                                            <th><asp:Localize ID="locGridMetricSumName" runat="server" meta:resourcekey="GridMetricSumName" Text=""></asp:Localize></th>
                                                                            <th><asp:Localize ID="locGridTarget" runat="server" meta:resourcekey="GridTargetValue" Text=""></asp:Localize></th>
                                                                            <th><asp:Localize ID="locGridActual" runat="server" meta:resourcekey="GridMeasuredValue" Text=""></asp:Localize></th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody data-template="validation-metrics-template" data-bind="source: ValidationMetrics"></tbody>
                                                                </table>
                                                                <br />
                                                                <table id="tblPatchTolerance" class="k-grid k-widget k-display-block" style="width:100%">
                                                                    <thead class="k-grid-header">
                                                                        <tr style="line-height: 25px;">
                                                                            <th><asp:Localize ID="Localize36" runat="server" meta:resourcekey="GridPatchTolSampleName" Text="SampleName"></asp:Localize></th>
                                                                            <th><asp:Localize ID="Localize37" runat="server" meta:resourcekey="GridTargetValue" Text="Tol"></asp:Localize></th>
                                                                            <th><asp:Localize ID="Localize38" runat="server" meta:resourcekey="GridMeasuredValue" Text="Actual"></asp:Localize></th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody data-template="patch-tolerance-template" data-bind="source: PatchTolerances"></tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>--%>

                                                <div id="divMeasurementSummary">
                                                    <table id="tblMeasurementSummary" cellspacing="0" cellpadding="0" border="0" class="product_list_table" style="margin: 0; width: 100%">
                                                        <tr>
                                                            <td>
                                                                <table cellspacing="0" cellpadding="0" border="0" class="product_list_table" style="margin: 0">
                                                                    <tr class="Gray">
                                                                        <td style="font-size: 13px;width: 200px;"">
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

                                                            <tr>
                                                                <td class="green-title">&nbsp; &#916;
                                                                                <asp:Localize Text="Paper L* a* b*" runat="server" ID="Localize19"
                                                                                    meta:resourcekey="locPaperResource1"></asp:Localize>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <div id="divChartInkPaperPaperLab1"></div>
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
                                            <div style="display: none;">
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
                                <div>
                                    <div>
                                        <p>G7</p>
                                    </div>
                                </div>
                                <div>
                                    <div>
                                        <p>Gamut</p>
                                    </div>
                                </div>
                                <div>
                                    <div>
                                        <p>TVI</p>
                                    </div>
                                </div>
                                <div>
                                    <div>
                                        <p>Patch Details</p>
                                    </div>
                                </div>
                                <div>
                                    <div>
                                        <p>Report</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="clear">
    </div>
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
