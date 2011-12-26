<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DestinationProfileList.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.DestinationProfileList" %>
<script type="text/javascript" language="javascript">
    function ConfirmDeleteDestinationProfile() {
        if (confirm('<%= DestinationProfile_DeleteDestinationProfile_Confirmation %>'))
        {
            //measurement.name
//            var serviceLink = "Services/PluginDataService.svc/DeleteDestinationProfile/" + destinationProfileId;
//            //Make ajax request...
//            jQuery.ajax({
//                type: "POST", //for complex object POST is preferred
//                contentType: "application/json; charset=utf-8",   //this is very important application/json; charset=utf-8";  else it will fail
//                url: serviceLink,  //"Services/PluginDataService.svc/SaveMeasurement",
//                processData: false, //     I found that if I use processData=true, then the resulting string sent to the service is in querystring format, not in JSON. Not what I want for passing complex objects. So I set it to false. Using true would be fine for simpler non-JSON requests where you're doing WebGet, and all the params are in the query string
//                dataType: "json",  ////this only applies to response from the server and (not request send to server)
//                dataFilter: function (data, type) {

//                    return true;
//                    //bindMeasurementData();
//                    //RemoveTr(data,measurementId);
//                },

//                //If the call succeeds
//                success:
//                    function (response) {   //the response param passed to the success callback contains the returned json
//                    },

//                //If the call fails
//                error: function (XMLHttpRequest, textStatus, errorThrown) {
//                    alert("Error");

//                    var jsonFault = JSON.parse(XMLHttpRequest.responseText);
//                    MeasurementDialog("Error Cause:" + jsonFault.Reason + " Error Detail:" + jsonFault.DetailedInformation, "Error");
//                }
//            });
        }
         
           
    }
    </script>
<div class="contant_top">&nbsp;</div>
<div class="contant_middle">
    <div class="head">
        <h1>Destination Profile</h1>
        <a href="AddDestinationProfile.aspx" class="button"><span>
            <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/new_project_icon.png" />
            New Destination Profile</span></a></div>
    <div class="clear">
    </div>   
           <asp:ListView ID="grdDestinationProfile" runat="server"  
        ItemPlaceholderID="phItemPlaceholder"  OnItemCommand="grdDestinationProfile_ItemCommand"
        >
            <LayoutTemplate>
                <table cellspacing="0" cellpadding="0" border="0" class="product_list_table">
                <thead>
                    <tr>
                        <th align="left" scope="col">Destination Profile Name</th>
                        <th align="left" scope="col" width="30%">IsUse</th>                        
                        <th align="center" scope="col" width="5%"></th>
                        <th align="center" scope="col" width="5%"></th>
                    </tr>
                </thead>
                <tbody>                                    
                    <asp:PlaceHolder ID="phItemPlaceholder" runat="server"></asp:PlaceHolder>
                </tbody>
                </table>
            </LayoutTemplate>
            <EmptyDataTemplate><div class="no_data_found">No records available</div></EmptyDataTemplate>
            <ItemTemplate>
                <tr class="purpal">
                    <td align="left"><asp:Label ID="lblDestinationProfileName" runat="server" Text='<%#Eval("ProfileName") %>'></asp:Label></td>
                    <td align="left" width="30%"><asp:Label ID="lblInUse" runat="server" Text='<%#DisplayIsActiveCustomValue(Eval("IsUse")) %>'></asp:Label></td>
                    <td align="center" width="5%"><asp:ImageButton ID="imgbtnEdit" runat="server" ToolTip="Edit Destination Profile" AlternateText="Edit" ImageUrl="../Images/edit_icon.png" CommandName="EditDestinationProfile" CommandArgument='<%# Eval("ProfileID") %>' /></td>                    
                    <td align="center" width="5%"><asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete Destination Profile" AlternateText="Delete" ImageUrl="../Images/delete_icon.png" CommandName="DeleteDestinationProfile" CommandArgument='<%# Eval("ProfileID") %>' OnClientClick="return ConfirmDeleteDestinationProfile()" /></td>                    
              </tr>
            </ItemTemplate>
            </asp:ListView>            
</div> 
<div class="contant_bottom">&nbsp;</div>
<div class="clear"></div>

