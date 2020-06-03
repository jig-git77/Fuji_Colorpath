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
    public abstract class SiteLicense
    {

        #region Variables
        private int _TaskeroUserID; 
        private string _SiteLicesneText = "";
        private string _IPAddress = "";
        private DateTime _AccedptedDate;
        private int _SiteLicenseTypeID = 0;
        private bool _SiteLicenseAgreed = false;
        #endregion

        #region Properties
        public int TaskeroUserID
        {
            get { return _TaskeroUserID; }
            set { _TaskeroUserID = value; }
        }

        public string SiteLicesneText
        {
            get { return _SiteLicesneText; }
            set { _SiteLicesneText = value; }
        }

        public DateTime AcceptedDate
        {
            get { return _AccedptedDate; }
            set { _AccedptedDate = value; }
        }
        
        public  string RemoteHost
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
        }
       
        public bool SiteLicenseAgreed
        {
            get { return _SiteLicenseAgreed; }
            set { _SiteLicenseAgreed = value; }
        }

        public int SiteLicenseTypeID
        {
            get { return _SiteLicenseTypeID; }
            set { _SiteLicenseTypeID = value; }
        }
        #endregion



    }
}
