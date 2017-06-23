<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="PatchPresetManagement.aspx.cs" Inherits="FujifilmUSA.Sync.Web.PatchPresetManagement" %>
<%@ Register Src="~/UserControl/PatchPresetManagement.ascx" TagName="PatchPresetManagement" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc:PatchPresetManagement ID="ucPatchPresetManagement"  runat="server"/>
</asp:Content>
