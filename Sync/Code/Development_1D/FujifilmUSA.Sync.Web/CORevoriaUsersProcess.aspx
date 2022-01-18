<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CORevoriaUsersProcess.aspx.cs" Inherits="FujifilmUSA.Sync.Web.CORevoriaUsersProcess" %>
<%@ Register Src="~/UserControl/CORevoriaUsersProcess.ascx" TagName="RevoriaUsersProcess" TagPrefix="ucCORevoriaUsersProcess" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <div id="bodyWrapper">
        <ucCORevoriaUsersProcess:RevoriaUsersProcess ID="ucCORevoriaUsersProcess" runat="server" />
    </div>
    </form>
</body>
</html>
