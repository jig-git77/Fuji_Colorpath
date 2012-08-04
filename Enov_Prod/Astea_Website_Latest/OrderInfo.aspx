<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderInfo.aspx.cs" Inherits="OrderInfo" %>
<%@ PreviousPageType VirtualPath="~/AssignedOrders.aspx" %>
<%@ Register src="ProgressBar.ascx" tagname="ProgressBar" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>OrderInfo</title>
<script type="text/javascript" src="include/Validation.js"></script>   
<link href="stylesheet/style.css" rel="stylesheet" type="text/css" />
</head>
<body id="PageBody" runat="server">
    <form id="form3" runat="server" defaultbutton="SaveButton">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional"><ContentTemplate>
 <uc1:ProgressBar ID="ProgressBar1" runat="server" />
<table style="width: 640px" class="maintable">
<tr>
<td colspan="2" style="width: 640px; height: 33px;"> <fieldset style="width: 640px; border-right: #000000 1px solid; border-top: #000000 1px solid; border-left: #000000 1px solid; border-bottom: #000000 1px solid;"><legend style="font-size: 10pt; font-family: Verdana;margin-left: 25px;"> Service Call Information</legend><table width="640" class="maintable">
  <tr>
    <td width="105" style="height: 26px"><asp:Label ID="OrderLabel" runat="server" Text="Order ID" Width="90px" CssClass="font_verdana"></asp:Label></td>
    <td style="width: 179px; height: 26px;"><asp:TextBox ID="OrderTxt" runat="server"  Font-Size="Smaller" ReadOnly="True" CssClass="txtbox" BackColor="WhiteSmoke" ></asp:TextBox></td>
    <td width="133" style="height: 26px">&nbsp;</td>
    <td width="166" style="height: 26px">&nbsp;</td>
  </tr>
  <tr>
    <td style="height: 14px"><asp:Label ID="CustLabel" runat="server" Text="Customer ID" Width="90px" CssClass="font_verdana"></asp:Label></td>
    <td style="width: 179px; height: 14px;"><asp:TextBox ID="CustTxt" runat="server" Font-Size="Small" ReadOnly="True" CssClass="txtbox" BackColor="WhiteSmoke"></asp:TextBox></td>
    <td style="height: 14px">
<asp:Label ID="lblCustName" runat="server" CssClass="font_verdana" Text="Customer Name"></asp:Label></td>
    <td style="height: 14px"><asp:TextBox ID="CustNameTxt" runat="server" Font-Size="Small" Width="150px" ReadOnly="True" CssClass="txtbox" BackColor="WhiteSmoke"></asp:TextBox></td>
  </tr>
  <tr>
    <td><asp:Label ID="SiteLabel" runat="server" Text="Site Id" Width="90px" CssClass="font_verdana"></asp:Label></td>
    <td style="width: 179px"><asp:TextBox ID="SiteTxt" runat="server" Font-Size="Small" ReadOnly="True" CssClass="txtbox" BackColor="WhiteSmoke"></asp:TextBox></td>
    <td><asp:Label ID="SiteNameLabel" runat="server" Text="Site Name" Width="90px" CssClass="font_verdana"></asp:Label></td>
    <td><asp:TextBox ID="SiteNameTxt" runat="server" Font-Size="Small" Width="150px" ReadOnly="True" CssClass="txtbox" BackColor="WhiteSmoke"></asp:TextBox></td>
  </tr>
  <tr>
    <td><asp:Label ID="RegLabel" runat="server" Text="Region" Width="90px" CssClass="font_verdana"></asp:Label></td>
    <td style="width: 179px"><asp:TextBox ID="RgnTxt" runat="server" Font-Size="Small" ReadOnly="True" CssClass="txtbox" BackColor="WhiteSmoke"></asp:TextBox></td>
    <td><asp:Label ID="POLabel" runat="server" Text="PO" Width="90px" CssClass="font_verdana"></asp:Label></td>
    <td><asp:TextBox ID="POTxt" runat="server" Width="90px" BackColor="WhiteSmoke" ReadOnly="false" CssClass="txtbox"></asp:TextBox></td>
  </tr>
  <tr>
    <td><asp:Label ID="Label1" runat="server" Text="Open Date" Width="90px" CssClass="font_verdana"></asp:Label></td>
    <td style="width: 179px"><asp:TextBox ID="OpenDtTxt" runat="server" Font-Size="Small" ReadOnly="True" CssClass="txtbox" BackColor="WhiteSmoke"></asp:TextBox></td>
    <td><asp:Label ID="ReqLabel" runat="server" Text="Req. Type" Width="90px" CssClass="font_verdana"></asp:Label></td>
    <td><asp:TextBox ID="ReqTxt" runat="server" Font-Size="Small" Width="90px" ReadOnly="True" CssClass="txtbox" BackColor="WhiteSmoke"></asp:TextBox></td>
  </tr>
  <tr>
    <td><asp:Label ID="ManuLabel" runat="server" Text="Manufacturer" Width="90px" CssClass="font_verdana"></asp:Label></td>
    <td style="width: 179px"><asp:TextBox ID="ManuTxt" runat="server" Font-Size="Small" ReadOnly="True" CssClass="txtbox" BackColor="WhiteSmoke"></asp:TextBox></td>
    <td><asp:Label ID="SerialLabel" runat="server" Text="Serial No." Width="90px" CssClass="font_verdana"></asp:Label></td>
    <td><asp:TextBox ID="SerialTxt" runat="server" Font-Size="Small" Width="90px" ReadOnly="True" CssClass="txtbox" BackColor="WhiteSmoke"></asp:TextBox></td>
  </tr>
  <tr>
    <td><asp:Label ID="ModelLabel" runat="server" Text="Model" Width="90px" CssClass="font_verdana"></asp:Label></td>
    <td style="width: 179px"><asp:TextBox ID="ModelTxt" runat="server" Font-Size="Small" ReadOnly="True" CssClass="txtbox" BackColor="WhiteSmoke"></asp:TextBox></td>
    <td><asp:Label ID="InstLabel" runat="server" Text="Inst. Date" Width="90px" CssClass="font_verdana"></asp:Label></td>
    <td><asp:TextBox ID="InstTxt" runat="server" Font-Size="Small" Width="90px" ReadOnly="True" CssClass="txtbox" BackColor="WhiteSmoke"></asp:TextBox></td>
  </tr>
  <tr>
    <td><asp:Label ID="ContLabel" runat="server" Text="Contract" Width="90px" CssClass="font_verdana"></asp:Label></td>
    <td style="width: 179px"><asp:TextBox ID="ContTxt" runat="server" Font-Size="Small" ReadOnly="True" CssClass="txtbox" BackColor="WhiteSmoke"></asp:TextBox></td>
    <td><asp:Label ID="ContDescLabel" runat="server" Text="Cont. Descr." Width="90px" CssClass="font_verdana"></asp:Label></td>
    <td><asp:TextBox ID="ContDescTxt" runat="server" Font-Size="Small" Width="150px" CssClass="txtbox" BackColor="WhiteSmoke"></asp:TextBox></td>
  </tr>
  <tr>
    <td><asp:Label ID="WarrLabel" runat="server" Text="Warranty" Width="90px" CssClass="font_verdana"></asp:Label></td>
    <td style="width: 179px"><asp:TextBox ID="WarrTxt" runat="server" Font-Size="Small" ReadOnly="True" CssClass="txtbox" BackColor="WhiteSmoke"></asp:TextBox></td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
</table></fieldset><br /><br />   <fieldset style="width: 640px; border-right: #000000 1px solid; border-top: #000000 1px solid; border-left: #000000 1px solid; border-bottom: #000000 1px solid;"><legend style="font-size: 10pt; font-family: Verdana;margin-left: 25px;">Customer Information</legend>
<table style="width: 640px">
<tr>
<td width="80" style="width: 60px; height: 21px"><asp:Label ID="AddressLabel" runat="server" Text="Address"  Width="66px" CssClass="font_verdana"></asp:Label></td>
<td colspan="5" style="width: 100px; height: 21px"><asp:TextBox ID="StreetTxt" runat="server"  ReadOnly="True" CssClass="txtbox" Width="403px" BackColor="WhiteSmoke"></asp:TextBox></td>
</tr>
<tr>
<td style="width: 60px; height: 21px"><asp:Label ID="CityLabel" runat="server" Text="City" Width="64px" CssClass="font_verdana"></asp:Label></td>
<td style="width: 46px; height: 21px"><asp:TextBox ID="CityTxt" runat="server" ReadOnly="True" CssClass="txtbox" BackColor="WhiteSmoke"></asp:TextBox></td>
<td style="width: 43px; height: 21px" align="right"><asp:Label ID="StateLabel" runat="server" Text="State" CssClass="font_verdana"></asp:Label></td>
<td style="height: 21px; width: 137px;"><asp:TextBox ID="StateTxt" runat="server" Width="50px"  ReadOnly="True" CssClass="txtbox" BackColor="WhiteSmoke"></asp:TextBox></td>
<td style="height:21px; width: 6px;" align="right"><asp:Label ID="ZipLabel" runat="server" Text="Zip" CssClass="font_verdana"></asp:Label></td>
<td style="height: 21px"><asp:TextBox ID="ZipTxt" runat="server" Width="86px"  ReadOnly="True" CssClass="txtbox" BackColor="WhiteSmoke"></asp:TextBox></td>
</tr>
<tr>
  <td style="width: 60px; height: 21px">
        <asp:Label ID="ContactLbl" runat="server" Text="Contact" Width="64px" CssClass="font_verdana"></asp:Label></td>
  <td style="width: 46px; height: 21px">
        <asp:TextBox ID="ContactText" ReadOnly="true" Font-Size="Small" runat="server" Width="140px" CssClass="txtbox" BackColor="WhiteSmoke"></asp:TextBox></td>
  <td style="width: 43px; height: 21px" align="right"><asp:Label ID="PhoneLabel" runat="server" Text="Phone" Width="48px" CssClass="font_verdana"></asp:Label></td>
  <td style="width: 137px; height: 21px"><asp:TextBox ID="PhoneTxt" ReadOnly="true" Font-Size="Small" runat="server" Width="147px" CssClass="txtbox" BackColor="WhiteSmoke"></asp:TextBox></td>
</tr>
</table>
</fieldset>
<br />
<table style="width: 640px">
<tr>
<td style="width: 647px; height: 13px"><asp:Label ID="Label2" runat="server" Text="Problem  Description" Width="133px" style="font-size: 10pt; font-family: Verdana;"></asp:Label></td>
</tr>
<tr>
<td style="height: 25px; width: 647px;"><asp:TextBox ID="PDescTxt" runat="server"  TextMode="MultiLine" MaxLength="254" Height="58px" Width="640px" ReadOnly="True" CssClass="txtbox"></asp:TextBox></td>
</tr>
<tr>
<td style="width: 647px; height: 20px"></td>
</tr>
<asp:UpdatePanel ID="UpdatePanelActivities" runat="server" UpdateMode="Conditional"><ContentTemplate>
<tr>
<td style="width: 647px; height: 20px"><asp:Label ID="ActvLabel" runat="server" Text="Activities" Width="109px" style="font-size: 10pt; font-family: Verdana;"></asp:Label>
<asp:Label ID="ActErrorLabel" runat="server" CssClass="errorlabel"></asp:Label></td></tr>
<tr><td style="width: 647px"><asp:Panel ID="pnlActivities"  runat="server" >
<asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" OnItemDataBound="DataGrid1_ItemDataBound" CssClass="Grid" Width="640px" >
            <Columns>
                <asp:TemplateColumn HeaderText="Update">
                    <itemtemplate>
                        <asp:checkbox runat="server" Enabled="true" id="ActvCheck" /></itemtemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="bpart_descr" SortExpression="bpart_descr" Readonly="True"
                      HeaderText="Activity" >
                   <ItemStyle Width="100px"></ItemStyle>
                </asp:BoundColumn>                
                <asp:TemplateColumn HeaderText="Duration">
                   <ItemTemplate>
                      <asp:TextBox 
                         id="TextBoxDur" runat="server" Width="60px"
                         Text='<%# DataBinder.Eval(Container, "DataItem.duration") %>'>                      </asp:TextBox>
                   </ItemTemplate>
                </asp:TemplateColumn>
                
                <asp:TemplateColumn HeaderText="Start Date">
                   <ItemTemplate>
                      <asp:TextBox 
                         id="TextBoxStDate" runat="server" Width="60px"
                         Text='<%# DataBinder.Eval(Container, "DataItem.arrive_dt", "{0:d}") %>'>                      </asp:TextBox>
                   </ItemTemplate>
                </asp:TemplateColumn>
                
                <asp:TemplateColumn HeaderText="Start Time">
                   <ItemTemplate>
                      <asp:TextBox 
                         id="TextBoxStTime" runat="server" Width="60px"
                         Text='<%# DataBinder.Eval(Container, "DataItem.arrive_tm", "{0:T}" ) %>'>                      </asp:TextBox>
                   </ItemTemplate>
                </asp:TemplateColumn>
                
                 <asp:TemplateColumn HeaderText="End Date">
                   <ItemTemplate>
                      <asp:TextBox 
                         id="TextBoxEndDate" runat="server" Width="60px"
                         Text='<%# DataBinder.Eval(Container, "DataItem.actual_dt", "{0:d}") %>'>                      </asp:TextBox>
                   </ItemTemplate>
                </asp:TemplateColumn>
               <asp:TemplateColumn HeaderText="End Time">
                   <ItemTemplate>
                      <asp:TextBox 
                         id="TextBoxEndTime" runat="server" Width="60px"
                         Text='<%# DataBinder.Eval(Container, "DataItem.actual_tm", "{0:T}") %>' >                      </asp:TextBox>
                   </ItemTemplate>
            </asp:TemplateColumn>
                 <asp:TemplateColumn HeaderText="Status">
                   <ItemTemplate>
                       &nbsp;<asp:Label ID="State" runat="server" Text="Label"></asp:Label>
                   </ItemTemplate>
                </asp:TemplateColumn>
                
                 <asp:TemplateColumn HeaderText="Travel">
                   <ItemTemplate>
                      <asp:TextBox 
                         id="TextBoxTravel" runat="server" Width="60px"
                         Text='<%# DataBinder.Eval(Container, "DataItem.travel_distance") %>'>                      </asp:TextBox>
                   </ItemTemplate>
                </asp:TemplateColumn>                
               <asp:BoundColumn DataField="demand_id" SortExpression="demand_id" 
                      HeaderText="DemandID" ReadOnly="True" Visible="False">
                   <ItemStyle Width="60px"></ItemStyle>
            </asp:BoundColumn>               
            </Columns>  <HeaderStyle CssClass="GridHeader"> </HeaderStyle>

        </asp:DataGrid></asp:Panel><asp:Button ID="ActvUpdate" runat="server" OnClick="ActvUpdate_Click" Text="Update" CssClass="Button" /></td>
</tr>
</ContentTemplate></asp:UpdatePanel>
<tr>
  <td style="height: 20px; width: 647px;">   </td>
</tr>
</table>
<asp:Panel ID="pnlMaterials" runat="server" Visible="false">
<table>
<tr>
 <td style="height: 20px; width: 647px;"><asp:Label ID="MatLabel" runat="server" Text="Materials" Width="76px" style="font-size: 10pt; font-family: Verdana;"></asp:Label>   </td>
</tr>
<tr>
  <td style="width: 740px">     <asp:DataGrid ID="DataGrid2" runat="server"  AutoGenerateColumns="false" Width="640px" CssClass="Grid" EnableViewState="true">
            <Columns>
                <asp:TemplateColumn HeaderText="Update">
                    <itemtemplate>
                        <asp:checkbox runat="server" Enabled="true" id="MatCheck"  />
                    </itemtemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="bpart_id" SortExpression="bpart_id" HeaderText="Product ID" ReadOnly="True">
                   <ItemStyle Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="qty" SortExpression="qty" HeaderText="Quantity" ReadOnly="True">
                   <ItemStyle Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Usage">
                   <ItemTemplate>
                      <asp:DropDownList id="UsageList" runat="server" Width="100px">
                         <asp:ListItem Value="0">NYD</asp:ListItem>
                         <asp:ListItem Value="1">Used/BPE</asp:ListItem>
                         <asp:ListItem Value="2">Not Used/GPE</asp:ListItem>
                      </asp:DropDownList>
                   </ItemTemplate>                   
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="bpart_descr" SortExpression="bpart_descr" 
                      HeaderText="Description" ReadOnly="True">
                   <ItemStyle Width="200px"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Comments">
                   <ItemTemplate>
                      <asp:TextBox 
                         id="TextBoxComment" runat="server" Width="200px" 
                         Text='<%# DataBinder.Eval(Container, "DataItem.comment_text") %>' BackColor="#E0E0E0" ReadOnly="True"></asp:TextBox>
                   </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn  DataField="demand_id" Visible="False" SortExpression="demand_id" 
                      HeaderText="DemandID" ReadOnly="True">
                   <ItemStyle Width="100px"></ItemStyle>
                </asp:BoundColumn> 
                 <asp:BoundColumn  DataField="acuity_printhead" Visible="false"  
                      HeaderText="acuity_printhead" ReadOnly="True">
                   <ItemStyle Width="5px"></ItemStyle>
                </asp:BoundColumn>     
                <asp:TemplateColumn HeaderText="Reason" >
                   <ItemTemplate>
                      <asp:DropDownList id="ddlReplaceReason" runat="server" Width="250px" Font-Size="Smaller" Visible="false" >
                         <asp:ListItem Value="0">--Select (ignore if not used)--</asp:ListItem>  
                         <asp:ListItem Value="1">Printhead Leaking (flush used incorrectly)</asp:ListItem>
                         <asp:ListItem Value="2">Printhead Leaking (flush not used)</asp:ListItem>
                         <asp:ListItem Value="3">Missing Nozzle (flush used incorrectly)</asp:ListItem>
                         <asp:ListItem Value="4">Missing Nozzle (flush not used)</asp:ListItem>   
                         <asp:ListItem Value="5">Deviated Nozzle (flush used incorrectly)</asp:ListItem> 
                         <asp:ListItem Value="6">Deviated Nozzle (flush not used)</asp:ListItem>    
                         <asp:ListItem Value="8">Head Strike</asp:ListItem>    
                         <asp:ListItem Value="7">Other (please specify on Return Tag)</asp:ListItem>  
                      </asp:DropDownList>
                   </ItemTemplate>                   
                </asp:TemplateColumn>     
                   <asp:BoundColumn  DataField="z_udf_float_2" Visible="false"  
                      ReadOnly="True">
                   <ItemStyle Width="5px"></ItemStyle>
                </asp:BoundColumn>               
            </Columns><HeaderStyle CssClass="GridHeader"> </HeaderStyle>
        </asp:DataGrid></td><tr>
<td style="width: 647px"> <asp:Button ID="matUpdate" runat="server" OnClick="matUpdate_Click" Text="Update" CssClass="btn" />
<asp:Label ID="MatErrorLabel" runat="server" Width="398px" CssClass="errorlabel_bold"></asp:Label>        </td>
</tr>
<tr>
  <td style="height: 10px; width: 647px;">   </td>
</tr>
</asp:Panel>
</table>
<table style="width: 640px">
<tr><td style="width: 640px"><asp:Label ID="ExpenLabel" runat="server" Text="Professional Services" Width="192px" style="font-size: 10pt; font-family: Verdana;"></asp:Label></td></tr>
<tr>
  <td style="width: 640px;"><asp:DataGrid ID="DataGrid3" runat="server" AutoGenerateColumns="False" CssClass="Grid"  Width="640px">
            <Columns>
                <asp:TemplateColumn HeaderText="Update">
                    <itemtemplate>
                        <asp:checkbox runat="server" Enabled="true" id="ExpCheck"  />                    </itemtemplate>
                </asp:TemplateColumn>
                
                 <asp:TemplateColumn HeaderText="Expense">
                   <ItemTemplate>
                      <asp:DropDownList id="ExpDropDownList" runat="server" Width="435px" DataSource="<%# GetGenreTable() %>" 
                             DataTextField ="bpart_desc" DataValueField="bpart_id" CssClass="dropDownStyle">                      </asp:DropDownList>
                   </ItemTemplate>                   
                </asp:TemplateColumn>               
               <asp:TemplateColumn HeaderText="Cost">
                   <ItemTemplate>
                      <asp:TextBox 
                         id="TextBoxCost" runat="server" Width="60px"
                         Text='<%# DataBinder.Eval(Container, "DataItem.cost") %>'>                      </asp:TextBox>
                   </ItemTemplate>
            </asp:TemplateColumn>
                
                 <asp:TemplateColumn>
                   <ItemTemplate>
                      <asp:TextBox 
                         id="TextBoxDemand" runat="server" Width="60px" ReadOnly="True" Visible="false"
                         Text='<%# DataBinder.Eval(Container, "DataItem.demand_id") %>'>                      </asp:TextBox>
                   </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
</asp:DataGrid></td>
</tr>
<tr>
  <td style="width: 640px"><asp:Button ID="AddExpense" runat="server" OnClick="Button1_Click" Text="ADD" Width="66px" CssClass="btn" />  
    <asp:Button ID="ExpUpdate" runat="server" OnClick="ExpUpdate_Click" Text="Update Expenses" Width="110px" CssClass="btn" />    
    <asp:Button ID="btnCancelExpense" runat="server" Text="Cancel" OnClick="btnCancelExpense_Click" CssClass="btn" />    
    <asp:Label ID="ExpErrorLabel" runat="server" Width="428px" CssClass="errorlabel_bold"></asp:Label></td>
</tr>
    <tr>
        <td style="width: 647px; height: 21px">
        </td>
    </tr>
<tr>
  <td style="width: 647px; height: 21px"><asp:Label ID="MangLabel" runat="server" Text="Management" Width="103px" style="font-size: 10pt; font-family: Verdana;"></asp:Label>&nbsp;</td>
</tr>
<tr>
  <td style="width: 647px">   <asp:DataGrid ID="DataGrid4" runat="server" AutoGenerateColumns="False" CssClass="Grid" >
            <Columns>
                <asp:TemplateColumn HeaderText="Management Activity">
                   <ItemTemplate>
                      <asp:DropDownList id="MangDropDownList" runat="server" Width="170px"                              DataTextField ="bpart_id" DataValueField="bpart_id">                      </asp:DropDownList>
                   </ItemTemplate>                   
<HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateColumn>                
                <asp:TemplateColumn HeaderText="Comments">
                   <ItemTemplate>
                      <asp:TextBox 
                         id="TextBoxComment" runat="server" Width="200px"  MaxLength="254"
                         Text='<%# DataBinder.Eval(Container, "DataItem.descr") %>'>                      </asp:TextBox>
                   </ItemTemplate>
<HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateColumn>               
            </Columns>
</asp:DataGrid></td>
</tr>
<tr>
  <td style="width: 647px"> <asp:Button ID="AddMngt" runat="server" OnClick="AddMngt_Click" Text="Add Comments" CssClass="btn" />&nbsp;<asp:Button ID="MangInsert" runat="server" OnClick="MangInsert_Click" Text="Insert" CssClass="btn" />&nbsp;<asp:Button
            ID="btnCancelMgt" runat="server" Text="Cancel" OnClick="btnCancelMgt_Click" CssClass="btn" /><asp:Label ID="MangErrorLabel" runat="server" Width="318px" CssClass="errorlabel_bold"></asp:Label></td>
</tr>
</table>
<table width="640" border="0">
  <tr>
    <td style="width: 31px">&nbsp;</td>
    <td width="549">&nbsp;</td>
  </tr>
  <tr>
    <td style="width: 31px"><asp:Label ID="ProblemLabel" runat="server" Text="Problem" Width="64px" CssClass="font_verdana"></asp:Label></td>
    <td ><asp:DropDownList width="232px" ID="PcodeDDList" runat="server" 
             CssClass="dropDownStyle" EnableTheming="True"> </asp:DropDownList>
     </td>
  </tr>
  <tr>
    <td style="width: 31px"><asp:Label ID="CauseLabel" runat="server" Text="Cause" Width="64px" CssClass="font_verdana"></asp:Label></td>
    <td><asp:DropDownList width="183px" ID="CCodeDDList" runat="server" DataSourceID="CcodeDataSource" 
            DataTextField="descr" DataValueField="cause_id" CssClass="dropDownStyle"> </asp:DropDownList>
      <asp:SqlDataSource ID="CcodeDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:EnovationConnectionString %>"
            SelectCommand="SELECT [cause_id], [descr] FROM [cause] UNION select '','' "></asp:SqlDataSource></td>
  </tr>
  <tr>
    <td style="width: 31px"><asp:Label ID="RepairLabel" runat="server" Text="Repair" Width="64px" CssClass="font_verdana"></asp:Label></td>
    <td ><asp:DropDownList width="183px" ID="RcodeDDList" runat="server" DataSourceID="RcodeDataSource"
            DataTextField="descr" DataValueField="rcode_id" CssClass="dropDownStyle"> </asp:DropDownList>
      <asp:SqlDataSource ID="RcodeDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:EnovationConnectionString %>"
            SelectCommand="SELECT [rcode_id], [descr] FROM [rcode] UNION select '','' "></asp:SqlDataSource></td>
  </tr>
</table>
     <br />
     <asp:UpdatePanel ID="UpdatePanelResText" runat="server" UpdateMode="Conditional"><ContentTemplate>
<table><tr>
  <td colspan="2"><asp:Label ID="ResTLabel" runat="server" Text="Resolve Text" Width="130px" style="font-size: 10pt; font-family: Verdana;"></asp:Label></td>
  </tr>
<tr>
  <td colspan="2"><asp:TextBox ID="ResolveTxt"  MaxLength="2000" TextMode="MultiLine" Wrap="true" BackColor="Transparent" runat="server" Height="126px" Width="640px" CssClass="txtbox"></asp:TextBox></td>
</tr>
<tr>
  <td colspan="2" style="height: 26px"><asp:Button ID="btnSaveResolveTxt" runat="server" OnClick="btnSaveResolveTxt_Click" CssClass="btn"
            Text="Save Text" /></td>
</tr></table></ContentTemplate></asp:UpdatePanel>
<table>
<tr>
  <td colspan="2" style="height: 9px">&nbsp;</td>
</tr>
<tr>
  <td colspan="2"><asp:Label ID="lblBillingNotes" runat="server" Text="Billing Notes" style="font-size: 10pt; font-family: Verdana;"></asp:Label></td>
</tr>
<tr>
  <td colspan="2"><asp:TextBox ID="txtBillingNotes" runat="server" Height="50px" Width="638px" MaxLength="253"  TextMode="MultiLine" Wrap="true" BackColor="Transparent" CssClass="txtbox" ></asp:TextBox></td>
</tr>
<tr>
  <td colspan="2">&nbsp;</td>
</tr>
<tr>
  <td colspan="2" style="height: 18px"><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="EmailTxt"
            ErrorMessage="Provide valid email address(es)" ValidationExpression="(\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\;*\,*\ *)*" validationgroup="ResolveSubmit" EnableClientScript="False"  CssClass="errorlabel_bold" Width="449px"></asp:RegularExpressionValidator></td>
</tr>
<tr><td style="height: 31px; width: 654px;">
  <table width="640px" border="0">
    <tr>
      <td align="left" ><asp:Label ID="EmailLabel" runat="server" Text="Email" CssClass="font_verdana" ></asp:Label></td>
      <td ><asp:TextBox ID="EmailTxt" runat="server" ValidationGroup="ResolveSubmit" CausesValidation="True" CssClass="txtbox" Width="349px"></asp:TextBox><asp:ImageButton ID="SaImgButton" runat="server" ImageUrl="~/images/icn_sa.gif" OnClick="SaImgButton_Click" /></td>
      <td ><asp:Label ID="FaxLabel" runat="server" Text="Fax"  CssClass="font_verdana"></asp:Label></td>
      <td ><asp:TextBox ID="FaxTxt" runat="server" CssClass="txtbox" Width="150px"></asp:TextBox></td>
    </tr>
  </table></td>
</tr>
<tr>
  <td style="height: 32px; width: 654px;"><table width="640" border="0">
    <tr>
      <td style="width: 107px; height: 18px;"><asp:CheckBox ID="CheckBox1" runat="server" Width="194px" ValidationGroup="ResolveSubmit" CausesValidation="True" Font-Bold="True" Text="Resolve" CssClass="font_verdana" Font-Italic="False" Font-Size="X-Small" />        
      </td>
      <td width="399" style="height: 18px"><asp:CheckBox ID="ChkTaskero" runat="server" CssClass="checkbox" Text="Interested in Taskero/PSG?" Width="215px" /> </td>
      <td width="54" style="height: 18px">&nbsp;</td>
    </tr>
  </table></td>
</tr>
<tr>
  <td colspan="2" style="height: 47px"><table width="640" border="0">
    <tr>
      <td style="width: 146px; height: 20px;"><asp:Label ID="lblServiceApprovedBy" runat="server" Text="Service Approved By:" BorderStyle="None" CssClass="font_verdana"></asp:Label><font class="Verdana" size="2" color="red">*</font></td>
      <td width="476" style="height: 20px"><asp:TextBox ID="txtServiceSignOff" runat="server" Font-Names="Verdana" MaxLength="120" Width="188px" CssClass="txtbox"></asp:TextBox></td>
    </tr>
  </table></td>
</tr>
<tr>
  <td colspan="3"><table >
<tr>
<td style="width: 91px" ><asp:Button ID="SaveButton"   runat="server" OnClick="SaveButton_Click" Text="Submit" ValidationGroup="ResolveSubmit" CssClass="SubmitButton" />
</td>
<td style="width: 138px" >  <asp:Button ID="BackList" runat="server" OnClick="BackList_Click" Text="Back To Call List"
             CssClass="SubmitButton" Width="128px" />
</td>
<td >   
      <asp:Button ID="LogoutBtn" runat="server" OnClick="LogoutBtn_Click" Text="Logout"
             CssClass="SubmitButton" /></td>
</tr>
    <tr>
</table>
    </tr>
    <tr><td style="width: 77px">  <asp:Label ID="ErrorLabel" runat="server" Width="622px" CssClass="errorlabel_bold" EnableTheming="False"></asp:Label></td></tr>
  </td>
</tr>
</table>      

</ContentTemplate></asp:UpdatePanel>
</form>
</body>
</html>
