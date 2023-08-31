<%@ Page Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="Analyze_DestinationProfile.aspx.cs" 
    Inherits="FujifilmUSA.Sync.Web.Analyze"
    MasterPageFile="~/MainMaster.Master" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/Analyze_DestinationProfile.ascx" TagName="Analyze_DestinationProfile" TagPrefix="uc" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <uc:Analyze_DestinationProfile ID="ucAnalyze_DestinationProfile" runat="server" />
</asp:Content>
