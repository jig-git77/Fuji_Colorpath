<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginControl.ascx.cs" Inherits="Common_LoginControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<script language="javascript" type="text/javascript">
   //========== Function used to call login process on pressing Enter key ===========//
   function CheckOnEnter(_event,_postbackfor)
   {
        //========== Check Key Code for to check event ===========//
        key = (window.event) ? _event.keyCode : _event.which;
        
        //========== Check key press event and if pressed key is enter than call login process ===========//
        if (key == 13)
        {
            //========== Call login function ===========//
            if(_postbackfor == 1)
                __doPostBack('<%=LBTN_Login1.UniqueID %>','');
            //========== Call forgot password function ===========//
            if(_postbackfor == 2)
                __doPostBack('<%=LBTN_ForgotPassword1.UniqueID %>','');
        }
   }
</script>



<asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>

<table id="TBL_Login" runat="server" border="0" cellspacing="1" cellpadding="4">
    <tr>
        <td colspan="2">
            &nbsp;<asp:Label ID="LblError1" runat="server" CssClass="ERROR"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Literal ID="LTRL_LoginUserName" runat="server"></asp:Literal><span class="REQ">*</span>
        </td>
        <td>
            <asp:TextBox ID="TXT_LoginUserName" runat="server" CssClass="TEXTBOX1"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Literal ID="LTRL_LoginPassword" runat="server"></asp:Literal><span class="REQ">*</span>
        </td>
        <td>
            <asp:TextBox ID="TXT_LoginPassword" runat="server" CssClass="TEXTBOX1" TextMode="Password"
                AutoPostBack="false"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:CheckBox ID="CHK_RememberMe" runat="server" CssClass="CHECKBOX" Text="Remember me!" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <div class="green1-buttonwrapper">
                <asp:LinkButton ID="LBTN_Login1" runat="server" CssClass="LINKBUTTON" Text="<span>Login</span>"
                    OnClick="LBTN_Login1_Click"></asp:LinkButton></div>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:LinkButton ID="LBTN_ForgotPassword" runat="server" OnClick="LBTN_ForgotPassword_Click"
                CssClass="LINKBUTTON2"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
</table>
<table id="TBL_ForgotPassword" runat="server" border="0" cellspacing="1" cellpadding="4">
    <tr>
        <td colspan="2">
            &nbsp;<asp:Label ID="LblError2" runat="server" CssClass="ERROR"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Literal ID="LTRL_ForgotPasswordUserName" runat="server"></asp:Literal><span
                class="REQ">*</span>
        </td>
        <td>
            <asp:TextBox ID="TXT_ForgotPasswordUserName" runat="server" CssClass="TEXTBOX1"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Literal ID="LTRL_ForgotPasswordEmail" runat="server"></asp:Literal><span class="REQ">*</span>
        </td>
        <td>
            <asp:TextBox ID="TXT_ForgotPasswordEmail" runat="server" CssClass="TEXTBOX1" AutoPostBack="false"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <div class="green1-buttonwrapper">
                <asp:LinkButton ID="LBTN_ForgotPassword1" runat="server" CssClass="LINKBUTTON" Text="<span>Forgot Password</span>"
                    OnClick="LBTN_ForgotPassword1_Click"></asp:LinkButton></div>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:LinkButton ID="LBTN_Login" runat="server" OnClick="LBTN_Login_Click" CssClass="LINKBUTTON2"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
</table>
 
</ContentTemplate>
</asp:UpdatePanel>
