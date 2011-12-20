//Date Serialize Issue: http://stackoverflow.com/questions/206384/how-to-format-json-date
Date.prototype.toMSJSON = function () {
    var date = '/Date(' + this.getTime() + ')/'; //the number of milliseconds since midnight of January 1, 1970 and the specified date.
    return date;
};

function GetReferenceChart(ProjectID) {
    
    //Make ajax request...
    jQuery.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "Services/PluginDataService.svc/GetReferenceChart/" + ProjectID,
        //data: JSON.stringify({ ProjectID: ProjectID }),
        processData: false,
        dataType: "json", //this only applies to response from the server and (not request send to server)
        //If the call succeeds
        success:
        function (response) {
           //ReferenceChart object returned as JSON from WCF
            var objChart = response;
            SetChartResult(objChart);
        },
        //If the call fails
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var jsonFault = JSON.parse(XMLHttpRequest.responseText);
            jQuery.growlUI('Error', "Error Cause:" + jsonFault.Reason + "</ br> Error Detail:" + jsonFault.DetailedInformation, 20000);
        }
    });
}

//this method initialize global chart object called g_chart which in turns being pass to the plug-in
function SetChartResult(objChart) {
    //var g_paper = new Array(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
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

    
    if (objChart.DeviceSetting != null && objChart.DeviceSetting != "undefined") {
        g_Chart["DeviceSettings"]["Colorimetric.Illumination"] = objChart.DeviceSetting.Illuminant;
        g_Chart["DeviceSettings"]["Colorimetric.DensityStandard"] = objChart.DeviceSetting.DensityStandard;
        g_Chart["DeviceSettings"]["Colorimetric.Observer"] = objChart.DeviceSetting.Observer;
        g_Chart["Mode"] = objChart.DeviceSetting.Mode;
    }
    
    if (objChart.MeasurementDeviceTypeId > 1 && objChart.ChartSettings != null) {
        g_Chart["ChartType"] = "Native";
        g_Chart["ChartSizeX"] = objChart.ChartSettings["ChartSizeX"];
        g_Chart["ChartSizeY"] = objChart.ChartSettings["ChartSizeY"];
        g_Chart["BarSizeX"] = objChart.ChartSettings["BarSizeX"];
        g_Chart["BarSizeY"] = objChart.ChartSettings["BarSizeY"];
        g_Chart["DistToBar"] = objChart.ChartSettings["DistToBar"];
        g_Chart["DistToFirstLineY"] = objChart.ChartSettings["DistToFirstLineY"];
        g_Chart["MarkerDistance"] = objChart.ChartSettings["MarkerDistance"];
        g_Chart["DistToFirstField"] = objChart.ChartSettings["DistToFirstField"];
        g_Chart["PatchSizeX"] = objChart.ChartSettings["PatchSizeX"];
        g_Chart["PatchSizeY"] = objChart.ChartSettings["PatchSizeY"];
    }


    
    g_Chart["RefBase"] = new Array();
    for (i = 0; i < g_Chart["Size"]; i++) {        
        var patch = new Object();
        patch["SampleName"] = objChart.ChartPatches[i].SampleName;                
        patch["HTMLColor"] = objChart.ChartPatches[i].HtmlColor;
        patch["CMYK_C"] = objChart.ChartPatches[i].CmykC;
        patch["CMYK_M"] = objChart.ChartPatches[i].CmykM;
        patch["CMYK_Y"] = objChart.ChartPatches[i].CmykY;
        patch["CMYK_K"] = objChart.ChartPatches[i].CmykK;
        patch["PatchTypeID"] = objChart.ChartPatches[i].PatchTypeID;
        g_Chart["RefBase"].push(patch);
    }
    
    
    //clear the Measured patch array
    if (MeasuredPatches.length > 0) MeasuredPatches.length = 0;
}