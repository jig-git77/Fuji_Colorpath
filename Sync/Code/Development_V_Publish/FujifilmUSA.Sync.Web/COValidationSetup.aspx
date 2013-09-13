<%@ Page Title="" Language="C#" MasterPageFile="~/COMaster.Master" AutoEventWireup="true" CodeBehind="COValidationSetup.aspx.cs" Inherits="FujifilmUSA.Sync.Web.COValidationSetup" %>
<%@ Register Src="~/UserControl/AddProject/ValidationSetup.ascx" TagName="ValidationSetup" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="contant_top">&nbsp;</div>
        <div class="contant_middle"><uc:ValidationSetup ID="ucValidationSetup" runat="server" /></div>    
    <div class="contant_bottom">&nbsp;</div>
</asp:Content>
