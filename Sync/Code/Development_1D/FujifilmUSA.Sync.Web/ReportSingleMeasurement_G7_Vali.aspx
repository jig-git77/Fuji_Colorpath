<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportSingleMeasurement_G7_Vali.aspx.cs" Inherits="FujifilmUSA.Sync.Web.ReportSingleMeasurement_G7_Vali" %>

<%@ Register Src="~/UserControl/ReportSingleMeasurement_G7_Vali.ascx" TagName="Report" TagPrefix="uc" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <uc:Report ID="ucReportSingle" runat="server"/>
    </div>
    </form>
</body>
</html>
