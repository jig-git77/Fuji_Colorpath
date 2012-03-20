<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddUserManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddUserManagement" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>

<script language="javascript">
    function ShowPassWord() {
        var txtPassWord = document.getElementById("<%= txtPassWord.ClientID %>");
        var lblPassWord = document.getElementById("<%= lblPassWord.ClientID %>");
        lblPassWord.innerHTML = txtPassWord.value;
        //alert(lblPassWord.value);
        return false;
    }

</script>

<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1>
            Add User Management</h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="10" width="97%">
                    <tr>
                        <td colspan="2">
                            <ucStatusbar:Statusbar ID="ucStatusbar" runat="server" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="admin_header" style="background-color:#CBF8ED; border:1px; border-style:solid; border-color:Gray;">
                            <div class="header">
                                <div class="content">
                                    <div class="text" style="float: left;">
                                        <span>Personal Information</span>
                                        </div>
                                    <div style="float: right"> </div>
                                <div class="clear"> </div>
                               </div>
                            </div>
                        </td>
                         <td class="admin_header" style="background-color:#CBF8ED; border:1px; border-style:solid; border-color:Gray;">
                            <div class="header">
                                <div class="content">
                                    <div class="text" style="float: left;">
                                        <span>Access Type</span>
                                        </div>
                                    <div style="float: right"> </div>
                                <div class="clear"> </div>
                               </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 50%;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr id="trListHeader" runat="server">
                                        <td>
                                            <asp:Panel ID="panelAddProjectDescription" runat="server" BorderStyle="None">
                                                <table>
                                                    <tr>
                                                        <td style="width: 140px;">
                                                            <span style="font-weight: bold;">Company Name: &nbsp<span style="color: Red;">*</span>
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <div style="float: left;">
                                                                <asp:DropDownList ID="cmbCompanyName" runat="server" Width="270px" AutoPostBack="true"
                                                                    CssClass="select" OnSelectedIndexChanged="cmbCompanyName_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div style="float: left">
                                                                <asp:RequiredFieldValidator ID="rfvCompnayName" runat="server" Display="Dynamic"
                                                                    ControlToValidate="cmbCompanyName" ForeColor="Red" ValidationGroup="vgAddUser">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please select compnay.
                                            </div>
                                            <div class="tooltip_bottom_left">
                                            </div>
                                            <div class="tooltip_bottom_right">
                                            </div>
                                        </div>
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="clear">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span style="font-weight: bold;">Branch Name: &nbsp<span style="color: Red;">*</span>
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <div style="float: left; line-height:18px; overflow-y: scroll; height:70px; border-width: 1px; border-style: solid;
                                                                width: 270px">
                                                                <asp:CheckBoxList ID="chkUserBranch" CssClass="checkListBox"  runat="server">
                                                                </asp:CheckBoxList>
                                                            </div>
                                                            <div style="float: left">
                                                            </div>
                                                            <div class="clear">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span style="font-weight: bold;">Region: &nbsp<span style="color: Red;">*</span>
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <div style="float: left;">
                                                                <asp:DropDownList ID="cmbRegion" Enabled="false" runat="server" Width="270px" CssClass="select">
                                                                    <asp:ListItem Text="--Select Region--" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div style="float: left">
                                                                <asp:RequiredFieldValidator ID="rfvRegion" runat="server" Display="Dynamic" ControlToValidate="cmbRegion"
                                                                    ForeColor="Red" ValidationGroup="vgAddUser">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please select region.
                                            </div>
                                            <div class="tooltip_bottom_left">
                                            </div>
                                            <div class="tooltip_bottom_right">
                                            </div>
                                        </div>
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="clear">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span style="font-weight: bold;">First Name: &nbsp<span style="color: Red;">*</span>
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <div style="float: left;">
                                                                <asp:TextBox ID="txtFirstName" runat="server" MaxLength="30" Width="270px" Height="19px"></asp:TextBox>
                                                            </div>
                                                            <div style="float: left">
                                                                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" Display="Dynamic" ControlToValidate="txtFirstName"
                                                                    ForeColor="Red" ValidationGroup="vgAddUser">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please provide first name.
                                            </div>
                                            <div class="tooltip_bottom_left">
                                            </div>
                                            <div class="tooltip_bottom_right">
                                            </div>
                                        </div>
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="clear">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span style="font-weight: bold;">Last Name: &nbsp<span style="color: Red;">*</span>
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <div style="float: left;">
                                                                <asp:TextBox ID="txtLastName" runat="server" MaxLength="30" Width="270px" Height="19px"></asp:TextBox>
                                                            </div>
                                                            <div style="float: left">
                                                                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" Display="Dynamic" ControlToValidate="txtLastName"
                                                                    ForeColor="Red" ValidationGroup="vgAddUser">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please provide last name.
                                            </div>
                                            <div class="tooltip_bottom_left">
                                            </div>
                                            <div class="tooltip_bottom_right">
                                            </div>
                                        </div>
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="clear">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span style="font-weight: bold;">Email: &nbsp<span style="color: Red;">*</span>
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <div style="float: left;">
                                                                <asp:TextBox ID="txtEmail" runat="server" MaxLength="30" Width="270px" Height="19px"></asp:TextBox>
                                                            </div>
                                                            <div style="float: left">
                                                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" Display="Dynamic" ControlToValidate="txtEmail"
                                                                    ForeColor="Red" ValidationGroup="vgAddUser">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please provide email-address.
                                            </div>
                                            <div class="tooltip_bottom_left">
                                            </div>
                                            <div class="tooltip_bottom_right">
                                            </div>
                                        </div>
                                                                </asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="revEmail" ControlToValidate="txtEmail" Display="Dynamic"
                                                                    runat="server" ForeColor="Red" ValidationGroup="vgAddUser" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                                  <div class="tooltip_round_box" style="width: 230px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please enter proper email-address.
                                            </div>
                                            <div class="tooltip_bottom_left">
                                            </div>
                                            <div class="tooltip_bottom_right">
                                            </div>
                                        </div>
                                                                </asp:RegularExpressionValidator>
                                                            </div>
                                                            <div class="clear">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span style="font-weight: bold;">User Name: &nbsp<span style="color: Red;">*</span>
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <div style="float: left;">
                                                                <asp:TextBox ID="txtUserName" runat="server" MaxLength="50" Width="270px" Height="19px"></asp:TextBox>
                                                            </div>
                                                            <div style="float: left">
                                                                <asp:RequiredFieldValidator ID="rfvUserName" runat="server" Display="Dynamic" ControlToValidate="txtUserName"
                                                                    ForeColor="Red" ValidationGroup="vgAddUser">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please provide user name.
                                            </div>
                                            <div class="tooltip_bottom_left">
                                            </div>
                                            <div class="tooltip_bottom_right">
                                            </div>
                                        </div>
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="clear">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span style="font-weight: bold;">PassWord: &nbsp<span style="color: Red;">*</span>
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <div style="float: left;">
                                                                <asp:TextBox ID="txtPassWord" runat="server" TextMode="Password" MaxLength="40" Width="100px"
                                                                    Height="19px"></asp:TextBox>
                                                            </div>
                                                             <div style="float: left;">
                                                                &nbsp; &nbsp; <asp:Label ID="lblPassWord" runat="server" CssClass="showpassword" Text =""></asp:Label>   
                                                            </div>
                                                            <div style="float: left">
                                                                <asp:RequiredFieldValidator ID="rfvPassWord" 
                                                                runat="server" 
                                                                Display="Dynamic" 
                                                                ControlToValidate="txtPassWord"
                                                                ForeColor="Red" ValidationGroup="vgAddUser">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please provide password.
                                            </div>
                                            <div class="tooltip_bottom_left">
                                            </div>
                                            <div class="tooltip_bottom_right">
                                            </div>
                                        </div>
                                                            </asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="clear">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span style="font-weight: bold;">Confirm Password: &nbsp<span style="color: Red;">*</span>
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <div style="float: left;">
                                                                <asp:TextBox ID="txtConfirmPass" runat="server" MaxLength="40" TextMode="Password"
                                                                    Width="100px" Height="19px"></asp:TextBox>
                                                            </div>
                                                            <div style="float: left">
                                                                &nbsp;
                                                                 <asp:RequiredFieldValidator ID="rfvConfirmPass" 
                                                                runat="server" 
                                                                Display="Dynamic" 
                                                                ControlToValidate="txtConfirmPass"
                                                                ForeColor="Red" ValidationGroup="vgAddUser">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please provide confirm password.
                                            </div>
                                            <div class="tooltip_bottom_left">
                                            </div>
                                            <div class="tooltip_bottom_right">
                                            </div>
                                        </div>
                                                            </asp:RequiredFieldValidator>
                                                            </div>
                                                            <div style="float: left;">
                                                                <asp:LinkButton 
                                                                        ID="lbtnShow" 
                                                                        runat="server" 
                                                                        OnClientClick="javascript:ShowPassWord(); return false;" 
                                                                        CssClass="gray_bt"
                                                                        >
                                                                    <span> Show Password </span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="clear">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span style="font-weight: bold;">Is Active ?: &nbsp &nbsp</span>
                                                        </td>
                                                        <td>
                                                            <div style="float: left;">
                                                                <asp:CheckBox ID="chkIsActive" runat="server" />
                                                            </div>
                                                            <div class="clear">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                        </td>
                        <td valign="top" style="width: 50%;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td style="width: 140px;">
                                            <span style="font-weight: bold;">Role: &nbsp;<span style="color: Red;">*</span>
                                            </span>
                                        </td>
                                        <td>
                                            <div style="float: left;">
                                                <asp:DropDownList ID="cmbRole" runat="server" CssClass="select" Width="270px">
                                                </asp:DropDownList>
                                            </div>
                                            <div style="float: left">
                                                <asp:RequiredFieldValidator ID="rfvRole" runat="server" ControlToValidate="cmbRole"
                                                    Display="Dynamic" ForeColor="Red" ValidationGroup="vgAddUser">
                                            <div class="tooltip_round_box" style="width: 200px;">
                                            <div class="tooltip_arrow" style="margin-top: -3px;">
                                                &nbsp;</div>
                                            <div class="tooltip_top_left">
                                            </div>
                                            <div class="tooltip_top_right">
                                            </div>
                                            <div class="tooltip_inside">
                                                Please select role.
                                            </div>
                                            <div class="tooltip_bottom_left">
                                            </div>
                                            <div class="tooltip_bottom_right">
                                            </div>
                                        </div>
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <div style="padding-left: 3px;">
                                <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" OnClientClick="javascript:ValidCompany()"  OnClick="lbtnSave_Click"
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
