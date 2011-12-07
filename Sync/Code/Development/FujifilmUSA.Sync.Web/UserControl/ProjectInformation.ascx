<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectInformation.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.ProjectInformation" %>
 <script language="javascript" type="text/javascript">
     function CloseProjectInformation() {
         var oWindow = GetProjectInformationRadWindow();
         GetProjectInformationRadWindow().close();
         return false;
     }

     function GetProjectInformationRadWindow() {
         var oWindow = null;
         if (window.radWindow) oWindow = window.radWindow;
         else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
         return oWindow;
     } 
</script>
<asp:Button ID="btnCloseWindow" runat="server" Text="Close" OnClientClick="javascript: CloseProjectInformation();"/>