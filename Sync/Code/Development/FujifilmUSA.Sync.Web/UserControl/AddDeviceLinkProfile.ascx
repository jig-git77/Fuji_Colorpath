<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddDeviceLinkProfile.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AddDeviceLinkProfile" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">

    <div class="head">
        <h1>
            Add Device Link Profile</h1>
        <asp:LinkButton ID="lbtnDeviceLinkProfileSave" runat="server" CssClass="button" 
            onclick="lbtnDeviceLinkProfileSave_Click" ValidationGroup ="vgDLP">
            <span>
                <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/save_icon.png" />
                Save</span></asp:LinkButton></div>
    <div class="clear">
    </div> 
    <div id="validationSummary" class="add_form" >
        <asp:ValidationSummary 
                ID="vsDeviceLinkProfile" 
                runat="server" 
                ShowSummary="true" 
                ValidationGroup="vgDLP" 
                ForeColor="Red"
                >
         </asp:ValidationSummary>
    </div>   
    <div class="add_form">
        <center>
            <ucStatusbar:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
        </center>
        <table border="0" cellspacing="5" cellpadding="0" class="Width100percent">
            <tr>
                <td class="ProjectListtd1">
                    <asp:Label ID="lblDeviceLinkProfileName" runat="server" Text="Name" ></asp:Label>
                    &nbsp; :
                </td>
               <td>
                   <asp:TextBox ID="txtDeviceLinkProfileName" runat="server" Width="178px" Height="19px"></asp:TextBox>
                   <asp:RequiredFieldValidator 
                        ID="rfvDeviceLinkProfileName" 
                        runat="server" 
                        Display="None"
                        ControlToValidate="txtDeviceLinkProfileName" 
                        ErrorMessage = "Please provide DLP name." 
                        ValidationGroup="vgDLP" ></asp:RequiredFieldValidator>
                </td>               
                  <td class="ProjectListtd1">
                    Profiler Option &nbsp; :
                </td>
                <td>
                    <asp:DropDownList ID="cmbProfilerOption" runat="server" Width="180px" CssClass="select">
                    </asp:DropDownList>
                </td>
            </tr>                     
            <tr>
                <td class="ProjectListtd1"><asp:Label ID="Label1" runat="server" Text="Select Source" ></asp:Label>&nbsp; :
                </td>
                <td >
                <%--<telerik:RadComboBox ID="cmbSourceProfile" runat="server" DataValueField="Key" DataTextField="Value" SkinID="width200px"  AutoPostBack="true" ></telerik:RadComboBox>--%>
                <asp:DropDownList ID="cmbSourceProfile" runat="server" DataValueField="Key" DataTextField="Value"  width="180px" CssClass="select"></asp:DropDownList>
                 <asp:RequiredFieldValidator 
                        ID="rfvSourceProfile" 
                        runat="server" 
                        Display="None"
                        ControlToValidate="cmbSourceProfile" 
                        InitialValue=""
                        ErrorMessage = "Please provide source profile." 
                        ValidationGroup="vgDLP" ></asp:RequiredFieldValidator>
                </td>
                <td class="ProjectListtd1"><asp:Label ID="Label2" runat="server" Text="Select Destination" ></asp:Label>&nbsp; :
                </td>
                <td>
                <%--<telerik:RadComboBox ID="cmbDestinationProfile" DataValueField="Key" DataTextField="Value" runat="server" SkinID="width200px" AutoPostBack="true" ></telerik:RadComboBox>--%>
                <asp:DropDownList ID="cmbDestinationProfile" runat="server" DataValueField="Key" DataTextField="Value"  width="180px" CssClass="select"></asp:DropDownList>
                <asp:RequiredFieldValidator 
                        ID="rfvDestinationProfile" 
                        runat="server" 
                        Display="None"
                        ControlToValidate="cmbDestinationProfile" 
                        InitialValue=""
                        ErrorMessage = "Please provide destination profile." 
                        ValidationGroup="vgDLP" ></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </div>        
</div>

<div class="contant_bottom">
    &nbsp;</div>
<div class="clear">
</div>
