
if (!window.console) console = {}; console.log = console.log || function () { }; console.warn = console.warn || function () { }; console.error = console.error || function () { }; console.info = console.info || function () { }; OSName = "Unknown OS"; if (navigator.appVersion.indexOf("Win") != -1) OSName = "Windows"; if (navigator.appVersion.indexOf("Mac") != -1) OSName = "MacOS"; if (navigator.appVersion.indexOf("X11") != -1) OSName = "UNIX"; if (navigator.appVersion.indexOf("Linux") != -1) OSName = "Linux"; if (!Array.prototype.indexOf) {
    Array.prototype.indexOf = function (obj, fromIndex) {
        if (fromIndex == null) { fromIndex = 0; } else if (fromIndex < 0) { fromIndex = Math.max(0, this.length + fromIndex); }
        for (var i = fromIndex, j = this.length; i < j; i++) {
            if (this[i] === obj)
                return i;
        }
        return -1;
    };
}
console.log("Loading xychartread.js"); var g_Chart = new Object(); MeasuredPatches = []; function LAB(L, A, B) { this.L = L; this.A = A; this.B = B; }
function XYZ(X, Y, Z) { this.X = X; this.Y = Y; this.Z = Z; }
function Density(Red, Green, Blue, Visual) { this.Red = Red; this.Green = Green; this.Blue = Blue; this.Visual = Visual; }
function Patch(LAB, XYZ, Density, sName) { this.LAB = LAB; this.XYZ = XYZ; this.Density = Density; this.SampleName = sName; }
paper = new Array(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0); function dec2hex(i) {
    var result = "00"; if (i >= 0 && i <= 15) { result = "0" + i.toString(16); }
    else if (i >= 16 && i <= 255) { result = i.toString(16); }
    return result;
}
function plugin0() { return document.getElementById('plugin0'); }
plugin = plugin0; function addEvent(obj, name, func) { if (window.addEventListener) { obj.addEventListener(name, func, false); } else { obj.attachEvent("on" + name, func); } }
if (/msie/i.test(navigator.userAgent)) {
    document.nativeGetElementById = document.getElementById; document.getElementById = function (id) {
        var elem = document.nativeGetElementById(id); if (elem) {
            if (elem.id == id) { return elem; }
            else { for (var i = 1; i < document.all[id].length; i++) { if (document.all[id][i].id == id) { return document.all[id][i]; } } } 
        }
        return null;
    };
}
function load() { addEvent(plugin(), 'fired', function (x) { alert('onfired() from load(): ' + x) }); console.log("Loading plugin version " + plugin().plugin_version); console.log("GUI Version 1.7"); plugin().setDebug(10240); var newHTML = '<B>Fuji Color Manager Plugin Version ' + plugin().plugin_version + '</B>'; document.getElementById('ApagoCMVersion').innerHTML = newHTML; SDKVerHTML = plugin().sdk_version; document.getElementById('SDKVersion').innerHTML = SDKVerHTML; }
function pluginLoaded() { var newHTML = '<B>Fuji Color Manager Plugin Version ' + plugin().plugin_version + '</B>'; document.getElementById('ApagoCMVersion').innerHTML = newHTML; SDKVerHTML = plugin().sdk_version; document.getElementById('SDKVersion').innerHTML = SDKVerHTML; }
function addTestEvent() { addEvent(plugin(), 'fired', function (x) { alert('onfired(): ' + x) }); }
function testEvent() { plugin().testEvent('test stuff'); }
function presentQuery(question) { OnQuery(question); }
function presentCalibrateDialog(scanner, myQuery) {
    if (myQuery["Request"] == "CALIBRATEACKNOWLEDGED") { RemoveModalWindow(); }
    myQuery["Prompt"] = myQuery[myQuery["Request"]]; presentQuery(myQuery);
}
function presentCalibrateDone(scanner, msg, final) {
    RemoveModalWindow(); if (typeof final != "undefined") { eval(final); }
    if (typeof msg == "string") { if (!(msg == "OK" || msg == "CANCELLED")) { alert(msg); } } else { alert("Calibration Successful"); } 
}
function calibrateDevice(scanner) {
    var myQuery = new Object(); myQuery["CALIBRATEINSTRUCTIONS"] = "<B><CENTER>Please place i1Pro in the calibration cradle<BR>and press press the i1 trigger<BR>or click anywhere in this window</CENTER></B>"; myQuery["CALIBRATEACKNOWLEDGED"] = "<B><CENTER>Calibration In Progress</CENTER></B>"; scanner["Mode"] = "Both"
    scanner["SampleSource"] = "Paper"; var result = plugin().calibrateDevice(scanner, myQuery, presentCalibrateDialog, presentCalibrateDone); if (result != "OK") { alert("Scanning Device has been unplugged"); } 
}
function presentChartDialog(scanner, chart, query) {
    if (query["Request"] == "READACKNOWLEDGED") { query["Prompt"] = "<B><CENTER>Chart read in progress</CENTER></B>"; } else { query["Prompt"] = "<B><CENTER>Please place the chart in the scanner<BR>and click in this window.</CENTER></B>"; }
    presentQuery(query);
}
function presentStripDialog(scanner, chart, query) {
    if (query["Request"] == "READACKNOWLEDGED") { query["Prompt"] = "<B><CENTER>Strip read in progress</CENTER></B>"; } else { if (scanner["Type"] == "iO") { query["Prompt"] = "<B><CENTER>Please place the chart on the platten and click to continue.</CENTER></B>"; } else { query["Prompt"] = "<B><CENTER>Please place the scanner over the strip " + chart["CurrentStrip"] + ".<BR>press and hold the trigger while sliding the scanner across the strip.</CENTER></B>"; } }
    presentQuery(query);
}
function presentScanDoneiSis(scanner, mychart, final) {
    RemoveModalWindow(); if (typeof mychart["Result"] == "string") {
        if (!(mychart["Result"] == "OK" || mychart["Result"] == "CANCELLED")) { alert(mychart["Result"]); }
        if (typeof final == "string") { eval(final); } else if (typeof final == "object") { final(); }
        return;
    }
    var newHTML = ""; newHTML += "<UL>"; var results = mychart["Result"]; var samples = results.length; var width = mychart["NumberOfColumns"]; var expected = mychart["NumberOfRows"] * width; if (results.length != expected) {
        alert("Failed to read expected number of samples(" + expected + " != " + results.length + ")"); if (typeof final == "string") { eval(final); } else if (typeof final == "object") { final(); }
        return;
    }
    for (ind = 0; ind < samples; ind++) { var LABResults = results[ind]["CIELab"]; var XYZResults = results[ind]["CIEXYZ"]; var DensityResults = results[ind]["Density"]; var sName = mychart["ReferencePatch"][ind]["SampleName"]; var PatchTypeID = mychart["ReferencePatch"][ind]["PatchTypeID"]; var CIELab = new LAB(LABResults["L"].toFixed(2), LABResults["A"].toFixed(2), LABResults["B"].toFixed(2)); var CIEXYZ = new XYZ(XYZResults["X"].toFixed(2), XYZResults["Y"].toFixed(2), XYZResults["Z"].toFixed(2)); var _Density = new Density(DensityResults[0].toFixed(5), DensityResults[1].toFixed(5), DensityResults[2].toFixed(5), DensityResults[3].toFixed(5)); mpatch = new Patch(); mpatch.LAB = LABResults; mpatch.XYZ = XYZResults; mpatch.Density = _Density; mpatch.SampleName = sName; mpatch.CMYK_C = mychart["ReferencePatch"][ind]["CMYK_C"]; mpatch.CMYK_M = mychart["ReferencePatch"][ind]["CMYK_M"]; mpatch.CMYK_Y = mychart["ReferencePatch"][ind]["CMYK_Y"]; mpatch.CMYK_K = mychart["ReferencePatch"][ind]["CMYK_K"]; mpatch.PatchTypeID = mychart["ReferencePatch"][ind]["PatchTypeID"]; MeasuredPatches.push(mpatch); }
    var newHTML = '<A HREF="#" onclick="javascript:upload()">Upload Data</A><BR>'; newHTML += '<DIV ID="Canvas" style="position:relative;height:450px;width:440px;">'; newHTML += '</DIV>'; var span = "UploadData" + scanner["Name"]; document.getElementById(span).innerHTML = newHTML; var jg = new jsGraphics("Canvas"); var cursample = 0; var x = 0; var y = 0; for (cursample = 0; cursample < samples; cursample++) { var srgb = results[cursample]["sRGB"]; var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]); jg.setColor(rgbx); jg.fillRect(x * 20, y * 20, 20, 20); x++; if (x >= width) { x = 0; y++; } }
    jg.paint(); if (typeof final == "string") { eval(final); } else if (typeof final == "object") { final(); } 
}
function presentScanDone(scanner, mychart, final) {
    RemoveModalWindow(); if (mychart["Size"] == MeasuredPatches.length) return; if (typeof mychart["Result"] == "string") {
        if (!(mychart["Result"] == "OK" || mychart["Result"] == "CANCELLED")) { alert(mychart["Result"]); }
        if (typeof final == "string") { eval(final); } else if (typeof final == "object") { final(); }
        if (mychart["Result"] != "CANCELLED") { var query = new Object(); query["Success"] = "Samples Complete"; query["Fail"] = "Failed to read samples"; var result = plugin().read(scanner, mychart, query, presentStripDialog, presentProgress, presentScanDone); if (result != "OK") { alert("Scanning Device has been unplugged"); } }
        return;
    }
    var newHTML = ""; newHTML += "<UL>"; var results = mychart["Result"]; var idx = mychart["NumberOfRows"]; if (!(scanner["Type"] == "iO" || scanner["Type"] == "iSis")) {
        if (results.length != mychart["NumberOfColumns"]) {
            alert("Failed to read expected number of samples" + results.length + " vs " + mychart["NumberOfColumns"]); if (typeof final == "string") { eval(final); } else if (typeof final == "object") { final(); }
            var query = new Object(); query["Success"] = "Samples Complete"; query["Fail"] = "Failed to read samples"; mychart["Result"] = null; var result = plugin().read(scanner, mychart, query, presentStripDialog, presentProgress, presentScanDone); if (result != "OK") { alert("Scanning Device has been unplugged"); }
            return;
        }
        var idx; var curstr = mychart["CurrentStrip"]; console.log("Looking for strip " + curstr); idx = mychart["RowNames"].indexOf(curstr); if (idx < 0 || idx > mychart["NumberOfRows"]) { alert("ERROR: Cannot determine current read column"); return; } 
    }
    if (typeof final == "string") { eval(final); } else if (typeof final == "object") { final(); }
    if (typeof g_Chart["Result"] == "undefined") { g_Chart["Result"] = new Array(); }
    g_Chart["Result"] = g_Chart["Result"].concat(results); for (ind = 0; ind < g_Chart["NumberOfColumns"]; ind++) { var LABResults = results[ind]["CIELab"]; var XYZResults = results[ind]["CIEXYZ"]; var DensityResults = results[ind]["Density"]; var sName = mychart["ReferencePatch"][ind]["SampleName"]; var PatchTypeID = mychart["ReferencePatch"][ind]["PatchTypeID"]; var CIELab = new LAB(LABResults["L"].toFixed(2), LABResults["A"].toFixed(2), LABResults["B"].toFixed(2)); var CIEXYZ = new XYZ(XYZResults["X"].toFixed(2), XYZResults["Y"].toFixed(2), XYZResults["Z"].toFixed(2)); var _Density = new Density(DensityResults[0].toFixed(5), DensityResults[1].toFixed(5), DensityResults[2].toFixed(5), DensityResults[3].toFixed(5)); mpatch = new Patch(); mpatch.LAB = LABResults; mpatch.XYZ = XYZResults; mpatch.Density = _Density; mpatch.SampleName = sName; mpatch.CMYK_C = mychart["ReferencePatch"][ind]["CMYK_C"]; mpatch.CMYK_M = mychart["ReferencePatch"][ind]["CMYK_M"]; mpatch.CMYK_Y = mychart["ReferencePatch"][ind]["CMYK_Y"]; mpatch.CMYK_K = mychart["ReferencePatch"][ind]["CMYK_K"]; mpatch.PatchTypeID = mychart["ReferencePatch"][ind]["PatchTypeID"]; MeasuredPatches.push(mpatch); }
    var next = -1; if (idx < (mychart["NumberOfRows"] - 1)) { next = idx + 1; }
    if (next == 1) { var jg = new jsGraphics("Canvas"); var x = 0; var y = idx; for (var cursample = 0; cursample < results.length; cursample++) { var srgb = results[cursample]["sRGB"]; var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]); jg.setColor(rgbx); jg.fillRect(x * 20, (y * 20) + 10, 20, 10); x++; if (x >= mychart["NumberOfColumns"]) { x = 0; y++; } } } else if (next == -1) {
        var newHTML = '<A HREF="#" onclick="javascript:upload()">Upload Data</A><BR>'; newHTML += '<DIV ID="Canvas" style="position:relative;height:918px;width:440px;">'; newHTML += '</DIV>'; var span = "UploadData" + scanner["Name"]; document.getElementById(span).innerHTML = newHTML; var jg = new jsGraphics("Canvas"); var x = 0; var y = 0; for (cursample = 0; cursample < mychart["RefBase"].length; cursample++) { var rgbx = mychart["RefBase"][cursample]["HTMLColor"]; jg.setColor(rgbx); jg.fillRect(x * 20, y * 20, 20, 10); x++; if (x >= mychart["NumberOfColumns"]) { x = 0; y++; } }
        jg.paint(); x = 0; y = 0; for (var cursample = 0; cursample < g_Chart["Result"].length; cursample++) { var srgb = g_Chart["Result"][cursample]["sRGB"]; var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]); jg.setColor(rgbx); jg.fillRect(x * 20, (y * 20) + 10, 20, 10); x++; if (x >= mychart["NumberOfColumns"]) { x = 0; y++; } } 
    } else { var jg = new jsGraphics("Canvas"); var x = 0; var y = idx; for (var cursample = 0; cursample < results.length; cursample++) { var srgb = results[cursample]["sRGB"]; var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]); jg.setColor(rgbx); jg.fillRect(x * 20, y * 20, 20, 20); x++; if (x >= mychart["NumberOfColumns"]) { x = 0; y++; } } }
    jg.paint(); if (next != -1) {
        mychart["CurrentStrip"] = mychart["RowNames"][next]; var query = new Object(); query["Success"] = "Samples Complete"; query["Fail"] = "Failed to read samples"; mychart["Result"] = null; if (typeof mychart["RefBase"] != "undefined") { mychart["ReferencePatch"] = mychart["RefBase"].slice(next * mychart["NumberOfColumns"], (next + 1) * mychart["NumberOfColumns"]); }
        var result = plugin().read(scanner, mychart, query, presentStripDialog, presentProgress, presentScanDone); if (result != "OK") { alert("Scanning Device has been unplugged"); } 
    } 
}
function presentProgress(scanner, mychart) {
    console.log("In presentProgress"); var result = mychart["Result"]; var samples = result.length; var prog = new Object(); var width = mychart["NumberOfColumns"]; var height = mychart["NumberOfRows"]; var lines = samples / width; prog["Prompt"] = "<CENTER>Reading progress " + samples + " samples</CENTER>"; prog["Answers"] = "NONE"; presentQuery(prog); var jg = new jsGraphics("Canvas"); var cursample = 0; var x = 0; var y = 0; for (cursample = 0; cursample < samples; cursample++) { var srgb = result[cursample]["sRGB"]; var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]); jg.setColor(rgbx); jg.fillRect(x * 20, y * 20, 20, 20); x++; if (x >= width) { x = 0; y++; } }
    jg.paint(); return "OK";
}
function presentProgressiSis(scanner, mychart) {
    console.log("In presentProgress"); var results = mychart["Result"]; var samples = results.length; var prog = new Object(); var width = mychart["NumberOfColumns"]; var height = mychart["NumberOfRows"]; var lines = samples / width; prog["Prompt"] = "<CENTER>Reading progress " + samples + " samples</CENTER>"; prog["Answers"] = "NONE"; presentQuery(prog); var jg = new jsGraphics("Canvas"); var cursample = 0; var x = 0; var y = 0; for (cursample = 0; cursample < samples; cursample++) { var srgb = results[cursample]["sRGB"]; var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]); jg.setColor(rgbx); jg.fillRect(x * 20, y * 20, 20, 20); x++; if (x >= width) { x = 0; y++; } }
    jg.paint(); return "OK";
}
function DisplayLog() { console.log("In DisplayLog"); var log = plugin().getLog(); var newHTML = 'Log:(' + log.length + ')<BR>'; newHTML += '<PRE>\n'; newHTML += log; newHTML += '</PRE><BR>'; document.getElementById("Log").innerHTML = newHTML; }
function scan(scanner, colorspace, chart) {
    var localchart = chart; localchart["ReadCS"] = colorspace; localchart["Substrate"] = paper; if (scanner["Type"] == "iO" || scanner["Type"] == "iSis") { if (typeof chart["RefBase"] != "undefined") { localchart["ReferencePatch"] = chart["RefBase"]; } } else { localchart["CurrentStrip"] = chart["RowNames"][0]; if (typeof chart["RefBase"] != "undefined") { localchart["ReferencePatch"] = chart["RefBase"].slice(0, chart["NumberOfColumns"]); } }
    var query = new Object(); query["Success"] = "Samples Complete"; query["Fail"] = "Failed to read samples"; scanner["Options"] = "AutoDirection"; scanner["SampleSource"] = "Paper"; if (typeof chart["DeviceSettings"] == "string") { scanner["DeviceSettings"] = chart["DeviceSettings"]; }
    var newHTML = ''; newHTML += '<DIV ID="Canvas" style="position:relative;height:850px;width:850px;">'; newHTML += '</DIV>'; var span = "UploadData" + scanner["Name"]; document.getElementById(span).innerHTML = newHTML; if (scanner["Type"] == "iO" || scanner["Type"] == "iSis") { var result = plugin().read(scanner, localchart, query, presentChartDialog, presentProgressiSis, presentScanDoneiSis); } else {
        var result = plugin().read(scanner, localchart, query, presentStripDialog, presentProgress, presentScanDone); if (result != "OK") { alert("Scanning Device has been unplugged"); }
        var jg = new jsGraphics("Canvas"); var x = 0; var y = 0; for (var cursample = 0; cursample < chart["RefBase"].length; cursample++) { var rgbx = chart["RefBase"][cursample]["HTMLColor"]; jg.setColor(rgbx); jg.fillRect(x * 20, y * 20, 20, 20); x++; if (x >= chart["NumberOfColumns"]) { x = 0; y++; } }
        jg.paint();
    } 
}
function presentScannerList(ScannerList) {
    g_ScannerList = ScannerList; var newHTML = ""; for (var i = 0; i < ScannerList.length; i++) {
        var scanner = g_ScannerList[i]; var scantag = "g_ScannerList[" + i + "]"; var features = scanner["Requirements"]; newHTML += '<DT>' + scanner['Name'] + ':' + scanner["Type"] + ':' + scanner['Serial'] + '</DT>'; newHTML += '<DD>'; if (features.indexOf("RequiresCalibration") >= 0 || features.indexOf("RequiresCalibration") >= 0) { newHTML += '<A HREF="#" onclick="javascript:calibrateDevice(' + scantag + ')">Calibrate</A><BR>'; }
        newHTML += '<DIV STYLE="margin-left:60px">'; newHTML += '<A HREF="#" onclick="javascript:scan(' + scantag + ',\'Density,CIELab,CIEXYZ,sRGB\', g_Chart)">Read Sync ' + g_Chart["Name"] + ' Chart</A> '; var span = "UploadData" + scanner["Name"]; newHTML += '<SPAN ID="' + span + '"></SPAN> '; newHTML += '</DIV>'; newHTML += '</DD>';
    }
    document.getElementById('ScannerList').innerHTML = newHTML;
}
function ListDevices() { result = plugin().requestScannerList(presentScannerList); if (result != 'OK') { alert("Failed to request scanner list: " + result); } }
function uploadCB(msg, xml) { alert(msg); RemoveModalWindow(); }
function upload() { saveMeasurement(); RemoveModalWindow(); }
var _rulesAdded = false; function OnCancel() { g_Question["Result"] = 'CANCEL'; var cancelstat = plugin().queryDone(g_Question); if (cancelstat == "Unhandled") { RemoveModalWindow(); } }
function OnQuery(question) {
    $('modalWindow').onclick = function () { OnModalWindowClick(question) }; var newHTML = question['Prompt']; g_Question = question; newHTML += '<span style ="position:absolute;right:0;bottom:0">'; newHTML += '<A HREF="#" onclick="javascript:OnCancel()">'; newHTML += 'CANCEL'; newHTML += '</A>'; newHTML += '</span>'; document.getElementById('modalWindow').innerHTML = newHTML; $('modalWindow').style.display = $('modalBackground').style.display = 'block'; OnWindowResize(); if (window.attachEvent)
        window.attachEvent('onresize', OnWindowResize); else if (window.addEventListener)
        window.addEventListener('resize', OnWindowResize, false); else
        window.onresize = OnWindowResize; if (document.all)
        document.documentElement.onscroll = OnWindowResize;
}
function OnWindowResize() { var left = window.XMLHttpRequest == null ? document.documentElement.scrollLeft : 0; var top = window.XMLHttpRequest == null ? document.documentElement.scrollTop : 0; var div = $('modalWindow'); div.style.left = Math.max((left + (GetWindowWidth() - div.offsetWidth) / 2), 0) + 'px'; div.style.top = '200px'; }
function OnModalWindowClick(question) {
    if (question["Answers"] == "NONE") { return; }
    RemoveModalWindow()
    question["Result"] = "OK"; plugin().queryDone(question);
}
function RemoveModalWindow() {
    $('modalWindow').style.display = $('modalBackground').style.display = 'none'; if (window.detachEvent)
        window.detachEvent('onresize', OnWindowResize); else if (window.removeEventListener)
        window.removeEventListener('resize', OnWindowResize, false); else
        window.onresize = null;
}
function RemoveSelectSpans() {
    var selects = document.getElementsByTagName('select'); for (var i = 0; i < selects.length; i++) {
        var select = selects[i]; if (select.clientWidth == 0 || select.clientHeight == 0 || select.nextSibling == null || select.nextSibling.className != 'selectReplacement') { continue; }
        select.parentNode.removeChild(select.nextSibling); select.style.display = select.cachedDisplay;
    } 
}
function ReplaceSelectsWithSpans() {
    var selects = document.getElementsByTagName('select'); for (var i = 0; i < selects.length; i++) {
        var select = selects[i]; if (select.clientWidth == 0 || select.clientHeight == 0 || select.nextSibling == null || select.nextSibling.className == 'selectReplacement') { continue; }
        var span = document.createElement('span'); span.style.height = (select.clientHeight - 4) + 'px'; span.style.width = (select.clientWidth - 6) + 'px'; span.style.display = 'inline-block'; span.style.border = '1px solid rgb(200, 210, 230)'; span.style.padding = '1px 0 0 4px'; span.style.fontFamily = 'Arial'; span.style.fontSize = 'smaller'; span.style.position = 'relative'; span.style.top = '1px'; span.className = 'selectReplacement'; span.innerHTML = select.options[select.selectedIndex].innerHTML + '<img src="custom_drop.gif" alt="drop down" style="position: absolute; right: 1px; top: 1px;" />'; select.cachedDisplay = select.style.display; select.style.display = 'none'; select.parentNode.insertBefore(span, select.nextSibling);
    } 
}
function GetWindowWidth() { var width = document.documentElement && document.documentElement.clientWidth || document.body && document.body.clientWidth || document.body && document.body.parentNode && document.body.parentNode.clientWidth || 0; return width; }
function GetWindowHeight() { var height = document.documentElement && document.documentElement.clientHeight || document.body && document.body.clientHeight || document.body && document.body.parentNode && document.body.parentNode.clientHeight || 0; return height; }
function $(id) { return document.getElementById(id); }
console.log("xychartread.js complete");