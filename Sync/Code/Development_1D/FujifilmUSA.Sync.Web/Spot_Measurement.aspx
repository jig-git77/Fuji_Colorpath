<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Spot_Measurement.aspx.cs" Inherits="FujifilmUSA.Sync.Web.Spot_Measurement" EnableEventValidation="false" %>
<%@ Register Src="~/UserControl/Spot_Measurement.ascx" TagName="Spot_Measurement" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:Spot_Measurement ID="ucSpot_Measurement" runat="server" />
</asp:Content>
