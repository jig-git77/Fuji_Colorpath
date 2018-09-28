#region SYSTEM
using System;
using System.Data;
using System.Data.SqlClient;
#endregion

#region Others
using Fuji.DataAccess;
#endregion

namespace Fuji.Business
{
    public abstract class CompanyBranding
    {
        #region Variables
        private string _TableName = "CompanyBranding";
        private int _BrandID = 0;
        private string _BrandName = "";
        private string _Css = "";
        private string _HeaderLogo = "";
        private string _LargeImage = "";

        private string _TopLine = "";
        private string _MainTabMenuText = "";
        private string _MainTabMenuTextOver = "";
        private string _MainTabMenuBack = "";
        private string _MainTabMenuBackOver = "";
        private string _MainTabMenuBorder = "";
        private string _MainTabMenuBorderOver = "";
        private string _MenuBottomLine = "";
        private string _SubTabMenuText = "";
        private string _SubTabMenuTextOver = "";
        private string _MainForm = "";
        private string _Footer = "";
        private string _FooterText = "";
        private int _CompanyID = 0;

        private string _PopUpTabMainColor = "";
        private string _PopUpTabText = "";
        private string _PopUpTabSelectedText = "";
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public int BrandID
        {
            get { return _BrandID; }
            set { _BrandID = value; }
        }
        public string BrandName
        {
            get { return _BrandName; }
            set { _BrandName = value; }
        }
        public string Css
        {
            get { return _Css; }
            set { _Css = value; }
        }
        public string HeaderLogo
        {
            get { return _HeaderLogo; }
            set { _HeaderLogo = value; }
        }
        public string LargeImage
        {
            get { return _LargeImage; }
            set { _LargeImage = value; }
        }

        public string TopLine
        {
            get { return _TopLine; }
            set { _TopLine = value ; }
        }
        public string MainTabMenuText
        {
            get { return _MainTabMenuText; }
            set { _MainTabMenuText = value; }
        }
        public string MainTabMenuTextOver
        {
            get { return _MainTabMenuTextOver; }
            set { _MainTabMenuTextOver = value; }
        }
        public string MainTabMenuBack 
        {
            get { return _MainTabMenuBack; }
            set { _MainTabMenuBack = value; }
        }
        public string MainTabMenuBackOver
        {
            get { return _MainTabMenuBackOver; }
            set { _MainTabMenuBackOver = value; }
        }
        public string MainTabMenuBorder
        {
            get { return _MainTabMenuBorder; }
            set { _MainTabMenuBorder = value; }
        }
        public string MainTabMenuBorderOver
        {
            get { return _MainTabMenuBorderOver; }
            set { _MainTabMenuBorderOver = value; }
        }
        public string MenuBottomLine
        {
            get { return _MenuBottomLine; }
            set { _MenuBottomLine = value; }
        }
        public string SubTabMenuText
        {
            get { return _SubTabMenuText; }
            set { _SubTabMenuText = value; }
        }
        public string SubTabMenuTextOver
        {
            get { return _SubTabMenuTextOver; }
            set { _SubTabMenuTextOver = value; }
        }
        public string MainForm
        {
            get { return _MainForm; }
            set { _MainForm = value; }
        }
        public string Footer
        {
            get { return _Footer; }
            set { _Footer = value; }
        }
        public string FooterText
        {
            get { return _FooterText; }
            set { _FooterText = value; }
        }
        public int CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        public string PopUpTabMainColor
        {
            get { return _PopUpTabMainColor; }
            set { _PopUpTabMainColor = value; }
        }
        public string PopUpTabText
        {
            get { return _PopUpTabText; }
            set { _PopUpTabText = value; }
        }
        public string PopUpTabSelectedText
        {
            get { return _PopUpTabSelectedText; }
            set { _PopUpTabSelectedText = value; }
        }
        #endregion        
    }
}
