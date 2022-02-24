﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintPreviewNew.aspx.cs" Inherits="FujifilmUSA.Sync.Web.PrintPreviewNew" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/UserControl/PrintPreviewNew.ascx" TagName="PrintPreview" TagPrefix="uc" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Preview</title>  
    <style type="text/css">
    body, html
    {
        font-family: Arial, Helvetica, sans-serif;
        font-size: 12px;
        color: #000;
        background: url(../Images/body_bg_small.png) repeat-x top left #c5c5c5;
    }
    </style>    
</head>
<body>
    <form id="form1" runat="server">
        <uc:PrintPreview ID="ucPrintPreview" runat="server" />    
    </form>
</body>
</html>