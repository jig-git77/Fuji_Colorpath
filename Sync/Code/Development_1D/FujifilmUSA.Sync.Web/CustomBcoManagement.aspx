<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AdminMaster.Master" CodeBehind="CustomBcoManagement.aspx.cs" 
    Inherits="FujifilmUSA.Sync.Web.CustomBcoManagement" %>

<%@ Register Src="~/UserControl/CustomBcoManagement.ascx" TagName="CustomBco" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:CustomBco ID="ucCustomBco"  runat="server"/>    
</asp:Content>
