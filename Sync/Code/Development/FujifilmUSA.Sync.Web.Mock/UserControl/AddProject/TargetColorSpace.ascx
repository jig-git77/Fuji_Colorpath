<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TargetColorSpace.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.TargetColorSpace" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
    <div class="formDeviceNoLine" style="display:none;">
        <ucStatusbar:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
       <%-- <div style="float: left">
            <label>
                Target Colorspace</label>
        </div>
        <div style="float: right">
            <asp:LinkButton ID="lbtnProfilerOptions" runat="server" CssClass="gray_bt" 
                onclick="lbtnProfilerOptions_Click">
                                        <span>Profiler Options</span>
            </asp:LinkButton>
        </div>--%>
        <div class="clear">
        </div>
    </div>
    <div class="listDevice">
        <table cellpadding="0" cellspacing="0" border="0" width="97%">
            <tr id="trListHeader" runat="server">
                <td>
                    <asp:Panel ID="panelAddProjectDescription" runat="server" BorderStyle="None">
                        <table>
                            <tr>
                                <td style="width: 130px">
                                    <span>
                                        Source Profile:</span>
                                </td>
                                <td style="width: 450px">
                                       <asp:DropDownList 
                                        ID="cmbSourceProfile" 
                                        runat="server" 
                                        Width="200px"  
                                        CssClass="select">
                                       </asp:DropDownList>
                                  
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" colspan="2">
                                    <span>
                                        Profile Description:</span>
                                </td>
                            </tr>
                            <tr>
                                <td  colspan="2">
                                    <asp:TextBox ID="txtProfileDescription" runat="server" 
                                    Width="115%"  
                                    Height="200px" 
                                    TextMode="MultiLine"
                                    Enabled="false"
                                    ReadOnly="true" ></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>