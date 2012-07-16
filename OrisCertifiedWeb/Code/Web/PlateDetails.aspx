<%@ Page Language="C#" AutoEventWireup="true"  EnableEventValidation="false" MasterPageFile="~/MasterDeviceTrendLinePlate.master" CodeFile="PlateDetails.aspx.cs" Inherits="PlateDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
<ContentTemplate>
<script language="javascript">
    function Pageopen( CPMeasurementGUID )
    {
        window.open( "DataHeaderPlate.aspx?CPMeasurementGUID="+ CPMeasurementGUID , 'DataHeaderPlate' , 'width=950px,height=700px,resizable=1,scrollbars=1' );
    }
</script>    
    <table cellpadding="2" cellspacing="0" border="0" class="popup-table" style="margin-top:5px">
        <tr class="popup-top-bar">
            <td style="width: 50%">
                &nbsp;&nbsp;<asp:Button ID="BTN_Header" runat="server" CssClass="popup-button" Text="Measurement File" />
                &nbsp;&nbsp;<asp:Button ID="btnDownloadFile" runat="server" CssClass="popup-button" Text="Download File" OnClick="BTN_DownloadFile_Click" />
            </td>
            <td>
                &nbsp;
            </td>
            <td style="width: 50%" align="right">
                <asp:Button ID="BTN_DeleteJob" runat="server" CssClass="popup-button" 
                    Text="Discard CPV Measurement" onclick="BTN_DeleteJob_Click" />&nbsp;&nbsp;
            </td>
        </tr>
        <tr  class="popup-table-td">
            <td valign="top" style="padding:5px">
                <div class="green-title">Output Device</div>
                <table cellpadding="0" cellspacing="0" border="0" class="DATAGRID" width="100%">
                    <tr>
                        <td style="width:20%;" class="ALTERNATINGITEMSTYLE"><strong>Name</strong></td>
                        <td style="width:30%;" class="ITEMSTYLE">&nbsp;<asp:Literal ID="LTRL_Name" runat="server" >&nbsp;</asp:Literal></td>
                        <td style="width:20%;" class="ALTERNATINGITEMSTYLE"><strong>SN</strong></td>
                        <td style="width:30%;" class="ITEMSTYLE">&nbsp;<asp:Literal ID="LTRL_SN" runat="server" >&nbsp;</asp:Literal></td>
                    </tr>
                    <tr>
                        <td class="ALTERNATINGITEMSTYLE"><strong>Manufacturer</strong></td>
                        <td class="ITEMSTYLE">&nbsp;<asp:Literal ID="LTRL_Manufacturer" runat="server" >&nbsp;</asp:Literal></td>
                        <td class="ALTERNATINGITEMSTYLE"><strong>Model</strong></td>
                        <td class="ITEMSTYLE">&nbsp;<asp:Literal ID="LTRL_Model" runat="server" >&nbsp;</asp:Literal></td>
                    </tr>
                 </table>                 
                 <br />
                 <div class="green-title">Measurement Info</div>   
                <table cellpadding="0" cellspacing="0" border="0" class="DATAGRID" width="100%">
                    <tr>
                        <td style="width:20%;" class="ALTERNATINGITEMSTYLE"><strong>Device</strong></td>
                        <td style="width:30%;" class="ITEMSTYLE">&nbsp;<asp:Literal ID="LTRL_Device" runat="server" >&nbsp;</asp:Literal></td>
                        <td style="width:20%;" class="ALTERNATINGITEMSTYLE"><strong>SN</strong></td>
                        <td style="width:30%;" class="ITEMSTYLE">&nbsp;<asp:Literal ID="LTRL_SN_1" runat="server" >&nbsp;</asp:Literal></td>
                    </tr>
                    <tr>
                        <td class="ALTERNATINGITEMSTYLE"><strong>User</strong></td>
                        <td class="ITEMSTYLE">&nbsp;<asp:Literal ID="LTRL_User" runat="server" >&nbsp;</asp:Literal></td>
                        <td class="ALTERNATINGITEMSTYLE"><strong>Customer</strong></td>
                        <td class="ITEMSTYLE">&nbsp;<asp:Literal ID="LTRL_Customer" runat="server" >&nbsp;</asp:Literal></td>
                    </tr>
                    <tr class="ALTERNATINGITEMSTYLE">
                        <td class="ALTERNATINGITEMSTYLE"><strong>Information</strong></td>
                        <td colspan="3" class="ITEMSTYLE">&nbsp;<asp:Literal ID="LTRL_Information" runat="server" >&nbsp;</asp:Literal></td>
                    </tr>
                    <tr class="ITEMSTYLE">
                        <td class="ALTERNATINGITEMSTYLE"><strong>Target File</strong></td>
                        <td colspan="3" class="ITEMSTYLE">&nbsp;<asp:Literal ID="LTRL_TargetFile" runat="server" >&nbsp;</asp:Literal></td>
                    </tr>
                    <tr class="ALTERNATINGITEMSTYLE">
                        <td class="ALTERNATINGITEMSTYLE"><strong>GUID</strong></td>
                        <td colspan="3" class="ITEMSTYLE">&nbsp;<asp:Literal ID="LTRL_GUID" runat="server" >&nbsp;</asp:Literal></td>
                    </tr>
                 </table>     
            </td>
            <td>&nbsp;</td>
            <td valign="top" style="padding:5px">
                <div class="green-title">Substrate Info</div>    
                <table cellpadding="0" cellspacing="0" border="0"  class="DATAGRID"  width="100%">
                    <tr>
                        <td style="width:20%;" class="ALTERNATINGITEMSTYLE"><strong>Manu</strong></td>
                        <td style="width:30%;" class="ITEMSTYLE">&nbsp;<asp:Literal ID="LTRL_Manu" runat="server" >&nbsp;</asp:Literal></td>
                        <td style="width:20%;" class="ALTERNATINGITEMSTYLE"><strong>SKU</strong></td>
                        <td style="width:30%;" class="ITEMSTYLE">&nbsp;<asp:Literal ID="LTRL_SKU" runat="server" >&nbsp;</asp:Literal></td>
                    </tr>
                     <tr>
                        <td class="ALTERNATINGITEMSTYLE"><strong>Batch</strong></td>
                        <td colspan="3" class="ITEMSTYLE">&nbsp;<asp:Literal ID="LTRL_Batch" runat="server" >&nbsp;</asp:Literal></td>                        
                    </tr>
                 </table>       
                 <br />
                 <div class="green-title">Chemistry Info</div>   
                <table cellpadding="0" cellspacing="0" border="0" class="DATAGRID" width="100%">
                    <tr class="ALTERNATINGITEMSTYLE">
                        <td style="width:20%;" class="ALTERNATINGITEMSTYLE"><strong>Manu</strong></td>
                        <td style="width:30%;" class="ITEMSTYLE">&nbsp;<asp:Literal ID="LTRL_Manu_1" runat="server" >&nbsp;</asp:Literal></td>
                        <td style="width:20%;" class="ALTERNATINGITEMSTYLE"><strong>SKU</strong></td>
                        <td style="width:30%;" class="ITEMSTYLE">&nbsp;<asp:Literal ID="LTRL_SKU_1" runat="server" >&nbsp;</asp:Literal></td>
                    </tr>
                     <tr class="ITEMSTYLE">
                        <td class="ALTERNATINGITEMSTYLE"><strong>Batch</strong></td>
                        <td colspan="4" class="ITEMSTYLE">&nbsp;<asp:Literal ID="LTRL_Batch_1" runat="server" >&nbsp;</asp:Literal></td>
                        
                    </tr>
                 </table>       
            </td>
        </tr>
     </table>    
<asp:Literal ID="LblError" runat="server"></asp:Literal>
<asp:Literal ID="LTRL_CPMeasurementGUID" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="LTRL_CPDeviceID" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="LTRL_CPDevicePrintTargetID" runat="server" Visible="false"></asp:Literal>  
<asp:Literal ID="LTRL_KeyDetails" runat="server" Visible="false"></asp:Literal>
</ContentTemplate>

</asp:Content>