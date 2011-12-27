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
<table cellpadding="0" cellspacing="5" border="0" width="80%">
    <tr id="trFilter" runat="server">
        <td>
            <table cellpadding="0" cellspacing="5" border="0" width="100%" style="margin: 0 0 0 19px;">
                <tr>
                    <td style="width:30%; font-size:13px; font-weight:bold" >
                        Project Name:
                    </td>
                    <td>
                        <asp:Label ID="lblProjectName" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width:30%; font-size:13px; font-weight:bold" >
                        Source Profile:
                    </td>
                    <td>
                        <asp:Label ID="lblSourceProfile" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width:30%; font-size:13px; font-weight:bold" >
                        Destination Profile:
                    </td>
                    <td>
                        <asp:Label ID="lblDestinationProfile" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width:30%; font-size:13px; font-weight:bold">
                        DLP:
                    </td>
                    <td>
                        <asp:Label ID="lblDLP" runat="server"></asp:Label>
                    </td>
                </tr>
               <%-- <tr>
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
                </tr>--%>
                <tr>
                    <td style="width:30%; font-size:13px; font-weight:bold">
                        Project Status:
                    </td>
                    <td><asp:Image ID="imgStatus" runat="server" ImageUrl="../Images/close_icon.png" /></td>
                </tr>
                <tr>
                    <td style="height:20px" colspan="2"></td>
                </tr>
            </table>
        </td>
    </tr>
</table>

<div class="color_pallate">
    <div class="color_pallate_head">
        SyncRun Detail</div>   
        <div class="color_pallate_middle">
            <div id="Div1">           
                <table id='lvMeasurement1' cellspacing='0' cellpadding='0' border='0' class='recent_upload'>
                    <tbody>
                        <tr>
                            <th scope="col" width="20%">
                                SyncRun Name
                            </th>
                            <th scope="col" width="10%">
                                Status
                            </th>
                        </tr>
                 <asp:Repeater ID="rptSyncRun" runat="server" OnItemDataBound="rptSyncRun_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                 <%# Eval("SyncRunName") %>
                            </td>
                            <%--<td align="center">
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                            </td>--%>
                            <td align="center"><asp:Image ID="imgStatus" runat="server" ImageUrl="../Images/close_icon.png" /></td>
                        </tr>
                    </ItemTemplate> 
                </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="color_pallate_bottom">
            &nbsp;</div>
    </div>
    <%--<table>
    <tr>
        <th align="center">
            SyncRun Detail
        </th>
    </tr>
    <tr>
        <td>
            <asp:ListView ID="grdSyncRun" runat="server" ItemPlaceholderID="phItemTemplate" OnItemDataBound="grdSyncRun_ItemDataBound">
                <LayoutTemplate>
                    <table border="0" cellspacing="0" cellpadding="0" class="dlp_table">
                        <thead>
                            <tr>
                                <th scope="col" align="center" width="30%">
                                    SyncRun Name
                                </th>
                                <th scope="col" align="center" width="10%">
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
</table>--%>
    <%--<asp:Button ID="btnCloseWindow" runat="server" Text="Close" OnClientClick="javascript: CloseProjectInformation();" />--%>
