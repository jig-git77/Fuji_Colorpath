<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssignRefChartWMetricTargetPreset.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AssignRefChartWMetricTargetPreset" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer" TagPrefix="ucValidationContainer" %>

<link rel="stylesheet" href="JQuery/datepicker-themes/base/jquery.ui.all.css">
<script src="JS/CommonFunctions.js?version=4" type="text/javascript"></script>

<style >
    .mycheckbox input[type="checkbox"] 
    { 
        margin-right: 5px; 
    }
</style>

<div class="admin_top">
    &nbsp;
</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1>
            <asp:Localize ID="locPrintStandardTypeMgmt" runat="server" Text="Assign Chart with MTP"
                meta:resourcekey="locPrintStandardTypeMgmtResource1"></asp:Localize></h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <center>
                    <fieldset style="width: 70%;">
                        <legend>
                            <asp:Label ID="lblHeader" runat="server" Text="Assign Chart with MTP" 
                                meta:resourcekey="lblHeaderResource1"></asp:Label>
                        </legend>
                        <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                            Visible="False" />
                        <table width="100%">
                            <asp:UpdatePanel runat="server">

                            </asp:UpdatePanel>
                            <tr>
                                <td align="right">
                                    <span style="font-weight: bold;"><asp:Localize Text="Reference Chart" 
                                        ID="locReferenceChartTypeID" runat="server" meta:resourcekey="locReferenceChartTypeIDResource1"></asp:Localize><span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td style="width:8px;">
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList AutoPostBack="true" ID="cmbReferenceChartTypeID" runat="server" Width="370px"  CssClass="select" OnSelectedIndexChanged="cmbReferenceChartTypeID_SelectedIndexChanged" 
                                            meta:resourcekey="cmbReferenceChartTypeIDResource1">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <span style="font-weight: bold;"><asp:Localize Text="Metric Target Preset" 
                                        ID="locRankId" runat="server" meta:resourcekey="locRankIdResource1"></asp:Localize><span style="color: Red;">*</span>
                                    </span>
                                </td>
                                <td style="width:8px;">
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td>
                                    <div style="float: left;"  >
                                        <asp:Panel BorderStyle="Inset" ScrollBars="Auto"  Width="350px" Height="200px" Wrap="true" runat="server" >
                                          <asp:CheckBoxList  runat="server" CssClass="mycheckbox" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Table" ID="chklistMetricTargetPresetID" />
                                         </asp:Panel>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <div class="text">
                                        <div style="padding-left: 3px;">
                                            <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgAssignRefChartWMetricTargetPreset"
                                           OnClick="lbtnSave_Click"     >
                                              <span> <asp:Localize Text="Save" runat="server" ID="locSave" 
                                                meta:resourcekey="locSaveResource1"></asp:Localize>
                                                </span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" 
                                                PostBackUrl="~/Settings.aspx">
                                             <span> <asp:Localize Text="Cancel" runat="server" ID="locCancel" 
                                                meta:resourcekey="locCancelResource1"></asp:Localize>
                                             </span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </center>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;
</div>
<div class="clear">
</div>
