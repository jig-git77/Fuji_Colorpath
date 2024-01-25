<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" EnableEventValidation="false"  CodeBehind="SyncMeasurementDevice.aspx.cs" Inherits="TaskeroWeb.SyncMeasurementDevice" %>

<%@ Register TagPrefix="custom" Namespace="Taskero.Business" Assembly="Taskero.Business" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<%@ Register Src="~/Common/AdminMenu.ascx" TagName="AdminMenu" TagPrefix="uc1" %>
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
                                                        <td>
                                                            <asp:Label ID="LBL_Error" runat="server" CssClass="ERROR"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="top">
                                                            <table cellspacing="1" cellpadding="2" border="0" id="TBLGRID" runat="server">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="BTN_AddSycnMeasurementDevice" runat="server" CssClass="BUTTON" 
                                                                            OnClick="BTN_AddSycnMeasurementDevice_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <rad:RadGrid ID="RG_SyncMeasurementDevice" runat="server" AutoGenerateColumns="False" 
                                                                            PagerStyle-Mode="NextPrevAndNumeric" AllowPaging="True" 
                                                                            AllowSorting="True" AllowFilteringByColumn="True"
                                                                            Width="920px" Skin="Green" PageSize="25" MasterTableView-EditFormSettings-EditFormType="Template"
                                                                            OnItemCommand="RG_SyncMeasurementDevice_ItemCommand" OnItemDataBound="RG_SyncMeasurementDevice_ItemDataBound"
                                                                            OnNeedDataSource="RG_SyncMeasurementDevice_NeedDataSource" OnPageIndexChanged="RG_SyncMeasurementDevice_PageIndexChanged"
                                                                            OnSortCommand="RG_SyncMeasurementDevice_SortCommand" 
                                                                            OnItemCreated="RG_SyncMeasurementDevice_ItemCreated" GridLines="None">
                                                                            <PagerStyle Mode="NextPrevAndNumeric" />
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                                            <ClientSettings>
                                                                                <Scrolling AllowScroll="true" ScrollHeight="400" />
                                                                            </ClientSettings>
                                                                            <MasterTableView>
                                                                                <RowIndicatorColumn Visible="False">
                                                                                    <HeaderStyle Width="20px" />
                                                                                </RowIndicatorColumn>
                                                                                <ExpandCollapseColumn Resizable="False" Visible="False">
                                                                                    <HeaderStyle Width="20px" />
                                                                                </ExpandCollapseColumn>
                                                                                <Columns>
                                                                                    <rad:GridBoundColumn DataField="CPSyncMeasurementDeviceID" HeaderText="CPSyncMeasurementDeviceID" 
                                                                                        Visible="false" UniqueName="CPSyncMeasurementDeviceID">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="BranchID" HeaderText="BranchID" Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="CompanyID" HeaderText="CompanyID" Visible="false">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="CompanyName" HeaderText="Company Name" ItemStyle-Width="150px"
                                                                                        HeaderStyle-Width="150px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                        <HeaderStyle Width="150px" />
                                                                                        <ItemStyle Width="150px" />
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="BranchName" HeaderText="Branch Name" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="CPSerialNUM" HeaderText="Dongle Serial No." AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="CPDongleID" HeaderText="CPDongleID" 
                                                                                        Visible="false" UniqueName="CPDongleID">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="SerialNumber" HeaderText="Serial Number" ItemStyle-Width="130px"
                                                                                        HeaderStyle-Width="130px" AutoPostBackOnFilter="true" 
                                                                                        CurrentFilterFunction="Contains">
                                                                                        <HeaderStyle Width="130px" />
                                                                                        <ItemStyle Width="130px" />
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="DeviceTypeID" HeaderText="DeviceTypeID" 
                                                                                        Visible="false" UniqueName="DeviceTypeID">
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridBoundColumn DataField="DeviceTypeName" HeaderText="Device Type" ItemStyle-Width="100px"
                                                                                        HeaderStyle-Width="100px" AutoPostBackOnFilter="true" 
                                                                                        CurrentFilterFunction="Contains">
                                                                                        <HeaderStyle Width="100px" />
                                                                                        <ItemStyle Width="100px" />
                                                                                    </rad:GridBoundColumn>  
                                                                                    <rad:GridBoundColumn DataField="IsActive" HeaderText="IsUsed" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                         <HeaderStyle Width="40px" />
                                                                                        <ItemStyle Width="40px" />
                                                                                    </rad:GridBoundColumn>
                                                                                    <rad:GridTemplateColumn HeaderText="Edit" AllowFiltering="false" ItemStyle-HorizontalAlign="Center"
                                                                                        HeaderStyle-Width="40px">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="IMGBTN_Edit" runat="server" CommandName="EditSyncMeasurementDevice" ImageUrl="~/Images/Edit-Icon.gif"
                                                                                                ToolTip="Edit" />
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle Width="40px" />
                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                    </rad:GridTemplateColumn>         
                                                                                      <rad:GridTemplateColumn HeaderText="Delete" AllowFiltering="false" ItemStyle-HorizontalAlign="Center"
                                                                                        HeaderStyle-Width="40px">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="IMGBTN_Delete" runat="server" CommandName="Delete" ImageUrl="~/Images/Delete-Icon.gif"
                                                                                                ToolTip="Delete"  />
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle Width="40px" />
                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                    </rad:GridTemplateColumn>                                                                          
                                                                                </Columns>
                                                                                <EditFormSettings EditFormType="Template">
                                                                                </EditFormSettings>
                                                                                <PagerStyle Mode="NumericPages" Position="Top" />
                                                                                <HeaderStyle HorizontalAlign="Center" />
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
                                                                    <td>
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
                                                                                        <asp:DropDownList ID="DDL_Branch" runat="server" CssClass="DROPDOWNLIST1" AutoPostBack="True" OnSelectedIndexChanged="DDL_Branch_SelectedIndexChanged" Style="width: 200px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="form-text" align="right">
                                                                                        <asp:Literal ID="LTRL_DongleSerialNo" runat="server"></asp:Literal>
                                                                                        <span class="REQ">*</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                       <asp:DropDownList ID="DDL_Dongle" runat="server" CssClass="DROPDOWNLIST1" Style="width: 200px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>                                                                               
                                                                                 <tr>
                                                                                    <td class="form-text" align="right">
                                                                                        <asp:Literal ID="LTRL_SerialNum" runat="server" ></asp:Literal>
                                                                                        <span class="REQ">*</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="TXT_SerialNum" runat="server" MaxLength="50" CssClass="TEXTBOX"
                                                                                            Style="width: 197px"></asp:TextBox></td>
                                                                                </tr>
                                                                                 <tr>
                                                                                    <td class="form-text" valign="top" align="right">
                                                                                        <asp:Literal ID="LTRL_DeviceType" runat="server"></asp:Literal>
                                                                                        <span class="REQ">*</span>&nbsp;:
                                                                                    </td>
                                                                                    <td>
                                                                                         <asp:DropDownList ID="DDL_DeviceType" runat="server" CssClass="DROPDOWNLIST1" Style="width: 200px">
                                                                                        </asp:DropDownList>
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
                                                                                        </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 10px" colspan="2">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <asp:Literal ID="LTRL_CPSyncMeasurementDeviceID" runat="server" Text="0" Visible="false"></asp:Literal>
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
