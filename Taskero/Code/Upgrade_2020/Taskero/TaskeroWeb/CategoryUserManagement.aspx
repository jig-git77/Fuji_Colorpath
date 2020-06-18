<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CategoryUserManagement.aspx.cs" EnableEventValidation="false" Inherits="TaskeroWeb.CategoryUserManagement" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>FUJIFILM Graphic Systems U.S.A., Inc.</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <meta name="keywords" content="FUJIFILM Graphic Systems U.S.A., Inc." />
    <meta name="description" content="FUJIFILM Graphic Systems U.S.A., Inc." />
    <link href="CSS/Default.css" rel="stylesheet" type="text/css" />
</head>
<body class="green-form-bg">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UP_SupportUser" runat="server">
        <ContentTemplate>
            <div>
                <table cellpadding="0" cellspacing="5" border="0" align="left" width="100%">
                    <tr>
                        <td class="ERROR">
                            <asp:Literal ID="LblError" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <fieldset>
                                <legend><b>
                                    <asp:Literal ID="LTRL_Assign_CategoryUser" runat="server" Text="Category User Permission"></asp:Literal></b>
                                </legend>
                                <table border="0" cellspacing="1" cellpadding="2" align="left">
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="LBL_Error" runat="server" CssClass="ERROR"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Literal ID="LTRL_CompanyName" runat="server" Text="Company"></asp:Literal>&nbsp;:
                                        </td>
                                        <td>
                                            <asp:Panel ID="PNL_Company" runat="server" ScrollBars="Auto" CssClass="CHECKBOXLIST"
                                                Width="350px">
                                                <asp:CheckBoxList ID="CHK_Company" runat="server" CssClass="CHECKBOX" AutoPostBack="true"
                                                    Width="300px" OnSelectedIndexChanged="CHK_Company_SelectedIndexChanged">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Literal ID="LTRL_BranchName" runat="server" Text="Branch"></asp:Literal>&nbsp;:
                                        </td>
                                        <td align="left">
                                            <asp:Panel ID="PNL_Branch" runat="server" ScrollBars="Auto" CssClass="CHECKBOXLIST"
                                                Visible="false">
                                                <asp:CheckBoxList ID="CHK_Branch" runat="server" CssClass="CHECKBOX" AutoPostBack="true"
                                                    Width="300px" OnSelectedIndexChanged="CHK_Branch_SelectedIndexChanged">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Literal ID="LTRL_Users" runat="server" Text="Users"></asp:Literal>&nbsp;:
                                        </td>
                                        <td align="left">
                                            <asp:Panel ID="PNL_Users" runat="server" ScrollBars="Auto" CssClass="CHECKBOXLIST"
                                                Visible="false">
                                                <asp:CheckBoxList ID="CHK_Users" runat="server" CssClass="CHECKBOX" AutoPostBack="true"
                                                    Width="300px" OnSelectedIndexChanged="CHK_Users_SelectedIndexChanged">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button ID="BTN_Save" TabIndex="48" runat="server" CssClass="BUTTON" CommandName="ResetFilter"
                                                OnClick="BTN_Save_Click" Text="Save"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
