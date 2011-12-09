<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddProject.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.AddProduct" %>
<%@ Register Src="~/UserControl/Statusbar.ascx" TagName="Statusbar" TagPrefix="ucStatusbar" %>
<script type="text/javascript" language="javascript">    
        function SelectDeviceLinkProfileId(dlpId) {            
            document.getElementById('<%= hdnDeviceLinkProfileId.ClientID %>').value = dlpId;
        }
        $(document).ready(function () {

            //Default Action
            $(".tabcontant").hide(); //Hide all content
            $("ul.tabs li:first").addClass("active").show(); //Activate first tab
            $(".tabcontant:first").show(); //Show first tab content

            //On Click Event
            $("ul.tabs li").click(function () {
                $("ul.tabs li").removeClass("active"); //Remove any "active" class
                $(this).addClass("active"); //Add "active" class to selected tab
                $(".tabcontant").hide(); //Hide all tab content
                var activeTab = $(this).find("a").attr("href"); //Find the rel attribute value to identify the active tab + content
                $(activeTab).fadeIn(); //Fade in the active content
                return false;
            });

        });
</script>
<div class="contant_top">
    &nbsp;</div>
<div class="contant_middle">    
    <div class="head">    
        <h1>
            Add Project</h1>        
        <asp:LinkButton ID="lbtnProjectSave" runat="server" CssClass="button" ValidationGroup="SaveProject"
            onclick="lbtnProjectSave_Click">
        <span>
            <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/save_icon.png" />
            Save</span></asp:LinkButton></div>
    <center><ucStatusbar:Statusbar ID="ucStatusbar1" runat="server" /></center>
    <div class="clear">
    </div>
    <div class="add_form">    
        <table border="0" cellspacing="5" cellpadding="0" class="Width100percent">
            <tr>
                <td class="ProjectListtd1">
                    <asp:Label ID="lblProjectName" runat="server" Text="Project Name" CssClass="LabelTitle"></asp:Label>
                    :
                </td>
                <td class="ProjectListtd2">
                    <telerik:RadTextBox ID="txtProjectName" runat="server" SkinID="width196px" ValidationGroup="SaveProject">
                    </telerik:RadTextBox><asp:RequiredFieldValidator ID="rfvProjectName" runat="server" ControlToValidate="txtProjectName" ErrorMessage="*" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="SaveProject"></asp:RequiredFieldValidator>
                </td>
                <td class="float_right">
                    <asp:Label ID="lblProjectType" runat="server" Text="Project Type" CssClass="LabelTitle"></asp:Label>
                    :
                </td>
                <td class="ProjectListtd4">
                    <telerik:RadComboBox ID="cmbProjectType" runat="server" SkinID="width200px">                      
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="ProjectListtd1">
                    <asp:Label ID="lblOutputType" runat="server" Text="Output Type" CssClass="LabelTitle"></asp:Label>
                    :
                </td>
                <td class="ProjectListtd2">
                    <telerik:RadComboBox ID="cmbOutputType" runat="server" SkinID="width200px">
                    </telerik:RadComboBox>
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
        <section id="tab1" class="contantbox tabcontant">
        <div class="tab_top">&nbsp;</div>
        <div class="tab_middle">
        <div class="combopart">
        <div class="float_l"><asp:Label ID="Label1" runat="server" Text="Select Source:" CssClass="LabelTitle"></asp:Label><telerik:RadComboBox ID="cmbSourceProfile" runat="server" DataValueField="Key" DataTextField="Value" SkinID="width200px"  AutoPostBack="true" OnSelectedIndexChanged="cmbSourceProfile_SelectedIndexChanged"></telerik:RadComboBox></div>
        <div class="float_r"><asp:Label ID="Label2" runat="server" Text="Select Source:" CssClass="LabelTitle"></asp:Label><telerik:RadComboBox ID="cmbDestinationProfile" DataValueField="Key" DataTextField="Value" runat="server" SkinID="width200px" AutoPostBack="true" OnSelectedIndexChanged="cmbDestinationProfile_SelectedIndexChanged"></telerik:RadComboBox></div>
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
  	                <td align="center" valign="middle"><asp:RadioButton ID="rbtnDeviceLinkProfileId" runat="server" GroupName="DeviceLinkProfile" /></td>
                    <td align="center"><%# Eval("DeviceLinkProfileName") %></td>
                    <td align="center"><%# Eval("SrcProfile.SourceProfileName") %></td>
                    <td align="center"><%# Eval("DestProfile.DestinationProfileName") %></td>                                        
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
</div>
        <div class="tab_bottom">&nbsp;</div>
        </section>
        <section id="tab2" class="contantbox tabcontant">
        <div class="tab_top">&nbsp;</div>
        <div class="tab_middle">
        <div class="combopart"><asp:HiddenField ID="hdnDeviceLinkProfileId" runat="server" Value="0"/>  
        <div class="float_l" style="padding-left: 20px;"><asp:Panel GroupingText="Measurement Settings" ID="pnlMeasurementSettings" runat="server">
                            <table border="0" cellpadding="0" cellspacing="4" style="padding-right: 20px;">
                                <tr>
                                    <td><asp:Label ID="lblIlluminant" runat="server" Text="Illuminant:"></asp:Label></td>
                                    <td><telerik:RadComboBox ID="cmbIlluminant" runat="server"></telerik:RadComboBox></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="lblObserver" runat="server" Text="Observer:"></asp:Label></td>
                                    <td><telerik:RadComboBox ID="cmbObserver" runat="server"></telerik:RadComboBox></td>
                                </tr>
                                <tr style="padding: 0px 0px 5px 0px;">
                                    <td><asp:Label ID="lblDensityType" runat="server" Text="Density Type:"></asp:Label></td>
                                    <td><telerik:RadComboBox ID="cmbDensityType" runat="server"></telerik:RadComboBox></td>
                                </tr>
                            </table>
                        </asp:Panel></div>
    <div class="float_r" style="padding-right: 20px;"><table border="0" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td><asp:Label ID="lblDeviceType" runat="server" Text="Device Type:"></asp:Label></td>
                                    <td><telerik:RadComboBox ID="cmbDeviceType" runat="server" DataValueField="Key" DataTextField="Value" OnSelectedIndexChanged="cmbDeviceType_SelectedIndexChanged" AutoPostBack="true" ></telerik:RadComboBox></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="lblAlignmentChart" runat="server" Text="Alignment Chart:"></asp:Label></td>
                                    <td><telerik:RadComboBox ID="cmbAlignmentChart" runat="server" DataValueField="Key" DataTextField="Value"></telerik:RadComboBox></td>
                                </tr>                                                  
                        </table>
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
