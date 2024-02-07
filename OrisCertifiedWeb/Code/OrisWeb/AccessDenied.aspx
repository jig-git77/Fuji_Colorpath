<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="OrisWeb.AccessDenied" %>

<%@ Register Src="~/Common/Menu.ascx" TagName="AdminMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="940" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <uc1:AdminMenu ID="UCAdminMenu" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="height: 20px;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="1" cellpadding="2">
                    <tr>
                        <td class="green-title" align="left">
                            Access Denied
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 10px;" align="center" class="green-form-bg">
                            <fieldset>
                                <legend>Access Denied</legend>
                                <img src="Images/AccessDenied.jpg" alt="Fuji Films" />
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>    
    <asp:Label ID="LblError" runat="server" CssClass="ERROR"></asp:Label>
</asp:Content>