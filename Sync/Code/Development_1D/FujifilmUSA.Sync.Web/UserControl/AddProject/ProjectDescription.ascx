<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ProjectDescription.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ProjectDescription" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>
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
    
    textarea {
        resize: none;
    }
    
    .textwidth35
    {
        width: 35px;
    }
    .textwidth30
    {
        width: 30px;
    }
</style>

<script language="javascript" type="text/javascript">
    jQuery(document).ready(function () {
        $("div[id*='accordion']").click
        (
                function () {
                    var $panel = $("div[id*='panelOffsetData']");
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
       
        $("div[id*='accordion']").click();

    });
</script>

<div class="head">
    <h1 id="hTitle" runat="server"><asp:Localize runat="server" 
            Text="New Print Condition - Description" ID="locNewPrintCondition" 
            meta:resourcekey="locNewPrintConditionResource1"></asp:Localize></h1>
    <asp:LinkButton ID="lnkbtnPrintDescription" runat="server" CssClass="button" ValidationGroup="vgDescription"
        OnClick="lnkbtnPrintDescription_Click">
        <span>
            <asp:Image ID="imgAdd" runat="server" 
        ImageUrl="~/Images/new_project_icon.png" meta:resourcekey="imgAddResource1" />
            <asp:Localize runat="server" ID="locTargetColorSpace" Text="Target ColorSpace" 
                meta:resourcekey="locTargetColorSpaceResource1"></asp:Localize>
        </span>
    </asp:LinkButton>

    <asp:LinkButton ID="lnkbtNext" runat="server" CssClass="button" 
        ValidationGroup="vgDescription" onclick="lnkbtnNext_Click" Visible="false">
        <span>
            <asp:Image ID="Image1" runat="server" 
        ImageUrl="~/Images/new_project_icon.png" meta:resourcekey="imgAddResource1" />
            <asp:Localize runat="server" ID="Localize1" Text="Next" 
                meta:resourcekey="locNextResource1"></asp:Localize>
        </span>
    </asp:LinkButton>

</div>
<div class="formDeviceNoLine" style="display: none;">
    <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
    <div class="clear">
    </div>
</div>
<div class="listDevice" style="margin-left: 10px;">
<%--<asp:UpdatePanel runat="server">
        <ContentTemplate>--%>
            <table cellpadding="0" cellspacing="10" border="0" width="97%" id="tbProjectDescription">
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
                                                <span><asp:Localize runat="server" Text="Project Description" 
                                                    ID="locProjectDescription" meta:resourcekey="locProjectDescriptionResource1"></asp:Localize></span>
                                            </div>
                                            <div style="float: right">
                                              
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Panel ID="panelAddProjectDescription"  Style="padding: 5px" runat="server" BorderStyle="None" 
                                       meta:resourcekey="panelAddProjectDescriptionResource1">
                                        <table>
                                            <tr>
                                                <td style="width: 130px">
                                                    <span><asp:Localize runat="server" Text="Press:" ID="locPress" 
                                                        meta:resourcekey="locPressResource1"></asp:Localize></span>
                                                </td>
                                                <td style="width: 400px">
                                                    <asp:TextBox ID="txtPress" runat="server" Width="200px" Height="19px" 
                                                        Enabled="False" meta:resourcekey="txtPressResource1"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span><asp:Localize runat="server" Text="Device Family:" ID="locDeviceFamily" 
                                                        meta:resourcekey="locDeviceFamilyResource1"></asp:Localize></span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDeviceFamily" runat="server" Width="200px" Height="19px" 
                                                        Enabled="False" meta:resourcekey="txtDeviceFamilyResource1"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span><asp:Localize runat="server" ID="locOperationType" Text="Operation:" meta:resourcekey="locOperationTypeResource1"></asp:Localize><span style="color: Red;">*</span></span>
                                                </td>
                                                <td>
                                                    <div style="float: left;">
                                                        <asp:DropDownList runat="server" ID="ddlLicenseType" Width="200px" 
                                                            CssClass="select" 
                                                            onselectedindexchanged="ddlLicenseType_SelectedIndexChanged" 
                                                            AutoPostBack="True"></asp:DropDownList>
                                                    </div>
                                                    <div style="float: left">
                                                        <asp:RequiredFieldValidator ID="rfvLicenseType" runat="server" Display="Dynamic" ControlToValidate="ddlLicenseType"
                                                            ForeColor="Red" ValidationGroup="vgDescription" InitialValue="-1"><ucValidationContainer:ValidationContainer ID="rfvcLicenseType" runat="server"
                                                                ValidationMsg="Please select operation." Size="200" />
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr id="Tr_Media" runat="server">
                                                <td>
                                                    <span><asp:Localize runat="server" Text="Media:" ID="locMedia" 
                                                        meta:resourcekey="locMediaResource1"></asp:Localize><span style="color: Red;">*</span></span>
                                                </td>
                                                <td>
                                                    <div style="float: left;">
                                                        <asp:TextBox ID="txtMedia" runat="server" Width="200px" Height="19px" 
                                                            MaxLength="50" meta:resourcekey="txtMediaResource1"></asp:TextBox>
                                                    </div>
                                                    <div style="float: left">
                                                        <asp:RequiredFieldValidator ID="rfvMedia" runat="server" Display="Dynamic" ControlToValidate="txtMedia"
                                                            ForeColor="Red" ValidationGroup="vgDescription"><ucValidationContainer:ValidationContainer ID="rfvcMediaContainer" runat="server"
                                                                ValidationMsg="Please provide media name." Size="200" />
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr id="Tr_Colorant" runat="server">
                                                <td>
                                                    <span><asp:Localize runat="server" Text="Colorant:" ID="locColorant" 
                                                        meta:resourcekey="locColorantResource1"></asp:Localize><span style="color: Red;">*</span></span>
                                                </td>
                                                <td>
                                                    <div style="float: left;">
                                                        <asp:TextBox ID="txtColorant" runat="server" Width="200px" Height="19px" 
                                                            MaxLength="50" meta:resourcekey="txtColorantResource1"></asp:TextBox>
                                                    </div>
                                                    <div style="float: left">
                                                        <asp:RequiredFieldValidator ID="rfvColorant" runat="server" Display="Dynamic" ControlToValidate="txtColorant"
                                                            ForeColor="Red" ValidationGroup="vgDescription"><ucValidationContainer:ValidationContainer ID="rfvColorantContainer" runat="server"
                                                                ValidationMsg="Please provide colorant name." Size="200" />
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr id="Tr_Rip" runat="server">
                                                <td>
                                                    <span><asp:Localize runat="server" Text="Rip Settings:" ID="locRipSettings" 
                                                        meta:resourcekey="locRipSettingsResource1"></asp:Localize><span style="color: Red;">*</span></span>
                                                </td>
                                                <td>
                                                    <div style="float: left;">
                                                        <asp:TextBox ID="txtRipSetting" runat="server" Width="200px" Height="19px" 
                                                            MaxLength="50" meta:resourcekey="txtRipSettingResource1"></asp:TextBox>
                                                    </div>
                                                    <div style="float: left">
                                                        <asp:RequiredFieldValidator ID="rfvRipSetting" runat="server" Display="Dynamic" ControlToValidate="txtRipSetting"
                                                            ForeColor="Red" ValidationGroup="vgDescription"><ucValidationContainer:ValidationContainer ID="rfvRipSettingsContainer" runat="server"
                                                                ValidationMsg="Please provide rip settings." Size="200" />
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <span><asp:Localize Text="Notes:" runat="server" ID="locNotes" 
                                                        meta:resourcekey="locNotesResource1"></asp:Localize></span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNotes" runat="server" Width="300px" Height="100px" TextMode="MultiLine"
                                                        MaxLength="500" meta:resourcekey="txtNotesResource1"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span><asp:Localize runat="server" Text="Technician Name:" 
                                                        ID="locTechnicianName" meta:resourcekey="locTechnicianNameResource1"></asp:Localize></span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTechnicianName" runat="server" Width="200px" Height="19px" 
                                                        MaxLength="30" meta:resourcekey="txtTechnicianNameResource1"></asp:TextBox>
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
                <tr id="Tr_OffsetPrintData" runat="server" visible="false">
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
                                                        <asp:Localize runat="server" Text="Printing Type" 
                                                        ID="locOffsetData" meta:resourcekey="locOffsetDataResource1"></asp:Localize></label>
                                                    <asp:DropDownList runat="server" ID="cmbPrintingType" Width="200px" CssClass="select" meta:resourcekey="cmbPrintingTypeResource1">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Panel ID="panelOffsetData" Style="padding: 5px" runat="server" BorderStyle="None"
                                        Width="100%" meta:resourcekey="panelOffsetDataResource1">
                                         <table>
                                            <tr>
                                                <td style="width: 130px">
                                                   <span><asp:Localize runat="server" ID="locInkOrder" Text="Ink Order:" 
                                                                    meta:resourcekey="locInkOrderResource1"></asp:Localize>&nbsp; </span>&nbsp;
                                                </td>
                                                <td style="width: 500px">
                                                    <asp:DropDownList runat="server" ID="cmbInkOrder" Width="200px" CssClass="select" meta:resourcekey="cmbInkOrderResource1">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 130px">
                                                   <span><asp:Localize runat="server" ID="locFountainSol" Text="Fountain Solution:" 
                                                                    meta:resourcekey="locFountainSolResource1"></asp:Localize>&nbsp; </span>&nbsp; 
                                                </td>
                                                <td style="width: 500px">
                                                     <asp:TextBox ID="txtFountainSol" runat="server" Width="200px" Height="19px" 
                                                        MaxLength="100" meta:resourcekey="txtFountainSolResource1"></asp:TextBox>
                                                        <span><asp:Localize runat="server" ID="Localize2" Text="*Include Manufacture, Name" 
                                                                    meta:resourcekey="locFounSolRefResource1"></asp:Localize>&nbsp; </span> 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 130px">
                                                   <span><asp:Localize runat="server" ID="locBlanket" Text="Blanket:" 
                                                                    meta:resourcekey="locBlanketResource1"></asp:Localize>&nbsp; </span>&nbsp;
                                                </td>
                                                <td style="width: 500px">
                                                     <asp:TextBox ID="txtBlanket" runat="server" Width="200px" Height="19px" 
                                                        MaxLength="100" meta:resourcekey="txtBlanketResource1"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 130px">
                                                   <span><asp:Localize runat="server" ID="locBlanketAge" Text="Blanket Age:" 
                                                                    meta:resourcekey="locBlanketAgeResource1"></asp:Localize>&nbsp; </span>&nbsp;
                                                </td>
                                                <td style="width: 400px">
                                                     <asp:TextBox ID="txtBlanketAge" runat="server" Width="200px" Height="19px" 
                                                        MaxLength="100" meta:resourcekey="txtBlanketAgeResource1"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 130px">
                                                   <span><asp:Localize runat="server" ID="locScreening" Text="Screening:" 
                                                                    meta:resourcekey="locScreeningResource1"></asp:Localize>&nbsp; </span>&nbsp; 
                                                </td>
                                                <td style="width: 500px">
                                                     <asp:TextBox ID="txtScreening" runat="server" Width="200px" Height="19px" 
                                                        MaxLength="100" meta:resourcekey="txtScreeningResource1"></asp:TextBox>
                                                        <span><asp:Localize runat="server" ID="Localize3" Text="*Include Type(AM-FM), Dot Shape, Frequency" 
                                                                    meta:resourcekey="locScreeningRefResource1"></asp:Localize>&nbsp; </span>  
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 130px">
                                                   <span><asp:Localize runat="server" ID="locCoating" Text="Coating:" 
                                                                    meta:resourcekey="locCoatingResource1"></asp:Localize>&nbsp; </span>&nbsp; 
                                                </td>
                                                <td style="width: 500px">
                                                     <asp:TextBox ID="txtCoating" runat="server" Width="200px" Height="19px" 
                                                        MaxLength="100" meta:resourcekey="txtCoatingResource1"></asp:TextBox>
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
            
    <%--    </ContentTemplate>
    </asp:UpdatePanel>--%>
</div>
