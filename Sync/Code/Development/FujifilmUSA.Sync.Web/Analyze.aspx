<%@ Page Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="AddMeasurement.aspx.cs" 
    Inherits="FujifilmUSA.Sync.Web.AddMeasurement"
    MasterPageFile="~/MainMaster.Master" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/Analyze.ascx" TagName="Analyze" TagPrefix="uc" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <uc:Analyze ID="ucAnalyze" runat="server" />
</asp:Content>
