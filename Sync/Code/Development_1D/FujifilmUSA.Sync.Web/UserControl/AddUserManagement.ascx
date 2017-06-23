<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="AddUserManagement.ascx.cs"
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
            jQuery("span[id*='spanPassWord']").html('<%= GetResourceString("spacesNotAllowed") %>');
            return false;
        }
        if (password.length < 4) {
            jQuery("span[id*='spanPassWord']").html('<%= GetResourceString("passwordLengthText") %>');
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
            jQuery("span[id*='spanConfirmPassWord']").html('<%= GetResourceString("passwordsAreDifferent") %>');
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
            jQuery("span[id*='spanConfirmPassWord']").html('<%= GetResourceString("confirmPasswordLengthText") %>');
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
        <h1><asp:Localize runat="server" Text="User Management" ID="locUserMgmt" 
                meta:resourcekey="locUserMgmtResource1"></asp:Localize></h1>
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
                                    <asp:Label ID="lblHeader" runat="server" Text="Add User" 
                                        meta:resourcekey="lblHeaderResource1"></asp:Label>
                                </legend>
                                <table cellpadding="0" cellspacing="3">
                                    <tr>
                                        <td colspan="2">
                                            <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                                                Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="width: 50%;">
                                            <center>
                                                <fieldset>
                                                    <legend><asp:Localize runat="server" Text="Personal Information" 
                                                            ID="locPersonalInfo" meta:resourcekey="locPersonalInfoResource1"></asp:Localize></legend>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <span style="font-weight: bold;"><asp:Localize runat="server" Text="First Name" 
                                                                    ID="locFirstName" meta:resourcekey="locFirstNameResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                                            </td>
                                                            <td>
                                                                <b>&nbsp;:&nbsp;</b>
                                                            </td>
                                                            <td>
                                                                <div style="float: left;">
                                                                    <asp:TextBox ID="txtFirstName" runat="server" MaxLength="30" Width="270px" 
                                                                        Height="19px" meta:resourcekey="txtFirstNameResource1"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left">
                                                                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" Display="Dynamic" ControlToValidate="txtFirstName"
                                                                        ForeColor="Red" ValidationGroup="vgAddUser"><ucValidationContainer:ValidationContainer ID="rfvFirstNameContainer" runat="server"
                                                                            ValidationMsg="Please provide first name." Size="200" />
</asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span style="font-weight: bold;"><asp:Localize runat="server" Text="Last Name" 
                                                                    ID="locLastName" meta:resourcekey="locLastNameResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                                            </td>
                                                            <td>
                                                                <b>&nbsp;:&nbsp;</b>
                                                            </td>
                                                            <td>
                                                                <div style="float: left;">
                                                                    <asp:TextBox ID="txtLastName" runat="server" MaxLength="30" Width="270px" 
                                                                        Height="19px" meta:resourcekey="txtLastNameResource1"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left">
                                                                    <asp:RequiredFieldValidator ID="rfvLastName" runat="server" Display="Dynamic" ControlToValidate="txtLastName"
                                                                        ForeColor="Red" ValidationGroup="vgAddUser"><ucValidationContainer:ValidationContainer ID="rfvLastNameContainer" runat="server"
                                                                            ValidationMsg="Please provide last name." Size="200" />
</asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span style="font-weight: bold;"><asp:Localize runat="server" Text="Email" 
                                                                    ID="locEmail" meta:resourcekey="locEmailResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                                            </td>
                                                            <td>
                                                                <b>&nbsp;:&nbsp;</b>
                                                            </td>
                                                            <td>
                                                                <div style="float: left;">
                                                                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Width="270px" 
                                                                        Height="19px" meta:resourcekey="txtEmailResource1"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left">
                                                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" Display="Dynamic" ControlToValidate="txtEmail"
                                                                        ForeColor="Red" ValidationGroup="vgAddUser"><ucValidationContainer:ValidationContainer ID="rfvEmailContainer" runat="server"
                                                                            ValidationMsg="Please provide email-address." Size="200" />
</asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="revEmail" ControlToValidate="txtEmail" Display="Dynamic"
                                                                        runat="server" ForeColor="Red" ValidationGroup="vgAddUser" 
                                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"><ucValidationContainer:ValidationContainer ID="revEmailContainer" runat="server"
                                                                            ValidationMsg="Please enter proper email-address." Size="200" />
</asp:RegularExpressionValidator>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span style="font-weight: bold;"><asp:Localize runat="server" Text="User Name" 
                                                                    ID="locUsername" meta:resourcekey="locUsernameResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                                            </td> 
                                                            <td>
                                                                <b>&nbsp;:&nbsp;</b>
                                                            </td>
                                                            <td>
                                                                <div style="float: left;">
                                                                    <asp:TextBox ID="txtAdminUserName" runat="server" MaxLength="50" Width="270px" 
                                                                        Height="19px" meta:resourcekey="txtAdminUserNameResource1"></asp:TextBox>
                                                                </div>
                                                                <div style="float: left">
                                                                    <asp:RequiredFieldValidator ID="rfvUserName" runat="server" Display="Dynamic" ControlToValidate="txtAdminUserName"
                                                                        ForeColor="Red" ValidationGroup="vgAddUser"><ucValidationContainer:ValidationContainer ID="rfvUserNameContainer" runat="server"
                                                                            ValidationMsg="Please provide user name." Size="200" />
</asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span style="font-weight: bold;"><asp:Localize runat="server" Text="Is Active?" 
                                                                    ID="locIsActive" meta:resourcekey="locIsActiveResource1"></asp:Localize></span>
                                                            </td>
                                                            <td>
                                                                <b>&nbsp;:&nbsp;</b>
                                                            </td>
                                                            <td>
                                                                <div style="float: left;">
                                                                    <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" 
                                                                        meta:resourcekey="chkIsActiveResource1" />
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="trPasswordReset">
                                                            <td runat="server">
                                                            </td>
                                                            <td runat="server">
                                                                <b>&nbsp;&nbsp;</b>
                                                            </td>
                                                            <td runat="server">
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
                                                    <legend><asp:Localize runat="server" ID="locAccessType" Text="Access Type" 
                                                            meta:resourcekey="locAccessTypeResource1"></asp:Localize></legend>
                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td style="width: 140px;">
                                                                <span style="font-weight: bold;"><asp:Localize runat="server" Text="Role" ID="locRole" meta:resourcekey="locRoleResource1"></asp:Localize><span style="color: Red;">*</span>
                                                            </td>
                                                            <td>
                                                                <b>&nbsp;:&nbsp;</b>
                                                            </td>
                                                            <td>
                                                                <div style="float: left;">
                                                                    <asp:DropDownList ID="cmbRole" AutoPostBack="True" runat="server" CssClass="select"
                                                                        Width="270px" OnSelectedIndexChanged="cmbRole_SelectedIndexChanged" 
                                                                        meta:resourcekey="cmbRoleResource1">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div style="float: left">
                                                                    <asp:RequiredFieldValidator ID="rfvRole" runat="server" ControlToValidate="cmbRole"
                                                                        Display="Dynamic" ForeColor="Red" ValidationGroup="vgAddUser"><ucValidationContainer:ValidationContainer ID="rfvRoleContainer" runat="server" ValidationMsg="Please select role."
                                                                            Size="200" />
</asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="trRegion">
                                                            <td runat="server">
                                                                <span style="font-weight: bold;"><asp:Localize runat="server" Text="Region" ID="locRegion" meta:resourcekey="locRegionResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                                            </td>
                                                            <td runat="server">
                                                                <b>&nbsp;:&nbsp;</b>
                                                            </td>
                                                            <td runat="server">
                                                                <div style="float: left;">
                                                                    <asp:DropDownList ID="cmbRegion" AutoPostBack="True" runat="server" Width="270px"
                                                                        CssClass="select" OnSelectedIndexChanged="cmbRegion_SelectedIndexChanged">
                                                                        <asp:ListItem Text="--Select Region--" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div style="float: left">
                                                                    <asp:RequiredFieldValidator ID="rfvRegion" runat="server" Display="Dynamic" ControlToValidate="cmbRegion"
                                                                        ForeColor="Red" ValidationGroup="vgAddUser"><ucValidationContainer:ValidationContainer ID="rfvRegionContainer" runat="server"
                                                                            ValidationMsg="Please select region." Size="200" />
</asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="trCompany">
                                                            <td style="width: 140px;" runat="server">
                                                                <span style="font-weight: bold;"><asp:Localize runat="server" Text="Company Name" ID="locCompanyName"  meta:resourcekey="locCompanyNameResource1"></asp:Localize><span style="color: Red;">*</span>
                                                                </span>
                                                            </td>
                                                            <td runat="server">
                                                                <b>&nbsp;:&nbsp;</b>
                                                            </td>
                                                            <td runat="server">
                                                                <div style="float: left;">
                                                                    <asp:DropDownList ID="cmbCompany" runat="server" Width="270px" AutoPostBack="True"
                                                                        CssClass="select" OnSelectedIndexChanged="cmbCompany_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div style="float: left">
                                                                    <asp:RequiredFieldValidator ID="rfvcompanyName" runat="server" Display="Dynamic"
                                                                        ControlToValidate="cmbCompany" ForeColor="Red" ValidationGroup="vgAddUser"><ucValidationContainer:ValidationContainer ID="rfvCompanyContainer" runat="server"
                                                                            ValidationMsg="Please select company." Size="200" />
</asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="trBranch">
                                                            <td runat="server">
                                                                <span style="font-weight: bold;"><asp:Localize runat="server" Text="Branch Name" ID="locBranchName" meta:resourcekey="locBranchNameResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                                            </td>
                                                            <td runat="server">
                                                                <b>&nbsp;:&nbsp;</b>
                                                            </td>
                                                            <td runat="server">
                                                                <div style="float: left; line-height: 18px; overflow-y: scroll; height: 70px; border-width: 1px;
                                                                    border-style: solid; width: 270px">
                                                                    <asp:CheckBoxList ID="chkUserBranch" CssClass="checkListBox" runat="server" ClientValidationFunction="ValidateChkList">
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                                <div style="float: left">
                                                                    <asp:CustomValidator ID="cvBranch" ClientValidationFunction="ValidateChkList" runat="server"
                                                                        ValidationGroup="vgAddUser"><ucValidationContainer:ValidationContainer ID="cvBranchContainer" runat="server"
                                                                            ValidationMsg="Please select branch." Size="200" />
</asp:CustomValidator>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                         <tr runat="server" id="tr_User_Type">
                                                            <td style="width: 140px;" runat="server">
                                                                <span style="font-weight: bold;"><asp:Localize runat="server" Text="User Type" ID="locUserType"  meta:resourcekey="locUserTypeResource1"></asp:Localize><span style="color: Red;">*</span>
                                                                </span>
                                                            </td>
                                                            <td runat="server">
                                                                <b>&nbsp;:&nbsp;</b>
                                                            </td>
                                                            <td runat="server">
                                                                <div style="float: left;">
                                                                    <asp:DropDownList ID="cmbUserType" runat="server" Width="270px" 
            CssClass="select" meta:resourcekey="cmbUserTypeResource1">
                                                                        <asp:ListItem Selected="True" Value="0" meta:resourcekey="ListItemResource1">Website User</asp:ListItem>
                                                                        <asp:ListItem Value="1" meta:resourcekey="ListItemResource2">XMF User</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div style="float: left">
                                                                  
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="tr_Language">
                                                                <td id="td_Language" style="width: 140px;" runat="server">
                                                                    <span style="font-weight: bold;"><asp:Localize runat="server" Text="User Language" ID="locUserLanguage"  meta:resourcekey="locUserLanguageResource1"></asp:Localize><span style="color: Red;">*</span>
                                                                    </span>
                                                                </td>
                                                                <td id="Td2" runat="server">
                                                                    <b>&nbsp;:&nbsp;</b>
                                                                </td>
                                                                <td id="Td3" runat="server">
                                                                    <div style="float: left;">
                                                                        <asp:DropDownList ID="ddlLanguages" runat="server" Width="270px" CssClass="select" >                                                                        
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
                                                    <span> <asp:Localize runat="server" Text="Save" 
                                    ID="locSave" meta:resourcekey="locSaveResource1"></asp:Localize>
 </span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" 
                                    PostBackUrl="~/UserManagement.aspx">
                                                    <span> <asp:Localize runat="server" Text="Cancel" 
                                    ID="locCancel" meta:resourcekey="locCancelResource1"></asp:Localize>
 </span>
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
