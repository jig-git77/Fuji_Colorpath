<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImportMeasurementFile.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.PopupControl.ImportMeasurementFile" %>
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
    

    function CloseImportWindow() {        
        var oWindow = GetMeasurementRadWindow();
        GetMeasurementRadWindow().close();
        //top.location.href = top.location.href.replace("#","");
    
        parent.window.location = top.location.href.replace("#", "");
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
                                    <div style="float: left;" class="text"><asp:Localize ID="locSelectProjectToImport" 
                                            runat="server" 
                                            Text="Select Project to import all its measurements in this project: " 
                                            meta:resourcekey="locSelectProjectToImportResource1"></asp:Localize>
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
                                               <%-- <telerik:RadAsyncUpload runat="server" ID="fucUploadChartPreview"
                                                    MaxFileInputsCount="1" PostbackTriggers="lbtnSave" 
                                                    meta:resourcekey="fucUploadChartPreviewResource1"  >
                                                </telerik:RadAsyncUpload>  --%>
                                                <asp:FileUpload ID="fuMeasurement" runat="server" meta:resourcekey="fuMeasurementResource1" />
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
                        Text="Cancel" meta:resourcekey="locCancelResource1" ></asp:Localize> 
                                
 
                                </span>
                    </asp:LinkButton>
                </div>
                <div class="buttonright">
                    <asp:LinkButton ID="lbtnImport" runat="server" ValidationGroup="vgSaveWindows" 
                        CssClass="gray_bt" onclick="lbtnImport_Click" 
                        OnClientClick="if (!OnClientClick()) {return false;}" 
                        >
                       
                    <span> <asp:Localize ID="locImport" runat="server" Text="Import" meta:resourcekey="locImportResource1" 
                        ></asp:Localize> 
                        
 
                        </span>
                    </asp:LinkButton>
                </div>
                <div class="clear">
                </div>
            </td>
        </tr>
    </table>

</div>