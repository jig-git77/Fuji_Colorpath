<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImportMeasurements.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.PopupControl.ImportMeasurements" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>

<script language="javascript" type="text/javascript">
    function Close() {
        var oWindow = GetMeasurementRadWindow();
        GetMeasurementRadWindow().close();
        return false;
    }

    function GetMeasurementRadWindow() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow;
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
        return oWindow;
    }

    function checkboxclick(e, idFragment) {       
        var currentCheckBox = e.srcElement || e.target;
        var inputs = document.getElementsByTagName("input");
        for (var i = 0; i < inputs.length; i++) {
            var input = inputs[i];
            if (input.id == currentCheckBox.id)
                continue;
            if (input.id.indexOf(idFragment) < 0)
                continue;
            //clear out the rest of the checkboxes 
            if (input.type && input.type == "checkbox") {
                input.checked = false;
            }
        }
    }

    function OnClientClick() {
        var checking = false;
        var answer;
        debugger;
        var inputs = document.getElementsByTagName("input");
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].checked == true) {
                checking = true;
                break;
            }
        }
        if (!checking) {
            alert("Atleast one project need to be selected!");
            return false;
        }
        else
            return true;
        
    }

    function CloseImportWindow() {
        var oWindow = GetMeasurementRadWindow();
        GetMeasurementRadWindow().close();
        top.location.href = top.location.href;
    }

</script>

<div style="padding-left:5px;">


     <table cellpadding="0" cellspacing="10" width="100%">
        <tr>
            <td>
                <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
            </td>
        </tr>
        <tr>
            <td>
                <div style="display: block;" class="contantbox tabcontant">
                    <div class="section_top">
                        &nbsp;</div>
                    <div class="section_middle">
                        <div class="combopart">
                            <div>
                            </div>
                            <div class="header">
                                <div class="content">
                                    <div style="float: left;" class="text">
                                       Select Project to import all its measurements in this project: 
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 5px;" >
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="testPanel" runat="server">
                                                   <ContentTemplate>
                                                       <telerik:RadGrid ID="rgProjectList" AllowFilteringByColumn="True" AllowPaging="True"
                                                            PageSize="5" runat="server" AutoGenerateColumns="False" EnableLinqExpressions="False"
                                                            OnNeedDataSource="rgProjectList_NeedDataSource" Width="100%" CellSpacing="0" GridLines="None"
                                                            AllowSorting="True" OnItemCommand="rgProjectList_ItemCommand">
                                                            <GroupingSettings CaseSensitive="False" />
                                                            <HeaderStyle Font-Bold="True" />
                                                            <PagerStyle Position="Top" AlwaysVisible="True" Font-Size="Small" />
                                                            <MasterTableView DataKeyNames="ProjectId">
                                                                <CommandItemSettings ExportToPdfText="Export to PDF" />
                                                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                                                                    Visible="True">
                                                                </RowIndicatorColumn>
                                                                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                                                                    Visible="True">
                                                                </ExpandCollapseColumn>
                                                                <Columns>
                                                                    <telerik:GridTemplateColumn DataField="ProjectId" AllowFiltering="False"
                                                                        UniqueName="Select" SortExpression="ProjectId" 
                                                                        FilterControlAltText="Filter Select column" 
                                                                        meta:resourcekey="GridTemplateColumnResource1">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkbtnSelect" runat="server" ToolTip="Select Project" onclick="checkboxclick(event, 'chkbtnSelect');"/>                                                                           
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="30px" />
                                                                    </telerik:GridTemplateColumn>
                                                        
                                                                    <telerik:GridTemplateColumn DataField="ProjectName" HeaderText="Name"
                                                                        UniqueName="ProjectName" SortExpression="ProjectName"
                                                                        FilterControlWidth="80px" AutoPostBackOnFilter="True" 
                                                                        CurrentFilterFunction="Contains" 
                                                                        FilterControlAltText="Filter ProjectName column" 
                                                                        meta:resourcekey="GridTemplateColumnResource3">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblProjectName" runat="server" Text='<%# Eval("ProjectName") %>' 
                                                                                meta:resourcekey="lblProjectNameResource1" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="breakWord80" Width="80px" Wrap="True" />
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridTemplateColumn UniqueName="SourceProfile" 
                                                                        DataField="SourceProfile.ProfileName" HeaderText="Standard" 
                                                                        FilterControlWidth="80px"
                                                                        SortExpression="SourceProfile.ProfileName" AutoPostBackOnFilter="True" 
                                                                        CurrentFilterFunction="Contains" 
                                                                        FilterControlAltText="Filter SourceProfile column" 
                                                                        meta:resourcekey="GridTemplateColumnResource4">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblStandard" runat="server" 
                                                                                Text='<%# Eval("SourceProfile.ProfileName") %>' 
                                                                                meta:resourcekey="lblStandardResource1" />
                                                                        </ItemTemplate>                                           
                                                                        <ItemStyle CssClass="breakWord80" Width="80px" Wrap="True" />
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridTemplateColumn DataField="PrintCondition.Media"
                                                                        HeaderText="Media" UniqueName="Media" SortExpression="PrintCondition.Media"
                                                                        FilterControlWidth="60px" AutoPostBackOnFilter="True" 
                                                                        CurrentFilterFunction="Contains" 
                                                                        FilterControlAltText="Filter Media column" 
                                                                        meta:resourcekey="GridTemplateColumnResource5">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMedia" runat="server" 
                                                                                Text='<%# Eval("PrintCondition.Media") %>' 
                                                                                meta:resourcekey="lblMediaResource1" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="breakWord60" Width="60px" Wrap="True" />
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridTemplateColumn 
                                                                        DataField="PrintCondition.Colorant"
                                                                        HeaderText="Colorant" UniqueName="Colorant" SortExpression="PrintCondition.Colorant"
                                                                        FilterControlWidth="60px" AutoPostBackOnFilter="True" 
                                                                        CurrentFilterFunction="Contains" 
                                                                        FilterControlAltText="Filter Colorant column" 
                                                                        meta:resourcekey="GridTemplateColumnResource6">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblColorant" runat="server" 
                                                                                Text='<%# Eval("PrintCondition.Colorant") %>' 
                                                                                meta:resourcekey="lblColorantResource1" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="breakWord60" Width="60px" />
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridDateTimeColumn DataField="CreateDate" HeaderText="Last Active"
                                                                            SortExpression="CreateDate" UniqueName="CreateDate" DataFormatString="{0:MM/dd/yy hh:mm}"
                                                                            ShowFilterIcon="False" FilterControlWidth="100px" 
                                                                        AutoPostBackOnFilter="True" CurrentFilterFunction="GreaterThanOrEqualTo" 
                                                                        FilterControlAltText="Filter CreateDate column" 
                                                                        meta:resourcekey="GridDateTimeColumnResource1">
                                                                            <HeaderStyle Width="100px" />
                                                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                   </telerik:GridDateTimeColumn>
                                       
                                                                    <telerik:GridTemplateColumn DataField="ProjectId" UniqueName="Delete" 
                                                                        FilterControlAltText="Filter Delete column" 
                                                                        meta:resourcekey="GridTemplateColumnResource7">                                           
                                                                        <FilterTemplate>
                                                                            <asp:LinkButton ID="lbtnClearFillter" runat="server" CssClass="button" CommandName="ClearFilter"
                                                                                ToolTip="Clear Fillter">
                                                                                <span>
                                                                                    <asp:Image ID="imgClearFillter" runat="server" 
                                                                                AlternateText="Clear Fillter" ImageUrl="~/Images/clear-fillter.png"
                                                                                meta:resourcekey="imgClearFillterResource1" />
                                                                                </span>
                                                                            </asp:LinkButton>
                                                                        </FilterTemplate>                                                              
                                                                        <HeaderStyle Width="40px" />
                                                                    </telerik:GridTemplateColumn>
                                                                </Columns>                                    
                                                            </MasterTableView>                                
                                                        </telerik:RadGrid>
                                                   </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="clear">
                            </div>
                        </div>
                    </div>
                    <div class="section_bottom">
                        &nbsp;</div>
                </div>
            </td>
        </tr>
       
        <tr>
            <td>
                <div class="buttonright" style="padding-left: 3px">
                    <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript: Close();"
                        CssClass="gray_bt">
                                        <span> <asp:Localize runat="server" ID="locCancel" 
                        Text="Cancel" meta:resourcekey="locCancelResource1"></asp:Localize> 
                                </span>
                    </asp:LinkButton>
                </div>
                <div class="buttonright">
                    <asp:LinkButton ID="lbtnImport" runat="server" ValidationGroup="vgSaveWindows" 
                        CssClass="gray_bt" onclick="lbtnImport_Click" OnClientClick="if (!OnClientClick()) {return false;}">
                       
                    <span> <asp:Localize ID="Localize2" runat="server" Text="Import" 
                        meta:resourcekey="LocalizeResource2"></asp:Localize> 
                        </span>
                    </asp:LinkButton>
                </div>
                <div class="clear">
                </div>
            </td>
        </tr>
    </table>

</div>