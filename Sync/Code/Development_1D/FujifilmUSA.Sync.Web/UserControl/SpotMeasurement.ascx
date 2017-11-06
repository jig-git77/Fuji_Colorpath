<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SpotMeasurement.ascx.cs" Inherits="FujifilmUSA.Sync.Web.UserControl.SpotMeasurement" %>
<link href="Content/kendo/2017.2.621/kendo.common.min.css" rel="stylesheet" />
<link href="Content/kendo/2017.2.621/kendo.default.min.css" rel="stylesheet" />
<script src="Scripts/kendo/2017.2.621/kendo.all.min.js"></script>
<script>
    $(document).ready(function () {
        $loader = $(".contant_middle");
        kendo.ui.progress($loader, true);
        setTimeout(function () { kendo.ui.progress($loader, false); document.getElementById("<%=lblMsg.ClientID%>").style.display = "block"; }, 3000);
    });
</script>
<div class="contant_top">
</div>
<div class="contant_middle">
    <div class="head">
        <h1 id="header" runat="server"><asp:Localize runat="server" Text="Calculating" 
                ID="locMeasurement" meta:resourcekey="locHeaderResource1"></asp:Localize></h1>
        <div class="Information_divAnalyze"> 
                        
            <asp:LinkButton ID="btnAnalyze" runat="server" CssClass="button" OnClick="btnAnalyze_Click"
                OnClientClick="return AllowToAnalyze();">
                <span>
                    <asp:Image ID="imgAdd" runat="server" ImageUrl="../Images/analyze.png" 
                    meta:resourcekey="imgAddResource1" />
                    
                    <asp:Localize runat="server" Text="Analyze" ID="locAnalyze" 
                                meta:resourcekey="locAnalyzeResource1"></asp:Localize>
                </span>
            </asp:LinkButton>    
            <%-- <asp:LinkButton ID="btnValidationSetup" runat="server" CssClass="buttonLeft" OnClick="btnValidationSetup_Click" Visible="false">
                <span>
                    <asp:Image ID="Image2" runat="server" 
                        ImageUrl="~/Images/new_project_icon.png" meta:resourcekey="Image2Resource1" />
                    <asp:Localize runat="server" Text="Validation Setup" id="locValidationSetup"  meta:resourcekey="locValidationSetupResource1"></asp:Localize>
                </span>
            </asp:LinkButton>    
             <asp:LinkButton ID="btnMeasurementSetup" runat="server" CssClass="buttonLeft" 
                Visible="false" onclick="btnMeasurementSetup_Click">
                <span>
                    <asp:Image ID="Image1" runat="server" 
                        ImageUrl="~/Images/new_project_icon.png" meta:resourcekey="Image2Resource1" />
                    <asp:Localize runat="server" Text="Measurement Setup" id="locMeasurementSetup"  meta:resourcekey="locMeasurementSetupResource1"></asp:Localize>
                </span>
            </asp:LinkButton>    
             <asp:LinkButton ID="btnCustomSourceProfile" runat="server" CssClass="button" 
                Visible="false" onclick="btnCustomSourceProfile_Click"  OnClientClick="return AllowToAnalyze();">
                <span>
                    <asp:Image ID="Image3" runat="server" 
                        ImageUrl="~/Images/analyze.png" meta:resourcekey="Image2Resource1" />
                    <asp:Localize runat="server" Text="Next" id="locCustomSourceProf"  meta:resourcekey="locCustomSourceProfResource1"></asp:Localize>
                </span>
            </asp:LinkButton>--%>                
        </div>
    </div>
    <%--<div class="mesurementStatusbar info_form">
        <div class="top">
            &nbsp;</div>
        <div class="middle" runat="server" id="tblMessage">
            <asp:Label ID="lblMessage" runat="server" Text="Status : Scanning device is not loaded."
                Style="font-weight: bold; padding-left: 5px;" 
                meta:resourcekey="lblMessageResource1"></asp:Label>
            <asp:Label ID="lblPluginMessage" runat="server" Text="loading..." Style="float: right;
                font-weight: bold; padding-right: 5px;" 
                meta:resourcekey="lblPluginMessageResource1"></asp:Label>
            &nbsp;
            <br />
        </div>
        <div class="bottom">
            &nbsp;</div>
    </div>--%>    
    <div class="mesurementStatusbar info_form">
        <br /><br /><b><asp:Label ID="lblMsg" runat="server" Text="Calculations complete. Click Analyze to continue." meta:resourcekey="locMessageResource1" style="display: none;" ></asp:Label></b>
    </div>
</div>
<div class="contant_bottom">
    &nbsp;</div>
<div class="clear">
</div>

