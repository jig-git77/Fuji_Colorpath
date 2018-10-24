#region System
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Fuji.Business
{
    public abstract class PressJobs
    {
        #region Variables
        private string _TableName = "Press.Jobs";
        private string _DongleID = "";
        private int _JobID = 0;
        private int _PressID = 0;
        private string _JobName = "";
        private int _JobCurrentSheetNum = 0;
        private string _JobCreatedDT = "";
        private string _JobCreatedGMT = "";
        private Int16 _IllumObsTypeID = 0;
        private Int16 _DeltaETypeId = 0;
        private string _Substrate = "";
        private decimal _SheetSize = 0;
        private string _Customer = "";
        private bool _IsUsed = true;
        #endregion

        #region Property
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public string DongleID
        {
            get { return _DongleID; }
            set { _DongleID = value; }
        }
        public int PressID
        {
            get { return _PressID; }
            set { _PressID = value; }
        }
        public int JobID
        {
            get { return _JobID; }
            set { _JobID = value; }
        }
        public string JobName
        {
            get { return _JobName; }
            set { _JobName = value; }
        }
        public int JobCurrentSheetNum
        {
            get { return _JobCurrentSheetNum; }
            set { _JobCurrentSheetNum = value; }
        }
        public string JobCreatedDT
        {
            get { return _JobCreatedDT; }
            set { _JobCreatedDT = value; }
        }
        public string JobCreatedGMT
        {
            get { return _JobCreatedGMT; }
            set { _JobCreatedGMT = value; }
        }
        public Int16 IllumObsTypeID
        {
            get { return _IllumObsTypeID; }
            set { _IllumObsTypeID = value; }
        }
        public Int16 DeltaETypeId
        {
            get { return _DeltaETypeId; }
            set { _DeltaETypeId = value; }
        }
       
        public string Substrate
        {
            get { return _Substrate; }
            set { _Substrate = value; }
        }
        public decimal SheetSize
        {
            get { return _SheetSize; }
            set { _SheetSize = value; }
        }
        public string Customer
        {
            get { return _Customer; }
            set { _Customer = value; }
        }
        public bool IsUsed
        {
            get { return _IsUsed; }
            set { _IsUsed = value; }
        }        
        #endregion
    }
}
