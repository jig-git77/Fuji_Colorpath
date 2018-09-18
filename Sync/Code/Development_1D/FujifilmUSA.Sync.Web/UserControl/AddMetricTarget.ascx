<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddMetricTarget.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AddMetricTarget" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer" TagPrefix="ucValidationContainer" %>
<script src="JS/CommonFunctions.js?version=4" type="text/javascript"></script>
<div class="admin_top">
    &nbsp;
</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1>
            <asp:Localize ID="locMetricTargetPreset" runat="server" Text="Metric Target Management"
                meta:resourcekey="locMetricTargetPresetResource1"></asp:Localize></h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <center>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <center>
                        <fieldset style="width: 60%;">
                            <legend>
                                <asp:Label ID="lblHeader" runat="server" Text="Add Metric Target Preset"  meta:resourcekey="lblHeaderResource1">
                                  </asp:Label>
                            </legend>
                            <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                                Visible="False" />
                            <table>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Preset Name" 
                                            Id="locPresetName" runat="server" 
                                            meta:resourcekey="locPresetNameResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <%--<asp:TextBox ID="txtPresetName" runat="server" MaxLength="50" Width="400px" 
                                                Height="19px" meta:resourcekey="txtPresetNameResource1"></asp:TextBox>--%>
                                            <asp:Label  runat="server" ID="lblPresetName"></asp:Label>
                                            <asp:HiddenField runat="server" ID="hdfTargetPresetID" />
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Description" ID="locDescription" 
                                            runat="server" meta:resourcekey="locDescriptionResource1"></asp:Localize>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtDescription" runat="server" MaxLength="50" Width="400px" 
                                                Height="52px" meta:resourcekey="txtDescriptionResource1" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                       
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td colspan="3">
                                        <asp:GridView runat="server" ID="grdMetricSum" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="MetricSumName" HeaderText="MetricSum Name" />
                                                <asp:TemplateField HeaderText="Target Value">
                                                    <ItemTemplate>
                                                        <asp:HiddenField runat="server" ID="hdfMetricSumId" Value='<%# Bind("MetricSumID") %>' />
                                                        <asp:HiddenField runat="server" ID="hdfTargetMetricId" Value='<%# Bind("MetricTargetId") %>' />
                                                        <asp:TextBox runat="server" ID="txtTargetValue" Width="30"  Text='<%# Bind("TargetValue") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="left">
                                        <div class="text">
                                            <div style="padding-left: 3px;">
                                                <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgAddMetricTargetPreset"
                                                    OnClick="lbtnSave_Click">
                                                    <span> <asp:Localize Text="Save" ID="locSave" runat="server" 
                                                    meta:resourcekey="locSaveResource1"></asp:Localize> </span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" 
                                                    onclick="lbtnCancel_Click">
                                                    <span> <asp:Localize Text="Cancel" ID="locCancel" runat="server" 
                                                    meta:resourcekey="locCancelResource1"></asp:Localize> </span>
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
        </center>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;
</div>
<div class="clear">
</div>
