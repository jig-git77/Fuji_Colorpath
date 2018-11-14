<%@ Page Title="" Language="C#" MasterPageFile="~/PopUpMaster.Master" AutoEventWireup="true" CodeBehind="PopupImportMeasurementFile.aspx.cs" Inherits="FujifilmUSA.Sync.Web.PopupImportMeasurementFile" %>
<%@ Register Src="~/UserControl/PopupControl/ImportMeasurementFile.ascx" 
    TagName="ImportWindow" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:ImportWindow ID="ucImportWindow" runat="server" />
</asp:Content>
