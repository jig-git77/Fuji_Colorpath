<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MeasurementSetup.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.MeasurementSetup" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>
<div class="head">
    <h1 id="hTitle" runat="server"><asp:Localize runat="server" 
            Text="New Print Condition - Measurement Setup" ID="locNewPrintCondition" 
            meta:resourcekey="locNewPrintConditionResource1"></asp:Localize>
    </h1>
    <asp:LinkButton ID="lnkbtnMeasurementSetup" runat="server" ValidationGroup="vgMeasurementSetup"
        CssClass="button" OnClick="lnkbtnMeasurementSetup_Click">
        <span>
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Search.png" 
        meta:resourcekey="Image1Resource1" />
            
<asp:Localize runat="server" Text="Review Selections" ID="locReviewSelection" 
        meta:resourcekey="locReviewSelectionResource1"></asp:Localize>
</span></asp:LinkButton>
    <asp:LinkButton ID="lbtnMeasure" runat="server" CssClass="button" 
        OnClick="lbtnMeasure_Click">
        <span>
            <asp:Image ID="imgAdd" runat="server" ImageUrl="~/Images/analyze.png" 
        meta:resourcekey="imgAddResource1" />
            
<asp:Localize runat="server" ID="locMeasure" Text="Measure" 
        meta:resourcekey="locMeasureResource1"></asp:Localize>
</span>
    </asp:LinkButton>
    <asp:LinkButton ID="lnkbtnProfilerOptions" runat="server" CssClass="buttonLeft" 
        OnClick="lnkbtnProfilerOptions_Click">
        <span>
            <asp:Image ID="Image2" runat="server" 
        ImageUrl="~/Images/new_project_icon.png" meta:resourcekey="Image2Resource1" />
            
<asp:Localize runat="server" Text="Profiler Options" ID="locProfilerOptions" 
        meta:resourcekey="locProfilerOptionsResource1"></asp:Localize>
</span></asp:LinkButton>
</div>
<div class="formDeviceNoLine" style="display: none;">
    <div class="clear">
    </div>
</div>
<div class="listDevice" style="margin-left: 10px;">
    <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
    <table cellpadding="0" cellspacing="10" border="0" width="97%">
        <tr id="trListHeader" runat="server">
            <td>
                <div style="display: block;" class="contantbox tabcontant">
                    <div class="section_top">
                        &nbsp;</div>
                    <div class="section_middle">
                        <div class="combopart">
                            <div class="header">
                                <div class="content">
                                    <div class="text" style="float: left">
                                        <asp:Localize runat="server" Text="Spectrophotometer Options" 
                                            ID="locSpectrophotometer" meta:resourcekey="locSpectrophotometerResource1"></asp:Localize>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="panelSpectrophotometerOptions" Style="padding: 5px" runat="server"
                                BorderStyle="None" 
                                meta:resourcekey="panelSpectrophotometerOptionsResource1">
                                <table>
                                    <tr>
                                        <td style="width: 160px">
                                            <span><asp:Localize Text="Measurement Device:" runat="server" 
                                                ID="locMeasurementDevice" meta:resourcekey="locMeasurementDeviceResource1"></asp:Localize><span style="color: Red;">&nbsp*</span> </span>
                                        </td>
                                        <td>
                                            <div style="float: left;">
                                                <asp:DropDownList ID="cmbMeasurementDevice" AutoPostBack="True" runat="server" Width="200px"
                                                    OnSelectedIndexChanged="cmbMeasurementDevice_SelectedIndexChanged" 
                                                    CssClass="select" meta:resourcekey="cmbMeasurementDeviceResource1">
                                                </asp:DropDownList>
                                            </div>
                                            <div style="float: left">
                                                <asp:RequiredFieldValidator ID="rfvMeasurementDevice" runat="server" Display="Dynamic"
                                                    ControlToValidate="cmbMeasurementDevice" ForeColor="Red" 
                                                    ValidationGroup="vgMeasurementSetup"><ucValidationContainer:ValidationContainer ID="rfvcMeasurementDeviceContainer" runat="server"
                                                        ValidationMsg="Please select measurement device." Size="200" />
</asp:RequiredFieldValidator>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span><asp:Localize Text="Illuminant:" runat="server" ID="locIlluminant" 
                                                meta:resourcekey="locIlluminantResource1"></asp:Localize></span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbIlluminant" runat="server" Width="200px" 
                                                CssClass="select" meta:resourcekey="cmbIlluminantResource1">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span><asp:Localize runat="server" Text="Observer:" ID="locObeserver" 
                                                meta:resourcekey="locObeserverResource1"></asp:Localize></span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbObserver" runat="server" Width="200px" 
                                                CssClass="select" meta:resourcekey="cmbObserverResource1">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr id="trUVSettings" runat="server">
                                        <td runat="server">
                                            <span><asp:Localize runat="server" Text="UV Settings:" ID="locUVSettings"></asp:Localize></span>
                                        </td>
                                        <td runat="server">
                                            <asp:DropDownList ID="cmbUVSettings" runat="server" Width="200px" CssClass="select">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
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
                                    <div class="text" style="float: left;">
                                        <label><asp:Localize runat="server" Text="Alignment Chart" 
                                            ID="locAlignmentChart" meta:resourcekey="locAlignmentChartResource1"></asp:Localize></label>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="panelAlignmentChart" Style="margin-left: 5px; margin-right: 5px;"
                                runat="server" BorderStyle="None" 
                                meta:resourcekey="panelAlignmentChartResource1">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <span><asp:Localize Text="Alignment Chart:" runat="server" 
                                                ID="txtAlignmentChart" meta:resourcekey="txtAlignmentChartResource1"></asp:Localize><span style="color: Red;">&nbsp*</span> </span>
                                        </td>
                                        <td>
                                            <div style="float: left;">
                                                <asp:DropDownList ID="cmbAlignmentChart" runat="server" Width="200px" CssClass="select"
                                                    AutoPostBack="True" 
                                                    OnSelectedIndexChanged="cmbAlignmentChart_SelectedIndexChanged" 
                                                    meta:resourcekey="cmbAlignmentChartResource1">
                                                </asp:DropDownList>
                                            </div>
                                            <div style="float: left">
                                                <asp:RequiredFieldValidator ID="rfvAlignmentChart" runat="server" Display="Dynamic"
                                                    ControlToValidate="cmbAlignmentChart" ForeColor="Red" InitialValue="0" 
                                                    ValidationGroup="vgMeasurementSetup"><ucValidationContainer:ValidationContainer ID="rfvcAlignmentChartContainer" runat="server"
                                                        ValidationMsg="Please select alignment chart." Size="200" />
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Image ID="imgChart" runat="server" meta:resourcekey="imgChartResource1" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="section_bottom">
                        &nbsp;</div>
                </div>
            </td>
        </tr>
    </table>
</div>
