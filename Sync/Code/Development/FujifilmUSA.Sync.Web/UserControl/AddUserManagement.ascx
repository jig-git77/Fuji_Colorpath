<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddUserManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddUserManagement" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>
<style type="text/css">
    
</style>
<script type="text/javascript">

    $(document).ready(function () {
        if ('<%= UserId %>' <= 0) {
            jQuery("input[id*='txtAdminUserName']").val('');
            jQuery("input[id*='txtAdminPassWord']").val('');
        }
    });

    // Custome Validator to valid password
    function ValidPassword(source, arguments) {
        arguments.IsValid = IsValidPassWord();
    }
    function IsValidPassWord() {
        var password = jQuery("input[id*='txtAdminPassWord']").val();
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

    // Custome Validator to valid confirm password and match password and confirm password
    function ConfirmPassWordPassword(source, arguments) {
        var flag = true;
        flag = IsValidConfirmPassWord();
        if (flag == false) {
            arguments.IsValid = false;
            return;
        }
        if (jQuery("input[id*='txtAdminPassWord']").val() != (jQuery("input[id*='txtConfirmPass']").val())) {
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
            jQuery("span[id*='spanConfirmPassWord']").html("Must be 4 char in comfirm password");
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
<script type="text/javascript" language="javascript">
    function ValidateChkList(source, arguments) {
        arguments.IsValid = IsCheckBoxChecked() ? true : false;
    }

    function IsCheckBoxChecked() {
        var isChecked = false;
        var list = document.getElementById('<%= chkUserBranch.ClientID %>');
        if (list != null) {
            for (var i = 0; i < list.rows.length; i++) {
                for (var j = 0; j < list.rows[i].cells.length; j++) {
                    var listControl = list.rows[i].cells[j].childNodes[0];
                    if (listControl.checked) {
                        isChecked = true;
                    }
                }
            }
        }
        return isChecked;
    }
</script>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1>
            User Management</h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" style="width: 98%;">
                    <tr>
                        <td>
                            <fieldset>
                                <legend>
                                    <asp:Label ID="lblHeader" runat="server" Text="Add User"></asp:Label>
                                </legend>
                                <table cellpadding="0" cellspacing="3">
                                    <tr>
                                        <td colspan="2">
                                            <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="width: 50%;">
                                            <center>
                                                <fieldset>
                                                    <legend>Personal Information</legend>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <span style="font-weight: bold;">First Name<span style="color: Red;">*</span> </span>
                                                            </td>
                                                            <td>
                                                                <b>&nbsp;:&nbsp;</b>
                                                            </td>
                                                            <td>
                                                                <div style="float: left;">
                                                                    <asp:TextBox ID="txtFirstName" runat="server" MaxLength="30" Width="270px" Height="19px"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left">
                                                                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" Display="Dynamic" ControlToValidate="txtFirstName"
                                                                        ForeColor="Red" ValidationGroup="vgAddUser">
                                                                        <ucValidationContainer:ValidationContainer ID="rfvFirstNameContainer" runat="server"
                                                                            ValidationMsg="Please provide first name." Size="200" />
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span style="font-weight: bold;">Last Name<span style="color: Red;">*</span> </span>
                                                            </td>
                                                            <td>
                                                                <b>&nbsp;:&nbsp;</b>
                                                            </td>
                                                            <td>
                                                                <div style="float: left;">
                                                                    <asp:TextBox ID="txtLastName" runat="server" MaxLength="30" Width="270px" Height="19px"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left">
                                                                    <asp:RequiredFieldValidator ID="rfvLastName" runat="server" Display="Dynamic" ControlToValidate="txtLastName"
                                                                        ForeColor="Red" ValidationGroup="vgAddUser">
                                                                        <ucValidationContainer:ValidationContainer ID="rfvLastNameContainer" runat="server"
                                                                            ValidationMsg="Please provide last name." Size="200" />
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span style="font-weight: bold;">Email<span style="color: Red;">*</span> </span>
                                                            </td>
                                                            <td>
                                                                <b>&nbsp;:&nbsp;</b>
                                                            </td>
                                                            <td>
                                                                <div style="float: left;">
                                                                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="30" Width="270px" Height="19px"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left">
                                                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" Display="Dynamic" ControlToValidate="txtEmail"
                                                                        ForeColor="Red" ValidationGroup="vgAddUser">
                                                                        <ucValidationContainer:ValidationContainer ID="rfvEmailContainer" runat="server"
                                                                            ValidationMsg="Please provide email-address." Size="200" />
                                                                    </asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="revEmail" ControlToValidate="txtEmail" Display="Dynamic"
                                                                        runat="server" ForeColor="Red" ValidationGroup="vgAddUser" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                                        <ucValidationContainer:ValidationContainer ID="revEmailContainer" runat="server"
                                                                            ValidationMsg="Please enter proper email-address." Size="200" />
                                                                    </asp:RegularExpressionValidator>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span style="font-weight: bold;">User Name<span style="color: Red;">*</span> </span>
                                                            </td>
                                                            <td>
                                                                <b>&nbsp;:&nbsp;</b>
                                                            </td>
                                                            <td>
                                                                <div style="float: left;">
                                                                    <asp:TextBox ID="txtAdminUserName" runat="server" MaxLength="50" Width="270px" Height="19px"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left">
                                                                    <asp:RequiredFieldValidator ID="rfvUserName" runat="server" Display="Dynamic" ControlToValidate="txtAdminUserName"
                                                                        ForeColor="Red" ValidationGroup="vgAddUser">
                                                                        <ucValidationContainer:ValidationContainer ID="rfvUserNameContainer" runat="server"
                                                                            ValidationMsg="Please provide user name." Size="200" />
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <%--<tr>
                                                            <td>
                                                                <span style="font-weight: bold;">PassWord<span style="color: Red;">*</span> </span>
                                                            </td>
                                                            <td>
                                                                <b>&nbsp;:&nbsp;</b>
                                                            </td>
                                                            <td>
                                                                <div>
                                                                    <asp:TextBox ID="txtAdminPassWord" Width="100px" MaxLength="30" runat="server" Height="19px"
                                                                        TextMode="Password" onkeydown="javascript:passwordcheck()"></asp:TextBox>
                                                                    <span id="CustomIndicator" class="Base"><span id="PasswordRepeatedIndicator"></span>
                                                                    </span>
                                                                </div>
                                                                <div style="float: left">
                                                                    <asp:CustomValidator ID="cvPassWord" ClientValidationFunction="ValidPassword" runat="server"
                                                                        ValidationGroup="vgAddUser">
                                                                            <div class="tooltip_round_box" style=" margin-top: -32px; margin-left: 102px; width: 250px;">
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
                                                            <td>
                                                                <span style="font-weight: bold;">Confirm Password<span style="color: Red;">*</span>
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <b>&nbsp;:&nbsp;</b>
                                                            </td>
                                                            <td>
                                                                <div style="float: left;">
                                                                    <asp:TextBox ID="txtConfirmPass" runat="server" MaxLength="40" TextMode="Password"
                                                                        Width="100px" Height="19px"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left">
                                                                    &nbsp;
                                                                    <asp:CustomValidator ID="cvConfirmPassword" ClientValidationFunction="ConfirmPassWordPassword"
                                                                        runat="server" ValidationGroup="vgAddUser">
                                                                            <div class="tooltip_round_box" style="margin-top: -32px; margin-left: 0px; width: 300px;">
                                                                            <div class="tooltip_arrow" style="margin-top: -3px;">&nbsp;</div>
                                                                            <div class="tooltip_top_left"></div>
                                                                            <div class="tooltip_top_right"></div>
                                                                            <div class="tooltip_inside"><span id='spanConfirmPassWord'>Please provide confirm password.</span></div>
                                                                            <div class="tooltip_bottom_left"></div>
                                                                            <div class="tooltip_bottom_right"></div>
                                                                            </div>
                                                                    </asp:CustomValidator>
                                                                </div>
                                                                <div style="float: left;">
                                                                    <asp:LinkButton ID="lbtnShow" runat="server" CssClass="gray_bt" OnClick="lbtnShow_Click">
                                                                    <span> Show Password </span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </td>
                                                        </tr>--%>
                                                        <tr>
                                                            <td>
                                                                <span style="font-weight: bold;">Is Active?</span>
                                                            </td>
                                                            <td>
                                                                <b>&nbsp;:&nbsp;</b>
                                                            </td>
                                                            <td>
                                                                <div style="float: left;">
                                                                    <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" />
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="trPasswordReset">
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <b>&nbsp;&nbsp;</b>
                                                            </td>
                                                            <td>
                                                                <div style="float: left;">
                                                                    <asp:LinkButton ID="lnkPasswordReset" runat="server" OnClick="lnkPasswordReset_Click" />
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </center>
                                        </td>
                                        <td valign="top" style="width: 50%;">
                                            <center>
                                                <fieldset>
                                                    <legend>Access Type</legend>
                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td style="width: 140px;">
                                                                <span style="font-weight: bold;">Role<span style="color: Red;">*</span>
                                                            </td>
                                                            <td>
                                                                <b>&nbsp;:&nbsp;</b>
                                                            </td>
                                                            <td>
                                                                <div style="float: left;">
                                                                    <asp:DropDownList ID="cmbRole" AutoPostBack="true" runat="server" CssClass="select"
                                                                        Width="270px" OnSelectedIndexChanged="cmbRole_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div style="float: left">
                                                                    <asp:RequiredFieldValidator ID="rfvRole" runat="server" ControlToValidate="cmbRole"
                                                                        Display="Dynamic" ForeColor="Red" ValidationGroup="vgAddUser">
                                                                        <ucValidationContainer:ValidationContainer ID="rfvRoleContainer" runat="server" ValidationMsg="Please select role."
                                                                            Size="200" />
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="trRegion">
                                                            <td>
                                                                <span style="font-weight: bold;">Region<span style="color: Red;">*</span> </span>
                                                            </td>
                                                            <td>
                                                                <b>&nbsp;:&nbsp;</b>
                                                            </td>
                                                            <td>
                                                                <div style="float: left;">
                                                                    <asp:DropDownList ID="cmbRegion" AutoPostBack="true" runat="server" Width="270px"
                                                                        CssClass="select" OnSelectedIndexChanged="cmbRegion_SelectedIndexChanged">
                                                                        <asp:ListItem Text="--Select Region--" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div style="float: left">
                                                                    <asp:RequiredFieldValidator ID="rfvRegion" runat="server" Display="Dynamic" ControlToValidate="cmbRegion"
                                                                        ForeColor="Red" ValidationGroup="vgAddUser">
                                                                        <ucValidationContainer:ValidationContainer ID="rfvRegionContainer" runat="server"
                                                                            ValidationMsg="Please select region." Size="200" />
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="trCompany">
                                                            <td style="width: 140px;">
                                                                <span style="font-weight: bold;">Company Name<span style="color: Red;">*</span>
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <b>&nbsp;:&nbsp;</b>
                                                            </td>
                                                            <td>
                                                                <div style="float: left;">
                                                                    <asp:DropDownList ID="cmbCompany" runat="server" Width="270px" AutoPostBack="true"
                                                                        CssClass="select" OnSelectedIndexChanged="cmbCompany_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div style="float: left">
                                                                    <asp:RequiredFieldValidator ID="rfvcompanyName" runat="server" Display="Dynamic"
                                                                        ControlToValidate="cmbCompany" ForeColor="Red" ValidationGroup="vgAddUser">
                                                                        <ucValidationContainer:ValidationContainer ID="rfvCompanyContainer" runat="server"
                                                                            ValidationMsg="Please select company." Size="200" />
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="trBranch">
                                                            <td>
                                                                <span style="font-weight: bold;">Branch Name<span style="color: Red;">*</span> </span>
                                                            </td>
                                                            <td>
                                                                <b>&nbsp;:&nbsp;</b>
                                                            </td>
                                                            <td>
                                                                <div style="float: left; line-height: 18px; overflow-y: scroll; height: 70px; border-width: 1px;
                                                                    border-style: solid; width: 270px">
                                                                    <asp:CheckBoxList ID="chkUserBranch" CssClass="checkListBox" runat="server" ClientValidationFunction="ValidateChkList">
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                                <div style="float: left">
                                                                    <asp:CustomValidator ID="cvBranch" ClientValidationFunction="ValidateChkList" runat="server"
                                                                        ValidationGroup="vgAddUser">
                                                                        <ucValidationContainer:ValidationContainer ID="cvBranchContainer" runat="server"
                                                                            ValidationMsg="Please select branch." Size="200" />
                                                                    </asp:CustomValidator>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                         <tr runat="server" id="tr_User_Type">
                                                            <td style="width: 140px;">
                                                                <span style="font-weight: bold;">User Type<span style="color: Red;">*</span>
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <b>&nbsp;:&nbsp;</b>
                                                            </td>
                                                            <td>
                                                                <div style="float: left;">
                                                                    <asp:DropDownList ID="cmbUserType" runat="server" Width="270px" CssClass="select">
                                                                        <asp:ListItem Selected="True" Value="0">Website User</asp:ListItem>
                                                                        <asp:ListItem Value="1">XMF User</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div style="float: left">
                                                                  
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </center>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <div style="margin-top: 10px; padding-left: 3px;">
                                <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" OnClick="lbtnSave_Click"
                                    ValidationGroup="vgAddUser">
                                                    <span> Save </span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" PostBackUrl="~/UserManagement.aspx">
                                                    <span> Cancel </span>
                                </asp:LinkButton>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
