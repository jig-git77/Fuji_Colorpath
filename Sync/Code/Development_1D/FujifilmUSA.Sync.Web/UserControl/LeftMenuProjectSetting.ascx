<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="LeftMenuProjectSetting.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.LeftMenuProjectSetting" %>
<ul id="menu2">
    <li>
        <ul class="submenu">
            <li>
                <asp:LinkButton runat="server" ID="lnkDescription"  
                    style="padding: 7px 0 6px 30px;" Text="1. Description" OnClick="lnkItem_Click" 
                    meta:resourcekey="lnkDescriptionResource1"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkTargetColorSpace" 
                    Text="2. Target ColorSpace" style="padding: 7px 0 6px 30px;"
                    OnClick="lnkItem_Click" meta:resourcekey="lnkTargetColorSpaceResource1"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkProfilerOption" 
                    Text="3. Profiler Options" style="padding: 7px 0 6px 30px;"  
                    OnClick="lnkItem_Click" meta:resourcekey="lnkProfilerOptionResource1"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkMeasurementSetup" 
                    Text="4. Measurement Setup" style="padding: 7px 0 6px 30px;" 
                    OnClick="lnkItem_Click" meta:resourcekey="lnkMeasurementSetupResource1"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkValidationSetup" 
                    Text="5. Validation Setup" style="padding: 7px 0 6px 30px;" 
                    OnClick="lnkItem_Click" meta:resourcekey="lnkValidationSetupResource1"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkSave" Text="6. Save" 
                    style="padding: 7px 0 6px 30px;" OnClick="lnkItem_Click" 
                    meta:resourcekey="lnkSaveResource1"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkMeasure" Text="7. Measure" 
                    style="padding: 7px 0 6px 30px;" OnClick="lnkItem_Click" 
                    meta:resourcekey="lnkMeasureResource1"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkAnalyze" Text="8. Analyze" 
                    style="padding: 7px 0 6px 30px;" OnClick="lnkItem_Click" 
                    meta:resourcekey="lnkAnalyzeResource1"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkDownload" Text="9. Download" 
                    style="padding: 7px 0 6px 30px;" OnClick="lnkItem_Click" 
                    meta:resourcekey="lnkDownloadResource1"></asp:LinkButton>
            </li>
        </ul>
    </li>
</ul>
