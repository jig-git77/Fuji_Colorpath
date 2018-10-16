<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddReferenceChartPreset.aspx.cs" Inherits="FujifilmUSA.Sync.Web.AddReferenceChartPreset" %>

<%@ Register Src="~/UserControl/AddReferenceChartPreset.ascx" TagName="AddReferenceChartPreset"
    TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:AddReferenceChartPreset ID="ucAddReferenceChartPreset" runat="server" />
</asp:Content>
