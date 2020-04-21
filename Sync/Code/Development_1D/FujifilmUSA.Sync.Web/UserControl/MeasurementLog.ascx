<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MeasurementLog.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.MeasurementLog" %>
<style type="text/css">
    div.RadGrid .rgRow
    {
        background: #F7F7F7;
    }
    div.RadGrid .rgAltRow
    {
        background: #F7F7F7;
    }
    .rgPager table
    {
        border-top: 0px;
        border-bottom: 1px solid #000000;
        border-spacing: 1pt;
        width: 100%;
    }
    .RadGrid_Default .rgRow a, .RadGrid_Default .rgAltRow a, .RadGrid_Default .rgEditRow a, .RadGrid_Default tr.rgEditRow a, .RadGrid_Default tr.rgHoveredRow a, .RadGrid_Default tr.rgActiveRow a, .RadGrid_Default .rgFooter a, .RadGrid_Default .rgEditForm a
    {
        color: #003CA9;
    }
    .RadGrid_Default td.rgPagerCell
    {
        -moz-border-bottom-colors: none;
        -moz-border-image: none;
        -moz-border-left-colors: none;
        -moz-border-right-colors: none;
        -moz-border-top-colors: none;
        border-color: #828282 #EEEEEE #EEEEEE;
        border-style: solid;
        border-width: 0px 0 0px 0px;
    }
    .RadGrid_Default .rgRow td, .RadGrid_Default .rgAltRow td, .RadGrid_Default .rgEditRow td, .RadGrid_Default .rgFooter td
    {
        border-style: none;
        border-width: 0 0 1px;
    }
    
        .RadGrid_Default .rgRow .rgSorted {
        background-color: #F7F7F7;
        border-bottom-color: #F7F7F7;
    }
    .RadGrid_Default .rgAltRow .rgSorted {
            background-color: #F7F7F7;
            border-bottom-color: #F7F7F7;
    }

    .RadGrid_Default th.rgSorted 
    {
            background-color: #F1F1F1;
            background-position: 0px;
    }
    
     .breakWord120
     {
            max-width: 120px !important;
            word-break: break-all !important;
            word-wrap: break-word !important;
            vertical-align: top;
            line-height: 15px;
     }
      .breakWord100
     {
            max-width: 100px !important;
            word-break: break-all !important;
            word-wrap: break-word !important;
            vertical-align: top;
            line-height: 15px;
     }
      .breakWord70
     {
            max-width: 70px !important;
            word-break: break-all !important;
            word-wrap: break-word !important;
            vertical-align: top;
            line-height: 15px;
     }
    
</style>

<div class="admin_top">
    &nbsp;
</div>
<div class="admin_middle">
    <div class="head">
        <h1><asp:Localize runat="server" Text="Measurement Log" ID="locMDM"
                meta:resourcekey="locMDMResource1"></asp:Localize></h1>       
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <asp:UpdatePanel ID="testPanel" runat="server">

            <ContentTemplate>                
                <telerik:RadGrid ID="rgMeasurementLog" AllowFilteringByColumn="True"
                    AllowSorting="True" AllowPaging="True" PageSize="20" runat="server" AutoGenerateColumns="False"
                    EnableLinqExpressions="False" OnNeedDataSource="rgMeasurementLog_NeedDataSource"
                    OnItemCommand="rgMeasurementLog_ItemCommand" Width="98%"
                    HeaderStyle-Font-Bold="true" CellSpacing="0" GridLines="None">
                    <GroupingSettings CaseSensitive="false" />

                    <HeaderStyle Font-Bold="True"></HeaderStyle>

                    <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />
                    <MasterTableView DataKeyNames="MeasurementDeviceId">
                        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

                        <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

                        <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn
                                UniqueName="EntryDate" AllowFiltering="true" HeaderText="EntryDate"
                                DataField="EntryDate" SortExpression="EntryDate"
                                ShowFilterIcon="true" FilterControlWidth="100" HeaderStyle-Width="100"
                                ItemStyle-CssClass="breakWord100" ItemStyle-Width="100"
                                ItemStyle-Wrap="true" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" >
                                <ItemTemplate>
                                    <asp:Label ID="lblEntryDate" runat="server" Text='<%# Eval("EntryDate") %>'
                                        meta:resourcekey="lblEntryDateResource1" />
                                </ItemTemplate>

                                <HeaderStyle Width="100px" CssClass="breakWord100"></HeaderStyle>

                                <ItemStyle Wrap="True" CssClass="breakWord100" Width="100px"></ItemStyle>
                            </telerik:GridTemplateColumn> 
                            <telerik:GridTemplateColumn
                                UniqueName="UserName" AllowFiltering="true" HeaderText="User Name"
                                DataField="UserName" SortExpression="UserName"
                                ShowFilterIcon="true" FilterControlWidth="120" HeaderStyle-Width="120"
                                ItemStyle-CssClass="breakWord120" ItemStyle-Width="120"
                                ItemStyle-Wrap="true" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>'
                                        meta:resourcekey="lblUserNameResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="120px"></HeaderStyle>
                                <ItemStyle Wrap="True" CssClass="breakWord120" Width="120px"></ItemStyle>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn
                                UniqueName="SerialNumber" AllowFiltering="true" HeaderText="Serial Number"
                                DataField="SerialNumber" SortExpression="SerialNumber"
                                ShowFilterIcon="true" FilterControlWidth="120" HeaderStyle-Width="120"
                                ItemStyle-CssClass="breakWord120" ItemStyle-Width="120"
                                ItemStyle-Wrap="true" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains">
                                <ItemTemplate>
                                    <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Eval("SerialNumber") %>'
                                        meta:resourcekey="lblSerialNumberResource1" />
                                </ItemTemplate>

                                <HeaderStyle Width="120px"></HeaderStyle>

                                <ItemStyle Wrap="True" CssClass="breakWord120" Width="120px"></ItemStyle>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn
                                UniqueName="InstrumentType" AllowFiltering="true" HeaderText="Instrument Type"
                                DataField="InstrumentType" SortExpression="InstrumentType"
                                ShowFilterIcon="true" FilterControlWidth="90" HeaderStyle-Width="90"
                                ItemStyle-CssClass="breakWord70" ItemStyle-Width="90"
                                ItemStyle-Wrap="true" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains">
                                <ItemTemplate>
                                    <asp:Label ID="lblInstrumentType" runat="server"
                                        Text='<%# Eval("InstrumentType") %>' meta:resourcekey="lblInstrumentTypeResource1" />
                                </ItemTemplate>

                                <HeaderStyle Width="90px"></HeaderStyle>

                                <ItemStyle Wrap="True" CssClass="breakWord70" Width="90px"></ItemStyle>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn
                                UniqueName="ErrorMessage" AllowFiltering="true"
                                HeaderText="ErrorMessage" DataField="ErrorMessage" SortExpression="ErrorMessage"
                                ShowFilterIcon="true" FilterControlWidth="100" HeaderStyle-Width="100"
                                ItemStyle-CssClass="breakWord100" ItemStyle-Width="100"
                                ItemStyle-Wrap="true" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains">
                                <ItemTemplate>
                                    <asp:Label ID="lblInstrumentType" runat="server"
                                        Text='<%# Eval("ErrorMessage") %>'
                                        meta:resourcekey="lblErrorMessageResource1" />
                                </ItemTemplate>

                                <HeaderStyle Width="100px"></HeaderStyle>

                                <ItemStyle Wrap="True" CssClass="breakWord100" Width="100px"></ItemStyle>
                            </telerik:GridTemplateColumn>
                             <telerik:GridTemplateColumn
                                UniqueName="ErrorDescription" AllowFiltering="false"
                                HeaderText="ErrorDescription" DataField="ErrorDescription" SortExpression="ErrorDescription"
                                ShowFilterIcon="true" FilterControlWidth="100" HeaderStyle-Width="100"
                                ItemStyle-CssClass="breakWord100" ItemStyle-Width="100"
                                ItemStyle-Wrap="true" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains">
                                <ItemTemplate>
                                    <asp:Label ID="lblErrorDescription" runat="server"
                                        title='<%# Eval("ErrorDescription") %>' Text='<%# Convert.ToString(Eval("ErrorDescriptionShort")) %>'
                                        meta:resourcekey="lblErrorMessageResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="100px"></HeaderStyle>
                                <ItemStyle Wrap="True" CssClass="breakWord100" Width="100px"></ItemStyle>
                            </telerik:GridTemplateColumn>

                                                       
                            
                            <telerik:GridTemplateColumn
                                DataField="BranchID" AllowFiltering="true" HeaderText=""
                                UniqueName="Edit" meta:resourcekey="GridTemplateColumnResource7">
                                <FilterTemplate>
                                    <center>
                                    <asp:LinkButton ID="lbtnClearFillter" runat="server" ToolTip="Clear Fillter" 
                                            CommandName="ClearFilter"> <span><asp:Image ID="imgClearFillter" runat="server" 
                                            AlternateText="Clear Fillter" ImageUrl="../Images/clear-fillter.png" 
                                            meta:resourcekey="imgClearFillterResource1" /></span></asp:LinkButton>
                                    </center>
                                </FilterTemplate>                                
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;
</div>
<div class="clear">
</div>
