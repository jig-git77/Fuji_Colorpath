<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrowserTest.aspx.cs" Inherits="FujifilmUSA.Sync.Web.BrowserTest" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Browser Test</title>
    
    <link href="CSS/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css"/>
   
    <script src="JQuery/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="JQuery/jquery-ui-1.8.16.custom.min.js"></script>
    <script type="text/javascript" src="JQuery/progressbar.js"></script>

    <script type="text/javascript">
        function Test_ChkBrowser() {            
            Test_Browser();
            Test_Javascript();
            Test_Cookies();
            Test_HTML5();
            Test_Plugins();
            Plugin_load();
        }
        function Test_Browser() {
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
            }
            // In MSIE, the true version is after "MSIE" in userAgent
            else if ((verOffset = navigatorAgent.indexOf("MSIE")) != -1) {
                browserName = "Microsoft Internet Explorer";
                fullVersionName = navigatorAgent.substring(verOffset + 5);
            }

            // In Chrome, the true version is after "Chrome" 
            else if ((verOffset = navigatorAgent.indexOf("Chrome")) != -1) {
                browserName = "Chrome";
                fullVersionName = navigatorAgent.substring(verOffset + 7);
            }

            // In Opera, the true version is after "Opera" or after "Version"
            else if ((verOffset = navigatorAgent.indexOf("Opera")) != -1) {
                browserName = "Opera";
                fullVersionName = navigatorAgent.substring(verOffset + 6);
                if ((verOffset = navigatorAgent.indexOf("Version")) != -1)
                    fullVersionName = navigatorAgent.substring(verOffset + 8);
            }

            // In Safari, the true version is after "Safari" or after "Version" 
            else if ((verOffset = navigatorAgent.indexOf("Safari")) != -1) {
                browserName = "Safari";
                fullVersionName = navigatorAgent.substring(verOffset + 7);
                if ((verOffset = navigatorAgent.indexOf("Version")) != -1)
                    fullVersionName = navigatorAgent.substring(verOffset + 8);
            }

            // In most other browsers, "name/version" is at the end of userAgent 
            else if ((nameOffset = navigatorAgent.lastIndexOf(' ') + 1) < (verOffset = navigatorAgent.lastIndexOf('/'))) {
                browserName = navigatorAgent.substring(nameOffset, verOffset);
                fullVersionName = navigatorAgent.substring(verOffset + 1);
                if (browserName.toLowerCase() == browserName.toUpperCase()) {
                    browserName = navigator.appName;
                }
            }
            // trim the fullVersionName string at semicolon/space if present
            if ((ix = fullVersionName.indexOf(";")) != -1)
                fullVersionName = fullVersionName.substring(0, ix);
            if ((ix = fullVersionName.indexOf(" ")) != -1)
                fullVersionName = fullVersionName.substring(0, ix);

            majorVersionName = parseInt('' + fullVersionName, 10);
            if (isNaN(majorVersionName)) {
                fullVersionName = '' + parseFloat(navigator.appVersion);
                majorVersionName = parseInt(navigator.appVersion, 10);
            }

            var d = document.getElementById('<%#Hidden_Browser.ClientID%>');
            d.value = browserName;
            var e = document.getElementById('<%#Hidden_BrowserVersion.ClientID%>');
            e.value = fullVersionName;         
        }
        function Test_Javascript() {
            var a = document.getElementById('<%#Hidden_Javascript.ClientID%>');
            a.value = "True";
        }
        function Test_Cookies() {
            var cookieEnabled = (navigator.cookieEnabled) ? 'True' : 'False';
            var b = document.getElementById('<%#Hidden_Cookies.ClientID%>');

            if (typeof navigator.cookieEnabled == "undefined" && !cookieEnabled) {
                document.cookie = "testcookie";
                cookieEnabled = (document.cookie.indexOf("testcookie") != -1) ? 'True' : 'False';
            }
            b.value = cookieEnabled;
        }
        function Test_HTML5() {
            var canvas = document.createElement('canvas');
            var answer = (!!(canvas.getContext && canvas.getContext('2d'))) ? 'True' : 'False';
            var c = document.getElementById('<%#Hidden_HTML5.ClientID%>');
            c.value = answer;
        }
        function Test_Plugins() {
            if (navigator.plugins && navigator.plugins.length > 0) {
                var L = navigator.plugins.length;          
                var Table_Plugin = document.getElementById('<%#Hidden_OtherPlugins.ClientID%>');
                Table_Plugin.value = "";
              
                for (var i = 0; i < L; i++) {
                    if (navigator.plugins[i].name.indexOf('Fuji') != -1) {
                        Table_Plugin.value +=  navigator.plugins[i].name ;
                    }
                    else {
                        Table_Plugin.value +=  navigator.plugins[i].name ;
                    }
                    if (i < L-1) {
                        Table_Plugin.value += ", ";
                    }
                }
            }
        }

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

            var c = document.getElementById('<%#Hidden_PluginMessage.ClientID%>');
            c.value = "Error in plugin.";
            addEvent(plugin(), 'fire', function (x) {
                alert('onfired() from load(): ' + x)
            });

            if (OSName == "Windows") {
                plugin().setDebug("C:/temp/FujiCM.log");
            } else {
                plugin().setDebug("/tmp/isis.log");
            }
            var newHTML = 'Fuji Color Manager Plugin Version ' + plugin().plugin_version;
            c.value = newHTML;
        }
    </script> 

    <style type="text/css">       
        body{background:#eee;font-family:Verdana, Helvetica, Arial, sans-serif;margin:0;padding:0;}
        .example{background:#FFF;width:650px;font-size:80%;border:1px #000 solid;margin:20px auto;padding:15px;position:relative;-moz-border-radius: 3px;-webkit-border-radius: 3px}
        h3 {text-align:center}

        .pbar .ui-progressbar-value {display:block !important}
        .pbar {overflow: hidden}
        .percent {position:relative;text-align: right;}
        .elapsed {position:relative;text-align: right;}
    </style>
</head>

<body>
    <form id="form1" runat="server">
    <div>  
       <table id="Table_Buttons" runat="server" width="100%">
            <tr>               
                <td align="center">
                    <input id="btn_ChkBrowser" type="button" value="Run ClientSide Test"  />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_ServerSide" runat="server" onclick="btn_ServerSide_Click" 
                        Text="Run ServerSide Test" />               
                    &nbsp;&nbsp;&nbsp;               
                    <asp:Button ID="btn_LoadGrid" runat="server" Text="Load Grid Data" 
                        onclick="btn_LoadGrid_Click" Visible="false" />
                </td>
            </tr>
            <tr>
                <td>
                     <asp:Label ID="lbl_Result" runat="server"></asp:Label>
                </td>
            </tr>
       </table>
    </div>

    <br />

    <div>
       <div class="example">
        <noscript>JavaScript: DISABLED <br /> This site requires JavaScript for navigation. Please enable JS and retry.</noscript> 
            <div id="progress1">
                <div class="percent"></div>
                <div class="pbar"></div>
                <div class="elapsed"></div>
            </div>
   
            <object id="plugin0" type="application/x-fujicmplugin" width="0" height="0"></object>
      
            <br />
            <div id="DivTable" runat="server" style="visibility:hidden;">               
                            <input id="Hidden_Browser" type="hidden" runat="server" />  
                            <input id="Hidden_BrowserVersion" type="hidden" runat="server" />                      
                            <input id="Hidden_Javascript" type="hidden" runat="server" />
                            <input id="Hidden_Cookies" type="hidden" runat="server" />
                            <input id="Hidden_HTML5" type="hidden" runat="server" />
                            <input id="Hidden_PluginMessage" type="hidden" runat="server" />  
                            <input id="Hidden_OtherPlugins" type="hidden" runat="server" />
            </div>
            <br />
            <div>
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
                </asp:ScriptManager>
                <asp:Panel ID="Panel_Grid" runat="server">
              
                  <asp:Button ID="btn_PDF" CssClass="button" Width="150px" 
                        Text="Export to PDF" runat="server" onclick="btn_PDF_Click" ></asp:Button>                   
                 
                <telerik:RadGrid ID="RadGrid_Test" runat="server" AutoGenerateColumns="false"                               
                Width="100%" CellSpacing="0" GridLines="None" >                                  
                <HeaderStyle Font-Size="12px" Font-Bold="True" ForeColor="Black" />
                <MasterTableView>  
                    <Columns>
                        <telerik:GridBoundColumn DataField="Description" HeaderText="Description" ItemStyle-Width="170px"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Details" HeaderText="Details" ></telerik:GridBoundColumn>                       
                    </Columns>                                        
                </MasterTableView>                                    
            </telerik:RadGrid>
               </asp:Panel>
            </div>
       </div> 

    </div>
    </form>
</body>
</html>
