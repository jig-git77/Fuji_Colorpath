<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="MToolVersionManagement.aspx.cs" Inherits="FujifilmUSA.Sync.Web.MToolVersionManagement" %>
<%@ Register Src="~/UserControl/MToolVersionManagement.ascx" TagName="MToolVersionManagement" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <uc:MToolVersionManagement ID="ucMToolVersionManagement"  runat="server"/>
</asp:Content>
