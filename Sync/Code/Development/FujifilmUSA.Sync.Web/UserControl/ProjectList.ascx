<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectList.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ProductList" %>
<script type="text/javascript" language="javascript">
    function ConfirmDeleteProject() {
        if (confirm("Are you sure you want to delete the selected project?"))
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
    <table border="0" cellspacing="0" cellpadding="0" class="product_list_table">
        <tr>
            <td>
                <telerik:RadGrid ID="grdProjectList" runat="server" AutoGenerateColumns="false"  OnItemCommand="grdProjectList_ItemCommand"
                    ShowFooter="false" OnNeedDataSource="grdProjectList_NeedDataSource" OnItemDataBound="grdProjectList_ItemDataBound" >
                    <MasterTableView DataKeyNames="ProjectId">
                        <Columns>
                            <%--<telerik:GridBoundColumn DataField="ProjectName" UniqueName="ProjectName" HeaderText="Project Name"></telerik:GridBoundColumn>--%>
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
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
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
                    width="400px"
                    height="400px"
                    title="Project"
                    behaviors="Close, Move" Behavior="Close, Move">
                </telerik:RadWindow>
            </Windows>

    </telerik:RadWindowManager>