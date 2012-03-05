<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddProject.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.Mock.UserControl.AddProject" %>
<%@ Register Src="~/UserControl/AddProject/AddProjectDescription.ascx" TagName="AddProjectDescription" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/AddProject/TargetColorSpace.ascx" TagName="TargetColorSpace" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/AddProject/ProfilerOptions.ascx" TagName="ProfilerOptions" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/AddProject/MeasurementSetup.ascx" TagName="MeasurementSetup" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/AddProject/SavePrintCondition.ascx" TagName="SavePrintCondition" TagPrefix="uc" %>
<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <h1 id="hTitle" runat="server">New Print Condition - Description</h1>
          <asp:LinkButton ID="lbtnCreateNewPrintCondition" runat="server" CssClass="button" 
                        ValidationGroup="vgDestination"
            onclick="lbtnCreateNewPrintCondition_Click">
        <span >
            <asp:Image ID="imgAdd" runat="server" ImageUrl="../../Images/new_project_icon.png" />
            Create New Print Condition</span></asp:LinkButton>
    </div>
    <div class="clear">
    </div>
       <uc:AddProjectDescription ID="ucAddProjectDescription" runat="server" />
       <uc:TargetColorSpace ID="ucTargetColorSpace" runat="server" />
       <uc:ProfilerOptions ID="ucProfilerOptions" runat="server" />
       <uc:MeasurementSetup ID="ucMeasurementSetup" runat="server" />
       <uc:SavePrintCondition ID="ucSavePrintCondition" runat="server" />
   
    <div class="clear">
    </div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
