<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/PopUpMaster.Master" CodeBehind="PrintCondition.aspx.cs" Inherits="FujifilmUSA.Sync.Web.PrintCondition" %>
<%@ Register Src="~/UserControl/ModelWindowContainer.ascx" TagName="ModelWindowContainer" TagPrefix="uc" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<uc:ModelWindowContainer ID="ucModelWindowContainer" runat="server" />
</asp:Content>


