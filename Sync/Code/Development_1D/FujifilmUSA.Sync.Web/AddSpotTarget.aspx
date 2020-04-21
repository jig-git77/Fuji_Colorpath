<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" 
    CodeBehind="AddSpotTarget.aspx.cs" Inherits="FujifilmUSA.Sync.Web.AddSpotTarget" %>
<%@ Register Src="~/UserControl/AddSpotTarget.ascx" TagName="AddSpotTarget"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:AddSpotTarget ID="ucAddSpotTarget" runat="server" />
</asp:Content>
