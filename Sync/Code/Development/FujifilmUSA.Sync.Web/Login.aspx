<%@ Page Language="C#" AutoEventWireup="true" Theme="" CodeBehind="Login.aspx.cs"
    Inherits="FujifilmUSA.Sync.Web.Login" %>

<%@ Register Src="~/UserControl/Login.ascx" TagName="Login" TagPrefix="ucLogin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>
    <link href="CSS/Common.css?version=4" rel="stylesheet" type="text/css" />
    <script src="JQuery/jquery-1.7.1.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="bodyWrapper">
        <ucLogin:Login ID="ucLogin1" runat="server" />
    </div>
    <div id="footer">
        <b>© 2012 FUJIFILM North America Corporation - Graphic Systems Division </b>
        <br>
        <br>
        <img alt="" src="Images/logo_Fujifilm.png">
    </div>
    </form>
</body>
</html>
