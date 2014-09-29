<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="FujifilmUSA.Sync.Web.AccessDenied" %>
<%@ Register Src="~/UserControl/AccessDenied.ascx" TagName="AccessDenied" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc:AccessDenied ID="ucAccessDenied"  runat="server"/>
</asp:Content>
