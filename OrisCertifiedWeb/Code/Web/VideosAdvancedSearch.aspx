<%@ Page Language="C#" AutoEventWireup="true"  EnableEventValidation="false" CodeFile="VideosAdvancedSearch.aspx.cs" Inherits="VideosAdvancedSearch" %>

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
            <div>
                <table cellpadding="0" cellspacing="0" border="0" align="left" width="100%">
                    <tr>
                        <td class="ERROR">
                            <asp:Literal ID="LblError" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                                <table border="0" cellspacing="0" cellpadding="0" width="100%" >
                                    <tr>
                                        <td class="green-title"><b>
                                            <asp:Literal ID="LTRL_Title" runat="server" Text="Advanced Search"></asp:Literal></b></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table border="0" cellpadding="5" cellspacing="5" width="100%">
                                               
                                                <tr>
                                                    <td><b><asp:Literal ID="LTRL_Category" runat="server" Text="By Category"></asp:Literal></b></td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left:35px">
                                                        <asp:DataList ID="dlCategoryList" runat="server" RepeatColumns="3" Width="100%" RepeatDirection="Horizontal">
                                                            <ItemTemplate>
                                                                <asp:Literal ID="ltrlCategoryID" Visible="false" runat="server" Text='<%# Eval("CategoryID") %>'></asp:Literal>
                                                                <asp:CheckBox ID="chkCategory" runat="server" Text='<%# Eval("Description") %>' />
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                        
                                                        <div id="divDynamicCategories" runat="server" visible="false">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><hr /></td>
                                                </tr>
                                                <tr>
                                                    <td><b><asp:Literal ID="LTRL_KeyWords" runat="server" Text="By Keywords"></asp:Literal></b></td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left:35px"><asp:TextBox ID="TXT_KeyWords" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="center"> <asp:Button ID="BTN_Search" TabIndex="48" runat="server" CssClass="BUTTON" Text="Search" OnClick="BTN_Search_Click"></asp:Button></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                        </td>
                    </tr>
                </table>
            </div>
    </form>
</body>
</html>
