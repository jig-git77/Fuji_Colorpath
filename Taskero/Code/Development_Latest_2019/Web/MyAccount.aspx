<%@ Page Language="C#" MasterPageFile="~/Home.master"  EnableEventValidation="false" AutoEventWireup="true" CodeFile="MyAccount.aspx.cs"
    Inherits="MyAccount" %>

<%@ Register Src="Common/Menu.ascx" TagName="AdminMenu" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="940" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td><uc1:AdminMenu ID="UCAdminMenu" runat="server" /></td>
        </tr>
        <tr>
            <td>
                &nbsp; 

            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="1" cellpadding="2">
                    <tr>
                        <td class="green-title" align="left"><b><asp:Literal ID="LTRL_Title" runat="server"></asp:Literal></b></td>
                    </tr>
                    <tr>
                        <td class="green-form-bg">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                        <tr>
                                            <td align="left" style="padding-left: 20px; padding-bottom: 5px; padding-top: 5px;" class="ERROR">
                                                <asp:Literal ID="LblError" runat="server"></asp:Literal>
                                                &nbsp;<asp:Literal ID="LTRL_ID" runat="server" Visible="False">0</asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <table width="100%" class="form-text" align="center">
                                                    <tr>
                                                        <td valign="top" style="width: 50%">
                                                            <fieldset>
                                                                <legend><b><asp:Literal ID="LTRL_SubTitle" runat="server"></asp:Literal></b></legend>
                                                                <table border="0" cellspacing="1" cellpadding="2" width="100%">
                                                                    <tr>
                                                                        <td align="right" style="width: 130px">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="right" class="required-text-small">
                                                                            Fields marked with (<span class="ERROR">*</span>) are Required
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" style="height: 7px;">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" valign="top">
                                                                            <asp:Literal ID="LTRL_CompanyName" runat="server"></asp:Literal>&nbsp;:
                                                                        </td>
                                                                        <td class="TEXT">
                                                                            <asp:Literal ID="LTRL_CompanyNameDisplay" runat="server"></asp:Literal>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" valign="top">
                                                                            <asp:Literal ID="LTRL_BranchName" runat="server"></asp:Literal>&nbsp;:
                                                                        </td>
                                                                        <td class="TEXT">
                                                                            <asp:Literal ID="LTRL_BranchNameDisplay" runat="server"></asp:Literal>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" valign="top">
                                                                            <asp:Literal ID="LTRL_DongleName" runat="server"></asp:Literal>&nbsp;:
                                                                        </td>
                                                                        <td class="TEXT">
                                                                            <asp:Literal ID="LTRL_DongleNameDisplay" runat="server"></asp:Literal>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Literal ID="LTRL_FirstName" runat="server"></asp:Literal><span class="ERROR">*</span>&nbsp;:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="TXT_FirstName" runat="server" MaxLength="30" CssClass="TEXTBOX"
                                                                                Width="200px"></asp:TextBox>
                                                                            <ajax:FilteredTextBoxExtender ID="FIL_FirstName" runat="server" TargetControlID="TXT_FirstName"
                                                                                FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=" '">
                                                                            </ajax:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Literal ID="LTRL_LastName" runat="server"></asp:Literal><span class="ERROR">*</span>&nbsp;:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="TXT_LastName" runat="server" MaxLength="30" CssClass="TEXTBOX" Width="200px"></asp:TextBox>
                                                                            <ajax:FilteredTextBoxExtender ID="FIL_LastName" runat="server" TargetControlID="TXT_LastName"
                                                                                FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=" '">
                                                                            </ajax:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Literal ID="LTRL_Email" runat="server"></asp:Literal><span class="ERROR">*</span>&nbsp;:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="TXT_Email" runat="server" MaxLength="50" CssClass="TEXTBOX" Width="200px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" valign="top">
                                                                            <asp:Literal ID="LTRL_LoginID" runat="server"></asp:Literal>&nbsp;:
                                                                        </td>
                                                                        <td class="TEXT">
                                                                            <asp:Literal ID="LTRL_LoginIDDisplay" runat="server"></asp:Literal>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Literal ID="LTRL_LoginPass" runat="server"></asp:Literal><span class="ERROR">*</span>&nbsp;:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="TXT_LoginPass" runat="server" TextMode="Password" MaxLength="20"
                                                                                CssClass="TEXTBOX" Width="150px"></asp:TextBox>
                                                                            <ajax:FilteredTextBoxExtender ID="FilteredPassword" runat="server" TargetControlID="TXT_LoginPass"
                                                                                FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers" ValidChars="-=\_[]{};:'/?.">
                                                                            </ajax:FilteredTextBoxExtender>
                                                                            <ajax:PasswordStrength ID="PasswordStrength1" runat="server" TargetControlID="TXT_LoginPass"
                                                                                DisplayPosition="RightSide" StrengthIndicatorType="Text" PreferredPasswordLength="15"
                                                                                PrefixText="Strength:" TextStrengthDescriptions="Very Poor;Weak;Average;Strong;Excellent"
                                                                                StrengthStyles="TextIndicator_Strength1;TextIndicator_Strength2;TextIndicator_Strength3;TextIndicator_Strength4;TextIndicator_Strength5"
                                                                                MinimumNumericCharacters="0" MinimumSymbolCharacters="0" RequiresUpperAndLowerCaseCharacters="false" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Literal ID="LTRL_LoginConfirmPassword" runat="server"></asp:Literal><span class="ERROR">
                                                                                *</span>&nbsp;:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="TXT_LoginConfirmPassword" TextMode="Password" runat="server" MaxLength="20"
                                                                                CssClass="TEXTBOX" Width="150px"></asp:TextBox>
                                                                            <ajax:FilteredTextBoxExtender ID="FilteredConformPassword" runat="server" TargetControlID="TXT_LoginConfirmPassword"
                                                                                FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers">
                                                                            </ajax:FilteredTextBoxExtender>
                                                                            <asp:Button ID="BTN_DisplayPasswordField" runat="server" CssClass="BUTTON" Text="Show Password"
                                                                                ToolTip="Click here to show password field values" OnClick="BTN_DisplayPasswordField_Click" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">Allowed IP&nbsp;:</td>
                                                                        <td>
                                                                            <asp:Literal ID="LTRL_AllowedIP" runat="server"></asp:Literal>
                                                                        </td>
                                                                    </tr>   
                                                                    <tr>
                                                                        <td align="right">
                                                                            Email When Yellow
                                                                            &nbsp;:
                                                                        </td>
                                                                        <td class="TEXT">
                                                                            <asp:DropDownList ID="DDL_EmailWhenYellow" runat="server" CssClass="DROPDOWNLIST" Width="153px">
                                                                                <asp:ListItem Value="1" Text="Yes" Selected="True"></asp:ListItem>
                                                                                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            Email When Red
                                                                            &nbsp;:
                                                                        </td>
                                                                        <td class="TEXT">
                                                                            <asp:DropDownList ID="DDL_EmailWhenRed" runat="server" CssClass="DROPDOWNLIST" Width="153px">
                                                                                <asp:ListItem Value="1" Text="Yes" Selected="True"></asp:ListItem>
                                                                                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Literal ID="LTRL_Active" runat="server"></asp:Literal>
                                                                            &nbsp;:
                                                                        </td>
                                                                        <td class="TEXT">
                                                                            <asp:Literal ID="LTRL_ActiveDisplay" runat="server"></asp:Literal>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" style="height: 7px;">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Button ID="BTN_Update" runat="server" CssClass="BUTTON" OnClick="BTN_Update_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </fieldset>
                                                        </td>
                                                        <td valign="top" style="width: 50%">
                                                            <fieldset>
                                                                <legend><b><asp:Literal ID="LTRL_SubTitle1" runat="server"></asp:Literal></b></legend>
                                                                <table border="0" cellspacing="1" cellpadding="2">
                                                                    <tr>
                                                                        <td align="right">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" valign="top">
                                                                            <asp:Literal ID="LTRL_RoleAndPermission" runat="server"></asp:Literal>
                                                                            &nbsp;:
                                                                        </td>
                                                                        <td class="TEXT">
                                                                            <asp:Literal ID="LTRL_RoleAndPermissionDisplay" runat="server"></asp:Literal>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" style="height: 7px;">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </fieldset>
                                                            <fieldset id="FLD_PrintBuyerInfo" runat="server" visible="false">
                                                                <legend>
                                                                    <b><asp:Literal ID="LTRL_PrintBuyerInfo" runat="server">Print Buyer Information</asp:Literal></b>
                                                                </legend>
                                                                <table width="100%" border="0" cellspacing="1" cellpadding="2" class="form-text">
                                                                    <tr>
                                                                        <td align="right" style="width: 120px;">
                                                                            <asp:Literal ID="LTRL_Customer" runat="server"></asp:Literal>&nbsp;<span class="ERROR">*</span>&nbsp;:
                                                                        </td>
                                                                        <td>
                                                                            <asp:Literal ID="LTRL_CustomerValue" runat="server"></asp:Literal>
                                                                        </td>
                                                                    </tr>                                                                    
                                                                    <tr>
                                                                        <td align="right" style="width: 120px;">
                                                                            Branding&nbsp;:
                                                                        </td>
                                                                        <td>
                                                                            <asp:Literal ID="LTRL_BrandName" runat="server"></asp:Literal>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </fieldset>
                                                            <fieldset>
                                                                <legend class="subtitle">
                                                                    Reports
                                                                </legend>
                                                                    <ol style="padding-left:40px;padding-top:10px;padding-bottom:10px">
                                                                    <asp:Repeater ID="RPT_Reports" runat="server">
                                                                        <ItemTemplate>
                                                                            <li><%#Eval("ReportDesc") %></li>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                    </ol>
                                                            </fieldset>
                                                            <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
                                                            <ajax:ModalPopupExtender ID="ModalPopupExtender" runat="server" BehaviorID="ModalPopupBehavior"
                                                                TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="Panel1" BackgroundCssClass="modalBackground" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel1" runat="server" Style="display: none">
                                        <table style="width:500px;height:150px" border="0" cellspacing="0" cellpadding="2">
                                            <tr>
                                                <td class="green-title" align="left">
                                                    <asp:Literal ID="LTRL_BrandInfo" runat="server">Brand Information</asp:Literal>
                                                    <asp:ImageButton ID="hideModalPopup" runat="server" ImageUrl="Images/Close2.png"
                                                        OnClientClick="return hideModalPopup()" Style="position: absolute; border: none; right: 7px;
                                                        top: 5px; text-align: right;"  />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <iframe id="FrmPopup" src="" frameborder="0" scrolling="no" width="500px" height="150px">
                                                    </iframe>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script language="javascript" type="text/javascript">
  
     function hideModalPopup() 
    {
            ev.preventDefault();        
            var modalPopupBehavior = $find('ModalPopupBehavior');
            modalPopupBehavior.hide();
            document.getElementById("FrmPopup").src = "";
            return false;
    }
     function OpenMeasurementWindow()
    {       
        document.getElementById("FrmPopup").src = "UploadBrandFiles.aspx";
        var modalPopupBehavior = $find('ModalPopupBehavior');
        modalPopupBehavior.show();
        return false;
    }
    </script>

</asp:Content>
