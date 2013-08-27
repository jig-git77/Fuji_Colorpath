<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InkOptSourceMiddle.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.InkOptSourceMiddle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/PopupControl/CreateDLP.ascx" TagName="CreateDLP" TagPrefix="uc" %>

 <script type="text/javascript">
     var previewImageCodedString = '';
     var prm = Sys.WebForms.PageRequestManager.getInstance();
     prm.add_initializeRequest(InitializeRequest);

     function InitializeRequest(sender, args) {

         var updateProgress = $get('<%= UpdateProgressClientID %>');
         var postBackElement = args.get_postBackElement();
         if (postBackElement.id == '<%= lbtnDownloadAndSaveDLP.ClientID %>') {

             updateProgress.control._associatedUpdatePanelId = 'dummyId';
         }
         else {

             updateProgress.control._associatedUpdatePanelId = null;
         }
     }
</script>
<script type="text/javascript" language="javascript">

    function ChooseRadWindow() {
        return true;
    }
</script>

<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">
        <asp:UpdatePanel ID="upCreateDLP" runat="server">
            <ContentTemplate>
                <div class="head">
                    <h1 id="header" runat="server">
                    </h1>
                    <div class="Information_divAnalyze">
                        <asp:LinkButton ID="lbtnDownloadAndSaveDLP" runat="server" CssClass="button" OnClick="lbtnDownloadAndSaveDLP_Click"
                            OnClientClick="return ChooseRadWindow();">
                            <span>
                                <asp:Image ID="Image2" runat="server" ImageUrl="../Images/export.png" meta:resourcekey="Image2Resource1" />
                                <asp:Localize runat="server" Text="Create DLP" ID="locCreateDLP" meta:resourcekey="locCreateDLPResource1"></asp:Localize>
                            </span>
                        </asp:LinkButton>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</div>
<div class="contant_bottom">
    &nbsp;</div>
 
 <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="upCreateDLP"
        DisplayAfter="50">
        <ProgressTemplate>
            <AjaxToolkit:AlwaysVisibleControlExtender ID="AlwaysVisibleControl" HorizontalSide="Center"
                VerticalSide="Middle" TargetControlID="updateProgress" HorizontalOffset="325"
                VerticalOffset="210" runat="server" Enabled="True">
            </AjaxToolkit:AlwaysVisibleControlExtender>
            <div style="background: #CCC !important;">
                <uc:CreateDLP runat="server" ID="ucCreateDLP" />
            </div>
            <div class="UpdateProgressBackgroundLessOptimize" style="position: fixed; left: 0px;
                top: 0px; z-index: 1000; width: 100%; height: 100%;" unselectable="on">
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

