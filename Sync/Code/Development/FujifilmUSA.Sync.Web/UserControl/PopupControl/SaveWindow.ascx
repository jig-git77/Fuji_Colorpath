<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveWindow.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.SaveWindow" %>
<div class="listDevice" style="margin-left: 10px">
    <table cellpadding="0" cellspacing="10" width="100%">
        <tr>
            <td>
                <div style="display: block;" class="contantbox tabcontant">
                    <div class="section_top">
                        &nbsp;</div>
                    <div class="section_middle">
                        <div class="combopart">
                            <div class="header">
                                <div class="content">
                                    <div style="float: left;" class="text">
                                        <label>
                                            1. Name Print Condition</label>
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
                                                <span>Print Condition Name:</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtProjectName" runat="server" Width="200px" Height="19px"></asp:TextBox>
                                                <asp:RequiredFieldValidator 
                                                    ID="rfvProjectName" 
                                                    runat="server" 
                                                    Display="Dynamic" 
                                                    ControlToValidate="txtProjectName"
                                                    ErrorMessage="Please provide project name." 
                                                    ForeColor="Red" 
                                                    ValidationGroup="vgSaveWindows"></asp:RequiredFieldValidator>
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
                <div style="display: block;" class="contantbox tabcontant">
                    <div class="section_top">
                        &nbsp;</div>
                    <div class="section_middle">
                        <div class="combopart">
                            <div class="header">
                                <div class="content">
                                    <div style="float: left;" class="text">
                                        <label>
                                            2. Download Chart</label>
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
                                                <span>Download chart and print chart through a workflow that has no colormanagement
                                                    applied :</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="lbtnTargetColorSpace" runat="server" CssClass="gray_bt">
                                        <span>DawnLoad Chart</span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnFileName" runat="server">
                                        <span>Sync 500</span>
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
                <div class="buttonright">
                    <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript: Close();"
                        CssClass="gray_bt">
                                        <span>
                                            Close </span>
                    </asp:LinkButton>
                </div>
                <div class="buttonright">
                    <asp:LinkButton ID="lbtnContinue" runat="server" ValidationGroup="vgSaveWindows" CssClass="gray_bt">
                <span> Continue </span>
                    </asp:LinkButton>
                </div>
                <div class="clear">
                </div>
            </td>
        </tr>
    </table>
</div>
