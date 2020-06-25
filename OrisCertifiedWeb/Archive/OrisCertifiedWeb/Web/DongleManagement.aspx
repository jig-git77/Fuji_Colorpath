<%@ Page Language="C#" MasterPageFile="~/Home.master"  EnableEventValidation="false" AutoEventWireup="true" CodeFile="DongleManagement.aspx.cs"
    Inherits="DongleManagement" %>

<%@ Register TagPrefix="custom" Namespace="Fuji.Business" Assembly="Fuji.Business" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>

<%@ Register Src="Common/AdminMenu.ascx" TagName="AdminMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="JS/CommonFunctions.js"></script>

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
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="middle">
                            <table width="100%" border="0" cellspacing="1" cellpadding="2">
                                <tr>
                                    <td class="green-title" align="left">
                                        <asp:Literal ID="LTRL_Title" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="center">
                                        <asp:Label ID="LblError" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" class="green-form-bg">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <table width="940px" border="0" cellspacing="5" cellpadding="0" align="center" class="maintable">
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="LBL_Error" runat="server" CssClass="ERROR"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="top">
                                                            <table cellspacing="1" cellpadding="2" border="0" id="TBLGRID" runat="server">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="BTN_AddDongle" runat="server" CssClass="BUTTON" OnClick="BTN_AddDongle_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <rad:RadGrid ID="RG_Dongle" runat="server" AutoGenerateColumns="false" PagerStyle-Mode="NextPrevAndNumeric"
                                                                            ShowHeader="true" AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="true"
                                                                            Width="920px" Skin="Green" PageSize="25" MasterTableView-EditFormSettings-EditFormType="Template"
                                                                            OnItemCommand="RG_Dongle_ItemCommand" OnItemDataBound="RG_Dongle_ItemDataBound"
                                                                            OnNeedDataSource="RG_Dongle_NeedDataSource" OnPageIndexChanged="RG_Dongle_PageIndexChanged"
                                                                            OnSortCommand="RG_Dongle_SortCommand" OnItemCreated="RG_Dongle_ItemCreated">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                                            <ClientSettings>
                                                                                <Scrolling AllowScroll="true" ScrollHeight="400" />
                                                                            </ClientSettings>
                                                                            <MasterTableView>
                                                                                <PagerStyle Mode="NumericPages" Position="Top" />
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <Columns>
                                                                                    <rad:GridBoundColumn DataField="CPDongleID" HeaderText="CPDongleID" Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="BranchID" HeaderText="BranchID" Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="CompanyID" HeaderText="CompanyID" Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="CompanyName" HeaderText="Company Name" ItemStyle-Width="150px"
                                                                                        HeaderStyle-Width="150px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="BranchName" HeaderText="Branch Name" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="CPSerialNUM" HeaderText="Dongle Serial No." AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="DongleSerial" HeaderText="Dongle Description" ItemStyle-Width="130px"
                                                                                        HeaderStyle-Width="130px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="LastModifiedBy" HeaderText="LastModifiedBy" Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                    <custom:FilterColumnByDate UniqueName="LastModifiedDate" DataField="LastModifiedDate"
                                                                                        HeaderText="LastModifiedDate" Visible="true" SortExpression="LastModifiedDate"
                                                                                        ItemStyle-Width="130px" HeaderStyle-Width="130px" ItemStyle-VerticalAlign="Top">
                                                                                        <HeaderStyle VerticalAlign="Top" />
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "LastModifiedDate")%>
                                                                                        </ItemTemplate>
                                                                                    </custom:FilterColumnByDate>
                                                                                    <rad:GridBoundColumn DataField="IsUsed" HeaderText="IsUsed" ItemStyle-HorizontalAlign="Center">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridTemplateColumn HeaderText="Edit" AllowFiltering="false" ItemStyle-HorizontalAlign="Center"
                                                                                        HeaderStyle-Width="40px">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="IMGBTN_Edit" runat="server" CommandName="EditDongle" ImageUrl="~/Images/Edit-Icon.gif"
                                                                                                ToolTip="Edit" />
                                                                                        </ItemTemplate>
                                                                                    </rad:GridTemplateColumn>
                                                                                    <rad:GridTemplateColumn HeaderText="Delete" AllowFiltering="false" ItemStyle-HorizontalAlign="Center"
                                                                                        HeaderStyle-Width="40px">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="IMGBTN_Delete" runat="server" CommandName="Delete" ImageUrl="~/Images/Delete-Icon.gif"
                                                                                                ToolTip="Delete" />
                                                                                        </ItemTemplate>
                                                                                    </rad:GridTemplateColumn>
                                                                                    <rad:GridBoundColumn DataField="DongleDescription" HeaderText="Dongle Description"
                                                                                        Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                </Columns>
                                                                                <PagerStyle AlwaysVisible="True" />
                                                                                <PagerTemplate>
                                                                                    <div style="float: left">
                                                                                        <asp:Literal ID="LTRL_RecordText" runat="server"></asp:Literal>&nbsp;
                                                                                        <asp:ImageButton ID="LBTN_First" CommandName="Page" CausesValidation="false" CommandArgument="First"
                                                                                            runat="server" ImageUrl="Images/Nav_FirstPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                                        <asp:ImageButton ID="LBTN_Prev" CommandName="Page" CausesValidation="false" CommandArgument="Prev"
                                                                                            runat="server" ImageUrl="Images/Nav_PreviousPage_off.gif" align="absmiddle">
                                                                                        </asp:ImageButton>
                                                                                        <asp:ImageButton ID="LBTN_Next" CommandName="Page" CausesValidation="false" CommandArgument="Next"
                                                                                            runat="server" ImageUrl="Images/Nav_NextPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                                        <asp:ImageButton ID="LBTN_Last" CommandName="Page" CausesValidation="false" CommandArgument="Last"
                                                                                            runat="server" ImageUrl="Images/Nav_LastPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                                        &nbsp;
                                                                                        <asp:TextBox ID="tbPageNumber" runat="server" Columns="3" Text='<%# (int)DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex") + 1 %>'
                                                                                            Visible="False" />
                                                                                        <asp:LinkButton ID="LBTN_Go" runat="server" CommandName="CustomChangePage" Visible="False">Go</asp:LinkButton>
                                                                                        <font class="text">| Records</font>
                                                                                        <asp:DropDownList ID="DDL_RecordPage" runat="server" AutoPostBack="true" CommandName="Page"
                                                                                            class="inputbox">
                                                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                                                            <asp:ListItem Value="25" Selected="True">25</asp:ListItem>
                                                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                                                            <asp:ListItem Value="100">100</asp:ListItem>
                                                                                            <asp:ListItem Value="500">500</asp:ListItem>
                                                                                            <asp:ListItem Value="1000">1000</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                        per page
                                                                                    </div>
                                                                                    <div style="float: right">
                                                                                        <asp:Button ID="BTN_Reset" TabIndex="48" runat="server" CssClass="BUTTON" CommandName="ResetFilter">
                                                                                        </asp:Button>
                                                                                        &nbsp;
                                                                                    </div>
                                                                                </PagerTemplate>
                                                                            </MasterTableView>
                                                                        </rad:RadGrid>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="left" style="padding-top: 10px">
                                                            <table cellspacing="0" cellpadding="0" border="0" id="TBLFORM" runat="server" align="center"
                                                                style="width: 400px; vertical-align: top">
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <fieldset>
                                                                            <legend class="subtitle">
                                                                                <asp:Literal ID="LTRL_SubTitle" runat="server"></asp:Literal>
                                                                            </legend>
                                                                            <table cellpadding="1" cellspacing="2">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        &nbsp;
                                                                                        <asp:Literal ID="LTRL_BranchID" runat="server" Visible="False">0</asp:Literal>
                                                                                    </td>
                                                                                    <td align="right" class="required-text-small">
                                                                                        Fields marked with (<span class="required-text-small-red">*</span>) are Required
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="form-text" align="right">
                                                                                        <asp:Literal ID="LTRL_CompanyName" runat="server"></asp:Literal>
                                                                                        <span class="REQ">*</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="DDL_Company" runat="server" AutoPostBack="True" CssClass="DROPDOWNLIST1"
                                                                                            OnSelectedIndexChanged="DDL_Company_SelectedIndexChanged" Style="width: 200px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="form-text" align="right">
                                                                                        <asp:Literal ID="LTRL_BranchName" runat="server"></asp:Literal>
                                                                                        <span class="REQ">*</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="DDL_Branch" runat="server" CssClass="DROPDOWNLIST1" Style="width: 200px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="form-text" align="right">
                                                                                        <asp:Literal ID="LTRL_DongleSerialNo" runat="server"></asp:Literal>
                                                                                        <span class="REQ">*</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="TXT_DongleSerialNo" runat="server" MaxLength="50" CssClass="TEXTBOX"
                                                                                            Style="width: 197px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="form-text" valign="top" align="right">
                                                                                        <asp:Literal ID="LTRL_DongleDescription" runat="server"></asp:Literal>
                                                                                        <span class="REQ">*</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="TXT_DongleDescription" runat="server" MaxLength="50" CssClass="TEXTBOX"
                                                                                            TextMode="MultiLine" Columns="23" Height="50px" onkeypress="CheckAlphaNumeric(event);"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right" class="form-text">
                                                                                        <asp:Literal ID="LTRL_Active" runat="server"></asp:Literal>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:CheckBox ID="CHK_IsActive" runat="server" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 10px" colspan="2">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2" align="center">
                                                                                        <asp:Button ID="BTN_AddSave" runat="server" CssClass="BUTTON" Style="width: 70px"
                                                                                            OnClick="BTN_AddSave_Click" />
                                                                                        &nbsp;<asp:Button ID="BTN_Cancel" runat="server" CssClass="BUTTON" Style="width: 70px"
                                                                                            OnClick="BTN_Cancel_Click" />&nbsp;
                                                                                        <asp:Button ID="BTN_Save_Device" runat="server" CssClass="BUTTON" Style="width: 150px"
                                                                                            OnClick="BTN_Save_Device_Click" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 10px" colspan="2">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <asp:Literal ID="LTRL_DongleID" runat="server" Text="0" Visible="false"></asp:Literal>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </fieldset>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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
