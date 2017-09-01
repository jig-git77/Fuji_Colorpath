<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="SpotMeasurement.aspx.cs" Inherits="FujifilmUSA.Sync.Web.SpotMeasurement" %>
<%@ Register Src="~/UserControl/SpotMeasurement.ascx" TagName="SpotMeasurement" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:SpotMeasurement ID="ucSpotMeasurement" runat="server" />
</asp:Content>
