<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="GlobalActivation.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.GlobalActivation" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>
  
  <script type="text/javascript">

      $(document).ready(function () {        
          
              jQuery("input[id*='txtAdminPassWord']").val('');          
      });

      // Custom Validator to valid password
      function ValidPassword(source, arguments) {
          arguments.IsValid = IsValidPassWord();
      }
      function IsValidPassWord() {
          var password = jQuery("input[id*='txtAdminPassWord']").val();
          var checkspace = /\s/;

          if (true == checkspace.test(password)) {
              jQuery("span[id*='spanPassWord']").html('<%= GetResourceString("spacesNotAllowed") %>');
              return false;
          }
          if (password.length < 4) {
              jQuery("span[id*='spanPassWord']").html('<%= GetResourceString("passwordLength") %>');
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
          if (jQuery("input[id*='txtAdminPassWord']").val() != (jQuery("input[id*='txtConfirmPass']").val())) {
              jQuery("span[id*='spanConfirmPassWord']").html('<%= GetResourceString("passwordsNotSame") %>');
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
              jQuery("span[id*='spanConfirmPassWord']").html('<%= GetResourceString("spacesNotAllowed") %>');
              return false;
          }
          if (password.length < 4) {
              jQuery("span[id*='spanConfirmPassWord']").html('<%= GetResourceString("confirmPasswordLength") %>');
              return false;
          }
          return true;
      }


      function passwordcheck() {
          var password = jQuery("input[id*='txtAdminPassWord']").val();
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
              jQuery("span[id*='PasswordRepeatedIndicator']").html('<%= GetResourceString("excellentText") %>');
              $("span[id*='PasswordRepeatedIndicator']").removeClass().addClass("L5");
          }
          else if (strong.test(password)) {
              $("span[id*='CustomIndicator']").css("display", "inline");
              jQuery("span[id*='PasswordRepeatedIndicator']").html('<%= GetResourceString("strongText") %>');
              $("span[id*='PasswordRepeatedIndicator']").removeClass().addClass("L4");
          }
          else if (Average.test(password) == true && bad.test(password) == false) {
              $("span[id*='CustomIndicator']").css("display", "inline");
              jQuery("span[id*='PasswordRepeatedIndicator']").html('<%= GetResourceString("averageText") %>');
              $("span[id*='PasswordRepeatedIndicator']").removeClass().addClass("L3");
          }
          else if (Weak.test(password)) {
              $("span[id*='CustomIndicator']").css("display", "inline");
              jQuery("span[id*='PasswordRepeatedIndicator']").html('<%= GetResourceString("weakText") %>');
              $("span[id*='PasswordRepeatedIndicator']").removeClass().addClass("L2");
          }
          else {
              $("span[id*='CustomIndicator']").css("display", "inline ");
              jQuery("span[id*='PasswordRepeatedIndicator']").html('<%= GetResourceString("veryPoorText") %>');
              $("span[id*='PasswordRepeatedIndicator']").removeClass().addClass("L1");
          }
      }
</script>
 
<div class="Global_top">
    &nbsp;</div>
<div class="Global_middle" style="line-height: 34px;">
    <div class="head">
        <h1><asp:Localize ID="locProfileMgmt" runat="server" Text="Profile Management" 
                meta:resourcekey="locProfileMgmtResource1"></asp:Localize></h1>
    </div>
    <div class="clear">
    </div>

    <div style="margin-left: 10px;">
        <%--        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
        <center>
            <fieldset style="width: 50%;">
                <legend style="margin-left: 40% !important;">
                    <asp:Label ID="lblHeader" runat="server" Text="Set Profile" 
                        meta:resourcekey="lblHeaderResource1"></asp:Label>
                </legend>
                <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                    Visible="False" />
                <table runat="server" id="tbPasswordForm">
                    <tr>
                        <td>
                            <span><asp:Localize runat="server" Text="First Name" ID="locFirstName" 
                                meta:resourcekey="locFirstNameResource1"></asp:Localize></span>
                        </td>
                        <td>
                            <b>&nbsp;:&nbsp;</b>
                        </td>
                        <td>
                            <div style="float: left;">
                                <asp:Label ID="lblFirstName" runat="server" Width="200px" MaxLength="100" 
                                    Height="19px" meta:resourcekey="lblFirstNameResource1"></asp:Label>
                            </div>
                            <div class="clear">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span><asp:Localize runat="server" Text="Last Name" ID="locLastName" 
                                meta:resourcekey="locLastNameResource1"></asp:Localize></span></td>
                        <td>
                            <b>&nbsp;:&nbsp;</b>
                        </td>
                        <td>
                            <div style="float: left;">
                                <asp:Label ID="lblLastName" runat="server" Width="200px" MaxLength="100" 
                                    Height="19px" meta:resourcekey="lblLastNameResource1"></asp:Label>
                            </div>
                            <div class="clear">
                            </div>
                        </td>
                    </tr>
                    <tr runat="server" id="trSecurityQuestion">
                        <td>
                            <span><asp:Localize runat="server" Text="Security Question" ID="locSecurityQue" 
                                meta:resourcekey="locSecurityQueResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                        </td>
                        <td>
                            <b>&nbsp;:&nbsp;</b>
                        </td>
                        <td>
                            <div style="float: left;">
                                <asp:DropDownList ID="cmbSecurityQuestion" runat="server" DataValueField="Key" DataTextField="Value"
                                    CssClass="select" Width="200px" 
                                    meta:resourcekey="cmbSecurityQuestionResource1">
                                </asp:DropDownList>
                            </div>
                            <div style="float: left">
                                <asp:RequiredFieldValidator ID="rfvSecurityQuestion" runat="server" Display="Dynamic"
                                    ControlToValidate="cmbSecurityQuestion" ForeColor="Red" ValidationGroup="vgSetProfile"
                                    InitialValue="0">
                                    <ucValidationContainer:ValidationContainer ID="rfvSecurityQuestionContainer" runat="server"
                                        ValidationMsg="Please select security question." Size="200" />
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="clear">
                            </div>
                        </td>
                    </tr>
                    <tr runat="server" id="trSecurityAnswer">
                        <td>
                            <span><asp:Localize runat="server" Text="Security Answer" ID="locSecurityAns" 
                                meta:resourcekey="locSecurityAnsResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                        </td>
                        <td>
                            <b>&nbsp;:&nbsp;</b>
                        </td>
                        <td>
                            <div style="float: left;">
                                <asp:TextBox ID="txtSecurityAnswer" runat="server" Width="200px" MaxLength="100"
                                    Height="19px" meta:resourcekey="txtSecurityAnswerResource1"></asp:TextBox>
                            </div>
                            <div style="float: left">
                                <asp:RequiredFieldValidator ID="rfvSecurityAnswer" runat="server" Display="Dynamic"
                                    ControlToValidate="txtSecurityAnswer" ForeColor="Red" 
                                    ValidationGroup="vgSetProfile">
                                    <ucValidationContainer:ValidationContainer ID="rfvSecurityAnswerContainer" runat="server"
                                        ValidationMsg="Please provide security answer." Size="200" />
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="clear">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span><asp:Localize runat="server" Text="New Password" ID="locNewPassword" 
                                meta:resourcekey="locNewPasswordResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                        </td>
                        <td>
                            <b>&nbsp;:&nbsp;</b>
                        </td>
                        <td>
                            <div style="float: left;">
                                <asp:TextBox ID="txtAdminPassWord" Width="100px" MaxLength="25" runat="server" Height="19px"
                                    TextMode="Password" onkeydown="javascript:passwordcheck()" 
                                    meta:resourcekey="txtAdminPassWordResource1"></asp:TextBox>
                                <span id="CustomIndicator" class="Base"><span id="PasswordRepeatedIndicator"></span>
                                </span>
                            </div>
                            <div style="float: left">
                                <asp:CustomValidator ID="cvPassWord" ClientValidationFunction="ValidPassword" runat="server"
                                    ValidationGroup="vgSetProfile">
                                        <div class="tooltip_round_box" style=" margin-top: 0px; margin-left: 0px; width: 250px;">
                                        <div class="tooltip_arrow" style="margin-top: -3px;">&nbsp;</div>
                                        <div class="tooltip_top_left"></div>
                                        <div class="tooltip_top_right"></div>
                                        <div class="tooltip_inside"><span id='spanPassWord'><asp:Localize Text="Please provide password." runat="server" ID="locProvidePassword" meta:resourcekey="locProvidePasswordResource1"></asp:Localize></span></div>
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
                            <span><asp:Localize runat="server" Text="Confirm Password" 
                                ID="locConfirmPassword" meta:resourcekey="locConfirmPasswordResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                        </td>
                        <td>
                            <b>&nbsp;:&nbsp;</b>
                        </td>
                        <td>
                            <div style="float: left;">
                                 <asp:TextBox ID="txtConfirmPass" runat="server" MaxLength="25" TextMode="Password"
                                             Width="100px" Height="19px" 
                                     meta:resourcekey="txtConfirmPassResource1"></asp:TextBox>
                            </div>
                            <div style="float: left">
                                <asp:CustomValidator ID="cvConfirmPassword" ClientValidationFunction="ConfirmPassWordPassword"
                                    runat="server" ValidationGroup="vgSetProfile">
                                    <div class="tooltip_round_box" style="margin-top: 0px; margin-left: 0px; width: 300px;">
                                    <div class="tooltip_arrow" style="margin-top: -3px;">&nbsp;</div>
                                    <div class="tooltip_top_left"></div>
                                    <div class="tooltip_top_right"></div>
                                    <div class="tooltip_inside"><span id='spanConfirmPassWord'><asp:Localize 
                                            runat="server" ID="locProvideConfirmPassword" 
                                            Text="Please provide confirm password." 
                                            meta:resourcekey="locProvideConfirmPasswordResource1"></asp:Localize>

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
                        <td>
                            <div class="text">
                                <div style="padding-left: 3px;">
                                    <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgSetProfile"
                                        OnClick="lbtnSave_Click">
                                                    <span> <asp:Localize runat="server" Text="Save & Login" 
                                        ID="locSaveAndLogin" meta:resourcekey="locSaveAndLoginResource1"></asp:Localize>

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
        <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
