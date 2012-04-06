<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftMenuProjectSetting.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.LeftMenuProjectSetting" %>
<ul id="menu2">
    <li>
        <ul class="submenu">
            <li>
                <asp:LinkButton runat="server" ID="lnkDescription"  style="padding: 7px 0 6px 30px;" Text="1. Description" OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkTargetColorSpace" Text="2. Target ColorSpace" style="padding: 7px 0 6px 30px;"
                    OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkProfilerOption" Text="3. Profiler Option" style="padding: 7px 0 6px 30px;"  OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkMeasurementSetup" Text="4. Measurement Setup" style="padding: 7px 0 6px 30px;" 
                    OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkSave" Text="5. Save" style="padding: 7px 0 6px 30px;" OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkMeasure" Text="6. Measure" style="padding: 7px 0 6px 30px;" OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkAnalyze" Text="7. Analyze" style="padding: 7px 0 6px 30px;" OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkDownload" Text="8. Download" style="padding: 7px 0 6px 30px;" OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
        </ul>
    </li>
</ul>
