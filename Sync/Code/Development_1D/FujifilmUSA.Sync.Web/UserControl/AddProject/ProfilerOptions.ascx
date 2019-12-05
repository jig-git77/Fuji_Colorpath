<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ProfilerOptions.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ProfilerOptions" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>

<link href="Content/kendo/2018.1.221/kendo.common.min.css" rel="stylesheet" />
<link href="Content/kendo/2018.1.221/kendo.default.min.css" rel="stylesheet" />
<script src="Scripts/kendo/2018.1.221/kendo.all.min.js"></script>

<style>
    .style1 {
        width: 150px;
        text-align: left;
    }

    .style4 {
        width: 80px;
    }

    .checkboxFont {
    }

        .checkboxFont Label {
            font-size: 12px;
        }

    textarea {
        resize: none;
    }

    .textwidth35 {
        width: 35px;
    }

    .textwidth30 {
        width: 30px;
    }

    #tabstrip h2 {
        font-weight: lighter;
        font-size: 5em;
        line-height: 1;
        padding: 0 0 0 30px;
        margin: 0;
    }

        #tabstrip h2 span {
            background: none;
            padding-left: 5px;
            font-size: .3em;
            vertical-align: top;
        }

    #tabstrip p {
        margin: 0;
        padding: 0;
    }
</style>

<script type="text/javascript">
    function validateTVI(evt) {

        if (evt.charCode > 31 && evt.charCode != 46 && (evt.charCode < 48 || evt.charCode > 57)) {
            alert('<%= ProfilerOptions_JavascriptAllownumbers %>');
            return false;
        }
    }

    function validateTVIRange(txtValue) {

        var txtTVI = document.getElementById(txtValue.id);
        var txtTVIvalue = document.getElementById(txtValue.id).value;
        if (txtTVIvalue > 50) {
            alert('<%= ProfilerOptions_JavascriptTVIRange %>');
            txtTVI.value = "";
            txtTVI.SetFocus();
            return false;
        }

        else
            return true;

    }

    function validateDensityRange(txtValue) {

        var txtDensity = document.getElementById(txtValue.id);
        var txtDensityvalue = document.getElementById(txtValue.id).value;
        if (txtDensityvalue > 2) {
            alert('<%= ProfilerOptions_JavascriptDensityRange %>');
            txtDensity.value = "";
            txtDensity.SetFocus();
            return false;
        }

        else
            return true;

    }

    function validateHueAngleRange(txtValue) {

        var txtHue = document.getElementById(txtValue.id);
        var txtHuevalue = document.getElementById(txtValue.id).value;
        if (txtHuevalue > 400) {
            <%--alert('<%= ProfilerOptions_JavascriptHueRange %>');--%>
            alert('out of range');
            txtHue.value = "";
            txtHue.SetFocus();
            return false;
        }

        else
            return true;

    }

    function clientValueChange(sender, eventArgs) {
        //var myDiv = document.getElementById("myDiv")
        //myDiv.innerHTML = "Old value is:" + eventArgs.get_oldValue()
        // + "  New value:" + sender.get_value();

        document.getElementById("<%=txtBlackStart.ClientID%>").val(sender.get_value());
    }

    /*Custom Profiler DLS set Slider value*/
    var rdsGCRAmountSilderCustom;
    var rdsBlackStartSliderCustom;
    var rdsBlackWidthSliderCustom;
    /*  Max and Min Value for GCR SlideBar */
    var MAXCustomGCRValue = -1;
    var MINCustomGCRValue = -2;
    var SettingBlackGeneration = "1.75";

    var rdsGrayBalanceSliderCustom;
    var rdsCoolerWarmerSliderCustom;
    var rdsTACSliderCustom;
    var rdsBlackTACSliderCustom;
    var rdsMaxBlackSliderCustom;

    function clientValueChangerdsGrayBalance(sender, eventArgs) {
        var txtGrayBalance = document.getElementById("<%=txtGrayBalance.ClientID%>")
        txtGrayBalance.value = sender.get_value();
        AdvancedPerceptualRenderingControl(txtGrayBalance.value);
    }
    function clientValueChangerdsCoolerWarmer(sender, eventArgs) {
        var txtCoolerWarmer = document.getElementById("<%=txtCoolerWarmer.ClientID%>")
        txtCoolerWarmer.value = sender.get_value();
        $("#<%=lblSettingPerceptualGrayCorrection.ClientID%>").text(txtCoolerWarmer.value);
        $("#<%=hdnSettingPerceptualGrayCorrection.ClientID%>").val(txtCoolerWarmer.value);
    }

    function clientValueChangerdsGCRAmount(sender, eventArgs) {
        var txtGCRAmount = document.getElementById("<%=txtGCRAmount.ClientID%>")
        txtGCRAmount.value = sender.get_value();
        SettingBlackGeneration = parseFloat(MINCustomGCRValue) + parseFloat(sender.get_value() / 100);
        $("#<%=lblSettingBlackGeneration.ClientID%>").text(SettingBlackGeneration);
        $("#<%=hdnSettingBlackGeneration.ClientID%>").val(SettingBlackGeneration);
    }
    function clientValueChangerdsBlackStartCustom(sender, eventArgs) {
        var txtBlackStartCustom = document.getElementById("<%=txtBlackStartCustom.ClientID%>")
        txtBlackStartCustom.value = sender.get_value();
        $("#<%=lblSettingBlackStart.ClientID%>").text(txtBlackStartCustom.value);
        $("#<%=hdnSettingBlackStart.ClientID%>").val(txtBlackStartCustom.value);
    }
    function clientValueChangerdsBlackWidthCustom(sender, eventArgs) {
        var txtBlackWidthCustom = document.getElementById("<%=txtBlackWidthCustom.ClientID%>")
        txtBlackWidthCustom.value = sender.get_value();
        $("#<%=lblSettingBlackWidth.ClientID%>").text(txtBlackWidthCustom.value);
        $("#<%=hdnSettingBlackWidth.ClientID%>").val(txtBlackWidthCustom.value);
    }


    /*OnClientLoad RadSlider*/
    function OnClientLoadrdsGrayBalance(sender, eventArgs) {
        rdsGrayBalanceSliderCustom = sender;
        rdsGrayBalanceSliderCustom.set_value('0');
        var txtGrayBalance = document.getElementById("<%=txtGrayBalance.ClientID%>")
        txtGrayBalance.value = rdsGrayBalanceSliderCustom.get_value();
        AdvancedPerceptualRenderingControl(txtGrayBalance.value);
    }
    function OnClientLoadrdsCoolerWarmer(sender, eventArgs) {
        rdsCoolerWarmerSliderCustom = sender;
        rdsCoolerWarmerSliderCustom.set_value('0');

        var txtCoolerWarmer = document.getElementById("<%=txtCoolerWarmer.ClientID%>")
        txtCoolerWarmer.value = rdsCoolerWarmerSliderCustom.get_value();

        $("#<%=lblSettingPerceptualGrayCorrection.ClientID%>").text(txtCoolerWarmer.value);
        $("#<%=hdnSettingPerceptualGrayCorrection.ClientID%>").val(txtCoolerWarmer.value);
    }
    function OnClientLoadrdsGCRAmount(sender, eventArgs) {
        rdsGCRAmountSilderCustom = sender;
        rdsGCRAmountSilderCustom.set_value('25');
        var txtGCRAmount = document.getElementById("<%=txtGCRAmount.ClientID%>")
        txtGCRAmount.value = rdsGCRAmountSilderCustom.get_value();

        SettingBlackGeneration = parseFloat(MINCustomGCRValue) + parseFloat(sender.get_value() / 100);
        $("#<%=lblSettingBlackGeneration.ClientID%>").text(SettingBlackGeneration);
        $("#<%=hdnSettingBlackGeneration.ClientID%>").val(SettingBlackGeneration);
        rdsGCRAmountSilderCustom.set_enabled(true);

        $("#<%=trSettingBlackStart.ClientID%>").show();
        $("#<%=trSettingBlackWidth.ClientID%>").show();

    }
    function OnClientLoadrdsBlackStartCustom(sender, eventArgs) {
        rdsBlackStartSliderCustom = sender;
        rdsBlackStartSliderCustom.set_enabled(true);
        rdsBlackStartSliderCustom.set_value('15');

        var txtBlackStartCustom = document.getElementById("<%=txtBlackStartCustom.ClientID%>")
        txtBlackStartCustom.value = rdsBlackStartSliderCustom.get_value();
        $("#<%=lblSettingBlackStart.ClientID%>").text(txtBlackStartCustom.value);
        $("#<%=hdnSettingBlackStart.ClientID%>").val(txtBlackStartCustom.value);


    }
    function OnClientLoadrdsBlackWidthCustom(sender, eventArgs) {
        rdsBlackWidthSliderCustom = sender;
        rdsBlackWidthSliderCustom.set_enabled(true);
        rdsBlackWidthSliderCustom.set_value('100');
        var txtBlackWidthCustom = document.getElementById("<%=txtBlackWidthCustom.ClientID%>")
        txtBlackWidthCustom.value = rdsBlackWidthSliderCustom.get_value();
        $("#<%=lblSettingBlackWidth.ClientID%>").text(txtBlackWidthCustom.value);
        $("#<%=hdnSettingBlackWidth.ClientID%>").val(txtBlackWidthCustom.value);


    }
    function OnClientLoadrdsTAC(sender, eventArgs) {
        rdsTACSliderCustom = sender;
        rdsTACSliderCustom.set_value('320');
        var txtTAC = document.getElementById("<%=txtTAC.ClientID%>")
        txtTAC.value = rdsTACSliderCustom.get_value();
        $("#<%=lblSettingInTotal.ClientID%>").text(txtTAC.value);
        $("#<%=hdnSettingInTotal.ClientID%>").val(txtTAC.value);

    }
    function OnClientLoadrdsBlackTAC(sender, eventArgs) {
        rdsBlackTACSliderCustom = sender;
        rdsBlackTACSliderCustom.set_value('320');
        var txtBlackTAC = document.getElementById("<%=txtBlackTAC.ClientID%>")
        txtBlackTAC.value = rdsBlackTACSliderCustom.get_value();
        $("#<%=lblSettingBlackTAC.ClientID%>").text(txtBlackTAC.value);
        $("#<%=hdnSettingBlackTAC.ClientID%>").val(txtBlackTAC.value);


    }
    function OnClientLoadrdsMaxBlack(sender, eventArgs) {
        rdsMaxBlackSliderCustom = sender;
        rdsMaxBlackSliderCustom.set_value('100');
        var txtMaxBlack = document.getElementById("<%=txtMaxBlack.ClientID%>")
        txtMaxBlack.value = rdsMaxBlackSliderCustom.get_value();

    }


    function clientValueChangerdsTAC(sender, eventArgs) {
        var txtTAC = document.getElementById("<%=txtTAC.ClientID%>")
        txtTAC.value = sender.get_value();

        $("#<%=lblSettingInTotal.ClientID%>").text(txtTAC.value);
        $("#<%=hdnSettingInTotal.ClientID%>").val(txtTAC.value);
    }
    function clientValueChangerdsBlackTAC(sender, eventArgs) {
        var txtBlackTAC = document.getElementById("<%=txtBlackTAC.ClientID%>")
        txtBlackTAC.value = sender.get_value();

        $("#<%=lblSettingBlackTAC.ClientID%>").text(txtBlackTAC.value);
        $("#<%=hdnSettingBlackTAC.ClientID%>").val(txtBlackTAC.value);
    }
    function clientValueChangerdsMaxBlack(sender, eventArgs) {
        var txtMaxBlack = document.getElementById("<%=txtMaxBlack.ClientID%>")
        txtMaxBlack.value = sender.get_value();
    }
    function OnChangecmbBalckGeneration() {
        var cmbBlackGeneration = document.getElementById("<%=cmbBlackGeneration.ClientID%>")
        if ($(cmbBlackGeneration).val() == "2") { //UCR 
            rdsGCRAmountSilderCustom.set_enabled(false);
            rdsBlackStartSliderCustom.set_enabled(true);
            rdsBlackWidthSliderCustom.set_enabled(true);
            $("#<%=lblSettingBlackGeneration.ClientID%>").text($('#<%=cmbBlackGeneration.ClientID %> option:selected').text());
            $("#<%=hdnSettingBlackGeneration.ClientID%>").val($('#<%=cmbBlackGeneration.ClientID %> option:selected').text());
            $("#<%=trSettingBlackStart.ClientID%>").show();
            $("#<%=trSettingBlackWidth.ClientID%>").show();
        }
        else if ($(cmbBlackGeneration).val() == "3") { //MaxK
            rdsGCRAmountSilderCustom.set_enabled(false);
            rdsBlackStartSliderCustom.set_enabled(true);
            rdsBlackWidthSliderCustom.set_enabled(false);
            $("#<%=lblSettingBlackGeneration.ClientID%>").text($('#<%=cmbBlackGeneration.ClientID %> option:selected').text());
            $("#<%=hdnSettingBlackGeneration.ClientID%>").val($('#<%=cmbBlackGeneration.ClientID %> option:selected').text());
            $("#<%=trSettingBlackWidth.ClientID%>").hide();

        }
        else if ($(cmbBlackGeneration).val() == "4") { //MinK
            rdsGCRAmountSilderCustom.set_enabled(false);
            rdsBlackStartSliderCustom.set_enabled(false);
            rdsBlackWidthSliderCustom.set_enabled(false);
            $("#<%=lblSettingBlackGeneration.ClientID%>").text($('#<%=cmbBlackGeneration.ClientID %> option:selected').text());
            $("#<%=hdnSettingBlackGeneration.ClientID%>").val($('#<%=cmbBlackGeneration.ClientID %> option:selected').text());
            $("#<%=trSettingBlackStart.ClientID%>").hide();
            $("#<%=trSettingBlackWidth.ClientID%>").hide();
        }
        else {
            rdsGCRAmountSilderCustom.set_enabled(true);
            rdsBlackStartSliderCustom.set_enabled(true);
            rdsBlackWidthSliderCustom.set_enabled(true);
            $("#<%=lblSettingBlackGeneration.ClientID%>").text(SettingBlackGeneration);
            $("#<%=hdnSettingBlackGeneration.ClientID%>").val(SettingBlackGeneration);
            $("#<%=trSettingBlackStart.ClientID%>").show();
            $("#<%=trSettingBlackWidth.ClientID%>").show();
        }
    }
    function OnChangecmbBlackCalculation() {
        var cmbBlackCalculation = document.getElementById("<%=cmbBlackCalculation.ClientID%>")
        var txtCustomCYAN = document.getElementById("<%=txtCustomCYAN.ClientID%>")
        var txtCustomMagenta = document.getElementById("<%=txtCustomMagenta.ClientID%>")
        var txtCustomYellow = document.getElementById("<%=txtCustomYellow.ClientID%>")
        var txtCustomBlack = document.getElementById("<%=txtCustomBlack.ClientID%>")

        if ($(cmbBlackCalculation).val() == "4") {
            $(txtCustomCYAN).show();
            $(txtCustomMagenta).show();
            $(txtCustomYellow).show();
            $(txtCustomBlack).show();
        }
        else {
            $(txtCustomCYAN).hide();
            $(txtCustomMagenta).hide();
            $(txtCustomYellow).hide();
            $(txtCustomBlack).hide();
        }
        if ($(cmbBlackCalculation).val() == "1" || $(cmbBlackCalculation).val() == "2") {
            $("#<%=lblSettingInmax.ClientID%>").text($('#<%=cmbBlackCalculation.ClientID %> option:selected').text());
            $("#<%=hdnSettingInmax.ClientID%>").val($('#<%=cmbBlackCalculation.ClientID %> option:selected').text());
        }
        else {
            $("#<%=lblSettingInmax.ClientID%>").text("");
            $("#<%=hdnSettingInmax.ClientID%>").val("");
        }
    }
    function OnChangecmbPerceptualRendering() {
        var cmbPerceptualRendering = document.getElementById("<%=cmbPerceptualRendering.ClientID%>");
        $("#<%=lblSettingSaturationCompression.ClientID%>").text($(cmbPerceptualRendering).val());
        $("#<%=lblSettingPerceptualCompression.ClientID%>").text($(cmbPerceptualRendering).val());
        $("#<%=hdnSettingSaturationCompression.ClientID%>").val($(cmbPerceptualRendering).val());
        $("#<%=hdnSettingPerceptualCompression.ClientID%>").val($(cmbPerceptualRendering).val());

    }

    function AdvancedPerceptualRenderingControl(rdsGrayBalanceValue) {
        var advancedperceptualRenderingValue = $('input[name$=rdBtnAdvancedPerceptualRendering]:checked').val();
        if (advancedperceptualRenderingValue == "1") { //Chroma

            $("#<%=trPerceptualChromaCompression.ClientID%>").show();
            $("#<%=trSaturationChromaCompression.ClientID%>").show();
            $("#<%=trPerceptualLightnessCorrection.ClientID%>").hide();
            $("#<%=trSaturationLightnessCorrection.ClientID%>").hide();
            $("#<%=trSaturationGrayCorrection.ClientID%>").hide();


            $("#<%=lblSettingPerceptualChromaCompression.ClientID%>").text(rdsGrayBalanceValue);
            $("#<%=lblSettingSaturationChromaCompression.ClientID%>").text(rdsGrayBalanceValue);

            $("#<%=hdnSettingPerceptualChromaCompression.ClientID%>").val(rdsGrayBalanceValue);
            $("#<%=hdnSettingSaturationChromaCompression.ClientID%>").val(rdsGrayBalanceValue);

        }
        else if (advancedperceptualRenderingValue == "2") { //Saturation

            $("#<%=trPerceptualChromaCompression.ClientID%>").show();
            $("#<%=trSaturationChromaCompression.ClientID%>").show();
            $("#<%=trPerceptualLightnessCorrection.ClientID%>").show();
            $("#<%=trSaturationLightnessCorrection.ClientID%>").show();
            $("#<%=trSaturationGrayCorrection.ClientID%>").show();

            $("#<%=lblSettingPerceptualChromaCompression.ClientID%>").text(rdsGrayBalanceValue);
            $("#<%=lblSettingSaturationChromaCompression.ClientID%>").text(rdsGrayBalanceValue);
            $("#<%=lblSettingPerceptualLightnessCorrection.ClientID%>").text(parseInt(rdsGrayBalanceValue) * parseInt(- 1));
            $("#<%=lblSettingSaturationLightnessCorrection.ClientID%>").text(parseInt(rdsGrayBalanceValue) * parseInt(- 1));
            $("#<%=lblSettingSaturationCompression.ClientID%>").text(rdsGrayBalanceValue);

            $("#<%=hdnSettingPerceptualChromaCompression.ClientID%>").val(rdsGrayBalanceValue);
            $("#<%=hdnSettingSaturationChromaCompression.ClientID%>").val(rdsGrayBalanceValue);
            $("#<%=hdnSettingPerceptualLightnessCorrection.ClientID%>").val(parseInt(rdsGrayBalanceValue) * parseInt(- 1));
            $("#<%=hdnSettingSaturationLightnessCorrection.ClientID%>").val(parseInt(rdsGrayBalanceValue) * parseInt(- 1));
            $("#<%=hdnSettingSaturationCompression.ClientID%>").val(rdsGrayBalanceValue);
        }
        else if (advancedperceptualRenderingValue == "3") { //Lightness
            $("#<%=trPerceptualChromaCompression.ClientID%>").hide();
            $("#<%=trSaturationChromaCompression.ClientID%>").hide();
            $("#<%=trPerceptualLightnessCorrection.ClientID%>").show();
            $("#<%=trSaturationLightnessCorrection.ClientID%>").show();
            $("#<%=trSaturationGrayCorrection.ClientID%>").hide();

            $("#<%=lblSettingPerceptualLightnessCorrection.ClientID%>").text(rdsGrayBalanceValue);
            $("#<%=lblSettingSaturationLightnessCorrection.ClientID%>").text(rdsGrayBalanceValue);

            $("#<%=hdnSettingPerceptualLightnessCorrection.ClientID%>").val(rdsGrayBalanceValue);
            $("#<%=hdnSettingSaturationLightnessCorrection.ClientID%>").val(rdsGrayBalanceValue);
        }
    }

</script>
<script lang="javascript" type="text/javascript">

    jQuery(document).ready(function () {
        $("#tabstrip").kendoTabStrip({
            animation: {
                open: {
                    effects: "fadeIn"
                }
            }
        });

        $("div[id*='accordion']").click
            (
            function () {
                var $panel = $("div[id*='panelAdvancedOptions']");
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


        // $("div[id*='accordion']").click();
        //OnChangecmbBlackCalculation();
        $('input[id*="txtTVI"]').each(function () {
            $(this).numeric({ allow: '.' });
        });

        $("#<%=chk100BlackCustom.ClientID%>").click(function () {
            var exceptionValue = $("#<%=lblSettingExceptions.ClientID%>").text();
            if ($(this).is(':checked')) {
                $("#<%=lblSettingExceptions.ClientID%>").text(parseInt(exceptionValue) + parseInt(1));
                $("#<%=hdnSettingExceptions.ClientID%>").val(parseInt(exceptionValue) + parseInt(1));
            }
            else {

                $("#<%=lblSettingExceptions.ClientID%>").text(parseInt(exceptionValue) - parseInt(1));
                $("#<%=hdnSettingExceptions.ClientID%>").val(parseInt(exceptionValue) - parseInt(1));
            }
        });
        $("#<%=chkPureGray.ClientID%>").click(function () {
            var exceptionValue = $("#<%=lblSettingExceptions.ClientID%>").text();
            if ($(this).is(':checked')) {
                $("#<%=lblSettingExceptions.ClientID%>").text(parseInt(exceptionValue) + parseInt(4));
                $("#<%=hdnSettingExceptions.ClientID%>").val(parseInt(exceptionValue) + parseInt(4));
            }
            else {
                $("#<%=lblSettingExceptions.ClientID%>").text(parseInt(exceptionValue) - parseInt(4));
                $("#<%=hdnSettingExceptions.ClientID%>").val(parseInt(exceptionValue) - parseInt(4));
            }
        });

        $("#<%=chkBrightenerCmp.ClientID%>").click(function () {
            if ($(this).is(':checked')) {
                $("#<%=lblSettingBrightenerCorrection.ClientID%>").text("0.50000");
                $("#<%=hdnSettingBrightenerCorrection.ClientID%>").val("0.50000");
            }
            else {
                $("#<%=lblSettingBrightenerCorrection.ClientID%>").text("0");
                $("#<%=hdnSettingBrightenerCorrection.ClientID%>").val("0");
            }
        });
        $("#<%=chkMeasurementCorrection.ClientID%>").click(function () {
            if ($(this).is(':checked')) {
                $("#<%=lblSettingMeasurementCorrection.ClientID%>").text("3");
                $("#<%=hdnSettingMeasurementCorrection.ClientID%>").val("3");

            }
            else {
                $("#<%=lblSettingMeasurementCorrection.ClientID%>").text("0");
                $("#<%=hdnSettingMeasurementCorrection.ClientID%>").val("0");
            }
        });
        $("#<%=rdBtnAdvancedPerceptualRendering.ClientID%>").click(function () {
            var txtGrayBalance = $("#<%=txtGrayBalance.ClientID%>").val();
            AdvancedPerceptualRenderingControl(txtGrayBalance);
        });

        SetDefaultValueCustomProfiler();
    });

    function SetDefaultValueCustomProfiler() {
        //Perceptual Rendering
        var cmbPerceptualRendering = document.getElementById("<%=cmbPerceptualRendering.ClientID%>");
        $(cmbPerceptualRendering).val('10');
        $("#<%=lblSettingSaturationCompression.ClientID%>").text($(cmbPerceptualRendering).val());
        $("#<%=lblSettingPerceptualCompression.ClientID%>").text($(cmbPerceptualRendering).val());
        $("#<%=hdnSettingSaturationCompression.ClientID%>").val($(cmbPerceptualRendering).val());
        $("#<%=hdnSettingPerceptualCompression.ClientID%>").val($(cmbPerceptualRendering).val());
        $('input[name$=rdBtnAdvancedPerceptualRendering]:checked').val('1');
        //Perceptual Rendering Ending

        //Measurement Processing
        $("#<%=lblSettingBrightenerCorrection.ClientID%>").text("0");
        $("#<%=lblSettingMeasurementCorrection.ClientID%>").text("0");
        $("#<%=hdnSettingMeasurementCorrection.ClientID%>").val("0");
        $("#<%=hdnSettingBrightenerCorrection.ClientID%>").val("0");
        //Measurement Processing Ending

        //Black Generation Start
        var cmbBlackGeneration = document.getElementById("<%=cmbBlackGeneration.ClientID%>");
        $(cmbBlackGeneration).val('1'); // Default GCR Selected      

        $("#<%=lblSettingExceptions.ClientID%>").text('0');
        $("#<%=hdnSettingExceptions.ClientID%>").val('0');
        //Black Generation ending

        ///Black Calculation
        var cmbBlackCalculation = document.getElementById("<%=cmbBlackCalculation.ClientID%>");
        $(cmbBlackCalculation).val('1');
        OnChangecmbBlackCalculation();
        //Black Calculation ending


    }

    function renderkendoTabStrip() {
        $("#tabstrip").kendoTabStrip({
            animation: {
                open: {
                    effects: "fadeIn"
                }
            }
        });
    }
</script>
<div class="head">
    <h1 id="hTitle" runat="server">
        <asp:Localize runat="server"
            Text="New Print Condition - Profiler Options" ID="locNewPrintCondition"
            meta:resourcekey="locNewPrintConditionResource1"></asp:Localize>
    </h1>
    <asp:LinkButton ID="lnkbtnProfilerOptions" runat="server" CssClass="button"
        OnClick="lnkbtnProfilerOptions_Click">
        <span>
            <asp:Image ID="Image1" runat="server"
                ImageUrl="~/Images/new_project_icon.png" meta:resourcekey="Image1Resource1" />

            <asp:Localize runat="server" Text="Measurement Setup" ID="locMeasurementSetup"
                meta:resourcekey="locMeasurementSetupResource1"></asp:Localize>
        </span>
    </asp:LinkButton>
    <asp:LinkButton ID="lnkbtnTargetColorSpace" runat="server"
        CssClass="buttonLeft" OnClick="lnkbtnTargetColorSpace_Click">
        <span>
            <asp:Image ID="imgAdd" runat="server"
                ImageUrl="~/Images/new_project_icon.png" meta:resourcekey="imgAddResource1" />

            <asp:Localize runat="server" Text="Target ColorSpace" ID="locTargetColorSpace"
                meta:resourcekey="locTargetColorSpaceResource1"></asp:Localize>
        </span>
    </asp:LinkButton>
</div>
<div class="formDeviceNoLine" style="display: none;">
    <div class="clear">
    </div>
</div>
<div class="listDevice" style="margin-left: 10px;">
    <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
    <table cellpadding="0" cellspacing="10" border="0" width="97%" id="tbOptionPreset" runat="server">
        <tr id="trListHeader" runat="server">
            <td>
                <div style="display: block;" class="contantbox tabcontant">
                    <div class="section_top">
                        &nbsp;
                    </div>
                    <div class="section_middle">
                        <div class="combopart">
                            <div class="header">
                                <div class="content">
                                    <div class="text" style="float: left;">
                                        <span>
                                            <asp:Localize runat="server" Text="Options Presets"
                                                ID="locOptionsPresets" meta:resourcekey="locOptionsPresetsResource1"></asp:Localize></span>
                                    </div>
                                    <div style="float: right">
                                        <%--  <asp:LinkButton ID="lbtnMeasurementSetup" runat="server" CssClass="gray_bt" OnClick="lbtnMeasurementSetup_Click">
                <span>Measurement Setup</span>
                                    </asp:LinkButton>--%>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="panelAddProjectDescription" Style="padding: 5px" runat="server"
                                BorderStyle="None" meta:resourcekey="panelAddProjectDescriptionResource1">
                                <table>
                                    <tr runat="server" id="trPresetoptionType" visible="false">
                                        <td style="width: 110px">
                                            <span>
                                                <asp:Localize runat="server" Text="Select Preset Type:"
                                                    ID="Localize14"></asp:Localize></span>
                                        </td>
                                        <td style="width: 450px">
                                            <asp:RadioButtonList ID="rdBtnPresetType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdBtnPresetType_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Predefined" Value="1" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Import" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Custom" Value="3"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trPresetUplod" visible="false">
                                        <td style="width: 110px">
                                            <span>
                                                <asp:Localize Text="Upload Profiler Settings (only csv)" runat="server"
                                                    ID="locUploadPofileFile" meta:resourcekey="locUploadProfilerResource1"></asp:Localize></span>
                                        </td>
                                        <td style="width: 450px">
                                            <telerik:RadAsyncUpload runat="server" ID="fnUploadProfileSettings"
                                                MaxFileInputsCount="1" PostbackTriggers="lbtnSave"
                                                meta:resourcekey="fncUploadProfileSettingsResource1">
                                            </telerik:RadAsyncUpload>
                                            <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgAddProfiles"
                                                OnClick="lbtnSave_Click">
                                                <span>
                                                    <asp:Localize runat="server" Text="Save" ID="locSave"
                                                        meta:resourcekey="locSaveResource1"></asp:Localize>
                                                </span>
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trSelectPreset">
                                        <td style="width: 110px">
                                            <span>
                                                <asp:Localize runat="server" Text="Selected Presets:"
                                                    ID="locSelectedPresets" meta:resourcekey="locSelectedPresetsResource1"></asp:Localize></span>
                                        </td>
                                        <td style="width: 450px">
                                            <asp:DropDownList ID="cmbSelectedPresets" runat="server" CssClass="select" DataValueField="Key"
                                                DataTextField="Value" AutoPostBack="True"
                                                OnSelectedIndexChanged="cmbSelectedPresets_SelectedIndexChanged"
                                                meta:resourcekey="cmbSelectedPresetsResource1">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trCustomPresetName">
                                        <td style="width: 130px">
                                            <span>
                                                <asp:Localize runat="server" Text="SCustom Preset Name"
                                                    ID="lblCustomPresetName" meta:resourcekey="lblCustomPresetName"></asp:Localize></span>

                                        </td>
                                        <td style="width: 450px">
                                            <asp:TextBox ID="txtCustomPresetName" runat="server" Enabled="False" ReadOnly="True"
                                                Text=""></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" colspan="3">
                                            <span>
                                                <asp:Localize runat="server" Text="Preset Description:"
                                                    ID="locPresetDescription" meta:resourcekey="locPresetDescriptionResource1"></asp:Localize></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtProfileDescription" runat="server" Width="110%" Height="100px"
                                                TextMode="MultiLine" Enabled="False" ReadOnly="True"
                                                Text="This preset is best used for analog devices ……"
                                                meta:resourcekey="txtProfileDescriptionResource1"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <%--<table>
                                    <tr>
                                        <td style="width: 100px">
                                            <span>
                                                <asp:Localize runat="server" Text="Selected Presets:"
                                                    ID="locSelectedPresets" meta:resourcekey="locSelectedPresetsResource1"></asp:Localize></span>
                                        </td>
                                        <td style="width: 450px">
                                            <asp:DropDownList ID="cmbSelectedPresets" runat="server" CssClass="select" DataValueField="Key"
                                                DataTextField="Value" AutoPostBack="True"
                                                OnSelectedIndexChanged="cmbSelectedPresets_SelectedIndexChanged"
                                                meta:resourcekey="cmbSelectedPresetsResource1">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rdBtnPresetType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdBtnPresetType_SelectedIndexChanged">
                                                <asp:ListItem Text="Regular" Value = "False"></asp:ListItem>
                                                <asp:ListItem Text="Custom" Value = "True"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:Localize runat="server" Text="Selected Presets:"
                                                    ID="locSelectedPresets" meta:resourcekey="locSelectedPresetsResource1"></asp:Localize>
                                            <telerik:RadAsyncUpload runat="server" ID="fnUploadProfileSettings"
                                                MaxFileInputsCount="1" PostbackTriggers="lbtnSave" 
                                                meta:resourcekey="fncUploadProfileSettingsResource1" >
                                            </telerik:RadAsyncUpload>  
                                             <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgAddProfiles"
                                            OnClick="lbtnSave_Click">
                                                    <span> <asp:Localize runat="server" Text="Save" ID="locSave" 
                                            meta:resourcekey="locSaveResource1" ></asp:Localize> 
                                             </span>
                                        </asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" colspan="3">
                                            <span>
                                                <asp:Localize runat="server" Text="Preset Description:"
                                                    ID="locPresetDescription" meta:resourcekey="locPresetDescriptionResource1"></asp:Localize></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtProfileDescription" runat="server" Width="120%" Height="100px"
                                                TextMode="MultiLine" Enabled="False" ReadOnly="True"
                                                Text="This preset is best used for analog devices ……"
                                                meta:resourcekey="txtProfileDescriptionResource1"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>--%>
                            </asp:Panel>
                            <div class="clear">
                            </div>
                        </div>
                    </div>
                    <div class="section_bottom">
                        &nbsp;
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div style="display: block;" class="contantbox tabcontant">
                    <div class="section_top">
                        &nbsp;
                    </div>
                    <div class="section_middle">
                        <div class="combopart">
                            <div id="accordion" class="header collapsibleContainerTitle">
                                <div class="content">
                                    <div class="text">
                                        <div style="float: left">
                                        </div>
                                        <div style="float: left;">
                                            <div id="imgHeader" class="expanded">
                                                &nbsp;
                                            </div>
                                            <label class="collapsibleContainerTitle">
                                                <asp:Localize runat="server" Text="Advanced Options"
                                                    ID="locAdvancedOptions" meta:resourcekey="locAdvancedOptionsResource1"></asp:Localize></label>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="upAdvancedOptions" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="panelAdvancedOptions" Style="padding: 1px" runat="server" BorderStyle="None" Width="100%" meta:resourcekey="panelAdvancedOptionsResource1">
                                        <table cellpadding="0" cellspacing="10" border="0">
                                            <tr>
                                                <td style="vertical-align: top; width: 100%">
                                                    <table cellpadding="0" cellspacing="10" border="0" id="tblregularProfileSetting" runat="server">
                                                        <tr>
                                                            <td valign="top" style="width: 320px; padding-right: 0px;">
                                                                <asp:Panel ID="panel1" Style="padding: 1px" runat="server" BorderStyle="None" Width="100%" Enabled="false">
                                                                    <div style="padding-bottom: 10px">
                                                                        <div class="section_small_top">
                                                                            &nbsp;
                                                                        </div>
                                                                        <div class="section_small_middle" style="height: 70px">
                                                                            <div class="combopart">
                                                                                <div class="header">
                                                                                    <div class="content">
                                                                                        <div class="text" style="float: left">
                                                                                            <label>
                                                                                                <asp:Localize runat="server" Text="Rendering" ID="locRendering1"
                                                                                                    meta:resourcekey="locRendering1Resource1"></asp:Localize></label>
                                                                                        </div>
                                                                                        <div style="float: right">
                                                                                        </div>
                                                                                        <div class="clear">
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div id="panelRendering" style="padding-left: 10px">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <span>
                                                                                                    <asp:Localize runat="server" ID="locRendering2" Text="Rendering:"
                                                                                                        meta:resourcekey="locRendering2Resource1"></asp:Localize>&nbsp; </span>&nbsp;
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:DropDownList runat="server" ID="cmbRendering"
                                                                                                    meta:resourcekey="cmbRenderingResource1">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                                <div class="clear">
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="section_small_bottom">
                                                                            &nbsp;
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                                <div style="display: block;">
                                                                    <div class="section_small_top">
                                                                        &nbsp;
                                                                    </div>
                                                                    <div class="section_small_middle" style="height: 306px">
                                                                        <div class="combopart">
                                                                            <div class="header">
                                                                                <div class="content">
                                                                                    <div class="text" style="float: left">
                                                                                        <label>
                                                                                            <asp:Localize runat="server" Text="Black - Generation"
                                                                                                ID="locBlackGeneration" meta:resourcekey="locBlackGenerationResource1"></asp:Localize></label>
                                                                                    </div>
                                                                                    <div style="float: right">
                                                                                    </div>
                                                                                    <div class="clear">
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div id="panelBlackGeneration" style="padding-left: 10px">
                                                                                <%--<asp:Panel ID="panelBlackGen" Style="padding: 1px" runat="server" BorderStyle="None" Width="100%" Enabled="true">--%>
                                                                                <asp:Panel ID="panelBlackGen" Style="padding: 1px" runat="server" BorderStyle="None" Width="100%" Enabled="false">
                                                                                    <table style="width: 1-" width="100%">
                                                                                        <tr>
                                                                                            <td class="style4">
                                                                                                <span>
                                                                                                    <asp:Localize runat="server" Text="GCR:" ID="locGCR"
                                                                                                        meta:resourcekey="locGCRResource1"></asp:Localize></span>
                                                                                            </td>
                                                                                            <td style="width: 160px;">
                                                                                                <telerik:RadSlider runat="server" ID="rdsMode" MinimumValue="-3" MaximumValue="-1" SmallChange="0.5" LargeChange="0.5" OnValueChanged="rdsMode_ValueChanged"
                                                                                                    Width="160px" DbValue="0" Height="22px" Length="160"
                                                                                                    meta:resourcekey="rdsModeResource1" SelectionEnd="10" SelectionStart="-100"
                                                                                                    Enabled="true" AutoPostBack="true">
                                                                                                </telerik:RadSlider>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox runat="server" ID="txtMode" Width="30px"
                                                                                                    meta:resourcekey="txtModeResource1" Enabled="false"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="style4">
                                                                                                <span>
                                                                                                    <asp:Localize runat="server" Text="Black-Start:" ID="locBlackStart"
                                                                                                        meta:resourcekey="locBlackStartResource1"></asp:Localize></span>
                                                                                            </td>
                                                                                            <td style="width: 160px;">
                                                                                                <telerik:RadSlider runat="server" ID="rdsBlackStart" OnValueChanged="rdsBlackStart_ValueChanged"
                                                                                                    Width="160px" DbValue="0" Height="22px" Length="160" MinimumValue="0" MaximumValue="100"
                                                                                                    meta:resourcekey="rdsBlackStartResource1" Enabled="true" AutoPostBack="true">
                                                                                                </telerik:RadSlider>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox runat="server" ID="txtBlackStart" Width="30px"
                                                                                                    meta:resourcekey="txtBlackStartResource1" Enabled="false"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="style4">
                                                                                                <span>
                                                                                                    <asp:Localize runat="server" Text="Black-Width:" ID="locBlackWidth"
                                                                                                        meta:resourcekey="locBlackWidthResource1"></asp:Localize></span>
                                                                                            </td>
                                                                                            <td style="width: 160px;">
                                                                                                <telerik:RadSlider runat="server" ID="rdsBlackWidth" OnValueChanged="rdsBlackWidth_ValueChanged"
                                                                                                    Width="160px" DbValue="0" Height="22px" Length="160" MinimumValue="0" MaximumValue="100"
                                                                                                    meta:resourcekey="rdsBlackWidthResource1" Enabled="true" AutoPostBack="true">
                                                                                                </telerik:RadSlider>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox runat="server" ID="txtBlackWidth" Width="30px"
                                                                                                    meta:resourcekey="txtBlackWidthResource1" Enabled="false"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="style4">
                                                                                                <span>
                                                                                                    <asp:Localize runat="server" Text="Max-Ink:" ID="locInkTotal"
                                                                                                        meta:resourcekey="locInkTotalResource1"></asp:Localize></span>
                                                                                            </td>
                                                                                            <td style="width: 160px;">
                                                                                                <%--<telerik:RadSlider runat="server" ID="rdsInkTotal" OnValueChanged="rdsInkTotal_ValueChanged" 
                                                                                    Width="160px" DbValue="0" Height="22px" Length="160"  MinimumValue="0" MaximumValue="400"
                                                                                    meta:resourcekey="rdsrdsInkTotalResource1" Enabled="true" AutoPostBack="true">
                                                                                </telerik:RadSlider>--%>
                                                                                                <asp:TextBox runat="server" ID="txtInkTotal" Width="70px"
                                                                                                    meta:resourcekey="txtInkTotalResource1" Enabled="false"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%--<asp:TextBox runat="server" ID="txtInkTotal" Width="30px" 
                                                                                    meta:resourcekey="txtInkTotalResource1" Enabled="false"></asp:TextBox>--%>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="style4">
                                                                                                <span>
                                                                                                    <asp:Localize runat="server" Text="Custom:" ID="locCustom"
                                                                                                        meta:resourcekey="locCustomResource1"></asp:Localize></span>
                                                                                            </td>
                                                                                            <td style="width: 160px;">
                                                                                                <asp:TextBox runat="server" ID="txtBlackPointCustom" Width="150px"
                                                                                                    meta:resourcekey="txtBlackPointCustomResource1"></asp:TextBox>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </asp:Panel>
                                                                            </div>
                                                                            <div class="clear">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="section_small_bottom">
                                                                        &nbsp;
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td valign="top" style="width: 320px;">
                                                                <asp:Panel ID="panel2" Style="padding: 1px" runat="server" BorderStyle="None" Width="100%" Enabled="false">
                                                                    <div style="display: block;">
                                                                        <div class="section_small_top">
                                                                            &nbsp;
                                                                        </div>
                                                                        <div class="section_small_middle" style="height: 400px;">
                                                                            <div class="combopart">
                                                                                <div class="header">
                                                                                    <div class="content">
                                                                                        <div class="text" style="float: left">
                                                                                            <label>
                                                                                                <asp:Localize runat="server" Text="Exceptions" ID="locExceptions"
                                                                                                    meta:resourcekey="locExceptionsResource1"></asp:Localize>
                                                                                            </label>
                                                                                        </div>
                                                                                        <div style="float: right">
                                                                                        </div>
                                                                                        <div class="clear">
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div id="panelExceptions" style="padding-left: 10px">
                                                                                    <table width="100%" class="checkboxFont" cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td class="style1">
                                                                                                <asp:CheckBox ID="chkTriplex" runat="server" Text="  Triplex"
                                                                                                    meta:resourcekey="chkTriplexResource1" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:CheckBox runat="server" ID="chkDuplex" Text="  Duplex"
                                                                                                    meta:resourcekey="chkDuplexResource1" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="style1">
                                                                                                <asp:CheckBox runat="server" ID="chkGray" Text="  Gray"
                                                                                                    meta:resourcekey="chkGrayResource1" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:CheckBox runat="server" ID="chk100Black" Text="  100% Black"
                                                                                                    meta:resourcekey="chk100BlackResource1" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="style1">
                                                                                                <asp:CheckBox runat="server" ID="chk100CMY" Text="  100% C,M,Y"
                                                                                                    meta:resourcekey="chk100CMYResource1" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:CheckBox runat="server" ID="chk100RGB" Text="  100% R,G,B"
                                                                                                    meta:resourcekey="chk100RGBResource1" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="style1">
                                                                                                <asp:CheckBox runat="server" ID="chkMaxCMY" Text="  Max. C,M,Y"
                                                                                                    meta:resourcekey="chkMaxCMYResource1" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:CheckBox runat="server" ID="chkMaxRGB" Text="  Max. R,G,B"
                                                                                                    meta:resourcekey="chkMaxRGBResource1" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="style1" colspan="2">
                                                                                                <asp:CheckBox ID="chkPrimaries" runat="server" Text="  Primaries"
                                                                                                    meta:resourcekey="chkPrimariesResource1" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="style1" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                <asp:CheckBox ID="chkPC" runat="server" Text="  C"
                                                                                    meta:resourcekey="chkPCResource1" />
                                                                                                <asp:CheckBox ID="chkPM" runat="server" Text="  M"
                                                                                                    meta:resourcekey="chkPMResource1" />
                                                                                                <asp:CheckBox ID="chkPY" runat="server" Text="  Y"
                                                                                                    meta:resourcekey="chkPYResource1" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="style1" colspan="2">
                                                                                                <asp:CheckBox ID="chkSecondaries" runat="server" Text="  Secondaries"
                                                                                                    meta:resourcekey="chkSecondariesResource1" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="style1" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                <asp:CheckBox ID="chkSMY" runat="server" Text="  MY"
                                                                                    meta:resourcekey="chkSMYResource1" />
                                                                                                <asp:CheckBox ID="chkSCY" runat="server" Text="  CY"
                                                                                                    meta:resourcekey="chkSCYResource1" />
                                                                                                <asp:CheckBox ID="chkSCM" runat="server" Text="  CM"
                                                                                                    meta:resourcekey="chkSCMResource1" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="style1">
                                                                                                <asp:CheckBox runat="server" ID="chkBlackOverPrint" Text="  Black Overprint"
                                                                                                    meta:resourcekey="chkBlackOverPrintResource1" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:CheckBox runat="server" ID="chkBorderClipping" Text="  Border Clipping"
                                                                                                    meta:resourcekey="chkBorderClippingResource1" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="style1">
                                                                                                <asp:CheckBox runat="server" ID="chkWhite" Text="  White"
                                                                                                    meta:resourcekey="chkWhiteResource1" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:CheckBox runat="server" ID="chk400Black" Text="  400% Black"
                                                                                                    meta:resourcekey="chk400BlackResource1" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="2" valign="top">
                                                                                                <table cellpadding="0" cellspacing="0" width="98%">
                                                                                                    <tr>
                                                                                                        <td colspan="3" valign="top"></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:Localize runat="server" Text="Range:" ID="locRange"
                                                                                                                meta:resourcekey="locRangeResource1"></asp:Localize>&nbsp;
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <telerik:RadSlider runat="server" ID="rdsRange" DbValue="0" Height="22px"
                                                                                                                Length="200" meta:resourcekey="rdsRangeResource1" Width="200px" Enabled="false">
                                                                                                            </telerik:RadSlider>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox runat="server" ID="txtRange" Width="30px"
                                                                                                                meta:resourcekey="txtRangeResource1"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                                <div class="clear">
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="section_small_bottom">
                                                                            &nbsp;
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; width: 100%">
                                                    <asp:Panel ID="pnlCustomSetting" runat="server" Width="120%">

                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%" id="tblCustomProfileSetting" runat="server">
                                                            <tr>
                                                                <td style="width: 100%">
                                                                    <div id="tabstrip" style="width: 670px; vertical-align: top;">
                                                                        <ul>
                                                                            <li class="k-state-active">
                                                                                <asp:Localize Text="Summary" ID="locDestinationProfile"
                                                                                    runat="server" meta:resourcekey="locDestinationProfile"></asp:Localize>
                                                                            </li>
                                                                            <li>
                                                                                <asp:Localize Text="Summary" ID="locDeviceLink"
                                                                                    runat="server" meta:resourcekey="locDeviceLink"></asp:Localize>
                                                                            </li>
                                                                            <li>Settings
                                                                            </li>
                                                                        </ul>
                                                                        <div>
                                                                            <div>
                                                                                <table style="vertical-align: top; width: 100%;" cellpadding="0" cellspacing="5" border="0">
                                                                                    <tr>
                                                                                        <td style="width: 50%; vertical-align: top">
                                                                                            <table style="width: 100%; vertical-align: top;" cellpadding="0" cellspacing="0" border="0">
                                                                                                <tr>
                                                                                                    <td style="vertical-align: top;">
                                                                                                        <fieldset style="margin-bottom: 10px;">
                                                                                                            <legend><b>
                                                                                                                <asp:Localize Text="Summary" ID="fldPerceptualRendering" runat="server" meta:resourcekey="fldPerceptualRendering">
                                                                                                                </asp:Localize>
                                                                                                            </b></legend>
                                                                                                            <asp:Panel ID="panel3" Style="padding: 1px" runat="server" BorderStyle="None" Width="100%">
                                                                                                                <table cellpadding="0" cellspacing="3" border="0" width="100%" style="vertical-align: top; padding-top: 10px;">
                                                                                                                    <tr>
                                                                                                                        <td style="vertical-align: top;">
                                                                                                                            <asp:DropDownList ID="cmbPerceptualRendering" runat="server" CssClass="select" DataValueField="Key" onchange="OnChangecmbPerceptualRendering()"
                                                                                                                                meta:resourcekey="cmbRenderingResource1">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="vertical-align: top; padding-top: 10px;">
                                                                                                                            <asp:Localize Text="Summary" ID="lblAdvancedPerceptualRendering" runat="server" meta:resourcekey="lblAdvancedPerceptualRendering"></asp:Localize></td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="vertical-align: top; font-size: 12px;">
                                                                                                                            <asp:RadioButtonList ID="rdBtnAdvancedPerceptualRendering" runat="server" RepeatDirection="Horizontal">
                                                                                                                                <asp:ListItem Text="Chroma" Value="1" Selected="True" meta:resourcekey="lblChromaResources"></asp:ListItem>
                                                                                                                                <asp:ListItem Text="Saturation" Value="2" meta:resourcekey="lblSaturationResources"></asp:ListItem>
                                                                                                                                <asp:ListItem Text="Lightness" Value="3" meta:resourcekey="lblLightnessResources"></asp:ListItem>
                                                                                                                            </asp:RadioButtonList>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="vertical-align: top; padding-top: 15px;">
                                                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="100%" style="vertical-align: top;">
                                                                                                                                <tr>
                                                                                                                                    <td style="width: 85%; vertical-align: top;">
                                                                                                                                        <telerik:RadSlider ID="rdsGrayBalance" runat="server" AutoPostBack="false" OnClientValueChanged="clientValueChangerdsGrayBalance" OnClientLoad="OnClientLoadrdsGrayBalance" DbValue="0" Enabled="true" Height="22px" LargeChange="0.5" Length="260" MaximumValue="20" MinimumValue="-20" SmallChange="1.0" Width="160px">
                                                                                                                                        </telerik:RadSlider>
                                                                                                                                    </td>
                                                                                                                                    <td style="vertical-align: top; width: 15%;">
                                                                                                                                        <asp:TextBox ID="txtGrayBalance" runat="server" Enabled="false" Width="30px"></asp:TextBox>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="vertical-align: top; padding-top: 15px; text-align: center;">
                                                                                                                            <asp:Localize Text="Summary" ID="lblGrayBalance" runat="server" meta:resourcekey="lblGrayBalance"></asp:Localize>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="vertical-align: top; padding-top: 15px;">
                                                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                                                                                <tr>
                                                                                                                                    <td style="width: 160px; vertical-align: top;">
                                                                                                                                        <telerik:RadSlider ID="rdsCoolerWarmer" runat="server" AutoPostBack="false" OnClientValueChanged="clientValueChangerdsCoolerWarmer" OnClientLoad="OnClientLoadrdsCoolerWarmer" DbValue="0" Enabled="true" Height="22px" LargeChange="0.5" Length="260" MaximumValue="20" MinimumValue="-20" SmallChange="1.0" Width="160px">
                                                                                                                                        </telerik:RadSlider>
                                                                                                                                    </td>
                                                                                                                                    <td style="vertical-align: top; width: 15%;">
                                                                                                                                        <asp:TextBox ID="txtCoolerWarmer" runat="server" Enabled="false" Width="30px"></asp:TextBox>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="vertical-align: top; padding-top: 15px;">
                                                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                                                                                <tr>
                                                                                                                                    <td style="text-align: center; width: 15%;">
                                                                                                                                        <asp:Localize Text="Summary" ID="lblCooler" runat="server" meta:resourcekey="lblCooler"></asp:Localize>
                                                                                                                                    </td>
                                                                                                                                    <td style="text-align: center; width: 15%;">
                                                                                                                                        <asp:Localize Text="Summary" ID="lblWarmer" runat="server" meta:resourcekey="lblWarmer"></asp:Localize>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </asp:Panel>
                                                                                                        </fieldset>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="vertical-align: top; padding-top: 15px;">
                                                                                                        <fieldset style="margin-bottom: 10px; height: 190px;">
                                                                                                            <legend><b>
                                                                                                                <asp:Localize Text="Summary" ID="fldMeasurementProcessing" runat="server" meta:resourcekey="fldMeasurementProcessing"></asp:Localize></b></legend>
                                                                                                            <asp:Panel ID="panel4" Style="padding: 1px; font-size: 10px; margin-top: 25px;" runat="server" BorderStyle="None" Width="100%">
                                                                                                                <table style="vertical-align: top; padding-top: 15px; padding-left: 15px;" cellpadding="0" cellspacing="5" border="0">
                                                                                                                    <tr>
                                                                                                                        <td style="vertical-align: top;">
                                                                                                                            <asp:CheckBox runat="server" ID="chkMeasurementCorrection" Text="  Measurement Correction " CssClass="checkboxFont Label" meta:resourcekey="chkMeasurementCorrectionResource1" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="vertical-align: top;">
                                                                                                                            <asp:CheckBox runat="server" ID="chkBrightenerCmp" Text="  Brightener Compensation " CssClass="checkboxFont Label" meta:resourcekey="chkBrightenerCompensationResource1" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </asp:Panel>
                                                                                                        </fieldset>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td style="width: 50%; vertical-align: top">
                                                                                            <table style="width: 100%; vertical-align: top;" cellpadding="0" cellspacing="0" border="0">
                                                                                                <tr>
                                                                                                    <td style="vertical-align: top; padding-bottom: 25px;">
                                                                                                        <fieldset>
                                                                                                            <legend><b>
                                                                                                                <asp:Localize Text="Summary" ID="fldBlackGeneration" runat="server" meta:resourcekey="fldBlackGeneration"></asp:Localize></b></legend>
                                                                                                            <asp:Panel ID="panel5" Style="padding: 1px; padding-top: 15px; height: 222px;" runat="server" BorderStyle="None" Width="100%">
                                                                                                                <table style="width: 100%" cellpadding="0" cellspacing="2" border="0">
                                                                                                                    <tr>
                                                                                                                        <td style="vertical-align: top; margin-left: 5px; width: 100%" colspan="3">
                                                                                                                            <asp:DropDownList ID="cmbBlackGeneration" runat="server" CssClass="select" DataValueField="Key"
                                                                                                                                DataTextField="Value" AutoPostBack="false" onchange="OnChangecmbBalckGeneration()">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="vertical-align: top; padding-top: 15px; width: 25%;">
                                                                                                                            <asp:Localize Text="Summary" ID="lblGCRAmount" runat="server" meta:resourcekey="lblGCRAmount"></asp:Localize></td>
                                                                                                                        <td style="vertical-align: top; padding-top: 15px; width: 60%">
                                                                                                                            <telerik:RadSlider ID="rdsGCRAmount" runat="server" OnClientValueChanged="clientValueChangerdsGCRAmount" OnClientLoad="OnClientLoadrdsGCRAmount" AutoPostBack="false" DbValue="25" Enabled="true" Height="22px" Length="180" MinimumValue="0" MaximumValue="100" SmallChange="5" LargeChange="10" Width="160px">
                                                                                                                            </telerik:RadSlider>
                                                                                                                        </td>
                                                                                                                        <td style="vertical-align: top; padding-top: 15px; width: 15%;">
                                                                                                                            <asp:TextBox ID="txtGCRAmount" runat="server" Enabled="false" Width="30px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="vertical-align: top; padding-top: 15px; width: 25%;">
                                                                                                                            <asp:Localize Text="Summary" ID="lblBlackStart" runat="server" meta:resourcekey="lblBlackStart"></asp:Localize></td>
                                                                                                                        <td style="vertical-align: top; padding-top: 15px; width: 60%;">
                                                                                                                            <telerik:RadSlider ID="rdsBlackStartCustom" runat="server" AutoPostBack="false" OnClientValueChanged="clientValueChangerdsBlackStartCustom" OnClientLoad="OnClientLoadrdsBlackStartCustom" DbValue="0" Enabled="true" Height="22px" Length="180" MinimumValue="0" MaximumValue="100" SmallChange="5" LargeChange="10" Width="160px">
                                                                                                                            </telerik:RadSlider>
                                                                                                                        </td>
                                                                                                                        <td style="vertical-align: top; padding-top: 15px; width: 15%;">
                                                                                                                            <asp:TextBox ID="txtBlackStartCustom" runat="server" Enabled="false" Width="30px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="vertical-align: top; padding-top: 15px; width: 25%;">
                                                                                                                            <asp:Localize Text="Summary" ID="lblBlackWidth" runat="server" meta:resourcekey="lblBlackWidth"></asp:Localize></td>
                                                                                                                        <td style="vertical-align: top; padding-top: 15px; width: 60%;">
                                                                                                                            <telerik:RadSlider ID="rdsBlackWidthCustom" runat="server" AutoPostBack="false" OnClientValueChanged="clientValueChangerdsBlackWidthCustom" OnClientLoad="OnClientLoadrdsBlackWidthCustom" DbValue="100" Enabled="true" Height="22px" Length="180" MinimumValue="0" MaximumValue="100" SmallChange="5" LargeChange="10" Width="160px">
                                                                                                                            </telerik:RadSlider>
                                                                                                                        </td>
                                                                                                                        <td style="vertical-align: top; padding-top: 15px; width: 15%;">
                                                                                                                            <asp:TextBox ID="txtBlackWidthCustom" runat="server" Enabled="false" Width="30px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="vertical-align: top; margin-left: 5px; padding-top: 15px; padding-left: 25px; width: 100%" colspan="3">
                                                                                                                            <table style="vertical-align: top;" cellpadding="0" cellspacing="5" border="0">
                                                                                                                                <tr>
                                                                                                                                    <td style="vertical-align: top;">
                                                                                                                                        <asp:CheckBox runat="server" ID="chk100BlackCustom" Text="  100% Black  " CssClass="checkboxFont Label" meta:resourcekey="chk100BlackCustom" />
                                                                                                                                    </td>
                                                                                                                                    <td style="vertical-align: top;">
                                                                                                                                        <asp:CheckBox runat="server" ID="chkPureGray" Text="  Pure Gray " CssClass="checkboxFont Label" meta:resourcekey="chkPureGray" />
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </asp:Panel>
                                                                                                        </fieldset>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="vertical-align: top;">
                                                                                                        <fieldset>
                                                                                                            <legend><b>
                                                                                                                <asp:Localize Text="Summary" ID="fldBlackCalculation" runat="server" meta:resourcekey="fldBlackCalculation"></asp:Localize></b></legend>
                                                                                                            <asp:Panel ID="panel6" Style="padding: 1px" runat="server" BorderStyle="None" Width="100%">
                                                                                                                <table style="width: 100%">
                                                                                                                    <tr>
                                                                                                                        <td style="vertical-align: top; padding-top: 10px;" colspan="3">
                                                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                                                                                <tr>
                                                                                                                                    <td style="vertical-align: top;">
                                                                                                                                        <asp:DropDownList ID="cmbBlackCalculation" runat="server" CssClass="select" DataValueField="Key"
                                                                                                                                            DataTextField="Value" AutoPostBack="false" onchange="OnChangecmbBlackCalculation()">
                                                                                                                                        </asp:DropDownList></td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="vertical-align: top; padding-top: 10px; width: 45%">
                                                                                                                            <asp:Localize Text="Summary" ID="lblTAC" runat="server" meta:resourcekey="lblTAC"></asp:Localize></td>
                                                                                                                        <td style="width: 40%; padding-top: 10px;">
                                                                                                                            <telerik:RadSlider ID="rdsTAC" runat="server" AutoPostBack="false" OnClientValueChanged="clientValueChangerdsTAC" OnClientLoad="OnClientLoadrdsTAC" DbValue="320" Enabled="true" Height="22px" Length="200" MinimumValue="0" MaximumValue="400" SelectionStart="20" SmallChange="10" LargeChange="20" Width="160px">
                                                                                                                            </telerik:RadSlider>
                                                                                                                        </td>
                                                                                                                        <td style="vertical-align: top; width: 15%; padding-top: 10px;">
                                                                                                                            <asp:TextBox ID="txtTAC" runat="server" Enabled="false" Width="30px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="vertical-align: top; width: 45%; padding-top: 10px;">
                                                                                                                            <asp:Localize Text="Summary" ID="lblBlackTAC" runat="server" meta:resourcekey="lblBlackTAC"></asp:Localize></td>
                                                                                                                        <td style="width: 40%; padding-top: 10px;">
                                                                                                                            <telerik:RadSlider ID="rdsBlackTAC" runat="server" AutoPostBack="false" OnClientValueChanged="clientValueChangerdsBlackTAC" OnClientLoad="OnClientLoadrdsBlackTAC" DbValue="320" Enabled="true" Height="22px" Length="200" MinimumValue="0" MaximumValue="400" SelectionStart="20" SmallChange="10" LargeChange="20" Width="160px">
                                                                                                                            </telerik:RadSlider>
                                                                                                                        </td>
                                                                                                                        <td style="vertical-align: top; padding-top: 10px; width: 14%">
                                                                                                                            <asp:TextBox ID="txtBlackTAC" runat="server" Enabled="false" Width="30px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="vertical-align: top; padding-top: 10px; width: 45%;">
                                                                                                                            <asp:Localize Text="Summary" ID="lblMaxBlack" runat="server" meta:resourcekey="lblMaxBlack"></asp:Localize></td>
                                                                                                                        <td style="width: 40%; padding-top: 15px;">
                                                                                                                            <telerik:RadSlider ID="rdsMaxBlack" runat="server" AutoPostBack="false" OnClientValueChanged="clientValueChangerdsMaxBlack" OnClientLoad="OnClientLoadrdsMaxBlack" DbValue="100" Enabled="true" Height="22px" Length="200" MinimumValue="0" MaximumValue="100" SelectionStart="10" SmallChange="5" LargeChange="20" Width="160px">
                                                                                                                            </telerik:RadSlider>
                                                                                                                        </td>
                                                                                                                        <td style="vertical-align: top; padding-top: 10px; width: 14%">
                                                                                                                            <asp:TextBox ID="txtMaxBlack" runat="server" Enabled="false" Width="30px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td colspan="3" style="width: 100%; padding-top: 5px; vertical-align: top">
                                                                                                                            <table style="width: 100%; vertical-align: top">
                                                                                                                                <tr>
                                                                                                                                    <td style="width: 10%; vertical-align: top;">
                                                                                                                                        <div style="background-color: CYAN; border: solid 1px; border-color: black; width: 15px; height: 15px;"></div>
                                                                                                                                    </td>
                                                                                                                                    <td style="width: 25%; vertical-align: top;">
                                                                                                                                        <asp:TextBox ID="txtCustomCYAN" runat="server" Width="30px"></asp:TextBox></td>
                                                                                                                                    <td style="width: 10%; vertical-align: top;">
                                                                                                                                        <div style="background-color: MAGENTA; border: solid 1px; border-color: black; width: 15px; height: 15px;"></div>
                                                                                                                                    </td>
                                                                                                                                    <td style="width: 25%; vertical-align: top;">
                                                                                                                                        <asp:TextBox ID="txtCustomMagenta" runat="server" Width="30px"></asp:TextBox></td>
                                                                                                                                    <td style="width: 10%; vertical-align: top;">
                                                                                                                                        <div style="background-color: YELLOW; border: solid 1px; border-color: black; width: 15px; height: 15px;"></div>
                                                                                                                                    </td>
                                                                                                                                    <td style="width: 25%; vertical-align: top;">
                                                                                                                                        <asp:TextBox ID="txtCustomYellow" runat="server" Width="30px"></asp:TextBox></td>
                                                                                                                                    <td style="width: 10%; vertical-align: top;">
                                                                                                                                        <div style="background-color: black; border: solid 1px; border-color: black; width: 15px; height: 15px;"></div>
                                                                                                                                    </td>
                                                                                                                                    <td style="width: 25%; vertical-align: top;">
                                                                                                                                        <asp:TextBox ID="txtCustomBlack" runat="server" Width="30px"></asp:TextBox>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </asp:Panel>
                                                                                                        </fieldset>
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
                                                                                Device Link Tab
                                                                            </div>
                                                                        </div>
                                                                        <div>
                                                                            <div>
                                                                                <asp:Panel ID="panel7" Style="padding: 1px" runat="server" BorderStyle="None" Width="100%">
                                                                                    <table style="width: 100%; vertical-align: top">
                                                                                        <tr>
                                                                                            <td style="vertical-align: top; width: 15%;">BlackGeneration
                                                                                            </td>
                                                                                            <td style="vertical-align: top; width: 5%;"><b>:</b>
                                                                                            </td>
                                                                                            <td style="vertical-align: top; width: 75%;">
                                                                                                <div style="float: left">
                                                                                                    <asp:Label runat="server" ID="lblSettingBlackGeneration" />
                                                                                                    <asp:HiddenField ID="hdnSettingBlackGeneration" runat="server" />
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="trSettingBlackStart" runat="server">
                                                                                            <td style="vertical-align: top;">BlackStart
                                                                                            </td>
                                                                                            <td style="vertical-align: top;"><b>:</b>
                                                                                            </td>
                                                                                            <td style="vertical-align: top;">
                                                                                                <div style="float: left">
                                                                                                    <asp:Label runat="server" ID="lblSettingBlackStart" />
                                                                                                    <asp:HiddenField ID="hdnSettingBlackStart" runat="server" />
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="trSettingBlackWidth" runat="server">
                                                                                            <td style="vertical-align: top;">BlackWidth
                                                                                            </td>
                                                                                            <td style="vertical-align: top;"><b>:</b>
                                                                                            </td>
                                                                                            <td style="vertical-align: top;">
                                                                                                <div style="float: left">
                                                                                                    <asp:Label runat="server" ID="lblSettingBlackWidth" />
                                                                                                    <asp:HiddenField ID="hdnSettingBlackWidth" runat="server" />
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="vertical-align: top;">Exceptions
                                                                                            </td>
                                                                                            <td style="vertical-align: top;"><b>:</b>
                                                                                            </td>
                                                                                            <td style="vertical-align: top;">
                                                                                                <div style="float: left">
                                                                                                    <asp:Label runat="server" ID="lblSettingExceptions" Text="0" />
                                                                                                    <asp:HiddenField ID="hdnSettingExceptions" runat="server" />
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="vertical-align: top;">InkMax
                                                                                            </td>
                                                                                            <td style="vertical-align: top;"><b>:</b>
                                                                                            </td>
                                                                                            <td style="vertical-align: top;">
                                                                                                <div style="float: left">
                                                                                                    <asp:Label runat="server" ID="lblSettingInmax" />
                                                                                                    <asp:HiddenField ID="hdnSettingInmax" runat="server" />
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="vertical-align: top;">InkTotal
                                                                                            </td>
                                                                                            <td style="vertical-align: top;"><b>:</b>
                                                                                            </td>
                                                                                            <td style="vertical-align: top;">
                                                                                                <div style="float: left">
                                                                                                    <asp:Label runat="server" ID="lblSettingInTotal" />
                                                                                                    <asp:HiddenField ID="hdnSettingInTotal" runat="server" />
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="vertical-align: top;">BlackTAC
                                                                                            </td>
                                                                                            <td style="vertical-align: top;"><b>:</b>
                                                                                            </td>
                                                                                            <td style="vertical-align: top;">
                                                                                                <div style="float: left">
                                                                                                    <asp:Label runat="server" ID="lblSettingBlackTAC" />
                                                                                                    <asp:HiddenField ID="hdnSettingBlackTAC" runat="server" />
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="vertical-align: top;">BrightenerCorrection
                                                                                            </td>
                                                                                            <td style="vertical-align: top;"><b>:</b>
                                                                                            </td>
                                                                                            <td style="vertical-align: top;">
                                                                                                <div style="float: left">
                                                                                                    <asp:Label runat="server" ID="lblSettingBrightenerCorrection" />
                                                                                                    <asp:HiddenField ID="hdnSettingBrightenerCorrection" runat="server" />
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="vertical-align: top;">MeasurementCorrection
                                                                                            </td>
                                                                                            <td style="vertical-align: top;"><b>:</b>
                                                                                            </td>
                                                                                            <td style="vertical-align: top;">
                                                                                                <div style="float: left">
                                                                                                    <asp:Label runat="server" ID="lblSettingMeasurementCorrection" />
                                                                                                    <asp:HiddenField ID="hdnSettingMeasurementCorrection" runat="server" />
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="vertical-align: top;">SaturationCompression
                                                                                            </td>
                                                                                            <td style="vertical-align: top;"><b>:</b>
                                                                                            </td>
                                                                                            <td style="vertical-align: top;">
                                                                                                <div style="float: left">
                                                                                                    <asp:Label runat="server" ID="lblSettingSaturationCompression" />
                                                                                                    <asp:HiddenField ID="hdnSettingSaturationCompression" runat="server" />
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="vertical-align: top;">PerceptualCompression
                                                                                            </td>
                                                                                            <td style="vertical-align: top;"><b>:</b>
                                                                                            </td>
                                                                                            <td style="vertical-align: top;">
                                                                                                <div style="float: left">
                                                                                                    <asp:Label runat="server" ID="lblSettingPerceptualCompression" />
                                                                                                    <asp:HiddenField ID="hdnSettingPerceptualCompression" runat="server" />
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="trPerceptualChromaCompression" runat="server">
                                                                                            <td style="vertical-align: top;">PerceptualChromaCompression
                                                                                            </td>
                                                                                            <td style="vertical-align: top;"><b>:</b>
                                                                                            </td>
                                                                                            <td style="vertical-align: top;">
                                                                                                <div style="float: left">
                                                                                                    <asp:Label runat="server" ID="lblSettingPerceptualChromaCompression" />
                                                                                                    <asp:HiddenField ID="hdnSettingPerceptualChromaCompression" runat="server" />
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="trPerceptualLightnessCorrection" runat="server">
                                                                                            <td style="vertical-align: top;">PerceptualLightnessCorrection
                                                                                            </td>
                                                                                            <td style="vertical-align: top;"><b>:</b>
                                                                                            </td>
                                                                                            <td style="vertical-align: top;">
                                                                                                <div style="float: left">
                                                                                                    <asp:Label runat="server" ID="lblSettingPerceptualLightnessCorrection" />
                                                                                                    <asp:HiddenField ID="hdnSettingPerceptualLightnessCorrection" runat="server" />
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="trSaturationLightnessCorrection" runat="server">
                                                                                            <td style="vertical-align: top;">SaturationLightnessCorrection
                                                                                            </td>
                                                                                            <td style="vertical-align: top;"><b>:</b>
                                                                                            </td>
                                                                                            <td style="vertical-align: top;">
                                                                                                <div style="float: left">
                                                                                                    <asp:Label runat="server" ID="lblSettingSaturationLightnessCorrection" />
                                                                                                    <asp:HiddenField ID="hdnSettingSaturationLightnessCorrection" runat="server" />
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="trSaturationChromaCompression" runat="server">
                                                                                            <td style="vertical-align: top;">SaturationChromaCompression
                                                                                            </td>
                                                                                            <td style="vertical-align: top;"><b>:</b>
                                                                                            </td>
                                                                                            <td style="vertical-align: top;">
                                                                                                <div style="float: left">
                                                                                                    <asp:Label runat="server" ID="lblSettingSaturationChromaCompression" />
                                                                                                    <asp:HiddenField ID="hdnSettingSaturationChromaCompression" runat="server" />
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="trSaturationGrayCorrection" runat="server">
                                                                                            <td style="vertical-align: top;">SaturationGrayCorrection
                                                                                            </td>
                                                                                            <td style="vertical-align: top;"><b>:</b>
                                                                                            </td>
                                                                                            <td style="vertical-align: top;">
                                                                                                <div style="float: left">
                                                                                                    <asp:Label ID="lblSettingSaturationGrayCorrection" runat="server" />
                                                                                                    <asp:HiddenField ID="hdnSettingSaturationGrayCorrection" runat="server" />
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="vertical-align: top;">PerceptualGrayCorrection
                                                                                            </td>
                                                                                            <td style="vertical-align: top;"><b>:</b>
                                                                                            </td>
                                                                                            <td style="vertical-align: top;">
                                                                                                <div style="float: left">
                                                                                                    <asp:Label ID="lblSettingPerceptualGrayCorrection" runat="server" />
                                                                                                    <asp:HiddenField ID="hdnSettingPerceptualGrayCorrection" runat="server" />
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="display: none;">
                                                                                            <td style="vertical-align: top;" colspan="3">
                                                                                                <asp:UpdatePanel ID="pnlbtnSaveCustom" runat="server">
                                                                                                    <ContentTemplate>
                                                                                                        <div style="float: left">
                                                                                                            <asp:LinkButton ID="lnkCustomProfileSave" runat="server" CssClass="gray_bt"
                                                                                                                OnClick="lnkCustomProfileSave_Click">
                                                                                                                <span>
                                                                                                                    <asp:Localize runat="server" Text="Save" ID="Localize15"
                                                                                                                        meta:resourcekey="locSaveResource1"></asp:Localize>
                                                                                                                </span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </asp:Panel>

                                                                            </div>
                                                                        </div>
                                                                </td>
                                                            </tr>
                                                        </table>

                                                    </asp:Panel>


                                                </td>
                                            </tr>
                                        </table>


                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="upAdvancedOptions"
                                DisplayAfter="50">
                                <ProgressTemplate>
                                    <AjaxToolkit:AlwaysVisibleControlExtender ID="AlwaysVisibleControl" HorizontalSide="Center"
                                        VerticalSide="Middle" TargetControlID="updateProgress" HorizontalOffset="325"
                                        VerticalOffset="210" runat="server" Enabled="True">
                                    </AjaxToolkit:AlwaysVisibleControlExtender>
                                    <div class="UpdateProgressBackgroundLessOptimize" style="position: fixed; left: 0px; top: 0px; z-index: 1000; width: 100%; height: 100%;"
                                        unselectable="on">
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </div>
                    <div class="tab_bottom">
                        &nbsp;
                    </div>
                </div>
            </td>
        </tr>
    </table>

    <table cellpadding="0" cellspacing="10" border="0" width="97%" id="tbMetricPreset" runat="server">
        <tr id="tr1" runat="server">
            <td>
                <div style="display: block;" class="contantbox tabcontant">
                    <div class="section_top">
                        &nbsp;
                    </div>
                    <div class="section_middle">
                        <div class="combopart">
                            <div class="header">
                                <div class="content">
                                    <div class="text" style="float: left;">
                                        <span>
                                            <asp:Localize runat="server" Text="Metric Presets"
                                                ID="locMetricPresets" meta:resourcekey="locMetricPresetsResource1"></asp:Localize></span>
                                    </div>
                                    <div style="float: right">
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnlAddProjectDescriptionMetricPreset" Style="padding: 5px" runat="server"
                                BorderStyle="None">
                                <table>
                                    <tr>
                                        <td style="width: 100px">
                                            <span>
                                                <asp:Localize runat="server" Text="Selected Presets:"
                                                    ID="locMSelectedPresets" meta:resourcekey="locMSelectedPresetsResource1"></asp:Localize></span>
                                        </td>
                                        <td style="width: 450px">
                                            <asp:DropDownList ID="ddlMetricPresets" runat="server" CssClass="select" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlMetricPresets_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" colspan="2">
                                            <span>
                                                <asp:Localize runat="server" Text="Preset Description:"
                                                    ID="locMPresetDescription" meta:resourcekey="locMPresetDescriptionResource1"></asp:Localize></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" colspan="2">
                                            <asp:TextBox ID="txtMPresetDescription" runat="server" Width="120%" Height="100px"
                                                TextMode="MultiLine" Enabled="False" ReadOnly="True"
                                                Text="This preset is best used for analog devices ……"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Panel ID="PanelOptionalRetarget" runat="server" GroupingText="<b>Optional Retarget</b>" Width="120%">
                                                <asp:UpdatePanel runat="server" ID="upd">
                                                    <ContentTemplate>
                                                        <div runat="server" id="divDevice" visible="false">
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 200px;">
                                                                        <span style="padding-left: 5px;">
                                                                            <asp:Localize runat="server" Text="Select Device: "
                                                                                ID="locSelectDevice" meta:resourcekey="locSelectDeviceResource1"></asp:Localize></span>
                                                                    </td>
                                                                    <td>
                                                                        <div style="float: left">
                                                                            <asp:DropDownList runat="server" ID="cmbDevice" AutoPostBack="true" CssClass="select" OnSelectedIndexChanged="cmbDevice_SelectedIndexChanged" Width="250"></asp:DropDownList>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div runat="server" id="divRetarget" visible="false">
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 200px;">
                                                                        <span style="padding-left: 5px;">
                                                                            <asp:Localize runat="server" Text="Select Certified Print Condition:"
                                                                                ID="Localize1" meta:resourcekey="locReTargetResource1"></asp:Localize></span>
                                                                    </td>
                                                                    <td>
                                                                        <div style="float: left">
                                                                            <asp:DropDownList ID="ddlReTargetList" runat="server" CssClass="select" AutoPostBack="true" OnSelectedIndexChanged="ddlReTargetList_SelectedIndexChanged" Width="250">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div runat="server" id="divRetargetDetails" visible="false">
                                                            <table style="padding-left: 5px;">
                                                                <tr>
                                                                    <td style="width: 200px;">
                                                                        <span>
                                                                            <asp:Localize runat="server" Text="Print Standard Type: "
                                                                                ID="Localize12" meta:resourcekey="locParentProjectNameResource1"></asp:Localize></span>
                                                                    </td>
                                                                    <td>
                                                                        <div style="float: left">
                                                                            <asp:Label runat="server" ID="lblPrintStandardTypeName" />
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 200px;">
                                                                        <span>
                                                                            <asp:Localize runat="server" Text="Metric Target Preset: "
                                                                                ID="Localize13" meta:resourcekey="locReTargetLibraryNameResource1"></asp:Localize></span>
                                                                    </td>
                                                                    <td>
                                                                        <div style="float: left">
                                                                            <asp:Label runat="server" ID="lblMetricTargetPresetName" />
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <div class="clear">
                            </div>
                        </div>
                    </div>
                    <div class="section_bottom">
                        &nbsp;
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div style="display: block;" class="contantbox tabcontant">
                    <div class="section_top">
                        &nbsp;
                    </div>
                    <div class="section_middle">
                        <div class="combopart">
                            <div id="dvaccordionMetric" class="header collapsibleContainerTitle">
                                <div class="content">
                                    <div class="text">
                                        <div style="float: left">
                                        </div>
                                        <div style="float: left;">
                                            <div id="dvimgHeader" class="expanded">
                                                &nbsp;
                                            </div>
                                            <label class="collapsibleContainerTitle">
                                                <asp:Localize runat="server" Text="Advanced Options"
                                                    ID="locAdvancedOptionsMetric" meta:resourcekey="locAdvancedOptionsMetricResource1"></asp:Localize></label>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="panelAdvancedOptionsMetricPresets" Style="padding: 1px" runat="server" BorderStyle="None"
                                Width="100%" Enabled="true">
                                <table cellpadding="0" cellspacing="10" border="0">
                                    <tr>
                                        <td valign="top" style="width: 250px; padding-right: 0px; border: 1px Solid Gray;">
                                            <div id="dvTolerances" style="padding-left: 5px;">
                                                <label>
                                                    <b>
                                                        <asp:Localize runat="server" Text="Tolerances"
                                                            ID="locTolerances" meta:resourcekey="locTolerancesResource1"></asp:Localize></b></label>
                                                <br />
                                                <asp:ListView ID="lvTolerances" runat="server" ItemPlaceholderID="phItemTolerances">
                                                    <LayoutTemplate>
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td></td>
                                                                <td></td>
                                                            </tr>
                                                            <asp:PlaceHolder ID="phItemTolerances" runat="server"></asp:PlaceHolder>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr style="line-height: 25px;">
                                                            <td style="width: 160px;">
                                                                <asp:Label runat="server" Text='<%# Eval("MetricSumName") %>' ID="lblToleranceName"></asp:Label>
                                                            </td>
                                                            <td class="style4">
                                                                <asp:Label runat="server" Text='<%# Eval("TargetValue") %>' ID="lblToleranceValue"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                                <br />
                                                <asp:CheckBox ID="chkWeightedTolerances" runat="server" Visible="false" />&nbsp;<asp:Label ID="lblWeightedTolerances" runat="server" Text="Use Weighted Tolerances" meta:resourcekey="lblWeightedTolerancesResource1" Visible="false"></asp:Label>
                                                <div id="Div_LinearizationType" runat="server" visible="false">
                                                    <asp:CheckBox ID="chkLinearizationType" runat="server" Checked="true" />&nbsp;<asp:Label ID="lblLinearizationType" runat="server" Text="Use LinearizationType TR015" meta:resourcekey="lblLinearizationTypeResource1"></asp:Label>
                                                </div>
                                                Choose Measurement Correction: 
                                                                    <asp:DropDownList ID="cmbMeaCorrection" runat="server">
                                                                        <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                                                        <asp:ListItem Value="1" Text="Correction" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="Smoothing"></asp:ListItem>
                                                                        <asp:ListItem Value="3" Text="Correction + Smoothing"></asp:ListItem>
                                                                    </asp:DropDownList>

                                            </div>
                                        </td>
                                        <td valign="top" style="width: 195px; border: 1px Solid Gray;">


                                            <div id="dvTVITargets" style="padding-left: 10px;">
                                                <label>
                                                    <b>
                                                        <asp:Localize runat="server" Text="TVI Targets" ID="locTVITargets" meta:resourcekey="locTVITargetsResource1"></asp:Localize></b>
                                                </label>
                                                <br />
                                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 23px;">
                                                    <tr style="line-height: 15px;">
                                                        <td></td>
                                                        <td colspan="2" align="center">
                                                            <b>
                                                                <asp:Localize runat="server" Text="Targets" ID="locTargets" meta:resourcekey="locTargetsResource1"></asp:Localize></b>
                                                        </td>
                                                    </tr>
                                                    <tr style="line-height: 15px;">
                                                        <td align="left">
                                                            <span><b>
                                                                <asp:Localize ID="locTVIPatch" runat="server" Text="Patch %" meta:resourcekey="locTVIPatchPerResource1"></asp:Localize></b></span>
                                                        </td>
                                                        <td align="center">
                                                            <span><b>
                                                                <asp:Localize ID="locTVICyan" runat="server" Text="CMY" meta:resourcekey="locTVICMYResource1"></asp:Localize></b></span>
                                                        </td>
                                                        <td align="center">
                                                            <span><b>
                                                                <asp:Localize ID="locTVIBlack" runat="server" Text="K" meta:resourcekey="locTVIBlackResource1"></asp:Localize></b></span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTVI10" runat="server" Text="10"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTVI10CMY" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTVI10K" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTVI20" runat="server" Text="20"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTVI20CMY" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTVI20K" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTVI25" runat="server" Text="25"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTVI25CMY" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTVI25K" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTVI30" runat="server" Text="30"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTVI30CMY" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTVI30K" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTVI40" runat="server" Text="40"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTVI40CMY" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTVI40K" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTVI50" runat="server" Text="50"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTVI50CMY" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTVI50K" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTVI60" runat="server" Text="60"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTVI60CMY" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTVI60K" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTVI70" runat="server" Text="70"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTVI70CMY" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTVI70K" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTVI75" runat="server" Text="75"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTVI75CMY" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTVI75K" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTVI80" runat="server" Text="80"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTVI80CMY" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTVI80K" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTVI90" runat="server" Text="90"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTVI90CMY" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTVI90K" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateTVIRange(this)"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </div>

                                        </td>

                                        <td valign="top" style="width: 195px; border: 1px Solid Gray;">


                                            <div id="Div1" style="padding-left: 10px;">
                                                <label>
                                                    <b>
                                                        <asp:Localize runat="server" Text="Density Targets" ID="Localize2" meta:resourcekey="locDensityTargetsResource1"></asp:Localize></b>
                                                </label>
                                                <br />
                                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 23px;">
                                                    <tr>
                                                        <td colspan="4" align="center">
                                                            <b>
                                                                <asp:Localize runat="server" Text="Targets" ID="Localize7" meta:resourcekey="locTargetsResource1"></asp:Localize></b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <span><b>
                                                                <asp:Localize ID="Localize3" runat="server" Text="C" meta:resourcekey="locDensityCResource1"></asp:Localize></b></span>
                                                        </td>
                                                        <td align="center">
                                                            <span><b>
                                                                <asp:Localize ID="Localize4" runat="server" Text="M" meta:resourcekey="locDensityMResource1"></asp:Localize></b></span>
                                                        </td>
                                                        <td align="center">
                                                            <span><b>
                                                                <asp:Localize ID="Localize5" runat="server" Text="Y" meta:resourcekey="locDensityYResource1"></asp:Localize></b></span>
                                                        </td>
                                                        <td align="center">
                                                            <span><b>
                                                                <asp:Localize ID="Localize6" runat="server" Text="K" meta:resourcekey="locDensityKResource1"></asp:Localize></b></span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtDensityC" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateDensityRange(this)"></asp:TextBox>
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtDensityM" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateDensityRange(this)"></asp:TextBox>
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtDensityY" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateDensityRange(this)"></asp:TextBox>
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtDensityK" runat="server" CssClass="textwidth35" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateDensityRange(this)"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <br />
                                            <div id="dvHueAngleTargets" style="padding-left: 10px;" runat="server" visible="false">
                                                <label>
                                                    <b>
                                                        <asp:Localize runat="server" Text="Hue Angle Targets" ID="Localize8" meta:resourcekey="locHueAngleTargetsResource1"></asp:Localize></b>
                                                </label>
                                                <br />
                                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 23px;">
                                                    <tr style="line-height: 15px;">
                                                        <td></td>
                                                        <td colspan="2" align="left">
                                                            <b>
                                                                <asp:Localize runat="server" Text="Targets" ID="Localize9" meta:resourcekey="locTargetsResource1"></asp:Localize></b>
                                                        </td>
                                                    </tr>
                                                    <tr style="line-height: 15px;">
                                                        <td align="left">
                                                            <span><b>
                                                                <asp:Localize ID="Localize10" runat="server" Text="Patch" meta:resourcekey="locHueAnglePatchPerResource1"></asp:Localize></b></span>
                                                        </td>
                                                        <td align="left">
                                                            <span><b>
                                                                <asp:Localize ID="Localize11" runat="server" Text="Hue Angle" meta:resourcekey="locHueAngleResource1"></asp:Localize></b></span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="C"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtHue_C" runat="server" CssClass="textwidth50" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateHueAngleRange(this)"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="M"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtHue_M" runat="server" CssClass="textwidth50" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateHueAngleRange(this)"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="Y"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtHue_Y" runat="server" CssClass="textwidth50" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateHueAngleRange(this)"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="R"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtHue_R" runat="server" CssClass="textwidth50" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateHueAngleRange(this)"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="G"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtHue_G" runat="server" CssClass="textwidth50" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateHueAngleRange(this)"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" Text="B"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtHue_B" runat="server" CssClass="textwidth50" MaxLength="5" onkeypress="return validateTVI(event)" onchange="return validateHueAngleRange(this)"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </div>

                                        </td>

                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="tab_bottom">
                        &nbsp;
                    </div>
                </div>
            </td>
        </tr>

    </table>
</div>

