//
// This is the IDeaaliance ISO 12647-7 chart for the I1/IO which comes with the IO
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

iso12647_7 = new Object();
iso12647_7["Name"] = "ISO 12647-7 Digital Control Strip 2009";
iso12647_7["ShortName"] = "ISO-12647-7";
iso12647_7["PrintName"] = "ISO-12647-7";
iso12647_7["Size"] = 54;
// Remember i1 charts are generally read vertical so if you look at
// the printout sideways it looks like it would be 21x40 with the columns named
iso12647_7["NumberOfRows"] = 2;
iso12647_7["NumberOfColumns"] = 27;
// Dimensions and offsets for IO table to calculate movement
iso12647_7["ChartSizeX"] = 240.0;
iso12647_7["ChartSizeY"] = 16.0;

iso12647_7["DistToFirstLineX"] = 24.0;
iso12647_7["DistToFirstLineY"] = 35.0;
iso12647_7["PatchSizeX"] = 9.0;
iso12647_7["PatchSizeY"] = 8.0;

iso12647_7["FirstSample"] = "TopLeft";
iso12647_7["ReadDirection"] = "LeftToRight";
iso12647_7["Mode"] = "Strip";

iso12647_7["DeviceSettings"] = new Object();
iso12647_7["DeviceSettings"]["Colorimetric.Illumination"] = "D50";        
iso12647_7["DeviceSettings"]["Colorimetric.Observer"] = "TwoDegree";        
iso12647_7["DeviceSettings"]["Colorimetric.WhiteBase"] = "Absolute";        
iso12647_7["DeviceSettings"]["Colorimetric.DensityStandard"] = "ANSIA";        
iso12647_7["DeviceSettings"]["Colorimetric.DensityFilterMode"] = "Auto";        
	
iso12647_7["RowNames"] = ["A","B"];
iso12647_7["RefBase"] = new Array();

var patch = new Object();	
patch["SampleName"] = "A1";
patch["HTMLColor"] = "#005B79";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 33.79;
patch["TargetLAB"]["A"] = -23.55;
patch["TargetLAB"]["B"] = -28.06;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A2";
patch["HTMLColor"] = "#009FD7";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 57.79;
patch["TargetLAB"]["A"] = -39.20;
patch["TargetLAB"]["B"] = -47.28;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A3";
patch["HTMLColor"] = "#43B6E1";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 68.60;
patch["TargetLAB"]["A"] = -25.63;
patch["TargetLAB"]["B"] = -35.33;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A4";
patch["HTMLColor"] = "#B2D9EC";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 84.47;
patch["TargetLAB"]["A"] = -10.15;
patch["TargetLAB"]["B"] = -16.45;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A5";
patch["HTMLColor"] = "#76294F";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 28.85;
patch["TargetLAB"]["A"] = 42.86;
patch["TargetLAB"]["B"] = -3.36;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A6";
patch["HTMLColor"] = "#D22083";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 49.27;
patch["TargetLAB"]["A"] = 73.36;
patch["TargetLAB"]["B"] = -5.75;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A7";
patch["HTMLColor"] = "#DD73A6";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 63.38;
patch["TargetLAB"]["A"] = 49.80;
patch["TargetLAB"]["B"] = -7.34;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A8";
patch["HTMLColor"] = "#E7C2D5";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 82.58;
patch["TargetLAB"]["A"] = 17.60;
patch["TargetLAB"]["B"] = -5.35;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A9";
patch["HTMLColor"] = "#817C1E";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 51.25;
patch["TargetLAB"]["A"] = -5.60;
patch["TargetLAB"]["B"] = 48.02;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A10";
patch["HTMLColor"] = "#F7E300";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 89.62;
patch["TargetLAB"]["A"] = -5.20;
patch["TargetLAB"]["B"] = 90.91;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A11";
patch["HTMLColor"] = "#F5E76D";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 91.30;
patch["TargetLAB"]["A"] = -4.93;
patch["TargetLAB"]["B"] = 57.83;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A12";
patch["HTMLColor"] = "#F4EFBE";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 93.70;
patch["TargetLAB"]["A"] = -2.90;
patch["TargetLAB"]["B"] = 22.30;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A13";
patch["HTMLColor"] = "#009DA6";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 54.92;
patch["TargetLAB"]["A"] = -51.36;
patch["TargetLAB"]["B"] = -21.55;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A14";
patch["HTMLColor"] = "#993488";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 40.33;
patch["TargetLAB"]["A"] = 51.38;
patch["TargetLAB"]["B"] = -23.37;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A15";
patch["HTMLColor"] = "#E7A415";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 72.82;
patch["TargetLAB"]["A"] = 20.42;
patch["TargetLAB"]["B"] = 71.07;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A16";
patch["HTMLColor"] = "#A07A4D";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 54.32;
patch["TargetLAB"]["A"] = 13.45;
patch["TargetLAB"]["B"] = 30.99;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A17";
patch["HTMLColor"] = "#9A565B";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 45.07;
patch["TargetLAB"]["A"] = 32.65;
patch["TargetLAB"]["B"] = 11.07;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A18";
patch["HTMLColor"] = "#715078";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 38.533165;
patch["TargetLAB"]["A"] = 22.867785;
patch["TargetLAB"]["B"] = -17.729872;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A19";
patch["HTMLColor"] = "#728E58";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 55.2;
patch["TargetLAB"]["A"] = -17.69;
patch["TargetLAB"]["B"] = 24.20;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A20";
patch["HTMLColor"] = "#46668B";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 40.84;
patch["TargetLAB"]["A"] = -3.25;
patch["TargetLAB"]["B"] = -25.66;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A21";
patch["HTMLColor"] = "#EDF1F0";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 93.94;
patch["TargetLAB"]["A"] = -0.48;
patch["TargetLAB"]["B"] = -1.65;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A22";
patch["HTMLColor"] = "#DFE4E2";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 89.30;
patch["TargetLAB"]["A"] = -0.87;
patch["TargetLAB"]["B"] = -1.22;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A23";
patch["HTMLColor"] = "#C2C7C7";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 79.40;
patch["TargetLAB"]["A"] = -0.77;
patch["TargetLAB"]["B"] = -1.81;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A24";
patch["HTMLColor"] = "#939899";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 61.93;
patch["TargetLAB"]["A"] = -0.88;
patch["TargetLAB"]["B"] = -1.96;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A25";
patch["HTMLColor"] = "#636768";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 42.60;
patch["TargetLAB"]["A"] = -0.69;
patch["TargetLAB"]["B"] = -1.88;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A26";
patch["HTMLColor"] = "#444646";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 28.05;
patch["TargetLAB"]["A"] = -0.27;
patch["TargetLAB"]["B"] = -0.78;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A27";
patch["HTMLColor"] = "#303230";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 18.56;
patch["TargetLAB"]["A"] = -0.47;
patch["TargetLAB"]["B"] = 0.21;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B1";
patch["HTMLColor"] = "#2D3154";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 18.43;
patch["TargetLAB"]["A"] = 7.45;
patch["TargetLAB"]["B"] = -23.57;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B2";
patch["HTMLColor"] = "#343F8C";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 27.20;
patch["TargetLAB"]["A"] = 15.76;
patch["TargetLAB"]["B"] = -46.01;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B3";
patch["HTMLColor"] = "#6965A7";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 44.42;
patch["TargetLAB"]["A"] = 14.73;
patch["TargetLAB"]["B"] = -35.66;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B4";
patch["HTMLColor"] = "#B0B0D2";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 72.34;
patch["TargetLAB"]["A"] = 5.97;
patch["TargetLAB"]["B"] = -17.98;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B5";
patch["HTMLColor"] = "#742C26";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 28.37;
patch["TargetLAB"]["A"] = 36.18;
patch["TargetLAB"]["B"] = 24.03;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B6";
patch["HTMLColor"] = "#D22B30";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 48.37;
patch["TargetLAB"]["A"] = 67.07;
patch["TargetLAB"]["B"] = 43.29;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B7";
patch["HTMLColor"] = "#DF7159";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 61.40;
patch["TargetLAB"]["A"] = 45.31;
patch["TargetLAB"]["B"] = 35.60;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B8";
patch["HTMLColor"] = "#EBBFAC";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 80.81;
patch["TargetLAB"]["A"] = 15.73;
patch["TargetLAB"]["B"] = 15.57;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B9";
patch["HTMLColor"] = "#005D37";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 33.00;
patch["TargetLAB"]["A"] = -36.46;
patch["TargetLAB"]["B"] = 11.99;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B10";
patch["HTMLColor"] = "#009B4D";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 53.49;
patch["TargetLAB"]["A"] = -62.27;
patch["TargetLAB"]["B"] = 23.82;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B11";
patch["HTMLColor"] = "#57B379";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 65.01;
patch["TargetLAB"]["A"] = -38.84;
patch["TargetLAB"]["B"] = 17.57;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B12";
patch["HTMLColor"] = "#B7D9C0";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 82.50;
patch["TargetLAB"]["A"] = -14.27;
patch["TargetLAB"]["B"] = 6.10;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B13";
patch["HTMLColor"] = "#007DBE";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 47.52;
patch["TargetLAB"]["A"] = -18.16;
patch["TargetLAB"]["B"] = -43.91;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B14";
patch["HTMLColor"] = "#D52667";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 49.59;
patch["TargetLAB"]["A"] = 68.10;
patch["TargetLAB"]["B"] = 11.77;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B15";
patch["HTMLColor"] = "#A5C530";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 74.19;
patch["TargetLAB"]["A"] = -24.27;
patch["TargetLAB"]["B"] = 61.67;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B16";
patch["HTMLColor"] = "#D5A896";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 72.79;
patch["TargetLAB"]["A"] = 16.86;
patch["TargetLAB"]["B"] = 15.09;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B17";
patch["HTMLColor"] = "#C06E5E";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 56.25;
patch["TargetLAB"]["A"] = 34.34;
patch["TargetLAB"]["B"] = 24.35;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B18";
patch["HTMLColor"] = "#9A5645";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 45.08;
patch["TargetLAB"]["A"] = 30.05;
patch["TargetLAB"]["B"] = 23.42;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B19";
patch["HTMLColor"] = "#3D837B";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 49.19;
patch["TargetLAB"]["A"] = -25.18;
patch["TargetLAB"]["B"] = -4.91;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B20";
patch["HTMLColor"] = "#F2F6F8";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 96.18;
patch["TargetLAB"]["A"] = -0.04;
patch["TargetLAB"]["B"] = -3.46;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B21";
patch["HTMLColor"] = "#ECF0F0";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 94.12;
patch["TargetLAB"]["A"] = -0.42;
patch["TargetLAB"]["B"] = -2.40;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B22";
patch["HTMLColor"] = "#DCE0E1";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 88.60;
patch["TargetLAB"]["A"] = -0.45;
patch["TargetLAB"]["B"] = -2.44;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B23";
patch["HTMLColor"] = "#BDC3C4";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 78.34;
patch["TargetLAB"]["A"] = -1.04;
patch["TargetLAB"]["B"] = -2.95;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B24";
patch["HTMLColor"] = "#8D9598";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 61.25;
patch["TargetLAB"]["A"] = -1.70;
patch["TargetLAB"]["B"] = -4.29;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B25";
patch["HTMLColor"] = "#62696A";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 43.53;
patch["TargetLAB"]["A"] = -1.78;
patch["TargetLAB"]["B"] = -3.09;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B26";
patch["HTMLColor"] = "#3E4040";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 25.81;
patch["TargetLAB"]["A"] = -0.36;
patch["TargetLAB"]["B"] = -1.01;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B27";
patch["HTMLColor"] = "#252624";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 12.75;
patch["TargetLAB"]["A"] = 0.20;
patch["TargetLAB"]["B"] = 0.18;
setpatch(iso12647_7["RefBase"], patch);

//
// Quick sanity check
//
if (iso12647_7["Size"] != iso12647_7["NumberOfRows"] * iso12647_7["NumberOfColumns"]) {
    alert("Chart specification error in Size vs Number Of Columns * Number Of Rows");
}
if (iso12647_7["RowNames"].length != iso12647_7["NumberOfRows"]) {
    alert("Chart specification error in Row Names vs Number Of Rows");
}

// Make the defined chart into the generic name for xychartread.js
g_TheChart = iso12647_7;