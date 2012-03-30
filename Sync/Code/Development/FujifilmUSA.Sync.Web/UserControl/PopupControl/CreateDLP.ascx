<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateDLP.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.CreateDLP" %>
<div style="display: block; margin: 10px;" class="contantbox tabcontant">
    <div class="section_top">
        &nbsp;</div>
    <div class="section_middle">
        <div class="combopart">
            <div style="padding: 5px;" id="cotainer">
                <div class="OpenPrintCondition">
                    <table width="100%">
                        <tr>
                            <td>
                                <div>
                                    <table>
                                        <tr>
                                            <td class="Content" style="line-height: 24px;">
                                                Creating DLP, Please wait...
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <table width="100%">
                                        <tr>
                                            <td class="Content" style="width: 100%; text-align: center; line-height: 24px;">
                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/processbar.gif" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <div style="float: left;">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/info_icon.png" />
                                </div>
                                <div style="float: left; padding-left: 5px; padding-top: 3px;">
                                    Once optimization is complete, apply DLP 000 to workflow and repaint the Sync 500
                                    patch chart.
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <div class="section_bottom">
        &nbsp;</div>
</div>
