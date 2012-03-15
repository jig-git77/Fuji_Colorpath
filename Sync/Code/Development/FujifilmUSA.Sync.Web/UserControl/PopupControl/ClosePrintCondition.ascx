<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClosePrintCondition.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ClosePrintCondition" %>

  <script language="javascript" type="text/javascript">
      function CloseReOptimize() {
          var oWindow = GetProjectInformationRadWindow();
          GetProjectInformationRadWindow().close();
          //window.parent.location = "Measurement.aspx?ProjectName=''"
          return false;
      }
   
      function CloseClosePrintConditionWindow() {
          var oWindow = GetProjectInformationRadWindow();
          GetProjectInformationRadWindow().close();
          return false;
      }

      function GetProjectInformationRadWindow() {
          var oWindow = null;
          if (window.radWindow) oWindow = window.radWindow;
          else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
          return oWindow;
      }

</script>

<div style="display: block; margin: 10px;" class="contantbox tabcontant">
    <div class="section_top">
        &nbsp;</div>
    <div class="section_middle">
        <div class="combopart">
            <div style="padding: 5px;" id="cotainer">
                <div class="OpenPrintCondition">
                    <table>
                        <tr>
                            <td>
                                <div>
                                    <table>
                                        <tr>
                                            <td class="Content" style="line-height: 24px;">
                                                <asp:Label ID="lblClosePrintConditionMessage" runat="server"></asp:Label>
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
                                <div class="buttonleft" style="padding-left: 3px">
                                    <asp:LinkButton ID="lbtnReOptimize" runat="server" CssClass="gray_bt" OnClientClick="javascript: CloseReOptimize(); return true;" OnClick="lbtnReOptimize_Click">
                <span>Re Optimize</span>
                                    </asp:LinkButton>
                                </div>
                                <div class="buttonleft">
                                    <asp:LinkButton ID="lbtnDownLoadCurrentDLP" runat="server" CssClass="gray_bt" OnClick="lbtnDownLoadCurrentDLP_Click">
                    <span>DownLoad Current DLP</span>
                                    </asp:LinkButton>
                                </div>
                                <%--<div class="buttonleft">
                                    <asp:LinkButton ID="lbtnViewLastAnalysis" runat="server" CssClass="gray_bt">
                <span>View Last Analysis</span>
                                    </asp:LinkButton>
                                </div>--%>
                                <div class="buttonleft">
                                    <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript: CloseClosePrintConditionWindow(); return false;"
                                        CssClass="gray_bt">
                        <span>Close</span>
                                    </asp:LinkButton>
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
