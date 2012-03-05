<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeviceList.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.Mock.UserControl.DeviceList" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
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
    <div class="formDevice">
        <ucStatusbar:Statusbar ID="Statusbar1" runat="server" Visible="false" />
        <table cellpadding="0" cellspacing="0" border="0" width="97%">
         <%--   <tr id="tr1" runat="server">
                <td>
                    <label>
                        Show Presses For:</label>
                </td>
            </tr>--%>
            <tr id="tr2" runat="server">
                <td>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td class="DeviceListFirstTD">
                                <asp:Label ID="Label1" runat="server" Text="Company"></asp:Label>
                                &nbsp; :
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbCompany" runat="server" DataValueField="Key" DataTextField="Value"
                                    OnSelectedIndexChanged="cmbCompany_SelectedIndexChanged1" AutoPostBack="true"
                                    CssClass="select" Width="180px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="DeviceListFirstTD">
                                <asp:Label ID="Label2" runat="server" Text="Branch"></asp:Label>
                                &nbsp :
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbBranch" runat="server" DataValueField="Key" DataTextField="Value"
                                    OnSelectedIndexChanged="cmbBranch_SelectedIndexChanged1" AutoPostBack="true"
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
                    <label>
                        Select Presses</label>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                        <AjaxSettings>
                            <telerik:AjaxSetting AjaxControlID="cmbCompany">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                                </UpdatedControls>
                            </telerik:AjaxSetting>
                            <telerik:AjaxSetting AjaxControlID="cmbBranch">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                                </UpdatedControls>
                            </telerik:AjaxSetting>
                            <telerik:AjaxSetting AjaxControlID="rgDeviceList">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="rgDeviceList" LoadingPanelID="RadAjaxLoadingPanel1" />
                                </UpdatedControls>
                            </telerik:AjaxSetting>
                        </AjaxSettings>
                    </telerik:RadAjaxManager>
                    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Office2010Silver" />
                    <telerik:RadGrid ID="rgDeviceList" ShowHeader="true" AllowFilteringByColumn="true"
                        AllowSorting="false" AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False"
                        EnableLinqExpressions="false" OnNeedDataSource="rgDeviceList_NeedDataSource"
                        OnItemCommand="rgDeviceList_ItemCommand" Width="670px">
                        <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />
                        <MasterTableView DataKeyNames="DeviceName">
                            <Columns>
                                <telerik:GridTemplateColumn DataField="DeviceName" AllowFiltering="true" HeaderText=""
                                    UniqueName="DeviceName" SortExpression="DeviceName" FilterControlWidth="200px">
                                    <ItemTemplate>
                                        <div class="product_list">
                                            <ul>
                                                <li>
                                                    <div class="left">
                                                        <asp:LinkButton ID="lbtnSelectDevice" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"DeviceName") %>'
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
    <div class="clear">
    </div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
<%--<table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 100px;">
                                                            <label style="font-size: 18px;">
                                                                Search</label>
                                                        </td>
                                                        <td style="width: 100px;">
                                                            <asp:TextBox ID="txtDeviceName" MaxLength="50" Font-Size="18px" runat="server" Width="300px"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 100px;">
                                                            <asp:LinkButton ID="lbtnSearchDevice" runat="server" ToolTip="Search Device" OnClick="lbtnSearchDevice_Click">
                                                                <span>
                                                                    <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/Search.png" /></span>
                                                            </asp:LinkButton>
                                                        </td>
                                                        <td style="width: 100%;">
                                                        </td>
                                                    </tr>
                                                </table>--%>
<%--<FilterTemplate>
                                                            <label>Search</label>
                                                            <asp:TextBox ID="txtDeviceName" MaxLength="50" Font-Size="18px" runat="server" Width="300px"></asp:TextBox>
                                                            <asp:LinkButton ID="lbtnSearchDevice" runat="server" ToolTip="Search Device">
                                                            <span>
                                                                  <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/Search.png" /></span> 
                                                            </asp:LinkButton>


                                                            <telerik:RadTextBox ID="RadTextBox1" runat="server" MaxLength="50" ></telerik:RadTextBox>
                                                            <telerik:RadButton ID="RadButton1" runat="server" OnClientClicked="CountryIndexChanged" ButtonType="LinkButton" BorderWidth="0">
                                                                <ContentTemplate>
                                                                    <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/Search.png" />    
                                                                </ContentTemplate>
                                                            </telerik:RadButton> 
                                                             <telerik:RadScriptBlock ID="RadScriptBlock3" runat="server">
                                                                    <script type="text/javascript">
                                                                        function CountryIndexChanged(sender, args) {
                                                                            debugger;
                                                                            var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                                            
                                                                            tableView.filter("DeviceName", args.get_item().get_value(), "EqualTo");
                                                                        }
                                                                    </script>
                                                                </telerik:RadScriptBlock>

                                                           <telerik:RadComboBox ID="RadComboBoxCountry" DataSourceID="SqlDataSource4" DataTextField="Country"
                                                                    DataValueField="Country" Height="100px" AppendDataBoundItems="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("Country").CurrentFilterValue %>'
                                                                    runat="server" OnClientSelectedIndexChanged="CountryIndexChanged">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="All" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                                <telerik:RadScriptBlock ID="RadScriptBlock3" runat="server">
                                                                    <script type="text/javascript">
                                                                        function CountryIndexChanged(sender, args) {
                                                                            var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                                            tableView.filter("Country", args.get_item().get_value(), "EqualTo");
                                                                        }
                                                                    </script>
                                                                </telerik:RadScriptBlock>
                                                           
                                                            </FilterTemplate>--%>