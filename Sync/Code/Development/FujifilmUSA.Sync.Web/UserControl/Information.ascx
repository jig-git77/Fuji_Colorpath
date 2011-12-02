<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Information.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.Information" %>
<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <h1>
            Information</h1>
        <%--<a href="#" class="button"><span>Analyze</span></a>--%>
        <div class="Information_divAnalyze">
            <telerik:RadButton ID="btnAnalyze" Text="Analyze" runat="server">
            </telerik:RadButton>
        </div>
    </div>
    <div class="clear">
    </div>
    <div class="info_form">
        <div class="float_l InformationtxtDivWidth">
            <asp:Label ID="lblMeasurementName" runat="server" Text="Measurement Name" CssClass="LabelTitle"></asp:Label>
            &nbsp;
            <telerik:RadTextBox ID="txtMeasurementName" runat="server" SkinID="width250px">
            </telerik:RadTextBox>
        </div>
        <div class="float_r">
            <telerik:RadButton ID="btnCalibrate" runat="server" Text="Calibrate">
            </telerik:RadButton>
        </div>
        <div class="clear">
        </div>
        <div class="float_l">
            <telerik:RadButton ID="btnMeasureOrUpload" runat="server" Text="Measure / Upload">
            </telerik:RadButton>
        </div>
        <div class="float_r">
            <telerik:RadButton ID="btnCancelMeasurement" runat="server" Text="Cancel Measurement">
            </telerik:RadButton>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="color_pallate">
        <div class="color_pallate_head">
            Colorbar</div>
        <div class="color_pallate_middle">
            <div class="color_bar">
                Color bar comes here
            </div>
        </div>
        <div class="color_pallate_bottom">
            &nbsp;</div>
    </div>
    <div class="color_pallate">
        <div class="color_pallate_head">
            Recent Upload</div>
        <div class="color_pallate_middle">
            List comes here
        </div>
        <div class="color_pallate_bottom">
            &nbsp;</div>
    </div>
    <div class="clear">
    </div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
