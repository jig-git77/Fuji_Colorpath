<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddMeasurement.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddMeasurement" %>
<%@ Register src="~/UserControl/AddMeasurementCommon.ascx" TagName="AddMeasurementCommon" TagPrefix="uc" %>
<%@ Register src="~/UserControl/MeasurementStatusbar.ascx" TagName="MeasurementStatusbar" TagPrefix="uc" %>
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js" type="text/javascript"></script>
<script type="text/javascript">
    jQuery(document).ready(
    function () {

        //Controlling Analyze button - If HasMeasurement = true do postback, else no postback
        jQuery('#<%= btnAnalyze.ClientID %>').click(
            function () {
                var hasMeasurementValue = jQuery('#<%= hdnHasMeasurement.ClientID %>').val();
                if (hasMeasurementValue == 'true')
                    return true;
                else
                    return false;
            });
    });
</script>
<div class="contant_top"></div>
<div class="contant_middle">
    <div class="head">
        <h1>
            Add Measurement</h1>
        <div class="Information_divAnalyze">
            <asp:LinkButton ID="btnAnalyze" runat="server" CssClass="button" 
                onclick="btnAnalyze_Click" >
                <span>
                    <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/analyze.png" />
                    Analyze</span>
            </asp:LinkButton>
        </div>
    </div>
  
    
    <uc:MeasurementStatusbar ID="ucMeasurementStatusbar" runat="server" />
    <uc:AddMeasurementCommon ID="ucAddMeasurementCommon" runat="server" Type="Optimization" />    
    <asp:HiddenField runat="server" ID="hdnHasMeasurement" />
</div>
<div class="contant_bottom">
    &nbsp;</div>
<div class="clear">
</div>
