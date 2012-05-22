<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.PopupControl.ForgotPassword" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>
<script language="javascript" type="text/javascript">
    function Close() {
        GetForgotPasswordRadWindow().close();
        return false;
    }

    function GetForgotPasswordRadWindow() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow;
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
        return oWindow;
    } 

</script>
<div class="listDevice" style="margin-left: 10px">
    <table cellpadding="0" cellspacing="10" width="100%">
        <tr>
            <td>
                <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="false" />
            </td>
        </tr>
        <tr>
            <td>
                <div style="display: block;" class="contantbox tabcontant">
                    <div class="section_top">
                        &nbsp;</div>
                    <div class="section_middle">
                        <div class="combopart">
                            <div>
                            </div>
                            <div class="header">
                                <div class="content">
                                    <div style="float: left;" class="text">
                                        <label>
                                            Forgot Password</label>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 5px;">
                                <table width="90%">
                                    <tbody>
                                        <tr>
                                            <td style="width: 140px;">
                                                <span>UserName:</span>
                                            </td>
                                            <td>
                                                <div style="float: left;">
                                                    <asp:TextBox ID="txtUserName" runat="server" Width="200px" Height="19px" MaxLength="30"></asp:TextBox>
                                                </div>
                                                <div style="float: left">
                                                    <asp:RequiredFieldValidator ID="rfvUserName" runat="server" Display="Dynamic" ControlToValidate="txtUserName"
                                                        ForeColor="Red" ValidationGroup="vgForgotPassword">
                                                        <ucValidationContainer:ValidationContainer ID="rfvUserNameContainer" runat="server"
                                                            ValidationMsg="Please provide user name." Size="200" />
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="left">
                                                <div>
                                                    <asp:LinkButton ID="lbtnRetrievePassword" runat="server" ValidationGroup="vgForgotPassword" CssClass="gray_bt"
                                                        OnClick="lbtnRetrievePassword_Click">
                                                            <span> Retrieve Password </span>
                                                    </asp:LinkButton>
                                                     <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript: Close();"
                                                        CssClass="gray_bt">
                                                             <span>
                                                                  Cancel </span>
                                                    </asp:LinkButton>
                                                </div>                                               
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>                           
                        </div>
                    </div>
                    <div class="section_bottom">
                        &nbsp;</div>
                </div>
            </td>
        </tr>
    </table>
</div>
