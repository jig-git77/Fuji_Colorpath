<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="AddDestinationProfileMeasurement.aspx.cs" Inherits="FujifilmUSA.Sync.Web.AddDestinationProfileMeasurement" %>
<%@ Register Src="~/UserControl/AddDestinationProfileMeasurement.ascx" TagName="AddDestinationProfileMeasurement" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc:AddDestinationProfileMeasurement ID="ucAddDestinationProfileMeasurement" runat="server" />
</asp:Content>
