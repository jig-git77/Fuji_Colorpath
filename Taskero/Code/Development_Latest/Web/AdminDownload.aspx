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
                                                    <a href="Download/cpv/mac/Taskero%20CPV%20Installer%20(Mac).zip">Colorpath Verified (CPV) Installer - Mac</a> 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle" style="height: 30px;padding-left: 15px;" class=DownloadALTERNATINGSTYLE1>
                                                    <a href="Download/cpv/pc/Taskero%20CPV%20Installer%20(PC).zip">Colorpath Verified (CPV) Installer - PC</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle" style="height: 30px;padding-left: 15px;" class=DownloadSTYLE1>
                                                    <a href="Download/er/mac/Taskero%20Display%20Installer%20(Mac).zip">Taskero Display (ER) Installer - Mac</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle" style="height: 30px;padding-left: 15px;" class=DownloadALTERNATINGSTYLE1>
                                                    <a href="Download/er/pc/Taskero%20Display%20Installer%20(PC).zip">Taskero Display (ER) Installer - PC</a>
                                                </td>
                                            </tr>
                                              <tr>
                                                <td valign="middle" style="height: 30px;padding-left: 15px;" class=DownloadSTYLE1>
                                                    <a href="Download/temprh/pc/Taskero%20Temp%20RH%20Installer%20(PC).zip">TempRH Installer - PC</a>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td valign="middle" style="height: 30px;padding-left: 15px;" class=DownloadALTERNATINGSTYLE1>
                                                    <a href="Download/ColorpathXF/ColorpathXFSetup.zip">ColorpathXF Installer</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle" style="height: 30px;padding-left: 15px;" class=DownloadSTYLE1>
                                                    <a href="Download/CTWeb/TaskeroCTWebSetup.zip">Taskero ColorTuner</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle" style="height: 30px;padding-left: 15px;" class=DownloadALTERNATINGSTYLE1>
                                                    <a href="Download/Colorpath_MacPlugin_Installer.zip">ColorPath MacPlugin</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle" style="height: 30px;padding-left: 15px;" class=DownloadSTYLE1>
                                                    <a href="Download/Colorpath_PCPlugin_Installer.zip">ColorPath PCPlugin</a>
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
