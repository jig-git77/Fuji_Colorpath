<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" 
CodeBehind="Permission.aspx.cs" 
Inherits="FujifilmUSA.Sync.Web.Permission" %>
<%@ Register Src="~/UserControl/Permission.ascx" TagName="Permission" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc:Permission ID="ucPermission"  runat="server"/>
</asp:Content>
