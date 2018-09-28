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

random331samples = new Object();
random331samples["Name"] = "Random 331";
random331samples["Size"] = 441;
// Remember i1 charts are generally read vertical so if you look at
// the printout sideways it looks like it would be 21x40 with the columns named
random331samples["NumberOfRows"] = 21;
random331samples["NumberOfColumns"] = 21;
random331samples["DeviceSettings"] = new Object();
random331samples["DeviceSettings"]["Colorimetric.Illumination"] = "D50";        
random331samples["RowNames"] = ["A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U"];
// This is used if the same chart is read on an IO table reader
// The sizes and distances are all in MM and are from the upper left corner of the sheet
// which should be aligned with the sheet marks on the platten of the iO.
random331samples["ChartSizeX"] = 228.6;		// Width of sample area
random331samples["ChartSizeY"] = 31.75;		// Height of sample area
// Distances are used as mm from the upper left corner of chart grid box.  The addressable area
// is over this, but making it the pre-drawn square makes creating charts so much easier.
random331samples["DistToFirstLineX"] = 11.1;    // left edge, we add 1/2 this for centering 
random331samples["DistToFirstLineY"] = 3.0;     // top edge
random331samples["PatchSizeX"] = 11.0;
random331samples["PatchSizeY"] = 7.93;

// Define the first sample and read direction and if the samples are strip or point readable.
// Note, point read samples are slow! 
random331samples["FirstSample"] = "TopRight";      // TopRight, TopLeft, BottomLeft, BottomRight
random331samples["ReadDirection"] = "RightToLeft"; // RightToLeft, LeftToRight, TopToBottom, BottomToTop
random331samples["Mode"] = "Strip";                // Strip or Point, point read will be slow but for charts 
                                                   // without a contrast trough, the auto detect won't work. 
                                                   // This mode would allow iSis charts to be used.
// optional device specific options, straight from the isis sdk:
// If these exist in the sample chart, they are copied to the scanner which is where
// we really look for settings.
random331samples["DeviceSettings"] = new Object();
random331samples["DeviceSettings"]["Colorimetric.Illumination"] = "D50";

// Fuji wants reference patches as a large single dimensionional array.  For the i1 I only take single strips.
// So I could either represent it here as their form of array and copy elements in, or I can represent this chart
// as the proper form for each strip and just assign that (which seems easier)
random331samples["ReferencePatch"] = new Array();
random331samples["RefBase"] = new Array();
var patch = new Object();
patch["SampleName"] = "A1";
patch["HTMLColor"] = "#1b3336"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 19.486826;
patch["TargetLAB"]["A"] = -9.074219;
patch["TargetLAB"]["B"] = -4.984375;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "A2";
patch["HTMLColor"] = "#362226"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 15.811887;
patch["TargetLAB"]["A"] = 10.488281;
patch["TargetLAB"]["B"] = 1.113281;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "A3";
patch["HTMLColor"] = "#ba7cb5"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 60.133272;
patch["TargetLAB"]["A"] = 31.378906;
patch["TargetLAB"]["B"] = -19.792969;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "A4";
patch["HTMLColor"] = "#16363b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 20.381434;
patch["TargetLAB"]["A"] = -10.914062;
patch["TargetLAB"]["B"] = -6.730469;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "A5";
patch["HTMLColor"] = "#51696e"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 42.512255;
patch["TargetLAB"]["A"] = -7.843750;
patch["TargetLAB"]["B"] = -6.324219;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "A6";
patch["HTMLColor"] = "#26542e"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 31.815257;
patch["TargetLAB"]["A"] = -23.789062;
patch["TargetLAB"]["B"] = 17.449219;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "A7";
patch["HTMLColor"] = "#414251"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 28.213848;
patch["TargetLAB"]["A"] = 2.476562;
patch["TargetLAB"]["B"] = -9.324219;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "A8";
patch["HTMLColor"] = "#8b2829"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 32.621017;
patch["TargetLAB"]["A"] = 42.515625;
patch["TargetLAB"]["B"] = 24.839844;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "A9";
patch["HTMLColor"] = "#70738c"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 48.884804;
patch["TargetLAB"]["A"] = 2.804688;
patch["TargetLAB"]["B"] = -13.886719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "A10";
patch["HTMLColor"] = "#2b393c"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 22.855392;
patch["TargetLAB"]["A"] = -5.324219;
patch["TargetLAB"]["B"] = -3.964844;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "A11";
patch["HTMLColor"] = "#222425"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 14.097733;
patch["TargetLAB"]["A"] = -0.921875;
patch["TargetLAB"]["B"] = -0.746094;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "A12";
patch["HTMLColor"] = "#b1ddcb"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 84.662990;
patch["TargetLAB"]["A"] = -17.718750;
patch["TargetLAB"]["B"] = 4.101562;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "A13";
patch["HTMLColor"] = "#723c28"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 32.147672;
patch["TargetLAB"]["A"] = 22.648438;
patch["TargetLAB"]["B"] = 23.285156;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "A14";
patch["HTMLColor"] = "#3d2d30"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 20.545343;
patch["TargetLAB"]["A"] = 7.570312;
patch["TargetLAB"]["B"] = 1.007812;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "A15";
patch["HTMLColor"] = "#c37f89"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 60.674020;
patch["TargetLAB"]["A"] = 27.710938;
patch["TargetLAB"]["B"] = 6.078125;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "A16";
patch["HTMLColor"] = "#7e5a6b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 42.426471;
patch["TargetLAB"]["A"] = 17.363281;
patch["TargetLAB"]["B"] = -3.683594;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "A17";
patch["HTMLColor"] = "#933c3b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 37.809436;
patch["TargetLAB"]["A"] = 37.398438;
patch["TargetLAB"]["B"] = 20.335938;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "A18";
patch["HTMLColor"] = "#eb314d"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 53.103554;
patch["TargetLAB"]["A"] = 70.273438;
patch["TargetLAB"]["B"] = 32.394531;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "A19";
patch["HTMLColor"] = "#192025"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 11.897978;
patch["TargetLAB"]["A"] = -1.929688;
patch["TargetLAB"]["B"] = -4.582031;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "A20";
patch["HTMLColor"] = "#519b90"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 59.036458;
patch["TargetLAB"]["A"] = -26.324219;
patch["TargetLAB"]["B"] = -1.421875;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "A21";
patch["HTMLColor"] = "#706c2f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 45.055147;
patch["TargetLAB"]["A"] = -4.859375;
patch["TargetLAB"]["B"] = 34.054688;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "B1";
patch["HTMLColor"] = "#6b3038"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 28.556985;
patch["TargetLAB"]["A"] = 27.570312;
patch["TargetLAB"]["B"] = 8.160156;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "B2";
patch["HTMLColor"] = "#735e69"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 42.141544;
patch["TargetLAB"]["A"] = 10.320312;
patch["TargetLAB"]["B"] = -2.917969;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "B3";
patch["HTMLColor"] = "#e3c491"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 80.912990;
patch["TargetLAB"]["A"] = 6.433594;
patch["TargetLAB"]["B"] = 29.988281;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "B4";
patch["HTMLColor"] = "#2c2a32"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 17.521446;
patch["TargetLAB"]["A"] = 2.664062;
patch["TargetLAB"]["B"] = -4.937500;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "B5";
patch["HTMLColor"] = "#2b5c7a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 36.556373;
patch["TargetLAB"]["A"] = -9.339844;
patch["TargetLAB"]["B"] = -22.386719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "B6";
patch["HTMLColor"] = "#3a6d61"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 42.104779;
patch["TargetLAB"]["A"] = -20.031250;
patch["TargetLAB"]["B"] = 1.183594;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "B7";
patch["HTMLColor"] = "#221e28"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 12.127757;
patch["TargetLAB"]["A"] = 3.687500;
patch["TargetLAB"]["B"] = -5.769531;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "B8";
patch["HTMLColor"] = "#fff89f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 97.113971;
patch["TargetLAB"]["A"] = -4.671875;
patch["TargetLAB"]["B"] = 44.250000;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "B9";
patch["HTMLColor"] = "#505647"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 35.764400;
patch["TargetLAB"]["A"] = -4.828125;
patch["TargetLAB"]["B"] = 7.820312;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "B10";
patch["HTMLColor"] = "#919f50"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 63.005515;
patch["TargetLAB"]["A"] = -14.085938;
patch["TargetLAB"]["B"] = 38.777344;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "B11";
patch["HTMLColor"] = "#60364c"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 28.737745;
patch["TargetLAB"]["A"] = 21.468750;
patch["TargetLAB"]["B"] = -5.117188;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "B12";
patch["HTMLColor"] = "#385253"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 32.858456;
patch["TargetLAB"]["A"] = -9.949219;
patch["TargetLAB"]["B"] = -3.761719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "B13";
patch["HTMLColor"] = "#f6a4a7"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 75.765931;
patch["TargetLAB"]["A"] = 31.636719;
patch["TargetLAB"]["B"] = 11.847656;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "B14";
patch["HTMLColor"] = "#331a1b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 12.927390;
patch["TargetLAB"]["A"] = 12.488281;
patch["TargetLAB"]["B"] = 5.113281;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "B15";
patch["HTMLColor"] = "#a4b45f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 70.562194;
patch["TargetLAB"]["A"] = -15.550781;
patch["TargetLAB"]["B"] = 40.957031;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "B16";
patch["HTMLColor"] = "#9b432e"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 40.312500;
patch["TargetLAB"]["A"] = 36.359375;
patch["TargetLAB"]["B"] = 31.339844;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "B17";
patch["HTMLColor"] = "#57482b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 31.660539;
patch["TargetLAB"]["A"] = 3.093750;
patch["TargetLAB"]["B"] = 20.136719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "B18";
patch["HTMLColor"] = "#5c6ab0"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 46.196385;
patch["TargetLAB"]["A"] = 9.152344;
patch["TargetLAB"]["B"] = -39.273438;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "B19";
patch["HTMLColor"] = "#7a6e81"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 48.065257;
patch["TargetLAB"]["A"] = 7.656250;
patch["TargetLAB"]["B"] = -8.937500;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "B20";
patch["HTMLColor"] = "#94a938"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 66.027880;
patch["TargetLAB"]["A"] = -19.152344;
patch["TargetLAB"]["B"] = 53.191406;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "B21";
patch["HTMLColor"] = "#1b4d47"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 29.309130;
patch["TargetLAB"]["A"] = -18.902344;
patch["TargetLAB"]["B"] = -1.449219;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "C1";
patch["HTMLColor"] = "#303122"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 19.707414;
patch["TargetLAB"]["A"] = -2.589844;
patch["TargetLAB"]["B"] = 9.023438;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "C2";
patch["HTMLColor"] = "#ffffff"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 100.000000;
patch["TargetLAB"]["A"] = 0.000000;
patch["TargetLAB"]["B"] = 0.000000;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "C3";
patch["HTMLColor"] = "#342b39"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 18.875613;
patch["TargetLAB"]["A"] = 7.031250;
patch["TargetLAB"]["B"] = -7.531250;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "C4";
patch["HTMLColor"] = "#c7c9cc"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 80.819547;
patch["TargetLAB"]["A"] = -0.531250;
patch["TargetLAB"]["B"] = -1.589844;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "C5";
patch["HTMLColor"] = "#33322d"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 20.839461;
patch["TargetLAB"]["A"] = -0.738281;
patch["TargetLAB"]["B"] = 3.707031;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "C6";
patch["HTMLColor"] = "#6b859d"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 54.229473;
patch["TargetLAB"]["A"] = -5.300781;
patch["TargetLAB"]["B"] = -15.898438;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "C7";
patch["HTMLColor"] = "#4d4985"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 33.593750;
patch["TargetLAB"]["A"] = 14.464844;
patch["TargetLAB"]["B"] = -33.613281;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "C8";
patch["HTMLColor"] = "#453243"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 23.530944;
patch["TargetLAB"]["A"] = 11.074219;
patch["TargetLAB"]["B"] = -7.046875;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "C9";
patch["HTMLColor"] = "#3d4329"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 27.144608;
patch["TargetLAB"]["A"] = -6.050781;
patch["TargetLAB"]["B"] = 15.058594;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "C10";
patch["HTMLColor"] = "#272930"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 16.446078;
patch["TargetLAB"]["A"] = 0.828125;
patch["TargetLAB"]["B"] = -5.171875;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "C11";
patch["HTMLColor"] = "#1a2927"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 15.219056;
patch["TargetLAB"]["A"] = -6.925781;
patch["TargetLAB"]["B"] = -0.531250;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "C12";
patch["HTMLColor"] = "#ceaa79"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 72.159926;
patch["TargetLAB"]["A"] = 8.597656;
patch["TargetLAB"]["B"] = 30.828125;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "C13";
patch["HTMLColor"] = "#34779c"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 47.081801;
patch["TargetLAB"]["A"] = -12.792969;
patch["TargetLAB"]["B"] = -26.355469;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "C14";
patch["HTMLColor"] = "#259665"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 55.197610;
patch["TargetLAB"]["A"] = -40.855469;
patch["TargetLAB"]["B"] = 16.632812;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "C15";
patch["HTMLColor"] = "#5a513a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 34.970895;
patch["TargetLAB"]["A"] = 0.855469;
patch["TargetLAB"]["B"] = 15.015625;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "C16";
patch["HTMLColor"] = "#61332a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 27.084865;
patch["TargetLAB"]["A"] = 20.570312;
patch["TargetLAB"]["B"] = 15.359375;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "C17";
patch["HTMLColor"] = "#8e63a8"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 48.658088;
patch["TargetLAB"]["A"] = 27.929688;
patch["TargetLAB"]["B"] = -30.406250;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "C18";
patch["HTMLColor"] = "#342c28"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 18.754596;
patch["TargetLAB"]["A"] = 3.660156;
patch["TargetLAB"]["B"] = 3.738281;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "C19";
patch["HTMLColor"] = "#718c67"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 55.277267;
patch["TargetLAB"]["A"] = -15.421875;
patch["TargetLAB"]["B"] = 16.425781;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "C20";
patch["HTMLColor"] = "#182f2f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 17.386642;
patch["TargetLAB"]["A"] = -9.082031;
patch["TargetLAB"]["B"] = -3.328125;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "C21";
patch["HTMLColor"] = "#703d7c"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 34.376532;
patch["TargetLAB"]["A"] = 30.464844;
patch["TargetLAB"]["B"] = -26.351562;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "D1";
patch["HTMLColor"] = "#12454e"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 26.447610;
patch["TargetLAB"]["A"] = -14.542969;
patch["TargetLAB"]["B"] = -10.675781;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "D2";
patch["HTMLColor"] = "#e77b72"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 64.100797;
patch["TargetLAB"]["A"] = 41.781250;
patch["TargetLAB"]["B"] = 24.871094;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "D3";
patch["HTMLColor"] = "#944546"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 40.000000;
patch["TargetLAB"]["A"] = 34.300781;
patch["TargetLAB"]["B"] = 16.050781;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "D4";
patch["HTMLColor"] = "#633c50"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 30.600490;
patch["TargetLAB"]["A"] = 20.148438;
patch["TargetLAB"]["B"] = -4.593750;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "D5";
patch["HTMLColor"] = "#f49dc5"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 74.612439;
patch["TargetLAB"]["A"] = 36.972656;
patch["TargetLAB"]["B"] = -6.285156;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "D6";
patch["HTMLColor"] = "#704164"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 34.283088;
patch["TargetLAB"]["A"] = 25.117188;
patch["TargetLAB"]["B"] = -11.933594;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "D7";
patch["HTMLColor"] = "#773b59"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 33.607537;
patch["TargetLAB"]["A"] = 29.839844;
patch["TargetLAB"]["B"] = -5.523438;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "D8";
patch["HTMLColor"] = "#341a1d"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 13.017770;
patch["TargetLAB"]["A"] = 13.660156;
patch["TargetLAB"]["B"] = 3.542969;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "D9";
patch["HTMLColor"] = "#49705c"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 43.904718;
patch["TargetLAB"]["A"] = -17.851562;
patch["TargetLAB"]["B"] = 6.660156;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "D10";
patch["HTMLColor"] = "#8d8d8d"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 58.688725;
patch["TargetLAB"]["A"] = -0.128906;
patch["TargetLAB"]["B"] = 0.355469;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "D11";
patch["HTMLColor"] = "#31203a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 15.502451;
patch["TargetLAB"]["A"] = 12.683594;
patch["TargetLAB"]["B"] = -13.324219;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "D12";
patch["HTMLColor"] = "#0c3939"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 21.107537;
patch["TargetLAB"]["A"] = -15.292969;
patch["TargetLAB"]["B"] = -4.371094;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "D13";
patch["HTMLColor"] = "#793050"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 31.464461;
patch["TargetLAB"]["A"] = 34.945312;
patch["TargetLAB"]["B"] = -3.132812;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "D14";
patch["HTMLColor"] = "#939d8f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 63.700980;
patch["TargetLAB"]["A"] = -5.972656;
patch["TargetLAB"]["B"] = 6.414062;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "D15";
patch["HTMLColor"] = "#204834"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 27.152267;
patch["TargetLAB"]["A"] = -18.804688;
patch["TargetLAB"]["B"] = 7.277344;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "D16";
patch["HTMLColor"] = "#03705b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 41.576287;
patch["TargetLAB"]["A"] = -33.644531;
patch["TargetLAB"]["B"] = 3.847656;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "D17";
patch["HTMLColor"] = "#604f3b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 34.929534;
patch["TargetLAB"]["A"] = 5.214844;
patch["TargetLAB"]["B"] = 14.929688;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "D18";
patch["HTMLColor"] = "#ec2b77"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 53.540135;
patch["TargetLAB"]["A"] = 73.785156;
patch["TargetLAB"]["B"] = 7.480469;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "D19";
patch["HTMLColor"] = "#1e1727"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 9.413297;
patch["TargetLAB"]["A"] = 6.597656;
patch["TargetLAB"]["B"] = -9.601562;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "D20";
patch["HTMLColor"] = "#3a4341"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 27.385110;
patch["TargetLAB"]["A"] = -4.011719;
patch["TargetLAB"]["B"] = -0.433594;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "D21";
patch["HTMLColor"] = "#88cda1"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 76.850490;
patch["TargetLAB"]["A"] = -29.343750;
patch["TargetLAB"]["B"] = 14.773438;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "E1";
patch["HTMLColor"] = "#2f1d22"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 13.402267;
patch["TargetLAB"]["A"] = 9.945312;
patch["TargetLAB"]["B"] = 0.355469;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "E2";
patch["HTMLColor"] = "#23394b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 22.896752;
patch["TargetLAB"]["A"] = -4.449219;
patch["TargetLAB"]["B"] = -13.832031;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "E3";
patch["HTMLColor"] = "#396190"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 39.862132;
patch["TargetLAB"]["A"] = -2.910156;
patch["TargetLAB"]["B"] = -30.539062;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "E4";
patch["HTMLColor"] = "#006c91"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 40.324755;
patch["TargetLAB"]["A"] = -27.371094;
patch["TargetLAB"]["B"] = -31.046875;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "E5";
patch["HTMLColor"] = "#451f31"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 17.933517;
patch["TargetLAB"]["A"] = 20.753906;
patch["TargetLAB"]["B"] = -3.144531;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "E6";
patch["HTMLColor"] = "#292021"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 13.255208;
patch["TargetLAB"]["A"] = 4.882812;
patch["TargetLAB"]["B"] = 0.554688;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "E7";
patch["HTMLColor"] = "#29433a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 25.920650;
patch["TargetLAB"]["A"] = -11.898438;
patch["TargetLAB"]["B"] = 2.265625;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "E8";
patch["HTMLColor"] = "#402f37"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 21.662071;
patch["TargetLAB"]["A"] = 9.117188;
patch["TargetLAB"]["B"] = -1.839844;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "E9";
patch["HTMLColor"] = "#221f31"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 12.778799;
patch["TargetLAB"]["A"] = 5.035156;
patch["TargetLAB"]["B"] = -11.136719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "E10";
patch["HTMLColor"] = "#6583a8"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 53.426777;
patch["TargetLAB"]["A"] = -3.273438;
patch["TargetLAB"]["B"] = -23.500000;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "E11";
patch["HTMLColor"] = "#332a21"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 17.922794;
patch["TargetLAB"]["A"] = 3.023438;
patch["TargetLAB"]["B"] = 7.859375;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "E12";
patch["HTMLColor"] = "#462829"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 19.932598;
patch["TargetLAB"]["A"] = 14.667969;
patch["TargetLAB"]["B"] = 5.445312;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "E13";
patch["HTMLColor"] = "#738650"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 53.363971;
patch["TargetLAB"]["A"] = -14.144531;
patch["TargetLAB"]["B"] = 26.871094;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "E14";
patch["HTMLColor"] = "#a8756d"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 54.419424;
patch["TargetLAB"]["A"] = 19.648438;
patch["TargetLAB"]["B"] = 13.031250;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "E15";
patch["HTMLColor"] = "#9e416e"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 41.836703;
patch["TargetLAB"]["A"] = 43.089844;
patch["TargetLAB"]["B"] = -6.062500;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "E16";
patch["HTMLColor"] = "#8a5e3a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 43.976716;
patch["TargetLAB"]["A"] = 15.562500;
patch["TargetLAB"]["B"] = 27.605469;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "E17";
patch["HTMLColor"] = "#00afee"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 63.249081;
patch["TargetLAB"]["A"] = -40.917969;
patch["TargetLAB"]["B"] = -48.230469;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "E18";
patch["HTMLColor"] = "#484745"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 30.196078;
patch["TargetLAB"]["A"] = 0.136719;
patch["TargetLAB"]["B"] = 1.230469;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "E19";
patch["HTMLColor"] = "#614f3f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 34.998468;
patch["TargetLAB"]["A"] = 5.851562;
patch["TargetLAB"]["B"] = 12.550781;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "E20";
patch["HTMLColor"] = "#2e8140"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 47.990196;
patch["TargetLAB"]["A"] = -37.246094;
patch["TargetLAB"]["B"] = 27.320312;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "E21";
patch["HTMLColor"] = "#418848"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 51.011029;
patch["TargetLAB"]["A"] = -34.101562;
patch["TargetLAB"]["B"] = 27.289062;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "F1";
patch["HTMLColor"] = "#22816f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 48.351716;
patch["TargetLAB"]["A"] = -31.339844;
patch["TargetLAB"]["B"] = 1.398438;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "F2";
patch["HTMLColor"] = "#518769"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 51.767770;
patch["TargetLAB"]["A"] = -23.753906;
patch["TargetLAB"]["B"] = 10.101562;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "F3";
patch["HTMLColor"] = "#f06fab"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 64.370404;
patch["TargetLAB"]["A"] = 54.800781;
patch["TargetLAB"]["B"] = -6.882812;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "F4";
patch["HTMLColor"] = "#5f806e"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 50.652574;
patch["TargetLAB"]["A"] = -14.890625;
patch["TargetLAB"]["B"] = 5.644531;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "F5";
patch["HTMLColor"] = "#191919"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 8.849571;
patch["TargetLAB"]["A"] = -0.097656;
patch["TargetLAB"]["B"] = -0.285156;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "F6";
patch["HTMLColor"] = "#ca9e3f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 67.919730;
patch["TargetLAB"]["A"] = 9.722656;
patch["TargetLAB"]["B"] = 54.121094;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "F7";
patch["HTMLColor"] = "#223029"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 18.483456;
patch["TargetLAB"]["A"] = -7.437500;
patch["TargetLAB"]["B"] = 2.820312;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "F8";
patch["HTMLColor"] = "#1d3b35"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 22.477022;
patch["TargetLAB"]["A"] = -13.296875;
patch["TargetLAB"]["B"] = 0.386719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "F9";
patch["HTMLColor"] = "#5b363d"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 27.322304;
patch["TargetLAB"]["A"] = 17.843750;
patch["TargetLAB"]["B"] = 3.132812;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "F10";
patch["HTMLColor"] = "#4a7d8c"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 49.159007;
patch["TargetLAB"]["A"] = -14.445312;
patch["TargetLAB"]["B"] = -14.070312;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "F11";
patch["HTMLColor"] = "#6f8576"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 53.382353;
patch["TargetLAB"]["A"] = -10.445312;
patch["TargetLAB"]["B"] = 5.527344;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "F12";
patch["HTMLColor"] = "#6c4848"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 34.609375;
patch["TargetLAB"]["A"] = 16.019531;
patch["TargetLAB"]["B"] = 6.253906;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "F13";
patch["HTMLColor"] = "#ac5591"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 48.806679;
patch["TargetLAB"]["A"] = 41.804688;
patch["TargetLAB"]["B"] = -16.054688;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "F14";
patch["HTMLColor"] = "#00a0b8"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 59.396446;
patch["TargetLAB"]["A"] = -34.289062;
patch["TargetLAB"]["B"] = -24.000000;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "F15";
patch["HTMLColor"] = "#8e4636"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 39.085478;
patch["TargetLAB"]["A"] = 30.093750;
patch["TargetLAB"]["B"] = 24.695312;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "F16";
patch["HTMLColor"] = "#291824"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 11.141238;
patch["TargetLAB"]["A"] = 10.476562;
patch["TargetLAB"]["B"] = -4.261719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "F17";
patch["HTMLColor"] = "#75713d"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 47.112439;
patch["TargetLAB"]["A"] = -4.574219;
patch["TargetLAB"]["B"] = 29.371094;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "F18";
patch["HTMLColor"] = "#ca4d55"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 50.732230;
patch["TargetLAB"]["A"] = 50.660156;
patch["TargetLAB"]["B"] = 22.757812;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "F19";
patch["HTMLColor"] = "#0e262b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 13.287377;
patch["TargetLAB"]["A"] = -7.902344;
patch["TargetLAB"]["B"] = -6.910156;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "F20";
patch["HTMLColor"] = "#583753"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 27.890625;
patch["TargetLAB"]["A"] = 19.085938;
patch["TargetLAB"]["B"] = -11.097656;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "F21";
patch["HTMLColor"] = "#4d1e28"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 18.857230;
patch["TargetLAB"]["A"] = 23.105469;
patch["TargetLAB"]["B"] = 4.695312;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "G1";
patch["HTMLColor"] = "#362d31"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 19.491422;
patch["TargetLAB"]["A"] = 5.292969;
patch["TargetLAB"]["B"] = -1.179688;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "G2";
patch["HTMLColor"] = "#12646c"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 38.265931;
patch["TargetLAB"]["A"] = -21.996094;
patch["TargetLAB"]["B"] = -11.203125;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "G3";
patch["HTMLColor"] = "#fdceae"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 86.332721;
patch["TargetLAB"]["A"] = 13.671875;
patch["TargetLAB"]["B"] = 22.578125;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "G4";
patch["HTMLColor"] = "#a8a14c"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 65.405944;
patch["TargetLAB"]["A"] = -5.550781;
patch["TargetLAB"]["B"] = 44.128906;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "G5";
patch["HTMLColor"] = "#214f3a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 29.992341;
patch["TargetLAB"]["A"] = -20.812500;
patch["TargetLAB"]["B"] = 7.503906;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "G6";
patch["HTMLColor"] = "#1c564c"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 32.758885;
patch["TargetLAB"]["A"] = -21.523438;
patch["TargetLAB"]["B"] = 0.140625;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "G7";
patch["HTMLColor"] = "#32a2bb"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 61.387868;
patch["TargetLAB"]["A"] = -25.121094;
patch["TargetLAB"]["B"] = -22.664062;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "G8";
patch["HTMLColor"] = "#a05648"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 45.430453;
patch["TargetLAB"]["A"] = 30.339844;
patch["TargetLAB"]["B"] = 22.515625;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "G9";
patch["HTMLColor"] = "#365c4b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 35.834865;
patch["TargetLAB"]["A"] = -17.445312;
patch["TargetLAB"]["B"] = 5.453125;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "G10";
patch["HTMLColor"] = "#31252a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 16.436887;
patch["TargetLAB"]["A"] = 6.585938;
patch["TargetLAB"]["B"] = -0.820312;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "G11";
patch["HTMLColor"] = "#006d46"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 40.049020;
patch["TargetLAB"]["A"] = -37.769531;
patch["TargetLAB"]["B"] = 13.996094;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "G12";
patch["HTMLColor"] = "#ffffff"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 100.000000;
patch["TargetLAB"]["A"] = 0.000000;
patch["TargetLAB"]["B"] = 0.000000;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "G13";
patch["HTMLColor"] = "#13528e"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 33.809743;
patch["TargetLAB"]["A"] = -1.609375;
patch["TargetLAB"]["B"] = -39.039062;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "G14";
patch["HTMLColor"] = "#646669"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 43.121936;
patch["TargetLAB"]["A"] = -0.371094;
patch["TargetLAB"]["B"] = -1.914062;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "G15";
patch["HTMLColor"] = "#4a3b29"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 26.127451;
patch["TargetLAB"]["A"] = 5.085938;
patch["TargetLAB"]["B"] = 13.468750;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "G16";
patch["HTMLColor"] = "#784c71"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 38.328738;
patch["TargetLAB"]["A"] = 23.851562;
patch["TargetLAB"]["B"] = -13.746094;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "G17";
patch["HTMLColor"] = "#936b87"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 50.050551;
patch["TargetLAB"]["A"] = 20.421875;
patch["TargetLAB"]["B"] = -9.222656;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "G18";
patch["HTMLColor"] = "#253636"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 21.026348;
patch["TargetLAB"]["A"] = -6.777344;
patch["TargetLAB"]["B"] = -2.507812;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "G19";
patch["HTMLColor"] = "#532d39"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 23.825061;
patch["TargetLAB"]["A"] = 19.039062;
patch["TargetLAB"]["B"] = 0.367188;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "G20";
patch["HTMLColor"] = "#513029"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 24.114583;
patch["TargetLAB"]["A"] = 14.714844;
patch["TargetLAB"]["B"] = 11.125000;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "G21";
patch["HTMLColor"] = "#9d9967"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 62.556679;
patch["TargetLAB"]["A"] = -4.339844;
patch["TargetLAB"]["B"] = 26.621094;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "H1";
patch["HTMLColor"] = "#13754e"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 43.509498;
patch["TargetLAB"]["A"] = -35.386719;
patch["TargetLAB"]["B"] = 13.722656;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "H2";
patch["HTMLColor"] = "#4a583d"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 35.641850;
patch["TargetLAB"]["A"] = -9.832031;
patch["TargetLAB"]["B"] = 13.867188;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "H3";
patch["HTMLColor"] = "#51615b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 39.649203;
patch["TargetLAB"]["A"] = -7.328125;
patch["TargetLAB"]["B"] = 1.386719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "H4";
patch["HTMLColor"] = "#203d6e"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 25.500919;
patch["TargetLAB"]["A"] = 2.925781;
patch["TargetLAB"]["B"] = -32.261719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "H5";
patch["HTMLColor"] = "#163354"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 20.338542;
patch["TargetLAB"]["A"] = -1.421875;
patch["TargetLAB"]["B"] = -23.699219;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "H6";
patch["HTMLColor"] = "#2b6568"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 39.163603;
patch["TargetLAB"]["A"] = -19.089844;
patch["TargetLAB"]["B"] = -7.449219;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "H7";
patch["HTMLColor"] = "#7a6b4d"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 46.164216;
patch["TargetLAB"]["A"] = 2.753906;
patch["TargetLAB"]["B"] = 19.156250;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "H8";
patch["HTMLColor"] = "#503a5d"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 27.991728;
patch["TargetLAB"]["A"] = 15.757812;
patch["TargetLAB"]["B"] = -17.250000;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "H9";
patch["HTMLColor"] = "#004d66"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 28.593750;
patch["TargetLAB"]["A"] = -19.714844;
patch["TargetLAB"]["B"] = -22.542969;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "H10";
patch["HTMLColor"] = "#10455f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 27.008272;
patch["TargetLAB"]["A"] = -9.726562;
patch["TargetLAB"]["B"] = -20.531250;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "H11";
patch["HTMLColor"] = "#38363a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 23.074449;
patch["TargetLAB"]["A"] = 1.535156;
patch["TargetLAB"]["B"] = -1.683594;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "H12";
patch["HTMLColor"] = "#6f3835"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 30.775123;
patch["TargetLAB"]["A"] = 24.347656;
patch["TargetLAB"]["B"] = 13.593750;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "H13";
patch["HTMLColor"] = "#554f32"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 33.681066;
patch["TargetLAB"]["A"] = -1.406250;
patch["TargetLAB"]["B"] = 18.035156;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "H14";
patch["HTMLColor"] = "#43805f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 48.943015;
patch["TargetLAB"]["A"] = -26.855469;
patch["TargetLAB"]["B"] = 11.644531;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "H15";
patch["HTMLColor"] = "#16414c"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 25.041360;
patch["TargetLAB"]["A"] = -12.242188;
patch["TargetLAB"]["B"] = -11.492188;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "H16";
patch["HTMLColor"] = "#cfbec3"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 78.423713;
patch["TargetLAB"]["A"] = 6.992188;
patch["TargetLAB"]["B"] = -0.242188;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "H17";
patch["HTMLColor"] = "#84605e"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 44.485294;
patch["TargetLAB"]["A"] = 14.597656;
patch["TargetLAB"]["B"] = 7.167969;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "H18";
patch["HTMLColor"] = "#506941"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 41.469056;
patch["TargetLAB"]["A"] = -15.289062;
patch["TargetLAB"]["B"] = 19.066406;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "H19";
patch["HTMLColor"] = "#93592b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 43.880208;
patch["TargetLAB"]["A"] = 21.476562;
patch["TargetLAB"]["B"] = 36.460938;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "H20";
patch["HTMLColor"] = "#00507d"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 31.542586;
patch["TargetLAB"]["A"] = -8.554688;
patch["TargetLAB"]["B"] = -32.332031;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "H21";
patch["HTMLColor"] = "#541d38"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 20.378370;
patch["TargetLAB"]["A"] = 27.851562;
patch["TargetLAB"]["B"] = -3.691406;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "I1";
patch["HTMLColor"] = "#748cad"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 57.311581;
patch["TargetLAB"]["A"] = -2.851562;
patch["TargetLAB"]["B"] = -20.593750;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "I2";
patch["HTMLColor"] = "#716b7a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 46.325061;
patch["TargetLAB"]["A"] = 4.539062;
patch["TargetLAB"]["B"] = -7.421875;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "I3";
patch["HTMLColor"] = "#c4beac"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 77.247243;
patch["TargetLAB"]["A"] = -0.226562;
patch["TargetLAB"]["B"] = 10.277344;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "I4";
patch["HTMLColor"] = "#2d432c"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 26.176471;
patch["TargetLAB"]["A"] = -13.167969;
patch["TargetLAB"]["B"] = 11.218750;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "I5";
patch["HTMLColor"] = "#525048"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 34.114583;
patch["TargetLAB"]["A"] = -0.757812;
patch["TargetLAB"]["B"] = 5.203125;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "I6";
patch["HTMLColor"] = "#583d34"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 28.515625;
patch["TargetLAB"]["A"] = 11.273438;
patch["TargetLAB"]["B"] = 10.332031;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "I7";
patch["HTMLColor"] = "#555564"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 36.544118;
patch["TargetLAB"]["A"] = 2.460938;
patch["TargetLAB"]["B"] = -8.863281;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "I8";
patch["HTMLColor"] = "#c84d42"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 50.105699;
patch["TargetLAB"]["A"] = 49.140625;
patch["TargetLAB"]["B"] = 33.531250;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "I9";
patch["HTMLColor"] = "#9c387b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 40.238971;
patch["TargetLAB"]["A"] = 47.386719;
patch["TargetLAB"]["B"] = -16.261719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "I10";
patch["HTMLColor"] = "#034143"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 24.025735;
patch["TargetLAB"]["A"] = -17.937500;
patch["TargetLAB"]["B"] = -7.093750;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "I11";
patch["HTMLColor"] = "#62697f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 44.543505;
patch["TargetLAB"]["A"] = 0.906250;
patch["TargetLAB"]["B"] = -13.191406;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "I12";
patch["HTMLColor"] = "#7e3f56"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 35.500919;
patch["TargetLAB"]["A"] = 30.187500;
patch["TargetLAB"]["B"] = -0.878906;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "I13";
patch["HTMLColor"] = "#474a56"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 31.662071;
patch["TargetLAB"]["A"] = 0.816406;
patch["TargetLAB"]["B"] = -7.308594;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "I14";
patch["HTMLColor"] = "#4e4c44"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 32.302390;
patch["TargetLAB"]["A"] = -0.101562;
patch["TargetLAB"]["B"] = 5.300781;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "I15";
patch["HTMLColor"] = "#96af82"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 68.616728;
patch["TargetLAB"]["A"] = -15.199219;
patch["TargetLAB"]["B"] = 20.191406;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "I16";
patch["HTMLColor"] = "#9e9eb2"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 65.551471;
patch["TargetLAB"]["A"] = 3.015625;
patch["TargetLAB"]["B"] = -10.617188;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "I17";
patch["HTMLColor"] = "#3c3f94"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 30.424326;
patch["TargetLAB"]["A"] = 19.285156;
patch["TargetLAB"]["B"] = -48.292969;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "I18";
patch["HTMLColor"] = "#7a2b2b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 29.833027;
patch["TargetLAB"]["A"] = 35.148438;
patch["TargetLAB"]["B"] = 18.945312;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "I19";
patch["HTMLColor"] = "#666478"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 43.039216;
patch["TargetLAB"]["A"] = 4.101562;
patch["TargetLAB"]["B"] = -10.804688;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "I20";
patch["HTMLColor"] = "#cdc46f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 78.468137;
patch["TargetLAB"]["A"] = -5.316406;
patch["TargetLAB"]["B"] = 43.546875;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "I21";
patch["HTMLColor"] = "#2f4737"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 27.820159;
patch["TargetLAB"]["A"] = -12.925781;
patch["TargetLAB"]["B"] = 6.769531;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "J1";
patch["HTMLColor"] = "#2d3336"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 20.643382;
patch["TargetLAB"]["A"] = -1.714844;
patch["TargetLAB"]["B"] = -3.050781;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "J2";
patch["HTMLColor"] = "#3993cf"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 57.765012;
patch["TargetLAB"]["A"] = -11.968750;
patch["TargetLAB"]["B"] = -39.332031;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "J3";
patch["HTMLColor"] = "#3a2046"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 17.156863;
patch["TargetLAB"]["A"] = 18.835938;
patch["TargetLAB"]["B"] = -19.312500;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "J4";
patch["HTMLColor"] = "#745843"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 39.957108;
patch["TargetLAB"]["A"] = 9.812500;
patch["TargetLAB"]["B"] = 16.808594;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "J5";
patch["HTMLColor"] = "#434b35"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 30.643382;
patch["TargetLAB"]["A"] = -6.667969;
patch["TargetLAB"]["B"] = 12.179688;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "J6";
patch["HTMLColor"] = "#2f4245"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 26.473652;
patch["TargetLAB"]["A"] = -7.218750;
patch["TargetLAB"]["B"] = -4.550781;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "J7";
patch["HTMLColor"] = "#1f4758"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 28.045343;
patch["TargetLAB"]["A"] = -10.437500;
patch["TargetLAB"]["B"] = -14.292969;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "J8";
patch["HTMLColor"] = "#62a481"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 62.238051;
patch["TargetLAB"]["A"] = -28.050781;
patch["TargetLAB"]["B"] = 11.597656;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "J9";
patch["HTMLColor"] = "#57a469"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 61.384804;
patch["TargetLAB"]["A"] = -34.773438;
patch["TargetLAB"]["B"] = 23.132812;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "J10";
patch["HTMLColor"] = "#d08879"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 64.113051;
patch["TargetLAB"]["A"] = 27.199219;
patch["TargetLAB"]["B"] = 20.000000;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "J11";
patch["HTMLColor"] = "#0b2b23"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 15.087316;
patch["TargetLAB"]["A"] = -14.050781;
patch["TargetLAB"]["B"] = 1.699219;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "J12";
patch["HTMLColor"] = "#505c63"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 38.100490;
patch["TargetLAB"]["A"] = -3.386719;
patch["TargetLAB"]["B"] = -6.167969;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "J13";
patch["HTMLColor"] = "#352526"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 16.686581;
patch["TargetLAB"]["A"] = 7.847656;
patch["TargetLAB"]["B"] = 2.121094;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "J14";
patch["HTMLColor"] = "#423c59"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 26.806066;
patch["TargetLAB"]["A"] = 8.203125;
patch["TargetLAB"]["B"] = -16.562500;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "J15";
patch["HTMLColor"] = "#6f6044"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 41.668199;
patch["TargetLAB"]["A"] = 2.695312;
patch["TargetLAB"]["B"] = 18.339844;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "J16";
patch["HTMLColor"] = "#ffffff"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 100.000000;
patch["TargetLAB"]["A"] = 0.000000;
patch["TargetLAB"]["B"] = 0.000000;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "J17";
patch["HTMLColor"] = "#cf8e63"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 64.928002;
patch["TargetLAB"]["A"] = 21.898438;
patch["TargetLAB"]["B"] = 33.464844;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "J18";
patch["HTMLColor"] = "#154465"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 26.893382;
patch["TargetLAB"]["A"] = -6.046875;
patch["TargetLAB"]["B"] = -24.769531;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "J19";
patch["HTMLColor"] = "#a44f6b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 45.349265;
patch["TargetLAB"]["A"] = 37.718750;
patch["TargetLAB"]["B"] = 0.957031;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "J20";
patch["HTMLColor"] = "#395244"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 32.369792;
patch["TargetLAB"]["A"] = -12.429688;
patch["TargetLAB"]["B"] = 5.097656;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "J21";
patch["HTMLColor"] = "#a1b5a9"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 71.803002;
patch["TargetLAB"]["A"] = -8.898438;
patch["TargetLAB"]["B"] = 3.847656;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "K1";
patch["HTMLColor"] = "#296551"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 38.449755;
patch["TargetLAB"]["A"] = -24.375000;
patch["TargetLAB"]["B"] = 5.328125;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "K2";
patch["HTMLColor"] = "#528996"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 53.651961;
patch["TargetLAB"]["A"] = -16.003906;
patch["TargetLAB"]["B"] = -13.191406;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "K3";
patch["HTMLColor"] = "#1c7d8d"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 47.697610;
patch["TargetLAB"]["A"] = -23.234375;
patch["TargetLAB"]["B"] = -16.984375;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "K4";
patch["HTMLColor"] = "#683637"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 29.253983;
patch["TargetLAB"]["A"] = 23.273438;
patch["TargetLAB"]["B"] = 9.476562;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "K5";
patch["HTMLColor"] = "#abd376"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 80.263480;
patch["TargetLAB"]["A"] = -25.218750;
patch["TargetLAB"]["B"] = 41.457031;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "K6";
patch["HTMLColor"] = "#008a4d"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 49.001225;
patch["TargetLAB"]["A"] = -54.968750;
patch["TargetLAB"]["B"] = 21.625000;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "K7";
patch["HTMLColor"] = "#f9cee2"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 86.962316;
patch["TargetLAB"]["A"] = 18.109375;
patch["TargetLAB"]["B"] = -3.980469;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "K8";
patch["HTMLColor"] = "#95a549"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 64.970895;
patch["TargetLAB"]["A"] = -16.054688;
patch["TargetLAB"]["B"] = 44.500000;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "K9";
patch["HTMLColor"] = "#6f5850"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 39.537377;
patch["TargetLAB"]["A"] = 8.945312;
patch["TargetLAB"]["B"] = 8.125000;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "K10";
patch["HTMLColor"] = "#7a5663"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 40.939032;
patch["TargetLAB"]["A"] = 16.652344;
patch["TargetLAB"]["B"] = -1.082031;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "K11";
patch["HTMLColor"] = "#434f36"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 31.868873;
patch["TargetLAB"]["A"] = -8.953125;
patch["TargetLAB"]["B"] = 13.257812;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "K12";
patch["HTMLColor"] = "#004e4b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 28.636642;
patch["TargetLAB"]["A"] = -27.312500;
patch["TargetLAB"]["B"] = -5.281250;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "K13";
patch["HTMLColor"] = "#cbd9cc"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 85.240502;
patch["TargetLAB"]["A"] = -6.433594;
patch["TargetLAB"]["B"] = 4.636719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "K14";
patch["HTMLColor"] = "#472f21"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 22.181373;
patch["TargetLAB"]["A"] = 9.648438;
patch["TargetLAB"]["B"] = 13.691406;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "K15";
patch["HTMLColor"] = "#614c5e"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 35.234375;
patch["TargetLAB"]["A"] = 11.656250;
patch["TargetLAB"]["B"] = -6.906250;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "K16";
patch["HTMLColor"] = "#423439"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 23.425245;
patch["TargetLAB"]["A"] = 7.390625;
patch["TargetLAB"]["B"] = -0.789062;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "K17";
patch["HTMLColor"] = "#336452"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 38.564645;
patch["TargetLAB"]["A"] = -20.957031;
patch["TargetLAB"]["B"] = 4.964844;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "K18";
patch["HTMLColor"] = "#005366"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 31.253064;
patch["TargetLAB"]["A"] = -20.042969;
patch["TargetLAB"]["B"] = -18.523438;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "K19";
patch["HTMLColor"] = "#5b3b32"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 28.469669;
patch["TargetLAB"]["A"] = 14.097656;
patch["TargetLAB"]["B"] = 11.300781;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "K20";
patch["HTMLColor"] = "#442023"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 17.558211;
patch["TargetLAB"]["A"] = 18.144531;
patch["TargetLAB"]["B"] = 6.281250;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "K21";
patch["HTMLColor"] = "#651d42"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 24.042586;
patch["TargetLAB"]["A"] = 35.144531;
patch["TargetLAB"]["B"] = -5.175781;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "L1";
patch["HTMLColor"] = "#212820"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 15.424326;
patch["TargetLAB"]["A"] = -4.847656;
patch["TargetLAB"]["B"] = 4.476562;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "L2";
patch["HTMLColor"] = "#006a59"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 38.892463;
patch["TargetLAB"]["A"] = -35.183594;
patch["TargetLAB"]["B"] = 1.132812;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "L3";
patch["HTMLColor"] = "#2d2c4a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 19.257047;
patch["TargetLAB"]["A"] = 7.109375;
patch["TargetLAB"]["B"] = -18.503906;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "L4";
patch["HTMLColor"] = "#169c5f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 56.796875;
patch["TargetLAB"]["A"] = -46.109375;
patch["TargetLAB"]["B"] = 21.875000;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "L5";
patch["HTMLColor"] = "#31425a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 27.483150;
patch["TargetLAB"]["A"] = -1.593750;
patch["TargetLAB"]["B"] = -16.726562;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "L6";
patch["HTMLColor"] = "#4b402a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 27.778799;
patch["TargetLAB"]["A"] = 2.035156;
patch["TargetLAB"]["B"] = 15.511719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "L7";
patch["HTMLColor"] = "#1e3727"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 20.554534;
patch["TargetLAB"]["A"] = -13.359375;
patch["TargetLAB"]["B"] = 6.964844;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "L8";
patch["HTMLColor"] = "#394b3f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 30.165441;
patch["TargetLAB"]["A"] = -9.722656;
patch["TargetLAB"]["B"] = 5.199219;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "L9";
patch["HTMLColor"] = "#3f3330"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 22.478554;
patch["TargetLAB"]["A"] = 5.523438;
patch["TargetLAB"]["B"] = 3.945312;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "L10";
patch["HTMLColor"] = "#9c6f48"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 50.886949;
patch["TargetLAB"]["A"] = 14.976562;
patch["TargetLAB"]["B"] = 28.871094;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "L11";
patch["HTMLColor"] = "#1f221b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 12.685355;
patch["TargetLAB"]["A"] = -2.714844;
patch["TargetLAB"]["B"] = 3.949219;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "L12";
patch["HTMLColor"] = "#5f767b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 47.948836;
patch["TargetLAB"]["A"] = -8.113281;
patch["TargetLAB"]["B"] = -5.535156;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "L13";
patch["HTMLColor"] = "#c57799"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 59.437806;
patch["TargetLAB"]["A"] = 34.691406;
patch["TargetLAB"]["B"] = -4.546875;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "L14";
patch["HTMLColor"] = "#fff56b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 95.991115;
patch["TargetLAB"]["A"] = -5.789062;
patch["TargetLAB"]["B"] = 66.652344;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "L15";
patch["HTMLColor"] = "#1d5ba7"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 37.939645;
patch["TargetLAB"]["A"] = 2.769531;
patch["TargetLAB"]["B"] = -47.398438;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "L16";
patch["HTMLColor"] = "#bf6944"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 54.185049;
patch["TargetLAB"]["A"] = 32.742188;
patch["TargetLAB"]["B"] = 36.292969;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "L17";
patch["HTMLColor"] = "#cdca52"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 80.018382;
patch["TargetLAB"]["A"] = -9.964844;
patch["TargetLAB"]["B"] = 58.484375;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "L18";
patch["HTMLColor"] = "#3b2d2a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 20.061275;
patch["TargetLAB"]["A"] = 6.093750;
patch["TargetLAB"]["B"] = 4.613281;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "L19";
patch["HTMLColor"] = "#6f5624"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 38.262868;
patch["TargetLAB"]["A"] = 6.480469;
patch["TargetLAB"]["B"] = 31.976562;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "L20";
patch["HTMLColor"] = "#633033"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 26.959252;
patch["TargetLAB"]["A"] = 23.898438;
patch["TargetLAB"]["B"] = 8.996094;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "L21";
patch["HTMLColor"] = "#8f88a5"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 57.993260;
patch["TargetLAB"]["A"] = 7.468750;
patch["TargetLAB"]["B"] = -14.859375;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "M1";
patch["HTMLColor"] = "#bf2865"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 44.178922;
patch["TargetLAB"]["A"] = 61.628906;
patch["TargetLAB"]["B"] = 3.761719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "M2";
patch["HTMLColor"] = "#373652"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 23.769914;
patch["TargetLAB"]["A"] = 6.113281;
patch["TargetLAB"]["B"] = -16.917969;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "M3";
patch["HTMLColor"] = "#a6a3a7"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 67.207414;
patch["TargetLAB"]["A"] = 1.828125;
patch["TargetLAB"]["B"] = -1.585938;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "M4";
patch["HTMLColor"] = "#68243b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 25.727635;
patch["TargetLAB"]["A"] = 32.644531;
patch["TargetLAB"]["B"] = 2.355469;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "M5";
patch["HTMLColor"] = "#c26062"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 53.109681;
patch["TargetLAB"]["A"] = 39.917969;
patch["TargetLAB"]["B"] = 17.968750;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "M6";
patch["HTMLColor"] = "#321827"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 12.697610;
patch["TargetLAB"]["A"] = 14.898438;
patch["TargetLAB"]["B"] = -3.812500;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "M7";
patch["HTMLColor"] = "#c1666c"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 54.329044;
patch["TargetLAB"]["A"] = 37.832031;
patch["TargetLAB"]["B"] = 14.230469;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "M8";
patch["HTMLColor"] = "#008360"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 47.284007;
patch["TargetLAB"]["A"] = -47.351562;
patch["TargetLAB"]["B"] = 8.746094;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "M9";
patch["HTMLColor"] = "#6a5753"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 38.844975;
patch["TargetLAB"]["A"] = 7.691406;
patch["TargetLAB"]["B"] = 5.347656;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "M10";
patch["HTMLColor"] = "#9b583d"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 45.007659;
patch["TargetLAB"]["A"] = 26.527344;
patch["TargetLAB"]["B"] = 28.160156;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "M11";
patch["HTMLColor"] = "#6b4068"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 33.486520;
patch["TargetLAB"]["A"] = 24.683594;
patch["TargetLAB"]["B"] = -15.691406;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "M12";
patch["HTMLColor"] = "#0d6d6b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 41.191789;
patch["TargetLAB"]["A"] = -26.902344;
patch["TargetLAB"]["B"] = -6.296875;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "M13";
patch["HTMLColor"] = "#929046"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 58.854167;
patch["TargetLAB"]["A"] = -7.054688;
patch["TargetLAB"]["B"] = 39.070312;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "M14";
patch["HTMLColor"] = "#3e3a4f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 25.503983;
patch["TargetLAB"]["A"] = 5.246094;
patch["TargetLAB"]["B"] = -11.878906;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "M15";
patch["HTMLColor"] = "#55494c"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 32.293199;
patch["TargetLAB"]["A"] = 5.656250;
patch["TargetLAB"]["B"] = 0.414062;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "M16";
patch["HTMLColor"] = "#2ba38c"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 60.202206;
patch["TargetLAB"]["A"] = -38.058594;
patch["TargetLAB"]["B"] = 2.218750;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "M17";
patch["HTMLColor"] = "#301822"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 12.058824;
patch["TargetLAB"]["A"] = 13.734375;
patch["TargetLAB"]["B"] = -1.394531;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "M18";
patch["HTMLColor"] = "#437651"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 45.203738;
patch["TargetLAB"]["A"] = -24.488281;
patch["TargetLAB"]["B"] = 14.906250;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "M19";
patch["HTMLColor"] = "#342320"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 15.903799;
patch["TargetLAB"]["A"] = 7.687500;
patch["TargetLAB"]["B"] = 5.617188;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "M20";
patch["HTMLColor"] = "#bb2d43"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 43.172488;
patch["TargetLAB"]["A"] = 57.265625;
patch["TargetLAB"]["B"] = 24.011719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "M21";
patch["HTMLColor"] = "#353436"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 22.003676;
patch["TargetLAB"]["A"] = 0.789062;
patch["TargetLAB"]["B"] = -0.816406;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "N1";
patch["HTMLColor"] = "#63694a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 43.391544;
patch["TargetLAB"]["A"] = -6.875000;
patch["TargetLAB"]["B"] = 16.730469;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "N2";
patch["HTMLColor"] = "#4a3829"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 25.191483;
patch["TargetLAB"]["A"] = 6.367188;
patch["TargetLAB"]["B"] = 12.261719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "N3";
patch["HTMLColor"] = "#3d3a26"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 24.385723;
patch["TargetLAB"]["A"] = -1.398438;
patch["TargetLAB"]["B"] = 13.265625;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "N4";
patch["HTMLColor"] = "#30423a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 26.081495;
patch["TargetLAB"]["A"] = -8.886719;
patch["TargetLAB"]["B"] = 2.156250;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "N5";
patch["HTMLColor"] = "#a29a98"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 64.223346;
patch["TargetLAB"]["A"] = 3.023438;
patch["TargetLAB"]["B"] = 2.273438;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "N6";
patch["HTMLColor"] = "#742a39"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 28.829657;
patch["TargetLAB"]["A"] = 34.359375;
patch["TargetLAB"]["B"] = 8.042969;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "N7";
patch["HTMLColor"] = "#264d75"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 31.364890;
patch["TargetLAB"]["A"] = -3.453125;
patch["TargetLAB"]["B"] = -27.386719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "N8";
patch["HTMLColor"] = "#98ac77"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 67.760417;
patch["TargetLAB"]["A"] = -14.292969;
patch["TargetLAB"]["B"] = 25.066406;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "N9";
patch["HTMLColor"] = "#bc9a8e"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 66.476716;
patch["TargetLAB"]["A"] = 12.046875;
patch["TargetLAB"]["B"] = 11.167969;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "N10";
patch["HTMLColor"] = "#2a2e1d"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 18.083640;
patch["TargetLAB"]["A"] = -4.152344;
patch["TargetLAB"]["B"] = 10.152344;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "N11";
patch["HTMLColor"] = "#291920"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 11.056985;
patch["TargetLAB"]["A"] = 9.332031;
patch["TargetLAB"]["B"] = -1.640625;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "N12";
patch["HTMLColor"] = "#c2b36b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 73.071385;
patch["TargetLAB"]["A"] = -1.652344;
patch["TargetLAB"]["B"] = 38.457031;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "N13";
patch["HTMLColor"] = "#17202f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 12.023591;
patch["TargetLAB"]["A"] = -0.312500;
patch["TargetLAB"]["B"] = -11.648438;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "N14";
patch["HTMLColor"] = "#5f4f4f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 35.219056;
patch["TargetLAB"]["A"] = 6.734375;
patch["TargetLAB"]["B"] = 2.730469;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "N15";
patch["HTMLColor"] = "#5e6b47"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 43.327206;
patch["TargetLAB"]["A"] = -10.304688;
patch["TargetLAB"]["B"] = 18.578125;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "N16";
patch["HTMLColor"] = "#0e3645"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 20.359988;
patch["TargetLAB"]["A"] = -9.472656;
patch["TargetLAB"]["B"] = -13.773438;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "N17";
patch["HTMLColor"] = "#10524a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 30.741422;
patch["TargetLAB"]["A"] = -22.300781;
patch["TargetLAB"]["B"] = -1.261719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "N18";
patch["HTMLColor"] = "#a44c88"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 45.477941;
patch["TargetLAB"]["A"] = 42.523438;
patch["TargetLAB"]["B"] = -16.332031;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "N19";
patch["HTMLColor"] = "#9e8faf"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 61.622243;
patch["TargetLAB"]["A"] = 10.542969;
patch["TargetLAB"]["B"] = -14.714844;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "N20";
patch["HTMLColor"] = "#76b36d"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 67.467831;
patch["TargetLAB"]["A"] = -30.882812;
patch["TargetLAB"]["B"] = 29.230469;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "N21";
patch["HTMLColor"] = "#f16a37"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 61.770833;
patch["TargetLAB"]["A"] = 51.226562;
patch["TargetLAB"]["B"] = 53.550781;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "O1";
patch["HTMLColor"] = "#a19a7f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 63.708640;
patch["TargetLAB"]["A"] = -0.937500;
patch["TargetLAB"]["B"] = 15.140625;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "O2";
patch["HTMLColor"] = "#576455"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 40.805760;
patch["TargetLAB"]["A"] = -7.167969;
patch["TargetLAB"]["B"] = 6.511719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "O3";
patch["HTMLColor"] = "#008ebf"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 52.140012;
patch["TargetLAB"]["A"] = -34.250000;
patch["TargetLAB"]["B"] = -39.296875;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "O4";
patch["HTMLColor"] = "#302922"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 17.412684;
patch["TargetLAB"]["A"] = 2.167969;
patch["TargetLAB"]["B"] = 6.113281;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "O5";
patch["HTMLColor"] = "#3a807b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 49.068627;
patch["TargetLAB"]["A"] = -23.597656;
patch["TargetLAB"]["B"] = -4.574219;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "O6";
patch["HTMLColor"] = "#793d4a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 33.915441;
patch["TargetLAB"]["A"] = 27.937500;
patch["TargetLAB"]["B"] = 4.707031;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "O7";
patch["HTMLColor"] = "#28503d"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 30.505515;
patch["TargetLAB"]["A"] = -18.414062;
patch["TargetLAB"]["B"] = 6.648438;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "O8";
patch["HTMLColor"] = "#434432"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 28.327206;
patch["TargetLAB"]["A"] = -3.152344;
patch["TargetLAB"]["B"] = 10.968750;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "O9";
patch["HTMLColor"] = "#5c7e99"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 50.906863;
patch["TargetLAB"]["A"] = -6.800781;
patch["TargetLAB"]["B"] = -18.914062;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "O10";
patch["HTMLColor"] = "#8e807a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 54.725797;
patch["TargetLAB"]["A"] = 4.460938;
patch["TargetLAB"]["B"] = 5.414062;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "O11";
patch["HTMLColor"] = "#2a3750"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 22.703738;
patch["TargetLAB"]["A"] = 0.609375;
patch["TargetLAB"]["B"] = -17.394531;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "O12";
patch["HTMLColor"] = "#231a1d"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 10.402880;
patch["TargetLAB"]["A"] = 5.171875;
patch["TargetLAB"]["B"] = -0.785156;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "O13";
patch["HTMLColor"] = "#543b24"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 27.385110;
patch["TargetLAB"]["A"] = 9.363281;
patch["TargetLAB"]["B"] = 19.050781;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "O14";
patch["HTMLColor"] = "#3f7ca5"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 49.387255;
patch["TargetLAB"]["A"] = -10.820312;
patch["TargetLAB"]["B"] = -28.031250;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "O15";
patch["HTMLColor"] = "#90404e"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 38.354779;
patch["TargetLAB"]["A"] = 35.375000;
patch["TargetLAB"]["B"] = 9.046875;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "O16";
patch["HTMLColor"] = "#072c29"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 15.376838;
patch["TargetLAB"]["A"] = -13.898438;
patch["TargetLAB"]["B"] = -2.003906;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "O17";
patch["HTMLColor"] = "#5d2027"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 22.253370;
patch["TargetLAB"]["A"] = 29.035156;
patch["TargetLAB"]["B"] = 10.375000;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "O18";
patch["HTMLColor"] = "#1e8b8b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 52.126225;
patch["TargetLAB"]["A"] = -29.765625;
patch["TargetLAB"]["B"] = -9.386719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "O19";
patch["HTMLColor"] = "#544c44"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 32.791054;
patch["TargetLAB"]["A"] = 2.414062;
patch["TargetLAB"]["B"] = 5.804688;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "O20";
patch["HTMLColor"] = "#00aba2"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 60.471814;
patch["TargetLAB"]["A"] = -57.324219;
patch["TargetLAB"]["B"] = -10.046875;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "O21";
patch["HTMLColor"] = "#434939"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 30.180760;
patch["TargetLAB"]["A"] = -5.292969;
patch["TargetLAB"]["B"] = 8.695312;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "P1";
patch["HTMLColor"] = "#007483"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 42.372855;
patch["TargetLAB"]["A"] = -34.765625;
patch["TargetLAB"]["B"] = -19.597656;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "P2";
patch["HTMLColor"] = "#48bc84"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 68.595282;
patch["TargetLAB"]["A"] = -43.691406;
patch["TargetLAB"]["B"] = 18.140625;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "P3";
patch["HTMLColor"] = "#242c30"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 17.233456;
patch["TargetLAB"]["A"] = -2.496094;
patch["TargetLAB"]["B"] = -3.937500;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "P4";
patch["HTMLColor"] = "#231f1f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 12.250306;
patch["TargetLAB"]["A"] = 2.359375;
patch["TargetLAB"]["B"] = 0.539062;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "P5";
patch["HTMLColor"] = "#977035"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 50.356924;
patch["TargetLAB"]["A"] = 10.992188;
patch["TargetLAB"]["B"] = 38.324219;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "P6";
patch["HTMLColor"] = "#2d372a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 21.897978;
patch["TargetLAB"]["A"] = -6.730469;
patch["TargetLAB"]["B"] = 6.531250;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "P7";
patch["HTMLColor"] = "#3b262f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 17.997855;
patch["TargetLAB"]["A"] = 11.832031;
patch["TargetLAB"]["B"] = -2.031250;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "P8";
patch["HTMLColor"] = "#ba2475"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 43.224571;
patch["TargetLAB"]["A"] = 62.171875;
patch["TargetLAB"]["B"] = -7.175781;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "P9";
patch["HTMLColor"] = "#4b2a63"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 23.606005;
patch["TargetLAB"]["A"] = 24.523438;
patch["TargetLAB"]["B"] = -28.097656;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "P10";
patch["HTMLColor"] = "#4c2723"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 20.811887;
patch["TargetLAB"]["A"] = 17.062500;
patch["TargetLAB"]["B"] = 10.605469;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "P11";
patch["HTMLColor"] = "#33434b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 27.227328;
patch["TargetLAB"]["A"] = -4.792969;
patch["TargetLAB"]["B"] = -7.179688;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "P12";
patch["HTMLColor"] = "#008876"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 49.543505;
patch["TargetLAB"]["A"] = -42.546875;
patch["TargetLAB"]["B"] = -0.816406;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "P13";
patch["HTMLColor"] = "#556749"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 41.409314;
patch["TargetLAB"]["A"] = -11.414062;
patch["TargetLAB"]["B"] = 14.445312;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "P14";
patch["HTMLColor"] = "#422a33"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 20.407475;
patch["TargetLAB"]["A"] = 12.125000;
patch["TargetLAB"]["B"] = -1.011719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "P15";
patch["HTMLColor"] = "#60967c"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 57.870711;
patch["TargetLAB"]["A"] = -23.187500;
patch["TargetLAB"]["B"] = 7.875000;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "P16";
patch["HTMLColor"] = "#92894f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 56.763174;
patch["TargetLAB"]["A"] = -2.804688;
patch["TargetLAB"]["B"] = 31.902344;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "P17";
patch["HTMLColor"] = "#202024"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 12.352941;
patch["TargetLAB"]["A"] = 0.667969;
patch["TargetLAB"]["B"] = -2.718750;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "P18";
patch["HTMLColor"] = "#777c4e"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 50.598958;
patch["TargetLAB"]["A"] = -7.730469;
patch["TargetLAB"]["B"] = 24.238281;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "P19";
patch["HTMLColor"] = "#885d4f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 43.863358;
patch["TargetLAB"]["A"] = 16.886719;
patch["TargetLAB"]["B"] = 15.289062;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "P20";
patch["HTMLColor"] = "#2d2538"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 16.450674;
patch["TargetLAB"]["A"] = 7.187500;
patch["TargetLAB"]["B"] = -10.945312;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "P21";
patch["HTMLColor"] = "#33241b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 15.934436;
patch["TargetLAB"]["A"] = 6.218750;
patch["TargetLAB"]["B"] = 8.734375;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "Q1";
patch["HTMLColor"] = "#3e3e33"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 25.891544;
patch["TargetLAB"]["A"] = -1.433594;
patch["TargetLAB"]["B"] = 7.062500;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "Q2";
patch["HTMLColor"] = "#77715e"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 47.863051;
patch["TargetLAB"]["A"] = -0.320312;
patch["TargetLAB"]["B"] = 11.796875;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "Q3";
patch["HTMLColor"] = "#395c3a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 35.752145;
patch["TargetLAB"]["A"] = -19.058594;
patch["TargetLAB"]["B"] = 15.433594;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "Q4";
patch["HTMLColor"] = "#a8534f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 46.240809;
patch["TargetLAB"]["A"] = 35.406250;
patch["TargetLAB"]["B"] = 19.378906;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "Q5";
patch["HTMLColor"] = "#4c4334"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 28.901654;
patch["TargetLAB"]["A"] = 1.953125;
patch["TargetLAB"]["B"] = 10.785156;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "Q6";
patch["HTMLColor"] = "#2d2529"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 15.827206;
patch["TargetLAB"]["A"] = 4.683594;
patch["TargetLAB"]["B"] = -1.367188;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "Q7";
patch["HTMLColor"] = "#755174"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 39.327512;
patch["TargetLAB"]["A"] = 20.441406;
patch["TargetLAB"]["B"] = -14.210938;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "Q8";
patch["HTMLColor"] = "#233b40"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 23.230699;
patch["TargetLAB"]["A"] = -8.664062;
patch["TargetLAB"]["B"] = -6.042969;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "Q9";
patch["HTMLColor"] = "#7f4f60"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 39.719669;
patch["TargetLAB"]["A"] = 22.343750;
patch["TargetLAB"]["B"] = -1.046875;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "Q10";
patch["HTMLColor"] = "#4f552f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 34.897365;
patch["TargetLAB"]["A"] = -7.687500;
patch["TargetLAB"]["B"] = 21.238281;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "Q11";
patch["HTMLColor"] = "#424333"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 27.976409;
patch["TargetLAB"]["A"] = -3.144531;
patch["TargetLAB"]["B"] = 9.828125;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "Q12";
patch["HTMLColor"] = "#675677"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 39.295343;
patch["TargetLAB"]["A"] = 12.402344;
patch["TargetLAB"]["B"] = -15.792969;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "Q13";
patch["HTMLColor"] = "#052c30"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 15.683211;
patch["TargetLAB"]["A"] = -12.667969;
patch["TargetLAB"]["B"] = -6.566406;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "Q14";
patch["HTMLColor"] = "#a4714b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 52.539828;
patch["TargetLAB"]["A"] = 17.347656;
patch["TargetLAB"]["B"] = 29.785156;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "Q15";
patch["HTMLColor"] = "#437059"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 43.558517;
patch["TargetLAB"]["A"] = -20.496094;
patch["TargetLAB"]["B"] = 8.113281;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "Q16";
patch["HTMLColor"] = "#00adc6"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 61.763174;
patch["TargetLAB"]["A"] = -50.027344;
patch["TargetLAB"]["B"] = -28.316406;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "Q17";
patch["HTMLColor"] = "#975073"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 43.632047;
patch["TargetLAB"]["A"] = 33.628906;
patch["TargetLAB"]["B"] = -6.617188;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "Q18";
patch["HTMLColor"] = "#492f2f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 22.492341;
patch["TargetLAB"]["A"] = 12.304688;
patch["TargetLAB"]["B"] = 4.570312;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "Q19";
patch["HTMLColor"] = "#6f3163"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 30.716912;
patch["TargetLAB"]["A"] = 33.117188;
patch["TargetLAB"]["B"] = -16.425781;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "Q20";
patch["HTMLColor"] = "#322c50"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 20.030637;
patch["TargetLAB"]["A"] = 10.398438;
patch["TargetLAB"]["B"] = -21.644531;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "Q21";
patch["HTMLColor"] = "#b67f6e"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 58.748468;
patch["TargetLAB"]["A"] = 20.359375;
patch["TargetLAB"]["B"] = 18.265625;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "R1";
patch["HTMLColor"] = "#00a85a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 58.789828;
patch["TargetLAB"]["A"] = -66.796875;
patch["TargetLAB"]["B"] = 26.664062;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "R2";
patch["HTMLColor"] = "#787775"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 50.090380;
patch["TargetLAB"]["A"] = -0.054688;
patch["TargetLAB"]["B"] = 1.535156;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "R3";
patch["HTMLColor"] = "#30232a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 15.580576;
patch["TargetLAB"]["A"] = 7.363281;
patch["TargetLAB"]["B"] = -2.121094;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "R4";
patch["HTMLColor"] = "#fdc376"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 83.069853;
patch["TargetLAB"]["A"] = 14.882812;
patch["TargetLAB"]["B"] = 46.925781;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "R5";
patch["HTMLColor"] = "#cb8243"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 61.211703;
patch["TargetLAB"]["A"] = 24.726562;
patch["TargetLAB"]["B"] = 45.378906;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "R6";
patch["HTMLColor"] = "#c06b3f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 54.662990;
patch["TargetLAB"]["A"] = 31.679688;
patch["TargetLAB"]["B"] = 39.460938;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "R7";
patch["HTMLColor"] = "#5e6239"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 40.551471;
patch["TargetLAB"]["A"] = -7.019531;
patch["TargetLAB"]["B"] = 23.152344;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "R8";
patch["HTMLColor"] = "#fff21a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 95.094975;
patch["TargetLAB"]["A"] = -6.410156;
patch["TargetLAB"]["B"] = 89.738281;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "R9";
patch["HTMLColor"] = "#696525"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 42.195159;
patch["TargetLAB"]["A"] = -5.207031;
patch["TargetLAB"]["B"] = 35.746094;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "R10";
patch["HTMLColor"] = "#426437"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 38.835784;
patch["TargetLAB"]["A"] = -19.890625;
patch["TargetLAB"]["B"] = 21.484375;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "R11";
patch["HTMLColor"] = "#5b337b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 29.174326;
patch["TargetLAB"]["A"] = 29.367188;
patch["TargetLAB"]["B"] = -34.277344;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "R12";
patch["HTMLColor"] = "#5f6992"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 44.784007;
patch["TargetLAB"]["A"] = 4.113281;
patch["TargetLAB"]["B"] = -23.781250;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "R13";
patch["HTMLColor"] = "#312e41"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 19.921875;
patch["TargetLAB"]["A"] = 5.191406;
patch["TargetLAB"]["B"] = -11.601562;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "R14";
patch["HTMLColor"] = "#533b36"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 27.671569;
patch["TargetLAB"]["A"] = 10.378906;
patch["TargetLAB"]["B"] = 7.605469;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "R15";
patch["HTMLColor"] = "#3a425e"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 28.059130;
patch["TargetLAB"]["A"] = 2.578125;
patch["TargetLAB"]["B"] = -17.785156;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "R16";
patch["HTMLColor"] = "#1e2b2c"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 16.461397;
patch["TargetLAB"]["A"] = -5.578125;
patch["TargetLAB"]["B"] = -2.500000;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "R17";
patch["HTMLColor"] = "#232228"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 13.564645;
patch["TargetLAB"]["A"] = 1.812500;
patch["TargetLAB"]["B"] = -3.906250;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "R18";
patch["HTMLColor"] = "#797475"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 49.181985;
patch["TargetLAB"]["A"] = 2.320312;
patch["TargetLAB"]["B"] = -0.179688;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "R19";
patch["HTMLColor"] = "#cfc126"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 77.406556;
patch["TargetLAB"]["A"] = -6.429688;
patch["TargetLAB"]["B"] = 71.347656;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "R20";
patch["HTMLColor"] = "#ddcb53"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 81.432292;
patch["TargetLAB"]["A"] = -3.789062;
patch["TargetLAB"]["B"] = 59.855469;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "R21";
patch["HTMLColor"] = "#36625d"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 38.235294;
patch["TargetLAB"]["A"] = -16.828125;
patch["TargetLAB"]["B"] = -2.171875;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "S1";
patch["HTMLColor"] = "#7e3448"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 32.984069;
patch["TargetLAB"]["A"] = 34.300781;
patch["TargetLAB"]["B"] = 4.789062;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "S2";
patch["HTMLColor"] = "#5f5e43"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 39.316789;
patch["TargetLAB"]["A"] = -3.117188;
patch["TargetLAB"]["B"] = 15.656250;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "S3";
patch["HTMLColor"] = "#3f535b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 33.926164;
patch["TargetLAB"]["A"] = -6.726562;
patch["TargetLAB"]["B"] = -7.148438;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "S4";
patch["HTMLColor"] = "#527a36"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 46.911765;
patch["TargetLAB"]["A"] = -24.113281;
patch["TargetLAB"]["B"] = 32.070312;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "S5";
patch["HTMLColor"] = "#68704a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 45.692402;
patch["TargetLAB"]["A"] = -8.015625;
patch["TargetLAB"]["B"] = 19.707031;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "S6";
patch["HTMLColor"] = "#84b4c9"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 70.430453;
patch["TargetLAB"]["A"] = -12.347656;
patch["TargetLAB"]["B"] = -16.394531;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "S7";
patch["HTMLColor"] = "#331f1c"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 14.327512;
patch["TargetLAB"]["A"] = 10.019531;
patch["TargetLAB"]["B"] = 6.390625;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "S8";
patch["HTMLColor"] = "#596c66"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 43.780637;
patch["TargetLAB"]["A"] = -8.335938;
patch["TargetLAB"]["B"] = 0.792969;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "S9";
patch["HTMLColor"] = "#bb2d31"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 42.930453;
patch["TargetLAB"]["A"] = 56.519531;
patch["TargetLAB"]["B"] = 34.539062;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "S10";
patch["HTMLColor"] = "#deadb3"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 75.376838;
patch["TargetLAB"]["A"] = 19.238281;
patch["TargetLAB"]["B"] = 4.050781;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "S11";
patch["HTMLColor"] = "#2f686c"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 40.510110;
patch["TargetLAB"]["A"] = -18.617188;
patch["TargetLAB"]["B"] = -8.250000;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "S12";
patch["HTMLColor"] = "#694f5a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 36.502757;
patch["TargetLAB"]["A"] = 12.781250;
patch["TargetLAB"]["B"] = -2.195312;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "S13";
patch["HTMLColor"] = "#282632"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 15.733762;
patch["TargetLAB"]["A"] = 3.589844;
patch["TargetLAB"]["B"] = -7.636719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "S14";
patch["HTMLColor"] = "#beb4d9"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 75.027574;
patch["TargetLAB"]["A"] = 8.960938;
patch["TargetLAB"]["B"] = -17.339844;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "S15";
patch["HTMLColor"] = "#746e2c"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 45.831801;
patch["TargetLAB"]["A"] = -4.027344;
patch["TargetLAB"]["B"] = 36.554688;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "S16";
patch["HTMLColor"] = "#44242d"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 18.906250;
patch["TargetLAB"]["A"] = 16.671875;
patch["TargetLAB"]["B"] = 1.125000;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "S17";
patch["HTMLColor"] = "#1a4158"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 25.675551;
patch["TargetLAB"]["A"] = -7.933594;
patch["TargetLAB"]["B"] = -17.996094;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "S18";
patch["HTMLColor"] = "#4c526f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 35.254289;
patch["TargetLAB"]["A"] = 3.253906;
patch["TargetLAB"]["B"] = -17.589844;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "S19";
patch["HTMLColor"] = "#33a6cc"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 63.256740;
patch["TargetLAB"]["A"] = -23.210938;
patch["TargetLAB"]["B"] = -28.847656;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "S20";
patch["HTMLColor"] = "#3a5178"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 33.998162;
patch["TargetLAB"]["A"] = -0.125000;
patch["TargetLAB"]["B"] = -25.425781;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "S21";
patch["HTMLColor"] = "#211a1b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 10.261949;
patch["TargetLAB"]["A"] = 3.828125;
patch["TargetLAB"]["B"] = 0.816406;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "T1";
patch["HTMLColor"] = "#bd5b32"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 50.738358;
patch["TargetLAB"]["A"] = 38.453125;
patch["TargetLAB"]["B"] = 42.269531;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "T2";
patch["HTMLColor"] = "#192c23"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 16.279105;
patch["TargetLAB"]["A"] = -10.417969;
patch["TargetLAB"]["B"] = 3.511719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "T3";
patch["HTMLColor"] = "#53645b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 40.768995;
patch["TargetLAB"]["A"] = -8.390625;
patch["TargetLAB"]["B"] = 2.984375;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "T4";
patch["HTMLColor"] = "#125357"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 31.764706;
patch["TargetLAB"]["A"] = -19.359375;
patch["TargetLAB"]["B"] = -8.191406;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "T5";
patch["HTMLColor"] = "#b793a6"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 64.849877;
patch["TargetLAB"]["A"] = 16.078125;
patch["TargetLAB"]["B"] = -4.531250;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "T6";
patch["HTMLColor"] = "#c3698e"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 55.903799;
patch["TargetLAB"]["A"] = 39.992188;
patch["TargetLAB"]["B"] = -3.769531;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "T7";
patch["HTMLColor"] = "#097b92"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 46.870404;
patch["TargetLAB"]["A"] = -23.324219;
patch["TargetLAB"]["B"] = -21.136719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "T8";
patch["HTMLColor"] = "#416da2"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 44.736520;
patch["TargetLAB"]["A"] = -2.761719;
patch["TargetLAB"]["B"] = -33.882812;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "T9";
patch["HTMLColor"] = "#533746"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 26.801471;
patch["TargetLAB"]["A"] = 14.835938;
patch["TargetLAB"]["B"] = -4.179688;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "T10";
patch["HTMLColor"] = "#8c6546"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 46.341912;
patch["TargetLAB"]["A"] = 13.375000;
patch["TargetLAB"]["B"] = 24.250000;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "T11";
patch["HTMLColor"] = "#65402c"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 31.326593;
patch["TargetLAB"]["A"] = 14.800781;
patch["TargetLAB"]["B"] = 19.214844;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "T12";
patch["HTMLColor"] = "#16201b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 11.380208;
patch["TargetLAB"]["A"] = -5.882812;
patch["TargetLAB"]["B"] = 2.234375;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "T13";
patch["HTMLColor"] = "#7faada"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 67.735907;
patch["TargetLAB"]["A"] = -5.457031;
patch["TargetLAB"]["B"] = -29.320312;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "T14";
patch["HTMLColor"] = "#1e252c"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 14.296875;
patch["TargetLAB"]["A"] = -1.820312;
patch["TargetLAB"]["B"] = -5.632812;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "T15";
patch["HTMLColor"] = "#491d40"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 18.776042;
patch["TargetLAB"]["A"] = 25.023438;
patch["TargetLAB"]["B"] = -12.390625;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "T16";
patch["HTMLColor"] = "#2c5040"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 31.115196;
patch["TargetLAB"]["A"] = -16.878906;
patch["TargetLAB"]["B"] = 5.367188;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "T17";
patch["HTMLColor"] = "#3e2925"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 19.131434;
patch["TargetLAB"]["A"] = 9.562500;
patch["TargetLAB"]["B"] = 6.750000;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "T18";
patch["HTMLColor"] = "#663e35"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 31.037071;
patch["TargetLAB"]["A"] = 16.949219;
patch["TargetLAB"]["B"] = 13.621094;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "T19";
patch["HTMLColor"] = "#36357a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 25.404412;
patch["TargetLAB"]["A"] = 17.144531;
patch["TargetLAB"]["B"] = -40.324219;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "T20";
patch["HTMLColor"] = "#742753"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 29.016544;
patch["TargetLAB"]["A"] = 37.605469;
patch["TargetLAB"]["B"] = -8.527344;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "T21";
patch["HTMLColor"] = "#222d21"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 16.956189;
patch["TargetLAB"]["A"] = -7.179688;
patch["TargetLAB"]["B"] = 6.183594;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "U1";
patch["HTMLColor"] = "#7c2e77"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 33.144914;
patch["TargetLAB"]["A"] = 41.445312;
patch["TargetLAB"]["B"] = -25.484375;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "U2";
patch["HTMLColor"] = "#00a980"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 59.526654;
patch["TargetLAB"]["A"] = -62.628906;
patch["TargetLAB"]["B"] = 7.433594;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "U3";
patch["HTMLColor"] = "#eb3137"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 52.832414;
patch["TargetLAB"]["A"] = 69.550781;
patch["TargetLAB"]["B"] = 44.914062;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "U4";
patch["HTMLColor"] = "#465929"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 35.442708;
patch["TargetLAB"]["A"] = -14.242188;
patch["TargetLAB"]["B"] = 25.027344;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "U5";
patch["HTMLColor"] = "#89acad"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 67.732843;
patch["TargetLAB"]["A"] = -12.257812;
patch["TargetLAB"]["B"] = -4.667969;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "U6";
patch["HTMLColor"] = "#3f3237"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 22.458640;
patch["TargetLAB"]["A"] = 6.433594;
patch["TargetLAB"]["B"] = -1.054688;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "U7";
patch["HTMLColor"] = "#716565"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 43.844975;
patch["TargetLAB"]["A"] = 5.230469;
patch["TargetLAB"]["B"] = 1.914062;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "U8";
patch["HTMLColor"] = "#007685"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 44.419424;
patch["TargetLAB"]["A"] = -26.300781;
patch["TargetLAB"]["B"] = -17.250000;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "U9";
patch["HTMLColor"] = "#557277"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 46.014093;
patch["TargetLAB"]["A"] = -10.218750;
patch["TargetLAB"]["B"] = -5.941406;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "U10";
patch["HTMLColor"] = "#8b2634"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 32.562806;
patch["TargetLAB"]["A"] = 43.332031;
patch["TargetLAB"]["B"] = 17.285156;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "U11";
patch["HTMLColor"] = "#385a69"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 36.107537;
patch["TargetLAB"]["A"] = -9.492188;
patch["TargetLAB"]["B"] = -12.562500;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "U12";
patch["HTMLColor"] = "#22222f"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 13.875613;
patch["TargetLAB"]["A"] = 2.500000;
patch["TargetLAB"]["B"] = -8.160156;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "U13";
patch["HTMLColor"] = "#974756"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 41.012561;
patch["TargetLAB"]["A"] = 35.398438;
patch["TargetLAB"]["B"] = 7.734375;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "U14";
patch["HTMLColor"] = "#b98755"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 60.592831;
patch["TargetLAB"]["A"] = 15.371094;
patch["TargetLAB"]["B"] = 34.636719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "U15";
patch["HTMLColor"] = "#00bff2"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 70.791973;
patch["TargetLAB"]["A"] = -31.238281;
patch["TargetLAB"]["B"] = -38.355469;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "U16";
patch["HTMLColor"] = "#72d1f6"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 78.962929;
patch["TargetLAB"]["A"] = -21.675781;
patch["TargetLAB"]["B"] = -27.386719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "U17";
patch["HTMLColor"] = "#1d261a"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 14.031863;
patch["TargetLAB"]["A"] = -6.027344;
patch["TargetLAB"]["B"] = 6.386719;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "U18";
patch["HTMLColor"] = "#523c69"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 29.491422;
patch["TargetLAB"]["A"] = 17.113281;
patch["TargetLAB"]["B"] = -22.804688;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "U19";
patch["HTMLColor"] = "#341f23"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 14.644608;
patch["TargetLAB"]["A"] = 11.402344;
patch["TargetLAB"]["B"] = 1.972656;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "U20";
patch["HTMLColor"] = "#40426c"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 29.154412;
patch["TargetLAB"]["A"] = 8.257812;
patch["TargetLAB"]["B"] = -25.203125;
setpatch(random331samples["RefBase"], patch);
patch["SampleName"] = "U21";
patch["HTMLColor"] = "#083a2b"
patch["TargetLAB"] = new Object()
patch["TargetLAB"]["L"] = 21.004902;
patch["TargetLAB"]["A"] = -20.230469;
patch["TargetLAB"]["B"] = 4.773438;
setpatch(random331samples["RefBase"], patch);

//
// Quick sanity check
//
if (random331samples["Size"] != random331samples["NumberOfRows"] * random331samples["NumberOfColumns"]) {
    alert("Chart specification error in Size vs Number Of Columns * Number Of Rows");
}
if (random331samples["RowNames"].length != random331samples["NumberOfRows"]) {
    alert("Chart specification error in Row Names vs Number Of Rows");
}
