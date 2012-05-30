<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeviceList.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.DeviceList" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<%@ Register Src="~/UserControl/PopupControl/CreateDLP.ascx" TagName="CreateDLP"
    TagPrefix="ucCreateDLP" %>
<%@ Register Src="~/UserControl/OperationSelection.ascx" TagName="OperationSelection"
    TagPrefix="ucOperationSelection" %>
<script type="text/javascript" language="javascript">
    jQuery(document).ready(function () {
        SetToolTip();
    });
    function pageLoad() {
        SetToolTip();
    }
    function SetToolTip() {
        $("[id*='lbtnSelectDevice']").each(function () {

            var offset = $(this).offset();
            var top = offset.top - 270;
            var left = ($(this).text().length * 5) + 140;

            $("[TDeviceId*=" + $(this).attr("DeviceId") + "]").offset({ top: top, left: left });

            $(this).mouseover(function () {
                $("[TDeviceId*=" + $(this).attr("DeviceId") + "]").show();
            }).mouseout(function () { $("[TDeviceId*=" + $(this).attr("DeviceId") + "]").hide(); });

        });
        alert('Load');
     }
    


    //------------------------------------------
    function InToolTip(divOperationSelection) {
        $(divOperationSelection).show();
    };

    function outToolTip(divOperationSelection) {
        $(divOperationSelection).hide();  
    };
</script>
<style type="text/css">
    .tooltipDiv
    {
        position: absolute;
      
    }
    
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
        <div class="formDevice">
            <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="false" />
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
                                        OnSelectedIndexChanged="cmbBranch_SelectedIndexChanged" AutoPostBack="true" CssClass="select"
                                        Width="180px">
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
                    <td class="Top_Grn_Header">
                        <h3>
                            Select Press
                        </h3>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="rgDeviceList" ShowHeader="true" AllowFilteringByColumn="true"
                            AllowSorting="true" AllowPaging="True" runat="server" AutoGenerateColumns="False"
                            EnableLinqExpressions="false" OnNeedDataSource="rgDeviceList_NeedDataSource"
                            OnItemCommand="rgDeviceList_ItemCommand" Width="675px" OnItemDataBound="rgDeviceList_ItemDataBound">
                            <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true">
                            </ClientSettings>
                            <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />
                            <HeaderStyle Font-Size="12px" Font-Bold="true" ForeColor="Black" />
                            <MasterTableView DataKeyNames="DeviceId,DeviceName,DeviceFamily.DeviceFamilyId,DeviceFamily.DeviceFamilyDesc,BranchName">
                                <Columns>
                                    <telerik:GridTemplateColumn DataField="DeviceName" AllowFiltering="true" HeaderText="Device Name"
                                        UniqueName="DeviceName" SortExpression="DeviceName" FilterControlWidth="200px"
                                        ShowFilterIcon="true" ItemStyle-CssClass="breakWord200">
                                        <ItemTemplate>
                                            <asp:panel id="divSelectDevice" runat="server" class="product_list" DeviceId='<%# Eval("DeviceId")%>'  >
                                                <ul>
                                                    <li>
                                                        <div class="left">
                                                            <asp:LinkButton ID="lbtnSelectDevice" runat="server" DeviceId='<%# Eval("DeviceId")%>' Text='<%# Eval("DeviceName")%>'
                                                                 OnClientClick="return false;">
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                    </li>
                                                </ul>
                                            </asp:panel>
                                            <div id="divOperationSelection" TDeviceId='<%# Eval("DeviceId")%>' class="tooltipDiv"
                                                style="display: none;" onmouseout="outToolTip(this)" onmouseover="InToolTip(this)" >
                                                <ucOperationSelection:OperationSelection ID="operationSelection" runat="server" DeviceId='<%# Eval("DeviceId")%>' />
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
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rgDeviceList">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgDeviceList" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <%--<telerik:AjaxUpdatedControl ControlID="RadToolTipManager1" />--%>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="cmbCompany" EventName="OnSelectedIndexChanged">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgDeviceList" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <%--<telerik:AjaxUpdatedControl ControlID="RadToolTipManager1" />--%>
                        <telerik:AjaxUpdatedControl ControlID="cmbBranch" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="cmbBranch" EventName="OnSelectedIndexChanged">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgDeviceList" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <%--<telerik:AjaxUpdatedControl ControlID="RadToolTipManager1" />--%>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
        </telerik:RadAjaxLoadingPanel>
        <%--  <telerik:RadToolTipManager ID="RadToolTipManager1" OffsetY="-10" HideEvent="LeaveToolTip"
                                        runat="server" OnAjaxUpdate="OnAjaxUpdate" RelativeTo="Element"
                                        Position="MiddleRight"  BackColor="#C0C0C0" Animation="Fade">
                               </telerik:RadToolTipManager>--%>
    </div>
    <div class="clear">
    </div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
