<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCustomBcoTarget.aspx.cs" Inherits="FujifilmUSA.Sync.Web.AddCustomBcoTarget"  MasterPageFile="~/AdminMaster.Master" %>

<%@ Register Src="~/UserControl/AddCustomBcoTarget.ascx" TagName="AddCustomBcoTarget" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:AddCustomBcoTarget ID="ucAddCustomBcoTarget" runat="server" />
</asp:Content>