<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityValidationRules.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.ActivityValidationRules" %>
<%@ Register Src="~/UserControl/AdminStatusbar.ascx" TagName="AdminStatusbar" TagPrefix="ucAdminStatusbar" %>
<script type="text/javascript" language="javascript">
    $(function () { 
        $('#<%= lbtnSave.ClientID %>').click(function() {
            var count = 0
            $('input[id*="chkRule"][type="checkbox"]').each(function () {
                if ($(this).is(":checked") == true)
                    count++;
            });
            if (count <= 0) {
                if (confirm("Are you sure you want to delete all activity validation rules?"))
                    return true;
                else
                    return false;
            }    
    });
});
</script>
<div class="admin_top">
    &nbsp;</div>
<div class="admin_middle">
    <div class="head">
        <h1>
            <asp:Localize ID="locActivityValidationRules" runat="server" 
                Text="Activity Validation Rules" 
                meta:resourcekey="locActivityValidationRulesResource1"></asp:Localize></h1>
        <asp:LinkButton ID="lbtnSave" runat="server" class="button" 
            OnClick="lbtnSave_Click">
            <span>
                <asp:Image ID="imgSave" runat="server" 
            ImageUrl="../Images/save_icon.png" />
                
<asp:Localize runat="server" Text="Save Rules" ID="locSaveRules" 
            meta:resourcekey="locSaveRulesResource1"></asp:Localize>
            

            </span>
        </asp:LinkButton>           
    </div>
    <div class="clear"></div>
    <div style="margin-left: 10px;">
        <table cellpadding="0" cellspacing="5" width="98%" border="0">
            <tr>
                <td colspan="2" align="center">
                    <ucAdminStatusbar:AdminStatusbar ID="fujiStatusbar" runat="server" Visible="False" />
                </td>
            </tr>
            <tr>    
                <td align="left" style="width: 7%; padding-left: 3px;">
                    <span style="font-weight: bold;"><asp:Localize ID="locActivity" runat="server" 
                        Text="Activity" meta:resourcekey="locActivityResource1"></asp:Localize>&nbsp;:&nbsp;</span>
                </td>
                <td align="left">
                    <asp:DropDownList Id="ddlActivityList" runat="server" 
                        OnSelectedIndexChanged="ddlActivityList_SelectedIndexChanged" 
                        AutoPostBack="True" ></asp:DropDownList>                    
                </td>                
            </tr>
            <tr>
                <td colspan="2">
                    <telerik:RadGrid ID="rgActivityList"
                    runat="server" AutoGenerateColumns="False" EnableLinqExpressions="False"
                    OnNeedDataSource="rgActivityList_NeedDataSource"
                    Width="100%" CellSpacing="0" GridLines="None">
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
                        <telerik:GridTemplateColumn UniqueName="IsRuleOn" HeaderText="Is Rule ON?" 
                                meta:resourcekey="GridTemplateColumnResource1">
                                <ItemTemplate>                                     
                                    <asp:CheckBox ID="chkRule" runat="server" Checked='<%# Eval("IsRuleOn") %>' 
                                         />         
                                </ItemTemplate>
                                <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle  Width="10%" HorizontalAlign="Center"></ItemStyle>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Name" 
                                meta:resourcekey="GridTemplateColumnResource2">
                                <ItemTemplate>                                    
                                    <asp:Label ID="lblName" runat="server" 
                                        Text='<%# Eval("ValidationRule.Name") %>'  />
                                    <asp:HiddenField ID="hdnValidationRuleId" runat="server" Value='<%# Eval("ValidationRule.ValidationRuleId") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="30%" />
                                <ItemStyle Width="30%"/>
                            </telerik:GridTemplateColumn>   
                            <telerik:GridTemplateColumn HeaderText="Description" 
                                meta:resourcekey="GridTemplateColumnResource3">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" 
                                        Text='<%# Eval("ValidationRule.Description") %>' 
                                        />
                                </ItemTemplate>
                                <HeaderStyle Width="60%" />
                                <ItemStyle Width="60%" />
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