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
    public abstract class CPSyncPrintCondition
    {
        #region Variables
        private string _TableName = "PrintCondition";
        private short _PrintCondtionID = 0;
        private short _ProjectID = 0;
        private string _PaperManufacturer = "";
        private string _PaperName = "";
        private string _PaperFinish = "";
        private string _PaperGrade = "";
        private string _PaperWeight = "";
        private string _PaperWhite = "";
        private string _InkManufacturer = "";
        private string _InkSetName = "";
        private Boolean _ISO2846Compliant = false;
        private string _BlanketManufacturer = "";
        private string _BlanketName = "";
        private string _BlanketAge = "";
        private string _FountainSolutionMfg = "";
        private string _FountainSolutionName = "";
        private string _Ink1 = "";
        private string _Ink2 = "";
        private string _Ink3 = "";
        private string _Ink4 = "";
        private string _DotShape = "";
        private int _DotSize = 0;
        private string _ScreeningType = "";
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public short PrintCondtionID { get { return _PrintCondtionID; } set { _PrintCondtionID = value; } }
        public short ProjectID { get { return _ProjectID; } set { _ProjectID = value; } }
        public string PaperManufacturer { get { return _PaperManufacturer; } set { _PaperManufacturer = value; } }
        public string PaperName { get { return _PaperName; } set { _PaperName = value; } }
        public string PaperFinish { get { return _PaperFinish; } set { _PaperFinish = value; } }
        public string PaperGrade { get { return _PaperGrade; } set { _PaperGrade = value; } }
        public string PaperWeight { get { return _PaperWeight; } set { _PaperWeight = value; } }
        public string PaperWhite { get { return _PaperWhite; } set { _PaperWhite = value; } }
        public string InkManufacturer { get { return _InkManufacturer; } set { _InkManufacturer = value; } }
        public string InkSetName { get { return _InkSetName; } set { _InkSetName = value; } }
        public Boolean ISO2846Compliant { get { return _ISO2846Compliant; } set { _ISO2846Compliant = value; } }
        public string BlanketManufacturer { get { return _BlanketManufacturer; } set { _BlanketManufacturer = value; } }
        public string BlanketName { get { return _BlanketName; } set { _BlanketName = value; } }
        public string BlanketAge { get { return _BlanketAge; } set { _BlanketAge = value; } }
        public string FountainSolutionMfg { get { return _FountainSolutionMfg; } set { _FountainSolutionMfg = value; } }
        public string FountainSolutionName { get { return _FountainSolutionName; } set { _FountainSolutionName = value; } }
        public string Ink1 { get { return _Ink1; } set { _Ink1 = value; } }
        public string Ink2 { get { return _Ink2; } set { _Ink2 = value; } }
        public string Ink3 { get { return _Ink3; } set { _Ink3 = value; } }
        public string Ink4 { get { return _Ink4; } set { _Ink4 = value; } }
        public string DotShape { get { return _DotShape; } set { _DotShape = value; } }
        public int DotSize { get { return _DotSize; } set { _DotSize = value; } }
        public string ScreeningType { get { return _ScreeningType; } set { _ScreeningType = value; } }
        
        #endregion
    }
}
