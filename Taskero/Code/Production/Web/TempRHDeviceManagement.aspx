<%@ Page Language="C#" MasterPageFile="~/Home.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="TempRHDeviceManagement.aspx.cs"
    Inherits="TempRHDeviceManagement" %>

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
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <table width="940px" border="0" cellspacing="5" cellpadding="0" align="center" class="maintable">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblError" runat="server" CssClass="ERROR"></asp:Label><asp:Literal ID="LTRL_ID" runat="server" Visible="False">0</asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <tr id="TRGridTable" runat="server">
                                                        <td valign="top">
                                                            <table cellspacing="5" cellpadding="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="BTN_AddTempRHDEVICES" runat="server" CssClass="BUTTON" OnClick="BTN_AddTempRHDEVICES_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <rad:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="True" AllowPaging="True"
                                                                            AllowSorting="True" AutoGenerateColumns="False" GridLines="None" Width="920px"
                                                                            PageSize="25" Skin="Green" OnNeedDataSource="RadGrid1_NeedDataSource" OnSortCommand="RadGrid1_SortCommand"
                                                                            OnItemCommand="RadGrid1_ItemCommand" MasterTableView-EditFormSettings-EditFormType="Template"
                                                                            OnItemCreated="RadGrid1_ItemCreated" OnItemDataBound="RadGrid1_ItemDataBound">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                                            <ClientSettings>
                                                                                <Scrolling AllowScroll="true" ScrollHeight="400" />
                                                                            </ClientSettings>
                                                                            <PagerStyle Position="Top" />
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <MasterTableView>
                                                                                <Columns>
                                                                                    <rad:GridBoundColumn DataField="TempRHDeviceID" HeaderText="TempRHDeviceID" UniqueName="column10"
                                                                                        Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="BranchName" HeaderText="BranchName" UniqueName="column1"
                                                                                        SortExpression="BranchName" ItemStyle-Width="240px" HeaderStyle-Width="240px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="TRHDeviceSerialNum" HeaderText="TRHDeviceSerialNum"
                                                                                        UniqueName="column2" SortExpression="TRHDeviceSerialNum" ItemStyle-Width="200px"
                                                                                        HeaderStyle-Width="200px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="TRHDeviceTypeDesc" HeaderText="TRHDevice Type" UniqueName="column3"
                                                                                        SortExpression="TRHDeviceTypeDesc" ItemStyle-Width="120px" HeaderStyle-Width="120px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="TargetTemp" HeaderText="Target Temperature" UniqueName="column4"
                                                                                        SortExpression="TargetTemp" ItemStyle-Width="120px" HeaderStyle-Width="120px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="TargetRH" HeaderText="Target RH" UniqueName="column6"
                                                                                        SortExpression="TargetRH" ItemStyle-Width="120px" HeaderStyle-Width="120px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridTemplateColumn AllowFiltering="false" HeaderText="Edit" ItemStyle-Width="40px"
                                                                                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="IMGBTN_Edit" runat="server" CommandName="EditTempRhDevice" ImageUrl="~/Images/Edit-Icon.gif"
                                                                                                ToolTip="Edit" />
                                                                                        </ItemTemplate>
                                                                                    </rad:GridTemplateColumn>
                                                                                    <rad:GridTemplateColumn AllowFiltering="false" HeaderText="Delete" ItemStyle-Width="45px"
                                                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="45px">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="IMGBTN_Delete" runat="server" CommandName="Delete" ImageUrl="~/Images/Delete-Icon.gif"
                                                                                                ToolTip="Delete" />
                                                                                        </ItemTemplate>
                                                                                    </rad:GridTemplateColumn>
                                                                                </Columns>
                                                                                <PagerStyle AlwaysVisible="True" Position="Top" />
                                                                                <PagerTemplate>
                                                                                    <div style="float: left">
                                                                                        <asp:Literal ID="LTRL_RecordText" runat="server"></asp:Literal>&nbsp;
                                                                                        <asp:ImageButton ID="lbFirst" CommandName="Page" CausesValidation="false" CommandArgument="First"
                                                                                            runat="server" ImageUrl="Images/Nav_FirstPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                                        <asp:ImageButton ID="lbPrev" CommandName="Page" CausesValidation="false" CommandArgument="Prev"
                                                                                            runat="server" ImageUrl="Images/Nav_PreviousPage_off.gif" align="absmiddle">
                                                                                        </asp:ImageButton>
                                                                                        <asp:ImageButton ID="lbNext" CommandName="Page" CausesValidation="false" CommandArgument="Next"
                                                                                            runat="server" ImageUrl="Images/Nav_NextPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                                        <asp:ImageButton ID="lbLast" CommandName="Page" CausesValidation="false" CommandArgument="Last"
                                                                                            runat="server" ImageUrl="Images/Nav_LastPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                                        &nbsp;
                                                                                        <asp:TextBox ID="tbPageNumber" runat="server" Columns="3" Text='<%# (int)DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex") + 1 %>'
                                                                                            Visible="False" />
                                                                                        <asp:LinkButton ID="lbGo" runat="server" CommandName="CustomChangePage" Visible="False">Go</asp:LinkButton>
                                                                                        <font class="text">| Records</font>
                                                                                        <asp:DropDownList ID="DDlRecordPage" runat="server" AutoPostBack="true" CommandName="Page"
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
                                                                                        <asp:Button ID="btnReset" TabIndex="48" runat="server" CssClass="BUTTON" CommandName="ResetFilter">
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
                                                    <tr id="TRMainTable" runat="server">
                                                        <td align="center">
                                                            <table class="form-text">
                                                                <tr>
                                                                    <td>
                                                                        <fieldset>
                                                                            <legend class="subtitle">
                                                                                <asp:Literal ID="LTRL_SubTitle" runat="server"></asp:Literal></legend>
                                                                            <table width="100%" border="0" cellspacing="1" cellpadding="2" class="form-text">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td align="right" class="required-text-small">
                                                                                        Fields marked with (<span class="ERROR">*</span>) are Required
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <asp:Literal ID="LTRL_CompanyName" runat="server"></asp:Literal><span class="ERROR">
                                                                                        *</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="DDL_Company" runat="server" CssClass="DROPDOWNLIST" AutoPostBack="True"
                                                                                            Width="200px" OnSelectedIndexChanged="DDL_Company_SelectedIndexChanged">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <asp:Literal ID="LTRL_BranchName" runat="server"></asp:Literal><span class="ERROR">
                                                                                        *</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="DDL_Branch" runat="server" CssClass="DROPDOWNLIST" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="DDL_Branch_SelectedIndexChanged">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right" valign="top">
                                                                                        <asp:Literal ID="LTRL_Devices" runat="server"></asp:Literal><span class="ERROR">
                                                                                        *</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Panel ID="PNL_Devices" runat="server" ScrollBars="Auto" CssClass="CHECKBOXLIST">
                                                                                            <asp:CheckBoxList ID="CHK_Devices" CssClass="CHECKBOX" runat="server">
                                                                                            </asp:CheckBoxList>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <asp:Literal ID="LTRL_TRHDeviceSerialNum" runat="server"></asp:Literal><span class="ERROR">
                                                                                        *</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="TXT_TRHDeviceSerialNum" runat="server" MaxLength="60" CssClass="TEXTBOX"
                                                                                            Width="197px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <asp:Literal ID="LTRL_TRHDeviceType" runat="server"></asp:Literal><span class="ERROR">
                                                                                        *</span>&nbsp;:
                                                                                    </td>
                                                                                    <td style="margin-left: 40px">
                                                                                        <asp:DropDownList ID="DDL_TRHDeviceType" runat="server" CssClass="DROPDOWNLIST" Width="200px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <asp:Literal ID="LTRL_TargetTemp" runat="server"></asp:Literal><span class="ERROR">
                                                                                        *</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="TXT_TargetTemp" runat="server" MaxLength="100" CssClass="TEXTBOX"
                                                                                            Width="100px" onkeypress="CheckDouble(this,event);"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <asp:Literal ID="LTRL_TargetRH" runat="server"></asp:Literal><span class="ERROR">
                                                                                        *</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="TXT_TargetRH" runat="server" MaxLength="100" CssClass="TEXTBOX"
                                                                                            Width="100px" onkeypress="CheckDouble(this,event);"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2" align="center">
                                                                                        <asp:Button ID="BTN_AddSave" runat="server" CssClass="BUTTON" OnClick="BTN_AddSave_Click" />
                                                                                        &nbsp;<asp:Button ID="BTN_Cancel" runat="server" CssClass="BUTTON" OnClick="BTN_Cancel_Click" />
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
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
