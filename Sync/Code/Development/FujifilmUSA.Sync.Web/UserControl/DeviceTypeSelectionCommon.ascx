<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeviceTypeSelectionCommon.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.DeviceTypeSelectionCommon" %>
<script type="text/javascript" language="javascript">
    function cmbDeviceTypeChangeEvent() {
        var measurementDevice = jQuery("[id$=cmbDeviceType]").val();
        var measurementDeviceId = jQuery.trim(measurementDevice.split('-')[1]);


        //Make ajax request...
        jQuery.ajax({
            type: "GET", //for complex object POST is preferred
            contentType: "application/json; charset=utf-8",   //this is very important application/json; charset=utf-8";  else it will fail
            url: "Services/PluginDataService.svc/GetReferenceChartTypeListByMeasurementDeviceId/" + measurementDeviceId,
            processData: false, //     I found that if I use processData=true, then the resulting string sent to the service is in querystring format, not in JSON. Not what I want for passing complex objects. So I set it to false. Using true would be fine for simpler non-JSON requests where you're doing WebGet, and all the params are in the query string
            cache: false,
            dataType: "json",  ////this only applies to response from the server and (not request send to server)
            //            dataFilter: function (data, type) {

            //                var ddAlignData = jQuery.parseJSON(data);
            //                var cmbAlignmentChart = jQuery("select[id$=cmbAlignmentChart]");
            //                cmbAlignmentChart.empty();
            //                $.each(ddAlignData, function (index, optionData) {
            //                    cmbAlignmentChart.append("<option value='"
            //                    + optionData.Key
            //                    + "'>" + optionData.Value
            //                    + "</option>");
            //                });
            //            },
            //If the call succeeds
            success:
                            function (response) {                                
                                var cmbAlignmentChart = jQuery("select[id$=cmbAlignmentChart]");
                                cmbAlignmentChart.empty();
                                $.each(response, function (index, optionData) {
                                    cmbAlignmentChart.append("<option value='"
                                    + optionData.Key
                                    + "'>" + optionData.Value
                                    + "</option>");
                                });
                                cmbAlignmentChartChangeEvent()
                            },
            //If the call fails
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                var jsonFault = JSON.parse(XMLHttpRequest.responseText);
                MeasurementDialog("Error Cause:" + jsonFault.Reason + " Error Detail:" + jsonFault.DetailedInformation, "Error");
            }
        });
    }
    var isLoad = false;
    function cmbAlignmentChartChangeEvent() {
        if (isLoad == false) 
        {
            var selectedval = jQuery("[id$='cmbAlignmentChart'] :selected").val();
            jQuery("input[id$=hdAlignmentChartSelectedValue]").val(selectedval);
        }
        else
        {
            var selectedval = jQuery("input[id$=hdAlignmentChartSelectedValue]").val();
            if (selectedval == null || selectedval == undefined || selectedval == '' || selectedval == '0') // When Edit call seleted value set from database.
            {
                var selectedval = jQuery("[id$='cmbAlignmentChart'] :selected").val();
                jQuery("input[id$=hdAlignmentChartSelectedValue]").val(selectedval);                
            }
            else // When inset selected value set bydefualt first value of combo. 
            {
                $("select[id$=cmbAlignmentChart]").val(selectedval);
            }
            isLoad = false;
        }

    }
    $(document).ready(function () {
        isLoad = true;
        jQuery("select[id$=cmbDeviceType]").change(cmbDeviceTypeChangeEvent);
        jQuery("select[id$=cmbAlignmentChart]").change(cmbAlignmentChartChangeEvent);
        cmbDeviceTypeChangeEvent();
    });

    function GetSelectedValue() {
        
    }
</script>
<td class="ProjectListtd1">
    Device Type &nbsp; :
</td>
<td>
    <asp:DropDownList ID="cmbDeviceType" runat="server" DataValueField="Key" DataTextField="Value"
        CssClass="select" Width="143px">
    </asp:DropDownList>
    <%--<select id="cmbDeviceType" runat="server" datavaluefield="Key" datatextfield="Value"
        class="select" width="143px">
    </select>--%>
</td>
<td>
    <asp:Label ID="lblAlignmentChart" runat="server" Text="Alignment Chart:"></asp:Label>&nbsp;
</td>
<td>
    <asp:DropDownList ID="cmbAlignmentChart" runat="server" DataValueField="Key" DataTextField="Value"
        CssClass="select " Width="143px" EnableViewState="false">        
    </asp:DropDownList>
    <asp:HiddenField ID="hdAlignmentChartSelectedValue" runat="server">
        </asp:HiddenField>

    <%--<select id="cmbAlignmentChart" name="cmbAlignmentChart" runat="server" datavaluefield="Key" datatextfield="Value"
        class="select" width="143px">
    </select>--%>
</td>
