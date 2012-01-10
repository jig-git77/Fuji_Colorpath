<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectList.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ProductList" %>
<script type="text/javascript" language="javascript">
    function ConfirmDeleteProject() {
        if (confirm('<%= ProjectList_DeleteProject_Confirmation %>'))
            return true;
        else
            return false;
    }
    
    function windowLoad(oWindow, args) {
        oWindow.set_title("Project Information");
        oWindow.set_status('');
    } 

    function OpenInformationBox(url) {        
        var oWnd = radopen(url, "ProjectInformationRadWindow");
        oWnd.add_pageLoad(windowLoad);       
        return false;
    }
</script>
<script language="javascript" type="text/javascript">
    function Close() {
        var oWindow = GetRadWindow1();
        //oWindow.BrowserWindow.location.href = GetRadWindow1().BrowserWindow.location.href;
        GetRadWindow1().close();
    }

    function GetRadWindow1() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow;
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
        return oWindow;
    } 
</script>


<div class="contant_top">
    &nbsp;</div>    
<div class="contant_middle">
    <div class="head">
        <h1>
            Project List</h1>
        <a href="AddProject.aspx" class="button"><span>
            <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/new_project_icon.png" />
            New Project</span></a></div>
    <div class="clear">
    </div>
    <%--<table border="0" cellspacing="0" cellpadding="0" class="product_list_table">
        <tr>
            <td>--%>
              <asp:UpdatePanel ID="upPanel" runat="server">
            <ContentTemplate>
            <asp:ListView ID="grdProjectList" runat="server" OnItemCommand="grdProjectList_ItemCommand" OnItemDataBound="grdProjectList_ItemDataBound" ItemPlaceholderID="phItemPlaceholder">
            <LayoutTemplate>
                <table cellspacing="0" cellpadding="0" border="0" class="product_list_table">
                <thead>
                    <tr>
                        <th align="left" scope="col">Project Name</th>
                        <th align="center" scope="col" width="10%">Info</th>
                        <th align="center" scope="col" width="10%">Delete</th>
                        <th align="center" scope="col" width="10%">Status</th>
                    </tr>
                </thead>
                <tbody>                                    
                    <asp:PlaceHolder ID="phItemPlaceholder" runat="server"></asp:PlaceHolder>
                </tbody>
                </table>
            </LayoutTemplate>
            <EmptyDataTemplate><div class="no_data_found">No records available</div></EmptyDataTemplate>
            <ItemTemplate>
                <tr class="purpal">
                    <td align="left"><asp:LinkButton ID="lbtnProjectName" runat="server" Text='<%# Eval("ProjectName") %>' ForeColor="Blue" Font-Underline="true" OnClick="lbtnProjectName_Click" /></td>
                    <td align="center"><asp:ImageButton ID="btnInfo" runat="server" ToolTip="Information" AlternateText="Information" ImageUrl="../Images/info_icon.png" /></td>
                    <td align="center"><asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete Project" AlternateText="Delete" ImageUrl="../Images/delete_icon.png" CommandName="DeleteProject" CommandArgument='<%# Eval("ProjectId") %>' /></td>
                    <td align="center"><asp:Image ID="imgStatus" runat="server" ImageUrl="../Images/close_icon.png" /></td>
              </tr>
            </ItemTemplate>
            </asp:ListView>
              </ContentTemplate>
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID="grdProjectList" EventName="ItemCommand" />
            </Triggers> 
        </asp:UpdatePanel>
                <%--<telerik:RadGrid ID="grdProjectList" runat="server" AutoGenerateColumns="false"  OnItemCommand="grdProjectList_ItemCommand"
                    ShowFooter="false" OnNeedDataSource="grdProjectList_NeedDataSource" OnItemDataBound="grdProjectList_ItemDataBound" >
                    <MasterTableView DataKeyNames="ProjectId">
                        <Columns>                            
                            <telerik:GridTemplateColumn UniqueName="ProjectName" HeaderText="Project Name">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnProjectName" runat="server" Text='<%# Eval("ProjectName") %>' ForeColor="Blue" Font-Underline="true" OnClick="lbtnProjectName_Click" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>                            
                            <telerik:GridTemplateColumn UniqueName="Info" HeaderText="Info">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnInfo" runat="server" ToolTip="Information" AlternateText="Information" ImageUrl="../Images/info_icon.png" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="Delete" HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete Project" AlternateText="Delete" ImageUrl="../Images/delete_icon.png" CommandName="DeleteProject" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="Status" HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Image ID="imgStatus" runat="server" ImageUrl="../Images/close_icon.png" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>--%>
           <%-- </td>
        </tr>
    </table>--%>
</div>
<div class="contant_bottom">
    &nbsp;</div>

<div class="clear">
</div>
<telerik:RadWindowManager ID="RadWindowManager" runat="server" Width="450px" 
    Height="400px" Modal="True" Title="Project Information">
    <Windows>
                <telerik:RadWindow 
                    id="ProjectInformationRadWindow" 
                    runat="server"
                    showcontentduringload="true"
                    width="750px"
                    height="500px"
                    title="Project"
                    behaviors="Close, Move" Behavior="Close, Move">
                </telerik:RadWindow>
            </Windows>

    </telerik:RadWindowManager>