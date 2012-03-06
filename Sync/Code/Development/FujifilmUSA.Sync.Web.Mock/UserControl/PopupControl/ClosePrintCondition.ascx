<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClosePrintCondition.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.Mock.UserControl.ClosePrintCondition" %>
<div style="display: block; margin:10px;"  class="contantbox tabcontant">
    <div class="section_top">
        &nbsp;</div>
    <div class="section_middle">
        <div class="combopart">
          <%--  <div class="header">
                <div class="content">
                    <div style="float: left;" class="text">
                        
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>--%>
            <div style="padding: 5px;" id="ContentPlaceHolder1_ucAddProject_ucMeasurementSetup_panelSpectrophotometerOptions">
                <table>
                    <tbody>
                        <tr >
                            <td colspan="4">
                                <asp:Label ID="lblClosePrintConditionMessage" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton ID="lbtnReOptimize" runat="server" CssClass="gray_bt">
                <span>Re Optimize</span>
                                    </asp:LinkButton>
                            </td>
                            <td>
                             <asp:LinkButton ID="lbtnDownLoadCurrentDLP" runat="server" CssClass="gray_bt">
                    <span>DownLoad Current DLP</span>
                                    </asp:LinkButton>
                            </td>
                            <td>
                               <asp:LinkButton ID="lbtnViewLastAnalysis" runat="server" CssClass="gray_bt">
                <span>View Last Analysis</span>
                                    </asp:LinkButton>
                            </td>
                            <td>
                               <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript: CloseClosePrintConditionWindow(); return false;"
                                            CssClass="gray_bt">
                        <span>Close</span>
                                        </asp:LinkButton>
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
