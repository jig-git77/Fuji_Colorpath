<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>FUIJIFILM - FTSS</title>
<style type="text/css">
<!--
.style1 {
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 12px;
}
.style3 {font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold; }
.style7 {color: #FF0000}
.tblStyle
{
BORDER:SOLID 1PX #c4c4c4;BACKGROUND-COLOR:#FFF;border-collapse:collapse
}
-->
</style>
<link href="stylesheet/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="frmDefault" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanelMain" runat="server"  ><ContentTemplate>
<table width="750" border="0">
  <tr>
    <td style="width: 52px">&nbsp;</td>
    <td colspan="2"><a href="EnovationLogin.aspx" class="style3">Continue to Login Page</a></td>
  </tr>
<tr>
<td style="width: 52px; height: 21px;"></td>
<td colspan="2" style="height: 21px"></td>
</tr>
    <tr>
        <td style="width: 52px; height: 21px">
        </td>
        <td colspan="2" style="height: 21px">
            <strong><span style="font-size: 10pt; font-family: Verdana">Download Checklist</span></strong></td>
    </tr>
    <tr>
        <td style="width: 52px; height: 21px">
        </td>
        <td colspan="2" rowspan="3">
            <div style="text-align: left">
                <table>
                    <tr>
                        <td style="width: 102px">
                        </td>
                        <td style="width: 266px">
                        </td>
                        <td style="width: 63px">
                        </td>
                    </tr>
                    <tr>
                        <td  class="font_th" >
                            Checklist For</td>
                        <td style="width: 266px">
                            <asp:DropDownList ID="ddlChecklistType" runat="server" Width="160px" AutoPostBack="True" OnSelectedIndexChanged="ddlChecklistType_SelectedIndexChanged" CssClass="ddlCheckList" >
                                <asp:ListItem Value="0" Selected>--Select--</asp:ListItem>
                                <asp:ListItem Value="1">Install</asp:ListItem>
                                <asp:ListItem Value="2">Preventive Maintenance</asp:ListItem>
                                <asp:ListItem Value="3">Training</asp:ListItem>
                                 <asp:ListItem Value="4">Plates</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="width: 63px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 102px; height: 28px">
                        </td>
                        <td style="width: 266px; height: 28px">
                            <asp:DropDownList ID="ddlCheckList" runat="server" Width="210px" CssClass="ddlCheckList">
                            </asp:DropDownList>
                            <asp:Button ID="btn_DownloadPDF" OnClick="btn_DownloadPDF_Click" runat="server"  CssClass="Go"                        Text="Go" Width="26px"></asp:Button></td>
                        <td style="width: 63px; height: 28px">
                            </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td style="width: 52px; height: 21px">
        </td>
    </tr>
    <tr>
        <td style="width: 52px; height: 21px">
        </td>
    </tr>
    <tr>
        <td style="width: 52px; height: 55px">
        </td>
        <td colspan="2" style="height: 55px">
        </td>
    </tr>
  <tr>
    <td style="width: 52px; height: 21px;">&nbsp;</td>
    <td colspan="2" style="height: 21px"><span class="style3">Website User Guide</span></td>
  </tr>
<tr>
<td style="width: 52px; height: 31px;"></td>
<td colspan="2" style="height: 31px">
<span style="font-size: 9pt; color: #0000ff; font-family: Verdana; text-decoration: underline">Call Closing Site User Guide</span></td>
</tr>
  <tr>
    <td style="width: 52px; height: 21px;">&nbsp;</td>
    <td colspan="2" style="height: 21px"><table width="550"  class="tblStyle">
      <tr>
        <td style="width: 656px; height: 36px;"><span class="style3">Using your cell phone as a modem</span></td>
        <td style="height: 36px; width: 568px;"><span class="style3">Setting up email on your phone</span></td>
      </tr>
        <tr>
            <td style="width: 656px; height: 18px"><span style="color: Red;font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;">New!</span><a href="PDF/Iphone Wi-Fi Tethering.pdf" target="_blank" class="style1">
                 
                    IPhone Wi-Fi Tethering </td>
            <td style="width: 568px; height: 18px">
                <span style="color: Red;font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;">New!</span><a href="PDF/Iphone Exchange Setup Instructions.PDF" target="_blank" class="style1">
                 
                    IPhone</td>
        </tr>
    </table></td>
  </tr>
</table>
</ContentTemplate>
<Triggers> 
<asp:PostBackTrigger ControlID="btn_DownloadPDF"  /> 
</Triggers> 
</asp:UpdatePanel> 
</form>  
</body>
</html>
