<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeviceList.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.DeviceList" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<%@ Register Src="~/UserControl/PopupControl/CreateDLP.ascx" TagName="CreateDLP"
    TagPrefix="ucCreateDLP" %>
<%@ Register Src="~/UserControl/OperationSelection.ascx" TagName="OperationSelection"
    TagPrefix="ucOperationSelection" %>
<script src="JS/jquery.tools.min.js"></script>
<script type="text/javascript" language="javascript">
    jQuery(document).ready(function () {
        $("[id*='lbtnSelectDevice']").each(function () {
            $(this).tooltip({
                tip: $("[TDeviceId*=" + $(this).attr("DeviceId") + "]"),
                position: 'center center',
                offset: [-43,30],
                effect: 'fade',
                fadeOutSpeed: 200,
                predelay: 500,
            });
        });
    });
</script>
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
    
    .RadGrid_Default .rgRow a, .RadGrid_Default .rgAltRow a, .RadGrid_Default .rgEditRow a, .RadGrid_Default tr.rgEditRow a, .RadGrid_Default tr.rgHoveredRow a, .RadGrid_Default tr.rgActiveRow a, .RadGrid_Default .rgFooter a, .RadGrid_Default .rgEditForm a
    {
        color: #003CA9;
    }
    
    .RadGrid .rgPagerLabel, .RadGrid .rgPager .RadComboBox, .RadGrid .rgPager .RadInput
    {
        color: black;
        margin: 0 4px 0 0;
        vertical-align: top;
    }
    .RadGrid_Default .rgRow .rgSorted
    {
        background-color: #F7F7F7;
        border-bottom-color: #F7F7F7;
    }
    .RadGrid_Default .rgAltRow .rgSorted
    {
        background-color: #F7F7F7;
        border-bottom-color: #F7F7F7;
    }
    
    .RadGrid_Default th.rgSorted
    {
        background-color: #F1F1F1;
        background-position: 0px;
    }
    .breakWord200
    {
        max-width: 200px !important;
        word-break: break-all !important;
        word-wrap: break-word !important;
        vertical-align: top;
        line-height: 15px;
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
                        <h1><asp:Localize ID="locDeviceList" runat="server" Text="Device List" 
                                meta:resourcekey="locDeviceListResource1"></asp:Localize></h1>
                    </td>
                    <td style="width: 200px">
                        <asp:LinkButton ID="lbtnReport" runat="server"><h1><asp:Localize ID="locReport" 
                                runat="server" Text="Report" meta:resourcekey="locReportResource1"></asp:Localize>
</h1></asp:LinkButton>
                    </td>
                    <td style="width: 50px">
                        <asp:LinkButton ID="LinkButton1" runat="server"><h1><asp:Localize ID="locHelp" 
                                runat="server" Text="Help" meta:resourcekey="locHelpResource1"></asp:Localize>
</h1></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="clear">
    </div>
    <div>
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <div class="formDevice">
                    <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
                    <table cellpadding="0" cellspacing="0" border="0" width="97%">
                        <tr id="tr2" runat="server">
                            <td runat="server">
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr runat="server" id="trCompany">
                                        <td class="DeviceListFirstTD" runat="server">
                                            <asp:Label ID="Label1" runat="server" Text="Company" 
                                                meta:resourcekey="Label1Resource1"></asp:Label>
                                            &nbsp; :
                                        </td>
                                        <td runat="server">
                                            <asp:DropDownList ID="cmbCompany" runat="server" DataValueField="Key" DataTextField="Value"
                                                OnSelectedIndexChanged="cmbCompany_SelectedIndexChanged" AutoPostBack="True"
                                                CssClass="select" Width="180px" meta:resourcekey="cmbCompanyResource1">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trBranch">
                                        <td class="DeviceListFirstTD" runat="server">
                                            <asp:Label ID="Label2" runat="server" Text="Branch" 
                                                meta:resourcekey="Label2Resource1"></asp:Label>
                                            &nbsp :
                                        </td>
                                        <td runat="server">
                                            <asp:DropDownList ID="cmbBranch" runat="server" DataValueField="Key" DataTextField="Value"
                                                OnSelectedIndexChanged="cmbBranch_SelectedIndexChanged" 
                                                AutoPostBack="True" CssClass="select"
                                                Width="180px" meta:resourcekey="cmbBranchResource1">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="listDevice">
                    <table id="tblDeviceList" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td class="Top_Grn_Header">
                                <h3>
                                    <asp:Localize ID="locSelectPress" runat="server" Text="Select Press" 
                                        meta:resourcekey="locSelectPressResource1"></asp:Localize>
                                </h3>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="rgDeviceList" AllowFilteringByColumn="True"
                                    AllowSorting="True" AllowPaging="True" runat="server" AutoGenerateColumns="False"
                                    EnableLinqExpressions="False" OnNeedDataSource="rgDeviceList_NeedDataSource"
                                    OnItemCommand="rgDeviceList_ItemCommand" Width="675px" CellSpacing="0" 
                                    GridLines="None" >
                                    <GroupingSettings CaseSensitive="False" />
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                    <PagerStyle Position="Top" AlwaysVisible="True" Font-Size="Small" />
                                    <HeaderStyle Font-Size="12px" Font-Bold="True" ForeColor="Black" />
                                    <MasterTableView DataKeyNames="DeviceId,DeviceName,DeviceFamily.DeviceFamilyId,DeviceFamily.DeviceFamilyDesc,BranchName">
                                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                                            Visible="True">
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                                            Visible="True">
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridTemplateColumn DataField="DeviceName" HeaderText="Device Name"
                                                UniqueName="DeviceName" SortExpression="DeviceName" 
                                                FilterControlWidth="200px" AutoPostBackOnFilter="True" 
                                                CurrentFilterFunction="Contains" 
                                                FilterControlAltText="Filter DeviceName column" 
                                                meta:resourcekey="GridTemplateColumnResource1">
                                                <ItemTemplate>
                                                    <asp:Panel ID="divSelectDevice" runat="server" class="product_list" 
                                                        meta:resourcekey="divSelectDeviceResource1">
                                                        <ul>
                                                            <li>
                                                                <div class="left">
                                                                            <asp:LinkButton ID="lbtnSelectDevice" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"DeviceId") %>'
                                                                                CommandName="Select" Text='<%# Eval("DeviceName") %>' 
                                                                                meta:resourcekey="lbtnSelectDeviceResource1"></asp:LinkButton>
                                                                        </div>
                                                                <div class="clear">
                                                                </div>
                                                            </li>
                                                        </ul>
                                                    </asp:Panel>
                                                    <div class="clear">
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="breakWord200" />
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <EditFormSettings>
                                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                            </EditColumn>
                                        </EditFormSettings>
                                        <PagerStyle AlwaysVisible="True" />
                                    </MasterTableView>
                                    <FilterMenu EnableImageSprites="False">
                                    </FilterMenu>
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
