<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportPerformanceSummaryCtrl.ascx.cs" Inherits="OrisWeb.ReportPerformanceSummaryCtrl" %>
<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
            <tr>
                <td class="green-title" valign="middle" align="left">
                    <asp:Label ID="lblReportTitle" runat="server" Text="Color Specification Adherence Report"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" id="TDReportTitle" runat="server">
                    <asp:Label ID="LblError" runat="server" Class="ERROR"></asp:Label><asp:Literal
                        ID="LTRL_CPMeasurementGUID" runat="server" Visible="false"></asp:Literal>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
        <asp:Panel Style="width: 100%" ID="PNL_Search" runat="server">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="maintable">
                <tr>
                    <td align="center" valign="top" class="green-form-bg" style="border-top: none">
                        <table width="80%" border="0" cellspacing="2" cellpadding="3" class="form-text">
                            <tr>
                                <td colspan="4" align="center">
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" height="10px">
                                    &nbsp;
                                </td>
                                <td width="25%" height="10px">
                                    <asp:Literal ID="LTRL_Company" runat="server"></asp:Literal>
                                </td>
                                <td width="25%" height="10px">
                                    <asp:Literal ID="LTRL_Branch1" runat="server"></asp:Literal>
                                </td>
                                <td width="25%" height="10px">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDL_Company" runat="server" CssClass="DROPDOWNLIST" AutoPostBack="true"
                                        OnSelectedIndexChanged="FillBranch">
                                        <asp:ListItem Value="0">--- Select Company ---</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDL_Branch" runat="server" AutoPostBack="true" CssClass="DROPDOWNLIST"
                                        OnSelectedIndexChanged="FillDates">
                                        <asp:ListItem Value="0">--- All ---</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="middle">
                                    <asp:Literal ID="LTRL_SearchByDate" runat="server"></asp:Literal>
                                </td>
                                <td colspan="3" valign="top">
                                    <table border="0" cellspacing="0" cellpadding="0" class="form-text">
                                        <tr>
                                            <td width="30">
                                                <asp:Literal ID="LTRL_From" runat="server"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TXT_FromDate" runat="server" CssClass="TEXTBOX" AutoPostBack="True"
                                                    MaxLength="10" Columns="10" OnTextChanged="TXT_DateText" Width="70px"></asp:TextBox>&nbsp;
                                            </td>
                                            <td width="20">
                                                <asp:ImageButton ID="IBTN_FromDate" runat="server" ImageUrl="Images/calendar-icon.png"
                                                    ImageAlign="AbsMiddle"></asp:ImageButton><ajaxToolkit:CalendarExtender ID="Cal_FromDate"
                                                        runat="server" TargetControlID="TXT_FromDate" PopupButtonID="IBTN_FromDate">
                                                    </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td width="10">
                                                &nbsp;
                                            </td>
                                            <td width="20">
                                                <asp:Literal ID="LTRL_To" runat="server"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TXT_ToDate" runat="server" CssClass="TEXTBOX" AutoPostBack="True"
                                                    MaxLength="10" Columns="10" OnTextChanged="TXT_DateText" Width="75px"></asp:TextBox>&nbsp;
                                            </td>
                                            <td width="20">
                                                <asp:ImageButton ID="IBTN_ToDate" runat="server" ImageUrl="Images/calendar-icon.png"
                                                    ImageAlign="AbsMiddle"></asp:ImageButton><ajaxToolkit:CalendarExtender ID="Cal_ToDate"
                                                        runat="server" TargetControlID="TXT_ToDate" PopupButtonID="IBTN_ToDate">
                                                    </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="center">
                                    <asp:Literal ID="LTRL_Field" runat="server"></asp:Literal>
                                </td>
                                <td align="center">
                                    <asp:Literal ID="LTRL_Operator" runat="server"></asp:Literal>
                                </td>
                                <td align="center">
                                    <asp:Literal ID="LTRL_Criteria" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Literal ID="LTRL_SearchOptionOne" runat="server"></asp:Literal>
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
                                <td>
                                    <asp:TextBox ID="TXT_OPT1_KeyWords" runat="server" CssClass="TEXTBOX"></asp:TextBox>&nbsp;<asp:Button
                                        ID="BTN_Clear1" runat="server" CssClass="green2-btn" Text="X" OnClick="BTN_Clear_Click"
                                        Ind="1"></asp:Button>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Literal ID="LTRL_SearchOptionTwo" runat="server"></asp:Literal>
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
                                <td>
                                    <asp:TextBox ID="TXT_OPT2_KeyWords" runat="server" CssClass="TEXTBOX"></asp:TextBox>&nbsp;<asp:Button
                                        ID="BTN_Clear2" runat="server" CssClass="green2-btn" Text="X" OnClick="BTN_Clear_Click"
                                        Ind="2"></asp:Button>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Literal ID="LTRL_SearchOptionThree" runat="server"></asp:Literal>
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
                                <td>
                                    <asp:TextBox ID="TXT_OPT3_KeyWords" runat="server" CssClass="TEXTBOX"></asp:TextBox>&nbsp;<asp:Button
                                        ID="BTN_Clear3" runat="server" CssClass="green2-btn" Text="X" OnClick="BTN_Clear_Click"
                                        Ind="3"></asp:Button>
                                </td>
                            </tr>
                            <tr> 
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="BTN_CreateReport" OnClientClick="Search()" OnClick="BTN_CreateReport_Click"
                                        runat="server" CssClass="green2-btn"></asp:Button>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;
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
            <table cellspacing="0" cellpadding="0" align="center" border="0" width="100%">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                            <tbody>
                                <tr>
                                    <td valign="top">
                                        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0" width="100%">
                                        <tr>
                                                <td style="height: 5px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center"><h1><asp:Literal ID="LTRL_CompanyName" runat="server"></asp:Literal></h1></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table class="TABLE5" cellspacing="0" cellpadding="0" border="0">
                                                        <tr>
                                                            <td class="HEADERSTYLE" colspan="4">
                                                                Summary
                                                            </td>
                                                        </tr>
                                                        <tr class="ITEMSTYLE">
                                                            <td style="width: 20%" id="TD_PBTitle" runat="server" visible="false"><strong>&nbsp; Print Buyer</strong></td>
                                                            <td style="width: 30%" id="TD_PBText" runat="server" visible="false"></td>
                                                            <td style="width: 20%">
                                                                <strong>&nbsp; Date Range</strong>
                                                            </td>
                                                            <td>
                                                                <asp:Literal ID="LTRL_DateRange" runat="server"></asp:Literal>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div style="float: left; width: 49%">
                                                        <table class="TABLE5" cellspacing="0" cellpadding="0" border="0">
                                                            <tbody>
                                                                <tr class="HEADERSTYLE">
                                                                    <td style="width: 50%" align="center">
                                                                        Parameter
                                                                    </td>
                                                                    <td style="width: 50%" align="center">
                                                                        Result
                                                                    </td>
                                                                </tr>
                                                                <tr class="ALTERNATINGITEMSTYLE">
                                                                    <td align="left">
                                                                        <strong>&nbsp;Average Delta E</strong>
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Literal ID="LTRL_AvgDeltaE" runat="server"></asp:Literal>
                                                                    </td>
                                                                </tr>
                                                                <tr class="ITEMSTYLE">
                                                                    <td align="left">
                                                                        <strong>&nbsp;Average Plate Deviation</strong>
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Literal ID="LTRL_AvgPlateDeviation" runat="server"></asp:Literal>
                                                                    </td>
                                                                </tr>
                                                                <tr class="ALTERNATINGITEMSTYLE">
                                                                    <td align="left">
                                                                        <strong>&nbsp;Error Rate</strong>
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Literal ID="LTRL_AvgErrorRate" runat="server"></asp:Literal>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                    <div style="float: left; width: 1%">
                                                        &nbsp;</div>
                                                    <div style="float: right; width: 50%">
                                                        <table class="TABLE5" cellspacing="0" cellpadding="0" border="0">
                                                            <tbody>
                                                                <tr class="HEADERSTYLE">
                                                                    <td style="width: 30%" align="center">
                                                                        Search Field
                                                                    </td>
                                                                    <td style="width: 30%" align="center">
                                                                        Operator
                                                                    </td>
                                                                    <td style="width: 40%" align="center">
                                                                        Criteria
                                                                    </td>
                                                                </tr>
                                                                <tr class="ALTERNATINGITEMSTYLE">
                                                                    <td class="BOLD" align="center">
                                                                        <asp:Literal ID="LTRL_Field1" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td class="BOLD" align="center">
                                                                        <asp:Literal ID="LTRL_Operator1" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td class="BOLD" align="center">
                                                                        <asp:Literal ID="LTRL_Criteria1" runat="server"></asp:Literal>
                                                                    </td>
                                                                </tr>
                                                                <tr class="ITEMSTYLE">
                                                                    <td class="BOLD" align="center">
                                                                        <asp:Literal ID="LTRL_Field2" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td class="BOLD" align="center">
                                                                        <asp:Literal ID="LTRL_Operator2" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td class="BOLD" align="center">
                                                                        <asp:Literal ID="LTRL_Criteria2" runat="server"></asp:Literal>
                                                                    </td>
                                                                </tr>
                                                                <tr class="ALTERNATINGITEMSTYLE">
                                                                    <td class="BOLD" align="center">
                                                                        <asp:Literal ID="LTRL_Field3" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td class="BOLD" align="center">
                                                                        <asp:Literal ID="LTRL_Operator3" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td class="BOLD" align="center">
                                                                        <asp:Literal ID="LTRL_Criteria3" runat="server"></asp:Literal>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>                                                                                        
                                            <tr><td style="height: 5px"></td></tr>
                                            <tr><td style="font-size:20px" align="center">---------- Company Summary ----------</td></tr>
                                            <tr>
                                                <td style="padding:3px;background-color:#efefef;color:#333;border:1px solid #808080">
                                                    <b style="font-size:15px">Company Summary</b><br />
                                                    <table class="DATAGRID" cellspacing="0" cellpadding="0" border="0" style="width: 100%">
                                                        <tr class="HEADERSTYLE"><td rowspan="2" style="width: 50%">Company Name</td><td rowspan="2" style="width: 10%">Reads</td><td rowspan="2" style="width: 10%">Error<br />Rate</td><td rowspan="2" style="width: 10%">Total<br />Errors</td><td rowspan="2" style="width: 10%">Ave<br />DE</td><td rowspan="2" style="width: 10%">Max<br />DE</td><td colspan="2" style="width: 10%">Substrate</td><td colspan="2" style="width: 10%">Cyan</td><td colspan="2" style="width: 10%">Magenta</td><td colspan="2" style="width: 10%">Yellow</td><td colspan="2" style="width: 10%">Black</td><td colspan="2" style="width: 10%">Gray</td><td colspan="2" style="width: 10%">Humidity</td><td colspan="2" style="width: 10%">Temp</td></tr>
                                                        <tr class="HEADERSTYLE"><td>DE</td><td>DH</td><td>DE</td><td>DH</td><td>DE</td><td>DH</td><td>DE</td><td>DH</td><td>DE</td><td>DH</td><td>DE</td><td>DH</td><td>AVE</td><td>STD</td><td>AVE</td><td>STD</td></tr>
                                                        <tr class="ITEMSTYLE"><td><strong><asp:Literal ID="LTRL_CD_CompanyName" runat="server"></asp:Literal></strong></td><td align="center"><asp:Literal ID="LTRL_CD_READS" runat="server"></asp:Literal></td><td align="center"><asp:Literal ID="LTRL_CD_ERRORRATE" runat="server"></asp:Literal>%</td><td align="center"><asp:Literal ID="LTRL_CD_TOTAL_ERRORS" runat="server"></asp:Literal></td><td align="center"><asp:Literal ID="LTRL_CD_AVG_DE" runat="server"></asp:Literal></td><td align="center"><asp:Literal ID="LTRL_CD_MAX_DE" runat="server"></asp:Literal></td><td align="center"><asp:Literal ID="LTRL_CD_SUBSTRATE_DE" runat="server"></asp:Literal></td><td align="center"><asp:Literal ID="LTRL_CD_SUBSTRATE_DH" runat="server"></asp:Literal></td><td align="center"><asp:Literal ID="LTRL_CD_CYAN_DE" runat="server"></asp:Literal></td><td align="center"><asp:Literal ID="LTRL_CD_CYAN_DH" runat="server"></asp:Literal></td><td align="center"><asp:Literal ID="LTRL_CD_MAGENTA_DE" runat="server"></asp:Literal></td><td align="center"><asp:Literal ID="LTRL_CD_MAGENTA_DH" runat="server"></asp:Literal></td><td align="center"><asp:Literal ID="LTRL_CD_YELLOW_DE" runat="server"></asp:Literal></td><td align="center"><asp:Literal ID="LTRL_CD_YELLOW_DH" runat="server"></asp:Literal></td><td align="center"><asp:Literal ID="LTRL_CD_BLACK_DE" runat="server"></asp:Literal></td><td align="center"><asp:Literal ID="LTRL_CD_BLACK_DH" runat="server"></asp:Literal></td><td align="center"><asp:Literal ID="LTRL_CD_GRAY_DE" runat="server"></asp:Literal></td><td align="center"><asp:Literal ID="LTRL_CD_GRAY_DH" runat="server"></asp:Literal></td><td align="center"><asp:Literal ID="LTRL_CD_HUMIDITY_AVE" runat="server"></asp:Literal></td><td align="center"><asp:Literal ID="LTRL_CD_HUMIDITY_STD" runat="server"></asp:Literal></td><td align="center"><asp:Literal ID="LTRL_CD_TEMP_AVE" runat="server"></asp:Literal></td><td align="center"><asp:Literal ID="LTRL_CD_TEMP_STD" runat="server"></asp:Literal></td></tr>                                                                                                                             
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr><td style="height: 5px"></td></tr>
                                            <tr>
                                                <td style="padding:3px;background-color:#efefef;color:#333;border:1px solid #808080">
                                                    <b style="font-size:15px">Branch Summary</b><br /><asp:Literal ID="LTRL_Branch_SortExpression" runat="server" Visible="false">BranchName</asp:Literal><asp:Literal ID="LTRL_Branch_SortOrder" runat="server" Visible="false"></asp:Literal>
                                                    <asp:DataGrid ID="DG_Brnach" runat="server"  OnItemCreated="DG_Branch_ItemCreated" OnSortCommand="DG_Brnach_SortCommand">
                                                        <Columns>
                                                            <asp:BoundColumn HeaderStyle-Width="50%" HeaderText="Branch Name" DataField="BranchName" SortExpression="BranchName"></asp:BoundColumn>
                                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="Reads" DataField="Reads" SortExpression="Reads"></asp:BoundColumn>
                                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="Error<br />Rate" DataField="ErrorRate" SortExpression="ErrorRate" DataFormatString="{0:f2}%"></asp:BoundColumn>
                                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="Total<br />Errors" DataField="Total_Errors" SortExpression="Total_Errors"></asp:BoundColumn>
                                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="Ave<br />DE" DataField="AVG_DE" SortExpression="AVG_DE"></asp:BoundColumn>
                                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="Max<br />DE" DataField="MAX_DE" SortExpression="MAX_DE"></asp:BoundColumn>                                                                                
                                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DE" DataField="SUBSTRATE_DE" SortExpression="SUBSTRATE_DE"></asp:BoundColumn>
                                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DH" DataField="SUBSTRATE_DH" SortExpression="SUBSTRATE_DH"></asp:BoundColumn>
                                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DE" DataField="CYAN_DE" SortExpression="CYAN_DE"></asp:BoundColumn>
                                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DH" DataField="CYAN_DH" SortExpression="CYAN_DH"></asp:BoundColumn>
                                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DE" DataField="MAGENTA_DE" SortExpression="MAGENTA_DE"></asp:BoundColumn>
                                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DH" DataField="MAGENTA_DH" SortExpression="MAGENTA_DH"></asp:BoundColumn>
                                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DE" DataField="YELLOW_DE" SortExpression="YELLOW_DE"></asp:BoundColumn>
                                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DH" DataField="YELLOW_DH" SortExpression="YELLOW_DH"></asp:BoundColumn>
                                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DE" DataField="BLACK_DE" SortExpression="BLACK_DE"></asp:BoundColumn>
                                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DH" DataField="BLACK_DH" SortExpression="BLACK_DH"></asp:BoundColumn>
                                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DE" DataField="GRAY_DE" SortExpression="GRAY_DE"></asp:BoundColumn>
                                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DH" DataField="GRAY_DH" SortExpression="GRAY_DH"></asp:BoundColumn>
                                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="AVE" DataField="HUMIDITY_AVE" SortExpression="HUMIDITY_AVE"></asp:BoundColumn>
                                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="STD" DataField="HUMIDITY_STD" SortExpression="HUMIDITY_STD"></asp:BoundColumn>
                                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="AVE" DataField="TEMP_AVE" SortExpression="TEMP_AVE"></asp:BoundColumn>
                                                            <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="STD" DataField="TEMP_STD" SortExpression="TEMP_STD" ></asp:BoundColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </td>
                                            </tr>
                                            <tr><td style="font-size:20px" align="center">---------- Branch Detail ----------</td></tr>
                                             <tr><td style="height: 5px"></td></tr>
                                                <asp:Repeater ID="RPT_Company" runat="server" OnItemDataBound="RPT_Company_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="padding:3px;background-color:#efefef;color:#333;border:1px solid #808080">
                                                                <b style="font-size:15px"><%#Eval("BranchName") %></b><br />
                                                                <asp:DataGrid ID="DG_ColorSpec" runat="server">
                                                                    <Columns>
                                                                        <asp:BoundColumn HeaderStyle-Width="50%" HeaderText="Device Name" DataField="CPDeviceDesc" SortExpression="CPDeviceDesc"></asp:BoundColumn>                                                                        
                                                                        <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="Reads" DataField="Reads" SortExpression="Reads"></asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="Error<br />Rate" DataField="ErrorRate" SortExpression="ErrorRate" DataFormatString="{0:f2}%"></asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="Total<br />Errors" DataField="Total_Errors" SortExpression="Total_Errors"></asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="Ave<br />DE" DataField="AVG_DE" SortExpression="AVG_DE"></asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="Max<br />DE" DataField="MAX_DE" SortExpression="MAX_DE"></asp:BoundColumn>                                                                                
                                                                        <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DE" DataField="SUBSTRATE_DE" SortExpression="SUBSTRATE_DE"></asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DH" DataField="SUBSTRATE_DH" SortExpression="SUBSTRATE_DH"></asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DE" DataField="CYAN_DE" SortExpression="CYAN_DE"></asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DH" DataField="CYAN_DH" SortExpression="CYAN_DH"></asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DE" DataField="MAGENTA_DE" SortExpression="MAGENTA_DE"></asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DH" DataField="MAGENTA_DH" SortExpression="MAGENTA_DH"></asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DE" DataField="YELLOW_DE" SortExpression="YELLOW_DE"></asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DH" DataField="YELLOW_DH" SortExpression="YELLOW_DH"></asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DE" DataField="BLACK_DE" SortExpression="BLACK_DE"></asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DH" DataField="BLACK_DH" SortExpression="BLACK_DH"></asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DE" DataField="GRAY_DE" SortExpression="GRAY_DE"></asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="DH" DataField="GRAY_DH" SortExpression="GRAY_DH"></asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="AVE" DataField="HUMIDITY_AVE" SortExpression="HUMIDITY_AVE"></asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="STD" DataField="HUMIDITY_STD" SortExpression="HUMIDITY_STD"></asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="AVE" DataField="TEMP_AVE" SortExpression="TEMP_AVE"></asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderStyle-Width="10%" HeaderText="STD" DataField="TEMP_STD" SortExpression="TEMP_STD"></asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderText="BranchID" DataField="BranchID" visible="false"></asp:BoundColumn>
                                                                    </Columns>
                                                                </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                             <tr><td style="height: 5px"></td></tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>                                             
                                            <tr>
                                                <td align="right">
                                                    <asp:Button ID="BTN_MakePDF" OnClick="BTN_MakePDF_Click" runat="server" CssClass="green2-btn"></asp:Button>
                                                    <asp:Button ID="BTN_BackToSearch" OnClientClick="BackToSearch()" OnClick="BTN_BackToSearch_Click" runat="server" CssClass="green2-btn"></asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="BTN_MakePDF" />
    </Triggers>
</asp:UpdatePanel>
