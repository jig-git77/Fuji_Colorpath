<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddMeasurement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddMeasurement" %>
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js" type="text/javascript"></script>
<script type="text/javascript">
    if (typeof jQuery == 'undefined') {
        document.write(unescape("%3Cscript src='JQuery/jquery-1.6.2.min.js' type='text/javascript'%3E%3C/script%3E"));
    }   
   
    function ConfirmDeleteMeasurement() {
        if (confirm('<%= AddMeasurement_DeleteMeasurement_Confirmation %>'))
            return true;
        else
            return false;
    }

</script>
<script src="JS/wz_jsgraphics.js" type="text/javascript"></script>
<script src="jQuery/json2.js" type="text/javascript"></script>
<script src="JQuery/jquery.simplemodal.js" type="text/javascript"></script>
<script src="jQuery/jquery-blockUI.js" type="text/javascript"></script>
<script src="jQuery/wtooltip.min.js" type="text/javascript"></script>
<script src="JS/excanvas.js" type="text/javascript"></script>
<script src="JS/chartread.js" type="text/javascript"></script>
<script src="JS/i1read.js" type="text/javascript"></script>
<script src="JS/isisread.js" type="text/javascript"></script>
<script src="JS/WebComm.js" type="text/javascript"></script>
<style>
    .my_rectangle
    {
        width: 25px;
        height: 25px;
        background: pink;
    }
    .tooltipclass
    {
        background: #fbfacd;
        border-width: 1px;
        border-style: solid;
        border-color: #000;
    }
    
    .pluginImageTooltipClass
    {
        background: #fbfacd;
        border-width: 1px;
        border-style: solid;
        border-color: #000;
        font-family: Tahoma;
        font-size: 11px;
    }
    #modalBackground
    {
        position: fixed;
        left: 0;
        top: 0;
        width: 100%;
        height: 100%;
        z-index: 9;
        background-color: #333333;
        display: none;
        opacity: 0.40;
        filter: alpha(opacity=40);
    }
    #modalWindow
    {
        position: fixed;
        left: 0;
        top: 0;
        z-index: 10;
        background-color: white;
        display: none;
        width: 40em;
        height: 10em;
    }
</style>
<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <h1>
            Add Measurement</h1>
        <div class="Information_divAnalyze">
            <asp:LinkButton ID="btnAnalyze" runat="server" CssClass="button" OnClick="btnAnalyze_Click">
                <span>
                    <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/analyze.png" />
                    Analyze</span>
            </asp:LinkButton>
        </div>
    </div>
    <div class="clear">
    </div>
    <div class="info_form">
        <table id="tblMessage" runat="server" width="100%" style="border: 1px solid #0C3D30;
            background-color: #6BB19C; color: Black; height: 45px;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="padding: 2px 2px 2px 5px; font-weight: bold;" valign="top">
                    <asp:Label ID="lblMessage" runat="server" Text="Status : Scanning device is not loaded."></asp:Label>
                </td>
                <td style="padding: 2px 5px 2px 2px; font-weight: bold;" align="right" valign="top">
                    <asp:Label ID="lblPluginMessage" runat="server" Text="loading..."></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="info_form">
        <div class="float_l InformationtxtDivWidth">
            <asp:Label ID="lblMeasurementName" runat="server" Text="Measurement Name" CssClass="LabelTitle"></asp:Label>
            &nbsp;
            <asp:TextBox ID="txtMeasurementName" runat="server" SkinID="width250px">
            </asp:TextBox>
            <%--<telerik:RadTextBox ID="txtMeasurementName" runat="server" SkinID="width250px">
            </telerik:RadTextBox>--%>
        </div>
        <div class="float_r">
            <span>
                <input type="button" id="btnCalibrate" value="Calibrate" /></span>
            <%--<telerik:RadButton ID="btnCalibrate" runat="server" Text="Calibrate">
            </telerik:RadButton>--%>
        </div>
        <div class="clear">
        </div>
        <div class="float_l">
            <input type="button" id="btnMeasureAndUploadChart" value="Measure / Upload" />
            <%--<telerik:RadButton ID="btnMeasureAndUploadChart" runat="server" Text="Measure / Upload">
            </telerik:RadButton>--%>
        </div>
        <div class="float_r">
            <input type="button" id="btnCancelMeasurement" value="Cancel Measurement" />
            <%--<telerik:RadButton ID="btnCancelMeasurement" runat="server" Text="Cancel Measurement">
            </telerik:RadButton>--%>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="color_pallate">
        <div class="color_pallate_head">
            Colorbar</div>
        <div class="color_pallate_middle">
            <div class="color_bar">
                <div id="Canvas" style="position: relative; height: 350px; width: 630px; overflow: visible;">
                </div>
            </div>
        </div>
        <div class="color_pallate_bottom">
            &nbsp;</div>
    </div>
    <div class="color_pallate">
        <div class="color_pallate_head">
            Recent Upload</div>
        <div class="color_pallate_middle">
            <%-- <telerik:RadGrid ID="grdRecentUploadList" runat="server" AutoGenerateColumns="false"
                OnNeedDataSource="grdRecentUploadList_NeedDataSource" BorderStyle="None" Width="99.9%">
                <MasterTableView>
                    <Columns>
                        <telerik:GridBoundColumn DataField="MeasurementName" UniqueName="MeasurementName"
                            HeaderText="Measurement Name">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="UploadDateTime" UniqueName="UploadDateTime" HeaderText="Upload Date Time">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Delete">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/delete_icon.png" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>--%>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btnBindGrid" runat="server" Text="Bind" Style="display: none;" OnClick="btnBindGrid_Click" />
                    <asp:ListView ID="lvMeasurement" runat="server" ItemPlaceholderID="phItemPlaceholder"
                        OnItemCommand="lvMeasurement_ItemCommand">
                        <LayoutTemplate>
                            <table border="0" cellspacing="0" cellpadding="0" class="recent_upload">
                                <tr>
                                    <th scope="col">
                                        Measurement Name
                                    </th>
                                    <th scope="col">
                                        Upload Date Time
                                    </th>
                                    <th scope="col">
                                        Delete
                                    </th>
                                </tr>
                                <asp:PlaceHolder ID="phItemPlaceholder" runat="server"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <EmptyDataTemplate>
                            <div class="no_data_found">
                                No records available</div>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblMeasurementName" runat="server" Text='<%#Eval("MeasurementName") %>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lblMeasurementDate" runat="server" Text=' <%# Eval("MeasurementDate").ToString().Replace(":000", " ") %>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete Project" AlternateText="Delete"
                                        ImageUrl="../Images/delete_icon.png" CommandName="DeleteProject" CommandArgument='<%# Eval("MeasurementId") %>' OnClientClick="javascript:return ConfirmDeleteMeasurement();" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <%-- <asp:UpdateProgress runat="server" DynamicLayout="true" ID="UP_Home1" AssociatedUpdatePanelID="UpdatePanel1"
                DisplayAfter="0">
                <ProgressTemplate>
                    <div id="DIV_Progress1" runat="server" class="PROGRESS" align="center">
                        <img src="Images/Loading2.gif" style="background: transparent;" alt="Loading" /><br />
                        <b>Loading</b></div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>
        </div>
        <div class="color_pallate_bottom">
            &nbsp;</div>
    </div>
    <div class="clear">
    </div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
<!-- modal content -->
<div id='confirm'>
    <div class='header'>
        <span></span>
    </div>
    <div class='message'>
    </div>
    <div class='buttons'>
        <div class='no simplemodal-close'>
            <input type="button" value="OK" id="btnCloseModelPopup" style="border: none; background-color: transparent;
                padding-top: 4px" /></div>
    </div>
</div>
<p>
    <asp:HiddenField ID="hdSyncRunID" runat="server" />
    <asp:HiddenField ID="hdProjectID" runat="server" />
    <asp:HiddenField ID="hdMeasurementTypeID" runat="server" />
</p>
<object id="plugin0" type="application/x-fujicmplugin" width="0" height="0">
</object>
<script language="javascript" type="text/javascript">
    var refreshTry = 0;
    function ControlVisibility() {
        jQuery("[id*='btnCalibrate']").css('display', 'none');
        jQuery("[id*='btnMeasureAndUploadChart']").val('Measure Chart');
        jQuery("[id*='btnMeasureAndUploadChart']").css('display', 'none');
        jQuery("[id*='btnCancelMeasurement']").css('display', 'none');
        load();   //first you have to load the plugin
        GetReferenceChart((jQuery("[id*='hdProjectID']").val()));
        ListDevices();
    }
    jQuery(document).ready(function () {
        jQuery("[id*='btnCalibrate']").click(function () {
            calibrateDevice(g_ScannerList[0]);
            return false;
        });
        jQuery("[id*='btnMeasureAndUploadChart']").click(function () {
            if (jQuery("[id*='btnMeasureAndUploadChart']").val() == 'Measure Chart') {
                var MeasurementName = jQuery("[id*='txtMeasurementName']").val();
                if (jQuery.trim(MeasurementName).length < 4) {
                    MeasurementDialog("Please enter measurement name that is at least three characters long.", "Error");
                    return false;
                }


                scan(g_ScannerList[0], 'Density,CIELab,CIEXYZ,sRGB', g_Chart);
            }
            else if (jQuery("[id*='btnMeasureAndUploadChart']").val() == 'Upload Measurement') {
                upload();
                document.getElementById('<%= btnBindGrid.ClientID %>').click();
                //call grid click event...
            }
        });

        jQuery("[id*='btnCancelMeasurement']").click(function () {
            refreshCanvas();
            jQuery("[id*='btnMeasureAndUploadChart']").val('Measure Chart');
            OnCancel();
            ListDevices();
        });
        jQuery("[id*='btnRefresh']").click(function () {
            refreshTry++;
            ControlVisibility();
            if (refreshTry == 2) {
                jQuery("[id*='lblMessage']").html(jQuery("[id*='lblMessage']").text() + '<br>Hint: Having problem with plugins? Try to close and re-open your browser!</br>');
            }
        });
    });

    function MeasurementDialog(message, type) {
        jQuery("[id*='confirm']").modal({
            closeHTML: "<a href='#' title='Close' class='modal-close'>x</a>",
            position: ["20%", ],
            overlayId: 'confirm-overlay',
            containerId: 'confirm-container',
            onShow: function (dialog) {
                var modal = this;
                jQuery('.message', dialog.data[0]).append(message);
                jQuery('.header', dialog.data[0]).append(type);
            }
        });
        jQuery("[id*='btnCloseModelPopup']").focus().click(function () {

        });
    }
    function saveMeasurement() {
        /****passing date and time from JavaScript to WCF serialization issue  http://stackoverflow.com/questions/4474352/send-jquery-json-to-wcf-rest-using-date ***/
        var dt = new Date();
        var dt1 = new Date(Date.UTC(dt.getFullYear(), dt.getMonth(), dt.getDate(), dt.getHours(), dt.getMinutes(), dt.getSeconds(), dt.getMilliseconds()));
        var MeasurementDateTime = dt1.toMSJSON();
        var SyncRunID = jQuery("input[id$=hdSyncRunID]").val();
        var MeasurementTypeID = jQuery("input[id$=hdMeasurementTypeID]").val();
        var MsmtName = jQuery("input[id*='txtMeasurementName']").val();
        var obj = { MeasurementName: MsmtName, MeasurementDate: MeasurementDateTime, SyncRunId: SyncRunID, MeasurementTypeId: MeasurementTypeID, Patches: MeasuredPatches };

        //Make ajax request...
        jQuery.ajax({
            type: "POST", //for complex object POST is preferred
            contentType: "application/json; charset=utf-8",   //this is very important application/json; charset=utf-8";  else it will fail
            url: "Services/PluginDataService.svc/SaveMeasurement",
            processData: false, //     I found that if I use processData=true, then the resulting string sent to the service is in querystring format, not in JSON. Not what I want for passing complex objects. So I set it to false. Using true would be fine for simpler non-JSON requests where you're doing WebGet, and all the params are in the query string
            dataType: "json",  ////this only applies to response from the server and (not request send to server)
            data: JSON.stringify(obj),
            dataFilter: function (data, type) {
                return data.replace(/"\\\/(Date\([0-9-]+\))\\\/"/gi, 'new $1');
            },
            //If the call succeeds
            success:
        function (response) {   //the response param passed to the success callback contains the returned json
            //document.getElementById('btnBindGrid.ClientID').click();
            MeasurementDialog("Response From Server', 'measurement added successfully", "Successfully saved");
            //jQuery.growlUI('Response From Server', 'measurement added successfully', 1000);

        },
            //If the call fails
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                var jsonFault = JSON.parse(XMLHttpRequest.responseText);
                MeasurementDialog("Error Cause:" + jsonFault.Reason + " Error Detail:" + jsonFault.DetailedInformation, "Error");
            }
        });

        /**clear the array **/
        OnCancel();
        refreshCanvas();
        jQuery("[id*='btnMeasureAndUploadChart']").val('Measure Chart');
    }
    if (window.addEventListener) // W3C standard
    {
        window.addEventListener('load', ControlVisibility(), false); // NB **not** 'onload'
    }
    else if (window.attachEvent) // Microsoft
    {
        window.attachEvent('onload', ControlVisibility());
    }
</script>
