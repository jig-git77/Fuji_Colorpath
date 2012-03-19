<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Measurement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.Measurement" %>
<%@ Register Src="~/UserControl/MeasurementCommon.ascx" TagName="MeasurementCommon"
    TagPrefix="uc" %>
<%--<script type="text/javascript">
    jQuery(document).ready(
    function () {

        //Controlling Analyze button - If HasMeasurement = true do postback, else no postback
        jQuery('#<%= btnAnalyze.ClientID %>').click(
            function () {
                return true;
                //                if (jQuery("input[id*='hdHasMeasurement']").val() > 0)
                //                    return true;
                //                else
                //                    return false;
            });
    });
</script>--%>
<div class="contant_top">
</div>
<div class="contant_middle">
    <div class="head">
        <h1 id="header" runat="server">
            Measurement</h1>
        <div class="Information_divAnalyze">
            <asp:LinkButton ID="btnAnalyze" runat="server" CssClass="button" OnClick="btnAnalyze_Click">
                <span>
                    <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/analyze.png" />
                    Analyze</span>
            </asp:LinkButton>
        </div>
    </div>
    <div class="mesurementStatusbar info_form">
        <div class="top">
            &nbsp;</div>
        <div class="middle" runat="server" id="tblMessage">
            <asp:Label ID="lblMessage" runat="server" Text="Status : Scanning device is not loaded."
                Style="font-weight: bold; padding-left: 5px;"></asp:Label>
            <asp:Label ID="lblPluginMessage" runat="server" Text="loading..." Style="float: right;
                font-weight: bold; padding-right: 5px;"></asp:Label>
            &nbsp;
            <br />
        </div>
        <div class="bottom">
            &nbsp;</div>
    </div>
    <uc:MeasurementCommon ID="ucMeasurementCommon" runat="server" Type="Optimization" />
</div>
<div class="contant_bottom">
    &nbsp;</div>
<div class="clear">
</div>
