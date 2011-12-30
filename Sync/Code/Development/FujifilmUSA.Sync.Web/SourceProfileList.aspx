<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="SourceProfileList.aspx.cs" Inherits="FujifilmUSA.Sync.Web.SourceProfileList" %>
<%@ Register Src="~/UserControl/SourceProfileList.ascx" TagName="SourceProfile" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc:SourceProfile ID="ucSourceProfile" runat="server" />
</asp:Content>
