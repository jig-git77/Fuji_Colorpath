<%@ Page Title="" Language="C#" MasterPageFile="~/SyncPopup.master" AutoEventWireup="true" CodeFile="SyncAddPressRunPopup.aspx.cs" Inherits="SyncAddPressRunPopup" EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="green-title">
                Add Press Run
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:Label ID="LblError" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table cellpadding="0" cellspacing="4" border="0" width="95%">                   
                      <tr>
                        <td class="FORM-TEXT-BLACK">
                            Press Run Name :
                        </td>
                        <td>
                            <asp:TextBox ID="TxtPressRunName" runat="server" CssClass="TEXTBOX"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>                        
                        <td>
                            <asp:HiddenField ID="ClientDatatime" runat="server" />
                        </td>
                    </tr>                    
                    <tr><td colspan="2">&nbsp;</td></tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="BTN_SavePressRun" runat="server" CssClass="BUTTON" 
                                Text="Save Press Run" OnClientClick="return GetClientDate();" onclick="BTN_SavePressRun_Click" />                                
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
     <script type='text/javascript' language='javascript'>
         function GetClientDate() {
             var dt = new Date();
             var tzo = dt.toDateString() + ' ' + dt.getHours() + ':' + dt.getMinutes() + ':' + dt.getSeconds();
             document.getElementById('<%= ClientDatatime.ClientID %>').value = tzo.toString();
             return true;
         }
     </script>
            
</asp:Content>

