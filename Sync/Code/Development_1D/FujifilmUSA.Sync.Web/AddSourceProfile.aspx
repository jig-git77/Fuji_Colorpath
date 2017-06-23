<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddSourceProfile.aspx.cs" Inherits="FujifilmUSA.Sync.Web.AddSourceProfile" %>
<%@ Register Src="~/UserControl/AddSourceProfile.ascx" TagName="AddSourceProfile" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:AddSourceProfile ID="ucAddSourceProfile"  runat="server"/>
</asp:Content>
