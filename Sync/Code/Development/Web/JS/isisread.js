// This function presents the iSis read query.  We do this
// in javascript so the web app can change everything including
// the language of the query.
function presentChartDialogiSis(scanner, chart, query) {
    if (query["Request"] == "READACKNOWLEDGED") {
        query["Prompt"] = "<B><CENTER>Chart read in progress</CENTER></B>";
    } else {
        query["Prompt"] = "<B><CENTER>Please place the chart in the scanner<BR>and click in this window.</CENTER></B>";
    }
    presentQuery(query);
}
function presentScanDoneiSis(scanner, mychart, final) {
    // make sure the popup window is gone
    RemoveModalWindow();
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
            alert(mychart["Result"]);
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
        alert("Failed to read expected number of samples(" + expected + " != " + results.length + ")");
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
    jQuery('#btnUploadData').css('display', '');

    var jg = new jsGraphics("Canvas");    // Use the "Canvas" div for drawing
    var cursample = 0;
    var x = 0;
    var y = 0;
    for (cursample = 0; cursample < samples; cursample++) {
        var srgb = results[cursample]["sRGB"];
        var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]);
        jg.setColor(rgbx);
        jg.fillRect(x * 20, y * 20, 20, 20);
        x++;
        if (x >= width) {
            x = 0;
            y++;
        }
    }
    jg.paint();

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
    prog["Prompt"] = "<CENTER>Reading progress " + samples + " samples</CENTER>";
    prog["Answers"] = "NONE";
    presentQuery(prog);

    var jg = new jsGraphics("Canvas");    // Use the "Canvas" div for drawing
    var cursample = 0;
    var x = 0;
    var y = 0;
    for (cursample = 0; cursample < samples; cursample++) {
        var srgb = result[cursample]["sRGB"];
        var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]);
        jg.setColor(rgbx);
        jg.fillRect(x * 20, y * 20, 20, 20);
        x++;
        if (x >= width) {
            x = 0;
            y++;
        }
    }
    jg.paint();
    return "OK";
}