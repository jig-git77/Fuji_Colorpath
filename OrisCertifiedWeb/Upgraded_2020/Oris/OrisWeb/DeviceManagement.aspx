<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="DeviceManagement.aspx.cs" EnableEventValidation="false" Inherits="OrisWeb.DeviceManagement" %>

<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>

<%@ Register Src="~/Common/AdminMenu.ascx" TagName="AdminMenu" TagPrefix="uc1" %>
<%@ Register TagPrefix="custom" Namespace="Oris.Business" Assembly="Oris.Business" %>
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
                                                                        <asp:Button ID="BTN_AddDevice" runat="server" CssClass="BUTTON" OnClick="BTN_AddDevice_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <rad:RadGrid ID="RG_Device" runat="server" AutoGenerateColumns="false" PagerStyle-Mode="NextPrevAndNumeric"
                                                                            ShowHeader="true" AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="true"
                                                                            Skin="Green" PageSize="25" MasterTableView-EditFormSettings-EditFormType="Template"
                                                                            OnItemCommand="RG_Device_ItemCommand" OnItemCreated="RG_Device_ItemCreated" OnItemDataBound="RG_Device_ItemDataBound"
                                                                            OnNeedDataSource="RG_Device_NeedDataSource" OnPageIndexChanged="RG_Device_PageIndexChanged"
                                                                            OnSortCommand="RG_Device_SortCommand" Width="920px">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                                            <ClientSettings>
                                                                                <Scrolling AllowScroll="true" ScrollHeight="400" />
                                                                            </ClientSettings>
                                                                            <PagerStyle Position="Top" />
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <MasterTableView>
                                                                                <PagerStyle Mode="NumericPages" Position="Top" />
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <Columns>
                                                                                    <rad:GridBoundColumn DataField="CPDeviceID" HeaderText="CPDeviceID" Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="CompanyID" HeaderText="CompanyID" Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="BranchID" HeaderText="BranchID" Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="CPDongleID" HeaderText="DongleID" Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="CompanyName" HeaderText="Company Name" ItemStyle-Width="160px"
                                                                                        HeaderStyle-Width="160px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="BranchName" HeaderText="Branch Name" ItemStyle-Width="120px" HeaderStyle-Width="120px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="CPSerialNUM" HeaderText="Dongle Serial No." AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="CPDeviceDesc" HeaderText="Device Description" ItemStyle-Width="180px" HeaderStyle-Width="180px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <custom:FilteringColumn UniqueName="MeasurementType" DataField="CPMeasurementType"
                                                                                        HeaderText="Measurement Type" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "CPMeasurementType")%>
                                                                                        </ItemTemplate>
                                                                                    </custom:FilteringColumn>
                                                                                    <custom:FilteringColumn UniqueName="DDeviceType" DataField="CPDeviceTypeDesc" HeaderText="Device Type"
                                                                                        Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container.DataItem, "CPDeviceTypeDesc")%>
                                                                                        </ItemTemplate>
                                                                                    </custom:FilteringColumn>
                                                                                    <rad:GridBoundColumn DataField="CPMeasurementTypeID" HeaderText="MeasurementTypeID"
                                                                                        Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="CPDeviceTypeID" HeaderText="DeviceTypeID" Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridTemplateColumn HeaderText="Edit" AllowFiltering="false" ItemStyle-HorizontalAlign="Center"
                                                                                        HeaderStyle-Width="40px">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="IMGBTN_Edit" runat="server" CommandName="EditDevice" ImageUrl="~/Images/Edit-Icon.gif"
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
                                                                                </Columns>
                                                                                <PagerStyle AlwaysVisible="True" />
                                                                                <PagerTemplate>
                                                                                    <div style="float: left">
                                                                                        <asp:Literal ID="LTRL_RecordText" runat="server"></asp:Literal>&nbsp;
                                                                                        <asp:ImageButton ID="IMGBTN_First" CommandName="Page" CausesValidation="false" CommandArgument="First"
                                                                                            runat="server" ImageUrl="Images/Nav_FirstPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                                        <asp:ImageButton ID="IMGBTN_Prev" CommandName="Page" CausesValidation="false" CommandArgument="Prev"
                                                                                            runat="server" ImageUrl="Images/Nav_PreviousPage_off.gif" align="absmiddle">
                                                                                        </asp:ImageButton>
                                                                                        <asp:ImageButton ID="IMGBTN_Next" CommandName="Page" CausesValidation="false" CommandArgument="Next"
                                                                                            runat="server" ImageUrl="Images/Nav_NextPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                                        <asp:ImageButton ID="IMGBTN_Last" CommandName="Page" CausesValidation="false" CommandArgument="Last"
                                                                                            runat="server" ImageUrl="Images/Nav_LastPage_off.gif" align="absmiddle"></asp:ImageButton>
                                                                                        &nbsp;
                                                                                        <asp:TextBox ID="TXT_PageNumber" runat="server" Columns="3" Text='<%# (int)DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex") + 1 %>'
                                                                                            Visible="False" />
                                                                                        <asp:LinkButton ID="LBTN_Go" runat="server" CommandName="CustomChangePage" Visible="False">Go</asp:LinkButton>
                                                                                        <font class="text">| Records</font>
                                                                                        <asp:DropDownList ID="DDL_RecordPage" runat="server" AutoPostBack="true" CommandName="Page"
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
                                                                style="width: 500px; vertical-align: top">
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <fieldset>
                                                                            <legend class="subtitle">
                                                                                <asp:Literal ID="LTRL_SubTitle" runat="server"></asp:Literal>
                                                                            </legend>
                                                                            <table cellpadding="1" cellspacing="2">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <asp:Literal ID="LTRL_DeviceID" runat="server" Visible="False">0</asp:Literal>
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
                                                                                            Style="width: 200px" OnSelectedIndexChanged="DDL_Company_SelectedIndexChanged">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="form-text" align="right">
                                                                                        <asp:Literal ID="LTRL_BranchName" runat="server"></asp:Literal>
                                                                                        <span class="REQ">*</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="DDL_Branch" runat="server" AutoPostBack="True" CssClass="DROPDOWNLIST1"
                                                                                            Style="width: 200px" OnSelectedIndexChanged="DDL_Branch_SelectedIndexChanged">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="form-text" align="right">
                                                                                        <asp:Literal ID="LTRL_DongleName" runat="server"></asp:Literal>
                                                                                        <span class="REQ">*</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="DDL_Dongle" runat="server" CssClass="DROPDOWNLIST1" Style="width: 200px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="form-text" align="right">
                                                                                        <asp:Literal ID="LTRL_DeviceDescription" runat="server"></asp:Literal>
                                                                                        <span class="REQ">*</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="TXT_DeviceDescription" runat="server" MaxLength="50" CssClass="TEXTBOX"
                                                                                            Style="width: 197px" onkeypress="CheckAlphaNumeric(event);"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="form-text" align="right">
                                                                                        <asp:Literal ID="LTRL_MeasurementType" runat="server"></asp:Literal>
                                                                                        <span class="REQ">*</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="DDL_MeasurementType" runat="server" CssClass="DROPDOWNLIST1"
                                                                                            Style="width: 200px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="form-text" align="right">
                                                                                        <asp:Literal ID="LTRL_DeviceType" runat="server"></asp:Literal>
                                                                                        <span class="REQ">*</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="DDL_DeviceType" runat="server" CssClass="DROPDOWNLIST1" Style="width: 200px">
                                                                                        </asp:DropDownList>
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
                                                                                            OnClick="BTN_Cancel_Click" />
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