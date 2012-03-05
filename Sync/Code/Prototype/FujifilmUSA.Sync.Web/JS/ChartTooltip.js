
var FADINGTOOLTIP
var wnd_height, wnd_width;
var tooltip_height, tooltip_width;
var tooltip_shown = false;
var transparency = 100;
var timer_id = 1;
var tooltiptext;

// override events
window.onload = WindowLoading;
window.onresize = UpdateWindowSize;
document.onmousemove = AdjustToolTipPosition;

function DisplayTooltip(tooltip_text) {
    FADINGTOOLTIP.innerHTML = tooltip_text;
    tooltip_shown = (tooltip_text != "") ? true : false;
    if (tooltip_text != "") {
        // Get tooltip window height
        tooltip_height = (FADINGTOOLTIP.style.pixelHeight) ? FADINGTOOLTIP.style.pixelHeight : FADINGTOOLTIP.offsetHeight;
        transparency = 0;
        ToolTipFading();
    }
    else {
        clearTimeout(timer_id);
        FADINGTOOLTIP.style.visibility = "hidden";
    }
}

function AdjustToolTipPosition(e) {    
    if (tooltip_shown) {
        // Depending on IE/Firefox, find out what object to use to find mouse position
        var ev;
        if (e)
            ev = e;
        else
            ev = event;

        FADINGTOOLTIP.style.visibility = "visible";
        offset_y = (ev.clientY + tooltip_height - document.body.scrollTop + 30 >= wnd_height) ? -15 - tooltip_height : 20;
        
        tooltipLeft = Math.min(wnd_width - tooltip_width - 10, Math.max(3, ev.clientX + 6)) + document.body.scrollLeft;
        
        if (tooltipLeft > 555) {
            FADINGTOOLTIP.style.left = (tooltipLeft - FADINGTOOLTIP.offsetWidth) + 'px';
        }
        else {
            FADINGTOOLTIP.style.left = tooltipLeft + 'px';  //Math.min(wnd_width - tooltip_width - 10, Math.max(3, ev.clientX + 6)) + document.body.scrollLeft + 'px';
        }
        //alert(FADINGTOOLTIP.style.left);
        FADINGTOOLTIP.style.top = ev.clientY + offset_y + document.body.scrollTop + 'px';
    }
}

function WindowLoading() {
    FADINGTOOLTIP = document.getElementById('FADINGTOOLTIP');

    // Get tooltip  window width				
    tooltip_width = (FADINGTOOLTIP.style.pixelWidth) ? FADINGTOOLTIP.style.pixelWidth : FADINGTOOLTIP.offsetWidth;

    // Get tooltip window height
    tooltip_height = (FADINGTOOLTIP.style.pixelHeight) ? FADINGTOOLTIP.style.pixelHeight : FADINGTOOLTIP.offsetHeight;

    UpdateWindowSize();
}

function ToolTipFading() {
    if (transparency <= 100) {
        FADINGTOOLTIP.style.filter = "alpha(opacity=" + transparency + ")";
        FADINGTOOLTIP.style.opacity = transparency / 100;
        transparency += 5;
        timer_id = setTimeout('ToolTipFading()', 35);
    }
}

function UpdateWindowSize() {
    wnd_height = document.body.clientHeight;
    wnd_width = document.body.clientWidth;
}
                                               
                                    