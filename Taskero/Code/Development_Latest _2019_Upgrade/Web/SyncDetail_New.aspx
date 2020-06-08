<%@ Page Title="" Language="C#" MasterPageFile="~/Home.master" AutoEventWireup="true" CodeFile="SyncDetail_New.aspx.cs" Inherits="SyncDetail_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
    <table width="940px" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="23" height="36" valign="top">
                            <img src="images/menu-curve-left.png" alt="FujiFilm" border="0" />
                        </td>
                        <td valign="top" class="content-menu-top">
                            <b>Sync</b>
                        </td>
                        <td width="23" valign="top">
                            <img src="images/menu-curve-right.png" alt="FujiFilm" border="0" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" class="SYNCTABLE">
                                <tr>
                                    <td class="green-title">
                                        &nbsp; Sync
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 500px;" valign="top">
                                        <table width="100%" style="height: 570px;" border="0" cellpadding="0" cellspacing="5"
                                            class="SYNCTABLE">
                                            <tr>
                                                <td style="width: 80%" valign="top">
                                                    <%--Main Body--%>
                                                    <table id="TblPress" runat="server" border="0" width="100%" style="height: 640px;"
                                                        cellpadding="0" cellspacing="0" class="TABLE1">
                                                        <tr>
                                                            <td valign="top" style="padding:10px;">
                                                               <b>
                                                                Taskero SYNC is now ColorPath SYNC!
                                                                <br /><br />
                                                                We've migrated SYNC features to a new website. Please call 1-800-FLY-FUJI (1-800-359-3854) to make the move to ColorPath SYNC.
                                                               </b>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
  
</asp:Content>

