<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MeasurementCommonNew.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.MeasurementCommonNew" %>
<%@ Import Namespace="FujifilmUSA.Sync.Business" %>
<script src="jQuery/jquery-1.7.1.min.js?version=<%=JsRandomNumber %>" type="text/javascript"></script>
<script src="jQuery/jquery.tmpl.js?version=<%=JsRandomNumber %>" type="text/javascript"></script>
<script src="JQuery/jquery.simplemodal.js?version=<%=JsRandomNumber %>" type="text/javascript"></script>
<div>
    <div class="info_form">
        <div class="greencornersbox" style="height: 30px !important;">
            <table style="width: 100%; vertical-align: top;">
                <tr>
                    <td style="width: 80%; vertical-align:top; text-align: left;">
                        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="mtoolmessage"></asp:Label>
                    </td>
                    <td style="width: 20%; vertical-align:top; text-align:center; float:none;">
                        <asp:UpdatePanel ID="pluginUpdatePnl" runat="server" RenderMode="Block" ChildrenAsTriggers="false" UpdateMode="Conditional">
                            <ContentTemplate>                               
                            <asp:LinkButton ID="btnAddPluginStatus" runat="server" CssClass="green_bt" OnClick="btnAddPluginStatus_Click">
                                <span>
                                    <asp:Localize runat="server" Text="Activate" ID="btnlocAddPluginStatus"
                                        meta:resourcekey="btnlocAddPluginStatus"></asp:Localize>
                                </span>
                            </asp:LinkButton>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnAddPluginStatus" />
                            </Triggers>                           
                        </asp:UpdatePanel>
                       
                    </td>
                </tr>
            </table>
        </div>
        <div class="clear">
        </div>
    </div>
    <br />
    <br />
    <div class="color_pallate">
        <div class="color_pallate_head">
            <asp:Localize runat="server" Text="Recent Upload" ID="locRecentUpload" meta:resourcekey="locRecentUploadResource1"></asp:Localize>
            <div id="Div_ImportMeasurements" style="float: right; padding-right: 10px;">
                <input type="button" id="btnImportMeasurements" class="link-button" style="width: 150px;" value='<%= GetResourceString("btnImportMeasurements") %>' onclick="javascript: return ShowImportWindow();" />
                <a class="link-button help-icon" href="javascript:void()" onclick="alert('help button');"></a>
                <a class="link-button refresh-icon" href="javascript:void()" onclick="bindMeasurementData();"></a>
            </div>
        </div>
        <div class="color_pallate_middle">
            <div id="measurementContainer">
            </div>
        </div>
        <div class="color_pallate_bottom">
            &nbsp; 
        </div>
    </div>
    <div style="float: right; margin-right: 20px;">
        <table style="width: 100%; vertical-align: top; text-align: center;">
            <tr>
                <td style="vertical-align: text-top; text-align: left;">
                    <span class="greycolorText">SN:</span>
                </td>
                <td style="vertical-align: text-top; text-align: left;">
                    <asp:Label ID="lblSN" runat="server" Text="30369" CssClass="greycolorText"></asp:Label>
                </td>
                <td style="vertical-align: text-top; text-align: left; padding-left: 5px;"></td>
                <td style="vertical-align: text-top; text-align: left;">
                    <span class="greycolorText">PID: </span>
                </td>
                <td style="vertical-align: text-top; text-align: left;">
                    <asp:Label ID="lblPID" runat="server" Text="12345" CssClass="greycolorText"></asp:Label>
                </td>
                <td style="vertical-align: text-top; text-align: left; padding-left: 5px;"></td>
                <td style="vertical-align: text-top; text-align: left;">
                    <span class="greycolorText">ORID:</span>
                </td>
                <td style="vertical-align: text-top; text-align: left;">
                    <asp:Label ID="lblORID" runat="server" Text="54321" CssClass="greycolorText"></asp:Label>
                </td>
            </tr>

        </table>
    </div>

</div>
<asp:HiddenField runat="server" ID="hdnBasePluginServiceUrl" ClientIDMode="Static" />
<asp:HiddenField ID="hdnPCSType" runat="server" />
<asp:HiddenField runat="server" ID="hdfAuthToken" ClientIDMode="Static" />
<asp:HiddenField ID="hdReferenceChartDisplayType" runat="server" />
<asp:HiddenField ID="hdIsVerification" runat="server" />
<asp:HiddenField ID="hdLicenseType" runat="server" />
<asp:HiddenField ID="hdOptimizeRunId" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hdProjectId" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hdReferenceChartTypeId" runat="server" />
<asp:HiddenField ID="hdSerialNumber" runat="server" Value="0" />
<asp:HiddenField ID="hdHasMeasurement" runat="server" Value="0" />
<asp:HiddenField ID="hdMeasurementsTotal" runat="server" Value="0" />
<asp:HiddenField ID="hdDisplayImport" runat="server" Value="no" />
<asp:HiddenField ID="hdDeviceType" runat="server" />
<asp:HiddenField ID="hdSpotProjectType" runat="server" />
<asp:HiddenField ID="hdnbtnAnalyzeParent" runat="server" />



<telerik:RadWindowManager ID="RadWindowManager" runat="server" Modal="True">
    <Windows>
        <telerik:RadWindow ID="ImportMeasurementWindow" runat="server" VisibleStatusbar="false"
            ShowContentDuringLoad="false" Title="Import Measurements" Width="730" Height="400"
            Behaviors="Close, Move" Behavior="Close, Move" NavigateUrl="~/PopupImportMeasurements.aspx">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>

<!-- modal content -->
<div id='MeasurementConfirmDialog'>
    <div class='header'>
        <span></span>
    </div>
    <div class='message'>
    </div>
    <div class='buttons'>
        <div class='no simplemodal-close'>
            <input type="button" id="btnCloseModelPopup" value='<%= GetResourceString("btnCloseModelPopup") %>' style="border: none; background-color: transparent; padding-top: 4px" />
        </div>
    </div>
</div>


<script id="MeasurementHeaderTemplate" type="text/x-jQuery-tmpl">
    <tr>
        <th scope="col">
            <b>#</b>
        </th>
        <th scope="col">
            <%= GetResourceString("measurementName") %>
        </th>
        <th scope="col">
            <%= GetResourceString("uploadDateTime") %>
        </th>
        <th scope="col">
            <%= GetResourceString("deleteButton") %>
        </th>
    </tr>
</script>

<script id="MeasurementDetailTemplate" type="text/x-jQuery-tmpl">
    <tr id="${MeasurementId}">
        <td align="center">
            <span id="lblAutoincr">${Autoincrement()}</span>
        </td>
        <td align="center">
            <span id="lblMeasurementName">${MeasurementName}</span>
        </td>
        <td align="center">
            <span id="lblMeasurementDate">${UploadDate}</span>
        </td>
        <td align="center">
            <input
                type="image"
                onclick="javascript: return ConfirmDeleteMeasurement(this);"
                alt="Delete"
                src="Images/delete_icon.png" title="Delete Measurement"
                id="imgbtnDelete"
                name=" ${MeasurementId}">
        </td>
    </tr>
</script>




<script lang="javascript" type="text/javascript">
    var BASE_SERVICE_URL = '';
    var AutoNumber = 0;
    jQuery(document).ready(function () {
        BASE_SERVICE_URL = $('#hdnBasePluginServiceUrl').val();
        bindMeasurementData();
    });

    function bindMeasurementData() {
        var OptimizeRunId = jQuery("input[id$=hdOptimizeRunId]").val();
        var ProjectId = jQuery("input[id$=hdProjectId]").val();
        var getMeasurementUrl = BASE_SERVICE_URL;
        if (jQuery("input[id$=hdIsVerification]").val() == "true") {
            getMeasurementUrl = "Services/PluginDataService.svc/GetVMeasurementsByProjectId/" + ProjectId;
            //getMeasurementUrl += "PluginData.svc/GetVMeasurementsByProjectId/" + ProjectId;
        }
        else if (jQuery("input[id$=hdLicenseType]").val() == '<%= (int)FujifilmUSA.Sync.Entity.ELicenseType.Sync1D %>') {
            getMeasurementUrl = "Services/PluginDataService.svc/Get1DMeasurementsByOptimizeId/" + OptimizeRunId;
            //getMeasurementUrl += "PluginData.svc/Get1DMeasurementsByOptimizeId/" + OptimizeRunId;
        }
        else if (jQuery("input[id$=hdLicenseType]").val() == '<%= (int)FujifilmUSA.Sync.Entity.ELicenseType.SpotLibrary %>') {
            getMeasurementUrl = "Services/PluginDataService.svc/GetSpotMeasurementsByOptimizeId/" + OptimizeRunId;
            //getMeasurementUrl += "PluginData.svc/GetSpotMeasurementsByOptimizeId/" + OptimizeRunId;
        }
        else {
            getMeasurementUrl = "Services/PluginDataService.svc/GetMeasurementsByOptimizeId/" + OptimizeRunId;
            //getMeasurementUrl += "PluginData.svc/GetMeasurementsByOptimizeId/" + OptimizeRunId;
        }

        jQuery.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: getMeasurementUrl,
            processData: false,
            dataType: "json",
            cache: false,
            dataFilter:
                function (data, type) {
                    var json = jQuery.parseJSON(data);
                    if (json.length > 0) {
                        json = json.sort((a, b) => b.MeasurementId - a.MeasurementId); // Descending order                         
                        AutoNumber = json.length+1;
                        $('#' + $('#<%=hdnbtnAnalyzeParent.ClientID%>').val() + '').show();

                        jQuery("input[id*='hdHasMeasurement']").val('true');
                        $("#measurementContainer").html("");
                        $("#measurementContainer").append("<table id ='lvMeasurement1'  cellspacing='0' cellpadding='0' border='0' class='recent_upload'><tbody></tbody></table>");
                        $("#MeasurementHeaderTemplate").tmpl().appendTo("#lvMeasurement1");
                        $("#MeasurementDetailTemplate").tmpl(json).appendTo("#lvMeasurement1");
                        jQuery("input[id*='hdMeasurementsTotal']").val(json.length);  //----------Set MeasurementLimit in hiddeninput-------------//      
                        //----------------Display Import Button--------------//  
                        if (jQuery("input[id*='hdDisplayImport']").val() == "yes") {
                            if (json.length < <%= (FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentProject.LicenseType == FujifilmUSA.Sync.Entity.ELicenseType.Sync1D ? ApplicationConfiguration.Instance.MeasurementLimitSync1D : ApplicationConfiguration.Instance.MeasurementsLimit)%>) {
                                $("#Div_ImportMeasurements").css('visibility', 'visible');
                            }
                            else {
                                $("#Div_ImportMeasurements").css('visibility', 'hidden');
                            }
                        }
                        else {
                            $("#Div_ImportMeasurements").css('visibility', 'hidden');
                        }
                    }
                    else {
                        $('#' + $('#<%=hdnbtnAnalyzeParent.ClientID%>').val() + '').hide();

                        jQuery("input[id*='hdHasMeasurement']").val('false');
                        $("#measurementContainer").html("");
                        $("#measurementContainer").append("<div class='no_data_found'>" + '<%= GetResourceString("noRecordsAvailable") %>' + "</div>");
                        //----------------Display Import Button--------------//
                        if (jQuery("input[id*='hdDisplayImport']").val() == "yes") {
                            $("#Div_ImportMeasurements").css('visibility', 'visible');
                        }
                        else {
                            $("#Div_ImportMeasurements").css('visibility', 'hidden');
                        }
                    }
                },
            success:
                function (response) { },
            error:
                function (XMLHttpRequest, textStatus, errorThrown) {
                    var jsonFault = JSON.parse(XMLHttpRequest.responseText);
                    MeasurementDialog('<%= GetResourceString("errorCause") %>' + jsonFault.Reason + " " + '<%= GetResourceString("errorDetail") %>' + jsonFault.DetailedInformation, "Error");
                }
        });
    }

    function ConfirmDeleteMeasurement(measurement) {
        if (confirm('<%= AddMeasurement_DeleteMeasurement_Confirmation %>')) {
            deletemeasurement(measurement.name);
            bindMeasurementData();
        }
        return false;
    }
    function Autoincrement() {        
        AutoNumber = parseInt(AutoNumber) - 1;
        return AutoNumber
    }
    function deletemeasurement(smeasurementId) {
        var measurementId = $.trim(smeasurementId);
        //var deleteMeasurementUrl = BASE_SERVICE_URL;
        if (jQuery("input[id$=hdIsVerification]").val() == "true") {
            deleteMeasurementUrl = "Services/PluginDataService.svc/DeleteVMeasurement/" + measurementId;
            //deleteMeasurementUrl += "PluginData.svc/DeleteVMeasurement/" + measurementId;
        }
        else if (jQuery("input[id$=hdLicenseType]").val() == '<%= (int)FujifilmUSA.Sync.Entity.ELicenseType.Sync1D %>') {
            deleteMeasurementUrl = "Services/PluginDataService.svc/Delete1DMeasurement/" + measurementId;
            //deleteMeasurementUrl += "PluginData.svc/Delete1DMeasurement/" + measurementId;
        }
        else if (jQuery("input[id$=hdLicenseType]").val() == '<%= (int)FujifilmUSA.Sync.Entity.ELicenseType.SpotLibrary %>') {
            deleteMeasurementUrl = "Services/PluginDataService.svc/DeleteSpotMeasurement/" + measurementId;
            //deleteMeasurementUrl += "PluginData.svc/DeleteSpotMeasurement/" + measurementId;
        }
        else {
            deleteMeasurementUrl = "Services/PluginDataService.svc/DeleteMeasurement/" + measurementId;
            //deleteMeasurementUrl += "PluginData.svc/DeleteMeasurement/" + measurementId;
        }
        jQuery.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: deleteMeasurementUrl,
            processData: false,
            dataType: "json",
            dataFilter: function (data, type) {
                return false;
            },
            headers: {
                'Authorization': 'Basic ' + $('#hdfAuthToken').val()
            },
            success:
                function (response) {
                },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                var jsonFault = JSON.parse(XMLHttpRequest.responseText);
                MeasurementDialog('<%= GetResourceString("errorCause") %>' + jsonFault.Reason + " " + '<%= GetResourceString("errorDetail") %>' + jsonFault.DetailedInformation, "Error");
            }
        });
    }
    function GetDate(jsonDate) {
        var value = new Date(parseInt(jsonDate.substr(6)));
        return value.getMonth() + 1 + "/" + value.getDate() + "/" + value.getFullYear() + " " + value.toLocaleTimeString();
    }


    function ControlDisability(btnCalibrate, CalibrateFlag, btnMeasure, MeasureFlag, btnCancel, CancelFlag, btnRewind, RewindFlag) {
        if (btnCalibrate == 1) {
            if (CalibrateFlag == true) {
                jQuery("[id*='btnCalibrate']").attr('disabled', '');
            }
            else {
                jQuery("[id*='btnCalibrate']").removeAttr('disabled', '');
            }
        }
        if (btnMeasure == 1) {
            if (MeasureFlag == true) {
                jQuery("[id*='btnMeasureAndUploadChart']").attr('disabled', '');
            }
            else {
                jQuery("[id*='btnMeasureAndUploadChart']").removeAttr('disabled', '');
            }
        }
        if (btnCancel == 1) {
            if (CancelFlag == true) {
                jQuery("[id*='btnCancelMeasurement']").attr('disabled', '');
            }
            else {
                jQuery("[id*='btnCancelMeasurement']").removeAttr('disabled', '');
            }
        }
        if (btnRewind == 1) {
            if (RewindFlag == true) {
                jQuery("[id*='btnRewindMeasurement']").attr('disabled', '');
            }
            else {
                jQuery("[id*='btnRewindMeasurement']").removeAttr('disabled', '');
            }
        }
    }

    /************ Progress bar ***************/
    //function LoadProgressBrowserWindow() {
    //    var divBackgroundProgress = document.getElementById('divBackgroundProgressMaster');
    //    var divProgressImage = document.getElementById('divProgressImageMaster');

    //    divBackgroundProgress.style.display = 'block';
    //    divProgressImage.style.display = 'block';

    //    var winWidth;
    //    var winHeight;
    //    winWidth = (window.screen.width / 2);
    //    winHeight = (window.screen.height / 2);

    //    divProgressImage.style.left = winWidth + 'px';
    //    divProgressImage.style.top = winHeight + 'px';

    //}
    function StopProgressBrowserWindow() {
        jQuery("[id*='divBackgroundProgressMaster']").hide();
        jQuery("[id*='divProgressImageMaster']").hide();
    }

    function ShowImportWindow() {
        var oWnd = $find("<%= ImportMeasurementWindow.ClientID %>");
        oWnd.show();
        return false;
    }

    function Close() {
        var oWnd = $find("<%= ImportMeasurementWindow.ClientID %>");
        oWnd.close();
        return false;
    }

    function MeasurementDialog(message, type) {
        jQuery("[id*='MeasurementConfirmDialog']").modal({
            closeHTML: "<a href='#' title='Close' class='modal-close'>x</a>",
            position: ["20%",],
            overlayId: 'MeasurementConfirmDialog-overlay',
            containerId: 'MeasurementConfirmDialog-container',
            onShow: function (dialog) {
                var modal = this;
                jQuery('.message', dialog.data[0]).append(message);
                jQuery('.header', dialog.data[0]).append(type);
            }
        });
    }

</script>

