<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="EnovationLogin.aspx.cs" Inherits="_Default" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>FujiFilm Technical Services and Solutions</title>
    <link href="stylesheet/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
    <fieldset style="width:450px;border-right: #000000 1px solid; border-top: #000000 1px solid; border-left: #000000 1px solid; border-bottom: #000000 1px solid;"><legend style="font-size: 10pt; font-family: Verdana;margin-left: 25px;
">Login</legend>
      <table border="0" style="width: 450px">
        <tr>
          <td style="width: 15px; height: 152px;" >&nbsp;</td>
          <td width="395" style="height: 152px" ><table  border="0">
<tr>
<td style="height: 14px">
</td>
<td style="height: 14px">
<img src="images/logo.gif" alt="FujiFilm" /></td>
</tr>
              <tr>
                <td style="height: 21px">&nbsp;</td>
                <td style="height: 21px"><asp:Label ID="ErrLabel" runat="server" Width="308px" CssClass="errorlabel_bold"></asp:Label></td>
              </tr>
              <tr>
                <td><span class="font_tahomabold">Login</span></td>
                <td><asp:TextBox ID="UserTextBox"   runat="server" CssClass="txtbox" Width="130px" TabIndex="1"></asp:TextBox></td>
              </tr>
              <tr>
                <td><span class="font_tahomabold">Password</span></td>
                <td><asp:TextBox ID="PwdTextBox"  TextMode="Password" runat="server" CssClass="txtbox" Width="129px" TabIndex="2" ></asp:TextBox></td>
              </tr>
              <tr>
                <td></td>
                <td><span class="font_verdana"><asp:CheckBox ID="CHK_RememberMe" runat="server" TabIndex="3" />Remember Me (Do not check on a public computer)</span></td>
              </tr>
              <tr>
                <td>&nbsp;</td>
                <td ><asp:Button ID="LoginButton" runat="server" OnClick="LoginButton_Click" Text="Submit" CssClass="SubmitButton" TabIndex="4" />
      <asp:UpdateProgress ID="updProgress" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
            <ProgressTemplate>            
            <img alt="progress" src="images/ajax-loader.gif"/>
                 
            </ProgressTemplate>
        </asp:UpdateProgress>
    </td>
              </tr>
            <tr><td>&nbsp;</td></tr>
          </table></td>
        </tr>
      </table></fieldset>
      </ContentTemplate>
      </asp:UpdatePanel>
  </form>
</body>
</html>
