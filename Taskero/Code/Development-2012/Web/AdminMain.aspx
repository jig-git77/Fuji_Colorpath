<%@ Page Language="C#" MasterPageFile="~/Home.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="AdminMain.aspx.cs"
    Inherits="AdminMain" %>

<%@ Register Src="Common/AdminMenu.ascx" TagName="AdminMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="940px" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
            <td>
                <table width="940" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <uc1:AdminMenu ID="UCAdminMenu" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LblError" runat="server" CssClass="ERROR"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="admin-home" align="center">
                            <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
                            <asp:DataList ID="DL_AccessType" runat="server" RepeatColumns="6" OnItemDataBound="DL_AccessType_ItemDataBound"
                                RepeatDirection="Horizontal">
                                <ItemTemplate>
                                    <table border="0" cellspacing="0" cellpadding="1" align="center">
                                        <tr>
                                            <td>
                                                <td align="center" valign="bottom" style="width: 130px" runat="server" id="TD_Role">
                                                    <a href='<%#DataBinder.Eval(Container.DataItem, "Page")%>' title='<%#DataBinder.Eval(Container.DataItem, "Desc")%>'>
                                                        <img src='<%#DataBinder.Eval(Container.DataItem, "Image")%>' border="0" alt='<%#DataBinder.Eval(Container.DataItem, "Desc")%>' /><br />
                                                        <%#DataBinder.Eval(Container.DataItem, "Desc")%></a>
                                                </td>
                                                <td width="5" align="center" valign="top" id="TD_divider" runat="server" visible="false">
                                                    <img src="images/admin-icon-divider.png" alt="divider" border="0" />
                                                </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
