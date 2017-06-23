<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ActivityValidationRules.aspx.cs" Inherits="FujifilmUSA.Sync.Web.ActivityValidationRules" %>
<%@ Register Src="~/UserControl/ActivityValidationRules.ascx" TagName="ActivityValidationRules" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <uc:ActivityValidationRules ID="ucActivityValidationRules"  runat="server"/>
</asp:Content>
