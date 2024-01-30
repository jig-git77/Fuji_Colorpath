using Oris.Common;
using OrisWeb.Models;
using System;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;


namespace OrisWeb
{
    public partial class UploadBrandFiles : PageBase
    {
        #region Declarations
        string _strCSSFile, _strHeaderFile, _strLargeFile;
        #endregion

        #region Page Load and Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //========== Clear Error =========//
                LblError.Text = "";

                //=========== Check Login ==========//
                Sessions.CheckUserLoginModelDialog();

                //=========== Bind Display input box with the value ==========//
                AssignJavascriptToShowColors(TXT_TopLine, TXT_Color_TopLine);
                AssignJavascriptToShowColors(TXT_MainTabMenuText, TXT_Color_MainTabMenuText);
                AssignJavascriptToShowColors(TXT_MainTabMenuTextOver, TXT_Color_MainTabMenuTextOver);
                AssignJavascriptToShowColors(TXT_MainTabMenuBack, TXT_Color_MainTabMenuBack);
                AssignJavascriptToShowColors(TXT_MainTabMenuBackOver, TXT_Color_MainTabMenuBackOver);
                AssignJavascriptToShowColors(TXT_MainTabMenuBorder, TXT_Color_MainTabMenuBorder);
                AssignJavascriptToShowColors(TXT_MainTabMenuBorderOver, TXT_Color_MainTabMenuBorderOver);
                AssignJavascriptToShowColors(TXT_MenuBottomLine, TXT_Color_MenuBottomLine);
                AssignJavascriptToShowColors(TXT_SubTabMenuText, TXT_Color_SubTabMenuText);
                AssignJavascriptToShowColors(TXT_SubTabMenuTextOver, TXT_Color_SubTabMenuTextOver);
                AssignJavascriptToShowColors(TXT_MainForm, TXT_Color_MainForm);
                AssignJavascriptToShowColors(TXT_Footer, TXT_Color_Footer);
                AssignJavascriptToShowColors(TXT_FooterText, TXT_Color_FooterText);

                //========== Default Settings =========//
                if (IsPostBack == false)
                {
                    //========== Default Settings =========//                
                    FillForm();
                }
            }
            catch (Exception Exc)
            {
                LblError.Text = Exc.Message;
            }
        }
        protected void BTN_Save_Click(object sender, EventArgs e)
        {
            try
            {
                //========== Set Sessions ==========//
                if (chkHeaderLogo.Checked == true)
                { Sessions.SetSessions("Header-Logo", ""); }
                else if (FUPLD_HeaderLogo.PostedFile.FileName != "")
                {
                    UploadFile(FUPLD_HeaderLogo, "Branding\\HeaderImage\\", "HeaderFile");
                    Sessions.SetSessions("Header-Logo", _strHeaderFile);
                }
                else
                { Sessions.SetSessions("Header-Logo", HFHeaderLogo.Value); }

                if (chkLargeImage.Checked == true)
                { Sessions.SetSessions("Large-Image", ""); }
                else if (FUPLD_LargeImage.PostedFile.FileName != "")
                {
                    UploadFile(FUPLD_LargeImage, "Branding\\LargeImage\\", "LargeFile");
                    Sessions.SetSessions("Large-Image", _strLargeFile);
                }
                else
                { Sessions.SetSessions("Large-Image", HFLargeImage.Value); }

                //========== Css File Sessions ==========//
                CreateCssFileAndAssignColors();
                if (CHK_ConfirmCssChange.Checked == true)
                {
                    Sessions.SetSessions("CSS-File", _strCSSFile);
                    Sessions.SetSessions("CSS-TopLine", TXT_TopLine.Text);
                    Sessions.SetSessions("CSS-MainTabMenuText", TXT_MainTabMenuText.Text);
                    Sessions.SetSessions("CSS-MainTabMenuTextOver", TXT_MainTabMenuTextOver.Text);
                    Sessions.SetSessions("CSS-MainTabMenuBack", TXT_MainTabMenuBack.Text);
                    Sessions.SetSessions("CSS-MainTabMenuBackOver", TXT_MainTabMenuBackOver.Text);
                    Sessions.SetSessions("CSS-MainTabMenuBorder", TXT_MainTabMenuBorder.Text);
                    Sessions.SetSessions("CSS-MainTabMenuBorderOver", TXT_MainTabMenuBorderOver.Text);
                    Sessions.SetSessions("CSS-MenuBottomLine", TXT_MenuBottomLine.Text);
                    Sessions.SetSessions("CSS-SubTabMenuText", TXT_SubTabMenuText.Text);
                    Sessions.SetSessions("CSS-SubTabMenuTextOver", TXT_SubTabMenuTextOver.Text);
                    Sessions.SetSessions("CSS-MainForm", TXT_MainForm.Text);
                    Sessions.SetSessions("CSS-Footer", TXT_Footer.Text);
                    Sessions.SetSessions("CSS-FooterText", TXT_FooterText.Text);
                }

                //========== Fills Form Again ==========//
                FillForm();
            }
            catch (Exception Exc)
            {
                LblError.Text = Exc.Message;
            }
        }
        #endregion

        #region Methods and Functions
        private void AssignJavascriptToShowColors(TextBox TXT, TextBox TXT_Colors) { TXT.Attributes.Add("OnBlur", "FillColor(this,'" + TXT_Colors.ClientID + "')"); }
        public Boolean UploadFile(FileUpload FILEUPLDER, string strFolderPath, string Filename)
        {

            if (FILEUPLDER.HasFile)
            {
                string TimeStamp = System.DateTime.Now.ToString();
                string FileExt = FILEUPLDER.PostedFile.FileName;
                FileExt = FileExt.Substring(FileExt.IndexOf("."));
                TimeStamp = TimeStamp.Replace("/", ""); TimeStamp = TimeStamp.Replace(":", ""); TimeStamp = TimeStamp.Replace(" ", "");
                TimeStamp = TimeStamp + FileExt;
                strFolderPath = strFolderPath + TimeStamp;
                string filepath = FILEUPLDER.PostedFile.FileName;
                string pat = @"\\(?:.+)\\(.+)\.(.+)";
                Regex r = new Regex(pat);
                Match m = r.Match(filepath);
                if (Filename == "HeaderFile")
                    _strHeaderFile = TimeStamp;
                else if (Filename == "LargeFile")
                    _strLargeFile = TimeStamp;
                FILEUPLDER.PostedFile.SaveAs(Server.MapPath(".\\") + strFolderPath);
                return true;
            }
            else
            {
                return false;
            }
        }
        private void FillForm()
        {
            HFHeaderLogo.Value = Sessions.GetSession("Header-Logo").ToString();
            LnkHeaderLogo.NavigateUrl = "Branding/HeaderImage/" + HFHeaderLogo.Value;
            if (HFHeaderLogo.Value != "")
            { LnkHeaderLogo.Visible = chkHeaderLogo.Visible = true; }
            else
            { LnkHeaderLogo.Visible = chkHeaderLogo.Visible = false; }

            HFLargeImage.Value = Sessions.GetSession("Large-Image").ToString();
            LnkLargeImage.NavigateUrl = "Branding/LargeImage/" + HFLargeImage.Value;
            if (HFLargeImage.Value != "")
            { LnkLargeImage.Visible = chkLargeImage.Visible = true; }
            else
            { LnkLargeImage.Visible = chkLargeImage.Visible = false; }

            chkHeaderLogo.Checked = false;
            chkLargeImage.Checked = false;

            LTRL_Css.Text = Sessions.GetSession("CSS-File").ToString();
            LTRL_TopLine.Text = TXT_TopLine.Text = Sessions.GetSession("CSS-TopLine").ToString();
            LTRL_MainTabMenuText.Text = TXT_MainTabMenuText.Text = Sessions.GetSession("CSS-MainTabMenuText").ToString();
            LTRL_MainTabMenuTextOver.Text = TXT_MainTabMenuTextOver.Text = Sessions.GetSession("CSS-MainTabMenuTextOver").ToString();
            LTRL_MainTabMenuBack.Text = TXT_MainTabMenuBack.Text = Sessions.GetSession("CSS-MainTabMenuBack").ToString();
            LTRL_MainTabMenuBackOver.Text = TXT_MainTabMenuBackOver.Text = Sessions.GetSession("CSS-MainTabMenuBackOver").ToString();
            LTRL_MainTabMenuBorder.Text = TXT_MainTabMenuBorder.Text = Sessions.GetSession("CSS-MainTabMenuBorder").ToString();
            LTRL_MainTabMenuBorderOver.Text = TXT_MainTabMenuBorderOver.Text = Sessions.GetSession("CSS-MainTabMenuBorderOver").ToString();
            LTRL_MenuBottomLine.Text = TXT_MenuBottomLine.Text = Sessions.GetSession("CSS-MenuBottomLine").ToString();
            LTRL_SubTabMenuText.Text = TXT_SubTabMenuText.Text = Sessions.GetSession("CSS-SubTabMenuText").ToString();
            LTRL_SubTabMenuTextOver.Text = TXT_SubTabMenuTextOver.Text = Sessions.GetSession("CSS-SubTabMenuTextOver").ToString();
            LTRL_MainForm.Text = TXT_MainForm.Text = Sessions.GetSession("CSS-MainForm").ToString();
            LTRL_Footer.Text = TXT_Footer.Text = Sessions.GetSession("CSS-Footer").ToString();
            LTRL_FooterText.Text = TXT_FooterText.Text = Sessions.GetSession("CSS-FooterText").ToString();

            TXT_Color_TopLine.Style.Add("background-color", TXT_TopLine.Text);
            TXT_Color_MainTabMenuText.Style.Add("background-color", TXT_MainTabMenuText.Text);
            TXT_Color_MainTabMenuTextOver.Style.Add("background-color", TXT_MainTabMenuTextOver.Text);
            TXT_Color_MainTabMenuBack.Style.Add("background-color", TXT_MainTabMenuBack.Text);
            TXT_Color_MainTabMenuBackOver.Style.Add("background-color", TXT_MainTabMenuBackOver.Text);
            TXT_Color_MainTabMenuBorder.Style.Add("background-color", TXT_MainTabMenuBorder.Text);
            TXT_Color_MainTabMenuBorderOver.Style.Add("background-color", TXT_MainTabMenuBorderOver.Text);
            TXT_Color_MenuBottomLine.Style.Add("background-color", TXT_MenuBottomLine.Text);
            TXT_Color_SubTabMenuText.Style.Add("background-color", TXT_SubTabMenuText.Text);
            TXT_Color_SubTabMenuTextOver.Style.Add("background-color", TXT_SubTabMenuTextOver.Text);
            TXT_Color_MainForm.Style.Add("background-color", TXT_MainForm.Text);
            TXT_Color_Footer.Style.Add("background-color", TXT_Footer.Text);
            TXT_Color_FooterText.Style.Add("background-color", TXT_FooterText.Text);
        }
        private void CreateCssFileAndAssignColors()
        {
            if (CHK_ConfirmCssChange.Checked == true)
            {
                //========= Assign To LTRL ==========//
                LTRL_TopLine.Text = TXT_TopLine.Text;
                LTRL_MainTabMenuText.Text = TXT_MainTabMenuText.Text;
                LTRL_MainTabMenuTextOver.Text = TXT_MainTabMenuTextOver.Text;
                LTRL_MainTabMenuBack.Text = TXT_MainTabMenuBack.Text;
                LTRL_MainTabMenuBackOver.Text = TXT_MainTabMenuBackOver.Text;
                LTRL_MainTabMenuBorder.Text = TXT_MainTabMenuBorder.Text;
                LTRL_MainTabMenuBorderOver.Text = TXT_MainTabMenuBorderOver.Text;
                LTRL_MenuBottomLine.Text = TXT_MenuBottomLine.Text;
                LTRL_SubTabMenuText.Text = TXT_SubTabMenuText.Text;
                LTRL_SubTabMenuTextOver.Text = TXT_SubTabMenuTextOver.Text;
                LTRL_MainForm.Text = TXT_MainForm.Text;
                LTRL_Footer.Text = TXT_Footer.Text;
                LTRL_FooterText.Text = TXT_FooterText.Text;

                //========= Read the Template Css ==========//
                System.IO.StreamReader objSR = new System.IO.StreamReader(Server.MapPath("Css/Template.css"));
                string TemplateCss = objSR.ReadToEnd();
                objSR.Close();

                //========= Replace The Values ==========//
                TemplateCss = TemplateCss.Replace("#~TopLine~#", LTRL_TopLine.Text);
                TemplateCss = TemplateCss.Replace("#~MainTabMenuText~#", LTRL_MainTabMenuText.Text);
                TemplateCss = TemplateCss.Replace("#~MainTabMenuTextOver~#", LTRL_MainTabMenuTextOver.Text);
                TemplateCss = TemplateCss.Replace("#~MainTabMenuBack~#", LTRL_MainTabMenuBack.Text);
                TemplateCss = TemplateCss.Replace("#~MainTabMenuBackOver~#", LTRL_MainTabMenuBackOver.Text);
                TemplateCss = TemplateCss.Replace("#~MainTabMenuBorder~#", LTRL_MainTabMenuBorder.Text);
                TemplateCss = TemplateCss.Replace("#~MainTabMenuBorderOver~#", LTRL_MainTabMenuBorderOver.Text);
                TemplateCss = TemplateCss.Replace("#~MenuBottomLine~#", LTRL_MenuBottomLine.Text);
                TemplateCss = TemplateCss.Replace("#~SubTabMenuText~#", LTRL_SubTabMenuText.Text);
                TemplateCss = TemplateCss.Replace("#~SubTabMenuTextOver~#", LTRL_SubTabMenuTextOver.Text);
                TemplateCss = TemplateCss.Replace("#~MainForm~#", LTRL_MainForm.Text);
                TemplateCss = TemplateCss.Replace("#~Footer~#", LTRL_Footer.Text);
                TemplateCss = TemplateCss.Replace("#~FooterText~#", LTRL_FooterText.Text);

                //========= Create New Css Template ==========//
                _strCSSFile = "PB-CSS-" + System.DateTime.Now.Second.ToString() + "-" + System.DateTime.Now.Millisecond.ToString() + ".css";

                //========= Write In a file ==========//
                System.IO.StreamWriter objSW = new System.IO.StreamWriter(Server.MapPath("Css/" + _strCSSFile));
                objSW.Write(TemplateCss);
                objSW.Close();

            }
        }
        #endregion

    }


}