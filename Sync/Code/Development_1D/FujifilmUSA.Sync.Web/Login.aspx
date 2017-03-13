<%@ Page Language="C#" AutoEventWireup="true" Theme="" CodeBehind="Login.aspx.cs"
    Inherits="FujifilmUSA.Sync.Web.Login" %>

<%@ Register Src="~/UserControl/Login.ascx" TagName="Login" TagPrefix="ucLogin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <title>Login</title>
    <link href="CSS/Common.css?version=4" rel="stylesheet" type="text/css" />
    <script src="JQuery/jquery-1.7.1.min.js" type="text/javascript"></script>

</head>
<script type="text/javascript">
    window.history.forward();
    function noBack() {
        window.history.forward();
    }
        </script>

<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        var navigatorVersion = navigator.appVersion;
        var navigatorAgent = navigator.userAgent;
        var browserName = navigator.appName;
        var fullVersionName = '' + parseFloat(navigator.appVersion);
        var majorVersionName = parseInt(navigator.appVersion, 10);
        var nameOffset, verOffset, ix;

        // In Firefox, the true version is after "Firefox" 
        if ((verOffset = navigatorAgent.indexOf("Firefox")) != -1) {
            browserName = "Firefox";
            fullVersionName = navigatorAgent.substring(verOffset + 8);

            if (fullVersionName > 51) {

                var PluginVersion = Plugin_load();
            
                if (PluginVersion == "") {
                    var r = confirm("This version of FireFox does not support the FujiFilm Measurement Plugin!\n\nPress OK for More Details or Cancel to Exit.");
                    if (r == true) {
                        document.location.href = 'FireFoxPluginDetails.aspx';
                    }
                }
            }
        }
    });
</script>

<script type="text/javascript">

    //-----------Script to check Fuji Plugin is installed or not--------//
    OSName = "Unknown OS";
    if (navigator.appVersion.indexOf("Win") != -1) OSName = "Windows";
    if (navigator.appVersion.indexOf("Mac") != -1) OSName = "MacOS";
    if (navigator.appVersion.indexOf("X11") != -1) OSName = "UNIX";
    if (navigator.appVersion.indexOf("Linux") != -1) OSName = "Linux";

    function plugin() {
        return document.getElementById('plugin0');
    }
    function addEvent(obj, name, func) {
        if (window.addEventListener) {
            obj.addEventListener(name, func, false);
        } else {
            obj.attachEvent("on" + name, func);
        }
    }

    function Plugin_load() {

        var c = document.getElementById("Hidden_PluginMessage");
        c.value = "Error in plugin.";
        addEvent(plugin(), 'fire', function (x) {
            alert('onfired() from load(): ' + x)
        });

        try {
            if (OSName == "Windows") {
                plugin().setDebug("C:/temp/FujiCM.log");
            } 
            else {
                plugin().setDebug("/tmp/isis.log");
            }

            var newHTML = 'Fuji Color Manager Plugin Version ' + plugin().plugin_version;
            c.value = newHTML;

            //------Assign Plugin Version-----//
            var d = document.getElementById("Hidden_PluginVersion");
            d.value = plugin().plugin_version;
        }
        catch (err) {
            return "";
        }
        return d.value;
    }

</script>

<body onload="noBack();" onpageshow="if (event.persisted) noBack();" onunload="">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress" runat="server" DynamicLayout="true" DisplayAfter="1500">
        <ProgressTemplate>
            <AjaxToolkit:AlwaysVisibleControlExtender ID="AlwaysVisibleControl" HorizontalSide="Center"
                VerticalSide="Middle" TargetControlID="UpdateProgress" runat="server">
            </AjaxToolkit:AlwaysVisibleControlExtender>
            <div class="UpdateProgressBackgroundLessOptimize" style="z-index: 1000; width: 100%; height: 100%;" unselectable="on">
            </div>
            <div id="divUpdate" style="color: #BABABA; position: absolute; z-index: 1010;">                                
                <div style="width: 100px; height: 25px;color: #000000; font-family: Arial,Helvetica,sans-serif; font-size: 12px;">Please wait...
                <br />
                <img src="Images/processbar.gif" alt="Loading" style="vertical-align: middle; margin: 15px;"></img>                
                </div>                                
                
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="bodyWrapper">
        <ucLogin:Login ID="ucLogin1" runat="server" />
    </div>
    <div id="footer">
        <center>
         <table style="width:954px">
            <tr>
                <td style="width:20%; padding-left:7px;">
                    <img alt="" src="Images/logo_Fujifilm.png" />
                </td>
                <td align="center">
                    <b> © <%= DateTime.Now.Year %> FUJIFILM North America Corporation - Graphics Division | 
                        <a href="Terms/ColorPath Sync TC 2013.pdf"   title="Terms of Use" target="_blank">Terms and Conditions</a>
                    </b>
                </td>
                <td align="right" style="width:20%;">
                     <img alt="" src="Images/logo_bottom.png" />
                </td>
            </tr>
        </table>
      </center>
    </div>

     <object id="plugin0" type="application/x-fujicmplugin" width="0" height="0">
            </object>
            <br />
            <div id="DivTable" runat="server" style="visibility: hidden;">
                <input id="Hidden_PluginMessage" type="hidden" runat="server" />
                <input id="Hidden_PluginVersion" type="hidden" runat="server" />
            </div>
    </form>
</body>
</html>
