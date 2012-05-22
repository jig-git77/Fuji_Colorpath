<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Activity_PrintCondition.aspx.cs" Inherits="FujifilmUSA.Sync.Web.Activity_PrintCondition" %>

<%@ Register Src="~/UserControl/Activity_PrintCondition.ascx" TagName="Activity_PrintCondition" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:Activity_PrintCondition ID="ucActivity_PrintCondition" runat="server" />
</asp:Content>
