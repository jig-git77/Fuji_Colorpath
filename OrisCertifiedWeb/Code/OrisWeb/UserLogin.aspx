<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="UserLogin.aspx.cs" Inherits="OrisWeb.UserLogin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="~/Common/ProgressBar.ascx" TagName="ProgressBar" TagPrefix="uc1" %>
<%@ Register Src="~/Common/LoginControl.ascx" tagname="LoginControl" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>FUJIFILM Graphic Systems U.S.A., Inc.</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <meta name="keywords" content="FUJIFILM Graphic Systems U.S.A., Inc." />
    <meta name="description" content="FUJIFILM Graphic Systems U.S.A., Inc." />
    <link href="css/Default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <div style="width:100%;height:50px"  class="logo-slogan">
                <div style="float:right;padding-right:50px;padding-top:5px;"><img src="images/powered-by.gif" align="middle" alt="FUJIFILM Graphic Systems U.S.A., Inc." /></div>                
            </div>            
            <div style="margin-left:40%;margin-top:20%;">
            <div style="width:300px; float:left;"><div style="height:10px">
                <span style="width:299px;height:10px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-top-2-300.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-top-2.png" width="299" height="10" border="0" alt="" /></span>
            </div>
            <div style="height:170px; background:url(images/popup-mid-300.png) top left repeat-y #FFF; padding:3px 16px 15px 10px; color:#666666; font-family:Tahoma, Arial, Helvetica, sans-serif; font-size:11px; text-align:left; vertical-align:top;border-left:#808080;border-right:#808080">
                  <uc1:LoginControl ID="LoginControl1" runat="server" />
            </div>
            <div style="clear:both"></div>
             <div style="float:left; height:19px">
                <span style="width:299px;height:19px;display:inline-block;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/popup-bottom-300.png');"><img style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" src="images/popup-bottom.png" width="299" height="19" border="0" alt="" /></span>
             </div>
            </div>            
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc1:ProgressBar ID="ProgressBar1" runat="server" />
    </form>
</body>
</html>
