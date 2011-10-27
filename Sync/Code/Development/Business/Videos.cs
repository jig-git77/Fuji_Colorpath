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
    public abstract class Videos
    {
        #region Variables
        private string _TableName = "Categories";
        private int _VideoID = 0;
        private int _CategoryID = 0;
        private string _LinkText = "";
        private string _VideoFile = "";
        private string _Description = "";
        private string _Keywords = "";
        private int _UploadTo = 0;
        private int _UploadedBy = 0;
        private DateTime _UploadedOn;
        private int _ModifiedBy = 0;
        private DateTime _ModifiedOn;
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public int VideoID
        {
            get { return _VideoID; }
            set { _VideoID = value; }
        }
        public int CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }
        public string LinkText
        {
            get { return _LinkText; }
            set { _LinkText = value; }
        }
        public string VideoFile
        {
            get { return _VideoFile; }
            set { _VideoFile = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public string Keywords
        {
            get { return _Keywords; }
            set { _Keywords = value; }
        }
        public int UploadTo
        {
            get { return _UploadTo; }
            set { _UploadTo = value; }
        }
        public int UploadedBy
        {
            get { return _UploadedBy; }
            set { _UploadedBy = value; }
        }
        public DateTime UploadedOn
        {
            get { return _UploadedOn; }
            set { _UploadedOn = value; }
        }
        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        public DateTime ModifiedOn
        {
            get { return _ModifiedOn; }
            set { _ModifiedOn = value; }
        }
        #endregion
    }
}
