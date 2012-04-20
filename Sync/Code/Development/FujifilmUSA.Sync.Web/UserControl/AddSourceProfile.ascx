<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddSourceProfile.ascx.cs"
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

<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle" style="line-height: 34px;">
    <div class="head">
        <h1>
            Source Profile</h1>
    </div>
    <div class="clear">
    </div>
    <div style="margin-left: 10px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
            <ContentTemplate>
                <center>
                    <fieldset style="width: 60%;">
                        <legend>
                            <asp:Label ID="lblHeader" runat="server" Text="Add Source Profile"></asp:Label>
                        </legend>
                        <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" Visible="false" />
                        <table>
                            <tr>
                                <td>
                                    <span>Region<span style="color: Red;">*</span> </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbRegion" runat="server" DataValueField="Key" DataTextField="Value"
                                            CssClass="select" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="cmbRegion_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                       <%-- <asp:RequiredFieldValidator ID="rfvRegion" runat="server" Display="Dynamic" ControlToValidate="cmbRegion"
                                            ForeColor="Red" ValidationGroup="vgSourceProfile" InitialValue="0">
                                            <ucValidationContainer:ValidationContainer ID="rfvRegionContainer" runat="server"
                                                ValidationMsg="Please select region." Size="200" />
                                        </asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span>Company Name<span style="color: Red;">*</span> </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbCompanyName" runat="server" DataValueField="Key" DataTextField="Value"
                                            CssClass="select" Width="200px">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left">
                                        <%--<asp:CustomValidator ID="cvCompanyName" ClientValidationFunction="IsRegionSelected" runat="server"
                                            ValidationGroup="vgSourceProfile">
                                            <ucValidationContainer:ValidationContainer ID="rfvCompanyuContainer" runat="server"
                                                ValidationMsg="Please select company." Size="200" />
                                        </asp:CustomValidator>--%>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span>Profile Name<span style="color: Red;">*</span> </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtSourceProfile" runat="server" Width="200px" MaxLength="50" Height="19px"></asp:TextBox>
                                        <asp:HiddenField ID="hdnSourceProfileID" runat="server" Value="0" />
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvSourceProfile" runat="server" Display="Dynamic"
                                            ControlToValidate="txtSourceProfile" ForeColor="Red" ValidationGroup="vgSourceProfile">
                                            <ucValidationContainer:ValidationContainer ID="rfvSourceProfileContainer" runat="server"
                                                ValidationMsg="Please provide source profile name." Size="200" />
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span>Profile Description:</span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtProfileDescription" runat="server" MaxLength="150" Width="300px"
                                            Height="50px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div style="float: left">
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px">
                                    <span>Profile<span style="color: Red;">*</span> </span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:FileUpload ID="fuProfile" runat="server" />
                                    </div>
                                    <div style="float: left">
                                        <asp:RequiredFieldValidator ID="rfvProfile" runat="server" Display="Dynamic" ControlToValidate="fuProfile"
                                            ForeColor="Red" ValidationGroup="vgSourceProfile">
                                            <ucValidationContainer:ValidationContainer ID="rfvProfileContainer" runat="server"
                                                ValidationMsg="Please provide source profile file." Size="200" />
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px">
                                    <span>Is Used?</span>
                                </td>
                                <td>
                                    <b>&nbsp;:&nbsp;</b>
                                </td>
                                <td style="width: 400px">
                                    <div style="float: left;">
                                        <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" />
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
                                                    <span> Save </span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="gray_bt" PostBackUrl="~/SourceProfileManagement.aspx">
                                                    <span> Cancel </span>
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
