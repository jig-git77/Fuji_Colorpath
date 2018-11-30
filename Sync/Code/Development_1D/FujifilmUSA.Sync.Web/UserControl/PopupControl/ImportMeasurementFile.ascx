<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImportMeasurementFile.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.PopupControl.ImportMeasurementFile" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>
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

    var validFilesTypes = ["json"];

    function ValidateFile() {
        var file = document.getElementById("<%=fuMeasurement.ClientID%>");
        var label = document.getElementById("<%=lblMessage.ClientID%>");
        var path = file.value;
        var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();
        var isValidFile = false;
        for (var i = 0; i < validFilesTypes.length; i++) {
            if (ext == validFilesTypes[i]) {
                isValidFile = true;
                break;
            }
        }

        if (!isValidFile) {
            label.style.color = "red";
            label.innerHTML = "Invalid File. Please upload a File with extension:\n\n" + validFilesTypes.join(", ");
        } else {
            label.innerHTML = '';
        }

        return isValidFile;
    }

</script>

<div style="padding-left: 5px;">


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
                        &nbsp;
                    </div>
                    <div class="section_middle">
                        <div class="combopart">
                            <div>
                            </div>
                            <div class="header">
                                <div class="content">
                                    <div style="float: left;" class="text">
                                        <asp:Localize ID="locSelectProjectToImport"
                                            runat="server"
                                            Text="Select Project file to import all its measurements in this project: "
                                            meta:resourcekey="locSelectProjectToImportResource1"></asp:Localize>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 5px;">
                                <table style="width: 100%">
                                    <tbody>
                                        <tr>
                                            <td colspan="4">
                                                <asp:Label runat="server" ID="lblMessage" Style="color: red;"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">                                                
                                                <asp:Label ID="lblMeasurementName" runat="server" Text="Measurement Name" meta:resourcekey="lblMeasurementNameResource1"></asp:Label>:&nbsp;
                                            </td>
                                            <td>
                                                <div style="float: left">
                                                    <asp:TextBox ID="txtMeasurementName" runat="server" Width="178px" Height="19px" onkeypress="return validateText(event)"  MaxLength="30"></asp:TextBox>
                                                </div>
                                                <div style="float: left">
                                                    <asp:RequiredFieldValidator ID="rfvMeasurementName" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtMeasurementName" ForeColor="Red"
                                                        ValidationGroup="vgImportMeasurement">
                                                        <ucValidationContainer:ValidationContainer ID="rfvMeasurementContainer" runat="server"
                                                            ValidationMsg="Please provide measurement name." Size="200" ResourceValidationMsg="MeasurementNameRequiredResource" />
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                            <td rowspan="2" id="tdNotesCaption" runat="server" visible="false" align="right">
                                                <asp:Label ID="lblNotes" runat="server" Text="Notes" meta:resourcekey="lblNotesResource1"></asp:Label>:&nbsp;
                                            </td>
                                            <td rowspan="2" id="tdNotes" runat="server" visible="false">
                                                <asp:TextBox ID="txtNotes" runat="server" Width="178px" Height="50px" TextMode="MultiLine" Style="resize: none;" MaxLength="1000"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trCustomer" runat="server" visible="false">
                                            <td align="right">
                                                <asp:Label ID="lblCustomer" runat="server" Text="Customer" meta:resourcekey="lblCustomerResource1"></asp:Label>:&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCustomer" runat="server" Width="178px" Height="19px" MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trValidationDetails" runat="server" visible="false">
                                            <td align="right">
                                                <asp:Label ID="lblPageOrSurface" runat="server" Text="Page/Surface" meta:resourcekey="lblPageOrSurfaceResource1"></asp:Label>:&nbsp;
                                            </td>
                                            <td style="height: 27px;">
                                                <asp:TextBox ID="txtPageOrSurface" runat="server" Width="178px" Height="19px" MaxLength="50"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblJDFJobId" runat="server" Text="JDF Job ID" meta:resourcekey="lblJDFJobIdResource1"></asp:Label>:&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtJDFJobId" runat="server" Width="178px" Height="19px" MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>                                        
                                        <tr>
                                            <td align="right">
                                                <asp:Localize runat="server" Text="Select File:" ID="locSelectMeasurementName" meta:resourcekey="SelectMeasurementNameResource1"></asp:Localize>
                                            </td>
                                            <td>
                                                <%-- <telerik:RadAsyncUpload runat="server" ID="fucUploadChartPreview"
                                                    MaxFileInputsCount="1" PostbackTriggers="lbtnSave" 
                                                    meta:resourcekey="fucUploadChartPreviewResource1"  >
                                                </telerik:RadAsyncUpload>  --%>
                                                <div style="float: left">
                                                    <asp:FileUpload ID="fuMeasurement" runat="server" meta:resourcekey="fuMeasurementResource1" />
                                                </div>
                                                <div style="float: left">
                                                    <asp:RequiredFieldValidator ID="rfvMeasurementUpload" runat="server" Display="Dynamic"
                                                        ControlToValidate="fuMeasurement" ForeColor="Red"
                                                        ValidationGroup="vgImportMeasurement">
                                                        <ucValidationContainer:ValidationContainer ID="rfvMeasurementUploadContainer" runat="server"
                                                            ValidationMsg="Please select measurement file." Size="200" ResourceValidationMsg="MeasurementFileRequiredResource" />
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="rfvMeasurementUploadFormat" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.json)$"
                                                        ControlToValidate="fuMeasurement" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="vgImportMeasurement">
                                                        <ucValidationContainer:ValidationContainer ID="rfvMeasurementUploadFormatContainer" runat="server"
                                                            ValidationMsg="Please select a valid json file." Size="200" ResourceValidationMsg="ValidMeasurementFileRequiredResource" />
                                                    </asp:RegularExpressionValidator>
                                                </div>
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
                        &nbsp;
                    </div>
                </div>
            </td>
        </tr>

        <tr>
            <td>
                <div class="buttonright" style="padding-left: 3px">
                    <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript: Close();"
                        CssClass="gray_bt">
                        <span>
                            <asp:Localize runat="server" ID="locCancel"
                                Text="Cancel" meta:resourcekey="locCancelResource1"></asp:Localize>


                        </span>
                    </asp:LinkButton>
                </div>
                <div class="buttonright">
                    <asp:LinkButton ID="lbtnImport" runat="server" ValidationGroup="vgImportMeasurement"
                        CssClass="gray_bt" OnClick="lbtnImport_Click"
                        OnClientClick="if (!OnClientClick()) {return false;}">

                        <span>
                            <asp:Localize ID="locImport" runat="server" Text="Import" meta:resourcekey="locImportResource1"></asp:Localize>


                        </span>
                    </asp:LinkButton>
                </div>
                <div class="clear">
                </div>
            </td>
        </tr>
    </table>

</div>
