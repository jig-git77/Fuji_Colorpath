//
// define the plugin concept of a chart for the iSis barcode workflow
// chart ECI2002CMYK
//
function setpatch(patcharray, patch) 
{
    var tpatch = new Object();
    tpatch["SampleName"] = patch["SampleName"];
    tpatch["HTMLColor"] = patch["HTMLColor"];
    tpatch["SampleID"] = patch["SampleID"]; 
    tpatch["TargetLAB"] = patch["TargetLAB"];	
    patcharray.push(tpatch);
}

eci2002 = new Object();
eci2002["Name"] = "IT8.7-4 R CMYK";
eci2002["ShortName"] = "IT8.7-4";
eci2002["PrintName"] = "IT8.7-4 CMYK";
eci2002["Size"] = 1617;
// Remember i1 charts are generally read vertical so if you look at
// the printout sideways it looks like it would be 21x40 with the columns named
eci2002["NumberOfRows"] = 49;
eci2002["NumberOfColumns"] = 33;
// iSis info for the isis version
eci2002["ChartType"] = "BarcodeChartinfo";
eci2002["Mode"] = "UVCut";
eci2002["ChartSizeX"] = 198.0;
eci2002["ChartSizeY"] = 293.0;
eci2002["BarSizeX"] = 198.0;
eci2002["BarSizeY"] = 6.0;
eci2002["DistToBar"] = 33.0;
eci2002["DistToFirstLineY"] = 20.0;
eci2002["MarkerDistance"] = 177.0;
eci2002["DistToFirstField"] = 10.5;
eci2002["PatchSizeX"] = 6.0;
eci2002["PatchSizeY"] = 6.0;

eci2002["DeviceSettings"] = new Object();
eci2002["DeviceSettings"]["Colorimetric.Illumination"] = "D50";        
eci2002["DeviceSettings"]["Colorimetric.Observer"] = "TwoDegree";        
eci2002["DeviceSettings"]["Colorimetric.WhiteBase"] = "Absolute";        
eci2002["DeviceSettings"]["Colorimetric.DensityStandard"] = "ANSIA";        
eci2002["DeviceSettings"]["Colorimetric.DensityFilterMode"] = "Auto";        
	
eci2002["RowNames"] = ["A1","B1","C1","D1","E1","F1","G1","H1","I1","J1","K1","L1","M1","N1","O1","P1","Q1","R1","S1","T1","U1","V1","W1","X1","Y1","Z1",
			  "A2","B2","C2","D2","E2","F2","G2","H2","I2","J2","K2","L2","M2","N2","O2","P2","Q2","R2","S2","T2","U2","V2","W2"];
// We have no refbase yet
//eci2002["RefBase"] = new Array();

//var patch = new Object();	
//patch["SampleName"] = "A1";
//patch["HTMLColor"] = "#005B79";
//patch["TargetLAB"] = new Object();
//patch["TargetLAB"]["L"] = 34.200741;
//patch["TargetLAB"]["A"] = -18.929525;
//patch["TargetLAB"]["B"] = -25.803614;
//setpatch(eci2002["RefBase"], patch);


//
// Quick sanity check
//
if (eci2002["Size"] != eci2002["NumberOfRows"] * eci2002["NumberOfColumns"]) {
    alert("Chart specification error in Size vs Number Of Columns * Number Of Rows");
}
if (eci2002["RowNames"].length != eci2002["NumberOfRows"]) {
    alert("Chart specification error in Row Names(" + eci2002["RowNames"].length + ") vs Number Of Rows(" + eci2002["NumberOfRows"] + ")");
}

// Make the defined chart into the generic name for xychartread.js
g_TheChart = eci2002;