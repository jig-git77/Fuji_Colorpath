<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftMenuProjectItem.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.LeftMenuProjectItem" %>

<script type="text/javascript">
    function ConfirmDelete() {
            return confirm('Are you sure you want to delete this optimization run?');
        }
</script>

<style type="text/css">

</style>

<ul id="menu2">
    <li id="menu6">
    <table cellpadding="0" cellspacing="0" width="100%" >
        <tr id="TR_Run" runat="server">
            <td style="width:100%;">
                    <asp:LinkButton runat="server" ID="lnkRunName" OnClick="lnkRunName_Click"></asp:LinkButton>                
            </td>
            <td align="left">              
                    <asp:ImageButton ID="btnRunDelete" runat="server" 
                    ImageUrl="~/Images/delete_icon_LeftMenu.png" ToolTip="Delete Run" OnClientClick="return ConfirmDelete()" 
                    Visible="false" onclick="btnRunDelete_Click"/>                        
            </td>
        </tr>
        <tr>
            <td colspan="2">
                 <ul class="submenu" runat="server" id="ulSubMenu">
                    <li>
                        <asp:LinkButton runat="server" ID="lnkMeasurement" Text="Measurement" OnClick="lnkItem_Click"></asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton runat="server" ID="lnkAnalyse" Text="Analyze" OnClick="lnkItem_Click"></asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton runat="server" ID="lnkDownload" Text="Download" OnClick="lnkItem_Click"></asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton runat="server" ID="lnkAddRun" onclick="lnkAddRun_Click" Text="New Run" Visible="false" ></asp:LinkButton>       
                    </li>
                </ul>         
            </td>          
        </tr>
    </table>
    </li>
</ul>
