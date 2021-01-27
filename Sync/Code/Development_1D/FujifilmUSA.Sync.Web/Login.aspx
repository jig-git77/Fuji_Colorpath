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
    <script src="JQuery/jquery-3.5.1.min.js" type="text/javascript"></script>

</head>
<script type="text/javascript">
    window.history.forward();
    function noBack() {
        window.history.forward();
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

    </form>
</body>
</html>
