<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="SupportUserBranch1.aspx.cs" Inherits="OrisWeb.SupportUserBranch1" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" Style="display: none" CssClass="green-form-bg"
        Width="900px" Height="600px">
        <asp:Panel ID="Panel3" runat="server" Style="cursor: move; background-color: #DDDDDD;
            border: solid 1px Gray; color: Black">
            <div class="green-title">
                <table>
                    <tr>
                        <td align="left" style="width: 95%">
                            <asp:Literal ID="LTRL_ModelTitle" runat="server"></asp:Literal>
                        </td>
                        <td align="right" style="padding-left: 100px">
                            <asp:ImageButton ID="IMGBTN_Close" runat="server" ImageUrl="~/Images/Close.png" Style="cursor: pointer" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <div>
            <p style="text-align: center;">
                <table cellpadding="0" cellspacing="5" border="0" align="center">
                    <tr>
                        <td>
                            <asp:Label ID="LBL_Error" runat="server" CssClass="ERROR"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset>
                                <legend>
                                    <asp:Literal ID="LTRL_AssignSupportUser" runat="server"></asp:Literal>
                                </legend>
                                <table border="0" cellspacing="1" cellpadding="2" class="form-text" align="left">
                                    <tr>
                                        <td align="right">
                                            <asp:Literal ID="LTRL_ID" runat="server" Visible="false">0</asp:Literal>
                                        </td>
                                        <td align="right" class="required-text-small">
                                            Fields marked with (<span class="required-text-small-red">*</span>) are Required
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Literal ID="LTRL_UserBranch" runat="server"></asp:Literal>
                                            <span class="required-text-small-red">*</span>&nbsp;:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDL_UserBranch" runat="server" CssClass="DROPDOWNLIST" AutoPostBack="true"
                                                OnSelectedIndexChanged="DDL_UserBranch_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td align="right">
                                            <asp:Literal ID="LTRL_SupportUser" runat="server"></asp:Literal>
                                            <span class="required-text-small-red">*</span>&nbsp;:
                                        </td>
                                        <td>
                                            <asp:DropDownList CssClass="DROPDOWNLIST" ID="DDL_SupportUser" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Button CssClass="BUTTON" ID="BTN_UpdateSupportUser" runat="server" Text="Update"
                                                OnClick="BTN_UpdateSupportUser_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div>
                                                <rad:RadGrid ID="RG_SupportUser" runat="server" AllowFilteringByColumn="True" AllowPaging="True"
                                                    AllowSorting="True" AutoGenerateColumns="False" GridLines="None" Width="100%"
                                                    PageSize="25" Skin="Green" MasterTableView-EditFormSettings-EditFormType="Template"
                                                    OnItemCommand="RG_SupportUser_ItemCommand" OnItemDataBound="RG_SupportUser_ItemDataBound"
                                                    OnNeedDataSource="RG_SupportUser_NeedDataSource" OnPageIndexChanged="RG_SupportUser_PageIndexChanged"
                                                    OnSortCommand="RG_SupportUser_SortCommand">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                    <ClientSettings>
                                                        <Scrolling AllowScroll="true" ScrollHeight="400" />
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
                                                        <PagerTemplate>
                                                            <div style="float: left">
                                                                <asp:Literal ID="LTRL_RecordText" runat="server"></asp:Literal>&nbsp;
                                                                <asp:ImageButton ID="IBTN_First" CommandName="Page" CausesValidation="false" CommandArgument="First"
                                                                    runat="server" ImageUrl="Images/Nav_FirstPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                <asp:ImageButton ID="IBTN_Prev" CommandName="Page" CausesValidation="false" CommandArgument="Prev"
                                                                    runat="server" ImageUrl="Images/Nav_PreviousPage_off.gif" align="absmiddle">
                                                                </asp:ImageButton>
                                                                <asp:ImageButton ID="IBTN_Next" CommandName="Page" CausesValidation="false" CommandArgument="Next"
                                                                    runat="server" ImageUrl="Images/Nav_NextPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                <asp:ImageButton ID="IBTN_Last" CommandName="Page" CausesValidation="false" CommandArgument="Last"
                                                                    runat="server" ImageUrl="Images/Nav_LastPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                &nbsp;
                                                                <asp:TextBox ID="TXT_PageNumber" runat="server" Columns="3" Text='<%# (int)DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex") + 1 %>'
                                                                    Visible="False" />
                                                                <asp:LinkButton ID="LBTN_Go" runat="server" CommandName="CustomChangePage" Visible="False">Go</asp:LinkButton>
                                                                <font class="text">| Records</font>
                                                                <asp:DropDownList ID="DDl_RecordPage" runat="server" AutoPostBack="true" CommandName="Page"
                                                                    class="inputbox">
                                                                    <asp:ListItem Value="5">5</asp:ListItem>
                                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                                    <asp:ListItem Value="25" Selected>25</asp:ListItem>
                                                                    <asp:ListItem Value="50">50</asp:ListItem>
                                                                    <asp:ListItem Value="100">100</asp:ListItem>
                                                                    <asp:ListItem Value="500">500</asp:ListItem>
                                                                    <asp:ListItem Value="1000">1000</asp:ListItem>
                                                                </asp:DropDownList>
                                                                per page
                                                            </div>
                                                            <div style="float: right">
                                                                <asp:Button ID="BTN_Reset" TabIndex="48" runat="server" CssClass="BUTTON" Text="Clear Filter"
                                                                    CommandName="ResetFilter"></asp:Button>
                                                                &nbsp;
                                                            </div>
                                                        </PagerTemplate>
                                                    </MasterTableView>
                                                </rad:RadGrid>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset>
                                <legend>
                                    <asp:Literal ID="LTRL_AssignUserDongle" runat="server"></asp:Literal>
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
                                            <asp:Literal ID="LTRL_UserDongle" runat="server"></asp:Literal>
                                            <span class="required-text-small-red">*</span>&nbsp;:
                                        </td>
                                        <td>
                                            <asp:DropDownList CssClass="DROPDOWNLIST" ID="DDL_UserDongle" runat="server" OnSelectedIndexChanged="DDL_UserDongle_SelectedIndexChanged">
                                            </asp:DropDownList>
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
            </p>
        </div>
    </asp:Panel>
    <%--<ajax:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="BTN_AddSupportUser"
        PopupControlID="Panel1" BackgroundCssClass="modalBackground" OkControlID="BTN_UpdateAssignDongle"
        OnOkScript="onOk()" CancelControlID="IMGBTN_Close" DropShadow="true" PopupDragHandleControlID="Panel3" />--%>
</asp:Content>
