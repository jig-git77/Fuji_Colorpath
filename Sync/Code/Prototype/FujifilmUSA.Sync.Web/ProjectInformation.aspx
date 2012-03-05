<%@ Page Title="" Language="C#" MasterPageFile="~/PopUpMaster.Master" AutoEventWireup="true" CodeBehind="ProjectInformation.aspx.cs" Inherits="FujifilmUSA.Sync.Web.ProjectInformation" %>
<%@ Register Src="~/UserControl/ProjectInformation.ascx" TagName="ProjectInformation" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:ProjectInformation ID="ucProjectInformation" runat="server" />
</asp:Content>
