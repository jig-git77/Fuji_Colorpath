<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SyncRunExport.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.SyncRunExport" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="uc" %>

<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <h1>
            Export</h1>
        <div class="Information_divAnalyze">
            <telerik:RadButton ID="btnCloseRun" Text="Close Run" runat="server" OnClick="btnCloseRun_Click">
            </telerik:RadButton>
            <telerik:RadButton ID="btnExport" Text="Export" runat="server" OnClick="btnExport_Click">
            </telerik:RadButton>
        </div>
    </div>
    <div class="clear">
    </div>
    <div class="ContainerClass">
       <div class="info_form" style="margin : 0 20px 0" >
            <uc:Statusbar ID="ucStatusbar1" runat="server" Visible="false" />
        </div>

    </div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
