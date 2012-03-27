<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" 
CodeBehind="SessionEnd.aspx.cs" 
Inherits="FujifilmUSA.Sync.Web.SessionEnd" %>
<%@ Register Src="~/UserControl/SessionEnd.ascx" TagName="SessionEnd" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc:SessionEnd ID="ucSessionEnd"  runat="server"/>
</asp:Content>
