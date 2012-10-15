Date.prototype.toMSJSON = function () {
    var date = '/Date(' + this.getTime() + ')/'; //the number of milliseconds since midnight of January 1, 1970 and the specified date.
    return date;
};
//here we are adding dummy querystring in URL as IE do not invoke service each and every time rather it takes from cache
function GetReferenceChart(ProjectID, ReferenceChartDisplayTypeId) {
    jQuery.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "Services/PluginDataService.svc/GetReferenceChart/" + ProjectID + "/" + ReferenceChartDisplayTypeId + "?TempId=" + Math.floor(Math.random() * 1000),
        processData: false,
        dataType: "json",
        success:
        function (response) {
            var objChart = response;
            SetChartResult(objChart);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var jsonFault = JSON.parse(XMLHttpRequest.responseText);
            jQuery.growlUI('Error', "Error Cause:" + jsonFault.Reason + "</ br> Error Detail:" + jsonFault.DetailedInformation, 20000);
        }
    });
}

function SetChartResult(objChart) {
    g_Chart["DeviceSettings"] = new Object();
    g_Chart["RowNames"] = new Array();
    g_Chart["Size"] = objChart.TotalPatches;
    g_Chart["Name"] = objChart.ChartName;
    g_Chart["NumberOfRows"] = objChart.NumOfRows;
    g_Chart["NumberOfColumns"] = objChart.NumOfCols;
    actKey = objChart.ActivationKey;
    g_Chart["ReadCS"] = "Density,CIELab,CIEXYZ,sRGB,Spectral";

    for (m = 1; m <= g_Chart["NumberOfRows"]; m++) {
        g_Chart["RowNames"].push("row " + m);
    }

    if (objChart.DeviceSetting != null && objChart.DeviceSetting.MeasurementSettings != null) {
        g_Chart["MeasurementDeviceSerial"] = objChart.DeviceSetting.SerialNumber;
        g_Chart["DeviceSettings"]["Colorimetric.Illumination"] = objChart.DeviceSetting.MeasurementSettings.IlluminantName;
        g_Chart["DeviceSettings"]["Colorimetric.DensityStandard"] = objChart.DeviceSetting.MeasurementSettings.DensityType;
        g_Chart["DeviceSettings"]["Colorimetric.Observer"] = objChart.DeviceSetting.MeasurementSettings.ObserverTypeName;
        g_Chart["Mode"] = objChart.DeviceSetting.MeasurementSettings.UvIncludedName;
    }

    if (objChart.ChartSetting != null) {
        g_Chart["ChartType"] = "Native";
        g_Chart["ChartSizeX"] = objChart.ChartSetting["ChartSizeX"];
        g_Chart["ChartSizeY"] = objChart.ChartSetting["ChartSizeY"];
        g_Chart["BarSizeX"] = objChart.ChartSetting["BarSizeX"];
        g_Chart["BarSizeY"] = objChart.ChartSetting["BarSizeY"];
        g_Chart["DistToBar"] = objChart.ChartSetting["DistToBar"];
        g_Chart["DistToFirstLineY"] = objChart.ChartSetting["DistToFirstLineY"];
        g_Chart["MarkerDistance"] = objChart.ChartSetting["MarkerDistance"];
        g_Chart["DistToFirstField"] = objChart.ChartSetting["DistToFirstField"];
        g_Chart["PatchSizeX"] = objChart.ChartSetting["PatchSizeX"];
        g_Chart["PatchSizeY"] = objChart.ChartSetting["PatchSizeY"];
        //For IO devices...
        g_Chart["FirstSample"] = objChart.ChartSetting["FirstSample"];
        g_Chart["ReadDirection"] = objChart.ChartSetting["ReadDirection"];
        g_Chart["ReadMode"] = objChart.ChartSetting["ReadMode"];
        g_Chart["RowsPerPage"] = objChart.ChartSetting["RowsPerPage"];
        g_Chart["Pages"] = objChart.ChartSetting["Pages"];
    }

    g_Chart["RefBase"] = new Array();
    for (i = 0; i < g_Chart["Size"]; i++) {
        var patch = new Object();
        patch["SampleName"] = objChart.ReferenceCharts[i].SampleName;
        patch["HTMLColor"] = objChart.ReferenceCharts[i].HtmlColor;
        patch["CMYK_C"] = objChart.ReferenceCharts[i].CmykC;
        patch["CMYK_M"] = objChart.ReferenceCharts[i].CmykM;
        patch["CMYK_Y"] = objChart.ReferenceCharts[i].CmykY;
        patch["CMYK_K"] = objChart.ReferenceCharts[i].CmykK;
        patch["PatchTypeID"] = objChart.ReferenceCharts[i].PatchTypeId;
        g_Chart["RefBase"].push(patch);
    }

    //clear the Measured patch array
    if (MeasuredPatches.length > 0) MeasuredPatches.length = 0;
}