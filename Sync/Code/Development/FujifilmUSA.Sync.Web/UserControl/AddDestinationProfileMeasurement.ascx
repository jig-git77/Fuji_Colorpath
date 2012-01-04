<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddDestinationProfileMeasurement.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.AddDestinationProfileMeasurement" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/MeasurementStatusbar.ascx" TagName="MeasurementStatusbar" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/AddMeasurementCommon.ascx" TagName="AddMeasurementCommon" TagPrefix="uc" %>

<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <h1>
            Add Destination Profile Measurement</h1>
             <asp:LinkButton ID="lbtnDestinationProfileSave" runat="server" CssClass="button"
            OnClick="lbtnDestinationProfileSave_Click">
            <span>
                <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/save_icon.png" />
                Save</span></asp:LinkButton>
        </div>
    <div class="clear">
    </div>
    <uc:MeasurementStatusbar ID="ucMeasurementStatusbar" runat="server" />    
    <uc:AddMeasurementCommon ID="ucAddMeasurementCommon" runat="server" Type="DestinationProfile" />
</div>
<div class="contant_bottom">
    &nbsp;</div>
<div class="clear">
</div>