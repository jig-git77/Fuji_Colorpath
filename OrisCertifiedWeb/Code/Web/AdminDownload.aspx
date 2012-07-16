<%@ Page Title="" Language="C#" MasterPageFile="~/Home.master" AutoEventWireup="true" CodeFile="AdminDownload.aspx.cs" Inherits="AdminDownload" %>
<%@ Register Src="Common/AdminMenu.ascx" TagName="AdminMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="940px" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
            <td>
                <table width="940" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <uc1:adminmenu id="UCAdminMenu" runat="server" />
                        </td>
                    </tr>
                    <tr>
                       <td>
                         
                          <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
                         <tr>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td></td><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /></td>         
            
                                </tr>
                                <tr id="TRContent" runat="server">
                                    <td  valign="top" align="center">
                                        <fieldset style="width:640px; border-right: #119c7a 1px solid; border-top: #119c7a 1px solid; border-left: #119c7a 1px solid; border-bottom: #119c7a 1px solid;">
                                        <legend  style="font-family:Tahoma; font-size:12px; color:#3a4800; margin-left: 15px;">Download</legend>
                                        <table width="640px" border="0" cellspacing="1" cellpadding="0"  >
                                            <tr>
                                                <td valign="middle" style="height: 32px;padding-left: 15px;" class=DownloadSTYLE1>
                                                    <a href="Download/TempRH%20Thum.zip">Temp RH Program </a> 
                                                </td>
                                            </tr>
                                            
                                            </table></fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table></td>
                    </tr>
                    <tr>
                        <td valign="middle">                           
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
