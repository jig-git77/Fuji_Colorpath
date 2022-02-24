<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="UpdateChangeLogManagement.aspx.cs" Inherits="FujifilmUSA.Sync.Web.UpdateChangeLogManagement" %>
<%@ Register Src="~/UserControl/UpdateChangeLogManagement.ascx" TagName="UpdateChangeLogManagement" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <uc:UpdateChangeLogManagement ID="ucUpdateChangeLogManagement"  runat="server"/>
</asp:Content>

