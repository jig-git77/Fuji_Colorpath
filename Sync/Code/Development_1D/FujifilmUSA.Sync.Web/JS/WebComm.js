Date.prototype.toMSJSON = function () {
    var date = '/Date(' + this.getTime() + ')/'; //the number of milliseconds since midnight of January 1, 1970 and the specified date.
    return date;
};
//here we are adding dummy querystring in URL as IE do not invoke service each and every time rather it takes from cache
function GetReferenceChart(ProjectID, ReferenceChartDisplayTypeId, IsSpotProjectType,serviceURL) {
   // debugger;
    jQuery.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "Services/PluginDataService.svc/GetReferenceChart/" + ProjectID + "/" + ReferenceChartDisplayTypeId + "/" + IsSpotProjectType + "?TempId=" + Math.floor(Math.random() * 1000),
        //url: serviceURL+  "PluginData.svc/GetReferenceChart/" + ProjectID + "/" + ReferenceChartDisplayTypeId + "/" + IsSpotProjectType + "?TempId=" + Math.floor(Math.random() * 1000),
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
     g_Chart["ReadCS"] = "Spectral,Density,CIELab,CIEXYZ,sRGB";
    //g_Chart["ReadCS"] = "CIELab,CIEXYZ,sRGB,Density";

    for (m = 1; m <= g_Chart["NumberOfRows"]; m++) {
        g_Chart["RowNames"].push("row " + m);
    }

    if (objChart.DeviceSetting != null && objChart.DeviceSetting.MeasurementSettings != null) {
        g_Chart["MeasurementDeviceSerial"] = objChart.DeviceSetting.SerialNumber;
        g_Chart["DeviceSettings"]["Colorimetric.Illumination"] = objChart.DeviceSetting.MeasurementSettings.IlluminantName;
        g_Chart["DeviceSettings"]["Colorimetric.DensityStandard"] = objChart.DeviceSetting.MeasurementSettings.DensityType;
        g_Chart["DeviceSettings"]["Colorimetric.Observer"] = objChart.DeviceSetting.MeasurementSettings.ObserverTypeName;
       
        if (objChart.DeviceSetting.MeasurementSettings.UVMode == -1) {      //----------If NULL then pass UVCut bydefault
            objChart.DeviceSetting.MeasurementSettings.UVMode = 2;
        }
      g_Chart["Mode"] = objChart.DeviceSetting.MeasurementSettings.UvIncludedName;
       var UVMode = "M" + objChart.DeviceSetting.MeasurementSettings.UVMode;
        var InstrumentTypeID = objChart.InstrumentTypeId;
		
		  if (InstrumentTypeID == 8) {  //For isis....maybe only2???
		  g_Chart["ChartType"] = "Native";
		  g_Chart["Mode"] = UVMode;
		    //g_Chart["DeviceSettings"]["IlluminationCondition"] = UVMode;
            //if (UVMode == "M0" || UVMode == "M1") {  //isis m0 and m1

			//g_Chart["DeviceSettings"]["IlluminationMode"] = 1;
			//g_Chart["DeviceSettings"]["MeasurementFlow"] = 1;

           // }
            //else {                                   //For isis m2 uvcut
              // g_Chart["DeviceSettings"]["IlluminationMode"] = 0;
				//g_Chart["DeviceSettings"]["MeasurementFlow"] = 0;
            //}
			//g_Chart["Mode"] = UVMode;
        }
        
        else if (InstrumentTypeID == 4) {  //For i1- Pro2 
            if (UVMode == "M1" || UVMode == "M2") {  //For i1-Pro2 m1,m2=DualReflectanceScan
                g_Chart["DeviceSettings"]["MeasurementMode"] = "DualReflectanceScan";
                g_Chart["DeviceSettings"]["ResultIndexKey"] = UVMode; //Only needs to be set in Dual Measurement Modes, Cannot be set for i1Pro
            }
            else {                                   //For i1-Pro2 m0=ReflectanceScan
                g_Chart["DeviceSettings"]["MeasurementMode"] = "ReflectanceScan";
            }
        }
        else if (InstrumentTypeID == 5) {  // 1 For i1-IO- Pro2
	
            g_Chart["ReadMode"] = objChart.DeviceSetting.MeasurementSettings.ReadMode; //Strip, Point
		

            if (UVMode == "M1" || UVMode == "M2") {  // For IO-Pro2 m1,m2=DualReflectanceSpot if Point mode or DualReflectanceScan if in strip mode
		
                if (g_Chart["ReadMode"] == "Strip") {
                    g_Chart["DeviceSettings"]["MeasurementMode"] = "DualReflectanceScan";
                } 
                else { 
                    g_Chart["DeviceSettings"]["MeasurementMode"] = "DualReflectanceSpot";
				
                }
                g_Chart["DeviceSettings"]["ResultIndexKey"] = UVMode; //Only needs to be set in Dual Measurement Modes, Cannot be set for i1Pro
            }
			else {      //1 For IO-Pro2 m0=ReflectanceSpot, if only "Point" mode otherwise it will get default "ReflectanceScan" which is set with device itself
                if (g_Chart["ReadMode"] == "Point") {
		
				
                    g_Chart["DeviceSettings"]["MeasurementMode"] = "ReflectanceSpot";
                }
            }
            if (g_Chart["ReadMode"] == "Point") { //Assign MeasurementCount only if ReadMode is set to "Point" in all cases of IO-Pro2
                g_Chart["NumPoints"] = objChart.DeviceSetting.MeasurementSettings.MeasurementCount; //1,2,3,4,5
            
        } 
        g_Chart["Mode"] = objChart.DeviceSetting.MeasurementSettings.UvIncludedName;
			}


            else if (InstrumentTypeID == 7) {  //For eXact devices
			
                if (UVMode == "M1" || UVMode == "M2") {  //For eXact m1,m2=DualReflectanceScan
                    g_Chart["DeviceSettings"]["MeasurementMode"] = "DualReflectanceScan";
                    g_Chart["DeviceSettings"]["ResultIndexKey"] = UVMode; //Only needs to be set in Dual Measurement Modes, Cannot be set for i1Pro
                }
                else {                                   //For eXact m0=ReflectanceScan
                    g_Chart["DeviceSettings"]["MeasurementMode"] = "ReflectanceScan";
                    g_Chart["DeviceSettings"]["ResultIndexKey"] = "M0";
                }
            }
      
    }

    if (objChart.ChartSetting != null) {

        // Remember i1 charts are generally read vertical so if you look at
        // the printout sideways it looks like it would be 21x40 with the columns named

        if (InstrumentTypeID == 6) {  //For LFP Instrument Type
            g_Chart["ChartType"] = "AutoLocation";
            g_Chart["Mode"] = "Contactless";
            //patch around the setting of density for the LFP, here set to StatusT
            g_Chart["ReadCS"] = "CIELab,CIEXYZ,sRGB,DensityT,Spectral";
            g_Chart["NumPoints"] = 1; //1,2,3,4,5
        }
        else if (InstrumentTypeID == 7) { //For eXact Instrument Type
            //hard coded setting for density now, here set to StatusT
            g_Chart["ReadCS"] = "CIELab,CIEXYZ,sRGB,DensityT,DensityE,Spectral" 
        }
        else {
            g_Chart["ChartType"] = "Native";
            g_Chart["ReadCS"] = "CIELab,CIEXYZ,sRGB,Density,Spectral";
           // g_Chart["ReadCS"] = "CIELab,CIEXYZ,sRGB,Density";
        }
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

        
        g_Chart["RowsPerPage"] = objChart.ChartSetting["RowsPerPage"];
        g_Chart["Pages"] = objChart.ChartSetting["Pages"];
    }

    g_Chart["RefBase"] = new Array();
    for (i = 0; i < g_Chart["Size"]; i++) {
        var patch = new Object();
        patch["SampleName"] = objChart.ReferenceCharts[i].SampleName;
        patch["HTMLColor"] = objChart.ReferenceCharts[i].HtmlColor;
        //patch["CMYK_C"] = objChart.ReferenceCharts[i].CmykC;
        //patch["CMYK_M"] = objChart.ReferenceCharts[i].CmykM;
        //patch["CMYK_Y"] = objChart.ReferenceCharts[i].CmykY;
        //patch["CMYK_K"] = objChart.ReferenceCharts[i].CmykK;
        //patch["PatchTypeID"] = objChart.ReferenceCharts[i].PatchTypeId;
        g_Chart["RefBase"].push(patch);
    }

    //clear the Measured patch array
    if (MeasuredPatches.length > 0) MeasuredPatches.length = 0;
}