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
        mpatch = new Patch();
        mpatch.CiexyzX = XYZResults["X"].toFixed(2);
        mpatch.CiexyzY = XYZResults["Y"].toFixed(2);
        mpatch.CiexyzZ = XYZResults["Z"].toFixed(2);
        mpatch.CielabL = LABResults["L"].toFixed(2);
        mpatch.CielabA = LABResults["A"].toFixed(2);
        mpatch.CielabB = LABResults["B"].toFixed(2);
        mpatch.SampleName = mychart["ReferencePatch"][ind]["SampleName"];
        mpatch.PatchTypeID = mychart["ReferencePatch"][ind]["PatchTypeID"];
        MeasuredPatches.push(mpatch);
    }

    jQuery("[id*='lblMessage']").css('color', 'Black');
    jQuery("[id*='lblMessage']").html("Status : Measurement Complete.");
    jQuery("[id*='btnMeasureAndUploadChart']").val('Upload Measurement');
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
    var chartWidth = parseInt(jQuery("[id*='Canvas']").width());
    var chartHeight = parseInt(jQuery("[id*='Canvas']").height());
    var columnCount = mychart["NumberOfColumns"];
    var rowCount = mychart["NumberOfRows"];
    var cellSize = Math.min((chartWidth / columnCount), (chartHeight / rowCount));
    prog["Prompt"] = "Reading progress " + samples + " samples";
    prog["Answers"] = "NONE";
    jQuery("[id*='lblMessage']").css('color', 'Black');
    jQuery("[id*='lblMessage']").html("Status : " + prog["Prompt"]);
    var jg = new jsGraphics(document.getElementById("Canvas").id);
    var cursample = 0;
    var x = 0;
    var y = 0;
    for (cursample = 0; cursample < samples; cursample++) {
        var srgb = result[cursample]["sRGB"];
        var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]);
        jg.setColor(rgbx);
        jg.fillRect(x * cellSize, y * cellSize, cellSize, cellSize);
        jg.paint();
        x++;
        if (x >= columnCount) {
            x = 0;
            y++;
        }

    }
    return "OK";
}