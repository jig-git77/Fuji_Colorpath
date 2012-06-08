<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfileManagement.ascx.cs"
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
             jQuery("span[id*='spanPassWord']").html("Spaces are not allowed.");
             return false;
         }
         if (password.length < 4) {
             jQuery("span[id*='spanPassWord']").html("Must be 4 char in password");
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
             jQuery("span[id*='spanConfirmPassWord']").html("Password and comfirm password are not same.");
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
             jQuery("span[id*='spanConfirmPassWord']").html("Spaces are not allowed.");
             return false;
         }
         if (password.length < 4) {
             jQuery("span[id*='spanConfirmPassWord']").html("Must be 4 char in confirm password");
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
             jQuery("span[id*='PasswordRepeatedIndicator']").html("Excellent");
             $("span[id*='PasswordRepeatedIndicator']").removeClass().addClass("L5");
         }
         else if (strong.test(password)) {
             $("span[id*='CustomIndicator']").css("display", "inline");
             jQuery("span[id*='PasswordRepeatedIndicator']").html("Strong");
             $("span[id*='PasswordRepeatedIndicator']").removeClass().addClass("L4");
         }
         else if (Average.test(password) == true && bad.test(password) == false) {
             $("span[id*='CustomIndicator']").css("display", "inline");
             jQuery("span[id*='PasswordRepeatedIndicator']").html("Average");
             $("span[id*='PasswordRepeatedIndicator']").removeClass().addClass("L3");
         }
         else if (Weak.test(password)) {
             $("span[id*='CustomIndicator']").css("display", "inline");
             jQuery("span[id*='PasswordRepeatedIndicator']").html("Weak");
             $("span[id*='PasswordRepeatedIndicator']").removeClass().addClass("L2");
         }
         else {
             $("span[id*='CustomIndicator']").css("display", "inline ");
             jQuery("span[id*='PasswordRepeatedIndicator']").html("Very Poor");
             $("span[id*='PasswordRepeatedIndicator']").removeClass().addClass("L1");
         }
     }
</script>

<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1>
            My Settings</h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <center>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <center>
                        <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" Visible="false" />
                        <fieldset style="width: 40%;">
                            <legend>
                                <asp:Label ID="lblHeader" runat="server" Text="Change Password"></asp:Label>
                            </legend>
                            <table>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;">User Name </span>
                                    </td>
                                    <td>
                                          <b>&nbsp;:&nbsp;</b>   
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblUserName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;">New Password<span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtNewPassword" runat="server" Height="19px" MaxLength="25" 
                                                onkeydown="javascript:passwordcheck()" TextMode="Password" Width="200px"></asp:TextBox>
                                            <span ID="CustomIndicator" class="Base"><span ID="PasswordRepeatedIndicator">
                                            </span></span>
                                        </div>
                                        <div style="float: left">
                                            <asp:CustomValidator ID="cvPassWord" runat="server" 
                                                ClientValidationFunction="ValidPassword" ValidationGroup="vgChangePassword">
                                                    <div class="tooltip_round_box" style=" margin-top: 0px; margin-left: 0px; width: 250px;">
                                                    <div class="tooltip_arrow" style="margin-top: -3px;">&nbsp;</div>
                                                    <div class="tooltip_top_left"></div>
                                                    <div class="tooltip_top_right"></div>
                                                    <div class="tooltip_inside"><span id='spanPassWord'>Please provide password.</span></div>
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
                                        <span style="font-weight: bold;">Confirm Password<span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtConfirmPass" runat="server" MaxLength="25" TextMode="Password"
                                                Width="200px" Height="19px"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:CustomValidator ID="cvConfirmPassword" ClientValidationFunction="ConfirmPassWordPassword"
                                                runat="server" ValidationGroup="vgChangePassword">
                                                <div class="tooltip_round_box" style="margin-top: 0px; margin-left: 0px; width: 300px;">
                                                <div class="tooltip_arrow" style="margin-top: -3px;">&nbsp;</div>
                                                <div class="tooltip_top_left"></div>
                                                <div class="tooltip_top_right"></div>
                                                <div class="tooltip_inside"><span id='spanConfirmPassWord'>Please provide confirm password.</span></div>
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
                                                    <span> Save </span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnCancelPassword" runat="server" CssClass="gray_bt" PostBackUrl="~/Settings.aspx">
                                                    <span> Cancel </span>
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
                        <br />
                        <fieldset style="width: 60%;">
                            <legend>
                                <asp:Label ID="Label1" runat="server" Text="Change Security Information"></asp:Label>
                            </legend>
                            <table>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;">Security Question<span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:DropDownList ID="cmbSecurityQuestion" runat="server" Width="400px" CssClass="select" DataValueField="Key" DataTextField="Value">
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvSecurityQuestion" runat="server" Display="Dynamic"
                                                ControlToValidate="cmbSecurityQuestion" ForeColor="Red" ValidationGroup="vgChangeSecurity">
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
                                        <span style="font-weight: bold;">Security Answer<span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtSecurityAnswer" runat="server" MaxLength="50" Width="400px" Height="19px"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvSecurityAnswer" runat="server" Display="Dynamic"
                                                ControlToValidate="txtSecurityAnswer" ForeColor="Red" ValidationGroup="vgChangeSecurity">
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
                                                    <span> Save </span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnCancelSecurity" runat="server" CssClass="gray_bt" PostBackUrl="~/Settings.aspx">
                                                    <span> Cancel </span>
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
