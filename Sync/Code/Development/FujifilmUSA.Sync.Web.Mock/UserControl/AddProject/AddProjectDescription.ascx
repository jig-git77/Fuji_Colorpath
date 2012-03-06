<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddProjectDescription.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.Mock.UserControl.AddProjectDescription" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>

<script type="text/javascript" language="javascript">

    function Validation() {
        
        
    }
    </script>
    <div class="formDeviceNoLine" style="display:none;">
        <ucStatusbar:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
       <%-- <div style="float: left">
            <label>
                Enter Print Condition Description</label>
        </div>
        <div style="float: right">
            <asp:LinkButton ID="lbtnTargetColorSpace" runat="server" CssClass="gray_bt" 
                onclick="lbtnTargetColorSpace_Click" ValidationGroup="vgDescription" >
                                        <span>Traget ColorSpace </span>
            </asp:LinkButton>
        </div>--%>
        <div class="clear">
        </div>
    </div>
    <div class="listDevice">
        <table cellpadding="0" cellspacing="0" border="0" width="97%">
            <tr id="trListHeader" runat="server">
                <td>
                    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                        <AjaxSettings>
                            <telerik:AjaxSetting AjaxControlID="rgProjectList">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="panelAddProjectDescription" LoadingPanelID="RadAjaxLoadingPanel1" />
                                </UpdatedControls>
                            </telerik:AjaxSetting>
                        </AjaxSettings>
                    </telerik:RadAjaxManager>
                    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"  />
                    <asp:Panel ID="panelAddProjectDescription" runat="server" BorderStyle="None">
                        <table>
                            <tr>
                                <td style="width: 130px">
                                    <span>
                                        Press:</span>
                                </td>
                                <td style="width: 400px">
                                    <asp:TextBox ID="txtPress" runat="server" Width="200px" Height="19px" Enabled="false" Text="Komori LS 40"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span>
                                        Device Family:</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDeviceFamily" runat="server" Width="200px"  Height="19px" Enabled="false" Text="Komori"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span>
                                        Media:<span style="color: Red;">*</span></span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMedia" runat="server" Width="200px" Height="19px"></asp:TextBox>
                                    <asp:RequiredFieldValidator 
                                            ID="rfvMedia" 
                                            runat="server" 
                                            Display="Dynamic"
                                            ControlToValidate="txtMedia" 
                                            ErrorMessage = "Please provide media name." 
                                             ForeColor="Red"
                                            ValidationGroup="vgDescription" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span>
                                        Colorant:<span style="color: Red;">*</span></span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtColorant" runat="server" Width="200px" Height="19px"></asp:TextBox>
                                    <asp:RequiredFieldValidator 
                                            ID="rfvColorant" 
                                            runat="server" 
                                            Display="Dynamic"
                                            ControlToValidate="txtColorant" 
                                            ErrorMessage = "Please provide colorant name." 
                                            ForeColor="Red"
                                            ValidationGroup="vgDescription" ></asp:RequiredFieldValidator>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span>
                                        Rip Settings:<span style="color: Red;">*</span></span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRipSetting" runat="server" Width="200px" Height="19px"></asp:TextBox>
                                    <asp:RequiredFieldValidator 
                                            ID="rfvRipSetting" 
                                            runat="server" 
                                            Display="Dynamic"
                                            ControlToValidate="txtRipSetting" 
                                            ErrorMessage = "Please provide rip setting name."
                                            ForeColor="Red" 
                                            ValidationGroup="vgDescription" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <span>
                                        Notes:</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNotes" runat="server" Width="300px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span>
                                        Technician Name:</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTechnicianName" runat="server" Width="200px" Height="19px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>