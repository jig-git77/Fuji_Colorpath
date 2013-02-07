<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="DownloadSoftwares.aspx.cs" Inherits="FujifilmUSA.Sync.Web.DownloadSoftwares" %>

<%@ Register Src="~/UserControl/DownloadSoftwares.ascx" TagName="DownloadSoftwares" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:DownloadSoftwares ID="ucDownloadSoftwares"  runat="server"/>
</asp:Content>
