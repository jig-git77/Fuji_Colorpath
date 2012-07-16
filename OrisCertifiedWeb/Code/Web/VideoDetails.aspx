<%@ Page Language="C#" AutoEventWireup="true"  EnableEventValidation="false" CodeFile="VideoDetails.aspx.cs" Inherits="VideoDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>FUJIFILM Graphic Systems U.S.A., Inc.</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <meta name="keywords" content="FUJIFILM Graphic Systems U.S.A., Inc." />
    <meta name="description" content="FUJIFILM Graphic Systems U.S.A., Inc." />
    <link href="CSS/Default.css" rel="stylesheet" type="text/css" />
</head>
<body class="green-form-bg">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" border="0" align="left" width="100%">
            <tr>
                <td class="ERROR">
                    <asp:Literal ID="LblError" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td class="green-title">
                                <b>
                                    <asp:Literal ID="LTRL_Title" runat="server" Text="Video Detail"></asp:Literal></b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellpadding="5" cellspacing="5" width="100%">
                                    <tr>
                                        <td valign="top">
                                            <div style="width: 300px; border: 1px solid #000;">
                                                <%--<object classid="clsid:02BF25D5-8C17-4B23-BC80-D3488ABDDC6B" codebase="http://www.apple.com/qtactivex/qtplugin.cab"
                                                    height="300" width="300">
                                                    <param name="src" value='<%= VideoFilePath %>'>
                                                    <param name="autoplay" value="true">
                                                    <param name="controller" value="true">
                                                    <embed src='<%= VideoFilePath %>' autoplay="false" controller="true" pluginspage="http://www.apple.com/quicktime/download/"
                                                        height="300" width="300" showlogo="false" scale="tofit">
                                                </object>--%>
                                                <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0"
                                                    width="300" height="300">
                                                    <param name="movie" value='player.swf?file=<%= VideoFilePath %>' />
                                                    <param name="wmode" value="transparent">
                                                    <param name="allowFullScreen" value="true"></param>
                                                    <param name="quality" value="high" />
                                                    <embed src='player.swf?file=<%= VideoFilePath %>' quality="high" wmode="transparent" pluginspage="http://www.macromedia.com/go/getflashplayer"
                                                        type="application/x-shockwave-flash" width="300" height="300"></embed></object>
                                            </div>
                                        </td>
                                        <td valign="top">
                                            <%= strDescription %>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
