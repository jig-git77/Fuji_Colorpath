<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SpotLibraryExport.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.PopupControl.SpotLibraryExport" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>
<link href="Content/kendo/2018.1.221/kendo.common.min.css" rel="stylesheet" />
<link href="Content/kendo/2018.1.221/kendo.default.min.css" rel="stylesheet" />
<script src="Scripts/kendo/2018.1.221/kendo.all.min.js" type="text/javascript"></script>
<style>
    .tblExport td {
        padding: 3px;
    }   
</style>
<script type="text/javascript">

    
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

    function test() {     
        var SERVICE_BASE_URL = $('#hdnBaseServiceUrl').val();
        if (validate()) {
            var apiParameter = {
                OptimizationRunId: parseInt($('#hdnOptiomizatoinRunID').val()),
                LibraryType: parseInt($('#cmbCMYKLibrary').val()),
                FileName: $('#txtFileName').val(),
                Server: $('#txtServer').val(),
                Model: $('#txtModel').val(),
                Media: $('#txtMedia').val(),
                Resolution: $('#txtResolution').val(),
                Mode: $('#txtMode').val(),
                Quality: $('#txtQuality').val(),
                InkPoolID: $('#txtInkPoolIOD').val(),
                Comment: $('#txtComment').val()
            }            
            kendo.ui.progress($('#divExport'), true);
            $.ajax({
                type: "POST",
                url: SERVICE_BASE_URL + 'api/Report/ExportSpotCMYKLibraray',                
                data: apiParameter,
                dataType: "json",
                success: function (response) {                    
                    kendo.ui.progress($('#divExport'), false);
                    if (response.data != null) {
                        console.log(response.data);
                        var fileName = '~' + response.data;
                        window.location.href = SERVICE_BASE_URL + '/api/Analyze/DownloadFile?filePath=' + fileName;
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    kendo.ui.progress($('#divExport'), false);
                    console.log(XMLHttpRequest.responseText);
                }
            });
        }
        return false;
    }

    function validate() {
        var isSuccess = true;               
        if ($("#cmbCMYKLibrary").val() == "") {
            isSuccess = false;
            $("#cmbCMYKLibrary").parent("div").next('div').find('span').show();
        }
        else {
            var type = $("#cmbCMYKLibrary").val();

            if ($("#txtFileName").val() == "") {
                isSuccess = false;
                $("#txtFileName").parent("div").next('div').find('span').show();
            }

            if (type == "1" || type == "2") {
                if ($("#txtServer").val() == "") {
                    isSuccess = false;
                    $("#txtServer").parent("div").next('div').find('span').show();
                }
                if ($("#txtMedia").val() == "") {
                    isSuccess = false;
                    $("#txtMedia").parent("div").next('div').find('span').show();
                }
                if ($("#txtModel").val() == "") {
                    isSuccess = false;
                    $("#txtModel").parent("div").next('div').find('span').show();
                }
                if ($("#txtResolution").val() == "") {
                    isSuccess = false;
                    $("#txtResolution").parent("div").next('div').find('span').show();
                }
                if (type = "2") {
                    if ($("#txtMode").val() == "") {
                        isSuccess = false;
                        $("#txtMode").parent("div").next('div').find('span').show();
                    }
                    if ($("#txtQuality").val() == "") {
                        isSuccess = false;
                        $("#txtQuality").parent("div").next('div').find('span').show();
                    }
                }
            }
            if (type == "4") {
                if ($("#txtInkPoolIOD").val() == "") {
                    isSuccess = false;
                    $("#txtInkPoolIOD").parent("div").next('div').find('span').show();
                }
                if ($("#txtComment").val() == "") {
                    isSuccess = false;
                    $("#txtComment").parent("div").next('div').find('span').show();
                }
            }
        }        
        return isSuccess;
    }
</script>
<%--<asp:UpdatePanel ID="upCreateDLP" runat="server">
    <ContentTemplate>--%>
<div id="divExport" style="padding-left: 5px;">
    <table cellpadding="0" cellspacing="10" width="100%">
        <%-- <tr>
            <td>
                <ucstatusbar:statusbar id="fujiStatusbar" runat="server" visible="False" />
            </td>
        </tr>--%>
        <tr>
            <td>
                <div style="display: block;" class="contantbox tabcontant">
                    <div class="section_top">
                        &nbsp;
                    </div>
                    <div class="section_middle">
                        <asp:HiddenField runat="server" ID="hdnBaseServiceUrl" ClientIDMode="Static" />
                        <asp:HiddenField runat="server" ID="hdnOptiomizatoinRunID" ClientIDMode="Static" />
                        <div class="combopart">
                            <div>
                            </div>
                            <div class="header">
                                <div class="content">
                                    <div style="float: left;" class="text">
                                        <asp:Localize ID="locHeader"
                                            runat="server" Text="Export CMYK Library" meta:resourcekey="locHeaderResource1"></asp:Localize>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 5px;">
                                <table class="tblExport">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <div>
                                                    <span>
                                                        <asp:Localize runat="server" Text="Library Type: " ID="Localize1"
                                                            meta:resourcekey="locTypeResource1"></asp:Localize><span style="color: Red;">*</span></span>
                                                </div>
                                            </td>
                                            <td>
                                                <div style="float: left">
                                                    <asp:DropDownList ID="cmbCMYKLibrary" ClientIDMode="Static" runat="server" Height="25px" Width="204px" OnSelectedIndexChanged="cmbCMYKLibrary_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Text="--Select--" meta:resourcekey="SelectTextResource1" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="Caldera" meta:resourcekey="CalderaResource1" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Caldera2" meta:resourcekey="Caldera2Resource1" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="ColorGate" meta:resourcekey="ColorgateResource1" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="XMF" meta:resourcekey="XMFResource1" Value="4"></asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div style="float: left">
                                                    <asp:RequiredFieldValidator ID="rfvCMYKLibrary" runat="server" Display="Dynamic" ControlToValidate="cmbCMYKLibrary"
                                                        ForeColor="Red" ValidationGroup="vgExportLibrary" InitialValue="">
                                                        <ucValidationContainer:ValidationContainer ID="rfvcCMYKLibrary" runat="server"
                                                            ValidationMsg="Please select library." Size="200" ResourceValidationMsg="LibraryRequiredResource" />
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                            <td>
                                                <div>
                                                    <span>
                                                        <asp:Localize runat="server" Text="File Name:" ID="locFileName"
                                                            meta:resourcekey="locFileNameResource1"></asp:Localize><span style="color: Red;">*</span></span>
                                                </div>
                                            </td>
                                            <td>
                                                <div style="float: left;">
                                                    <asp:TextBox ID="txtFileName" runat="server" Width="200px" Height="19px" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                                <div style="float: left">
                                                    <asp:RequiredFieldValidator ID="rfvtxtFileName" runat="server" Display="Dynamic" ControlToValidate="txtFileName"
                                                        ForeColor="Red" ValidationGroup="vgExportLibrary" InitialValue="">
                                                        <ucValidationContainer:ValidationContainer ID="vctxtFileName" runat="server"
                                                            ValidationMsg="Please enter file name." Size="200" ResourceValidationMsg="FileNameRequiredResource" />
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trCalderaRow1" visible="false">
                                            <td>
                                                <span>
                                                    <asp:Localize runat="server" Text="Server:" ID="locServer"
                                                        meta:resourcekey="locServerResource1"></asp:Localize><span style="color: Red;">*</span></span>
                                            </td>
                                            <td>
                                                <div style="float: left;">
                                                    <asp:TextBox ID="txtServer" runat="server" Width="200px" Height="19px" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                                <div style="float: left">
                                                    <asp:RequiredFieldValidator ID="rfvtxtServer" runat="server" Display="Dynamic" ControlToValidate="txtServer"
                                                        ForeColor="Red" ValidationGroup="vgExportLibrary" InitialValue="">
                                                        <ucValidationContainer:ValidationContainer ID="vctxtServer" runat="server"
                                                            ValidationMsg="Please server name." Size="200" ResourceValidationMsg="ServerRequiredResource" />
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </td>
                                            <td>
                                                <span>
                                                    <asp:Localize runat="server" Text="Model:" ID="locModel"
                                                        meta:resourcekey="locModelResource1"></asp:Localize><span style="color: Red;">*</span></span>
                                            </td>
                                            <td>
                                                <div style="float: left;">
                                                    <asp:TextBox ID="txtModel" runat="server" Width="200px" Height="19px" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                                <div style="float: left">
                                                    <asp:RequiredFieldValidator ID="rfvtxtModel" runat="server" Display="Dynamic" ControlToValidate="txtModel"
                                                        ForeColor="Red" ValidationGroup="vgExportLibrary" InitialValue="">
                                                        <ucValidationContainer:ValidationContainer ID="vctxtModel" runat="server"
                                                            ValidationMsg="Please model name." Size="200" ResourceValidationMsg="ModelRequiredResource" />
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trCalderaRow2" visible="false">
                                            <td>
                                                <span>
                                                    <asp:Localize runat="server" Text="Media:" ID="locMedia"
                                                        meta:resourcekey="locMediaResource1"></asp:Localize><span style="color: Red;">*</span></span>
                                            </td>
                                            <td>
                                                <div style="float: left;">
                                                    <asp:TextBox ID="txtMedia" runat="server" Width="200px" Height="19px" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                                <div style="float: left">
                                                    <asp:RequiredFieldValidator ID="rfvtxtMedia" runat="server" Display="Dynamic" ControlToValidate="txtMedia"
                                                        ForeColor="Red" ValidationGroup="vgExportLibrary" InitialValue="">
                                                        <ucValidationContainer:ValidationContainer ID="vctxtMedia" runat="server"
                                                            ValidationMsg="Please media name." Size="200" ResourceValidationMsg="MediaRequiredResource" />
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </td>
                                            <td>
                                                <span>
                                                    <asp:Localize runat="server" Text="Resolution:" ID="locResolution"
                                                        meta:resourcekey="locResolutionResource1"></asp:Localize><span style="color: Red;">*</span></span>
                                            </td>
                                            <td>
                                                <div style="float: left;">
                                                    <asp:TextBox ID="txtResolution" runat="server" Width="200px" Height="19px" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                                                </div>

                                                <div style="float: left">
                                                    <asp:RequiredFieldValidator ID="rfvtxtResolution" runat="server" Display="Dynamic" ControlToValidate="txtResolution"
                                                        ForeColor="Red" ValidationGroup="vgExportLibrary" InitialValue="">
                                                        <ucValidationContainer:ValidationContainer ID="vctxtResolution" runat="server"
                                                            ValidationMsg="Please enter resolution." Size="200" ResourceValidationMsg="ResolutionRequiredResource" />
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trCaldera2Row1" visible="false">
                                            <td>
                                                <span>
                                                    <asp:Localize runat="server" Text="Mode:" ID="locMode"
                                                        meta:resourcekey="locModeResource1"></asp:Localize><span style="color: Red;">*</span></span>
                                            </td>
                                            <td>
                                                <div style="float: left;">
                                                    <asp:TextBox ID="txtMode" runat="server" Width="200px" Height="19px"
                                                        MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                                <div style="float: left">
                                                    <asp:RequiredFieldValidator ID="rfvMode" runat="server" Display="Dynamic" ControlToValidate="txtMode"
                                                        ForeColor="Red" ValidationGroup="vgExportLibrary" InitialValue="">
                                                        <ucValidationContainer:ValidationContainer ID="rfvModeContainer" runat="server"
                                                            ValidationMsg="Please enter mode." Size="200" ResourceValidationMsg="ModeRequiredResource" />
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </td>
                                            <td>
                                                <span>
                                                    <asp:Localize runat="server" Text="Quality:" ID="locQuality"
                                                        meta:resourcekey="locQualityResource1"></asp:Localize><span style="color: Red;">*</span></span>
                                            </td>
                                            <td>
                                                <div style="float: left;">
                                                    <asp:TextBox ID="txtQuality" runat="server" Width="200px" Height="19px"
                                                        MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                                                </div>

                                                <div style="float: left">
                                                    <asp:RequiredFieldValidator ID="rfvQuality" runat="server" Display="Dynamic" ControlToValidate="txtQuality"
                                                        ForeColor="Red" ValidationGroup="vgExportLibrary" InitialValue="">
                                                        <ucValidationContainer:ValidationContainer ID="rfvQualityContainer" runat="server"
                                                            ValidationMsg="Please enter quality." Size="200" ResourceValidationMsg="QualityRequiredResource" />
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trXMF" visible="false">
                                            <td>
                                                <span>
                                                    <asp:Localize runat="server" Text="Ink Pool ID: " ID="locInkPoolID"
                                                        meta:resourcekey="locInkPoolIDResource1"></asp:Localize><span style="color: Red;">*</span></span>
                                            </td>
                                            <td>
                                                <div style="float: left;">
                                                    <asp:TextBox ID="txtInkPoolIOD" runat="server" Width="200px" Height="19px"
                                                        MaxLength="50" Text="1001" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                                <div style="float: left">
                                                    <asp:RequiredFieldValidator ID="rfvInkPoolID" runat="server" Display="Dynamic" ControlToValidate="txtInkPoolIOD"
                                                        ForeColor="Red" ValidationGroup="vgExportLibrary" InitialValue="">
                                                        <ucValidationContainer:ValidationContainer ID="rfvInkPoolIDContainer" runat="server"
                                                            ValidationMsg="Please enter Ink Pool ID." Size="200" ResourceValidationMsg="InkPoolIDRequiredResource" />
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </td>
                                            <td>
                                                <span>
                                                    <asp:Localize runat="server" Text="Comment: " ID="locComment"
                                                        meta:resourcekey="CommentResource1"></asp:Localize><span style="color: Red;">*</span></span>
                                            </td>
                                            <td>
                                                <div style="float: left;">
                                                    <asp:TextBox ID="txtComment" runat="server" Width="200px" Height="19px"
                                                        MaxLength="50" Text="Ink Pool from Colorpath Sync" ClientIDMode="Static"></asp:TextBox>
                                                </div>

                                                <div style="float: left">
                                                    <asp:RequiredFieldValidator ID="rfvComment" runat="server" Display="Dynamic" ControlToValidate="txtComment"
                                                        ForeColor="Red" ValidationGroup="vgExportLibrary" InitialValue="">
                                                        <ucValidationContainer:ValidationContainer ID="rfvCommentContainer" runat="server"
                                                            ValidationMsg="Please enter comment." Size="200" ResourceValidationMsg="CommentRequiredResource" />
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="clear">
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
                    <asp:LinkButton runat="server" ID="lbtnExportCMYK" CssClass="gray_bt" OnClientClick="return test()">
                        <span>
                            <asp:Localize ID="locExport" runat="server" Text="Export" meta:resourcekey="locGenetareReportResource1"></asp:Localize></span>
                    </asp:LinkButton>
                </div>
                <div class="clear">
                </div>
            </td>
        </tr>
    </table>

</div>
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>
