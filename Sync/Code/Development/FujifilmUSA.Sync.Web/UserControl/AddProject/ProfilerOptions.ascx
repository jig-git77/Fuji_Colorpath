<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfilerOptions.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ProfilerOptions" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<style type="text/css">
    .style1
    {
        width: 150px;
        text-align: left;
    }
    
    .style4
    {
        width: 80px;
    }
    
    .checkboxFont
    {
    }
    
    .checkboxFont Label
    {
        font-size: 12px;
    }
</style>


<script language="javascript" type="text/javascript">
    jQuery(document).ready(function () {
        $("div[id*='accordion']").click
        (
                function () {
                    var $panel = $("div[id*='panelAdvancedOptions']");
                    var $img = $("div[id*='imgHeader']");
                    $panel.slideToggle(500);
                    if ($img.hasClass('expanded')) {
                        $img.addClass('collapsed').removeClass('expanded');
                    }
                    else {
                        $img.addClass('expanded').removeClass('collapsed');
                    }
                    return false;
                }
        );
        //        var $panel = $("div[id*='panelAdvancedOptions']");
        //        $panel.slideToggle(0);
        $("div[id*='accordion']").click();
    });
</script>
<div class="head">
    <h1 id="hTitle" runat="server">
        New Print Condition - Profiler Options
    </h1>
    <asp:LinkButton ID="lnkbtnProfilerOptions" runat="server" CssClass="button" OnClick="lnkbtnProfilerOptions_Click">
        <span>
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/new_project_icon.png" />
            Measurement Setup</span></asp:LinkButton>
</div>
<div class="formDeviceNoLine" style="display: none;">
    <div class="clear">
    </div>
</div>
<div class="listDevice" style="margin-left: 10px;">
    <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="false" />
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
                                    <div class="text" style="float: left;">
                                        <span>Options Presets</span>
                                    </div>
                                    <div style="float: right">
                                        <%--  <asp:LinkButton ID="lbtnMeasurementSetup" runat="server" CssClass="gray_bt" OnClick="lbtnMeasurementSetup_Click">
                <span>Measurement Setup</span>
                                    </asp:LinkButton>--%>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="panelAddProjectDescription" Style="padding: 5px" runat="server" BorderStyle="None">
                                <table>
                                    <tr>
                                        <td style="width: 100px">
                                            <span>Selected Presets:</span>
                                        </td>
                                        <td style="width: 450px">
                                            <asp:DropDownList ID="cmbSelectedPresets" runat="server" CssClass="select"
                                                DataValueField="Key" DataTextField="Value" AutoPostBack="true" 
                                                OnSelectedIndexChanged="cmbSelectedPresets_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" colspan="2">
                                            <span>Preset Description:</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtProfileDescription" runat="server" Width="120%" Height="100px"
                                                TextMode="MultiLine" Enabled="false" ReadOnly="true" 
                                                Text="This preset is best used for analog devices ……"></asp:TextBox>
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
                            <div id="accordion" class="header collapsibleContainerTitle">
                                <div class="content">
                                    <div class="text">
                                        <div style="float: left">
                                        </div>
                                        <div style="float: left;">
                                            <div id="imgHeader" class="expanded">
                                                &nbsp;</div>
                                            <label class="collapsibleContainerTitle">
                                                Advanced Options</label>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="panelAdvancedOptions" Style="padding: 1px" runat="server" BorderStyle="None"
                                Width="100%" Enabled="false">    

                                <table cellpadding="0" cellspacing="10" border="0">
                                    <tr>
                                        <td valign="top" style="width: 320px;padding-right:0px;">
                                            <div style="padding-bottom:10px">
                                                <div class="section_small_top">
                                                    &nbsp;</div>
                                                <div class="section_small_middle" style="height: 70px">
                                                    <div class="combopart">
                                                        <div class="header">
                                                            <div class="content">
                                                                <div class="text" style="float: left">
                                                                    <label>
                                                                        Rendering</label>
                                                                </div>
                                                                <div style="float: right">
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="panelRendering" style="padding-left: 10px">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <span>Rendering:&nbsp; </span>
                                                                    &nbsp;</td>
                                                                    <td>
                                                                        <asp:DropDownList runat="server" ID="cmbRendering">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="section_small_bottom">
                                                    &nbsp;</div>
                                            </div>
                                            <div style="display: block;">
                                                <div class="section_small_top">
                                                    &nbsp;</div>
                                                <div class="section_small_middle" style="height: 306px">
                                                    <div class="combopart">
                                                        <div class="header">
                                                            <div class="content">
                                                                <div class="text" style="float: left">
                                                                    <label>
                                                                        Black - Generation</label>
                                                                </div>
                                                                <div style="float: right">
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="panelBlackGeneration" style="padding-left: 10px">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td class="style4">
                                                                        <span>GCR:</span>
                                                                    </td>
                                                                    <td style="width: 160px;">
                                                                        <telerik:RadSlider runat="server" ID="rdsMode" MinimumValue="-100" MaximumValue="10"
                                                                            Width="160px">
                                                                        </telerik:RadSlider>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtMode" Width="30px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style4">
                                                                        <span>Black-Start:</span>
                                                                    </td>
                                                                    <td style="width: 160px;">
                                                                        <telerik:RadSlider runat="server" ID="rdsBlackStart" MinimumValue="0" MaximumValue="100"
                                                                            Width="160px">
                                                                        </telerik:RadSlider>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtBlackStart" Width="30px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style4">
                                                                        <span>Black-Width:</span>
                                                                    </td>
                                                                    <td style="width: 160px;">
                                                                        <telerik:RadSlider runat="server" ID="rdsBlackWidth" MinimumValue="0" MaximumValue="100"
                                                                            Width="160px">
                                                                        </telerik:RadSlider>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtBlackWidth" Width="30px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style4">
                                                                        <span>Max-Ink:</span>
                                                                    </td>
                                                                    <td style="width: 160px;">
                                                                        <asp:TextBox runat="server" ID="txtMaxInk" Width="70px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style4">
                                                                        <span>Custom:</span>
                                                                    </td>
                                                                    <td style="width: 160px;">
                                                                        <asp:TextBox runat="server" ID="txtBlackPointCustom" Width="150px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="section_small_bottom">
                                                    &nbsp;</div>
                                            </div>
                                        </td>
                                        <td valign="top" style="width: 320px;" >
                                            <div style="display: block;">
                                                <div class="section_small_top">
                                                    &nbsp;</div>
                                                <div class="section_small_middle" style="height: 400px;">
                                                    <div class="combopart">
                                                        <div class="header">
                                                            <div class="content">
                                                                <div class="text" style="float: left">
                                                                    <label>
                                                                        Exceptions</label>
                                                                </div>
                                                                <div style="float: right">
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="panelExceptions" style="padding-left: 10px">
                                                            <table width="100%" class="checkboxFont" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="style1">                                                                     
                                                                            <asp:CheckBox ID="chkTriplex" runat="server" Text="  Triplex" />                                                                        
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ID="chkDuplex" Text="  Duplex" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style1">
                                                                        <asp:CheckBox runat="server" ID="chkGray" Text="  Gray" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ID="chk100Black" Text="  100% Black" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style1">
                                                                        <asp:CheckBox runat="server" ID="chk100CMY" Text="  100% C,M,Y" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ID="chk100RGB" Text="  100% R,G,B" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style1">
                                                                        <asp:CheckBox runat="server" ID="chkMaxCMY" Text="  Max. C,M,Y" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ID="chkMaxRGB" Text="  Max. R,G,B" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style1" colspan="2" >
                                                                        <asp:CheckBox ID="chkPrimaries" runat="server" Text="  Primaries" />
                                                                    </td>                                                                  
                                                                </tr>
                                                                <tr>
                                                                    <td class="style1" colspan="2">                                                                       
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                                                       
                                                                            <asp:CheckBox ID="chkPC" runat="server" Text="  C" />
                                                                            <asp:CheckBox ID="chkPM" runat="server" Text="  M" />
                                                                            <asp:CheckBox ID="chkPY" runat="server" Text="  Y" />                                                                     
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style1" colspan="2">
                                                                        <asp:CheckBox ID="chkSecondaries" runat="server" Text="  Secondaries" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style1" colspan="2">   
                                                                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                                                      
                                                                            <asp:CheckBox ID="chkSMY" runat="server" Text="  MY" />
                                                                            <asp:CheckBox ID="chkSCY" runat="server" Text="  CY" />
                                                                            <asp:CheckBox ID="chkSCM" runat="server" Text="  CM" />                                                                       
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style1">
                                                                        <asp:CheckBox runat="server" ID="chkBlackOverPrint" Text="  Black Overprint" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ID="chkBorderClipping" Text="  Border Clipping" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style1">
                                                                        <asp:CheckBox runat="server" ID="chkWhite" Text="  White" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox runat="server" ID="chk400Black" Text="  400% Black" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" valign="top">
                                                                        <table cellpadding="0" cellspacing="0" width="98%">
                                                                            <tr>
                                                                                <td colspan="3" valign="top">
                                                                                    <%--<img src="../../Images/section_line_bg.png" width="300px" height="1px" />--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Range:&nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    <telerik:RadSlider runat="server" ID="rdsRange" MinimumValue="0" MaximumValue="100">
                                                                                    </telerik:RadSlider>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox runat="server" ID="txtRange" Width="30px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="section_small_bottom">
                                                    &nbsp;</div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="tab_bottom">
                        &nbsp;</div>
                </div>
            </td>
        </tr>
    </table>
</div>
