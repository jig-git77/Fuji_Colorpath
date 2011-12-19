<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddMeasurement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddMeasurement" %>
<%@ Register src="~/UserControl/AddMeasurementCommon.ascx" TagName="AddMeasurementCommon" TagPrefix="uc" %>
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js" type="text/javascript"></script>
<script type="text/javascript">
jQuery(document).ready(function () {

        //Controlling Analyze button - If HasMeasurement = true do postback, else no postback
        jQuery('#<%= btnAnalyze.ClientID %>').click(function () {
            var hasMeasurementValue = jQuery('#<%= hdnHasMeasurement.ClientID %>').val();
            if (hasMeasurementValue == 'true')
                return true;
            else
                return false;
        });
         jQuery("[id*='btnRefresh']").click(function () {
            refreshTry++;
            ControlVisibility();
            if (refreshTry == 2) {
                jQuery("[id*='lblMessage']").html(jQuery("[id*='lblMessage']").text() + '<br>Hint: Having problem with plugins? Try to close and re-open your browser!</br>');
            }
        });
</script>
<div class="contant_top"></div>
<div class="contant_middle">
    <div class="head">
        <h1>
            Add Measurement</h1>
        <div class="Information_divAnalyze">
            <asp:LinkButton ID="btnAnalyze" runat="server" CssClass="button" >
                <span>
                    <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/analyze.png" />
                    Analyze</span>
            </asp:LinkButton>
        </div>
    </div>
  
    <div class="info_form">
        <table id="tblMessage" runat="server" width="100%" style="border: 1px solid #0C3D30;
            background-color: #6BB19C; color: Black; height: 45px;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="padding: 2px 2px 2px 5px; font-weight: bold;" valign="top">
                    <asp:Label ID="lblMessage" runat="server" Text="Status : Scanning device is not loaded."></asp:Label>
                </td>
                <td style="padding: 2px 5px 2px 2px; font-weight: bold;" align="right" valign="top">
                    <asp:Label ID="lblPluginMessage" runat="server" Text="loading..."></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <uc:AddMeasurementCommon ID="ucAddMeasurementCommon" runat="server" />
    <asp:HiddenField runat="server" ID="hdnHasMeasurement" />
</div>
<div class="contant_bottom">
    &nbsp;</div>
<div class="clear">
</div>
