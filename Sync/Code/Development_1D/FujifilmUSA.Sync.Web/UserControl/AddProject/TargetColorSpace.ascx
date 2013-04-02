<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="TargetColorSpace.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.TargetColorSpace" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<style type="text/css">
textarea {
    resize: none;
}
</style>
<div class="head">
    <h1 id="hTitle" runat="server"><asp:Localize runat="server" 
            Text="New Print Condition - Target Colorspace" ID="locTargetColorspace" 
            meta:resourcekey="locTargetColorspaceResource1"></asp:Localize>
    </h1>
    <asp:LinkButton ID="lnkbtnTargetColorSpace" runat="server" CssClass="button" 
        OnClick="lnkbtnTargetColorSpace_Click">
        <span>
            <asp:Image ID="imgAdd" runat="server" 
        ImageUrl="~/Images/new_project_icon.png" meta:resourcekey="imgAddResource1" />
            
<asp:Localize runat="server" Text="Profiler Options" ID="locProfilerOptions" 
        meta:resourcekey="locProfilerOptionsResource1"></asp:Localize>
</span></asp:LinkButton>
    <asp:LinkButton ID="lnkbtnProjectDescription" runat="server" 
        CssClass="buttonLeft" OnClick="lnkbtnProjectDescription_Click">
        <span>
            <asp:Image ID="Image1" runat="server" 
        ImageUrl="~/Images/new_project_icon.png" meta:resourcekey="Image1Resource1" />
            
<asp:Localize runat="server" Text="Description" ID="locDescription" 
        meta:resourcekey="locDescriptionResource1"></asp:Localize>
</span></asp:LinkButton>
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
                    <table>
                        <tr>
                            <td style="width: 130px">
                                <span><asp:Localize runat="server" Text="Target Print Condition:" ID="locTargetPrint"></asp:Localize></span>
                            </td>
                            <td style="width: 450px">
                                <asp:DropDownList ID="cmbPrintStandardType" runat="server" CssClass="select" DataValueField="Key"
                                    DataTextField="Value" OnSelectedIndexChanged="cmbPrintStandardType_SelectedIndexChanged"
                                    AutoPostBack="True" meta:resourcekey="cmbSourceProfileResource1">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 130px">
                                <span><asp:Localize runat="server" Text="Source Profile:" ID="locSourceProfile" 
                                    meta:resourcekey="locSourceProfileResource1"></asp:Localize></span>
                            </td>
                            <td style="width: 450px">
                                <asp:Label ID="lblSourceProfileName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="2">
                                <span><asp:Localize runat="server" Text="Profile Description:" 
                                    ID="locProfileDesc" meta:resourcekey="locProfileDescResource1"></asp:Localize></span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:TextBox ID="txtProfileDescription" runat="server" Width="115%" Height="100px"
                                    TextMode="MultiLine" Enabled="False" ReadOnly="True" 
                                    meta:resourcekey="txtProfileDescriptionResource1"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</div>
