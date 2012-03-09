<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Analyze.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.Analyze" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="uc" %>
<script type="text/javascript" language="javascript">
    function ConfirmMeasurement() {
        if (confirm('<%= AddMeasurement_DeleteMeasurement_Confirmation %>'))
            return true;
        else
            return false;
    }
</script>

<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <h1>
            Analyze</h1>
        <div class="Information_divAnalyze">
            <%--<asp:LinkButton ID="btnOptimize" runat="server"  CssClass="button" 
                onclick="btnOptimize_Click">
                <span>
                    <asp:Image ID="Image2" runat="server" ImageUrl="../Images/export.png" />
                   Optimize Profile</span> &nbsp; 
            </asp:LinkButton>--%>
        </div>
    </div>
    <div class="clear">
    </div>

    <div class="ContainerClass">
        <div class="info_form" style="margin : 0 20px 0" >
            <uc:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
        </div>
        <div class="color_pallate">
            <div class="color_pallate_head">
            Analyze Measurement
            </div>
            <div class="color_pallate_middle">
                <div class="color_bar">
                </div>
            </div>
            <div class="color_pallate_bottom">
                &nbsp;</div>
        </div>
        <div class="color_pallate">
            <div class="color_pallate_head">Chart for Analyze
            </div>
            <div class="color_pallate_middle">
                <div class="color_bar">
                    <div id="Div1" style="position: relative; height: 460px; width: 630px; overflow: visible;">
                    </div>
                </div>
            </div>
            <div class="color_pallate_bottom">
                &nbsp;</div>
        </div>
    </div>
</div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
