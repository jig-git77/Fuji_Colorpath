<%@ Page Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="Measurement.aspx.cs" 
    Inherits="FujifilmUSA.Sync.Web.Measurement"
    MasterPageFile="~/MainMaster.Master" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/Measurement.ascx" TagName="Measurement" TagPrefix="uc" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <uc:Measurement ID="ucMeasurement" runat="server" />
</asp:Content>
