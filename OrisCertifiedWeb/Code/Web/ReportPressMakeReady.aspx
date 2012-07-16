<%@ Page Title="" Language="C#" MasterPageFile="~/Home.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="ReportPressMakeReady.aspx.cs" Inherits="ReportPressMakeReady" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>--%>
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
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:Panel Style="width: 100%;" ID="PNL_Search" runat="server">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="padding: 5px;" class="TABLE1">
                                                    <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                                                        <tr>
                                                            <td class="green-title" valign="middle" align="left">
                                                                <asp:Label ID="lblReportTitle" runat="server" Text="Make Ready Report (Search Criteria)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" class="green-form-bg" style="border-bottom: none" id="TDReportTitle"
                                                                runat="server">
                                                                <asp:Label ID="LblError" runat="server" CssClass="ERROR"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="maintable">
                                                        <tr>
                                                            <td align="center" valign="top" class="green-form-bg" style="border-top: none; padding-left: 0px;">
                                                                <table width="100%" border="0" cellspacing="2" cellpadding="3" class="form-text-rep">
                                                                    <tr>
                                                                        <td colspan="4" align="center">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 23%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td style="width: 20%">
                                                                            <asp:Literal ID="LTRL_Company" runat="server" Text="Company"></asp:Literal>
                                                                        </td>
                                                                        <td style="width: 34%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td style="width: 23%">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="DDL_Company" runat="server" CssClass="DROPDOWNLIST" AutoPostBack="true"
                                                                                Width="90%" OnSelectedIndexChanged="DDL_Company_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <asp:Literal ID="LTRL_Branch" runat="server" Text="Branch"></asp:Literal>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Literal ID="LTRL_PressDevice" runat="server" Text="Press"></asp:Literal>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                        <td valign="top">
                                                                            <asp:Panel ID="PNL_Branch" runat="server" ScrollBars="Auto" CssClass="CHECKBOXLIST"
                                                                                Visible="false">
                                                                                <asp:CheckBoxList ID="CHK_Branch" runat="server" CssClass="CHECKBOX" AutoPostBack="true"
                                                                                    Width="150px" OnSelectedIndexChanged="CHK_Branch_SelectedIndexChanged">
                                                                                </asp:CheckBoxList>
                                                                            </asp:Panel>
                                                                        </td>
                                                                        <td valign="top">
                                                                            <asp:Panel ID="PNL_PressChkList" runat="server" ScrollBars="Auto" CssClass="CHECKBOXLIST"
                                                                                Visible="false">
                                                                                <asp:CheckBoxList ID="CHK_Press" runat="server" CssClass="CHECKBOX">
                                                                                </asp:CheckBoxList>
                                                                            </asp:Panel>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                        <td colspan="2" style="padding: 0px;" align="center">
                                                                            <fieldset style="width: 100%; border-right: #000000 1px solid; border-top: #000000 1px solid;
                                                                                border-left: #000000 1px solid; border-bottom: #000000 1px solid;">
                                                                                <legend style="margin-left: 15px;" class="form-text-rep">
                                                                                    Filter</legend>
                                                                                <table border="0" cellspacing="2" cellpadding="3" class="form-text-rep" width="90%">
                                                                                    <tr>
                                                                                        <td style="width: 27%; height: 21px;" colspan="2">
                                                                                            <asp:Literal ID="Literal1" runat="server" Text="Job"></asp:Literal>
                                                                                        </td>
                                                                                        <td style="width: 27%; height: 21px;" colspan="2">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="2">
                                                                                            <asp:TextBox ID="txtJobName" runat="server"></asp:TextBox>
                                                                                        </td>
                                                                                        <td colspan="2">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td valign="top" align="left">
                                                                                            From
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="TXT_FromDate" runat="server" CssClass="TEXTBOX" AutoPostBack="True"
                                                                                                OnTextChanged="TXT_DateText" Columns="10" MaxLength="10" Width="75px"></asp:TextBox>&nbsp;<asp:ImageButton
                                                                                                    ID="IBTN_FromDate" runat="server" ImageAlign="AbsMiddle" ImageUrl="Images/calendar-icon.png">
                                                                                                </asp:ImageButton><ajaxToolkit:CalendarExtender ID="Cal_FromDate" runat="server"
                                                                                                    TargetControlID="TXT_FromDate" PopupButtonID="IBTN_FromDate">
                                                                                                </ajaxToolkit:CalendarExtender>
                                                                                        </td>
                                                                                        <td valign="top" align="left">
                                                                                            To
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="TXT_ToDate" runat="server" CssClass="TEXTBOX" AutoPostBack="True"
                                                                                                OnTextChanged="TXT_DateText" Columns="10" MaxLength="10" Width="75px"></asp:TextBox>&nbsp;<asp:ImageButton
                                                                                                    ID="IBTN_ToDate" runat="server" ImageAlign="AbsMiddle" ImageUrl="Images/calendar-icon.png">
                                                                                                </asp:ImageButton><ajaxToolkit:CalendarExtender ID="Cal_ToDate" runat="server" TargetControlID="TXT_ToDate"
                                                                                                    PopupButtonID="IBTN_ToDate">
                                                                                                </ajaxToolkit:CalendarExtender>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td valign="middle" align="left">
                                                                                            From
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <table border="0" cellspacing="0" cellpadding="0" class="form-text-rep" width="100%">
                                                                                                <tr>
                                                                                                    <td style="padding: 0px;">
                                                                                                        <asp:DropDownList ID="DDL_FromHours" runat="server" CssClass="DROPDOWNLIST" Width="40px">
                                                                                                            <asp:ListItem Value="00">12</asp:ListItem>
                                                                                                            <asp:ListItem Value="01">01</asp:ListItem>
                                                                                                            <asp:ListItem Value="02">02</asp:ListItem>
                                                                                                            <asp:ListItem Value="03">03</asp:ListItem>
                                                                                                            <asp:ListItem Value="04">04</asp:ListItem>
                                                                                                            <asp:ListItem Value="05">05</asp:ListItem>
                                                                                                            <asp:ListItem Value="06">06</asp:ListItem>
                                                                                                            <asp:ListItem Value="07">07</asp:ListItem>
                                                                                                            <asp:ListItem Value="08">08</asp:ListItem>
                                                                                                            <asp:ListItem Value="09">09</asp:ListItem>
                                                                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td style="padding: 2px;">
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td style="padding: 0px;">
                                                                                                        <asp:DropDownList ID="DDL_FromMins" runat="server" CssClass="DROPDOWNLIST" Width="40px">
                                                                                                            <asp:ListItem Value="00">00</asp:ListItem>
                                                                                                            <asp:ListItem Value="01">01</asp:ListItem>
                                                                                                            <asp:ListItem Value="02">02</asp:ListItem>
                                                                                                            <asp:ListItem Value="03">03</asp:ListItem>
                                                                                                            <asp:ListItem Value="04">04</asp:ListItem>
                                                                                                            <asp:ListItem Value="05">05</asp:ListItem>
                                                                                                            <asp:ListItem Value="06">06</asp:ListItem>
                                                                                                            <asp:ListItem Value="07">07</asp:ListItem>
                                                                                                            <asp:ListItem Value="08">08</asp:ListItem>
                                                                                                            <asp:ListItem Value="09">09</asp:ListItem>
                                                                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                                                                            <asp:ListItem Value="12">12</asp:ListItem>
                                                                                                            <asp:ListItem Value="13">13</asp:ListItem>
                                                                                                            <asp:ListItem Value="14">14</asp:ListItem>
                                                                                                            <asp:ListItem Value="15">15</asp:ListItem>
                                                                                                            <asp:ListItem Value="16">16</asp:ListItem>
                                                                                                            <asp:ListItem Value="17">17</asp:ListItem>
                                                                                                            <asp:ListItem Value="18">18</asp:ListItem>
                                                                                                            <asp:ListItem Value="19">19</asp:ListItem>
                                                                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                                                                            <asp:ListItem Value="21">21</asp:ListItem>
                                                                                                            <asp:ListItem Value="22">22</asp:ListItem>
                                                                                                            <asp:ListItem Value="23">23</asp:ListItem>
                                                                                                            <asp:ListItem Value="24">24</asp:ListItem>
                                                                                                            <asp:ListItem Value="25">25</asp:ListItem>
                                                                                                            <asp:ListItem Value="26">26</asp:ListItem>
                                                                                                            <asp:ListItem Value="27">27</asp:ListItem>
                                                                                                            <asp:ListItem Value="28">28</asp:ListItem>
                                                                                                            <asp:ListItem Value="29">29</asp:ListItem>
                                                                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                                                                            <asp:ListItem Value="31">31</asp:ListItem>
                                                                                                            <asp:ListItem Value="32">32</asp:ListItem>
                                                                                                            <asp:ListItem Value="33">33</asp:ListItem>
                                                                                                            <asp:ListItem Value="34">34</asp:ListItem>
                                                                                                            <asp:ListItem Value="35">35</asp:ListItem>
                                                                                                            <asp:ListItem Value="36">36</asp:ListItem>
                                                                                                            <asp:ListItem Value="37">37</asp:ListItem>
                                                                                                            <asp:ListItem Value="38">38</asp:ListItem>
                                                                                                            <asp:ListItem Value="39">39</asp:ListItem>
                                                                                                            <asp:ListItem Value="40">40</asp:ListItem>
                                                                                                            <asp:ListItem Value="41">41</asp:ListItem>
                                                                                                            <asp:ListItem Value="42">42</asp:ListItem>
                                                                                                            <asp:ListItem Value="43">43</asp:ListItem>
                                                                                                            <asp:ListItem Value="44">44</asp:ListItem>
                                                                                                            <asp:ListItem Value="45">45</asp:ListItem>
                                                                                                            <asp:ListItem Value="46">46</asp:ListItem>
                                                                                                            <asp:ListItem Value="47">47</asp:ListItem>
                                                                                                            <asp:ListItem Value="48">48</asp:ListItem>
                                                                                                            <asp:ListItem Value="49">49</asp:ListItem>
                                                                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                                                                            <asp:ListItem Value="51">51</asp:ListItem>
                                                                                                            <asp:ListItem Value="52">52</asp:ListItem>
                                                                                                            <asp:ListItem Value="53">53</asp:ListItem>
                                                                                                            <asp:ListItem Value="54">54</asp:ListItem>
                                                                                                            <asp:ListItem Value="55">55</asp:ListItem>
                                                                                                            <asp:ListItem Value="56">56</asp:ListItem>
                                                                                                            <asp:ListItem Value="57">57</asp:ListItem>
                                                                                                            <asp:ListItem Value="58">58</asp:ListItem>
                                                                                                            <asp:ListItem Value="59">59</asp:ListItem>
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td style="padding: 0px;">
                                                                                                        <asp:RadioButtonList ID="RBTN_FromAMPM" runat="server" CssClass="RADIOBUTTONLIST"
                                                                                                            RepeatDirection="horizontal">
                                                                                                            <asp:ListItem Value="0" Selected="true">AM</asp:ListItem>
                                                                                                            <asp:ListItem Value="12">PM</asp:ListItem>
                                                                                                        </asp:RadioButtonList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td valign="middle" align="left">
                                                                                            To
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <table border="0" cellpadding="0" cellspacing="0" class="form-text-rep" width="100%">
                                                                                                <tr>
                                                                                                    <td style="padding: 0px;">
                                                                                                        <asp:DropDownList ID="DDL_ToHours" runat="server" CssClass="DROPDOWNLIST" Width="40px">
                                                                                                            <asp:ListItem Value="00">12</asp:ListItem>
                                                                                                            <asp:ListItem Value="01">01</asp:ListItem>
                                                                                                            <asp:ListItem Value="02">02</asp:ListItem>
                                                                                                            <asp:ListItem Value="03">03</asp:ListItem>
                                                                                                            <asp:ListItem Value="04">04</asp:ListItem>
                                                                                                            <asp:ListItem Value="05">05</asp:ListItem>
                                                                                                            <asp:ListItem Value="06">06</asp:ListItem>
                                                                                                            <asp:ListItem Value="07">07</asp:ListItem>
                                                                                                            <asp:ListItem Value="08">08</asp:ListItem>
                                                                                                            <asp:ListItem Value="09">09</asp:ListItem>
                                                                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                                                                            <asp:ListItem Selected="true" Value="11">11</asp:ListItem>
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td style="padding: 2px;">
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td style="padding: 0px;">
                                                                                                        <asp:DropDownList ID="DDL_ToMins" runat="server" CssClass="DROPDOWNLIST" Width="40px">
                                                                                                            <asp:ListItem Value="00">00</asp:ListItem>
                                                                                                            <asp:ListItem Value="01">01</asp:ListItem>
                                                                                                            <asp:ListItem Value="02">02</asp:ListItem>
                                                                                                            <asp:ListItem Value="03">03</asp:ListItem>
                                                                                                            <asp:ListItem Value="04">04</asp:ListItem>
                                                                                                            <asp:ListItem Value="05">05</asp:ListItem>
                                                                                                            <asp:ListItem Value="06">06</asp:ListItem>
                                                                                                            <asp:ListItem Value="07">07</asp:ListItem>
                                                                                                            <asp:ListItem Value="08">08</asp:ListItem>
                                                                                                            <asp:ListItem Value="09">09</asp:ListItem>
                                                                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                                                                            <asp:ListItem Value="12">12</asp:ListItem>
                                                                                                            <asp:ListItem Value="13">13</asp:ListItem>
                                                                                                            <asp:ListItem Value="14">14</asp:ListItem>
                                                                                                            <asp:ListItem Value="15">15</asp:ListItem>
                                                                                                            <asp:ListItem Value="16">16</asp:ListItem>
                                                                                                            <asp:ListItem Value="17">17</asp:ListItem>
                                                                                                            <asp:ListItem Value="18">18</asp:ListItem>
                                                                                                            <asp:ListItem Value="19">19</asp:ListItem>
                                                                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                                                                            <asp:ListItem Value="21">21</asp:ListItem>
                                                                                                            <asp:ListItem Value="22">22</asp:ListItem>
                                                                                                            <asp:ListItem Value="23">23</asp:ListItem>
                                                                                                            <asp:ListItem Value="24">24</asp:ListItem>
                                                                                                            <asp:ListItem Value="25">25</asp:ListItem>
                                                                                                            <asp:ListItem Value="26">26</asp:ListItem>
                                                                                                            <asp:ListItem Value="27">27</asp:ListItem>
                                                                                                            <asp:ListItem Value="28">28</asp:ListItem>
                                                                                                            <asp:ListItem Value="29">29</asp:ListItem>
                                                                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                                                                            <asp:ListItem Value="31">31</asp:ListItem>
                                                                                                            <asp:ListItem Value="32">32</asp:ListItem>
                                                                                                            <asp:ListItem Value="33">33</asp:ListItem>
                                                                                                            <asp:ListItem Value="34">34</asp:ListItem>
                                                                                                            <asp:ListItem Value="35">35</asp:ListItem>
                                                                                                            <asp:ListItem Value="36">36</asp:ListItem>
                                                                                                            <asp:ListItem Value="37">37</asp:ListItem>
                                                                                                            <asp:ListItem Value="38">38</asp:ListItem>
                                                                                                            <asp:ListItem Value="39">39</asp:ListItem>
                                                                                                            <asp:ListItem Value="40">40</asp:ListItem>
                                                                                                            <asp:ListItem Value="41">41</asp:ListItem>
                                                                                                            <asp:ListItem Value="42">42</asp:ListItem>
                                                                                                            <asp:ListItem Value="43">43</asp:ListItem>
                                                                                                            <asp:ListItem Value="44">44</asp:ListItem>
                                                                                                            <asp:ListItem Value="45">45</asp:ListItem>
                                                                                                            <asp:ListItem Value="46">46</asp:ListItem>
                                                                                                            <asp:ListItem Value="47">47</asp:ListItem>
                                                                                                            <asp:ListItem Value="48">48</asp:ListItem>
                                                                                                            <asp:ListItem Value="49">49</asp:ListItem>
                                                                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                                                                            <asp:ListItem Value="51">51</asp:ListItem>
                                                                                                            <asp:ListItem Value="52">52</asp:ListItem>
                                                                                                            <asp:ListItem Value="53">53</asp:ListItem>
                                                                                                            <asp:ListItem Value="54">54</asp:ListItem>
                                                                                                            <asp:ListItem Value="55">55</asp:ListItem>
                                                                                                            <asp:ListItem Value="56">56</asp:ListItem>
                                                                                                            <asp:ListItem Value="57">57</asp:ListItem>
                                                                                                            <asp:ListItem Value="58">58</asp:ListItem>
                                                                                                            <asp:ListItem Selected="true" Value="59">59</asp:ListItem>
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td style="padding: 0px;">
                                                                                                        <asp:RadioButtonList ID="RBTN_ToAMPM" runat="server" CssClass="RADIOBUTTONLIST" RepeatDirection="horizontal">
                                                                                                            <asp:ListItem Value="0">AM</asp:ListItem>
                                                                                                            <asp:ListItem Selected="true" Value="12">PM</asp:ListItem>
                                                                                                        </asp:RadioButtonList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </fieldset>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4" align="center">
                                                                            <asp:Button ID="BTN_CreateReport" OnClick="BTN_CreateReport_Click" runat="server"
                                                                                CssClass="green2-btn" Text="Create Report"></asp:Button>
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
                                    <br />
                                    <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
                                    <ajaxToolkit:ModalPopupExtender runat="server" ID="programmaticModalPopup" BehaviorID="programmaticModalPopupBehavior"
                                        Y="50" TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="programmaticPopup"
                                        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="programmaticPopupDragHandle">
                                    </ajaxToolkit:ModalPopupExtender>
                                    <asp:Panel runat="server" ID="programmaticPopup" Style="display: none; height: 500px;
                                        width: 700; background-color: White; border: solid 1px #000000;" ScrollBars="Auto">
                                        <a id="hideModalPopupViaClientButton" href="#" style="border: none; text-decoration: none;">
                                            <img src="Images/Close2.png" style="position: absolute; border: none; right: 10px;
                                                top: 9px; text-align: right;" />
                                        </a>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="padding: 5px;" class="TABLE1">
                                                    <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                                                        <tr>
                                                            <td class="green-title" valign="middle" align="left">
                                                                <asp:Label ID="Label1" runat="server" Text="Make Ready Report"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table style="width: 100%" cellspacing="0" cellpadding="0" align="center" border="0">
                                                        <tr align="center">
                                                            <td colspan="2">
                                                                <br />
                                                                <b>Make Ready Report</b><br />
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="width: 50%">
                                                                <table cellpadding="2" width="100%" cellspacing="0" border="0" class="popup-table"
                                                                    style="margin-top: 5px">
                                                                    <tr class="popup-table-td" valign="top">
                                                                        <td style="padding: 5px">
                                                                            <div class="green-title">
                                                                                Report Parameters</div>
                                                                            <table cellpadding="0" cellspacing="0" class="DATAGRID" border="0" style="width: 100%">
                                                                                <tr class="ITEMSTYLE">
                                                                                    <td align="left" style="width: 50%">
                                                                                        <b>Date Range&nbsp;&nbsp;</b>
                                                                                    </td>
                                                                                    <td style="width: 50%">
                                                                                        &nbsp;&nbsp;
                                                                                        <asp:Literal ID="LTRL_DateRange" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ALTERNATINGITEMSTYLE">
                                                                                    <td align="left">
                                                                                        <b>Time Range&nbsp;&nbsp;</b>
                                                                                    </td>
                                                                                    <td>
                                                                                        &nbsp;&nbsp;<asp:Literal ID="LTRL_TimeRange" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ITEMSTYLE">
                                                                                    <td align="left">
                                                                                        <b>Company&nbsp;&nbsp;</b>
                                                                                    </td>
                                                                                    <td>
                                                                                        &nbsp;&nbsp;<asp:Literal ID="LTRL_CompanyName" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ALTERNATINGITEMSTYLE">
                                                                                    <td align="left">
                                                                                        <b>Branch&nbsp;&nbsp;</b>
                                                                                    </td>
                                                                                    <td>
                                                                                        &nbsp;&nbsp;<asp:Literal ID="LTRL_BranchName" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="ITEMSTYLE">
                                                                                    <td align="left">
                                                                                        <b>Job Run Name&nbsp;&nbsp;</b>
                                                                                    </td>
                                                                                    <td>
                                                                                        &nbsp;&nbsp;<asp:Literal ID="LTRL_JobRunName" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                </tr>
                                                                                
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td valign="top">
                                                                 <table cellpadding="2" width="100%" cellspacing="0" border="0" class="popup-table"
                                                                    style="margin-top: 5px">
                                                                    <tr class="popup-table-td" valign="top">
                                                                        <td style="padding: 5px"><b>Selected Press</b>
                                                                            <div class="green-title"></div>
                                                                            <table cellpadding="0" cellspacing="0" class="DATAGRID" border="0" style="width: 100%">
                                                                               <asp:Literal ID="LTRL_PressName" runat="server"></asp:Literal>
                                                                                
                                                                               
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr id="TRCompanySummary" runat="server">
                                                            <td colspan="2">
                                                                <div class="green-title">
                                                                    Company Summary</div>
                                                                <table border="0" cellpadding="0" cellspacing="0" class="DATAGRID" style="width: 100%">
                                                                    <tr class="HEADERSTYLE">
                                                                        <td style="width: 15%">
                                                                            Company Name
                                                                        </td>
                                                                        <td>
                                                                            #Of Jobs
                                                                        </td>
                                                                        <td>
                                                                            Ave. MR Sheets/Job
                                                                        </td>
                                                                        <td>
                                                                            Total MR Sheets
                                                                        </td>
                                                                        <td>
                                                                            Ave MR Min/Job
                                                                        </td>
                                                                        <td>
                                                                            Total MR Time
                                                                        </td>
                                                                        <td>
                                                                            Ave Error %
                                                                        </td>
                                                                        <td>
                                                                            Ave Delta E
                                                                        </td>
                                                                    </tr>
                                                                    <asp:Label ID="LBL_CompanyDetail" runat="server" Text=""></asp:Label>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr id="TRBranchSummary" runat="server">
                                                            <td colspan="2">
                                                                <div class="green-title">
                                                                    Branch Summary</div>
                                                                <table border="0" cellpadding="0" cellspacing="0" class="DATAGRID" style="width: 100%">
                                                                    <tr class="HEADERSTYLE">
                                                                        <td style="width: 15%">
                                                                            Branch Name
                                                                        </td>
                                                                        <td>
                                                                            #Of Jobs
                                                                        </td>
                                                                        <td>
                                                                            Ave. MR Sheets/Job
                                                                        </td>
                                                                        <td>
                                                                            Total MR Sheets
                                                                        </td>
                                                                        <td>
                                                                            Ave MR Min/Job
                                                                        </td>
                                                                        <td>
                                                                            Total MR Time
                                                                        </td>
                                                                        <td>
                                                                            Ave Error %
                                                                        </td>
                                                                        <td>
                                                                            Ave Delta E
                                                                        </td>
                                                                    </tr>
                                                                    <asp:Label ID="LBL_BranchDetail" runat="server" Text=""></asp:Label>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" align="center">
                                                                <asp:Button ID="BTN_CreatePDF" OnClick="BTN_CreatePDF_Click" runat="server" CssClass="green2-btn"
                                                                    Text="Create PDF"></asp:Button>
                                                            </td>
                                                        </tr>
                                                        <%--    <tr>
                                                            <td colspan="2">
                                                                <div class="green-title">
                                                                    Press Summary</div>
                                                                <table border="0" cellpadding="0" cellspacing="0" class="DATAGRID" style="width: 100%">
                                                                    <tr class="HEADERSTYLE">
                                                                        <td style="width: 15%">
                                                                            Press Name
                                                                        </td>
                                                                        <td>
                                                                            #Of Jobs
                                                                        </td>
                                                                        <td>
                                                                            Ave. MR Sheets/Job
                                                                        </td>
                                                                        <td>
                                                                            Total MR Sheets
                                                                        </td>
                                                                        <td>
                                                                            Ave MR Min/Job
                                                                        </td>
                                                                        <td>
                                                                            Total MR Time
                                                                        </td>
                                                                        <td>
                                                                            Ave Error %
                                                                        </td>
                                                                        <td>
                                                                            Ave Delta E
                                                                        </td>
                                                                    </tr>
                                                                    <asp:Label ID="LBL_PressDetail" runat="server" Text=""></asp:Label>
                                                                </table>
                                                            </td>
                                                        </tr>--%>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="BTN_CreatePDF" />
                                </Triggers>
                            </asp:UpdatePanel>
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

    <script language="javascript" type="text/javascript">
        function pageLoad() {
            $addHandler($get("hideModalPopupViaClientButton"), 'click', hideModalPopupViaClient);
        }

        function hideModalPopupViaClient(ev) {
            ev.preventDefault();
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.hide();
        }

        function OpenMeasurementWindow() {
            if (screen.width <= 1024) {
                document.getElementById("<%=programmaticPopup.ClientID %>").style.height = "630px";
            }
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.show();

        }    
    </script>

</asp:Content>
