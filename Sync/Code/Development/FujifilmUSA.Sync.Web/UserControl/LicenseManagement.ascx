<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LicenseManagement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.LicenseManagement" %>
<%@ Register TagName="Statusbar" TagPrefix="ucStatusbar" Src="Statusbar.ascx" %>
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
</style>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server" >
        <script type="text/javascript" language="javascript">
         function ConfirmDeleteLicense() {
            if (confirm('<%= LicenseManagement_DeleteLicense_Confirmation %>'))
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
        <h1>
            License Management</h1>
        <asp:LinkButton ID="likAdd" runat="server" class="button" OnClick="likAdd_Click">
            <span>
                <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/new_project_icon.png" />
                Add License</span>
        </asp:LinkButton>
        <asp:LinkButton ID="lbtnAddLicenseType" runat="server" class="button" 
            onclick="lbtnAddLicenseType_Click" >
            <span>
                <asp:Image ID="Image1" runat="server" ImageUrl="../Images/new_project_icon.png" />
                Add License Type</span>
        </asp:LinkButton>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
     <asp:UpdatePanel runat="server">
            <ContentTemplate>
            <ucStatusbar:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
            <asp:HiddenField ID="hdSelectedCompanyId" runat="server" Value="0" />
        <telerik:RadGrid ID="rgLicense" ShowHeader="true" AllowFilteringByColumn="true" AllowSorting="false"
            AllowPaging="True" PageSize="5" runat="server" AutoGenerateColumns="False" EnableLinqExpressions="false"
            OnNeedDataSource="rgLicense_NeedDataSource" OnItemCommand="rgLicense_ItemCommand"
             Width="98%" HeaderStyle-Font-Bold="true" >
            <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />
            <MasterTableView DataKeyNames="LicenseID,Branch.CompanyId,Branch.BranchId,LicenseTypeId,LicenseDesc,ActivationDate,ExpirationDate,IsActive">
                <Columns>

                    <telerik:GridTemplateColumn 
                        DataField="LicenseDesc" 
                        AllowFiltering="true" 
                        HeaderText="License Desc"
                        UniqueName="LicenseDesc" 
                        SortExpression="LicenseDesc" 
                        FilterControlWidth="120"
                        ItemStyle-Wrap="true" 
                        CurrentFilterFunction="Contains" 
                        ShowFilterIcon="false"
                        AutoPostBackOnFilter="true"
                        >
                        <ItemTemplate>
                            <asp:Label ID="lblLicenseDesc" runat="server" Text='<%# Eval("LicenseDesc") %>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn 
                        DataField="LicenseType.Type" 
                        AllowFiltering="true" 
                        HeaderText="License Type"
                        UniqueName="LicenseType" 
                        SortExpression="LicenseType.Type" 
                        FilterControlWidth="100"
                        ItemStyle-Wrap="true" 
                        CurrentFilterFunction="Contains" 
                        ShowFilterIcon="false"
                        AutoPostBackOnFilter="true">
                        <ItemTemplate>
                            <asp:Label ID="lblLicenseType" runat="server" Text='<%# Eval("LicenseType.Type") %>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridDateTimeColumn 
	                        DataField="ActivationDate" 
	                        HeaderText="Activation Date" 
	                        SortExpression="ActivationDate" 
	                        UniqueName="ActivationDate" 
	                        CurrentFilterFunction="StartsWith" 
	                        AutoPostBackOnFilter="true" 
	                        Display="true" 
	                        DataFormatString="{0:yyyy-MM-dd}"
                            ShowFilterIcon="false"
                            FilterControlWidth="100" >
	                        <ItemStyle HorizontalAlign="Left" Width="100" />
                        </telerik:GridDateTimeColumn>
                    <telerik:GridDateTimeColumn 
	                        DataField="ExpirationDate" 
	                        HeaderText="Expiration Date" 
	                        SortExpression="ExpirationDate" 
	                        UniqueName="ExpirationDate" 
	                        CurrentFilterFunction="StartsWith" 
	                        AutoPostBackOnFilter="true" 
	                        Display="true" 
	                        DataFormatString="{0:yyyy-MM-dd}"
                            ShowFilterIcon="false"
                            FilterControlWidth="100">
	                        <ItemStyle HorizontalAlign="Left"  Width="100" />
                        </telerik:GridDateTimeColumn>
                    <telerik:GridNumericColumn 
                        DataField="NumOfDevice" 
                        AllowFiltering="true" 
                        HeaderText="Total Devices"
                        UniqueName="NumOfDevice" 
                        SortExpression="NumOfDevice" 
                        FilterControlWidth="50"
                        ItemStyle-Wrap="true" 
                        CurrentFilterFunction="Contains" 
                        ShowFilterIcon="false"
                        AutoPostBackOnFilter="true"
                        >
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right"  Width="50" />
                    </telerik:GridNumericColumn>
                    <telerik:GridTemplateColumn 
                        DataField="Branch.BranchName" 
                        AllowFiltering="true" 
                        HeaderText="Branch Name"
                        UniqueName="BranchName" 
                        SortExpression="Branch.BranchName" 
                        FilterControlWidth="100"
                        ItemStyle-Wrap="true" 
                        CurrentFilterFunction="Contains" 
                        ShowFilterIcon="false"
                        AutoPostBackOnFilter="true"
                        >
                        <ItemTemplate>
                            <asp:Label ID="lblBranchName" runat="server" Text='<%# Eval("Branch.BranchName") %>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn 
                                    UniqueName="IsActive" 
                                    AllowFiltering="false" 
                                    HeaderText="IsActive?"
                                    DataField="IsActive" 
                                    ItemStyle-HorizontalAlign="Center"  
                                    >
                                    <ItemTemplate>
                                        <asp:Image ID="imgIsActive"  runat="server" ImageUrl=<%# Convert.ToBoolean(Eval("IsActive")) ? "~/Images/checkbox_checked.png" : "~/Images/checkbox_unchecked.png"%> />
                                        
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn 
                        DataField="LicenseID" 
                        AllowFiltering="true" 
                        HeaderText=""
                        UniqueName="Edit">
                        <FilterTemplate>
                                 <asp:LinkButton ID="lbtnClearFillter" runat="server" CssClass="gray_bt" CommandName="ClearFilter">
                                                    <span> Clear Fillter </span>
                                </asp:LinkButton>
                        </FilterTemplate>
                        <ItemTemplate>
                        
                            
                            <div>
                                <center>
                                <div style="width: 45px;">     
                                    <div style="float:left; padding-right: 10px;">
                                         <asp:ImageButton ID="imgbtnEdit" 
                                                        runat="server" 
                                                        ToolTip="Edit License" 
                                                        AlternateText="Edit"
                                                        ImageUrl="../Images/edit_icon.png" 
                                                        CommandArgument ='<%# Eval("LicenseID") %>'
                                                        CommandName="Edit" />
                                    </div>
                                    <div style="float:left;" >
                                       <asp:ImageButton ID="imgbtnDelete" runat="server" 
                                                        ToolTip="Delete License" 
                                                        AlternateText="Delete"
                                                        ImageUrl="../Images/delete_icon.png" 
                                                        OnClientClick="javascript:return ConfirmDeleteLicense();"
                                                        CommandArgument ='<%# Eval("LicenseID") %>'
                                                        CommandName="Delete" />
                                     </div>
                                    <div class="clear"></div>
                            </div>
                            </center>
                            </div>

                         
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

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
