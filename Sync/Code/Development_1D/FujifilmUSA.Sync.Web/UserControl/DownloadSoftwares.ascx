<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="DownloadSoftwares.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.DownloadSoftwares" %>

<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
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
    .breakWord200
     {
            max-width: 200px !important;
            word-break: break-all !important;
            word-wrap: break-word !important;
            vertical-align: top;
            line-height: 15px;
     }
    .breakWord300
     {
            max-width: 300px !important;
            word-break: break-all !important;
            word-wrap: break-word !important;
            vertical-align: top;
            line-height: 15px;
     }
     
    
</style>

<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle">
    <div class="head">
        <h1><asp:Localize runat="server" Text="Download" ID="locDownload" 
                meta:resourcekey="locDownloadResource1"></asp:Localize></h1>       
    </div>
    <div class="clear">
    </div>
    <center>
        <div style="margin-left: 10px;" >
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                    Visible="False" />
                <telerik:RadGrid ID="rgDownload" AllowSorting="True"
                    AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False" EnableLinqExpressions="False"
                    OnNeedDataSource="rgDownload_NeedDataSource" Width="75%" CellSpacing="0" 
                    GridLines="None">
                    <HeaderStyle Font-Bold="True" />
                    <PagerStyle Position="Top" Font-Size="Small" />
                    <MasterTableView DataKeyNames="DownloadId,DownloadName">
                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                            Visible="True">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                            Visible="True">
                        </ExpandCollapseColumn>
                        <Columns>                             
                            <telerik:GridTemplateColumn DataField="DownloadName" HeaderText="Software Name"
                                   UniqueName="DownloadName" SortExpression="DownloadName" 
                                FilterControlAltText="Filter DownloadName column" 
                                meta:resourcekey="GridTemplateColumnResource1">
                                        <ItemTemplate>
                                            <div class="Download_list">
                                                <ul>
                                                    <li>
                                                        <div class="left">
                                                            <a href='<%# Eval("DownloadPath") %>'>
                                                                <asp:Label ID="lblDownloadName" runat="server" 
                                                                Text='<%# Eval("DownloadName") %>' 
                                                                meta:resourcekey="lblDownloadNameResource1" />
                                                            </a>
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="breakWord200" />
                                </telerik:GridTemplateColumn>                                                  
                        </Columns>
                        
                    </MasterTableView>
             
                </telerik:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </center>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
