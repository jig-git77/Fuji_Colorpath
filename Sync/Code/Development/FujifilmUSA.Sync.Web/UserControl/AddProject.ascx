<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddProject.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddProduct" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<%@ Register Src="~/UserControl/DeviceTypeSelectionCommon.ascx" TagName="DeviceTypeSelectionCommon" TagPrefix="uc" %>

<script type="text/javascript" language="javascript">
    $(document).ready(function () {

        //Default Action
        $(".tabcontant").hide(); //Hide all content
        $("ul.tabs li:eq(" + $('#<%= hdnSelectedTabIndex.ClientID %>').val() + ")").addClass("active").show(); //Activate first tab
        $(".tabcontant:eq(" + $('#<%= hdnSelectedTabIndex.ClientID %>').val() + ")").show(); //Show first tab content

        //On Click Event
        $("ul.tabs li").click(function () {
            $("ul.tabs li").removeClass("active"); //Remove any "active" class
            $('#<%= hdnSelectedTabIndex.ClientID %>').val($(this).index());
            $(this).addClass("active"); //Add "active" class to selected tab
            $(".tabcontant").hide(); //Hide all tab content
            var activeTab = $(this).find("a").attr("href"); //Find the rel attribute value to identify the active tab + content
            $(activeTab).fadeIn(); //Fade in the active content
            return false;
        });

        

    });
</script>
<script type="text/javascript" language="javascript">
    function fnCheckUnCheck(currentRadioButton,dlpId) {
        $('input[type="radio"][id*="grdDLP_rbtnDeviceLinkProfileId"]').each(function () {

            if ($(this).attr('id') == currentRadioButton) {
                $(this).attr('checked', true);                
                document.getElementById('<%= hdnDeviceLinkProfileId.ClientID %>').value = dlpId;
            }
            else {
                $(this).attr('checked', false);
            }
        });
    }
</script>
<asp:HiddenField ID="hdnSelectedTabIndex" runat="server" Value="0" />
<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">    
    <div class="head">    
        <h1>
            Add Project</h1>        
        <asp:LinkButton ID="lbtnProjectSave" runat="server" CssClass="button" 
            onclick="lbtnProjectSave_Click">
        <span>
            <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/save_icon.png" />
            Save</span></asp:LinkButton></div>    
    <div class="clear">
    </div>
    <div class="add_form" style= "margin-top: 0px;"  >    
    <center><ucStatusbar:Statusbar ID="ucStatusbar1" runat="server" Visible="false" /></center>
        <table border="0" cellspacing="5" cellpadding="0" class="Width100percent">
            <tr>
                <td class="ProjectListtd1">
                    <asp:Label ID="lblProjectName" runat="server" Text="Project Name" ></asp:Label>
                    &nbsp; :
                </td>
                <td class="ProjectListtd2">
                   <%-- <telerik:RadTextBox ID="txtProjectName" runat="server" SkinID="width196px">
                    </telerik:RadTextBox>--%><%--<asp:RequiredFieldValidator ID="rfvProjectName" runat="server" ControlToValidate="txtProjectName" ErrorMessage="*" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="SaveProject"></asp:RequiredFieldValidator>--%>
                    <asp:TextBox ID="txtProjectName" runat="server" Width="178px" Height="19px"></asp:TextBox>
                </td>
                <td class="ProjectListtd1">
                    <asp:Label ID="lblProjectType" runat="server" Text="Project Type"></asp:Label>
                    &nbsp; :
                </td>
                <td class="ProjectListtd2">
                   <%-- <telerik:RadComboBox ID="cmbProjectType" runat="server" SkinID="width200px">                      
                    </telerik:RadComboBox>--%>
                    <asp:DropDownList ID="cmbProjectType" runat="server" Width="180px" CssClass="select">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr> 
                <td class="ProjectListtd1">
                    <asp:Label ID="lblOutputType" runat="server" Text="Output Type" ></asp:Label>
                    :
                </td>
                <td class="ProjectListtd2">
                   <%-- <telerik:RadComboBox ID="cmbOutputType" runat="server" SkinID="width200px">
                    </telerik:RadComboBox>--%>
                    <asp:DropDownList ID="cmbOutputType" runat="server" Width="180px"  CssClass="select"></asp:DropDownList>
                </td>
                <td class="float_right">
                    &nbsp;
                </td>
                <td class="ProjectListtd4">
                    &nbsp;
                </td>
            </tr>           
        </table>
    </div>
    <div style="height: 10px;">&nbsp;</div>
      <section id="tabpart">
        <section class="inner_tab">
            <ul class="tabs">
                <li><span><a href="#tab1">DLP</a></span></li>
                <li><span><a href="#tab2">Measurement Setup</a></span></li>                                
            </ul>
            <div class="clear"></div>
        </section>
        <section id="tab1" class="contantbox tabcontant" >
        <div class="tab_top">&nbsp;</div>
        <div class="tab_middle">
        <asp:UpdatePanel ID="upPanel" runat="server">
        <ContentTemplate>

       
        <div class="combopart">
        <div class="float_l" style="padding-left: 10px;" >
        <asp:Label ID="Label1" runat="server" Text="Select Source"></asp:Label> &nbsp; : &nbsp;
        <%--<telerik:RadComboBox ID="cmbSourceProfile" runat="server" DataValueField="Key" DataTextField="Value" SkinID="width200px"  AutoPostBack="true" OnSelectedIndexChanged="cmbSourceProfile_SelectedIndexChanged"></telerik:RadComboBox></div>--%>
        <asp:DropDownList ID="cmbSourceProfile" runat="server" DataValueField="Key" DataTextField="Value" Width="180px"  AutoPostBack="true" OnSelectedIndexChanged="cmbSourceProfile_SelectedIndexChanged"  CssClass="select"></asp:DropDownList></div>
        <div class="float_r" style=" padding-right : 10px;">
        <asp:Label ID="Label2" runat="server" Text="Select Destination" ></asp:Label> &nbsp; : &nbsp;
        <%--<telerik:RadComboBox ID="cmbDestinationProfile" DataValueField="Key" DataTextField="Value" runat="server" SkinID="width200px" AutoPostBack="true" OnSelectedIndexChanged="cmbDestinationProfile_SelectedIndexChanged"></telerik:RadComboBox></div>--%>
        <asp:DropDownList ID="cmbDestinationProfile" DataValueField="Key" DataTextField="Value" runat="server" Width="180px" AutoPostBack="true" OnSelectedIndexChanged="cmbDestinationProfile_SelectedIndexChanged"  CssClass="select"></asp:DropDownList></div>
        <div class="clear"></div>
        

        </div>
        <asp:ListView ID="grdDLP" runat="server" ItemPlaceholderID="phItemTemplate" OnItemDataBound="grdDLP_ItemDataBound">
            <LayoutTemplate>
                <table border="0" cellspacing="0" cellpadding="0" class="dlp_table">
                    <thead>
                        <tr>
                        <th scope="col" align="left">&nbsp;</th>
                        <th scope="col" align="center">DLP Name</th>
                        <th scope="col" align="center">Source</th>
                        <th scope="col" align="center">Destination</th>                                            
                        </tr>
                    </thead>
                    <tbody>
                        <asp:PlaceHolder ID="phItemTemplate" runat="server"></asp:PlaceHolder>
                    </tbody>
                </table>
            </LayoutTemplate>
            <EmptyDataTemplate><div class="no_data_found">No records available</div></EmptyDataTemplate>
            <ItemTemplate>
                <tr class="purpal">
  	                <td align="center" valign="middle"><asp:RadioButton ID="rbtnDeviceLinkProfileId" runat="server" GroupName="DeviceLinkProfile"/></td>
                    <td align="center"><%# Eval("ProfileName") %></td>
                    <td align="center"><%# Eval("SrcProfile.ProfileName")%></td>
                    <td align="center"><%# Eval("DestProfile.ProfileName")%></td>                                        
                </tr>
            </ItemTemplate> 
        </asp:ListView>
            <%--<telerik:RadGrid ID="grdDLP" runat="server" AutoGenerateColumns="false" OnNeedDataSource="grdDLP_NeedDataSource" OnItemDataBound="grdDLP_ItemDataBound"
                                EnableEmbeddedSkins="false" Skin="Fujifilm" SkinID="CommonGrid">
                                <MasterTableView ShowHeadersWhenNoRecords="true" EnableNoRecordsTemplate="true">
                                    <NoRecordsTemplate>
                                        <div>There are no records to display</div>
                                    </NoRecordsTemplate>
                                    <Columns>
                                        <telerik:GridTemplateColumn>
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rbtnDeviceLinkProfileId" runat="server" GroupName="DeviceLinkProfile" />                                                                   
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="DeviceLinkProfileName" HeaderText="Device Link Profile Name" UniqueName="DeviceLinkProfileName">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="SrcProfile.SourceProfileName" HeaderText="Source Profile Name" UniqueName="SourceProfileName">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DestProfile.DestinationProfileName" HeaderText="Destination Profile Name"
                                            UniqueName="DestinationProfileName">
                                        </telerik:GridBoundColumn>                                                            
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="true">
                                </ClientSettings>
                            </telerik:RadGrid>--%>
        
         </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cmbSourceProfile" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cmbDestinationProfile" EventName="SelectedIndexChanged" />
           
        </Triggers>
        </asp:UpdatePanel>
    </div>
        <div class="tab_bottom">&nbsp;</div>
        </section>
        <section id="tab2" class="contantbox tabcontant"  >
        <div class="tab_top">&nbsp;</div>
        <div class="tab_middle">
        <div class="combopart"><asp:HiddenField ID="hdnDeviceLinkProfileId" runat="server" Value="0"/>  
        <div style="margin-top : 15px; "><asp:Panel GroupingText="Measurement Settings" ID="pnlMeasurementSettings" runat="server">
                            <table border="0" cellpadding="0" cellspacing="0" style ="padding-left : 5px;">
                                    <td style =" padding-right: 5px;"><asp:Label ID="lblIlluminant" runat="server" Text="Illuminant:"></asp:Label></td>
                                    <td ><asp:DropDownList ID="cmbIlluminant" runat="server" Width="142px"  CssClass="select"></asp:DropDownList></td>
                                    <td width = "20px"></td>
                                    <td style =" padding-right: 5px;"><asp:Label ID="lblObserver" runat="server" Text="Observer:"></asp:Label></td>
                                    <td><asp:DropDownList ID="cmbObserver" runat="server" Width="142px"  CssClass="select"></asp:DropDownList></td>
                                    <td width = "20px"></td>
                                    <td style =" padding-right: 5px;"><asp:Label ID="lblDensityType" runat="server" Text="Density Type:" ></asp:Label></td>
                                    <td><asp:DropDownList ID="cmbDensityType" runat="server" Width="135px"  CssClass="select"></asp:DropDownList></td>
                            </table>
                        </asp:Panel></div>
                        <div style=" margin-top : 15px; ">
    <div id="DeviceSettings">
        <fieldset>
            <legend>Device Settings </legend>
            <table cellspacing="4" cellpadding="0" style="width:100%;" border="0" style="padding-right: 20px;">
                <tbody>
                    <tr>
                        <uc:DeviceTypeSelectionCommon ID="ucDeviceTypeSelectionCommon" runat="server" Type="Optimization" />
                    </tr>
                </tbody>
            </table>
        </fieldset>
    </div>
</div>



    <div class="clear"></div>
</div>                            	
</div>
        <div class="tab_bottom">&nbsp;</div>
        </section>
    </section>
</div>
<div class="contant_bottom">
    &nbsp;</div>
<div class="clear">
</div>
