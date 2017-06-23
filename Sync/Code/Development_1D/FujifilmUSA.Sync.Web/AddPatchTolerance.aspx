<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddPatchTolerance.aspx.cs" Inherits="FujifilmUSA.Sync.Web.AddPatchTolerance" %>
<%@ Register Src="~/UserControl/AddPatchTolerance.ascx" TagName="AddPatchTolerance" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:AddPatchTolerance ID="ucAddPatchTolerance" runat="server"/>
</asp:Content>