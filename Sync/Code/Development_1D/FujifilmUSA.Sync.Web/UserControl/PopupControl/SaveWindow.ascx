<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="SaveWindow.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.SaveWindow" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>
<script language="javascript" type="text/javascript">
    function Close() {
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
<div class="listDevice" style="margin-left: 10px">
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
                                    <div style="float: left;" class="text">
                                        <label><asp:Localize runat="server" Text="1. Name Print Condition" 
                                            ID="locNamePrintCondition" meta:resourcekey="locNamePrintConditionResource1"></asp:Localize></label>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 5px;" id="ContentPlaceHolder1_ucAddProject_ucMeasurementSetup_panelSpectrophotometerOptions">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="width: 140px;">
                                                <span><asp:Localize runat="server" Text="Print Condition Name:" 
                                                    ID="locPrintConditionName" meta:resourcekey="locPrintConditionNameResource1"></asp:Localize></span>
                                            </td>
                                            <td>
                                                <div style="float: left;">
                                                    <asp:TextBox ID="txtProjectName" runat="server" Width="200px" Height="19px" 
                                                        MaxLength="30" meta:resourcekey="txtProjectNameResource1"></asp:TextBox>
                                                </div>
                                                <div style="float: left">
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"  Display="Dynamic"
                                                         ControlToValidate="txtProjectName" ValidationGroup="vgSaveWindows" ValidationExpression='^(?![.\-])(?:[^\\\/:*?"<>|#])*[^.\-\\\/:*?"<>|#]$'
                                                         ForeColor="Red" ErrorMessage="RegularExpressionValidator">
                                                             <ucValidationContainer:ValidationContainer ID="rfvProjectContainer1" runat="server"
                                                            ValidationMsg="Invalid Project name.<br />Special characters (/, \, :, *, ?, &quot;, <, >, |, #) and '.' or '-' at first or last positions are not permitted." Size="200" />
                                                         </asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator ID="rfvProjectName" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtProjectName" ForeColor="Red" 
                                                        ValidationGroup="vgSaveWindows">
                                                        <ucValidationContainer:ValidationContainer ID="rfvProjectContainer" runat="server"
                                                            ValidationMsg="Please provide project name." Size="200" />
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
                        &nbsp;</div>
                </div>
            </td>
        </tr>
        <tr id="Tr_DownloadChart" runat="server">
            <td>
                <div style="display: block;" class="contantbox tabcontant">
                    <div class="section_top">
                        &nbsp;</div>
                    <div class="section_middle">
                        <div class="combopart">
                            <div class="header">
                                <div class="content">
                                    <div style="float: left;" class="text">
                                        <label><asp:Localize runat="server" Text="2. Download Chart" 
                                            ID="locDownloadChart" meta:resourcekey="locDownloadChartResource1"></asp:Localize></label>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 5px;" id="Div1">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <span><asp:Localize runat="server" 
                                                    Text="Download chart and print chart through a workflow that has no colormanagement applied :" 
                                                    meta:resourcekey="LocalizeResource1"></asp:Localize></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="lbtnDownloadChart" runat="server" CssClass="gray_bt" 
                                                    OnClick="lbtnDownloadChart_Click">
                                                    <span><asp:Localize runat="server" Text="Download Chart" ID="locDownloadChartButton" 
                                                        meta:resourcekey="locDownloadChartButtonResource1"></asp:Localize>
                                                    </span>
                                                </asp:LinkButton>
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
                        Text="Cancel" meta:resourcekey="locCancelResource1"></asp:Localize> 
 </span>
                    </asp:LinkButton>
                </div>
                <div class="buttonright">
                    <asp:LinkButton ID="lbtnSave" runat="server" ValidationGroup="vgSaveWindows" CssClass="gray_bt"
                        OnClick="lbtnSave_Click">
                    <span> <asp:Localize runat="server" Text="Save" 
                        meta:resourcekey="LocalizeResource2"></asp:Localize> 
 </span>
                    </asp:LinkButton>
                </div>
                <div class="clear">
                </div>
            </td>
        </tr>
    </table>
</div>
