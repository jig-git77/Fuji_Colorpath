<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddPrintStandardType.aspx.cs" Inherits="FujifilmUSA.Sync.Web.AddPrintStandardType" %>
<%@ Register Src="~/UserControl/AddPrintStandardType.ascx" TagName="AddPrintStandardType" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:AddPrintStandardType ID="ucAddPrintStandardType"  runat="server"/>
</asp:Content>

