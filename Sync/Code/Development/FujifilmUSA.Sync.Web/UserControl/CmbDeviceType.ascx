<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CmbDeviceType.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.CmbDeviceType" %>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

    
            jQuery("select[id$=cmbDeviceType]").change(
                function () {
                    var measurementDevice = jQuery("[id$=cmbDeviceType]").val();
                    var measurementDeviceId = jQuery.trim(measurementDevice.split('-')[1]);

                    //Make ajax request...
                    jQuery.ajax({
                        type: "GET", //for complex object POST is preferred
                        contentType: "application/json; charset=utf-8",   //this is very important application/json; charset=utf-8";  else it will fail
                        url: "Services/PluginDataService.svc/GetReferenceChartTypeListByMeasurementDeviceId/" + measurementDeviceId,
                        processData: false, //     I found that if I use processData=true, then the resulting string sent to the service is in querystring format, not in JSON. Not what I want for passing complex objects. So I set it to false. Using true would be fine for simpler non-JSON requests where you're doing WebGet, and all the params are in the query string
                        dataType: "json",  ////this only applies to response from the server and (not request send to server)
                        dataFilter: function (data, type) {
                            var cmbAlignmentChart = jQuery("select[id$=cmbAlignmentChart]");
                            cmbAlignmentChart.find('option').remove();

                            var json = jQuery.parseJSON(data);
                            var i = 1;
                            for (i = 0; i < json.length; i++) {
                                jQuery("select[id$=cmbAlignmentChart]").append('<option value=' + json[i].Key + '>' + json[i].Value + '</option>');
                            }
                        },
                        //If the call succeeds
                        success:
                            function (response) {
                                GetRefChartData();
                            },
                        //If the call fails
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var jsonFault = JSON.parse(XMLHttpRequest.responseText);
                            MeasurementDialog("Error Cause:" + jsonFault.Reason + " Error Detail:" + jsonFault.DetailedInformation, "Error");
                        }
                    });
                });
        });
    </script>


     <td class="ProjectListtd1" >
        Device Type &nbsp; :
    </td>
    <td>
        <asp:DropDownList ID="cmbDeviceType" runat="server" DataValueField="Key" DataTextField="Value"
            CssClass="select" Width="180px">
        </asp:DropDownList>
    </td>

    <td class="ProjectListtd1">
        Alignment Chart &nbsp; :
    </td>
    <td>
        <asp:DropDownList ID="cmbAlignmentChart" runat="server" DataValueField="Key" DataTextField="Value"
            CssClass="select " Width="180px" onchange=" GetRefChartData();">
        </asp:DropDownList>
    </td>

