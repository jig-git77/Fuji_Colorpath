<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="AssignSupportUser.aspx.cs" Inherits="AssignSupportUser" %>
<%@ Register src="Common/ProgressBar.ascx" tagname="ProgressBar" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FUJIFILM Graphic Systems U.S.A., Inc.</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <meta name="keywords" content="FUJIFILM Graphic Systems U.S.A., Inc." />
    <meta name="description" content="FUJIFILM Graphic Systems U.S.A., Inc." />
    <link href="CSS/Default.css" rel="stylesheet" type="text/css" />    
</head>
<body>
    <form id="form1" runat="server">    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <asp:UpdatePanel ID="UP_SupportUser" runat="server">
            <ContentTemplate>
                <div>
                    <table cellpadding="0" cellspacing="5" border="0" align="left" width="95%">
                        <tr>
                            <td>
                                <asp:Label ID="LblError" runat="server" CssClass="ERROR"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                            <fieldset>
                                <legend>
                                    <asp:Literal ID="LTRL_Assign_Support_User" runat="server"></asp:Literal>
                                </legend>
                                <table border="0" cellspacing="1" cellpadding="2" align="left">
                                    <tr>
                                        <td align="right">
                                            <asp:Literal ID="LTRL_ID" runat="server" Visible="false">0</asp:Literal>
                                        </td>
                                        <td align="right" class="required-text-small">
                                            Fields marked with (<span class="required-text-small-red">*</span>) are Required
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Literal ID="LTRL_UserList" runat="server"></asp:Literal>
                                            <span class="required-text-small-red">*</span>&nbsp;:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDL_UserList" runat="server" CssClass="DROPDOWNLIST" AutoPostBack="True" OnSelectedIndexChanged="DDL_UserList_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Literal ID="LTRL_Company" runat="server"></asp:Literal>
                                            <span class="required-text-small-red">*</span>&nbsp;:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDL_Company" runat="server" CssClass="DROPDOWNLIST" AutoPostBack="True" OnSelectedIndexChanged="DDL_Company_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>                                      
                                    <tr><td colspan="2"></td></tr>                                  
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:GridView ID="GV_Branch" runat="server" AutoGenerateColumns="False" 
                                                Width="350px" onrowcreated="GV_Branch_RowCreated">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CHK_Data" runat="server" AID='<%#Eval("BranchID")%>' AutoPostBack="true" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="BranchID" HeaderText="Branch ID" Visible="false" />
                                                    <asp:BoundField DataField="BranchName" HeaderText="Branch Name" Visible="true" />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr><td colspan="2"></td></tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:GridView ID="GV_Dongle" runat="server" AutoGenerateColumns="false" Width="350px">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CHK_Data" runat="server" AID='<%#Eval("CPDongleID")%>' />                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CPDongleID" HeaderText="Dongle ID" Visible="false" />
                                                    <asp:BoundField DataField="CPSerialNUM" HeaderText="Dongle Name" />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Button CssClass="BUTTON" ID="BTN_UpdateSupportUser" runat="server" Text="Update"
                                                OnClick="BTN_UpdateSupportUser_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        </tr>
                    </table>
                </div>
                <uc1:ProgressBar ID="ProgressBar1" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>    
    </form>
</body>
</html>
