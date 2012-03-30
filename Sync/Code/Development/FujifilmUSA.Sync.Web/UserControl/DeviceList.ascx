<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeviceList.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.DeviceList" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<%@ Register Src="~/UserControl/PopupControl/CreateDLP.ascx" TagName="CreateDLP"
    TagPrefix="ucCreateDLP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<style type="text/css">
    div.RadGrid .rgRow
    {
        background: #F7F7F7;
    }
    div.RadGrid .rgAltRow
    {
        background: #F7F7F7;
    }
    .RadGrid_Default, .RadGrid_Default .rgMasterTable, .RadGrid_Default .rgDetailTable, .RadGrid_Default .rgGroupPanel table, .RadGrid_Default .rgCommandRow table, .RadGrid_Default .rgEditForm table, .RadGrid_Default .rgPager table, .GridToolTip_Default
    {
        font: 22px "Arial";
        color: #003CA9;
        cursor: pointer;
        text-decoration: none;
    }
    .RadGrid_Default .rgRow td, .RadGrid_Default .rgAltRow td, .RadGrid_Default .rgEditRow td, .RadGrid_Default .rgFooter td
    {
        border-style: none;
    }
    
    .RadGrid_Default td.rgPagerCell
    {
        -moz-border-bottom-colors: none;
        -moz-border-image: none;
        -moz-border-left-colors: none;
        -moz-border-right-colors: none;
        -moz-border-top-colors: none;
        border-color: #000000;
        border-style: solid;
        border-width: 0;
    }
    
    .rgPager table
    {
        border-top: 0px;
        border-bottom: 1px solid #000000;
        border-spacing: 1pt;
        width: 100%;
    }
    .RadGrid_Default, .RadGrid_Default .rgMasterTable, .RadGrid_Default .rgDetailTable, .RadGrid_Default .rgGroupPanel table, .RadGrid_Default .rgCommandRow table, .RadGrid_Default .rgEditForm table, .RadGrid_Default .rgPager table, .GridToolTip_Default
    {
        color: #003CA9;
        cursor: pointer;
        font: 14px "Arial";
        text-decoration: none;
    }
    .rgHeader
    {
        display: none;
    }
    .RadGrid_Default .rgRow a, .RadGrid_Default .rgAltRow a, .RadGrid_Default .rgEditRow a, .RadGrid_Default tr.rgEditRow a, .RadGrid_Default tr.rgHoveredRow a, .RadGrid_Default tr.rgActiveRow a, .RadGrid_Default .rgFooter a, .RadGrid_Default .rgEditForm a
    {
        color: #003CA9;
    }
</style>
<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <div>
            <table>
                <tr>
                    <td style="width: 80%">
                        <h1>
                            Device List</h1>
                    </td>
                    <td style="width: 200px">
                        <asp:LinkButton ID="lbtnReport" runat="server"><h1>Report</h1></asp:LinkButton>
                    </td>
                    <td style="width: 50px">
                        <asp:LinkButton ID="LinkButton1" runat="server"><h1>Help</h1></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="clear">
    </div>
    <div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="formDevice">
                    <ucStatusbar:Statusbar ID="Statusbar1" runat="server" Visible="false" />
                    <table cellpadding="0" cellspacing="0" border="0" width="97%">
                        <tr id="tr2" runat="server">
                            <td>
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr runat="server" id="trCompany">
                                        <td class="DeviceListFirstTD">
                                            <asp:Label ID="Label1" runat="server" Text="Company"></asp:Label>
                                            &nbsp; :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbCompany" runat="server" DataValueField="Key" DataTextField="Value"
                                                OnSelectedIndexChanged="cmbCompany_SelectedIndexChanged" AutoPostBack="true"
                                                CssClass="select" Width="180px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trBranch">
                                        <td class="DeviceListFirstTD">
                                            <asp:Label ID="Label2" runat="server" Text="Branch"></asp:Label>
                                            &nbsp :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbBranch" runat="server" DataValueField="Key" DataTextField="Value"
                                                OnSelectedIndexChanged="cmbBranch_SelectedIndexChanged" AutoPostBack="true"
                                                CssClass="select" Width="180px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="listDevice">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <telerik:RadGrid ID="rgDeviceList" ShowHeader="true" AllowFilteringByColumn="true"
                                    AllowSorting="false" AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False"
                                    EnableLinqExpressions="false" OnNeedDataSource="rgDeviceList_NeedDataSource"
                                    OnItemCommand="rgDeviceList_ItemCommand" Width="670px">
                                    <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true">
                                    </ClientSettings>
                                    <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />
                                    <MasterTableView DataKeyNames="DeviceId,DeviceName,DeviceFamily.DeviceFamilyId,DeviceFamily.DeviceFamilyDesc">
                                        <Columns>
                                            <telerik:GridTemplateColumn DataField="DeviceName" AllowFiltering="true" HeaderText=""
                                                UniqueName="DeviceName" SortExpression="DeviceName" FilterControlWidth="200px"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <div class="product_list">
                                                        <ul>
                                                            <li>
                                                                <div class="left">
                                                                    <asp:LinkButton ID="lbtnSelectDevice" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"DeviceId") %>'
                                                                        CommandName="Select">
                                                                                    <span><%# Eval("DeviceName")%></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="clear">
    </div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
