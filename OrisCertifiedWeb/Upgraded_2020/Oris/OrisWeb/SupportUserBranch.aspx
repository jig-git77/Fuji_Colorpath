<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupportUserBranch.aspx.cs" Inherits="OrisWeb.SupportUserBranch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="5" border="0" align="left">
                    <tr>
                        <td>
                            <asp:Label ID="LBL_Error" runat="server" CssClass="ERROR"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <fieldset>
                                <legend>
                                    <asp:Label Font-Bold="true" ID="LBL_AssignSupportUser" runat="server" style="color:Black"></asp:Label>
                                </legend>
                                <table border="0" cellspacing="1" cellpadding="2" class="form-text" align="left">
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="LBL_ID" runat="server" Visible="false">0</asp:Label>
                                        </td>
                                        <td align="right" class="required-text-small">
                                            Fields marked with (<span class="required-text-small-red">*</span>) are Required
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="LBL_UserBranch" runat="server"></asp:Label>
                                            <span class="required-text-small-red">*</span>&nbsp;:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDL_UserBranch" runat="server" CssClass="DROPDOWNLIST">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td align="right">
                                            <asp:Label ID="LBL_SupportUser" runat="server"></asp:Label>
                                            <span class="required-text-small-red">*</span>&nbsp;:
                                        </td>
                                        <td>
                                            <asp:DropDownList CssClass="DROPDOWNLIST" ID="DDL_SupportUser" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div>
                                                <rad:RadGrid ID="RG_SupportUserBranch" runat="server" AllowFilteringByColumn="True"
                                                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" GridLines="None"
                                                    Width="100%" PageSize="25" Skin="Green" MasterTableView-EditFormSettings-EditFormType="Template">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                    <ClientSettings>
                                                        <Scrolling AllowScroll="true" ScrollHeight="400"/>
                                                    </ClientSettings>
                                                    <PagerStyle Position="Top" />
                                                    <MasterTableView AllowSorting="true" AllowNaturalSort="false">
                                                        <NoRecordsTemplate>
                                                            No Data Found.</NoRecordsTemplate>
                                                        <EditFormSettings>
                                                            <EditColumn UniqueName="EditCommandColumn1">
                                                            </EditColumn>
                                                        </EditFormSettings>
                                                        <PagerStyle Mode="NumericPages" Position="Top" />
                                                        <RowIndicatorColumn Visible="False">
                                                            <HeaderStyle Width="30px"></HeaderStyle>
                                                        </RowIndicatorColumn>
                                                        <ExpandCollapseColumn Visible="False" Resizable="False">
                                                            <HeaderStyle Width="20px"></HeaderStyle>
                                                        </ExpandCollapseColumn>
                                                        <Columns>
                                                            <rad:GridTemplateColumn>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CHKBOX1" runat="server" />
                                                                </ItemTemplate>
                                                            </rad:GridTemplateColumn>
                                                            <rad:GridBoundColumn DataField="TaskeroUserID" HeaderText="ID" Visible="false">
                                                            </rad:GridBoundColumn>
                                                            <rad:GridBoundColumn DataField="LoginID" HeaderText="User Name" SortExpression="LoginID">
                                                            </rad:GridBoundColumn>
                                                            <rad:GridBoundColumn DataField="FirstName" HeaderText="First Name" SortExpression="FirstName">
                                                            </rad:GridBoundColumn>
                                                            <rad:GridBoundColumn DataField="LastName" HeaderText="Last Name" SortExpression="LastName">
                                                            </rad:GridBoundColumn>
                                                            <rad:GridBoundColumn DataField="Email" HeaderText="Email" SortExpression="Email">
                                                            </rad:GridBoundColumn>
                                                            <rad:GridTemplateColumn HeaderText="Delete" AllowFiltering="false" HeaderStyle-Width="45px">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LBTN_Delete" runat="server" Text="Delete" CommandName="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </rad:GridTemplateColumn>
                                                        </Columns>
                                                        <PagerStyle AlwaysVisible="True" />
                                                    </MasterTableView>
                                                </rad:RadGrid>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Button CssClass="BUTTON" ID="BTN_UpdateSupportUser" runat="server" Text="Update"
                                                OnClick="BTN_UpdateSupportUser_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset>
                                <legend>
                                    <asp:Label Font-Bold="true" ID="LBL_AssignUserDongle" runat="server" style="color:Black"></asp:Label>
                                </legend>
                                <table border="0" cellspacing="1" cellpadding="2" class="form-text" align="center">
                                    <tr>
                                        <td align="right">
                                        </td>
                                        <td align="right" class="required-text-small">
                                            Fields marked with (<span class="required-text-small-red">*</span>) are Required
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="LBL_UserDongle" runat="server"></asp:Label>
                                            <span class="required-text-small-red">*</span>&nbsp;:
                                        </td>
                                        <td>
                                            <asp:DropDownList CssClass="DROPDOWNLIST" ID="DDL_UserDongle" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div>
                                                <rad:RadGrid ID="RG_SupportUserDongle" runat="server" AllowFilteringByColumn="True"
                                                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" GridLines="None"
                                                    Width="100%" PageSize="25" Skin="Green" MasterTableView-EditFormSettings-EditFormType="Template">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                    <ClientSettings>
                                                        <Scrolling AllowScroll="true" ScrollHeight="400"/>
                                                    </ClientSettings>
                                                    <PagerStyle Position="Top" />
                                                    <MasterTableView AllowSorting="true" AllowNaturalSort="false">
                                                        <NoRecordsTemplate>
                                                            No Data Found.</NoRecordsTemplate>
                                                        <EditFormSettings>
                                                            <EditColumn UniqueName="EditCommandColumn1">
                                                            </EditColumn>
                                                        </EditFormSettings>
                                                        <PagerStyle Mode="NumericPages" Position="Top" />
                                                        <RowIndicatorColumn Visible="False">
                                                            <HeaderStyle Width="30px"></HeaderStyle>
                                                        </RowIndicatorColumn>
                                                        <ExpandCollapseColumn Visible="False" Resizable="False">
                                                            <HeaderStyle Width="20px"></HeaderStyle>
                                                        </ExpandCollapseColumn>
                                                        <Columns>
                                                            <rad:GridTemplateColumn>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                                </ItemTemplate>
                                                            </rad:GridTemplateColumn>
                                                            <rad:GridBoundColumn DataField="TaskeroUserID" HeaderText="ID" Visible="false">
                                                            </rad:GridBoundColumn>
                                                            <rad:GridBoundColumn DataField="LoginID" HeaderText="User Name" SortExpression="LoginID">
                                                            </rad:GridBoundColumn>
                                                            <rad:GridBoundColumn DataField="FirstName" HeaderText="First Name" SortExpression="FirstName">
                                                            </rad:GridBoundColumn>
                                                            <rad:GridBoundColumn DataField="LastName" HeaderText="Last Name" SortExpression="LastName">
                                                            </rad:GridBoundColumn>
                                                            <rad:GridBoundColumn DataField="Email" HeaderText="Email" SortExpression="Email">
                                                            </rad:GridBoundColumn>
                                                            <rad:GridTemplateColumn HeaderText="Delete" AllowFiltering="false" HeaderStyle-Width="45px">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" Text="Delete" CommandName="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </rad:GridTemplateColumn>
                                                        </Columns>
                                                        <PagerStyle AlwaysVisible="True" />
                                                    </MasterTableView>
                                                </rad:RadGrid>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Button CssClass="BUTTON" ID="BTN_UpdateAssignDongle" runat="server" Text="Update"
                                                OnClick="BTN_UpdateAssignDongle_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <
    </div>
    </form>
</body>
</html>
