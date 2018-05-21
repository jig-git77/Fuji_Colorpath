<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" 
    CodeBehind="SpotTargetManagement.aspx.cs" Inherits="FujifilmUSA.Sync.Web.SpotTargetManagement" %>

<%@ Register Src="~/UserControl/SpotTargetManagement.ascx" TagName="SpotTarget" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:SpotTarget ID="ucSpotTarget"  runat="server"/>    
</asp:Content>
