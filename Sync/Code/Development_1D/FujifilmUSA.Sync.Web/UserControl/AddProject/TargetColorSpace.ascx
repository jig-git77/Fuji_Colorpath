<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="TargetColorSpace.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.TargetColorSpace" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<style type="text/css">
textarea {
    resize: none;
}
 .textwidth50
    {
        width: 50px;
    }    
.textwidth40
    {
        width: 40px;
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
                    <table width="100%">
                        <tr>
                            <td >
                                <span><asp:Localize runat="server" Text="Target Print Condition:" ID="locTargetPrint"></asp:Localize></span>
                            </td>
                            <td colspan="3" >
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
                            <td colspan="3">
                                <asp:Label ID="lblSourceProfileName" runat="server"></asp:Label>
                            </td>
                        </tr>                       
                        <tr>
                            <td colspan="4" valign="top" >
                                <span><asp:Localize runat="server" Text="Profile Description:" 
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
                            <td >
                                <span><asp:Localize runat="server" Text="Project Type:" ID="locProjectType" 
                                    meta:resourcekey="locProjectTypeResource1"></asp:Localize></span></td>
                            <td colspan="3" >
                                <asp:DropDownList ID="cmbProjectType" runat="server" onselectedindexchanged="cmbProjectType_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="G7" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="ISO" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>                           
                        </tr>
                        <tr>
                             <td >
                                <span><asp:Localize runat="server" Text="Density Type:" ID="Localize1" 
                                    meta:resourcekey="locDensityTypeResource1"></asp:Localize></span></td>
                            <td>
                                <asp:DropDownList ID="cmbDensityType" runat="server" >                                  
                                </asp:DropDownList>
                            </td>                        
                             <td style="width:130px;" >
                                <span><asp:Localize runat="server" Text="PCS DataType:" ID="Localize2" 
                                    meta:resourcekey="locPCSDataTypeResource1"></asp:Localize></span></td>
                            <td >
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
                            &nbsp;</div>
                        <div class="section_middle">
                            <div class="combopart">
                                <div class="header">
                                    <div class="content">
                                        <div class="text" style="float: left;">  
                                                <label><asp:Localize runat="server" Text="Color Metric" 
                                                ID="locTargetLAB"></asp:Localize></label>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                                
                             
                                     
                                     <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="padding-left:8px;">
                                                <asp:CheckBox ID="chk_IsPaperScaling" runat="server" CssClass="checkbox" Text="  Relative Paper Scaling" 
                                                    AutoPostBack="True" oncheckedchanged="chk_IsPaperScaling_CheckedChanged" />
                                            </td>
                                            <td>
                                                <asp:Panel ID="Panel_PaperLAB" Style="padding: 5px" runat="server" BorderStyle="None" Enabled="false">
                                                    New Paper Values:
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
                                                         onclick="btn_PaperApply_Click" ValidationGroup="a" />
                                                    <asp:Button ID="btn_PaperClear" runat="server" onclick="btn_PaperClear_Click" 
                                                         Text="Reset" />
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
                                                  <tr align="center" style="line-height:15px;">
                                                    <td></td>
                                                    <td colspan="3"><b>Targets</b></td>
                                                  </tr>
                                                <tr align="center" style="line-height:15px;">
                                                <td align="left"><b>Patch</b></td>
                                                <td><b>L*</b></td>
                                                <td><b>a*</b></td>
                                                <td><b>b*</b></td>
                                            </tr>
                                                <asp:PlaceHolder ID="phItems" runat="server"></asp:PlaceHolder>    
                                            </table>                                            
                                        </LayoutTemplate>
                                        <ItemTemplate>                                            
                                            <tr>
                                                <td style="width: 150px;"><asp:Label ID="lblPatch" runat="server" Text='<%# Eval("PatchType.Type") %>'></asp:Label>
                                                    <asp:HiddenField ID="hdnPatchTypeId" runat="server" Value='<%# Eval("PatchType.PatchTypeId") %>' />
                                                </td>
                                                <td><asp:TextBox ID="txtPatchTargetL" runat="server" Text='<%# string.Format("{0:n2}", Eval("TargetL")) %>' CssClass="textwidth50"></asp:TextBox></td>
                                                <td><asp:TextBox ID="txtPatchTargetA" runat="server" Text='<%# string.Format("{0:n2}", Eval("TargetA")) %>' CssClass="textwidth50"></asp:TextBox></td>
                                                <td><asp:TextBox ID="txtPatchTargetB" runat="server" Text='<%# string.Format("{0:n2}", Eval("TargetB")) %>' CssClass="textwidth50"></asp:TextBox></td>
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
                        &nbsp;</div>
                </div>
           </td> 
        </tr>
    </table>
</div>
