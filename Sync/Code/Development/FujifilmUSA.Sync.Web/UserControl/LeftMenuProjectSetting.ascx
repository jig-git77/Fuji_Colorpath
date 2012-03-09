<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftMenuProjectSetting.ascx.cs"
    Inherits="FujifilmUSA.Sync.Web.UserControl.LeftMenuProjectSetting" %>
<ul id="menu2">
    <li>
        <ul class="submenu">
            <li>
                <asp:LinkButton runat="server" ID="lnkDescription" Text="1. Description" OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkTargetColorSpace" Text="2. Target ColorSpace"
                    OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkProfilerOption" Text="3. Profiler Option" OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkMeasurementSetup" Text="4. Measurement Setup"
                    OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkSave" Text="5. Save" OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkMeasure" Text="6. Measure" OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkAnalyze" Text="7. Analyze" OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lnkDownload" Text="8. Download" OnClick="lnkItem_Click"></asp:LinkButton>
            </li>
        </ul>
    </li>
</ul>
