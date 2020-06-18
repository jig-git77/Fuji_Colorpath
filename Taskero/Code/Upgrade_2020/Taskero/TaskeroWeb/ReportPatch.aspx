<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" EnableEventValidation="false"  CodeBehind="ReportPatch.aspx.cs" Inherits="TaskeroWeb.ReportPatch" %>

<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register Src="Common/ReportMenu.ascx" TagName="ReportMenu" TagPrefix="uc1" %>
<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" align="center" border="0" width="100%">
        <tr>
            <td align="center">
                <table width="940" border="0" cellspacing="0" cellpadding="0" align="center">
                    <tr>
                        <td>
                            <!--Report Menu Conttrol Will Load here-->
                            <uc1:ReportMenu ID="ReportMenu1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table width="940" border="0" cellspacing="0" cellpadding="0" align="center">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">                                
                                <tr>
                                    <td style="padding: 5px;" class="TABLE1">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                                                    <tr>
                                                        <td class="green-title" valign="middle" align="left" colspan="4">
                                                            <asp:Label ID="lblReportTitle" runat="server" Text="Patch Detail Report(Search Criteria)"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" class="green-form-bg" style="border-bottom: none" id="TDReportTitle"
                                                            runat="server">
                                                            <asp:Label ID="LBL_TRSearch" runat="server" Visible="false"></asp:Label>
                                                            <asp:Label ID="LBL_TRResult" runat="server" Visible="false"></asp:Label>
                                                            <asp:Label ID="LblError" runat="server" CssClass="ERROR"></asp:Label><asp:Label ID="LBL_CPMeasurementGUID1"
                                                                runat="server" Visible="false"></asp:Label><asp:Label ID="LBL_CPMeasurementGUID2"
                                                                    runat="server" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel Style="width: 100%" ID="PNL_Search" runat="server">
                                                    <table style="width: 100%" cellspacing="0" cellpadding="0" border="0">
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">
                                                                <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                                                                    <tr>
                                                                        <td align="center" valign="top" class="green-form-bg" style="border-top: none">
                                                                            <table width="80%" border="0" cellspacing="2" cellpadding="3" class="form-text">
                                                                                <tr>
                                                                                    <td colspan="5">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 20%" align="center">
                                                                                        Company
                                                                                    </td>
                                                                                    <td style="width: 20%" align="center">
                                                                                        Branch
                                                                                    </td>
                                                                                    <td style="width: 20%" align="center">
                                                                                        Dongle No.
                                                                                    </td>
                                                                                    <td style="width: 20%" align="center">
                                                                                        Device
                                                                                    </td>
                                                                                    <td style="width: 20%" align="center">
                                                                                        Print Target
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <asp:DropDownList ID="DDL_Company" runat="server" CssClass="DROPDOWNLIST" AutoPostBack="true"
                                                                                            OnSelectedIndexChanged="FillBranch">
                                                                                            <asp:ListItem Value="0">--- Select Company ---</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:DropDownList ID="DDL_Branch" runat="server" CssClass="DROPDOWNLIST" AutoPostBack="true"
                                                                                            OnSelectedIndexChanged="FillDongle">
                                                                                            <asp:ListItem Value="0">--- Select Branch ---</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:DropDownList ID="DDL_DongleNo" runat="server" CssClass="DROPDOWNLIST" AutoPostBack="true"
                                                                                            OnSelectedIndexChanged="FillDevice">
                                                                                            <asp:ListItem Value="0">--- Select Dongle ---</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:DropDownList ID="DDL_Device" runat="server" CssClass="DROPDOWNLIST" AutoPostBack="true"
                                                                                            OnSelectedIndexChanged="FillPrintTarget">
                                                                                            <asp:ListItem Value="0">--- Select Device ---</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:DropDownList ID="DDL_PrintTarget" runat="server" CssClass="DROPDOWNLIST" AutoPostBack="true"
                                                                                            OnSelectedIndexChanged="FillDates">
                                                                                            <asp:ListItem Value="0">--- Select Print Target ---</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right" valign="baseline">
                                                                                        Search By Date
                                                                                    </td>
                                                                                    <td valign="top" colspan="3">
                                                                                        From&nbsp;<asp:TextBox ID="TXT_FromDate" runat="server" CssClass="TEXTBOX" AutoPostBack="True"
                                                                                            MaxLength="10" Columns="10" OnTextChanged="TXT_DateText" Width="75px"></asp:TextBox>&nbsp;<asp:ImageButton
                                                                                                ID="IBTN_FromDate" runat="server" ImageUrl="Images/calendar-icon.png" ImageAlign="AbsMiddle">
                                                                                            </asp:ImageButton><ajaxToolkit:CalendarExtender ID="Cal_FromDate" runat="server"
                                                                                                TargetControlID="TXT_FromDate" PopupButtonID="IBTN_FromDate">
                                                                                            </ajaxToolkit:CalendarExtender>
                                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; To&nbsp;<asp:TextBox ID="TXT_ToDate" runat="server"
                                                                                            CssClass="TEXTBOX" AutoPostBack="True" MaxLength="10" Columns="10" Width="75px"
                                                                                            OnTextChanged="TXT_DateText"></asp:TextBox>&nbsp;<asp:ImageButton ID="IBTN_ToDate"
                                                                                                runat="server" ImageUrl="Images/calendar-icon.png" ImageAlign="AbsMiddle">
                                                                                        </asp:ImageButton><ajaxToolkit:CalendarExtender ID="Cal_ToDate" runat="server" TargetControlID="TXT_ToDate"
                                                                                            PopupButtonID="IBTN_ToDate">
                                                                                        </ajaxToolkit:CalendarExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        Field
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        Operator
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        Criteria
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        Search Option One
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="DDL_OPT1_Field" runat="server" CssClass="DROPDOWNLIST">
                                                                                            <asp:ListItem Value="">---</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement CP1 ON CP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND CP1.[User]">User</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement CP1 ON CP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND CP1.Job">Job</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement CP1 ON CP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND CP1.Info_Text">Info Text</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement CP1 ON CP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND CP1.Customer">Customer</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP1 ON DP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP1.Substrate">Substrate Manufacturer</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP1 ON DP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP1.Substrate_SKU">Substrate SKU</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP1 ON DP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP1.Substrate_Batch">Substrate Batch</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP1 ON DP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP1.Ink">Ink Manufacturer</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP1 ON DP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP1.Ink_SKU">Ink SKU</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP1 ON DP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP1.Ink_Batch">Ink Batch</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP1 ON DP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP1.Chemistry">Chemistry Manufacturer</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP1 ON DP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP1.Chemistry_Batch">Chemistry Batch</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP1 ON DP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND CONVERT( VARCHAR , DP1.Resolution )">Resolution</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP1 ON DP1.CPMeasurementGUID = PMD.CPMeasurementGUID AND CONVERT( VARCHAR , DP1.Line_Screen )">Dot Shape</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_OD OD1 ON OD1.CPMeasurementGUID = PMD.CPMeasurementGUID AND OD1.Category">Device Category</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_OD OD1 ON OD1.CPMeasurementGUID = PMD.CPMeasurementGUID AND OD1.Manufacturer">Device Manufacturer</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_OD OD1 ON OD1.CPMeasurementGUID = PMD.CPMeasurementGUID AND OD1.Model">Device Model</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_OD OD1 ON OD1.CPMeasurementGUID = PMD.CPMeasurementGUID AND OD1.RIP_Type">Rip Type</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_PT PT1 ON PT1.CPMeasurementGUID = PMD.CPMeasurementGUID INNER JOIN CPDevicePrintTarget CPPT1 ON CPPT1.CPDevicePrintTargetID = PT1.CPDevicePrintTargetID AND CPPT1.PrintTarget">Print Target Name</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_PT PT1 ON PT1.CPMeasurementGUID = PMD.CPMeasurementGUID AND PT1.Proof_Supplier">Print Target Supplier</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_MD MD1 ON MD1.CPMeasurementGUID = PMD.CPMeasurementGUID AND MD1.Instrument">Measurement Device</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_PT PT1 ON PT1.CPMeasurementGUID = PMD.CPMeasurementGUID AND PT1.Target_File">Target Name</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="DDL_OPT1_Operator" runat="server" CssClass="DROPDOWNLIST">
                                                                                            <asp:ListItem Value="">---</asp:ListItem>
                                                                                            <asp:ListItem Value="= '<<>>'">Equals</asp:ListItem>
                                                                                            <asp:ListItem Value="<> '<<>>'">Does Not Equal</asp:ListItem>
                                                                                            <asp:ListItem Value="LIKE '%<<>>%'">Contains</asp:ListItem>
                                                                                            <asp:ListItem Value="NOT LIKE '%<<>>%'">Does Not Contain</asp:ListItem>
                                                                                            <asp:ListItem Value="LIKE '<<>>%'">Start With</asp:ListItem>
                                                                                            <asp:ListItem Value="LIKE '%<<>>'">Ends With</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td nowrap>
                                                                                        <asp:TextBox ID="TXT_OPT1_KeyWords" runat="server" CssClass="TEXTBOX"></asp:TextBox>&nbsp;<asp:Button
                                                                                            ID="BTN_Clear1" runat="server" CssClass="green2-btn" Text="X" OnClick="BTN_Clear_Click"
                                                                                            Ind="1"></asp:Button>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        Search Option Two
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="DDL_OPT2_Field" runat="server" CssClass="DROPDOWNLIST">
                                                                                            <asp:ListItem Value="">---</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement CP2 ON CP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND CP2.[User]">User</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement CP2 ON CP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND CP2.Job">Job</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement CP2 ON CP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND CP2.Info_Text">Info Text</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement CP2 ON CP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND CP2.Customer">Customer</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP2 ON DP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP2.Substrate">Substrate Manufacturer</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP2 ON DP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP2.Substrate_SKU">Substrate SKU</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP2 ON DP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP2.Substrate_Batch">Substrate Batch</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP2 ON DP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP2.Ink">Ink Manufacturer</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP2 ON DP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP2.Ink_SKU">Ink SKU</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP2 ON DP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP2.Ink_Batch">Ink Batch</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP2 ON DP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP2.Chemistry">Chemistry Manufacturer</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP2 ON DP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP2.Chemistry_Batch">Chemistry Batch</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP2 ON DP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND CONVERT( VARCHAR , DP2.Resolution )">Resolution</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP2 ON DP2.CPMeasurementGUID = PMD.CPMeasurementGUID AND CONVERT( VARCHAR , DP2.Line_Screen )">Dot Shape</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_OD OD2 ON OD2.CPMeasurementGUID = PMD.CPMeasurementGUID AND OD2.Category">Device Category</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_OD OD2 ON OD2.CPMeasurementGUID = PMD.CPMeasurementGUID AND OD2.Manufacturer">Device Manufacturer</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_OD OD2 ON OD2.CPMeasurementGUID = PMD.CPMeasurementGUID AND OD2.Model">Device Model</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_OD OD2 ON OD2.CPMeasurementGUID = PMD.CPMeasurementGUID AND OD2.RIP_Type">Rip Type</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_PT PT2 ON PT2.CPMeasurementGUID = PMD.CPMeasurementGUID INNER JOIN CPDevicePrintTarget CPPT2 ON CPPT2.CPDevicePrintTargetID = PT2.CPDevicePrintTargetID AND CPPT2.PrintTarget">Print Target Name</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_PT PT2 ON PT2.CPMeasurementGUID = PMD.CPMeasurementGUID AND PT2.Proof_Supplier">Print Target Supplier</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_MD MD2 ON MD2.CPMeasurementGUID = PMD.CPMeasurementGUID AND MD2.Instrument">Measurement Device</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_PT PT2 ON PT2.CPMeasurementGUID = PMD.CPMeasurementGUID AND PT2.Target_File">Target Name</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="DDL_OPT2_Operator" runat="server" CssClass="DROPDOWNLIST">
                                                                                            <asp:ListItem Value="">---</asp:ListItem>
                                                                                            <asp:ListItem Value="= '<<>>'">Equals</asp:ListItem>
                                                                                            <asp:ListItem Value="<> '<<>>'">Does Not Equal</asp:ListItem>
                                                                                            <asp:ListItem Value="LIKE '%<<>>%'">Contains</asp:ListItem>
                                                                                            <asp:ListItem Value="NOT LIKE '%<<>>%'">Does Not Contain</asp:ListItem>
                                                                                            <asp:ListItem Value="LIKE '<<>>%'">Start With</asp:ListItem>
                                                                                            <asp:ListItem Value="LIKE '%<<>>'">Ends With</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td nowrap>
                                                                                        <asp:TextBox ID="TXT_OPT2_KeyWords" runat="server" CssClass="TEXTBOX"></asp:TextBox>&nbsp;<asp:Button
                                                                                            ID="BTN_Clear2" runat="server" CssClass="green2-btn" Text="X" OnClick="BTN_Clear_Click"
                                                                                            Ind="2"></asp:Button>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        Search Option Three
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="DDL_OPT3_Field" runat="server" CssClass="DROPDOWNLIST">
                                                                                            <asp:ListItem Value="">---</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement CP3 ON CP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND CP3.[User]">User</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement CP3 ON CP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND CP3.Job">Job</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement CP3 ON CP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND CP3.Info_Text">Info Text</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement CP3 ON CP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND CP3.Customer">Customer</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP3 ON DP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP3.Substrate">Substrate Manufacturer</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP3 ON DP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP3.Substrate_SKU">Substrate SKU</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP3 ON DP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP3.Substrate_Batch">Substrate Batch</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP3 ON DP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP3.Ink">Ink Manufacturer</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP3 ON DP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP3.Ink_SKU">Ink SKU</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP3 ON DP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP3.Ink_Batch">Ink Batch</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP3 ON DP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP3.Chemistry">Chemistry Manufacturer</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP3 ON DP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND DP3.Chemistry_Batch">Chemistry Batch</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP3 ON DP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND CONVERT( VARCHAR , DP3.Resolution )">Resolution</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_DP DP3 ON DP3.CPMeasurementGUID = PMD.CPMeasurementGUID AND CONVERT( VARCHAR , DP3.Line_Screen )">Dot Shape</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_OD OD3 ON OD3.CPMeasurementGUID = PMD.CPMeasurementGUID AND OD3.Category">Device Category</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_OD OD3 ON OD3.CPMeasurementGUID = PMD.CPMeasurementGUID AND OD3.Manufacturer">Device Manufacturer</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_OD OD3 ON OD3.CPMeasurementGUID = PMD.CPMeasurementGUID AND OD3.Model">Device Model</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_OD OD3 ON OD3.CPMeasurementGUID = PMD.CPMeasurementGUID AND OD3.RIP_Type">Rip Type</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_PT PT3 ON PT3.CPMeasurementGUID = PMD.CPMeasurementGUID INNER JOIN CPDevicePrintTarget CPPT3 ON CPPT3.CPDevicePrintTargetID = PT3.CPDevicePrintTargetID AND CPPT3.PrintTarget">Print Target Name</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_PT PT3 ON PT3.CPMeasurementGUID = PMD.CPMeasurementGUID AND PT3.Proof_Supplier">Print Target Supplier</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_MD MD3 ON MD3.CPMeasurementGUID = PMD.CPMeasurementGUID AND MD3.Instrument">Measurement Device</asp:ListItem>
                                                                                            <asp:ListItem Value="INNER JOIN CPMeasurement_PT PT3 ON PT3.CPMeasurementGUID = PMD.CPMeasurementGUID AND PT3.Target_File">Target Name</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="DDL_OPT3_Operator" runat="server" CssClass="DROPDOWNLIST">
                                                                                            <asp:ListItem Value="">---</asp:ListItem>
                                                                                            <asp:ListItem Value="= '<<>>'">Equals</asp:ListItem>
                                                                                            <asp:ListItem Value="<> '<<>>'">Does Not Equal</asp:ListItem>
                                                                                            <asp:ListItem Value="LIKE '%<<>>%'">Contains</asp:ListItem>
                                                                                            <asp:ListItem Value="NOT LIKE '%<<>>%'">Does Not Contain</asp:ListItem>
                                                                                            <asp:ListItem Value="LIKE '<<>>%'">Start With</asp:ListItem>
                                                                                            <asp:ListItem Value="LIKE '%<<>>'">Ends With</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td nowrap>
                                                                                        <asp:TextBox ID="TXT_OPT3_KeyWords" runat="server" CssClass="TEXTBOX"></asp:TextBox>&nbsp;<asp:Button
                                                                                            ID="BTN_Clear3" runat="server" CssClass="green2-btn" Text="X" OnClick="BTN_Clear_Click"
                                                                                            Ind="3"></asp:Button>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="3">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td align="right">
                                                                                        <asp:Button ID="BTN_CreateReport" OnClick="BTN_CreateReport_Click" runat="server"
                                                                                            CssClass="green2-btn" Text="Create Report"></asp:Button>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="5" height="10px">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="BTN_CreateReport" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel Style="width: 100%" ID="PNL_Result" runat="server">
                                                    <table class="TABLE2" cellspacing="0" cellpadding="0" align="center" border="0" style="width: 100%">
                                                        <tr>
                                                            <td valign="middle" align="left" colspan="2">
                                                                <table cellspacing="5" cellpadding="0" width="100%" align="center" border="0">
                                                                    <tr>
                                                                        <td style="width: 80%" class="ReportHeader" align="center" nowrap>
                                                                            <asp:Label ID="LBL_DeviceName" runat="server"></asp:Label>&nbsp;-&nbsp;<asp:Label
                                                                                ID="LBL_Branch" runat="server"></asp:Label>&nbsp;Report<br />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3">
                                                                            <table class="DATAGRID1" cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td class="HEADERSTYLE1" colspan="3">
                                                                                        Summary
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ITEMSTYLE1">
                                                                                    <td colspan="3">
                                                                                        <strong>&nbsp; Date Range</strong>&nbsp;&nbsp;<asp:Label ID="LBL_DateRange" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3">
                                                                            <table class="DATAGRID1" cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                                <tr class="HEADERSTYLE1">
                                                                                    <td style="width: 20%" align="center">
                                                                                        Search Field
                                                                                    </td>
                                                                                    <td style="width: 20%" align="center">
                                                                                        Operator
                                                                                    </td>
                                                                                    <td style="width: 20%" align="center">
                                                                                        Criteria
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ITEMSTYLE1">
                                                                                    <td align="center">
                                                                                        <asp:Label ID="LBL_Field1" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Label ID="LBL_Operator1" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Label ID="LBL_Criteria1" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ALTERNATINGITEMSTYLE1">
                                                                                    <td align="center">
                                                                                        <asp:Label ID="LBL_Field2" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Label ID="LBL_Operator2" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Label ID="LBL_Criteria2" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ITEMSTYLE1">
                                                                                    <td align="center">
                                                                                        <asp:Label ID="LBL_Field3" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Label ID="LBL_Operator3" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:Label ID="LBL_Criteria3" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-left: 5px; padding-right: 5px">
                                                                <asp:Label ID="LBL_SortExpression1" runat="server" Visible="false">Sample_ID</asp:Label>
                                                                <asp:Label ID="LBL_SortOrder1" runat="server" Visible="false"></asp:Label>
                                                                <asp:DataGrid ID="DGPatchDetail" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                    OnItemDataBound="DGPatchDetail_ItemDataBound1" AllowSorting="True" OnSortCommand="SortCommandPatchDetail_OnClick"
                                                                    CellPadding="0">
                                                                    <Columns>
                                                                        <asp:TemplateColumn HeaderText="&lt;img src='images/title_color.gif' border=0 align='center' vspace='3' &gt;">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="LBL_Color" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Width="5%"></HeaderStyle>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox runat="server"></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:BoundColumn DataField="Sample_Name" HeaderText="&lt;img src='images/title_patch_id.gif' border=0 align='center' &gt;"
                                                                            SortExpression="Sample_ID"
                                                                            ItemStyle-HorizontalAlign="Left">
                                                                            <HeaderStyle Width="4%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="CMYK_C" HeaderText="&lt;img src = 'images/title_c.gif' border =0 align='center' vspace='3' /&gt;"
                                                                            SortExpression="CMYK_C">
                                                                            <HeaderStyle Width="4%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="CMYK_M" HeaderText="&lt;img src = 'images/title_m.gif' border =0 align='center' vspace='3' /&gt;"
                                                                            SortExpression="CMYK_M">
                                                                            <HeaderStyle Width="4%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="CMYK_Y" HeaderText="&lt;img src = 'images/title_y.gif' border =0  align='center' vspace='3' /&gt;"
                                                                            SortExpression="CMYK_Y">
                                                                            <HeaderStyle Width="4%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="CMYK_K" HeaderText="&lt;img src = 'images/title_k.gif' border =0 align='center' vspace='3' /&gt;"
                                                                            SortExpression="CMYK_K">
                                                                            <HeaderStyle Width="4%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="LAB_L1" HeaderText="&lt;img src = 'images/title_target_l.gif' border =0  align='center' vspace='3' /&gt;"
                                                                            SortExpression="LAB_L1">
                                                                            <HeaderStyle Width="5%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="LAB_A1" HeaderText="&lt;img src = 'images/title_target_a.gif' border =0  align='center' vspace='3' /&gt;"
                                                                            SortExpression="LAB_A1">
                                                                            <HeaderStyle Width="5%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="LAB_B1" HeaderText="&lt;img src = 'images/title_target_b.gif' border =0 align='center' vspace='3' /&gt;"
                                                                            SortExpression="LAB_B1">
                                                                            <HeaderStyle Width="5%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="MinDL" DataFormatString="{0:f2}" HeaderText="&lt;img src = 'images/title_min_delta_l.gif' border =0 align='center' vspace='3' /&gt;"
                                                                            SortExpression="MinDL">
                                                                            <HeaderStyle Width="5%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="MaxDL" DataFormatString="{0:f2}" HeaderText="&lt;img src = 'images/max_delta_l.gif ' border =0 align='center' vspace='3' /&gt;"
                                                                            SortExpression="MaxDL">
                                                                            <HeaderStyle Width="5%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="AvgDL" DataFormatString="{0:f2}" HeaderText="&lt;img src = 'images/title_ave_delta_l.gif' border =0 align='center' vspace='3' /&gt;"
                                                                            SortExpression="AvgDL">
                                                                            <HeaderStyle Width="5%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="MinDA" DataFormatString="{0:f2}" HeaderText="&lt;img src = 'images/title_delta_a.gif' border =0 align='center' vspace='3' /&gt;"
                                                                            SortExpression="MinDA">
                                                                            <HeaderStyle Width="5%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="MaxDA" DataFormatString="{0:f2}" HeaderText="&lt;img src = 'images/title_max_delta_a.gif' border =0  align='center' vspace='3' /&gt;"
                                                                            SortExpression="MaxDA">
                                                                            <HeaderStyle Width="5px"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <%--<asp:BoundColumn DataField="AvgDA" DataFormatString="{0:f2}" HeaderText="&lt;img src = 'images/title_ave_delta_a.gif' border =0 align='center' vspace='3' /&gt;"
                                SortExpression="AvgDA">
                                <HeaderStyle Width="5%"></HeaderStyle>
                            </asp:BoundColumn>--%>
                                                                        <asp:BoundColumn DataField="MinDB" DataFormatString="{0:f2}" HeaderText="&lt;img src = 'images/title_min_delta_b.gif' border =0 align='center' vspace='3' /&gt;"
                                                                            SortExpression="MinDB">
                                                                            <HeaderStyle Width="5%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="MaxDB" DataFormatString="{0:f2}" HeaderText="&lt;img src = 'images/title_max_delta_b.gif' border =0 align='center' vspace='3' /&gt;"
                                                                            SortExpression="MaxDB">
                                                                            <HeaderStyle Width="5%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <%--<asp:BoundColumn DataField="AvgDB" DataFormatString="{0:f2}" HeaderText="&lt;img src = 'images/title_ave_delta_b.gif' border =0  align='center' vspace='3'/&gt;"
                                SortExpression="AvgDB">
                                <HeaderStyle Width="5%"></HeaderStyle>
                            </asp:BoundColumn>--%>
                                                                        <asp:BoundColumn DataField="MinDE" DataFormatString="{0:f2}" HeaderText="&lt;img src = 'images/title_min_delta_e.gif' border =0 align='center' vspace='3' /&gt;"
                                                                            SortExpression="MinDE">
                                                                            <HeaderStyle Width="5%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="MaxDE" DataFormatString="{0:f2}" HeaderText="&lt;img src ='images/title_max_delta_e.gif' border =0  align='center' vspace='3'/&gt;"
                                                                            SortExpression="MaxDE">
                                                                            <HeaderStyle Width="5%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="AvgDE" DataFormatString="{0:f2}" HeaderText="&lt;img src ='images/title_ave_Delta_e.gif' border =0 align='center' vspace='3' /&gt;"
                                                                            SortExpression="AvgDE">
                                                                            <HeaderStyle Width="5%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="STDDEV_DE" DataFormatString="{0:f2}" HeaderText="&lt;img src ='images/title_stddev_deltae.gif' border =0 align='center' vspace='3' /&gt;"
                                                                            SortExpression="STDDEV_DE">
                                                                            <HeaderStyle Width="5%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="AveDensity" DataFormatString="{0:f2}" HeaderText="&lt;img src = 'images/title_AVEDensity.gif' border =0  align='center' vspace='3'/&gt;"
                                                                            SortExpression="AveDensity">
                                                                            <HeaderStyle Width="5%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="StdDevDensity" DataFormatString="{0:f2}" HeaderText="&lt;img src = 'images/title_STDDevDensity.gif' border =0  align='center' vspace='3'/&gt;"
                                                                            SortExpression="StdDevDensity">
                                                                            <HeaderStyle Width="5%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                    </Columns>
                                                                </asp:DataGrid>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Button ID="BTN_MakePDF" OnClick="BTN_MakePDF_Click" runat="server" CssClass="green2-btn"
                                                                    Text="Generate PDF"></asp:Button>&nbsp;<asp:Button ID="BTN_BackToSearch" OnClick="BTN_BackToSearch_Click"
                                                                        runat="server" CssClass="green2-btn" Text="Back To Search"></asp:Button>&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="BTN_MakePDF" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
