<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="RoleManagement.aspx.cs" Inherits="OrisWeb.RoleManagement" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<%@ Register Src="~/Common/AdminMenu.ascx" TagName="AdminMenu" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="940" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <uc1:AdminMenu ID="UCAdminMenu" runat="server" />
                    </td>
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
                                <td class="green-title" align="left">
                                    <asp:Literal ID="LTRL_Title" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="green-form-bg">
                                    <table width="940px" border="0" cellspacing="5" cellpadding="0" align="center" class="maintable">
                                        <tr>
                                            <td>
                                                <asp:Label ID="LblError" runat="server" CssClass="ERROR"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table border="0" cellspacing="1" cellpadding="2" class="form-text" align="center">
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="BtnUpdate" runat="server" CssClass="BUTTON" Enabled="false" OnClick="BtnUpdate_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="left">
                                                            <div style="width: 910px">
                                                                <fieldset>
                                                                    <legend>
                                                                        <asp:Literal ID="LTRL_SubTitle" runat="server"></asp:Literal>
                                                                    </legend>
                                                                    <table width="100%" border="0" cellspacing="1" cellpadding="2" class="form-text">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:GridView ID="GVRoles" runat="server" AllowPaging="false" AutoGenerateColumns="false"
                                                                                    DataKeyNames="PermissionID" OnRowDataBound="GVRoles_RowDataBound" Width="100%">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="DisplayRow" HeaderText="" ItemStyle-Font-Bold="true" ItemStyle-Width="300px">
                                                                                        </asp:BoundField>
                                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Country Admin"
                                                                                            ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="ChkCA" runat="server" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Company Admin"
                                                                                            ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="ChkCMA" runat="server" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Branch Admin"
                                                                                            ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="ChkBA" runat="server" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Print Buyer"
                                                                                            ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="ChkPA" runat="server" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Support User"
                                                                                            ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="ChkSA" runat="server" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Site User" ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="ChkSIA" runat="server" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </fieldset>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="top">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
