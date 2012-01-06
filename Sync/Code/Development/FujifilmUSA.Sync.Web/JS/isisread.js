// This function presents the iSis read query.  We do this
// in javascript so the web app can change everything including
// the language of the query.
function presentChartDialogiSis(scanner, chart, query) {
    if (query["Request"] == "READACKNOWLEDGED") {        
        query["Prompt"] = "Chart read in progress";
    } else {        
        query["Prompt"] = "Please place the chart in the scanner and click in this window.";
    }
    jQuery("[id*='lblMessage']").css('color', 'Black');
    jQuery("[id*='lblMessage']").html("Status : " + query["Prompt"]);
}
var area = new Object();
area["W"] = 0;
area["H"] = 0;

var distance = new Object();
distance["R"] = 0;
distance["C"] = 0;

var drawCells = 0;
var cId = 0;
var x1 = 0;
var y1 = 0;

function presentScanDoneiSis(scanner, mychart, final) {
    // Before we finalize the request, get all the values from
    // the strip read and present them to the user.
    // This is a syncronous operation since the data is 
    // stored and ready to go.
    // The samples object is the same object that describes the
    // chart and expected results, but with the values we actually
    // read added.
    if (typeof mychart["Result"] == "string") {
        // This type of query means we finish up with an error, so just present the
        // error
        if (!(mychart["Result"] == "OK" || mychart["Result"] == "CANCELLED")) {
            MeasurementDialog(mychart["Result"], "Error");
        }
        if (typeof final == "string") {
            eval(final);
        } else if (typeof final == "object") {
            final();
        }
        return;
    }
    //
    // Now let the user know how things ended up.
    //
    var newHTML = "";
    newHTML += "<UL>";
    var results = mychart["Result"];
    var samples = results.length;
    var width = mychart["NumberOfColumns"];
    var expected = mychart["NumberOfRows"] * width;
    if (results.length != expected) {
        MeasurementDialog("Failed to read expected number of samples(" + expected + " != " + results.length + ")", "Error");
        if (typeof final == "string") {
            eval(final);
        } else if (typeof final == "object") {
            final();
        }
        return;
    }

    for (ind = 0; ind < samples; ind++) {
        var LABResults = results[ind]["CIELab"];
        var XYZResults = results[ind]["CIEXYZ"];
        var DensityResults = results[ind]["Density"];
        var sName = mychart["ReferencePatch"][ind]["SampleName"];
        var PatchTypeID = mychart["ReferencePatch"][ind]["PatchTypeID"];
        var CIELab = new LAB(LABResults["L"].toFixed(2), LABResults["A"].toFixed(2), LABResults["B"].toFixed(2));
        var CIEXYZ = new XYZ(XYZResults["X"].toFixed(2), XYZResults["Y"].toFixed(2), XYZResults["Z"].toFixed(2));
        var _Density = new Density(DensityResults[0].toFixed(5), DensityResults[1].toFixed(5), DensityResults[2].toFixed(5), DensityResults[3].toFixed(5));        
        mpatch = new Patch();
        mpatch.LAB = LABResults;
        mpatch.XYZ = XYZResults;
        mpatch.Density = _Density;
        mpatch.SampleName = sName;
        mpatch.CMYK_C = mychart["ReferencePatch"][ind]["CMYK_C"];
        mpatch.CMYK_M = mychart["ReferencePatch"][ind]["CMYK_M"];
        mpatch.CMYK_Y = mychart["ReferencePatch"][ind]["CMYK_Y"];
        mpatch.CMYK_K = mychart["ReferencePatch"][ind]["CMYK_K"];
        mpatch.PatchTypeID = mychart["ReferencePatch"][ind]["PatchTypeID"];
        MeasuredPatches.push(mpatch);
    }



    // From here on down, we are done with the reading of the strips.
    // We can now present the final results to the user.
    // Lets draw our sampled colors over the top of the displayed
    // image.
    //
    // Add an upload button and canvas
    //

    jQuery("[id*='lblMessage']").css('color', 'Black');
    jQuery("[id*='lblMessage']").html("Status : Measurement Complete.");
    jQuery("[id*='btnMeasureAndUploadChart']").val('Upload Measurement');


    var w = parseInt(jQuery("[id*='Canvas']").width());
    var h = parseInt(jQuery("[id*='Canvas']").height());
    var startPosition = 1;
    col = mychart["NumberOfColumns"];
    row = mychart["NumberOfRows"];
    area["H"] = (((h / row) / 2) / 2) + '';
    distance["R"] = (((h / row) / 2) / 2) + '';
    area["W"] = ((w / col) / 2) + '';
    distance["C"] = ((w / col) / 2) + '';
    var noOfCells = mychart["Size"];
    if (y1 == 0) {
        y1 = parseInt(area["H"]);
    }
    y1 = y1 + (parseInt(area["H"])) + parseInt(distance["R"] * 2);

    x1 = 0;
    col = results.length;
    for (j = 0; j < col; j++) {

        if (noOfCells > drawCells) {

            x1 = x1 + parseInt(area["W"]) + parseInt(distance["C"]);

            var xx1 = startPosition + x1;
            var yy1 = startPosition + y1;

            //var jg2 = new jsGraphics("Canvas");
            var jg2 = new jsGraphics(document.getElementById("Canvas").id); 

            var srgb = results[j]["sRGB"];
            var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]);
            jg2.setColor(rgbx);
            jg2.fillRect(xx1, yy1, parseInt(area["W"]), parseInt(area["H"]));
            jg2.paint();
            drawCells++;
        }
    }


    //    var jg = new jsGraphics("Canvas");    // Use the "Canvas" div for drawing
    //    var cursample = 0;
    //    var x = 0;
    //    var y = 0;
    //    for (cursample = 0; cursample < samples; cursample++) {
    //        var srgb = results[cursample]["sRGB"];
    //        var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]);
    //        jg.setColor(rgbx);
    //        jg.fillRect(x * 20, y * 20, 20, 20);
    //        x++;
    //        if (x >= width) {
    //            x = 0;
    //            y++;
    //        }
    //    }
    //    jg.paint();

    // We have to make one more call to the library.  The 
    // calibrate itself ran in a non-browser thread.  We need
    // the browser thread to join with the calibrate thread to
    // keep the process state tidy.
    if (typeof final == "string") {
        eval(final);
    } else if (typeof final == "object") {
        final();
    }
}
function presentProgressiSis(scanner, mychart) {
    console.log("In presentProgress");
    var result = mychart["Result"];
    var samples = result.length;
    var prog = new Object();
    var width = mychart["NumberOfColumns"];
    var height = mychart["NumberOfRows"];
    var lines = samples / width;
    prog["Prompt"] = "Reading progress " + samples + " samples";
    prog["Answers"] = "NONE";
    jQuery("[id*='lblMessage']").css('color', 'Black');
    jQuery("[id*='lblMessage']").html("Status : " + prog["Prompt"]);
    
//    var jg = new jsGraphics("Canvas");    // Use the "Canvas" div for drawing
//    var cursample = 0;
//    var x = 0;
//    var y = 0;
//    for (cursample = 0; cursample < samples; cursample++) {
//        var srgb = result[cursample]["sRGB"];
//        var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]);
//        jg.setColor(rgbx);
//        jg.fillRect(x * 20, y * 20, 20, 20);
//        x++;
//        if (x >= width) {
//            x = 0;
//            y++;
//        }
//    }
//    jg.paint();
    return "OK";
}