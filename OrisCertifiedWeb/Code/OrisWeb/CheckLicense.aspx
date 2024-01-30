<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="CheckLicense.aspx.cs" EnableEventValidation="false" Inherits="OrisWeb.CheckLicense" %>
<%@ Register Src="~/Common/HomeMenu.ascx" TagName="HomeMenu" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script language="javascript" type="text/javascript">
   function pageLoad() {
            $addHandler($get("hideModalPopupViaClientButton"), 'click', hideModalPopupViaClient);
   }

   function hideModalPopupViaClient(ev) {
            ev.preventDefault();
            var modalPopupBehavior = $find('ModalPopupBehavior');
            modalPopupBehavior.hide();
   }

   function OpenMeasurementWindow() {
            if (screen.width <= 1024) {
                document.getElementById("<%=ModalPopup.ClientID %>").style.height = "630px";
            }
            var modalPopupBehavior = $find('ModalPopupBehavior');
            modalPopupBehavior.show();

   }    
</script>
<asp:Button runat="server" ID="TargetControlForModalPopup" Style="display: none" />
<ajax:modalpopupextender runat="server" ID="ModalPopup" BehaviorID="ModalPopupBehavior" Y="250"  TargetControlID="TargetControlForModalPopup" PopupControlID="pnlSiteLicense" BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="ModalPopupDragHandle"  >
</ajax:modalpopupextender>
<asp:Panel runat="server" ID="pnlSiteLicense" Style="display:none; height:400px; width: 750; background-color: White; border: solid 1px #000000;">
<a id="hideModalPopupViaClientButton" href="#" style="border: none; text-decoration: none;"><img src="Images/Close2.png" style="position: absolute; border: none; right: 10px;top: 9px; text-align: right;" /></a>          
          <table style="width: 500px" Style="background-color: White;border: solid 0.5px #000000;" cellpadding="5" cellspacing="0"> 
                                        <tr>
                                            <td valign="middle" align="left">
                                                    <table  CssClass="LicenseBox">
  <tr>
    <td class="green-title" valign="middle" align="left">You must agree to the following license.</td>
  </tr>
  <tr>
    <td valign="middle" align="left"><textarea name="LicenseText" cols="80"  rows="20" readonly="readonly" >1. Definitions
 
The terms &quot;reproduce,&quot; &quot;reproduction,&quot; &quot;derivative works,&quot; and &quot;distribution&quot; have the same meaning here as under U.S. copyright law.
 
A &quot;contribution&quot; is the original software, or any additions or changes to the software.
 
A &quot;contributor&quot; is any person that distributes its contribution under this license.
 
&quot;Licensed patents&quot; are a contributor's patent claims that read directly on its contribution.
 
2. Grant of Rights
 
(A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
 
(B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
 
3. Conditions and Limitations
 
(A) No Trademark License- This license does not grant you rights to use any contributor's name, logo, or trademarks.
 
(B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
 
(C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
 
(D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
 
(E) The software is licensed &quot;as-is.&quot; You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
</textarea></td>
  </tr>
</table>
                                        </td>
                                        </tr>
                                        <tr>
                                        <td> <div align="center">
      <asp:Button ID="OkButton" runat="server" Text="I Agree" CssClass="AgreeButton" OnClick="AgreeButton_Click" />&nbsp;&nbsp;
      <asp:Button ID="CancelButton" runat="server" Text="I Disagree" CssClass="AgreeButton"  OnClick="DisAgreeButton_Click" />
   </div></td>
                                        </tr>
                                        </table> <br />
  
                                  </asp:Panel>
    <table width="940px" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
            <td>
                <!--Menu Conttrol Will Load here-->
                <uc1:HomeMenu ID="HomeMenu1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="100" align="center" valign="bottom">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 100%;" align="center">
                            <a href="#" title="Taskero Universe">
                                <asp:Label ID="LblError" runat="server" CssClass="ERROR"></asp:Label></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
</asp:Content>

