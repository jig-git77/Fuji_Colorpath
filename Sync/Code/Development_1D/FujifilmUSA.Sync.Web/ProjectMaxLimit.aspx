<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ProjectMaxLimit.aspx.cs" Inherits="FujifilmUSA.Sync.Web.ProjectMaxLimit" %>
<%@ Register Src="~/UserControl/ProjectMaxLimit.ascx" TagName="ProjectMaxLimit" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <uc1:ProjectMaxLimit ID="ucProjectMaxLimit" runat="server" />
</asp:Content>
