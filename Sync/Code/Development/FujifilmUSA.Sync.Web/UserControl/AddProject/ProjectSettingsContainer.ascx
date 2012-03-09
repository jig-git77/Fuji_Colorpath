<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectSettingsContainer.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.ProjectSettingsContainer" %>
<%@ Reference Control="~/UserControl/AddProject/ProjectDescription.ascx" %>
<%@ Reference Control="~/UserControl/AddProject/TargetColorSpace.ascx" %>
<%@ Reference Control="~/UserControl/AddProject/TargetColorSpace.ascx" %>
<%@ Reference Control="~/UserControl/AddProject/ProfilerOptions.ascx" %>
<%@ Reference Control="~/UserControl/AddProject/MeasurementSetup.ascx" %>
<%@ Reference Control="~/UserControl/AddProject/SavePrintCondition.ascx" %>
<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <asp:PlaceHolder runat="server" ID="ph"></asp:PlaceHolder>
    <div class="clear" />
</div>
<div class="contant_bottom">
    &nbsp;</div>
