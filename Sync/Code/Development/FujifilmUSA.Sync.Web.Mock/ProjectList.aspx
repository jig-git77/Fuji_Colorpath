<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="ProjectList.aspx.cs" Inherits="FujifilmUSA.Sync.Web.Mock.ProjectList" %>

<%@ Register Src="~/UserControl/ProjectList/ProjectList.ascx" TagName="ProjectList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <uc1:ProjectList ID="ucProjectList" runat="server" />
</asp:Content>
