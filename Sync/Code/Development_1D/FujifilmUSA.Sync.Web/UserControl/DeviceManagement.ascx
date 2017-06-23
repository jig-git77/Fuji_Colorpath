<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="DeviceManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.DeviceManagement" %>
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
    
    
</style>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript" language="javascript">
        function ConfirmDeleteDevice() {
            if (confirm('<%= DeviceManagement_DeleteDevice_Confirmation %>'))
                return true;
            else
                return false;
        }
    </script>
</telerik:RadCodeBlock>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle">
    <div class="head">
        <h1><asp:Localize ID="locDeviceMgmt" runat="server" Text="Device Management" 
                meta:resourcekey="locDeviceMgmtResource1"></asp:Localize></h1>
        <asp:LinkButton ID="likAdd" runat="server" class="button" 
            OnClick="likAdd_Click" meta:resourcekey="likAddResource1">
            <span>
                <asp:Image ID="imgAdd" runat="server" 
            ImageUrl="../Images/new_project_icon.png" meta:resourcekey="imgAddResource1" />
                
<asp:Localize Text="Add Device" runat="server" ID="locAddDevice" 
            meta:resourcekey="locAddDeviceResource1"></asp:Localize>
</span>
        </asp:LinkButton>
        <asp:LinkButton ID="lbtnAddDeviceFamily" runat="server" class="button" 
            OnClick="lbtnAddDeviceFamily_Click" Visible="False" 
            meta:resourcekey="lbtnAddDeviceFamilyResource1">
            <span>
                <asp:Image ID="Image1" runat="server" 
            ImageUrl="../Images/new_project_icon.png" meta:resourcekey="Image1Resource1" />
                
<asp:Localize runat="server" Text="Add Device Family" ID="locAddDeviceFamily" 
            meta:resourcekey="locAddDeviceFamilyResource1"></asp:Localize>
</span>
        </asp:LinkButton>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
      <%--  <asp:UpdatePanel runat="server">
            <ContentTemplate>--%>
                <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
            Visible="False" />
                <asp:HiddenField ID="hdSelectedCompanyId" runat="server" Value="0" />
                <telerik:RadGrid ID="rgDevice" AllowFilteringByColumn="True" AllowSorting="True"
                    AllowPaging="True" PageSize="5" runat="server" 
            AutoGenerateColumns="False" EnableLinqExpressions="False"
                    OnNeedDataSource="rgDevice_NeedDataSource" OnItemCommand="rgDevice_ItemCommand"
                    Width="98%" HeaderStyle-Font-Bold="true" CellSpacing="0" 
            GridLines="None">
                    <GroupingSettings CaseSensitive="false" />

<HeaderStyle Font-Bold="True"></HeaderStyle>

                    <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />
                    <MasterTableView DataKeyNames="DeviceID,DeviceName,LicenseID,DeviceFamily.DeviceFamilyID,License.Branch.BranchId,License.Branch.Company.CompanyId,License.BranchId,IsActive">
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

<ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn DataField="DeviceName" AllowFiltering="true" HeaderText="Device Name"
                                UniqueName="DeviceName" SortExpression="DeviceName" 
                                FilterControlWidth="120" HeaderStyle-Width ="120" 
                                ShowFilterIcon="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                ItemStyle-CssClass="breakWord120" ItemStyle-Width="120" 
                                ItemStyle-Wrap="true" meta:resourcekey="GridTemplateColumnResource1">                              
                                <ItemTemplate>
                                    <asp:Label ID="lblDeviceName" runat="server" Text='<%# Eval("DeviceName") %>' 
                                        meta:resourcekey="lblDeviceNameResource1" />
                                </ItemTemplate>

<HeaderStyle Width="120px"></HeaderStyle>

<ItemStyle Wrap="True" CssClass="breakWord120" Width="120px"></ItemStyle>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="DeviceFamily.DeviceFamilyDesc" AllowFiltering="true"
                                HeaderText="Device Family Name" UniqueName="DeviceFamilyDesc" SortExpression="DeviceFamily.DeviceFamilyDesc"
                                FilterControlWidth="100" HeaderStyle-Width ="100"  
                                ItemStyle-CssClass="breakWord100" ItemStyle-Width="100" ItemStyle-Wrap="true"
                                ShowFilterIcon="true" AutoPostBackOnFilter="true" 
                                CurrentFilterFunction="Contains" meta:resourcekey="GridTemplateColumnResource2">
                                <ItemTemplate>
                                    <asp:Label ID="lblDeviceFamilyDesc" runat="server" 
                                        Text='<%# Eval("DeviceFamily.DeviceFamilyDesc") %>' 
                                        meta:resourcekey="lblDeviceFamilyDescResource1" />
                                </ItemTemplate>

<HeaderStyle Width="100px"></HeaderStyle>

<ItemStyle Wrap="True" CssClass="breakWord100" Width="100px"></ItemStyle>
                            </telerik:GridTemplateColumn>                                                      
                            <telerik:GridTemplateColumn DataField="License.Branch.Company.CompanyName" AllowFiltering="true"
                                HeaderText="Company Name" UniqueName="CompanyName" SortExpression="License.Branch.Company.CompanyName"
                                FilterControlWidth="120" HeaderStyle-Width ="120"  
                                ItemStyle-CssClass="breakWord120" ItemStyle-Width="120" ItemStyle-Wrap="true"
                                ShowFilterIcon="true" DataType="System.String" AutoPostBackOnFilter="true" 
                                CurrentFilterFunction="Contains" meta:resourcekey="GridTemplateColumnResource3">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompanyName" runat="server" 
                                        Text='<%# Eval("License.Branch.Company.CompanyName") %>' 
                                        meta:resourcekey="lblCompanyNameResource1" />
                                </ItemTemplate>

<HeaderStyle Width="120px"></HeaderStyle>

<ItemStyle Wrap="True" CssClass="breakWord120" Width="120px"></ItemStyle>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="License.Branch.BranchName" AllowFiltering="true"
                                HeaderText="Branch Name" UniqueName="BranchName" SortExpression="License.Branch.BranchName"
                                FilterControlWidth="100" HeaderStyle-Width ="100"  
                                ItemStyle-CssClass="breakWord100" ItemStyle-Width="100" ItemStyle-Wrap="true"
                                ShowFilterIcon="true" DataType="System.String" AutoPostBackOnFilter="true" 
                                CurrentFilterFunction="Contains" meta:resourcekey="GridTemplateColumnResource4">
                                <ItemTemplate>
                                    <asp:Label ID="lblBranchName" runat="server" 
                                        Text='<%# Eval("License.Branch.BranchName") %>' 
                                        meta:resourcekey="lblBranchNameResource1" />
                                </ItemTemplate>

<HeaderStyle Width="100px"></HeaderStyle>

<ItemStyle Wrap="True" CssClass="breakWord100" Width="100px"></ItemStyle>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="IsActive" AllowFiltering="false" HeaderText="IsActive?"
                                DataField="IsActive" ItemStyle-HorizontalAlign="Center"  
                                FilterControlWidth="40" HeaderStyle-Width ="40" 
                                meta:resourcekey="GridTemplateColumnResource5" >
                                <ItemTemplate>
                                    <asp:Image ID="imgIsActive" runat="server" 
                                        ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) ? "~/Images/checkbox_checked.png" : "~/Images/checkbox_unchecked.png" %>' 
                                        meta:resourcekey="imgIsActiveResource1" />
                                </ItemTemplate>

<HeaderStyle Width="40px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="DeviceID" AllowFiltering="true" HeaderText=""
                                UniqueName="Edit"  FilterControlWidth="70" HeaderStyle-Width ="70" 
                                meta:resourcekey="GridTemplateColumnResource6">
                                <FilterTemplate>
                                    <center>
                                    <asp:LinkButton ID="lbtnClearFillter" runat="server"  ToolTip ="Clear Fillter" 
                                            CommandName="ClearFilter">
                                                    <span>
                                                        <asp:Image ID="imgClearFillter" runat="server" 
                                            AlternateText="Clear Fillter" ImageUrl="../Images/clear-fillter.png" 
                                            meta:resourcekey="imgClearFillterResource1" />

                                                    </span>
                                    </asp:LinkButton>
                                    </center>
                                </FilterTemplate>
                                <ItemTemplate>
                                    <div>
                                        <center>
                                            <div style="width: 45px;">
                                                <div style="float: left; padding-right: 10px;">
                                                    <asp:ImageButton ID="imgbtnEdit" runat="server" ToolTip="Edit Device" AlternateText="Edit"
                                                        ImageUrl="../Images/edit_icon.png" CommandArgument='<%# Eval("DeviceID") %>'
                                                        CommandName="Edit" meta:resourcekey="imgbtnEditResource1" />
                                                </div>
                                                <div style="float: left;">
                                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete Device" AlternateText="Delete"
                                                        ImageUrl="../Images/delete_icon.png" OnClientClick="javascript:return ConfirmDeleteDevice();"
                                                        CommandArgument='<%# Eval("DeviceID") %>' CommandName="Delete" 
                                                        meta:resourcekey="imgbtnDeleteResource1" />
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                        </center>
                                    </div>
                                </ItemTemplate>

<HeaderStyle Width="70px"></HeaderStyle>
                            </telerik:GridTemplateColumn>
                        </Columns>

                    </MasterTableView>

                </telerik:RadGrid>
         <%--   </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
