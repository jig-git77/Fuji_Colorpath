<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta content="index,follow" name="robots" />
    <meta content="text/html; charset=iso-8859-1" http-equiv="Content-Type" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="minimum-scale=1.0, width=device-width, maximum-scale=0.6667, user-scalable=no"
        name="viewport" />
    <title>::..Fujifilm..::</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="wapper">
        <div class="poweredby_page2">
            <span>
                <img src="images/poweredby_icon-1.png" alt="powered by" title="powered by" /></span>
            <span>powered by</span></div>
        <div class="logo_inner">
            <span class="logo_inner_span"><a href="Logout.aspx" class="logout">Logout</a></span><a
                    href="#"><img src="images/logo.png" alt="Fujifilm" title="Fujifilm" /></a></div>
        <div class="img_inner">
            <img src="images/img_inner.png" alt="Fujifilm" title="Fujifilm" /></div>
        <div class="bottom_link">
            <ul>
                <li><a href="JobList.aspx"><strong><span class="icon_job">JOBS</span></strong></a></li>
                <li><a href="DeviceList.aspx"><strong><span class="icon_device">Device</span></strong></a></li>
                <li><a href="#"><strong><span class="icon_status">Status</span></strong></a></li>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
