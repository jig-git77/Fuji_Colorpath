<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ApplicationConfiguration.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.ApplicationConfiguration" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<script type="text/javascript" language="javascript">
    $(function () {
        $('#<%= lbtnSave.ClientID %>').click(function () {
            if (confirm('<%= GetResourceString("confirmMessageSaveValues") %>'))
                return true;
            else
                return false;            
        });
    });
</script>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle">
    <div class="head">
        <h1>
            <asp:Localize ID="locApplicationConfiguration" runat="server" 
                Text="Application Configuration" 
                meta:resourcekey="locApplicationConfigurationResource1"></asp:Localize></h1>
        <asp:LinkButton ID="lbtnSave" runat="server" class="button" 
            OnClick="lbtnSave_Click">
            <span>
                <asp:Image ID="imgSave" runat="server" 
            ImageUrl="../Images/save_icon.png" />                
                
<asp:Localize runat="server" Text="Save" ID="locSave" 
            meta:resourcekey="locSaveResource1"></asp:Localize>            
            
            
            </span>
        </asp:LinkButton>           
    </div>
    <div class="clear"></div>
    <div style="margin-left: 10px;">
        <table cellpadding="0" cellspacing="5" width="98%" border="0">                        
             <tr>
                <td align="center">
                    <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" Visible="False" />
                </td>
            </tr>
            <tr>    
                <td align="left" style="padding-bottom: 10px;">
                    <span style="font-weight: bold;">
                    <asp:Localize ID="locNote" runat="server" 
                        Text="Note:" meta:resourcekey="locNoteResource1"></asp:Localize>&nbsp;</span><asp:Localize 
                        ID="locNoteText" runat="server" 
                        Text="Please modify application configuration values during off working hours, as it will affect activity of online users." 
                        meta:resourcekey="locNoteTextResource1"></asp:Localize>
                </td>                
            </tr>
            <tr>
                <td>
                    <telerik:RadGrid ID="rgApplicationConfiguration"
                    runat="server" AutoGenerateColumns="False" EnableLinqExpressions="False"
                    OnNeedDataSource="rgApplicationConfiguration_NeedDataSource"
                    Width="100%" CellSpacing="0" GridLines="None" 
                        meta:resourcekey="rgApplicationConfigurationResource1">
                    <GroupingSettings CaseSensitive="False" />
                    <HeaderStyle Font-Bold="True" />                    
                    <MasterTableView>                        
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

                        <RowIndicatorColumn
                            Visible="True">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn
                            Visible="True">
                        </ExpandCollapseColumn>
                        <Columns>
                        <telerik:GridTemplateColumn HeaderText="Configuration Key" 
                                meta:resourcekey="GridTemplateColumnResource1">
                                <ItemTemplate>                         
                                    <asp:Label ID="lblKey" runat="server" Text='<%# Eval("ConfigurationKey") %>' 
                                        meta:resourcekey="lblKeyResource1"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="40%" HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle  Width="40%" HorizontalAlign="Left"></ItemStyle>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Configuration Value" 
                                meta:resourcekey="GridTemplateColumnResource2">
                                <ItemTemplate>                                                                    
                                    <asp:TextBox ID="txtValue" runat="server" 
                                        Text='<%# Eval("ConfigurationValue") %>' Width="90%" 
                                        meta:resourcekey="txtValueResource1"></asp:TextBox>                                    
                                    <asp:HiddenField ID="hdnConfigurationId" runat="server" Value='<%# Eval("ApplicationConfigurationId") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="60%" />
                                <ItemStyle Width="60%"/>
                            </telerik:GridTemplateColumn>                                                          
                        </Columns>                        

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>
                    </MasterTableView>                    

<FilterMenu EnableImageSprites="False"></FilterMenu>
                </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
</div>
<div class="admin_bottom">&nbsp;</div>
<div class="clear"></div>