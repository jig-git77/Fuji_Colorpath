<%@ Page Title="Import Measurements" Language="C#" MasterPageFile="~/PopUpMaster.Master" AutoEventWireup="true" CodeBehind="PopupImportMeasurements.aspx.cs" Inherits="FujifilmUSA.Sync.Web.PopupImportMeasurements" %>

<%@ Register Src="~/UserControl/PopupControl/ImportMeasurements.ascx" 
    TagName="ImportWindow" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <uc:ImportWindow ID="ucImportWindow" runat="server" />
</asp:Content>
