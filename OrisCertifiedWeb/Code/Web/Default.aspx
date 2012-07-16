<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<%@ Register src="Common/ProgressBar.ascx" tagname="ProgressBar" tagprefix="uc1" %>
<%@ Register src="Common/LoginControl.ascx" tagname="LoginControl" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>CGS Publishing Technologies International</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <meta name="keywords" content="CGS Publishing Technologies International GmbH" />
    <meta name="description" content="CGS Publishing Technologies International GmbH" />
    <link href="css/Default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" class="maintable">
                <tr>
                    <td valign="top">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td valign="top">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td class="header-top-border">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="top">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="center">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="top">
                                                            <table width="940" border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td valign="top" class="logo-slogan">
                                                                        <a href="index.htm" title="CGS">
                                                                            <img src="images/cgs_logo_sm.gif" alt="CGS" width="100" height="36" border="0" /></a>CGS 
                                                                        Publishing Technologies International</td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor="#0094D6" style="height: 1px;">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="center" class="content-bg">
                                    <table width="940" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td height="45" align="center" valign="top">
                                                <table width="600" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td width="23" height="36" valign="top">
                                                            <img src="images/menu-curve-left.png" alt="FujiFilm" border="0" />
                                                        </td>
                                                        <td valign="top" class="content-menu-top">
                                                           <!-- <span class="note-green">Please note:</span> <span class="note-black">if no local language is available, English will be shown as default</span>-->
                                                            &nbsp;
                                                        </td>
                                                        <td width="23" valign="top">
                                                            <img src="images/menu-curve-right.png" alt="FujiFilm" border="0" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <table width="600" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td width="25" valign="top">
                                                            <img src="images/curve-bg-lefttop.png" alt="" border="0" />
                                                        </td>
                                                        <td align="center" valign="top" class="login-bg-top" >
                                                            <img src="images/oriscert_logo_small.jpg" alt="CGS" border="0" vspace="10" width="300px" height="251px" />
                                                        </td>
                                                        <td width="25" valign="top">
                                                            <img src="images/curve-bg-righttop.png" alt="" border="0" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="bottom">
                                                            <img src="images/curve-bg-leftbottom.png" alt="" border="0" />
                                                        </td>   
                                                        <td align="center" valign="top" class="login-bg-bottom">
                                                            <uc1:LoginControl ID="LoginControl1" runat="server" />
                                                        </td>
                                                        <td valign="bottom">
                                                            <img src="images/curve-bg-rightbottom.png" alt="" border="0" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td height="60" valign="bottom">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="footer">
                            <tr>
                                <td class="copyright">
                                    © <%=System.DateTime.Now.Year %> CGS Publishing Technologies International
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>                
    <uc1:ProgressBar ID="ProgressBar1" runat="server" />
    </form>
</body>
</html>
