<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="AddSourceProfile.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddSourceProfile" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<%@ Register Src="~/UserControl/ValidationContainer.ascx" TagName="ValidationContainer"
    TagPrefix="ucValidationContainer" %>
    <script type="text/javascript">
        function IsRegionSelected(source, arguments) {
            if (document.getElementById('<%= cmbRegion.ClientID %>').value != "0") 
                arguments.IsValid = true;
            else
                arguments.IsValid = false;
        }
    </script>

    <style type="text/css">
        textarea {
            resize: none;
        }
</style>

<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1><asp:Localize runat="server" Text="Source Profile" ID="locSourceProfile" 
                meta:resourcekey="locSourceProfileResource1"></asp:Localize></h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <center>
                    <fieldset style="width: 60%;">
                        <legend>
                            <asp:Label ID="lblHeader" runat="server" Text="Add Source Profile" 
                                meta:resourcekey="lblHeaderResource1"></asp:Label>
                        </legend>
                        <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" 
                            Visible="False" />
                        <table>
                            <tr>
                                <td align="right">
                                    <span style="font-weight:bold;"><asp:Localize runat="server" Text="Region" 
                                        ID="locRegion" meta:resourcekey="locRegionResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbRegion" runat="server" DataValueField="Key" DataTextField="Value"
                                            CssClass="select" Width="200px" AutoPostBack="True" 
                                            OnSelectedIndexChanged="cmbRegion_SelectedIndexChanged" 
                                            meta:resourcekey="cmbRegionResource1">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <span style="font-weight:bold;"><asp:Localize runat="server" 
                                        Text="Company Name" ID="locCompanyName" 
                                        meta:resourcekey="locCompanyNameResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbCompanyName" runat="server" DataValueField="Key" DataTextField="Value"
                                            CssClass="select" Width="200px" meta:resourcekey="cmbCompanyNameResource1">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <span style="font-weight:bold;"><asp:Localize runat="server" 
                                        Text="Profile Name" ID="locProfileName" 
                                        meta:resourcekey="locProfileNameResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtSourceProfile" runat="server" Width="200px" MaxLength="50" 
                                            Height="19px" meta:resourcekey="txtSourceProfileResource1"></asp:TextBox>
                                        <asp:HiddenField ID="hdnSourceProfileID" runat="server" Value="0" />
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvSourceProfile" runat="server" Display="Dynamic"
                                            ControlToValidate="txtSourceProfile" ForeColor="Red" 
                                            ValidationGroup="vgSourceProfile" meta:resourcekey="rfvSourceProfileResource1"><ucValidationContainer:ValidationContainer ID="rfvSourceProfileContainer" runat="server"
                                                ValidationMsg="Please provide source profile name." Size="200" />
</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <span style="font-weight:bold;"><asp:Localize runat="server" 
                                        Text="Profile Description:" ID="locProfileDesc" 
                                        meta:resourcekey="locProfileDescResource1"></asp:Localize></span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtProfileDescription" runat="server" MaxLength="150" Width="300px"
                                            Height="50px" TextMode="MultiLine" 
                                            meta:resourcekey="txtProfileDescriptionResource1"></asp:TextBox>
                                    </div>
                                    <div style="float: left">
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px" align="right">
                                    <span style="font-weight:bold;"><asp:Localize runat="server" Text="Profile" 
                                        ID="locProfile" meta:resourcekey="locProfileResource1"></asp:Localize><span style="color: Red;">*</span> </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:FileUpload ID="fuProfile" runat="server" 
                                            meta:resourcekey="fuProfileResource1" />
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvProfile" runat="server" Display="Dynamic" ControlToValidate="fuProfile"
                                            ForeColor="Red" ValidationGroup="vgSourceProfile" 
                                            meta:resourcekey="rfvProfileResource1"><ucValidationContainer:ValidationContainer ID="rfvProfileContainer" runat="server"
                                                ValidationMsg="Please provide source profile file." Size="200" />
</asp:RequiredFieldValidator>     
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px" align="right">
                                    <span style="font-weight:bold;"><asp:Localize runat="server" Text="Is Used?" 
                                        ID="locIsUsed" meta:resourcekey="locIsUsedResource1"></asp:Localize></span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" 
                                            meta:resourcekey="chkIsActiveResource1" />
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
                                            <asp:LinkButton ID="lbtnSave" runat="server" CssClass="gray_bt" ValidationGroup="vgSourceProfile"
                                                OnClick="lbtnSave_Click">
                                                    <span> <asp:Localize runat="server" Text="Save" ID="locSave" 
                                                meta:resourcekey="locSaveResource1"></asp:Localize>
 </span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" 
                                                PostBackUrl="~/SourceProfileManagement.aspx">
                                                    <span> <asp:Localize ID="locCancel" runat="server" Text="Cancel" 
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
            <Triggers>
                <asp:PostBackTrigger ControlID="lbtnSave" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</div>
<div class="admin_bottom">
    &nbsp;</div>
<div class="clear">
</div>
