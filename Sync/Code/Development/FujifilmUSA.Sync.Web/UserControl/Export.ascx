<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Export.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.Export" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<div class="contant_top">
</div>
<div class="contant_middle">
    <div class="head">
        <h1 id="header" runat="server">
            Export</h1>
        <div class="Information_divAnalyze">
            <asp:LinkButton ID="lbtnMeasure" runat="server" CssClass="button" OnClick="lbtnMeasure_Click">
                <span>
                    <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/analyze.png" />
                    Measure</span>
            </asp:LinkButton>
        </div>
    </div>
    <div class="listDevice" style="margin-left: 15px;">
        <table cellpadding="0" cellspacing="5" border="0">
            <tr id="tr0">
                <td>
                    <ucStatusbar:Statusbar ID="fujiStatusbar" runat="server" Visible="false" />
                </td>
            </tr>
            <tr id="tr1">
                <td>
                    <div style="display: block;" class="contantbox tabcontant">
                        <div class="section_top">
                            &nbsp;</div>
                        <div class="section_middle">
                            <div class="combopart">
                                <div class="header">
                                    <div class="content">
                                        <div class="text" style="float: left">
                                            Download DLP and Chart
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                                <div style="border-style: None; padding: 5px">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <span>Download DLP and Implement in Workflow :</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lbtnDownLoadDLP" runat="server" CssClass="gray_bt" OnClick="lbtnDownLoadDLP_Click">
                                                    <span>Download DLP</span>
                                                    </asp:LinkButton>
                                                    <%--<asp:LinkButton ID="lbtnDLPFileName" runat="server">
                                                    <span>Komori LS 40 test DLP 000.icc</span>
                                                    </asp:LinkButton>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span>Download Chart and Print through DLP Workflow :</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lbtnDownLoadChart" runat="server" CssClass="gray_bt" OnClick="lbtnDownLoadChart_Click">
                                                    <span>Download Chart</span>
                                                    </asp:LinkButton>
                                                    <%--<asp:LinkButton ID="lbtnChartFileName" runat="server">
                                                    <span>Sync 500</span>
                                                    </asp:LinkButton>--%>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </div>
                        <div class="section_bottom">
                            &nbsp;</div>
                    </div>
                </td>
            </tr>
            <tr id="tr2">
                <td>
                    <div style="display: block;" class="contantbox tabcontant">
                        <div class="section_top">
                            &nbsp;</div>
                        <div class="section_middle">
                            <div class="combopart">
                                <div class="header">
                                    <div class="content">
                                        <div class="text" style="float: left">
                                            Advanced Downloads
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                                <div style="border-style: None; padding: 5px">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <span>Download Source ICC:</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lbtnDownloadSourceICC" runat="server" CssClass="gray_bt" OnClick="lbtnDownloadSourceICC_Click">
                                                    <span>Download Source ICC</span>
                                                    </asp:LinkButton>
                                                    <%--<asp:LinkButton ID="lbtnDownloadSourceICCFileName" runat="server">
                                                    <span>Komori LS 40 test.icc</span>
                                                    </asp:LinkButton>--%>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </div>
                        <div class="section_bottom">
                            &nbsp;</div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
<div class="clear">
</div>
