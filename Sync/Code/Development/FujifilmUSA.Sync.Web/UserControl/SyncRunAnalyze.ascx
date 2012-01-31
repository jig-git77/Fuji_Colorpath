<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SyncRunAnalyze.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.SyncRunAnalyze" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="uc" %>
<script type="text/javascript" language="javascript">
    function ConfirmMeasurement() {
        if (confirm('<%= AddMeasurement_DeleteMeasurement_Confirmation %>'))
            return true;
        else
            return false;
    }
</script>

<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <h1>
            Analyze</h1>
        <div class="Information_divAnalyze">
            <asp:LinkButton ID="btnOptimize" runat="server"  CssClass="button" 
                onclick="btnOptimize_Click">
                <span>
                    <asp:Image ID="Image2" runat="server" ImageUrl="../Images/export.png" />
                   Optimize Profile</span> &nbsp; 
            </asp:LinkButton>
            <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click" CssClass="button">
                <span>
                    <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/export.png" />
                    Export</span>
            </asp:LinkButton>            
            <asp:LinkButton ID="btnCertify" runat="server" CssClass="button" OnClick="btnCertify_Click">
                <span>
                    <asp:Image ID="Image1" runat="server" ImageUrl="../Images/export.png" />
                    Certify</span>
            </asp:LinkButton> &nbsp;
        
        </div>
    </div>
    <div class="clear">
    </div>

    <div class="ContainerClass">
        <div class="info_form" style="margin : 0 20px 0" >
            <uc:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
        </div>
        <div class="color_pallate">
            <div class="color_pallate_head">
            Measurement Selection
            </div>
            <div class="color_pallate_middle">
                <div class="color_bar">
                <asp:ListView ID="grdMeasurement" runat="server" ItemPlaceholderID="phItemPlaceholder"
                        OnItemCommand="grdMeasurement_ItemCommand"  >
                        <LayoutTemplate>
                            <table cellspacing="0" cellpadding="0" border="0" class="product_list_table" style="margin:0" >
                                <thead>
                                  <tr>
                                        <th style="font-size:13px;" >
                                            Measurement Name
                                        </th>
                                        <th style="font-size:13px;">
                                            Upload Date
                                        </th>
                                        <th style="font-size:13px;">
                                            Export
                                        </th>
                                        <th style="font-size:13px;">
                                            Delete
                                        </th>
                                  </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="phItemPlaceholder" runat="server"></asp:PlaceHolder>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                        <EmptyDataTemplate>
                            <div class="no_data_found">No records available</div>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr class="purpal">
                                <td align="left">
                                    <asp:Label ID="lblMeasurementName" runat="server" Text='<%#Eval("MeasurementName") %>'></asp:Label>
                                </td>
                                <td align="center" width="30%">
                                    <asp:Label ID="lblMeasurementDate" runat="server" Text='<%# string.Format("{0:d}",Eval("MeasurementDate")) %>'></asp:Label>
                                </td>
                                <td align="center" width="8%">
                                    <div>
                                        <div style="float:left;">
                                    <asp:ImageButton 
                                        ID="imgbtnExportTxt" 
                                        runat="server" 
                                        ToolTip="Export Txt"
                                        AlternateText="Export Txt" 
                                        ImageUrl="../Images/txt_icon.png" 
                                        CommandName="ExportTxt"
                                        CommandArgument='<%# Eval("MeasurementID") %>' OnClientClick="return true;" />
                                        </div>
                                        <div style="float:right;">
                                        </div>
                                    <asp:ImageButton 
                                        ID="imgbtnExportCvs" 
                                        runat="server" 
                                        ToolTip="Export Cvs"
                                        AlternateText="Export Cvs" 
                                        ImageUrl="../Images/cvs_icon.png" 
                                        CommandName="ExportCvs"
                                        CommandArgument='<%# Eval("MeasurementID") %>' OnClientClick="return true;" />
                                    </div>
                                </td>
                                <td align="center" width="5%">
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete Measurement"
                                        AlternateText="Delete" 
                                        ImageUrl="../Images/delete_icon.png" 
                                        CommandName="DeleteMeasurement"
                                        CommandArgument='<%# Eval("MeasurementID") %>' 
                                        OnClientClick="return ConfirmMeasurement()" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                
                   <%-- <div id="Canvas" style="position: relative; height: 130px; width: 630px; overflow: visible;">
                       
                    </div>--%>
                </div>
            </div>
            <div class="color_pallate_bottom">
                &nbsp;</div>
        </div>
        <div class="color_pallate">
            <div class="color_pallate_head">Chart for Analyze
            </div>
            <div class="color_pallate_middle">
                <div class="color_bar">
                    <div id="Div1" style="position: relative; height: 460px; width: 630px; overflow: visible;">
                        
                    </div>
                </div>
            </div>
            <div class="color_pallate_bottom">
                &nbsp;</div>
        </div>
    </div>
</div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
