<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="ProfileManagement.aspx.cs" Inherits="FujifilmUSA.Sync.Web.ProfileManagement" %>

<%@ Register Src="UserControl/ProfileManagement.ascx" TagName="ProfileManagement"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ProfileManagement ID="ucProfileManagement" runat="server" />
</asp:Content>
