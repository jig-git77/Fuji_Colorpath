<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="LeftMenuProjectItem.ascx.cs"

    Inherits="FujifilmUSA.Sync.Web.UserControl.LeftMenuProjectItem" %>
<script type="text/javascript">
    function ConfirmDelete() {
        return confirm('<%= GetResourceString("deleteRunMsg") %>');
    }

    function ConfirmDLPDelete() {
        return confirm('<%= FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentProject.LicenseType == FujifilmUSA.Sync.Entity.ELicenseType.SpotLibrary? GetResourceString("deleteNCPMsg"):GetResourceString("deleteDLPMsg") %>');
    }
</script>

<style type="text/css">

</style>

<ul id="menu2">
    <li id="menu6">
    <table cellpadding="0" cellspacing="0" width="100%" >
        <tr id="TR_Run" runat="server">
            <td style="width:100%;">
                    <asp:LinkButton runat="server" ID="lnkRunName" OnClick="lnkRunName_Click" 
                        meta:resourcekey="lnkRunNameResource1"></asp:LinkButton>                
            </td>
            <td align="left">              
                    <asp:ImageButton ID="btnRunDelete" runat="server" 
                    ImageUrl="~/Images/delete_icon_LeftMenu.png" ToolTip="Delete Run" OnClientClick="return ConfirmDelete()" 
                    Visible="False" onclick="btnRunDelete_Click" 
                        meta:resourcekey="btnRunDeleteResource1"/>                        
            </td>
        </tr>
        <tr>
            <td colspan="2">
                 <ul class="submenu" runat="server" id="ulSubMenu">
                    <li>
                        <asp:LinkButton runat="server" ID="lnkMeasurement" Text="Measurement" 
                            OnClick="lnkItem_Click" meta:resourcekey="lnkMeasurementResource1"></asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton runat="server" ID="lnkAnalyse" Text="Analyze" 
                            OnClick="lnkItem_Click" meta:resourcekey="lnkAnalyseResource1"></asp:LinkButton>
                    </li>
                    <li>
                         <table cellpadding="0" cellspacing="0" width="100%" >
                            <tr id="TR1" runat="server">
                                <td style="width:100%;">
                                    <asp:LinkButton runat="server" ID="lnkDownload" Text="Download" 
                                        OnClick="lnkItem_Click" meta:resourcekey="lnkDownloadResource1"></asp:LinkButton>
                                </td>
                                <td align="left">       
                                    <asp:ImageButton ID="btnDLPDelete" runat="server" 
                                    ImageUrl="~/Images/delete_icon_LeftMenu.png" ToolTip="Delete Run" OnClientClick="return ConfirmDLPDelete()" 
                                    Visible="False" meta:resourcekey="btnDLPDeleteResource1" 
                                    onclick="btnDLPDelete_Click"/>    
                                </td>
                            </tr>
                         </table>
                    </li>
                    <li>
                        <asp:LinkButton runat="server" ID="lnkAddRun" onclick="lnkAddRun_Click" 
                            Text="New Run" Visible="False" meta:resourcekey="lnkAddRunResource1" ></asp:LinkButton>       
                    </li>
                </ul>         
            </td>          
        </tr>
    </table>
    </li>
</ul>
