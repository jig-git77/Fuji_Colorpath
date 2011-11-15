//
// define the plugin concept of a chart for the random 331 samples chart
// This is a two page chart 
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
// iSis info for the isis version
iso12647_7["ChartType"] = "Native";
iso12647_7["Mode"] = "UV";
iso12647_7["ChartSizeX"] = 191.0;
iso12647_7["ChartSizeY"] = 70.0;
iso12647_7["BarSizeX"] = 162.0;
iso12647_7["BarSizeY"] = 6.0;
iso12647_7["DistToBar"] = 35.0;
iso12647_7["DistToFirstLineY"] = 12.0;
iso12647_7["MarkerDistance"] = 177.0;
iso12647_7["DistToFirstField"] = 10.5;
iso12647_7["PatchSizeX"] = 6.0;
iso12647_7["PatchSizeY"] = 6.0;

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
patch["TargetLAB"]["L"] = 34.200741;
patch["TargetLAB"]["A"] = -18.929525;
patch["TargetLAB"]["B"] = -25.803614;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A2";
patch["HTMLColor"] = "#009FD7";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 59.029877;
patch["TargetLAB"]["A"] = -31.174332;
patch["TargetLAB"]["B"] = -42.392433;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A3";
patch["HTMLColor"] = "#43B6E1";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 69.622032;
patch["TargetLAB"]["A"] = -20.753235;
patch["TargetLAB"]["B"] = -31.335903;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A4";
patch["HTMLColor"] = "#B2D9EC";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 84.884171;
patch["TargetLAB"]["A"] = -9.302110;
patch["TargetLAB"]["B"] = -13.376403;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A5";
patch["HTMLColor"] = "#76294F";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 28.979124;
patch["TargetLAB"]["A"] = 40.701256;
patch["TargetLAB"]["B"] = -4.744488;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A6";
patch["HTMLColor"] = "#D22083";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 47.947716;
patch["TargetLAB"]["A"] = 73.649734;
patch["TargetLAB"]["B"] = -8.821011;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A7";
patch["HTMLColor"] = "#DD73A6";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 62.707886;
patch["TargetLAB"]["A"] = 48.778145;
patch["TargetLAB"]["B"] = -7.876658;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A8";
patch["HTMLColor"] = "#E7C2D5";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 82.602501;
patch["TargetLAB"]["A"] = 16.663879;
patch["TargetLAB"]["B"] = -4.217052;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A9";
patch["HTMLColor"] = "#817C1E";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 51.071758;
patch["TargetLAB"]["A"] = -9.531707;
patch["TargetLAB"]["B"] = 49.700226;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A10";
patch["HTMLColor"] = "#F7E300";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 89.484619;
patch["TargetLAB"]["A"] = -11.666775;
patch["TargetLAB"]["B"] = 93.962112;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A11";
patch["HTMLColor"] = "#F5E76D";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 91.101265;
patch["TargetLAB"]["A"] = -10.316044;
patch["TargetLAB"]["B"] = 59.640324;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A12";
patch["HTMLColor"] = "#F4EFBE";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 94.094940;
patch["TargetLAB"]["A"] = -6.027073;
patch["TargetLAB"]["B"] = 23.834515;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A13";
patch["HTMLColor"] = "#009DA6";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 56.907818;
patch["TargetLAB"]["A"] = -48.161297;
patch["TargetLAB"]["B"] = -18.331791;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A14";
patch["HTMLColor"] = "#993488";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 39.745358;
patch["TargetLAB"]["A"] = 54.348515;
patch["TargetLAB"]["B"] = -25.655001;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A15";
patch["HTMLColor"] = "#E7A415";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 72.670868;
patch["TargetLAB"]["A"] = 14.860630;
patch["TargetLAB"]["B"] = 73.120949;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A16";
patch["HTMLColor"] = "#A07A4D";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 54.501732;
patch["TargetLAB"]["A"] = 9.855390;
patch["TargetLAB"]["B"] = 31.621880;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A17";
patch["HTMLColor"] = "#9A565B";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 44.756344;
patch["TargetLAB"]["A"] = 30.912251;
patch["TargetLAB"]["B"] = 10.308069;
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
patch["TargetLAB"]["L"] = 56.068398;
patch["TargetLAB"]["A"] = -21.090866;
patch["TargetLAB"]["B"] = 26.119370;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A20";
patch["HTMLColor"] = "#46668B";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 41.799938;
patch["TargetLAB"]["A"] = -1.325607;
patch["TargetLAB"]["B"] = -24.791359;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A21";
patch["HTMLColor"] = "#EDF1F0";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 95.002808;
patch["TargetLAB"]["A"] = -1.466900;
patch["TargetLAB"]["B"] = -0.209403;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A22";
patch["HTMLColor"] = "#DFE4E2";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 90.425812;
patch["TargetLAB"]["A"] = -1.961142;
patch["TargetLAB"]["B"] = 0.181007;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A23";
patch["HTMLColor"] = "#C2C7C7";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 80.453232;
patch["TargetLAB"]["A"] = -1.870185;
patch["TargetLAB"]["B"] = -0.392568;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A24";
patch["HTMLColor"] = "#939899";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 62.764515;
patch["TargetLAB"]["A"] = -1.522779;
patch["TargetLAB"]["B"] = -1.142883;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A25";
patch["HTMLColor"] = "#636768";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 42.951855;
patch["TargetLAB"]["A"] = -1.377463;
patch["TargetLAB"]["B"] = -1.209760;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A26";
patch["HTMLColor"] = "#444646";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 28.645411;
patch["TargetLAB"]["A"] = -1.219258;
patch["TargetLAB"]["B"] = -0.014931;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "A27";
patch["HTMLColor"] = "#303230";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 18.488626;
patch["TargetLAB"]["A"] = -1.493558;
patch["TargetLAB"]["B"] = 1.112545;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B1";
patch["HTMLColor"] = "#2D3154";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 19.620119;
patch["TargetLAB"]["A"] = 9.925172;
patch["TargetLAB"]["B"] = -23.843199;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B2";
patch["HTMLColor"] = "#343F8C";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 28.802267;
patch["TargetLAB"]["A"] = 21.819056;
patch["TargetLAB"]["B"] = -45.932716;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B3";
patch["HTMLColor"] = "#6965A7";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 45.766399;
patch["TargetLAB"]["A"] = 17.891020;
patch["TargetLAB"]["B"] = -35.171188;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B4";
patch["HTMLColor"] = "#B0B0D2";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 73.364174;
patch["TargetLAB"]["A"] = 6.569803;
patch["TargetLAB"]["B"] = -16.637539;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B5";
patch["HTMLColor"] = "#742C26";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 27.892242;
patch["TargetLAB"]["A"] = 34.158112;
patch["TargetLAB"]["B"] = 22.507513;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B6";
patch["HTMLColor"] = "#D22B30";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 47.060318;
patch["TargetLAB"]["A"] = 65.896423;
patch["TargetLAB"]["B"] = 42.739998;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B7";
patch["HTMLColor"] = "#DF7159";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 60.650864;
patch["TargetLAB"]["A"] = 42.464108;
patch["TargetLAB"]["B"] = 34.411846;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B8";
patch["HTMLColor"] = "#EBBFAC";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 81.310562;
patch["TargetLAB"]["A"] = 13.088882;
patch["TargetLAB"]["B"] = 15.608859;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B9";
patch["HTMLColor"] = "#005D37";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 33.143826;
patch["TargetLAB"]["A"] = -41.641953;
patch["TargetLAB"]["B"] = 15.259147;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B10";
patch["HTMLColor"] = "#009B4D";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 54.507988;
patch["TargetLAB"]["A"] = -71.072487;
patch["TargetLAB"]["B"] = 28.944380;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B11";
patch["HTMLColor"] = "#57B379";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 66.357887;
patch["TargetLAB"]["A"] = -43.150185;
patch["TargetLAB"]["B"] = 20.692694;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B12";
patch["HTMLColor"] = "#B7D9C0";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 83.886909;
patch["TargetLAB"]["A"] = -16.342909;
patch["TargetLAB"]["B"] = 8.151484;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B13";
patch["HTMLColor"] = "#007DBE";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 48.807640;
patch["TargetLAB"]["A"] = -12.075901;
patch["TargetLAB"]["B"] = -44.054604;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B14";
patch["HTMLColor"] = "#D52667";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 48.262226;
patch["TargetLAB"]["A"] = 70.809929;
patch["TargetLAB"]["B"] = 9.155750;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B15";
patch["HTMLColor"] = "#A5C530";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 75.145813;
patch["TargetLAB"]["A"] = -30.611307;
patch["TargetLAB"]["B"] = 65.571121;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B16";
patch["HTMLColor"] = "#D5A896";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 73.173996;
patch["TargetLAB"]["A"] = 14.272183;
patch["TargetLAB"]["B"] = 15.693367;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B17";
patch["HTMLColor"] = "#C06E5E";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 55.879013;
patch["TargetLAB"]["A"] = 32.145233;
patch["TargetLAB"]["B"] = 24.389130;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B18";
patch["HTMLColor"] = "#9A5645";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 44.265804;
patch["TargetLAB"]["A"] = 28.405369;
patch["TargetLAB"]["B"] = 23.420536;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B19";
patch["HTMLColor"] = "#3D837B";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 50.114918;
patch["TargetLAB"]["A"] = -26.154936;
patch["TargetLAB"]["B"] = -3.155291;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B20";
patch["HTMLColor"] = "#F2F6F8";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 96.906631;
patch["TargetLAB"]["A"] = -1.019686;
patch["TargetLAB"]["B"] = -1.244628;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B21";
patch["HTMLColor"] = "#ECF0F0";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 94.711845;
patch["TargetLAB"]["A"] = -1.363009;
patch["TargetLAB"]["B"] = -0.572717;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B22";
patch["HTMLColor"] = "#DCE0E1";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 89.282364;
patch["TargetLAB"]["A"] = -1.301795;
patch["TargetLAB"]["B"] = -0.734293;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B23";
patch["HTMLColor"] = "#BDC3C4";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 78.720108;
patch["TargetLAB"]["A"] = -2.072483;
patch["TargetLAB"]["B"] = -1.206732;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B24";
patch["HTMLColor"] = "#8D9598";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 61.590382;
patch["TargetLAB"]["A"] = -2.649635;
patch["TargetLAB"]["B"] = -2.727973;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B25";
patch["HTMLColor"] = "#62696A";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 43.591450;
patch["TargetLAB"]["A"] = -2.675891;
patch["TargetLAB"]["B"] = -1.934731;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B26";
patch["HTMLColor"] = "#3E4040";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 25.528811;
patch["TargetLAB"]["A"] = -0.881240;
patch["TargetLAB"]["B"] = -0.368917;
setpatch(iso12647_7["RefBase"], patch);

patch["SampleName"] = "B27";
patch["HTMLColor"] = "#252624";
patch["TargetLAB"] = new Object();
patch["TargetLAB"]["L"] = 12.239089;
patch["TargetLAB"]["A"] = -0.924371;
patch["TargetLAB"]["B"] = 1.098138;
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