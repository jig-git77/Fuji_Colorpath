<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Activity_PrintCondition.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Activity_PrintCondition" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
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


<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle">
    <div class="head">
        <h1>
            Activity Management</h1>        
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" Visible="false" />

        <telerik:RadGrid ID="RadGrid1" runat="server" Width="98%" ShowStatusBar="true"
            AutoGenerateColumns="False" PageSize="7" AllowSorting="True" AllowMultiRowSelection="False"
            AllowPaging="True" OnDetailTableDataBind="RadGrid1_DetailTableDataBind" 
            OnNeedDataSource="RadGrid1_NeedDataSource" OnPreRender="RadGrid1_PreRender">
            <PagerStyle Mode="NumericPages"></PagerStyle>
            <MasterTableView Width="100%" DataKeyNames="CompanyID" AllowMultiColumnSorting="True">
                <DetailTables>
                    <telerik:GridTableView DataKeyNames="BranchID" Name="BranchDetails" Width="100%">
                        <DetailTables>
                            <telerik:GridTableView DataKeyNames="BranchID" Name="UserDetails" Width="100%">
                                <Columns>
                                    <telerik:GridBoundColumn SortExpression="UserName" HeaderText="UserName" HeaderButtonType="TextButton"
                                        DataField="UserName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="PrintCondCreate" HeaderText="PrintCondCreate" HeaderButtonType="TextButton"
                                        DataField="PrintCondCreate">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="PrintCondDelete" HeaderText="PrintCondDelete" HeaderButtonType="TextButton"
                                        DataField="PrintCondDelete">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="UploadMeasure" HeaderText="UploadMeasure" HeaderButtonType="TextButton"
                                        DataField="UploadMeasure">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="CreateDLP" HeaderText="CreateDLP" HeaderButtonType="TextButton"
                                        DataField="CreateDLP">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="OptDLP" HeaderText="OptDLP" HeaderButtonType="TextButton"
                                        DataField="OptDLP">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </telerik:GridTableView>
                        </DetailTables>
                        <Columns>
                            <telerik:GridBoundColumn SortExpression="BranchID" HeaderText="BranchID" HeaderButtonType="TextButton"
                                DataField="BranchID">
                            </telerik:GridBoundColumn>                           
                            <telerik:GridBoundColumn SortExpression="BranchName" HeaderText="BranchName" HeaderButtonType="TextButton"
                                DataField="BranchName" UniqueName="BranchName">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </telerik:GridTableView>
                </DetailTables>
                <Columns>
                    <telerik:GridBoundColumn SortExpression="CompanyID" HeaderText="CompanyID" HeaderButtonType="TextButton"
                        DataField="CompanyID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn SortExpression="CompanyName" HeaderText="CompanyName" HeaderButtonType="TextButton"
                        DataField="CompanyName">
                    </telerik:GridBoundColumn>                   
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>

  </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
