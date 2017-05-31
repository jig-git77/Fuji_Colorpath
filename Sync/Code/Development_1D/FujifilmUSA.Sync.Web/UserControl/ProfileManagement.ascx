<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ProfileManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ProfileManagement" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>

 <script type="text/javascript">

     $(document).ready(function () {
       
         jQuery("input[id*='txtNewPassword']").val('');
     });

     // Custom Validator to valid password
     function ValidPassword(source, arguments) {
         arguments.IsValid = IsValidPassWord();
     }
     function IsValidPassWord() {
         var password = jQuery("input[id*='txtNewPassword']").val();
         var checkspace = /\s/;

         if (true == checkspace.test(password)) {
             jQuery("span[id*='spanPassWord']").html('<%= GetResourceString("spacesNotAllowedText") %>');
             return false;
         }
         if (password.length < 4) {
             jQuery("span[id*='spanPassWord']").html('<%= GetResourceString("passwordLengthText") %>');
             return false;
         }
         return true;
     }

     // Custom Validator to valid confirm password and match password and confirm password
     function ConfirmPassWordPassword(source, arguments) {
         var flag = true;
         flag = IsValidConfirmPassWord();
         if (flag == false) {
             arguments.IsValid = false;
             return;
         }
         if (jQuery("input[id*='txtNewPassword']").val() != (jQuery("input[id*='txtConfirmPass']").val())) {
             jQuery("span[id*='spanConfirmPassWord']").html('<%= GetResourceString("passwordNotMatchText") %>');
             arguments.IsValid = false;
             return;
         }
         arguments.IsValid = true;
     }
     function IsValidConfirmPassWord() {
         var password = jQuery("input[id*='txtConfirmPass']").val();
         var noofchar = /^.*(?=.{1,4}).*$/;
         var checkspace = /\s/;

         if (true == checkspace.test(password)) {
             jQuery("span[id*='spanConfirmPassWord']").html('<%= GetResourceString("spacesNotAllowedText") %>');
             return false;
         }
         if (password.length < 4) {
             jQuery("span[id*='spanConfirmPassWord']").html('<%= GetResourceString("confirmPasswordLengthText") %>');
             return false;
         }
         return true;
     }


     function passwordcheck() {
         var password = jQuery("input[id*='txtNewPassword']").val();
         var Excellent = /^.*(?=.{1,})(?=.*[A-Z])(?=.*[\d])(?=.*[\W]).*$/;
         var strong = /^[a-zA-Z\d\W_]*(?=[a-zA-Z\d\W_]{1,})(((?=[a-zA-Z\d\W_]*[A-Z])(?=[a-zA-Z\d\W_]*[\d]))|((?=[a-zA-Z\d\W_]*[A-Z])(?=[a-zA-Z\d\W_]*[\W_]))|((?=[a-zA-Z\d\W_]*[\d])(?=[a-zA-Z\d\W_]*[\W_])))[a-zA-Z\d\W_]*$/;
         var Average = /^[a-zA-Z\d\W_]*(?=[a-zA-Z\d\W_]{1,})(?=[a-zA-Z\d\W_]*[A-Z]|[a-zA-Z\d\W_]*[\d]|[a-zA-Z\d\W_]*[\W_])[a-zA-Z\d\W_]*$/;
         var Weak = /^((^[a-z]{1,}$)|(^[A-Z]{1,}$)|(^[\d]{1,}$)|(^[\W_]{1,}$))$/;


         if (password.length == 0) {
             $("span[id*='CustomIndicator']").css("display", "none");
             jQuery("span[id*='PasswordRepeatedIndicator']").html("");
             $("span[id*='PasswordRepeatedIndicator']").removeClass();
             return;
         }

         if (Excellent.test(password)) {
             $("span[id*='CustomIndicator']").css("display", "inline");
             jQuery("span[id*='PasswordRepeatedIndicator']").html('<%= GetResourceString("passwordExcellent") %>');
             $("span[id*='PasswordRepeatedIndicator']").removeClass().addClass("L5");
         }
         else if (strong.test(password)) {
             $("span[id*='CustomIndicator']").css("display", "inline");
             jQuery("span[id*='PasswordRepeatedIndicator']").html('<%= GetResourceString("passwordStrong") %>');
             $("span[id*='PasswordRepeatedIndicator']").removeClass().addClass("L4");
         }
         else if (Average.test(password) == true && bad.test(password) == false) {
             $("span[id*='CustomIndicator']").css("display", "inline");
             jQuery("span[id*='PasswordRepeatedIndicator']").html('<%= GetResourceString("passwordAverage") %>');
             $("span[id*='PasswordRepeatedIndicator']").removeClass().addClass("L3");
         }
         else if (Weak.test(password)) {
             $("span[id*='CustomIndicator']").css("display", "inline");
             jQuery("span[id*='PasswordRepeatedIndicator']").html('<%= GetResourceString("passwordWeak") %>');
             $("span[id*='PasswordRepeatedIndicator']").removeClass().addClass("L2");
         }
         else {
             $("span[id*='CustomIndicator']").css("display", "inline ");
             jQuery("span[id*='PasswordRepeatedIndicator']").html('<%= GetResourceString("passwordVeryPoor") %>');
             $("span[id*='PasswordRepeatedIndicator']").removeClass().addClass("L1");
         }
     }
</script>

<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1><asp:Localize runat="server" Text="My Settings" ID="locMySettings" 
                meta:resourcekey="locMySettingsResource1"></asp:Localize></h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <center>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <center>
                        <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                            Visible="False" />
                        <fieldset style="width: 50%;">
                            <legend>
                                <asp:Label ID="lblChangeLanguage" runat="server" Text="Change Language" meta:resourcekey="lblChangeLanguageResource1" ></asp:Label>
                            </legend>
                            <table>                               
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Language" 
                                            runat="server" ID="locLanguage" meta:resourcekey="locLanguageResource1"></asp:Localize><span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:DropDownList ID="ddlLanguages" runat="server" CssClass="select"></asp:DropDownList>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvLanguage" runat="server" Display="Dynamic"
                                                ControlToValidate="ddlLanguages" ForeColor="Red" 
                                                ValidationGroup="vgChangeLanguage">
                                                <ucValidationContainer:ValidationContainer ID="rfvucChangeLanguage" runat="server"
                                                    ValidationMsg="Please select language." Size="200" />
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>                                
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="left">
                                        <div class="text">
                                            <div style="padding-left: 3px;">
                                                <asp:LinkButton ID="lbtnSaveLanguage" runat="server" CssClass="gray_bt" ValidationGroup="vgChangeLanguage"
                                                    OnClick="lbtnChangeLanguage_Click">
                                                    <span> <asp:Localize Text="Save" runat="server" ID="locSaveLanguage" 
                                                    meta:resourcekey="locSavePwdResource1"></asp:Localize> 
 </span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnCancelLanguage" runat="server" CssClass="gray_bt" 
                                                    PostBackUrl="~/Settings.aspx">
                                                    <span> <asp:Localize Text="Cancel" runat="server" ID="locCancelLanguage" 
                                                    meta:resourcekey="locCancelPwdResource1"></asp:Localize> 
 </span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <br />
                       
                        <fieldset style="width: 50%;">
                            <legend>
                                <asp:Label ID="lblHeader" runat="server" Text="Change Password" 
                                    meta:resourcekey="lblHeaderResource1"></asp:Label>
                            </legend>
                            <table>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="User Name" runat="server" 
                                            ID="locUsername" meta:resourcekey="locUsernameResource1"></asp:Localize> </span>
                                    </td>
                                    <td>
                                          <b>&nbsp;:&nbsp;</b>   
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblUserName" runat="server" 
                                            meta:resourcekey="lblUserNameResource1"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="New Password" 
                                            runat="server" ID="locNewPassword" meta:resourcekey="locNewPasswordResource1"></asp:Localize><span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtNewPassword" runat="server" Height="19px" MaxLength="25" 
                                                onkeydown="javascript:passwordcheck()" TextMode="Password" Width="200px" 
                                                meta:resourcekey="txtNewPasswordResource1"></asp:TextBox>
                                            <span ID="CustomIndicator" class="Base"><span ID="PasswordRepeatedIndicator">
                                            </span></span>
                                        </div>
                                        <div style="float: left">
                                            <asp:CustomValidator ID="cvPassWord" runat="server" 
                                                ClientValidationFunction="ValidPassword" 
                                                ValidationGroup="vgChangePassword">
                                                    <div class="tooltip_round_box" style=" margin-top: 0px; margin-left: 0px; width: 250px;">
                                                    <div class="tooltip_arrow" style="margin-top: -3px;">&nbsp;</div>
                                                    <div class="tooltip_top_left"></div>
                                                    <div class="tooltip_top_right"></div>
                                                    <div class="tooltip_inside"><span id='spanPassWord'><asp:Localize 
                                                            Text="Please provide password." runat="server" ID="locPasswordRequired" 
                                                            meta:resourcekey="locPasswordRequiredResource1"></asp:Localize>
</span></div>
                                                    <div class="tooltip_bottom_left"></div>
                                                    <div class="tooltip_bottom_right"></div>
                                                    </div>
                                            </asp:CustomValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Confirm Password" 
                                            runat="server" ID="locConfirmPassword" 
                                            meta:resourcekey="locConfirmPasswordResource1"></asp:Localize><span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtConfirmPass" runat="server" MaxLength="25" TextMode="Password"
                                                Width="200px" Height="19px" meta:resourcekey="txtConfirmPassResource1"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:CustomValidator ID="cvConfirmPassword" ClientValidationFunction="ConfirmPassWordPassword"
                                                runat="server" ValidationGroup="vgChangePassword">
                                                <div class="tooltip_round_box" style="margin-top: 0px; margin-left: 0px; width: 300px;">
                                                <div class="tooltip_arrow" style="margin-top: -3px;">&nbsp;</div>
                                                <div class="tooltip_top_left"></div>
                                                <div class="tooltip_top_right"></div>
                                                <div class="tooltip_inside"><span id='spanConfirmPassWord'><asp:Localize 
                                                        runat="server" Text="Please provide confirm password." 
                                                        ID="locConfirmPasswordRequired" 
                                                        meta:resourcekey="locConfirmPasswordRequiredResource1"></asp:Localize>
</span></div>
                                                <div class="tooltip_bottom_left"></div>
                                                <div class="tooltip_bottom_right"></div>
                                                </div>
                                         </asp:CustomValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="left">
                                        <div class="text">
                                            <div style="padding-left: 3px;">
                                                <asp:LinkButton ID="lbtnSavePassword" runat="server" CssClass="gray_bt" ValidationGroup="vgChangePassword"
                                                    OnClick="lbtnSavePassword_Click">
                                                    <span> <asp:Localize Text="Save" runat="server" ID="locSavePwd" 
                                                    meta:resourcekey="locSavePwdResource1"></asp:Localize> 
 </span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnCancelPassword" runat="server" CssClass="gray_bt" 
                                                    PostBackUrl="~/Settings.aspx">
                                                    <span> <asp:Localize Text="Cancel" runat="server" ID="locCancelPwd" 
                                                    meta:resourcekey="locCancelPwdResource1"></asp:Localize> 
 </span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <br />
                        
                        <fieldset style="width: 60%;">
                            <legend>
                                <asp:Label ID="Label1" runat="server" Text="Change Security Information" 
                                    meta:resourcekey="Label1Resource1"></asp:Label>
                            </legend>
                            <table>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Security Question" 
                                            runat="server" ID="locSecurityQuestion" 
                                            meta:resourcekey="locSecurityQuestionResource1"></asp:Localize><span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:DropDownList ID="cmbSecurityQuestion" runat="server" Width="400px" 
                                                CssClass="select" DataValueField="Key" DataTextField="Value" 
                                                meta:resourcekey="cmbSecurityQuestionResource1">
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvSecurityQuestion" runat="server" Display="Dynamic"
                                                ControlToValidate="cmbSecurityQuestion" ForeColor="Red" 
                                                ValidationGroup="vgChangeSecurity">
                                                <ucValidationContainer:ValidationContainer ID="rfvcSecurityQuestionContainer" runat="server"
                                                    ValidationMsg="Please select security question." Size="200" />
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize runat="server" 
                                            Text="Security Answer" ID="locSecurityAnswer" 
                                            meta:resourcekey="locSecurityAnswerResource1"></asp:Localize><span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtSecurityAnswer" runat="server" MaxLength="50" Width="400px" 
                                                Height="19px" meta:resourcekey="txtSecurityAnswerResource1"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvSecurityAnswer" runat="server" Display="Dynamic"
                                                ControlToValidate="txtSecurityAnswer" ForeColor="Red" 
                                                ValidationGroup="vgChangeSecurity">
                                                <ucValidationContainer:ValidationContainer ID="rfvcSecurityAnswerContainer" runat="server"
                                                    ValidationMsg="Please provide security answer." Size="200" />
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="left">
                                        <div class="text">
                                            <div style="padding-left: 3px;">
                                                <asp:LinkButton ID="lbtnSaveSecurity" runat="server" CssClass="gray_bt" ValidationGroup="vgChangeSecurity"
                                                    OnClick="lbtnSaveSecurity_Click">
                                                    <span> <asp:Localize runat="server" Text="Save" 
                                                    ID="locSaveSecurityQuestion" 
                                                    meta:resourcekey="locSaveSecurityQuestionResource1"></asp:Localize> 
 </span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnCancelSecurity" runat="server" CssClass="gray_bt" 
                                                    PostBackUrl="~/Settings.aspx">
                                                    <span> <asp:Localize Text="Cancel" runat="server" 
                                                    ID="locCancelSecurityQuestion" 
                                                    meta:resourcekey="locCancelSecurityQuestionResource1"></asp:Localize> 
 </span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <br />
                       
                        <fieldset style="width: 50%; visibility:hidden;">
                            <legend>
                                <asp:Label ID="lblChangeUserDevice" runat="server" Text="Change User Device Info" 
                                    meta:resourcekey="lblChangeUserDeviceResource1"></asp:Label>
                            </legend>
                            <table>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Serial Number" 
                                            runat="server" ID="locSerialNumber" 
                                            meta:resourcekey="locSerialNumberResource1"></asp:Localize><span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:DropDownList ID="cmbMeasurementDevice" runat="server" Width="200px" 
                                                CssClass="select" DataValueField="Key" DataTextField="Value" 
                                                meta:resourcekey="cmbMeasurementDeviceResource1">
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                ControlToValidate="cmbMeasurementDevice" ForeColor="Red" 
                                                ValidationGroup="vgChangeUserDevice">
                                                <ucValidationContainer:ValidationContainer ID="ValidationContainer1" runat="server"
                                                    ValidationMsg="Please select device serial number." Size="200" />
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Density Type" 
                                            runat="server" ID="locDensityType" 
                                            meta:resourcekey="locDensityTypeResource1"></asp:Localize>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:DropDownList ID="cmbDensityType" runat="server" Width="200px" 
                                                CssClass="select" DataValueField="Key" DataTextField="Value" 
                                                meta:resourcekey="cmbDensityTypeResource1">
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left">
                                          
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                               <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="UV Settings" 
                                            runat="server" ID="locUVSettings" 
                                            meta:resourcekey="locUVSettingsResource1"></asp:Localize>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:DropDownList ID="cmbUVSettings" runat="server" Width="200px" 
                                                CssClass="select" DataValueField="Key" DataTextField="Value" 
                                                meta:resourcekey="cmbUVSettingsResource1">
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left">
                                           
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="left">
                                        <div class="text">
                                            <div style="padding-left: 3px;">
                                                <asp:LinkButton ID="lbtnSaveUserDevice" runat="server" CssClass="gray_bt" 
                                                    ValidationGroup="vgChangeUserDevice" onclick="lbtnSaveUserDevice_Click">
                                                    <span> <asp:Localize runat="server" Text="Save" 
                                                        ID="Localize3" 
                                                        meta:resourcekey="locSaveUserDeviceResource1"></asp:Localize> 
                                                    </span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" 
                                                    PostBackUrl="~/Settings.aspx">
                                                    <span> <asp:Localize Text="Cancel" runat="server" 
                                                        ID="Localize4" 
                                                        meta:resourcekey="locCancelUserDeviceResource1"></asp:Localize> 
                                                    </span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </center>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
