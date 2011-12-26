<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectInformation.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ProjectInformation" %>
<script language="javascript" type="text/javascript">
    function CloseProjectInformation() {
        var oWindow = GetProjectInformationRadWindow();
        GetProjectInformationRadWindow().close();
        return false;
    }

    function GetProjectInformationRadWindow() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow;
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
        return oWindow;
    } 
</script>
<div class="admin_middle">
    <div class="add_form">
        <table cellpadding="0" cellspacing="5" border="0" width="100%">
            <tr id="trFilter" runat="server">
                <td>
                    <table cellpadding="0" cellspacing="5" border="0" width="100%">
                        <tr>
                            <td>
                                Project Name:
                            </td>
                            <td>
                                <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Source Profile:
                            </td>
                            <td>
                                <asp:Label ID="lblSourceProfile" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Destination Profile:
                            </td>
                            <td>
                                <asp:Label ID="lblDestinationProfile" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                DLP;
                            </td>
                            <td>
                                <asp:Label ID="lblDLP" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Device Type:
                            </td>
                            <td>
                                <asp:Label ID="lblDeviceType" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                ReferenceChart Type:
                            </td>
                            <td>
                                <asp:Label ID="lblReferenceChartType" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Project Status:
                            </td>
                            <td>
                                <asp:Label ID="lblProjectStatus" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th align="center">SyncRun Detail</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListView ID="grdSyncRun" runat="server" ItemPlaceholderID="phItemTemplate" 
                                    onitemdatabound="grdSyncRun_ItemDataBound" >
                                    <LayoutTemplate>
                                        <table border="0" cellspacing="0" cellpadding="0" class="dlp_table">
                                            <thead>
                                                <tr>
                                                    <th scope="col" align="left">
                                                        &nbsp;
                                                    </th>
                                                    <th scope="col" align="center">
                                                        SyncRun Name
                                                    </th>
                                                    <th scope="col" align="center">
                                                        Status
                                                    </th>                                                   
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:PlaceHolder ID="phItemTemplate" runat="server"></asp:PlaceHolder>
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                        <div class="no_data_found">
                                            No records available</div>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr class="purpal">                                            
                                            <td align="center">
                                                <%# Eval("SyncRunName") %>
                                            </td>
                                            <td align="center">
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>                                                
                                            </td>                                            
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</div>
<asp:Button ID="btnCloseWindow" runat="server" Text="Close" OnClientClick="javascript: CloseProjectInformation();" />