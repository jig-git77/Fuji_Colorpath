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

spotchart = new Object();
spotchart["Name"] = "Spot Chart";
spotchart["Size"] = 1;
spotchart["NumberOfRows"] = 1;
spotchart["NumberOfColumns"] = 1;
spotchart["DeviceSettings"] = new Object();
spotchart["DeviceSettings"]["ColorimetricIlluminant"] = "D50";        
spotchart["PatchSizeX"] = 11.0;
spotchart["PatchSizeY"] = 7.93;

// Fuji wants reference patches as a large single dimensionional array.  For the i1 I only take single strips.
// So I could either represent it here as their form of array and copy elements in, or I can represent this chart
// as the proper form for each strip and just assign that (which seems easier)
spotchart["ReferencePatch"] = new Array();
spotchart["RefBase"] = new Array();
var patch = new Object();
patch["SampleName"] = "A1";
patch["HTMLColor"] = "#1b3336"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 19.486826;
patch["TargetLAB"]["A"] = -9.074219;
patch["TargetLAB"]["B"] = -4.984375;
setpatch(spotchart["RefBase"], patch);
