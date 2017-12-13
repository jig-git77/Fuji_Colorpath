<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="TargetColorSpace.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.TargetColorSpace" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<style type="text/css">
    textarea {
        resize: none;
    }

    .textwidth50 {
        width: 50px;
    }

    .textwidth40 {
        width: 40px;
    }
</style>

<div class="head">
    <h1 id="hTitle" runat="server">
        <asp:Localize runat="server"
            Text="New Print Condition - Target Colorspace" ID="locTargetColorspace"
            meta:resourcekey="locTargetColorspaceResource1"></asp:Localize>
    </h1>
    <asp:LinkButton ID="lnkbtnTargetColorSpace" runat="server" CssClass="button"
        OnClick="lnkbtnTargetColorSpace_Click" ValidationGroup="b">
        <span>
            <asp:Image ID="imgAdd" runat="server"
                ImageUrl="~/Images/new_project_icon.png" meta:resourcekey="imgAddResource1" />

            <asp:Localize runat="server" Text="Profiler Options" ID="locProfilerOptions"
                meta:resourcekey="locProfilerOptionsResource1"></asp:Localize>
        </span>
    </asp:LinkButton>
    <asp:LinkButton ID="lnkbtnProjectDescription" runat="server"
        CssClass="buttonLeft" OnClick="lnkbtnProjectDescription_Click">
        <span>
            <asp:Image ID="Image1" runat="server"
                ImageUrl="~/Images/new_project_icon.png" meta:resourcekey="Image1Resource1" />

            <asp:Localize runat="server" Text="Description" ID="locDescription"
                meta:resourcekey="locDescriptionResource1"></asp:Localize>
        </span>
    </asp:LinkButton>
    <asp:LinkButton ID="lnkbtNext" runat="server" CssClass="button"
        OnClick="lnkbtnNext_Click" Visible="false">
        <span>
            <asp:Image ID="Image2" runat="server"
                ImageUrl="~/Images/new_project_icon.png" meta:resourcekey="imgAddResource11" />
            <asp:Localize runat="server" ID="Localize50" Text="Next"
                meta:resourcekey="locNextResource1"></asp:Localize>
        </span>
    </asp:LinkButton>
</div>
<div class="formDeviceNoLine" style="display: none;">
    <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="False" />
    <div class="clear">
    </div>
</div>
<div class="listDevice">
    <table cellpadding="0" cellspacing="0" border="0" width="97%">
        <tr id="trListHeader" runat="server">
            <td>
                <asp:Panel ID="panelAddProjectDescription" runat="server" BorderStyle="None"
                    meta:resourcekey="panelAddProjectDescriptionResource1">
                    <table width="100%">
                        <tr>
                            <td>
                                <span>
                                    <asp:Localize runat="server" Text="Profile Category:" ID="locTargetPrintCategory" meta:resourcekey="locTargetPrintCategoryResource1"></asp:Localize></span>
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="cmbTargetPrintCategory" runat="server" CssClass="select" DataValueField="Key"
                                    DataTextField="Value" OnSelectedIndexChanged="cmbTargetPrintCategory_SelectedIndexChanged"
                                    AutoPostBack="True" meta:resourcekey="cmbTargetPrintCategoryResource1">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span>
                                    <asp:Localize runat="server" Text="Target Print Condition:" ID="locTargetPrint" meta:resourcekey="locTargetPrintResource1"></asp:Localize></span>
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="cmbPrintStandardType" runat="server" CssClass="select" DataValueField="Key"
                                    DataTextField="Value" OnSelectedIndexChanged="cmbPrintStandardType_SelectedIndexChanged"
                                    AutoPostBack="True" meta:resourcekey="cmbSourceProfileResource1">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 130px">
                                <span>
                                    <asp:Localize runat="server" Text="Source Profile:" ID="locSourceProfile"
                                        meta:resourcekey="locSourceProfileResource1"></asp:Localize></span>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblSourceProfileName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" valign="top">
                                <span>
                                    <asp:Localize runat="server" Text="Profile Description:"
                                        ID="locProfileDesc" meta:resourcekey="locProfileDescResource1"></asp:Localize></span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:TextBox ID="txtProfileDescription" runat="server" Width="100%" Height="50px"
                                    TextMode="MultiLine" Enabled="False" ReadOnly="True"
                                    meta:resourcekey="txtProfileDescriptionResource1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trProjectType" runat="server">
                            <td colspan="4">
                                <asp:Panel ID="PanelProjectType" runat="server" GroupingText="Project Type" Width="100%">
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td colspan="2">
                                                <span style="padding-left: 5px;">
                                                    <asp:Localize runat="server" Text="Choose Method:" ID="locChooseMethod"
                                                        meta:resourcekey="locChooseMethodResource1"></asp:Localize></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="width: 130px; padding-left: 5px;">
                                                <asp:DropDownList ID="cmbProjectType" runat="server" OnSelectedIndexChanged="cmbProjectType_SelectedIndexChanged" AutoPostBack="true" Width="100px">
                                                    <asp:ListItem Text="G7" Value="1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="ISO" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtProjectTypeDesc" runat="server" TextMode="MultiLine" Height="50px" Width="98%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="height: 7px;"></td>
                        </tr>
                        <tr id="trDensityPCS" runat="server" visible="false">
                            <td></td>
                            <td></td>
                            <td style="width: 130px;">
                                <span>
                                    <asp:Localize runat="server" Text="PCS DataType:" ID="Localize2"
                                        meta:resourcekey="locPCSDataTypeResource1"></asp:Localize></span></td>
                            <td>
                                <asp:DropDownList ID="cmbPCSDataType" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr id="trTargetLAB" runat="server">
            <td>
                <div style="display: block;" class="contantbox tabcontant">
                    <div class="section_top">
                        &nbsp;
                    </div>
                    <div class="section_middle">
                        <div class="combopart">
                            <div class="header">
                                <div class="content">
                                    <div class="text" style="float: left;">
                                        <label>
                                            <asp:Localize runat="server" Text="Color Metric"
                                                ID="locTargetLAB" meta:resourcekey="locTargetLABResource1"></asp:Localize></label>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>



                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="padding-left: 8px;">
                                        <asp:CheckBox ID="chk_IsPaperScaling" runat="server" CssClass="checkbox" Text="  Relative Paper Scaling"
                                            AutoPostBack="True" OnCheckedChanged="chk_IsPaperScaling_CheckedChanged" meta:resourcekey="chk_IsPaperScalingResource1" />
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel_PaperLAB" Style="padding: 5px" runat="server" BorderStyle="None" Enabled="false">
                                            <asp:Localize runat="server" Text="New Paper Values:" ID="locPanel_PaperLAB" meta:resourcekey="Panel_PaperLABResource1"></asp:Localize>

                                            L*<asp:TextBox ID="txtPaper_L" runat="server" CssClass="textwidth40"></asp:TextBox>
                                            <asp:RangeValidator ID="RangeValidator2" runat="server"
                                                ControlToValidate="txtPaper_L" ErrorMessage="*" ForeColor="Red"
                                                ValidationGroup="a" MinimumValue="0" MaximumValue="100" Type="Double"></asp:RangeValidator>
                                            &nbsp;
                                                    a*<asp:TextBox ID="txtPaper_a" runat="server" CssClass="textwidth40"></asp:TextBox>
                                            <asp:RangeValidator ID="RangeValidator3" runat="server"
                                                ControlToValidate="txtPaper_a" ErrorMessage="*" ForeColor="Red"
                                                ValidationGroup="a" MinimumValue="-128" MaximumValue="127" Type="Double"></asp:RangeValidator>
                                            &nbsp;
                                                    b*<asp:TextBox ID="txtPaper_b" runat="server" CssClass="textwidth40"></asp:TextBox>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server"
                                                ControlToValidate="txtPaper_b" ErrorMessage="*" ForeColor="Red"
                                                ValidationGroup="a" MinimumValue="-128" MaximumValue="127" Type="Double"></asp:RangeValidator>
                                            &nbsp;
                                                    <asp:Button ID="btn_PaperApply" runat="server" Text="Apply!"
                                                        OnClick="btn_PaperApply_Click" ValidationGroup="a" meta:resourcekey="locbtn_PaperApplyResource1" />
                                            <asp:Button ID="btn_PaperClear" runat="server" OnClick="btn_PaperClear_Click"
                                                Text="Reset" meta:resourcekey="locbtn_PaperClearResource1" />
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="panel1" Style="padding: 5px" runat="server"
                                            BorderStyle="None" Enabled="false">
                                            <asp:ListView ID="lvTargetLAB" runat="server" ItemPlaceholderID="phItems">
                                                <LayoutTemplate>
                                                    <table border="0" cellspacing="2" cellpadding="0" width="50%">
                                                        <tr align="center" style="line-height: 15px;">
                                                            <td></td>
                                                            <td colspan="3"><b>
                                                                <asp:Localize runat="server" Text="Targets" ID="locTargets" meta:resourcekey="locTargetsResource1"></asp:Localize></b></td>
                                                        </tr>
                                                        <tr align="center" style="line-height: 15px;">
                                                            <td align="left">
                                                                <b>
                                                                    <asp:Localize runat="server" Text="Patch"
                                                                        ID="locPatch" meta:resourcekey="locPatchResource1"></asp:Localize></b>
                                                            </td>
                                                            <td><b>L*</b></td>
                                                            <td><b>a*</b></td>
                                                            <td><b>b*</b></td>
                                                        </tr>
                                                        <asp:PlaceHolder ID="phItems" runat="server"></asp:PlaceHolder>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="width: 150px;">
                                                            <asp:Label ID="lblPatch" runat="server" Text='<%# Eval("PatchType.Type") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdnPatchTypeId" runat="server" Value='<%# Eval("PatchType.PatchTypeId") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPatchTargetL" runat="server" Text='<%# string.Format("{0:n2}", Eval("TargetL")) %>' CssClass="textwidth50"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtPatchTargetA" runat="server" Text='<%# string.Format("{0:n2}", Eval("TargetA")) %>' CssClass="textwidth50"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtPatchTargetB" runat="server" Text='<%# string.Format("{0:n2}", Eval("TargetB")) %>' CssClass="textwidth50"></asp:TextBox></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>






                        </div>
                    </div>
                    <div class="section_bottom">
                        &nbsp;
                    </div>
                </div>
            </td>
        </tr>

        <tr id="trSpotTarget" runat="server" visible="false">
            <td>
                <asp:UpdatePanel runat="server" ID="upd">
                    <ContentTemplate>
                        <asp:Panel ID="panelSpotTarget" runat="server" BorderStyle="None"
                            meta:resourcekey="panelAddProjectDescriptionResource1">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <span>
                                            <asp:Localize runat="server" Text="Spot Target:" ID="Localize1" meta:resourcekey="locSpotTargetResource1"></asp:Localize></span>
                                    </td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="cmbSpotTargetList" runat="server" CssClass="select" DataValueField="Key"
                                            DataTextField="Value" OnSelectedIndexChanged="cmbSpotTargetList_SelectedIndexChanged"
                                            AutoPostBack="True" meta:resourcekey="cmbSpotTargetResource1">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="trMeasurementMode" runat="server">
                                    <td style="width: 130px">
                                        <span>
                                            <asp:Localize runat="server" Text="Measurement Mode:" ID="Localize3"
                                                meta:resourcekey="locMeasurementResource1"></asp:Localize></span>
                                    </td>
                                    <td colspan="3">
                                        <asp:Label ID="lblMeasMode" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" valign="top">
                                        <span>
                                            <asp:Localize runat="server" Text="Owner Name / Description:"
                                                ID="Localize4" meta:resourcekey="locOwnerResource1"></asp:Localize></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:TextBox ID="txtOwner" runat="server" Width="100%" Height="50px"
                                            TextMode="MultiLine" Enabled="False" ReadOnly="True"
                                            meta:resourcekey="txtOwnerDescriptionResource1"></asp:TextBox>
                                    </td>
                                </tr>


                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>

         <tr id="trPaperScaling_4D" runat="server" visible="false">
            <td>
                <div style="display: block;" class="contantbox tabcontant">
                    <div class="section_top">
                        &nbsp;
                    </div>
                    <div class="section_middle">
                        <div class="combopart">
                            <div class="header">
                                <div class="content">
                                    <div class="text" style="float: left;">
                                        <label>
                                            <asp:Localize runat="server" Text="Color Metric"
                                                ID="Localize5" meta:resourcekey="locTargetLABResource1"></asp:Localize></label>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>



                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="padding-left: 8px;">
                                        <asp:CheckBox ID="chk_IsPaperScaling4D" runat="server" CssClass="checkbox" Text="  Relative Paper Scaling"
                                            AutoPostBack="True" OnCheckedChanged="chk_IsPaperScaling4D_CheckedChanged" meta:resourcekey="chk_IsPaperScalingResource1" />
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel_PaperLAB4D_White" Style="padding: 5px" runat="server" BorderStyle="None" Enabled="false">
                                            <asp:Localize runat="server" Text="New White Paper Values:" ID="Localize6" meta:resourcekey="Panel_PaperLABResource1"></asp:Localize>

                                            L*<asp:TextBox ID="txtPaper_L_4D_White" runat="server" CssClass="textwidth40"></asp:TextBox>
                                            <asp:RangeValidator ID="RangeValidator4" runat="server"
                                                ControlToValidate="txtPaper_L_4D_White" ErrorMessage="*" ForeColor="Red"
                                                ValidationGroup="b" MinimumValue="0" MaximumValue="100" Type="Double"></asp:RangeValidator>
                                            &nbsp;
                                                    a*<asp:TextBox ID="txtPaper_a_4D_White" runat="server" CssClass="textwidth40"></asp:TextBox>
                                            <asp:RangeValidator ID="RangeValidator5" runat="server"
                                                ControlToValidate="txtPaper_a_4D_White" ErrorMessage="*" ForeColor="Red"
                                                ValidationGroup="b" MinimumValue="-128" MaximumValue="127" Type="Double"></asp:RangeValidator>
                                            &nbsp;
                                                    b*<asp:TextBox ID="txtPaper_b_4D_White" runat="server" CssClass="textwidth40"></asp:TextBox>
                                            <asp:RangeValidator ID="RangeValidator6" runat="server"
                                                ControlToValidate="txtPaper_b_4D_White" ErrorMessage="*" ForeColor="Red"
                                                ValidationGroup="b" MinimumValue="-128" MaximumValue="127" Type="Double"></asp:RangeValidator>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="padding-left: 8px;">
                                       
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel_PaperLAB4D_Black" Style="padding: 5px" runat="server" BorderStyle="None" Enabled="false">
                                            <asp:Localize runat="server" Text="New Black Paper Values:" ID="Localize7" meta:resourcekey="Panel_PaperLABResource1"></asp:Localize>

                                            L*<asp:TextBox ID="txtPaper_L_4D_Black" runat="server" CssClass="textwidth40"></asp:TextBox>
                                            <asp:RangeValidator ID="RangeValidator7" runat="server"
                                                ControlToValidate="txtPaper_L_4D_Black" ErrorMessage="*" ForeColor="Red"
                                                ValidationGroup="b" MinimumValue="0" MaximumValue="100" Type="Double"></asp:RangeValidator>
                                            &nbsp;
                                                    a*<asp:TextBox ID="txtPaper_a_4D_Black" runat="server" CssClass="textwidth40"></asp:TextBox>
                                            <asp:RangeValidator ID="RangeValidator8" runat="server"
                                                ControlToValidate="txtPaper_a_4D_Black" ErrorMessage="*" ForeColor="Red"
                                                ValidationGroup="b" MinimumValue="-128" MaximumValue="127" Type="Double"></asp:RangeValidator>
                                            &nbsp;
                                                    b*<asp:TextBox ID="txtPaper_b_4D_Black" runat="server" CssClass="textwidth40"></asp:TextBox>
                                            <asp:RangeValidator ID="RangeValidator9" runat="server"
                                                ControlToValidate="txtPaper_b_4D_Black" ErrorMessage="*" ForeColor="Red"
                                                ValidationGroup="b" MinimumValue="-128" MaximumValue="127" Type="Double"></asp:RangeValidator>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="section_bottom">
                        &nbsp;
                    </div>
                </div>
            </td>
        </tr>
    </table>
</div>
