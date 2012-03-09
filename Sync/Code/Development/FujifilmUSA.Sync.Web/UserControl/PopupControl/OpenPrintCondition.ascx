<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenPrintCondition.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.OpenPrintCondition" %>


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
            <div style="padding: 5px;" id="cotainer">
                <div class="OpenPrintCondition">
        <table>
            <tr>
                <td>
                    <div>
                        <table>
                            <tr>
                                <td class="Content" style="line-height:24px;"><asp:Label ID="lblOpenPrintConditionMessage" runat="server"></asp:Label>
                                </td>    
                            </tr>
                        </table>
                    </div>
                    <div class="clear">
                    </div>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <div class="buttonright" style=" padding-left: 3px">
                        <asp:LinkButton ID="lbtnClose" runat="server" 
                        OnClientClick="javascript: CloseOpenPrintConditionWindow(); return false;"
                            CssClass="gray_bt">
                                        <span>
                                            Close </span>
                        </asp:LinkButton>
                    </div>
                    <div class="buttonright">
                        <asp:LinkButton ID="lbtnContinue" runat="server" CssClass="gray_bt" 
                            OnClick="lbtnContinue_Click">
                <span> Continue </span>
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

