<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageUserVideoAccess.aspx.cs" EnableEventValidation="false" Inherits="OrisWeb.ManageUserVideoAccess" %>
<%@ Register Assembly="RadComboBox.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>FUJIFILM Graphic Systems U.S.A., Inc.</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <meta name="keywords" content="FUJIFILM Graphic Systems U.S.A., Inc." />
    <meta name="description" content="FUJIFILM Graphic Systems U.S.A., Inc." />
    <link href="CSS/Default.css" rel="stylesheet" type="text/css" />
</head>
<body  style="background:#efefef;">
    <form id="form1" runat="server" class="green-form-bg">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UP_UserCategory" runat="server">
        <ContentTemplate>
            <div  style="height:100%; width:100%">
                <table cellpadding="0" cellspacing="5" border="0" align="left" width="100%">
                    <tr>
                        <td class="ERROR"><asp:Literal ID="LblError" runat="server"></asp:Literal></td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <fieldset>
                                <legend>
                                    <b><asp:Literal ID="LTRL_Assign_VideoAccess" runat="server"></asp:Literal></b>
                                </legend>
                                <table border="0" cellspacing="1" cellpadding="2" align="left">
                                    <tr>
                                        <td align="right">
                                            <asp:Literal ID="LTRL_BranchID" runat="server" Visible="false">0</asp:Literal><asp:Literal ID="LTRL_CompanyID" runat="server" Visible="false">0</asp:Literal>
                                        </td>
                                        <td align="right" class="required-text-small">
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:DataGrid ID="GV_User_And_Categories" runat="server" 
                                                AutoGenerateColumns="false" Width="550px" 
                                                 OnItemDataBound="GV_User_And_Categories_RowDataBound" 
                                                onitemcommand="GV_User_And_Categories_ItemCommand">
                                                <Columns>
                                                    <asp:TemplateColumn HeaderStyle-Width="30px" HeaderText="Category" ItemStyle-VerticalAlign="Top">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="LTRL_UserCategory_ID" runat="server" Visible="false"></asp:Literal>
                                                            <asp:Literal ID="LTRL_UserCategory" runat="server"></asp:Literal>
                                                            <rad:RadComboBox runat="server" ID="DDL_Categories" CssClass="DROPDOWNLIST" Width="160px" Height="200px">
                                                                                            </rad:RadComboBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle VerticalAlign="Top" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderStyle-Width="150px" HeaderText="User" HeaderStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:Panel ID="PNL_Users" runat="server" ScrollBars="Auto" CssClass="CHECKBOXLIST">
                                                                <asp:CheckBoxList ID="CHK_Users" runat="server" Width="200px">
                                                                </asp:CheckBoxList>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="30px" Wrap="False" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" HeaderText="Save">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="IMGBTN_Save" runat="server" CommandName="SaveUserCategory" ImageUrl="~/Images/Icon-Save.gif"
                                                                ToolTip="Click to save" />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="60px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="IMGBTN_Delete" runat="server" CommandName="DeleteUserCategory" ImageUrl="~/Images/Delete-Icon.gif" ToolTip="Click to delete" />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="60px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
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