<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectList.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ProductList" %>
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
                <telerik:RadGrid ID="grdProjectList" runat="server" AutoGenerateColumns="false" 
                    ShowFooter="false" OnNeedDataSource="grdProjectList_NeedDataSource">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridBoundColumn DataField="ProjectName" UniqueName="ProjectName" HeaderText="Project Name">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="Info" HeaderText="Info">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnInfo" runat="server" ImageUrl="../Images/info_icon.png" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="Delete" HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnDelete" runat="server"  ImageUrl="../Images/delete_icon.png"/>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="Status" HeaderText="Status">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnStatus" runat="server" ImageUrl="../Images/close_icon.png" />
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
