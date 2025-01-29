<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ProfilerOptionPresetsManagement.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.ProfilerOptionPresetsManagement" %>
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
    
     .breakWord150
     {
            max-width: 150px !important;
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
     .gray-out {
    opacity: 0.5; /* Reduce opacity */
    pointer-events: none; /* Disable interaction */
}
     
</style>
<script type="text/javascript" language="javascript">
    function ConfirmDeleteSourceProfile() {
        if (confirm('<%= ProfilerOptionPresetsManagement_DeleteSourceProfile_Confirmation %>'))
            return true;
        else
            return false;
    }
</script>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle">
    <div class="head">
        <h1><asp:Localize runat="server" Text="Profiler Option Preset Management" 
                ID="locProfilerOptionPresetMangementResource" meta:resourcekey="locProfilerOptionPresetMangementResource1"></asp:Localize></h1>
        <asp:LinkButton ID="likAdd" runat="server" class="button" 
            OnClick="likAdd_Click" meta:resourcekey="likAddResource1">
            <span>
                <asp:Image ID="imgAdd" runat="server" 
            ImageUrl="../Images/new_project_icon.png" meta:resourcekey="imgAddResource1" />
                
<asp:Localize Text="Add Device Family Profiler Presets" runat="server" ID="locProfilerOptionPreset" 
            meta:resourcekey="locAddProfilerOptionPresetResource1"></asp:Localize>
</span>
        </asp:LinkButton>
    </div>
<div class="clear">
    </div>
    <div style="margin-left: 10px;">
    <%--<asp:UpdatePanel ID="testPanel" runat="server">
        <ContentTemplate>--%>
            <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
            Visible="False" />
            <telerik:RadGrid 
                ID="rgProfilerOptionPresetMangement" 
                AllowFilteringByColumn="True"
                AllowSorting="True" 
                AllowPaging="True" 
                PageSize="5" 
                runat="server" 
                AutoGenerateColumns="False"
                EnableLinqExpressions="False" 
                OnNeedDataSource="rgProfilerOptionPresetMangement_NeedDataSource"
                OnItemCommand="rgProfilerOptionPresetMangement_ItemCommand" 
                OnItemDataBound="rgProfilerOptionPresetMangement_ItemDataBound"
                Width="98%" 
                HeaderStyle-Font-Bold="true" CellSpacing="0" GridLines="None">
                <GroupingSettings CaseSensitive="false" />                

<HeaderStyle Font-Bold="True"></HeaderStyle>

                <PagerStyle Position="Top" AlwaysVisible="true" Font-Size="Small" />

                <MasterTableView DataKeyNames="DeviceFamilyProfilerOptionID,PresetName">
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

<ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                    <Columns>

                        <telerik:GridTemplateColumn 
                            UniqueName="PresetName" 
                            HeaderText="Preset Name"
                            DataField="PresetName" 
                            SortExpression="PresetName" 
                            ShowFilterIcon="true"
                            FilterControlWidth="150"
                            HeaderStyle-Width ="150" 
                            AllowFiltering="true" 
                            ItemStyle-CssClass="breakWord150" ItemStyle-Width="150" 
                            ItemStyle-Wrap="true" AutoPostBackOnFilter="true" 
                            CurrentFilterFunction="Contains" meta:resourcekey="GridTemplateColumnResource1">
                            <ItemTemplate>
                                <asp:Label ID="lblPresetName" runat="server" Text='<%# Eval("PresetName") %>' 
                                    meta:resourcekey="locPresetNameResource1"/>
                            </ItemTemplate>
                            <HeaderStyle Width="200px"></HeaderStyle>
                            <ItemStyle Wrap="True" CssClass="breakWord150" Width="150px"></ItemStyle>
                        </telerik:GridTemplateColumn>
                        
                        <telerik:GridTemplateColumn 
                            UniqueName="ProjectType" 
                            HeaderText="Project Type"
                            DataField="ProjectType" 
                            SortExpression="ProjectType" 
                            ShowFilterIcon="true"
                            FilterControlWidth="70"
                            HeaderStyle-Width ="70" 
                            AllowFiltering="true" 
                            ItemStyle-CssClass="breakWord70" ItemStyle-Width="70" 
                            ItemStyle-Wrap="true" AutoPostBackOnFilter="true" 
                            CurrentFilterFunction="Contains" meta:resourcekey="GridTemplateColumnResource6">
                            <ItemTemplate >
                                <asp:Label ID="lblProjectType" runat="server" Text='<%# Eval("ProjectType") %>' />
                            </ItemTemplate>
                            <HeaderStyle Width="150px"></HeaderStyle>
                            <ItemStyle Wrap="True" CssClass="breakWord150" Width="70px"></ItemStyle>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn 
                            UniqueName="DeviceFamily" 
                            HeaderText="Device Family"
                            DataField="DeviceFamily" 
                            SortExpression="DeviceFamily" 
                            ShowFilterIcon="true"
                            FilterControlWidth="150"
                            HeaderStyle-Width ="150" 
                            AllowFiltering="true" 
                            ItemStyle-CssClass="breakWord150" ItemStyle-Width="150" 
                            ItemStyle-Wrap="true" AutoPostBackOnFilter="true" 
                            CurrentFilterFunction="Contains" meta:resourcekey="GridTemplateColumnResource7">
                            <ItemTemplate>
                                <asp:Label ID="lblDeviceFamily" runat="server" Text='<%# Eval("DeviceFamily") %>' 
                                   meta:resourcekey="locDeviceFamilyResource1"/>
                            </ItemTemplate>
                            <HeaderStyle Width="200px"></HeaderStyle>
                            <ItemStyle Wrap="True" CssClass="breakWord150" Width="150px"></ItemStyle>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn 
                            UniqueName="IsActive" 
                            AllowFiltering="true" 
                            HeaderText="Is Active"
                            DataField="IsActive" 
                            SortExpression="IsActive" 
                            ShowFilterIcon="true"
                            FilterControlWidth="150"
                            HeaderStyle-Width ="150"
                            ItemStyle-CssClass="breakWord150" ItemStyle-Width="150" 
                            ItemStyle-Wrap="true" AutoPostBackOnFilter="true" 
                            CurrentFilterFunction="Contains" meta:resourcekey="GridTemplateColumnResource2">
                           <ItemTemplate>
                                    <asp:Image ID="imgIsActive" runat="server" 
                                        ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) ? "~/Images/checkbox_checked.png" : "~/Images/checkbox_unchecked.png" %>' 
                                        meta:resourcekey="imgIsActiveResource1" />
                                </ItemTemplate>

<HeaderStyle Width="150px"></HeaderStyle>

<ItemStyle Wrap="True" CssClass="breakWord150" Width="150px"></ItemStyle>
                        </telerik:GridTemplateColumn>
   <telerik:GridTemplateColumn 
                                DataField="DeviceFamilyProfilerOptionID" 
                                AllowFiltering="true" 
                                HeaderText=""
                                UniqueName="Edit" meta:resourcekey="GridTemplateColumnResource5">
                                <FilterTemplate>
                                    <center>
                                    <asp:LinkButton ID="lbtnClearFillter" runat="server" ToolTip ="Clear Fillter" 
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
                                                      <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Edit Profiler Option Preset" AlternateText="Edit"
                                                        ImageUrl="../Images/edit_icon.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"DeviceFamilyProfilerOptionID") %>'
                                                        CommandName="Edit" meta:resourcekey="imgbtnUpdateResource1" />
                                                </div>
                                                <div style="float: left;">
                                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete Profiler Preset"
                                                        AlternateText="Delete" ImageUrl="../Images/delete_icon.png" OnClientClick="return ConfirmDeleteSourceProfile();"
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"DeviceFamilyProfilerOptionID") %>'
                                                        CommandName="Delete" Visible="true" 
                                                        meta:resourcekey="imgbtnDeleteResource1" />
                                                     <asp:HiddenField runat="server" ID="hdfIsDelete" Value='<%# Convert.ToInt32(Eval("IsDeleted"))%>'  />
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                        </center>
                                    </div>
                                </ItemTemplate>
                         </telerik:GridTemplateColumn>                       
                    </Columns>


                </MasterTableView>

            </telerik:RadGrid>
       <%-- </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lbtnProfile" />
        </Triggers>
    </asp:UpdatePanel>--%>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
