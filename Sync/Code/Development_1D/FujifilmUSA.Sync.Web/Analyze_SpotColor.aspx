<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Analyze_SpotColor.aspx.cs" Inherits="FujifilmUSA.Sync.Web.Analyze_SpotColor" %>
<%@ Register Src="~/UserControl/Analyze_SpotColor.ascx" TagName="Analyze_SpotColor" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <uc:Analyze_SpotColor ID="ucAnalyze_SpotColor" runat="server" />
</asp:Content>
