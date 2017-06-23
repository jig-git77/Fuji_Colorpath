<%@ Page Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="Export.aspx.cs" 
    Inherits="FujifilmUSA.Sync.Web.Export"
    MasterPageFile="~/MainMaster.Master" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/Export.ascx" TagName="Export" TagPrefix="uc" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <uc:Export ID="ucExport" runat="server" />

</asp:Content>
