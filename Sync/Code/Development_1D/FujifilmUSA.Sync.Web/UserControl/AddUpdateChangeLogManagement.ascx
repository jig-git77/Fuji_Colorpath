<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddUpdateChangeLogManagement.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AddUpdateChangeLogManagement" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer" TagPrefix="ucValidationContainer" %>
<script src="JS/CommonFunctions.js?version=4" type="text/javascript"></script>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1><asp:Localize ID="locUpdateChangeLogManagement" runat="server" Text="Update Change Log Management"
            meta:resourcekey="locUpdateChangeLogManagementResource1"></asp:Localize></h1>
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
                                <asp:Localize ID="lblHeader" runat="server" Text="Add Update Change Log" 
                                    meta:resourcekey="loclblHeaderResource1"></asp:Localize>
                            </legend>
                            <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" Visible="False" />
                            <table>
                                 <tr>
                                <td align="right">
                                     <span style="font-weight: bold;"><asp:Localize ID="locHeaderLine" runat="server" Text="Header Line" 
                                         meta:resourcekey="loclblHeaderlineResource1"></asp:Localize>
                                         <span style="color: Red;">*</span>
                                      </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtHeaderLine" runat="server"  Width="400px" Height="19px"></asp:TextBox>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvHeaderLine" runat="server" Display="Dynamic" ControlToValidate="txtHeaderLine"
                                            ForeColor="Red" ValidationGroup="vgvgAddUpdateChangeLog" >          
                                            <ucValidationContainer:ValidationContainer ID="rfvValHeaderLine" runat="server"
                                                    ValidationMsg="Please provide header line." Size="200"/>
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Sub Header Line" 
                                            ID="locSubHeaderLine" runat="server" meta:resourcekey="loclocSubHeaderLineResource1"></asp:Localize><span style="color: Red;">*</span>
                                        </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;: &nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtSubHeaderLine" runat="server"  Width="400px" Height="19px"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                           <asp:RequiredFieldValidator ID="rfvSubHeaderLine" runat="server" Display="Dynamic" ControlToValidate="txtSubHeaderLine"
                                                ForeColor="Red" ValidationGroup="vgvgAddUpdateChangeLog">
                                               <ucValidationContainer:ValidationContainer ID="rfvVSubHeaderLine" runat="server"
                                                    ValidationMsg="Please provide sub header line." Size="200"/>
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;">
                                            <asp:Localize Text="Description" Id="locDescription" runat="server"
                                                meta:resourcekey="loclocDescriptionResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtDescription" runat="server"  Width="400px" TextMode="MultiLine" Height="200px"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" Display="Dynamic" ControlToValidate="txtDescription"
                                                ForeColor="Red" ValidationGroup="vgvgAddUpdateChangeLog">
                                                <ucValidationContainer:ValidationContainer ID="rfvMinReqVersionContainer" runat="server"
                                                    ValidationMsg="Please provide Description." Size="200"/>
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize Text="Activation Date" 
                                        ID="locActivationDate" runat="server" meta:resourcekey="locActivationDateResource1" ></asp:Localize><span style="color: Red;">*</span>
                                    </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                       <div style="float:left;">
                                        <telerik:RadDatePicker RenderMode="Lightweight" ID="txtActivationDateNew" Height="19px" Width="100px"  DateInput-DateFormat="MM/dd/yyyy" runat="server" ></telerik:RadDatePicker>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvActivationDate" runat="server" Display="Dynamic"
                                            ControlToValidate="txtActivationDateNew" ForeColor="Red" 
                                            ValidationGroup="vgvgAddUpdateChangeLog"><ucValidationContainer:ValidationContainer ID="rfvActivateContainer" runat="server"
                                                ValidationMsg="Please select activation date." Size="200" ResourceValidationMsg="ActivationDateRequiredResource"/>
                                    </asp:RequiredFieldValidator>
                                    </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                  <tr>
                                    <td align="right">
                                       <span style="font-weight: bold;"><asp:Localize ID="locExpirationDate" 
                                        runat="server" Text="Expiration Date" 
                                        meta:resourcekey="locExpirationDateResource1"></asp:Localize><span style="color: Red;">*</span>
                                    </span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float:left;">
                                        <telerik:RadDatePicker RenderMode="Lightweight" ID="txtExpirationDateNew" Height="19px" Width="100px"  DateInput-DateFormat="MM/dd/yyyy"  runat="server" >
                                        </telerik:RadDatePicker>
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvExpirationDate" runat="server" Display="Dynamic"
                                            ControlToValidate="txtExpirationDateNew" ForeColor="Red" 
                                            ValidationGroup="vgvgAddUpdateChangeLog"><ucValidationContainer:ValidationContainer ID="rfvExpiraDateContainer" runat="server"
                                                ValidationMsg="Please select expiration date." Size="200" ResourceValidationMsg="ExpirationDateRequiredResource"/>
                                            </asp:RequiredFieldValidator>
                                    </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize ID="locIsActive" runat="server" 
                                            Text="Is Active?"  meta:resourcekey="locIsActiveResource1"></asp:Localize></span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:CheckBox ID="chkIsActive" runat="server" Checked="True"/>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span style="font-weight: bold;"><asp:Localize ID="locIsAlert" runat="server" 
                                            Text="Is Alert?" meta:resourcekey="locIsAlertResource1"></asp:Localize></span>
                                    </td>
                                    <td>
                                        <b>&nbsp;:&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        <div style="float: left;">
                                            <asp:CheckBox ID="chkIsAlert" runat="server"/>
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
                                    <td align="left">
                                        <div class="text">
                                            <div style="padding-left: 3px;">
                                                <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgvgAddUpdateChangeLog"
                                                    OnClick="lbtnSave_Click">
                                                    <span> <asp:Localize Text="Save" ID="locSave" runat="server" 
                                                    meta:resourcekey="locSaveResource1"></asp:Localize> </span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" 
                                                    PostBackUrl="~/UpdateChangeLogManagement.aspx">
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
    &nbsp;</div>
<div class="clear">
</div>