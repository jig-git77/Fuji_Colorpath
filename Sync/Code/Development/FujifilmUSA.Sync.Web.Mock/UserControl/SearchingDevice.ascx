<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchingDevice.ascx.cs" Inherits="FujifilmUSA.Sync.Web.Mock.UserControl.SearchingDevice" %>

<script type="text/javascript"> 
//<![CDATA[
    var theForm = document.forms['form1'];
    if (!theForm) {
        theForm = document.form1;
    }

    

    function __doPostBack(eventTarget, eventArgument) {
        if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
            theForm.__EVENTTARGET.value = eventTarget;
            theForm.__EVENTARGUMENT.value = eventArgument;
            theForm.submit();
        }
    }
//]]>
    // __doPostBack(<%=lbtnSearchDevice.UniqueID %>,\'\')
</script>
<input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" value="" />
<input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="" />
<div class="SearchList">
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 100px;"> 
                <label style="font-size:18px;">Search</label>
            </td>
            <td style="width: 100px;">
                
                <asp:TextBox ID="txtDeviceName" MaxLength="50"  Font-Size="18px" 
                    runat="server" Width="300px"></asp:TextBox>
            </td>
            <td style="width: 100px;">
                <asp:LinkButton ID="lbtnSearchDevice" runat="server" ToolTip="Search Device" 
                    onclick="lbtnSearchDevice_Click">
                    <span> <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/Search.png" /></span>
                </asp:LinkButton>
            </td>
            <td style="width: 100%;">
            </td>
        </tr>
    </table>
</div>
