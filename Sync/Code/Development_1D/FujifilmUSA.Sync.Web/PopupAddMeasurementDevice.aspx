<%@ Page Title="" Language="C#" MasterPageFile="~/PopUpMaster.Master" AutoEventWireup="true" CodeBehind="PopupAddMeasurementDevice.aspx.cs" Inherits="FujifilmUSA.Sync.Web.PopupAddMeasurementDevice" %>
<%--<%@ Register Src="~/UserControl/PopupControl/AddMeasurementDevice.ascx" 
    TagName="ImportWindow" TagPrefix="uc" %>--%>
<%@ Register Src="~/UserControl/AddMeasurementDevice.ascx" TagName="AddMeasurementDevice" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .admin_middle{
            min-height: 400px !important;
        }
        h1{
            display:none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<uc:ImportWindow ID="ucImportWindow" runat="server" />--%>
    <uc:AddMeasurementDevice ID="ucAddMeasurementDevice"  runat="server"/>
</asp:Content>
