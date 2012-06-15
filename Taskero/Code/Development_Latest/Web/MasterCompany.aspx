<%@ Page Language="C#" MasterPageFile="~/Home.master"  EnableEventValidation="false" AutoEventWireup="true" CodeFile="MasterCompany.aspx.cs"
    Inherits="MasterCompany" %>

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
                                    <td align="center" class="green-form-bg">
                                        <asp:UpdatePanel ID="UP_MasterCompany" runat="server">
                                            <ContentTemplate>
                                                <table width="940px" border="0" cellspacing="5" cellpadding="0" align="center" class="maintable">
                                                    <tr>
                                                        <td><asp:Label ID="LBL_Error" runat="server" CssClass="ERROR"></asp:Label></td>
                                                    </tr>
                                                    <tr id="TRGridTable" runat="server">
                                                        <td valign="top">
                                                            <table cellspacing="1" cellpadding="2" border="0" id="TBLGRID" runat="server" width="920px">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="BTN_AddCompany" runat="server" CssClass="BUTTON" OnClick="BTN_AddCompany_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <rad:RadGrid ID="RG_MasterCompany" runat="server" AutoGenerateColumns="false" PagerStyle-Mode="NextPrevAndNumeric"
                                                                            ShowHeader="true" AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="true"
                                                                            Width="920px" OnPageIndexChanged="RG_MasterCompany_PageIndexChanged" OnNeedDataSource="RG_MasterCompany_NeedDataSource"
                                                                            Skin="Green" PageSize="25" OnSortCommand="RG_MasterCompany_SortCommand" OnItemCommand="RG_MasterCompany_ItemCommand"
                                                                            OnItemDataBound="RG_MasterCompany_ItemDataBound" MasterTableView-EditFormSettings-EditFormType="Template"
                                                                            OnItemCreated="RG_MasterCompany_ItemCreated">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="false" Width="920px" />
                                                                            <ClientSettings>
                                                                                <Scrolling AllowScroll="true" ScrollHeight="400" />
                                                                            </ClientSettings>
                                                                            <PagerStyle Position="Top" />
                                                                            <HeaderStyle HorizontalAlign="Center" Width="920px" />
                                                                            <MasterTableView>
                                                                                <PagerStyle Mode="NumericPages" Position="Top" Width="910px" />
                                                                                <HeaderStyle HorizontalAlign="Center" Width="920px" />
                                                                                <Columns>
                                                                                    <rad:GridBoundColumn DataField="CompanyID" HeaderText="CompanyID" Visible="false"
                                                                                        UniqueName="column1">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="BrandID" HeaderText="BrandID" Visible="false" UniqueName="column2">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="CompanyName" HeaderText="Company Name" UniqueName="column3"
                                                                                        Visible="True" ItemStyle-Width="450px" HeaderStyle-Width="450px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="BrandName" UniqueName="BrandName" Visible="False"
                                                                                        HeaderText="Brand Name1" ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="BrandName" HeaderText="Brand Name" UniqueName="column4"
                                                                                        SortExpression="BrandName" ItemStyle-Width="350px" HeaderStyle-Width="350px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridTemplateColumn HeaderText="Edit" AllowFiltering="false" ItemStyle-HorizontalAlign="Center"
                                                                                        ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="IMGBTN_Edit" runat="server" CommandName="EditCompany" ImageUrl="~/Images/Edit-Icon.gif"
                                                                                                ToolTip="Edit" />
                                                                                        </ItemTemplate>
                                                                                    </rad:GridTemplateColumn>
                                                                                    <rad:GridTemplateColumn HeaderText="Delete" AllowFiltering="false" ItemStyle-HorizontalAlign="Center"
                                                                                        ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="IMGBTN_Delete" runat="server" CommandName="Delete" ImageUrl="~/Images/Delete-Icon.gif"
                                                                                                ToolTip="Delete" />
                                                                                        </ItemTemplate>
                                                                                    </rad:GridTemplateColumn>
                                                                                </Columns>
                                                                                <PagerStyle AlwaysVisible="True" />
                                                                                <PagerTemplate>
                                                                                    <div style="float: left">
                                                                                        <asp:Literal ID="LTRL_RecordText" runat="server"></asp:Literal>
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
                                                                                            <asp:ListItem Value="25">25</asp:ListItem>
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
                                                                            <table cellpadding="2" cellspacing="1">
                                                                                <tr>
                                                                                    <td>
                                                                                        &nbsp;<asp:Literal ID="LTRL_CompanyID" runat="server" Text="0" Visible="false"></asp:Literal> 
                                                                                    </td>
                                                                                    <td align="right" class="required-text-small">
                                                                                        Fields marked with (<span class="required-text-small-red">*</span>) are Required
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="form-text" style="padding-left: 10px">
                                                                                        <asp:Literal ID="LTRL_CompanyName" runat="server"></asp:Literal><span class="required-text-small-red">*</span>&nbsp;:&nbsp;
                                                                                    </td>
                                                                                    <td align="left">
                                                                                        <asp:TextBox ID="TXT_CompanyName" runat="server" MaxLength="50" CssClass="TEXTBOX"
                                                                                            Style="width: 250px" onkeypress="CheckAlphaNumeric(event);"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="form-text" style="padding-left: 10px">
                                                                                        <asp:Literal ID="LTRL_BrandName" runat="server"></asp:Literal>&nbsp;:&nbsp;
                                                                                    </td>
                                                                                    <td align="left">
                                                                                        <asp:DropDownList ID="DDL_BrandName" runat="server" CssClass="DROPDOWNLIST" Style="width: 253px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 10px" colspan="2">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2" align="center">
                                                                                        <asp:Button ID="BTN_AddSave" runat="server" CssClass="BUTTON" OnClick="BTN_AddSave_Click"
                                                                                            Style="width: 70px" />
                                                                                        &nbsp;<asp:Button ID="BTN_Cancel" runat="server" CssClass="BUTTON" OnClick="BTN_Cancel_Click"
                                                                                            Style="width: 70px" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 10px" colspan="2">
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
                </table>
            </td>
        </tr>
    </table>
</asp:Content>